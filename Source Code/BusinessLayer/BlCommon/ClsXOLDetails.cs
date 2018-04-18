using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Maintenance;
using System.Data;
using Cms.Model.Support;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Maintenance.Security;

namespace Cms.BusinessLayer.BlCommon
{
    public class ClsXOLDetails :Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
    {
        public ClsXOLDetails()
        {
        }

        public int Add(ClsXOLInfo objXOLInfo)
        {
            int returnValue = 0;

            if (objXOLInfo.RequiredTransactionLog)
            {
                objXOLInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddXOLInformation.aspx.resx");
                returnValue = objXOLInfo.Add();

            }
            return returnValue;
        }

        public int Update(ClsXOLInfo objXOLInfo)
        {
            int returnValue = 0;

            if (objXOLInfo.RequiredTransactionLog)
            {
                objXOLInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddXOLInformation.aspx.resx");

                returnValue = objXOLInfo.Update();

            }
            return returnValue;
        }

        public int ActivateDeactivate(ClsXOLInfo objXOLInfo)
        {
            int returnValue = 0;

            if (objXOLInfo.RequiredTransactionLog)
            {
                objXOLInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddXOLInformation.aspx.resx");

                returnValue = objXOLInfo.ActivateDeactivate();

            }
            return returnValue;
        }

        public DataTable FetchData(ref ClsXOLInfo objXOLInfo)
        {

            DataSet ds = null;

            try
            {
                ds = objXOLInfo.FetchData();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objXOLInfo);
                    return ds.Tables[0];
                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }



        }
    }



}
