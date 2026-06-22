using System;
using System.Runtime.Serialization;

namespace TouristDestinations_Component2.Models
{
    [DataContract]
    public class TouristDestination
    {
        [DataMember]
        public Guid Id { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Country { get; private set; }
        [DataMember]
        public string Type { get; private set; }

        public TouristDestination()
        {
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
