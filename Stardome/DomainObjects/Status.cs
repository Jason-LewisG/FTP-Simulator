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
    
    public partial class Status
    {
        public Status()
        {
            this.Files = new HashSet<File>();
            this.Folders = new HashSet<Folder>();
        }
    
        public int Id { get; set; }
        public string Status1 { get; set; }
    
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }
    }
}
