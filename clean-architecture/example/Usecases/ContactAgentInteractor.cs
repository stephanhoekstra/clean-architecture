using example.Entities;
using example.Gateways;

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
    public class ContactAgentInteractor
    {
        private readonly IRepository<HouseViewing> _repository = new InMemoryHouseViewingRepository();
        private readonly EmailService _emailService = new EmailService();

        public ContactAgentResponseMessage Handle(ContactAgentRequestMessage requestMessage)
        {
            if (Validate(requestMessage) == false)
                return new ContactAgentResponseMessage
                {
                    Status = ContactAgentResponseMessage.ResponseStatus.ValidationFailed
                };

            var viewing = new HouseViewing
            {
                CustomerEmailAddress = requestMessage.CustomerEmailAddress,
                CustomerPhoneNumber = requestMessage.CustomerPhoneNumber,
                DateTime = null //determined later
            };

            _repository.Save(viewing);//specify that the entity should be saved, but we don't care how.

            _emailService.SendEmail(viewing);

            return new ContactAgentResponseMessage();
        }

        private bool Validate(ContactAgentRequestMessage requestMessage)
        {
            if (string.IsNullOrEmpty(requestMessage.CustomerEmailAddress)) return false;
            if (string.IsNullOrEmpty(requestMessage.HouseAddress)) return false;
            return true;
        }
    }

    public class ContactAgentRequestMessage
    {
        public string CustomerEmailAddress { get; set; }
        public int CustomerPhoneNumber { get; set; }
        public string HouseAddress { get; set; } //the house in which the customer is potentially interested
    }

    public class ContactAgentResponseMessage
    {
        public ResponseStatus Status { get; set; }

        public enum ResponseStatus
        {
            Succes = 0,
            ValidationFailed = 1
        }
    }
}
