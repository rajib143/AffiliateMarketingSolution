using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using AMA.CoreBusinessLayer.Interface;
using log4net;
using AffiliateMarketingAutomation.CoreDataLayer;
using AffiliateMarketingAutomation.CoreDataLayer.Models;
using Microsoft.Extensions.Logging;

namespace AMA.CoreBusinessLayer.Implementation
{
    public class AdminBL : IAdminBL
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOfferBrandRepository _offerBrandRepository;
      //  private readonly ILootLoOnlineDbEntity _lootLoOnlineEntity;

        public AdminBL()
        {
            _categoryRepository = new CategoryRepository();
            _offerBrandRepository = new OfferBrandRepository();
          //  _lootLoOnlineEntity = new LootLoOnlineEntity();
        }

        public async Task<List<Category>> GetCategories(ILogger log)
        {
            try
            {
                return _categoryRepository.GetAll().Result;
            }
            catch (Exception ex)
            {
                log.LogError("Erro in GetCategories" + ex.Message);
                throw ex;
            }
        }
        public async Task<List<Category>> GetCategories(Expression<Func<Category, bool>> predicate, ILogger log)
        {
            try
            {
                return _categoryRepository.Find(predicate).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public async Task<List<GetParentChildCategories_Result>> GetParentChildsCategories(int parentId, ILogger log)
        //{
        //    try
        //    {
        //        return _lootLoOnlineEntity.GetParentChildCategories(parentId).Result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public async Task<List<Category>> GetCategories(int? page, int? pageSize, Expression<Func<Category, bool>> predicate, Expression<Func<Category, object>> sort, ILogger log)
        {
            try
            {
                return _categoryRepository.GetAllByFilter(page, pageSize, predicate, sort).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Category> GetCategoryByTitle(string title, ILogger log)
        {
            try
            {
                return _categoryRepository.Get(d => d.Name.Equals(title));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> AddCategory(Category category, ILogger log)
        {
            try
            {
                return _categoryRepository.Add(category).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> AddBulkCategory(List<Category> categorys, ILogger log)
        {
            try
            {
                return _categoryRepository.BulkAdd(categorys).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<int> UpdateCategory(Category category, ILogger log)
        {
            try
            {
                return _categoryRepository.Update(category).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RemoveCategory(Category category, ILogger log)
        {
            try
            {
                return _categoryRepository.Delete(category).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> BulkRemoveCategory(List<Category> categorys, ILogger log)
        {
            try
            {
                return _categoryRepository.BulkDelete(categorys).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Offer Brand

        public async Task<List<OfferBrand>> GetOfferBrands(ILogger log)
        {
            try
            {
                return _offerBrandRepository.GetAll().Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<OfferBrand>> GetOfferBrands(int? page, int? pageSize, Expression<Func<OfferBrand, bool>> predicate, Expression<Func<OfferBrand, object>> sort, ILogger log)
        {
            try
            {
                return _offerBrandRepository.GetAllByFilter(page, pageSize, predicate, sort).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<OfferBrand> GetOfferBrandByTitle(string title, ILogger log)
        {
            try
            {
                return _offerBrandRepository.Get(d => d.BrandName.Equals(title));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> AddOfferBrand(OfferBrand offerBrand, ILogger log)
        {
            try
            {
                return _offerBrandRepository.Add(offerBrand).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> AddBulkOfferBrand(List<OfferBrand> offerBrands, ILogger log)
        {
            try
            {
                return _offerBrandRepository.BulkAdd(offerBrands).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<int> UpdateOfferBrand(OfferBrand offerBrand, ILogger log)
        {
            try
            {
                return _offerBrandRepository.Update(offerBrand).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RemoveOfferBrand(OfferBrand offerBrand, ILogger log)
        {
            try
            {
                return _offerBrandRepository.Delete(offerBrand).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> BulkRemoveOfferBrand(List<OfferBrand> offerBrands, ILogger log)
        {
            try
            {
                return _offerBrandRepository.BulkDelete(offerBrands).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
