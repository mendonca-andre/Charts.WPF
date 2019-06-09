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
    using System;
#endif
    using System.Windows;
    using System.Windows.Controls;

#if SILVERLIGHT
    public class UniformGridPanel : Panel
#else
    public class UniformGridPanel : Grid
#endif
    {
        public Orientation Orientation
        {
            get => (Orientation)this.GetValue(OrientationProperty);
            set => this.SetValue(OrientationProperty, value);
        }
        public double MinimalGridWidth
        {
            get => (double)this.GetValue(MinimalGridWidthProperty);
            set => this.SetValue(MinimalGridWidthProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty =
          DependencyProperty.Register("Orientation",
          typeof(Orientation), typeof(UniformGridPanel), new PropertyMetadata(Orientation.Horizontal, null));
        public static readonly DependencyProperty MinimalGridWidthProperty =
          DependencyProperty.Register("MinimalGridWidth",
          typeof(double), typeof(UniformGridPanel), new PropertyMetadata(100.0));

        private int cols;
        private int rows;

        /// <summary>
        /// The squaresize.
        /// </summary>
        Size squaresize = new Size(0.0, 0.0);

        protected override Size MeasureOverride(Size availableSize)
        {
            return this.RecalcRowsAndCols(availableSize, true);
        }

        private Size RecalcRowsAndCols(Size availableSize, bool withMeasure)
        {
            if (this.Children.Count == 0)
            {
                this.rows = 0;
                this.cols = 0;
                return new Size(0, 0);
            }

            // we need to calc the size of a tile
            var gridMinimalWidth = this.MinimalGridWidth;
            var smallestSize = 0.0;

            foreach (UIElement child in this.Children)
            {
                if (withMeasure)
                {
                    // only measure when called from MeasureOverride to prevent layout cycle
                    child.Measure(availableSize);
                }

                if (child.DesiredSize.Width > gridMinimalWidth)
                {
                    gridMinimalWidth = child.DesiredSize.Width;
                }

                if (child.DesiredSize.Height > gridMinimalWidth)
                {
                    gridMinimalWidth = child.DesiredSize.Height;
                }

                if (child.DesiredSize.Width > smallestSize)
                {
                    smallestSize = child.DesiredSize.Width;
                }

                if (child.DesiredSize.Height > smallestSize)
                {
                    smallestSize = child.DesiredSize.Height;
                }
            }

            if (this.Children.Count == 1)
            {
                this.cols = 1;
                this.rows = 1;
                return new Size(smallestSize, smallestSize);
            }
          
            // ok, we try to place the childs elements in a nice grid and try to use the available space

            // 1. do we have all space in all directions, then we create a rectangle with rows equals cols
            if(double.IsInfinity(availableSize.Width) && double.IsInfinity(availableSize.Height) )
            {
                var squareRoot = Math.Sqrt(this.Children.Count);
                this.rows = (int)Math.Ceiling(squareRoot);
                this.cols = (int)Math.Ceiling(squareRoot);
                this.squaresize.Width = gridMinimalWidth;
                this.squaresize.Height = gridMinimalWidth;

                // we use a uniform grid with same width and height
                // e.g. 8 children = 3 x 3
                // e.g. 4 = 2 x 2 
            }
            else if (double.IsInfinity(availableSize.Width))
            {
                // ok, width is infinite, height is limited
                // we try to fit as much elements in height

                // is height enough for 1 item
                var assumeMinimalGridSize = gridMinimalWidth;

                // it there enough height to place at least one item?
                if (availableSize.Height < assumeMinimalGridSize)
                {
                    // no, height is not enough, so we use the available height for our square
                    assumeMinimalGridSize = availableSize.Height;
                }

                // try to fit the elements into the space
                this.rows = (int)Math.Floor(availableSize.Height / gridMinimalWidth);
                if (this.rows == 0)
                {
                    this.rows = 1;
                }

                this.cols = (int)Math.Ceiling((this.Children.Count * 1.0) / (this.rows * 1.0));

                this.squaresize.Width = availableSize.Height / this.rows;
                this.squaresize.Height = availableSize.Height / this.rows;
                
            }
            else if (double.IsInfinity(availableSize.Height))
            {
                // ok, height is infinite, width is limited

                // ok, width is infinite, height is limited
                // we try to fit as much elements in height

                // is height enough for 1 item
                var assumeMinimalGridSize = gridMinimalWidth;

                // it there enough height to place at least one item?
                if (availableSize.Width < assumeMinimalGridSize)
                {
                    // no, height is not enough, so we use the available height for our square
                    assumeMinimalGridSize = availableSize.Width;
                }

                // try to fit the elements into the space
                this.cols = (int)Math.Floor(availableSize.Width / gridMinimalWidth);
                if (this.cols == 0)
                {
                    this.cols = 1;
                }

                this.rows = (int)Math.Ceiling((this.Children.Count * 1.0) / (this.cols * 1.0));

                this.squaresize.Width = availableSize.Width / this.cols;
                this.squaresize.Height = availableSize.Width / this.cols;

                // is there more space in wi
            }
            else
            {
                // size is strict, we fill the available space
                // critical part
                // find the maximum square size which can be placed inside the available space
                // 1. is there enough space for all Elements??
                var squareFootageAvailable = availableSize.Width * availableSize.Height;
                var squareFootageChildren = this.Children.Count * (gridMinimalWidth * gridMinimalWidth);

                /*
                                if (squareFootageAvailable >= squareFootageChildren)
                                {
                                    // ok, we start with the current size and find the best fit of rows and cols
                                }
                                else
                                {
                                    */
                // ok, not enough space for all children, we need to scale the items
                // so we need to find the largest square which can be place inside the area
                // we start with placing all children in one row and go down to place children in one column
                // between these combination lies the truth
                this.squaresize = new Size(0.0, 0.0); // we start with the smallest size
                for (var i = 1; i <= this.Children.Count; i++)
                {
                    var ceiling = (int)Math.Ceiling((this.Children.Count * 1.0) / (i * 1.0));

                    var availableSizeWidth = availableSize.Width / ceiling;
                    var availableSizeHeight = availableSize.Height / i;
                    var squareSize = Math.Min(availableSizeWidth, availableSizeHeight);

                    if (!(squareSize >= this.squaresize.Width) && !(squareSize >= this.squaresize.Height))
                    {
                        continue;
                    }

                    this.squaresize = new Size(squareSize, squareSize);
                    this.rows = i;
                    this.cols = ceiling;
                }

                // }
            }

            // stretch it to availableSize            
            var minimalSize = new Size(this.cols * this.squaresize.Width, this.rows * this.squaresize.Height);
            return minimalSize;
        }

        /// <summary>
        /// The arrange override.
        /// </summary>
        /// <param name="finalSize">
        /// The final size.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            // we need to recalculate the layout because maybe we get finally more space then expected (then we will fill it)
            if (this.Children.Count == 0)
            {
                return new Size(0.0, 0.0);
            }

            if (this.Children.Count == 1)
            {
                var rect = new Rect(0, 0, finalSize.Width, finalSize.Height);
                this.Children[0].Arrange(rect);
                return finalSize;
            }

            var result = this.RecalcRowsAndCols(finalSize, false);

            // do we have more space???
            if (this.cols > 0)
            {
                if (finalSize.Width > (this.squaresize.Width * this.cols))
                {
                    this.squaresize.Width = finalSize.Width / this.cols;
                }
            }

            if (this.rows > 0)
            {
                if (finalSize.Height > (this.squaresize.Height * this.rows))
                {
                    this.squaresize.Height = finalSize.Height / this.rows;
                }
            }

            var row = 0;
            var col = 0;
            for (var i = 0; i < this.Children.Count; i++)
            {
                var left = col * this.squaresize.Width;
                var top = row * this.squaresize.Height;
                var rect = new Rect(left, top, this.squaresize.Width, this.squaresize.Height);
                this.Children[i].Arrange(rect);

                col++;

                if (col != this.cols)
                {
                    continue;
                }

                col = 0;
                row++;
            }

            return finalSize;
        }
    }
}
