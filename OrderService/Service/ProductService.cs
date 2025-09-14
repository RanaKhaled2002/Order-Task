using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using OrderService.DTOs;
using OrderService.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;
        private readonly IAuthService _authService;

        public ProductService(HttpClient httpClient, IConfiguration configuration, IDistributedCache cache,IAuthService authService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cache = cache;
            _authService = authService;
        }

        public async Task<List<GetAllProductsDTO>> GetAllProductsAsync()
        {
            const string productsCacheKey = "AllProductsRedis";

            var token = await _authService.GetTokenAsync();

            var cachedProducts = await _cache.GetStringAsync(productsCacheKey);
            if (!string.IsNullOrEmpty(cachedProducts))
            {
                return JsonSerializer.Deserialize<List<GetAllProductsDTO>>(cachedProducts);
            }

            var request = new HttpRequestMessage(HttpMethod.Get, _configuration["ProductsUrl"]);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException("Token is expired or invalid");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ProductResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var products = data?.value ?? new List<GetAllProductsDTO>();

            var serialized = JsonSerializer.Serialize(products);
            await _cache.SetStringAsync(productsCacheKey, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });

            return products;
        }
    }
}
