namespace miRobotEditor.ViewModels
{
    public class GlobalOptions
    {

        public GlobalOptions()
        {
            Instance = this;
            Options = new GlobalOptionsModel();
        }
        public static GlobalOptions Instance { get; private set; }

        public GlobalOptionsModel Options { get; set; }
    }
}