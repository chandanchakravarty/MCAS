/******************************************************************************************
<Author					: -   Sumit Chhabra
<Start Date				: -	03/29/2006
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  Ravindra
<Modified By			: -  April 04-2006
<Purpose				: -  Added plicy level functions
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
//using Cms.Model.Application.GeneralLiability;

namespace Cms.BusinessLayer.BlApplication.GeneralLiability
{
	/// <summary>
	/// Business Logic for Locations
	/// </summary>
	public class ClsGeneralLiabilityDetails : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_GENERAL_LIABILITY_DETAILS			=	"APP_GENERAL_LIABILITY_DETAILS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
	
		//private const string ACTIVATE_DEACTIVATE_PROC	= "";
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

		#region public Utility Functions
		public static string GetLocations(int CustomerID, int AppID, int AppVersionID)
		{
			DataSet dsLocations=new DataSet();
			dsLocations=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetGenLiabLocations",CustomerID,AppID,AppVersionID);
			if(dsLocations!=null && dsLocations.Tables.Count>0 && dsLocations.Tables[0].Rows.Count>0 && dsLocations.Tables[0].Rows[0]["LOCATIONS"]!=null)
				return dsLocations.Tables[0].Rows[0]["LOCATIONS"].ToString();
			else
				return null;
		}

		public static string GetPolicyLocations(int intCustomerId, int intPolicyId, int intPolicyVersionId)//,string UserFlag)
		{
			DataSet dsLocations=new DataSet();
			dsLocations=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetPolicyGenLiabLocations",intCustomerId,intPolicyId ,intPolicyVersionId);
			if(dsLocations!=null && dsLocations.Tables.Count>0 && dsLocations.Tables[0].Rows.Count>0 && dsLocations.Tables[0].Rows[0]["LOCATIONS"]!=null)
					return dsLocations.Tables[0].Rows[0]["LOCATIONS"].ToString();
			else
				return null;
		}
		
		public static string GetGenDetailsXML(int Customer_ID, int App_ID, int App_Version_ID)//,int App_Gen_ID)
		{
			DataSet dsLocations=new DataSet();
			dsLocations=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetAPP_GENERAL_LIABILITY_DETAILS",Customer_ID,App_ID,App_Version_ID);
			if(dsLocations!=null && dsLocations.Tables.Count>0)
				return Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsLocations.Tables[0]);
			else
				return "";
		}

		public static string GetPolicyGenDetailsXML(int intCustomerId, int intPolicyId, int intPolicyVersionId)
		{
			DataSet dsLocations=new DataSet();
			dsLocations=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetPOL_GENERAL_LIABILITY_DETAILS",intCustomerId,intPolicyId ,intPolicyVersionId );  
			if(dsLocations!=null && dsLocations.Tables.Count>0)
				return Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsLocations.Tables[0]);
			else
				return "";
		}

		public static string GetClassCode(string LookupName,string LookupValueCode)
		{
			try
			{
				DataSet dsLocations=new DataSet();
				dsLocations=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetLookupDescFromCodes",LookupName,LookupValueCode);
				if(dsLocations!=null && dsLocations.Tables.Count>0 && dsLocations.Tables[0].Rows.Count>0)
					return dsLocations.Tables[0].Rows[0]["LOOKUP_VALUE_DESC"].ToString();
				else
					return "";
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally{}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsGeneralLiabilityDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		}
		#endregion

        //#region Add(Insert) functions
        ///// <summary>
        ///// Saves the information passed in model object to database.
        ///// </summary>
        ///// <param name="ObjLocationsInfo">Model class object.</param>
        ///// <returns>No of records effected.</returns>
        //public int Add(ClsGeneralLiabilityDetailsInfo objGeneralLiabilityDetailsInfo)
        //{
        //    string		strStoredProc	=	"Proc_InsertAPP_GENERAL_LIABILITY_DETAILS";
        //    DateTime	RecordDate		=	DateTime.Now;
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

        //    try
        //    {

        //        objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralLiabilityDetailsInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@APP_ID",objGeneralLiabilityDetailsInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralLiabilityDetailsInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@CLASS_CODE",objGeneralLiabilityDetailsInfo.CLASS_CODE);			
        //        if(objGeneralLiabilityDetailsInfo.LOCATION_ID==0)
        //            objDataWrapper.AddParameter("@LOCATION_ID",System.DBNull.Value);			
        //        else
        //            objDataWrapper.AddParameter("@LOCATION_ID",objGeneralLiabilityDetailsInfo.LOCATION_ID);			
        //        objDataWrapper.AddParameter("@BUSINESS_DESCRIPTION",objGeneralLiabilityDetailsInfo.BUSINESS_DESCRIPTION);
        //        objDataWrapper.AddParameter("@COVERAGE_TYPE",objGeneralLiabilityDetailsInfo.COVERAGE_TYPE);
        //        objDataWrapper.AddParameter("@COVERAGE_FORM",objGeneralLiabilityDetailsInfo.COVERAGE_FORM);
        //        objDataWrapper.AddParameter("@EXPOSURE_BASE",objGeneralLiabilityDetailsInfo.EXPOSURE_BASE);
        //        if(objGeneralLiabilityDetailsInfo.EXPOSURE==0)
        //            objDataWrapper.AddParameter("@EXPOSURE",System.DBNull.Value);
        //        else
        //            objDataWrapper.AddParameter("@EXPOSURE",objGeneralLiabilityDetailsInfo.EXPOSURE);
        //        if(objGeneralLiabilityDetailsInfo.RATE==0)
        //            objDataWrapper.AddParameter("@RATE",System.DBNull.Value);
        //        else
        //            objDataWrapper.AddParameter("@RATE",objGeneralLiabilityDetailsInfo.RATE);

        //        objDataWrapper.AddParameter("@CREATED_BY",objGeneralLiabilityDetailsInfo.CREATED_BY);
        //        objDataWrapper.AddParameter("@CREATED_DATETIME",objGeneralLiabilityDetailsInfo.CREATED_DATETIME);
				
        //        SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_GEN_ID",objGeneralLiabilityDetailsInfo.APP_GEN_ID,SqlDbType.Int,ParameterDirection.Output);
														
        //        int returnResult = 0;
        //        if(TransactionLogRequired)
        //        {
        //            objGeneralLiabilityDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/GeneralLiability/GeneralLiabilityDetails.aspx.resx");
        //            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //            string strTranXML = objBuilder.GetTransactionLogXML(objGeneralLiabilityDetailsInfo);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            objTransactionInfo.TRANS_TYPE_ID	=	1;
        //            objTransactionInfo.RECORDED_BY		=	objGeneralLiabilityDetailsInfo.CREATED_BY;
        //            objTransactionInfo.CLIENT_ID		=   objGeneralLiabilityDetailsInfo.CUSTOMER_ID; 
        //            objTransactionInfo.APP_ID			=	objGeneralLiabilityDetailsInfo.APP_ID;
        //            objTransactionInfo.APP_VERSION_ID	=	objGeneralLiabilityDetailsInfo.APP_VERSION_ID;
        //            objTransactionInfo.TRANS_DESC		=	"General Liability Details is added";
        //            objTransactionInfo.CHANGE_XML		=	strTranXML;
        //            //Executing the query
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //        }
        //        else
        //        {
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
        //        }
        //        //int APP_GEN_ID = int.Parse(objSqlParameter.Value.ToString());
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //        /*if (APP_GEN_ID == -1)
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            objGeneralLiabilityDetailsInfo.APP_GEN_ID = APP_GEN_ID;
        //            return returnResult;
        //        }*/
        //        return returnResult;
				
				
        //    }
        //    catch(Exception ex)
        //    {
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
        //        throw(ex);
        //    }
        //    finally
        //    {
        //        if(objDataWrapper != null) objDataWrapper.Dispose();
        //    }
        //}
        //#endregion


		#region AddPolicy function
		public int AddPolicy(Cms.Model.Policy.GeneralLiability.ClsGeneralLiabilityDetailsInfo  objGeneralLiabilityDetailsInfo)
		{
			string		strStoredProc	=	"Proc_InsertPOL_GENERAL_LIABILITY_DETAILS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{

				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralLiabilityDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objGeneralLiabilityDetailsInfo.POLICY_ID); 
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objGeneralLiabilityDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@CLASS_CODE",objGeneralLiabilityDetailsInfo.CLASS_CODE);			
				if(objGeneralLiabilityDetailsInfo.LOCATION_ID==0)
					objDataWrapper.AddParameter("@LOCATION_ID",System.DBNull.Value);			
				else
					objDataWrapper.AddParameter("@LOCATION_ID",objGeneralLiabilityDetailsInfo.LOCATION_ID);			
				objDataWrapper.AddParameter("@BUSINESS_DESCRIPTION",objGeneralLiabilityDetailsInfo.BUSINESS_DESCRIPTION);
				objDataWrapper.AddParameter("@COVERAGE_TYPE",objGeneralLiabilityDetailsInfo.COVERAGE_TYPE);
				objDataWrapper.AddParameter("@COVERAGE_FORM",objGeneralLiabilityDetailsInfo.COVERAGE_FORM);
				objDataWrapper.AddParameter("@EXPOSURE_BASE",objGeneralLiabilityDetailsInfo.EXPOSURE_BASE);
				if(objGeneralLiabilityDetailsInfo.EXPOSURE==0)
					objDataWrapper.AddParameter("@EXPOSURE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@EXPOSURE",objGeneralLiabilityDetailsInfo.EXPOSURE);
				if(objGeneralLiabilityDetailsInfo.RATE==0)
					objDataWrapper.AddParameter("@RATE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@RATE",objGeneralLiabilityDetailsInfo.RATE);

				objDataWrapper.AddParameter("@CREATED_BY",objGeneralLiabilityDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objGeneralLiabilityDetailsInfo.CREATED_DATETIME);
				
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@POLICY_GEN_ID",objGeneralLiabilityDetailsInfo.POLICY_GEN_ID ,SqlDbType.Int,ParameterDirection.Output);
														
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objGeneralLiabilityDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/GeneralLiability/PolicyGeneralLiabilityDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objGeneralLiabilityDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objGeneralLiabilityDetailsInfo.CREATED_BY;
					objTransactionInfo.CLIENT_ID		=   objGeneralLiabilityDetailsInfo.CUSTOMER_ID; 
					objTransactionInfo.POLICY_ID 		=	objGeneralLiabilityDetailsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objGeneralLiabilityDetailsInfo.POLICY_VERSION_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1725", "");// "General Liability Details is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				

