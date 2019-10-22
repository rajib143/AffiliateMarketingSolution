using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiliateAPIConsumeJob
{
    class Program
    {

        public static IAffiliateAPI GetAffiliateAPI(string productName)
        {
            return affiliateProducts[productName];
        }
        static void Main(string[] args)
        {
            LogWriter logWriter = new LogWriter("AfiliateAPIConsumeJob");
            try
            {
                args = new string[] { "Flipkart" };
                affiliateProducts = new Dictionary<string, IAffiliateAPI>();
                affiliateProducts.Add("Flipkart", new FlipkartAPI());

                if (!string.IsNullOrEmpty(args[0]))
                {
                    logWriter.LogWrite(string.Format("Application Started for {0} API.", args[0]));
                    IAffiliateAPI affiliateAPI = GetAffiliateAPI(args[0]);

                    Setting setting = new Setting();

                    affiliateAPI.ProcessAllOffers(setting);
                    affiliateAPI.ProcessOfferProducts(setting);

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
