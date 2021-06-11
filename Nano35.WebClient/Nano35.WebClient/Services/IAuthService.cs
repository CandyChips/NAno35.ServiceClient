using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Nano35.Contexts.Http.Identity;
using Nano35.Contexts.Http.Instance;
using Nano35.Contracts.Identity.Models;

namespace Nano35.WebClient.Services
{
    public interface IAuthService
    {
        Task Login(string token);
        Task LogOut();
        Task<UserViewModel> GetCurrentUser();
        Task<List<Guid>> GetCurrentRoles();
        Task Register(RegisterHttpBody model);
    }

    public class AuthService : IAuthService
    {
        private readonly RequestManager _requestManager;
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider  _customAuthenticationStateProvider;
        private readonly HttpGet _get;
        private readonly NavigationManager _navigationManager;
        public AuthService(
            RequestManager requestManager,
            ILocalStorageService localStorage,
            HttpClient httpClient,
            AuthenticationStateProvider  customAuthenticationStateProvider, 
            HttpGet get, 
            NavigationManager navigationManager)
        {
            _requestManager = requestManager;
            _httpClient = httpClient;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
            _get = get;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
        }

        public async Task Login(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
            ((CustomAuthenticationStateProvider) _customAuthenticationStateProvider).NotifyAsAuthenticated(token);
        }

        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthenticationStateProvider) _customAuthenticationStateProvider).NotifyAsLogout();
        }

        public async Task<UserViewModel> GetCurrentUser()
        {
            var response = new UserViewModel();
            await _get.InvokeAsync<GetUserFromTokenHttpResponse>(
                $"Identity/FromToken",
                resp =>
                {
                    if (resp.IsSuccess()) {response = resp.Success.Data;}
                    else
                    {
                        _navigationManager.NavigateTo("/instance-view");
                    }
                });
            return response;
        }

        public async Task<List<Guid>> GetCurrentRoles()
        {
            var response = new List<Guid>();
            await _get.InvokeAsync<GetAllRolesByUserHttpResponse>($"Workers/Current/Roles",
                resp =>
                {
                    if (resp.IsSuccess()) {response = resp.Success.Roles.ToList();}
                    else
                    {
                        Console.WriteLine(resp.Error);
                        _navigationManager.NavigateTo("/instance-view");
                    }
                });
            return response;
        }

        public Task Register(RegisterHttpBody model)
        {
            throw new NotImplementedException();
        }
    }
}