﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMA.BusinessLayer.ConsumeAPIs;
using AMA.BusinessLayer.Models;
using AMA.DataLayer.Data;
using Newtonsoft.Json;
using AMA.DataLayer;
using AMA.BusinessLayer.Interface;
using System.Linq.Expressions;
using log4net;
using AMA.BusinessLayer.Enum;
using AMA.BusinessLayer.Utility;

namespace AMA.BusinessLayer.Implementation
{
    public class FlipkartBL : IFlipkartBL
    {

        private readonly IOfferProductRepository _offerProductRepository;
        private readonly IAllOfferRepository _allOfferRepository;
        private readonly IDealsOfTheDayOfferRepository _dealsOfTheDayOfferRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly Setting setting;
        public FlipkartBL()
        {
            _offerProductRepository = new OfferProductRepository();
            _allOfferRepository = new AllOfferRepository();
            _categoryRepository = new CategoryRepository();
            _dealsOfTheDayOfferRepository = new DealsOfTheDayOfferRepository();
            setting = new Setting();
        }


        public async Task ProcessOfferProducts(ILog log)
        {
            try
            {
                List<ProductCatagory> productCatagories = FlipkartAPI.GetFlipkartProductCategorys(setting);
                foreach (var item in productCatagories.Where(x => x != null).ToList())
                {
                    try
                    {

                        FlipkartProducts result = new FlipkartProducts();

                        var responseProducts = FlipkartAPI.GetFlipkartCategoryProducts(setting, item.getApi);

                        result.validTill = responseProducts.validTill;
                        result.products.AddRange(responseProducts.products.Where(x => x.productBaseInfoV1.inStock = true));
                        // && x.productShippingInfoV1.sellerAverageRating.HasValue && x.productShippingInfoV1.sellerAverageRating > 0));

                        log.Info("Get " + result.products.Count() + " offer products from " + SiteName.Flipkart.ToString() + " for catagory " + item.resourceName);

                        if (result.products.Count() > 0)
                            await InserIntoOfferproducts(result, log);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error in " + SiteName.Flipkart.ToString() + " offer products processing for catagory " + item.resourceName, ex);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("Error in " + SiteName.Flipkart.ToString() + " offer products processing ", ex);
                throw ex;
            }
        }
        private async Task InserIntoOfferproducts(FlipkartProducts result, ILog log)
        {
            try
            {
                List<OfferProduct> offerProducts = new List<OfferProduct>();
                DateTime validTillDateTime = result.validTill.FromUnixTime();

                Parallel.ForEach(result.products.Where(x => x != null).OrderByDescending(x => x.productShippingInfoV1.sellerNoOfReviews).ToList(), (item) =>
                    {
                        try
                        {
                            string[] shotTitle = item.productBaseInfoV1.title.SetNullIfEmpty().Split(' ');

                        //var resultDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.productBaseInfoV1.imageUrls.ToString());
                        //!offerProducts.Any(x => !string.IsNullOrEmpty(x.productId) && x.productId.Equals(item.productBaseInfoV1.productId))&&
                        if (item != null && !string.IsNullOrEmpty(item.productBaseInfoV1.title)
                             //&& item.productBaseInfoV1.maximumRetailPrice.amount > 0
                             //&& item.productBaseInfoV1.flipkartSellingPrice.amount > 0
                             //&& item.productBaseInfoV1.flipkartSpecialPrice.amount > 0
                             && item.productBaseInfoV1.discountPercentage > 0)
                            {
                                offerProducts.Add(new OfferProduct()
                                {
                                    productId = item.productBaseInfoV1.productId,
                                    validTill = validTillDateTime,
                                    shotTitle = shotTitle.Count() > 3 ? shotTitle[0] + " " + shotTitle[1] + " " + shotTitle[2] + "@" + item.productBaseInfoV1.discountPercentage + "% OFF"
                                    : item.productBaseInfoV1.title + "@" + item.productBaseInfoV1.discountPercentage + "% OFF",
                                    title = item.productBaseInfoV1.title,
                                    productDescription = item.productBaseInfoV1.productDescription,
                                    imageUrls_200 = item.productBaseInfoV1.imageUrls["200x200"].SetNullIfEmpty(),
                                    imageUrls_400 = item.productBaseInfoV1.imageUrls["400x400"].SetNullIfEmpty(),
                                    imageUrls_800 = item.productBaseInfoV1.imageUrls["800x800"].SetNullIfEmpty(),
                                    productFamily = item.productBaseInfoV1.productFamily.SetNullIfEmpty(), //
                                maximumRetailPrice = item.productBaseInfoV1.maximumRetailPrice.amount,
                                    SellingPrice = item.productBaseInfoV1.flipkartSellingPrice.amount,
                                    SpecialPrice = item.productBaseInfoV1.flipkartSpecialPrice.amount,
                                    currency = item.productBaseInfoV1.maximumRetailPrice.currency,
                                    productUrl = item.productBaseInfoV1.productUrl.SetNullIfEmpty(),
                                    productBrand = item.productBaseInfoV1.productBrand.SetNullIfEmpty(),
                                    inStock = item.productBaseInfoV1.inStock,
                                    codAvailable = item.productBaseInfoV1.codAvailable,
                                    discountPercentage = item.productBaseInfoV1.discountPercentage,
                                    offers = item.productBaseInfoV1.offers.SetNullIfEmpty(),
                                    categoryPath = item.productBaseInfoV1.categoryPath.SetNullIfEmpty(),
                                    attributes = item.productBaseInfoV1.attributes.SetNullIfEmpty(),
                                    shippingCharges = item.productShippingInfoV1.shippingCharges.amount,
                                    estimatedDeliveryTime = item.productShippingInfoV1.estimatedDeliveryTime,
                                    sellerName = item.productShippingInfoV1.sellerName.SetNullIfEmpty(),
                                    sellerAverageRating = item.productShippingInfoV1.sellerAverageRating,
                                    sellerNoOfRatings = item.productShippingInfoV1.sellerNoOfRatings,
                                    sellerNoOfReviews = item.productShippingInfoV1.sellerNoOfReviews,
                                    keySpecs = item.categorySpecificInfoV1.keySpecs.SetNullIfEmpty(),
                                    detailedSpecs = item.categorySpecificInfoV1.detailedSpecs.SetNullIfEmpty(),
                                    specificationList = item.categorySpecificInfoV1.specificationList.SetNullIfEmpty(),
                                    booksInfo = item.categorySpecificInfoV1.booksInfo.SetNullIfEmpty(),
                                    lifeStyleInfo = item.categorySpecificInfoV1.lifeStyleInfo.SetNullIfEmpty(),
                                    IsUpdated = false,
                                    CreatedDate = DateTime.Now,
                                    SiteName = SiteName.Flipkart.ToString()
                                });

                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex.Message, ex);
                        }
                    //}
                });

                // bulk insertion 
                await AddBulkOfferProducts(offerProducts, log);
            }
            catch (Exception ex)
            {
                log.Error("Error in " + SiteName.Flipkart.ToString() + " InserIntoOfferproducts processing ", ex);
                // throw;
            }
        }
        public async Task ProcessAllOffers(ILog log)
        {
            try
            {
                string strAllOffers = FlipkartAPIResult.GetAllFlipkartOffers(setting).Result;

                FlipkartAllOffers flipkartAllOffers = JsonConvert.DeserializeObject<FlipkartAllOffers>(strAllOffers);
                List<Models.DealsOfTheDay> Offers = new List<Models.DealsOfTheDay>();
                List<AllOffer> allOffers = new List<AllOffer>();
                Offers.AddRange(flipkartAllOffers.allOffersList);

                log.Info("Get " + Offers.Count() + " AllOffers from " + SiteName.Flipkart.ToString());

                Parallel.ForEach(Offers.Where(x => x.url != null).ToList(), (item) =>
                   {
                       try
                       {
                           DateTime startTime = Setting.UnixTimeToDateTime(item.startTime);
                           DateTime endTime = Setting.UnixTimeToDateTime(item.endTime);
                           string imageUrls_default = item.imageUrls.FirstOrDefault(x => x.resolutionType == "default") != null ?
                                                                                   item.imageUrls.FirstOrDefault(x => x.resolutionType == "default").url : string.Empty;
                           string imageUrls_mid = item.imageUrls.FirstOrDefault(x => x.resolutionType == "mid") != null ?
                                                                                   item.imageUrls.FirstOrDefault(x => x.resolutionType == "mid").url : string.Empty;
                           string imageUrls_low = item.imageUrls.FirstOrDefault(x => x.resolutionType == "low") != null ?
                                                                                   item.imageUrls.FirstOrDefault(x => x.resolutionType == "low").url : string.Empty;
                           allOffers.Add(new AllOffer()
                           {
                               startTime = startTime,
                               endTime = endTime,
                               title = item.title,
                               name = item.name,
                               description = item.description,
                               url = item.url,
                               category = item.category,
                               imageUrls_default = imageUrls_default,
                               imageUrls_mid = imageUrls_mid,
                               imageUrls_low = imageUrls_low,
                               availability = item.availability
                           });
                       }
                       catch (Exception ex)
                       {
                           log.Error("Error in " + SiteName.Flipkart.ToString() + " AllOffer processing ", ex);
                        //throw;
                    }
                   });

                this.AddBulkAllOffers(allOffers.Where(x => x != null).ToList(), log);
            }
            catch (Exception ex)
            {
                log.Error("Error in " + SiteName.Flipkart.ToString() + " AllOffer processing ", ex);
                throw ex;
            }
        }

        public async Task ProcessDealsOfTheDayOffers(ILog log)
        {
            try
            {
                string strAllOffers = FlipkartAPIResult.GetDealsOfTheDayOffer(setting).Result;

                FlipkartAllOffers flipkartAllOffers = JsonConvert.DeserializeObject<FlipkartAllOffers>(strAllOffers);
                List<Models.DealsOfTheDay> Offers = new List<Models.DealsOfTheDay>();
                List<DealsOfTheDayOffer> dealsOfTheDayOffers = new List<DealsOfTheDayOffer>();
                Offers.AddRange(flipkartAllOffers.dotdList);

                log.Info("Get " + Offers.Count() + " DealsOfTheDay from " + SiteName.Flipkart.ToString());

                Parallel.ForEach(Offers.Where(x => x.url != null).ToList(), (item) =>
                {
                    try
                    {
                        string imageUrls_default = item.imageUrls.FirstOrDefault(x => x.resolutionType == "default") != null ?
                                                         item.imageUrls.FirstOrDefault(x => x.resolutionType == "default").url : string.Empty;
                        string imageUrls_mid = item.imageUrls.FirstOrDefault(x => x.resolutionType == "mid") != null ?
                                                                                item.imageUrls.FirstOrDefault(x => x.resolutionType == "mid").url : string.Empty;
                        string imageUrls_low = item.imageUrls.FirstOrDefault(x => x.resolutionType == "low") != null ?
                                                                                item.imageUrls.FirstOrDefault(x => x.resolutionType == "low").url : string.Empty;
                        dealsOfTheDayOffers.Add(new DealsOfTheDayOffer()
                        {
                            startTime = DateTime.Now,
                            endTime = DateTime.Now.AddHours(24),
                            title = item.title,
                            name = item.name,
                            description = item.description,
                            url = item.url,
                            category = item.category,
                            imageUrls_default = imageUrls_default,
                            imageUrls_mid = imageUrls_mid,
                            imageUrls_low = imageUrls_low,
                            availability = item.availability
                        });
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error in " + SiteName.Flipkart.ToString() + " AllOffer processing ", ex);
                        //throw;
                    }
                });

                this.AddBulkDealsOfTheDay(dealsOfTheDayOffers.Where(x => x != null).ToList(), log);
            }
            catch (Exception ex)
            {
                log.Error("Error in " + SiteName.Flipkart.ToString() + " AllOffer processing ", ex);
                throw ex;
            }
        }

        public async Task RemoveOldOffers(ILog log)
        {
            try
            {
                log.Info("RemoveOldOffers started");
                var lstRemoveProducts = this.GetOfferProducts(log).Result.Where(x => x.validTill < DateTime.Now.AddMinutes(-1));
                this.RemoveBulkOfferProducts(lstRemoveProducts.ToList(), log);


                var lstRemoveOffers = this.GetAllOffers(log).Result.Where(x => x.endTime < DateTime.Now.AddMinutes(-1));
                this.RemoveBulkAllOffers(lstRemoveOffers.ToList(), log);

                log.Info("RemoveOldOffers completed");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        #region OfferProduct
        public async Task<List<OfferProduct>> GetOfferProducts(ILog log)
        {
            try
            {
                return _offerProductRepository.GetAll().Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<List<OfferProduct>> GetOfferProducts(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort, ILog log)
        {
            try
            {
                return _offerProductRepository.GetAllByFilter(page, pageSize, predicate, sort).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<OfferProduct> GetOfferProductByTitle(string title, ILog log)
        {
            try
            {
                return _offerProductRepository.Get(d => d.title.Equals(title));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<int> AddOfferProduct(OfferProduct offerProduct, ILog log)
        {
            try
            {
                if (_offerProductRepository.Get(x => x.productId == offerProduct.productId) == null)
                {
                    return _offerProductRepository.Add(offerProduct).Result;
                }
                else
                {
                    return _offerProductRepository.Update(offerProduct).Result;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<bool> AddBulkOfferProducts(List<OfferProduct> offerProducts, ILog log)
        {
            try
            {
                // var noDupsOfferProducts = new HashSet<OfferProduct>(offerProducts).ToList();
                offerProducts = offerProducts.Where(x => x != null).ToList();
                string catName = offerProducts.First().categoryPath;
                var dbOfferProductList = await _offerProductRepository.Find(x => x.categoryPath == catName);

                #region Insert

                List<OfferProduct> result = offerProducts.Except(dbOfferProductList).ToList();

                _offerProductRepository.BulkAdd(result).Wait();

                #endregion

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                // throw;
            }

            return true;
        }
        public async Task<int> UpdateOfferProduct(OfferProduct offerProduct, ILog log)
        {
            try
            {
                return _offerProductRepository.Update(offerProduct).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<bool> RemoveOfferProduct(OfferProduct offerProduct, ILog log)
        {
            try
            {
                return _offerProductRepository.Delete(offerProduct).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<bool> RemoveBulkOfferProducts(List<OfferProduct> offerProducts, ILog log)
        {
            try
            {
                return _offerProductRepository.BulkDelete(offerProducts).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        #endregion
        #region AllOffers
        public async Task<List<AllOffer>> GetAllOffers(ILog log)
        {
            try
            {
                return _allOfferRepository.GetAll().Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                throw ex;
            }
        }
        public async Task<List<AllOffer>> GetAllOffers(int? page, int? pageSize, Expression<Func<AllOffer, bool>> predicate, Expression<Func<AllOffer, object>> sort, ILog log)
        {
            try
            {
                return _allOfferRepository.GetAllByFilter(page, pageSize, predicate, sort).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                throw ex;
            }
        }
        public async Task<AllOffer> GetAllOfferByTitle(string title, ILog log)
        {
            try
            {
                return _allOfferRepository.Get(d => d.title.Equals(title));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                throw ex;
            }
        }
        public async Task<int> AddAllOffer(AllOffer allOffer, ILog log)
        {
            try
            {
                return _allOfferRepository.Add(allOffer).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<bool> AddBulkAllOffers(List<AllOffer> allOffers, ILog log)
        {
            try
            {
                return _allOfferRepository.BulkAdd(allOffers).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                throw;
            }
        }

        public async Task<int> UpdateAllOffer(AllOffer allOffer, ILog log)
        {
            try
            {
                return _allOfferRepository.Update(allOffer).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<bool> RemoveAllOffer(AllOffer allOffer, ILog log)
        {
            try
            {
                return _allOfferRepository.Delete(allOffer).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                throw ex;
            }
        }
        public async Task<bool> RemoveBulkAllOffers(List<AllOffer> allOffers, ILog log)
        {
            try
            {
                return _allOfferRepository.BulkDelete(allOffers).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        #endregion

        #region DealsOfTheDay
        public async Task<bool> AddBulkDealsOfTheDay(List<DealsOfTheDayOffer> dealsOfTheDayOffers, ILog log)
        {
            try
            {
                return _dealsOfTheDayOfferRepository.BulkAdd(dealsOfTheDayOffers).Result;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                throw;
            }
        }
        #endregion
    }
}
