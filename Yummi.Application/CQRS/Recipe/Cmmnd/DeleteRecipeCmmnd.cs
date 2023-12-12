using MediatR;
using Yummi.Application.Models;

namespace Yummi.Application.CQRS.Recipe.Cmmnd
{
    public class DeleteRecipeCmmnd:IRequest<OperationResponse<Core.Entities.Recipe>>
    {
        public int Id { get; set; }
    }
}
