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
    public class ClsMonetaryDetails :Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
    {
        public ClsMonetaryDetails()
        {
        }

        public int AddMonetaryInformation(ClsMonetaryInfo objMonetaryInfo)
        {
            int returnValue = 0;

            if (objMonetaryInfo.RequiredTransactionLog)
            {
                objMonetaryInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddMonetaryDetails.aspx.resx");
                returnValue = objMonetaryInfo.AddMonetaryInformation();

            }
            return returnValue;
        }

        public int UpdateMonetaryInformation(ClsMonetaryInfo objMonetaryInfo)
        {
            int returnValue = 0;

            if (objMonetaryInfo.RequiredTransactionLog)
            {
                objMonetaryInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddMonetaryDetails.aspx.resx");

                returnValue = objMonetaryInfo.UpdateMonetaryInformation();

            }
            return returnValue;
        }

        public DataTable FetchData(ref ClsMonetaryInfo objMonetaryInfo)
        {

            DataSet ds = null;

            try
            {
                ds = objMonetaryInfo.FetchData();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objMonetaryInfo);
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
