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
                new Recipe { Id = 3, Name = "TEST" },
                new Recipe { Id = 5, Name = "arroz chino" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Recipe>>();
            mockDbSet.As<IAsyncEnumerable<Recipe>>()
                .Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new TestAsyncEnumerator<Recipe>(mockRecipeList.GetEnumerator()));

            mockDbSet.As<IQueryable<Recipe>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Recipe>(mockRecipeList.Provider));

            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(mockRecipeList.Expression);
            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(mockRecipeList.ElementType);
            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(() => mockRecipeList.GetEnumerator());

            var mockContext = new Mock<YummiDbContext>();
            mockContext.Setup(c => c.Set<Recipe>()).Returns(mockDbSet.Object);

            var repository = new GenericRepository<Recipe>(mockContext.Object);

            // Act
            var recipes =  await repository.GetAllAsync();

            // Assert
            Assert.NotNull(recipes);
            Assert.Equal(3, recipes.Count());           
            Assert.Equal("arroz chino", recipes.ElementAt(0).Name);
        }
       

        // Other tests...

    }
}
