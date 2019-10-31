using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMA.DataLayer.Data;
using AMA.DataLayer;
using System.Linq.Expressions;
using AMA.BusinessLayer.Interface;

namespace AMA.BusinessLayer.Implementation
{
    public class AdminBL : IAdminBL
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOfferBrandRepository _offerBrandRepository;

        public AdminBL()
        {
            _categoryRepository = new CategoryRepository();
            _offerBrandRepository = new OfferBrandRepository();
        }

        public async Task<List<Category>> GetCategories()
        {
            try
            {
                return _categoryRepository.GetAll().Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<Category>> GetCategories(int? page, int? pageSize, Expression<Func<Category, bool>> predicate, Expression<Func<Category, object>> sort)
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
        public async Task<Category> GetCategoryByTitle(string title)
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
        public async Task<int> AddCategory(Category category)
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
        public async Task<bool> AddBulkCategory(List<Category> categorys)
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
        public async Task<int> UpdateCategory(Category category)
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
        public async Task<bool> RemoveCategory(Category category)
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
        public async Task<bool> BulkRemoveCategory(List<Category> categorys)
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

        public async Task<List<OfferBrand>> GetOfferBrands()
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
        public async Task<List<OfferBrand>> GetOfferBrands(int? page, int? pageSize, Expression<Func<OfferBrand, bool>> predicate, Expression<Func<OfferBrand, object>> sort)
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
        public async Task<OfferBrand> GetOfferBrandByTitle(string title)
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
        public async Task<int> AddOfferBrand(OfferBrand offerBrand)
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
        public async Task<bool> AddBulkOfferBrand(List<OfferBrand> offerBrands)
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
        public async Task<int> UpdateOfferBrand(OfferBrand offerBrand)
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
        public async Task<bool> RemoveOfferBrand(OfferBrand offerBrand)
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
        public async Task<bool> BulkRemoveOfferBrand(List<OfferBrand> offerBrands)
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
