/******************************************************************************************
<Author				: -   Gaurav
<Start Date				: -	8/23/2005 12:28:41 PM
<End Date				: -	
<Description				: - 	This file is used to business class for Holder interest
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
<Modified Date			: - Nov 09,2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Additional Info in the Transaction log to record LOB name and screen name has been added
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsGeneralHolderInterest : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_GENERAL_HOLDER_INTEREST			=	"APP_GENERAL_HOLDER_INTEREST";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _CUSTOMER_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_GENERAL_HOLDER_INTEREST";
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
		public ClsGeneralHolderInterest()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

//        #region Add(Insert) functions
//        /// <summary>
//        /// Saves the information passed in model object to database.
//        /// </summary>
//        /// <param name="objGeneralHolderInterestInfo">Model class object.</param>
//        /// <returns>No of records effected.</returns>
//        public int Add(ClsGeneralHolderInterestInfo objGeneralHolderInterestInfo)
//        {
//            string		strStoredProc	=	"Proc_InsertGeneralAdditionalInterest";
//            DateTime	RecordDate		=	DateTime.Now;
//            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

//            try
//            {
//                objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralHolderInterestInfo.CUSTOMER_ID);
//                objDataWrapper.AddParameter("@APP_ID",objGeneralHolderInterestInfo.APP_ID);
//                objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralHolderInterestInfo.APP_VERSION_ID);				
//                objDataWrapper.AddParameter("@DWELLING_ID",objGeneralHolderInterestInfo.DWELLING_ID);
//                objDataWrapper.AddParameter("@MEMO",objGeneralHolderInterestInfo.MEMO);
//                objDataWrapper.AddParameter("@NATURE_OF_INTEREST",objGeneralHolderInterestInfo.NATURE_OF_INTEREST);
//                objDataWrapper.AddParameter("@RANK",objGeneralHolderInterestInfo.RANK);
//                objDataWrapper.AddParameter("@LOAN_REF_NUMBER",objGeneralHolderInterestInfo.LOAN_REF_NUMBER);
//                objDataWrapper.AddParameter("@CREATED_BY",objGeneralHolderInterestInfo.CREATED_BY);
//                objDataWrapper.AddParameter("@CREATED_DATETIME",objGeneralHolderInterestInfo.CREATED_DATETIME);
//                objDataWrapper.AddParameter("@HOLDER_NAME",objGeneralHolderInterestInfo.HOLDER_NAME);
//                objDataWrapper.AddParameter("@HOLDER_ADD1",objGeneralHolderInterestInfo.HOLDER_ADD1);
//                objDataWrapper.AddParameter("@HOLDER_ADD2",objGeneralHolderInterestInfo.HOLDER_ADD2);
//                objDataWrapper.AddParameter("@HOLDER_CITY",objGeneralHolderInterestInfo.HOLDER_CITY);
//                objDataWrapper.AddParameter("@HOLDER_COUNTRY",objGeneralHolderInterestInfo.HOLDER_COUNTRY);
//                objDataWrapper.AddParameter("@HOLDER_STATE",objGeneralHolderInterestInfo.HOLDER_STATE);
//                objDataWrapper.AddParameter("@HOLDER_ZIP",objGeneralHolderInterestInfo.HOLDER_ZIP);				
//                objDataWrapper.AddParameter("@IS_ACTIVE","Y");				
				
////				objDataWrapper.AddParameter("@MAIN_PHONE",objGeneralHolderInterestInfo.MAIN_PHONE);
////				objDataWrapper.AddParameter("@MAIN_FAX_NUMBER",objGeneralHolderInterestInfo.MAIN_FAX_NUMBER);
////				objDataWrapper.AddParameter("@CERTIFICATE_REQUIRED",objGeneralHolderInterestInfo.CERTIFICATE_REQUIRED);
//                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ADD_INT_ID",objGeneralHolderInterestInfo.ADD_INT_ID,SqlDbType.Int,ParameterDirection.Output);

//                int returnResult = 0;
//                if(TransactionLogRequired)
//                {
//                    objGeneralHolderInterestInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/AdditionalInterest.aspx.resx");
//                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//                    string strTranXML = objBuilder.GetTransactionLogXML(objGeneralHolderInterestInfo);
//                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//                    objTransactionInfo.TRANS_TYPE_ID	=	1;
//                    objTransactionInfo.RECORDED_BY		=	objGeneralHolderInterestInfo.CREATED_BY;
//                    objTransactionInfo.CLIENT_ID		=   objGeneralHolderInterestInfo.CUSTOMER_ID;
//                    objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
//                    //Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
//                    //							worked upon as well as Screen Name					
//                    objTransactionInfo.CUSTOM_INFO = ";LOB : " + "General Liability" + ";Screen Name: " +  "Additional Interest";
//                    objTransactionInfo.CHANGE_XML		=	strTranXML;
//                    //Executing the query
//                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
//                }
//                else
//                {
//                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
//                }
//                int ADD_INT_ID = int.Parse(objSqlParameter.Value.ToString());
//                objDataWrapper.ClearParameteres();
//                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//                if (ADD_INT_ID == -1)
//                {
//                    return -1;
//                }
//                else
//                {
//                    objGeneralHolderInterestInfo.ADD_INT_ID = ADD_INT_ID;
//                    return returnResult;
//                }
//            }
//            catch(Exception ex)
//            {
//                throw(ex);
//            }
//            finally
//            {
//                if(objDataWrapper != null) objDataWrapper.Dispose();
//            }
//        }
//        #endregion

//        #region Update method
//        /// <summary>
//        /// Update method that recieves Model object to save.
//        /// </summary>
//        /// <param name="objOldGeneralHolderInterestInfo">Model object having old information</param>
//        /// <param name="objGeneralHolderInterestInfo">Model object having new information(form control's value)</param>
//        /// <returns>No. of rows updated (1 or 0)</returns>
//        public int Update(ClsGeneralHolderInterestInfo objOldGeneralHolderInterestInfo,ClsGeneralHolderInterestInfo objGeneralHolderInterestInfo)
//        {
//            string		strStoredProc	=	"Proc_UpdateGeneralAdditionalInterest";
//            string strTranXML;
//            int returnResult = 0;
//            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//            try 
//            {
//                objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralHolderInterestInfo.CUSTOMER_ID);
//                objDataWrapper.AddParameter("@APP_ID",objGeneralHolderInterestInfo.APP_ID);
//                objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralHolderInterestInfo.APP_VERSION_ID);
//                objDataWrapper.AddParameter("@ADD_INT_ID",objGeneralHolderInterestInfo.ADD_INT_ID);				
//                objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralHolderInterestInfo.MODIFIED_BY);
//                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralHolderInterestInfo.LAST_UPDATED_DATETIME);
//                objDataWrapper.AddParameter("@HOLDER_NAME",objGeneralHolderInterestInfo.HOLDER_NAME);
//                objDataWrapper.AddParameter("@HOLDER_ADD1",objGeneralHolderInterestInfo.HOLDER_ADD1);
//                objDataWrapper.AddParameter("@HOLDER_ADD2",objGeneralHolderInterestInfo.HOLDER_ADD2);
//                objDataWrapper.AddParameter("@HOLDER_CITY",objGeneralHolderInterestInfo.HOLDER_CITY);
//                objDataWrapper.AddParameter("@HOLDER_COUNTRY",objGeneralHolderInterestInfo.HOLDER_COUNTRY);
//                objDataWrapper.AddParameter("@DWELLING_ID",objGeneralHolderInterestInfo.DWELLING_ID);
//                objDataWrapper.AddParameter("@MEMO",objGeneralHolderInterestInfo.MEMO);
//                objDataWrapper.AddParameter("@HOLDER_STATE",objGeneralHolderInterestInfo.HOLDER_STATE);
//                objDataWrapper.AddParameter("@HOLDER_ZIP",objGeneralHolderInterestInfo.HOLDER_ZIP);
//                objDataWrapper.AddParameter("@NATURE_OF_INTEREST ",objGeneralHolderInterestInfo.NATURE_OF_INTEREST );
//                objDataWrapper.AddParameter("@RANK ",objGeneralHolderInterestInfo.RANK );
//                objDataWrapper.AddParameter("@LOAN_REF_NUMBER ",objGeneralHolderInterestInfo.LOAN_REF_NUMBER );

////				objDataWrapper.AddParameter("@MAIN_PHONE",objGeneralHolderInterestInfo.MAIN_PHONE);
////				objDataWrapper.AddParameter("@MAIN_FAX_NUMBER",objGeneralHolderInterestInfo.MAIN_FAX_NUMBER);
////				objDataWrapper.AddParameter("@CERTIFICATE_REQUIRED",objGeneralHolderInterestInfo.CERTIFICATE_REQUIRED);
//                if(base.TransactionLogRequired) 
//                {
//                    objGeneralHolderInterestInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/aspx/GeneralLiability/AdditionalInterest.aspx.resx");
//                    objBuilder.GetUpdateSQL(objOldGeneralHolderInterestInfo,objGeneralHolderInterestInfo,out strTranXML);

//                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//                    objTransactionInfo.TRANS_TYPE_ID	=	3;
//                    objTransactionInfo.RECORDED_BY		=	objGeneralHolderInterestInfo.MODIFIED_BY;
//                    objTransactionInfo.CLIENT_ID		=   objGeneralHolderInterestInfo.CUSTOMER_ID;
//                    objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
//                    //Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
//                    //							worked upon as well as Screen Name					
//                    objTransactionInfo.CUSTOM_INFO = ";LOB : " + "General Liability" + ";Screen Name: " +  "Additional Interest";
//                    objTransactionInfo.CHANGE_XML		=	strTranXML;
//                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

//                }
//                else
//                {
//                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
//                }
//                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//                return returnResult;
//            }
//            catch(Exception ex)
//            {
//                throw(ex);
//            }
//            finally
//            {
//                if(objDataWrapper != null) 
//                {
//                    objDataWrapper.Dispose();
//                }
//                if(objBuilder != null) 
//                {
//                    objBuilder = null;
//                }
//            }
//        }
//        #endregion

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(int CUSTOMER_ID,int APP_ID, int APP_VERSION_ID, int ADD_INT_ID)
		{
			string strSql = "Proc_GetXMLGeneralAdditionalInterest";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);
			objDataWrapper.AddParameter("@ADD_INT_ID",ADD_INT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		public static string GetXmlForPageControlsForPolicy(int CUSTOMER_ID,int POLICY_ID, int POLICY_VERSION_ID, int ADD_INT_ID)
		{
			string strSql = "Proc_GetXMLGeneralAdditionalInterestForPolicy";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);
			objDataWrapper.AddParameter("@ADD_INT_ID",ADD_INT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion		

		#region GetDataTable
		public static DataSet FillGeneralLiabilityAdditionalInterestDetails(int CUSTOMER_ID,int APP_ID, int APP_VERSION_ID, int ADD_INT_ID)
		{
			string strSql = "Proc_GetGeneralLiabilityAdditionalInterestDetails";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);
			objDataWrapper.AddParameter("@ADD_INT_ID",ADD_INT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion		

		#region Delete functions
		public int Delete(int ADD_INT_ID)
		{
			string		strStoredProc	=	"Proc_DeleteGeneralAdditionalInterest";			
			int returnResult = 0;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{				
				objDataWrapper.AddParameter("@ADD_INT_ID",ADD_INT_ID);				
				
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);				
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
			}
		}

		#endregion

		#region Activate/ Deactivate function
		#endregion

	}
}
