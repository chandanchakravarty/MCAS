/******************************************************************************************
<Author					: -   Nidhi
<Start Date				: -	1/4/2006 5:44:52 PM
<End Date				: -	
<Description			: - 	Methods for Reinsurance Contract
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Methods for Reinsurance Contract
	/// </summary>
	public class ClsReinsuranceInformation : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_REINSURANCE_CONTRACT			=	"MNT_REINSURANCE_CONTRACT";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		 
		//private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACT";
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
		public static DataTable GetReinsuranceContracts()
		{
			string strSql = "Proc_GetReinsuranceContractsInDropDown";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}
		public static void GetReinsuranceContractsInDropDown(DropDownList combo)
		{
			combo.DataSource = GetReinsuranceContracts();
			combo.DataTextField = "CONTRACT_NAME";
			combo.DataValueField = "CONTRACT_NAME_ID";
			combo.DataBind();
		}
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsReinsuranceInformation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			//base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReinsuranceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsReinsuranceInfo objReinsuranceInfo)
		{
			string		strStoredProc	=	"Proc_MNT_REIN_INSERT_CONTRACT";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				 
				objDataWrapper.AddParameter("@CONTRACT_TYPE",objReinsuranceInfo.CONTRACT_TYPE);
				objDataWrapper.AddParameter("@CONTRACT_NUMBER",objReinsuranceInfo.CONTRACT_NUMBER);
				objDataWrapper.AddParameter("@CONTRACT_DESC",objReinsuranceInfo.CONTRACT_DESC);
				
				objDataWrapper.AddParameter("@LOSS_ADJUSTMENT_EXPENSE",objReinsuranceInfo.LOSS_ADJUSTMENT_EXPENSE);
				objDataWrapper.AddParameter("@RISK_EXPOSURE",objReinsuranceInfo.RISK_EXPOSURE);
				objDataWrapper.AddParameter("@CONTRACT_LOB",objReinsuranceInfo.CONTRACT_LOB);
				//objDataWrapper.AddParameter("@BROKERID",objReinsuranceInfo.BROKERID);
				objDataWrapper.AddParameter("@STATE_ID",objReinsuranceInfo.STATE_ID);
				objDataWrapper.AddParameter("@ORIGINAL_CONTACT_DATE",objReinsuranceInfo.ORIGINAL_CONTACT_DATE);
				objDataWrapper.AddParameter("@CONTACT_YEAR",objReinsuranceInfo.CONTACT_YEAR);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objReinsuranceInfo.EFFECTIVE_DATE);
				objDataWrapper.AddParameter("@EXPIRATION_DATE",objReinsuranceInfo.EXPIRATION_DATE);
				objDataWrapper.AddParameter("@COMMISSION",objReinsuranceInfo.COMMISSION);
				objDataWrapper.AddParameter("@CALCULATION_BASE",objReinsuranceInfo.CALCULATION_BASE);
				objDataWrapper.AddParameter("@PER_OCCURRENCE_LIMIT",objReinsuranceInfo.PER_OCCURRENCE_LIMIT);
				objDataWrapper.AddParameter("@ANNUAL_AGGREGATE",objReinsuranceInfo.ANNUAL_AGGREGATE);
				objDataWrapper.AddParameter("@DEPOSIT_PREMIUMS",objReinsuranceInfo.DEPOSIT_PREMIUMS);
				objDataWrapper.AddParameter("@DEPOSIT_PREMIUM_PAYABLE",objReinsuranceInfo.DEPOSIT_PREMIUM_PAYABLE);
				objDataWrapper.AddParameter("@MINIMUM_PREMIUM",objReinsuranceInfo.MINIMUM_PREMIUM);
			
				objDataWrapper.AddParameter("@SEQUENCE_NUMBER",objReinsuranceInfo.SEQUENCE_NUMBER);
				objDataWrapper.AddParameter("@TERMINATION_DATE",objReinsuranceInfo.TERMINATION_DATE);
				objDataWrapper.AddParameter("@TERMINATION_REASON",objReinsuranceInfo.TERMINATION_REASON);
				objDataWrapper.AddParameter("@COMMENTS",objReinsuranceInfo.COMMENTS);

				objDataWrapper.AddParameter("@FOLLOW_UP_FIELDS",objReinsuranceInfo.FOLLOW_UP_FIELDS);
				objDataWrapper.AddParameter("@COMMISSION_APPLICABLE",objReinsuranceInfo.COMMISSION_APPLICABLE);
				objDataWrapper.AddParameter("@REINSURANCE_PREMIUM_ACCOUNT",objReinsuranceInfo.REINSURANCE_PREMIUM_ACCOUNT);
				objDataWrapper.AddParameter("@REINSURANCE_PAYABLE_ACCOUNT",objReinsuranceInfo.REINSURANCE_PAYABLE_ACCOUNT);
				objDataWrapper.AddParameter("@REINSURANCE_COMMISSION_ACCOUNT",objReinsuranceInfo.REINSURANCE_COMMISSION_ACCOUNT);
				objDataWrapper.AddParameter("@REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT",objReinsuranceInfo.REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT);
				objDataWrapper.AddParameter("@FOLLOW_UP_FOR",objReinsuranceInfo.FOLLOW_UP_FOR);
                objDataWrapper.AddParameter("CASH_CALL_LIMIT",objReinsuranceInfo.CASH_CALL_LIMIT);
			
				objDataWrapper.AddParameter("@CREATED_BY",objReinsuranceInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objReinsuranceInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MAX_NO_INSTALLMENT", objReinsuranceInfo.MAX_NO_INSTALLMENT); //Added by Aditya for TFS BUG # 2512
                objDataWrapper.AddParameter("@RI_CONTRACTUAL_DEDUCTIBLE", 4);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CONTRACT_ID",objReinsuranceInfo.CONTRACT_ID,SqlDbType.Int,ParameterDirection.Output);
				int reterr=0;
				SqlParameter objSqlParameter_err  = (SqlParameter) objDataWrapper.AddParameter("@iERROR",reterr,SqlDbType.Int,ParameterDirection.Output);

				//For Capture New Data :Start
				string strGetlobProcedure = "Proc_GetLobList";
				string strNewlobDesc = "";
				string strNewlob = objReinsuranceInfo.CONTRACT_LOB;
				DataWrapper objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				//strNewlob = strNewlob.Substring(0,(strNewlob.Length-1));
				objNewWrapper.AddParameter("@LOB",strNewlob);
                objNewWrapper.AddParameter("@lang_id", BlCommon.ClsCommon.BL_LANG_ID);
				DataSet dsNewlob = new DataSet();
				dsNewlob = objNewWrapper.ExecuteDataSet(strGetlobProcedure);
				foreach (DataRow dr in dsNewlob.Tables[0].Rows)
				{
					strNewlobDesc = strNewlobDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewlobDesc = strNewlobDesc.Substring(0,(strNewlobDesc.Length-1));
				objReinsuranceInfo.LOB_TLOG = strNewlobDesc;
				
				//For Capture New Data :Start
				string strGetStateProcedure = "Proc_GetStateList";
				string strNewStateDesc = "";
				string strNewState = objReinsuranceInfo.STATE_ID;
				objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				//strNewState = strNewState.Substring(0,(strNewState.Length-1));
				objNewWrapper.AddParameter("@STATE",strNewState);
				DataSet dsNewState = new DataSet();
				dsNewState = objNewWrapper.ExecuteDataSet(strGetStateProcedure);
				foreach (DataRow dr in dsNewState.Tables[0].Rows)
				{
					strNewStateDesc = strNewStateDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewStateDesc = strNewStateDesc.Substring(0,(strNewStateDesc.Length-1));
				objReinsuranceInfo.STATE_TLOG = strNewStateDesc;

				//For Capture New Data :Start
				string strGetRiskProcedure = "Proc_GetCoverageCatagory";
				string strNewRiskDesc = "";
				string strNewRisk = objReinsuranceInfo.RISK_EXPOSURE;
				objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				//strNewRisk = strNewRisk.Substring(0,(strNewRisk.Length-1));
				objNewWrapper.AddParameter("@CATEGORY",strNewRisk);
				DataSet dsNewRisk = new DataSet();
				dsNewRisk = objNewWrapper.ExecuteDataSet(strGetRiskProcedure);
				foreach (DataRow dr in dsNewRisk.Tables[0].Rows)
				{
					strNewRiskDesc = strNewRiskDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewRiskDesc = strNewRiskDesc.Substring(0,(strNewRiskDesc.Length-1));
				objReinsuranceInfo.RISK_TLOG = strNewRiskDesc;

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsuranceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Reinsurance Contract Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				reterr = int.Parse(objSqlParameter_err.Value.ToString());
				int  intContractID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				if(reterr == 1 || intContractID== -1)
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if ( intContractID== -1)
				{

					return -1;
				}
				else if(reterr == 1)
				{
					return -2;
				}
				else
				{
						objReinsuranceInfo.CONTRACT_ID = intContractID;
						return intContractID;
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

	#region Reinsurance Premium Builder Section
		#region AddReinPremiumBuilder(Insert) function
		public int AddReinPremiumBuilder(ClsReinsurancePremiumBuildInfo objReinsurancePremiumBuildInfo)
		{
			string		strStoredProc	=	"Proc_MNT_REIN_INSERT_PREMIUM_BUILDER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				 
				objDataWrapper.AddParameter("@CONTRACT_ID",objReinsurancePremiumBuildInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@CONTRACT",objReinsurancePremiumBuildInfo.CONTRACT);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objReinsurancePremiumBuildInfo.EFFECTIVE_DATE);
				objDataWrapper.AddParameter("@EXPIRY_DATE",objReinsurancePremiumBuildInfo.EXPIRY_DATE);
				objDataWrapper.AddParameter("@LAYER",objReinsurancePremiumBuildInfo.LAYER);
				objDataWrapper.AddParameter("@COVERAGE_CATEGORY",objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY);
				objDataWrapper.AddParameter("@CALCULATION_BASE",objReinsurancePremiumBuildInfo.CALCULATION_BASE);
				objDataWrapper.AddParameter("@INSURANCE_VALUE",objReinsurancePremiumBuildInfo.INSURANCE_VALUE);
				objDataWrapper.AddParameter("@TOTAL_INSURANCE_FROM",objReinsurancePremiumBuildInfo.TOTAL_INSURANCE_FROM);
				objDataWrapper.AddParameter("@TOTAL_INSURANCE_TO",objReinsurancePremiumBuildInfo.TOTAL_INSURANCE_TO);
				objDataWrapper.AddParameter("@OTHER_INST",objReinsurancePremiumBuildInfo.OTHER_INST);
				objDataWrapper.AddParameter("@RATE_APPLIED",objReinsurancePremiumBuildInfo.RATE_APPLIED);
				objDataWrapper.AddParameter("@CONSTRUCTION",objReinsurancePremiumBuildInfo.CONSTRUCTION);
				objDataWrapper.AddParameter("@PROTECTION",objReinsurancePremiumBuildInfo.PROTECTION);
				objDataWrapper.AddParameter("@ALARM_CREDIT",objReinsurancePremiumBuildInfo.ALARM_CREDIT);
				objDataWrapper.AddParameter("@ALARM_PERCENTAGE",objReinsurancePremiumBuildInfo.ALARM_PERCENTAGE);
				objDataWrapper.AddParameter("@HOME_CREDIT",objReinsurancePremiumBuildInfo.HOME_CREDIT);
				objDataWrapper.AddParameter("@HOME_AGE",objReinsurancePremiumBuildInfo.HOME_AGE);
				objDataWrapper.AddParameter("@HOME_PERCENTAGE",objReinsurancePremiumBuildInfo.HOME_PERCENTAGE);
				objDataWrapper.AddParameter("@COMMENTS",objReinsurancePremiumBuildInfo.COMMENTS);
				objDataWrapper.AddParameter("@CREATED_BY",objReinsurancePremiumBuildInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objReinsurancePremiumBuildInfo.CREATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PREMIUM_BUILDER_ID",objReinsurancePremiumBuildInfo.PREMIUM_BUILDER_ID,SqlDbType.Int,ParameterDirection.Output);

				
				string strGetCoverageCategoryProcedure="Proc_GetCoverageCatagory";

				//For Capture New Data :Start
				string strNewCategoryDesc = "";
				//string strNewDate = "";
				string strNewCategory = objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY;
				DataWrapper objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strNewCategory = strNewCategory.Substring(0,(strNewCategory.Length-1));
				objNewWrapper.AddParameter("@CATEGORY",strNewCategory);
				DataSet dsNewCategory = new DataSet();
				dsNewCategory = objNewWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				foreach (DataRow dr in dsNewCategory.Tables[0].Rows)
				{
					strNewCategoryDesc = strNewCategoryDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewCategoryDesc = strNewCategoryDesc.Substring(0,(strNewCategoryDesc.Length-1));
				objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY_TLOG = strNewCategoryDesc;
				
				//For Capture New Data :Start
				string strNewProtectionDesc = "";
				//strNewDate = "";
				string strNewProtection = objReinsurancePremiumBuildInfo.PROTECTION;
				objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strNewProtection = strNewProtection.Substring(0,(strNewProtection.Length-1));
				objNewWrapper.AddParameter("@CATEGORY",strNewProtection);
				DataSet dsNewProtection = new DataSet();
				dsNewProtection = objNewWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				foreach (DataRow dr in dsNewProtection.Tables[0].Rows)
				{
					strNewProtectionDesc = strNewProtectionDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewProtectionDesc = strNewProtectionDesc.Substring(0,(strNewProtectionDesc.Length-1));
				objReinsurancePremiumBuildInfo.PROTECTION_TLOG = strNewProtectionDesc;

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsurancePremiumBuildInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Maintenance/Reinsurance/AddReinsurancePremiumBuilder.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsurancePremiumBuildInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReinsurancePremiumBuildInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Reinsurance Premium Builder Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intPremiumID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if ( intPremiumID== -1)
				{

					return -1;
				}
				else
				{
					objReinsurancePremiumBuildInfo.PREMIUM_BUILDER_ID = intPremiumID;
					return intPremiumID;
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
		#region UpdateReinPremiumBuilder function

		public int UpdateReinPremiumBuilder(ClsReinsurancePremiumBuildInfo objOldReinsurancePremiumBuildInfo,ClsReinsurancePremiumBuildInfo objReinsurancePremiumBuildInfo)
		{
			string		strStoredProc	=	"Proc_MNT_REIN_UPDATE_PREMIUM_BUILDER";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@PREMIUM_BUILDER_ID",objReinsurancePremiumBuildInfo.PREMIUM_BUILDER_ID);
				objDataWrapper.AddParameter("@CONTRACT_ID",objReinsurancePremiumBuildInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@CONTRACT",objReinsurancePremiumBuildInfo.CONTRACT);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objReinsurancePremiumBuildInfo.EFFECTIVE_DATE);
				objDataWrapper.AddParameter("@EXPIRY_DATE",objReinsurancePremiumBuildInfo.EXPIRY_DATE);
				objDataWrapper.AddParameter("@LAYER",objReinsurancePremiumBuildInfo.LAYER);
				objDataWrapper.AddParameter("@COVERAGE_CATEGORY",objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY);
				objDataWrapper.AddParameter("@CALCULATION_BASE",objReinsurancePremiumBuildInfo.CALCULATION_BASE);
				objDataWrapper.AddParameter("@INSURANCE_VALUE",objReinsurancePremiumBuildInfo.INSURANCE_VALUE);
				objDataWrapper.AddParameter("@TOTAL_INSURANCE_FROM",objReinsurancePremiumBuildInfo.TOTAL_INSURANCE_FROM);
				objDataWrapper.AddParameter("@TOTAL_INSURANCE_TO",objReinsurancePremiumBuildInfo.TOTAL_INSURANCE_TO);
				objDataWrapper.AddParameter("@OTHER_INST",objReinsurancePremiumBuildInfo.OTHER_INST);
				objDataWrapper.AddParameter("@RATE_APPLIED",objReinsurancePremiumBuildInfo.RATE_APPLIED);
				objDataWrapper.AddParameter("@CONSTRUCTION",objReinsurancePremiumBuildInfo.CONSTRUCTION);
				objDataWrapper.AddParameter("@PROTECTION",objReinsurancePremiumBuildInfo.PROTECTION);
				objDataWrapper.AddParameter("@ALARM_CREDIT",objReinsurancePremiumBuildInfo.ALARM_CREDIT);
				objDataWrapper.AddParameter("@ALARM_PERCENTAGE",objReinsurancePremiumBuildInfo.ALARM_PERCENTAGE);
				objDataWrapper.AddParameter("@HOME_CREDIT",objReinsurancePremiumBuildInfo.HOME_CREDIT);
				objDataWrapper.AddParameter("@HOME_AGE",objReinsurancePremiumBuildInfo.HOME_AGE);
				objDataWrapper.AddParameter("@HOME_PERCENTAGE",objReinsurancePremiumBuildInfo.HOME_PERCENTAGE);
				objDataWrapper.AddParameter("@COMMENTS",objReinsurancePremiumBuildInfo.COMMENTS);
				objDataWrapper.AddParameter("@MODIFIED_BY",objReinsurancePremiumBuildInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReinsurancePremiumBuildInfo.LAST_UPDATED_DATETIME);				
			
				string strGetCoverageCategoryProcedure="Proc_GetCoverageCatagory";

				//For Capture New Data :Start
				string strNewCategoryDesc = "";
				//string strNewDate = "";
				string strNewCategory = objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY;
				DataWrapper objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strNewCategory = strNewCategory.Substring(0,(strNewCategory.Length-1));
				objNewWrapper.AddParameter("@CATEGORY",strNewCategory);
				DataSet dsNewCategory = new DataSet();
				dsNewCategory = objNewWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				foreach (DataRow dr in dsNewCategory.Tables[0].Rows)
				{
					strNewCategoryDesc = strNewCategoryDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewCategoryDesc = strNewCategoryDesc.Substring(0,(strNewCategoryDesc.Length-1));
				objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY_TLOG = strNewCategoryDesc;
				
				//For Capture New Data :Start
				string strNewProtectionDesc = "";
				//strNewDate = "";
				string strNewProtection = objReinsurancePremiumBuildInfo.PROTECTION;
				objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strNewProtection = strNewProtection.Substring(0,(strNewProtection.Length-1));
				objNewWrapper.AddParameter("@CATEGORY",strNewProtection);
				DataSet dsNewProtection = new DataSet();
				dsNewProtection = objNewWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				foreach (DataRow dr in dsNewProtection.Tables[0].Rows)
				{
					strNewProtectionDesc = strNewProtectionDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewProtectionDesc = strNewProtectionDesc.Substring(0,(strNewProtectionDesc.Length-1));
				objReinsurancePremiumBuildInfo.PROTECTION_TLOG = strNewProtectionDesc;

				
				//For Capture Old Data :Start
				string strOldCategoryDesc = "";
				//string strOldLob = "";
				//string strOldDate = "";
				string strOldCategory = objOldReinsurancePremiumBuildInfo.COVERAGE_CATEGORY;
				DataWrapper objOldWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strOldCategory = strOldCategory.Substring(0,(strOldCategory.Length-1));
				objOldWrapper.AddParameter("@CATEGORY",strOldCategory);
				DataSet dsOldCategory = new DataSet();
				dsOldCategory = objOldWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				foreach (DataRow dr in dsOldCategory.Tables[0].Rows)
				{
					strOldCategoryDesc = strOldCategoryDesc + dr.ItemArray[0].ToString() + ",";
				}
				strOldCategoryDesc = strOldCategoryDesc.Substring(0,(strOldCategoryDesc.Length-1));
				objOldReinsurancePremiumBuildInfo.COVERAGE_CATEGORY_TLOG = strOldCategoryDesc;
				
				//For Capture Old Data :Start
				string strOldProtectionDesc = "";
				//strOldLob = "";
				//strOldDate = "";
				string strOldProtection = objOldReinsurancePremiumBuildInfo.PROTECTION;
				objOldWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strOldProtection = strOldProtection.Substring(0,(strOldProtection.Length-1));
				objOldWrapper.AddParameter("@CATEGORY",strOldProtection);
				DataSet dsOldProtection = new DataSet();
				dsOldProtection = objOldWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				foreach (DataRow dr in dsOldProtection.Tables[0].Rows)
				{
					strOldProtectionDesc = strOldProtectionDesc + dr.ItemArray[0].ToString() + ",";
				}
				strOldProtectionDesc = strOldProtectionDesc.Substring(0,(strOldProtectionDesc.Length-1));
				objOldReinsurancePremiumBuildInfo.PROTECTION_TLOG = strOldProtectionDesc;
				
				if(base.TransactionLogRequired) 
				{
					objReinsurancePremiumBuildInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Maintenance/Reinsurance/AddReinsurancePremiumBuilder.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsurancePremiumBuildInfo,objReinsurancePremiumBuildInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReinsurancePremiumBuildInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Reinsurance Premium Builder Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if (returnResult>0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
		#region DeleteReinPremiumBuilder function
		public int DeleteReinPremiumBuilder(ClsReinsurancePremiumBuildInfo objReinsurancePremiumBuildInfo,int PREMIUM_BUILDER_ID,int intUserId)
		{
			string strSql = "Proc_MNT_REIN_DELETE_PREMIUM_BUILDER";
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@PREMIUM_BUILDER_ID",PREMIUM_BUILDER_ID);

			string strGetCoverageCategoryProcedure="Proc_GetCoverageCatagory";

			//For Capture New Data :Start
			string strNewCategoryDesc = "";
			//string strNewDate = "";
			string strNewCategory = objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY;
			DataWrapper objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
			strNewCategory = strNewCategory.Substring(0,(strNewCategory.Length-1));
			objNewWrapper.AddParameter("@CATEGORY",strNewCategory);
			DataSet dsNewCategory = new DataSet();
			dsNewCategory = objNewWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
			foreach (DataRow dr in dsNewCategory.Tables[0].Rows)
			{
				strNewCategoryDesc = strNewCategoryDesc + dr.ItemArray[0].ToString() + ",";
			}
			strNewCategoryDesc = strNewCategoryDesc.Substring(0,(strNewCategoryDesc.Length-1));
			objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY_TLOG = strNewCategoryDesc;
				
			//For Capture New Data :Start
			string strNewProtectionDesc = "";
			//strNewDate = "";
			string strNewProtection = objReinsurancePremiumBuildInfo.PROTECTION;
			objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
			strNewProtection = strNewProtection.Substring(0,(strNewProtection.Length-1));
			objNewWrapper.AddParameter("@CATEGORY",strNewProtection);
			DataSet dsNewProtection = new DataSet();
			dsNewProtection = objNewWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
			foreach (DataRow dr in dsNewProtection.Tables[0].Rows)
			{
				strNewProtectionDesc = strNewProtectionDesc + dr.ItemArray[0].ToString() + ",";
			}
			strNewProtectionDesc = strNewProtectionDesc.Substring(0,(strNewProtectionDesc.Length-1));
			objReinsurancePremiumBuildInfo.PROTECTION_TLOG = strNewProtectionDesc;

			if(base.TransactionLogRequired) 
			{
				objReinsurancePremiumBuildInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Maintenance/Reinsurance/AddReinsurancePremiumBuilder.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objReinsurancePremiumBuildInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	objReinsurancePremiumBuildInfo.MODIFIED_BY;
				objTransactionInfo.TRANS_DESC		=	"Reinsurance Premium Builder Has Been Deleted";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				returnResult = objDataWrapper.ExecuteNonQuery(strSql,objTransactionInfo);

			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
			}
			return returnResult;
		}
		#endregion
		#region ActivateDeactivateReinPremiumBuilder function
		public void ActivateDeactivateReinPremiumBuilder(ClsReinsurancePremiumBuildInfo objReinsurancePremiumBuildInfo,string isActive)
		{
			DataWrapper objDataWrapper	=	new DataWrapper( ConnStr, CommandType.StoredProcedure);
			string strActivateDeactivateProcedure="Proc_MNT_REIN_DEACTIVATE_ACTIVATE_PREMIUM_BUILDER";
			try
			{
				objDataWrapper.AddParameter("@PREMIUM_BUILDER_ID",objReinsurancePremiumBuildInfo.PREMIUM_BUILDER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",isActive);

				string strGetCoverageCategoryProcedure="Proc_GetCoverageCatagory";

				//For Capture New Data :Start
				string strNewCategoryDesc = "";
				//string strNewDate = "";
				string strNewCategory = objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY;
				DataWrapper objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strNewCategory = strNewCategory.Substring(0,(strNewCategory.Length-1));
				objNewWrapper.AddParameter("@CATEGORY",strNewCategory);
				DataSet dsNewCategory = new DataSet();
				dsNewCategory = objNewWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				foreach (DataRow dr in dsNewCategory.Tables[0].Rows)
				{
					strNewCategoryDesc = strNewCategoryDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewCategoryDesc = strNewCategoryDesc.Substring(0,(strNewCategoryDesc.Length-1));
				objReinsurancePremiumBuildInfo.COVERAGE_CATEGORY_TLOG = strNewCategoryDesc;
				
				//For Capture New Data :Start
				string strNewProtectionDesc = "";
				//strNewDate = "";
				string strNewProtection = objReinsurancePremiumBuildInfo.PROTECTION;
				objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strNewProtection = strNewProtection.Substring(0,(strNewProtection.Length-1));
				objNewWrapper.AddParameter("@CATEGORY",strNewProtection);
				DataSet dsNewProtection = new DataSet();
				dsNewProtection = objNewWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				foreach (DataRow dr in dsNewProtection.Tables[0].Rows)
				{
					strNewProtectionDesc = strNewProtectionDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewProtectionDesc = strNewProtectionDesc.Substring(0,(strNewProtectionDesc.Length-1));
				objReinsurancePremiumBuildInfo.PROTECTION_TLOG = strNewProtectionDesc;


				if(TransactionLogRequired) 
				{
					objReinsurancePremiumBuildInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Maintenance/Reinsurance/AddReinsurancePremiumBuilder.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsurancePremiumBuildInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID		=	3;
					if(isActive.Equals("Y"))
						objTransactionInfo.TRANS_DESC		=	"Reinsurance Premium Builder has been activated.";
					else
						objTransactionInfo.TRANS_DESC		=	"Reinsurance Premium Builder has been deactivated.";
					objTransactionInfo.RECORDED_BY			=	objReinsurancePremiumBuildInfo.CREATED_BY;
					objTransactionInfo.CHANGE_XML			=	strTranXML;
					//					objTransactionInfo.CUSTOM_INFO			=	strLoss;
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
		#endregion
		#region GetReinPremiumBuilder function
		/// <summary>
		/// Returns the data in the form of XML of specified intCoverageId
		/// </summary>
		/// <param name="CoverageId">Coverage Id whose data will be returned</param>
		/// <returns>XML of data</returns>
		public static string GetReinPremiumBuilder(int intPremiumBuilderId )
		{
			String strStoredProc = "Proc_MNT_REIN_GET_PREMIUM_BUILDER";
			DataSet dsReinPremiumBuilderInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@PREMIUM_BUILDER_ID",intPremiumBuilderId);
				
				dsReinPremiumBuilderInfo = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				if (dsReinPremiumBuilderInfo.Tables[0].Rows.Count != 0)
				{
					return dsReinPremiumBuilderInfo.GetXml();
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

	#endregion


		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldReinsuranceInfo">Model object having old information</param>
		/// <param name="objReinsuranceInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsReinsuranceInfo objOldReinsuranceInfo,ClsReinsuranceInfo objReinsuranceInfo)
		{
			string		strStoredProc	=	"Proc_MNT_REIN_UPDATE_CONTRACT";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CONTRACT_ID",objReinsuranceInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@CONTRACT_TYPE",objReinsuranceInfo.CONTRACT_TYPE);
				objDataWrapper.AddParameter("@CONTRACT_NUMBER",objReinsuranceInfo.CONTRACT_NUMBER);
				objDataWrapper.AddParameter("@CONTRACT_DESC",objReinsuranceInfo.CONTRACT_DESC);
				
				objDataWrapper.AddParameter("@LOSS_ADJUSTMENT_EXPENSE",objReinsuranceInfo.LOSS_ADJUSTMENT_EXPENSE);
				objDataWrapper.AddParameter("@RISK_EXPOSURE",objReinsuranceInfo.RISK_EXPOSURE);
				objDataWrapper.AddParameter("@CONTRACT_LOB",objReinsuranceInfo.CONTRACT_LOB);
				//objDataWrapper.AddParameter("@BROKERID",objReinsuranceInfo.BROKERID);
				objDataWrapper.AddParameter("@STATE_ID",objReinsuranceInfo.STATE_ID);
				objDataWrapper.AddParameter("@ORIGINAL_CONTACT_DATE",objReinsuranceInfo.ORIGINAL_CONTACT_DATE);
				objDataWrapper.AddParameter("@CONTACT_YEAR",objReinsuranceInfo.CONTACT_YEAR);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objReinsuranceInfo.EFFECTIVE_DATE);
				objDataWrapper.AddParameter("@EXPIRATION_DATE",objReinsuranceInfo.EXPIRATION_DATE);
				objDataWrapper.AddParameter("@COMMISSION",objReinsuranceInfo.COMMISSION);
				objDataWrapper.AddParameter("@CALCULATION_BASE",objReinsuranceInfo.CALCULATION_BASE);
				objDataWrapper.AddParameter("@PER_OCCURRENCE_LIMIT",objReinsuranceInfo.PER_OCCURRENCE_LIMIT);
				objDataWrapper.AddParameter("@ANNUAL_AGGREGATE",objReinsuranceInfo.ANNUAL_AGGREGATE);
				objDataWrapper.AddParameter("@DEPOSIT_PREMIUMS",objReinsuranceInfo.DEPOSIT_PREMIUMS);
				objDataWrapper.AddParameter("@DEPOSIT_PREMIUM_PAYABLE",objReinsuranceInfo.DEPOSIT_PREMIUM_PAYABLE);
				objDataWrapper.AddParameter("@MINIMUM_PREMIUM",objReinsuranceInfo.MINIMUM_PREMIUM);
			
				objDataWrapper.AddParameter("@SEQUENCE_NUMBER",objReinsuranceInfo.SEQUENCE_NUMBER);
				objDataWrapper.AddParameter("@TERMINATION_DATE",objReinsuranceInfo.TERMINATION_DATE);
				objDataWrapper.AddParameter("@TERMINATION_REASON",objReinsuranceInfo.TERMINATION_REASON);
				objDataWrapper.AddParameter("@COMMENTS",objReinsuranceInfo.COMMENTS);
				objDataWrapper.AddParameter("@FOLLOW_UP_FIELDS",objReinsuranceInfo.FOLLOW_UP_FIELDS);
				objDataWrapper.AddParameter("@COMMISSION_APPLICABLE",objReinsuranceInfo.COMMISSION_APPLICABLE);
				objDataWrapper.AddParameter("@REINSURANCE_PREMIUM_ACCOUNT",objReinsuranceInfo.REINSURANCE_PREMIUM_ACCOUNT);
				objDataWrapper.AddParameter("@REINSURANCE_PAYABLE_ACCOUNT",objReinsuranceInfo.REINSURANCE_PAYABLE_ACCOUNT);
				objDataWrapper.AddParameter("@REINSURANCE_COMMISSION_ACCOUNT",objReinsuranceInfo.REINSURANCE_COMMISSION_ACCOUNT);
				objDataWrapper.AddParameter("@REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT",objReinsuranceInfo.REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT);
				objDataWrapper.AddParameter("@FOLLOW_UP_FOR",objReinsuranceInfo.FOLLOW_UP_FOR);
                objDataWrapper.AddParameter("CASH_CALL_LIMIT", objReinsuranceInfo.CASH_CALL_LIMIT);
			
				objDataWrapper.AddParameter("@MODIFIED_BY",objReinsuranceInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReinsuranceInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@MAX_NO_INSTALLMENT", objReinsuranceInfo.MAX_NO_INSTALLMENT); //Added by Aditya for TFS BUG # 2512
                objDataWrapper.AddParameter("@RI_CONTRACTUAL_DEDUCTIBLE", 4);
				//For Capture New Data :Start
				string strGetlobProcedure = "Proc_GetLobList";
				string strNewlobDesc = "";
				string strNewlob = objReinsuranceInfo.CONTRACT_LOB;
				DataWrapper objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
//				strNewlob = strNewlob.Substring(0,(strNewlob.Length-1));
				objNewWrapper.AddParameter("@LOB",strNewlob);
                objNewWrapper.AddParameter("@lang_id", BlCommon.ClsCommon.BL_LANG_ID);
				DataSet dsNewlob = new DataSet();
				dsNewlob = objNewWrapper.ExecuteDataSet(strGetlobProcedure);
				foreach (DataRow dr in dsNewlob.Tables[0].Rows)
				{
					strNewlobDesc = strNewlobDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewlobDesc = strNewlobDesc.Substring(0,(strNewlobDesc.Length-1));
				objReinsuranceInfo.LOB_TLOG = strNewlobDesc;
				
				string strGetStateProcedure = "Proc_GetStateList";
				string strNewStateDesc = "";
				string strNewState = objReinsuranceInfo.STATE_ID;
				objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
//				strNewState = strNewState.Substring(0,(strNewState.Length-1));
				objNewWrapper.AddParameter("@STATE",strNewState);
				DataSet dsNewState = new DataSet();
				dsNewState = objNewWrapper.ExecuteDataSet(strGetStateProcedure);
				foreach (DataRow dr in dsNewState.Tables[0].Rows)
				{
					strNewStateDesc = strNewStateDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewStateDesc = strNewStateDesc.Substring(0,(strNewStateDesc.Length-1));
				objReinsuranceInfo.STATE_TLOG = strNewStateDesc;

				string strGetRiskProcedure = "Proc_GetCoverageCatagory";
				string strNewRiskDesc = "";
				string strNewRisk = objReinsuranceInfo.RISK_EXPOSURE;
				objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
//				strNewRisk = strNewRisk.Substring(0,(strNewRisk.Length-1));
				objNewWrapper.AddParameter("@CATEGORY",strNewRisk);
				DataSet dsNewRisk = new DataSet();
				dsNewRisk = objNewWrapper.ExecuteDataSet(strGetRiskProcedure);
				foreach (DataRow dr in dsNewRisk.Tables[0].Rows)
				{
					strNewRiskDesc = strNewRiskDesc + dr.ItemArray[0].ToString() + ",";
				}
				strNewRiskDesc = strNewRiskDesc.Substring(0,(strNewRiskDesc.Length-1));
				objReinsuranceInfo.RISK_TLOG = strNewRiskDesc;
				//For Capture New Data :End

				//For Capture Old Data :Start
				objOldReinsuranceInfo.LOB_TLOG = objOldReinsuranceInfo.CONTRACT_LOB;
				objOldReinsuranceInfo.STATE_TLOG = objOldReinsuranceInfo.STATE_ID;
				objOldReinsuranceInfo.RISK_TLOG = objOldReinsuranceInfo.RISK_EXPOSURE;
				//For Capture Old Data :End

				
				if(base.TransactionLogRequired) 
				{
					objReinsuranceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceInfo.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceInfo,objReinsuranceInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Reinsurance Contract Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if (returnResult>0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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

		# region GET VARIOUS ACCOUNT
		public DataTable GetDifferentAccount(string GL_ID,string accountTypeID)
		{
			string strStoredProc = "Proc_GetGLAccountsInDropDownReInsurance";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@GL_ID",GL_ID);
			objDataWrapper.AddParameter("@accountTypeID",accountTypeID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			return objDataTable;
		}
		# endregion GET VARIOUS ACCOUNT


		# region GET Reinsurance Contract Type
		public DataTable GetReinsuranceContractType()
		{
			string strStoredProc = "Proc_GetReinContractTypeInDropDown";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LANG_ID", BlCommon.ClsCommon.BL_LANG_ID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			return objDataTable;
        }
       
      
		# endregion GET Reinsurance Contract Type
        # region GET Reinsurance Contract Type
        // fetch contract type for application reinsurers with effective and expiration date.

        public DataTable GetReinsurance_ContractType(int customer_id, int policy_id, int Poversion_id)
        {
            string strStoredProc = "Proc_GetReinsurer_Contract";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
            objDataWrapper.AddParameter("@POLICY_ID", policy_id);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", Poversion_id);
            DataTable objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
            return objDataTable;
        }
        # endregion


        # region Fill Contract year Dropdown
        public DataTable GetReinsuranceContractYear()
		{
			string strStoredProc = "Proc_GetReinContractYearInDropDown";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			return objDataTable;
		}
		#endregion 
		
		# region GetCarrierUsers

		public DataTable GetCarrierUsers()
		{
			string strStoredProc = "Proc_GetCarrierUser";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			return objDataTable;
		}
		#endregion

		#region G E T  D A T A   F O R   E D I T   M O D E

		public DataSet GetDataForPageControls(string CONTRACT_ID)
		{
			string strSql = "Proc_MNT_REIN_GETXML_CONTRACT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACT_ID",CONTRACT_ID);
            objDataWrapper.AddParameter("@lang_id", BlCommon.ClsCommon.BL_LANG_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion
				
		#region DEACTIVATE OR ACTIVATE CONTRACT TYPE

		public int GetDeactivateActivate(ClsReinsuranceInfo objReinsuranceInfo,string CONTRACT_ID,string Status_Check)
		{
			int returnResult =0;
			string strSql = "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CONTRACT_ID",CONTRACT_ID);
			objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

			//For Capture New Data :Start
//			string strGetlobProcedure = "Proc_GetLobList";
//			string strNewlobDesc = "";
//			string strNewlob = objReinsuranceInfo.CONTRACT_LOB;
//			DataWrapper objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
//			objNewWrapper.AddParameter("@LOB",strNewlob);
//			DataSet dsNewlob = new DataSet();
//			dsNewlob = objNewWrapper.ExecuteDataSet(strGetlobProcedure);
//			foreach (DataRow dr in dsNewlob.Tables[0].Rows)
//			{
//				strNewlobDesc = strNewlobDesc + dr.ItemArray[0].ToString() + ",";
//			}
//			strNewlobDesc = strNewlobDesc.Substring(0,(strNewlobDesc.Length-1));
//			objReinsuranceInfo.LOB_TLOG = strNewlobDesc;
//				
//			string strGetStateProcedure = "Proc_GetStateList";
//			string strNewStateDesc = "";
//			string strNewState = objReinsuranceInfo.STATE_ID;
//			objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
//			objNewWrapper.AddParameter("@STATE",strNewState);
//			DataSet dsNewState = new DataSet();
//			dsNewState = objNewWrapper.ExecuteDataSet(strGetStateProcedure);
//			foreach (DataRow dr in dsNewState.Tables[0].Rows)
//			{
//				strNewStateDesc = strNewStateDesc + dr.ItemArray[0].ToString() + ",";
//			}
//			strNewStateDesc = strNewStateDesc.Substring(0,(strNewStateDesc.Length-1));
//			objReinsuranceInfo.STATE_TLOG = strNewStateDesc;
//
//			string strGetRiskProcedure = "Proc_GetCoverageCatagory";
//			string strNewRiskDesc = "";
//			string strNewRisk = objReinsuranceInfo.RISK_EXPOSURE;
//			objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
//			objNewWrapper.AddParameter("@CATEGORY",strNewRisk);
//			DataSet dsNewRisk = new DataSet();
//			dsNewRisk = objNewWrapper.ExecuteDataSet(strGetRiskProcedure);
//			foreach (DataRow dr in dsNewRisk.Tables[0].Rows)
//			{
//				strNewRiskDesc = strNewRiskDesc + dr.ItemArray[0].ToString() + ",";
//			}
//			strNewRiskDesc = strNewRiskDesc.Substring(0,(strNewRiskDesc.Length-1));
//			objReinsuranceInfo.RISK_TLOG = strNewRiskDesc;
			//For Capture New Data :End

			if(base.TransactionLogRequired) 
			{
				objReinsuranceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceInfo);

				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	objReinsuranceInfo.MODIFIED_BY;
				if(Status_Check == "Y")
					objTransactionInfo.TRANS_DESC		=	"Reinsurance Contract Has Been Activated";
				else
					objTransactionInfo.TRANS_DESC		=	"Reinsurance Contract has been deactivated.";

				objTransactionInfo.CHANGE_XML		=	strTranXML;
				returnResult = objDataWrapper.ExecuteNonQuery(strSql,objTransactionInfo);

			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
			}
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return returnResult;
		}
		
		#endregion

		#region DELETE RECORDS FROM TABLE

		public int Delete(ClsReinsuranceInfo objReinsuranceInfo,string CONTRACT_ID)
		{
			string strSql = "Proc_MNT_REIN_DELETE_CONTRACT";
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@HAS_ERROR", 0, SqlDbType.Int, ParameterDirection.Output);

			objDataWrapper.AddParameter("@CONTRACT_ID",CONTRACT_ID);
//			SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

			//For Capture New Data :Start
//			string strGetlobProcedure = "Proc_GetLobList";
//			string strNewlobDesc = "";
//			string strNewlob = objReinsuranceInfo.CONTRACT_LOB;
//			DataWrapper objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
//			objNewWrapper.AddParameter("@LOB",strNewlob);
//			DataSet dsNewlob = new DataSet();
//			dsNewlob = objNewWrapper.ExecuteDataSet(strGetlobProcedure);
//			foreach (DataRow dr in dsNewlob.Tables[0].Rows)
//			{
//				strNewlobDesc = strNewlobDesc + dr.ItemArray[0].ToString() + ",";
//			}
//			strNewlobDesc = strNewlobDesc.Substring(0,(strNewlobDesc.Length-1));
//			objReinsuranceInfo.LOB_TLOG = strNewlobDesc;
//				
//			string strGetStateProcedure = "Proc_GetStateList";
//			string strNewStateDesc = "";
//			string strNewState = objReinsuranceInfo.STATE_ID;
//			objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
//			objNewWrapper.AddParameter("@STATE",strNewState);
//			DataSet dsNewState = new DataSet();
//			dsNewState = objNewWrapper.ExecuteDataSet(strGetStateProcedure);
//			foreach (DataRow dr in dsNewState.Tables[0].Rows)
//			{
//				strNewStateDesc = strNewStateDesc + dr.ItemArray[0].ToString() + ",";
//			}
//			strNewStateDesc = strNewStateDesc.Substring(0,(strNewStateDesc.Length-1));
//			objReinsuranceInfo.STATE_TLOG = strNewStateDesc;
//
//			string strGetRiskProcedure = "Proc_GetCoverageCatagory";
//			string strNewRiskDesc = "";
//			string strNewRisk = objReinsuranceInfo.RISK_EXPOSURE;
//			objNewWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
//			objNewWrapper.AddParameter("@CATEGORY",strNewRisk);
//			DataSet dsNewRisk = new DataSet();
//			dsNewRisk = objNewWrapper.ExecuteDataSet(strGetRiskProcedure);
//			foreach (DataRow dr in dsNewRisk.Tables[0].Rows)
//			{
//				strNewRiskDesc = strNewRiskDesc + dr.ItemArray[0].ToString() + ",";
//			}
//			strNewRiskDesc = strNewRiskDesc.Substring(0,(strNewRiskDesc.Length-1));
//			objReinsuranceInfo.RISK_TLOG = strNewRiskDesc;
			//For Capture New Data :End

			if(base.TransactionLogRequired) 
			{
				objReinsuranceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddReinsuranceInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceInfo);

				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	objReinsuranceInfo.MODIFIED_BY;
				objTransactionInfo.TRANS_DESC		=	"Reinsurance Contract Has Been Deleted";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				returnResult = objDataWrapper.ExecuteNonQuery(strSql,objTransactionInfo);

			}
			else
			{
				//objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

				//DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
			}
            int HasError = 0;
            if (objSqlParameter.Value.ToString() != "")
                HasError = int.Parse(objSqlParameter.Value.ToString());
            if (HasError == -4)
                returnResult = HasError;
			return returnResult;
		}

		#endregion

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(string CONTRACT_ID)
		{
			string strSql = "Proc_GetXMLMNT_REINSURANCE_CONTRACT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACT_ID",CONTRACT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		public static DataSet GetDataSetForPageControls(string CONTRACT_ID)
		{
			string strSql = "Proc_GetXMLMNT_REINSURANCE_CONTRACT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACT_ID",CONTRACT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion

		#region GetReinsurenceContractInfo
		public static DataTable GetReinsurenceContractInfo(string CONTRACT_ID)
		{
				
			string strSql = "Proc_GetREINSURANCE_CONTRACT_INFO";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACT_ID",CONTRACT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}
        /// <summary>
        /// Get Reinsurence Break Down
        /// </summary>
        /// <returns></returns>
        //Added by Pradeep Kushwaha on 21-Jan-2011
        public DataSet GetReinsurenceBreakDown()
        {
            string strStoredProc = "Proc_GetReinPolicyId";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strStoredProc);
            return objDataSet;
        }
		#endregion

		#region Copy Contract
		public int CopyContract(DataTable dtReinsurenceContract,int strCreatedBy)
		{			
			string	strStoredProc =	"Proc_COPY_REINSURANCE_CONTRACT";
			
			int returnResult = 0;

			try
			{				
				for(int i=0;i < dtReinsurenceContract.Rows.Count;i++)
				{
					DataRow dr=dtReinsurenceContract.Rows[i];					

					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

					objDataWrapper.AddParameter("@CONTRACT_ID",int.Parse(dr["CONTRACT_ID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY",strCreatedBy,SqlDbType.Int);
					returnResult =objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				}
				return returnResult;
			
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}	

		#endregion
		
	}
}
