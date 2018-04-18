/******************************************************************************************
<Author				: -     Kuldeep Saxena
<Start Date			: -	    11/Feb/2011
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
using Cms.Model.Policy;


namespace Cms.BusinessLayer.BlCommon.Accumulation
{
  public  class ClsPolicyAccumulationDetails:Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        private const	string		MNT_ACCUMULATION_REFERENCE			=	"POL_ACCUMULATION_DETAILS";

		#region Private Instance Variables
		private	bool	boolTransactionLog;
		
        //private const string ACTIVATE_DEACTIVATE_PROC = "ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_REFERENCE";
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
        public ClsPolicyAccumulationDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			//base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
            
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objBudgetCategoryInfo ">Model class object.</param>
		/// <returns>No of records effected.</returns>
        public int Add(ClsPolicyAccumulationDetailsInfo objPolicyAccumulationDetailsInfo, string XmlFilePath)
		{
            string strStoredProc = "PROC_INSERT_POL_ACCUMULATION_DETAILS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
                        
			try
			{

    //           
    //@ACCUMULATION_CODE ,
    //@ACC_REF_NO ,
    //@TOTAL_NO_OF_POLICIES ,
    //@OWN_RETENTION_LIMIT,
    //@TREATY_CAPACITY_LIMIT ,
    //@ACCUMULATION_LIMIT_AVAILABLE ,
    //@TOTAL_SUM_INSURED ,
    //@FACULTATIVE_RI ,
    //@GROSS_RETAINED_SUM_INSURED ,
    //@OWN_RETENTION ,
    //@QUOTA_SHARE ,
    //@FIRST_SURPLUS ,
    //@OWN_ABSOLUTE_NET_RETENSTION ,
    
                objDataWrapper.AddParameter("@ACCUMULATION_CODE", objPolicyAccumulationDetailsInfo.ACCUMULATION_CODE);
                objDataWrapper.AddParameter("@ACC_REF_NO", objPolicyAccumulationDetailsInfo.ACC_REF_NO);
                objDataWrapper.AddParameter("@TOTAL_NO_OF_POLICIES", objPolicyAccumulationDetailsInfo.TOTAL_NO_OF_POLICIES);
                objDataWrapper.AddParameter("@OWN_RETENTION_LIMIT", objPolicyAccumulationDetailsInfo.OWN_RETENTION_LIMIT);
                objDataWrapper.AddParameter("@TREATY_CAPACITY_LIMIT", objPolicyAccumulationDetailsInfo.TREATY_CAPACITY_LIMIT);
                objDataWrapper.AddParameter("@ACCUMULATION_LIMIT_AVAILABLE", objPolicyAccumulationDetailsInfo.ACCUMULATION_LIMIT_AVAILABLE);
                objDataWrapper.AddParameter("@TOTAL_SUM_INSURED", objPolicyAccumulationDetailsInfo.TOTAL_SUM_INSURED);
                objDataWrapper.AddParameter("@FACULTATIVE_RI", objPolicyAccumulationDetailsInfo.FACULTATIVE_RI);
                objDataWrapper.AddParameter("@GROSS_RETAINED_SUM_INSURED", objPolicyAccumulationDetailsInfo.GROSS_RETAINED_SUM_INSURED);
                objDataWrapper.AddParameter("@OWN_RETENTION", objPolicyAccumulationDetailsInfo.OWN_RETENTION);
                objDataWrapper.AddParameter("@QUOTA_SHARE", objPolicyAccumulationDetailsInfo.QUOTA_SHARE);
                objDataWrapper.AddParameter("@FIRST_SURPLUS", objPolicyAccumulationDetailsInfo.FIRST_SURPLUS);
                objDataWrapper.AddParameter("@OWN_ABSOLUTE_NET_RETENSTION", objPolicyAccumulationDetailsInfo.OWN_ABSOLUTE_NET_RETENSTION);
                objDataWrapper.AddParameter("@CREATED_BY",objPolicyAccumulationDetailsInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME",System.DateTime.Now);
                objDataWrapper.AddParameter("@CUSTOMER_ID",objPolicyAccumulationDetailsInfo.CUSTOMER_ID );
                objDataWrapper.AddParameter("@POLICY_ID", objPolicyAccumulationDetailsInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPolicyAccumulationDetailsInfo.POLICY_VERSION_ID);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@ACCUMULATION_ID", objPolicyAccumulationDetailsInfo.ACCUMULATION_ID, SqlDbType.Int, ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
                    //objFundTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
                    objPolicyAccumulationDetailsInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objPolicyAccumulationDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY = objPolicyAccumulationDetailsInfo.CREATED_BY;
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
                    objPolicyAccumulationDetailsInfo.ACCUMULATION_ID= ACC_ID;
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
                    objPolicyAccumulationDetailsInfo.ACCUMULATION_ID = ACC_ID;
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
        public int Update(ClsPolicyAccumulationDetailsInfo objOldAccumulationReferenceInfo, ClsPolicyAccumulationDetailsInfo objAccumulationReferenceInfo, string XmlFilePath)
		{
            //string strStoredProc = "UPDATE_MNT_ACCUMULATION_REFERENCE";
            //string strTranXML;
            //int returnResult = 0;
            //SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            //DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            //try 
            //{
            //    objDataWrapper.AddParameter("@ACC_ID", objAccumulationReferenceInfo.ACC_ID);
            //    objDataWrapper.AddParameter("@ACC_REF_NO", objAccumulationReferenceInfo.ACC_REF_NO);
            //    objDataWrapper.AddParameter("@LOB_ID", objAccumulationReferenceInfo.LOB_ID);
            //    objDataWrapper.AddParameter("@CRITERIA_ID", objAccumulationReferenceInfo.CRITERIA_ID);
            //    objDataWrapper.AddParameter("@CRITERIA_VALUE", objAccumulationReferenceInfo.CRITERIA_VALUE);
            //    objDataWrapper.AddParameter("@TREATY_CAPACITY_LIMIT", objAccumulationReferenceInfo.TREATY_CAPACITY_LIMIT);
            //    objDataWrapper.AddParameter("@USED_LIMIT", objAccumulationReferenceInfo.USED_LIMIT);
            //    objDataWrapper.AddParameter("@EFFECTIVE_DATE", objAccumulationReferenceInfo.EFFECTIVE_DATE);
            //    objDataWrapper.AddParameter("@EXPIRATION_DATE", objAccumulationReferenceInfo.EXPIRATION_DATE);
               
               
               
            //    if(base.TransactionLogRequired) 
            //    {
            //        //objBudgetCategoryInfo .TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
            //        objAccumulationReferenceInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);

            //        objBuilder.GetUpdateSQL(objOldAccumulationReferenceInfo, objAccumulationReferenceInfo, out strTranXML);

            //        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
            //        objTransactionInfo.TRANS_TYPE_ID	=	3;
            //        objTransactionInfo.RECORDED_BY = objAccumulationReferenceInfo.MODIFIED_BY;
            //        objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
            //        objTransactionInfo.CHANGE_XML		=	strTranXML;
            //        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

            //    }
            //    else
            //    {
            //        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
            //    }
            //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            //    return returnResult;
            //}
            //catch(Exception ex)
            //{
            //    throw(ex);
            //}
            //finally
            //{
            //    if(objDataWrapper != null) 
            //    {
            //        objDataWrapper.Dispose();
            //    }
            //    if(objBuilder != null) 
            //    {
            //        objBuilder = null;
            //    }
            //}
            return 0;
		}
		#endregion

        #region ActivateDeactivate() function
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objDVInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        
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
		public static  DataSet GetAccumulationCriteriaList(int LOB_ID)
        {
            string strSql = "GET_CRITERIA_LIST";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LOB_ID", LOB_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }

        //Added by Ruchika Chauhan on 5-March-2012 for TFS Bug # 3635        
        public DataSet FillAccumulationReference(int Acc_id, string Acc_Ref, int Cust_id, int Policy_id, int Policy_version_id, out string TotalPolicies, out string TotalSumInsured, out string AvailableLimit)
        {
            string strStoredProc = "Proc_GetAccumulationReference";
            
            DataSet dsAccounts = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@Acc_id", Acc_id, SqlDbType.Int);
            objDataWrapper.AddParameter("@Acc_Ref", Acc_Ref, SqlDbType.NVarChar);
            objDataWrapper.AddParameter("@CUSTOMER_ID", Cust_id, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_version_id, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", Policy_id, SqlDbType.Int);
            SqlParameter sqlTotalPolicies = (SqlParameter)objDataWrapper.AddParameter("@TOTAL_POLICIES", null, SqlDbType.Int, ParameterDirection.Output);
            SqlParameter sqlTotSumInsured = (SqlParameter)objDataWrapper.AddParameter("@TotSumInsured", null, SqlDbType.Int, ParameterDirection.Output);
            SqlParameter sqlAvailableLimit = (SqlParameter)objDataWrapper.AddParameter("@AvailableLimit", null, SqlDbType.Int, ParameterDirection.Output);

            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);           

            TotalPolicies = sqlTotalPolicies.Value.ToString();
            TotalSumInsured = sqlTotSumInsured.Value.ToString();
            AvailableLimit = sqlAvailableLimit.Value.ToString();

            return dsAccounts;
        }

        public DataSet FillAccumulationCode(int Acc_id)
        {

            string strStoredProc1 = "Proc_GenerateAccumulationCode";

            DataSet dsAccounts = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@Acc_id", Acc_id, SqlDbType.Int);
            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc1);                       
            return dsAccounts;
        }
       
        //Added by Ruchika Chauhan on 13-March-2012 for TFS Bug # 3635        
        public DataSet FillAccumulationReferenceAfterPolicyCommit(int Policy_id, int Policy_version_id, int Cust_id)
        {
            string strStoredProc1 = "PROC_GET_POL_ACCUMULATION_DETAILS";

            DataSet dsAccounts = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);                      
            
            objDataWrapper.AddParameter("@POLICY_ID", Policy_id, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_version_id, SqlDbType.Int);
            objDataWrapper.AddParameter("@CUSTOMER_ID", Cust_id, SqlDbType.Int);

            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc1);           

            return dsAccounts;
        }

        //Added by Kuldeep Saxena on 13-March-2012 for TFS Bug # 3635 to fill grid      
        public DataSet GetAcccumulatedPolicyDetails(string Acc_Ref)
        {
            string strStoredProc1 = "PROC_GET_ACCUMULATED_POLICY_DETAILS";

            DataSet dsAccounts = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            objDataWrapper.AddParameter("@Acc_Ref", Acc_Ref, SqlDbType.NVarChar);
          
            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc1);

            return dsAccounts;
        }
    }
}
