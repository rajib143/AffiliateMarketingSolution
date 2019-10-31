using AMA.BusinessLayer.AbstractFactory;
using AMA.BusinessLayer.Implementation;
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
        
        public void ProcessOffer()
        {
            try
            {
                
                Task.Run(() =>
                {
                    
                    flipkartBL.ProcessOfferProducts();
                    

                }).Wait();

                Task.Run(() =>
                {
                    flipkartBL.ProcessAllOffers();

                }).Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcessAllOffers()
        {
            try
            {
                Task.Run(() =>
                {
                    flipkartBL.ProcessAllOffers();
                }).Wait();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RemoveOldOffers()
        {
            try
            {
                //Task.Run(() =>
                //{
                    var lstRemoveProducts = flipkartBL.GetOfferProducts().Result.Where(x => x.CreatedDate < DateTime.Now.AddHours(-2));

                   flipkartBL.RemoveBulkOfferProducts(lstRemoveProducts.ToList());

               // }).Wait();

                Task.Run(() =>
                {
                    var lstRemoveOffers = flipkartBL.GetAllOffers().Result.Where(x => x.endTime > DateTime.Now);

                    flipkartBL.RemoveBulkAllOffers(lstRemoveOffers.ToList());

                }).Wait();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
