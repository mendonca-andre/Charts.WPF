namespace Charts.WPF.Core
{
    using System;
    using System.Reflection;

    public static class Extensions
    {
        public static PropertyInfo[] GetAllProperties(this Type type)
        {
#if NETFX_CORE
            return type.GetRuntimeProperties().ToArray();
#else            
    #if SILVERLIGHT
            return type.GetProperties();
#else
            return type.GetProperties();
#endif
#endif
        }
    }
}
