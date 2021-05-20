using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class GetAllWorksRequest : 
        RequestProvider<GetAllWorksHttpQuery, GetAllWorksHttpResponse>
    {
        public GetAllWorksRequest(IRequestManager requestManager, HttpClient httpClient, GetAllWorksHttpQuery request) : 
            base(requestManager, httpClient, request) { }

        public override async Task<GetAllWorksHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.RepairOrdersServer}/Works?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllWorksHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}