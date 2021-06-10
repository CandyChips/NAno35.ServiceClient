using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Nano35.WebClient.Services
{
    public class JsConsole
    {
        private readonly IJSRuntime _jsRuntime;
        public JsConsole(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public async Task LogAsync(object message)
        {
            await _jsRuntime.InvokeVoidAsync("console.log", message);
        }
    }
}