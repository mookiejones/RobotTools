using System;
using System.Windows;

namespace miRobotEditor.ViewModels
{
    public class FindandReplaceControl:Window
    {
        private static FindandReplaceControl _instance;

        public FindandReplaceControl(object instance)
        {
            throw new NotImplementedException();
        }

        private FindandReplaceControl()
        {
            throw new NotImplementedException();
        }

        public static FindandReplaceControl Instance
        {
            get { return _instance ?? (_instance = new FindandReplaceControl()); }
            set { _instance = value; }
        }
    }
}