using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AMA.CoreBusinessLayer.AbstractFactory;
using AMA.CoreBusinessLayer.Implementation;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AffiliateMarketingAutomation.FunctionApp
{
    public static class fnAfiliateAPIConsumeJob
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

        [FunctionName("fnAfiliateAPIConsumeJob")]
        public static async Task Run([TimerTrigger("0 0 */6 * * *")] TimerInfo myTimer, ILogger log)
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
                    affiliateAPI.ProcessOfferProducts(log);
                    affiliateAPI.ProcessAllOffers(log);
                    affiliateAPI.ProcessDealsOfTheDayOffers(log);



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
        }
    }
}
