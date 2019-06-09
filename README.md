# Charts.WPF
Modern UI (Metro) Charts for WPF.

# Changes:
Code ported to C#6. General bug fixes and optimizations for new frameworks.

#Screenshots
(Screenshots/light.png)

###Examples

```csharp
public class TestClass
{
    public string Category { get; set; }
    public int Number  { get; set; }
}

<chart:ChartSeries
    SeriesTitle="Errors"
    DisplayMember="Category"
    ValueMember="Number"    
    ItemsSource="{Binding Path=Errors}" />
```
   

fork of http://modernuicharts.codeplex.com/
  
