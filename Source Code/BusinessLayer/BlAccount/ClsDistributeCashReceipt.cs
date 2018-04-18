using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Account;


namespace Cms.BusinessLayer.BlAccount
{
	


	public class ClsDistributeCashReceipt :  Cms.BusinessLayer.BlAccount.ClsAccount,IDisposable
	{
		Distribute objDistribute = null;
		public ClsDistributeCashReceipt()
		{
			objDistribute = new ActualDistribute();
		}
		public ClsDistributeCashReceipt(Distribute obj)
		{
			objDistribute = obj;
		}
		public static DataTable FetchGLAccounts()
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetGLAccounts");
				return dsTemp.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}
		#region geting transaction XML for Delete
		protected ClsDistributeCheckInfo GenerateTranObjectDelete(DataRow objNew,int intLineItemId ,string strCalledFrom)
		{
			ClsDistributeCheckInfo objTranObject =new ClsDistributeCheckInfo();
			
			//objTranObject.IDEN_ROW_ID =int.Parse(objNew["IDEN_ROW_ID"].ToString());
			//objTranObject.GROUP_ID=int.Parse(objNew["GROUP_ID"].ToString());
			
			objTranObject.GROUP_ID= intLineItemId;
			objTranObject.GROUP_TYPE= strCalledFrom;

			objTranObject.ACCOUNT_ID=int.Parse(objNew["ACCOUNT_ID"].ToString());			
			objTranObject.DISTRIBUTION_PERCT=Double.Parse(objNew["DISTRIBUTION_PERCT"].ToString());
			objTranObject.DISTRIBUTION_AMOUNT=Double.Parse(objNew["DISTRIBUTION_AMOUNT"].ToString());

			//objTranObject.IS_ACTIVE = objNew["IS_ACTIVE"].ToString();
			objTranObject.NOTE = objNew["NOTE"].ToString();
			//objTranObject.CREATED_BY = int.Parse(objNew["CREATED_BY"].ToString());
			//objTranObject.MODIFIED_BY = int.Parse(objNew["MODIFIED_BY"].ToString());
			//objTranObject.CREATED_DATETIME = Convert.ToDateTime(objNew["CREATED_DATETIME"].ToString());
			return objTranObject;
		}
		#endregion

		#region geting transaction XML for insert
		protected ClsDistributeCheckInfo GenerateTranObjectInsert(DataRow objNew,int intLineItemId ,string strCalledFrom)
		{
			ClsDistributeCheckInfo objTranObject =new ClsDistributeCheckInfo();
			
			//objTranObject.IDEN_ROW_ID =int.Parse(objNew["IDEN_ROW_ID"].ToString());
			//objTranObject.GROUP_ID=int.Parse(objNew["GROUP_ID"].ToString());
			
			objTranObject.GROUP_ID= intLineItemId;
			objTranObject.GROUP_TYPE= strCalledFrom;

			objTranObject.ACCOUNT_ID=int.Parse(objNew["ACCOUNT_ID"].ToString());			
			objTranObject.DISTRIBUTION_PERCT=Double.Parse(objNew["DISTRIBUTION_PERCT"].ToString());
			objTranObject.DISTRIBUTION_AMOUNT=Double.Parse(objNew["DISTRIBUTION_AMOUNT"].ToString());

			//objTranObject.IS_ACTIVE = objNew["IS_ACTIVE"].ToString();
			objTranObject.NOTE = objNew["NOTE"].ToString();
			//objTranObject.CREATED_BY = int.Parse(objNew["CREATED_BY"].ToString());
			//objTranObject.MODIFIED_BY = int.Parse(objNew["MODIFIED_BY"].ToString());
			//objTranObject.CREATED_DATETIME = Convert.ToDateTime(objNew["CREATED_DATETIME"].ToString());
			return objTranObject;
		}
		#endregion

