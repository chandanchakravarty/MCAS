/******************************************************************************************
<Author					: - Mohit Agarwal
<Start Date				: -	5-Dec-2007
<End Date				: -	
<Description			: - Executes SP of MNT_CLAIM_COVERAGE
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
using Cms.Model.Claims;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// Summary description for ClsDummyClaimsCoverage.
	/// </summary>
	public class ClsDummyClaimsCoverage: Cms.BusinessLayer.BLClaims.ClsClaims
	{
		public ClsDummyClaimsCoverage()
		{
		}
			
		#region Add
		public int Add(ClsDummyClaimsCoverageInfo objClaimsCoverageInfo)
		{
			string		strStoredProc	=	"Proc_InsertMNT_CLAIM_COVERAGE";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{			
				objDataWrapper.AddParameter("@CLAIM_ID",objClaimsCoverageInfo.CLAIM_ID);		
				objDataWrapper.AddParameter("@COV_DES",objClaimsCoverageInfo.COV_DES);		
				objDataWrapper.AddParameter("@STATE_ID",objClaimsCoverageInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objClaimsCoverageInfo.LOB_ID);		

				if(objClaimsCoverageInfo.LIMIT_1 > 0)
					objDataWrapper.AddParameter("@LIMIT_1",objClaimsCoverageInfo.LIMIT_1);		
				else
					objDataWrapper.AddParameter("@LIMIT_1",System.DBNull.Value);		
				if(objClaimsCoverageInfo.DEDUCTIBLE_1 > 0)
					objDataWrapper.AddParameter("@DEDUCTIBLE_1",objClaimsCoverageInfo.DEDUCTIBLE_1);		
				else
					objDataWrapper.AddParameter("@DEDUCTIBLE_1",System.DBNull.Value);		

				objDataWrapper.AddParameter("@IS_ACTIVE",objClaimsCoverageInfo.IS_ACTIVE);		
				objDataWrapper.AddParameter("@CREATED_BY",objClaimsCoverageInfo.CREATED_BY);		
				objDataWrapper.AddParameter("@CREATED_DATETIME",objClaimsCoverageInfo.CREATED_DATETIME);		
//				objDataWrapper.AddParameter("@MODIFIED_BY",objClaimsCoverageInfo.MODIFIED_BY);		
//				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objClaimsCoverageInfo.LAST_UPDATED_DATETIME);		
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@COV_ID",objClaimsCoverageInfo.COV_ID,SqlDbType.Int ,ParameterDirection.Output);

//				int returnResult = 0;				
//				if(TransactionLogRequired)
//				{
//					objDummyPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"\Claims\Aspx\Policy\DummyPolicyPopUp.aspx.resx");
//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();					
//					string strTranXML = objBuilder.GetTransactionLogXML(objDummyPolicyInfo);
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.RECORDED_BY		=	objDummyPolicyInfo.CREATED_BY;					
//					objTransactionInfo.TRANS_DESC		=	"New DummyPolicy is added";
//					objTransactionInfo.CHANGE_XML		=	strTranXML;					
//					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);					
//				}
//				else
					int returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				int intCOV_ID		=	int.Parse(objSqlParameter.Value.ToString());
				return intCOV_ID;
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return -1;
				//throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		#endregion

		#region Update
		public int Update(ClsDummyClaimsCoverageInfo objClaimsCoverageInfo)
		{
			string		strStoredProc	=	"Proc_UpdateMNT_CLAIM_COVERAGE";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{			
				objDataWrapper.AddParameter("@COV_ID",objClaimsCoverageInfo.COV_ID);		
				objDataWrapper.AddParameter("@CLAIM_ID",objClaimsCoverageInfo.CLAIM_ID);		
				objDataWrapper.AddParameter("@COV_DES",objClaimsCoverageInfo.COV_DES);		
				objDataWrapper.AddParameter("@STATE_ID",objClaimsCoverageInfo.STATE_ID);		
				objDataWrapper.AddParameter("@LOB_ID",objClaimsCoverageInfo.LOB_ID);		
				if(objClaimsCoverageInfo.LIMIT_1 > 0)
					objDataWrapper.AddParameter("@LIMIT_1",objClaimsCoverageInfo.LIMIT_1);		
				else
					objDataWrapper.AddParameter("@LIMIT_1",System.DBNull.Value);		
				if(objClaimsCoverageInfo.DEDUCTIBLE_1 > 0)
					objDataWrapper.AddParameter("@DEDUCTIBLE_1",objClaimsCoverageInfo.DEDUCTIBLE_1);		
				else
					objDataWrapper.AddParameter("@DEDUCTIBLE_1",System.DBNull.Value);		
				objDataWrapper.AddParameter("@IS_ACTIVE",objClaimsCoverageInfo.IS_ACTIVE);		
//				objDataWrapper.AddParameter("@CREATED_BY",objClaimsCoverageInfo.CREATED_BY);		
//				objDataWrapper.AddParameter("@CREATED_DATETIME",objClaimsCoverageInfo.CREATED_DATETIME);		
				objDataWrapper.AddParameter("@MODIFIED_BY",objClaimsCoverageInfo.MODIFIED_BY);		
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objClaimsCoverageInfo.LAST_UPDATED_DATETIME);		

//				int returnResult = 0;				
//				if(TransactionLogRequired)
//				{
//					objDummyPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"\Claims\Aspx\Policy\DummyPolicyPopUp.aspx.resx");
//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();					
//					string strTranXML = objBuilder.GetTransactionLogXML(objDummyPolicyInfo);
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.RECORDED_BY		=	objDummyPolicyInfo.CREATED_BY;					
//					objTransactionInfo.TRANS_DESC		=	"New DummyPolicy is added";
//					objTransactionInfo.CHANGE_XML		=	strTranXML;					
//					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);					
//				}
//				else
					int returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 0;
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return -1;
				//throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		#endregion

		#region Delete
		public int Delete(int covId)
		{
			string		strStoredProc	=	"Proc_DeleteMNT_CLAIM_COVERAGE_COV_ID";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{			
				objDataWrapper.AddParameter("@COV_ID",covId);		
				//				int returnResult = 0;				
				//				if(TransactionLogRequired)
				//				{
				//					objDummyPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"\Claims\Aspx\Policy\DummyPolicyPopUp.aspx.resx");
				//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();					
				//					string strTranXML = objBuilder.GetTransactionLogXML(objDummyPolicyInfo);
				//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				//					objTransactionInfo.TRANS_TYPE_ID	=	1;
				//					objTransactionInfo.RECORDED_BY		=	objDummyPolicyInfo.CREATED_BY;					
				//					objTransactionInfo.TRANS_DESC		=	"New DummyPolicy is added";
				//					objTransactionInfo.CHANGE_XML		=	strTranXML;					
				//					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);					
				//				}
				//				else
				int returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 0;
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return -1;
				//throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		#endregion

		#region Activate/Deactivate
		public int Activate(int covId, string IsActive)
		{
			string		strStoredProc	=	"Proc_ActivateMNT_CLAIM_COVERAGE_COV_ID";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{			
				objDataWrapper.AddParameter("@COV_ID",covId);		
				objDataWrapper.AddParameter("@IS_ACTIVE",IsActive);		
				//				int returnResult = 0;				
				//				if(TransactionLogRequired)
				//				{
				//					objDummyPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"\Claims\Aspx\Policy\DummyPolicyPopUp.aspx.resx");
				//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();					
				//					string strTranXML = objBuilder.GetTransactionLogXML(objDummyPolicyInfo);
				//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				//					objTransactionInfo.TRANS_TYPE_ID	=	1;
				//					objTransactionInfo.RECORDED_BY		=	objDummyPolicyInfo.CREATED_BY;					
				//					objTransactionInfo.TRANS_DESC		=	"New DummyPolicy is added";
				//					objTransactionInfo.CHANGE_XML		=	strTranXML;					
				//					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);					
				//				}
				//				else
				int returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 0;
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return -1;
				//throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		#endregion

		public static string GetCoverageId(string strCOV_ID_CLAIM, string strCLAIM_ID)
		{
			string		strStoredProc	=	"Proc_GetMNT_CLAIM_COVERAGE_COV_ID";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			DataSet dsSet = new DataSet();
			try
			{			
				objDataWrapper.AddParameter("@COV_ID_CLAIM",strCOV_ID_CLAIM.Split('~')[0]);
				objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);
				if(strCOV_ID_CLAIM.Split('~').Length > 1)
					objDataWrapper.AddParameter("@TOT_COV_CLAIM",strCOV_ID_CLAIM.Split('~')[1]);
				dsSet = objDataWrapper.ExecuteDataSet(strStoredProc);
				if(dsSet!=null && dsSet.Tables.Count>0 && dsSet.Tables[0].Rows.Count > 0)
					return dsSet.Tables[0].Rows[0]["COV_ID"].ToString();
				else
					return "0";								
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				//throw(ex);
				return "0";
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
				if(dsSet!=null) dsSet.Dispose();
			}
		}

		#region Get Claim Coverage data for dummy policy
		public static DataTable GetClaimCoverageDataForDummyPolicy(int intCOV_ID)
		{
			string		strStoredProc	=	"Proc_GetMNT_CLAIM_COVERAGE";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			DataSet dsSet = new DataSet();
			try
			{			
				objDataWrapper.AddParameter("@COV_ID",intCOV_ID);
				dsSet = objDataWrapper.ExecuteDataSet(strStoredProc);
				if(dsSet!=null && dsSet.Tables.Count>0)
					return dsSet.Tables[0];
				else
					return null;								
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return null;	
				//throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
				if(dsSet!=null) dsSet.Dispose();
			}
		}

		#endregion

	}
}
