using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Net.Http;

namespace SCUMServerListener
{
    public enum Data
    {
        Name,
        Players,
        Status,
        MaxPlayers,
        Ip,
        Port,
        Time
    };

    public class Server
    {
        private string _name;
        private string _id;

        public string Name => this._name;
        public string ID => this._id;

        public Server(string id, string name)
        {
            this._id = id;
            this._name = name;
        }
    }

    public static class ServerData
    {
        public static string GetLookupString(string serverString)
        {
            const string apiUrl = "https://api.battlemetrics.com/servers?page%5Bsize%5D=50&filter%5Bgame%5D=scum&filter%5Bsearch%5D=";

            serverString = serverString.Replace(" ", "%20");

            return $"{apiUrl}{serverString}";
        }

        public static bool GetServers(string lookUpString, out List<Server> results)
        {
            string? json;

            results = new List<Server>();

            using (WebClient client = new WebClient())
            {
                try
                {
                    json = client.DownloadString(lookUpString);

                    if (json is null) return false;

                    dynamic dataSet = JObject.Parse(json);
                    var servers = dataSet["data"];

                    foreach (var server in servers)
                    {
                        results.Add(new Server(server["id"].ToString(), server["attributes"]["name"].ToString()));
                    }
                }
                catch (WebException)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool RetrieveData(string serverId, out Dictionary<Data, string> results)
        {
            string? data;
            const string apiUrl = "https://api.battlemetrics.com/servers/";
            results = new();

            using (WebClient client = new WebClient())
            {
                try
                {
                    data = client.DownloadString($"{apiUrl}{serverId}");

                    if (data is null) return false;

                    dynamic result = JsonConvert.DeserializeObject(data);

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
                    if(ex is WebException or System.Net.Sockets.SocketException)
                        return false;
                    throw;
                }
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