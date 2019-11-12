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

        public ActionResult Index()
        {
            ViewBag.Title = "Welcome to Loot Lo Online";

            MainOffersModel model = new MainOffersModel();
           

            return View();
        }


        public ActionResult BindCategories()
        {
            try
            {
                string CateroryAPI = ConfigurationManager.AppSettings["CateroryAPI"];
                List<CategoryModel> Catagories = new List<CategoryModel>();

                HttpResponseMessage response = AMAManager.GetClientResponse(CateroryAPI);
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Catagories = JsonConvert.DeserializeObject<List<CategoryModel>>(data);

                }
               
                return PartialView("~/Views/Shared/_Category.cshtml", Catagories);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
    }
}
