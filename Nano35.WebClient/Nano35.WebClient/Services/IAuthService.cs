using Nano35.HttpContext.identity;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.WebClient.Services
{
    public interface IAuthService
    {
        Task<GenerateUserTokenSuccessHttpResponse> Login(GenerateUserTokenHttpBody loginRequest);
        Task LogOut();
        Task<GetUserByIdResultContract> GetCurrentUser();
        Task<RegisterSuccessHttpResponse> Register(RegisterHttpBody model);
    }

    public class AuthService : IAuthService
    {
        [Inject] private ISessionProvider SessionProvider { get; set; }
        
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

        public async Task<GenerateUserTokenSuccessHttpResponse> Login(GenerateUserTokenHttpBody loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync($"{_requestManager.IdentityServer}/Identity/Authenticate", loginRequest);
            if (!result.IsSuccessStatusCode)
                throw new Exception(await result.Content.ReadAsStringAsync());
            var success = await result.Content
                .ReadFromJsonAsync<GenerateUserTokenSuccessHttpResponse>();
            await _localStorage.SetItemAsync("authToken", success?.Token);
            ((CustomAuthenticationStateProvider) _customAuthenticationStateProvider).NotifyAsAuthenticated(
                success?.Token);
            return success;
        }

        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthenticationStateProvider) _customAuthenticationStateProvider).NotifyAsLogout();
        }

        public async Task<GetUserByIdResultContract> GetCurrentUser()
        {
            var result = await _httpClient.GetAsync($"{_requestManager.IdentityServer}/Identity/FromToken");
            if (!result.IsSuccessStatusCode)
                throw new Exception(await result.Content.ReadAsStringAsync());
            else
            {
                return await result.Content.ReadFromJsonAsync<GetUserByIdResultContract>();
            }
        }

        public Task<RegisterSuccessHttpResponse> Register(RegisterHttpBody model)
        {
            throw new NotImplementedException();
        }
    }
}