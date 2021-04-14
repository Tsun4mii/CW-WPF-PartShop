using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Commands
{
    public class UndoCommand : IUndo
    {
        private readonly Func<object, Part> execute;
        private readonly Action<object> unexecute;

        public event EventHandler CanExecuteChanged;

        public UndoCommand(Func<object, Part> execute, Action<object> unexecute)
        {
            this.execute = execute;
            this.unexecute = unexecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var res = this.execute(parameter);
            //SearchControl.undoCommandManager.Add(this, res); //--Note: Админ контролл
        }

        public void Unexecute(object parameter)
        {
            this.unexecute(parameter);
        }
    }
}
