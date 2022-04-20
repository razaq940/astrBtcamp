using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Entities.Models;

namespace Northwind.Contracts.Interfaces
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetAllOrderDetail(bool trackChanges);

        OrderDetail GetOrderDetail(int oderId,int productId, bool trackChanges);

        void CreateOrderDetail(OrderDetail orderDetail);

        void DeleteOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
    }
}
