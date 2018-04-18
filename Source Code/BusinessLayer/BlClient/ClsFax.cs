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
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model;

namespace Cms.BusinessLayer.BlClient
{
	/// <summary>
	/// Summary description for ClsFax.
	/// </summary>
	/// 
	public class ClsFax : Cms.BusinessLayer.BlClient.ClsClient
	{
		private const string		CLT_CUSTOMER_FAX		= "CLT_CUSTOMER_FAX";
		private const string ACTIVATE_DEACTIVATE_PROC		= "Proc_ActivateDeactivateCustomer";
		private bool boolTransactionRequired				= true;
		private	bool boolTransactionLog;

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

		public ClsFax()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objFaxInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsFaxInfo objFaxInfo,int intCreatedBy)
		{
			string		strStoredProc	=	"Proc_InsertFaxDetails";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objFaxInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@FAX_NUMBER",objFaxInfo.FAX_NUMBER);
				objDataWrapper.AddParameter("@FAX_FROM_NAME",objFaxInfo.FAX_FROM_NAME);
				objDataWrapper.AddParameter("@FAX_FROM",objFaxInfo.FAX_FROM);
				objDataWrapper.AddParameter("@FAX_TO",objFaxInfo.FAX_TO);
				objDataWrapper.AddParameter("@FAX_RECIPIENTS",objFaxInfo.FAX_RECIPIENTS);
				objDataWrapper.AddParameter("@FAX_SUBJECT",objFaxInfo.FAX_SUBJECT);
				objDataWrapper.AddParameter("@FAX_REFERENCE",objFaxInfo.FAX_REFERENCE);
				objDataWrapper.AddParameter("@FAX_BODY",objFaxInfo.FAX_BODY);
				objDataWrapper.AddParameter("@FAX_ATTACH_PATH",objFaxInfo.FAX_ATTACH_PATH);
				objDataWrapper.AddParameter("@FAX_SEND_DATE",objFaxInfo.FAX_SEND_DATE);
				objDataWrapper.AddParameter("@FAX_RETURN_CODE",objFaxInfo.FAX_RETURN_CODE);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@Fax_Row_Id",SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objFaxInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\Aspx\Fax.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objFaxInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY	=	objFaxInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1713", "");// "Fax has been Send";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				int RowId  = int.Parse(objSqlParameter.Value.ToString());
				objFaxInfo.FAX_ROW_ID=RowId;
				objDataWrapper.ClearParameteres();

				ClsDiary objDiary = new ClsDiary();
				objDiary.TransactionLogRequired = true;		
	
				//Insert into Diary
				Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();

				objToDoListInfo.SUBJECTLINE		=	objFaxInfo.FAX_SUBJECT;
				objToDoListInfo.NOTE			=	objFaxInfo.FAX_BODY;
				objToDoListInfo.SYSTEMFOLLOWUPID = -1;
//				objToDoListInfo.STARTTIME		=	System.DateTime.Now;
//				objToDoListInfo.ENDTIME			=	System.DateTime.Now;
				objToDoListInfo.RECDATE			=	System.DateTime.Now;
				objToDoListInfo.TOUSERID		=	intCreatedBy;	
				objToDoListInfo.CREATED_DATETIME = DateTime.Now;
				objToDoListInfo.FOLLOWUPDATE	=	DateTime.Parse(System.DateTime.Now.AddDays(5).ToString());
				objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
				objToDoListInfo.CUSTOMER_ID		=	objFaxInfo.CUSTOMER_ID;
				objToDoListInfo.FROMUSERID		=	intCreatedBy;
				objToDoListInfo.LISTTYPEID		=	9 ; 	
				
				objDiary.Add(objToDoListInfo,objDataWrapper);
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

		
		public int AddNew(ClsFaxInfo objFaxInfo,int intCreatedBy)
		{
			string		strStoredProc	=	"Proc_InsertFaxDetails";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objFaxInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@FAX_NUMBER",objFaxInfo.FAX_NUMBER);
				objDataWrapper.AddParameter("@FAX_FROM_NAME",objFaxInfo.FAX_FROM_NAME);
				objDataWrapper.AddParameter("@FAX_FROM",objFaxInfo.FAX_FROM);
				objDataWrapper.AddParameter("@FAX_TO",objFaxInfo.FAX_TO);
				objDataWrapper.AddParameter("@FAX_RECIPIENTS",objFaxInfo.FAX_RECIPIENTS);
				objDataWrapper.AddParameter("@FAX_SUBJECT",objFaxInfo.FAX_SUBJECT);
				objDataWrapper.AddParameter("@FAX_REFERENCE",objFaxInfo.FAX_REFERENCE);
				objDataWrapper.AddParameter("@FAX_BODY",objFaxInfo.FAX_BODY);
				objDataWrapper.AddParameter("@FAX_ATTACH_PATH",objFaxInfo.FAX_ATTACH_PATH);
				objDataWrapper.AddParameter("@FAX_SEND_DATE",objFaxInfo.FAX_SEND_DATE);
				objDataWrapper.AddParameter("@FAX_RETURN_CODE",objFaxInfo.FAX_RETURN_CODE);
				objDataWrapper.AddParameter("@DIARY_ITEM_REQ",objFaxInfo.DIARY_ITEM_REQ);

				if(objFaxInfo.DIARY_ITEM_REQ == "Y")
                    objDataWrapper.AddParameter("@FOLLOW_UP_DATE",objFaxInfo.FOLLOW_UP_DATE);
				else
					objDataWrapper.AddParameter("@FOLLOW_UP_DATE",System.DBNull.Value);

				objDataWrapper.AddParameter("@DIARY_ITEM_TO",objFaxInfo.DIARY_ITEM_TO);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@Fax_Row_Id",SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objFaxInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\Aspx\Fax.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objFaxInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objFaxInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1714","")+" " + objFaxInfo.FAX_FROM_NAME + " "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1197","")+" "+ objFaxInfo.FAX_TO;//"Fax has been Send From " + objFaxInfo.FAX_FROM_NAME + " To " + objFaxInfo.FAX_TO;					
					objTransactionInfo.CLIENT_ID		=	objFaxInfo.CUSTOMER_ID;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";Recipients = " + objFaxInfo.FAX_RECIPIENTS + ";Subject = " + objFaxInfo.FAX_SUBJECT + ";Attachment = " + objFaxInfo.FAX_ATTACH_PATH;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				int RowId  = int.Parse(objSqlParameter.Value.ToString());
				objFaxInfo.FAX_ROW_ID=RowId;
				objDataWrapper.ClearParameteres();

