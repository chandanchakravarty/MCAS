/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	8/22/2005 3:50:48 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Nov 09,2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Additional Info in the Transaction log to record LOB name and screen name has been added
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
	/// ddd
	/// </summary>
	public class ClsDeductibles : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_GENERAL_DEDUCTIBLES_COMMISSION			=	"APP_GENERAL_DEDUCTIBLES_COMMISSION";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _;
		//private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateDeductibles";
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
		public ClsDeductibles()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			//base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

       #region Add(Insert) functions
        ///// <summary>
        ///// Saves the information passed in model object to database.
        ///// </summary>
        ///// <param name="ObjDeductiblesCommissionInfo">Model class object.</param>
        ///// <returns>No of records effected.</returns>
        //public int Add(ClsDeductiblesCommissionInfo ObjDeductiblesCommissionInfo)
        //{
        //    string		strStoredProc	=	"Proc_InsertDeductiblesCommission";
        //    DateTime	RecordDate		=	DateTime.Now;
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

        //    try
        //    {
        //        objDataWrapper.AddParameter("@CUSTOMER_ID",ObjDeductiblesCommissionInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@APP_ID",ObjDeductiblesCommissionInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",ObjDeductiblesCommissionInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@BODILY_INJURY_DEDUCTIBLE_AMOUNT",ObjDeductiblesCommissionInfo.BODILY_INJURY_DEDUCTIBLE_AMOUNT);
        //        objDataWrapper.AddParameter("@PREMISES_PREMIUM",ObjDeductiblesCommissionInfo.PREMISES_PREMIUM);
        //        objDataWrapper.AddParameter("@TOTAL_ACCOUNT_PREMIUM",ObjDeductiblesCommissionInfo.TOTAL_ACCOUNT_PREMIUM);
        //        objDataWrapper.AddParameter("@COMMISSION_PERCENT",ObjDeductiblesCommissionInfo.COMMISSION_PERCENT);
        //        objDataWrapper.AddParameter("@COMMISSION_AMOUNT",ObjDeductiblesCommissionInfo.COMMISSION_AMOUNT);
        //        objDataWrapper.AddParameter("@IS_ACTIVE",ObjDeductiblesCommissionInfo.IS_ACTIVE);
        //        objDataWrapper.AddParameter("@CREATED_BY",ObjDeductiblesCommissionInfo.CREATED_BY);
        //        objDataWrapper.AddParameter("@CREATED_DATETIME",ObjDeductiblesCommissionInfo.CREATED_DATETIME);
				
        //        SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DEDUCTIBLES_ID",ObjDeductiblesCommissionInfo.DEDUCTIBLES_ID,SqlDbType.Int,ParameterDirection.Output);

        //        int returnResult = 0;
        //        if(TransactionLogRequired)
        //        {
        //            ObjDeductiblesCommissionInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/Deductibles.aspx.resx");
        //            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //            string strTranXML = objBuilder.GetTransactionLogXML(ObjDeductiblesCommissionInfo);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            objTransactionInfo.TRANS_TYPE_ID	=	1;
        //            objTransactionInfo.RECORDED_BY		=	ObjDeductiblesCommissionInfo.CREATED_BY;
        //            objTransactionInfo.CLIENT_ID		=   ObjDeductiblesCommissionInfo.CUSTOMER_ID;
        //            objTransactionInfo.TRANS_DESC		=	"Deductibles Has Been Added";
        //            objTransactionInfo.APP_ID			=	ObjDeductiblesCommissionInfo.APP_ID;
        //            objTransactionInfo.APP_VERSION_ID	=	ObjDeductiblesCommissionInfo.APP_VERSION_ID;
        //            //Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
        //            //							worked upon as well as Screen Name
        //            //objTransactionInfo.CUSTOM_INFO		=	";LOB = " + "General Liability" + ";Screen Name = " +  "Deductibles";
        //            objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Deductibles";
        //            objTransactionInfo.CHANGE_XML		=	strTranXML;
        //            //Executing the query
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //        }
        //        else
        //        {
        //            returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
        //        }
        //        int DEDUCTIBLES_ID    = int.Parse(objSqlParameter.Value.ToString());
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //        if (DEDUCTIBLES_ID    == -1)
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            ObjDeductiblesCommissionInfo.DEDUCTIBLES_ID = DEDUCTIBLES_ID   ;
        //                return returnResult;
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
        #endregion

        #region Update method
        ///// <summary>
        ///// Update method that recieves Model object to save.
        ///// </summary>
        ///// <param name="objOldDeductiblesCommissionInfo">Model object having old information</param>
        ///// <param name="ObjDeductiblesCommissionInfo">Model object having new information(form control's value)</param>
        ///// <returns>No. of rows updated (1 or 0)</returns>
        //public int Update(ClsDeductiblesCommissionInfo objOldDeductiblesCommissionInfo,ClsDeductiblesCommissionInfo ObjDeductiblesCommissionInfo)
        //{
        //    string		strStoredProc	=	"Proc_UpdateDEDUCTIBLESCOMMISSION";
        //    string strTranXML;
        //    int returnResult = 0;
        //    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
        //    try 
        //    {
        //        objDataWrapper.AddParameter("@CUSTOMER_ID",ObjDeductiblesCommissionInfo.CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@APP_ID",ObjDeductiblesCommissionInfo.APP_ID);
        //        objDataWrapper.AddParameter("@APP_VERSION_ID",ObjDeductiblesCommissionInfo.APP_VERSION_ID);
        //        objDataWrapper.AddParameter("@DEDUCTIBLES_ID",ObjDeductiblesCommissionInfo.DEDUCTIBLES_ID);
        //        objDataWrapper.AddParameter("@BODILY_INJURY_DEDUCTIBLE_AMOUNT",ObjDeductiblesCommissionInfo.BODILY_INJURY_DEDUCTIBLE_AMOUNT);
        //        objDataWrapper.AddParameter("@PREMISES_PREMIUM",ObjDeductiblesCommissionInfo.PREMISES_PREMIUM);
        //        objDataWrapper.AddParameter("@TOTAL_ACCOUNT_PREMIUM",ObjDeductiblesCommissionInfo.TOTAL_ACCOUNT_PREMIUM);
        //        objDataWrapper.AddParameter("@COMMISSION_PERCENT",ObjDeductiblesCommissionInfo.COMMISSION_PERCENT);
        //        objDataWrapper.AddParameter("@COMMISSION_AMOUNT",ObjDeductiblesCommissionInfo.COMMISSION_AMOUNT);
        //        objDataWrapper.AddParameter("@IS_ACTIVE",ObjDeductiblesCommissionInfo.IS_ACTIVE);
				
        //        objDataWrapper.AddParameter("@MODIFIED_BY",ObjDeductiblesCommissionInfo.MODIFIED_BY);
        //        objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjDeductiblesCommissionInfo.LAST_UPDATED_DATETIME);
        //        if(base.TransactionLogRequired) 
        //        {
        //            ObjDeductiblesCommissionInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/Deductibles.aspx.resx");
        //            objBuilder.GetUpdateSQL(objOldDeductiblesCommissionInfo,ObjDeductiblesCommissionInfo,out strTranXML);
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
        //                returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
        //            else
        //            {
        //                objTransactionInfo.TRANS_TYPE_ID	=	3;
        //                objTransactionInfo.RECORDED_BY		=	ObjDeductiblesCommissionInfo.MODIFIED_BY;
        //                objTransactionInfo.CLIENT_ID		=   ObjDeductiblesCommissionInfo.CUSTOMER_ID;
        //                objTransactionInfo.TRANS_DESC		=	"Deductibles Has Been Updated";
        //                objTransactionInfo.APP_ID			=	ObjDeductiblesCommissionInfo.APP_ID;
        //                objTransactionInfo.APP_VERSION_ID	=	ObjDeductiblesCommissionInfo.APP_VERSION_ID;
        //                //Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
        //                //							worked upon as well as Screen Name
        //                //objTransactionInfo.CUSTOM_INFO		=	";LOB = " + "General Liability" + ";Screen Name = " +  "Deductibles";
        //                objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Deductibles";
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
        #endregion

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(string CUSTOMER_ID)
		{
			string strSql = "Proc_GetXMLAPP_GENERAL_DEDUCTIBLES_COMMISSION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion

		#region GetDeductiblesCommissionXml
		public static string GetDeductiblesCommissionXml(int intCustoemrId, int intAppId, int intAppVersionId)
		{
			string strStoredProc = "Proc_GetDeductiblesCommission";
			DataSet dsDeductiblesCommission= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				

				dsDeductiblesCommission= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsDeductiblesCommission.Tables[0].Rows.Count != 0)
				{
					return dsDeductiblesCommission.GetXml();
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
