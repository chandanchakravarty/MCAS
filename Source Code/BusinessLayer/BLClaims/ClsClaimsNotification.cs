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
	/// Summary description for ClsClaimsNotification.
	/// </summary>
	public class ClsClaimsNotification : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		GetCLM_CLAIM_INFO			=	"Proc_GetCLM_CLAIM_INFO";
		private const	string		InsertCLM_CLAIM_INFO		=	"Proc_InsertCLM_CLAIM_INFO";
		private const	string		UpdateCLM_CLAIM_INFO		=	"Proc_UpdateCLM_CLAIM_INFO";
		private const	string		GetClaimsLookup				=	"Proc_GetClaimsInformationLookupUp";		
		private const	string		GetCLM_CLAIM_INFO_DESCRIPTION	=	"Proc_GetCLM_CLAIM_INFO_DESCRIPTION";
		private const	string		UpdateCLM_CLAIM_INFO_DESCRIPTION	=	"Proc_UpdateCLM_CLAIM_INFO_DESCRIPTION";
		private const	string		CheckIncompleteActivity		=	"Proc_CheckIncompleteActivity";
		private const	string		CheckReservesAdded			=	"Proc_CheckReservesAdded";
		private const	string		GetCatastropheCodes			=	"Proc_GetClaimCatastropheCode";
		private const	string		CheckActivitiesInAuthQueue	=	"Proc_ClaimActivitiesInAuthQueue";
		private const	string		GetListCLM_CLAIM_INFO		=	"Proc_GetListClaims";
		private const	string		InsertCLM_LINKED_CLAIMS		=	"Proc_InsertCLM_LINKED_CLAIMS";
		private const	string		GetCLM_LINKED_CLAIMS		=	"Proc_GetCLM_LINKED_CLAIMS";
		private const	string		GetClaimsPinkSlipLookup		=	"Proc_GetClaimsPinkSlipLookup";
        private const   string      GenerateOfficialClaim       = "Proc_GenerateOfficialClaimNumber";
		
		
		
		public ClsClaimsNotification()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Add(Insert) functions
		public int Add(ClsClaimsNotficationInfo objClaimsNotficationInfo)
		{			
			string linkedClaimNumber = "";//Done for Itrack Issue 6932 on 10 Feb 2010
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{				
				objDataWrapper.AddParameter("@CUSTOMER_ID",objClaimsNotficationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objClaimsNotficationInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objClaimsNotficationInfo.POLICY_VERSION_ID);
				//objDataWrapper.AddParameter("@CLAIM_NUMBER",objClaimsNotficationInfo.CLAIM_NUMBER);
				objDataWrapper.AddParameter("@LOSS_DATE",objClaimsNotficationInfo.LOSS_DATE);
				objDataWrapper.AddParameter("@ADJUSTER_CODE",objClaimsNotficationInfo.ADJUSTER_CODE);
				objDataWrapper.AddParameter("@ADJUSTER_ID",objClaimsNotficationInfo.ADJUSTER_ID);
				objDataWrapper.AddParameter("@REPORTED_BY",objClaimsNotficationInfo.REPORTED_BY);
				objDataWrapper.AddParameter("@CATASTROPHE_EVENT_CODE",objClaimsNotficationInfo.CATASTROPHE_EVENT_CODE);				
				//objDataWrapper.AddParameter("@CLAIMANT_INSURED",objClaimsNotficationInfo.CLAIMANT_INSURED);
				objDataWrapper.AddParameter("@INSURED_RELATIONSHIP",objClaimsNotficationInfo.INSURED_RELATIONSHIP);
				objDataWrapper.AddParameter("@CLAIMANT_NAME",objClaimsNotficationInfo.CLAIMANT_NAME);
				objDataWrapper.AddParameter("@COUNTRY",objClaimsNotficationInfo.COUNTRY);
				objDataWrapper.AddParameter("@ZIP",objClaimsNotficationInfo.ZIP);
				objDataWrapper.AddParameter("@ADDRESS1",objClaimsNotficationInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objClaimsNotficationInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objClaimsNotficationInfo.CITY);
				objDataWrapper.AddParameter("@HOME_PHONE",objClaimsNotficationInfo.HOME_PHONE);
				objDataWrapper.AddParameter("@WORK_PHONE",objClaimsNotficationInfo.WORK_PHONE);
				objDataWrapper.AddParameter("@MOBILE_PHONE",objClaimsNotficationInfo.MOBILE_PHONE);
				objDataWrapper.AddParameter("@WHERE_CONTACT",objClaimsNotficationInfo.WHERE_CONTACT);
				objDataWrapper.AddParameter("@WHEN_CONTACT",objClaimsNotficationInfo.WHEN_CONTACT);
				objDataWrapper.AddParameter("@DIARY_DATE",objClaimsNotficationInfo.DIARY_DATE);
				objDataWrapper.AddParameter("@CLAIM_STATUS",objClaimsNotficationInfo.CLAIM_STATUS);
				objDataWrapper.AddParameter("@CLAIM_STATUS_UNDER",objClaimsNotficationInfo.CLAIM_STATUS_UNDER);
				objDataWrapper.AddParameter("@OUTSTANDING_RESERVE",objClaimsNotficationInfo.OUTSTANDING_RESERVE);
				objDataWrapper.AddParameter("@RESINSURANCE_RESERVE",objClaimsNotficationInfo.RESINSURANCE_RESERVE);
				objDataWrapper.AddParameter("@PAID_LOSS",objClaimsNotficationInfo.PAID_LOSS);
				objDataWrapper.AddParameter("@PAID_EXPENSE",objClaimsNotficationInfo.PAID_EXPENSE);
				objDataWrapper.AddParameter("@RECOVERIES",objClaimsNotficationInfo.RECOVERIES);
				objDataWrapper.AddParameter("@CLAIM_DESCRIPTION",objClaimsNotficationInfo.CLAIM_DESCRIPTION);
				objDataWrapper.AddParameter("@CREATED_BY",objClaimsNotficationInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objClaimsNotficationInfo.CREATED_DATETIME);
				//objDataWrapper.AddParameter("@SUB_ADJUSTER",objClaimsNotficationInfo.SUB_ADJUSTER);
				//objDataWrapper.AddParameter("@SUB_ADJUSTER_CONTACT",objClaimsNotficationInfo.SUB_ADJUSTER_CONTACT);
				objDataWrapper.AddParameter("@EXTENSION",objClaimsNotficationInfo.EXTENSION);
				objDataWrapper.AddParameter("@LOSS_TIME_AM_PM",objClaimsNotficationInfo.LOSS_TIME_AM_PM);				
				objDataWrapper.AddParameter("@LITIGATION_FILE",objClaimsNotficationInfo.LITIGATION_FILE);								
				objDataWrapper.AddParameter("@HOMEOWNER",objClaimsNotficationInfo.HOMEOWNER);								
				objDataWrapper.AddParameter("@RECR_VEH",objClaimsNotficationInfo.RECR_VEH);								
				objDataWrapper.AddParameter("@IN_MARINE",objClaimsNotficationInfo.IN_MARINE);								
				objDataWrapper.AddParameter("@STATE",objClaimsNotficationInfo.STATE);								
				objDataWrapper.AddParameter("@CLAIMANT_PARTY",objClaimsNotficationInfo.CLAIMANT_PARTY);
                objDataWrapper.AddParameter("@CLAIMANT_TYPE", objClaimsNotficationInfo.CLAIMANT_TYPE);								
				//objDataWrapper.AddParameter("@LINKED_TO_CLAIM",objClaimsNotficationInfo.LINKED_TO_CLAIM);								
				//objDataWrapper.AddParameter("@ADD_FAULT",objClaimsNotficationInfo.ADD_FAULT);								
				//objDataWrapper.AddParameter("@TOTAL_LOSS",objClaimsNotficationInfo.TOTAL_LOSS);								
				objDataWrapper.AddParameter("@NOTIFY_REINSURER",objClaimsNotficationInfo.NOTIFY_REINSURER);								
				objDataWrapper.AddParameter("@LOB_ID",objClaimsNotficationInfo.LOB_ID);
				objDataWrapper.AddParameter("@REPORTED_TO",objClaimsNotficationInfo.REPORTED_TO);
				if(objClaimsNotficationInfo.FIRST_NOTICE_OF_LOSS!=System.DateTime.MinValue)
					objDataWrapper.AddParameter("@FIRST_NOTICE_OF_LOSS",objClaimsNotficationInfo.FIRST_NOTICE_OF_LOSS);
				else
					objDataWrapper.AddParameter("@FIRST_NOTICE_OF_LOSS",null);
				objDataWrapper.AddParameter("@LINKED_CLAIM_ID_LIST",objClaimsNotficationInfo.LINKED_CLAIM_ID_LIST);				
				objDataWrapper.AddParameter("@RECIEVE_PINK_SLIP_USERS_LIST",objClaimsNotficationInfo.RECIEVE_PINK_SLIP_USERS_LIST);
				objDataWrapper.AddParameter("@NEW_RECIEVE_PINK_SLIP_USERS_LIST",objClaimsNotficationInfo.NEW_RECIEVE_PINK_SLIP_USERS_LIST);
				objDataWrapper.AddParameter("@PINK_SLIP_TYPE_LIST",objClaimsNotficationInfo.PINK_SLIP_TYPE_LIST);
				objDataWrapper.AddParameter("@AT_FAULT_INDICATOR",objClaimsNotficationInfo.AT_FAULT_INDICATOR);//Done for Itrack Issue 6620 on 27 Nov 09

                objDataWrapper.AddParameter("@REINSURANCE_TYPE", objClaimsNotficationInfo.REINSURANCE_TYPE);
                objDataWrapper.AddParameter("@REIN_CLAIM_NUMBER", objClaimsNotficationInfo.REIN_CLAIM_NUMBER);
                objDataWrapper.AddParameter("@REIN_LOSS_NOTICE_NUM", objClaimsNotficationInfo.REIN_LOSS_NOTICE_NUM);
                objDataWrapper.AddParameter("@IS_VICTIM_CLAIM", objClaimsNotficationInfo.IS_VICTIM_CLAIM);

                if (objClaimsNotficationInfo.LAST_DOC_RECEIVE_DATE != System.DateTime.MinValue)
                    objDataWrapper.AddParameter("@LAST_DOC_RECEIVE_DATE", objClaimsNotficationInfo.LAST_DOC_RECEIVE_DATE);
                else
                    objDataWrapper.AddParameter("@LAST_DOC_RECEIVE_DATE", DBNull.Value);

                if (objClaimsNotficationInfo.POSSIBLE_PAYMENT_DATE != System.DateTime.MinValue)
                    objDataWrapper.AddParameter("@POSSIBLE_PAYMENT_DATE", objClaimsNotficationInfo.POSSIBLE_PAYMENT_DATE);
                else
                    objDataWrapper.AddParameter("@POSSIBLE_PAYMENT_DATE", DBNull.Value);


				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CLAIM_ID",objClaimsNotficationInfo.CLAIM_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlReturn  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				//Done for Itrack Issue 6932 on 10 Feb 2010 --> Done to show LINKED_CLAIM_ID_LIST in Transaction Log 
				returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_CLAIM_INFO);
				
				if(objClaimsNotficationInfo.LINKED_CLAIM_ID_LIST != null && objClaimsNotficationInfo.LINKED_CLAIM_ID_LIST !="")
				{
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(objSqlParameter.Value.ToString()));
					DataSet ds = objDataWrapper.ExecuteDataSet(GetCLM_LINKED_CLAIMS);
					for(int i=0; i< ds.Tables[0].Rows.Count;i++)
					{
						linkedClaimNumber += ds.Tables[0].Rows[i]["CLAIM_NUMBER"].ToString() + ',';
					}
					objClaimsNotficationInfo.LINKED_CLAIM_ID_LIST = linkedClaimNumber.Substring(0,linkedClaimNumber.Length-1);
				}
				//Added till here
				if(TransactionLogRequired) 
				{
					objClaimsNotficationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddClaimsNotification.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objClaimsNotficationInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objClaimsNotficationInfo.CREATED_BY;
					objTransactionInfo.CLIENT_ID		=	objClaimsNotficationInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID		=	objClaimsNotficationInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID=objClaimsNotficationInfo.POLICY_VERSION_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1833", "");// "Claims Notification is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Done for Itrack Issue 6932 on 10 Feb 2010 --> Done to show LINKED_CLAIM_ID_LIST in Transaction Log 
					//returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_CLAIM_INFO,objTransactionInfo);
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//Done for Itrack Issue 6932 on 10 Feb 2010 --> Done to show LINKED_CLAIM_ID_LIST in Transaction Log 
//				else
//					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_CLAIM_INFO);
				

				int CLAIM_ID = 0;
				if (returnResult>0)
				{
					CLAIM_ID = int.Parse(objSqlParameter.Value.ToString());					
					objClaimsNotficationInfo.CLAIM_ID = CLAIM_ID;
					AddClaimsDiaryEntry(objClaimsNotficationInfo,objDataWrapper);
					//AddLinkedClaims(objClaimsNotficationInfo.CLAIM_ID,objClaimsNotficationInfo.LINKED_CLAIM_ID);
				}
				else if(objSqlReturn.Value!=null && objSqlReturn.Value.ToString()!="")
				{
					returnResult = int.Parse(objSqlReturn.Value.ToString());
				}

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return returnResult;
				
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		
		#endregion

		private void AddClaimsDiaryEntry(ClsClaimsNotficationInfo objClaimsNotficationInfo, DataWrapper objDataWrapper)
		{
			Cms.Model.Diary.TodolistInfo objTodolistInfo = new Cms.Model.Diary.TodolistInfo();
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
			objTodolistInfo.FROMUSERID			=   objClaimsNotficationInfo.CREATED_BY;			
			objTodolistInfo.LISTTYPEID			=   (int)ClsDiary.enumDiaryType.CLAIM_REVIEW;
			/*Commented by Asfa Praveen (05-Feb-2008) - iTrack issue #3526 Part 5
			objTodolistInfo.NOTE = objTodolistInfo.SUBJECTLINE        =   "New Claim Added";						
			*/
			objTodolistInfo.NOTE				=	"";
            objTodolistInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1834", "");// "New Claim Added";						
			objTodolistInfo.LISTOPEN			=   "Y";
			objTodolistInfo.CUSTOMER_ID			=	objClaimsNotficationInfo.CUSTOMER_ID;									
			objTodolistInfo.POLICY_ID			=	objClaimsNotficationInfo.POLICY_ID;
			objTodolistInfo.POLICY_VERSION_ID	=	objClaimsNotficationInfo.POLICY_VERSION_ID;						
			objTodolistInfo.CREATED_BY = objTodolistInfo.MODIFIED_BY = objClaimsNotficationInfo.CREATED_BY;
			objTodolistInfo.CREATED_DATETIME = objTodolistInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;						
			objTodolistInfo.LOB_ID = int.Parse(objClaimsNotficationInfo.LOB_ID);
			objTodolistInfo.CLAIMID				=	objClaimsNotficationInfo.CLAIM_ID;			
			objTodolistInfo.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.CLAIM;
			objDiary.DiaryEntryfromSetup(objTodolistInfo,objDataWrapper);
		}

		private void AddLinkedClaims(int CLAIM_ID, string LINKED_CLAIM_ID_LIST)
		{
			//			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			//			objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			//			objWrapper.AddParameter("@LINKED_CLAIM_ID_LIST",LINKED_CLAIM_ID_LIST);
			//			objWrapper.ExecuteNonQuery(InsertCLM_LINKED_CLAIMS);						
		}
		

		#region Update Claims Notification method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldLocationInfo">Model object having old information</param>
		/// <param name="objLocationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsClaimsNotficationInfo objOldClaimsNotficationInfo, ClsClaimsNotficationInfo objClaimsNotficationInfo)
		{
			int returnResult = 0;	
			string linkedClaimNumber = "";//Done for Itrack Issue 6932 on 10 Feb 2010
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objClaimsNotficationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objClaimsNotficationInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objClaimsNotficationInfo.POLICY_VERSION_ID);				
				objDataWrapper.AddParameter("@CLAIM_ID",objClaimsNotficationInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@LOSS_DATE",objClaimsNotficationInfo.LOSS_DATE);
				objDataWrapper.AddParameter("@ADJUSTER_CODE",objClaimsNotficationInfo.ADJUSTER_CODE);
				objDataWrapper.AddParameter("@ADJUSTER_ID",objClaimsNotficationInfo.ADJUSTER_ID);
				objDataWrapper.AddParameter("@REPORTED_BY",objClaimsNotficationInfo.REPORTED_BY);
				objDataWrapper.AddParameter("@CATASTROPHE_EVENT_CODE",objClaimsNotficationInfo.CATASTROPHE_EVENT_CODE);				
				//objDataWrapper.AddParameter("@CLAIMANT_INSURED",objClaimsNotficationInfo.CLAIMANT_INSURED);
				objDataWrapper.AddParameter("@INSURED_RELATIONSHIP",objClaimsNotficationInfo.INSURED_RELATIONSHIP);
				objDataWrapper.AddParameter("@CLAIMANT_NAME",objClaimsNotficationInfo.CLAIMANT_NAME);
				objDataWrapper.AddParameter("@COUNTRY",objClaimsNotficationInfo.COUNTRY);
				objDataWrapper.AddParameter("@ZIP",objClaimsNotficationInfo.ZIP);
				objDataWrapper.AddParameter("@ADDRESS1",objClaimsNotficationInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objClaimsNotficationInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objClaimsNotficationInfo.CITY);
				objDataWrapper.AddParameter("@HOME_PHONE",objClaimsNotficationInfo.HOME_PHONE);
				objDataWrapper.AddParameter("@WORK_PHONE",objClaimsNotficationInfo.WORK_PHONE);
				objDataWrapper.AddParameter("@MOBILE_PHONE",objClaimsNotficationInfo.MOBILE_PHONE);
				objDataWrapper.AddParameter("@WHERE_CONTACT",objClaimsNotficationInfo.WHERE_CONTACT);
				objDataWrapper.AddParameter("@WHEN_CONTACT",objClaimsNotficationInfo.WHEN_CONTACT);
				objDataWrapper.AddParameter("@DIARY_DATE",objClaimsNotficationInfo.DIARY_DATE);
				objDataWrapper.AddParameter("@CLAIM_STATUS",objClaimsNotficationInfo.CLAIM_STATUS);
				objDataWrapper.AddParameter("@CLAIM_STATUS_UNDER",objClaimsNotficationInfo.CLAIM_STATUS_UNDER);
				objDataWrapper.AddParameter("@OUTSTANDING_RESERVE",objOldClaimsNotficationInfo.OUTSTANDING_RESERVE);
				objDataWrapper.AddParameter("@RESINSURANCE_RESERVE",objOldClaimsNotficationInfo.RESINSURANCE_RESERVE);
				objDataWrapper.AddParameter("@PAID_LOSS",objOldClaimsNotficationInfo.PAID_LOSS);
				objDataWrapper.AddParameter("@PAID_EXPENSE",objOldClaimsNotficationInfo.PAID_EXPENSE);
				objDataWrapper.AddParameter("@RECOVERIES",objOldClaimsNotficationInfo.RECOVERIES);
				objDataWrapper.AddParameter("@CLAIM_DESCRIPTION",objClaimsNotficationInfo.CLAIM_DESCRIPTION);				
				objDataWrapper.AddParameter("@MODIFIED_BY",objClaimsNotficationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objClaimsNotficationInfo.LAST_UPDATED_DATETIME);
				//objDataWrapper.AddParameter("@SUB_ADJUSTER",objClaimsNotficationInfo.SUB_ADJUSTER);
				//objDataWrapper.AddParameter("@SUB_ADJUSTER_CONTACT",objClaimsNotficationInfo.SUB_ADJUSTER_CONTACT);
				objDataWrapper.AddParameter("@EXTENSION",objClaimsNotficationInfo.EXTENSION);
				objDataWrapper.AddParameter("@LOSS_TIME_AM_PM",objClaimsNotficationInfo.LOSS_TIME_AM_PM);				
				objDataWrapper.AddParameter("@LITIGATION_FILE",objClaimsNotficationInfo.LITIGATION_FILE);
				objDataWrapper.AddParameter("@STATE",objClaimsNotficationInfo.STATE);								
				objDataWrapper.AddParameter("@CLAIMANT_PARTY",objClaimsNotficationInfo.CLAIMANT_PARTY);												
				//				objDataWrapper.AddParameter("@LINKED_TO_CLAIM",objClaimsNotficationInfo.LINKED_TO_CLAIM);								
				//objDataWrapper.AddParameter("@ADD_FAULT",objClaimsNotficationInfo.ADD_FAULT);								
				//objDataWrapper.AddParameter("@TOTAL_LOSS",objClaimsNotficationInfo.TOTAL_LOSS);								
				objDataWrapper.AddParameter("@NOTIFY_REINSURER",objClaimsNotficationInfo.NOTIFY_REINSURER);
				objDataWrapper.AddParameter("@REPORTED_TO",objClaimsNotficationInfo.REPORTED_TO);
				if(objClaimsNotficationInfo.FIRST_NOTICE_OF_LOSS!=System.DateTime.MinValue)
					objDataWrapper.AddParameter("@FIRST_NOTICE_OF_LOSS",objClaimsNotficationInfo.FIRST_NOTICE_OF_LOSS);
				else
					objDataWrapper.AddParameter("@FIRST_NOTICE_OF_LOSS",null);

                if (objClaimsNotficationInfo.LAST_DOC_RECEIVE_DATE != System.DateTime.MinValue)
                    objDataWrapper.AddParameter("@LAST_DOC_RECEIVE_DATE", objClaimsNotficationInfo.LAST_DOC_RECEIVE_DATE);
                else
                    objDataWrapper.AddParameter("@LAST_DOC_RECEIVE_DATE", null);

				objDataWrapper.AddParameter("@LINKED_CLAIM_ID_LIST",objClaimsNotficationInfo.LINKED_CLAIM_ID_LIST);
				objDataWrapper.AddParameter("@RECIEVE_PINK_SLIP_USERS_LIST",objClaimsNotficationInfo.RECIEVE_PINK_SLIP_USERS_LIST);
				objDataWrapper.AddParameter("@NEW_RECIEVE_PINK_SLIP_USERS_LIST",objClaimsNotficationInfo.NEW_RECIEVE_PINK_SLIP_USERS_LIST);
				objDataWrapper.AddParameter("@PINK_SLIP_TYPE_LIST",objClaimsNotficationInfo.PINK_SLIP_TYPE_LIST);
				objDataWrapper.AddParameter("@AT_FAULT_INDICATOR",objClaimsNotficationInfo.AT_FAULT_INDICATOR);//Done for Itrack Issue 6620 on 27 Nov 09
                
                objDataWrapper.AddParameter("@REINSURANCE_TYPE", objClaimsNotficationInfo.REINSURANCE_TYPE);
                objDataWrapper.AddParameter("@REIN_CLAIM_NUMBER", objClaimsNotficationInfo.REIN_CLAIM_NUMBER);
                objDataWrapper.AddParameter("@REIN_LOSS_NOTICE_NUM", objClaimsNotficationInfo.REIN_LOSS_NOTICE_NUM);
                objDataWrapper.AddParameter("@IS_VICTIM_CLAIM", objClaimsNotficationInfo.IS_VICTIM_CLAIM);

                if (objClaimsNotficationInfo.POSSIBLE_PAYMENT_DATE != System.DateTime.MinValue)
                    objDataWrapper.AddParameter("@POSSIBLE_PAYMENT_DATE", objClaimsNotficationInfo.POSSIBLE_PAYMENT_DATE);
                else
                    objDataWrapper.AddParameter("@POSSIBLE_PAYMENT_DATE", DBNull.Value);

				SqlParameter objSqlReturn  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);
				//Done for Itrack Issue 6932 on 10 Feb 2010 --> Done to show LINKED_CLAIM_ID_LIST in Transaction Log 
				returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_CLAIM_INFO);

				//Done for Itrack Issue 7073 on 19 Feb 2010
				if(objClaimsNotficationInfo.LINKED_CLAIM_ID_LIST != null && objClaimsNotficationInfo.LINKED_CLAIM_ID_LIST !="")
				{
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CLAIM_ID",objClaimsNotficationInfo.CLAIM_ID);
					DataSet ds = objDataWrapper.ExecuteDataSet(GetCLM_LINKED_CLAIMS);
					for(int i=0; i< ds.Tables[0].Rows.Count;i++)
					{
						linkedClaimNumber += ds.Tables[0].Rows[i]["CLAIM_NUMBER"].ToString() + ',';
					}
					
					objClaimsNotficationInfo.LINKED_CLAIM_ID_LIST = linkedClaimNumber.Substring(0,linkedClaimNumber.Length-1);
				}
				//Added till here
				if(TransactionLogRequired) 
				{
					objClaimsNotficationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddClaimsNotification.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldClaimsNotficationInfo,objClaimsNotficationInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	objClaimsNotficationInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID		=	objClaimsNotficationInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID=objClaimsNotficationInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objClaimsNotficationInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1810","")+" " + objClaimsNotficationInfo.CLAIM_NUMBER + " "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1835",""); //"Claim Number " + objClaimsNotficationInfo.CLAIM_NUMBER + " is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Done for Itrack Issue 6932 on 10 Feb 2010 --> Done to show LINKED_CLAIM_ID_LIST in Transaction Log 
					//returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_CLAIM_INFO,objTransactionInfo);
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//Done for Itrack Issue 6932 on 10 Feb 2010 --> Done to show LINKED_CLAIM_ID_LIST in Transaction Log 
//				else
//					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_CLAIM_INFO);
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				//AddLinkedClaims(objClaimsNotficationInfo.CLAIM_ID,objClaimsNotficationInfo.LINKED_CLAIM_ID);
				if(returnResult<=0 && objSqlReturn.Value!=null && objSqlReturn.Value.ToString()!="")
				{
					returnResult = int.Parse(objSqlReturn.Value.ToString());
				}

				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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

		#region Get Claims Notification Old Data
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataSet GetClaimsNotification(int CLAIM_ID)													  
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			objWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			DataSet ds = objWrapper.ExecuteDataSet(GetCLM_CLAIM_INFO);			
			if(ds!=null && ds.Tables.Count>0)
				return ds;
			else
				return null;
		}
		
		#endregion		

		#region Get Claims ID and Other Details from Claim Number
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CLAIM_NUMBER"></param>
		/// <returns></returns>
		public DataSet GetClaimsDetailsFromNumber(string CLAIM_NUMBER)													  
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			objWrapper.AddParameter("@CLAIM_NUMBER",CLAIM_NUMBER);
			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetCliamDetails");			
			if(ds!=null && ds.Tables.Count>0)
				return ds;
			else
				return null;
		}

		#endregion		

		#region Get Lookup Tables Data for Claims Notification
		public static DataSet GetClaimsLookupData(string strCustomerID,string strLOSS_DATE, string strPolicyID, string strPolicyVersionID,string strClaimId, int LobID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			objWrapper.AddParameter("@CUSTOMER_ID",strCustomerID);
			if(strLOSS_DATE=="")
				objWrapper.AddParameter("@LOSS_DATE",strLOSS_DATE);
			else
				objWrapper.AddParameter("@LOSS_DATE",System.DBNull.Value);
			objWrapper.AddParameter("@POLICY_ID",strPolicyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",strPolicyVersionID);
            objWrapper.AddParameter("@LOB_ID", LobID);		
			DataSet ds = objWrapper.ExecuteDataSet(GetClaimsLookup);			
			if(ds!=null && ds.Tables.Count>0)
				return ds;
			else
				return null;
		}
		public DataSet GetPinkSlips(string strClaimId)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
			objWrapper.AddParameter("@CLAIM_ID",strClaimId);
			DataSet ds = objWrapper.ExecuteDataSet(GetClaimsPinkSlipLookup);			
			if(ds!=null && ds.Tables.Count>0)
				return ds;
			else
				return null;
		}
		#endregion

		#region Check for date of loss entered against Policy Effective and Expiration Date
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataSet CheckLossDateAgainstPolicy(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, DateTime LOSS_DATE)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
				objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
				objWrapper.AddParameter("@POLICY_ID",POLICY_ID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);
				objWrapper.AddParameter("@LOSS_DATE",LOSS_DATE);
				SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
				DataSet ds = objWrapper.ExecuteDataSet("Proc_CheckLossDateAgainstPolicy");			
				objWrapper.ClearParameteres();
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return ds;
			}
			catch(Exception ex)
			{
				throw(ex);				
			}
			finally{}
			
		}
		#endregion

        public string GenerateOfficialClaimNumber(string strCustomerID, string strPolicyID, string strPolicyVersionID, DateTime LossDate,DateTime FNOLDate, string strClaimId, int AdusterID, int LangID)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", strCustomerID);
            objWrapper.AddParameter("@POLICY_ID", strPolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", strPolicyVersionID);
            objWrapper.AddParameter("@CLAIM_ID", strClaimId);
            objWrapper.AddParameter("@LOSS_DATE", LossDate);

            if(FNOLDate==DateTime.MinValue)
                objWrapper.AddParameter("@FNOL_DATE", DBNull.Value);
            else
                objWrapper.AddParameter("@FNOL_DATE", FNOLDate);

            objWrapper.AddParameter("@ADJUSTER_ID", AdusterID);
            objWrapper.AddParameter("@LANG_ID", LangID);

            DataSet ds = objWrapper.ExecuteDataSet(GenerateOfficialClaim);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return null;
        }


		#region Get Claims Description for Claim Payment Screen
		public static string GetClaimDescription(string strCLAIM_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_CLAIM_INFO_DESCRIPTION);
			if(objDataSet!=null && objDataSet.Tables.Count>0 && objDataSet.Tables[0].Rows.Count>0 && objDataSet.Tables[0].Rows[0]["CLAIM_DESCRIPTION"]!=null && objDataSet.Tables[0].Rows[0]["CLAIM_DESCRIPTION"].ToString()!="")
				return objDataSet.Tables[0].Rows[0]["CLAIM_DESCRIPTION"].ToString();
			else
				return "";
		}
		#endregion

		#region Update Claims Description from Claims Payment Screen
		public static int UpdateClaimDescription(string strCLAIM_ID,string strDescription)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			objDataWrapper.AddParameter("@CLAIM_DESCRIPTION",strDescription);			
			int intResult	= objDataWrapper.ExecuteNonQuery(UpdateCLM_CLAIM_INFO_DESCRIPTION);
			return intResult;
		}
		#endregion

		#region Check for Existence of any incomplete Activity for the claim
		public static string CheckForIncompleteActivity(string strCLAIM_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",0,SqlDbType.Int,ParameterDirection.ReturnValue);
			objDataWrapper.ExecuteNonQuery(CheckIncompleteActivity);
			string ActivityID = objSqlParameter.Value.ToString();
			return ActivityID;
		}
		#endregion

		#region Get Currently Logged In User Name
		public static string GetCurrentUserName(string UserID)
		{
			string userName = "";
			DataSet dsTemp = ClsUser.GetUserName(UserID);
			if (dsTemp.Tables[0].Rows.Count > 0)
				userName = dsTemp.Tables[0].Rows[0][0].ToString();
			else
				userName = "";

			return userName; 
		}
		#endregion

		#region Check whether reserves for current claim have been added or not
		public static string CheckForReservesAdded(string strCLAIM_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",0,SqlDbType.Int,ParameterDirection.ReturnValue);
			objDataWrapper.ExecuteNonQuery(CheckReservesAdded);
			string ActivityID = objSqlParameter.Value.ToString();
			return ActivityID;
		}
		#endregion

		#region Check whether any activities are awaiting authorisation of current user
		public static string CheckForActivitiesInQueue(string strCLAIM_ID, string strUSER_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);			
			objDataWrapper.AddParameter("@USER_ID",strUSER_ID);
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",0,SqlDbType.Int,ParameterDirection.ReturnValue);
			objDataWrapper.ExecuteNonQuery(CheckActivitiesInAuthQueue);
			string ActivityID = objSqlParameter.Value.ToString();
			return ActivityID;
		}
		#endregion	

		#region Get Catastrophe Event Codes
		public static DataTable GetCatastropheEventCodes(int LOB_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
          
            objWrapper.AddParameter("@LOB_ID", LOB_ID);
			
			DataSet ds = objWrapper.ExecuteDataSet(GetCatastropheCodes);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		#endregion


		#region Get List of Claims 
		public DataTable GetClaimsList(int ExistingClaim_ID, string searchCriteria, string searchText)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			
			objWrapper.AddParameter("@CLAIM_ID",ExistingClaim_ID);
			objWrapper.AddParameter("@SEARCHOPTION",searchCriteria);
			objWrapper.AddParameter("@SEARCHTEXT",searchText);
            objWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
			DataSet ds = objWrapper.ExecuteDataSet(GetListCLM_CLAIM_INFO);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		#endregion


		#region Temporary function for fetching scheduled item records
		public  DataTable GetSchRecords(string strClaim_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);										
			objWrapper.AddParameter("@CLAIM_ID",strClaim_ID);
			DataSet ds = objWrapper.ExecuteDataSet("getSchItemCovgForClaims");			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		public  DataTable GetOldSchRecords(string strClaim_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);										
			objWrapper.AddParameter("@CLAIM_ID",strClaim_ID);
			DataSet ds = objWrapper.ExecuteDataSet("getOldSchItemCovgForClaims");			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		#endregion
		

		public DataSet GetPaidLoss(int ClaimID,out int ReturnValue)
		{
			try
			{
				DataSet dsTemp = new DataSet();
				ReturnValue = 1;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CLAIM_ID",ClaimID,SqlDbType.Int);
				SqlParameter objSqlReturn  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_PaidLossClaimsByCoverage");
				if(objSqlReturn!=null && objSqlReturn.Value.ToString()!="")
					ReturnValue = int.Parse(objSqlReturn.Value.ToString());
				return dsTemp;
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		public int UpdateStatus(int ClaimID, int Status)
		{
			int returnResult = 0;	
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",ClaimID);
				objDataWrapper.AddParameter("@CLAIM_STATUS",Status);
				returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdateStatusCLM_CLAIM_INFO");
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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

	}
}
