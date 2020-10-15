using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.Models
{
    public class Setting
    {

        public string FlipkartAllOffersApiUrl { get; set; }
        public string FlipkartDealsOfTheDayOfferApiUrl { get; set; }
        public string FlipkartProductCatagoryApiUrl { get; set; }
        //public string FlipkartCategoryProductApiUrl { get; set; }
        public string FkAffiliateId { get; set; }
        public string FkAffiliateToken { get; set; }
        public Setting()
        {
            //FlipkartAllOffersApiUrl = Environment.GetEnvironmentVariable("FlipkartAllOffersApiUrl");
            //FlipkartDealsOfTheDayOfferApiUrl = Environment.GetEnvironmentVariable("FlipkartDealsOfTheDayOfferApiUrl");
            //FlipkartProductCatagoryApiUrl = Environment.GetEnvironmentVariable("FlipkartProductCatagoryApiUrl");
            //FkAffiliateId = Environment.GetEnvironmentVariable("FkAffiliateId");
            //FkAffiliateToken = Environment.GetEnvironmentVariable("FkAffiliateToken");

            FlipkartAllOffersApiUrl = "https://affiliate-api.flipkart.net/affiliate/offers/v1/all/json";
            FlipkartDealsOfTheDayOfferApiUrl = "https://affiliate-api.flipkart.net/affiliate/offers/v1/dotd/json";
            FlipkartProductCatagoryApiUrl = "https://affiliate-api.flipkart.net/affiliate/api/rajibmaha.json";
            FkAffiliateId = "rajibmaha";
            FkAffiliateToken = "d2f2e72f8e2a4aff946adb037d647ca1";

        }

        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            try
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
                return dtDateTime;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       
    }
}
