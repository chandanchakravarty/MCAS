/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	6/9/2005 11:57:41 AM
<End Date				: -	
<Description				: - 	Business logic class for Master Journal entry screen.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:16 June 2005 - 
<Modified By				: Gaurav- 
<Purpose				: Added function PrintPriview()- 
*******************************************************************************************/ 

using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
//using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Account;
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Business logic class for journal entry master screen.
	/// </summary>
	public class ClsJournalEntryMaster : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		private const	string		ACT_JOURNAL_MASTER			=	"ACT_JOURNAL_MASTER";
		private const string GET_TEMPLATE_PROC	= "Proc_GetTemplate";
		public const string TEMPLATE_PARENT_NODE = "TemplatesForCopy";


		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_JOURNAL_MASTER";
		#endregion

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
		
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsJournalEntryMaster()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objJournalEntryMasterInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsJournalEntryMasterInfo objJournalEntryMasterInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_JOURNAL_MASTER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				
				objDataWrapper.AddParameter("@JOURNAL_GROUP_TYPE",objJournalEntryMasterInfo.JOURNAL_GROUP_TYPE);
				objDataWrapper.AddParameter("@TRANS_DATE",objJournalEntryMasterInfo.TRANS_DATE);
				objDataWrapper.AddParameter("@JOURNAL_GROUP_CODE",objJournalEntryMasterInfo.JOURNAL_GROUP_CODE);
				objDataWrapper.AddParameter("@DESCRIPTION",objJournalEntryMasterInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@DIV_ID",objJournalEntryMasterInfo.DIV_ID);
				objDataWrapper.AddParameter("@DEPT_ID",objJournalEntryMasterInfo.DEPT_ID);
				objDataWrapper.AddParameter("@PC_ID",objJournalEntryMasterInfo.PC_ID);
				objDataWrapper.AddParameter("@GL_ID",objJournalEntryMasterInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objJournalEntryMasterInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@FREQUENCY",objJournalEntryMasterInfo.FREQUENCY);
				
				if (objJournalEntryMasterInfo.START_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@START_DATE",objJournalEntryMasterInfo.START_DATE);
				else
					objDataWrapper.AddParameter("@START_DATE",null);

				if (objJournalEntryMasterInfo.END_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@END_DATE",objJournalEntryMasterInfo.END_DATE);
				else
					objDataWrapper.AddParameter("@END_DATE",null);

				objDataWrapper.AddParameter("@DAY_OF_THE_WK",objJournalEntryMasterInfo.DAY_OF_THE_WK);
				
				if (objJournalEntryMasterInfo.LAST_PROCESSED_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@LAST_PROCESSED_DATE",objJournalEntryMasterInfo.LAST_PROCESSED_DATE);
				else
					objDataWrapper.AddParameter("@LAST_PROCESSED_DATE",null);

				objDataWrapper.AddParameter("@IS_COMMITED",objJournalEntryMasterInfo.IS_COMMITED);

				if (objJournalEntryMasterInfo.DATE_COMMITED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_COMMITED",objJournalEntryMasterInfo.DATE_COMMITED);
				else
					objDataWrapper.AddParameter("@DATE_COMMITED",null);

				objDataWrapper.AddParameter("@IMPORTSTATUS",objJournalEntryMasterInfo.IMPORTSTATUS);
				
				if (objJournalEntryMasterInfo.LAST_VALID_POSTING_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@LAST_VALID_POSTING_DATE",objJournalEntryMasterInfo.LAST_VALID_POSTING_DATE);
				else
					objDataWrapper.AddParameter("@LAST_VALID_POSTING_DATE",null);

				objDataWrapper.AddParameter("@NO_OF_RUN",objJournalEntryMasterInfo.NO_OF_RUN);
				objDataWrapper.AddParameter("@CREATED_BY",objJournalEntryMasterInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objJournalEntryMasterInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@TEMP_JE_NUM",objJournalEntryMasterInfo.TEMP_JE_NUM);
				
				SqlParameter objJENoSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@JOURNAL_ENTRY_NO",SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@JOURNAL_ID",SqlDbType.Int,ParameterDirection.Output);
				SqlParameter oSqlParamJENum  = (SqlParameter) objDataWrapper.AddParameter("@TMPVAR",SqlDbType.Int,ParameterDirection.Output);
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objJournalEntryMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddJournalEntryMaster.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objJournalEntryMasterInfo);
					if(objJournalEntryMasterInfo.JOURNAL_GROUP_TYPE == "RC")
						strTranXML = ProcessTransXml(strTranXML,objJournalEntryMasterInfo.FREQUENCY,objJournalEntryMasterInfo.DAY_OF_THE_WK);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objJournalEntryMasterInfo.CREATED_BY;
					//objTransactionInfo.TRANS_DESC		=	"New master Journal Entry detail  has been added.";
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1756", "");// "New Journal Entry added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
								
				objJournalEntryMasterInfo.JOURNAL_ID = int.Parse(objSqlParameter .Value.ToString());
				objJournalEntryMasterInfo.JOURNAL_ENTRY_NO = objJENoSqlParameter.Value.ToString();
				objJournalEntryMasterInfo.TEMP_JE_NUM = int.Parse(oSqlParamJENum.Value.ToString());

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
		/// <param name="objOldJournalEntryMasterInfo">Model object having old information</param>
		/// <param name="objJournalEntryMasterInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsJournalEntryMasterInfo objOldJournalEntryMasterInfo,ClsJournalEntryMasterInfo objJournalEntryMasterInfo)
		{
			string strStoredProc = "Proc_UpdateACT_JOURNAL_MASTER";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@JOURNAL_ENTRY_NO",objJournalEntryMasterInfo.JOURNAL_ENTRY_NO);
				objDataWrapper.AddParameter("@JOURNAL_ID",objJournalEntryMasterInfo.JOURNAL_ID);
				objDataWrapper.AddParameter("@JOURNAL_GROUP_TYPE",objJournalEntryMasterInfo.JOURNAL_GROUP_TYPE);
				objDataWrapper.AddParameter("@TRANS_DATE",objJournalEntryMasterInfo.TRANS_DATE);
				objDataWrapper.AddParameter("@JOURNAL_GROUP_CODE",objJournalEntryMasterInfo.JOURNAL_GROUP_CODE);
				objDataWrapper.AddParameter("@DESCRIPTION",objJournalEntryMasterInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@DIV_ID",objJournalEntryMasterInfo.DIV_ID);
				objDataWrapper.AddParameter("@DEPT_ID",objJournalEntryMasterInfo.DEPT_ID);
				objDataWrapper.AddParameter("@PC_ID",objJournalEntryMasterInfo.PC_ID);
				objDataWrapper.AddParameter("@GL_ID",objJournalEntryMasterInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objJournalEntryMasterInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@FREQUENCY",objJournalEntryMasterInfo.FREQUENCY);
				
				if (objJournalEntryMasterInfo.START_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@START_DATE",objJournalEntryMasterInfo.START_DATE);
				else
					objDataWrapper.AddParameter("@START_DATE",null);

				if (objJournalEntryMasterInfo.END_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@END_DATE",objJournalEntryMasterInfo.END_DATE);
				else
					objDataWrapper.AddParameter("@END_DATE",null);

				objDataWrapper.AddParameter("@DAY_OF_THE_WK",objJournalEntryMasterInfo.DAY_OF_THE_WK);

				if (objJournalEntryMasterInfo.LAST_PROCESSED_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@LAST_PROCESSED_DATE",objJournalEntryMasterInfo.LAST_PROCESSED_DATE);
				else
					objDataWrapper.AddParameter("@LAST_PROCESSED_DATE",null);

				objDataWrapper.AddParameter("@IS_COMMITED",objJournalEntryMasterInfo.IS_COMMITED);

				if (objJournalEntryMasterInfo.DATE_COMMITED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_COMMITED",objJournalEntryMasterInfo.DATE_COMMITED);
				else
					objDataWrapper.AddParameter("@DATE_COMMITED",null);
				
				objDataWrapper.AddParameter("@IMPORTSTATUS",objJournalEntryMasterInfo.IMPORTSTATUS);

				if (objJournalEntryMasterInfo.LAST_VALID_POSTING_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@LAST_VALID_POSTING_DATE",objJournalEntryMasterInfo.LAST_VALID_POSTING_DATE);
				else
					objDataWrapper.AddParameter("@LAST_VALID_POSTING_DATE",null);

				objDataWrapper.AddParameter("@NO_OF_RUN",objJournalEntryMasterInfo.NO_OF_RUN);
				objDataWrapper.AddParameter("@MODIFIED_BY",objJournalEntryMasterInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objJournalEntryMasterInfo.LAST_UPDATED_DATETIME);
				
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

				if(TransactionLogRequired) 
				{
					objJournalEntryMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddJournalEntryMaster.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldJournalEntryMasterInfo,objJournalEntryMasterInfo,out strTranXML);
					//added by uday to update the no of days & frequency					
					if(objJournalEntryMasterInfo.JOURNAL_GROUP_TYPE == "RC")
					strTranXML = ProcessTransXml(strTranXML,objJournalEntryMasterInfo.FREQUENCY,objJournalEntryMasterInfo.DAY_OF_THE_WK);
                    //
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objJournalEntryMasterInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1757", "");// "Journal Entry has been modified.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO = "Journal Entry # " + objJournalEntryMasterInfo.JOURNAL_ENTRY_NO.ToString();
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (int.Parse(objRetVal.Value.ToString()) == -2)
				{
					return -2;
				}
				else
				{
					return returnResult;
				}
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

		#region GetJournalEntryInfo
		/// <summary>
		/// Returns the data in the form of XML of specified journal id
		/// </summary>
		/// <param name="JournalId">Journal id whose data will be returned</param>
		/// <returns>XML of data</returns>
		public static string GetJournalEntryInfo(int intJournalId )
		{
			String strStoredProc = "Proc_GetACT_JOURNAL_MASTER";
			DataSet dsLocationInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@JOURNAL_ID",intJournalId);
				
				dsLocationInfo = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				if (dsLocationInfo.Tables[0].Rows.Count != 0)
				{
					return dsLocationInfo.GetXml();
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
		#endregion

		#region GetDepartment dropdown

		public static void GetDepartmentDropdown( DropDownList objDropDownList, int JournalId)
		{
			try
			{
				int intDivisionId;
				string strXML = GetJournalEntryInfo(JournalId);
				
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(strXML);
				System.Xml.XmlNode node = doc.SelectSingleNode("/NewDataSet/Table/DIV_ID");
				
				if (node != null)
				{
					if (node.InnerXml.Trim() != "")
					{
						intDivisionId = int.Parse(node.InnerXml);
						BlCommon.ClsDivision.GetDepartment(objDropDownList, intDivisionId,"Y");
					}
				}
				doc = null;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

		#region GetMaxEntryNo
		public static int GetMaxEntryNo()
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_GetACT_JOURNAL_MASTER_MAXENTRYNO";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@JOURNAL_ENTRY_NO", SqlDbType.Int, ParameterDirection.Output);
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				Value = int.Parse(objSqlParameter.Value.ToString());

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		
		#endregion
		#region GetMaxJID
		public static int GetMaxJID()
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_GetACT_JOURNAL_MASTER_MAXENTRY_JE_ID";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@JOURNAL_ID", SqlDbType.Int, ParameterDirection.Output);
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				Value = int.Parse(objSqlParameter.Value.ToString());

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

		
		#region copyTemplate
		public int copyTemplate(XmlDocument XmlDoc,int CreatedBy)
		{
			DataWrapper objDataWrapper=null;
			try
			{		
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsJournalEntryMaster.TEMPLATE_PARENT_NODE);
				if(xNode==null)
					return -1;				

				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				string EntryNumber = "";			
				string strNewJournalEntryNo = ClsJournalEntryMaster.GetMaxEntryNo().ToString();
				for (int i=0; i< xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@JOURNAL_ID",xNode.ChildNodes[i].SelectSingleNode("JOURNAL_ID").InnerText.ToString());
					objDataWrapper.AddParameter("@CREATED_BY",CreatedBy);
					SqlParameter objSqlParameter = (SqlParameter) objDataWrapper.AddParameter("@NEW_JOURNAL_ID", SqlDbType.Int, ParameterDirection.Output);
					SqlParameter objSqlParameterJeNo = (SqlParameter) objDataWrapper.AddParameter("@NEW_JOURNAL_ENTRY_NO", SqlDbType.Int, ParameterDirection.Output);
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_CopyACT_JOURNAL_MASTER");
					EntryNumber = EntryNumber + " , " + xNode.ChildNodes[i].SelectSingleNode("JOURNAL_ENTRY_NO").InnerText.ToString();
				}
				EntryNumber = EntryNumber.Trim().ToString().TrimStart(',');				
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	CreatedBy;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1758", "");// "Journal Entry has been copied.";
					objTransactionInfo.CUSTOM_INFO		=	";Journal Entry number " + strNewJournalEntryNo + " has been copied from Journal Entry number "+EntryNumber+"";
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				else
				{}	

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 1;
			}
			catch
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
            
				return 0;
			}
			finally
			{}
		}
			
		#endregion 


		#region GetTemplate
		public static DataTable GetTemplate()
		{
			DataSet dsTtmplate = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				dsTtmplate = objDataWrapper.ExecuteDataSet(GET_TEMPLATE_PROC);
				return dsTtmplate.Tables[0];
				
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

		#region Copy
		/// <summary>
		/// Copy the whole record of specified id and return the new JournalId
		/// </summary>
		/// <returns></returns>
		public int Copy(int intJournalId,ClsJournalEntryMasterInfo objJournalEntryMasterInfo,string strJournalNo)
		{
			try
			{
				String strStoredProc = "Proc_CopyACT_JOURNAL_MASTER";
				int Value,NEW_JOURNAL_ENTRY_NO;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@JOURNAL_ID", intJournalId);
				objDataWrapper.AddParameter("@CREATED_BY", objJournalEntryMasterInfo.CREATED_BY);
				SqlParameter objSqlParameter = (SqlParameter) objDataWrapper.AddParameter("@NEW_JOURNAL_ID", SqlDbType.Int, ParameterDirection.Output);
				SqlParameter objSqlParameterJeNo = (SqlParameter) objDataWrapper.AddParameter("@NEW_JOURNAL_ENTRY_NO", SqlDbType.Int, ParameterDirection.Output);

				if(TransactionLogRequired) 
				{					
					objJournalEntryMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/Aspx/AddJournalEntryMaster.aspx.resx");
					Value	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					// New Journal NO to be displayed on Screen
					NEW_JOURNAL_ENTRY_NO = int.Parse(objSqlParameterJeNo.Value.ToString());
					//objJournalEntryMasterInfo.JOURNAL_ID = NEW_JOURNAL_ID;
					objJournalEntryMasterInfo.JOURNAL_ENTRY_NO = NEW_JOURNAL_ENTRY_NO.ToString();
					string strTranXML = objBuilder.GetTransactionLogXML(objJournalEntryMasterInfo);

					
				//	SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				//	string strTranXML = objBuilder.GetTransactionLogXML(objJournalEntryMasterInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1758", "");// "Journal Entry has been copied.";
					objTransactionInfo.CUSTOM_INFO      =   "Copied from Journal Entry # " + strJournalNo;
					objTransactionInfo.RECORDED_BY		=	objJournalEntryMasterInfo.CREATED_BY;
					objTransactionInfo.CHANGE_XML		= strTranXML;
					//objTransactionInfo.CUSTOM_INFO		=	";Journal Id = " + intJournalId.ToString();
					//changes made uday to get copy centry in Trans log
					//Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
                   //
				}
				else//if no transaction required
				{
					Value	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				Value = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

		#region Delete
		/// <summary>
		/// Delete the whole record of spefied id 
		/// </summary>
		/// <returns>Nos of rows deleted</returns>
		public int Delete(int intJournalId, int UserId, string strJournalNo)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_DeleteACT_JOURNAL_MASTER";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@JOURNAL_ID", intJournalId);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY = UserId;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1759", "");// "Journal Entry has been deleted.";					
					objTransactionInfo.CUSTOM_INFO = "Journal Entry # " + strJournalNo;
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

				Value = int.Parse(objRetVal.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

		#region ChildRecordExists
		/// <summary>
		/// Checks whther child records of any journal exists or not
		/// </summary>
		/// <param name="JournalId">Journal id whose child record will be searched</param>
		/// <returns>True if child records exists else false</returns>
		public bool ChildRecordExists(int JournalId)
		{
			try
			{
				/*Calling the stored procedure to check*/
				String strSQL = "Proc_ExistsACT_JOURNAL_LINE_ITEMS";
				bool Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@JOURNAL_ID", JournalId);
				SqlParameter objSqlParameter = (SqlParameter) objDataWrapper.AddParameter("@EXISTS", SqlDbType.Bit, ParameterDirection.Output);

				objDataWrapper.ExecuteNonQuery(strSQL);
				
				Value = (bool)objSqlParameter.Value;
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
				
				return Value;
				
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

		#region GetGLAccountXML
		/// <summary>
		/// Retreivs the General ledger info of GL of the specfied journal
		/// </summary>
		/// <param name="JournalId">Journal id whose ledger's info will be fetched</param>
		/// <returns>XML of general ledger</returns>
		public static string GetGLAccountXML(int JournalId)
		{
			try
			{
				//For retreiving the general kedger id , first retreivin the journal information
				string strXML = GetJournalEntryInfo(JournalId);
				
				//Loading the xml returned by above function
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(strXML);
				
				//Retreiving the general ledger
				System.Xml.XmlNode nodeGl = doc.SelectSingleNode("/NewDataSet/Table/GL_ID");
				if ( nodeGl != null)
				{
					//Retreiving the fiscal id
					System.Xml.XmlNode nodeFiscal = doc.SelectSingleNode("/NewDataSet/Table/FISCAL_ID");
					if (nodeFiscal != null)
					{
						//Retreiving the xml from gl account table
						string strGL = nodeGl.InnerXml;
						string strRetVal = "";

						DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						objDataWrapper.AddParameter("@GL_ID", nodeGl.InnerText);
						objDataWrapper.AddParameter("@FISCAL_ID", nodeFiscal.InnerText);

						DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetACT_GENERAL_LEDGER_POSTING_INFO");

						if (ds.Tables[0].Rows.Count > 0)
						{
							strRetVal = ds.GetXml();
						}

						return strRetVal;
					}
				}
			
				return "";
			}
			catch (Exception objExp)
			{
				throw(objExp);
			}
		}
		#endregion

		#region Process TransXML
		public string ProcessTransXml(string strXml,string strFrequency,string strDayofWeek)
		{			
			XmlDocument tDoc = new XmlDocument();
			string strOuterXml = "";
			
			try
			{
				tDoc.LoadXml(strXml);
				#region Modify Frequency
				if(strFrequency!="")
				{
					//XmlNode frqNode = tDoc.SelectSingleNode("LabelFieldMapping/Map/@field='FREQUENCY'");
                    XmlNode frqNode = tDoc.SelectSingleNode("//Map[@field='FREQUENCY']");					
					if(frqNode!=null)
					{
						XmlAttribute attrNew = frqNode.Attributes["NewValue"];
						if(attrNew!=null)
						{
							switch(Convert.ToInt32(attrNew.InnerText.ToString().Trim()))
							{
								case 1:
									attrNew.InnerText = "Weekly";
									break;
								case 2:
									attrNew.InnerText = "Bi-Weekly";
									break;
								case 3:
									attrNew.InnerText = "Monthly";
									break;
								case 4:
									attrNew.InnerText = "Quarterly";
									break;
								case 5:
									attrNew.InnerText = "Semi-Annually";
									break;
								case 6:
									attrNew.InnerText = "Annually";
									break;
								default :
									break;
							}							

						}
						XmlAttribute attrOld = frqNode.Attributes["OldValue"];
						if(attrOld!=null)
						{
							if(attrOld.InnerText.ToString()!="")
							{
								switch(Convert.ToInt32(attrOld.InnerText.ToString().Trim()))
								{
									case 1:
										attrOld.InnerText = "Weekly";
										break;
									case 2:
										attrOld.InnerText = "Bi-Weekly";
										break;
									case 3:
										attrOld.InnerText = "Monthly";
										break;
									case 4:
										attrOld.InnerText = "Quarterly";
										break;
									case 5:
										attrOld.InnerText = "Semi-Annually";
										break;
									case 6:
										attrOld.InnerText = "Annually";
										break;
									default :
										break;
								}
							}
						}
					}
				}
				#endregion				
				#region Modify DaysWeek
				if(strDayofWeek!="")
				{
					//XmlNode dofwkNode = tDoc.SelectSingleNode("LabelFieldMapping/Map/@field='DAY_OF_THE_WK'");
					XmlNode dofwkNode = tDoc.SelectSingleNode("//Map[@field='DAY_OF_THE_WK']");					
					if(dofwkNode!=null)
					{
						XmlAttribute attrNewdofW = dofwkNode.Attributes["NewValue"];
						if(attrNewdofW!=null)
						{
							if(strFrequency == "1")
							{
								switch(Convert.ToInt32(attrNewdofW.InnerText.ToString().Trim()))
								{
									case 1:
										attrNewdofW.InnerText = "Sunday";
										break;
									case 2:
										attrNewdofW.InnerText = "Monday";
										break;
									case 3:
										attrNewdofW.InnerText = "Tuesday";
										break;
									case 4:
										attrNewdofW.InnerText = "Wednesday";
										break;
									case 5:
										attrNewdofW.InnerText = "Thrusday";
										break;
									case 6:
										attrNewdofW.InnerText = "Friday";
										break;
									case 7:
										attrNewdofW.InnerText = "Saturday";
										break;
									default :
										break;
								}
							}
							else
								attrNewdofW.InnerText = "";

							

						}
						XmlAttribute attrdofWOld = dofwkNode.Attributes["OldValue"];
						if(attrdofWOld!=null)
						{   //changed by uday
							if(attrdofWOld.InnerText.ToString()!="" || attrdofWOld.InnerText.ToString()== null)
							{
								if(strFrequency == "1")  //If frequency is weekly then show Days TL.
								{
									switch(Convert.ToInt32(attrdofWOld.InnerText.ToString().Trim()))
									{
										case 1:
											attrdofWOld.InnerText = "Sunday";
											break;
										case 2:
											attrdofWOld.InnerText = "Monday";
											break;
										case 3:
											attrdofWOld.InnerText = "Tuesday";
											break;
										case 4:
											attrdofWOld.InnerText = "Wednesday";
											break;
										case 5:
											attrdofWOld.InnerText = "Thrusday";
											break;
										case 6:
											attrdofWOld.InnerText = "Friday";
											break;
										case 7:
											attrdofWOld.InnerText = "Saturday";
											break;
										default :
											break;
									}
								}
								else
									attrdofWOld.InnerText = "";

							}
						}
					}
				}
				#endregion
				if(!tDoc.SelectSingleNode("//LabelFieldMapping").HasChildNodes)
					strOuterXml = "";
				else
					strOuterXml = tDoc.OuterXml;

				
                 return strOuterXml;
			}
         
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#endregion

		//*************
		//Fetch Account on basis of Transaction code passed.
		public static string GetAccountXML(string TranCode)
		{
			try
			{
				string strRetVal="";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@TRAN_CODE",TranCode);
				DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetDefaultAccountForTranCode");
				if (ds.Tables[0].Rows.Count > 0)
				{
					strRetVal = ds.GetXml();
				}

				return strRetVal;
			}
			catch (Exception objExp)
			{
				throw(objExp);
			}
		}

		//*************

		#region PrintPreview
		/// <summary>
		/// Returns the data to be used for showing the print preview
		/// </summary>
		/// <param name="JournalId">Journal id whose line items will be returned</param>
		/// <returns>Dataset containing the data</returns>
		public DataSet PrintPriview(int intJournal_Id)
		{
			string strProc = "Proc_GetJournalEntry";
			DataSet objDataSet = new DataSet();
			
			try
			{
				objDataSet = DataWrapper.ExecuteDataset(ConnStr,strProc,intJournal_Id);

				if(objDataSet.Tables[0].Rows.Count >0)
					return objDataSet;
				else
					return null;
				
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				objDataSet.Dispose();
			}

		}
		#endregion

		#region Commit
		/// <summary>
		/// Commits the specifed journal entry
		/// </summary>
		/// <param name="JournalId">Journal id to commit</param>
		/// <returns>True if sucessfull else false</returns>
		public int Commit(ClsJournalEntryMasterInfo objInfo)
		{
			int JournalId = objInfo.JOURNAL_ID;
			int CommitedBy = objInfo.MODIFIED_BY;
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_CommitACT_JOURNAL_MASTER";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@JOURNAL_ID", JournalId);
				objDataWrapper.AddParameter("@COMMITED_BY", CommitedBy);
				objDataWrapper.AddParameter("@PARAM1", null);
				objDataWrapper.AddParameter("@PARAM2", null);
				objDataWrapper.AddParameter("@PARAM3", null);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				
				if(base.TransactionLogRequired) 
				{
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/aspx/AddJournalEntryMaster.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1760", "");// "Journal Entry Has Been Committed";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				Value = int.Parse(objRetVal.Value.ToString());

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
				
				return Value;				
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

		
		#region Fill Frquency DropDown

		/// <summary>
		/// Fill the Frequency Drop Down List
		/// </summary>
		/// <param name="objDdlFrequency"></param>
		public void FillFrequency(DropDownList objDdlFrequency)
		{
			DataSet dsTemp = GetFrequecy();
			
			if (dsTemp != null)
			{
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count++)
					{
						objDdlFrequency.Items.Insert(count,dsTemp.Tables[0].Rows[count]["FREQUENCY_DESCRIPTION"].ToString());
						objDdlFrequency.Items[count].Value = dsTemp.Tables[0].Rows[count]["FREQUENCY_CODE"].ToString();
					}
				}
			
			}
		}



		/// <summary>
		/// Get the Frequency Details
		/// </summary>
		/// <returns></returns>
		private DataSet GetFrequecy()
		{
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				DataSet dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetFrquency");

				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					return dsTemp;
				}
				else
					return dsTemp = null;
			}
			catch (Exception objExp)
			{
				throw(objExp);
			}
		}
		#endregion

		public void ActivateDeactivateJournal(string strCode,string isActive,string journalno)
		{			
			
			DataWrapper objDataWrapper	=	new DataWrapper( ConnStr, CommandType.StoredProcedure);
			string strActivateDeactivateProcedure="Proc_ActivateDeactivateJournalRecord";
			try
			{
				objDataWrapper.AddParameter("@CODE",strCode);
				objDataWrapper.AddParameter("@IS_ACTIVE",isActive);				
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID		=	3;
					if(isActive.Equals("Y"))
					{
						objTransactionInfo.RECORDED_BY =  Convert.ToInt32(System.Web.HttpContext.Current.Session["userid"].ToString().Trim());
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1761", "");// "Journal Entry Detail has been activated.";
					}
					else
					{
						objTransactionInfo.RECORDED_BY =  Convert.ToInt32(System.Web.HttpContext.Current.Session["userid"].ToString().Trim());
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1762", "");// "Journal Entry Detail has been deactivated.";
						
					}					
				//	objTransactionInfo.CUSTOM_INFO			=	strCode;
					objTransactionInfo.CUSTOM_INFO          =   "Journal Entry # " + journalno.ToString();
					objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure, objTransactionInfo);
					
				}
				else
					objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure);

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}	
		}

		#region Methods to be called from EOD process
		public int ProcessRecurringJEs(int UserId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,
				DataWrapper.SetAutoCommit.OFF);

			try
			{
				String strStoredProc = "Proc_ProcessAutoRecurJournalEntries";
				
				objDataWrapper.AddParameter("@USER_ID", UserId );
				int RetVal = objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return RetVal;

			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
		}

		public int PostRecurringJEs(int JournalID,int DayOfWeek,int Frequency,DateTime StartDate,
			DateTime EndDate, DateTime LastValidPostingDate, int UserId, string strJournalNo)
		{
			try
			{
				String strStoredProc = "Proc_PostAutoRecurJournalEntries";
				int Value;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@FREQUENCY", Frequency);
				if(EndDate.Ticks != 0)
					objDataWrapper.AddParameter("@END_DATE", EndDate);
				else
					objDataWrapper.AddParameter("@END_DATE",null);
				if(LastValidPostingDate.Ticks != 0)
					objDataWrapper.AddParameter("@LAST_VALID_POSTING_DATE", LastValidPostingDate);
				else
					objDataWrapper.AddParameter("@LAST_VALID_POSTING_DATE",null);
				objDataWrapper.AddParameter("@DAY_OF_WK", DayOfWeek);
				objDataWrapper.AddParameter("@JOURNAL_ID ", JournalID);
				if(StartDate.Ticks !=0 )
					objDataWrapper.AddParameter("@START_DATE", StartDate);
				else
					objDataWrapper.AddParameter("@START_DATE",null);
				objDataWrapper.AddParameter("@USER_ID", UserId );
				SqlParameter objSqlParameter = (SqlParameter) objDataWrapper.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.Output);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		= UserId;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1763", "");// "Recurring Journal Entries have been posted.";
					objTransactionInfo.CUSTOM_INFO		=	"Journal Entry # " + strJournalNo;
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

				Value = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#endregion 

		public bool HavingLineItems(int JournalID)
		{

			try
			{
				String strStoredProc = "Proc_GetNumberOfLineItems";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@JOURNAL_ID", JournalID);
				SqlParameter objSqlParameter = (SqlParameter) objDataWrapper.AddParameter("@NUMBER_OF_ITEMS", SqlDbType.Int, ParameterDirection.Output);

				objDataWrapper.ExecuteNonQuery(strStoredProc);
				Value = int.Parse(objSqlParameter.Value.ToString());

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				if (Value  > 0)
					return true;
				else
					return false;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		
		}

	}
}
