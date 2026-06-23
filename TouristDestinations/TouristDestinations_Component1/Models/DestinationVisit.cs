using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using TouristDestinations_Component1.States;

namespace TouristDestinations_Component1.Models
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

        [XmlIgnore]
        public VisitState State { get; set; }

        public DestinationVisit()
        {
            State = new PopularState(this);
            StateType = VisitStateType.Popular;
        }

        public DestinationVisit(Guid destinationId, DateTime dateOfVisit, int numberOfVisitors, int durationOfVisit, double revenue)
        {
            DestinationId = destinationId;
            DateOfVisit = dateOfVisit;
            NumberOfVisitors = numberOfVisitors;
            DurationOfVisit = durationOfVisit;
            Revenue = revenue;
            State = new PopularState(this);
            StateType = VisitStateType.Popular;
        }

        public void SetState(VisitState newState)
        {
            State = newState;
            switch (newState)
            {
                case PopularState _:
                    StateType = VisitStateType.Popular;
                    break;
                case StableState _:
                    StateType = VisitStateType.Stable;
                    break;
                case DeclineState _:
                    StateType = VisitStateType.Decline;
                    break;
                case OffSeasonState _:
                    StateType = VisitStateType.OffSeason;
                    break;
            }
        }

        public void ChangeState()
        {
            State.ChangeState();
        }
    }
}
