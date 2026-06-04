using MindFree.Interfaces;
using MindFree.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MindFree.Services
{
    public class ResultService
    {
        private HttpClient _httpClient { get; set; } = default!;
        private ICookie _cookie { get; set; } = default!;

        private ResultsResponse _resultsResponse = new();

        private List<Result> _results = new List<Result>();


        public ResultService(HttpClient httpClient, ICookie cookie) {
            _httpClient = httpClient;
            _cookie = cookie;
        }


        public async Task CloseResults(string month, string year)
        {
            if(string.IsNullOrEmpty(month) || string.IsNullOrEmpty(year)) throw new Exception("O mês ou o ano de fechamento não foram informados.");

            Result result = new Result();
            result.Year = year;
            result.Month = month;


            string _token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(_token))
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    await _httpClient.PostAsJsonAsync<Result>("closeresult", result);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<Result>> GetResults(string year)
        {

            if (string.IsNullOrEmpty(year)) throw new Exception("O ano de fechamento não foi informado.");

            string _token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(_token))
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    _resultsResponse = await _httpClient.GetFromJsonAsync<ResultsResponse>($"closeresults/{year}");
                    _results = _resultsResponse!.Results;

                    return _results;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return new List<Result>();
            }
        }
        public async Task OpenResult(string month, string year)
        {

            if (string.IsNullOrEmpty(month) || string.IsNullOrEmpty(year)) throw new Exception("O mês ou o ano de fechamento não foram informados.");

            string _token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(_token))
            {
                Result result = new Result();
                result.Year = year;
                result.Month = month;

                try
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    await _httpClient.PutAsJsonAsync<Result>("openresult", result);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


    }



    public class ResultsResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public List<Result> Results { get; set; }
    }

}
