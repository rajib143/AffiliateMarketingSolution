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
    public class OfferProductsController : ApiController
    {
        private AMAManager _AMAManager = new AMAManager();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Post: api/OfferProducts
        [HttpPost]
        [ResponseType(typeof(SP_GET_OfferProducts_Search_Paging_Sorting_Result))]
        public async Task<IHttpActionResult> GetOfferProducts(OfferProductRequestModel requestModel)
        {
            List<SP_GET_OfferProducts_Search_Paging_Sorting_Result> offerProducts = new List<SP_GET_OfferProducts_Search_Paging_Sorting_Result>();

            try
            {
                string macAddress = AMA.BusinessLayer.Utility.Utility.GetMACAddress();

                offerProducts = _AMAManager.Client.Offers.GetOfferProductsBySP(requestModel.searchText, null, null, requestModel.catagoryID, macAddress, requestModel.page, requestModel.pageSize, requestModel.sort, log).Result.ToList();

                if (offerProducts == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                throw ex;
            }

            return Ok(offerProducts);
        }


    }
}