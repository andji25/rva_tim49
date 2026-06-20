using System;
using System.Xml.Serialization;
using TouristDestinations_Component1.States;

namespace TouristDestinations_Component1.Models
{
    public class DestinationVisit
    {
        public Guid DestinationId { get; private set; }
        public DateTime DateOfVisit { get; private set; }
        public int NumberOfVisitors { get; private set; }
        public int DurationOfVisit { get; private set; }
        public double Revenue { get; private set; }
        public VisitStateType StateType { get; private set; }

        [XmlIgnore]
        public VisitState State { get; private set; }

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
