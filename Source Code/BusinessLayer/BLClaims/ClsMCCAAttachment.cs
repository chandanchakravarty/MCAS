/******************************************************************************************
<Author					: -   Vijay Arora
<Start Date				: -	  08-08-2006
<End Date				: -	
<Description			: -   Business Layer Class for MCCA Attachment.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - VSS test
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance.Claims;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Claims;
namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsMCCAAttachment : Cms.BusinessLayer.BLClaims.ClsClaims
	{

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsMCCAAttachment()
		{}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMCCAInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsMCCAAttachmentInfo objMCCAInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_MCCA_ATTACHMENT";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@POLICY_PERIOD_DATE_FROM",objMCCAInfo.POLICY_PERIOD_DATE_FROM);
				objDataWrapper.AddParameter("@POLICY_PERIOD_DATE_TO",objMCCAInfo.POLICY_PERIOD_DATE_TO);
				objDataWrapper.AddParameter("@LOSS_PERIOD_DATE_FROM",objMCCAInfo.LOSS_PERIOD_DATE_FROM);
				objDataWrapper.AddParameter("@LOSS_PERIOD_DATE_TO",objMCCAInfo.LOSS_PERIOD_DATE_TO);
				objDataWrapper.AddParameter("@MCCA_ATTACHMENT_POINT",objMCCAInfo.MCCA_ATTACHMENT_POINT);
				objDataWrapper.AddParameter("@CREATED_BY",objMCCAInfo.CREATED_BY);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@MCCA_ATTACHMENT_ID",objMCCAInfo.MCCA_ATTACHMENT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					objMCCAInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Maintenance/Claims/AddMCCAAttachment.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objMCCAInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objMCCAInfo.CREATED_BY; 
					objTransactionInfo.RECORD_DATE_TIME =	DateTime.Now; 
					objTransactionInfo.TRANS_DESC		=	"New Claims MCCA Attachment is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				if (returnResult>0)
				{
					objMCCAInfo.MCCA_ATTACHMENT_ID = int.Parse(objSqlParameter.Value.ToString());					
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
		/// <param name="objOldMCCAInfo">Model object having old information</param>
		/// <param name="objMCCAInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsMCCAAttachmentInfo objOldMCCAInfo,ClsMCCAAttachmentInfo objMCCAInfo)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_MCCA_ATTACHMENT";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@MCCA_ATTACHMENT_ID",objMCCAInfo.MCCA_ATTACHMENT_ID);
				objDataWrapper.AddParameter("@POLICY_PERIOD_DATE_FROM",objMCCAInfo.POLICY_PERIOD_DATE_FROM);
				objDataWrapper.AddParameter("@POLICY_PERIOD_DATE_TO",objMCCAInfo.POLICY_PERIOD_DATE_TO);
				objDataWrapper.AddParameter("@LOSS_PERIOD_DATE_FROM",objMCCAInfo.LOSS_PERIOD_DATE_FROM);
				objDataWrapper.AddParameter("@LOSS_PERIOD_DATE_TO",objMCCAInfo.LOSS_PERIOD_DATE_TO);
				objDataWrapper.AddParameter("@MCCA_ATTACHMENT_POINT",objMCCAInfo.MCCA_ATTACHMENT_POINT);
				objDataWrapper.AddParameter("@MODIFIED_BY",objMCCAInfo.MODIFIED_BY);

				if(TransactionLogRequired) 
				{
					objMCCAInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Maintenance/Claims/AddMCCAAttachment.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMCCAInfo,objMCCAInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objMCCAInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Claims MCCA Attachment is modified";
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
		public string GetXmlForPageControls(int MCCA_ATTACHMENT_ID)
		{
			DataSet dsTemp = GetValuesOfMCCAAttachment(MCCA_ATTACHMENT_ID);
			return dsTemp.GetXml();
		}

		public DataSet GetValuesOfMCCAAttachment(int MCCA_ATTACHMENT_ID)
		{
			string strSql = "Proc_GetValuesForCLM_MCCA_ATTACHMENT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@MCCA_ATTACHMENT_ID",MCCA_ATTACHMENT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		public int ActivateDeactivateMCCAAttachment(int MCCA_ATTACHMENT_ID, string strIS_ACTIVE)
		{
			string strSql = "Proc_ActivateDeactivateCLM_MCCA_ATTACHMENT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@MCCA_ATTACHMENT_ID",MCCA_ATTACHMENT_ID);
			objDataWrapper.AddParameter("@IS_ACTIVE",strIS_ACTIVE);
			int returnResult = objDataWrapper.ExecuteNonQuery(strSql);
			return returnResult;
		}
		#endregion


		#region Delete Method
		public int Delete(int MCCA_ATTACHMENT_ID)
		{
			string		strStoredProc	=	"Proc_DeleteCLM_MCCA_ATTACHMENT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@MCCA_ATTACHMENT_ID",MCCA_ATTACHMENT_ID);

				int returnResult = 0;
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
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
	}
}
