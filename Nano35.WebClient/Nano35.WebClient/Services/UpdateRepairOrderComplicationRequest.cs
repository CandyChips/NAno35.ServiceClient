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
    public class UpdateRepairOrderComplicationRequest : 
        RequestProvider<UpdateRepairOrdersComplicationHttpBody, UpdateRepairOrdersComplicationHttpResponse>
    {
        public UpdateRepairOrderComplicationRequest(IRequestManager requestManager, HttpClient httpClient, UpdateRepairOrdersComplicationHttpBody request) : 
            base(requestManager, httpClient, request) {}
        

        public override async Task<UpdateRepairOrdersComplicationHttpResponse> Send()
        {
            HttpContent req = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
            var response = await HttpClient.PatchAsync($"{RequestManager.RepairOrdersServer}/RepairOrders/Complication",req);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UpdateRepairOrdersComplicationHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}