/******************************************************************************************
<Author				: -     Kuldeep Saxena
<Start Date			: -	    24/10/2011
<End Date			: -	
<Description		: -     Business layer class to add update and delete
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Maintenance.Accumulation;


namespace Cms.BusinessLayer.BlCommon.Accumulation
{
   public class ClsAccumulationReference : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        private const	string		MNT_ACCUMULATION_REFERENCE			=	"MNT_ACCUMULATION_REFERENCE";

		#region Private Instance Variables
		private	bool	boolTransactionLog;
		
        private const string ACTIVATE_DEACTIVATE_PROC = "ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_REFERENCE";
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
        public ClsAccumulationReference()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objBudgetCategoryInfo ">Model class object.</param>
		/// <returns>No of records effected.</returns>
        public int Add(ClsAccumulationReferenceInfo objAccumulationReferenceInfo, string XmlFilePath)
		{
            string strStoredProc = "INSERT_MNT_ACCUMULATION_REFERENCE";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
                        
			try
			{

                //"@ACC_ID"
                //"@ACC_REF_NO"
                //"@CRITERIA_ID"
                //"@CRITERIA_VALUE"
                //"@TREATY_CAPACITY_LIMIT"
                //"@USED_LIMIT"
                //"@EFFECTIVE_DATE"
                //"@EXPIRATION_DATE"
                objDataWrapper.AddParameter("@ACC_REF_NO", objAccumulationReferenceInfo.ACC_REF_NO);
                objDataWrapper.AddParameter("@LOB_ID", objAccumulationReferenceInfo.LOB_ID);
                objDataWrapper.AddParameter("@CRITERIA_ID", objAccumulationReferenceInfo.CRITERIA_ID);
                objDataWrapper.AddParameter("@CRITERIA_VALUE", objAccumulationReferenceInfo.CRITERIA_VALUE);
                objDataWrapper.AddParameter("@TREATY_CAPACITY_LIMIT", objAccumulationReferenceInfo.TREATY_CAPACITY_LIMIT);
                objDataWrapper.AddParameter("@USED_LIMIT", objAccumulationReferenceInfo.USED_LIMIT);
                objDataWrapper.AddParameter("@EFFECTIVE_DATE", objAccumulationReferenceInfo.EFFECTIVE_DATE);
                objDataWrapper.AddParameter("@EXPIRATION_DATE", objAccumulationReferenceInfo.EXPIRATION_DATE);
                objDataWrapper.AddParameter("@IS_ACTIVE", objAccumulationReferenceInfo.IS_ACTIVE);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@ACC_ID", objAccumulationReferenceInfo.ACC_ID, SqlDbType.Int, ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
                    //objFundTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
                    objAccumulationReferenceInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objAccumulationReferenceInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY = objAccumulationReferenceInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}


                int ACC_ID;
				if (objSqlParameter.Value != null && objSqlParameter.Value.ToString() != "")
				{
                    ACC_ID = int.Parse(objSqlParameter.Value.ToString());
                    objAccumulationReferenceInfo.ACC_ID = ACC_ID;
				}
				else
				{
                    ACC_ID = -1;
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (ACC_ID == -1)
				{
					return -1;
				}
				else
				{
                    objAccumulationReferenceInfo.ACC_ID = ACC_ID;
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
		/// <param name="objOldBudgetCategoryInfo ">Model object having old information</param>
		/// <param name="objBudgetCategoryInfo ">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsAccumulationReferenceInfo objOldAccumulationReferenceInfo, ClsAccumulationReferenceInfo objAccumulationReferenceInfo, string XmlFilePath)
		{
            string strStoredProc = "UPDATE_MNT_ACCUMULATION_REFERENCE";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
                objDataWrapper.AddParameter("@ACC_ID", objAccumulationReferenceInfo.ACC_ID);
                objDataWrapper.AddParameter("@ACC_REF_NO", objAccumulationReferenceInfo.ACC_REF_NO);
                objDataWrapper.AddParameter("@LOB_ID", objAccumulationReferenceInfo.LOB_ID);
                objDataWrapper.AddParameter("@CRITERIA_ID", objAccumulationReferenceInfo.CRITERIA_ID);
                objDataWrapper.AddParameter("@CRITERIA_VALUE", objAccumulationReferenceInfo.CRITERIA_VALUE);
                objDataWrapper.AddParameter("@TREATY_CAPACITY_LIMIT", objAccumulationReferenceInfo.TREATY_CAPACITY_LIMIT);
                objDataWrapper.AddParameter("@USED_LIMIT", objAccumulationReferenceInfo.USED_LIMIT);
                objDataWrapper.AddParameter("@EFFECTIVE_DATE", objAccumulationReferenceInfo.EFFECTIVE_DATE);
                objDataWrapper.AddParameter("@EXPIRATION_DATE", objAccumulationReferenceInfo.EXPIRATION_DATE);
               
               
               
				if(base.TransactionLogRequired) 
				{
					//objBudgetCategoryInfo .TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
                    objAccumulationReferenceInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);

                    objBuilder.GetUpdateSQL(objOldAccumulationReferenceInfo, objAccumulationReferenceInfo, out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
                    objTransactionInfo.RECORDED_BY = objAccumulationReferenceInfo.MODIFIED_BY;
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

        #region ActivateDeactivate() function
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objDVInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int ActivateDeactivateDefaultValues(ClsAccumulationReferenceInfo objDefaultValuesInfo, string strIS_ACTIVE)
        {
            string strStoredProc = "ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_REFERENCE";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            try
            {
                objDataWrapper.AddParameter("@ACC_ID", objDefaultValuesInfo.ACC_ID);
                objDataWrapper.AddParameter("@IS_ACTIVE", strIS_ACTIVE);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objDefaultValuesInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    if (strIS_ACTIVE.ToUpper() == "Y")
                        objTransactionInfo.TRANS_DESC = "Accumulation Reference Default Value is Activated.";
                    else
                        objTransactionInfo.TRANS_DESC = "Accumulation Reference Default Value is Deactivated.";
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);


                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        #endregion

		#region "GetxmlMethods"
		public DataSet GetAccumulationReference(string ACC_ID)
		{
            string strSql = "GET_MNT_ACCUMULATION_REFERENCE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ACC_ID",ACC_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

        public static string GetXmlForPageControls(string ACC_ID)
        {
            string strSql = "GET_MNT_ACCUMULATION_REFERENCE";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@ACC_ID", int.Parse(ACC_ID));
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet.GetXml();
        }

		#endregion

       

		public static DataSet GetWolverineUsers(string systemID)
		{
			
			string strSQLQuery = "SELECT ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS WOLVERINE_USERS,[USER_ID] as WOLVERINE_USER_ID FROM MNT_USER_LIST WHERE USER_SYSTEM_ID = '" +systemID + "' and ISNULL(IS_ACTIVE,'N') = 'Y'  ORDER BY WOLVERINE_USERS ASC";
			DataSet objDS = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
				
			try
			{
				objDS = objDataWrapper.ExecuteDataSet(strSQLQuery);
				return objDS;
			}
			catch(Exception exc)
			{
				throw (new Exception("Some error occurred.Please try again !. " +exc.Message));
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
        public static  DataSet GetAccumulationCriteriaList(int LOB_ID)
        {
            string strSql = "GET_CRITERIA_LIST";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LOB_ID", LOB_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }


       //Added by Ruchika Chauhan on 2-March-2012 for TFS Bug # 3635
        public static string GenerateAccumulationReferenceCode(int LOB_ID)
        {
            string strSql = "PROC_GenerateAccumulationReferenceCode";

            DataSet dsICode = new DataSet();

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LOB_ID", LOB_ID);

            dsICode = objDataWrapper.ExecuteDataSet(strSql);

            return dsICode.Tables[0].Rows[0][0].ToString();
        }       
    }
}
