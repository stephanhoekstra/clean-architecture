using System;
using example.Entities;
using example.Gateways;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace example.Usecases
{
    /// <summary>
    /// Use case: Contact real estate agent
    /// 
    /// As a consumer I want to contact the real estate agent 
    /// because I am interested in a specific house
    /// and I would like to be contacted for a follow-up meeting so I can
    /// visit the house in real life, and make a more informed decision 
    /// with regards to potentially buying it.
    /// 
    /// Data:
    /// - Customer email address(required, must be valid)
    /// - Customer phone number(required)
    /// - HouseId - every house on funda has an Id.
    /// 
    /// Primary Course
    /// - The customer issues a "Contact Real Estate Agent" command with above data
    /// - System validates all data
    /// - System saves the contact information for reporting purposes
    /// - System sends an email with customer data to real estate agent 
    ///
    /// Exception Course: Validation Error
    /// - System delivers the error to customer
    /// </summary>
    public class ContactAgentInteractor : IRequestHandler<ContactAgentRequestMessage,ContactAgentResponseMessage>
    {
        private readonly IRepository<int, House> _repository = new InMemoryHouseRepository();
        private readonly IValidator<ContactAgentRequestMessage> _validator = new ContactAgentRequestMessageValidator();

        public ContactAgentResponseMessage Handle(ContactAgentRequestMessage request)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.IsValid == false)
                return new ContactAgentResponseMessage(validationResult);

            var house = _repository.Get(request.HouseId);
            house.RegisterInterest(new Interest
            {
                CustomerEmailAddress = request.CustomerEmailAddress,
                CustomerPhoneNumber = request.CustomerPhoneNumber,
                CreationDate = DateTime.Now
            });

            _repository.Save(house);

            return new ContactAgentResponseMessage(validationResult, request.HouseId);
        }
    }

    public class ContactAgentRequestMessageValidator : AbstractValidator<ContactAgentRequestMessage>
    {
        public ContactAgentRequestMessageValidator()
        {
            RuleFor(r => r.CustomerEmailAddress).NotEmpty();
            RuleFor(r => r.CustomerEmailAddress).EmailAddress().WithMessage("Not a valid email address");
        }
    } 

    public class ContactAgentRequestMessage : IRequest<ContactAgentResponseMessage>
    {
        public string CustomerEmailAddress { get; set; }
        public long CustomerPhoneNumber { get; set; }
        public int HouseId { get; set; } //the house in which the customer is potentially interested
    }

    public class ContactAgentResponseMessage
    {
        public ValidationResult ValidationResult { get; }
        public long? HouseId { get; private set; } //the response object needs this information, so the user knows which object we're talking about;

        public ContactAgentResponseMessage(ValidationResult validationResult, int? houseId =null)
        {
            HouseId = houseId;
            ValidationResult = validationResult;
        }
    }

}
