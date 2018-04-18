/******************************************************************************************
<Author				: - Charles Gomes
<Created Date		: -	15/Jun/2010 
<Description		: - Model Class of Quick Application
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Cms.EbixDataLayer;
using Cms.Model.Support;
using Cms.EbixDataTypes;

namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsQuickAppInfo : ClsModelBaseClass
    {
        private static string RiskUrl = "";

        public static string RISK_URL
        {
            set
            {
                RiskUrl = GerRiskUrl(value);
            }
            get
            {
                return RiskUrl;
            }
        }

        private static string ScreenID = "";
        public static string SCREEN_ID
        {
            set
            {
                ScreenID = GetScreenID(value);
            }
            get
            {
                return ScreenID;
            }
        }


        public ClsQuickAppInfo()
        {
            this.PropertyCollection();
        }

        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("POLICY_LOB", POLICY_LOB);
            base.htPropertyCollection.Add("POLICY_SUBLOB", POLICY_SUBLOB);
            base.htPropertyCollection.Add("APP_TERMS", APP_TERMS);
            base.htPropertyCollection.Add("APP_EFFECTIVE_DATE", APP_EFFECTIVE_DATE);
            base.htPropertyCollection.Add("APP_EXPIRATION_DATE", APP_EXPIRATION_DATE);
            base.htPropertyCollection.Add("BILL_TYPE_ID", BILL_TYPE_ID);
            base.htPropertyCollection.Add("INSTALL_PLAN_ID", INSTALL_PLAN_ID);
            base.htPropertyCollection.Add("FLAG", FLAG);
            base.htPropertyCollection.Add("DIV_ID_DEPT_ID_PC_ID", DIV_ID_DEPT_ID_PC_ID);
            base.htPropertyCollection.Add("POLICY_CURRENCY", POLICY_CURRENCY);
        }

        public EbixInt32 CUSTOMER_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]) == null ? new EbixInt32("CUSTOMER_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 POLICY_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]) == null ? new EbixInt32("POLICY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 POLICY_VERSION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]) == null ? new EbixInt32("POLICY_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString POLICY_LOB
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_LOB"]) == null ? new EbixString("POLICY_LOB") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_LOB"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_LOB"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString FLAG
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FLAG"]) == null ? new EbixString("FLAG") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FLAG"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FLAG"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString POLICY_SUBLOB
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_SUBLOB"]) == null ? new EbixString("POLICY_SUBLOB") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_SUBLOB"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_SUBLOB"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString APP_TERMS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["APP_TERMS"]) == null ? new EbixString("APP_TERMS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["APP_TERMS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["APP_TERMS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString DIV_ID_DEPT_ID_PC_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DIV_ID_DEPT_ID_PC_ID"]) == null ? new EbixString("DIV_ID_DEPT_ID_PC_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DIV_ID_DEPT_ID_PC_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DIV_ID_DEPT_ID_PC_ID"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDateTime APP_EFFECTIVE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["APP_EFFECTIVE_DATE"]) == null ? new EbixDateTime("APP_EFFECTIVE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["APP_EFFECTIVE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["APP_EFFECTIVE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixDateTime APP_EXPIRATION_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["APP_EXPIRATION_DATE"]) == null ? new EbixDateTime("APP_EXPIRATION_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["APP_EXPIRATION_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["APP_EXPIRATION_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixInt32 INSTALL_PLAN_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSTALL_PLAN_ID"]) == null ? new EbixInt32("INSTALL_PLAN_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSTALL_PLAN_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSTALL_PLAN_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 BILL_TYPE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BILL_TYPE_ID"]) == null ? new EbixInt32("BILL_TYPE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BILL_TYPE_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BILL_TYPE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 POLICY_CURRENCY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_CURRENCY"]) == null ? new EbixInt32("POLICY_CURRENCY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_CURRENCY"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_CURRENCY"]).CurrentValue = Convert.ToInt32(value);
            }
        }


        private static string GerRiskUrl(string sLOB_ID)
        {
            string url = "";

            switch (int.Parse(sLOB_ID))
            {
                case 1:
                    url = @"";

                    break;
                case 2:
                    url = @"";
                    break;
                case 3:
                    url = @"";
                    break;
                case 4:
                    url = @"/cms/policies/aspx/watercraft/PolicyAddWatercraftInformation.aspx?PAGEFROM=QAPP&CalledFrom=""&BOATID=";
                    break;
                case 5:
                    url = @"";
                    break;
                case 6:
                    url = @"";
                    break;
                case 7:
                    url = @"";
                    break;
                case 8:
                    url = @"";
                    break;
                case 9:
                    url = @"/Cms/Policies/Aspx/NamedPerils/AddNamedPerils.aspx?CALLEDFROM=QAPP&PERIL_ID=";//All risks and named perils
                    break;
                case 10:
                    url = @"/Cms/Policies/Aspx/AddProductLocationInfo.aspx?PAGEFROM=QAPP&CalledFrom=CompCondo&PRODUCT_RISK_ID=";//Comprehensive Condominium 
                    break;
                case 11:
                    url = @"/Cms/Policies/Aspx/AddProductLocationInfo.aspx?PAGEFROM=QAPP&CalledFrom=CompComp&PRODUCT_RISK_ID=";//Comprehensive Company 
                    break;
                case 12:
                    url = @"/Cms/Policies/Aspx/AddPolicyDwellingInfo.aspx?PAGEFROM=QAPP&CalledFrom=GenCvlLib&PRODUCT_RISK_ID="; //General Civil Liability
                    break;
                case 13:
                    url = @"/Cms/Policies/Aspx/MariTime/AddMeriTimeInfo.aspx?CALLEDFROM=QAPP&MARITIME_ID=";//Maritime
                    break;
                case 14:
                    url = @"/Cms/Policies/Aspx/AddProductLocationInfo.aspx?PAGEFROM=QAPP&CalledFrom=RISK&PRODUCT_RISK_ID=";//Diversified Risk
                    break;
                case 15:
                    url = @"/Cms/Policies/Aspx/Accident/AddInvidualInfo.aspx?PAGEFROM=QAPP&CalledFrom=INDPA&PERSONAL_INFO_ID=";//Individual personal accident
                    break;
                case 16:
                    url = @"/Cms/Policies/Aspx/AddPolicyDwellingInfo.aspx?PAGEFROM=QAPP&CalledFrom=ROBBERY&PRODUCT_RISK_ID="; //Robbery product 
                    break;
                case 17:
                    url = @"/Cms/Policies/Aspx/Transportation/AddCivilTransportationVehicleInfo.aspx?PAGEFROM=QAPP&CalledFrom=FLVEHICLEINFO&VEHICLE_ID=";//Facultative Liability
                    break;
                case 18:
                    url = @"/Cms/Policies/Aspx/Transportation/AddCivilTransportationVehicleInfo.aspx?PAGEFROM=QAPP&CalledFrom=CLTVEHICLEINFO&VEHICLE_ID=";
                    break;
                case 19:
                    url = @"/Cms/Policies/Aspx/AddPolicyDwellingInfo.aspx?PAGEFROM=QAPP&CalledFrom=DWELLING&PRODUCT_RISK_ID="; //Dwelling product 
                    break;
                case 20:
                    url = @"/Cms/Policies/Aspx/Transportation/AddCommodityInfo.aspx?PAGEFROM=QAPP&CalledFrom=NCTRANS&COMMODITY_ID=";
                    break;
                case 21:
                    url = @"/Cms/Policies/Aspx/Accident/AddInvidualInfo.aspx?PAGEFROM=QAPP&CalledFrom=CPCACC&PERSONAL_INFO_ID=";
                    break;
                case 22:
                    url = @"/Cms/Policies/Aspx/Accident/AddPassengerAccidentInfo.aspx?PAGEFROM=QAPP&CalledFrom=PAPEACC&PERSONAL_ACCIDENT_ID=";//Personal Accident for Passengers
                    break;
                case 23:
                    url = @"/Cms/Policies/Aspx/Transportation/AddCommodityInfo.aspx?PAGEFROM=QAPP&CalledFrom=INTERNTRANS&COMMODITY_ID=";
                    break;
                default:
                    url = @"";
                    break;
            }

            return url;
        }

        private static string GetScreenID(string sLOB_ID)
        {
            string screen_ID = "";
            switch (int.Parse(sLOB_ID))
            {
                case 1:
                    screen_ID = "";
                    break;

                case 2:
                    screen_ID = "";
                    break;

                case 3:
                    screen_ID = "";
                    break;

                case 4:
                    screen_ID = "246_0";
                    break;

                case 5:
                    screen_ID = "";
                    break;

                case 6:
                    screen_ID = "";
                    break;

                case 7:
                    screen_ID = "";
                    break;

                case 8:
                    screen_ID = "";
                    break;

                case 9:
                    screen_ID = "453";
                    break;
                case 10:
                    screen_ID = "458";
                    break;
                case 11:
                    screen_ID = "458";
                    break;
                case 12:
                    screen_ID = "466";
                    break;
                case 13:
                    screen_ID = "455";
                    break;
                case 14:
                    screen_ID = "458";
                    break;
                case 15:
                    screen_ID = "497";
                    break;

                case 16:
                    screen_ID = "478";
                    break;

                case 17:
                    screen_ID = "486";
                    break;

                case 18:
                    screen_ID = "486";
                    break;

                case 19:
                    screen_ID = "491";
                    break;

                case 20:
                    screen_ID = "488";
                    break;

                case 21:
                    screen_ID = "497";
                    break;

                case 22:
                    screen_ID = "499";
                    break;

                case 23:
                    screen_ID = "495";
                    break;

            }
            return screen_ID;
        }

        public int QuickAppAddUpd()
        {
            int returnValue = 0;
            try
            {
                if (FLAG.CurrentValue == "I")
                {
                    base.Proc_Add_Name = "Proc_QuickAppInfo";
                    base.TRANS_TYPE_ID = 248;
                    base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                    base.RECORDED_BY = CREATED_BY.CurrentValue;

                    base.ReturnIDName = "@NEW_POLICY_ID";

                    CUSTOMER_ID.IsDBParam = true;
                    POLICY_ID.IsDBParam = true;
                    POLICY_VERSION_ID.IsDBParam = true;

                    IS_ACTIVE.IsDBParam = false;
                    CREATED_BY.IsDBParam = false;
                    CREATED_DATETIME.IsDBParam = false;
                    MODIFIED_BY.IsDBParam = false;
                    LAST_UPDATED_DATETIME.IsDBParam = false;

                    returnValue = base.Save();

                    returnValue = POLICY_ID.CurrentValue = ReturnIDNameValue;
                }
                else if (FLAG.CurrentValue == "U")
                {
                    base.Proc_Update_Name = "Proc_QuickAppInfo";
                    base.TRANS_TYPE_ID = 249;
                    base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                    base.RECORDED_BY = CREATED_BY.CurrentValue;
                    base.MODIFIED_BY.CurrentValue = CREATED_BY.CurrentValue;

                    base.ReturnIDName = "@NEW_POLICY_ID";

                    IS_ACTIVE.IsDBParam = false;
                    CREATED_BY.IsDBParam = false;
                    CREATED_DATETIME.IsDBParam = false;
                    MODIFIED_BY.IsDBParam = false;
                    LAST_UPDATED_DATETIME.IsDBParam = false;

                    returnValue = base.Update();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return returnValue;
        }

        public DataSet FetchData()
        {
            if (FLAG.CurrentValue == "F")
            {
                DataSet dstemp = null;

                try
                {
                    base.Proc_FetchData = "Proc_QuickAppInfo";
                    base.htGetDataParamCollections.Clear();
                    base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                    base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                    base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                    base.htGetDataParamCollections.Add("@FLAG", FLAG.CurrentValue);

                    base.ReturnIDName = "@NEW_POLICY_ID";

                    dstemp = base.GetData();

                    if (dstemp != null)
                    {
                        return dstemp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public string attachCustomer(int qCustomer_ID, int iCustomerID, int iPolID, int iPolVerID, string ConnStr)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            int retVal = 0;
            try
            {
                SqlParameter appID = (SqlParameter)objDataWrapper.AddParameter("@NEW_POLICY_ID", SqlDbType.Int, ParameterDirection.Output);
                SqlParameter polID = (SqlParameter)objDataWrapper.AddParameter("@NEW_APP_ID", SqlDbType.Int, ParameterDirection.Output);

                objDataWrapper.AddParameter("@CUSTOMER_ID", qCustomer_ID, SqlDbType.Int);
                objDataWrapper.AddParameter("@NEW_CUSTOMER_ID", iCustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", iPolID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", iPolVerID, SqlDbType.SmallInt);
                retVal = objDataWrapper.ExecuteNonQuery("Proc_AttachQuickAppCustomer");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return iCustomerID.ToString() + "~" + appID.Value.ToString() + "~" + polID.Value.ToString();
            }
            catch
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                return "";
            }
        }

        public int MakeApp(int iCustomerID, int iPolID, int iPolVerID, string sAPP_NUMBER, string ConnStr)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            int retVal = 0;
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", iCustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", iPolID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", iPolVerID, SqlDbType.SmallInt);
                objDataWrapper.AddParameter("@APP_NUMBER", sAPP_NUMBER, SqlDbType.NVarChar);
                retVal = objDataWrapper.ExecuteNonQuery("Proc_MakeAppFromQApp");
                if (retVal > 0)
                {
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    return 1;
                }
                else
                {
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                    return -1;
                }
            }
            catch(Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                return -2;
            }
        }

        public int MakeApp_MarineCargo(int iCustomerID, int iPolID, int iPolVerID, string sAPP_NUMBER, string ConnStr)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            int retVal = 0;
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", iCustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", iPolID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", iPolVerID, SqlDbType.SmallInt);
                objDataWrapper.AddParameter("@APP_NUMBER", sAPP_NUMBER, SqlDbType.NVarChar);
                retVal = objDataWrapper.ExecuteNonQuery("Proc_MakeAppFromQApp_MarineCargo");
                if (retVal > 0)
                {
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    return 1;
                }
                else
                {
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                return -2;
            }
        }

        public DataTable fillRisk(int iCustomerID, int iPolID, int iPolVerID, string ConnStr)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            DataSet ds = null;
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", iCustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", iPolID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", iPolVerID, SqlDbType.SmallInt);
                ds = objDataWrapper.ExecuteDataSet("Proc_FillQuickAppRiskInfo");

                if (ds != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
    }
}
