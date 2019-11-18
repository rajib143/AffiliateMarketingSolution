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
        Task<List<SP_GET_OfferProducts_Search_Paging_Sorting_Result>> GetOfferProductsBySP(string searchText, int? page, int? pageSize, string sort, ILog log = null);
        Task<List<SP_GET_AllOffers_Search_Paging_Sorting_Result>> GetAllOffersBySP(string searchText, DateTime? startTime, DateTime? endTime, int? page, int? pageSize, string sort, ILog log = null);
        Task<List<SP_GET_OfferProducts_Search_Result>> GetSearchOfferProductsBySP(string searchText, int? page, int? pageSize, string sort, ILog log = null);

    }
}