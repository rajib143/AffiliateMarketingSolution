using AMA.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.AbstractFactory
{
   public interface IAllOffer
    {
        Task<List<AllOffer>> GetAllOffers();
        Task<List<AllOffer>> GetAllOffers(int? page, int? pageSize, Expression<Func<AllOffer, bool>> predicate, Expression<Func<AllOffer, object>> sort);

        Task<AllOffer> GetAllOfferByTitle(string title);
        Task<int> AddAllOffer(AllOffer allOffer);
        Task<bool> AddBulkAllOffers(List<AllOffer> allOffers);
        Task<int> UpdateAllOffer(AllOffer allOffer);
        Task<bool> RemoveAllOffer(AllOffer allOffer);
        Task<bool> RemoveBulkAllOffers(List<AllOffer> allOffer);

        Task ProcessAllOffers();
    }
}
