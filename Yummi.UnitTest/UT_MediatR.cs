using Moq;
using Xunit;
using Yummi.Application.CQRS.Recipe.Qry;
using Yummi.Application.CQRS.Recipe.Qry.Hndlr;
using Yummi.Core.Entities;
using Yummi.Core.Interfaces;

namespace Yummi.UnitTest
{
    public class UT_MediatR
    {
        [Fact]
        public async Task TstMediatR_Hndlr_and_Qry()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRecipeRepository = new Mock<IRecipeRepository>();

            // Setup your mock data here
            var recipe = new Recipe { /* your data */ };

            mockRecipeRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(recipe);
            mockUnitOfWork.Setup(u => u.RecipeRepository).Returns(mockRecipeRepository.Object);

            var handler = new GetByIdRecipeQryHndlr(mockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(new GetByIdRecipeQry { Id = 1 }, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            Assert.Equal(recipe, result.Payload);
        }
    }
}
