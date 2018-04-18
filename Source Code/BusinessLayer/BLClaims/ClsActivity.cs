/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	5/24/2006 5:11:10 PM
<End Date			: -	
<Description		: - 	
<Review Date		: - 
<Reviewed By		: - 	
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
using Cms.Model.Claims;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BLClaims
{
/// <summary>
/// 
/// </summary>
	public class ClsActivity : Cms.BusinessLayer.BLClaims.ClsClaims
	{

        public string IS_VOID_ACTIVITY = "N";

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsActivity()
		{}
		#endregion
		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objActivityInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsActivityInfo objActivityInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
                int returnValue = this.Add(objActivityInfo, objDataWrapper);
				if(returnValue>0 )
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


        
		public int Add(ClsActivityInfo objActivityInfo,DataWrapper objDataWrapper)
		{
           
			string		strStoredProc	=	"Proc_InsertCLM_ACTIVITY";
			DateTime	RecordDate		=	DateTime.Now;			
				
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{
				objDataWrapper.ClearParameteres();
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				
				objDataWrapper.AddParameter("@ACTIVITY_REASON",objActivityInfo.ACTIVITY_REASON);
				objDataWrapper.AddParameter("@ACTIVITY_STATUS",objActivityInfo.ACTIVITY_STATUS);
				objDataWrapper.AddParameter("@REASON_DESCRIPTION",objActivityInfo.REASON_DESCRIPTION);
				objDataWrapper.AddParameter("@CREATED_BY",objActivityInfo.CREATED_BY);
				objDataWrapper.AddParameter("@RESERVE_TRAN_CODE",objActivityInfo.RESERVE_TRAN_CODE);
				objDataWrapper.AddParameter("@ACTION_ON_PAYMENT",objActivityInfo.ACTION_ON_PAYMENT);
                objDataWrapper.AddParameter("@IS_VOID_ACTIVITY", IS_VOID_ACTIVITY);
                objDataWrapper.AddParameter("@COI_TRAN_TYPE", objActivityInfo.COI_TRAN_TYPE);
				objDataWrapper.AddParameter("@ACCOUNTING_SUPPRESSED",objActivityInfo.ACCOUNTING_SUPPRESSED,SqlDbType.Bit);
                objDataWrapper.AddParameter("@TEXT_ID", objActivityInfo.TEXT_ID);
                objDataWrapper.AddParameter("@TEXT_DESCRIPTION", objActivityInfo.TEXT_DESCRIPTION);
            
                
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objActivityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddActivity.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objActivityInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	objActivityInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID		=	objActivityInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objActivityInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objActivityInfo.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1811", "");//"New Activity is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1810","")+" : " + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 15 Jan 2010
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				
                //if (int.Parse(objSqlParameter.Value.ToString()) > 0)
                //{
					objActivityInfo.ACTIVITY_ID = int.Parse(objSqlParameter.Value.ToString());
				//}
             
				objDataWrapper.ClearParameteres();
				//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return returnResult;
					
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
//				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion

		public void PerformActngEntryForNewReserve(ClsActivityInfo objActivityInfo,DataWrapper objDataWrapper)
		{
			//objDataWrapper.ClearParameteres();
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//RPSINGH - 7 APRIL 2007 - Adding acct entries in case of New Reserve creation
			//11836 -> new reserve and 11801 -> complete
//			if ((objActivityInfo.ACTIVITY_REASON == 11836)&& (objActivityInfo.ACTIVITY_STATUS == 11801))
//			{
				//GET DR AND CR ACCT FOR ACTIVITY

				objDataWrapper.CommandType=CommandType.Text;
				DataSet DS=objDataWrapper.ExecuteDataSet("EXEC PROC_GET_CLM_DR_CR_ACCT_FOR_ACTIVITY " + objActivityInfo.CLAIM_ID + ", " + objActivityInfo.ACTIVITY_ID);

				string strDrAcct="";
				string strCrAcct="";

				if (DS!=null &&  DS.Tables.Count>0 && DS.Tables[0]!=null && DS.Tables[0].Rows.Count>0)
				{
					strDrAcct=DS.Tables[0].Rows[0][1].ToString();
					strCrAcct=DS.Tables[0].Rows[0][2].ToString();
				}

				PerformGLEntry(objDataWrapper,objActivityInfo.CLAIM_ID,objActivityInfo.ACTIVITY_ID,objActivityInfo.CREATED_BY,strDrAcct,strCrAcct);
				/*objDataWrapper.CommandType=CommandType.StoredProcedure;
				//Commit Acct data -> PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY

				# region Posting for DEBIT entry					
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID);

				objDataWrapper.AddParameter("@USER_ID",int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()));
				
				objDataWrapper.AddParameter("@DIV_ID",ConfigurationSettings.AppSettings.Get("DIV_ID").ToString());
				objDataWrapper.AddParameter("@DEPT_ID",ConfigurationSettings.AppSettings.Get("DEPT_ID").ToString());
				objDataWrapper.AddParameter("@PC_ID",ConfigurationSettings.AppSettings.Get("PC_ID").ToString());
				
				objDataWrapper.AddParameter("@ACCOUNT_ID",strDrAcct);
				//Incase of DR pass '1' and in case of CR pass '-1'
				objDataWrapper.AddParameter("@TRANSACTION_AMOUNT","1");
				
				objDataWrapper.ExecuteNonQuery("PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY");
				
				#endregion


				# region Posting for CREDIT entry
				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID);

				objDataWrapper.AddParameter("@USER_ID",int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()));
				
				objDataWrapper.AddParameter("@DIV_ID",ConfigurationSettings.AppSettings.Get("DIV_ID").ToString());
				objDataWrapper.AddParameter("@DEPT_ID",ConfigurationSettings.AppSettings.Get("DEPT_ID").ToString());
				objDataWrapper.AddParameter("@PC_ID",ConfigurationSettings.AppSettings.Get("PC_ID").ToString());
				
				objDataWrapper.AddParameter("@ACCOUNT_ID",strCrAcct);
				//Incase of DR pass '1' and in case of CR pass '-1'
				objDataWrapper.AddParameter("@TRANSACTION_AMOUNT","-1");
				
				objDataWrapper.ExecuteNonQuery("PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY");
				
				#endregion*/
//			}
		}

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldActivityInfo">Model object having old information</param>
		/// <param name="objActivityInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsActivityInfo objOldActivityInfo,ClsActivityInfo objActivityInfo)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_ACTIVITY";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();

				objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID);
				objDataWrapper.AddParameter("@REASON_DESCRIPTION",objActivityInfo.REASON_DESCRIPTION);
				objDataWrapper.AddParameter("@MODIFIED_BY",objActivityInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@RESERVE_TRAN_CODE",objActivityInfo.RESERVE_TRAN_CODE);
				objDataWrapper.AddParameter("@ACTION_ON_PAYMENT",objActivityInfo.ACTION_ON_PAYMENT);
				objDataWrapper.AddParameter("@ACCOUNTING_SUPPRESSED",objActivityInfo.ACCOUNTING_SUPPRESSED,SqlDbType.Bit);
                objDataWrapper.AddParameter("@COI_TRAN_TYPE", objActivityInfo.COI_TRAN_TYPE);
                objDataWrapper.AddParameter("@TEXT_ID", objActivityInfo.TEXT_ID);
                objDataWrapper.AddParameter("@TEXT_DESCRIPTION", objActivityInfo.TEXT_DESCRIPTION);
                
				if(TransactionLogRequired) 
				{
					objActivityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddActivity.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldActivityInfo,objActivityInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	objActivityInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID		=	objActivityInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objActivityInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objActivityInfo.MODIFIED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1809", "");//"Activity is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1810","")+" : " + claimNumber;//"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 15 Jan 2010
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

        //shubhanshu
        public DataSet GetDescription()
        {
            string strSql = "Proc_PaymentClaim";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }






		#region "GetxmlMethods"
		public DataSet GetValuesForPageControls(string CLAIM_ID, string ACTIVITY_ID)
		{
			string strSql = "Proc_GetXMLProcCLM_ACTIVITY";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@ACTIVITY_ID",int.Parse(ACTIVITY_ID));
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

        public DataSet GetClaimActivityList(string CLAIM_NUMBER, int LANG_ID)
        {
            string strSql = "Proc_GetClaimActivityList";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CLAIM_NUMBER", CLAIM_NUMBER);
            objDataWrapper.AddParameter("@LANG_ID", LANG_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }

		public static DataSet GetCalimStatus(string strCLAIM_ID)
		{
			string strSql = "Proc_GetCLM_STATUS";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}

		public static DataSet GetCalimDiaryLimits(int varACTIVITY_REASON)
		{
			string strSql = "Proc_GetCLM_DIARY_LIMIT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ACTIVITY_REASON",varACTIVITY_REASON);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}

		public string GetXmlForPageControls(string CLAIM_ID, string ACTIVITY_ID)
		{
			DataSet objDataSet = GetValuesForPageControls(CLAIM_ID,ACTIVITY_ID);
			return objDataSet.GetXml();
		}

		public static bool AnyReserveUpdateAdded(string strCLAIM_ID)
		{
			string strSql = "Proc_ReserveUpdateAddedCLM_ACTIVITY";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);		
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ACTIVITY_ID",0,SqlDbType.Int,ParameterDirection.ReturnValue);
			objDataWrapper.ExecuteNonQuery(strSql);
			int returnValue = int.Parse(objSqlParameter.Value.ToString());
			if(returnValue>0)
				return true;
			else
				return false;		

		}
		public int UpdateActivityDescription(string strCLAIM_ID, string strACTIVITY_ID, string strDESCRIPTION)
		{
			string strSql = "Proc_UpdateClaimActivityDescription";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);		
			objDataWrapper.AddParameter("@ACTIVITY_ID",strACTIVITY_ID);					
			objDataWrapper.AddParameter("@DESCRIPTION",strDESCRIPTION);					
			int returnValue = objDataWrapper.ExecuteNonQuery(strSql);			
			return returnValue;
		}

		public string GetUserName(int userID)
		{
			string strProcedure = "Proc_GetUser";
			string strUserName = "";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@UserId",userID);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);

				if (objDataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dr = objDataSet.Tables[0].Rows[0];
				
					if (dr["USER_TITLE"] != DBNull.Value) 
						strUserName = dr["USER_TITLE"].ToString() + " ";
				
					if (dr["USER_FNAME"] != DBNull.Value) 
						strUserName += dr["USER_FNAME"].ToString() + " ";
					else
						strUserName += " ";

					if (dr["USER_LNAME"] != DBNull.Value) 
						strUserName += dr["USER_LNAME"].ToString();
				
				}

				return strUserName; 
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

	
		#endregion

		#region "CompleteActivity"
		//public int CompleteActivity(string strCLAIM_ID, string strACTIVITY_ID,string strADJUSTER_ID,string strUSER_ID,string strWolverineUser1, string strWolverineUser2)
        public int CompleteActivity(ClsActivityInfo objActivityInfo, int ActivityReason, int ActionOnPayment, int CompletedBy, out int Error, int LangID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
                int returnValue = this.CompleteActivity(objActivityInfo, ActivityReason, ActionOnPayment, CompletedBy, out Error, objDataWrapper, LangID);
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

        // THIS IS TEMPORARY FUNCTION CREATED BY SANTOSH GAUTAM
        public int CompleteActivity(ClsActivityInfo objActivityInfo, int ActivityReason, int ActionOnPayment, int CompletedBy, out int Error, DataWrapper objDataWrapper, int LangID)
        {
             Error=0;
             int RetVal = 0;
            string strSql = "Proc_CompleteClaimActivities";
            
            objDataWrapper.ClearParameteres();
            DataSet ds = new DataSet();
            objDataWrapper.AddParameter("@CLAIM_ID", objActivityInfo.CLAIM_ID);
           // ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
           // string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();

            SqlParameter returnSqlParameter;
            returnSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETNVALUE", SqlDbType.Int, ParameterDirection.ReturnValue);

            objDataWrapper.AddParameter("@ACTIVITY_ID", objActivityInfo.ACTIVITY_ID);
            objDataWrapper.AddParameter("@ACTIVITY_REASON", ActivityReason);
            objDataWrapper.AddParameter("@ACTION_ON_PAYMENT", ActionOnPayment);

            objDataWrapper.AddParameter("@COMPETED_BY", CompletedBy);
            objDataWrapper.AddParameter("@TEXT_ID", objActivityInfo.TEXT_ID);
            objDataWrapper.AddParameter("@TEXT_DESCRIPTION", objActivityInfo.TEXT_DESCRIPTION);
            objDataWrapper.AddParameter("@REASON_DESCRIPTION", objActivityInfo.REASON_DESCRIPTION);
            objDataWrapper.AddParameter("@LANG_ID", LangID);
             RetVal= objDataWrapper.ExecuteNonQuery(strSql);

             Error = (int)returnSqlParameter.Value;

            return RetVal;
           
           
        }
        // ACTUAL FUNCTION NAME IS CompleteActivity
		public int CompleteActivity1(ClsActivityInfo objActivityInfo,string strADJUSTER_ID,DataWrapper objDataWrapper)
		{

            //Added by Santosh Gautam on 3 Dec 2010
			// old value :string strSql = "Proc_CompleteClaimActivity";

           string strSql = "Proc_CompleteClaimActivities";
			int returnVal=0;
			try
			{
				//RPSINGH - CLAIMS ACCT INTEGRATION
				//GET DR AND CR ACCT FOR ACTIVITY
				/*DataWrapper objDataWrap = new DataWrapper(ConnStr,CommandType.Text);
				DataSet DS=objDataWrap.ExecuteDataSet("EXEC PROC_GET_CLM_DR_CR_ACCT_FOR_ACTIVITY " + objActivityInfo.CLAIM_ID + ", " + objActivityInfo.ACTIVITY_ID);*/
				//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.ClearParameteres();
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();

				objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID);

				//Done for Itrack Issue 7169 on 12 Aug 2010
				//When Paid Loss, Final is reversed and the Followup Re-open activity is Incomplete and we go to complete the re-open activity,
				//the activity should be visible as a reversed activity(in Purple color),i.e. is ACCOUNTING_SUPPRESSED should be 1
				// which currently becomes 0 when we complete the activity as suppress accounting checkbox is not visible for re-open activities 
				if(objActivityInfo.ACTION_ON_PAYMENT == 168)
				{
					DataSet dsAccountingSuppressed = GetValuesForPageControls(objActivityInfo.CLAIM_ID.ToString(),objActivityInfo.ACTIVITY_ID.ToString());
					if(dsAccountingSuppressed.Tables[0].Rows[0]["ACCOUNTING_SUPPRESSED"].ToString() == "True")
						objActivityInfo.ACCOUNTING_SUPPRESSED = 1;
					else
						objActivityInfo.ACCOUNTING_SUPPRESSED = 0;
				}

				string strDrAcct="";
				string strCrAcct="";
				if(objActivityInfo.ACCOUNTING_SUPPRESSED.ToString() == "0")
				{
					DataSet DS = objDataWrapper.ExecuteDataSet("PROC_GET_CLM_DR_CR_ACCT_FOR_ACTIVITY");
					if (DS!=null &&  DS.Tables.Count>0 && DS.Tables[0]!=null && DS.Tables[0].Rows.Count>0)
					{
						strDrAcct=DS.Tables[0].Rows[0][1].ToString();
						strCrAcct=DS.Tables[0].Rows[0][2].ToString();
					}
					else
					{
						return 7;
					}
				}
				//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID);
				objDataWrapper.AddParameter("@USER_ID",objActivityInfo.CREATED_BY);
				objDataWrapper.AddParameter("@ADJUSTER_ID_PARAM",strADJUSTER_ID);
				objDataWrapper.AddParameter("@ACCOUNTING_SUPPRESSED",objActivityInfo.ACCOUNTING_SUPPRESSED,SqlDbType.Bit);
				//objDataWrapper.AddParameter("@WOLVERINE_USER1",strWolverineUser1);
				//objDataWrapper.AddParameter("@WOLVERINE_USER2",strWolverineUser2);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ACTIVITY_ID",0,SqlDbType.Int,ParameterDirection.ReturnValue);
				SqlParameter objNofifyUsersParam  = (SqlParameter) objDataWrapper.AddParameter("@NOTIFY_USER",0,SqlDbType.Int,ParameterDirection.Output);
				objDataWrapper.ExecuteNonQuery(strSql);
				returnVal = int.Parse(objSqlParameter.Value.ToString());
			

				if(returnVal == 1) //Activity has been completed successfully
				{
					//If we have to send the notification
					if(objNofifyUsersParam!=null && Convert.ToInt32(objNofifyUsersParam.Value.ToString())==1)
					{
						SendNotifyDiaryEntry(objActivityInfo,objDataWrapper);
					}
					
					//Commit Acct data -> PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY

					# region Posting for DEBIT entry
					if(objActivityInfo.ACCOUNTING_SUPPRESSED.ToString() == "0")
					{
						if(objActivityInfo.GL_POSTING_REQUIRED == "Y")
							PerformGLEntry(objDataWrapper,objActivityInfo.CLAIM_ID,objActivityInfo.ACTIVITY_ID,objActivityInfo.CREATED_BY,strDrAcct,strCrAcct);
					
						//					objDataWrapper.ClearParameteres();
						//					objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
						//					objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID);
						//
						//					objDataWrapper.AddParameter("@USER_ID",int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()));
						//				
						//					objDataWrapper.AddParameter("@DIV_ID",ConfigurationSettings.AppSettings.Get("DIV_ID").ToString());
						//					objDataWrapper.AddParameter("@DEPT_ID",ConfigurationSettings.AppSettings.Get("DEPT_ID").ToString());
						//					objDataWrapper.AddParameter("@PC_ID",ConfigurationSettings.AppSettings.Get("PC_ID").ToString());
						//				
						//					objDataWrapper.AddParameter("@ACCOUNT_ID",strDrAcct);
						//					//Incase of DR pass '1' and in case of CR pass '-1'
						//					objDataWrapper.AddParameter("@TRANSACTION_AMOUNT","1");
						//				
						//					objDataWrapper.ExecuteNonQuery("PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY");
						//				
						//					returnVal = int.Parse(objSqlParameter.Value.ToString());
						#endregion


						# region Posting for CREDIT entry					
						//					objDataWrapper.ClearParameteres();
						//
						//					objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
						//					objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID);
						//
						//					objDataWrapper.AddParameter("@USER_ID",int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()));
						//				
						//					objDataWrapper.AddParameter("@DIV_ID",ConfigurationSettings.AppSettings.Get("DIV_ID").ToString());
						//					objDataWrapper.AddParameter("@DEPT_ID",ConfigurationSettings.AppSettings.Get("DEPT_ID").ToString());
						//					objDataWrapper.AddParameter("@PC_ID",ConfigurationSettings.AppSettings.Get("PC_ID").ToString());
						//				
						//					objDataWrapper.AddParameter("@ACCOUNT_ID",strCrAcct);
						//					//Incase of DR pass '1' and in case of CR pass '-1'
						//					objDataWrapper.AddParameter("@TRANSACTION_AMOUNT","-1");
						//				
						//					objDataWrapper.ExecuteNonQuery("PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY");
						//				
						//					returnVal = int.Parse(objSqlParameter.Value.ToString());
						#endregion


						//Commit check info	-> PROC_INSERT_CLAIMS_CHECK_ON_COMMIT						
						objDataWrapper.ClearParameteres();

						objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
						objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID);
				
						objDataWrapper.AddParameter("@USER_ID",objActivityInfo.CREATED_BY);

						objDataWrapper.AddParameter("@DIV_ID",ConfigurationManager.AppSettings.Get("DIV_ID").ToString());
                        objDataWrapper.AddParameter("@DEPT_ID", ConfigurationManager.AppSettings.Get("DEPT_ID").ToString());
                        objDataWrapper.AddParameter("@PC_ID", ConfigurationManager.AppSettings.Get("PC_ID").ToString());

						objDataWrapper.AddParameter("@DR_ACCT",strDrAcct);
						objDataWrapper.AddParameter("@CR_ACCT",strCrAcct);

						objDataWrapper.ExecuteNonQuery("PROC_INSERT_CLAIMS_CHECK_ON_COMMIT");
					}

					if(TransactionLogRequired) 
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
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1812", "");// "Activity has been completed";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
                        objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1810","")+" : " + claimNumber;//"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 15 Jan 2010
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
					}

					returnVal = int.Parse(objSqlParameter.Value.ToString());
					
				}//Activity has been completed successfully
			}

			catch(Exception excep)
			{
				throw (excep);
			}

			return returnVal;
		
		}
		#endregion

		public int AddReserveDetails(ClsActivityInfo objActivityInfo,System.Collections.ArrayList aList,int iNewReserveAdded)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			ClsReserveDetails objReserveDetails=new ClsReserveDetails();;
			try
			{
              
				int intReturnValue=0;
				if(iNewReserveAdded==0)
                    intReturnValue = this.Add(objActivityInfo, objDataWrapper);
				objDataWrapper.ClearParameteres();
				intReturnValue = objReserveDetails.Add(aList,objDataWrapper);
				if(intReturnValue>0 && (iNewReserveAdded==0))
				{
					//this.CompleteActivity(objActivityInfo,"0",objDataWrapper);
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					//Added by Asfa (18-Jan-2008) - iTrack issue #3393
					string strClmLimit="";
							
					DataSet dsClmDiaryLimit = GetCalimDiaryLimits(objActivityInfo.ACTIVITY_REASON);
					if(dsClmDiaryLimit!=null && dsClmDiaryLimit.Tables.Count>0 && dsClmDiaryLimit.Tables[0].Rows.Count>0)
					{
						if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT || objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT)
							strClmLimit = dsClmDiaryLimit.Tables[0].Rows[0]["CLAIM_PAYMENT_LIMIT"].ToString();
						else if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.RESERVE_UPDATE)
							strClmLimit = dsClmDiaryLimit.Tables[0].Rows[0]["CLAIM_RESERVE_LIMIT"].ToString();
					}
					
					if((objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT  || objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT)&& objActivityInfo.PAYMENT_AMOUNT>int.Parse(strClmLimit))
					{
						SendNotifyDiaryEntry(objActivityInfo);
					}
					else if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.RESERVE_UPDATE)
					{
						DataSet dsTemp = GetClaimDetails(objActivityInfo.CLAIM_ID);
						if (dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
						{
							if(Convert.ToDouble(dsTemp.Tables[0].Rows[0]["OUTSTANDING_RESERVE"].ToString()) > Convert.ToDouble(strClmLimit))
							{
								SendNotifyDiaryEntry(objActivityInfo);	
							}
						}
					}

				}
				
				else
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return intReturnValue;				
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper=null;
			}
		}

		public void SendNotifyDiaryEntry(ClsActivityInfo objActivityInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			this.SendNotifyDiaryEntry(objActivityInfo,objDataWrapper);
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		}
		public void SendNotifyDiaryEntry(ClsActivityInfo objActivityInfo, DataWrapper objDataWrapper)
		{
			Cms.Model.Diary.TodolistInfo objTodolistInfo = new Cms.Model.Diary.TodolistInfo();
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
			objTodolistInfo.FROMUSERID         =   objActivityInfo.CREATED_BY;			
			//Added by Asfa (15-Jan-2008) - iTrack issue #3393
			string strClmLimit="";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@ACTIVITY_REASON",objActivityInfo.ACTIVITY_REASON);
			DataSet dsClmDiaryLimit = objDataWrapper.ExecuteDataSet("Proc_GetCLM_DIARY_LIMIT");
			if(dsClmDiaryLimit!=null && dsClmDiaryLimit.Tables.Count>0 && dsClmDiaryLimit.Tables[0].Rows.Count>0)
			{
				if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT  || objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT)
					strClmLimit = dsClmDiaryLimit.Tables[0].Rows[0]["CLAIM_PAYMENT_LIMIT"].ToString();
				else if(objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.RESERVE_UPDATE)
					strClmLimit = dsClmDiaryLimit.Tables[0].Rows[0]["CLAIM_RESERVE_LIMIT"].ToString();
			}
			if((objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT &&  objActivityInfo.PAYMENT_AMOUNT > Convert.ToDouble(strClmLimit)) || (objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT)&& objActivityInfo.EXPENSES > Convert.ToDouble(strClmLimit))
			{
				objTodolistInfo.LISTTYPEID         =   (int)ClsDiary.enumDiaryType.PAYMENT_LIMIT_EXCEEDED;
				//Done for Itrack Issue 6101 on 17 Sept 09
                objTodolistInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1818", "");// "Payment exceeded";
                objTodolistInfo.NOTE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1819", "");// "Payment has been posted that exceeds company limit";
			}
			else if(objActivityInfo.ACTIVITY_REASON== (int)enumTransactionLookup.RESERVE_UPDATE)
			{
				objTodolistInfo.LISTTYPEID         =   (int)ClsDiary.enumDiaryType.RESERVES_LIMIT_EXCEEDED;
				//Done for Itrack Issue 6101 on 17 Sept 09
                objTodolistInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1820", "");// "Reserves exceeded";
                objTodolistInfo.NOTE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1821", "");// "Reserve has been posted that exceeds company limit";			
			}
			else
			{
				objTodolistInfo.LISTTYPEID         =   (int)ClsDiary.enumDiaryType.ADJUSTER_LIMIT_NOTIFICATION;
                objTodolistInfo.NOTE = objTodolistInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1822", ""); //"Notification Limit for Adjuster has been exceeded";						
			}
			objTodolistInfo.LISTOPEN            =   "Y";
			objTodolistInfo.CUSTOMER_ID			=	objActivityInfo.CUSTOMER_ID;									
			objTodolistInfo.POLICY_ID			=	objActivityInfo.POLICY_ID;
			objTodolistInfo.POLICY_VERSION_ID	=	objActivityInfo.POLICY_VERSION_ID;						
			objTodolistInfo.CREATED_BY			=	objTodolistInfo.MODIFIED_BY = objActivityInfo.CREATED_BY;
			objTodolistInfo.CREATED_DATETIME	=	objTodolistInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;						
			objTodolistInfo.LOB_ID				=   objActivityInfo.LOB_ID;
			objTodolistInfo.CLAIMID				=	objActivityInfo.CLAIM_ID;			
			objTodolistInfo.MODULE_ID			= (int)BlCommon.ClsDiary.enumModuleMaster.CLAIM;
			objDiary.DiaryEntryfromSetup(objTodolistInfo,objDataWrapper);
		}

		//Added for Itrack Issue 6542 on 20 Oct 09 
		public void SendNotifyDiaryAuthorityLimitExceeded(ClsActivityInfo objActivityInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			this.SendNotifyDiaryAuthorityLimitExceeded(objActivityInfo,objDataWrapper);
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		}

		public void SendNotifyDiaryAuthorityLimitExceeded(ClsActivityInfo objActivityInfo, DataWrapper objDataWrapper)
		{
			Cms.Model.Diary.TodolistInfo objTodolistInfo = new Cms.Model.Diary.TodolistInfo();
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
			objTodolistInfo.FROMUSERID					 = objActivityInfo.CREATED_BY;			
			
			if((objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT) || (objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT))
			{
				objTodolistInfo.LISTTYPEID         =   (int)ClsDiary.enumDiaryType.PAYMENT_LIMIT_EXCEEDED;
                objTodolistInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1823", "");//   "Payment exceeds Users limit of Authority";
                objTodolistInfo.NOTE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1824", "");// "Activity could not be completed as the Payment exceeds the Users limit of Authority";
			}
			else if(objActivityInfo.ACTIVITY_REASON== (int)enumTransactionLookup.RESERVE_UPDATE)
			{
				objTodolistInfo.LISTTYPEID         =   (int)ClsDiary.enumDiaryType.RESERVES_LIMIT_EXCEEDED;
                objTodolistInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1825", "");// "Reserves exceeds Users limit of Authority";
                objTodolistInfo.NOTE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1826", "");// "Activity could not be completed as the Reserve exceeds the Users limit of Authority";			
			}
			else
			{
				objTodolistInfo.LISTTYPEID         =   (int)ClsDiary.enumDiaryType.ADJUSTER_LIMIT_NOTIFICATION;
                objTodolistInfo.NOTE = objTodolistInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1822", "");// "Notification Limit for Adjuster has been exceeded";						
			}
			objTodolistInfo.LISTOPEN			=   "Y";
			objTodolistInfo.CUSTOMER_ID			=	objActivityInfo.CUSTOMER_ID;									
			objTodolistInfo.POLICY_ID			=	objActivityInfo.POLICY_ID;
			objTodolistInfo.POLICY_VERSION_ID	=	objActivityInfo.POLICY_VERSION_ID;						
			objTodolistInfo.CREATED_BY			=	objTodolistInfo.MODIFIED_BY = objActivityInfo.CREATED_BY;
			objTodolistInfo.CREATED_DATETIME	=	objTodolistInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;						
			objTodolistInfo.LOB_ID				=	objActivityInfo.LOB_ID;
			objTodolistInfo.CLAIMID				=	objActivityInfo.CLAIM_ID;			
			objTodolistInfo.MODULE_ID			=	(int)BlCommon.ClsDiary.enumModuleMaster.CLAIM;
			objDiary.DiaryEntryfromSetup(objTodolistInfo,objDataWrapper);
		}
		//Added till here

		//Added for Itrack Issue 7169 on 6 May 2010 
		public void SendReversePaymentDiaryEntry(ClsActivityInfo objActivityInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			this.SendReversePaymentDiaryEntry(objActivityInfo,objDataWrapper);
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		}

		public void SendReversePaymentDiaryEntry(ClsActivityInfo objActivityInfo, DataWrapper objDataWrapper)
		{
			Cms.Model.Diary.TodolistInfo objTodolistInfo = new Cms.Model.Diary.TodolistInfo();
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
			objTodolistInfo.FROMUSERID					 = objActivityInfo.CREATED_BY;			
			
			if((objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT) || (objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT) || (objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.RECOVERY))
			{
				objTodolistInfo.LISTTYPEID         =   (int)ClsDiary.enumDiaryType.REVERSE_PAYMENT;
                objTodolistInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1827", "");// "Claim Transaction has been reversed.";
                objTodolistInfo.NOTE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1827", ""); //"Claim Transaction has been reversed.";
			}
			
			objTodolistInfo.LISTOPEN			=   "Y";
			objTodolistInfo.CUSTOMER_ID			=	objActivityInfo.CUSTOMER_ID;									
			objTodolistInfo.POLICY_ID			=	objActivityInfo.POLICY_ID;
			objTodolistInfo.POLICY_VERSION_ID	=	objActivityInfo.POLICY_VERSION_ID;						
			objTodolistInfo.CREATED_BY			=	objTodolistInfo.MODIFIED_BY = objActivityInfo.CREATED_BY;
			objTodolistInfo.CREATED_DATETIME	=	objTodolistInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;						
			objTodolistInfo.LOB_ID				=	objActivityInfo.LOB_ID;
			objTodolistInfo.CLAIMID				=	objActivityInfo.CLAIM_ID;			
			objTodolistInfo.MODULE_ID			=	(int)BlCommon.ClsDiary.enumModuleMaster.CLAIM;
			objDiary.DiaryEntryfromSetup(objTodolistInfo,objDataWrapper);
		}
		//Added till here


		/// <summary>
		/// We should get diary entry whenever void or reverse transaction is done on paid loss final
		/// transaction that has had other transaction done after paid loss final.
		/// </summary>
		/// <param name="objActivityInfo"></param>
		public void SendPaidLossFinalDiaryEntry(ClsActivityInfo objActivityInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			this.SendPaidLossFinalDiaryEntry(objActivityInfo,objDataWrapper);
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		}

		/// <summary>
		/// We should get diary entry whenever void or reverse transaction is done on paid loss final
		/// transaction that has had other transaction done after paid loss final.
		/// </summary>
		/// <param name="objActivityInfo"></param>
		/// <param name="objDataWrapper"></param>
		public void SendPaidLossFinalDiaryEntry(ClsActivityInfo objActivityInfo, DataWrapper objDataWrapper)
		{
			Cms.Model.Diary.TodolistInfo objTodolistInfo = new Cms.Model.Diary.TodolistInfo();
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
			objTodolistInfo.FROMUSERID					 = objActivityInfo.CREATED_BY;			
			
			if((objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.CLAIM_PAYMENT) || (objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.EXPENSE_PAYMENT) || (objActivityInfo.ACTIVITY_REASON==(int)enumTransactionLookup.RECOVERY))
			{
				objTodolistInfo.LISTTYPEID         =   (int)ClsDiary.enumDiaryType.PAID_LOSS_FINAL_VOIDED;
                objTodolistInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1828", "");// "Paid Loss Final Voided.";
                objTodolistInfo.NOTE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1828", "");// "Paid Loss Final Voided.";
			}
			
			objTodolistInfo.LISTOPEN			=   "Y";
			objTodolistInfo.CUSTOMER_ID			=	objActivityInfo.CUSTOMER_ID;									
			objTodolistInfo.POLICY_ID			=	objActivityInfo.POLICY_ID;
			objTodolistInfo.POLICY_VERSION_ID	=	objActivityInfo.POLICY_VERSION_ID;						
			objTodolistInfo.CREATED_BY			=	objTodolistInfo.MODIFIED_BY = objActivityInfo.CREATED_BY;
			objTodolistInfo.CREATED_DATETIME	=	objTodolistInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;						
			objTodolistInfo.LOB_ID				=	objActivityInfo.LOB_ID;
			objTodolistInfo.CLAIMID				=	objActivityInfo.CLAIM_ID;			
			objTodolistInfo.MODULE_ID			=	(int)BlCommon.ClsDiary.enumModuleMaster.CLAIM;
			objDiary.DiaryEntryfromSetup(objTodolistInfo,objDataWrapper);
		}

		#region Peforming Accounting Entries for Activities
		public void PerformGLEntry(DataWrapper objDataWrapper, int CLAIM_ID, int ACTIVITY_ID, int strUserId, string strDrAcct,string strCrAcct )
		{
			objDataWrapper.CommandType=CommandType.StoredProcedure;
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@ACTIVITY_ID",ACTIVITY_ID);

			objDataWrapper.AddParameter("@USER_ID",strUserId);

            objDataWrapper.AddParameter("@DIV_ID", ConfigurationManager.AppSettings.Get("DIV_ID").ToString());
            objDataWrapper.AddParameter("@DEPT_ID", ConfigurationManager.AppSettings.Get("DEPT_ID").ToString());
            objDataWrapper.AddParameter("@PC_ID", ConfigurationManager.AppSettings.Get("PC_ID").ToString());
				
			objDataWrapper.AddParameter("@DEBIT_ACCOUNT_ID",strDrAcct);
			objDataWrapper.AddParameter("@CREDIT_ACCOUNT_ID",strCrAcct);
			//Incase of DR pass '1' and in case of CR pass '-1'
			//objDataWrapper.AddParameter("@TRANSACTION_AMOUNT",TransAmt);
				
			objDataWrapper.ExecuteNonQuery("PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY");
			objDataWrapper.ClearParameteres();	
		}
		#endregion

		#region Update Reserve method
		/// <summary>
		/// Update Reserve method that saves the reserve amount info at the table
		/// </summary>
		/// <param name="objOldActivityInfo">Model object having old information</param>
		/// <param name="objActivityInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateActivityReserve(ClsActivityInfo objActivityInfo)
		{
			string		strStoredProc	=	"Proc_UpdateActivityReserve";		
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objActivityInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityInfo.ACTIVITY_ID);
				objDataWrapper.AddParameter("@RESERVE_AMOUNT",objActivityInfo.RESERVE_AMOUNT);
				objDataWrapper.AddParameter("@PAYMENT_AMOUNT",objActivityInfo.PAYMENT_AMOUNT);
				objDataWrapper.AddParameter("@EXPENSES",objActivityInfo.EXPENSES);
				objDataWrapper.AddParameter("@RECOVERY",objActivityInfo.RECOVERY);
				objDataWrapper.AddParameter("@RI_RESERVE",objActivityInfo.RI_RESERVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objActivityInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objActivityInfo.LAST_UPDATED_DATETIME);

				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				objDataWrapper.ClearParameteres();				
				if ((objActivityInfo.ACTIVITY_REASON == 11836)&& (objActivityInfo.ACTIVITY_STATUS == 11801))
					PerformActngEntryForNewReserve(objActivityInfo,objDataWrapper);
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
		#endregion

