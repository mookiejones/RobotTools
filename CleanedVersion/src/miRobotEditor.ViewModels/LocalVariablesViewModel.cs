using miRobotEditor.Core.Classes;

namespace miRobotEditor.ViewModels
{
    public class LocalVariablesViewModel:ToolViewModel
    {
        public const string ToolContentId = "LocalVariablesTool";

        public LocalVariablesViewModel() 
        {
            ContentId = ToolContentId;

          //  IconSource = Utilities.GetIcon(Global.IconProperty);
        }

    }
}
