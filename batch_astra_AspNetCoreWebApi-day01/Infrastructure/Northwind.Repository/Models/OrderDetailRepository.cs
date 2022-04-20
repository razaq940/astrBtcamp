using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Entities.Models;
using Northwind.Contracts.Interfaces;
using Northwind.Entities.Contexts;

namespace Northwind.Repository.Models
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            Create(orderDetail);
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            Delete(orderDetail);
        }

        public IEnumerable<OrderDetail> GetAllOrderDetail(bool trackChanges) =>
            FindAll(trackChanges)
                .ToList();

        public OrderDetail GetOrderDetail(int orderId, int productId, bool trackChanges) =>
            FindByCondition(c => c.OrderId.Equals(orderId) && c.ProductId.Equals(productId), trackChanges).SingleOrDefault();


        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            Update(orderDetail);
        }
    }
}
