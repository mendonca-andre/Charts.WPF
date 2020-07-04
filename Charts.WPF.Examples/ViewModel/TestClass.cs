// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestClass.cs" company="AM">
//   Andre Mendonca
// </copyright>
// <summary>
//   Defines the SeriesData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Charts.WPF.Example.ViewModel
{
    using System.ComponentModel;

    /// <summary>
    /// The test class.
    /// </summary>
    public class TestClass : INotifyPropertyChanged
    {

        /// <summary>
        /// The number.
        /// </summary>
        private float number;

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public float Number
        {
            get => this.number;
            set
            {
                this.number = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
            }
        }
    }
}
