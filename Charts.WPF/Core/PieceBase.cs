namespace Charts.WPF.Core
{
#if NETFX_CORE
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Shapes;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml;
    using Windows.Foundation;
    using Windows.UI;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Core;
#else
#endif
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// The piece base.
    /// </summary>
    [TemplateVisualState(Name = StateSelectionUnselected, GroupName = GroupSelectionStates)]
    [TemplateVisualState(Name = StateSelectionSelected, GroupName = GroupSelectionStates)]
    public abstract class PieceBase : Control
    {
        #region Fields

        internal const string StateSelectionUnselected = "Unselected";
        internal const string StateSelectionSelected = "Selected";
        internal const string GroupSelectionStates = "SelectionStates";

        public static readonly DependencyProperty ClientHeightProperty =
            DependencyProperty.Register("ClientHeight", typeof(double), typeof(PieceBase),
            new PropertyMetadata(0.0, OnSizeChanged));
        
        public static readonly DependencyProperty ClientWidthProperty =
            DependencyProperty.Register("ClientWidth", typeof(double), typeof(PieceBase),
            new PropertyMetadata(0.0, OnSizeChanged));

        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(PieceBase),
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty ParentChartProperty =
            DependencyProperty.Register("ParentChart", typeof(ChartBase), typeof(PieceBase),
            new PropertyMetadata(null));

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(PieceBase),
            new PropertyMetadata(false, OnIsSelectedChanged));
        
        public static readonly DependencyProperty IsClickedByUserProperty =
            DependencyProperty.Register("IsClickedByUser", typeof(bool), typeof(PieceBase),
            new PropertyMetadata(false));

        #endregion Fields

        #region Properties

        public double ClientHeight
        {
            get => (double)this.GetValue(ClientHeightProperty);
            set => this.SetValue(ClientHeightProperty, value);
        }

        public double ClientWidth
        {
            get => (double)this.GetValue(ClientWidthProperty);
            set => this.SetValue(ClientWidthProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)this.GetValue(IsSelectedProperty);
            set => this.SetValue(IsSelectedProperty, value);
        }

        public bool IsClickedByUser
        {
            get => (bool)this.GetValue(IsClickedByUserProperty);
            set => this.SetValue(IsClickedByUserProperty, value);
        }

        public ChartBase ParentChart
        {
            get => (ChartBase)this.GetValue(ParentChartProperty);
            set => this.SetValue(ParentChartProperty, value);
        }

        public Brush SelectedBrush
        {
            get => (Brush)this.GetValue(SelectedBrushProperty);
            set => this.SetValue(SelectedBrushProperty, value);
        }

        public string Caption => (this.DataContext as DataPoint).DisplayName;

        #endregion Properties

        #region Methods

        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PieceBase).DrawGeometry(false);
        }

        protected virtual void DrawGeometry(bool withAnimation = true)
        {
        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (PieceBase)d;
            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;
            source.OnIsSelectedPropertyChanged(oldValue, newValue);
        }

        protected virtual void OnIsSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            this.IsClickedByUser = false;
            VisualStateManager.GoToState(this, newValue ? StateSelectionSelected : StateSelectionUnselected, true);
        }

#if NETFX_CORE
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            InternalOnApplyTemplate();
        }
#else
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.InternalOnApplyTemplate();
        }

#endif

        protected virtual void InternalOnApplyTemplate()
        {
        }

        protected void RegisterMouseEvents(UIElement slice)
        {

            if (slice != null)
            {
#if NETFX_CORE
                slice.PointerPressed += delegate 
#else
                slice.MouseLeftButtonUp += delegate
#endif
                {
                    this.InternalMousePressed();
                };

#if NETFX_CORE
                slice.PointerMoved += delegate
#else
                slice.MouseMove += delegate
#endif
                {
                    this.InternalMouseMoved();
                };
            }
        }

        private void InternalMousePressed()
        {
            this.SetValue(IsClickedByUserProperty, true);
        }

        private void InternalMouseMoved()
        {
            // SetValue(PieceBase.Is, true);
        }

#if NETFX_CORE
        protected override void OnPointerPressed(Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);
            HandleMouseDown();
            e.Handled = true;
        }
#else
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.HandleMouseDown();
            e.Handled = true;
        }

#endif

        private void HandleMouseDown()
        {
            this.IsClickedByUser = true;
        }

        #endregion Methods
    }
}
