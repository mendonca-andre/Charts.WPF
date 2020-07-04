#if NETFX_CORE

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

#else

#endif

namespace Charts.WPF.ChartControls
{
    using System.Windows;
    using System.Windows.Controls;

    public class ChartTitle : ContentControl
    {        
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ChartTitle),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SubTitleProperty = DependencyProperty.Register(
            "SubTitle",
            typeof(string),
            typeof(ChartTitle),
            new PropertyMetadata(null));

        static ChartTitle()
        {
#if NETFX_CORE
            // do nothing
#elif SILVERLIGHT
            // do nothing
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartTitle), new FrameworkPropertyMetadata(typeof(ChartTitle))); 
#endif
        }

        public ChartTitle()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(ChartTitle);
#elif SILVERLIGHT
            this.DefaultStyleKey = typeof(ChartTitle);
#else

            // do nothing
#endif
        }

        public string Title
        {
            get => (string)this.GetValue(TitleProperty);
            set => this.SetValue(TitleProperty, value);
        }

        public string SubTitle
        {
            get => (string)this.GetValue(SubTitleProperty);
            set => this.SetValue(SubTitleProperty, value);
        }
    }
}
