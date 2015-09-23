namespace core.domain
{
    public class AddressValidator : IAddressValidator
    {
        
    }

    public interface IAddressValidator
    {
    }

    public class Address
    {
        public string Street { get; set; }
        public int HouseNumber { get; set; }
    }
}