using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlCommon
{
    public class ClsPremisesInformation : Cms.BusinessLayer.BlCommon.ClsCommon
    {
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


        //public DataSet GetPremisesLocDetails(string CUST_ID, string POL_ID, string POL_VERSION_ID, string LOCATION_ID, string PREMISES_ID)
        public DataSet GetPremisesLocDetails(string CUST_ID, string POL_ID, string POL_VERSION_ID, string LOCATION_ID)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@CUSTOMER_ID", CUST_ID);
            sqlParams[1] = new SqlParameter("@POLICY_ID", POL_ID);
            sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID", POL_VERSION_ID);
            //sqlParams[3] = new SqlParameter("@LOCATION_ID", LOCATION_ID);
            //sqlParams[4] = new SqlParameter("@PREMISES_ID", PREMISES_ID);
            DataSet ds = null;
            try
            {
                ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "GET_POL_BOP_PREMISES_LOC_DETAILS", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
            }
            //return ds;
        }
        public int AddPremisesLocDetails(ClsPremisesInfo objPremiseInfo, string XmlFilePath)
        {

            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objPremiseInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            objDataWrapper.AddParameter("@CUSTOMER_ID", objPremiseInfo.CUSTOMER_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", objPremiseInfo.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPremiseInfo.POLICY_VERSION_ID);
            objDataWrapper.AddParameter("@LOCATION_ID", objPremiseInfo.LOCATION_ID);
            //objDataWrapper.AddParameter("@PREMISES_ID", objPremiseInfo.PREMISES_ID);

            ////BUILDING
            objDataWrapper.AddParameter("@DESC_BLDNG", objPremiseInfo.DESC_BLDNG);
            objDataWrapper.AddParameter("@DESC_OPERTN", objPremiseInfo.DESC_OPERTN);
            objDataWrapper.AddParameter("@LST_ALL_OCCUP", objPremiseInfo.LST_ALL_OCCUP);
            objDataWrapper.AddParameter("@ANN_SALES", objPremiseInfo.ANN_SALES);
            objDataWrapper.AddParameter("@TOT_PAYROLL", objPremiseInfo.TOT_PAYROLL);
            objDataWrapper.AddParameter("@PROT_CLS", objPremiseInfo.PROT_CLS);
            objDataWrapper.AddParameter("@IS_ALM_USED", objPremiseInfo.IS_ALM_USED);
            objDataWrapper.AddParameter("@IS_RES_SPACE", objPremiseInfo.IS_RES_SPACE);

            objDataWrapper.AddParameter("@RES_SPACE_SMK_DET", objPremiseInfo.RES_SPACE_SMK_DET);
            objDataWrapper.AddParameter("@RES_OCC", objPremiseInfo.RES_OCC);
            objDataWrapper.AddParameter("@FIRE_HYDRANT_DIST", objPremiseInfo.FIRE_HYDRANT_DIST);
            objDataWrapper.AddParameter("@FIRE_STATION_DIST", objPremiseInfo.FIRE_STATION_DIST);
            objDataWrapper.AddParameter("@FIRE_DIST_NAME", objPremiseInfo.FIRE_DIST_NAME);


            objDataWrapper.AddParameter("@FIRE_DIST_CODE", objPremiseInfo.FIRE_DIST_CODE);
            objDataWrapper.AddParameter("@BCEGS", objPremiseInfo.BCEGS);
            objDataWrapper.AddParameter("@CITY_LMT", objPremiseInfo.CITY_LMT);
            objDataWrapper.AddParameter("@SWIMMING_POOL", objPremiseInfo.SWIMMING_POOL);
            objDataWrapper.AddParameter("@PLAY_GROUND", objPremiseInfo.PLAY_GROUND);


            objDataWrapper.AddParameter("@BUILD_UNDER_CON", objPremiseInfo.BUILD_UNDER_CON);
            objDataWrapper.AddParameter("@BUILD_SHPNG_CENT", objPremiseInfo.BUILD_SHPNG_CENT);
            objDataWrapper.AddParameter("@BOILER", objPremiseInfo.BOILER);
            objDataWrapper.AddParameter("@MED_EQUIP", objPremiseInfo.MED_EQUIP);
            objDataWrapper.AddParameter("@ALARM_TYPE", objPremiseInfo.ALARM_TYPE);
            //hghhghg
            objDataWrapper.AddParameter("@ALARM_DESC", objPremiseInfo.ALARM_DESC);
            objDataWrapper.AddParameter("@SAFE_VAULT", objPremiseInfo.SAFE_VAULT);
            objDataWrapper.AddParameter("@PREMISE_ALARM", objPremiseInfo.PREMISE_ALARM);
            objDataWrapper.AddParameter("@CYL_DOOR_LOCK", objPremiseInfo.CYL_DOOR_LOCK);

            objDataWrapper.AddParameter("@SAFE_VAULT_LBL", objPremiseInfo.SAFE_VAULT_LBL);
            // new ig
            objDataWrapper.AddParameter("@SAFE_VAULT_CLASS", objPremiseInfo.SAFE_VAULT_CLASS);
            objDataWrapper.AddParameter("@SAFE_VAULT_MANUFAC", objPremiseInfo.SAFE_VAULT_MANUFAC);
            objDataWrapper.AddParameter("@MAX_CASH_PREM", objPremiseInfo.MAX_CASH_PREM);
            objDataWrapper.AddParameter("@MAX_CASH_MSG", objPremiseInfo.MAX_CASH_MSG);
            // new it

            objDataWrapper.AddParameter("@MONEY_OVER_NIGHT", objPremiseInfo.MONEY_OVER_NIGHT);
            objDataWrapper.AddParameter("@FREQUENCY_DEPOSIT", objPremiseInfo.FREQUENCY_DEPOSIT);
            objDataWrapper.AddParameter("@SAFE_DOOR_CONST", objPremiseInfo.SAFE_DOOR_CONST);
            objDataWrapper.AddParameter("@GRADE", objPremiseInfo.GRADE);
            objDataWrapper.AddParameter("@OTH_PROTECTION", objPremiseInfo.OTH_PROTECTION);
            objDataWrapper.AddParameter("@RIGHT_EXP_DESC", objPremiseInfo.RIGHT_EXP_DESC);
            objDataWrapper.AddParameter("@RIGHT_EXP_DIST", objPremiseInfo.RIGHT_EXP_DIST);
            objDataWrapper.AddParameter("@LEFT_EXP_DESC", objPremiseInfo.LEFT_EXP_DESC);

            objDataWrapper.AddParameter("@LEFT_EXP_DIST", objPremiseInfo.LEFT_EXP_DIST);
            objDataWrapper.AddParameter("@FRONT_EXP_DESC", objPremiseInfo.FRONT_EXP_DESC);
            objDataWrapper.AddParameter("@FRONT_EXP_DIST", objPremiseInfo.FRONT_EXP_DIST);
            objDataWrapper.AddParameter("@REAR_EXP_DESC", objPremiseInfo.REAR_EXP_DESC);

            objDataWrapper.AddParameter("@REAR_EXP_DIST", objPremiseInfo.REAR_EXP_DIST);
            objDataWrapper.AddParameter("@COUNTY", objPremiseInfo.COUNTY);

            objDataWrapper.AddParameter("@BLANKETRATE", objPremiseInfo.BLANKETRATE);
            
            
            //SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@LOC_DETAILS_ID", SqlDbType.Int, ParameterDirection.Output);
            SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@PREMISES_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objPremiseInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objPremiseInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objPremiseInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objPremiseInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objPremiseInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_BOP_PREMISES_LOC_DETAILS", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_BOP_PREMISES_LOC_DETAILS");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                int PREMLOC_ID;
                if (retParam.Value != null && retParam.Value.ToString() != "")
                {
                    PREMLOC_ID = int.Parse(retParam.Value.ToString());
                    //objPremiseInfo.LOC_DETAILS_ID = PREMLOC_ID;
                    objPremiseInfo.PREMISES_ID = PREMLOC_ID;
                }
                else
                {
                    PREMLOC_ID = -1;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return PREMLOC_ID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }
        public int UpdatePremisesLocDetails(ClsPremisesInfo objPremiseInfo, string XmlFilePath)
        {

            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objPremiseInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            //objDataWrapper.AddParameter("@CUSTOMER_ID", objPremiseInfo.CUSTOMER_ID, SqlDbType.Int);
            //objDataWrapper.AddParameter("@POLICY_ID", objPremiseInfo.POLICY_ID);
            //objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPremiseInfo.POLICY_VERSION_ID);
            //objDataWrapper.AddParameter("@LOCATION_ID", objPremiseInfo.LOCATION_ID);
            //objDataWrapper.AddParameter("@PREMISES_ID", objPremiseInfo.PREMISES_ID);

            ////BUILDING
            //objDataWrapper.AddParameter("@LOC_DETAILS_ID", objPremiseInfo.LOC_DETAILS_ID);
            objDataWrapper.AddParameter("@PREMISES_ID", objPremiseInfo.PREMISES_ID);
            

            objDataWrapper.AddParameter("@DESC_BLDNG", objPremiseInfo.DESC_BLDNG);
            objDataWrapper.AddParameter("@DESC_OPERTN", objPremiseInfo.DESC_OPERTN);
            objDataWrapper.AddParameter("@LST_ALL_OCCUP", objPremiseInfo.LST_ALL_OCCUP);
            objDataWrapper.AddParameter("@ANN_SALES", objPremiseInfo.ANN_SALES);
            objDataWrapper.AddParameter("@TOT_PAYROLL", objPremiseInfo.TOT_PAYROLL);
            objDataWrapper.AddParameter("@PROT_CLS", objPremiseInfo.PROT_CLS);
            objDataWrapper.AddParameter("@IS_ALM_USED", objPremiseInfo.IS_ALM_USED);
            objDataWrapper.AddParameter("@IS_RES_SPACE", objPremiseInfo.IS_RES_SPACE);

            objDataWrapper.AddParameter("@RES_SPACE_SMK_DET", objPremiseInfo.RES_SPACE_SMK_DET);
            objDataWrapper.AddParameter("@RES_OCC", objPremiseInfo.RES_OCC);
            objDataWrapper.AddParameter("@FIRE_HYDRANT_DIST", objPremiseInfo.FIRE_HYDRANT_DIST);
            objDataWrapper.AddParameter("@FIRE_STATION_DIST", objPremiseInfo.FIRE_STATION_DIST);
            objDataWrapper.AddParameter("@FIRE_DIST_NAME", objPremiseInfo.FIRE_DIST_NAME);


            objDataWrapper.AddParameter("@FIRE_DIST_CODE", objPremiseInfo.FIRE_DIST_CODE);
            objDataWrapper.AddParameter("@BCEGS", objPremiseInfo.BCEGS);
            objDataWrapper.AddParameter("@CITY_LMT", objPremiseInfo.CITY_LMT);
            objDataWrapper.AddParameter("@SWIMMING_POOL", objPremiseInfo.SWIMMING_POOL);
            objDataWrapper.AddParameter("@PLAY_GROUND", objPremiseInfo.PLAY_GROUND);


            objDataWrapper.AddParameter("@BUILD_UNDER_CON", objPremiseInfo.BUILD_UNDER_CON);
            objDataWrapper.AddParameter("@BUILD_SHPNG_CENT", objPremiseInfo.BUILD_SHPNG_CENT);
            objDataWrapper.AddParameter("@BOILER", objPremiseInfo.BOILER);
            objDataWrapper.AddParameter("@MED_EQUIP", objPremiseInfo.MED_EQUIP);
            objDataWrapper.AddParameter("@ALARM_TYPE", objPremiseInfo.ALARM_TYPE);
            //hghhghg
            objDataWrapper.AddParameter("@ALARM_DESC", objPremiseInfo.ALARM_DESC);
            objDataWrapper.AddParameter("@SAFE_VAULT", objPremiseInfo.SAFE_VAULT);
            objDataWrapper.AddParameter("@PREMISE_ALARM", objPremiseInfo.PREMISE_ALARM);
            objDataWrapper.AddParameter("@CYL_DOOR_LOCK", objPremiseInfo.CYL_DOOR_LOCK);

            objDataWrapper.AddParameter("@SAFE_VAULT_LBL", objPremiseInfo.SAFE_VAULT_LBL);
            // new ig
            objDataWrapper.AddParameter("@SAFE_VAULT_CLASS", objPremiseInfo.SAFE_VAULT_CLASS);
            objDataWrapper.AddParameter("@SAFE_VAULT_MANUFAC", objPremiseInfo.SAFE_VAULT_MANUFAC);
            objDataWrapper.AddParameter("@MAX_CASH_PREM", objPremiseInfo.MAX_CASH_PREM);
            objDataWrapper.AddParameter("@MAX_CASH_MSG", objPremiseInfo.MAX_CASH_MSG);
            // new it

            objDataWrapper.AddParameter("@MONEY_OVER_NIGHT", objPremiseInfo.MONEY_OVER_NIGHT);
            objDataWrapper.AddParameter("@FREQUENCY_DEPOSIT", objPremiseInfo.FREQUENCY_DEPOSIT);
            objDataWrapper.AddParameter("@SAFE_DOOR_CONST", objPremiseInfo.SAFE_DOOR_CONST);
            objDataWrapper.AddParameter("@GRADE", objPremiseInfo.GRADE);
            objDataWrapper.AddParameter("@OTH_PROTECTION", objPremiseInfo.OTH_PROTECTION);
            objDataWrapper.AddParameter("@RIGHT_EXP_DESC", objPremiseInfo.RIGHT_EXP_DESC);
            objDataWrapper.AddParameter("@RIGHT_EXP_DIST", objPremiseInfo.RIGHT_EXP_DIST);
            objDataWrapper.AddParameter("@LEFT_EXP_DESC", objPremiseInfo.LEFT_EXP_DESC);

            objDataWrapper.AddParameter("@LEFT_EXP_DIST", objPremiseInfo.LEFT_EXP_DIST);
            objDataWrapper.AddParameter("@FRONT_EXP_DESC", objPremiseInfo.FRONT_EXP_DESC);
            objDataWrapper.AddParameter("@FRONT_EXP_DIST", objPremiseInfo.FRONT_EXP_DIST);
            objDataWrapper.AddParameter("@REAR_EXP_DESC", objPremiseInfo.REAR_EXP_DESC);

            objDataWrapper.AddParameter("@REAR_EXP_DIST", objPremiseInfo.REAR_EXP_DIST);
            objDataWrapper.AddParameter("@COUNTY", objPremiseInfo.COUNTY);
            objDataWrapper.AddParameter("@BLANKETRATE", objPremiseInfo.BLANKETRATE);


           // SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@LOC_DETAILS_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objPremiseInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objPremiseInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objPremiseInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objPremiseInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objPremiseInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Updated";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_BOP_PREMISES_LOC_DETAILS", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_BOP_PREMISES_LOC_DETAILS");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                //int PREMLOC_ID;
                //if (retParam.Value != null && retParam.Value.ToString() != "")
                //{
                //    PREMLOC_ID = int.Parse(retParam.Value.ToString());
                //    objPremiseInfo.LOC_DETAILS_ID = PREMLOC_ID;
                //}
                //else
                //{
                //    PREMLOC_ID = -1;
                //}

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return returnResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }

        public int DeletePremisesLocDetails(ClsPremisesInfo objPremiseInfo, string XmlFilePath)
        {
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            if (this.boolTransactionRequired)
            {
                objPremiseInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }
            //objDataWrapper.AddParameter("@LOC_DETAILS_ID", objPremiseInfo.LOC_DETAILS_ID);
            objDataWrapper.AddParameter("@PREMISES_ID", objPremiseInfo.PREMISES_ID);
            
            int intResult = 0;
            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objPremiseInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objPremiseInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objPremiseInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objPremiseInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objPremiseInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Deleted";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    intResult = objDataWrapper.ExecuteNonQuery("DELETE_POL_BOP_PREMISES_LOC_DETAILS", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    intResult = objDataWrapper.ExecuteNonQuery("DELETE_POL_BOP_PREMISES_LOC_DETAILS");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

               // int PREMLOC_ID;
                //if (retParam.Value != null && retParam.Value.ToString() != "")
                //{
                //    PREMLOC_ID = int.Parse(retParam.Value.ToString());
                //    objPremiseInfo.PREMLOC_ID = PREMLOC_ID;
                //}
                //else
                //{
                //    PREMLOC_ID = -1;
                //}

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                //return intResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            return intResult;
        }
        
        //public DataSet GetPremisesInformation(string CUST_ID, string POL_ID, string POL_VERSION_ID, string @LOCATION_ID,string @PREMISES_ID)
        public DataSet GetPremisesInformation(string CUST_ID, string POL_ID, string POL_VERSION_ID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CUSTOMER_ID", CUST_ID);
            sqlParams[1] = new SqlParameter("@POLICY_ID", POL_ID);
            sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID", POL_VERSION_ID);
            //sqlParams[3] = new SqlParameter("@LOCATION_ID", LOCATION_ID);
            //sqlParams[4] = new SqlParameter("@PREMISES_ID", PREMISES_ID);
            DataSet ds = null;
            try
            {
                ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "GET_POL_BOP_PREMISESLOCATIONS", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
            }
            //return ds;
        }

        public String GetMaxIDOfLocationNumber(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID, ref Int64 NUMBER)
        {
            String returnValue = String.Empty;
            DataSet dsTemp = new DataSet();
            string strSql = "Proc_GetMaxPremisesLocationNum";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", CUSTOMER_ID);
                

                SqlParameter returnSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN", SqlDbType.Int, ParameterDirection.ReturnValue);
                SqlParameter objSqlNUMBERPrem = (SqlParameter)objDataWrapper.AddParameter("@LOCATION_NUMBER", NUMBER, SqlDbType.BigInt, ParameterDirection.InputOutput);


                objDataWrapper.ExecuteNonQuery(strSql);
                returnValue = returnSqlParameter.Value.ToString();

                if (objSqlNUMBERPrem != null && objSqlNUMBERPrem.Value.ToString() != "")
                    NUMBER = Convert.ToInt64(objSqlNUMBERPrem.Value.ToString());

                return returnValue;
            }
            catch
            { return String.Empty; }
            finally
            { objDataWrapper.Dispose(); }
        }

        public int AddPremisesInformation(ClsNatureBusinessInfo objNatureBusinessInfo, string XmlFilePath)
        {

            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objNatureBusinessInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            objDataWrapper.AddParameter("@CUSTOMER_ID", objNatureBusinessInfo.CUSTOMER_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", objNatureBusinessInfo.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objNatureBusinessInfo.POLICY_VERSION_ID);
            //objDataWrapper.AddParameter("@BUSINESS_ID", objNatureBusinessInfo.BUSINESS_ID);
            objDataWrapper.AddParameter("@BUSINESS_NATURE", objNatureBusinessInfo.BUSINESS_NATURE);
            objDataWrapper.AddParameter("@PRIMARY_OPERATION", objNatureBusinessInfo.PRIMARY_OPERATION);
            objDataWrapper.AddParameter("@BUSINESS_START_DATE", objNatureBusinessInfo.BUSINESS_START_DATE);
            objDataWrapper.AddParameter("@OTHER_OPERATION", objNatureBusinessInfo.OTHER_OPERATION);
            objDataWrapper.AddParameter("@REPAIR_WORK", objNatureBusinessInfo.REPAIR_WORK);
            objDataWrapper.AddParameter("@PREMISES_WORK", objNatureBusinessInfo.PREMISES_WORK);
            objDataWrapper.AddParameter("@RETAIL_STORE", objNatureBusinessInfo.RETAIL_STORE);


            SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@PREMISES_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objNatureBusinessInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objNatureBusinessInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objNatureBusinessInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objNatureBusinessInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objNatureBusinessInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_NATURE_OF_BUSINESS", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_NATURE_OF_BUSINESS");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                int BUSINESS_ID;
                if (retParam.Value != null && retParam.Value.ToString() != "")
                {
                    BUSINESS_ID = int.Parse(retParam.Value.ToString());
                    objNatureBusinessInfo.BUSINESS_ID = BUSINESS_ID;
                }
                else
                {
                    BUSINESS_ID = -1;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return BUSINESS_ID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }

        //Added by Rajiv
        public DataSet GetPremisesLocation(string CUST_ID, string POL_ID, string POL_VERSION_ID, string @LOCATION_ID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CUSTOMER_ID", CUST_ID);
            sqlParams[1] = new SqlParameter("@POLICY_ID", POL_ID);
            sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID", POL_VERSION_ID);
            sqlParams[3] = new SqlParameter("@LOCATION_ID", LOCATION_ID);
            
            DataSet ds = null;
            try
            {
                ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "GET_POL_BOP_PREMISESLOCATIONS", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
            }
            //return ds;
        }
        //Added by Rajiv
        //public int AddPremisesLocation(ClsPremisesInfo objNatureBusinessInfo, string XmlFilePath)
        public int AddPremisesLocation(ClsPremiseLocationInfo objPremiseInfo, string XmlFilePath)

           
        {
            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objPremiseInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }
            
            objDataWrapper.AddParameter("@CUSTOMER_ID", objPremiseInfo.CUSTOMER_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", objPremiseInfo.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPremiseInfo.POLICY_VERSION_ID);
           // objDataWrapper.AddParameter("@LOCATION_ID", objPremiseInfo.LOCATION_ID);
            objDataWrapper.AddParameter("@BUILDING", objPremiseInfo.BUILDING);
          
            objDataWrapper.AddParameter("@STREET_ADDR", objPremiseInfo.STREET_ADDR);
            objDataWrapper.AddParameter("@CITY", objPremiseInfo.CITY);
            objDataWrapper.AddParameter("@STATE", objPremiseInfo.STATE);
            objDataWrapper.AddParameter("@COUNTY", objPremiseInfo.COUNTY);
            objDataWrapper.AddParameter("@ZIP", objPremiseInfo.ZIP);
            objDataWrapper.AddParameter("@INTEREST", objPremiseInfo.INTEREST);
            objDataWrapper.AddParameter("@FL_TM_EMP", objPremiseInfo.FL_TM_EMP);
            objDataWrapper.AddParameter("@PT_TM_EMP", objPremiseInfo.PT_TM_EMP);

            objDataWrapper.AddParameter("@ANN_REVENUE", objPremiseInfo.ANN_REVENUE);
            objDataWrapper.AddParameter("@OCC_AREA", objPremiseInfo.OCC_AREA);
            objDataWrapper.AddParameter("@OPEN_AREA", objPremiseInfo.OPEN_AREA);
            objDataWrapper.AddParameter("@TOT_AREA", objPremiseInfo.TOT_AREA);

            objDataWrapper.AddParameter("@AREA_LEASED", objPremiseInfo.AREA_LEASED);            
            
            //SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@PREMLOC_ID", SqlDbType.Int, ParameterDirection.Output);
            SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@LOCATION_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;
            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objPremiseInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objPremiseInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objPremiseInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objPremiseInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objPremiseInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    
                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_BOP_PREMISESLOCATIONS", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_BOP_PREMISESLOCATIONS");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                //int PREMLOC_ID;
                int LOC_ID;
                if (retParam.Value != null && retParam.Value.ToString() != "")
                {
                    LOC_ID = int.Parse(retParam.Value.ToString());
                    objPremiseInfo.PREMLOC_ID = LOC_ID;
                }
                else
                {
                    LOC_ID = -1;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return LOC_ID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }


        public int UpdatePremisesLocation(ClsPremiseLocationInfo objPremiseInfo, string XmlFilePath)
        {

            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objPremiseInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            //objDataWrapper.AddParameter("@CUSTOMER_ID", objPremiseInfo.CUSTOMER_ID, SqlDbType.Int);
            //objDataWrapper.AddParameter("@POLICY_ID", objPremiseInfo.POLICY_ID);
            //objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPremiseInfo.POLICY_VERSION_ID);

            //objDataWrapper.AddParameter("@PREMLOC_ID", objPremiseInfo.PREMLOC_ID);
            objDataWrapper.AddParameter("@LOCATION_ID", objPremiseInfo.LOCATION_ID);
            objDataWrapper.AddParameter("@BUILDING", objPremiseInfo.BUILDING);

            //BUILDING
            objDataWrapper.AddParameter("@STREET_ADDR", objPremiseInfo.STREET_ADDR);
            objDataWrapper.AddParameter("@CITY", objPremiseInfo.CITY);
            objDataWrapper.AddParameter("@STATE", objPremiseInfo.STATE);
            objDataWrapper.AddParameter("@COUNTY", objPremiseInfo.COUNTY);
            objDataWrapper.AddParameter("@ZIP", objPremiseInfo.ZIP);
            objDataWrapper.AddParameter("@INTEREST", objPremiseInfo.INTEREST);
            objDataWrapper.AddParameter("@FL_TM_EMP", objPremiseInfo.FL_TM_EMP);
            objDataWrapper.AddParameter("@PT_TM_EMP", objPremiseInfo.PT_TM_EMP);

            objDataWrapper.AddParameter("@ANN_REVENUE", objPremiseInfo.ANN_REVENUE);
            objDataWrapper.AddParameter("@OCC_AREA", objPremiseInfo.OCC_AREA);
            objDataWrapper.AddParameter("@OPEN_AREA", objPremiseInfo.OPEN_AREA);
            objDataWrapper.AddParameter("@TOT_AREA", objPremiseInfo.TOT_AREA);
            
            objDataWrapper.AddParameter("@AREA_LEASED", objPremiseInfo.AREA_LEASED);

            
            //SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@PREMLOC_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objPremiseInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objPremiseInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objPremiseInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objPremiseInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objPremiseInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Updated";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_BOP_PREMISESLOCATIONS", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_BOP_PREMISESLOCATIONS");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                //int PREMLOC_ID;
                //if (retParam.Value != null && retParam.Value.ToString() != "")
                //{
                //    PREMLOC_ID = int.Parse(retParam.Value.ToString());
                //    objPremiseInfo.PREMLOC_ID = PREMLOC_ID;
                //}
                //else
                //{
                //    PREMLOC_ID = -1;
                //}

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return returnResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }

        public int DeletePremisesLocation(ClsPremiseLocationInfo objPremiseInfo, string XmlFilePath)
        {
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
           
            if (this.boolTransactionRequired)
            {
                objPremiseInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }
           // objDataWrapper.AddParameter("@PREMLOC_ID", objPremiseInfo.PREMLOC_ID);
            objDataWrapper.AddParameter("@LOCATION_ID", objPremiseInfo.LOCATION_ID);
            
            int intResult=0;
            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objPremiseInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objPremiseInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objPremiseInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objPremiseInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objPremiseInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Deleted";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    intResult = objDataWrapper.ExecuteNonQuery("DELETE_POL_BOP_PREMISESLOCATIONS", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    intResult = objDataWrapper.ExecuteNonQuery("DELETE_POL_BOP_PREMISESLOCATIONS");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                int LOC_ID;
                //if (retParam.Value != null && retParam.Value.ToString() != "")
                //{
                //    PREMLOC_ID = int.Parse(retParam.Value.ToString());
                //    objPremiseInfo.PREMLOC_ID = PREMLOC_ID;
                //}
                //else
                //{
                //    PREMLOC_ID = -1;
                //}

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                //return intResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            return  intResult;	
        }

    }
}
