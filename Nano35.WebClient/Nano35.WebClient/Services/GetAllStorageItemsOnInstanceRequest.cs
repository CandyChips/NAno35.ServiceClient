using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllStorageItemsOnInstanceRequest : 
        RequestProvider<GetAllStorageItemsOnInstanceHttpQuery, GetAllStorageItemsOnInstanceSuccessResultContract>
    {
        public GetAllStorageItemsOnInstanceRequest(IRequestManager requestManager, HttpClient httpClient, GetAllStorageItemsOnInstanceHttpQuery request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<GetAllStorageItemsOnInstanceSuccessResultContract> Send()
        {
            var response = await HttpClient.GetAsync($"http://localhost:5003/Warehouse/GetAllStorageItemsOnInstance?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllStorageItemsOnInstanceSuccessResultContract>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}