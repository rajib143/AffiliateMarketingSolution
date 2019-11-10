using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMA.DataLayer.Data;
using AMA.DataLayer;
using System.Linq.Expressions;
using AMA.BusinessLayer.AbstractFactory;

namespace AMA.BusinessLayer.Interface
{
    public interface IFlipkartBL : ISiteOffer
    {
        #region Offer Product
        //Task<List<OfferProduct>> GetOfferProducts();
        //Task<List<OfferProduct>> GetOfferProducts(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort);

        //Task<OfferProduct> GetOfferProductByTitle(string title);
        //Task<int> AddOfferProduct(OfferProduct offerProduct);
        //Task<bool> AddBulkOfferProducts(List<OfferProduct> offerProducts);
        //Task<int> UpdateOfferProduct(OfferProduct offerProduct);
        //Task<bool> RemoveOfferProduct(OfferProduct offerProduct);
        //Task<bool> RemoveBulkOfferProducts(List<OfferProduct> offerProduct);

        #endregion

        #region All Offer
        //Task<List<AllOffer>> GetAllOffers();
        //Task<List<AllOffer>> GetAllOffers(int? page, int? pageSize, Expression<Func<AllOffer, bool>> predicate, Expression<Func<AllOffer, object>> sort);

        //Task<AllOffer> GetAllOfferByTitle(string title);
        //Task<int> AddAllOffer(AllOffer allOffer);
        //Task<bool> AddBulkAllOffers(List<AllOffer> allOffers);
        //Task<int> UpdateAllOffer(AllOffer allOffer);
        //Task<bool> RemoveAllOffer(AllOffer allOffer);
        //Task<bool> RemoveBulkAllOffers(List<AllOffer> allOffer); 
        #endregion
    }
}
