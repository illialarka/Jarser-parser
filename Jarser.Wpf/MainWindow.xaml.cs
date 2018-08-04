using System;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Jarser.Logger;
using Jarser.Wpf.ApplicationViewModel;
using Microsoft.Win32;

namespace Jarser.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string BrowserEmulationKey = InternetExplorerRootKey + @"\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
        private const string InternetExplorerRootKey = @"Software\Microsoft\Internet Explorer";

        private readonly ILogger _logger = new Logger.Logger(typeof(MainWindowViewModel));
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            if (!SetBrowserEmulationVersion(BrowserEmulationVersion.Version11Edge))
            {
                _logger.Error("Initialization was failed! NOT SET THE VERSION OF BROWSER!");
            }

            MainBrowser.Navigating += (s, e) =>
            {
                var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (fiComWebBrowser == null)
                {
                    return;
                }

                var objComWebBrowser = fiComWebBrowser.GetValue(MainBrowser);
                if (objComWebBrowser == null)
                {
                    return;
                }

                objComWebBrowser.GetType().InvokeMember("Silent", System.Reflection.BindingFlags.SetProperty, null,
                    objComWebBrowser, new object[] {true});
            };

            MainBrowser.Navigated += (sender, args) =>
            {
                var webBrowserSource = MainBrowser.Source.ToString();

                var correctUrlMatchValue =
                    Regex.Match(webBrowserSource, "(?<=https://)www.instagram.com").Value;

                if (string.IsNullOrEmpty(correctUrlMatchValue))
                {
                    MessageBox.Show(
                        "You can navigate only to \"https://www.instagram.com\" page. Please, navigate to correct page. You will redirect to correct page.",
                        "Wrong URL", MessageBoxButton.OK);
                    MainBrowser.Source = new Uri("https://www.instagram.com/");
                    MainBrowser.Navigate("https://www.instagram.com/");
                }
            };

            var curDir = Directory.GetCurrentDirectory();
            DocumentationBrowser.Navigate($"file:///{curDir}/Doc/documentation.htm");
        }

        public static int GetInternetExplorerMajorVersion()
        {
            int result;
            result = 0;

            try
            {
                RegistryKey key;
                key = Registry.LocalMachine.OpenSubKey(InternetExplorerRootKey);

                if (key != null)
                {
                    object value;
                    value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);

                    if (value != null)
                    {
                        string version;
                        int separator;

                        version = value.ToString();
                        separator = version.IndexOf('.');
                        if (separator != -1)
                        {
                            int.TryParse(version.Substring(0, separator), out result);
                        }
                    }
                }
            }
            catch (SecurityException)
            {
                // The user does not have the permissions required to read from the registry key.
            }
            catch (UnauthorizedAccessException)
            {
                // The user does not have the necessary registry rights.
            }

            return result;
        }

        public static BrowserEmulationVersion GetBrowserEmulationVersion()
        {
            BrowserEmulationVersion result;
            result = BrowserEmulationVersion.Default;

            try
            {
                RegistryKey key;
                key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);
                if (key != null)
                {
                    string programName;
                    object value;

                    programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                    value = key.GetValue(programName, null);

                    if (value != null)
                    {
                        result = (BrowserEmulationVersion)Convert.ToInt32(value);
                    }
                }
            }
            catch (SecurityException)
            {
                // The user does not have the permissions required to read from the registry key.
            }
            catch (UnauthorizedAccessException)
            {
                // The user does not have the necessary registry rights.
            }

            return result;
        }

        public static bool SetBrowserEmulationVersion(BrowserEmulationVersion browserEmulationVersion)
        {
            bool result = false;

            try
            {
                RegistryKey key;

                key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);

                if (key != null)
                {
                    string programName;

                    programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);

                    if (browserEmulationVersion != BrowserEmulationVersion.Default)
                    {
                        // if it's a valid value, update or create the value
                        key.SetValue(programName, (int)browserEmulationVersion, RegistryValueKind.DWord);
                    }
                    else
                    {
                        // otherwise, remove the existing value
                        key.DeleteValue(programName, false);
                    }

                    result = true;
                }
            }
            catch (SecurityException)
            {
                // The user does not have the permissions required to read from the registry key.
            }
            catch (UnauthorizedAccessException)
            {
                // The user does not have the necessary registry rights.
            }

            return result;
        }

        public static bool SetBrowserEmulationVersion()
        {
            int ieVersion;
            BrowserEmulationVersion emulationCode;

            ieVersion = GetInternetExplorerMajorVersion();

            if (ieVersion >= 11)
            {
                emulationCode = BrowserEmulationVersion.Version11;
            }
            else
            {
                switch (ieVersion)
                {
                    case 10:
                        emulationCode = BrowserEmulationVersion.Version10;
                        break;
                    case 9:
                        emulationCode = BrowserEmulationVersion.Version9;
                        break;
                    case 8:
                        emulationCode = BrowserEmulationVersion.Version8;
                        break;
                    default:
                        emulationCode = BrowserEmulationVersion.Version7;
                        break;
                }
            }

            return SetBrowserEmulationVersion(emulationCode);
        }

        public static bool IsBrowserEmulationSet()
        {
            return GetBrowserEmulationVersion() != BrowserEmulationVersion.Default;
        }
    }

    public enum BrowserEmulationVersion
    {
        Default = 0,
        Version7 = 7000,
        Version8 = 8000,
        Version8Standards = 8888,
        Version9 = 9000,
        Version9Standards = 9999,
        Version10 = 10000,
        Version10Standards = 10001,
        Version11 = 11000,
        Version11Edge = 11001
    }
}
