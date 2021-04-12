using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllStorageItemsOnInstanceRequest : 
        RequestProvider<GetAllStorageItemsOnInstanceHttpQuery, GetAllStorageItemsOnInstanceSuccessHttpResponse>
    {
        public GetAllStorageItemsOnInstanceRequest(IRequestManager requestManager, HttpClient httpClient, GetAllStorageItemsOnInstanceHttpQuery request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<GetAllStorageItemsOnInstanceSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.LocalStorageServer}/Warehouse/GetAllStorageItemsOnInstance?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllStorageItemsOnInstanceSuccessHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}