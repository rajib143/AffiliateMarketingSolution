﻿using AffiliateMarketingAutomation.CoreDataLayer;
using AMA.CoreBusinessLayer.Interface;

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.Implementation
{
    public class Offers : IOffers
    {
        private LootLoOnlineDatabaseEntities lootLoOnline = new LootLoOnlineDatabaseEntities();
        public Offers()
        {

        }

        //public async Task<List<SP_GET_AllOffers_Search_Paging_Sorting_Result>> GetAllOffersBySP(string searchText, DateTime? startTime, DateTime? endTime, int? page, int? pageSize, string sort, ILogger log = null)
        //{
        //    try
        //    {
        //        return await lootLoOnline.GetAllOffersBySP(searchText, startTime, endTime, page, pageSize, sort, log);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<List<SP_GET_OfferProducts_Search_Paging_Sorting_Result>> GetOfferProductsBySP(string searchText, string productBrand, string attributes,int? categoryId, string macId, int? pageNbr, int? pageSize, string sortCol, ILogger log = null)
        //{
        //    try
        //    {
        //        return await lootLoOnline.GetOfferProductsBySP(searchText, productBrand, attributes, categoryId, macId, pageNbr, pageSize, sortCol, log);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<List<SP_GET_OfferProducts_Search_Result>> GetSearchOfferProductsBySP(string searchText, int? page, int? pageSize, string sort, ILogger log = null)
        //{
        //    try
        //    {
        //        return await lootLoOnline.GetSearchOfferProductsBySP(searchText, page, pageSize, sort, log);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
   
    }
}
