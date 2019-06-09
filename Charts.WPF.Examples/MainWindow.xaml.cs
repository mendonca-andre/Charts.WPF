namespace Charts.WPF.Examples
{
    using System.Windows;
    using System.Windows.Media;

    using Charts.WPF.Examples.ViewModel;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = new TestPageViewModel();
        }

        private void ShellView_Loaded_1(object sender, RoutedEventArgs e)
        {
            var m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
            var dx = m.M11;
            var dy = m.M22;

            var s = (ScaleTransform)this.mainGrid.LayoutTransform;
            s.ScaleX = 1 / dx;
            s.ScaleY = 1 / dy;
        }
    }
}
