namespace RobotTools.UI.Editor.ViewModels
{
    internal class ToolTipViewModel:ViewModelBase
    {
        private string _title;
        public string Title { get => _title; set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            } }

        private string _message;
        public string Message
        {
            get => _message; set
            {
                _message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }
    }
}
