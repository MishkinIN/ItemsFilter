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
                if (!String.IsNullOrEmpty(value)) {
                    var b64string = value;
                    byte[] buffer = new byte[((b64string.Length * 3) + 3) / 4 -(b64string.Length > 0 && b64string[b64string.Length - 1] == '=' ?
                        b64string.Length > 1 && b64string[b64string.Length - 2] == '=' ?
                        2 : 1 : 0)];

                    int written;
                    bool success = Convert.TryFromBase64String(b64string, buffer, out written);
                    if (success) {
                        base.Picture = buffer;
                    }
                    else
                        base.Picture = null;
                }
            }
        }
    }
}
