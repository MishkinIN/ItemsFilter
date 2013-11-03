using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BolapanControl.ItemsFilter
{
    //TODO: CheckTranslate: Представляет стандартный DataGrid с включенным в шаблок заголовка столбца фильтром.
    /// <summary>
    /// Define a standard DataGrid with the included ColumnFilter in the column header template.
    /// </summary>
    public class FilterDataGrid:DataGrid
    {
        static FilterDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterDataGrid),
          new FrameworkPropertyMetadata(typeof(FilterDataGrid)));
      
        }
    }
}
