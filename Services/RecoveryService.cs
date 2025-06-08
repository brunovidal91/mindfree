using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MindFree.Interfaces;
using MindFree.Models;

namespace MindFree.Services
{
    public class RecoveryService
    {
        private readonly HttpClient _httpClient;

        public RecoveryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RecoveryResponse> SendLink(string email)
        {
            if (string.IsNullOrEmpty(email)) {
                throw new Exception("Favor informar o e-mail");
            }

            User user = new User { Email = email};
            RecoveryResponse res = new RecoveryResponse();

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync<User>("recovery", user);

                
                res.StatusCode = response.StatusCode;
                res.Message = response.Content.ReadFromJsonAsync<RecoveryResponse>().Result.Message;

                if (response.IsSuccessStatusCode) {

                    res.IsSuccessStatusCode = true;
                    return res;
                }
                else
                {
                    res.IsSuccessStatusCode = false;
                    return res;
                }
            }
            catch (Exception ex) {
                res.Message += ex.Message;
                res.IsSuccessStatusCode = false;
                return res;
            }

        }
    }

    public class RecoveryResponse
    {
        public System.Net.HttpStatusCode StatusCode;
        public string Message { get; set; } = string.Empty;
        public bool IsSuccessStatusCode { get; set; }
    }
}
