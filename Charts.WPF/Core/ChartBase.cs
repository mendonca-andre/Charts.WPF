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
    using Windows.UI.Xaml.Data;
#else
#endif
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    using Charts.WPF.ChartControls;

    public abstract class ChartBase : Control, INotifyPropertyChanged
    {
        #region Fields

        private bool onApplyTemplateFinished;
        private ObservableCollection<string> gridLines = new ObservableCollection<string>();

        public static readonly DependencyProperty PlotterAreaStyleProperty =
            DependencyProperty.Register("PlotterAreaStyle",
            typeof(Style), typeof(ChartBase), new PropertyMetadata(null));

        public static readonly DependencyProperty ChartAreaStyleProperty =
            DependencyProperty.Register("ChartAreaStyle",
            typeof(Style),  typeof(ChartBase), new PropertyMetadata(null));

        public static readonly DependencyProperty ChartLegendItemStyleProperty =
            DependencyProperty.Register("ChartLegendItemStyle",
            typeof(Style), typeof(ChartBase), new PropertyMetadata(null));

        public static readonly DependencyProperty ChartTitleProperty =
            DependencyProperty.Register("ChartTitle",
            typeof(string),  typeof(ChartBase), new PropertyMetadata("ChartTitle"));

        public static readonly DependencyProperty ChartSubTitleProperty =
            DependencyProperty.Register("ChartSubTitle",
            typeof(string), typeof(ChartBase), new PropertyMetadata("ChartSubTitle"));

        public static readonly DependencyProperty ChartTitleVisibilityProperty =
            DependencyProperty.Register("ChartTitleVisibility",
            typeof(Visibility), typeof(ChartBase), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty ChartLegendVisibilityProperty =
            DependencyProperty.Register("ChartLegendVisibility",
            typeof(Visibility), typeof(ChartBase), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty PaletteProperty =
            DependencyProperty.Register("Palette",
            typeof(ResourceDictionaryCollection), typeof(ChartBase), new PropertyMetadata(null, OnPaletteChanged));

        public static readonly DependencyProperty DefaultPaletteProperty =
            DependencyProperty.Register("DefaultPalette",
            typeof(ResourceDictionaryCollection), typeof(ChartBase), new PropertyMetadata(null));
        
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush",
            typeof(Brush), typeof(ChartBase), new PropertyMetadata(new SolidColorBrush(Colors.Orange)));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem",
            typeof(object), typeof(ChartBase), new PropertyMetadata(null));
        
        public static readonly DependencyProperty IsRowColumnSwitchedProperty =
            DependencyProperty.Register("IsRowColumnSwitched",
            typeof(bool), typeof(ChartBase), new PropertyMetadata(false, OnIsRowColumnSwitchedChanged));

        public static readonly DependencyProperty ChartTitleStyleProperty =
            DependencyProperty.Register("ChartTitleStyle",
            typeof(Style), typeof(ChartBase), new PropertyMetadata(null));

        public static readonly DependencyProperty SeriesProperty =
            DependencyProperty.Register("Series",
            typeof(ObservableCollection<ChartSeries>), typeof(ChartBase), new PropertyMetadata(null, OnSeriesChanged));
        
        public static readonly DependencyProperty InternalDataContextProperty =
            DependencyProperty.Register("InternalDataContext",
            typeof(object), typeof(ChartBase), new PropertyMetadata(null, InternalDataContextChanged));

        public static readonly DependencyProperty SeriesSourceProperty =
           DependencyProperty.Register("SeriesSource",
           typeof(IEnumerable), typeof(ChartBase), new PropertyMetadata(null, OnSeriesSourceChanged));

        public static readonly DependencyProperty ToolTipFormatProperty =
            DependencyProperty.Register("ToolTipFormat",
            typeof(string), typeof(ChartBase), new PropertyMetadata("{0} ({1})"));

        public static readonly DependencyProperty ExceptionsProperty =
           DependencyProperty.Register("Exceptions",
           typeof(ObservableCollection<string>), typeof(ChartBase), new PropertyMetadata(new ObservableCollection<string>()));

        public static readonly DependencyProperty SeriesTemplateProperty =
           DependencyProperty.Register("SeriesTemplate",
           typeof(DataTemplate), typeof(ChartBase), new PropertyMetadata(null, OnSeriesTemplateChanged));

        public static readonly DependencyProperty MaxDataPointValueProperty =
            DependencyProperty.Register("MaxDataPointValue",
            typeof(double), typeof(ChartBase), new PropertyMetadata(0.0, OnMaxDataPointValueChanged));

        public static readonly DependencyProperty SumOfDataPointGroupProperty =
             DependencyProperty.Register("SumOfDataPointGroup",
             typeof(double), typeof(ChartBase), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaxDataPointGroupSumProperty =
             DependencyProperty.Register("MaxDataPointGroupSum",
             typeof(double), typeof(ChartBase), new PropertyMetadata(0.0, OnMaxDataPointGroupSumChanged));

        #endregion Fields

        
        #region DataContext stuff

        public static DependencyProperty DataContextWatcherProperty = DependencyProperty.Register(
            "DataContextWatcher",
            typeof(object),
            typeof(ChartBase),
            new PropertyMetadata(null, DataContextWatcher_Changed));

        public static void DataContextWatcher_Changed(
               DependencyObject sender,
               DependencyPropertyChangedEventArgs args)
        {
            var senderControl = sender as ChartBase;
            if (senderControl != null)
            {
                senderControl.InternalDataContextChanged();
            }
        }

        private static void InternalDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChartBase).InternalDataContextChanged();
        }

        private void InternalDataContextChanged()
        {
            this.UpdateDataContextOfSeries();
        }

        public object InternalDataContext
        {
            get => this.GetValue(InternalDataContextProperty);
            set => this.SetValue(InternalDataContextProperty, value);
        }

        private void UpdateDataContextOfSeries()
        {
            this.onApplyTemplateFinished = false;
            foreach (var newItem in this.Series)
            {
                if (newItem is FrameworkElement)
                {
                    newItem.DataContext = this.DataContext;
                }
            }

            this.onApplyTemplateFinished = true;
            this.UpdateSeries();
        }

        #endregion

        #region INotifiy stuff

        private static void OnSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChartBase).AttachEventHandler(e.NewValue);
        }

        private void AttachEventHandler(object collection)
        {
            if (collection is INotifyCollectionChanged)
            {
                (collection as INotifyCollectionChanged).CollectionChanged += this.ChartBase_CollectionChanged;
            }
        }

        void ChartBase_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var newSeries in e.NewItems)
                {
                    if (newSeries is ItemsControl)
                    {
#if NETFX_CORE
                        (newSeries as ItemsControl).Items.VectorChanged += Items_VectorChanged;

#else
                        if ((newSeries as ItemsControl).Items is INotifyCollectionChanged)
                        {
                            ((INotifyCollectionChanged)(newSeries as ItemsControl).Items).CollectionChanged += this.Window1_CollectionChanged;
                        }

#endif
                    }
                }
            }
        }

