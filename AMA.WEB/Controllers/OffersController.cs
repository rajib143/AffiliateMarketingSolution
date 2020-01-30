using AMA.DataLayer.Data;
using AMA.WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AMA.WEB.Controllers
{
    public class OffersController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Int32 pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

        // GET: ProductList
        public ActionResult Index(int catagoryID, int page = 1)
        {
            ViewBag.Title = "LootLo Offers";
            ViewBag.keywords = "LootLoOnline,LootLo,Offers";
            try
            {
                ViewBag.hdnCategory = catagoryID;
                MainOffersModel model = new MainOffersModel();
                string offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] + "/api/OfferProducts";
                log.Info("Offers Controller: OfferproductApiUrl-" + offerproductApiUrl);

                OfferProductRequestModel requestModel = new OfferProductRequestModel()
                {
                    searchText = string.Empty,
                    catagoryID = catagoryID,
                    page = page,
                    pageSize = pageSize,
                    sort = string.Empty

                };
                HttpResponseMessage response = AMAManager.GetClientResponse(offerproductApiUrl, requestModel).Result;
             
               if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("Offers Controller: OfferproductApiUrl result count {0} , Value {1}", data.Count(), data));

                    model.offerProducts = new List<AMAOfferProduct>();
                    model.offerProducts = JsonConvert.DeserializeObject<List<AMAOfferProduct>>(data);

                    if (model.offerProducts.Count > 0)
                    {
                        ViewBag.ROWNUM = model.offerProducts.LastOrDefault().ROWNUM ;
                        ViewBag.TotalRowCount = model.offerProducts.LastOrDefault().TotalCount;
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ActionResult OfferProducts(string CategoryId, int page = 1)
        {
            ViewBag.Title = "LootLo Offer Products";
           
            try
            {
                MainOffersModel model = new MainOffersModel();

                string offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] + "/api/OfferProducts";
                log.Info("Offers Controller: OfferproductApiUrl-" + offerproductApiUrl);

                OfferProductRequestModel requestModel = new OfferProductRequestModel()
                {
                    searchText = string.Empty,
                    catagoryID = !string.IsNullOrEmpty(CategoryId)? Int32.Parse(CategoryId): 0,
                    page = page,
                    pageSize = pageSize,
                    sort = string.Empty

                };
                HttpResponseMessage response = AMAManager.GetClientResponse(offerproductApiUrl, requestModel).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("Offers Controller: OfferproductApiUrl result count {0} , Value {1}", data.Count(), data));
                    model.offerProducts = JsonConvert.DeserializeObject<List<AMAOfferProduct>>(data);
                }

                return PartialView("_OfferProducts", model.offerProducts);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}