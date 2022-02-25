// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Define a filter that  using a collection of values ​​for the selection criteria.
    /// </summary>
    public interface IMultiValueFilter : IFilter {
        IEnumerable AvailableValues {
            get;
            set;
        }
        /// <summary>
        /// Collection of values ​​involved in composition the selection criteria.
        /// </summary>
        ReadOnlyObservableCollection<object> SelectedValues { get; }
        /// <summary>
        /// Receive SelectionChanged event for synchronize the collection SelectedValues.
        /// </summary>
        void SelectedValuesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e);
        
    }
}
