using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace core.interactors
{
    public class CreateOrderInteractor : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        public CreateOrderResponse Handle(CreateOrderRequest message)
        {
            return new CreateOrderResponse();
        }
    }

    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(r => r.ProductId).NotEmpty();
            RuleFor(r => r.UnitCount).GreaterThanOrEqualTo(1);
        }
    }

    public class CreateOrderRequest : IRequest<CreateOrderResponse>
    {
        public Guid ProductId { get; set; }
        public int UnitCount { get; set; }
    }

    public class CreateOrderResponse
    {
        Guid OrderId { get; set; }
    }
}
