// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestPageViewModel.cs" company="Am">
//   Andre Mendonca
// </copyright>
// <summary>
//   Defines the TestPageViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Charts.WPF.Example.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    /// <summary>
    /// The test page view model.
    /// </summary>
    public class TestPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The selected item.
        /// </summary>
        private object selectedItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestPageViewModel"/> class.
        /// </summary>
        public TestPageViewModel()
        {
            this.Series = new ObservableCollection<SeriesData>();

            this.Errors = new ObservableCollection<TestClass>();
            this.Warnings = new ObservableCollection<TestClass>();

            this.Errors.Add(new TestClass { Category = "Globalization", Number = 66 });
            this.Errors.Add(new TestClass { Category = "Features", Number = 23 });
            this.Errors.Add(new TestClass { Category = "Content Types", Number = 12 });
            this.Errors.Add(new TestClass { Category = "Correctness", Number = 94 });
            this.Errors.Add(new TestClass { Category = "Naming", Number = 45 });
            this.Errors.Add(new TestClass { Category = "Best Practices", Number = 29 });

            this.Warnings.Add(new TestClass { Category = "Globalization", Number = 34 });
            this.Warnings.Add(new TestClass { Category = "Features", Number = 23 });
            this.Warnings.Add(new TestClass { Category = "Content Types", Number = 15 });
            this.Warnings.Add(new TestClass { Category = "Correctness", Number = 66 });
            this.Warnings.Add(new TestClass { Category = "Naming", Number = 56 });
            this.Warnings.Add(new TestClass { Category = "Best Practices", Number = 34 });

            this.Series.Add(new SeriesData { DisplayName = "Errors", Items = this.Errors });
            this.Series.Add(new SeriesData { DisplayName = "Warnings", Items = this.Warnings });
        }

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public ObservableCollection<TestClass> Errors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the warnings.
        /// </summary>
        public ObservableCollection<TestClass> Warnings
        {
            get;
            set;
        }

        /// <summary>
        /// The notify property changed.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        private void NotifyPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
