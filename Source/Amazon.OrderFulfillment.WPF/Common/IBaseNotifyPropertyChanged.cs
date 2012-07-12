using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Amazon.OrderFulfillment.WPF.Common
{
    public interface IBaseNotifyPropertyChanged: INotifyPropertyChanged
    {
        void Raise<T>(Expression<Func<T>> propertyExpression);
        void Raise<T>(params Expression<Func<T>>[] propertyExpressions);
    }
}