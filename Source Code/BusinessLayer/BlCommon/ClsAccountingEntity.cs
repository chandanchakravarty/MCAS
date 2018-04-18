
/******************************************************************************************
	<Author					: Gaurav Tyagi- >
	<Start Date				: 15 April, 2005-	>
	<End Date				: 22 April, 2005- >
	<Description			: - >This file is used to implement methods / functions 
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: 15 April , 2005- >
	<Modified By			: Gaurav Tyagi- >
	<Purpose				: Add Fill drop down functions- >
	
	<Modified Date			: - Anshuman
	<Modified By			: - June 08, 2005
	<Purpose				: - transaction description modified
*******************************************************************************************/
using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Account;
using Cms.Model.Maintenance;

namespace Cms.BusinessLayer.BlCommon
{
    /// <summary>
    /// Summary description for ClsAccountingEntity.
    /// </summary>
    public class ClsAccountingEntity : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        private const string MNT_ACCOUNTING_ENTITY_LIST = "MNT_ACCOUNTING_ENTITY_LIST";
        private const string ACTIVATE_DEACTIVATE_PROCEDURE = "Proc_ActivateDeactivateAccountingEntity";

        private bool boolTransactionRequired = true;
        public bool TransactionRequired
        {
            get
            {
                return boolTransactionRequired;
            }
            set
            {
                boolTransactionRequired = value;
            }
        }

        #region Public Property
        private int REC_ID;

        public int RecordId
        {
            get
            {
                return REC_ID;
            }
        }
        #endregion

        #region Constructors
        public ClsAccountingEntity()
        {
            base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROCEDURE;
        }

        public ClsAccountingEntity(bool transactionLogRequired)
            : this()
        {
            base.TransactionLogRequired = transactionLogRequired;

        }
        #endregion

