using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Entities.Models;

namespace Northwind.Contracts.Interfaces
{
    public interface ICartService
    {
        Tuple<int, IEnumerable<Product>, string> GetAllProduct(bool trackChanges);
        Tuple<int, IEnumerable<OrderDetail>?, string> GetAllChart();
        Tuple<int, OrderDetail?, string> RemoveAllChart();
        Tuple<int, OrderDetail? ,string> AddProductToChart(int productId, int? quantity, string custId);
        Tuple<int, OrderDetail?, string> ReduceProductQuantity(int productId);
        Tuple<int, OrderDetail?, string> RemoveProductFromChart(int productId);
        Tuple<int, Order?, string> Checkout(int id);
        Tuple<int, Order?, string> Shipped(int id);



    }
}
