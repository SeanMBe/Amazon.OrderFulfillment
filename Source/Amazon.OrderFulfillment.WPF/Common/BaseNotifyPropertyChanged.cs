using System;
using System.ComponentModel;
using System.Linq.Expressions;
using MavenThought.Commons.Extensions;

namespace Amazon.OrderFulfillment.WPF.Common
{
    public abstract class BaseNotifyPropertyChanged : IBaseNotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected BaseNotifyPropertyChanged()
        {
            PropertyChanged = delegate { };
        }

        public void Raise<T>(Expression<Func<T>> propertyExpression)
        {
            var body = propertyExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("'propertyExpression' should be a member expression");

            var expression = body.Expression as ConstantExpression;
            if (expression == null)
                throw new ArgumentException("'propertyExpression' body should be a constant expression");

            var target = Expression.Lambda(expression).Compile().DynamicInvoke();

            var e = new PropertyChangedEventArgs(body.Member.Name);
            PropertyChanged(target, e);
        }

        public void Raise<T>(params Expression<Func<T>>[] propertyExpressions)
        {
            propertyExpressions.ForEach(Raise);
        }
    }
}