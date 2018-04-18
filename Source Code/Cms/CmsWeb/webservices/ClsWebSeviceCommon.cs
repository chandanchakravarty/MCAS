using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Xml;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BLClaims;
using Cms.DataLayer;
using System.Data.SqlClient;

namespace Cms.CmsWeb.webservices
{
    /// <summary>
    /// Summary description for ClsWebServiceCommon.
    /// </summary>
    public class ClsWebServiceCommon
    {
        private const string customizedVan_Truck = "11335";//Added for iTrack Issue 6227 on 11 Aug 09
        public ClsWebServiceCommon()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string FetchZipForState(int stateID, string ZipID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@STATE_ID", stateID);
                objDataWrapper.AddParameter("@ZIP_ID", ZipID);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchZipForState");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp.Tables[0].Rows[0][0].ToString() != "")
                {
                    return dsTemp.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }
        }


        public int ClaimsCheckOperation(string CheckIDs, string Operation)
        {
            return ClsClaims.ClaimsCheckOperation(CheckIDs.Replace("%20", "").Replace("%2C", ","), Operation);
        }

        #region Check for date of loss entered against Policy Effective and Expiration Date
        /// <summary>
        /// Returns only the locations associated with an application
        /// </summary>
        /// <param name="intCustomerID"></param>
        /// <param name="intAppID"></param>
        /// <param name="intAppVersionID"></param>
        /// <returns></returns>
        public DataSet CheckLossDateAgainstPolicy(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, DateTime LOSS_DATE)
        {
            try
            {
                DataWrapper objWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objWrapper.AddParameter("@LOSS_DATE", LOSS_DATE);
                SqlParameter sqlParamRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL", SqlDbType.Int, ParameterDirection.ReturnValue);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_ValidateLossDate");
                objWrapper.ClearParameteres();
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }

        }
        #endregion

