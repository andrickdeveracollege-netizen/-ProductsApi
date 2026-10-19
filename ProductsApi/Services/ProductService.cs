using ProductsApi.Exceptions;
using ProductsApi.Models.Domain;

namespace ProductsApi.Services;

public class ProductService
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;

    public IReadOnlyList<Product> GetAll()
    {
        return _products.AsReadOnly();
    }

    public Product GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id)
            ?? throw new NotFoundException(
                $"Product with ID {id} was not found.");
    }

    public Product Create(Product product)
    {
        EnsureUniqueSku(product.SKU);

        product.Id = _nextId++;
        _products.Add(product);

        return product;
    }

    public Product Update(int id, Product updatedProduct)
    {
        var existing = GetById(id);

        EnsureUniqueSku(updatedProduct.SKU, id);

        existing.Name = updatedProduct.Name;
        existing.SKU = updatedProduct.SKU;
        existing.Price = updatedProduct.Price;
        existing.SalePrice = updatedProduct.SalePrice;

        return existing;
    }

    public void Delete(int id)
    {
        var existing = GetById(id);
        _products.Remove(existing);
    }

    private void EnsureUniqueSku(string sku, int? excludeId = null)
    {
        var duplicate = _products.Any(p =>
            string.Equals(
                p.SKU,
                sku,
                StringComparison.OrdinalIgnoreCase)
            && p.Id != excludeId);

        if (duplicate)
        {
            throw new ConflictException(
                $"A product with SKU '{sku}' already exists.");
        }
    }
}