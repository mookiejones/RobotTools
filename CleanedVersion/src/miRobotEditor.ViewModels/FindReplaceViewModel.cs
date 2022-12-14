using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace miRobotEditor.ViewModels
{
    public class FindReplaceViewModel:ViewModelBase
    {

        #region  Commands
        private static RelayCommand _findpreviouscommand;
        public static ICommand FindPreviousCommand
        {
            get
            {
                return _findpreviouscommand ??
                       (_findpreviouscommand = new RelayCommand(FindPrevious));
            }
        }
        private static RelayCommand _findnextcommand;
        public static ICommand FindNextCommand
        {
            get { return _findnextcommand ?? (_findnextcommand = new RelayCommand(FindNext)); }
        }
        private static RelayCommand _replacecommand;
        public static ICommand ReplaceCommand
        {
            get
            {
                return _replacecommand ??
                       (_replacecommand = new RelayCommand(Replace));
            }
        }
        private static RelayCommand _replaceallcommand;
        public static ICommand ReplaceAllCommand
        {
            get
            {
                return _replaceallcommand ??
                       (_replaceallcommand = new RelayCommand(ReplaceAll));
            }
        }
        private static RelayCommand _highlightallcommand;
        public static ICommand HighlightAllCommand
        {
            get
            {
                return _highlightallcommand ??
                       (_highlightallcommand = new RelayCommand(HighlightAll));
            }
        }
        private RelayCommand _findallcommand;
        public ICommand FindAllCommand
        {
            get
            {
                return _findallcommand ??
                       (_findallcommand = new RelayCommand(FindAll));
            }
        }
        #endregion

        private static FindReplaceViewModel _instance;
        public static FindReplaceViewModel Instance
        {
            get { return _instance ?? (_instance = new FindReplaceViewModel()); }
            set { _instance = value; }
        }

        #region Properties

        
        #region UseRegex
        /// <summary>
        /// The <see cref="UseRegex" /> property's name.
        /// </summary>
        private const string UseRegexPropertyName = "UseRegex";

        private bool _useRegex = false;

        /// <summary>
        /// Sets and gets the UseRegex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool UseRegex
        {
            get
            {
                return _useRegex;
            }

            set
            {
                if (_useRegex == value)
                {
                    return;
                }

                RaisePropertyChanging(UseRegexPropertyName);
                _useRegex = value;
                RaisePropertyChanged(UseRegexPropertyName);
            }
        }
        #endregion
        
        #region MatchCase
        /// <summary>
        /// The <see cref="MatchCase" /> property's name.
        /// </summary>
        private const string MatchCasePropertyName = "MatchCase";

        private bool _matchCase = false;

        /// <summary>
        /// Sets and gets the MatchCase property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool MatchCase
        {
            get
            {
                return _matchCase;
            }

            set
            {
                if (_matchCase == value)
                {
                    return;
                }

                RaisePropertyChanging(MatchCasePropertyName);
                _matchCase = value;
                RaisePropertyChanged(MatchCasePropertyName);
            }
        }
        #endregion

        
        #region MatchWholeWord
        /// <summary>
        /// The <see cref="MatchWholeWord" /> property's name.
        /// </summary>
        private const string MatchWholeWordPropertyName = "MatchWholeWord";

        private bool _matchWholeWord = false;

        /// <summary>
        /// Sets and gets the MatchWholeWord property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool MatchWholeWord
        {
            get
            {
                return _matchWholeWord;
            }

            set
            {
                if (_matchWholeWord == value)
                {
                    return;
                }

                RaisePropertyChanging(MatchWholeWordPropertyName);
                _matchWholeWord = value;
                RaisePropertyChanged(MatchWholeWordPropertyName);
            }
        }
        #endregion
    
        public Regex RegexPattern
        {
            get
            {
                var pattern = UseRegex == false ? Regex.Escape(LookFor) : LookFor;
                var options = MatchCase ? 0 : 1;
                return new Regex(pattern, (RegexOptions)options);
            }
        }

        public string RegexString
        {
            get
            {
                return UseRegex == false ? Regex.Escape(LookFor) : LookFor;
            }
        }

        
        #region LookFor
        /// <summary>
        /// The <see cref="LookFor" /> property's name.
        /// </summary>
        private const string LookForPropertyName = "LookFor";

        private string _lookFor = String.Empty;

        /// <summary>
        /// Sets and gets the LookFor property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LookFor
        {
            get
            {
                return _lookFor;
            }

            set
            {
                if (_lookFor == value)
                {
                    return;
                }

                RaisePropertyChanging(LookForPropertyName);
                _lookFor = value;
                RaisePropertyChanged(LookForPropertyName);
            }
        }
        #endregion

        
        #region ReplaceWith
        /// <summary>
        /// The <see cref="ReplaceWith" /> property's name.
        /// </summary>
        private const string ReplaceWithPropertyName = "ReplaceWith";

        private string _replaceWith = String.Empty;

        /// <summary>
        /// Sets and gets the ReplaceWith property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ReplaceWith
        {
            get
            {
                return _replaceWith;
            }

            set
            {
                if (_replaceWith == value)
                {
                    return;
                }

                RaisePropertyChanging(ReplaceWithPropertyName);
                _replaceWith = value;
                RaisePropertyChanged(ReplaceWithPropertyName);
            }
        }
        #endregion


        
        #region SearchResult
        /// <summary>
        /// The <see cref="SearchResult" /> property's name.
        /// </summary>
        private const string SearchResultPropertyName = "SearchResult";

        private string _searchResult = String.Empty;

        /// <summary>
        /// Sets and gets the SearchResult property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SearchResult
        {
            get
            {
                return _searchResult;
            }

            set
            {
                if (_searchResult == value)
                {
                    return;
                }

                RaisePropertyChanging(SearchResultPropertyName);
                _searchResult = value;
                RaisePropertyChanged(SearchResultPropertyName);
            }
        }
        #endregion

        #endregion

        private static void FindPrevious()
        {
            throw new NotImplementedException();
        }
        private static void FindNext()
        {
            throw new NotImplementedException();
//            Workspace.Instance.ActiveEditor.TextBox.FindText();
        }

        private static void Replace()
        {
            throw new NotImplementedException();
//            Workspace.Instance.ActiveEditor.TextBox.ReplaceText();
        }

        private static void ReplaceAll()
        {
            throw new NotImplementedException();
        }

        private static void HighlightAll()
        {
            throw new NotImplementedException();
        }

        private static void FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
