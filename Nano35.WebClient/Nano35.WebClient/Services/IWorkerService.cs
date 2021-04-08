using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public interface IWorkerService
    {
        Task<GetAllWorkersSuccessHttpResponse> GetAllWorkers(Guid id);
        Task<GetAllWorkerRolesSuccessHttpResponse> GetAllWorkerRoles();
        Task<CreateWorkerSuccessHttpResponse> Send(CreateUnitHttpBody request);
    }

    public class WorkerService : IWorkerService
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestManager _requestManager;

        public WorkerService(HttpClient httpClient, IRequestManager requestManager)
        {
            _httpClient = httpClient;
            _requestManager = requestManager;
        }
        
        public async Task<GetAllWorkersSuccessHttpResponse> GetAllWorkers(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_requestManager.InstanceServer}/Workers/GetAllWorkers?InstanceId={id}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllWorkersSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
        
        public async Task<GetAllWorkerRolesSuccessHttpResponse> GetAllWorkerRoles()
        {
            var response = await _httpClient.GetAsync($"{_requestManager.InstanceServer}/Workers/GetAllWorkerRoles");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllWorkerRolesSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }

        public async Task<CreateWorkerSuccessHttpResponse> Send(CreateUnitHttpBody request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_requestManager.InstanceServer}/Workers/CreateWorker", request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateWorkerSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}