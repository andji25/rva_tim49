using System;

namespace TouristDestinations_Component1.Models
{
    public class TouristDestination
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public string Type { get; private set; }

        public TouristDestination()
        {
            Id = Guid.NewGuid();
        }

        public TouristDestination(string name, string country, string type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Country = country;
            Type = type;
        }
    }
}
