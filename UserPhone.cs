//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MoHEmotionPilot
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPhone
    {
        public System.Guid User_ID { get; set; }
        public string User_Phone_Num { get; set; }
    
        public virtual User User { get; set; }
    }
}
