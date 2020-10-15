using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using AMA.CoreBusinessLayer.AbstractFactory;
using AMA.CoreBusinessLayer.Implementation;

namespace AffiliateMarketingAutomation.FunctionApp
{
    public static class Function1
    {
        private static readonly log4net.ILog logLocal = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //log4net.Config.XmlConfigurator.Configure();
        // BasicConfigurator.Configure();
        //XmlConfigurator.Configure();

        public static Dictionary<string, ISiteOffer> affiliateProducts;
        public static ISiteOffer GetAffiliateAPI(string productName)
        {
            return affiliateProducts[productName];
        }
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            LogWriter logWriter = new LogWriter("AfiliateAPIConsumeJob");

            try
            {
                string[] args = new string[] { "Flipkart" };
                affiliateProducts = new Dictionary<string, ISiteOffer>();
                affiliateProducts.Add("Flipkart", new FlipkartBL());

                if (!string.IsNullOrEmpty(args[0]))
                {
                    //logWriter.LogWrite(string.Format("Application Started for {0} API.", args[0]));
                    log.LogInformation(string.Format("Application Started for {0} API.", args[0]));
                    ISiteOffer affiliateAPI = GetAffiliateAPI(args[0]);
                    //Task.Run(() =>
                    //{
                    await affiliateAPI.RemoveOldOffers(log);
                    affiliateAPI.ProcessAllOffers(log);
                    affiliateAPI.ProcessDealsOfTheDayOffers(log);
                    affiliateAPI.ProcessOfferProducts(log);


                    //}).Wait();

                    //logWriter.LogWrite(string.Format("Application ended for {0} API.", args[0]));
                    log.LogInformation(string.Format("Application ended for {0} API.", args[0]));
                }
            }
            catch (Exception ex)
            {
                //logWriter.LogWrite(string.Format("Error in application processing."));
                //logWriter.LogWrite(ex.Message);
                //logWriter.LogWrite(ex.StackTrace);

                log.LogError(string.Format("Error in application processing."), ex);
                //throw ex;
            }
            return new OkResult();
        }
    }
}
