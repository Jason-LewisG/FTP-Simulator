//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stardome.DomainObjects
{
    using System;
    using System.Collections.Generic;
    
    public partial class Action
    {
        public Action()
        {
            this.Logs = new HashSet<Log>();
        }
    
        public int Id { get; set; }
        public string Action1 { get; set; }
    
        public virtual ICollection<Log> Logs { get; set; }
    }
}