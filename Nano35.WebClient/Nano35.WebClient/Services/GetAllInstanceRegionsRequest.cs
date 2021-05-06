using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class GetAllInstanceRegionsRequest : 
        RequestProvider<GetAllRegionsHttpQuery, GetAllRegionsSuccessHttpResponse>
    {
        public GetAllInstanceRegionsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllRegionsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetAllRegionsSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.InstanceServer}/InstancesRegions");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllRegionsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}