using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AMA.DataLayer.Data;
using AMA.WEB.Models;

namespace AMA.WEB.Controllers.API
{
    public class AllOffersController : ApiController
    {
        private AMAManager _AMAManager = new AMAManager();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // HttpPost: api/AllOffers/5
        [HttpPost]
        [ResponseType(typeof(SP_GET_AllOffers_Search_Paging_Sorting_Result))]
        public async Task<IHttpActionResult> GetAllOffers(FlipKartAllOffersRequestModel requestModel)
        {
            List<SP_GET_AllOffers_Search_Paging_Sorting_Result> offerProducts = _AMAManager.Client.Offers.GetAllOffersBySP(requestModel.searchText, null, requestModel.endDate, requestModel.page, requestModel.pageSize, requestModel.sort, log).Result.ToList();

            offerProducts = offerProducts.Where(x => x.imageUrls_default != null).OrderByDescending(x => x.endTime).ToList();
            if (offerProducts == null)
            {
                return NotFound();
            }

            return Ok(offerProducts);
        }

        
    }
}