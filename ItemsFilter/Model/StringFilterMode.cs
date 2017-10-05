// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.View;
using System.ComponentModel;

namespace BolapanControl.ItemsFilter.Model {
    
    /// <summary>
    /// String filter compare mode.
    /// </summary>
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum StringFilterMode {
        StartsWith,
        EndsWith,
        Contains,
        Equals
    }
}
