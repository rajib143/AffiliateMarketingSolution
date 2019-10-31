using AMA.BusinessLayer.Implementation;
using AMA.BusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.AbstractFactory
{
    public class AMAClient
    {
        public IOffer offers;
        public IAdminBL Admin;
        public IVisitedUserBL VisitedUser;
        public AMAClient(IOffer offer)
        {
            offers = offer;
            Admin = new AdminBL();
            VisitedUser = new VisitedUserBL();
        }
    }
}
