/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	4/28/2005 5:07:51 PM
<End Date				: -	
<Description				: - 	Business layer class for AddDriverDetail page.
<Review Date				: - 
<Reviewed By			: - 	

Modification History
<Modified Date			: - 03/05/2005
<Modified By			: - Pradeep Iyer	
<Purpose				: - Corrected the order of Transaction XML call in Add and Update

<Modified Date			: - 13/05/2005
<Modified By			: - nidhi
<Purpose				: - Added two more functions for Add and Update of motorcycle driver

<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified

<Modified Date			: - Anurag Verma
<Modified By			: - Sep 22, 2005
<Purpose				: - removing and adding parameters from AddWatercraftOperator method and UpdateWatercraftOperator method

<Modified Date			: - Sumit Chhabra
<Modified By			: - Oct 07, 2005
<Purpose				: - Added another parameter safe_driver_renewal_discount for appUmbrellaDriverDetails

<Modified Date           : - 15/10/2005
<Modified By             : - Mohit Gupta
<Purpose                 : - Added overladed method for GetDriverCount()

<Modified Date           : - 15/10/2005
<Modified By             : - Mohit Gupta
<Purpose                 : - Added overladed method for DeleteDriver()

<Modified Date           : - 17/10/2005
<Modified By             : - Vijay Arora
<Purpose                 : - Add a parameter in Add Function for principal / occasional assigned vehicle

<Modified Date           : - 07-11-2005
<Modified By             : - Vijay Arora
<Purpose                 : - Write the Policy Functions

<Modified Date			: - 11/11/2005
<Modified By			: - Pawan Papreja
<Purpose				: - Corrected driver/operator changes

<Modified Date			: - 24/11/2005
<Modified By			: - Vijay Arora
<Purpose				: - Added the Policy WaterCraft Functions.

<Modified Date			: - 08/12/2005
<Modified By			: - Praveen kasana
<Purpose				: - Added Method to Gets Boat Ids against one application for rule implementation(Boat Operators)

<Modified Date			: - 23/1/2006
<Modified By			: - Nidhi
<Purpose				: - Added a field for no of dependants in Add function 

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
//using Cms.Model.Application.Watercrafts;
using Cms.Model.Policy;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Business Logic class for add Driver details.
	/// </summary>
	public class ClsDriverDetail : Cms.BusinessLayer.BlApplication.clsapplication
	{
		private const string APP_DRIVER_DETAILS = "APP_DRIVER_DETAILS";
		//private const string INERT_CUSTOMER_DRIVER_DETAILS = "Proc_InsertCustomerDriverDetails";
		private const string INERT_APP_DRIVER_DETAILS = "Proc_InsertAppDriverDetails";
		private const string INSERT_UMBRELLA_DRIVER_DETAILS = "Proc_InsertUmbrellaDriverDetails";
		//private const string INSERT_WATERCRAFT_OPERATOR_DETAILS = "Proc_InsertWDriverDetails";
		private const string INSERT_UMBRELLA_OPERATOR_DETAILS = "Proc_InsertUmbrellaOperatorDetails";

		//private const	string		GET_CUSTOMER_DRIVER_DETAILS_PROC		=	"Proc_GetCustomerDriverDetails";
		private const	string		GET_APP_DRIVER_DETAILS_PROC		=	"Proc_GetAppDriverDetails";
		private const string GET_UMB_DRIVER_DETAILS_PROC = "Proc_GetUmbrellaDriverDetails";

		private const	string		GET_POL_DRIVER_DETAILS_PROC		=	"Proc_GetPolDriversDetails";
		
		//ClsCommon objCommon=new ClsCommon();
		

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _DRIVER_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateDriverDetails";
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

		#region AddParameters

		/// <summary>
		/// Add the parameters of proc from model object.
		/// </summary>
		/// <param name="objPriorPolicyInfo">Model class object.</param>
		/// <param name="objDatawrapper">Datawrapper class object.</param>
		/// <returns>void</returns>
		private void AddParameters(Cms.Model.Application.ClsDriverDetailsInfo objDriverDetailsInfo, DataWrapper objDataWrapper,char InsertUpdate)
		{
			objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
			objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
			objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
			objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
			objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
			objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
			objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
			objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
			objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
			objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
			objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
			objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
			objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
			objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
			objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);

			if (objDriverDetailsInfo.DRIVER_DOB != DateTime.MinValue)
				objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
			else
				objDataWrapper.AddParameter("@DRIVER_DOB",null);

			objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
			objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
			objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
			objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",DefaultValues.GetStringNull(objDriverDetailsInfo.DRIVER_DRIV_LIC));
			objDataWrapper.AddParameter("@DRIVER_LIC_STATE",DefaultValues.GetStringNull(objDriverDetailsInfo.DRIVER_LIC_STATE));
			
			objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			
			//objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",null);
			
			if (objDriverDetailsInfo.DATE_LICENSED != DateTime.MinValue)
				objDataWrapper.AddParameter("@DATE_LICENSED",objDriverDetailsInfo.DATE_LICENSED);
			else
				objDataWrapper.AddParameter("@DATE_LICENSED",null);

			objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
			//objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);

			
			// ADDED BY PRAVEEN KUMAR On 17-12-2008
			objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",DefaultValues.GetStringNull(objDriverDetailsInfo.DRIVER_DRIV_TYPE));

		    
			//objDataWrapper.AddParameter("@DRIVER_OCC_CODE",null);
			objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
			
			objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
			objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
			objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
			if(objDriverDetailsInfo.DRIVER_INCOME==0)
			{
				objDataWrapper.AddParameter("@DRIVER_INCOME",null);
			}
			else

				objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);
			//objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
			objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
			objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",DefaultValues.GetStringNull(objDriverDetailsInfo.DRIVER_DRINK_VIOLATION));
			objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
			objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
			objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
			objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
			objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
			objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
			objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);

			objDataWrapper.AddParameter("@INSERTUPDATE",InsertUpdate.ToString());
		}
		#endregion

		#endregion

		#region Constructors
		/// <summary>
		/// 		/// deafault constructor
		/// </summary>
		public ClsDriverDetail()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDriverDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/// 

		public int Add(ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom)
		{
			return AddAppDriverDetails(objDriverDetailsInfo,strCalledFrom);
		}
		public int Add(ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom,string strCustomInfo)
		{			
			return AddAppDriverDetails(objDriverDetailsInfo,strCalledFrom,strCustomInfo);
				
		}
		 
		#endregion		
		
		public int SaveDriverDetailsAcord(ClsDriverDetailsInfo objDriverDetailsInfo,DataWrapper objDataWrapper)
		{
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			
			//AddParameters(objDriverDetailsInfo,objDataWrapper,'I');
				
			objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
			objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
			objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
			objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
			objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
			objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
			objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
			objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
			objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
			objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
			objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
			objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
			objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
			objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
			objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);
			
			//adding for saving driver Violations 
			objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);


			if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
				objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
			else
				objDataWrapper.AddParameter("@DRIVER_DOB",null);

			objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
			objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
			objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
			objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
			objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);
			
			objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			
			//objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",null);			

			objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
			objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
		    
			//objDataWrapper.AddParameter("@DRIVER_OCC_CODE",null);
			objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
			
			objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
			objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
			objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
			if(objDriverDetailsInfo.DRIVER_INCOME==0)
			{
				objDataWrapper.AddParameter("@DRIVER_INCOME",null);
			}
			else

				objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);
			//objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
			objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
			objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
			objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
			objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
			objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
			objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
			objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
			objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
			objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);

			objDataWrapper.AddParameter("@INSERTUPDATE","I");

			objDataWrapper.AddParameter("@RELATIONSHIP_CODE",objDriverDetailsInfo.RELATIONSHIP_CODE);
			
			objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);

			objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);

			objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
			objDataWrapper.AddParameter("@MODIFIED_BY",null);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",RecordDate);
			objDataWrapper.AddParameter("@DATE_LICENSED",DefaultValues.GetDateNull(objDriverDetailsInfo.DATE_LICENSED));
			objDataWrapper.AddParameter("@SAFE_DRIVER",null);
			objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLEID);
			objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID));
			objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);	
			objDataWrapper.AddParameter ("@NO_DEPENDENTS", objDriverDetailsInfo.NO_DEPENDENTS);
			objDataWrapper.AddParameter ("@WAIVER_WORK_LOSS_BENEFITS", objDriverDetailsInfo.WAIVER_WORK_LOSS_BENEFITS);
			objDataWrapper.AddParameter ("@COLL_STUD_AWAY_HOME", objDriverDetailsInfo.COLL_STUD_AWAY_HOME);
			objDataWrapper.AddParameter ("@CYCL_WITH_YOU", objDriverDetailsInfo.CYCL_WITH_YOU );
		
			//No Cycle Endorsement :  27 feb 2006
			objDataWrapper.AddParameter ("@NO_CYCLE_ENDMT", objDriverDetailsInfo.NO_CYCLE_ENDMT);

			//


			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@NEW_DRIVER",SqlDbType.Int,ParameterDirection.Output);
				
			//objSqlParameter.Value = objDriverDetailsInfo.DRIVER_ID;

			int returnResult = 0;
				
			returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertAppDriverDetails_ACORD");
				
			int DRIVER_ID = -1;
				
			if ( objSqlParameter.Value != System.DBNull.Value )
			{
				DRIVER_ID = Convert.ToInt32(objSqlParameter.Value);
			}
				
			objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
				
			//Transaction log
			if(TransactionLogRequired)
			{
				objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
				objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1593", "");// "New driver is added from Quick Quote";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
			}
			//Update Vehicle Class 
			//UpdateVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
			return DRIVER_ID;
			
		}
		public int SaveDriverDetailsCapitalAcord(ClsDriverDetailsInfo objDriverDetailsInfo,DataWrapper objDataWrapper)
		{
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			
			//AddParameters(objDriverDetailsInfo,objDataWrapper,'I');
				
			objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
			objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
			objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
			objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
			objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
			objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
			objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
			objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
			objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
			objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
			objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
			objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
			objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
			objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
			objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);
			
			//adding for saving driver Violations 
			objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);


			if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
				objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
			else
				objDataWrapper.AddParameter("@DRIVER_DOB",null);

			objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
			objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
			objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
			objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
			objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);
			
			objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			
			//objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",null);			

			objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
			objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
		    
			//objDataWrapper.AddParameter("@DRIVER_OCC_CODE",null);
			objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
			
			objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
			objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
			objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
			if(objDriverDetailsInfo.DRIVER_INCOME==0)
			{
				objDataWrapper.AddParameter("@DRIVER_INCOME",null);
			}
			else

				objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);
			//objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
			objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
			objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
			objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
			objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
			objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
			objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
			objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
			objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
			objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);

			objDataWrapper.AddParameter("@INSERTUPDATE","I");

			objDataWrapper.AddParameter("@RELATIONSHIP_CODE",objDriverDetailsInfo.RELATIONSHIP_CODE);
			
			objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);

			objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);

			objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
			objDataWrapper.AddParameter("@MODIFIED_BY",null);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",RecordDate);
			objDataWrapper.AddParameter("@DATE_LICENSED",DefaultValues.GetDateNull(objDriverDetailsInfo.DATE_LICENSED));
			objDataWrapper.AddParameter("@SAFE_DRIVER",null);
			objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLEID);
			objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID));
			objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);	
			objDataWrapper.AddParameter ("@NO_DEPENDENTS", objDriverDetailsInfo.NO_DEPENDENTS);
			objDataWrapper.AddParameter ("@WAIVER_WORK_LOSS_BENEFITS", objDriverDetailsInfo.WAIVER_WORK_LOSS_BENEFITS);
			objDataWrapper.AddParameter ("@COLL_STUD_AWAY_HOME", objDriverDetailsInfo.COLL_STUD_AWAY_HOME);
			objDataWrapper.AddParameter ("@CYCL_WITH_YOU", objDriverDetailsInfo.CYCL_WITH_YOU );
		
			//No Cycle Endorsement :  27 feb 2006
			objDataWrapper.AddParameter ("@NO_CYCLE_ENDMT", objDriverDetailsInfo.NO_CYCLE_ENDMT);

			//


			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@NEW_DRIVER",SqlDbType.Int,ParameterDirection.Output);
				
			//objSqlParameter.Value = objDriverDetailsInfo.DRIVER_ID;

			int returnResult = 0;
				
			returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertAppDriverDetailsRealTime_ACORD");
				
			int DRIVER_ID = -1;
				
			if ( objSqlParameter.Value != System.DBNull.Value )
			{
				DRIVER_ID = Convert.ToInt32(objSqlParameter.Value);
			}
				
			objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
			string sbStrXml="";
			if(DRIVER_ID!=-1)
			{
				//objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
				
				//AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo,strCalledFrom);
				DataTable dtVehicle =new DataTable();
				objDataWrapper.ClearParameteres();
				dtVehicle=ClsVehicleInformation.GetAppVehicle(objDataWrapper,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDriverDetailsInfo.DRIVER_ID);
					 
				AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo, "PPA",dtVehicle,ref sbStrXml);
					
			}				
			//Transaction log
			if(TransactionLogRequired)
			{
				objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
				objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1593", "");//"New driver is added from Quick Quote";
				objTransactionInfo.CHANGE_XML		=	"<root>" + strTranXML + sbStrXml + "</root>";
				//objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
			}
			//Update Vehicle Class 
			UpdateVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
			return DRIVER_ID;
			
		}
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDriverDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/// 
		public int AddAppDriverDetails(ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
			
				AddParameters(objDriverDetailsInfo,objDataWrapper,'I');
				
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);

				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@SAFE_DRIVER",objDriverDetailsInfo.SAFE_DRIVER);
				//// Added By anurag Verma on 15/09/2005 for merging screens
				objDataWrapper.AddParameter("@Good_Driver_Student_Discount",objDriverDetailsInfo.Good_Driver_Student_Discount);
				objDataWrapper.AddParameter("@Premier_Driver_Discount",objDriverDetailsInfo.Premier_Driver_Discount);
				if (objDriverDetailsInfo.VEHICLE_ID != null)
					objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				else
					objDataWrapper.AddParameter("@VEHICLE_ID",0);

				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);

				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom); 
				//added by vj on 17-10-2005 for principal / occasional assigned vehicle
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				//added By Shafi
				if(strCalledFrom.ToString()=="PPA")
					objDataWrapper.AddParameter("@NO_DEPENDENTS",DefaultValues.GetIntNull(objDriverDetailsInfo.NO_DEPENDENTS));

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1500", "");//"New driver is added";
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='RELATIONSHIP' and @NewValue='0']","NewValue","null");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS ,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS );
				}
				
				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				
						
				///Update Endorsements based on Driver attributes/////////////////////////////////////////////
				this.UpdateDriverEndorsements(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				/////////////////////////////////////////////////////////////////////////////////////////////
				

				//Updating the vehicle class 
				/*UpdateVehicleClass(objDataWrapper, objDriverDetailsInfo.APP_ID, objDriverDetailsInfo.APP_VERSION_ID
					, objDriverDetailsInfo.CUSTOMER_ID, objDriverDetailsInfo.DRIVER_ID
					, int.Parse(objDriverDetailsInfo.VEHICLEID));*/
								
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (DRIVER_ID == -1)
				{
					return -1;
				}
				else
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
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

		public int AddAppDriverDetails(ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom,string strCustomInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				

				AddParameters(objDriverDetailsInfo,objDataWrapper,'I');
				
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);

				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@SAFE_DRIVER",objDriverDetailsInfo.SAFE_DRIVER);
				//// Added By anurag Verma on 15/09/2005 for merging screens
				objDataWrapper.AddParameter("@Good_Driver_Student_Discount",objDriverDetailsInfo.Good_Driver_Student_Discount);
				objDataWrapper.AddParameter("@Premier_Driver_Discount",objDriverDetailsInfo.Premier_Driver_Discount);
				//objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLEID);
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);

				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				if (objDriverDetailsInfo.VEHICLE_ID != null)
					objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				else
					objDataWrapper.AddParameter("@VEHICLE_ID",0);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom); 
				//added by vj on 17-10-2005 for principal / occasional assigned vehicle
				if(objDriverDetailsInfo.IN_MILITARY.ToString() =="10963" && objDriverDetailsInfo.STATIONED_IN_US_TERR.ToString() =="10964")
				{
					objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",""); 
				}
				else
				{
					objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				}
				if(strCalledFrom =="PPA")
					objDataWrapper.AddParameter("@NO_DEPENDENTS",objDriverDetailsInfo.NO_DEPENDENTS);
				objDataWrapper.AddParameter("@WAIVER_WORK_LOSS_BENEFITS",objDriverDetailsInfo.WAIVER_WORK_LOSS_BENEFITS);
				objDataWrapper.AddParameter("@FORM_F95",objDriverDetailsInfo.FORM_F95); 
				objDataWrapper.AddParameter("@EXT_NON_OWN_COVG_INDIVI",objDriverDetailsInfo.EXT_NON_OWN_COVG_INDIVI); 
				objDataWrapper.AddParameter("@HAVE_CAR",objDriverDetailsInfo.HAVE_CAR);
				objDataWrapper.AddParameter("@STATIONED_IN_US_TERR",objDriverDetailsInfo.STATIONED_IN_US_TERR); 
				objDataWrapper.AddParameter("@IN_MILITARY",objDriverDetailsInfo.IN_MILITARY);  
				objDataWrapper.AddParameter("@FULL_TIME_STUDENT",objDriverDetailsInfo.FULL_TIME_STUDENT);  
				objDataWrapper.AddParameter("@SUPPORT_DOCUMENT",objDriverDetailsInfo.SUPPORT_DOCUMENT);  
				objDataWrapper.AddParameter("@SIGNED_WAIVER_BENEFITS_FORM",objDriverDetailsInfo.SIGNED_WAIVER_BENEFITS_FORM);  
				objDataWrapper.AddParameter("@PARENTS_INSURANCE",objDriverDetailsInfo.PARENTS_INSURANCE);  

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
//				if(TransactionLogRequired)
//				{
//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
//					
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
//					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
//					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
//					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
//					objTransactionInfo.TRANS_DESC		=	"New driver is added";
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='RELATIONSHIP' and @NewValue='0']","NewValue","null");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
//					objTransactionInfo.CHANGE_XML		=	strTranXML;
//					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
//					//Executing the query
//					returnResult	= objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS ,objTransactionInfo);
//				}
//				else
//				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS );
//				}
				
				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());

				objDataWrapper.ClearParameteres();
				//				objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
				//				objDataWrapper.AddParameter("@AppID",objDriverDetailsInfo.APP_ID);
				//				objDataWrapper.AddParameter("@AppVersionID",objDriverDetailsInfo.APP_VERSION_ID);
				//				objDataWrapper.AddParameter("@DriverID",objDriverDetailsInfo.DRIVER_ID);
				//				objDataWrapper.ExecuteNonQuery("Proc_DeleteAssignedVehicle");				
				//				objDataWrapper.ClearParameteres();
				//
				//				if(objDriverDetailsInfo.ASSIGNED_VEHICLE!="")
				//				{
				//					foreach (string str in objDriverDetailsInfo.ASSIGNED_VEHICLE.Split('|'))
				//					{
				//						objDataWrapper.ClearParameteres();
				//						objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
				//						objDataWrapper.AddParameter("@AppID",objDriverDetailsInfo.APP_ID);
				//						objDataWrapper.AddParameter("@AppVersionID",objDriverDetailsInfo.APP_VERSION_ID);
				//						objDataWrapper.AddParameter("@DriverID",DRIVER_ID);
				//						objDataWrapper.AddParameter("@VehID",str.Split('~')[0]);
				//						objDataWrapper.AddParameter("@PrinOccID",str.Split('~')[1]);					
				//						returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertAssignedVehicle");
				//					}
				//				}
				if(DRIVER_ID!=-1)
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
					string sbStrXml="";
					//AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo,strCalledFrom);
					DataTable dtVehicle =new DataTable();
					dtVehicle=ClsVehicleInformation.GetAppVehicle(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDriverDetailsInfo.DRIVER_ID);
					 
					AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo, strCalledFrom,dtVehicle,ref sbStrXml);
					
					if(TransactionLogRequired)
					{
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='RELATIONSHIP' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_ID']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
						strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
						objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1500", ""); //"New driver is added";
						
						
						//strTranXML = strTranXML.Remove("</LabelFieldMapping>","");
						//strTranXML.Append(sbStrXml);
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						//Executing the query
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
					}
				}

				
				objDataWrapper.ClearParameteres();
				//Updating the vehicle class 
				/*UpdateVehicleClass(objDataWrapper, objDriverDetailsInfo.APP_ID, objDriverDetailsInfo.APP_VERSION_ID
					, objDriverDetailsInfo.CUSTOMER_ID, objDriverDetailsInfo.DRIVER_ID
					, int.Parse(objDriverDetailsInfo.VEHICLEID));*/
				
				//Update Driver Endorsements/////////////
				
				//this.UpdateDriverEndorsements(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.AutoDriverDep);
				///////////////////////

				
				
				if (DRIVER_ID == -1)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
				else
				{
					//objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
					//Update the Vehicle Class : 16 May 2006
					//UpdateVehicleClass(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID ,objDriverDetailsInfo.APP_VERSION_ID);
					if(returnResult>0)
						UpdateVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID, objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
					//End Update the Vehicle Class
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
				
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

		////		#region FETCH INPUT XML FOR DRIVER DISCOUNT
		//		public static void FetchInputXMLforDriverDiscount(int customerID, int appID, int appVersionID,int vehicleID)
		//		{
		//			string strXML= "" ;			
		//
		//			//Fetch inputXMl from the Rating Proc
		//			strXML = ClsGeneralInformation.FetchDriverApplicationInputXML(customerID,appID,appVersionID);
		//			XmlDocument AppDoc = new XmlDocument();
		//			try
		//			{
		//				//Load XMLDoc from Rating Procs
		//				AppDoc.LoadXml(strXML);
		//				
		//				//Get policy Nodes
		//				XmlNode AppNode = AppDoc.SelectSingleNode("QUICKQUOTE/POLICY");
		//				string strPolicy = AppNode.InnerXml;
		//			}
		//			catch(Exception exc)
		//			{
		//				throw(exc);	
		//			}
		//			finally
		//			{}
		//			return strClassXML;
		//		}
		//		#endregion	

		#region Vehicle Class : 15 may 2006 
		//Fetch the Input XML from the Rating proc : 15 may 2006
		public string FetchInputXMLforClassForApp(int customerID, int appID, int appVersionID,int vehicleID)
		{
			string strXML= "" ,	strDriverClassInfo="",strDrivers ="",strClassXML="";			

			//Fetch inputXMl from the Rating Proc
			strXML = ClsGeneralInformation.FetchPrivatePassengerInputXML(customerID,appID,appVersionID);
			XmlDocument AppDoc = new XmlDocument();
			try
			{
				//Load XMLDoc from Rating Procs
				AppDoc.LoadXml(strXML);
				
				//Get policy Nodes
				XmlNode AppNode = AppDoc.SelectSingleNode("QUICKQUOTE/POLICY");
				string strPolicy = AppNode.InnerXml;
			
				//loop to fetch all drivers who have this vehicle assigned.
				foreach(XmlNode DriverNode in AppDoc.SelectNodes("//DRIVERS/*"))
				{
					string assignedVehicle =DriverNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString()==""?"0":DriverNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString();
					if(Convert.ToInt32(assignedVehicle) == vehicleID)
					{
						strDriverClassInfo = strDriverClassInfo + "<DRIVER>" +  DriverNode.InnerXml + "</DRIVER>";
					}
				}
				//Get Driver Nodes				
				strDrivers = "<DRIVERINFO>" + strDriverClassInfo + "</DRIVERINFO>";
				//Making the XMLCLASS format
				strClassXML= "<CLASS>" + strPolicy + strDrivers + "</CLASS>";
					
			}
			catch(Exception exc)
			{
				throw(exc);	
			}
			finally
			{}
			return strClassXML;
		}


		#region Vehicle Class Pol Level
		//Fetch the Input XML from the Rating proc at POL LEVEL  : 18 May 2006
		public string FetchInputXMLforClassForPol(int customerID, int polID, int polVersionID,int vehicleID)
		{
			string strXML= "" ,	strDriverClassInfo="",strDrivers ="",strClassXML="";			

			//Fetch inputXML from the Rating Proc
			ClsGeneralInformation objpol = new ClsGeneralInformation();
			strXML = objpol.FetchPolicyPrivatePassengerInputXML(customerID,polID,polVersionID);
			XmlDocument AppDoc = new XmlDocument();
			try
			{
				//Load XMLDoc from Rating Procs
				AppDoc.LoadXml(strXML);
				
				//Get policy Nodes
				XmlNode AppNode = AppDoc.SelectSingleNode("QUICKQUOTE/POLICY");
				string strPolicy = AppNode.InnerXml;
			
				//loop to fetch all drivers who have this vehicle assigned.
				foreach(XmlNode DriverNode in AppDoc.SelectNodes("//DRIVERS/*"))
				{
					string assignedVehicle =DriverNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString()==""?"0":DriverNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString();
					if(Convert.ToInt32(assignedVehicle) == vehicleID)
					{
						strDriverClassInfo = strDriverClassInfo + "<DRIVER>" +  DriverNode.InnerXml + "</DRIVER>";
					}
				}
				//Get Driver Nodes				
				strDrivers = "<DRIVERINFO>" + strDriverClassInfo + "</DRIVERINFO>";
				//Making the XMLCLASS format
				strClassXML= "<CLASS>" + strPolicy + strDrivers + "</CLASS>";
					
			}
			catch(Exception exc)
			{
				throw(exc);	
			}
			finally
			{}
			return strClassXML;
		}
		//Modified the Class of all the Vehicles according to the Rules POL LEVEL : 18 may 2006
		public void UpdateVehicleClassPol(int customerID,int polID,int polVersionID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			// Get the Vehicle Ids against the customerID, appID, appVersionID
			string vehicleIDs = ClsGeneralInformation.GetVehicleIDsPolicy(customerID,polID,polVersionID);
			if(vehicleIDs != "-1")
			{
				string[] vehicleID = new string[0];
				vehicleID = vehicleIDs.Split('^');
				// Run a loop to get the inputXML for each vehicleID
				for (int iCounter=0; iCounter<vehicleID.Length ; iCounter++)
				{			
					
					//Fetch inputxml for class
					string strAutoXml="",strVehicleClass="";
					strAutoXml = FetchInputXMLforClassForPol(customerID,polID,polVersionID,int.Parse(vehicleID[iCounter]));

					//Fetch the Vehicle Class on the basis of the inputxml
					strVehicleClass = FetchAutoClass(strAutoXml);

					//Update the vehicle class
					objDataWrapper.ClearParameteres();						
					objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
					objDataWrapper.AddParameter("@POLICY_ID",polID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
					objDataWrapper.AddParameter("@VEHICLE_ID",vehicleID[iCounter]);
					objDataWrapper.AddParameter("@VEHICLE_CLASS",strVehicleClass);
					objDataWrapper.ExecuteNonQuery("PROC_UPDATEVEHICLECLASS_POL");					
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				}
			}

		}
		

		#endregion


		//
		public string FetchAutoClass(string inputXMLForClass)
		{
			try
			{
				#region SETTING THE DEFAULT VALUE IN THE CLASS
				
				string autoClass = "";
				// Set default class in this string variable
				string  vehicleClassFilePath = ClsCommon.GetKeyValueWithIP("VEHICLE_CLASS");
				XmlDocument docTemp = new XmlDocument();
				docTemp.Load(vehicleClassFilePath);
				XmlNode nodTemp = docTemp.SelectSingleNode("VEHICLECLASS");
				if(nodTemp !=null)
					autoClass = nodTemp.FirstChild.Attributes["DEFAULT_CLASS"].Value.ToString().Trim();

				#endregion

				#region FETCHING THE AUTO CLASS
				//fetch the classxml
				ClsAuto objAuto = new ClsAuto();
				string vehicleClassXML = objAuto.GetVehicleClass(inputXMLForClass,"AUTOP");							
				XmlDocument ClassDoc = new XmlDocument();
				ClassDoc.LoadXml(vehicleClassXML);
				XmlNode ClassNode = ClassDoc.SelectSingleNode("//CLASS");				
				if(ClassNode!=null && ClassNode.SelectSingleNode("VEHICLECLASS").InnerText.ToString().Trim() !="")
				{
					autoClass = ClassNode.SelectSingleNode("VEHICLECLASS").InnerText.ToString();
				}
				#endregion

				return autoClass;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}		
		}
		//Modified the Class of all the Vehicles according to the Rules : 17 may 2006
		public void UpdateVehicleClass(int customerID,int appID,int appVersionID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			// Get the Vehicle Ids against the customerID, appID, appVersionID
			string vehicleIDs = ClsGeneralInformation.GetVehicleIDs(customerID,appID,appVersionID);
			if(vehicleIDs != "-1")
			{
				string[] vehicleID = new string[0];
				vehicleID = vehicleIDs.Split('^');
				// Run a loop to get the inputXML for each vehicleID
				for (int iCounter=0; iCounter<vehicleID.Length ; iCounter++)
				{			
					
					//Fetch inputxml for class
					string strAutoXml="",strVehicleClass="";
					strAutoXml = FetchInputXMLforClassForApp(customerID,appID,appVersionID,int.Parse(vehicleID[iCounter]));

					//Fetch the Vehicle Class on the basis of the inputxml
					strVehicleClass = FetchAutoClass(strAutoXml);

					//Update the vehicle class
					objDataWrapper.ClearParameteres();						
					objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
					objDataWrapper.AddParameter("@APP_ID",appID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
					objDataWrapper.AddParameter("@VEHICLE_ID",vehicleID[iCounter]);
					objDataWrapper.AddParameter("@VEHICLE_CLASS",strVehicleClass);
					objDataWrapper.ExecuteNonQuery("PROC_UPDATEVEHICLECLASS");					
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				}
			}

		}
		

		#endregion

		#region AddMotorDriverDetails functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDriverDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddMotorDriverDetails(ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom)
		{
			return AddMotorDriverDetails(objDriverDetailsInfo,strCalledFrom,"");
			//			DateTime	RecordDate		=	DateTime.Now;
			//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//
			//			try
			//			{
			//				AddParameters(objDriverDetailsInfo,objDataWrapper,'I');
			//				
			//				objDataWrapper.AddParameter("@DRIVER_FAX",objDriverDetailsInfo.DRIVER_FAX);
			//				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
			//				
			//				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
			//
			//				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
			//				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
			//				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
			//				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
			//				objDataWrapper.AddParameter("@MODIFIED_BY",null);
			//				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
			//				objDataWrapper.AddParameter("@SAFE_DRIVER",null);
			//				//objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
			//				//objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
			//
			//				//// Added By anurag Verma on 19/09/2005 for merging screens
			//				objDataWrapper.AddParameter("@Mature_Driver",objDriverDetailsInfo.Mature_Driver);
			//				objDataWrapper.AddParameter("@Mature_Driver_Discount",objDriverDetailsInfo.Mature_Driver_Discount);
			//				objDataWrapper.AddParameter("@Preferred_Risk_Discount",objDriverDetailsInfo.Preferred_Risk_Discount);
			//				objDataWrapper.AddParameter("@Preferred_Risk",objDriverDetailsInfo.Preferred_Risk);
			//				objDataWrapper.AddParameter("@TransferExp_Renewal_Discount",objDriverDetailsInfo.TransferExp_Renewal_Discount);
			//			//	objDataWrapper.AddParameter("@TransferExperience_RenewalCredit",objDriverDetailsInfo.TransferExperience_RenewalCredit );
			//
			//
			//				//objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLEID);
			//				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
			//				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
			//				
			//				//added by vj on 17-10-2005
			//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID); 
			//				objDataWrapper.AddParameter("@Called_From",strCalledFrom);
			//				
			//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);
			//
			//				int returnResult = 0;
			//				if(TransactionLogRequired)
			//				{
			//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddMotorDriverDetails.aspx.resx");
			//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			//					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
			//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			//					objTransactionInfo.TRANS_TYPE_ID	=	1;
			//					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
			//					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
			//					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
			//					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
			//					objTransactionInfo.TRANS_DESC		=	"New driver is added";
			//					objTransactionInfo.CHANGE_XML		=	strTranXML;
			//					//Executing the query
			//					returnResult	= objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS ,objTransactionInfo);
			//				}
			//				else
			//				{
			//					returnResult	= objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS );
			//				}
			//				
			//				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
			//				
			//				objDataWrapper.ClearParameteres();
			//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			//				
			//				if (DRIVER_ID == -1)
			//				{
			//					return -1;
			//				}
			//				else
			//				{
			//					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
			//					return returnResult;
			//				}
			//			}
			//			catch(Exception ex)
			//			{
			//				throw(ex);
			//			}
			//			finally
			//			{
			//				if(objDataWrapper != null) objDataWrapper.Dispose();
			//			}
		}

		public int AddMotorDriverDetails(ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom, string strCustomInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				AddParameters(objDriverDetailsInfo,objDataWrapper,'I');
				
				//objDataWrapper.AddParameter("@DRIVER_FAX",objDriverDetailsInfo.DRIVER_FAX);
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				//objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);

				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@SAFE_DRIVER",null);
				//objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
				//objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);

				//// Added By anurag Verma on 19/09/2005 for merging screens
				objDataWrapper.AddParameter("@Mature_Driver",objDriverDetailsInfo.Mature_Driver);
				objDataWrapper.AddParameter("@Mature_Driver_Discount",objDriverDetailsInfo.Mature_Driver_Discount);
				objDataWrapper.AddParameter("@Preferred_Risk_Discount",objDriverDetailsInfo.Preferred_Risk_Discount);
				objDataWrapper.AddParameter("@Preferred_Risk",objDriverDetailsInfo.Preferred_Risk);
				
				//Commented by Charles on 2-Jul-09 for Itrack issue 6012
				//objDataWrapper.AddParameter("@TransferExp_Renewal_Discount",objDriverDetailsInfo.TransferExp_Renewal_Discount);
				//objDataWrapper.AddParameter("@TransferExperience_RenewalCredit",objDriverDetailsInfo.TransferExperience_RenewalCredit );


				//objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLEID);
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);

				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				
				//added by vj on 17-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID); 
				objDataWrapper.AddParameter("@NO_CYCLE_ENDMT",objDriverDetailsInfo.NO_CYCLE_ENDMT); 
				objDataWrapper.AddParameter("@Called_From",strCalledFrom);

				objDataWrapper.AddParameter("@CYCL_WITH_YOU",objDriverDetailsInfo.CYCL_WITH_YOU); 
				objDataWrapper.AddParameter("@COLL_STUD_AWAY_HOME",objDriverDetailsInfo.COLL_STUD_AWAY_HOME); 				
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);
//				string sbStrXml="";
//				AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo, strCalledFrom,ref sbStrXml);
//				//sbStrXml = sbStrXml.Remove("<LabelFieldMapping>");
				int returnResult = 0;
//				if(TransactionLogRequired)
//				{
//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddMotorDriverDetails.aspx.resx");
//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
//					strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
//					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
//					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
//					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
//					objTransactionInfo.TRANS_DESC		=	"New driver is added";
//					//strTranXML = strTranXML.Remove("</LabelFieldMapping>","");
//					//strTranXML.Append(sbStrXml);
//					objTransactionInfo.CHANGE_XML		=	strTranXML;
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
//					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
//					//Executing the query
//					returnResult	= objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS ,objTransactionInfo);
//				}
//				else
//				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS );
//				}
				
				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
				
				

				if(DRIVER_ID!=-1)
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
					//AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo,strCalledFrom);
					DataTable dtVehicle =new DataTable();
					dtVehicle=ClsVehicleInformation.GetAppVehicle(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDriverDetailsInfo.DRIVER_ID);
					string sbStrXml="";
					AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo, strCalledFrom,dtVehicle,ref sbStrXml);
					//sbStrXml = sbStrXml.Remove("<LabelFieldMapping>");
					//int returnResult = 0;
					if(TransactionLogRequired)
					{
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddMotorDriverDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_ID']");
						strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
						objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1500", ""); //"New driver is added";
						//strTranXML = strTranXML.Remove("</LabelFieldMapping>","");
						//strTranXML.Append(sbStrXml);
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						//Executing the query
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
					}
								

				}
				
				
				//Call Update Class
				objDataWrapper.ClearParameteres();
				if(DRIVER_ID>0)
				{
					ClsVehicleCoverages objCoverage=new ClsVehicleCoverages("MOTOR");
					objCoverage.UpdateCoveragesByRuleApp(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.AutoDriverDep);
					objDataWrapper.ClearParameteres();
					UpdateMotorVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID, objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				}

				if (DRIVER_ID == -1)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
				else
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
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
		
		#region AddUmbrellaDriverDetails functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDriverDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/// 
		public int AddUmbrellaDriverDetails(ClsDriverDetailsInfo objDriverDetailsInfo)
		{
			return AddUmbrellaDriverDetails(objDriverDetailsInfo,"");

			//			DateTime	RecordDate		=	DateTime.Now;
			//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//
			//			try
			//			{
			//				AddParameters(objDriverDetailsInfo,objDataWrapper,'I');
			//				
			//				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
			//				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);
			//
			//				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
			//				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
			//				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
			//				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
			//				objDataWrapper.AddParameter("@MODIFIED_BY",null);
			//				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
			//				
			//				//added by vj on 17-10-2005 for principal / occasional assigned vehicle
			//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
			//				//objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLEID))); 
			//				objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLEID))); 
			//				
			//				//Added By Ravindra(02/27/2006) For Operator
			//				objDataWrapper.AddParameter("@OP_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_VEHICLE_ID)); 
			//				objDataWrapper.AddParameter("@OP_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_APP_VEHICLE_PRIN_OCC_ID)); 
			//				objDataWrapper.AddParameter("@OP_DRIVER_COST_GAURAD_AUX",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_DRIVER_COST_GAURAD_AUX )); 
			//				///////////////////////////////
			//			
			//				
			//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);
			//
			//				int returnResult = 0;
			//				if(TransactionLogRequired)
			//				{
			//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
			//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			//					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
			//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			//					objTransactionInfo.TRANS_TYPE_ID	=	1;
			//					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
			//					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
			//					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
			//					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
			//					objTransactionInfo.TRANS_DESC		=	"New driver is added";
			//					objTransactionInfo.CHANGE_XML		=	strTranXML;
			//					//Executing the query
			//					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_UMBRELLA_DRIVER_DETAILS ,objTransactionInfo);
			//				}
			//				else
			//				{
			//					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_UMBRELLA_DRIVER_DETAILS );
			//				}
			//				
			//				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
			//				
			//				objDataWrapper.ClearParameteres();
			//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			//				
			//				if (DRIVER_ID == -1)
			//				{
			//					return -1;
			//				}
			//				else
			//				{
			//					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
			//					return returnResult;
			//				}
			//			}
			//			catch(Exception ex)
			//			{
			//				throw(ex);
			//			}
			//			finally
			//			{
			//				if(objDataWrapper != null) objDataWrapper.Dispose();
			//			}
		}

		public int AddUmbrellaDriverDetails(ClsDriverDetailsInfo objDriverDetailsInfo,string strCustomInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				AddParameters(objDriverDetailsInfo,objDataWrapper,'I');
				
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);

				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				
				//added by vj on 17-10-2005 for principal / occasional assigned vehicle
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				//objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLEID))); 
				objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLE_ID))); 

				//Added By Ravindra(02/27/2006) For Operator
				objDataWrapper.AddParameter("@OP_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_VEHICLE_ID)); 
				objDataWrapper.AddParameter("@OP_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_APP_VEHICLE_PRIN_OCC_ID)); 
				objDataWrapper.AddParameter("@OP_DRIVER_COST_GAURAD_AUX",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_DRIVER_COST_GAURAD_AUX )); 
				///////////////////////////////
				objDataWrapper.AddParameter("@MOT_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.MOT_VEHICLE_ID)); 
				objDataWrapper.AddParameter("@MOT_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.MOT_APP_VEHICLE_PRIN_OCC_ID)); 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/UmbDriverDetails.aspx.resx");					
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1500", ""); //"New driver is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_UMBRELLA_DRIVER_DETAILS ,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_UMBRELLA_DRIVER_DETAILS );
				}
				
				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
				//for Umbrella Coverages by Pravesh
				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.RiskDependent,0);			
				///end 
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (DRIVER_ID == -1)
				{
					return -1;
				}
				else
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
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

		/// Sumit Chhabra:03/12/2007
		/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
		public int AddPolicyUmbrellaDriverDetails(Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo objDriverDetailsInfo, string strCustomInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",null);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",null);
				objDataWrapper.AddParameter("@DRIVER_ADD1",null);
				objDataWrapper.AddParameter("@DRIVER_ADD2",null);
				objDataWrapper.AddParameter("@DRIVER_CITY",null);
				objDataWrapper.AddParameter("@DRIVER_STATE",null);
				objDataWrapper.AddParameter("@DRIVER_ZIP",null);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",null);
				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",null);
				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",null);
				objDataWrapper.AddParameter("@DRIVER_EXT",null);
				objDataWrapper.AddParameter("@DRIVER_MOBILE",null);
				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);
				objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);			
				objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",null);
				if (objDriverDetailsInfo.DATE_LICENSED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_LICENSED",objDriverDetailsInfo.DATE_LICENSED);
				else
					objDataWrapper.AddParameter("@DATE_LICENSED",null);

				objDataWrapper.AddParameter("@DRIVER_REL",null);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
				objDataWrapper.AddParameter("@DRIVER_OCC_CODE",null);
			
				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",null);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",null);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",null);
				objDataWrapper.AddParameter("@DRIVER_INCOME",null);				
				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",null);
				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",null);
				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",null);
				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",null);
				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",null);
				objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",null);
				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",null);
				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",null);
				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",null);
				objDataWrapper.AddParameter("@INSERTUPDATE","I");				
				objDataWrapper.AddParameter("@RELATIONSHIP",null);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);
				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",null);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
		
				objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLE_ID))); 

				objDataWrapper.AddParameter("@OP_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_VEHICLE_ID)); 
				objDataWrapper.AddParameter("@OP_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_APP_VEHICLE_PRIN_OCC_ID)); 
				objDataWrapper.AddParameter("@OP_DRIVER_COST_GAURAD_AUX",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_DRIVER_COST_GAURAD_AUX )); 
				objDataWrapper.AddParameter("@MOT_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.MOT_VEHICLE_ID)); 
				objDataWrapper.AddParameter("@MOT_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.MOT_APP_VEHICLE_PRIN_OCC_ID)); 


//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",null);
//				objDataWrapper.AddParameter("@VEHICLE_ID",null);				
//				objDataWrapper.AddParameter("@OP_VEHICLE_ID",null);
//				objDataWrapper.AddParameter("@OP_APP_VEHICLE_PRIN_OCC_ID",null);
//				objDataWrapper.AddParameter("@OP_DRIVER_COST_GAURAD_AUX",null);	
				objDataWrapper.AddParameter("@FORM_F95",objDriverDetailsInfo.FORM_F95); 
//				objDataWrapper.AddParameter("@MOT_APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.MOT_APP_VEHICLE_PRIN_OCC_ID); 
//				objDataWrapper.AddParameter("@MOT_VEHICLE_ID",objDriverDetailsInfo.MOT_VEHICLE_ID); 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/UmbDriverDetails.aspx.resx");					
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
					objTransactionInfo.POLICY_ID		=	objDriverDetailsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID=	objDriverDetailsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1500", ""); //"New driver is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;					
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaPolicyDriver",objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaPolicyDriver");
				}
				
				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				//for Umbrella Coverages by Pravesh
				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,RuleType.RiskDependent,0);			
				///end 
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (DRIVER_ID == -1)
				{
					return -1;
				}
				else
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
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
		/// <param name="objOldDriverDetailsInfo">Model object having old information</param>
		/// <param name="objDriverDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom)
		{
			return UpdateAppDriverDetails(objOldDriverDetailsInfo, objDriverDetailsInfo,strCalledFrom,"");
		}

		public int Update(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom, string strCustomInfo)
		{			
			return UpdateAppDriverDetails(objOldDriverDetailsInfo, objDriverDetailsInfo,strCalledFrom,strCustomInfo);			
		}
		public int Update(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom, string strCustomInfo,string strAssignXml)
		{			
			return UpdateAppDriverDetails(objOldDriverDetailsInfo, objDriverDetailsInfo,strCalledFrom,strCustomInfo,strAssignXml);			
		}


		#endregion


		#region Delete Function
		public int DeleteDriver(int customerID,int appID,int appVersionID,int driverID)
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@APP_ID",appID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",driverID);
				

				returnResult = objDataWrapper.ExecuteNonQuery("PROC_DELETEDRIVER");

				return returnResult;
				
			}
			catch(Exception exc)
			{
				throw(exc);
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

		public int DeleteMotorDriverPolicy(ClsPolicyDriverInfo objDriverDetailInfo, string strCustomInfo)
		{
			string		strStoredProc	=	"PROC_DELETEMOTORDRIVERPOLICY";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailInfo.DRIVER_ID);				

				int returnResult = 0;
				if(TransactionLogRequired)
				{						
					objDriverDetailInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/aspx/Motorcycle/PolicyAddMotorDriver.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID		=	objDriverDetailInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID=	objDriverDetailInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1595", "");// "Policy Motorcycle Driver is deleted";										
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				UpdateMotorVehicleClassPOL(objDataWrapper,objDriverDetailInfo.CUSTOMER_ID,objDriverDetailInfo.POLICY_ID,objDriverDetailInfo.POLICY_VERSION_ID);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
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
		/// <summary>
		/// Overloaded DeleteDriver method
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="driverID"></param>
		/// <param name="strCalledFrom"></param>
		/// <returns></returns>
		public int DeleteDriver(int customerID,int appID,int appVersionID,int driverID,string strCalledFrom)
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@APP_ID",appID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",driverID);
				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
				returnResult = objDataWrapper.ExecuteNonQuery("PROC_DELETEDRIVER");
				return returnResult;
	
			}
			catch(Exception exc)
			{
				throw(exc);
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

		public int DeleteDriver(int customerID,int appID,int appVersionID,int driverID,string strCalledFrom,string strCalledFor)
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@APP_ID",appID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",driverID);
				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
				objDataWrapper.AddParameter("@CALLEDFOR",strCalledFor);

				returnResult = objDataWrapper.ExecuteNonQuery("PROC_DELETEDRIVER");
				return returnResult;
	
			}
			catch(Exception exc)
			{
				throw(exc);
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

		public int DeleteDriver(ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom, string hidCustomInfo)
		{
			string		strStoredProc	=	"PROC_DELETEDRIVER";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);

				int returnResult = 0;
				if(TransactionLogRequired)
				{						
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddDriverDetails.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1594", "");// "Driver is Deleted";
					objTransactionInfo.CUSTOM_INFO		=	hidCustomInfo;
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();

				//Update Endorsements/////////////

				//this.UpdateDriverEndorsements(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.AutoDriverDep);
				//////////////////////////
				if(strCalledFrom=="PPA")
				{
					objDataWrapper.ClearParameteres();
					UpdateVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				}
				else if(strCalledFrom=="MOT")
				{
					objDataWrapper.ClearParameteres();
					UpdateMotorVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
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

		public int DeleteDriver(ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom,string strCalledFor, string hidCustomInfo)
		{
			string		strStoredProc	=	"PROC_DELETEDRIVER";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
				objDataWrapper.AddParameter("@CALLEDFOR",strCalledFor);

				int returnResult = 0;
				if(TransactionLogRequired)
				{						
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddDriverDetails.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1594", "");//"Driver is Deleted";
					objTransactionInfo.CUSTOM_INFO		=	hidCustomInfo;
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
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
       
		/*public int DeleteDriver(ClsWatercraftOperatorInfo objWatercraftOperatorInfo,string strCalledFrom, string strCustomInfo)
		{
			string		strStoredProc	=	"PROC_DELETEDRIVER";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftOperatorInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftOperatorInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftOperatorInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Operator is Deleted";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
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
		}*/		

		//		public int DeleteDriver(ClsUmbrellaOperatorInfo objUmbrellaOperatorInfo,string strCalledFrom,string strCalledFor, string strCustomInfo)
		//		{
		//			string		strStoredProc	=	"PROC_DELETEDRIVER";			
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//
		//			try
		//			{
		//				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaOperatorInfo.CUSTOMER_ID);
		//				objDataWrapper.AddParameter("@APP_ID",objUmbrellaOperatorInfo.APP_ID);
		//				objDataWrapper.AddParameter("@APP_VERSION_ID",objUmbrellaOperatorInfo.APP_VERSION_ID);
		//				objDataWrapper.AddParameter("@DRIVER_ID",objUmbrellaOperatorInfo.DRIVER_ID);
		//				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
		//				objDataWrapper.AddParameter("@CALLEDFOR",strCalledFor);
		//
		//				int returnResult = 0;
		//				if(TransactionLogRequired)
		//				{	
		//					
		//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//					objTransactionInfo.TRANS_TYPE_ID	=	1;
		//					objTransactionInfo.APP_ID			=	objUmbrellaOperatorInfo.APP_ID;
		//					objTransactionInfo.APP_VERSION_ID	=	objUmbrellaOperatorInfo.APP_VERSION_ID;
		//					objTransactionInfo.CLIENT_ID		=	objUmbrellaOperatorInfo.CUSTOMER_ID;
		//					objTransactionInfo.RECORDED_BY		=	objUmbrellaOperatorInfo.MODIFIED_BY;
		//					objTransactionInfo.TRANS_DESC		=	"Operator is Deleted";
		//					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
		//					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		//				}
		//				else
		//				{
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
		//				}				
		//				objDataWrapper.ClearParameteres();
		//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		//				return returnResult;
		//			}
		//			catch(Exception ex)
		//			{
		//				throw(ex);				
		//			}
		//			finally
		//			{
		//				if(objDataWrapper != null) objDataWrapper.Dispose();
		//			}	
		//
		//		}


		#endregion



		#region UpdateAppDriverDetails method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldDriverDetailsInfo">Model object having old information</param>
		/// <param name="objDriverDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateAppDriverDetails(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo, string strCalledFrom,string strAssignXml)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				AddParameters(objDriverDetailsInfo,objDataWrapper,'U');

				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);
				objDataWrapper.AddParameter("@SAFE_DRIVER",objDriverDetailsInfo.SAFE_DRIVER);

				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY", null);
				objDataWrapper.AddParameter("@CREATED_DATETIME", null);
				objDataWrapper.AddParameter("@MODIFIED_BY", objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDriverDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);

				//// Added By anurag Verma on 15/09/2005 for merging screens
				objDataWrapper.AddParameter("@Good_Driver_Student_Discount",objDriverDetailsInfo.Good_Driver_Student_Discount);
				objDataWrapper.AddParameter("@Premier_Driver_Discount",objDriverDetailsInfo.Premier_Driver_Discount);
				//objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLEID);
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);

				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				if (objDriverDetailsInfo.VEHICLE_ID != null)
					objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				else
					objDataWrapper.AddParameter("@VEHICLE_ID",0);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom); 
				if(strCalledFrom.ToString().ToUpper()=="PPA")
					objDataWrapper.AddParameter("@NO_DEPENDENTS",DefaultValues.GetIntNull(objDriverDetailsInfo.NO_DEPENDENTS));

				

				//added by vj on 17-10-2005 for vehicle assigned principal / occasional
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
					

				if(TransactionLogRequired) 
				{

					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1596", "");//"Driver is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS, objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS);
				}
				
				objDataWrapper.ClearParameteres();
				
				///Update Endorsements based on Driver attributes/////////////////////////////////////////////
				//objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				//objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				//objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);

				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.AutoDriverDep);

				
				//objDataWrapper.ExecuteNonQuery("Proc_UPDATE_DRIVER_ENDORSEMENTS");

				//Updating the vehicle class 
				/*UpdateVehicleClass(objDataWrapper, objDriverDetailsInfo.APP_ID, objDriverDetailsInfo.APP_VERSION_ID
					, objDriverDetailsInfo.CUSTOMER_ID, objDriverDetailsInfo.DRIVER_ID
					, int.Parse(objDriverDetailsInfo.VEHICLEID));*/

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
		public void AddAppAssignedVehicles(DataWrapper objDataWrapper,ClsDriverDetailsInfo  objDriverDetailsInfo, string strCalledFrom,ref string sbStrXml)
		{
			DataTable dt = new DataTable();
			AddAppAssignedVehicles(objDataWrapper,"",objDriverDetailsInfo,strCalledFrom,dt,ref sbStrXml);
			
		}
		/// <summary>
		/// 12 sep 2007
		/// </summary>
		/// <param name="objDataWrapper"></param>
		/// <param name="objDriverDetailsInfo"></param>
		/// <param name="strCalledFrom"></param>
		/// <param name="sbStrXml"></param>
	/*	public void AddAppAssignedBoats(DataWrapper objDataWrapper,ClsWatercraftOperatorInfo objWatercraftOperatorInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			AddAppAssignedBoats(objDataWrapper,"",objWatercraftOperatorInfo,strCalledFrom,dt,ref sbStrXml);
		}*/
		/// <summary>
		/// 12 sep 2007
		/// </summary>
		/// <param name="objDataWrapper"></param>
		/// <param name="strAssignXml"></param>
		/// <param name="objDriverDetailsInfo"></param>
		/// <param name="strCalledFrom"></param>
		/// <param name="dt"></param>
		/// <param name="sbStrXml"></param>

        //public void AddAppAssignedBoats(DataWrapper objDataWrapper,string strAssignXml,ClsWatercraftOperatorInfo  objWatercraftOperatorInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
        //{
        //    try
        //    {
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.AddParameter("@CustID",objWatercraftOperatorInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@AppID",objWatercraftOperatorInfo.APP_ID);
        //        objDataWrapper.AddParameter("@AppVersionID",objWatercraftOperatorInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@DriverID",objWatercraftOperatorInfo.DRIVER_ID);
        //        objDataWrapper.ExecuteNonQuery("Proc_DeleteAssignedBoat");
        //        int returnResult = 0;
        //        StringBuilder sbTranXML=new StringBuilder();
        //        sbTranXML.Append("<LabelFieldMapping>");
        //        string strTranXML="";
        //        if(!(objWatercraftOperatorInfo.ASSIGNED_VEHICLE==""))
        //        {
        //            int vehicleIndex= 0;
        //            foreach (string str in objWatercraftOperatorInfo.ASSIGNED_VEHICLE.Split('|'))
        //            {	
        //                    string strTrim = "";
        //                    strTrim=str.TrimEnd('~');
        //                    string[] arrstr = strTrim.Split('~');
        //                    string strMakeModel="";
        //                    objDataWrapper.ClearParameteres();
        //                    objDataWrapper.AddParameter("@CustID",objWatercraftOperatorInfo.CUSTOMER_ID);
        //                    objDataWrapper.AddParameter("@AppID",objWatercraftOperatorInfo.APP_ID);
        //                    objDataWrapper.AddParameter("@AppVersionID",objWatercraftOperatorInfo.APP_VERSION_ID);
        //                    objDataWrapper.AddParameter("@DriverID",objWatercraftOperatorInfo.DRIVER_ID);
						
        //                    objDataWrapper.AddParameter("@BoatID",arrstr[0]);
        //                    if(arrstr.Length>1)
        //                        objDataWrapper.AddParameter("@PrinOccID",arrstr[1]);
        //                    else
        //                        objDataWrapper.AddParameter("@PrinOccID","");
        //                    //						objDataWrapper.AddParameter("@BoatID",str.Split('~')[0]);
        //                    //						objDataWrapper.AddParameter("@PrinOccID",str.Split('~')[1]);
        //                    returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertAssignedBoat");
        //                    if(TransactionLogRequired)
        //                    {
        //                        objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/Watercrafts/AddWatercraftDriverDetails.aspx.resx");
        //                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
								
        //                        DataRow[] drMake = dt.Select("BOAT_ID=" + arrstr[0]);
        //                        if(drMake.Length !=0 )
        //                        {
        //                             strMakeModel = drMake[0]["MODEL_MAKE"].ToString();
        //                        }
						
        //                        XmlDocument objAssignXml = new XmlDocument();
        //                        if(strAssignXml != null && strAssignXml != "")
        //                        {
        //                            objAssignXml.LoadXml(strAssignXml);
        //                            string strOldAssign = objAssignXml.SelectSingleNode("NewDataSet").ChildNodes.Item(vehicleIndex).ChildNodes.Item(3).InnerXml;
        //                            if(strOldAssign !="" )
        //                            {
        //                                /*Manoj Rathore Itrack # 5815 on 6th May 2009
        //                                    strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
        //                                    if(arrstr.Length>1)
        //                                    strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='" + strOldAssign + "' NewValue='"+ arrstr[1] + "' />"; 
        //                                */

        //                                strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
        //                                if(arrstr.Length>1)
        //                                    strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strOldAssign) + "\" NewValue=\""+ RemoveJunkXmlCharactersRates(arrstr[1]) + "\" />";
        //                            }
        //                        }
        //                        else
        //                        { 
        //                            /*Manoj Rathore Itrack # 5815 on 6th May 2009
        //                           * strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
        //                            if(arrstr.Length>1)
        //                            strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='' NewValue='"+ arrstr[1] + "' />";
        //                           */

        //                            strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
        //                            if(arrstr.Length>1)
        //                                strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue=\"\" NewValue=\""+ ClsCommon.RemoveJunkXmlCharacters(arrstr[1]) + "\" />";
        //                        }
        //                        sbTranXML.Append(strTranXML);
        //                        strTranXML="";
        //                    }
        //                    else
        //                    {
							
        //                    }
        //                }
        //                vehicleIndex++;
					
        //            sbTranXML.Append("</LabelFieldMapping>");
        //            sbStrXml = sbTranXML.ToString();
        //            //				
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw(ex);
        //    }
        //    finally
        //    {
			
        //    }
        //}
		#region POL ADD POLICY BOATS :Added 13 Sep 2007
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objDataWrapper"></param>
		/// <param name="objWatercraftOperatorInfo"></param>
		/// <param name="strCalledFrom"></param>
		/// <param name="dt"></param>
		/// <param name="sbStrXml"></param>
		public void AddPolAssignedBoats(DataWrapper objDataWrapper,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			AddPolAssignedBoats(objDataWrapper,"",objWatercraftOperatorInfo,strCalledFrom,dt,ref sbStrXml);
		}
		public void AddPolAssignedBoats(DataWrapper objDataWrapper,string strAssignXml,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo  objWatercraftOperatorInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			try
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CustID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@PolID",objWatercraftOperatorInfo.POLICY_ID);
				objDataWrapper.AddParameter("@PolVersionID",objWatercraftOperatorInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DriverID",objWatercraftOperatorInfo.DRIVER_ID);
				objDataWrapper.ExecuteNonQuery("Proc_DeleteAssignedBoat_Pol");
				int returnResult = 0;
				StringBuilder sbTranXML=new StringBuilder();
				sbTranXML.Append("<LabelFieldMapping>");
				string strTranXML="";
				if(!(objWatercraftOperatorInfo.ASSIGNED_VEHICLE==""))
				{
					int vehicleIndex= 0;
					foreach (string str in objWatercraftOperatorInfo.ASSIGNED_VEHICLE.Split('|'))
					{	
						string strTrim = "";
						strTrim=str.TrimEnd('~');
						string[] arrstr = strTrim.Split('~');
						objDataWrapper.ClearParameteres();
						objDataWrapper.AddParameter("@CustID",objWatercraftOperatorInfo.CUSTOMER_ID);
						objDataWrapper.AddParameter("@PolID",objWatercraftOperatorInfo.POLICY_ID);
						objDataWrapper.AddParameter("@PolVersionID",objWatercraftOperatorInfo.POLICY_VERSION_ID);
						objDataWrapper.AddParameter("@DriverID",objWatercraftOperatorInfo.DRIVER_ID);
						objDataWrapper.AddParameter("@BoatID",arrstr[0]);
						if(arrstr.Length>1)
							objDataWrapper.AddParameter("@PrinOccID",arrstr[1]);
						else
							objDataWrapper.AddParameter("@PrinOccID","");
						returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertAssignedBoat_Pol");
						if(TransactionLogRequired)
						{
							objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Watercraft/PolicyAddWatercraftOperator.aspx.resx");
							SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
							DataRow[] drMake = dt.Select("BOAT_ID=" + str.Split('~')[0]);
							string strMakeModel = drMake[0]["MODEL_MAKE"].ToString();
						
							XmlDocument objAssignXml = new XmlDocument();
							if(strAssignXml != null && strAssignXml != "")
							{
								objAssignXml.LoadXml(strAssignXml);
								string strOldAssign = objAssignXml.SelectSingleNode("NewDataSet").ChildNodes.Item(vehicleIndex).ChildNodes.Item(3).InnerXml;
								if(strOldAssign !="" )
								{
									/*Manoj Rathore Itrack # 5815 on 6th May 2009
									 * strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
										if(arrstr.Length>1)
										strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='" + strOldAssign + "' NewValue='"+ arrstr[1] + "' />";
									*/			
								
									strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
									if(arrstr.Length>1)
										strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strOldAssign) + "\" NewValue=\""+ RemoveJunkXmlCharactersRates(arrstr[1]) + "\" />";
								}
							}
							else
							{
								/*Manoj Rathore Itrack # 5815 on 6th May 2009
									strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
									if(arrstr.Length>1)
									strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='' NewValue='"+ arrstr[1] + "' />";
								*/

								strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
								if(arrstr.Length>1)
									strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue=\"\" NewValue=\""+ ClsCommon.RemoveJunkXmlCharacters(arrstr[1]) + "\" />";
							}
							sbTranXML.Append(strTranXML);
							strTranXML="";
						}
						else
						{
							
						}
						vehicleIndex++;
					}
					sbTranXML.Append("</LabelFieldMapping>");
					sbStrXml = sbTranXML.ToString();
					//				
				}
			}
			catch (Exception ex)
			{
				throw(ex);
			}
			finally
			{
			
			}
		}
		#endregion
		//Done for Itrack Issue 6737 on 17 Nov 09
		/*public void AddAppAssignedReacreationalVehicles(DataWrapper objDataWrapper,ClsWatercraftOperatorInfo objWatercraftOperatorInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			AddAppAssignedReacreationalVehicles(objDataWrapper,"",objWatercraftOperatorInfo,strCalledFrom,dt,ref sbStrXml);
		}*/

        //public void AddAppAssignedReacreationalVehicles(DataWrapper objDataWrapper,string strAssignXml,ClsWatercraftOperatorInfo  objWatercraftOperatorInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
        //{
        //    try
        //    {
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.AddParameter("@CustID",objWatercraftOperatorInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@AppID",objWatercraftOperatorInfo.APP_ID);
        //        objDataWrapper.AddParameter("@AppVersionID",objWatercraftOperatorInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@DriverID",objWatercraftOperatorInfo.DRIVER_ID);
        //        objDataWrapper.ExecuteNonQuery("Proc_DeleteAssigned_RecreationalVehicle_App");
        //        int returnResult = 0;
        //        StringBuilder sbTranXML=new StringBuilder();
        //        sbTranXML.Append("<LabelFieldMapping>");
        //        string strTranXML="";
        //        if(!(objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE==""))
        //        {
        //            int vehicleIndex= 0;
        //            foreach (string str in objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE.Split('|'))
        //            {	
        //                string strTrim = "";
        //                strTrim=str.TrimEnd('~');
        //                string[] arrstr = strTrim.Split('~');
        //                string strMakeModel="";
        //                objDataWrapper.ClearParameteres();
        //                objDataWrapper.AddParameter("@CustID",objWatercraftOperatorInfo.CUSTOMER_ID);
        //                objDataWrapper.AddParameter("@AppID",objWatercraftOperatorInfo.APP_ID);
        //                objDataWrapper.AddParameter("@AppVersionID",objWatercraftOperatorInfo.APP_VERSION_ID);
        //                objDataWrapper.AddParameter("@DriverID",objWatercraftOperatorInfo.DRIVER_ID);
						
        //                objDataWrapper.AddParameter("@REC_VEH_ID",arrstr[0]);
        //                if(arrstr.Length>1)
        //                    objDataWrapper.AddParameter("@PrinOccID",arrstr[1]);
        //                else
        //                    objDataWrapper.AddParameter("@PrinOccID","");

        //                returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertAssigned_RecreationalVehicle_App");
        //                if(TransactionLogRequired)
        //                {
        //                    objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/Watercrafts/AddWatercraftDriverDetails.aspx.resx");
        //                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
								
        //                    DataRow[] drMake = dt.Select("REC_VEH_ID=" + arrstr[0]);
        //                    if(drMake.Length !=0 )
        //                    {
        //                        strMakeModel = drMake[0]["MODEL_MAKE"].ToString();
        //                    }
						
        //                    XmlDocument objAssignXml = new XmlDocument();
        //                    if(strAssignXml != null && strAssignXml != "")
        //                    {
        //                        objAssignXml.LoadXml(strAssignXml);
        //                        string strOldAssign = objAssignXml.SelectSingleNode("NewDataSet").ChildNodes.Item(vehicleIndex).ChildNodes.Item(3).InnerXml;
        //                        if(strOldAssign !="" )
        //                        {
        //                            /*Manoj Rathore Itrack # 5815 on 6th May 2009
        //                                    strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
        //                                    if(arrstr.Length>1)
        //                                    strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='" + strOldAssign + "' NewValue='"+ arrstr[1] + "' />"; 
        //                                */

        //                            strTranXML= strTranXML + "<Map label=\"Drive \" field=\"REC_VEH_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
        //                            if(arrstr.Length>1)
        //                                strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_REC_VEHICLE_PRIN_OCC_ID\" OldValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strOldAssign) + "\" NewValue=\""+ RemoveJunkXmlCharactersRates(arrstr[1]) + "\" />";
        //                        }
        //                    }
        //                    else
        //                    { 
        //                        /*Manoj Rathore Itrack # 5815 on 6th May 2009
        //                           * strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
        //                            if(arrstr.Length>1)
        //                            strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='' NewValue='"+ arrstr[1] + "' />";
        //                           */

        //                        strTranXML= strTranXML + "<Map label=\"Drive \" field=\"REC_VEH_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
        //                        if(arrstr.Length>1)
        //                            strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_REC_VEHICLE_PRIN_OCC_ID\" OldValue=\"\" NewValue=\""+ ClsCommon.RemoveJunkXmlCharacters(arrstr[1]) + "\" />";
        //                    }
        //                    sbTranXML.Append(strTranXML);
        //                    strTranXML="";
        //                }
        //                else
        //                {
							
        //                }
        //            }
        //            vehicleIndex++;
					
        //            sbTranXML.Append("</LabelFieldMapping>");
        //            sbStrXml = sbTranXML.ToString();
        //            //				
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw(ex);
        //    }
        //    finally
        //    {
			
        //    }
        //}

		#region POL ADD POLICY RECREATIONAL BOATS :Added 17 Nov 2009
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objDataWrapper"></param>
		/// <param name="objWatercraftOperatorInfo"></param>
		/// <param name="strCalledFrom"></param>
		/// <param name="dt"></param>
		/// <param name="sbStrXml"></param>
		public void AddPolAssignedReacreationalVehicles(DataWrapper objDataWrapper,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			AddPolAssignedReacreationalVehicles(objDataWrapper,"",objWatercraftOperatorInfo,strCalledFrom,dt,ref sbStrXml);
		}
		public void AddPolAssignedReacreationalVehicles(DataWrapper objDataWrapper,string strAssignXml,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo  objWatercraftOperatorInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			try
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CustID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@PolID",objWatercraftOperatorInfo.POLICY_ID);
				objDataWrapper.AddParameter("@PolVersionID",objWatercraftOperatorInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DriverID",objWatercraftOperatorInfo.DRIVER_ID);
				objDataWrapper.ExecuteNonQuery("Proc_DeleteAssigned_RecreationalVehicle_Pol");
				int returnResult = 0;
				StringBuilder sbTranXML=new StringBuilder();
				sbTranXML.Append("<LabelFieldMapping>");
				string strTranXML="";
				if(!(objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE==""))
				{
					int vehicleIndex= 0;
					foreach (string str in objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE.Split('|'))
					{	
						string strTrim = "";
						strTrim=str.TrimEnd('~');
						string[] arrstr = strTrim.Split('~');
						objDataWrapper.ClearParameteres();
						objDataWrapper.AddParameter("@CUSTID",objWatercraftOperatorInfo.CUSTOMER_ID);
						objDataWrapper.AddParameter("@POLID",objWatercraftOperatorInfo.POLICY_ID);
						objDataWrapper.AddParameter("@POLVERSIONID",objWatercraftOperatorInfo.POLICY_VERSION_ID);
						objDataWrapper.AddParameter("@DRIVERID",objWatercraftOperatorInfo.DRIVER_ID);
						objDataWrapper.AddParameter("@REC_VEH_ID",arrstr[0]);
						if(arrstr.Length>1)
							objDataWrapper.AddParameter("@PRINOCCID",arrstr[1]);
						else
							objDataWrapper.AddParameter("@PRINOCCID","");
						returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertAssigned_RecreationalVehicle_Pol");
						if(TransactionLogRequired)
						{
							objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Watercraft/PolicyAddWatercraftOperator.aspx.resx");
							SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
							DataRow[] drMake = dt.Select("REC_VEH_ID=" + str.Split('~')[0]);
							string strMakeModel = drMake[0]["MODEL_MAKE"].ToString();
						
							XmlDocument objAssignXml = new XmlDocument();
							if(strAssignXml != null && strAssignXml != "")
							{
								objAssignXml.LoadXml(strAssignXml);
								string strOldAssign = objAssignXml.SelectSingleNode("NewDataSet").ChildNodes.Item(vehicleIndex).ChildNodes.Item(3).InnerXml;
								if(strOldAssign !="" )
								{
									/*Manoj Rathore Itrack # 5815 on 6th May 2009
									 * strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
										if(arrstr.Length>1)
										strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='" + strOldAssign + "' NewValue='"+ arrstr[1] + "' />";
									*/			
								
									strTranXML= strTranXML + "<Map label=\"Drive \" field=\"REC_VEH_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
									if(arrstr.Length>1)
										strTranXML= strTranXML + "<Map label=\"as \" field=\"POL_REC_VEHICLE_PRIN_OCC_ID\" OldValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strOldAssign) + "\" NewValue=\""+ RemoveJunkXmlCharactersRates(arrstr[1]) + "\" />";
								}
							}
							else
							{
								/*Manoj Rathore Itrack # 5815 on 6th May 2009
									strTranXML= strTranXML + "<Map label=\"Drive \" field=\"BOAT_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
									if(arrstr.Length>1)
									strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='' NewValue='"+ arrstr[1] + "' />";
								*/

								strTranXML= strTranXML + "<Map label=\"Drive \" field=\"REC_VEH_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
								if(arrstr.Length>1)
									strTranXML= strTranXML + "<Map label=\"as \" field=\"POL_REC_VEHICLE_PRIN_OCC_ID\" OldValue=\"\" NewValue=\""+ ClsCommon.RemoveJunkXmlCharacters(arrstr[1]) + "\" />";
							}
							sbTranXML.Append(strTranXML);
							strTranXML="";
						}
						else
						{
							
						}
						vehicleIndex++;
					}
					sbTranXML.Append("</LabelFieldMapping>");
					sbStrXml = sbTranXML.ToString();
					//				
				}
			}
			catch (Exception ex)
			{
				throw(ex);
			}
			finally
			{
			
			}
		}
		#endregion
		//Added till here

		public void AddAppAssignedVehicles(DataWrapper objDataWrapper,ClsDriverDetailsInfo  objDriverDetailsInfo, string strCalledFrom)
		{
			string strXml="";
			DataTable dt = new DataTable();
			AddAppAssignedVehicles(objDataWrapper,"",objDriverDetailsInfo, strCalledFrom,dt,ref strXml);
		}

		public void AddAppAssignedVehicles(DataWrapper objDataWrapper,ClsDriverDetailsInfo objDriverDetailsInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			AddAppAssignedVehicles(objDataWrapper,"",objDriverDetailsInfo,strCalledFrom,dt,ref sbStrXml);
		}

		public void AddAppAssignedVehicles(DataWrapper objDataWrapper,string strAssignXml,ClsDriverDetailsInfo  objDriverDetailsInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			try
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@AppID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@AppVersionID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DriverID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.ExecuteNonQuery("Proc_DeleteAssignedVehicle");
				int returnResult = 0;
				StringBuilder sbTranXML=new StringBuilder();
				sbTranXML.Append("<LabelFieldMapping>");
				string strTranXML="";
				if(!(objDriverDetailsInfo.ASSIGNED_VEHICLE=="" || (strCalledFrom.ToUpper()=="PPA" && objDriverDetailsInfo.DRIVER_DRIV_TYPE!="11603")))//11603-Licensed Driver Type
				{
					int vehicleIndex= 0;
					foreach (string str in objDriverDetailsInfo.ASSIGNED_VEHICLE.Split('|'))
					{	
						
						objDataWrapper.ClearParameteres();
						objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
						objDataWrapper.AddParameter("@AppID",objDriverDetailsInfo.APP_ID);
						objDataWrapper.AddParameter("@AppVersionID",objDriverDetailsInfo.APP_VERSION_ID);
						objDataWrapper.AddParameter("@DriverID",objDriverDetailsInfo.DRIVER_ID);
						objDataWrapper.AddParameter("@VehID",str.Split('~')[0]);
						if(objDriverDetailsInfo.IN_MILITARY.ToString() =="10963" && objDriverDetailsInfo.STATIONED_IN_US_TERR.ToString() =="10964")
						{
							objDataWrapper.AddParameter("@PrinOccID","11931");
						}
						else
						{
								objDataWrapper.AddParameter("@PrinOccID",str.Split('~')[1]);
						}
						returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertAssignedVehicle");
						if(TransactionLogRequired)
						{
							objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
							SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
							DataRow[] drMake = dt.Select("VEHICLE_ID=" + str.Split('~')[0]);
							string strMakeModel = drMake[0]["MODEL_MAKE"].ToString();
						
							XmlDocument objAssignXml = new XmlDocument();
							if(strAssignXml != null && strAssignXml != "")
							{
								objAssignXml.LoadXml(strAssignXml);
								string strOldAssign = objAssignXml.SelectSingleNode("NewDataSet").ChildNodes.Item(vehicleIndex).ChildNodes.Item(3).InnerXml;
								if(strOldAssign !="" )
								{
									//Manoj Rathore itrack # 5815 on 6 May 2009
									//strTranXML= strTranXML + "<Map label=\"Drive \" field=\"VEHICLE_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
									//strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='" + strOldAssign + "' NewValue='"+ str.Split('~')[1] + "' />";

									strTranXML= strTranXML + "<Map label=\"Drive \" field=\"VEHICLE_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
									strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strOldAssign) + "\" NewValue=\""+ RemoveJunkXmlCharactersRates(str.Split('~')[1]) + "\" />";
								}
							}
							else
							{
								//Manoj Rathore itrack # 5815 on 6 May 2009
								//strTranXML= strTranXML + "<Map label=\"Drive \" field=\"VEHICLE_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
								//strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='' NewValue='"+ str.Split('~')[1] + "' />";

								strTranXML= strTranXML + "<Map label=\"Drive \" field=\"VEHICLE_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
								strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue=\"\" NewValue=\""+ ClsCommon.RemoveJunkXmlCharacters(str.Split('~')[1]) + "\" />";
							}
							sbTranXML.Append(strTranXML);
							strTranXML="";
						}
						else
						{
							
						}
						vehicleIndex++;
					}
					sbTranXML.Append("</LabelFieldMapping>");
					sbStrXml = sbTranXML.ToString();
					//				
				}
			}
			catch (Exception ex)
			{
				throw(ex);
			}
			finally
			{
			
			}
		}		

		public void AddPolAssignedVehicles(DataWrapper objDataWrapper,ClsPolicyDriverInfo  objDriverDetailsInfo, string strCalledFrom)
		{
			string strXml="";
			DataTable dt = new DataTable();
			AddPolAssignedVehicles(objDataWrapper,"",objDriverDetailsInfo, strCalledFrom,dt,ref strXml);
		}


		public void AddPolAssignedVehicles(DataWrapper objDataWrapper,ClsPolicyDriverInfo objDriverDetailsInfo, string strCalledFrom,ref string sbStrXml)
		{
			DataTable dt = new DataTable();
			AddPolAssignedVehicles(objDataWrapper,"",objDriverDetailsInfo,strCalledFrom,dt,ref sbStrXml);
		}
		public void AddPolAssignedVehicles(DataWrapper objDataWrapper,ClsPolicyDriverInfo objDriverDetailsInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			AddPolAssignedVehicles(objDataWrapper,"",objDriverDetailsInfo,strCalledFrom,dt,ref sbStrXml);
		}
		public void AddPolAssignedVehicles(DataWrapper objDataWrapper,string strAssignXml,ClsPolicyDriverInfo objDriverDetailsInfo, string strCalledFrom,DataTable dt,ref string sbStrXml)
		{
			try 
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@PolID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@PolVersionID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DriverID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.ExecuteNonQuery("Proc_DeletePolAssignedVehicle");
				objDataWrapper.ClearParameteres();
				int returnResult = 0;
				StringBuilder sbTranXML=new StringBuilder();
				sbTranXML.Append("<LabelFieldMapping>");
				string strTranXML="";
				if(!(objDriverDetailsInfo.ASSIGNED_VEHICLE=="" || (strCalledFrom.ToUpper()=="PPA" && objDriverDetailsInfo.DRIVER_DRIV_TYPE!="11603")))//11603-Licensed Driver Type
				{
					int vehicleIndex = 0;
					foreach (string str in objDriverDetailsInfo.ASSIGNED_VEHICLE.Split('|'))
					{
						objDataWrapper.ClearParameteres();
						objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
						objDataWrapper.AddParameter("@PolID",objDriverDetailsInfo.POLICY_ID);
						objDataWrapper.AddParameter("@PolVersionID",objDriverDetailsInfo.POLICY_VERSION_ID);
						objDataWrapper.AddParameter("@DriverID",objDriverDetailsInfo.DRIVER_ID);
						objDataWrapper.AddParameter("@VehID",str.Split('~')[0]);
						if(objDriverDetailsInfo.IN_MILITARY.ToString()=="10963" && objDriverDetailsInfo.STATIONED_IN_US_TERR.ToString()=="10964")
						{
							objDataWrapper.AddParameter("@PrinOccID","11931");	
						}
						else
						{
							objDataWrapper.AddParameter("@PrinOccID",str.Split('~')[1]);	
						}
						returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolAssignedVehicle");
						if(TransactionLogRequired)
						{
                            objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/AddPolicyDriver.aspx.resx");
							SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
							DataRow[] drMake = dt.Select("VEHICLE_ID=" + str.Split('~')[0]);
							string strMakeModel = drMake[0]["MODEL_MAKE"].ToString();
////////////////

							XmlDocument objAssignXml = new XmlDocument();
							if(strAssignXml != null && strAssignXml != "")
							{
								objAssignXml.LoadXml(strAssignXml);
								string strOldAssign = objAssignXml.SelectSingleNode("NewDataSet").ChildNodes.Item(vehicleIndex).ChildNodes.Item(3).InnerXml;
								if(strOldAssign !="" )
								{
									/*Manoj Rathore Itrack # 5815 on 6th May 2009
										strTranXML= strTranXML + "<Map label=\"Drive \" field=\"VEHICLE_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
										strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='" + strOldAssign + "' NewValue='"+ str.Split('~')[1] + "' />";
									 */

									strTranXML= strTranXML + "<Map label=\"Drive \" field=\"VEHICLE_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
									strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strOldAssign) + "\" NewValue=\""+ RemoveJunkXmlCharactersRates(str.Split('~')[1]) + "\" />";
								}
							}
							else
							{
								/*Manoj Rathore Itrack # 5815 on 6th May 2009
										strTranXML= strTranXML + "<Map label=\"Drive \" field=\"VEHICLE_ID\" OldValue='' NewValue='" + strMakeModel + "'/>";
									strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue='' NewValue='"+ str.Split('~')[1] + "' />";
								 */
								strTranXML= strTranXML + "<Map label=\"Drive \" field=\"VEHICLE_ID\" OldValue=\"\" NewValue=\"" + ClsCommon.RemoveJunkXmlCharacters(strMakeModel) + "\"/>";
								strTranXML= strTranXML + "<Map label=\"as \" field=\"APP_VEHICLE_PRIN_OCC_ID\" OldValue=\"\" NewValue=\""+ ClsCommon.RemoveJunkXmlCharacters(str.Split('~')[1]) + "\" />";
							}
							sbTranXML.Append(strTranXML);
							strTranXML="";
						}
						else
						{
					
						}
						vehicleIndex++;
					}
					sbTranXML.Append("</LabelFieldMapping>");
					sbStrXml = sbTranXML.ToString();
				}
			}
			catch (Exception ex)
			{
				throw(ex);
			}
			finally
			{
			
			}
	}
		

		public int UpdateAppDriverDetails(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo, string strCalledFrom, string strCustomInfo,string strAssignXml)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				AddParameters(objDriverDetailsInfo,objDataWrapper,'U');

				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);
				objDataWrapper.AddParameter("@SAFE_DRIVER",objDriverDetailsInfo.SAFE_DRIVER);

				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY", null);
				objDataWrapper.AddParameter("@CREATED_DATETIME", null);
				objDataWrapper.AddParameter("@MODIFIED_BY", objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDriverDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);

				//// Added By anurag Verma on 15/09/2005 for merging screens
				objDataWrapper.AddParameter("@Good_Driver_Student_Discount",objDriverDetailsInfo.Good_Driver_Student_Discount);
				objDataWrapper.AddParameter("@Premier_Driver_Discount",objDriverDetailsInfo.Premier_Driver_Discount);
				//objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLEID);

				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);
				
				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				if (objDriverDetailsInfo.VEHICLE_ID != null)
					objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				else
					objDataWrapper.AddParameter("@VEHICLE_ID",0);

				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom); 
				if(strCalledFrom.ToString()=="PPA")
					objDataWrapper.AddParameter("@NO_DEPENDENTS",objDriverDetailsInfo.NO_DEPENDENTS);

				//added by vj on 17-10-2005 for vehicle assigned principal / occasional
				if(objDriverDetailsInfo.IN_MILITARY.ToString() =="10963" && objDriverDetailsInfo.STATIONED_IN_US_TERR.ToString() =="10964")
				{
					objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID","11931"); 
				}
				else
				{
					objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				}
				objDataWrapper.AddParameter("@WAIVER_WORK_LOSS_BENEFITS",objDriverDetailsInfo.WAIVER_WORK_LOSS_BENEFITS);
				objDataWrapper.AddParameter("@FORM_F95",objDriverDetailsInfo.FORM_F95); 
				objDataWrapper.AddParameter("@EXT_NON_OWN_COVG_INDIVI",objDriverDetailsInfo.EXT_NON_OWN_COVG_INDIVI); 
				objDataWrapper.AddParameter("@HAVE_CAR",objDriverDetailsInfo.HAVE_CAR);
				objDataWrapper.AddParameter("@STATIONED_IN_US_TERR",objDriverDetailsInfo.STATIONED_IN_US_TERR); 
				objDataWrapper.AddParameter("@IN_MILITARY",objDriverDetailsInfo.IN_MILITARY);  
				objDataWrapper.AddParameter("@FULL_TIME_STUDENT",objDriverDetailsInfo.FULL_TIME_STUDENT);  
				objDataWrapper.AddParameter("@SUPPORT_DOCUMENT",objDriverDetailsInfo.SUPPORT_DOCUMENT);  
				objDataWrapper.AddParameter("@SIGNED_WAIVER_BENEFITS_FORM",objDriverDetailsInfo.SIGNED_WAIVER_BENEFITS_FORM);  
				objDataWrapper.AddParameter("@PARENTS_INSURANCE",objDriverDetailsInfo.PARENTS_INSURANCE);

//				if(TransactionLogRequired) 
//				{
//
//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);					
//					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
//						returnResult	=	objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS);
//					else				
//					{
//						objTransactionInfo.TRANS_TYPE_ID	=	3;
//						objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
//						objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
//						objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
//						objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
//						objTransactionInfo.TRANS_DESC		=	"Driver is modified";
//						objTransactionInfo.CHANGE_XML		=	strTranXML;
//						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
//						returnResult = objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS, objTransactionInfo);
//					}
//				}
//				else
//				{
					returnResult = objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS);
//				}

				objDataWrapper.ClearParameteres();
				/*objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@AppID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@AppVersionID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DriverID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.ExecuteNonQuery("Proc_DeleteAssignedVehicle");

				if(objDriverDetailsInfo.ASSIGNED_VEHICLE!="")
				{
					foreach (string str in objDriverDetailsInfo.ASSIGNED_VEHICLE.Split('|'))
					{
						objDataWrapper.ClearParameteres();
						objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
						objDataWrapper.AddParameter("@AppID",objDriverDetailsInfo.APP_ID);
						objDataWrapper.AddParameter("@AppVersionID",objDriverDetailsInfo.APP_VERSION_ID);
						objDataWrapper.AddParameter("@DriverID",objDriverDetailsInfo.DRIVER_ID);
						objDataWrapper.AddParameter("@VehID",str.Split('~')[0]);
						objDataWrapper.AddParameter("@PrinOccID",str.Split('~')[1]);					
						objDataWrapper.ExecuteNonQuery("Proc_InsertAssignedVehicle");
					}
				}*/
				
				DataTable dtVehicle =new DataTable();
				dtVehicle=ClsVehicleInformation.GetAppVehicle(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDriverDetailsInfo.DRIVER_ID);
				string sbStrXml="";
				AddAppAssignedVehicles(objDataWrapper,strAssignXml,objDriverDetailsInfo,strCalledFrom,dtVehicle,ref sbStrXml);
				//AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo, strCalledFrom,dtVehicle,ref sbStrXml);
				//sbStrXml = sbStrXml.Remove("<LabelFieldMapping>");
				//int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
					//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_ID']");
					strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1596", "");// "Driver is modified";
					//strTranXML = strTranXML.Remove("</LabelFieldMapping>","");
					//strTranXML.Append(sbStrXml);
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				///Update Endorsements based on Driver attributes/////////////////////////////////////////////
				//this.UpdateDriverEndorsements(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.AutoDriverDep);
				//////////
			
				objDataWrapper.ClearParameteres();
				//Update the Vehicle Class : 16 May 2006
				//UpdateVehicleClass(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID ,objDriverDetailsInfo.APP_VERSION_ID);
				//End Update the Vehicle Class
				if(returnResult>0)
					UpdateVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID, objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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

		#region UpdateMotorDriverDetails method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldDriverDetailsInfo">Model object having old information</param>
		/// <param name="objDriverDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateMotorDriverDetails(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom)
		{
			return UpdateMotorDriverDetails(objOldDriverDetailsInfo,objDriverDetailsInfo,strCalledFrom,"");
			//			string strTranXML;
			//			int returnResult = 0;
			//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//
			//			try 
			//			{
			//				AddParameters(objDriverDetailsInfo,objDataWrapper,'U');
			//
			//				objDataWrapper.AddParameter("@DRIVER_FAX",objDriverDetailsInfo.DRIVER_FAX);
			//				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
			//				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
			//				objDataWrapper.AddParameter("@SAFE_DRIVER",null);
			//
			//				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
			//				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
			//				objDataWrapper.AddParameter("@CREATED_BY", null);
			//				objDataWrapper.AddParameter("@CREATED_DATETIME", null);
			//				objDataWrapper.AddParameter("@MODIFIED_BY", objDriverDetailsInfo.MODIFIED_BY);
			//				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDriverDetailsInfo.LAST_UPDATED_DATETIME);
			//				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
			//				//objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
			//			//	objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
			//
			//				//// Added By anurag Verma on 19/09/2005 for merging screens
			//				objDataWrapper.AddParameter("@Mature_Driver",objDriverDetailsInfo.Mature_Driver);
			//				objDataWrapper.AddParameter("@Mature_Driver_Discount",objDriverDetailsInfo.Mature_Driver_Discount);
			//				objDataWrapper.AddParameter("@Preferred_Risk_Discount",objDriverDetailsInfo.Preferred_Risk_Discount);
			//				objDataWrapper.AddParameter("@Preferred_Risk",objDriverDetailsInfo.Preferred_Risk);
			//				objDataWrapper.AddParameter("@TransferExp_Renewal_Discount",objDriverDetailsInfo.TransferExp_Renewal_Discount);
			//				objDataWrapper.AddParameter("@TransferExperience_RenewalCredit",objDriverDetailsInfo.TransferExperience_RenewalCredit );
			//
			//
			//				//objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLEID);
			//				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
			//				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
			//
			//				//added by vj on 17-10-2005
			//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID); 
			//				objDataWrapper.AddParameter("@Called_From",strCalledFrom); 
			//
			//				if(TransactionLogRequired) 
			//				{
			//
			//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddMotorDriverDetails.aspx.resx");
			//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			//					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);
			//
			//					objTransactionInfo.TRANS_TYPE_ID	=	3;
			//					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
			//					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
			//					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
			//					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
			//					objTransactionInfo.TRANS_DESC		=	"Driver is modified";
			//					objTransactionInfo.CHANGE_XML		=	strTranXML;
			//					returnResult = objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS, objTransactionInfo);
			//
			//				}
			//				else
			//				{
			//					returnResult = objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS);
			//				}
			//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			//				return returnResult;
			//			}
			//			catch(Exception ex)
			//			{
			//				throw(ex);
			//			}
			//			finally
			//			{
			//				if(objDataWrapper != null) 
			//				{
			//					objDataWrapper.Dispose();
			//				}
			//				if(objBuilder != null) 
			//				{
			//					objBuilder = null;
			//				}
			//			}
		}

		public int UpdateMotorDriverDetails(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom, string strCustomInfo)
		{
			return UpdateMotorDriverDetails(objOldDriverDetailsInfo,objDriverDetailsInfo,strCalledFrom,strCustomInfo,"");
		}
		public int UpdateMotorDriverDetails(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo,string strCalledFrom, string strCustomInfo,string strAssignXml)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				AddParameters(objDriverDetailsInfo,objDataWrapper,'U');

				//objDataWrapper.AddParameter("@DRIVER_FAX",objDriverDetailsInfo.DRIVER_FAX);
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				//objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
				objDataWrapper.AddParameter("@SAFE_DRIVER",null);

				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY", null);
				objDataWrapper.AddParameter("@CREATED_DATETIME", null);
				objDataWrapper.AddParameter("@MODIFIED_BY", objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDriverDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				//objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
				//objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);

				//// Added By anurag Verma on 19/09/2005 for merging screens
				objDataWrapper.AddParameter("@Mature_Driver",objDriverDetailsInfo.Mature_Driver);
				objDataWrapper.AddParameter("@Mature_Driver_Discount",objDriverDetailsInfo.Mature_Driver_Discount);
				objDataWrapper.AddParameter("@Preferred_Risk_Discount",objDriverDetailsInfo.Preferred_Risk_Discount);
				objDataWrapper.AddParameter("@Preferred_Risk",objDriverDetailsInfo.Preferred_Risk);
				
				//Commented by Charles on 2-Jul-09 for Itrack issue 6012
				//objDataWrapper.AddParameter("@TransferExp_Renewal_Discount",objDriverDetailsInfo.TransferExp_Renewal_Discount);			
				//objDataWrapper.AddParameter("@TransferExperience_RenewalCredit",objDriverDetailsInfo.TransferExperience_RenewalCredit );


				//objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLEID);
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);

				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 

				//added by vj on 17-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID); 
				objDataWrapper.AddParameter("@NO_CYCLE_ENDMT",objDriverDetailsInfo.NO_CYCLE_ENDMT); 
				objDataWrapper.AddParameter("@Called_From",strCalledFrom); 
				objDataWrapper.AddParameter("@CYCL_WITH_YOU",objDriverDetailsInfo.CYCL_WITH_YOU); 
				objDataWrapper.AddParameter("@COLL_STUD_AWAY_HOME",objDriverDetailsInfo.COLL_STUD_AWAY_HOME); 

//				if(TransactionLogRequired) 
//				{
//
//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddMotorDriverDetails.aspx.resx");
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);
//					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
//						returnResult = objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS);
//					else				
//					{	
//
//						objTransactionInfo.TRANS_TYPE_ID	=	3;
//						objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
//						objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
//						objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
//						objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
//						objTransactionInfo.TRANS_DESC		=	"Driver is modified";
//						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
//						objTransactionInfo.CHANGE_XML		=	strTranXML;
//						returnResult = objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS, objTransactionInfo);
//					}					
//
//				}
//				else
//				{
					returnResult = objDataWrapper.ExecuteNonQuery(INERT_APP_DRIVER_DETAILS);
//				}
				DataTable dtVehicle =new DataTable();
				dtVehicle=ClsVehicleInformation.GetAppVehicle(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDriverDetailsInfo.DRIVER_ID);
				string sbStrXml="";
				AddAppAssignedVehicles(objDataWrapper,strAssignXml,objDriverDetailsInfo,strCalledFrom,dtVehicle,ref sbStrXml);
				//AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo, strCalledFrom,ref sbStrXml);
				//sbStrXml = sbStrXml.Remove("<LabelFieldMapping>");
				//int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddMotorDriverDetails.aspx.resx");
					//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_ID']");
					strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1596", "");//"Driver is modified";
					//strTranXML = strTranXML.Remove("</LabelFieldMapping>","");
					//strTranXML.Append(sbStrXml);
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
						
				
				//AddAppAssignedVehicles(objDataWrapper,objDriverDetailsInfo,strCalledFrom);

				objDataWrapper.ClearParameteres();

				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages("MOTOR");
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.AutoDriverDep);
				objDataWrapper.ClearParameteres();
				//Call Class Update Method
				UpdateMotorVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID, objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
		
		
		#region UpdateUmbrellaDriverDetails method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldDriverDetailsInfo">Model object having old information</param>
		/// <param name="objDriverDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateUmbrellaDriverDetails(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				AddParameters(objDriverDetailsInfo,objDataWrapper,'U');

				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);

				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY", null);
				objDataWrapper.AddParameter("@CREATED_DATETIME", null);
				objDataWrapper.AddParameter("@MODIFIED_BY", objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDriverDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				
		
				//added by vj on 17-10-2005 for principal / occasional assigned vehicle
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				//objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLEID))); 
				objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLEID))); 

				//Added By Ravindra(02/27/2006) For Operator
				objDataWrapper.AddParameter("@OP_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_VEHICLE_ID)); 
				objDataWrapper.AddParameter("@OP_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_APP_VEHICLE_PRIN_OCC_ID)); 
				objDataWrapper.AddParameter("@OP_DRIVER_COST_GAURAD_AUX",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_DRIVER_COST_GAURAD_AUX )); 
				///////////////////////////////
				///
				if(TransactionLogRequired) 
				{

					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1596", "");//"Driver is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(INSERT_UMBRELLA_DRIVER_DETAILS, objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(INSERT_UMBRELLA_DRIVER_DETAILS);
				}
				//for Umbrella Coverages by Pravesh
				ClsUmbrellaCoverages objUmbCoverage=new ClsUmbrellaCoverages();
				objUmbCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.RiskDependent,0);			
				///end 
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

		/// Sumit Chhabra:03/12/2007
		/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
		public int UpdatePolicyUmbrellaDriver(Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo objOldDriverDetailsInfo,Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo objDriverDetailsInfo, string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",null);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",null);
				objDataWrapper.AddParameter("@DRIVER_ADD1",null);
				objDataWrapper.AddParameter("@DRIVER_ADD2",null);
				objDataWrapper.AddParameter("@DRIVER_CITY",null);
				objDataWrapper.AddParameter("@DRIVER_STATE",null);
				objDataWrapper.AddParameter("@DRIVER_ZIP",null);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",null);
				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",null);
				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",null);
				objDataWrapper.AddParameter("@DRIVER_EXT",null);
				objDataWrapper.AddParameter("@DRIVER_MOBILE",null);
				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);
				objDataWrapper.AddParameter("@DRIVER_SSN",null);
				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);
				objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",null);
				if (objDriverDetailsInfo.DATE_LICENSED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_LICENSED",objDriverDetailsInfo.DATE_LICENSED);
				else
					objDataWrapper.AddParameter("@DATE_LICENSED",null);

				objDataWrapper.AddParameter("@DRIVER_REL",null);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
				objDataWrapper.AddParameter("@DRIVER_OCC_CODE",null);
				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",null);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",null);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",null);
				objDataWrapper.AddParameter("@DRIVER_INCOME",null);
				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",null);
				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",null);
				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",null);
				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",null);
				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",null);
				objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",null);
				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",null);
				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",null);
				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",null);

				objDataWrapper.AddParameter("@INSERTUPDATE","U");

				objDataWrapper.AddParameter("@RELATIONSHIP",null);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);

				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY", null);
				objDataWrapper.AddParameter("@CREATED_DATETIME", null);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				///////
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLE_ID))); 
				objDataWrapper.AddParameter("@OP_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_VEHICLE_ID)); 
				objDataWrapper.AddParameter("@OP_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_APP_VEHICLE_PRIN_OCC_ID)); 
				objDataWrapper.AddParameter("@OP_DRIVER_COST_GAURAD_AUX",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_DRIVER_COST_GAURAD_AUX )); 
				objDataWrapper.AddParameter("@MOT_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.MOT_VEHICLE_ID)); 
				objDataWrapper.AddParameter("@MOT_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.MOT_APP_VEHICLE_PRIN_OCC_ID)); 
//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);

				///////
//				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",null);
//				objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLE_ID))); 
//				objDataWrapper.AddParameter("@OP_VEHICLE_ID",null);
//				objDataWrapper.AddParameter("@OP_APP_VEHICLE_PRIN_OCC_ID",null);
//				objDataWrapper.AddParameter("@OP_DRIVER_COST_GAURAD_AUX",null);
				objDataWrapper.AddParameter("@FORM_F95",objDriverDetailsInfo.FORM_F95); 
//				objDataWrapper.AddParameter("@MOT_APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.MOT_APP_VEHICLE_PRIN_OCC_ID); 
//				objDataWrapper.AddParameter("@MOT_VEHICLE_ID",objDriverDetailsInfo.MOT_VEHICLE_ID); 
				if(TransactionLogRequired) 
				{

					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/UmbDriverDetails.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID		=	objDriverDetailsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objDriverDetailsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1596", "");//"Driver is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaPolicyDriver", objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaPolicyDriver");
				}
				//for Umbrella Coverages by Pravesh
				ClsUmbrellaCoverages objUmbCoverage=new ClsUmbrellaCoverages();
				objUmbCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,RuleType.RiskDependent,0);			
				///end 
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

		public int UpdateUmbrellaDriverDetails(ClsDriverDetailsInfo objOldDriverDetailsInfo,ClsDriverDetailsInfo objDriverDetailsInfo, string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				AddParameters(objDriverDetailsInfo,objDataWrapper,'U');

				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);

				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CREATED_BY", null);
				objDataWrapper.AddParameter("@CREATED_DATETIME", null);
				objDataWrapper.AddParameter("@MODIFIED_BY", objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDriverDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);

				//added by vj on 17-10-2005 for principal / occasional assigned vehicle
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				//objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLEID))); 
				objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNullFromNegative(Convert.ToInt32(objDriverDetailsInfo.VEHICLE_ID))); 

				//Added By Ravindra(02/27/2006) For Operator
				objDataWrapper.AddParameter("@OP_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_VEHICLE_ID)); 
				objDataWrapper.AddParameter("@OP_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_APP_VEHICLE_PRIN_OCC_ID)); 
				objDataWrapper.AddParameter("@OP_DRIVER_COST_GAURAD_AUX",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.OP_DRIVER_COST_GAURAD_AUX )); 
				///////////////////////////////
				objDataWrapper.AddParameter("@MOT_VEHICLE_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.MOT_VEHICLE_ID)); 
				objDataWrapper.AddParameter("@MOT_APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.MOT_APP_VEHICLE_PRIN_OCC_ID)); 
				if(TransactionLogRequired) 
				{

					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/UmbDriverDetails.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1596", "");//"Driver is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					returnResult = objDataWrapper.ExecuteNonQuery(INSERT_UMBRELLA_DRIVER_DETAILS, objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(INSERT_UMBRELLA_DRIVER_DETAILS);
				}
				//for Umbrella Coverages by Pravesh
				ClsUmbrellaCoverages objUmbCoverage=new ClsUmbrellaCoverages();
				objUmbCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.RiskDependent,0);			
				///end 
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
		


		#region GetAppDriverDetailsXML

		public static string GetAppDriverDetailsXML(int intCustomerId, int intAppId, int intAppVersionId, int intDriverId)
		{

			DataSet dsPolicyInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);

				dsPolicyInfo = objDataWrapper.ExecuteDataSet(GET_APP_DRIVER_DETAILS_PROC);
				
				if (dsPolicyInfo.Tables[0].Rows.Count != 0)
				{
					//return dsPolicyInfo.GetXml();
					return ClsCommon.GetXMLEncoded(dsPolicyInfo.Tables[0]);
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
		public static string GetUmbDriverDetailsXML(int intCustomerId, int intAppId, int intAppVersionId, int intDriverId)
		{

			DataSet dsPolicyInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);

				dsPolicyInfo = objDataWrapper.ExecuteDataSet(GET_UMB_DRIVER_DETAILS_PROC);
				
				if (dsPolicyInfo.Tables[0].Rows.Count != 0)
				{
					return dsPolicyInfo.GetXml();
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

		public static string GetPolicyUmbrellaDriverDetailsXML(int intCustomerId, int intPolicyID, int intPolicyVersionID, int intDriverId)
		{

			DataSet dsPolicyInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",intPolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);

				dsPolicyInfo = objDataWrapper.ExecuteDataSet("Proc_GetPolicyUmbrellaDriverDetails");
				
				if (dsPolicyInfo.Tables[0].Rows.Count != 0)
				{
					return dsPolicyInfo.GetXml();
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



		#region GetPolDriverDetailsXML

		public static string GetPolDriverDetailsXML(int intCustomerId, int intPolId, int intPolVersionId, int intDriverId)
		{

			DataSet dsPolicyInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@POL_ID",intPolId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);

				dsPolicyInfo = objDataWrapper.ExecuteDataSet(GET_POL_DRIVER_DETAILS_PROC);
				
				if (dsPolicyInfo.Tables[0].Rows.Count != 0)
				{
					return ClsCommon.GetXMLEncoded(dsPolicyInfo.Tables[0]);
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




		#region activate deactivate
		public void ActivateDeactivateCustomerDriver(int CustomerId, int intDriverId, string strStatus)
		{	
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			string strStoredProc = "Proc_ActivateDeactivateCustomerDriverDetails";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
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

		public void ActivateDeactivateAppDriver(int CustomerId, int AppId, int AppVersionId, int intDriverId, string strStatus)
		{
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			string strStoredProc = "Proc_ActivateDeactivateAppDriverDetails";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@APP_ID",AppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				objDataWrapper.ExecuteNonQuery(strStoredProc);
				//Upating the class while Deactivating and Activating the Drivers :
				UpdateVehicleClass(CustomerId,AppId,AppVersionId);
				//End Update the Vehicle Class
				
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		
		public void ActivateDeactivateUmbDriver(int CustomerId, int AppId, int AppVersionId, int intDriverId, string strStatus)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			string strStoredProc = "Proc_ActivateDeactivateUmbDriverDetails";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@APP_ID",AppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				objDataWrapper.ExecuteNonQuery(strStoredProc);
				//added by pravesh
				ClsUmbrellaCoverages objUmbCoverage=new ClsUmbrellaCoverages();
				objUmbCoverage.UpdateCoveragesByRuleApp(objDataWrapper,CustomerId,AppId,AppVersionId,RuleType.RiskDependent,0);     
				//end 
								
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
		public void ActivateDeactivateAppDriver(ClsDriverDetailsInfo objDriverDetailsInfo, string strStatus,string strCustomInfo,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAppDriverDetails";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);				

				int returnResult = 0;
				if(TransactionLogRequired)
				{						
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddDriverDetails.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1601","");//"Driver is Activated";
					else
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1602", "");//"Driver is Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				if(returnResult>0)
					UpdateMotorVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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

		public void ActivateDeactivateAppDriver(ClsDriverDetailsInfo objDriverDetailsInfo, string strStatus,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAppDriverDetails";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{						
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddDriverDetails.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
                        objTransactionInfo.TRANS_DESC   =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1601", "");//"Driver is Activated";
					else
                        objTransactionInfo.TRANS_DESC   =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1602", "");//	"Driver is Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();

				//Update Endorsements//////////
				//this.UpdateDriverEndorsements(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.AutoDriverDep);
				objDataWrapper.ClearParameteres();
				UpdateVehicleClassNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,objDataWrapper);
				////////////////////////////
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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
		/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
		public void ActivateDeactivateUmbDriver(ClsDriverDetailsInfo objDriverDetailsInfo, string strStatus,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateUmbDriverDetails";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{						
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddDriverDetails.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objDriverDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objDriverDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
                        objTransactionInfo.TRANS_DESC   =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1601", "");//"Driver is Activated";
					else
                        objTransactionInfo.TRANS_DESC   =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1602", "");//"Driver is Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}		
				//added by pravesh
				ClsUmbrellaCoverages objUmbCoverage=new ClsUmbrellaCoverages();
				objUmbCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.APP_ID,objDriverDetailsInfo.APP_VERSION_ID,RuleType.RiskDependent,0);     
				//end 

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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

		//		public void ActivateDeactivateUmbOperator(ClsUmbrellaOperatorInfo objUmbrellaOperatorInfo , string strStatus,string strCustomInfo)
		//		{
		//			string		strStoredProc	=	"Proc_ActivateDeactivateUmbrellaOperator";			
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//
		//			try
		//			{
		//				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaOperatorInfo.CUSTOMER_ID);
		//				objDataWrapper.AddParameter("@APP_ID",objUmbrellaOperatorInfo.APP_ID);
		//				objDataWrapper.AddParameter("@APP_VERSION_ID",objUmbrellaOperatorInfo.APP_VERSION_ID);
		//				objDataWrapper.AddParameter("@DRIVER_ID",objUmbrellaOperatorInfo.DRIVER_ID);
		//				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
		//
		//				int returnResult = 0;
		//				if(TransactionLogRequired)
		//				{						
		//					objUmbrellaOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddDriverDetails.aspx.resx");
		//					
		//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//					objTransactionInfo.TRANS_TYPE_ID	=	1;
		//					objTransactionInfo.APP_ID			=	objUmbrellaOperatorInfo.APP_ID;
		//					objTransactionInfo.APP_VERSION_ID	=	objUmbrellaOperatorInfo.APP_VERSION_ID;
		//					objTransactionInfo.CLIENT_ID		=	objUmbrellaOperatorInfo.CUSTOMER_ID;
		//					objTransactionInfo.RECORDED_BY		=	objUmbrellaOperatorInfo.MODIFIED_BY;
		//					if(strStatus.ToUpper()=="Y")
		//						objTransactionInfo.TRANS_DESC		=	"Operator is Activated";
		//					else
		//						objTransactionInfo.TRANS_DESC		=	"Operator is Deactivated";
		//					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
		//					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		//				}
		//				else
		//				{
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
		//				}				
		//				objDataWrapper.ClearParameteres();
		//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
		//			}
		//			catch(Exception ex)
		//			{
		//				throw(ex);				
		//			}
		//			finally
		//			{
		//				if(objDataWrapper != null) objDataWrapper.Dispose();
		//			}	
		//
		//			
		//		}

		/*public void ActivateDeactivateWatercraftOperator(ClsWatercraftOperatorInfo objWatercraftOperatorInfo, string strStatus,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateWatercraftOperator";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftOperatorInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftOperatorInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{						
					objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Watercraft/PolicyAddWatercraftOperator.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID=	objWatercraftOperatorInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")              
						objTransactionInfo.TRANS_DESC		=	"Operator is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Operator is Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
			}
			catch(Exception ex)
			{
				throw(ex);				
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}	

			
		}*/
		#endregion

		#region WATERCRAFT
		
		#region "ACORD"
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objWatercraftOperatorInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/*public int AddOperator(ClsWatercraftOperatorInfo objWatercraftOperatorInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertWatercraftOperatorInfo_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
				 
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

		
			objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objWatercraftOperatorInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftOperatorInfo.APP_VERSION_ID);	
			objDataWrapper.AddParameter("@DRIVER_FNAME",objWatercraftOperatorInfo.DRIVER_FNAME);
			objDataWrapper.AddParameter("@DRIVER_MNAME",objWatercraftOperatorInfo.DRIVER_MNAME);
			objDataWrapper.AddParameter("@DRIVER_LNAME",objWatercraftOperatorInfo.DRIVER_LNAME);
			objDataWrapper.AddParameter("@DRIVER_CODE",objWatercraftOperatorInfo.DRIVER_CODE);
			objDataWrapper.AddParameter("@DRIVER_SUFFIX",objWatercraftOperatorInfo.DRIVER_SUFFIX);
			objDataWrapper.AddParameter("@DRIVER_ADD1",objWatercraftOperatorInfo.DRIVER_ADD1);
			objDataWrapper.AddParameter("@DRIVER_ADD2",objWatercraftOperatorInfo.DRIVER_ADD2);
			objDataWrapper.AddParameter("@DRIVER_CITY",objWatercraftOperatorInfo.DRIVER_CITY);
			objDataWrapper.AddParameter("@DRIVER_STATE",objWatercraftOperatorInfo.DRIVER_STATE);
			objDataWrapper.AddParameter("@DRIVER_ZIP",objWatercraftOperatorInfo.DRIVER_ZIP);
			objDataWrapper.AddParameter("@DRIVER_COUNTRY",objWatercraftOperatorInfo.DRIVER_COUNTRY);
			objDataWrapper.AddParameter("@MARITAL_STATUS",objWatercraftOperatorInfo.MARITAL_STATUS);
				
			if(objWatercraftOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
			{
				objDataWrapper.AddParameter("@DRIVER_DOB",objWatercraftOperatorInfo.DRIVER_DOB);
			}
				
				
			objDataWrapper.AddParameter("@DRIVER_SSN",objWatercraftOperatorInfo.DRIVER_SSN);
				
			objDataWrapper.AddParameter("@DRIVER_SEX",objWatercraftOperatorInfo.DRIVER_SEX);
			objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objWatercraftOperatorInfo.DRIVER_DRIV_LIC);
			objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objWatercraftOperatorInfo.DRIVER_LIC_STATE);
				
			if(objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
			{
				objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX);
			}
			else
			{
				objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
			}
				
			objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objWatercraftOperatorInfo.EXPERIENCE_CREDIT );
			objDataWrapper.AddParameter("@VEHICLE_ID",objWatercraftOperatorInfo.VEHICLE_ID);
			objDataWrapper.AddParameter("@PERCENT_DRIVEN",objWatercraftOperatorInfo.PERCENT_DRIVEN);
				
			objDataWrapper.AddParameter("@CREATED_BY",objWatercraftOperatorInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftOperatorInfo.CREATED_DATETIME);
			
			//add By kranti on 27th April 2007
			//for driver violations
			objDataWrapper.AddParameter("@VIOLATIONS",objWatercraftOperatorInfo.VIOLATIONS);
			
			//added by vj on 17-10-2005
			objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
				 
			// Years Licensed
			objDataWrapper.AddParameter("@YEARS_LICENSED",objWatercraftOperatorInfo.YEARS_LICENSED);
			//Saftey course
			objDataWrapper.AddParameter("@WAT_SAFETY_COURSE",objWatercraftOperatorInfo.WAT_SAFETY_COURSE);
			objDataWrapper.AddParameter("@CERT_COAST_GUARD",objWatercraftOperatorInfo.CERT_COAST_GUARD);

			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);

			int returnResult = 0;
			if(TransactionLogRequired)
			{
				objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftOperatorInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.CREATED_BY;
				objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID	=	objWatercraftOperatorInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
				objTransactionInfo.TRANS_DESC		=	"New watercraft operator information is added from Quick quote.";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
					
				objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				objDataWrapper.ClearParameteres();

				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
						
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}

				
			objWatercraftOperatorInfo.DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				
				
			objDataWrapper.ClearParameteres();
				
			//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			return objWatercraftOperatorInfo.DRIVER_ID;
		
		}*/

		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objWatercraftOperatorInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/*public int AddWatercraftOperator(ClsWatercraftOperatorInfo objWatercraftOperatorInfo)
		{
			string		strStoredProc	=	"Proc_InsertWatercraftOperatorInfo";
			DateTime	RecordDate		=	DateTime.Now;
				 
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftOperatorInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftOperatorInfo.APP_VERSION_ID);
				 
				
				objDataWrapper.AddParameter("@DRIVER_FNAME",objWatercraftOperatorInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objWatercraftOperatorInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objWatercraftOperatorInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objWatercraftOperatorInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objWatercraftOperatorInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objWatercraftOperatorInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objWatercraftOperatorInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objWatercraftOperatorInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objWatercraftOperatorInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objWatercraftOperatorInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objWatercraftOperatorInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@MARITAL_STATUS",objWatercraftOperatorInfo.MARITAL_STATUS);
				objDataWrapper.AddParameter("@VIOLATIONS",objWatercraftOperatorInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objWatercraftOperatorInfo.MVR_ORDERED);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objWatercraftOperatorInfo.DRIVER_DRIV_TYPE);

				if(objWatercraftOperatorInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objWatercraftOperatorInfo.DATE_ORDERED);
				}
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objWatercraftOperatorInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objWatercraftOperatorInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objWatercraftOperatorInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objWatercraftOperatorInfo.MVR_DRIV_LIC_APPL);
				
				objDataWrapper.AddParameter("@MVR_REMARKS",objWatercraftOperatorInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objWatercraftOperatorInfo.MVR_STATUS);

				if(objWatercraftOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DRIVER_DOB",objWatercraftOperatorInfo.DRIVER_DOB);
				}
				
				
				objDataWrapper.AddParameter("@DRIVER_SSN",objWatercraftOperatorInfo.DRIVER_SSN);
				
				objDataWrapper.AddParameter("@DRIVER_SEX",objWatercraftOperatorInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objWatercraftOperatorInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objWatercraftOperatorInfo.DRIVER_LIC_STATE);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objWatercraftOperatorInfo.DRIVER_DRIV_TYPE);
				if(objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX);
				}
				else
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
				}
				
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objWatercraftOperatorInfo.EXPERIENCE_CREDIT );
				objDataWrapper.AddParameter("@VEHICLE_ID",objWatercraftOperatorInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objWatercraftOperatorInfo.PERCENT_DRIVEN);
				
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftOperatorInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftOperatorInfo.CREATED_DATETIME);
			
				//added by vj on 17-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
				 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);


				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftOperatorInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftOperatorInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New watercraft operator information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				if (returnResult > 0)
				{
					objWatercraftOperatorInfo.DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
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
        */
        //public int AddWatercraftOperator(ClsWatercraftOperatorInfo objWatercraftOperatorInfo, string strCustomInfo)
        //{
        //    /*Modified Assigned Boats 
        //     * Pkasana
        //     * : 12 sep 2007*/
        //    string		strStoredProc	=	"Proc_InsertWatercraftOperatorInfo";
        //    DateTime	RecordDate		=	DateTime.Now;
				 
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

        //    try
        //    {
        //        objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@APP_ID",objWatercraftOperatorInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftOperatorInfo.APP_VERSION_ID);
				 
				
        //        objDataWrapper.AddParameter("@DRIVER_FNAME",objWatercraftOperatorInfo.DRIVER_FNAME);
        //        objDataWrapper.AddParameter("@DRIVER_MNAME",objWatercraftOperatorInfo.DRIVER_MNAME);
        //        objDataWrapper.AddParameter("@DRIVER_LNAME",objWatercraftOperatorInfo.DRIVER_LNAME);
        //        objDataWrapper.AddParameter("@DRIVER_CODE",objWatercraftOperatorInfo.DRIVER_CODE);
        //        objDataWrapper.AddParameter("@DRIVER_SUFFIX",objWatercraftOperatorInfo.DRIVER_SUFFIX);
        //        objDataWrapper.AddParameter("@DRIVER_ADD1",objWatercraftOperatorInfo.DRIVER_ADD1);
        //        objDataWrapper.AddParameter("@DRIVER_ADD2",objWatercraftOperatorInfo.DRIVER_ADD2);
        //        objDataWrapper.AddParameter("@DRIVER_CITY",objWatercraftOperatorInfo.DRIVER_CITY);
        //        objDataWrapper.AddParameter("@DRIVER_STATE",objWatercraftOperatorInfo.DRIVER_STATE);
        //        objDataWrapper.AddParameter("@DRIVER_ZIP",objWatercraftOperatorInfo.DRIVER_ZIP);
        //        objDataWrapper.AddParameter("@DRIVER_COUNTRY",objWatercraftOperatorInfo.DRIVER_COUNTRY);
        //        objDataWrapper.AddParameter("@MARITAL_STATUS",objWatercraftOperatorInfo.MARITAL_STATUS);
        //        objDataWrapper.AddParameter("@VIOLATIONS",objWatercraftOperatorInfo.VIOLATIONS);
        //        objDataWrapper.AddParameter("@MVR_ORDERED",objWatercraftOperatorInfo.MVR_ORDERED);
        //        objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objWatercraftOperatorInfo.DRIVER_DRIV_TYPE);

        //        if(objWatercraftOperatorInfo.DATE_ORDERED!=DateTime.MinValue)
        //        {
        //            objDataWrapper.AddParameter("@DATE_ORDERED",objWatercraftOperatorInfo.DATE_ORDERED);
        //        }
				
        //        //Added by Mohit Agarwal 29-Jun-07 ITrack 2030
        //        objDataWrapper.AddParameter("@MVR_CLASS",objWatercraftOperatorInfo.MVR_CLASS);
        //        objDataWrapper.AddParameter("@MVR_LIC_CLASS",objWatercraftOperatorInfo.MVR_LIC_CLASS);
        //        objDataWrapper.AddParameter("@MVR_LIC_RESTR",objWatercraftOperatorInfo.MVR_LIC_RESTR);
        //        objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objWatercraftOperatorInfo.MVR_DRIV_LIC_APPL);
        //        objDataWrapper.AddParameter("@MVR_REMARKS",objWatercraftOperatorInfo.MVR_REMARKS);
        //        objDataWrapper.AddParameter("@MVR_STATUS",objWatercraftOperatorInfo.MVR_STATUS);

        //        if(objWatercraftOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
        //        {
        //            objDataWrapper.AddParameter("@DRIVER_DOB",objWatercraftOperatorInfo.DRIVER_DOB);
        //        }
				
				
        //        objDataWrapper.AddParameter("@DRIVER_SSN",objWatercraftOperatorInfo.DRIVER_SSN);
				
        //        objDataWrapper.AddParameter("@DRIVER_SEX",objWatercraftOperatorInfo.DRIVER_SEX);
        //        objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objWatercraftOperatorInfo.DRIVER_DRIV_LIC);
        //        objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objWatercraftOperatorInfo.DRIVER_LIC_STATE);
        //        objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",DefaultValues.GetIntNull(objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX));
        //        objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objWatercraftOperatorInfo.EXPERIENCE_CREDIT );
        //        objDataWrapper.AddParameter("@VEHICLE_ID",objWatercraftOperatorInfo.VEHICLE_ID);
        //        objDataWrapper.AddParameter("@PERCENT_DRIVEN",objWatercraftOperatorInfo.PERCENT_DRIVEN);
        //        objDataWrapper.AddParameter("@CREATED_BY",objWatercraftOperatorInfo.CREATED_BY);
        //        objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftOperatorInfo.CREATED_DATETIME);
        //        //added by vj on 17-10-2005
        //        objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
        //        objDataWrapper.AddParameter("@WAT_SAFETY_COURSE",objWatercraftOperatorInfo.WAT_SAFETY_COURSE);
        //        objDataWrapper.AddParameter("@CERT_COAST_GUARD",objWatercraftOperatorInfo.CERT_COAST_GUARD);
        //        objDataWrapper.AddParameter("@REC_VEH_ID",objWatercraftOperatorInfo.REC_VEH_ID);
        //        objDataWrapper.AddParameter("@APP_REC_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_REC_VEHICLE_PRIN_OCC_ID);
 
        //        SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);


        //        //Change Trans log : 12 sep 2007 (Assigned Boats)
        //        int returnResult = 0;
        //        returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);

        //        int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
        //        if(DRIVER_ID!=-1)
        //        {
        //            objWatercraftOperatorInfo.DRIVER_ID = DRIVER_ID;
        //            string sbStrXml="",sbStrRecXml="";//Done for Itrack Issue 6737 on 17 Nov 09
        //            string calledfrom="";
        //            DataTable dtVehicle =new DataTable();
        //            DataTable dtRecVehicle =new DataTable();//Done for Itrack Issue 6737 on 17 Nov 09
        //            dtVehicle=clsWatercraftInformation.GetAppBoat(objWatercraftOperatorInfo.CUSTOMER_ID,objWatercraftOperatorInfo.APP_ID,objWatercraftOperatorInfo.APP_VERSION_ID,objWatercraftOperatorInfo.DRIVER_ID);
        //            //Done for Itrack Issue 6737 on 17 Nov 09
        //            dtRecVehicle=clsWatercraftInformation.GetAppReacreationalVehicles(objWatercraftOperatorInfo.CUSTOMER_ID,objWatercraftOperatorInfo.APP_ID,objWatercraftOperatorInfo.APP_VERSION_ID,objWatercraftOperatorInfo.DRIVER_ID);
        //            AddAppAssignedBoats(objDataWrapper,objWatercraftOperatorInfo,calledfrom,dtVehicle,ref sbStrXml);
        //            //Done for Itrack Issue 6737 on 17 Nov 09
        //            if(objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE != null && objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE != "")
        //              AddAppAssignedReacreationalVehicles(objDataWrapper,objWatercraftOperatorInfo,calledfrom,dtRecVehicle,ref sbStrRecXml);
					
        //            if(TransactionLogRequired)
        //            {
        //                objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
        //                SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //                string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftOperatorInfo);
        //                //Done for Itrack Issue 6737 on 17 Nov 09
        //                if(sbStrRecXml != "")
        //                 strTranXML= "<root>" + strTranXML + sbStrXml + sbStrRecXml + "</root>";
        //                else
        //                 strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
        //                Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //                objTransactionInfo.TRANS_TYPE_ID	=	1;
        //                objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.CREATED_BY;
        //                objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
        //                objTransactionInfo.APP_VERSION_ID	=	objWatercraftOperatorInfo.APP_VERSION_ID;
        //                objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
        //                objTransactionInfo.TRANS_DESC		=	"New watercraft operator information is added";
        //                objTransactionInfo.CHANGE_XML		=	strTranXML;
        //                objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
        //                //Executing the query
        //                //returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //                //Executing the query
        //                objDataWrapper.ExecuteNonQuery(objTransactionInfo);

        //            }
        //        }
        //        //End change 12 sep 2007

        //        /*int returnResult = 0;
        //        if(TransactionLogRequired)
        //        {
        //            objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
        //            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //            string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftOperatorInfo);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            objTransactionInfo.TRANS_TYPE_ID	=	1;
        //            objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.CREATED_BY;
        //            objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
        //            objTransactionInfo.APP_VERSION_ID	=	objWatercraftOperatorInfo.APP_VERSION_ID;
        //            objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
        //            objTransactionInfo.TRANS_DESC		=	"New watercraft operator information is added";
        //            objTransactionInfo.CHANGE_XML		=	strTranXML;
        //            objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
        //            //Executing the query
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //        }
        //        else
        //        {
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
        //        }

        //        if (returnResult > 0)
        //        {
        //            objWatercraftOperatorInfo.DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
        //        }*/
				
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
        //        return returnResult;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw(ex);
        //    }
        //    finally
        //    {
        //        if(objDataWrapper != null) objDataWrapper.Dispose();
        //    }
        //}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldWatercraftOperatorInfo">Model object having old information</param>
		/// <param name="objWatercraftOperatorInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		/*public int UpdateWatercraftOperator(ClsWatercraftOperatorInfo objOldWatercraftOperatorInfo,ClsWatercraftOperatorInfo objWatercraftOperatorInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdateWatercraftOperatorInfo";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftOperatorInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftOperatorInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID);
				
				objDataWrapper.AddParameter("@DRIVER_FNAME",objWatercraftOperatorInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objWatercraftOperatorInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objWatercraftOperatorInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objWatercraftOperatorInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objWatercraftOperatorInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objWatercraftOperatorInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objWatercraftOperatorInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objWatercraftOperatorInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objWatercraftOperatorInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objWatercraftOperatorInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objWatercraftOperatorInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@MARITAL_STATUS",objWatercraftOperatorInfo.MARITAL_STATUS);
				objDataWrapper.AddParameter("@VIOLATIONS",objWatercraftOperatorInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objWatercraftOperatorInfo.MVR_ORDERED);
				
				if(objWatercraftOperatorInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objWatercraftOperatorInfo.DATE_ORDERED);
				}
				
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objWatercraftOperatorInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objWatercraftOperatorInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objWatercraftOperatorInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objWatercraftOperatorInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objWatercraftOperatorInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objWatercraftOperatorInfo.MVR_STATUS);

				if(objWatercraftOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DRIVER_DOB",objWatercraftOperatorInfo.DRIVER_DOB);
				}
				objDataWrapper.AddParameter("@DRIVER_SSN",objWatercraftOperatorInfo.DRIVER_SSN);
				
				objDataWrapper.AddParameter("@DRIVER_SEX",objWatercraftOperatorInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objWatercraftOperatorInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objWatercraftOperatorInfo.DRIVER_LIC_STATE);
				if(objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX);
				}
				else
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
				}
				
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objWatercraftOperatorInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@VEHICLE_ID",objWatercraftOperatorInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objWatercraftOperatorInfo.PERCENT_DRIVEN);
			 			
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftOperatorInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftOperatorInfo.LAST_UPDATED_DATETIME);
				
				//added by vj on 17-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);


				if(TransactionLogRequired) 
				{
					objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldWatercraftOperatorInfo,objWatercraftOperatorInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftOperatorInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Watercraft operator information is modified";
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
		#region Update watercraft Overload :13 SEP 2007
		public int UpdateWatercraftOperator(ClsWatercraftOperatorInfo objOldWatercraftOperatorInfo,ClsWatercraftOperatorInfo objWatercraftOperatorInfo, string strCustomInfo,string strAssignXml,string strAssignRecXml)//Done for Itrack Issue 6737 on 17 Nov 09
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdateWatercraftOperatorInfo";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftOperatorInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftOperatorInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID);
				
				objDataWrapper.AddParameter("@DRIVER_FNAME",objWatercraftOperatorInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objWatercraftOperatorInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objWatercraftOperatorInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objWatercraftOperatorInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objWatercraftOperatorInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objWatercraftOperatorInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objWatercraftOperatorInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objWatercraftOperatorInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objWatercraftOperatorInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objWatercraftOperatorInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objWatercraftOperatorInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@MARITAL_STATUS",objWatercraftOperatorInfo.MARITAL_STATUS);
				objDataWrapper.AddParameter("@VIOLATIONS",objWatercraftOperatorInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objWatercraftOperatorInfo.MVR_ORDERED);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objWatercraftOperatorInfo.DRIVER_DRIV_TYPE);

				if(objWatercraftOperatorInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objWatercraftOperatorInfo.DATE_ORDERED);
				}
				
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objWatercraftOperatorInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objWatercraftOperatorInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objWatercraftOperatorInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objWatercraftOperatorInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objWatercraftOperatorInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objWatercraftOperatorInfo.MVR_STATUS);

				if(objWatercraftOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DRIVER_DOB",objWatercraftOperatorInfo.DRIVER_DOB);
				}
				objDataWrapper.AddParameter("@DRIVER_SSN",objWatercraftOperatorInfo.DRIVER_SSN);
				
				objDataWrapper.AddParameter("@DRIVER_SEX",objWatercraftOperatorInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objWatercraftOperatorInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objWatercraftOperatorInfo.DRIVER_LIC_STATE);
				objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",DefaultValues.GetIntNull(objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX));
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objWatercraftOperatorInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@VEHICLE_ID",objWatercraftOperatorInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objWatercraftOperatorInfo.PERCENT_DRIVEN);
			 			
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftOperatorInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftOperatorInfo.LAST_UPDATED_DATETIME);
				
				//added by vj on 17-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
				objDataWrapper.AddParameter("@WAT_SAFETY_COURSE",objWatercraftOperatorInfo.WAT_SAFETY_COURSE);
				objDataWrapper.AddParameter("@CERT_COAST_GUARD",objWatercraftOperatorInfo.CERT_COAST_GUARD);
				objDataWrapper.AddParameter("@REC_VEH_ID",objWatercraftOperatorInfo.REC_VEH_ID);
				objDataWrapper.AddParameter("@APP_REC_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_REC_VEHICLE_PRIN_OCC_ID);

				//Modified Trans Log 13 sep 2007

				returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				objDataWrapper.ClearParameteres();

				string sbStrXml="",sbStrRecXml="";//Done for Itrack Issue 6737 on 17 Nov 09
				string calledfrom="";

				DataTable dtVehicle =new DataTable();
				DataTable dtRecVehicle =new DataTable();//Done for Itrack Issue 6737 on 17 Nov 09
				dtVehicle=clsWatercraftInformation.GetAppBoat(objWatercraftOperatorInfo.CUSTOMER_ID,objWatercraftOperatorInfo.APP_ID,objWatercraftOperatorInfo.APP_VERSION_ID,objWatercraftOperatorInfo.DRIVER_ID);
				//Done for Itrack Issue 6737 on 17 Nov 09
				dtRecVehicle=clsWatercraftInformation.GetAppReacreationalVehicles(objWatercraftOperatorInfo.CUSTOMER_ID,objWatercraftOperatorInfo.APP_ID,objWatercraftOperatorInfo.APP_VERSION_ID,objWatercraftOperatorInfo.DRIVER_ID);
				AddAppAssignedBoats(objDataWrapper,strAssignXml,objWatercraftOperatorInfo,calledfrom,dtVehicle,ref sbStrXml);
				//Done for Itrack Issue 6737 on 17 Nov 09
				if(objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE != null && objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE != "")
				  AddAppAssignedReacreationalVehicles(objDataWrapper,strAssignRecXml,objWatercraftOperatorInfo,calledfrom,dtRecVehicle,ref sbStrRecXml);

				if(TransactionLogRequired)
				{
					objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objWatercraftOperatorInfo);
					//Done for Itrack Issue 6737 on 17 Nov 09
					if(sbStrRecXml !="")
					 strTranXML= "<root>" + strTranXML + sbStrXml + sbStrRecXml + "</root>";
					else
					 strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftOperatorInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Watercraft operator information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);

				}

				//End Trans Log 13 sep 2007

				//Commented trans Log 13 sep 2007
                //if(TransactionLogRequired) 
                //{
                //    objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
                //    strTranXML = objBuilder.GetTransactionLogXML(objOldWatercraftOperatorInfo,objWatercraftOperatorInfo);
                //    if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
                //        returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
                //    else				
                //    {	
                //        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                //        objTransactionInfo.TRANS_TYPE_ID	=	3;
                //        objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
                //        objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
                //        objTransactionInfo.APP_VERSION_ID	=	objWatercraftOperatorInfo.APP_VERSION_ID;
                //        objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
                //        objTransactionInfo.TRANS_DESC		=	"Watercraft operator information is modified";
                //        objTransactionInfo.CHANGE_XML		=	strTranXML;
                //        objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
                //        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                //    }

                //}
                //else
                //{
                //    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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
            */
		#endregion


/*
		public int UpdateWatercraftOperator(ClsWatercraftOperatorInfo objOldWatercraftOperatorInfo,ClsWatercraftOperatorInfo objWatercraftOperatorInfo, string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdateWatercraftOperatorInfo";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftOperatorInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftOperatorInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID);
				
				objDataWrapper.AddParameter("@DRIVER_FNAME",objWatercraftOperatorInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objWatercraftOperatorInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objWatercraftOperatorInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objWatercraftOperatorInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objWatercraftOperatorInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objWatercraftOperatorInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objWatercraftOperatorInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objWatercraftOperatorInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objWatercraftOperatorInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objWatercraftOperatorInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objWatercraftOperatorInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@MARITAL_STATUS",objWatercraftOperatorInfo.MARITAL_STATUS);
				objDataWrapper.AddParameter("@VIOLATIONS",objWatercraftOperatorInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objWatercraftOperatorInfo.MVR_ORDERED);
				
				if(objWatercraftOperatorInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objWatercraftOperatorInfo.DATE_ORDERED);
				}
				
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objWatercraftOperatorInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objWatercraftOperatorInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objWatercraftOperatorInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objWatercraftOperatorInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objWatercraftOperatorInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objWatercraftOperatorInfo.MVR_STATUS);

				if(objWatercraftOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DRIVER_DOB",objWatercraftOperatorInfo.DRIVER_DOB);
				}
				objDataWrapper.AddParameter("@DRIVER_SSN",objWatercraftOperatorInfo.DRIVER_SSN);
				
				objDataWrapper.AddParameter("@DRIVER_SEX",objWatercraftOperatorInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objWatercraftOperatorInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objWatercraftOperatorInfo.DRIVER_LIC_STATE);
				objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",DefaultValues.GetIntNull(objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX));
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objWatercraftOperatorInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@VEHICLE_ID",objWatercraftOperatorInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objWatercraftOperatorInfo.PERCENT_DRIVEN);
			 			
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftOperatorInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftOperatorInfo.LAST_UPDATED_DATETIME);
				
				//added by vj on 17-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
				objDataWrapper.AddParameter("@WAT_SAFETY_COURSE",objWatercraftOperatorInfo.WAT_SAFETY_COURSE);
				objDataWrapper.AddParameter("@CERT_COAST_GUARD",objWatercraftOperatorInfo.CERT_COAST_GUARD);
				objDataWrapper.AddParameter("@REC_VEH_ID",objWatercraftOperatorInfo.REC_VEH_ID);
				objDataWrapper.AddParameter("@APP_REC_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_REC_VEHICLE_PRIN_OCC_ID);

				
				if(TransactionLogRequired) 
				{
					objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldWatercraftOperatorInfo,objWatercraftOperatorInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
						objTransactionInfo.APP_ID			=	objWatercraftOperatorInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objWatercraftOperatorInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Watercraft operator information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
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
		}*/
		#endregion
		
		//#endregion
		
		/// <summary>
		/// Retrieving Exsiting driver from app_driver_details.
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataTable FetchExistingDriverFromCurrentApp(int intCustomerID,int intAppID,int intAppVersionID)
		{
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@APPID",intAppID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetExistingDriver");
			return dsTemp.Tables[0];
		}

		/// <summary>
		/// Retrieving Exsiting driver from APP_UMBRELLA_DRIVER_DETAILS.
		/// </summary>
		/// <param name="intCustomerID"></param>		
		/// <returns></returns>
		
		public static DataTable FetchExistingDriverForUmbrella(int intCustomerID,int intAppID,int intAppVersionID)
		{
	
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@APPID",intAppID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetExistingDriverForUmbrella");
			return dsTemp.Tables[0];
		}
		/// <summary>
		/// Retrieving Exsiting driver from APP_UMBRELLA_DRIVER_DETAILS.
		/// </summary>
		/// <param name="intCustomerID"></param>		
		/// <returns></returns>
		//		public static DataTable FetchExistingOperatorForUmbrella(int intCustomerID,int intAppID,int intAppVersionID)
		//		{
		//			DataSet dsTemp=new DataSet() ;
		//            DataWrapper objDataWrapper=new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
		//			objDataWrapper.AddParameter("@APPID",intAppID);
		//			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
		//			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetExistingOperatorForUmbrella");
		//			return dsTemp.Tables[0];
		//		}
		/// <summary>
		/// Retrieving Exsiting driver from APP_WATERCRAFT_DRIVER_DETAILS.
		/// </summary>
		/// <param name="intCustomerID"></param>		
		/// <returns></returns>
		public static DataTable FetchExistingDriverForWatercraft(int intCustomerID,int intAppID,int intAppVersionID)
		{
	
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@APPID",intAppID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetExistingDriverForWatercraft");
			return dsTemp.Tables[0];
		}
		/// <summary>
		/// Function for retrieving Customer name on the basis of customer id as parameter.   
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <returns></returns>

		

//		public static string GetCustomerNameXML(int intCustomerID)
//		{
//			DataSet dsTemp = new DataSet();			
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
//			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCustomerInfo");
//			return dsTemp.GetXml();
//		
//		}

		public static string GetCustomerNameXML(int intCustomerID)
		{
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCustomerInfo");

			string localxml = dsTemp.GetXml();
			//Added by Mohit Agarwal 5-Sep
			#region Make a new node
			if(localxml.IndexOf("NewDataSet") >= 0)
			{
				string lblSSN = "";
				XmlDocument objxml = new XmlDocument();
				objxml.LoadXml(dsTemp.GetXml());

				XmlNode node = objxml.SelectSingleNode("NewDataSet");
				foreach(XmlNode nodes in node.SelectNodes("Table"))
				{
					XmlNode noder1 = nodes.SelectSingleNode("SSN_NO");
					if(noder1!=null && noder1.InnerText!="")
					{

						string strSSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
						if(strSSN_NO.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
						{
							string strvaln = "xxx-xx-";
							strvaln += strSSN_NO.Substring(strvaln.Length, strSSN_NO.Length - strvaln.Length);
							lblSSN = strvaln;
						}
						else
							lblSSN = "";
					}
					else
							lblSSN = "";


					XmlNode newnode = objxml.CreateElement("DECRYPT_SSN_NO");
					newnode.InnerText = lblSSN;
					nodes.AppendChild(newnode);

				}
				return objxml.OuterXml;
			}
			#endregion

			return dsTemp.GetXml();
		
		}

		/// <summary>
		/// Function for retrieving driver count
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static int GetDriverCount(int intCustomerID,int intAppID,int intAppVersionID)
		{
			DataSet dsTemp = new DataSet();			
			int retResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@APPID",intAppID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetDriverCount");
			if(dsTemp!=null)
			{
				if(dsTemp.Tables[0].Rows.Count>0 )
					retResult=int.Parse(dsTemp.Tables[0].Rows[0]["DRIVER_ID"].ToString())  ;		
			}			
			return retResult;
		}
		/// <summary>
		/// Overloaded Function for retrieving driver count.
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static int GetDriverCount(int intCustomerID,int intAppID,int intAppVersionID,string strCalledFrom)
		{
			DataSet dsTemp = new DataSet();			
			int retResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@APPID",intAppID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);

			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetDriverCount");
			if(dsTemp!=null)
			{
				if(dsTemp.Tables[0].Rows.Count>0 )
					retResult=int.Parse(dsTemp.Tables[0].Rows[0]["DRIVER_ID"].ToString())  ;		
			}			
			return retResult;
		}

		public static int GetDriverCount(int intCustomerID,int intAppID,int intAppVersionID,string strCalledFrom,string strCalledFor)
		{
			DataSet dsTemp = new DataSet();			
			int retResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@APPID",intAppID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
			objDataWrapper.AddParameter("@CALLEDFOR",strCalledFor);

			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetDriverCount");
			if(dsTemp!=null)
			{
				if(dsTemp.Tables[0].Rows.Count>0 )
					retResult=int.Parse(dsTemp.Tables[0].Rows[0]["DRIVER_ID"].ToString())  ;		
			}			
			return retResult;
		}


		/// <summary>
		///	Coping the existing driver from the App_Driver_Details to App_Driver_Details with different Driver_Id. 
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public static void InsertExistingDriver(DataTable dtSelectedDriver,int from_Customer_ID,int from_App_ID,int from_App_Version_ID,int from_User_Id)
		{			
			string	strStoredProc =	"Proc_InsertExistingDriver";
			

			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];					
					
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",from_App_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",from_App_Version_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
			
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}	

		public void InsertExistingDriver(DataTable dtSelectedDriver,int from_Customer_ID,int from_App_ID,int from_App_Version_ID,int from_User_Id, string strCalledFrom)
		{			
			string	strStoredProc = "Proc_InsertExistingDriver";
			string DriverInfo = "";
			int returnResult;

			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];					
					//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",from_App_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",from_App_Version_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					objDataWrapper.AddParameter("@Called_From",strCalledFrom,SqlDbType.VarChar);							
					DriverInfo +=";Driver Name = " + dr["DriverName"].ToString() + ", Driver Code = " + dr["DriverCode"].ToString();

					if(TransactionLogRequired && i==(dtSelectedDriver.Rows.Count-1))
					{
						ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/CopyApplicantDriver.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID			=	from_App_ID;
						objTransactionInfo.APP_VERSION_ID	=	from_App_Version_ID;
						objTransactionInfo.CLIENT_ID		=	from_Customer_ID;
						objTransactionInfo.RECORDED_BY		=	from_User_Id;
						objTransactionInfo.TRANS_DESC		=	"Driver is copied";
						objTransactionInfo.CUSTOM_INFO		=	DriverInfo;
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}				
					objDataWrapper.ClearParameteres();					
				}
				objDataWrapper.ClearParameteres();
				//Update Driver endorsements
				//Update Endorsements//////////
				this.UpdateDriverEndorsements(from_Customer_ID,from_App_ID,from_App_Version_ID,objDataWrapper);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
				////////////////////////////
				///

			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw (exc);
			}
			finally
			{}
		}	
		


		//		public void InsertExistingUmbrellaOperator(DataTable dtSelectedDriver,int from_Customer_ID,int from_App_ID,int from_App_Version_ID,int from_User_Id)
		//		{			
		//			string	strStoredProc = "Proc_InsertExistingUmbrellaOperator";
		//			string DriverInfo="";
		//			int returnResult;
		//			try
		//			{				
		//				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
		//				{
		//					DataRow dr=dtSelectedDriver.Rows[i];					
		//					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
		//					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
		//					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
		//					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
		//					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
		//					objDataWrapper.AddParameter("@TO_APP_ID",from_App_ID,SqlDbType.Int);
		//					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",from_App_Version_ID,SqlDbType.Int);
		//					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
		//					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
		//					//objDataWrapper.ExecuteNonQuery(strStoredProc);
		//					DriverInfo +=";Operator Name = " + dr["DriverName"].ToString() + ", Operator Code = " + dr["DriverCode"].ToString();
		//
		//					if(TransactionLogRequired && i==(dtSelectedDriver.Rows.Count-1))
		//					{
		//						ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
		//						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/CopyApplicantDriver.aspx.resx");
		//						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		//						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
		//						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//						objTransactionInfo.TRANS_TYPE_ID	=	1;
		//						objTransactionInfo.APP_ID			=	from_App_ID;
		//						objTransactionInfo.APP_VERSION_ID	=	from_App_Version_ID;
		//						objTransactionInfo.CLIENT_ID		=	from_Customer_ID;
		//						objTransactionInfo.RECORDED_BY		=	from_User_Id;
		//						objTransactionInfo.TRANS_DESC		=	"Operator is copied";
		//						objTransactionInfo.CUSTOM_INFO		=	DriverInfo;
		//						//objTransactionInfo.CHANGE_XML		=	strTranXML;
		//						//Executing the query
		//						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		//					}
		//					else
		//					{
		//						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
		//					}							
		//				}							
		//			}
		//			catch(Exception exc)
		//			{
		//				throw (exc);
		//			}
		//			finally
		//			{}
		//		}	
		//		
		/// <summary>
		///	Coping the existing driver from the App_Driver_Details to App_Driver_Details with different Driver_Id. 
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public void InsertExistingUmbrellaDriver(DataTable dtSelectedDriver,int from_Customer_ID,int from_App_ID,int from_App_Version_ID,int from_User_Id)
		{			
			string	strStoredProc = "Proc_InsertExistingUmbrellaDriver";
			string DriverInfo="";
			int returnResult;
			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];					
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",from_App_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",from_App_Version_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					//objDataWrapper.ExecuteNonQuery(strStoredProc);
					DriverInfo +=";Driver Name = " + dr["DriverName"].ToString() + ", Driver Code = " + dr["DriverCode"].ToString();

					if(TransactionLogRequired && i==(dtSelectedDriver.Rows.Count-1))
					{
						ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/CopyApplicantDriver.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID			=	from_App_ID;
						objTransactionInfo.APP_VERSION_ID	=	from_App_Version_ID;
						objTransactionInfo.CLIENT_ID		=	from_Customer_ID;
						objTransactionInfo.RECORDED_BY		=	from_User_Id;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1583", "");// "Driver is copied";
						objTransactionInfo.CUSTOM_INFO		=	DriverInfo;
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}							
				}							
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}	
		/// <summary>
		///	Coping the existing driver from the App_Driver_Details to App_Driver_Details with different Driver_Id. 
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public void InsertExistingWatercraftDriver(DataTable dtSelectedDriver,int from_Customer_ID,int from_App_ID,int from_App_Version_ID,int from_User_Id)
		{			
			string	strStoredProc = "Proc_InsertExistingWatercraftDriver";
			string DriverInfo="";
			int returnResult;
			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];					
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",from_App_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",from_App_Version_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					//objDataWrapper.ExecuteNonQuery(strStoredProc);
					DriverInfo +=";Operator Name = " + dr["DriverName"].ToString() + ", Operator Code = " + dr["DriverCode"].ToString();

					if(TransactionLogRequired && i==(dtSelectedDriver.Rows.Count-1))
					{
						ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/CopyApplicantDriver.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID			=	from_App_ID;
						objTransactionInfo.APP_VERSION_ID	=	from_App_Version_ID;
						objTransactionInfo.CLIENT_ID		=	from_Customer_ID;
						objTransactionInfo.RECORDED_BY		=	from_User_Id;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1598", "");//"Operator is copied";
						objTransactionInfo.CUSTOM_INFO		=	DriverInfo;
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}					
				}							
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}
	
		public int CheckDriverExistence(ClsDriverDetailsInfo objDriverDetailsInfo,DataWrapper objDataWrapper)
		{
			string strStoredProc =	"Proc_CheckAPP_DRIVER_DETAILS_EXISTENCE";

			objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objDriverDetailsInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objDriverDetailsInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
			objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
			objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
			SqlParameter driverID = (SqlParameter)objDataWrapper.AddParameter("@DRIVER_ID",SqlDbType.Int,ParameterDirection.Output);
			
			objDataWrapper.ExecuteNonQuery(strStoredProc);
			
			return Convert.ToInt32(driverID.Value);
			
		}


		public static string  GetDriverIDs(int customerID, int appID, int appVersionID)
		{
			return GetDriverIDs(customerID, appID, appVersionID,null);
		}

		/// <summary>
		/// Gets Drivers Ids against one application
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Driver Ids as carat separated string </returns>
		public static string  GetDriverIDs(int customerID, int appID, int appVersionID,DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_GetDriverIDs";
			
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strDriverIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strDriverIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strDriverIds = strDriverIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strDriverIds;
		}
// Get Assigned driver of vehicle
		public static string  GetAssinedDriverId(int customerID, int appID, int appVersionID,int vehicleID,DataWrapper objWrapper, string strCalledFrom)
		{

			string	strStoredProc =	"";
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			
			if (strCalledFrom=="APP")
			{
				strStoredProc =	"Proc_GetAssindDriverIDsToVehicleapp";
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@APP_ID",appID);
				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				objWrapper.AddParameter("@VEHICLE_ID",vehicleID);

			}
			else
			{
				strStoredProc =	"Proc_GetAssindDriverIDsToVehiclepol";
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@POL_ID",appID);
				objWrapper.AddParameter("@POL_VERSION_ID",appVersionID);
				objWrapper.AddParameter("@VEHICLE_ID",vehicleID);

			}		
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strDriverIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strDriverIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strDriverIds = strDriverIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strDriverIds;
		}
		//ITRACK # 5081
		// Get Assigned driver of vehicle for MOTORCYCLE
		public static string  GetMotorAssinedDriverId(int customerID, int appID, int appVersionID,int vehicleID,DataWrapper objWrapper, string strCalledFrom)
		{

			string	strStoredProc =	"";
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			
			if (strCalledFrom=="APP")
			{
				strStoredProc =	"Proc_GetAssindDriverIDsToMotorcycleApp";
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@APP_ID",appID);
				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				objWrapper.AddParameter("@VEHICLE_ID",vehicleID);

			}
			else
			{
				strStoredProc =	"Proc_GetAssindDriverIDsToMotorcyclePol";
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@POL_ID",appID);
				objWrapper.AddParameter("@POL_VERSION_ID",appVersionID);
				objWrapper.AddParameter("@VEHICLE_ID",vehicleID);

			}		
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			objWrapper.ClearParameteres();
			
			int intCount=ds.Tables[0].Rows.Count;
			string strDriverIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strDriverIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strDriverIds = strDriverIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strDriverIds;
		}
		/// Praveen Singh , Dated : 9/1/2006
		/// <summary>
		/// Gets Drivers Ids against one policy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <returns>Driver Ids as carat separated string </returns>
		public static string  GetDriverIDs_forPolicy(int customerID, int policyID, int policyVersionID)
		{
			return GetDriverIDs_forPolicy(customerID, policyID, policyVersionID,null);
		}
		public static string  GetDriverIDs_forPolicy(int customerID, int policyID, int policyVersionID, DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_GetDriverIDs_Policy";
			if(objWrapper==null)	
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",policyID);
			objWrapper.AddParameter("@POL_VERSION_ID",policyVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			objWrapper.ClearParameteres();
			
			int intCount=ds.Tables[0].Rows.Count;
			string strDriverIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strDriverIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strDriverIds = strDriverIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strDriverIds;
		}
 


		/// <summary>
		/// Gets Violation Ids against one driver in one application
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Violation Ids as carat separated string </returns>
		public static string  GetWCViolationIDs(int customerID, int appID, int appVersionID, int driverID)
		{
			string	strStoredProc =	"Proc_GetWCViolationIDs";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DRIVER_ID",driverID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strViolationIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strViolationIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strViolationIds = strViolationIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strViolationIds;
		}


		/// <summary>
		/// Gets Violation Ids against one driver in one application
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Violation Ids as carat separated string </returns>
		public static string  GetWCViolationIDs_Pol(int customerID, int policyID, int policyVersionID, int driverID)
		{
			string	strStoredProc =	"Proc_GetWCViolationIDs_Pol";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@DRIVER_ID",driverID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strViolationIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strViolationIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strViolationIds = strViolationIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strViolationIds;
		}

		public string GetAppViolationPoints(int CustomerID, int AppID, int AppVersionID, int DriverID)
		{
			string violationIDs = ClsDriverDetail.GetViolationIDs(CustomerID,AppID,AppVersionID,DriverID);
			string strStoredProcForAuto_ViolationsComponent ="Proc_GetRatingInformationForAuto_DriverViolationComponent";
			int sumofViolationPoints=0, sumofAccidentPoints=0, sumofMvrPoints=0; //Set Violation Variables
			StringBuilder strViolationNodes = new StringBuilder(); //For Driver Violations .
			StringBuilder returnString = new StringBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			DataSet dsTempXML = new DataSet();
			string strMVRPoints,strReturnXML,strpolicy="";
			if(violationIDs != "-1")
			{

				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID);
				objDataWrapper.AddParameter("@APPID",AppID);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID);	
				dsTempXML =	objDataWrapper.ExecuteDataSet("Proc_GetRatingInformationForAuto_AppComponent");			
				objDataWrapper.ClearParameteres();

				strpolicy = dsTempXML.GetXml();
				strpolicy= strpolicy.Replace("<NewDataSet>","");
				strpolicy= strpolicy.Replace ("</NewDataSet>","");			
				strpolicy= strpolicy.Replace("<Table>","");
				strpolicy= strpolicy.Replace("</Table>","");

				returnString.Append("<VIOLATIONS>");
				string[] violationID = new string[0];
				violationID = violationIDs.Split('^');

				strViolationNodes.Remove(0,strViolationNodes.Length);
				
				
	 

				// Run a loop to get the inputXML for each vehicleID
				for (int iCounterForViolations=0; iCounterForViolations<violationID.Length ; iCounterForViolations++)
				{
					//For just Violation Nodes
					strViolationNodes.Append("<VIOLATION ID='"+ violationID[iCounterForViolations]+"'>"); 
					//End		
					returnString.Append("<VIOLATION ID='"+ violationID[iCounterForViolations]+"'>");
					objDataWrapper.AddParameter("@CUSTOMERID",CustomerID);
					objDataWrapper.AddParameter("@APPID",AppID);
					objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID);	
					objDataWrapper.AddParameter("@DRIVERID",DriverID);						
					objDataWrapper.AddParameter("@APP_MVR_ID",violationID[iCounterForViolations]);

					dsTempXML =	objDataWrapper.ExecuteDataSet(strStoredProcForAuto_ViolationsComponent);			
					objDataWrapper.ClearParameteres();

					strReturnXML = dsTempXML.GetXml();
					strReturnXML= strReturnXML.Replace("<NewDataSet>","");
					strReturnXML= strReturnXML.Replace ("</NewDataSet>","");			
					strReturnXML= strReturnXML.Replace("<Table>","");
					strReturnXML= strReturnXML.Replace("</Table>","");
					//Appending the Violations 
					strViolationNodes.Append(strReturnXML);
					returnString.Append(strReturnXML);
					//End 
					returnString.Append("</VIOLATION>");
					//Appending the Violations 
					strViolationNodes.Append("</VIOLATION>");
					//End 
				}
				//Set the values of the variables Calling method to Calculate points:
				string strMVRPointsForSurchargeCyclXML = "<ViolationsMVRPoints>" + strpolicy + "<violations>" + strViolationNodes.ToString() +  "</violations>" + "</ViolationsMVRPoints>";
				ClsQuickQuote ClsQQobj = new ClsQuickQuote();
				strMVRPoints = ClsQQobj.GetMVRPointsForSurcharge(strMVRPointsForSurchargeCyclXML,"AUTOP");
				returnString.Append("</VIOLATIONS>");
				//End 
			}
			else
			{
				//if strViolation is blank (No Violations Selected //
				strMVRPoints  = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>-1</SUMOFACCIDENTPOINTS></POINTS>";
			}
			
			XmlDocument PointsDoc = new XmlDocument();
			PointsDoc.LoadXml(strMVRPoints);
			XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
			if(PointNode!=null)
			{							
				sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
				sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
				sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                
				returnString.Append("<MVR>");
				returnString.Append(sumofMvrPoints);
				returnString.Append("</MVR>");

				returnString.Append("<SUMOFVIOLATIONPOINTS>");
				returnString.Append(sumofViolationPoints);
				returnString.Append("</SUMOFVIOLATIONPOINTS>");

				returnString.Append("<SUMOFACCIDENTPOINTS>");
				returnString.Append(sumofAccidentPoints);
				returnString.Append("</SUMOFACCIDENTPOINTS>");
							
			}
			return returnString.ToString();
		}



		public string GetPolViolationPoints(int CustomerID, int PolicyID, int PolicyVersionID, int DriverID)
		{
			string violationIDs = ClsDriverDetail.GetViolationIDsForPolicy(CustomerID,PolicyID,PolicyVersionID,DriverID);
			string strStoredProcForAuto_ViolationsComponent ="Proc_GetPolicyRatingInformationForAuto_DriverViolationComponent";
			int sumofViolationPoints=0, sumofAccidentPoints=0, sumofMvrPoints=0; //Set Violation Variables
			StringBuilder strViolationNodes = new StringBuilder(); //For Driver Violations .
			StringBuilder returnString = new StringBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			DataSet dsTempXML = new DataSet();
			string strMVRPoints,strReturnXML,strpolicy="";
			if(violationIDs != "-1")
			{

				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID);
				objDataWrapper.AddParameter("@POLICYID",PolicyID);
				objDataWrapper.AddParameter("@POLICYVERSIONID",PolicyVersionID);				 
				dsTempXML =	objDataWrapper.ExecuteDataSet("Proc_GetPolicyRatingInformationForAuto_AppComponent");			
				objDataWrapper.ClearParameteres();

				strpolicy = dsTempXML.GetXml();
				strpolicy= strpolicy.Replace("<NewDataSet>","");
				strpolicy= strpolicy.Replace ("</NewDataSet>","");			
				strpolicy= strpolicy.Replace("<Table>","");
				strpolicy= strpolicy.Replace("</Table>","");

				returnString.Append("<VIOLATIONS>");
				string[] violationID = new string[0];
				violationID = violationIDs.Split('^');

				strViolationNodes.Remove(0,strViolationNodes.Length);
				
				
	 

				// Run a loop to get the inputXML for each vehicleID
				for (int iCounterForViolations=0; iCounterForViolations<violationID.Length ; iCounterForViolations++)
				{
					//For just Violation Nodes
					strViolationNodes.Append("<VIOLATION ID='"+ violationID[iCounterForViolations]+"'>"); 
					//End		
					returnString.Append("<VIOLATION ID='"+ violationID[iCounterForViolations]+"'>");
					objDataWrapper.AddParameter("@CUSTOMERID",CustomerID);
					objDataWrapper.AddParameter("@POLICYID",PolicyID);
					objDataWrapper.AddParameter("@POLICYVERSIONID",PolicyVersionID);	
					objDataWrapper.AddParameter("@DRIVERID",DriverID);						
					objDataWrapper.AddParameter("@POL_MVR_ID",violationID[iCounterForViolations]);

					dsTempXML =	objDataWrapper.ExecuteDataSet(strStoredProcForAuto_ViolationsComponent);			
					objDataWrapper.ClearParameteres();

					strReturnXML = dsTempXML.GetXml();
					strReturnXML= strReturnXML.Replace("<NewDataSet>","");
					strReturnXML= strReturnXML.Replace ("</NewDataSet>","");			
					strReturnXML= strReturnXML.Replace("<Table>","");
					strReturnXML= strReturnXML.Replace("</Table>","");
					//Appending the Violations 
					strViolationNodes.Append(strReturnXML);
					returnString.Append(strReturnXML);
					//End 
					returnString.Append("</VIOLATION>");
					//Appending the Violations 
					strViolationNodes.Append("</VIOLATION>");
					//End 
				}
				//Set the values of the variables Calling method to Calculate points:
				string strMVRPointsForSurchargeCyclXML = "<ViolationsMVRPoints>" + strpolicy + "<violations>" + strViolationNodes.ToString() +  "</violations>" + "</ViolationsMVRPoints>";
				ClsQuickQuote ClsQQobj = new ClsQuickQuote();
				strMVRPoints = ClsQQobj.GetMVRPointsForSurcharge(strMVRPointsForSurchargeCyclXML,"AUTOP");
				returnString.Append("</VIOLATIONS>");
				//End 
			}
			else
			{
				//if strViolation is blank (No Violations Selected //
				strMVRPoints  = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>-1</SUMOFACCIDENTPOINTS></POINTS>";
			}
			
			XmlDocument PointsDoc = new XmlDocument();
			PointsDoc.LoadXml(strMVRPoints);
			XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
			if(PointNode!=null)
			{							
				sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
				sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
				sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                
				returnString.Append("<MVR>");
				returnString.Append(sumofMvrPoints);
				returnString.Append("</MVR>");

				returnString.Append("<SUMOFVIOLATIONPOINTS>");
				returnString.Append(sumofViolationPoints);
				returnString.Append("</SUMOFVIOLATIONPOINTS>");

				returnString.Append("<SUMOFACCIDENTPOINTS>");
				returnString.Append(sumofAccidentPoints);
				returnString.Append("</SUMOFACCIDENTPOINTS>");
							
			}
			return returnString.ToString();
		}


		public static string  GetViolationIDs(int customerID, int appID, int appVersionID, int driverID)
		{
			return GetViolationIDs(customerID, appID, appVersionID, driverID,null);
		}


		/// <summary>
		/// Gets Violation Ids against one driver in one application
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Violation Ids as carat separated string </returns>
		public static string  GetViolationIDs(int customerID, int appID, int appVersionID, int driverID,DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_GetViolationIDs";
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DRIVER_ID",driverID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strViolationIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strViolationIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strViolationIds = strViolationIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strViolationIds;
		}


		/// Written by praveen Singh , Dated : 9th - Jan - 2006
		/// <summary>
		/// Gets Violation Ids against one driver in one policy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <returns>Violation Ids as carat separated string </returns>
		public static string  GetViolationIDsForPolicy(int customerID, int policyID, int policyVersionID, int driverID)
		{
			return GetViolationIDsForPolicy(customerID, policyID, policyVersionID, driverID,null);
		}
		public static string  GetViolationIDsForPolicy(int customerID, int policyID, int policyVersionID, int driverID, DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_GetViolationIDs_Policy";
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",policyID);
			objWrapper.AddParameter("@POL_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@DRIVER_ID",driverID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strViolationIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strViolationIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strViolationIds = strViolationIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strViolationIds;
		}
		/// <summary>
		/// Gets Drivers Ids against one application for rule implementation
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Driver Ids as carat separated string </returns>
		public static string  GetRuleDriverIDs(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetDrivers";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMERID",customerID);
			objWrapper.AddParameter("@APPID",appID);
			objWrapper.AddParameter("@APPVERSIONID",appVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strDriverIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strDriverIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strDriverIds = strDriverIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}				
			}
			return strDriverIds;
		}

		/// <summary>
		/// Gets Boat Ids against one application for rule implementation(Boat Operators)
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Boat Ids as carat separated string </returns>
		public static string  GetRuleWCDriverIDs(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetOperators";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMERID",customerID);
			objWrapper.AddParameter("@APPID",appID);
			objWrapper.AddParameter("@APPVERSIONID",appVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strDriverIds="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strDriverIds=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strDriverIds = strDriverIds + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}				
			}
			return strDriverIds;
		}


		public static DataTable FetchApplicantsForCustomer(int intCustomerID)
		{
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			//objDataWrapper.AddParameter("@APPID",intAppID);
			//objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetExistingApplicantsForCustomer");
			return dsTemp.Tables[0];
		}

		public  void InsertApplicantsToDriver(DataTable dtSelectedDriver,int Customer_ID,int App_ID,int App_Version_ID,int User_Id, string strCalledFrom, string strCalledFor)
		{			
			string	strStoredProc = "Proc_InsertApplicantToDriver";			
			string DriverInfo="", DriverCode="", TransDesc="";
			//int returnResult =0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];										
					objDataWrapper.AddParameter("@CUSTOMER_ID",Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@APP_ID",App_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@APP_VERSION_ID",App_Version_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@APPLICANT_ID",int.Parse(dr["ApplicantID"].ToString()),SqlDbType.Int);					
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",User_Id,SqlDbType.Int);		
					objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom,SqlDbType.VarChar);		
					objDataWrapper.AddParameter("@CALLED_FOR",strCalledFor,SqlDbType.VarChar);							
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_CODE",strCalledFor,SqlDbType.VarChar,ParameterDirection.Output,30);									
					
					
					objDataWrapper.ExecuteNonQuery(strStoredProc);
					objDataWrapper.ClearParameteres();
					DriverCode = objSqlParameter.Value.ToString();
					if(strCalledFrom.ToUpper()=="PPA" || strCalledFrom.ToUpper()=="MOT" || (strCalledFrom.ToUpper()=="UMB" && strCalledFor.ToUpper()!="WAT"))
					{
						DriverInfo +=";Driver Name = " + dr["ApplicantName"].ToString() + ", Driver Code = " + DriverCode;
						TransDesc="Driver is copied";
					}
					else if(strCalledFrom.ToUpper()=="WAT" || strCalledFrom.ToUpper()=="HOM"|| strCalledFrom.ToUpper()=="HOME" || (strCalledFrom.ToUpper()=="UMB" && strCalledFor.ToUpper()=="WAT"))
					{
						DriverInfo +=";Operator Name = " + dr["ApplicantName"].ToString() + ", Operator Code = " + DriverCode;
						TransDesc="Operator is copied";
					}
					//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);									
				}	
				if(TransactionLogRequired)// && i==(dtSelectedDriver.Rows.Count-1))
				{
					ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/CopyApplicantDriver.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	App_ID;
					objTransactionInfo.APP_VERSION_ID	=	App_Version_ID;
					objTransactionInfo.CLIENT_ID		=	Customer_ID;
					objTransactionInfo.RECORDED_BY		=	User_Id;
					objTransactionInfo.TRANS_DESC		=	TransDesc;
					objTransactionInfo.CUSTOM_INFO		=	DriverInfo;
					//objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
					
					
			}
			catch(Exception exc)
			{				
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (exc);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}	

		public int GetMVRPoints(string strCUSTOMERID,string strAppId, string strAppVersionId,string strDRIVER_ID)			
		{
			string		strStoredProc	=	"Proc_GetMVRPoints";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",strCUSTOMERID);
				objDataWrapper.AddParameter("@APP_ID",strAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",strDRIVER_ID);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@MVR_POINTS",null,SqlDbType.Int,ParameterDirection.Output);
				
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				int MvrPoints = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return MvrPoints;
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


		public int GetMVRPointsForPolicy(string strCUSTOMERID,string strPolicyId, string strPolicyVersionId,string strDRIVER_ID)			
		{
			string		strStoredProc	=	"Proc_GetMVRPointsForPolicy";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",strCUSTOMERID);
				objDataWrapper.AddParameter("@POLICY_ID",strPolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",strPolicyVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",strDRIVER_ID);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@MVR_POINTS",null,SqlDbType.Int,ParameterDirection.Output);
				
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				int MvrPoints = int.Parse(objSqlParameter.Value.ToString());

				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return MvrPoints;
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


		public int FetchPointsAssignedInfo(int customerID, int appID, int appVersionID, int driverID)
		{
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CustID",customerID);
			objDataWrapper.AddParameter("@AppID",appID);
			objDataWrapper.AddParameter("@AppVerID",appVersionID);
			objDataWrapper.AddParameter("@DriverID",driverID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppPointsAssigned");
			return Convert.ToInt32(dsTemp.Tables[0].Rows[0][0]);
		}

		public int FetchPolPointsAssignedInfo(int customerID, int polID, int polVerID, int driverID)
		{
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CustID",customerID);
			objDataWrapper.AddParameter("@PolID",polID);
			objDataWrapper.AddParameter("@PolVerID",polVerID);
			objDataWrapper.AddParameter("@DriverID",driverID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolPointsAssigned");
			return Convert.ToInt32(dsTemp.Tables[0].Rows[0][0]);
		}

		#region UpdateDriver'sVehicleClass
		/// <summary>
		/// Updated the vehicle class of any driver
		/// </summary>
		/// <param name="AppID">Application identification no</param>
		/// <param name="AppVerId">Application version identification no</param>
		/// <param name="CustomerId">Customeridentification no</param>
		/// <param name="DriverId">Driver identification no</param>
		/// <returns>viod</returns>
		private void UpdateVehicleClass(DataWrapper objDataWrapper, int AppId, int AppVerId, int CustomerId, int DriverID, int VehicleId)
		{
			try
			{
				//Getting the vechicle class
				string strClass = GetDriverVehicleClass(AppId, AppVerId, CustomerId, DriverID);
				
				if (strClass.Trim() != "")
				{
					//Updating the vehicle class
					ClsVehicleInformation objVehicle = new ClsVehicleInformation();
					objVehicle.UpdateVehicleClass(objDataWrapper, AppId, AppVerId, CustomerId, VehicleId, strClass); 
				}
				
			}
			catch 
			{
				throw (new Exception("Error occured while updating vehicle class for this driver. Please try again later."));
			}
		}
		#endregion
		 
		#region GetDriverVehicleClass
		/// <summary>
		/// Returns the vehicle class for the specified driver of any application
		/// </summary>
		/// <param name="AppID">Application identification no</param>
		/// <param name="AppVerId">Application version identification no</param>
		/// <param name="CustomerId">Customeridentification no</param>
		/// <param name="DriverId">Driver identification no</param>
		/// <returns>Vehiclass class of driver</returns>
		private string GetDriverVehicleClass(int AppId, int AppVerId, int CustomerId, int DriverID)
		{
			ClsMvrInformation objMvrInformation = new ClsMvrInformation();
			int intViolationPoint = objMvrInformation.GetViolationPoints(AppId, AppVerId, CustomerId, DriverID);
			
			//Getting the details of application driver details in the form of  xml
			string strXML = GetAppDriverDetailsXML( AppId, AppVerId, CustomerId, DriverID);
			
			//Constant for driver type
			string PRINCIPLE_DRIVER		= "3475";
			string OWNER				= "3475";
			string OCCASIONAL_DRIVER	= "3475";

			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml(strXML);

			string strDriverType	= ClsCommon.GetNodeValue(doc, "//DRIVER_DRIV_TYPE");
			DateTime dtpDOB			= ConvertToDate( ClsCommon.GetNodeValue(doc, "//DRIVER_DOB"));
			string strDriverSex		= ClsCommon.GetNodeValue(doc, "//DRIVER_SEX");
			int intAge				= ((TimeSpan) (DateTime.Now - dtpDOB)).Days/365;

			string strVehClassType = "";

			//Find the class type	
		
			//Check for CLASS P
			if ((strDriverType.ToUpper() == PRINCIPLE_DRIVER.ToUpper() || strDriverType.ToUpper() == OWNER.ToUpper()) && intViolationPoint <= 4)
			{
				if (intAge >= 25 && intAge <= 29)
				{
					strVehClassType = "PA";
				
				}
				else if (intAge >= 30 && intAge <= 34)
				{
					strVehClassType = "PB";
				}
				else if (intAge >= 35 && intAge <= 44)
				{
					strVehClassType = "PC";
				}
				else if (intAge >= 45 && intAge <= 49)
				{
					strVehClassType = "PD";
				}
				else if (intAge >= 50 && intAge <= 69)
				{
					strVehClassType = "PE";
				}
				else if (intAge >= 70 )
				{
					strVehClassType = "PF";
				}
			}

			if (strVehClassType.Trim() != "")
				return strVehClassType;


			//CHECK FOR CLASS 2
			if ((strDriverType.ToUpper() == OCCASIONAL_DRIVER.ToUpper()))
			{
				if (intAge >= 21 && intAge <= 24)
				{
					strVehClassType = "2A";
				
				}
				else if (intAge >= 18 && intAge <= 20)
				{
					strVehClassType = "2B";
				}
				else if (intAge >= 16 && intAge <= 17)
				{
					strVehClassType = "2C";
				}
			}

			if (strVehClassType.Trim() != "")
				return strVehClassType;

			//CHECK FOR CLASS 3
			if ((strDriverType.ToUpper() == OWNER.ToUpper() || strDriverType.ToUpper() == PRINCIPLE_DRIVER.ToUpper()) && strDriverSex.ToUpper() == "M")
			{
				if (intAge >= 21 && intAge <= 24)
				{
					strVehClassType = "3A";
				
				}
				else if (intAge >= 18 && intAge <= 20)
				{
					strVehClassType = "3B";
				}
				else if (intAge >= 16 && intAge <= 17)
				{
					strVehClassType = "3C";
				}
			}

			if (strVehClassType.Trim() != "")
				return strVehClassType;

			//CHECK FOR CLASS 4
			if ((strDriverType.ToUpper() == OWNER.ToUpper() || strDriverType.ToUpper() == PRINCIPLE_DRIVER.ToUpper()) && strDriverSex.ToUpper() == "F")
			{
				if (intAge >= 21 && intAge <= 24)
				{
					strVehClassType = "4A";
				
				}
				else if (intAge >= 18 && intAge <= 20)
				{
					strVehClassType = "4B";
				}
				else if (intAge >= 16 && intAge <= 17)
				{
					strVehClassType = "4C";
				}
			}

			if (strVehClassType.Trim() != "")
				return strVehClassType;



			//CHECK FOR CLASS 5
			//Check to be implemented for college student
			return strVehClassType;

		}
		#endregion

		//		#region Add(Insert) functions
		//		/// <summary>
		//		/// Saves the information passed in model object to database.
		//		/// </summary>
		//		/// <param name="objWatercraftOperatorInfo">Model class object.</param>
		//		/// <returns>No of records effected.</returns>
		//		public int AddUmbrellaOperatorDetails(ClsUmbrellaOperatorInfo objUmbrellaOperatorInfo)
		//		{
		//			string		strStoredProc	=	"Proc_InsertUmbrellaOperatorInfo";
		//			DateTime	RecordDate		=	DateTime.Now;
		//				 
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//
		//			try
		//			{
		//				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaOperatorInfo.CUSTOMER_ID);
		//				objDataWrapper.AddParameter("@APP_ID",objUmbrellaOperatorInfo.APP_ID);
		//				objDataWrapper.AddParameter("@APP_VERSION_ID",objUmbrellaOperatorInfo.APP_VERSION_ID);
		//				 
		//				
		//				objDataWrapper.AddParameter("@DRIVER_FNAME",objUmbrellaOperatorInfo.DRIVER_FNAME);
		//				objDataWrapper.AddParameter("@DRIVER_MNAME",objUmbrellaOperatorInfo.DRIVER_MNAME);
		//				objDataWrapper.AddParameter("@DRIVER_LNAME",objUmbrellaOperatorInfo.DRIVER_LNAME);
		//				objDataWrapper.AddParameter("@DRIVER_CODE",objUmbrellaOperatorInfo.DRIVER_CODE);
		//				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objUmbrellaOperatorInfo.DRIVER_SUFFIX);
		//				objDataWrapper.AddParameter("@DRIVER_ADD1",objUmbrellaOperatorInfo.DRIVER_ADD1);
		//				objDataWrapper.AddParameter("@DRIVER_ADD2",objUmbrellaOperatorInfo.DRIVER_ADD2);
		//				objDataWrapper.AddParameter("@DRIVER_CITY",objUmbrellaOperatorInfo.DRIVER_CITY);
		//				objDataWrapper.AddParameter("@DRIVER_STATE",objUmbrellaOperatorInfo.DRIVER_STATE);
		//				objDataWrapper.AddParameter("@DRIVER_ZIP",objUmbrellaOperatorInfo.DRIVER_ZIP);
		//				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objUmbrellaOperatorInfo.DRIVER_COUNTRY);
		//				
		//				if(objUmbrellaOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_DOB",objUmbrellaOperatorInfo.DRIVER_DOB);
		//				}
		//				
		//				
		//				objDataWrapper.AddParameter("@DRIVER_SSN",objUmbrellaOperatorInfo.DRIVER_SSN);
		//				
		//				objDataWrapper.AddParameter("@DRIVER_SEX",objUmbrellaOperatorInfo.DRIVER_SEX);
		//				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objUmbrellaOperatorInfo.DRIVER_DRIV_LIC);
		//				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objUmbrellaOperatorInfo.DRIVER_LIC_STATE);
		//				
		//				if(objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX);
		//				}
		//				else
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
		//				}
		//				
		//				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objUmbrellaOperatorInfo.EXPERIENCE_CREDIT );
		//				objDataWrapper.AddParameter("@VEHICLE_ID",objUmbrellaOperatorInfo.VEHICLE_ID);
		//				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objUmbrellaOperatorInfo.PERCENT_DRIVEN);
		//				
		//				objDataWrapper.AddParameter("@CREATED_BY",objUmbrellaOperatorInfo.CREATED_BY);
		//				objDataWrapper.AddParameter("@CREATED_DATETIME",objUmbrellaOperatorInfo.CREATED_DATETIME);
		//			
		//				//added by vj on 17-10-2005
		//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objUmbrellaOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
		//				 
		//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objUmbrellaOperatorInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);
		//
		//				int returnResult = 0;
		//				if(TransactionLogRequired)
		//				{
		//					objUmbrellaOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
		//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		//					string strTranXML = objBuilder.GetTransactionLogXML(objUmbrellaOperatorInfo);
		//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//					objTransactionInfo.TRANS_TYPE_ID	=	1;
		//					objTransactionInfo.RECORDED_BY		=	objUmbrellaOperatorInfo.CREATED_BY;
		//					objTransactionInfo.APP_ID			=	objUmbrellaOperatorInfo.APP_ID;
		//					objTransactionInfo.APP_VERSION_ID	=	objUmbrellaOperatorInfo.APP_VERSION_ID;
		//					objTransactionInfo.CLIENT_ID		=	objUmbrellaOperatorInfo.CUSTOMER_ID;
		//					objTransactionInfo.TRANS_DESC		=	"New watercraft operator information is added";
		//					objTransactionInfo.CHANGE_XML		=	strTranXML;
		//					//Executing the query
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		//				}
		//				else
		//				{
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
		//				}
		//
		//				if (returnResult > 0)
		//				{
		//					objUmbrellaOperatorInfo.DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
		//				}
		//				
		//				objDataWrapper.ClearParameteres();
		//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		//				
		//				return returnResult;
		//			}
		//			catch(Exception ex)
		//			{
		//				throw(ex);
		//			}
		//			finally
		//			{
		//				if(objDataWrapper != null) objDataWrapper.Dispose();
		//			}
		//		}
		//
		//		public int AddUmbrellaOperatorDetails(ClsUmbrellaOperatorInfo objUmbrellaOperatorInfo, string strCustomInfo)
		//		{
		//			string		strStoredProc	=	"Proc_InsertUmbrellaOperatorInfo";
		//			DateTime	RecordDate		=	DateTime.Now;
		//				 
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//
		//			try
		//			{
		//				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaOperatorInfo.CUSTOMER_ID);
		//				objDataWrapper.AddParameter("@APP_ID",objUmbrellaOperatorInfo.APP_ID);
		//				objDataWrapper.AddParameter("@APP_VERSION_ID",objUmbrellaOperatorInfo.APP_VERSION_ID);
		//				 
		//				
		//				objDataWrapper.AddParameter("@DRIVER_FNAME",objUmbrellaOperatorInfo.DRIVER_FNAME);
		//				objDataWrapper.AddParameter("@DRIVER_MNAME",objUmbrellaOperatorInfo.DRIVER_MNAME);
		//				objDataWrapper.AddParameter("@DRIVER_LNAME",objUmbrellaOperatorInfo.DRIVER_LNAME);
		//				objDataWrapper.AddParameter("@DRIVER_CODE",objUmbrellaOperatorInfo.DRIVER_CODE);
		//				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objUmbrellaOperatorInfo.DRIVER_SUFFIX);
		//				objDataWrapper.AddParameter("@DRIVER_ADD1",objUmbrellaOperatorInfo.DRIVER_ADD1);
		//				objDataWrapper.AddParameter("@DRIVER_ADD2",objUmbrellaOperatorInfo.DRIVER_ADD2);
		//				objDataWrapper.AddParameter("@DRIVER_CITY",objUmbrellaOperatorInfo.DRIVER_CITY);
		//				objDataWrapper.AddParameter("@DRIVER_STATE",objUmbrellaOperatorInfo.DRIVER_STATE);
		//				objDataWrapper.AddParameter("@DRIVER_ZIP",objUmbrellaOperatorInfo.DRIVER_ZIP);
		//				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objUmbrellaOperatorInfo.DRIVER_COUNTRY);
		//				
		//				if(objUmbrellaOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_DOB",objUmbrellaOperatorInfo.DRIVER_DOB);
		//				}
		//				
		//				
		//				objDataWrapper.AddParameter("@DRIVER_SSN",objUmbrellaOperatorInfo.DRIVER_SSN);
		//				
		//				objDataWrapper.AddParameter("@DRIVER_SEX",objUmbrellaOperatorInfo.DRIVER_SEX);
		//				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objUmbrellaOperatorInfo.DRIVER_DRIV_LIC);
		//				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objUmbrellaOperatorInfo.DRIVER_LIC_STATE);
		//				
		//				if(objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX);
		//				}
		//				else
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
		//				}
		//				
		//				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objUmbrellaOperatorInfo.EXPERIENCE_CREDIT );
		//				objDataWrapper.AddParameter("@VEHICLE_ID",objUmbrellaOperatorInfo.VEHICLE_ID);
		//				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objUmbrellaOperatorInfo.PERCENT_DRIVEN);
		//				
		//				objDataWrapper.AddParameter("@CREATED_BY",objUmbrellaOperatorInfo.CREATED_BY);
		//				objDataWrapper.AddParameter("@CREATED_DATETIME",objUmbrellaOperatorInfo.CREATED_DATETIME);
		//			
		//				//added by vj on 17-10-2005
		//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objUmbrellaOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
		//				 
		//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objUmbrellaOperatorInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);
		//
		//				int returnResult = 0;
		//				if(TransactionLogRequired)
		//				{
		//					objUmbrellaOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
		//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		//					string strTranXML = objBuilder.GetTransactionLogXML(objUmbrellaOperatorInfo);
		//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//					objTransactionInfo.TRANS_TYPE_ID	=	1;
		//					objTransactionInfo.RECORDED_BY		=	objUmbrellaOperatorInfo.CREATED_BY;
		//					objTransactionInfo.APP_ID			=	objUmbrellaOperatorInfo.APP_ID;
		//					objTransactionInfo.APP_VERSION_ID	=	objUmbrellaOperatorInfo.APP_VERSION_ID;
		//					objTransactionInfo.CLIENT_ID		=	objUmbrellaOperatorInfo.CUSTOMER_ID;
		//					objTransactionInfo.TRANS_DESC		=	"New watercraft operator information is added";
		//					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
		//					objTransactionInfo.CHANGE_XML		=	strTranXML;
		//					//Executing the query
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		//				}
		//				else
		//				{
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
		//				}
		//
		//				if (returnResult > 0)
		//				{
		//					objUmbrellaOperatorInfo.DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
		//				}
		//				
		//				objDataWrapper.ClearParameteres();
		//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		//				
		//				return returnResult;
		//			}
		//			catch(Exception ex)
		//			{
		//				throw(ex);
		//			}
		//			finally
		//			{
		//				if(objDataWrapper != null) objDataWrapper.Dispose();
		//			}
		//		}
		//		#endregion

		//		#region Update method for Umbrella
		//		/// <summary>
		//		/// Update method that recieves Model object to save.
		//		/// </summary>
		//		/// <param name="objOldWatercraftOperatorInfo">Model object having old information</param>
		//		/// <param name="objWatercraftOperatorInfo">Model object having new information(form control's value)</param>
		//		/// <returns>No. of rows updated (1 or 0)</returns>
		//		public int UpdateUmbrellaOperatorDetails(ClsUmbrellaOperatorInfo objOldUmbrellaOperatorInfo,ClsUmbrellaOperatorInfo objUmbrellaOperatorInfo)
		//		{
		//			string strTranXML;
		//			int returnResult = 0;
		//			string strStoredProc="Proc_UpdateUmbrellaOperatorInfo";
		//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//			try 
		//			{
		//				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaOperatorInfo.CUSTOMER_ID);
		//				objDataWrapper.AddParameter("@APP_ID",objUmbrellaOperatorInfo.APP_ID);
		//				objDataWrapper.AddParameter("@APP_VERSION_ID",objUmbrellaOperatorInfo.APP_VERSION_ID);
		//				objDataWrapper.AddParameter("@DRIVER_ID",objUmbrellaOperatorInfo.DRIVER_ID);
		//				
		//				objDataWrapper.AddParameter("@DRIVER_FNAME",objUmbrellaOperatorInfo.DRIVER_FNAME);
		//				objDataWrapper.AddParameter("@DRIVER_MNAME",objUmbrellaOperatorInfo.DRIVER_MNAME);
		//				objDataWrapper.AddParameter("@DRIVER_LNAME",objUmbrellaOperatorInfo.DRIVER_LNAME);
		//				objDataWrapper.AddParameter("@DRIVER_CODE",objUmbrellaOperatorInfo.DRIVER_CODE);
		//				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objUmbrellaOperatorInfo.DRIVER_SUFFIX);
		//				objDataWrapper.AddParameter("@DRIVER_ADD1",objUmbrellaOperatorInfo.DRIVER_ADD1);
		//				objDataWrapper.AddParameter("@DRIVER_ADD2",objUmbrellaOperatorInfo.DRIVER_ADD2);
		//				objDataWrapper.AddParameter("@DRIVER_CITY",objUmbrellaOperatorInfo.DRIVER_CITY);
		//				objDataWrapper.AddParameter("@DRIVER_STATE",objUmbrellaOperatorInfo.DRIVER_STATE);
		//				objDataWrapper.AddParameter("@DRIVER_ZIP",objUmbrellaOperatorInfo.DRIVER_ZIP);
		//				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objUmbrellaOperatorInfo.DRIVER_COUNTRY);
		//				
		//				if(objUmbrellaOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_DOB",objUmbrellaOperatorInfo.DRIVER_DOB);
		//				}
		//				objDataWrapper.AddParameter("@DRIVER_SSN",objUmbrellaOperatorInfo.DRIVER_SSN);
		//				
		//				objDataWrapper.AddParameter("@DRIVER_SEX",objUmbrellaOperatorInfo.DRIVER_SEX);
		//				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objUmbrellaOperatorInfo.DRIVER_DRIV_LIC);
		//				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objUmbrellaOperatorInfo.DRIVER_LIC_STATE);
		//				if(objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX);
		//				}
		//				else
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
		//				}
		//				
		//				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objUmbrellaOperatorInfo.EXPERIENCE_CREDIT);
		//				objDataWrapper.AddParameter("@VEHICLE_ID",objUmbrellaOperatorInfo.VEHICLE_ID);
		//				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objUmbrellaOperatorInfo.PERCENT_DRIVEN);
		//			 			
		//				objDataWrapper.AddParameter("@MODIFIED_BY",objUmbrellaOperatorInfo.MODIFIED_BY);
		//				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objUmbrellaOperatorInfo.LAST_UPDATED_DATETIME);
		//				
		//				//added by vj on 17-10-2005
		//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objUmbrellaOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
		//
		//
		//				if(TransactionLogRequired) 
		//				{
		//					objUmbrellaOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
		//					strTranXML = objBuilder.GetTransactionLogXML(objOldUmbrellaOperatorInfo,objUmbrellaOperatorInfo);
		//
		//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//					objTransactionInfo.TRANS_TYPE_ID	=	3;
		//					objTransactionInfo.RECORDED_BY		=	objUmbrellaOperatorInfo.MODIFIED_BY;
		//					objTransactionInfo.APP_ID			=	objUmbrellaOperatorInfo.APP_ID;
		//					objTransactionInfo.APP_VERSION_ID	=	objUmbrellaOperatorInfo.APP_VERSION_ID;
		//					objTransactionInfo.CLIENT_ID		=	objUmbrellaOperatorInfo.CUSTOMER_ID;
		//					objTransactionInfo.TRANS_DESC		=	"Watercraft operator information is modified";
		//					objTransactionInfo.CHANGE_XML		=	strTranXML;
		//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		//
		//				}
		//				else
		//				{
		//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
		//				}
		//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		//				return returnResult;
		//			}
		//			catch(Exception ex)
		//			{
		//				throw(ex);
		//			}
		//			finally
		//			{
		//				if(objDataWrapper != null) 
		//				{
		//					objDataWrapper.Dispose();
		//				}
		//				if(objBuilder != null) 
		//				{
		//					objBuilder = null;
		//				}
		//			}
		//		}
		//
		//		public int UpdateUmbrellaOperatorDetails(ClsUmbrellaOperatorInfo objOldUmbrellaOperatorInfo,ClsUmbrellaOperatorInfo objUmbrellaOperatorInfo, string strCustomInfo)
		//		{
		//			string strTranXML;
		//			int returnResult = 0;
		//			string strStoredProc="Proc_UpdateUmbrellaOperatorInfo";
		//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//			try 
		//			{
		//				objDataWrapper.AddParameter("@CUSTOMER_ID",objUmbrellaOperatorInfo.CUSTOMER_ID);
		//				objDataWrapper.AddParameter("@APP_ID",objUmbrellaOperatorInfo.APP_ID);
		//				objDataWrapper.AddParameter("@APP_VERSION_ID",objUmbrellaOperatorInfo.APP_VERSION_ID);
		//				objDataWrapper.AddParameter("@DRIVER_ID",objUmbrellaOperatorInfo.DRIVER_ID);
		//				
		//				objDataWrapper.AddParameter("@DRIVER_FNAME",objUmbrellaOperatorInfo.DRIVER_FNAME);
		//				objDataWrapper.AddParameter("@DRIVER_MNAME",objUmbrellaOperatorInfo.DRIVER_MNAME);
		//				objDataWrapper.AddParameter("@DRIVER_LNAME",objUmbrellaOperatorInfo.DRIVER_LNAME);
		//				objDataWrapper.AddParameter("@DRIVER_CODE",objUmbrellaOperatorInfo.DRIVER_CODE);
		//				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objUmbrellaOperatorInfo.DRIVER_SUFFIX);
		//				objDataWrapper.AddParameter("@DRIVER_ADD1",objUmbrellaOperatorInfo.DRIVER_ADD1);
		//				objDataWrapper.AddParameter("@DRIVER_ADD2",objUmbrellaOperatorInfo.DRIVER_ADD2);
		//				objDataWrapper.AddParameter("@DRIVER_CITY",objUmbrellaOperatorInfo.DRIVER_CITY);
		//				objDataWrapper.AddParameter("@DRIVER_STATE",objUmbrellaOperatorInfo.DRIVER_STATE);
		//				objDataWrapper.AddParameter("@DRIVER_ZIP",objUmbrellaOperatorInfo.DRIVER_ZIP);
		//				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objUmbrellaOperatorInfo.DRIVER_COUNTRY);
		//				
		//				if(objUmbrellaOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_DOB",objUmbrellaOperatorInfo.DRIVER_DOB);
		//				}
		//				objDataWrapper.AddParameter("@DRIVER_SSN",objUmbrellaOperatorInfo.DRIVER_SSN);
		//				
		//				objDataWrapper.AddParameter("@DRIVER_SEX",objUmbrellaOperatorInfo.DRIVER_SEX);
		//				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objUmbrellaOperatorInfo.DRIVER_DRIV_LIC);
		//				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objUmbrellaOperatorInfo.DRIVER_LIC_STATE);
		//				if(objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX);
		//				}
		//				else
		//				{
		//					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
		//				}
		//				
		//				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objUmbrellaOperatorInfo.EXPERIENCE_CREDIT);
		//				objDataWrapper.AddParameter("@VEHICLE_ID",objUmbrellaOperatorInfo.VEHICLE_ID);
		//				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objUmbrellaOperatorInfo.PERCENT_DRIVEN);
		//			 			
		//				objDataWrapper.AddParameter("@MODIFIED_BY",objUmbrellaOperatorInfo.MODIFIED_BY);
		//				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objUmbrellaOperatorInfo.LAST_UPDATED_DATETIME);
		//				
		//				//added by vj on 17-10-2005
		//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objUmbrellaOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
		//
		//
		//				if(TransactionLogRequired) 
		//				{
		//					objUmbrellaOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftDriverDetails.aspx.resx");
		//					strTranXML = objBuilder.GetTransactionLogXML(objOldUmbrellaOperatorInfo,objUmbrellaOperatorInfo);
		//
		//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//					objTransactionInfo.TRANS_TYPE_ID	=	3;
		//					objTransactionInfo.RECORDED_BY		=	objUmbrellaOperatorInfo.MODIFIED_BY;
		//					objTransactionInfo.APP_ID			=	objUmbrellaOperatorInfo.APP_ID;
		//					objTransactionInfo.APP_VERSION_ID	=	objUmbrellaOperatorInfo.APP_VERSION_ID;
		//					objTransactionInfo.CLIENT_ID		=	objUmbrellaOperatorInfo.CUSTOMER_ID;
		//					objTransactionInfo.TRANS_DESC		=	"Watercraft operator information is modified";
		//					objTransactionInfo.CHANGE_XML		=	strTranXML;
		//					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
		//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		//
		//				}
		//				else
		//				{
		//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
		//				}
		//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		//				return returnResult;
		//			}
		//			catch(Exception ex)
		//			{
		//				throw(ex);
		//			}
		//			finally
		//			{
		//				if(objDataWrapper != null) 
		//				{
		//					objDataWrapper.Dispose();
		//				}
		//				if(objBuilder != null) 
		//				{
		//					objBuilder = null;
		//				}
		//			}
		//		}
		//		#endregion

		//		#region GetXML Mehtod for Umbrella
		//		public static string FetchUmbrellaDriverInfoXML(int CustomerID,int AppID, int AppVersionID, int DriverID)
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
		//				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbrellaDriverInfo");
		//			
		//				return dsTemp.GetXml();
		//				 
		//			}
		//			catch(Exception exc)
		//			{throw (exc);}
		//			finally
		//			{}
		//		}
		//		#endregion


		#region Policy Functions
		
		/// <summary>
		/// Function for retrieving driver count
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static int GetPolicyDriverCount(int intCustomerID,int intPolicyID,int intPolicyVersionID)
		{
			DataSet dsTemp = new DataSet();			
			int retResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@POLICYID",intPolicyID);
			objDataWrapper.AddParameter("@POLICYVERSIONID",intPolicyVersionID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyDriverCount");
			if(dsTemp!=null)
			{
				if(dsTemp.Tables[0].Rows.Count>0 )
					retResult=int.Parse(dsTemp.Tables[0].Rows[0]["DRIVER_ID"].ToString())  ;		
			}			
			return retResult;
		}

		
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDriverDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/// 
		public int AddPolicyDriverDetails(ClsPolicyDriverInfo objDriverDetailsInfo)
		{			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
				objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
				objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);

				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);

				objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",DefaultValues.GetStringNull(objDriverDetailsInfo.DRIVER_LIC_STATE));
			
				objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			
				if (objDriverDetailsInfo.DATE_EXP_START.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_EXP_START",objDriverDetailsInfo.DATE_EXP_START);
				else
					objDataWrapper.AddParameter("@DATE_EXP_START",null);

				objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
		    
				objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
			
				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
				
				if(objDriverDetailsInfo.DRIVER_INCOME==0)
					objDataWrapper.AddParameter("@DRIVER_INCOME",null);
				else
					objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);

				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);
				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
				objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@SAFE_DRIVER",objDriverDetailsInfo.SAFE_DRIVER);
				objDataWrapper.AddParameter("@GOOD_DRIVER_STUDENT_DISCOUNT",objDriverDetailsInfo.GOOD_DRIVER_STUDENT_DISCOUNT);
				objDataWrapper.AddParameter("@PREMIER_DRIVER_DISCOUNT",objDriverDetailsInfo.PREMIER_DRIVER_DISCOUNT);
				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);
				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
				
				//Added by Swarup on 15/12/2006
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
				
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);
				
				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);

				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/AddPolicyDriver.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
					objTransactionInfo.POLICY_ID 		=	objDriverDetailsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objDriverDetailsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1503", "");//"New policy driver is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyDriver" ,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyDriver");
				}
				
				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (DRIVER_ID == -1)
				{
					return -1;
				}
				else
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
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

		public int AddPolicyDriverDetails(ClsPolicyDriverInfo objDriverDetailsInfo, string strCalledFrom, string strCustomInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
				objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
				objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);
				objDataWrapper.AddParameter("@FORM_F95",objDriverDetailsInfo.FORM_F95); 
				objDataWrapper.AddParameter("@EXT_NON_OWN_COVG_INDIVI",objDriverDetailsInfo.EXT_NON_OWN_COVG_INDIVI); 
			
				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);

				objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",DefaultValues.GetStringNull(objDriverDetailsInfo.DRIVER_DRIV_LIC));
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",DefaultValues.GetStringNull(objDriverDetailsInfo.DRIVER_LIC_STATE));
			
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);
	
				objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			
				if (objDriverDetailsInfo.DATE_LICENSED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_LICENSED",objDriverDetailsInfo.DATE_LICENSED);
				else
					objDataWrapper.AddParameter("@DATE_LICENSED",null);

				objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
		    
				objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
			
				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
				
				if(objDriverDetailsInfo.DRIVER_INCOME==0)
					objDataWrapper.AddParameter("@DRIVER_INCOME",null);
				else
					objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);

				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);
				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",DefaultValues.GetStringNull(objDriverDetailsInfo.DRIVER_DRINK_VIOLATION));
				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
				objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@SAFE_DRIVER",objDriverDetailsInfo.SAFE_DRIVER);
				objDataWrapper.AddParameter("@GOOD_DRIVER_STUDENT_DISCOUNT",objDriverDetailsInfo.GOOD_DRIVER_STUDENT_DISCOUNT);
				objDataWrapper.AddParameter("@PREMIER_DRIVER_DISCOUNT",objDriverDetailsInfo.PREMIER_DRIVER_DISCOUNT);
				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);
				objDataWrapper.AddParameter("@VEHICLE_ID",DefaultValues.GetIntNull(objDriverDetailsInfo.VEHICLE_ID));
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				if(strCalledFrom.ToString().ToUpper()=="PPA")
					objDataWrapper.AddParameter("@NO_DEPENDENTS",DefaultValues.GetIntNull(objDriverDetailsInfo.NO_DEPENDENTS));

				objDataWrapper.AddParameter ("@WAIVER_WORK_LOSS_BENEFITS", objDriverDetailsInfo.WAIVER_WORK_LOSS_BENEFITS);
				objDataWrapper.AddParameter ("@FULL_TIME_STUDENT", objDriverDetailsInfo.FULL_TIME_STUDENT);
				objDataWrapper.AddParameter ("@SUPPORT_DOCUMENT", objDriverDetailsInfo.SUPPORT_DOCUMENT);
				objDataWrapper.AddParameter ("@SIGNED_WAIVER_BENEFITS_FORM", objDriverDetailsInfo.SIGNED_WAIVER_BENEFITS_FORM);

				objDataWrapper.AddParameter ("@HAVE_CAR", objDriverDetailsInfo.HAVE_CAR);
				objDataWrapper.AddParameter ("@STATIONED_IN_US_TERR", objDriverDetailsInfo.STATIONED_IN_US_TERR);
				objDataWrapper.AddParameter ("@IN_MILITARY", objDriverDetailsInfo.IN_MILITARY);
				objDataWrapper.AddParameter("@PARENTS_INSURANCE",objDriverDetailsInfo.PARENTS_INSURANCE);
				
				//Added By swarup on 15/12/2006
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
				
				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
//				if(TransactionLogRequired)
//				{
//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/AddPolicyDriver.aspx.resx");
//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
//					objTransactionInfo.POLICY_ID 		=	objDriverDetailsInfo.POLICY_ID;
//					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objDriverDetailsInfo.POLICY_VERSION_ID;
//					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
//					objTransactionInfo.TRANS_DESC		=	"New policy driver is added";
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='RELATIONSHIP' and @NewValue='0']","NewValue","null");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
//					objTransactionInfo.CHANGE_XML		=	strTranXML;
//					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
//					//Executing the query
//					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyDriver" ,objTransactionInfo);
//				}
//				else
//				{
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyDriver");
//				}
				
				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				if(DRIVER_ID!=-1)
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
					objDataWrapper.ClearParameteres();
					DataTable dtVehicle =new DataTable();
					dtVehicle=ClsVehicleInformation.GetPolVehicle(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,objDriverDetailsInfo.DRIVER_ID);
					
					string sbStrXml="";
					AddPolAssignedVehicles(objDataWrapper,objDriverDetailsInfo,strCalledFrom,dtVehicle,ref sbStrXml);
					if(TransactionLogRequired)
					{
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/AddPolicyDriver.aspx.resx");						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='RELATIONSHIP' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_ID']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
						strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/AddPolicyDriver.aspx.resx");
						//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
						objTransactionInfo.POLICY_ID 		=	objDriverDetailsInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objDriverDetailsInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1503", ""); //"New policy driver is added";
						
						
						//strTranXML = strTranXML.Remove("</LabelFieldMapping>","");
						//strTranXML.Append(sbStrXml);
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						//Executing the query
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
					}

				}
				
				//				foreach (string str in objDriverDetailsInfo.ASSIGNED_VEHICLE.Split('|'))
				//				{
				//					objDataWrapper.ClearParameteres();
				//					objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
				//					objDataWrapper.AddParameter("@PolID",objDriverDetailsInfo.POLICY_ID);
				//					objDataWrapper.AddParameter("@PolVersionID",objDriverDetailsInfo.POLICY_VERSION_ID);
				//					objDataWrapper.AddParameter("@DriverID",DRIVER_ID);
				//					objDataWrapper.AddParameter("@VehID",str.Split('~')[0]);
				//					objDataWrapper.AddParameter("@PrinOccID",str.Split('~')[1]);					
				//					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolAssignedVehicle");
				//				}

				
				///Update Endorsements based on Driver attributes/////////////////////////////////////////////
				//this.UpdatePolicyDriverEndorsements(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,objDataWrapper);
				objDataWrapper.ClearParameteres();
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID ,objDriverDetailsInfo.POLICY_ID ,objDriverDetailsInfo.POLICY_VERSION_ID,RuleType.AutoDriverDep);
				
				/////////////////////////////////////////////////////////////////////////////////////////////
			
				
				
				
				if (DRIVER_ID == -1)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
				else
				{
					objDataWrapper.ClearParameteres();
					//objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
					//Update the Vehicle Class : 18 May 2006
					//UpdateVehicleClassPol(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID ,objDriverDetailsInfo.POLICY_VERSION_ID);
					//End Update the Vehicle Class
					if(returnResult>0)
						UpdateVehicleClassPolNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID ,objDriverDetailsInfo.POLICY_VERSION_ID,objDataWrapper);
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
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



		/// <summary>
		/// Returns the driver details into the xml string format.
		/// </summary>
		/// <param name="intCustomerId"></param>
		/// <param name="intPolicyId"></param>
		/// <param name="intPolicyVersionId"></param>
		/// <param name="intDriverId"></param>
		/// <returns></returns>
		public static string GetPolicyDriverDetailsXML(int intCustomerId, int intPolicyId, int intPolicyVersionId, int intDriverId)
		{

			DataSet dsPolicyInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",intPolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);

				dsPolicyInfo = objDataWrapper.ExecuteDataSet("Proc_GetPolicyDriverDetails");
				
				if (dsPolicyInfo.Tables[0].Rows.Count != 0)
				{
					return dsPolicyInfo.GetXml();
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


		/// <summary>
		/// This function will activate or deactivate the policy driver.
		/// </summary>
		/// <param name="CustomerId"></param>
		/// <param name="PolicyId"></param>
		/// <param name="PolicyVersionId"></param>
		/// <param name="intDriverId"></param>
		/// <param name="strStatus"></param>
		/// Sumit Chhabra:03/12/2007
		/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
		public int ActivateDeactivatePolicyDriver(int CustomerId, int PolicyId, int PolicyVersionId, int intDriverId, string strStatus,int modifiedBy, string strCustomInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			int queryResult;
			string strStoredProc = "Proc_ActivateDeactivatePolicyDriver";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				if(TransactionLogRequired) 
				{

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	modifiedBy;
					objTransactionInfo.POLICY_ID 		=	PolicyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	PolicyVersionId;
					objTransactionInfo.CLIENT_ID		=	CustomerId;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					if(strStatus.ToUpper()=="Y")
                        objTransactionInfo.TRANS_DESC   =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1599", "");//"Policy Driver is Activated";
					else
                        objTransactionInfo.TRANS_DESC   =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1600", "");//"Policy Driver is Deactivated";
					queryResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					queryResult=objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();

				///Update Endorsements based on Driver attributes/////////////////////////////////////////////
				//this.UpdatePolicyDriverEndorsements(CustomerId,PolicyId,PolicyVersionId,objDataWrapper);
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper ,CustomerId ,PolicyId,PolicyVersionId,RuleType.AutoDriverDep);
				
				////////////////////////////////////////////////////////////////////////////////////////
				
				objDataWrapper.ClearParameteres();
				//Update the Vehicle Class : 18 May 2006
				//UpdateVehicleClassPol(CustomerId,PolicyId,PolicyVersionId);
				UpdateVehicleClassPolNew(CustomerId,PolicyId,PolicyVersionId,objDataWrapper);
				//End Update the Vehicle Class

				if(queryResult>0)
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return 1;
				}
				else
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}

		public int ActivateDeactivateUmbrellaPolicyDriver(int CustomerId, int PolicyId, int PolicyVersionId, int intDriverId, string strStatus,int modifiedBy, string strCustomInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_ActivateDeactivatePolicyUmbrellaDriver";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				if(TransactionLogRequired) 
				{

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	modifiedBy;
					objTransactionInfo.POLICY_ID 		=	PolicyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	PolicyVersionId;
					objTransactionInfo.CLIENT_ID		=	CustomerId;
					objTransactionInfo.CUSTOM_INFO = strCustomInfo;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1599","");//"Policy Driver is Activated";
					else
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1600", "");// "Policy Driver is Deactivated";

					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				//added by pravesh
				ClsUmbrellaCoverages objUmbCoverage=new ClsUmbrellaCoverages();
				objUmbCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,CustomerId,PolicyId,PolicyVersionId,RuleType.RiskDependent,0);     
				//end 								
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
		/// <param name="objOldDriverDetailsInfo">Model object having old information</param>
		/// <param name="objDriverDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicyDriver(ClsPolicyDriverInfo objOldDriverDetailsInfo,ClsPolicyDriverInfo objDriverDetailsInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
				objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
				objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);
				objDataWrapper.AddParameter("@FORM_F95",objDriverDetailsInfo.FORM_F95); 
				objDataWrapper.AddParameter("@EXT_NON_OWN_COVG_INDIVI",objDriverDetailsInfo.EXT_NON_OWN_COVG_INDIVI); 
			
				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);

				objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);
			
				objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			
				if (objDriverDetailsInfo.DATE_EXP_START.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_EXP_START",objDriverDetailsInfo.DATE_EXP_START);
				else
					objDataWrapper.AddParameter("@DATE_EXP_START",null);

				objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
		    
				objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
			
				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
				
				if(objDriverDetailsInfo.DRIVER_INCOME==0)
					objDataWrapper.AddParameter("@DRIVER_INCOME",null);
				else
					objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);

				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);
				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
				objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@SAFE_DRIVER",objDriverDetailsInfo.SAFE_DRIVER);
				objDataWrapper.AddParameter("@GOOD_DRIVER_STUDENT_DISCOUNT",objDriverDetailsInfo.GOOD_DRIVER_STUDENT_DISCOUNT);
				objDataWrapper.AddParameter("@PREMIER_DRIVER_DISCOUNT",objDriverDetailsInfo.PREMIER_DRIVER_DISCOUNT);
				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);
				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
//				if()
//				{
//					objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID","11931"); 
//				}
//				else
//				{
					objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
			//	}
				objDataWrapper.AddParameter("@MODIFIED_BY", objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDriverDetailsInfo.LAST_UPDATED_DATETIME);
				
				//Added by swarup on 15/12/2006
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);

				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				if(TransactionLogRequired) 
				{

					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/AddPolicyDriver.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID 		=	objDriverDetailsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objDriverDetailsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1597", "");// "Policy Driver is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyDriver", objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyDriver");
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
		public static void InsertPolicyExistingDriver(DataTable dtSelectedDriver,int from_Customer_ID,int from_Policy_ID,int from_Policy_Version_ID,int from_User_Id)
		{			
			string	strStoredProc =	"Proc_InsertPolicyExistingDriver";
			

			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];					
					
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_ID",int.Parse(dr["PolicyID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_VERSION_ID",int.Parse(dr["PolicyVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_ID",from_Policy_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_VERSION_ID",from_Policy_Version_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
			
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}	
		
		// Commented by Swastika : Currently not being used.
		//Added by Swastika on 27th Mar'06 for Pol Iss # 31
		////		public void InsertPolicyExistingUmbrellaOperator(DataTable dtSelectedDriver,int from_Customer_ID,int from_Policy_ID,int from_Policy_Version_ID,int from_User_Id)
		////		{			
		////			string	strStoredProc = "Proc_InsertPolicyExistingUmbrellaOperator";
		////			string DriverInfo="";
		////			int returnResult;
		////			try
		////			{				
		////				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
		////				{
		////					DataRow dr=dtSelectedDriver.Rows[i];					
		////					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
		////					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
		////					objDataWrapper.AddParameter("@FROM_POLICY_ID",int.Parse(dr["PolicyID"].ToString()),SqlDbType.Int);
		////					objDataWrapper.AddParameter("@FROM_POLICY_VERSION_ID",int.Parse(dr["PolicyVersionID"].ToString()),SqlDbType.Int);
		////					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
		////					objDataWrapper.AddParameter("@TO_POLICY_ID",from_Policy_ID,SqlDbType.Int);
		////					objDataWrapper.AddParameter("@TO_POLICY_VERSION_ID",from_Policy_Version_ID,SqlDbType.Int);
		////					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
		////					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
		////					//objDataWrapper.ExecuteNonQuery(strStoredProc);
		////					DriverInfo +=";Operator Name = " + dr["DriverName"].ToString() + ", Operator Code = " + dr["DriverCode"].ToString();
		////
		////					if(TransactionLogRequired && i==(dtSelectedDriver.Rows.Count-1))
		////					{
		////						ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
		////						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyCopyApplicantDriver.aspx.resx");
		////						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		////						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
		////						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		////						objTransactionInfo.TRANS_TYPE_ID	=	1;
		////						objTransactionInfo.POLICY_ID			=	from_Policy_ID;
		////						objTransactionInfo.POLICY_VER_TRACKING_ID	=	from_Policy_Version_ID;
		////						objTransactionInfo.CLIENT_ID		=	from_Customer_ID;
		////						objTransactionInfo.RECORDED_BY		=	from_User_Id;
		////						objTransactionInfo.TRANS_DESC		=	"Operator is copied";
		////						objTransactionInfo.CUSTOM_INFO		=	DriverInfo;
		////						//objTransactionInfo.CHANGE_XML		=	strTranXML;
		////						//Executing the query
		////						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		////					}
		////					else
		////					{
		////						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
		////					}							
		////				}							
		////			}
		////			catch(Exception exc)
		////			{
		////				throw (exc);
		////			}
		////			finally
		////			{}
		////		}	
		


		
		public void InsertPolicyExistingUmbrellaDriver(DataTable dtSelectedDriver,int from_Customer_ID,int from_Policy_ID,int from_Policy_Version_ID,int from_User_Id)
		{			
			string	strStoredProc = "Proc_InsertPolicyExistingUmbrellaDriver";
			string DriverInfo="";
			int returnResult;
			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];					
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_ID",int.Parse(dr["PolicyID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_VERSION_ID",int.Parse(dr["PolicyVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_ID",from_Policy_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_VERSION_ID",from_Policy_Version_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					//objDataWrapper.ExecuteNonQuery(strStoredProc);
					DriverInfo +=";Driver Name = " + dr["DriverName"].ToString() + ", Driver Code = " + dr["DriverCode"].ToString();

					if(TransactionLogRequired && i==(dtSelectedDriver.Rows.Count-1))
					{
						ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyCopyApplicantDriver.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID			=	from_Policy_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	from_Policy_Version_ID;
						objTransactionInfo.CLIENT_ID		=	from_Customer_ID;
						objTransactionInfo.RECORDED_BY		=	from_User_Id;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1519", "");// " Policy Driver is copied";
						objTransactionInfo.CUSTOM_INFO		=	DriverInfo;
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}							
				}							
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}	

		

		
		public void InsertPolicyExistingWatercraftDriver(DataTable dtSelectedDriver,int from_Customer_ID,int from_Policy_ID,int from_Policy_Version_ID,int from_User_Id)
		{			
			string	strStoredProc = "Proc_InsertPolicyExistingWatercraftDriver";
			string DriverInfo="";
			int returnResult;
			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];					
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_ID",int.Parse(dr["PolicyID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_VERSION_ID",int.Parse(dr["PolicyVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_ID",from_Policy_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_VERSION_ID",from_Policy_Version_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					//objDataWrapper.ExecuteNonQuery(strStoredProc);
					DriverInfo +=";Operator Name = " + dr["DriverName"].ToString() + ", Operator Code = " + dr["DriverCode"].ToString();

					if(TransactionLogRequired && i==(dtSelectedDriver.Rows.Count-1))
					{
						ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyCopyApplicantDriver.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID			=	from_Policy_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	from_Policy_Version_ID;
						objTransactionInfo.CLIENT_ID		=	from_Customer_ID;
						objTransactionInfo.RECORDED_BY		=	from_User_Id;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1598", "");// "Operator is copied";
						objTransactionInfo.CUSTOM_INFO		=	DriverInfo;
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}					
				}							
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}

	

		
		public void InsertPolicyExistingDriver(DataTable dtSelectedDriver,int from_Customer_ID,int from_Policy_ID,int from_Policy_Version_ID,int from_User_Id, string strCalledFrom)
		{			
			string	strStoredProc = "Proc_InsertPolicyExistingDriver";
			string DriverInfo = "";
			int returnResult;

			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];					
					//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_ID",int.Parse(dr["PolicyID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_VERSION_ID",int.Parse(dr["PolicyVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_ID",from_Policy_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_VERSION_ID",from_Policy_Version_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					objDataWrapper.AddParameter("@Called_From",strCalledFrom,SqlDbType.VarChar);							
					DriverInfo +=";Driver Name = " + dr["DriverName"].ToString() + ", Driver Code = " + dr["DriverCode"].ToString();

					if(TransactionLogRequired && i==(dtSelectedDriver.Rows.Count-1))
					{
						ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyCopyApplicantDriver.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID			=	from_Policy_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	from_Policy_Version_ID;
						objTransactionInfo.CLIENT_ID		=	from_Customer_ID;
						objTransactionInfo.RECORDED_BY		=	from_User_Id;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1584", "");// "Policy Driver is copied";
						objTransactionInfo.CUSTOM_INFO		=	DriverInfo;
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}				
					objDataWrapper.ClearParameteres();					
				}
				objDataWrapper.ClearParameteres();
				//Update Driver endorsements
				//Update Endorsements//////////
				//this.UpdatePolicyDriverEndorsements(from_Customer_ID,from_Policy_ID,from_Policy_Version_ID,objDataWrapper);
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper ,from_Customer_ID ,from_Policy_ID,from_Policy_Version_ID,RuleType.AutoDriverDep);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
				////////////////////////////
				///

			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw (exc);
			}
			finally
			{}
		}	
	
		public int UpdatePolicyDriver(ClsPolicyDriverInfo objOldDriverDetailsInfo,ClsPolicyDriverInfo objDriverDetailsInfo,string strCalledFrom, string strCustomInfo)
		{
			return 	UpdatePolicyDriver(objOldDriverDetailsInfo,objDriverDetailsInfo,strCalledFrom,strCustomInfo,"");
		}
		
		public int UpdatePolicyDriver(ClsPolicyDriverInfo objOldDriverDetailsInfo,ClsPolicyDriverInfo objDriverDetailsInfo, string strCalledFrom, string strCustomInfo,string strAssignXml)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
				objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
				objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);
				objDataWrapper.AddParameter("@FORM_F95",objDriverDetailsInfo.FORM_F95); 
				objDataWrapper.AddParameter("@EXT_NON_OWN_COVG_INDIVI",objDriverDetailsInfo.EXT_NON_OWN_COVG_INDIVI); 
			
				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);

				objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);
			
				objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			
				if (objDriverDetailsInfo.DATE_LICENSED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_LICENSED",objDriverDetailsInfo.DATE_LICENSED);
				else
					objDataWrapper.AddParameter("@DATE_LICENSED",null);

				objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
		    
				objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
			
				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
				
				if(objDriverDetailsInfo.DRIVER_INCOME==0)
					objDataWrapper.AddParameter("@DRIVER_INCOME",null);
				else
					objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);

				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",null);
				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
				objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@SAFE_DRIVER",objDriverDetailsInfo.SAFE_DRIVER);
				objDataWrapper.AddParameter("@GOOD_DRIVER_STUDENT_DISCOUNT",objDriverDetailsInfo.GOOD_DRIVER_STUDENT_DISCOUNT);
				objDataWrapper.AddParameter("@PREMIER_DRIVER_DISCOUNT",objDriverDetailsInfo.PREMIER_DRIVER_DISCOUNT);
				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);
				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				if(objDriverDetailsInfo.IN_MILITARY.ToString()=="10963" && objDriverDetailsInfo.STATIONED_IN_US_TERR.ToString()=="10964")
				{
					objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID","11931"); 
				}
				else
				{
					objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",DefaultValues.GetIntNullFromNegative(objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID)); 
				}
				objDataWrapper.AddParameter("@MODIFIED_BY", objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDriverDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@CALLED_FROM", strCalledFrom);
				if(strCalledFrom.ToString().ToUpper()=="PPA")
					objDataWrapper.AddParameter("@NO_DEPENDENTS",DefaultValues.GetIntNull(objDriverDetailsInfo.NO_DEPENDENTS));

				objDataWrapper.AddParameter ("@WAIVER_WORK_LOSS_BENEFITS", objDriverDetailsInfo.WAIVER_WORK_LOSS_BENEFITS);
				objDataWrapper.AddParameter ("@FULL_TIME_STUDENT", objDriverDetailsInfo.FULL_TIME_STUDENT);
				objDataWrapper.AddParameter ("@SUPPORT_DOCUMENT", objDriverDetailsInfo.SUPPORT_DOCUMENT);
				objDataWrapper.AddParameter ("@SIGNED_WAIVER_BENEFITS_FORM", objDriverDetailsInfo.SIGNED_WAIVER_BENEFITS_FORM);
			
				objDataWrapper.AddParameter ("@HAVE_CAR", objDriverDetailsInfo.HAVE_CAR);
				objDataWrapper.AddParameter ("@STATIONED_IN_US_TERR", objDriverDetailsInfo.STATIONED_IN_US_TERR);
				objDataWrapper.AddParameter ("@IN_MILITARY", objDriverDetailsInfo.IN_MILITARY);
				objDataWrapper.AddParameter("@PARENTS_INSURANCE",objDriverDetailsInfo.PARENTS_INSURANCE);
				
				//Added by Swarup on 15/12/2006
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}
			
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);
				
				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);

				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );
				
				//				if(TransactionLogRequired) 
//				{
//
//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/AddPolicyDriver.aspx.resx");
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);
//					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
//						returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyDriver");
//					else
//					{
//						objTransactionInfo.TRANS_TYPE_ID	=	3;
//						objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
//						objTransactionInfo.POLICY_ID 		=	objDriverDetailsInfo.POLICY_ID;
//						objTransactionInfo.POLICY_VER_TRACKING_ID =	objDriverDetailsInfo.POLICY_VERSION_ID;
//						objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
//						objTransactionInfo.TRANS_DESC		=	"Policy Driver is modified";
//						objTransactionInfo.CHANGE_XML		=	strTranXML;
//						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
//						returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyDriver", objTransactionInfo);
//					}
//
//				}
//				else
//				{
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyDriver");
//				}
				
				objDataWrapper.ClearParameteres();

				DataTable dtVehicle =new DataTable();
				dtVehicle=ClsVehicleInformation.GetPolVehicle(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,objDriverDetailsInfo.DRIVER_ID);
				string sbStrXml="";
				AddPolAssignedVehicles(objDataWrapper,strAssignXml,objDriverDetailsInfo,strCalledFrom,dtVehicle,ref sbStrXml);
				//sbStrXml = sbStrXml.Remove("<LabelFieldMapping>");
				//int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/AddPolicyDriver.aspx.resx");
					//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_ID']");
					strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID 		=	objDriverDetailsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objDriverDetailsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1520", "");// "Policy Automobile driver is modified";
					//strTranXML = strTranXML.Remove("</LabelFieldMapping>","");
					//strTranXML.Append(sbStrXml);
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//				objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
				//				objDataWrapper.AddParameter("@PolID",objDriverDetailsInfo.POLICY_ID);
				//				objDataWrapper.AddParameter("@PolVersionID",objDriverDetailsInfo.POLICY_VERSION_ID);
				//				objDataWrapper.AddParameter("@DriverID",objDriverDetailsInfo.DRIVER_ID);
				//				objDataWrapper.ExecuteNonQuery("Proc_DeletePolAssignedVehicle");
				//
				//				foreach (string str in objDriverDetailsInfo.ASSIGNED_VEHICLE.Split('|'))
				//				{
				//					objDataWrapper.ClearParameteres();
				//					objDataWrapper.AddParameter("@CustID",objDriverDetailsInfo.CUSTOMER_ID);
				//					objDataWrapper.AddParameter("@PolID",objDriverDetailsInfo.POLICY_ID);
				//					objDataWrapper.AddParameter("@PolVersionID",objDriverDetailsInfo.POLICY_VERSION_ID);
				//					objDataWrapper.AddParameter("@DriverID",objDriverDetailsInfo.DRIVER_ID);
				//					objDataWrapper.AddParameter("@VehID",str.Split('~')[0]);
				//					objDataWrapper.AddParameter("@PrinOccID",str.Split('~')[1]);					
				//					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolAssignedVehicle");
				//				}


				///Update Endorsements based on Driver attributes/////////////////////////////////////////////
				//this.UpdatePolicyDriverEndorsements(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,objDataWrapper);
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID ,objDriverDetailsInfo.POLICY_ID ,objDriverDetailsInfo.POLICY_VERSION_ID,RuleType.AutoDriverDep);
				
				/////////////////////////////////////////////////////////////////////////////////////////////
				///
				
				
				//Update the Vehicle Class : 18 May 2006
				//UpdateVehicleClassPol(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID ,objDriverDetailsInfo.POLICY_VERSION_ID);
				if(returnResult>0)
					UpdateVehicleClassPolNew(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID ,objDriverDetailsInfo.POLICY_VERSION_ID,objDataWrapper);
				//End Update the Vehicle Class

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
		/// This function will delete the policy driver.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="driverID"></param>
		/// <returns></returns>
		/// Sumit Chhabra:03/12/2007
		/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
		public int DeletePolicyDriver(int customerID,int policyID,int policyVersionID,int driverID, int modifiedBy,string strCustomInfo)
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@POLICY_ID",policyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",driverID);
				
				if(TransactionLogRequired) 
				{

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	modifiedBy;
					objTransactionInfo.POLICY_ID 		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionID;
					objTransactionInfo.CLIENT_ID		=	customerID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1522", "");// "Policy Driver is Deleted";
					objTransactionInfo.CUSTOM_INFO		= strCustomInfo;
						
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_DeletePolicyDriver", objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_DeletePolicyDriver");
				
				objDataWrapper.ClearParameteres();

				///Update Endorsements based on Driver attributes/////////////////////////////////////////////
				//this.UpdatePolicyDriverEndorsements(customerID,policyID,policyVersionID,objDataWrapper);
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper ,customerID ,policyID,policyVersionID,RuleType.AutoDriverDep);
				
				/////////////////////////////////////////////////////////////////////////////////////////////
				objDataWrapper.ClearParameteres();
				UpdateVehicleClassPolNew(customerID,policyID,policyVersionID,objDataWrapper);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return returnResult;
	
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(exc);
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


		public int DeletePolicyUmbrellaDriver(int customerID,int policyID,int policyVersionID,int driverID, int modifiedBy, string strCustomInfo)
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@POLICY_ID",policyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",driverID);
				
				if(TransactionLogRequired) 
				{

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	modifiedBy;
					objTransactionInfo.POLICY_ID 		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionID;
					objTransactionInfo.CLIENT_ID		=	customerID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1522", "");// "Policy Driver is Deleted";
					objTransactionInfo.CUSTOM_INFO = strCustomInfo;
						
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_DeletePolicyUmbrellaDriver", objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_DeletePolicyUmbrellaDriver");
				
				objDataWrapper.ClearParameteres();

				///Update Endorsements based on Driver attributes/////////////////////////////////////////////
				//this.UpdatePolicyDriverEndorsements(customerID,policyID,policyVersionID,objDataWrapper);
				/////////////////////////////////////////////////////////////////////////////////////////////
				//for Umbrella Coverages by Pravesh
				ClsUmbrellaCoverages objUmbCoverage=new ClsUmbrellaCoverages();
				objUmbCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,customerID,policyID,policyVersionID,RuleType.RiskDependent,0);			
				///end 
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return returnResult;
	
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(exc);
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
	
		#region Policy MotorCycle Functions
		/// <summary>
		/// This function will return the policy motorcycle driver string in xml format.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="driverID"></param>
		/// <returns></returns>
		public static string GetPolicyMotorDriverXML(int customerID, int policyID, int policyVersionID, int driverID)
		{

			DataSet dsPolicyInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@POLICY_ID",policyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",driverID);

				dsPolicyInfo = objDataWrapper.ExecuteDataSet("Proc_GetPolicyMotorDriver");
				
				if (dsPolicyInfo.Tables[0].Rows.Count != 0)
				{
					return dsPolicyInfo.GetXml();
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

		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDriverDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/// Sumit Chhabra:12/03/2007:Given method is overloaded, thus call transferred to overloaded method
		public int AddPolicyMotorDriver(ClsPolicyDriverInfo objDriverDetailsInfo)
		{
			return AddPolicyMotorDriver(objDriverDetailsInfo,"","");
			//			DateTime	RecordDate		=	DateTime.Now;
			//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//
			//			try
			//			{
			//				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
			//				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
			//				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
			//				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
			//				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
			//				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
			//				objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
			//				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
			//				objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
			//				objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
			//				objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
			//				objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
			//				objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
			//				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
			//				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
			//				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
			//				objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
			//				objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);
			//
			//				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
			//					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
			//				else
			//					objDataWrapper.AddParameter("@DRIVER_DOB",null);
			//
			//				objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
			//				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
			//				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
			//				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
			//				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);
			//			
			//				objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			//			
			//				if (objDriverDetailsInfo.DATE_LICENSED.Ticks != 0)
			//					objDataWrapper.AddParameter("@DATE_LICENSED",objDriverDetailsInfo.DATE_LICENSED);
			//				else
			//					objDataWrapper.AddParameter("@DATE_LICENSED",null);
			//
			//				objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
			//				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
			//				objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
			//				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
			//				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
			//				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
			//				if(objDriverDetailsInfo.DRIVER_INCOME==0)
			//				{
			//					objDataWrapper.AddParameter("@DRIVER_INCOME",null);
			//				}
			//				else
			//
			//					objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);
			//				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
			//				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
			//				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
			//				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
			//				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
			//				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
			//				objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
			//				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
			//				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
			//				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);
			//				objDataWrapper.AddParameter("@DRIVER_FAX",objDriverDetailsInfo.DRIVER_FAX);
			//				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
			//				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
			//				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
			//				objDataWrapper.AddParameter("@MATURE_DRIVER",objDriverDetailsInfo.MATURE_DRIVER);
			//				objDataWrapper.AddParameter("@MATURE_DRIVER_DISCOUNT",objDriverDetailsInfo.MATURE_DRIVER_DISCOUNT);
			//				objDataWrapper.AddParameter("@PREFERRED_RISK_DISCOUNT",objDriverDetailsInfo.PREFERRED_RISK_DISCOUNT);
			//				objDataWrapper.AddParameter("@PREFERRED_RISK",objDriverDetailsInfo.PREFERRED_RISK);
			//				objDataWrapper.AddParameter("@TRANSFEREXP_RENEWAL_DISCOUNT",objDriverDetailsInfo.TRANSFEREXP_RENEWAL_DISCOUNT);
			//				objDataWrapper.AddParameter("@TRANSFEREXPERIENCE_RENEWALCREDIT",objDriverDetailsInfo.TRANSFEREXPERIENCE_RENEWALCREDIT);
			//
			//				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
			//				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
			//				
			//				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
			//				{
			//					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
			//				}
			//
			//				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
			//				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
			//				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID); 
			//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);
			//
			//				int returnResult = 0;
			//				if(TransactionLogRequired)
			//				{
			//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MotorCycle/PolicyAddMotorDriver.aspx.resx");
			//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			//					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
			//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			//					objTransactionInfo.TRANS_TYPE_ID	=	1;
			//					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
			//					objTransactionInfo.POLICY_ID		=	objDriverDetailsInfo.POLICY_ID; 
			//					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objDriverDetailsInfo.POLICY_VERSION_ID; 
			//					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
			//					objTransactionInfo.TRANS_DESC		=	"New policy driver is added";
			//					objTransactionInfo.CHANGE_XML		=	strTranXML;
			//					//Executing the query
			//					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyMotorDriver" ,objTransactionInfo);
			//				}
			//				else
			//				{
			//					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyMotorDriver");
			//				}
			//				
			//				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
			//				
			//				objDataWrapper.ClearParameteres();
			//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			//				
			//				if (DRIVER_ID == -1)
			//				{
			//					return -1;
			//				}
			//				else
			//				{
			//					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
			//					return returnResult;
			//				}
			//			}
			//			catch(Exception ex)
			//			{
			//				throw(ex);
			//			}
			//			finally
			//			{
			//				if(objDataWrapper != null) objDataWrapper.Dispose();
			//			}
		}


		public int AddPolicyMotorDriver(ClsPolicyDriverInfo objDriverDetailsInfo,string strCalledFrom, string strCustomInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
				objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
				objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);

				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);

				objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);
			
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);
				//objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);
			
				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				if (objDriverDetailsInfo.DATE_LICENSED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_LICENSED",objDriverDetailsInfo.DATE_LICENSED);
				else
					objDataWrapper.AddParameter("@DATE_LICENSED",null);

				objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
				//objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
				if(objDriverDetailsInfo.DRIVER_INCOME==0)
				{
					objDataWrapper.AddParameter("@DRIVER_INCOME",null);
				}
				else

					objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);
				//objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
				//objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);
				//objDataWrapper.AddParameter("@DRIVER_FAX",objDriverDetailsInfo.DRIVER_FAX);
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@CREATED_BY",objDriverDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDriverDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MATURE_DRIVER",objDriverDetailsInfo.MATURE_DRIVER);
				objDataWrapper.AddParameter("@MATURE_DRIVER_DISCOUNT",objDriverDetailsInfo.MATURE_DRIVER_DISCOUNT);
				objDataWrapper.AddParameter("@PREFERRED_RISK_DISCOUNT",objDriverDetailsInfo.PREFERRED_RISK_DISCOUNT);
				objDataWrapper.AddParameter("@PREFERRED_RISK",objDriverDetailsInfo.PREFERRED_RISK);
				/*Commented by Charles on 2-Jul-09 for Itrack issue 6012
				objDataWrapper.AddParameter("@TRANSFEREXP_RENEWAL_DISCOUNT",objDriverDetailsInfo.TRANSFEREXP_RENEWAL_DISCOUNT);
				objDataWrapper.AddParameter("@TRANSFEREXPERIENCE_RENEWALCREDIT",objDriverDetailsInfo.TRANSFEREXPERIENCE_RENEWALCREDIT);
				*/
				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID); 
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom); 
				objDataWrapper.AddParameter("@NO_CYCLE_ENDMT",objDriverDetailsInfo.NO_CYCLE_ENDMT); 
				objDataWrapper.AddParameter("@CYCL_WITH_YOU",objDriverDetailsInfo.CYCL_WITH_YOU); 
				objDataWrapper.AddParameter("@COLL_STUD_AWAY_HOME",objDriverDetailsInfo.COLL_STUD_AWAY_HOME);
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
//				if(TransactionLogRequired)
//				{
//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MotorCycle/PolicyAddMotorDriver.aspx.resx");
//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//					string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
//					objTransactionInfo.POLICY_ID		=	objDriverDetailsInfo.POLICY_ID; 
//					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objDriverDetailsInfo.POLICY_VERSION_ID; 
//					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
//					objTransactionInfo.TRANS_DESC		=	"Policy Motorcycle driver is added";
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
//					objTransactionInfo.CHANGE_XML		=	strTranXML;
//					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
//					//Executing the query
//					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyMotorDriver" ,objTransactionInfo);
//				}
//				else
//				{
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyMotorDriver");
//				}
				
				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
				if(DRIVER_ID!=-1)
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
					DataTable dtVehicle =new DataTable();
					dtVehicle=ClsVehicleInformation.GetPolVehicle(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,objDriverDetailsInfo.DRIVER_ID);
					string sbStrXml="";
					AddPolAssignedVehicles(objDataWrapper,objDriverDetailsInfo,strCalledFrom,dtVehicle,ref sbStrXml);
					if(TransactionLogRequired)
					{
						objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MotorCycle/PolicyAddMotorDriver.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_ID']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
						strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.CREATED_BY;
						objTransactionInfo.POLICY_ID		=	objDriverDetailsInfo.POLICY_ID; 
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	objDriverDetailsInfo.POLICY_VERSION_ID; 
						objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1585", "");// "Policy Motorcycle driver is added";
						
						//strTranXML = strTranXML.Remove("</LabelFieldMapping>","");
						//strTranXML.Append(sbStrXml);
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						//Executing the query
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
					}
				}

				
				
				if (DRIVER_ID == -1)
				{
					return -1;

				}
				else
				{
					objDriverDetailsInfo.DRIVER_ID = DRIVER_ID;
					objDataWrapper.ClearParameteres();
					ClsVehicleCoverages objCoverage=new ClsVehicleCoverages("MOTOR");
					objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,RuleType.AutoDriverDep);
					objDataWrapper.ClearParameteres();
					UpdateMotorVehicleClassPOL(objDataWrapper,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID);
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
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

		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldDriverDetailsInfo">Model object having old information</param>
		/// <param name="objDriverDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		/// Sumit Chhabra:03/12/2007: Following method is overrloaded, thus call transferred to new method
		public int UpdatePolicyMotorDriver(ClsPolicyDriverInfo objOldDriverDetailsInfo,ClsPolicyDriverInfo objDriverDetailsInfo)
		{
			return UpdatePolicyMotorDriver(objOldDriverDetailsInfo,objDriverDetailsInfo,"","");
			/*string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
				objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
				objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);

				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);

				objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);
			
				//objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			
				if (objDriverDetailsInfo.DATE_LICENSED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_LICENSED",objDriverDetailsInfo.DATE_LICENSED);
				else
					objDataWrapper.AddParameter("@DATE_LICENSED",null);

				objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
				//objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
				if(objDriverDetailsInfo.DRIVER_INCOME==0)
				{
					objDataWrapper.AddParameter("@DRIVER_INCOME",null);
				}
				else

					objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);
				//objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
				//objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);
				//objDataWrapper.AddParameter("@DRIVER_FAX",objDriverDetailsInfo.DRIVER_FAX);
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@MODIFIED_BY",objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDriverDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@MATURE_DRIVER",objDriverDetailsInfo.MATURE_DRIVER);
				objDataWrapper.AddParameter("@MATURE_DRIVER_DISCOUNT",objDriverDetailsInfo.MATURE_DRIVER_DISCOUNT);
				objDataWrapper.AddParameter("@PREFERRED_RISK_DISCOUNT",objDriverDetailsInfo.PREFERRED_RISK_DISCOUNT);
				objDataWrapper.AddParameter("@PREFERRED_RISK",objDriverDetailsInfo.PREFERRED_RISK);
				objDataWrapper.AddParameter("@TRANSFEREXP_RENEWAL_DISCOUNT",objDriverDetailsInfo.TRANSFEREXP_RENEWAL_DISCOUNT);
				objDataWrapper.AddParameter("@TRANSFEREXPERIENCE_RENEWALCREDIT",objDriverDetailsInfo.TRANSFEREXPERIENCE_RENEWALCREDIT);
				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID); 
				objDataWrapper.AddParameter("@CYCL_WITH_YOU",objDriverDetailsInfo.CYCL_WITH_YOU); 
				objDataWrapper.AddParameter("@COLL_STUD_AWAY_HOME",objDriverDetailsInfo.COLL_STUD_AWAY_HOME); 
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}

				if(TransactionLogRequired) 
				{

					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddMotorDriverDetails.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID 		=	objDriverDetailsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objDriverDetailsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Policy MotorCycle driver is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyMotorDriver", objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyMotorDriver");
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
			}*/
		}

		public int UpdatePolicyMotorDriver(ClsPolicyDriverInfo objOldDriverDetailsInfo,ClsPolicyDriverInfo objDriverDetailsInfo,string strCalledFrom, string strCustomInfo)
		{
			return 	UpdatePolicyMotorDriver(objOldDriverDetailsInfo,objDriverDetailsInfo,strCalledFrom,strCustomInfo,"");
		}
		public int UpdatePolicyMotorDriver(ClsPolicyDriverInfo objOldDriverDetailsInfo,ClsPolicyDriverInfo objDriverDetailsInfo,string strCalledFrom, string strCustomInfo,string strAssignXml)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDriverDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDriverDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objDriverDetailsInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objDriverDetailsInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objDriverDetailsInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objDriverDetailsInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objDriverDetailsInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objDriverDetailsInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objDriverDetailsInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objDriverDetailsInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objDriverDetailsInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objDriverDetailsInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objDriverDetailsInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objDriverDetailsInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",objDriverDetailsInfo.DRIVER_HOME_PHONE);
				objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",objDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
				objDataWrapper.AddParameter("@DRIVER_EXT",objDriverDetailsInfo.DRIVER_EXT);
				objDataWrapper.AddParameter("@DRIVER_MOBILE",objDriverDetailsInfo.DRIVER_MOBILE);

				if (objDriverDetailsInfo.DRIVER_DOB.Ticks != 0)
					objDataWrapper.AddParameter("@DRIVER_DOB",objDriverDetailsInfo.DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);

				objDataWrapper.AddParameter("@DRIVER_SSN",objDriverDetailsInfo.DRIVER_SSN);
				objDataWrapper.AddParameter("@DRIVER_MART_STAT",objDriverDetailsInfo.DRIVER_MART_STAT);
				objDataWrapper.AddParameter("@DRIVER_SEX",objDriverDetailsInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objDriverDetailsInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objDriverDetailsInfo.DRIVER_LIC_STATE);
			
				objDataWrapper.AddParameter("@DRIVER_LIC_CLASS",objDriverDetailsInfo.DRIVER_LIC_CLASS);
			
				if (objDriverDetailsInfo.DATE_LICENSED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_LICENSED",objDriverDetailsInfo.DATE_LICENSED);
				else
					objDataWrapper.AddParameter("@DATE_LICENSED",null);

				objDataWrapper.AddParameter("@DRIVER_REL",objDriverDetailsInfo.DRIVER_REL);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objDriverDetailsInfo.DRIVER_DRIV_TYPE);
				objDataWrapper.AddParameter("@DRIVER_OCC_CODE",objDriverDetailsInfo.DRIVER_OCC_CODE);
				objDataWrapper.AddParameter("@DRIVER_OCC_CLASS",objDriverDetailsInfo.DRIVER_OCC_CLASS);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_NAME",objDriverDetailsInfo.DRIVER_DRIVERLOYER_NAME);
				objDataWrapper.AddParameter("@DRIVER_DRIVERLOYER_ADD",objDriverDetailsInfo.DRIVER_DRIVERLOYER_ADD);
				if(objDriverDetailsInfo.DRIVER_INCOME==0)
				{
					objDataWrapper.AddParameter("@DRIVER_INCOME",null);
				}
				else

					objDataWrapper.AddParameter("@DRIVER_INCOME",objDriverDetailsInfo.DRIVER_INCOME);
				objDataWrapper.AddParameter("@DRIVER_BROADEND_NOFAULT",objDriverDetailsInfo.DRIVER_BROADEND_NOFAULT);
				objDataWrapper.AddParameter("@DRIVER_PHYS_MED_IMPAIRE",objDriverDetailsInfo.DRIVER_PHYS_MED_IMPAIRE);
				objDataWrapper.AddParameter("@DRIVER_DRINK_VIOLATION",objDriverDetailsInfo.DRIVER_DRINK_VIOLATION);
				objDataWrapper.AddParameter("@DRIVER_PREF_RISK",objDriverDetailsInfo.DRIVER_PREF_RISK);
				objDataWrapper.AddParameter("@DRIVER_GOOD_STUDENT",objDriverDetailsInfo.DRIVER_GOOD_STUDENT);
				objDataWrapper.AddParameter("@DRIVER_STUD_DIST_OVER_HUNDRED",objDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED);
				objDataWrapper.AddParameter("@DRIVER_LIC_SUSPENDED",objDriverDetailsInfo.DRIVER_LIC_SUSPENDED);
				objDataWrapper.AddParameter("@DRIVER_VOLUNTEER_POLICE_FIRE",objDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE);
				objDataWrapper.AddParameter("@DRIVER_US_CITIZEN",objDriverDetailsInfo.DRIVER_US_CITIZEN);
				objDataWrapper.AddParameter("@SAFE_DRIVER_RENEWAL_DISCOUNT",objDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT);
				objDataWrapper.AddParameter("@DRIVER_FAX",objDriverDetailsInfo.DRIVER_FAX);
				objDataWrapper.AddParameter("@RELATIONSHIP",objDriverDetailsInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@MODIFIED_BY",objDriverDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDriverDetailsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@MATURE_DRIVER",objDriverDetailsInfo.MATURE_DRIVER);
				objDataWrapper.AddParameter("@MATURE_DRIVER_DISCOUNT",objDriverDetailsInfo.MATURE_DRIVER_DISCOUNT);
				objDataWrapper.AddParameter("@PREFERRED_RISK_DISCOUNT",objDriverDetailsInfo.PREFERRED_RISK_DISCOUNT);
				objDataWrapper.AddParameter("@PREFERRED_RISK",objDriverDetailsInfo.PREFERRED_RISK);

				/* Commented by Charles on 2-Jul-09 for Itrack 6012
				objDataWrapper.AddParameter("@TRANSFEREXP_RENEWAL_DISCOUNT",objDriverDetailsInfo.TRANSFEREXP_RENEWAL_DISCOUNT);
				objDataWrapper.AddParameter("@TRANSFEREXPERIENCE_RENEWALCREDIT",objDriverDetailsInfo.TRANSFEREXPERIENCE_RENEWALCREDIT);
				*/
				objDataWrapper.AddParameter("@VEHICLE_ID",objDriverDetailsInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objDriverDetailsInfo.PERCENT_DRIVEN); 
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom); 
				objDataWrapper.AddParameter("@NO_CYCLE_ENDMT",objDriverDetailsInfo.NO_CYCLE_ENDMT); 
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID); 
				objDataWrapper.AddParameter("@CYCL_WITH_YOU",objDriverDetailsInfo.CYCL_WITH_YOU); 
				objDataWrapper.AddParameter("@COLL_STUD_AWAY_HOME",objDriverDetailsInfo.COLL_STUD_AWAY_HOME); 
				objDataWrapper.AddParameter("@VIOLATIONS",objDriverDetailsInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objDriverDetailsInfo.MVR_ORDERED);
				
				if(objDriverDetailsInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objDriverDetailsInfo.DATE_ORDERED);
				}

				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objDriverDetailsInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objDriverDetailsInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objDriverDetailsInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objDriverDetailsInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objDriverDetailsInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objDriverDetailsInfo.MVR_STATUS);
				if(objDriverDetailsInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objDriverDetailsInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objDriverDetailsInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objDriverDetailsInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

//				if(TransactionLogRequired) 
//				{
//
//					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddMotorDriverDetails.aspx.resx");
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);
//					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
//						returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyMotorDriver");
//					else
//					{
//						objTransactionInfo.TRANS_TYPE_ID	=	3;
//						objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
//						objTransactionInfo.POLICY_ID 		=	objDriverDetailsInfo.POLICY_ID;
//						objTransactionInfo.POLICY_VER_TRACKING_ID	=	objDriverDetailsInfo.POLICY_VERSION_ID;
//						objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
//						objTransactionInfo.TRANS_DESC		=	"Policy Motorcycle driver is modified";
//						objTransactionInfo.CHANGE_XML		=	strTranXML;
//						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
//						returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyMotorDriver", objTransactionInfo);
//					}
//
//				}
//				else
//				{
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyMotorDriver");
//				}
				objDataWrapper.ClearParameteres();
				DataTable dtVehicle =new DataTable();
				dtVehicle=ClsVehicleInformation.GetPolVehicle(objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,objDriverDetailsInfo.DRIVER_ID);
				string sbStrXml="";
				AddPolAssignedVehicles(objDataWrapper,strAssignXml,objDriverDetailsInfo,strCalledFrom,dtVehicle,ref sbStrXml);
				//sbStrXml = sbStrXml.Remove("<LabelFieldMapping>");
				//int returnResult = 0;
				if(TransactionLogRequired)
				{
                    objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MotorCycle/PolicyAddMotorDriver.aspx.resx");
					//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					strTranXML = objBuilder.GetTransactionLogXML(objOldDriverDetailsInfo, objDriverDetailsInfo);
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_ID']");
					strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDriverDetailsInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID 		=	objDriverDetailsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objDriverDetailsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objDriverDetailsInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1586", "");// "Policy Motorcycle driver is modified";
					//strTranXML = strTranXML.Remove("</LabelFieldMapping>","");
					//strTranXML.Append(sbStrXml);
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='MVR_ORDERED' and @NewValue='0']","NewValue","null");
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
						
				objDataWrapper.ClearParameteres();
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages("MOTOR");
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper ,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID,RuleType.AutoDriverDep);
				objDataWrapper.ClearParameteres();
				UpdateMotorVehicleClassPOL(objDataWrapper,objDriverDetailsInfo.CUSTOMER_ID,objDriverDetailsInfo.POLICY_ID,objDriverDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
		/// Set Vehicle Class while Updating Driver for MOTOR
		/// </summary>
		/// <param name="objDataWrapper"></param>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="DriverID"></param>
		/// <param name="VehicleID"></param>
		/// <param name="DRIVER_DOB"></param>
		/// <returns></returns>
		public int SetMotorVehicleClassRuleForPolicy(DataWrapper objDataWrapper ,int CustomerID,int PolicyID,int PolicyVersionID,int DriverID,int VehicleID,DateTime DRIVER_DOB )
		{
			int	returnResult=0;
			try
			{
				if (objDataWrapper==null)
					objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.ClearParameteres(); 
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",DriverID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID);
				if (DRIVER_DOB!=DateTime.MinValue) 
					objDataWrapper.AddParameter("@DRIVER_DOB",DRIVER_DOB);
				else
					objDataWrapper.AddParameter("@DRIVER_DOB",null);
				returnResult = objDataWrapper.ExecuteNonQuery("Proc_SetMotorVehicleClassRuleForPolicy");
				objDataWrapper.ClearParameteres(); 
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		/// <summary>																														/// <summary>
		/// This function will activate or deactivate the policy motorcycle driver.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="driverID"></param>
		/// <param name="status"></param>
		public int ActivateDeactivatePolicyMotorDriver(int customerID, int policyID, int policyVersionID, int driverID, int modifiedBy, string status, string strCustomInfo)
		{
			int intRetVal;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			string strStoredProc = "Proc_ActivateDeactivatePolicyMotorDriver";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@POLICY_ID",policyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",driverID);
				objDataWrapper.AddParameter("@IS_ACTIVE",status);
				objDataWrapper.AddParameter("@CALLEDFROM","MOT");
				if(TransactionLogRequired) 
				{				
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	modifiedBy;
					objTransactionInfo.POLICY_ID 		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	policyVersionID;
					objTransactionInfo.CLIENT_ID		=	customerID;
					if(status.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1587","");//"Policy Motorcycle driver is activated.";
					else
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1588", "");// "Policy Motorcycle driver is deactivated.";
					
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					intRetVal = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);				
				}
				else
					intRetVal = objDataWrapper.ExecuteNonQuery(strStoredProc);
				objDataWrapper.ClearParameteres();
				UpdateMotorVehicleClassPOL(objDataWrapper,customerID,policyID,policyVersionID);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
			if(intRetVal >0)
			{
				return 1;
			}
			else
			{
				return -1;
			}

		}
		public int ActivateDeactivatePolicyWatercraftOperator(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo, string strStatus,string strCustomInfo) //Changed return type from void by Charles on 22-Oct-09 for Itrack 6603
		{
			string		strStoredProc	=	"Proc_ActivateDeactivatePolicyWatercraftOperator";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int returnResult = 0;//Moved here by Charles on 22-Oct-09 for Itrack 6603
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftOperatorInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftOperatorInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				
				if(TransactionLogRequired)
				{
                    objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/aspx/Watercraft/PolicyAddWatercraftOperator.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID			=	objWatercraftOperatorInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objWatercraftOperatorInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
					//Added FOR Itrack Issue #5479
					string strDriverName = objWatercraftOperatorInfo.DRIVER_FNAME + " " +  objWatercraftOperatorInfo.DRIVER_MNAME + " " + objWatercraftOperatorInfo.DRIVER_LNAME;  
					string strDriverCode = objWatercraftOperatorInfo.DRIVER_CODE;  
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1589","");//"Operator is Activated";
					else
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1590", "");// "Operator is Deactivated";
					//objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
			}
			catch(Exception ex)
			{
				throw(ex);				
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}	
			if(returnResult>0)//Added by Charles on 22-Oct-09 for Itrack 6603
			{
				return returnResult;
			}
			else
			{
				return -1;
			}//Added till here			
		}
		#endregion


		#region Policy WaterCraft Functions
		/// <summary>
		/// This function will add the policy watercraft operator.
		/// </summary>
		/// <param name="objWatercraftOperatorInfo"></param>
		/// <returns></returns>
		public int AddPolicyWatercraftOperator(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyWatercraftOperatorInfo";
			DateTime	RecordDate		=	DateTime.Now;
				 
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftOperatorInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftOperatorInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objWatercraftOperatorInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objWatercraftOperatorInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objWatercraftOperatorInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objWatercraftOperatorInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objWatercraftOperatorInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objWatercraftOperatorInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objWatercraftOperatorInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objWatercraftOperatorInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objWatercraftOperatorInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objWatercraftOperatorInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objWatercraftOperatorInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objWatercraftOperatorInfo.DRIVER_DRIV_TYPE);

				if(objWatercraftOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DRIVER_DOB",objWatercraftOperatorInfo.DRIVER_DOB);
				}
				
				
				objDataWrapper.AddParameter("@DRIVER_SSN",objWatercraftOperatorInfo.DRIVER_SSN);
				
				objDataWrapper.AddParameter("@DRIVER_SEX",objWatercraftOperatorInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objWatercraftOperatorInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objWatercraftOperatorInfo.DRIVER_LIC_STATE);
				
				if(objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX);
				}
				else
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
				}
				
				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objWatercraftOperatorInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objWatercraftOperatorInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objWatercraftOperatorInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objWatercraftOperatorInfo.MVR_DRIV_LIC_APPL);
				
				objDataWrapper.AddParameter("@MVR_REMARKS",objWatercraftOperatorInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objWatercraftOperatorInfo.MVR_STATUS);

				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftOperatorInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftOperatorInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objWatercraftOperatorInfo.EXPERIENCE_CREDIT );
				objDataWrapper.AddParameter("@VEHICLE_ID",objWatercraftOperatorInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objWatercraftOperatorInfo.PERCENT_DRIVEN);
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
				objDataWrapper.AddParameter("@WAT_SAFETY_COURSE",objWatercraftOperatorInfo.WAT_SAFETY_COURSE);
				objDataWrapper.AddParameter("@CERT_COAST_GUARD",objWatercraftOperatorInfo.CERT_COAST_GUARD);
				objDataWrapper.AddParameter("@APP_REC_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_REC_VEHICLE_PRIN_OCC_ID);
				objDataWrapper.AddParameter("@REC_VEH_ID",objWatercraftOperatorInfo.REC_VEH_ID);
				objDataWrapper.AddParameter("@VIOLATIONS",objWatercraftOperatorInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objWatercraftOperatorInfo.MVR_ORDERED);
				objDataWrapper.AddParameter("@MARITAL_STATUS",objWatercraftOperatorInfo.MARITAL_STATUS);
				
				if(objWatercraftOperatorInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objWatercraftOperatorInfo.DATE_ORDERED);
				}
				 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID,SqlDbType.Int,ParameterDirection.Output);

				//Change Trans log : 13 sep 2007 (Assigned Boats)
				int returnResult = 0;
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);

				int DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				if(DRIVER_ID!=-1)
				{
					objWatercraftOperatorInfo.DRIVER_ID = DRIVER_ID;
					string sbStrXml="";
					string calledfrom="";
					string sbStrRecXml="";//Done for Itrack Issue 6737 on 17 Nov 09

					DataTable dtVehicle =new DataTable();
					DataTable dtRecVehicle =new DataTable();//Done for Itrack Issue 6737 on 17 Nov 09

					dtVehicle=clsWatercraftInformation.GetPolBoat(objWatercraftOperatorInfo.CUSTOMER_ID,objWatercraftOperatorInfo.POLICY_ID,objWatercraftOperatorInfo.POLICY_VERSION_ID,objWatercraftOperatorInfo.DRIVER_ID);
					//Done for Itrack Issue 6737 on 17 Nov 09
					dtRecVehicle=clsWatercraftInformation.GetPolReacreationalVehicles(objWatercraftOperatorInfo.CUSTOMER_ID,objWatercraftOperatorInfo.POLICY_ID,objWatercraftOperatorInfo.POLICY_VERSION_ID,objWatercraftOperatorInfo.DRIVER_ID);

					AddPolAssignedBoats(objDataWrapper,objWatercraftOperatorInfo,calledfrom,dtVehicle,ref sbStrXml);
					//Done for Itrack Issue 6737 on 17 Nov 09
					if(objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE != null && objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE !="")
					  AddPolAssignedReacreationalVehicles(objDataWrapper,objWatercraftOperatorInfo,calledfrom,dtRecVehicle,ref sbStrRecXml);
					
					if(TransactionLogRequired)
					{

						objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftOperator.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftOperatorInfo);
						//Done for Itrack Issue 6737 on 17 Nov 09
						if(sbStrRecXml != "")
						  strTranXML= "<root>" + strTranXML + sbStrXml + sbStrRecXml + "</root>";
						else
						  strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.CREATED_BY;
						objTransactionInfo.POLICY_ID 		=	objWatercraftOperatorInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID =	objWatercraftOperatorInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1591", "");// "New watercraft operator information is added";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//Executing the query
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
						

					}
				}

				#region Commented on 13 sep 2007
				/*int re turnResult = 0;
				if(TransactionLogRequired)
				{
					objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftOperator.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftOperatorInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.CREATED_BY;
					objTransactionInfo.POLICY_ID 		=	objWatercraftOperatorInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objWatercraftOperatorInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New watercraft operator information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				if (returnResult > 0)
				{
					objWatercraftOperatorInfo.DRIVER_ID = int.Parse(objSqlParameter.Value.ToString());
				}*/
				#endregion
				
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
		/// This function will update the policy watercraft operator.
		/// </summary>
		/// <param name="objOldWatercraftOperatorInfo"></param>
		/// <param name="objWatercraftOperatorInfo"></param>
		/// <returns></returns>
		public int UpdatePolicyWatercraftOperator(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo objOldWatercraftOperatorInfo,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo)
		{
			//update watercraft 13 sep 2007
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdatePolicyWatercraftOperatorInfo";

			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftOperatorInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftOperatorInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objWatercraftOperatorInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objWatercraftOperatorInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objWatercraftOperatorInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objWatercraftOperatorInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objWatercraftOperatorInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objWatercraftOperatorInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objWatercraftOperatorInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objWatercraftOperatorInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objWatercraftOperatorInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objWatercraftOperatorInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objWatercraftOperatorInfo.DRIVER_COUNTRY);
				
				if(objWatercraftOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DRIVER_DOB",objWatercraftOperatorInfo.DRIVER_DOB);
				}
				objDataWrapper.AddParameter("@DRIVER_SSN",objWatercraftOperatorInfo.DRIVER_SSN);
				
				objDataWrapper.AddParameter("@DRIVER_SEX",objWatercraftOperatorInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objWatercraftOperatorInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objWatercraftOperatorInfo.DRIVER_LIC_STATE);
				if(objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX);
				}
				else
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
				}
				
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftOperatorInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftOperatorInfo.LAST_UPDATED_DATETIME);

				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objWatercraftOperatorInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@VEHICLE_ID",objWatercraftOperatorInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objWatercraftOperatorInfo.PERCENT_DRIVEN);
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
				objDataWrapper.AddParameter("@WAT_SAFETY_COURSE",objWatercraftOperatorInfo.WAT_SAFETY_COURSE);
				objDataWrapper.AddParameter("@CERT_COAST_GUARD",objWatercraftOperatorInfo.CERT_COAST_GUARD);
				objDataWrapper.AddParameter("@APP_REC_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_REC_VEHICLE_PRIN_OCC_ID);
				objDataWrapper.AddParameter("@REC_VEH_ID",objWatercraftOperatorInfo.REC_VEH_ID);
				objDataWrapper.AddParameter("@VIOLATIONS",objWatercraftOperatorInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objWatercraftOperatorInfo.MVR_ORDERED);
				objDataWrapper.AddParameter("@MARITAL_STATUS",objWatercraftOperatorInfo.MARITAL_STATUS);
				
				if(objWatercraftOperatorInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objWatercraftOperatorInfo.DATE_ORDERED);
				}

				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objWatercraftOperatorInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objWatercraftOperatorInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objWatercraftOperatorInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objWatercraftOperatorInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objWatercraftOperatorInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objWatercraftOperatorInfo.MVR_STATUS);

				if(TransactionLogRequired) 
				{
					objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftOperator.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldWatercraftOperatorInfo,objWatercraftOperatorInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID 		=	objWatercraftOperatorInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objWatercraftOperatorInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Watercraft operator information is modified";
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
		#region Update WaterCraft Operator Method Overload (Extra Parameter for TransXml) :Modifed 13 Sep 2007
		public int UpdatePolicyWatercraftOperator(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo objOldWatercraftOperatorInfo,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo,string strAssignXml,string strAssignRecXml)//Done for Itrack Issue 6737 on 17 Nov 09
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdatePolicyWatercraftOperatorInfo";

			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftOperatorInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftOperatorInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@DRIVER_FNAME",objWatercraftOperatorInfo.DRIVER_FNAME);
				objDataWrapper.AddParameter("@DRIVER_MNAME",objWatercraftOperatorInfo.DRIVER_MNAME);
				objDataWrapper.AddParameter("@DRIVER_LNAME",objWatercraftOperatorInfo.DRIVER_LNAME);
				objDataWrapper.AddParameter("@DRIVER_CODE",objWatercraftOperatorInfo.DRIVER_CODE);
				objDataWrapper.AddParameter("@DRIVER_SUFFIX",objWatercraftOperatorInfo.DRIVER_SUFFIX);
				objDataWrapper.AddParameter("@DRIVER_ADD1",objWatercraftOperatorInfo.DRIVER_ADD1);
				objDataWrapper.AddParameter("@DRIVER_ADD2",objWatercraftOperatorInfo.DRIVER_ADD2);
				objDataWrapper.AddParameter("@DRIVER_CITY",objWatercraftOperatorInfo.DRIVER_CITY);
				objDataWrapper.AddParameter("@DRIVER_STATE",objWatercraftOperatorInfo.DRIVER_STATE);
				objDataWrapper.AddParameter("@DRIVER_ZIP",objWatercraftOperatorInfo.DRIVER_ZIP);
				objDataWrapper.AddParameter("@DRIVER_COUNTRY",objWatercraftOperatorInfo.DRIVER_COUNTRY);
				objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",objWatercraftOperatorInfo.DRIVER_DRIV_TYPE);

				if(objWatercraftOperatorInfo.DRIVER_DOB!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DRIVER_DOB",objWatercraftOperatorInfo.DRIVER_DOB);
				}
				objDataWrapper.AddParameter("@DRIVER_SSN",objWatercraftOperatorInfo.DRIVER_SSN);
				
				objDataWrapper.AddParameter("@DRIVER_SEX",objWatercraftOperatorInfo.DRIVER_SEX);
				objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",objWatercraftOperatorInfo.DRIVER_DRIV_LIC);
				objDataWrapper.AddParameter("@DRIVER_LIC_STATE",objWatercraftOperatorInfo.DRIVER_LIC_STATE);
				if(objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX != 0)
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX);
				}
				else
				{
					objDataWrapper.AddParameter("@DRIVER_COST_GAURAD_AUX",null);
				}
				
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftOperatorInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftOperatorInfo.LAST_UPDATED_DATETIME);

				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objWatercraftOperatorInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@VEHICLE_ID",objWatercraftOperatorInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@PERCENT_DRIVEN",objWatercraftOperatorInfo.PERCENT_DRIVEN);
				objDataWrapper.AddParameter("@APP_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID);
				objDataWrapper.AddParameter("@WAT_SAFETY_COURSE",objWatercraftOperatorInfo.WAT_SAFETY_COURSE);
				objDataWrapper.AddParameter("@CERT_COAST_GUARD",objWatercraftOperatorInfo.CERT_COAST_GUARD);
				objDataWrapper.AddParameter("@APP_REC_VEHICLE_PRIN_OCC_ID",objWatercraftOperatorInfo.APP_REC_VEHICLE_PRIN_OCC_ID);
				objDataWrapper.AddParameter("@REC_VEH_ID",objWatercraftOperatorInfo.REC_VEH_ID);
				objDataWrapper.AddParameter("@VIOLATIONS",objWatercraftOperatorInfo.VIOLATIONS);
				objDataWrapper.AddParameter("@MVR_ORDERED",objWatercraftOperatorInfo.MVR_ORDERED);
				objDataWrapper.AddParameter("@MARITAL_STATUS",objWatercraftOperatorInfo.MARITAL_STATUS);
				
				if(objWatercraftOperatorInfo.DATE_ORDERED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_ORDERED",objWatercraftOperatorInfo.DATE_ORDERED);
				}

				//Added by Mohit Agarwal 29-Jun-07 ITrack 2030
				objDataWrapper.AddParameter("@MVR_CLASS",objWatercraftOperatorInfo.MVR_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_CLASS",objWatercraftOperatorInfo.MVR_LIC_CLASS);
				objDataWrapper.AddParameter("@MVR_LIC_RESTR",objWatercraftOperatorInfo.MVR_LIC_RESTR);
				objDataWrapper.AddParameter("@MVR_DRIV_LIC_APPL",objWatercraftOperatorInfo.MVR_DRIV_LIC_APPL);

				objDataWrapper.AddParameter("@MVR_REMARKS",objWatercraftOperatorInfo.MVR_REMARKS);
				objDataWrapper.AddParameter("@MVR_STATUS",objWatercraftOperatorInfo.MVR_STATUS);

				//Added 13 sep 2007
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				objDataWrapper.ClearParameteres();

				DataTable dtVehicle =new DataTable();
				DataTable dtRecVehicle =new DataTable();//Done for Itrack Issue 6737 on 17 Nov 09
				dtVehicle=clsWatercraftInformation.GetPolBoat(objWatercraftOperatorInfo.CUSTOMER_ID,objWatercraftOperatorInfo.POLICY_ID,objWatercraftOperatorInfo.POLICY_VERSION_ID,objWatercraftOperatorInfo.DRIVER_ID);
				//Done for Itrack Issue 6737 on 17 Nov 09
				dtRecVehicle=clsWatercraftInformation.GetPolReacreationalVehicles(objWatercraftOperatorInfo.CUSTOMER_ID,objWatercraftOperatorInfo.POLICY_ID,objWatercraftOperatorInfo.POLICY_VERSION_ID,objWatercraftOperatorInfo.DRIVER_ID);
				string sbStrXml="",strCalledFrom="",sbStrRecXml="";//Done for Itrack Issue 6737 on 17 Nov 09
				AddPolAssignedBoats(objDataWrapper,strAssignXml,objWatercraftOperatorInfo,strCalledFrom,dtVehicle,ref sbStrXml);
				//Done for Itrack Issue 6737 on 17 Nov 09
				if(objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE != null && objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE != "")
				  AddPolAssignedReacreationalVehicles(objDataWrapper,strAssignRecXml,objWatercraftOperatorInfo,strCalledFrom,dtRecVehicle,ref sbStrRecXml);
				
				if(TransactionLogRequired) 
				{
					objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftOperator.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldWatercraftOperatorInfo,objWatercraftOperatorInfo);
					//Done for Itrack Issue 6737 on 17 Nov 09
					if(sbStrRecXml != "")
					  strTranXML= "<root>" + strTranXML + sbStrXml + sbStrRecXml + "</root>";
					else
					  strTranXML= "<root>" + strTranXML + sbStrXml + "</root>";
					  
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID 		=	objWatercraftOperatorInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objWatercraftOperatorInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Watercraft operator information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);

				}
				
				/*if(TransactionLogRequired) 
				{
					objWatercraftOperatorInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftOperator.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldWatercraftOperatorInfo,objWatercraftOperatorInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID 		=	objWatercraftOperatorInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objWatercraftOperatorInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Watercraft operator information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}*/
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
		/// This function will delete the policy watercraft operator.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="DriverID"></param>
		/// <returns></returns>
		public int DeletePolicyWaterCraftOperator(int CustomerID,int PolicyID,int PolicyVersionID,int DriverID)
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",DriverID);
				
				returnResult = objDataWrapper.ExecuteNonQuery("Proc_DeletePolicyWaterCraftOperator");
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
	
			}
			catch(Exception exc)
			{
				throw(exc);
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

		public int DeletePolicyWaterCraftOperator(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_DeletePolicyWaterCraftOperator";			
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftOperatorInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftOperatorInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftOperatorInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objWatercraftOperatorInfo.DRIVER_ID);
				
				if(TransactionLogRequired)
				{	
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID			=	objWatercraftOperatorInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objWatercraftOperatorInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftOperatorInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftOperatorInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1592", "");// "Operator is Deleted";
					//Added For Itrack Issue #5479.
					string strDriverName				= objWatercraftOperatorInfo.DRIVER_FNAME + " " +  objWatercraftOperatorInfo.DRIVER_MNAME + " " + objWatercraftOperatorInfo.DRIVER_LNAME;  
					string strDriverCode				= objWatercraftOperatorInfo.DRIVER_CODE;					
					//objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
	
			}
			catch(Exception exc)
			{
				throw(exc);
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
		/// Updates the driver specific endorsements for the application
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersion"></param>
		/// <param name="objWrapper"></param>
		/// <returns></returns>
		public int UpdateDriverEndorsements(int customerID, int appID, int appVersion, DataWrapper objWrapper)
		{
			if ( objWrapper.CommandParameters.Length > 0 )
			{
				objWrapper.ClearParameteres();
			}

			//The Sp
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersion);
			
			objWrapper.ExecuteNonQuery("Proc_UPDATE_DRIVER_ENDORSEMENTS");
			
			return 1;

		}	
			

		/// <summary>
		/// Updates the driver specific endorsements for the application
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersion"></param>
		/// <param name="objWrapper"></param>
		/// <returns></returns>
		public int UpdateDriverEndorsements(int customerID, int appID, int appVersion, int vehicleID, DataWrapper objWrapper)
		{
			if ( objWrapper.CommandParameters.Length > 0 )
			{
				objWrapper.ClearParameteres();
			}

			//The Sp
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersion);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);

			objWrapper.ExecuteNonQuery("Proc_UPDATE_DRIVER_ENDORSEMENTS_VEHICLE");
			
			return 1;

		}	
			

		/// <summary>
		/// Updates the driver specific endorsements for the policy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersion"></param>
		/// <param name="objWrapper"></param>
		/// <returns></returns>
		public int UpdatePolicyDriverEndorsements(int customerID, int policyID, int policyVersion, DataWrapper objWrapper)
		{
			objWrapper.ClearParameteres();

			//The Sp
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersion);
			
			objWrapper.ExecuteNonQuery("Proc_UPDATE_DRIVER_ENDORSEMENTS_POLICY");
			
			return 1;

		}	
		// Added by Swastika  on 28th Mar'06 for SDLC>Buttons Functionality # 21
		public int UpdatePolicyDriverEndorsements(int customerID, int policyID, int policyVersion, int vehicleID, DataWrapper objWrapper)
		{
			if ( objWrapper.CommandParameters.Length > 0 )
			{
				objWrapper.ClearParameteres();
			}

			//The Sp
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersion);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);

			objWrapper.ExecuteNonQuery("Proc_UPDATE_DRIVER_ENDORSEMENTS_VEHICLE_POLICY");
			
			return 1;

		}	

		//Added by Swastika on 27th Mar'06 for Pol Iss # 52

		public  void InsertPolicyApplicantsToDriver(DataTable dtSelectedDriver,int Customer_ID,int Policy_ID,int Policy_Version_ID,int User_Id, string strCalledFrom, string strCalledFor)
		{			
			string	strStoredProc = "Proc_InsertPolicyApplicantToDriver";			
			string DriverInfo="", DriverCode="", TransDesc="";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];										
					objDataWrapper.AddParameter("@CUSTOMER_ID",Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@POLICY_ID",Policy_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",Policy_Version_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@APPLICANT_ID",int.Parse(dr["ApplicantID"].ToString()),SqlDbType.Int);					
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",User_Id,SqlDbType.Int);		
					objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom,SqlDbType.VarChar);		
					objDataWrapper.AddParameter("@CALLED_FOR",strCalledFor,SqlDbType.VarChar);							
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DRIVER_CODE",strCalledFor,SqlDbType.VarChar,ParameterDirection.Output,30);					
					
												
					objDataWrapper.ExecuteNonQuery(strStoredProc);
					objDataWrapper.ClearParameteres();
					DriverCode = objSqlParameter.Value.ToString();
					if(strCalledFrom.ToUpper()=="PPA" || strCalledFrom.ToUpper()=="MOT" || (strCalledFrom.ToUpper()=="UMB" && strCalledFor.ToUpper()!="WAT"))
					{
						DriverInfo +=";Driver Name = " + dr["ApplicantName"].ToString() + ", Driver Code = " + DriverCode;
						TransDesc="Driver is copied";
					}
					else if(strCalledFrom.ToUpper()=="WAT" || strCalledFrom.ToUpper()=="HOM"|| strCalledFrom.ToUpper()=="HOME" || (strCalledFrom.ToUpper()=="UMB" && strCalledFor.ToUpper()=="WAT"))
					{
						DriverInfo +=";Operator Name = " + dr["ApplicantName"].ToString() + ", Operator Code = " + DriverCode;
						TransDesc="Operator is copied";
					}
					//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);									
				}	
				if(TransactionLogRequired)// && i==(dtSelectedDriver.Rows.Count-1))
				{
					ClsDriverDetailsInfo objDriverDetailsInfo=new ClsDriverDetailsInfo();
					objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyCopyApplicantDriver.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID			=	1;
					objTransactionInfo.POLICY_ID				=	Policy_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	Policy_Version_ID;
					objTransactionInfo.CLIENT_ID				=	Customer_ID;
					objTransactionInfo.RECORDED_BY				=	User_Id;
					objTransactionInfo.TRANS_DESC				=	TransDesc;
					objTransactionInfo.CUSTOM_INFO				=	DriverInfo;
					//objTransactionInfo.CHANGE_XML				=	strTranXML;
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
					
					
			}
			catch(Exception exc)
			{				
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (exc);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}	
	
		//End : Swastika 

		//Added by Swastika on 27th Mar'06 for Pol Iss # 31
			
		public static DataTable FetchPolicyExistingDriverForUmbrella(int intCustomerID,int intPolicyID,int intPolicyVersionID)
		{
	
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@POLICYID",intPolicyID);
			objDataWrapper.AddParameter("@POLICYVERSIONID",intPolicyVersionID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyExistingDriverForUmbrella");
			return dsTemp.Tables[0];
		}

		//Added by Swastika on 27th Mar'06 for Pol Iss # 31
		
		public static DataTable FetchPolicyExistingDriverForWatercraft(int intCustomerID,int intPolicyID,int intPolicyVersionID)
		{
	
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@POLICYID",intPolicyID);
			objDataWrapper.AddParameter("@POLICYVERSIONID",intPolicyVersionID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyExistingDriverForWatercraft");
			return dsTemp.Tables[0];
		}

		//Added by Swastika on 27th Mar'06 for Pol Iss # 31
		
		public static DataTable FetchPolicyExistingDriverFromCurrentApp(int intCustomerID,int intPolicyID,int intPolicyVersionID)
		{
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@POLICYID",intPolicyID);
			objDataWrapper.AddParameter("@POLICYVERSIONID",intPolicyVersionID);
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyExistingDriver");
			return dsTemp.Tables[0];
		}

		public static DataSet FetchPolicyUmbrellaBoatInfo(int customerId,int policyId, int policyVersionId)
		{
			string		strStoredProc	=	"Proc_FetchPolicyUmbrellaBoatInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionId,SqlDbType.Int);                                           
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

		public static DataTable GetDriverAssignedForVehicleApp(int CustomerID, int AppID, int AppVersionID)
		{
			return GetDriverAssignedForVehicleApp(CustomerID, AppID, AppVersionID,null);
		}
		public static DataTable GetDriverAssignedForVehicleApp(int CustomerID, int AppID, int AppVersionID,DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_GetAssignedDriversForVehicleForApp";			
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objWrapper.AddParameter("@APP_ID",AppID);
			objWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)
			{
				return ds.Tables[0];
			}
			else
				return null;
		}

		public static DataTable GetDriverAssignedForVehiclePol(int CustomerID, int PoID, int PolVersionID)
		{
			return GetDriverAssignedForVehiclePol(CustomerID,PoID,PolVersionID,null);
		}

		public static DataTable GetDriverAssignedForVehiclePol(int CustomerID, int PoID, int PolVersionID,DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_GetAssignedDriversForVehicleForPol";			
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objWrapper.AddParameter("@POLICY_ID",PoID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		public static DataTable GetDriverAssignedForVehicleAppMotor(int CustomerID, int AppID, int AppVersionID,DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_GetAssignedDriversForVehicleForApp_Motor";			
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objWrapper.AddParameter("@APP_ID",AppID);
			objWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)
			{
				return ds.Tables[0];
			}
			else
				return null;
		}

		public static DataTable GetDriverAssignedForVehiclePol_Motor(int CustomerID, int PoID, int PolVersionID,DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_GetAssignedDriversForVehicleForPol_Motor";			
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objWrapper.AddParameter("@POLICY_ID",PoID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		public void UpdateVehicleClassNew(int CustomerID, int AppID, int AppVersionID)
		{
			UpdateVehicleClassNew(CustomerID, AppID, AppVersionID,null);
		}
		public void UpdateVehicleClassNew(int CustomerID, int AppID, int AppVersionID,DataWrapper objDataWrapper)
		{
			string driverInputXml ="";
			string eligibleDriver ="";
			string strVehicleClassXml = "";
			string strDriverXml = "";
			int iEligibleDriverId = 0;
			int isecDriver=0;
			//int iSecDrvForClass = 0;
			//string strFchDrvClXml="";
			string strFchDrvClXmlForClass="";
			ClsQuickQuote objQuickQuote = new ClsQuickQuote();
			DataTable dtMvr;

			if (objDataWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			//Get the XML for customer,app,version
			ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();

			string strInputXML = objGeneralInfo.GetInputXML(CustomerID,AppID,AppVersionID,((int)enumLOB.AUTOP).ToString(),objDataWrapper);

			//Encode Xml Charecters
			strInputXML = ClsCommon.EncodeXMLCharacters(strInputXML); 
			strInputXML = ClsCommon.DecodeXMLCharacters(strInputXML); 
			strInputXML=strInputXML.ToUpper();
			strInputXML=strInputXML.Replace("&AMP;","&amp;");
			strInputXML=strInputXML.Replace("\n","");
			strInputXML=strInputXML.Replace("\r","");
			strInputXML=strInputXML.Replace("\t","");
			XmlDocument gxmlAutopQuickQuoteXml =new XmlDocument(); 
			gxmlAutopQuickQuoteXml.LoadXml(strInputXML);
			
		
			XmlNodeList vehNodesForClass = gxmlAutopQuickQuoteXml.SelectNodes("QUICKQUOTE/VEHICLES/VEHICLE");
			XmlNodeList drvNodesForDrvCount = gxmlAutopQuickQuoteXml.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER");
			// COUNT number of Vehicle and Driver 
			int cntVehicle=vehNodesForClass.Count;
			int cntDriver=drvNodesForDrvCount.Count;

			//Adding new drive node to an existing vehicle node	
			DataTable dtAssignedDriver = ClsDriverDetail.GetDriverAssignedForVehicleApp(CustomerID,AppID,AppVersionID,objDataWrapper);
			
			foreach(XmlNode vehNodeForClass in vehNodesForClass)
			{
					int VehicleID = int.Parse(vehNodeForClass.Attributes.Item(0).InnerXml.ToString().Trim());
					for(int i =0;i<dtAssignedDriver.Rows.Count;i++)
					{
						if(VehicleID == int.Parse(dtAssignedDriver.Rows[i]["VEHICLE_ID"].ToString()) && dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString() !="")
						{
							int idrivVioAccip=0, idrivAccip=0;
							XmlElement strDriverNode = gxmlAutopQuickQuoteXml.CreateElement("DRIVER");						
							XmlNode xNode = gxmlAutopQuickQuoteXml.SelectSingleNode("QUICKQUOTE/VEHICLES/VEHICLE[@ID=" + VehicleID.ToString() + "]");
							XmlAttribute xVehicleIDAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("VEHICLEASSIGNEDASOPERATOR");
							xVehicleIDAttribute.Value = dtAssignedDriver.Rows[i]["VEHICLE_ID"].ToString();
							strDriverNode.Attributes.Append(xVehicleIDAttribute);

							XmlAttribute xPRIN_OCC_Attribute = gxmlAutopQuickQuoteXml.CreateAttribute("VEHICLEDRIVEDAS");
							xPRIN_OCC_Attribute.Value = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString().Trim();
							strDriverNode.Attributes.Append(xPRIN_OCC_Attribute);	

							//Driver ID
							XmlAttribute xDriverIDAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("ID");
							xDriverIDAttribute.Value = dtAssignedDriver.Rows[i]["DRIVER_ID"].ToString().Trim();
							strDriverNode.Attributes.Append(xDriverIDAttribute);

							//Age of Driver
							XmlAttribute xDriverAgeAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("AGEOFDRIVER");
							xDriverAgeAttribute.Value = dtAssignedDriver.Rows[i]["DRIVER_AGE"].ToString().Trim();
							strDriverNode.Attributes.Append(xDriverAgeAttribute);

							//Birthdate
							XmlAttribute xDriverDOBAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("BIRTHDATE");
							xDriverDOBAttribute.Value = dtAssignedDriver.Rows[i]["BIRTHDATE"].ToString().Trim();
							strDriverNode.Attributes.Append(xDriverDOBAttribute);

							//Sex of Driver
							XmlAttribute xDriverGenderAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("GENDER");
							xDriverGenderAttribute.Value = dtAssignedDriver.Rows[i]["GENDER"].ToString().Trim();
							strDriverNode.Attributes.Append(xDriverGenderAttribute);

							//College Student
							XmlAttribute xDriverCollStdAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("COLLEGESTUDENT");
							xDriverCollStdAttribute.Value = dtAssignedDriver.Rows[i]["COLLEGESTUDENT"].ToString().Trim();
							strDriverNode.Attributes.Append(xDriverCollStdAttribute);
						
							//Have Car
							XmlAttribute xDriverHaveCarAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("HAVE_CAR");
							xDriverHaveCarAttribute.Value = dtAssignedDriver.Rows[i]["HAVE_CAR"].ToString().Trim();
							strDriverNode.Attributes.Append(xDriverHaveCarAttribute);
						
							// Driver drives Vehicle as
							XmlAttribute aSPRIN_OCC_Attribute = gxmlAutopQuickQuoteXml.CreateAttribute("VEHICLEDRIVEDAS");
							aSPRIN_OCC_Attribute.Value = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDASCODE"].ToString().Trim();
							strDriverNode.Attributes.Append(aSPRIN_OCC_Attribute);
						
							//Creating Node for birthdate also
							XmlElement strBirthDateNode = gxmlAutopQuickQuoteXml.CreateElement("BIRTHDATE");
							strBirthDateNode.InnerXml = dtAssignedDriver.Rows[i]["BIRTHDATE"].ToString().Trim();
							strDriverNode.AppendChild(strBirthDateNode);

							//Creating Node for AGE OF DRIVER also
							XmlElement strDriverAgeNode = gxmlAutopQuickQuoteXml.CreateElement("AGEOFDRIVER");
							strDriverAgeNode.InnerXml = dtAssignedDriver.Rows[i]["DRIVER_AGE"].ToString().Trim();
							strDriverNode.AppendChild(strDriverAgeNode);

							XmlElement xPRIN_OCC_Node = gxmlAutopQuickQuoteXml.CreateElement("VEHICLEDRIVEDAS");
							xPRIN_OCC_Node.InnerXml = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString().Trim();
							strDriverNode.AppendChild(xPRIN_OCC_Node);
							XmlElement strMVRNode = gxmlAutopQuickQuoteXml.CreateElement("SUMOFVIOLATIONPOINTS");
							XmlElement strAccNode = gxmlAutopQuickQuoteXml.CreateElement("SUMOFACCIDENTPOINTS");
                            if((aSPRIN_OCC_Attribute.Value == "PPA") || (aSPRIN_OCC_Attribute.Value  == "OPA") || (aSPRIN_OCC_Attribute.Value =="YOPA") || (aSPRIN_OCC_Attribute.Value =="YPPA"))
							{
								
								dtMvr = objQuickQuote.GetMVRPointsForSurcharge(CustomerID,AppID,AppVersionID,int.Parse(dtAssignedDriver.Rows[i]["DRIVER_ID"].ToString().Trim()),"APP",objDataWrapper);
								if(dtMvr!=null && dtMvr.Rows.Count>0)	
								{
									idrivVioAccip = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
									
								}
								if(dtMvr!=null && dtMvr.Rows.Count>0)	
								{
									idrivAccip = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
									
								}
								
							}
							strMVRNode.InnerXml = idrivVioAccip.ToString();
							strDriverNode.AppendChild(strMVRNode);
							strAccNode.InnerXml = idrivAccip.ToString();
							strDriverNode.AppendChild(strAccNode);
							//Now user will decide whose driver chrges will be considered for class calculation
							//Driver Violation And Accident point
							// if total driver on the policy is one
							/*if(drvNodesForDrvCount.Count.ToString() == "1")
							{
								if(isecDriver >1 )
								{
									strFchDrvClXml ="false";
								}
							}
							if (strFchDrvClXml == "false")
							{
								XmlElement strMVRNode = gxmlAutopQuickQuoteXml.CreateElement("SUMOFVIOLATIONPOINTS");
								strMVRNode.InnerXml = "0";
								strDriverNode.AppendChild(strMVRNode);
							}
							else
							{
								XmlElement strMVRNode = gxmlAutopQuickQuoteXml.CreateElement("SUMOFVIOLATIONPOINTS");
								dtMvr = objQuickQuote.GetMVRPointsForSurcharge(CustomerID,AppID,AppVersionID,int.Parse(dtAssignedDriver.Rows[i]["DRIVER_ID"].ToString().Trim()),"APP",objDataWrapper);
								if(dtMvr!=null && dtMvr.Rows.Count>0)	
								{
									idrivVioAccip = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
									idrivVioAccip += int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
									strMVRNode.InnerXml = idrivVioAccip.ToString();
								}
								strDriverNode.AppendChild(strMVRNode);
							}*/
							
							xNode.AppendChild(strDriverNode);						
						}
						isecDriver++;
					}				
			}
			
			XmlNode PolicyClassNode = gxmlAutopQuickQuoteXml.SelectSingleNode("//POLICY"); //Get Policy 
			if(PolicyClassNode==null || PolicyClassNode.InnerXml==null || PolicyClassNode.InnerXml.ToString()=="")
				return;
			string strPolicyClassInfo = PolicyClassNode.InnerXml;
			
			foreach(XmlNode vehNodeForClass in vehNodesForClass)
			{
				//If the vehicle type is commercial, we do not need to set the class, lets continue with other vehicles
				if(vehNodeForClass.SelectSingleNode("VEHICLETYPEUSE")!=null && vehNodeForClass.SelectSingleNode("VEHICLETYPEUSE").InnerXml.ToString().ToUpper()=="COMMERCIAL")
					continue;
				string strVehicleType ="";
				if(vehNodeForClass.SelectSingleNode("VEHICLETYPE")!=null)
				{
					strVehicleType = vehNodeForClass.SelectSingleNode("VEHICLETYPE").InnerText;
				}
				int VehicleID = int.Parse(vehNodeForClass.Attributes.Item(0).InnerXml.ToString().Trim());				
				XmlNodeList driverNodesForClass = vehNodeForClass.SelectNodes("DRIVER");
				strDriverXml = ""; 

				foreach(XmlNode node in driverNodesForClass)
				{
					strDriverXml = strDriverXml + node.OuterXml;
				}

				//Get the Eligible Driver 
				eligibleDriver = "<ELIGIBLEDRIVERS>" + "<POLICY>" + strPolicyClassInfo.ToUpper() + "</POLICY>" + strDriverXml.ToUpper() + "</ELIGIBLEDRIVERS>"; 
				ClsAuto objAuto = new ClsAuto();
				driverInputXml = objAuto.GetEligibleDrivers(eligibleDriver);				
				string strVehicleClass="";				
				if((strVehicleType=="TR" || strVehicleType=="CTT")&& driverInputXml=="")
				{
					strVehicleClass="";
				}
				else if(driverInputXml=="")
				{
					strVehicleClass="PA";
				}
				else
				{
					XmlDocument xDriverDoc = new XmlDocument();
					xDriverDoc.LoadXml(driverInputXml);

                     					
					XmlNode xNodes = xDriverDoc.SelectSingleNode("DRIVER");	
					driverInputXml = "<DRIVER>";
					for(int i=0;i<xNodes.Attributes.Count;i++)
					{
						if(xNodes.Attributes[i].Name != "VEHICLEDRIVEDAS")
						{
							driverInputXml+= "<" + xNodes.Attributes[i].Name + ">" + xNodes.Attributes[i].Value.ToString() + "</" + xNodes.Attributes[i].Name + ">";
						}
							driverInputXml+= "<" + xNodes.SelectSingleNode("VEHICLEDRIVEDAS").Name +">" + xNodes.SelectSingleNode("VEHICLEDRIVEDAS").InnerText + "</"+ xNodes.SelectSingleNode("VEHICLEDRIVEDAS").Name +">";
					}
					driverInputXml+= "<VEHICLECOUNT>"+cntVehicle.ToString() +"</VEHICLECOUNT><DRIVERCOUNT>"+ cntDriver.ToString() +"</DRIVERCOUNT><VEHICLETYPE>"+ strVehicleType +"</VEHICLETYPE>";
					if(xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS") !=null)
					{
						driverInputXml+= "<" + xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS").Name.ToString() +">" + xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText + "</" + xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS").Name.ToString() +">" + "<" + xNodes.SelectSingleNode("SUMOFACCIDENTPOINTS").Name.ToString() +">" + xNodes.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText + "</" + xNodes.SelectSingleNode("SUMOFACCIDENTPOINTS").Name.ToString() +">"+"</DRIVER>";	
					}
					else
					{
						driverInputXml+= "<" + xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS").Name.ToString() +">" + 0 + "</" + xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS").Name.ToString() +">" + "<" + xNodes.SelectSingleNode("SUMOFACCIDENTPOINTS").Name.ToString() +">" + 0 + "</" + xNodes.SelectSingleNode("SUMOFACCIDENTPOINTS").Name.ToString() +">"+"</DRIVER>";
					}
						//Load the driver that was used for calculating the vehicle class
					iEligibleDriverId = int.Parse(xNodes.Attributes["ID"].Value.ToString());
	
					// if policy have only one driver
					// This code will be commented as Now user will select to whome points will applied.
					/*if(drvNodesForDrvCount.Count.ToString() == "1")
					{
						if(iSecDrvForClass >=1)
						{
							strFchDrvClXmlForClass = "false";
						}
					}
					if (strFchDrvClXmlForClass == "false")
					{
						driverInputXml+= "<SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS></DRIVER>";								
					}
					else
					{
						DataTable dtTemp = objQuickQuote.GetMVRPointsForSurcharge(CustomerID,AppID,AppVersionID,int.Parse(xNodes.Attributes["ID"].Value.ToString()),"APP",objDataWrapper);
						if(dtTemp!=null && dtTemp.Rows.Count>0)
							driverInputXml+= "<SUMOFVIOLATIONPOINTS>" + dtTemp.Rows[0]["SUM_MVR_POINTS"].ToString() + "</SUMOFVIOLATIONPOINTS>" + "<SUMOFACCIDENTPOINTS>" + dtTemp.Rows[0]["ACCIDENT_POINTS"].ToString() +"</SUMOFACCIDENTPOINTS></DRIVER>";
						else
							driverInputXml+= "<SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS></DRIVER>";								
						iSecDrvForClass++;
					}*/
					//Set the eligible driver
					string classXml = "<CLASS>" + strPolicyClassInfo.ToUpper() + "<DRIVERINFO>" + driverInputXml.ToUpper() +"</DRIVERINFO>" + "</CLASS>";
					ClsAuto gObjQQAuto = new ClsAuto();
					// if driver has assinged two vehicle and class comes in 1,2,3 then first vehicle will be assignd original class but remaining vehicle will be given PA class
					string strDrvAge = xDriverDoc.SelectSingleNode("DRIVER/AGEOFDRIVER").InnerText.ToString();
					if(drvNodesForDrvCount.Count.ToString() == "1")
					{
						if(strFchDrvClXmlForClass == "false" && int.Parse(strDrvAge) <25)
						{
							strVehicleClass = "PA";
						}
						else
						{
							strVehicleClassXml = gObjQQAuto.GetVehicleClass(classXml,"AUTOP");					
							xDriverDoc.LoadXml(strVehicleClassXml);
							strVehicleClass = xDriverDoc.SelectSingleNode("CLASS/VEHICLECLASS").InnerText.ToString();
						}

					}
					else
					{
						strVehicleClassXml = gObjQQAuto.GetVehicleClass(classXml,"AUTOP");					
						xDriverDoc.LoadXml(strVehicleClassXml);
						strVehicleClass = xDriverDoc.SelectSingleNode("CLASS/VEHICLECLASS").InnerText.ToString();
					}
				}
				objDataWrapper.ClearParameteres();						
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@APP_ID",AppID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID);
				objDataWrapper.AddParameter("@VEHICLE_CLASS",strVehicleClass);
				if(iEligibleDriverId!=0)
					objDataWrapper.AddParameter("@CLASS_DRIVERID",iEligibleDriverId);					
				objDataWrapper.ExecuteNonQuery("PROC_UPDATEVEHICLECLASS");					
				objDataWrapper.ClearParameteres();
				
				
			}
		}
		
		# region U P D A T E  M O T O R  V E H I C L E  C L A S S
		public void UpdateMotorVehicleClassNew(int CustomerID, int AppID, int AppVersionID)
		{
			UpdateMotorVehicleClassNew(CustomerID,AppID,AppVersionID,null);
		}
		public void UpdateMotorVehicleClassNew(int CustomerID, int AppID, int AppVersionID,DataWrapper objDataWrapper)
		{
			string strVehicleClassXml = "",strDriverXml = "";
			int isecDriver=0;
			int iEligibleDriverId = 0;
			//Get the XML for customer,app,version
			ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
			string strInputXML = objGeneralInfo.GetInputXML(CustomerID,AppID,AppVersionID,((int)enumLOB.CYCL).ToString(),objDataWrapper);
			ClsAuto objAuto = new ClsAuto();

			//Encode Xml Charecters
			strInputXML = ClsCommon.EncodeXMLCharacters(strInputXML); 
			strInputXML = ClsCommon.DecodeXMLCharacters(strInputXML); 
			strInputXML=strInputXML.ToUpper();
			strInputXML=strInputXML.Replace("&AMP;","&amp;");
			strInputXML=strInputXML.Replace("\n","");
			strInputXML=strInputXML.Replace("\r","");
			strInputXML=strInputXML.Replace("\t","");

			//Prepare Class XML
			XmlDocument gxmlMotorQuickQuoteXml = new XmlDocument();
			gxmlMotorQuickQuoteXml.LoadXml(strInputXML);

			//Get Policy XML :
			XmlNode polNode = gxmlMotorQuickQuoteXml.SelectSingleNode("//POLICY");
			string strPOL = polNode.OuterXml.ToString();

			XmlNodeList vehNodesForClass = gxmlMotorQuickQuoteXml.SelectNodes("QUICKQUOTE/VEHICLES/VEHICLE");
			XmlNodeList drvNodesForClass = gxmlMotorQuickQuoteXml.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER");

			//////////////////////////////////////////////////////NEW CODE
			// COUNT number of Vehicle and Driver 
			int cntVehicle=vehNodesForClass.Count;
			int cntDriver=drvNodesForClass.Count;

			//Adding new drive node to an existing vehicle node	
			DataTable dtAssignedDriver = ClsDriverDetail.GetDriverAssignedForVehicleAppMotor(CustomerID,AppID,AppVersionID,objDataWrapper);
			foreach(XmlNode vehNodeForClass in vehNodesForClass)
			{
				int VehicleID = int.Parse(vehNodeForClass.Attributes.Item(0).InnerXml.ToString().Trim());
				for(int i =0;i<dtAssignedDriver.Rows.Count;i++)
				{
					if(VehicleID == int.Parse(dtAssignedDriver.Rows[i]["VEHICLE_ID"].ToString()) && dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString() !="")
					{

						XmlElement strDriverNode = gxmlMotorQuickQuoteXml.CreateElement("DRIVER");						
						XmlNode xNode = gxmlMotorQuickQuoteXml.SelectSingleNode("QUICKQUOTE/VEHICLES/VEHICLE[@ID=" + VehicleID.ToString() + "]");
						XmlAttribute xVehicleIDAttribute = gxmlMotorQuickQuoteXml.CreateAttribute("VEHICLEASSIGNEDASOPERATOR");
						xVehicleIDAttribute.Value = dtAssignedDriver.Rows[i]["VEHICLE_ID"].ToString();
						strDriverNode.Attributes.Append(xVehicleIDAttribute);

						XmlAttribute xPRIN_OCC_Attribute = gxmlMotorQuickQuoteXml.CreateAttribute("VEHICLEDRIVEDAS");
						xPRIN_OCC_Attribute.Value = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString().Trim();
						strDriverNode.Attributes.Append(xPRIN_OCC_Attribute);	

						//Driver ID
						XmlAttribute xDriverIDAttribute = gxmlMotorQuickQuoteXml.CreateAttribute("ID");
						xDriverIDAttribute.Value = dtAssignedDriver.Rows[i]["DRIVER_ID"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverIDAttribute);

						//Age of Driver
						XmlAttribute xDriverAgeAttribute = gxmlMotorQuickQuoteXml.CreateAttribute("AGEOFDRIVER");
						xDriverAgeAttribute.Value = dtAssignedDriver.Rows[i]["DRIVER_AGE"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverAgeAttribute);

						//Birthdate
						XmlAttribute xDriverDOBAttribute = gxmlMotorQuickQuoteXml.CreateAttribute("BIRTHDATE");
						xDriverDOBAttribute.Value = dtAssignedDriver.Rows[i]["BIRTHDATE"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverDOBAttribute);

						//Sex of Driver
						XmlAttribute xDriverGenderAttribute = gxmlMotorQuickQuoteXml.CreateAttribute("GENDER");
						xDriverGenderAttribute.Value = dtAssignedDriver.Rows[i]["GENDER"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverGenderAttribute);

					
						// Driver drives Vehicle as
						XmlAttribute aSPRIN_OCC_Attribute = gxmlMotorQuickQuoteXml.CreateAttribute("VEHICLEDRIVEDAS");
						aSPRIN_OCC_Attribute.Value = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDASCODE"].ToString().Trim();
						strDriverNode.Attributes.Append(aSPRIN_OCC_Attribute);
						
						//Creating Node for birthdate also
						XmlElement strBirthDateNode = gxmlMotorQuickQuoteXml.CreateElement("BIRTHDATE");
						strBirthDateNode.InnerXml = dtAssignedDriver.Rows[i]["BIRTHDATE"].ToString().Trim();
						strDriverNode.AppendChild(strBirthDateNode);

						//Creating Node for AGE OF DRIVER also
						XmlElement strDriverAgeNode = gxmlMotorQuickQuoteXml.CreateElement("AGEOFDRIVER");
						strDriverAgeNode.InnerXml = dtAssignedDriver.Rows[i]["DRIVER_AGE"].ToString().Trim();
						strDriverNode.AppendChild(strDriverAgeNode);

						XmlElement xPRIN_OCC_Node = gxmlMotorQuickQuoteXml.CreateElement("VEHICLEDRIVEDAS");
						xPRIN_OCC_Node.InnerXml = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString().Trim();
						strDriverNode.AppendChild(xPRIN_OCC_Node);

						xNode.AppendChild(strDriverNode);						
					}
					isecDriver++;
				}				
			}


			/////////////////////////////////////////////////////////////END NEW CODE
			
			if(objDataWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			strDriverXml = "";

			foreach(XmlNode vehNodeForClass in vehNodesForClass)
			{
				int VehicleID = int.Parse(vehNodeForClass.Attributes.Item(0).InnerXml.ToString().Trim());				
				XmlNodeList driverNodesForClass = vehNodeForClass.SelectNodes("DRIVER");
				strDriverXml = ""; 

				foreach(XmlNode node in driverNodesForClass)
				{
					strDriverXml = strDriverXml + node.OuterXml;
				}

				//Get the Eligible Driver : If Getch the Youngest Driver
				string eligibleDriver = "<ELIGIBLEDRIVERS>" + strDriverXml.ToUpper() + "</ELIGIBLEDRIVERS>"; 
				strDriverXml = objAuto.GetEligibleDriversMotor(eligibleDriver);

				if(strDriverXml!="")
				{
					XmlDocument xDriverDoc = new XmlDocument();
					xDriverDoc.LoadXml(strDriverXml);

                     					
					XmlNode xNodes = xDriverDoc.SelectSingleNode("DRIVER");	
					strDriverXml = "<DRIVER>";
					for(int i=0;i<xNodes.Attributes.Count;i++)
					{
						if(xNodes.Attributes[i].Name != "VEHICLEDRIVEDAS")
						{
							strDriverXml+= "<" + xNodes.Attributes[i].Name + ">" + xNodes.Attributes[i].Value.ToString() + "</" + xNodes.Attributes[i].Name + ">";
						}
						strDriverXml+= "<" + xNodes.SelectSingleNode("VEHICLEDRIVEDAS").Name +">" + xNodes.SelectSingleNode("VEHICLEDRIVEDAS").InnerText + "</"+ xNodes.SelectSingleNode("VEHICLEDRIVEDAS").Name +">";
					}

					strDriverXml+= "<VEHICLECOUNT>"+cntVehicle.ToString() +"</VEHICLECOUNT><DRIVERCOUNT>"+ cntDriver.ToString() +"</DRIVERCOUNT>"+"</DRIVER>";

					//Load the driver that was used for calculating the vehicle class
					iEligibleDriverId = int.Parse(xNodes.Attributes["ID"].Value.ToString());

				}

				/*intVehicleID = int.Parse(vehNodeForClass.Attributes.Item(0).InnerXml.ToString().Trim());				
				foreach(XmlNode node in drvNodesForClass)
				{
					if(node!=null)
					{
						iEligibleDriverId = int.Parse(node.Attributes.Item(0).InnerXml.ToString().Trim());
						if(node.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR")!=null)
						{
							if(int.Parse(node.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString().Trim()) 
								== intVehicleID)
								strDriverXml = strDriverXml + node.OuterXml;
						}
					}
				}*/

				//Set the eligible driver
				string classXml = "<CLASS>" +  strPOL.ToUpper() + "<DRIVERINFO>" + strDriverXml.ToUpper() + "</DRIVERINFO>" + "</CLASS>";
				//CaLL THIS METHD INSIDE LOOP
				strVehicleClassXml = objAuto.GetMotorVehicleClass(classXml);

				
			
			objDataWrapper.ClearParameteres();						
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objDataWrapper.AddParameter("@APP_ID",AppID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
			objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID);
			objDataWrapper.AddParameter("@VEHICLE_CLASS",strVehicleClassXml);
			objDataWrapper.AddParameter("@CLASS_DRIVERID",iEligibleDriverId);
			objDataWrapper.ExecuteNonQuery("PROC_UPDATEVEHICLECLASS");					
			objDataWrapper.ClearParameteres();
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		}
	}
		/// <summary>
		/// Updating Motor Vehicle Class at pol level
		/// </summary> 
		/// <param name="CustomerID"></param>
		/// <param name="PolID"></param>
		/// <param name="PolVersionID"></param>by pravesh
		public void UpdateMotorVehicleClassPOL(int CustomerID, int PolID, int PolVersionID)
		{
            DataWrapper objDataWrapper= new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                this.UpdateMotorVehicleClassPOL(objDataWrapper, CustomerID, PolID, PolVersionID);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception Ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (Ex);
            }
		}
		public void UpdateMotorVehicleClassPOL(DataWrapper objDataWrapper, int CustomerID, int PolID, int PolVersionID)
		{
			//string driverInputXml ="";
			string strVehicleClassXml = "";
			int isecDriver=0;
			int iEligibleDriverId = 0;
			string strDriverXml = "";
            //if (objDataWrapper==null)
            //    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//Get the XML for customer,app,version
			ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
			string strInputXML = objGeneralInfo.GetPolicyInputXML(CustomerID,PolID,PolVersionID,((int)enumLOB.CYCL).ToString(),objDataWrapper);
			strInputXML=strInputXML.Replace("&AMP;","&amp;");
			//Added by Sibin on 26 Feb 09 for Itrack Issue 5304
			strInputXML=strInputXML.Replace("\n","");
			strInputXML=strInputXML.Replace("\r","");
			strInputXML=strInputXML.Replace("\t","");
			//Added till here
			ClsAuto objAuto = new ClsAuto();
			//Prepare Class XML
			XmlDocument gxmlMotorQuickQuoteXml = new XmlDocument();
			gxmlMotorQuickQuoteXml.LoadXml(strInputXML);			
			//Get Policy XML :
			XmlNode polNode = gxmlMotorQuickQuoteXml.SelectSingleNode("//POLICY");
			string strPOL = polNode.OuterXml.ToString();
			//Get all Veh
			XmlNodeList vehNodesForClass = gxmlMotorQuickQuoteXml.SelectNodes("QUICKQUOTE/VEHICLES/VEHICLE");
			XmlNodeList drvNodesForClass = gxmlMotorQuickQuoteXml.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER");

			// COUNT number of Vehicle and Driver 
			int cntVehicle=vehNodesForClass.Count;
			int cntDriver=drvNodesForClass.Count;

			DataTable dtAssignedDriver = ClsDriverDetail.GetDriverAssignedForVehiclePol_Motor(CustomerID,PolID,PolVersionID,objDataWrapper);
			
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			foreach(XmlNode vehNodeForClass in vehNodesForClass)
			{
				int VehicleID = int.Parse(vehNodeForClass.Attributes.Item(0).InnerXml.ToString().Trim());
				for(int i =0;i<dtAssignedDriver.Rows.Count;i++)
				{
					if(VehicleID == int.Parse(dtAssignedDriver.Rows[i]["VEHICLE_ID"].ToString()) && dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString() !="")
					{

						XmlElement strDriverNode = gxmlMotorQuickQuoteXml.CreateElement("DRIVER");						
						XmlNode xNode = gxmlMotorQuickQuoteXml.SelectSingleNode("QUICKQUOTE/VEHICLES/VEHICLE[@ID=" + VehicleID.ToString() + "]");
						XmlAttribute xVehicleIDAttribute = gxmlMotorQuickQuoteXml.CreateAttribute("VEHICLEASSIGNEDASOPERATOR");
						xVehicleIDAttribute.Value = dtAssignedDriver.Rows[i]["VEHICLE_ID"].ToString();
						strDriverNode.Attributes.Append(xVehicleIDAttribute);

						XmlAttribute xPRIN_OCC_Attribute = gxmlMotorQuickQuoteXml.CreateAttribute("VEHICLEDRIVEDAS");
						xPRIN_OCC_Attribute.Value = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString().Trim();
						strDriverNode.Attributes.Append(xPRIN_OCC_Attribute);	

						//Driver ID
						XmlAttribute xDriverIDAttribute = gxmlMotorQuickQuoteXml.CreateAttribute("ID");
						xDriverIDAttribute.Value = dtAssignedDriver.Rows[i]["DRIVER_ID"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverIDAttribute);

						//Age of Driver
						XmlAttribute xDriverAgeAttribute = gxmlMotorQuickQuoteXml.CreateAttribute("AGEOFDRIVER");
						xDriverAgeAttribute.Value = dtAssignedDriver.Rows[i]["DRIVER_AGE"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverAgeAttribute);

						//Birthdate
						XmlAttribute xDriverDOBAttribute = gxmlMotorQuickQuoteXml.CreateAttribute("BIRTHDATE");
						xDriverDOBAttribute.Value = dtAssignedDriver.Rows[i]["BIRTHDATE"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverDOBAttribute);

									
						// Driver drives Vehicle as
						XmlAttribute aSPRIN_OCC_Attribute = gxmlMotorQuickQuoteXml.CreateAttribute("VEHICLEDRIVEDAS");
						aSPRIN_OCC_Attribute.Value = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDASCODE"].ToString().Trim();
						strDriverNode.Attributes.Append(aSPRIN_OCC_Attribute);
						
						//Creating Node for birthdate also
						XmlElement strBirthDateNode = gxmlMotorQuickQuoteXml.CreateElement("BIRTHDATE");
						strBirthDateNode.InnerXml = dtAssignedDriver.Rows[i]["BIRTHDATE"].ToString().Trim();
						strDriverNode.AppendChild(strBirthDateNode);

						//Creating Node for AGE OF DRIVER also
						XmlElement strDriverAgeNode = gxmlMotorQuickQuoteXml.CreateElement("AGEOFDRIVER");
						strDriverAgeNode.InnerXml = dtAssignedDriver.Rows[i]["DRIVER_AGE"].ToString().Trim();
						strDriverNode.AppendChild(strDriverAgeNode);

						XmlElement xPRIN_OCC_Node = gxmlMotorQuickQuoteXml.CreateElement("VEHICLEDRIVEDAS");
						xPRIN_OCC_Node.InnerXml = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString().Trim();
						strDriverNode.AppendChild(xPRIN_OCC_Node);

						xNode.AppendChild(strDriverNode);						
					}
					isecDriver++;
				}				
			}


			strDriverXml = "";

			foreach(XmlNode vehNodeForClass in vehNodesForClass)
			{
				int VehicleID = int.Parse(vehNodeForClass.Attributes.Item(0).InnerXml.ToString().Trim());				
				XmlNodeList driverNodesForClass = vehNodeForClass.SelectNodes("DRIVER");
				strDriverXml = ""; 

				foreach(XmlNode node in driverNodesForClass)
				{
					strDriverXml = strDriverXml + node.OuterXml;
				}

				//Get the Eligible Driver : If Getch the Youngest Driver
				string eligibleDriver = "<ELIGIBLEDRIVERS>" + strDriverXml.ToUpper() + "</ELIGIBLEDRIVERS>"; 
				strDriverXml = objAuto.GetEligibleDriversMotor(eligibleDriver);

				if(strDriverXml!="")
				{
					XmlDocument xDriverDoc = new XmlDocument();
					xDriverDoc.LoadXml(strDriverXml);

                     					
					XmlNode xNodes = xDriverDoc.SelectSingleNode("DRIVER");	
					strDriverXml = "<DRIVER>";
					for(int i=0;i<xNodes.Attributes.Count;i++)
					{
						if(xNodes.Attributes[i].Name != "VEHICLEDRIVEDAS")
						{
							strDriverXml+= "<" + xNodes.Attributes[i].Name + ">" + xNodes.Attributes[i].Value.ToString() + "</" + xNodes.Attributes[i].Name + ">";
						}
						strDriverXml+= "<" + xNodes.SelectSingleNode("VEHICLEDRIVEDAS").Name +">" + xNodes.SelectSingleNode("VEHICLEDRIVEDAS").InnerText + "</"+ xNodes.SelectSingleNode("VEHICLEDRIVEDAS").Name +">";
					}

					strDriverXml+= "<VEHICLECOUNT>"+cntVehicle.ToString() +"</VEHICLECOUNT><DRIVERCOUNT>"+ cntDriver.ToString() +"</DRIVERCOUNT>"+"</DRIVER>";

					//Load the driver that was used for calculating the vehicle class
					iEligibleDriverId = int.Parse(xNodes.Attributes["ID"].Value.ToString());

				}

				
				string classXml = "<CLASS>" +  strPOL.ToUpper() + "<DRIVERINFO>" + strDriverXml.ToUpper() + "</DRIVERINFO>" + "</CLASS>";
											
				//CaLL THIS METHD INSIDE LOOP

				strVehicleClassXml = objAuto.GetMotorVehicleClass(classXml);
				objDataWrapper.ClearParameteres();                                
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID);
				objDataWrapper.AddParameter("@VEHICLE_CLASS",strVehicleClassXml);
				objDataWrapper.AddParameter("@CLASS_DRIVERID",iEligibleDriverId);				
				objDataWrapper.ExecuteNonQuery("PROC_UPDATEVEHICLECLASS_POL");                          
				objDataWrapper.ClearParameteres();
				//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}

		}

		#endregion
		public void UpdateVehicleClassPolNew(int CustomerID, int PolID, int PolVersionID)
		{
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                UpdateVehicleClassPolNew(CustomerID, PolID, PolVersionID, null);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception Ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (Ex);
            }
		}
		public void UpdateVehicleClassPolNew(int CustomerID, int PolID, int PolVersionID,DataWrapper objDataWrapper)
		{
			string driverInputXml ="";
			string eligibleDriver ="";
			string strVehicleClassXml = "";
			string strDriverXml = "";
			
			int iEligibleDriverId = 0;
			int isecDriver=0;
			//int iSecDrvForClass = 0;
			//string strFchDrvClXml="";
			string strFchDrvClXmlForClass="";
			
			ClsQuickQuote objQuickQuote = new ClsQuickQuote();
			DataTable dtMvr;

			//Get the XML for customer,app,version

			ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
            //if (objDataWrapper==null)
            //    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string strInputXML = objGeneralInfo.GetPolicyInputXML(CustomerID,PolID,PolVersionID,((int)enumLOB.AUTOP).ToString(),objDataWrapper);

			//Encode Xml Charecters
			strInputXML = ClsCommon.EncodeXMLCharacters(strInputXML); 
			strInputXML = ClsCommon.DecodeXMLCharacters(strInputXML); 
			strInputXML=strInputXML.ToUpper();
			strInputXML=strInputXML.Replace("&AMP;","&amp;");
			strInputXML=strInputXML.Replace("\n","");
			strInputXML=strInputXML.Replace("\r","");
			strInputXML=strInputXML.Replace("\t","");
			strInputXML=strInputXML.Replace("&GT;","&gt;");
			XmlDocument gxmlAutopQuickQuoteXml =new XmlDocument(); 
			gxmlAutopQuickQuoteXml.LoadXml(strInputXML);

			XmlNodeList vehNodesForClass = gxmlAutopQuickQuoteXml.SelectNodes("QUICKQUOTE/VEHICLES/VEHICLE");
			XmlNodeList drvNodesForDrvCount = gxmlAutopQuickQuoteXml.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER");
			int cntVehicle=vehNodesForClass.Count;
			int cntDriver=drvNodesForDrvCount.Count;
			//Adding new drive node to an existing vehicle node	
			DataTable dtAssignedDriver = ClsDriverDetail.GetDriverAssignedForVehiclePol(CustomerID,PolID,PolVersionID,objDataWrapper);
			if (objDataWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			foreach(XmlNode vehNodeForClass in vehNodesForClass)
			{
				int VehicleID = int.Parse(vehNodeForClass.Attributes.Item(0).InnerXml.ToString().Trim());
				for(int i =0;i<dtAssignedDriver.Rows.Count;i++)
				{
					if(VehicleID == int.Parse(dtAssignedDriver.Rows[i]["VEHICLE_ID"].ToString()))
					{
						int idrivVioAccip=0,idrivAccip=0;
						XmlElement strDriverNode = gxmlAutopQuickQuoteXml.CreateElement("DRIVER");						
						XmlNode xNode = gxmlAutopQuickQuoteXml.SelectSingleNode("QUICKQUOTE/VEHICLES/VEHICLE[@ID=" + VehicleID.ToString() + "]");
						XmlAttribute xVehicleIDAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("VEHICLEASSIGNEDASOPERATOR");
						xVehicleIDAttribute.Value = dtAssignedDriver.Rows[i]["VEHICLE_ID"].ToString();
						strDriverNode.Attributes.Append(xVehicleIDAttribute);

						XmlAttribute xPRIN_OCC_Attribute = gxmlAutopQuickQuoteXml.CreateAttribute("VEHICLEDRIVEDAS");
						xPRIN_OCC_Attribute.Value = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString().Trim();
						strDriverNode.Attributes.Append(xPRIN_OCC_Attribute);	

						//Driver ID
						XmlAttribute xDriverIDAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("ID");
						xDriverIDAttribute.Value = dtAssignedDriver.Rows[i]["DRIVER_ID"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverIDAttribute);

						//Age of Driver
						XmlAttribute xDriverAgeAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("AGEOFDRIVER");
						xDriverAgeAttribute.Value = dtAssignedDriver.Rows[i]["DRIVER_AGE"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverAgeAttribute);

						// Driver drives Vehicle as
						XmlAttribute aSPRIN_OCC_Attribute = gxmlAutopQuickQuoteXml.CreateAttribute("VEHICLEDRIVEDAS");
						aSPRIN_OCC_Attribute.Value = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDASCODE"].ToString().Trim();
						strDriverNode.Attributes.Append(aSPRIN_OCC_Attribute);
						
						//Birthdate
						XmlAttribute xDriverDOBAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("BIRTHDATE");
						xDriverDOBAttribute.Value = dtAssignedDriver.Rows[i]["BIRTHDATE"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverDOBAttribute);

						//Sex of Driver
						XmlAttribute xDriverGenderAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("GENDER");
						xDriverGenderAttribute.Value = dtAssignedDriver.Rows[i]["GENDER"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverGenderAttribute);

						//College Student
						XmlAttribute xDriverCollStdAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("COLLEGESTUDENT");
						xDriverCollStdAttribute.Value = dtAssignedDriver.Rows[i]["COLLEGESTUDENT"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverCollStdAttribute);		
				
						//Have Car
						XmlAttribute xDriverHaveCarAttribute = gxmlAutopQuickQuoteXml.CreateAttribute("HAVE_CAR");
						xDriverHaveCarAttribute.Value = dtAssignedDriver.Rows[i]["HAVE_CAR"].ToString().Trim();
						strDriverNode.Attributes.Append(xDriverHaveCarAttribute);
						
						XmlElement strMVRNode = gxmlAutopQuickQuoteXml.CreateElement("SUMOFVIOLATIONPOINTS");
						XmlElement strAccNode = gxmlAutopQuickQuoteXml.CreateElement("SUMOFACCIDENTPOINTS");
						if((aSPRIN_OCC_Attribute.Value == "PPA") || (aSPRIN_OCC_Attribute.Value  == "OPA") || (aSPRIN_OCC_Attribute.Value =="YOPA") || (aSPRIN_OCC_Attribute.Value =="YPPA"))
						{
							
							dtMvr = objQuickQuote.GetMVRPointsForSurcharge(CustomerID,PolID,PolVersionID,int.Parse(dtAssignedDriver.Rows[i]["DRIVER_ID"].ToString().Trim()),"POL");
							if(dtMvr!=null && dtMvr.Rows.Count>0)	
							{
								idrivVioAccip = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
								
							}
							
							
							if(dtMvr!=null && dtMvr.Rows.Count>0)	
							{
								idrivAccip = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
								
							}
							
						}
						strAccNode.InnerXml = idrivAccip.ToString();
						strMVRNode.InnerXml = idrivVioAccip.ToString();
						strDriverNode.AppendChild(strMVRNode);
						strDriverNode.AppendChild(strAccNode);
						
						//Creating Node for birthdate also
						XmlElement strBirthDateNode = gxmlAutopQuickQuoteXml.CreateElement("BIRTHDATE");
						strBirthDateNode.InnerXml = dtAssignedDriver.Rows[i]["BIRTHDATE"].ToString().Trim();
						strDriverNode.AppendChild(strBirthDateNode);

						//Creating Node for AGE OF DRIVER also
						XmlElement strDriverAgeNode = gxmlAutopQuickQuoteXml.CreateElement("AGEOFDRIVER");
						strDriverAgeNode.InnerXml = dtAssignedDriver.Rows[i]["DRIVER_AGE"].ToString().Trim();
						strDriverNode.AppendChild(strDriverAgeNode);

						XmlElement xPRIN_OCC_Node = gxmlAutopQuickQuoteXml.CreateElement("VEHICLEDRIVEDAS");
						xPRIN_OCC_Node.InnerXml = dtAssignedDriver.Rows[i]["VEHICLEDRIVEDAS"].ToString().Trim();
						strDriverNode.AppendChild(xPRIN_OCC_Node);
						//Driver Violation And Accident point
						// if total driver on the policy is one
						/*if(drvNodesForDrvCount.Count.ToString() == "1")
						{
							if(isecDriver >1 )
							{
								strFchDrvClXml ="false";
							}
						}
						if (strFchDrvClXml == "false")
						{
							XmlElement strMVRNode = gxmlAutopQuickQuoteXml.CreateElement("SUMOFVIOLATIONPOINTS");
							strMVRNode.InnerXml = "0";
							strDriverNode.AppendChild(strMVRNode);
						}
						else
						{

							XmlElement strMVRNode = gxmlAutopQuickQuoteXml.CreateElement("SUMOFVIOLATIONPOINTS");
							dtMvr = objQuickQuote.GetMVRPointsForSurcharge(CustomerID,PolID,PolVersionID,int.Parse(dtAssignedDriver.Rows[i]["DRIVER_ID"].ToString().Trim()),"POL");
							if(dtMvr!=null && dtMvr.Rows.Count>0)	
							{
								idrivVioAccip = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
								idrivVioAccip += int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
								strMVRNode.InnerXml = idrivVioAccip.ToString();
							}	
							strDriverNode.AppendChild(strMVRNode);
						}*/
						xNode.AppendChild(strDriverNode);						
					}
					isecDriver++;
				}
			}
			
			XmlNode PolicyClassNode = gxmlAutopQuickQuoteXml.SelectSingleNode("//POLICY"); //Get Policy 			
			if(PolicyClassNode==null || PolicyClassNode.InnerXml==null || PolicyClassNode.InnerXml.ToString()=="")
				return;
			string strPolicyClassInfo = PolicyClassNode.InnerXml;
			// //string strDriverClassInfo = "";

			foreach(XmlNode vehNodeForClass in vehNodesForClass)
			{
				//If the vehicle type is commercial, we do not need to set the class, lets continue with other vehicles
				if(vehNodeForClass.SelectSingleNode("VEHICLETYPEUSE")!=null && vehNodeForClass.SelectSingleNode("VEHICLETYPEUSE").InnerXml.ToString().ToUpper()=="COMMERCIAL")
					continue;
				string strVehicleType ="";
				if(vehNodeForClass.SelectSingleNode("VEHICLETYPE")!=null)
				{
					strVehicleType = vehNodeForClass.SelectSingleNode("VEHICLETYPE").InnerText;
				}
				int VehicleID = int.Parse(vehNodeForClass.Attributes.Item(0).InnerXml.ToString().Trim());				
				XmlNodeList driverNodesForClass = vehNodeForClass.SelectNodes("DRIVER");
				strDriverXml = ""; 

				foreach(XmlNode node in driverNodesForClass)
				{
					strDriverXml = strDriverXml + node.OuterXml;
				}

				//Get the Eligible Driver 
				eligibleDriver = "<ELIGIBLEDRIVERS>" + "<POLICY>" + strPolicyClassInfo.ToUpper() + "</POLICY>" + strDriverXml.ToUpper() + "</ELIGIBLEDRIVERS>"; 
				ClsAuto objAuto = new ClsAuto();
				driverInputXml = objAuto.GetEligibleDrivers(eligibleDriver);				
				string strVehicleClass="";
				if((strVehicleType=="TR" || strVehicleType=="CTT")&& driverInputXml=="")
				{
					strVehicleClass="";
				}
				else if(driverInputXml=="")
				{
					strVehicleClass="PA";
				}
				else
				{
					XmlDocument xDriverDoc = new XmlDocument();
					xDriverDoc.LoadXml(driverInputXml);
					
					XmlNode xNodes = xDriverDoc.SelectSingleNode("DRIVER");	
					driverInputXml = "<DRIVER>";
					driverInputXml+= "<VEHICLECOUNT>"+cntVehicle.ToString() +"</VEHICLECOUNT><DRIVERCOUNT>"+ cntDriver.ToString() +"</DRIVERCOUNT><VEHICLETYPE>"+ strVehicleType +"</VEHICLETYPE>";
					for(int i=0;i<xNodes.Attributes.Count;i++)
					{
						if(xNodes.Attributes[i].Name != "VEHICLEDRIVEDAS")
						{
							driverInputXml+= "<" + xNodes.Attributes[i].Name + ">" + xNodes.Attributes[i].Value.ToString() + "</" + xNodes.Attributes[i].Name + ">";
						}
						driverInputXml+= "<" + xNodes.SelectSingleNode("VEHICLEDRIVEDAS").Name +">" + xNodes.SelectSingleNode("VEHICLEDRIVEDAS").InnerText + "</"+ xNodes.SelectSingleNode("VEHICLEDRIVEDAS").Name +">";
					}
					if(xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS") !=null)
					{
						driverInputXml+= "<" + xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS").Name.ToString() +">" + xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText + "</" + xNodes.SelectSingleNode("SUMOFVIOLATIONPOINTS").Name.ToString() +">" + "<" + xNodes.SelectSingleNode("SUMOFACCIDENTPOINTS").Name.ToString() +">" + xNodes.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText + "</" + xNodes.SelectSingleNode("SUMOFACCIDENTPOINTS").Name.ToString() +">"+"</DRIVER>";	
					}
					else
					{
						driverInputXml+= "</DRIVER>";
					}
					//Load the driver that was used for calculating the vehicle class
					iEligibleDriverId = int.Parse(xNodes.Attributes["ID"].Value.ToString());
					// if policy have only one driver
					/*if(drvNodesForDrvCount.Count.ToString() == "1")
					{
						if(iSecDrvForClass >=1)
						{
							strFchDrvClXmlForClass = "false";
						}
					}
					if (strFchDrvClXmlForClass == "false")
					{
						driverInputXml+= "<SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS></DRIVER>";								
					}
					else
					{
						DataTable dtTemp = objQuickQuote.GetMVRPointsForSurcharge(CustomerID,PolID,PolVersionID,int.Parse(xNodes.Attributes["ID"].Value.ToString()),"POL",objDataWrapper);
						if(dtTemp!=null && dtTemp.Rows.Count>0)
							driverInputXml+= "<SUMOFVIOLATIONPOINTS>" + dtTemp.Rows[0]["SUM_MVR_POINTS"].ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>"+  dtTemp.Rows[0]["ACCIDENT_POINTS"].ToString() + "</SUMOFACCIDENTPOINTS></DRIVER>";
						else
							driverInputXml+= "<SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS></DRIVER>";								
					iSecDrvForClass++;
					}*/
					//Set the eligible driver
					string classXml = "<CLASS>" + strPolicyClassInfo.ToUpper() + "<DRIVERINFO>" + driverInputXml.ToUpper() + "</DRIVERINFO>" + "</CLASS>";
					ClsAuto gObjQQAuto = new ClsAuto();
					string strDrvAge = xDriverDoc.SelectSingleNode("DRIVER/AGEOFDRIVER").InnerText.ToString();
					if(drvNodesForDrvCount.Count.ToString() == "1")
					{
						if(strFchDrvClXmlForClass == "false" && int.Parse(strDrvAge) <25)
						{
							strVehicleClass = "PA";
						}
						else
						{
							strVehicleClassXml = gObjQQAuto.GetVehicleClass(classXml,"AUTOP");					
							xDriverDoc.LoadXml(strVehicleClassXml);
							strVehicleClass = xDriverDoc.SelectSingleNode("CLASS/VEHICLECLASS").InnerText.ToString();
						}
					}
					else
					{
						strVehicleClassXml = gObjQQAuto.GetVehicleClass(classXml,"AUTOP");					
						xDriverDoc.LoadXml(strVehicleClassXml);
						strVehicleClass = xDriverDoc.SelectSingleNode("CLASS/VEHICLECLASS").InnerText.ToString();
					}
				}
				objDataWrapper.ClearParameteres();						
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID);
				objDataWrapper.AddParameter("@VEHICLE_CLASS",strVehicleClass);
				if(iEligibleDriverId!=0)
					objDataWrapper.AddParameter("@CLASS_DRIVERID",iEligibleDriverId);					
				objDataWrapper.ExecuteNonQuery("PROC_UPDATEVEHICLECLASS_POL");					
				objDataWrapper.ClearParameteres();
								
			}
		}


		
	

		public DataSet GetAutoDriversForCustomer(int CUSTOMER_ID)
		{
			string		strStoredProc	=	"Proc_GetAutoDriversForCustomer";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataSet dsTemp=null;
           		
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);				
				dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
				if(dsTemp!=null && dsTemp.Tables.Count>0)
					return dsTemp;
				else
					return null;
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(dsTemp!=null)
					dsTemp=null;
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}			
		}
	
		/* Commented by Charles on 2-Jul-09 for Itrack issue 6012
		 
		//Method added by Charles for Itrack Issue 5744 on 8-Jun-2009
		/// <summary>
		/// Gets YearsWithWolverine and YearsContinuouslyInsured
		/// </summary>
		/// <param name="customerID">Customer ID</param>
		/// <param name="appID">Application ID</param>
		/// <param name="appVersionID">Application Version</param>
		/// <returns>Dataset containing YearsWithWolverine and YearsContinuouslyInsured</returns>
		public DataSet GetYearsWolvYearsContIns(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc	=	"Proc_GetYearsWithWolverine";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.ON);
			
			try
			{	
				DataSet dsTemp = new DataSet();
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@APP_ID",appID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@YEARS_WITH_WOLVERINE",SqlDbType.VarChar,ParameterDirection.Output);
				objSqlParameter.Size=2;
				dsTemp=objDataWrapper.ExecuteDataSet(strStoredProc);					
				return dsTemp;
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
		*/

		//Added by Sibin for Itrack Issue 5428 on 18 Feb 09
		public int GetInsuredWithWolverine(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc	=	"Proc_GetYearsWithWolverine";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int result=0;
			try
			{	
				DataSet dsTemp = new DataSet();
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@APP_ID",appID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@YEARS_WITH_WOLVERINE",SqlDbType.VarChar,ParameterDirection.Output);
				objSqlParameter.Size=2;
				dsTemp=objDataWrapper.ExecuteDataSet(strStoredProc);
				result= int.Parse(objSqlParameter.Value.ToString());	
				return result;
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

		/* Commented by Charles on 2-Jul-09 for Itrack issue 6012
		 
		//Method added by Charles for Itrack Issue 5744 on 5-Jun-2009
		/// <summary>
		/// Gets Policy YearsWithWolverine and YearsContinuouslyInsured
		/// </summary>
		/// <param name="customerID">Customer ID</param>
		/// <param name="polID">Policy ID</param>
		/// <param name="polVersionID">Policy Version</param>
		/// <returns>Dataset containing YearsWithWolverine and YearsContinuouslyInsured</returns>
		public DataSet GetPolYearsWolvYearsContIns(int customerID, int polID, int polVersionID)
		{
			string	strStoredProc	=	"Proc_GetPolicyYearsWithWolverine";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.ON);

			try
			{	
				DataSet dsTemp = new DataSet();
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@POLICY_ID",polID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@YEARS_WITH_WOLVERINE",SqlDbType.VarChar,ParameterDirection.Output);
				objSqlParameter.Size=2;
				dsTemp=objDataWrapper.ExecuteDataSet(strStoredProc);
				return dsTemp;
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
		*/

		//Added by Sibin for Itrack Issue 5428 on 18 Feb 09
		public int GetPolicyInsuredWithWolverine(int customerID, int polID, int polVersionID)
		{
			string	strStoredProc	=	"Proc_GetPolicyYearsWithWolverine";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int result=0;
			try
			{	
				DataSet dsTemp = new DataSet();
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@POLICY_ID",polID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@YEARS_WITH_WOLVERINE",SqlDbType.VarChar,ParameterDirection.Output);
				objSqlParameter.Size=2;
				dsTemp=objDataWrapper.ExecuteDataSet(strStoredProc);
				result= int.Parse(objSqlParameter.Value.ToString());	
				return result;
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
		//Added for Itrack Issue 5457 on 14 April 2009
		public string CheckDriverDelete(int customerID, int AppID, int VersionID, int driverID, string calledFrom)
		{
			string	strStoredProc	=	"Proc_CheckDeleteDriver";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string result="";
			try
			{	
				DataSet dsTemp = new DataSet();
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@APP_ID",AppID);
				objDataWrapper.AddParameter("@VERSION_ID",VersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",driverID);
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);
				dsTemp=objDataWrapper.ExecuteDataSet(strStoredProc);

				if(dsTemp!=null && dsTemp.Tables.Count>0)
				{
					if(dsTemp.Tables[0].Rows.Count > 0)
					{
						result = "";
						for(int count=0;count<dsTemp.Tables[0].Rows.Count;count++)
						{
							result+= dsTemp.Tables[0].Rows[count]["DRIVER_DETAILS"].ToString();
							if(count!=dsTemp.Tables[0].Rows.Count-1)
							{
								result += ", ";
							}
						}
						result += "~";
					}

					if(dsTemp.Tables[1].Rows.Count > 0)
					{
						for(int count=0;count<dsTemp.Tables[1].Rows.Count;count++)
						{
							result+= dsTemp.Tables[1].Rows[count]["LOSS_ID"].ToString();
							if(count!=dsTemp.Tables[1].Rows.Count-1)
							{
								result += ", ";
							}
						}
					}

				}
				return result;
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
	}
}
