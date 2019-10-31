using AMA.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.AbstractFactory
{
    public interface IOffer : IAllOffer
    {
        Task<List<OfferProduct>> GetOfferProducts();
        Task<List<OfferProduct>> GetOfferProducts(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort);

        Task<OfferProduct> GetOfferProductByTitle(string title);
        Task<int> AddOfferProduct(OfferProduct offerProduct);
        Task<bool> AddBulkOfferProducts(List<OfferProduct> offerProducts);
        Task<int> UpdateOfferProduct(OfferProduct offerProduct);
        Task<bool> RemoveOfferProduct(OfferProduct offerProduct);
        Task<bool> RemoveBulkOfferProducts(List<OfferProduct> offerProduct);

        Task ProcessOfferProducts();
        Task RemoveOldOffers();
    }
}