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
    
    public partial class Shot
    {
        public int Shot_ID { get; set; }
        public string Score_Title { get; set; }
        public double Score_Value { get; set; }
        public System.Guid User_ID { get; set; }
        public string Shot_Seq_Num { get; set; }
        public int Visit_ID { get; set; }
    
        public virtual User User { get; set; }
        public virtual Visit Visit { get; set; }
    }
}
