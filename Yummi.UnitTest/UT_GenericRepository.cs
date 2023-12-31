using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Yummi.Application.CQRS.Recipe.Qry;
using Yummi.Application.Models;
using Yummi.Core.DTOs;
using Yummi.Core.Entities;
using Yummi.Core.Interfaces;
using Yummi.Persistance.DataContext;
using Yummi.Persistance.Repositories;
using Yummi.UnitTest.Mock;
using Yummi.WebAPI.Controllers;

namespace Yummi.UnitTest
{
    public class UT_GenericRepository
    {
        #region Recipe(s) UT

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
                .Returns(new MockAsyncEnumerator<Recipe>(mockRecipeList.GetEnumerator()));

            mockDbSet.As<IQueryable<Recipe>>()
                .Setup(m => m.Provider)
                .Returns(new MockAsyncQueryProvider<Recipe>(mockRecipeList.Provider));

            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(mockRecipeList.Expression);
            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(mockRecipeList.ElementType);
            mockDbSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(() => mockRecipeList.GetEnumerator());

            var mockContext = new Mock<YummiDbContext>();
            mockContext.Setup(c => c.Set<Recipe>()).Returns(mockDbSet.Object);

            var repository = new GenericRepository<Recipe>(mockContext.Object);

            // Act
            var result =  await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());           
            Assert.Equal("spaghetti bolognese", result.ElementAt(1).Name);
            Assert.Equal("TEST", result.ElementAt(2).Name);
            Assert.Equal("arroz chino", result.ElementAt(0).Name);
        }
        [Fact]
        public async void From_Recipe_GetByIdRecipe()
        {         
            // Arrange
            var id = 1;
            var recipeName = "spaghetti bolognese";
            var recipe = new Recipe { Id = id, Name = recipeName };
            var recipes = new List<Recipe> { recipe }.AsQueryable();
            var mockRecipeList = Enumerable.Empty<Recipe>().AsQueryable();           

            var mockDbSet = new Mock<DbSet<Recipe>>();

            mockDbSet.Setup(d => d.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(recipe);

            var mockContext = new Mock<YummiDbContext>();
            mockContext.Setup(c => c.Set<Recipe>()).Returns(mockDbSet.Object);

            var repository = new GenericRepository<Recipe>(mockContext.Object);

            // Act
            var result = await repository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }
        [Fact]
        public async Task From_Recipe_AddRecipe()
        {
            // Arrange
            var entity = new Recipe { 
                Name="This is a test",
                Description="N/A",
                Instructions= "Bla bla bla Bla bla bla Bla bla bla Bla bla bla",
                Cuisine="ABC",
                CookTime=1,
                Course="N/A",
                PrepTime=1               
             }; 

            var mockSet = new Mock<DbSet<Recipe>>();
            var mockContext = new Mock<YummiDbContext>();
            mockContext.Setup(c => c.Set<Recipe>()).Returns(mockSet.Object);

            var repository = new GenericRepository<Recipe>(mockContext.Object);

            // Act
            await repository.AddAsync(entity);

            // Assert
            mockSet.Verify(m => m.AddAsync(It.IsAny<Recipe>(), It.IsAny<CancellationToken>()), Times.Once());
        }
        [Fact]
        public async Task From_Recipe_DeleteAsync()
        {
            // Arrange
            var id = 5;
            var name = "arroz chino";
            var entity = new Recipe { Id = id, Name = name }; // Replace 'YourEntity' with your actual entity class

            var mockSet = new Mock<DbSet<Recipe>>();
            mockSet.Setup(m => m.Remove(It.IsAny<Recipe>()));

            var mockContext = new Mock<YummiDbContext>();
            mockContext.Setup(c => c.Set<Recipe>()).Returns(mockSet.Object);

            var repository = new Mock<GenericRepository<Recipe>>(mockContext.Object);           

            // Act
            await repository.Object.DeleteAsync(id);

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Recipe>()), Times.Once());
        }
        [Fact]
        public async Task From_Recipe_UpdateRecipe() { }
        #endregion        

        #region Ingredient(s) UT

        // Other tests...
        #endregion

        #region RecipeIngredient(s) UT

        [Fact]
        public async Task From_RecipeIngredient_GetAllRecipeIngredients()
        {
           // Arrange
            var recipeName = "spaghetti bolognese";
            var recipe = new Recipe { Id = 1, Name = recipeName };
            var ingredient = new Ingredient { Id = 1, Name = "Garlic" };
            var measure = new Measure { Id = 2, Name = "Grm" };
            var recipeIngredient = new RecipeIngredient { RecipeId = 1,
                IngredientId = 1,
                MeasureId = 2,
                Amount = 3 };

            var recipes = new List<Recipe> { recipe }.AsQueryable();
            var ingredients = new List<Ingredient> { ingredient }.AsQueryable();
            var measures = new List<Measure> { measure }.AsQueryable();
            var recipeIngredients = new List<RecipeIngredient> { recipeIngredient }.AsQueryable();

            var mockContext = new Mock<YummiDbContext>();

            var mockSetRecipes = new Mock<DbSet<Recipe>>();
            mockSetRecipes.ReturnsDbSet(recipes);
            mockSetRecipes.As<IAsyncEnumerable<Recipe>>()
               .Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
               .Returns(new MockAsyncEnumerator<Recipe>(recipes.GetEnumerator()));

            mockSetRecipes.As<IQueryable<Recipe>>()
                .Setup(m => m.Provider)
                .Returns(new MockAsyncQueryProvider<Recipe>(recipes.Provider));

            var mockSetIngredients = new Mock<DbSet<Ingredient>>();
            mockSetIngredients.ReturnsDbSet(ingredients);
            mockSetIngredients.As<IAsyncEnumerable<Ingredient>>()
                .Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new MockAsyncEnumerator<Ingredient>(ingredients.GetEnumerator()));

            mockSetIngredients.As<IQueryable<Ingredient>>()
                .Setup(m => m.Provider)
                .Returns(new MockAsyncQueryProvider<Ingredient>(ingredients.Provider));


            var mockSetMeasures = new Mock<DbSet<Measure>>();
            mockSetMeasures.ReturnsDbSet(measures);
            mockSetMeasures.As<IAsyncEnumerable<Measure>>()
               .Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
               .Returns(new MockAsyncEnumerator<Measure>(measures.GetEnumerator()));

            mockSetMeasures.As<IQueryable<Measure>>()
                .Setup(m => m.Provider)
                .Returns(new MockAsyncQueryProvider<Measure>(measures.Provider));

            var mockSetRecipeIngredients = new Mock<DbSet<RecipeIngredient>>();
            mockSetRecipeIngredients.ReturnsDbSet(recipeIngredients);
            mockSetRecipeIngredients.As<IAsyncEnumerable<RecipeIngredient>>()
                .Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new MockAsyncEnumerator<RecipeIngredient>(recipeIngredients.GetEnumerator()));

            mockSetRecipeIngredients.As<IQueryable<RecipeIngredient>>()
                .Setup(m => m.Provider)
                .Returns(new MockAsyncQueryProvider<RecipeIngredient>(recipeIngredients.Provider));


            mockContext.Setup(c => c.Set<Recipe>()).Returns(mockSetRecipes.Object);
            mockContext.Setup(c => c.Set<Ingredient>()).Returns(mockSetIngredients.Object);
            mockContext.Setup(c => c.Set<Measure>()).Returns(mockSetMeasures.Object);
            mockContext.Setup(c => c.Set<RecipeIngredient>()).Returns(mockSetRecipeIngredients.Object);

            var repository = new RecipeIngredientRepository(mockContext.Object);

            // Act
            var result = await repository.GetAllRecipeIngredients(recipeName);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);            
            Assert.Equal("Garlic", result[0].Name);         
           
        }
        #endregion

        #region Measure UT

        // Other tests...
        #endregion

        // Other tests...
    }
}
