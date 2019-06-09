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
    public class AutoSizeTextBlock : Control
#else
    public class AutoSizeTextBlock : Control
#endif
    {
        public static readonly DependencyProperty IsHeightExceedsSpaceProperty = DependencyProperty.Register(
            "IsHeightExceedsSpace",
            typeof(bool),
            typeof(AutoSizeTextBlock),
            new PropertyMetadata(false));

        public static readonly DependencyProperty IsWidthExceedsSpaceProperty = DependencyProperty.Register(
            "IsWidthExceedsSpace",
            typeof(bool),
            typeof(AutoSizeTextBlock),
            new PropertyMetadata(false));

        public static readonly DependencyProperty TextBlockStyleProperty = DependencyProperty.Register(
            "TextBlockStyle",
            typeof(Style),
            typeof(AutoSizeTextBlock),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(AutoSizeTextBlock),
            new PropertyMetadata(null));

        static AutoSizeTextBlock()
        {
#if NETFX_CORE
            // do nothing
#elif SILVERLIGHT
            // do nothing
#else
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(AutoSizeTextBlock),
                new FrameworkPropertyMetadata(typeof(AutoSizeTextBlock)));
#endif
        }

        public AutoSizeTextBlock()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(AutoSizeTextBlock);
#elif SILVERLIGHT
            this.DefaultStyleKey = typeof(AutoSizeTextBlock);
#else

            // do nothing
#endif
        }

#if NETFX_CORE
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            InternalOnApplyTemplate();
        }
#else
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.InternalOnApplyTemplate();
        }

#endif
        Border mainBorder;

        /// <summary>
        /// The main text block.
        /// </summary>
        TextBlock mainTextBlock;

        // double initialheight = 0.0;
        private void InternalOnApplyTemplate()
        {
            this.mainBorder = this.GetTemplateChild("PART_Border") as Border;
            this.mainTextBlock = this.GetTemplateChild("PART_TextBlock") as TextBlock;
        }

        /// <summary>
        /// Gets or sets the text block style.
        /// </summary>
        public Style TextBlockStyle
        {
            get => (Style)this.GetValue(TextBlockStyleProperty);
            set => this.SetValue(TextBlockStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is height exceeds space.
        /// </summary>
        public bool IsHeightExceedsSpace
        {
            get => (bool)this.GetValue(IsHeightExceedsSpaceProperty);
            set => this.SetValue(IsHeightExceedsSpaceProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is width exceeds space.
        /// </summary>
        public bool IsWidthExceedsSpace
        {
            get => (bool)this.GetValue(IsWidthExceedsSpaceProperty);
            set => this.SetValue(IsWidthExceedsSpaceProperty, value);
        }

        /// <summary>
        /// The measure override.
        /// </summary>
        /// <param name="availableSize">
        /// The available size.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var returnedSize = new Size(0, 0); // we do not need space
            this.mainTextBlock.Measure(new Size(double.MaxValue, double.MaxValue));

            if (double.IsInfinity(availableSize.Height)
                || (availableSize.Height > this.mainTextBlock.DesiredSize.Height))
            {
                // there is enough space, we return our minimum space
                returnedSize.Height = this.mainTextBlock.DesiredSize.Height;
            }

            if (double.IsInfinity(availableSize.Width) || (availableSize.Width > this.mainTextBlock.DesiredSize.Width))
            {
                // there is enough space, we return our minimum space
                returnedSize.Width = this.mainTextBlock.DesiredSize.Width;
            }

            return returnedSize;

            /*
            mainTextBlock.Visibility = Visibility.Collapsed;
            if (availableSize.Height < initialheight)
            {
                //if the is not enough height for the text, 
                //return new Size(0, availableSize.Height);
            }
            else
            {
                //return new Size(0, initialheight);
            }

            return new Size(0, 0);
           // Size baseSize = base.MeasureOverride(availableSize);
           // return baseSize;
             * */
        }

        private TextBlock GetCopyOfMainTextBlock()
        {
            var b = new TextBlock();
            b.FontFamily = this.mainTextBlock.FontFamily;
            b.FontSize = this.mainTextBlock.FontSize;
            b.FontStyle = this.mainTextBlock.FontStyle;
            b.LineHeight = this.mainTextBlock.LineHeight;
            b.LineStackingStrategy = this.mainTextBlock.LineStackingStrategy;
            b.FontWeight = this.mainTextBlock.FontWeight;
            return b;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.mainTextBlock.ActualHeight > 0.0)
            {
                if ((this.mainTextBlock.ActualHeight > finalSize.Height)
                    || (this.mainTextBlock.ActualWidth > finalSize.Width))
                {
                    this.IsHeightExceedsSpace = true;
                    this.Opacity = 0;
                }
                else
                {
                    this.IsHeightExceedsSpace = false;
                    this.Opacity = 1;
                }
            }

            if (this.mainTextBlock.ActualWidth > 0.0)
            {
                if ((this.mainTextBlock.ActualHeight > finalSize.Height)
                    || (this.mainTextBlock.ActualWidth > finalSize.Width))
                {
                    this.IsWidthExceedsSpace = true;
                    this.Opacity = 0;
                }
                else
                {
                    this.IsWidthExceedsSpace = false;
                    this.Opacity = 1;
                }
            }

            /*
            //mainTextBlock.MaxWidth = finalSize.Width;
            //mainTextBlock.Visibility = Visibility.Visible;
            TextBlock tempBlock = GetCopyOfMainTextBlock();
            tempBlock.Text = mainTextBlock.Text;
            tempBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            double currentWidth = tempBlock.DesiredSize.Width;

            if (tempBlock.DesiredSize.Height > finalSize.Height)
            {
                IsHeightExceedsSpace = true;
                this.Opacity = 0;
            }
            else
            {
                IsHeightExceedsSpace = false;
                this.Opacity = 1;
            }

            //is textblock larger than the available width, then we scale it down
            if (currentWidth > finalSize.Width)
            {
                
            }
            else
            {
                mainTextBlock.Visibility = Visibility.Visible;
            }

            mainBorder.Width = finalSize.Width;
            mainBorder.Height = finalSize.Height;
             * */
            return base.ArrangeOverride(finalSize);
        }
    }
}
