//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GradebookOnlineApp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class oceny_etapow
    {
        public Nullable<int> id_etap { get; set; }
        public Nullable<int> id_prow { get; set; }
        public Nullable<int> id_student { get; set; }
        public string wartosc { get; set; }
    
        public virtual etapy etapy { get; set; }
        public virtual prowadzacy prowadzacy { get; set; }
        public virtual studenci studenci { get; set; }
    }
}
