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

namespace ServerListener
{
    public class Server
    {
        private string _name;
        private string _id;

        public string Name
        {
            get => this._name;
        }

        public string ID
        {
            get => this._id;
        }

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

        public static bool RetrieveData(string serverId, out string[] results)
        {
            string? data;
            const string apiUrl = "https://api.battlemetrics.com/servers/";
            results = new string[7];

            using (WebClient client = new WebClient())
            {
                try
                {
                    data = client.DownloadString($"{apiUrl}{serverId}");

                    if (data is null) return false;

                    dynamic result = JsonConvert.DeserializeObject(data);

                    results[0] = result["data"]["attributes"]["name"].ToString();
                    results[1] = result["data"]["attributes"]["players"].ToString();
                    results[2] = result["data"]["attributes"]["status"].ToString();
                    results[3] = result["data"]["attributes"]["maxPlayers"].ToString();
                    results[4] = result["data"]["attributes"]["ip"].ToString();
                    results[5] = result["data"]["attributes"]["port"].ToString();
                    results[6] = result["data"]["attributes"]["details"]["time"].ToString();

                    return true;

                }
                catch (WebException)
                {
                    return false;
                }
                catch (System.Net.Sockets.SocketException)
                {
                    return false;
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