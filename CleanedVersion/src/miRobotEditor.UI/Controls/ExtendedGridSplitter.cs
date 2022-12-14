using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
namespace miRobotEditor.UI.Controls
{

    /// An updated version of the standard ExtendedGridSplitter control that includes a centered handle
    /// which allows complete collapsing and expanding of the appropriate grid column or row.
    /// </summary>
    [Localizable(false), TemplatePart(Name = ElementHorizontalHandleName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = ElementLabelName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = ElementSwitcharrowHandleName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = ElementVerticalHandleName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = ElementHorizontalTemplateName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementVerticalTemplateName, Type = typeof(FrameworkElement))]
    public partial class ExtendedGridSplitter : GridSplitter
    {

        public static bool Animating = false;



        #region TemplateParts

        private const string ElementLabelName = "LabelHandle";
        private const string ElementSwitcharrowHandleName = "SwitchArrows";
        private const string ElementHorizontalHandleName = "HorizontalGridSplitterHandle";
        private const string ElementVerticalHandleName = "VerticalGridSplitterHandle";
        private const string ElementHorizontalTemplateName = "HorizontalTemplate";
        private const string ElementVerticalTemplateName = "VerticalTemplate";
        private const string ElementGridsplitterBackground = "GridSplitterBackground";


        public ToggleButton GridSplitterButton { get { return _elementVerticalGridSplitterButton; } set { _elementVerticalGridSplitterButton = value; } }
        private static ToggleButton _elementHorizontalGridSplitterButton;
        private static ToggleButton _elementVerticalGridSplitterButton;


        private Rectangle _elementGridSplitterBackground;

        #endregion


        #region Dependency Properties

