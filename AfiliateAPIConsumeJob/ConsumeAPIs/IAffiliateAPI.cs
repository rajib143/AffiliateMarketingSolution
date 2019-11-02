using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiliateAPIConsumeJob.ConsumeAPIs
{
    public interface IAffiliateAPI
    {
        void OffersProcessing(ILog log);
        void RemoveOldOffers(ILog log);

    }
}
