using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.Commands
{
    interface IUndo : ICommand
    {
        void Unexecute(object obj);
    }
}
