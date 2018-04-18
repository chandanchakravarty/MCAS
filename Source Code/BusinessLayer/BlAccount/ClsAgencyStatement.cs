using System;
using System.Data;
using Cms.DataLayer;
using System.Collections;
using System.Data.SqlClient;

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Summary description for ClsAgencyStatement.
	/// </summary>
	/// 
	

	public class ClsAgencyStatement :Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private  const string TYPE_REG = "REG";
		private  const string TYPE_ADC = "ADC";
		private  const string TYPE_COMP = "COMP";    
		private			bool		boolTransactionLog;
		public ClsAgencyStatement()
		{
			//
			// TODO: Add constructor logic here
			//
			boolTransactionLog	= base.TransactionLogRequired;
		}
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
		
		/// <summary>
		/// Saves the details of agency statement of specified month in database
		/// </summary>
		/// <param name="MonthNumber">Month number</param>
		/// <returns>1 Sucessfull else 0</returns>
		public int SaveAgencyStatement(int MonthNumber,int YearNumber,string commissionType,int userID)
		{
			string strSql = "Proc_SaveAgencyStatement";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objWrapper.AddParameter("@MONTH", MonthNumber);
				objWrapper.AddParameter("@YEAR", YearNumber);
				objWrapper.AddParameter("@COMM_TYPE",commissionType);
				SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@RETVALUE",null,SqlDbType.Int,ParameterDirection.Output);
                
				/*objWrapper.ExecuteNonQuery("Proc_SaveAgencyStatement");
				if(objSqlParameter!=null && objSqlParameter.Value!=null && objSqlParameter.Value.ToString()!="")
					RetVal =  int.Parse(objSqlParameter.Value.ToString());
				else
					RetVal = 0;//Exists
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return RetVal ; */

				//Trans Log
				int RetVal = 0; 
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;					
						objTransactionInfo.RECORDED_BY		=	userID;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1656", ""); //"Agency Statement processed";
						objTransactionInfo.CUSTOM_INFO		=	"Commision Type :"+commissionType+ "; Month :"+ MonthNumber +"; Year :" + YearNumber;
						returnResult	=	objWrapper.ExecuteNonQuery(strSql,objTransactionInfo);
					
				}
				else
				{
					returnResult	=	objWrapper.ExecuteNonQuery(strSql);
				}


				if(objSqlParameter!=null && objSqlParameter.Value!=null && objSqlParameter.Value.ToString()!="")
					RetVal =  int.Parse(objSqlParameter.Value.ToString());
				else
					RetVal = 0;//Exists


				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return RetVal;
				

			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(new Exception("Error occured in SaveAgencyStatement.\n" 
					+ objExp.Message.ToString()));
			}
		}

	///added by uday to get current & previous years in drop down
		public int GetYearAgencyStatement(int YearNumber,string CommissionType)
		{
		  Cms.DataLayer.DataWrapper objWrapper = null;
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@YEAR", YearNumber);
				objWrapper.AddParameter("@COMM_TYPE",CommissionType);

				SqlParameter objSqlParameter =  (SqlParameter) objWrapper.AddParameter("@RETVALUE",null,SqlDbType.Int,ParameterDirection.Output);

				objWrapper.ExecuteNonQuery("Proc_GetYearAgencyStatement");
				if(objSqlParameter!=null && objSqlParameter.Value!=null && objSqlParameter.Value.ToString()!="")
					return int.Parse(objSqlParameter.Value.ToString());
				else
					return 0;
			}
			catch (Exception objExp)
			{
				throw(new Exception("Error occured in GetYearAgencyStatement.\n" 
					+ objExp.Message.ToString()));
			}
		}
