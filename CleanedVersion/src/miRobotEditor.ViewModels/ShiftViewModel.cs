using miRobotEditor.Core.Classes;

namespace miRobotEditor.ViewModels
{
    public class ShiftViewModel:ToolViewModel
    {
        public ShiftViewModel() : base()
        {

        }


        
        #region OldValues
        /// <summary>
        /// The <see cref="OldValues" /> property's name.
        /// </summary>
        private const string OldValuesPropertyName = "OldValues";

        private CartesianPosition _oldValues = new CartesianPosition{ Header="Old Values"};

        /// <summary>
        /// Sets and gets the OldValues property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CartesianPosition OldValues
        {
            get
            {
                return _oldValues;
            }

            set
            {
                if (_oldValues == value)
                {
                    return;
                }

                RaisePropertyChanging(OldValuesPropertyName);
                _oldValues = value;
                RaisePropertyChanged(OldValuesPropertyName);
            }
        }
        #endregion


        
        #region NewValues
        /// <summary>
        /// The <see cref="NewValues" /> property's name.
        /// </summary>
        private const string NewValuesPropertyName = "NewValues";

        private CartesianPosition _newValues = new CartesianPosition{Header="New Values"};

        /// <summary>
        /// Sets and gets the NewValues property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CartesianPosition NewValues
        {
            get
            {
                return _newValues;
            }

            set
            {
                if (_newValues == value)
                {
                    return;
                }

                RaisePropertyChanging(NewValuesPropertyName);
                _newValues = value;
                RaisePropertyChanged(NewValuesPropertyName);
            }
        }
        #endregion


        
        #region DiffValues
        /// <summary>
        /// The <see cref="DiffValues" /> property's name.
        /// </summary>
        private const string DiffValuesPropertyName = "DiffValues";

        private CartesianPosition _diffValues = new CartesianPosition{Header="Difference"};

        /// <summary>
        /// Sets and gets the DiffValues property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CartesianPosition DiffValues
        {
            get
            {
                return _diffValues;
            }

            set
            {
                if (_diffValues == value)
                {
                    return;
                }

                RaisePropertyChanging(DiffValuesPropertyName);
                _diffValues = value;
                RaisePropertyChanged(DiffValuesPropertyName);
            }
        }
        #endregion
      

       
        private static ShiftViewModel _instance;
        public static ShiftViewModel Instance { get { return _instance ?? new ShiftViewModel(); }
            set
            {
                _instance = value;
             
            }
        }
    }
}
