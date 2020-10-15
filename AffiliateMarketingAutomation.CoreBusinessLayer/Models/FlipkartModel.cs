using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.Models
{
    public class FlipkartModel
    {
    }
    public class ProductCatagory
    {
        public string resourceName { get; set; }
        public string apiName { get; set; }
        public string getApi { get; set; }
        public string deltaGet { get; set; }

    }
    public class FlipKartProfuctCatagory
    {
        public string title { get; set; }
        public string description { get; set; }
        public List<apiGroups> apiGroups { get; set; }
    }

    public class apiGroups
    {
        public List<affiliate> affiliate { get; set; }
    }

    public class affiliate
    {
        public string name { get; set; }
        public List<apiListings> apiListings { get; set; }
    }

    public class apiListings
    {
        public List<catagories> catagories { get; set; }
        public string apiName { get; set; }
    }

    public class catagories
    {
        public List<availableVariants> availableVariants { get; set; }
    }

    public class availableVariants
    {
        public List<V1> v1 { get; set; }
    }

    public class V1
    {
        public string resourceName { get; set; }
        public string put { get; set; }
        public string post { get; set; }
        public string get { get; set; }
        public string deltaGet { get; set; }
        public string delete { get; set; }
    }

    public class FlipkartAllOffers
    {
        public FlipkartAllOffers()
        {
            allOffersList = new List<DealsOfTheDayResponseModel>();
            dotdList = new List<DealsOfTheDayResponseModel>();
        }
        public List<DealsOfTheDayResponseModel> allOffersList { get; set; }
        public List<DealsOfTheDayResponseModel> dotdList { get; set; }
    }

    public class DealsOfTheDayResponseModel
    {
        public DealsOfTheDayResponseModel()
        {
            imageUrls = new List<imageUrl>();
        }

        public long startTime { get; set; }
        public long endTime { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string category { get; set; }
        public List<imageUrl> imageUrls { get; set; }
        public string availability { get; set; }
    }
    public class imageUrl
    {
        public string url { get; set; }
        public string resolutionType { get; set; }
    }
}
