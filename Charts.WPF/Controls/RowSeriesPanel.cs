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

#if SILVERLIGHT
    public class RowSeriesPanel : Panel
#else
    public class RowSeriesPanel : Grid
#endif
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in this.Children)
                child.Measure(availableSize);

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var cellSize = new Size(finalSize.Width, finalSize.Height / this.Children.Count);
            var col = 0;

            double leftposition = 0;
            foreach (UIElement child in this.Children)
            {
                var height= finalSize.Height;
                var width = child.DesiredSize.Width;
                var x = leftposition;
                double y = 0;
                var rect = new Rect(x, y, width, height);
                child.Arrange(rect);

                leftposition += width;
                col++;
            }

            return finalSize;
        }
    }
}
