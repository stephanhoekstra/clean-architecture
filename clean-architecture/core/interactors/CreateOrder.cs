using System;
using core.domain;
using FluentValidation;
using MediatR;

namespace core.interactors
{
    namespace core.interactors
    {
        public class CreateOrderRequest : IRequest<CreateOrderResponse>
        {
            public Guid CustomerId { get; set; }
            public Address ShippingAddress { get; set; }
            public Guid ProductId { get; set; }
            public int ProductQuantity { get; set; }
        }

        public class CreateOrderInteractor : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
        {
            private readonly IValidator<CreateOrderRequest> _validator;
            private readonly Inventory _inventory;
            private readonly IGateWay<Order> _orderGateway;

            public CreateOrderInteractor(IValidator<CreateOrderRequest> validator, Inventory inventory, IGateWay<Order> orderGateway)
            {
                _validator = validator;
                _inventory = inventory;
                _orderGateway = orderGateway;
            }

            public CreateOrderResponse Handle(CreateOrderRequest message)
            {
                var validation = _validator.Validate(message);
                if (validation.IsValid == false) { throw  new ValidationException(validation.Errors);}

                _inventory.Reserve(message.ProductQuantity, message.ProductId);
                var order = new Order();

                _orderGateway.Save(order);

                return new CreateOrderResponse {OrderId = order.Id};
            }
        }

        public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
        {
            CreateOrderRequestValidator()
            {
                RuleFor(r => r.ProductId).NotEmpty();
                RuleFor(r => r.ProductQuantity).GreaterThanOrEqualTo(1);
                RuleFor(r => r.ShippingAddress).NotEmpty();
                RuleFor(r => r.ShippingAddress).SetValidator(new AddressValidator());
            }
        }

        public class CreateOrderResponse
        {
            public Guid OrderId { get; set; }
        }

    }
}

namespace core.interactors.core.interactors
{
    public class Inventory
    {
        public void Reserve(int productQuantity, Guid productId)
        {
        }
    }
}
