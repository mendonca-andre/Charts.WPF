// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestClass.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SeriesData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Charts.WPF.Examples.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    /// <summary>
    /// The series data.
    /// </summary>
    public class SeriesData
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public ObservableCollection<TestClass> Items { get; set; }
    }

    /// <summary>
    /// The test class.
    /// </summary>
    public class TestClass : INotifyPropertyChanged
    {
        public string Category { get; set; }

        private float number = 0;
        public float Number
        {
            get => this.number;
            set
            {
                this.number = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
            }
        }

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
