
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MindFree.Interfaces;
using MindFree.Utils;

namespace MindFree
{
    public class Middleware
    {
        private string? _token { get; set; }
        private string _currentUri { get; set; } = null!;

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private ICookie _cookie { get; set; }

        public async Task Intercept()
        {

            _token = await _cookie.GetValue("app_token");
            _currentUri = _navigationManager.Uri;

            if (string.IsNullOrEmpty(_token)) {

                _navigationManager.NavigateTo("/login");

            }

        }

    }
}
