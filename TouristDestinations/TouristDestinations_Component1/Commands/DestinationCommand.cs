using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Commands
{
    public abstract class DestinationCommand : IUndoableCommand
    {
        protected TouristDestination destination;
        protected IDestinationRepository repository;

        public abstract void Execute();
        public abstract void Undo();
    }
}
