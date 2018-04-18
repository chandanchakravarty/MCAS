/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	5/24/2006
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
	public class ClsWatercraftPropertyDamaged :  Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		CLM_WATERCRAFT_PROPERTY_DAMAGED			=	"CLM_WATERCRAFT_PROPERTY_DAMAGED";
		private const	string		GetCLM_WATERCRAFT_PROPERTY_DAMAGED			=	"Proc_GetCLM_WATERCRAFT_PROPERTY_DAMAGED";
		private const	string		InsertCLM_WATERCRAFT_PROPERTY_DAMAGED		=	"Proc_InsertCLM_WATERCRAFT_PROPERTY_DAMAGED";
		private const	string		UpdateCLM_WATERCRAFT_PROPERTY_DAMAGED		=	"Proc_UpdateCLM_WATERCRAFT_PROPERTY_DAMAGED";
		
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
		public ClsWatercraftPropertyDamaged()
		{
			boolTransactionLog	= base.TransactionLogRequired;			
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in ADDRESS1 object to database.
		/// </summary>
		/// <param name="objWatercraftPropertyDamagedInfo">ADDRESS1 class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsWatercraftPropertyDamagedInfo objWatercraftPropertyDamagedInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objWatercraftPropertyDamagedInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@DESCRIPTION",objWatercraftPropertyDamagedInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@OTHER_VEHICLE",objWatercraftPropertyDamagedInfo.OTHER_VEHICLE);
				objDataWrapper.AddParameter("@OTHER_INSURANCE_NAME",objWatercraftPropertyDamagedInfo.OTHER_INSURANCE_NAME);
				objDataWrapper.AddParameter("@OTHER_OWNER_NAME",objWatercraftPropertyDamagedInfo.OTHER_OWNER_NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objWatercraftPropertyDamagedInfo.ADDRESS1);		
				objDataWrapper.AddParameter("@ADDRESS2",objWatercraftPropertyDamagedInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objWatercraftPropertyDamagedInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objWatercraftPropertyDamagedInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objWatercraftPropertyDamagedInfo.ZIP);			
				objDataWrapper.AddParameter("@HOME_PHONE",objWatercraftPropertyDamagedInfo.HOME_PHONE);
				objDataWrapper.AddParameter("@WORK_PHONE",objWatercraftPropertyDamagedInfo.WORK_PHONE);
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftPropertyDamagedInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftPropertyDamagedInfo.CREATED_DATETIME);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PROPERTY_DAMAGED_ID",objWatercraftPropertyDamagedInfo.PROPERTY_DAMAGED_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWatercraftPropertyDamagedInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddWatercraftPropertyDamaged.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftPropertyDamagedInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objWatercraftPropertyDamagedInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Watercraft Property Damaged Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_WATERCRAFT_PROPERTY_DAMAGED,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_WATERCRAFT_PROPERTY_DAMAGED);
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
					objWatercraftPropertyDamagedInfo.PROPERTY_DAMAGED_ID = PROPERTY_DAMAGED_ID;
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
		/// Update method that recieves ADDRESS1 object to save.
		/// </summary>
		/// <param name="objOldInsuredVehicleInfo">ADDRESS1 object haADDRESS2g old information</param>
		/// <param name="objWatercraftPropertyDamagedInfo">ADDRESS1 object haADDRESS2g new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsWatercraftPropertyDamagedInfo objOldWatercraftPropertyDamagedInfo,ClsWatercraftPropertyDamagedInfo objWatercraftPropertyDamagedInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@PROPERTY_DAMAGED_ID",objWatercraftPropertyDamagedInfo.PROPERTY_DAMAGED_ID);				
				objDataWrapper.AddParameter("@CLAIM_ID",objWatercraftPropertyDamagedInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@DESCRIPTION",objWatercraftPropertyDamagedInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@OTHER_VEHICLE",objWatercraftPropertyDamagedInfo.OTHER_VEHICLE);
				objDataWrapper.AddParameter("@OTHER_INSURANCE_NAME",objWatercraftPropertyDamagedInfo.OTHER_INSURANCE_NAME);
				objDataWrapper.AddParameter("@OTHER_OWNER_NAME",objWatercraftPropertyDamagedInfo.OTHER_OWNER_NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objWatercraftPropertyDamagedInfo.ADDRESS1);		
				objDataWrapper.AddParameter("@ADDRESS2",objWatercraftPropertyDamagedInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objWatercraftPropertyDamagedInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objWatercraftPropertyDamagedInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objWatercraftPropertyDamagedInfo.ZIP);			
				objDataWrapper.AddParameter("@HOME_PHONE",objWatercraftPropertyDamagedInfo.HOME_PHONE);
				objDataWrapper.AddParameter("@WORK_PHONE",objWatercraftPropertyDamagedInfo.WORK_PHONE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftPropertyDamagedInfo.MODIFIED_BY);				
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftPropertyDamagedInfo.LAST_UPDATED_DATETIME);				
				

				if(base.TransactionLogRequired) 
				{
					objWatercraftPropertyDamagedInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddWatercraftPropertyDamaged.aspx.resx");
					objBuilder.GetUpdateSQL(objOldWatercraftPropertyDamagedInfo,objWatercraftPropertyDamagedInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objWatercraftPropertyDamagedInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Watercraft Property Damaged Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_WATERCRAFT_PROPERTY_DAMAGED,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_WATERCRAFT_PROPERTY_DAMAGED);
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
		public static DataTable GetOldDataForPageControls(string CLAIM_ID, string PROPERTY_DAMAGED_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@PROPERTY_DAMAGED_ID",PROPERTY_DAMAGED_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_WATERCRAFT_PROPERTY_DAMAGED);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		#endregion
		
	}
}
