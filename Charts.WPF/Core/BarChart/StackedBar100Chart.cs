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
    public class StackedBar100Chart : ChartBase
    {
        /// <summary>
        /// Initializes the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        static StackedBar100Chart()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackedBar100Chart), new FrameworkPropertyMetadata(typeof(StackedBar100Chart)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        public StackedBar100Chart()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(StackedBar100Chart);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(StackedBar100Chart);
#endif
        }

        /// <summary>
        /// The grid lines max value.
        /// </summary>
        protected override double GridLinesMaxValue => 100.0;
    }
}