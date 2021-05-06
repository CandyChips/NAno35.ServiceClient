using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class GetAllRepairOrdersRequest : 
        RequestProvider<GetAllRepairOrdersBody, GetAllRepairOrdersSuccessResponse>
    {
        public GetAllRepairOrdersRequest(IRequestManager requestManager, HttpClient httpClient, GetAllRepairOrdersBody request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetAllRepairOrdersSuccessResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.RepairOrdersServer}/RepairOrders?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllRepairOrdersSuccessResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}