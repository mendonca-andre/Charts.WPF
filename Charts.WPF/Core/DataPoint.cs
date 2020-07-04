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
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    public class DataPoint : DependencyObject, INotifyPropertyChanged
    {
        public static readonly DependencyProperty MaxDataPointValueProperty =
           DependencyProperty.Register("MaxDataPointValue",
           typeof(double),
           typeof(DataPoint),
           new PropertyMetadata(0.0, MaxDataPointValueChanged));

        public static readonly DependencyProperty MaxDataPointGroupSumProperty =
           DependencyProperty.Register("MaxDataPointGroupSum",
           typeof(double),
           typeof(DataPoint),
           new PropertyMetadata(0.0, MaxDataPointGroupSumChanged));

        public static readonly DependencyProperty SumOfDataPointGroupProperty =
           DependencyProperty.Register("SumOfDataPointGroup",
           typeof(double),
           typeof(DataPoint),
           new PropertyMetadata(0.0, SumOfDataPointGroupChanged));

        public static readonly DependencyProperty StartValueProperty =
          DependencyProperty.Register("StartValue",
          typeof(double),
          typeof(DataPoint),
          new PropertyMetadata(0.0));

        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected",
          typeof(bool),
          typeof(DataPoint),
          new PropertyMetadata(false));

        public static readonly DependencyProperty SelectedBrushProperty =
          DependencyProperty.Register("SelectedBrush",
          typeof(Brush),
          typeof(DataPoint),
          new PropertyMetadata(null));

        public static readonly DependencyProperty ItemBrushProperty =
         DependencyProperty.Register("ItemBrush",
         typeof(Brush),
         typeof(DataPoint),
         new PropertyMetadata(null));

        public static readonly DependencyProperty IsClickedByUserProperty =
          DependencyProperty.Register("IsClickedByUser",
          typeof(bool),
          typeof(DataPoint),
          new PropertyMetadata(false, OnIsClickedByUserChanged));

        public static readonly DependencyProperty ToolTipFormatProperty =
          DependencyProperty.Register("ToolTipFormat",
          typeof(string),
          typeof(DataPoint),
          new PropertyMetadata(string.Empty, OnToolTipFormatChanged));

        private static void OnIsClickedByUserChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                (d as DataPoint).UpdateSelection();
            }
        }

        private static void OnToolTipFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).UpdateDisplayProperties();
        }

        private void UpdateSelection()
        {
            this.SetValue(SelectedItemProperty, this.ReferencedObject);
        }

        public static readonly DependencyProperty SelectedItemProperty =
          DependencyProperty.Register("SelectedItem",
          typeof(object),
          typeof(DataPoint),
          new PropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).SelectedItemChanged(e.NewValue);
        }

        public ChartBase ParentChart
        { get; private set; }

        public DataPoint(ChartBase parentChart)
        {
            this.ParentChart = parentChart;
        }

        private void SelectedItemChanged(object selectedObject)
        {
            if (selectedObject == this.ReferencedObject)
            {
                this.SetValue(IsSelectedProperty, true);
            }
            else
            {
                // IsSelected = false;
                this.SetValue(IsSelectedProperty, false);             
            }
        }

        public object SelectedItem
        {
            get => this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// Contains the absolute StartValue of the item depending on the values of the previous items values
        /// </summary>
        public bool IsSelected
        {
            get => (bool)this.GetValue(IsSelectedProperty);
            set => this.SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// Contains the absolute StartValue of the item depending on the values of the previous items values
        /// </summary>
        public double StartValue
        {
            get => (double)this.GetValue(StartValueProperty);
            set => this.SetValue(StartValueProperty, value);
        }

        private static void SumOfDataPointGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).SumOfDataPointGroupChanged(double.Parse(e.NewValue.ToString()));
        }

        private void SumOfDataPointGroupChanged(double p)
        {
            this.RaisePropertyChangeEvent("PercentageFromSumOfDataPointGroup");
        }
        
        private static void MaxDataPointValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).MaxDataPointValueChanged(double.Parse(e.NewValue.ToString()));
        }

        private void MaxDataPointValueChanged(double p)
        {
            this.RaisePropertyChangeEvent("PercentageFromMaxDataPointValue");
        }

        public double PercentageFromSumOfDataPointGroup
        {
            get
            {
                if (this.SumOfDataPointGroup > 0)
                {
                    return this.Value / this.SumOfDataPointGroup;
                }

                return 0.0;
            }
        }        

        public double PercentageFromMaxDataPointValue
        {
            get
            {
                if (this.MaxDataPointValue > 0)
                {
                    return this.Value / this.MaxDataPointValue;
                }

                return 0.0;
            }
        }

        private static void MaxDataPointGroupSumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).MaxDataPointGroupSumChanged(double.Parse(e.NewValue.ToString()));
        }

        private void MaxDataPointGroupSumChanged(double newMaxValue)
        {
            this.RaisePropertyChangeEvent("PercentageFromMaxDataPointGroupSum");
        }

        private void UpdatePercentage()
        {
            this.RaisePropertyChangeEvent("PercentageFromMaxDataPointGroupSum");
            this.RaisePropertyChangeEvent("PercentageFromMaxDataPointValue");
            this.RaisePropertyChangeEvent("PercentageFromSumOfDataPointGroup");
        }

        public double PercentageFromMaxDataPointGroupSum
        {
            get
            {
                if (this.MaxDataPointGroupSum > 0)
                {
                    return this.Value / this.MaxDataPointGroupSum;
                }

                return 0.0;
            }
        }

        /// <summary>
        /// Summe der Werte in meiner Gruppe
        /// </summary>
        public double SumOfDataPointGroup
        {
            get => (double)this.GetValue(SumOfDataPointGroupProperty);
            set => this.SetValue(SumOfDataPointGroupProperty, value);
        }

        /// <summary>
        /// Von außen wird dieser Wert gefüllt
        /// </summary>
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

        public string ToolTipFormat
        {
            get => (string)this.GetValue(ToolTipFormatProperty);
            set => this.SetValue(ToolTipFormatProperty, value);
        }

        public Brush SelectedBrush
        {
            get => (Brush)this.GetValue(SelectedBrushProperty);
            set => this.SetValue(SelectedBrushProperty, value);
        }

        

        public string SeriesCaption
        {
            get;
            set;
        }

        public Brush ItemBrush
        {
            get => (Brush)this.GetValue(ItemBrushProperty);
            set => this.SetValue(ItemBrushProperty, value);
        }

        private object _ReferencedObject;

        public object ReferencedObject
        {
            get => this._ReferencedObject;
            set
            {
                this._ReferencedObject = value;
                this.UpdateDisplayProperties();
                if (this._ReferencedObject is INotifyPropertyChanged)
                {
                    (this._ReferencedObject as INotifyPropertyChanged).PropertyChanged += this.DataPoint_PropertyChanged;
                }
            }
        }


        private void UpdateDisplayProperties()
        {
            this.RaisePropertyChangeEvent("Value");
            this.RaisePropertyChangeEvent("FormattedValue");
            this.RaisePropertyChangeEvent("DisplayName");
        }               


        // get notified if value changes
        void DataPoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.ValueMember)
            {
                // raiseproperty change on value
                this.UpdateDisplayProperties();
                this.UpdatePercentage();
            }

            if (e.PropertyName == this.DisplayMember)
            {
                // raiseproperty change on displayname
            }
        }

        public string ValueMember { get; set; }

        public string DisplayMember { get; set; }

        public string DisplayName
        {
            get
            {
                if (this._ReferencedObject == null)
                {
                    return string.Empty;
                }

                return this.GetItemValue(this._ReferencedObject, this.DisplayMember);
            }
        }

        public string Caption => this.DisplayName;

        public string FormattedValue => string.Format(this.ToolTipFormat, this.Caption, this.Value, this.SeriesCaption, this.PercentageFromMaxDataPointGroupSum, this.PercentageFromMaxDataPointValue, this.PercentageFromSumOfDataPointGroup);

        public double Value
        {
            get
            {
                if (this._ReferencedObject == null)
                {
                    return 0.0d;
                }

                return double.Parse(this.GetItemValue(this._ReferencedObject, this.ValueMember));
            }
        }

        private string GetItemValue(object item, string propertyName)
        {
            if (item != null)
            {
                foreach (var info in item.GetType().GetAllProperties())
                {
                    if (info.Name == propertyName)
                    {
                        var v = info.GetValue(item, null);
                        return v.ToString();
                    }
                }

                throw new Exception(string.Format("Property '{0}' not found on item of type '{1}'", propertyName, item.GetType()));
            }

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangeEvent(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
