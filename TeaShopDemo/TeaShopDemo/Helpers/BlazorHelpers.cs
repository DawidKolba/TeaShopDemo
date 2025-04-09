using Microsoft.JSInterop;

namespace TeaShopDemo.Helpers
{
    public static class BlazorHelpers
    {
        public static async Task RegisterExternalNavigationHandlerAsync(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("registerExternalNavigationHandler");
        }
    }
}