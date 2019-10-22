using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMA.BusinessLayer.ConsumeAPIs;
using AMA.BusinessLayer.Models;
using AMA.DataLayer.Data;
using Newtonsoft.Json;

namespace AMA.BusinessLayer.Implementation
{
    public class FlipkartBL
    {
        public IEntity<OfferProduct> offerproductEntity;
        public IEntity<AllOffer> allOfferEntity;
        public Setting setting;
        public FlipkartBL()
        {
            offerproductEntity = new Entity<OfferProduct>();
            allOfferEntity = new Entity<AllOffer>();
            setting = new Setting();
        }

        public async Task ProcessOfferProducts()
        {
            try
            {
                List<ProductCatagory> productCatagories = FlipkartAPI.GetFlipkartProductCategorys(setting);
                List<Product> products = new List<Product>();
                
                Parallel.ForEach(productCatagories.ToList(), (item) =>
                {
                    var result = FlipkartAPI.GetFlipkartCategoryProducts(setting, item.getApi).products.ToList();
                    products.AddRange(result.Where(x => x.productBaseInfoV1.inStock = true));
                });

                //foreach (var item in productCatagories)
                //{
                //    var result = FlipkartAPI.GetFlipkartCategoryProducts(setting, item.getApi).products.ToList();

                //    products.AddRange(result.Where(x => x.productBaseInfoV1.inStock = true));
                //    //foreach (var product in result.Where(x => x.productBaseInfoV1.inStock = true))
                //    //{
                //    //    products.Add(product);
                //    //}
                //}

                //TODO : bulk insertion 
                //FlipkartDataLayer.InsertOrUpdateIntoOfferProducts(product);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task ProcessAllOffers()
        {
            try
            {
                string strAllOffers = FlipkartAPIResult.GetAllFlipkartOffers(setting).Result;

                FlipkartAllOffers allOffers = JsonConvert.DeserializeObject<FlipkartAllOffers>(strAllOffers);
                List<DealsOfTheDay> Offers = new List<DealsOfTheDay>();
                Offers.AddRange(allOffers.allOffersList);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ProcessCatagory(string catagory)
        {
            try
            {
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
