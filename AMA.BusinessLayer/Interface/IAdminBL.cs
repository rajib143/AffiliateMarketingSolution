using AMA.DataLayer.Data;
using log4net;
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
        Task<List<Category>> GetCategories(ILog log);
        Task<List<Category>> GetCategories(int? page, int? pageSize, Expression<Func<Category, bool>> predicate, Expression<Func<Category, object>> sort, ILog log);
        Task<Category> GetCategoryByTitle(string title, ILog log);
        Task<int> AddCategory(Category allOffer, ILog log);
        Task<bool> AddBulkCategory(List<Category> allOffers, ILog log);
        Task<int> UpdateCategory(Category allOffer, ILog log);
        Task<bool> RemoveCategory(Category allOffer, ILog log);
        Task<bool> BulkRemoveCategory(List<Category> allOffer, ILog log);


        Task<List<OfferBrand>> GetOfferBrands(ILog log);
        Task<List<OfferBrand>> GetOfferBrands(int? page, int? pageSize, Expression<Func<OfferBrand, bool>> predicate, Expression<Func<OfferBrand, object>> sort, ILog log);
        Task<OfferBrand> GetOfferBrandByTitle(string title, ILog log);
        Task<int> AddOfferBrand(OfferBrand allOffer, ILog log);
        Task<bool> AddBulkOfferBrand(List<OfferBrand> allOffers, ILog log);
        Task<int> UpdateOfferBrand(OfferBrand allOffer, ILog log);
        Task<bool> RemoveOfferBrand(OfferBrand allOffer, ILog log);
        Task<bool> BulkRemoveOfferBrand(List<OfferBrand> allOffer, ILog log);
    }
}
