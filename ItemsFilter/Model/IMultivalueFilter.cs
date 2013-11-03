using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BolapanControl.ItemsFilter.Model {
    //TODO: Translate: Фильтр, использующий в критерии отбора коллекцию значений.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMultiValueFilter : IFilter {
        IEnumerable AvailableValues {
            get;
            set;
        }
        //TODO: Translate: Коллеция значений, котоая используется в критерии отбора фильтра.
        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<object> SelectedValues { get; }
        //TODO: Translate: Синхронизирует коллекцию SelectedValues.
        /// <summary>
        /// Change filtered values.
        /// </summary>
        void SelectedValuesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e);
        
    }
}
