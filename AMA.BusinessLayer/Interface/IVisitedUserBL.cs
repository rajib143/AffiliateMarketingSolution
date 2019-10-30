using AMA.DataLayer.Data;
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
        Task<List<VisitedUser>> GetVisitedUsers();
        Task<List<VisitedUser>> GetVisitedUsers(int? page, int? pageSize, Expression<Func<VisitedUser, bool>> predicate, Expression<Func<VisitedUser, object>> sort);

        Task<VisitedUser> GetVisitedUserByMacID(string MacID);
        Task<int> AddVisitedUser(VisitedUser allOffer);
        Task<bool> AddBulkVisitedUser(List<VisitedUser> allOffers);
        Task<int> UpdateVisitedUser(VisitedUser allOffer);
        Task<bool> RemoveVisitedUser(VisitedUser allOffer);
        Task<bool> BulkRemoveVisitedUser(List<VisitedUser> allOffer);
    }
}
