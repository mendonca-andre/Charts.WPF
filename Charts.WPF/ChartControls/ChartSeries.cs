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
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// The chart series.
    /// </summary>
    public class ChartSeries : ItemsControl
    { 
        public static readonly DependencyProperty DisplayMemberProperty =
            DependencyProperty.Register("DisplayMember",
            typeof(string),
            typeof(ChartSeries),
            new PropertyMetadata(null));
        public static readonly DependencyProperty ValueMemberProperty =
            DependencyProperty.Register("ValueMember",
            typeof(string),
            typeof(ChartSeries),
            new PropertyMetadata(null));
        public static readonly DependencyProperty SeriesTitleProperty =

            DependencyProperty.Register("SeriesTitle",
            typeof(string),
            typeof(ChartSeries),
            new PropertyMetadata(null));

        public string SeriesTitle
        {
            get => (string)this.GetValue(SeriesTitleProperty);
            set => this.SetValue(SeriesTitleProperty, value);
        }

        public string DisplayMember
        {
            get => (string)this.GetValue(DisplayMemberProperty);
            set => this.SetValue(DisplayMemberProperty, value);
        }

        public string ValueMember
        {
            get => (string)this.GetValue(ValueMemberProperty);
            set => this.SetValue(ValueMemberProperty, value);
        }
    }
}
