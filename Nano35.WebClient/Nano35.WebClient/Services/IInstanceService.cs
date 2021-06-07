using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
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
        private readonly HttpGet _get;
        private readonly NavigationManager _navigationManager;

        public InstanceService(HttpClient httpClient, IRequestManager requestManager, ISessionProvider sessionProvider, HttpGet get, NavigationManager navigationManager)
        {
            _get = get;
            _httpClient = httpClient;
            _requestManager = requestManager;
            _sessionProvider = sessionProvider;
            _navigationManager = navigationManager;
        }
        public InstanceViewModel Instance { get; set; }
        
        public InstanceViewModel GetCurrentInstance()
        {
            return Instance;
        }

        public async Task SetInstanceById(Guid id)
        {
            await _get.InvokeAsync<GetInstanceByIdHttpResponse>(
                _requestManager.InstanceServer, $"Instances/{id}",
                resp =>
                {
                    if (resp.IsSuccess()) {Instance = resp.Success.Data;}
                    else
                    {
                        Console.WriteLine(resp.Error);
                        _navigationManager.NavigateTo("/instance-view");
                    }
                });
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