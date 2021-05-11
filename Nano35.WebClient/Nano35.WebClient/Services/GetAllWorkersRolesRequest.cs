using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class GetAllWorkersRolesRequest : 
        RequestProvider<GetAllWorkerRolesHttpQuery, GetAllWorkerRolesSuccessHttpResponse>
    {
        public GetAllWorkersRolesRequest(IRequestManager requestManager, HttpClient httpClient, GetAllWorkerRolesHttpQuery request) : 
            base(requestManager, httpClient, request) { }

        public override async Task<GetAllWorkerRolesSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.InstanceServer}/WorkersRoles");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllWorkerRolesSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}