using AvalonDock.Layout.Serialization;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RobotTools.Core.Messages;
using RobotTools.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace RobotTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow  
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<MainViewModel>();

            MainViewModel.This.InitCommandBinding(this);

#if DESIGNER
            SendDesignMessages();
#endif
        }

        private void SendDesignMessages()
        {
            for(var i = 0; i < 20; i++)
            {
                var message = $"Sample Message {i}";
                var title = $"Sample Title{i}";

                var msg = new SampleMessage(title, message);

                // Send a message from some other module
                WeakReferenceMessenger.Default.Send<IMessageBase>(msg);
            }
        }

        #region LoadLayoutCommand
        RelayCommand _loadLayoutCommand = null;
        public ICommand LoadLayoutCommand
        {
            get
            {
                if (_loadLayoutCommand == null)
                {
                    _loadLayoutCommand = new RelayCommand( OnLoadLayout, CanLoadLayout);
                }

                return _loadLayoutCommand;
            }
        }

        private bool CanLoadLayout( )
        {
            return File.Exists(@".\AvalonDock.Layout.config");
        }

        private void OnLoadLayout( )
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            //Here I've implemented the LayoutSerializationCallback just to show
            // a way to feed layout desarialization with content loaded at runtime
            //Actually I could in this case let AvalonDock to attach the contents
            //from current layout using the content ids
            //LayoutSerializationCallback should anyway be handled to attach contents
            //not currently loaded
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
            {
                //if (e.Model.ContentId == FileStatsViewModel.ToolContentId)
                //    e.Content = Workspace.This.FileStats;
                //else if (!string.IsNullOrWhiteSpace(e.Model.ContentId) &&
                //    File.Exists(e.Model.ContentId))
                //    e.Content = Workspace.This.Open(e.Model.ContentId);
            };
            layoutSerializer.Deserialize(@".\AvalonDock.Layout.config");
        }

        #endregion 

        #region SaveLayoutCommand
        RelayCommand _saveLayoutCommand = null;
        public ICommand SaveLayoutCommand
        {
            get
            {
                if (_saveLayoutCommand == null)
                {
                    _saveLayoutCommand = new RelayCommand( OnSaveLayout, CanSaveLayout);
                }

                return _saveLayoutCommand;
            }
        }

        private bool CanSaveLayout( )
        {
            return true;
        }

        private void OnSaveLayout( )
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.Serialize(@".\AvalonDock.Layout.config");
        }

        #endregion 

        private void OnDumpToConsole(object sender, RoutedEventArgs e)
        {
#if DEBUG
            dockManager.Layout.ConsoleDump(0);
#endif
        }

    }
}
