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
    public class ClsBillingInformationInfo : ClsModelBaseClass
    {
        public ClsBillingInformationInfo()
        {
            this.PropertyCollection();
        }
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("BILLING_ID", BILLING_ID);
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("LOB_ID", LOB_ID);
            base.htPropertyCollection.Add("BILLING_TYPE", BILLING_TYPE);
            base.htPropertyCollection.Add("BILLING_PLAN", BILLING_PLAN);
            base.htPropertyCollection.Add("DOWN_PAYMENT_MODE", DOWN_PAYMENT_MODE);
            base.htPropertyCollection.Add("PROXY_SIGN_OBTAIN", PROXY_SIGN_OBTAIN);
            base.htPropertyCollection.Add("UNDERWRITER", UNDERWRITER);
            base.htPropertyCollection.Add("ROLLOVER", ROLLOVER);
            base.htPropertyCollection.Add("RECIVED_PREMIUM", RECIVED_PREMIUM);
            base.htPropertyCollection.Add("COMP_APP_BONUS_APPLIES", COMP_APP_BONUS_APPLIES);
            base.htPropertyCollection.Add("CURRENT_RESIDENCE", CURRENT_RESIDENCE);
        }

        public EbixInt32 BILLING_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BILLING_ID"]) == null ? new EbixInt32("BILLING_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BILLING_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["BILLING_ID"]).CurrentValue = Convert.ToInt32(value);
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

        public EbixString LOB_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_ID"]) == null ? new EbixString("LOB_ID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOB_ID"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BILLING_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BILLING_TYPE"]) == null ? new EbixString("BILLING_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BILLING_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BILLING_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString BILLING_PLAN
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BILLING_PLAN"]) == null ? new EbixString("BILLING_PLAN") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BILLING_PLAN"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BILLING_PLAN"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString DOWN_PAYMENT_MODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DOWN_PAYMENT_MODE"]) == null ? new EbixString("DOWN_PAYMENT_MODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DOWN_PAYMENT_MODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DOWN_PAYMENT_MODE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString PROXY_SIGN_OBTAIN
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROXY_SIGN_OBTAIN"]) == null ? new EbixString("PROXY_SIGN_OBTAIN") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROXY_SIGN_OBTAIN"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROXY_SIGN_OBTAIN"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString UNDERWRITER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["UNDERWRITER"]) == null ? new EbixString("UNDERWRITER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["UNDERWRITER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["UNDERWRITER"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString ROLLOVER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ROLLOVER"]) == null ? new EbixString("ROLLOVER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ROLLOVER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ROLLOVER"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString RECIVED_PREMIUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECIVED_PREMIUM"]) == null ? new EbixString("RECIVED_PREMIUM") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECIVED_PREMIUM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECIVED_PREMIUM"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString COMP_APP_BONUS_APPLIES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COMP_APP_BONUS_APPLIES"]) == null ? new EbixString("COMP_APP_BONUS_APPLIES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COMP_APP_BONUS_APPLIES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COMP_APP_BONUS_APPLIES"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString CURRENT_RESIDENCE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CURRENT_RESIDENCE"]) == null ? new EbixString("CURRENT_RESIDENCE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CURRENT_RESIDENCE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CURRENT_RESIDENCE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public int AddPolBillinInfo()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_Insert_POL_BILLING_INFO";
                base.ProcReturnValue = true;

                base.ReturnIDName = "@BILLING_ID";
                base.TRANS_DESC = "Billing Info Added";
                base.RECORDED_BY = CREATED_BY.CurrentValue;

                this.BILLING_ID.IsDBParam=false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
              
                this.ProcReturnValue = true;
                returnResult = Proc_ReturnValue;
                returnResult = base.Save();

                this.BILLING_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }

        public int updatePolBillingInfo()
        {
            int returnValue = 0;
            try
            {
               base.Proc_Update_Name = "Proc_UPDATE_POL_BILLING_INFO";
               base.TRANS_DESC = "Billing Info Updated";
               base.RECORDED_BY = MODIFIED_BY.CurrentValue;

                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                returnValue = base.Update();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }

        public DataSet FetchData(Int32 BILLING_ID, string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string LOB_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GET_POL_BILLING_INFO";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@BILLING_ID", BILLING_ID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@LOB_ID", LOB_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }

        public DataSet FetchId(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string LOB_ID)
        {
            DataSet dsCount = null;

            try
            {
               
                base.Proc_FetchData = "Proc_FETCH_POL_BILLING_INFO";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@LOB_ID", LOB_ID);
                dsCount = base.GetData();
                this.BILLING_ID.CurrentValue = base.ReturnIDNameValue; 

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }

        public DataSet GET_BILL_PLAN()
        {
            DataSet dsCount = null;

            try
            {

                base.Proc_FetchData = "Proc_BILLING_PLAN_POL_BILLING_INFO";
                base.htGetDataParamCollections.Clear();
                dsCount = base.GetData();
                this.BILLING_ID.CurrentValue = base.ReturnIDNameValue;

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }

        public int DelPolBillinInfo()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Delete_Name = "DELETE_POL_BILLING_INFO";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LOB_ID", LOB_ID.CurrentValue);

                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.TRANS_DESC = "Billing Info Deleted";

               
                returnResult = base.Delete();
                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }

        //public int ActivateDeactivatePolBillingInfo()
        //{
        //    int returnValue = 0;
        //    try
        //    {

        //        base.Proc_ActivateDeactivate_Name = "Proc_ACTIVE_DEACTIVE_POL_BILLING_INFO";

        //        base.htGetDataParamCollections.Clear();
        //        base.htGetDataParamCollections.Add("@BILLING_ID", BILLING_ID.CurrentValue);
        //        base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);
        //        returnValue = base.ActivateDeactivate();

        //    }//try
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }//catch (Ex
        //    return returnValue;

        //}
    }
}
