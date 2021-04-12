using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllPlacesOfStorageItemOnUnitRequest : 
        RequestProvider<GetAllPlacesOfStorageItemOnUnitHttpQuery, GetAllPlacesOfStorageItemOnUnitSuccessHttpResponse>
    {
        public GetAllPlacesOfStorageItemOnUnitRequest(IRequestManager requestManager, HttpClient httpClient, GetAllPlacesOfStorageItemOnUnitHttpQuery request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<GetAllPlacesOfStorageItemOnUnitSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.LocalStorageServer}/Warehouse/GetAllPlacesOfStorageItemOnUnit?StorageItemId={Request.StorageItemId}&UnitContainsStorageItemId={Request.UnitContainsStorageItemId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllPlacesOfStorageItemOnUnitSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}