using System.Collections.Generic;
using TouristDestinations_Component1.Interfaces;

namespace TouristDestinations_Component1.Services
{
    public class CommandManager
    {
        private Stack<IUndoableCommand> undoStack;
        private Stack<IUndoableCommand> redoStack;
        private ILogger logger;

        public CommandManager(ILogger logger)
        {
            this.logger = logger;
            undoStack = new Stack<IUndoableCommand>();
            redoStack = new Stack<IUndoableCommand>();
        }

        public void ExecuteCommand(IUndoableCommand command)
        {
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear();
            logger.Log($"Executed: {command.GetType().Name}");
        }

        public void Undo()
        {
            if (undoStack.Count == 0) return;
            IUndoableCommand command = undoStack.Pop();
            command.Undo();
            redoStack.Push(command);
            logger.Log($"Undo: {command.GetType().Name}");
        }

        public void Redo()
        {
            if (redoStack.Count == 0) return;
            IUndoableCommand command = redoStack.Pop();
            command.Execute();
            undoStack.Push(command);
            logger.Log($"Redo: {command.GetType().Name}");
        }
    }
}
