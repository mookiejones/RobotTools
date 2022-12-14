using System;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using miRobotEditor.Core.Interfaces;

namespace miRobotEditor.Core.Classes
{
    public class Variable : ViewModelBase, IVariable
    {
        #region IsSelected

        /// <summary>
        ///     The <see cref="IsSelected" /> property's name.
        /// </summary>
        public const string IsSelectedPropertyName = "IsSelected";

        private bool _isSelected;

        /// <summary>
        ///     Sets and gets the IsSelected property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }

            set
            {
                if (_isSelected == value)
                {
                    return;
                }

                RaisePropertyChanging(IsSelectedPropertyName);
                _isSelected = value;
                RaisePropertyChanged(IsSelectedPropertyName);
            }
        }

        #endregion

        #region Icon

        /// <summary>
        ///     The <see cref="Icon" /> property's name.
        /// </summary>
        public const string IconPropertyName = "Icon";

        private BitmapImage _image;

        /// <summary>
        ///     Sets and gets the Icon property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public BitmapImage Icon
        {
            get { return _image; }

            set
            {
                if (_image == value)
                {
                    return;
                }

                RaisePropertyChanging(IconPropertyName);
                _image = value;
                RaisePropertyChanged(IconPropertyName);
            }
        }

        #endregion

        #region Description

        /// <summary>
        ///     The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _description = String.Empty;

        /// <summary>
        ///     Sets and gets the Description property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Description
        {
            get { return _description; }

            set
            {
                if (_description == value)
                {
                    return;
                }

                RaisePropertyChanging(DescriptionPropertyName);
                _description = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }

        #endregion

        #region Name

        /// <summary>
        ///     The <see cref="Name" /> property's name.
        /// </summary>
        private const string NamePropertyName = "Name";

        private string _name = String.Empty;

        /// <summary>
        ///     Sets and gets the Name property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Name
        {
            get { return _name; }

            set
            {
                if (_name == value)
                {
                    return;
                }

                RaisePropertyChanging(NamePropertyName);
                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

        #endregion

        #region Type

        /// <summary>
        ///     The <see cref="Type" /> property's name.
        /// </summary>
        public const string TypePropertyName = "Type";

        private string _type = String.Empty;

        /// <summary>
        ///     Sets and gets the Type property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Type
        {
            get { return _type; }

            set
            {
                if (_type == value)
                {
                    return;
                }

                RaisePropertyChanging(TypePropertyName);
                _type = value;
                RaisePropertyChanged(TypePropertyName);
            }
        }

        #endregion

        #region Path

        /// <summary>
        ///     The <see cref="Path" /> property's name.
        /// </summary>
        public const string PathPropertyName = "Path";

        private string _path = String.Empty;

        /// <summary>
        ///     Sets and gets the Path property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Path
        {
            get { return _path; }

            set
            {
                if (_path == value)
                {
                    return;
                }

                RaisePropertyChanging(PathPropertyName);
                _path = value;
                RaisePropertyChanged(PathPropertyName);
            }
        }

        #endregion

        #region Value

        /// <summary>
        ///     The <see cref="Value" /> property's name.
        /// </summary>
        public const string ValuePropertyName = "Value";

        private string _value = String.Empty;

        /// <summary>
        ///     Sets and gets the Value property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Value
        {
            get { return _value; }

            set
            {
                if (_value == value)
                {
                    return;
                }

                RaisePropertyChanging(ValuePropertyName);
                _value = value;
                RaisePropertyChanged(ValuePropertyName);
            }
        }

        #endregion

        #region Comment

        /// <summary>
        ///     The <see cref="Comment" /> property's name.
        /// </summary>
        public const string CommentPropertyName = "Comment";

        private string _comment = String.Empty;

        /// <summary>
        ///     Sets and gets the Comment property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Comment
        {
            get { return _comment; }

            set
            {
                if (_comment == value)
                {
                    return;
                }

                RaisePropertyChanging(CommentPropertyName);
                _comment = value;
                RaisePropertyChanged(CommentPropertyName);
            }
        }

        #endregion

        #region Declaration

        /// <summary>
        ///     The <see cref="Declaration" /> property's name.
        /// </summary>
        public const string DeclarationPropertyName = "Declaration";

        private string _declaration = String.Empty;

        /// <summary>
        ///     Sets and gets the Declaration property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Declaration
        {
            get { return _declaration; }

            set
            {
                if (_declaration == value)
                {
                    return;
                }

                RaisePropertyChanging(DeclarationPropertyName);
                _declaration = value;
                RaisePropertyChanged(DeclarationPropertyName);
            }
        }

        #endregion

        #region Offset

        /// <summary>
        ///     The <see cref="Offset" /> property's name.
        /// </summary>
        public const string OffsetPropertyName = "Offset";

        private int _offset = -1;

        /// <summary>
        ///     Sets and gets the Offset property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int Offset
        {
            get { return _offset; }

            set
            {
                if (_offset == value)
                {
                    return;
                }

                RaisePropertyChanging(OffsetPropertyName);
                _offset = value;
                RaisePropertyChanged(OffsetPropertyName);
            }
        }

        #endregion
    }
}