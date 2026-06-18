using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Commands
{
    public class DeleteDestinationCommand : DestinationCommand
    {
        public DeleteDestinationCommand(TouristDestination destination, IDestinationRepository repository)
        {
            this.destination = destination;
            this.repository = repository;
        }

        public override void Execute()
        {
            repository.Delete(destination.Id);
        }

        public override void Undo()
        {
            repository.Add(destination);
        }
    }
}
