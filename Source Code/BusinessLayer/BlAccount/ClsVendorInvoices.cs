/******************************************************************************************
<Author				: -   Ajit Singh chahal
<Start Date				: -	6/28/2005 10:11:44 AM
<End Date				: -	
<Description				: - 	Business Logic for vendor invoices.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Account;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Business Logic for vendor invoices.
	/// </summary>
	public class ClsVendorInvoices : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		private const	string		ACT_VENDOR_INVOICES			=	"ACT_VENDOR_INVOICES";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateVendorInvoices";
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
		public ClsVendorInvoices()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsVendorInvoices(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objVendorInvoicesInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsVendorInvoicesInfo objVendorInvoicesInfo,string strVendorInvoiceDetails)
		{
			string		strStoredProc	=	"Proc_InsertACT_VENDOR_INVOICES";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@VENDOR_ID",objVendorInvoicesInfo.VENDOR_ID);
				objDataWrapper.AddParameter("@INVOICE_NUM",objVendorInvoicesInfo.INVOICE_NUM);
				objDataWrapper.AddParameter("@REF_PO_NUM",objVendorInvoicesInfo.REF_PO_NUM);
				objDataWrapper.AddParameter("@TRANSACTION_DATE",objVendorInvoicesInfo.TRANSACTION_DATE);
				objDataWrapper.AddParameter("@DUE_DATE",objVendorInvoicesInfo.DUE_DATE);
				objDataWrapper.AddParameter("@INVOICE_AMOUNT",objVendorInvoicesInfo.INVOICE_AMOUNT);
				objDataWrapper.AddParameter("@NOTE",objVendorInvoicesInfo.NOTE);
				objDataWrapper.AddParameter("@IS_ACTIVE",objVendorInvoicesInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objVendorInvoicesInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objVendorInvoicesInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objVendorInvoicesInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objVendorInvoicesInfo.LAST_UPDATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@INVOICE_ID",objVendorInvoicesInfo.INVOICE_ID,SqlDbType.Int,ParameterDirection.Output);
				objDataWrapper.AddParameter("@FISCAL_ID",objVendorInvoicesInfo.FISCAL_ID);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objVendorInvoicesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddVendorInvoice.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objVendorInvoicesInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objVendorInvoicesInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Vendor Invoice Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=   strVendorInvoiceDetails;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int INVOICE_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (INVOICE_ID == -1)
				{
					return -1;
				}
				else
				{
					objVendorInvoicesInfo.INVOICE_ID = INVOICE_ID;
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


		/// <summary>
		/// Fatch Vendor Name and Invoice Number
		/// </summary>
		/// <param name="venderId"></param>
		/// <returns></returns>
		public DataSet FatchVendorNameInvoiceNo(int InvoiceId)
		{
			string	strStoredProc =	"Proc_FatchVendorNameInvoiceNo";
			
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@INVOICE_ID",InvoiceId);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

				return ds ;
			}
			catch
			{
				return null;
			}	
		}

		public static string FetchVendorEFTInfo(int VendorId)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objWrapper.AddParameter("@VENDOR_ID",VendorId);
				string strSQL = "Proc_GetVendorEFTInfo";
				DataSet objDS = objWrapper.ExecuteDataSet(strSQL);
				if(objDS != null && objDS.Tables.Count > 0 && objDS.Tables[0].Rows.Count > 0)
				{
					int	 intEFTMode; 
					string strEFTMode; 
					intEFTMode = int.Parse(objDS.Tables[0].Rows[0]["ALLOWS_EFT"].ToString());
					if(intEFTMode == int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
						strEFTMode = "N";
					else
						strEFTMode = "Y";
					return strEFTMode;
				}
				else
					return "0";
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			
		}

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldVendorInvoicesInfo">Model object having old information</param>
		/// <param name="objVendorInvoicesInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsVendorInvoicesInfo objOldVendorInvoicesInfo,ClsVendorInvoicesInfo objVendorInvoicesInfo,string strVendorInvoiceDetails)
		{
			//Fetch Vendor Name 
			DataSet VendorNameInvoiceNo = FatchVendorNameInvoiceNo(objVendorInvoicesInfo.INVOICE_ID);
			string VendorName = "";
			if(VendorNameInvoiceNo.Tables[0].Rows.Count > 0)
			{
				VendorName	=	VendorNameInvoiceNo.Tables[0].Rows[0]["VENDOR_NAME"].ToString();
			}	
			//end 

			string		strStoredProc	=	"Proc_UpdateACT_VENDOR_INVOICES";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@INVOICE_ID",objVendorInvoicesInfo.INVOICE_ID);
				objDataWrapper.AddParameter("@VENDOR_ID",objVendorInvoicesInfo.VENDOR_ID);
				objDataWrapper.AddParameter("@INVOICE_NUM",objVendorInvoicesInfo.INVOICE_NUM);
				objDataWrapper.AddParameter("@REF_PO_NUM",objVendorInvoicesInfo.REF_PO_NUM);
				objDataWrapper.AddParameter("@TRANSACTION_DATE",objVendorInvoicesInfo.TRANSACTION_DATE);
				objDataWrapper.AddParameter("@DUE_DATE",objVendorInvoicesInfo.DUE_DATE);
				objDataWrapper.AddParameter("@INVOICE_AMOUNT",objVendorInvoicesInfo.INVOICE_AMOUNT);
				objDataWrapper.AddParameter("@NOTE",objVendorInvoicesInfo.NOTE);
				//objDataWrapper.AddParameter("@IS_COMMITTED",objVendorInvoicesInfo.IS_COMMITTED);
				//objDataWrapper.AddParameter("@DATE_COMMITTED",objVendorInvoicesInfo.DATE_COMMITTED);
				objDataWrapper.AddParameter("@MODIFIED_BY",objVendorInvoicesInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objVendorInvoicesInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@FISCAL_ID",objVendorInvoicesInfo.FISCAL_ID);
				if(base.TransactionLogRequired) 
				{
					objVendorInvoicesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddVendorInvoice.aspx.resx");
					objBuilder.GetUpdateSQL(objOldVendorInvoicesInfo,objVendorInvoicesInfo,out strTranXML, "N");

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objVendorInvoicesInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Vendor Invoice Has Been Updated";
					//objTransactionInfo.CUSTOM_INFO		=   "Vendor Name:" + VendorName + "<br>Invoice #:" + objVendorInvoicesInfo.INVOICE_NUM  ;
					objTransactionInfo.CUSTOM_INFO		=   strVendorInvoiceDetails;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
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

		#region Delete Vendor Invoice
		public int Delete(ClsVendorInvoicesInfo objVendorInvoicesInfo, string strVendorInvoiceDetails)
		{
			//Fetch Vendor Name 
			DataSet VendorNameInvoiceNo = FatchVendorNameInvoiceNo(objVendorInvoicesInfo.INVOICE_ID);
			string VendorName = "";
			if(VendorNameInvoiceNo.Tables[0].Rows.Count > 0)
			{
				VendorName	=	VendorNameInvoiceNo.Tables[0].Rows[0]["VENDOR_NAME"].ToString();
			}	
			//end 

			string		strStoredProc	=	"Proc_DeleteACT_VENDOR_INVOICES";
			//string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@INVOICE_ID",objVendorInvoicesInfo.INVOICE_ID);
				
				if(base.TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objVendorInvoicesInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Vendor Invoice Has Been Deleted";
					//objTransactionInfo.CUSTOM_INFO		=   "Vendor Name:" + VendorName + "<br>Invoice #:" + objVendorInvoicesInfo.INVOICE_NUM  ;
					objTransactionInfo.CUSTOM_INFO		=   strVendorInvoiceDetails;
					
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

		#region "Approve"
		/*	public int Approve(ClsVendorInvoicesInfo objVendorInvoicesInfo)
			{
				string		strStoredProc	=	"Proc_ApproveACT_VENDOR_INVOICES";
				string strTranXML;
				int returnResult = 0;
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try 
				{
					objDataWrapper.AddParameter("@INVOICE_ID",objVendorInvoicesInfo.INVOICE_ID);
					objDataWrapper.AddParameter("@IS_APPROVED",objVendorInvoicesInfo.IS_APPROVED);
					objDataWrapper.AddParameter("@APPROVED_BY",objVendorInvoicesInfo.APPROVED_BY);
					objDataWrapper.AddParameter("@APPROVED_DATE_TIME",objVendorInvoicesInfo.APPROVED_DATE_TIME);
					objDataWrapper.AddParameter("@MODIFIED_BY",objVendorInvoicesInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objVendorInvoicesInfo.LAST_UPDATED_DATETIME);

					if(base.TransactionLogRequired) 
					{
						objVendorInvoicesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddVendorInvoice.aspx.resx");
						strTranXML=objBuilder.GetTransactionLogXML(objVendorInvoicesInfo );

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objVendorInvoicesInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vendor Invoice Has Been Approved";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
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
			}*/
		#endregion

		#region "commit"
		public int Commit(ClsVendorInvoicesInfo objVendorInvoicesInfo)
		{
			string		strStoredProc			=	"Proc_CommitACT_VENDOR_INVOICES";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@INVOICE_ID",objVendorInvoicesInfo.INVOICE_ID);
				objDataWrapper.AddParameter("@IS_APPROVED",objVendorInvoicesInfo.IS_APPROVED);
				objDataWrapper.AddParameter("@APPROVED_BY",objVendorInvoicesInfo.APPROVED_BY);
				objDataWrapper.AddParameter("@APPROVED_DATE_TIME",objVendorInvoicesInfo.APPROVED_DATE_TIME);
				objDataWrapper.AddParameter("@VENDOR_ID",objVendorInvoicesInfo.VENDOR_ID);
				objDataWrapper.AddParameter("@DIV_ID",ConfigurationManager.AppSettings.Get("DIV_ID"));
                objDataWrapper.AddParameter("@DEPT_ID", ConfigurationManager.AppSettings.Get("DEPT_ID"));
                objDataWrapper.AddParameter("@PC_ID", ConfigurationManager.AppSettings.Get("PC_ID"));
				objDataWrapper.AddParameter("@IS_COMMITTED",objVendorInvoicesInfo.IS_COMMITTED);
				objDataWrapper.AddParameter("@DATE_COMMITTED",objVendorInvoicesInfo.DATE_COMMITTED);
				objDataWrapper.AddParameter("@MODIFIED_BY",objVendorInvoicesInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objVendorInvoicesInfo.LAST_UPDATED_DATETIME);
				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RetVal",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(base.TransactionLogRequired) 
				{
					objVendorInvoicesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddVendorInvoice.aspx.resx");
					strTranXML=objBuilder.GetTransactionLogXML(objVendorInvoicesInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objVendorInvoicesInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Vendor Invoice Has Been Committed";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(objSqlRetParameter.Value!=null && int.Parse(objSqlRetParameter.Value.ToString())<=0)
					return int.Parse(objSqlRetParameter.Value.ToString());
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
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

		/// <summary>
		/// Rule: Invoice can be approved only if Distributed fully, can be committed only if approved.
		/// </summary>
		/// <param name="INVOICE_ID"></param>
		/// <returns>status of invoice </returns>
		/// N: Not Distributed
		/// D:Distributed 
		/// A:approved it is implicit that invoice is distributed.
		/// C:committed , it is implicit that invoice is distributed and approved.
		public static string GetInvoiceStatus(string INVOICE_ID)
		{
			string strSql = "Proc_GetInvoiceStatus_ACT_VENDOR_INVOICES";
			SqlParameter[] param = new SqlParameter[1];
			param[0] = new SqlParameter("@INVOICE_ID",INVOICE_ID);
			string status=DataWrapper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strSql,param).ToString();
			return status;		
		}
		#endregion

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(string INVOICE_ID)
		{
			string strSql = "Proc_GetXMLACT_VENDOR_INVOICES";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@INVOICE_ID",INVOICE_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion

		#region Pending Invoices Methods

		public static DataSet GetPendingInvoices(int VendorID,int CheckID)
		{
			string strSql = "PROC_GET_PENDING_INVOICES";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@VENDOR_ID",VendorID);
			objDataWrapper.AddParameter("@CHECK_ID",CheckID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		public static DataSet GetVendorPendingInvoices(string FromTranDate,string ToTranDate,string FromDueDate,string ToDueDate,double FromAmount,double ToAmount,int VendorId)
		{
			string strSql = "Proc_FetchPendingVendorInvoices";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			if(FromTranDate!= "")
				objDataWrapper.AddParameter("@FROM_TRAN_DATE",FromTranDate);
			else
				objDataWrapper.AddParameter("@FROM_TRAN_DATE",null);
			if(ToTranDate!= "")
				objDataWrapper.AddParameter("@TO_TRAN_DATE",ToTranDate);
			else
				objDataWrapper.AddParameter("@TO_TRAN_DATE",null);
			if(FromDueDate!= "")
				objDataWrapper.AddParameter("@FROM_DUE_DATE",FromDueDate);
			else
				objDataWrapper.AddParameter("@FROM_DUE_DATE",null);
			if(ToDueDate!= "")
				objDataWrapper.AddParameter("@TO_DUE_DATE",ToDueDate);
			else
				objDataWrapper.AddParameter("@TO_DUE_DATE",null);
			if(FromAmount != 0.0)
				objDataWrapper.AddParameter("@FROM_AMOUNT",FromAmount);
			else
				objDataWrapper.AddParameter("@FROM_AMOUNT",null);
			if(ToAmount != 0.0)
				objDataWrapper.AddParameter("@TO_AMOUNT",ToAmount);
			else
				objDataWrapper.AddParameter("@TO_AMOUNT",null);
			if(VendorId != 0)
				objDataWrapper.AddParameter("@VENDOR_ID",VendorId);
			else
				objDataWrapper.AddParameter("@VENDOR_ID",null);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}


		public static DataSet FetchPendingAgencyStatements(int month,int year,string agencyName,string agencyType)
		{
			DataSet ds = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				if(month == 0)
					objDataWrapper.AddParameter("@MONTH", null, SqlDbType.Int);
				else
					objDataWrapper.AddParameter("@MONTH", month, SqlDbType.Int);

				if(year == 0)
					objDataWrapper.AddParameter("@YEAR", null, SqlDbType.Int);
				else
					objDataWrapper.AddParameter("@YEAR", year, SqlDbType.Int);
				if(agencyName == "")
					objDataWrapper.AddParameter("@AGENCY_NAME", null, SqlDbType.VarChar);
				else
					objDataWrapper.AddParameter("@AGENCY_NAME", agencyName, SqlDbType.VarChar);

				if(agencyType == "")
					objDataWrapper.AddParameter("@COMM_TYPE", null, SqlDbType.VarChar);
				else
					objDataWrapper.AddParameter("@COMM_TYPE", agencyType, SqlDbType.VarChar);



				//objDataWrapper.AddParameter("@COMM_TYPE", commType, SqlDbType.VarChar);
				//ds = objDataWrapper.ExecuteDataSet("Proc_FetchPendingAgencyStatements");
				ds = objDataWrapper.ExecuteDataSet("Proc_RPTPendingAgencyStatements");
				
				return ds;
				
				//ds.Dispose();
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 			
			}

		}
		
		public static DataSet FetchReinsuranceCoverageReport(string lob,string state)
		{
			DataSet ds = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			try
			{
				if(lob == "")
					objDataWrapper.AddParameter("@LOB", null, SqlDbType.VarChar);
				else
					objDataWrapper.AddParameter("@LOB", lob, SqlDbType.VarChar);

				if(state == "")
					objDataWrapper.AddParameter("@STATE", null, SqlDbType.VarChar);
				else
					objDataWrapper.AddParameter("@STATE", state, SqlDbType.VarChar);
				
				ds = objDataWrapper.ExecuteDataSet("Proc_RPTReinsuranceCoverageReport");
				
				return ds;				
				
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 			
			}

		}

		public static double GetCheckAmount(int CheckID,int VendorID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
			string strSql = "SELECT CHECK_AMOUNT FROM TEMP_ACT_CHECK_INFORMATION WHERE PAYEE_ENTITY_ID = " + VendorID + " AND CHECK_ID = " + CheckID;
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			double CheckAmount = double.Parse(objDataSet.Tables[0].Rows[0]["CHECK_AMOUNT"].ToString());
			return CheckAmount;
			
		}

		public int SavePendingInvoices(ClsPendingVendorInvoicesInfo objPVIInfo)
		{
			string		strStoredProc	=	"Proc_InsertVendorCheckDistribution";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CHECK_ID",objPVIInfo.CHECK_ID);
				objDataWrapper.AddParameter("@VENDOR_ID",objPVIInfo.VENDOR_ID);
				objDataWrapper.AddParameter("@OPEN_ITEM_ROW_ID",objPVIInfo.OPEN_ITEM_ROW_ID);
				objDataWrapper.AddParameter("@AMOUNT_TO_APPLY",objPVIInfo.AMOUNT_TO_APPLY);
				
				objDataWrapper.AddParameter("@REF_INVOICE_ID",objPVIInfo.REF_INVOICE_ID );
				objDataWrapper.AddParameter("@REF_INVOICE_NO",objPVIInfo.REF_INVOICE_NO );
				objDataWrapper.AddParameter("@REF_INVOICE_REF_NO",objPVIInfo.REF_INVOICE_REF_NO );
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@IDEN_ROW_ID",objPVIInfo.IDEN_ROW_ID,SqlDbType.Int,ParameterDirection.Output);
	
				int returnResult = 0;
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				int IDEN_ROW_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				objPVIInfo.IDEN_ROW_ID = IDEN_ROW_ID;
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

		public int UpdatePendingInvoices(ClsPendingVendorInvoicesInfo objPVIInfo)
		{
			string		strStoredProc	=	"Proc_UpdateVendorCheckDistribution";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CHECK_ID",objPVIInfo.CHECK_ID);
				objDataWrapper.AddParameter("@IDEN_ROW_ID",objPVIInfo.IDEN_ROW_ID);
				objDataWrapper.AddParameter("@AMOUNT_TO_APPLY",objPVIInfo.AMOUNT_TO_APPLY);
						
				int returnResult = 0;
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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

		public int DeletePendingInvoices(ClsPendingVendorInvoicesInfo objPVIInfo)
		{
			string		strStoredProc	=	"Proc_DeleteVendorCheckDistribution";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CHECK_ID",objPVIInfo.CHECK_ID);
				objDataWrapper.AddParameter("@IDEN_ROW_ID",objPVIInfo.IDEN_ROW_ID);
										
				int returnResult = 0;
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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

		public int ChkForPayAmtAndCheckAmt(int CheckID,int VendorID)
		{
			string strProc = "Proc_CheckEqualPayAmountCheckAmount";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objWrapper.AddParameter("@CHECK_ID",CheckID);
			objWrapper.AddParameter("@VENDOR_ID",VendorID);
			int RetVal;
			SqlParameter objParam = (SqlParameter)objWrapper.AddParameter("@RETVAL",0,SqlDbType.Int,ParameterDirection.Output);
			RetVal = objWrapper.ExecuteNonQuery(strProc);
			RetVal = int.Parse(objParam.Value.ToString());
			objWrapper.ClearParameteres();
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return RetVal;
		}

		
		#endregion

	}
}
