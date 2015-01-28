// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using GalaSoft.MvvmLight.Command;
using Northwind.NET.EF6Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Northwind.NET.Sample.ViewModel {
    public class OrdersVm : INotifyPropertyChanged {
        private readonly CollectionViewSource cvs = new CollectionViewSource();
        private readonly ICollectionView ordersView;

        private readonly ICommand moveFirstCommand, moveToPreviousCommand, moveToNextCommand, moveToNewCommand, moveToLastCommand;

        public OrdersVm() {
            try {
                cvs.Source = Workspace.This.Orders
                        .OrderBy(ord => ord.Employee == null ? -1 : ord.Employee.ID)
                        .Select(ord => ord)
                        .ToList();
            }
            catch (Exception ex) {
                App.LogException(ex);
            }
            ordersView = cvs.View;
            moveFirstCommand = new RelayCommand(MoveToFirst, CanMoveToFirst);
            moveToPreviousCommand = new RelayCommand(MoveToPrevious, CanMoveToPrevious);
            moveToNextCommand = new RelayCommand(MoveToNext, CanMoveToNext);
            moveToNewCommand = new RelayCommand(MoveToNew);
            moveToLastCommand = new RelayCommand(MoveToLast, CanMoveToLast);

        }
        public ICollectionView OrdersCollectionView {
            get { return ordersView; }
        }
        public ICommand MoveToLastCommand {
            get { return moveToLastCommand; }
        }
        public ICommand MoveToNewCommand {
            get { return moveToNewCommand; }
        }
        public ICommand MoveToNextCommand {
            get { return moveToNextCommand; }
        }
        public ICommand MoveToPreviousCommand {
            get { return moveToPreviousCommand; }
        }
        public ICommand MoveToFirstCommand {
            get { return moveFirstCommand; }
        }

        private bool CanMoveToLast() {
            return ordersView != null && !ordersView.IsCurrentAfterLast;
        }

        private void MoveToLast() {
            ordersView.MoveCurrentToLast();
        }

        private void MoveToNew() {

            ((ObservableCollection<Order>)(ordersView.SourceCollection))
                .Add(new Order
                {
                    Employee = ((Order)(ordersView.CurrentItem)).Employee,
                    ID = Workspace.This.Orders.Select(order => order.ID).Max(),
                    OrderDate = DateTime.Now
                });

        }

        private bool CanMoveToNext() {
            return ordersView != null && !ordersView.IsCurrentAfterLast;
        }

        private void MoveToNext() {
            ordersView.MoveCurrentToNext();
            if (ordersView.IsCurrentAfterLast)
                ordersView.MoveCurrentToLast();
        }

        private bool CanMoveToPrevious() {
            return ordersView != null && ordersView.CurrentPosition > 0;
        }

        private void MoveToPrevious() {
            ordersView.MoveCurrentToPrevious();
        }

        private bool CanMoveToFirst() {
            return ordersView != null && ordersView.CurrentPosition > 0;
        }

        private void MoveToFirst() {
            ordersView.MoveCurrentToFirst();
        }
        #region Члены INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName) {
            VerifyPropertyName(propertyName);

            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        protected void VerifyPropertyName(string propertyName) {
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
                var descriptor = this as ICustomTypeDescriptor;

                if (descriptor != null) {
                    if (descriptor.GetProperties()
                        .Cast<PropertyDescriptor>()
                        .Any(property => property.Name == propertyName)) {
                        return;
                    }
                }
#endif

                throw new ArgumentException("Property not found", propertyName);
            }
#endif
        }
        #endregion

    }
}