///uday
		/// <summary>
		/// Returns the details of agency statement
		/// </summary>
		/// <param name="AgencyId"></param>
		/// <param name="MonthNumber"></param>
		/// <returns></returns>
		public DataSet GetAgencyStatement(int AgencyId, int MonthNumber,int MonthYear,string commType)
		{	
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@AGENCY_ID", AgencyId);
				objWrapper.AddParameter("@MONTH", MonthNumber);
				objWrapper.AddParameter("@YEAR", MonthYear);
				objWrapper.AddParameter("@COMM_TYPE", commType);
				return objWrapper.ExecuteDataSet("Proc_GetAgencyStatement");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetAgencyStatement \n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		public  DataSet GetCreditCardSweepHistoryDetails(string DateFromSpool,string DateToSpool, string DateFromSweep,string  DateToSweep,string ProcessStatus,string TransactionAmount,string Users)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				if(DateFromSpool!="")
					objWrapper.AddParameter("@FROM_DATE_SPOOL ",DateFromSpool);
				else
					objWrapper.AddParameter("@FROM_DATE_SPOOL ",null);
				
				if(DateToSpool!="")
					objWrapper.AddParameter("@TO_DATE_SPOOL",DateToSpool);
				else
					objWrapper.AddParameter("@TO_DATE_SPOOL",null);
				
				if(DateFromSweep!="")
					objWrapper.AddParameter("@FROM_DATE_SWEEP",DateFromSweep);
				else
					objWrapper.AddParameter("@FROM_DATE_SWEEP",null);
				
				if(DateToSweep!="")
					objWrapper.AddParameter("@TO_DATE_SWEEP",DateToSweep);
				else
					objWrapper.AddParameter("@TO_DATE_SWEEP",null);				
				if(ProcessStatus!="")
					objWrapper.AddParameter("@PROCESS_STATUS",ProcessStatus);
				else
					objWrapper.AddParameter("@PROCESS_STATUS",null);	

				if(TransactionAmount!="")
					objWrapper.AddParameter("@TRANSACTION_AMOUNT",TransactionAmount);
				else
					objWrapper.AddParameter("@TRANSACTION_AMOUNT",null);	

				//Added By Raghav For itrack Issue4646
				if(Users!="")
					objWrapper.AddParameter("@USERS",Users);
				else
					objWrapper.AddParameter("@USERS",null);

				return objWrapper.ExecuteDataSet("Proc_GetCreditCardSweepHistory");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetCreditCardSweepHistoryDetails \n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}

		}

		//Added By Swarup 27-feb-2008

        public DataSet GetRTLImportHistoryDetails(DateTime DateFrom, DateTime DateTo, string DepositNumber, string ProcessStatus, string DepositType)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
                if (DateFrom.ToString() != "" && DateFrom != Convert.ToDateTime(null))
                    objWrapper.AddParameter("@FROM_DATE ", DateFrom, SqlDbType.DateTime);
				else
                    objWrapper.AddParameter("@FROM_DATE ", System.DBNull.Value, SqlDbType.DateTime);

                if (DateTo.ToString() != "" && DateTo != Convert.ToDateTime(null))
					objWrapper.AddParameter("@TO_DATE",DateTo, SqlDbType.DateTime);
				else
                    objWrapper.AddParameter("@TO_DATE", System.DBNull.Value, SqlDbType.DateTime);
				if(DepositNumber!="")
					objWrapper.AddParameter("@DEPOSIT_NUMBER",DepositNumber);
				else
					objWrapper.AddParameter("@DEPOSIT_NUMBER",null);	


				if(ProcessStatus!="")
					objWrapper.AddParameter("@PROCESS_STATUS",ProcessStatus);
				else
					objWrapper.AddParameter("@PROCESS_STATUS",null);

                if (DepositType != "")
                    objWrapper.AddParameter("@DEPOSITE_TYPE", DepositType);
                else
                    objWrapper.AddParameter("@DEPOSITE_TYPE", null);	


                objWrapper.AddParameter("@LANG_ID", Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID);	
				
				return objWrapper.ExecuteDataSet("Proc_GetRTLImportHistory");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetRTLImportHistoryDetails \n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}

		}
		//Added By Raghav For Itrack Issue#4646
		public DataSet GetMntUsers(string SystemId)
		{	
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@USER_SYSTEM_ID", SystemId);
				return objWrapper.ExecuteDataSet("Proc_GetMntUsers");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetMntUsers \n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		public  DataSet GetCustomerAgencyPaymentsHistory(string FromDate,string ToDate, string PolicyNo,string  Agency,string Customer,string Amount)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				if(FromDate!="")
					objWrapper.AddParameter("@FROM_DATE ",FromDate);
				else
					objWrapper.AddParameter("@FROM_DATE ",null);
				
				if(ToDate!="")
					objWrapper.AddParameter("@TO_DATE",ToDate);
				else
					objWrapper.AddParameter("@TO_DATE",null);
				
				if(PolicyNo!="")
					objWrapper.AddParameter("@POLICY_NUMBER",PolicyNo);
				else
					objWrapper.AddParameter("@POLICY_NUMBER",null);
				
				if(Agency!="")
					objWrapper.AddParameter("@AGENCY_ID",Agency);
				else
					objWrapper.AddParameter("@AGENCY_ID",null);				
				if(Customer!="")
					objWrapper.AddParameter("@CUSTOMER_ID",Customer);
				else
					objWrapper.AddParameter("@CUSTOMER_ID",null);	
				if(Amount!="")
					objWrapper.AddParameter("@AMOUNT",Amount);
				else
					objWrapper.AddParameter("@AMOUNT",null);	

				

				return objWrapper.ExecuteDataSet("Proc_GetCustomerAgencyPaymentsHistory");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetCustomerAgencyPaymentsHistory \n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}

		}
		public DataSet GetBudgetCategoryDetails(int BudgetCategoryId, int MonthNumber,int MonthYear)
		{	
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@BUDGETCATEGORY_ID", BudgetCategoryId);
				objWrapper.AddParameter("@MONTH", MonthNumber);
				objWrapper.AddParameter("@YEAR", MonthYear);
				//objWrapper.AddParameter("@COMM_TYPE", commType);
				return objWrapper.ExecuteDataSet("Proc_BudgetCategoryDetails");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetBudgetCategoryDetails \n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		public  DataSet GetSweepHistoryDetailsEFT(string DateFromSpool,string DateToSpool, string DateFromSweep,string  DateToSweep,string EntityType,string TransactionAmount)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				if(DateFromSpool!="")
					objWrapper.AddParameter("@FROM_DATE_SPOOL ",DateFromSpool);
				else
					objWrapper.AddParameter("@FROM_DATE_SPOOL ",null);
				
				if(DateToSpool!="")
					objWrapper.AddParameter("@TO_DATE_SPOOL",DateToSpool);
				else
					objWrapper.AddParameter("@TO_DATE_SPOOL",null);
				
				if(DateFromSweep!="")
					objWrapper.AddParameter("@FROM_DATE_SWEEP",DateFromSweep);
				else
					objWrapper.AddParameter("@FROM_DATE_SWEEP",null);
				
				if(DateToSweep!="")
					objWrapper.AddParameter("@TO_DATE_SWEEP",DateToSweep);
				else
					objWrapper.AddParameter("@TO_DATE_SWEEP",null);				
				
				if(EntityType!="")
					objWrapper.AddParameter("@ENTITY_TYPE",EntityType);
				else
					objWrapper.AddParameter("@ENTITY_TYPE",null);

				if(TransactionAmount!="")
					objWrapper.AddParameter("@TRANSACTION_AMOUNT",TransactionAmount);
				else
					objWrapper.AddParameter("@TRANSACTION_AMOUNT",null);	

				return objWrapper.ExecuteDataSet("Proc_GetEFTSweepHistory");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetSweepHistoryDetailsEFT \n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}

		}
		

		/// <summary>
		/// Returns the months not used for generating agenct statement
		/// </summary>
		/// <returns>Ilist interface object</returns>
		public DataTable GetMonths(int yearno,string commType)
		{			
			//int currYear = System.DateTime.Now.Year;
			//int prevYear = currYear -1;			
			int intYear =yearno;
			try
			{
				DataTable dt = DataWrapper.ExecuteDataset(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.Text, "SELECT ISNULL(max(MONTH_NUMBER),0) AS MONTH_NUMBER FROM ACT_AGENCY_STATEMENT WHERE MONTH_YEAR = " + intYear + " and MONTH_NUMBER is not null and COMM_TYPE = '" + commType + "' order by MONTH_NUMBER").Tables[0];
				
				DataTable dtNew = new DataTable();
				DataRow dr;

				dtNew.Columns.Add("Month_Name");
				dtNew.Columns.Add("Month_Index");
                
				/*
				dtNew.Columns.Add("MONTH_YEAR");
				dtNew.Columns.Add("Year_INDEX");
				*/
				int intMonth = int.Parse(dt.Rows[0][0].ToString());
			
				for (int ctr= intMonth + 1 ; ctr<=12; ctr++)
				{
					//if (dt.Select("MONTH_NUMBER=" + ctr.ToString()).Length != 0)
					//{
						dr = dtNew.NewRow();
						dr["Month_Name"] = GetMonth(ctr);
						dr["Month_Index"] = ctr;
						dtNew.Rows.Add(dr);
					//}
				}

				return dtNew;
			}
			catch(Exception objExp)
			{
				throw(new Exception("Error occured in GetMonths function. \n " + objExp.Message.ToString()));
			}
		}

		private string GetMonth(int MonthNumber)
		{
			switch(MonthNumber)
			{
				case 1:
					return "Jan";
					break;
				case 2:
					return "Feb";
					break;
				case 3:
					return "Mar";
					break;
				case 4:
					return "Apr";
					break;
				case 5:
					return "May";
					break;
				case 6:
					return "Jun";
					break;
				case 7:
					return "Jul";
					break;
				case 8:
					return "Aug";
					break;
				case 9:
					return "Sep";
					break;
				case 10:
					return "Oct";
					break;
				case 11:
					return "Nov";
					break;
				case 12:
					return "Dec";
					break;
			}
			return "";
		}

	}
}
