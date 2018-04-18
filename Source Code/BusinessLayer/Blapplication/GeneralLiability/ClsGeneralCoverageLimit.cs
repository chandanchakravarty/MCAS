/******************************************************************************************
<Author				: -   Gaurav
<Start Date				: -	8/22/2005 10:57:53 AM
<End Date				: -	
<Description				: - 	This file is used to
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Nov 09,2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Additional Info in the Transaction log to record LOB name and screen name has been added
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Application;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class clsGeneralCoverageLimit : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_GENERAL_COVERAGE_LIMIT_INFO			=	"APP_GENERAL_COVERAGE_LIMIT_INFO";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _CUSTOMER_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_GENERAL_COVERAGE_LIMIT_INFO";
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
		public clsGeneralCoverageLimit()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objGeneralCoverageLimitInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
        /*
		public int Add(ClsGeneralCoverageLimitInfo objGeneralCoverageLimitInfo)
		{
			string		strStoredProc	=	"Proc_InsertGeneralCustomerLimit";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@APP_ID",objGeneralCoverageLimitInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralCoverageLimitInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralCoverageLimitInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",objGeneralCoverageLimitInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@CLASS_CODE",objGeneralCoverageLimitInfo.CLASS_CODE);
				objDataWrapper.AddParameter("@BUSINESS_DESCRIPTION",objGeneralCoverageLimitInfo.BUSINESS_DESCRIPTION);
				//objDataWrapper.AddParameter("@TERRITORY",objGeneralCoverageLimitInfo.TERRITORY);
				objDataWrapper.AddParameter("@COVERAGE_TYPE",objGeneralCoverageLimitInfo.COVERAGE_TYPE);
				objDataWrapper.AddParameter("@COVERAGE_FORM",objGeneralCoverageLimitInfo.COVERAGE_FORM);
				
				if (objGeneralCoverageLimitInfo.COVERAGE_FORM == 11649)
				{
					objDataWrapper.AddParameter("@RETRO_DATE",null);
				}
				else
				{

					if(objGeneralCoverageLimitInfo.RETRO_DATE == DateTime.MinValue)
					{
						objDataWrapper.AddParameter("@RETRO_DATE",null);
					}
					else
					{
						objDataWrapper.AddParameter("@RETRO_DATE",objGeneralCoverageLimitInfo.RETRO_DATE);
					}
				}
				if(objGeneralCoverageLimitInfo.EXTENDED_REPORTING_DATE == DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@EXTENDED_REPORTING_DATE",null);
				}
				else
				{	
					objDataWrapper.AddParameter("@EXTENDED_REPORTING_DATE",objGeneralCoverageLimitInfo.EXTENDED_REPORTING_DATE);
				}
				objDataWrapper.AddParameter("@FREE_TRADE_ZONE",objGeneralCoverageLimitInfo.FREE_TRADE_ZONE);
				//objDataWrapper.AddParameter("@EACH_OCCURRRENCE",objGeneralCoverageLimitInfo.EACH_OCCURRRENCE);
				//objDataWrapper.AddParameter("@DAMAGE_TO_PREMISES",objGeneralCoverageLimitInfo.DAMAGE_TO_PREMISES);
				objDataWrapper.AddParameter("@PERSONAL_ADVERTISING_INJURY",objGeneralCoverageLimitInfo.PERSONAL_ADVERTISING_INJURY);
				//objDataWrapper.AddParameter("@MEDICAL_EXPENSE",objGeneralCoverageLimitInfo.MEDICAL_EXPENSE);
				objDataWrapper.AddParameter("@PRODUCTS_COMPLETED_OPERATIONS",objGeneralCoverageLimitInfo.PRODUCTS_COMPLETED_OPERATIONS);
				//objDataWrapper.AddParameter("@GENERAL_AGGREGATE",objGeneralCoverageLimitInfo.GENERAL_AGGREGATE);
				objDataWrapper.AddParameter("@PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE",objGeneralCoverageLimitInfo.PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE);
				//objDataWrapper.AddParameter("@FIRE_DAMAGE_LEGAL",objGeneralCoverageLimitInfo.FIRE_DAMAGE_LEGAL);
				objDataWrapper.AddParameter("@EMPLOYEE_BENEFITS",objGeneralCoverageLimitInfo.EMPLOYEE_BENEFITS);
				objDataWrapper.AddParameter("@LIMIT_IDENTIFIER",objGeneralCoverageLimitInfo.LIMIT_IDENTIFIER);

				objDataWrapper.AddParameter("@EXPOSURE",objGeneralCoverageLimitInfo.EXPOSURE);
				objDataWrapper.AddParameter("@ADDITIONAL_EXPOSURE",objGeneralCoverageLimitInfo.ADDITIONAL_EXPOSURE);
				objDataWrapper.AddParameter("@RATING_BASE",objGeneralCoverageLimitInfo.RATING_BASE);				
				objDataWrapper.AddParameter("@INSURED_TYPE",objGeneralCoverageLimitInfo.INSURED_TYPE);				
				if(objGeneralCoverageLimitInfo.RATE==-1)
					objDataWrapper.AddParameter("@RATE",System.DBNull.Value);
				else	
					objDataWrapper.AddParameter("@RATE",objGeneralCoverageLimitInfo.RATE);
				

				objDataWrapper.AddParameter("@IS_ACTIVE",objGeneralCoverageLimitInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objGeneralCoverageLimitInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objGeneralCoverageLimitInfo.CREATED_DATETIME);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@COVERAGE_ID",objGeneralCoverageLimitInfo.CUSTOMER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objGeneralCoverageLimitInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/GeneralCoverageLimit.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objGeneralCoverageLimitInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objGeneralCoverageLimitInfo.CREATED_BY;					
					objTransactionInfo.CLIENT_ID		=   objGeneralCoverageLimitInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID			=	objGeneralCoverageLimitInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objGeneralCoverageLimitInfo.APP_VERSION_ID;
					objTransactionInfo.TRANS_DESC		=	"New Coverage Has Been Added";
					//Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
					//							worked upon as well as Screen Name					
					//objTransactionInfo.CUSTOM_INFO = ";LOB = " + "General Liability" + ";Screen Name = " +  "General Coverage Limit";
					objTransactionInfo.CUSTOM_INFO = ";Screen Name = " +  "General Coverage Limit";
						
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int COVERAGE_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (COVERAGE_ID == -1)
				{
					return -1;
				}
				else
				{
					objGeneralCoverageLimitInfo.COVERAGE_ID = COVERAGE_ID;
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
		}*/
		#endregion

        //#region Update method
        ///// <summary>
        ///// Update method that recieves Model object to save.
        ///// </summary>
        ///// <param name="objOldGeneralCoverageLimitInfo">Model object having old information</param>
        ///// <param name="objGeneralCoverageLimitInfo">Model object having new information(form control's value)</param>
        ///// <returns>No. of rows updated (1 or 0)</returns>
        //public int Update(ClsGeneralCoverageLimitInfo objOldGeneralCoverageLimitInfo,ClsGeneralCoverageLimitInfo objGeneralCoverageLimitInfo)
        //{
        //    string		strStoredProc	=	"Proc_UpdateGeneralCoverageLimit";
        //    string strTranXML;
        //    int returnResult = 0;
        //    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
        //    try 
        //    {
        //        objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralCoverageLimitInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@APP_ID",objGeneralCoverageLimitInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralCoverageLimitInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@COVERAGE_ID",objGeneralCoverageLimitInfo.COVERAGE_ID);
        //        objDataWrapper.AddParameter("@LOCATION_ID",objGeneralCoverageLimitInfo.LOCATION_ID);
        //        objDataWrapper.AddParameter("@CLASS_CODE",objGeneralCoverageLimitInfo.CLASS_CODE);
        //        objDataWrapper.AddParameter("@BUSINESS_DESCRIPTION",objGeneralCoverageLimitInfo.BUSINESS_DESCRIPTION);
        //        //objDataWrapper.AddParameter("@TERRITORY",objGeneralCoverageLimitInfo.TERRITORY);
        //        objDataWrapper.AddParameter("@COVERAGE_TYPE",objGeneralCoverageLimitInfo.COVERAGE_TYPE);
        //        objDataWrapper.AddParameter("@COVERAGE_FORM",objGeneralCoverageLimitInfo.COVERAGE_FORM);
        //        objDataWrapper.AddParameter("@INSURED_TYPE",objGeneralCoverageLimitInfo.INSURED_TYPE);
				
				
        //        if (objGeneralCoverageLimitInfo.COVERAGE_FORM == 11649)
        //        {
        //            objDataWrapper.AddParameter("@RETRO_DATE",null);
        //        }
        //        else
        //        {

        //            if(objGeneralCoverageLimitInfo.RETRO_DATE == DateTime.MinValue)
        //            {
        //                objDataWrapper.AddParameter("@RETRO_DATE",null);
        //            }
        //            else
        //            {
        //                objDataWrapper.AddParameter("@RETRO_DATE",objGeneralCoverageLimitInfo.RETRO_DATE);
        //            }
        //        }


        //        if(objGeneralCoverageLimitInfo.EXTENDED_REPORTING_DATE == DateTime.MinValue)
        //        {
        //            objDataWrapper.AddParameter("@EXTENDED_REPORTING_DATE",null);
        //        }
        //        else
        //        {	
        //            objDataWrapper.AddParameter("@EXTENDED_REPORTING_DATE",objGeneralCoverageLimitInfo.EXTENDED_REPORTING_DATE);
        //        }objDataWrapper.AddParameter("@FREE_TRADE_ZONE",objGeneralCoverageLimitInfo.FREE_TRADE_ZONE);
        //        //objDataWrapper.AddParameter("@EACH_OCCURRRENCE",objGeneralCoverageLimitInfo.EACH_OCCURRRENCE);
        //        //objDataWrapper.AddParameter("@DAMAGE_TO_PREMISES",objGeneralCoverageLimitInfo.DAMAGE_TO_PREMISES);
        //        objDataWrapper.AddParameter("@PERSONAL_ADVERTISING_INJURY",objGeneralCoverageLimitInfo.PERSONAL_ADVERTISING_INJURY);
        //        //objDataWrapper.AddParameter("@MEDICAL_EXPENSE",objGeneralCoverageLimitInfo.MEDICAL_EXPENSE);
        //        objDataWrapper.AddParameter("@PRODUCTS_COMPLETED_OPERATIONS",objGeneralCoverageLimitInfo.PRODUCTS_COMPLETED_OPERATIONS);
        //        //objDataWrapper.AddParameter("@GENERAL_AGGREGATE",objGeneralCoverageLimitInfo.GENERAL_AGGREGATE);
        //        objDataWrapper.AddParameter("@PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE",objGeneralCoverageLimitInfo.PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE);
        //        //objDataWrapper.AddParameter("@FIRE_DAMAGE_LEGAL",objGeneralCoverageLimitInfo.FIRE_DAMAGE_LEGAL);
        //        objDataWrapper.AddParameter("@EMPLOYEE_BENEFITS",objGeneralCoverageLimitInfo.EMPLOYEE_BENEFITS);
        //        objDataWrapper.AddParameter("@LIMIT_IDENTIFIER",objGeneralCoverageLimitInfo.LIMIT_IDENTIFIER);
				
        //        objDataWrapper.AddParameter("@EXPOSURE",objGeneralCoverageLimitInfo.EXPOSURE);
        //        objDataWrapper.AddParameter("@ADDITIONAL_EXPOSURE",objGeneralCoverageLimitInfo.ADDITIONAL_EXPOSURE);
        //        objDataWrapper.AddParameter("@RATING_BASE",objGeneralCoverageLimitInfo.RATING_BASE);
				
        //        //objDataWrapper.AddParameter("@RATE",objGeneralCoverageLimitInfo.RATE);
        //        if(objGeneralCoverageLimitInfo.RATE==-1)
        //            objDataWrapper.AddParameter("@RATE",System.DBNull.Value);
        //        else	
        //            objDataWrapper.AddParameter("@RATE",objGeneralCoverageLimitInfo.RATE);
				

        //        objDataWrapper.AddParameter("@IS_ACTIVE",objGeneralCoverageLimitInfo.IS_ACTIVE);
				
        //        objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralCoverageLimitInfo.MODIFIED_BY);
        //        objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralCoverageLimitInfo.LAST_UPDATED_DATETIME);
        //        if(base.TransactionLogRequired) 
        //        {
        //            objGeneralCoverageLimitInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/GeneralCoverageLimit.aspx.resx");
        //            objBuilder.GetUpdateSQL(objOldGeneralCoverageLimitInfo,objGeneralCoverageLimitInfo,out strTranXML);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
        //                returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
        //            else
        //            {
        //                objTransactionInfo.TRANS_TYPE_ID	=	3;
        //                objTransactionInfo.RECORDED_BY		=	objGeneralCoverageLimitInfo.MODIFIED_BY;
        //                objTransactionInfo.CLIENT_ID		=   objGeneralCoverageLimitInfo.CUSTOMER_ID;
        //                objTransactionInfo.TRANS_DESC		=	"Coverage Has Been Updated";
        //                objTransactionInfo.APP_ID			=	objGeneralCoverageLimitInfo.APP_ID;
        //                objTransactionInfo.APP_VERSION_ID	=	objGeneralCoverageLimitInfo.APP_VERSION_ID;
        //                //Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
        //                //							worked upon as well as Screen Name					
        //                //objTransactionInfo.CUSTOM_INFO = ";LOB = " + "General Liability" + ";Screen Name = " +  "General Coverage Limit";
        //                objTransactionInfo.CUSTOM_INFO = ";Screen Name = " +  "General Coverage Limit";
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

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(int CUSTOMER_ID, int APP_ID, int APP_VERSION_ID)
		{
			string strSql = "Proc_GetXMLAPP_GENERAL_COVERAGE_LIMIT_INFO";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);
			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count > 0)
				//return objDataSet.GetXml();
				return ClsCommon.GetXMLEncoded(objDataSet.Tables[0]);
			else
				return "";
		}
		#endregion
	}
}
