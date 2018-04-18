/******************************************************************************************
<Author				: -   Agniswar Das
<Start Date			: -	28/09/2011
<End Date			: -	
<Description		: - 	Business layer class to add update and delete
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
using Cms.Model.Maintenance.Accounting;

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Business layer for budget category 
	/// </summary>
	public class ClsFundType : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_FUND_TYPES			=	"MNT_FUND_TYPES";

		#region Private Instance Variables
		private	bool	boolTransactionLog;
		//private int _CATEGEORY_ID;
        private const string ACTIVATE_DEACTIVATE_PROC = "ACTIVATE_DEACTIVATE_MNT_FUND_TYPES";
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
        public ClsFundType()
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
		public int Add(ClsFundTypeInfo  objFundTypeInfo, string XmlFilePath )
		{
            string strStoredProc = "INSERT_MNT_FUND_TYPES";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
                        
			try
			{
                objDataWrapper.AddParameter("@FUND_TYPE_CODE", objFundTypeInfo.FUND_TYPE_CODE);
                objDataWrapper.AddParameter("@FUND_TYPE_NAME", objFundTypeInfo.FUND_TYPE_NAME);
                objDataWrapper.AddParameter("@FUND_TYPE_SOURCE_D", objFundTypeInfo.FUND_TYPE_SOURCE_D);
                objDataWrapper.AddParameter("@FUND_TYPE_SOURCE_DO", objFundTypeInfo.FUND_TYPE_SOURCE_DO);
                objDataWrapper.AddParameter("@FUND_TYPE_SOURCE_RIO", objFundTypeInfo.FUND_TYPE_SOURCE_RIO);
                objDataWrapper.AddParameter("@FUND_TYPE_SOURCE_RIA", objFundTypeInfo.FUND_TYPE_SOURCE_RIA);
                objDataWrapper.AddParameter("@IS_ACTIVE", objFundTypeInfo.IS_ACTIVE);
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@FUND_TYPE_ID", objFundTypeInfo.FUND_TYPE_ID, SqlDbType.Int, ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
                    //objFundTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
                    objFundTypeInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objFundTypeInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY = objFundTypeInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				

				int CATEGEORY_ID;
				if (objSqlParameter.Value != null && objSqlParameter.Value.ToString() != "")
				{
					CATEGEORY_ID = int.Parse(objSqlParameter.Value.ToString());
				}
				else
				{
					CATEGEORY_ID = -1;
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (CATEGEORY_ID == -1)
				{
					return -1;
				}
				else
				{
                    objFundTypeInfo.FUND_TYPE_ID = CATEGEORY_ID;
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
        public int Update(ClsFundTypeInfo objOldBudgetCategoryInfo, ClsFundTypeInfo objBudgetCategoryInfo, string XmlFilePath)
		{
            string strStoredProc = "UPDATE_MNT_FUND_TYPES";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
                objDataWrapper.AddParameter("@FUND_TYPE_ID", objBudgetCategoryInfo.FUND_TYPE_ID);
                objDataWrapper.AddParameter("@FUND_TYPE_CODE", objBudgetCategoryInfo.FUND_TYPE_CODE);
                objDataWrapper.AddParameter("@FUND_TYPE_NAME", objBudgetCategoryInfo.FUND_TYPE_NAME);
                objDataWrapper.AddParameter("@FUND_TYPE_SOURCE_D", objBudgetCategoryInfo.FUND_TYPE_SOURCE_D);
                objDataWrapper.AddParameter("@FUND_TYPE_SOURCE_DO", objBudgetCategoryInfo.FUND_TYPE_SOURCE_DO);
                objDataWrapper.AddParameter("@FUND_TYPE_SOURCE_RIO", objBudgetCategoryInfo.FUND_TYPE_SOURCE_RIO);
                objDataWrapper.AddParameter("@FUND_TYPE_SOURCE_RIA", objBudgetCategoryInfo.FUND_TYPE_SOURCE_RIA);
				if(base.TransactionLogRequired) 
				{
					//objBudgetCategoryInfo .TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
                    objBudgetCategoryInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);

					objBuilder.GetUpdateSQL(objOldBudgetCategoryInfo ,objBudgetCategoryInfo ,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objBudgetCategoryInfo .MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}
				if(objBuilder != null) 
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
        public int ActivateDeactivateDefaultValues(ClsFundTypeInfo objDefaultValuesInfo, string strIS_ACTIVE)
        {
            string strStoredProc = "ACTIVATE_DEACTIVATE_MNT_FUND_TYPES";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            try
            {
                objDataWrapper.AddParameter("@FUND_TYPE_ID", objDefaultValuesInfo.FUND_TYPE_ID);
                objDataWrapper.AddParameter("@IS_ACTIVE", strIS_ACTIVE);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objDefaultValuesInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    if (strIS_ACTIVE.ToUpper() == "Y")
                        objTransactionInfo.TRANS_DESC = "Claims Default Value is Activated.";
                    else
                        objTransactionInfo.TRANS_DESC = "Claims Default Value is Deactivated.";
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
		public DataSet GetBudgetCategory(string CATEGEORY_ID)
		{
			string strSql = "Proc_GetMNT_FUND_TYPES";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CATEGEORY_ID",CATEGEORY_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

        public static string GetXmlForPageControls(string DETAIL_TYPE_ID)
        {
            string strSql = "GET_MNT_FUND_TYPES";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@FUND_TYPE_ID", int.Parse(DETAIL_TYPE_ID));
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet.GetXml();
        }

		#endregion

		#region Fill Drop Down Function
		/// <summary>
		/// This function will fill the budget category drop down list control.
		/// </summary>
		/// <param name="objDropDown"></param>
		public void GetBudgetCategoryInDropdown(DropDownList objDropDown)
		{
			
			string strStoredProc = "Proc_GetDropDownMNT_FUND_TYPES";
			DataSet objDstBC = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDstBC = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				objDropDown.DataSource = objDstBC.Tables[0];
				objDropDown.DataTextField = "CATEGORY_DEPARTEMENT_NAME";
				objDropDown.DataValueField = "ACCOUNT_ID";
				objDropDown.DataBind();
				objDropDown.Items.Insert(0,"");
				//objDropDown.Items[0].Value = "0";
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		/// <summary>
		/// For GL Accounts
		/// </summary>
		/// <param name="objDropDown"></param>
		public void GetBudgetCategoryInDropdownGLActs(DropDownList objDropDown)
		{
			
			string strStoredProc = "Proc_GetDropDownMNT_FUND_TYPES_GL_ACTS";
			DataSet objDstBC = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDstBC = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				objDropDown.DataSource = objDstBC.Tables[0];
				objDropDown.DataTextField = "CATEGORY_DEPARTEMENT_NAME";
				objDropDown.DataValueField = "CATEGEORY_ID";
				objDropDown.DataBind();
				objDropDown.Items.Insert(0,"");
				//objDropDown.Items[0].Value = "0";
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}

		public static DataSet GetWolverineUsers(string systemID)
		{
			
			string strSQLQuery = "SELECT ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS WOLVERINE_USERS,[USER_ID] as WOLVERINE_USER_ID FROM MNT_USER_LIST WHERE USER_SYSTEM_ID = '" +systemID + "' and ISNULL(IS_ACTIVE,'N') = 'Y'  ORDER BY WOLVERINE_USERS ASC";
			DataSet objDS = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
				
			try
			{
				objDS = objDataWrapper.ExecuteDataSet(strSQLQuery);
				return objDS;
			}
			catch(Exception exc)
			{
				throw (new Exception("Some error occurred.Please try again !. " +exc.Message));
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		#endregion
	}
}
