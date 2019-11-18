using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AMA.DataLayer.Data
{
    public class LootLoOnlineEntity
    {
        private LootLoOnlineDatabaseEntities _lootLoOnlineDatabaseEntities = new LootLoOnlineDatabaseEntities();

        public LootLoOnlineEntity()
        {

        }

        public async Task<List<SP_GET_OfferProducts_Search_Paging_Sorting_Result>> GetOfferProductsBySP(string searchText, int? page, int? pageSize, string sort, ILog log = null)
        {
            try
            {
                //Task.Run(() =>
                // {
                var result = _lootLoOnlineDatabaseEntities.SP_GET_OfferProducts_Search_Paging_Sorting(searchText, page, pageSize, sort).AsQueryable();

                //if (predicate != null)
                //    result = result.Where(predicate);

                //if (sort != null)
                //    result = result.OrderByDescending(sort);

                //if (page.HasValue && pageSize.HasValue)
                //    result = result.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

                // } ).Wait();

                return result.ToList();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<List<SP_GET_AllOffers_Search_Paging_Sorting_Result>> GetAllOffersBySP(string searchText,DateTime? startTime, DateTime? endTime, int? page, int? pageSize, string sort, ILog log = null)
        {
            try
            {
                
                var result = _lootLoOnlineDatabaseEntities.SP_GET_AllOffers_Search_Paging_Sorting(searchText,startTime,endTime, page, pageSize, sort).AsQueryable();

               
                return result.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<SP_GET_OfferProducts_Search_Result>> GetSearchOfferProductsBySP(string searchText, int? page, int? pageSize, string sort, ILog log = null)
        {
            try
            {
                //Task.Run(() =>
                // {
                var result = _lootLoOnlineDatabaseEntities.SP_GET_OfferProducts_Search(searchText, page, pageSize, sort).AsQueryable();

                //if (predicate != null)
                //    result = result.Where(predicate);

                //if (sort != null)
                //    result = result.OrderByDescending(sort);

                //if (page.HasValue && pageSize.HasValue)
                //    result = result.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

                // } ).Wait();

                return result.ToList();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

    }
}
