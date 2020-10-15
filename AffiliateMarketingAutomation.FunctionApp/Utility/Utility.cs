using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AffiliateMarketingAutomation.FunctionApp
{
    public static class Utility
    {

        public static string SetNullIfEmpty(this string value)
        {
            if (value == null)
                value = string.Empty;

            return value;

        }
    }
}
