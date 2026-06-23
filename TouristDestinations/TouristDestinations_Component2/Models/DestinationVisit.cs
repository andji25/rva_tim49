using System;
using System.Runtime.Serialization;

namespace TouristDestinations_Component2.Models
{
    [DataContract(Namespace = "http://touristdestinations.com")]
    public class DestinationVisit
    {
        [DataMember]
        public Guid DestinationId { get; set; }
        [DataMember]
        public DateTime DateOfVisit { get; set; }
        [DataMember]
        public int NumberOfVisitors { get; set; }
        [DataMember]
        public int DurationOfVisit { get; set; }
        [DataMember]
        public double Revenue { get; set; }
        [DataMember]
        public VisitStateType StateType { get; set; }

        public DestinationVisit()
        {
        }

        public DestinationVisit(Guid destinationId, DateTime dateOfVisit, int numberOfVisitors, int durationOfVisit, double revenue)
        {
            DestinationId = destinationId;
            DateOfVisit = dateOfVisit;
            NumberOfVisitors = numberOfVisitors;
            DurationOfVisit = durationOfVisit;
            Revenue = revenue;
            StateType = VisitStateType.Popular;
        }
    }
}