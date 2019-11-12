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
            AllBannerOffers = new List<AllOffer>();
            DOTSOffers = new List<AllOffer>();
            offerProducts = new List<OfferProduct>();
        }

        public List<AllOffer> AllBannerOffers { get; set; }
        public List<AllOffer> DOTSOffers { get; set; }
        public List<OfferProduct> offerProducts { get; set; }

    }
}