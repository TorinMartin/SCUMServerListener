using Newtonsoft.Json;

namespace SCUMServerListener.API
{
    public class Server
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
    }

    public class Details
    {
        [JsonProperty("time")]
        public string Time { get; set; }
    }

    public class Attributes
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("players")]
        public int Players { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("maxPlayers")]
        public int MaxPlayers { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }
        
        [JsonProperty("details")]
        public Details Details { get; set; }
    }
}
