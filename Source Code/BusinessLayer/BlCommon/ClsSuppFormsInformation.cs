using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlCommon
{
    public class ClsSuppFormsInformation : Cms.BusinessLayer.BlCommon.ClsCommon
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

        public DataSet GetPremisesLocDetails(string CUST_ID, string POL_ID, string POL_VERSION_ID, string LOCATION_ID, string PREMISES_ID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CUSTOMER_ID", CUST_ID);
            sqlParams[1] = new SqlParameter("@POLICY_ID", POL_ID);
            sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID", POL_VERSION_ID);
            sqlParams[3] = new SqlParameter("@LOCATION_ID", LOCATION_ID);
            sqlParams[4] = new SqlParameter("@PREMISES_ID", PREMISES_ID);
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
            objDataWrapper.AddParameter("@PREMISES_ID", objPremiseInfo.PREMISES_ID);

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
            
            
            SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@LOC_DETAILS_ID", SqlDbType.Int, ParameterDirection.Output);

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
                    objPremiseInfo.LOC_DETAILS_ID = PREMLOC_ID;
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
            objDataWrapper.AddParameter("@LOC_DETAILS_ID", objPremiseInfo.LOC_DETAILS_ID);
            

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
            objDataWrapper.AddParameter("@LOC_DETAILS_ID", objPremiseInfo.LOC_DETAILS_ID);
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
        // For Restaurant
        public DataSet GetRestaurantInformation(string CUST_ID, string POL_ID, string POL_VERSION_ID, string @LOCATION_ID, string @PREMISES_ID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CUSTOMER_ID", CUST_ID);
            sqlParams[1] = new SqlParameter("@POLICY_ID", POL_ID);
            sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID", POL_VERSION_ID);
            sqlParams[3] = new SqlParameter("@LOCATION_ID", LOCATION_ID);
            sqlParams[4] = new SqlParameter("@PREMISES_ID", PREMISES_ID);
            DataSet ds = null;
            try
            {
                ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "GET_POL_SUP_FORM_RESTAURANT", sqlParams);
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
        public int AddRestaurantInformation(ClsSuppFormRestaurantInfo objRestaurantInfo, string XmlFilePath)
        {

            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objRestaurantInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            objDataWrapper.AddParameter("@CUSTOMER_ID", objRestaurantInfo.CUSTOMER_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", objRestaurantInfo.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objRestaurantInfo.POLICY_VERSION_ID);
            //objDataWrapper.AddParameter("@BUSINESS_ID", objNatureBusinessInfo.BUSINESS_ID);
            objDataWrapper.AddParameter("@LOCATION_ID", objRestaurantInfo.LOCATION_ID);
            objDataWrapper.AddParameter("@PREMISES_ID", objRestaurantInfo.PREMISES_ID);

            objDataWrapper.AddParameter("@SEATINGCAPACITY", objRestaurantInfo.SEATINGCAPACITY);
            objDataWrapper.AddParameter("@BUS_TYP_RESTURANT", objRestaurantInfo.BUS_TYP_RESTURANT);
            objDataWrapper.AddParameter("@BUS_TYP_FM_STYLE", objRestaurantInfo.BUS_TYP_FM_STYLE);
            objDataWrapper.AddParameter("@BUS_TYP_NGHT_CLUB", objRestaurantInfo.BUS_TYP_NGHT_CLUB);
            objDataWrapper.AddParameter("@BUS_TYP_FRNCHSED", objRestaurantInfo.BUS_TYP_FRNCHSED);


            objDataWrapper.AddParameter("@BUS_TYP_NT_FRNCHSED", objRestaurantInfo.BUS_TYP_NT_FRNCHSED);
            objDataWrapper.AddParameter("@BUS_TYP_SEASONAL", objRestaurantInfo.BUS_TYP_SEASONAL);
            objDataWrapper.AddParameter("@BUS_TYP_YR_ROUND", objRestaurantInfo.BUS_TYP_YR_ROUND);
            objDataWrapper.AddParameter("@BUS_TYP_DINNER", objRestaurantInfo.BUS_TYP_DINNER);
            objDataWrapper.AddParameter("@BUS_TYP_BNQT_HALL", objRestaurantInfo.BUS_TYP_BNQT_HALL);
            objDataWrapper.AddParameter("@BUS_TYP_BREKFAST", objRestaurantInfo.BUS_TYP_BREKFAST);


            objDataWrapper.AddParameter("@BUS_TYP_FST_FOOD", objRestaurantInfo.BUS_TYP_FST_FOOD);
          
            objDataWrapper.AddParameter("@BUS_TYP_TAVERN", objRestaurantInfo.BUS_TYP_TAVERN);

            objDataWrapper.AddParameter("@BUS_TYP_OTHER", objRestaurantInfo.BUS_TYP_OTHER);
            objDataWrapper.AddParameter("@STAIRWAYS", objRestaurantInfo.STAIRWAYS);

            objDataWrapper.AddParameter("@ELEVATORS", objRestaurantInfo.ELEVATORS);

            objDataWrapper.AddParameter("@ESCALATORS", objRestaurantInfo.ESCALATORS);
            objDataWrapper.AddParameter("@GRILLING", objRestaurantInfo.GRILLING);

            objDataWrapper.AddParameter("@FRYING", objRestaurantInfo.FRYING);

            objDataWrapper.AddParameter("@BROILING", objRestaurantInfo.BROILING);
            objDataWrapper.AddParameter("@ROASTING", objRestaurantInfo.ROASTING);

            objDataWrapper.AddParameter("@COOKING", objRestaurantInfo.COOKING);

            objDataWrapper.AddParameter("@PRK_TYP_VALET", objRestaurantInfo.PRK_TYP_VALET);
            objDataWrapper.AddParameter("@PRK_TYP_PREMISES", objRestaurantInfo.PRK_TYP_PREMISES);

            objDataWrapper.AddParameter("@OPR_ON_PREMISES", objRestaurantInfo.OPR_ON_PREMISES);

            objDataWrapper.AddParameter("@OPR_OFF_PREMISES", objRestaurantInfo.OPR_OFF_PREMISES);
            objDataWrapper.AddParameter("@EMRG_LIGHTS", objRestaurantInfo.EMRG_LIGHTS);

            objDataWrapper.AddParameter("@WOOD_STOVE", objRestaurantInfo.WOOD_STOVE);
            objDataWrapper.AddParameter("@HIST_MARKER", objRestaurantInfo.HIST_MARKER);
            objDataWrapper.AddParameter("@EXTNG_SYS_COV_COOKNG", objRestaurantInfo.EXTNG_SYS_COV_COOKNG);

            objDataWrapper.AddParameter("@EXTNG_SYS_MNT_CNTRCT", objRestaurantInfo.EXTNG_SYS_MNT_CNTRCT);
            objDataWrapper.AddParameter("@GAS_OFF_COOKNG", objRestaurantInfo.GAS_OFF_COOKNG);
            objDataWrapper.AddParameter("@HOOD_FILTER_CLND", objRestaurantInfo.HOOD_FILTER_CLND);


            objDataWrapper.AddParameter("@HOOD_DUCTS_EQUIP", objRestaurantInfo.HOOD_DUCTS_EQUIP);
            objDataWrapper.AddParameter("@HOOD_DUCTS_MNT_SCH", objRestaurantInfo.HOOD_DUCTS_MNT_SCH);
            objDataWrapper.AddParameter("@BC_EXTNG_AVL", objRestaurantInfo.BC_EXTNG_AVL);

            objDataWrapper.AddParameter("@ADQT_CLEARANCE", objRestaurantInfo.ADQT_CLEARANCE);

            objDataWrapper.AddParameter("@BEER_SALES", objRestaurantInfo.BEER_SALES);
            objDataWrapper.AddParameter("@WINE_SALES", objRestaurantInfo.WINE_SALES);
            objDataWrapper.AddParameter("@FULL_BAR", objRestaurantInfo.FULL_BAR);

            objDataWrapper.AddParameter("@TOT_EXPNS_FOOD_LIQUOR", objRestaurantInfo.TOT_EXPNS_FOOD_LIQUOR);

            objDataWrapper.AddParameter("@TOT_EXPNS_OTHERS", objRestaurantInfo.TOT_EXPNS_OTHERS);
            objDataWrapper.AddParameter("@NET_PROFIT", objRestaurantInfo.NET_PROFIT);

            objDataWrapper.AddParameter("@ACCNT_PAYABLE", objRestaurantInfo.ACCNT_PAYABLE);

            objDataWrapper.AddParameter("@NOTES_PAYABLE", objRestaurantInfo.NOTES_PAYABLE);
            objDataWrapper.AddParameter("@BNK_LOANS_PAYABLE", objRestaurantInfo.BNK_LOANS_PAYABLE);



            SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@RESTAURANT_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objRestaurantInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objRestaurantInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objRestaurantInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objRestaurantInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objRestaurantInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_SUP_FORM_RESTAURANT", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_SUP_FORM_RESTAURANT");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                int RESTAURANT_ID;
                if (retParam.Value != null && retParam.Value.ToString() != "")
                {
                    RESTAURANT_ID = int.Parse(retParam.Value.ToString());
                    objRestaurantInfo.RESTAURANT_ID = RESTAURANT_ID;
                }
                else
                {
                    RESTAURANT_ID = -1;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return RESTAURANT_ID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }

        public int UpdateRestaurantInformation(ClsSuppFormRestaurantInfo objRestaurantInfo, string XmlFilePath)
        {

            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objRestaurantInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            //objDataWrapper.AddParameter("@CUSTOMER_ID", objRestaurantInfo.CUSTOMER_ID, SqlDbType.Int);
            //objDataWrapper.AddParameter("@POLICY_ID", objRestaurantInfo.POLICY_ID);
            //objDataWrapper.AddParameter("@POLICY_VERSION_ID", objRestaurantInfo.POLICY_VERSION_ID);
            ////objDataWrapper.AddParameter("@BUSINESS_ID", objNatureBusinessInfo.BUSINESS_ID);
            //objDataWrapper.AddParameter("@LOCATION_ID", objRestaurantInfo.LOCATION_ID);
            //objDataWrapper.AddParameter("@PREMISES_ID", objRestaurantInfo.PREMISES_ID);
            objDataWrapper.AddParameter("@RESTAURANT_ID", objRestaurantInfo.RESTAURANT_ID);

            objDataWrapper.AddParameter("@SEATINGCAPACITY", objRestaurantInfo.SEATINGCAPACITY);
            objDataWrapper.AddParameter("@BUS_TYP_RESTURANT", objRestaurantInfo.BUS_TYP_RESTURANT);
            objDataWrapper.AddParameter("@BUS_TYP_FM_STYLE", objRestaurantInfo.BUS_TYP_FM_STYLE);
            objDataWrapper.AddParameter("@BUS_TYP_NGHT_CLUB", objRestaurantInfo.BUS_TYP_NGHT_CLUB);
            objDataWrapper.AddParameter("@BUS_TYP_FRNCHSED", objRestaurantInfo.BUS_TYP_FRNCHSED);


            objDataWrapper.AddParameter("@BUS_TYP_NT_FRNCHSED", objRestaurantInfo.BUS_TYP_NT_FRNCHSED);
            objDataWrapper.AddParameter("@BUS_TYP_SEASONAL", objRestaurantInfo.BUS_TYP_SEASONAL);
            objDataWrapper.AddParameter("@BUS_TYP_YR_ROUND", objRestaurantInfo.BUS_TYP_YR_ROUND);
            objDataWrapper.AddParameter("@BUS_TYP_DINNER", objRestaurantInfo.BUS_TYP_DINNER);
            objDataWrapper.AddParameter("@BUS_TYP_BNQT_HALL", objRestaurantInfo.BUS_TYP_BNQT_HALL);
            objDataWrapper.AddParameter("@BUS_TYP_BREKFAST", objRestaurantInfo.BUS_TYP_BREKFAST);


            objDataWrapper.AddParameter("@BUS_TYP_FST_FOOD", objRestaurantInfo.BUS_TYP_FST_FOOD);

            objDataWrapper.AddParameter("@BUS_TYP_TAVERN", objRestaurantInfo.BUS_TYP_TAVERN);

            objDataWrapper.AddParameter("@BUS_TYP_OTHER", objRestaurantInfo.BUS_TYP_OTHER);
            objDataWrapper.AddParameter("@STAIRWAYS", objRestaurantInfo.STAIRWAYS);

            objDataWrapper.AddParameter("@ELEVATORS", objRestaurantInfo.ELEVATORS);

            objDataWrapper.AddParameter("@ESCALATORS", objRestaurantInfo.ESCALATORS);
            objDataWrapper.AddParameter("@GRILLING", objRestaurantInfo.GRILLING);

            objDataWrapper.AddParameter("@FRYING", objRestaurantInfo.FRYING);

            objDataWrapper.AddParameter("@BROILING", objRestaurantInfo.BROILING);
            objDataWrapper.AddParameter("@ROASTING", objRestaurantInfo.ROASTING);

            objDataWrapper.AddParameter("@COOKING", objRestaurantInfo.COOKING);

            objDataWrapper.AddParameter("@PRK_TYP_VALET", objRestaurantInfo.PRK_TYP_VALET);
            objDataWrapper.AddParameter("@PRK_TYP_PREMISES", objRestaurantInfo.PRK_TYP_PREMISES);

            objDataWrapper.AddParameter("@OPR_ON_PREMISES", objRestaurantInfo.OPR_ON_PREMISES);

            objDataWrapper.AddParameter("@OPR_OFF_PREMISES", objRestaurantInfo.OPR_OFF_PREMISES);
            objDataWrapper.AddParameter("@EMRG_LIGHTS", objRestaurantInfo.EMRG_LIGHTS);

            objDataWrapper.AddParameter("@WOOD_STOVE", objRestaurantInfo.WOOD_STOVE);
            objDataWrapper.AddParameter("@HIST_MARKER", objRestaurantInfo.HIST_MARKER);
            objDataWrapper.AddParameter("@EXTNG_SYS_COV_COOKNG", objRestaurantInfo.EXTNG_SYS_COV_COOKNG);

            objDataWrapper.AddParameter("@EXTNG_SYS_MNT_CNTRCT", objRestaurantInfo.EXTNG_SYS_MNT_CNTRCT);
            objDataWrapper.AddParameter("@GAS_OFF_COOKNG", objRestaurantInfo.GAS_OFF_COOKNG);
            objDataWrapper.AddParameter("@HOOD_FILTER_CLND", objRestaurantInfo.HOOD_FILTER_CLND);


            objDataWrapper.AddParameter("@HOOD_DUCTS_EQUIP", objRestaurantInfo.HOOD_DUCTS_EQUIP);
            objDataWrapper.AddParameter("@HOOD_DUCTS_MNT_SCH", objRestaurantInfo.HOOD_DUCTS_MNT_SCH);
            objDataWrapper.AddParameter("@BC_EXTNG_AVL", objRestaurantInfo.BC_EXTNG_AVL);

            objDataWrapper.AddParameter("@ADQT_CLEARANCE", objRestaurantInfo.ADQT_CLEARANCE);

            objDataWrapper.AddParameter("@BEER_SALES", objRestaurantInfo.BEER_SALES);
            objDataWrapper.AddParameter("@WINE_SALES", objRestaurantInfo.WINE_SALES);
            objDataWrapper.AddParameter("@FULL_BAR", objRestaurantInfo.FULL_BAR);

            objDataWrapper.AddParameter("@TOT_EXPNS_FOOD_LIQUOR", objRestaurantInfo.TOT_EXPNS_FOOD_LIQUOR);

            objDataWrapper.AddParameter("@TOT_EXPNS_OTHERS", objRestaurantInfo.TOT_EXPNS_OTHERS);
            objDataWrapper.AddParameter("@NET_PROFIT", objRestaurantInfo.NET_PROFIT);

            objDataWrapper.AddParameter("@ACCNT_PAYABLE", objRestaurantInfo.ACCNT_PAYABLE);

            objDataWrapper.AddParameter("@NOTES_PAYABLE", objRestaurantInfo.NOTES_PAYABLE);
            objDataWrapper.AddParameter("@BNK_LOANS_PAYABLE", objRestaurantInfo.BNK_LOANS_PAYABLE);



            //SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@SHOP_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objRestaurantInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objRestaurantInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objRestaurantInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objRestaurantInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objRestaurantInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Updated";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_SUP_FORM_RESTAURANT", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_SUP_FORM_RESTAURANT");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                //int RESTAURANT_ID;
                //if (retParam.Value != null && retParam.Value.ToString() != "")
                //{
                //    RESTAURANT_ID = int.Parse(retParam.Value.ToString());
                //    objRestaurantInfo.RESTAURANT_ID = RESTAURANT_ID;
                //}
                //else
                //{
                //    RESTAURANT_ID = -1;
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
        public int DeleteRestaurantInformation(ClsSuppFormRestaurantInfo objRestaurantInfo, string XmlFilePath)
        {
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            if (this.boolTransactionRequired)
            {
                objRestaurantInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }
            objDataWrapper.AddParameter("@RESTAURANT_ID", objRestaurantInfo.RESTAURANT_ID);
            int intResult = 0;
            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objRestaurantInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objRestaurantInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objRestaurantInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objRestaurantInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objRestaurantInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Deleted";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    intResult = objDataWrapper.ExecuteNonQuery("DELETE_POL_SUP_FORM_RESTAURANT", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    intResult = objDataWrapper.ExecuteNonQuery("DELETE_POL_SUP_FORM_RESTAURANT");

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

        //For Restaurant
        //public DataSet GetPremisesInformation(string CUST_ID, string POL_ID, string POL_VERSION_ID, string @LOCATION_ID,string @PREMISES_ID)
        public DataSet GetShopInformation(string CUST_ID, string POL_ID, string POL_VERSION_ID, string @LOCATION_ID, string @PREMISES_ID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CUSTOMER_ID", CUST_ID);
            sqlParams[1] = new SqlParameter("@POLICY_ID", POL_ID);
            sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID", POL_VERSION_ID);
            sqlParams[3] = new SqlParameter("@LOCATION_ID", LOCATION_ID);
            sqlParams[4] = new SqlParameter("@PREMISES_ID", PREMISES_ID);
            DataSet ds = null;
            try
            {
                ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "GET_POL_SUP_FORM_SHOP", sqlParams);
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

        public int AddShopInformation(ClsSuppFormShopInfo objShopInfoInfo, string XmlFilePath)
        {

            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objShopInfoInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            objDataWrapper.AddParameter("@CUSTOMER_ID", objShopInfoInfo.CUSTOMER_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", objShopInfoInfo.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objShopInfoInfo.POLICY_VERSION_ID);
            //objDataWrapper.AddParameter("@BUSINESS_ID", objNatureBusinessInfo.BUSINESS_ID);
            objDataWrapper.AddParameter("@LOCATION_ID", objShopInfoInfo.LOCATION_ID);
            objDataWrapper.AddParameter("@PREMISES_ID", objShopInfoInfo.PREMISES_ID);
            objDataWrapper.AddParameter("@UNITS", objShopInfoInfo.UNITS);
            objDataWrapper.AddParameter("@PERCENT_OCUP", objShopInfoInfo.PERCENT_OCUP);
            objDataWrapper.AddParameter("@RESTURANT_OCUP", objShopInfoInfo.RESTURANT_OCUP);
            objDataWrapper.AddParameter("@FLAME_COOKING", objShopInfoInfo.FLAME_COOKING);
            objDataWrapper.AddParameter("@NUM_FRYERS", objShopInfoInfo.NUM_FRYERS);


            objDataWrapper.AddParameter("@NUM_GRILLS", objShopInfoInfo.NUM_GRILLS);
            objDataWrapper.AddParameter("@DUCT_SYS", objShopInfoInfo.DUCT_SYS);
            objDataWrapper.AddParameter("@SUPPR_SYS", objShopInfoInfo.SUPPR_SYS);
            objDataWrapper.AddParameter("@DUCT_CLND_PST_SIX_MONTHS", objShopInfoInfo.DUCT_CLND_PST_SIX_MONTHS);
            objDataWrapper.AddParameter("@IS_INSURED", objShopInfoInfo.IS_INSURED);
            objDataWrapper.AddParameter("@TENANT_LIABILITY", objShopInfoInfo.TENANT_LIABILITY);


            objDataWrapper.AddParameter("@PERCENT_SALES", objShopInfoInfo.PERCENT_SALES);
            objDataWrapper.AddParameter("@SEPARATE_BAR", objShopInfoInfo.SEPARATE_BAR);
            
            objDataWrapper.AddParameter("@BBQ_PIT", objShopInfoInfo.BBQ_PIT);
            objDataWrapper.AddParameter("@BBQ_PIT_DIST", objShopInfoInfo.BBQ_PIT_DIST);
            objDataWrapper.AddParameter("@BLDG_TYPE_COOKNG", objShopInfoInfo.BLDG_TYPE_COOKNG);

            objDataWrapper.AddParameter("@IS_ENTERTNMT", objShopInfoInfo.IS_ENTERTNMT);



            SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@SHOP_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objShopInfoInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objShopInfoInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objShopInfoInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objShopInfoInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objShopInfoInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_SUP_FORM_SHOP", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_SUP_FORM_SHOP");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                int SHOP_ID;
                if (retParam.Value != null && retParam.Value.ToString() != "")
                {
                    SHOP_ID = int.Parse(retParam.Value.ToString());
                    objShopInfoInfo.SHOP_ID = SHOP_ID;
                }
                else
                {
                    SHOP_ID = -1;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return SHOP_ID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }

        public int UpdateShopInformation(ClsSuppFormShopInfo objShopInfoInfo, string XmlFilePath)
        {

            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objShopInfoInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            //objDataWrapper.AddParameter("@CUSTOMER_ID", objShopInfoInfo.CUSTOMER_ID, SqlDbType.Int);
            //objDataWrapper.AddParameter("@POLICY_ID", objShopInfoInfo.POLICY_ID);
            //objDataWrapper.AddParameter("@POLICY_VERSION_ID", objShopInfoInfo.POLICY_VERSION_ID);
            //objDataWrapper.AddParameter("@BUSINESS_ID", objNatureBusinessInfo.BUSINESS_ID);
            //objDataWrapper.AddParameter("@LOCATION_ID", objShopInfoInfo.LOCATION_ID);
            //objDataWrapper.AddParameter("@PREMISES_ID", objShopInfoInfo.PREMISES_ID);
            objDataWrapper.AddParameter("@SHOP_ID", objShopInfoInfo.SHOP_ID);

            objDataWrapper.AddParameter("@UNITS", objShopInfoInfo.UNITS);
            objDataWrapper.AddParameter("@PERCENT_OCUP", objShopInfoInfo.PERCENT_OCUP);
            objDataWrapper.AddParameter("@RESTURANT_OCUP", objShopInfoInfo.RESTURANT_OCUP);
            objDataWrapper.AddParameter("@FLAME_COOKING", objShopInfoInfo.FLAME_COOKING);
            objDataWrapper.AddParameter("@NUM_FRYERS", objShopInfoInfo.NUM_FRYERS);


            objDataWrapper.AddParameter("@NUM_GRILLS", objShopInfoInfo.NUM_GRILLS);
            objDataWrapper.AddParameter("@DUCT_SYS", objShopInfoInfo.DUCT_SYS);
            objDataWrapper.AddParameter("@SUPPR_SYS", objShopInfoInfo.SUPPR_SYS);
            objDataWrapper.AddParameter("@DUCT_CLND_PST_SIX_MONTHS", objShopInfoInfo.DUCT_CLND_PST_SIX_MONTHS);
            objDataWrapper.AddParameter("@IS_INSURED", objShopInfoInfo.IS_INSURED);
            objDataWrapper.AddParameter("@TENANT_LIABILITY", objShopInfoInfo.TENANT_LIABILITY);


            objDataWrapper.AddParameter("@PERCENT_SALES", objShopInfoInfo.PERCENT_SALES);
            objDataWrapper.AddParameter("@SEPARATE_BAR", objShopInfoInfo.SEPARATE_BAR);

            objDataWrapper.AddParameter("@BBQ_PIT", objShopInfoInfo.BBQ_PIT);
            objDataWrapper.AddParameter("@BBQ_PIT_DIST", objShopInfoInfo.BBQ_PIT_DIST);
            objDataWrapper.AddParameter("@BLDG_TYPE_COOKNG", objShopInfoInfo.BLDG_TYPE_COOKNG);

            objDataWrapper.AddParameter("@IS_ENTERTNMT", objShopInfoInfo.IS_ENTERTNMT);



            //SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@SHOP_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objShopInfoInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objShopInfoInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objShopInfoInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objShopInfoInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objShopInfoInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Updated";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_SUP_FORM_SHOP", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_SUP_FORM_SHOP");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                //int SHOP_ID;
                //if (retParam.Value != null && retParam.Value.ToString() != "")
                //{
                //    SHOP_ID = int.Parse(retParam.Value.ToString());
                //    objShopInfoInfo.SHOP_ID = SHOP_ID;
                //}
                //else
                //{
                //    SHOP_ID = -1;
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

        public int DeleteShopInformation(ClsSuppFormShopInfo objPremiseInfo, string XmlFilePath)
        {
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            if (this.boolTransactionRequired)
            {
                objPremiseInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }
            objDataWrapper.AddParameter("@SHOP_ID", objPremiseInfo.SHOP_ID);
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


                    intResult = objDataWrapper.ExecuteNonQuery("DELETE_POL_SUP_FORM_SHOP", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    intResult = objDataWrapper.ExecuteNonQuery("DELETE_POL_SUP_FORM_SHOP");

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
            
            SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@PREMLOC_ID", SqlDbType.Int, ParameterDirection.Output);

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

                int PREMLOC_ID;
                if (retParam.Value != null && retParam.Value.ToString() != "")
                {
                    PREMLOC_ID = int.Parse(retParam.Value.ToString());
                    objPremiseInfo.PREMLOC_ID = PREMLOC_ID;
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

            objDataWrapper.AddParameter("@PREMLOC_ID", objPremiseInfo.PREMLOC_ID);
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
            objDataWrapper.AddParameter("@PREMLOC_ID", objPremiseInfo.PREMLOC_ID);
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

                int PREMLOC_ID;
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
