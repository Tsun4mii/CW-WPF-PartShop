using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Commands
{
    public struct UndoInfo
    {
        public UndoCommand command;
        public Part partChanged;

        public UndoInfo(UndoCommand command, Part device)
        {
            this.command = command;
            this.partChanged = device;
        }
    }

    public class UndoManager
    {
        public Stack<UndoInfo> UndoCommands = new Stack<UndoInfo>();
        public Stack<UndoInfo> RedoCommands = new Stack<UndoInfo>();

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

        public void Add(UndoCommand command, Part device)
        {
            if (device != null)
            {
                UndoCommands.Push(new UndoInfo(command, device));
            }
        }
    }
}
