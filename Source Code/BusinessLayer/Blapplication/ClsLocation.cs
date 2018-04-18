/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	5/11/2005 5:02:21 PM
<End Date				: -	
<Description				: - 	Business layer class for Location.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified

<Modified Date           : Mohit Gupta
<Modified By             : 27-10-2005
<Purpose                 : Added overloaded method GetLocationsForApplication with four parameters.  
*******************************************************************************************/
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BlApplication
{
    /// <summary>
    /// Business layer class for Location.
    /// </summary>
    public class ClsLocation : Cms.BusinessLayer.BlApplication.clsapplication
    {
        private const string GET_LOCATION_INFO_PROC = "Proc_GetLocationInfo";
        private const string GET_POLICY_LOCATION_INFO_PROC = "Proc_GetPolicyLocationInfo";
        private string strStoredProc = "";
        #region Private Instance Variables
        private bool boolTransactionLog;
        // private int _LOCATION_ID;
        private const string ACTIVATE_DEACTIVATE_PROC = "Proc_ActivateDeactivateLocation";
        #endregion

        #region Public Properties
        public bool TransactionLog
        {
            set
            {
                boolTransactionLog = value;
            }
            get
            {
                return boolTransactionLog;
            }
        }
        #endregion

        #region Utility Functions
        public static DataTable GetPolStateNameOnStateID(int custId, int polId, int polVerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@custId", custId);
            sqlParams[1] = new SqlParameter("@polId", polId);
            sqlParams[2] = new SqlParameter("@polVerId", polVerId);
            DataSet ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_GetPolStateNameforID", sqlParams);
            return ds.Tables[0];
        }

        #endregion

        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsLocation()
        {
            boolTransactionLog = base.TransactionLogRequired;
            base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
        }
        #endregion

        public int ActivateDeactivate(int customerID, int appID, int appVersionID,
                                        int locationID, string action)
        {
            string strStoredProc = "Proc_ActivateDeactivateLocation";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            objWrapper.AddParameter("@LOCATION_ID", locationID);
            objWrapper.AddParameter("@IS_ACTIVE", action);
            SqlParameter paramRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL", SqlDbType.Int, ParameterDirection.ReturnValue);

            objWrapper.ExecuteNonQuery(strStoredProc);

            int retVal = Convert.ToInt32(paramRetVal.Value);
            return retVal;
        }

        //Added by Raghav on 17/07/2008
        public int GeneralLocation(int customerID, int appID, int appVersionID, string calledFrom)
        {
            string strsql = "Proc_LocationCount";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objDataWrapper.AddParameter("@APP_ID", appID);
            objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            objDataWrapper.AddParameter("@CALLED_FROM", calledFrom);
            SqlParameter paramRetval = (SqlParameter)objDataWrapper.AddParameter("@Ret_val", SqlDbType.Int, ParameterDirection.ReturnValue);
            objDataWrapper.ExecuteNonQuery(strsql);
            int retval = Convert.ToInt32(paramRetval.Value);
            return retval;
        }

        public int ActivateDeactivatePolicy(int customerID, int polID, int polVersionID,
            int locationID, string action,int modifiedby)
        {
            string strStoredProc = "Proc_ActivateDeactivatePolicyLocation";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POL_ID", polID);
            objWrapper.AddParameter("@POL_VERSION_ID", polVersionID);
            objWrapper.AddParameter("@LOCATION_ID", locationID);
            objWrapper.AddParameter("@IS_ACTIVE", action);
            int retVal;
           
            if (TransactionLogRequired)
            {
                Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                if (action == "Y")
                {
                    objTransactionInfo.TRANS_TYPE_ID = 186;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1464", ""); //"Location has been Activated";
                }
                else
                {
                    objTransactionInfo.TRANS_TYPE_ID = 187;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1465", "");  //"Location has been Deactivated";
                }
                
                objTransactionInfo.RECORDED_BY = modifiedby;
                objTransactionInfo.POLICY_ID = polID;
                objTransactionInfo.POLICY_VER_TRACKING_ID = polVersionID;
                objTransactionInfo.CLIENT_ID = customerID;
                
                retVal = objWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

            }
            else
            {
                SqlParameter paramRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL", SqlDbType.Int, ParameterDirection.ReturnValue);

                objWrapper.ExecuteNonQuery(strStoredProc);

                retVal = Convert.ToInt32(paramRetVal.Value);
            }

            if (retVal > 0)
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            else
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
            return retVal;

        }


        #region Add(Insert) functions



        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objLocationInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int SaveLocation(ClsLocationInfo objLocationInfo, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_SaveLocation_ACORD";
            DateTime RecordDate = DateTime.Now;
            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);


            strStoredProc = "Proc_SaveLocation_ACORD";


            //objLocationInfo.LOCATION_TYPE = ClsCommon.GetLookupUniqueId("LOCTYP", objLocationInfo.IS_PRIMARY);

            objDataWrapper.AddParameter("@CUSTOMER_ID", objLocationInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@APP_ID", objLocationInfo.APP_ID);
            objDataWrapper.AddParameter("@APP_VERSION_ID", objLocationInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@LOC_NUM", objLocationInfo.LOC_NUM);
            objDataWrapper.AddParameter("@IS_PRIMARY", objLocationInfo.IS_PRIMARY);
            objDataWrapper.AddParameter("@LOC_ADD1", objLocationInfo.LOC_ADD1);
            objDataWrapper.AddParameter("@LOC_ADD2", objLocationInfo.LOC_ADD2);
            objDataWrapper.AddParameter("@LOC_CITY", objLocationInfo.LOC_CITY);
            objDataWrapper.AddParameter("@LOC_COUNTY", objLocationInfo.LOC_COUNTY);
            objDataWrapper.AddParameter("@LOC_STATE", objLocationInfo.LOC_STATE);
            objDataWrapper.AddParameter("@LOC_ZIP", objLocationInfo.LOC_ZIP);
            objDataWrapper.AddParameter("@LOCATION_TYPE", objLocationInfo.LOCATION_TYPE);

            if (objLocationInfo.LOC_COUNTRY != null)
            {
                objDataWrapper.AddParameter("@LOC_COUNTRY", objLocationInfo.LOC_COUNTRY);
            }
            objDataWrapper.AddParameter("@PHONE_NUMBER", null);
            objDataWrapper.AddParameter("@FAX_NUMBER", null);
            objDataWrapper.AddParameter("@DEDUCTIBLE", null);
            objDataWrapper.AddParameter("@NAMED_PERILL", null);
            objDataWrapper.AddParameter("@CREATED_BY", objLocationInfo.CREATED_BY);
            objDataWrapper.AddParameter("@CREATED_DATETIME", RecordDate);
            objDataWrapper.AddParameter("@DESCRIPTION", objLocationInfo.DESCRIPTION);
            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@LOCATION_ID", objLocationInfo.LOCATION_ID, SqlDbType.Int, ParameterDirection.Output);

            /*
            if(TransactionLogRequired)
            {
                objLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/AddLocation.aspx.resx");
                SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                string strTranXML = objBuilder.GetTransactionLogXML(objLocationInfo);
                Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
                objTransactionInfo.TRANS_TYPE_ID	=	1;
                objTransactionInfo.APP_ID = objLocationInfo.APP_ID;
                objTransactionInfo.APP_VERSION_ID = objLocationInfo.APP_VERSION_ID;
                objTransactionInfo.CLIENT_ID = objLocationInfo.CUSTOMER_ID;
                objTransactionInfo.RECORDED_BY		=	objLocationInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC		=	"New location detail is added";
                objTransactionInfo.CHANGE_XML		=	strTranXML;
                //Executing the query
                returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
            }
            else
            {*/
            objDataWrapper.ExecuteNonQuery(strStoredProc);


            int LOCATION_ID = 0;

            LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());

            objLocationInfo.LOCATION_ID = LOCATION_ID;
            return LOCATION_ID;

        }

        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objLocationInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsLocationInfo objLocationInfo)
        {
            string strStoredProc = "Proc_InsertLocation";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            strStoredProc = "Proc_InsertLocation";

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objLocationInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", objLocationInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", objLocationInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@LOC_NUM", objLocationInfo.LOC_NUM);
                if (objLocationInfo.IS_PRIMARY != null)
                {
                    if (objLocationInfo.IS_PRIMARY.Trim() != "")
                    {
                        objDataWrapper.AddParameter("@IS_PRIMARY", objLocationInfo.IS_PRIMARY);
                    }
                    else
                    {
                        objDataWrapper.AddParameter("@IS_PRIMARY", DBNull.Value);
                    }
                }
                else
                {
                    objDataWrapper.AddParameter("@IS_PRIMARY", DBNull.Value);
                }
                objDataWrapper.AddParameter("@LOC_ADD1", objLocationInfo.LOC_ADD1);
                objDataWrapper.AddParameter("@LOC_ADD2", objLocationInfo.LOC_ADD2);
                objDataWrapper.AddParameter("@LOC_CITY", objLocationInfo.LOC_CITY);
                objDataWrapper.AddParameter("@LOC_COUNTY", objLocationInfo.LOC_COUNTY);
                objDataWrapper.AddParameter("@LOC_STATE", objLocationInfo.LOC_STATE);
                objDataWrapper.AddParameter("@LOC_ZIP", objLocationInfo.LOC_ZIP);
                if (objLocationInfo.LOC_COUNTRY != null)
                {
                    objDataWrapper.AddParameter("@LOC_COUNTRY", objLocationInfo.LOC_COUNTRY);
                }
                objDataWrapper.AddParameter("@PHONE_NUMBER", null);
                objDataWrapper.AddParameter("@FAX_NUMBER", null);

                objDataWrapper.AddParameter("@RENTED_WEEKLY", objLocationInfo.RENTED_WEEKLY);
                objDataWrapper.AddParameter("@WEEKS_RENTED", objLocationInfo.WEEKS_RENTED);

                objDataWrapper.AddParameter("@CREATED_BY", objLocationInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objLocationInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@DESCRIPTION", objLocationInfo.DESCRIPTION);

                objDataWrapper.AddParameter("@LOCATION_TYPE", objLocationInfo.LOCATION_TYPE);
                if (objLocationInfo.LOSSREPORT_ORDER != 0)
                    objDataWrapper.AddParameter("@LOSSREPORT_ORDER", objLocationInfo.LOSSREPORT_ORDER);
                else
                    objDataWrapper.AddParameter("@LOSSREPORT_ORDER", System.DBNull.Value);

                if (objLocationInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@LOSSREPORT_DATETIME", objLocationInfo.LOSSREPORT_DATETIME);
                else
                    objDataWrapper.AddParameter("@LOSSREPORT_DATETIME", System.DBNull.Value);

                objDataWrapper.AddParameter("@REPORT_STATUS", objLocationInfo.REPORT_STATUS);


                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@LOCATION_ID", objLocationInfo.LOCATION_ID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/AddLocation.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objLocationInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.APP_ID = objLocationInfo.APP_ID;
                    objTransactionInfo.APP_VERSION_ID = objLocationInfo.APP_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objLocationInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY = objLocationInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1447", "");// "New location detail is added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                int LOCATION_ID = 0;
                if (returnResult > 0)
                {
                    LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                objLocationInfo.LOCATION_ID = LOCATION_ID;
                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objLocationInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsPolicyLocationInfo objLocationInfo)
        {
            string strStoredProc = "Proc_InsertPolicyLocation";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            int RET = 0;
            strStoredProc = "Proc_InsertPolicyLocation";

            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", objLocationInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POL_ID", objLocationInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POL_VERSION_ID", objLocationInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@LOC_NUM", objLocationInfo.LOC_NUM);
                if (objLocationInfo.IS_PRIMARY != null)
                {
                    if (objLocationInfo.IS_PRIMARY.Trim() != "")
                    {
                        objDataWrapper.AddParameter("@IS_PRIMARY", objLocationInfo.IS_PRIMARY);
                    }
                    else
                    {
                        objDataWrapper.AddParameter("@IS_PRIMARY", DBNull.Value);
                    }
                }
                else
                {
                    objDataWrapper.AddParameter("@IS_PRIMARY", DBNull.Value);
                }
                objDataWrapper.AddParameter("@LOC_ADD1", objLocationInfo.LOC_ADD1);
                objDataWrapper.AddParameter("@LOC_ADD2", objLocationInfo.LOC_ADD2);
                objDataWrapper.AddParameter("@LOC_CITY", objLocationInfo.LOC_CITY);
                objDataWrapper.AddParameter("@LOC_COUNTY", objLocationInfo.LOC_COUNTY);
                objDataWrapper.AddParameter("@LOC_STATE", objLocationInfo.LOC_STATE);
                objDataWrapper.AddParameter("@LOC_ZIP", objLocationInfo.LOC_ZIP);
                if (objLocationInfo.LOC_COUNTRY != null)
                {
                    objDataWrapper.AddParameter("@LOC_COUNTRY", objLocationInfo.LOC_COUNTRY);
                }
                objDataWrapper.AddParameter("@PHONE_NUMBER", objLocationInfo.PHONE_NUMBER);
                objDataWrapper.AddParameter("@FAX_NUMBER", objLocationInfo.FAX_NUMBER);
                //objDataWrapper.AddParameter("@DEDUCTIBLE",objLocationInfo.DEDUCTIBLE);
                //objDataWrapper.AddParameter("@NAMED_PERILL",objLocationInfo.NAMED_PERILL);
                objDataWrapper.AddParameter("@CREATED_BY", objLocationInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objLocationInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@DESCRIPTION", objLocationInfo.DESCRIPTION);
                objDataWrapper.AddParameter("@LOCATION_TYPE", objLocationInfo.LOCATION_TYPE);

                objDataWrapper.AddParameter("@RENTED_WEEKLY", objLocationInfo.RENTED_WEEKLY);
                objDataWrapper.AddParameter("@WEEKS_RENTED", objLocationInfo.WEEKS_RENTED);

                if (objLocationInfo.LOSSREPORT_ORDER != 0)
                    objDataWrapper.AddParameter("@LOSSREPORT_ORDER", objLocationInfo.LOSSREPORT_ORDER);
                else
                    objDataWrapper.AddParameter("@LOSSREPORT_ORDER", System.DBNull.Value);

                if (objLocationInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@LOSSREPORT_DATETIME", objLocationInfo.LOSSREPORT_DATETIME);
                else
                    objDataWrapper.AddParameter("@LOSSREPORT_DATETIME", System.DBNull.Value);

                objDataWrapper.AddParameter("@REPORT_STATUS", objLocationInfo.REPORT_STATUS);

                //added By Chetna 
                objDataWrapper.AddParameter("@CAL_NUM", objLocationInfo.CAL_NUM);
                objDataWrapper.AddParameter("@NAME", objLocationInfo.NAME);
                objDataWrapper.AddParameter("@NUMBER", objLocationInfo.NUMBER);
                objDataWrapper.AddParameter("@DISTRICT", objLocationInfo.DISTRICT);
                objDataWrapper.AddParameter("@OCCUPIED", objLocationInfo.OCCUPIED);
                objDataWrapper.AddParameter("@EXT", objLocationInfo.EXT);
                objDataWrapper.AddParameter("@CATEGORY", objLocationInfo.CATEGORY);
                objDataWrapper.AddParameter("@ACTIVITY_TYPE", objLocationInfo.ACTIVITY_TYPE);
                objDataWrapper.AddParameter("@CONSTRUCTION", objLocationInfo.CONSTRUCTION);
                objDataWrapper.AddParameter("@SOURCE_LOCATION_ID", objLocationInfo.SOURCE_LOCATION_ID);
                objDataWrapper.AddParameter("@IS_BILLING", objLocationInfo.IS_BILLING);



                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@LOCATION_ID", objLocationInfo.LOCATION_ID, SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSqlParameter1 = (SqlParameter)objDataWrapper.AddParameter("@RET", SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/aspx/AddPolicyLocations.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objLocationInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.TRANS_TYPE_ID = 194;
                    objTransactionInfo.POLICY_ID = objLocationInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objLocationInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objLocationInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY = objLocationInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1447", "");// "New location detail is added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                int LOCATION_ID = 0;
                if (returnResult > 0)
                {
                    LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());
                    objLocationInfo.LOCATION_ID = LOCATION_ID;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                objLocationInfo.LOCATION_ID = LOCATION_ID;

                RET = int.Parse(objSqlParameter1.Value.ToString());
                return RET;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        #endregion

        #region Update method

        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldLocationInfo">Model object having old information</param>
        /// <param name="objLocationInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int UpdateLocation(ClsLocationInfo objLocationInfo, DataWrapper objDataWrapper)
        {
            //string strTranXML;
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            //DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            strStoredProc = "Proc_UpdateLocation";

            objDataWrapper.AddParameter("@CUSTOMER_ID", objLocationInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@APP_ID", objLocationInfo.APP_ID);
            objDataWrapper.AddParameter("@APP_VERSION_ID", objLocationInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@LOC_NUM", objLocationInfo.LOC_NUM);
            objDataWrapper.AddParameter("@IS_PRIMARY", objLocationInfo.IS_PRIMARY);
            objDataWrapper.AddParameter("@LOC_ADD1", objLocationInfo.LOC_ADD1);
            objDataWrapper.AddParameter("@LOC_ADD2", objLocationInfo.LOC_ADD2);
            objDataWrapper.AddParameter("@LOC_CITY", objLocationInfo.LOC_CITY);
            objDataWrapper.AddParameter("@LOC_COUNTY", objLocationInfo.LOC_COUNTY);
            objDataWrapper.AddParameter("@LOC_STATE", objLocationInfo.LOC_STATE);
            objDataWrapper.AddParameter("@LOC_ZIP", objLocationInfo.LOC_ZIP);
            if (objLocationInfo.LOC_COUNTRY != null)
            {
                objDataWrapper.AddParameter("@LOC_COUNTRY", objLocationInfo.LOC_COUNTRY);
            }
            objDataWrapper.AddParameter("@PHONE_NUMBER", null);
            objDataWrapper.AddParameter("@FAX_NUMBER", null);

            objDataWrapper.AddParameter("@RENTED_WEEKLY", objLocationInfo.RENTED_WEEKLY);
            objDataWrapper.AddParameter("@WEEKS_RENTED", objLocationInfo.WEEKS_RENTED);

            objDataWrapper.AddParameter("@MODIFIED_BY", objLocationInfo.MODIFIED_BY);
            objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objLocationInfo.LAST_UPDATED_DATETIME);
            objDataWrapper.AddParameter("@DESCRIPTION", objLocationInfo.DESCRIPTION);
            objDataWrapper.AddParameter("@LOCATION_ID", objLocationInfo.LOCATION_ID);
            returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

            //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return returnResult;

        }



        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldLocationInfo">Model object having old information</param>
        /// <param name="objLocationInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsLocationInfo objOldLocationInfo, ClsLocationInfo objLocationInfo)
        {
            string strTranXML;
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            strStoredProc = "Proc_UpdateLocation";
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objLocationInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", objLocationInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", objLocationInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@LOC_NUM", objLocationInfo.LOC_NUM);
                if (objLocationInfo.IS_PRIMARY != null)
                {
                    if (objLocationInfo.IS_PRIMARY.Trim() != "")
                    {
                        objDataWrapper.AddParameter("@IS_PRIMARY", objLocationInfo.IS_PRIMARY);
                    }
                    else
                    {
                        objDataWrapper.AddParameter("@IS_PRIMARY", DBNull.Value);
                    }
                }
                else
                {
                    objDataWrapper.AddParameter("@IS_PRIMARY", DBNull.Value);
                }
                objDataWrapper.AddParameter("@LOC_ADD1", objLocationInfo.LOC_ADD1);
                objDataWrapper.AddParameter("@LOC_ADD2", objLocationInfo.LOC_ADD2);
                objDataWrapper.AddParameter("@LOC_CITY", objLocationInfo.LOC_CITY);
                objDataWrapper.AddParameter("@LOC_COUNTY", objLocationInfo.LOC_COUNTY);
                objDataWrapper.AddParameter("@LOC_STATE", objLocationInfo.LOC_STATE);
                objDataWrapper.AddParameter("@LOC_ZIP", objLocationInfo.LOC_ZIP);
                if (objLocationInfo.LOC_COUNTRY != null)
                {
                    objDataWrapper.AddParameter("@LOC_COUNTRY", objLocationInfo.LOC_COUNTRY);
                }
                objDataWrapper.AddParameter("@PHONE_NUMBER", null);
                objDataWrapper.AddParameter("@FAX_NUMBER", null);

                objDataWrapper.AddParameter("@MODIFIED_BY", objLocationInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objLocationInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@DESCRIPTION", objLocationInfo.DESCRIPTION);
                objDataWrapper.AddParameter("@LOCATION_ID", objLocationInfo.LOCATION_ID);
                objDataWrapper.AddParameter("@LOCATION_TYPE", objLocationInfo.LOCATION_TYPE);

                objDataWrapper.AddParameter("@RENTED_WEEKLY", objLocationInfo.RENTED_WEEKLY);
                objDataWrapper.AddParameter("@WEEKS_RENTED", objLocationInfo.WEEKS_RENTED);

                if (objLocationInfo.LOSSREPORT_ORDER != 0)
                    objDataWrapper.AddParameter("@LOSSREPORT_ORDER", objLocationInfo.LOSSREPORT_ORDER);
                else
                    objDataWrapper.AddParameter("@LOSSREPORT_ORDER", System.DBNull.Value);

                if (objLocationInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@LOSSREPORT_DATETIME", objLocationInfo.LOSSREPORT_DATETIME);
                else
                    objDataWrapper.AddParameter("@LOSSREPORT_DATETIME", System.DBNull.Value);

                objDataWrapper.AddParameter("@REPORT_STATUS", objLocationInfo.REPORT_STATUS);

                if (TransactionLogRequired)
                {
                    objLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/AddLocation.aspx.resx");
                    strTranXML = objBuilder.GetTransactionLogXML(objOldLocationInfo, objLocationInfo);
                    if (strTranXML == "<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    else
                    {
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                        objTransactionInfo.TRANS_TYPE_ID = 3;
                        objTransactionInfo.APP_ID = objLocationInfo.APP_ID;
                        objTransactionInfo.APP_VERSION_ID = objLocationInfo.APP_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objLocationInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objLocationInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1466", "");// "Location detail is modified";
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    }

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                objDataWrapper.ClearParameteres();

                //Update coverages/////////////////////////////////////////////////////
                objDataWrapper.ClearParameteres();
                int DwellingID = GetDwellingForLocation(objDataWrapper, objLocationInfo.CUSTOMER_ID,
                                objLocationInfo.APP_ID, objLocationInfo.APP_VERSION_ID, objLocationInfo.LOCATION_ID);

                ClsGeneralInformation obj = new ClsGeneralInformation();
                string strlob = "";
                strlob = obj.Fun_GetLObID(objLocationInfo.CUSTOMER_ID,
                    objLocationInfo.APP_ID, objLocationInfo.APP_VERSION_ID);
                if (strlob == ((int)enumLOB.HOME).ToString())
                {
                    if (DwellingID > 0)
                    {
                        ClsHomeCoverages objCoverages = new ClsHomeCoverages();
                        objCoverages.UpdateCoveragesByRuleApp(objDataWrapper, objLocationInfo.CUSTOMER_ID, objLocationInfo.APP_ID,
                            objLocationInfo.APP_VERSION_ID, RuleType.RiskDependent, DwellingID);
                    }
                }
                if (strlob == ((int)enumLOB.REDW).ToString())
                {
                    if (DwellingID > 0)
                    {
                        ClsHomeCoverages objCoverages = new ClsHomeCoverages("RENTAL");
                        objCoverages.UpdateCoveragesByRuleApp(objDataWrapper, objLocationInfo.CUSTOMER_ID, objLocationInfo.APP_ID,
                            objLocationInfo.APP_VERSION_ID, RuleType.RiskDependent, DwellingID);
                    }
                }

                //Update Coverages for Seasonal/Secondary if Is Primary = "No"//////////////
                //UpdateCoverages(objLocationInfo,objDataWrapper);
                //////////////////////////

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }

        private int GetDwellingForLocation(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int LocationId)
        {
            string strStoredProc = "Proc_Get_DWELLINGS_FOR_LOCATION";

            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
            objDataWrapper.AddParameter("@APP_ID", AppId);
            objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionId);
            objDataWrapper.AddParameter("@LOCATION_ID", LocationId);
            DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
            objDataWrapper.ClearParameteres();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["DWELLING_ID"]);
                }
            }

            return 0;


        }

        private int GetDwellingForLocationPOL(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int LocationId)
        {
            string strStoredProc = "Proc_Get_DWELLINGS_FOR_LOCATION_POL";

            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
            objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
            objDataWrapper.AddParameter("@LOCATION_ID", LocationId);
            DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
            objDataWrapper.ClearParameteres();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["DWELLING_ID"]);
                }
            }

            return 0;


        }

        /// <summary>
        /// Updates the application dwelling coverages when Location is updated
        /// </summary>
        /// <param name="objOldLocationInfo"></param>
        /// <param name="objDataWrapper"></param>
        private void UpdateCoverages(ClsLocationInfo objOldLocationInfo, DataWrapper objDataWrapper)
        {
            /*
            string	strStoredProc =	"Proc_Get_DWELLINGS_FOR_LOCATION";
			
            //DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			
            objDataWrapper.AddParameter("@CUSTOMER_ID",objOldLocationInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@APP_ID",objOldLocationInfo.APP_ID);
            objDataWrapper.AddParameter("@APP_VERSION_ID",objOldLocationInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@LOCATION_ID",objOldLocationInfo.LOCATION_ID);

            DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			
            objDataWrapper.ClearParameteres();


            DataTable dtState = ds.Tables[0];
            DataTable dtDwellings = ds.Tables[1];
			
            //If no dwellings , return

            if ( dtDwellings.Rows.Count == 0 ) return;
			
            int stateID = 0;
            int fraudID = 0;	

            if ( dtState.Rows[0]["STATE_ID"] != DBNull.Value )
            {
                stateID = Convert.ToInt32(dtState.Rows[0]["STATE_ID"]);
            }

            if ( stateID == 14 )
            {
                fraudID = 158;
            }
            else
            {
                fraudID = 84;
            }
			
            int dwellingID = Convert.ToInt32(dtDwellings.Rows[0]["DWELLING_ID"]);
            */

            if (objDataWrapper.CommandParameters.Length > 0)
            {
                objDataWrapper.ClearParameteres();
            }

            objDataWrapper.AddParameter("@CUSTOMER_ID", objOldLocationInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@APP_ID", objOldLocationInfo.APP_ID);
            objDataWrapper.AddParameter("@APP_VERSION_ID", objOldLocationInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@LOCATION_ID", objOldLocationInfo.LOCATION_ID);

            objDataWrapper.ExecuteNonQuery("Proc_UPDATE_APP_DWELLING_COVERAGES_FROM_LOCATION");

            objDataWrapper.ClearParameteres();

        }


        /// <summary>
        /// Updates the policy dwelling coverages when location is updated
        /// </summary>
        /// <param name="objOldLocationInfo"></param>
        /// <param name="objDataWrapper"></param>
        private void UpdatePolicyCoveragesFromLocation(ClsPolicyLocationInfo objOldLocationInfo, DataWrapper objDataWrapper)
        {


            if (objDataWrapper.CommandParameters.Length > 0)
            {
                objDataWrapper.ClearParameteres();
            }

            objDataWrapper.AddParameter("@CUSTOMER_ID", objOldLocationInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@POLICY_ID", objOldLocationInfo.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objOldLocationInfo.POLICY_VERSION_ID);
            objDataWrapper.AddParameter("@LOCATION_ID", objOldLocationInfo.LOCATION_ID);

            objDataWrapper.ExecuteNonQuery("Proc_UPDATE_POL_DWELLING_COVERAGES_FROM_LOCATION");

            objDataWrapper.ClearParameteres();

        }


        public int ActivateDeactivate(ClsLocationInfo objLocationInfo, string IS_ACTIVE)
        {
            int returnResult = 0;
            int retVal;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            strStoredProc = "Proc_ActivateDeactivateLocation";
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objLocationInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", objLocationInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", objLocationInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@LOCATION_ID", objLocationInfo.LOCATION_ID);
                objDataWrapper.AddParameter("@IS_ACTIVE", IS_ACTIVE);
                SqlParameter paramRetVal = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL", SqlDbType.Int, ParameterDirection.ReturnValue);




                if (TransactionLogRequired)
                {
                    objLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/AddLocation.aspx.resx");
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.APP_ID = objLocationInfo.APP_ID;
                    objTransactionInfo.APP_VERSION_ID = objLocationInfo.APP_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objLocationInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY = objLocationInfo.MODIFIED_BY;
                    if (IS_ACTIVE == "Y")
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1467", "");// "Location detail is activated";
                    else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1468", "");// "Location detail is deactivated";
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                retVal = Convert.ToInt32(paramRetVal.Value);
                return retVal;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }



        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldLocationInfo">Model object having old information</param>
        /// <param name="objLocationInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsPolicyLocationInfo objOldLocationInfo, ClsPolicyLocationInfo objLocationInfo)
        {
            string strTranXML;
            int returnResult = 0;
            int RET = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            strStoredProc = "Proc_UpdatePolicyLocation";
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objLocationInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POL_ID", objLocationInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POL_VERSION_ID", objLocationInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@LOC_NUM", objLocationInfo.LOC_NUM);
                if (objLocationInfo.IS_PRIMARY != null)
                {
                    if (objLocationInfo.IS_PRIMARY.Trim() != "")
                    {
                        objDataWrapper.AddParameter("@IS_PRIMARY", objLocationInfo.IS_PRIMARY);
                    }
                    else
                    {
                        objDataWrapper.AddParameter("@IS_PRIMARY", DBNull.Value);
                    }
                }
                else
                {
                    objDataWrapper.AddParameter("@IS_PRIMARY", DBNull.Value);
                }
                objDataWrapper.AddParameter("@LOC_ADD1", objLocationInfo.LOC_ADD1);
                objDataWrapper.AddParameter("@LOC_ADD2", objLocationInfo.LOC_ADD2);
                objDataWrapper.AddParameter("@LOC_CITY", objLocationInfo.LOC_CITY);
                objDataWrapper.AddParameter("@LOC_COUNTY", objLocationInfo.LOC_COUNTY);
                objDataWrapper.AddParameter("@LOC_STATE", objLocationInfo.LOC_STATE);
                objDataWrapper.AddParameter("@LOC_ZIP", objLocationInfo.LOC_ZIP);
                if (objLocationInfo.LOC_COUNTRY != null)
                {
                    objDataWrapper.AddParameter("@LOC_COUNTRY", objLocationInfo.LOC_COUNTRY);
                }
                objDataWrapper.AddParameter("@PHONE_NUMBER", objLocationInfo.PHONE_NUMBER);
                objDataWrapper.AddParameter("@FAX_NUMBER", objLocationInfo.FAX_NUMBER);
                //objDataWrapper.AddParameter("@DEDUCTIBLE",objLocationInfo.DEDUCTIBLE);
                //objDataWrapper.AddParameter("@NAMED_PERILL",objLocationInfo.NAMED_PERILL);
                objDataWrapper.AddParameter("@MODIFIED_BY", objLocationInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objLocationInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@DESCRIPTION", objLocationInfo.DESCRIPTION);
                objDataWrapper.AddParameter("@LOCATION_ID", objLocationInfo.LOCATION_ID);
                objDataWrapper.AddParameter("@LOCATION_TYPE", objLocationInfo.LOCATION_TYPE);

                objDataWrapper.AddParameter("@WEEKS_RENTED", objLocationInfo.WEEKS_RENTED);
                objDataWrapper.AddParameter("@RENTED_WEEKLY", objLocationInfo.RENTED_WEEKLY);

                if (objLocationInfo.LOSSREPORT_ORDER != 0)
                    objDataWrapper.AddParameter("@LOSSREPORT_ORDER", objLocationInfo.LOSSREPORT_ORDER);
                else
                    objDataWrapper.AddParameter("@LOSSREPORT_ORDER", System.DBNull.Value);

                if (objLocationInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@LOSSREPORT_DATETIME", objLocationInfo.LOSSREPORT_DATETIME);
                else
                    objDataWrapper.AddParameter("@LOSSREPORT_DATETIME", System.DBNull.Value);
                objDataWrapper.AddParameter("@REPORT_STATUS", objLocationInfo.REPORT_STATUS);

                //added By Chetna

                objDataWrapper.AddParameter("@CAL_NUM", objLocationInfo.CAL_NUM);
                objDataWrapper.AddParameter("@NAME", objLocationInfo.NAME);
                objDataWrapper.AddParameter("@NUMBER", objLocationInfo.NUMBER);
                objDataWrapper.AddParameter("@DISTRICT", objLocationInfo.DISTRICT);
                objDataWrapper.AddParameter("@OCCUPIED", objLocationInfo.OCCUPIED);
                objDataWrapper.AddParameter("@EXT", objLocationInfo.EXT);
                objDataWrapper.AddParameter("@CATEGORY", objLocationInfo.CATEGORY);
                objDataWrapper.AddParameter("@ACTIVITY_TYPE", objLocationInfo.ACTIVITY_TYPE);
                objDataWrapper.AddParameter("@CONSTRUCTION", objLocationInfo.CONSTRUCTION);
                objDataWrapper.AddParameter("@IS_BILLING", objLocationInfo.IS_BILLING);
                SqlParameter objSqlParameter1 = (SqlParameter)objDataWrapper.AddParameter("@RET", SqlDbType.Int, ParameterDirection.Output);

                if (TransactionLogRequired)
                {
                    objLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\AddPolicyLocations.aspx.resx");
                    strTranXML = objBuilder.GetTransactionLogXML(objOldLocationInfo, objLocationInfo);
                    if (strTranXML == "" || strTranXML == "<LabelFieldMapping></LabelFieldMapping>")
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    else
                    {
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                        objTransactionInfo.TRANS_TYPE_ID = 195;
                        objTransactionInfo.POLICY_ID = objLocationInfo.POLICY_ID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objLocationInfo.POLICY_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objLocationInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objLocationInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1466", ""); //"Location detail is modified";
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    }

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                //Update coverages/////////////////////////////////////////////////////
                //objDataWrapper.ClearParameteres();
                //int DwellingID =GetDwellingForLocationPOL(objDataWrapper,objLocationInfo.CUSTOMER_ID,
                //    objLocationInfo.POLICY_ID ,objLocationInfo.POLICY_VERSION_ID ,objLocationInfo.LOCATION_ID);
                ////Get The Lob Id
                //ClsGeneralInformation obj=new ClsGeneralInformation();
                //string strlob="";
                //strlob=obj.GetPolicyLOBID(objLocationInfo.CUSTOMER_ID,
                //    objLocationInfo.POLICY_ID ,objLocationInfo.POLICY_VERSION_ID ).Tables[0].Rows[0][0].ToString();
                //if(strlob == ((int)enumLOB.HOME).ToString())
                //{
                //    if(DwellingID > 0)
                //    {
                //        ClsHomeCoverages objCoverages= new ClsHomeCoverages();
                //        objCoverages.UpdateCoveragesByRulePolicy(objDataWrapper,objLocationInfo.CUSTOMER_ID,
                //            objLocationInfo.POLICY_ID,
                //            objLocationInfo.POLICY_VERSION_ID,
                //            RuleType.RiskDependent,
                //            DwellingID);
                //    }
                //}
                //if(strlob == ((int)enumLOB.REDW).ToString())
                //{
                //    if(DwellingID > 0)
                //    {
                //        ClsHomeCoverages objCoverages= new ClsHomeCoverages("RENTAL");
                //        objCoverages.UpdateCoveragesByRulePolicy(objDataWrapper,objLocationInfo.CUSTOMER_ID,
                //            objLocationInfo.POLICY_ID,
                //            objLocationInfo.POLICY_VERSION_ID,
                //            RuleType.RiskDependent,
                //            DwellingID);
                //    }
                //}

                //Update Dwelling coverages based on location attributes//////////////
                //this.UpdatePolicyCoveragesFromLocation(objLocationInfo,objDataWrapper);
                ////////////////////////////////////////////////////

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
             //   return returnResult;

                RET = int.Parse(objSqlParameter1.Value.ToString());
                return RET;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }
        #endregion

        #region Delete Method
        /// <summary>
        /// DELETES POL LEVEL LOCATION INFO
        /// </summary>
        /// <param name="objLocationInfo"></param>
        /// <returns></returns>
        public int Delete(ClsPolicyLocationInfo objLocationInfo)
        {
            int returnResult = 0;
            string strStoredProc = "Proc_DeletePolLocations";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objLocationInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objLocationInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objLocationInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@LOCATION_ID", objLocationInfo.LOCATION_ID);


                if (TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 185;
                    objTransactionInfo.RECORDED_BY = objLocationInfo.MODIFIED_BY;
                    objTransactionInfo.POLICY_ID = objLocationInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objLocationInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objLocationInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1469", "");// "Location has been Deleted";
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                if (returnResult > 0)
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                else
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }

        //Added by Pradeep Kushwaha on 02-07-2010
        /// <summary>
        /// Get the max  Location number
        /// </summary>
        /// <param name="LOCATION_NUMBER">Location Number</param>
        /// <param name="flag">Could be in between (1,2)</param>
        /// <returns></returns>
        public String GetMaxIDOfLocationNumber(Int32 CUSTOMER_ID,ref Int64 NUMBER, Int32 flag)
        {
            String returnValue = String.Empty;
            DataSet dsTemp = new DataSet();
            string strSql = "Proc_GetMaxLocationNumber";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@FLAG", flag);
               
                SqlParameter returnSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN", SqlDbType.Int, ParameterDirection.ReturnValue);
                SqlParameter objSqlNUMBERPrem = (SqlParameter)objDataWrapper.AddParameter("@LOCATION_NUMBER", NUMBER, SqlDbType.BigInt, ParameterDirection.InputOutput);


                objDataWrapper.ExecuteNonQuery(strSql);
                returnValue = returnSqlParameter.Value.ToString();

                if (objSqlNUMBERPrem != null && objSqlNUMBERPrem.Value.ToString() != "")
                    NUMBER = Convert.ToInt64(objSqlNUMBERPrem.Value.ToString());

                return returnValue;
            }
            catch 
            { return String.Empty; }
            finally
            { objDataWrapper.Dispose(); }
        }


        /// <summary>
        /// DELETES APP LEVEL LOCATION INFO
        /// </summary>
        /// <param name="intCustomerId"></param>
        /// <param name="intAppid"></param>
        /// <param name="intAppVersionId"></param>
        /// <param name="intLocationId"></param>
        /// <returns></returns>

        public int Delete(ClsLocationInfo objLocationInfo)
        {
            int returnResult = 0;
            string strStoredProc = "Proc_DeleteAppLocations";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objLocationInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", objLocationInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", objLocationInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@LOCATION_ID", objLocationInfo.LOCATION_ID);


                if (TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 185;
                    objTransactionInfo.RECORDED_BY = objLocationInfo.MODIFIED_BY;
                    objTransactionInfo.APP_ID = objLocationInfo.APP_ID;
                    objTransactionInfo.APP_VERSION_ID = objLocationInfo.APP_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objLocationInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1469", "");// "Location has been Deleted";
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                if (returnResult > 0)
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                else
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }
        #endregion

        #region GetLocationInfo

        public static string GetLocationInfo(int intCustomerId, int intAppid, int intAppVersionId, int intLocationId)
        {

            DataSet dsLocationInfo = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerId);
                objDataWrapper.AddParameter("@APP_ID", intAppid);
                objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);
                objDataWrapper.AddParameter("@LOCATION_ID", intLocationId);

                dsLocationInfo = objDataWrapper.ExecuteDataSet(GET_LOCATION_INFO_PROC);

                if (dsLocationInfo.Tables[0].Rows.Count != 0)
                {
                    //return dsLocationInfo.GetXml();
                    return ClsCommon.GetXMLEncoded(dsLocationInfo.Tables[0]);
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

        /// <summary>
        /// Get all the locations based on the customer id.
        /// </summary>
        /// <returns></returns>
        public DataSet GetLocationInfoDetails(Int32 CustomerID)
        {
            string strStoredProc = "Proc_GetLocationInfoDetails";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);


            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds;
        }

        public DataSet GetLocationSourceDetails(Int32 CustomerID, Int32 LocationId)
        {
            string strStoredProc = "Proc_FetchLocationSource";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@LOCATION_ID", LocationId);
            

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds;
        }

        public static string FetchLocationDetails(Int32 CustomerID,Int32 LocationId)
        {
            string strStoredProc = "Proc_FetchLocationDetails";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@LOCATION_ID", LocationId);
            

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

           // ds.Tables[0].Rows[0]["LOC_NUM"] = Convert.ToInt32(null);


            if (ds.Tables[0].Rows.Count != 0)
            {
                return ClsCommon.GetXMLEncoded(ds.Tables[0]);
                //return dsLocationInfo.GetXml();
            }
            else
            {
                return "";
            }
        }

        public static string FetchLocationDetails(Int32 CustomerID, Int32 LocationId, string CALLEDFROM, string ADDRESS)
        {
            string strStoredProc = "Proc_FetchLocationDetails";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@LOCATION_ID", LocationId);
            objWrapper.AddParameter("@CALLEDFROM", CALLEDFROM);
            objWrapper.AddParameter("@ADDRESS", ADDRESS);


            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            // ds.Tables[0].Rows[0]["LOC_NUM"] = Convert.ToInt32(null);


            if (ds.Tables[0].Rows.Count != 0)
            {
                return ClsCommon.GetXMLEncoded(ds.Tables[0]);
                //return dsLocationInfo.GetXml();
            }
            else
            {
                return "";
            }
        }

        public static string GetPolicyLocationInfo(int intCustomerId, int intPolid, int intPolVersionId, int intLocationId)
        {

            DataSet dsLocationInfo = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerId);
                objDataWrapper.AddParameter("@POL_ID", intPolid);
                objDataWrapper.AddParameter("@POL_VERSION_ID", intPolVersionId);
                objDataWrapper.AddParameter("@LOCATION_ID", intLocationId);

                dsLocationInfo = objDataWrapper.ExecuteDataSet(GET_POLICY_LOCATION_INFO_PROC);

                if (dsLocationInfo.Tables[0].Rows.Count != 0)
                {
                    return ClsCommon.GetXMLEncoded(dsLocationInfo.Tables[0]);
                    //return dsLocationInfo.GetXml();
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

        /// <summary>
        /// Returns the locations along with Sublocation description associated with an application
        /// </summary>
        /// <param name="intCustomerID"></param>
        /// <param name="intAppID"></param>
        /// <param name="intAppVersionID"></param>
        /// <returns></returns>
        public static DataTable GetLocationsForApplication1(int intCustomerID, int intAppID, int intAppVersionID)
        {
            string strStoredProc = "Proc_GetLocationsForApplication1";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
            objWrapper.AddParameter("@APP_ID", intAppID);
            objWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds.Tables[0];
        }

        /// <summary>
        /// Returns the locations along with Sublocation description associated with an application
        /// </summary>
        /// <param name="intCustomerID"></param>
        /// <param name="intAppID"></param>
        /// <param name="intAppVersionID"></param>
        /// <returns></returns>
        public static DataTable GetLocationsForApplication(int intCustomerID, int intAppID, int intAppVersionID)
        {
            string strStoredProc = "Proc_GetLocationsForApplication";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
            objWrapper.AddParameter("@APP_ID", intAppID);
            objWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);
            objWrapper.AddParameter("@DWELLING_ID", null);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds.Tables[0];
        }

        /// <summary>
        /// Returns the locations along with Sublocation description associated with an application
        /// </summary>
        /// <param name="intCustomerID"></param>
        /// <param name="intAppID"></param>
        /// <param name="intAppVersionID"></param>
        /// <returns></returns>
        public static DataTable GetLocationsForPolicy(int intCustomerID, int intPolID, int intPolVersionID, int intDwellingID)
        {
            string strStoredProc = "Proc_GetLocationsForPolicy";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
            objWrapper.AddParameter("@POL_ID", intPolID);
            objWrapper.AddParameter("@POL_VERSION_ID", intPolVersionID);
            objWrapper.AddParameter("@DWELLING_ID", intDwellingID);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds.Tables[0];
        }

        /// <summary>
        /// Returns the locations along with Sublocation description associated with an application
        /// </summary>
        /// <param name="intCustomerID"></param>
        /// <param name="intAppID"></param>
        /// <param name="intAppVersionID"></param>
        /// <returns></returns>
        public static DataTable GetLocationsForApplication(int intCustomerID, int intAppID, int intAppVersionID, int intDwellingID)
        {
            string strStoredProc = "Proc_GetLocationsForApplication";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
            objWrapper.AddParameter("@APP_ID", intAppID);
            objWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);
            objWrapper.AddParameter("@DWELLING_ID", intDwellingID);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds.Tables[0];
        }

        /// <summary>
        /// Returns only the locations associated with an application
        /// </summary>
        /// <param name="intCustomerID"></param>
        /// <param name="intAppID"></param>
        /// <param name="intAppVersionID"></param>
        /// <returns></returns>
        public static DataTable GetLocations(int intCustomerID, int intAppID, int intAppVersionID)
        {
            string strStoredProc = "Proc_GetLocations";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
            objWrapper.AddParameter("@APP_ID", intAppID);
            objWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds.Tables[0];
        }
        /// <summary>
        /// get Locations with customer details (used in Loss report)
        /// </summary>
        /// <param name="intCustomerID"></param>
        /// <param name="intAppID"></param>
        /// <param name="intAppVersionID"></param>
        /// <returns></returns>
        public static DataTable GetLocationsWithCustomer(int intCustomerID, int intAppID, int intAppVersionID)
        {
            string strStoredProc = "Proc_GetLocationsWithCustomer";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
            objWrapper.AddParameter("@APP_ID", intAppID);
            objWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds.Tables[0];
        }

        //Added by Mohit Agarwal for Policy 16-Jul-2007
        public static DataTable GetLocationsWithCustomer(int intCustomerID, int intPolID, int intPolVersionID, string CalledFrom)
        {
            string strStoredProc = "Proc_GetLocationsWithCustomer";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
            objWrapper.AddParameter("@APP_ID", intPolID);
            objWrapper.AddParameter("@APP_VERSION_ID", intPolVersionID);
            objWrapper.AddParameter("@CALLED_FROM", CalledFrom);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds.Tables[0];
        }

        #endregion



        #region SelectLocationNumber

        public string LocationNumber(int intCustomerId, int intAppId, int intAppVersionId)
        {
            string strStoredProc = "Proc_LocationNumber";
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", intCustomerId);
            objWrapper.AddParameter("@APP_ID", intAppId);
            objWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);

            DataSet ds = new DataSet();
            try
            {
                ds = objWrapper.ExecuteDataSet(strStoredProc);
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["Loc_Num"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }

        public string PolicyLocationNumber(int intCustomerId, int intPolId, int intPolVersionId)
        {
            string strStoredProc = "Proc_PolicyLocationNumber";
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", intCustomerId);
            objWrapper.AddParameter("@POL_ID", intPolId);
            objWrapper.AddParameter("@POL_VERSION_ID", intPolVersionId);

            DataSet ds = new DataSet();
            try
            {
                ds = objWrapper.ExecuteDataSet(strStoredProc);
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["Loc_Num"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }
        #endregion

        #region SELECTOCCUPIEDAS
        //Modified for itrack-1152/TFS#2598
        public static DataSet SELECTOCCUPIEDAS(string _rubrica, int _OCCUPIED_ID, string _CALLED_FOR)
        {
            string strStoredProc = "PROC_SELECTOCCUPIEDAS";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@RUBRICA", _rubrica);
            objWrapper.AddParameter("@OCCUPIED_ID", _OCCUPIED_ID);
            objWrapper.AddParameter("@CALLED_FOR", _CALLED_FOR);
            
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds;
        }
        #endregion

        #region SELECTACTIVITYTYPE
        public static DataTable SELECTACTIVITYTYPE()
        {
            //int TYPE=0;
            string strStoredProc = "PROC_SELECTACTIVITYTYPE";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            //objWrapper.AddParameter("@TYPE", TYPE);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds.Tables[0];
        }

        #endregion




    }
}
