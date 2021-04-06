using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllStorageItemsOnUnitRequest : 
        RequestProvider<GetAllStorageItemsOnUnitHttpQuery, GetAllStorageItemsOnUnitSuccessHttpResponse>
    {
        public GetAllStorageItemsOnUnitRequest(IRequestManager requestManager, HttpClient httpClient, GetAllStorageItemsOnUnitHttpQuery request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<GetAllStorageItemsOnUnitSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"http://localhost:5003/Warehouse/GetAllStorageItemsOnUnit?UnitId={Request.UnitId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllStorageItemsOnUnitSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}