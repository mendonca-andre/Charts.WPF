namespace Charts.WPF.Examples.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class TestPageViewModel : INotifyPropertyChanged
    {
        public TestPageViewModel()
        {
            this.Series = new ObservableCollection<SeriesData>();

            this.Errors = new ObservableCollection<TestClass>();
            this.Warnings = new ObservableCollection<TestClass>();

            this.Errors.Add(new TestClass() { Category = "Globalization", Number = 66 });
            this.Errors.Add(new TestClass() { Category = "Features", Number = 23 });
            this.Errors.Add(new TestClass() { Category = "Content Types", Number = 12 });
            this.Errors.Add(new TestClass() { Category = "Correctness", Number = 94 });
            this.Errors.Add(new TestClass() { Category = "Naming", Number = 45 });
            this.Errors.Add(new TestClass() { Category = "Best Practices", Number = 29 });

            this.Warnings.Add(new TestClass() { Category = "Globalization", Number = 34 });
            this.Warnings.Add(new TestClass() { Category = "Features", Number = 23 });
            this.Warnings.Add(new TestClass() { Category = "Content Types", Number = 15 });
            this.Warnings.Add(new TestClass() { Category = "Correctness", Number = 66 });
            this.Warnings.Add(new TestClass() { Category = "Naming", Number = 56 });
            this.Warnings.Add(new TestClass() { Category = "Best Practices", Number = 34 });

            this.Series.Add(new SeriesData() { DisplayName = "Errors", Items = this.Errors });
            this.Series.Add(new SeriesData() { DisplayName = "Warnings", Items = this.Warnings });
        }

        /// <summary>
        /// The selected item.
        /// </summary>
        private object selectedItem;
        public object SelectedItem
        {
            get => this.selectedItem;

            set
            {
                this.selectedItem = value;
                this.NotifyPropertyChanged("SelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets the series.
        /// </summary>
        public ObservableCollection<SeriesData> Series
        {
            get;
            set;
        }

        public ObservableCollection<TestClass> Errors
        {
            get;
            set;
        }

        public ObservableCollection<TestClass> Warnings
        {
            get;
            set;
        }

        private void NotifyPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
