using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;
using Nano35.HttpContext.storage;
using Newtonsoft.Json;

namespace Nano35.WebClient.Services
{
    public class UpdateRepairOrderDeviceRequest : 
        RequestProvider<UpdateRepairOrdersDeviceBody, UpdateRepairOrdersDeviceSuccessResponse>
    {
        public UpdateRepairOrderDeviceRequest(IRequestManager requestManager, HttpClient httpClient, UpdateRepairOrdersDeviceBody request) : 
            base(requestManager, httpClient, request) {}
        

        public override async Task<UpdateRepairOrdersDeviceSuccessResponse> Send()
        {
            HttpContent req = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
            var response = await HttpClient.PatchAsync($"{RequestManager.RepairOrdersServer}/RepairOrders/Device",req);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UpdateRepairOrdersDeviceSuccessResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}