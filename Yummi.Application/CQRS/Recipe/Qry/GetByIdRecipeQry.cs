using MediatR;
using Yummi.Application.Models;

namespace Yummi.Application.CQRS.Recipe.Qry
{
    public class GetByIdRecipeQry:IRequest<OperationResponse<Core.Entities.Recipe>>
    {
        public int Id { get; set; }
    }
}
