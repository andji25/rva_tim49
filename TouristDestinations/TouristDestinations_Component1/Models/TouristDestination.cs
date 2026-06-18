using System;

namespace TouristDestinations_Component1.Models
{
    public class TouristDestination
    {
        private Guid id;
        private string name;
        private string country;
        private string type;

        public Guid Id => id;
        public string Name => name;
        public string Country => country;
        public string Type => type;

        public TouristDestination()
        {
            id = Guid.NewGuid();
        }

        public TouristDestination(string name, string country, string type)
        {
            id = Guid.NewGuid();
            this.name = name;
            this.country = country;
            this.type = type;
        }
    }
}
