namespace Charts.WPF.Controls
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
    using System.Windows.Controls;

    public class CustomWrapPanel : Panel 
    {
        public Orientation Orientation
        {
            get => (Orientation)this.GetValue(OrientationProperty);
            set => this.SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty =
          DependencyProperty.Register("Orientation",
          typeof(Orientation), typeof(CustomWrapPanel), null);

        public CustomWrapPanel()
        {
            this.Orientation = Orientation.Horizontal;            
        }
        
        protected override Size MeasureOverride(Size availableSize)
        {
            var minWidth = 0.0;
            foreach (UIElement child in this.Children)
            {
                child.Measure(new Size(availableSize.Width, availableSize.Height));
                if (minWidth < child.DesiredSize.Width)
                {
                    minWidth = child.DesiredSize.Width;
                }
            }

            return new Size(minWidth, 0);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Children.Count > 0)
            {
                var z = this.SimpleArrange(finalSize.Width, finalSize.Height);
                if(this.Height != z.Height)
                {
                    this.Height = z.Height;
                }

                return z;
            }

            return new Size(0, 0);
        }

        private Size SimpleArrange(double availableWidth, double availableHeight)
        {
            var point = new Point(0, 0);
            var i = 0;
            var columnCount = 0;

            if (this.Orientation == Orientation.Vertical)
            {
                var largestItemWidth = 0.0;
                var finalWidth = 0.0;

                foreach (UIElement child in this.Children)
                {                    
                        child.Arrange(new Rect(point, new Point(point.X + child.DesiredSize.Width, point.Y + child.DesiredSize.Height)));
                        finalWidth = point.X + child.DesiredSize.Width;

                        if (child.DesiredSize.Width > largestItemWidth)
                        {
                            largestItemWidth = child.DesiredSize.Width;
                        }

                        point.Y = point.Y + child.DesiredSize.Height;

                        if ((i + 1) < this.Children.Count)
                        {
                            if ((point.Y + this.Children[i + 1].DesiredSize.Height) > availableHeight)
                            {
                                point.Y = 0;
                                point.X = point.X + largestItemWidth;
                                largestItemWidth = 0.0;
                                columnCount++;
                            }
                        }

                        i++;
                    
                }

                return new Size(finalWidth, availableHeight);
            }

            var largestItemHeight = 0.0;
            var finalHeight = 0.0;
            var largestWidth = 0.0;

            foreach (UIElement child in this.Children)
            {                    
                child.Arrange(new Rect(point, new Point(point.X + child.DesiredSize.Width, point.Y + child.DesiredSize.Height)));
                        
                finalHeight = point.Y + child.DesiredSize.Height;
                if (largestWidth < point.X + child.DesiredSize.Width)
                {
                    largestWidth = point.X + child.DesiredSize.Width;
                }

                if (child.DesiredSize.Height > largestItemHeight)
                {
                    largestItemHeight = child.DesiredSize.Height;
                }

                point.X = point.X + child.DesiredSize.Width;

                if ((i + 1) < this.Children.Count)
                {
                    if ((point.X + this.Children[i + 1].DesiredSize.Width) > availableWidth)
                    {
                        point.X = 0;
                        point.Y = point.Y + largestItemHeight;
                        largestItemHeight = 0.0;
                    }
                }

                i++;
                   
            }

            // return new Size(largestWidth, finalHeight);
            return new Size(availableWidth, finalHeight);
        }
    }
}

