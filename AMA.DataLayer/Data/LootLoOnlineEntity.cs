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

        public async Task<List<SP_GET_OfferProducts_Search_Paging_Sorting_Result>> GetOfferProductsBySP(int? page, int? pageSize, string searchText, string sort, ILog log = null)
        {
            try
            {
                //Task.Run(() =>
                // {
                var result = _lootLoOnlineDatabaseEntities.SP_GET_OfferProducts_Search_Paging_Sorting(searchText, searchText, decimal.Zero, page, pageSize, sort).AsQueryable();

                //if (predicate != null)
                //    result = result.Where(predicate);

                //if (sort != null)
                //    result = result.OrderByDescending(sort);

                if (page.HasValue && pageSize.HasValue)
                    result = result.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

                // } ).Wait();

                 return result.ToList();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        Task<List<OfferProduct>> GetAllOffersBySP(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort, ILog log = null);

    }
}
