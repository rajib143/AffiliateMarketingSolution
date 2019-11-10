using AMA.DataLayer.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.AbstractFactory
{
    public interface ISiteOffer : IAllOffer
    {
        Task<List<OfferProduct>> GetOfferProducts(ILog log=null);
        Task<List<OfferProduct>> GetOfferProducts(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort, ILog log=null);

        Task<OfferProduct> GetOfferProductByTitle(string title, ILog log = null);
        Task<int> AddOfferProduct(OfferProduct offerProduct, ILog log = null);
        Task<bool> AddBulkOfferProducts(List<OfferProduct> offerProducts, ILog log = null);
        Task<int> UpdateOfferProduct(OfferProduct offerProduct, ILog log = null);
        Task<bool> RemoveOfferProduct(OfferProduct offerProduct, ILog log = null);
        Task<bool> RemoveBulkOfferProducts(List<OfferProduct> offerProduct, ILog log = null);

        Task ProcessOfferProducts(ILog log);
        Task RemoveOldOffers(ILog log);
    }
}