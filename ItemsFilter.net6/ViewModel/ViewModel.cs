// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace BolapanControl.ItemsFilter.ViewModel {
    /// <summary>
    /// Provide INotifyPropertyChanged implementation for derived class.
    /// </summary>
    public class ViewModel : INotifyPropertyChanged {

        #region Члены INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName) {
            VerifyPropertyName(propertyName);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName) {
            var myType = this.GetType();

#if NETFX_CORE
            if (!string.IsNullOrEmpty(propertyName)
                && myType.GetTypeInfo().GetDeclaredProperty(propertyName) == null)
            {
                throw new ArgumentException("Property not found", propertyName);
            }
#else
            if (!string.IsNullOrEmpty(propertyName)
                && myType.GetProperty(propertyName) == null) {
#if !SILVERLIGHT

                if (this is ICustomTypeDescriptor descriptor) {
                    if (descriptor.GetProperties()
                        .Cast<PropertyDescriptor>()
                        .Any(property => property.Name == propertyName)) {
                        return;
                    }
                }
                throw new ArgumentException("Property not found", propertyName);
            }
#endif
#endif
        }

    }
}
