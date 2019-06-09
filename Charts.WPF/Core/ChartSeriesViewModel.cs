namespace Charts.WPF.Core
{
#if NETFX_CORE

    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml;

#else
    using System.Windows;
#endif
    using System.Windows.Media;

    /// <summary>
    /// we cannot use the ChartSeries directly because we will join the data to internal Chartseries
    /// </summary>
    public class ChartLegendItemViewModel : DependencyObject
    {
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption",
            typeof(string),
            typeof(ChartLegendItemViewModel),
            new PropertyMetadata(null));
        public static readonly DependencyProperty ItemBrushProperty =
            DependencyProperty.Register("ItemBrush",
            typeof(Brush),
            typeof(ChartLegendItemViewModel),
            new PropertyMetadata(null));

        public string Caption
        {
            get => (string)this.GetValue(CaptionProperty);
            set => this.SetValue(CaptionProperty, value);
        }

        public Brush ItemBrush
        {
            get => (Brush)this.GetValue(ItemBrushProperty);
            set => this.SetValue(ItemBrushProperty, value);
        }
    }
}
