using MediatR;
using Yummi.Application.Models;
using Yummi.Core.Interfaces;

namespace Yummi.Application.CQRS.Recipe.Cmmnd.hndlr
{
    public class DeleteRecipeCmmndHndlr : IRequestHandler<DeleteRecipeCmmnd,
        OperationResponse<Core.Entities.Recipe>>
    {
        private readonly IUnitOfWork _uOw;
        public DeleteRecipeCmmndHndlr(IUnitOfWork uOw)
        {
            _uOw = uOw;
        }
        public async Task<OperationResponse<Core.Entities.Recipe>> Handle(DeleteRecipeCmmnd request, CancellationToken cancellationToken)
        {
            var reponse = new OperationResponse<Core.Entities.Recipe>();
            try
            {
                await _uOw.RecipeRepository.DeleteAsync(request.Id);
                await _uOw.SaveAsync();
            }
            catch (Exception ex)
            {
                reponse.IsError = true;
                reponse.Errors.Add(new Error { Message = ex.Message });
            }
            return reponse;
        }
    }
}
