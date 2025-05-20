using MindFree.Models;
using MindFree.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MindFree.Services
{
    public class TransactionService
    {
        private HttpClient _httpClient { get; set; } = default!;
        private ICookie _cookie { get; set; } = default!;

        private TransactionResponse _transactionResponse { get; set; } = new();

        public TransactionService(HttpClient httpClient, ICookie cookie) { 
            _cookie = cookie;
            _httpClient = httpClient;
        }

        public async Task<List<Transaction>> GetTransactions(string date, string datareq, string month, string year)
        {
            string _token = await _cookie.GetValue("app_token");
            List<Transaction> _transactions = new List<Transaction>();

            if (!string.IsNullOrEmpty(_token))
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    _transactionResponse = await _httpClient.GetFromJsonAsync<TransactionResponse>($"transactions/12/{datareq}/{month}/{year}");
                    _transactions = _transactionResponse.Transactions;
                    return _transactions;
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "teste");
                    
                }
            }
            else
            {
                return new List<Transaction>();
            }
        }
    
        public async Task InsertTransaction(Transaction transaction)
        {
            if (transaction == null) throw new Exception("O lançamento não pode ser nula");
            if (transaction.Category == null) throw new Exception("A categoria não pode ser nula");
            if (transaction.Category.title == null) throw new Exception("O título da categoria não pode ser nulo");
            if (transaction.Year == null) throw new Exception("O ano do lançamento não pode ser nulo");
            if (transaction.Date == null) throw new Exception("O dia do lançamento não pode ser nulo");


            string _token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(_token))
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    await _httpClient.PostAsJsonAsync<Transaction>("transactions", transaction);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task DeleteTransaction(Transaction transaction)
        {
            if (transaction == null) throw new Exception("O lançamento não pode ser nulo.");
            if (string.IsNullOrEmpty(transaction.Id)) throw new Exception("O id do lançamento não pode ser nulo.");

            string _token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(_token)) {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    await _httpClient.DeleteAsync($"transactions/{transaction.Id}");

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
    }

    public class TransactionResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
    }
}
