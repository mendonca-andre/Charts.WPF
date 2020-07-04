namespace Charts.WPF.Core.PieChart
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
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using Charts.WPF.Core.Doughnut;

    /// <summary>
    /// The pie piece.
    /// </summary>
    public class PiePiece : PieceBase
    {
        #region Fields

        private Path slice;
        private Border label;
                
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));
        
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0, UpdatePie));
        
        public static readonly DependencyProperty SumOfDataSeriesProperty =
            DependencyProperty.Register("SumOfDataSeries", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0, UpdatePie));
        
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0, UpdatePie));
        
        public static readonly DependencyProperty StartValueProperty =
            DependencyProperty.Register("StartValue", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0, UpdatePie));
        
        public static readonly DependencyProperty DoughnutInnerRadiusRatioProperty =
            DependencyProperty.Register("DoughnutInnerRadiusRatio", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0, OnDoughnutInnerRadiusRatioChanged));
        
        public static readonly DependencyProperty GeometryProperty =
            DependencyProperty.Register("Geometry", typeof(Geometry), typeof(PiePiece),
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty SelectionGeometryProperty =
            DependencyProperty.Register("SelectionGeometry", typeof(Geometry), typeof(PiePiece),
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty MouseOverGeometryProperty =
            DependencyProperty.Register("MouseOverGeometry", typeof(Geometry), typeof(PiePiece),
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty LineGeometryProperty =
            DependencyProperty.Register("LineGeometry", typeof(Geometry), typeof(PiePiece),
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty LabelXPosProperty =
            DependencyProperty.Register("LabelXPos", typeof(double), typeof(PiePiece),
            new PropertyMetadata(10.0));
        
        public static readonly DependencyProperty LabelYPosProperty =
            DependencyProperty.Register("LabelYPos", typeof(double), typeof(PiePiece),
            new PropertyMetadata(10.0));

        #endregion Fields

        #region Constructors

        static PiePiece()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PiePiece), new FrameworkPropertyMetadata(typeof(PiePiece)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PiePiece"/> class.
        /// </summary>
        public PiePiece()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PiePiece);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(PiePiece);
#endif
            this.Loaded += this.PiePiece_Loaded;
        }

        #endregion Constructors
 
        #region Properties

        public double DoughnutInnerRadiusRatio
        {
            get => (double)this.GetValue(DoughnutInnerRadiusRatioProperty);
            set => this.SetValue(DoughnutInnerRadiusRatioProperty, value);
        }        

        public double LabelXPos
        {
            get => (double)this.GetValue(LabelXPosProperty);
            set => this.SetValue(LabelXPosProperty, value);
        }
        public double LabelYPos
        {
            get => (double)this.GetValue(LabelYPosProperty);
            set => this.SetValue(LabelYPosProperty, value);
        }

        public Geometry LineGeometry
        {
            get => (Geometry)this.GetValue(LineGeometryProperty);
            set => this.SetValue(LineGeometryProperty, value);
        }
        
        public Geometry Geometry
        {
            get => (Geometry)this.GetValue(GeometryProperty);
            set => this.SetValue(GeometryProperty, value);
        }

        public Geometry SelectionGeometry
        {
            get => (Geometry)this.GetValue(SelectionGeometryProperty);
            set => this.SetValue(SelectionGeometryProperty, value);
        }

        public Geometry MouseOverGeometry
        {
            get => (Geometry)this.GetValue(MouseOverGeometryProperty);
            set => this.SetValue(MouseOverGeometryProperty, value);
        }

        /// <summary>
        /// The value that this pie piece represents.
        /// </summary>
        public double SumOfDataSeries
        {
            get => (double)this.GetValue(SumOfDataSeriesProperty);
            set => this.SetValue(SumOfDataSeriesProperty, value);
        }

        /// <summary>
        /// The value that this pie piece represents.
        /// </summary>
        public double StartValue
        {
            get => (double)this.GetValue(StartValueProperty);
            set => this.SetValue(StartValueProperty, value);
        }

        /// <summary>
        /// The value that this pie piece represents.
        /// </summary>
        public double MaxValue
        {
            get => (double)this.GetValue(MaxValueProperty);
            set => this.SetValue(MaxValueProperty, value);
        }

        /// <summary>
        /// The value that this pie piece represents.
        /// </summary>
        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the percent.
        /// </summary>
        /// <value>The percent.</value>
        public double Percent
        {
            get
            {
                if (this.SumOfDataSeries > 0.0)
                {
                    return (this.Value / this.SumOfDataSeries) * 100;
                }

                return 0.0;
            }
        }

        public bool IsDoughnut => this.ParentChart is DoughnutChart;

        #endregion Properties

        #region Methods

        private static void UpdatePie(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PiePiece).DrawGeometry();
        }
        
        private static void OnDoughnutInnerRadiusRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PiePiece).DrawGeometry();
        }

        void PiePiece_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawGeometry();
        }

        protected override void InternalOnApplyTemplate()
        {
            this.label = this.GetTemplateChild("PART_Label") as Border;
            this.slice = this.GetTemplateChild("Slice") as Path;
            this.RegisterMouseEvents(this.slice);
        }

        protected override void DrawGeometry(bool withAnimation = true)
        {    
            try
            {
                if (this.ClientWidth <= 0.0)
                {
                    return;
                }

                if (this.ClientHeight <= 0.0)
                {
                    return;
                }

                if (this.SumOfDataSeries > 0)
                {
                    var sss = this.ActualWidth;

                    var x = this.ClientWidth;
                    var m_startpercent = this.StartValue / this.SumOfDataSeries * 100;
                    var m_endpercent = (this.StartValue + this.Value) / this.SumOfDataSeries * 100;

                    var center = this.GetCenter();

                    var startAngle = (360 / 100.0) * m_startpercent;
                    var endAngle = (360 / 100.0) * m_endpercent;
                    var radius = this.GetRadius();
                    var isLarge = (endAngle - startAngle) > 180.0;

                    this.LayoutSegment(startAngle, endAngle, radius, this.DoughnutInnerRadiusRatio, center, this.IsDoughnut);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

         private Point GetCircumferencePoint(double angle, double radius, double centerx, double centery)
         {
             angle = angle - 90;

             var angleRad = (Math.PI / 180.0) * angle;

             var x = centerx + radius * Math.Cos(angleRad);
             var y = centery + radius * Math.Sin(angleRad);

             return new Point(x, y);
         }

        internal void LayoutSegment(double startAngle, double endAngle, double radius, double gapScale, Point center, bool isDoughnut)
        {
            try
            {
                if (startAngle > 360)
                {
                    return;
                }

                if (endAngle > 360)
                {
                    return;
                }

                if ((startAngle == 0.0) && (endAngle == 0.0))
                {
                    return;
                }

                if (endAngle > 359.5)
                {
                    endAngle = 359.5; // pie disappears if endAngle is 360                 
                }

                // Segment Geometry
                var pieRadius = radius;
                var gapRadius = pieRadius * ((gapScale == 0.0) ? 0.25 : gapScale);

                var A = this.GetCircumferencePoint(startAngle, pieRadius, center.X, center.Y);
                var B = isDoughnut ? this.GetCircumferencePoint(startAngle, gapRadius, center.X, center.Y) : center;
                var C = this.GetCircumferencePoint(endAngle, gapRadius, center.X, center.Y);
                var D = this.GetCircumferencePoint(endAngle, pieRadius, center.X, center.Y);

                var isReflexAngle = Math.Abs(endAngle - startAngle) > 180.0;

                var segments = new PathSegmentCollection();
                segments.Add(new LineSegment { Point = B });

                if (isDoughnut)
                {
                    segments.Add(
                        new ArcSegment
                            {
                                Size = new Size(gapRadius, gapRadius),
                                Point = C,
                                SweepDirection = SweepDirection.Clockwise,
                                IsLargeArc = isReflexAngle
                            });
                }

                segments.Add(new LineSegment { Point = D });
                segments.Add(
                    new ArcSegment
                        {
                            Size = new Size(pieRadius, pieRadius),
                            Point = A,
                            SweepDirection = SweepDirection.Counterclockwise,
                            IsLargeArc = isReflexAngle
                        });

                var segmentPath = new Path
                                      {
                                           StrokeLineJoin = PenLineJoin.Round,
                                           Stroke = new SolidColorBrush { Color = Colors.Black },
                                           StrokeThickness = 0.0d,
                                           Data = new PathGeometry
                                                      {
                                                          Figures = new PathFigureCollection
                                                                        {
                                                                            new PathFigure
                                                                                {
                                                                                    IsClosed = true,
                                                                                    StartPoint = A,
                                                                                    Segments = segments
                                                                                }
                                                                        }
                                                      }
                                       };
                this.SetValue(GeometryProperty, this.CloneDeep(segmentPath.Data as PathGeometry));
                this.SetValue(SelectionGeometryProperty, this.CloneDeep(segmentPath.Data as PathGeometry));

                var inRadius = radius * 0.65;
                var outRadius = radius * 1.25;

                var midAngle = startAngle + ((endAngle - startAngle) / 2.0);
                var pointOnCircle = this.GetCircumferencePoint(midAngle, pieRadius, center.X, center.Y);

                // recalculate midangle if point is to close to top or lower border
                var distanceToCenter = Math.Abs(pointOnCircle.Y - center.Y);
                var factor = distanceToCenter / center.Y;

                var midAngleBefore = midAngle;
                if ((this.GetQuadrant(pointOnCircle, center) == 1) || (this.GetQuadrant(pointOnCircle, center) == 3))
                {
                    // point is in quadrant 1 center, we go further the end angle
                    midAngle = startAngle + ((endAngle - startAngle) / 2.0)
                                          + (((endAngle - startAngle) / 2.0) * factor);
                }
                else
                {
                    // point 
                    midAngle = startAngle + ((endAngle - startAngle) / 2.0)
                               - (((endAngle - startAngle) / 2.0) * factor);
                }

                pointOnCircle = this.GetCircumferencePoint(midAngle, pieRadius, center.X, center.Y);

                var pointOuterCircle = this.GetCircumferencePoint(midAngle, pieRadius + 10, center.X, center.Y);
                var pointerMoreOuter = new Point(pointOuterCircle.X - 10, pointOuterCircle.Y);
                if (pointOnCircle.X > center.X)
                {
                    pointerMoreOuter = new Point(pointOuterCircle.X + 10, pointOuterCircle.Y);
                }

                var linesegments = new PathSegmentCollection();
                linesegments.Add(new LineSegment { Point = pointOuterCircle });
                linesegments.Add(new LineSegment { Point = pointerMoreOuter });

                var linesegmentPath = new Path
                                          {
                                               StrokeLineJoin = PenLineJoin.Round,
                                               Stroke = new SolidColorBrush { Color = Colors.Black },
                                               StrokeThickness = 2.0d,
                                               Data = new PathGeometry
                                                          {
                                                              Figures = new PathFigureCollection
                                                                            {
                                                                                new PathFigure
                                                                                    {
                                                                                        IsClosed = false,
                                                                                        StartPoint = pointOnCircle,
                                                                                        Segments = linesegments
                                                                                    }
                                                                            }
                                                          }
                                           };
                this.SetValue(LineGeometryProperty, this.CloneDeep(linesegmentPath.Data as PathGeometry));

                /*
                                label.Measure(new Size(420, 420));
                                double labelwidth = label.DesiredSize.Width;
                                double labelwidth = label.DesiredSize.Width;
                                
                                Size s = label.DesiredSize;
                                double x = this.Value;
                                */
                this.label.SetValue(Canvas.TopProperty, pointerMoreOuter.Y - (this.label.ActualHeight / 2.0));
                if (pointerMoreOuter.X > center.X)
                {
                    this.label.SetValue(Canvas.LeftProperty, pointerMoreOuter.X);
                }
                else
                {
                    this.label.SetValue(Canvas.LeftProperty, pointerMoreOuter.X - (this.label.ActualWidth));
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        private int GetQuadrant(Point pointOnCircle, Point center)
        {
            if (pointOnCircle.X > center.X)
            {
                if (pointOnCircle.Y > center.Y)
                {
                    return 2;
                }

                return 1;
            }

            if (pointOnCircle.Y > center.Y)
            {
                return 3;
            }

            return 4;
        }
        

        /// <summary>
        /// Gets the center.
        /// </summary>
        /// <returns></returns>
        private Point GetCenter()
        {
            return new Point(this.ClientWidth / 2, this.ClientHeight/2);
        }

        public PathGeometry CloneDeep(PathGeometry pathGeometry)
        {
            var newPathGeometry = new PathGeometry();
            foreach (var figure in pathGeometry.Figures)
            {
                var newFigure = new PathFigure();
                newFigure.StartPoint = figure.StartPoint;

                // Even figures have to be deep cloned. Assigning them directly will result in
                // an InvalidOperationException being thrown with the message "Element is already the child of another element."
                foreach (var segment in figure.Segments)
                {
                    // I only impemented cloning the abstract PathSegments to one implementation, 
                    // the PolyLineSegment class. If your paths use other kinds of segments, you'll need
                    // to implement that kind of coding yourself.
                    var segmentAsPolyLineSegment = segment as PolyLineSegment;
                    if (segmentAsPolyLineSegment != null)
                    {
                        var newSegment = new PolyLineSegment();
                        foreach (var point in segmentAsPolyLineSegment.Points)
                        {
                            newSegment.Points.Add(point);
                        }

                        newFigure.Segments.Add(newSegment);
                    }

                    var segmentAsLineSegment = segment as LineSegment;
                    if (segmentAsLineSegment != null)
                    {
                        var newSegment = new LineSegment();
                        newSegment.Point = segmentAsLineSegment.Point;
                        newFigure.Segments.Add(newSegment);
                    }

                    var segmentAsArcSegment = segment as ArcSegment;
                    if (segmentAsArcSegment != null)
                    {
                        var newSegment = new ArcSegment();
                        newSegment.Point = segmentAsArcSegment.Point;
                        newSegment.SweepDirection = segmentAsArcSegment.SweepDirection;
                        newSegment.RotationAngle = segmentAsArcSegment.RotationAngle;
                        newSegment.IsLargeArc = segmentAsArcSegment.IsLargeArc;
                        newSegment.Size = segmentAsArcSegment.Size;
                        newFigure.Segments.Add(newSegment);
                    }
                }

                newPathGeometry.Figures.Add(newFigure);
            }

            return newPathGeometry;
        }

        /// <summary>
        /// Gets the radius.
        /// </summary>
        /// <returns></returns>
        private double GetRadius()
        {
            double result;
            if (this.ClientHeight < (this.ClientWidth - 50))
            {
                result = this.ClientHeight / 2;
            }
            else
            {
                result = (this.ClientWidth - 50) / 2;
            }

            return result - 10;
        }

        #endregion Methods
    }
}