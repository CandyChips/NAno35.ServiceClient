using Nano35.HttpContext.identity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.HttpContext.instance;

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
        private readonly IRequestManager _requestManager;
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider  _customAuthenticationStateProvider;

        public AuthService(
            IRequestManager requestManager,
            ILocalStorageService localStorage,
            HttpClient httpClient,
            AuthenticationStateProvider  customAuthenticationStateProvider)
        {
            _requestManager = requestManager;
            _httpClient = httpClient;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
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
            var response = await new HttpGetRequest<GetUserFromTokenHttpResponse>(_httpClient, $"{_requestManager.IdentityServer}/Identity/FromToken").GetAsync();
            return response.Data;
        }

        public async Task<List<Guid>> GetCurrentRoles()
        {
            var response = await new HttpGetRequest<GetAllRolesByUserHttpResponse>(_httpClient, $"{_requestManager.InstanceServer}/Workers/Current/Roles").GetAsync();
            return response.Roles;
        }

        public Task Register(RegisterHttpBody model)
        {
            throw new NotImplementedException();
        }
    }
}