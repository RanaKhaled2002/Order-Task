using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OrderService.DTOs;
using OrderService.Interfaces;
using System.Net.Http.Json;

namespace OrderService.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private TokenDTO _Token;

        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<TokenDTO> LoginAsync()
        {
            var form = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", "intern" },
                { "password", "Intern@123" },
                { "scope", "api" },
                { "client_id", "EF8E9DD7-8412-E177-C079-7CE5EABA5808@2B Egypt" },
                { "client_secret", "Onvph66SXCDmOqaZO3ZnnA" }
            };

            var response = await _httpClient.PostAsync(_configuration["TokenUrl"], new FormUrlEncodedContent(form));
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Token fetch failed: {response.StatusCode} - {error}");
            }

            var result = await response.Content.ReadFromJsonAsync<TokenDTO>();
            if (result?.Token == null)
                throw new Exception("Token not found");

            result.ExpirationTime = DateTime.UtcNow.AddSeconds(result.expires_in);

            _Token = result;

            return result;
        }

        public Task<string> GetTokenAsync()
        {
            if (_Token == null || string.IsNullOrEmpty(_Token.Token))
                throw new UnauthorizedAccessException("Unauthorized");

            if (_Token.ExpirationTime < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Token expired");

            return Task.FromResult(_Token.Token);
        }
    }
}
