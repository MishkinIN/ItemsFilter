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

namespace Northwind.NET.Sample.SampleData {
    public class Category : Northwind.NET.EF6Model.Category {
        public string PictureAsString {
            get {
                if (base.Picture != null) {
                    return Convert.ToBase64String(base.Picture);
                }
                return String.Empty;
            }
            set {
                if (String.IsNullOrEmpty(value)) {
                    base.Picture = Convert.FromBase64String(value);
                }
                else
                    base.Picture = null;
            }
        }
    }
}
