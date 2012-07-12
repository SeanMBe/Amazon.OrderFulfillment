using System;
using System.Windows;

namespace Amazon.OrderFulfillment.WPF.DependencyProperties
{
    public static class ResourceKeyBindingPropertyFactory
    {
        public static DependencyProperty CreateResourceKeyBindingProperty(DependencyProperty boundProperty, Type ownerClass)
        {
            var property = DependencyProperty.RegisterAttached(
                boundProperty.Name + "ResourceKeyBinding",
                typeof(object),
                ownerClass,
                new PropertyMetadata(null, (dp, e) =>
                                               {
                                                   var element = dp as FrameworkElement;
                                                   if (element == null)
                                                   {
                                                       return;
                                                   }

                                                   element.SetResourceReference(boundProperty, e.NewValue);
                                               }));

            return property;
        }
    }
}