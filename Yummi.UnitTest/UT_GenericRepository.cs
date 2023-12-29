using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;
using Yummi.Core.Entities;
using Yummi.Persistance.DataContext;
using Yummi.Persistance.Repositories;
using Yummi.UnitTest;

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
            var entity = new Recipe { Id = id }; // Replace 'Recipe' with your actual entity class

            var entities = new List<Recipe>
            {
                new Recipe { Id = 1, Name = "spaghetti bolognese" },
                new Recipe { Id = 3, Name = "TEST" },
                new Recipe { Id = 5, Name = "arroz chino" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Recipe>>();           

            mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(() => entities.GetEnumerator());

            var mockContext = new Mock<YummiDbContext>();
            mockContext.Setup(c => c.Set<Recipe>()).Returns(mockSet.Object);

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
            var entity = new Recipe { Id = 5, Name = "arroz chino" }; // Replace 'YourEntity' with your actual entity class

            var mockSet = new Mock<DbSet<Recipe>>();
            mockSet.Setup(m => m.Remove(It.IsAny<Recipe>()));

            var mockContext = new Mock<YummiDbContext>();
            mockContext.Setup(c => c.Set<Recipe>()).Returns(mockSet.Object);

            var repository = new Mock<GenericRepository<Recipe>>(mockContext.Object);
            repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);

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
            var recipeName = "Test Recipe";
            var recipe = new Recipe { Id = 1, Name = recipeName };
            var ingredient = new Ingredient { Id = 1, Name = "Test Ingredient" };
            var measure = new Measure { Id = 1 };
            var recipeIngredient = new RecipeIngredient { RecipeId = 1, IngredientId = 1, MeasureId = 1, Amount = 1 };

            var recipes = new List<Recipe> { recipe }.AsQueryable();
            var ingredients = new List<Ingredient> { ingredient }.AsQueryable();
            var measures = new List<Measure> { measure }.AsQueryable();
            var recipeIngredients = new List<RecipeIngredient> { recipeIngredient }.AsQueryable();

            var mockSetRecipes = new Mock<DbSet<Recipe>>();
            mockSetRecipes.ReturnsDbSet(recipes);

            var mockSetIngredients = new Mock<DbSet<Ingredient>>();
            mockSetIngredients.ReturnsDbSet(ingredients);

            var mockSetMeasures = new Mock<DbSet<Measure>>();
            mockSetMeasures.ReturnsDbSet(measures);

            var mockSetRecipeIngredients = new Mock<DbSet<RecipeIngredient>>();
            mockSetRecipeIngredients.ReturnsDbSet(recipeIngredients);

            var mockContext = new Mock<YummiDbContext>();
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
            Assert.Equal("Test Ingredient", result[0].Name);
            Assert.Equal(1, result[0].Amount);
        }
        #endregion

        #region Measure UT

        // Other tests...
        #endregion

        // Other tests...
    }
}
