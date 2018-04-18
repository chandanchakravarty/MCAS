/******************************************************************************************
<Author				: -   Gaurav
<Start Date				: -	8/18/2005 11:10:45 AM
<End Date				: -	
<Description				: - 	This file is used to
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Nov 09,2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Additional Info in the Transaction log to record LOB name and screen name has been added
<Modified Date			: - 16/12/2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Check has been added at update of data to prevent an entry from going into transaction log when no modication has taken place.
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
//using Cms.Model.Application.GeneralLiability;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlApplication.GeneralLiability
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsGeneralUnderwritingInformation : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_GENERAL_UNDERWRITING_INFO			=	"APP_GENERAL_UNDERWRITING_INFO";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _CUSTOMER_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateGeneralUnderWriting";
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
		public ClsGeneralUnderwritingInformation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

        //#region Add(Insert) functions
        ///// <summary>
        ///// Saves the information passed in model object to database.
        ///// </summary>
        ///// <param name="objGeneralInfo">Model class object.</param>
        ///// <returns>No of records effected.</returns>
        //public int Add(ClsGeneralUnderwritingInfo objGeneralInfo)
        //{
        //    string		strStoredProc	=	"Proc_InsertGeneralUnderWriting";
        //    DateTime	RecordDate		=	DateTime.Now;
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

        //    try
        //    {
        //        objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@INSURANCE_DECLINED_FIVE_YEARS",objGeneralInfo.INSURANCE_DECLINED_FIVE_YEARS);
        //        objDataWrapper.AddParameter("@MEDICAL_PROFESSIONAL_EMPLOYEED",objGeneralInfo.MEDICAL_PROFESSIONAL_EMPLOYEED);
        //        objDataWrapper.AddParameter("@EXPOSURE_RATIOACTIVE_NUCLEAR",objGeneralInfo.EXPOSURE_RATIOACTIVE_NUCLEAR);
        //        objDataWrapper.AddParameter("@HAVE_PAST_PRESENT_OPERATIONS",objGeneralInfo.HAVE_PAST_PRESENT_OPERATIONS);
        //        objDataWrapper.AddParameter("@ANY_OPERATIONS_SOLD",objGeneralInfo.ANY_OPERATIONS_SOLD);
        //        objDataWrapper.AddParameter("@MACHINERY_LOANED",objGeneralInfo.MACHINERY_LOANED);
        //        objDataWrapper.AddParameter("@ANY_WATERCRAFT_LEASED",objGeneralInfo.ANY_WATERCRAFT_LEASED);
        //        objDataWrapper.AddParameter("@ANY_PARKING_OWNED",objGeneralInfo.ANY_PARKING_OWNED);
        //        objDataWrapper.AddParameter("@FEE_CHARGED_PARKING",objGeneralInfo.FEE_CHARGED_PARKING);
        //        objDataWrapper.AddParameter("@RECREATION_PROVIDED",objGeneralInfo.RECREATION_PROVIDED);
        //        objDataWrapper.AddParameter("@SWIMMING_POOL_PREMISES",objGeneralInfo.SWIMMING_POOL_PREMISES);
        //        objDataWrapper.AddParameter("@SPORTING_EVENT_SPONSORED",objGeneralInfo.SPORTING_EVENT_SPONSORED);
        //        objDataWrapper.AddParameter("@STRUCTURAL_ALTERATION_CONTEMPATED",objGeneralInfo.STRUCTURAL_ALTERATION_CONTEMPATED);
        //        objDataWrapper.AddParameter("@DEMOLITION_EXPOSURE_CONTEMPLATED",objGeneralInfo.DEMOLITION_EXPOSURE_CONTEMPLATED);
        //        objDataWrapper.AddParameter("@CUSTOMER_ACTIVE_JOINT_VENTURES",objGeneralInfo.CUSTOMER_ACTIVE_JOINT_VENTURES);
        //        objDataWrapper.AddParameter("@LEASE_EMPLOYEE",objGeneralInfo.LEASE_EMPLOYEE);
        //        objDataWrapper.AddParameter("@LABOR_INTERCHANGE_OTH_BUSINESS",objGeneralInfo.LABOR_INTERCHANGE_OTH_BUSINESS);
        //        objDataWrapper.AddParameter("@DAY_CARE_FACILITIES",objGeneralInfo.DAY_CARE_FACILITIES);
        //        objDataWrapper.AddParameter("@IS_ACTIVE",objGeneralInfo.IS_ACTIVE);
        //        objDataWrapper.AddParameter("@CREATED_BY",objGeneralInfo.CREATED_BY);
        //        objDataWrapper.AddParameter("@CREATE_DATETIME",objGeneralInfo.CREATED_DATETIME);
        //        objDataWrapper.AddParameter("@ADDITIONAL_COMMENTS",objGeneralInfo.ADDITIONAL_COMMENTS);
        //        //----------------------Added by Mohit om 11/10/2005----------------------------------.
				
        //        if (objGeneralInfo.INSURANCE_DECLINED_FIVE_YEARS == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_INSURANCE_DECLINED",objGeneralInfo.DESC_INSURANCE_DECLINED);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_INSURANCE_DECLINED",null);					
        //        }				
        //        if (objGeneralInfo.MEDICAL_PROFESSIONAL_EMPLOYEED  == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_MEDICAL_PROFESSIONAL",objGeneralInfo.DESC_MEDICAL_PROFESSIONAL);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_MEDICAL_PROFESSIONAL",null);					
        //        }

				
        //            if (objGeneralInfo.EXPOSURE_RATIOACTIVE_NUCLEAR == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_EXPOSURE_RATIOACTIVE",objGeneralInfo.DESC_EXPOSURE_RATIOACTIVE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_EXPOSURE_RATIOACTIVE",null);					
        //        }


				
        //            if (objGeneralInfo.HAVE_PAST_PRESENT_OPERATIONS == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_HAVE_PAST_PRESENT",objGeneralInfo.DESC_HAVE_PAST_PRESENT);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_HAVE_PAST_PRESENT",null);					
        //        }


				
        //            if (objGeneralInfo.ANY_OPERATIONS_SOLD == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_OPERATIONS",objGeneralInfo.DESC_ANY_OPERATIONS);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_OPERATIONS",null);					
        //        }

				
        //            if (objGeneralInfo.MACHINERY_LOANED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_MACHINERY_LOANED",objGeneralInfo.DESC_MACHINERY_LOANED);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_MACHINERY_LOANED",null);					
        //        }

				
        //            if (objGeneralInfo.ANY_WATERCRAFT_LEASED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_WATERCRAFT",objGeneralInfo.DESC_ANY_WATERCRAFT);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_WATERCRAFT",null);					
        //        }

				
        //            if (objGeneralInfo.ANY_PARKING_OWNED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_PARKING",objGeneralInfo.DESC_ANY_PARKING);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_PARKING",null);					
        //        }

				
        //            if (objGeneralInfo.FEE_CHARGED_PARKING == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_FEE_CHARGED",objGeneralInfo.DESC_FEE_CHARGED);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_FEE_CHARGED",null);					
        //        }
				
				
        //            if (objGeneralInfo.RECREATION_PROVIDED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_RECREATION_PROVIDED",objGeneralInfo.DESC_RECREATION_PROVIDED);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_RECREATION_PROVIDED",null);					
        //        }

				
        //            if (objGeneralInfo.SWIMMING_POOL_PREMISES == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_SWIMMING_POOL",objGeneralInfo.DESC_SWIMMING_POOL);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_SWIMMING_POOL",null);					
        //        }

				
        //            if (objGeneralInfo.SPORTING_EVENT_SPONSORED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_SPORTING_EVENT",objGeneralInfo.DESC_SPORTING_EVENT);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_SPORTING_EVENT",null);					
        //        }
				
				
        //            if (objGeneralInfo.STRUCTURAL_ALTERATION_CONTEMPATED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_STRUCTURAL_ALTERATION",objGeneralInfo.DESC_STRUCTURAL_ALTERATION);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_STRUCTURAL_ALTERATION",null);					
        //        }

				
        //            if (objGeneralInfo.DEMOLITION_EXPOSURE_CONTEMPLATED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_DEMOLITION_EXPOSURE",objGeneralInfo.DESC_DEMOLITION_EXPOSURE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_DEMOLITION_EXPOSURE",null);					
        //        }

				
        //            if (objGeneralInfo.CUSTOMER_ACTIVE_JOINT_VENTURES == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_CUSTOMER_ACTIVE",objGeneralInfo.DESC_CUSTOMER_ACTIVE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_CUSTOMER_ACTIVE",null);					
        //        }

				
        //            if (objGeneralInfo.LEASE_EMPLOYEE == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_LEASE_EMPLOYEE",objGeneralInfo.DESC_LEASE_EMPLOYEE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_LEASE_EMPLOYEE",null);					
        //        }
				
				
        //            if (objGeneralInfo.LABOR_INTERCHANGE_OTH_BUSINESS == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_LABOR_INTERCHANGE",objGeneralInfo.DESC_LABOR_INTERCHANGE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_LABOR_INTERCHANGE",null);					
        //        }

				
        //            if (objGeneralInfo.DAY_CARE_FACILITIES == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_DAY_CARE",objGeneralInfo.DESC_DAY_CARE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_DAY_CARE",null);					
        //        }

        //        //-----------------------------End----------------------------------------------------.

        //        SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID,SqlDbType.Int,ParameterDirection.Input);

        //        int returnResult = 0;
        //        if(TransactionLogRequired)
        //        {
        //            objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/GeneralInformation.aspx.resx");
        //            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //            string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            objTransactionInfo.TRANS_TYPE_ID	=	1;
        //            objTransactionInfo.RECORDED_BY		=	objGeneralInfo.CREATED_BY;
        //            objTransactionInfo.CLIENT_ID		=   objGeneralInfo.CUSTOMER_ID; 
        //            objTransactionInfo.APP_ID			=	objGeneralInfo.APP_ID;
        //            objTransactionInfo.APP_VERSION_ID	=	objGeneralInfo.APP_VERSION_ID;
        //            objTransactionInfo.TRANS_DESC		=	"Underwriting  Questions Has Been Added";
        //            //Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
        //            //							worked upon as well as Screen Name
        //            //objTransactionInfo.CUSTOM_INFO		=	";LOB = " + "General Liability" + ";Screen Name = " +  "Underwriting Questions";
        //            objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Underwriting Questions";
        //            objTransactionInfo.CHANGE_XML		=	strTranXML;
        //            //Executing the query
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //        }
        //        else
        //        {
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
        //        }
        //        int CUSTOMER_ID = int.Parse(objSqlParameter.Value.ToString());
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //        if (CUSTOMER_ID == -1)
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            objGeneralInfo.CUSTOMER_ID = CUSTOMER_ID;
        //            return returnResult;
        //        }
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
        //#endregion

        //#region Update method
        ///// <summary>
        ///// Update method that recieves Model object to save.
        ///// </summary>
        ///// <param name="objOldGeneralInfo">Model object having old information</param>
        ///// <param name="objGeneralInfo">Model object having new information(form control's value)</param>
        ///// <returns>No. of rows updated (1 or 0)</returns>
        //public int Update(ClsGeneralUnderwritingInfo objOldGeneralInfo,ClsGeneralUnderwritingInfo objGeneralInfo)
        //{
        //    string strTranXML;
        //    int returnResult = 0;
        //    string		strStoredProc	=	"Proc_UpdateGeneralUnderWriting";
        //    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
        //    try 
        //    {
        //        objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@INSURANCE_DECLINED_FIVE_YEARS",objGeneralInfo.INSURANCE_DECLINED_FIVE_YEARS);
        //        objDataWrapper.AddParameter("@MEDICAL_PROFESSIONAL_EMPLOYEED",objGeneralInfo.MEDICAL_PROFESSIONAL_EMPLOYEED);
        //        objDataWrapper.AddParameter("@EXPOSURE_RATIOACTIVE_NUCLEAR",objGeneralInfo.EXPOSURE_RATIOACTIVE_NUCLEAR);
        //        objDataWrapper.AddParameter("@HAVE_PAST_PRESENT_OPERATIONS",objGeneralInfo.HAVE_PAST_PRESENT_OPERATIONS);
        //        objDataWrapper.AddParameter("@ANY_OPERATIONS_SOLD",objGeneralInfo.ANY_OPERATIONS_SOLD);
        //        objDataWrapper.AddParameter("@MACHINERY_LOANED",objGeneralInfo.MACHINERY_LOANED);
        //        objDataWrapper.AddParameter("@ANY_WATERCRAFT_LEASED",objGeneralInfo.ANY_WATERCRAFT_LEASED);
        //        objDataWrapper.AddParameter("@ANY_PARKING_OWNED",objGeneralInfo.ANY_PARKING_OWNED);
        //        objDataWrapper.AddParameter("@FEE_CHARGED_PARKING",objGeneralInfo.FEE_CHARGED_PARKING);
        //        objDataWrapper.AddParameter("@RECREATION_PROVIDED",objGeneralInfo.RECREATION_PROVIDED);
        //        objDataWrapper.AddParameter("@SWIMMING_POOL_PREMISES",objGeneralInfo.SWIMMING_POOL_PREMISES);
        //        objDataWrapper.AddParameter("@SPORTING_EVENT_SPONSORED",objGeneralInfo.SPORTING_EVENT_SPONSORED);
        //        objDataWrapper.AddParameter("@STRUCTURAL_ALTERATION_CONTEMPATED",objGeneralInfo.STRUCTURAL_ALTERATION_CONTEMPATED);
        //        objDataWrapper.AddParameter("@DEMOLITION_EXPOSURE_CONTEMPLATED",objGeneralInfo.DEMOLITION_EXPOSURE_CONTEMPLATED);
        //        objDataWrapper.AddParameter("@CUSTOMER_ACTIVE_JOINT_VENTURES",objGeneralInfo.CUSTOMER_ACTIVE_JOINT_VENTURES);
        //        objDataWrapper.AddParameter("@LEASE_EMPLOYEE",objGeneralInfo.LEASE_EMPLOYEE);
        //        objDataWrapper.AddParameter("@LABOR_INTERCHANGE_OTH_BUSINESS",objGeneralInfo.LABOR_INTERCHANGE_OTH_BUSINESS);
        //        objDataWrapper.AddParameter("@DAY_CARE_FACILITIES",objGeneralInfo.DAY_CARE_FACILITIES);
        //        objDataWrapper.AddParameter("@IS_ACTIVE",objGeneralInfo.IS_ACTIVE);
        //        objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralInfo.MODIFIED_BY);
        //        objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralInfo.LAST_UPDATED_DATETIME);
        //        objDataWrapper.AddParameter("@ADDITIONAL_COMMENTS",objGeneralInfo.ADDITIONAL_COMMENTS);
        //        //----------------------Added by Mohit om 11/10/2005----------------------------------.
				
        //        if (objGeneralInfo.INSURANCE_DECLINED_FIVE_YEARS == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_INSURANCE_DECLINED",objGeneralInfo.DESC_INSURANCE_DECLINED);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_INSURANCE_DECLINED",null);					
        //        }				
        //        if (objGeneralInfo.MEDICAL_PROFESSIONAL_EMPLOYEED  == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_MEDICAL_PROFESSIONAL",objGeneralInfo.DESC_MEDICAL_PROFESSIONAL);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_MEDICAL_PROFESSIONAL",null);					
        //        }

				
        //        if (objGeneralInfo.EXPOSURE_RATIOACTIVE_NUCLEAR == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_EXPOSURE_RATIOACTIVE",objGeneralInfo.DESC_EXPOSURE_RATIOACTIVE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_EXPOSURE_RATIOACTIVE",null);					
        //        }


				
        //        if (objGeneralInfo.HAVE_PAST_PRESENT_OPERATIONS == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_HAVE_PAST_PRESENT",objGeneralInfo.DESC_HAVE_PAST_PRESENT);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_HAVE_PAST_PRESENT",null);					
        //        }


				
        //        if (objGeneralInfo.ANY_OPERATIONS_SOLD == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_OPERATIONS",objGeneralInfo.DESC_ANY_OPERATIONS);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_OPERATIONS",null);					
        //        }

				
        //        if (objGeneralInfo.MACHINERY_LOANED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_MACHINERY_LOANED",objGeneralInfo.DESC_MACHINERY_LOANED);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_MACHINERY_LOANED",null);					
        //        }

				
        //        if (objGeneralInfo.ANY_WATERCRAFT_LEASED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_WATERCRAFT",objGeneralInfo.DESC_ANY_WATERCRAFT);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_WATERCRAFT",null);					
        //        }

				
        //        if (objGeneralInfo.ANY_PARKING_OWNED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_PARKING",objGeneralInfo.DESC_ANY_PARKING);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_ANY_PARKING",null);					
        //        }

				
        //        if (objGeneralInfo.FEE_CHARGED_PARKING == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_FEE_CHARGED",objGeneralInfo.DESC_FEE_CHARGED);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_FEE_CHARGED",null);					
        //        }
				
				
        //        if (objGeneralInfo.RECREATION_PROVIDED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_RECREATION_PROVIDED",objGeneralInfo.DESC_RECREATION_PROVIDED);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_RECREATION_PROVIDED",null);					
        //        }

				
        //        if (objGeneralInfo.SWIMMING_POOL_PREMISES == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_SWIMMING_POOL",objGeneralInfo.DESC_SWIMMING_POOL);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_SWIMMING_POOL",null);					
        //        }

				
        //        if (objGeneralInfo.SPORTING_EVENT_SPONSORED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_SPORTING_EVENT",objGeneralInfo.DESC_SPORTING_EVENT);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_SPORTING_EVENT",null);					
        //        }
				
				
        //        if (objGeneralInfo.STRUCTURAL_ALTERATION_CONTEMPATED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_STRUCTURAL_ALTERATION",objGeneralInfo.DESC_STRUCTURAL_ALTERATION);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_STRUCTURAL_ALTERATION",null);					
        //        }

				
        //        if (objGeneralInfo.DEMOLITION_EXPOSURE_CONTEMPLATED == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_DEMOLITION_EXPOSURE",objGeneralInfo.DESC_DEMOLITION_EXPOSURE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_DEMOLITION_EXPOSURE",null);					
        //        }

				
        //        if (objGeneralInfo.CUSTOMER_ACTIVE_JOINT_VENTURES == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_CUSTOMER_ACTIVE",objGeneralInfo.DESC_CUSTOMER_ACTIVE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_CUSTOMER_ACTIVE",null);					
        //        }

				
        //        if (objGeneralInfo.LEASE_EMPLOYEE == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_LEASE_EMPLOYEE",objGeneralInfo.DESC_LEASE_EMPLOYEE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_LEASE_EMPLOYEE",null);					
        //        }
				
				
        //        if (objGeneralInfo.LABOR_INTERCHANGE_OTH_BUSINESS == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_LABOR_INTERCHANGE",objGeneralInfo.DESC_LABOR_INTERCHANGE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_LABOR_INTERCHANGE",null);					
        //        }

				
        //        if (objGeneralInfo.DAY_CARE_FACILITIES == "Y")
        //        {
        //            objDataWrapper.AddParameter("@DESC_DAY_CARE",objGeneralInfo.DESC_DAY_CARE);
        //        }
        //        else
        //        {
        //            objDataWrapper.AddParameter("@DESC_DAY_CARE",null);					
        //        }

        //        //-----------------------------End----------------------------------------------------.



        //        if(TransactionLogRequired) 
        //        {
        //            //string strUpdate = objBuilder.GetUpdateSQL(objOldGeneralInfo,objGeneralInfo,out strTranXML);

        //            objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/GeneralInformation.aspx.resx");
        //            strTranXML	=	objBuilder.GetTransactionLogXML(objOldGeneralInfo,objGeneralInfo);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
        //                returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
        //            else
        //            {
        //                objTransactionInfo.TRANS_TYPE_ID	=	3;
        //                objTransactionInfo.RECORDED_BY		=	objGeneralInfo.MODIFIED_BY;
        //                objTransactionInfo.CLIENT_ID		=   objGeneralInfo.CUSTOMER_ID; 
        //                objTransactionInfo.APP_ID			=	objGeneralInfo.APP_ID;
        //                objTransactionInfo.APP_VERSION_ID	=	objGeneralInfo.APP_VERSION_ID;
        //                objTransactionInfo.TRANS_DESC		=	"Underwriting Questions Has Been Updated";
        //                //Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
        //                //							worked upon as well as Screen Name
        //                //objTransactionInfo.CUSTOM_INFO		=	";LOB = " + "General Liability" + ";Screen Name = " +  "Underwriting Questions";
        //                objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Underwriting Questions";
        //                objTransactionInfo.CHANGE_XML		=	strTranXML;
        //                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //            }
        //        }
        //        else
        //        {
        //            returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
        //        }
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //        return returnResult;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw(ex);
        //    }
        //    finally
        //    {
        //        if(objDataWrapper != null) 
        //        {
        //            objDataWrapper.Dispose();
        //        }
        //        if(objBuilder != null) 
        //        {
        //            objBuilder = null;
        //        }
        //    }
        //}
        //#endregion

		public static string GetXmlForControls(int CustId, int AppId, int AppVerId)
		{
			string strProcName = "Proc_GetGeneralUnderWriting";
			DataSet dsLocationInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustId);
				objDataWrapper.AddParameter("@APP_ID",AppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVerId);
				

				dsLocationInfo = objDataWrapper.ExecuteDataSet(strProcName);
				
				if (dsLocationInfo.Tables[0].Rows.Count != 0)
				{
					return ClsCommon.GetXMLEncoded(dsLocationInfo.Tables[0]);
					//return dsLocationInfo.GetXml();
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


		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objGeneralInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddPolicyGenLib(Cms.Model.Policy.GeneralLiability.ClsGeneralInfo objGeneralInfo)
		{
			string		strStoredProc	=	"Proc_InsertGeneralUnderWriting";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@POLICY_ID",objGeneralInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objGeneralInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@INSURANCE_DECLINED_FIVE_YEARS",objGeneralInfo.INSURANCE_DECLINED_FIVE_YEARS);
				objDataWrapper.AddParameter("@MEDICAL_PROFESSIONAL_EMPLOYEED",objGeneralInfo.MEDICAL_PROFESSIONAL_EMPLOYEED);
				objDataWrapper.AddParameter("@EXPOSURE_RATIOACTIVE_NUCLEAR",objGeneralInfo.EXPOSURE_RATIOACTIVE_NUCLEAR);
				objDataWrapper.AddParameter("@HAVE_PAST_PRESENT_OPERATIONS",objGeneralInfo.HAVE_PAST_PRESENT_OPERATIONS);
				objDataWrapper.AddParameter("@ANY_OPERATIONS_SOLD",objGeneralInfo.ANY_OPERATIONS_SOLD);
				objDataWrapper.AddParameter("@MACHINERY_LOANED",objGeneralInfo.MACHINERY_LOANED);
				objDataWrapper.AddParameter("@ANY_WATERCRAFT_LEASED",objGeneralInfo.ANY_WATERCRAFT_LEASED);
				objDataWrapper.AddParameter("@ANY_PARKING_OWNED",objGeneralInfo.ANY_PARKING_OWNED);
				objDataWrapper.AddParameter("@FEE_CHARGED_PARKING",objGeneralInfo.FEE_CHARGED_PARKING);
				objDataWrapper.AddParameter("@RECREATION_PROVIDED",objGeneralInfo.RECREATION_PROVIDED);
				objDataWrapper.AddParameter("@SWIMMING_POOL_PREMISES",objGeneralInfo.SWIMMING_POOL_PREMISES);
				objDataWrapper.AddParameter("@SPORTING_EVENT_SPONSORED",objGeneralInfo.SPORTING_EVENT_SPONSORED);
				objDataWrapper.AddParameter("@STRUCTURAL_ALTERATION_CONTEMPATED",objGeneralInfo.STRUCTURAL_ALTERATION_CONTEMPATED);
				objDataWrapper.AddParameter("@DEMOLITION_EXPOSURE_CONTEMPLATED",objGeneralInfo.DEMOLITION_EXPOSURE_CONTEMPLATED);
				objDataWrapper.AddParameter("@CUSTOMER_ACTIVE_JOINT_VENTURES",objGeneralInfo.CUSTOMER_ACTIVE_JOINT_VENTURES);
				objDataWrapper.AddParameter("@LEASE_EMPLOYEE",objGeneralInfo.LEASE_EMPLOYEE);
				objDataWrapper.AddParameter("@LABOR_INTERCHANGE_OTH_BUSINESS",objGeneralInfo.LABOR_INTERCHANGE_OTH_BUSINESS);
				objDataWrapper.AddParameter("@DAY_CARE_FACILITIES",objGeneralInfo.DAY_CARE_FACILITIES);
				objDataWrapper.AddParameter("@IS_ACTIVE",objGeneralInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objGeneralInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATE_DATETIME",objGeneralInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@ADDITIONAL_COMMENTS",objGeneralInfo.ADDITIONAL_COMMENTS);
				//----------------------Added by Mohit om 11/10/2005----------------------------------.
				
				if (objGeneralInfo.INSURANCE_DECLINED_FIVE_YEARS == "Y")
				{
					objDataWrapper.AddParameter("@DESC_INSURANCE_DECLINED",objGeneralInfo.DESC_INSURANCE_DECLINED);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_INSURANCE_DECLINED",null);					
				}				
				if (objGeneralInfo.MEDICAL_PROFESSIONAL_EMPLOYEED  == "Y")
				{
					objDataWrapper.AddParameter("@DESC_MEDICAL_PROFESSIONAL",objGeneralInfo.DESC_MEDICAL_PROFESSIONAL);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_MEDICAL_PROFESSIONAL",null);					
				}

				
				if (objGeneralInfo.EXPOSURE_RATIOACTIVE_NUCLEAR == "Y")
				{
					objDataWrapper.AddParameter("@DESC_EXPOSURE_RATIOACTIVE",objGeneralInfo.DESC_EXPOSURE_RATIOACTIVE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_EXPOSURE_RATIOACTIVE",null);					
				}


				
				if (objGeneralInfo.HAVE_PAST_PRESENT_OPERATIONS == "Y")
				{
					objDataWrapper.AddParameter("@DESC_HAVE_PAST_PRESENT",objGeneralInfo.DESC_HAVE_PAST_PRESENT);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_HAVE_PAST_PRESENT",null);					
				}


				
				if (objGeneralInfo.ANY_OPERATIONS_SOLD == "Y")
				{
					objDataWrapper.AddParameter("@DESC_ANY_OPERATIONS",objGeneralInfo.DESC_ANY_OPERATIONS);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_ANY_OPERATIONS",null);					
				}

				
				if (objGeneralInfo.MACHINERY_LOANED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_MACHINERY_LOANED",objGeneralInfo.DESC_MACHINERY_LOANED);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_MACHINERY_LOANED",null);					
				}

				
				if (objGeneralInfo.ANY_WATERCRAFT_LEASED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_ANY_WATERCRAFT",objGeneralInfo.DESC_ANY_WATERCRAFT);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_ANY_WATERCRAFT",null);					
				}

				
				if (objGeneralInfo.ANY_PARKING_OWNED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_ANY_PARKING",objGeneralInfo.DESC_ANY_PARKING);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_ANY_PARKING",null);					
				}

				
				if (objGeneralInfo.FEE_CHARGED_PARKING == "Y")
				{
					objDataWrapper.AddParameter("@DESC_FEE_CHARGED",objGeneralInfo.DESC_FEE_CHARGED);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_FEE_CHARGED",null);					
				}
				
				
				if (objGeneralInfo.RECREATION_PROVIDED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_RECREATION_PROVIDED",objGeneralInfo.DESC_RECREATION_PROVIDED);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_RECREATION_PROVIDED",null);					
				}

				
				if (objGeneralInfo.SWIMMING_POOL_PREMISES == "Y")
				{
					objDataWrapper.AddParameter("@DESC_SWIMMING_POOL",objGeneralInfo.DESC_SWIMMING_POOL);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_SWIMMING_POOL",null);					
				}

				
				if (objGeneralInfo.SPORTING_EVENT_SPONSORED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_SPORTING_EVENT",objGeneralInfo.DESC_SPORTING_EVENT);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_SPORTING_EVENT",null);					
				}
				
				
				if (objGeneralInfo.STRUCTURAL_ALTERATION_CONTEMPATED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_STRUCTURAL_ALTERATION",objGeneralInfo.DESC_STRUCTURAL_ALTERATION);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_STRUCTURAL_ALTERATION",null);					
				}

				
				if (objGeneralInfo.DEMOLITION_EXPOSURE_CONTEMPLATED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_DEMOLITION_EXPOSURE",objGeneralInfo.DESC_DEMOLITION_EXPOSURE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_DEMOLITION_EXPOSURE",null);					
				}

				
				if (objGeneralInfo.CUSTOMER_ACTIVE_JOINT_VENTURES == "Y")
				{
					objDataWrapper.AddParameter("@DESC_CUSTOMER_ACTIVE",objGeneralInfo.DESC_CUSTOMER_ACTIVE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_CUSTOMER_ACTIVE",null);					
				}

				
				if (objGeneralInfo.LEASE_EMPLOYEE == "Y")
				{
					objDataWrapper.AddParameter("@DESC_LEASE_EMPLOYEE",objGeneralInfo.DESC_LEASE_EMPLOYEE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_LEASE_EMPLOYEE",null);					
				}
				
				
				if (objGeneralInfo.LABOR_INTERCHANGE_OTH_BUSINESS == "Y")
				{
					objDataWrapper.AddParameter("@DESC_LABOR_INTERCHANGE",objGeneralInfo.DESC_LABOR_INTERCHANGE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_LABOR_INTERCHANGE",null);					
				}

				
				if (objGeneralInfo.DAY_CARE_FACILITIES == "Y")
				{
					objDataWrapper.AddParameter("@DESC_DAY_CARE",objGeneralInfo.DESC_DAY_CARE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_DAY_CARE",null);					
				}

				//-----------------------------End----------------------------------------------------.

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID,SqlDbType.Int,ParameterDirection.Input);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/GeneralInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objGeneralInfo.CREATED_BY;
					objTransactionInfo.CLIENT_ID		=   objGeneralInfo.CUSTOMER_ID; 
					objTransactionInfo.POLICY_ID			=	objGeneralInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objGeneralInfo.POLICY_VERSION_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1721", "");// "Underwriting  Questions Has Been Added";					
					objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Underwriting Questions";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int CUSTOMER_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (CUSTOMER_ID == -1)
				{
					return -1;
				}
				else
				{
					objGeneralInfo.CUSTOMER_ID = CUSTOMER_ID;
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
		/// <param name="objOldGeneralInfo">Model object having old information</param>
		/// <param name="objGeneralInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicyGenLib(Cms.Model.Policy.GeneralLiability.ClsGeneralInfo objOldGeneralInfo,Cms.Model.Policy.GeneralLiability.ClsGeneralInfo objGeneralInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdatePolicyGeneralUnderWriting";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objGeneralInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objGeneralInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@INSURANCE_DECLINED_FIVE_YEARS",objGeneralInfo.INSURANCE_DECLINED_FIVE_YEARS);
				objDataWrapper.AddParameter("@MEDICAL_PROFESSIONAL_EMPLOYEED",objGeneralInfo.MEDICAL_PROFESSIONAL_EMPLOYEED);
				objDataWrapper.AddParameter("@EXPOSURE_RATIOACTIVE_NUCLEAR",objGeneralInfo.EXPOSURE_RATIOACTIVE_NUCLEAR);
				objDataWrapper.AddParameter("@HAVE_PAST_PRESENT_OPERATIONS",objGeneralInfo.HAVE_PAST_PRESENT_OPERATIONS);
				objDataWrapper.AddParameter("@ANY_OPERATIONS_SOLD",objGeneralInfo.ANY_OPERATIONS_SOLD);
				objDataWrapper.AddParameter("@MACHINERY_LOANED",objGeneralInfo.MACHINERY_LOANED);
				objDataWrapper.AddParameter("@ANY_WATERCRAFT_LEASED",objGeneralInfo.ANY_WATERCRAFT_LEASED);
				objDataWrapper.AddParameter("@ANY_PARKING_OWNED",objGeneralInfo.ANY_PARKING_OWNED);
				objDataWrapper.AddParameter("@FEE_CHARGED_PARKING",objGeneralInfo.FEE_CHARGED_PARKING);
				objDataWrapper.AddParameter("@RECREATION_PROVIDED",objGeneralInfo.RECREATION_PROVIDED);
				objDataWrapper.AddParameter("@SWIMMING_POOL_PREMISES",objGeneralInfo.SWIMMING_POOL_PREMISES);
				objDataWrapper.AddParameter("@SPORTING_EVENT_SPONSORED",objGeneralInfo.SPORTING_EVENT_SPONSORED);
				objDataWrapper.AddParameter("@STRUCTURAL_ALTERATION_CONTEMPATED",objGeneralInfo.STRUCTURAL_ALTERATION_CONTEMPATED);
				objDataWrapper.AddParameter("@DEMOLITION_EXPOSURE_CONTEMPLATED",objGeneralInfo.DEMOLITION_EXPOSURE_CONTEMPLATED);
				objDataWrapper.AddParameter("@CUSTOMER_ACTIVE_JOINT_VENTURES",objGeneralInfo.CUSTOMER_ACTIVE_JOINT_VENTURES);
				objDataWrapper.AddParameter("@LEASE_EMPLOYEE",objGeneralInfo.LEASE_EMPLOYEE);
				objDataWrapper.AddParameter("@LABOR_INTERCHANGE_OTH_BUSINESS",objGeneralInfo.LABOR_INTERCHANGE_OTH_BUSINESS);
				objDataWrapper.AddParameter("@DAY_CARE_FACILITIES",objGeneralInfo.DAY_CARE_FACILITIES);
				objDataWrapper.AddParameter("@IS_ACTIVE",objGeneralInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@ADDITIONAL_COMMENTS",objGeneralInfo.ADDITIONAL_COMMENTS);
				//----------------------Added by Mohit om 11/10/2005----------------------------------.
				
				if (objGeneralInfo.INSURANCE_DECLINED_FIVE_YEARS == "Y")
				{
					objDataWrapper.AddParameter("@DESC_INSURANCE_DECLINED",objGeneralInfo.DESC_INSURANCE_DECLINED);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_INSURANCE_DECLINED",null);					
				}				
				if (objGeneralInfo.MEDICAL_PROFESSIONAL_EMPLOYEED  == "Y")
				{
					objDataWrapper.AddParameter("@DESC_MEDICAL_PROFESSIONAL",objGeneralInfo.DESC_MEDICAL_PROFESSIONAL);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_MEDICAL_PROFESSIONAL",null);					
				}

				
				if (objGeneralInfo.EXPOSURE_RATIOACTIVE_NUCLEAR == "Y")
				{
					objDataWrapper.AddParameter("@DESC_EXPOSURE_RATIOACTIVE",objGeneralInfo.DESC_EXPOSURE_RATIOACTIVE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_EXPOSURE_RATIOACTIVE",null);					
				}


				
				if (objGeneralInfo.HAVE_PAST_PRESENT_OPERATIONS == "Y")
				{
					objDataWrapper.AddParameter("@DESC_HAVE_PAST_PRESENT",objGeneralInfo.DESC_HAVE_PAST_PRESENT);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_HAVE_PAST_PRESENT",null);					
				}


				
				if (objGeneralInfo.ANY_OPERATIONS_SOLD == "Y")
				{
					objDataWrapper.AddParameter("@DESC_ANY_OPERATIONS",objGeneralInfo.DESC_ANY_OPERATIONS);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_ANY_OPERATIONS",null);					
				}

				
				if (objGeneralInfo.MACHINERY_LOANED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_MACHINERY_LOANED",objGeneralInfo.DESC_MACHINERY_LOANED);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_MACHINERY_LOANED",null);					
				}

				
				if (objGeneralInfo.ANY_WATERCRAFT_LEASED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_ANY_WATERCRAFT",objGeneralInfo.DESC_ANY_WATERCRAFT);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_ANY_WATERCRAFT",null);					
				}

				
				if (objGeneralInfo.ANY_PARKING_OWNED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_ANY_PARKING",objGeneralInfo.DESC_ANY_PARKING);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_ANY_PARKING",null);					
				}

				
				if (objGeneralInfo.FEE_CHARGED_PARKING == "Y")
				{
					objDataWrapper.AddParameter("@DESC_FEE_CHARGED",objGeneralInfo.DESC_FEE_CHARGED);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_FEE_CHARGED",null);					
				}
				
				
				if (objGeneralInfo.RECREATION_PROVIDED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_RECREATION_PROVIDED",objGeneralInfo.DESC_RECREATION_PROVIDED);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_RECREATION_PROVIDED",null);					
				}

				
				if (objGeneralInfo.SWIMMING_POOL_PREMISES == "Y")
				{
					objDataWrapper.AddParameter("@DESC_SWIMMING_POOL",objGeneralInfo.DESC_SWIMMING_POOL);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_SWIMMING_POOL",null);					
				}

				
				if (objGeneralInfo.SPORTING_EVENT_SPONSORED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_SPORTING_EVENT",objGeneralInfo.DESC_SPORTING_EVENT);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_SPORTING_EVENT",null);					
				}
				
				
				if (objGeneralInfo.STRUCTURAL_ALTERATION_CONTEMPATED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_STRUCTURAL_ALTERATION",objGeneralInfo.DESC_STRUCTURAL_ALTERATION);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_STRUCTURAL_ALTERATION",null);					
				}

				
				if (objGeneralInfo.DEMOLITION_EXPOSURE_CONTEMPLATED == "Y")
				{
					objDataWrapper.AddParameter("@DESC_DEMOLITION_EXPOSURE",objGeneralInfo.DESC_DEMOLITION_EXPOSURE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_DEMOLITION_EXPOSURE",null);					
				}

				
				if (objGeneralInfo.CUSTOMER_ACTIVE_JOINT_VENTURES == "Y")
				{
					objDataWrapper.AddParameter("@DESC_CUSTOMER_ACTIVE",objGeneralInfo.DESC_CUSTOMER_ACTIVE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_CUSTOMER_ACTIVE",null);					
				}

				
				if (objGeneralInfo.LEASE_EMPLOYEE == "Y")
				{
					objDataWrapper.AddParameter("@DESC_LEASE_EMPLOYEE",objGeneralInfo.DESC_LEASE_EMPLOYEE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_LEASE_EMPLOYEE",null);					
				}
				
				
				if (objGeneralInfo.LABOR_INTERCHANGE_OTH_BUSINESS == "Y")
				{
					objDataWrapper.AddParameter("@DESC_LABOR_INTERCHANGE",objGeneralInfo.DESC_LABOR_INTERCHANGE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_LABOR_INTERCHANGE",null);					
				}

				
				if (objGeneralInfo.DAY_CARE_FACILITIES == "Y")
				{
					objDataWrapper.AddParameter("@DESC_DAY_CARE",objGeneralInfo.DESC_DAY_CARE);
				}
				else
				{
					objDataWrapper.AddParameter("@DESC_DAY_CARE",null);					
				}

				//-----------------------------End----------------------------------------------------.



				if(TransactionLogRequired) 
				{
					//string strUpdate = objBuilder.GetUpdateSQL(objOldGeneralInfo,objGeneralInfo,out strTranXML);

					objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/GeneralInformation.aspx.resx");
					strTranXML	=	objBuilder.GetTransactionLogXML(objOldGeneralInfo,objGeneralInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objGeneralInfo.MODIFIED_BY;
						objTransactionInfo.CLIENT_ID		=   objGeneralInfo.CUSTOMER_ID; 
						objTransactionInfo.POLICY_ID			=	objGeneralInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	objGeneralInfo.POLICY_VERSION_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1722", "");// "Underwriting Questions Has Been Updated";						
						objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Underwriting Questions";
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

		public static string GetPolicyXmlForControls(int intCustId, int intPolicyId, int intPolicyVersionId)
		{
			string strProcName = "Proc_GetPolicyGeneralUnderWriting";
			DataSet dsLocationInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustId);
				objDataWrapper.AddParameter("@POLICY_ID",intPolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionId);
				

				dsLocationInfo = objDataWrapper.ExecuteDataSet(strProcName);
				
				if (dsLocationInfo.Tables[0].Rows.Count != 0)
				{
					return ClsCommon.GetXMLEncoded(dsLocationInfo.Tables[0]);
					//return dsLocationInfo.GetXml();
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

		
	}
}
