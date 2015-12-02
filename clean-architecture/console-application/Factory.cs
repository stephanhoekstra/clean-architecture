using example.Entities;
using example.Gateways;

namespace console_application
{
    public static class Factory
    {
        public static InMemoryHouseRepository CreateDummyInMemoryHouseRepository()
        {
            var dummy = new InMemoryHouseRepository();
            dummy.Save(new House { Id = 45474845, Address = "Dam 1, Amsterdam", Leads = { } });
            return dummy;
        }
    }
}