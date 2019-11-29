using AfiliateAPIConsumeJob.ConsumeAPIs;
using AfiliateAPIConsumeJob.Utility;
using AMA.BusinessLayer.AbstractFactory;
using AMA.BusinessLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using log4net.Appender;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace AfiliateAPIConsumeJob
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //log4net.Config.XmlConfigurator.Configure();
        // BasicConfigurator.Configure();
        //XmlConfigurator.Configure();

        public static Dictionary<string, ISiteOffer> affiliateProducts;
        public static ISiteOffer GetAffiliateAPI(string productName)
        {
            return affiliateProducts[productName];
        }
        static void Main(string[] args)
        {
            LogWriter logWriter = new LogWriter("AfiliateAPIConsumeJob");
            
            try
            {
                args = new string[] { "Flipkart" };
                affiliateProducts = new Dictionary<string, ISiteOffer>();
                affiliateProducts.Add("Flipkart", new FlipkartBL());

                if (!string.IsNullOrEmpty(args[0]))
                {
                    //logWriter.LogWrite(string.Format("Application Started for {0} API.", args[0]));
                    log.Info(string.Format("Application Started for {0} API.", args[0]));
                    ISiteOffer affiliateAPI = GetAffiliateAPI(args[0]);
                    Task.Run(() =>
                    {
                        affiliateAPI.RemoveOldOffers(log);
                        affiliateAPI.ProcessAllOffers(log);
                        affiliateAPI.ProcessOfferProducts(log);
                        

                    }).Wait();

                    //logWriter.LogWrite(string.Format("Application ended for {0} API.", args[0]));
                    log.Info(string.Format("Application ended for {0} API.", args[0]));
                }
            }
            catch (Exception ex)
            {
                //logWriter.LogWrite(string.Format("Error in application processing."));
                //logWriter.LogWrite(ex.Message);
                //logWriter.LogWrite(ex.StackTrace);

                log.Error(string.Format("Error in application processing."), ex);
                //throw ex;
            }
        }
    }
}
