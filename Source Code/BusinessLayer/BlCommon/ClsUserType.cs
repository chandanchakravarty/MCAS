/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	5/10/2005 11:01:50 AM
<End Date				: -	
<Description				: - 	This file is used to
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance;
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// This file is used
	/// </summary>
	public class ClsUserType : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_USER_TYPES			=	"MNT_USER_TYPES";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _USER_TYPE_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateUserType";
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
		public ClsUserType()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objUserTypeInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsUserTypeInfo objUserTypeInfo)
		{
			string		strStoredProc	=	"Proc_InsertUserType";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@USER_TYPE_CODE",objUserTypeInfo.USER_TYPE_CODE);
				objDataWrapper.AddParameter("@USER_TYPE_DESC",objUserTypeInfo.USER_TYPE_DESC);
				objDataWrapper.AddParameter("@USER_TYPE_SYSTEM",objUserTypeInfo.USER_TYPE_SYSTEM);
				objDataWrapper.AddParameter("@USER_TYPE_FOR_CARRIER",objUserTypeInfo.USER_TYPE_FOR_CARRIER);
				objDataWrapper.AddParameter("@IS_ACTIVE",objUserTypeInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objUserTypeInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objUserTypeInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@SYSTEM_GENERATED_CODE",0); // 0 has been explicitly passed for NON SYSTEM GENERATED CODES (USER CODES)
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@USER_TYPE_ID",objUserTypeInfo.USER_TYPE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objUserTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AddUserType.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objUserTypeInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objUserTypeInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1551", "");// "New user type is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int USER_TYPE_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (USER_TYPE_ID == -1)
				{
					return -1;
				}
				else
				{
					objUserTypeInfo.USER_TYPE_ID = USER_TYPE_ID;
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
		/// <param name="objOldUserTypeInfo">Model object having old information</param>
		/// <param name="objUserTypeInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsUserTypeInfo objOldUserTypeInfo,ClsUserTypeInfo objUserTypeInfo)
		{
			string		strStoredProc =	"Proc_UpdateUserType";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@User_Type_Id",objUserTypeInfo.USER_TYPE_ID);
				objDataWrapper.AddParameter("@USER_TYPE_CODE",objUserTypeInfo.USER_TYPE_CODE);
				objDataWrapper.AddParameter("@USER_TYPE_DESC",objUserTypeInfo.USER_TYPE_DESC);
				objDataWrapper.AddParameter("@USER_TYPE_SYSTEM",objUserTypeInfo.USER_TYPE_SYSTEM);
				objDataWrapper.AddParameter("@USER_TYPE_FOR_CARRIER",objUserTypeInfo.USER_TYPE_FOR_CARRIER);
				//objDataWrapper.AddParameter("@IS_ACTIVE",objUserTypeInfo.IS_ACTIVE);
				//objDataWrapper.AddParameter("@CREATED_BY",objUserTypeInfo.CREATED_BY);
				//objDataWrapper.AddParameter("@CREATED_DATETIME",objUserTypeInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objUserTypeInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objUserTypeInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@SYSTEM_GENERATED_CODE",0); // 0 has been explicitly passed for NON SYSTEM GENERATED CODES (USER CODES)
				if(TransactionLogRequired) 
				{

					objUserTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AddUserType.aspx.resx");Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldUserTypeInfo,objUserTypeInfo);
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	objUserTypeInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1552", "");//"User type is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";User Type Code = " + objUserTypeInfo.USER_TYPE_CODE ;
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

		#region Activate Deactivate
		public int ActivateDeactivateUserType(int UserTypeID, string strSTATUS,string CustomerInfo,int ModifiedBy)
		{
			string	strStoredProc =	"Proc_ActivateDeactivateUserType";
			int retVal=-1;
			int returnResult;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objDataWrapper.AddParameter("@CODE",UserTypeID);
			objDataWrapper.AddParameter("@IS_ACTIVE",strSTATUS);
			SqlParameter paramRetVal = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			if(TransactionLogRequired) 
			{
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	ModifiedBy;
				if(strSTATUS.ToUpper().Equals("Y"))
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1553", "");//"User Type has been deactivated successfully";
				else
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1554", "");//"User Type has been activated successfully";
				objTransactionInfo.CUSTOM_INFO			=	CustomerInfo;
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			
			if(paramRetVal.Value.ToString()!="")
				retVal = Convert.ToInt32(paramRetVal.Value);
			return retVal;
		}
		#endregion

		#region "Get Xml Methods"
		public static string GetXmlForPageControls(string strUserTypeId)
		{
			//<Gaurav> 31 May 2005 START: InLine Query Changes to Stroded Proc
			string strProcedure = "Proc_GetUserType";
			DataSet dsUserType = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@UserTypeId",strUserTypeId);
                objDataWrapper.AddParameter("@Lang_id", ClsCommon.BL_LANG_ID);
					dsUserType = objDataWrapper.ExecuteDataSet(strProcedure);

				if(dsUserType.Tables[0].Rows.Count!=0)
				{
					return dsUserType.GetXml();
				}
				else
				{
					return "";
				}
			}
			catch(Exception ex)
			{
				throw (ex);
			}
			finally
			{
				objDataWrapper.Dispose();
				dsUserType.Dispose();
			}
			//<Gaurav> 31 May 2005 END: InLine Query Changes to Stroded Proc

			
		}

		public static DataSet GetUserType(string strUserTypeId)
		{
			//<Gaurav> 31 May 2005 START: InLine Query Changes to Stroded Proc
			string strProcedure = "Proc_GetUserType";
			DataSet dsUserType = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@UserTypeId",strUserTypeId);
				dsUserType = objDataWrapper.ExecuteDataSet(strProcedure);

					return dsUserType;
			
			}
			catch(Exception ex)
			{
				throw (ex);
			}
			finally
			{
				objDataWrapper.Dispose();
				dsUserType.Dispose();
			}
			//<Gaurav> 31 May 2005 END: InLine Query Changes to Stroded Proc

			
		}
		#endregion

	}
}
