using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.States
{
    public class PopularState : VisitState
    {
        public PopularState(DestinationVisit visit) : base(visit) { }

        public override void ChangeState()
        {
            CheckState();
        }

        protected override void CheckState()
        {
            if (visit.NumberOfVisitors < 100)
                visit.SetState(new StableState(visit));
        }
    }
}
