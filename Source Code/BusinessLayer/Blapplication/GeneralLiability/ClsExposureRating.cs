/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	8/18/2005 4:43:06 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 16/12/2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Check has been added at update of data to prevent an entry from going into transaction log when no modication has taken place.
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// d
	/// </summary>
	public class ClsExposureRating : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_GENERAL_EXPOSURE_RATING			=	"APP_GENERAL_EXPOSURE_RATING";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _EXPOSURE_RATING_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "";
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
		public ClsExposureRating()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		//	base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

        //#region Add(Insert) functions
        ///// <summary>
        ///// Saves the information passed in model object to database.
        ///// </summary>
        ///// <param name="ObjExposureRatingInfo">Model class object.</param>
        ///// <returns>No of records effected.</returns>
        //public int Add(ClsExposureRatingInfo ObjExposureRatingInfo)
        //{
        //    string		strStoredProc	=	"Proc_InsertexposureRating";
        //    DateTime	RecordDate		=	DateTime.Now;
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

        //    try
        //    {
        //        objDataWrapper.AddParameter("@CUSTOMER_ID",ObjExposureRatingInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@APP_ID",ObjExposureRatingInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",ObjExposureRatingInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@EXPOSURE",ObjExposureRatingInfo.EXPOSURE);
        //        objDataWrapper.AddParameter("@ADDITIONAL_EXPOSURE",ObjExposureRatingInfo.ADDITIONAL_EXPOSURE);
        //        objDataWrapper.AddParameter("@RATING_BASE",ObjExposureRatingInfo.RATING_BASE);
        //        objDataWrapper.AddParameter("@RATE",ObjExposureRatingInfo.RATE);
        //        objDataWrapper.AddParameter("@IS_ACTIVE",ObjExposureRatingInfo.IS_ACTIVE);
        //        objDataWrapper.AddParameter("@CREATED_BY",ObjExposureRatingInfo.CREATED_BY);
        //        objDataWrapper.AddParameter("@CREATE_DATETIME",ObjExposureRatingInfo.CREATED_DATETIME);
        //        //objDataWrapper.AddParameter("@MODIFIED_BY",ObjExposureRatingInfo.MODIFIED_BY);
        //        //objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjExposureRatingInfo.LAST_UPDATED_DATETIME);
        //        SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@EXPOSURE_RATING_ID",ObjExposureRatingInfo.EXPOSURE_RATING_ID,SqlDbType.Int,ParameterDirection.Output);

        //        int returnResult = 0;
        //        if(TransactionLogRequired)
        //        {
        //            ObjExposureRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/GeneralLiability/ExposureRating.aspx.resx");
        //            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //            string strTranXML = objBuilder.GetTransactionLogXML(ObjExposureRatingInfo);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            objTransactionInfo.TRANS_TYPE_ID	=	1;
        //            objTransactionInfo.RECORDED_BY		=	ObjExposureRatingInfo.CREATED_BY;
        //            objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
        //            objTransactionInfo.CHANGE_XML		=	strTranXML;
        //            //Executing the query
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //        }
        //        else
        //        {
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
        //        }
        //        int EXPOSURE_RATING_ID = int.Parse(objSqlParameter.Value.ToString());
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //        if (EXPOSURE_RATING_ID == -1)
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            ObjExposureRatingInfo.EXPOSURE_RATING_ID = EXPOSURE_RATING_ID;
        //            return returnResult;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw(ex);
        //    }
        //    finally
        //    {
        //        if(objDataWrapper != null) objDataWrapper.Dispose();
        //    }
        //}
        //#endregion

        //#region Update method
        ///// <summary>
        ///// Update method that recieves Model object to save.
        ///// </summary>
        ///// <param name="objOldExposureRatingInfo">Model object having old information</param>
        ///// <param name="ObjExposureRatingInfo">Model object having new information(form control's value)</param>
        ///// <returns>No. of rows updated (1 or 0)</returns>
        //public int Update(ClsExposureRatingInfo objOldExposureRatingInfo,ClsExposureRatingInfo ObjExposureRatingInfo)
        //{
        //    string		strStoredProc	=	"Proc_UpdateAPP_GENERAL_EXPOSURE_RATING";
        //    string strTranXML;
        //    int returnResult = 0;
        //    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
        //    try 
        //    {
        //        objDataWrapper.AddParameter("@CUSTOMER_ID",ObjExposureRatingInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@APP_ID",ObjExposureRatingInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",ObjExposureRatingInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@EXPOSURE_RATING_ID",ObjExposureRatingInfo.EXPOSURE_RATING_ID);
        //        objDataWrapper.AddParameter("@EXPOSURE",ObjExposureRatingInfo.EXPOSURE);
        //        objDataWrapper.AddParameter("@ADDITIONAL_EXPOSURE",ObjExposureRatingInfo.ADDITIONAL_EXPOSURE);
        //        objDataWrapper.AddParameter("@RATING_BASE",ObjExposureRatingInfo.RATING_BASE);
        //        objDataWrapper.AddParameter("@RATE",ObjExposureRatingInfo.RATE);
        //        objDataWrapper.AddParameter("@IS_ACTIVE",ObjExposureRatingInfo.IS_ACTIVE);
        //        objDataWrapper.AddParameter("@MODIFIED_BY",ObjExposureRatingInfo.MODIFIED_BY);
        //        objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjExposureRatingInfo.LAST_UPDATED_DATETIME);
        //        if(base.TransactionLogRequired) 
        //        {
        //            ObjExposureRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/GeneralLiability/ExposureRating.aspx.resx");
        //            objBuilder.GetUpdateSQL(objOldExposureRatingInfo,ObjExposureRatingInfo,out strTranXML);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
        //                returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
        //            else
        //            {
        //                objTransactionInfo.TRANS_TYPE_ID	=	3;
        //                objTransactionInfo.RECORDED_BY		=	ObjExposureRatingInfo.MODIFIED_BY;
        //                objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
        //                objTransactionInfo.CHANGE_XML		=	strTranXML;
        //                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //            }

        //        }
        //        else
        //        {
        //            returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
        //        }
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //        return returnResult;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw(ex);
        //    }
        //    finally
        //    {
        //        if(objDataWrapper != null) 
        //        {
        //            objDataWrapper.Dispose();
        //        }
        //        if(objBuilder != null) 
        //        {
        //            objBuilder = null;
        //        }
        //    }
        //}
        //#endregion

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(string CUSTOMER_ID)
		{
			string strSql = "Proc_GetXMLAPP_GENERAL_EXPOSURE_RATING";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion
		#region GetExposureRatingXml
		public static string GetExposureRatingXml(int intCustoemrId, int intAppId, int intAppVersionId)
		{
			string strStoredProc = "Proc_GetExposureratingInformation";
			DataSet dsExposureRating= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				//objDataWrapper.AddParameter("@EXPOSURE_RATING_ID",intEXPOSURE_RATING_ID);

				dsExposureRating= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsExposureRating.Tables[0].Rows.Count != 0)
				{
					return dsExposureRating.GetXml();
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



	}
}
