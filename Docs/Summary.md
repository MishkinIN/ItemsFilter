## Резюме
Библиотека *ItemsFilter* позволяет быстро и эффективно реализовывать интерактивные элементы 
пользовательского интерфейса для фильтрации отображаемой коллекции (Быстрый фильтр). 

В состав библиотеки входит готовый Control `ColumnFilter`, позволяющий подключать 
быстрый фильтр к свойствам простых и сложных типов (`int`, `real`, `string`, `bool`, `enum`, `object` и т.д.). 
В зависимости от типа свойства, `ColumnFilter` отображает следующий набор фильтров:
<table>
    <tr>
        <td><b>Property type</td>
        <td><b>Filters</td>
    </tr>
    <tr>
        <td>int, real, long etc.</td>
        <td>EqualFilter, LessOrEqualFilter, GreaterOrEqualFilter, RangeFilter</td>
    </tr>
    <tr>
        <td>IComparable</td>
        <td>EqualFilter, LessOrEqualFilter, GreaterOrEqualFilter, RangeFilter</td>
    </tr>
    <tr>
        <td>String</td>
        <td>EqualFilter, StringFilter</td>
    </tr>
    <tr>
        <td>Bool</td>
        <td>EqualFilter</td>
    </tr>
    <tr>
        <td>Object</td>
        <td>EqualFilter</td>
    </tr>
</table>

Включение `ColumnFilter` в `DataGrid` и настройка его внешнего вида доступно через стиль
и осуществляется без изменения разработанных форм. 

Создание пользовательских фильтров, реализующих специализированные условия фильтрации, 
и включение их в пользовательский интерфейс легко осуществляется в несколько строк кода.

Если требуется помощь в кастомизации под нужды Вашего проекта, обращайтесь к [контрибьютеру](mailto:Mishkin_Ivan@mail.ru) библиотеки *ItemsFilter*.

**Enjoy**!

[Назад](Examle7.CustomersView.md "Фильтрация элементов в TreeView. (CustomersView)") <<
[Оглавление](Readme.md)
