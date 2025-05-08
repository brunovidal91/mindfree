
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using MindFree.Models;

namespace MindFree.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        public UserService( HttpClient httpClient) { 
            _httpClient = httpClient;
        }
        public async Task<User> Login(User user)
        {
            User userData = new();

            var userResp = await _httpClient.PostAsJsonAsync("login", user);

            if(userResp != null)
            {
                userData = await userResp.Content.ReadFromJsonAsync<User>();
            }

            return userData;
        }
    }
}
