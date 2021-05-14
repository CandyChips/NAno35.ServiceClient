using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllPlacesOnStorageRequest : 
        RequestProvider<GetAllPlacesOnStorageHttpContext, GetAllPlacesOnStorageSuccessHttpResponse>
    {
        public GetAllPlacesOnStorageRequest(IRequestManager requestManager, HttpClient httpClient, GetAllPlacesOnStorageHttpContext request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<GetAllPlacesOnStorageSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync(
                $"{RequestManager.StorageServer}/Warehouse/GetAllPlacesOnStorage?" 
                + (Request.UnitId == Guid.Empty ? "" : $"UnitId={Request.UnitId}")
                + (Request.StorageItemId == Guid.Empty ? "" : $"StorageItemId={Request.StorageItemId}"));
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllPlacesOnStorageSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}