        #region Add(Insert) Function
        public int Add(Cms.Model.Maintenance.ClsAccountingInfo objAccountingInfo, bool transactionLogReq)
        {
            string strStoredProc = "Proc_InsertAccountingEntity";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new

                DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@ENTITY_ID", objAccountingInfo.ENTITY_ID);
                objDataWrapper.AddParameter("@ENTITY_TYPE", objAccountingInfo.ENTITY_TYPE);
                objDataWrapper.AddParameter("@DIVISION_ID", objAccountingInfo.DIVISION_ID);
                objDataWrapper.AddParameter("@DEPARTMENT_ID", objAccountingInfo.DEPARTMENT_ID);
                objDataWrapper.AddParameter("@PROFIT_CENTER_ID", objAccountingInfo.PROFIT_CENTER_ID);
                objDataWrapper.AddParameter("@Is_Active", objAccountingInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@Created_By", objAccountingInfo.CREATED_BY);
                objDataWrapper.AddParameter("@Created_DateTime", objAccountingInfo.CREATED_DATETIME);
                //objDataWrapper.AddParameter("@REC_ID",objAccountingInfo.REC_ID,SqlDbType.Int,ParameterDirection.Output);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@REC_ID", null, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                //if transaction required
                if (base.TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    objAccountingInfo.TransactLabel = MapTransactionLabel("/cmsweb/maintenance/AddAccountingEntity.aspx.resx");
                    string strTranXML = objBuilder.GetTransactionLogXML(objAccountingInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objAccountingInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1627", ""); //"New accounting entity is added";	
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                REC_ID = int.Parse(objSqlParameter.Value.ToString());

                if (REC_ID == -1)
                {
                    returnResult = -1;
                }

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

        public int Add(Cms.Model.Maintenance.ClsAccountingInfo objAccountingEntityInfo)
        {
            return Add(objAccountingEntityInfo, TransactionRequired);
        }
        #endregion

        /*
		#region Add(Activate/Deactivate) functions
		/// <summary>
		/// This Function is used to update Status of a record in MNT_USER_TYPES table.
		/// This function is called from AddUserType page.
		/// </summary>
		/// <param name="UserTypeId">User Type Id to check the status of record</param>
		/// <param name="value">Value can be N or Y</param>
		/// <returns></returns>
		public bool ActivateDeactivateAccountingEntity(int intRecId, string value)
		{
			try
			{
				//base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROCEDURE;
				base.ActivateDeactivate(intRecId.ToString(),value);
				//			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
				//			
				//				objDataWrapper.AddParameter("@User_Type_ID",intUserTypeId);
				//				objDataWrapper.AddParameter("@Is_Active",value);
				//				objDataWrapper.ExecuteNonQuery(strStoredProc);
				//				objDataWrapper.ClearParameteres();
				//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return true;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
							
			
		}
		#endregion
		*/

        #region "Get Xml Methods"
        public static string GetXmlForPageControls(string strAccountEntityId, string strEntityType)
        {
            string strXml = "";
            string strSql = "";
            switch (strEntityType.ToUpper().ToString())
            {
                case "FC":
                    strSql = "SELECT acList.REC_ID,acList.ENTITY_ID,acList.ENTITY_TYPE,fcList.COMPANY_NAME as EntityName,divList.DIV_ID as Division,deptList.DEPT_ID as DeptId,pcList.PC_ID as ProfitCenterId,acList.IS_ACTIVE,";
                    strSql += " acList.CREATED_BY,acList.CREATED_DATETIME,acList.MODIFIED_BY,acList.LAST_UPDATED_DATETIME";
                    //cid1,ccode2,add1 4,phone5,fax6,mobile7,lname8,fname9,mname10,add2 11,	city12,state13,province14,zip15,country16,ext17,email18,														pager19,hphone20,tollfree21,note22 , IS_ACTIVE 23, CONTACT_SALUTATION 24,CONTACT_POS 25,CONTACT_TYPE_ID26,INDIVIDUAL_CONTACT_ID27,GetQuery32
                    //1,2,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27
                    strSql += " FROM MNT_ACCOUNTING_ENTITY_LIST acList";
                    strSql += " INNER JOIN MNT_DIV_LIST divList ON acList.DIVISION_ID=divList.DIV_ID ";
                    strSql += " INNER JOIN MNT_DEPT_LIST deptList ON acList.DEPARTMENT_ID=deptList.DEPT_ID ";
                    strSql += " INNER JOIN MNT_PROFIT_CENTER_LIST pcList ON acList.PROFIT_CENTER_ID = pcList.PC_ID";
                    strSql += " INNER JOIN MNT_FINANCE_COMPANY_LIST fcList ON acList.ENTITY_Id= fcList.COMPANY_ID";
                    strSql += " WHERE acList.REC_ID=" + strAccountEntityId + "";
                    break;
                case "TAX":
                    strSql = "SELECT acList.REC_ID,acList.ENTITY_ID,acList.ENTITY_TYPE,taxList.TAX_NAME as EntityName,divList.DIV_ID as Division,deptList.DEPT_ID as DeptId,pcList.PC_ID as ProfitCenterId,acList.IS_ACTIVE,";
                    strSql += " acList.CREATED_BY,acList.CREATED_DATETIME,acList.MODIFIED_BY,acList.LAST_UPDATED_DATETIME";
                    //cid1,ccode2,add1 4,phone5,fax6,mobile7,lname8,fname9,mname10,add2 11,	city12,state13,province14,zip15,country16,ext17,email18,														pager19,hphone20,tollfree21,note22 , IS_ACTIVE 23, CONTACT_SALUTATION 24,CONTACT_POS 25,CONTACT_TYPE_ID26,INDIVIDUAL_CONTACT_ID27,GetQuery32
                    //1,2,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27
                    strSql += " FROM MNT_ACCOUNTING_ENTITY_LIST acList";
                    strSql += " INNER JOIN MNT_DIV_LIST divList ON acList.DIVISION_ID=divList.DIV_ID ";
                    strSql += " INNER JOIN MNT_DEPT_LIST deptList ON acList.DEPARTMENT_ID=deptList.DEPT_ID ";
                    strSql += " INNER JOIN MNT_PROFIT_CENTER_LIST pcList ON acList.PROFIT_CENTER_ID = pcList.PC_ID";
                    strSql += " INNER JOIN MNT_TAX_ENTITY_LIST taxList ON acList.ENTITY_Id= taxList.TAX_ID";
                    strSql += " WHERE acList.REC_ID=" + strAccountEntityId + "";
                    break;

            }
            DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr, CommandType.Text, strSql);

            if (objDataSet.Tables[0].Rows.Count == 0)
            {
                strXml = "";
            }
            else
            {
                strXml = objDataSet.GetXml().ToString();
            }
            return strXml;


        }
        #endregion



        #region Delete functions
        /// <summary>
        /// deletes the information passed in model object to database.
        /// </summary>
        public int Delete(int intRecID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            string strStoredProc = "Proc_DeleteAccountingEntity";
            objDataWrapper.AddParameter("@RecId", intRecID);
            int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
            return intResult;

        }
        #endregion

        /// <summary>
        /// Policy Meleventos
        /// </summary>
        /// <param name="objCurrencyRateinfo"></param>
        /// <returns></returns>
        #region PolicyMeleventos
        public int AddPolicy(ClsPolicyMeleventInfo objClsPolicyMeleventInfo)
        {
            int returnValue = 0;

            if (objClsPolicyMeleventInfo.RequiredTransactionLog)
            {

                //D:\Projects\EBIX-ADVANTAGE-BRAZIL\Source Code\Cms\Account\Aspx\PolicyMelEventsDetail.aspx.resx
                objClsPolicyMeleventInfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/Aspx/PolicyMelEventsDetail.aspx.resx");
                returnValue = objClsPolicyMeleventInfo.AddPolicy();

            }
            else
                returnValue = objClsPolicyMeleventInfo.AddPolicy();
            return returnValue;
        }

        public int UpdatePolicy(ClsPolicyMeleventInfo objClsPolicyMeleventInfo)
        {
            int returnValue = 0;

            if (objClsPolicyMeleventInfo.RequiredTransactionLog)
            {
                objClsPolicyMeleventInfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/Aspx/PolicyMelEventsDetail.aspx.resx");


                returnValue = objClsPolicyMeleventInfo.UpdatePolicy();

            }//if (objCurrencyRateinfo.RequiredTransactionLog)

            return returnValue;
        }


        public ClsPolicyMeleventInfo FetchData(Int32 row_id)
        {

            DataSet dsCount = null;
            ClsPolicyMeleventInfo objClsPolicyMeleventInfo = new ClsPolicyMeleventInfo();
            try
            {
                dsCount = objClsPolicyMeleventInfo.FetchData(row_id);

                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objClsPolicyMeleventInfo);
                }

            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return objClsPolicyMeleventInfo;
        }

        public int DeletePolicy(ClsPolicyMeleventInfo objClsPolicyMeleventInfo)
        {

            int returnValue = 0;

            if (objClsPolicyMeleventInfo.RequiredTransactionLog)
            {
                objClsPolicyMeleventInfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/Aspx/PolicyMelEventsDetail.aspx.resx");
                returnValue = objClsPolicyMeleventInfo.DeletePolicy();

            }
            return returnValue;
        }
        #endregion



        #region Update Function
        public int Update(ClsAccountingInfo objAccountingInfo, ClsAccountingInfo objOldAccountingInfo)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

            string strTranXML;
            int returnResult = 0;
            objBuilder.WhereClause = " WHERE REC_ID = " + objOldAccountingInfo.REC_ID.ToString();
            objAccountingInfo.TransactLabel = MapTransactionLabel("/cmsweb/maintenance/AddAccountingEntity.aspx.resx");
            string strUpdate = objBuilder.GetUpdateSQL(objOldAccountingInfo, objAccountingInfo, out strTranXML);

            if (strUpdate != "")
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                try
                {
                    if (base.TransactionLogRequired)
                    {
                        ClsTransactionInfo objTransactionInfo = new ClsTransactionInfo();

                        objTransactionInfo.TRANS_TYPE_ID = 2;
                        objTransactionInfo.RECORDED_BY = objAccountingInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1628", ""); //"Accounting Entity is modified";
                        objTransactionInfo.CHANGE_XML = strTranXML;

                        returnResult = objDataWrapper.ExecuteNonQuery(strUpdate, objTransactionInfo);

                    }
                    else
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery(strUpdate);
                    }
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                    return returnResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (objDataWrapper != null)
                    {
                        objDataWrapper.Dispose();
                    }
                    if (objBuilder != null)
                    {
                        objBuilder = null;
                    }
                }

            }
            else
            {
                return 0;
            }

        }


    }
        #endregion



}

