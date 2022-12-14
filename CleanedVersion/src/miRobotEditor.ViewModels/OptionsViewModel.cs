/*
 * Created by SharpDevelop.
 * User: cberman
 * Date: 4/8/2013
 * Time: 8:41 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace miRobotEditor.ViewModels
{
	/// <summary>
	/// Description of OptionsViewModel.
	/// </summary>
	public class OptionsViewModel:ViewModelBase
	{
		
		public OptionsViewModel Instance{get;set;}
		public OptionsViewModel()
		{
			Instance=this;
		}
		
		
		
		 private  RelayCommand _applyCommand;
        public  ICommand ApplyCommand
        {
            get { return _applyCommand ?? (_applyCommand = new RelayCommand(Apply)); }
        }
        
        private  RelayCommand _okCommand;
        public  ICommand OkCommand
        {
            get { return _okCommand ?? (_okCommand = new RelayCommand(Ok)); }
        }
        
        private  RelayCommand _cancelCommand;
        public  ICommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(Cancel)); }
        }

	    static void Apply(){}
	    static void Ok(){}
	    static void Cancel(){}

        
	}
}
