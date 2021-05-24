using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class SetPerformerOfRepairOrderHttpQueryRequest : 
        RequestProvider<SetPerformerOfRepairOrderHttpQuery, SetPerformerOfRepairOrderHttpResponse>
    {
        public SetPerformerOfRepairOrderHttpQueryRequest(IRequestManager requestManager, HttpClient httpClient, SetPerformerOfRepairOrderHttpQuery request) : 
            base(requestManager, httpClient, request) { }

        public override async Task<SetPerformerOfRepairOrderHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.RepairOrdersServer}/RepairOrders/SetUserAsPerformerOfRepairOrder", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<SetPerformerOfRepairOrderHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}