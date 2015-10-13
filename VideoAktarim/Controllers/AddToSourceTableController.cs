using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoAktarim.Models;

namespace VideoAktarim.Controllers
{
    // Template_2 tablosunda hazır hale getirilen videoları programid ye göre derslere basıyor.(Bu son aşama)

    public class AddToSourceTableController : Controller
    {
        SAUPORTDB db = new SAUPORTDB();

        public ActionResult Index()
        {
            AddSource();
            return View();
        }
        public void AddSource()
        {
            Guid programid = Guid.Parse("b85c91d1-1017-4570-aac0-88cf626ae288");

            Guid mcourseid = Guid.Parse("B85B7184-EBE2-4189-B012-88D19CA0F337");

            Guid termid = Guid.Parse("10836C03-8779-4E45-88BF-88D2B9B2D40C");

            var liste = (from temp in db.WL_SR_ST_TEMPLATE_2
                         join pm in db.AL_CS_MT_PMCOURSE on temp.MCOURSEID equals pm.MCOURSEID
                         join pr in db.AL_ST_PROGRAMS on pm.PROGRAMID equals pr.PROGRAMID
                         join cs in db.AL_CS_ST_COURSE on temp.MCOURSEID equals cs.MCOURSEID
                         where pr.PROGRAMID == programid && cs.PROGRAMID == programid && cs.TERMID == termid && temp.MCOURSEID == mcourseid
                         select new TemplateModel{ PROGRAMID = pr.PROGRAMID, MCOURSEID = temp.MCOURSEID, COURSEID = cs.COURSEID, TEMPLATEID = temp.TEMPLATEID, REFID = temp.REFID, TITLE = temp.TITLE, TEXTFIELD1 = temp.TEXTFIELD1, NUMBERFIELD1 = temp.NUMBERFIELD1, URL = temp.SRCURL }).ToList().Distinct(new ElessonComparer()).OrderBy(t => t.MCOURSEID);

            ViewBag.liste = liste;


            foreach (var item in liste)
            {
                var model = new AL_SR_ST_SOURCE();
                model.ACTIVITYID = Guid.NewGuid();
                model.COURSEID = item.COURSEID;
                if (item.TITLE.Length > 49)
                {
                    model.ACTIVITYNAME = item.TITLE.Substring(0, 48);
                }
                else
                {
                    model.ACTIVITYNAME = item.TITLE;
                }
                model.ENROLLMENTTYPE = 1;
                if (string.IsNullOrWhiteSpace(item.TEXTFIELD1))
                {
                    model.STARTWEEK = 1;
                }
                else
                {
                    model.STARTWEEK = Convert.ToInt32(item.TEXTFIELD1);
                }

                if (string.IsNullOrWhiteSpace(item.NUMBERFIELD1.ToString()))
                {
                    model.ENDWEEK = Convert.ToInt32(item.NUMBERFIELD1);
                }
                else
                {
                    model.ENDWEEK = model.STARTWEEK;
                }
                model.TEXTFIELD2 = "SIYASETBILIMIVERGIHUKUKU";
                model.USERCREATED = Guid.Parse("3a7069ef-3027-4b0f-bd18-88cf65e6be9c");
                model.DATECREATED = DateTime.Now;
                model.USERMODIFIED = Guid.Parse("3a7069ef-3027-4b0f-bd18-88cf65e6be9c");
                model.DATEMODIFIED = DateTime.Now;

                if (item.URL.Contains("G:\\Content\\SAUPORT\\Video\\"))
                {
                    model.SRCURL = item.URL.Replace("G:\\Content\\SAUPORT\\Video\\", "http://www.sauportcontent.sakarya.edu.tr/Videos/").Replace("\\", "/");
                }
                else if (item.URL.Contains("/Files/Source/Templates/"))
                {
                    model.SRCURL = item.URL.Replace("/Files/Source/Templates/", "http://www.sauportcontent.sakarya.edu.tr/Videos/");
                }

                model.DBSTATUS = 0;
                model.SRCTYPE = "flv";
                model.FEEDBACK = null;
                model.ITEMID = null;
                model.ACTIVITYCODE = null;
                model.ITEMORDER = 0;
                model.ISOPTIONAL = 0;
                model.SCOREABLE = 0;
                model.PASSINGSCORE = 0;
                db.AL_SR_ST_SOURCE.Add(model); // SOURCE TABLOSUNA EKLENDİ.

                var groupid = db.AL_CS_ST_GROUP.Where(g => g.COURSEID == model.COURSEID).FirstOrDefault().GROUPID;
                var activityGroup = new AL_CS_MT_ACTIVITY_GROUP();
                activityGroup.GROUPID = groupid;
                activityGroup.ACTIVITYID = model.ACTIVITYID;
                activityGroup.TIMEADDED = DateTime.Now;
                db.AL_CS_MT_ACTIVITY_GROUP.Add(activityGroup);

                var templatemodel = db.WL_SR_ST_TEMPLATE_2.Where(t => t.TEMPLATEID == item.TEMPLATEID).FirstOrDefault();
                templatemodel.SOURCEADDED = 99999;

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                }
            }
        }
    }
}