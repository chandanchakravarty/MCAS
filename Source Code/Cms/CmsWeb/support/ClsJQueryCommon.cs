using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
namespace Cms.CmsWeb.support
{
    public class ClsjQueryCommon
    {
        private static List<Dictionary<string, object>> RowsToDictionary(DataTable table)
        {
            List<Dictionary<string, object>> objs = new List<Dictionary<string, object>>();
            foreach (DataRow dr in table.Rows)
            {
                Dictionary<string, object> drow = new Dictionary<string, object>();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    drow.Add(table.Columns[i].ColumnName, dr[i]);
                }
                objs.Add(drow);
            }

            return objs;
        }

        public static Dictionary<string, object> ToJson(DataTable table)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add(table.TableName, RowsToDictionary(table));
            return d;
        }

        public static Dictionary<string, object> ToJson(DataSet data)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (DataTable table in data.Tables)
            {
                d.Add(table.TableName, RowsToDictionary(table));
            }
            return d;
        }

        public static string GetJSONString(DataTable Dt)
        {

            string[] StrDc = new string[Dt.Columns.Count];
            string HeadStr = string.Empty;

            for (int i = 0; i < Dt.Columns.Count; i++)
            {

                StrDc[i] = Dt.Columns[i].Caption;

                HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

            StringBuilder Sb = new StringBuilder();
            Sb.Append("{\"" + Dt.TableName + "\" : [");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {

                string TempStr = HeadStr;
                Sb.Append("{");

                for (int j = 0; j < Dt.Columns.Count; j++)
                {

                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString());
                }

                Sb.Append(TempStr + "},");
            }

            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
            Sb.Append("]};");

            return Sb.ToString();
        }
    }
}
