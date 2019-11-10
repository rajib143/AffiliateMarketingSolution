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
            ViewBag.Title = "Welcome to Loot Online";

            return View();
        }


        public ActionResult BindCategories()
        {
            try
            {
                string CateroryAPI = ConfigurationManager.AppSettings["CateroryAPI"];

                List<CategoryModel> Catagories = new List<CategoryModel>();

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(CateroryAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    Task.Run(() =>
                    {
                        HttpResponseMessage response =  client.GetAsync(CateroryAPI).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var data = response.Content.ReadAsStringAsync().Result;
                            Catagories = JsonConvert.DeserializeObject<List<CategoryModel>>(data);

                        }
                    }).Wait();
                  

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
