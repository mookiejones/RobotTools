using System;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using miRobotEditor.Core.Handlers;

namespace miRobotEditor.ViewModels
{
    public sealed class UpdateCheckerModel : DependencyObject
    {

        public event UpdateRequiredHandler UpdateRequired;
        private void RaiseUpdateRequired()
        {
            if (UpdateRequired != null)
                UpdateRequired(this, new EventArgs());
        }
        public UpdateVersion Version { get; set; }

        #region Commands


        
      
       
        private RelayCommand _cancelcommand;

        public ICommand CancelCommand
        {
            get { return _cancelcommand ?? (_cancelcommand = new RelayCommand(Cancel,() => Version != null)); }
        }
        #endregion


        #region Properties




        public bool UpdateApplication
        {
            get { return (bool)GetValue(UpdateApplicationProperty); }
            set { SetValue(UpdateApplicationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpdateApplication.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpdateApplicationProperty =
            DependencyProperty.Register("UpdateApplication", typeof(bool), typeof(UpdateCheckerModel), new PropertyMetadata(false));

        
        

public string UpdateText
{
    get { return (string)GetValue(UpdateTextProperty); }
    set { SetValue(UpdateTextProperty, value); }
}

// Using a DependencyProperty as the backing store for UpdateText.  This enables animation, styling, binding, etc...
public static readonly DependencyProperty UpdateTextProperty = 
    DependencyProperty.Register("UpdateText", typeof(string), typeof(UpdateCheckerModel), new PropertyMetadata(""));

   
        public string Title
        {
            get { return String.Format("{0} Updater", ProductName); }
        }

        public string ProductName
        {
            get { return String.Empty; } // App.ProductName; }
        }


/*        public bool AskForUpdates
        {
            get { return Settings.Default.CheckForUpdates; }
            set { Settings.Default.CheckForUpdates = value; RaisePropertyChanged(); }
        }
            */
        #endregion

//            protected  void Update()
 //       {
  //          UpdateApplication = true;
   //     }
             void Cancel()
        {

        }



        public UpdateCheckerModel()
        {
            // Should I check for updates
            CheckForUpdates();
        }


        public class UpdateVersion
        {

            public bool IsOld
            {
                get
                {
                    if (Current.Major > Major) return true;
                    if (Current.Minor > Minor) return true;
                    return Current.Revision > Revision;
                }
            }


            public Version Current
            {
                get;
                set;
            }
            public string Version
            {
                get { return String.Format("{0}.{1}.{2}.{3}", Major, Minor, MinorRevision, Revision); }
                set
                {
                    var v = value.Split('.');
                    Major = Convert.ToInt32(v[0]);
                    Minor = Convert.ToInt32(v[1]);
                    MinorRevision = Convert.ToInt32(v[2]);
                    Revision = Convert.ToInt32(v[3]);
                }
            }
            public int Major { get; set; }
            public int Minor { get; set; }
            public int MinorRevision { get; set; }
            public int Revision { get; set; }
            public string Link { get; set; }
        }

        private void CheckForUpdates()
        {

            try
            {
                const string link = @"https://sites.google.com/site/dmcautomation/home/software/dmc-robot-editor";
                using (var client = new WebClient())
                {
                    var contents = client.DownloadString(link);
                    const string dlLink = "Latest Version</div><div><br /></div><div><br /></div><div>-<a href=\"";
                    var dlink = contents.Substring(contents.IndexOf(dlLink, StringComparison.Ordinal) + dlLink.Length);

                    dlink = dlink.Substring(0, dlink.IndexOf("\"", StringComparison.Ordinal));

                    const string dlVersion = "DMC Robot Editor V";

                    var dversion =
                        contents.Substring(contents.IndexOf(dlVersion, StringComparison.Ordinal) + dlVersion.Length);

                    dversion = dversion.Substring(0, dversion.IndexOf("<", StringComparison.Ordinal));

                    Version = new UpdateVersion
                    {
                        Link = dlink,
                        Version = dversion,
                        Current = Assembly.GetEntryAssembly().GetName().Version
                    };
                }


                if (Version.IsOld)
                    RaiseUpdateRequired();
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {
            }
        }
    }
}
