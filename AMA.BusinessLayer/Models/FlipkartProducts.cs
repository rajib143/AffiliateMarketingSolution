using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.Models
{
    public class FlipkartProducts
    {
        public FlipkartProducts()
        {
            products = new List<Product>();
        }
        public long validTill { get; set; }
        public List<Product> products { get; set; }
    }

    public class Product
    {
        public productBaseInfoV1 productBaseInfoV1 { get; set; }
        public productShippingInfoV1 productShippingInfoV1 { get; set; }
        public categorySpecificInfoV1 categorySpecificInfoV1 { get; set; }
    }

    public class productShippingInfoV1
    {
        public shippingCharge shippingCharges { get; set; }
        public string estimatedDeliveryTime { get; set; }
        public string sellerName { get; set; }
        public decimal? sellerAverageRating { get; set; }
        public decimal? sellerNoOfRatings { get; set; }
        public decimal? sellerNoOfReviews { get; set; }
    }

    public class shippingCharge
    {
        public decimal? amount { get; set; }
        public string currency { get; set; }
    }
    public class categorySpecificInfoV1
    {
        public object keySpecs { get; set; }
        public object detailedSpecs { get; set; }
        public object specificationList { get; set; }
        public object booksInfo { get; set; }
        public object lifeStyleInfo { get; set; }
    }

    public class productBaseInfoV1
    {
        public string productId { get; set; }
        public string title { get; set; }
        public string productDescription { get; set; }
        public Dictionary<string, string> imageUrls { get; set; }
        public object productFamily { get; set; }
        public Price maximumRetailPrice { get; set; }
        public Price flipkartSellingPrice { get; set; }
        public Price flipkartSpecialPrice { get; set; }
        public string productUrl { get; set; }
        public string productBrand { get; set; }
        public bool inStock { get; set; }
        public bool codAvailable { get; set; }
        public decimal discountPercentage { get; set; }
        public object offers { get; set; }
        public string categoryPath { get; set; }
        public object attributes { get; set; }

    }

    public class Price
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }


    public class imageUrls
    {
        public string x200 { get; set; }
        public string x400 { get; set; }
        public string x800 { get; set; }
    }
}
