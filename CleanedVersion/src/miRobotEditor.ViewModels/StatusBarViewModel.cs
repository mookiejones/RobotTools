using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace miRobotEditor.ViewModels
{
   public sealed class StatusBarViewModel:ViewModelBase
   {


       #region Constructor
       public StatusBarViewModel()
       {


           //Get Initial Key Status
          GetInitialKeyState();
       }
       #endregion

       #region Commands

       private RelayCommand<KeyEventArgs> _keyPressedCommand;
       public ICommand KeyPressedCommand
       {
           get
           {
               return _keyPressedCommand ?? (_keyPressedCommand = new RelayCommand<KeyEventArgs>(ManageKeys, param => true));
           }
       }

       #endregion

       #region Static Properties
           private static StatusBarViewModel _instance;
           public static StatusBarViewModel Instance { get { return _instance ?? (_instance=new StatusBarViewModel()); } }
       #endregion

       #region Status Bar Items

           
           #region IsScrollPressed
           /// <summary>
           /// The <see cref="IsScrollPressed" /> property's name.
           /// </summary>
           private const string IsScrollPressedPropertyName = "IsScrollPressed";

           private bool _isScrollPressed = false;

           /// <summary>
           /// Sets and gets the IsScrollPressed property.
           /// Changes to that property's value raise the PropertyChanged event. 
           /// </summary>
           public bool IsScrollPressed
           {
               get
               {
                   return _isScrollPressed;
               }

               set
               {
                   if (_isScrollPressed == value)
                   {
                       return;
                   }

                   RaisePropertyChanging(IsScrollPressedPropertyName);
                   _isScrollPressed = value;
                   RaisePropertyChanged(IsScrollPressedPropertyName);
               }
           }
           #endregion

           
           #region IsNumPressed
           /// <summary>
           /// The <see cref="IsNumPressed" /> property's name.
           /// </summary>
           private const string IsNumPressedPropertyName = "IsNumPressed";

           private bool _isNumPressed = false;

           /// <summary>
           /// Sets and gets the IsNumPressed property.
           /// Changes to that property's value raise the PropertyChanged event. 
           /// </summary>
           public bool IsNumPressed
           {
               get
               {
                   return _isNumPressed;
               }

               set
               {
                   if (_isNumPressed == value)
                   {
                       return;
                   }

                   RaisePropertyChanging(IsNumPressedPropertyName);
                   _isNumPressed = value;
                   RaisePropertyChanged(IsNumPressedPropertyName);
               }
           }
           #endregion

           
           #region IsInsPressed
           /// <summary>
           /// The <see cref="IsInsPressed" /> property's name.
           /// </summary>
           private const string IsInsPressedPropertyName = "IsInsPressed";

           private bool _isInsPressed = false;

           /// <summary>
           /// Sets and gets the IsInsPressed property.
           /// Changes to that property's value raise the PropertyChanged event. 
           /// </summary>
           public bool IsInsPressed
           {
               get
               {
                   return _isInsPressed;
               }

               set
               {
                   if (_isInsPressed == value)
                   {
                       return;
                   }

                   RaisePropertyChanging(IsInsPressedPropertyName);
                   _isInsPressed = value;
                   RaisePropertyChanged(IsInsPressedPropertyName);
               }
           }
           #endregion

           
           #region IsCapsPressed
           /// <summary>
           /// The <see cref="IsCapsPressed" /> property's name.
           /// </summary>
           private const string IsCapsPressedPropertyName = "IsCapsPressed";

           private bool _isCapsPressed = false;

           /// <summary>
           /// Sets and gets the IsCapsPressed property.
           /// Changes to that property's value raise the PropertyChanged event. 
           /// </summary>
           public bool IsCapsPressed
           {
               get
               {
                   return _isCapsPressed;
               }

               set
               {
                   if(_isCapsPressed == value)
                   {
                       return;
                   }

                   RaisePropertyChanging(IsCapsPressedPropertyName);
                    _isCapsPressed = value;
                   RaisePropertyChanged(IsCapsPressedPropertyName);
               }
           }
           #endregion
   
       
        #endregion

        public void ManageKeys( KeyEventArgs e)
        {
            if (e == null) return;
            switch (e.Key)
            {
                case Key.Capital:
                    IsCapsPressed = e.IsToggled;
                    break;
                case Key.Insert:
                    IsInsPressed = e.IsToggled;
                    break;
                case Key.NumLock:
                    IsNumPressed = e.IsToggled;
                    break;
                case Key.Scroll:
                    IsScrollPressed = e.IsToggled;
                    break;
            }
        }


       void GetInitialKeyState()
        {/*
            IsCapsPressed = NativeMethods.GetKeyState((int)VKeyStates.CapsKey) != 0;
            IsInsPressed = NativeMethods.GetKeyState((int)VKeyStates.InsKey) != 0;
            IsNumPressed = NativeMethods.GetKeyState((int)VKeyStates.NumKey) != 0;
            IsScrollPressed = NativeMethods.GetKeyState((int)VKeyStates.ScrollKey) != 0;
          * */
        }
        private enum VKeyStates
        {
            /// <summary>
            /// the caps lock key
            /// </summary>
            CapsKey = 0x14,
            /// <summary>
            /// the numlock key
            /// </summary>
            NumKey = 0x90,
            /// <summary>
            /// the scroll key
            /// </summary>
            ScrollKey = 0x91,
            /// <summary>
            /// the ins key
            /// </summary>
            InsKey = 0x2d
        }

    }
}
