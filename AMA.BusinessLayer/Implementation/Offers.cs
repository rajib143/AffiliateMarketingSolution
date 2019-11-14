using AMA.BusinessLayer.Interface;
using AMA.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.Implementation
{
    public class Offers : IOffers
    {
        private readonly IOfferProductRepository _offerProductRepository;

        public async Task<List<OfferProduct>> GetOfferProducts(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort, ILog log)
        {
            try
            {

                return _offerProductRepository.GetAllByFilter(page, pageSize, predicate, sort).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
