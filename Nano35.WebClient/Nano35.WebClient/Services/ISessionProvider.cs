using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Nano35.WebClient.Services
{
    public interface ISessionProvider
    {
        Task<Guid> GetCurrentUserId();
        Task SetCurrentUserId(Guid id);
        Task RemoveCurrentUserId();
        Task<Guid> GetCurrentInstanceId();
        Task SetCurrentInstanceId(Guid id);
        Task RemoveCurrentInstanceId();
        Task<bool> IsCurrentSessionIdExist();
        Task<Guid> GetCurrentUnitId();
        Task SetCurrentUnitId(Guid id);
        Task RemoveCurrentUnitId();
    }

    public class SessionProvider : ISessionProvider
    {         
        
        private readonly ILocalStorageService _localStorage;

        public SessionProvider(
            ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<Guid> GetCurrentUserId()
        {
           
            return await _localStorage.GetItemAsync<Guid>("CurrentUserId");
        }

        public async Task SetCurrentUserId(Guid id)
        {
            await _localStorage.SetItemAsync("CurrentUserId", id);
        }

        public async Task RemoveCurrentUserId()
        {
            await _localStorage.RemoveItemAsync("CurrentUserId");
        }

        public async Task<Guid> GetCurrentInstanceId()
        {
            
            return await _localStorage.GetItemAsync<Guid>("CurrentInstanceId");
        }

        public async Task SetCurrentInstanceId(Guid id)
        {
            await _localStorage.SetItemAsync("CurrentInstanceId", id);
        }

        public async Task RemoveCurrentInstanceId()
        {
            await _localStorage.RemoveItemAsync("CurrentInstanceId");
        }

        public async Task<bool> IsCurrentSessionIdExist()
        {
           return await _localStorage.ContainKeyAsync("CurrentInstanceId");
        }

        public async Task<Guid> GetCurrentUnitId()
        {
            Console.Write("qwe");
            return await _localStorage.GetItemAsync<Guid>("CurrentUnitId");
        }

        public async Task SetCurrentUnitId(Guid id)
        {
            await _localStorage.SetItemAsync("CurrentUnitId", id);
        }

        public async Task RemoveCurrentUnitId()
        {
            await _localStorage.RemoveItemAsync("CurrentUnitId");
        }
        
        
    }
}