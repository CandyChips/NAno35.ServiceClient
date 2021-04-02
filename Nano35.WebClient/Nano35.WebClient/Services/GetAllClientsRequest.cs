using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class GetAllClientsRequest : 
        RequestProvider<GetAllClientsHttpQuery, GetAllClientsSuccessHttpResponse>
    {
        public GetAllClientsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllClientsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetAllClientsSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.InstanceServer}/Clients/GetAllClients?InstanceId={Request.InstanceId}&ClientTypeId={Request.ClientTypeId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllClientsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}