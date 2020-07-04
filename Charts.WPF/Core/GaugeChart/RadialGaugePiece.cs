namespace Charts.WPF.Core.GaugeChart
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
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    public enum EasingFunction
    {
        Linear = 0,
        EaseInQuad = 1,
        EaseOutQuad = 2, 
        EaseInOutQuad = 3,
        EaseInCubic = 4,
        EaseOutCubic = 5,
        EaseInOutCubic = 6,
        EaseInQuart = 7,
        EaseOutQuart = 8,
        EaseInExpo = 9,
        EaseOutExpo = 10
    }

    [TemplateVisualState(Name = StateSelectionSelected, GroupName = GroupSelectionStates)]
    public class RadialGaugePiece : PieceBase
    {        
        #region Fields

        private Path slice;
        private DispatcherTimer timer;

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(RadialGaugePiece),
            new PropertyMetadata(0.0));        
        
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(RadialGaugePiece),
            new PropertyMetadata(0.0, UpdatePie));
        
        public static readonly DependencyProperty AnimatedValueProperty =
            DependencyProperty.Register("AnimatedValue", typeof(double), typeof(RadialGaugePiece),
            new PropertyMetadata(0.0, OnAnimatedValueChanged));

        public static readonly DependencyProperty FormattedAnimatedValueProperty =
            DependencyProperty.Register("FormattedAnimatedValue", typeof(string), typeof(RadialGaugePiece),
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty GeometryProperty =
            DependencyProperty.Register("Geometry", typeof(Geometry), typeof(RadialGaugePiece),
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty BackgroundGeometryProperty =
            DependencyProperty.Register("BackgroundGeometry", typeof(Geometry), typeof(RadialGaugePiece),
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty SelectionGeometryProperty =
            DependencyProperty.Register("SelectionGeometry", typeof(Geometry), typeof(RadialGaugePiece),
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty MouseOverGeometryProperty =
            DependencyProperty.Register("MouseOverGeometry", typeof(Geometry), typeof(RadialGaugePiece),
            new PropertyMetadata(null));
 
        #endregion Fields
        
        #region Constructors

        static RadialGaugePiece()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadialGaugePiece), new FrameworkPropertyMetadata(typeof(RadialGaugePiece)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadialGaugePiece"/> class.
        /// </summary>
        public RadialGaugePiece()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(RadialGaugePiece);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(RadialGaugePiece);
#endif
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timer_Tick;

            this.Loaded += this.RadialGaugePiece_Loaded;
        }

        private static void UpdatePie(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as RadialGaugePiece).UpdatePie(); 
        }

        void RadialGaugePiece_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdatePie();          
        }

        double animationCounter;
        double animationStartValue;

        private void UpdatePie()
        {
            // Value is value we need to show
            // check if Value is equal to AnimatedValue
            if (this.AnimatedValue != this.Value)
            {
                // we need an animation to achieve the Value
                // we take duration
                this.animationCounter = 0;
                this.animationStartValue = this.AnimatedValue;

                this.timer.Interval = TimeSpan.FromMilliseconds(33.3);
                this.timer.Start();
                this.Tick();  // initial tick without waiting
            }
        }

        private void Tick()
        {
            if (double.IsInfinity(this.Value))
            {
            }

            if (this.AnimatedValue != this.Value)
            {
                // recalc the animatedvalue
                var t = this.animationCounter;
                var b = this.animationStartValue;
                var c = this.Value - this.animationStartValue;
                double d = 30;

                this.AnimatedValue = this.getFormula(EasingFunction.EaseOutQuart, t, b, d, c);
                this.DrawGeometry();
                this.animationCounter++;
            }
            else
            {
                this.AnimatedValue = this.Value;
                this.DrawGeometry();
                this.timer.Stop();
            }
        }

        private double getFormula(EasingFunction animType, double t, double b, double d, double c)
        {
            // adjust formula to selected algoritm from combobox
            switch (animType)
            {
                case EasingFunction.Linear:
                    // simple linear tweening - no easing 
                    return (c * t / d + b);

                case EasingFunction.EaseInQuad:
                    // quadratic (t^2) easing in - accelerating from zero velocity
                    return (c * (t /= d) * t + b);

                case EasingFunction.EaseOutQuad:
                    // quadratic (t^2) easing out - decelerating to zero velocity
                    return (-c * (t = t / d) * (t - 2) + b);

                case EasingFunction.EaseInOutQuad:
                    // quadratic easing in/out - acceleration until halfway, then deceleration
                    if ((t /= d / 2) < 1) return (c / 2 * t * t + b); else return (-c / 2 * ((--t) * (t - 2) - 1) + b);

                case EasingFunction.EaseInCubic:
                    // cubic easing in - accelerating from zero velocity
                    return (c * (t /= d) * t * t + b);

                case EasingFunction.EaseOutCubic:
                    // cubic easing in - accelerating from zero velocity
                    return (c * ((t = t / d - 1) * t * t + 1) + b);

                case EasingFunction.EaseInOutCubic:
                    // cubic easing in - accelerating from zero velocity
                    if ((t /= d / 2) < 1) return (c / 2 * t * t * t + b); else return (c / 2 * ((t -= 2) * t * t + 2) + b);

                case EasingFunction.EaseInQuart:
                    // quartic easing in - accelerating from zero velocity
                    return (c * (t /= d) * t * t * t + b);

                case EasingFunction.EaseOutQuart:
	                return (-c * ((t = t / d - 1) * t * t * t - 1) + b);

                case EasingFunction.EaseInExpo:
                    // exponential (2^t) easing in - accelerating from zero velocity
                    if (t == 0) return b; else return (c * Math.Pow(2, (10 * (t / d - 1))) + b);

                case EasingFunction.EaseOutExpo:
                    // exponential (2^t) easing out - decelerating to zero velocity
                    if (t == d) return (b + c); else return (c * (-Math.Pow(2, -10 * t / d) + 1) + b);

                default:
                    return 0;
            }
        }
      
#if NETFX_CORE
        private void timer_Tick(object sender, object e)
        {
            Tick();
        }
#else
        private void timer_Tick(object sender, EventArgs e)
        {
            this.Tick();
        }

#endif

        #endregion Constructors
        
        #region Properties

        public Geometry Geometry
        {
            get => (Geometry)this.GetValue(GeometryProperty);
            set => this.SetValue(GeometryProperty, value);
        }

        public Geometry BackgroundGeometry
        {
            get => (Geometry)this.GetValue(BackgroundGeometryProperty);
            set => this.SetValue(BackgroundGeometryProperty, value);
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

        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        public double AnimatedValue
        {
            get => (double)this.GetValue(AnimatedValueProperty);
            set => this.SetValue(AnimatedValueProperty, value);
        }

        public double FormattedAnimatedValue
        {
            get => (double)this.GetValue(FormattedAnimatedValueProperty);
            set => this.SetValue(FormattedAnimatedValueProperty, value);
        }
        
        #endregion Properties

        #region Methods

        private static void OnAnimatedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as RadialGaugePiece).UpdateFormattedValue();
        }

        private void UpdateFormattedValue()
        {
            this.SetValue(FormattedAnimatedValueProperty, this.AnimatedValue.ToString("F0"));
        }

        protected override void InternalOnApplyTemplate()
        {
            this.slice = this.GetTemplateChild("Slice") as Path;
            this.RegisterMouseEvents(this.slice);
        }

        protected override void DrawGeometry(bool withAnimation = true)
        {    
            try
            {
                // Children.Clear();
                if (this.ClientWidth <= 0.0)
                {
                    return;
                }

                if (this.ClientHeight <= 0.0)
                {
                    return;
                }
                
                double m_startpercent = 0;
                var m_endpercent = this.AnimatedValue; // Value;

                var center = this.GetCenter();

                var startAngle = (360 / 100.0) * m_startpercent;
                var endAngle = (360 / 100.0) * m_endpercent;
                var radius = this.GetRadius();
                var isLarge = (endAngle - startAngle) > 180.0;

                var segmentPathData = this.LayoutSegment(startAngle, endAngle, radius, 0.50, center, true);
                if (segmentPathData != null)
                {
                    this.SetValue(GeometryProperty, this.CloneDeep(segmentPathData as PathGeometry));
                    this.SetValue(SelectionGeometryProperty, this.CloneDeep(segmentPathData as PathGeometry));
                }

                var segmentPathDataBackground = this.LayoutSegment(endAngle, 360, radius, 0.50, center, true);
                if (segmentPathDataBackground != null)
                {
                    this.SetValue(BackgroundGeometryProperty, this.CloneDeep(segmentPathDataBackground as PathGeometry));
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
                
        private Geometry LayoutSegment(double startAngle, double endAngle, double radius, double gapScale, Point center, bool isDoughnut)
        {
            try
            {
                if (startAngle > 360)
                {
                    return null;
                }

                if (endAngle > 360)
                {
                    return null;
                }

                if ((startAngle == 0.0) && (endAngle == 0.0))
                {
                    return null;
                }

                if (endAngle >= 359 )
                {
                    endAngle = 359;    // pie disappears if endAngle is 360                 
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
                    segments.Add(new ArcSegment
                                     {
                        Size = new Size(gapRadius, gapRadius),
                        Point = C,
                        SweepDirection = SweepDirection.Clockwise,
                        IsLargeArc = isReflexAngle
                    });
                }

                segments.Add(new LineSegment { Point = D });
                segments.Add(new ArcSegment
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

                return segmentPath.Data;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return null;
        }
                        

        /// <summary>
        /// Gets the center.
        /// </summary>
        /// <returns></returns>
        private Point GetCenter()
        {
            return new Point(this.ClientWidth / 2, this.ClientHeight/2);
        }

        private PathGeometry CloneDeep(PathGeometry pathGeometry)
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
            if (this.ClientHeight < this.ClientWidth)
            {
                result = this.ClientHeight / 2;
            }
            else
            {
                result = this.ClientWidth / 2;
            }

            return result;
        }

        #endregion Methods
    }
}