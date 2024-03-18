using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WellDesignedAPI.Common.Helpers
{
    public static class EntityAndSqlPropertyHelper
    {
        public static bool IsPropertyNameValid<T>(string propertyName)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            return properties.Any(prop => prop.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
        }

        public static bool PropertyIsDateTimeType<T>(string propertyName)
        {
            try
            {
                Type type = typeof(T);
                PropertyInfo property = type.GetProperty(propertyName);

                //prop doesn't exist
                if (property == null)
                    return false;

                return property.PropertyType == typeof(DateTime);
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool PropertyIsDecimalType<T>(string csharpPropertyName)
        {
            try
            {
                var propertyInfo = typeof(T).GetProperty(csharpPropertyName);

                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Property '{csharpPropertyName}' not found in type '{typeof(T).Name}'.");
                }

                var columnAttribute = propertyInfo.GetCustomAttributes<ColumnAttribute>().FirstOrDefault();

                return columnAttribute != null && columnAttribute.TypeName.Contains("DECIMAL");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool PropertyIsIntegerType<T>(string csharpPropertyName)
        {
            try
            {
                var propertyInfo = typeof(T).GetProperty(csharpPropertyName);

                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Property '{csharpPropertyName}' not found in type '{typeof(T).Name}'.");
                }

                var columnAttribute = propertyInfo.GetCustomAttributes<ColumnAttribute>().FirstOrDefault();

                return columnAttribute != null && columnAttribute.TypeName.Contains("INT");
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
