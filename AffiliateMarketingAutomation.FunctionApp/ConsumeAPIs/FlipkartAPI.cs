using AMA.CoreBusinessLayer.AbstractFactory;
using AMA.CoreBusinessLayer.Implementation;
using log4net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffiliateMarketingAutomation.FunctionApp.ConsumeAPIs
{
    public class FlipkartAPI : IFipkartAPI
    {
        public FlipkartBL flipkartBL { get; set; }
        public AMAClient _AMAClient { get; set; }
        public FlipkartAPI()
        {
            flipkartBL = new FlipkartBL();
            _AMAClient = new AMAClient(new FlipkartBL());
        }
        public void OffersProcessing(ILogger log)
        {
            try
            {
                
                Task.Run(() =>
                {
                    
                    flipkartBL.ProcessOfferProducts(log);
                    

                }).Wait();

                Task.Run(() =>
                {
                    flipkartBL.ProcessAllOffers(log);

                }).Wait();
            }
            catch (Exception ex)
            {
                log.LogError("Error in ProcessingOffers", ex);
                throw ex;
            }
        }
        public void ProcessingFlipkartDOTOffers(ILogger log)
        {
            try
            {
                Task.Run(() =>
                {
                    flipkartBL.ProcessAllOffers(log);
                }).Wait();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RemoveOldOffers(ILogger log)
        {
            try
            {
                //Task.Run(() =>
                //{
                    var lstRemoveProducts = flipkartBL.GetOfferProducts(log).Result.Where(x => x.CreatedDate < DateTime.Now.AddHours(-2));

                   flipkartBL.RemoveBulkOfferProducts(lstRemoveProducts.ToList(),log);

               // }).Wait();

                Task.Run(() =>
                {
                    var lstRemoveOffers = flipkartBL.GetAllOffers(log).Result.Where(x => x.endTime > DateTime.Now);

                    flipkartBL.RemoveBulkAllOffers(lstRemoveOffers.ToList(),log);

                }).Wait();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
