// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChartLegendItem.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ChartLegendItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
    using System.Windows.Media;

    /// <summary>
    /// The chart legend item.
    /// </summary>
    public class ChartLegendItem : ContentControl
    {
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
            "Caption",
            typeof(string),
            typeof(ChartLegendItem),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ChartLegendItem),
            new PropertyMetadata(0.0));
        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(ChartLegendItem),
            new PropertyMetadata(null));
        public static readonly DependencyProperty ItemBrushProperty =
            DependencyProperty.Register("ItemBrush", typeof(Brush), typeof(ChartLegendItem),
            new PropertyMetadata(null));

        /// <summary>
        /// Initializes static members of the <see cref="ChartLegendItem"/> class.
        /// </summary>
        static ChartLegendItem()
        {
#if NETFX_CORE
            // do nothing
#elif SILVERLIGHT
            // do nothing
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartLegendItem), new FrameworkPropertyMetadata(typeof(ChartLegendItem))); 
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartLegendItem"/> class.
        /// </summary>
        public ChartLegendItem()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(ChartLegendItem);
#elif SILVERLIGHT
            this.DefaultStyleKey = typeof(ChartLegendItem);
#else

            // do nothing
#endif
        }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        public string Caption
        {
            get => (string)this.GetValue(CaptionProperty);
            set => this.SetValue(CaptionProperty, value);
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        public double Percentage
        {
            get => (double)this.GetValue(PercentageProperty);
            set => this.SetValue(PercentageProperty, value);
        }

        /// <summary>
        /// Gets or sets the item brush.
        /// </summary>
        public Brush ItemBrush
        {
            get => (Brush)this.GetValue(ItemBrushProperty);
            set => this.SetValue(ItemBrushProperty, value);
        }
    }
}
