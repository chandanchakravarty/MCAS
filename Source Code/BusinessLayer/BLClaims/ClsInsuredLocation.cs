/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	5/1/2006 3:25:06 PM
<End Date				: -	
<Description				: - 	Buisness Layer Class for Insured Location
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
using Cms.Model.Claims; 

namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsInsuredLocation : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsInsuredLocation()
		{}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objILInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsInsuredLocationInfo objILInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_INSURED_LOCATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objILInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@LOCATION_DESCRIPTION",objILInfo.LOCATION_DESCRIPTION);
				objDataWrapper.AddParameter("@ADDRESS1",objILInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objILInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objILInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objILInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objILInfo.ZIP);
				objDataWrapper.AddParameter("@COUNTRY",objILInfo.COUNTRY);
				objDataWrapper.AddParameter("@CREATED_BY",objILInfo.CREATED_BY);
				objDataWrapper.AddParameter("@POLICY_LOCATION_ID",objILInfo.POLICY_LOCATION_ID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@INSURED_LOCATION_ID",objILInfo.INSURED_LOCATION_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objILInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddInsuredLocation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objILInfo);	
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;	
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objILInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Insured Location is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Done for Itrack Issue 6932 on 15 Jan 2010
					//returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber + ";" + "Location Id : " + objSqlParameter.Value.ToString();
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				

				if (returnResult>0)
				{
					objILInfo.INSURED_LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());					
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
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldILInfo">Model object having old information</param>
		/// <param name="objILInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsInsuredLocationInfo objOldILInfo,ClsInsuredLocationInfo objILInfo)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_INSURED_LOCATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objILInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@INSURED_LOCATION_ID",objILInfo.INSURED_LOCATION_ID);
				//objDataWrapper.AddParameter("@CLAIM_ID",objILInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@LOCATION_DESCRIPTION",objILInfo.LOCATION_DESCRIPTION);
				objDataWrapper.AddParameter("@ADDRESS1",objILInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objILInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objILInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objILInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objILInfo.ZIP);
				objDataWrapper.AddParameter("@COUNTRY",objILInfo.COUNTRY);
				objDataWrapper.AddParameter("@MODIFIED_BY",objILInfo.MODIFIED_BY);

				if(TransactionLogRequired) 
				{
					objILInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Claims/Aspx/AddInsuredLocation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldILInfo,objILInfo);//Done for Itrack issue 6932 on 3 June 2010
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;	
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objILInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Insured Location is modified";
					//Done for Itrack Issue 6932 on 15 Jan 2010
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber + ";" + "Location Id : " + objILInfo.INSURED_LOCATION_ID;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

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
		#endregion

		#region "GetxmlMethods"
		public string GetXmlForPageControls(int INSURED_LOCATION_ID, int CLAIM_ID)
		{
			DataSet dsTemp = GetValuesOfPageControls(INSURED_LOCATION_ID,CLAIM_ID);
			return dsTemp.GetXml();
		}

		public DataSet GetValuesOfPageControls(int INSURED_LOCATION_ID, int CLAIM_ID)
		{
			string strSql = "Proc_GetXMLCLM_INSURED_LOCATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@INSURED_LOCATION_ID",INSURED_LOCATION_ID);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion

		#region Delete Method
		public int Delete(int INSURED_LOCATION_ID, int CLAIM_ID)
		{
			string		strStoredProc	=	"Proc_DeleteCLM_INSURED_LOCATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@INSURED_LOCATION_ID",INSURED_LOCATION_ID);
				objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);

				int returnResult = 0;
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
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

		#region Return DataSet for Policy Locations
		public DataSet GetPolicyLocations(int CLAIM_ID)
		{
			string strSql = "Proc_GetPolicyLocations";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}



		#endregion

		#region Return DataTable for Individual Policy Location
		public DataTable GetPolicyLocationById(int CLAIM_ID, int LOCATION_ID)
		{
			string strSql = "Proc_GetPolicyLocations";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@LOCATION_ID",LOCATION_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}



		#endregion

		//Added for Itrack Issue 5833 on 20 May 2009
		public int CheckClaimActivityReserve(int claimID,int locationID,string calledFrom)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				string strSql = "Proc_CheckCLM_ACTIVITY_RESERVE";
				objDataWrapper.ClearParameteres();			
				objDataWrapper.AddParameter("@CLAIM_ID",claimID);
				objDataWrapper.AddParameter("@INSUREDVEHICLE_LOCATION_BOAT_ID",locationID);
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom.ToUpper());
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESULT",null,SqlDbType.Int,ParameterDirection.Output);
				int result= objDataWrapper.ExecuteNonQuery(strSql);
				result=  int.Parse(objSqlParameter.Value.ToString());
				return result;

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
			}
		}

		public int DeleteLocation(ClsInsuredLocationInfo objILInfo,int claimID, int locationID,string calledFrom)
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",claimID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@INSUREDVEHICLE_LOCATION_BOAT_ID",locationID);
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);

				if(TransactionLogRequired) 
				{
					objILInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddInsuredLocation.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objILInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objILInfo.CREATED_BY;					
					objTransactionInfo.TRANS_DESC		=	"Insured Location is deleted";//Done for Itrack Issue 6932 on 3 June 2010
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 1 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery("PROC_DELETEINSUREDVEHICLE",objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery("PROC_DELETEINSUREDVEHICLE");

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

		public int ActivateDeactivateLocation(ClsInsuredLocationInfo objILInfo,int claimID, int locationID, string flagActivateDeactivate,string calledFrom)
		{
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",claimID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@INSUREDVEHICLE_LOCATION_BOAT_ID",locationID);
				objDataWrapper.AddParameter("@ACTIVATEDEACTIVATE",flagActivateDeactivate);
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);

				if(TransactionLogRequired) 
				{
					objILInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddInsuredLocation.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objILInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objILInfo.CREATED_BY;					
					if(flagActivateDeactivate=="Y")
						objTransactionInfo.TRANS_DESC		=	"Insured Location is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Insured Location is Deactivated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber;//Done for Itrack Issue 6932 on 1 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery("PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE",objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery("PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE");

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
	 //Added till here
	}
}
