using AMA.CoreBusinessLayer.Implementation;
using AMA.CoreBusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.CoreBusinessLayer.AbstractFactory
{
    public class AMAClient
    {
        public ISiteOffer SiteOffer;
        public IAdminBL Admin;
        public IVisitedUserBL VisitedUser;
        public IOffers Offers;
        public AMAClient(ISiteOffer _siteOfferBL)
        {
            SiteOffer = _siteOfferBL;
            Admin = new AdminBL();
            VisitedUser = new VisitedUserBL();
            Offers = new Offers();
        }
        public AMAClient()
        {
            SiteOffer = new FlipkartBL();
            Admin = new AdminBL();
            VisitedUser = new VisitedUserBL();
            Offers = new Offers();
        }
    }
}
