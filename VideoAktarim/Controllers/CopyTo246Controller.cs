using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoAktarim.Models;

namespace VideoAktarim.Controllers
{
    //Hddye uygun formatta atılmış olan videoları alarak sunucuya kopyalar aynı zamanda TEMPLATE_2 tablosuna kaydeder.

    public class CopyTo246Controller : Controller
    {
        SAUPORTDB db = new SAUPORTDB();

        public ActionResult Index()
        {
            Copy246();
            return View();
        }
        
        public void Copy246()
        {
            var HddPath = @"H:\246Dosyalar\";
            var TasinacakDizin = @"H:\246HAZIR\";

            string[] AnaDersler = Directory.GetDirectories(HddPath);
            foreach (var AnaDers in AnaDersler)
            {
                var MCID = new DirectoryInfo(AnaDers).Name; //klasörden ANADERSID bulunuyor.

                string[] Aktiviteler = Directory.GetDirectories(AnaDers);
                foreach (var Aktivite in Aktiviteler)
                {
                    var EID = new DirectoryInfo(Aktivite).Name; // İç klasörden ELESSONID bulunuyor.
                    var vidPath = Directory.GetFiles(Aktivite); // hdd deki videonun yolu alınıyor.

                    if (!Directory.Exists(TasinacakDizin + MCID)) // taşınacak yerde anaders klasörü yoksa
                    {
                        Directory.CreateDirectory(TasinacakDizin + MCID); // önce bu klasör oluşturuluyor.
                    }
                    CreateTemplate(MCID, EID, vidPath[0]);

                }

            }
        }

        public void CreateTemplate(string MCourseId, string ElessonId, string vidUrl)
        {
            var HddPath = @"H:\246Dosyalar\";
            var TasinacakDizin = @"H:\246HAZIR\";

            if (System.IO.File.Exists(vidUrl))// hdd'de bu dizinde gerçekten bir video var mı?
            {
                WL_SR_ST_TEMPLATE_2 newTemplate = new WL_SR_ST_TEMPLATE_2();
                newTemplate.TEMPLATEID = Guid.NewGuid();
                newTemplate.MCOURSEID = Guid.Parse(MCourseId);
                newTemplate.REFID = Guid.Parse(ElessonId);
                newTemplate.TEXTFIELD2 = "ENDMUH.KAL.IY.YON.";
                newTemplate.USERCREATED = Guid.Parse("3a7069ef-3027-4b0f-bd18-88cf65e6be9c");
                newTemplate.DATECREATED = DateTime.Now;
                newTemplate.USERMODIFIED = Guid.Parse("3a7069ef-3027-4b0f-bd18-88cf65e6be9c");
                newTemplate.DATEMODIFIED = DateTime.Now;
                db.WL_SR_ST_TEMPLATE_2.Add(newTemplate);
                

                var vidName = new FileInfo(vidUrl).Name;
                if (!Directory.Exists(TasinacakDizin + MCourseId + @"\" + newTemplate.TEMPLATEID))
                {
                    Directory.CreateDirectory(TasinacakDizin + MCourseId + @"\" + newTemplate.TEMPLATEID);
                }
                System.IO.File.Copy(vidUrl, TasinacakDizin + MCourseId + @"\" + newTemplate.TEMPLATEID + @"\" + vidName);

                newTemplate.SRCURL = @"G:\Content\SAUPORT\Video\" + MCourseId + @"\" + newTemplate.TEMPLATEID + @"\" + vidName;

                db.SaveChanges();
            }
        }
    }
}