using Microsoft.AspNetCore.Http;
using OrderService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Interfaces
{
    public interface IProductService
    {
        Task<List<GetAllProductsDTO>> GetAllProductsAsync();
    }
}
