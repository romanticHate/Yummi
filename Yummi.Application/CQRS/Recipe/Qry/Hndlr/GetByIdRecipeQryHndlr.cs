using MediatR;
using Yummi.Application.Models;
using Yummi.Core.Interfaces;

namespace Yummi.Application.CQRS.Recipe.Qry.Hndlr
{
    public class GetByIdRecipeQryHndlr : IRequestHandler<GetByIdRecipeQry,
        OperationResponse<Core.Entities.Recipe>>
    {
        private readonly IUnitOfWork _uOw;
        public GetByIdRecipeQryHndlr(IUnitOfWork uOw)
        {
            _uOw = uOw;
        }
        public async Task<OperationResponse<Core.Entities.Recipe>> Handle(GetByIdRecipeQry request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResponse<Core.Entities.Recipe>();
            try
            {
                result.Payload = await _uOw.RecipeRepository.GetByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Errors.Add(new Error { Message = ex.Message });
            }
            return result;  
        }
    }
}
