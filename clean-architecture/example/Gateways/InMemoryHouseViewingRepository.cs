using System.Collections.Generic;
using example.Entities;

namespace example.Gateways
{
    public class InMemoryHouseViewingRepository
    {
        private static readonly Dictionary<int, HouseViewing> Store = new Dictionary<int, HouseViewing>();

        public HouseViewing Get(int key)
        {
            return Store[key];
        }

        public HouseViewing Save(HouseViewing value)
        {
            if (value.Id.HasValue)
            {
                Store[value.Id.Value] = value;
            }
            return value;
        }

    }
}