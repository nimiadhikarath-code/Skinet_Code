using Core.Entities;

namespace Core.Interface;

public interface IproductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand
    , string? type,string? sort);
    Task<Product?> GetProductByIdAsync(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}