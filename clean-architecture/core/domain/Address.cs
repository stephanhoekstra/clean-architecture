using FluentValidation;

namespace core.domain
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a.Street).NotEmpty();
            RuleFor(r => r.HouseNumber).NotEmpty();
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public int HouseNumber { get; set; }
    }
}