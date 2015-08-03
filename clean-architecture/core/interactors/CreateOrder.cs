using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.domain;
using FluentValidation;
using MediatR;

namespace core.interactors
{
    public class CreateOrderInteractor : IRequestHandler<CreateOrderInteractor.Request, CreateOrderInteractor.Response>
    {
        private readonly Inventory _inventory;
        private readonly IOrderGateWay _gateWay;

        public CreateOrderInteractor(Inventory inventory, IOrderGateWay gateWay)
        {
            _inventory = inventory;
            _gateWay = gateWay;
        }

        public Response Handle(Request message)
        {
            var order = new Order();

            Product product = new Product();
            if(_inventory.CountAvailable(p=> p == product) < message.Quantity)
                return new Response {Status = Response.Statuses.ProductNotInStock};

            _inventory.Reserve(order);
            
            _gateWay.Save(order);

            return new Response
            {
                OrderId = order.Id,
                Status = Response.Statuses.Success
            };
        }
        

        public class Request : IRequest<Response>
        {
            public class Validator : AbstractValidator<Request>
            {
                public Validator()
                {
                    RuleFor(r => r.ProductId).NotEmpty();
                    RuleFor(r => r.Quantity).GreaterThanOrEqualTo(1);
                }
            }

            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public class Response
        {
            public enum Statuses
            {
                Success = 0,
                ProductNotInStock = 1,
            }
            public Statuses Status { get; set; }
            public Guid? OrderId { get; set; }
        }
    }

    internal class Inventory
    {
        private List<InventoryLine> _lines;

        public Inventory(List<InventoryLine> lines)
        {
            _lines = lines;
        }

        public int CountAvailable(Func<Product, bool> func)
        {
            return _lines.Count(line => func(line.Product) && line.Availability == AvailabilityStatus.Available);
        }

        public void Reserve(Product product, int quantity)
        {
            _lines.Where(line => line.Product == product && line.Availability == AvailabilityStatus.Available).Reserve();
        }
    }

    internal class Product
    {
    }

    internal class InventoryLine
    {
        public void Reserve()
        {
            if (Availability == AvailabilityStatus.Reserved)
                throw new InvalidOperationException("Cannot reserve item that is already reserved");

            Availability = AvailabilityStatus.Reserved;
        }
        public Product Product { get; set; }
        public AvailabilityStatus Availability { get; set; }
    }

    internal enum AvailabilityStatus
    {
        Available = 0,
        Reserved = 1
    }
}
