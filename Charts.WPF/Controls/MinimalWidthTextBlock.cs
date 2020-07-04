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
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

#if SILVERLIGHT
    public class MinimalWidthTextBlock : Control
#else
    public class MinimalWidthTextBlock : Control
#endif
    { 
        private const char DEFAULTCHARSEPARATOR = '|';


        public static readonly DependencyProperty TextBlockStyleProperty =
            DependencyProperty.Register("TextBlockStyle", typeof(Style), typeof(MinimalWidthTextBlock),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MinimalWidthTextBlock),
            new PropertyMetadata(null));

        static MinimalWidthTextBlock()
        {
#if NETFX_CORE
            // do nothing
#elif SILVERLIGHT
            // do nothing
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MinimalWidthTextBlock), new FrameworkPropertyMetadata(typeof(MinimalWidthTextBlock))); 
#endif
        }

        public MinimalWidthTextBlock()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(MinimalWidthTextBlock);
#elif SILVERLIGHT
            this.DefaultStyleKey = typeof(MinimalWidthTextBlock);
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
        TextBlock mainTextBlock;
        private void InternalOnApplyTemplate()
        {
            this.mainBorder = this.GetTemplateChild("PART_Border") as Border; 
            this.mainTextBlock = this.GetTemplateChild("PART_TextBlock") as TextBlock;
        }

        public Style TextBlockStyle
        {
            get => (Style)this.GetValue(TextBlockStyleProperty);
            set => this.SetValue(TextBlockStyleProperty, value);
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var baseSize = base.MeasureOverride(availableSize);

            if (this.mainTextBlock != null)
            {
                var text = this.mainTextBlock.Text;
                var separator = DEFAULTCHARSEPARATOR;

                if (text.Contains(" "))
                {
                    separator = ' ';
                }

                if (text.Contains("."))
                {
                    separator = '.';
                }

                if (separator != DEFAULTCHARSEPARATOR)
                {
                    // find all combinations how the sentence could be split into 2 lines
                    var allcombinations = this.GetAllLinesCombinations(text, separator);

                    var bestWidth = double.PositiveInfinity;
                    foreach (var combination in allcombinations)
                    {
                        // now find the max width of the combination
                        var maxwidth = this.GetMaxTextWidth(combination);

                        // but we want the smallest combination
                        if (maxwidth < bestWidth)
                        {
                            bestWidth = maxwidth;
                        }
                    }

                    return new Size(bestWidth, this.GetLineHeight(this.mainTextBlock.Text) * 2);
                }                
            }

            return baseSize;
        }

        private double GetLineHeight(string teststring)
        {
            var b = this.GetCopyOfMainTextBlock();
            b.Text = "teststring";
            b.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return b.DesiredSize.Height;
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

        private double GetMaxTextWidth(string[] combination)
        {
            var longestLineWidth = 0.0;
            foreach (var line in combination)
            {
                var lineWidth = this.GetLineWidth(line);
                if (lineWidth > longestLineWidth)
                {
                    longestLineWidth = lineWidth;
                }
            }

            return longestLineWidth;
        }

        private double GetLineWidth(string line)
        {
            var b = this.GetCopyOfMainTextBlock();            
            b.Text = line;
            b.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return b.DesiredSize.Width;
        }

        private List<string[]> GetAllLinesCombinations(string text, char separator)
        {
            // Number of site features
            var combinations = new List<string[]>();

            // string[] allWords = text.Split(new char[] { ' ' });
            var startposition = 0;

            while (true)
            {
                var spacePosition = text.IndexOf(separator, startposition);
                if (spacePosition < 0)
                {
                    return combinations;
                }

                var firstPart = text.Substring(0, spacePosition);
                var secondPart = text.Substring(spacePosition);

                combinations.Add(new[] { firstPart.Trim(), secondPart.Trim() });
                startposition = spacePosition + 1;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this.mainBorder.Width = finalSize.Width;
            this.mainBorder.Height = finalSize.Height;
            return base.ArrangeOverride(finalSize);
        }
    }
}
