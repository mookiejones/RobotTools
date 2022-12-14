/*
 * Created by SharpDevelop.
 * User: cberman
 * Date: 4/11/2013
 * Time: 6:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace miRobotEditor.ViewModels
{
    // ReSharper disable UnusedMember.Local

	/// <summary>
	/// Description of KUKAObjectBrowserViewModel.
	/// </summary>
	public class KUKAObjectBrowserViewModel:ViewModelBase
	{
		public class FunctionClass
		{
			string Name{get;set;}
			string Type {get;set;}
			string Path{get;set;}
			string IsGlobal{get;set;}
			string Info{get;set;}
		}
		public class VariableClass
			{
			string Name{get;set;}
			string Type {get;set;}
			string Path{get;set;}
			string IsGlobal{get;set;}
			string Info{get;set;}
		}
		public class EnumClass
			{
			string Name{get;set;}
			string Type {get;set;}
			string Path{get;set;}
			string IsGlobal{get;set;}
			string Info{get;set;}
		}
		public class StructureClass
			{
			string Name{get;set;}
			string Type {get;set;}
			string Path{get;set;}
			string IsGlobal{get;set;}
			string Info{get;set;}
		}

        // ReSharper restore UnusedMember.Local


	    readonly ObservableCollection<FunctionClass> _functionItems = new ObservableCollection<FunctionClass>();
	    readonly ReadOnlyObservableCollection<FunctionClass> _readonlyFunctionItems = null;
         public ReadOnlyObservableCollection<FunctionClass> FunctionItems { get { return _readonlyFunctionItems ?? new ReadOnlyObservableCollection<FunctionClass>(_functionItems); } }

		private readonly ObservableCollection<VariableClass> _variableItems = new ObservableCollection<VariableClass>();
	    readonly ReadOnlyObservableCollection<VariableClass> _readonlyVariableItems = null;
        public ReadOnlyObservableCollection<VariableClass> VariableItems { get { return _readonlyVariableItems ?? new ReadOnlyObservableCollection<VariableClass>(_variableItems); } }


		private readonly ObservableCollection<EnumClass> _enumItems = new ObservableCollection<EnumClass>();
	    readonly ReadOnlyObservableCollection<EnumClass> _readonlyEnumItems = null;
        public ReadOnlyObservableCollection<EnumClass> EnumItems { get { return _readonlyEnumItems ?? new ReadOnlyObservableCollection<EnumClass>(_enumItems); } }


		private readonly ObservableCollection<StructureClass> _structureItems = new ObservableCollection<StructureClass>();
	    readonly ReadOnlyObservableCollection<StructureClass> _readonlyStructureItems = null;
        public ReadOnlyObservableCollection<StructureClass> StructureItems { get { return _readonlyStructureItems ?? new ReadOnlyObservableCollection<StructureClass>(_structureItems); } }
		
		public static KUKAObjectBrowserViewModel Instance{get;set;}
		
		private  RelayCommand _clearFilterCommand;

        public  ICommand ClearFilterCommand
        {
        	get { 
        		return _clearFilterCommand ?? (_clearFilterCommand = new RelayCommand(() => FilterText = String.Empty, () => (!String.IsNullOrEmpty(FilterText)))); }
        }
        
		
        
        #region SelectedItem
        /// <summary>
        /// The <see cref="SelectedItem" /> property's name.
        /// </summary>
        private const string SelectedItemPropertyName = "SelectedItem";

        private ListViewItem _selectedItem = new ListViewItem();

        /// <summary>
        /// Sets and gets the SelectedItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ListViewItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if (_selectedItem == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedItemPropertyName);
                _selectedItem = value;
                RaisePropertyChanged(SelectedItemPropertyName);
            }
        }
        #endregion
        
        #region FilterText
        /// <summary>
        /// The <see cref="FilterText" /> property's name.
        /// </summary>
        private const string FilterTextPropertyName = "FilterText";

        private string _filterText = String.Empty;

        /// <summary>
        /// Sets and gets the FilterText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FilterText
        {
            get
            {
                return _filterText;
            }

            set
            {
                if (_filterText == value)
                {
                    return;
                }

                RaisePropertyChanging(FilterTextPropertyName);
                _filterText = value;
                RaisePropertyChanged(FilterTextPropertyName);
            }
        }
        #endregion
        
        #region Functions
        /// <summary>
        /// The <see cref="Functions" /> property's name.
        /// </summary>
        private const string FunctionsPropertyName = "Functions";

        private string _functions = "2";

        /// <summary>
        /// Sets and gets the Functions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Functions
        {
            get
            {
                return _functions;
            }

            set
            {
                if (_functions == value)
                {
                    return;
                }

                RaisePropertyChanging(FunctionsPropertyName);
                _functions = value;
                RaisePropertyChanged(FunctionsPropertyName);
            }
        }
        #endregion

		private string _variableitems = "0";
		public string VariablesItems{get{return _variableitems;}set{_variableitems=value;RaisePropertyChanged("VariableItems");}}
		
		public KUKAObjectBrowserViewModel()
		{
			Instance=this;
		}
	}
}
