using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Account;
using System.Collections;

namespace Cms.BusinessLayer.BlCommon.Accounting
{
	
	/// <summary>
	/// Summary description for ClsAddNSFEntry.
	/// </summary>
	public class ClsAddNSFEntry:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		ACT_BANK_INFORMATION			=	"ACT_BANK_INFORMATION";
		
		private const int NSF_FEES =37;
		private const int LATE_FEES=38;

		public ClsAddNSFEntry()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public int ChargeLateFees(int Customer_ID, int PolicyID,int PolicyVersionID,int FeesAmount)
		{
			return ChargeLateFees(null, Customer_ID,  PolicyID, PolicyVersionID, FeesAmount);
		}

		public int ChargeLateFees( DataWrapper objDataWrapper,int Customer_ID, int PolicyID,int PolicyVersionID,int FeesAmount)
		{
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			if (objDataWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string		strStoredProc	=	"Proc_InsertACT_CUSTOMER_OPEN_ITEMS";
			objDataWrapper.ClearParameteres(); 
			objDataWrapper.AddParameter("@TOTAL_DUE",FeesAmount);
			objDataWrapper.AddParameter("@CUSTOMER_ID",Customer_ID  );
			objDataWrapper.AddParameter("@POLICY_ID",PolicyID );
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
			objDataWrapper.AddParameter("@TRAN_ID",LATE_FEES);
			objDataWrapper.AddParameter("@POLICY_NUMBER",DBNull.Value );
				
			int	returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return returnResult;

		}
       
 //Added For itrack Issue #4906
		public int InsertReinstatementEntry(ArrayList al)
		{
			string		strStoredProc	=	"Proc_InsertACT_CUSTOMER_OPEN_ITEMS";
			DateTime	RecordDate		=	DateTime.Now;
			int returnResult=0;
			ClsAddNSFEntryInfo objNSFEntry=null;
			bool status=false;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					objNSFEntry=(ClsAddNSFEntryInfo)al[i];
					objDataWrapper.AddParameter("@TOTAL_DUE",objNSFEntry.TOTAL_DUE );
					objDataWrapper.AddParameter("@CUSTOMER_ID",objNSFEntry.CUSTOMER_ID );
					objDataWrapper.AddParameter("@POLICY_ID",objNSFEntry.POLICY_ID );
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objNSFEntry.POLICY_VERSION_ID );
					objDataWrapper.AddParameter("@TRAN_ID",objNSFEntry.TransID);
					objDataWrapper.AddParameter("@POLICY_NUMBER",objNSFEntry.POLICY_NO);
					string strTranXML = "Reinstatement  Fees charged = " + "$" +objNSFEntry.TOTAL_DUE;
					//string strTranXML = "Policy Number : " + PolNum + ";Fee Reversed  : " + objFeeReversalInfo.FEES_REVERSE;
					//string strTranXML = "Reinstatament Fee Charged: " + "$"+objNSFEntry.TOTAL_DUE;
					if(TransactionLogRequired)
					{
						objNSFEntry.TransactLabel  = ClsCommon.MapTransactionLabel("/Account/Aspx/AddResintatementFees.aspx.resx");
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objNSFEntry.CREATED_BY;				
						objTransactionInfo.CLIENT_ID         =   objNSFEntry.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID      =     objNSFEntry.POLICY_ID; 												
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNSFEntry.POLICY_VERSION_ID;
                        objTransactionInfo.TRANS_DESC       = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1640", "");// "Reinstatement  Fees have been charged successfully.";
						objTransactionInfo.CUSTOM_INFO      = strTranXML;
						
                         
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					if(returnResult < 0)
					{
						status=false;
						break;
					}
					status=true;
					objDataWrapper.ClearParameteres();
				}
				if(status ==true)
				{				
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);

				}
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
		//Added For Itrack Issue #4906 Late_Fee_screen
		public int InsertLateFee(ArrayList al)
		{
			string		strStoredProc	=	"Proc_InsertACT_CUSTOMER_OPEN_ITEMS";
			DateTime	RecordDate		=	DateTime.Now;
			int returnResult=0;
			ClsAddNSFEntryInfo objNSFEntry=null;
			bool status=false;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					objNSFEntry=(ClsAddNSFEntryInfo)al[i];
					objDataWrapper.AddParameter("@TOTAL_DUE",objNSFEntry.TOTAL_DUE );
					objDataWrapper.AddParameter("@CUSTOMER_ID",objNSFEntry.CUSTOMER_ID );
					objDataWrapper.AddParameter("@POLICY_ID",objNSFEntry.POLICY_ID );
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objNSFEntry.POLICY_VERSION_ID );
					objDataWrapper.AddParameter("@TRAN_ID",objNSFEntry.TransID);
					objDataWrapper.AddParameter("@POLICY_NUMBER",objNSFEntry.POLICY_NO);
					string strTranXML = "Late Fee Charged = " + "$" +objNSFEntry.TOTAL_DUE;
					//string strTranXML = "Policy Number : " + PolNum + ";Fee Reversed  : " + objFeeReversalInfo.FEES_REVERSE;
					//string strTranXML = "Reinstatament Fee Charged: " + "$"+objNSFEntry.TOTAL_DUE;
					if(TransactionLogRequired)
					{
						objNSFEntry.TransactLabel  = ClsCommon.MapTransactionLabel("/Account/Aspx/ChargeLateFees.aspx.resx");
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objNSFEntry.CREATED_BY;				
						objTransactionInfo.CLIENT_ID         =   objNSFEntry.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID      =     objNSFEntry.POLICY_ID; 												
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNSFEntry.POLICY_VERSION_ID;
                        objTransactionInfo.TRANS_DESC       = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1641", "");// "Late Fees have been charged successfully.";
						objTransactionInfo.CUSTOM_INFO      = strTranXML;
						
                         
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					if(returnResult < 0)
					{
						status=false;
						break;
					}
					status=true;
					objDataWrapper.ClearParameteres();
				}
				if(status ==true)
				{				
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);

				}
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
		//Added For Charge Installment screen
		public int InsertInstallmentFee(ArrayList al)
		{
			string		strStoredProc	=	"Proc_InsertACT_CUSTOMER_OPEN_ITEMS";
			DateTime	RecordDate		=	DateTime.Now;
			int returnResult=0;
			ClsAddNSFEntryInfo objNSFEntry=null;
			bool status=false;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					objNSFEntry=(ClsAddNSFEntryInfo)al[i];
					objDataWrapper.AddParameter("@TOTAL_DUE",objNSFEntry.TOTAL_DUE );
					objDataWrapper.AddParameter("@CUSTOMER_ID",objNSFEntry.CUSTOMER_ID );
					objDataWrapper.AddParameter("@POLICY_ID",objNSFEntry.POLICY_ID );
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objNSFEntry.POLICY_VERSION_ID );
					objDataWrapper.AddParameter("@TRAN_ID",objNSFEntry.TransID);
					objDataWrapper.AddParameter("@POLICY_NUMBER",objNSFEntry.POLICY_NO);
					string strTranXML = "Installment Fee Charged = " + "$" + String.Format("{0:n}",objNSFEntry.TOTAL_DUE);
					//string strTranXML = "Policy Number : " + PolNum + ";Fee Reversed  : " + objFeeReversalInfo.FEES_REVERSE;
					//string strTranXML = "Reinstatament Fee Charged: " + "$"+objNSFEntry.TOTAL_DUE;
					if(TransactionLogRequired)
					{
						objNSFEntry.TransactLabel  = ClsCommon.MapTransactionLabel("/Account/Aspx/ChargeLateFees.aspx.resx");
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objNSFEntry.CREATED_BY;				
						objTransactionInfo.CLIENT_ID         =   objNSFEntry.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID      =     objNSFEntry.POLICY_ID; 												
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNSFEntry.POLICY_VERSION_ID;
                        objTransactionInfo.TRANS_DESC       = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1642", "");// "Installment Fees have been charged successfully.";
						objTransactionInfo.CUSTOM_INFO      = strTranXML;
						
                         
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					if(returnResult < 0)
					{
						status=false;
						break;
					}
					status=true;
					objDataWrapper.ClearParameteres();
				}
				if(status ==true)
				{				
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);

				}
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
		public int InsertAdjustLateFeeEntry(ArrayList al)
		{
			string		strStoredProc	=	"Proc_InsertACT_CUSTOMER_OPEN_ITEMS";
			DateTime	RecordDate		=	DateTime.Now;
			int returnResult=0;
			ClsAddNSFEntryInfo objNSFEntry=null;
			bool status=false;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					objNSFEntry=(ClsAddNSFEntryInfo)al[i];
					objDataWrapper.AddParameter("@TOTAL_DUE",objNSFEntry.TOTAL_DUE );
					objDataWrapper.AddParameter("@CUSTOMER_ID",objNSFEntry.CUSTOMER_ID );
					objDataWrapper.AddParameter("@POLICY_ID",objNSFEntry.POLICY_ID );
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objNSFEntry.POLICY_VERSION_ID );
					objDataWrapper.AddParameter("@TRAN_ID",objNSFEntry.TransID);
					objDataWrapper.AddParameter("@POLICY_NUMBER",objNSFEntry.POLICY_NO);
					
						if(TransactionLogRequired)
						{
							objNSFEntry.TransactLabel  = ClsCommon.MapTransactionLabel("/Account/Aspx/AddNSFEntry.aspx.resx");
							SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
							string strTranXML = objBuilder.GetTransactionLogXML(objNSFEntry);
							Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
							objTransactionInfo.TRANS_TYPE_ID	=	1;
							objTransactionInfo.RECORDED_BY		=	objNSFEntry.CREATED_BY;
                            objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1578", "");// "Record Has Been Added";
							objTransactionInfo.CHANGE_XML		=	strTranXML;

							//Executing the query
							returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						
						}
						else
						{
							returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						}
						if(returnResult < 0)
						{
							status=false;
							break;
						}
						status=true;
						objDataWrapper.ClearParameteres();
					}
					if(status ==true)
					{				
						objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					}
					else
					{
						objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);

					}
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
		public int InsertAdjustNfsEntry(ArrayList al)
		{
			string		strStoredProc	=	"Proc_InsertACT_CUSTOMER_OPEN_ITEMS";
			DateTime	RecordDate		=	DateTime.Now;
			int returnResult=0;
			ClsAddNSFEntryInfo objNSFEntry=null;
			bool status=false;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					objNSFEntry=(ClsAddNSFEntryInfo)al[i];
					objDataWrapper.AddParameter("@TOTAL_DUE",objNSFEntry.TOTAL_DUE );
					objDataWrapper.AddParameter("@CUSTOMER_ID",objNSFEntry.CUSTOMER_ID );
					objDataWrapper.AddParameter("@POLICY_ID",objNSFEntry.POLICY_ID );
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objNSFEntry.POLICY_VERSION_ID );
					objDataWrapper.AddParameter("@TRAN_ID",objNSFEntry.TransID);
					objDataWrapper.AddParameter("@POLICY_NUMBER",objNSFEntry.POLICY_NO);					
					//string strTranXML = "Policy Number    : " + objNSFEntry.POLICY_NO  + ";NSF Fee Charged : " + "$" + objNSFEntry.TOTAL_DUE;
					//string strTranXML = "Reinstatament Fee Charged : " + "$" + String.Format ("{0:N5}",objNSFEntry.TOTAL_DUE);
					string amt=	(string)(objNSFEntry.TOTAL_DUE).ToString();		
					string strTranXML = " Non Sufficient Fund Fees charged = " + "$" + amt; //Double.Parse(objNSFEntry.TOTAL_DUE.ToString());		
					if(TransactionLogRequired)
					{
						objNSFEntry.TransactLabel  = ClsCommon.MapTransactionLabel("/Account/Aspx/AddNSFEntry.aspx.resx");
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;						
						//TO MOVE TO LOCAL VSS
						//Added For Itrack Issue #5425.						
						objTransactionInfo.CLIENT_ID        =   objNSFEntry.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID        =  objNSFEntry.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNSFEntry.POLICY_VERSION_ID;
                        objTransactionInfo.RECORDED_BY      = objNSFEntry.CREATED_BY;        
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1643", "");// "Non Sufficient Fund Fees have been charged successfully.";               
						objTransactionInfo.CUSTOM_INFO		=	strTranXML;
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);					
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					if(returnResult < 0)
					{
						status=false;
						break;
					}
					status=true;
					objDataWrapper.ClearParameteres();
				}
				if(status ==true)
				{				
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);

				}
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
	}
}
