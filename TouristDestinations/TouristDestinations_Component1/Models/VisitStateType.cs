using System.Runtime.Serialization;

namespace TouristDestinations_Component1.Models
{
    [DataContract(Namespace = "http://touristdestinations.com")]
    public enum VisitStateType
    {
        [EnumMember]
        Popular,
        [EnumMember]
        Stable,
        [EnumMember]
        Decline,
        [EnumMember]
        OffSeason
    }
}