//				int POLICY_GEN_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
//				if (POLICY_GEN_ID == -1)
//				{
//					return -1;
//				}
//				else
//				{
//					objGeneralLiabilityDetailsInfo.POLICY_GEN_ID=POLICY_GEN_ID ;
//					return returnResult;
//				}
				
				
			}
			catch(Exception ex)
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion

        //#region Update method
        ///// <summary>
        ///// Update method that recieves Model object to save.
        ///// </summary>
        ///// <param name="objOldLocationsInfo">Model object having old information</param>
        ///// <param name="ObjLocationsInfo">Model object having new information(form control's value)</param>
        ///// <returns>No. of rows updated (1 or 0)</returns>
        //public int Update(ClsGeneralLiabilityDetailsInfo objGeneralLiabilityDetailsInfo,ClsGeneralLiabilityDetailsInfo objOldGeneralLiabilityDetailsInfo)
        //{
        //    string		strStoredProc	=	"Proc_UpdateAPP_GENERAL_LIABILITY_DETAILS";
        //    string strTranXML;
        //    int returnResult = 0;
        //    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
        //    try 
        //    {
        //        objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralLiabilityDetailsInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@APP_ID",objGeneralLiabilityDetailsInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralLiabilityDetailsInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@CLASS_CODE",objGeneralLiabilityDetailsInfo.CLASS_CODE);			
        //        if(objGeneralLiabilityDetailsInfo.LOCATION_ID==0)
        //            objDataWrapper.AddParameter("@LOCATION_ID",System.DBNull.Value);			
        //        else
        //            objDataWrapper.AddParameter("@LOCATION_ID",objGeneralLiabilityDetailsInfo.LOCATION_ID);			
        //        objDataWrapper.AddParameter("@BUSINESS_DESCRIPTION",objGeneralLiabilityDetailsInfo.BUSINESS_DESCRIPTION);
        //        objDataWrapper.AddParameter("@COVERAGE_TYPE",objGeneralLiabilityDetailsInfo.COVERAGE_TYPE);
        //        objDataWrapper.AddParameter("@COVERAGE_FORM",objGeneralLiabilityDetailsInfo.COVERAGE_FORM);
        //        objDataWrapper.AddParameter("@EXPOSURE_BASE",objGeneralLiabilityDetailsInfo.EXPOSURE_BASE);
        //        if(objGeneralLiabilityDetailsInfo.EXPOSURE==0)
        //            objDataWrapper.AddParameter("@EXPOSURE",System.DBNull.Value);
        //        else
        //            objDataWrapper.AddParameter("@EXPOSURE",objGeneralLiabilityDetailsInfo.EXPOSURE);
        //        if(objGeneralLiabilityDetailsInfo.RATE==0)
        //            objDataWrapper.AddParameter("@RATE",System.DBNull.Value);
        //        else
        //            objDataWrapper.AddParameter("@RATE",objGeneralLiabilityDetailsInfo.RATE);
        //        objDataWrapper.AddParameter("@APP_GEN_ID",objGeneralLiabilityDetailsInfo.APP_GEN_ID);
        //        objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralLiabilityDetailsInfo.MODIFIED_BY);
        //        objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralLiabilityDetailsInfo.LAST_UPDATED_DATETIME);
				
        //        if(base.TransactionLogRequired) 
        //        {
        //            objGeneralLiabilityDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/GeneralLiability/AddGeneralLocation.aspx.resx");
        //            strTranXML = objBuilder.GetTransactionLogXML(objOldGeneralLiabilityDetailsInfo,objGeneralLiabilityDetailsInfo);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
        //                returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
        //            else
        //            {
        //                objTransactionInfo.TRANS_TYPE_ID	=	3;
        //                objTransactionInfo.RECORDED_BY		=	objGeneralLiabilityDetailsInfo.MODIFIED_BY;
        //                objTransactionInfo.CLIENT_ID		=   objGeneralLiabilityDetailsInfo.CUSTOMER_ID; 
        //                objTransactionInfo.TRANS_DESC		=	"General Liability Details has been modified.";
        //                objTransactionInfo.APP_ID			=	objGeneralLiabilityDetailsInfo.APP_ID;
        //                objTransactionInfo.APP_VERSION_ID	=	objGeneralLiabilityDetailsInfo.APP_VERSION_ID;						
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
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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

		#region UpdatePolicy method
		public int UpdatePolicy(Cms.Model.Policy.GeneralLiability.ClsGeneralLiabilityDetailsInfo  objGeneralLiabilityDetailsInfo
				,Cms.Model.Policy.GeneralLiability.ClsGeneralLiabilityDetailsInfo  objOldGeneralLiabilityDetailsInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_GENERAL_LIABILITY_DETAILS";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralLiabilityDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objGeneralLiabilityDetailsInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objGeneralLiabilityDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@CLASS_CODE",objGeneralLiabilityDetailsInfo.CLASS_CODE);			
