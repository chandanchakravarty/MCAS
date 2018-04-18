/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for Contract Types
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Configuration;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsReinsuranceContractType.
	/// </summary>
	public class ClsReinsuranceContractType : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		private const string MNT_REINSURANCE_CONTRACT_TYPE	 =	"MNT_REINSURANCE_CONTRACT_TYPE";
		private	bool boolTransactionLog;

		public ClsReinsuranceContractType()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= "MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACTTYPE";
		}

		# endregion 

		#region A D D  (I N S E R T)
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReinsuranceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsReinsuranceContractTypeInfo objReinsuranceContractTypeInfo)
		{
			string		strStoredProc	=	"Proc_InsertContractType";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CONTRACT_TYPE_DESC",objReinsuranceContractTypeInfo.CONTRACT_TYPE_DESC);
				objDataWrapper.AddParameter("@CREATED_BY",objReinsuranceContractTypeInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objReinsuranceContractTypeInfo.CREATED_DATETIME);				
				
				//objDataWrapper.AddParameter("@IS_ACTIVE"				,objReinsuranceContractTypeInfo.IS_ACTIVE);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CONTRACTTYPEID",objReinsuranceContractTypeInfo.CONTRACTTYPEID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsuranceContractTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceContractType.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceContractTypeInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceContractTypeInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Contract Type Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intContract_TypeID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (intContract_TypeID == -1)
				{
					return -1;
				}
				else
				{
					objReinsuranceContractTypeInfo.CONTRACTTYPEID= intContract_TypeID;
					return intContract_TypeID;
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
			
		# endregion 

		#region U P D A T E   
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldReinsuranceInfo">Model object having old information</param>
		/// <param name="objReinsuranceInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsReinsuranceContractTypeInfo objOldReinsuranceContractTypeInfo, ClsReinsuranceContractTypeInfo objReinsuranceContractTypeInfo)
		{
			string	strStoredProc	=	"Proc_UpdateContractType";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CONTRACTTYPEID"			,objReinsuranceContractTypeInfo.CONTRACTTYPEID);
				objDataWrapper.AddParameter("@CONTRACT_TYPE_DESC"		,objReinsuranceContractTypeInfo.CONTRACT_TYPE_DESC);
				objDataWrapper.AddParameter("@MODIFIED_BY",objReinsuranceContractTypeInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReinsuranceContractTypeInfo.LAST_UPDATED_DATETIME);				
				
				if(base.TransactionLogRequired) 
				{
					objReinsuranceContractTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceContractType.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceContractTypeInfo, objReinsuranceContractTypeInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceContractTypeInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Contract Type Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
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

		# endregion 

		#region G E T  D A T A   F O R   E D I T   M O D E

		public DataSet GetDataForPageControls(string CONTRACTTYPEID)
		{
			string strSql = "Proc_GetXMLMNT_REINSURANCE_CONTRACTTYPE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACTTYPEID",CONTRACTTYPEID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion

		#region DEACTIVATE OR ACTIVATE CONTRACT TYPE

		public int GetDeactivateActivate(string Contract_Type_ID,string Status_Check)
		{
			string strSql = "MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACTTYPE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACTTYPEID",Contract_Type_ID);
			objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

			//DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			int returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
			return returnResult;
		}

		#endregion
	}
}
