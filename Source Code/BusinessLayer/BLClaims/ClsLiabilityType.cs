/******************************************************************************************
<Author					: -   Vijay Arora
<Start Date				: -		5/3/2006 4:06:04 PM
<End Date				: -	
<Description			: - 	Business Layer Class for Occurance Details
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
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
	public class ClsLiabilityType : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const string InsertCLM_LIABILITY_TYPE		=	"Proc_InsertCLM_LIABILITY_TYPE";
		private const string UpdateCLM_LIABILITY_TYPE		=	"Proc_UpdateCLM_LIABILITY_TYPE";
		private const string GetCLM_LIABILITY_TYPE			=	"Proc_GetCLM_LIABILITY_TYPE";


		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objLiabilityTypeInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsLiabilityTypeInfo objLiabilityTypeInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objLiabilityTypeInfo.CLAIM_ID);				
				objDataWrapper.AddParameter("@PREMISES_INSURED",objLiabilityTypeInfo.PREMISES_INSURED);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objLiabilityTypeInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@TYPE_OF_PREMISES",objLiabilityTypeInfo.TYPE_OF_PREMISES);
				objDataWrapper.AddParameter("@CREATED_BY",objLiabilityTypeInfo.CREATED_BY);				
				objDataWrapper.AddParameter("@CREATED_DATETIME",objLiabilityTypeInfo.CREATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LIABILITY_TYPE_ID",objLiabilityTypeInfo.LIABILITY_TYPE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					objLiabilityTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Claims/Aspx/AddOccurrenceDetails.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objLiabilityTypeInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objLiabilityTypeInfo.CREATED_BY; 
					objTransactionInfo.RECORD_DATE_TIME =	DateTime.Now;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1764", ""); //"New Liability Type is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_LIABILITY_TYPE,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_LIABILITY_TYPE);
				
				if (returnResult>0)
				{
					objLiabilityTypeInfo.LIABILITY_TYPE_ID = int.Parse(objSqlParameter.Value.ToString());					
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
		/// <param name="objOldLiabilityTypeInfo">Model object having old information</param>
		/// <param name="objLiabilityTypeInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsLiabilityTypeInfo objOldLiabilityTypeInfo,ClsLiabilityTypeInfo objLiabilityTypeInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@LIABILITY_TYPE_ID",objLiabilityTypeInfo.LIABILITY_TYPE_ID);				
				objDataWrapper.AddParameter("@CLAIM_ID",objLiabilityTypeInfo.CLAIM_ID);				
				objDataWrapper.AddParameter("@PREMISES_INSURED",objLiabilityTypeInfo.PREMISES_INSURED);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objLiabilityTypeInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@TYPE_OF_PREMISES",objLiabilityTypeInfo.TYPE_OF_PREMISES);
				objDataWrapper.AddParameter("@MODIFIED_BY",objLiabilityTypeInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objLiabilityTypeInfo.LAST_UPDATED_DATETIME);

				if(TransactionLogRequired) 
				{
					objLiabilityTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Claims/Aspx/AddOccurrenceDetails.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldLiabilityTypeInfo,objLiabilityTypeInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objLiabilityTypeInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1752", "");// "Liability Type is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_LIABILITY_TYPE,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_LIABILITY_TYPE);

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
		public static DataTable GetXmlForPageControls(int intLIABILITY_TYPE_ID, int intCLAIM_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@LIABILITY_TYPE_ID",intLIABILITY_TYPE_ID);
			objDataWrapper.AddParameter("@CLAIM_ID",intCLAIM_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_LIABILITY_TYPE);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}	
		#endregion
	
	}
}
