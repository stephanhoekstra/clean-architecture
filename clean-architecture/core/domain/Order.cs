using System;

namespace core.domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
