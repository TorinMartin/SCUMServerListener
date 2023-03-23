using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Net.Http;

namespace SCUMServerListener
{
    public static class ServerData
    {
        private const string API_URL = "https://api.battlemetrics.com/servers?page%5Bsize%5D=50&filter%5Bgame%5D=scum&filter%5Bsearch%5D=";
        private const string GEN_API_URL = "https://api.battlemetrics.com/servers/";
        private static HttpClient _client = new();

        private static HttpResponseMessage SendRequest(string url)
        {
            var response = _client.GetAsync(url).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            return response;
        }

        public static string GetLookupString(string serverString) => $"{API_URL}{Uri.EscapeDataString(serverString)}";

        public static bool GetServers(string lookUpString, out IEnumerable<dynamic> results)
        {
            results = new List<dynamic>();

            try
            {
                var json = SendRequest(lookUpString).Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (string.IsNullOrEmpty(json)) return false;
                dynamic obj = JsonConvert.DeserializeObject(json);
                IEnumerable<dynamic> servers = obj?.data;
                results = servers.Select(server => new Server { ID = server.attributes?.id, Name = server.attributes?.name });
            }
            catch (HttpRequestException)
            {
                return false;
            }
            return true;
        }

        public static bool RetrieveData(ref Server server)
        {
            server ??= new()
            {
                ID = AppSettings.Instance.DefaultServerId
            };

            try
            {
                var json = SendRequest($"{GEN_API_URL}{server.ID}").Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (string.IsNullOrEmpty(json)) return false;
                dynamic obj = JsonConvert.DeserializeObject(json);

                server.Name = obj.data?.attributes?.name ?? string.Empty;
                server.Players = obj.data?.attributes?.players ?? string.Empty;
                server.Status = obj.data?.attributes?.status ?? string.Empty;
                server.MaxPlayers = obj.data?.attributes?.maxPlayers ?? string.Empty;
                server.Ip = obj.data?.attributes?.ip ?? string.Empty;
                server.Port = obj.data?.attributes?.port ?? string.Empty;
                server.Time = obj.data?.attributes?.details?.time ?? string.Empty;

                return true;
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException or System.Net.Sockets.SocketException)
                {
                    return false;
                }
                throw;
            }
        }

        public static double Ping(string host, int echoNum)
        {
            long totalTime = 0;
            int timeout = 120;
            Ping pingSender = new Ping();

            for (int i = 0; i < echoNum; i++)
            {
                PingReply reply = pingSender.Send(host, timeout);
                if (reply.Status == IPStatus.Success)
                {
                    totalTime += reply.RoundtripTime;
                }
            }
            return totalTime / echoNum;
        }
    }
}