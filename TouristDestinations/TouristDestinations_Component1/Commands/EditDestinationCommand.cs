using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Commands
{
    public class EditDestinationCommand : DestinationCommand
    {
        private TouristDestination oldDestination;

        public EditDestinationCommand(TouristDestination newDestination, TouristDestination oldDestination, IDestinationRepository repository)
        {
            this.destination = newDestination;
            this.oldDestination = oldDestination;
            this.repository = repository;
        }

        public override void Execute()
        {
            repository.Edit(destination);
        }

        public override void Undo()
        {
            repository.Edit(oldDestination);
        }
    }
}
