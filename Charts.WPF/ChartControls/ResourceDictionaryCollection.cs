namespace Charts.WPF.ChartControls
{
#if NETFX_CORE
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Shapes;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml;
    using Windows.Foundation;
    using Windows.UI;
    using Windows.UI.Core;
#else
#endif
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// The resource dictionary collection.
    /// </summary>
    public class ResourceDictionaryCollection : ObservableCollection<ResourceDictionary>
    {
    }
}
