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
    public class StackedColumnsPanel : Panel
#else
    public class StackedColumnsPanel : Grid
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
            var cellSize = new Size(finalSize.Width / this.Children.Count, finalSize.Height);
            var col = 0;

            var bottomposition = finalSize.Height;
            foreach (UIElement child in this.Children)
            {
                var width= finalSize.Width;
                var height = child.DesiredSize.Height;
                double x = 0;
                var y = bottomposition - height;
                var rect = new Rect(x, y, width, height);
                child.Arrange(rect);

                bottomposition -= height;
                col++;
            }

            return finalSize;
        }
    }
}
