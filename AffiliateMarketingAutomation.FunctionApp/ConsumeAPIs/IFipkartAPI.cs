using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffiliateMarketingAutomation.FunctionApp.ConsumeAPIs
{
  public interface IFipkartAPI : IAffiliateAPI
    {
        void ProcessingFlipkartDOTOffers(ILogger log);

    }
}
