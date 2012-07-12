using System.Windows;
using System.Windows.Controls;

namespace Amazon.OrderFulfillment.WPF.DependencyProperties
{
    public static class ResourceKeyBindings
    {
        public static DependencyProperty SourceResourceKeyBindingProperty = ResourceKeyBindingPropertyFactory.CreateResourceKeyBindingProperty(
            Image.SourceProperty,
            typeof(ResourceKeyBindings));

        public static void SetSourceResourceKeyBinding(DependencyObject dp, object resourceKey)
        {
            dp.SetValue(SourceResourceKeyBindingProperty, resourceKey);
        }

        public static object GetSourceResourceKeyBinding(DependencyObject dp)
        {
            return dp.GetValue(SourceResourceKeyBindingProperty);
        }
    }
}