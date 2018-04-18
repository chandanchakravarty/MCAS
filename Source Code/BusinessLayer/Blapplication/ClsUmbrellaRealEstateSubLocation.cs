/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	5/30/2005 3:06:45 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Application;
using System.Data.SqlClient;
using Cms.DataLayer;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsUmbrellaRealEstateSubLoc : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_UMBRELLA_REAL_ESTATE_SUB_LOC =	"APP_UMBRELLA_REAL_ESTATE_SUB_LOC";

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


		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjRealEstateLocationInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(Cms.Model.Application.ClsUmbrellaRealEstateSubLocInfo ObjRealEstateLocationInfo)
		{
			string		strStoredProc	=	"Proc_InsertRealEstateSubLoc";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjRealEstateLocationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjRealEstateLocationInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjRealEstateLocationInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",ObjRealEstateLocationInfo.LOCATION_ID);				
				objDataWrapper.AddParameter("@SUB_LOC_NUMBER",ObjRealEstateLocationInfo.SUB_LOC_NUMBER);
				objDataWrapper.AddParameter("@SUB_LOC_TYPE",ObjRealEstateLocationInfo.SUB_LOC_TYPE);
				objDataWrapper.AddParameter("@SUB_LOC_DESC",ObjRealEstateLocationInfo.SUB_LOC_DESC);
				objDataWrapper.AddParameter("@SUB_LOC_CITY_LIMITS",ObjRealEstateLocationInfo.SUB_LOC_CITY_LIMITS);
				objDataWrapper.AddParameter("@SUB_LOC_INTEREST",ObjRealEstateLocationInfo.SUB_LOC_INTEREST);
				objDataWrapper.AddParameter("@SUB_LOC_OCCUPIED_PERCENT",DefaultValues.GetDoubleNullFromNegative(ObjRealEstateLocationInfo.SUB_LOC_OCCUPIED_PERCENT));
				objDataWrapper.AddParameter("@SUB_LOC_OCC_DESC",ObjRealEstateLocationInfo.SUB_LOC_OCC_DESC);
				//objDataWrapper.AddParameter("@IS_ACTIVE",ObjRealEstateLocationInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",ObjRealEstateLocationInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",ObjRealEstateLocationInfo.CREATED_DATETIME);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@SUB_LOC_ID",ObjRealEstateLocationInfo.SUB_LOC_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjRealEstateLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/AddUmbrellaRealEstateSubLocation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjRealEstateLocationInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = ObjRealEstateLocationInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = ObjRealEstateLocationInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = ObjRealEstateLocationInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	ObjRealEstateLocationInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New real estate's sub location detail is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if (returnResult >0)
				{
					ObjRealEstateLocationInfo.SUB_LOC_ID = int.Parse(objSqlParameter.Value.ToString());
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
		/// <param name="objOldUmbrellaRealEstateLocationInfo">Model object having old information</param>
		/// <param name="ObjUmbrellaRealEstateLocationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(Cms.Model.Application.ClsUmbrellaRealEstateSubLocInfo objOldUmbrellaRealEstateLocationInfo,ClsUmbrellaRealEstateSubLocInfo ObjUmbrellaRealEstateLocationInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc= "Proc_UpdateRealEstateSubLocation";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjUmbrellaRealEstateLocationInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",ObjUmbrellaRealEstateLocationInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjUmbrellaRealEstateLocationInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",ObjUmbrellaRealEstateLocationInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@SUB_LOC_ID",ObjUmbrellaRealEstateLocationInfo.SUB_LOC_ID);
				objDataWrapper.AddParameter("@SUB_LOC_NUMBER",ObjUmbrellaRealEstateLocationInfo.SUB_LOC_NUMBER);
				objDataWrapper.AddParameter("@SUB_LOC_TYPE",ObjUmbrellaRealEstateLocationInfo.SUB_LOC_TYPE);
				objDataWrapper.AddParameter("@SUB_LOC_DESC",ObjUmbrellaRealEstateLocationInfo.SUB_LOC_DESC);
				objDataWrapper.AddParameter("@SUB_LOC_CITY_LIMITS",ObjUmbrellaRealEstateLocationInfo.SUB_LOC_CITY_LIMITS);
				objDataWrapper.AddParameter("@SUB_LOC_INTEREST",ObjUmbrellaRealEstateLocationInfo.SUB_LOC_INTEREST);
				objDataWrapper.AddParameter("@SUB_LOC_OCCUPIED_PERCENT",DefaultValues.GetDoubleNullFromNegative(ObjUmbrellaRealEstateLocationInfo.SUB_LOC_OCCUPIED_PERCENT));
				objDataWrapper.AddParameter("@SUB_LOC_OCC_DESC",ObjUmbrellaRealEstateLocationInfo.SUB_LOC_OCC_DESC);				
				objDataWrapper.AddParameter("@MODIFIED_BY",ObjUmbrellaRealEstateLocationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjUmbrellaRealEstateLocationInfo.LAST_UPDATED_DATETIME);
				if(TransactionLogRequired) 
				{
					strTranXML = objBuilder.GetTransactionLogXML(objOldUmbrellaRealEstateLocationInfo,ObjUmbrellaRealEstateLocationInfo);

					ObjUmbrellaRealEstateLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/AddUmbrellaRealEstateSubLocation.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	ObjUmbrellaRealEstateLocationInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID		=	ObjUmbrellaRealEstateLocationInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID			=	ObjUmbrellaRealEstateLocationInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	ObjUmbrellaRealEstateLocationInfo.APP_VERSION_ID;
					objTransactionInfo.TRANS_DESC		=	"Real estate's sub location detail is modified";
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

		
		#region GetUmbrellaRealEstateSubLocInfo
		public static string GetUmbrellaRealEstateSubLocInfo(int intCustomerId,int intAppid, int intAppVersionId, int intLocationId, int intSubLocationId)
		{
			string strStoredProc = "Proc_GetUmbrellaRealEstateSubLoc";
			DataSet dsUmbrellaRealEstateSubLocInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_ID",intAppid);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@LOCATION_ID",intLocationId);
				objDataWrapper.AddParameter("@SUB_LOC_ID",intSubLocationId);

				dsUmbrellaRealEstateSubLocInfo = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				if (dsUmbrellaRealEstateSubLocInfo.Tables[0].Rows.Count != 0)
				{
					return dsUmbrellaRealEstateSubLocInfo.GetXml();
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
			#endregion

		}
		
	}
}
