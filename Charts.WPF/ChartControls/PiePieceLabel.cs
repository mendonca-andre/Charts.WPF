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
    /// The pie piece label.
    /// </summary>
    public class PiePieceLabel : Control
    {
        /// <summary>
        /// The caption property.
        /// </summary>
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
            "Caption",
            typeof(string),
            typeof(PiePieceLabel),
            new PropertyMetadata(null));

        /// <summary>
        /// The value property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(PiePieceLabel),
            new PropertyMetadata(0.0, OnValueChanged));

        /// <summary>
        /// The on value changed.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// The percentage property.
        /// </summary>
        public static readonly DependencyProperty PercentageProperty = DependencyProperty.Register(
            "Percentage",
            typeof(double),
            typeof(PiePieceLabel),
            new PropertyMetadata(null));

        /// <summary>
        /// The item brush property.
        /// </summary>
        public static readonly DependencyProperty ItemBrushProperty = DependencyProperty.Register(
            "ItemBrush",
            typeof(Brush),
            typeof(PiePieceLabel),
            new PropertyMetadata(null));

        /// <summary>
        /// Initializes static members of the <see cref="PiePieceLabel"/> class.
        /// </summary>
        static PiePieceLabel()
        {
#if NETFX_CORE
            // do nothing
#elif SILVERLIGHT
            // do nothing
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PiePieceLabel), new FrameworkPropertyMetadata(typeof(PiePieceLabel))); 
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PiePieceLabel"/> class.
        /// </summary>
        public PiePieceLabel()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PiePieceLabel);
#elif SILVERLIGHT
            this.DefaultStyleKey = typeof(PiePieceLabel);
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
