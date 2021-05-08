using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Commands
{
    public struct UndoInfo<T>
    {
        public UndoCommand<T> command;
        public T partChanged;

        public UndoInfo(UndoCommand<T> command, T device)
        {
            this.command = command;
            this.partChanged = device;
        }
    }

    public class UndoManager<T>
    {
        public Stack<UndoInfo<T>> UndoCommands = new Stack<UndoInfo<T>>();
        public Stack<UndoInfo<T>> RedoCommands = new Stack<UndoInfo<T>>();

        public void Undo()
        {
            if (UndoCommands.Count > 0)
            {
                var info = UndoCommands.Pop();
                info.command.Unexecute(info.partChanged);
                RedoCommands.Push(info);
            }
        }


        public void Redo()
        {
            if (RedoCommands.Count > 0)
            {
                var info = RedoCommands.Pop();
                info.command.Execute(info.partChanged);
            }
        }

        public void Add(UndoCommand<T> command, T device)
        {
            if (device != null)
            {
                UndoCommands.Push(new UndoInfo<T>(command, device));
            }
        }
    }
}
