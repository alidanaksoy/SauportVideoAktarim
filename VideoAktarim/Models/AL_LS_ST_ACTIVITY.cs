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
    
    public partial class AL_LS_ST_ACTIVITY
    {
        public System.Guid ACTIVITYID { get; set; }
        public Nullable<System.Guid> COURSEID { get; set; }
        public Nullable<System.Guid> ITEMID { get; set; }
        public int ITEMORDER { get; set; }
        public string ACTIVITYNAME { get; set; }
        public string ACTIVITYCODE { get; set; }
        public byte ENROLLMENTTYPE { get; set; }
        public Nullable<byte> FEEDBACK { get; set; }
        public string DESCRIPTION { get; set; }
        public byte ISOPTIONAL { get; set; }
        public byte SCOREABLE { get; set; }
        public decimal PASSINGSCORE { get; set; }
        public Nullable<int> STARTWEEK { get; set; }
        public Nullable<int> STARTDAY { get; set; }
        public Nullable<int> ENDWEEK { get; set; }
        public Nullable<int> ENDDAY { get; set; }
        public string TEXTFIELD1 { get; set; }
        public string TEXTFIELD2 { get; set; }
        public string TEXTFIELD3 { get; set; }
        public Nullable<double> NUMBERFIELD1 { get; set; }
        public Nullable<double> NUMBERFIELD2 { get; set; }
        public Nullable<double> NUMBERFIELD3 { get; set; }
        public System.DateTime DATECREATED { get; set; }
        public System.DateTime DATEMODIFIED { get; set; }
        public System.Guid USERCREATED { get; set; }
        public System.Guid USERMODIFIED { get; set; }
        public Nullable<byte> DBSTATUS { get; set; }
        public Nullable<int> TOTALPAGE { get; set; }
        public string FINISHMESSAGE { get; set; }
        public string MESSAGETOSHOW { get; set; }
        public byte TRACKING { get; set; }
        public byte AUTOSCORRING { get; set; }
        public Nullable<System.Guid> SACTIVITYID { get; set; }
        public Nullable<System.Guid> OLDITEMID { get; set; }
    }
}