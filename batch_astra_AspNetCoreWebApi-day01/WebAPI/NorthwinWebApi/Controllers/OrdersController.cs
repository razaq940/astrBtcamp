using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Contracts;
using System;
using System.Linq;
using Northwind.Entities.DTO;
using AutoMapper;
using System.Collections.Generic;
using Northwind.Entities.Models;
using Northwind.Contracts.Interfaces;


namespace NorthwinWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ICartService _cartService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public OrdersController(IRepositoryManager repository, ICartService cartService, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _cartService = cartService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Shipped")]
        
        public IActionResult Shipped(ShippedDto shippedDto, int id)
        {
            try
            {
                var result = _cartService.Shipped(shippedDto, id);
                if (result.Item1 == -1)
                {
                    return BadRequest(result.Item3);
                }
                return Ok(_mapper.Map<OrderDto>(result.Item2));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Checkout")]
        public IActionResult CheckOut(int orderId)
        {
            try
            {
                var result = _cartService.Checkout(orderId);
                if(result.Item1 == -1)
                {
                    return BadRequest(result.Item3);
                }
                return Ok(_mapper.Map<OrderDto>(result.Item2));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost("AddToCart")]
        public IActionResult AddToChart(CartDto cartDto)
        {
            try
            {
                var result = _cartService.AddProductToChart(cartDto.ProductId, cartDto.Quantity, cartDto.CustomerId);
                if(result.Item1 == -1)
                {
                    return BadRequest();
                }
                return Ok(_mapper.Map<OrderDetailDto>(result.Item2));


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            try
            {
                
                var result = _cartService.GetAllProduct(trackChanges: false);
                
                return Ok(_mapper.Map<IEnumerable<ProductDto>>(result.Item2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       

    }
}
