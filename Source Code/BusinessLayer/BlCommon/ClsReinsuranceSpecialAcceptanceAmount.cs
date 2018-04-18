/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 18, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for Special Acceptance Amount 
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
	/// Summary description for ClsReinsuranceSpecialAcceptanceAmount.
	/// </summary>
	public class ClsReinsuranceSpecialAcceptanceAmount:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		
		# region D E C L A R A T I O N 

		private const string MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT="MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT";
		private	bool boolTransactionLog;

		public ClsReinsuranceSpecialAcceptanceAmount()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		}

		# endregion 

		#region A D D  (I N S E R T)
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReinsuranceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsReinsuranceSpecialAcceptanceAmountInfo objReinsuranceSpecialAcceptanceAmountInfo)
		{
			string		strStoredProc	=	"MNT_REIN_INSERT_SPECIALACCEPTANCEAMOUNT";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@SPECIAL_ACCEPTANCE_LIMIT"	,objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT);
				
				if(objReinsuranceSpecialAcceptanceAmountInfo.EFFECTIVE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@EFFECTIVE_DATE"	,objReinsuranceSpecialAcceptanceAmountInfo.EFFECTIVE_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_DATE",System.DBNull.Value);
				objDataWrapper.AddParameter("@LOB_ID"	,objReinsuranceSpecialAcceptanceAmountInfo.LOB_ID);
				objDataWrapper.AddParameter("@CREATED_BY",objReinsuranceSpecialAcceptanceAmountInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objReinsuranceSpecialAcceptanceAmountInfo.CREATED_DATETIME);				

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@SPECIAL_ACCEPTANCE_LIMIT_ID",objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsuranceSpecialAcceptanceAmountInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/Reinsurance/MasterSetup/SpecialAcceptanceAmount.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceSpecialAcceptanceAmountInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceSpecialAcceptanceAmountInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Special Acceptance Limit Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intSpecial_Acceptance_Limit_ID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (intSpecial_Acceptance_Limit_ID == -1)
				{
					return -1;
				}
				else
				{
					objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT_ID= intSpecial_Acceptance_Limit_ID;
					return intSpecial_Acceptance_Limit_ID;
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
		public int Update(ClsReinsuranceSpecialAcceptanceAmountInfo objOldReinsuranceSpecialAcceptanceAmountInfo, ClsReinsuranceSpecialAcceptanceAmountInfo objReinsuranceSpecialAcceptanceAmountInfo)
		{
			string	strStoredProc	=	"MNT_REIN_UPDATE_SPECIALACCEPTANCEAMOUNT";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@SPECIAL_ACCEPTANCE_LIMIT_ID"				,objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT_ID);
				objDataWrapper.AddParameter("@SPECIAL_ACCEPTANCE_LIMIT"				,objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT);
				if(objReinsuranceSpecialAcceptanceAmountInfo.EFFECTIVE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@EFFECTIVE_DATE"	,objReinsuranceSpecialAcceptanceAmountInfo.EFFECTIVE_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_DATE",System.DBNull.Value);
				objDataWrapper.AddParameter("@LOB_ID"	,objReinsuranceSpecialAcceptanceAmountInfo.LOB_ID);
				objDataWrapper.AddParameter("@MODIFIED_BY",objReinsuranceSpecialAcceptanceAmountInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReinsuranceSpecialAcceptanceAmountInfo.LAST_UPDATED_DATETIME);				

				if(base.TransactionLogRequired) 
				{
					objReinsuranceSpecialAcceptanceAmountInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/Reinsurance/MasterSetup/SpecialAcceptanceAmount.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceSpecialAcceptanceAmountInfo, objReinsuranceSpecialAcceptanceAmountInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceSpecialAcceptanceAmountInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Special Acceptance Amount Has Been Updated";
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

		public DataSet GetDataForPageControls(string SPECIAL_ACCEPTANCE_LIMIT_ID)
		{
			string strSql = "MNT_REIN_GETXML_SPECIALACCEPTANCEAMOUNT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@SPECIAL_ACCEPTANCE_LIMIT_ID",SPECIAL_ACCEPTANCE_LIMIT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion
		#region		ACTIVATE/DEACTIVATE
		public void ActivateDeactivateSpecialAcceptance(ClsReinsuranceSpecialAcceptanceAmountInfo objReinsuranceSpecialAcceptanceAmountInfo,string isActive)
		{
			DataWrapper objDataWrapper	=	new DataWrapper( ConnStr, CommandType.StoredProcedure);
			string strActivateDeactivateProcedure="Proc_ActivateDeactivateSpecialAcceptance";
			try
			{
				objDataWrapper.AddParameter("@SPECIAL_ACCEPTANCE_LIMIT_ID",objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",isActive);
				if(TransactionLogRequired) 
				{
					objReinsuranceSpecialAcceptanceAmountInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/Reinsurance/MasterSetup/SpecialAcceptanceAmount.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceSpecialAcceptanceAmountInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY      =   objReinsuranceSpecialAcceptanceAmountInfo.MODIFIED_BY;
					if(isActive.Equals("Y"))
						objTransactionInfo.TRANS_DESC		=	"Special Acceptance Amount has been activated.";
					else
						objTransactionInfo.TRANS_DESC		=	"Special Acceptance Amount has been deactivated.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure, objTransactionInfo);
				}
				else
					objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure);

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}	
		}

		#endregion

		
	}
}

