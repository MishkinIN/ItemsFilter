### Внедрение в DataGrid через стиль (CategoriesView).
Нет необходимости переделывать все формы: просто измените стиль `DataGrid` по умолчанию. 
Для этого уже подготовлен словарь *ItemsFilterStyle.xaml*, который достаточно включить
в состав ресурсов проекта. После этого все элементы `DataGrid` в проекте 
приобретут в заголовке быстрый фильтр. Заодно вы обретете возможность настроить стиль
`ColumnFilter` в соответствии с индивидуальным дизайном Вашего приложения.
#### Как это выглядит
![DataGrid column filter](Picture/Pic5.gif "Рис.1")
#### Как использовать
В ресурсы для приложения добавьте ссылку на *ItemsFilterStyle.xaml*.

File *App.xaml*:
``` xaml
    <Application x:Class="Northwind.NET.Sample.App"
                 ...
    >
        <Application.Resources>
            <ResourceDictionary>
                <ResourceDictionary.MergedDictionaries>
                    ...
                    <ResourceDictionary Source="Themes/ItemsFilterStyle.xaml" />
		       ...
                </ResourceDictionary.MergedDictionaries>
            </ResourceDictionary>
        </Application.Resources>
    </Application>
```
#### Как это работает
Производится внедрение элемента управления `ColumnFilter`в заголовок `DataGrid` через переопределение 
стиля для `DataGridColumnHeader`.

File *ItemsFilterStyle.xaml*:
``` xaml
    <ResourceDictionary 
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                  ...  
                  xmlns:bsFilter="http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter"
                  ...>
    ...
        <Style x:Key="DataGridColumnHeaderStyle" TargetType="{x:Type DataGridColumnHeader}">
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="{x:Type DataGridColumnHeader}">
                        <Grid >
                            ...
                            <bsFilter:ColumnFilter 
                               ParentCollection="{Binding ItemsSource,
                               RelativeSource={RelativeSource FindAncestor,
             		                    AncestorType={x:Type DataGrid}}}" />
                            ...
                        </Grid>
                    </ControlTemplate>
                </Setter>
            </Setter>
        </Style>
        <Style TargetType="{x:Type DataGrid}">
            <Setter Property="ColumnHeaderStyle" 
                Value="{StaticResource DataGridColumnHeaderStyle}" />
        </Style>
    </ResourceDictionary>
```

[Назад](Examle1.EmployeesView.md "Использование готового элемента управления FilterDataGrid (EmployeesView)") <<
[Оглавление](Readme.md) >>
[Вперед](Examle3.ProductsView.md "Настройка внешнего вида фильтра. (ProductsView)")