/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	10/3/2005 5:03:34 PM
<End Date			: -	
<Description		: - 	Business layer class to add update and delete the budget category
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

namespace Cms.BusinessLayer.BlCommon.Accounting
{
	/// <summary>
	/// Business layer for budget category 
	/// </summary>
	public class ClsBudgetCategory : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		ACT_BUDGET_CATEGORY			=	"ACT_BUDGET_CATEGORY";

		#region Private Instance Variables
		private	bool	boolTransactionLog;
		//private int _CATEGEORY_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_BUDGET_CATEGORY";
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
		public ClsBudgetCategory()
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
		public int Add(ClsBudgetCategoryInfo  objBudgetCategoryInfo )
		{
			string		strStoredProc	=	"Proc_Insert_ACT_BUDGET_CATEGORY";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CATEGEORY_CODE",objBudgetCategoryInfo .CATEGEORY_CODE);
				objDataWrapper.AddParameter("@CATEGORY_DEPARTEMENT_NAME",objBudgetCategoryInfo .CATEGORY_DEPARTEMENT_NAME);
				objDataWrapper.AddParameter("@RESPONSIBLE_EMPLOYEE_NAME",objBudgetCategoryInfo .RESPONSIBLE_EMPLOYEE_NAME);
				objDataWrapper.AddParameter("@CREATED_BY",objBudgetCategoryInfo .CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objBudgetCategoryInfo .CREATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CATEGEORY_ID",objBudgetCategoryInfo .CATEGEORY_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objBudgetCategoryInfo .TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objBudgetCategoryInfo );
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objBudgetCategoryInfo .CREATED_BY;
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
					objBudgetCategoryInfo .CATEGEORY_ID = CATEGEORY_ID;
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
		public int Update(ClsBudgetCategoryInfo  objOldBudgetCategoryInfo ,ClsBudgetCategoryInfo  objBudgetCategoryInfo )
		{
			string		strStoredProc	=	"Proc_Update_ACT_BUDGET_CATEGORY";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CATEGEORY_ID",objBudgetCategoryInfo .CATEGEORY_ID);
				objDataWrapper.AddParameter("@CATEGEORY_CODE",objBudgetCategoryInfo .CATEGEORY_CODE);
				objDataWrapper.AddParameter("@CATEGORY_DEPARTEMENT_NAME",objBudgetCategoryInfo .CATEGORY_DEPARTEMENT_NAME);
				objDataWrapper.AddParameter("@RESPONSIBLE_EMPLOYEE_NAME",objBudgetCategoryInfo .RESPONSIBLE_EMPLOYEE_NAME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objBudgetCategoryInfo .MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objBudgetCategoryInfo .LAST_UPDATED_DATETIME);
				if(base.TransactionLogRequired) 
				{
					objBudgetCategoryInfo .TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
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

		#region "GetxmlMethods"
		public DataSet GetBudgetCategory(string CATEGEORY_ID)
		{
			string strSql = "Proc_GetACT_BUDGET_CATEGORY";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CATEGEORY_ID",CATEGEORY_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion

		#region Fill Drop Down Function
		/// <summary>
		/// This function will fill the budget category drop down list control.
		/// </summary>
		/// <param name="objDropDown"></param>
		public void GetBudgetCategoryInDropdown(DropDownList objDropDown)
		{
			
			string strStoredProc = "Proc_GetDropDownACT_BUDGET_CATEGORY";
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
			
			string strStoredProc = "Proc_GetDropDownACT_BUDGET_CATEGORY_GL_ACTS";
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
