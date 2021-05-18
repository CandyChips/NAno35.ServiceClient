using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllSellsRequest : 
        RequestProvider<GetAllSellsHttpQuery, GetAllSellsSuccessHttpResponse>
    {
        public GetAllSellsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllSellsHttpQuery request) : 
            base(requestManager, httpClient, request) { }

        public override async Task<GetAllSellsSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.StorageServer}/Sells?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllSellsSuccessHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}