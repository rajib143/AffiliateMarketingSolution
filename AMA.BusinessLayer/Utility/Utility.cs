using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        public static string SetNullIfEmpty(this string value)
        {
            if (value == null)
                value = string.Empty;

            return value;

        }
        public static string SetNullIfEmpty(this object value)
        {
            string result ;
            if (value == null)
                result = string.Empty;
            else
                result= value.ToString();

            return result;

        }
    }
}
