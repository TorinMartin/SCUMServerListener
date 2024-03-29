﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SCUMServerListener.API
{
    public record ServerSearchResult(string Id, string Name);
    
    public static class ApiUtil
    {
        private const string ApiHostUrl = "https://api.battlemetrics.com/servers";
        private const int RequestSize = 50;
        private const int Timeout = 120;
        private static readonly HttpClient Client = new();

        private static async Task<HttpResponseMessage> SendRequestAsync(string url)
        {
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public static async Task<List<ServerSearchResult>> GetServersAsync(string query)
        {
            var result = new List<ServerSearchResult>();

            try
            {
                var url = $"{ApiHostUrl}?page%5Bsize%5D={RequestSize}&filter%5Bgame%5D=scum&filter%5Bsearch%5D={Uri.EscapeDataString(query)}";
                var response = await SendRequestAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(json)) return result;
                
                dynamic resultObj = JObject.Parse(json);
                IEnumerable<dynamic> servers = resultObj["data"];
                result = servers.Select(server => new ServerSearchResult(server["attributes"]["id"].ToString(), server["attributes"]["name"].ToString())).ToList();
            }
            catch (HttpRequestException)
            {
                //TODO: Log
            }
            
            return result;
        }

        public static async Task<Server?> QueryServerByIdAsync(string? id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            
            try
            {
                var response = await SendRequestAsync($"{ApiHostUrl}/{id}"); 
                var json = await response.Content.ReadAsStringAsync();

                return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<Server>(json);
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
            var totalTime = 0L;
            var pingSender = new Ping();
            
            for (var i = 0; i < echoNum; i++)
            {
                var reply = pingSender.Send(host, Timeout);
                if (reply.Status == IPStatus.Success)
                {
                    totalTime += reply.RoundtripTime;
                }
            }
            return totalTime / echoNum;
        }
    }
}