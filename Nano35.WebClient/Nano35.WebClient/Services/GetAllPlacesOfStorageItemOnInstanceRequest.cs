using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllPlacesOfStorageItemOnInstanceRequest : 
        RequestProvider<GetAllPlacesOfStorageItemOnInstanceHttpQuery, GetAllPlacesOfStorageItemOnInstanceSuccessHttpResponse>
    {
        public GetAllPlacesOfStorageItemOnInstanceRequest(IRequestManager requestManager, HttpClient httpClient, GetAllPlacesOfStorageItemOnInstanceHttpQuery request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<GetAllPlacesOfStorageItemOnInstanceSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"http://localhost:5003/Warehouse/GetAllPlacesOfStorageItemOnInstance?StorageItemId={Request.StorageItemId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllPlacesOfStorageItemOnInstanceSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}