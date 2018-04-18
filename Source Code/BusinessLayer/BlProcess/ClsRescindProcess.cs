/******************************************************************************************
<Author				: -  kranti
<Start Date			: -	 17 jan 2007
<End Date			: -	 
<Description		: -  Class for Rescind Process.
<Review Date		: - 
<Reviewed By		: - 	

Modification History
<Modified By		: - Pravesh K Chandel
<Modified Date		: - 9 April 2007	
<Purpose			: -  
*******************************************************************************************/ 

using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.Model.Policy.Process;

namespace Cms.BusinessLayer.BlProcess
{
	
	/// <summary>
	/// Summary description for ClsRescindProcess.
	/// </summary>
	public class ClsRescindProcess : ClsPolicyProcess
	{
		public ClsRescindProcess()
		{			
		}
		#region override vertual function
		protected override bool OnWriteTransactionLog()
		{
			return false;
		}
		protected override bool OnCheckProcessEligibility()
		{
			return false;
		}
		protected override bool OnSetPolicyStatus()
		{
			return false;
		}
		#endregion
		public override bool StartProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
		{
			//Calling the base class start process methos which will
			//insert the record in POL_POLICY_PROCESS table
			//and will do the transaction log entry
			try
			{
				
				//For checking policy status ... now rescind process’ll not work on cancelled or suspended process				
				//string policyStatus = Proc_GetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID);
				//Remove the call for sp and take the value of the status directly from model object itself
//				string PolicyCurrentStatus = GetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID);
//				if(PolicyCurrentStatus == "NORMAL" )
//				{
					TransactionDescription = new System.Text.StringBuilder();
					base.BeginTransaction(); 
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND;
				//Checking the eligibility
				if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
				{
					base.RollbackTransaction();
					return false;
				}

				//Creating new version of policy
                int NewVersionID = 0;
                 string NewDispVersionID="";
				CreatePolicyNewVersion(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out NewVersionID,out NewDispVersionID,"29");
                TransactionDescription.Append("New version (" + NewDispVersionID + ") of Policy has been created.;");
				objProcessInfo.NEW_POLICY_VERSION_ID = NewVersionID;

				//updating the status of new Version of the policy
				string strNewStatusDesc="",strNewStatus="";
				SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.PROCESS_ID ,out strNewStatusDesc,out strNewStatus ); 
				objProcessInfo.POLICY_CURRENT_STATUS =strNewStatus ;
				TransactionDescription.Append("Status of New version (" + NewVersionID.ToString("#.0") + ") of Policy has been updated to " + strNewStatusDesc  + ".;");
				bool retVal =  base.StartProcess (objProcessInfo);
				if (retVal)
				{
					//Write transaction log entry
					base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID
						,GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.CREATED_BY, TransactionDescription.ToString(),TransactionChangeXML.ToString());
					//Commiting the database transaction
					base.CommitTransaction();
				}
				else
					base.RollbackTransaction(); 
				return retVal;
				
			}
			catch(Exception exc)
			{
				base.RollbackTransaction();
				throw (exc);
			}

		}
		public static DateTime GetPolicyInceptionDate(int CustomerID,int PolID, int PolVersionID)
		{
			string		strStoredProc	=	"Proc_GetPolicyInceptionDate";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			DataSet dsTemp = new DataSet();
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POL_ID",PolID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",PolVersionID);
				dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
				return Convert.ToDateTime(dsTemp.Tables[0].Rows[0][0].ToString());
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

		/// <summary>
		/// Gets the Agency Phone No.
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intPolicyID"></param>
		/// <param name="intPolicyVersionID"></param>
		/// <returns></returns>
		public DataSet GetAgencyPhoneNo(int intCustomerID,int intPolicyID,int intPolicyVersionID)
		{
		
			string strStoredProc = "Proc_GetAgencyPhoneNo"; //check this is for cancellation
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CustomerID",intCustomerID);
				objDataWrapper.AddParameter("@PolicyID",intPolicyID);
				objDataWrapper.AddParameter("@PolicyVersionID",intPolicyVersionID);
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				return ds;				
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
		/// Gets the Return Premium
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="CalcMethod"></param>
		/// <returns></returns>
		public int PostReturnPremium(int CustomerID, int PolicyID, int PolicyVersionID, int CalcMethod, int RowID, int PremiumAmount,int MCCAFee, int OtherFee, out string Description)
		{
			string		strStoredProc	=	"Proc_GetPolicyProcessReturnPremium"; //check this is for cancellation 
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
				objDataWrapper.AddParameter("@CALCULATE_METHOD",CalcMethod);
				objDataWrapper.AddParameter("@ROW_ID",RowID);
				objDataWrapper.AddParameter("@PREMIUM_AMOUNT",PremiumAmount);
				objDataWrapper.AddParameter("@MCCA_FEE",MCCAFee);
				objDataWrapper.AddParameter("@OTHER_FEE",OtherFee);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@TRAN_DESC",null,SqlDbType.VarChar,ParameterDirection.Output,1000);

				int returnResult = 0;
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);

				Description = objSqlParameter.Value.ToString();

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

		public override bool CommitProcess(ClsProcessInfo objProcessInfo)
		{
			//string PolicyPreviousStatus = GetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID);
			try
			{
				objProcessInfo.PROCESS_ID = ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS;
				ClsPolicyErrMsg.strMessage="";
				base.BeginTransaction();
				if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
				{
					base.RollbackTransaction();
					ClsPolicyErrMsg.strMessage ="This Process is not Eligible on This Policy."; 
					return false;
				}
				//int PremiumAmout = 0, MCCAFee = 0, OtherFee = 0;
				//int ReturnPremium = 0;				
				decimal CarryForwardAmount;
				string Desc = "";
				// UPDATING RETURN PREMIUM AND EFF DATE IN PROCESS TABLE
				UpdateProcessInfoBeforeCommit(objProcessInfo);

				if(objProcessInfo.POLICY_PREVIOUS_STATUS.ToUpper() == POLICY_STATUS_NORMAL)
				{
					
					if (objProcessInfo.RETURN_PREMIUM!=0)
					{
						int returnResult = base.PostCancellationPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,
							objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.POLICY_VERSION_ID, objProcessInfo.RETURN_PREMIUM,objProcessInfo.ROW_ID,
							objProcessInfo.EFFECTIVE_DATETIME ,out Desc,out CarryForwardAmount,
							objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID  ,objProcessInfo.CANCELLATION_TYPE,
							objProcessInfo.CANCELLATION_OPTION );
						TransactionDescription.Append("\n " +  Desc  + ";");
					}
						//Entring the record in notice generation
					/*Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objProcessNoticeInfo = base.GetProcessNoticeInfo(objProcessInfo);
					objProcessNoticeInfo.NOTICE_DESCRIPTION = "Return Premium or Rescind be Printed for this Process";
					AddProcessNotice(objProcessNoticeInfo, objWrapper);
					TransactionDescription.Append("\n Notice Page Entry has been done;");
					*/	
				}
				else if(objProcessInfo.POLICY_PREVIOUS_STATUS.ToUpper() == POLICY_STATUS_SUSPENDED)
				{
					base.ProcessPremiumOnDecline(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,
						objProcessInfo.POLICY_VERSION_ID);
					TransactionDescription.Append("\n " +  Desc  + ";");
				
					/*
					//Entring the record in notice generation
					Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objProcessNoticeInfo = base.GetProcessNoticeInfo(objProcessInfo);
					objProcessNoticeInfo.NOTICE_DESCRIPTION = "Return Premium or Rescind be Printed for this Process";
					AddProcessNotice(objProcessNoticeInfo, objWrapper);
					TransactionDescription.Append("\n Notice Page Entry has been done;");
						*/
				}
				else
				{
					//base.RollbackTransaction();
					//ClsPolicyErrMsg.strMessage="This Process Could not be Committed as Policy Status is defferent form Active or Suspended.";
					//return false ;
					// by pravesh on 31 july as calling PostCancellationPremium for All status other then Suspended and Normal as discuss with Ravindera
					if (objProcessInfo.RETURN_PREMIUM!=0)
					{
						int returnResult = base.PostCancellationPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,
							objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.POLICY_VERSION_ID, objProcessInfo.RETURN_PREMIUM,objProcessInfo.ROW_ID,
							objProcessInfo.EFFECTIVE_DATETIME ,out Desc,out CarryForwardAmount,
							objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID  ,objProcessInfo.CANCELLATION_TYPE,
							objProcessInfo.CANCELLATION_OPTION );
						TransactionDescription.Append("\n " +  Desc  + ";");
					}
				}
				//updating the status of the Policy
				string strNewStatusDesc="",strNewStatus="";
				SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.PROCESS_ID ,out strNewStatusDesc,out strNewStatus ); 
				//updating the status of the Policy prior version to Inactive
				SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,objProcessInfo.PROCESS_ID,POLICY_STATUS_INACTIVE); 
				objProcessInfo.POLICY_CURRENT_STATUS =strNewStatus ;
				TransactionDescription.Append("Status of Policy has been updated to " + strNewStatusDesc  + ".;");

				bool retVal=base.CommitProcess(objProcessInfo,"NEWVERSION");
				if (retVal==false)
				{
					base.RollbackTransaction();
					return retVal;
				}
				//Will set up a Follow up automatically for  Alicia Hart
				AddDiary(objProcessInfo); 
			
				#region  generating PDF and Adding Print jobs
				//no Notice was found in the docment sheet
				#endregion
				
				base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, 
					GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY,TransactionDescription.ToString(),TransactionChangeXML.ToString());
				base.CommitTransaction();
				return retVal;

			}
		
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}

		public override bool RollbackProcess(ClsProcessInfo objProcessInfo)
		{
			try
			{	
				base.BeginTransaction();
				//base.RollbackProcess (objProcessInfo);
				if (base.RollbackProcess (objProcessInfo,"NEWVERSION") == true)
				{
					//Deleting the newer version
					base.DeletePolicyVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
					TransactionDescription.Append("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0") 
						+ ") of policy has been deleted.;");
					//Updating the transaction log
					base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
						GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY,TransactionDescription.ToString());
					
					base.CommitTransaction();
					return true;
				}
				else
				{
					base.RollbackTransaction();
					return false;
				}
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}
		/// <summary>
		/// adding diary entry for follow up
		/// </summary>
		/// <param name="objProcessInfo"></param>
		/// <param name="strCalledFor"></param>
		public void AddDiary(ClsProcessInfo objProcessInfo)
		{
			Cms.Model.Diary.TodolistInfo  objTodo=new  Cms.Model.Diary.TodolistInfo();
			objTodo.CUSTOMER_ID =objProcessInfo.CUSTOMER_ID;
			objTodo.POLICY_ID = objProcessInfo.POLICY_ID;
			objTodo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
			objTodo.LISTTYPEID =(int)BlCommon.ClsDiary.enumDiaryType.RESCISSION_CHECK;  
			objTodo.SUBJECTLINE = "Rescind Check";
			objTodo.NOTE		= "Rescind Check";
			//objTodo.RECDATE =System.DateTime.Now;
			objTodo.MODULE_ID=(int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;   
			objTodo.LISTOPEN ="Y";
			objTodo.FROMUSERID = objProcessInfo.CREATED_BY; 
			if (IsEODProcess)
				objTodo.CREATED_BY= EODUserID;
			else
				objTodo.CREATED_BY= objProcessInfo.CREATED_BY;
			objTodo.RECDATE = DateTime.Now;
			objTodo.LOB_ID = objProcessInfo.LOB_ID; 
			objTodo.FOLLOWUPDATE= objProcessInfo.EFFECTIVE_DATETIME;
			 
			
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary=new Cms.BusinessLayer.BlCommon.ClsDiary();
			//ArrayList alresult=new ArrayList(); 
			try
			{
				//alresult=
				objDiary.DiaryEntryfromSetup(objTodo,objWrapper);
			}
			catch(Exception ex)
			{
				throw ex;			
				
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}
		}
	}
}
