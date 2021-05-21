using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.Contracts.Cashbox.Artifacts;
using Nano35.HttpContext.Cashbox;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class GetAllRepairOrderCashOperations : 
        RequestProvider<GetAllRepairOrderOperationsHttpQuery, GetAllRepairOrderOperationsHttpResponse>
    {
        public GetAllRepairOrderCashOperations(IRequestManager requestManager, HttpClient httpClient, GetAllRepairOrderOperationsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetAllRepairOrderOperationsHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.CashboxServer}/Cashbox/RepairOrderOperations?unitId={Request.UnitId}&userId={Request.PerformerId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllRepairOrderOperationsHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}