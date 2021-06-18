using Microsoft.AspNetCore.Components;
using Radzen;

namespace Nano35.WebClient.Services
{
    public interface IToolTipService
    {
        void ShowToolTip(ElementReference elementReference, string text, TooltipOptions options = null);
    }
    
    public class ToolTipService : IToolTipService
    {
        private readonly TooltipService _tooltipService;
        public ToolTipService(TooltipService tooltipService) { _tooltipService = tooltipService; }
        public void ShowToolTip(ElementReference elementReference, string text, TooltipOptions options = null) => 
            _tooltipService.Open(elementReference, text, options);
    }
}