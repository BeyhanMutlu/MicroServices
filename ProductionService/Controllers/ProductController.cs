using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductionService.Dtos;
using ProductionService.Interfaces;
using ProductionService.Models;
using ProductionService.SyncDataServices.Http;

namespace ProductionService.Controllers
{
    [Route("api/[controller]"),ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repo;
        private readonly IMapper _mapper;
        private readonly IOrderDataClient _orderDataClient;
         private readonly IMessageBusClient _messageBusClient;

        public ProductController(
            IProductRepo repo, 
            IMapper mapper,
            IOrderDataClient orderDataClient,
            IMessageBusClient messageBusClient)
        {
            _repo = repo;
            _mapper = mapper;
            _orderDataClient = orderDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetProducts()
        {
            var res = _repo.GetAllProducts(); 
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(res));
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ProductReadDto> GetProductById(int id)
        {
            var res = _repo.GetProductById(id);
            if(res !=null)
            return Ok(_mapper.Map<ProductReadDto>(res));
            else
            return NotFound();
        }

        [HttpPost]        
        public async Task<ActionResult<ProductReadDto>> CreateProduct(ProductCreateDto productInfo)
        {
            var productModel = _mapper.Map<Product>(productInfo);
            _repo.CreateProduct(productModel);
            _repo.SaveChanges();

            var productReadDto = _mapper.Map<ProductReadDto>(productModel);

            #region send sync msg
            try
            {
                await _orderDataClient.SendProductToOrder(productReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"---> Could not send synchronously: {ex.Message}");
            }
            #endregion
            #region send async msg
            try
            {
                var productPublishedDto = _mapper.Map<ProductPublishedDto>(productReadDto);
                productPublishedDto.Event = "Product_Published";
                _messageBusClient.PublishNewProduct(productPublishedDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"---> Could not send asynchronously: {ex.Message}");
            }
            #endregion         

            return CreatedAtRoute(nameof(GetProductById), new {Id = productReadDto.Id},productReadDto);
        }

    }
}