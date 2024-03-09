
using AutoMapper;
using OrderService.Dtos;
using OrderService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Tree;

namespace OrderService.Controllers
{ 
    [Route("api/Ordsrv/[controller]"),ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IOrderRepo _repo;
        private readonly IMapper _mapper;

        public ProductController(
            IOrderRepo repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductionReadDto>> GetProducts()
        {
            Console.WriteLine("---> Getting Products from OrderService");
            var productItems = _repo.GetAllProducts();
            return Ok(_mapper.Map<IEnumerable<ProductionReadDto>>(productItems));
        }        
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("---> Inbound post order service");
            return Ok("---> Inbound test of production controller");
        }
    }
}