/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	9/5/2005 5:14:08 PM
<End Date				: -	
<Description				: - 	Business logic class for default hierarchy page.
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
using Cms.DataLayer;
using Cms.Model.Maintenance;
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Business logic class for default hierarchy page.
	/// </summary>
	public class ClsDefaultHierarchy : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_DEFAULT_HIERARCHY			=	"MNT_DEFAULT_HIERARCHY";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _REC_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateMNT_DEFAULT_HIERARCHY";
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
		public ClsDefaultHierarchy()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDefaultHierarchyInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsDefaultHierarchyInfo objDefaultHierarchyInfo)
		{
			string		strStoredProc	=	"Proc_InsertDefaultHierarchy";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@AGENCY_ID",objDefaultHierarchyInfo.AGENCY_ID);
				objDataWrapper.AddParameter("@DIV_ID",objDefaultHierarchyInfo.DIV_ID);
				objDataWrapper.AddParameter("@DEPT_ID",objDefaultHierarchyInfo.DEPT_ID);
				objDataWrapper.AddParameter("@PC_ID",objDefaultHierarchyInfo.PC_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",objDefaultHierarchyInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objDefaultHierarchyInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDefaultHierarchyInfo.CREATED_DATETIME);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@REC_ID",objDefaultHierarchyInfo.REC_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objDefaultHierarchyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddDefaultHierarchy.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDefaultHierarchyInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objDefaultHierarchyInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New default hierarchy is Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int REC_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (REC_ID == -1)
				{
					return -1;
				}
				else
				{
					objDefaultHierarchyInfo.REC_ID = REC_ID;
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
		/// <param name="objOldDefaultHierarchyInfo">Model object having old information</param>
		/// <param name="objDefaultHierarchyInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsDefaultHierarchyInfo objOldDefaultHierarchyInfo,ClsDefaultHierarchyInfo objDefaultHierarchyInfo)
		{
			string strTranXML;
			string		strStoredProc	=	"Proc_UpdateDefaultHierarchy";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@REC_ID",objDefaultHierarchyInfo.REC_ID);
				objDataWrapper.AddParameter("@AGENCY_ID",objDefaultHierarchyInfo.AGENCY_ID);
				objDataWrapper.AddParameter("@DIV_ID",objDefaultHierarchyInfo.DIV_ID);
				objDataWrapper.AddParameter("@DEPT_ID",objDefaultHierarchyInfo.DEPT_ID);
				objDataWrapper.AddParameter("@PC_ID",objDefaultHierarchyInfo.PC_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",objDefaultHierarchyInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objDefaultHierarchyInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDefaultHierarchyInfo.LAST_UPDATED_DATETIME);
				if(TransactionLogRequired) 
				{
					objDefaultHierarchyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddDefaultHierarchy.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldDefaultHierarchyInfo,objDefaultHierarchyInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDefaultHierarchyInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Default hierarchy is Updated";
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

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(int AGENCY_ID)
		{
			string strSql = "Proc_GetDefaultHierarchy";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@AGENCY_ID",AGENCY_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}

		public static DataSet GetDefaultHierarchy(int AGENCY_ID)
		{
			string strSql = "Proc_GetDefaultHierarchy";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@AGENCY_ID",AGENCY_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion

	
	}
}
