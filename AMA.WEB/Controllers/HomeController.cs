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
            ViewBag.Title = "Welcome to LootLo Online";

            MainOffersModel model = new MainOffersModel();

            try
            {
                string offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                    string.Format("api/OfferProducts?searchText={0}&page={1}&pageSize={2}&sort={3}", string.Empty, 1, pageSize, string.Empty);

                //string offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                //   string.Format("api/OfferProducts?page={0}&pageSize={1}", 1, pageSize);

                log.Info("OfferproductApiUrl-" + offerproductApiUrl);
                HttpResponseMessage response = AMAManager.GetClientResponse(offerproductApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("OfferproductApiUrl result count {0} , Value {1}", data.Count(), data));
                    model.offerProducts = JsonConvert.DeserializeObject<List<SP_GET_OfferProducts_Search_Paging_Sorting_Result>>(data);
                }

                string AllOfferApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                   string.Format("api/AllOffers?searchText={0}&page={1}&pageSize={2}&sort={3}", string.Empty, 1, pageSize, string.Empty);

                //string AllOfferApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                //   string.Format("api/AllOffers?page={0}&pageSize={1}", 1, pageSize);
                log.Info("AllOfferApiUrl-" + AllOfferApiUrl);

                HttpResponseMessage AllOfferResponse = AMAManager.GetClientResponse(AllOfferApiUrl);
                if (AllOfferResponse.IsSuccessStatusCode)
                {
                    var AllOfferData = AllOfferResponse.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("AllOfferApiUrl result count {0} , Value {1}", AllOfferData.Count(), AllOfferData));
                    model.AllBannerOffers = JsonConvert.DeserializeObject<List<SP_GET_AllOffers_Search_Paging_Sorting_Result>>(AllOfferData);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(model);
        }
    }
}
