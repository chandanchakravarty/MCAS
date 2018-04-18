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

namespace Cms.BusinessLayer.BlApplication
{
    public class ClsInterestRates : Cms.BusinessLayer.BlCommon.ClsCommon
    {
        public ClsInterestRates()
        {
        }        

        public ClsInterestRatesInfo FetchData(Int32 INTEREST_RATE_ID)  
        {

            DataSet ds = null;
            ClsInterestRatesInfo objInterestRatesInfo = new ClsInterestRatesInfo();

            try
            {
                ds = objInterestRatesInfo.FetchData(INTEREST_RATE_ID);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objInterestRatesInfo);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return objInterestRatesInfo;

        }

        public int AddInterestRateInformation(ClsInterestRatesInfo objInterestRatesInfo) 
        {
            int returnValue = 0;

            if (objInterestRatesInfo.RequiredTransactionLog)
            {
                objInterestRatesInfo.TransactLabel =

                ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\InterestRateDetail.aspx.resx");
                returnValue = objInterestRatesInfo.AddInterestRateInformation();

            }
            return returnValue;
        }

        public int UpdateInterestRateInformation(ClsInterestRatesInfo objInterestRatesInfo)  
        {
            int returnValue = 0;

            if (objInterestRatesInfo.RequiredTransactionLog)
            {
                objInterestRatesInfo.TransactLabel =

                ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\InterestRateDetail.aspx.resx");

                returnValue = objInterestRatesInfo.UpdateInterestRateInformation();

            }
            return returnValue;
        }

        public int DeleteInterestRateInformation(ClsInterestRatesInfo objInterestRatesInfo)  
        {
            int returnValue = 0;

            if (objInterestRatesInfo.RequiredTransactionLog)
            {
                objInterestRatesInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\InterestRateDetail.aspx.resx");

                returnValue = objInterestRatesInfo.DeleteInterestRateInformation();
            }
            return returnValue;
        }        

        public int Updatefees(ClsInterestRatesInfo objInterestRatesInfo)
        {
            int returnValue = 0;

            if (objInterestRatesInfo.RequiredTransactionLog)
            {
                objInterestRatesInfo.TransactLabel =

                ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddFeeLimit.aspx.resx");

                returnValue = objInterestRatesInfo.Updatefees();

            }
            return returnValue;
        }

        public string GetOldXML()
        {
            string strStoredProc = "Proc_Get_MNT_SYSTEM_PARAMS";
            DataSet dsSystemParam = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                dsSystemParam = objDataWrapper.ExecuteDataSet(strStoredProc);
                if (dsSystemParam.Tables[0].Rows.Count != 0)
                {
                    return dsSystemParam.GetXml();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static DataTable FetchIOFDetails(int LOB_ID)
        {
            string strProcedure = "Proc_GetIOFDetails";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@LOB_ID", LOB_ID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public int UpdateIOFDetails(ClsInterestRatesInfo objInterestRatesInfo)
        {
            int returnValue = 0;

            if (objInterestRatesInfo.RequiredTransactionLog)
            {
                objInterestRatesInfo.TransactLabel =

                ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddIOFDetails.aspx.resx");

                returnValue = objInterestRatesInfo.UpdateIOFDetails();

            }
            return returnValue;
        }        
       
    }
}
