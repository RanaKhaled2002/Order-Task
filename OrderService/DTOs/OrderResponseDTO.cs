using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DTOs
{
    public class OrderResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ValueField ordertype { get; set; }
        public ValueField OrderNbr { get; set; }
        public ValueField CancelReason { get; set; }
    }
}
