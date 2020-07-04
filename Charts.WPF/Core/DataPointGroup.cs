namespace Charts.WPF.Core
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
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows;

    public class DataPointGroup : DependencyObject, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SumOfDataPointGroupProperty =
            DependencyProperty.Register("SumOfDataPointGroup",
            typeof(double),
            typeof(DataPointGroup),
            new PropertyMetadata(0.0));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem",
            typeof(object),
            typeof(DataPointGroup),
            new PropertyMetadata(null));

        public object SelectedItem
        {
            get => this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }
        public double SumOfDataPointGroup
        {
            get => (double)this.GetValue(SumOfDataPointGroupProperty);
            set => this.SetValue(SumOfDataPointGroupProperty, value);
        }

        public ObservableCollection<DataPoint> DataPoints
        { get; set; }

        public ChartBase ParentChart
        { get; private set; }
       
        public DataPointGroup(ChartBase parentChart, string caption, bool showcaption)
        {
            this.ParentChart = parentChart;
            this.Caption = caption;
            this.ShowCaption = showcaption;

            this.DataPoints = new ObservableCollection<DataPoint>();
            this.DataPoints.CollectionChanged += this.Items_CollectionChanged;
        }

        void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach(var item in e.NewItems)
            {
                if (item is INotifyPropertyChanged)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += this.DataPointGroup_PropertyChanged;
                }
            }
        }

        void DataPointGroup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                this.RecalcValues();
            }
        }

        private void RecalcValues()
        {
            var maxValue = 0.0;
            var sum = 0.0;
            foreach (var item in this.DataPoints)
            {
                item.StartValue = sum;
                sum += item.Value;
                if (item.Value > maxValue)
                {
                    maxValue = item.Value;
                }
            }

            this.SumOfDataPointGroup = sum;
            this.RaisePropertyChangeEvent("SumOfDataPointGroup");
        }

        public string Caption { get; private set; }

        public bool ShowCaption { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangeEvent(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
