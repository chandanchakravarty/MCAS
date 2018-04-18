/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	4/24/2006 12:26:20 PM
<End Date				: -	
<Description				: - 	Business Layer class for table named CLM_CATASTROPHE_EVENT
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance.Claims;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsCatastropheEvent : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsCatastropheEvent()
		{
			base.strActivateDeactivateProcedure = "Proc_ActivateDeactivateCLM_CATASTROPHE_EVENT";
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objCEInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsCatastropheEventInfo objCEInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_CATASTROPHE_EVENT";
			DateTime	RecordDate		=	DateTime.Now;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CATASTROPHE_EVENT_TYPE",objCEInfo.CATASTROPHE_EVENT_TYPE);
				objDataWrapper.AddParameter("@DATE_FROM",objCEInfo.DATE_FROM);
				objDataWrapper.AddParameter("@DATE_TO",objCEInfo.DATE_TO);
				objDataWrapper.AddParameter("@DESCRIPTION",objCEInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@CAT_CODE",objCEInfo.CAT_CODE);
				objDataWrapper.AddParameter("@CREATED_BY",objCEInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objCEInfo.CREATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CATASTROPHE_EVENT_ID",objCEInfo.CATASTROPHE_EVENT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objCEInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddCatastropheEvent.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objCEInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objCEInfo.CREATED_BY; 
					objTransactionInfo.RECORD_DATE_TIME =	DateTime.Now; 
					objTransactionInfo.TRANS_DESC		=	"New Catastrophe Event is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				if (returnResult>0)
				{
					objCEInfo.CATASTROPHE_EVENT_ID = int.Parse(objSqlParameter.Value.ToString());					
				}

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return returnResult;
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
		/// <param name="objOldCEInfo">Model object having old information</param>
		/// <param name="objCEInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsCatastropheEventInfo objOldCEInfo,ClsCatastropheEventInfo objCEInfo)
		{
			string	strStoredProc	=	"Proc_UpdateCLM_CATASTROPHE_EVENT";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CATASTROPHE_EVENT_ID",objCEInfo.CATASTROPHE_EVENT_ID);
				objDataWrapper.AddParameter("@CATASTROPHE_EVENT_TYPE",objCEInfo.CATASTROPHE_EVENT_TYPE);
				objDataWrapper.AddParameter("@DATE_FROM",objCEInfo.DATE_FROM);
				objDataWrapper.AddParameter("@DATE_TO",objCEInfo.DATE_TO);
				objDataWrapper.AddParameter("@DESCRIPTION",objCEInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@CAT_CODE",objCEInfo.CAT_CODE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objCEInfo.MODIFIED_BY);
				
				if(TransactionLogRequired) 
				{
					objCEInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddCatastropheEvent.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldCEInfo,objCEInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objCEInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Catastrophe Event is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
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
		public static string GetXmlForPageControls(string CATASTROPHE_EVENT_ID)
		{
			string strSql = "Proc_GetDataCLM_CATASTROPHE_EVENT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CATASTROPHE_EVENT_ID",int.Parse(CATASTROPHE_EVENT_ID));
            objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion

		
	}
}
