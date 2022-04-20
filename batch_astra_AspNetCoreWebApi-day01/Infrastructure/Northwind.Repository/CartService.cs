using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Entities.Models;
using Northwind.Contracts.Interfaces;
using Northwind.Entities.Contexts;
using Northwind.Contracts;
using AutoMapper;

namespace Northwind.Repository.Models
{
    public class CartService : ICartService
    {
        private readonly IRepositoryManager _repository;
        
        

       

        public CartService(IRepositoryManager repository)
        {
            _repository = repository;
            
        }
        
        public Tuple<int, OrderDetail, string> AddProductToChart(int productId, int? quantity, string custId)
        {
            
            OrderDetail orderDetail = new OrderDetail();
            Order order = new Order();
            Product product = new Product();
            Customer customer = new Customer();
            try
            {

                product = _repository.Product.GetProduct(productId, trackChanges: false);
                order = _repository.Order.GetAllOrder(trackChanges: true).Where(c => c.CustomerId == custId && c.ShippedDate == null).SingleOrDefault();
               
                #region addNewOrder
                if (order == null)
                {
                    order.CustomerId = custId;
                    order.OrderDate = DateTime.Now;
                    _repository.Order.CreateOrder(order);
                    _repository.Save();
                }

                #endregion addNewOrder
                orderDetail = _repository.OrderDetail.GetOrderDetail(order.OrderId, productId, trackChanges: true);
                #region addNewOrderDetail
                if (orderDetail == null)
                {
                    orderDetail.OrderId = order.OrderId;
                    orderDetail.ProductId = productId;
                    orderDetail.UnitPrice = (decimal)product.UnitPrice;
                    orderDetail.Quantity = (short)quantity;
                    _repository.OrderDetail.CreateOrderDetail(orderDetail);
                    _repository.Save();
                }
                #endregion addNewOrderDetail
                #region addQuantityProduct
                else
                {
                    if(orderDetail.Quantity == null)
                    {
                        orderDetail.Quantity = (short)quantity;
                    }
                    else
                    {
                        orderDetail.Quantity += (short)quantity;
                    }
                    _repository.OrderDetail.UpdateOrderDetail(orderDetail);
                    _repository.Save();
                }
                #endregion addQuantityProduct
                return Tuple.Create(1, orderDetail, "Succes");

            }
            catch (Exception ex)
            {

                return Tuple.Create(-1, orderDetail, ex.Message);
            }
            
        }
        
        
        public Tuple<int, Order, string> Checkout(int id)
        {
            /*
            Order order1 = new Order();
            try
            {
                var order = repositoryContext.Orders.Find(id);
                if(order == null)
                {
                    return Tuple.Create(-1, order, "Order Id is noty fund");
                }
                order.RequiredDate = DateTime.Now;
                List<OrderDetail> orderDetail = repositoryContext.OrderDetails.Where(o => o.OrderId == id).ToList();
                foreach (var item in orderDetail)
                {
                    var product = repositoryContext.Products.Where(o => o.ProductId == item.ProductId).FirstOrDefault();
                    product.UnitsInStock -= item.Quantity;
                    repositoryContext.Update(product);
                    repositoryContext.SaveChanges();
                }
                repositoryContext.Orders.Update(order);
                repositoryContext.SaveChanges();
                return Tuple.Create(1, order, "Succes");

            }
            catch(Exception ex)
            {
                return Tuple.Create(-1, order1, ex.Message);
            }
            */
            throw new NotImplementedException();
        }
        
        public Tuple<int, IEnumerable<OrderDetail>, string> GetAllChart()
        {
            throw new NotImplementedException();
        }

        public Tuple<int, IEnumerable<Product>, string> GetAllProduct(bool trackChanges)
        {
            IEnumerable<Product> products1 = null;
            try
            {
                IEnumerable<Product> products = _repository.Product.GetAllProduct(trackChanges: false);

                return Tuple.Create(1, products, "succes");
            }
            catch(Exception ex)
            {
                return Tuple.Create(-1,products1, ex.Message);
            }
        }

        public Tuple<int, OrderDetail, string> ReduceProductQuantity(int productId)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, OrderDetail, string> RemoveAllChart()
        {
            throw new NotImplementedException();
        }

        public Tuple<int, OrderDetail, string> RemoveProductFromChart(int productId)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, Order, string> Shipped(int id)
        {
            throw new NotImplementedException();
        }
    }
}
