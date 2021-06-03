using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;
using Radzen;

namespace Nano35.WebClient.Services
{
    public interface IInstanceService
    {
        public InstanceViewModel Instance { get; set; }
        InstanceViewModel GetCurrentInstance();
        Task SetInstanceById(Guid id);
        Task<bool> IsInstanceExist();
    }
    public class InstanceService :
        IInstanceService
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestManager _requestManager;
        private readonly ISessionProvider _sessionProvider;
        private readonly NotificationService _notificationService;

        public InstanceService(HttpClient httpClient, IRequestManager requestManager, ISessionProvider sessionProvider, NotificationService notificationService)
        {
            _httpClient = httpClient;
            _requestManager = requestManager;
            _sessionProvider = sessionProvider;
            _notificationService = notificationService;
        }
        public InstanceViewModel Instance { get; set; }
        
        public InstanceViewModel GetCurrentInstance()
        {
            return Instance;
        }

        public async Task SetInstanceById(Guid id)
        {
            Instance = (await new HttpGetRequest<GetInstanceByIdHttpResponse>(_httpClient, _notificationService , $"{_requestManager.InstanceServer}/Instances/{id}").GetAsync()).Data;
        }

        public async Task<bool> IsInstanceExist()
        {
            if (Instance != null)
            {
                return true;
            }
            else
            {
                await SetInstanceById(await _sessionProvider.GetCurrentInstanceId());
                return true;
            }
        }


    }
}