using AutoMapper;
using VirtualShop.ProductApi.DTOs;
using VirtualShop.ProductApi.Models;
using VirtualShop.ProductApi.Repositories;

namespace VirtualShop.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsEntities = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntities);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var productEntity = await _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task AddProduct(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.Create(productEntity);
            productDTO.Id = productEntity.Id;
        }

        public async Task UpdateProduct(ProductDTO ProductDTO)
        {
            var productEntity = _mapper.Map<Product>(ProductDTO);
            await _productRepository.Update(productEntity);
        }

        public async Task RemoveProduct(int id)
        {
            await _productRepository.Delete(id);
        }

    }
}
