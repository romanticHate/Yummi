using Moq;
using Xunit;
using Yummi.Core.Entities;
using Yummi.Persistance.DataContext;
using Yummi.Persistance.Repositories;

namespace Yummi.UnitTest
{
    public class GenericRepositoryUt
    {
        [Fact]
        public async void Get_All_Recipes()
        {
            // Arrange
            var mockContext = new Mock<YummiDbContext>();
            var mockDbSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<Recipe>>();
            mockContext.Setup(m => m.Set<Recipe>()).Returns(mockDbSet.Object);

            var repository = new GenericRepository<Recipe>(mockContext.Object);

            // Act
            var recipes = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(recipes);
            Assert.Equal(3, recipes.Count());
        }       

        //[Fact]
        //public async void Returns_GetAllRecipes()
        //{
        //    // Arrange
        //    var listReceipeResponse = new List<Recipe>
        // {
        //     new() { Id = 1,Name="Demo"}
        // };

        //    var mockContext = new Mock<YummiDbContext>();
        //    var mockDbSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<Recipe>>();
        //    mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(listReceipeResponse.AsQueryable().Provider);
        //    mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(listReceipeResponse.AsQueryable().Expression);
        //    mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(listReceipeResponse.AsQueryable().ElementType);
        //    mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(listReceipeResponse.GetEnumerator());

        //    mockContext.Setup(m => m.Set<Recipe>()).Returns(mockDbSet.Object);
        //    var repository = new GenericRepository<Recipe>(mockContext.Object);

        //    // Act
        //    var recipes = await repository.GetAllAsync();

        //    // Assert
        //    Assert.NotNull(recipes);
        //    Assert.Equal(listReceipeResponse.Count, recipes.Count());
        //}

        // Other tests...

    }
}
