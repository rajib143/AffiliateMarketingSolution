﻿using AMA.BusinessLayer.Implementation;
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
        public FlipkartAPI()
        {
            flipkartBL = new FlipkartBL();
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


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}