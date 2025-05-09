using Microsoft.AspNetCore.Components;
using MindFree.Interfaces;

namespace MindFree.Services
{
    public class Logout
    {
        [Inject]
        private ICookie _cookie { get; set; } = null!;

        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

        public async Task Leave()
        {
            await _cookie.SetValue("app_token", "", 0);
            _navigationManager.NavigateTo("login");

        }
    }
}
