//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMA.DataLayer.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class DealsOfTheDayOffer
    {
        public long ID { get; set; }
        public Nullable<System.DateTime> startTime { get; set; }
        public Nullable<System.DateTime> endTime { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string category { get; set; }
        public string imageUrls_default { get; set; }
        public string imageUrls_mid { get; set; }
        public string imageUrls_low { get; set; }
        public string availability { get; set; }
    }
}
