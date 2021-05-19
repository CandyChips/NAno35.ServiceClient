using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetStorageItemByIdRequest : 
        RequestProvider<GetStorageItemByIdHttpQuery, GetStorageItemByIdSuccessHttpResponse>
    {
        public GetStorageItemByIdRequest(IRequestManager requestManager, HttpClient httpClient, GetStorageItemByIdHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetStorageItemByIdSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.StorageServer}/StorageItems/{Request.Id}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetStorageItemByIdSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}