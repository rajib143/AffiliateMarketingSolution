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

namespace AMA.BusinessLayer.Implementation
{
    public class FlipkartBL : IFlipkartBL
    {

        private readonly IOfferProductRepository _offerProductRepository;
        private readonly IAllOfferRepository _allOfferRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly Setting setting;
        public FlipkartBL()
        {
            _offerProductRepository = new OfferProductRepository();
            _allOfferRepository = new AllOfferRepository();
            _categoryRepository = new CategoryRepository();
            setting = new Setting();
        }


        public async Task ProcessOfferProducts()
        {
            try
            {
                List<ProductCatagory> productCatagories = FlipkartAPI.GetFlipkartProductCategorys(setting);

                foreach (var item in productCatagories.ToList())
                {
                    try
                    {
                        List<Product> products = new List<Product>();

                        var result = FlipkartAPI.GetFlipkartCategoryProducts(setting, item.getApi).products.ToList();
                        products.AddRange(result.Where(x => x.productBaseInfoV1.inStock = true && x.productShippingInfoV1.sellerAverageRating.HasValue
                                                        && x.productShippingInfoV1.sellerAverageRating > 0));

                        if (products.Count() > 0)
                            await InserIntoOfferproducts(products);
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task InserIntoOfferproducts(List<Product> products)
        {
            List<OfferProduct> offerProducts = new List<OfferProduct>();

            Parallel.ForEach(products.OrderByDescending(x => x.productShippingInfoV1.sellerNoOfReviews).ToList(), (item) =>
            //foreach (var item in products.OrderByDescending(x => x.productShippingInfoV1.sellerNoOfReviews).ToList())
            {
                try
                {
                    //    var item = products.OrderByDescending(x => x.productShippingInfoV1.sellerNoOfReviews).First();
                    var resultDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.productBaseInfoV1.imageUrls.ToString());
                    if (!offerProducts.Any(x => x.productId.Equals(item.productBaseInfoV1.productId))
                     && !string.IsNullOrEmpty(item.productBaseInfoV1.title)
                     && item.productBaseInfoV1.maximumRetailPrice.amount > 0
                     && item.productBaseInfoV1.flipkartSellingPrice.amount > 0
                     && item.productBaseInfoV1.flipkartSpecialPrice.amount > 0
                     && item.productBaseInfoV1.discountPercentage > 0)
                    {
                        offerProducts.Add(new OfferProduct()
                        {
                            productId = item.productBaseInfoV1.productId,
                            title = item.productBaseInfoV1.title + "@" + item.productBaseInfoV1.discountPercentage + "% OFF",
                            productDescription = item.productBaseInfoV1.productDescription,
                            imageUrls_200 = resultDic.Values.ToArray()[0].ToString(),
                            imageUrls_400 = resultDic.Values.ToArray()[1].ToString(),
                            imageUrls_800 = resultDic.Values.ToArray()[2].ToString(),

                            productFamily = item.productBaseInfoV1.productFamily.ToString(), //

                            maximumRetailPrice = item.productBaseInfoV1.maximumRetailPrice.amount,
                            flipkartSellingPrice = item.productBaseInfoV1.flipkartSellingPrice.amount,
                            flipkartSpecialPrice = item.productBaseInfoV1.flipkartSpecialPrice.amount,
                            currency = item.productBaseInfoV1.maximumRetailPrice.currency,
                            productUrl = item.productBaseInfoV1.productUrl,
                            productBrand = item.productBaseInfoV1.productBrand,
                            inStock = item.productBaseInfoV1.inStock,
                            codAvailable = item.productBaseInfoV1.codAvailable,
                            discountPercentage = item.productBaseInfoV1.discountPercentage,
                            offers = item.productBaseInfoV1.offers.ToString(),
                            categoryPath = item.productBaseInfoV1.categoryPath,
                            attributes = item.productBaseInfoV1.attributes.ToString(),
                            shippingCharges = item.productShippingInfoV1.shippingCharges.amount,
                            estimatedDeliveryTime = item.productShippingInfoV1.estimatedDeliveryTime,
                            sellerName = item.productShippingInfoV1.sellerName,
                            sellerAverageRating = item.productShippingInfoV1.sellerAverageRating,
                            sellerNoOfRatings = item.productShippingInfoV1.sellerNoOfRatings,
                            sellerNoOfReviews = item.productShippingInfoV1.sellerNoOfReviews,
                            keySpecs = item.categorySpecificInfoV1.keySpecs.ToString(),
                            detailedSpecs = item.categorySpecificInfoV1.detailedSpecs.ToString(),
                            specificationList = item.categorySpecificInfoV1.specificationList.ToString(),
                            booksInfo = item.categorySpecificInfoV1.booksInfo.ToString(),
                            lifeStyleInfo = item.categorySpecificInfoV1.lifeStyleInfo.ToString(),
                            IsUpdated = false,
                            CreatedDate = DateTime.Now

                        });

                    }
                }
                catch (Exception ex)
                {

                }
                //}
            });

            // bulk insertion 
            await AddBulkOfferProducts(offerProducts);
        }
        public async Task ProcessAllOffers()
        {
            try
            {
                string strAllOffers = FlipkartAPIResult.GetAllFlipkartOffers(setting).Result;

                FlipkartAllOffers flipkartAllOffers = JsonConvert.DeserializeObject<FlipkartAllOffers>(strAllOffers);
                List<DealsOfTheDay> Offers = new List<DealsOfTheDay>();
                List<AllOffer> allOffers = new List<AllOffer>();
                Offers.AddRange(flipkartAllOffers.allOffersList);


                Parallel.ForEach(Offers.ToList(), (item) =>
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
                });

                this.AddBulkAllOffers(allOffers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveOldOffers()
        {
            try
            {
                var lstRemoveProducts = this.GetOfferProducts().Result.Where(x => x.CreatedDate < DateTime.Now.AddHours(-2));
                this.RemoveBulkOfferProducts(lstRemoveProducts.ToList());


                var lstRemoveOffers = this.GetAllOffers().Result.Where(x => x.endTime > DateTime.Now);
                this.RemoveBulkAllOffers(lstRemoveOffers.ToList());


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region OfferProduct
        public async Task<List<OfferProduct>> GetOfferProducts()
        {
            try
            {
                return _offerProductRepository.GetAll().Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<OfferProduct>> GetOfferProducts(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort)
        {
            try
            {
                return _offerProductRepository.GetAllByFilter(page, pageSize, predicate, sort).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<OfferProduct> GetOfferProductByTitle(string title)
        {
            try
            {
                return _offerProductRepository.Get(d => d.title.Equals(title));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> AddOfferProduct(OfferProduct offerProduct)
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

                throw ex;
            }
        }
        public async Task<bool> AddBulkOfferProducts(List<OfferProduct> offerProducts)
        {
            try
            {
                // var noDupsOfferProducts = new HashSet<OfferProduct>(offerProducts).ToList();

                string catName = offerProducts.First().categoryPath;
                var dbOfferProductList = await _offerProductRepository.Find(x => x.categoryPath == catName);

                #region Insert

                List<OfferProduct> result = offerProducts.Except(dbOfferProductList).ToList();

                _offerProductRepository.BulkAdd(result).Wait();

                #endregion

            }
            catch (Exception ex)
            {

                //throw;
            }

            return true;
        }
        public async Task<int> UpdateOfferProduct(OfferProduct offerProduct)
        {
            try
            {
                return _offerProductRepository.Update(offerProduct).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RemoveOfferProduct(OfferProduct offerProduct)
        {
            try
            {
                return _offerProductRepository.Delete(offerProduct).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RemoveBulkOfferProducts(List<OfferProduct> offerProducts)
        {
            try
            {
                return _offerProductRepository.BulkDelete(offerProducts).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region AllOffers
        public async Task<List<AllOffer>> GetAllOffers()
        {
            try
            {
                return _allOfferRepository.GetAll().Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<AllOffer>> GetAllOffers(int? page, int? pageSize, Expression<Func<AllOffer, bool>> predicate, Expression<Func<AllOffer, object>> sort)
        {
            try
            {
                return _allOfferRepository.GetAllByFilter(page, pageSize, predicate, sort).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<AllOffer> GetAllOfferByTitle(string title)
        {
            try
            {
                return _allOfferRepository.Get(d => d.title.Equals(title));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> AddAllOffer(AllOffer allOffer)
        {
            try
            {
                return _allOfferRepository.Add(allOffer).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> AddBulkAllOffers(List<AllOffer> allOffers)
        {
            try
            {
                return _allOfferRepository.BulkAdd(allOffers).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<int> UpdateAllOffer(AllOffer allOffer)
        {
            try
            {
                return _allOfferRepository.Update(allOffer).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RemoveAllOffer(AllOffer allOffer)
        {
            try
            {
                return _allOfferRepository.Delete(allOffer).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RemoveBulkAllOffers(List<AllOffer> allOffers)
        {
            try
            {
                return _allOfferRepository.BulkDelete(allOffers).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