//		#region ActivateDeactivate Activity
//		public int ActivateDeactivateActivity(string strCLAIM_ID, string strACTIVITY_ID, string strSTATUS)
//		{
//			string strSql = "Proc_ActivateDeactivateCLM_ACTIVITY";
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);
//			objDataWrapper.AddParameter("@ACTIVITY_ID",strACTIVITY_ID);
//			objDataWrapper.AddParameter("@IS_ACTIVE",strSTATUS);
//			int returnResult =  objDataWrapper.ExecuteNonQuery(strSql);		
//			return returnResult;
//			
//		}
//		#endregion

		#region Check for existence of any activity still not completed/ Authorized
		// Condition added For iTrack issue #5926
		public bool AnyIncompleteActivity(string strCLAIM_ID)
		{
			return AnyIncompleteActivity(strCLAIM_ID,"-1");
		}
		public bool AnyIncompleteActivity(string strCLAIM_ID,string strActivityId)
		{
			string strSql = "Proc_IncompleteActivityCLM_ACTIVITY";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);
				// Condition added For iTrack issue #5926
			if(strActivityId!="-1")
				objDataWrapper.AddParameter("@ACTIVITY_ID",strActivityId);		

			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",0,SqlDbType.Int,ParameterDirection.ReturnValue);
			objDataWrapper.ExecuteNonQuery(strSql);
			int returnValue = int.Parse(objSqlParameter.Value.ToString());
			if(returnValue>0)
				return true;
			else
				return false;		

		}
		#endregion

        public bool ClaimHasCloseReserveActivity(int ClaimID)
        {
            string strSql = "Proc_GetClaimHasCloseReserveActivity";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CLAIM_ID", ClaimID);

            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@HAS_CLOSED_ACTIVITY", 0, SqlDbType.Char, ParameterDirection.Output);
            objDataWrapper.ExecuteNonQuery(strSql);
            string HAS_CLOSED_ACTIVITY = objSqlParameter.Value.ToString();

            if (HAS_CLOSED_ACTIVITY =="Y")
                return true;
            else
                return false;

        }
		/*public static DataTable ReserveTransactionCodes(string strCLAIM_ID)
		{
			string	strStoredProc =	"Proc_GetReserveCodes";			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);						
			DataSet dsTemp = objWrapper.ExecuteDataSet(strStoredProc);
			if(dsTemp!=null && dsTemp.Tables.Count>0)
				return dsTemp.Tables[0];
			else
				return null;
		}*/

		public int CompleteActivitiesStatus(string strCLAIM_ID)
		{
			string		strStoredProc	=	"Proc_CompleteActivityStatus";			
			int returnResult = 0;			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);		
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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
			}
		}

        public int DeleteActivity(int ClaimID, int ActivityID)
        {
            string strStoredProc = "Proc_DeleteClaimActivity";
            int returnResult = 0;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objDataWrapper.AddParameter("@ACTIVITY_ID", ActivityID);
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }

		public string GetActivitySummary(string strCLAIM_ID, int PolicyCurrency)
		{
			string strSql = "Proc_GetClaimActivitySummary";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);
            objDataWrapper.AddParameter("@POLICY_CURRENCY", PolicyCurrency);						
			DataSet dsTemp = objDataWrapper.ExecuteDataSet(strSql);
			if(dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0)
			{
				System.Text.StringBuilder strSummaryRow = new System.Text.StringBuilder();
				

				strSummaryRow.Append("<table class='midcolora' border='0'  width='100%' style='back-color:Pink'>");
				/*strSummaryRow.Append("<tr id='trBlankRow'><td width='10%'><b>Summary</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Reason</b></td>");
				strSummaryRow.Append("<td ><b>Outstanding</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Paid</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Reason</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Recovery</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Incurred</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Expense</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Reinsurance Outstanding</b></td>");
				strSummaryRow.Append("<td width='%'><b>Loss Reinsurance Recovered</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Expense Reinsurance Recovered</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Status</b></td>");
				strSummaryRow.Append("<td width='8%'><b>Activity Date</b></td>");
				strSummaryRow.Append("<td width='10%'><b>Added By</b></td></tr>");*/
                //^^^^^^


                strSummaryRow.Append("<tr><td class='midcolora' width='13%'><b>@SUMMARY@</b></td>");
                if (dsTemp.Tables[0].Rows[0]["CLAIM_RESERVE_AMOUNT"] != null && dsTemp.Tables[0].Rows[0]["CLAIM_RESERVE_AMOUNT"].ToString() != "")
                    strSummaryRow.Append("<td  class='midcolorr' width='8%' align='right'>" + dsTemp.Tables[0].Rows[0]["CLAIM_RESERVE_AMOUNT"].ToString() + "</td>");
				else
                    strSummaryRow.Append("<td class='midcolorr' width='8%' align='right'>&nbsp;</td>");

                if (dsTemp.Tables[0].Rows[0]["CLAIM_PAYMENT_AMOUNT"] != null && dsTemp.Tables[0].Rows[0]["CLAIM_PAYMENT_AMOUNT"].ToString() != "")
                    strSummaryRow.Append("<td class='midcolorr' width='8%' align='right'>" + dsTemp.Tables[0].Rows[0]["CLAIM_PAYMENT_AMOUNT"].ToString() + "</td>");
				else
					strSummaryRow.Append("<td class='midcolorr' width='8%' align='right'>&nbsp;</td>");

                if (dsTemp.Tables[0].Rows[0]["PAYMENT_AMOUNT"] != null && dsTemp.Tables[0].Rows[0]["PAYMENT_AMOUNT"].ToString() != "")
                    strSummaryRow.Append("<td class='midcolorr' width='8%' align='right'>" + dsTemp.Tables[0].Rows[0]["PAYMENT_AMOUNT"].ToString() + "</td>");
				else
					strSummaryRow.Append("<td class='midcolorr' width='8%' align='right'>&nbsp;</td>");

                if (dsTemp.Tables[0].Rows[0]["CLAIM_RI_RESERVE"] != null && dsTemp.Tables[0].Rows[0]["CLAIM_RI_RESERVE"].ToString() != "")
                    strSummaryRow.Append("<td class='midcolorr' width='8%' align='right'>" + dsTemp.Tables[0].Rows[0]["CLAIM_RI_RESERVE"].ToString() + "</td>");
				else
                    strSummaryRow.Append("<td class='midcolorr' width='8%' align='right'>&nbsp;</td>");

                if (dsTemp.Tables[0].Rows[0]["RI_PAID_RESERVE"] != null && dsTemp.Tables[0].Rows[0]["RI_PAID_RESERVE"].ToString() != "")
                    strSummaryRow.Append("<td class='midcolorr' width='10%' align='right'>" + dsTemp.Tables[0].Rows[0]["RI_PAID_RESERVE"].ToString() + "</td>");
				else
                    strSummaryRow.Append("<td class='midcolorr' width='10%' align='right'>&nbsp;</td>");

                if (dsTemp.Tables[0].Rows[0]["CO_TOTAL_RESERVE_AMT"] != null && dsTemp.Tables[0].Rows[0]["CO_TOTAL_RESERVE_AMT"].ToString() != "")
                    strSummaryRow.Append("<td class='midcolorr' width='10%' align='right'>" + dsTemp.Tables[0].Rows[0]["CO_TOTAL_RESERVE_AMT"].ToString() + "</td>");
				else
                    strSummaryRow.Append("<td class='midcolorr' width='10%' align='right'>&nbsp;</td>");

                if (dsTemp.Tables[0].Rows[0]["CO_PAID_RESREVE"] != null && dsTemp.Tables[0].Rows[0]["CO_PAID_RESREVE"].ToString() != "")
                    strSummaryRow.Append("<td class='midcolorr' width='10%' align='right'>" + dsTemp.Tables[0].Rows[0]["CO_PAID_RESREVE"].ToString() + "</td>");
				else
                    strSummaryRow.Append("<td class='midcolorr' width='10%' align='right'>&nbsp;</td>");

                strSummaryRow.Append("<td class='midcolorr' width='10%' align='right'>&nbsp;</td>");
                strSummaryRow.Append("<td class='midcolorr' width='10%' align='right'>&nbsp;</td>");
                strSummaryRow.Append("<td class='midcolorr' width='5%' align='right'>&nbsp;</td>");
                strSummaryRow.Append("</tr></table>");
				
				//strSummaryRow.Append("<script>document.getElementById('trBlankRow').style.display='none'</script>");
				
				return strSummaryRow.ToString();
			}
			else
				return "";
		
		}

		public DataTable GetAdjusterLimits(string CLAIM_ID, string ADJUSTER_ID)
		{
			string strSql = "Proc_GetAdjusterLimits";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@ADJUSTER_ID",int.Parse(ADJUSTER_ID));			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		public DataTable GetActivityCodes(string strRecordStatus, int LangID)
		{
			string strSql = "Proc_GetActivityCodes";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@RECORD_STATUS",strRecordStatus);
            objDataWrapper.AddParameter("@LANG_ID", LangID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}

		#region "ActivateDeactivateActivity"
		public int ActivateDeactivateActivity(ClsActivityInfo objActivityInfo,string strCLAIM_ID, string strACTIVITY_ID, string strACTIVITY_STATUS, string strIS_ACTIVE)
		{
			string strSql = "Proc_ActivateDeactivateCLM_ACTIVITY";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			//Done for Itrack Issue 6932 on 1 Feb 2010
			DataSet ds = new DataSet();
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(strCLAIM_ID));
			ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
			string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
			int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
			int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
			int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

			objDataWrapper.AddParameter("@ACTIVITY_ID",strACTIVITY_ID);
			objDataWrapper.AddParameter("@ACTIVITY_STATUS",strACTIVITY_STATUS);
			objDataWrapper.AddParameter("@IS_ACTIVE",strIS_ACTIVE);			
			int ReturnResult = objDataWrapper.ExecuteNonQuery(strSql);		
	
			//Done for Itrack Issue 6932 on 15 Jan 2010
			if(TransactionLogRequired) 
			{
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				objActivityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddActivity.aspx.resx");
				string strTranXML = objBuilder.GetTransactionLogXML(objActivityInfo);					
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				//Done for Itrack Issue 6932 on 1 Feb 2010
				objTransactionInfo.CLIENT_ID		=	customerID;
				objTransactionInfo.POLICY_ID		=	policyID;
				objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
				objTransactionInfo.RECORDED_BY		=	objActivityInfo.CREATED_BY;
				objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
				if(strIS_ACTIVE.ToUpper() == "N")
					objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1813","");//"Activity has been deactivated";
				else
                    objTransactionInfo.TRANS_DESC       = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1814", "");// "Activity has been activated";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
                objTransactionInfo.CUSTOM_INFO =        Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1810","")+" : " + claimNumber;//"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 1 Feb 2010
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}

			return ReturnResult;		
		}
		//Added for Itrack Issue 7169 on 29 April 2010 -- 
		public string GetIS_BNK_RECONCILED(string CHECK_ID)
		{
			string strSql = "Proc_GetIS_BNK_RECONCILED";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CHECK_ID",CHECK_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0].Rows[0]["IS_BNK_RECONCILED"].ToString();
		}

		public DataSet GetClm_DetailType_Values(int intAction_on_Payment)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@DETAIL_TYPE_ID",intAction_on_Payment);
			DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetValuesCLM_TYPE_DETAIL");
			return ds;
		}

        public DataSet PrintClaimReceipt(int ClaimID, int ActivityID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.ClearParameteres();
            objDataWrapper.AddParameter("@CLAIM_ID", ClaimID);
            objDataWrapper.AddParameter("@ACTIVITY_ID", ActivityID);
            return objDataWrapper.ExecuteDataSet("Proc_GetProductClaimRecipt");
			
		}
		
		public string CheckVoidedActivity(int intACTION_ON_PAYMENT)
		{
			string	strStoredProc =	"Proc_chkVoid_Activities";
			string retVoidVal ="";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.ClearParameteres();
			
			try
			{
				objWrapper.AddParameter("@ACTION_ON_PAYMENT",intACTION_ON_PAYMENT); 
				DataSet dsVoidActivity = objWrapper.ExecuteDataSet(strStoredProc);
				
				if(dsVoidActivity!=null)
				{
					if (dsVoidActivity.Tables[0] != null && dsVoidActivity.Tables[0].Rows.Count > 0)
						retVoidVal = dsVoidActivity.Tables[0].Rows[0]["IS_VOID_ACTIVITY"].ToString();
				}
				objWrapper.ClearParameteres();
				return retVoidVal ;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
        //  Added by santosh kumar gautam on 27 dec 2010
        public string CheckClaimVoidedActivity(int ClaimID, int ActivityID)
        {
            string strStoredProc = "Proc_CheckClaimVoidedActivity";
            string retVoidVal = "";
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.ClearParameteres();

            try
            {
                objWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objWrapper.AddParameter("@ACTIVITY_ID", ActivityID);
                DataSet dsVoidActivity = objWrapper.ExecuteDataSet(strStoredProc);

                if (dsVoidActivity != null)
                {
                    if (dsVoidActivity.Tables[0] != null && dsVoidActivity.Tables[0].Rows.Count > 0)
                        retVoidVal = dsVoidActivity.Tables[0].Rows[0]["IS_VOIDED_REVERSED_ACTIVITY"].ToString();
                }
                objWrapper.ClearParameteres();
                return retVoidVal;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

		//Modified for Itrack Issue 7169 on 21 June 2010
		//Modified for Itrack Issue 7169 on 21 June 2010
		public DataSet CheckAllowActivityComplete(int claimID, int activityID)
		{
			string	strStoredProc =	"Proc_chkActivityCommit";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.ClearParameteres();
			
			try
			{
				objWrapper.AddParameter("@CLAIM_ID",claimID);
				if(activityID != -1)
					objWrapper.AddParameter("@ACTIVITY_ID",activityID);
				DataSet dsActivityComplete = objWrapper.ExecuteDataSet(strStoredProc);
				objWrapper.ClearParameteres();

				return dsActivityComplete;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		public int AddAutoActivity(ClsActivityInfo objActivityInfo,int intClaimID,int intActivityId, int intAction_on_Payment, int intCreatedBy,int intACCOUNTING_SUPPRESSED,out int isPaidLossFinalVoided )
		{

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				isPaidLossFinalVoided = 0;
				int returnResult = 0;
				string		strStoredProc	=	"Proc_AUTO_ACTIVITY_VOID_InsertCLM_ACTIVITY";
				string amount = "";
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				

				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",intClaimID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@CLAIM_ID",intClaimID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",intActivityId);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetXMLProcCLM_ACTIVITY");
				string []stractivityReason = ds.Tables[0].Rows[0]["ACTIVITY_REASON"].ToString().Split('^');
				
				if(stractivityReason[0] == "11774")
				{//Added for Itrack Issue 6932(Attachment 5) on 5 July 2010
					if(ds.Tables[0].Rows[0]["EXPENSES"].ToString() !="")
						amount = ds.Tables[0].Rows[0]["EXPENSES"].ToString();
					else if(ds.Tables[0].Rows[0]["EXPENSE_REINSURANCE_RECOVERED"].ToString() !="")
						amount = ds.Tables[0].Rows[0]["EXPENSE_REINSURANCE_RECOVERED"].ToString();
					else if(ds.Tables[0].Rows[0]["LOSS_REINSURANCE_RECOVERED"].ToString() !="")
						amount = ds.Tables[0].Rows[0]["LOSS_REINSURANCE_RECOVERED"].ToString();
					else if(ds.Tables[0].Rows[0]["RECOVERY"].ToString() !="")
						amount = ds.Tables[0].Rows[0]["RECOVERY"].ToString();
				}
				else if(stractivityReason[0] == "11775")
					amount = ds.Tables[0].Rows[0]["PAYMENT_AMOUNT"].ToString();
				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@CLAIM_ID",intClaimID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",intActivityId);
				objDataWrapper.AddParameter("@ACTION_ON_PAYMENT",intAction_on_Payment);
				objDataWrapper.AddParameter("@CREATED_BY",intCreatedBy);
				objDataWrapper.AddParameter("@ACCOUNTING_SUPPRESSED",intACCOUNTING_SUPPRESSED);

				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@IS_PAID_LOSS_FINAL_VOIDED",SqlDbType.Int,ParameterDirection.Output);
								
				
				if(TransactionLogRequired) 
				{
					objActivityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddActivity.aspx.resx");					
					string strTranXML = objBuilder.GetTransactionLogXML(objActivityInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	objActivityInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID		=	objActivityInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objActivityInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objActivityInfo.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					if(intACCOUNTING_SUPPRESSED == 0)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1815","")+" # " + intActivityId + " "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1816","");//"Activity # " + intActivityId + " has been Voided.";
					else
                        objTransactionInfo.TRANS_DESC =         Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1815","")+" # " + intActivityId + " "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1817","");//"Activity # " + intActivityId + " has been Reversed.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1810","")+" : " + claimNumber + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1840","")+" = " + amount.Replace("-", ""); //"Claim Number : " + claimNumber + "; Amount = " + amount.Replace("-", "");//Done for Itrack Issue 6932(Attachment 5) on 5 July 2010 
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);

				if(objSqlParameter.Value!=null && int.Parse(objSqlParameter.Value.ToString())>0)
					isPaidLossFinalVoided= int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
		}

		//Added for Itrack Issue 7169 on 23 June 2010
		public string chkVisiblebtnVoid_Reserve(int claimID)
		{
			string	strStoredProc =	"Proc_btnVoid_Reverse_Visible";
			string result = "";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.ClearParameteres();
			
			try
			{
				objWrapper.AddParameter("@CLAIM_ID",claimID); 
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
				
				if(ds!=null)
				{
					if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
						result = ds.Tables[0].Rows[0]["VOID_REVERSE_BUTTON_VISIBLE"].ToString();
				}
				objWrapper.ClearParameteres();

				return result;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
 
		#endregion

        #region CLAIM ACTIVITY
        // HERE WE ARE PASSING CLAIM ID IS SEPERATE PARAMETER BECUSE SOME TIME THIS FUNCTION CAN BE CALL OTHER THEN ACTIVITY PAGE
        public int VoidClaimActivity(ClsActivityInfo objActivityInfo, Int32 VoidActivityID,double Factor )
        {
             DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            int RetValue = 0;
            try
            {
                RetValue = Add(objActivityInfo);

                if (RetValue > 0)
                {

                    int ActivityID = objActivityInfo.ACTIVITY_ID;

                   
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                    objDataWrapper.ClearParameteres();

                    objDataWrapper.AddParameter("@CLAIM_ID", objActivityInfo.CLAIM_ID);
                    objDataWrapper.AddParameter("@ACTIVITY_ID", objActivityInfo.ACTIVITY_ID);
                    objDataWrapper.AddParameter("@ACTIVITY_REASON", objActivityInfo.ACTIVITY_REASON);
                    objDataWrapper.AddParameter("@ACTION_ON_PAYMENT", objActivityInfo.ACTION_ON_PAYMENT);                        
                    objDataWrapper.AddParameter("@VOID_ACTIVITY_ID", VoidActivityID);
                    objDataWrapper.AddParameter("@FACTOR", Factor);
                 
          
                    objDataWrapper.AddParameter("@MODIFIED_BY", objActivityInfo.MODIFIED_BY);
                    objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objActivityInfo.LAST_UPDATED_DATETIME);
                    
                    RetValue = objDataWrapper.ExecuteNonQuery("Proc_CreateSystemGeneratedClaimActivity");

                    //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                    //------------Added by Pradeep Kushwaha on 05-10-2011---To maintaining transaction log ----------//
                    
                    DataSet ds = new DataSet();
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.AddParameter("@CLAIM_ID", objActivityInfo.CLAIM_ID);
                    ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
                    string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
                    int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
                    int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
                    int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
                    
                    
                    if (TransactionLogRequired)
                    {
                        objActivityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddActivity.aspx.resx");
                        string strTranXML = objBuilder.GetTransactionLogXML(objActivityInfo);
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        objTransactionInfo.TRANS_TYPE_ID = 3;
                        objTransactionInfo.CLIENT_ID = customerID;
                        objTransactionInfo.POLICY_ID = policyID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = policyVersionId;
                        objTransactionInfo.RECORDED_BY = objActivityInfo.MODIFIED_BY;
                        objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("2080", "");
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1810", "") + " : " + claimNumber;

                        objDataWrapper.ExecuteNonQuery(objTransactionInfo);
                        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    }
                    //----------Added till here ---------------------------------------------------------//
                }
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            return RetValue;
        }


        public int UpdateClaimActivityReserve(int CreatedBy, DateTime CreatedDateTime,  int LangID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                int returnValue = this.UpdateClaimActivityReserve(objDataWrapper,  CreatedBy, CreatedDateTime,  LangID);
                if (returnValue > 0)
                {
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper = null;
            }
        }

        public int CloseClaim(int CreatedBy, DateTime CreatedDateTime, int LangID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                int returnValue = this.CloseClaim(objDataWrapper, CreatedBy, CreatedDateTime, LangID);
                if (returnValue > 0)
                {
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper = null;
            }


        }



        public int UpdateClaimActivityReserve(DataWrapper objDataWrapper, int CreatedBy, DateTime CreatedDateTime,int LangID)
        {

            string strStoredProc = "Proc_AutoReserveClaimActivity";
            DateTime RecordDate = DateTime.Now;

            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            try
            {
                objDataWrapper.ClearParameteres();
                
                
                objDataWrapper.AddParameter("@CREATED_BY", CreatedBy);                
                objDataWrapper.AddParameter("@CREATED_DATETIME", CreatedDateTime);
              
                objDataWrapper.AddParameter("@LANG_ID", LangID);

                int returnResult = 0;
               
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                objDataWrapper.ClearParameteres();
               

                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //				if(objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        public int CloseClaim(DataWrapper objDataWrapper,  int CreatedBy, DateTime CreatedDateTime, int LangID)
        {

            string strStoredProc = "Proc_CloseClaim";
            DateTime RecordDate = DateTime.Now;

            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            try
            {
                objDataWrapper.ClearParameteres();
              
                objDataWrapper.AddParameter("@CREATED_BY", CreatedBy);                
                objDataWrapper.AddParameter("@CREATED_DATETIME", CreatedDateTime);               
                objDataWrapper.AddParameter("@LANG_ID", LangID);

                int returnResult = 0;
               
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                objDataWrapper.ClearParameteres();
               

                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //				if(objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
       

#endregion
        
	}
	public enum enumTransactionLookup
	{
		RESERVE_UPDATE = 11773,
		EXPENSE_PAYMENT = 11774,
		CLAIM_PAYMENT = 11775,
		RECOVERY = 11776,
		REINSURANCE = 11777			

	}


   
}
