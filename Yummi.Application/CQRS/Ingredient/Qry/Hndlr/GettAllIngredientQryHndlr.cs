using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Yummi.Application.Models;
using Yummi.Core.Interfaces;

namespace Yummi.Application.CQRS.Ingredient.Qry.Hndlr
{
    public class GettAllIngredientQryHndlr : IRequestHandler<GetAllIngredientQry,
        OperationResponse<IEnumerable<Core.Entities.Ingredient>>>
    {
        private readonly IUnitOfWork _uOw;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
      
        public GettAllIngredientQryHndlr(IUnitOfWork uOw)
        {
            _uOw = uOw;
        }

        public async Task<OperationResponse<IEnumerable<Core.Entities.Ingredient>>> Handle(GetAllIngredientQry request,
            CancellationToken cancellationToken)
        {
            var response = new OperationResponse<IEnumerable<Core.Entities.Ingredient>>();
            try
            {
                response.Payload = await _uOw.IngredientRepository.GetAllAsync();
            }
            catch (Exception  ex)
            {
                response.IsError = true;
                response.Errors.Add(new Error { Message = ex.Message });
            }
            return response;
        }
    }
}
