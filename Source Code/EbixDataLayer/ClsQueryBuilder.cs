/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	30-03-2010
<End Date			: -	
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/

using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;

namespace Cms.EbixDataLayer
{
    public class ClsQueryBuilder
    {
        public ClsQueryBuilder()
        { }
       
    }
    public class DefaultValues
    {

        public static int GetIntFromString(string strInt)
        {
            if (strInt.Trim() == "")
            {
                return -1;
            }

            return int.Parse(strInt);
        }

        public static Decimal GetDecimalFromString(string strDecimal)
        {
            if (strDecimal.Trim() == "")
            {
                return -1;
            }

            return Decimal.Parse(strDecimal);
        }

        public static Double GetDoubleFromString(string strDouble)
        {
            if (strDouble.Trim() == "")
            {
                return -1;
            }

            return Double.Parse(strDouble);
        }

        public static DateTime GetDateFromString(string strDate)
        {
            if (strDate.Trim() == "")
            {
                return DateTime.MinValue;
            }

            return DateTime.Parse(strDate);

        }

        public static object GetDateNull(DateTime dt)
        {
            if (dt == DateTime.MinValue)
            {
                return System.DBNull.Value;
            }
            else
            {
                return dt;
            }

        }

        public static int GetInt(object o)
        {
            return o == DBNull.Value ? 0 : Convert.ToInt32(o);
        }

        public static string GetString(object o)
        {
            return o == DBNull.Value ? "" : Convert.ToString(o);
        }

        public static DateTime GetDateTime(object o)
        {
            return o == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(o);
        }

        public static object GetDoubleNull(double doubleValue)
        {
            if (doubleValue == 0 || doubleValue == -1)
            {
                return System.DBNull.Value;
            }
            else
            {
                return doubleValue;
            }

        }

        public static object GetIntNull(int intValue)
        {
            if (intValue == 0 || intValue == -1)
            {
                return System.DBNull.Value;
            }
            else
            {
                return intValue;
            }

        }

        public static object GetDoubleNullFromNegative(double doubleValue)
        {
            if (doubleValue == -1.0 || doubleValue== Double.MinValue)
            {
                return System.DBNull.Value;
            }
            else
            {
                return doubleValue;
            }

        }

        public static object GetDecimalNullFromNegative(decimal decimalValue)
        {
            if (decimalValue == -1 || decimalValue==decimal.MinValue)
            {
                return System.DBNull.Value;
            }
            else
            {
                return decimalValue;
            }

        }
        public static object GetIntNullFromNegative(int intValue)
        {
            if (intValue == -1 || intValue==Int32.MinValue)
            {
                return System.DBNull.Value;
            }
            else
            {
                return intValue;
            }

        }

        public static object GetStringNull(string strValue)
        {
            if (strValue == "")
            {
                return System.DBNull.Value;
            }
            else
            {
                return strValue;
            }

        }

    }
}
