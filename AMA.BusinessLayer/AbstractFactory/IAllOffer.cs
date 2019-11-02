using AMA.DataLayer.Data;
using log4net;
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
        Task<List<AllOffer>> GetAllOffers(ILog log );
        Task<List<AllOffer>> GetAllOffers(int? page, int? pageSize, Expression<Func<AllOffer, bool>> predicate, Expression<Func<AllOffer, object>> sort, ILog log);

        Task<AllOffer> GetAllOfferByTitle(string title, ILog log);
        Task<int> AddAllOffer(AllOffer allOffer, ILog log);
        Task<bool> AddBulkAllOffers(List<AllOffer> allOffers, ILog log);
        Task<int> UpdateAllOffer(AllOffer allOffer, ILog log);
        Task<bool> RemoveAllOffer(AllOffer allOffer, ILog log);
        Task<bool> RemoveBulkAllOffers(List<AllOffer> allOffer,ILog log );

        Task ProcessAllOffers(ILog log);
    }
}
