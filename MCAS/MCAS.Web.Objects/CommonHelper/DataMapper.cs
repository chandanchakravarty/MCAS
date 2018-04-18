using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;

namespace MCAS.Web.Objects.CommonHelper
{
    public class DataMapper
    {
        #region Public static methods

        public static void Map(object source, object target, bool suppressExceptions, params string[] ignoreList)
        {
            List<string> ignore = new List<string>(ignoreList);
            foreach (var propertyName in GetPropertyNames(source.GetType()))
            {
                if (!ignore.Contains(propertyName))
                {
                    try
                    {
                        object value = source.GetType().GetProperty(propertyName).GetValue(source, null);
                        target.GetType().GetProperty(propertyName).SetValue(target,value,null);
                    }
                    catch (Exception ex)
                    {
                        if (!suppressExceptions)
                            throw new ArgumentException(
                                String.Format("Data Mapping Exception {0}", propertyName), ex);
                    }
                }
            }
        }

        private static IList<string> GetPropertyNames(Type sourceType)
        {
            List<string> result = new List<string>();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(sourceType);
            foreach (PropertyDescriptor item in props)
                if (item.IsBrowsable)
                    result.Add(item.Name);
            return result;
        }

        #endregion
    }
}

