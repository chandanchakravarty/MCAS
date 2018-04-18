/******************************************************************************************
<Author				    : -   Avijit Goswami
<Start Date				: -	19/03/2012 
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: 
<Modified By			: 
<Purpose				: 
<Modified Date		    : - 
<Modified By		    : - 
<Purpose			    : - 
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
using Cms.Model.Policy;
using Cms.Model.Maintenance;

namespace Cms.BusinessLayer.BlApplication
{
    public class ClsPolicyUWQMarine : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        private const string POL_MARINE_CARGO_GEN_INFO = "Proc_InsertPOL_MARINE_CARGO_GEN_INFO";
        private bool boolTransactionLog;

        #region Public Properties
        public bool TransactionLog
        {
            set
            {
                boolTransactionLog = value;
            }
            get
            {
                return boolTransactionLog;
            }
        }
        #endregion

        #region Constructors
        public ClsPolicyUWQMarine()
        {
            boolTransactionLog = base.TransactionLogRequired;
        }
        #endregion
        public int Add(ClsPolicyUWQMarineInfo objClsPolicyUWQMarineInfo)
        {
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objClsPolicyUWQMarineInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POL_ID", objClsPolicyUWQMarineInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POL_VERSION_ID", objClsPolicyUWQMarineInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@ANY_FARMING_BUSINESS_COND", objClsPolicyUWQMarineInfo.ANY_FARMING_BUSINESS_COND);
                objDataWrapper.AddParameter("@ANY_RESIDENCE_EMPLOYEE", objClsPolicyUWQMarineInfo.ANY_RESIDENCE_EMPLOYEE);
                objDataWrapper.AddParameter("@DESC_RESIDENCE_EMPLOYEE", objClsPolicyUWQMarineInfo.DESC_RESIDENCE_EMPLOYEE);
                objDataWrapper.AddParameter("@ANY_OTHER_RESI_OWNED", objClsPolicyUWQMarineInfo.ANY_OTHER_RESI_OWNED);
                objDataWrapper.AddParameter("@DESC_OTHER_RESIDENCE", objClsPolicyUWQMarineInfo.DESC_OTHER_RESIDENCE);
                objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP", objClsPolicyUWQMarineInfo.ANY_OTH_INSU_COMP);
                objDataWrapper.AddParameter("@DESC_OTHER_INSURANCE", objClsPolicyUWQMarineInfo.DESC_OTHER_INSURANCE);
                objDataWrapper.AddParameter("@HAS_INSU_TRANSFERED_AGENCY", objClsPolicyUWQMarineInfo.HAS_INSU_TRANSFERED_AGENCY);
                objDataWrapper.AddParameter("@DESC_INSU_TRANSFERED_AGENCY", objClsPolicyUWQMarineInfo.DESC_INSU_TRANSFERED_AGENCY);
                objDataWrapper.AddParameter("@ANY_COV_DECLINED_CANCELED", objClsPolicyUWQMarineInfo.ANY_COV_DECLINED_CANCELED);
                objDataWrapper.AddParameter("@DESC_COV_DECLINED_CANCELED", objClsPolicyUWQMarineInfo.DESC_COV_DECLINED_CANCELED);

                objDataWrapper.AddParameter("@ANIMALS_EXO_PETS_HISTORY", objClsPolicyUWQMarineInfo.ANIMALS_EXO_PETS_HISTORY);
                objDataWrapper.AddParameter("@BREED", objClsPolicyUWQMarineInfo.BREED);
                objDataWrapper.AddParameter("@OTHER_DESCRIPTION", objClsPolicyUWQMarineInfo.OTHER_DESCRIPTION);
                objDataWrapper.AddParameter("@CONVICTION_DEGREE_IN_PAST", objClsPolicyUWQMarineInfo.CONVICTION_DEGREE_IN_PAST);
                objDataWrapper.AddParameter("@DESC_CONVICTION_DEGREE_IN_PAST", objClsPolicyUWQMarineInfo.DESC_CONVICTION_DEGREE_IN_PAST);
                objDataWrapper.AddParameter("@ANY_RENOVATION", objClsPolicyUWQMarineInfo.ANY_RENOVATION);
                objDataWrapper.AddParameter("@DESC_RENOVATION", objClsPolicyUWQMarineInfo.DESC_RENOVATION);
                objDataWrapper.AddParameter("@TRAMPOLINE", objClsPolicyUWQMarineInfo.TRAMPOLINE);
                objDataWrapper.AddParameter("@DESC_TRAMPOLINE", objClsPolicyUWQMarineInfo.DESC_TRAMPOLINE);
                objDataWrapper.AddParameter("@LEAD_PAINT_HAZARD", objClsPolicyUWQMarineInfo.LEAD_PAINT_HAZARD);
                objDataWrapper.AddParameter("@DESC_LEAD_PAINT_HAZARD", objClsPolicyUWQMarineInfo.DESC_LEAD_PAINT_HAZARD);
                objDataWrapper.AddParameter("@RENTERS", objClsPolicyUWQMarineInfo.RENTERS);
                objDataWrapper.AddParameter("@DESC_RENTERS", objClsPolicyUWQMarineInfo.DESC_RENTERS);
                objDataWrapper.AddParameter("@BUILD_UNDER_CON_GEN_CONT", objClsPolicyUWQMarineInfo.BUILD_UNDER_CON_GEN_CONT);
                objDataWrapper.AddParameter("@REMARKS", objClsPolicyUWQMarineInfo.REMARKS);
                objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED", objClsPolicyUWQMarineInfo.MULTI_POLICY_DISC_APPLIED);
                objDataWrapper.AddParameter("@CREATED_BY", objClsPolicyUWQMarineInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objClsPolicyUWQMarineInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@NO_OF_PETS", objClsPolicyUWQMarineInfo.NO_OF_PETS);
                objDataWrapper.AddParameter("@IS_SWIMPOLL_HOTTUB", objClsPolicyUWQMarineInfo.IS_SWIMPOLL_HOTTUB);
                if (objClsPolicyUWQMarineInfo.LAST_INSPECTED_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@LAST_INSPECTED_DATE", objClsPolicyUWQMarineInfo.LAST_INSPECTED_DATE);
                else
                    objDataWrapper.AddParameter("@LAST_INSPECTED_DATE", null);
                objDataWrapper.AddParameter("@IS_RENTED_IN_PART", objClsPolicyUWQMarineInfo.IS_RENTED_IN_PART);
                objDataWrapper.AddParameter("@IS_VACENT_OCCUPY", objClsPolicyUWQMarineInfo.IS_VACENT_OCCUPY);
                objDataWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER", objClsPolicyUWQMarineInfo.IS_DWELLING_OWNED_BY_OTHER);
                objDataWrapper.AddParameter("@IS_PROP_NEXT_COMMERICAL", objClsPolicyUWQMarineInfo.IS_PROP_NEXT_COMMERICAL);
                objDataWrapper.AddParameter("@DESC_PROPERTY", objClsPolicyUWQMarineInfo.DESC_PROPERTY);
                objDataWrapper.AddParameter("@ARE_STAIRWAYS_PRESENT", objClsPolicyUWQMarineInfo.ARE_STAIRWAYS_PRESENT);
                objDataWrapper.AddParameter("@DESC_STAIRWAYS", objClsPolicyUWQMarineInfo.DESC_STAIRWAYS);
                objDataWrapper.AddParameter("@IS_OWNERS_DWELLING_CHANGED", objClsPolicyUWQMarineInfo.IS_OWNERS_DWELLING_CHANGED);
                objDataWrapper.AddParameter("@DESC_OWNER", objClsPolicyUWQMarineInfo.DESC_OWNER);
                objDataWrapper.AddParameter("@DESC_VACENT_OCCUPY", objClsPolicyUWQMarineInfo.DESC_VACENT_OCCUPY);
                objDataWrapper.AddParameter("@DESC_RENTED_IN_PART", objClsPolicyUWQMarineInfo.DESC_RENTED_IN_PART);
                objDataWrapper.AddParameter("@DESC_DWELLING_OWNED_BY_OTHER", objClsPolicyUWQMarineInfo.DESC_DWELLING_OWNED_BY_OTHER);
                objDataWrapper.AddParameter("@ANY_HEATING_SOURCE", objClsPolicyUWQMarineInfo.ANY_HEATING_SOURCE);
                objDataWrapper.AddParameter("@DESC_ANY_HEATING_SOURCE", objClsPolicyUWQMarineInfo.DESC_ANY_HEATING_SOURCE);
                objDataWrapper.AddParameter("@NON_SMOKER_CREDIT", objClsPolicyUWQMarineInfo.NON_SMOKER_CREDIT);
                objDataWrapper.AddParameter("@SWIMMING_POOL", objClsPolicyUWQMarineInfo.SWIMMING_POOL);
                objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE", objClsPolicyUWQMarineInfo.SWIMMING_POOL_TYPE);                
                objDataWrapper.AddParameter("@Any_Forming", objClsPolicyUWQMarineInfo.Any_Forming);
                objDataWrapper.AddParameter("@Premises", DefaultValues.GetIntNull(objClsPolicyUWQMarineInfo.Premises));
                objDataWrapper.AddParameter("@Of_Acres", DefaultValues.GetDoubleNull(objClsPolicyUWQMarineInfo.Of_Acres));
                objDataWrapper.AddParameter("@IsAny_Horse", DefaultValues.GetStringNull(objClsPolicyUWQMarineInfo.IsAny_Horse));
                objDataWrapper.AddParameter("@Of_Acres_P", DefaultValues.GetDoubleNull(objClsPolicyUWQMarineInfo.Of_Acres_P));
                objDataWrapper.AddParameter("@No_Horses", DefaultValues.GetIntNull(objClsPolicyUWQMarineInfo.No_Horses));                
                objDataWrapper.AddParameter("@location", DefaultValues.GetStringNull(objClsPolicyUWQMarineInfo.Location));
                objDataWrapper.AddParameter("@DESC_location", DefaultValues.GetStringNull(objClsPolicyUWQMarineInfo.DESC_Location));                
                objDataWrapper.AddParameter("@DESC_IS_SWIMPOLL_HOTTUB", objClsPolicyUWQMarineInfo.DESC_IS_SWIMPOLL_HOTTUB);
                objDataWrapper.AddParameter("@DESC_MULTI_POLICY_DISC_APPLIED", objClsPolicyUWQMarineInfo.DESC_MULTI_POLICY_DISC_APPLIED);
                objDataWrapper.AddParameter("@DESC_BUILD_UNDER_CON_GEN_CONT", objClsPolicyUWQMarineInfo.DESC_BUILD_UNDER_CON_GEN_CONT);
                objDataWrapper.AddParameter("@DIVING_BOARD", objClsPolicyUWQMarineInfo.DIVING_BOARD);
                objDataWrapper.AddParameter("@APPROVED_FENCE", objClsPolicyUWQMarineInfo.APPROVED_FENCE);
                objDataWrapper.AddParameter("@SLIDE", objClsPolicyUWQMarineInfo.SLIDE);
                objDataWrapper.AddParameter("@YEARS_INSU_WOL", DefaultValues.GetIntNull(objClsPolicyUWQMarineInfo.YEARS_INSU_WOL));
                objDataWrapper.AddParameter("@YEARS_INSU", DefaultValues.GetIntNull(objClsPolicyUWQMarineInfo.YEARS_INSU));
                objDataWrapper.AddParameter("@DESC_FARMING_BUSINESS_COND", DefaultValues.GetStringNull(objClsPolicyUWQMarineInfo.DESC_FARMING_BUSINESS_COND));
                objDataWrapper.AddParameter("@PROVIDE_HOME_DAY_CARE", objClsPolicyUWQMarineInfo.PROVIDE_HOME_DAY_CARE);
                                
                objDataWrapper.AddParameter("@MODULAR_MANUFACTURED_HOME", objClsPolicyUWQMarineInfo.MODULAR_MANUFACTURED_HOME);
                objDataWrapper.AddParameter("@BUILT_ON_CONTINUOUS_FOUNDATION", objClsPolicyUWQMarineInfo.BUILT_ON_CONTINUOUS_FOUNDATION);
                objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE", objClsPolicyUWQMarineInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE);
                objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC", objClsPolicyUWQMarineInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC);

                objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN", objClsPolicyUWQMarineInfo.PROPERTY_ON_MORE_THAN);
                objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN_DESC", objClsPolicyUWQMarineInfo.PROPERTY_ON_MORE_THAN_DESC);
                objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME", objClsPolicyUWQMarineInfo.DWELLING_MOBILE_HOME);
                objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME_DESC", objClsPolicyUWQMarineInfo.DWELLING_MOBILE_HOME_DESC);
                objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART", objClsPolicyUWQMarineInfo.PROPERTY_USED_WHOLE_PART);
                objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART_DESC", objClsPolicyUWQMarineInfo.PROPERTY_USED_WHOLE_PART_DESC);
                objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES", objClsPolicyUWQMarineInfo.ANY_PRIOR_LOSSES);
                objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC", objClsPolicyUWQMarineInfo.ANY_PRIOR_LOSSES_DESC);                
                if (objClsPolicyUWQMarineInfo.NON_WEATHER_CLAIMS != -1)
                    objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS", objClsPolicyUWQMarineInfo.NON_WEATHER_CLAIMS);
                else
                    objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS", System.DBNull.Value);
                if (objClsPolicyUWQMarineInfo.WEATHER_CLAIMS != -1)
                    objDataWrapper.AddParameter("@WEATHER_CLAIMS", objClsPolicyUWQMarineInfo.WEATHER_CLAIMS);
                else
                    objDataWrapper.AddParameter("@WEATHER_CLAIMS", System.DBNull.Value);

                int returnResult = 0;

                if (TransactionLog)
                {
                    objClsPolicyUWQMarineInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/MariTime/PolicyUWQMarine.aspx.resx");
                    string strTranXML = objBuilder.GetTransactionLogXML(objClsPolicyUWQMarineInfo);
                    ClsTransactionInfo objTransactionInfo = new ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.POLICY_ID = objClsPolicyUWQMarineInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objClsPolicyUWQMarineInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objClsPolicyUWQMarineInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY = objClsPolicyUWQMarineInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "New information is added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(POL_MARINE_CARGO_GEN_INFO, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(POL_MARINE_CARGO_GEN_INFO);
                }
                objDataWrapper.ClearParameteres();
                
                //ClsHomeCoverages objCoverage;
                //if (objClsPolicyUWQMarineInfo.CalledFrom == "HOME")
                //{
                //    objCoverage = new ClsHomeCoverages();
                //    objCoverage.UpdateCoveragesByRuleApp(objDataWrapper, objClsPolicyUWQMarineInfo.CUSTOMER_ID, objClsPolicyUWQMarineInfo.POLICY_ID, objClsPolicyUWQMarineInfo.POLICY_VERSION_ID, RuleType.OtherAppDependent);
                //}
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        public int Update(ClsPolicyUWQMarineInfo objOldClsPolicyUWQMarineInfo, ClsPolicyUWQMarineInfo objClsPolicyUWQMarineInfo)
        {
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "Proc_UpdatePOL_MARINE_CARGO_GEN_INFO";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objClsPolicyUWQMarineInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POL_ID", objClsPolicyUWQMarineInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POL_VERSION_ID", objClsPolicyUWQMarineInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@ANY_FARMING_BUSINESS_COND", objClsPolicyUWQMarineInfo.ANY_FARMING_BUSINESS_COND);
                //objDataWrapper.AddParameter("@ANY_RESIDENCE_EMPLOYEE", objClsPolicyUWQMarineInfo.ANY_RESIDENCE_EMPLOYEE);
                //objDataWrapper.AddParameter("@DESC_RESIDENCE_EMPLOYEE", objClsPolicyUWQMarineInfo.DESC_RESIDENCE_EMPLOYEE);
                //objDataWrapper.AddParameter("@ANY_OTHER_RESI_OWNED", objClsPolicyUWQMarineInfo.ANY_OTHER_RESI_OWNED);
                //objDataWrapper.AddParameter("@DESC_OTHER_RESIDENCE", objClsPolicyUWQMarineInfo.DESC_OTHER_RESIDENCE);
                //objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP", objClsPolicyUWQMarineInfo.ANY_OTH_INSU_COMP);
                //objDataWrapper.AddParameter("@DESC_OTHER_INSURANCE", objClsPolicyUWQMarineInfo.DESC_OTHER_INSURANCE);
                //objDataWrapper.AddParameter("@HAS_INSU_TRANSFERED_AGENCY", objClsPolicyUWQMarineInfo.HAS_INSU_TRANSFERED_AGENCY);
                //objDataWrapper.AddParameter("@DESC_INSU_TRANSFERED_AGENCY", objClsPolicyUWQMarineInfo.DESC_INSU_TRANSFERED_AGENCY);
                //objDataWrapper.AddParameter("@ANY_COV_DECLINED_CANCELED", objClsPolicyUWQMarineInfo.ANY_COV_DECLINED_CANCELED);
                //objDataWrapper.AddParameter("@DESC_COV_DECLINED_CANCELED", objClsPolicyUWQMarineInfo.DESC_COV_DECLINED_CANCELED);

                //objDataWrapper.AddParameter("@ANIMALS_EXO_PETS_HISTORY", objClsPolicyUWQMarineInfo.ANIMALS_EXO_PETS_HISTORY);
                //objDataWrapper.AddParameter("@BREED", objClsPolicyUWQMarineInfo.BREED);
                //objDataWrapper.AddParameter("@OTHER_DESCRIPTION", objClsPolicyUWQMarineInfo.OTHER_DESCRIPTION);
                //objDataWrapper.AddParameter("@CONVICTION_DEGREE_IN_PAST", objClsPolicyUWQMarineInfo.CONVICTION_DEGREE_IN_PAST);
                //objDataWrapper.AddParameter("@DESC_CONVICTION_DEGREE_IN_PAST", objClsPolicyUWQMarineInfo.DESC_CONVICTION_DEGREE_IN_PAST);
                //objDataWrapper.AddParameter("@ANY_RENOVATION", objClsPolicyUWQMarineInfo.ANY_RENOVATION);
                //objDataWrapper.AddParameter("@DESC_RENOVATION", objClsPolicyUWQMarineInfo.DESC_RENOVATION);
                //objDataWrapper.AddParameter("@TRAMPOLINE", objClsPolicyUWQMarineInfo.TRAMPOLINE);
                //objDataWrapper.AddParameter("@DESC_TRAMPOLINE", objClsPolicyUWQMarineInfo.DESC_TRAMPOLINE);
                //objDataWrapper.AddParameter("@LEAD_PAINT_HAZARD", objClsPolicyUWQMarineInfo.LEAD_PAINT_HAZARD);
                //objDataWrapper.AddParameter("@DESC_LEAD_PAINT_HAZARD", objClsPolicyUWQMarineInfo.DESC_LEAD_PAINT_HAZARD);
                //objDataWrapper.AddParameter("@RENTERS", objClsPolicyUWQMarineInfo.RENTERS);
                //objDataWrapper.AddParameter("@DESC_RENTERS", objClsPolicyUWQMarineInfo.DESC_RENTERS);
                //objDataWrapper.AddParameter("@BUILD_UNDER_CON_GEN_CONT", objClsPolicyUWQMarineInfo.BUILD_UNDER_CON_GEN_CONT);
                objDataWrapper.AddParameter("@REMARKS", objClsPolicyUWQMarineInfo.REMARKS);
                //objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED", objClsPolicyUWQMarineInfo.MULTI_POLICY_DISC_APPLIED);
                //objDataWrapper.AddParameter("@CREATED_BY", objClsPolicyUWQMarineInfo.CREATED_BY);
                //objDataWrapper.AddParameter("@CREATED_DATETIME", objClsPolicyUWQMarineInfo.CREATED_DATETIME);
                //objDataWrapper.AddParameter("@NO_OF_PETS", objClsPolicyUWQMarineInfo.NO_OF_PETS);
                //objDataWrapper.AddParameter("@IS_SWIMPOLL_HOTTUB", objClsPolicyUWQMarineInfo.IS_SWIMPOLL_HOTTUB);
                //if (objClsPolicyUWQMarineInfo.LAST_INSPECTED_DATE != DateTime.MinValue)
                //    objDataWrapper.AddParameter("@LAST_INSPECTED_DATE", objClsPolicyUWQMarineInfo.LAST_INSPECTED_DATE);
                //else
                //    objDataWrapper.AddParameter("@LAST_INSPECTED_DATE", null);
                //objDataWrapper.AddParameter("@IS_RENTED_IN_PART", objClsPolicyUWQMarineInfo.IS_RENTED_IN_PART);
                //objDataWrapper.AddParameter("@IS_VACENT_OCCUPY", objClsPolicyUWQMarineInfo.IS_VACENT_OCCUPY);
                //objDataWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER", objClsPolicyUWQMarineInfo.IS_DWELLING_OWNED_BY_OTHER);
                //objDataWrapper.AddParameter("@IS_PROP_NEXT_COMMERICAL", objClsPolicyUWQMarineInfo.IS_PROP_NEXT_COMMERICAL);
                //objDataWrapper.AddParameter("@DESC_PROPERTY", objClsPolicyUWQMarineInfo.DESC_PROPERTY);
                //objDataWrapper.AddParameter("@ARE_STAIRWAYS_PRESENT", objClsPolicyUWQMarineInfo.ARE_STAIRWAYS_PRESENT);
                //objDataWrapper.AddParameter("@DESC_STAIRWAYS", objClsPolicyUWQMarineInfo.DESC_STAIRWAYS);
                //objDataWrapper.AddParameter("@IS_OWNERS_DWELLING_CHANGED", objClsPolicyUWQMarineInfo.IS_OWNERS_DWELLING_CHANGED);
                //objDataWrapper.AddParameter("@DESC_OWNER", objClsPolicyUWQMarineInfo.DESC_OWNER);
                //objDataWrapper.AddParameter("@DESC_VACENT_OCCUPY", objClsPolicyUWQMarineInfo.DESC_VACENT_OCCUPY);
                //objDataWrapper.AddParameter("@DESC_RENTED_IN_PART", objClsPolicyUWQMarineInfo.DESC_RENTED_IN_PART);
                //objDataWrapper.AddParameter("@DESC_DWELLING_OWNED_BY_OTHER", objClsPolicyUWQMarineInfo.DESC_DWELLING_OWNED_BY_OTHER);
                //objDataWrapper.AddParameter("@ANY_HEATING_SOURCE", objClsPolicyUWQMarineInfo.ANY_HEATING_SOURCE);
                //objDataWrapper.AddParameter("@DESC_ANY_HEATING_SOURCE", objClsPolicyUWQMarineInfo.DESC_ANY_HEATING_SOURCE);
                //objDataWrapper.AddParameter("@NON_SMOKER_CREDIT", objClsPolicyUWQMarineInfo.NON_SMOKER_CREDIT);
                //objDataWrapper.AddParameter("@SWIMMING_POOL", objClsPolicyUWQMarineInfo.SWIMMING_POOL);
                //objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE", objClsPolicyUWQMarineInfo.SWIMMING_POOL_TYPE);
                //objDataWrapper.AddParameter("@Any_Forming", objClsPolicyUWQMarineInfo.Any_Forming);
                //objDataWrapper.AddParameter("@Premises", DefaultValues.GetIntNull(objClsPolicyUWQMarineInfo.Premises));
                //objDataWrapper.AddParameter("@Of_Acres", DefaultValues.GetDoubleNull(objClsPolicyUWQMarineInfo.Of_Acres));
                //objDataWrapper.AddParameter("@IsAny_Horse", DefaultValues.GetStringNull(objClsPolicyUWQMarineInfo.IsAny_Horse));
                //objDataWrapper.AddParameter("@Of_Acres_P", DefaultValues.GetDoubleNull(objClsPolicyUWQMarineInfo.Of_Acres_P));
                //objDataWrapper.AddParameter("@No_Horses", DefaultValues.GetIntNull(objClsPolicyUWQMarineInfo.No_Horses));
                //objDataWrapper.AddParameter("@location", DefaultValues.GetStringNull(objClsPolicyUWQMarineInfo.Location));
                //objDataWrapper.AddParameter("@DESC_location", DefaultValues.GetStringNull(objClsPolicyUWQMarineInfo.DESC_Location));
                //objDataWrapper.AddParameter("@DESC_IS_SWIMPOLL_HOTTUB", objClsPolicyUWQMarineInfo.DESC_IS_SWIMPOLL_HOTTUB);
                //objDataWrapper.AddParameter("@DESC_MULTI_POLICY_DISC_APPLIED", objClsPolicyUWQMarineInfo.DESC_MULTI_POLICY_DISC_APPLIED);
                //objDataWrapper.AddParameter("@DESC_BUILD_UNDER_CON_GEN_CONT", objClsPolicyUWQMarineInfo.DESC_BUILD_UNDER_CON_GEN_CONT);
                //objDataWrapper.AddParameter("@DIVING_BOARD", objClsPolicyUWQMarineInfo.DIVING_BOARD);
                //objDataWrapper.AddParameter("@APPROVED_FENCE", objClsPolicyUWQMarineInfo.APPROVED_FENCE);
                //objDataWrapper.AddParameter("@SLIDE", objClsPolicyUWQMarineInfo.SLIDE);
                //objDataWrapper.AddParameter("@YEARS_INSU_WOL", DefaultValues.GetIntNull(objClsPolicyUWQMarineInfo.YEARS_INSU_WOL));
                //objDataWrapper.AddParameter("@YEARS_INSU", DefaultValues.GetIntNull(objClsPolicyUWQMarineInfo.YEARS_INSU));
                //objDataWrapper.AddParameter("@DESC_FARMING_BUSINESS_COND", DefaultValues.GetStringNull(objClsPolicyUWQMarineInfo.DESC_FARMING_BUSINESS_COND));
                //objDataWrapper.AddParameter("@PROVIDE_HOME_DAY_CARE", objClsPolicyUWQMarineInfo.PROVIDE_HOME_DAY_CARE);

                //objDataWrapper.AddParameter("@MODULAR_MANUFACTURED_HOME", objClsPolicyUWQMarineInfo.MODULAR_MANUFACTURED_HOME);
                //objDataWrapper.AddParameter("@BUILT_ON_CONTINUOUS_FOUNDATION", objClsPolicyUWQMarineInfo.BUILT_ON_CONTINUOUS_FOUNDATION);
                //objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE", objClsPolicyUWQMarineInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE);
                //objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC", objClsPolicyUWQMarineInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC);

                //objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN", objClsPolicyUWQMarineInfo.PROPERTY_ON_MORE_THAN);
                //objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN_DESC", objClsPolicyUWQMarineInfo.PROPERTY_ON_MORE_THAN_DESC);
                //objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME", objClsPolicyUWQMarineInfo.DWELLING_MOBILE_HOME);
                //objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME_DESC", objClsPolicyUWQMarineInfo.DWELLING_MOBILE_HOME_DESC);
                //objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART", objClsPolicyUWQMarineInfo.PROPERTY_USED_WHOLE_PART);
                //objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART_DESC", objClsPolicyUWQMarineInfo.PROPERTY_USED_WHOLE_PART_DESC);
                //objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES", objClsPolicyUWQMarineInfo.ANY_PRIOR_LOSSES);
                //objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC", objClsPolicyUWQMarineInfo.ANY_PRIOR_LOSSES_DESC);
                //if (objClsPolicyUWQMarineInfo.NON_WEATHER_CLAIMS != -1)
                //    objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS", objClsPolicyUWQMarineInfo.NON_WEATHER_CLAIMS);
                //else
                //    objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS", System.DBNull.Value);
                //if (objClsPolicyUWQMarineInfo.WEATHER_CLAIMS != -1)
                //    objDataWrapper.AddParameter("@WEATHER_CLAIMS", objClsPolicyUWQMarineInfo.WEATHER_CLAIMS);
                //else
                //    objDataWrapper.AddParameter("@WEATHER_CLAIMS", System.DBNull.Value);

                if (TransactionLog)
                {
                    objClsPolicyUWQMarineInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/MariTime/PolicyUWQMarine.aspx.resx");
                    strTranXML = objBuilder.GetTransactionLogXML(objClsPolicyUWQMarineInfo);
                    ClsTransactionInfo objTransactionInfo = new ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.POLICY_ID = objClsPolicyUWQMarineInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objClsPolicyUWQMarineInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objClsPolicyUWQMarineInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY = objClsPolicyUWQMarineInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "New information is added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.ClearParameteres();

                //ClsHomeCoverages objCoverage;
                //if (objClsPolicyUWQMarineInfo.CalledFrom == "HOME")
                //{
                //    objCoverage = new ClsHomeCoverages();
                //    objCoverage.UpdateCoveragesByRuleApp(objDataWrapper, objClsPolicyUWQMarineInfo.CUSTOMER_ID, objClsPolicyUWQMarineInfo.POLICY_ID, objClsPolicyUWQMarineInfo.POLICY_VERSION_ID, RuleType.OtherAppDependent);
                //}
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
        public DataSet FetchPolicyMotorGenInfoData(int CustomerId, int PolicyId, int PolicyVersionId)
        {
            string strStoredProc = "Proc_FetchPOL_MARINE_CARGO_GEN_INFO";
            DataSet dsCount = null;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);
                dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
            return dsCount;
        }
    }
}
