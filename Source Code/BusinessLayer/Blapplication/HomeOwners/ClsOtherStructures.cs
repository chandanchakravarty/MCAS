/******************************************************************************************
<Author				: -   Shafii
<Start Date				: -	6/23/2006 12:42:02 PM
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
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.Model.Policy;
using Cms.Model.Policy.Homeowners;

namespace Cms.BusinessLayer.BlApplication.HomeOwners
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsOtherStructures : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_OTHER_STRUCTURE_DWELLING			=	"APP_OTHER_STRUCTURE_DWELLING";
		public const string PREMISES_LOCATION_ON_PREMISES_RENTED_OTHERS = "11968";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _OTHER_STRUCTURE_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_OTHER_STRUCTURE_DWELLING";
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
		public ClsOtherStructures()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		public int AddAcord(ClsOtherStructuresInfo objOtherStructuresInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertAPP_OTHER_STRUCTURE_DWELLING_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOtherStructuresInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOtherStructuresInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOtherStructuresInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOtherStructuresInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@PREMISES_LOCATION",objOtherStructuresInfo.PREMISES_LOCATION);
				objDataWrapper.AddParameter("@PREMISES_DESCRIPTION",objOtherStructuresInfo.PREMISES_DESCRIPTION);
				objDataWrapper.AddParameter("@PREMISES_USE",objOtherStructuresInfo.PREMISES_USE);
				objDataWrapper.AddParameter("@PREMISES_CONDITION",objOtherStructuresInfo.PREMISES_CONDITION);
				objDataWrapper.AddParameter("@PICTURE_ATTACHED",objOtherStructuresInfo.PICTURE_ATTACHED);
				objDataWrapper.AddParameter("@COVERAGE_BASIS",objOtherStructuresInfo.COVERAGE_BASIS);
				objDataWrapper.AddParameter("@SATELLITE_EQUIPMENT",objOtherStructuresInfo.SATELLITE_EQUIPMENT);
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objOtherStructuresInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objOtherStructuresInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objOtherStructuresInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objOtherStructuresInfo.LOCATION_ZIP);

				
				objDataWrapper.AddParameter("@ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED));
				objDataWrapper.AddParameter("@INSURING_VALUE",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE));
				objDataWrapper.AddParameter("@INSURING_VALUE_OFF_PREMISES",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE_OFF_PREMISES));
				objDataWrapper.AddParameter("@COVERAGE_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.COVERAGE_AMOUNT));
				
				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@CREATED_BY",objOtherStructuresInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now );
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",System.DBNull.Value);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",objOtherStructuresInfo.OTHER_STRUCTURE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objOtherStructuresInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddOtherStructures.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOtherStructuresInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objOtherStructuresInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objOtherStructuresInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objOtherStructuresInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objOtherStructuresInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Other structure details has been added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int OTHER_STRUCTURE_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (OTHER_STRUCTURE_ID == -1)
				{
					return -1;
				}
				else
				{
					objOtherStructuresInfo.OTHER_STRUCTURE_ID = OTHER_STRUCTURE_ID;
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
		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objOtherStructuresInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsOtherStructuresInfo objOtherStructuresInfo,string strcalledFrom)
		{
			string		strStoredProc	=	"Proc_InsertAPP_OTHER_STRUCTURE_DWELLING";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOtherStructuresInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOtherStructuresInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOtherStructuresInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOtherStructuresInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@PREMISES_LOCATION",objOtherStructuresInfo.PREMISES_LOCATION);
				objDataWrapper.AddParameter("@PREMISES_DESCRIPTION",objOtherStructuresInfo.PREMISES_DESCRIPTION);
				objDataWrapper.AddParameter("@PREMISES_USE",objOtherStructuresInfo.PREMISES_USE);
				objDataWrapper.AddParameter("@PREMISES_CONDITION",objOtherStructuresInfo.PREMISES_CONDITION);
				objDataWrapper.AddParameter("@PICTURE_ATTACHED",objOtherStructuresInfo.PICTURE_ATTACHED);
				objDataWrapper.AddParameter("@COVERAGE_BASIS",objOtherStructuresInfo.COVERAGE_BASIS);
				objDataWrapper.AddParameter("@SATELLITE_EQUIPMENT",objOtherStructuresInfo.SATELLITE_EQUIPMENT);
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objOtherStructuresInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objOtherStructuresInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objOtherStructuresInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objOtherStructuresInfo.LOCATION_ZIP);
				
				objDataWrapper.AddParameter("@ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED));
				objDataWrapper.AddParameter("@INSURING_VALUE",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE));
				objDataWrapper.AddParameter("@INSURING_VALUE_OFF_PREMISES",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE_OFF_PREMISES));

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@CREATED_BY",objOtherStructuresInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOtherStructuresInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				if(objOtherStructuresInfo.COVERAGE_AMOUNT==0 || objOtherStructuresInfo.COVERAGE_AMOUNT==0.0)
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",null);
				else
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",objOtherStructuresInfo.COVERAGE_AMOUNT);
				objDataWrapper.AddParameter("@LIABILITY_EXTENDED",objOtherStructuresInfo.LIABILITY_EXTENDED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",objOtherStructuresInfo.OTHER_STRUCTURE_ID,SqlDbType.Int,ParameterDirection.Output);

				objDataWrapper.AddParameter("@SOLID_FUEL_DEVICE",objOtherStructuresInfo.SOLID_FUEL_DEVICE); //Added by Charles on 27-Nov-09 for Itrack 6681
				objDataWrapper.AddParameter("@APPLY_ENDS",objOtherStructuresInfo.APPLY_ENDS);//Added by Charles on 3-Dec-09 for Itrack 6405

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objOtherStructuresInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddOtherStructures.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOtherStructuresInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objOtherStructuresInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objOtherStructuresInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objOtherStructuresInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objOtherStructuresInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Other structure details has been added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int OTHER_STRUCTURE_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();

				ClsHomeCoverages objCoverages;
				
				if(strcalledFrom.ToString().Trim().ToUpper() == "RENTAL")
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}

				objCoverages.UpdateCoveragesByRuleApp(objDataWrapper,objOtherStructuresInfo.CUSTOMER_ID,
													objOtherStructuresInfo.APP_ID,
													objOtherStructuresInfo.APP_VERSION_ID,
													RuleType.RiskDependent,
													objOtherStructuresInfo.DWELLING_ID );

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (OTHER_STRUCTURE_ID == -1)
				{
					return -1;
				}
				else
				{
					objOtherStructuresInfo.OTHER_STRUCTURE_ID = OTHER_STRUCTURE_ID;
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
		/// Saves the information passed in model object to database at Policy level.
		/// </summary>
		/// <param name="objOtherStructuresInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add_Pol(Cms.Model.Policy.Homeowners.ClsPolicyOtherStructuresInfo objOtherStructuresInfo)
		{
			string		strStoredProc	=	"Proc_InsertPOL_OTHER_STRUCTURE_DWELLING";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOtherStructuresInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objOtherStructuresInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objOtherStructuresInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOtherStructuresInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@PREMISES_LOCATION",objOtherStructuresInfo.PREMISES_LOCATION);
				objDataWrapper.AddParameter("@PREMISES_DESCRIPTION",objOtherStructuresInfo.PREMISES_DESCRIPTION);
				objDataWrapper.AddParameter("@PREMISES_USE",objOtherStructuresInfo.PREMISES_USE);
				objDataWrapper.AddParameter("@PREMISES_CONDITION",objOtherStructuresInfo.PREMISES_CONDITION);
				objDataWrapper.AddParameter("@PICTURE_ATTACHED",objOtherStructuresInfo.PICTURE_ATTACHED);
				objDataWrapper.AddParameter("@COVERAGE_BASIS",objOtherStructuresInfo.COVERAGE_BASIS);
				objDataWrapper.AddParameter("@SATELLITE_EQUIPMENT",objOtherStructuresInfo.SATELLITE_EQUIPMENT);
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objOtherStructuresInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objOtherStructuresInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objOtherStructuresInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objOtherStructuresInfo.LOCATION_ZIP);
				
				objDataWrapper.AddParameter("@ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED));
				objDataWrapper.AddParameter("@INSURING_VALUE",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE));
				objDataWrapper.AddParameter("@INSURING_VALUE_OFF_PREMISES",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE_OFF_PREMISES));

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@CREATED_BY",objOtherStructuresInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOtherStructuresInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				if(objOtherStructuresInfo.COVERAGE_AMOUNT==0 || objOtherStructuresInfo.COVERAGE_AMOUNT==0.0)
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",null);
				else
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",objOtherStructuresInfo.COVERAGE_AMOUNT);
				objDataWrapper.AddParameter("@LIABILITY_EXTENDED",objOtherStructuresInfo.LIABILITY_EXTENDED);
				objDataWrapper.AddParameter("@SOLID_FUEL_DEVICE",objOtherStructuresInfo.SOLID_FUEL_DEVICE); //Added by Charles on 27-Nov-09 for Itrack 6681
				objDataWrapper.AddParameter("@APPLY_ENDS",objOtherStructuresInfo.APPLY_ENDS); //Added by Charles on 3-Dec-09 for Itrack 6405
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",objOtherStructuresInfo.OTHER_STRUCTURE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;				
				if(TransactionLogRequired)
				{
					objOtherStructuresInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyAddOtherStructures.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOtherStructuresInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID = objOtherStructuresInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objOtherStructuresInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objOtherStructuresInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objOtherStructuresInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Other structure details has been added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}		
				
				int OTHER_STRUCTURE_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				
				ClsHomeCoverages objCoverages;
				
				if(objOtherStructuresInfo.CalledFrom.ToString().ToUpper().Equals("RENTAL"))
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}

                

				objCoverages.UpdateCoveragesByRulePolicy(objDataWrapper,objOtherStructuresInfo.CUSTOMER_ID,
					objOtherStructuresInfo.POLICY_ID ,
					objOtherStructuresInfo.POLICY_VERSION_ID, 
					RuleType.RiskDependent,
					objOtherStructuresInfo.DWELLING_ID);

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (OTHER_STRUCTURE_ID == -1)
				{
					return -1;
				}
				else
				{
					objOtherStructuresInfo.OTHER_STRUCTURE_ID = OTHER_STRUCTURE_ID;
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

		#region Delete method
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objOldOtherStructuresInfo"></param>
		/// <returns></returns>
		public int Delete(ClsOtherStructuresInfo objOldOtherStructuresInfo,string strcalledFrom)
		{
			string		strStoredProc	=	"PROC_DELETE_OTHER_STRUCTURE_INFO";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldOtherStructuresInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOldOtherStructuresInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldOtherStructuresInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOldOtherStructuresInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",objOldOtherStructuresInfo.OTHER_STRUCTURE_ID);
				SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					
					objOldOtherStructuresInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddOtherStructures.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objOldOtherStructuresInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objOldOtherStructuresInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objOldOtherStructuresInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objOldOtherStructuresInfo.MODIFIED_BY;					
					objTransactionInfo.TRANS_DESC		=	"Other Structure Info Has Been Deleted";
					objTransactionInfo.CUSTOM_INFO		=	";Dwelling ID = " + objOldOtherStructuresInfo.DWELLING_ID + ";Other Structure ID = " + objOldOtherStructuresInfo.OTHER_STRUCTURE_ID;					
					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
							
				ClsHomeCoverages objCoverages;
				
				if(strcalledFrom.ToString().Trim().ToUpper() == "RENTAL")
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}

				objCoverages.UpdateCoveragesByRuleApp(objDataWrapper,objOldOtherStructuresInfo.CUSTOMER_ID,
					objOldOtherStructuresInfo.APP_ID,
					objOldOtherStructuresInfo.APP_VERSION_ID,
					RuleType.RiskDependent,
					objOldOtherStructuresInfo.DWELLING_ID );
				

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return int.Parse(sqlParamRetVal.Value.ToString());
			}
			catch(Exception ex)
			{
				throw(ex);
				return -1;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		/// <summary>
		/// Policy level
		/// </summary>
		/// <param name="objOldOtherStructuresInfo"></param>
		/// <returns></returns>
		public int Delete_Pol(ClsPolicyOtherStructuresInfo objOldOtherStructuresInfo)
		{
			string		strStoredProc	=	"PROC_POL_DELETE_OTHER_STRUCTURE_INFO";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldOtherStructuresInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objOldOtherStructuresInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objOldOtherStructuresInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOldOtherStructuresInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",objOldOtherStructuresInfo.OTHER_STRUCTURE_ID);
				SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					objOldOtherStructuresInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyAddOtherStructures.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objOldOtherStructuresInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objOldOtherStructuresInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objOldOtherStructuresInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objOldOtherStructuresInfo.MODIFIED_BY;					
					objTransactionInfo.TRANS_DESC		=	"Other Structure Info Has Been Deleted";
					objTransactionInfo.CUSTOM_INFO		=	";Dwelling ID = " + objOldOtherStructuresInfo.DWELLING_ID + ";Other Structure ID = " + objOldOtherStructuresInfo.OTHER_STRUCTURE_ID;					
					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				ClsHomeCoverages objCoverages;
				
				if(objOldOtherStructuresInfo.CalledFrom.ToString().ToUpper().Equals("RENTAL"))
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}

                

				objCoverages.UpdateCoveragesByRulePolicy(objDataWrapper,objOldOtherStructuresInfo.CUSTOMER_ID,
					objOldOtherStructuresInfo.POLICY_ID ,
					objOldOtherStructuresInfo.POLICY_VERSION_ID, 
					RuleType.RiskDependent,
					objOldOtherStructuresInfo.DWELLING_ID);
			
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return int.Parse(sqlParamRetVal.Value.ToString());
			}
			catch(Exception ex)
			{
				throw(ex);
				//return -1;
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
		/// <param name="objOldOtherStructuresInfo">Model object having old information</param>
		/// <param name="objOtherStructuresInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsOtherStructuresInfo objOldOtherStructuresInfo,ClsOtherStructuresInfo objOtherStructuresInfo,string strcalledFrom)
		{
			string		strStoredProc	=	"Proc_UpdateAPP_OTHER_STRUCTURE_DWELLING";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOtherStructuresInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOtherStructuresInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOtherStructuresInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOtherStructuresInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",objOtherStructuresInfo.OTHER_STRUCTURE_ID);
				objDataWrapper.AddParameter("@PREMISES_LOCATION",objOtherStructuresInfo.PREMISES_LOCATION);
				objDataWrapper.AddParameter("@PREMISES_DESCRIPTION",objOtherStructuresInfo.PREMISES_DESCRIPTION);
				objDataWrapper.AddParameter("@PREMISES_USE",objOtherStructuresInfo.PREMISES_USE);
				objDataWrapper.AddParameter("@PREMISES_CONDITION",objOtherStructuresInfo.PREMISES_CONDITION);
				objDataWrapper.AddParameter("@PICTURE_ATTACHED",objOtherStructuresInfo.PICTURE_ATTACHED);
				objDataWrapper.AddParameter("@COVERAGE_BASIS",objOtherStructuresInfo.COVERAGE_BASIS);
				objDataWrapper.AddParameter("@SATELLITE_EQUIPMENT",objOtherStructuresInfo.SATELLITE_EQUIPMENT);
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objOtherStructuresInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objOtherStructuresInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objOtherStructuresInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objOtherStructuresInfo.LOCATION_ZIP);

				objDataWrapper.AddParameter("@ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED));
				objDataWrapper.AddParameter("@INSURING_VALUE",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE));
				objDataWrapper.AddParameter("@INSURING_VALUE_OFF_PREMISES",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE_OFF_PREMISES));

				objDataWrapper.AddParameter("@MODIFIED_BY",objOtherStructuresInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOtherStructuresInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@SOLID_FUEL_DEVICE",objOtherStructuresInfo.SOLID_FUEL_DEVICE); //Added by Charles on 27-Nov-09 for Itrack 6681
				objDataWrapper.AddParameter("@APPLY_ENDS",objOtherStructuresInfo.APPLY_ENDS); //Added by Charles on 3-Dec-09 for Itrack 6405

				if(objOtherStructuresInfo.COVERAGE_AMOUNT==0 || objOtherStructuresInfo.COVERAGE_AMOUNT==0.0)
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",null);
				else
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",objOtherStructuresInfo.COVERAGE_AMOUNT);
				objDataWrapper.AddParameter("@LIABILITY_EXTENDED",objOtherStructuresInfo.LIABILITY_EXTENDED);
				if(base.TransactionLogRequired) 
				{
					objOtherStructuresInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddOtherStructures.aspx.resx");
					objBuilder.GetUpdateSQL(objOldOtherStructuresInfo,objOtherStructuresInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objOtherStructuresInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objOtherStructuresInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objOtherStructuresInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objOtherStructuresInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Other Structures Detail Has Been Modified";
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='INSURING_VALUE' and @NewValue='-1']","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='INSURING_VALUE' and @OldValue='-1']","OldValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED' and @NewValue='-1']","NewValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED' and @OldValue='-1']","OldValue","null");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='SATELLITE_EQUIPMENT' and @NewValue='10964']","NewValue","No");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='SATELLITE_EQUIPMENT' and @OldValue='10964']","OldValue","No");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='SATELLITE_EQUIPMENT' and @NewValue='10963']","NewValue","Yes");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='SATELLITE_EQUIPMENT' and @OldValue='10963']","OldValue","Yes");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='PICTURE_ATTACHED' and @NewValue='10964']","NewValue","No");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='PICTURE_ATTACHED' and @OldValue='10964']","OldValue","No");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='PICTURE_ATTACHED' and @NewValue='10963']","NewValue","Yes");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='PICTURE_ATTACHED' and @OldValue='10963']","OldValue","Yes");

					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				objDataWrapper.ClearParameteres();
				
				ClsHomeCoverages objCoverages;
				if(strcalledFrom.ToString().Trim().ToUpper() == "RENTAL")
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}

				objCoverages.UpdateCoveragesByRuleApp(objDataWrapper,objOtherStructuresInfo.CUSTOMER_ID,
					objOtherStructuresInfo.APP_ID,
					objOtherStructuresInfo.APP_VERSION_ID,
					RuleType.RiskDependent,
					objOtherStructuresInfo.DWELLING_ID );
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
		/// <param name="objOldOtherStructuresInfo">Model object having old information</param>
		/// <param name="objOtherStructuresInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update_Pol(ClsPolicyOtherStructuresInfo objOldOtherStructuresInfo,ClsPolicyOtherStructuresInfo objOtherStructuresInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_OTHER_STRUCTURE_DWELLING";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOtherStructuresInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objOtherStructuresInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objOtherStructuresInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOtherStructuresInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",objOtherStructuresInfo.OTHER_STRUCTURE_ID);
				objDataWrapper.AddParameter("@PREMISES_LOCATION",objOtherStructuresInfo.PREMISES_LOCATION);
				objDataWrapper.AddParameter("@PREMISES_DESCRIPTION",objOtherStructuresInfo.PREMISES_DESCRIPTION);
				objDataWrapper.AddParameter("@PREMISES_USE",objOtherStructuresInfo.PREMISES_USE);
				objDataWrapper.AddParameter("@PREMISES_CONDITION",objOtherStructuresInfo.PREMISES_CONDITION);
				objDataWrapper.AddParameter("@PICTURE_ATTACHED",objOtherStructuresInfo.PICTURE_ATTACHED);
				objDataWrapper.AddParameter("@COVERAGE_BASIS",objOtherStructuresInfo.COVERAGE_BASIS);
				objDataWrapper.AddParameter("@SATELLITE_EQUIPMENT",objOtherStructuresInfo.SATELLITE_EQUIPMENT);
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objOtherStructuresInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objOtherStructuresInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objOtherStructuresInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objOtherStructuresInfo.LOCATION_ZIP);

				objDataWrapper.AddParameter("@ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED));
				objDataWrapper.AddParameter("@INSURING_VALUE",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE));
				objDataWrapper.AddParameter("@INSURING_VALUE_OFF_PREMISES",DefaultValues.GetDoubleNullFromNegative(objOtherStructuresInfo.INSURING_VALUE_OFF_PREMISES));

				objDataWrapper.AddParameter("@MODIFIED_BY",objOtherStructuresInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOtherStructuresInfo.LAST_UPDATED_DATETIME);
				if(objOtherStructuresInfo.COVERAGE_AMOUNT==0 || objOtherStructuresInfo.COVERAGE_AMOUNT==0.0)
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",null);
				else
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",objOtherStructuresInfo.COVERAGE_AMOUNT);
				objDataWrapper.AddParameter("@LIABILITY_EXTENDED",objOtherStructuresInfo.LIABILITY_EXTENDED);
				objDataWrapper.AddParameter("@SOLID_FUEL_DEVICE",objOtherStructuresInfo.SOLID_FUEL_DEVICE); //Added by Charles on 27-Nov-09 for Itrack 6681
				objDataWrapper.AddParameter("@APPLY_ENDS",objOtherStructuresInfo.APPLY_ENDS); //Added by Charles on 3-Dec-09 for Itrack 6405
				if(base.TransactionLogRequired) 
				{
					objOtherStructuresInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyAddOtherStructures.aspx.resx");
					objBuilder.GetUpdateSQL(objOldOtherStructuresInfo,objOtherStructuresInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID = objOtherStructuresInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objOtherStructuresInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objOtherStructuresInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objOtherStructuresInfo.MODIFIED_BY;
				
					objTransactionInfo.TRANS_DESC		=	"Other Structures Detail Has Been Modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.ClearParameteres();

				ClsHomeCoverages objCoverages;
				
				if(objOtherStructuresInfo.CalledFrom.ToString().ToUpper().Equals("RENTAL"))
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}

                

				objCoverages.UpdateCoveragesByRulePolicy(objDataWrapper,objOtherStructuresInfo.CUSTOMER_ID,
					objOtherStructuresInfo.POLICY_ID ,
					objOtherStructuresInfo.POLICY_VERSION_ID, 
					RuleType.RiskDependent,
					objOtherStructuresInfo.DWELLING_ID);

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
		public static string GetXmlForPageControls(int CUSTOMER_ID,int APP_ID,int APP_VERSION_ID,int DWELLING_ID,  int OTHER_STRUCTURE_ID)
		{
			string strSql = "Proc_GetXMLAPP_OTHER_STRUCTURE_DWELLING";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);
			objDataWrapper.AddParameter("@DWELLING_ID",DWELLING_ID);
			objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",OTHER_STRUCTURE_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if (objDataSet.Tables[0].Rows.Count > 0)
			{
				return objDataSet.GetXml();
			}
			else
			{
				return "";
			}
		}

		public static string GetXmlForPageControls_Pol(int CUSTOMER_ID,int POL_ID,int POL_VERSION_ID,int DWELLING_ID,  int OTHER_STRUCTURE_ID)
		{
			string strSql = "Proc_GetXMLPOL_OTHER_STRUCTURE_DWELLING";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@POL_ID",POL_ID);
			objDataWrapper.AddParameter("@POL_VERSION_ID",POL_VERSION_ID);
			objDataWrapper.AddParameter("@DWELLING_ID",DWELLING_ID);
			objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",OTHER_STRUCTURE_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if (objDataSet.Tables[0].Rows.Count > 0)
			{
				return objDataSet.GetXml();
			}
			else
			{
				return "";
			}
		}


		#endregion


		public int ActivateDeActivateAppOtherStructureDetails(ClsOtherStructuresInfo objModel,bool blnActivate,string strcalledFrom)
		{
			string strProcName = "";
			if (blnActivate == true)
				strProcName = "Proc_ActivateAppOtherStructureDetails";
			else
				strProcName = "Proc_DeActivateAppOtherStructureDetails";

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objModel.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objModel.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objModel.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objModel.DWELLING_ID);
				objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",objModel.OTHER_STRUCTURE_ID);

				int retVal = objDataWrapper.ExecuteNonQuery(strProcName);
				ClsHomeCoverages objCoverages;
				
				if(strcalledFrom.ToString().Trim().ToUpper() == "RENTAL")
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}

				objCoverages.UpdateCoveragesByRuleApp(objDataWrapper,objModel.CUSTOMER_ID,
					objModel.APP_ID,
					objModel.APP_VERSION_ID,
					RuleType.RiskDependent,
					objModel.DWELLING_ID );
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return retVal;
			}
			catch (Exception excep)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
				throw(excep);
			}
		}

		public int ActivateDeActivatePolOtherStructureDetails(ClsPolicyOtherStructuresInfo objModel,bool blnActivate)
		{
			string strProcName = "";
			if (blnActivate == true)
				strProcName = "Proc_ActivatePolOtherStructureDetails";
			else
				strProcName = "Proc_DeActivatePolOtherStructureDetails";

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objModel.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objModel.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objModel.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objModel.DWELLING_ID);
				objDataWrapper.AddParameter("@OTHER_STRUCTURE_ID",objModel.OTHER_STRUCTURE_ID);

				int retVal = objDataWrapper.ExecuteNonQuery(strProcName);
				objDataWrapper.ClearParameteres();
				
				ClsHomeCoverages objCoverages;
				
				if(objModel.CalledFrom.ToString().ToUpper().Equals("RENTAL"))
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}

                

				objCoverages.UpdateCoveragesByRulePolicy(objDataWrapper,objModel.CUSTOMER_ID,
					objModel.POLICY_ID ,
					objModel.POLICY_VERSION_ID, 
					RuleType.RiskDependent,
					objModel.DWELLING_ID);

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return retVal;
			}
			catch (Exception excep)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
				throw(excep);
			}
		}
	}
}
