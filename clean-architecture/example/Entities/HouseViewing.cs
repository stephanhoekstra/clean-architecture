using System;

namespace example.Entities
{
    /// <summary>
    /// A house viewing (scheduled) appointment with the real estate agent 
    /// for the customer to visit the house (in the real world)
    /// </summary>
    public class HouseViewing
    {
        public int? Id { get; set; }
        public string HouseAddress { get; set; }
        public int CustomerPhoneNumber { get; set; }
        public string CustomerEmailAddress { get; set; }
        public DateTime? DateTime { get; set; }

    }
}
