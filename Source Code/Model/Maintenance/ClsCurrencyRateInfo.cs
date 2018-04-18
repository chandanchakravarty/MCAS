using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model.Support;
using Cms.EbixDataTypes;
using System.Data;

namespace Cms.Model.Maintenance
{
    [Serializable]
    public class ClsCurrencyRateInfo : ClsModelBaseClass
    {
        public ClsCurrencyRateInfo()
        {
            this.PropertyCollection();
        }

        #region Delare the add parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>


        private void PropertyCollection() {
            base.htPropertyCollection.Add("CRR_RATE_ID", CRR_RATE_ID);
            base.htPropertyCollection.Add("CURRENCY_ID", CURRENCY_ID);
            base.htPropertyCollection.Add("RATE", RATE);
            base.htPropertyCollection.Add("RATE_EFFETIVE_FROM", RATE_EFFETIVE_FROM);
            base.htPropertyCollection.Add("RATE_EFFETIVE_TO", RATE_EFFETIVE_TO);        
        }
        #endregion

        #region Declare the Property for every data table columns
        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 CRR_RATE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CRR_RATE_ID"]) == null ? new EbixInt32("CRR_RATE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CRR_RATE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CRR_RATE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixInt32 CURRENCY_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CURRENCY_ID"]) == null ? new EbixInt32("CURRENCY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CURRENCY_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CURRENCY_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixDouble RATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RATE"]) == null ? new EbixDouble("RATE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RATE"]).CurrentValue = Convert.ToDouble(value);
            }
    
    }
        public EbixDateTime RATE_EFFETIVE_FROM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RATE_EFFETIVE_FROM"]) == null ? new EbixDateTime("RATE_EFFETIVE_FROM") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RATE_EFFETIVE_FROM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RATE_EFFETIVE_FROM"]).CurrentValue = Convert.ToDateTime(value);
            }
        }
        public EbixDateTime RATE_EFFETIVE_TO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RATE_EFFETIVE_TO"]) == null ? new EbixDateTime("RATE_EFFETIVE_TO") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RATE_EFFETIVE_TO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RATE_EFFETIVE_TO"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        #endregion

        #region Methods
        public int AddCurrencyRate()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_InsertCurrencyRate";
                base.ReturnIDName = "@CRR_RATE_ID";              
                //For Transaction Log
                base.TRANS_TYPE_ID = 295;         
                base.RECORDED_BY = CREATED_BY.CurrentValue;        
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;            
                this.CRR_RATE_ID.IsDBParam = false;
                this.RATE_EFFETIVE_FROM.IsTimePartRequired = false;               
                this.RATE_EFFETIVE_TO.IsTimePartRequired = false;
                this.CRR_RATE_ID.IsDBParam = false;
                returnResult = base.Save();
               // this.CRR_RATE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

                if (returnResult > 0)
                {
                    this.CRR_RATE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                }
                else
                {
                    return base.ReturnIDNameValue;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
            
        }
        public int UpdateCurrencyRate()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "Proc_UpdateCurrencyRate";
             //   base.ReturnIDName = "@CRR_RATE_ID";  
                //For Transaction Log
                base.TRANS_TYPE_ID = 296;
                base.CLIENT_ID = 0;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID = 0;             
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
              //  this.IS_ACTIVE.IsDBParam = false;             
                returnValue = base.Update();
                if (returnValue < 0)
                {
                    return base.ReturnIDNameValue;
                    //  this.CRR_RATE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                }
                //else
                //{
                //    return base.ReturnIDNameValue;
                //}

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }

        public DataSet FetchData(int CRR_RATE_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetCurrencyRate";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CRR_RATE_ID", CRR_RATE_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }

        public int ActivateDeactivateCurrencyRate()
        {
            int returnValue = 0;
            try
            {
                base.Proc_ActivateDeactivate_Name = "Proc_ActivateDeactivateCurrencyRate";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CRR_RATE_ID", CRR_RATE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);


                //For Transaction Log

                base.CLIENT_ID = 0;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID = 0;

                if (IS_ACTIVE.CurrentValue.ToString() == "Y")
                {
                    base.TRANS_TYPE_ID = 101;
                    //base.TRANS_DESC = "Activated";
                }
                else
                {
                    base.TRANS_TYPE_ID = 102;
                    //base.TRANS_DESC = "Deactivated";
                }
                base.ProcReturnValue = true;
                base.ActivateDeactivate();
                returnValue = Proc_ReturnValue;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return returnValue;

        }

        #endregion


    }
   
}
