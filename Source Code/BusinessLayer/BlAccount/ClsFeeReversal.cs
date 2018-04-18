using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Account;
using System.Collections;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Summary description for ClsFeeReversal.
	/// </summary>
	public class ClsFeeReversal : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		public ClsFeeReversal()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		

		public void ReverseLateFees(int CustomerID , int PolicyID , int PolicyVersionID , DataWrapper objWrapper)
		{
			objWrapper.ClearParameteres();

			objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objWrapper.AddParameter("@POLICY_ID",PolicyID );
			objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
			objWrapper.ExecuteNonQuery("Proc_ReverseLateFees");
			objWrapper.ClearParameteres();
		}

		public static DataSet GetFeesDetail(string policyNo,string feeType,string fromDate,string toDate)
		{
			string strSql = "Proc_FetchFeeInformations";

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			if(policyNo!=null && policyNo.Length<=0)
				objDataWrapper.AddParameter("@POLICYNO",DBNull.Value);
			else
				objDataWrapper.AddParameter("@POLICYNO",policyNo);
			
			if(fromDate!=null && fromDate.Length<=0)
				objDataWrapper.AddParameter("@FROMDATE",DBNull.Value);
			else
				objDataWrapper.AddParameter("@FROMDATE",fromDate);

			if(toDate!=null && toDate.Length<=0)
				objDataWrapper.AddParameter("@TODATE",DBNull.Value);
			else
				objDataWrapper.AddParameter("@TODATE",toDate);					

			if(feeType!=null && feeType.Length<=0)
				objDataWrapper.AddParameter("@FEETYPE",DBNull.Value);
			else
				objDataWrapper.AddParameter("@FEETYPE",feeType);
			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}


		public int AddFeeReversalRecords(ArrayList al, out ArrayList arrSavedRecords)
		{
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);			

			bool SaveStatus = true;

			int intRetVal;
			arrSavedRecords = new ArrayList();
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					if (((ClsFeeReversalInfo)al[i]).IDEN_ROW_ID <= 0)
					{
						intRetVal = Add(objWrapper, (ClsFeeReversalInfo)al[i]);
						
						if (intRetVal <= 0 )
						{
							//Some error occured, hence updating the save status flag
							SaveStatus = false;
						}
						else
						{
							arrSavedRecords.Add(al[i]);
							//arrSavedRecords.Add(((ClsFeeReversalInfo)al[i]).FEES_REVERSE);
						}
					}
					else
					{
						intRetVal = Update(objWrapper,(ClsFeeReversalInfo)al[i]);
						if (intRetVal <= 0 )

						{
							//Some error occured, hence updating the save status flag
							SaveStatus = false;
						}
						else
						{
							arrSavedRecords.Add(((ClsFeeReversalInfo)al[i]).IDEN_ROW_ID);
							arrSavedRecords.Add(((ClsFeeReversalInfo)al[i]).FEES_REVERSE);
						}
					}
				}

				if (SaveStatus == false)
				{
					//Some error occured while saving deposit details, hence rollbacking
					objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
					return -1;
				}				
				
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				return 1;
			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		
		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ClsFeeReversalInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add( DataLayer.DataWrapper objDataWrapper,ClsFeeReversalInfo ClsFeeReversalInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_FEE_REVERSAL";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				objDataWrapper.ClearParameteres();				
				objDataWrapper.AddParameter("@FEE_TYPE",ClsFeeReversalInfo.FEE_TYPE);
				objDataWrapper.AddParameter("@FEES_AMOUNT",ClsFeeReversalInfo.FEES_AMOUNT);
				objDataWrapper.AddParameter("@FEES_REVERSE",ClsFeeReversalInfo.FEES_REVERSE);
				objDataWrapper.AddParameter("@IS_ACTIVE",ClsFeeReversalInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",ClsFeeReversalInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",ClsFeeReversalInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@CUSTOMER_OPEN_ITEM_ID",ClsFeeReversalInfo.CUSTOMER_OPEN_ITEM_ID);				
				objDataWrapper.AddParameter("@CUSTOMER_ID",ClsFeeReversalInfo.CUSTOMER_ID);	
				objDataWrapper.AddParameter("@POLICY_ID",ClsFeeReversalInfo.POLICY_ID);	
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",ClsFeeReversalInfo.POLICY_VERSION_ID);					
				//				objDataWrapper.AddParameter("@IS_COMMITTED",ClsFeeReversalInfo.IS_COMMITTED);					
				objDataWrapper.AddParameter("@IS_COMMITTED",0);					
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);						
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@IDEN_ROW_ID",ClsFeeReversalInfo.IDEN_ROW_ID,SqlDbType.Int,ParameterDirection.Output);
				int returnResult = 0;				
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);				
				int intIDEN_ROW_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				
				if (intIDEN_ROW_ID == -1)
				{
					return -1;
				}
				else
				{
					ClsFeeReversalInfo.IDEN_ROW_ID = intIDEN_ROW_ID;
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#endregion

		private int Update(DataLayer.DataWrapper objDataWrapper, ClsFeeReversalInfo ClsFeeReversalInfo)
		{
			string strStoredProc = "Proc_UpdateACT_FEE_REVERSAL";
			int returnResult = 0;			
			try
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@FEES_REVERSE",ClsFeeReversalInfo.FEES_REVERSE);
				objDataWrapper.AddParameter("@MODIFIED_BY",ClsFeeReversalInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ClsFeeReversalInfo.LAST_UPDATED_DATETIME);						
				objDataWrapper.AddParameter("@IDEN_ROW_ID",ClsFeeReversalInfo.IDEN_ROW_ID);		
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				objDataWrapper.ClearParameteres();		
				return returnResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}

		
		
		/// <summary>
		///  Delete records from ACT_FEE_REVERSAL table
		/// </summary>
		/// <param name="dt"></param>
		public void Delete(DataTable dt)
		{
			
			try
			{
				string deleteProcName = "PROC_DELETEACT_FEE_REVERSAL";
				foreach(DataRow dr in dt.Rows)
				{
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@IDEN_ROW_ID",int.Parse(dr["RowId"].ToString()),SqlDbType.Int);
					int result = objDataWrapper.ExecuteNonQuery(deleteProcName);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{}
		}
		
		/*public int Reverse(ArrayList arrItems)
		{
			int result =0;
			try
			{
				string processProcName = "PROC_PROCESS_ACT_FEE_REVERSAL";				
				for(int i=0; i < arrItems.Count ; i++)
				{
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@IDEN_ROW_ID",Convert.ToInt32(arrItems[i].ToString()) ,SqlDbType.Int);
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESULT",null,SqlDbType.Int,ParameterDirection.Output);
					result = objDataWrapper.ExecuteNonQuery(processProcName);
					result = int.Parse(objSqlParameter.Value.ToString());
				}
				return result;
			}
			catch(Exception ex)
			{ 
				throw(ex);
			}
			finally
			{}
		}*/
		public int Reverse(ArrayList arrItems,string PolNum)
		{
			int result =0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				string processProcName = "PROC_PROCESS_ACT_FEE_REVERSAL";	
				ClsFeeReversalInfo objFeeReversalInfo	= null;
				for(int i=0; i < arrItems.Count ; i++)
				{
					objFeeReversalInfo=(ClsFeeReversalInfo)arrItems[i];					
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@IDEN_ROW_ID",objFeeReversalInfo.IDEN_ROW_ID,SqlDbType.Int);
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESULT",null,SqlDbType.Int,ParameterDirection.Output);
					string strTranXML = "Policy Number : " + PolNum + ";Fee Reversed  : " + objFeeReversalInfo.FEES_REVERSE;
					if(TransactionLogRequired)
					{
						objFeeReversalInfo.TransactLabel  = ClsCommon.MapTransactionLabel("/Account/Aspx/FeeReversal.aspx.resx");
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objFeeReversalInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1715", "");// "Fee Reversal done successfully.";
						objTransactionInfo.CUSTOM_INFO		=	strTranXML;

						result	= objDataWrapper.ExecuteNonQuery(processProcName,objTransactionInfo);
					}
					else
					{
						result	= objDataWrapper.ExecuteNonQuery(processProcName);
					}
					result = int.Parse(objSqlParameter.Value.ToString());					
				}
				objDataWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				return result;
			}
			catch(Exception ex)
			{ 
				objDataWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{}
		}
		#region Recharge Fee
		public int Recharge(int idenRow,double insFee,string PolNum,int userID)
		{
			int result =0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
					string processProcName = "Proc_UpdateRechargeFee";	

					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@IDEN_ROW_ID",idenRow,SqlDbType.Int);
					objDataWrapper.AddParameter("@INS_FEE",insFee,SqlDbType.Int);
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESULT",null,SqlDbType.Int,ParameterDirection.Output);
					string strTranXML = "Policy Number : " + PolNum + ";Fee Recharged  : " + insFee;
					if(TransactionLogRequired)
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	userID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1716", "");// "Fee Recharged successfully.";
						objTransactionInfo.CUSTOM_INFO		=	strTranXML;

						result	= objDataWrapper.ExecuteNonQuery(processProcName,objTransactionInfo);
					}
					else
					{
						result	= objDataWrapper.ExecuteNonQuery(processProcName);
					}
					result = int.Parse(objSqlParameter.Value.ToString());					
				
					objDataWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
					return result;
			}
			catch(Exception ex)
			{ 
				objDataWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{}
		}
		#endregion
	}
}







