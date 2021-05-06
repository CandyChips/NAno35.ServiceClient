using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class GetAllMyRepairOrdersRequest : 
        RequestProvider<GetAllMyRepairOrdersBody, GetAllMyRepairOrdersSuccessResponse>
    {
        public GetAllMyRepairOrdersRequest(IRequestManager requestManager, HttpClient httpClient, GetAllMyRepairOrdersBody request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetAllMyRepairOrdersSuccessResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.RepairOrdersServer}/RepairOrders/My");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllMyRepairOrdersSuccessResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}