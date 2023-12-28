using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Yummi.Core.Entities;
using Yummi.Persistance.DataContext;
using Yummi.Persistance.Repositories;

namespace Yummi.UnitTest
{
    public class UT_GenericRepository
    {
        [Fact]
        public async void From_Recipe_GetAllRecipes()
        {
            // Arrange

            var mockRecipeList = new List<Recipe>
            {
                new Recipe { Id = 1, Name = "spaghetti bolognese" },
                new Recipe { Id = 3, Name = "TEST" }
            };

            var mockDbSet = new Mock<DbSet<Recipe>>();
            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(mockRecipeList.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(mockRecipeList.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(mockRecipeList.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(() => mockRecipeList.GetEnumerator());

            var mockContext = new Mock<YummiDbContext>();
            mockContext.Setup(m => m.Set<Recipe>()).Returns(mockDbSet.Object);

            var repository = new GenericRepository<Recipe>(mockContext.Object);

            // Act
            var recipes =  await repository.GetAllAsync();

            // Assert
            Assert.NotNull(recipes);
            Assert.Equal(2, recipes.Count());           
            Assert.Equal("TEST", recipes.ElementAt(1).Name);
        }
       

        // Other tests...

    }
}
