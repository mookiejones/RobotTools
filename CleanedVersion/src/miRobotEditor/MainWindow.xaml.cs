using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Practices.ServiceLocation;
using miRobotEditor.Core.Classes;
using miRobotEditor.Core.Classes.Messaging;
using miRobotEditor.Core.Classes.Messaging.Interfaces;
using miRobotEditor.EditorControl;
using miRobotEditor.Properties;
using miRobotEditor.Resources;
using miRobotEditor.ViewModel;
using miRobotEditor.ViewModels;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;


namespace miRobotEditor
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            Initialize();
           
        }
        #endregion

        public static MainWindow Instance { get; set; }

        private void AddDocument(DocumentModel obj)
        {
            throw new NotImplementedException();
        }

        private void Initialize()
        {


            Closing += (s, e) => ViewModelLocator.Cleanup();
            DragEnter += (s, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effects = DragDropEffects.Copy;
            };

            ThemeManager.ChangeAppTheme(Application.Current, "Light");
            Messenger.Default.Register<DocumentModel>(this, AddDocument);

            KeyDown += (s, e) => StatusBarViewModel.Instance.ManageKeys(e);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private void DropFiles(object sender, DragEventArgs e)
        {
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);

            foreach (var t in files)
            {
                var title = MessageResources.FileDropped;
                var description = String.Format(MessageResources.Opening, t);

                var msg = new OutputWindowMessage(title, description, MsgIcon.Info);
                Messenger.Default.Send<IMessage>(msg);

                var fm = new FileMessage(t);
                Messenger.Default.Send(fm);
            }
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            Settings.Default.OpenDocuments = String.Empty;
            LayoutDocumentPane docpane = DockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();

            if (docpane != null)
                foreach (
                    DocumentModel d in
                        docpane.Children.Select(doc => doc.Content as DocumentModel)
                            .Where(d => d != null && d.FilePath != null))
                {
                    Settings.Default.OpenDocuments += d.FilePath + ';';
                }

            Settings.Default.Save();

            SaveLayout();

            var main = ServiceLocator.Current.GetInstance<MainViewModel>();
            main.IsClosing = true;

//            App.Application.Shutdown();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            //   LoadLayout();
                LoadItems();
//            Splasher.CloseSplash();
            //  LoadLayout();

        }

        private void LoadItems()
        {
            //Load Files that were closed with the window the last time the Program was executed
            LoadOpenFiles();
            //If No open files, Open one
            var docpane = DockManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (docpane != null && docpane.ChildrenCount == 0)
            {
                var main = ServiceLocator.Current.GetInstance<MainViewModel>();
                main.AddNewFile();
            }


            ProcessArgs();
        }


        private static void LoadOpenFiles()
        {
            /*
             //string[] s = Settings.Default.OpenDocuments.Split(';');
             for (int i = 0; i < s.Length - 1; i++)
             {
                 if (File.Exists(s[i]))
                     OpenFile(s[i]);
             }
              * */
        }

        /// <summary>
        ///     Open file from parameters sent to program
        /// </summary>
        private static void ProcessArgs()
        {
            string[] args = Environment.GetCommandLineArgs();

            for (int i = 1; i < args.Length; i++)
            {
                OpenFile(args[i]);
            }
        }

        private void SaveLayout()
        {
            var serializer = new XmlLayoutSerializer(DockManager);
            using (var stream = new StreamWriter(Global.DockConfig))
                serializer.Serialize(stream);
        }


        private void LoadLayout()
        {
            if (!File.Exists(Global.DockConfig))
                return;

            var serializer = new XmlLayoutSerializer(DockManager);
            using (new StreamReader(Global.DockConfig)) serializer.Deserialize(Global.DockConfig);
        }

        private static void OpenFile(string filename)
        {
            var main = ServiceLocator.Current.GetInstance<MainViewModel>();
            main.Open(filename);
        }
    }
}