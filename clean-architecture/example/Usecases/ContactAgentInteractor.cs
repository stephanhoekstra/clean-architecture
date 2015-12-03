using System;
using example.Entities;
using example.Gateways;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace example.Usecases
{
    
    public class ContactAgentInteractor : IRequestHandler<ContactAgentRequestMessage,ContactAgentResponseMessage>
    {
        private readonly IRepository<int, House> _repository;
        private readonly IValidator<ContactAgentRequestMessage> _validator;

        public ContactAgentInteractor(IValidator<ContactAgentRequestMessage> validator, IRepository<int, House> repository)
        {
            _validator = validator;
            _repository = repository;
        }

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
