using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllStorageItemsRequest : 
        RequestProvider<GetAllStorageItemsQuery, GetAllStorageItemsSuccessHttpResponse>
    {
        public GetAllStorageItemsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllStorageItemsQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetAllStorageItemsSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"http://localhost:5003/StorageItems/GetAllStorageItems?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllStorageItemsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}