		#region geting transaction XML for update
		protected ClsDistributeCheckInfo GenerateTranObjectUpdate(DataRow objNew)
		{
			ClsDistributeCheckInfo objTranObject =new ClsDistributeCheckInfo();
			
			objTranObject.IDEN_ROW_ID =int.Parse(objNew["IDEN_ROW_ID"].ToString());
			objTranObject.ACCOUNT_ID=int.Parse(objNew["ACCOUNT_ID"].ToString());
			objTranObject.DISTRIBUTION_AMOUNT=Double.Parse(objNew["DISTRIBUTION_AMOUNT"].ToString());
			objTranObject.DISTRIBUTION_PERCT=Double.Parse(objNew["DISTRIBUTION_PERCT"].ToString());
			//objTranObject.GROUP_ID=int.Parse(objNew["GROUP_ID"].ToString());
			//objTranObject.GROUP_TYPE= objNew["GROUP_TYPE"].ToString();
			//objTranObject.IS_ACTIVE = objNew["IS_ACTIVE"].ToString();
			objTranObject.NOTE = objNew["NOTE"].ToString();
			//objTranObject.CREATED_BY = int.Parse(objNew["CREATED_BY"].ToString());
			//objTranObject.MODIFIED_BY = int.Parse(objNew["MODIFIED_BY"].ToString());
			//objTranObject.CREATED_DATETIME = Convert.ToDateTime(objNew["CREATED_DATETIME"].ToString());
			return objTranObject;
		}
		#endregion

