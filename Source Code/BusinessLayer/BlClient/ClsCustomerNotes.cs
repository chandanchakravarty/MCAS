/******************************************************************************************
<Author					: -   Ashwani Kumar
<Start Date				: -	4/25/2005 7:35:34 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Client;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model;

namespace Cms.BusinessLayer.BlClient
{
	/// <summary>
	/// 
	/// </summary>
	public class clsCustomerNotes : Cms.BusinessLayer.BlClient.ClsClient
	{
		private const	string		CLT_CUSTOMER_NOTES			=	"CLT_CUSTOMER_NOTES";
		private const	string		GET_CUSTOMER_NOTES_PROC	=	"Proc_GetCustomerNotes";
		public const string PinkSlipNotesTypeID = "11932";
		
		#region Private Instance Variables
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateCLT_CUSTOMER_NOTES";
		private	bool boolTransactionLog;
		//private int NOTES_ID;		
		private SqlParameter objSqlParameter ;
		
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

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public clsCustomerNotes()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objCustomerNotesInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/// 
		public int  Add(Cms.Model.Client.ClsCustomerNotesInfo objCustomerNotesInfo)
		{
			return Add(objCustomerNotesInfo,"");
		}
		public int  Add(Cms.Model.Client.ClsCustomerNotesInfo objCustomerNotesInfo,string strVersion)
		{
			string	strStoredProc = "Proc_InsertCustomerNotes";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				AddParam(objDataWrapper,objCustomerNotesInfo); 
				objDataWrapper.AddParameter("@CREATED_BY",objCustomerNotesInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objCustomerNotesInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objCustomerNotesInfo.LAST_UPDATED_DATETIME);
				
				//TO_FOLLOWUP_ID(TOUSERID)
				objDataWrapper.AddParameter("@TO_FOLLOWUP_ID",objCustomerNotesInfo.TO_FOLLOWUP_ID);


				objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@NOTES_ID",SqlDbType.Int,ParameterDirection.Output);	
 
				int returnResult = 0;				
				if(TransactionLogRequired)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objCustomerNotesInfo.TransactLabel = ClsCommon.MapTransactionLabel("Client/Aspx/AddCustomerNotes.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objCustomerNotesInfo);
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objCustomerNotesInfo.CREATED_BY;
					objTransactionInfo.CLIENT_ID		=	objCustomerNotesInfo.CUSTOMER_ID;				
					
					   
					//objTransactionInfo.POLICY_ID        =   objCustomerNotesInfo.POLICY_ID;
					//objTransactionInfo.POLICY_VER_TRACKING_ID = objCustomerNotesInfo.POLICY_VER_TRACKING_ID; 
					//Condition added For Itrack issue 5137.
					if (objCustomerNotesInfo.QQ_APP_POL=="POL") 
					{
						objTransactionInfo.POLICY_ID =objCustomerNotesInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID  =objCustomerNotesInfo.POLICY_VER_TRACKING_ID;  
					}
					else if (objCustomerNotesInfo.QQ_APP_POL=="APP") 
					{
						objTransactionInfo.APP_ID =objCustomerNotesInfo.POLICY_ID;
						objTransactionInfo.APP_VERSION_ID =objCustomerNotesInfo.POLICY_VER_TRACKING_ID;  
					}
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1492", "");//"New note is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				//add diary entry
				objCustomerNotesInfo.NOTES_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();				
						
				if (objCustomerNotesInfo.DIARY_ITEM_REQ == "1")
				{	
					ClsDiary objDiary = new ClsDiary();
					objDiary.TransactionLogRequired = true;		
		
					//Insert into Diary
					Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();
					objToDoListInfo.SUBJECTLINE=objCustomerNotesInfo.NOTES_SUBJECT;					
					objToDoListInfo.NOTE=objCustomerNotesInfo.NOTES_DESC;
					objToDoListInfo.CREATED_DATETIME = DateTime.Now;
					objToDoListInfo.CUSTOMER_ID= objCustomerNotesInfo.CUSTOMER_ID;
					objToDoListInfo.FROMUSERID =objCustomerNotesInfo.CREATED_BY;
					objToDoListInfo.LISTTYPEID = (int)ClsDiary.enumDiaryType.COMMUNICATION_FOLLOW_UPS;   
					objToDoListInfo.MODULE_ID=   (int)ClsDiary.enumModuleMaster.CUSTOMER;  
					objToDoListInfo.LISTOPEN="Y";
					//Commented by Anurag Verma on 20/03/2007 as these properties are removed
					//objToDoListInfo.POLICYCLIENTID =objCustomerNotesInfo.CUSTOMER_ID;
					//objToDoListInfo.POLICYVERSION =objCustomerNotesInfo.POLICY_VER_TRACKING_ID;

					if (objCustomerNotesInfo.CLAIMS_ID !=0)
						objToDoListInfo.CLAIMID = objCustomerNotesInfo.CLAIMS_ID;                    
					if (objCustomerNotesInfo.QQ_APP_POL=="POL") 
					{
						objToDoListInfo.POLICY_ID =objCustomerNotesInfo.POLICY_ID;
						objToDoListInfo.POLICY_VERSION_ID =objCustomerNotesInfo.POLICY_VER_TRACKING_ID;  
					}
					else if (objCustomerNotesInfo.QQ_APP_POL=="APP") 
					{
						objToDoListInfo.APP_ID =objCustomerNotesInfo.POLICY_ID;
						objToDoListInfo.APP_VERSION_ID =objCustomerNotesInfo.POLICY_VER_TRACKING_ID;  
					}	
					else
					{
						objToDoListInfo.QUOTEID =objCustomerNotesInfo.POLICY_ID;
						objToDoListInfo.APP_ID = objCustomerNotesInfo.POLICY_VER_TRACKING_ID;
						if (strVersion!=null && strVersion!="")
						{
							string[] aRappPol=strVersion.Split('^');
							
							objToDoListInfo.APP_VERSION_ID = int.Parse(aRappPol[0]);
							if(aRappPol.Length>1)
							objToDoListInfo.POLICY_ID = int.Parse(aRappPol[1]);
							if(aRappPol.Length>2)
								objToDoListInfo.POLICY_VERSION_ID = int.Parse(aRappPol[2]);
						}
					}

					objToDoListInfo.SYSTEMFOLLOWUPID = -1;


					///Commented by Anurag Verma on 12-03-2006 for checking new diary object
					#region OLD CODE BEFORE DIARY OBJECT---DO NOT DELETE
										
					objToDoListInfo.TOUSERID=objCustomerNotesInfo.CUSTOMER_ID;
					objToDoListInfo.STARTTIME = System.DateTime.Now;
					objToDoListInfo.ENDTIME = System.DateTime.Now;
					objToDoListInfo.RECDATE = System.DateTime.Now;
					objToDoListInfo.TOUSERID=objCustomerNotesInfo.CREATED_BY;	
					
					objToDoListInfo.FOLLOWUPDATE =  objCustomerNotesInfo.FOLLOW_UP_DATE;
					//DateTime.Parse(System.DateTime.Now.AddDays(5).ToString());					
					objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					
					objToDoListInfo.RECORDED_BY		=	objCustomerNotesInfo.CREATED_BY;
	
					objToDoListInfo.TOUSERID   = objCustomerNotesInfo.TO_FOLLOWUP_ID;
					objToDoListInfo.PRIORITY   = "M";					
					objDiary.Add(objToDoListInfo,objDataWrapper);
					#endregion

					//objDiary.DiaryEntryfromSetup(objToDoListInfo); 

				}
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (objCustomerNotesInfo.NOTES_ID == -1)
				{
					return -1;
				}
				else
				{
					return returnResult;
				}
				//End Diary entry				
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
				
			}
		}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldCustomerNotesInfo">Model object having old information</param>
		/// <param name="objCustomerNotesInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update( ClsCustomerNotesInfo objOldCustomerNotesInfo,ClsCustomerNotesInfo objCustomerNotesInfo)
		{			
			string	strStoredProc = "Proc_UpdateCustomerNotes";			
			string strTranXML="";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				//AddParam(objDataWrapper,objCustomerNotesInfo);
				objDataWrapper.AddParameter("@MODIFIED_BY",objCustomerNotesInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objCustomerNotesInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@TO_FOLLOWUP_ID",objCustomerNotesInfo.TO_FOLLOWUP_ID);
				objDataWrapper.AddParameter("@NOTES_SUBJECT",objOldCustomerNotesInfo.NOTES_SUBJECT);
				if(objCustomerNotesInfo.NOTES_TYPE != 0)
					objDataWrapper.AddParameter("@NOTES_TYPE",objCustomerNotesInfo.NOTES_TYPE);
				else
					objDataWrapper.AddParameter("@NOTES_TYPE",null);
		
				if(objCustomerNotesInfo.POLICY_ID ==0)
					objDataWrapper.AddParameter("@POLICY_ID",null);
				else
					objDataWrapper.AddParameter("@POLICY_ID",objCustomerNotesInfo.POLICY_ID);

				objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objCustomerNotesInfo.POLICY_VER_TRACKING_ID);
				objDataWrapper.AddParameter("@CLAIMS_ID",objCustomerNotesInfo.CLAIMS_ID);
				objDataWrapper.AddParameter("@NOTES_DESC",objCustomerNotesInfo.NOTES_DESC);
				objDataWrapper.AddParameter("@DIARY_ITEM_REQ",objCustomerNotesInfo.DIARY_ITEM_REQ);
				objDataWrapper.AddParameter("@VISIBLE_TO_AGENCY",null);
				if(objCustomerNotesInfo.FOLLOW_UP_DATE !=DateTime.MinValue)
					objDataWrapper.AddParameter("@FOLLOW_UP_DATE",objCustomerNotesInfo.FOLLOW_UP_DATE);
				else
					objDataWrapper.AddParameter("@FOLLOW_UP_DATE",System.DBNull.Value);
				objDataWrapper.AddParameter("@QQ_APP_POL",objCustomerNotesInfo.QQ_APP_POL);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objCustomerNotesInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@NOTEUPID",objCustomerNotesInfo.NOTES_ID);

				if(TransactionLogRequired) 
				{
					objCustomerNotesInfo.TransactLabel = ClsCommon.MapTransactionLabel("Client/Aspx/AddCustomerNotes.aspx.resx");
					objBuilder.GetUpdateSQL(objOldCustomerNotesInfo,objCustomerNotesInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objBuilder.TableName = objOldCustomerNotesInfo.TableInfo.TableName;
					objBuilder.WhereClause = " WHERE NOTES_ID = " + objOldCustomerNotesInfo.NOTES_ID.ToString();
					
					//strTranXML=objBuilder.GetTransactionLogXML(objOldCustomerNotesInfo,objCustomerNotesInfo);
					if(strTranXML	== "<LabelFieldMapping></LabelFieldMapping>")
						returnResult=objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objCustomerNotesInfo.MODIFIED_BY;
						objTransactionInfo.CLIENT_ID		=	objCustomerNotesInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1493", "");//"Note has been modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//add Diary Entry 11th june by kranti

				//End Diary Entry
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

			//	objToDoListInfo.TOUSERID   = objCustomerNotesInfo.TO_FOLLOWUP_ID;
		}		

		
		private void  AddParam(DataWrapper objDataWrapper,ClsCustomerNotesInfo objCustomerNotesInfo)
		{
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objCustomerNotesInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@NOTES_SUBJECT",objCustomerNotesInfo.NOTES_SUBJECT);
				if(objCustomerNotesInfo.NOTES_TYPE != 0)
					objDataWrapper.AddParameter("@NOTES_TYPE",objCustomerNotesInfo.NOTES_TYPE);
				else
					objDataWrapper.AddParameter("@NOTES_TYPE",null);
		
				if(objCustomerNotesInfo.POLICY_ID ==0)
					objDataWrapper.AddParameter("@POLICY_ID",null);
				else
					objDataWrapper.AddParameter("@POLICY_ID",objCustomerNotesInfo.POLICY_ID);

				objDataWrapper.AddParameter("@CLAIMS_ID",objCustomerNotesInfo.CLAIMS_ID);
				objDataWrapper.AddParameter("@NOTES_DESC",objCustomerNotesInfo.NOTES_DESC);				
				objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objCustomerNotesInfo.POLICY_VER_TRACKING_ID);				
				objDataWrapper.AddParameter("@VISIBLE_TO_AGENCY",null);
				objDataWrapper.AddParameter("@QQ_APP_POL",objCustomerNotesInfo.QQ_APP_POL);
				objDataWrapper.AddParameter("@DIARY_ITEM_REQ",objCustomerNotesInfo.DIARY_ITEM_REQ);
				if (objCustomerNotesInfo.FOLLOW_UP_DATE != DateTime.MinValue) 
					objDataWrapper.AddParameter("@FOLLOW_UP_DATE",objCustomerNotesInfo.FOLLOW_UP_DATE);

				
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}



		#endregion

		//<Gaurav Tyagi> 30 May 2005 : START: Added to get Applicaiton numbers, BUG NO<546>
		#region GetCustomerNotesInfo
		public static string GetCustomerNotesInfo(int intNoteId,int intCustomerID )
		{

			DataSet dsCustomerNotes = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@Note_Id",intNoteId);
				objDataWrapper.AddParameter("@Customer_Id",intCustomerID);
				dsCustomerNotes = objDataWrapper.ExecuteDataSet(GET_CUSTOMER_NOTES_PROC);
			
				return dsCustomerNotes.GetXml();
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


		public static DataSet GetCustomerInfo(int intCustomerID,string strdelString)
		{

			DataSet dsCustomerNotes = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerID);
				objDataWrapper.AddParameter ("@NOTES_ID",strdelString);
				dsCustomerNotes = objDataWrapper.ExecuteDataSet("Proc_FetchCustomerNotes");
			
				return dsCustomerNotes;
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
		/// This function is used to get Application number regarding particular Customer 
		/// </summary>
		/// <param name="objDropDownList">Name of the Control</param>
		/// <param name="intCustomerId">Id of customer for which we need Application number</param>
		public static void GetCustomerApplication(DropDownList objDropDownList,int intCustomerId,int LangID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
            objDataWrapper.AddParameter("@LANG_ID", LangID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillApplicationDropDown").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "APP_NUMBER";
			objDropDownList.DataValueField = "VERSION_ID";
			objDropDownList.DataBind();
			
			
		}
		
		#endregion
		//<Gaurav Tyagi> 30 May 2005 : END: Added to get Applicaiton numbers, BUG NO<546>

        //Policy_ID Added For itrack Issue #6007.   
		public DataTable GetCustomerClaimNumber(int CustomerID , string  PolicyID , string Called_From)//, out int intReturnVal)
		{
			DataSet dsCustomerNotes = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID); 
				objDataWrapper.AddParameter("@CALLED_FROM",Called_From);

				//if(Called_From  != "APP" && Called_From != "")
				//{
					//objDataWrapper.AddParameter("@POLICY_ID",PolicyID); 
					//objDataWrapper.AddParameter("@CALLED_FROM",Called_From);
					//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@intReturnVal",null,SqlDbType.Int,ParameterDirection.Output);
					//intReturnVal = int.Parse(objSqlParameter.Value.ToString());
				//}				
				dsCustomerNotes = objDataWrapper.ExecuteDataSet("Proc_GetCustomerClaims");
				if(dsCustomerNotes !=null  && dsCustomerNotes.Tables.Count > 0)
				{
					return dsCustomerNotes.Tables[0];  
				}
				else
				{
				   return null;
				}
				//return dsCustomerNotes.Tables;
				
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

		
	}
}
