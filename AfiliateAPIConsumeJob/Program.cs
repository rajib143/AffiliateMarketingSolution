using AfiliateAPIConsumeJob.ConsumeAPIs;
using AfiliateAPIConsumeJob.Utility;
using AMA.BusinessLayer.AbstractFactory;
using AMA.BusinessLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiliateAPIConsumeJob
{
    class Program
    {
        public static Dictionary<string, IOffer> affiliateProducts;
        public static IOffer GetAffiliateAPI(string productName)
        {
            return affiliateProducts[productName];
        }
        static void Main(string[] args)
        {
            LogWriter logWriter = new LogWriter("AfiliateAPIConsumeJob");
            try
            {
                args = new string[] { "Flipkart" };
                affiliateProducts = new Dictionary<string, IOffer>();
                affiliateProducts.Add("Flipkart", new FlipkartBL());

                if (!string.IsNullOrEmpty(args[0]))
                {
                    logWriter.LogWrite(string.Format("Application Started for {0} API.", args[0]));
                    IOffer affiliateAPI = GetAffiliateAPI(args[0]);
                    Task.Run(() =>
                    {
                        affiliateAPI.RemoveOldOffers();
                        affiliateAPI.ProcessOfferProducts();
                        affiliateAPI.ProcessAllOffers();

                    }).Wait();

                    logWriter.LogWrite(string.Format("Application ended for {0} API.", args[0]));
                }
            }
            catch (Exception ex)
            {
                logWriter.LogWrite(string.Format("Error in application processing."));
                logWriter.LogWrite(ex.Message);
                logWriter.LogWrite(ex.StackTrace);
                //throw ex;
            }
        }
    }
}
