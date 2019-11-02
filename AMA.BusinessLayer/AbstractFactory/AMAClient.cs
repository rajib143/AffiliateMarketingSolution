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
        public IOffer offerBL;
        public IAdminBL Admin;
        public IVisitedUserBL VisitedUser;
        public AMAClient(IOffer _offerBL)
        {
            offerBL = _offerBL;
            Admin = new AdminBL();
            VisitedUser = new VisitedUserBL();
        }
    }
}
