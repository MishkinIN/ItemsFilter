﻿// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Collections.Generic;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Define a class that represent set of filter initializers.
    /// </summary>
    public class FilterInitializersManager : List<FilterInitializer>, IList<FilterInitializer>, IEnumerable<FilterInitializer> {
        private static readonly Lazy<FilterInitializersManager> _lzdefault = new(GetDefaults);
        /// <summary>
        /// Represent default instance of FilterInitializersManager that include common used initializers.
        /// </summary>
        public static IEnumerable<FilterInitializer> Default {
            get {
                return _lzdefault.Value;
            }
        }
        private static FilterInitializersManager GetDefaults() {
            return new FilterInitializersManager
                    {
                        new EqualFilterInitializer(),
                        new LessOrEqualFilterInitializer(),
                        new GreaterOrEqualFilterInitializer(),
                        new RangeFilterInitializer(),
                        new StringFilterInitializer(),
                        new EnumFilterInitializer(),
                    };
        }
        /// <summary>
        /// Create empty instance of FilterInitializersManager.
        /// </summary>
        public FilterInitializersManager() {
        }
        /// <summary>
        /// Create instance of FilterInitializersManager and add initializers.
        /// </summary>
        /// <param name="initializers">Enumerable of IFilterInitializer to add.</param>
        public FilterInitializersManager(IEnumerable<FilterInitializer> initializers)
            : base() {
            AddRange(initializers);
        }
    }
}