        public string GetViolations(int CUSTOMER_ID, int APP_ID, int APP_VERSION_ID, int VIOLATION_ID, string CALLED_FROM)
        {

            try
            {
                DataSet dsTemp = new DataSet();
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", APP_VERSION_ID);
                objDataWrapper.AddParameter("@VIOLATION_ID", VIOLATION_ID);
                objDataWrapper.AddParameter("@CALLED_FROM", CALLED_FROM);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetMNT_VIOLATIONS_NEW");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                {
                    result = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
                }
                else
                {
                    result = "";
                }
                return result;
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }

        }

        public string GetCustomerPolicies(int CUSTOMER_ID, int AGENCY_ID)
        {

            try
            {
                DataSet dsTemp = new DataSet();
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                if (AGENCY_ID != 0)
                {
                    objDataWrapper.AddParameter("@AGENCY_ID", AGENCY_ID);
                }
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_Get_Policy_For_Customer");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                {
                    result = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
                }
                else
                {
                    result = "";
                }
                return result;
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }
        }

        public string GetCountyForZip(string zip)
        {

            try
            {
                DataSet dsTemp = new DataSet();
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@ZIP_ID", zip);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCountyForZip");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    result = (dsTemp.Tables[0].Rows[0][0]).ToString();
                }
                else
                {
                    result = "";
                }
                return result;
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }

        }

        public string DeleteCoverage(string CUST_ID, string APP_ID, string APP_VERSION_ID, string COVG_ID)
        {
            return (new ClsSchItemsCovg().DeletePolicyInlandMarineCoveragesSingle(APP_ID, APP_VERSION_ID, CUST_ID, COVG_ID).ToString());
        }

        public string FetchTerritoryForZipStateLob(string zipId, int lobId, int stateId, int intCustomerId, int intAppId, int intAppVersionId, string calledFrom, int intvehicleuse)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@ZIP_ID", zipId);
                objDataWrapper.AddParameter("@LOB_ID", lobId);
                objDataWrapper.AddParameter("@STATE_ID", stateId);
                objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerId);
                objDataWrapper.AddParameter("@APP_ID", intAppId);
                objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);
                objDataWrapper.AddParameter("@CALLED_FROM", calledFrom);
                objDataWrapper.AddParameter("@VEHICLE_USE", intvehicleuse);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchTerritoryForZipStateLob");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    result = (dsTemp.Tables[0].Rows[0][0]).ToString().Trim();
                }
                else
                {
                    result = "";
                }
                return result;
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }
        }

        public string GetDetailsFromVIN(string VIN)
        {
            return ClsVinMaster.GetDetailsFromVIN(VIN).GetXml();
        }

        public string CancelProcReturnPremium(string CustomerId, string PolicyId, string PolicyVersionId, string ChangeEffDate)
        {
            return this.CancelProcReturnPremiumAmount(CustomerId, PolicyId, PolicyVersionId, ChangeEffDate, "0", "0");
        }

        public string CancelProcReturnPremiumAmount(string CustomerId, string PolicyId, string PolicyVersionId, string ChangeEffDate, string CancellationType, string CancellatoinOption)
        {
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objDataWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE", Convert.ToDateTime(ChangeEffDate));
                objDataWrapper.AddParameter("@CANCELLTION_TYPE", CancellationType);
                objDataWrapper.AddParameter("@CANCELLATION_OPTION", CancellatoinOption);

                SqlParameter objSqlCancelPrem = (SqlParameter)objDataWrapper.AddParameter("@CANCELLATION_PREMIUM", SqlDbType.Int, ParameterDirection.Output);
                objDataWrapper.ExecuteNonQuery("Proc_CalculateReturnPremium");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (objSqlCancelPrem != null && objSqlCancelPrem.Value.ToString() != "")
                    return objSqlCancelPrem.Value.ToString();
                else
                    return "";
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return "";
            }
            finally
            {
            }

        }

        public string CompareData(string compareXML)
        {
            string strXML;
            compareXML = HttpContext.Current.Server.UrlDecode(compareXML);
            try
            {
                strXML = ClsCommon.CompareData(compareXML);
            }
            catch (Exception ex)
            {
                strXML = "";
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return strXML;
        }

        public string FillState(string CountyID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string strSql = "Proc_GetStateListForCountry";
               // string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

                objDataWrapper.AddParameter("@COUNTRY_ID", CountyID);
                DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (objDataSet != null && objDataSet.Tables.Count > 0)
                {
                    return objDataSet.GetXml();
                }
                else
                    return "";
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }

        }

        //Added by aditya, for itrack - 1284
        public DataSet FillAgency( int Broker,string date ,int LangId)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string strSql = "Proc_GetAgencyInfo";               
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);

                objDataWrapper.AddParameter("@LANG_ID", LangId);
                dsTemp = objDataWrapper.ExecuteDataSet(strSql);
                DataView dv = new DataView(dsTemp.Tables[0], "(TERMINATION_DATE >= '" + date + "') and AGENCY_TYPE =" + Broker, "AGENCY_NAME_ACTIVE_STATUS ASC", DataViewRowState.CurrentRows);
                DataTable dt = dv.ToTable().Select("", "AGENCY_NAME_ACTIVE_STATUS").CopyToDataTable<DataRow>(); ;
                dsTemp.Tables.Remove(dsTemp.Tables[0]);
                dsTemp.Tables.Add(dt);
                objDataWrapper.Dispose();
                return dsTemp;
            }
            catch 
            { return null; }
            finally
            { }

        }

        public string FillSubModule(string Module)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dtTemp = new DataTable();
                string strSql = "SP_GET_MENU_LIST";              
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                dtTemp = objDataWrapper.ExecuteDataSet(strSql).Tables[2];
                if (Module != "All")
                {
                    dtTemp = dtTemp.Select("(PARENT_ID = '" + Module + "' ) ", "SUB_MODULE_ID,SUB_MODULE_NAME").CopyToDataTable<DataRow>();
                }
                ds.Tables.Add(dtTemp.Copy());                 
                objDataWrapper.Dispose();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.GetXml();
                }
                else
                    return "";
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return "";
            }
            finally
            { }

        }

        public DataSet FillActiveState(int CountyID)
        {
            try
            {

                string strSql = "ProcGetActiveStateId";

                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);


                objDataWrapper.AddParameter("@COUNTRY_ID", CountyID, SqlDbType.Int);
                DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return objDataSet;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }

        }
        /// <summary>
        /// Get the Make Model Using fipe code 
        /// </summary>
        /// <param name="FipeCode"></param>
        /// <returns></returns>
        public string GetMakeModelUsingFipeCode(String FipeCode)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string strSql = "Proc_GetMakeModelData";
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

                objDataWrapper.AddParameter("@FIPE_CODE", FipeCode);
               
                SqlParameter objSqlMAKE_MODELPrem = (SqlParameter)objDataWrapper.AddParameter("@MAKEMODEL", null, SqlDbType.NVarChar, ParameterDirection.Output, 100);
                SqlParameter objSqlLOOUP_UNIQUE_ID = (SqlParameter)objDataWrapper.AddParameter("@LOOUP_UNIQUE_ID", null, SqlDbType.NVarChar, ParameterDirection.Output, 100);
                SqlParameter objSqlCAPACITY = (SqlParameter)objDataWrapper.AddParameter("@CAPACITY", null, SqlDbType.NVarChar, ParameterDirection.Output, 100);

                objDataWrapper.ExecuteNonQuery(strSql);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                String MakeModel = String.Empty;
                if (objSqlMAKE_MODELPrem != null && objSqlMAKE_MODELPrem.Value.ToString() != "")
                    MakeModel = objSqlMAKE_MODELPrem.Value.ToString();
                
                String UniqueCode = String.Empty;
                if (objSqlLOOUP_UNIQUE_ID != null && objSqlLOOUP_UNIQUE_ID.Value.ToString() != "")
                    UniqueCode = objSqlLOOUP_UNIQUE_ID.Value.ToString();

                String capacity = String.Empty;
                if (objSqlCAPACITY != null && objSqlCAPACITY.Value.ToString() != "")
                    capacity = objSqlCAPACITY.Value.ToString();

                if (MakeModel != "" )
                    result = MakeModel + "~" + UniqueCode + "~" + capacity;
                else
                    result = "";
              
                return result;
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }

        }//public string GetMakeModelUsingFipeCode(String FipeCode)

        /// <summary>
        /// Get Max id of CLIENT_ORDER and VEHICLE_NUMBER
        /// </summary>
        /// <param name="FipeCode"></param>
        /// <returns></returns>
        public String GetTheMaxIdOFClientOrderAndVehicleNumber(Int32 CUSTOMER_ID, Int32 POLICY_ID, Int32 POLICY_VERSION_ID, ref Int64 CLIENT_ORDER, ref Int64 VEHICLE_NUMBER, Int32 Flag)
        {
            try
            {
                String returnValue = String.Empty;

                DataSet dsTemp = new DataSet();
                string strSql = "PROC_GET_MAX_CLIENT_ORDER_AND_VEHICLENUMBER";

                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@CLIENT_ORDER", CLIENT_ORDER);
                objDataWrapper.AddParameter("@VEHICLE_NUMBER", VEHICLE_NUMBER);
                objDataWrapper.AddParameter("@FLAG", Flag);


                SqlParameter returnSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN", SqlDbType.Int, ParameterDirection.ReturnValue);

                SqlParameter objSqlCLIENT_ORDERPrem = (SqlParameter)objDataWrapper.AddParameter("@CLIENT_ORDER_OUT", null, SqlDbType.BigInt, ParameterDirection.Output);
                SqlParameter objSqlVEHICLE_NUMBERPrem = (SqlParameter)objDataWrapper.AddParameter("@VEHICLE_NUMBER_OUT", null, SqlDbType.BigInt, ParameterDirection.Output);

                objDataWrapper.ExecuteNonQuery(strSql);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                returnValue = returnSqlParameter.Value.ToString();

                if (objSqlCLIENT_ORDERPrem != null && objSqlCLIENT_ORDERPrem.Value.ToString() != "")
                {
                    CLIENT_ORDER = Convert.ToInt64(objSqlCLIENT_ORDERPrem.Value.ToString());

                }

                if (objSqlVEHICLE_NUMBERPrem != null && objSqlVEHICLE_NUMBERPrem.Value.ToString() != "")
                {
                    VEHICLE_NUMBER = Convert.ToInt64(objSqlVEHICLE_NUMBERPrem.Value.ToString());

                }

                return returnValue;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return ""; 
            }
            finally
            { }

        }//public string GetMakeModelUsingFipeCode(String FipeCode)

        //Added by Pradeep Kushwaha on 26-05-2010
        /// <summary>
        /// Get the max id of Vessel number and Voyage number
        /// </summary>
        /// <param name="CUSTOMER_ID">Customer id </param>
        /// <param name="POLICY_ID">Policy id </param>
        /// <param name="POLICY_VERSION_ID">Policy Version id</param>
        /// <param name="VESSEL_NUMBER">Vessel Number </param>
        /// <param name="COMMODITY_NUMBER">Voyage Number</param>
        /// <param name="NUMBER">Voyage Number and Vessel Number (depending upon called for)</param>
        /// <param name="CALLEDFOR">which could be VoyageNo or VesselNo</param>
        /// <param name="flag">Could be in between the number (1,2,3 and 4)</param>
        /// <returns></returns>
        public String GetMaxIDofVesselNoandVoyageNo(Int32 CUSTOMER_ID, Int32 POLICY_ID, Int32 POLICY_VERSION_ID, Int64 VESSELNUMBER, Int64 COMMODITYNUMBER, ref Int64 NUMBER, String CALLEDFOR, Int32 flag)
        {
            try
            {
                String returnValue = String.Empty;

                DataSet dsTemp = new DataSet();
                string strSql = "Proc_Get_Max_VoyageNo_And_VesselNo";

                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@COMMODITY_NUMBER", COMMODITYNUMBER);
                objDataWrapper.AddParameter("@VESSEL_NUMBER", VESSELNUMBER);
                objDataWrapper.AddParameter("@FLAG", flag);
                objDataWrapper.AddParameter("@CALLEDFOR", CALLEDFOR);


                SqlParameter returnSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN", SqlDbType.Int, ParameterDirection.ReturnValue);

                SqlParameter objSqlNUMBERPrem = (SqlParameter)objDataWrapper.AddParameter("@NUMBER", null, SqlDbType.BigInt, ParameterDirection.Output);


                objDataWrapper.ExecuteNonQuery(strSql);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                returnValue = returnSqlParameter.Value.ToString();

                if (objSqlNUMBERPrem != null && objSqlNUMBERPrem.Value.ToString() != "")
                {
                    NUMBER = Convert.ToInt64(objSqlNUMBERPrem.Value.ToString());

                }

                return returnValue;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return ""; 
            }
            finally
            { }
        }

        /// <summary>
        /// Get the Customer Address Details Based on the countryid  and zip code 
        /// </summary>
        /// <param name="ZIPCODE"></param>
        /// <param name="COUNTRYID"></param>
        /// <returns></returns>
        public String GetAddressDetailsBasedOnZipeCode(String ZIPCODE, Int32 COUNTRYID)
        {
            try
            {
                String returnValue = String.Empty;

                DataSet dsTemp = new DataSet();
                string strSql = "Proc_Get_Address_Details_UsingZipCode";

                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

                objDataWrapper.AddParameter("@ZIPCODE", ZIPCODE);
                objDataWrapper.AddParameter("@COUNTRYID", COUNTRYID);


                SqlParameter returnSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN", SqlDbType.Int, ParameterDirection.ReturnValue);

                dsTemp = objDataWrapper.ExecuteDataSet(strSql);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                {
                    returnValue = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
                }
                else
                {
                    returnValue = "";
                }

                return Cms.BusinessLayer.BlCommon.ClsCommon.DecodeXMLCharacters(returnValue);  // Added by Aditya on 26-08-2011 for itrack # 1450
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return ""; 
            }
            finally
            { }
        }

        /// <summary>
        /// Get the Customer Address Details Based on the countryid  and zip code 
        /// </summary>
        /// <param name="ZIPCODE"></param>
        /// <param name="COUNTRYID"></param>
        /// <returns></returns>
        public String CheckAddressDetails(String ZIPCODE, Int32 COUNTRYID)
        {
            try
            {
                String returnValue = String.Empty;

                DataSet dsTemp = new DataSet();
                string strSql = "Proc_Get_Address_Details_UsingZipCode";

                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

                objDataWrapper.AddParameter("@ZIPCODE", ZIPCODE);
                objDataWrapper.AddParameter("@COUNTRYID", COUNTRYID);


                SqlParameter returnSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN", SqlDbType.Int, ParameterDirection.ReturnValue);

                dsTemp = objDataWrapper.ExecuteDataSet(strSql);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                {
                    returnValue = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
                }
                else
                {
                    returnValue = "";
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return ""; 
            }
            finally
            { }
        }

        //Following Method will fetch the territory based on ZIP code and LOB_ID
        public string FetchTerritoryForZip(string zipId, int lobId)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@ZIP_ID", zipId);
                objDataWrapper.AddParameter("@LOBID", lobId);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchTerritoryForZip");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    result = (dsTemp.Tables[0].Rows[0][0]).ToString();
                }
                else
                {
                    result = "";
                }
                return result;
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }
        }

        /// <summary>
        /// update or insert data into database. Implementation needs to be coded.
        /// </summary>
        /// <param name="SaveDataXML">XML string conating data to be saved</param>
        /// <returns>"true" or "false" depending save succeed or failed.</returns>
        //		[WebMethod]
        public string SaveData(string SaveDataXML)
        {
            //string saveXML		=		Server.UrlDecode(SaveDataXML);
            string saveXML = HttpContext.Current.Server.UrlDecode(SaveDataXML);
            return "false";
        }

        public string FetchZipState(string ZipID, int stateID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@STATE_ID", stateID);
                objDataWrapper.AddParameter("@ZIP_ID", ZipID);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchZipState");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp.Tables[0].Rows[0][0].ToString().Trim() != "")
                {
                    //return true;//result=(dsTemp.Tables[0].Rows[0][0]).ToString();
                    return dsTemp.Tables[0].Rows[0][0].ToString().Trim();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }
        }
        #region SYMBOL LOGIC
        public string GetSymbolForAppPolicy(string VehicleType, int Amount, int Year)
        {
            return GetSymbolForVehicle(VehicleType, Amount, Year);
        }
        public string GetSymbolForVehicle(string VehicleType, int Amount, int Year)
        {
            int Symbol = 0;
            try
            {
                switch (VehicleType)
                {
                    case "11334": //Get Symbol for Private Pesanger
                    case "11337": //Get Symbol for Trailer
                    case "11336": ////Get Symbol for Motor
                    case "11870": //Get Symbol for Campers
                    case "11338": //Get Symbol for Local Haul - Intermittent
                    case "11339": //Get Symbol for Local Haul
                    case "11340": //Get Symbol for Trailer  - Intermittent
                    case "11341": //Get Symbol for Trailer
                    case "11871": //Get Symbol for Long Haul
                    case "11868": //Get Symbol for Classic Car
                    case "11869": //Get Symbol for Antique Car
                        Symbol = GetSymbol(VehicleType, Amount);
                        break;
                    case "11335": ////Get Symbol for CustomizedVan
                        Symbol = GetSymbolForCustomized(VehicleType, Amount, Year);
                        break;
                    default:
                        Symbol = 0;
                        break;
                }
                return Symbol.ToString();
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return "";
            }
            finally { }
        }
        /// <summary>
        /// //Added for iTrack Issue 6227 on 10 Aug 09
        /// </summary>
        /// <param name="VehicleType"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public string GetHighestSymbol(string VehicleType, int Year)
        {
            string Symbol = "0";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml"));
            string amount = "";
            //XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='11337']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 

            if (Year >= 1990)
                Year = 1990;
            else
                Year = 1989;

            XmlNodeList xNodeList = null;
            if (VehicleType == customizedVan_Truck)
                xNodeList = xDoc.SelectNodes("GeneralRules/VehicleType[@ID='" + VehicleType + "']/Year[@ID='" + Year.ToString() + "']");
            else
                xNodeList = xDoc.SelectNodes("GeneralRules/VehicleType[@ID='" + VehicleType + "']");

            if (xNodeList.Count > 0)
            {
                XmlNode node = xNodeList[0];

                if (node.LastChild != null)
                {
                    if (node.LastChild.SelectSingleNode("Symbol") != null)
                        Symbol = node.LastChild.SelectSingleNode("Symbol").InnerText;
                    //Get the Highest Amount
                    amount = node.LastChild.Attributes["Amount2"].Value;

                }
            }
            return Symbol + "^" + amount;
        }

        public int GetSymbolForCustomized(string VehicleType, int Amount, int Year)
        {
            int Symbol = 0;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml"));

            if (Year >= 1990)
                Year = 1990;
            else
                Year = 1989;


            //XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='11337']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
            XmlNodeList xNodeList = xDoc.SelectNodes("GeneralRules/VehicleType[@ID='" + VehicleType + "']/Year[@ID='" + Year.ToString() + "']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]");

            if (xNodeList.Count > 0)
            {
                XmlNode node = xNodeList[0];

                XmlNode symbolNode = node.SelectSingleNode("Symbol");

                string strSymbol = symbolNode.InnerText.Trim();
                Symbol = Convert.ToInt32(strSymbol);

            }

            return Symbol;
        }

        #endregion
        public string FetchAgencyCSRProducer(int AgencyId, int LobID, string sSystemId)//LobID added by Charles on 28-May-2010
        {
            string CSR = "";
            try
            {
                ClsUser objUser = new ClsUser();
                DataTable dtCSRProducers = objUser.GetCSRProducers(AgencyId, LobID, sSystemId);
                CSR = dtCSRProducers.DataSet.GetXml();
                return CSR;
            }
            catch
            { return ""; }
            finally
            { }
        }
        public DataSet GetProtectionClass(string protectionClass, int milesToDwell, string feetToHydrant, string lobCode)
        {
            return (new ClsHome().FetchProtectionClass(protectionClass, milesToDwell, feetToHydrant, lobCode));
        }

        public int GetSymbol(string VehicleType, int Amount)
        {
            int Symbol = 0;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml"));


            //XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='11337']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
            XmlNodeList xNodeList = xDoc.SelectNodes("GeneralRules/VehicleType[@ID='" + VehicleType + "']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]");

            if (xNodeList.Count > 0)
            {
                XmlNode node = xNodeList[0];

                XmlNode symbolNode = node.SelectSingleNode("Symbol");

                string strSymbol = symbolNode.InnerText.Trim();
                Symbol = Convert.ToInt32(strSymbol);

            }

            return Symbol;
        }
        /// <summary>
        /// Itrack 5985 : kasana
        /// Used in Add Claims - To Check 
        /// Any time there is a policy with any Process in Progress or Suspended Process 
        /// </summary>
        /// <param name="rowXML"></param>
        /// <returns></returns>
        public int GetClaimPolicyStatus(string rowXML)
        {

            int retVal = 0;
            if (rowXML != "")
            {
                string custID = "";
                string polID = "";
                string polVerID = "";
                custID = ClsCommon.FetchValueFromXML("CUSTOMER_ID", rowXML);
                polID = ClsCommon.FetchValueFromXML("POLICY_ID", rowXML);
                polVerID = ClsCommon.FetchValueFromXML("POLICY_VERSION_ID", rowXML);

                DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", custID);
                objDataWrapper.AddParameter("@POLICY_ID", polID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", polVerID);
                SqlParameter objParam = (SqlParameter)objDataWrapper.AddParameter("@RetVal", SqlDbType.Int, ParameterDirection.ReturnValue);

                objDataWrapper.ExecuteNonQuery("Proc_GetClaimPolicyStatus");

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (objParam.Value != null)
                    return int.Parse(objParam.Value.ToString());



            }
            return retVal;

        }
        public string CustomerTypeTitles(string CustomerType, int Lang_id)
        {
            try
            {
                //exec  @LookupCode = N'CUSTYPE'  
                DataSet dsTemp = new DataSet();
                string strSql = "Proc_GetLookupValues";//"select LOOKUP_UNIQUE_ID, LOOKUP_VALUE_DESC,LOOKUP_VALUE_CODE, Type from MNT_LOOKUP_VALUES where (LOOKUP_ID=358 and Type= '" + CustomerType + "') or (LOOKUP_ID=358 and Type is null)";//"Proc_GetCustomerLookup";//"Proc_GetCustomerLookup";//
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@LookupCode", "%SAL");
                objDataWrapper.AddParameter("@LANG_ID", Lang_id);
                dsTemp = objDataWrapper.ExecuteDataSet(strSql);

                DataView dv = new DataView(dsTemp.Tables[0], "Type= '" + CustomerType + "' or Type is null", "LOOKUP_VALUE_DESC", DataViewRowState.CurrentRows);
                DataTable dt = dv.ToTable();
                dsTemp.Tables.Remove(dsTemp.Tables[0]);
                dsTemp.Tables.Add(dt);


                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp != null && dsTemp.Tables.Count > 0)
                {
                    result = dsTemp.GetXml();
                    return result;
                }
                else
                    return "";
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }
        }
        public string CustomerTypeTitlesNew(string CustomerType)
        {
            try
            {
                //exec  @LookupCode = N'CUSTYPE'  
                DataSet dsTemp = new DataSet();
                string strSql = "PROC_SELECTACTIVITYTYPE";//"select LOOKUP_UNIQUE_ID, LOOKUP_VALUE_DESC,LOOKUP_VALUE_CODE, Type from MNT_LOOKUP_VALUES where (LOOKUP_ID=358 and Type= '" + CustomerType + "') or (LOOKUP_ID=358 and Type is null)";//"Proc_GetCustomerLookup";//"Proc_GetCustomerLookup";//
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                // objDataWrapper.AddParameter("@LookupCode", "%SAL");
                // objDataWrapper.AddParameter("@LANG_ID", LANG_ID);
                dsTemp = objDataWrapper.ExecuteDataSet(strSql);
                DataView dv = new DataView(dsTemp.Tables[0], "TYPE='" + CustomerType + "' or TYPE is null", "ACTIVITY_DESC", DataViewRowState.CurrentRows);
                DataTable dt = dv.ToTable();
                dsTemp.Tables.Remove(dsTemp.Tables[0]);
                dsTemp.Tables.Add(dt);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp != null && dsTemp.Tables.Count > 0)
                {
                    result = dsTemp.GetXml();
                    return result;
                }
                else
                    return "";
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }
        }

        public string CommissionTypeAgency(string CommissionType)
        {
            try
            {
                 
                DataSet dsTemp = new DataSet();
                string strSql = "Proc_GetAgencyInfo";
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                dsTemp = objDataWrapper.ExecuteDataSet(strSql);              
                if (CommissionType == "14701")
                {
                    DataView dv = new DataView(dsTemp.Tables[0], "AGENCY_TYPE_ID='" + CommissionType + "' or AGENCY_TYPE_ID is null", "AGENCY_DISPLAY_NAME", DataViewRowState.CurrentRows);
                    DataTable dt = dv.ToTable().Select("", "DISPLAY_NAME").CopyToDataTable<DataRow>(); ;
                    dsTemp.Tables.Remove(dsTemp.Tables[0]);
                    dsTemp.Tables.Add(dt);
                }
                else
                {
                    DataTable dt2 = dsTemp.Tables[0].Select("", "AGENCY_TYPE_ID DESC").CopyToDataTable<DataRow>();
                    dsTemp.Tables.Remove(dsTemp.Tables[0]);
                    dsTemp.Tables.Add(dt2);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp != null && dsTemp.Tables.Count > 0)
                {
                    result = dsTemp.GetXml();
                    return result;
                }
                else
                    return "";
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
                return ""; 
            }
            finally
            { }
        }

        public string Get_PolicyCancellationReturnPremium(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, DateTime CANCELLATION_DATE) 
        {
            string Premium = "";
            
            DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
            objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
            objDataWrapper.AddParameter("@CANCELLATION_EFFECTIVE_DATE", CANCELLATION_DATE);            
            DataSet ds = objDataWrapper.ExecuteDataSet("PROC_GET_CANCELLATION_RETURN_PREMIUM");
            objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) 
            {
                Premium = ds.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString() ;//== "" ? 0 : Convert.ToDouble(ds.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString());
            }
            return Premium;
        }
        public string Get_PolProrataAmount(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID,DateTime CancellationDate)
        {
            string Premium = "";

            DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
            objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
            objDataWrapper.AddParameter("@CANCELLATION_EFFECTIVE_DATE", CancellationDate);
            DataSet ds = objDataWrapper.ExecuteDataSet("PROC_GetCacellationProAmount");
            objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Premium = ds.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString();// == "" ? 0 : Convert.ToDouble(ds.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString());
            }
            return Premium;
        }
        //Added for itrack-927
        /// <summary>
        /// Get bank data
        /// </summary>
        /// <param name="SearchData"></param>
        /// <param name="CalledFrom"></param>
        /// <returns></returns>
        public System.Collections.Generic.List<String> GetBankData(String SearchData, String SerachFor, String CalledFor, String Calledfrom)
        {
            
            System.Collections.Generic.List<String> sbSearchedData = new System.Collections.Generic.List<String>();
            string strSql = "Proc_GetBankDetailsData";

            DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            objDataWrapper.AddParameter("@SearchData", SearchData);
            objDataWrapper.AddParameter("@CalledFor", CalledFor);
            objDataWrapper.AddParameter("@SearchFor", SerachFor);
            objDataWrapper.AddParameter("@Calledfrom", Calledfrom);

            objDataWrapper.ExecuteNonQuery(strSql);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            if (objDataSet != null && objDataSet.Tables[0].Rows.Count > 0)
            {
                for (int count = 0; count < objDataSet.Tables[0].Rows.Count; count++)
                {
                    sbSearchedData.Add(objDataSet.Tables[0].Rows[count]["SearchData"].ToString());

                }
            }//if (objDataSet != null && objDataSet.Tables[0].Rows.Count > 0)
            return sbSearchedData;
        }//public System.Collections.Generic.List<String> GetBankData(String SearchData, String CalledFor)
        //Added till here 
    }
}
