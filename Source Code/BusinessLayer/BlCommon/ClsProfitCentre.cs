/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	5/9/2005 3:16:04 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
	using System;
	using System.Data;
	using System.Text;
	using System.Xml;
	using System.Data.SqlClient;
	using Cms.DataLayer;
	using System.Configuration;
	using Cms.Model.Maintenance;
	using System.Web.UI.WebControls;

	namespace Cms.BusinessLayer.BlCommon
	{
		/// <summary>
		/// 
		/// </summary>
		public class ClsProfitCentre : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
		{
			private const	string		MNT_PROFIT_CENTER_LIST			=	"MNT_PROFIT_CENTER_LIST";
	 

			#region Private Instance Variables
			private			bool		boolTransactionLog;
			// private int _PC_ID;
			private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateProfitCenter";
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
			public ClsProfitCentre()
			{
				boolTransactionLog	= base.TransactionLogRequired;
				base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
			}
			#endregion

			#region Add(Insert) functions
			/// <summary>
			/// Saves the information passed in model object to database.
			/// </summary>
			/// <param name="ObjProfitCenterInfo">Model class object.</param>
			/// <returns>No of records effected.</returns>
			public int Add(Cms.Model.Maintenance.ClsProfitCenterInfo  ObjProfitCenterInfo)
			{
				string		strStoredProc	=	"Proc_InsertProfitCenter";
				DateTime	RecordDate		=	DateTime.Now;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				try
				{
					objDataWrapper.AddParameter("@Code",ObjProfitCenterInfo.PC_CODE);
					objDataWrapper.AddParameter("@Name",ObjProfitCenterInfo.PC_NAME);
					objDataWrapper.AddParameter("@Add1",ObjProfitCenterInfo.PC_ADD1);
					objDataWrapper.AddParameter("@Add2",ObjProfitCenterInfo.PC_ADD2);
					objDataWrapper.AddParameter("@City",ObjProfitCenterInfo.PC_CITY);
					objDataWrapper.AddParameter("@State",ObjProfitCenterInfo.PC_STATE);
					objDataWrapper.AddParameter("@Zip",ObjProfitCenterInfo.PC_ZIP);
					objDataWrapper.AddParameter("@Country",ObjProfitCenterInfo.PC_COUNTRY);
					objDataWrapper.AddParameter("@Phone",ObjProfitCenterInfo.PC_PHONE);
					objDataWrapper.AddParameter("@Extension",ObjProfitCenterInfo.PC_EXT);
					objDataWrapper.AddParameter("@Fax",ObjProfitCenterInfo.PC_FAX);
					objDataWrapper.AddParameter("@EMail",ObjProfitCenterInfo.PC_EMAIL);
					objDataWrapper.AddParameter("@Created_By",ObjProfitCenterInfo.CREATED_BY);
					objDataWrapper.AddParameter("@Created_DateTime",ObjProfitCenterInfo.CREATED_DATETIME);
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ProfitCenterId",ObjProfitCenterInfo.PC_ID,SqlDbType.Int,ParameterDirection.Output);

					int returnResult = 0;
					if(TransactionLogRequired)
					{
						ObjProfitCenterInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsWeb/Maintenance/addProfitcenter.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(ObjProfitCenterInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	232;
						objTransactionInfo.RECORDED_BY		=	ObjProfitCenterInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
                        objTransactionInfo.CUSTOM_INFO = ObjProfitCenterInfo.CUSTOM_INFO;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					int PC_ID = int.Parse(objSqlParameter.Value.ToString());
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					if (PC_ID == -1)
					{
						return -1;
					}
					else
					{
						ObjProfitCenterInfo.PC_ID = PC_ID;
						return returnResult;
					}
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
			/// <param name="objOldAddProfitCenterInfo">Model object having old information</param>
			/// <param name="ObjProfitCenterInfo">Model object having new information(form control's value)</param>
			/// <returns>No. of rows updated (1 or 0)</returns>
			public int Update(Cms.Model.Maintenance.ClsProfitCenterInfo objOldAddProfitCenterInfo,ClsProfitCenterInfo ObjProfitCenterInfo)
			{
				string strStoredProc="Proc_UpdateProfitCenter";
				string strTranXML;
				int returnResult = 0;
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try 
				{
					objDataWrapper.AddParameter("@PC_CODE",ObjProfitCenterInfo.PC_CODE);
					objDataWrapper.AddParameter("@PC_ID",ObjProfitCenterInfo.PC_ID);
					objDataWrapper.AddParameter("@PC_NAME",ObjProfitCenterInfo.PC_NAME);
					objDataWrapper.AddParameter("@PC_ADD1",ObjProfitCenterInfo.PC_ADD1);
					objDataWrapper.AddParameter("@PC_ADD2",ObjProfitCenterInfo.PC_ADD2);
					objDataWrapper.AddParameter("@PC_CITY",ObjProfitCenterInfo.PC_CITY);
					objDataWrapper.AddParameter("@PC_STATE",ObjProfitCenterInfo.PC_STATE);
					objDataWrapper.AddParameter("@PC_ZIP",ObjProfitCenterInfo.PC_ZIP);
					objDataWrapper.AddParameter("@PC_COUNTRY",ObjProfitCenterInfo.PC_COUNTRY);
					objDataWrapper.AddParameter("@PC_PHONE",ObjProfitCenterInfo.PC_PHONE);
					objDataWrapper.AddParameter("@PC_EXT",ObjProfitCenterInfo.PC_EXT);
					objDataWrapper.AddParameter("@PC_FAX",ObjProfitCenterInfo.PC_FAX);
					objDataWrapper.AddParameter("@PC_EMAIL",ObjProfitCenterInfo.PC_EMAIL);
					objDataWrapper.AddParameter("@MODIFIED_BY",ObjProfitCenterInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjProfitCenterInfo.LAST_UPDATED_DATETIME);

					if(TransactionLogRequired) 
					{
						ObjProfitCenterInfo.TransactLabel  = ClsCommon.MapTransactionLabel("cmsWeb/Maintenance/addProfitcenter.aspx.resx");
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						strTranXML = objBuilder.GetTransactionLogXML(objOldAddProfitCenterInfo,ObjProfitCenterInfo);
						objTransactionInfo.TRANS_TYPE_ID	=	233;
						objTransactionInfo.RECORDED_BY		=	ObjProfitCenterInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
                        objTransactionInfo.CUSTOM_INFO = ObjProfitCenterInfo.CUSTOM_INFO;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

					}
					else
					{
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
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

			#region GetProfitCenterXml
			public static string GetProfitCenterXml(int intPC_ID)
			{
				string strStoredProc = "Proc_GetProfitCenter";
				DataSet dsProfitCenter = new DataSet();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				try
				{
					objDataWrapper.AddParameter("@PC_ID",intPC_ID);
					dsProfitCenter = objDataWrapper.ExecuteDataSet(strStoredProc);
					if (dsProfitCenter.Tables[0].Rows.Count != 0)
					{
						return dsProfitCenter.GetXml();
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


			public int DeleteProfitCenter(int intPCID, string customInfo , int modifiedBy )
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				string	strStoredProc =	"Proc_DeleteProfitCenter";
				objDataWrapper.AddParameter("@ProfitCenterId",intPCID);
				int intResult;	
				if(TransactionLogRequired) 
				{
					//ObjProfitCenterInfo.TransactLabel  = ClsCommon.MapTransactionLabel("cmsWeb/Maintenance/addProfitcenter.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//strTranXML = objBuilder.GetTransactionLogXML(objOldAddProfitCenterInfo,ObjProfitCenterInfo);
					objTransactionInfo.TRANS_TYPE_ID	=	234;
					objTransactionInfo.RECORDED_BY		=	modifiedBy;
					objTransactionInfo.TRANS_DESC		=	"";
//					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO      = customInfo; //"Profit Center Name:" + ObjProfitCenterInfo.PC_NAME +"<br>"+ "Profit Center Code:" + ObjProfitCenterInfo.PC_CODE;
					intResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return intResult;
		
			}
			
		}
	}
