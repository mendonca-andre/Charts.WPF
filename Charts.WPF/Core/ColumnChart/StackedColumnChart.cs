namespace Charts.WPF.Core.ColumnChart
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

    /// <summary>
    /// Represents an Instance of the bar-chart
    /// </summary>
    public class StackedColumnChart : ChartBase
    {
        /// <summary>
        /// Initializes the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        static StackedColumnChart()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackedColumnChart), new FrameworkPropertyMetadata(typeof(StackedColumnChart)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        public StackedColumnChart()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(StackedColumnChart);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(StackedColumnChart);
#endif
        }

        protected override double GridLinesMaxValue => this.MaxDataPointGroupSum;

        protected override void OnMaxDataPointGroupSumChanged(double p)
        {
            this.UpdateGridLines();
        }
    }
}