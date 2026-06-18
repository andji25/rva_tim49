using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Commands
{
    public class EditVisitCommand : VisitCommand
    {
        private DestinationVisit oldVisit;

        public EditVisitCommand(DestinationVisit newVisit, DestinationVisit oldVisit, IVisitRepository repository)
        {
            this.visit = newVisit;
            this.oldVisit = oldVisit;
            this.repository = repository;
        }

        public override void Execute()
        {
            repository.Edit(visit);
        }

        public override void Undo()
        {
            repository.Edit(oldVisit);
        }
    }
}
