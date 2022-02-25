// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BolapanControl.ItemsFilter {
    /// <summary>
    /// Provides information and event data that is associated with the BolapanControl.ItemsFilter.FilterPresenter.Filter event.
    /// </summary>
    public class FilterEventArgs : EventArgs {
        private object item;

        internal FilterEventArgs(object item) {
            this.item = item;
            Accepted = true;
        }
        /// <summary>
        /// Gets or sets a value that indicates whether the item passes the filter.
        /// Returns:
        ///     true if the item passes the filter; otherwise, false. The default is true.
        /// </summary>
        public bool Accepted { get; set; }
        /// <summary>
        /// Gets the object that the filter should test.
        /// Returns:
        /// The object that the filter should test. The default is null.
        /// </summary>
        public object Item {
            get { return item; }
        }
    }
}
