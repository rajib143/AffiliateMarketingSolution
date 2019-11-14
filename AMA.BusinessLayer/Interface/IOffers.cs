using AMA.DataLayer.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.Interface
{
    public interface IOffers
    {
        Task<List<OfferProduct>> GetOfferProductsBySP(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort, ILog log = null);
        Task<List<OfferProduct>> GetAllOffersBySP(int? page, int? pageSize, Expression<Func<OfferProduct, bool>> predicate, Expression<Func<OfferProduct, object>> sort, ILog log = null);

    }
}