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
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    using Charts.WPF.Core.ColumnChart;

    public class BarPiece : PieceBase
    {
        #region Fields

        private Border slice;

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(BarPiece),
            new PropertyMetadata(0.0, OnPercentageChanged));
        
        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.Register("ColumnWidth", typeof(double), typeof(BarPiece),
            new PropertyMetadata(0.0));

        #endregion Fields

        #region Constructors

        static BarPiece()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BarPiece), new FrameworkPropertyMetadata(typeof(BarPiece)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnPiece"/> class.
        /// </summary>
        public BarPiece()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(BarPiece);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(BarPiece);
#endif
            this.Loaded += this.ColumnPiece_Loaded;
        }

        #endregion Constructors

        #region Properties

        public double Percentage
        {
            get => (double)this.GetValue(PercentageProperty);
            set => this.SetValue(PercentageProperty, value);
        }

        public double ColumnWidth
        {
            get => (double)this.GetValue(ColumnWidthProperty);
            set => this.SetValue(ColumnWidthProperty, value);
        }

        #endregion Properties

        #region Methods

        private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BarPiece).DrawGeometry();
        }

        protected override void InternalOnApplyTemplate()
        {
            this.slice = this.GetTemplateChild("Slice") as Border;
            this.RegisterMouseEvents(this.slice);
        }

        void ColumnPiece_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawGeometry();
        }

        /// <summary>
        /// Draws the geometry.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void DrawGeometry(bool withAnimation = true)
        {    
            try
            {
                if (this.ClientWidth <= 0.0)
                {
                    return;
                }

                if (this.ClientHeight <= 0.0)
                {
                    return;
                }

                double startWidth = 0;
                if (this.slice.Width > 0)
                {
                    startWidth = this.slice.Width;
                }

                var scaleAnimation = new DoubleAnimation();
                scaleAnimation.From = startWidth;
                scaleAnimation.To = this.ClientWidth * this.Percentage;
                scaleAnimation.Duration = TimeSpan.FromMilliseconds(500);
                scaleAnimation.EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut };
                var storyScaleX = new Storyboard();
                storyScaleX.Children.Add(scaleAnimation);
                
                Storyboard.SetTarget(storyScaleX, this.slice);

#if NETFX_CORE
                scaleAnimation.EnableDependentAnimation = true;
                Storyboard.SetTargetProperty(storyScaleX, "Width");
#else
                Storyboard.SetTargetProperty(storyScaleX, new PropertyPath("Width"));
#endif
                storyScaleX.Begin();
                         
                // SetValue(ColumnPiece.ColumnHeightProperty, this.ClientHeight * Percentage);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        #endregion Methods
    }
}