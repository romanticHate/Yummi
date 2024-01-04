using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Yummi.Application.CQRS.Recipe.Qry;
using Yummi.Application.Models;
using Yummi.Core.DTOs;
using Yummi.Core.Entities;
using Yummi.Core.Interfaces;
using Yummi.WebAPI.Controllers;

namespace Yummi.UnitTest
{
    public class UT_RecipeController
    {
        #region RecipeController UT

        [Fact]
        public async Task From_RecipeController_GetAll_Returns_AllRecipes()
        {
            // Arrange           
            var recipeDto = new RecipeDto { /* set properties here */ };
            var recipeDtos = new List<RecipeDto> { recipeDto };

            var response = new OperationResponse<IEnumerable<Recipe>>();

            var mockLogger = new Mock<ILogger<RecipeController>>(); // mock Logger           
            var mockUoW = new Mock<IUnitOfWork>(); // mock UoW           
            var mockMapper = new Mock<IMapper>(); // mock Automap
            mockMapper.Setup(m => m.Map<List<RecipeDto>>(It.IsAny<object>()))
                .Returns(recipeDtos);
            var mockMediator = new Mock<IMediator>(); // mock MediatR
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllRecipeQry>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var controller = new RecipeController(mockLogger.Object,
                mockUoW.Object,
                mockMapper.Object,
                mockMediator.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<RecipeDto>>(okResult.Value);

            mockMediator.Verify(m => m.Send(It.IsAny<GetAllRecipeQry>(),
                It.IsAny<CancellationToken>()), Times.Once());
        }
        [Fact]
        public async Task From_RecipeController_GetByIdAsync_Returns_Recipe()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<RecipeController>>(); // mock Logger           
            var mockUoW = new Mock<IUnitOfWork>(); // mock UoW 
            var mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();

            // Setup your mock data here
            var dataResponse = new OperationResponse<Recipe> { /* your data */ };
            var dataRecipeDto = new RecipeDto { Name = "Test" };


            mockMediator.Setup(m => m.Send(It.IsAny<GetByIdRecipeQry>(), It.IsAny<CancellationToken>())).ReturnsAsync(dataResponse);
            mockMapper.Setup(m => m.Map<RecipeDto>(It.IsAny<Recipe>())).Returns(dataRecipeDto);

            var controller = new RecipeController(mockLogger.Object,
                mockUoW.Object,
                mockMapper.Object,
                mockMediator.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            //var returnValue = Assert.IsType<List<RecipeDto>>(okResult.Value);
            var returnValue = okResult.Value as RecipeDto;
            Assert.NotNull(returnValue);

            Assert.Equal("Test", returnValue.Name);

            mockMediator.Verify(m => m.Send(It.IsAny<GetByIdRecipeQry>(),
                It.IsAny<CancellationToken>()), Times.Once());
        }
        #endregion
    }
}
