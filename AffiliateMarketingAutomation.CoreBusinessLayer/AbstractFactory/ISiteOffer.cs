using AffiliateMarketingAutomation.CoreDataLayer.Models;
using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.AbstractFactory
{
    public interface ISiteOffer : IAllOffer
    {
        Task<List<OfferProduct>> GetOfferProducts(ILogger log =null);
        Task<List<OfferProduct>> GetOfferProducts(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort, ILogger log =null);

        Task<OfferProduct> GetOfferProductByTitle(string title, ILogger log = null);
        Task<int> AddOfferProduct(OfferProduct offerProduct, ILogger log = null);
        Task<bool> AddBulkOfferProducts(List<OfferProduct> offerProducts, ILogger log = null);
        Task<int> UpdateOfferProduct(OfferProduct offerProduct, ILogger log = null);
        Task<bool> RemoveOfferProduct(OfferProduct offerProduct, ILogger log = null);
        Task<bool> RemoveBulkOfferProducts(List<OfferProduct> offerProduct, ILogger log = null);

        Task ProcessOfferProducts(ILogger log);
        Task RemoveOldOffers(ILogger log);
    }
}