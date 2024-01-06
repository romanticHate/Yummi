using AutoMapper;
using Xunit;
using Yummi.Application.Mappers;
using Yummi.Core.Entities;

namespace Yummi.UnitTest
{
    public class UT_AutoMapper
    {
        private IMapper _mapper;       
        public UT_AutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RecipeMap>(); 
            });

            _mapper = config.CreateMapper();
        }
        [Fact]
        public void TstAutoMaper()
        {
            // Arrange
            var source = new Recipe
            {                
                Id = 1,
                Name = "Test Recipe"               
            }; 

            // Act
            var destination = _mapper.Map<Recipe>(source);

            // Assert           
            Assert.Equal(source.Name, destination.Name);
        }
    }
}
