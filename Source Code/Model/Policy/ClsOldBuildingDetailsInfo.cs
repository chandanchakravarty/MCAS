/******************************************************************************************
<Author					: - Amit Kr Mishra 
<Start Date				: -	26th November,2011
<End Date				: -	
<Description			: - Its maintain Old Building information for BOP
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
    public class ClsOldBuildingDetailsInfo : ClsModelBaseClass
    {
        public ClsOldBuildingDetailsInfo()
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
            base.htPropertyCollection.Add("OLDBLD_ID", OLDBLD_ID);
            base.htPropertyCollection.Add("WHN_WIRING_UPDT", WHN_WIRING_UPDT);
            base.htPropertyCollection.Add("WIRING_IN_CNDCT", WIRING_IN_CNDCT);
            base.htPropertyCollection.Add("FUSES_RPLCD", FUSES_RPLCD);
            base.htPropertyCollection.Add("ALM_WIRING", ALM_WIRING);
            base.htPropertyCollection.Add("WHN_PLBMG_MODRS", WHN_PLBMG_MODRS);
            base.htPropertyCollection.Add("TYP_WTR_PIPS", TYP_WTR_PIPS);
            base.htPropertyCollection.Add("WHN_HEATNG_MODRS", WHN_HEATNG_MODRS);
            base.htPropertyCollection.Add("TYP_SYS", TYP_SYS);
            base.htPropertyCollection.Add("TYP_FUEL", TYP_FUEL);
            base.htPropertyCollection.Add("WHN_ROOF_REPRD", WHN_ROOF_REPRD);
            base.htPropertyCollection.Add("WHN_ROOF_REPLCD", WHN_ROOF_REPLCD);
            base.htPropertyCollection.Add("ROOF_MTRL", ROOF_MTRL);
            base.htPropertyCollection.Add("SPF", SPF);
            base.htPropertyCollection.Add("ANY_ABSTS", ANY_ABSTS);
            base.htPropertyCollection.Add("ANY_FRB_ABSTS", ANY_FRB_ABSTS);
            base.htPropertyCollection.Add("ABSTS_ABT_DNE", ABSTS_ABT_DNE);
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

        public EbixInt32 OLDBLD_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["OLDBLD_ID"]) == null ? new EbixInt32("OLDBLD_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["OLDBLD_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["OLDBLD_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString WHN_WIRING_UPDT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_WIRING_UPDT"]) == null ? new EbixString("WHN_WIRING_UPDT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_WIRING_UPDT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_WIRING_UPDT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString WIRING_IN_CNDCT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WIRING_IN_CNDCT"]) == null ? new EbixString("WIRING_IN_CNDCT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WIRING_IN_CNDCT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WIRING_IN_CNDCT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString FUSES_RPLCD
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FUSES_RPLCD"]) == null ? new EbixString("FUSES_RPLCD") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FUSES_RPLCD"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FUSES_RPLCD"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString ALM_WIRING
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ALM_WIRING"]) == null ? new EbixString("ALM_WIRING") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ALM_WIRING"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ALM_WIRING"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString WHN_PLBMG_MODRS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_PLBMG_MODRS"]) == null ? new EbixString("WHN_PLBMG_MODRS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_PLBMG_MODRS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_PLBMG_MODRS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString TYP_WTR_PIPS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_WTR_PIPS"]) == null ? new EbixString("TYP_WTR_PIPS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_WTR_PIPS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_WTR_PIPS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString WHN_HEATNG_MODRS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_HEATNG_MODRS"]) == null ? new EbixString("WHN_HEATNG_MODRS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_HEATNG_MODRS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_HEATNG_MODRS"]).CurrentValue = Convert.ToString(value);
            }
        }
        
        public EbixString TYP_SYS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_SYS"]) == null ? new EbixString("TYP_SYS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_SYS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_SYS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString TYP_FUEL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_FUEL"]) == null ? new EbixString("TYP_FUEL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_FUEL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_FUEL"]).CurrentValue = Convert.ToString(value);
            }
        }  
      
        public EbixString WHN_ROOF_REPRD
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_ROOF_REPRD"]) == null ? new EbixString("WHN_ROOF_REPRD") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_ROOF_REPRD"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_ROOF_REPRD"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString WHN_ROOF_REPLCD
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_ROOF_REPLCD"]) == null ? new EbixString("WHN_ROOF_REPLCD") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_ROOF_REPLCD"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["WHN_ROOF_REPLCD"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString ROOF_MTRL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ROOF_MTRL"]) == null ? new EbixString("ROOF_MTRL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ROOF_MTRL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ROOF_MTRL"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString SPF
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SPF"]) == null ? new EbixString("SPF") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SPF"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SPF"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString ANY_ABSTS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_ABSTS"]) == null ? new EbixString("ANY_ABSTS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_ABSTS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_ABSTS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString ANY_FRB_ABSTS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_FRB_ABSTS"]) == null ? new EbixString("ANY_FRB_ABSTS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_FRB_ABSTS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_FRB_ABSTS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString ABSTS_ABT_DNE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ABSTS_ABT_DNE"]) == null ? new EbixString("ABSTS_ABT_DNE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ABSTS_ABT_DNE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ABSTS_ABT_DNE"]).CurrentValue = Convert.ToString(value);
            }
        }

       
        public int AddOldBuildingDetailADD()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "PROC_INSERT_POL_SUP_FORM_OLD_BLD";
                base.ProcReturnValue = true;

                base.ReturnIDName = "@OLDBLD_ID";
                base.TRANS_DESC = "Building Detail Added";
                base.RECORDED_BY = CREATED_BY.CurrentValue;

                this.OLDBLD_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.ProcReturnValue = true;
                returnResult = Proc_ReturnValue;
                returnResult = base.Save();

                this.OLDBLD_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }


        public int updateOldBuildingDetail()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "PROC_UPDATE_POL_SUP_FORM_OLD_BLD";
                base.TRANS_DESC = "Old Building Detail Updated";
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

        public DataSet FetchData(Int32 OLDBLD_ID, Int32 CUSTOMER_ID, Int32 POLICY_ID, Int32 POLICY_VERSION_ID, Int32 LOCATION_ID, Int32 PREMISES_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "PROC_GET_POL_SUP_FORM_OLD_BLD";
                base.htGetDataParamCollections.Clear();

                this.IS_ACTIVE.IsDBParam = false;

                base.htGetDataParamCollections.Add("@OLDBLD_ID", OLDBLD_ID);
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

                base.Proc_FetchData = "PROC_FETCH_POL_SUP_FORM_OLD_BLD";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID);
                dsCount = base.GetData();
                this.OLDBLD_ID.CurrentValue = base.ReturnIDNameValue;

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }

        public int DelOldBuildingDetail()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Delete_Name = "DELETE_POL_SUP_FORM_OLD_BLD";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@OLDBLD_ID", OLDBLD_ID.CurrentValue);
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.TRANS_DESC = "Constractor Detail Deleted";


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
