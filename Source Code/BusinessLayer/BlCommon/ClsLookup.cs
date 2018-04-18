/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	8/3/2005 2:15:40 PM
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
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance.Security;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsLookup.
	/// </summary>
	public class ClsLookup :Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private const	string		MNT_LOOKUP_VALUES			=	"MNT_LOOKUP_VALUES";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _LOOKUP_UNIQUE_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateMNT_LOOKUP_VALUES";
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
		public ClsLookup()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjLookUpInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsLookupInfo ObjLookUpInfo,int CreatedBy)
		{
			string		strStoredProc	=	"Proc_InsertLookUpValue";
			DateTime	RecordDate		=	DateTime.Now;
			TransactionLogRequired=true;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@LOOKUP_ID",ObjLookUpInfo.LOOKUP_ID);
				//objDataWrapper.AddParameter("@LOOKUP_VALUE_ID",ObjLookUpInfo.LOOKUP_VALUE_ID);
				objDataWrapper.AddParameter("@LOOKUP_VALUE_CODE",ObjLookUpInfo.LOOKUP_VALUE_CODE);
				objDataWrapper.AddParameter("@LOOKUP_VALUE_DESC",ObjLookUpInfo.LOOKUP_VALUE_DESC);
				//objDataWrapper.AddParameter("@LOOKUP_SYS_DEF",ObjLookUpInfo.LOOKUP_SYS_DEF);
				objDataWrapper.AddParameter("@IS_ACTIVE",ObjLookUpInfo.IS_ACTIVE);
				//objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjLookUpInfo.LAST_UPDATED_DATETIME);
				//objDataWrapper.AddParameter("@LOOKUP_FRAME_OR_MASONRY",ObjLookUpInfo.LOOKUP_FRAME_OR_MASONRY);
				//objDataWrapper.AddParameter("@Type",ObjLookUpInfo.Type);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LOOKUP_UNIQUE_ID",ObjLookUpInfo.LOOKUP_UNIQUE_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter1  = (SqlParameter) objDataWrapper.AddParameter("@LOOKUP_VALUE_ID",ObjLookUpInfo.LOOKUP_VALUE_ID,SqlDbType.Int,ParameterDirection.Output);
				int returnResult = 0;

				if(TransactionLogRequired)
				{
					ObjLookUpInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddLookup.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjLookUpInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	CreatedBy;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int LOOKUP_UNIQUE_ID = int.Parse(objSqlParameter.Value.ToString());
				int LOOKUP_VALUE_ID = int.Parse(objSqlParameter1.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (LOOKUP_UNIQUE_ID == -1)
				{
					return -1;
				}
				else
				{
					ObjLookUpInfo.LOOKUP_UNIQUE_ID = LOOKUP_UNIQUE_ID;
					ObjLookUpInfo.LOOKUP_VALUE_ID = LOOKUP_VALUE_ID;
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
		/// <param name="objOldLookUpInfo">Model object having old information</param>
		/// <param name="ObjLookUpInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsLookupInfo objOldLookUpInfo,ClsLookupInfo ObjLookUpInfo)
		{
			string strTranXML;
			int returnResult = 0;
			TransactionLogRequired=true;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string strStoredProc="Proc_UpdateLookUpValue";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				//objDataWrapper.AddParameter("@LOOKUP_ID",ObjLookUpInfo.LOOKUP_ID);
				//objDataWrapper.AddParameter("@LOOKUP_VALUE_ID",ObjLookUpInfo.LOOKUP_VALUE_ID);
				objDataWrapper.AddParameter("@LOOKUP_UNIQUE_ID",ObjLookUpInfo.LOOKUP_UNIQUE_ID);
				objDataWrapper.AddParameter("@LOOKUP_VALUE_CODE",ObjLookUpInfo.LOOKUP_VALUE_CODE);
				objDataWrapper.AddParameter("@LOOKUP_VALUE_DESC",ObjLookUpInfo.LOOKUP_VALUE_DESC);
				//objDataWrapper.AddParameter("@LOOKUP_SYS_DEF",ObjLookUpInfo.LOOKUP_SYS_DEF);
				//objDataWrapper.AddParameter("@IS_ACTIVE",ObjLookUpInfo.IS_ACTIVE);
				//objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjLookUpInfo.LAST_UPDATED_DATETIME);
				//objDataWrapper.AddParameter("@LOOKUP_FRAME_OR_MASONRY",ObjLookUpInfo.LOOKUP_FRAME_OR_MASONRY);
				//objDataWrapper.AddParameter("@Type",ObjLookUpInfo.Type);
				
				if(TransactionLogRequired) 
				{
					ObjLookUpInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddLookup.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldLookUpInfo,ObjLookUpInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	ObjLookUpInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
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

		
			public static string GetLookUpDetailXml(int LookUpUniqueId)
			{			
				string strProcedure = "Proc_GetLookUpDetailXml";
				DataSet objDataSet = new DataSet();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

				try
				{
					objDataWrapper.AddParameter("@LookUpUniqueId",LookUpUniqueId);
					objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);

					if(objDataSet.Tables[0].Rows.Count!=0)
					{
						return objDataSet.GetXml();
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
		
		public static DataTable GetLookupTableData()
		{			
			string strProcedure = "Proc_GetLookUpTableData";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
                objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
				return objDataSet.Tables[0];
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
		public static DataTable GetLookupTableData(int LookUpId)
		{			
			string strProcedure = "Proc_GetLookUpTableDataForLookUpID";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@LookUpId",LookUpId);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
				return objDataSet.Tables[0];
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
		public static DataTable GetLookupTableData(string LookUpName)
		{			
			string strProcedure = "Proc_GetLookUpTableDataForLookUpName";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
               
                objDataWrapper.AddParameter("@LookUpName",LookUpName);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
				return objDataSet.Tables[0];
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

        public static DataTable GetVehicleModelByMake(int MAKEID)
        {
            string strProcedure = "Proc_GetVehicleModelByMake";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@MAKER_LOOKUP_ID", MAKEID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static DataTable GetVehicleTypeByMake(int MAKEID)
        {
            string strProcedure = "Proc_GetVehicleTypeByMake";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@MAKER_LOOKUP_ID", MAKEID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static DataTable GetLookupVehicleCoverages(int LOBID)
        {
            string strProcedure = "Proc_GetVehicleCoveragesType";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@LOB_ID", LOBID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }


        public static DataTable GetLookupValueFromUniqueID(int UniqueID)
        {
            string strProcedure = "Proc_GetLookupvaluesFromUniqueID";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@LOOKUP_UNIQUE_ID", UniqueID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static DataTable GetLookupUniqueIDFromCode(string Code)
        {
            string strProcedure = "Proc_GetLookupUniqueIDFromCode";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@LOOKUP_VALUE_CODE", Code);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static DataTable GetCountryName(string country)
        {
            string strProcedure = "Proc_GetCountryList";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@COUNTRY_ID", country);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        //Proc_CoverageByCoverageID

        public static DataTable GetCoverageInfobyID(int COVID)
        {
            string strProcedure = "Proc_CoverageByCoverageID";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@COV_ID", COVID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static DataTable GetModelDesc(int ModelID)
        {
            string strProcedure = "Proc_GetModelDesc";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@MODELID", ModelID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static DataTable GetModelTypeDesc(int TypeID)
        {
            string strProcedure = "Proc_GetModelTypeDesc";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@TYPEID", TypeID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
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

