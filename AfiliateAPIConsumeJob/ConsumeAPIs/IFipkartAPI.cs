using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiliateAPIConsumeJob.ConsumeAPIs
{
  public interface IFipkartAPI : IAffiliateAPI
    {
        void ProcessingFlipkartDOTOffers(ILog log);

    }
}
