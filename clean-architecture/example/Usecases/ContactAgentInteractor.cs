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
    /// Data:
    /// - Customer email address(required, must be valid)
    /// - Customer phone number(required)
    /// - House Address
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
        private readonly IRepository<HouseViewing> _repository = new InMemoryHouseViewingRepository();
        private readonly EmailService _emailService = new EmailService();
        private readonly IValidator<ContactAgentRequestMessage> _validator = new ContactAgentRequestMessageValidator();

        public ContactAgentResponseMessage Handle(ContactAgentRequestMessage requestMessage)
        {
            var validationResult = _validator.Validate(requestMessage);
            if (validationResult.IsValid == false)
                return new ContactAgentResponseMessage(validationResult);

            return new ContactAgentResponseMessage(validationResult, requestMessage.ObjectId);
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
        public int ObjectId { get; set; } //the house in which the customer is potentially interested
    }

    public class ContactAgentResponseMessage
    {
        public ValidationResult ValidationResult { get; }
        public long? ObjectId { get; private set; } //the response object needs this information, so the user knows which object we're talking about;

        public ContactAgentResponseMessage(ValidationResult validationResult, int? objectId =null)
        {
            ObjectId = objectId;
            ValidationResult = validationResult;
        }
    }

}
