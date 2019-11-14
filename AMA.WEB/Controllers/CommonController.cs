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
    public class CommonController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Common
        public ActionResult Index()
        {
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
                return PartialView("~/Views/Common/_Category.cshtml", Catagories);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}