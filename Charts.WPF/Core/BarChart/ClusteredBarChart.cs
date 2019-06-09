// <summary>
//   Represents an Instance of the bar-chart
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
    public class ClusteredBarChart : ChartBase
    {
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        static ClusteredBarChart()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ClusteredBarChart), new FrameworkPropertyMetadata(typeof(ClusteredBarChart)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        public ClusteredBarChart()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(ClusteredBarChart);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(ClusteredBarChart);
#endif
        }

        #endregion Constructors

        protected override double GridLinesMaxValue => this.MaxDataPointValue;

        protected override void OnMaxDataPointValueChanged(double p)
        {
            this.UpdateGridLines();
        }

        public override bool IsUseNextBiggestMaxValue => true;
    }
}