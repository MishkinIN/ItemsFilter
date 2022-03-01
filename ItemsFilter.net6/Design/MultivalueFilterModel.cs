// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System.Collections.ObjectModel;

namespace BolapanControl.ItemsFilter.Design {
    internal class MultiValueFilterModel : IMultiValueFilter {

        private readonly string[] values;
        private readonly ObservableCollection<object> selectedValues;
        public MultiValueFilterModel() {
            values = new string[]{
                "Item 1",
                "Item2"
            };
            ObservableCollection<object> observableCollection = new() {
                values[0]
            };
            selectedValues = observableCollection;
        }
        public System.Collections.IEnumerable AvailableValues {
            get {
                return values;
            }
            set {
                ;
            }
        }

        public ReadOnlyObservableCollection<object> SelectedValues {
            get { return new ReadOnlyObservableCollection<object>(selectedValues); }
        }

        public void SelectedValuesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            ;
        }

        //public void Attach(FilterPresenter presenter) {
        //    ;
        //}

        //public void Detach(FilterPresenter presenter) {
        //    ;
        //}

        //public void Attach(ViewModel.FilterControlVm vm) {
        //    ;
        //}

        //public void Detach(ViewModel.FilterControlVm vm) {
        //    ;
        //}

        public bool IsActive {
            get {
                return true;
            }
            set {
                ;
            }
        }

#pragma warning disable CA1822 // Mark members as static
        public string Name {
#pragma warning restore CA1822 // Mark members as static
            get {
                return "Equality:";
            }
            set {
                ;
            }
        }

        public void IsMatch(FilterPresenter sender, FilterEventArgs e) {
            ;
        }
    }
}
