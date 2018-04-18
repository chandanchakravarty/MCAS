/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	5/25/2005 10:39:07 AM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
<Modified Date			: - Sumit Chhabra
<Modified By			: - Oct 10, 2005
<Purpose				: - Added another function GetNewLocationNumber that will generate new location number

<Modified Date			: - Ravindra Gupta
<Modified By			: - March 21, 2006
<Purpose				: - To add policy level functions

*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using Cms.DataLayer;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application;


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsRealEstateLocation : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_UMBRELLA_REAL_ESTATE_LOCATION			=	"APP_UMBRELLA_REAL_ESTATE_LOCATION";
		private const string strStoredProcAN			= "Proc_UpdateRemarks";


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
		public ClsRealEstateLocation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjRealEstateLocationInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(Cms.Model.Application.ClsRealEstateLocationInfo ObjRealEstateLocationInfo)
		{
			string		strStoredProc	=	"Proc_InsertRealEstateLocation";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjRealEstateLocationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjRealEstateLocationInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjRealEstateLocationInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CLIENT_LOCATION_NUMBER",ObjRealEstateLocationInfo.CLIENT_LOCATION_NUMBER);
				objDataWrapper.AddParameter("@LOCATION_NUMBER",ObjRealEstateLocationInfo.LOCATION_NUMBER);
				objDataWrapper.AddParameter("@ADDRESS_1",ObjRealEstateLocationInfo.ADDRESS_1);
				objDataWrapper.AddParameter("@ADDRESS_2",ObjRealEstateLocationInfo.ADDRESS_2);
				objDataWrapper.AddParameter("@CITY",ObjRealEstateLocationInfo.CITY);
				objDataWrapper.AddParameter("@COUNTY",ObjRealEstateLocationInfo.COUNTY);
				objDataWrapper.AddParameter("@STATE",ObjRealEstateLocationInfo.STATE);
				objDataWrapper.AddParameter("@ZIPCODE",ObjRealEstateLocationInfo.ZIPCODE);
				objDataWrapper.AddParameter("@PHONE_NUMBER",ObjRealEstateLocationInfo.PHONE_NUMBER);
				objDataWrapper.AddParameter("@FAX_NUMBER",ObjRealEstateLocationInfo.FAX_NUMBER);
				//objDataWrapper.AddParameter("@REMARKS",ObjRealEstateLocationInfo.REMARKS);
				//objDataWrapper.AddParameter("@IS_ACTIVE",ObjRealEstateLocationInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",ObjRealEstateLocationInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",ObjRealEstateLocationInfo.CREATED_DATETIME);
				//objDataWrapper.AddParameter("@MODIFIED_BY",ObjRealEstateLocationInfo.MODIFIED_BY);
				//objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjRealEstateLocationInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@OCCUPIED_BY",ObjRealEstateLocationInfo.OCCUPIED_BY);
				if(ObjRealEstateLocationInfo.NUM_FAMILIES==0)
					objDataWrapper.AddParameter("@NUM_FAMILIES",null);
				else
					objDataWrapper.AddParameter("@NUM_FAMILIES",ObjRealEstateLocationInfo.NUM_FAMILIES);
				objDataWrapper.AddParameter("@BUSS_FARM_PURSUITS",ObjRealEstateLocationInfo.BUSS_FARM_PURSUITS);
				objDataWrapper.AddParameter("@BUSS_FARM_PURSUITS_DESC",ObjRealEstateLocationInfo.BUSS_FARM_PURSUITS_DESC);
				objDataWrapper.AddParameter("@LOC_EXCLUDED",ObjRealEstateLocationInfo.LOC_EXCLUDED);
				objDataWrapper.AddParameter("@PERS_INJ_COV_82",ObjRealEstateLocationInfo.PERS_INJ_COV_82);	
				objDataWrapper.AddParameter("@OTHER_POLICY",ObjRealEstateLocationInfo.OTHER_POLICY);	
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LOCATION_ID",ObjRealEstateLocationInfo.LOCATION_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjRealEstateLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/AddRealEstateLocation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjRealEstateLocationInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = ObjRealEstateLocationInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = ObjRealEstateLocationInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = ObjRealEstateLocationInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	ObjRealEstateLocationInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1447", "");	//"New location detail is added";
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
					ObjRealEstateLocationInfo.LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());
				}

				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,ObjRealEstateLocationInfo.CUSTOMER_ID,ObjRealEstateLocationInfo.APP_ID,ObjRealEstateLocationInfo.APP_VERSION_ID,RuleType.RiskDependent,0);
				
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

		#region AddPolicy (Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjRealEstateLocationInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddPolicy(Cms.Model.Policy.Umbrella.ClsRealEstateLocationInfo  ObjRealEstateLocationInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyRealEstateLocation";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjRealEstateLocationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",ObjRealEstateLocationInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",ObjRealEstateLocationInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@CLIENT_LOCATION_NUMBER",ObjRealEstateLocationInfo.CLIENT_LOCATION_NUMBER);
				objDataWrapper.AddParameter("@LOCATION_NUMBER",ObjRealEstateLocationInfo.LOCATION_NUMBER);
				objDataWrapper.AddParameter("@ADDRESS_1",ObjRealEstateLocationInfo.ADDRESS_1);
				objDataWrapper.AddParameter("@ADDRESS_2",ObjRealEstateLocationInfo.ADDRESS_2);
				objDataWrapper.AddParameter("@CITY",ObjRealEstateLocationInfo.CITY);
				objDataWrapper.AddParameter("@COUNTY",ObjRealEstateLocationInfo.COUNTY);
				objDataWrapper.AddParameter("@STATE",ObjRealEstateLocationInfo.STATE);
				objDataWrapper.AddParameter("@ZIPCODE",ObjRealEstateLocationInfo.ZIPCODE);
				objDataWrapper.AddParameter("@PHONE_NUMBER",ObjRealEstateLocationInfo.PHONE_NUMBER);
				objDataWrapper.AddParameter("@FAX_NUMBER",ObjRealEstateLocationInfo.FAX_NUMBER);
				objDataWrapper.AddParameter("@CREATED_BY",ObjRealEstateLocationInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
				objDataWrapper.AddParameter("@OCCUPIED_BY",ObjRealEstateLocationInfo.OCCUPIED_BY);
				if(ObjRealEstateLocationInfo.NUM_FAMILIES==0)
					objDataWrapper.AddParameter("@NUM_FAMILIES",null);
				else
					objDataWrapper.AddParameter("@NUM_FAMILIES",ObjRealEstateLocationInfo.NUM_FAMILIES);
				objDataWrapper.AddParameter("@BUSS_FARM_PURSUITS",ObjRealEstateLocationInfo.BUSS_FARM_PURSUITS);
				objDataWrapper.AddParameter("@BUSS_FARM_PURSUITS_DESC",ObjRealEstateLocationInfo.BUSS_FARM_PURSUITS_DESC);
				objDataWrapper.AddParameter("@LOC_EXCLUDED",ObjRealEstateLocationInfo.LOC_EXCLUDED);
				objDataWrapper.AddParameter("@PERS_INJ_COV_82",ObjRealEstateLocationInfo.PERS_INJ_COV_82);
				objDataWrapper.AddParameter("@OTHER_POLICY",ObjRealEstateLocationInfo.OTHER_POLICY);	

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LOCATION_ID",ObjRealEstateLocationInfo.LOCATION_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjRealEstateLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddRealEstateLocation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjRealEstateLocationInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID=ObjRealEstateLocationInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID =ObjRealEstateLocationInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID = ObjRealEstateLocationInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	ObjRealEstateLocationInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1470", "");// "Policy location detail is added";
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
					ObjRealEstateLocationInfo.LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());
				}

				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,ObjRealEstateLocationInfo.CUSTOMER_ID,ObjRealEstateLocationInfo.POLICY_ID,ObjRealEstateLocationInfo.POLICY_VERSION_ID,RuleType.RiskDependent,0);
				
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
		/// <param name="objOldRealEstateLocationInfo">Model object having old information</param>
		/// <param name="ObjRealEstateLocationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(Cms.Model.Application.ClsRealEstateLocationInfo objOldRealEstateLocationInfo,Cms.Model.Application.ClsRealEstateLocationInfo ObjRealEstateLocationInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdateRealEstateLocation ";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjRealEstateLocationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjRealEstateLocationInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjRealEstateLocationInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CLIENT_LOCATION_NUMBER",ObjRealEstateLocationInfo.CLIENT_LOCATION_NUMBER);
				objDataWrapper.AddParameter("@LOCATION_ID",ObjRealEstateLocationInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@LOCATION_NUMBER",ObjRealEstateLocationInfo.LOCATION_NUMBER);
				objDataWrapper.AddParameter("@ADDRESS_1",ObjRealEstateLocationInfo.ADDRESS_1);
				objDataWrapper.AddParameter("@ADDRESS_2",ObjRealEstateLocationInfo.ADDRESS_2);
				objDataWrapper.AddParameter("@CITY",ObjRealEstateLocationInfo.CITY);
				objDataWrapper.AddParameter("@COUNTY",ObjRealEstateLocationInfo.COUNTY);
				objDataWrapper.AddParameter("@STATE",ObjRealEstateLocationInfo.STATE);
				objDataWrapper.AddParameter("@ZIPCODE",ObjRealEstateLocationInfo.ZIPCODE);
				objDataWrapper.AddParameter("@PHONE_NUMBER",ObjRealEstateLocationInfo.PHONE_NUMBER);
				objDataWrapper.AddParameter("@FAX_NUMBER",ObjRealEstateLocationInfo.FAX_NUMBER);
				//objDataWrapper.AddParameter("@REMARKS",ObjRealEstateLocationInfo.REMARKS);
				//objDataWrapper.AddParameter("@IS_ACTIVE",ObjRealEstateLocationInfo.IS_ACTIVE);
				//objDataWrapper.AddParameter("@CREATED_BY",ObjRealEstateLocationInfo.CREATED_BY);
				//objDataWrapper.AddParameter("@CREATED_DATETIME",ObjRealEstateLocationInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",ObjRealEstateLocationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjRealEstateLocationInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@OCCUPIED_BY",ObjRealEstateLocationInfo.OCCUPIED_BY);
				if(ObjRealEstateLocationInfo.NUM_FAMILIES==0)
					objDataWrapper.AddParameter("@NUM_FAMILIES",null);
				else
					objDataWrapper.AddParameter("@NUM_FAMILIES",ObjRealEstateLocationInfo.NUM_FAMILIES);
				objDataWrapper.AddParameter("@BUSS_FARM_PURSUITS",ObjRealEstateLocationInfo.BUSS_FARM_PURSUITS);
				objDataWrapper.AddParameter("@BUSS_FARM_PURSUITS_DESC",ObjRealEstateLocationInfo.BUSS_FARM_PURSUITS_DESC);
				objDataWrapper.AddParameter("@LOC_EXCLUDED",ObjRealEstateLocationInfo.LOC_EXCLUDED);
				objDataWrapper.AddParameter("@PERS_INJ_COV_82",ObjRealEstateLocationInfo.PERS_INJ_COV_82);				
				objDataWrapper.AddParameter("@OTHER_POLICY",ObjRealEstateLocationInfo.OTHER_POLICY);	

				if(TransactionLogRequired) 
				{
					
					ObjRealEstateLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/AddRealEstateLocation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldRealEstateLocationInfo,ObjRealEstateLocationInfo);
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.APP_ID			=	ObjRealEstateLocationInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	ObjRealEstateLocationInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	ObjRealEstateLocationInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	ObjRealEstateLocationInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1466", "");// "Location detail is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,ObjRealEstateLocationInfo.CUSTOMER_ID,ObjRealEstateLocationInfo.APP_ID,ObjRealEstateLocationInfo.APP_VERSION_ID,RuleType.RiskDependent,0);
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

		#region UpdatePolicy method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldRealEstateLocationInfo">Model object having old information</param>
		/// <param name="ObjRealEstateLocationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicy(Cms.Model.Policy.Umbrella.ClsRealEstateLocationInfo  objOldRealEstateLocationInfo,Cms.Model.Policy.Umbrella.ClsRealEstateLocationInfo  ObjRealEstateLocationInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdatePolicyRealEstateLocation ";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjRealEstateLocationInfo.CUSTOMER_ID );
				objDataWrapper.AddParameter("@POLICY_ID",ObjRealEstateLocationInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",ObjRealEstateLocationInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@CLIENT_LOCATION_NUMBER",ObjRealEstateLocationInfo.CLIENT_LOCATION_NUMBER);
				objDataWrapper.AddParameter("@LOCATION_ID",ObjRealEstateLocationInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@LOCATION_NUMBER",ObjRealEstateLocationInfo.LOCATION_NUMBER);
				objDataWrapper.AddParameter("@ADDRESS_1",ObjRealEstateLocationInfo.ADDRESS_1);
				objDataWrapper.AddParameter("@ADDRESS_2",ObjRealEstateLocationInfo.ADDRESS_2);
				objDataWrapper.AddParameter("@CITY",ObjRealEstateLocationInfo.CITY);
				objDataWrapper.AddParameter("@COUNTY",ObjRealEstateLocationInfo.COUNTY);
				objDataWrapper.AddParameter("@STATE",ObjRealEstateLocationInfo.STATE);
				objDataWrapper.AddParameter("@ZIPCODE",ObjRealEstateLocationInfo.ZIPCODE);
				objDataWrapper.AddParameter("@PHONE_NUMBER",ObjRealEstateLocationInfo.PHONE_NUMBER);
				objDataWrapper.AddParameter("@FAX_NUMBER",ObjRealEstateLocationInfo.FAX_NUMBER);
				objDataWrapper.AddParameter("@MODIFIED_BY",ObjRealEstateLocationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",RecordDate );
				objDataWrapper.AddParameter("@OCCUPIED_BY",ObjRealEstateLocationInfo.OCCUPIED_BY);
				if(ObjRealEstateLocationInfo.NUM_FAMILIES==0)
					objDataWrapper.AddParameter("@NUM_FAMILIES",null);
				else
					objDataWrapper.AddParameter("@NUM_FAMILIES",ObjRealEstateLocationInfo.NUM_FAMILIES);
				objDataWrapper.AddParameter("@BUSS_FARM_PURSUITS",ObjRealEstateLocationInfo.BUSS_FARM_PURSUITS);
				objDataWrapper.AddParameter("@BUSS_FARM_PURSUITS_DESC",ObjRealEstateLocationInfo.BUSS_FARM_PURSUITS_DESC);
				objDataWrapper.AddParameter("@LOC_EXCLUDED",ObjRealEstateLocationInfo.LOC_EXCLUDED);
				objDataWrapper.AddParameter("@PERS_INJ_COV_82",ObjRealEstateLocationInfo.PERS_INJ_COV_82);
				objDataWrapper.AddParameter("@OTHER_POLICY",ObjRealEstateLocationInfo.OTHER_POLICY);	

				if(TransactionLogRequired) 
				{
					
					ObjRealEstateLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddRealEstateLocation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldRealEstateLocationInfo,ObjRealEstateLocationInfo);
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.POLICY_ID 		=	ObjRealEstateLocationInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	ObjRealEstateLocationInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	ObjRealEstateLocationInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	ObjRealEstateLocationInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1471", "");// "Policy location detail is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,ObjRealEstateLocationInfo.CUSTOMER_ID,ObjRealEstateLocationInfo.POLICY_ID,ObjRealEstateLocationInfo.POLICY_VERSION_ID,RuleType.RiskDependent,0);
				
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

		#region GetRealEstateLocationXml
		public static string GetRealEstateLocationXml(int intCustoemrId, int intAppId,int intAppVersionId,int intLocationId)
		{
			string strStoredProc = "Proc_GetRealEstateLocation";
			DataSet dsRealEstateLocation = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@LOCATION_ID",intLocationId);
				
				dsRealEstateLocation= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsRealEstateLocation.Tables[0].Rows.Count != 0)
				{
					return dsRealEstateLocation.GetXml();
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

		#region GetPolicyRealEstateLocation
		public static DataTable GetPolicyRealEstateLocation(int intCustomerId, int intPolicyId,int intPolicyVersionId,int intLocationId)
		{
			string strStoredProc = "Proc_GetPolicyRealEstateLocation";
			DataSet dsRealEstateLocation = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",intPolicyId );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionId );
				objDataWrapper.AddParameter("@LOCATION_ID",intLocationId);
				
				dsRealEstateLocation= objDataWrapper.ExecuteDataSet(strStoredProc);
				return dsRealEstateLocation.Tables[0];
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

		#region REMARKS
		public int UpdateRemarks(ClsRealEstateLocationInfo objOldRealEstateLocationInfo,ClsRealEstateLocationInfo objRealEstateLocationInfo)
		{
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			int returnResult = 0;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
			try
			{

				objDataWrapper.AddParameter("@CUSTOMER_ID",objRealEstateLocationInfo.CUSTOMER_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",objRealEstateLocationInfo.APP_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objRealEstateLocationInfo.APP_VERSION_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@LOCATION_ID",objRealEstateLocationInfo.LOCATION_ID,SqlDbType.Int);
				if(objRealEstateLocationInfo.REMARKS.Trim()=="")
					objDataWrapper.AddParameter("@REMARKS",System.DBNull.Value);
				else
				objDataWrapper.AddParameter("@REMARKS",objRealEstateLocationInfo.REMARKS);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURNVALUE",objRealEstateLocationInfo.APP_VERSION_ID,SqlDbType.Int,ParameterDirection.ReturnValue);
				/*try
				{
				
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN);
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					returnResult= int.Parse(objSqlParameter.Value.ToString());					
					return returnResult;
				}*/
				if(TransactionLogRequired) 
				{
					objRealEstateLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Remarks.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldRealEstateLocationInfo,objRealEstateLocationInfo);                   
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID			=	objRealEstateLocationInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objRealEstateLocationInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objRealEstateLocationInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objRealEstateLocationInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1472", "");// "Remarks is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				returnResult= int.Parse(objSqlParameter.Value.ToString());					
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
		/// Function for viewing the details of Remarks
		/// </summary>
		/// <returns></returns>
		public string ViewRemarks(string strCustomerID,string strAppID,string strAppVerID,string strLocationID)
		{
			DataSet dsRemarks = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_ID",strAppID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@LOCATION_ID",strLocationID,SqlDbType.Int);
			dsRemarks = objDataWrapper.ExecuteDataSet("Proc_GetRemarks");
			
			return dsRemarks.GetXml();
		}
		
		#endregion

		#region UpdatePolicyRemarks Function
		public int UpdatePolicyRemarks(Cms.Model.Policy.Umbrella.ClsRealEstateLocationInfo  objOldInfo,Cms.Model.Policy.Umbrella.ClsRealEstateLocationInfo  objInfo)
		{
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string strStoreProc="Proc_UpdatePolicyLocationRemarks";
			int returnResult = 0;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
			try
			{

				objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID ,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID ,SqlDbType.Int);
				objDataWrapper.AddParameter("@LOCATION_ID",objInfo.LOCATION_ID,SqlDbType.Int);
				if(objInfo.REMARKS.Trim()=="")
					objDataWrapper.AddParameter("@REMARKS",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
				
				
				if(TransactionLogRequired) 
				{
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyLocationRemarks.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objInfo);                   
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoreProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.POLICY_ID 		=	objInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1473", "");// "Policy Location Remarks is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoreProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN);
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
		
		#region ViewPolicyLocationRemarks Function 
		public string ViewPolicyLocationRemarks(int intCustomerID,int intPolicyID,int intPolicyVerID,int intLocationID)
		{
			DataSet dsRemarks = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_ID",intPolicyID ,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVerID ,SqlDbType.Int);
			objDataWrapper.AddParameter("@LOCATION_ID",intLocationID,SqlDbType.Int);
			dsRemarks = objDataWrapper.ExecuteDataSet("Proc_GetPolicyLocationRemarks");
			
			return dsRemarks.GetXml();
		}
		#endregion

		#region Activate deactivate function
		public void ActivateDeactivateLocation(int intCustomerId, int intAppId, int intAppVersionID, int intLocationID, string strStatus )
		{
			string strStoredProc = "Proc_ActivateDeactvateRealEstateLocation";
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionID);
				objDataWrapper.AddParameter("@LOCATION_ID",intLocationID);
				objDataWrapper.AddParameter("@STATUS",strStatus);
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,intCustomerId,intAppId,intAppVersionID,RuleType.RiskDependent,0);
				
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

		#region Activate deactivate Policy function
		public void ActivateDeactivatePolicyLocation(int intCustomerId, int intPolicyId, int intPolicyVersionID, int intLocationID, string strStatus )
		{
			string strStoredProc = "Proc_ActivateDeactvatePolicyRealEstateLocation";
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",intPolicyId );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionID );
				objDataWrapper.AddParameter("@LOCATION_ID",intLocationID);
				objDataWrapper.AddParameter("@STATUS",strStatus);
				
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				ClsUmbrellaCoverages objCoverage=new ClsUmbrellaCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,intCustomerId,intPolicyId,intPolicyVersionID,RuleType.RiskDependent,0);
				
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

		#region Delete function
		/// <summary>
		/// DELETES APP LEVEL LOCATION INFO
		/// </summary>
		/// <param name="ObjRealEstateLocationInfo"></param>
		/// <returns>intRetVal</returns>
		public int DeleteLocation(ClsRealEstateLocationInfo ObjRealEstateLocationInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeleteAppRealEstateLocations";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjRealEstateLocationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjRealEstateLocationInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjRealEstateLocationInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",ObjRealEstateLocationInfo.LOCATION_ID);
					

				if(TransactionLogRequired) 
				{			
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID			=	3;   
					objTransactionInfo.RECORDED_BY				=	ObjRealEstateLocationInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID					=	ObjRealEstateLocationInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID			=	ObjRealEstateLocationInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID				=	ObjRealEstateLocationInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1469", "");// "Location has been Deleted";
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		}
		#endregion

		#region DeletePolicy function
		/// <summary>
		/// DELETES POL LEVEL LOCATION INFO
		/// </summary>
		/// <param name="ObjRealEstateLocationInfo"></param>
		/// <returns>intRetVal</returns>
		public int DeletePolicyLocation(Cms.Model.Policy.Umbrella.ClsRealEstateLocationInfo ObjInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeletePolRealEstateLocations";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",ObjInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",ObjInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",ObjInfo.LOCATION_ID);
					

				if(TransactionLogRequired) 
				{			
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID			=	3;   
					objTransactionInfo.RECORDED_BY				=	ObjInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID				=	ObjInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	ObjInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID				=	ObjInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1469", "");// "Location has been Deleted";
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		} 
		#endregion

		#region GetNewLocationNumber
		public static int GetNewLocationNumber(string strCustomerID, string strAppID, string strAppVersionID, string strCalledFrom)
		{
			string strStoredProc = "Proc_GetNewLocationNumber";
			DataSet dsGeneralLocations= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			int NewLocationNum=0,returnResult=0;
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", strCustomerID);
				objDataWrapper.AddParameter("@APP_ID", strAppID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVersionID);				
				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CODE",NewLocationNum,SqlDbType.Int,ParameterDirection.Output);
				returnResult= objDataWrapper.ExecuteNonQuery(strStoredProc);
				if (returnResult!=0)				
					NewLocationNum= int.Parse(objSqlParameter.Value.ToString());					
				else
					NewLocationNum=1;
				return NewLocationNum;
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				if(objDataWrapper!=null) objDataWrapper.Dispose();
			}
		}
		#endregion

		#region GetNewPolicyLocationNumber
		public static int GetNewPolicyLocationNumber(int intCustomerID,int intPolicyID,int intPolicyVersionID)
		{
			
			string		strStoredProc	=	"Proc_GetNewPolicyLocationNumber";
						
			int nextLocationNumber = 0;

			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",intCustomerID );
			sqlParams[1] = new SqlParameter("@POLICY_ID",intPolicyID );
			sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID",intPolicyVersionID );
			
			try
			{
				nextLocationNumber = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return nextLocationNumber;
		
		}
		#endregion
	}
}
