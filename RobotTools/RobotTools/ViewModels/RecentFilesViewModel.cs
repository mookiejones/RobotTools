using RobotTools.ViewModels.MRU;
using System;
using System.IO;

namespace RobotTools.ViewModels
{
    internal class RecentFilesViewModel : Base.ToolViewModel
    {
        private MRUListVM mMruList;

        public const string ToolContentId = "RecentFilesTool";

        public RecentFilesViewModel()
          : base("Recent Files")
        {
            ////Workspace.This.ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);
            ContentId = ToolContentId;

            mMruList = new MRUListVM();
        }

        public override Uri IconSource
        {
            get
            {
                return new Uri("pack://application:,,,/RobotTools;component/ViewModels/MRU/Images/NoPin16.png", UriKind.RelativeOrAbsolute);
            }
        }

        public MRUListVM MruList
        {
            get
            {
                return mMruList;
            }

            private set
            {
                if (mMruList != value)
                {
                    mMruList = value;
                    NotifyPropertyChanged(() => MruList);
                }
            }
        }

        public void AddNewEntryIntoMRU(string filePath)
        {
            if (MruList.FindMRUEntry(filePath) == null)
            {
                MRUEntryVM e = new MRUEntryVM() { IsPinned = false, PathFileName = filePath };

                MruList.AddMRUEntry(e);

                NotifyPropertyChanged(() => MruList);
            }
        }
    }
}
