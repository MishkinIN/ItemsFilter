using BolapanControl.ItemsFilter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolapanControl.ItemsFilter.ViewModel {
    /// <summary>
    /// Represent FilterControlVm without content
    /// </summary>
    public class EmptyFilterControlVm :FilterControlVm {
        internal EmptyFilterControlVm():base() {

        }
        protected override void InsertItem(int index, Filter filter) {
        }
        protected override void MoveItem(int oldIndex, int newIndex) {
        }
        protected override void RemoveItem(int index) {
        }
        protected override void SetItem(int index, Filter filter) {
        }
    }
}
