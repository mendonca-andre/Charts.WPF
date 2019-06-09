namespace Charts.WPF.Core.PieChart
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

    public class PieChart : ChartBase
    {
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="PieChart"/> class.
        /// </summary>
        static PieChart()
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT

#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PieChart), new FrameworkPropertyMetadata(typeof(PieChart)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PieChart"/> class.
        /// </summary>
        public PieChart()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PieChart);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(PieChart);
#endif
            
        }

        #endregion Constructors

        protected override double GridLinesMaxValue => 0.0;

        protected virtual bool IsDoughtnutEnabled => false;

        public double PieMinimalSize
        {
            get => (double)this.GetValue(PieMinimalSizeProperty);
            set => this.SetValue(PieMinimalSizeProperty, value);
        }

        public static readonly DependencyProperty PieMinimalSizeProperty =
          DependencyProperty.Register("PieMinimalSize",
          typeof(double), typeof(PieChart), new PropertyMetadata(250.0));

        public static readonly DependencyProperty InnerRadiusRatioProperty =
            DependencyProperty.Register("InnerRadiusRatio", typeof(double), typeof(PieChart),
            new PropertyMetadata(0.0));

        public double InnerRadiusRatio
        {
            get => (double)this.GetValue(InnerRadiusRatioProperty);
            set => this.SetValue(InnerRadiusRatioProperty, value);
        }
    }
}