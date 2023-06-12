using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Threading.Tasks;

namespace SCUMServerListener
{
    public static class ServerData
    {
        private const string API_URL = "https://api.battlemetrics.com/servers?page%5Bsize%5D=50&filter%5Bgame%5D=scum&filter%5Bsearch%5D=";
        private const string GEN_API_URL = "https://api.battlemetrics.com/servers/";
        private static HttpClient _client = new();

        private static async Task<HttpResponseMessage> SendRequest(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public static string GetLookupString(string serverString) => $"{API_URL}{Uri.EscapeDataString(serverString)}";

        public static async Task<List<Server>> GetServers(string lookUpString)
        {
            var result = new List<Server>();

            try
            {
                var response = await SendRequest(lookUpString);
                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(json)) return result;

                dynamic resultObj = JObject.Parse(json);
                IEnumerable<dynamic> servers = resultObj["data"];
                result = servers.Select(server => new Server { ID = server["attributes"]["id"], Name = server["attributes"]["name"] }).ToList();
            }
            catch (HttpRequestException)
            {
                //TODO: Log
            }
            return result;
        }

        public static async Task<Server?> RetrieveData(Server server)
        {
            server ??= new()
            {
                ID = AppSettings.Instance.DefaultServerId
            };

            try
            {
                var response = await SendRequest($"{GEN_API_URL}{server.ID}"); 
                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(json)) return null;

                dynamic obj = JsonConvert.DeserializeObject(json);

                server.Name = obj["data"]["attributes"]["name"].ToString();
                server.Players = obj["data"]["attributes"]["players"].ToString();
                server.Status = obj["data"]["attributes"]["status"].ToString();
                server.MaxPlayers = obj["data"]["attributes"]["maxPlayers"].ToString();
                server.Ip = obj["data"]["attributes"]["ip"].ToString();
                server.Port = obj["data"]["attributes"]["port"].ToString();
                server.Time = obj["data"]["attributes"]["details"]["time"].ToString();

                return server;
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException or System.Net.Sockets.SocketException)
                {
                    return null;
                }
                throw;
            }
        }

        public static double Ping(string host, int echoNum)
        {
            long totalTime = 0;
            int timeout = 120;
            var pingSender = new Ping();

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