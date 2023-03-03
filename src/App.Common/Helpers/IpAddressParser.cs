using System.Net;

namespace App.Common.Helpers
{
    /// <summary>
    ///     Ip mapper helper
    /// </summary>
    public static class IpAddressParser
    {
        /// <summary>
        ///      Parse to IpV4
        /// </summary>
        /// <param name="ipAddress">ip address</param>
        /// <returns>mapped ip address</returns>
        public static string ParseToIpV4(string ipAddress)
        {
            if (!string.IsNullOrEmpty(ipAddress) && IPAddress.TryParse(ipAddress, out var ipv4) && ipv4.IsIPv4MappedToIPv6)
            {
                 return ipv4.MapToIPv4().ToString();
            }

            return ipAddress;
        }
    }
}
