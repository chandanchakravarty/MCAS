/******************************************************************************************
<Author					: - Sneha
<Start Date				: -	23/11/2011
<End Date				: -	
<Description			: - 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Xml;
using Cms.EbixDataLayer;
namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsWarehouseDetailInfo : ClsModelBaseClass
    {
        public ClsWarehouseDetailInfo()
        {
            this.PropertyCollection();
        }

        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("LOCATION_ID", LOCATION_ID);
            base.htPropertyCollection.Add("PREMISES_ID", PREMISES_ID);
            base.htPropertyCollection.Add("WAREHOUSE_ID", WAREHOUSE_ID);
            base.htPropertyCollection.Add("BUILDINGS", BUILDINGS);
            base.htPropertyCollection.Add("OWN_MGMR", OWN_MGMR);
            base.htPropertyCollection.Add("RES_MGMR", RES_MGMR);
            base.htPropertyCollection.Add("DAYTIME_ATTNDT", DAYTIME_ATTNDT);
            base.htPropertyCollection.Add("ANY_BUSS_ACTY", ANY_BUSS_ACTY);
            base.htPropertyCollection.Add("VLT_STYLE", VLT_STYLE);
            base.htPropertyCollection.Add("TRUCK_RENTAL", TRUCK_RENTAL);
            base.htPropertyCollection.Add("MGMR_KYS_CUST_UNIT", MGMR_KYS_CUST_UNIT);
            base.htPropertyCollection.Add("NOTICE_SENT", NOTICE_SENT);
            base.htPropertyCollection.Add("SALES_TENANT_LST_TWELVE_MNTHS", SALES_TENANT_LST_TWELVE_MNTHS);
            base.htPropertyCollection.Add("ANY_COLD_STORAGE", ANY_COLD_STORAGE);
            base.htPropertyCollection.Add("MGMR_TYPE", MGMR_TYPE);
            base.htPropertyCollection.Add("STORAGEUNITS", STORAGEUNITS);
            base.htPropertyCollection.Add("IS_FENCED", IS_FENCED);
            base.htPropertyCollection.Add("IS_PRKNG_AVL", IS_PRKNG_AVL);
            base.htPropertyCollection.Add("IS_BOAT_PRKNG_AVL", IS_BOAT_PRKNG_AVL);
            base.htPropertyCollection.Add("ANY_FIREARMS", ANY_FIREARMS);
            base.htPropertyCollection.Add("TENANT_LCKS_CHK", TENANT_LCKS_CHK);
            base.htPropertyCollection.Add("ANY_BUSN_GUIDELINES", ANY_BUSN_GUIDELINES);
            base.htPropertyCollection.Add("NO_DYS_TENANT_PROP_SOLD", NO_DYS_TENANT_PROP_SOLD);
            base.htPropertyCollection.Add("DISP_PUBL", DISP_PUBL);
            base.htPropertyCollection.Add("ANY_CLIMATE_CNTL", ANY_CLIMATE_CNTL);
            base.htPropertyCollection.Add("GUARD_DOG", GUARD_DOG);

        }

        public EbixInt32 WAREHOUSE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["WAREHOUSE_ID"]) == null ? new EbixInt32("WAREHOUSE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["WAREHOUSE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["WAREHOUSE_ID"]).CurrentValue = Convert.ToInt32(value);
            }

        }

        public EbixInt32 PREMISES_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PREMISES_ID"]) == null ? new EbixInt32("PREMISES_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PREMISES_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PREMISES_ID"]).CurrentValue = Convert.ToInt32(value);
            }

        }


        public EbixInt32 LOCATION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION_ID"]) == null ? new EbixInt32("LOCATION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION_ID"]).CurrentValue = Convert.ToInt32(value);
            }

        }

        public EbixString CUSTOMER_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CUSTOMER_ID"]) == null ? new EbixString("CUSTOMER_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CUSTOMER_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CUSTOMER_ID"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString POLICY_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_ID"]) == null ? new EbixString("POLICY_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_ID"]).CurrentValue = Convert.ToString(value);
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

        public EbixInt32 BUILDINGS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BUILDINGS"]) == null ? new EbixInt32("BUILDINGS") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BUILDINGS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BUILDINGS"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString OWN_MGMR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OWN_MGMR"]) == null ? new EbixString("OWN_MGMR") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OWN_MGMR"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OWN_MGMR"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString RES_MGMR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RES_MGMR"]) == null ? new EbixString("RES_MGMR") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RES_MGMR"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RES_MGMR"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString DAYTIME_ATTNDT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DAYTIME_ATTNDT"]) == null ? new EbixString("DAYTIME_ATTNDT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DAYTIME_ATTNDT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DAYTIME_ATTNDT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString ANY_BUSS_ACTY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_BUSS_ACTY"]) == null ? new EbixString("ANY_BUSS_ACTY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_BUSS_ACTY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_BUSS_ACTY"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString VLT_STYLE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VLT_STYLE"]) == null ? new EbixString("VLT_STYLE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VLT_STYLE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VLT_STYLE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString TRUCK_RENTAL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TRUCK_RENTAL"]) == null ? new EbixString("TRUCK_RENTAL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TRUCK_RENTAL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TRUCK_RENTAL"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString MGMR_KYS_CUST_UNIT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MGMR_KYS_CUST_UNIT"]) == null ? new EbixString("MGMR_KYS_CUST_UNIT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MGMR_KYS_CUST_UNIT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MGMR_KYS_CUST_UNIT"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixString NOTICE_SENT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NOTICE_SENT"]) == null ? new EbixString("NOTICE_SENT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NOTICE_SENT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NOTICE_SENT"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixInt32 SALES_TENANT_LST_TWELVE_MNTHS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SALES_TENANT_LST_TWELVE_MNTHS"]) == null ? new EbixInt32("SALES_TENANT_LST_TWELVE_MNTHS") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SALES_TENANT_LST_TWELVE_MNTHS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SALES_TENANT_LST_TWELVE_MNTHS"]).CurrentValue = Convert.ToInt32(value);
            }

        }

        public EbixString ANY_COLD_STORAGE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_COLD_STORAGE"]) == null ? new EbixString("ANY_COLD_STORAGE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_COLD_STORAGE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_COLD_STORAGE"]).CurrentValue = Convert.ToString(value);
            }

        }
        
        public EbixString MGMR_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MGMR_TYPE"]) == null ? new EbixString("MGMR_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MGMR_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MGMR_TYPE"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixInt32 STORAGEUNITS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STORAGEUNITS"]) == null ? new EbixInt32("STORAGEUNITS") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STORAGEUNITS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STORAGEUNITS"]).CurrentValue = Convert.ToInt32(value);
            }

        }

        public EbixString IS_FENCED
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_FENCED"]) == null ? new EbixString("IS_FENCED") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_FENCED"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_FENCED"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixString IS_PRKNG_AVL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PRKNG_AVL"]) == null ? new EbixString("IS_PRKNG_AVL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PRKNG_AVL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PRKNG_AVL"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixString IS_BOAT_PRKNG_AVL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_BOAT_PRKNG_AVL"]) == null ? new EbixString("IS_BOAT_PRKNG_AVL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_BOAT_PRKNG_AVL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_BOAT_PRKNG_AVL"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixString ANY_FIREARMS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_FIREARMS"]) == null ? new EbixString("ANY_FIREARMS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_FIREARMS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_FIREARMS"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixString TENANT_LCKS_CHK
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TENANT_LCKS_CHK"]) == null ? new EbixString("TENANT_LCKS_CHK") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TENANT_LCKS_CHK"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TENANT_LCKS_CHK"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixString ANY_BUSN_GUIDELINES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_BUSN_GUIDELINES"]) == null ? new EbixString("ANY_BUSN_GUIDELINES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_BUSN_GUIDELINES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_BUSN_GUIDELINES"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixInt32 NO_DYS_TENANT_PROP_SOLD
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NO_DYS_TENANT_PROP_SOLD"]) == null ? new EbixInt32("NO_DYS_TENANT_PROP_SOLD") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NO_DYS_TENANT_PROP_SOLD"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NO_DYS_TENANT_PROP_SOLD"]).CurrentValue = Convert.ToInt32(value);
            }

        }

        public EbixString DISP_PUBL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISP_PUBL"]) == null ? new EbixString("DISP_PUBL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISP_PUBL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISP_PUBL"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixString ANY_CLIMATE_CNTL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_CLIMATE_CNTL"]) == null ? new EbixString("ANY_CLIMATE_CNTL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_CLIMATE_CNTL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_CLIMATE_CNTL"]).CurrentValue = Convert.ToString(value);
            }

        }

        public EbixString GUARD_DOG
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["GUARD_DOG"]) == null ? new EbixString("GUARD_DOG") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["GUARD_DOG"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["GUARD_DOG"]).CurrentValue = Convert.ToString(value);
            }

        }

        public int AddWareHouseDetlADD()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_Insert_POL_SUP_FORM_WAREHOUSE";
                base.ProcReturnValue = true;

                base.ReturnIDName = "@WAREHOUSE_ID";
                base.TRANS_DESC = "Property Detail Added";
                base.RECORDED_BY = CREATED_BY.CurrentValue;

                this.WAREHOUSE_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.ProcReturnValue = true;
                returnResult = Proc_ReturnValue;
                returnResult = base.Save();

                this.WAREHOUSE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }

        public int updateWareHouseDetl()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "Proc_UPDATE_POL_SUP_FORM_WAREHOUSE";
                base.TRANS_DESC = "Property Detail Updated";
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                returnValue = base.Update();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }

        public DataSet FetchData(Int32 WAREHOUSE_ID, string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string LOCATION_ID, string PREMISES_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GET_POL_SUP_FORM_WAREHOUSE";
                base.htGetDataParamCollections.Clear();

                this.IS_ACTIVE.IsDBParam = false;

                base.htGetDataParamCollections.Add("@WAREHOUSE_ID", WAREHOUSE_ID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }

        public DataSet FetchId(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string PREMISES_ID, string LOCATION_ID)
        {
            DataSet dsCount = null;

            try
            {

                base.Proc_FetchData = "Proc_FETCH_POL_SUP_FORM_WAREHOUSE";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID);
                dsCount = base.GetData();
                this.WAREHOUSE_ID.CurrentValue = base.ReturnIDNameValue;

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }


        public int DelWareHouseDetl()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Delete_Name = "DELETE_POL_SUP_FORM_WAREHOUSE";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@WAREHOUSE_ID", WAREHOUSE_ID.CurrentValue);
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.TRANS_DESC = "Property Detail Deleted";


                returnResult = base.Delete();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }

    }
}
