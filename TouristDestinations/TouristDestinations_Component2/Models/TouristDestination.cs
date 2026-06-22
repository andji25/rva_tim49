using System;
using System.Runtime.Serialization;

namespace TouristDestinations_Component2.Models
{
    [DataContract]
    public class TouristDestination
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Type { get; set; }

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
