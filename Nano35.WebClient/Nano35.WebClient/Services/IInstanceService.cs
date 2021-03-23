using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;
using Nano35.WebClient.Pages;

namespace Nano35.WebClient.Services
{
    public interface IInstanceService
    {
        public IInstanceViewModel Instance { get; set; }
        IInstanceViewModel GetCurrentInstance();
        Task SetInstanceById(Guid id);
        Task<bool> IsInstanceExist();
    }
    public class InstanceService :
        IInstanceService
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestManager _requestManager;
        private readonly ISessionProvider _sessionProvider;

        public InstanceService(HttpClient httpClient, IRequestManager requestManager, ISessionProvider sessionProvider)
        {
            _httpClient = httpClient;
            _requestManager = requestManager;
            _sessionProvider = sessionProvider;
        }
        public IInstanceViewModel Instance { get; set; }
        
        public IInstanceViewModel GetCurrentInstance()
        {
            return Instance;
        }

        public async Task SetInstanceById(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_requestManager.InstanceServer}/Instances/GetInstanceById?Id={id}");
            if (response.IsSuccessStatusCode)
            {
                Instance = (await response.Content.ReadFromJsonAsync<GetInstanceByIdSuccessHttpResponse>())?.Data;
            }
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