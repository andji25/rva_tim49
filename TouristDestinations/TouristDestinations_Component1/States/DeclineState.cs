using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.States
{
    public class DeclineState : VisitState
    {
        public DeclineState(DestinationVisit visit) : base(visit) { }

        public override void ChangeState()
        {
            CheckState();
        }

        protected override void CheckState()
        {
            if (visit.Revenue < 1000.0)
                visit.SetState(new OffSeasonState(visit));
        }
    }
}
