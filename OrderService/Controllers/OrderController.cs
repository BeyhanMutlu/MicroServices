using System.ComponentModel.Design;
using AutoMapper;
using OrderService.Dtos;
using OrderService.Interfaces;
using OrderService.Models;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/Ordsrv/products/{productId}/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _repo;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepo repo,IMapper mapper)
        {
            _repo=repo;
            _mapper=mapper;            
        }
        [HttpGet]
        public ActionResult<IEnumerable<OrderReadDto>> GetOrdersForProduct(int productId)
        {
            Console.WriteLine($"---> Hit GetOrdersForProduct: {productId}");
            if(!_repo.ProductExists(productId))
            {
                return NotFound();
            }
            var orders = _repo.GetOrdersForProduct(productId);
            return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(orders));
        }
        [HttpGet("{orderId}",Name ="GetOrderForProduct")]
        public ActionResult<OrderReadDto> GetOrderForProduct(int productId,int orderId)
        {
            Console.WriteLine($"---> Hit GetOrderForProduct: {productId} / {orderId}");
            if(!_repo.ProductExists(productId))
            {
                return NotFound();
            }
            var order = _repo.GetOrder(productId,orderId);
            if(order == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderReadDto>(order));
        }
        [HttpPost]
        public ActionResult<OrderReadDto> CreateOrderForProduct(int productId,OrderCreateDto orderCDto)
        {
            Console.WriteLine($"---> Hit CreateOrderForProduct: {productId}");
            if(!_repo.ProductExists(productId))
            {
                return NotFound();
            }
            var order = _mapper.Map<Order>(orderCDto);
            _repo.CreateOrder(productId,order);
            _repo.SaveChanges();

            var orderReadDto = _mapper.Map<OrderReadDto>(order);
            return CreatedAtRoute(nameof(GetOrderForProduct),
                new {productId = productId, orderID = orderReadDto.Id},
                orderReadDto);
        }
    }
}