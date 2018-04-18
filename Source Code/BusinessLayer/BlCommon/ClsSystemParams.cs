using System;
using System.Data;
using System.Xml;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance;


/******************************************************************************************
	<Author					: Gaurav Tyagi- >
	<Start Date				: March 16, 2005-	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 8/7/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Adding sqlParamater for SYS_INSURANCE_SCORE_VALIDITY field
								  cretaing one function that returns INSURANCE score
	
	<Modified Date			: - > 30/06/2010
	<Modified By			: - > Pradeep Kushwaha
	<Purpose				: - > Add new Column NOTIFY_RECVE_INSURED
	
*******************************************************************************************/
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsSystemParams.
	/// </summary>
	public class ClsSystemParams:ClsCommon
	{
		private bool boolTransactionLog;
		public ClsSystemParams()
		{
			boolTransactionLog	=	TransactionLogRequired;
		}

		#region Update System Parameters
/// <summary>
/// This function is used to update System Parameters in MNT_SYSTEM_PARAMS Table.
/// Function is called from SystemParams page.
/// </summary>
/// <param name="intBadLoginAttempt"></param>
/// <param name="intRenewalForAlert"></param>
/// <param name="intPendingQuoteForAlert"></param>
/// <param name="intQuotedQuoteForAlert"></param>
/// <param name="intNumberDaysExpire"></param>
/// <param name="intNumberDaysPendingNTU"></param>
/// <param name="intNumberDaysExpireQuote"></param>
/// <param name="intDefaultPolicyTerm"></param>
/// <returns></returns>
		/*public int UpdateSystemParams
			(int intBadLoginAttempt, int intRenewalForAlert,
			int intPendingQuoteForAlert, int intQuotedQuoteForAlert, int intNumberDaysExpire,
			int intNumberDaysPendingNTU, int intNumberDaysExpireQuote, int intDefaultPolicyTerm,
			string strGraphLogAllow, double dblInstallmentFees, double dblNonSufficientFundFees,
			double dblReinstatementFees, double dblEmployeeDiscount, string strPrintFollowing,int intClaimsNumber, bool TransactionLogReq,string insScore,
			double dblMinInstallPlan,double dblAmtUnderPayment,int intMinDays_Premium,int intMinDays_Cancel,int intPostPhone,int intPostCancel)*/
		public int UpdateSystemParams(ClsSystemParamsInfo objOldSystemParamsInfo,ClsSystemParamsInfo objSystemParamsInfo,string ModifiedBy)
		{
			string strProcName		=	"Proc_UpdateSystemParams";
			DateTime dtRecordDate		=	DateTime.Now;

			int intreturnResult=0;
			string strTranXML;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			Cms.DataLayer.DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@Bad_Login_Attempt",objSystemParamsInfo.SYS_BAD_LOGON_ATTMPT);
				objDataWrapper.AddParameter("@Renewal_For_Alert",objSystemParamsInfo.SYS_RENEWAL_FOR_ALERT);
				objDataWrapper.AddParameter("@Pending_Quote_For_Alert",objSystemParamsInfo.SYS_PENDING_QUOTE_FOR_ALERT);
				objDataWrapper.AddParameter("@Quoted_Quote_For_Alert",objSystemParamsInfo.SYS_QUOTED_QUOTE_FOR_ALERT);
				objDataWrapper.AddParameter("@Number_Days_Expire",objSystemParamsInfo.SYS_NUM_DAYS_EXPIRE);
				objDataWrapper.AddParameter("@Number_Days_Pending_NTU",objSystemParamsInfo.SYS_NUM_DAYS_PEN_TO_NTU);
				objDataWrapper.AddParameter("@Number_Days_Expire_Quote",objSystemParamsInfo.SYS_NUM_DAYS_EXPIRE_QUOTE);
				objDataWrapper.AddParameter("@Default_Policy_Term",objSystemParamsInfo.SYS_DEFAULT_POL_TERM);
				objDataWrapper.AddParameter("@Graph_Logo_Allow",objSystemParamsInfo.SYS_GRAPH_LOGO_ALLOW);
				objDataWrapper.AddParameter("@Installment_Fees",objSystemParamsInfo.SYS_INSTALLMENT_FEES);
				objDataWrapper.AddParameter("@Non_Sufficient_Fund_Fees",objSystemParamsInfo.SYS_NON_SUFFICIENT_FUND_FEES);
				objDataWrapper.AddParameter("@Reinstatement_Fees",objSystemParamsInfo.SYS_REINSTATEMENT_FEES);
				objDataWrapper.AddParameter("@Employee_Discount",objSystemParamsInfo.SYS_EMPLOYEE_DISCOUNT);
				objDataWrapper.AddParameter("@Print_Following",objSystemParamsInfo.SYS_PRINT_FOLLOWING);
				objDataWrapper.AddParameter("@Claim_Number",objSystemParamsInfo.SYS_CLAIM_NO);
				objDataWrapper.AddParameter("@SYS_INSURANCE_SCORE_VALIDITY",objSystemParamsInfo.SYS_INSURANCE_SCORE_VALIDITY);
				objDataWrapper.AddParameter("@SYS_INDICATE_POL_AS",objSystemParamsInfo.SYS_INDICATE_POL_AS);
				//Added By Shafi
				objDataWrapper.AddParameter("@SYS_Min_Install_Plan",objSystemParamsInfo.SYS_Min_Install_Plan);
				objDataWrapper.AddParameter("@SYS_AmtUnder_Payment",objSystemParamsInfo.SYS_AmtUnder_Payment);
				objDataWrapper.AddParameter("@SYS_MinDays_Premium",DefaultValues.GetIntNull(objSystemParamsInfo.SYS_MinDays_Premium));
				objDataWrapper.AddParameter("@SYS_MinDays_Cancel",DefaultValues.GetIntNull(objSystemParamsInfo.SYS_MinDays_Cancel));
				objDataWrapper.AddParameter("@SYS_Post_Phone",DefaultValues.GetIntNull(objSystemParamsInfo.SYS_Post_Phone));
				objDataWrapper.AddParameter("@SYS_Post_Cancel",DefaultValues.GetIntNull(objSystemParamsInfo.SYS_Post_Cancel));
			
				//Added by Mohit Agarwal 1-Aug-2007 for CLTIN
				objDataWrapper.AddParameter("@POSTAGE_FEE",objSystemParamsInfo.POSTAGE_FEE);
				objDataWrapper.AddParameter("@RESTR_DELIV_FEE",objSystemParamsInfo.RESTR_DELIV_FEE);
				objDataWrapper.AddParameter("@CERTIFIED_FEE",objSystemParamsInfo.CERTIFIED_FEE);
				objDataWrapper.AddParameter("@RET_RECEIPT_FEE",objSystemParamsInfo.RET_RECEIPT_FEE);
                objDataWrapper.AddParameter("NOTIFY_RECVE_INSURED", objSystemParamsInfo.NOTIFY_RECVE_INSURED);
                objDataWrapper.AddParameter("BASE_CURRENCY", objSystemParamsInfo.BASE_CURRENCY);
                objDataWrapper.AddParameter("@DAYS_FOR_BOLETO_EXPIRATION", objSystemParamsInfo.DAYS_FOR_BOLETO_EXPIRATION);
				if(TransactionLogRequired) 
				{

					objSystemParamsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/SystemParams.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldSystemParamsInfo,objSystemParamsInfo);
					if (strTranXML =="")
					{
						strTranXML =objBuilder.GetTransactionLogXML(objSystemParamsInfo);
					}
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	int.Parse(ModifiedBy);
					objTransactionInfo.TRANS_DESC		=	"General Setup is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					intreturnResult = objDataWrapper.ExecuteNonQuery(strProcName,objTransactionInfo);

				}
				else
				{
					intreturnResult	=	objDataWrapper.ExecuteNonQuery(strProcName);
				}
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return intreturnResult;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(objDataWrapper !=null)
				objDataWrapper.Dispose();
			}

		}

		#endregion

		#region Get Old XML
		public string GetOldXML()
		{
			string strStoredProc = "Proc_GetXmlMNT_SYSTEM_PARAMS";
			DataSet dsSystemParam = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				dsSystemParam = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsSystemParam.Tables[0].Rows.Count != 0)
				{
					return dsSystemParam.GetXml();
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

		#region Select System Parameters

		/// <summary>
		/// This function is used to reterive records from MNT_SYSTEM_PARAMS
		/// Function is called from SystemParams page.
		/// </summary>
		/// <returns>DataSet with result</returns>
		public DataSet GetSystemParams()
		{
			string strProcName = "Proc_GetSystemParams";
			return DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,strProcName);
		}

		public string GetSystemParamsIS()
		{
			string strProcName = "Proc_GetSystemParamsIS";
			return	DataWrapper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strProcName).ToString() ;			
		}
        
		#endregion
	}
}
