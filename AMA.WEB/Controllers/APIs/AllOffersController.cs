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

        private LootLoOnlineDatabaseEntities db = new LootLoOnlineDatabaseEntities();

        // GET: api/AllOffers
        public IQueryable<SP_GET_AllOffers_Search_Paging_Sorting_Result> GetAllOffers()
        {
            List<SP_GET_AllOffers_Search_Paging_Sorting_Result> offerProducts = _AMAManager.Client.Offers.GetAllOffersBySP(null,null,null, 1, 50, null, log).Result.ToList();
           
            return offerProducts.AsQueryable();
        }

        // GET: api/AllOffers/5
        [ResponseType(typeof(SP_GET_AllOffers_Search_Paging_Sorting_Result))]
        public async Task<IHttpActionResult> GetAllOffers(string searchText, int page , int pageSize, string sort )
        {
            DateTime startTime = DateTime.Now, endTime = DateTime.Now.AddDays(5);
            List<SP_GET_AllOffers_Search_Paging_Sorting_Result> offerProducts = _AMAManager.Client.Offers.GetAllOffersBySP(!string.IsNullOrEmpty(searchText) ? searchText : null, null,endTime, page, pageSize, !string.IsNullOrEmpty(sort) ? sort : null, log).Result.ToList();

            offerProducts = offerProducts.Where(x => x.imageUrls_default != null).OrderByDescending(x => x.endTime).ToList();
            if (offerProducts == null)
            {
                return NotFound();
            }

            return Ok(offerProducts);
        }

        // PUT: api/AllOffers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAllOffer(long id, AllOffer offerProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offerProduct.Id)
            {
                return BadRequest();
            }

            db.Entry(offerProduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllOfferExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AllOffers
        [ResponseType(typeof(AllOffer))]
        public async Task<IHttpActionResult> PostAllOffer(AllOffer offerProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AllOffers.Add(offerProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AllOfferExists(offerProduct.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = offerProduct.Id }, offerProduct);
        }

        // DELETE: api/AllOffers/5
        [ResponseType(typeof(AllOffer))]
        public async Task<IHttpActionResult> DeleteAllOffer(string id)
        {
            AllOffer offerProduct = await db.AllOffers.FindAsync(id);
            if (offerProduct == null)
            {
                return NotFound();
            }

            db.AllOffers.Remove(offerProduct);
            await db.SaveChangesAsync();

            return Ok(offerProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AllOfferExists(long id)
        {
            return db.AllOffers.Count(e => e.Id== id) > 0;
        }
    }
}