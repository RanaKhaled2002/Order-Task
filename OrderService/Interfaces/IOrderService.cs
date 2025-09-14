using Microsoft.AspNetCore.Http;
using OrderService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> CreateOrderAsync(OrderRequestDTO request,HttpContext context);
        Task<string> CancelOrderAsync(Cancel cancel, HttpContext context);
    }
}
