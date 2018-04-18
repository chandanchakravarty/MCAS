/******************************************************************************************
<Author				: -   
<Start Date				: -	4/21/2006 11:42:59 AM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance.Claims;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// fd
	/// </summary>
	public class ClsAdjusterDetails : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		CLM_ADJUSTER		=	"CLM_ADJUSTER";
		private const	string		GetCLM_ADJUSTER		=	"Proc_GetCLM_ADJUSTER";
		private const	string		InsertCLM_ADJUSTER  =	"Proc_InsertCLM_ADJUSTER";
		private const	string		UpdateCLM_ADJUSTER  =	"Proc_UpdateCLM_ADJUSTER";		
		private const	string		GetAGENCY_USERS		=	"Proc_GetAgencyUsersList";
		

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _ADJUSTER_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateClaimAdjuster";
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsAdjusterDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objAdjusterInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsAdjusterDetailsInfo objAdjusterInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_ADJUSTER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@ADJUSTER_TYPE",objAdjusterInfo.ADJUSTER_TYPE);
				objDataWrapper.AddParameter("@ADJUSTER_NAME",objAdjusterInfo.ADJUSTER_NAME);
				objDataWrapper.AddParameter("@ADJUSTER_CODE",objAdjusterInfo.ADJUSTER_CODE);
				objDataWrapper.AddParameter("@SUB_ADJUSTER",objAdjusterInfo.SUB_ADJUSTER);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_LEGAL_NAME",objAdjusterInfo.SUB_ADJUSTER_LEGAL_NAME);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_ADDRESS1",objAdjusterInfo.SUB_ADJUSTER_ADDRESS1);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_ADDRESS2",objAdjusterInfo.SUB_ADJUSTER_ADDRESS2);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_CITY",objAdjusterInfo.SUB_ADJUSTER_CITY);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_STATE",objAdjusterInfo.SUB_ADJUSTER_STATE);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_ZIP",objAdjusterInfo.SUB_ADJUSTER_ZIP);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_PHONE",objAdjusterInfo.SUB_ADJUSTER_PHONE);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_FAX",objAdjusterInfo.SUB_ADJUSTER_FAX);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_EMAIL",objAdjusterInfo.SUB_ADJUSTER_EMAIL);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_WEBSITE",objAdjusterInfo.SUB_ADJUSTER_WEBSITE);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_NOTES",objAdjusterInfo.SUB_ADJUSTER_NOTES);
				objDataWrapper.AddParameter("@IS_ACTIVE",objAdjusterInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objAdjusterInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objAdjusterInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_COUNTRY",objAdjusterInfo.SUB_ADJUSTER_COUNTRY);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_CONTACT_NAME",objAdjusterInfo.SUB_ADJUSTER_CONTACT_NAME);
				objDataWrapper.AddParameter("@SA_ADDRESS1",objAdjusterInfo.SA_ADDRESS1);
				objDataWrapper.AddParameter("@SA_ADDRESS2",objAdjusterInfo.SA_ADDRESS2);
				objDataWrapper.AddParameter("@SA_CITY",objAdjusterInfo.SA_CITY);
				objDataWrapper.AddParameter("@SA_COUNTRY",objAdjusterInfo.SA_COUNTRY);
				objDataWrapper.AddParameter("@SA_STATE",objAdjusterInfo.SA_STATE);
				objDataWrapper.AddParameter("@SA_ZIPCODE",objAdjusterInfo.SA_ZIPCODE);
				objDataWrapper.AddParameter("@SA_PHONE",objAdjusterInfo.SA_PHONE);
				objDataWrapper.AddParameter("@SA_FAX",objAdjusterInfo.SA_FAX);
				objDataWrapper.AddParameter("@LOB_ID",objAdjusterInfo.LOB_ID);
				objDataWrapper.AddParameter("@USER_ID",objAdjusterInfo.USER_ID);
                objDataWrapper.AddParameter("@DISPLAY_ON_CLAIM", objAdjusterInfo.DISPLAY_ON_CLAIM);

                //Added by Agniswar for Singapore Implementation on 16 Sep 2011

                objDataWrapper.AddParameter("@SUB_ADJUSTER_GST", objAdjusterInfo.SUB_ADJUSTER_GST);
                objDataWrapper.AddParameter("@SUB_ADJUSTER_GST_REG_NO", objAdjusterInfo.SUB_ADJUSTER_GST_REG_NO);
                objDataWrapper.AddParameter("@SUB_ADJUSTER_MOBILE", objAdjusterInfo.SUB_ADJUSTER_MOBILE);
                objDataWrapper.AddParameter("@SUB_ADJUSTER_CLASSIFICATION", objAdjusterInfo.SUB_ADJUSTER_CLASSIFICATION);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ADJUSTER_ID",objAdjusterInfo.ADJUSTER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objAdjusterInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddAdjusterDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objAdjusterInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAdjusterInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Claim Adjuster Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				

				if (returnResult>0)
					objAdjusterInfo.ADJUSTER_ID = int.Parse(objSqlParameter.Value.ToString());					
				else
					returnResult = -1;


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
		/// <param name="objOldAdjusterInfo">Model object having old information</param>
		/// <param name="objAdjusterInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsAdjusterDetailsInfo objOldAdjusterInfo,ClsAdjusterDetailsInfo objAdjusterInfo,string customInfo)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_ADJUSTER";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@ADJUSTER_ID",objAdjusterInfo.ADJUSTER_ID);
				objDataWrapper.AddParameter("@ADJUSTER_TYPE",objAdjusterInfo.ADJUSTER_TYPE);
				objDataWrapper.AddParameter("@ADJUSTER_NAME",objAdjusterInfo.ADJUSTER_NAME);
				objDataWrapper.AddParameter("@ADJUSTER_CODE",objAdjusterInfo.ADJUSTER_CODE);
				objDataWrapper.AddParameter("@SUB_ADJUSTER",objAdjusterInfo.SUB_ADJUSTER);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_LEGAL_NAME",objAdjusterInfo.SUB_ADJUSTER_LEGAL_NAME);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_ADDRESS1",objAdjusterInfo.SUB_ADJUSTER_ADDRESS1);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_ADDRESS2",objAdjusterInfo.SUB_ADJUSTER_ADDRESS2);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_CITY",objAdjusterInfo.SUB_ADJUSTER_CITY);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_STATE",objAdjusterInfo.SUB_ADJUSTER_STATE);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_ZIP",objAdjusterInfo.SUB_ADJUSTER_ZIP);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_PHONE",objAdjusterInfo.SUB_ADJUSTER_PHONE);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_FAX",objAdjusterInfo.SUB_ADJUSTER_FAX);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_EMAIL",objAdjusterInfo.SUB_ADJUSTER_EMAIL);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_WEBSITE",objAdjusterInfo.SUB_ADJUSTER_WEBSITE);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_NOTES",objAdjusterInfo.SUB_ADJUSTER_NOTES);
				objDataWrapper.AddParameter("@MODIFIED_BY",objAdjusterInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objAdjusterInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_COUNTRY",objAdjusterInfo.SUB_ADJUSTER_COUNTRY);
				objDataWrapper.AddParameter("@SUB_ADJUSTER_CONTACT_NAME",objAdjusterInfo.SUB_ADJUSTER_CONTACT_NAME);
				objDataWrapper.AddParameter("@SA_ADDRESS1",objAdjusterInfo.SA_ADDRESS1);
				objDataWrapper.AddParameter("@SA_ADDRESS2",objAdjusterInfo.SA_ADDRESS2);
				objDataWrapper.AddParameter("@SA_CITY",objAdjusterInfo.SA_CITY);
				objDataWrapper.AddParameter("@SA_COUNTRY",objAdjusterInfo.SA_COUNTRY);
				objDataWrapper.AddParameter("@SA_STATE",objAdjusterInfo.SA_STATE);
				objDataWrapper.AddParameter("@SA_ZIPCODE",objAdjusterInfo.SA_ZIPCODE);
				objDataWrapper.AddParameter("@SA_PHONE",objAdjusterInfo.SA_PHONE);
				objDataWrapper.AddParameter("@SA_FAX",objAdjusterInfo.SA_FAX);
				objDataWrapper.AddParameter("@LOB_ID",objAdjusterInfo.LOB_ID);
				objDataWrapper.AddParameter("@USER_ID",objAdjusterInfo.USER_ID);
                objDataWrapper.AddParameter("@DISPLAY_ON_CLAIM", objAdjusterInfo.DISPLAY_ON_CLAIM);

                //Added by Agniswar for Singapore Implementation on 16 Sep 2011

                objDataWrapper.AddParameter("@SUB_ADJUSTER_GST", objAdjusterInfo.SUB_ADJUSTER_GST);
                objDataWrapper.AddParameter("@SUB_ADJUSTER_GST_REG_NO", objAdjusterInfo.SUB_ADJUSTER_GST_REG_NO);
                objDataWrapper.AddParameter("@SUB_ADJUSTER_MOBILE", objAdjusterInfo.SUB_ADJUSTER_MOBILE);
                objDataWrapper.AddParameter("@SUB_ADJUSTER_CLASSIFICATION", objAdjusterInfo.SUB_ADJUSTER_CLASSIFICATION);

				if(base.TransactionLogRequired) 
				{
					objAdjusterInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddAdjusterDetails.aspx.resx");
					objBuilder.GetUpdateSQL(objOldAdjusterInfo,objAdjusterInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objAdjusterInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Claim Adjuster Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	customInfo;
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
		#endregion

		#region "GetxmlMethods"
//		public static string GetXmlForPageControls(string ADJUSTER_ID)
//		{
//			string strSql = "Proc_GetXMLCLM_ADJUSTER";
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//			objDataWrapper.AddParameter("@ADJUSTER_ID",ADJUSTER_ID);
//			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
//			return objDataSet.GetXml();
//		}
		#endregion

		#region GetAdjusterDetails
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static string GetAdjusterDetails(int intAdjuster_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@ADJUSTER_ID",intAdjuster_ID);
			DataSet ds = objWrapper.ExecuteDataSet(GetCLM_ADJUSTER);			
			if(ds!=null && ds.Tables.Count>0)
				return Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
			else
				return "";
		}

		#endregion

		#region GetAdjusterCode
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataTable GetAdjusterCode(string strCarrierID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);			
			objWrapper.AddParameter("@CARRIER_ID",strCarrierID);			
			DataSet ds = objWrapper.ExecuteDataSet(GetAGENCY_USERS);
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		#endregion


        
	}
}
