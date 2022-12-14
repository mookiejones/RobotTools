using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using KRC4Options.Annotations;

namespace KRC4Options
{
    public sealed class MainViewModel : ViewModelBase
    {
        private const string ConfigXmlPath = @"C:\KRC\ROBOTER\Config\System\Common\ConfigXML.xml";
        private const string AdminPath = @"C:\KRC\SmartHMI\Config\Authentication.config";

        #region Config

        /// <summary>
        ///     The <see cref="Config" /> property's name.
        /// </summary>
        private const string ConfigPropertyName = "Config";

        private ConfigList _config = new ConfigList();

        /// <summary>
        ///     Sets and gets the Config property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ConfigList Config
        {
            get { return _config; }

            set
            {
                if (_config == value)
                {
                    return;
                }

                OnPropertyChanging(ConfigPropertyName);
                _config = value;
                OnPropertyChanged(ConfigPropertyName);
            }
        }

        #endregion

        #region Authentication

        /// <summary>
        ///     The <see cref="Authentication" /> property's name.
        /// </summary>
        private const string AuthenticationPropertyName = "Authentication";

        private configuration _authentication = new configuration();

        /// <summary>
        ///     Sets and gets the Authentication property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public configuration Authentication
        {
            get { return _authentication; }

            set
            {
                if (_authentication == value)
                {
                    return;
                }

                OnPropertyChanging(AuthenticationPropertyName);
                _authentication = value;
                OnPropertyChanged(AuthenticationPropertyName);
            }
        }

        #endregion

        public MainViewModel()
        {

            MakeDirectories();

            ReadConfig();
            ReadAuthentication();
        }

        ~MainViewModel()
        {
            WriteConfig();
            WriteAuthentication();

#if DEBUG
            RemoveDirectories();
#endif
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void RemoveDirectories()
        {
            RemoveDirectory(ConfigXmlPath);
            RemoveDirectory(AdminPath);

        }


        private static void RemoveDirectory(string path)
        {
            if (File.Exists(path))
                File.Delete(path);

            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }
        private static void MakeDirectories()
        {

            if (File.Exists(ConfigXmlPath))
                File.Copy(ConfigXmlPath, @"C:\\ConfigXML.xml", true);

            if (File.Exists(AdminPath))
                File.Copy(AdminPath, @"C:\\Authentication.config", true);

#if DEBUG
            var fi = new FileInfo(ConfigXmlPath);

            if (!Directory.Exists(fi.DirectoryName))
                Directory.CreateDirectory(fi.DirectoryName);
            fi = new FileInfo(AdminPath);

            if (!Directory.Exists(fi.DirectoryName))
                Directory.CreateDirectory(fi.DirectoryName);

            if (!File.Exists(AdminPath))
                WriteAuthentication();


            if (!File.Exists(ConfigXmlPath))
                WriteConfig();
#endif
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private void WriteAuthentication()
        {
            var serial = new XmlSerializer(typeof (configurationAuthenticationManagement));
            using (var stream = new StreamWriter(AdminPath))
            {
                serial.Serialize(stream, Authentication);
            }
        }

        private void WriteConfig()
        {
            var serial = new XmlSerializer(typeof (ConfigList));
            using (var stream = new StreamWriter(ConfigXmlPath))
            {
                serial.Serialize(stream, Config);
            }
        }

        private void ReadAuthentication()
        {
            var serial = new XmlSerializer(typeof (configuration));
            using (var stream = new StreamReader(AdminPath))
            {

                    Authentication = (configuration) serial.Deserialize(stream);

            }
        }

        private void ReadConfig()
        {
            var serial = new XmlSerializer(typeof (ConfigList));
            using (var stream = new StreamReader(ConfigXmlPath))
            {
                Config = (ConfigList) serial.Deserialize(stream);
            }
        }

   
    }

    public class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        [NotifyPropertyChangingInvocator]
        protected void OnPropertyChanging(string propertyName)
        {
            PropertyChangingEventHandler handler = PropertyChanging;
            if (handler != null) handler(this, new PropertyChangingEventArgs(propertyName));
        }
    }
}