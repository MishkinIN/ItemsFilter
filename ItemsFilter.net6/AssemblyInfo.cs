using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: ComVisible(false)]
[assembly: InternalsVisibleTo($"ItemsFilter.net6.Test")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsDefinition("http://schemas.bolapansoft.com/xaml/Controls", "BolapanControl")]
[assembly: XmlnsPrefix("http://schemas.bolapansoft.com/xaml/Controls", "bsControl")]

[assembly: XmlnsDefinition("http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter", "BolapanControl.ItemsFilter")]
[assembly: XmlnsDefinition("http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter", "BolapanControl.ItemsFilter.View")]
[assembly: XmlnsDefinition("http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter", "BolapanControl.ItemsFilter.ViewModel")]
[assembly: XmlnsDefinition("http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter", "BolapanControl.ItemsFilter.Initializer")]
[assembly: XmlnsDefinition("http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter", "BolapanControl.ItemsFilter.Model")]
[assembly: XmlnsPrefix("http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter", "bsFilter")]
