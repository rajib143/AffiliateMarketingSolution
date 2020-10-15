using AffiliateMarketingAutomation.CoreDataLayer;
using AffiliateMarketingAutomation.CoreDataLayer.Models;
using AMA.CoreBusinessLayer.Interface;
using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.Implementation
{
   public class VisitedUserBL :IVisitedUserBL
    {
        private readonly IVisitedUserRepository _visitedUserRepository;
        
        public VisitedUserBL()
        {
            _visitedUserRepository = new VisitedUserRepository();
        }

        #region VisitedUser
        public async Task<List<VisitedUser>> GetVisitedUsers(ILogger log)
        {
            try
            {
                return _visitedUserRepository.GetAll().Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<VisitedUser>> GetVisitedUsers(int? page, int? pageSize, Expression<Func<VisitedUser, bool>> predicate, Expression<Func<VisitedUser, object>> sort, ILogger log)
        {
            try
            {
                return _visitedUserRepository.GetAllByFilter(page, pageSize, predicate, sort).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<VisitedUser> GetVisitedUserByMacID(string MacId, ILogger log)
        {
            try
            {
                return _visitedUserRepository.Get(d => d.MacId.Equals(MacId));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> AddVisitedUser(VisitedUser visitedUser, ILogger logr)
        {
            try
            {
                return _visitedUserRepository.Add(visitedUser).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> AddBulkVisitedUser(List<VisitedUser> visitedUsers, ILogger log)
        {
            try
            {
                return _visitedUserRepository.BulkAdd(visitedUsers).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<int> UpdateVisitedUser(VisitedUser visitedUser, ILogger log)
        {
            try
            {
                return _visitedUserRepository.Update(visitedUser).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RemoveVisitedUser(VisitedUser visitedUser, ILogger log)
        {
            try
            {
                return _visitedUserRepository.Delete(visitedUser).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> BulkRemoveVisitedUser(List<VisitedUser> visitedUsers, ILogger log)
        {
            try
            {
                return _visitedUserRepository.BulkDelete(visitedUsers).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
