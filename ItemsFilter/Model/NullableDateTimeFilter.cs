﻿// ****************************************************************************
// <author>Jonas Tampier</author>
// <email>jonas@tampier.de</email>
// <date>02.10.2017</date>
// <project>ItemsFilter</project>
// <license> GNU Lesser General Public License version 3 (LGPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.View;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;

namespace BolapanControl.ItemsFilter.Model {


    /// <summary>
    /// Defines the nullable DateTime filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [View(typeof(DateTimeFilterView))]
    public class NullableDateTimeFilter : NullableRangeFilter<DateTime>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullableDateTimeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public NullableDateTimeFilter(ItemPropertyInfo propertyInfo)
            : base(propertyInfo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullableDateTimeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="CompareFrom">Minimum value.</param>
        /// <param name="CompareTo">Maximum value.</param>
        public NullableDateTimeFilter(ItemPropertyInfo propertyInfo, DateTime CompareFrom, DateTime CompareTo)
            : base(propertyInfo, CompareFrom, CompareTo)
        {
        }
    }
}
