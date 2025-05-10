
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MindFree.Interfaces;
using MindFree.Models;
using MindFree.Utils;

namespace MindFree
{
    public class Middleware
    {
        private string? _token { get; set; }
        private string _currentUri { get; set; } = null!;

        [Inject]
        private NavigationManager? _navigationManager { get; set; }

        [Inject]
        private ICookie? _cookie { get; set; }
        [Inject]
        private HttpClient? _httpClient { get; set; }



        public async Task Intercept()
        {
            _token = await _cookie.GetValue("app_token");
            //_currentUri = _navigationManager.Uri;
            bool check = await CheckToken(_token);

            if (!check)
            {
                _navigationManager.NavigateTo("login");
            }
            else
            {
                _navigationManager.NavigateTo("dashboard");

            }

        }

        private async Task<bool> CheckToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Me? me = await _httpClient.GetFromJsonAsync<Me>("me");

                if (string.IsNullOrEmpty(me.Email))
                {
                    await _cookie.SetValue("app_token", "", 1);
                    return false;
                }

                await _cookie.SetValue("userCode", me.Name);
                await _cookie.SetValue("erro", "");
                return true;

            }
            catch (Exception ex)
            {
                await _cookie.SetValue("erro", ex.Message);
                await _cookie.SetValue("app_token", "", 1);
                return false;
            }

        }
    }
}
