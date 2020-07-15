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

namespace ServerListener
{
    public class Server
    {
        private string name;
        private string id;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public Server(string argid, string argname)
        {
            this.name = argname;
            this.ID = argid;
        }
    }

    public class ServerData
    {
        public string GetLookupString(string serverString)
        {

            for (int i = 0; i < serverString.Length; i++)
            {
                if (serverString[i] == ' ')
                {
                    serverString = serverString.Remove(i, 1);
                    serverString = serverString.Insert(i, "%20");
                    i++;
                }
            }
            return "https://api.battlemetrics.com/servers?sort=rank&fields%5Bserver%5D=rank%2Cname%2Cplayers%2CmaxPlayers%2Caddress%2Cip%2Cport%2Ccountry%2Clocation%2Cdetails%2Cstatus&relations%5Bserver%5D=game%2CserverGroup&filter%5Bgame%5D=scum&filter%5Bsearch%5D=" + serverString;
        }

        public List<Server> GetServerID(string lookUpString)
        {

            string json = null;

            List<Server> results = new List<Server>();

            using (WebClient client = new WebClient())
            {
                try
                {
                    json = client.DownloadString(lookUpString);

                    dynamic dataSet = JObject.Parse(json);

                    var servers = dataSet["data"];

                    foreach (var server in servers)
                    {
                        results.Add(new Server((string)server["id"], (string)server["attributes"]["name"]));
                    }
                    return results;
                }
                catch (WebException)
                {
                    return null;
                }
            }
        }

        public string[] RetrieveData(string serverId)
        {
            string data;

            string[] results = new string[6];

            using (WebClient client = new WebClient())
            {
                try
                {
                    data = client.DownloadString("https://api.battlemetrics.com/servers/" + serverId);

                    dynamic result = JsonConvert.DeserializeObject(data);

                    results[0] = result["data"]["attributes"]["name"].ToString();
                    results[1] = result["data"]["attributes"]["players"].ToString();
                    results[2] = result["data"]["attributes"]["status"].ToString();
                    results[3] = result["data"]["attributes"]["maxPlayers"].ToString();
                    results[4] = result["data"]["attributes"]["ip"].ToString();
                    results[5] = result["data"]["attributes"]["port"].ToString();

                    return results;

                }
                catch (WebException)
                {
                    return null;
                }
            }
        }

        public string GetServerTime(string ip, string port)
        {
            string time = "";
            string gamePort = (int.Parse(port) - 2).ToString();
            string data;

            string apiURL = $"https://scumservers.net/api.php?ip={ip}&port={gamePort}";

            using (WebClient client = new WebClient())
            {
                try
                {
                    data = client.DownloadString(apiURL);
                    if ((data.StartsWith("{") && data.EndsWith("}"))){
                        try
                        {
                            dynamic result = JsonConvert.DeserializeObject(data);
                            time = result["serverTime"].ToString();
                            return time;
                        }
                        catch (JsonSerializationException)
                        {
                            return null;
                        }
                    }
                }
                catch (WebException)
                {
                    return null;
                }
            }

            return null;
        }

        public double Ping(string host, int echoNum)
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
