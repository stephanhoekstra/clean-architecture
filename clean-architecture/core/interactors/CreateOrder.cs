using System;
using System.Collections.Generic;
using core.domain;
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

        public class CreateOrderInteractor : ICreateOrderInteractor 
        {
            private readonly ICreateOrderRequestValidator _validator;
            private readonly Inventory _inventory;
            private readonly IOrderGateWay _orderGateway;

            public CreateOrderInteractor(Inventory inventory, IOrderGateWay orderGateway, ICreateOrderRequestValidator validator)
            {
                _inventory = inventory;
                _orderGateway = orderGateway;
                _validator = validator;
            }

            public CreateOrderResponse Handle(CreateOrderRequest message)
            {
                var validation = _validator.Validate(message);
                if (validation.IsValid == false) { throw  new ValidationException();}

                _inventory.Reserve(message.ProductQuantity, message.ProductId);
                var order = new Order();

                _orderGateway.Save(order);

                return new CreateOrderResponse {OrderId = order.Id};
            }
        }

        public class CreateOrderRequestValidator : ICreateOrderRequestValidator
        {
            public ValidationResult Validate(CreateOrderRequest message)
            {
                return new ValidationResult();
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


    public interface ICreateOrderInteractor : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
    }

    public interface ICreateOrderRequestValidator
    {
        ValidationResult Validate(CreateOrderRequest message);
    }

    public class ValidationResult
    {
        public List<string> ValidationErrors{ get; set; }
        public bool IsValid => ValidationErrors != null && ValidationErrors.Count == 0;
    }

    public class ValidationException : Exception
    {
        
    }
}
