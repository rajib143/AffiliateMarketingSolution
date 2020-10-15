using AMA.CoreBusinessLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.ConsumeAPIs
{
    public class FlipkartAPI
    {
        public static List<ProductCatagory> GetFlipkartProductCategorys(Setting setting)
        {
            try
            {
                List<ProductCatagory> productCatagory = new List<ProductCatagory>();
                Task<string> response = Task.Run<string>(async () => await FlipkartAPIResult.GetFlipkartProductCategory(setting));
                var resultDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Result);
                var apiGroups = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultDic["apiGroups"].ToString());
                var affiliate = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiGroups["affiliate"].ToString());
                var apiListings = JsonConvert.DeserializeObject<Dictionary<string, object>>(affiliate["apiListings"].ToString());

                //foreach (string key in apiListings.Keys)
                //{
                Parallel.ForEach(apiListings.Keys, (key) =>
                {
                    try
                    {
                        string apiname = key;
                        string value = apiListings[key].ToString();
                        var availableVariants = JsonConvert.DeserializeObject<Dictionary<string, object>>(value);
                        var v1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(availableVariants["availableVariants"].ToString());
                        var vresource = JsonConvert.DeserializeObject<Dictionary<string, string>>(v1["v1.1.0"].ToString());

                        productCatagory.Add(new ProductCatagory()
                        {
                            apiName = apiname,
                            resourceName = vresource["resourceName"],
                            getApi = vresource["get"],
                            deltaGet = vresource["deltaGet"],

                        });
                    }
                    catch (Exception ex)
                    {
                    }
                });
                //  }
                return productCatagory;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static FlipkartProducts GetFlipkartCategoryProducts(Setting setting, string url)
        {
            try
            {
                Task<string> response = Task.Run<string>(async () => await FlipkartAPIResult.GetFlipkartCategoryProduct(setting, url));
                var flipkartProducts = JsonConvert.DeserializeObject<FlipkartProducts>(response.Result);
                return flipkartProducts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
