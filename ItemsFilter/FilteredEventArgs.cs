using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BolapanControl.ItemsFilter {
    //TODO:Translate: Представляет аргументы события изменения фильтра коллекции.
    /// <summary>
    /// 
    /// </summary>
    public class FilteredEventArgs:EventArgs {
        private readonly ICollectionView cv;
        internal FilteredEventArgs(ICollectionView cv) {
            this.cv = cv;
        }
        /// <summary>
        /// Filtered Collection view.
        /// </summary>
        public ICollectionView CollectionView {
            get { return cv; }
        }
    }
}
