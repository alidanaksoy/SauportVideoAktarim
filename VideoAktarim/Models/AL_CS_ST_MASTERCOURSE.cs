//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VideoAktarim.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AL_CS_ST_MASTERCOURSE
    {
        public System.Guid MCOURSEID { get; set; }
        public string MCOURSECODE { get; set; }
        public string MCOURSENAME { get; set; }
        public string DESCRIPTION { get; set; }
        public int CREDITS { get; set; }
        public Nullable<int> ECTS_CREDITS { get; set; }
        public int CREDIT_THEORIC { get; set; }
        public int CREDIT_APPLICATION { get; set; }
        public byte ISCOMMON { get; set; }
        public Nullable<System.DateTime> DATECREATED { get; set; }
        public string MCOURSEID_OLD { get; set; }
        public string SYNCKEY { get; set; }
        public Nullable<System.DateTime> SYNCDATE { get; set; }
        public byte AUTOSYNC { get; set; }
        public string SYNCNEWKEY { get; set; }
    }
}
