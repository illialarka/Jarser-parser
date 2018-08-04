using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Jarser.Export;
using Jarser.Logger;
using Jarser.Parser;
using Jarser.Parser.Sctipts;
using Jarser.Parser.User;
using Jarser.Wpf.Annotations;
using Jarser.Wpf.Commands;
using Jarser.Wpf.ScriptInserter;
using Microsoft.Win32;
using WebBrowser = System.Windows.Controls.WebBrowser;

namespace Jarser.Wpf.ApplicationViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ILogger _logger = new Logger.Logger(typeof(MainWindowViewModel));
        private IParser _parserInstance;

        #region BackFielForProperties

        private string _url;

        private IEnumerable<User> _parsedUsers;

        private Command _navigateCommand;

        private Command _refreshBrowser;

        private Command _inserScript;

        private Command _parser;

        private Command _selectedUsersWithPhoneNumber;

        private Command _navigateBack;

        private Command _navigateForward;

        private Command _clearResults;

        private Command _saveFile;

        private Command _closeParsing;

        private Command _closeParsingByNicks;

        private Command _parseByNicknames;

        private bool _selectedWihtPhoneNumber;

        private int _progressParse;

        private int _progressParseByNicks;

        private string _progressDescription;

        private string _progressDescriptionByNicks;

        private bool _id;

        private bool _biography;

        private bool _blockedByView;

        private bool _countryBlock;

        private bool _externalUrl;

        private bool _externalUrlShimmed;

        private bool _followers;

        private bool _following;

        private bool _fullName;

        private bool _isPrivate;

        private bool _isVerified;

        private bool _profilePictureUrl;

        private bool _profilePictureHdUrl;

        private bool _userName;

        private bool _phoneNumber;

        private IEnumerable<int> _delay = new List<int>
        {
            100, 200, 300, 400, 500, 600, 700, 800, 900, 1000
        };

        private int _delayValue = 500;

        #endregion

        public MainWindowViewModel()
        {
            Url = "https://www.instagram.com/";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region BindingProperty

        public int DelayValue
        {
            get => _delayValue;
            set
            {
                _delayValue = value;
                OnPropertyChanged(nameof(DelayValue));
            }
        }

        public IEnumerable<int> Delays
        {
            get => _delay;
            set
            {
                _delay = value;
                OnPropertyChanged(nameof(Delays));
            }
        }

        public IEnumerable<User> ParsedUsers
        {
            get
            {
                if (_selectedWihtPhoneNumber)
                {
                    return _parsedUsers.Where(x => !string.IsNullOrWhiteSpace(x.PhoneNumber) && !string.IsNullOrEmpty(x.PhoneNumber));
                }

                return _parsedUsers;
            }

            set
            {
                _parsedUsers = value;
                OnPropertyChanged(nameof(ParsedUsers));
            }
        }

        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        public int ProgressParse
        {
            get => _progressParse;
            set
            {
                _progressParse = value;
                OnPropertyChanged(nameof(ProgressParse));
            }
        }

        public int ProgressParseByNicks
        {
            get => _progressParseByNicks;
            set
            {
                _progressParseByNicks = value;
                OnPropertyChanged(nameof(ProgressParseByNicks));
            }
        }

        public string ProgressDescription
        {
            get => _progressDescription;
            set
            {
                _progressDescription = value;
                OnPropertyChanged(nameof(ProgressDescription));
            }
        }

        public string ProgressDescriptionByNicks
        {
            get => _progressDescriptionByNicks;
            set
            {
                _progressDescriptionByNicks = value;
                OnPropertyChanged(nameof(ProgressDescriptionByNicks));
            }
        }

        public bool Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public bool Biography
        {
            get => _biography;
            set
            {
                _biography = value;
                OnPropertyChanged(nameof(Biography));
            }
        }

        public bool PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public bool UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public bool ProfilePictureHdUrl
        {
            get => _profilePictureHdUrl;
            set
            {
                _profilePictureHdUrl = value;
                OnPropertyChanged(nameof(ProfilePictureHdUrl));
            }
        }

        public bool ProfilePictureUrl
        {
            get => _profilePictureUrl;
            set
            {
                _profilePictureUrl = value;
                OnPropertyChanged(nameof(ProfilePictureUrl));
            }
        }

        public bool IsVerified
        {
            get => _isVerified;
            set
            {
                _isVerified = value;
                OnPropertyChanged(nameof(IsVerified));
            }
        }

        public bool IsPrivate
        {
            get => _isPrivate;
            set
            {
                _isPrivate = value;
                OnPropertyChanged(nameof(IsPrivate));
            }
        }

        public bool FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public bool Following
        {
            get => _following;
            set
            {
                _following = value;
                OnPropertyChanged(nameof(Following));
            }
        }

        public bool Followers
        {
            get => _followers;
            set
            {
                _followers = value;
                OnPropertyChanged(nameof(Followers));
            }
        }

        public bool ExternalUrlShimmed
        {
            get => _externalUrlShimmed;
            set
            {
                _externalUrlShimmed = value;
                OnPropertyChanged(nameof(ExternalUrlShimmed));
            }
        }

        public bool ExternalUrl
        {
            get => _externalUrl;
            set
            {
                _externalUrl = value;
                OnPropertyChanged(nameof(ExternalUrl));
            }
        }

        public bool CountryBlock
        {
            get => _countryBlock;
            set
            {
                _countryBlock = value;
                OnPropertyChanged(nameof(CountryBlock));
            }
        }

        public bool BlockedByView
        {
            get => _blockedByView;
            set
            {
                _blockedByView = value;
                OnPropertyChanged(nameof(BlockedByView));
            }
        }

        #endregion

        #region Commands

        public Command Navigate
        {
            get
            {
                return _navigateCommand ??
                       (_navigateCommand = new Command((browser) =>
                       {
                           if (browser is WebBrowser webBrowser)
                           {
                               _logger.Info($"Change url ({Url}).");
                               webBrowser.Navigate(Url);
                           }
                       }));
            }
        }

        public Command NavigateForward
        {
            get
            {
                return _navigateForward ??
                       (_navigateForward = new Command(
                       (browser) =>
                       {
                           if (browser is WebBrowser webBrowser)
                           {
                               _logger.Info("Navigate forward.");
                               webBrowser.GoForward();
                           }
                       },
                       (browser) =>
                       {
                           if (browser is WebBrowser webBrowser)
                           {
                               return webBrowser.CanGoForward;
                           }

                           return false;
                       }));
            }
        }

        public Command NavigateBack
        {
            get
            {
                return _navigateBack ??
                       (_navigateBack = new Command(
                       (browser) =>
                       {
                           if (browser is WebBrowser webBrowser)
                           {
                               _logger.Info("Navigate back.");
                               webBrowser.GoBack();
                           }
                       },
                       (browser) =>
                       {
                           if (browser is WebBrowser webBrowser)
                           {
                               return webBrowser.CanGoBack;
                           }

                           return false;
                       }));
            }
        }

        public Command RefreshPage
        {
            get
            {
                return _refreshBrowser ??
                       (_refreshBrowser = new Command((browser) =>
                       {
                           if (browser is WebBrowser webBrowser)
                           {
                               _logger.Info($"Refreshh browser {webBrowser.Source}.");
                               webBrowser.Refresh();
                           }
                       }));
            }
        }

        public Command InsertScript
        {
            get
            {
                return _inserScript ??
                       (_inserScript = new Command((browser) =>
                       {
                           if (browser is WebBrowser webBrowser)
                           {
                               try
                               {
                                   var onFollowersPage = Regex.Match(webBrowser.Source.ToString(), "followers").Success;
                                   if (!onFollowersPage)
                                   {
                                       var errorProvider = new ErrorsProcessing.ErrorsLoader();
                                       var error = errorProvider.GetErrorById(1);
                                       MessageBox.Show(error.ErrorText, error.Caption, MessageBoxButton.OK);
                                       return;
                                   }

                                   _logger.Info($"Inserting scripts {webBrowser.Source}.");
                                   var jsInserter = new JsScriptInserter(webBrowser);
                                   var jsTextProvider = new JsScriptProvider();
                                   string curDir = Directory.GetCurrentDirectory();
                                   var path = new Uri($"file:///{curDir}/scripts/scroller_script.js");
                                   jsInserter.InsertScript(jsTextProvider.SelectScript(path));
                                   _logger.Info($"Inserted script from {path}.");
                               }
                               catch (Exception e)
                               {
                                   _logger.Exception(e);
                               }
                           }
                       }));
            }
        }

        public Command Parse
        {
            get
            {
                return _parser ??
                       (_parser = new Command(async (browser) =>
                       {
                           if (_parserInstance != null)
                           {
                               var errorProvider = new ErrorsProcessing.ErrorsLoader();
                               var error = errorProvider.GetErrorById(2);
                               MessageBox.Show(error.ErrorText, error.ErrorText, MessageBoxButton.OK);
                           }

                           if (browser is WebBrowser webBrowser)
                           {
                               try
                               {
                                   var onFollowersPage = Regex.Match(webBrowser.Source.ToString(), "followers").Success;
                                   if (!onFollowersPage)
                                   {
                                       var errorProvider = new ErrorsProcessing.ErrorsLoader();
                                       var error = errorProvider.GetErrorById(3);
                                       MessageBox.Show(error.ErrorText, error.Caption, MessageBoxButton.OK);
                                       return;
                                   }

                                   _logger.Info($"Run parser on {webBrowser.Source}.");

                                   dynamic doc = webBrowser.Document;
                                   var htmlText = doc.documentElement.InnerHtml;
                                   _parserInstance = new Parser.Parser(htmlText);
                                   _parserInstance.Delay = DelayValue;
                                   _parserInstance.ProcessParse += OnProcessParseListener;

                                   ParsedUsers = await _parserInstance.ParseFollowersAsync();

                                   _logger.Info($"Finden users {ParsedUsers.Count()}.");
                               }
                               catch (Exception e)
                               {
                                   _logger.Exception(e);
                               }
                           }
                       }));
            }
        }

        public Command CloseParsing
        {
            get
            {
                return _closeParsing ??
                       (_closeParsing = new Command((obj) =>
                       {
                           if (_parserInstance != null)
                           {
                               _parserInstance.Cancel();
                               _parserInstance = null;
                               ProgressDescription = string.Empty;
                               ProgressParse = 0;
                           }
                           else
                           {
                               var errorProvider = new ErrorsProcessing.ErrorsLoader();
                               var error = errorProvider.GetErrorById(4);
                               MessageBox.Show(error.ErrorText, error.Caption, MessageBoxButton.OK);
                           }
                       })); 
            }
        }

        public Command CloseParsingByNicks
        {
            get
            {
                return _closeParsingByNicks ??
                       (_closeParsingByNicks = new Command((obj) =>
                       {
                           if (_parserInstance != null)
                           {
                               _parserInstance.Cancel();
                               _parserInstance = null;
                               ProgressDescriptionByNicks = string.Empty;
                               ProgressParseByNicks = 0;
                           }
                           else
                           {
                               var errorProvider = new ErrorsProcessing.ErrorsLoader();
                               var error = errorProvider.GetErrorById(4);
                               MessageBox.Show(error.ErrorText, error.Caption, MessageBoxButton.OK);
                           }
                       }));
            }
        }

        public Command SelectedUsersWithPhoneNumber
        {
            get
            {
                return _selectedUsersWithPhoneNumber ??
                       (_selectedUsersWithPhoneNumber = new Command((obj) =>
                       {
                           _selectedWihtPhoneNumber = !_selectedWihtPhoneNumber;
                           ParsedUsers = _parsedUsers;
                       }));
            }
        }

        public Command ClearResult
        {
            get
            {
                return _clearResults ??
                       (_clearResults = new Command(obj => ParsedUsers = null));
            }
        }

        public Command SaveFile
        {
            get
            {
                return _saveFile ??
                       (_saveFile = new Command((obj) =>
                       {
                           try
                           {
                               var saveDialogWindow = new SaveFileDialog();
                               saveDialogWindow.Filter = "CSV file(*.csv) | *.csv";
                               if (saveDialogWindow.ShowDialog() == true)
                               {
                                   if (!saveDialogWindow.CheckPathExists)
                                   {
                                       _logger.Warning($"Path is already exist.");
                                       var errorProvider = new ErrorsProcessing.ErrorsLoader();
                                       var error = errorProvider.GetErrorById(5);
                                       MessageBox.Show(error.ErrorText, error.Caption, MessageBoxButton.OK);
                                   }

                                   var fileName = saveDialogWindow.FileName;

                                   var exporter = new CSVExporter();

                                   var settings = new ExportSettings(
                                       biography: Biography,
                                       blockedByView: BlockedByView,
                                       countryBlock: CountryBlock,
                                       externalUrl: ExternalUrl,
                                       externalUrlShimmed: ExternalUrlShimmed,
                                       followers: Followers,
                                       following: Following,
                                       fullName: FullName,
                                       id: Id,
                                       isPrivate: IsPrivate,
                                       isVerified: IsVerified,
                                       profilePictureUrl: ProfilePictureUrl,
                                       profilePictureHdUrl: ProfilePictureHdUrl,
                                       userName: UserName,
                                       phoneNumber: PhoneNumber);

                                   exporter.ExportTo(ParsedUsers, settings, Path.GetFullPath(fileName));
                               }
                           }
                           catch (Exception exc)
                           {
                               _logger.Exception(exc);
                           }
                       }));
            }
        }

        public Command ParseByNickNames
        {
            get
            {
                return _parseByNicknames ??
                       (_parseByNicknames = new Command(async (obj) =>
                       {
                           try
                           {
                               if(obj is TextBox textBox)
                               { 
                                   var listNicks = textBox.Text.Trim('\n').Split('\n');
                                   _logger.Info($"Run parser on {listNicks.Length}.");
                                   _parserInstance = new Parser.Parser { Delay = DelayValue };
                                   _parserInstance.ProcessParse += OnProccesParserListenerByNicks;

                                   ParsedUsers = await _parserInstance.ParseFollowersAsync(listNicks);

                                   _logger.Info($"Finden users {ParsedUsers.Count()}.");
                               }
                               else
                               {
                                   MessageBox.Show("It is not string");
                               }
                           }
                           catch (Exception e)
                           {
                               _logger.Exception(e);
                           }
                       }));
            }
        }

        #endregion

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnProccesParserListenerByNicks(object sender, ParserProcessEventArgs args)
        {
            ProgressParseByNicks = (args.ProcessedItem * 100) / args.TotalItem;
            ProgressDescriptionByNicks = $"{args.ProcessedItem} out of {args.TotalItem}";

            if (ProgressParseByNicks == 100)
            {
                ProgressParseByNicks = 0;
                ProgressDescriptionByNicks = string.Empty;
                _parserInstance = null;
                MessageBox.Show($"Parsing was done. Jarser found {args.TotalItem}.");
            }
        }

        private void OnProcessParseListener(object sender, ParserProcessEventArgs args)
        {
            int percent = (args.ProcessedItem * 100) / args.TotalItem;
            ProgressParse = percent;
            ProgressDescription = $"{args.ProcessedItem} out of {args.TotalItem}";
            if (percent == 100)
            {
                ProgressParse = 0;
                ProgressDescription = string.Empty;
                _parserInstance = null;
                MessageBox.Show($"Parsing was done. Jarser found {args.TotalItem}.");
            }
        }
    }
}
