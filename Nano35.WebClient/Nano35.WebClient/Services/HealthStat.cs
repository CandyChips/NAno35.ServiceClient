using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nano35.WebClient.Services
{

    public class HealthStatus
    {
        private readonly IRequestManager RequestManager;

        public HealthStatus(HttpClient client)
        {
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
                Console.WriteLine(uri);
                var state = await RequestManager.HealthCheck(uri);
                Availability.Add(uri,state);
                return state;
            }
        }
    }
}