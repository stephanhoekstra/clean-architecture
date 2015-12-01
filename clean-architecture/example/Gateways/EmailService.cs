using example.Entities;

namespace example.Gateways
{
    /// <summary>
    /// this should be refactored because it defies the dependency rule
    /// </summary>
    public class EmailService
    {
        public void SendEmail(HouseViewing viewing)
        {
            //this is fake. 
        }
    }
}