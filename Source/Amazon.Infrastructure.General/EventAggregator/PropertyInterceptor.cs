using System;
using System.Collections.Generic;
using Castle.DynamicProxy;

namespace Amazon.Infrastructure.General.EventAggregator
{
    public class PropertyInterceptor : IInterceptor
    {
        private readonly IDictionary<string, object> _properties = new Dictionary<string, object>();

        public void Intercept(IInvocation invocation)
        {
            var key = invocation.Method.Name.Substring(4);

            if (invocation.Method.Name.StartsWith("set_"))
            {
                _properties[key] = invocation.Arguments[0];

                return;
            }

            if (invocation.Method.Name.StartsWith("get_"))
            {
                object value;
                
                _properties.TryGetValue(key, out value);

                if (value == null && invocation.TargetType.IsValueType)
                {
                    value = Activator.CreateInstance(invocation.TargetType);
                }

                invocation.ReturnValue = value;
                return;
            }
            
            invocation.Proceed();
        }
    }
}