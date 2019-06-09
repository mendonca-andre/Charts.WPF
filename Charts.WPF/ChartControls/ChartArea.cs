// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChartArea.cs" company="">
//   
// </copyright>
// <summary>
//   The chart area.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Charts.WPF.ChartControls
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
    using Windows.UI.Xaml.Data;

#else
#endif
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;

    using Charts.WPF.Core;
    using Charts.WPF.Core.PieChart;

    /// <summary>
    /// The chart area.
    /// </summary>
    public class ChartArea : ContentControl
    {
        static ChartArea()
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT

#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartArea), new FrameworkPropertyMetadata(typeof(ChartArea)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartArea"/> class. 
        /// Initializes a new instance of the <see cref="PieChart"/> class.
        /// </summary>
        public ChartArea()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(ChartArea);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(ChartArea);
#endif
        }

        /// <summary>
        /// The parent chart property.
        /// </summary>
        public static readonly DependencyProperty ParentChartProperty = DependencyProperty.Register(
            "ParentChart",
            typeof(ChartBase),
            typeof(ChartArea),
            new PropertyMetadata(null));

        /// <summary>
        /// The chart legend item style property.
        /// </summary>
        public static readonly DependencyProperty ChartLegendItemStyleProperty = DependencyProperty.Register(
            "ChartLegendItemStyle",
            typeof(Style),
            typeof(ChartArea),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the parent chart.
        /// </summary>
        public ChartBase ParentChart
        {
            get => (ChartBase)this.GetValue(ParentChartProperty);
            set => this.SetValue(ParentChartProperty, value);
        }

        /// <summary>
        /// The grid lines.
        /// </summary>
        public ObservableCollection<string> GridLines => this.ParentChart.GridLines;

        /// <summary>
        /// The data point groups.
        /// </summary>
        public ObservableCollection<DataPointGroup> DataPointGroups => this.ParentChart.DataPointGroups;

        /// <summary>
        /// The chart legend items.
        /// </summary>
        public ObservableCollection<ChartLegendItemViewModel> ChartLegendItems => this.ParentChart.ChartLegendItems;

        /// <summary>
        /// Gets or sets the chart legend item style.
        /// </summary>
        public Style ChartLegendItemStyle
        {
            get => (Style)this.GetValue(ChartLegendItemStyleProperty);
            set => this.SetValue(ChartLegendItemStyleProperty, value);
        }
    }
}
