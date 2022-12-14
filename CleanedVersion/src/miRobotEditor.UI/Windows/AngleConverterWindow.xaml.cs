using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using miRobotEditor.Core.Classes.AngleConverter;
using miRobotEditor.Core.Enums;
using miRobotEditor.UI.Controls;

namespace miRobotEditor.UI.Windows
{
    /// <summary>
    /// Interaction logic for AngleConverterWindow.xaml
    /// </summary>
    public sealed partial class AngleConverterWindow : UserControl
    {
        public AngleConverterWindow()
        {
            InitializeComponent();

        }
    }

    public sealed class AngleConverterModel : DependencyObject
    {
        public const string ToolContentId = "AngleConverterTool";

        public static CartesianEnum CartesianType { get; set; }


   
#region InputItems        


public ValueBoxModel InputItems
{
    get { return (ValueBoxModel)GetValue(InputItemsProperty); }
    set { SetValue(InputItemsProperty, value);
        ;
    }
}

// Using a DependencyProperty as the backing store for InputItems.  This enables animation, styling, binding, etc...
public static readonly DependencyProperty InputItemsProperty = 
    DependencyProperty.Register("InputItems", typeof(ValueBoxModel), typeof(AngleConverterModel));

#endregion
#region OutputItems
        

public ValueBoxModel OutputItems
{
    get { return (ValueBoxModel)GetValue(OutputItemsProperty); }
    set { SetValue(OutputItemsProperty, value);
       ;
    }
}

// Using a DependencyProperty as the backing store for OutputItems.  This enables animation, styling, binding, etc...
public static readonly DependencyProperty OutputItemsProperty = 
    DependencyProperty.Register("OutputItems", typeof(ValueBoxModel), typeof(AngleConverterModel));


#endregion
  
        #region Constructor
public AngleConverterModel()
        {
            InputItems = new ValueBoxModel();
            OutputItems = new ValueBoxModel();

            InputItems.ItemsChanged+=Convert;
           OutputItems.ItemsChanged +=Convert;

        }
        #endregion

        #region Fields

            private bool _isConverting;
            private RotationMatrix3D _rotationMatrix = RotationMatrix3D.Identity();
        #endregion

          
    public void Convert(object sender,ItemsChangedEventArgs e)
    {
        if ((InputItems == null | OutputItems == null)) return;

        if (_isConverting) return;
        _isConverting = true;
        var result = new Vector3D();
        var scalar = 0.0;
        var rotationMatrix = new Quaternion();

        switch (InputItems.SelectedItem)
        {
            case CartesianEnum.ABB_Quaternion:
                rotationMatrix = new Quaternion(InputItems.V1, InputItems.V2, InputItems.V3, InputItems.V4);
//TODO Come Back to this                    this.rotationMatrix = this.rotationMatrix;
                break;

            case CartesianEnum.Roll_Pitch_Yaw:
                _rotationMatrix = RotationMatrix3D.FromRPY(InputItems.V1, InputItems.V2, InputItems.V3);
                break;

            case CartesianEnum.Axis_Angle:
                _rotationMatrix = RotationMatrix3D.RotateAroundVector(new Vector3D(InputItems.V1, InputItems.V2, InputItems.V3), InputItems.V4);
                break;

            case CartesianEnum.Kuka_ABC:
                _rotationMatrix = RotationMatrix3D.FromABC(InputItems.V1, InputItems.V2, InputItems.V3);
                break;

            case CartesianEnum.Euler_ZYZ:
                _rotationMatrix = RotationMatrix3D.FromEulerZYZ(InputItems.V1, InputItems.V2, InputItems.V3);
                break;
        }

        switch (OutputItems.SelectedItem)
        {
            case CartesianEnum.ABB_Quaternion:
                rotationMatrix = (Quaternion) _rotationMatrix;
                result = rotationMatrix.Vector;
                scalar = rotationMatrix.Scalar;
                break;

            case CartesianEnum.Roll_Pitch_Yaw:
                result = _rotationMatrix.RPY;
                break;

            case CartesianEnum.Axis_Angle:
                result = _rotationMatrix.RotationAxis();
                scalar = _rotationMatrix.RotationAngle();
                break;

            case CartesianEnum.Kuka_ABC:
                result = _rotationMatrix.ABC;
                break;

            case CartesianEnum.Euler_ZYZ:
                result = _rotationMatrix.EulerZYZ;
                break;

            case CartesianEnum.Alpha_Beta_Gamma:
                result = _rotationMatrix.ABG;
                break;
        }
        var str = rotationMatrix.ToString("F3");
        if ((Matrix != null) && (Matrix != str))
            Matrix = str;

        WriteValues(result, 0.0, false);           

        if (OutputItems.SelectedItem == CartesianEnum.ABB_Quaternion)
            WriteValues(result, scalar, true);
        if (OutputItems.SelectedItem==CartesianEnum.Axis_Angle)
            OutputItems.V4 = scalar;

        _isConverting = false;
    }


    private void WriteValues(Vector3D result, double scalar, bool isScalar)
    {
        switch (isScalar)
        {
            case false:
                OutputItems.V1 = result.X;
                OutputItems.V2 = result.Y;
                OutputItems.V3 = result.Z;
                break;

            case true:
                OutputItems.V1 = scalar;
                OutputItems.V2 = result.X;
                OutputItems.V3 = result.Y;
                OutputItems.V4 = result.Z;
                break;
        }
    }

    // Properties
        private double EPSILON
    {
        get
        {
            return double.Epsilon;
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public string Error
    {
        get
        {
            return null;
        }
    }

    #region Matrix


    public string Matrix
    {
        get { return (string)GetValue(MatrixProperty); }
        set { SetValue(MatrixProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Matrix.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MatrixProperty =
        DependencyProperty.Register("Matrix", typeof(string), typeof(AngleConverterModel), new PropertyMetadata(""));

    
    #endregion


    // Nested Types
    }
}
