using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualShop.ProductApi.DTOs;
using VirtualShop.ProductApi.Services;

namespace VirtualShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var categoriesDto = await _productService.GetProducts();

            if (categoriesDto is null)
                return NotFound("Categories not found");

            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var ProductDTO = await _productService.GetProductById(id);

            if (ProductDTO is null)
                return NotFound("product not found");

            return Ok(ProductDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Invalid Data");

            await _productService.AddProduct(productDTO);

            return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
                return BadRequest();

            if (productDTO is null)
                return BadRequest();

            await _productService.UpdateProduct(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var ProductDTO = await _productService.GetProductById(id);

            if (ProductDTO is null)
                return NotFound("product not found");

            await _productService.RemoveProduct(id);

            return Ok(ProductDTO);
        }
    }
}
