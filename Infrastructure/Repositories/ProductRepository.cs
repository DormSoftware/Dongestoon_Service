using Domain.Entities;
using FuzzySharp;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Infrastructure.Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ICollection<Product> GetAllProducts()
    {
        return _dbContext.Products.ToList();
    }

    public ICollection<Product> FindProductByName(string name)
    {
        return _dbContext.Products.Where(x => x.Name.Contains(name)).ToList();
    }

    public Product FindProductById(Guid id)
    {
        return _dbContext.Products.SingleOrDefault(x => x.Id == id) ??
               throw new NoProductFoundWithGivenException(id);
    }

    public ICollection<Product> GetAllProductWithCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public ICollection<Product> SearchInProducts()
    {
        throw new NotImplementedException();
    }
}