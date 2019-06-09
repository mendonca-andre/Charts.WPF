namespace Charts.WPF.Core.BarChart
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

    using Charts.WPF.Core.ColumnChart;

    /// <summary>
    /// Represents an Instance of the bar-chart
    /// </summary>
    public class StackedBarChart : ChartBase
    {
        /// <summary>
        /// Initializes the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        static StackedBarChart()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackedBarChart), new FrameworkPropertyMetadata(typeof(StackedBarChart)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        public StackedBarChart()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(StackedBarChart);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(StackedBarChart);
#endif
        }

        protected override double GridLinesMaxValue => this.MaxDataPointGroupSum;

        protected override void OnMaxDataPointGroupSumChanged(double p)
        {
            this.UpdateGridLines();
        }
    }
}