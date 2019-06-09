#if NETFX_CORE

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#else

#endif

namespace Charts.WPF.ChartControls
{
    using System.Windows;
    using System.Windows.Controls;

    using Charts.WPF.Core;

    /// <summary>
    /// The plotter area.
    /// </summary>
    public class PlotterArea : ContentControl
    {
        /// <summary>
        /// The data point item template property.
        /// </summary>
        public static readonly DependencyProperty DataPointItemTemplateProperty = DependencyProperty.Register(
            "DataPointItemTemplate",
            typeof(DataTemplate),
            typeof(PlotterArea),
            new PropertyMetadata(null));

        /// <summary>
        /// The data point items panel property.
        /// </summary>
        public static readonly DependencyProperty DataPointItemsPanelProperty = DependencyProperty.Register(
            "DataPointItemsPanel",
            typeof(ItemsPanelTemplate),
            typeof(PlotterArea),
            new PropertyMetadata(null));

        /// <summary>
        /// The chart legend item style property.
        /// </summary>
        public static readonly DependencyProperty ChartLegendItemStyleProperty = DependencyProperty.Register(
            "ChartLegendItemStyle",
            typeof(Style),
            typeof(PlotterArea),
            new PropertyMetadata(null));

        /// <summary>
        /// The parent chart property.
        /// </summary>
        public static readonly DependencyProperty ParentChartProperty = DependencyProperty.Register(
            "ParentChart",
            typeof(ChartBase),
            typeof(PlotterArea),
            new PropertyMetadata(null));

        /// <summary>
        /// Initializes static members of the <see cref="PlotterArea"/> class.
        /// </summary>
        static PlotterArea()
        {
#if NETFX_CORE
            // do nothing
#elif SILVERLIGHT
            // do nothing
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlotterArea), new FrameworkPropertyMetadata(typeof(PlotterArea))); 
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlotterArea"/> class.
        /// </summary>
        public PlotterArea()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PlotterArea);
#elif SILVERLIGHT
            this.DefaultStyleKey = typeof(PlotterArea);
#else

            // do nothing
#endif
        }

        /// <summary>
        /// Gets or sets the chart legend item style.
        /// </summary>
        public Style ChartLegendItemStyle
        {
            get => (Style)this.GetValue(ChartLegendItemStyleProperty);
            set => this.SetValue(ChartLegendItemStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the data point item template.
        /// </summary>
        public DataTemplate DataPointItemTemplate
        {
            get => (DataTemplate)this.GetValue(DataPointItemTemplateProperty);
            set => this.SetValue(DataPointItemTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the data point items panel.
        /// </summary>
        public ItemsPanelTemplate DataPointItemsPanel
        {
            get => (ItemsPanelTemplate)this.GetValue(DataPointItemsPanelProperty);
            set => this.SetValue(DataPointItemsPanelProperty, value);
        }

        /// <summary>
        /// Gets or sets the parent chart.
        /// </summary>
        public ChartBase ParentChart
        {
            get => (ChartBase)this.GetValue(ParentChartProperty);
            set => this.SetValue(ParentChartProperty, value);
        }
    }
}
