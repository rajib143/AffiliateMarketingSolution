using AffiliateMarketingAutomation.CoreDataLayer.Models;
using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.AbstractFactory
{
   public interface IAllOffer
    {
        Task<List<AllOffer>> GetAllOffers(ILogger log );
        Task<List<AllOffer>> GetAllOffers(int? page, int? pageSize, Expression<Func<AllOffer, bool>> predicate, Expression<Func<AllOffer, object>> sort, ILogger log);

        Task<AllOffer> GetAllOfferByTitle(string title, ILogger log);
        Task<int> AddAllOffer(AllOffer allOffer, ILogger log);
        Task<bool> AddBulkAllOffers(List<AllOffer> allOffers, ILogger log);
        Task<int> UpdateAllOffer(AllOffer allOffer, ILogger log);
        Task<bool> RemoveAllOffer(AllOffer allOffer, ILogger log);
        Task<bool> RemoveBulkAllOffers(List<AllOffer> allOffer,ILogger log );

        Task ProcessAllOffers(ILogger log);
        Task ProcessDealsOfTheDayOffers(ILogger log);
    }
}
