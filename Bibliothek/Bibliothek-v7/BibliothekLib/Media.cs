//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BibliothekLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class Media
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int WorkId { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual Work Work { get; set; }
        public virtual User User { get; set; }
    }
}