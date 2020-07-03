# Charts.WPF
Modern UI Charts for WPF.

```csharp
Install-Package Charts.WPF -Version 1.0.0
```

# Release Notes:
Code ported for the newest .NET frameworks. General bug fixes and optimizations made.

### Support me

If you have found this project helpful, either as a library that you use or as a learning tool, please consider buying me a coffee:

<a href="https://www.buymeacoffee.com/amendonca" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-red.png" alt="Buy Me A Coffee" style="height: 51px !important;width: 217px !important;" ></a>

### Screenshots

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