		public int InsertDistributionDetail(Double totalAmount ,DataTable dt,int intLineItemId,string strCalledFrom,int intCreatedBy)
		{		
		
			string	strStoredProc = objDistribute.InsertDistributionDetail;
			int result=0;

			DataSet VendorNameInvoiceNo = FatchVendorNameInvoiceNo(intLineItemId);
			string VendorName ="";
			string InvoiceNo = "";

			if(VendorNameInvoiceNo.Tables[0].Rows.Count > 0)
			{
				VendorName	=	VendorNameInvoiceNo.Tables[0].Rows[0]["VENDOR_NAME"].ToString();
				InvoiceNo	=	VendorNameInvoiceNo.Tables[0].Rows[0]["INVOICE_NUM"].ToString();				
			}	

			StringBuilder sbTranXML = new StringBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			sbTranXML.Append("<root>");	
			try
			{				
				for(int i=0;i < dt.Rows.Count;i++)
				{
					DataRow dr=dt.Rows[i];					
					//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					
					objDataWrapper.AddParameter("@GROUP_ID",intLineItemId,SqlDbType.Int);
					objDataWrapper.AddParameter("@GROUP_TYPE",strCalledFrom,SqlDbType.VarChar);
					objDataWrapper.AddParameter("@ACCOUNT_ID",int.Parse(dr["ACCOUNT_ID"].ToString()),SqlDbType.Int);
					if (dr["DISTRIBUTION_PERCT"].ToString() != "")
					{
						objDataWrapper.AddParameter("@DISTRIBUTION_PERCT",double.Parse(dr["DISTRIBUTION_PERCT"].ToString()),SqlDbType.Decimal);
					}
					else
					{
						objDataWrapper.AddParameter("@DISTRIBUTION_PERCT",null);
					}
					objDataWrapper.AddParameter("@DISTRIBUTION_AMOUNT",double.Parse(dr["DISTRIBUTION_AMOUNT"].ToString()),SqlDbType.Decimal);
					objDataWrapper.AddParameter("@NOTE",dr["NOTE"].ToString(),SqlDbType.NVarChar);
					objDataWrapper.AddParameter("@CREATED_BY",intCreatedBy,SqlDbType.Int);
					
					
					//Added Transction log by kranti
					string strTranXML = "";
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					ClsDistributeCheckInfo objNew= this.GenerateTranObjectInsert(dr,intLineItemId, strCalledFrom);
					objNew.TransactLabel = BlCommon.ClsCommon.MapTransactionLabel("Account/Aspx/DistributeCashReceipt.aspx.resx");
					
					strTranXML = objBuilder.GetTransactionLogXML(objNew);
					if (strTranXML!="")
						sbTranXML.Append(strTranXML);
					result	= objDataWrapper.ExecuteNonQuery(strStoredProc); ///,objTransactionInfo);
					objDataWrapper.ClearParameteres();




					//result=objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				sbTranXML.Append("</root>");
				if(TransactionLogRequired)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objDataWrapper.ClearParameteres();
					objTransactionInfo.TRANS_TYPE_ID		=	1;
					objTransactionInfo.RECORDED_BY		=  	intCreatedBy; 

					if(strCalledFrom=="VEN")
					{
						objTransactionInfo.TRANS_DESC		=	"Vendor Invoice distribution has been Inserted successfully";					
						objTransactionInfo.CUSTOM_INFO		=	"Vendor Name:" + VendorName + ";Invoice Number :" + InvoiceNo + ";Total Invoice Amount :" + totalAmount  ;
					}
					else if(strCalledFrom == "CHQ")
					{
						objTransactionInfo.TRANS_DESC		=	"Check distribution has been Inserted successfully";	
						objTransactionInfo.CUSTOM_INFO		=	"Payee Name :" + FetchPayeeName(intLineItemId) + ";Total Check Amount :" + totalAmount  ;
					}
					else if(strCalledFrom == "EditC")
					{
						objTransactionInfo.TRANS_DESC		=	"Check distribution has been Inserted successfully";	
						objTransactionInfo.CUSTOM_INFO		=	"Payee Name :" + FetchPayeeName(intLineItemId) + ";Total Check Amount :" + totalAmount  ;
					}
					else if(strCalledFrom == "DEP")
					{
						ClsDepositDetails obj  = new ClsDepositDetails();
						DataSet dsDeposit  =  obj.GetDepositId(intLineItemId);
						string strDEPOSIT_TYPE = dsDeposit.Tables[0].Rows[0]["DEPOSIT_TYPE"].ToString();
						
						objTransactionInfo.TRANS_DESC		=	"Deposit distribution has been Inserted successfully";	
						objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + FatchDepositNumber(intLineItemId) + ";Total Check Amount :" + totalAmount  + "; Deposit Type : " +strDEPOSIT_TYPE;
					}
					else
					{
						objTransactionInfo.CUSTOM_INFO		=	"Total Amount :" + totalAmount;
						objTransactionInfo.TRANS_DESC		=	"Distribution has been Inserted successfully";	
					}


					objTransactionInfo.CHANGE_XML			=	sbTranXML.ToString();
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				return result;			
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}

//		public static DataTable FetchPagedGLAccounts(int currentPage,int pageSize,out int totalRecords)
//		{
//			
//			try
//			{
//				DataSet dsTemp = new DataSet();		
//				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//				objDataWrapper.AddParameter("@CurrentPage",currentPage,SqlDbType.Int);
//				objDataWrapper.AddParameter("@PageSize",pageSize,SqlDbType.Int);
//				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@TotalRecords",SqlDbType.Int,ParameterDirection.Output);
//				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPagedGLAccounts");
//				totalRecords=int.Parse(objSqlParameter.Value.ToString());						
//				return dsTemp.Tables[0];
//			}
//			catch(Exception exc)
//			{
//				throw (exc);
//			}
//			finally
//			{}	
//		}
		
		/// <summary>
		/// Fetch Payee Name
		/// </summary>
		/// <param name="checkId"></param>
		/// <returns></returns>
		public string  FetchPayeeName(int checkId)
		{
			string	strStoredProc =	"Proc_FetchPayeeName";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CHECK_ID",checkId);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			try
			{
				return ds.Tables[0].Rows[0]["PAYEE_ENTITY_NAME"].ToString();
			}
			catch
			{
				return "";
			}
		}

		public string  FetchPayeeNameCheck(int checkId)
		{
			string	strStoredProc =	"Proc_FetchPayeeNameCheck";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CHECK_ID",checkId);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			try
			{
				return ds.Tables[0].Rows[0]["PAYEE_ENTITY_NAME"].ToString();
			}
			catch
			{
				return "";
			}
		}


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

		public string FatchDepositNumber(int cdLineItem)
		{
			string	strStoredProc =	"Proc_FatchDepositNumber";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CD_LINE_ITEM_ID",cdLineItem);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			try
			{
				return ds.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();
			}
			catch
			{
				return "";
			}

		}
		
		
		public DataSet GetDistributionAmount(int intCD_LINE_ITEM,string calledFrom)
		{
			string	strStoredProc =	"Proc_FetchDistributionDetails";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CD_LINE_ITEM_ID",intCD_LINE_ITEM);
			objWrapper.AddParameter("@CALLED_FROM",calledFrom);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			return ds;
			
		}

        
		/// <summary>
		/// FetchDistributionDetail
		/// </summary>
		/// <param name="intLineItemTypeId"></param>
		/// <param name="strCalledFrom"></param>
		/// <param name="currentPage"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalRecords"></param>
		/// <param name="TotalSumExceptThisPage"></param>
		/// <returns></returns>
		public DataTable FetchDistributionDetail(int intLineItemTypeId,string strCalledFrom,int currentPage,int pageSize,out int totalRecords, out double TotalSumExceptThisPage)
		{			
			try
			{
				DataSet dsTemp = new DataSet();		
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CD_LINE_ITEM_ID",intLineItemTypeId,SqlDbType.Int);
				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom,SqlDbType.VarChar);
				objDataWrapper.AddParameter("@CurrentPage",currentPage,SqlDbType.Int);
				objDataWrapper.AddParameter("@PageSize",pageSize,SqlDbType.Int);
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@TotalRecords",SqlDbType.Int,ParameterDirection.Output);
				dsTemp = objDataWrapper.ExecuteDataSet(objDistribute.GetDistributionDetails);
				totalRecords=int.Parse(objSqlParameter.Value.ToString());
				if(dsTemp.Tables.Count>1)
				{
					TotalSumExceptThisPage = Convert.ToDouble(dsTemp.Tables[1].Rows[0][0]);
				}
				else
					TotalSumExceptThisPage=0;

				return dsTemp.Tables[0];				
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}	
		}

		/// <summary>
		///  Delete Distribution Detail
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		/// dt,int.Parse(hidLineItemId.Value),hidCalledFrom.Value,intFromUserId);
		public int DeleteDistributionDetail(Double totalAmount,DataTable dt,int intLineItemId,string strCalledFrom,int intCreatedBy)
		{			
			int result=0;
			DataSet VendorNameInvoiceNo = FatchVendorNameInvoiceNo(intLineItemId);
			string VendorName ="";
			string InvoiceNo = "";
			//Deposit Distribution 
			string strACCTNO  = "";
			string strAmtPerc = "";
			string strAmtDistributed = "";

			if(VendorNameInvoiceNo.Tables[0].Rows.Count > 0)
			{
				VendorName	=	VendorNameInvoiceNo.Tables[0].Rows[0]["VENDOR_NAME"].ToString();
				InvoiceNo	=	VendorNameInvoiceNo.Tables[0].Rows[0]["INVOICE_NUM"].ToString();				
			}	

			SqlParameter objRetVal = null;

		
			try
			{
				foreach(DataRow dr in dt.Rows)
				{
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@IDEN_ROW_ID",int.Parse(dr["RowId"].ToString()),SqlDbType.Int);
					objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
					
					//Added tran Log.
					if(TransactionLogRequired)
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID		=	1;
						objTransactionInfo.RECORDED_BY		=  	intCreatedBy; 

						//Get Distribution Detail of Deposits
						DataSet dsDeposit  =  GetDistributionAmount(int.Parse(dr["RowId"].ToString()),strCalledFrom);
						if(dsDeposit!=null)
						{
							if(dsDeposit.Tables[0].Rows.Count > 0)
							{
								strACCTNO = dsDeposit.Tables[0].Rows[0]["ACCOUNT_ID"].ToString();
								strAmtPerc = dsDeposit.Tables[0].Rows[0]["DISTRIBUTION_PERCT"].ToString();
								strAmtDistributed = dsDeposit.Tables[0].Rows[0]["DISTRIBUTION_AMOUNT"].ToString();
							}
						}
						


						if(strCalledFrom=="VEN")
						{
							objTransactionInfo.TRANS_DESC		=	"Vendor Invoice distribution has been Deleted successfully";					
							objTransactionInfo.CUSTOM_INFO		=	"Vendor Name :" + VendorName + ";Invoice Number :" + InvoiceNo + ";Total Invoice Amount :" + totalAmount   + ";Account # : " + strACCTNO + ";Percentage : " + strAmtPerc+ ";Amount : " + strAmtDistributed;	
						}
						else if(strCalledFrom == "CHQ")
						{
							objTransactionInfo.TRANS_DESC		=	"Check distribution has been Deleted successfully";	
							objTransactionInfo.CUSTOM_INFO		=	"Payee Name :" + FetchPayeeName(intLineItemId) + ";Total Check Amount :" + totalAmount + ";Account # : " + strACCTNO + ";Percentage : " + strAmtPerc+ ";Amount : " + strAmtDistributed;	
						}
						else if(strCalledFrom == "EditC")
						{
							objTransactionInfo.TRANS_DESC		=	"Check distribution has been Deleted successfully";	
							objTransactionInfo.CUSTOM_INFO		=	"Payee Name :" + FetchPayeeName(intLineItemId) + ";Total Check Amount :" + totalAmount + ";Account # : " + strACCTNO + ";Percentage : " + strAmtPerc+ ";Amount : " + strAmtDistributed;	
						}
						else if(strCalledFrom == "DEP")
						{
							ClsDepositDetails obj  = new ClsDepositDetails();
							DataSet dsDepositDel  =  obj.GetDepositId(intLineItemId);
							string strDEPOSIT_TYPE = dsDepositDel.Tables[0].Rows[0]["DEPOSIT_TYPE"].ToString();
							
							objTransactionInfo.TRANS_DESC		=	"Deposit distribution has been Deleted successfully";	
							objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + FatchDepositNumber(intLineItemId) + ";Total Check Amount :" + totalAmount + ";	Deposit Type : " + strDEPOSIT_TYPE + ";Account # : " + strACCTNO + ";Percentage : " + strAmtPerc+ ";Amount : " + strAmtDistributed;
						}
						else
						{
							objTransactionInfo.CUSTOM_INFO		=	"Total Amount :" + totalAmount;
							objTransactionInfo.TRANS_DESC		=	"Distribution has been deleted successfully";	
						}

						result = objDataWrapper.ExecuteNonQuery(objDistribute.DeleteDistributionDetails,objTransactionInfo);
					}
					else
					{
						result = objDataWrapper.ExecuteNonQuery(objDistribute.DeleteDistributionDetails);
                    }
					result = int.Parse(objRetVal.Value.ToString());

					objDataWrapper.ClearParameteres();
					objDataWrapper.Dispose();

					
				}
				//result = int.Parse(objRetVal.Value.ToString());
				return result;		
				
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
			}	
		}
		

