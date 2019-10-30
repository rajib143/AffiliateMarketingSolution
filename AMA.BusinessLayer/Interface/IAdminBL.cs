using AMA.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.Interface
{
    public interface IAdminBL
    {
        Task<List<Category>> GetCategories();
        Task<List<Category>> GetCategories(int? page, int? pageSize, Expression<Func<Category, bool>> predicate, Expression<Func<Category, object>> sort);
        Task<Category> GetCategoryByTitle(string title);
        Task<int> AddCategory(Category allOffer);
        Task<bool> AddBulkCategory(List<Category> allOffers);
        Task<int> UpdateCategory(Category allOffer);
        Task<bool> RemoveCategory(Category allOffer);
        Task<bool> BulkRemoveCategory(List<Category> allOffer);


        Task<List<OfferBrand>> GetOfferBrands();
        Task<List<OfferBrand>> GetOfferBrands(int? page, int? pageSize, Expression<Func<OfferBrand, bool>> predicate, Expression<Func<OfferBrand, object>> sort);
        Task<OfferBrand> GetOfferBrandByTitle(string title);
        Task<int> AddOfferBrand(OfferBrand allOffer);
        Task<bool> AddBulkOfferBrand(List<OfferBrand> allOffers);
        Task<int> UpdateOfferBrand(OfferBrand allOffer);
        Task<bool> RemoveOfferBrand(OfferBrand allOffer);
        Task<bool> BulkRemoveOfferBrand(List<OfferBrand> allOffer);
    }
}
