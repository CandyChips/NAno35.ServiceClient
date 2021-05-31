using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using Nano35.WebClient.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Append.Blazor.Printing;
using Blazored.LocalStorage;
using Blazored.Modal;
using Radzen;

namespace Nano35.WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
            builder.Services.AddScoped<IRequestManager, ClusterRequestManager>();
            builder.Services.AddScoped<IPrintingService, PrintingService>();
            builder.Services.AddScoped<IInstanceService, InstanceService>();
            builder.Services.AddScoped<ISessionProvider, SessionProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddBlazoredModal();
                       
            await builder.Build().RunAsync();
        }
    }
}
