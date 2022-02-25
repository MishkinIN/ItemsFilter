// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BolapanControl.ItemsFilter {
    /// <summary>
    /// Provides data for the <see cref="Filtered"/> event
    /// </summary>
    public class FilteredEventArgs:EventArgs {
        private readonly ICollectionView cv;
        internal FilteredEventArgs(ICollectionView cv) {
            this.cv = cv;
        }
        /// <summary>
        /// Filtered CollectionView.
        /// </summary>
        public ICollectionView CollectionView {
            get { return cv; }
        }
    }
}
