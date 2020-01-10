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

            ViewBag.hdnCategory = catagoryID;
            try
            {
                MainOffersModel model = new MainOffersModel();
                string offerproductApiUrl = string.Empty;
           
                    offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                         string.Format("api/OfferProducts?searchText={0}&catagoryID={1}&page={2}&pageSize={3}&sort={4}", string.Empty, catagoryID, 1, pageSize, string.Empty);

                log.Info("Offers Controller: OfferproductApiUrl-" + offerproductApiUrl);
                HttpResponseMessage response = AMAManager.GetClientResponse(offerproductApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("Offers Controller: OfferproductApiUrl result count {0} , Value {1}", data.Count(), data));
                    model.offerProducts = JsonConvert.DeserializeObject<List<AMAOfferProduct>>(data);
                }

                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ActionResult OfferProducts(String Category, int page = 1)
        {
            ViewBag.Title = "LootLo Offer Products";
           
            try
            {
                MainOffersModel model = new MainOffersModel();
                string offerproductApiUrl = string.Empty;
                if (Category == "ALL")
                {
                    offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                    string.Format("api/OfferProducts?searchText={0}&page={1}&pageSize={2}&sort={3}", string.Empty, page, pageSize, string.Empty);
                }
                else
                {
                    offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                         string.Format("api/OfferProducts?searchText={0}&page={1}&pageSize={2}&sort={3}", Category, page, pageSize, string.Empty);
                }

                log.Info("Offers Controller: OfferproductApiUrl-" + offerproductApiUrl);
                HttpResponseMessage response = AMAManager.GetClientResponse(offerproductApiUrl);
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