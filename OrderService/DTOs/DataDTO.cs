using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DTOs
{
    public class DataDTO
    {
        public ValueField CustomerId { get; set; }
        public ValueField OrderType { get; set; }
        public ValueField Branch { get; set; }
        public ValueField Is2bMagento { get; set; }
        public ValueField Hold { get; set; }
        public ValueField HalanOrder { get; set; }
        public ValueField ClientName { get; set; }
        public List<OrderDetail> Details { get; set; }
    }

    public class ValueField
    {
        public string Value { get; set; }
    }

    public class OrderDetail
    {
        public ValueField InventoryID { get; set; }
        public ValueField OrderQty { get; set; }
        public ValueField WareHouseID { get; set; }
    }

    public class CancelOrder
    {
        public ValueField ordertype { get; set; }
        public ValueField OrderNbr { get; set; }
        public ValueField CancelReason { get; set; }
    }

    public class Cancel
    {
        public CancelOrder entity { get; set; }
    }

}
