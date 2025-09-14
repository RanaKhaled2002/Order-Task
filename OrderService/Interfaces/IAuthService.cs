using Microsoft.AspNetCore.Http;
using OrderService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Interfaces
{
    public interface IAuthService
    {
        public Task<TokenDTO> LoginAsync();
        public Task<string> GetTokenAsync();
    }
}
