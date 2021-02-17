# Charts.WPF
Modern UI Charts for WPF.

```csharp
Install-Package Charts.WPF -Version 1.0.0
```

# Release Notes:
Code ported for the newest .NET frameworks. General bug fixes and optimizations made.

### Screenshots
![Default](https://github.com/mendonca-andre/Charts.WPF/blob/master/Screenshots/default.png)

![Light Theme](https://github.com/mendonca-andre/Charts.WPF/blob/master/Screenshots/light.png)

![Dark Theme](https://github.com/mendonca-andre/Charts.WPF/blob/master/Screenshots/dark.png)

### Examples

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
  
Fork from http://modernuicharts.codeplex.com.
