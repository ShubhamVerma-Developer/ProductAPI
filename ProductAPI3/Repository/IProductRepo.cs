using ProductAPI3.Models;

namespace ProductAPI3.Repository
{
    public interface IProductRepo
    {
        Task<List<ProductModel>> GetAll();
        Task<ProductModel> GetById(int id);
        Task<string> Create(ProductModel product);
        Task<string> Update(ProductModel product, int id);
        Task<string> Remove(int id);

    }
}
