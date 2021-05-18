using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetWorkerRolesByIdRequest : 
        RequestProvider<GetAllRolesByUserHttpQuery, GetAllRolesByUserSuccessHttpResponse>
    {
        public GetWorkerRolesByIdRequest(IRequestManager requestManager, HttpClient httpClient, GetAllRolesByUserHttpQuery request) : base(requestManager, httpClient, request) { }

        public override async Task<GetAllRolesByUserSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.InstanceServer}/Workers/{Request.UserId}/Roles");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllRolesByUserSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}