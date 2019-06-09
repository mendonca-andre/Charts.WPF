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
    public class ClusteredColumnChart : ChartBase
    {
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        static ClusteredColumnChart()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ClusteredColumnChart), new FrameworkPropertyMetadata(typeof(ClusteredColumnChart)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusteredColumnChart"/> class.
        /// </summary>
        public ClusteredColumnChart()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(ClusteredColumnChart);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(ClusteredColumnChart);
#endif
        }

        #endregion Constructors

        /// <summary>
        /// The grid lines max value.
        /// </summary>
        protected override double GridLinesMaxValue => this.MaxDataPointValue;

        /// <summary>
        /// The on max data point value changed.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        protected override void OnMaxDataPointValueChanged(double p)
        {
            this.UpdateGridLines();
        }

        /// <summary>
        /// The is use next biggest max value.
        /// </summary>
        public override bool IsUseNextBiggestMaxValue => true;
    }
}