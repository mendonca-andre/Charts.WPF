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
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// The evenly distributed columns grid.
    /// </summary>
    public class EvenlyDistributedColumnsGrid : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            try
            {
                /*
                if (double.IsInfinity(availableSize.Width))
                {
                    availableSize.Width = 1000;
                }
                if (double.IsInfinity(availableSize.Height))
                {
                    availableSize.Height = 1000;
                }
                */
                // gleichmäßige Verteilung, deshalb suchen wir die breiteste Column und multiplizieren mit Anzahl der Spalten
                var maxColumnWidth = 0.0;
                var minColumnHeight = 0.0;
                foreach (UIElement child in this.Children)
                {
                    if (this.Children.Count > 1)
                    {
                    }

                    child.Measure(availableSize);
                    if(maxColumnWidth < child.DesiredSize.Width)
                    {
                        maxColumnWidth = child.DesiredSize.Width;
                    }

                    if (minColumnHeight < child.DesiredSize.Height)
                    {
                        minColumnHeight = child.DesiredSize.Height;
                    }
                }

                availableSize.Width = maxColumnWidth * this.Children.Count;
                availableSize.Height = minColumnHeight;
                

                /*

                Size cellSize = GetCellSize(internalAvailableSize);

                //is there any element which would exceed the cell width
                if (OneElementExceedsCellWidth(cellSize.Width))
                {
                    //we switch to 2 rows, we need the order space for 2 rows
                    double heightOfOneRow = GetHighestElement();
                    return new Size(internalAvailableSize.Width, heightOfOneRow * 2);
                }
                 * */
               
                return availableSize;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return new Size(0, 0);
            }            
        }

        private double GetHighestElement()
        {
            var highestElementHeight = 0.0;
            foreach (UIElement child in this.Children)
            {
                if (child.DesiredSize.Height > highestElementHeight)
                {
                    highestElementHeight = child.DesiredSize.Height;
                }
            }

            return highestElementHeight;
        }

        private Size GetCellSize(Size availableSize)
        {
            return new Size(availableSize.Width / this.Children.Count, availableSize.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // calculate the space for each column
            var cellSize = this.GetCellSize(finalSize);
            var cellWidth = cellSize.Width;
            var cellHeight = cellSize.Height;

            var col = 0;
            foreach (UIElement child in this.Children)
            {
                var middlePointX = cellSize.Width * col + cellSize.Width / 2.0;
                child.Arrange(new Rect(new Point(middlePointX - cellWidth / 2.0, 0.0), new Size(cellWidth, cellHeight)));
                col++;
            }

            return finalSize;
        }
    }
}
