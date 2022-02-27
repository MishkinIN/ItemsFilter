using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.ViewModel {
    /// <summary>
    /// Represent FilterControlVm without content
    /// </summary>
    public class EmptyFilterControlVm : FilterControlVm {
        internal EmptyFilterControlVm() : base() {

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