		/// <summary>
		/// Update Distribution Detail
		/// </summary>
		/// <param name="totalAmount"></param>
		/// <param name="CalledFrom"></param>
		/// <param name="oldDt"></param>
		/// <param name="dt"></param>
		/// <param name="intModifiedBy"></param>
		/// <param name="intLineItemId"></param>
		/// <returns></returns>
		public  int UpdateDistributionDetail(Double totalAmount ,string CalledFrom,DataTable oldDt, DataTable dt,int intModifiedBy,int intLineItemId)
		{		
		
			string	strStoredProc = objDistribute.UpdateDistributionDetail;
			int result=0;
			
			//Add by kranti 
			DataSet VendorNameInvoiceNo = FatchVendorNameInvoiceNo(intLineItemId);
			string VendorName = "";
			string InvoiceNo = "" ;

			if(VendorNameInvoiceNo.Tables[0].Rows.Count > 0)
			{
				VendorName	=	VendorNameInvoiceNo.Tables[0].Rows[0]["VENDOR_NAME"].ToString();
				InvoiceNo	=	VendorNameInvoiceNo.Tables[0].Rows[0]["INVOICE_NUM"].ToString();				
			}	
			//end 

			StringBuilder sbTranXML = new StringBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			sbTranXML.Append("<root>");	

			
		
			
			try
			{				
				for(int i=0;i < dt.Rows.Count;i++)
				{
					DataRow dr=dt.Rows[i];
					DataRow oldDr = oldDt.Rows[i];
					//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@IDEN_ROW_ID",int.Parse(dr["IDEN_ROW_ID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@ACCOUNT_ID",int.Parse(dr["ACCOUNT_ID"].ToString()),SqlDbType.Int);
					sbTranXML.Append("Account Id: " + int.Parse(dr["ACCOUNT_ID"].ToString()));	
					if (dr["DISTRIBUTION_PERCT"].ToString() != "" )
					{
						objDataWrapper.AddParameter("@DISTRIBUTION_PERCT",double.Parse(dr["DISTRIBUTION_PERCT"].ToString()),SqlDbType.Decimal);
					}
					else
					{
						objDataWrapper.AddParameter("@DISTRIBUTION_PERCT",null);
					}
					objDataWrapper.AddParameter("@DISTRIBUTION_AMOUNT",double.Parse(dr["DISTRIBUTION_AMOUNT"].ToString()),SqlDbType.Decimal);
					objDataWrapper.AddParameter("@NOTE",dr["NOTE"].ToString(),SqlDbType.NVarChar);
					objDataWrapper.AddParameter("@MODIFIED_BY",intModifiedBy,SqlDbType.Int);
					//objDataWrapper.AddParameter("@CREATED_BY",intCreatedBy,SqlDbType.Int);
					//					if(TransactionLogRequired)
					//{																			
						//						objVendorInvoicesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/DistributeCashReceipt.aspx.resx");
						//						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//						string strTranXML = objBuilder.GetTransactionLogXML(objVendorInvoicesInfo);
					string strTranXML = "";
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

											
					ClsDistributeCheckInfo objOld= this.GenerateTranObjectUpdate(oldDr);					
					ClsDistributeCheckInfo objNew= this.GenerateTranObjectUpdate(dr);
					objNew.TransactLabel = BlCommon.ClsCommon.MapTransactionLabel("Account/Aspx/DistributeCashReceipt.aspx.resx");
					
					strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);
						if (strTranXML!="")
							sbTranXML.Append(strTranXML);
						//objTransactionInfo.TRANS_TYPE_ID	=	1;
						//objTransactionInfo.RECORDED_BY		=	intModifiedBy; 
						//objTransactionInfo.TRANS_DESC		=	"Vendor Distribution has been updated successfully";
						//						objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						result	= objDataWrapper.ExecuteNonQuery(strStoredProc); ///,objTransactionInfo);
						objDataWrapper.ClearParameteres();
				}
				sbTranXML.Append("</root>");
				if(TransactionLogRequired)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objDataWrapper.ClearParameteres();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	intModifiedBy; 
					//objTransactionInfo.TRANS_DESC		=	"Distribution has been updated successfully";
					//objTransactionInfo.CUSTOM_INFO		=	"Total Amount :" + totalAmount;


					if(CalledFrom=="VEN")
					{
						objTransactionInfo.TRANS_DESC		=	"Vendor Invoice distribution has been updated successfully";					
						objTransactionInfo.CUSTOM_INFO		=	"Vendor Name:" + VendorName + ";Invoice Number :" + InvoiceNo + ";Total Invoice Amount :" + totalAmount  ;
					}
					else if(CalledFrom == "CHQ")
					{
						objTransactionInfo.TRANS_DESC		=	"Check distribution has been updated successfully";	
						objTransactionInfo.CUSTOM_INFO		=	"Payee Name :" + FetchPayeeName(intLineItemId) + ";Total Check Amount :" + totalAmount  ;
					}
					else if(CalledFrom == "DEP")
					{
						ClsDepositDetails obj  = new ClsDepositDetails();
						DataSet dsDeposit  =  obj.GetDepositId(intLineItemId);
						string strDEPOSIT_TYPE = dsDeposit.Tables[0].Rows[0]["DEPOSIT_TYPE"].ToString();
						
						objTransactionInfo.TRANS_DESC		=	"Deposit distribution has been updated successfully";	
						objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + FatchDepositNumber(intLineItemId) + ";Total Check Amount :" + totalAmount + "; Deposit Type : " + strDEPOSIT_TYPE ;
					}
					else
					{
						objTransactionInfo.TRANS_DESC		=	"Distribution has been updated successfully";	
						objTransactionInfo.CUSTOM_INFO		=	"Total Amount :" + totalAmount;
					}

					objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				return result;			
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}
		
	}


	public abstract class Distribute
	{
		public string GetDistributionDetails ="";
		public string InsertDistributionDetail ="";
		public string DeleteDistributionDetails ="";
		public string UpdateDistributionDetail ="";
	}
	public class TempDistribute : Distribute
	{
		public TempDistribute()
		{
			GetDistributionDetails ="Proc_GetTempDistributionDetails";
			InsertDistributionDetail ="Proc_InsertTempDistributionDetail";
			DeleteDistributionDetails ="Proc_DeleteTempDistributionDetails";
			UpdateDistributionDetail ="Proc_UpdateTempDistributionDetail";
		}
	}
	public class ActualDistribute : Distribute
	{
		public ActualDistribute()
		{
			GetDistributionDetails ="Proc_GetDistributionDetails";
			InsertDistributionDetail ="Proc_InsertDistributionDetail";
			DeleteDistributionDetails ="Proc_DeleteDistributionDetails";
			UpdateDistributionDetail ="Proc_UpdateDistributionDetail";
		}
	}
}
