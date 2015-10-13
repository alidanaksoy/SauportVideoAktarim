using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoAktarim.Models
{
    public class AktarimVM
    {
    }
    public class SourceModel
    {
        public string ProgramName { get; set; }
        public Guid MCourseId { get; set; }
        public string CourseName { get; set; }
        public Guid ElessonId { get; set; }
        public string Title { get; set; }
    }
    public class TemplateModel
    {
        public Guid? PROGRAMID { get; set; }
        public Guid? TEMPLATEID { get; set; }
        public Guid? MCOURSEID { get; set; }
        public Guid? COURSEID { get; set; }
        public string TITLE { get; set; }
        public string TEXTFIELD1 { get; set; }
        public string TEXTFIELD2 { get; set; }
        public string TEXTFIELD3 { get; set; }
        public Nullable<double> NUMBERFIELD1 { get; set; }
        public int NUMBERFIELD2 { get; set; }
        public int NUMBERFIELD3 { get; set; }
        public Guid? REFID { get; set; }
        public string URL { get; set; }
        public int STARTWEEK { get; set; }
        public int ENDWEEK { get; set; }
    }
    public class ElessonComparer : IEqualityComparer<TemplateModel>
    {
        public bool Equals(TemplateModel x, TemplateModel y)
        {
            if (x.REFID == y.REFID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(TemplateModel obj)
        {
            return obj.REFID.GetHashCode();
        }
    }
}