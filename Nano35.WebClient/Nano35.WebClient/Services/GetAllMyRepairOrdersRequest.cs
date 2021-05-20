using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class GetAllMyRepairOrdersRequest : 
        RequestProvider<GetAllMyRepairOrdersHttpQuery, GetAllMyRepairOrdersHttpResponse>
    {
        public GetAllMyRepairOrdersRequest(IRequestManager requestManager, HttpClient httpClient, GetAllMyRepairOrdersHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetAllMyRepairOrdersHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.RepairOrdersServer}/RepairOrders/My");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllMyRepairOrdersHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}