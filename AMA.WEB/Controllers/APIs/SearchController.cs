using AMA.DataLayer.Data;
using AMA.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AMA.WEB.Controllers.APIs
{
    public class SearchController : ApiController
    {
        private AMAManager _AMAManager = new AMAManager();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private LootLoOnlineDatabaseEntities db = new LootLoOnlineDatabaseEntities();

        // GET: api/Search
        public IQueryable<SP_GET_OfferProducts_Search_Result> GetOfferProducts()
        {
            List<SP_GET_OfferProducts_Search_Result> offerProducts = _AMAManager.Client.Offers.GetSearchOfferProductsBySP(null, 1, 50, null, log).Result.ToList();

            return offerProducts.AsQueryable();
        }

        // GET: api/Search/5
        [ResponseType(typeof(SP_GET_OfferProducts_Search_Result))]
        public async Task<IHttpActionResult> GetSearchOfferProduct(string searchText)
        {
           // OfferProduct offerProduct = await db.OfferProducts.FindAsync(id);
            List<SP_GET_OfferProducts_Search_Result> offerProducts = _AMAManager.Client.Offers.GetSearchOfferProductsBySP(searchText, 1, 50, null, log).Result.ToList();

            if (offerProducts == null)
            {
                return NotFound();
            }

            return Ok(offerProducts);
        }
    }
}
