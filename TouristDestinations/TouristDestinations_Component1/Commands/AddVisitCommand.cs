using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Commands
{
    public class AddVisitCommand : VisitCommand
    {
        public AddVisitCommand(DestinationVisit visit, IVisitRepository repository)
        {
            this.visit = visit;
            this.repository = repository;
        }

        public override void Execute()
        {
            repository.Add(visit);
        }

        public override void Undo()
        {
            repository.Delete(visit.DestinationId, visit.DateOfVisit);
        }
    }
}
