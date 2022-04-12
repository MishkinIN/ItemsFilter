# ItemsFilter
ItemsFilter provides the developer with a means to implement a custom filter control: both in standard WPF UI elements (DataGrid, ListView, etc.), and in any newly created CustomControls that render the collection.

For example, a standard DataGrid might look like this:

![DataGrid column filter](Picture\Pic1.gif)

Here, through the style, a button for quick filtering by the contents of the column is added to the header of the standard WPF DataGrid (approximately as it can be done in Excel or in the standard Windows Explorer in table mode). 

But it is just as easy to add controls for a quick filter to other elements that inherit from ItemsControl: ListBox, ComboBox, TreeView, Menu, ToolBar etc.

To use it in an application, it is enough to specify a reference to the ItemsFilter.net6 assembly in the project and inject the FilterView through the style or CustomControl.
