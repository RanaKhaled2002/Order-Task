using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DTOs
{
    public class OrderRequestDTO
    {
        public List<ProductDTO> Products { get; set; }
    }
}