#if NETFX_CORE
        void Items_VectorChanged(Windows.Foundation.Collections.IObservableVector<object> sender, Windows.Foundation.Collections.IVectorChangedEventArgs @event)
        {
            UpdateSeries();
        }
#else
        private void Window1_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // new items added to a series, we may update them
            this.UpdateSeries();
        }

#endif
        #endregion

        #region Constructors

        public ChartBase()
        {
            this.Series = new ObservableCollection<ChartSeries>();
            this.SetBinding(DataContextWatcherProperty, new Binding());

            this.UpdateGridLines();
        }

        #endregion Constructors

        #region Properties

        public Style PlotterAreaStyle
        {
            get => (Style)this.GetValue(PlotterAreaStyleProperty);
            set => this.SetValue(PlotterAreaStyleProperty, value);
        }

        public Style ChartAreaStyle
        {
            get => (Style)this.GetValue(ChartAreaStyleProperty);
            set => this.SetValue(ChartAreaStyleProperty, value);
        }

        public Style ChartLegendItemStyle
        {
            get => (Style)this.GetValue(ChartLegendItemStyleProperty);
            set => this.SetValue(ChartLegendItemStyleProperty, value);
        }

        public string ChartTitle
        {
            get => (string)this.GetValue(ChartTitleProperty);
            set => this.SetValue(ChartTitleProperty, value);
        }

        public string ChartSubTitle
        {
            get => (string)this.GetValue(ChartSubTitleProperty);
            set => this.SetValue(ChartSubTitleProperty, value);
        }

        public Visibility ChartTitleVisibility
        {
            get => (Visibility)this.GetValue(ChartTitleVisibilityProperty);
            set => this.SetValue(ChartTitleVisibilityProperty, value);
        }
        
        public Style ChartTitleStyle
        {
            get => (Style)this.GetValue(ChartTitleStyleProperty);
            set => this.SetValue(ChartTitleStyleProperty, value);
        }

        public double MaxDataPointValue
        {
            get => (double)this.GetValue(MaxDataPointValueProperty);
            set => this.SetValue(MaxDataPointValueProperty, value);
        }

        public double MaxDataPointGroupSum
        {
            get => (double)this.GetValue(MaxDataPointGroupSumProperty);
            set => this.SetValue(MaxDataPointGroupSumProperty, value);
        }

        public ObservableCollection<ChartSeries> Series
        {
            get => (ObservableCollection<ChartSeries>)this.GetValue(SeriesProperty);
            set => this.SetValue(SeriesProperty, value);
        }

        public object SelectedItem
        {
            get => this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }
        public bool IsRowColumnSwitched
        {
            get => (bool)this.GetValue(IsRowColumnSwitchedProperty);
            set => this.SetValue(IsRowColumnSwitchedProperty, value);
        }

        public ResourceDictionaryCollection Palette
        {
            get => (ResourceDictionaryCollection)this.GetValue(PaletteProperty);
            set => this.SetValue(PaletteProperty, value);
        }

        public ResourceDictionaryCollection DefaultPalette
        {
            get => (ResourceDictionaryCollection)this.GetValue(DefaultPaletteProperty);
            set => this.SetValue(DefaultPaletteProperty, value);
        }
        
        public Brush SelectedBrush
        {
            get => (Brush)this.GetValue(SelectedBrushProperty);
            set => this.SetValue(SelectedBrushProperty, value);
        }        

        public string ToolTipFormat
        {
            get => (string)this.GetValue(ToolTipFormatProperty);
            set => this.SetValue(ToolTipFormatProperty, value);
        }

        public Visibility ChartLegendVisibility
        {
            get => (Visibility)this.GetValue(ChartLegendVisibilityProperty);
            set => this.SetValue(ChartLegendVisibilityProperty, value);
        }
                
        public IEnumerable SeriesSource
        {
            get => (IEnumerable)this.GetValue(SeriesSourceProperty);
            set => this.SetValue(SeriesSourceProperty, value);
        }

        public ObservableCollection<string> Exceptions
        {
            get => (ObservableCollection<string>)this.GetValue(ExceptionsProperty);
            set => this.SetValue(ExceptionsProperty, value);
        }

        public DataTemplate SeriesTemplate
        {
            get => (DataTemplate)this.GetValue(SeriesTemplateProperty);
            set => this.SetValue(SeriesTemplateProperty, value);
        }

        public double SumOfDataPointGroup
        {
            get => (double)this.GetValue(SumOfDataPointGroupProperty);
            set => this.SetValue(SumOfDataPointGroupProperty, value);
        }

        public ObservableCollection<string> GridLines => this.gridLines;

        /// <summary>
        /// In ColumnGrid we need some space above the column to show the number above the column,
        /// this is not needed in StackedChart
        /// </summary>
        public virtual bool IsUseNextBiggestMaxValue => false;

        #endregion Properties

        #region Methods

