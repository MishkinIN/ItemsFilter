using BolapanControl.ItemsFilter.ViewModel;
using System;
using System.ComponentModel;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Defines the contract for a filter used by the FilteredCollection
    /// </summary>
    public interface IFilter {
        //TODO: Translate: Получает или задает значение определяющее участвует ли фильтр в фильтрации представления коллекции.
        /// <summary>
        /// Get or set value that indicates are filter include in filter function.
        /// </summary>
        bool IsActive {
            get;
            set;
        }

        /// <summary>
        /// Determines whether the specified target is matching the criteria.
        /// </summary>
        void IsMatch(FilterPresenter sender, FilterEventArgs e);
    }
    
}
