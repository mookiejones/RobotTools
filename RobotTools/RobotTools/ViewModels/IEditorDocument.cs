using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotTools.UI.Editor;

namespace RobotTools.ViewModels
{
    interface IEditorDocument
    {
        AvalonEditor TextBox { get; set; }
    }
}
