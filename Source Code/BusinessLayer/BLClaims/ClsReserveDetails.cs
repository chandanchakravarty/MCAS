using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Claims;
using Cms.BusinessLayer.BlCommon;
using System.Text;
using System.Collections;

namespace Cms.BusinessLayer.BLClaims
{

	
	/// <summary>
	/// Summary description for ClsReserveDetails.
	/// </summary>
	public class ClsReserveDetails : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		public ClsReserveDetails()
		{
		}
		public virtual DataSet GetOldData(string strCLAIM_ID)
		{
			return null;
		}
		//Added for Itrack Issue 5548 on 12 June 2009
		public virtual DataSet GetOldData(string strCLAIM_ID, int intActivity)
		{
			return null;
		}
		public  static  DataSet GetDummyPolicyCoveragesForReserve(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetDummyPolicyCoveragesForClaimsReserve";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds!=null)
				return ds;
			else
				return null;
		
		}

		public static string AddCloseReserveDetails(ClsActivityInfo objActivityInfo,string strCLAIM_ID,string UserID)
		{
			string	strStoredProc =	"Proc_InsertClaimCloseReserveDetails";			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);				
			
			try
			{
				objWrapper.ClearParameteres();
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
				//objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));-->Done for Itrack Issue 6873 on 25 Jan 2010
				ds = objWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();

				//Ravindra(01-29-2009)
				objWrapper.AddParameter("@USER_ID",int.Parse(UserID));

				ds= objWrapper.ExecuteDataSet(strStoredProc);
				objWrapper.ClearParameteres();

				//Done for Itrack Issue 6932 on 15 Jan 2010
				ClsCommon objCommon=new ClsCommon();
				if(objCommon.TransactionLogRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					objActivityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddActivity.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objActivityInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	objActivityInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID		=	objActivityInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objActivityInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objActivityInfo.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"Activity has been completed";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 15 Jan 2010
					objWrapper.ExecuteNonQuery(objTransactionInfo);
					objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}

				if(ds!= null && ds.Tables[0].Rows.Count>0 && ds.Tables[0].Rows[0]["ACTIVITY_ID"].ToString()!="")
					return ds.Tables[0].Rows[0]["ACTIVITY_ID"].ToString();
				else
					return "";
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);				
			}
			return "";
		}
		////Added For Itrack Issue #6144.
		//Added strActivityID,is_dr_cr_exists for Itrack Issue #6274,6372 on 23-sep-2009
		public static string AddCloseReinsuranceReserveDetails(ClsActivityInfo objActivityInfo,string strCLAIM_ID,string UserID,string strActivityID,out int is_dr_cr_exists)
		{
			string	strStoredProc =	"Proc_InsertClaimReinsuranceCloseReserveDetails";			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);				
			is_dr_cr_exists=0;
			try
			{
				objWrapper.ClearParameteres();
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
				//objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));-->Done for Itrack Issue 6873 on 25 Jan 2010
				ds = objWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();

				//Ravindra(01-29-2009)
				objWrapper.AddParameter("@USER_ID",int.Parse(UserID));
				//Added  For Itrack Issue #6274,6372 on 23-sep-2009
				if(strActivityID!="")
				{
					objWrapper.AddParameter("@ACTIVITY_ID",strActivityID);
				}
				//objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID)); -->Done for Itrack Issue 6873 on 25 Jan 2010
				SqlParameter retParam = (SqlParameter)objWrapper.AddParameter("@IS_DR_CR_EXISTS",SqlDbType.Int,ParameterDirection.Output);
				ds= objWrapper.ExecuteDataSet(strStoredProc);
				is_dr_cr_exists = Convert.ToInt32(retParam.Value.ToString());
				objWrapper.ClearParameteres();

				//Done for Itrack Issue 6932 on 15 Jan 2010
				ClsCommon objCommon=new ClsCommon();
				if(objCommon.TransactionLogRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					objActivityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddActivity.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objActivityInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	objActivityInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID		=	objActivityInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objActivityInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objActivityInfo.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"Activity has been completed";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;
					objWrapper.ExecuteNonQuery(objTransactionInfo);
					objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}

				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if(ds!= null && ds.Tables[0].Rows.Count>0 && ds.Tables[0].Rows[0]["ACTIVITY_ID"].ToString()!="")
					return ds.Tables[0].Rows[0]["ACTIVITY_ID"].ToString();
				else
					return "";
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);				
			}
			return "";
		}

		//Added for Itrack Issue 6079 on 10 July 2009
		//Added intACTION_ON_PAYMENT For Itrack Issue #6274,6372 on 23-sep-2009
		public string CheckCloseReserveDetails(string strCLAIM_ID,int intACTION_ON_PAYMENT)
		{
			string	strStoredProc =	"Proc_CheckCloseReserveDetails";
			string retVal ="";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.ClearParameteres();
			
			try
			{
				objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
				//Added  For Itrack Issue #6274,6372 on 23-sep-2009
				objWrapper.AddParameter("@ACTION_ON_PAYMENT",intACTION_ON_PAYMENT); 
				DataSet ds= objWrapper.ExecuteDataSet(strStoredProc);
				objWrapper.ClearParameteres();				

				if(ds!=null)
				{
					if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
						retVal = ds.Tables[0].Rows[0][0].ToString();
				}
				return retVal ;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		public static ClsReserveDetails CreateReserveObject(string strLOB_ID)
		{
            if (strLOB_ID == ((int)enumLOB.AUTOP).ToString() || strLOB_ID == ((int)enumLOB.CYCL).ToString() || strLOB_ID == ((int)enumLOB.BOAT).ToString() )//|| strLOB_ID == ((int)enumLOB.ALL_RISK).ToString())				
				return new ClsAutoMotorBoatReserveDetails();			
			else if(strLOB_ID == ((int)enumLOB.HOME).ToString() || strLOB_ID == ((int)enumLOB.REDW).ToString())
				return new ClsHomeReserveDetails();
			else if(strLOB_ID ==  ((int)enumLOB.UMB).ToString())
				return new ClsUmbReserveDetails();			
			else
				return new ClsReserveDetails();
		}

		public  virtual  DataSet GetPolicyCoveragesForReserve(string strCLAIM_ID)
		{
			return null;
		}

		public static string GetActivityID(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetClaimActivityID";			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);						
			SqlParameter retParam = (SqlParameter)objWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);			
			objWrapper.ExecuteNonQuery(strStoredProc);			
			string ActivityID = retParam.Value.ToString();
			return ActivityID;
		}

		public  DataTable GetSchRecords(string strClaim_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);										
			objWrapper.AddParameter("@CLAIM_ID",strClaim_ID);
			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetSchItemCovgForClaims");			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		public  DataTable GetOldSchRecords(string strClaim_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);										
			objWrapper.AddParameter("@CLAIM_ID",strClaim_ID);
			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetOldSchItemCovgForClaims");			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		public static DataSet GetReserveAccounts(string strCLAIM_ID,string strACTIVITY_ID,string strTRANSACTION_ID)
		{
			string	strStoredProc =	"Proc_GetCLM_RESERVE_ACCOUNTS";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
			objWrapper.AddParameter("@ACTIVITY_ID",int.Parse(strACTIVITY_ID));
			objWrapper.AddParameter("@TRANSACTION_ID",int.Parse(strTRANSACTION_ID));
			DataSet objDataSet = objWrapper.ExecuteDataSet(strStoredProc);
			if(objDataSet!=null && objDataSet.Tables[0].Rows.Count>0)
				return objDataSet;
			else
				return null;
		}

		public static string GetTransactionCategory(string strACTION_ON_PAYMENT)
		{
			string	strStoredProc =	"Proc_GetCLM_ACTIVITY_TRANSACTION_CATEGORY";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@ACTION_ON_PAYMENT",int.Parse(strACTION_ON_PAYMENT));
			DataSet objDataSet = objWrapper.ExecuteDataSet(strStoredProc);
			if(objDataSet!=null)
				return objDataSet.Tables[0].Rows[0]["TRANSACTION_CATEGORY"].ToString();
			else
				return null;
		}
		
		public static string GetTransactionID(string strCLAIM_ID, string strACTIVITY_ID)
		{
			string	strStoredProc =	"Proc_GetCLM_RESERVE_TRANSACTION_ID";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
			objWrapper.AddParameter("@ACTIVITY_ID",int.Parse(strACTIVITY_ID));
			//objWrapper.AddParameter("@TRANSACTION_ID",int.Parse(strTRANSACTION_ID));
			DataSet objDataSet = objWrapper.ExecuteDataSet(strStoredProc);
			if(objDataSet!=null)
				return objDataSet.Tables[0].Rows[0]["TRANSACTION_ID"].ToString();
			else
				return null;
		}
		
		public static DataSet GetOldDataForCurrentActivityReserve(string strCLAIM_ID, string strACTIVITY_ID, string strTRANSACTION_ID)
		{
			string	strStoredProc =	"Proc_GetCLM_CURRENT_ACTIVITY_RESERVE";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
			objWrapper.AddParameter("@ACTIVITY_ID",int.Parse(strACTIVITY_ID));
			objWrapper.AddParameter("@TRANSACTION_ID",int.Parse(strTRANSACTION_ID));
			DataSet objDataSet = objWrapper.ExecuteDataSet(strStoredProc);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}
		public static DataSet GetOldDataForDummyPolicyCurrentActivityReserve(string strCLAIM_ID, string strACTIVITY_ID, string strTRANSACTION_ID)
		{
			string	strStoredProc =	"Proc_GetCLM_DUMMYPOLICY_CURRENT_ACTIVITY_RESERVE";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
			objWrapper.AddParameter("@ACTIVITY_ID",int.Parse(strACTIVITY_ID));
			objWrapper.AddParameter("@TRANSACTION_ID",int.Parse(strTRANSACTION_ID));
			DataSet objDataSet = objWrapper.ExecuteDataSet(strStoredProc);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}

		public static DataSet CheckClaimStatus(string strCLAIM_ID, string strACTIVITY_ID)
		{
			string	strStoredProc =	"Proc_GetCLM_ACTIVITY_STATUS";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
			objWrapper.AddParameter("@ACTIVITY_ID",int.Parse(strACTIVITY_ID));
			DataSet objDataSet = objWrapper.ExecuteDataSet(strStoredProc);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}

		//Done for Itrack Issue 5548 on 28 May 09
		public static DataSet GetOldDataForPage(string strCLAIM_ID)
		{
			return GetOldDataForPage(strCLAIM_ID,-1);
		}
		public static DataSet GetOldDataForPage(string strCLAIM_ID,int intActivityId)
		{
			try
			{
				string	strStoredProc =	"Proc_GetCLM_ACTIVITY_RESERVE";
			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				//Done for Itrack Issue 5548 on 28 May 09
				objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
				if (intActivityId!=-1)
					objWrapper.AddParameter("@ACTIVITY_ID",intActivityId);			
			
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		
		}	
	
		public static DataSet GetOldDataForDummyPolicyPage(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetCLM_DUMMY_POLICY_ACTIVITY_RESERVE";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null)
				return ds;
			else
				return null;
		
		}
		
		public int Add(ArrayList alReserves)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				int returnValue = this.Add(alReserves,objDataWrapper);
				if(returnValue>0)
				{
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				return returnValue;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper = null;
			}
		}

		public int Add(ArrayList alReserves,DataWrapper objWrapper)
		{
			
			string	strStoredProc =	"Proc_InsertCLM_ACTIVITY_RESERVE";

			//DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			
			

			try
			{
				//Added for Itrack Issue 7775 on 13 Aug 2010
				double outstanding = 0.0;
				string claimNumber = "";
				int customerId = 0,policyId = 0,policyVersionId = 0;

				DataSet ds = new DataSet();
				Cms.Model.Claims.ClsReserveDetailsInfo objclaimID = (ClsReserveDetailsInfo)alReserves[0];
				objWrapper.AddParameter("@CLAIM_ID",objclaimID.CLAIM_ID);
				ds = objWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				customerId = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				policyId = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				//Loop thru aray list
				for(int i = 0; i < alReserves.Count; i++ )
				{
					Cms.Model.Claims.ClsReserveDetailsInfo objNew = (ClsReserveDetailsInfo)alReserves[i];

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CLAIM_ID",objNew.CLAIM_ID);
					objWrapper.AddParameter("@ACTIVITY_ID",objNew.ACTIVITY_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@PRIMARY_EXCESS",objNew.PRIMARY_EXCESS);
					objWrapper.AddParameter("@MCCA_ATTACHMENT_POINT",objNew.MCCA_ATTACHMENT_POINT);
					objWrapper.AddParameter("@MCCA_APPLIES",objNew.MCCA_APPLIES);
					objWrapper.AddParameter("@ATTACHMENT_POINT",objNew.ATTACHMENT_POINT);
					objWrapper.AddParameter("@OUTSTANDING",objNew.OUTSTANDING);
					objWrapper.AddParameter("@REINSURANCE_CARRIER",objNew.REINSURANCE_CARRIER);
					objWrapper.AddParameter("@RI_RESERVE",objNew.RI_RESERVE);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);					
					objWrapper.AddParameter("@POLICY_LIMITS",objNew.POLICY_LIMITS);					
					objWrapper.AddParameter("@RETENTION_LIMITS",objNew.RETENTION_LIMITS);
					objWrapper.AddParameter("@CREATED_BY",objNew.CREATED_BY);
					objWrapper.AddParameter("@CREATED_DATETIME",objNew.CREATED_DATETIME);
					objWrapper.AddParameter("@ACTION_ON_PAYMENT",objNew.ACTION_ON_PAYMENT);
					objWrapper.AddParameter("@CRACCTS",objNew.CRACCTS);
					objWrapper.AddParameter("@DRACCTS",objNew.DRACCTS);
					objWrapper.AddParameter("@TRANSACTION_ID",objNew.TRANSACTION_ID);
					objWrapper.AddParameter("@ACTUAL_RISK_ID",objNew.ACTUAL_RISK_ID);
					objWrapper.AddParameter("@ACTUAL_RISK_TYPE",objNew.ACTUAL_RISK_TYPE);

					//Added for Itrack Issue 7775 on 13 Aug 2010
					if(objNew.OUTSTANDING.ToString() != "" && objNew.OUTSTANDING != 0.0)
						outstanding = outstanding + objNew.OUTSTANDING;
					else if(objNew.RI_RESERVE.ToString() != "" && objNew.RI_RESERVE != 0.0)
						outstanding = outstanding + objNew.RI_RESERVE;


					objWrapper.ExecuteNonQuery(strStoredProc);
					objWrapper.ClearParameteres();
				}

				//Added for Itrack Issue 7775 on 13 Aug 2010
				if(TransactionLogRequired) 
				{
					string strTranXML = "";
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerId;
					objTransactionInfo.POLICY_ID		=	policyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objclaimID.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"Reserve Details has been added";						
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " + claimNumber + "; Activity # : " + objclaimID.ACTIVITY_ID + "; Total Outstanding: = " + String.Format("{0:n}",outstanding);
					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}

				
			}
			catch(Exception ex)
			{
				//objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);				
				throw(ex);
			} 
			
			//tran.Commit();
			//conn.Close();
			//objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}

		public int Update(ArrayList alReserves)
		{
			
			string	strStoredProc =	"Proc_UpdateCLM_ACTIVITY_RESERVE";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			
			

			try
			{
				//Added for Itrack Issue 7775 on 13 Aug 2010
				double outstanding = 0.0;
				string claimNumber = "";
				int customerId = 0,policyId = 0,policyVersionId = 0;

				DataSet ds = new DataSet();
				Cms.Model.Claims.ClsReserveDetailsInfo objclaimID = (ClsReserveDetailsInfo)alReserves[0];
				objWrapper.AddParameter("@CLAIM_ID",objclaimID.CLAIM_ID);
				ds = objWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				customerId = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				policyId = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				//Loop thru aray list
				for(int i = 0; i < alReserves.Count; i++ )
				{
					Cms.Model.Claims.ClsReserveDetailsInfo objNew = (ClsReserveDetailsInfo)alReserves[i];

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CLAIM_ID",objNew.CLAIM_ID);
					objWrapper.AddParameter("@RESERVE_ID",objNew.RESERVE_ID);
					objWrapper.AddParameter("@ACTIVITY_ID",objNew.ACTIVITY_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@PRIMARY_EXCESS",objNew.PRIMARY_EXCESS);
					objWrapper.AddParameter("@MCCA_ATTACHMENT_POINT",objNew.MCCA_ATTACHMENT_POINT);
					objWrapper.AddParameter("@MCCA_APPLIES",objNew.MCCA_APPLIES);
					objWrapper.AddParameter("@ATTACHMENT_POINT",objNew.ATTACHMENT_POINT);
					objWrapper.AddParameter("@OUTSTANDING",objNew.OUTSTANDING);
					objWrapper.AddParameter("@REINSURANCE_CARRIER",objNew.REINSURANCE_CARRIER);
					objWrapper.AddParameter("@RI_RESERVE",objNew.RI_RESERVE);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@POLICY_LIMITS",objNew.POLICY_LIMITS);					
					objWrapper.AddParameter("@RETENTION_LIMITS",objNew.RETENTION_LIMITS);
					objWrapper.AddParameter("@MODIFIED_BY",objNew.MODIFIED_BY);
					objWrapper.AddParameter("@LAST_UPDATED_DATETIME",objNew.LAST_UPDATED_DATETIME);
					objWrapper.AddParameter("@ACTION_ON_PAYMENT",objNew.ACTION_ON_PAYMENT);
					objWrapper.AddParameter("@CRACCTS",objNew.CRACCTS);
					objWrapper.AddParameter("@DRACCTS",objNew.DRACCTS);
					objWrapper.AddParameter("@TRANSACTION_ID",objNew.TRANSACTION_ID);
					objWrapper.AddParameter("@ACTUAL_RISK_ID",objNew.ACTUAL_RISK_ID);
					objWrapper.AddParameter("@ACTUAL_RISK_TYPE",objNew.ACTUAL_RISK_TYPE);

					//Added for Itrack Issue 7775 on 13 Aug 2010
					if(objNew.OUTSTANDING.ToString() != "" && objNew.OUTSTANDING != 0.0)
						outstanding = outstanding + objNew.OUTSTANDING;
					else if(objNew.RI_RESERVE.ToString() != "" && objNew.RI_RESERVE != 0.0)
						outstanding = outstanding + objNew.RI_RESERVE;

					objWrapper.ExecuteNonQuery(strStoredProc);
					objWrapper.ClearParameteres();
				}

				//Added for Itrack Issue 7775 on 13 Aug 2010
				if(TransactionLogRequired) 
				{				
					string strTranXML = "";
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerId;
					objTransactionInfo.POLICY_ID		=	policyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objclaimID.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"Reserve Details has been updated";						
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " + claimNumber + "; Activity # : " + objclaimID.ACTIVITY_ID + "; Total Outstanding: = " + String.Format("{0:n}",outstanding);
					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);				
				throw(ex);
			} 
			
			//tran.Commit();
			//conn.Close();
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}

		public static DataSet GetTrailerDetails(string CustomerID,string PolicyID, string PolicyVersionID, string AssociatedBoat, string TrailerID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",int.Parse(CustomerID));
				objDataWrapper.AddParameter("@POLICY_ID",int.Parse(PolicyID));
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",int.Parse(PolicyVersionID));
				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",int.Parse(AssociatedBoat));
				objDataWrapper.AddParameter("@TRAILER_ID",int.Parse(TrailerID));
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTRAILER_Details");
			
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}


		public static DataSet GetTrailerDataSet(string CustomerID,string PolicyID, string PolicyVersionID, string AssociatedBoat)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",int.Parse(CustomerID));
				objDataWrapper.AddParameter("@POLICY_ID",int.Parse(PolicyID));
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",int.Parse(PolicyVersionID));
				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",int.Parse(AssociatedBoat));

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTRAILER_INFO");
			
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		public static int GetPredefineAttachmentPoint(string strClaimID)
		{
			string	strStoredProc =	"Proc_GetPreDefinedCLM_MCCA_ATTACHMENT";
			int retVal = 0;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CLAIM_ID",int.Parse(strClaimID));			
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null)
			{
				if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
					retVal = int.Parse(ds.Tables[0].Rows[0][0].ToString());
			}

			return retVal;
		
		}	


		
		
	}

	public class ClsAutoMotorBoatReserveDetails : ClsReserveDetails
	{
		public ClsAutoMotorBoatReserveDetails()
		{
		}
		public  override   DataSet GetPolicyCoveragesForReserve(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetAutoMotorBoatCoveragesForClaimsReserve";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds!=null)
				return ds;
			else
				return null;
		
		}

		
		
		public override DataSet GetOldData(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetCLM_ACTIVITY_RESERVE_MOTOR";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds!=null)
				return ds;
			else
				return null;
		}
	}

	public class ClsRentalReserveDetails : ClsReserveDetails
	{
		public ClsRentalReserveDetails()
		{
		}
		public  override DataSet GetPolicyCoveragesForReserve(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetHomeCoveragesForClaimsReserve";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds!=null)
				return ds;
			else
				return null;
		
		}
	}

	public class ClsUmbReserveDetails : ClsReserveDetails
	{
		public ClsUmbReserveDetails()
		{
		}
		public override DataSet GetOldData(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetCLM_ACTIVITY_RESERVE_UMB";			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);						
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)			
				return ds;
			else
				return null;	
		}

		public  override   DataSet GetPolicyCoveragesForReserve(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetUmbCoveragesForClaimsReserve";			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);						
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)			
				return ds;
			else
				return null;		
		}

	}

	public class ClsHomeReserveDetails : ClsReserveDetails
	{
		public ClsHomeReserveDetails()
		{
		}
		public  override DataSet GetPolicyCoveragesForReserve(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetHomeCoveragesForClaimsReserve";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds!=null)
				return ds;
			else
				return null;
		
		}
		public override DataSet GetOldData(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetCLM_ACTIVITY_RESERVE_HOME";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds!=null)
				return ds;
			else
				return null;
		}
		//Added for Itrack Issue 5548 on 12 June 2009
		public override DataSet GetOldData(string strCLAIM_ID, int intActivity)
		{
			string	strStoredProc =	"Proc_GetCLM_ACTIVITY_RESERVE_HOME";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);
			objWrapper.AddParameter("@ACTIVITY_ID",intActivity);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds!=null)
				return ds;
			else
				return null;
		}


	}


}
