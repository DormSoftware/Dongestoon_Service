using Domain.Entities;
using FluentAssertions;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Infrastructure.Repositories;

public class ProductRepositoryTests
{
    private readonly IProductRepository _sut;
    private ApplicationDbContext _dbContext;


    public ProductRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>();
        dbContextOptions.UseInMemoryDatabase($"testDb");


        _dbContext = new ApplicationDbContext(dbContextOptions.Options);
        _sut = new ProductRepository(_dbContext);
    }

    [Fact]
    public void FindProductById_ShouldReturnProductWithGivenId_WhenEver()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product
        {
            Id = productId,
            Name = "some pr name",
            Price = 1
        };
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();

        // Act
        var actual = _sut.FindProductById(productId);
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();

        // Assert
        actual.Should().BeEquivalentTo(product);
    }

    [Fact]
    public void FindProductByName_ShouldRRreturnProductsWithSimilarName()
    {
        // Arrange

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "some pr name",
            Price = 1
        };
        var product2 = new Product
        {
            Id = Guid.NewGuid(),
            Name = "some pr name 2",
            Price = 1
        };
        _dbContext.Products.AddRange(product, product2);
        _dbContext.SaveChanges();

        var expected = new List<Product>
        {
            product,
            product2
        };

        // Act
        var actual = _sut.FindProductByName("some pr");
        _dbContext.Products.RemoveRange(product, product2);
        _dbContext.SaveChanges();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}