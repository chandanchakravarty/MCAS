/******************************************************************************************
<Author				: -     Kuldeep Saxena
<Start Date			: -	    14/03/2012
<End Date			: -	
<Description		: -     Business layer class to add update and delete
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Maintenance;

namespace Cms.BusinessLayer.BlCommon
{
  public  class ClsPortMaster : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        private const	string		MNT_PORT_MASTER	=	"MNT_PORT_MASTER";

		#region Private Instance Variables
		private	bool	boolTransactionLog;
		
        private const string ACTIVATE_DEACTIVATE_PROC = "ACTIVATE_DEACTIVATE_MNT_PORT_MASTER";
		#endregion

		#region Public Properties
		public bool TransactionLog
		{
			set
			{
				boolTransactionLog	=	value;
			}
			get
			{
				return boolTransactionLog;
			}
		}
		#endregion

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
        public ClsPortMaster()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objBudgetCategoryInfo ">Model class object.</param>
		/// <returns>No of records effected.</returns>
        public int Add(ClsPortMasterInfo objPortMasterInfo, int CreatedBy)
		{
            string strStoredProc = "PROC_INSERT_MNT_PORT_MASTER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
                        
			try
			{

                objDataWrapper.AddParameter("@ISO_CODE", objPortMasterInfo.ISO_CODE);
                objDataWrapper.AddParameter("@PORT_TYPE", objPortMasterInfo.PORT_TYPE);
                objDataWrapper.AddParameter("@COUNTRY", objPortMasterInfo.COUNTRY);
                objDataWrapper.AddParameter("@ADDITIONAL_WAR_RATE", objPortMasterInfo.ADDITIONAL_WAR_RATE);
                objDataWrapper.AddParameter("@FROM_DATE", objPortMasterInfo.FROM_DATE);
                objDataWrapper.AddParameter("@TO_DATE", objPortMasterInfo.TO_DATE);
                objDataWrapper.AddParameter("@SETTLEMENT_AGENT_CODE", objPortMasterInfo.SETTLEMENT_AGENT_CODE);
                objDataWrapper.AddParameter("@SETTLING_AGENT_NAME", objPortMasterInfo.SETTLING_AGENT_NAME);
                objDataWrapper.AddParameter("@IS_ACTIVE", objPortMasterInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objPortMasterInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objPortMasterInfo.CREATED_DATETIME);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@PORT_CODE", objPortMasterInfo.PORT_CODE, SqlDbType.Int, ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
                    string strTranXML = "";
                    objPortMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddPortMaster.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    if (objPortMasterInfo.TransactLabel != "")
                    {
                        strTranXML = objBuilder.GetTransactionLogXML(objPortMasterInfo);
                    }
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = CreatedBy;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}


                int Port_Code;
				if (objSqlParameter.Value != null && objSqlParameter.Value.ToString() != "")
				{
                    Port_Code = int.Parse(objSqlParameter.Value.ToString());
                    objPortMasterInfo.PORT_CODE = Port_Code;
				}
				else
				{
                    Port_Code = -1;
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (Port_Code == -1)
				{
					return -1;
				}
				else
				{
                    objPortMasterInfo.PORT_CODE = Port_Code;
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldBudgetCategoryInfo ">Model object having old information</param>
		/// <param name="objBudgetCategoryInfo ">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsPortMasterInfo objOldPortMasterInfo, ClsPortMasterInfo objPortMasterInfo)
        {
            string strStoredProc = "PROC_UPDATE_MNT_PORT_MASTER";
            string strTranXML;
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@PORT_CODE", objPortMasterInfo.PORT_CODE);
                objDataWrapper.AddParameter("@ISO_CODE", objPortMasterInfo.ISO_CODE);
                objDataWrapper.AddParameter("@PORT_TYPE", objPortMasterInfo.PORT_TYPE);
                objDataWrapper.AddParameter("@COUNTRY", objPortMasterInfo.COUNTRY);
                objDataWrapper.AddParameter("@ADDITIONAL_WAR_RATE", objPortMasterInfo.ADDITIONAL_WAR_RATE);
                objDataWrapper.AddParameter("@FROM_DATE", objPortMasterInfo.FROM_DATE);
                objDataWrapper.AddParameter("@TO_DATE", objPortMasterInfo.TO_DATE);
                objDataWrapper.AddParameter("@SETTLEMENT_AGENT_CODE", objPortMasterInfo.SETTLEMENT_AGENT_CODE);
                objDataWrapper.AddParameter("@SETTLING_AGENT_NAME", objPortMasterInfo.SETTLING_AGENT_NAME);
                objDataWrapper.AddParameter("@MODIFIED_BY", objPortMasterInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objPortMasterInfo.LAST_UPDATED_DATETIME);



                if (base.TransactionLogRequired)
                {
                    objPortMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addagency.aspx.resx");
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    strTranXML = objBuilder.GetTransactionLogXML(objOldPortMasterInfo, objPortMasterInfo);
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objPortMasterInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = "Information Updated Successfully.";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //objTransactionInfo.CUSTOM_INFO = ";Agency Name = " + ObjAgencyInfo.AGENCY_DISPLAY_NAME + ";Agency Code = " + ObjAgencyInfo.AGENCY_CODE;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
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
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }
		#endregion

        #region ActivateDeactivate() function
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objDVInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        /// //cRETED BY KULDEEP ON 15-MAR-2011
        public int ActivateDeactivatePortDetails(ClsPortMasterInfo objDefaultValuesInfo, string strIS_ACTIVE)
        {
            string strStoredProc = "PROC_ACTIVATE_DEACTIVATE_MNT_PORT_MASTER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            try
            {
                objDataWrapper.AddParameter("@PORT_CODE", objDefaultValuesInfo.PORT_CODE);
                objDataWrapper.AddParameter("@IS_ACTIVE", strIS_ACTIVE);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objDefaultValuesInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    if (strIS_ACTIVE.ToUpper() == "Y")
                        objTransactionInfo.TRANS_DESC = "Port Master Details Activated.";
                    else
                        objTransactionInfo.TRANS_DESC = "Port Master Details Deactivated.";
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);


                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

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
        #endregion

		#region "GetxmlMethods"
		

        public static string GetXmlForPageControls(string Port_Code)
        {
            string strSql = "Proc_Get_MNT_PORT_MASTER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@PORT_CODE", int.Parse(Port_Code));
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet.GetXml();
        }
        public static DataTable GetSettlingAgentList()
        {
            //int TYPE=0;
            string strStoredProc = "Proc_Get_All_MARINE_CARGO_SETTLING_AGENTS";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            //objWrapper.AddParameter("@TYPE", TYPE);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds.Tables[0];
        }
		#endregion
    }
}
