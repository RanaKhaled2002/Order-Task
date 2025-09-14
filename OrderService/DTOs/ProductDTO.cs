using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DTOs
{
    public class ProductDTO
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
    }

    public class GetAllProductsDTO
    {
        public string sku { get; set; }
        public string qty { get; set; }
        public string price { get; set; }
        public string special_price { get; set; }
        public string is_in_stock { get; set; }
        public int ID { get; set; }
        public string ISActive { get; set; }
        public string InventoryID { get; set; }
    }

    public class ProductResponse
    {
        public List<GetAllProductsDTO> value { get; set; }
    }
}