//				if(objGeneralLiabilityDetailsInfo.LOCATION_ID==0)
//					objDataWrapper.AddParameter("@LOCATION_ID",System.DBNull.Value);			
//				else
//					objDataWrapper.AddParameter("@LOCATION_ID",objGeneralLiabilityDetailsInfo.LOCATION_ID);			
				objDataWrapper.AddParameter("@BUSINESS_DESCRIPTION",objGeneralLiabilityDetailsInfo.BUSINESS_DESCRIPTION);
				objDataWrapper.AddParameter("@COVERAGE_TYPE",objGeneralLiabilityDetailsInfo.COVERAGE_TYPE);
				objDataWrapper.AddParameter("@COVERAGE_FORM",objGeneralLiabilityDetailsInfo.COVERAGE_FORM);
				objDataWrapper.AddParameter("@EXPOSURE_BASE",objGeneralLiabilityDetailsInfo.EXPOSURE_BASE);
				if(objGeneralLiabilityDetailsInfo.EXPOSURE==0)
					objDataWrapper.AddParameter("@EXPOSURE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@EXPOSURE",objGeneralLiabilityDetailsInfo.EXPOSURE);
				if(objGeneralLiabilityDetailsInfo.RATE==0)
					objDataWrapper.AddParameter("@RATE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@RATE",objGeneralLiabilityDetailsInfo.RATE);
//				objDataWrapper.AddParameter("@POLICY_GEN_ID",objGeneralLiabilityDetailsInfo.POLICY_GEN_ID);
				objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralLiabilityDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralLiabilityDetailsInfo.LAST_UPDATED_DATETIME);
				
				if(base.TransactionLogRequired) 
				{
					objGeneralLiabilityDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/GeneralLiability/PolicyAddGeneralLocation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldGeneralLiabilityDetailsInfo,objGeneralLiabilityDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objGeneralLiabilityDetailsInfo.MODIFIED_BY;
						objTransactionInfo.CLIENT_ID		=   objGeneralLiabilityDetailsInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1726", "");// "General Liability Details has been modified.";
						objTransactionInfo.POLICY_ID 		=	objGeneralLiabilityDetailsInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objGeneralLiabilityDetailsInfo.POLICY_VERSION_ID;
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
				objDataWrapper.ClearParameteres();
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
//
//		#region "GetxmlMethods"
//		public static string GetXmlForPageControls(string CUSTOMER_ID)
//		{
//			string strSql = "Proc_GetXMLAPP_LOCATIONS";
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
//			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
//			return objDataSet.GetXml();
//		}
//		#endregion

		

		


	}
}
