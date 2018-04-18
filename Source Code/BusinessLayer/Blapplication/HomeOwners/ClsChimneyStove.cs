/******************************************************************************************
<Author				: -   
<Start Date				: -	5/20/2005 3:36:59 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  18-11-2005
<Modified By			: -  Vijay Arora
<Purpose				: - Added the Policy Functions. 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Application.HomeOwners;
namespace Cms.BusinessLayer.BlApplication.HomeOwners
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsChimneyStove : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_HOME_OWNER_CHIMNEY_STOVE	=	"APP_HOME_OWNER_CHIMNEY_STOVE";
		private const	string		INSERT_PROC						=	"Proc_InsertAPP_HOME_OWNER_CHIMNEY_STOVE";
		private const	string		UPDATE_PROC						=	"Proc_UpdateAPP_HOME_OWNER_CHIMNEY_STOVE";		
		private int					intLoggedinUser;

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		
		private const string ACTIVATE_DEACTIVATE_PROC	= "PROC_ACTIVATEDEACTIVATEAPP_HOME_OWNER_CHIMNEY_STOVE";
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

		public int LoggedinUserId
		{
			set
			{
				intLoggedinUser		=	value;
			}
			get
			{
				return intLoggedinUser;
			}
		}
		#endregion

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsChimneyStove()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objChimneyStoveInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsChimneyStoveInfo objChimneyStoveInfo)
		{
			//string		strStoredProc	=	"Proc_InsertAPP_HOME_OWNER_CHIMNEY_STOVE";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objChimneyStoveInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objChimneyStoveInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objChimneyStoveInfo.APP_VERSION_ID);

				objDataWrapper.AddParameter("@FUEL_ID",objChimneyStoveInfo.FUEL_ID);

				if(objChimneyStoveInfo.IS_STOVE_VENTED !=null)
					objDataWrapper.AddParameter("@IS_STOVE_VENTED",objChimneyStoveInfo.IS_STOVE_VENTED);

				if(objChimneyStoveInfo.OTHER_DEVICES_ATTACHED!=null)
					objDataWrapper.AddParameter("@OTHER_DEVICES_ATTACHED",objChimneyStoveInfo.OTHER_DEVICES_ATTACHED);

				if(objChimneyStoveInfo.CHIMNEY_CONSTRUCTION!=null)
					objDataWrapper.AddParameter("@CHIMNEY_CONSTRUCTION",objChimneyStoveInfo.CHIMNEY_CONSTRUCTION);
				
				if(objChimneyStoveInfo.CONSTRUCT_OTHER_DESC!=null)
					objDataWrapper.AddParameter("@CONSTRUCT_OTHER_DESC",objChimneyStoveInfo.CONSTRUCT_OTHER_DESC);
				
				if(objChimneyStoveInfo.IS_TILE_FLUE_LINING!=null)
					objDataWrapper.AddParameter("@IS_TILE_FLUE_LINING",objChimneyStoveInfo.IS_TILE_FLUE_LINING);

				if(objChimneyStoveInfo.IS_CHIMNEY_GROUND_UP!=null)
					objDataWrapper.AddParameter("@IS_CHIMNEY_GROUND_UP",objChimneyStoveInfo.IS_CHIMNEY_GROUND_UP);

				if(objChimneyStoveInfo.CHIMNEY_INST_AFTER_HOUSE_BLT!=null)
					objDataWrapper.AddParameter("@CHIMNEY_INST_AFTER_HOUSE_BLT",objChimneyStoveInfo.CHIMNEY_INST_AFTER_HOUSE_BLT);

				if(objChimneyStoveInfo.IS_CHIMNEY_COVERED!=null)
					objDataWrapper.AddParameter("@IS_CHIMNEY_COVERED",objChimneyStoveInfo.IS_CHIMNEY_COVERED);

				objDataWrapper.AddParameter("@DIST_FROM_SMOKE_PIPE",DefaultValues.GetIntNullFromNegative(objChimneyStoveInfo.DIST_FROM_SMOKE_PIPE));
				
				if(objChimneyStoveInfo.THIMBLE_OR_MATERIAL!=null)
					objDataWrapper.AddParameter("@THIMBLE_OR_MATERIAL",objChimneyStoveInfo.THIMBLE_OR_MATERIAL);
				
				if(objChimneyStoveInfo.STOVE_PIPE_IS!=null)
					objDataWrapper.AddParameter("@STOVE_PIPE_IS",objChimneyStoveInfo.STOVE_PIPE_IS);
				
				if(objChimneyStoveInfo.DOES_SMOKE_PIPE_FIT!=null)
					objDataWrapper.AddParameter("@DOES_SMOKE_PIPE_FIT",objChimneyStoveInfo.DOES_SMOKE_PIPE_FIT);
				
				objDataWrapper.AddParameter("@SMOKE_PIPE_WASTE_HEAT",objChimneyStoveInfo.SMOKE_PIPE_WASTE_HEAT);
				
				if(objChimneyStoveInfo.STOVE_CONN_SECURE!=null)
					objDataWrapper.AddParameter("@STOVE_CONN_SECURE",objChimneyStoveInfo.STOVE_CONN_SECURE);
				
				if(objChimneyStoveInfo.SMOKE_PIPE_PASS!=null)
					objDataWrapper.AddParameter("@SMOKE_PIPE_PASS",objChimneyStoveInfo.SMOKE_PIPE_PASS);
				
				if(objChimneyStoveInfo.SELECT_PASS!=null)
					objDataWrapper.AddParameter("@SELECT_PASS",objChimneyStoveInfo.SELECT_PASS);
				
				objDataWrapper.AddParameter("@PASS_INCHES",DefaultValues.GetDoubleNullFromNegative(objChimneyStoveInfo.PASS_INCHES));//Changed from GetIntNullFromNegative by Charles on 21-Oct-09 for Itrack 6599
				
				objDataWrapper.AddParameter("@IS_ACTIVE",objChimneyStoveInfo.IS_ACTIVE);
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
																						
					objChimneyStoveInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\HomeOwners\AddChimneyStove.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objChimneyStoveInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	LoggedinUserId;
					objTransactionInfo.APP_ID			=	objChimneyStoveInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objChimneyStoveInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objChimneyStoveInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New chimney stove information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_PROC,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_PROC);
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
		/// <param name="objOldChimneyStoveInfo">Model object having old information</param>
		/// <param name="objChimneyStoveInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsChimneyStoveInfo objOldChimneyStoveInfo,ClsChimneyStoveInfo objChimneyStoveInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objChimneyStoveInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objChimneyStoveInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objChimneyStoveInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@FUEL_ID",objChimneyStoveInfo.FUEL_ID);
				objDataWrapper.AddParameter("@IS_STOVE_VENTED",objChimneyStoveInfo.IS_STOVE_VENTED);
				objDataWrapper.AddParameter("@OTHER_DEVICES_ATTACHED",objChimneyStoveInfo.OTHER_DEVICES_ATTACHED);
				objDataWrapper.AddParameter("@CHIMNEY_CONSTRUCTION",objChimneyStoveInfo.CHIMNEY_CONSTRUCTION);
				objDataWrapper.AddParameter("@CONSTRUCT_OTHER_DESC",objChimneyStoveInfo.CONSTRUCT_OTHER_DESC);
				objDataWrapper.AddParameter("@IS_TILE_FLUE_LINING",objChimneyStoveInfo.IS_TILE_FLUE_LINING);
				objDataWrapper.AddParameter("@IS_CHIMNEY_GROUND_UP",objChimneyStoveInfo.IS_CHIMNEY_GROUND_UP);
				objDataWrapper.AddParameter("@CHIMNEY_INST_AFTER_HOUSE_BLT",objChimneyStoveInfo.CHIMNEY_INST_AFTER_HOUSE_BLT);
				objDataWrapper.AddParameter("@IS_CHIMNEY_COVERED",objChimneyStoveInfo.IS_CHIMNEY_COVERED);
				objDataWrapper.AddParameter("@DIST_FROM_SMOKE_PIPE",DefaultValues.GetIntNullFromNegative(objChimneyStoveInfo.DIST_FROM_SMOKE_PIPE));
				objDataWrapper.AddParameter("@THIMBLE_OR_MATERIAL",objChimneyStoveInfo.THIMBLE_OR_MATERIAL);
				objDataWrapper.AddParameter("@STOVE_PIPE_IS",objChimneyStoveInfo.STOVE_PIPE_IS);
				objDataWrapper.AddParameter("@DOES_SMOKE_PIPE_FIT",objChimneyStoveInfo.DOES_SMOKE_PIPE_FIT);
				objDataWrapper.AddParameter("@SMOKE_PIPE_WASTE_HEAT",objChimneyStoveInfo.SMOKE_PIPE_WASTE_HEAT);
				objDataWrapper.AddParameter("@STOVE_CONN_SECURE",objChimneyStoveInfo.STOVE_CONN_SECURE);
				objDataWrapper.AddParameter("@SMOKE_PIPE_PASS",objChimneyStoveInfo.SMOKE_PIPE_PASS);
				objDataWrapper.AddParameter("@SELECT_PASS",objChimneyStoveInfo.SELECT_PASS);
				objDataWrapper.AddParameter("@PASS_INCHES",DefaultValues.GetDoubleNullFromNegative(objChimneyStoveInfo.PASS_INCHES));//Changed from GetIntNullFromNegative by Charles on 21-Oct-09 for Itrack 6599
				
				if(TransactionLog) 
				{
					objChimneyStoveInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\HomeOwners\AddChimneyStove.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldChimneyStoveInfo,objChimneyStoveInfo);					
					if(strTranXML=="<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="") 
						returnResult = objDataWrapper.ExecuteNonQuery(UPDATE_PROC);
					else
					{

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	LoggedinUserId;
						objTransactionInfo.APP_ID			=	objChimneyStoveInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objChimneyStoveInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objChimneyStoveInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Chimney stove information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(UPDATE_PROC,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(UPDATE_PROC);
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

		#region GetChimneyInformationXml
		public static string GetChimneyInformationXml(int intCustoemrId, int intAppId, int intAppVersionId,int intFuelId)
		{
			string strStoredProc = "Proc_GetAPP_HOME_OWNER_CHIMNEY_STOVE";
			DataSet dsChimneyInformation = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@FUEL_ID",intFuelId);


				dsChimneyInformation= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsChimneyInformation.Tables[0].Rows.Count != 0)
				{
					return dsChimneyInformation.GetXml();
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

		#region IDisposable Members

		public void Dispose()
		{
			// TODO:  Add ClsChimneyStove.Dispose implementation
		}

		#endregion

	/*	public int ActivateDeactivate(ClsChimneyStoveInfo objChimneyStoveInfo,string CODE)
		{
			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objChimneyStoveInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objChimneyStoveInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objChimneyStoveInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@FUEL_ID",objChimneyStoveInfo.FUEL_ID);				
				objDataWrapper.AddParameter("@IS_ACTIVE",CODE);				
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
																						
					objChimneyStoveInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\HomeOwners\AddChimneyStove.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objChimneyStoveInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	LoggedinUserId;
					objTransactionInfo.APP_ID			=	objChimneyStoveInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objChimneyStoveInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objChimneyStoveInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Chimney stove information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(ACTIVATE_DEACTIVATE_PROC,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(ACTIVATE_DEACTIVATE_PROC);
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
		}	 */

		#region Policy Functions
		/// <summary>
		/// Returns the Policy Chimney Stove Details.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="fuelID"></param>
		/// <returns></returns>
		public static string GetPolicyChimneyInformationXml(int customerID, int policyID, int policyVersionID,int fuelID)
		{
			string strStoredProc = "Proc_Get_POL_HOME_OWNER_CHIMNEY_STOVE";
			DataSet dsChimneyInformation = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@POLICY_ID",policyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
				objDataWrapper.AddParameter("@FUEL_ID",fuelID);

				dsChimneyInformation= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsChimneyInformation.Tables[0].Rows.Count != 0)
				{
					return dsChimneyInformation.GetXml();
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
		/// Saves the Policy Chimney Stove Details.
		/// </summary>
		/// <param name="objChimneyStoveInfo"></param>
		/// <returns></returns>
		public int AddPolicyChimneyStove(Cms.Model.Policy.Homeowners.ClsPolicyChimneyStoveInfo objChimneyStoveInfo)
		{
		
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objChimneyStoveInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objChimneyStoveInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objChimneyStoveInfo.POLICY_VERSION_ID);

				objDataWrapper.AddParameter("@FUEL_ID",objChimneyStoveInfo.FUEL_ID);

				if(objChimneyStoveInfo.IS_STOVE_VENTED !=null)
					objDataWrapper.AddParameter("@IS_STOVE_VENTED",objChimneyStoveInfo.IS_STOVE_VENTED);

				if(objChimneyStoveInfo.OTHER_DEVICES_ATTACHED!=null)
					objDataWrapper.AddParameter("@OTHER_DEVICES_ATTACHED",objChimneyStoveInfo.OTHER_DEVICES_ATTACHED);

				if(objChimneyStoveInfo.CHIMNEY_CONSTRUCTION!=null)
					objDataWrapper.AddParameter("@CHIMNEY_CONSTRUCTION",objChimneyStoveInfo.CHIMNEY_CONSTRUCTION);
				
				if(objChimneyStoveInfo.CONSTRUCT_OTHER_DESC!=null)
					objDataWrapper.AddParameter("@CONSTRUCT_OTHER_DESC",objChimneyStoveInfo.CONSTRUCT_OTHER_DESC);
				
				if(objChimneyStoveInfo.IS_TILE_FLUE_LINING!=null)
					objDataWrapper.AddParameter("@IS_TILE_FLUE_LINING",objChimneyStoveInfo.IS_TILE_FLUE_LINING);

				if(objChimneyStoveInfo.IS_CHIMNEY_GROUND_UP!=null)
					objDataWrapper.AddParameter("@IS_CHIMNEY_GROUND_UP",objChimneyStoveInfo.IS_CHIMNEY_GROUND_UP);

				if(objChimneyStoveInfo.CHIMNEY_INST_AFTER_HOUSE_BLT!=null)
					objDataWrapper.AddParameter("@CHIMNEY_INST_AFTER_HOUSE_BLT",objChimneyStoveInfo.CHIMNEY_INST_AFTER_HOUSE_BLT);

				if(objChimneyStoveInfo.IS_CHIMNEY_COVERED!=null)
					objDataWrapper.AddParameter("@IS_CHIMNEY_COVERED",objChimneyStoveInfo.IS_CHIMNEY_COVERED);

				objDataWrapper.AddParameter("@DIST_FROM_SMOKE_PIPE",DefaultValues.GetIntNullFromNegative(objChimneyStoveInfo.DIST_FROM_SMOKE_PIPE));
				
				if(objChimneyStoveInfo.THIMBLE_OR_MATERIAL!=null)
					objDataWrapper.AddParameter("@THIMBLE_OR_MATERIAL",objChimneyStoveInfo.THIMBLE_OR_MATERIAL);
				
				if(objChimneyStoveInfo.STOVE_PIPE_IS!=null)
					objDataWrapper.AddParameter("@STOVE_PIPE_IS",objChimneyStoveInfo.STOVE_PIPE_IS);
				
				if(objChimneyStoveInfo.DOES_SMOKE_PIPE_FIT!=null)
					objDataWrapper.AddParameter("@DOES_SMOKE_PIPE_FIT",objChimneyStoveInfo.DOES_SMOKE_PIPE_FIT);
				
				objDataWrapper.AddParameter("@SMOKE_PIPE_WASTE_HEAT",objChimneyStoveInfo.SMOKE_PIPE_WASTE_HEAT);
				
				if(objChimneyStoveInfo.STOVE_CONN_SECURE!=null)
					objDataWrapper.AddParameter("@STOVE_CONN_SECURE",objChimneyStoveInfo.STOVE_CONN_SECURE);
				
				if(objChimneyStoveInfo.SMOKE_PIPE_PASS!=null)
					objDataWrapper.AddParameter("@SMOKE_PIPE_PASS",objChimneyStoveInfo.SMOKE_PIPE_PASS);
				
				if(objChimneyStoveInfo.SELECT_PASS!=null)
					objDataWrapper.AddParameter("@SELECT_PASS",objChimneyStoveInfo.SELECT_PASS);
				
				objDataWrapper.AddParameter("@PASS_INCHES",DefaultValues.GetDoubleNullFromNegative(objChimneyStoveInfo.PASS_INCHES));//Changed from GetIntNullFromNegative by Charles on 21-Oct-09 for Itrack 6599
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
																						
					objChimneyStoveInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\HomeOwner\PolicyAddChimneyStove.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objChimneyStoveInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	LoggedinUserId;
					objTransactionInfo.POLICY_ID		=	objChimneyStoveInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objChimneyStoveInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objChimneyStoveInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New policy chimney stove information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPOL_HOME_OWNER_CHIMNEY_STOVE",objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPOL_HOME_OWNER_CHIMNEY_STOVE");
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
		/// Updates the policy Chimney Stove Details
		/// </summary>
		/// <param name="objOldChimneyStoveInfo"></param>
		/// <param name="objChimneyStoveInfo"></param>
		/// <returns></returns>
		public int UpdatePolicyChimneyStove(Cms.Model.Policy.Homeowners.ClsPolicyChimneyStoveInfo objOldChimneyStoveInfo, Cms.Model.Policy.Homeowners.ClsPolicyChimneyStoveInfo objChimneyStoveInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objChimneyStoveInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objChimneyStoveInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objChimneyStoveInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@FUEL_ID",objChimneyStoveInfo.FUEL_ID);
				objDataWrapper.AddParameter("@IS_STOVE_VENTED",objChimneyStoveInfo.IS_STOVE_VENTED);
				objDataWrapper.AddParameter("@OTHER_DEVICES_ATTACHED",objChimneyStoveInfo.OTHER_DEVICES_ATTACHED);
				objDataWrapper.AddParameter("@CHIMNEY_CONSTRUCTION",objChimneyStoveInfo.CHIMNEY_CONSTRUCTION);
				objDataWrapper.AddParameter("@CONSTRUCT_OTHER_DESC",objChimneyStoveInfo.CONSTRUCT_OTHER_DESC);
				objDataWrapper.AddParameter("@IS_TILE_FLUE_LINING",objChimneyStoveInfo.IS_TILE_FLUE_LINING);
				objDataWrapper.AddParameter("@IS_CHIMNEY_GROUND_UP",objChimneyStoveInfo.IS_CHIMNEY_GROUND_UP);
				objDataWrapper.AddParameter("@CHIMNEY_INST_AFTER_HOUSE_BLT",objChimneyStoveInfo.CHIMNEY_INST_AFTER_HOUSE_BLT);
				objDataWrapper.AddParameter("@IS_CHIMNEY_COVERED",objChimneyStoveInfo.IS_CHIMNEY_COVERED);
				objDataWrapper.AddParameter("@DIST_FROM_SMOKE_PIPE",DefaultValues.GetIntNullFromNegative(objChimneyStoveInfo.DIST_FROM_SMOKE_PIPE));
				objDataWrapper.AddParameter("@THIMBLE_OR_MATERIAL",objChimneyStoveInfo.THIMBLE_OR_MATERIAL);
				objDataWrapper.AddParameter("@STOVE_PIPE_IS",objChimneyStoveInfo.STOVE_PIPE_IS);
				objDataWrapper.AddParameter("@DOES_SMOKE_PIPE_FIT",objChimneyStoveInfo.DOES_SMOKE_PIPE_FIT);
				objDataWrapper.AddParameter("@SMOKE_PIPE_WASTE_HEAT",objChimneyStoveInfo.SMOKE_PIPE_WASTE_HEAT);
				objDataWrapper.AddParameter("@STOVE_CONN_SECURE",objChimneyStoveInfo.STOVE_CONN_SECURE);
				objDataWrapper.AddParameter("@SMOKE_PIPE_PASS",objChimneyStoveInfo.SMOKE_PIPE_PASS);
				objDataWrapper.AddParameter("@SELECT_PASS",objChimneyStoveInfo.SELECT_PASS);
				objDataWrapper.AddParameter("@PASS_INCHES",DefaultValues.GetDoubleNullFromNegative(objChimneyStoveInfo.PASS_INCHES));//Changed from GetIntNullFromNegative by Charles on 21-Oct-09 for Itrack 6599
				if(TransactionLog) 
				{
					objChimneyStoveInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\aspx\HomeOwner\PolicyAddChimneyStove.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldChimneyStoveInfo,objChimneyStoveInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePOL_HOME_OWNER_CHIMNEY_STOVE");
					else
					{

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	LoggedinUserId;
						objTransactionInfo.POLICY_ID 		=	objChimneyStoveInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	objChimneyStoveInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objChimneyStoveInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Chimney stove information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePOL_HOME_OWNER_CHIMNEY_STOVE",objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdatePOL_HOME_OWNER_CHIMNEY_STOVE");
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
	}
}