#if NETFX_CORE
        protected override void OnApplyTemplate()
        {
            InternalOnApplyTemplate();
        }
#else
        public override void OnApplyTemplate()
        {
            this.InternalOnApplyTemplate();
        }

#endif

        public void InternalOnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.onApplyTemplateFinished = true;
            this.UpdateSeries();
        }

        private ObservableCollection<ChartLegendItemViewModel> chartLegendItems = new ObservableCollection<ChartLegendItemViewModel>();
        public ObservableCollection<ChartLegendItemViewModel> ChartLegendItems => this.chartLegendItems;

        private static void OnSeriesSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (IEnumerable)e.OldValue;
            var newValue = (IEnumerable)e.NewValue;
            var source = (ChartBase)d;
            source.OnSeriesSourceChanged(oldValue, newValue);
        }

        private static void OnPaletteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChartBase).UpdateColorsOfDataPoints();
        }

        private static void OnIsRowColumnSwitchedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChartBase).UpdateData();
        }

        private string GetPropertyValue(object item, string propertyName)
        {
            foreach (var info in item.GetType().GetAllProperties())
            {
                if (info.Name == propertyName)
                {
                    var v = info.GetValue(item, null);
                    return v.ToString();
                }
            }

            throw new Exception("Value not found");
        }

        private Brush GetItemBrush(int index)
        {
            var usedPalette = this.DefaultPalette;
            if (this.Palette != null)
            {
                usedPalette = this.Palette;
            }

            if (usedPalette != null)
            {
                // returns the color from palette with the given index
                // for indexes large than the number of color in the palette we will start at the beginning 
                var paletteIndex = index % usedPalette.Count;
                var resDictionary = usedPalette[paletteIndex];
                try
                {
                    foreach (var entry in resDictionary.Values)
                    {
                        if (entry is Brush)
                        {
                            return entry as Brush;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }

            return new SolidColorBrush(Colors.Red);
        }

        /// <summary>
        /// take a number, e.g.
        /// 43456 -> 50000
        /// 1324 -> 1400
        /// 123 -> 130
        /// 8 -> 10
        /// 23 -> 30
        /// 82 -> 90
        /// 92 -> 100
        /// 1.5 -> 2
        /// 33 -> 40
        /// </summary>
        /// <param name="newMaxValue"></param>
        /// <returns></returns>
        private double CalculateMaxValue(double newMaxValue)
        {
            var bestMaxValue = 0.0;
            var bestDivisor = 1;

            this.GetBestValues(newMaxValue, ref bestMaxValue, ref bestDivisor);

            return bestMaxValue;
        }

        private double CalculateDistance(double givenBestMaxValue)
        {
            var bestMaxValue = 0.0;
            var bestDivisor = 1;
            var distance = 0.0;

            this.GetBestValues(givenBestMaxValue, ref bestMaxValue, ref bestDivisor);
            distance = bestMaxValue / bestDivisor;

            return distance;
        }


        private void GetBestValues(double wert, ref double bestMaxValue, ref int bestDivisor)
        {
            if (wert == 60.0)
            {
            }

            var wertString = wert.ToString(CultureInfo.InvariantCulture);
            double tensBelowNull = 1;

            if (wert <= 1)
            {
                // 0.72  -> 0.8
                // 0.00145
                // 0.0007453 0> 7453

                // count digits after comma
                var digitsAfterComma = wertString.Replace("0.", string.Empty).Length;
                tensBelowNull = Math.Pow(10, digitsAfterComma);
                wert = wert * tensBelowNull;
                wertString = wert.ToString(CultureInfo.InvariantCulture);
            }

            if (wertString.Contains("."))
            {
                wertString = wertString.Substring(0, wertString.IndexOf("."));
            }

            var digitsBeforeComma = wertString.Length;
            var roundedValue = (int)Math.Ceiling(wert);
            double tens = 0;
            if (digitsBeforeComma > 2)
            {
                tens = Math.Pow(10, digitsBeforeComma - 2);
                var wertWith2Digits = wert / tens;
                roundedValue = (int)Math.Ceiling(wertWith2Digits);
            }
            else if (digitsBeforeComma == 1)
            {
                tens = 0.1;
                var wertWith2Digits = wert / tens;
                roundedValue = (int)Math.Ceiling(wertWith2Digits);
            }

            var finaldivisor = this.FindBestDivisor(ref roundedValue);

            var roundedValueDouble = roundedValue / tensBelowNull;

            if (tens > 0)
            {
                roundedValueDouble = roundedValueDouble * tens;
            }

            bestMaxValue = roundedValueDouble;
            bestDivisor = finaldivisor;

        }

        private int FindBestDivisor(ref int roundedValue)
        {
            if (this.IsUseNextBiggestMaxValue)
            {
                // roundedValue += 1;
            }

            while (true)
            {
                var divisors = new[] { 2, 5, 10, 25 };
                foreach (var divisor in divisors)
                {
                    var div = roundedValue % divisor;
                    var mod = roundedValue / divisor;

                    if ((roundedValue < 10) && (mod == 1))
                    {
                        return roundedValue;
                    }

                    if ((div == 0) && (mod <= 10))
                    {
                        return mod;
                    }
                }

                roundedValue = roundedValue + 1;
            }
        }

        protected abstract double GridLinesMaxValue
        {
            get;
        }

        protected void UpdateGridLines()
        {
            var distance = this.CalculateDistance(this.GridLinesMaxValue);
            this.gridLines.Clear();
            for (var i = distance; i <= this.GridLinesMaxValue; i += distance)
            {
                this.gridLines.Add(i.ToString());
            }
        }

       public bool HasExceptions => this.Exceptions.Any();

       ObservableCollection<DataPointGroup> groupedSeries = new ObservableCollection<DataPointGroup>();
        private void UpdateGroupedSeries()
        {
            // data validation
            this.Exceptions.Clear();

            // ensure that caption of series is not null, otherwise all other series would be ignored (data from the same series are collection to the same datapointgroup) 
            if (this.Series.Any(series => string.IsNullOrEmpty(series.SeriesTitle)))
            {
                this.Exceptions.Add("Series with empty caption cannot be used.");
            }

            // ensure that each series has a different name
            if (this.Series.GroupBy(series => series.SeriesTitle).Any(group => group.Count() > 1))
            {
                this.Exceptions.Add("Series with duplicate name cannot be used.");
            }

            if (!this.HasExceptions)
            {

                var result = new List<DataPointGroup>();
                try
                {
                    if (this.GetIsRowColumnSwitched())
                    {
                        ///sammle erst alle Gruppen zusammen
                        foreach (var initialSeries in this.Series)
                        {
                            var itemIndex = 0;
                            foreach (var seriesItem in initialSeries.Items)
                            {                                
                                var seriesItemCaption = this.GetPropertyValue(seriesItem, initialSeries.DisplayMember); // Security
                                var dataPointGroup = result.Where(group => group.Caption == seriesItemCaption).FirstOrDefault();
                                if (dataPointGroup == null)
                                {
                                    dataPointGroup = new DataPointGroup(this, seriesItemCaption, this.Series.Count > 1 ? true : false);
                                    dataPointGroup.PropertyChanged += this.dataPointGroup_PropertyChanged;
                                    result.Add(dataPointGroup);

                                    this.CreateDataPointGroupBindings(dataPointGroup);

                                    var seriesIndex = 0;
                                    foreach (var allSeries in this.Series)
                                    {
                                        var datapoint = new DataPoint(this);
                                        datapoint.SeriesCaption = allSeries.SeriesTitle;
                                        datapoint.ValueMember = allSeries.ValueMember;
                                        datapoint.DisplayMember = allSeries.DisplayMember;
                                        datapoint.ItemBrush = this.Series.Count == 1 ? this.GetItemBrush(itemIndex) : this.GetItemBrush(seriesIndex); // if only one series, use different color for each datapoint, if multiple series we use different color for each series
                                        datapoint.PropertyChanged += this.groupdItem_PropertyChanged;

                                        this.CreateDataPointBindings(datapoint, dataPointGroup);

                                        dataPointGroup.DataPoints.Add(datapoint);
                                        seriesIndex++;
                                    }

                                    itemIndex++;
                                }                                
                            }
                        }

                        ///gehe alle Series durch (Security, Naming etc.)
                        foreach (var series in this.Series)
                        {
                            foreach (var seriesItem in series.Items)
                            {
                                var seriesItemCaption = this.GetPropertyValue(seriesItem, series.DisplayMember); // Security

                                // finde die gruppe mit dem Namen
                                var addToGroup = result.Where(group => group.Caption == seriesItemCaption).FirstOrDefault();

                                // finde in der Gruppe 
                                var groupdItem = addToGroup.DataPoints.Where(item => item.SeriesCaption == series.SeriesTitle).FirstOrDefault();
                                groupdItem.ReferencedObject = seriesItem;
                            }
                        }
                    }
                    else
                    {
                        foreach (var initialSeries in this.Series)
                        {
                            // erstelle für jede Series einen DataPointGroup, darin wird dann für jedes Item in jeder Serie ein DataPoint angelegt
                            var dataPointGroup = new DataPointGroup(this, initialSeries.SeriesTitle, this.Series.Count > 1 ? true : false);
                            dataPointGroup.PropertyChanged += this.dataPointGroup_PropertyChanged;
                            result.Add(dataPointGroup);

                            this.CreateDataPointGroupBindings(dataPointGroup);

                            // stelle nun sicher, dass alle DataPointGruppen die gleichen Datapoints hat
                            foreach (var allSeries in this.Series)
                            {
                                var seriesIndex = 0;
                                foreach (var seriesItem in allSeries.Items)
                                {
                                    var seriesItemCaption = this.GetPropertyValue(seriesItem, initialSeries.DisplayMember); // Security
                                    var existingDataPoint = dataPointGroup.DataPoints.Where(datapoint => datapoint.SeriesCaption == seriesItemCaption).FirstOrDefault();
                                    if (existingDataPoint == null)
                                    {
                                        var datapoint = new DataPoint(this);
                                        datapoint.SeriesCaption = seriesItemCaption;
                                        datapoint.ValueMember = allSeries.ValueMember;
                                        datapoint.DisplayMember = allSeries.DisplayMember;
                                        datapoint.ItemBrush = this.GetItemBrush(seriesIndex);
                                        datapoint.PropertyChanged += this.groupdItem_PropertyChanged;

                                        this.CreateDataPointBindings(datapoint, dataPointGroup);

                                        dataPointGroup.DataPoints.Add(datapoint);
                                    }

                                    seriesIndex++;
                                }
                            }
                        }

                        ///gehe alle Series durch (Security, Naming etc.)
                        foreach (var series in this.Series)
                        {
                            foreach (var seriesItem in series.Items)
                            {
                                // finde die gruppe mit dem Namen
                                var addToGroup = result.Where(group => group.Caption == series.SeriesTitle).FirstOrDefault();

                                // finde in der Gruppe das richtige Element
                                var seriesItemCaption = this.GetPropertyValue(seriesItem, series.DisplayMember); // Security

                                var groupdItem = addToGroup.DataPoints.Where(item => item.SeriesCaption == seriesItemCaption).FirstOrDefault();
                                groupdItem.ReferencedObject = seriesItem;
                            }
                        }
                    }
                }
                catch
                {
                }

                // finished, copy all to the array
                this.groupedSeries.Clear();
                foreach (var item in result)
                {
                    this.groupedSeries.Add(item);
                }

                this.UpdateColorsOfDataPoints();

                this.chartLegendItems.Clear();
                var firstgroup = this.groupedSeries.FirstOrDefault();
                if (firstgroup != null)
                {
                    foreach (var dataPoint in firstgroup.DataPoints)
                    {
                        var legendItem = new ChartLegendItemViewModel();

                        var captionBinding = new Binding();
                        captionBinding.Source = dataPoint;
                        captionBinding.Mode = BindingMode.OneWay;
                        captionBinding.Path = new PropertyPath("SeriesCaption");
                        BindingOperations.SetBinding(legendItem, ChartLegendItemViewModel.CaptionProperty, captionBinding);

                        var brushBinding = new Binding();
                        brushBinding.Source = dataPoint;
                        brushBinding.Mode = BindingMode.OneWay;
                        brushBinding.Path = new PropertyPath("ItemBrush");
                        BindingOperations.SetBinding(legendItem, ChartLegendItemViewModel.ItemBrushProperty, brushBinding);
                        
                        this.chartLegendItems.Add(legendItem); 
                    }
                }

                this.RecalcSumOfDataPointGroup();
            }
        }

        private bool GetIsRowColumnSwitched()
        {
            if (this.IsRowColumnSwitched)
            {                
                // special case for piechart with 1 series, it does not make sense to switch
                if ((this as PieChart.PieChart) != null)
                {
                    if (this.Series.Count <= 1)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private void UpdateColorsOfDataPoints()
        {
            foreach (var dataPointGroup in this.groupedSeries)
            {
                var index = 0;
                foreach (var dataPoint in dataPointGroup.DataPoints)
                {
                    dataPoint.SetValue(DataPoint.ItemBrushProperty, this.GetItemBrush(index));
                    index++;
                }
            }

            /*
                        int legendindex = 0;
                        foreach (var legendItem in chartLegendItems)
                        {
                            legendItem.ItemBrush = GetItemBrush(legendindex);
                        }
                        */
        }

        private void CreateDataPointBindings(DataPoint datapoint, DataPointGroup dataPointGroup)
        {
            // Sende an Datapoints the maximalvalue des Charts mit (wichtig in clustered Column chart)
            var maxDataPointValueBinding = new Binding();
            maxDataPointValueBinding.Source = this;
            maxDataPointValueBinding.Mode = BindingMode.OneWay;
            maxDataPointValueBinding.Path = new PropertyPath("MaxDataPointValue");
            BindingOperations.SetBinding(datapoint, DataPoint.MaxDataPointValueProperty, maxDataPointValueBinding);

            // Sende den Datapoints the höchste Summe einer DataPointGroup mit (wichtig für stacked chart)
            var maxDataPointGroupSumBinding = new Binding();
            maxDataPointGroupSumBinding.Source = this;
            maxDataPointGroupSumBinding.Mode = BindingMode.OneWay;
            maxDataPointGroupSumBinding.Path = new PropertyPath("MaxDataPointGroupSum");
            BindingOperations.SetBinding(datapoint, DataPoint.MaxDataPointGroupSumProperty, maxDataPointGroupSumBinding);

            // Sende den Datapoint die Summe seiner Datagroup
            var sumBinding = new Binding();
            sumBinding.Source = dataPointGroup;
            sumBinding.Mode = BindingMode.OneWay;
            sumBinding.Path = new PropertyPath("SumOfDataPointGroup");
            BindingOperations.SetBinding(datapoint, DataPoint.SumOfDataPointGroupProperty, sumBinding);

            var selectionBinding = new Binding();
            selectionBinding.Source = dataPointGroup;
            selectionBinding.Mode = BindingMode.TwoWay;
            selectionBinding.Path = new PropertyPath("SelectedItem");
            BindingOperations.SetBinding(datapoint, DataPoint.SelectedItemProperty, selectionBinding);

            var selectedBrushBinding = new Binding();
            selectedBrushBinding.Source = this;
            selectedBrushBinding.Mode = BindingMode.OneWay;
            selectedBrushBinding.Path = new PropertyPath("SelectedBrush");
            BindingOperations.SetBinding(datapoint, DataPoint.SelectedBrushProperty, selectedBrushBinding);

            // tooltip format (may change sometimes)
            var tooltipFormatBinding = new Binding();
            tooltipFormatBinding.Source = this;
            tooltipFormatBinding.Mode = BindingMode.OneWay;
            tooltipFormatBinding.Path = new PropertyPath("ToolTipFormat");
            BindingOperations.SetBinding(datapoint, DataPoint.ToolTipFormatProperty, tooltipFormatBinding);

        }

        private void CreateDataPointGroupBindings(DataPointGroup dataPointGroup)
        {
            var groupBinding = new Binding();
            groupBinding.Source = this;
            groupBinding.Mode = BindingMode.TwoWay;
            groupBinding.Path = new PropertyPath("SelectedItem");
            BindingOperations.SetBinding(dataPointGroup, DataPointGroup.SelectedItemProperty, groupBinding);
        }

        void dataPointGroup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SumOfDataPointGroup")
            {
                this.RecalcSumOfDataPointGroup();
            }
        }

        private void RecalcSumOfDataPointGroup()
        {
            var maxValue = 0.0;
            foreach (var dataPointGroup in this.DataPointGroups)
            {
                if (dataPointGroup.SumOfDataPointGroup > maxValue)
                {
                    maxValue = dataPointGroup.SumOfDataPointGroup;
                }
            }

            this.MaxDataPointGroupSum = this.CalculateMaxValue(maxValue);
        }

        void groupdItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RecalcMaxDataPointValue();
        }

        private void RecalcMaxDataPointValue()
        {
            var maxValue = 0.0;
            foreach (var dataPointGroup in this.DataPointGroups)
            {
                foreach (var dataPoint in dataPointGroup.DataPoints)
                {
                    if (dataPoint.Value > maxValue)
                    {
                        maxValue = dataPoint.Value;
                    }
                }
            }

            this.MaxDataPointValue = this.CalculateMaxValue(maxValue);
        }

        public ObservableCollection<DataPointGroup> DataPointGroups => this.groupedSeries;

        private void UpdateData()
        {
            this.UpdateGroupedSeries();
        }

        private void OnSeriesSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            this.UpdateSeries();
        }

        private T LoadDataTemplate<T>(DataTemplate template, object dataContext) where T : FrameworkElement
        {
            var element = template.LoadContent();
            var view = element as T;
            view.DataContext = dataContext;

#if NETFX_CORE
#elif SILVERLIGHT
#else

            // update the bindings for wpf
            var enumerator = element.GetLocalValueEnumerator();
            while (enumerator.MoveNext())
            {
                var bind = enumerator.Current;

                if (bind.Value is BindingExpression)
                {
                    view.SetBinding(bind.Property, ((BindingExpression)bind.Value).ParentBinding);
                }
            }

#endif
            return view;
        }

        private void UpdateSeries()
        {
            if (!this.onApplyTemplateFinished)
            {
                // avoid updating the chart during initialize phase, wait for final OnApplyTemplate call
                return;
            }

            if (this.Series != null)
            {
                if (this.SeriesTemplate != null)
                {
                    if (this.SeriesSource != null)
                    {
                        if (this.SeriesSource is INotifyCollectionChanged)
                        {
                            var collectionChanged = this.SeriesSource as INotifyCollectionChanged;
                            collectionChanged.CollectionChanged -= this.SeriesSourceCollectionChanged;
                            collectionChanged.CollectionChanged += this.SeriesSourceCollectionChanged;
                        }

                        this.Series.Clear();
                        foreach (var item in this.SeriesSource)
                        {
                            var series = this.LoadDataTemplate<ChartSeries>(this.SeriesTemplate, item); // .LoadContent() as ChartSeries;

                            if (series != null)
                            {
                                // set data context
                                series.DataContext = item;
                                this.Series.Add(series);
                            }
                        }
                    }
                }

                this.UpdateGroupedSeries();
            }
        }

        private void SeriesSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // new or remove series, easiest way: recreate all
            this.UpdateSeries();
        }

        private static void OnMaxDataPointValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChartBase).OnMaxDataPointValueChanged((double)e.NewValue);
        }

        protected virtual void OnMaxDataPointValueChanged(double p)
        {

        }

        private static void OnSeriesTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // mandelkow (d as ChartBase).UpdateSeries();
        }

        private static void OnMaxDataPointGroupSumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChartBase).OnMaxDataPointGroupSumChanged((double)e.NewValue);
        }

        protected virtual void OnMaxDataPointGroupSumChanged(double p)
        {

        }
        
        private void RaisePropertyChangeEvent(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}