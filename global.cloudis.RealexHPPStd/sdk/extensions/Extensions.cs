using global.cloudis.RealexHPP.sdk.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.sdk.extensions
{
    public static class Extensions
    {
        public static T GetPropertyValue<T>(this IHPP instance, string propertyName) 
        {
            return (T)(instance.GetType().GetTypeInfo().GetDeclaredProperty(propertyName).GetValue(instance));
        }

        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetTypeInfo().GetDeclaredProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }
    }
}
