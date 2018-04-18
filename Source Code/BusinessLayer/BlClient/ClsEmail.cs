/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	6/29/2005 12:02:40 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Client;
using System.Data.SqlClient;
//using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model;

namespace Cms.BusinessLayer.BlClient
{
	/// <summary>
	/// Summary description for ClsEmail.
	/// </summary>
	public class ClsEmail : Cms.BusinessLayer.BlClient.ClsClient
	{
		private const	string		CLT_CUSTOMER_EMAIL			=	"CLT_CUSTOMER_EMAIL";
		private			bool		boolTransactionLog;
		private bool boolTransactionRequired			= true;
		//private int _;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateCustomer";
		

		#region Public Properties
		public bool TransactionLog
		{
			set
			{
				boolTransactionLog	=	value;
			}
			get
			{
				return boolTransactionLog;
			}
		}
		#endregion

		public bool TransactionRequired
		{
			get
			{
				return boolTransactionRequired;
			}
			set
			{
				boolTransactionRequired=value;
			}
		}

		#region private Utility Functions
		#endregion
		public ClsEmail()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objEmailInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsEmailInfo objEmailInfo,int intCreatedBy)
		{
			string		strStoredProc	=	"Proc_InsertEmailDetails";
			//DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				//objDataWrapper.AddParameter("@EMAIL_ROW_ID",objEmailInfo.EMAIL_ROW_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objEmailInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@EMAIL_FROM_NAME",objEmailInfo.EMAIL_FROM_NAME);
				objDataWrapper.AddParameter("@EMAIL_FROM",objEmailInfo.EMAIL_FROM);
				objDataWrapper.AddParameter("@EMAIL_TO",objEmailInfo.EMAIL_TO);
				objDataWrapper.AddParameter("@EMAIL_RECIPIENTS",objEmailInfo.EMAIL_RECIPIENTS);
				objDataWrapper.AddParameter("@EMAIL_SUBJECT",objEmailInfo.EMAIL_SUBJECT);
				objDataWrapper.AddParameter("@EMAIL_MESSAGE",objEmailInfo.EMAIL_MESSAGE);
				objDataWrapper.AddParameter("@EMAIL_ATTACH_PATH",objEmailInfo.EMAIL_ATTACH_PATH);
				objDataWrapper.AddParameter("@EMAIL_SEND_DATE",objEmailInfo.EMAIL_SEND_DATE);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@Email_Row_Id",SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objEmailInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\Aspx\Email.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objEmailInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objEmailInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1707", "");// "Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				int RowId  = int.Parse(objSqlParameter.Value.ToString());
				objEmailInfo.EMAIL_ROW_ID=RowId;
				objDataWrapper.ClearParameteres();
				if (objEmailInfo.DIARY_ITEM_REQ =="Y")
				{
					ClsDiary objDiary = new ClsDiary();
					objDiary.TransactionLogRequired = true;		
		
					//Insert into Diary
					Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();

					objToDoListInfo.NOTE=objEmailInfo.EMAIL_MESSAGE;
					objToDoListInfo.CREATED_DATETIME = DateTime.Now;
					objToDoListInfo.CUSTOMER_ID= objEmailInfo.CUSTOMER_ID;
					objToDoListInfo.FROMUSERID =intCreatedBy;
					objToDoListInfo.LISTTYPEID =(int)ClsDiary.enumDiaryType.COMMUNICATION_FOLLOW_UPS; 
					objToDoListInfo.MODULE_ID = (int)ClsDiary.enumModuleMaster.CUSTOMER;  
					objToDoListInfo.SYSTEMFOLLOWUPID		= -1;
					objToDoListInfo.LISTOPEN ="Y";

					objDiary.DiaryEntryfromSetup(objToDoListInfo); 
		
					///Commented by Anurag Verma on 12-03-2006 for checking new diary object
					#region OLD CODE BEFORE DIARY OBJECT---DO NOT DELETE
//					objToDoListInfo.SUBJECTLINE=objEmailInfo.EMAIL_SUBJECT;
//					objToDoListInfo.STARTTIME = System.DateTime.Now;
//					objToDoListInfo.ENDTIME = System.DateTime.Now;
//					objToDoListInfo.RECDATE = System.DateTime.Now;
//					objToDoListInfo.TOUSERID=intCreatedBy;						
//					objToDoListInfo.FOLLOWUPDATE = objEmailInfo.FOLLOW_UP_DATE; 
//						//DateTime.Parse(System.DateTime.Now.AddDays(5).ToString());
//					objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;					
//					objDiary.Add(objToDoListInfo,objDataWrapper);
					#endregion


				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if ( == -1)
//				{
//					return -1;
//				}
//				else
//				{
//					objEmailInfo. = ;
//						return returnResult;
//				}
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
		public int AddNew(ClsEmailInfo objEmailInfo,int intCreatedBy,int intPOLICY_ID,int intPOLICY_VERSION_ID,int intAPP_ID)
		{
			return AddNew( objEmailInfo,intCreatedBy,intPOLICY_ID,intPOLICY_VERSION_ID, intAPP_ID,0,0,0);
		}
		
		public static DataSet GetClaimDetails(int ClaimID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CLAIM_ID",ClaimID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetClaimDetails");
				return dsTemp;
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		public static DataSet GetPolicyAppNumber(int PolicyID, int CustID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyAppNumber");
				return dsTemp;
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		public static DataSet GetQQAppNumber(int AppID, int QQID, int CustID, string strCalledFor)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustID,SqlDbType.Int);
				objDataWrapper.AddParameter("@QQ_ID",QQID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CALLED_FOR",strCalledFor,SqlDbType.VarChar);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppNumber");
				return dsTemp;
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		public int AddNew(ClsEmailInfo objEmailInfo,int intCreatedBy,int intPOLICY_ID,int intPOLICY_VERSION_ID,int intAPP_ID,int intAPP_VERSION_ID, int intQUOTE_ID,int intCLAIM_ID)
		{
			string		strStoredProc	=	"Proc_InsertEmailDetails";
			//DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				//objDataWrapper.AddParameter("@EMAIL_ROW_ID",objEmailInfo.EMAIL_ROW_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objEmailInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@EMAIL_FROM_NAME",objEmailInfo.EMAIL_FROM_NAME);
				objDataWrapper.AddParameter("@EMAIL_FROM",objEmailInfo.EMAIL_FROM);
				objDataWrapper.AddParameter("@EMAIL_TO",objEmailInfo.EMAIL_TO);
				objDataWrapper.AddParameter("@EMAIL_RECIPIENTS",objEmailInfo.EMAIL_RECIPIENTS);
				objDataWrapper.AddParameter("@EMAIL_SUBJECT",objEmailInfo.EMAIL_SUBJECT);
				objDataWrapper.AddParameter("@EMAIL_MESSAGE",objEmailInfo.EMAIL_MESSAGE);
				objDataWrapper.AddParameter("@EMAIL_ATTACH_PATH",objEmailInfo.EMAIL_ATTACH_PATH);
				objDataWrapper.AddParameter("@EMAIL_SEND_DATE",objEmailInfo.EMAIL_SEND_DATE);
				objDataWrapper.AddParameter("@CREATED_BY",intCreatedBy);
				objDataWrapper.AddParameter("@DIARY_ITEM_REQ",objEmailInfo.DIARY_ITEM_REQ);
				objDataWrapper.AddParameter("@POLICY_NUMBER",objEmailInfo.POLICY_NUMBER);
				objDataWrapper.AddParameter("@CLAIM_NUMBER",objEmailInfo.CLAIM_NUMBER);
				objDataWrapper.AddParameter("@APP_NUMBER ",objEmailInfo.APP_NUMBER );
				objDataWrapper.AddParameter("@QUOTE",objEmailInfo.QUOTE);
				if (objEmailInfo.DIARY_ITEM_REQ == "Y")
				{
					objDataWrapper.AddParameter("@FOLLOW_UP_DATE",objEmailInfo.FOLLOW_UP_DATE);
					objDataWrapper.AddParameter("@DIARY_ITEM_TO",objEmailInfo.DIARY_ITEM_TO);
				}
				objDataWrapper.AddParameter("@POLICY_ID",intPOLICY_ID);
				objDataWrapper.AddParameter("@APP_ID", intAPP_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPOLICY_VERSION_ID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@Email_Row_Id",SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objEmailInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\Aspx\Email.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objEmailInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objEmailInfo.CREATED_BY;
					//objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.TRANS_DESC		=	"Email has been Sent From " + objEmailInfo.EMAIL_FROM_NAME + " To " + objEmailInfo.EMAIL_RECIPIENTS;					
					objTransactionInfo.APP_ID			=	intAPP_ID;
					objTransactionInfo.POLICY_ID		=	intPOLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	intPOLICY_VERSION_ID;
					objTransactionInfo.APP_VERSION_ID	=	intAPP_VERSION_ID;
					objTransactionInfo.QUOTE_ID			=	intQUOTE_ID;
					objTransactionInfo.QUOTE_VERSION_ID	=	1;
					objTransactionInfo.CLIENT_ID		=	objEmailInfo.CUSTOMER_ID;
					objTransactionInfo.CHANGE_XML		=	strTranXML;					
					//changes made by uday to hide attachment text, if file is not attached
					objTransactionInfo.CUSTOM_INFO		=	";Recipients = " + objEmailInfo.EMAIL_RECIPIENTS + ";Subject = " + objEmailInfo.EMAIL_SUBJECT;
					if(objEmailInfo.EMAIL_ATTACH_PATH!="")
					{
						objTransactionInfo.CUSTOM_INFO+=";Attachment = " + objEmailInfo.EMAIL_ATTACH_PATH;
					}
					//
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				int RowId  = int.Parse(objSqlParameter.Value.ToString());
				objEmailInfo.EMAIL_ROW_ID=RowId;
				objDataWrapper.ClearParameteres();
				if (objEmailInfo.DIARY_ITEM_REQ =="Y")
				{
					ClsDiary objDiary = new ClsDiary();
					objDiary.TransactionLogRequired = true;		
		
					//Insert into Diary 
					
					#region DIARY SETUP 
  //Commented by Swarup on 10-01-2008 Itrack #3023
					/*	Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();

						objToDoListInfo.NOTE=objEmailInfo.EMAIL_MESSAGE;
						objToDoListInfo.CREATED_DATETIME = DateTime.Now;
						objToDoListInfo.CUSTOMER_ID= objEmailInfo.CUSTOMER_ID;
						objToDoListInfo.FROMUSERID =intCreatedBy;
						objToDoListInfo.LISTTYPEID =(int)ClsDiary.enumDiaryType.EMAIL;
						objToDoListInfo.MODULE_ID = (int)ClsDiary.enumModuleMaster.CUSTOMER;
						objToDoListInfo.LISTOPEN ="Y";
						objToDoListInfo.POLICY_ID =intPOLICY_ID;
						objToDoListInfo.POLICY_VERSION_ID =intPOLICY_VERSION_ID;
						objToDoListInfo.APP_ID =intAPP_ID;
						objToDoListInfo.CREATED_BY = objEmailInfo.CREATED_BY;
						//Commented by Anurag Verma on 20/03/2007 as these properties are removed
						//objToDoListInfo.POLICYCLIENTID = objEmailInfo.CUSTOMER_ID;
						//objToDoListInfo.POLICYID =intPOLICY_ID;
						//objToDoListInfo.POLICYVERSION  =intPOLICY_VERSION_ID;

						objDiary.DiaryEntryfromSetup(objToDoListInfo); */
					
					//Insert into Diary
					if (objEmailInfo.DIARY_ITEM_REQ == "Y")
					{
						Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();

						objToDoListInfo.SUBJECTLINE		=	objEmailInfo.EMAIL_SUBJECT;
						objToDoListInfo.NOTE			=	objEmailInfo.EMAIL_MESSAGE;				
						objToDoListInfo.RECDATE			=	System.DateTime.Now;				
						objToDoListInfo.CREATED_DATETIME =	DateTime.Now;				
						objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
						objToDoListInfo.CUSTOMER_ID		=	objEmailInfo.CUSTOMER_ID;
						objToDoListInfo.FROMUSERID		=	intCreatedBy;
						objToDoListInfo.LISTTYPEID		=	(int)ClsDiary.enumDiaryType.COMMUNICATION_FOLLOW_UPS;   
						objToDoListInfo.MODULE_ID		=	(int)ClsDiary.enumModuleMaster.CUSTOMER;  
						objToDoListInfo.APP_ID			=	intAPP_ID;
						objToDoListInfo.POLICY_ID		=	intPOLICY_ID;
						objToDoListInfo.POLICY_VERSION_ID	=	intPOLICY_VERSION_ID;
						objToDoListInfo.APP_VERSION_ID	=	intAPP_VERSION_ID;
						objToDoListInfo.QUOTEID			=	intQUOTE_ID;
						objToDoListInfo.CLAIMID			=	intCLAIM_ID;

						objToDoListInfo.LISTOPEN		=	"Y";
						objToDoListInfo.SYSTEMFOLLOWUPID = -1;
//						objToDoListInfo.STARTTIME = System.DateTime.Now;
//						objToDoListInfo.ENDTIME = System.DateTime.Now;
						objToDoListInfo.RECDATE = System.DateTime.Now;
									
						objToDoListInfo.FOLLOWUPDATE =  objEmailInfo.FOLLOW_UP_DATE;
						//DateTime.Parse(System.DateTime.Now.AddDays(5).ToString());					
						objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					
						objToDoListInfo.RECORDED_BY		=	objEmailInfo.CREATED_BY;
						objToDoListInfo.TOUSERID   = objEmailInfo.DIARY_ITEM_TO;
						objToDoListInfo.PRIORITY   = "M";
						objDiary.Add(objToDoListInfo,objDataWrapper);
					
						//objDiary.DiaryEntryfromSetup(objToDoListInfo); 
					}
	#endregion

					///Commented by Anurag Verma on 12-03-2006 for checking new diary object
					#region OLD CODE BEFORE DIARY OBJECT---DO NOT DELETE
					//objToDoListInfo.SUBJECTLINE=objEmailInfo.EMAIL_SUBJECT;
					//objToDoListInfo.NOTE=objEmailInfo.EMAIL_MESSAGE;
					//objToDoListInfo.STARTTIME = System.DateTime.Now;
					//objToDoListInfo.ENDTIME = System.DateTime.Now;
					//objToDoListInfo.RECDATE = System.DateTime.Now;
					//objToDoListInfo.TOUSERID=intCreatedBy;	
					//objToDoListInfo.CREATED_DATETIME = DateTime.Now;
					//objToDoListInfo.FOLLOWUPDATE = objEmailInfo.FOLLOW_UP_DATE;  
						//DateTime.Parse(System.DateTime.Now.AddDays(5).ToString());
					//objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//objToDoListInfo.CUSTOMER_ID= objEmailInfo.CUSTOMER_ID;
					//objToDoListInfo.FROMUSERID =intCreatedBy;
					//objToDoListInfo.LISTTYPEID =9 ; 
					//objDiary.Add(objToDoListInfo,objDataWrapper);
					#endregion
				}
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (returnResult!=-1)
					return RowId;
				else
					return returnResult;
				//				if ( == -1)
				//				{
				//					return -1;
				//				}
				//				else
				//				{
				//					objEmailInfo. = ;
				//						return returnResult;
				//				}
				//return returnResult;
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

		public static string  GetEmailXml(int intEmailRecId, int intCustomerId)
		{
			string strProcedure = "Proc_GetEmailInfo";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@Customer_ID",intCustomerId);
				objDataWrapper.AddParameter("@RowId",intEmailRecId);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);

				if(objDataSet.Tables[0].Rows.Count>0)
				{
					return objDataSet.GetXml();
				}
				else
				{
					return "";
				}
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
			//<Gaurav> 31 May 2005 END: InLine Query Changes to Stroded Proc

			
		}
		

//		#region Update method
//		/// <summary>
//		/// Update method that recieves Model object to save.
//		/// </summary>
//		/// <param name="objOldEmailInfo">Model object having old information</param>
//		/// <param name="objEmailInfo">Model object having new information(form control's value)</param>
//		/// <returns>No. of rows updated (1 or 0)</returns>
//		public int Update(ClsEmailInfo objOldEmailInfo,ClsEmailInfo objEmailInfo)
//		{
//			string strTranXML;
//			string strStoredProc="Proc_UpdateEmailDetails";
//			int returnResult = 0;
//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//			try 
//			{
//				objDataWrapper.AddParameter("@Email_Row_Id",objEmailInfo.EMAIL_ROW_ID);
//				objDataWrapper.AddParameter("@CUSTOMER_ID",objEmailInfo.CUSTOMER_ID);
//				objDataWrapper.AddParameter("@EMAIL_FROM_NAME",objEmailInfo.EMAIL_FROM_NAME);
//				objDataWrapper.AddParameter("@EMAIL_FROM",objEmailInfo.EMAIL_FROM);
//				objDataWrapper.AddParameter("@EMAIL_TO",objEmailInfo.EMAIL_TO);
//				objDataWrapper.AddParameter("@EMAIL_RECIPIENTS",objEmailInfo.EMAIL_RECIPIENTS);
//				objDataWrapper.AddParameter("@EMAIL_SUBJECT",objEmailInfo.EMAIL_SUBJECT);
//				objDataWrapper.AddParameter("@EMAIL_MESSAGE",objEmailInfo.EMAIL_MESSAGE);
//				objDataWrapper.AddParameter("@EMAIL_ATTACH_PATH",objEmailInfo.EMAIL_ATTACH_PATH);
//				if(TransactionRequired) 
//				{
//					objEmailInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\Aspx\Email.aspx.resx");
//					string strUpdate = objBuilder.GetUpdateSQL(objOldEmailInfo,objEmailInfo,out strTranXML);
//
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	3;
//					objTransactionInfo.RECORDED_BY		=	objEmailInfo.MODIFIED_BY;
//					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
//					objTransactionInfo.CHANGE_XML		=	strTranXML;
//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
//
//				}
//				else
//				{
//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
//				}
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				return returnResult;
//			}
//			catch(Exception ex)
//			{
//				throw(ex);
//			}
//			finally
//			{
//				if(objDataWrapper != null) 
//				{
//					objDataWrapper.Dispose();
//				}
//				if(objBuilder != null) 
//				{
//					objBuilder = null;
//				}
//			}
//		}
//		#endregion

		
	}
}


