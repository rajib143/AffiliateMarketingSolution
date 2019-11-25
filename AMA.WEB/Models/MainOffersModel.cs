using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMA.DataLayer.Data;

namespace AMA.WEB.Models
{
    public class MainOffersModel
    {
        public MainOffersModel()
        {
            AllBannerOffers = new List<SP_GET_AllOffers_Search_Paging_Sorting_Result>();
            DOTSOffers = new List<AllOffer>();
            offerProducts = new List<SP_GET_OfferProducts_Search_Paging_Sorting_Result>();
        }

        public List<SP_GET_AllOffers_Search_Paging_Sorting_Result> AllBannerOffers { get; set; }
        public List<AllOffer> DOTSOffers { get; set; }
        public List<SP_GET_OfferProducts_Search_Paging_Sorting_Result> offerProducts { get; set; }

    }
}