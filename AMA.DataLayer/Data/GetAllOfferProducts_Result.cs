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
    
    public partial class GetAllOfferProducts_Result
    {
        public string productId { get; set; }
        public string categoryPath { get; set; }
        public string title { get; set; }
        public string productDescription { get; set; }
        public string productUrl { get; set; }
        public string imageUrls_200 { get; set; }
        public string imageUrls_400 { get; set; }
        public string imageUrls_800 { get; set; }
        public string currency { get; set; }
        public Nullable<decimal> flipkartSellingPrice { get; set; }
        public decimal flipkartSpecialPrice { get; set; }
        public decimal maximumRetailPrice { get; set; }
        public Nullable<decimal> discountPercentage { get; set; }
        public string offers { get; set; }
        public string attributes { get; set; }
        public string productBrand { get; set; }
    }
}
