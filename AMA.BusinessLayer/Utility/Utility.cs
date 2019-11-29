using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.BusinessLayer.Utility
{
   public static class Utility
    {
        public static DateTime FromUnixTime(this long unixTime)
        {
            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTime);
            return dtDateTime;
        }

    }
}
