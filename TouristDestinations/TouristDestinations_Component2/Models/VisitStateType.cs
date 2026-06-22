using System.Runtime.Serialization;

namespace TouristDestinations_Component2.Models
{
    [DataContract]
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