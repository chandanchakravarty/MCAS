/******************************************************************************************
<Author				: - Vijay Joshi
<Start Date			: -	5/19/2005 3:48:36 PM
<End Date			: -	
<Description		: - Business logic file for Personal article general information.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified

<Modified Date			: - Pradeep
<Modified By			: - Nov 14, 2005
<Purpose				: - Added code for Policy Underwriting questions
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.Homeowners;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Business logic class for Personal article general information.
	/// </summary>
	public class ClsPersArticleGenInformation : Cms.BusinessLayer.BlApplication.clsapplication
	{
		private const	string		APP_HOME_OWNER_PER_ART_GEN_INFO			=	"APP_HOME_OWNER_PER_ART_GEN_INFO";
		private const string GET_PERSONAL_INFO_PROC = "Proc_GetAPP_HOME_OWNER_PER_ART_GEN_INFO_XML";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _APP_VERSION_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "";
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
		public ClsPersArticleGenInformation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPersArticleGenInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPersArticleGenInfo objPersArticleGenInfo)
		{
			string		strStoredProc	=	"Proc_InsertAPP_HOME_OWNER_PER_ART_GEN_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPersArticleGenInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objPersArticleGenInfo.APP_ID);
				objDataWrapper.AddParameter("@PROPERTY_EXHIBITED",objPersArticleGenInfo.PROPERTY_EXHIBITED);
				objDataWrapper.AddParameter("@DESC_PROPERTY_EXHIBITED",objPersArticleGenInfo.DESC_PROPERTY_EXHIBITED);
				objDataWrapper.AddParameter("@DEDUCTIBLE_APPLY",objPersArticleGenInfo.DEDUCTIBLE_APPLY);
				objDataWrapper.AddParameter("@DESC_DEDUCTIBLE_APPLY",objPersArticleGenInfo.DESC_DEDUCTIBLE_APPLY);
				objDataWrapper.AddParameter("@PROPERTY_USE_PROF_COMM",objPersArticleGenInfo.PROPERTY_USE_PROF_COMM);
				objDataWrapper.AddParameter("@OTHER_INSU_WITH_COMPANY",objPersArticleGenInfo.OTHER_INSU_WITH_COMPANY);
				objDataWrapper.AddParameter("@DESC_INSU_WITH_COMPANY",objPersArticleGenInfo.DESC_INSU_WITH_COMPANY);
				objDataWrapper.AddParameter("@LOSS_OCCURED_LAST_YEARS",objPersArticleGenInfo.LOSS_OCCURED_LAST_YEARS);
				objDataWrapper.AddParameter("@DESC_LOSS_OCCURED_LAST_YEARS",objPersArticleGenInfo.DESC_LOSS_OCCURED_LAST_YEARS);
				objDataWrapper.AddParameter("@DECLINED_CANCELED_COVERAGE",objPersArticleGenInfo.DECLINED_CANCELED_COVERAGE);
				objDataWrapper.AddParameter("@ADD_RATING_COV_INFO",objPersArticleGenInfo.ADD_RATING_COV_INFO);
				objDataWrapper.AddParameter("@CREATED_BY",objPersArticleGenInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objPersArticleGenInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objPersArticleGenInfo.APP_VERSION_ID);

				objDataWrapper.AddParameter("@DESC_PROPERTY_USE_PROF_COMM",objPersArticleGenInfo.DESC_PROPERTY_USE_PROF_COMM);


				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPersArticleGenInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/AddPersArticleGenInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPersArticleGenInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objPersArticleGenInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objPersArticleGenInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPersArticleGenInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPersArticleGenInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New personal article/inland marine general info is added";
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
		/// <param name="objOldPersArticleGenInfo">Model object having old information</param>
		/// <param name="objPersArticleGenInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPersArticleGenInfo objOldPersArticleGenInfo,ClsPersArticleGenInfo objPersArticleGenInfo)
		{
			string strStoredProc = "Proc_UpdateAPP_HOME_OWNER_PER_ART_GEN_INFO";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPersArticleGenInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objPersArticleGenInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objPersArticleGenInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@PROPERTY_EXHIBITED",objPersArticleGenInfo.PROPERTY_EXHIBITED);
				objDataWrapper.AddParameter("@DESC_PROPERTY_EXHIBITED",objPersArticleGenInfo.DESC_PROPERTY_EXHIBITED);
				objDataWrapper.AddParameter("@DEDUCTIBLE_APPLY",objPersArticleGenInfo.DEDUCTIBLE_APPLY);
				objDataWrapper.AddParameter("@DESC_DEDUCTIBLE_APPLY",objPersArticleGenInfo.DESC_DEDUCTIBLE_APPLY);
				objDataWrapper.AddParameter("@PROPERTY_USE_PROF_COMM",objPersArticleGenInfo.PROPERTY_USE_PROF_COMM);
				objDataWrapper.AddParameter("@OTHER_INSU_WITH_COMPANY",objPersArticleGenInfo.OTHER_INSU_WITH_COMPANY);
				objDataWrapper.AddParameter("@DESC_INSU_WITH_COMPANY",objPersArticleGenInfo.DESC_INSU_WITH_COMPANY);
				objDataWrapper.AddParameter("@LOSS_OCCURED_LAST_YEARS",objPersArticleGenInfo.LOSS_OCCURED_LAST_YEARS);
				objDataWrapper.AddParameter("@DESC_LOSS_OCCURED_LAST_YEARS",objPersArticleGenInfo.DESC_LOSS_OCCURED_LAST_YEARS);
				objDataWrapper.AddParameter("@DECLINED_CANCELED_COVERAGE",objPersArticleGenInfo.DECLINED_CANCELED_COVERAGE);
				objDataWrapper.AddParameter("@ADD_RATING_COV_INFO",objPersArticleGenInfo.ADD_RATING_COV_INFO);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPersArticleGenInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPersArticleGenInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@DESC_PROPERTY_USE_PROF_COMM",objPersArticleGenInfo.DESC_PROPERTY_USE_PROF_COMM);

				if(TransactionLogRequired) 
				{
					objPersArticleGenInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/AddPersArticleGenInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldPersArticleGenInfo,objPersArticleGenInfo);
					if(strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID = objPersArticleGenInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objPersArticleGenInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objPersArticleGenInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objPersArticleGenInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Personal article/inland marine general information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

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

		#region GetLocationInfo

		public static string GetPersArticleGenInformationXML(int intCustomerId,int intAppid, int intAppVersionId)
		{

			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_ID",intAppid);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);

				ds = objDataWrapper.ExecuteDataSet(GET_PERSONAL_INFO_PROC);
				
				if (ds.Tables[0].Rows.Count != 0)
				{
					return ds.GetXml();
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

		#region "Policy"
		
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPersArticleGenInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddGenInfo(ClsPolicyPersArticleGenInfo objPersArticleGenInfo)
		{
			string		strStoredProc	=	"Proc_Insert_POL_HOME_OWNER_PER_ART_GEN_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPersArticleGenInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objPersArticleGenInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objPersArticleGenInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@PROPERTY_EXHIBITED",objPersArticleGenInfo.PROPERTY_EXHIBITED);
				objDataWrapper.AddParameter("@DESC_PROPERTY_EXHIBITED",objPersArticleGenInfo.DESC_PROPERTY_EXHIBITED);
				objDataWrapper.AddParameter("@DEDUCTIBLE_APPLY",objPersArticleGenInfo.DEDUCTIBLE_APPLY);
				objDataWrapper.AddParameter("@DESC_DEDUCTIBLE_APPLY",objPersArticleGenInfo.DESC_DEDUCTIBLE_APPLY);
				objDataWrapper.AddParameter("@PROPERTY_USE_PROF_COMM",objPersArticleGenInfo.PROPERTY_USE_PROF_COMM);
				objDataWrapper.AddParameter("@OTHER_INSU_WITH_COMPANY",objPersArticleGenInfo.OTHER_INSU_WITH_COMPANY);
				objDataWrapper.AddParameter("@DESC_INSU_WITH_COMPANY",objPersArticleGenInfo.DESC_INSU_WITH_COMPANY);
				objDataWrapper.AddParameter("@LOSS_OCCURED_LAST_YEARS",objPersArticleGenInfo.LOSS_OCCURED_LAST_YEARS);
				objDataWrapper.AddParameter("@DESC_LOSS_OCCURED_LAST_YEARS",objPersArticleGenInfo.DESC_LOSS_OCCURED_LAST_YEARS);
				objDataWrapper.AddParameter("@DECLINED_CANCELED_COVERAGE",objPersArticleGenInfo.DECLINED_CANCELED_COVERAGE);
				objDataWrapper.AddParameter("@ADD_RATING_COV_INFO",objPersArticleGenInfo.ADD_RATING_COV_INFO);
				objDataWrapper.AddParameter("@CREATED_BY",objPersArticleGenInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objPersArticleGenInfo.CREATED_DATETIME);
				
				objDataWrapper.AddParameter("@DESC_PROPERTY_USE_PROF_COMM",objPersArticleGenInfo.DESC_PROPERTY_USE_PROF_COMM);


				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPersArticleGenInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolcyInlandMarineUnderwritingQue.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPersArticleGenInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objPersArticleGenInfo.POLICY_ID;
					objTransactionInfo.APP_VERSION_ID = objPersArticleGenInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPersArticleGenInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPersArticleGenInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New personal article/inland marine general info is added";
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
		
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldPersArticleGenInfo">Model object having old information</param>
		/// <param name="objPersArticleGenInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateGenInfo(ClsPolicyPersArticleGenInfo objOldPersArticleGenInfo,ClsPolicyPersArticleGenInfo objPersArticleGenInfo)
		{
			string strStoredProc = "Proc_Update_POL_HOME_OWNER_PER_ART_GEN_INFO";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPersArticleGenInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objPersArticleGenInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objPersArticleGenInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@PROPERTY_EXHIBITED",objPersArticleGenInfo.PROPERTY_EXHIBITED);
				objDataWrapper.AddParameter("@DESC_PROPERTY_EXHIBITED",objPersArticleGenInfo.DESC_PROPERTY_EXHIBITED);
				objDataWrapper.AddParameter("@DEDUCTIBLE_APPLY",objPersArticleGenInfo.DEDUCTIBLE_APPLY);
				objDataWrapper.AddParameter("@DESC_DEDUCTIBLE_APPLY",objPersArticleGenInfo.DESC_DEDUCTIBLE_APPLY);
				objDataWrapper.AddParameter("@PROPERTY_USE_PROF_COMM",objPersArticleGenInfo.PROPERTY_USE_PROF_COMM);
				objDataWrapper.AddParameter("@OTHER_INSU_WITH_COMPANY",objPersArticleGenInfo.OTHER_INSU_WITH_COMPANY);
				objDataWrapper.AddParameter("@DESC_INSU_WITH_COMPANY",objPersArticleGenInfo.DESC_INSU_WITH_COMPANY);
				objDataWrapper.AddParameter("@LOSS_OCCURED_LAST_YEARS",objPersArticleGenInfo.LOSS_OCCURED_LAST_YEARS);
				objDataWrapper.AddParameter("@DESC_LOSS_OCCURED_LAST_YEARS",objPersArticleGenInfo.DESC_LOSS_OCCURED_LAST_YEARS);
				objDataWrapper.AddParameter("@DECLINED_CANCELED_COVERAGE",objPersArticleGenInfo.DECLINED_CANCELED_COVERAGE);
				objDataWrapper.AddParameter("@ADD_RATING_COV_INFO",objPersArticleGenInfo.ADD_RATING_COV_INFO);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPersArticleGenInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPersArticleGenInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@DESC_PROPERTY_USE_PROF_COMM",objPersArticleGenInfo.DESC_PROPERTY_USE_PROF_COMM);

				if(TransactionLogRequired) 
				{
					objPersArticleGenInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolcyInlandMarineUnderwritingQue.aspx.resx");
					
					strTranXML = objBuilder.GetTransactionLogXML(objOldPersArticleGenInfo,objPersArticleGenInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID = objPersArticleGenInfo.POLICY_ID;
						objTransactionInfo.APP_VERSION_ID = objPersArticleGenInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objPersArticleGenInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objPersArticleGenInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Personal article/inland marine general information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

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
		
		/// <summary>
		/// Gets a single record from POL_HOME_OWNER_PER_ART_GEN_INFO
		/// </summary>
		/// <param name="intCustomerId"></param>
		/// <param name="intPolicyID"></param>
		/// <param name="intPolicyVersionId"></param>
		/// <returns></returns>
		public DataSet GetPolPersArticleGenInformation(int intCustomerId,int intPolicyID, 
												int intPolicyVersionID)
		{

			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",intPolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionID);

				ds = objDataWrapper.ExecuteDataSet("Proc_Get_POL_HOME_OWNER_PER_ART_GEN_INFO");
				
				return ds;
				
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

	}
}
