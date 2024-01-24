using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiKoppelingTesten.ApiService
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiClient(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<string> PostAsync(string endpoint, string content)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}", new StringContent(content));
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return $"Error: {ex.Message}";
            }
        }
    }
}
