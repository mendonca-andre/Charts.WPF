#if NETFX_CORE

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

#else

#endif

namespace Charts.WPF.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class FadingListView : ItemsControl
    {
        public static readonly DependencyProperty RealWidthProperty =
            DependencyProperty.Register("RealWidth", typeof(double), typeof(FadingListView),
            new PropertyMetadata(0.0));
        public static readonly DependencyProperty RealHeightProperty =
            DependencyProperty.Register("RealHeight", typeof(double), typeof(FadingListView),
            new PropertyMetadata(0.0));

        static FadingListView()
        {
#if NETFX_CORE
            // do nothing
#elif SILVERLIGHT
            // do nothing
#else

            // DefaultStyleKeyProperty.OverrideMetadata(typeof(FadingListView), new FrameworkPropertyMetadata(typeof(FadingListView))); 
#endif
        }

        public FadingListView()
        {
            this.SizeChanged += this.FadingListView_SizeChanged;
#if NETFX_CORE
            // this.DefaultStyleKey = typeof(FadingListView);
#elif SILVERLIGHT
            // this.DefaultStyleKey = typeof(FadingListView);
#else

            // do nothing
#endif
        }

        public double RealWidth
        {
            get => (double)this.GetValue(RealWidthProperty);
            set => this.SetValue(RealWidthProperty, value);
        }

        public double RealHeight
        {
            get => (double)this.GetValue(RealHeightProperty);
            set => this.SetValue(RealHeightProperty, value);
        }

        void FadingListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.RealWidth = this.ActualWidth;
            this.RealHeight = this.ActualHeight;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (this.Items != null)
            {
                if (this.Items.Count < 100)
                {
                    var index = this.ItemContainerGenerator.IndexFromContainer(element);
                    var lb = (ContentPresenter)element;

                    var waitTime = TimeSpan.FromMilliseconds(index * (500.0 / this.Items.Count));

                    lb.Opacity = 0.0;
                    var anm = new DoubleAnimation();
                    anm.From = 0;
                    anm.To = 1;
                    anm.Duration = TimeSpan.FromMilliseconds(250);
                    anm.BeginTime = waitTime;

                    var storyda = new Storyboard();
                    storyda.Children.Add(anm);
                    Storyboard.SetTarget(storyda, lb);
#if NETFX_CORE
                    Storyboard.SetTargetProperty(storyda, "Opacity");
#else
                    Storyboard.SetTargetProperty(storyda, new PropertyPath(OpacityProperty));
#endif
                    storyda.Begin();
                }
            }

            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
