using AMA.DataLayer.Data;
using AMA.WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AMA.WEB.Controllers
{
    public class HomeController : Controller
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Int32 pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        public ActionResult Index()
        {
            ViewBag.Title = "Welcome to LootLoOnline";
            ViewBag.keywords = "LootLoOnline,LootLo,Offers";

            string offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] + "/api/OfferProducts";
            log.Info("OfferproductApiUrl-" + offerproductApiUrl);
            MainOffersModel model = new MainOffersModel();

            try
            {
                OfferProductRequestModel requestModel = new OfferProductRequestModel()
                {
                    searchText = string.Empty,
                    catagoryID = 0,
                    page = 1,
                    pageSize = pageSize,
                    sort = string.Empty

                };
                HttpResponseMessage response = AMAManager.GetClientResponse(offerproductApiUrl, requestModel).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("OfferproductApiUrl result count {0} , Value {1}", data.Count(), data));

                    model.offerProducts = new List<AMAOfferProduct>();
                    model.offerProducts = JsonConvert.DeserializeObject<List<AMAOfferProduct>>(data);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(model);
        }

        public ActionResult BindAllFipkartAllOffers()
        {
            try
            {
                string offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] + "/api/AllOffers";

                FlipKartAllOffersRequestModel requestModel = new FlipKartAllOffersRequestModel()
                {
                    searchText = string.Empty,
                    endDate = DateTime.Now.AddDays(-1),
                    page = 1,
                    pageSize = pageSize,
                    sort = string.Empty

                };
                HttpResponseMessage response = AMAManager.GetClientResponse(offerproductApiUrl, requestModel).Result;

                if (response.IsSuccessStatusCode)
                {
                    var AllOfferData = response.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("AllOfferApiUrl result count {0} , Value {1}", AllOfferData.Count(), AllOfferData));
                   
                    List<FipkartAllOffers> lstFipkartAllOffers = JsonConvert.DeserializeObject<List<FipkartAllOffers>>(AllOfferData);
                    return PartialView("~/Views/Home/_AllOffers.cshtml", lstFipkartAllOffers);
                }
                else
                {
                    throw new InvalidOperationException("Error in AllOfferApi fetch.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
