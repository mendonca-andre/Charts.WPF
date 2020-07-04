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
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class ColumnPiece : PieceBase
    {
        #region Fields

        private Border slice;

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(ColumnPiece),
            new PropertyMetadata(0.0, OnPercentageChanged));
        
        public static readonly DependencyProperty ColumnHeightProperty =
            DependencyProperty.Register("ColumnHeight", typeof(double), typeof(ColumnPiece),
            new PropertyMetadata(0.0));
        
        #endregion Fields

        #region Constructors

        static ColumnPiece()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColumnPiece), new FrameworkPropertyMetadata(typeof(ColumnPiece)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnPiece"/> class.
        /// </summary>
        public ColumnPiece()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(ColumnPiece);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(ColumnPiece);
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

        public double ColumnHeight
        {
            get => (double)this.GetValue(ColumnHeightProperty);
            set => this.SetValue(ColumnHeightProperty, value);
        }
 
        #endregion Properties

        #region Methods

        private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ColumnPiece)?.DrawGeometry();
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

                double startHeight = 0;
                if (this.slice.Height > 0)
                {
                    startHeight = this.slice.Height;
                }

                var scaleAnimation = new DoubleAnimation
                                         {
                                             From = startHeight,
                                             To = this.ClientHeight * this.Percentage,
                                             Duration = TimeSpan.FromMilliseconds(withAnimation ? 500 : 0),
                                             EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
                                         };
                var storyScaleX = new Storyboard();
                storyScaleX.Children.Add(scaleAnimation);

                Storyboard.SetTarget(storyScaleX, this.slice);

#if NETFX_CORE
                scaleAnimation.EnableDependentAnimation = true;
                Storyboard.SetTargetProperty(storyScaleX, "Height");
#else
                Storyboard.SetTargetProperty(storyScaleX, new PropertyPath("Height"));
#endif
                storyScaleX.Begin();
   
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        #endregion Methods
    }
}