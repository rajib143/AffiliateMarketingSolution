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
   public interface IVisitedUserBL
    {
        Task<List<VisitedUser>> GetVisitedUsers(ILog log);
        Task<List<VisitedUser>> GetVisitedUsers(int? page, int? pageSize, Expression<Func<VisitedUser, bool>> predicate, Expression<Func<VisitedUser, object>> sort, ILog log);

        Task<VisitedUser> GetVisitedUserByMacID(string MacID, ILog log);
        Task<int> AddVisitedUser(VisitedUser allOffer, ILog log);
        Task<bool> AddBulkVisitedUser(List<VisitedUser> allOffers, ILog log);
        Task<int> UpdateVisitedUser(VisitedUser allOffer, ILog log);
        Task<bool> RemoveVisitedUser(VisitedUser allOffer, ILog log);
        Task<bool> BulkRemoveVisitedUser(List<VisitedUser> allOffer, ILog log);
    }
}
