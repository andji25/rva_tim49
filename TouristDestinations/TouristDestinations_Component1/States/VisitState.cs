using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.States
{
    public abstract class VisitState
    {
        protected DestinationVisit visit;

        public VisitState(DestinationVisit visit)
        {
            this.visit = visit;
        }

        public abstract void ChangeState();
        protected abstract void CheckState();
    }
}
