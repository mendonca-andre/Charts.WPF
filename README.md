# Charts.WPF
Charts for WPF
### fork of http://modernuicharts.codeplex.com/

### Changes:
Code ported to C#6. General bug fixes and optimizations for new frameworks.

###Examples
public class TestClass
{
    public string Category { get; set; }
    public int Number  { get; set; }
    public Brush Brush { get; set; } // <--
}
Then add that member to the ChartSeries xaml

<chart:ChartSeries
    SeriesTitle="Errors"
    DisplayMember="Category"
    ValueMember="Number"
    BrushMember="Brush" // <--
    ItemsSource="{Binding Path=Errors}" />
    
