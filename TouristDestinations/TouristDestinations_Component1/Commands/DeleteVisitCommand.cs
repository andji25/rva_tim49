using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Commands
{
    public class DeleteVisitCommand : VisitCommand
    {
        public DeleteVisitCommand(DestinationVisit visit, IVisitRepository repository)
        {
            this.visit = visit;
            this.repository = repository;
        }

        public override void Execute()
        {
            repository.Delete(visit.DestinationId, visit.DateOfVisit);
        }

        public override void Undo()
        {
            repository.Add(visit);
        }
    }
}
