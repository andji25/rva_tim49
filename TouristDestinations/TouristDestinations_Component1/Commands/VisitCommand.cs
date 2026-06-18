using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Commands
{
    public abstract class VisitCommand : IUndoableCommand
    {
        protected DestinationVisit visit;
        protected IVisitRepository repository;

        public abstract void Execute();
        public abstract void Undo();
    }
}
