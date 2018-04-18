/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	4/27/2005 4:04:28 PM
<End Date				: -	4/27/2005
<Description				: - 	This File is Used to implement Methods and Function , General Information of Private Passenger Automobiles
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified

<Modified Date			: - Mohit
<Modified By			: - 22/09/2005
<Purpose				: - Added function GetMotorCycle().

<Modified Date			: - Vijay Arora
<Modified By			: - 08-11-2005
<Purpose				: - Write the Policy Functions.
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;  
using Cms.Model.Policy;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// This Class is Used to implement Methods and Function , General Information of Private Passenger Automobiles
	/// </summary>
	public class ClsPPGeneralInformation : Cms.BusinessLayer.BlCommon.ClsCommon 
	{
		private const	string		APP_AUTO_GEN_INFO			=	"APP_AUTO_GEN_INFO";
		//private const	string		INSERT_UPDATE_PROC		=	"Proc_InsertPPGeneralInformation";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _CUSTOMER_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivatePPGeneralInformation";
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
		public ClsPPGeneralInformation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		

		/// <summary>
		/// Saves the information passed in model object to database.QQ to Utier tab
		/// </summary>
		/// <param name="objPPUtier"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public int SaveUnderwritingTier(Cms.Model.Application.ClsUnderwritingTierInfo  objPPUtier,DataWrapper objDataWrapper)
		{
			
			string		strStoredProc	=	"Proc_Save_PP_UnderTierInfo_ACORD";
			DateTime	RecordDate		=	DateTime.Now;

			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",objPPUtier.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objPPUtier.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objPPUtier.APP_VERSION_ID);
			objDataWrapper.AddParameter("@UNDRWRTINGTIER",objPPUtier.UNDERWRITING_TIER);
			objDataWrapper.AddParameter("@UNTIER_ASSIGNED_DATE",objPPUtier.UNTIER_ASSIGNED_DATE);

			int returnResult = 0;
			if(TransactionLogRequired)
			{
				objPPUtier.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Application/Aspx/PPGeneralInformation_Additional.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objPPUtier);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.APP_ID			=	objPPUtier.APP_ID;
				objTransactionInfo.APP_VERSION_ID	=	objPPUtier.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID		=	objPPUtier.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objPPUtier.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Private passenger Underwriting Tier is added from Quick quote.";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
				
				
			return returnResult;



		}
		
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPPGeneralInformation">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Save(ClsPPGeneralInformationInfo  objPPGeneralInformation,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_Save_PPGenInfo_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			
				objDataWrapper.AddParameter("@APP_ID",objPPGeneralInformation.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objPPGeneralInformation.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH",objPPGeneralInformation.ANY_NON_OWNED_VEH);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_PP_DESC",objPPGeneralInformation.ANY_NON_OWNED_VEH_PP_DESC);
				objDataWrapper.AddParameter("@CAR_MODIFIED",objPPGeneralInformation.CAR_MODIFIED);
				objDataWrapper.AddParameter("@CAR_MODIFIED_DESC",objPPGeneralInformation.CAR_MODIFIED_DESC);
				objDataWrapper.AddParameter("@EXISTING_DMG",objPPGeneralInformation.EXISTING_DMG);
				objDataWrapper.AddParameter("@EXISTING_DMG_PP_DESC",objPPGeneralInformation.EXISTING_DMG_PP_DESC);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH",objPPGeneralInformation.ANY_CAR_AT_SCH);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH_DESC",objPPGeneralInformation.ANY_CAR_AT_SCH_DESC);
				//objDataWrapper.AddParameter("@SCHOOL_CARS_LIST",objPPGeneralInformation.SCHOOL_CARS_LIST);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU",objPPGeneralInformation.ANY_OTH_AUTO_INSU);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU_DESC",objPPGeneralInformation.ANY_OTH_AUTO_INSU_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_PP_DESC",objPPGeneralInformation.ANY_OTH_INSU_COMP_PP_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objPPGeneralInformation.ANY_OTH_INSU_COMP);
				//objDataWrapper.AddParameter("@OTHER_POLICY_NUMBER_LIST",objPPGeneralInformation.OTHER_POLICY_NUMBER_LIST);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY",objPPGeneralInformation.H_MEM_IN_MILITARY);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_DESC",objPPGeneralInformation.H_MEM_IN_MILITARY_DESC);
				//objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_LIST",objPPGeneralInformation.H_MEM_IN_MILITARY_LIST);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objPPGeneralInformation.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_PP_DESC",objPPGeneralInformation.DRIVER_SUS_REVOKED_PP_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objPPGeneralInformation.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_PP_DESC",objPPGeneralInformation.PHY_MENTL_CHALLENGED_PP_DESC);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_PP_DESC",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY_PP_DESC);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER",objPPGeneralInformation.INS_AGENCY_TRANSFER);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_PP_DESC",objPPGeneralInformation.INS_AGENCY_TRANSFER_PP_DESC);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objPPGeneralInformation.COVERAGE_DECLINED);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_PP_DESC",objPPGeneralInformation.COVERAGE_DECLINED_PP_DESC);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED",objPPGeneralInformation.AGENCY_VEH_INSPECTED);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_PP_DESC",objPPGeneralInformation.AGENCY_VEH_INSPECTED_PP_DESC);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE",objPPGeneralInformation.USE_AS_TRANSPORT_FEE);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE_DESC",objPPGeneralInformation.USE_AS_TRANSPORT_FEE_DESC);
				objDataWrapper.AddParameter("@SALVAGE_TITLE",objPPGeneralInformation.SALVAGE_TITLE);
				objDataWrapper.AddParameter("@SALVAGE_TITLE_PP_DESC",objPPGeneralInformation.SALVAGE_TITLE_PP_DESC);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO",objPPGeneralInformation.ANY_ANTIQUE_AUTO);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO_DESC",objPPGeneralInformation.ANY_ANTIQUE_AUTO_DESC);
				//objDataWrapper.AddParameter("@ANTIQUE_AUTO_LIST",objPPGeneralInformation.ANTIQUE_AUTO_LIST);
				objDataWrapper.AddParameter("@REMARKS",objPPGeneralInformation.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objPPGeneralInformation.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objPPGeneralInformation.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@INSERTUPDATE","I");
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPPGeneralInformation.CUSTOMER_ID);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED );
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED_PP_DESC );
				objDataWrapper.AddParameter("@IS_COST_OVER_DEFINED_LIMIT",objPPGeneralInformation.IS_COST_OVER_DEFINED_LIMIT);
				objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );

				if (objPPGeneralInformation.COST_EQUIPMENT_DESC!=0)
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",objPPGeneralInformation.COST_EQUIPMENT_DESC );
				}
				else
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",null);
				}

				
				if (objPPGeneralInformation.IS_OTHER_THAN_INSURED == "1")
				{
					objDataWrapper.AddParameter("@FullName",objPPGeneralInformation.FullName );
					if(objPPGeneralInformation.DATE_OF_BIRTH!= DateTime.MinValue)
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",objPPGeneralInformation.DATE_OF_BIRTH );
					else
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
					objDataWrapper.AddParameter("@DrivingLisence",objPPGeneralInformation.DrivingLisence );
					objDataWrapper.AddParameter("@PolicyNumber",objPPGeneralInformation.PolicyNumber );
					objDataWrapper.AddParameter("@CompanyName",objPPGeneralInformation.CompanyName );
					objDataWrapper.AddParameter("@InsuredElseWhere",objPPGeneralInformation.InsuredElseWhere );
					objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objPPGeneralInformation.IS_OTHER_THAN_INSURED );
					//objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
					objDataWrapper.AddParameter("@WhichCycle",objPPGeneralInformation.WhichCycle );
				}
				else
				{
					objDataWrapper.AddParameter("@FullName",null);
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null);
					objDataWrapper.AddParameter("@DrivingLisence",null);
					objDataWrapper.AddParameter("@WhichCycle",null);
					objDataWrapper.AddParameter("@PolicyNumber",null );
					objDataWrapper.AddParameter("@CompanyName",null );
					objDataWrapper.AddParameter("@InsuredElseWhere",null);
					objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",null );
					//objDataWrapper.AddParameter("@CURR_RES_TYPE",null );
				
				}
				
			objDataWrapper.AddParameter("@IS_COMMERCIAL_USE",objPPGeneralInformation.IS_COMMERCIAL_USE);
			objDataWrapper.AddParameter("@IS_USEDFOR_RACING",objPPGeneralInformation.IS_USEDFOR_RACING);
			
			objDataWrapper.AddParameter("@IS_MORE_WHEELS",objPPGeneralInformation.IS_MORE_WHEELS);
			objDataWrapper.AddParameter("@IS_EXTENDED_FORKS",objPPGeneralInformation.IS_EXTENDED_FORKS);
			objDataWrapper.AddParameter("@IS_LICENSED_FOR_ROAD",objPPGeneralInformation.IS_LICENSED_FOR_ROAD);
			objDataWrapper.AddParameter("@IS_MODIFIED_INCREASE_SPEED",objPPGeneralInformation.IS_MODIFIED_INCREASE_SPEED);
			objDataWrapper.AddParameter("@IS_MODIFIED_KIT",objPPGeneralInformation.IS_MODIFIED_KIT);
			objDataWrapper.AddParameter("@IS_TAKEN_OUT",objPPGeneralInformation.IS_TAKEN_OUT );
			objDataWrapper.AddParameter("@IS_CONVICTED_CARELESS_DRIVE",objPPGeneralInformation.IS_CONVICTED_CARELESS_DRIVE );
			objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objPPGeneralInformation.IS_CONVICTED_ACCIDENT );
			
			//years insured and years insured wth wolverine
			objDataWrapper.AddParameter("@YEARS_INSU",objPPGeneralInformation.YEARS_INSU);
			objDataWrapper.AddParameter("@YEARS_INSU_WOL",objPPGeneralInformation.YEARS_INSU_WOL );
			objDataWrapper.AddParameter("@SEAT_BELT_CREDIT",objPPGeneralInformation.SEAT_BELT_CREDIT  );
			objDataWrapper.AddParameter("@APPLY_PERS_UMB_POL",objPPGeneralInformation.APPLY_PERS_UMB_POL);
			//Itrack  5801
			objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objPPGeneralInformation.ANY_PRIOR_LOSSES);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPPGeneralInformation.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Application/Aspx/PPGeneralInformationIframe.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPPGeneralInformation);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objPPGeneralInformation.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objPPGeneralInformation.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPPGeneralInformation.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPPGeneralInformation.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Private passenger general information is added from Quick quote.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				
				return returnResult;
				//				}
			
		}


		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPPGeneralInformation">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPPGeneralInformationInfo  objPPGeneralInformation)
		{
			string		strStoredProc	=	"Proc_InsertPPGeneralInformation";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@APP_ID",objPPGeneralInformation.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objPPGeneralInformation.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH",objPPGeneralInformation.ANY_NON_OWNED_VEH);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_PP_DESC",objPPGeneralInformation.ANY_NON_OWNED_VEH_PP_DESC);
				objDataWrapper.AddParameter("@CAR_MODIFIED",objPPGeneralInformation.CAR_MODIFIED);
				objDataWrapper.AddParameter("@CAR_MODIFIED_DESC",objPPGeneralInformation.CAR_MODIFIED_DESC);
				objDataWrapper.AddParameter("@EXISTING_DMG",objPPGeneralInformation.EXISTING_DMG);
				objDataWrapper.AddParameter("@EXISTING_DMG_PP_DESC",objPPGeneralInformation.EXISTING_DMG_PP_DESC);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH",objPPGeneralInformation.ANY_CAR_AT_SCH);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH_DESC",objPPGeneralInformation.ANY_CAR_AT_SCH_DESC);
				//objDataWrapper.AddParameter("@SCHOOL_CARS_LIST",objPPGeneralInformation.SCHOOL_CARS_LIST);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU",objPPGeneralInformation.ANY_OTH_AUTO_INSU);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU_DESC",objPPGeneralInformation.ANY_OTH_AUTO_INSU_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_PP_DESC",objPPGeneralInformation.ANY_OTH_INSU_COMP_PP_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objPPGeneralInformation.ANY_OTH_INSU_COMP);
				//objDataWrapper.AddParameter("@OTHER_POLICY_NUMBER_LIST",objPPGeneralInformation.OTHER_POLICY_NUMBER_LIST);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY",objPPGeneralInformation.H_MEM_IN_MILITARY);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_DESC",objPPGeneralInformation.H_MEM_IN_MILITARY_DESC);
				//objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_LIST",objPPGeneralInformation.H_MEM_IN_MILITARY_LIST);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objPPGeneralInformation.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_PP_DESC",objPPGeneralInformation.DRIVER_SUS_REVOKED_PP_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objPPGeneralInformation.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_PP_DESC",objPPGeneralInformation.PHY_MENTL_CHALLENGED_PP_DESC);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_PP_DESC",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY_PP_DESC);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER",objPPGeneralInformation.INS_AGENCY_TRANSFER);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_PP_DESC",objPPGeneralInformation.INS_AGENCY_TRANSFER_PP_DESC);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objPPGeneralInformation.COVERAGE_DECLINED);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_PP_DESC",objPPGeneralInformation.COVERAGE_DECLINED_PP_DESC);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED",objPPGeneralInformation.AGENCY_VEH_INSPECTED);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_PP_DESC",objPPGeneralInformation.AGENCY_VEH_INSPECTED_PP_DESC);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE",objPPGeneralInformation.USE_AS_TRANSPORT_FEE);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE_DESC",objPPGeneralInformation.USE_AS_TRANSPORT_FEE_DESC);
				objDataWrapper.AddParameter("@SALVAGE_TITLE",objPPGeneralInformation.SALVAGE_TITLE);
				objDataWrapper.AddParameter("@SALVAGE_TITLE_PP_DESC",objPPGeneralInformation.SALVAGE_TITLE_PP_DESC);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO",objPPGeneralInformation.ANY_ANTIQUE_AUTO);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO_DESC",objPPGeneralInformation.ANY_ANTIQUE_AUTO_DESC);
				objDataWrapper.AddParameter("@REMARKS",objPPGeneralInformation.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objPPGeneralInformation.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objPPGeneralInformation.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objPPGeneralInformation.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@INSERTUPDATE","I");
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPPGeneralInformation.CUSTOMER_ID);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED );
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED_PP_DESC );
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU));
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objPPGeneralInformation.ANY_PRIOR_LOSSES);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objPPGeneralInformation.ANY_PRIOR_LOSSES_DESC);
				if (objPPGeneralInformation.COST_EQUIPMENT_DESC!=0)
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",objPPGeneralInformation.COST_EQUIPMENT_DESC );
				}
				else
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",null);
				}
				objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
				objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objPPGeneralInformation.IS_OTHER_THAN_INSURED );
				if (objPPGeneralInformation.IS_OTHER_THAN_INSURED == "1")
				{
					objDataWrapper.AddParameter("@FullName",objPPGeneralInformation.FullName );
					if(objPPGeneralInformation.DATE_OF_BIRTH!= DateTime.MinValue)
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",objPPGeneralInformation.DATE_OF_BIRTH );
					else
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
					objDataWrapper.AddParameter("@DrivingLisence",objPPGeneralInformation.DrivingLisence );
					objDataWrapper.AddParameter("@PolicyNumber",objPPGeneralInformation.PolicyNumber );
					objDataWrapper.AddParameter("@CompanyName",objPPGeneralInformation.CompanyName );
					objDataWrapper.AddParameter("@InsuredElseWhere",objPPGeneralInformation.InsuredElseWhere );					
					objDataWrapper.AddParameter("@WhichCycle",objPPGeneralInformation.WhichCycle );
				}
				else
				{
					objDataWrapper.AddParameter("@FullName",null);
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null);
					objDataWrapper.AddParameter("@DrivingLisence",null);
					objDataWrapper.AddParameter("@WhichCycle",null);
					objDataWrapper.AddParameter("@PolicyNumber",null );
					objDataWrapper.AddParameter("@CompanyName",null );
					objDataWrapper.AddParameter("@InsuredElseWhere",null);					
				}

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPPGeneralInformation.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Application/Aspx/PPGeneralInformationIframe.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPPGeneralInformation);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objPPGeneralInformation.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objPPGeneralInformation.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPPGeneralInformation.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPPGeneralInformation.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Private passenger underwriting questions is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				//int CUSTOMER_ID = int.Parse(objSqlParameter.Value.ToString());
				//int CUSTOMER_ID = 1;

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				//				if (CUSTOMER_ID == -1)
				//				{
				//					return -1;
				//				}
				//				else
				//				{
				//objPPGeneralInformation.CUSTOMER_ID = CUSTOMER_ID;
				return returnResult;
				//				}
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
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPPGeneralInformation">Model class object.</param>
		/// <returns>No of records effected.</returns>
		//Function added by Charles on 24-Dec-09 for Itrack 6830
		public int Add(Cms.Model.Application.ClsUnderwritingTierInfo objPPGeneralInformation)
		{
			string		strStoredProc	=	"Proc_Save_APP_UNDERWRITING_TIER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPPGeneralInformation.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objPPGeneralInformation.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objPPGeneralInformation.APP_VERSION_ID);
				
				objDataWrapper.AddParameter("@UNDERWRITING_TIER",objPPGeneralInformation.UNDERWRITING_TIER); 
				if(objPPGeneralInformation.UNTIER_ASSIGNED_DATE == DateTime.MinValue)
					objDataWrapper.AddParameter("@UNTIER_ASSIGNED_DATE",null);
 			    else
					objDataWrapper.AddParameter("@UNTIER_ASSIGNED_DATE",objPPGeneralInformation.UNTIER_ASSIGNED_DATE); 
				objDataWrapper.AddParameter("@CAP_INC",objPPGeneralInformation.CAP_INC); 
				objDataWrapper.AddParameter("@CAP_DEC",objPPGeneralInformation.CAP_DEC); 
				objDataWrapper.AddParameter("@CAP_RATE_CHANGE_REL",objPPGeneralInformation.CAP_RATE_CHANGE_REL); 
				objDataWrapper.AddParameter("@CAP_MIN_MAX_ADJUST",objPPGeneralInformation.CAP_MIN_MAX_ADJUST); 
				objDataWrapper.AddParameter("@ACL_PREMIUM",objPPGeneralInformation.ACL_PREMIUM); 									
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPPGeneralInformation.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Application/Aspx/PPGeneralInformation_Additional.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPPGeneralInformation);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objPPGeneralInformation.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objPPGeneralInformation.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPPGeneralInformation.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPPGeneralInformation.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Private passenger underwriting tier is added";
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
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPPGeneralInformation">Model class object.</param>
		/// <returns>No of records effected.</returns>
		//Function added by Charles on 24-Dec-09 for Itrack 6830
		public int Add(Cms.Model.Policy.ClsUnderwritingTierInfo objPPGeneralInformation)
		{
			string		strStoredProc	=	"Proc_Save_POL_UNDERWRITING_TIER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPPGeneralInformation.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objPPGeneralInformation.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objPPGeneralInformation.POLICY_VERSION_ID);
				
				objDataWrapper.AddParameter("@UNDERWRITING_TIER",objPPGeneralInformation.UNDERWRITING_TIER); 
				if(objPPGeneralInformation.UNTIER_ASSIGNED_DATE == DateTime.MinValue)
					objDataWrapper.AddParameter("@UNTIER_ASSIGNED_DATE",null);
				else
					objDataWrapper.AddParameter("@UNTIER_ASSIGNED_DATE",objPPGeneralInformation.UNTIER_ASSIGNED_DATE); 
				objDataWrapper.AddParameter("@CAP_INC",objPPGeneralInformation.CAP_INC); 
				objDataWrapper.AddParameter("@CAP_DEC",objPPGeneralInformation.CAP_DEC); 
				objDataWrapper.AddParameter("@CAP_RATE_CHANGE_REL",objPPGeneralInformation.CAP_RATE_CHANGE_REL); 
				objDataWrapper.AddParameter("@CAP_MIN_MAX_ADJUST",objPPGeneralInformation.CAP_MIN_MAX_ADJUST); 
				objDataWrapper.AddParameter("@ACL_PREMIUM",objPPGeneralInformation.ACL_PREMIUM); 									
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPPGeneralInformation.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Policies/Aspx/PolicyPPGeneralInformation_Additional.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPPGeneralInformation);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objPPGeneralInformation.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objPPGeneralInformation.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPPGeneralInformation.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPPGeneralInformation.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Private passenger underwriting tier is added";
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
		 /// 
		 /// </summary>
		 /// <param name="customer_id"></param>
		 /// <returns></returns>

		public static int CheckExistancePolicy(int customer_id)
		{
			string		strStoredProc	=	"Proc_CheckExistPolicy";
			int retValue=0;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMERID",customer_id);
				DataSet da  =    objDataWrapper.ExecuteDataSet(strStoredProc);
				if(da.Tables.Count > 0 )
				{
					retValue=int.Parse(da.Tables[0].Rows[0][0].ToString());

				}
				return retValue;
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
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMotorCycleInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddMotorCycle(ClsPPGeneralInformationInfo objMotorCycleInfo)
		{
			string		strStoredProc	=	"Proc_InsertAPP_AUTO_GEN_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@APP_ID",objMotorCycleInfo.APP_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMotorCycleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMotorCycleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH",objMotorCycleInfo.ANY_NON_OWNED_VEH);
                
				objDataWrapper.AddParameter("@EXISTING_DMG",objMotorCycleInfo.EXISTING_DMG);
                
                
                
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objMotorCycleInfo.ANY_OTH_INSU_COMP);
                
                
                
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objMotorCycleInfo.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objMotorCycleInfo.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY",objMotorCycleInfo.ANY_FINANCIAL_RESPONSIBILITY);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER",objMotorCycleInfo.INS_AGENCY_TRANSFER);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objMotorCycleInfo.COVERAGE_DECLINED);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED",objMotorCycleInfo.AGENCY_VEH_INSPECTED);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE",objMotorCycleInfo.USE_AS_TRANSPORT_FEE);
				objDataWrapper.AddParameter("@SALVAGE_TITLE",objMotorCycleInfo.SALVAGE_TITLE);
				objDataWrapper.AddParameter("@REMARKS",objMotorCycleInfo.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objMotorCycleInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objMotorCycleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objMotorCycleInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@IS_COMMERCIAL_USE",objMotorCycleInfo.IS_COMMERCIAL_USE);
				objDataWrapper.AddParameter("@IS_USEDFOR_RACING",objMotorCycleInfo.IS_USEDFOR_RACING);
				objDataWrapper.AddParameter("@IS_COST_OVER_DEFINED_LIMIT",objMotorCycleInfo.IS_COST_OVER_DEFINED_LIMIT);
				objDataWrapper.AddParameter("@IS_MORE_WHEELS",objMotorCycleInfo.IS_MORE_WHEELS);
				objDataWrapper.AddParameter("@IS_EXTENDED_FORKS",objMotorCycleInfo.IS_EXTENDED_FORKS);
				objDataWrapper.AddParameter("@IS_LICENSED_FOR_ROAD",objMotorCycleInfo.IS_LICENSED_FOR_ROAD);
				objDataWrapper.AddParameter("@IS_MODIFIED_INCREASE_SPEED",objMotorCycleInfo.IS_MODIFIED_INCREASE_SPEED);
				objDataWrapper.AddParameter("@IS_MODIFIED_KIT",objMotorCycleInfo.IS_MODIFIED_KIT);
				objDataWrapper.AddParameter("@IS_TAKEN_OUT",objMotorCycleInfo.IS_TAKEN_OUT);
				objDataWrapper.AddParameter("@IS_CONVICTED_CARELESS_DRIVE",objMotorCycleInfo.IS_CONVICTED_CARELESS_DRIVE);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objMotorCycleInfo.IS_CONVICTED_ACCIDENT);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objMotorCycleInfo.MULTI_POLICY_DISC_APPLIED );


				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_MC_DESC",objMotorCycleInfo.ANY_NON_OWNED_VEH_MC_DESC );
				objDataWrapper.AddParameter("@EXISTING_DMG_MC_DESC",objMotorCycleInfo.EXISTING_DMG_MC_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_MC_DESC",objMotorCycleInfo.ANY_OTH_INSU_COMP_MC_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_MC_DESC",objMotorCycleInfo.DRIVER_SUS_REVOKED_MC_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_MC_DESC",objMotorCycleInfo.PHY_MENTL_CHALLENGED_MC_DESC );
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_MC_DESC",objMotorCycleInfo.ANY_FINANCIAL_RESPONSIBILITY_MC_DESC);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_MC_DESC",objMotorCycleInfo.INS_AGENCY_TRANSFER_MC_DESC );
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_MC_DESC",objMotorCycleInfo.COVERAGE_DECLINED_MC_DESC);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_MC_DESC",objMotorCycleInfo.AGENCY_VEH_INSPECTED_MC_DESC );
				objDataWrapper.AddParameter("@SALVAGE_TITLE_MC_DESC",objMotorCycleInfo.SALVAGE_TITLE_MC_DESC);
				objDataWrapper.AddParameter("@IS_COMMERCIAL_USE_DESC",objMotorCycleInfo.IS_COMMERCIAL_USE_DESC);
				objDataWrapper.AddParameter("@IS_USEDFOR_RACING_DESC",objMotorCycleInfo.IS_USEDFOR_RACING_DESC );
				objDataWrapper.AddParameter("@IS_COST_OVER_DEFINED_LIMIT_DESC",objMotorCycleInfo.IS_COST_OVER_DEFINED_LIMIT_DESC);
				objDataWrapper.AddParameter("@IS_MORE_WHEELS_DESC",objMotorCycleInfo.IS_MORE_WHEELS_DESC);
				objDataWrapper.AddParameter("@IS_EXTENDED_FORKS_DESC",objMotorCycleInfo.IS_EXTENDED_FORKS_DESC);
				objDataWrapper.AddParameter("@IS_LICENSED_FOR_ROAD_DESC",objMotorCycleInfo.IS_LICENSED_FOR_ROAD_DESC);
				objDataWrapper.AddParameter("@IS_MODIFIED_INCREASE_SPEED_DESC",objMotorCycleInfo.IS_MODIFIED_INCREASE_SPEED_DESC);
				objDataWrapper.AddParameter("@IS_MODIFIED_KIT_DESC",objMotorCycleInfo.IS_MODIFIED_KIT_DESC);
				objDataWrapper.AddParameter("@IS_TAKEN_OUT_DESC",objMotorCycleInfo.IS_TAKEN_OUT_DESC);
				objDataWrapper.AddParameter("@IS_CONVICTED_CARELESS_DRIVE_DESC",objMotorCycleInfo.IS_CONVICTED_CARELESS_DRIVE_DESC );
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT_DESC",objMotorCycleInfo.IS_CONVICTED_ACCIDENT_DESC);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_MC_DESC",objMotorCycleInfo.MULTI_POLICY_DISC_APPLIED_MC_DESC);

				objDataWrapper.AddParameter("@FullName",objMotorCycleInfo.FullName );
				if(objMotorCycleInfo.DATE_OF_BIRTH!= DateTime.MinValue)
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",objMotorCycleInfo.DATE_OF_BIRTH );
				else
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
				objDataWrapper.AddParameter("@DrivingLisence",objMotorCycleInfo.DrivingLisence );
				objDataWrapper.AddParameter("@PolicyNumber",objMotorCycleInfo.PolicyNumber );
				objDataWrapper.AddParameter("@CompanyName",objMotorCycleInfo.CompanyName );
				objDataWrapper.AddParameter("@InsuredElseWhere",objMotorCycleInfo.InsuredElseWhere );

				objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objMotorCycleInfo.IS_OTHER_THAN_INSURED );
				objDataWrapper.AddParameter("@CURR_RES_TYPE",objMotorCycleInfo.CURR_RES_TYPE );
				objDataWrapper.AddParameter("@WhichCycle",objMotorCycleInfo.WhichCycle );

				if(objMotorCycleInfo.YEARS_INSU==0)
				{
					objDataWrapper.AddParameter("@YEARS_INSU",null);
				}
				else
				 objDataWrapper.AddParameter("@YEARS_INSU",objMotorCycleInfo.YEARS_INSU);
				if(objMotorCycleInfo.YEARS_INSU_WOL==0)
				{
					objDataWrapper.AddParameter("@YEARS_INSU_WOL",null);
				}
				else
				  objDataWrapper.AddParameter("@YEARS_INSU_WOL",objMotorCycleInfo.YEARS_INSU_WOL);

				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objMotorCycleInfo.ANY_PRIOR_LOSSES );
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objMotorCycleInfo.ANY_PRIOR_LOSSES_DESC );
				objDataWrapper.AddParameter("@APPLY_PERS_UMB_POL",objMotorCycleInfo.APPLY_PERS_UMB_POL );
				objDataWrapper.AddParameter("@APPLY_PERS_UMB_POL_DESC",objMotorCycleInfo.APPLY_PERS_UMB_POL_DESC );

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objMotorCycleInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/AddMotorCycleGeneralInformationIframe.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMotorCycleInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMotorCycleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMotorCycleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMotorCycleInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMotorCycleInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Motorcycle general information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}                
				objDataWrapper.ClearParameteres();

				//Added By Shafi 03-10-2006
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages("MOTOR");
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objMotorCycleInfo.CUSTOMER_ID,objMotorCycleInfo.APP_ID,objMotorCycleInfo.APP_VERSION_ID,RuleType.UnderWriting);
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                
				if(returnResult == -1)
				{
					return -1;
				}
				else
				{                    
					return returnResult;
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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldPPGeneralInformation">Model object having old information</param>
		/// <param name="objPPGeneralInformation">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPPGeneralInformationInfo objOldPPGeneralInformation,ClsPPGeneralInformationInfo objPPGeneralInformation)
		{
			string		strStoredProc	=	"Proc_InsertPPGeneralInformation";
			
			string strTranXML;
			int returnResult = 0;
			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
					
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPPGeneralInformation.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objPPGeneralInformation.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objPPGeneralInformation.APP_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH",objPPGeneralInformation.ANY_NON_OWNED_VEH);
				objDataWrapper.AddParameter("@CAR_MODIFIED",objPPGeneralInformation.CAR_MODIFIED);
				objDataWrapper.AddParameter("@CAR_MODIFIED_DESC",objPPGeneralInformation.CAR_MODIFIED_DESC);
				objDataWrapper.AddParameter("@EXISTING_DMG",objPPGeneralInformation.EXISTING_DMG);
				objDataWrapper.AddParameter("@EXISTING_DMG_PP_DESC",objPPGeneralInformation.EXISTING_DMG_PP_DESC);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH",objPPGeneralInformation.ANY_CAR_AT_SCH);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH_DESC",objPPGeneralInformation.ANY_CAR_AT_SCH_DESC);
				//objDataWrapper.AddParameter("@SCHOOL_CARS_LIST",objPPGeneralInformation.SCHOOL_CARS_LIST);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU",objPPGeneralInformation.ANY_OTH_AUTO_INSU);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU_DESC",objPPGeneralInformation.ANY_OTH_AUTO_INSU_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objPPGeneralInformation.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_PP_DESC",objPPGeneralInformation.ANY_OTH_INSU_COMP_PP_DESC);
				//objDataWrapper.AddParameter("@OTHER_POLICY_NUMBER_LIST",objPPGeneralInformation.OTHER_POLICY_NUMBER_LIST);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY",objPPGeneralInformation.H_MEM_IN_MILITARY);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_DESC",objPPGeneralInformation.H_MEM_IN_MILITARY_DESC);
				//objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_LIST",objPPGeneralInformation.H_MEM_IN_MILITARY_LIST);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objPPGeneralInformation.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_PP_DESC",objPPGeneralInformation.DRIVER_SUS_REVOKED_PP_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objPPGeneralInformation.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_PP_DESC",objPPGeneralInformation.PHY_MENTL_CHALLENGED_PP_DESC);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_PP_DESC",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY_PP_DESC);
				//objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_PP_DESC",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY_PP_DESC);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER",objPPGeneralInformation.INS_AGENCY_TRANSFER);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_PP_DESC",objPPGeneralInformation.INS_AGENCY_TRANSFER_PP_DESC);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objPPGeneralInformation.COVERAGE_DECLINED);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_PP_DESC",objPPGeneralInformation.COVERAGE_DECLINED_PP_DESC);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED",objPPGeneralInformation.AGENCY_VEH_INSPECTED);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_PP_DESC",objPPGeneralInformation.AGENCY_VEH_INSPECTED_PP_DESC);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE",objPPGeneralInformation.USE_AS_TRANSPORT_FEE);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE_DESC",objPPGeneralInformation.USE_AS_TRANSPORT_FEE_DESC);
				objDataWrapper.AddParameter("@SALVAGE_TITLE",objPPGeneralInformation.SALVAGE_TITLE);
				objDataWrapper.AddParameter("@SALVAGE_TITLE_PP_DESC",objPPGeneralInformation.SALVAGE_TITLE_PP_DESC);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO",objPPGeneralInformation.ANY_ANTIQUE_AUTO);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO_DESC",objPPGeneralInformation.ANY_ANTIQUE_AUTO_DESC);
				//objDataWrapper.AddParameter("@ANTIQUE_AUTO_LIST",objPPGeneralInformation.ANTIQUE_AUTO_LIST);
				objDataWrapper.AddParameter("@REMARKS",objPPGeneralInformation.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objPPGeneralInformation.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",null);
				objDataWrapper.AddParameter("@CREATED_DATETIME",null);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPPGeneralInformation.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPPGeneralInformation.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@INSERTUPDATE","U");
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_PP_DESC",objPPGeneralInformation.ANY_NON_OWNED_VEH_PP_DESC);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED_PP_DESC );
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU));
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objPPGeneralInformation.ANY_PRIOR_LOSSES);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objPPGeneralInformation.ANY_PRIOR_LOSSES_DESC);
				if (objPPGeneralInformation.COST_EQUIPMENT_DESC!=0)
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",objPPGeneralInformation.COST_EQUIPMENT_DESC );
				}
				else
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",null);
				}
				objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
				objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objPPGeneralInformation.IS_OTHER_THAN_INSURED );
				if (objPPGeneralInformation.IS_OTHER_THAN_INSURED == "1")
				{
					objDataWrapper.AddParameter("@FullName",objPPGeneralInformation.FullName );
					if(objPPGeneralInformation.DATE_OF_BIRTH!= DateTime.MinValue)
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",objPPGeneralInformation.DATE_OF_BIRTH );
					else
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
					objDataWrapper.AddParameter("@DrivingLisence",objPPGeneralInformation.DrivingLisence );
					objDataWrapper.AddParameter("@PolicyNumber",objPPGeneralInformation.PolicyNumber );
					objDataWrapper.AddParameter("@CompanyName",objPPGeneralInformation.CompanyName );
					objDataWrapper.AddParameter("@InsuredElseWhere",objPPGeneralInformation.InsuredElseWhere );
					//objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objPPGeneralInformation.IS_OTHER_THAN_INSURED );
					//objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
					objDataWrapper.AddParameter("@WhichCycle",objPPGeneralInformation.WhichCycle );
				}
				else
				{
					objDataWrapper.AddParameter("@FullName",null);
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null);
					objDataWrapper.AddParameter("@DrivingLisence",null);
					objDataWrapper.AddParameter("@WhichCycle",null);
					objDataWrapper.AddParameter("@PolicyNumber",null );
					objDataWrapper.AddParameter("@CompanyName",null );
					objDataWrapper.AddParameter("@InsuredElseWhere",null);
					//objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",null );
					//objDataWrapper.AddParameter("@CURR_RES_TYPE",null );
				
				}

				//					objDataWrapper.AddParameter("@FullName",objPPGeneralInformation.FullName );
				//					if(objPPGeneralInformation.DATE_OF_BIRTH!= DateTime.MinValue)
				//						objDataWrapper.AddParameter("@DATE_OF_BIRTH",objPPGeneralInformation.DATE_OF_BIRTH );
				//					else
				//						objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
				//					objDataWrapper.AddParameter("@DrivingLisence",objPPGeneralInformation.DrivingLisence );
				//					objDataWrapper.AddParameter("@PolicyNumber",objPPGeneralInformation.PolicyNumber );
				//					objDataWrapper.AddParameter("@CompanyName",objPPGeneralInformation.CompanyName );
				//					objDataWrapper.AddParameter("@InsuredElseWhere",objPPGeneralInformation.InsuredElseWhere );
				//					objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objPPGeneralInformation.IS_OTHER_THAN_INSURED );
				//					objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
				//					objDataWrapper.AddParameter("@WhichCycle",objPPGeneralInformation.WhichCycle );

					

				if(TransactionLogRequired) 
				{						
					
					objPPGeneralInformation.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Application/Aspx/PPGeneralInformationIframe.aspx.resx");Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldPPGeneralInformation,objPPGeneralInformation);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{							
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID = objPPGeneralInformation.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objPPGeneralInformation.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objPPGeneralInformation.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objPPGeneralInformation.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Private passenger underwriting questions is modified";
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
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldPPGeneralInformation">Model object having old information</param>
		/// <param name="objPPGeneralInformation">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		//Function added by Charles on 24-Dec-09 for Itrack 6830
		public int Update(Cms.Model.Application.ClsUnderwritingTierInfo objOldPPGeneralInformation,Cms.Model.Application.ClsUnderwritingTierInfo objPPGeneralInformation)
		{
			string		strStoredProc	=	"Proc_Save_APP_UNDERWRITING_TIER";
			
			string strTranXML;
			int returnResult = 0;
			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
					
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPPGeneralInformation.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objPPGeneralInformation.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objPPGeneralInformation.APP_VERSION_ID);

				objDataWrapper.AddParameter("@UNDERWRITING_TIER",objPPGeneralInformation.UNDERWRITING_TIER);
				if(objPPGeneralInformation.UNTIER_ASSIGNED_DATE == DateTime.MinValue)
					objDataWrapper.AddParameter("@UNTIER_ASSIGNED_DATE",null);
				else
					objDataWrapper.AddParameter("@UNTIER_ASSIGNED_DATE",objPPGeneralInformation.UNTIER_ASSIGNED_DATE);
				objDataWrapper.AddParameter("@CAP_INC",objPPGeneralInformation.CAP_INC);
				objDataWrapper.AddParameter("@CAP_DEC",objPPGeneralInformation.CAP_DEC);
				objDataWrapper.AddParameter("@CAP_RATE_CHANGE_REL",objPPGeneralInformation.CAP_RATE_CHANGE_REL);
				objDataWrapper.AddParameter("@CAP_MIN_MAX_ADJUST",objPPGeneralInformation.CAP_MIN_MAX_ADJUST);
				objDataWrapper.AddParameter("@ACL_PREMIUM",objPPGeneralInformation.ACL_PREMIUM);				
				
				if(TransactionLogRequired) 
				{						
					
					objPPGeneralInformation.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Application/Aspx/PPGeneralInformation_Additional.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldPPGeneralInformation,objPPGeneralInformation);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{							
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID = objPPGeneralInformation.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objPPGeneralInformation.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objPPGeneralInformation.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objPPGeneralInformation.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Private passenger underwriting tier is modified";
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
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldPPGeneralInformation">Model object having old information</param>
		/// <param name="objPPGeneralInformation">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		//Function added by Charles on 24-Dec-09 for Itrack 6830
		public int Update(Cms.Model.Policy.ClsUnderwritingTierInfo objOldPPGeneralInformation,Cms.Model.Policy.ClsUnderwritingTierInfo objPPGeneralInformation)
		{
			string		strStoredProc	=	"Proc_Save_POL_UNDERWRITING_TIER";
			
			string strTranXML;
			int returnResult = 0;
			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{					
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPPGeneralInformation.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objPPGeneralInformation.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objPPGeneralInformation.POLICY_VERSION_ID);

				objDataWrapper.AddParameter("@UNDERWRITING_TIER",objPPGeneralInformation.UNDERWRITING_TIER);
				if(objPPGeneralInformation.UNTIER_ASSIGNED_DATE == DateTime.MinValue)
					objDataWrapper.AddParameter("@UNTIER_ASSIGNED_DATE",null);
				else
					objDataWrapper.AddParameter("@UNTIER_ASSIGNED_DATE",objPPGeneralInformation.UNTIER_ASSIGNED_DATE);
				objDataWrapper.AddParameter("@CAP_INC",objPPGeneralInformation.CAP_INC);
				objDataWrapper.AddParameter("@CAP_DEC",objPPGeneralInformation.CAP_DEC);
				objDataWrapper.AddParameter("@CAP_RATE_CHANGE_REL",objPPGeneralInformation.CAP_RATE_CHANGE_REL);
				objDataWrapper.AddParameter("@CAP_MIN_MAX_ADJUST",objPPGeneralInformation.CAP_MIN_MAX_ADJUST);
				objDataWrapper.AddParameter("@ACL_PREMIUM",objPPGeneralInformation.ACL_PREMIUM);				
				
				if(TransactionLogRequired) 
				{				
					objPPGeneralInformation.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Policies/Aspx/PolicyPPGeneralInformation_Additional.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldPPGeneralInformation,objPPGeneralInformation);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{							
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.POLICY_ID = objPPGeneralInformation.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objPPGeneralInformation.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objPPGeneralInformation.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objPPGeneralInformation.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Private passenger underwriting tier is modified";
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
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldMotorCycleInfo">Model object having old information</param>
		/// <param name="objMotorCycleInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateMotorCycle(ClsPPGeneralInformationInfo objOldMotorCycleInfo,ClsPPGeneralInformationInfo objMotorCycleInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string strStoredProc="Proc_UpdateAPP_AUTO_GEN_INFO";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_ID",objMotorCycleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMotorCycleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMotorCycleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH",objMotorCycleInfo.ANY_NON_OWNED_VEH);
                
				objDataWrapper.AddParameter("@EXISTING_DMG",objMotorCycleInfo.EXISTING_DMG);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objMotorCycleInfo.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objMotorCycleInfo.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objMotorCycleInfo.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY",objMotorCycleInfo.ANY_FINANCIAL_RESPONSIBILITY);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER",objMotorCycleInfo.INS_AGENCY_TRANSFER);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objMotorCycleInfo.COVERAGE_DECLINED);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED",objMotorCycleInfo.AGENCY_VEH_INSPECTED);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE",objMotorCycleInfo.USE_AS_TRANSPORT_FEE);
				objDataWrapper.AddParameter("@SALVAGE_TITLE",objMotorCycleInfo.SALVAGE_TITLE);
				objDataWrapper.AddParameter("@REMARKS",objMotorCycleInfo.REMARKS);
				objDataWrapper.AddParameter("@MODIFIED_BY",objMotorCycleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objMotorCycleInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@IS_COMMERCIAL_USE",objMotorCycleInfo.IS_COMMERCIAL_USE);
				objDataWrapper.AddParameter("@IS_USEDFOR_RACING",objMotorCycleInfo.IS_USEDFOR_RACING);
				objDataWrapper.AddParameter("@IS_COST_OVER_DEFINED_LIMIT",objMotorCycleInfo.IS_COST_OVER_DEFINED_LIMIT);
				objDataWrapper.AddParameter("@IS_MORE_WHEELS",objMotorCycleInfo.IS_MORE_WHEELS);
				objDataWrapper.AddParameter("@IS_EXTENDED_FORKS",objMotorCycleInfo.IS_EXTENDED_FORKS);
				objDataWrapper.AddParameter("@IS_LICENSED_FOR_ROAD",objMotorCycleInfo.IS_LICENSED_FOR_ROAD);
				objDataWrapper.AddParameter("@IS_MODIFIED_INCREASE_SPEED",objMotorCycleInfo.IS_MODIFIED_INCREASE_SPEED);
				objDataWrapper.AddParameter("@IS_MODIFIED_KIT",objMotorCycleInfo.IS_MODIFIED_KIT);
				objDataWrapper.AddParameter("@IS_TAKEN_OUT",objMotorCycleInfo.IS_TAKEN_OUT);
				objDataWrapper.AddParameter("@IS_CONVICTED_CARELESS_DRIVE",objMotorCycleInfo.IS_CONVICTED_CARELESS_DRIVE);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objMotorCycleInfo.IS_CONVICTED_ACCIDENT);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objMotorCycleInfo.MULTI_POLICY_DISC_APPLIED);

				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_MC_DESC",objMotorCycleInfo.ANY_NON_OWNED_VEH_MC_DESC );
				objDataWrapper.AddParameter("@EXISTING_DMG_MC_DESC",objMotorCycleInfo.EXISTING_DMG_MC_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_MC_DESC",objMotorCycleInfo.ANY_OTH_INSU_COMP_MC_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_MC_DESC",objMotorCycleInfo.DRIVER_SUS_REVOKED_MC_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_MC_DESC",objMotorCycleInfo.PHY_MENTL_CHALLENGED_MC_DESC );
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_MC_DESC",objMotorCycleInfo.ANY_FINANCIAL_RESPONSIBILITY_MC_DESC);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_MC_DESC",objMotorCycleInfo.INS_AGENCY_TRANSFER_MC_DESC );
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_MC_DESC",objMotorCycleInfo.COVERAGE_DECLINED_MC_DESC);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_MC_DESC",objMotorCycleInfo.AGENCY_VEH_INSPECTED_MC_DESC );
				objDataWrapper.AddParameter("@SALVAGE_TITLE_MC_DESC",objMotorCycleInfo.SALVAGE_TITLE_MC_DESC);
				objDataWrapper.AddParameter("@IS_COMMERCIAL_USE_DESC",objMotorCycleInfo.IS_COMMERCIAL_USE_DESC);
				objDataWrapper.AddParameter("@IS_USEDFOR_RACING_DESC",objMotorCycleInfo.IS_USEDFOR_RACING_DESC );
				objDataWrapper.AddParameter("@IS_COST_OVER_DEFINED_LIMIT_DESC",objMotorCycleInfo.IS_COST_OVER_DEFINED_LIMIT_DESC);
				objDataWrapper.AddParameter("@IS_MORE_WHEELS_DESC",objMotorCycleInfo.IS_MORE_WHEELS_DESC);
				objDataWrapper.AddParameter("@IS_EXTENDED_FORKS_DESC",objMotorCycleInfo.IS_EXTENDED_FORKS_DESC);
				objDataWrapper.AddParameter("@IS_LICENSED_FOR_ROAD_DESC",objMotorCycleInfo.IS_LICENSED_FOR_ROAD_DESC);
				objDataWrapper.AddParameter("@IS_MODIFIED_INCREASE_SPEED_DESC",objMotorCycleInfo.IS_MODIFIED_INCREASE_SPEED_DESC);
				objDataWrapper.AddParameter("@IS_MODIFIED_KIT_DESC",objMotorCycleInfo.IS_MODIFIED_KIT_DESC);
				objDataWrapper.AddParameter("@IS_TAKEN_OUT_DESC",objMotorCycleInfo.IS_TAKEN_OUT_DESC);
				objDataWrapper.AddParameter("@IS_CONVICTED_CARELESS_DRIVE_DESC",objMotorCycleInfo.IS_CONVICTED_CARELESS_DRIVE_DESC );
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT_DESC",objMotorCycleInfo.IS_CONVICTED_ACCIDENT_DESC);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_MC_DESC",objMotorCycleInfo.MULTI_POLICY_DISC_APPLIED_MC_DESC);
				
				
				objDataWrapper.AddParameter("@PolicyNumber",objMotorCycleInfo.PolicyNumber );
				objDataWrapper.AddParameter("@CompanyName",objMotorCycleInfo.CompanyName );
				objDataWrapper.AddParameter("@InsuredElseWhere",objMotorCycleInfo.InsuredElseWhere );
				if(objMotorCycleInfo.YEARS_INSU==0)
				{
					 objDataWrapper.AddParameter("@YEARS_INSU",null);
				}
					else

				      objDataWrapper.AddParameter("@YEARS_INSU",objMotorCycleInfo.YEARS_INSU);
				if(objMotorCycleInfo.YEARS_INSU_WOL==0)
				{
					objDataWrapper.AddParameter("@YEARS_INSU_WOL",null);
				}
				else
				  objDataWrapper.AddParameter("@YEARS_INSU_WOL",objMotorCycleInfo.YEARS_INSU_WOL);
				
				objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objMotorCycleInfo.IS_OTHER_THAN_INSURED );
				
				if (objMotorCycleInfo.IS_OTHER_THAN_INSURED == "1")
				{
					objDataWrapper.AddParameter("@FullName",objMotorCycleInfo.FullName );
					if(objMotorCycleInfo.DATE_OF_BIRTH!= DateTime.MinValue)
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",objMotorCycleInfo.DATE_OF_BIRTH );
					else
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
					objDataWrapper.AddParameter("@DrivingLisence",objMotorCycleInfo.DrivingLisence );
					objDataWrapper.AddParameter("@WhichCycle",objMotorCycleInfo.WhichCycle );
				}
				else
				{
					objDataWrapper.AddParameter("@FullName",null);
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null);
					objDataWrapper.AddParameter("@DrivingLisence",null);
					objDataWrapper.AddParameter("@WhichCycle",null);			
				
				}
				
				objDataWrapper.AddParameter("@CURR_RES_TYPE",objMotorCycleInfo.CURR_RES_TYPE );
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objMotorCycleInfo.ANY_PRIOR_LOSSES );
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objMotorCycleInfo.ANY_PRIOR_LOSSES_DESC );
				objDataWrapper.AddParameter("@APPLY_PERS_UMB_POL",objMotorCycleInfo.APPLY_PERS_UMB_POL );
				objDataWrapper.AddParameter("@APPLY_PERS_UMB_POL_DESC",objMotorCycleInfo.APPLY_PERS_UMB_POL_DESC );
				

				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objMotorCycleInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/AddMotorCycleGeneralInformationIframe.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMotorCycleInfo,objMotorCycleInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID = objMotorCycleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objMotorCycleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMotorCycleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMotorCycleInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Motorcycle general information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				//Added By Shafi 03-10-2006
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages("MOTOR");
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objMotorCycleInfo.CUSTOMER_ID,objMotorCycleInfo.APP_ID,objMotorCycleInfo.APP_VERSION_ID,RuleType.UnderWriting);

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
		/// This function is used to generate XML based on the Customer Id, Application Id and Application Version Id
		/// </summary>
		/// <param name="intCustId">Customer Id</param>
		/// <param name="intAppId">Application Id</param>
		/// <param name="intAppVersionId">Application Version Id</param>
		/// <returns></returns>
		public static string GetXml(int intCustId, int intAppId, int intAppVersionId)
		{
			//string sql= "Select * from APP_AUTO_GEN_INFO where CUSTOMER_ID="+intCustId+" and APP_ID="+intAppId+" and APP_VERSION_ID="+intAppVersionId+"";
			string strXml="";
			string		strStoredProc	=	"Proc_FetchPPGenInfo";
			DataSet dsCount=null;
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@APP_ID",intAppId,SqlDbType.Int);
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId,SqlDbType.Int);
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsCount.Tables[0].Rows.Count == 0)
				{
					strXml ="";
				}
				else
				{
					//strXml = dsCount.GetXml().ToString();
					strXml	= ClsCommon.GetXMLEncoded(dsCount.Tables[0]);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(dsCount!=null)
					dsCount.Dispose();
			}
			return strXml;


		}

		/// <summary>
		/// This function is used to generate XML based on the Customer Id, Application Id and Application Version Id
		/// </summary>
		/// <param name="intCustId">Customer Id</param>
		/// <param name="intAppId">Application Id</param>
		/// <param name="intAppVersionId">Application Version Id</param>
		/// <returns></returns>
		//Function added by Charles on 23-Dec-09 for Itrack 6830
		public static string GetXml_UW_Tier(int intCustId, int intAppId, int intAppVersionId)
		{
			string strXml="";
			string	strStoredProc	=	"Proc_FetchAPP_UNDERWRITING_TIER";
			DataSet dsCount=null;
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",intAppId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId,SqlDbType.Int);
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsCount.Tables[0].Rows.Count == 0)
				{
					strXml ="";
				}
				else
				{
					strXml	= ClsCommon.GetXMLEncoded(dsCount.Tables[0]);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(dsCount!=null)
					dsCount.Dispose();
			}
			return strXml;
		}

		public int CheckForAmount(int appId,int customerId,int appVersionId)
		{
			string strStoredProc ="Proc_FetchMotorGreaterAmount";
			DataSet dsCount=null;
			try
			{
    
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
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
			if(dsCount.Tables[0].Rows.Count>0)
				return 1;
			else
				return 0;
		}

		/*public int CheckForAmountPolicy(int customerId,int policyId,int policyVersionId)
		{
			string strStoredProc ="Proc_FetchMotorGreaterAmountPolicy";
			int intAmount=Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings.Get("MAX_AMOUNT").ToString());
			DataSet dsCount=null;
			try
			{
    
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@MAXAMOUNT",intAmount);
                        
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
        			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
        				
			}
			if(dsCount.Tables[0].Rows.Count>0)
				return 1;
			else
				return 0;
		}*/
		#region FETCHING DATA
		public DataSet FetchData(int appId,int customerId,int appVersionId)
		{
			string		strStoredProc	=	"Proc_FetchMotorCycleGenInfo";
			DataSet dsCount=null;
			try
			{
    
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
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
		#endregion

		/// <summary>
		/// For fetching the Motor Cycles.
		/// </summary>
		/// <param name="cust_id"></param>
		/// <param name="app_id"></param>
		/// <param name="app_ver_id"></param>
		/// <returns></returns>
		#region
		public static DataTable GetMotorCycle(int cust_id,int app_id,int app_ver_id)
		{
			string		strStoredProc	=	"Proc_GetMotorCycle";
			DataSet ds=new DataSet();
			try
			{
    
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@APP_ID",app_id,SqlDbType.Int);
				objDataWrapper.AddParameter("@CUSTOMER_ID",cust_id,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",app_ver_id,SqlDbType.Int);
                ds = objDataWrapper.ExecuteDataSet(strStoredProc);
        		return ds.Tables[0];	
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
        				
			}	
		}
		#endregion

		#region Policy Functions
		/// <summary>
		/// This function is used to generate XML based on the Customer Id, Policy Id and Policy Version Id
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <returns></returns>
		public static string GetPolicyXml(int customerID, int policyID, int policyVersionID)
		{
			string strXml="";
			string		strStoredProc	=	"Proc_GetPolicyGeneralInformation";
			DataSet dsCount=null;
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID,SqlDbType.Int);
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsCount.Tables[0].Rows.Count == 0)
				{
					strXml ="";
				}
				else
				{
					strXml = dsCount.GetXml().ToString();
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(dsCount!=null)
					dsCount.Dispose();
			}
			return strXml;
		}

		/// <summary>
		/// This function is used to generate XML based on the Customer Id, Policy Id and Policy Version Id
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <returns></returns>
		//Function added by Charles on 24-Dec-09 for Itrack 6830
		public static string GetPolicyXml_UW_Tier(int customerID, int policyID, int policyVersionID)
		{
			string strXml="";
			string		strStoredProc	=	"Proc_GetPolicyGeneralInformationUN_Tier";
			DataSet dsCount=null;
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID,SqlDbType.Int);
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsCount.Tables[0].Rows.Count == 0)
				{
					strXml ="";
				}
				else
				{
					strXml = dsCount.GetXml().ToString();
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(dsCount!=null)
					dsCount.Dispose();
			}
			return strXml;
		}


		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPPGeneralInformation">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddPolicyGeneralInformation(ClsPolicyGeneralInfo objPPGeneralInformation)
		{
			string		strStoredProc	=	"Proc_InsertPolicyGeneralInformation";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPPGeneralInformation.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objPPGeneralInformation.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objPPGeneralInformation.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH",objPPGeneralInformation.ANY_NON_OWNED_VEH);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_PP_DESC",objPPGeneralInformation.ANY_NON_OWNED_VEH_PP_DESC);
				objDataWrapper.AddParameter("@CAR_MODIFIED",objPPGeneralInformation.CAR_MODIFIED);
				objDataWrapper.AddParameter("@CAR_MODIFIED_DESC",objPPGeneralInformation.CAR_MODIFIED_DESC);
				objDataWrapper.AddParameter("@EXISTING_DMG",objPPGeneralInformation.EXISTING_DMG);
				objDataWrapper.AddParameter("@EXISTING_DMG_PP_DESC",objPPGeneralInformation.EXISTING_DMG_PP_DESC);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH",objPPGeneralInformation.ANY_CAR_AT_SCH);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH_DESC",objPPGeneralInformation.ANY_CAR_AT_SCH_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU",objPPGeneralInformation.ANY_OTH_AUTO_INSU);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU_DESC",objPPGeneralInformation.ANY_OTH_AUTO_INSU_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objPPGeneralInformation.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_PP_DESC",objPPGeneralInformation.ANY_OTH_INSU_COMP_PP_DESC);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY",objPPGeneralInformation.H_MEM_IN_MILITARY);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_DESC",objPPGeneralInformation.H_MEM_IN_MILITARY_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objPPGeneralInformation.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_PP_DESC",objPPGeneralInformation.DRIVER_SUS_REVOKED_PP_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objPPGeneralInformation.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_PP_DESC",objPPGeneralInformation.PHY_MENTL_CHALLENGED_PP_DESC);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_PP_DESC",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY_PP_DESC);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER",objPPGeneralInformation.INS_AGENCY_TRANSFER);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_PP_DESC",objPPGeneralInformation.INS_AGENCY_TRANSFER_PP_DESC);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objPPGeneralInformation.COVERAGE_DECLINED);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_PP_DESC",objPPGeneralInformation.COVERAGE_DECLINED_PP_DESC);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED",objPPGeneralInformation.AGENCY_VEH_INSPECTED);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_PP_DESC",objPPGeneralInformation.AGENCY_VEH_INSPECTED_PP_DESC);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE",objPPGeneralInformation.USE_AS_TRANSPORT_FEE);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE_DESC",objPPGeneralInformation.USE_AS_TRANSPORT_FEE_DESC);
				objDataWrapper.AddParameter("@SALVAGE_TITLE",objPPGeneralInformation.SALVAGE_TITLE);
				objDataWrapper.AddParameter("@SALVAGE_TITLE_PP_DESC",objPPGeneralInformation.SALVAGE_TITLE_PP_DESC);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO",objPPGeneralInformation.ANY_ANTIQUE_AUTO);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO_DESC",objPPGeneralInformation.ANY_ANTIQUE_AUTO_DESC);
				objDataWrapper.AddParameter("@REMARKS",objPPGeneralInformation.REMARKS);
                objDataWrapper.AddParameter("@POLICY_DESCRIPTION", objPPGeneralInformation.POLICY_DESCRIPTION);
				objDataWrapper.AddParameter("@IS_ACTIVE",objPPGeneralInformation.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objPPGeneralInformation.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objPPGeneralInformation.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED );
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED_PP_DESC );
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objPPGeneralInformation.ANY_PRIOR_LOSSES);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objPPGeneralInformation.ANY_PRIOR_LOSSES_DESC);
				

				//added By Shafi 17-01-2006
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU));
				
				
				if (objPPGeneralInformation.COST_EQUIPMENT_DESC != "0")
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",objPPGeneralInformation.COST_EQUIPMENT_DESC );
				}
				else
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",null);
				}

				objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
				//objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
				objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objPPGeneralInformation.IS_OTHER_THAN_INSURED);
				if (objPPGeneralInformation.IS_OTHER_THAN_INSURED == "1")
				{
					objDataWrapper.AddParameter("@FULLNAME",objPPGeneralInformation.FullName);
					if(objPPGeneralInformation.DATE_OF_BIRTH!= DateTime.MinValue)
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",objPPGeneralInformation.DATE_OF_BIRTH );
					else
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );

					objDataWrapper.AddParameter("@DRIVINGLISENCE",objPPGeneralInformation.DrivingLisence );
					objDataWrapper.AddParameter("@POLICYNUMBER",objPPGeneralInformation.PolicyNumber );
					objDataWrapper.AddParameter("@COMPANYNAME",objPPGeneralInformation.CompanyName );
					objDataWrapper.AddParameter("@INSUREDELSEWHERE",objPPGeneralInformation.InsuredElseWhere );
					//objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objPPGeneralInformation.IS_OTHER_THAN_INSURED );
					//objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
					objDataWrapper.AddParameter("@WHICHCYCLE",objPPGeneralInformation.WhichCycle );
				}
				else
				{
					objDataWrapper.AddParameter("@FULLNAME",null);
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
					objDataWrapper.AddParameter("@DRIVINGLISENCE",null);
					objDataWrapper.AddParameter("@POLICYNUMBER",null);
					objDataWrapper.AddParameter("@COMPANYNAME",null);
					objDataWrapper.AddParameter("@INSUREDELSEWHERE",null);
					//objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",null);
					//objDataWrapper.AddParameter("@CURR_RES_TYPE",null);
					objDataWrapper.AddParameter("@WHICHCYCLE",null);
				
				}

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPPGeneralInformation.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Policies/Aspx/Automobile/PolicyAutoGeneralInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPPGeneralInformation);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objPPGeneralInformation.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objPPGeneralInformation.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPPGeneralInformation.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPPGeneralInformation.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1523", "");// "Policy Private passenger general information is added";
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
		/// Updates the General Information of Policy.
		/// </summary>
		/// <param name="objOldPPGeneralInformation"></param>
		/// <param name="objPPGeneralInformation"></param>
		/// <returns></returns>
		
		public int UpdatePolicyGeneralInformation(ClsPolicyGeneralInfo objOldPPGeneralInformation,ClsPolicyGeneralInfo objPPGeneralInformation)
		{
			string		strStoredProc	=	"Proc_UpdatePolicyGeneralInformation";
			
			string strTranXML;
			int returnResult = 0;
			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPPGeneralInformation.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objPPGeneralInformation.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objPPGeneralInformation.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH",objPPGeneralInformation.ANY_NON_OWNED_VEH);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_PP_DESC",objPPGeneralInformation.ANY_NON_OWNED_VEH_PP_DESC);
				objDataWrapper.AddParameter("@CAR_MODIFIED",objPPGeneralInformation.CAR_MODIFIED);
				objDataWrapper.AddParameter("@CAR_MODIFIED_DESC",objPPGeneralInformation.CAR_MODIFIED_DESC);
				objDataWrapper.AddParameter("@EXISTING_DMG",objPPGeneralInformation.EXISTING_DMG);
				objDataWrapper.AddParameter("@EXISTING_DMG_PP_DESC",objPPGeneralInformation.EXISTING_DMG_PP_DESC);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH",objPPGeneralInformation.ANY_CAR_AT_SCH);
				objDataWrapper.AddParameter("@ANY_CAR_AT_SCH_DESC",objPPGeneralInformation.ANY_CAR_AT_SCH_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU",objPPGeneralInformation.ANY_OTH_AUTO_INSU);
				objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU_DESC",objPPGeneralInformation.ANY_OTH_AUTO_INSU_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objPPGeneralInformation.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_PP_DESC",objPPGeneralInformation.ANY_OTH_INSU_COMP_PP_DESC);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY",objPPGeneralInformation.H_MEM_IN_MILITARY);
				objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_DESC",objPPGeneralInformation.H_MEM_IN_MILITARY_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objPPGeneralInformation.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_PP_DESC",objPPGeneralInformation.DRIVER_SUS_REVOKED_PP_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objPPGeneralInformation.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_PP_DESC",objPPGeneralInformation.PHY_MENTL_CHALLENGED_PP_DESC);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_PP_DESC",objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY_PP_DESC);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER",objPPGeneralInformation.INS_AGENCY_TRANSFER);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_PP_DESC",objPPGeneralInformation.INS_AGENCY_TRANSFER_PP_DESC);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objPPGeneralInformation.COVERAGE_DECLINED);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_PP_DESC",objPPGeneralInformation.COVERAGE_DECLINED_PP_DESC);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED",objPPGeneralInformation.AGENCY_VEH_INSPECTED);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_PP_DESC",objPPGeneralInformation.AGENCY_VEH_INSPECTED_PP_DESC);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE",objPPGeneralInformation.USE_AS_TRANSPORT_FEE);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE_DESC",objPPGeneralInformation.USE_AS_TRANSPORT_FEE_DESC);
				objDataWrapper.AddParameter("@SALVAGE_TITLE",objPPGeneralInformation.SALVAGE_TITLE);
				objDataWrapper.AddParameter("@SALVAGE_TITLE_PP_DESC",objPPGeneralInformation.SALVAGE_TITLE_PP_DESC);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO",objPPGeneralInformation.ANY_ANTIQUE_AUTO);
				objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO_DESC",objPPGeneralInformation.ANY_ANTIQUE_AUTO_DESC);
				objDataWrapper.AddParameter("@REMARKS",objPPGeneralInformation.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objPPGeneralInformation.IS_ACTIVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPPGeneralInformation.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPPGeneralInformation.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED );
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED_PP_DESC );
				//added By Shafi 17-01-2006
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU));
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objPPGeneralInformation.ANY_PRIOR_LOSSES);
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objPPGeneralInformation.ANY_PRIOR_LOSSES_DESC);
				
				
				if (objPPGeneralInformation.COST_EQUIPMENT_DESC != "0")
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",objPPGeneralInformation.COST_EQUIPMENT_DESC );
				}
				else
				{
					objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC",null);
				}

				objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
				objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objPPGeneralInformation.IS_OTHER_THAN_INSURED );
				if (objPPGeneralInformation.IS_OTHER_THAN_INSURED == "1")
				{
					objDataWrapper.AddParameter("@FULLNAME",objPPGeneralInformation.FullName);
					if(objPPGeneralInformation.DATE_OF_BIRTH!= DateTime.MinValue)
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",objPPGeneralInformation.DATE_OF_BIRTH );
					else
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );

					objDataWrapper.AddParameter("@DRIVINGLISENCE",objPPGeneralInformation.DrivingLisence );
					objDataWrapper.AddParameter("@POLICYNUMBER",objPPGeneralInformation.PolicyNumber );
					objDataWrapper.AddParameter("@COMPANYNAME",objPPGeneralInformation.CompanyName );
					objDataWrapper.AddParameter("@INSUREDELSEWHERE",objPPGeneralInformation.InsuredElseWhere );
					//objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objPPGeneralInformation.IS_OTHER_THAN_INSURED );
					//objDataWrapper.AddParameter("@CURR_RES_TYPE",objPPGeneralInformation.CURR_RES_TYPE );
					objDataWrapper.AddParameter("@WHICHCYCLE",objPPGeneralInformation.WhichCycle );
				}
				else
				{
					objDataWrapper.AddParameter("@FULLNAME",null);
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
					objDataWrapper.AddParameter("@DRIVINGLISENCE",null);
					objDataWrapper.AddParameter("@POLICYNUMBER",null);
					objDataWrapper.AddParameter("@COMPANYNAME",null);
					objDataWrapper.AddParameter("@INSUREDELSEWHERE",null);
					//objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",null);
					//objDataWrapper.AddParameter("@CURR_RES_TYPE",null);
					objDataWrapper.AddParameter("@WHICHCYCLE",null);
				
				}	
				

				if(TransactionLogRequired) 
				{	
					objPPGeneralInformation.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("/Policies/Aspx/Automobile/PolicyAutoGeneralInformation.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldPPGeneralInformation,objPPGeneralInformation);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.POLICY_ID = objPPGeneralInformation.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objPPGeneralInformation.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objPPGeneralInformation.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objPPGeneralInformation.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1504", "");//"Private passenger general information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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
		/// Get the policy general information for MotorCycle LOB.
		/// </summary>
		/// <param name="polId"></param>
		/// <param name="customerId"></param>
		/// <param name="polVersionId"></param>
		/// <returns></returns>
		public DataSet FetchPolicyMotorGenInfoData(int polId,int customerId,int polVersionId)
		{
			string		strStoredProc	=	"Proc_FetchPolicyMotorCycleGenInfo";
			DataSet dsCount=null;
			try
			{    
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@POLICY_ID",polId,SqlDbType.Int);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionId,SqlDbType.Int);                        
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

			/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMotorCycleInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddPolicyMotorCycleGeneralInformation(ClsPolicyGeneralInfo objMotorCycleInfo)
		{
			string		strStoredProc	=	"Proc_InsertPOLICY_AUTO_GEN_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@POLICY_ID",objMotorCycleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMotorCycleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMotorCycleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH",objMotorCycleInfo.ANY_NON_OWNED_VEH);                
				objDataWrapper.AddParameter("@EXISTING_DMG",objMotorCycleInfo.EXISTING_DMG);
                objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objMotorCycleInfo.ANY_OTH_INSU_COMP);       
                objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objMotorCycleInfo.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objMotorCycleInfo.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY",objMotorCycleInfo.ANY_FINANCIAL_RESPONSIBILITY);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER",objMotorCycleInfo.INS_AGENCY_TRANSFER);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objMotorCycleInfo.COVERAGE_DECLINED);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED",objMotorCycleInfo.AGENCY_VEH_INSPECTED);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE",objMotorCycleInfo.USE_AS_TRANSPORT_FEE);
				objDataWrapper.AddParameter("@SALVAGE_TITLE",objMotorCycleInfo.SALVAGE_TITLE);
				objDataWrapper.AddParameter("@REMARKS",objMotorCycleInfo.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objMotorCycleInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objMotorCycleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objMotorCycleInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@IS_COMMERCIAL_USE",objMotorCycleInfo.IS_COMMERCIAL_USE);
				objDataWrapper.AddParameter("@IS_USEDFOR_RACING",objMotorCycleInfo.IS_USEDFOR_RACING);
				objDataWrapper.AddParameter("@IS_COST_OVER_DEFINED_LIMIT",objMotorCycleInfo.IS_COST_OVER_DEFINED_LIMIT);
				objDataWrapper.AddParameter("@IS_MORE_WHEELS",objMotorCycleInfo.IS_MORE_WHEELS);
				objDataWrapper.AddParameter("@IS_EXTENDED_FORKS",objMotorCycleInfo.IS_EXTENDED_FORKS);
				objDataWrapper.AddParameter("@IS_LICENSED_FOR_ROAD",objMotorCycleInfo.IS_LICENSED_FOR_ROAD);
				objDataWrapper.AddParameter("@IS_MODIFIED_INCREASE_SPEED",objMotorCycleInfo.IS_MODIFIED_INCREASE_SPEED);
				objDataWrapper.AddParameter("@IS_MODIFIED_KIT",objMotorCycleInfo.IS_MODIFIED_KIT);
				objDataWrapper.AddParameter("@IS_TAKEN_OUT",objMotorCycleInfo.IS_TAKEN_OUT);
				objDataWrapper.AddParameter("@IS_CONVICTED_CARELESS_DRIVE",objMotorCycleInfo.IS_CONVICTED_CARELESS_DRIVE);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objMotorCycleInfo.IS_CONVICTED_ACCIDENT);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objMotorCycleInfo.MULTI_POLICY_DISC_APPLIED );
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_MC_DESC",objMotorCycleInfo.ANY_NON_OWNED_VEH_MC_DESC );
				objDataWrapper.AddParameter("@EXISTING_DMG_MC_DESC",objMotorCycleInfo.EXISTING_DMG_MC_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_MC_DESC",objMotorCycleInfo.ANY_OTH_INSU_COMP_MC_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_MC_DESC",objMotorCycleInfo.DRIVER_SUS_REVOKED_MC_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_MC_DESC",objMotorCycleInfo.PHY_MENTL_CHALLENGED_MC_DESC );
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_MC_DESC",objMotorCycleInfo.ANY_FINANCIAL_RESPONSIBILITY_MC_DESC);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_MC_DESC",objMotorCycleInfo.INS_AGENCY_TRANSFER_MC_DESC );
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_MC_DESC",objMotorCycleInfo.COVERAGE_DECLINED_MC_DESC);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_MC_DESC",objMotorCycleInfo.AGENCY_VEH_INSPECTED_MC_DESC );
				objDataWrapper.AddParameter("@SALVAGE_TITLE_MC_DESC",objMotorCycleInfo.SALVAGE_TITLE_MC_DESC);
				objDataWrapper.AddParameter("@IS_COMMERCIAL_USE_DESC",objMotorCycleInfo.IS_COMMERCIAL_USE_DESC);
				objDataWrapper.AddParameter("@IS_USEDFOR_RACING_DESC",objMotorCycleInfo.IS_USEDFOR_RACING_DESC );
				objDataWrapper.AddParameter("@IS_COST_OVER_DEFINED_LIMIT_DESC",objMotorCycleInfo.IS_COST_OVER_DEFINED_LIMIT_DESC);
				objDataWrapper.AddParameter("@IS_MORE_WHEELS_DESC",objMotorCycleInfo.IS_MORE_WHEELS_DESC);
				objDataWrapper.AddParameter("@IS_EXTENDED_FORKS_DESC",objMotorCycleInfo.IS_EXTENDED_FORKS_DESC);
				objDataWrapper.AddParameter("@IS_LICENSED_FOR_ROAD_DESC",objMotorCycleInfo.IS_LICENSED_FOR_ROAD_DESC);
				objDataWrapper.AddParameter("@IS_MODIFIED_INCREASE_SPEED_DESC",objMotorCycleInfo.IS_MODIFIED_INCREASE_SPEED_DESC);
				objDataWrapper.AddParameter("@IS_MODIFIED_KIT_DESC",objMotorCycleInfo.IS_MODIFIED_KIT_DESC);
				objDataWrapper.AddParameter("@IS_TAKEN_OUT_DESC",objMotorCycleInfo.IS_TAKEN_OUT_DESC);
				objDataWrapper.AddParameter("@IS_CONVICTED_CARELESS_DRIVE_DESC",objMotorCycleInfo.IS_CONVICTED_CARELESS_DRIVE_DESC );
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT_DESC",objMotorCycleInfo.IS_CONVICTED_ACCIDENT_DESC);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_MC_DESC",objMotorCycleInfo.MULTI_POLICY_DISC_APPLIED_MC_DESC);
				objDataWrapper.AddParameter("@FullName",objMotorCycleInfo.FullName );
				if(objMotorCycleInfo.DATE_OF_BIRTH!= DateTime.MinValue)
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",objMotorCycleInfo.DATE_OF_BIRTH );
				else
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
				objDataWrapper.AddParameter("@DrivingLisence",objMotorCycleInfo.DrivingLisence );
				objDataWrapper.AddParameter("@PolicyNumber",objMotorCycleInfo.PolicyNumber );
				objDataWrapper.AddParameter("@CompanyName",objMotorCycleInfo.CompanyName );
				objDataWrapper.AddParameter("@InsuredElseWhere",objMotorCycleInfo.InsuredElseWhere );

				objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objMotorCycleInfo.IS_OTHER_THAN_INSURED );
				objDataWrapper.AddParameter("@CURR_RES_TYPE",objMotorCycleInfo.CURR_RES_TYPE );
				objDataWrapper.AddParameter("@WhichCycle",objMotorCycleInfo.WhichCycle );

				//added By Shafi 16-01-2006
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objMotorCycleInfo.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objMotorCycleInfo.YEARS_INSU));
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objMotorCycleInfo.ANY_PRIOR_LOSSES );
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objMotorCycleInfo.ANY_PRIOR_LOSSES_DESC );
				objDataWrapper.AddParameter("@APPLY_PERS_UMB_POL",objMotorCycleInfo.APPLY_PERS_UMB_POL );
				objDataWrapper.AddParameter("@APPLY_PERS_UMB_POL_DESC",objMotorCycleInfo.APPLY_PERS_UMB_POL_DESC );

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objMotorCycleInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"Policies\Aspx\Motorcycle\PolicyMotorcycleGeneralInformationIframe.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMotorCycleInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objMotorCycleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objMotorCycleInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMotorCycleInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMotorCycleInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Policy Motorcycle general information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}                
				objDataWrapper.ClearParameteres();
				//Added By Shafi 03-10-2006
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages("MOTOR");
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objMotorCycleInfo.CUSTOMER_ID,objMotorCycleInfo.POLICY_ID,objMotorCycleInfo.POLICY_VERSION_ID,RuleType.UnderWriting);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);                
				if(returnResult == -1)
				{
					return -1;
				}
				else
				{                    
					return returnResult;
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


		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldMotorCycleInfo">Model object having old information</param>
		/// <param name="objMotorCycleInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicyMotorCycleGenInformation(ClsPolicyGeneralInfo objOldMotorCycleInfo,ClsPolicyGeneralInfo objMotorCycleInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string strStoredProc="Proc_UpdatePOLICY_AUTO_GEN_INFO";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@POLICY_ID",objMotorCycleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMotorCycleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMotorCycleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH",objMotorCycleInfo.ANY_NON_OWNED_VEH);                
				objDataWrapper.AddParameter("@EXISTING_DMG",objMotorCycleInfo.EXISTING_DMG);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objMotorCycleInfo.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objMotorCycleInfo.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objMotorCycleInfo.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY",objMotorCycleInfo.ANY_FINANCIAL_RESPONSIBILITY);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER",objMotorCycleInfo.INS_AGENCY_TRANSFER);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objMotorCycleInfo.COVERAGE_DECLINED);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED",objMotorCycleInfo.AGENCY_VEH_INSPECTED);
				objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE",objMotorCycleInfo.USE_AS_TRANSPORT_FEE);
				objDataWrapper.AddParameter("@SALVAGE_TITLE",objMotorCycleInfo.SALVAGE_TITLE);
				objDataWrapper.AddParameter("@REMARKS",objMotorCycleInfo.REMARKS);
				objDataWrapper.AddParameter("@MODIFIED_BY",objMotorCycleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objMotorCycleInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@IS_COMMERCIAL_USE",objMotorCycleInfo.IS_COMMERCIAL_USE);
				objDataWrapper.AddParameter("@IS_USEDFOR_RACING",objMotorCycleInfo.IS_USEDFOR_RACING);
				objDataWrapper.AddParameter("@IS_COST_OVER_DEFINED_LIMIT",objMotorCycleInfo.IS_COST_OVER_DEFINED_LIMIT);
				objDataWrapper.AddParameter("@IS_MORE_WHEELS",objMotorCycleInfo.IS_MORE_WHEELS);
				objDataWrapper.AddParameter("@IS_EXTENDED_FORKS",objMotorCycleInfo.IS_EXTENDED_FORKS);
				objDataWrapper.AddParameter("@IS_LICENSED_FOR_ROAD",objMotorCycleInfo.IS_LICENSED_FOR_ROAD);
				objDataWrapper.AddParameter("@IS_MODIFIED_INCREASE_SPEED",objMotorCycleInfo.IS_MODIFIED_INCREASE_SPEED);
				objDataWrapper.AddParameter("@IS_MODIFIED_KIT",objMotorCycleInfo.IS_MODIFIED_KIT);
				objDataWrapper.AddParameter("@IS_TAKEN_OUT",objMotorCycleInfo.IS_TAKEN_OUT);
				objDataWrapper.AddParameter("@IS_CONVICTED_CARELESS_DRIVE",objMotorCycleInfo.IS_CONVICTED_CARELESS_DRIVE);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objMotorCycleInfo.IS_CONVICTED_ACCIDENT);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objMotorCycleInfo.MULTI_POLICY_DISC_APPLIED);

				objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_MC_DESC",objMotorCycleInfo.ANY_NON_OWNED_VEH_MC_DESC );
				objDataWrapper.AddParameter("@EXISTING_DMG_MC_DESC",objMotorCycleInfo.EXISTING_DMG_MC_DESC);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_MC_DESC",objMotorCycleInfo.ANY_OTH_INSU_COMP_MC_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_MC_DESC",objMotorCycleInfo.DRIVER_SUS_REVOKED_MC_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_MC_DESC",objMotorCycleInfo.PHY_MENTL_CHALLENGED_MC_DESC );
				objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_MC_DESC",objMotorCycleInfo.ANY_FINANCIAL_RESPONSIBILITY_MC_DESC);
				objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_MC_DESC",objMotorCycleInfo.INS_AGENCY_TRANSFER_MC_DESC );
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_MC_DESC",objMotorCycleInfo.COVERAGE_DECLINED_MC_DESC);
				objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_MC_DESC",objMotorCycleInfo.AGENCY_VEH_INSPECTED_MC_DESC );
				objDataWrapper.AddParameter("@SALVAGE_TITLE_MC_DESC",objMotorCycleInfo.SALVAGE_TITLE_MC_DESC);
				objDataWrapper.AddParameter("@IS_COMMERCIAL_USE_DESC",objMotorCycleInfo.IS_COMMERCIAL_USE_DESC);
				objDataWrapper.AddParameter("@IS_USEDFOR_RACING_DESC",objMotorCycleInfo.IS_USEDFOR_RACING_DESC );
				objDataWrapper.AddParameter("@IS_COST_OVER_DEFINED_LIMIT_DESC",objMotorCycleInfo.IS_COST_OVER_DEFINED_LIMIT_DESC);
				objDataWrapper.AddParameter("@IS_MORE_WHEELS_DESC",objMotorCycleInfo.IS_MORE_WHEELS_DESC);
				objDataWrapper.AddParameter("@IS_EXTENDED_FORKS_DESC",objMotorCycleInfo.IS_EXTENDED_FORKS_DESC);
				objDataWrapper.AddParameter("@IS_LICENSED_FOR_ROAD_DESC",objMotorCycleInfo.IS_LICENSED_FOR_ROAD_DESC);
				objDataWrapper.AddParameter("@IS_MODIFIED_INCREASE_SPEED_DESC",objMotorCycleInfo.IS_MODIFIED_INCREASE_SPEED_DESC);
				objDataWrapper.AddParameter("@IS_MODIFIED_KIT_DESC",objMotorCycleInfo.IS_MODIFIED_KIT_DESC);
				objDataWrapper.AddParameter("@IS_TAKEN_OUT_DESC",objMotorCycleInfo.IS_TAKEN_OUT_DESC);
				objDataWrapper.AddParameter("@IS_CONVICTED_CARELESS_DRIVE_DESC",objMotorCycleInfo.IS_CONVICTED_CARELESS_DRIVE_DESC );
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT_DESC",objMotorCycleInfo.IS_CONVICTED_ACCIDENT_DESC);
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_MC_DESC",objMotorCycleInfo.MULTI_POLICY_DISC_APPLIED_MC_DESC);
				objDataWrapper.AddParameter("@PolicyNumber",objMotorCycleInfo.PolicyNumber );
				objDataWrapper.AddParameter("@CompanyName",objMotorCycleInfo.CompanyName );
				objDataWrapper.AddParameter("@InsuredElseWhere",objMotorCycleInfo.InsuredElseWhere );
				objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED",objMotorCycleInfo.IS_OTHER_THAN_INSURED );
                if (objMotorCycleInfo.IS_OTHER_THAN_INSURED == "1")
				{
					objDataWrapper.AddParameter("@FullName",objMotorCycleInfo.FullName );
					if(objMotorCycleInfo.DATE_OF_BIRTH!= DateTime.MinValue)
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",objMotorCycleInfo.DATE_OF_BIRTH );
					else
						objDataWrapper.AddParameter("@DATE_OF_BIRTH",null );
					objDataWrapper.AddParameter("@DrivingLisence",objMotorCycleInfo.DrivingLisence );
					objDataWrapper.AddParameter("@WhichCycle",objMotorCycleInfo.WhichCycle );
				}
				else
				{
					objDataWrapper.AddParameter("@FullName",null);
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",null);
					objDataWrapper.AddParameter("@DrivingLisence",null);
					objDataWrapper.AddParameter("@WhichCycle",null);			
				
				}
				
				objDataWrapper.AddParameter("@CURR_RES_TYPE",objMotorCycleInfo.CURR_RES_TYPE );
				//added By Shafi 16-01-2006
				objDataWrapper.AddParameter("@YEARS_INSU_WOL",DefaultValues.GetIntNull(objMotorCycleInfo.YEARS_INSU_WOL));
				objDataWrapper.AddParameter("@YEARS_INSU",DefaultValues.GetIntNull(objMotorCycleInfo.YEARS_INSU));

				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES",objMotorCycleInfo.ANY_PRIOR_LOSSES );
				objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC",objMotorCycleInfo.ANY_PRIOR_LOSSES_DESC );
				objDataWrapper.AddParameter("@APPLY_PERS_UMB_POL",objMotorCycleInfo.APPLY_PERS_UMB_POL );
				objDataWrapper.AddParameter("@APPLY_PERS_UMB_POL_DESC",objMotorCycleInfo.APPLY_PERS_UMB_POL_DESC );


				

				if(TransactionLogRequired) 
				{					
					objMotorCycleInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"Policies\Aspx\Motorcycle\PolicyMotorcycleGeneralInformationIframe.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldMotorCycleInfo,objMotorCycleInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.POLICY_ID = objMotorCycleInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objMotorCycleInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMotorCycleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMotorCycleInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Policy Motorcycle general information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				//Added By Shafi 03-10-2006
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages("MOTOR");
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objMotorCycleInfo.CUSTOMER_ID,objMotorCycleInfo.POLICY_ID,objMotorCycleInfo.POLICY_VERSION_ID,RuleType.UnderWriting);

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
		/// Gets the details of the Policy motorcycle for MotorCycle LOB.
		/// </summary>
		/// <param name="cust_id"></param>
		/// <param name="pol_id"></param>
		/// <param name="pol_ver_id"></param>
		/// <returns></returns>
		public static DataTable GetPolicyMotorCycle(int cust_id,int pol_id,int pol_ver_id)
		{
			string		strStoredProc	=	"Proc_GetPolicyMotorCycle";
			DataSet ds=new DataSet();
			try
			{
    
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@POLICY_ID",pol_id,SqlDbType.Int);
				objDataWrapper.AddParameter("@CUSTOMER_ID",cust_id,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",pol_ver_id,SqlDbType.Int);
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				return ds.Tables[0];	
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
        				
			}	
		}
		#endregion
	}   
}
