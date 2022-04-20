using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Entities.DTO
{
    public class CartDto
    {
        public int ProductId { get; set; }
        public short Quantity { get; set; }
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
    }
}
