### Использование готового элемента управления FilterDataGrid (EmployeesView).
Чтобы добавить быстрый фильтр в заголовок `DataGrid` без лишних хлопот, 
просто примените в форме `FilterDataGrid`, который отличается от стандартного `DataGrid` только одним
– в шаблон заголовка столбца уже внедрен `ColumnFilter`:
#### Как это выглядит
![DataGrid column filter](Picture/Pic1.gif "Рис.1")
#### Как использовать
``` xaml
    <UserControl x:Class="Northwind.NET.Sample.View.EmployeesView"
                 ...
                 xmlns:bsFilter="http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter"
                 ...>
    <bsFilter:FilterDataGrid  ItemsSource="{Binding}" ... />
    </UserControl>
```
#### Как это работает
Элемент управления `ColumnFilter` внедрен в заголовок столбца `DataGrid`. При загрузке `ColumnFilter`
извлекает значение привязки для столбца, в котором он расположен, и экземпляр представления коллекции, привязанной к родительскому `DataGrid`
через свойство `DataGrid.ItemsSource`. Исходя из извлеченных параметров, формируются модели фильтров,
подходящих для столбца, и отображаются в `ColumnFilter`. 
Если для столбца информацию о привязке извлечь невозможно (например, для `DataGridTemplateColumn`),
привязку `ColumnFilter` можно уточнить с помощью присоединенного свойства
`bsFilter:ColumnFilter.BindingPath="<имя_свойства_элемента_коллекции>"`.


[Назад](ALittleBackground.md "Немного предыстории: Предпосылки для использования. Детали реализации."). <<
[Оглавление](Readme.md) >>
[Вперед](Examle2.CategoriesView.md "Внедрение в DataGrid через стиль (CategoriesView)")
