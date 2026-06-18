using System;
using TouristDestinations_Component1.States;

namespace TouristDestinations_Component1.Models
{
    public class DestinationVisit
    {
        private Guid destinationId;
        private DateTime dateOfVisit;
        private int numberOfVisitors;
        private int durationOfVisit;
        private double revenue;
        private VisitState state;

        public Guid DestinationId => destinationId;
        public DateTime DateOfVisit => dateOfVisit;
        public int NumberOfVisitors => numberOfVisitors;
        public int DurationOfVisit => durationOfVisit;
        public double Revenue => revenue;

        public DestinationVisit(Guid destinationId, DateTime dateOfVisit, int numberOfVisitors, int durationOfVisit, double revenue)
        {
            this.destinationId = destinationId;
            this.dateOfVisit = dateOfVisit;
            this.numberOfVisitors = numberOfVisitors;
            this.durationOfVisit = durationOfVisit;
            this.revenue = revenue;
            this.state = new PopularState(this);
        }

        public void SetState(VisitState newState)
        {
            state = newState;
        }

        public void ChangeState()
        {
            state.ChangeState();
        }
    }
}
