using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChartJS.Blazor
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class ChartJSInterop : IAsyncDisposable
    {

        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        private readonly ILogger<ChartJSInterop> _logger;

        public ChartJSInterop(IJSRuntime jsRuntime, ILogger<ChartJSInterop> Logger)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BlazorChartJs/chartJSBlazor.js").AsTask());
            _logger = Logger;
        }

        public async ValueTask<bool> CreateChart(string id, ChartConfig configs)
        {
            var module = await _moduleTask.Value;
            var res = await module.InvokeAsync<bool>("createChart", id, configs);
            return res;
        }


        public async ValueTask DestroyChart(string id)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("destroyChart",id);
        }
        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
