using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetRepairOrderByIdRequest : 
        RequestProvider<GetRepairOrderByIdHttpBody, GetRepairOrderByIdSuccessHttpResponse>
    {
        public GetRepairOrderByIdRequest(IRequestManager requestManager, HttpClient httpClient, GetRepairOrderByIdHttpBody request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetRepairOrderByIdSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"http://localhost:5004/RepairOrder/GetRepairOrderById?Id={Request.Id}");
            if (response.IsSuccessStatusCode) 
            {
                return (await response.Content.ReadFromJsonAsync<GetRepairOrderByIdSuccessHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}