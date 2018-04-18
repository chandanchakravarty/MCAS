/******************************************************************************************
<Author				: -   Anshuman
<Start Date				: -	5/18/2005 4:54:28 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
<Modified Date		: - Mohit Gupta.
<Modified By		: - 22/09/2005
<Purpose			: - Adding code in the functions for added fields in APP_HOME_OWNER_GEN_INFO(change request 742)
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
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlApplication.HomeOwners;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsHomeGeneralInformation : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		#region Private Instance Variables
		private const	string		APP_HOME_OWNER_GEN_INFO		=	"APP_HOME_OWNER_GEN_INFO";
		private const	string		INSERT_PROC					=	"Proc_InsertAPP_HOME_OWNER_GEN_INFO";
		private const	string		INSERT_POLICY_PROC			=	"Proc_InsertPOL_HOME_OWNER_GEN_INFO";
		private const	string		UPDATE_PROC					=	"Proc_UpdateAPP_HOME_OWNER_GEN_INFO";
		private const	string		UPDATE_POLICY_PROC			=	"Proc_UpdatePOL_HOME_OWNER_GEN_INFO";

		private	bool	boolTransactionLog;
		// private int		_APP_VERSION_ID;
		
		//DataSet dsCovLmits = null;
		//DataSet dsDwellingInfo = null;

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
		public static string GetGeneralInformationXml(int intCustoemrId, int intAppId, int intAppVersionId, string strCalledFrom)
		{
			string strStoredProc			= "Proc_GetAPP_HOME_OWNER_GEN_INFO";
			DataSet dsGeneralInformation	= new DataSet();
			DataWrapper objDataWrapper		= new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				
				dsGeneralInformation		= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsGeneralInformation.Tables.Count>0)
				{
					if (dsGeneralInformation.Tables[0].Rows.Count != 0)
					{
						//return dsGeneralInformation.GetXml();
						return ClsCommon.GetXMLEncoded(dsGeneralInformation.Tables[0]);
					}
					else
					{
						return "";
					}
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


		#region Check For Existing Policy Of Home Or Other Lobs

		public static DataRow  CheckExistancePolicySecHeat(int customer_id,int appId,int appVersion,string lobId)
		{
			
			string		strStoredProc	=	"Proc_CheckExistPolicyAndSecondaryHeat";
						
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMERID",customer_id);
				objDataWrapper.AddParameter("@APPID",appId);
				objDataWrapper.AddParameter("@APPVERSIONID",appVersion);
				objDataWrapper.AddParameter("@LOBID",lobId);
				DataSet da  =    objDataWrapper.ExecuteDataSet(strStoredProc);
				return da.Tables[0].Rows[0];
				
				
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
		public static string GetPolicyGeneralInformationXml(int intCustoemrId, int intPolId, int intPolVersionId)
		{
			string strStoredProc			= "Proc_GetPOL_HOME_OWNER_GEN_INFO";
			DataSet dsGeneralInformation	= new DataSet();
			DataWrapper objDataWrapper		= new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@POL_ID",intPolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",intPolVersionId);
				
				dsGeneralInformation		= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsGeneralInformation.Tables[0].Rows.Count != 0)
				{
					return dsGeneralInformation.GetXml();
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

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsHomeGeneralInformation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objGeneralInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(clsGeneralInfo objGeneralInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder	=	new SqlUpdateBuilder();
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_FARMING_BUSINESS_COND",objGeneralInfo.ANY_FARMING_BUSINESS_COND);
				// commented by mohit as field removed from the page.
				//objDataWrapper.AddParameter("@DESC_BUSINESS",objGeneralInfo.DESC_BUSINESS);
				// end

				objDataWrapper.AddParameter("@ANY_RESIDENCE_EMPLOYEE",objGeneralInfo.ANY_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@DESC_RESIDENCE_EMPLOYEE",objGeneralInfo.DESC_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@ANY_OTHER_RESI_OWNED",objGeneralInfo.ANY_OTHER_RESI_OWNED);
				objDataWrapper.AddParameter("@DESC_OTHER_RESIDENCE",objGeneralInfo.DESC_OTHER_RESIDENCE);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objGeneralInfo.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@DESC_OTHER_INSURANCE",objGeneralInfo.DESC_OTHER_INSURANCE);
				objDataWrapper.AddParameter("@HAS_INSU_TRANSFERED_AGENCY",objGeneralInfo.HAS_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@DESC_INSU_TRANSFERED_AGENCY",objGeneralInfo.DESC_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@ANY_COV_DECLINED_CANCELED",objGeneralInfo.ANY_COV_DECLINED_CANCELED);
				objDataWrapper.AddParameter("@DESC_COV_DECLINED_CANCELED",objGeneralInfo.DESC_COV_DECLINED_CANCELED);
				
				objDataWrapper.AddParameter("@ANIMALS_EXO_PETS_HISTORY",objGeneralInfo.ANIMALS_EXO_PETS_HISTORY);
				

				objDataWrapper.AddParameter("@BREED",objGeneralInfo.BREED);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objGeneralInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@CONVICTION_DEGREE_IN_PAST",objGeneralInfo.CONVICTION_DEGREE_IN_PAST);
				objDataWrapper.AddParameter("@DESC_CONVICTION_DEGREE_IN_PAST",objGeneralInfo.DESC_CONVICTION_DEGREE_IN_PAST);
				objDataWrapper.AddParameter("@ANY_RENOVATION",objGeneralInfo.ANY_RENOVATION);
				objDataWrapper.AddParameter("@DESC_RENOVATION",objGeneralInfo.DESC_RENOVATION);
				objDataWrapper.AddParameter("@TRAMPOLINE",objGeneralInfo.TRAMPOLINE);
				objDataWrapper.AddParameter("@DESC_TRAMPOLINE",objGeneralInfo.DESC_TRAMPOLINE);
				objDataWrapper.AddParameter("@LEAD_PAINT_HAZARD",objGeneralInfo.LEAD_PAINT_HAZARD);
				objDataWrapper.AddParameter("@DESC_LEAD_PAINT_HAZARD",objGeneralInfo.DESC_LEAD_PAINT_HAZARD);

				
				objDataWrapper.AddParameter("@RENTERS",objGeneralInfo.RENTERS);
				objDataWrapper.AddParameter("@DESC_RENTERS",objGeneralInfo.DESC_RENTERS);
				objDataWrapper.AddParameter("@BUILD_UNDER_CON_GEN_CONT",objGeneralInfo.BUILD_UNDER_CON_GEN_CONT);
				objDataWrapper.AddParameter("@REMARKS",objGeneralInfo.REMARKS);
                objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objGeneralInfo.MULTI_POLICY_DISC_APPLIED);     
				objDataWrapper.AddParameter("@CREATED_BY",objGeneralInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objGeneralInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@NO_OF_PETS",objGeneralInfo.NO_OF_PETS);

				//Gaurav : new field added itrack issues change impact doc 756/742
				 
				objDataWrapper.AddParameter("@IS_SWIMPOLL_HOTTUB",objGeneralInfo.IS_SWIMPOLL_HOTTUB);
				if(objGeneralInfo.LAST_INSPECTED_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",objGeneralInfo.LAST_INSPECTED_DATE );
				else
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",null );
				
				//----------------------------------Added by mohit--------------.
					
				objDataWrapper.AddParameter("@IS_RENTED_IN_PART",objGeneralInfo.IS_RENTED_IN_PART);
				objDataWrapper.AddParameter("@IS_VACENT_OCCUPY",objGeneralInfo.IS_VACENT_OCCUPY);
				objDataWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objGeneralInfo.IS_DWELLING_OWNED_BY_OTHER);
				objDataWrapper.AddParameter("@IS_PROP_NEXT_COMMERICAL",objGeneralInfo.IS_PROP_NEXT_COMMERICAL);
				objDataWrapper.AddParameter("@DESC_PROPERTY",objGeneralInfo.DESC_PROPERTY);
				objDataWrapper.AddParameter("@ARE_STAIRWAYS_PRESENT",objGeneralInfo.ARE_STAIRWAYS_PRESENT);
				objDataWrapper.AddParameter("@DESC_STAIRWAYS",objGeneralInfo.DESC_STAIRWAYS);
				objDataWrapper.AddParameter("@IS_OWNERS_DWELLING_CHANGED",objGeneralInfo.IS_OWNERS_DWELLING_CHANGED);
				objDataWrapper.AddParameter("@DESC_OWNER",objGeneralInfo.DESC_OWNER);
				
				
				
				objDataWrapper.AddParameter("@DESC_VACENT_OCCUPY",objGeneralInfo.DESC_VACENT_OCCUPY);
				objDataWrapper.AddParameter("@DESC_RENTED_IN_PART",objGeneralInfo.DESC_RENTED_IN_PART);
				objDataWrapper.AddParameter("@DESC_DWELLING_OWNED_BY_OTHER",objGeneralInfo.DESC_DWELLING_OWNED_BY_OTHER);
				
				objDataWrapper.AddParameter("@ANY_HEATING_SOURCE",objGeneralInfo.ANY_HEATING_SOURCE);
				objDataWrapper.AddParameter("@DESC_ANY_HEATING_SOURCE",objGeneralInfo.DESC_ANY_HEATING_SOURCE);
				
				objDataWrapper.AddParameter("@NON_SMOKER_CREDIT",objGeneralInfo.NON_SMOKER_CREDIT);
				//-----------------------------------End-------------------------.
				
				// Added by Mohit on 4/11/2005
				objDataWrapper.AddParameter("@SWIMMING_POOL",objGeneralInfo.SWIMMING_POOL);
				objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objGeneralInfo.SWIMMING_POOL_TYPE);
				// End
				//Added By Shafi On 12/21/2005
				objDataWrapper.AddParameter("@Any_Forming",objGeneralInfo.Any_Forming);
				objDataWrapper.AddParameter("@Premises",DefaultValues.GetIntNull(objGeneralInfo.Premises));
				objDataWrapper.AddParameter("@Of_Acres",DefaultValues.GetDoubleNull(objGeneralInfo.Of_Acres));
				objDataWrapper.AddParameter("@IsAny_Horse",DefaultValues.GetStringNull(objGeneralInfo.IsAny_Horse));
				objDataWrapper.AddParameter("@Of_Acres_P",DefaultValues.GetDoubleNull(objGeneralInfo.Of_Acres_P));
				objDataWrapper.AddParameter("@No_Horses",DefaultValues.GetIntNull(objGeneralInfo.No_Horses));
				objDataWrapper.AddParameter("@DESC_FARMING_BUSINESS_COND",DefaultValues.GetStringNull(objGeneralInfo.DESC_FARMING_BUSINESS_COND));
				objDataWrapper.AddParameter("@location",DefaultValues.GetStringNull(objGeneralInfo.Location));
				objDataWrapper.AddParameter("@DESC_location",DefaultValues.GetStringNull(objGeneralInfo.DESC_Location));
				objDataWrapper.AddParameter("@DOG_SURCHARGE",objGeneralInfo.DOG_SURCHARGE);

				//added By Shafi 27-03-2006
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objGeneralInfo.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objGeneralInfo.YEARS_INSU));
				//objDataWrapper.AddParameter("",objGeneralInfo.an
				//Added by Sumit for new description fields on 30-03-2006
				objDataWrapper.AddParameter("@DESC_IS_SWIMPOLL_HOTTUB",objGeneralInfo.DESC_IS_SWIMPOLL_HOTTUB);
				objDataWrapper.AddParameter("@DESC_MULTI_POLICY_DISC_APPLIED",objGeneralInfo.DESC_MULTI_POLICY_DISC_APPLIED);
				objDataWrapper.AddParameter("@DESC_BUILD_UNDER_CON_GEN_CONT",objGeneralInfo.DESC_BUILD_UNDER_CON_GEN_CONT);
				objDataWrapper.AddParameter("@DIVING_BOARD",objGeneralInfo.DIVING_BOARD);
				objDataWrapper.AddParameter("@APPROVED_FENCE",objGeneralInfo.APPROVED_FENCE);
				objDataWrapper.AddParameter("@SLIDE",objGeneralInfo.SLIDE);

				objDataWrapper.AddParameter("@PROVIDE_HOME_DAY_CARE",objGeneralInfo.PROVIDE_HOME_DAY_CARE);
				objDataWrapper.AddParameter("@MODULAR_MANUFACTURED_HOME",objGeneralInfo.MODULAR_MANUFACTURED_HOME);
				objDataWrapper.AddParameter("@BUILT_ON_CONTINUOUS_FOUNDATION",objGeneralInfo.BUILT_ON_CONTINUOUS_FOUNDATION);

				objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE",objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE);
				objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC",objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC);


				objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN",objGeneralInfo.PROPERTY_ON_MORE_THAN);
				objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN_DESC",objGeneralInfo.PROPERTY_ON_MORE_THAN_DESC);
				objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME",objGeneralInfo.DWELLING_MOBILE_HOME);
				objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME_DESC",objGeneralInfo.DWELLING_MOBILE_HOME_DESC);
				objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART",objGeneralInfo.PROPERTY_USED_WHOLE_PART);
				objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART_DESC",objGeneralInfo.PROPERTY_USED_WHOLE_PART_DESC);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objGeneralInfo.ANY_PRIOR_LOSSES);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objGeneralInfo.ANY_PRIOR_LOSSES_DESC);
				objDataWrapper.AddParameter("@BOAT_WITH_HOMEOWNER",objGeneralInfo.BOAT_WITH_HOMEOWNER);
				//Added for Itrack Issue 6640 on 10 Dec 09
				if(objGeneralInfo.NON_WEATHER_CLAIMS != -1)
					objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS",objGeneralInfo.NON_WEATHER_CLAIMS);
				else
					objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS",System.DBNull.Value);
				if(objGeneralInfo.WEATHER_CLAIMS != -1)
					objDataWrapper.AddParameter("@WEATHER_CLAIMS",objGeneralInfo.WEATHER_CLAIMS);
				else
					objDataWrapper.AddParameter("@WEATHER_CLAIMS",System.DBNull.Value);


				int returnResult = 0;
				if(TransactionLog)
				{
					objGeneralInfo.TransactLabel		=	ClsCommon.MapTransactionLabel("/application/Aspx/HomeOwners/AddGeneralInformationIFrame.aspx.resx");
					string	strTranXML					=	objBuilder.GetTransactionLogXML(objGeneralInfo);
					ClsTransactionInfo objTransactionInfo = new ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objGeneralInfo.CREATED_BY;
					//objTransactionInfo.TRANS_DESC		=	"New Homeowner's general information is added";
					objTransactionInfo.TRANS_DESC		=	"Underwriting Questions is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_PROC,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_PROC);
				}
				objDataWrapper.ClearParameteres();

				//Update coverages///////////////
				ClsHomeCoverages objCoverage;
				if(objGeneralInfo.CalledFrom == "HOME")
				{
					objCoverage=new ClsHomeCoverages();
					objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objGeneralInfo.CUSTOMER_ID,objGeneralInfo.APP_ID,objGeneralInfo.APP_VERSION_ID,RuleType.OtherAppDependent);
				}
				//this.UpdateApplicationCoverages(objGeneralInfo.CUSTOMER_ID,objGeneralInfo.APP_ID,objGeneralInfo.APP_VERSION_ID,objDataWrapper);
				////////////////////////////////
				
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

		#region POLICY FUNCTIONS
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objGeneralInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPolicyHomeownerGeneralInfo objGeneralInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder	=	new SqlUpdateBuilder();
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objGeneralInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objGeneralInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_FARMING_BUSINESS_COND",objGeneralInfo.ANY_FARMING_BUSINESS_COND);
				objDataWrapper.AddParameter("@ANY_RESIDENCE_EMPLOYEE",objGeneralInfo.ANY_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@DESC_RESIDENCE_EMPLOYEE",objGeneralInfo.DESC_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@ANY_OTHER_RESI_OWNED",objGeneralInfo.ANY_OTHER_RESI_OWNED);
				objDataWrapper.AddParameter("@DESC_OTHER_RESIDENCE",objGeneralInfo.DESC_OTHER_RESIDENCE);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objGeneralInfo.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@DESC_OTHER_INSURANCE",objGeneralInfo.DESC_OTHER_INSURANCE);
				objDataWrapper.AddParameter("@HAS_INSU_TRANSFERED_AGENCY",objGeneralInfo.HAS_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@DESC_INSU_TRANSFERED_AGENCY",objGeneralInfo.DESC_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@ANY_COV_DECLINED_CANCELED",objGeneralInfo.ANY_COV_DECLINED_CANCELED);
				objDataWrapper.AddParameter("@DESC_COV_DECLINED_CANCELED",objGeneralInfo.DESC_COV_DECLINED_CANCELED);
			
			    objDataWrapper.AddParameter("@ANIMALS_EXO_PETS_HISTORY",objGeneralInfo.ANIMALS_EXO_PETS_HISTORY);
				objDataWrapper.AddParameter("@BREED",objGeneralInfo.BREED);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objGeneralInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@CONVICTION_DEGREE_IN_PAST",objGeneralInfo.CONVICTION_DEGREE_IN_PAST);
				objDataWrapper.AddParameter("@DESC_CONVICTION_DEGREE_IN_PAST",objGeneralInfo.DESC_CONVICTION_DEGREE_IN_PAST);
				objDataWrapper.AddParameter("@ANY_RENOVATION",objGeneralInfo.ANY_RENOVATION);
				objDataWrapper.AddParameter("@DESC_RENOVATION",objGeneralInfo.DESC_RENOVATION);
				objDataWrapper.AddParameter("@TRAMPOLINE",objGeneralInfo.TRAMPOLINE);
				objDataWrapper.AddParameter("@DESC_TRAMPOLINE",objGeneralInfo.DESC_TRAMPOLINE);
				objDataWrapper.AddParameter("@LEAD_PAINT_HAZARD",objGeneralInfo.LEAD_PAINT_HAZARD);
				objDataWrapper.AddParameter("@DESC_LEAD_PAINT_HAZARD",objGeneralInfo.DESC_LEAD_PAINT_HAZARD);
				objDataWrapper.AddParameter("@RENTERS",objGeneralInfo.RENTERS);
				objDataWrapper.AddParameter("@DESC_RENTERS",objGeneralInfo.DESC_RENTERS);
				objDataWrapper.AddParameter("@BUILD_UNDER_CON_GEN_CONT",objGeneralInfo.BUILD_UNDER_CON_GEN_CONT);
				objDataWrapper.AddParameter("@REMARKS",objGeneralInfo.REMARKS);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objGeneralInfo.MULTI_POLICY_DISC_APPLIED);     
				objDataWrapper.AddParameter("@CREATED_BY",objGeneralInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objGeneralInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@NO_OF_PETS",objGeneralInfo.NO_OF_PETS);
				objDataWrapper.AddParameter("@IS_SWIMPOLL_HOTTUB",objGeneralInfo.IS_SWIMPOLL_HOTTUB);
				if(objGeneralInfo.LAST_INSPECTED_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",objGeneralInfo.LAST_INSPECTED_DATE );
				else
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",null );
				objDataWrapper.AddParameter("@IS_RENTED_IN_PART",objGeneralInfo.IS_RENTED_IN_PART);
				objDataWrapper.AddParameter("@IS_VACENT_OCCUPY",objGeneralInfo.IS_VACENT_OCCUPY);
				objDataWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objGeneralInfo.IS_DWELLING_OWNED_BY_OTHER);
				objDataWrapper.AddParameter("@IS_PROP_NEXT_COMMERICAL",objGeneralInfo.IS_PROP_NEXT_COMMERICAL);
				objDataWrapper.AddParameter("@DESC_PROPERTY",objGeneralInfo.DESC_PROPERTY);
				objDataWrapper.AddParameter("@ARE_STAIRWAYS_PRESENT",objGeneralInfo.ARE_STAIRWAYS_PRESENT);
				objDataWrapper.AddParameter("@DESC_STAIRWAYS",objGeneralInfo.DESC_STAIRWAYS);
				objDataWrapper.AddParameter("@IS_OWNERS_DWELLING_CHANGED",objGeneralInfo.IS_OWNERS_DWELLING_CHANGED);
				objDataWrapper.AddParameter("@DESC_OWNER",objGeneralInfo.DESC_OWNER);
				objDataWrapper.AddParameter("@DESC_VACENT_OCCUPY",objGeneralInfo.DESC_VACENT_OCCUPY);
				objDataWrapper.AddParameter("@DESC_RENTED_IN_PART",objGeneralInfo.DESC_RENTED_IN_PART);
				objDataWrapper.AddParameter("@DESC_DWELLING_OWNED_BY_OTHER",objGeneralInfo.DESC_DWELLING_OWNED_BY_OTHER);
				objDataWrapper.AddParameter("@ANY_HEATING_SOURCE",objGeneralInfo.ANY_HEATING_SOURCE);
				objDataWrapper.AddParameter("@DESC_ANY_HEATING_SOURCE",objGeneralInfo.DESC_ANY_HEATING_SOURCE);
				objDataWrapper.AddParameter("@NON_SMOKER_CREDIT",objGeneralInfo.NON_SMOKER_CREDIT);
				objDataWrapper.AddParameter("@SWIMMING_POOL",objGeneralInfo.SWIMMING_POOL);
				objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objGeneralInfo.SWIMMING_POOL_TYPE);
				//Added by Sumit on 07-02-06
				objDataWrapper.AddParameter("@Any_Forming",objGeneralInfo.Any_Forming);
				objDataWrapper.AddParameter("@Premises",DefaultValues.GetIntNull(objGeneralInfo.Premises));
				objDataWrapper.AddParameter("@Of_Acres",DefaultValues.GetDoubleNull(objGeneralInfo.Of_Acres));
				objDataWrapper.AddParameter("@IsAny_Horse",DefaultValues.GetStringNull(objGeneralInfo.IsAny_Horse));
				objDataWrapper.AddParameter("@Of_Acres_P",DefaultValues.GetDoubleNull(objGeneralInfo.Of_Acres_P));
				objDataWrapper.AddParameter("@No_Horses",DefaultValues.GetIntNull(objGeneralInfo.No_Horses));
				//objDataWrapper.AddParameter("@DESC_FARMING_BUSINESS_COND",DefaultValues.GetStringNull(objGeneralInfo.DESC_FARMING_BUSINESS_COND));
				objDataWrapper.AddParameter("@location",DefaultValues.GetStringNull(objGeneralInfo.Location));
				objDataWrapper.AddParameter("@DESC_location",DefaultValues.GetStringNull(objGeneralInfo.DESC_Location));				
				//Added by Sumit for new description fields on 30-03-2006
				objDataWrapper.AddParameter("@DESC_IS_SWIMPOLL_HOTTUB",objGeneralInfo.DESC_IS_SWIMPOLL_HOTTUB);
				objDataWrapper.AddParameter("@DESC_MULTI_POLICY_DISC_APPLIED",objGeneralInfo.DESC_MULTI_POLICY_DISC_APPLIED);
				objDataWrapper.AddParameter("@DESC_BUILD_UNDER_CON_GEN_CONT",objGeneralInfo.DESC_BUILD_UNDER_CON_GEN_CONT);
				objDataWrapper.AddParameter("@DIVING_BOARD",objGeneralInfo.DIVING_BOARD);
				objDataWrapper.AddParameter("@APPROVED_FENCE",objGeneralInfo.APPROVED_FENCE);
				objDataWrapper.AddParameter("@SLIDE",objGeneralInfo.SLIDE);
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objGeneralInfo.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objGeneralInfo.YEARS_INSU));
				objDataWrapper.AddParameter("@DESC_FARMING_BUSINESS_COND",DefaultValues.GetStringNull(objGeneralInfo.DESC_FARMING_BUSINESS_COND));
				objDataWrapper.AddParameter("@PROVIDE_HOME_DAY_CARE",objGeneralInfo.PROVIDE_HOME_DAY_CARE);

				//RPSINGH	
				objDataWrapper.AddParameter("@MODULAR_MANUFACTURED_HOME",objGeneralInfo.MODULAR_MANUFACTURED_HOME);
				objDataWrapper.AddParameter("@BUILT_ON_CONTINUOUS_FOUNDATION",objGeneralInfo.BUILT_ON_CONTINUOUS_FOUNDATION);
				objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE",objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE);
				objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC",objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC);

				objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN",objGeneralInfo.PROPERTY_ON_MORE_THAN);
				objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN_DESC",objGeneralInfo.PROPERTY_ON_MORE_THAN_DESC);
				objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME",objGeneralInfo.DWELLING_MOBILE_HOME);
				objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME_DESC",objGeneralInfo.DWELLING_MOBILE_HOME_DESC);
				objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART",objGeneralInfo.PROPERTY_USED_WHOLE_PART);
				objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART_DESC",objGeneralInfo.PROPERTY_USED_WHOLE_PART_DESC);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objGeneralInfo.ANY_PRIOR_LOSSES);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objGeneralInfo.ANY_PRIOR_LOSSES_DESC);
				//Added for Itrack Issue 6640 on 11 Dec 09
				if(objGeneralInfo.NON_WEATHER_CLAIMS != -1)
					objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS",objGeneralInfo.NON_WEATHER_CLAIMS);
				else
					objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS",System.DBNull.Value);
				if(objGeneralInfo.WEATHER_CLAIMS != -1)
					objDataWrapper.AddParameter("@WEATHER_CLAIMS",objGeneralInfo.WEATHER_CLAIMS);
				else
					objDataWrapper.AddParameter("@WEATHER_CLAIMS",System.DBNull.Value);



				int returnResult = 0;
				
				if(TransactionLog)
				{
					objGeneralInfo.TransactLabel		=	ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyHomeownerGeneralInfo.aspx.resx");
					string	strTranXML					=	objBuilder.GetTransactionLogXML(objGeneralInfo);
					ClsTransactionInfo objTransactionInfo = new ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID  = objGeneralInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objGeneralInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objGeneralInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Homeowner's general information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_POLICY_PROC,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_POLICY_PROC);
				}
				objDataWrapper.ClearParameteres();

				//Update coverages/endorsements  for all dwellings///////////////
				ClsHomeCoverages objCoverage;
				if(objGeneralInfo.CalledFrom == "HOME")
				{
					objCoverage=new ClsHomeCoverages();
					objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objGeneralInfo.CUSTOMER_ID,objGeneralInfo.POLICY_ID ,objGeneralInfo.POLICY_VERSION_ID,RuleType.OtherAppDependent);
				}
				//this.UpdatePolicyCoverages(objGeneralInfo.CUSTOMER_ID,objGeneralInfo.POLICY_ID,objGeneralInfo.POLICY_VERSION_ID,objDataWrapper);
				////
				
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
		/// <param name="objOldGeneralInfo">Model object having old information</param>
		/// <param name="objGeneralInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPolicyHomeownerGeneralInfo objOldGeneralInfo,ClsPolicyHomeownerGeneralInfo objGeneralInfo)
		{
			string strTranXML;
			int returnResult			= 0;
			DataWrapper objDataWrapper	= new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objGeneralInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POL_VERSION_ID",objGeneralInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@ANY_FARMING_BUSINESS_COND",objGeneralInfo.ANY_FARMING_BUSINESS_COND);
				objDataWrapper.AddParameter("@ANY_RESIDENCE_EMPLOYEE",objGeneralInfo.ANY_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@DESC_RESIDENCE_EMPLOYEE",objGeneralInfo.DESC_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@ANY_OTHER_RESI_OWNED",objGeneralInfo.ANY_OTHER_RESI_OWNED);
				objDataWrapper.AddParameter("@DESC_OTHER_RESIDENCE",objGeneralInfo.DESC_OTHER_RESIDENCE);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objGeneralInfo.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@DESC_OTHER_INSURANCE",objGeneralInfo.DESC_OTHER_INSURANCE);
				objDataWrapper.AddParameter("@HAS_INSU_TRANSFERED_AGENCY",objGeneralInfo.HAS_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@DESC_INSU_TRANSFERED_AGENCY",objGeneralInfo.DESC_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@ANY_COV_DECLINED_CANCELED",objGeneralInfo.ANY_COV_DECLINED_CANCELED);
				objDataWrapper.AddParameter("@DESC_COV_DECLINED_CANCELED",objGeneralInfo.DESC_COV_DECLINED_CANCELED);
				
				objDataWrapper.AddParameter("@ANIMALS_EXO_PETS_HISTORY",objGeneralInfo.ANIMALS_EXO_PETS_HISTORY);
				objDataWrapper.AddParameter("@BREED",objGeneralInfo.BREED);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objGeneralInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@ANY_RENOVATION",objGeneralInfo.ANY_RENOVATION);
				objDataWrapper.AddParameter("@DESC_RENOVATION",objGeneralInfo.DESC_RENOVATION);
				objDataWrapper.AddParameter("@CONVICTION_DEGREE_IN_PAST",objGeneralInfo.CONVICTION_DEGREE_IN_PAST);
				objDataWrapper.AddParameter("@DESC_CONVICTION_DEGREE_IN_PAST",objGeneralInfo.DESC_CONVICTION_DEGREE_IN_PAST);
				objDataWrapper.AddParameter("@TRAMPOLINE",objGeneralInfo.TRAMPOLINE);
				objDataWrapper.AddParameter("@DESC_TRAMPOLINE",objGeneralInfo.DESC_TRAMPOLINE);
				objDataWrapper.AddParameter("@LEAD_PAINT_HAZARD",objGeneralInfo.LEAD_PAINT_HAZARD);
				objDataWrapper.AddParameter("@DESC_LEAD_PAINT_HAZARD",objGeneralInfo.DESC_LEAD_PAINT_HAZARD);
				objDataWrapper.AddParameter("@RENTERS",objGeneralInfo.RENTERS);
				objDataWrapper.AddParameter("@DESC_RENTERS",objGeneralInfo.DESC_RENTERS);
				objDataWrapper.AddParameter("@BUILD_UNDER_CON_GEN_CONT",objGeneralInfo.BUILD_UNDER_CON_GEN_CONT);
				objDataWrapper.AddParameter("@REMARKS",objGeneralInfo.REMARKS);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objGeneralInfo.MULTI_POLICY_DISC_APPLIED);         
				objDataWrapper.AddParameter("@IS_ACTIVE",objGeneralInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@NO_OF_PETS",objGeneralInfo.NO_OF_PETS);
				objDataWrapper.AddParameter("@IS_SWIMPOLL_HOTTUB",objGeneralInfo.IS_SWIMPOLL_HOTTUB);
				if(objGeneralInfo.LAST_INSPECTED_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",objGeneralInfo.LAST_INSPECTED_DATE );
				else
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",null );
				objDataWrapper.AddParameter("@IS_RENTED_IN_PART",objGeneralInfo.IS_RENTED_IN_PART);
				objDataWrapper.AddParameter("@IS_VACENT_OCCUPY",objGeneralInfo.IS_VACENT_OCCUPY);
				objDataWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objGeneralInfo.IS_DWELLING_OWNED_BY_OTHER);
				objDataWrapper.AddParameter("@IS_PROP_NEXT_COMMERICAL",objGeneralInfo.IS_PROP_NEXT_COMMERICAL);
				objDataWrapper.AddParameter("@DESC_PROPERTY",objGeneralInfo.DESC_PROPERTY);
				objDataWrapper.AddParameter("@ARE_STAIRWAYS_PRESENT",objGeneralInfo.ARE_STAIRWAYS_PRESENT);
				objDataWrapper.AddParameter("@DESC_STAIRWAYS",objGeneralInfo.DESC_STAIRWAYS);
				objDataWrapper.AddParameter("@IS_OWNERS_DWELLING_CHANGED",objGeneralInfo.IS_OWNERS_DWELLING_CHANGED);
				objDataWrapper.AddParameter("@DESC_OWNER",objGeneralInfo.DESC_OWNER);
				objDataWrapper.AddParameter("@DESC_VACENT_OCCUPY",objGeneralInfo.DESC_VACENT_OCCUPY);
				objDataWrapper.AddParameter("@DESC_RENTED_IN_PART",objGeneralInfo.DESC_RENTED_IN_PART);
				objDataWrapper.AddParameter("@DESC_DWELLING_OWNED_BY_OTHER",objGeneralInfo.DESC_DWELLING_OWNED_BY_OTHER);
				objDataWrapper.AddParameter("@ANY_HEATING_SOURCE",objGeneralInfo.ANY_HEATING_SOURCE);
				objDataWrapper.AddParameter("@DESC_ANY_HEATING_SOURCE",objGeneralInfo.DESC_ANY_HEATING_SOURCE);
				objDataWrapper.AddParameter("@NON_SMOKER_CREDIT",objGeneralInfo.NON_SMOKER_CREDIT); // Added on 19/10/2005
				objDataWrapper.AddParameter("@SWIMMING_POOL",objGeneralInfo.SWIMMING_POOL);
				objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objGeneralInfo.SWIMMING_POOL_TYPE);
				//Added by Sumit on 07-02-06
				objDataWrapper.AddParameter("@Any_Forming",objGeneralInfo.Any_Forming);
				objDataWrapper.AddParameter("@Premises",DefaultValues.GetIntNull(objGeneralInfo.Premises));
				objDataWrapper.AddParameter("@Of_Acres",DefaultValues.GetDoubleNull(objGeneralInfo.Of_Acres));
				objDataWrapper.AddParameter("@IsAny_Horse",DefaultValues.GetStringNull(objGeneralInfo.IsAny_Horse));
				objDataWrapper.AddParameter("@Of_Acres_P",DefaultValues.GetDoubleNull(objGeneralInfo.Of_Acres_P));
				objDataWrapper.AddParameter("@No_Horses",DefaultValues.GetIntNull(objGeneralInfo.No_Horses));				
				objDataWrapper.AddParameter("@location",DefaultValues.GetStringNull(objGeneralInfo.Location));
				objDataWrapper.AddParameter("@DESC_location",DefaultValues.GetStringNull(objGeneralInfo.DESC_Location));
				//Added by Sumit for new description fields on 30-03-2006
				objDataWrapper.AddParameter("@DESC_IS_SWIMPOLL_HOTTUB",objGeneralInfo.DESC_IS_SWIMPOLL_HOTTUB);
				objDataWrapper.AddParameter("@DESC_MULTI_POLICY_DISC_APPLIED",objGeneralInfo.DESC_MULTI_POLICY_DISC_APPLIED);
				objDataWrapper.AddParameter("@DESC_BUILD_UNDER_CON_GEN_CONT",objGeneralInfo.DESC_BUILD_UNDER_CON_GEN_CONT);
				objDataWrapper.AddParameter("@DIVING_BOARD",objGeneralInfo.DIVING_BOARD);
				objDataWrapper.AddParameter("@APPROVED_FENCE",objGeneralInfo.APPROVED_FENCE);
				objDataWrapper.AddParameter("@SLIDE",objGeneralInfo.SLIDE);
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objGeneralInfo.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objGeneralInfo.YEARS_INSU));
				objDataWrapper.AddParameter("@DESC_FARMING_BUSINESS_COND",DefaultValues.GetStringNull(objGeneralInfo.DESC_FARMING_BUSINESS_COND));
				
				objDataWrapper.AddParameter("@PROVIDE_HOME_DAY_CARE",objGeneralInfo.PROVIDE_HOME_DAY_CARE);

				//RPSINGH
				objDataWrapper.AddParameter("@MODULAR_MANUFACTURED_HOME",objGeneralInfo.MODULAR_MANUFACTURED_HOME);
				objDataWrapper.AddParameter("@BUILT_ON_CONTINUOUS_FOUNDATION",objGeneralInfo.BUILT_ON_CONTINUOUS_FOUNDATION);
				objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE",objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE);
				objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC",objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC);

				objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN",objGeneralInfo.PROPERTY_ON_MORE_THAN);
				objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN_DESC",objGeneralInfo.PROPERTY_ON_MORE_THAN_DESC);
				objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME",objGeneralInfo.DWELLING_MOBILE_HOME);
				objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME_DESC",objGeneralInfo.DWELLING_MOBILE_HOME_DESC);
				objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART",objGeneralInfo.PROPERTY_USED_WHOLE_PART);
				objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART_DESC",objGeneralInfo.PROPERTY_USED_WHOLE_PART_DESC);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objGeneralInfo.ANY_PRIOR_LOSSES);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objGeneralInfo.ANY_PRIOR_LOSSES_DESC);
				objDataWrapper.AddParameter("@BOAT_WITH_HOMEOWNER",objGeneralInfo.BOAT_WITH_HOMEOWNER);
				//Added for Itrack Issue 6640 on 11 Dec 09
				if(objGeneralInfo.NON_WEATHER_CLAIMS != -1)
					objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS",objGeneralInfo.NON_WEATHER_CLAIMS);
				else
					objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS",System.DBNull.Value);
				if(objGeneralInfo.WEATHER_CLAIMS != -1)
					objDataWrapper.AddParameter("@WEATHER_CLAIMS",objGeneralInfo.WEATHER_CLAIMS);
				else
					objDataWrapper.AddParameter("@WEATHER_CLAIMS",System.DBNull.Value);
				
				if(TransactionLog) 
				{
					objGeneralInfo.TransactLabel		=	ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyHomeownerGeneralInfo.aspx.resx");
					strTranXML							=	objBuilder.GetTransactionLogXML(objOldGeneralInfo,objGeneralInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(UPDATE_POLICY_PROC);
					else
					{
						ClsTransactionInfo objTransactionInfo = new ClsTransactionInfo();
					
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.POLICY_ID = objGeneralInfo.POLICY_ID ;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = objGeneralInfo.POLICY_VERSION_ID ;
						objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objGeneralInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Homeowner's general information is modified";					
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						returnResult = objDataWrapper.ExecuteNonQuery(UPDATE_POLICY_PROC,objTransactionInfo);
					}
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(UPDATE_POLICY_PROC);
				}

				
				//Update coverages/endorsements  for all dwellings///////////////
				ClsHomeCoverages objCoverage;
				if(objGeneralInfo.CalledFrom == "HOME")
				{
					objCoverage=new ClsHomeCoverages();
					objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objGeneralInfo.CUSTOMER_ID,objGeneralInfo.POLICY_ID ,objGeneralInfo.POLICY_VERSION_ID,RuleType.OtherAppDependent);
				}
				//this.UpdatePolicyCoverages(objGeneralInfo.CUSTOMER_ID,objGeneralInfo.POLICY_ID,objGeneralInfo.POLICY_VERSION_ID,objDataWrapper);
				////
				
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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldGeneralInfo">Model object having old information</param>
		/// <param name="objGeneralInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(clsGeneralInfo objOldGeneralInfo,clsGeneralInfo objGeneralInfo)
		{
			string strTranXML;
			int returnResult			= 0;
			DataWrapper objDataWrapper	= new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_FARMING_BUSINESS_COND",objGeneralInfo.ANY_FARMING_BUSINESS_COND);
				
				// Commented by mohit on 4/11/2005 as field removed from the page.
				//objDataWrapper.AddParameter("@DESC_BUSINESS",objGeneralInfo.DESC_BUSINESS);
				
				// end

				objDataWrapper.AddParameter("@ANY_RESIDENCE_EMPLOYEE",objGeneralInfo.ANY_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@DESC_RESIDENCE_EMPLOYEE",objGeneralInfo.DESC_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@ANY_OTHER_RESI_OWNED",objGeneralInfo.ANY_OTHER_RESI_OWNED);
				objDataWrapper.AddParameter("@DESC_OTHER_RESIDENCE",objGeneralInfo.DESC_OTHER_RESIDENCE);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objGeneralInfo.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@DESC_OTHER_INSURANCE",objGeneralInfo.DESC_OTHER_INSURANCE);
				objDataWrapper.AddParameter("@HAS_INSU_TRANSFERED_AGENCY",objGeneralInfo.HAS_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@DESC_INSU_TRANSFERED_AGENCY",objGeneralInfo.DESC_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@ANY_COV_DECLINED_CANCELED",objGeneralInfo.ANY_COV_DECLINED_CANCELED);
				objDataWrapper.AddParameter("@DESC_COV_DECLINED_CANCELED",objGeneralInfo.DESC_COV_DECLINED_CANCELED);
				
				objDataWrapper.AddParameter("@ANIMALS_EXO_PETS_HISTORY",objGeneralInfo.ANIMALS_EXO_PETS_HISTORY);
				
				objDataWrapper.AddParameter("@BREED",objGeneralInfo.BREED);
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objGeneralInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@ANY_RENOVATION",objGeneralInfo.ANY_RENOVATION);
				objDataWrapper.AddParameter("@DESC_RENOVATION",objGeneralInfo.DESC_RENOVATION);
				objDataWrapper.AddParameter("@CONVICTION_DEGREE_IN_PAST",objGeneralInfo.CONVICTION_DEGREE_IN_PAST);
				objDataWrapper.AddParameter("@DESC_CONVICTION_DEGREE_IN_PAST",objGeneralInfo.DESC_CONVICTION_DEGREE_IN_PAST);



				//Gaurav : Following fields has been dropped itrack issue no: 725

//				objDataWrapper.AddParameter("@MORE_THEN_FIVE_ACRES",objGeneralInfo.MORE_THEN_FIVE_ACRES);
//				objDataWrapper.AddParameter("@DESC_MORE_THEN_FIVE_ACRES",objGeneralInfo.DESC_MORE_THEN_FIVE_ACRES);
//				objDataWrapper.AddParameter("@RETROFITTED_FOR_EARTHQUAKE",objGeneralInfo.RETROFITTED_FOR_EARTHQUAKE);
//				objDataWrapper.AddParameter("@DESC_RETRO_FOR_EARTHQUAKE",objGeneralInfo.DESC_RETRO_FOR_EARTHQUAKE);
				
//				objDataWrapper.AddParameter("@MANAGER_ON_PERMISES",objGeneralInfo.MANAGER_ON_PERMISES);
//				objDataWrapper.AddParameter("@DESC_MANAGER_ON_PERMISES",objGeneralInfo.DESC_MANAGER_ON_PERMISES);
//				objDataWrapper.AddParameter("@SECURITY_ATTENDENT",objGeneralInfo.SECURITY_ATTENDENT);
//				objDataWrapper.AddParameter("@DESC_SECURITY_ATTENDENT",objGeneralInfo.DESC_SECURITY_ATTENDENT);
//				objDataWrapper.AddParameter("@BUILDING_ENT_LOCKED",objGeneralInfo.BUILDING_ENT_LOCKED);
//				objDataWrapper.AddParameter("@DESC_BUILDING_ENT_LOCKED",objGeneralInfo.DESC_BUILDING_ENT_LOCKED);
//				objDataWrapper.AddParameter("@ANY_UNCORRECT_FIRE_CODE_VIOL",objGeneralInfo.ANY_UNCORRECT_FIRE_CODE_VIOL);
//				objDataWrapper.AddParameter("@DESC_UNCORRECT_FIRE_CODE_VIOL",objGeneralInfo.DESC_UNCORRECT_FIRE_CODE_VIOL);
				
//				objDataWrapper.AddParameter("@HOUSE_FOR_SALE",objGeneralInfo.HOUSE_FOR_SALE);
//				objDataWrapper.AddParameter("@DESC_HOUSE_FOR_SALE",objGeneralInfo.DESC_HOUSE_FOR_SALE);
//				objDataWrapper.AddParameter("@ANY_NON_RESI_PROPERTY",objGeneralInfo.ANY_NON_RESI_PROPERTY);
//				objDataWrapper.AddParameter("@DESC_NON_RESI_PROPERTY",objGeneralInfo.DESC_NON_RESI_PROPERTY);
				
//				objDataWrapper.AddParameter("@STRUCT_ORI_BUILT_FOR",objGeneralInfo.STRUCT_ORI_BUILT_FOR);
//				objDataWrapper.AddParameter("@DESC_STRUCT_ORI_BUILT_FOR",objGeneralInfo.DESC_STRUCT_ORI_BUILT_FOR);
				
//				objDataWrapper.AddParameter("@FUEL_OIL_TANK_PERMISES",objGeneralInfo.FUEL_OIL_TANK_PERMISES);
//				objDataWrapper.AddParameter("@DESC_FUEL_OIL_TANK_PERMISES",objGeneralInfo.DESC_FUEL_OIL_TANK_PERMISES);
				objDataWrapper.AddParameter("@TRAMPOLINE",objGeneralInfo.TRAMPOLINE);
				objDataWrapper.AddParameter("@DESC_TRAMPOLINE",objGeneralInfo.DESC_TRAMPOLINE);
				objDataWrapper.AddParameter("@LEAD_PAINT_HAZARD",objGeneralInfo.LEAD_PAINT_HAZARD);
				objDataWrapper.AddParameter("@DESC_LEAD_PAINT_HAZARD",objGeneralInfo.DESC_LEAD_PAINT_HAZARD);
				objDataWrapper.AddParameter("@RENTERS",objGeneralInfo.RENTERS);
				objDataWrapper.AddParameter("@DESC_RENTERS",objGeneralInfo.DESC_RENTERS);
				objDataWrapper.AddParameter("@BUILD_UNDER_CON_GEN_CONT",objGeneralInfo.BUILD_UNDER_CON_GEN_CONT);
				objDataWrapper.AddParameter("@REMARKS",objGeneralInfo.REMARKS);
                objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objGeneralInfo.MULTI_POLICY_DISC_APPLIED);         
				objDataWrapper.AddParameter("@IS_ACTIVE",objGeneralInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@NO_OF_PETS",objGeneralInfo.NO_OF_PETS);
				
				//Gaurav : new field added itrack issues change impact doc 756/742
				objDataWrapper.AddParameter("@IS_SWIMPOLL_HOTTUB",objGeneralInfo.IS_SWIMPOLL_HOTTUB);
				//objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",objGeneralInfo.LAST_INSPECTED_DATE);

				if(objGeneralInfo.LAST_INSPECTED_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",objGeneralInfo.LAST_INSPECTED_DATE );
				else
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",null );
					
				//----------------------------------Added by mohit--------------.
					
				objDataWrapper.AddParameter("@IS_RENTED_IN_PART",objGeneralInfo.IS_RENTED_IN_PART);
				objDataWrapper.AddParameter("@IS_VACENT_OCCUPY",objGeneralInfo.IS_VACENT_OCCUPY);
				objDataWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objGeneralInfo.IS_DWELLING_OWNED_BY_OTHER);
				objDataWrapper.AddParameter("@IS_PROP_NEXT_COMMERICAL",objGeneralInfo.IS_PROP_NEXT_COMMERICAL);
				objDataWrapper.AddParameter("@DESC_PROPERTY",objGeneralInfo.DESC_PROPERTY);
				objDataWrapper.AddParameter("@ARE_STAIRWAYS_PRESENT",objGeneralInfo.ARE_STAIRWAYS_PRESENT);
				objDataWrapper.AddParameter("@DESC_STAIRWAYS",objGeneralInfo.DESC_STAIRWAYS);
				objDataWrapper.AddParameter("@IS_OWNERS_DWELLING_CHANGED",objGeneralInfo.IS_OWNERS_DWELLING_CHANGED);
				objDataWrapper.AddParameter("@DESC_OWNER",objGeneralInfo.DESC_OWNER);

				objDataWrapper.AddParameter("@DESC_VACENT_OCCUPY",objGeneralInfo.DESC_VACENT_OCCUPY);
				objDataWrapper.AddParameter("@DESC_RENTED_IN_PART",objGeneralInfo.DESC_RENTED_IN_PART);
				objDataWrapper.AddParameter("@DESC_DWELLING_OWNED_BY_OTHER",objGeneralInfo.DESC_DWELLING_OWNED_BY_OTHER);
				
				objDataWrapper.AddParameter("@ANY_HEATING_SOURCE",objGeneralInfo.ANY_HEATING_SOURCE);
				objDataWrapper.AddParameter("@DESC_ANY_HEATING_SOURCE",objGeneralInfo.DESC_ANY_HEATING_SOURCE);
				
				objDataWrapper.AddParameter("@NON_SMOKER_CREDIT",objGeneralInfo.NON_SMOKER_CREDIT); // Added on 19/10/2005
				//-----------------------------------End-------------------------.

				// Added by Mohit on 4/11/2005
				objDataWrapper.AddParameter("@SWIMMING_POOL",objGeneralInfo.SWIMMING_POOL);
				objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objGeneralInfo.SWIMMING_POOL_TYPE);
				// End
				objDataWrapper.AddParameter("@Any_Forming",objGeneralInfo.Any_Forming);
				objDataWrapper.AddParameter("@Premises",DefaultValues.GetIntNull(objGeneralInfo.Premises));
				objDataWrapper.AddParameter("@Of_Acres",DefaultValues.GetDoubleNull(objGeneralInfo.Of_Acres));
				objDataWrapper.AddParameter("@IsAny_Horse",DefaultValues.GetStringNull(objGeneralInfo.IsAny_Horse));
				objDataWrapper.AddParameter("@Of_Acres_P",DefaultValues.GetDoubleNull(objGeneralInfo.Of_Acres_P));
				objDataWrapper.AddParameter("@No_Horses",DefaultValues.GetIntNull(objGeneralInfo.No_Horses));
				objDataWrapper.AddParameter("@DESC_FARMING_BUSINESS_COND",DefaultValues.GetStringNull(objGeneralInfo.DESC_FARMING_BUSINESS_COND));
				objDataWrapper.AddParameter("@location",DefaultValues.GetStringNull(objGeneralInfo.Location));
				objDataWrapper.AddParameter("@DESC_location",DefaultValues.GetStringNull(objGeneralInfo.DESC_Location));
				objDataWrapper.AddParameter("@DOG_SURCHARGE",objGeneralInfo.DOG_SURCHARGE);

				//added By Shafi 27-03-2006
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objGeneralInfo.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objGeneralInfo.YEARS_INSU));

				//Added by Sumit for new description fields on 30-03-2006
				objDataWrapper.AddParameter("@DESC_IS_SWIMPOLL_HOTTUB",objGeneralInfo.DESC_IS_SWIMPOLL_HOTTUB);
				objDataWrapper.AddParameter("@DESC_MULTI_POLICY_DISC_APPLIED",objGeneralInfo.DESC_MULTI_POLICY_DISC_APPLIED);
				objDataWrapper.AddParameter("@DESC_BUILD_UNDER_CON_GEN_CONT",objGeneralInfo.DESC_BUILD_UNDER_CON_GEN_CONT);
				objDataWrapper.AddParameter("@DIVING_BOARD",objGeneralInfo.DIVING_BOARD);
				objDataWrapper.AddParameter("@APPROVED_FENCE",objGeneralInfo.APPROVED_FENCE);
				objDataWrapper.AddParameter("@SLIDE",objGeneralInfo.SLIDE);


				objDataWrapper.AddParameter("@PROVIDE_HOME_DAY_CARE",objGeneralInfo.PROVIDE_HOME_DAY_CARE);
				objDataWrapper.AddParameter("@MODULAR_MANUFACTURED_HOME",objGeneralInfo.MODULAR_MANUFACTURED_HOME);
				objDataWrapper.AddParameter("@BUILT_ON_CONTINUOUS_FOUNDATION",objGeneralInfo.BUILT_ON_CONTINUOUS_FOUNDATION);
				
				objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE",objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE);
				objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC",objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC);

				objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN",objGeneralInfo.PROPERTY_ON_MORE_THAN);
				objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN_DESC",objGeneralInfo.PROPERTY_ON_MORE_THAN_DESC);
				objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME",objGeneralInfo.DWELLING_MOBILE_HOME);
				objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME_DESC",objGeneralInfo.DWELLING_MOBILE_HOME_DESC);
				objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART",objGeneralInfo.PROPERTY_USED_WHOLE_PART);
				objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART_DESC",objGeneralInfo.PROPERTY_USED_WHOLE_PART_DESC);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objGeneralInfo.ANY_PRIOR_LOSSES);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objGeneralInfo.ANY_PRIOR_LOSSES_DESC);
				objDataWrapper.AddParameter("@BOAT_WITH_HOMEOWNER",objGeneralInfo.BOAT_WITH_HOMEOWNER);
				//Added for Itrack Issue 6640 on 10 Dec 09
				if(objGeneralInfo.NON_WEATHER_CLAIMS != -1)
					objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS",objGeneralInfo.NON_WEATHER_CLAIMS);
				else
					objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS",System.DBNull.Value);
				if(objGeneralInfo.WEATHER_CLAIMS != -1)
					objDataWrapper.AddParameter("@WEATHER_CLAIMS",objGeneralInfo.WEATHER_CLAIMS);
				else
					objDataWrapper.AddParameter("@WEATHER_CLAIMS",System.DBNull.Value);




				if(TransactionLog) 
				{
					objGeneralInfo.TransactLabel		=	ClsCommon.MapTransactionLabel("/application/Aspx/HomeOwners/AddGeneralInformationIFrame.aspx.resx");
					strTranXML							=	objBuilder.GetTransactionLogXML(objOldGeneralInfo,objGeneralInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(UPDATE_PROC);
					else				
					{
						ClsTransactionInfo objTransactionInfo = new ClsTransactionInfo();
						
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objGeneralInfo.MODIFIED_BY;
						//objTransactionInfo.TRANS_DESC		=	"Homeowner's general information is modified";
						objTransactionInfo.TRANS_DESC		=	"Underwriting Questions is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						returnResult = objDataWrapper.ExecuteNonQuery(UPDATE_PROC,objTransactionInfo);
					}
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(UPDATE_PROC);
				}

				ClsHomeCoverages objCoverage;
				if(objGeneralInfo.CalledFrom == "HOME")
				{
					objCoverage=new ClsHomeCoverages();
					objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objGeneralInfo.CUSTOMER_ID,objGeneralInfo.APP_ID,objGeneralInfo.APP_VERSION_ID,RuleType.OtherAppDependent);
				}
				//Update coverages///////////
				//this.UpdateApplicationCoverages(objGeneralInfo.CUSTOMER_ID,objGeneralInfo.APP_ID,objGeneralInfo.APP_VERSION_ID,objDataWrapper);
				////////////////////
				
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

		
		
		
		
/// <summary>

		
		
		
		public DataTable GetValueForPageControls(string CUSTOMER_ID ,string APP_ID,string APP_VERSION_ID)
		{
			string strSql = "Proc_PrefillAPP_HOME_OWNER_GEN_INFO";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);			
			objDataWrapper.AddParameter("@APP_ID",APP_ID);			
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}

		public DataTable GetValueForPageControlsPolicy(string CUSTOMER_ID ,string POL_ID,string POL_VERSION_ID)
		{
			string strSql = "Proc_PrefillAPP_HOME_OWNER_GEN_INFO_Policy";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);			
			objDataWrapper.AddParameter("@POL_ID",POL_ID);			
			objDataWrapper.AddParameter("@POL_VERSION_ID",POL_VERSION_ID);			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}


		#region IDisposable Members

		public void Dispose()
		{
			// TODO:  Add ClsHomeGeneralInformation.Dispose implementation
		}

		#endregion

		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objGeneralInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Save(clsGeneralInfo objGeneralInfo,DataWrapper objDataWrapper)
		{
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder	=	new SqlUpdateBuilder();
			
		
				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_FARMING_BUSINESS_COND",objGeneralInfo.ANY_FARMING_BUSINESS_COND);
				//objDataWrapper.AddParameter("@DESC_BUSINESS",objGeneralInfo.DESC_BUSINESS);
				objDataWrapper.AddParameter("@ANY_RESIDENCE_EMPLOYEE",objGeneralInfo.ANY_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@DESC_RESIDENCE_EMPLOYEE",objGeneralInfo.DESC_RESIDENCE_EMPLOYEE);
				objDataWrapper.AddParameter("@ANY_OTHER_RESI_OWNED",objGeneralInfo.ANY_OTHER_RESI_OWNED);
				objDataWrapper.AddParameter("@DESC_OTHER_RESIDENCE",objGeneralInfo.DESC_OTHER_RESIDENCE);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objGeneralInfo.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@DESC_OTHER_INSURANCE",objGeneralInfo.DESC_OTHER_INSURANCE);
				objDataWrapper.AddParameter("@HAS_INSU_TRANSFERED_AGENCY",objGeneralInfo.HAS_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@DESC_INSU_TRANSFERED_AGENCY",objGeneralInfo.DESC_INSU_TRANSFERED_AGENCY);
				objDataWrapper.AddParameter("@ANY_COV_DECLINED_CANCELED",objGeneralInfo.ANY_COV_DECLINED_CANCELED);
				objDataWrapper.AddParameter("@DESC_COV_DECLINED_CANCELED",objGeneralInfo.DESC_COV_DECLINED_CANCELED);
				objDataWrapper.AddParameter("@ANIMALS_EXO_PETS_HISTORY",objGeneralInfo.ANIMALS_EXO_PETS_HISTORY);
				objDataWrapper.AddParameter("@BREED",objGeneralInfo.BREED);
				//Add by kranti
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objGeneralInfo.ANY_PRIOR_LOSSES);
			
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objGeneralInfo.OTHER_DESCRIPTION);


			
				objDataWrapper.AddParameter("@CONVICTION_DEGREE_IN_PAST",objGeneralInfo.CONVICTION_DEGREE_IN_PAST);
				objDataWrapper.AddParameter("@DESC_CONVICTION_DEGREE_IN_PAST",objGeneralInfo.DESC_CONVICTION_DEGREE_IN_PAST);
//			
				objDataWrapper.AddParameter("@ANY_RENOVATION",objGeneralInfo.ANY_RENOVATION);
				objDataWrapper.AddParameter("@DESC_RENOVATION",objGeneralInfo.DESC_RENOVATION);
//				objDataWrapper.AddParameter("@HOUSE_FOR_SALE",objGeneralInfo.HOUSE_FOR_SALE);
//				objDataWrapper.AddParameter("@DESC_HOUSE_FOR_SALE",objGeneralInfo.DESC_HOUSE_FOR_SALE);
//				objDataWrapper.AddParameter("@ANY_NON_RESI_PROPERTY",objGeneralInfo.ANY_NON_RESI_PROPERTY);
//				objDataWrapper.AddParameter("@DESC_NON_RESI_PROPERTY",objGeneralInfo.DESC_NON_RESI_PROPERTY);
				objDataWrapper.AddParameter("@TRAMPOLINE",objGeneralInfo.TRAMPOLINE);
				objDataWrapper.AddParameter("@DESC_TRAMPOLINE",objGeneralInfo.DESC_TRAMPOLINE);
//				objDataWrapper.AddParameter("@STRUCT_ORI_BUILT_FOR",objGeneralInfo.STRUCT_ORI_BUILT_FOR);
//				objDataWrapper.AddParameter("@DESC_STRUCT_ORI_BUILT_FOR",objGeneralInfo.DESC_STRUCT_ORI_BUILT_FOR);
				objDataWrapper.AddParameter("@LEAD_PAINT_HAZARD",objGeneralInfo.LEAD_PAINT_HAZARD);
				objDataWrapper.AddParameter("@DESC_LEAD_PAINT_HAZARD",objGeneralInfo.DESC_LEAD_PAINT_HAZARD);
//				objDataWrapper.AddParameter("@FUEL_OIL_TANK_PERMISES",objGeneralInfo.FUEL_OIL_TANK_PERMISES);
//				objDataWrapper.AddParameter("@DESC_FUEL_OIL_TANK_PERMISES",objGeneralInfo.DESC_FUEL_OIL_TANK_PERMISES);
				objDataWrapper.AddParameter("@RENTERS",objGeneralInfo.RENTERS);
				objDataWrapper.AddParameter("@DESC_RENTERS",objGeneralInfo.DESC_RENTERS);
				objDataWrapper.AddParameter("@BUILD_UNDER_CON_GEN_CONT",objGeneralInfo.BUILD_UNDER_CON_GEN_CONT);
				objDataWrapper.AddParameter("@REMARKS",objGeneralInfo.REMARKS);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objGeneralInfo.MULTI_POLICY_DISC_APPLIED);     
				objDataWrapper.AddParameter("@CREATED_BY",objGeneralInfo.CREATED_BY);
				objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
				objDataWrapper.AddParameter("@NO_OF_PETS",objGeneralInfo.NO_OF_PETS);
				objDataWrapper.AddParameter("@NON_SMOKER_CREDIT",objGeneralInfo.NON_SMOKER_CREDIT);
				objDataWrapper.AddParameter("@ANY_HEATING_SOURCE",objGeneralInfo.ANY_HEATING_SOURCE);
            	//Added for Exp and Loss free 25 jan 2006
			if(objGeneralInfo.IS_LOSS_FREE_12_MONTHS !="-1")
			{
				objDataWrapper.AddParameter("@IS_LOSS_FREE_12_MONTHS",objGeneralInfo.IS_LOSS_FREE_12_MONTHS);
			}
				
				objDataWrapper.AddParameter("@EXP_AGE_CREDIT",objGeneralInfo.EXP_AGE_CREDIT);

			//Added of No of years insured :  9 feb 2006
			objDataWrapper.AddParameter("@NO_OF_YEARS_INSURED",objGeneralInfo.NO_OF_YEARS_INSURED);
			//



				//Gaurav : new field added itrack issues change impact doc 756/742
				objDataWrapper.AddParameter("@IS_SWIMPOLL_HOTTUB",objGeneralInfo.IS_SWIMPOLL_HOTTUB);
				//objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",objGeneralInfo.LAST_INSPECTED_DATE);
				
				if(objGeneralInfo.LAST_INSPECTED_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",objGeneralInfo.LAST_INSPECTED_DATE );
				else
					objDataWrapper.AddParameter("@LAST_INSPECTED_DATE",null );
				
				
			objDataWrapper.AddParameter("@IS_VACENT_OCCUPY",objGeneralInfo.IS_VACENT_OCCUPY);
			objDataWrapper.AddParameter("@IS_RENTED_IN_PART",objGeneralInfo.IS_RENTED_IN_PART);
			objDataWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objGeneralInfo.IS_DWELLING_OWNED_BY_OTHER);
			objDataWrapper.AddParameter("@IS_PROP_NEXT_COMMERICAL",objGeneralInfo.IS_PROP_NEXT_COMMERICAL);
			objDataWrapper.AddParameter("@ARE_STAIRWAYS_PRESENT",objGeneralInfo.ARE_STAIRWAYS_PRESENT);
			objDataWrapper.AddParameter("@IS_OWNERS_DWELLING_CHANGED",objGeneralInfo.IS_OWNERS_DWELLING_CHANGED);
			objDataWrapper.AddParameter("@SWIMMING_POOL",objGeneralInfo.SWIMMING_POOL);
			objDataWrapper.AddParameter("@ANY_FORMING",objGeneralInfo.Any_Forming);
			
			objDataWrapper.AddParameter("@Location",objGeneralInfo.Location );

			objDataWrapper.AddParameter("@PREMISES",DefaultValues.GetIntNull(objGeneralInfo.Premises));
			objDataWrapper.AddParameter("@ISANY_HORSE",DefaultValues.GetStringNull(objGeneralInfo.IsAny_Horse));

			//RPSINGH -- 17 July 2006
			objDataWrapper.AddParameter("@PROPERTY_ON_MORE_THAN",objGeneralInfo.PROPERTY_ON_MORE_THAN);
			objDataWrapper.AddParameter("@PROPERTY_USED_WHOLE_PART",objGeneralInfo.PROPERTY_USED_WHOLE_PART);
			objDataWrapper.AddParameter("@DWELLING_MOBILE_HOME",objGeneralInfo.DWELLING_MOBILE_HOME);

			objDataWrapper.AddParameter("@VALUED_CUSTOMER_DISCOUNT_OVERRIDE",objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE);
			objDataWrapper.AddParameter("@MODULAR_MANUFACTURED_HOME",objGeneralInfo.MODULAR_MANUFACTURED_HOME);

			objDataWrapper.AddParameter("@YEARS_INSU_WOL",objGeneralInfo.YEARS_INSU_WOL);

			//Itrack 6640
			objDataWrapper.AddParameter("@NON_WEATHER_CLAIMS",objGeneralInfo.NON_WEATHER_CLAIMS);
			objDataWrapper.AddParameter("@WEATHER_CLAIMS",objGeneralInfo.WEATHER_CLAIMS);
									
			int returnResult = 0;
			string strTranXML="";
			//add by kranti for Transaction Log 
			if(TransactionLog) 
			{
				objGeneralInfo.TransactLabel		=	ClsCommon.MapTransactionLabel("/application/Aspx/HomeOwners/AddGeneralInformationIFrame.aspx.resx");
				strTranXML							=	objBuilder.GetTransactionLogXML(objGeneralInfo);
				if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
					returnResult	=	objDataWrapper.ExecuteNonQuery("Proc_SaveAPP_HOME_OWNER_GEN_INFO_ACORD");
				else				
				{
					ClsTransactionInfo objTransactionInfo = new ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objGeneralInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Underwriting Questions is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_SaveAPP_HOME_OWNER_GEN_INFO_ACORD",objTransactionInfo);
				}
			}
			else
			{
				returnResult = objDataWrapper.ExecuteNonQuery("Proc_SaveAPP_HOME_OWNER_GEN_INFO_ACORD");
			}

			//End By kranti

			//returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SaveAPP_HOME_OWNER_GEN_INFO_ACORD");

			//Commited By Shafi Handled By Xml
			
			//RP -- Smokers credit
//			if (objGeneralInfo.NON_SMOKER_CREDIT == "1")
//			{
//				objDataWrapper.ClearParameteres();
//				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
//				objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID);
//				objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralInfo.APP_VERSION_ID);
//				
//				returnResult	= objDataWrapper.ExecuteNonQuery("PROC_INSERT_SMOKER_COVERAGE_ACORD");
//			}

			return returnResult;
			
		}
		
		/// <summary>
		/// Updates coverages when General Information is updated/added
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="objDataWrapper"></param>
		public void UpdatePolicyCoverages(int customerID, int polID, int polVersionID, DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 ) 
			{
				objDataWrapper.ClearParameteres();
			}

			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.AddParameter("@POLICY_ID",polID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);

			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_COVERAGES_POLICY_FROM_GEN_INFO");
		}
		
		/// <summary>
		/// Updates coverages when General Information is updated/added
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="objDataWrapper"></param>
		public void UpdatePolicyCoverages(int customerID, int polID, int polVersionID, int dwellingID,DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 ) 
			{
				objDataWrapper.ClearParameteres();
			}

			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.AddParameter("@POLICY_ID",polID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objDataWrapper.AddParameter("@DWELLING_ID",dwellingID);

			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_COVERAGES_POLICY_FROM_GEN_INFO_FOR_DWELLING");
		}

		/// <summary>
		/// Updates coverages when General Information is updated/added
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="objDataWrapper"></param>
		public void UpdateApplicationCoverages(int customerID, int appID, int appVersionID, DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 ) 
			{
				objDataWrapper.ClearParameteres();
			}

			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.AddParameter("@APP_ID",appID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);

			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_COVERAGES_FROM_GEN_INFO");
		}

		/// <summary>
		/// Updates coverages when General Information is updated/added
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="objDataWrapper"></param>
		public void UpdateApplicationCoverages(int customerID, int appID, int appVersionID, int dwellingID,DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 ) 
			{
				objDataWrapper.ClearParameteres();
			}

			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.AddParameter("@APP_ID",appID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objDataWrapper.AddParameter("@DWELLING_ID",dwellingID);

			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_COVERAGES_FROM_GEN_INFO_FOR_DWELLING");
		}

	}
}
