using Microsoft.AspNetCore.Components;
using MindFree.Interfaces;
using MindFree.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MindFree.Services
{
    public class ConfigService
    {
        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public ICookie Cookie { get; set; }
        public Config Config { get; set; }

        public ConfigService(HttpClient httpClient, ICookie cookie) { 
            HttpClient = httpClient;
            Cookie = cookie;
        }

        public async Task<Config> GetConfig(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var config = await HttpClient.GetFromJsonAsync<Config>("config");

            await Cookie.SetValue($"config", $"{config.CurrentYear},{config.InvestmentRate.ToString()}");

            return config;
        }

        public async Task UpdateConfig(string token, Config config)
        {

            if(int.Parse(config.CurrentYear) < 2000 || int.Parse(config.CurrentYear) > DateTime.Now.Year)
            {
                throw new Exception($"O ano precisa estar entre 2000 e {DateTime.Now.Year}.");
            }

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await HttpClient.PutAsJsonAsync<Config>("config", config);
        }
    }
}