				ClsDiary objDiary = new ClsDiary();
				objDiary.TransactionLogRequired = true;		
	
				//Insert into Diary
				if (objFaxInfo.DIARY_ITEM_REQ == "Y")
				{
					Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();

					objToDoListInfo.SUBJECTLINE		=	objFaxInfo.FAX_SUBJECT;
					objToDoListInfo.NOTE			=	objFaxInfo.FAX_BODY;				
					objToDoListInfo.RECDATE			=	System.DateTime.Now;				
					objToDoListInfo.CREATED_DATETIME =	DateTime.Now;				
					objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objToDoListInfo.CUSTOMER_ID		=	objFaxInfo.CUSTOMER_ID;
					objToDoListInfo.FROMUSERID		=	intCreatedBy;
					objToDoListInfo.LISTTYPEID		=	(int)ClsDiary.enumDiaryType.COMMUNICATION_FOLLOW_UPS;   
					objToDoListInfo.MODULE_ID		=	(int)ClsDiary.enumModuleMaster.CUSTOMER;  
					objToDoListInfo.LISTOPEN		=	"Y";

//					objToDoListInfo.STARTTIME = System.DateTime.Now;
//					objToDoListInfo.ENDTIME = System.DateTime.Now;
					objToDoListInfo.RECDATE = System.DateTime.Now;
					objToDoListInfo.SYSTEMFOLLOWUPID		= -1;				
					objToDoListInfo.FOLLOWUPDATE =  objFaxInfo.FOLLOW_UP_DATE;
					//DateTime.Parse(System.DateTime.Now.AddDays(5).ToString());					
					objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					
					objToDoListInfo.RECORDED_BY		=	objFaxInfo.CREATED_BY;	
					objToDoListInfo.TOUSERID   = objFaxInfo.DIARY_ITEM_TO;
					objToDoListInfo.PRIORITY   = "M";
					objDiary.Add(objToDoListInfo,objDataWrapper);
					
					//objDiary.DiaryEntryfromSetup(objToDoListInfo); 
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (returnResult!=-1)
					return RowId;
				else
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

		public static string GetFaxXml(int intFaxRecId, int intCustomerId)
		{
			string strProcedure = "Proc_GetFaxInfo";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@Customer_ID",intCustomerId);
				objDataWrapper.AddParameter("@RowId",intFaxRecId);
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
		}
		

		//		#region Update method
		//		/// <summary>
		//		/// Update method that recieves Model object to save.
		//		/// </summary>
		//		/// <param name="objOldFaxInfo">Model object having old information</param>
		//		/// <param name="objFaxInfo">Model object having new information(form control's value)</param>
		//		/// <returns>No. of rows updated (1 or 0)</returns>
		//		public int Update(ClsFaxInfo objOldFaxInfo,ClsFaxInfo objFaxInfo)
		//		{
		//			string strTranXML;
		//			string strStoredProc="Proc_UpdateFaxDetails";
		//			int returnResult = 0;
		//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//			try 
		//			{
		//				objDataWrapper.AddParameter("@Fax_Row_Id",objFaxInfo.FAX_ROW_ID);
		//				objDataWrapper.AddParameter("@CUSTOMER_ID",objFaxInfo.CUSTOMER_ID);
		//				objDataWrapper.AddParameter("@FAX_FROM_NAME",objFaxInfo.FAX_FROM_NAME);
		//				objDataWrapper.AddParameter("@FAX_FROM",objFaxInfo.FAX_FROM);
		//				objDataWrapper.AddParameter("@FAX_TO",objFaxInfo.FAX_TO);
		//				objDataWrapper.AddParameter("@FAX_RECIPIENTS",objFaxInfo.FAX_RECIPIENTS);
		//				objDataWrapper.AddParameter("@FAX_SUBJECT",objFaxInfo.FAX_SUBJECT);
		//				objDataWrapper.AddParameter("@FAX_MESSAGE",objFaxInfo.FAX_MESSAGE);
		//				objDataWrapper.AddParameter("@FAX_ATTACH_PATH",objFaxInfo.FAX_ATTACH_PATH);
		//				if(TransactionRequired) 
		//				{
		//					objFaxInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\Aspx\Fax.aspx.resx");
		//					string strUpdate = objBuilder.GetUpdateSQL(objOldFaxInfo,objFaxInfo,out strTranXML);
		//
		//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//					objTransactionInfo.TRANS_TYPE_ID	=	3;
		//					objTransactionInfo.RECORDED_BY		=	objFaxInfo.MODIFIED_BY;
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


