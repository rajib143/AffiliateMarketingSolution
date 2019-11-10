using AMA.BusinessLayer.Interface;
using AMA.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.Implementation
{
    public class Offers : IOffers
    {
        private readonly IOfferProductRepository _offerProductRepository;

        public void GetOffers()
        {

        }
    }
}
