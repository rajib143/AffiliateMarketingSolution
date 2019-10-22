using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiliateAPIConsumeJob.ConsumeAPIs
{
    public interface IAffiliateAPI
    {
        void ProcessOffer(Setting setting);
        void RemoveOldOffers(Setting setting);
        void ProcessDealsOfTheDayOffers(Setting setting);
    }
}
