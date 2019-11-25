﻿using System;
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

        private LootLoOnlineDatabaseEntities db = new LootLoOnlineDatabaseEntities();

        // GET: api/OfferProducts
        public IQueryable<SP_GET_OfferProducts_Search_Paging_Sorting_Result> GetOfferProducts()
        {
            List<SP_GET_OfferProducts_Search_Paging_Sorting_Result> offerProducts = _AMAManager.Client.Offers.GetOfferProductsBySP(null, 1, 50, null, log).Result.ToList();

            return offerProducts.AsQueryable();
        }

        // GET: api/OfferProducts
        [ResponseType(typeof(SP_GET_OfferProducts_Search_Paging_Sorting_Result))]
        public async Task<IHttpActionResult> GetOfferProducts(string searchText, int page, int pageSize, string sort)
        {
            List<SP_GET_OfferProducts_Search_Paging_Sorting_Result> offerProducts = new List<SP_GET_OfferProducts_Search_Paging_Sorting_Result>();

            offerProducts = _AMAManager.Client.Offers.GetOfferProductsBySP(!string.IsNullOrEmpty(searchText) ? searchText : null, page, pageSize, !string.IsNullOrEmpty(sort) ? sort : null, log).Result.ToList();
          
            if (offerProducts == null)
            {
                return NotFound();
            }

            return Ok(offerProducts);
        }

        // PUT: api/OfferProducts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOfferProduct(string id, OfferProduct offerProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offerProduct.productId)
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
                if (!OfferProductExists(id))
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

        // POST: api/OfferProducts
        [ResponseType(typeof(OfferProduct))]
        public async Task<IHttpActionResult> PostOfferProduct(OfferProduct offerProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OfferProducts.Add(offerProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfferProductExists(offerProduct.productId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = offerProduct.productId }, offerProduct);
        }

        // DELETE: api/OfferProducts/5
        [ResponseType(typeof(OfferProduct))]
        public async Task<IHttpActionResult> DeleteOfferProduct(string id)
        {
            OfferProduct offerProduct = await db.OfferProducts.FindAsync(id);
            if (offerProduct == null)
            {
                return NotFound();
            }

            db.OfferProducts.Remove(offerProduct);
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

        private bool OfferProductExists(string id)
        {
            return db.OfferProducts.Count(e => e.productId == id) > 0;
        }
    }
}