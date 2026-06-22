using System;
using System.Runtime.Serialization;

namespace TouristDestinations_Component2.Models
{
    [DataContract]
    public class DestinationVisit
    {
        [DataMember]
        public Guid DestinationId { get; private set; }
        [DataMember]
        public DateTime DateOfVisit { get; private set; }
        [DataMember]
        public int NumberOfVisitors { get; private set; }
        [DataMember]
        public int DurationOfVisit { get; private set; }
        [DataMember]
        public double Revenue { get; private set; }
        [DataMember]
        public VisitStateType StateType { get; private set; }

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