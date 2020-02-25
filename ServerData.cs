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
        string name;
        string ID;

        public Server(string argid, string argname)
        {
            this.name = argname;
            this.ID = argid;
        }

        public string getID()
        {
            return this.ID;
        }
        public string GetName()
        {
            return this.name;
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

            return "https://www.battlemetrics.com/servers/scum?q=" + serverString + "&sort=score";
        }

        public List<Server> GetServerID(string lookUpString)
        {

            string htmlCode;
            string data = null;

            List<Server> results = new List<Server>();

            using (WebClient client = new WebClient())
            {
                try
                {
                    htmlCode = client.DownloadString(lookUpString);

                    Regex serverIdReg = new Regex("{.*}}</script>");
                    Match serverIdMatch = serverIdReg.Match(htmlCode);

                    if (serverIdMatch.Success)
                    {
                        var result = serverIdMatch.ToString();
                        if (result.Contains("</script>"))
                        {
                            data = result.Remove(result.IndexOf("</script>"), "</script>".Length);
                        }
                    }

                    dynamic dataSet = JObject.Parse(data);
                    var servers = (JObject)dataSet["servers"]["servers"];

                    foreach (var server in servers)
                    {
                        results.Add(new Server(server.Key, server.Value.Value<string>("name")));
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

            string htmlCode;

            string[] results = new string[6];

            using (WebClient client = new WebClient())
            {
                try
                {
                    //htmlCode = client.DownloadString("https://api.battlemetrics.com/servers/2608083");
                    htmlCode = client.DownloadString("https://api.battlemetrics.com/servers/" + serverId);

                    dynamic result = JsonConvert.DeserializeObject(htmlCode);

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
