using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Commands
{
    public class AddDestinationCommand : DestinationCommand
    {
        public AddDestinationCommand(TouristDestination destination, IDestinationRepository repository)
        {
            this.destination = destination;
            this.repository = repository;
        }

        public override void Execute()
        {
            repository.Add(destination);
        }

        public override void Undo()
        {
            repository.Delete(destination.Id);
        }
    }
}
