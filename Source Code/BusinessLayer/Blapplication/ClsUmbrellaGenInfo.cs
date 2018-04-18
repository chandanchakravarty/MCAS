using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsUmbrellaGenInfo.
	/// </summary>
	public class ClsUmbrellaGen : Cms.BusinessLayer.BlApplication.clsapplication  
	{
		public ClsUmbrellaGen()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		/// <summary>
		/// Gets a single Employment info record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
		public DataSet GetUmbrellaGenInfo(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetAPP_UMBRELLA_GEN_INFOByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}


		public DataSet GetPolUmbrellaGenInfo(int customerID, int policyID, int policyVersionID)
		{
			string	strStoredProc =	"Proc_GetPOL_UMBRELLA_GEN_INFOByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}

		/// <summary>
		/// Updates a General information record
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Update(Cms.Model.Application.ClsUmbrellaGenInfo objOldInfo,Cms.Model.Application.ClsUmbrellaGenInfo objInfo)
		{
			string	strStoredProc =	"Proc_UpdateAPP_UMBRELLA_GEN_INFO";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/UmbrellaGenInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objInfo);
			}
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, 
				CommandType.StoredProcedure,
				DataWrapper.MaintainTransaction.YES,
				DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@ANY_AIRCRAFT_OWNED_LEASED",objInfo.ANY_AIRCRAFT_OWNED_LEASED);
			objDataWrapper.AddParameter("@ANY_OPERATOR_CON_TRAFFIC",objInfo.ANY_OPERATOR_CON_TRAFFIC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_IMPIRED",objInfo.ANY_OPERATOR_IMPIRED);
			objDataWrapper.AddParameter("@ANY_SWIMMING_POOL",objInfo.ANY_SWIMMING_POOL);
			objDataWrapper.AddParameter("@REAL_STATE_VEHICLE_USED",objInfo.REAL_STATE_VEHICLE_USED);
			objDataWrapper.AddParameter("@REAL_STATE_VEH_OWNED_HIRED",objInfo.REAL_STATE_VEH_OWNED_HIRED);
			objDataWrapper.AddParameter("@ENGAGED_IN_FARMING",objInfo.ENGAGED_IN_FARMING);
			objDataWrapper.AddParameter("@HOLD_NON_COMP_POSITION",objInfo.HOLD_NON_COMP_POSITION);
			objDataWrapper.AddParameter("@ANY_FULL_TIME_EMPLOYEE",objInfo.ANY_FULL_TIME_EMPLOYEE);
			objDataWrapper.AddParameter("@NON_OWNED_PROPERTY_CARE",objInfo.NON_OWNED_PROPERTY_CARE);
			objDataWrapper.AddParameter("@BUSINESS_PROF_ACTIVITY",objInfo.BUSINESS_PROF_ACTIVITY);
			objDataWrapper.AddParameter("@REDUCED_LIMIT_OF_LIBLITY",objInfo.REDUCED_LIMIT_OF_LIBLITY);
			objDataWrapper.AddParameter("@ANY_COVERAGE_DECLINED",objInfo.ANY_COVERAGE_DECLINED);
			objDataWrapper.AddParameter("@ANIMALS_EXOTIC_PETS",objInfo.ANIMALS_EXOTIC_PETS);
			objDataWrapper.AddParameter("@INSU_TRANSFERED_IN_AGENCY",objInfo.INSU_TRANSFERED_IN_AGENCY);
			objDataWrapper.AddParameter("@PENDING_LITIGATIONS",objInfo.PENDING_LITIGATIONS);
			objDataWrapper.AddParameter("@IS_TEMPOLINE",objInfo.IS_TEMPOLINE);
			objDataWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objDataWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@ANY_AIRCRAFT_OWNED_LEASED_DESC",objInfo.ANY_AIRCRAFT_OWNED_LEASED_DESC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_CON_TRAFFIC_DESC",objInfo.ANY_OPERATOR_CON_TRAFFIC_DESC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_IMPIRED_DESC",objInfo.ANY_OPERATOR_IMPIRED_DESC);
			objDataWrapper.AddParameter("@ANY_SWIMMING_POOL_DESC",objInfo.ANY_SWIMMING_POOL_DESC);
			objDataWrapper.AddParameter("@REAL_STATE_VEHICLE_USED_DESC",objInfo.REAL_STATE_VEHICLE_USED_DESC);
			objDataWrapper.AddParameter("@REAL_STATE_VEH_OWNED_HIRED_DESC",objInfo.REAL_STATE_VEH_OWNED_HIRED_DESC);
			objDataWrapper.AddParameter("@ENGAGED_IN_FARMING_DESC",objInfo.ENGAGED_IN_FARMING_DESC);
			objDataWrapper.AddParameter("@HOLD_NON_COMP_POSITION_DESC",objInfo.HOLD_NON_COMP_POSITION_DESC);
			objDataWrapper.AddParameter("@ANY_FULL_TIME_EMPLOYEE_DESC",objInfo.ANY_FULL_TIME_EMPLOYEE_DESC);
			objDataWrapper.AddParameter("@NON_OWNED_PROPERTY_CARE_DESC",objInfo.NON_OWNED_PROPERTY_CARE_DESC);
			objDataWrapper.AddParameter("@BUSINESS_PROF_ACTIVITY_DESC",objInfo.BUSINESS_PROF_ACTIVITY_DESC);
			objDataWrapper.AddParameter("@REDUCED_LIMIT_OF_LIBLITY_DESC",objInfo.REDUCED_LIMIT_OF_LIBLITY_DESC);
			objDataWrapper.AddParameter("@ANIMALS_EXOTIC_PETS_DESC",objInfo.ANIMALS_EXOTIC_PETS_DESC);
			objDataWrapper.AddParameter("@ANY_COVERAGE_DECLINED_DESC",objInfo.ANY_COVERAGE_DECLINED_DESC);
			objDataWrapper.AddParameter("@INSU_TRANSFERED_IN_AGENCY_DESC",objInfo.INSU_TRANSFERED_IN_AGENCY_DESC);
			objDataWrapper.AddParameter("@PENDING_LITIGATIONS_DESC",objInfo.PENDING_LITIGATIONS_DESC);
			objDataWrapper.AddParameter("@IS_TEMPOLINE_DESC",objInfo.IS_TEMPOLINE_DESC);
			objDataWrapper.AddParameter("@HAVE_NON_OWNED_AUTO_POL_DESC",objInfo.HAVE_NON_OWNED_AUTO_POL_DESC);
			objDataWrapper.AddParameter("@INS_DOMICILED_OUTSIDE_DESC",objInfo.INS_DOMICILED_OUTSIDE_DESC);
			objDataWrapper.AddParameter("@HOME_DAY_CARE_DESC",objInfo.HOME_DAY_CARE_DESC);
			objDataWrapper.AddParameter("@HAVE_NON_OWNED_AUTO_POL",objInfo.HAVE_NON_OWNED_AUTO_POL);
			objDataWrapper.AddParameter("@INS_DOMICILED_OUTSIDE",objInfo.INS_DOMICILED_OUTSIDE);
			objDataWrapper.AddParameter("@HOME_DAY_CARE",objInfo.HOME_DAY_CARE);
			objDataWrapper.AddParameter("@UND_REMARKS",objInfo.UND_REMARKS);
			objDataWrapper.AddParameter("@APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC",objInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC);
			objDataWrapper.AddParameter("@APPLI_UNDERSTAND_LIABILITY_EXCLUDED",objInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED);
			objDataWrapper.AddParameter("@AUTO_CYCL_TRUCKS_DESC",objInfo.AUTO_CYCL_TRUCKS_DESC);
			objDataWrapper.AddParameter("@AUTO_CYCL_TRUCKS",objInfo.AUTO_CYCL_TRUCKS);
			objDataWrapper.AddParameter("@RECR_VEH_DESC",objInfo.RECR_VEH_DESC);
			objDataWrapper.AddParameter("@RECR_VEH",objInfo.RECR_VEH);
			objDataWrapper.AddParameter("@WAT_DWELL_DESC",objInfo.WAT_DWELL_DESC);
			objDataWrapper.AddParameter("@WAT_DWELL",objInfo.WAT_DWELL);
			objDataWrapper.AddParameter("@HOME_RENT_DWELL_DESC",objInfo.HOME_RENT_DWELL_DESC);
			objDataWrapper.AddParameter("@HOME_RENT_DWELL",objInfo.HOME_RENT_DWELL);
			objDataWrapper.AddParameter("@CALCULATIONS",objInfo.CALCULATIONS);
			/***** added by Manoj on 5 th jan 2007*******/
			objDataWrapper.AddParameter("@OFFICE_PREMISES",objInfo.OFFICE_PREMISES);
			objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",objInfo.RENTAL_DWELLINGS_UNIT);

			objDataWrapper.AddParameter("@FAMILIES",objInfo.FAMILIES);

			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					

					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
					
					objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Umbrella general information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,RuleType.RiskDependent,0);
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;

		}
		

		public int UpdatePolicyUmbrella(Cms.Model.Policy.Umbrella.ClsUmbrellaGenInfo objOldInfo,Cms.Model.Policy.Umbrella.ClsUmbrellaGenInfo objInfo)
		{
			string	strStoredProc =	"Proc_UpdatePOL_UMBRELLA_GEN_INFO";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Umbrella/PolicyUmbrellaGenInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objInfo);
			}
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, 
				CommandType.StoredProcedure,
				DataWrapper.MaintainTransaction.YES,
				DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
			objDataWrapper.AddParameter("@ANY_AIRCRAFT_OWNED_LEASED",objInfo.ANY_AIRCRAFT_OWNED_LEASED);
			objDataWrapper.AddParameter("@ANY_OPERATOR_CON_TRAFFIC",objInfo.ANY_OPERATOR_CON_TRAFFIC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_IMPIRED",objInfo.ANY_OPERATOR_IMPIRED);
			objDataWrapper.AddParameter("@ANY_SWIMMING_POOL",objInfo.ANY_SWIMMING_POOL);
			objDataWrapper.AddParameter("@REAL_STATE_VEHICLE_USED",objInfo.REAL_STATE_VEHICLE_USED);
			objDataWrapper.AddParameter("@REAL_STATE_VEH_OWNED_HIRED",objInfo.REAL_STATE_VEH_OWNED_HIRED);
			objDataWrapper.AddParameter("@ENGAGED_IN_FARMING",objInfo.ENGAGED_IN_FARMING);
			objDataWrapper.AddParameter("@HOLD_NON_COMP_POSITION",objInfo.HOLD_NON_COMP_POSITION);
			objDataWrapper.AddParameter("@ANY_FULL_TIME_EMPLOYEE",objInfo.ANY_FULL_TIME_EMPLOYEE);
			objDataWrapper.AddParameter("@NON_OWNED_PROPERTY_CARE",objInfo.NON_OWNED_PROPERTY_CARE);
			objDataWrapper.AddParameter("@BUSINESS_PROF_ACTIVITY",objInfo.BUSINESS_PROF_ACTIVITY);
			objDataWrapper.AddParameter("@REDUCED_LIMIT_OF_LIBLITY",objInfo.REDUCED_LIMIT_OF_LIBLITY);
			objDataWrapper.AddParameter("@ANY_COVERAGE_DECLINED",objInfo.ANY_COVERAGE_DECLINED);
			objDataWrapper.AddParameter("@ANIMALS_EXOTIC_PETS",objInfo.ANIMALS_EXOTIC_PETS);
			objDataWrapper.AddParameter("@INSU_TRANSFERED_IN_AGENCY",objInfo.INSU_TRANSFERED_IN_AGENCY);
			objDataWrapper.AddParameter("@PENDING_LITIGATIONS",objInfo.PENDING_LITIGATIONS);
			objDataWrapper.AddParameter("@IS_TEMPOLINE",objInfo.IS_TEMPOLINE);
			objDataWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objDataWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@ANY_AIRCRAFT_OWNED_LEASED_DESC",objInfo.ANY_AIRCRAFT_OWNED_LEASED_DESC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_CON_TRAFFIC_DESC",objInfo.ANY_OPERATOR_CON_TRAFFIC_DESC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_IMPIRED_DESC",objInfo.ANY_OPERATOR_IMPIRED_DESC);
			objDataWrapper.AddParameter("@ANY_SWIMMING_POOL_DESC",objInfo.ANY_SWIMMING_POOL_DESC);
			objDataWrapper.AddParameter("@REAL_STATE_VEHICLE_USED_DESC",objInfo.REAL_STATE_VEHICLE_USED_DESC);
			objDataWrapper.AddParameter("@REAL_STATE_VEH_OWNED_HIRED_DESC",objInfo.REAL_STATE_VEH_OWNED_HIRED_DESC);
			objDataWrapper.AddParameter("@ENGAGED_IN_FARMING_DESC",objInfo.ENGAGED_IN_FARMING_DESC);
			objDataWrapper.AddParameter("@HOLD_NON_COMP_POSITION_DESC",objInfo.HOLD_NON_COMP_POSITION_DESC);
			objDataWrapper.AddParameter("@ANY_FULL_TIME_EMPLOYEE_DESC",objInfo.ANY_FULL_TIME_EMPLOYEE_DESC);
			objDataWrapper.AddParameter("@NON_OWNED_PROPERTY_CARE_DESC",objInfo.NON_OWNED_PROPERTY_CARE_DESC);
			objDataWrapper.AddParameter("@BUSINESS_PROF_ACTIVITY_DESC",objInfo.BUSINESS_PROF_ACTIVITY_DESC);
			objDataWrapper.AddParameter("@REDUCED_LIMIT_OF_LIBLITY_DESC",objInfo.REDUCED_LIMIT_OF_LIBLITY_DESC);
			objDataWrapper.AddParameter("@ANIMALS_EXOTIC_PETS_DESC",objInfo.ANIMALS_EXOTIC_PETS_DESC);
			objDataWrapper.AddParameter("@ANY_COVERAGE_DECLINED_DESC",objInfo.ANY_COVERAGE_DECLINED_DESC);
			objDataWrapper.AddParameter("@INSU_TRANSFERED_IN_AGENCY_DESC",objInfo.INSU_TRANSFERED_IN_AGENCY_DESC);
			objDataWrapper.AddParameter("@PENDING_LITIGATIONS_DESC",objInfo.PENDING_LITIGATIONS_DESC);
			objDataWrapper.AddParameter("@IS_TEMPOLINE_DESC",objInfo.IS_TEMPOLINE_DESC);
			objDataWrapper.AddParameter("@HAVE_NON_OWNED_AUTO_POL_DESC",objInfo.HAVE_NON_OWNED_AUTO_POL_DESC);
			objDataWrapper.AddParameter("@INS_DOMICILED_OUTSIDE_DESC",objInfo.INS_DOMICILED_OUTSIDE_DESC);
			objDataWrapper.AddParameter("@HOME_DAY_CARE_DESC",objInfo.HOME_DAY_CARE_DESC);
			objDataWrapper.AddParameter("@HAVE_NON_OWNED_AUTO_POL",objInfo.HAVE_NON_OWNED_AUTO_POL);
			objDataWrapper.AddParameter("@INS_DOMICILED_OUTSIDE",objInfo.INS_DOMICILED_OUTSIDE);
			objDataWrapper.AddParameter("@HOME_DAY_CARE",objInfo.HOME_DAY_CARE);
			objDataWrapper.AddParameter("@UND_REMARKS",objInfo.UND_REMARKS);
			objDataWrapper.AddParameter("@APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC",objInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC);
			objDataWrapper.AddParameter("@APPLI_UNDERSTAND_LIABILITY_EXCLUDED",objInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED);
			objDataWrapper.AddParameter("@AUTO_CYCL_TRUCKS_DESC",objInfo.AUTO_CYCL_TRUCKS_DESC);
			objDataWrapper.AddParameter("@AUTO_CYCL_TRUCKS",objInfo.AUTO_CYCL_TRUCKS);
			objDataWrapper.AddParameter("@RECR_VEH_DESC",objInfo.RECR_VEH_DESC);
			objDataWrapper.AddParameter("@RECR_VEH",objInfo.RECR_VEH);
			objDataWrapper.AddParameter("@WAT_DWELL_DESC",objInfo.WAT_DWELL_DESC);
			objDataWrapper.AddParameter("@WAT_DWELL",objInfo.WAT_DWELL);
			objDataWrapper.AddParameter("@HOME_RENT_DWELL_DESC",objInfo.HOME_RENT_DWELL_DESC);
			objDataWrapper.AddParameter("@HOME_RENT_DWELL",objInfo.HOME_RENT_DWELL);
			objDataWrapper.AddParameter("@CALCULATIONS",objInfo.CALCULATIONS);
			objDataWrapper.AddParameter("@OFFICE_PREMISES",objInfo.OFFICE_PREMISES);
			objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",objInfo.RENTAL_DWELLINGS_UNIT);
			objDataWrapper.AddParameter("@FAMILIES",objInfo.FAMILIES);

			
			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					

					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID = objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objInfo.POLICY_VERSION_ID;
					
					objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Policy Umbrella general information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.POLICY_ID,objInfo.POLICY_VERSION_ID,RuleType.RiskDependent,0);

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;

		}
		
		/// <summary>
		/// Adds a general information record
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Add(Cms.Model.Application.ClsUmbrellaGenInfo objInfo)
		{
			string	strStoredProc =	"Proc_InsertAPP_UMBRELLA_GEN_INFO";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/UmbrellaGenInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr, 
														CommandType.StoredProcedure,
														DataWrapper.MaintainTransaction.YES,
														DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@ANY_AIRCRAFT_OWNED_LEASED",objInfo.ANY_AIRCRAFT_OWNED_LEASED);
			objDataWrapper.AddParameter("@ANY_OPERATOR_CON_TRAFFIC",objInfo.ANY_OPERATOR_CON_TRAFFIC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_IMPIRED",objInfo.ANY_OPERATOR_IMPIRED);
			objDataWrapper.AddParameter("@ANY_SWIMMING_POOL",objInfo.ANY_SWIMMING_POOL);
			objDataWrapper.AddParameter("@REAL_STATE_VEHICLE_USED",objInfo.REAL_STATE_VEHICLE_USED);
			objDataWrapper.AddParameter("@REAL_STATE_VEH_OWNED_HIRED",objInfo.REAL_STATE_VEH_OWNED_HIRED);
			objDataWrapper.AddParameter("@ENGAGED_IN_FARMING",objInfo.ENGAGED_IN_FARMING);
			objDataWrapper.AddParameter("@HOLD_NON_COMP_POSITION",objInfo.HOLD_NON_COMP_POSITION);
			objDataWrapper.AddParameter("@ANY_FULL_TIME_EMPLOYEE",objInfo.ANY_FULL_TIME_EMPLOYEE);
			objDataWrapper.AddParameter("@NON_OWNED_PROPERTY_CARE",objInfo.NON_OWNED_PROPERTY_CARE);
			objDataWrapper.AddParameter("@BUSINESS_PROF_ACTIVITY",objInfo.BUSINESS_PROF_ACTIVITY);
			objDataWrapper.AddParameter("@REDUCED_LIMIT_OF_LIBLITY",objInfo.REDUCED_LIMIT_OF_LIBLITY);
			objDataWrapper.AddParameter("@ANY_COVERAGE_DECLINED",objInfo.ANY_COVERAGE_DECLINED);
			objDataWrapper.AddParameter("@ANIMALS_EXOTIC_PETS",objInfo.ANIMALS_EXOTIC_PETS);
			objDataWrapper.AddParameter("@INSU_TRANSFERED_IN_AGENCY",objInfo.INSU_TRANSFERED_IN_AGENCY);
			objDataWrapper.AddParameter("@PENDING_LITIGATIONS",objInfo.PENDING_LITIGATIONS);
			objDataWrapper.AddParameter("@IS_TEMPOLINE",objInfo.IS_TEMPOLINE);
			objDataWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objDataWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			objDataWrapper.AddParameter("@ANY_AIRCRAFT_OWNED_LEASED_DESC",objInfo.ANY_AIRCRAFT_OWNED_LEASED_DESC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_CON_TRAFFIC_DESC",objInfo.ANY_OPERATOR_CON_TRAFFIC_DESC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_IMPIRED_DESC",objInfo.ANY_OPERATOR_IMPIRED_DESC);
			objDataWrapper.AddParameter("@ANY_SWIMMING_POOL_DESC",objInfo.ANY_SWIMMING_POOL_DESC);
			objDataWrapper.AddParameter("@REAL_STATE_VEHICLE_USED_DESC",objInfo.REAL_STATE_VEHICLE_USED_DESC);
			objDataWrapper.AddParameter("@REAL_STATE_VEH_OWNED_HIRED_DESC",objInfo.REAL_STATE_VEH_OWNED_HIRED_DESC);
			objDataWrapper.AddParameter("@ENGAGED_IN_FARMING_DESC",objInfo.ENGAGED_IN_FARMING_DESC);
			objDataWrapper.AddParameter("@HOLD_NON_COMP_POSITION_DESC",objInfo.HOLD_NON_COMP_POSITION_DESC);
			objDataWrapper.AddParameter("@ANY_FULL_TIME_EMPLOYEE_DESC",objInfo.ANY_FULL_TIME_EMPLOYEE_DESC);
			objDataWrapper.AddParameter("@NON_OWNED_PROPERTY_CARE_DESC",objInfo.NON_OWNED_PROPERTY_CARE_DESC);
			objDataWrapper.AddParameter("@BUSINESS_PROF_ACTIVITY_DESC",objInfo.BUSINESS_PROF_ACTIVITY_DESC);
			objDataWrapper.AddParameter("@REDUCED_LIMIT_OF_LIBLITY_DESC",objInfo.REDUCED_LIMIT_OF_LIBLITY_DESC);
			objDataWrapper.AddParameter("@ANIMALS_EXOTIC_PETS_DESC",objInfo.ANIMALS_EXOTIC_PETS_DESC);
			objDataWrapper.AddParameter("@ANY_COVERAGE_DECLINED_DESC",objInfo.ANY_COVERAGE_DECLINED_DESC);
			objDataWrapper.AddParameter("@INSU_TRANSFERED_IN_AGENCY_DESC",objInfo.INSU_TRANSFERED_IN_AGENCY_DESC);
			objDataWrapper.AddParameter("@PENDING_LITIGATIONS_DESC",objInfo.PENDING_LITIGATIONS_DESC);
			objDataWrapper.AddParameter("@IS_TEMPOLINE_DESC",objInfo.IS_TEMPOLINE_DESC);
			objDataWrapper.AddParameter("@HAVE_NON_OWNED_AUTO_POL_DESC",objInfo.HAVE_NON_OWNED_AUTO_POL_DESC);
			objDataWrapper.AddParameter("@INS_DOMICILED_OUTSIDE_DESC",objInfo.INS_DOMICILED_OUTSIDE_DESC);
			objDataWrapper.AddParameter("@HOME_DAY_CARE_DESC",objInfo.HOME_DAY_CARE_DESC);
			objDataWrapper.AddParameter("@HAVE_NON_OWNED_AUTO_POL",objInfo.HAVE_NON_OWNED_AUTO_POL);
			objDataWrapper.AddParameter("@INS_DOMICILED_OUTSIDE",objInfo.INS_DOMICILED_OUTSIDE);
			objDataWrapper.AddParameter("@HOME_DAY_CARE",objInfo.HOME_DAY_CARE);
			objDataWrapper.AddParameter("@UND_REMARKS",objInfo.UND_REMARKS);
			objDataWrapper.AddParameter("@APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC",objInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC);
			objDataWrapper.AddParameter("@APPLI_UNDERSTAND_LIABILITY_EXCLUDED",objInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED);
			objDataWrapper.AddParameter("@AUTO_CYCL_TRUCKS_DESC",objInfo.AUTO_CYCL_TRUCKS_DESC);
			objDataWrapper.AddParameter("@AUTO_CYCL_TRUCKS",objInfo.AUTO_CYCL_TRUCKS);
			objDataWrapper.AddParameter("@RECR_VEH_DESC",objInfo.RECR_VEH_DESC);
			objDataWrapper.AddParameter("@RECR_VEH",objInfo.RECR_VEH);
			objDataWrapper.AddParameter("@WAT_DWELL_DESC",objInfo.WAT_DWELL_DESC);
			objDataWrapper.AddParameter("@WAT_DWELL",objInfo.WAT_DWELL);
			objDataWrapper.AddParameter("@HOME_RENT_DWELL_DESC",objInfo.HOME_RENT_DWELL_DESC);
			objDataWrapper.AddParameter("@HOME_RENT_DWELL",objInfo.HOME_RENT_DWELL);
			objDataWrapper.AddParameter("@CALCULATIONS",objInfo.CALCULATIONS);
			/***** added by Manoj on 5 th jan 2007*******/
			objDataWrapper.AddParameter("@OFFICE_PREMISES",objInfo.OFFICE_PREMISES);
			objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",objInfo.RENTAL_DWELLINGS_UNIT);

			objDataWrapper.AddParameter("@FAMILIES",objInfo.FAMILIES);
			
			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New umbrella general information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}

				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,RuleType.RiskDependent,0);
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;

		}


		public int AddPolicyUmbrellaGenInfo(Cms.Model.Policy.Umbrella.ClsUmbrellaGenInfo objInfo)
		{
			string	strStoredProc =	"Proc_InsertPOL_UMBRELLA_GEN_INFO";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Umbrella/PolicyUmbrellaGenInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr, 
				CommandType.StoredProcedure,
				DataWrapper.MaintainTransaction.YES,
				DataWrapper.SetAutoCommit.OFF);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
			objDataWrapper.AddParameter("@ANY_AIRCRAFT_OWNED_LEASED",objInfo.ANY_AIRCRAFT_OWNED_LEASED);
			objDataWrapper.AddParameter("@ANY_OPERATOR_CON_TRAFFIC",objInfo.ANY_OPERATOR_CON_TRAFFIC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_IMPIRED",objInfo.ANY_OPERATOR_IMPIRED);
			objDataWrapper.AddParameter("@ANY_SWIMMING_POOL",objInfo.ANY_SWIMMING_POOL);
			objDataWrapper.AddParameter("@REAL_STATE_VEHICLE_USED",objInfo.REAL_STATE_VEHICLE_USED);
			objDataWrapper.AddParameter("@REAL_STATE_VEH_OWNED_HIRED",objInfo.REAL_STATE_VEH_OWNED_HIRED);
			objDataWrapper.AddParameter("@ENGAGED_IN_FARMING",objInfo.ENGAGED_IN_FARMING);
			objDataWrapper.AddParameter("@HOLD_NON_COMP_POSITION",objInfo.HOLD_NON_COMP_POSITION);
			objDataWrapper.AddParameter("@ANY_FULL_TIME_EMPLOYEE",objInfo.ANY_FULL_TIME_EMPLOYEE);
			objDataWrapper.AddParameter("@NON_OWNED_PROPERTY_CARE",objInfo.NON_OWNED_PROPERTY_CARE);
			objDataWrapper.AddParameter("@BUSINESS_PROF_ACTIVITY",objInfo.BUSINESS_PROF_ACTIVITY);
			objDataWrapper.AddParameter("@REDUCED_LIMIT_OF_LIBLITY",objInfo.REDUCED_LIMIT_OF_LIBLITY);
			objDataWrapper.AddParameter("@ANY_COVERAGE_DECLINED",objInfo.ANY_COVERAGE_DECLINED);
			objDataWrapper.AddParameter("@ANIMALS_EXOTIC_PETS",objInfo.ANIMALS_EXOTIC_PETS);
			objDataWrapper.AddParameter("@INSU_TRANSFERED_IN_AGENCY",objInfo.INSU_TRANSFERED_IN_AGENCY);
			objDataWrapper.AddParameter("@PENDING_LITIGATIONS",objInfo.PENDING_LITIGATIONS);
			objDataWrapper.AddParameter("@IS_TEMPOLINE",objInfo.IS_TEMPOLINE);
			objDataWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objDataWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			objDataWrapper.AddParameter("@ANY_AIRCRAFT_OWNED_LEASED_DESC",objInfo.ANY_AIRCRAFT_OWNED_LEASED_DESC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_CON_TRAFFIC_DESC",objInfo.ANY_OPERATOR_CON_TRAFFIC_DESC);
			objDataWrapper.AddParameter("@ANY_OPERATOR_IMPIRED_DESC",objInfo.ANY_OPERATOR_IMPIRED_DESC);
			objDataWrapper.AddParameter("@ANY_SWIMMING_POOL_DESC",objInfo.ANY_SWIMMING_POOL_DESC);
			objDataWrapper.AddParameter("@REAL_STATE_VEHICLE_USED_DESC",objInfo.REAL_STATE_VEHICLE_USED_DESC);
			objDataWrapper.AddParameter("@REAL_STATE_VEH_OWNED_HIRED_DESC",objInfo.REAL_STATE_VEH_OWNED_HIRED_DESC);
			objDataWrapper.AddParameter("@ENGAGED_IN_FARMING_DESC",objInfo.ENGAGED_IN_FARMING_DESC);
			objDataWrapper.AddParameter("@HOLD_NON_COMP_POSITION_DESC",objInfo.HOLD_NON_COMP_POSITION_DESC);
			objDataWrapper.AddParameter("@ANY_FULL_TIME_EMPLOYEE_DESC",objInfo.ANY_FULL_TIME_EMPLOYEE_DESC);
			objDataWrapper.AddParameter("@NON_OWNED_PROPERTY_CARE_DESC",objInfo.NON_OWNED_PROPERTY_CARE_DESC);
			objDataWrapper.AddParameter("@BUSINESS_PROF_ACTIVITY_DESC",objInfo.BUSINESS_PROF_ACTIVITY_DESC);
			objDataWrapper.AddParameter("@REDUCED_LIMIT_OF_LIBLITY_DESC",objInfo.REDUCED_LIMIT_OF_LIBLITY_DESC);
			objDataWrapper.AddParameter("@ANIMALS_EXOTIC_PETS_DESC",objInfo.ANIMALS_EXOTIC_PETS_DESC);
			objDataWrapper.AddParameter("@ANY_COVERAGE_DECLINED_DESC",objInfo.ANY_COVERAGE_DECLINED_DESC);
			objDataWrapper.AddParameter("@INSU_TRANSFERED_IN_AGENCY_DESC",objInfo.INSU_TRANSFERED_IN_AGENCY_DESC);
			objDataWrapper.AddParameter("@PENDING_LITIGATIONS_DESC",objInfo.PENDING_LITIGATIONS_DESC);
			objDataWrapper.AddParameter("@IS_TEMPOLINE_DESC",objInfo.IS_TEMPOLINE_DESC);
			objDataWrapper.AddParameter("@HAVE_NON_OWNED_AUTO_POL_DESC",objInfo.HAVE_NON_OWNED_AUTO_POL_DESC);
			objDataWrapper.AddParameter("@INS_DOMICILED_OUTSIDE_DESC",objInfo.INS_DOMICILED_OUTSIDE_DESC);
			objDataWrapper.AddParameter("@HOME_DAY_CARE_DESC",objInfo.HOME_DAY_CARE_DESC);
			objDataWrapper.AddParameter("@HAVE_NON_OWNED_AUTO_POL",objInfo.HAVE_NON_OWNED_AUTO_POL);
			objDataWrapper.AddParameter("@INS_DOMICILED_OUTSIDE",objInfo.INS_DOMICILED_OUTSIDE);
			objDataWrapper.AddParameter("@HOME_DAY_CARE",objInfo.HOME_DAY_CARE);
			objDataWrapper.AddParameter("@UND_REMARKS",objInfo.UND_REMARKS);
			objDataWrapper.AddParameter("@APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC",objInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC);
			objDataWrapper.AddParameter("@APPLI_UNDERSTAND_LIABILITY_EXCLUDED",objInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED);
			objDataWrapper.AddParameter("@AUTO_CYCL_TRUCKS_DESC",objInfo.AUTO_CYCL_TRUCKS_DESC);
			objDataWrapper.AddParameter("@AUTO_CYCL_TRUCKS",objInfo.AUTO_CYCL_TRUCKS);
			objDataWrapper.AddParameter("@RECR_VEH_DESC",objInfo.RECR_VEH_DESC);
			objDataWrapper.AddParameter("@RECR_VEH",objInfo.RECR_VEH);
			objDataWrapper.AddParameter("@WAT_DWELL_DESC",objInfo.WAT_DWELL_DESC);
			objDataWrapper.AddParameter("@WAT_DWELL",objInfo.WAT_DWELL);
			objDataWrapper.AddParameter("@HOME_RENT_DWELL_DESC",objInfo.HOME_RENT_DWELL_DESC);
			objDataWrapper.AddParameter("@HOME_RENT_DWELL",objInfo.HOME_RENT_DWELL);
			objDataWrapper.AddParameter("@CALCULATIONS",objInfo.CALCULATIONS);
			objDataWrapper.AddParameter("@OFFICE_PREMISES",objInfo.OFFICE_PREMISES);
			objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",objInfo.RENTAL_DWELLINGS_UNIT);
			objDataWrapper.AddParameter("@FAMILIES",objInfo.FAMILIES);

			
			SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID = objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objInfo.POLICY_VERSION_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Policy umbrella general information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.POLICY_ID,objInfo.POLICY_VERSION_ID,RuleType.RiskDependent,0);
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;

		}



//		public static string FetchUmbrellaOperatorInfoXML(int CustomerID,int AppID, int AppVersionID, int DriverID)
//		{
//			try
//			{
//				DataSet dsTemp = new DataSet();
//			
//				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
//				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
//				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
//				objDataWrapper.AddParameter("@DRIVER_ID",DriverID,SqlDbType.Int);
//
//				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbrellaOperatorInfo");
//			
//				//return dsTemp.GetXml();
//				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
//				 
//			}
//			catch(Exception exc)
//			{throw (exc);}
//			finally
//			{}
//		}

		public static DataSet FetchUmbrellaBoatInfo(int appId,int customerId,int appVersionId)
		{
			string		strStoredProc	=	"Proc_FetchUmbrellaBoatInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);                           
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}


	}
}
