//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mds3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SeriesEnty
    {
        public int SeriesEntryId { get; set; }
        
        public Nullable<int> PlayerId { get; set; }
    

        public Nullable<int> FormatId { get; set; }
    
        public virtual Format Format { get; set; }
        public virtual Player Player { get; set; }
    }
}
