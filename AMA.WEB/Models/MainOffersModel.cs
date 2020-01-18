using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMA.DataLayer.Data;

namespace AMA.WEB.Models
{
    public class MainOffersModel
    {
        public MainOffersModel()
        {
            fipkartAllOffers = new List<FipkartAllOffers>();
            DOTSOffers = new List<FipkartAllOffers>();
            offerProducts = new List<AMAOfferProduct>();
        }

        public List<FipkartAllOffers> fipkartAllOffers { get; set; }
        public List<FipkartAllOffers> DOTSOffers { get; set; }
        public List<AMAOfferProduct> offerProducts { get; set; }

    }

    public  class FipkartAllOffers
    {
        public long Id { get; set; }
        public Nullable<System.DateTime> startTime { get; set; }
        public Nullable<System.DateTime> endTime { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string category { get; set; }
        public string imageUrls_default { get; set; }
        public string imageUrls_low { get; set; }
        public string imageUrls_mid { get; set; }
        public string availability { get; set; }
        public Nullable<int> TotalCount { get; set; }
    }

    public class AMAOfferProduct
    {
        public string productId { get; set; }
        public string categoryPath { get; set; }
        public string shotTitle { get; set; }
        public string title { get; set; }
        public string productDescription { get; set; }
        public string productUrl { get; set; }
        public string imageUrls_200 { get; set; }
        public string imageUrls_400 { get; set; }
        public string imageUrls_800 { get; set; }
        public string currency { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal SpecialPrice { get; set; }
        public decimal maximumRetailPrice { get; set; }
        public Nullable<decimal> discountPercentage { get; set; }
        public string offers { get; set; }
        public string attributes { get; set; }
        public string productBrand { get; set; }
        public Nullable<long> ROWNUM { get; set; }
        public Nullable<int> TotalCount { get; set; }
    }
}