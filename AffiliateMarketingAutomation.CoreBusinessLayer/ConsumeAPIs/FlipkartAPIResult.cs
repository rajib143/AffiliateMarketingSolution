using AMA.CoreBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.ConsumeAPIs
{
    public class FlipkartAPIResult
    {
        public static async Task<string> GetFlipkartAllOffersAPIInformation(Setting setting)
        {
            try
            {
                string fileJsonString = string.Empty;
                //string url = "https://affiliate-api.flipkart.net/affiliate/offers/v1/all/json";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", setting.FkAffiliateId);
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", setting.FkAffiliateToken);

                    var response = await client.GetAsync(setting.FlipkartAllOffersApiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        fileJsonString = await response.Content.ReadAsStringAsync();
                    }
                }
                return fileJsonString;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static async Task<string> GetFlipkartProductCategory(Setting setting)
        {
            try
            {
                string fileJsonString = string.Empty;
                //string url = "https://affiliate-api.flipkart.net/affiliate/offers/v1/all/json";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", setting.FkAffiliateId);
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", setting.FkAffiliateToken);

                    var response = await client.GetAsync(setting.FlipkartProductCatagoryApiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        fileJsonString = await response.Content.ReadAsStringAsync();
                    }
                }
                return fileJsonString;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static async Task<string> GetFlipkartCategoryProduct(Setting setting, string url)
        {
            try
            {
                string fileJsonString = string.Empty;
                //string url = "https://affiliate-api.flipkart.net/affiliate/offers/v1/all/json";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", setting.FkAffiliateId);
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", setting.FkAffiliateToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        fileJsonString = await response.Content.ReadAsStringAsync();
                    }
                }
                return fileJsonString;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static async Task<string> GetAllFlipkartOffers(Setting setting)
        {
            try
            {
                string fileJsonString = string.Empty;
                //string url = "https://affiliate-api.flipkart.net/affiliate/offers/v1/all/json";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", setting.FkAffiliateId);
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", setting.FkAffiliateToken);

                    var response = await client.GetAsync(setting.FlipkartAllOffersApiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        fileJsonString = await response.Content.ReadAsStringAsync();
                    }
                }
                return fileJsonString;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<string> GetDealsOfTheDayOffer(Setting setting)
        {
            try
            {
                string fileJsonString = string.Empty;
                //string url = "https://affiliate-api.flipkart.net/affiliate/offers/v1/all/json";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", setting.FkAffiliateId);
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", setting.FkAffiliateToken);

                    var response = await client.GetAsync(setting.FlipkartDealsOfTheDayOfferApiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        fileJsonString = await response.Content.ReadAsStringAsync();
                    }
                }
                return fileJsonString;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
