namespace TouristDestinations_Component1.Interfaces
{
    public interface IUndoableCommand
    {
        void Execute();
        void Undo();
    }
}
