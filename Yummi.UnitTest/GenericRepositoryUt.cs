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
        public async void GetAllRecipes_ReturnsAllRecipes()
        {
            // Arrange
            var mockContext = new Mock<YummiDbContext>();
            var mockDbSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<Recipe>>();
            mockContext.Setup(m => m.Set<Recipe>()).Returns(mockDbSet.Object);

            var repository = new GenericRepository<Recipe>(mockContext.Object);

            // Act
            var recipes = await repository.GetAllAsync();

            // Assert
            mockDbSet.Verify(m => m.FirstOrDefault(), Times.Once());
        }

        // Other tests...
    }
}
