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
   public interface IVisitedUserBL
    {
        Task<List<VisitedUser>> GetVisitedUsers(ILogger log);
        Task<List<VisitedUser>> GetVisitedUsers(int? page, int? pageSize, Expression<Func<VisitedUser, bool>> predicate, Expression<Func<VisitedUser, object>> sort, ILogger log);

        Task<VisitedUser> GetVisitedUserByMacID(string MacID, ILogger log);
        Task<int> AddVisitedUser(VisitedUser allOffer, ILogger log);
        Task<bool> AddBulkVisitedUser(List<VisitedUser> allOffers, ILogger log);
        Task<int> UpdateVisitedUser(VisitedUser allOffer, ILogger log);
        Task<bool> RemoveVisitedUser(VisitedUser allOffer, ILogger log);
        Task<bool> BulkRemoveVisitedUser(List<VisitedUser> allOffer, ILogger log);
    }
}
