using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class GetAllWorkersRequest : 
        RequestProvider<GetAllWorkersHttpQuery, GetAllWorkersSuccessHttpResponse>
    {
        public GetAllWorkersRequest(IRequestManager requestManager, HttpClient httpClient, GetAllWorkersHttpQuery request) : 
            base(requestManager, httpClient, request) { }

        public override async Task<GetAllWorkersSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.InstanceServer}/Workers?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllWorkersSuccessHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}