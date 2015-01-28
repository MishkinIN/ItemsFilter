// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BolapanControl.ItemsFilter.Design {
    class MultiValueFilterModel : IMultiValueFilter {

        private string[] values;
        private ObservableCollection<object> selectedValues;
        public MultiValueFilterModel() {
            values = new string[]{
                "Item 1",
                "Item2"
            };
            selectedValues = new ObservableCollection<object>();
            selectedValues.Add(values[0]);
        }
        public System.Collections.IEnumerable AvailableValues {
            get {
                return values;
            }
            set {
                ;
            }
        }

        public System.Collections.ObjectModel.ReadOnlyObservableCollection<object> SelectedValues {
            get { return new System.Collections.ObjectModel.ReadOnlyObservableCollection<object>(selectedValues); }
        }

        public void SelectedValuesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            ;
        }

        public void Attach(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            ;
        }

        public void Detach(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            ;
        }

        public void Attach(BolapanControl.ItemsFilter.ViewModel.FilterControlVm vm) {
            ;
        }

        public void Detach(BolapanControl.ItemsFilter.ViewModel.FilterControlVm vm) {
            ;
        }

        public bool IsActive {
            get {
                return true;
            }
            set {
                ;
            }
        }

        public string Name {
            get {
                return "Equality:";
            }
            set {
                ;
            }
        }

        public void IsMatch(BolapanControl.ItemsFilter.FilterPresenter sender, BolapanControl.ItemsFilter.FilterEventArgs e) {
            ;
        }
    }
}
