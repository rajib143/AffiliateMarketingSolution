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
            MainOffersModel model = new MainOffersModel();
            try
            {
                string offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                    string.Format("api/OfferProducts?searchText={0}&catagoryID={1}&page={2}&pageSize={3}&sort={4}", string.Empty, 0, 1, pageSize, string.Empty);
                log.Info("OfferproductApiUrl-" + offerproductApiUrl);
                HttpResponseMessage response = AMAManager.GetClientResponse(offerproductApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("OfferproductApiUrl result count {0} , Value {1}", data.Count(), data));
                    model.offerProducts = JsonConvert.DeserializeObject<List<AMAOfferProduct>>(data);
                }

                //string AllOfferApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                //   string.Format("api/AllOffers?searchText={0}&page={1}&pageSize={2}&sort={3}", string.Empty, 1, pageSize, string.Empty);
                //log.Info("AllOfferApiUrl-" + AllOfferApiUrl);
                //HttpResponseMessage AllOfferResponse = AMAManager.GetClientResponse(AllOfferApiUrl);

                //if (AllOfferResponse.IsSuccessStatusCode)
                //{
                //    var AllOfferData = AllOfferResponse.Content.ReadAsStringAsync().Result;
                //    log.Info(string.Format("AllOfferApiUrl result count {0} , Value {1}", AllOfferData.Count(), AllOfferData));
                //    model.fipkartAllOffers = JsonConvert.DeserializeObject<List<FipkartAllOffers>>(AllOfferData);
                //}

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
                string AllOfferApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                   string.Format("api/AllOffers?searchText={0}&page={1}&pageSize={2}&sort={3}", string.Empty, 1, pageSize, string.Empty);
                log.Info("AllOfferApiUrl-" + AllOfferApiUrl);
                HttpResponseMessage AllOfferResponse = AMAManager.GetClientResponse(AllOfferApiUrl);

                if (AllOfferResponse.IsSuccessStatusCode)
                {
                    var AllOfferData = AllOfferResponse.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("AllOfferApiUrl result count {0} , Value {1}", AllOfferData.Count(), AllOfferData));
                    List<FipkartAllOffers> lstFipkartAllOffers = JsonConvert.DeserializeObject<List<FipkartAllOffers>>(AllOfferData);
                    return PartialView("~/Views/Home/_AllOffers.cshtml", lstFipkartAllOffers);
                }else
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
