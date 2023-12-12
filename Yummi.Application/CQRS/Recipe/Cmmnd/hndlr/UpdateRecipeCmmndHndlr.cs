using MediatR;
using Yummi.Application.Models;
using Yummi.Core.Interfaces;

namespace Yummi.Application.CQRS.Recipe.Cmmnd.hndlr
{
    public class UpdateRecipeCmmndHndlr : IRequestHandler<UpdateRecipeCmmnd,
        OperationResponse<Core.Entities.Recipe>>
    {
        private readonly IUnitOfWork _uOw;
        public UpdateRecipeCmmndHndlr(IUnitOfWork uOw)
        {
            _uOw = uOw;        }
        public async Task<OperationResponse<Core.Entities.Recipe>> Handle(UpdateRecipeCmmnd request, CancellationToken cancellationToken)
        {
            var response = new OperationResponse<Core.Entities.Recipe>();
            try
            {
                _uOw.RecipeRepository.Update(new Core.Entities.Recipe
                {
                    Name = request.Name,
                    CookTime = request.CookTime,
                    Course = request.Course,
                    Cuisine = request.Cuisine,
                    Description = request.Description,
                    Instructions = request.Instructions,
                    PrepTime = request.PrepTime,
                });
                await _uOw.SaveAsync();
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Errors.Add(new Error { Message = ex.Message });
            }
            return response;
        }
    }
}
