using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AffiliateMarketingAutomation.CoreDataLayer.Models
{
    [Table("OfferProducts", Schema = "Offer")]
    public class OfferProduct
    {
        [Key]
        public string productId { get; set; }
        public Nullable<System.DateTime> validTill { get; set; }
        public string shotTitle { get; set; }
        public string title { get; set; }
        public string productDescription { get; set; }
        public string imageUrls_200 { get; set; }
        public string imageUrls_400 { get; set; }
        public string imageUrls_800 { get; set; }
        public string productFamily { get; set; }
        public decimal maximumRetailPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal SpecialPrice { get; set; }
        public string currency { get; set; }
        public string productUrl { get; set; }
        public string productBrand { get; set; }
        public Nullable<bool> inStock { get; set; }
        public Nullable<bool> codAvailable { get; set; }
        public Nullable<decimal> discountPercentage { get; set; }
        public string offers { get; set; }
        public string categoryPath { get; set; }
        public string attributes { get; set; }
        public Nullable<decimal> shippingCharges { get; set; }
        public string estimatedDeliveryTime { get; set; }
        public string sellerName { get; set; }
        public Nullable<decimal> sellerAverageRating { get; set; }
        public Nullable<decimal> sellerNoOfRatings { get; set; }
        public Nullable<decimal> sellerNoOfReviews { get; set; }
        public string keySpecs { get; set; }
        public string detailedSpecs { get; set; }
        public string specificationList { get; set; }
        public string booksInfo { get; set; }
        public string lifeStyleInfo { get; set; }
        public Nullable<bool> IsUpdated { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string SiteName { get; set; }
    }
}
