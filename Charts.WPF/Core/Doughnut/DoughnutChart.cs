namespace Charts.WPF.Core.Doughnut
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
    using Charts.WPF.Core.PieChart;

    /// <summary>
    /// The doughnut chart.
    /// </summary>
    public class DoughnutChart : PieChart
    {
        /// <summary>
        /// The grid lines max value.
        /// </summary>
        protected override double GridLinesMaxValue => 0.0;
    }
}