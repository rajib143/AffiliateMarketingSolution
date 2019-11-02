using AMA.BusinessLayer.AbstractFactory;
using AMA.BusinessLayer.Implementation;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiliateAPIConsumeJob.ConsumeAPIs
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
        public void OffersProcessing(ILog log)
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
                log.Error("Error in ProcessingOffers", ex);
                throw ex;
            }
        }
        public void ProcessingFlipkartDOTOffers(ILog log)
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
        public void RemoveOldOffers(ILog log)
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
