﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Entities.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public string ShipName { get; set; }
        public DateTime? ShippedDate { get; set; }


    }
}
