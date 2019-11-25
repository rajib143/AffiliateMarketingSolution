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
        public ActionResult Index(String Category, int page = 1)
        {
            ViewBag.Title = "LootLo Offers";

            try
            {
                MainOffersModel model = new MainOffersModel();
                string offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                    string.Format("api/OfferProducts?searchText={0}&page={1}&pageSize={2}&sort={3}", Category, page, pageSize, string.Empty);

                //string offerproductApiUrl = ConfigurationManager.AppSettings["HostAPI"] +
                //   string.Format("api/OfferProducts?page={0}&pageSize={1}", 1, pageSize);

                log.Info("Offers Controller: OfferproductApiUrl-" + offerproductApiUrl);
                HttpResponseMessage response = AMAManager.GetClientResponse(offerproductApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    log.Info(string.Format("Offers Controller: OfferproductApiUrl result count {0} , Value {1}", data.Count(), data));
                    model.offerProducts = JsonConvert.DeserializeObject<List<SP_GET_OfferProducts_Search_Paging_Sorting_Result>>(data);
                }

                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}