using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using Nano35.WebClient.Services;
using System;
using System.Net.Http;
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
            builder.Services.AddScoped<IPrintingService, PrintingService>();
            builder.Services.AddScoped<IInstanceService, InstanceService>();
            builder.Services.AddScoped<ISessionProvider, SessionProvider>();
            builder.Services.AddScoped<IToolTipService, ToolTipService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddTransient<RequestManager>();
            builder.Services.AddTransient<HealthService>();
            builder.Services.AddTransient<HttpGet>();
            builder.Services.AddTransient<HttpPost>();
            builder.Services.AddTransient<HttpPatch>();
            builder.Services.AddTransient<HttpDelete>();
            builder.Services.AddTransient<JsConsole>();
            builder.Services.AddBlazoredModal();
            
            await builder.Build().RunAsync();
        }
    }
}
