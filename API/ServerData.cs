﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Net.Http;

namespace SCUMServerListener
{
    public static class ServerData
    {
        private const string apiUrl = "https://api.battlemetrics.com/servers?page%5Bsize%5D=50&filter%5Bgame%5D=scum&filter%5Bsearch%5D=";
        private const string genApiUrl = "https://api.battlemetrics.com/servers/";

        private static HttpClient _client = new();

        private static HttpResponseMessage SendRequest(string url)
        {
            var response = _client.GetAsync(url).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            return response;
        }

        //public static string GetLookupString(string serverString) => $"{apiUrl}{serverString.Replace(" ", "%20")}";
        public static string GetLookupString(string serverString) => $"{apiUrl}{Uri.EscapeDataString(serverString)}";

        public static bool GetServers(string lookUpString, out IEnumerable<dynamic> results)
        {
            results = new List<dynamic>();

            try
            {
                var json = SendRequest(lookUpString).Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (string.IsNullOrEmpty(json)) return false;
                dynamic resultObj = JObject.Parse(json);
                IEnumerable<dynamic> servers = resultObj["data"];
                results = servers.Select(server => new Server { ID = server["attributes"]["id"], Name = server["attributes"]["name"] });
            }
            catch (HttpRequestException)
            {
                return false;
            }
            return true;
        }

        public static bool RetrieveData(string serverId, out Dictionary<Data, string> results)
        {
            results = new();

            try
            {
                var json = SendRequest($"{genApiUrl}{serverId}").Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (string.IsNullOrEmpty(json)) return false;
                dynamic result = JsonConvert.DeserializeObject(json);

                results.Add(Data.Name, result["data"]["attributes"]["name"].ToString());
                results.Add(Data.Players, result["data"]["attributes"]["players"].ToString());
                results.Add(Data.Status, result["data"]["attributes"]["status"].ToString());
                results.Add(Data.MaxPlayers, result["data"]["attributes"]["maxPlayers"].ToString());
                results.Add(Data.Ip, result["data"]["attributes"]["ip"].ToString());
                results.Add(Data.Port, result["data"]["attributes"]["port"].ToString());
                results.Add(Data.Time, result["data"]["attributes"]["details"]["time"].ToString());

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