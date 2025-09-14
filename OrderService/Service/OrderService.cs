using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OrderService.DTOs;
using OrderService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Service
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public OrderService(HttpClient httpClient,IAuthService authService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _authService = authService;
            _configuration = configuration;
        }

        public async Task<OrderResponseDTO> CreateOrderAsync(OrderRequestDTO request,HttpContext context)
        {
            var data = new DataDTO
            {
                CustomerId = new ValueField { Value = "INTERN" },
                OrderType = new ValueField { Value = "DW" },
                Branch = new ValueField { Value = "2" },
                Is2bMagento = new ValueField { Value = "false" },
                Hold = new ValueField { Value = "true" },
                HalanOrder = new ValueField { Value = "true" },
                ClientName = new ValueField { Value = "01090661898" },
                Details = request.Products.Select(p => new OrderDetail
                {
                    InventoryID = new ValueField { Value = p.SKU },
                    OrderQty = new ValueField { Value = p.Quantity.ToString() },
                    WareHouseID = new ValueField { Value = "K0" }
                }).ToList()
            };

            //var token = await _authService.GetTokenAsync();

            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                throw new UnauthorizedAccessException("Unauthorize");

            var token = authHeader.Substring("Bearer ".Length);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsJsonAsync(_configuration["CreateOrder"], data);

            if (!response.IsSuccessStatusCode) return new OrderResponseDTO { Success = false, Message = "Order Failed" };

            var responseData = await response.Content.ReadFromJsonAsync<OrderResponseDTO>();

            return new OrderResponseDTO
            {
                Success = true,
                Message = "Order Created",
                OrderNbr = responseData.OrderNbr,
                ordertype = responseData.ordertype,
                CancelReason = responseData.CancelReason
            };
        }

        public async Task<string> CancelOrderAsync(Cancel cancel,HttpContext context)
        {
            //var token = await _authService.GetTokenAsync();
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                throw new UnauthorizedAccessException("Unauthorize");


            var token = authHeader.Substring("Bearer ".Length);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync(_configuration["CancelOrder"], cancel);

            if (!response.IsSuccessStatusCode) return "Failed";

            return "Successed";
        }
    }
}
