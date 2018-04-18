/******************************************************************************************
<Author					: - Amar
<Start Date				: -	5/1/2006 5:17:10 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Claims;
using Cms.BusinessLayer.BlCommon;


namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsPropertyDamaged :  Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		CLM_PROPERTY_DAMAGED			=	"CLM_PROPERTY_DAMAGED";
		private const	string		GetCLM_PROPERTY_DAMAGED			=	"Proc_GetCLM_PROPERTY_DAMAGED";
		private const	string		InsertCLM_PROPERTY_DAMAGED		=	"Proc_InsertCLM_PROPERTY_DAMAGED";
		private const	string		UpdateCLM_PROPERTY_DAMAGED		=	"Proc_UpdateCLM_PROPERTY_DAMAGED";

		public const string PROP_DAMAGED_TYPE_VEHICLE = "11962";
		public const string PROP_DAMAGED_TYPE_HOME = "11963";
		public const string PROP_DAMAGED_TYPE_OTHER = "11964";
		
		#region Private Instance Variables
		private			bool		boolTransactionLog;	
		
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
		public ClsPropertyDamaged()
		{
			boolTransactionLog	= base.TransactionLogRequired;			
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPropertyDamagedInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPropertyDamagedInfo objPropertyDamagedInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objPropertyDamagedInfo.CLAIM_ID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@DAMAGED_ANOTHER_VEHICLE",objPropertyDamagedInfo.DAMAGED_ANOTHER_VEHICLE);
				objDataWrapper.AddParameter("@NON_OWNED_VEHICLE",objPropertyDamagedInfo.NON_OWNED_VEHICLE);
				objDataWrapper.AddParameter("@VEHICLE_ID",objPropertyDamagedInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objPropertyDamagedInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objPropertyDamagedInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objPropertyDamagedInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objPropertyDamagedInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objPropertyDamagedInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@PLATE_NUMBER",objPropertyDamagedInfo.PLATE_NUMBER);
				objDataWrapper.AddParameter("@DESCRIPTION",objPropertyDamagedInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@OTHER_INSURANCE",objPropertyDamagedInfo.OTHER_INSURANCE);
				objDataWrapper.AddParameter("@AGENCY_NAME",objPropertyDamagedInfo.AGENCY_NAME);
				objDataWrapper.AddParameter("@POLICY_NUMBER",objPropertyDamagedInfo.POLICY_NUMBER);
				objDataWrapper.AddParameter("@CREATED_BY",objPropertyDamagedInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objPropertyDamagedInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@OWNER_ID",objPropertyDamagedInfo.OWNER_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objPropertyDamagedInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",objPropertyDamagedInfo.ESTIMATE_AMOUNT);
				//objDataWrapper.AddParameter("@TYPE_OF_HOME",objPropertyDamagedInfo.TYPE_OF_HOME);

				objDataWrapper.AddParameter("@ADDRESS1",objPropertyDamagedInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objPropertyDamagedInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objPropertyDamagedInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objPropertyDamagedInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objPropertyDamagedInfo.ZIP);
				objDataWrapper.AddParameter("@COUNTRY",objPropertyDamagedInfo.COUNTRY);
				objDataWrapper.AddParameter("@PROP_DAMAGED_TYPE",objPropertyDamagedInfo.PROP_DAMAGED_TYPE);
				objDataWrapper.AddParameter("@PARTY_TYPE",objPropertyDamagedInfo.PARTY_TYPE);
				objDataWrapper.AddParameter("@PARTY_TYPE_DESC",objPropertyDamagedInfo.PARTY_TYPE_DESC);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PROPERTY_DAMAGED_ID",objPropertyDamagedInfo.PROPERTY_DAMAGED_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPropertyDamagedInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddPropertyDamaged.aspx.resx");//Done for Itrack Issue 7775 on 12 Aug 2010
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPropertyDamagedInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objPropertyDamagedInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1647","");//"Property Damaged has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 1 Feb 2010
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_PROPERTY_DAMAGED,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_PROPERTY_DAMAGED);
				}
				int PROPERTY_DAMAGED_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (PROPERTY_DAMAGED_ID == -1)
				{
					return -1;
				}
				else
				{
					objPropertyDamagedInfo.PROPERTY_DAMAGED_ID = PROPERTY_DAMAGED_ID;
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
		/// <param name="objOldInsuredVehicleInfo">Model object having old information</param>
		/// <param name="objPropertyDamagedInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPropertyDamagedInfo objOldPropertyDamagedInfo,ClsPropertyDamagedInfo objPropertyDamagedInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objPropertyDamagedInfo.CLAIM_ID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@PROPERTY_DAMAGED_ID",objPropertyDamagedInfo.PROPERTY_DAMAGED_ID);
				objDataWrapper.AddParameter("@DAMAGED_ANOTHER_VEHICLE",objPropertyDamagedInfo.DAMAGED_ANOTHER_VEHICLE);
				objDataWrapper.AddParameter("@NON_OWNED_VEHICLE",objPropertyDamagedInfo.NON_OWNED_VEHICLE);
				objDataWrapper.AddParameter("@VEHICLE_ID",objPropertyDamagedInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objPropertyDamagedInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objPropertyDamagedInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objPropertyDamagedInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objPropertyDamagedInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objPropertyDamagedInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@PLATE_NUMBER",objPropertyDamagedInfo.PLATE_NUMBER);
				objDataWrapper.AddParameter("@DESCRIPTION",objPropertyDamagedInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@OTHER_INSURANCE",objPropertyDamagedInfo.OTHER_INSURANCE);
				objDataWrapper.AddParameter("@AGENCY_NAME",objPropertyDamagedInfo.AGENCY_NAME);
				objDataWrapper.AddParameter("@POLICY_NUMBER",objPropertyDamagedInfo.POLICY_NUMBER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPropertyDamagedInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@OWNER_ID",objPropertyDamagedInfo.OWNER_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objPropertyDamagedInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPropertyDamagedInfo.LAST_UPDATED_DATETIME);				
				objDataWrapper.AddParameter("@ESTIMATE_AMOUNT",objPropertyDamagedInfo.ESTIMATE_AMOUNT);
				objDataWrapper.AddParameter("@ADDRESS1",objPropertyDamagedInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objPropertyDamagedInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objPropertyDamagedInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objPropertyDamagedInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objPropertyDamagedInfo.ZIP);
				objDataWrapper.AddParameter("@COUNTRY",objPropertyDamagedInfo.COUNTRY);
				objDataWrapper.AddParameter("@PROP_DAMAGED_TYPE",objPropertyDamagedInfo.PROP_DAMAGED_TYPE);
				objDataWrapper.AddParameter("@PARTY_TYPE",objPropertyDamagedInfo.PARTY_TYPE);
				objDataWrapper.AddParameter("@PARTY_TYPE_DESC",objPropertyDamagedInfo.PARTY_TYPE_DESC);


				if(base.TransactionLogRequired) 
				{
					objPropertyDamagedInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddPropertyDamaged.aspx.resx");
					objBuilder.GetUpdateSQL(objOldPropertyDamagedInfo,objPropertyDamagedInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objPropertyDamagedInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1648", "");// "Property Damaged Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber; //"Claim Number : " + claimNumber;//Done for Itrack Issue 6932 on 1 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_PROPERTY_DAMAGED,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_PROPERTY_DAMAGED);
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
		public static DataTable GetXmlForPageControls(string CLAIM_ID, string PROPERTY_DAMAGED_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@PROPERTY_DAMAGED_ID",PROPERTY_DAMAGED_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_PROPERTY_DAMAGED);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		#endregion
		
	}
}
