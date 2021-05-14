using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllSelleDetailsRequest : 
        RequestProvider<GetAllSellDetailsHttpQuery, GetAllSellDetailsSuccessHttpResponse>
    {
        public GetAllSelleDetailsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllSellDetailsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetAllSellDetailsSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.StorageServer}/Sells/{Request.SelleId}/Details");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllSellDetailsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}