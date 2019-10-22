using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.DataLayer.Data
{
  public  class FlipkartDL
    {
        private LootLoOnlineDatabaseEntities LootLoOnlineDatabaseEntities { get; set; }
        public FlipkartDL()
        {
            LootLoOnlineDatabaseEntities = DatabaseConnection.Entityinstance;
        }
        public async Task<bool> InsertBulkOfferProducts(List<OfferProduct> products)
        {
            try
            {

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
