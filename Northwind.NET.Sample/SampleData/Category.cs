using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.SampleData {
    class Category:Northwind.NET.EF6Model.Category {
        public string PictureAsString {
            get {
                return Convert.ToBase64String(base.Picture);
            }
            set { base.Picture = Convert.FromBase64String(value); }
        }
    }
}
