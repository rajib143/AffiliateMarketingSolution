using AffiliateMarketingAutomation.CoreDataLayer.Models;
using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.Interface
{
    public interface IAdminBL
    {
        Task<List<Category>> GetCategories(ILogger log);
        Task<List<Category>> GetCategories(Expression<Func<Category, bool>> predicate, ILogger log);
       // Task<List<GetParentChildCategories_Result>> GetParentChildsCategories(int parentId, ILogger log);
        Task<List<Category>> GetCategories(int? page, int? pageSize, Expression<Func<Category, bool>> predicate, Expression<Func<Category, object>> sort, ILogger log);
        Task<Category> GetCategoryByTitle(string title, ILogger log);
        Task<int> AddCategory(Category allOffer, ILogger log);
        Task<bool> AddBulkCategory(List<Category> allOffers, ILogger log);
        Task<int> UpdateCategory(Category allOffer, ILogger log);
        Task<bool> RemoveCategory(Category allOffer, ILogger log);
        Task<bool> BulkRemoveCategory(List<Category> allOffer, ILogger log);


        Task<List<OfferBrand>> GetOfferBrands(ILogger log);
        Task<List<OfferBrand>> GetOfferBrands(int? page, int? pageSize, Expression<Func<OfferBrand, bool>> predicate, Expression<Func<OfferBrand, object>> sort, ILogger log);
        Task<OfferBrand> GetOfferBrandByTitle(string title, ILogger log);
        Task<int> AddOfferBrand(OfferBrand allOffer, ILogger log);
        Task<bool> AddBulkOfferBrand(List<OfferBrand> allOffers, ILogger log);
        Task<int> UpdateOfferBrand(OfferBrand allOffer, ILogger log);
        Task<bool> RemoveOfferBrand(OfferBrand allOffer, ILogger log);
        Task<bool> BulkRemoveOfferBrand(List<OfferBrand> allOffer, ILogger log);
    }
}
