using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class SetUserAsPerformerOfRepairOrderQueryRequest : 
        RequestProvider<SetUserAsPerformerOfRepairOrderQuery, SetUserAsPerformerOfRepairOrderSuccessResponse>
    {
        public SetUserAsPerformerOfRepairOrderQueryRequest(IRequestManager requestManager, HttpClient httpClient, SetUserAsPerformerOfRepairOrderQuery request) : 
            base(requestManager, httpClient, request) { }

        public override async Task<SetUserAsPerformerOfRepairOrderSuccessResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.RepairOrdersServer}/RepairOrders/SetUserAsPerformerOfRepairOrder", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<SetUserAsPerformerOfRepairOrderSuccessResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}