namespace Charts.WPF.Example.ViewModel
{
    using System.Collections.ObjectModel;

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
}