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

    public class EvenlyDistributedRowGrid : Panel
    {
        public static readonly DependencyProperty IsReverseOrderProperty =
            DependencyProperty.Register("IsReverseOrder",
            typeof(bool),
            typeof(EvenlyDistributedRowGrid),
            new PropertyMetadata(false));      

        public bool IsReverseOrder
        {
            get => (bool)this.GetValue(IsReverseOrderProperty);
            set => this.SetValue(IsReverseOrderProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var returnedSize = availableSize;

            if (double.IsInfinity(availableSize.Height))
            {
                // ok, we have all the space we need, so we take it
                var maxColumnWidth = 0.0;
                var minColumnHeight = 0.0;
                foreach (UIElement child in this.Children)
                {
                    child.Measure(availableSize);
                    if (maxColumnWidth < child.DesiredSize.Width)
                    {
                        maxColumnWidth = child.DesiredSize.Width;
                    }

                    if (minColumnHeight < child.DesiredSize.Height)
                    {
                        minColumnHeight = child.DesiredSize.Height;
                    }
                }

                returnedSize.Width = maxColumnWidth;
                returnedSize.Height = minColumnHeight * this.Children.Count;
                return returnedSize;
            }
            else
            {
                // oh no, the height is limited, so we can only take this height
                var spaceForHeight = availableSize.Height / this.Children.Count;

                var maxColumnWidth = 0.0;
                var minColumnHeight = 0.0;
                foreach (UIElement child in this.Children)
                {
                    child.Measure(new Size(availableSize.Width, spaceForHeight));
                    if (maxColumnWidth < child.DesiredSize.Width)
                    {
                        maxColumnWidth = child.DesiredSize.Width;
                    }

                    if (minColumnHeight < child.DesiredSize.Height)
                    {
                        minColumnHeight = child.DesiredSize.Height;
                    }
                }

                returnedSize.Width = maxColumnWidth;
                returnedSize.Height = minColumnHeight * this.Children.Count;
                return returnedSize;
            }
        }

        private double GetLargestElementWidth(Size availableSize)
        {
            var minimalWidth = 0.0;
            foreach (UIElement child in this.Children)
            {
                child.Measure(availableSize);
                if (child.DesiredSize.Width > minimalWidth)
                {
                    minimalWidth = child.DesiredSize.Width;
                }
            }

            return minimalWidth;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var cellSize = new Size(finalSize.Width, finalSize.Height / this.Children.Count);
            int row = 0, col = 0;
            var reverseStartPoint = finalSize.Height - cellSize.Height;
            foreach (UIElement child in this.Children)
            {
                if (this.IsReverseOrder)
                {
                    child.Arrange(new Rect(new Point(cellSize.Width * col, reverseStartPoint - cellSize.Height * row), cellSize));
                }
                else
                {
                    child.Arrange(new Rect(new Point(cellSize.Width * col, cellSize.Height * row), cellSize));
                }

                row++;
            }

            /*
            if (minimalWidth > 0.0)
            {
                if (this.Width != minimalWidth)
                {
                    this.Width = minimalWidth;
                }
            }
            */
            return new Size(finalSize.Width, finalSize.Height);
        }
    }
}
