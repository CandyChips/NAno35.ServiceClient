using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class GetAllClientStatesRequest : 
        RequestProvider<GetAllClientStatesHttpQuery, GetAllClientStatesSuccessHttpResponse>
    {
        public GetAllClientStatesRequest(IRequestManager requestManager, HttpClient httpClient, GetAllClientStatesHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetAllClientStatesSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.InstanceServer}/ClientsStates");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllClientStatesSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}