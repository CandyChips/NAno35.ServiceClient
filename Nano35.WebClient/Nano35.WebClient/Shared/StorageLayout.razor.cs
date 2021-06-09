using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Nano35.WebClient.Services;


namespace Nano35.WebClient.Shared
{
    public partial class StorageLayout
    {
        [Inject] private ISessionProvider SessionProvider { get; set; }
        [Inject] private HealthService HealthService { get; set; }
        [Inject] private IInstanceService InstanceService { get; set; }
        
        private bool _serverAvailable = false;
        private bool _loading;
        
        protected override async Task OnInitializedAsync()
        {
            _loading = false;
            _serverAvailable = await HealthService.CheckAsync();
            if(!_serverAvailable)
                NavigationManager.NavigateTo("/instances");
            if(!await SessionProvider.IsCurrentSessionIdExist())
                NavigationManager.NavigateTo("/instances");
            await InstanceService.IsInstanceExist();
            _loading = true;
        }
    }
}