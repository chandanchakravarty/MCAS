/******************************************************************************************
<Author					: -   Mohit Gupta
<Start Date				: -	  6/7/2005 3:09:47 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 22 Sept 2005
<Modified By			: - Gaurav Tyagi
<Purpose				: - Few fields removed from Database
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

namespace Cms.BusinessLayer.BlApplication.HomeOwners
{
	/// <summary>
	/// Summary description for ClsDwellingCoverageLimit.
	/// </summary>
	public class ClsDwellingCoverageLimit : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
    	private const string APP_DWELLING_COVERAGE ="APP_DWELLING_COVERAGE";
		
		#region Private Instance Variables
		private bool boolTransactionLog;
		private bool boolTransactionRequired			= true;
		//private int _DWELLING_ID;
		private const string ACTIVATE_DEACTIVATE_PROC= "Proc_ActivateDeactivateAPP_DWELLING_COVERAGE";
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
		#region Public Properties
		
		
		public bool TransactionRequired
		{
			get
			{
				return boolTransactionRequired;
			}
			set
			{
				boolTransactionRequired=value;
			}
		}
		#endregion

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsDwellingCoverageLimit()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDwellingCoverageLimitInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsDwellingCoverageLimitInfo objDwellingCoverageLimitInfo)
		{
			
			string		strStoredProc	=	"Proc_InsertAPP_DWELLING_COVERAGE";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingCoverageLimitInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingCoverageLimitInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@DWELLING_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.DWELLING_LIMIT));
				objDataWrapper.AddParameter("@DWELLING_REPLACE_COST",objDwellingCoverageLimitInfo.DWELLING_REPLACE_COST);
				objDataWrapper.AddParameter("@OTHER_STRU_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.OTHER_STRU_LIMIT));
				objDataWrapper.AddParameter("@PERSONAL_PROP_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_PROP_LIMIT));



				
				objDataWrapper.AddParameter("@OTHER_STRU_DESC",objDwellingCoverageLimitInfo.OTHER_STRU_DESC);
				objDataWrapper.AddParameter("@REPLACEMENT_COST_CONTS",objDwellingCoverageLimitInfo.REPLACEMENT_COST_CONTS);
				objDataWrapper.AddParameter("@LOSS_OF_USE",objDwellingCoverageLimitInfo.LOSS_OF_USE);
				objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT));
				objDataWrapper.AddParameter("@PERSONAL_LIAB_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_LIAB_LIMIT));
				objDataWrapper.AddParameter("@MED_PAY_EACH_PERSON",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.MED_PAY_EACH_PERSON));
				objDataWrapper.AddParameter("@ALL_PERILL_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.ALL_PERILL_DEDUCTIBLE_AMT));
				


				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDwellingCoverageLimitInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\HomeOwners\AddDwellingCoverageLimit.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDwellingCoverageLimitInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDwellingCoverageLimitInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objDwellingCoverageLimitInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDwellingCoverageLimitInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDwellingCoverageLimitInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1694", "");//"New dwelling details coverage limit is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//int DWELLING_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				
				//Update Coverages////////////////////////////////////////////////////////
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingCoverageLimitInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingCoverageLimitInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

				objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS");

				objDataWrapper.ClearParameteres();
				//////////////////////////////////////////////////////////////////////////
				
				//Update Endorsements////////////////////////////////////////////////////////
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingCoverageLimitInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingCoverageLimitInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS");

				objDataWrapper.ClearParameteres();
				//////////////////////////////////////////////////////////////////////////
				
				//Update Watercraft coverages for Home->Watercraft//////////////////
				this.UpdateWatercraftCoverages(objDwellingCoverageLimitInfo.CUSTOMER_ID,objDwellingCoverageLimitInfo.APP_ID,objDwellingCoverageLimitInfo.APP_VERSION_ID,objDataWrapper);
				//End of watercraft coverages

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (returnResult == -1)
				{
					return -1;
				}
				else
				{
					//objDwellingCoverageLimitInfo.DWELLING_ID = DWELLING_ID;
					return returnResult;
				}
				//return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);

				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion
		
		/// <summary>
		/// Updates default watercraft coverages for Home-> Watercraft
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public void UpdateWatercraftCoverages(int customerID,int appID, int appVersionID,DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 )
			{
				objDataWrapper.ClearParameteres();
			}

			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.AddParameter("@APP_ID",appID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);

			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_WATERCRAFT_COVERAGES_FROM_HOME");
		}

		#region POLICY COVERAGE ADD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="polID"></param>
        /// <param name="polVersionID"></param>
        /// <param name="objDataWrapper"></param>
		public void UpdatePolicyWatercraftCoverages(int customerID,int polID, int polVersionID,DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 )
			{
				objDataWrapper.ClearParameteres();
			}

			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.AddParameter("@POLICY_ID",polID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);

			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_WATERCRAFT_COVERAGES_FROM_HOME_POLICY");
		}

		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDwellingCoverageLimitInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPolicyDwellingCoverageLimitInfo objDwellingCoverageLimitInfo)
		{
			
			string		strStoredProc	=	"Proc_InsertPOL_DWELLING_COVERAGE";
			///string		strStoredProc	=	"Proc_InsertAPP_DWELLING_COVERAGE";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objDwellingCoverageLimitInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objDwellingCoverageLimitInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@DWELLING_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.DWELLING_LIMIT));
				objDataWrapper.AddParameter("@DWELLING_REPLACE_COST",objDwellingCoverageLimitInfo.DWELLING_REPLACE_COST);
				objDataWrapper.AddParameter("@OTHER_STRU_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.OTHER_STRU_LIMIT));
				objDataWrapper.AddParameter("@PERSONAL_PROP_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_PROP_LIMIT));
				
				objDataWrapper.AddParameter("@OTHER_STRU_DESC",objDwellingCoverageLimitInfo.OTHER_STRU_DESC);
				objDataWrapper.AddParameter("@REPLACEMENT_COST_CONTS",objDwellingCoverageLimitInfo.REPLACEMENT_COST_CONTS);
				objDataWrapper.AddParameter("@LOSS_OF_USE",objDwellingCoverageLimitInfo.LOSS_OF_USE);
				//objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT));
				if(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT==-1)
					objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",null);
				else
					objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT));
				objDataWrapper.AddParameter("@PERSONAL_LIAB_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_LIAB_LIMIT));
				objDataWrapper.AddParameter("@MED_PAY_EACH_PERSON",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.MED_PAY_EACH_PERSON));
				objDataWrapper.AddParameter("@ALL_PERILL_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.ALL_PERILL_DEDUCTIBLE_AMT));
				


				int returnResult = 0;
				
				if(TransactionLogRequired)
				{
					objDwellingCoverageLimitInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Homeowner\PolicyAddDwellingCoverageLimit.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDwellingCoverageLimitInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDwellingCoverageLimitInfo.CREATED_BY;
					objTransactionInfo.POLICY_ID 			=	objDwellingCoverageLimitInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objDwellingCoverageLimitInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID		=	objDwellingCoverageLimitInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1694", "");//"New dwelling details coverage limit is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//int DWELLING_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				
				//Update Coverages////////////////////////////////////////////////////////
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objDwellingCoverageLimitInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POL_VERSION_ID",objDwellingCoverageLimitInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

				objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS_FOR_POLICY");

				objDataWrapper.ClearParameteres();
				//////////////////////////////////////////////////////////////////////////
				
				//Update Endorsements////////////////////////////////////////////////////////
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objDwellingCoverageLimitInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objDwellingCoverageLimitInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS_FOR_POLICY");

				objDataWrapper.ClearParameteres();

				//Update Watercraft coverages for Home->Watercraft//////////////////
	 			this.UpdatePolicyWatercraftCoverages(objDwellingCoverageLimitInfo.CUSTOMER_ID,objDwellingCoverageLimitInfo.POLICY_ID,objDwellingCoverageLimitInfo.POLICY_VERSION_ID,objDataWrapper);
				//End of watercraft coverages

				//////////////////////////////////////////////////////////////////////////

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (returnResult == -1)
				{
					return -1;
				}
				else
				{
					//objDwellingCoverageLimitInfo.DWELLING_ID = DWELLING_ID;
					return returnResult;
				}
				//return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);

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
		/// <param name="objOldDwellingCoverageLimitInfo">Model object having old information</param>
		/// <param name="objDwellingCoverageLimitInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsDwellingCoverageLimitInfo objOldDwellingCoverageLimitInfo,ClsDwellingCoverageLimitInfo objDwellingCoverageLimitInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdateAPP_DWELLING_COVERAGE";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{

				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingCoverageLimitInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingCoverageLimitInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@DWELLING_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.DWELLING_LIMIT));
				objDataWrapper.AddParameter("@DWELLING_REPLACE_COST",objDwellingCoverageLimitInfo.DWELLING_REPLACE_COST);


				objDataWrapper.AddParameter("@OTHER_STRU_DESC",objDwellingCoverageLimitInfo.OTHER_STRU_DESC);
				objDataWrapper.AddParameter("@OTHER_STRU_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.OTHER_STRU_LIMIT));
				objDataWrapper.AddParameter("@PERSONAL_PROP_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_PROP_LIMIT));
				objDataWrapper.AddParameter("@REPLACEMENT_COST_CONTS",objDwellingCoverageLimitInfo.REPLACEMENT_COST_CONTS);
				objDataWrapper.AddParameter("@LOSS_OF_USE",objDwellingCoverageLimitInfo.LOSS_OF_USE);
				objDataWrapper.AddParameter("@ALL_PERILL_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.ALL_PERILL_DEDUCTIBLE_AMT));
				objDataWrapper.AddParameter("@MED_PAY_EACH_PERSON",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.MED_PAY_EACH_PERSON));
				objDataWrapper.AddParameter("@PERSONAL_LIAB_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_LIAB_LIMIT));
				objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT));

				if(TransactionRequired) 
				{
					objDwellingCoverageLimitInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\HomeOwners\AddDwellingCoverageLimit.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldDwellingCoverageLimitInfo,objDwellingCoverageLimitInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDwellingCoverageLimitInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objDwellingCoverageLimitInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDwellingCoverageLimitInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDwellingCoverageLimitInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1695", "");// "Dwelling details coverage limit is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				//Update Coverages////////////////////////////////////////////////////////
				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingCoverageLimitInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingCoverageLimitInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

				objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS");

				objDataWrapper.ClearParameteres();
				//////////////////////////////////////////////////////////////////////////
				
				//Update Endorsements////////////////////////////////////////////////////////
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingCoverageLimitInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingCoverageLimitInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS");

				objDataWrapper.ClearParameteres();
				//////////////////////////////////////////////////////////////////////////
				
				//Update Watercraft coverages for Home->Watercraft//////////////////
				this.UpdateWatercraftCoverages(objDwellingCoverageLimitInfo.CUSTOMER_ID,objDwellingCoverageLimitInfo.APP_ID,objDwellingCoverageLimitInfo.APP_VERSION_ID,objDataWrapper);
				//End of watercraft coverages

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
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldDwellingCoverageLimitInfo">Model object having old information</param>
		/// <param name="objDwellingCoverageLimitInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPolicyDwellingCoverageLimitInfo objOldDwellingCoverageLimitInfo,ClsPolicyDwellingCoverageLimitInfo objDwellingCoverageLimitInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdatePOL_DWELLING_COVERAGE";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{

				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objDwellingCoverageLimitInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objDwellingCoverageLimitInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@DWELLING_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.DWELLING_LIMIT));
				objDataWrapper.AddParameter("@DWELLING_REPLACE_COST",objDwellingCoverageLimitInfo.DWELLING_REPLACE_COST);


				objDataWrapper.AddParameter("@OTHER_STRU_DESC",objDwellingCoverageLimitInfo.OTHER_STRU_DESC);
				objDataWrapper.AddParameter("@OTHER_STRU_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.OTHER_STRU_LIMIT));
				objDataWrapper.AddParameter("@PERSONAL_PROP_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_PROP_LIMIT));
				objDataWrapper.AddParameter("@REPLACEMENT_COST_CONTS",objDwellingCoverageLimitInfo.REPLACEMENT_COST_CONTS);
				objDataWrapper.AddParameter("@LOSS_OF_USE",objDwellingCoverageLimitInfo.LOSS_OF_USE);
				objDataWrapper.AddParameter("@ALL_PERILL_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.ALL_PERILL_DEDUCTIBLE_AMT));
				objDataWrapper.AddParameter("@MED_PAY_EACH_PERSON",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.MED_PAY_EACH_PERSON));
				objDataWrapper.AddParameter("@PERSONAL_LIAB_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_LIAB_LIMIT));
				//objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT));
				if(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT==-1)
					objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",null);
				else
					objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT));

				//Gaurav : Commented Fields has been removed, itrack issue no : 730
				
				//if(objDwellingCoverageLimitInfo.LOSS_OF_USE_PREMIUM==0)
				//{
				//	objDataWrapper.AddParameter("@LOSS_OF_USE_PREMIUM",null);
				//}
				//else
				//{
				//					objDataWrapper.AddParameter("@LOSS_OF_USE_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.LOSS_OF_USE_PREMIUM));
				//}
				//if(objDwellingCoverageLimitInfo.PERSONAL_LIAB_LIMIT==0)
				//{
				//	objDataWrapper.AddParameter("@PERSONAL_LIAB_LIMIT",null);
				//}
				//else
				//{
					
				//}
				//if(objDwellingCoverageLimitInfo.PERSONAL_LIAB_PREMIUM==0)
				//{
				//	objDataWrapper.AddParameter("@PERSONAL_LIAB_PREMIUM",null);
				//}
				//else
				//{
				//					objDataWrapper.AddParameter("@PERSONAL_LIAB_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_LIAB_PREMIUM));
				//}
				//if(objDwellingCoverageLimitInfo.MED_PAY_EACH_PERSON==0)
				//{
				//	objDataWrapper.AddParameter("@MED_PAY_EACH_PERSON",null);
				//}
				//else
				//{
					
				//}
				//if(objDwellingCoverageLimitInfo.MED_PAY_EACH_PERSON_PREMIUM==0)
				//{
				//	objDataWrapper.AddParameter("@MED_PAY_EACH_PERSON_PREMIUM",null);
				//}
				//else
				//{
				//					objDataWrapper.AddParameter("@MED_PAY_EACH_PERSON_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.MED_PAY_EACH_PERSON_PREMIUM));
				//}
				//				objDataWrapper.AddParameter("@INFLATION_GUARD",objDwellingCoverageLimitInfo.INFLATION_GUARD);
				//if(objDwellingCoverageLimitInfo.ALL_PERILL_DEDUCTIBLE_AMT==0)
				//{
				//	objDataWrapper.AddParameter("@ALL_PERILL_DEDUCTIBLE_AMT",null);
				//}
				//else
				//{
					
				//}
				//if(objDwellingCoverageLimitInfo.WIND_HAIL_DEDUCTIBLE_AMT==0)
				//{
				//	objDataWrapper.AddParameter("@WIND_HAIL_DEDUCTIBLE_AMT",null);
				//}
				//else
				//{
				//					objDataWrapper.AddParameter("@WIND_HAIL_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.WIND_HAIL_DEDUCTIBLE_AMT));
				//}
				//if(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT==0)
				//{
				//	objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",null);
				//}
				//else
				//{
					
				//}
				//				objDataWrapper.AddParameter("@FORM",objDwellingCoverageLimitInfo.FORM);
				//				objDataWrapper.AddParameter("@FORM_OTHER_DESC",objDwellingCoverageLimitInfo.FORM_OTHER_DESC);
				//				objDataWrapper.AddParameter("@COVERAGE",objDwellingCoverageLimitInfo.COVERAGE);
				//				objDataWrapper.AddParameter("@COVERAGE_OTHER_DESC",objDwellingCoverageLimitInfo.COVERAGE_OTHER_DESC);
				//if(objDwellingCoverageLimitInfo.DWELLING_LIMIT==0)
				//{
				//	objDataWrapper.AddParameter("@DWELLING_LIMIT",null);
				//}
				//else
				//{
					
				//}
				//if(objDwellingCoverageLimitInfo.DWELLING_PREMIUM==0)
				//{
				//	objDataWrapper.AddParameter("@DWELLING_PREMIUM",null);
				//}
				//else
				//{
				//					objDataWrapper.AddParameter("@DWELLING_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.DWELLING_PREMIUM));
				//}
				
				//if(objDwellingCoverageLimitInfo.OTHER_STRU_LIMIT==0)
				//{
				//	objDataWrapper.AddParameter("@OTHER_STRU_LIMIT",null);
				//}
				//else
				//{
					
				//}
				
				//if(objDwellingCoverageLimitInfo.PERSONAL_PROP_LIMIT==0)
				//{
				//	objDataWrapper.AddParameter("@PERSONAL_PROP_LIMIT",null);
				//}
				//else
				//{
					
				//}
				if(TransactionRequired) 
				{
					objDwellingCoverageLimitInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Homeowner\PolicyAddDwellingCoverageLimit.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldDwellingCoverageLimitInfo,objDwellingCoverageLimitInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objDwellingCoverageLimitInfo.CREATED_BY;
						objTransactionInfo.POLICY_ID 		=	objDwellingCoverageLimitInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objDwellingCoverageLimitInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objDwellingCoverageLimitInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1695", "");//"Dwelling details coverage limit is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}				

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				//Update Coverages////////////////////////////////////////////////////////
				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objDwellingCoverageLimitInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objDwellingCoverageLimitInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

				objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS_FOR_POLICY");

				objDataWrapper.ClearParameteres();
				//////////////////////////////////////////////////////////////////////////
				
				//Update Endorsements////////////////////////////////////////////////////////
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objDwellingCoverageLimitInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objDwellingCoverageLimitInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS_FOR_POLICY");

				objDataWrapper.ClearParameteres();
				//////////////////////////////////////////////////////////////////////////
				/////Update Watercraft coverages for Home->Watercraft//////////////////
				this.UpdatePolicyWatercraftCoverages(objDwellingCoverageLimitInfo.CUSTOMER_ID,objDwellingCoverageLimitInfo.POLICY_ID,objDwellingCoverageLimitInfo.POLICY_VERSION_ID,objDataWrapper);
				//End of watercraft coverages
				
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

      
  

		//end
		//start

 		public static DataSet GetDwellingCoverageLimitsForPolicy(int intCustomer_ID,int intPol_ID,int intPol_Version_ID ,int intDwelling_ID)
		{
			string strStoredProc = "Proc_GetDwellingCoverageXmlForPolicy";
			DataSet dsDwellingCoverage = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomer_ID);
				objDataWrapper.AddParameter("@POLICY_ID",intPol_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPol_Version_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",intDwelling_ID);
				dsDwellingCoverage = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				return dsDwellingCoverage;
				
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


		//end

		#region GetDwellingCoverageXml

		public static DataSet GetDwellingCoverageLimits(int intCustomer_ID,int intApp_ID,int intApp_Version_ID ,int intDwelling_ID)
		{
			string strStoredProc = "Proc_GetDwellingCoverageXml";
			DataSet dsDwellingCoverage = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomer_ID);
				objDataWrapper.AddParameter("@APP_ID",intApp_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intApp_Version_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",intDwelling_ID);
				dsDwellingCoverage = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				return dsDwellingCoverage;
				
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

		public static string GetDwellingCoverageXml(int intCustomer_ID,int intApp_ID,int intApp_Version_ID ,int intDwelling_ID)
		{
			
			string strStoredProc = "Proc_GetDwellingCoverageXml";
			DataSet dsDwellingCoverage = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomer_ID);
				objDataWrapper.AddParameter("@APP_ID",intApp_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intApp_Version_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",intDwelling_ID);
				dsDwellingCoverage = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsDwellingCoverage.Tables[0].Rows.Count != 0)
				{
					return ClsCommon.GetXMLEncoded(dsDwellingCoverage.Tables[0]); //Ravindra(04-10-2006)
					//return dsDwellingCoverage.GetXml();
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

		public static string GetPolicyDwellingCoverageXml(int intCustomer_ID,int intPol_ID,int intPol_Version_ID ,int intDwelling_ID)
		{
			
			string strStoredProc = "Proc_GetPolicyDwellingCoverageXml";
			DataSet dsDwellingCoverage = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomer_ID);
				objDataWrapper.AddParameter("@POL_ID",intPol_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",intPol_Version_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",intDwelling_ID);
				dsDwellingCoverage = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsDwellingCoverage.Tables[0].Rows.Count != 0)
				{
					return ClsCommon.GetXMLEncoded(dsDwellingCoverage.Tables[0]);
					//return dsDwellingCoverage.GetXml();
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

		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldDwellingCoverageLimitInfo">Model object having old information</param>
		/// <param name="objDwellingCoverageLimitInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Save(ClsDwellingCoverageLimitInfo objDwellingCoverageLimitInfo,DataWrapper objDataWrapper)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_SaveAPP_DWELLING_COVERAGE_ACORD";
			//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
							objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingCoverageLimitInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingCoverageLimitInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);
			objDataWrapper.AddParameter("@DWELLING_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.DWELLING_LIMIT));
			objDataWrapper.AddParameter("@DWELLING_REPLACE_COST",objDwellingCoverageLimitInfo.DWELLING_REPLACE_COST);
			objDataWrapper.AddParameter("@OTHER_STRU_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.OTHER_STRU_LIMIT));
			objDataWrapper.AddParameter("@OTHER_STRU_DESC",objDwellingCoverageLimitInfo.OTHER_STRU_DESC);
			objDataWrapper.AddParameter("@PERSONAL_PROP_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_PROP_LIMIT));
			objDataWrapper.AddParameter("@REPLACEMENT_COST_CONTS",objDwellingCoverageLimitInfo.REPLACEMENT_COST_CONTS);
			objDataWrapper.AddParameter("@LOSS_OF_USE",objDwellingCoverageLimitInfo.LOSS_OF_USE);
			if(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT==-1)
				objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",null);
			else
				objDataWrapper.AddParameter("@THEFT_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.THEFT_DEDUCTIBLE_AMT));
			objDataWrapper.AddParameter("@PERSONAL_LIAB_LIMIT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.PERSONAL_LIAB_LIMIT));
			objDataWrapper.AddParameter("@MED_PAY_EACH_PERSON",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.MED_PAY_EACH_PERSON));
			objDataWrapper.AddParameter("@ALL_PERILL_DEDUCTIBLE_AMT",DefaultValues.GetDoubleNullFromNegative(objDwellingCoverageLimitInfo.ALL_PERILL_DEDUCTIBLE_AMT));

			
			returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
			
			objDataWrapper.ClearParameteres();

			//Trnsaction log
			if(TransactionLogRequired)
			{
				
				objDwellingCoverageLimitInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\HomeOwners\AddDwellingCoverageLimit.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objDwellingCoverageLimitInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objDwellingCoverageLimitInfo.CREATED_BY;
				objTransactionInfo.APP_ID			=	objDwellingCoverageLimitInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID	=	objDwellingCoverageLimitInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID		=	objDwellingCoverageLimitInfo.CUSTOMER_ID;
                objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1694", "");// "New dwelling details coverage limit is added";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				
				objDataWrapper.ClearParameteres();
			}
			///////
			
			
			//Modified on 13 Jan 2006
			//Update Coverages////////////////////////////////////////////////////////
			objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objDwellingCoverageLimitInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingCoverageLimitInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

			objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS");

			objDataWrapper.ClearParameteres();
			//////////////////////////////////////////////////////////////////////////
				
			//Update Endorsements////////////////////////////////////////////////////////
			objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingCoverageLimitInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objDwellingCoverageLimitInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingCoverageLimitInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@DWELLING_ID",objDwellingCoverageLimitInfo.DWELLING_ID);

			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS");

			objDataWrapper.ClearParameteres();
			//////////////////////////////////////////////////////////////////////////
				
				return 1;
		}
/// <summary>
/// 
/// </summary>
/// <param name="customerID"></param>
/// <param name="polID"></param>
/// <param name="polVersionID"></param>
/// <returns></returns>
		public static DataSet GetLiabilityDeductiblesForPolicy(int customerID, int polID, int polVersionID)
		{
			string	strStoredProc =	"Proc_GetLiabilityDeductiblesPolicy";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
		
			//objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		


		public static DataSet GetLiabilityDeductibles(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetLiabilityDeductibles";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
		
			//objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}
		
		
	}
}

