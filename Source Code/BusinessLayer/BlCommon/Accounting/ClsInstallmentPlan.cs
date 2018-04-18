/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	6/7/2005 1:20:29 PM
<End Date				: -	
<Description				: - 	Business logic class for Installment plan screen.
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
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Maintenance.Accounting;

namespace Cms.BusinessLayer.BlCommon.Accounting
{
	/// <summary>
	/// Business logic class for Installment plan screen.
	/// </summary>
	public class ClsInstallmentPlan : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const string ACT_INSTALL_PLAN_DETAIL			=	"ACT_INSTALL_PLAN_DETAIL";
		private const string GET_PLAN_INFO = "Proc_GetACT_INSTALL_PLAN_DETAIL";
		private static int fullPayPlanId;

		public static int  FullPayPlanID()
		{
			return fullPayPlanId;
		}
		
		
	
		static ClsInstallmentPlan()
		{

			//Retreiving the full pay plan installment id and saving it static variable
			try
			{
				string PlanID = DataWrapper.ExecuteScalar(ConnStr
					, CommandType.Text
					, "SELECT IDEN_PLAN_ID FROM ACT_INSTALL_PLAN_DETAIL WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0) = 1").ToString();

				if (PlanID != "")
					fullPayPlanId = int.Parse(PlanID);
			}
			catch 
			{
				fullPayPlanId = 0;
			}
				
		}

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_INSTALL_PLAN_DETAIL";
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
		public ClsInstallmentPlan()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objInstallmentPlanInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsInstallmentPlanInfo objInstallmentPlanInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_INSTALL_PLAN_DETAIL";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@PLAN_CODE",objInstallmentPlanInfo.PLAN_CODE);
				objDataWrapper.AddParameter("@PLAN_DESCRIPTION",objInstallmentPlanInfo.PLAN_DESCRIPTION);
				objDataWrapper.AddParameter("@PLAN_TYPE",objInstallmentPlanInfo.PLAN_TYPE);
				objDataWrapper.AddParameter("@NO_OF_PAYMENTS",objInstallmentPlanInfo.NO_OF_PAYMENTS);
				objDataWrapper.AddParameter("@MONTHS_BETWEEN",objInstallmentPlanInfo.MONTHS_BETWEEN);
				
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN1",objInstallmentPlanInfo.PERCENT_BREAKDOWN1);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN2",objInstallmentPlanInfo.PERCENT_BREAKDOWN2);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN3",objInstallmentPlanInfo.PERCENT_BREAKDOWN3);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN4",objInstallmentPlanInfo.PERCENT_BREAKDOWN4);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN5",objInstallmentPlanInfo.PERCENT_BREAKDOWN5);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN6",objInstallmentPlanInfo.PERCENT_BREAKDOWN6);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN7",objInstallmentPlanInfo.PERCENT_BREAKDOWN7);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN8",objInstallmentPlanInfo.PERCENT_BREAKDOWN8);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN9",objInstallmentPlanInfo.PERCENT_BREAKDOWN9);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN10",objInstallmentPlanInfo.PERCENT_BREAKDOWN10);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN11",objInstallmentPlanInfo.PERCENT_BREAKDOWN11);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN12",objInstallmentPlanInfo.PERCENT_BREAKDOWN12);

				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP1",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP1);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP2",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP2);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP3",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP3);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP4",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP4);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP5",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP5);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP6",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP6);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP7",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP7);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP8",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP8);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP9",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP9);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP10",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP10);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP11",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP11);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP12",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP12);


				objDataWrapper.AddParameter("@CREATED_BY",objInstallmentPlanInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objInstallmentPlanInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@INSTALLMENT_FEES",objInstallmentPlanInfo.INSTALLMENT_FEES);				
				
				if(objInstallmentPlanInfo.LATE_FEES==0.0)
					objDataWrapper.AddParameter("@LATE_FEES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@LATE_FEES",objInstallmentPlanInfo.LATE_FEES);
				/* --as these fields are removed from billing plan page //by pravesh
				if(objInstallmentPlanInfo.SERVICE_CHARGE==0.0)
					objDataWrapper.AddParameter("@SERVICE_CHARGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@SERVICE_CHARGE",objInstallmentPlanInfo.SERVICE_CHARGE);

				if(objInstallmentPlanInfo.CONVENIENCE_FEES==0.0)
					objDataWrapper.AddParameter("@CONVENIENCE_FEES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@CONVENIENCE_FEES",objInstallmentPlanInfo.CONVENIENCE_FEES);
				*/
				objDataWrapper.AddParameter("@GRACE_PERIOD",objInstallmentPlanInfo.GRACE_PERIOD);
				objDataWrapper.AddParameter("@NON_SUFFICIENT_FUND_FEES",objInstallmentPlanInfo.NON_SUFFICIENT_FUND_FEES);
				objDataWrapper.AddParameter("@REINSTATEMENT_FEES",objInstallmentPlanInfo.REINSTATEMENT_FEES);
				objDataWrapper.AddParameter("@MIN_INSTALLMENT_AMOUNT",objInstallmentPlanInfo.MIN_INSTALLMENT_AMOUNT);
				objDataWrapper.AddParameter("@AMTUNDER_PAYMENT",objInstallmentPlanInfo.AMTUNDER_PAYMENT);
				objDataWrapper.AddParameter("@MINDAYS_PREMIUM",objInstallmentPlanInfo.MINDAYS_PREMIUM);
				objDataWrapper.AddParameter("@MINDAYS_CANCEL",objInstallmentPlanInfo.MINDAYS_CANCEL);
				objDataWrapper.AddParameter("@POST_PHONE",objInstallmentPlanInfo.POST_PHONE);
				objDataWrapper.AddParameter("@POST_CANCEL",objInstallmentPlanInfo.POST_CANCEL);
				objDataWrapper.AddParameter("@DEFAULT_PLAN",objInstallmentPlanInfo.DEFAULT_PLAN);
				//added by pravesh on 30 nov 2006 for plan payment mode and applicable premium
				objDataWrapper.AddParameter("@APPLABLE_POLTERM",objInstallmentPlanInfo.APPLABLE_POLTERM);
				objDataWrapper.AddParameter("@PLAN_PAYMENT_MODE",objInstallmentPlanInfo.PLAN_PAYMENT_MODE);
				objDataWrapper.AddParameter("@NO_INS_DOWNPAY",objInstallmentPlanInfo.NO_INS_DOWNPAY);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY",objInstallmentPlanInfo.MODE_OF_DOWNPAY);
				objDataWrapper.AddParameter("@NO_INS_DOWNPAY_RENEW",objInstallmentPlanInfo.NO_INS_DOWNPAY_RENEW);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY_RENEW",objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW);

				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY1",objInstallmentPlanInfo.MODE_OF_DOWNPAY1);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY2",objInstallmentPlanInfo.MODE_OF_DOWNPAY2);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY_RENEW1",objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW1);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY_RENEW2",objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW2);
				objDataWrapper.AddParameter("@PAST_DUE_RENEW",objInstallmentPlanInfo.PAST_DUE_RENEW);
				objDataWrapper.AddParameter("@DAYS_DUE_PREM_NOTICE_PRINTD",objInstallmentPlanInfo.DAYS_DUE_PREM_NOTICE_PRINTD);
                objDataWrapper.AddParameter("@DAYS_SUBSEQUENT_INSTALLMENTS", objInstallmentPlanInfo.DAYS_SUBSEQUENT_INSTALLMENTS);
                objDataWrapper.AddParameter("@INTREST_RATES", objInstallmentPlanInfo.INTREST_RATES);
                objDataWrapper.AddParameter("@SUBSEQUENT_INSTALLMENTS_OPTION", objInstallmentPlanInfo.SUBSEQUENT_INSTALLMENTS_OPTION);
                objDataWrapper.AddParameter("@BASE_DATE_DOWNPAYMENT", objInstallmentPlanInfo.BASE_DATE_DOWNPAYMENT);
                objDataWrapper.AddParameter("@DUE_DAYS_DOWNPYMT", objInstallmentPlanInfo.DUE_DAYS_DOWNPYMT);
                objDataWrapper.AddParameter("@BDATE_INSTALL_NXT_DOWNPYMT", objInstallmentPlanInfo.BDATE_INSTALL_NXT_DOWNPYMT);
                objDataWrapper.AddParameter("@DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT", objInstallmentPlanInfo.DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT);
                objDataWrapper.AddParameter("@DOWN_PAYMENT_PLAN", objInstallmentPlanInfo.DOWN_PAYMENT_PLAN);
                objDataWrapper.AddParameter("@DOWN_PAYMENT_PLAN_RENEWAL", objInstallmentPlanInfo.DOWN_PAYMENT_PLAN_RENEWAL);
                objDataWrapper.AddParameter("@APPLICABLE_LOB", objInstallmentPlanInfo.APPLICABLE_LOB);
                objDataWrapper.AddParameter("@RECVE_NOTIFICATION_DOC", objInstallmentPlanInfo.RECVE_NOTIFICATION_DOC);//Added by Pradeep
                 
				//
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@IDEN_PLAN_ID",objInstallmentPlanInfo.IDEN_PLAN_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objInstallmentPlanInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Accounting/AddInstallmentPlan.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInstallmentPlanInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	235;
					objTransactionInfo.RECORDED_BY		=	objInstallmentPlanInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (returnResult > 0)
				{
					objInstallmentPlanInfo.IDEN_PLAN_ID = int.Parse(objSqlParameter.Value.ToString());
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
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldInstallmentPlanInfo">Model object having old information</param>
		/// <param name="objInstallmentPlanInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsInstallmentPlanInfo objOldInstallmentPlanInfo,ClsInstallmentPlanInfo objInstallmentPlanInfo)
		{
			string strTranXML;
			string strStoredProc = "Proc_UpdateACT_INSTALL_PLAN_DETAIL";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@PLAN_CODE",objInstallmentPlanInfo.PLAN_CODE);
				objDataWrapper.AddParameter("@PLAN_DESCRIPTION",objInstallmentPlanInfo.PLAN_DESCRIPTION);
				//objDataWrapper.AddParameter("@PLAN_TYPE",objInstallmentPlanInfo.PLAN_TYPE);
				objDataWrapper.AddParameter("@NO_OF_PAYMENTS",objInstallmentPlanInfo.NO_OF_PAYMENTS);
				objDataWrapper.AddParameter("@MONTHS_BETWEEN",objInstallmentPlanInfo.MONTHS_BETWEEN);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN1",objInstallmentPlanInfo.PERCENT_BREAKDOWN1);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN2",objInstallmentPlanInfo.PERCENT_BREAKDOWN2);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN3",objInstallmentPlanInfo.PERCENT_BREAKDOWN3);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN4",objInstallmentPlanInfo.PERCENT_BREAKDOWN4);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN5",objInstallmentPlanInfo.PERCENT_BREAKDOWN5);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN6",objInstallmentPlanInfo.PERCENT_BREAKDOWN6);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN7",objInstallmentPlanInfo.PERCENT_BREAKDOWN7);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN8",objInstallmentPlanInfo.PERCENT_BREAKDOWN8);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN9",objInstallmentPlanInfo.PERCENT_BREAKDOWN9);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN10",objInstallmentPlanInfo.PERCENT_BREAKDOWN10);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN11",objInstallmentPlanInfo.PERCENT_BREAKDOWN11);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWN12",objInstallmentPlanInfo.PERCENT_BREAKDOWN12);

				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP1",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP1);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP2",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP2);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP3",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP3);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP4",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP4);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP5",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP5);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP6",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP6);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP7",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP7);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP8",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP8);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP9",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP9);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP10",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP10);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP11",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP11);
				objDataWrapper.AddParameter("@PERCENT_BREAKDOWNRP12",objInstallmentPlanInfo.PERCENT_BREAKDOWNRP12);




				objDataWrapper.AddParameter("@MODIFIED_BY",objInstallmentPlanInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objInstallmentPlanInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@IDEN_PLAN_ID",objInstallmentPlanInfo.IDEN_PLAN_ID);
								
				if(objInstallmentPlanInfo.LATE_FEES==0.0)
					objDataWrapper.AddParameter("@LATE_FEES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@LATE_FEES",objInstallmentPlanInfo.LATE_FEES);
			
				objDataWrapper.AddParameter("@GRACE_PERIOD",objInstallmentPlanInfo.GRACE_PERIOD);
				objDataWrapper.AddParameter("@INSTALLMENT_FEES",objInstallmentPlanInfo.INSTALLMENT_FEES);
				objDataWrapper.AddParameter("@NON_SUFFICIENT_FUND_FEES",objInstallmentPlanInfo.NON_SUFFICIENT_FUND_FEES);
				objDataWrapper.AddParameter("@REINSTATEMENT_FEES",objInstallmentPlanInfo.REINSTATEMENT_FEES);
				objDataWrapper.AddParameter("@MIN_INSTALLMENT_AMOUNT",objInstallmentPlanInfo.MIN_INSTALLMENT_AMOUNT);
				objDataWrapper.AddParameter("@AMTUNDER_PAYMENT",objInstallmentPlanInfo.AMTUNDER_PAYMENT);
				objDataWrapper.AddParameter("@MINDAYS_PREMIUM",objInstallmentPlanInfo.MINDAYS_PREMIUM);
				objDataWrapper.AddParameter("@MINDAYS_CANCEL",objInstallmentPlanInfo.MINDAYS_CANCEL);
				objDataWrapper.AddParameter("@POST_PHONE",objInstallmentPlanInfo.POST_PHONE);
				objDataWrapper.AddParameter("@POST_CANCEL",objInstallmentPlanInfo.POST_CANCEL);
				objDataWrapper.AddParameter("@DEFAULT_PLAN",objInstallmentPlanInfo.DEFAULT_PLAN);
				//added by pravesh on 30 nov 2006 for plan payment mode and applicable premium
				objDataWrapper.AddParameter("@APPLABLE_POLTERM",objInstallmentPlanInfo.APPLABLE_POLTERM);
				objDataWrapper.AddParameter("@PLAN_PAYMENT_MODE",objInstallmentPlanInfo.PLAN_PAYMENT_MODE);
				objDataWrapper.AddParameter("@NO_INS_DOWNPAY",objInstallmentPlanInfo.NO_INS_DOWNPAY);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY",objInstallmentPlanInfo.MODE_OF_DOWNPAY);
				objDataWrapper.AddParameter("@NO_INS_DOWNPAY_RENEW",objInstallmentPlanInfo.NO_INS_DOWNPAY_RENEW);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY_RENEW",objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY1",objInstallmentPlanInfo.MODE_OF_DOWNPAY1);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY2",objInstallmentPlanInfo.MODE_OF_DOWNPAY2);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY_RENEW1",objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW1);
				objDataWrapper.AddParameter("@MODE_OF_DOWNPAY_RENEW2",objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW2);
				objDataWrapper.AddParameter("@PAST_DUE_RENEW",objInstallmentPlanInfo.PAST_DUE_RENEW);
				objDataWrapper.AddParameter("@DAYS_DUE_PREM_NOTICE_PRINTD",objInstallmentPlanInfo.DAYS_DUE_PREM_NOTICE_PRINTD);
                objDataWrapper.AddParameter("@INTREST_RATES", objInstallmentPlanInfo.INTREST_RATES);
                objDataWrapper.AddParameter("@DAYS_SUBSEQUENT_INSTALLMENTS", objInstallmentPlanInfo.DAYS_SUBSEQUENT_INSTALLMENTS);
                objDataWrapper.AddParameter("@SUBSEQUENT_INSTALLMENTS_OPTION", objInstallmentPlanInfo.SUBSEQUENT_INSTALLMENTS_OPTION);
                objDataWrapper.AddParameter("@BASE_DATE_DOWNPAYMENT", objInstallmentPlanInfo.BASE_DATE_DOWNPAYMENT);
                objDataWrapper.AddParameter("@DUE_DAYS_DOWNPYMT", objInstallmentPlanInfo.DUE_DAYS_DOWNPYMT);
                objDataWrapper.AddParameter("@BDATE_INSTALL_NXT_DOWNPYMT", objInstallmentPlanInfo.BDATE_INSTALL_NXT_DOWNPYMT);
                objDataWrapper.AddParameter("@DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT", objInstallmentPlanInfo.DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT);
                objDataWrapper.AddParameter("@DOWN_PAYMENT_PLAN", objInstallmentPlanInfo.DOWN_PAYMENT_PLAN);
                objDataWrapper.AddParameter("@DOWN_PAYMENT_PLAN_RENEWAL", objInstallmentPlanInfo.DOWN_PAYMENT_PLAN_RENEWAL);
                objDataWrapper.AddParameter("@APPLICABLE_LOB", objInstallmentPlanInfo.APPLICABLE_LOB);
                objDataWrapper.AddParameter("@RECVE_NOTIFICATION_DOC", objInstallmentPlanInfo.RECVE_NOTIFICATION_DOC);//Added by pradeep
		
				//
				if(TransactionLogRequired) 
				{
					objInstallmentPlanInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb\\accounting\\AddInstallmentPlan.aspx.resx");string strUpdate = objBuilder.GetUpdateSQL(objOldInstallmentPlanInfo,objInstallmentPlanInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	236;
					objTransactionInfo.RECORDED_BY		=	objInstallmentPlanInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"";
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

		#region Activate/Deactivate


		public static int ActivateDeactivate(int intPlanId,string isActive, Cms.Model.Maintenance.Accounting.ClsInstallmentPlanInfo objInstallment)
		{
			string strProc="Proc_InstallActivateDeactivate";
			DataWrapper objDatawrapper=new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				
				objDatawrapper.AddParameter("@IDEN_PLAN_ID",DefaultValues.GetIntNull(intPlanId));
				objDatawrapper.AddParameter("@IS_ACTIVE",DefaultValues.GetStringNull(isActive));
				int queryResult=0;
				Cms.BusinessLayer.BlCommon.ClsCommon obj=new ClsCommon();
					
				if(obj.TransactionLogRequired)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY=objInstallment.CREATED_BY;
					
					if(isActive=="N")
					{
                        objTransactionInfo.TRANS_TYPE_ID = 237;
						objTransactionInfo.TRANS_DESC   =  "";
					}
					else
					{
                        objTransactionInfo.TRANS_TYPE_ID = 238;
						objTransactionInfo.TRANS_DESC =    "";
					}
					   queryResult=objDatawrapper.ExecuteNonQuery(strProc,objTransactionInfo);
				}
				else
				{
                      queryResult=objDatawrapper.ExecuteNonQuery(strProc);
				}
                
				objDatawrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if(queryResult>0)
				{
					return 1;
				}
				else
				{
					return 0;
				}
			}
			catch(Exception ex)
			{
				objDatawrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw ex;
			}
           
		}




        #endregion

		#region GetLocationInfo
		/// <summary>
		/// Returns the XML of Installment plan record
		/// </summary>
		/// <param name="intInstallmentId">Id of record in Installment Plan table</param>
		/// <returns>XML of record of specified id</returns>
		public static string GetInstallmentPlanInfo(int intPlanId )
		{

			DataSet dsPlanInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@IDEN_PLAN_ID",intPlanId);
				dsPlanInfo = objDataWrapper.ExecuteDataSet(GET_PLAN_INFO);
				
				if (dsPlanInfo.Tables[0].Rows.Count != 0)
				{
					return dsPlanInfo.GetXml();
				}
				else
				{
					return "";
				}

				dsPlanInfo.Dispose();
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

		/// <summary>
		/// Return the default billing plan id, sets on maintenance section
		/// </summary>
		/// <returns></returns>
		public int GetDefaultPlanId(int PolTerm)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objWrapper.AddParameter("@APPLABLE_POLTERM",PolTerm);
				DataSet ds = objWrapper.ExecuteDataSet("Proc_Get_Default_ACT_INSTALL_PLAN_DETAIL");
				if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
					return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				else
					return 0;
			}
			catch (Exception objExp)
			{
				throw (new Exception("Following error occured while fetching default plan." + objExp.Message));
			}
		}
		public int GetDefaultPlanId()
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				DataSet ds = objWrapper.ExecuteDataSet("Proc_Get_Default_ACT_INSTALL_PLAN_DETAIL");
				if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
					return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				else
					return 0;
			}
			catch (Exception objExp)
			{
				throw (new Exception("Following error occured while fetching default plan." + objExp.Message));
			}
		}





       
    }
}
