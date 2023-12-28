using MediatR;
using Yummi.Application.Models;
using Yummi.Core.Interfaces;

namespace Yummi.Application.CQRS.Recipe.Qry.Hndlr
{
    public class GetAllRecipeQryHndlr : IRequestHandler<GetAllRecipeQry,
        OperationResponse<IEnumerable<Core.Entities.Recipe>>>
    {
        private readonly IUnitOfWork _uOw;
        public GetAllRecipeQryHndlr(IUnitOfWork uOw)
        {
            _uOw = uOw;
        }

        public async Task<OperationResponse<IEnumerable<Core.Entities.Recipe>>> Handle(GetAllRecipeQry request, 
            CancellationToken cancellationToken)
        {
            var result = new OperationResponse<IEnumerable<Core.Entities.Recipe>>();
            try
            {
                result.Payload =  await _uOw.RecipeRepository.GetAllAsync();               
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
