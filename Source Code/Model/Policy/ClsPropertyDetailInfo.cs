/******************************************************************************************
<Author					: - Sneha
<Start Date				: -	22/11/2011
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
    public class ClsPropertyDetailInfo : ClsModelBaseClass
    {
        public ClsPropertyDetailInfo()
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
            base.htPropertyCollection.Add("PROPERTY_ID", PROPERTY_ID);
            base.htPropertyCollection.Add("PROP_DEDUCT", PROP_DEDUCT);
            base.htPropertyCollection.Add("PROP_WNDSTORM", PROP_WNDSTORM);
            base.htPropertyCollection.Add("OPT_CVG", OPT_CVG);
            base.htPropertyCollection.Add("BLD_LMT", BLD_LMT);
            base.htPropertyCollection.Add("BLD_PERCENT_COINS", BLD_PERCENT_COINS);
            base.htPropertyCollection.Add("BLD_VALU", BLD_VALU);
            base.htPropertyCollection.Add("BLD_INF", BLD_INF);
            base.htPropertyCollection.Add("BPP_LMT", BPP_LMT);
            base.htPropertyCollection.Add("BPP_PERCENT_COINS", BPP_PERCENT_COINS);
            base.htPropertyCollection.Add("BPP_VALU", BPP_VALU);
            base.htPropertyCollection.Add("BPP_STOCK", BPP_STOCK);
            base.htPropertyCollection.Add("YEAR_BUILT", YEAR_BUILT);
            base.htPropertyCollection.Add("CONST_TYPE", CONST_TYPE);
            base.htPropertyCollection.Add("NUM_STORIES", NUM_STORIES);
            base.htPropertyCollection.Add("PERCENT_SPRINKLERS", PERCENT_SPRINKLERS);
            base.htPropertyCollection.Add("BP_PRESENT", BP_PRESENT);
            base.htPropertyCollection.Add("BP_FNSHD", BP_FNSHD);
            base.htPropertyCollection.Add("BP_OPEN", BP_OPEN);
            base.htPropertyCollection.Add("BI_WIRNG_YR", BI_WIRNG_YR);
            base.htPropertyCollection.Add("BI_ROOFING_YR", BI_ROOFING_YR);
            base.htPropertyCollection.Add("BI_PLMG_YR", BI_PLMG_YR);
            base.htPropertyCollection.Add("BI_ROOF_TYP", BI_ROOF_TYP);
            base.htPropertyCollection.Add("BI_HEATNG_YR", BI_HEATNG_YR);
            base.htPropertyCollection.Add("BI_WIND_CLASS", BI_WIND_CLASS);
        }

        public EbixInt32 PROPERTY_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROPERTY_ID"]) == null ? new EbixInt32("PROPERTY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROPERTY_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROPERTY_ID"]).CurrentValue = Convert.ToInt32(value);
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

        public EbixString POLICY_VERSION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_VERSION_ID"]) == null ? new EbixString("POLICY_VERSION_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_VERSION_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_VERSION_ID"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString PROP_DEDUCT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROP_DEDUCT"]) == null ? new EbixString("PROP_DEDUCT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROP_DEDUCT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROP_DEDUCT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString PROP_WNDSTORM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROP_WNDSTORM"]) == null ? new EbixString("PROP_WNDSTORM") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROP_WNDSTORM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROP_WNDSTORM"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString OPT_CVG
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPT_CVG"]) == null ? new EbixString("OPT_CVG") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPT_CVG"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPT_CVG"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BLD_LMT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_LMT"]) == null ? new EbixString("BLD_LMT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_LMT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_LMT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BLD_PERCENT_COINS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_PERCENT_COINS"]) == null ? new EbixString("BLD_PERCENT_COINS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_PERCENT_COINS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_PERCENT_COINS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BLD_VALU
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_VALU"]) == null ? new EbixString("BLD_VALU") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_VALU"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_VALU"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BLD_INF
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_INF"]) == null ? new EbixString("BLD_INF") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_INF"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BLD_INF"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BPP_LMT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_LMT"]) == null ? new EbixString("BPP_LMT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_LMT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_LMT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BPP_PERCENT_COINS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_PERCENT_COINS"]) == null ? new EbixString("BPP_PERCENT_COINS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_PERCENT_COINS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_PERCENT_COINS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BPP_VALU
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_VALU"]) == null ? new EbixString("BPP_VALU") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_VALU"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_VALU"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BPP_STOCK
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_STOCK"]) == null ? new EbixString("BPP_STOCK") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_STOCK"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BPP_STOCK"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString YEAR_BUILT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["YEAR_BUILT"]) == null ? new EbixString("YEAR_BUILT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["YEAR_BUILT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["YEAR_BUILT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString CONST_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONST_TYPE"]) == null ? new EbixString("CONST_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONST_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONST_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString NUM_STORIES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NUM_STORIES"]) == null ? new EbixString("NUM_STORIES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NUM_STORIES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NUM_STORIES"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString PERCENT_SPRINKLERS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_SPRINKLERS"]) == null ? new EbixString("PERCENT_SPRINKLERS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_SPRINKLERS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_SPRINKLERS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BP_PRESENT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BP_PRESENT"]) == null ? new EbixString("BP_PRESENT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BP_PRESENT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BP_PRESENT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BP_FNSHD
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BP_FNSHD"]) == null ? new EbixString("BP_FNSHD") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BP_FNSHD"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BP_FNSHD"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BP_OPEN
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BP_OPEN"]) == null ? new EbixString("BP_OPEN") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BP_OPEN"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BP_OPEN"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BI_WIRNG_YR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_WIRNG_YR"]) == null ? new EbixString("BI_WIRNG_YR") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_WIRNG_YR"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_WIRNG_YR"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BI_ROOFING_YR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_ROOFING_YR"]) == null ? new EbixString("BI_ROOFING_YR") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_ROOFING_YR"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_ROOFING_YR"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BI_PLMG_YR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_PLMG_YR"]) == null ? new EbixString("BI_PLMG_YR") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_PLMG_YR"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_PLMG_YR"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BI_ROOF_TYP
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_ROOF_TYP"]) == null ? new EbixString("BI_ROOF_TYP") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_ROOF_TYP"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_ROOF_TYP"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BI_HEATNG_YR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_HEATNG_YR"]) == null ? new EbixString("BI_HEATNG_YR") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_HEATNG_YR"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_HEATNG_YR"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BI_WIND_CLASS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_WIND_CLASS"]) == null ? new EbixString("BI_WIND_CLASS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_WIND_CLASS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BI_WIND_CLASS"]).CurrentValue = Convert.ToString(value);
            }
        }


        public int AddPropDetailADD()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_Insert_POL_PREMISES_PROPERTY_INFO";
                base.ProcReturnValue = true;

                base.ReturnIDName = "@PROPERTY_ID";
                base.TRANS_DESC = "Property Detail Added";
                base.RECORDED_BY = CREATED_BY.CurrentValue;

                this.PROPERTY_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.ProcReturnValue = true;
                returnResult = Proc_ReturnValue;
                returnResult = base.Save();

                this.PROPERTY_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }

        public int updatePropDetail()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "Proc_UPDATE_POL_PREMISES_PROPERTY_INFO";
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

        public DataSet FetchData(Int32 PROPERTY_ID, string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID,string LOCATION_ID, string PREMISES_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GET_POL_PREMISES_PROPERTY_INFO";
                base.htGetDataParamCollections.Clear();

                this.IS_ACTIVE.IsDBParam = false;

                base.htGetDataParamCollections.Add("@PROPERTY_ID", PROPERTY_ID);
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

        public DataSet FetchId(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID,string PREMISES_ID, string LOCATION_ID)
        {
            DataSet dsCount = null;

            try
            {

                base.Proc_FetchData = "Proc_FETCH_POL_PREMISES_PROPERTY_INFO";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID);
                dsCount = base.GetData();
                this.PROPERTY_ID.CurrentValue = base.ReturnIDNameValue;

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }


        public int DelPropDetail()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Delete_Name = "DELETE_POL_PREMISES_PROPERTY_INFO";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PROPERTY_ID", PROPERTY_ID.CurrentValue);
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