        /// <summary>
        /// Gets or sets a value that indicates the CollapseMode.
        /// </summary>
        public GridSplitterCollapseMode CollapseMode
        {
            get { return (GridSplitterCollapseMode)GetValue(CollapseModeProperty); }
            set { SetValue(CollapseModeProperty, value); }
        }
        /// <summary>
        /// Identifies the CollapseMode dependency property
        /// </summary>
        public static readonly DependencyProperty CollapseModeProperty = DependencyProperty.Register("CollapseMode", typeof(GridSplitterCollapseMode), typeof(ExtendedGridSplitter), new PropertyMetadata(GridSplitterCollapseMode.None, OnCollapseModePropertyChanged));
        /// <summary>
        /// Gets or sets the style that customizes the appearance of the horizontal handle 
        /// that is used to expand and collapse the ExtendedGridSplitter.
        /// </summary>
        public Style SwitchArrowStyle
        {
            get { return (Style)GetValue(SwitchArrowStyleProperty); }
            set { SetValue(SwitchArrowStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style that customizes the appearance of the horizontal handle 
        /// that is used to expand and collapse the ExtendedGridSplitter.
        /// </summary>
        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style that customizes the appearance of the horizontal handle 
        /// that is used to expand and collapse the ExtendedGridSplitter.
        /// </summary>
        public Color FocusColor
        {
            get { return (Color)GetValue(FocusColorProperty); }
            set { SetValue(FocusColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style that customizes the appearance of the horizontal handle 
        /// that is used to expand and collapse the ExtendedGridSplitter.
        /// </summary>
        public Color UnfocusColor
        {
            get { return (Color)GetValue(UnfocusColorProperty); }
            set { SetValue(UnfocusColorProperty, value); }
        }


        /// <summary>
        /// Gets or sets the style that customizes the appearance of the horizontal handle 
        /// that is used to expand and collapse the ExtendedGridSplitter.
        /// </summary>
        public Style HorizontalHandleStyle
        {
            get { return (Style)GetValue(HorizontalHandleStyleProperty); }
            set { SetValue(HorizontalHandleStyleProperty, value); }
        }

        /// <summary>
        /// Identifies the HorizontalHandleStyle dependency property
        /// </summary>
        public static readonly DependencyProperty HorizontalHandleStyleProperty = DependencyProperty.Register("HorizontalHandleStyle", typeof(Style), typeof(ExtendedGridSplitter), null);

        /// <summary>
        /// Identifies the HorizontalHandleStyle dependency property
        /// </summary>
        public static readonly DependencyProperty FocusColorProperty = DependencyProperty.Register("FocusColor", typeof(Color), typeof(ExtendedGridSplitter), null);

        /// <summary>
        /// Identifies the HorizontalHandleStyle dependency property
        /// </summary>
        public static readonly DependencyProperty UnfocusColorProperty = DependencyProperty.Register("UnfocusColor", typeof(Color), typeof(ExtendedGridSplitter), null);

        /// <summary>
        /// Identifies the HorizontalHandleStyle dependency property
        /// </summary>
        public static readonly DependencyProperty LabelStyleProperty = DependencyProperty.Register("LabelStyle", typeof(Style), typeof(ExtendedGridSplitter), null);

        /// <summary>
        /// Identifies the SwitchArrowStyle dependency property
        /// </summary>
        public static readonly DependencyProperty SwitchArrowStyleProperty = DependencyProperty.Register("SwitchArrowStyle", typeof(Style), typeof(ExtendedGridSplitter), null);

        ///<summary>
        /// Gets or sets the style that customizes the appearance of the vertical handle 
        /// that is used to expand and collapse the ExtendedGridSplitter.
        /// </summary>
        public Style VerticalHandleStyle
        {
            get { return (Style)GetValue(VerticalHandleStyleProperty); }
            set { SetValue(VerticalHandleStyleProperty, value); }
        }

        /// <summary>
        /// Identifies the VerticalHandleStyle dependency property
        /// </summary>
        public static readonly DependencyProperty VerticalHandleStyleProperty = DependencyProperty.Register("VerticalHandleStyle", typeof(Style), typeof(ExtendedGridSplitter), null);

        /// <summary>
        /// Gets or sets a value that indicates if the collapse and
        /// expanding actions should be animated.
        /// </summary>
        public bool IsAnimated
        {
            get { return (bool)GetValue(IsAnimatedProperty); }
            set { SetValue(IsAnimatedProperty, value); }
        }

        /// <summary>
        /// Identifies the VerticalHandleStyle dependency property
        /// </summary>
        public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register("IsAnimated", typeof(bool), typeof(ExtendedGridSplitter), null);

        /// <summary>
        /// Gets or sets a value that indicates if the target column is 
        /// currently collapsed.
        /// </summary>
        public bool IsCollapsed
        {
            get { return (bool)GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }

        /// <summary>
        /// Identifies the IsCollapsed dependency property
        /// </summary>
        public static readonly DependencyProperty IsCollapsedProperty = DependencyProperty.Register("IsCollapsed", typeof(bool), typeof(ExtendedGridSplitter), new PropertyMetadata(OnIsCollapsedPropertyChanged));

        /// <summary>
        /// The IsCollapsed property porperty changed handler.
        /// </summary>
        /// <param name="d">ExtendedGridSplitter that changed IsCollapsed.</param>
        /// <param name="e">An instance of DependencyPropertyChangedEventArgs.</param>
        private static void OnIsCollapsedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var s = d as ExtendedGridSplitter;

            var value = (bool)e.NewValue;
            if (s != null) s.OnIsCollapsedChanged(value);
        }

        /// <summary>
        /// The CollapseModeProperty property changed handler.
        /// </summary>
        /// <param name="d">ExtendedGridSplitter that changed IsCollapsed.</param>
        /// <param name="e">An instance of DependencyPropertyChangedEventArgs.</param>
        private static void OnCollapseModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var s = d as ExtendedGridSplitter;

            var value = (GridSplitterCollapseMode)e.NewValue;
            if (s != null) s.OnCollapseModeChanged(value);
        }

        #endregion

        #region Local Members

        private GridCollapseDirection _gridCollapseDirection = GridCollapseDirection.Rows;
        private GridLength _savedGridLength;
        private double _savedActualValue;
        private const double AnimationTimeMillis = 200;

        #endregion
        /// <summary>
        /// An enumeration that specifies the direction the ExtendedGridSplitter will
        /// be collapased (Rows or Columns).
        /// </summary>
        private enum GridCollapseDirection
        {
            Rows
        }

        public ExtendedGridSplitter()
        {
            // Set default values
            DefaultStyleKey = typeof(ExtendedGridSplitter);
            HorizontalAlignment = HorizontalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Stretch;
            Height = 20;
            CollapseMode = GridSplitterCollapseMode.Previous;
            //  LayoutUpdated += delegate { _gridCollapseDirection = GetCollapseDirection(); };
            Loaded += delegate
            {
                Collapse(); IsAnimated = true;
            };

            // All ExtendedGridSplitter visual states are handled by the parent GridSplitter class.
        }


        /// <summary>
        /// This method is called when the tempalte should be applied to the control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _elementHorizontalGridSplitterButton = GetTemplateChild(ElementHorizontalHandleName) as ToggleButton;
            _elementVerticalGridSplitterButton = GetTemplateChild(ElementVerticalHandleName) as ToggleButton;
            _elementGridSplitterBackground = GetTemplateChild(ElementGridsplitterBackground) as Rectangle;


            // Wire up the Checked and Unchecked events of the HorizontalGridSplitterHandle.
            if (_elementHorizontalGridSplitterButton != null)
            {
                _elementHorizontalGridSplitterButton.Checked += GridSplitterButtonChecked;
                _elementHorizontalGridSplitterButton.Unchecked += GridSplitterButtonUnchecked;
            }

            // Wire up the Checked and Unchecked events of the VerticalGridSplitterHandle.
            if (_elementVerticalGridSplitterButton != null)
            {
                _elementVerticalGridSplitterButton.Checked += GridSplitterButtonChecked;
                _elementVerticalGridSplitterButton.Unchecked += GridSplitterButtonUnchecked;
            }

            // Set default direction since we don't have all the components layed out yet.
            _gridCollapseDirection = GridCollapseDirection.Rows;
            // Directely call these events so design-time view updates appropriately
            OnCollapseModeChanged(CollapseMode);
            OnIsCollapsedChanged(IsCollapsed);
        }

        #region Protected Members

        /// <summary>
        /// Handles the property change event of the IsCollapsed property.
        /// </summary>
        /// <param name="isCollapsed">The new value for the IsCollapsed property.</param>
        protected virtual void OnIsCollapsedChanged(bool isCollapsed)
        {
            // Sets the target ToggleButton's IsChecked property equal
            // to the provided isCollapsed property.

            if (_elementVerticalGridSplitterButton != null)
                _elementVerticalGridSplitterButton.IsChecked = isCollapsed;
        }


        /// <summary>
        /// Handles the property change event of the CollapseMode property.
        /// </summary>
        /// <param name="collapseMode">The new value for the CollapseMode property.</param>
        protected virtual void OnCollapseModeChanged(GridSplitterCollapseMode collapseMode)
        {

            if (_elementVerticalGridSplitterButton != null)
                _elementVerticalGridSplitterButton.Visibility = Visibility.Visible;

            // Rotate the direction that the handle is facing depending on the CollapseMode.
            switch (collapseMode)
            {
                case GridSplitterCollapseMode.Previous:
                    if (_elementVerticalGridSplitterButton != null)
                    {
                        //                          if (Dispatcher.CheckAccess())
                        //                              _elementVerticalGridSplitterButton.RenderTransform.SetValue( ScaleTransform.ScaleXProperty, -1.0);
                        //                          else
                        //                             Dispatcher.BeginInvoke(new ChangeTransform(_elementVerticalGridSplitterButton.RenderTransform.SetValue),ScaleTransform.ScaleXProperty, -1.0));

                    }
                    break;
                case GridSplitterCollapseMode.Next:
                    if (_elementVerticalGridSplitterButton != null)
                    {
                        //                            if (Dispatcher.CheckAccess())
                        //                                _elementVerticalGridSplitterButton.RenderTransform.SetValue(ScaleTransform.ScaleXProperty, -1.0);
                        //                            else
                        //                                Dispatcher.BeginInvoke(new ChangeTransform(_elementVerticalGridSplitterButton.RenderTransform.SetValue),ScaleTransform.ScaleXProperty, 1.0));

                    }
                    break;
            }



        }

        #endregion

        #region Collapse and Expand Methods



        private static Grid _parentGrid;

        /// <summary>
        /// Collapses the target ColumnDefinition or RowDefinition.
        /// </summary>
        public void Collapse()
        {
            var parentGrid = Parent as Grid;
            if (parentGrid != null)
                _parentGrid = parentGrid;
            else
                parentGrid = _parentGrid;

            if (_gridCollapseDirection == GridCollapseDirection.Rows)
            {
                // Get the index of the row containing the splitter
                var splitterIndex = (int)GetValue(Grid.RowProperty);

                // Determing the curent CollapseMode
                if (CollapseMode == GridSplitterCollapseMode.Previous)
                {
                    // Save the next rows Height information
                    if (parentGrid != null)
                    {
                        _savedGridLength = parentGrid.RowDefinitions[splitterIndex + 1].Height;
                        _savedActualValue = parentGrid.RowDefinitions[splitterIndex + 1].ActualHeight;

                        // Collapse the next row
                        if (IsAnimated)
                            AnimateCollapse(parentGrid.RowDefinitions[splitterIndex + 1]);
                        else
                            parentGrid.RowDefinitions[splitterIndex + 1].SetValue(RowDefinition.HeightProperty, new GridLength(0));
                    }
                }
                else
                {
                    // Save the previous row's Height information
                    if (parentGrid != null)
                    {
                        _savedGridLength = parentGrid.RowDefinitions[splitterIndex + 1].Height;
                        _savedActualValue = parentGrid.RowDefinitions[splitterIndex + 1].ActualHeight;

                        // Collapse the previous row
                        if (IsAnimated)
                            AnimateCollapse(parentGrid.RowDefinitions[splitterIndex - 1]);
                        else
                            parentGrid.RowDefinitions[splitterIndex + 1].SetValue(RowDefinition.HeightProperty, new GridLength(0));
                    }
                }
            }
            IsCollapsed = true;

        }

        /// <summary>
        /// Expands the target ColumnDefinition or RowDefinition.
        /// </summary>
        public void Expand()
        {

            var parentGrid = Parent as Grid;
            if (parentGrid != null)
                _parentGrid = parentGrid;

            // Get the index of the row containing the splitter
            const int splitterIndex = 1;

            // Determine the curent CollapseMode
            if (CollapseMode == GridSplitterCollapseMode.Previous)
            {
                // Expand the next row
                if (IsAnimated)
                {
                    if (parentGrid != null) AnimateExpand(parentGrid.RowDefinitions[splitterIndex + 1]);
                }
                else if (parentGrid != null)
                    parentGrid.RowDefinitions[splitterIndex + 1].SetValue(RowDefinition.HeightProperty, _savedGridLength);
            }
            else
            {
                // Expand the previous row
                if (IsAnimated)
                {
                    if (parentGrid != null) AnimateExpand(parentGrid.RowDefinitions[splitterIndex - 1]);
                }
                else if (parentGrid != null)
                    parentGrid.RowDefinitions[splitterIndex - 1].SetValue(RowDefinition.HeightProperty, _savedGridLength);
            }

            IsCollapsed = false;
        }

        #endregion

        #region Event Handlers and Throwers

        // Define Collapsed and Expanded evenets
        public event EventHandler<EventArgs> Collapsed;
        public event EventHandler<EventArgs> Expanded;

        /// <summary>
        /// Raises the Collapsed event.
        /// </summary>
        /// <param name="e">Contains event arguments.</param>
        protected virtual void OnCollapsed(EventArgs e)
        {
            var handler = Collapsed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the Expanded event.
        /// </summary>
        /// <param name="e">Contains event arguments.</param>
        protected virtual void OnExpanded(EventArgs e)
        {
            var handler = Expanded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Handles the Checked event of either the Vertical or Horizontal
        /// GridSplitterHandle ToggleButton.
        /// </summary>
        /// <param name="sender">An instance of the ToggleButton that fired the event.</param>
        /// <param name="e">Contains event arguments for the routed event that fired.</param>
        private void GridSplitterButtonChecked(object sender, RoutedEventArgs e)
        {
            if (IsCollapsed) return;
            // In our case, Checked = Collapsed.  Which means we want everything
            // ready to be expanded.
            Collapse();

            IsCollapsed = true;

            // Deactivate the background so the splitter can not be dragged.
            _elementGridSplitterBackground.IsHitTestVisible = false;
            _elementGridSplitterBackground.Opacity = 0.5;

            // Raise the Collapsed event.
            OnCollapsed(EventArgs.Empty);
        }


        /// <summary>
        /// Handles the Unchecked event of either the Vertical or Horizontal
        /// GridSplitterHandle ToggleButton.
        /// </summary>
        /// <param name="sender">An instance of the ToggleButton that fired the event.</param>
        /// <param name="e">Contains event arguments for the routed event that fired.</param>
        private void GridSplitterButtonUnchecked(object sender, RoutedEventArgs e)
        {
            if (!IsCollapsed) return;
            // In our case, Unchecked = Expanded.  Which means we want everything
            // ready to be collapsed.
            Expand();

            IsCollapsed = false;

            // Activate the background so the splitter can be dragged again.
            _elementGridSplitterBackground.IsHitTestVisible = true;
            //_elementGridSplitterBackground.Opacity = 1;

            // Raise the Expanded event.
            OnExpanded(EventArgs.Empty);
        }

        #endregion

        #region Collapse and Expand Animations

        #region Property for animating rows

        private RowDefinition _animatingRow;

#pragma warning disable 169
        private static readonly DependencyProperty RowHeightAnimationProperty =
#pragma warning restore 169
 DependencyProperty.Register(
                "RowHeightAnimation",
                typeof(double),
                typeof(ExtendedGridSplitter),
                new PropertyMetadata(RowHeightAnimationChanged)
                );

        private static void RowHeightAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var extendedGridSplitter = d as ExtendedGridSplitter;
            if (extendedGridSplitter != null)
                extendedGridSplitter._animatingRow.Height = new GridLength((double)e.NewValue);
        }

        #endregion


        /// <summary>
        /// Uses DoubleAnimation and a StoryBoard to animated the collapsing
        /// of the specificed ColumnDefinition or RowDefinition.
        /// </summary>
        /// <param name="definition">The RowDefinition or ColumnDefintition that will be collapsed.</param>
        private void AnimateCollapse(object definition)
        {
            // Setup the animation and StoryBoard
            var gridLengthAnimation = new DoubleAnimation { Duration = new Duration(TimeSpan.FromMilliseconds(AnimationTimeMillis)) };
            var sb = new Storyboard();

            // Add the animation to the StoryBoard
            sb.Children.Add(gridLengthAnimation);

            // Specify the target RowDefinition and property (Height) that will be altered by the animation.
            _animatingRow = (RowDefinition)definition;
            Storyboard.SetTarget(gridLengthAnimation, this);
            Storyboard.SetTargetProperty(gridLengthAnimation, new PropertyPath("RowHeightAnimation"));




            gridLengthAnimation.From = _animatingRow.ActualHeight;
            gridLengthAnimation.To = 0;

            // Start the StoryBoard.
            sb.Begin();
        }

        /// <summary>
        /// Uses DoubleAnimation and a StoryBoard to animate the expansion
        /// of the specificed ColumnDefinition or RowDefinition.
        /// </summary>
        /// <param name="definition">The RowDefinition or ColumnDefintition that will be expanded.</param>
        private void AnimateExpand(object definition)
        {
            // Setup the animation and StoryBoard
            var gridLengthAnimation = new DoubleAnimation { Duration = new Duration(TimeSpan.FromMilliseconds(AnimationTimeMillis)) };
            var sb = new Storyboard();

            // Add the animation to the StoryBoard
            sb.Children.Add(gridLengthAnimation);

            // Specify the target RowDefinition and property (Height) that will be altered by the animation.
            _animatingRow = (RowDefinition)definition;
            Storyboard.SetTarget(gridLengthAnimation, this);
            Storyboard.SetTargetProperty(gridLengthAnimation, new PropertyPath("RowHeightAnimation"));


            gridLengthAnimation.From = _animatingRow.ActualHeight;
            gridLengthAnimation.To = _savedActualValue;

            // Start the StoryBoard.
            sb.Begin();
        }

        #endregion


    }
}
