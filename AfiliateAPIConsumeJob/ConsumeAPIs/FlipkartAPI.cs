using AfiliateAPIConsumeJob.BusinessLayer;
using AfiliateAPIConsumeJob.Data;
using AfiliateAPIConsumeJob.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiliateAPIConsumeJob.ConsumeAPIs
{
    public class FlipkartAPI :  IFipkartAPI
    {
        
        public void ProcessOffer(Setting setting)
        {
            try
            {
                Task.Run(() =>
                {
                    List<ProductCatagory> productCatagories = FlipkartBL.GetFlipkartProductCategorys(setting);

                    List<Product> products = new List<Product>();

                    foreach (var item in productCatagories)
                    {
                        var result = FlipkartBL.GetFlipkartCategoryProducts(setting, item.getApi).products.ToList();
                        foreach (var product in result.Where(x => x.productBaseInfoV1.inStock = true))
                        {
                            
                            products.Add(product);
                        }
                    }

                    //TODO : bulk insertion 
                    //FlipkartDataLayer.InsertOrUpdateIntoOfferProducts(product);

                }).Wait();

                Task.Run(() =>
                {
                    ProcessAllOffers(setting);

                }).Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcessAllOffers(Setting setting)
        {
            try
            {
                Task.Run(() =>
                {
                    string strAllOffers = APIResult.GetAllFlipkartOffers(setting).Result;

                    FlipkartAllOffers allOffers = JsonConvert.DeserializeObject<FlipkartAllOffers>(strAllOffers);

                    foreach (var item in allOffers.allOffersList)
                    {

                        FlipkartDataLayer.InsertOrUpdateIntoAllOffers(item);
                    }
                }).Wait();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RemoveOldOffers(Setting setting)
        {
            try
            {
                Task.Run(() =>
                {

                    FlipkartDataLayer.RemoveOldOfferProducts();

                }).Wait();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ProcessDealsOfTheDayOffers(Setting setting)
        {

        }
    }
}
