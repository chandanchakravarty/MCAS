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
	public class ClsOccurrenceDetails : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objODInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsOccurrenceDetailsInfo objODInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_OCCURRENCE_DETAIL";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objODInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetClaimDetails");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				int lobId = int.Parse(ds.Tables[0].Rows[0]["POLICY_LOB"].ToString());

				//objDataWrapper.AddParameter("@CLAIM_ID",objODInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@LOSS_DESCRIPTION",objODInfo.LOSS_DESCRIPTION);
				objDataWrapper.AddParameter("@AUTHORITY_CONTACTED",objODInfo.AUTHORITY_CONTACTED);
				objDataWrapper.AddParameter("@REPORT_NUMBER",objODInfo.REPORT_NUMBER);
				objDataWrapper.AddParameter("@VIOLATIONS",objODInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@CREATED_BY",objODInfo.CREATED_BY);
				objDataWrapper.AddParameter("@LOSS_TYPE",objODInfo.LOSS_TYPE);
				objDataWrapper.AddParameter("@LOSS_LOCATION",objODInfo.LOSS_LOCATION);
                // Added by Santosh Kumar Gautam on 25 Nov 2010
                objDataWrapper.AddParameter("@LOSS_LOCATION_ZIP", objODInfo.LOSS_LOCATION_ZIP);
                objDataWrapper.AddParameter("@LOSS_LOCATION_CITY", objODInfo.LOSS_LOCATION_CITY);
                objDataWrapper.AddParameter("@LOSS_LOCATION_STATE", objODInfo.LOSS_LOCATION_STATE);

				objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",objODInfo.ESTIMATE_AMOUNT);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objODInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@WATERBACKUP_SUMPPUMP_LOSS",objODInfo.WATERBACKUP_SUMPPUMP_LOSS); //Added by Charles on 1-Dec-09 for Itrack 6647
				objDataWrapper.AddParameter("@WEATHER_RELATED_LOSS",objODInfo.WEATHER_RELATED_LOSS);  //Added for Itrack 6640 on 9 Dec 09
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@OCCURRENCE_DETAIL_ID",objODInfo.OCCURRENCE_DETAIL_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					objODInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Claims/Aspx/AddOccurrenceDetails.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objODInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;	
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objODInfo.CREATED_BY; 
					objTransactionInfo.RECORD_DATE_TIME =	DateTime.Now; 
					if(lobId == 1 || lobId ==6 || lobId ==7)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1830","");//"Property Loss is added";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1829", "");// "New Occurrence Detail is added";

                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 15 Jan 2010
					//Done for Itrack Issue 6932 on 4 June 2010
					if(lobId == 1 || lobId ==4 || lobId ==6 || lobId ==7)
					{
						strTranXML = strTranXML.Replace("Loss Type","Kind of Loss");
						strTranXML = strTranXML.Replace("Authority Contacted","Department Notified");
					}
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				if (returnResult>0)
				{
					objODInfo.OCCURRENCE_DETAIL_ID = int.Parse(objSqlParameter.Value.ToString());					
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
		/// <param name="objOldODInfo">Model object having old information</param>
		/// <param name="objODInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsOccurrenceDetailsInfo objOldODInfo,ClsOccurrenceDetailsInfo objODInfo)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_OCCURRENCE_DETAIL";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objODInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetClaimDetails");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				int lobId = int.Parse(ds.Tables[0].Rows[0]["POLICY_LOB"].ToString());

				objDataWrapper.AddParameter("@OCCURRENCE_DETAIL_ID",objODInfo.OCCURRENCE_DETAIL_ID);
				//objDataWrapper.AddParameter("@CLAIM_ID",objODInfo.CLAIM_ID);//Done for Itrack Issue 6932 on 15 Jan 2010
				objDataWrapper.AddParameter("@LOSS_DESCRIPTION",objODInfo.LOSS_DESCRIPTION);
				objDataWrapper.AddParameter("@AUTHORITY_CONTACTED",objODInfo.AUTHORITY_CONTACTED);
				objDataWrapper.AddParameter("@REPORT_NUMBER",objODInfo.REPORT_NUMBER);
				objDataWrapper.AddParameter("@VIOLATIONS",objODInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MODIFIED_BY",objODInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LOSS_TYPE",objODInfo.LOSS_TYPE);
				objDataWrapper.AddParameter("@LOSS_LOCATION",objODInfo.LOSS_LOCATION);
                // Added by Santosh Kumar Gautam on 25 Nov 2010
                objDataWrapper.AddParameter("@LOSS_LOCATION_ZIP", objODInfo.LOSS_LOCATION_ZIP);
                objDataWrapper.AddParameter("@LOSS_LOCATION_CITY", objODInfo.LOSS_LOCATION_CITY);
                objDataWrapper.AddParameter("@LOSS_LOCATION_STATE", objODInfo.LOSS_LOCATION_STATE);

				objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",objODInfo.ESTIMATE_AMOUNT);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objODInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@WATERBACKUP_SUMPPUMP_LOSS",objODInfo.WATERBACKUP_SUMPPUMP_LOSS); //Added by Charles on 1-Dec-09 for Itrack 6647
				objDataWrapper.AddParameter("@WEATHER_RELATED_LOSS",objODInfo.WEATHER_RELATED_LOSS);  //Added for Itrack 6640 on 9 Dec 09

				if(TransactionLogRequired) 
				{
					objODInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Claims/Aspx/AddOccurrenceDetails.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldODInfo,objODInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;	
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objODInfo.MODIFIED_BY;
					if(lobId == 1 || lobId ==6 || lobId ==7)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1831","");//"Property Loss is modified";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1832", "");// "Occurrence Detail is modified";

                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 15 Jan 2010
					//Done for Itrack Issue 6932 on 4 June 2010
					if(lobId == 1 || lobId ==4 || lobId ==6 || lobId ==7)
					{
						strTranXML = strTranXML.Replace("Loss Type","Kind of Loss");
						strTranXML = strTranXML.Replace("Authority Contacted","Department Notified");
					}
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
		public string GetXmlForPageControls(int OCCURRENCE_DETAIL_ID, int CLAIM_ID)
		{
			DataSet dsTemp = GetValuesOfOccuranceDetail(OCCURRENCE_DETAIL_ID,CLAIM_ID);
			return dsTemp.GetXml();
		}

		public DataSet GetValuesOfOccuranceDetail(int OCCURRENCE_DETAIL_ID, int CLAIM_ID)
		{
			string strSql = "Proc_GetValuesOfCLM_OCCURRENCE_DETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@OCCURRENCE_DETAIL_ID",OCCURRENCE_DETAIL_ID);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		public DataSet GetDateTimeOfLoss(int CLAIM_ID)
		{
			string strSql = "Proc_GetClaimDateTimeOfLoss";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion
	
		#region Delete Method
		public int Delete(int OCCURRENCE_DETAIL_ID, int CLAIM_ID)
		{
			string		strStoredProc	=	"Proc_DeleteCLM_OCCURRENCE_DETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@OCCURRENCE_DETAIL_ID",OCCURRENCE_DETAIL_ID);
				objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);

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
