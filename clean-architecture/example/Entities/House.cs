using System.Collections.Generic;

namespace example.Entities
{
    public class House
    {
        public int? Id { get; set; }
        public string Address { get; set; }
        public IList<Interest> Leads { get;  }

        public House()
        {
            Leads = new List<Interest>();
        }

        public void RegisterInterest(Interest interest)
        {
            Leads.Add(interest);
        }
    }
}