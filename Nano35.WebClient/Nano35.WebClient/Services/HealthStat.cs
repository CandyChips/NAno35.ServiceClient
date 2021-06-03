using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nano35.WebClient.Services
{

    public class HealthStatus
    {
        private readonly IRequestManager RequestManager;
        private readonly HttpClient _client;

        public HealthStatus(HttpClient client)
        {
            _client = client;
            RequestManager = new ClusterRequestManager(client);
            Availability = new Dictionary<string, bool>();
        }

        private readonly Dictionary<string, bool> Availability;

        public async Task<bool> Check(string uri, string endpoint)
        {
            if (Availability.ContainsKey(uri))
            {
                Availability.TryGetValue(uri, out var res);
                return res;
            }
            else
            {
                try
                {
                    (await _client.GetAsync($"{uri}/health")).EnsureSuccessStatusCode() ;
                    Availability.Add(uri,true);
                    return true;
                }
                catch (Exception e)
                {
                    Availability.Add(uri,false);
                    return false;
                }
            }
        }
    }
}