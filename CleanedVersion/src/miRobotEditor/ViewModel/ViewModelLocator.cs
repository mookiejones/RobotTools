/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:miRobotEditor.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System.Diagnostics.CodeAnalysis;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using miRobotEditor.Design;
using miRobotEditor.Model;
using miRobotEditor.ViewModels;

namespace miRobotEditor.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            var ioc = SimpleIoc.Default;
            if (ioc == null)
                return;
            ServiceLocator.SetLocatorProvider(() => (IServiceLocator) SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<temp>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MessageViewModel>();
            SimpleIoc.Default.Register<NotesViewModel>();
            SimpleIoc.Default.Register<StatusBarViewModel>();
        }


        /// <summary>
        ///     Gets the Main property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public temp Temp
        {
            get { return ServiceLocator.Current.GetInstance<temp>(); }
        }

        /// <summary>
        ///     Gets the Main property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public StatusBarViewModel StatusBar
        {
            get { return ServiceLocator.Current.GetInstance<StatusBarViewModel>(); }
        }


        public MessageViewModel Messages
        {
            get { return ServiceLocator.Current.GetInstance<MessageViewModel>(); }
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public NotesViewModel Notes
        {
            get { return ServiceLocator.Current.GetInstance<NotesViewModel>(); }
        }

        private static IServiceLocator GetProvider()
        {
            IServiceLocator ioc = SimpleIoc.Default;
            if (ioc != null)
                return ioc;
            return null;
        }


        /// <summary>
        ///     Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}