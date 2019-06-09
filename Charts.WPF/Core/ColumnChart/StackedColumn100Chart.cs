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
    public class StackedColumn100Chart : ChartBase
    {
        /// <summary>
        /// Initializes the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        static StackedColumn100Chart()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackedColumn100Chart), new FrameworkPropertyMetadata(typeof(StackedColumn100Chart)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        public StackedColumn100Chart()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(StackedColumn100Chart);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(StackedColumn100Chart);
#endif
        }

        protected override double GridLinesMaxValue => 100.0;
    }
}