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
    public class GetAllWageCashOperations : 
        RequestProvider<GetAllWageOperationsHttpQuery, GetAllWageOperationsHttpResponse>
    {
        public GetAllWageCashOperations(IRequestManager requestManager, HttpClient httpClient, GetAllWageOperationsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetAllWageOperationsHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.CashboxServer}/Cashbox/WageOperations?unitId={Request.UnitId}&userId={Request.WorkerId}&isWageConfirmed={(int) Request.IsWageConfirmed}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllWageOperationsHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}