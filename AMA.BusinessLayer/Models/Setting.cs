using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.Models
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
            FlipkartAllOffersApiUrl = ConfigurationManager.AppSettings["FlipkartAllOffersApiUrl"];
            FlipkartDealsOfTheDayOfferApiUrl = ConfigurationManager.AppSettings["FlipkartDealsOfTheDayOfferApiUrl"];
            FlipkartProductCatagoryApiUrl = ConfigurationManager.AppSettings["FlipkartProductCatagoryApiUrl"];
            FkAffiliateId = ConfigurationManager.AppSettings["FkAffiliateId"];
            FkAffiliateToken = ConfigurationManager.AppSettings["FkAffiliateToken"];


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
