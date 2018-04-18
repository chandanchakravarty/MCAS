using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model.Support;
using Cms.EbixDataTypes;
using System.Data;

namespace Cms.Model.Maintenance
{
    /// <summary>
    /// Database Model for DiscountSurchage.
    /// </summary>
    [Serializable] 
    public class ClsDiscountSurchargeInfo : ClsModelBaseClass
    {
        public ClsDiscountSurchargeInfo()
        {
            this.PropertyCollection();
        }

        #region Delare the add parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("DISCOUNT_ID", DISCOUNT_ID);
            base.htPropertyCollection.Add("TYPE_ID", TYPE_ID);
            base.htPropertyCollection.Add("LOB_ID", LOB_ID);
            base.htPropertyCollection.Add("SUBLOB_ID", SUBLOB_ID);
            base.htPropertyCollection.Add("DISCOUNT_DESCRIPTION", DISCOUNT_DESCRIPTION);
            base.htPropertyCollection.Add("PERCENTAGE", PERCENTAGE);
            //base.htPropertyCollection.Add("IS_ACTIVE", typeof(string));
            //base.htPropertyCollection.Add("CREATED_BY", typeof(int));
            //base.htPropertyCollection.Add("CREATED_DATETIME", typeof(DateTime));
            //base.htPropertyCollection.Add("MODIFIED_BY", typeof(int));
            //base.htPropertyCollection.Add("LAST_UPDATED_DATETIME", typeof(DateTime));
            base.htPropertyCollection.Add("EFFECTIVE_DATE", EFFECTIVE_DATE);
            base.htPropertyCollection.Add("FINAL_DATE", FINAL_DATE);
            base.htPropertyCollection.Add("LEVEL",LEVEL);
        }
        #endregion

        #region Declare the Property for every data table columns
        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 DISCOUNT_ID
        {
            get
            { 
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DISCOUNT_ID"]) == null ? new EbixInt32("DISCOUNT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DISCOUNT_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DISCOUNT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 TYPE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_ID"]) == null ? new EbixInt32("TYPE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 LOB_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_ID"]) == null ? new EbixInt32("LOB_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOB_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 SUBLOB_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBLOB_ID"]) == null ? new EbixInt32("SUBLOB_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBLOB_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBLOB_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString DISCOUNT_DESCRIPTION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISCOUNT_DESCRIPTION"]) == null ? new EbixString("DISCOUNT_DESCRIPTION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISCOUNT_DESCRIPTION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISCOUNT_DESCRIPTION"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDouble PERCENTAGE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PERCENTAGE"]) == null ? new EbixDouble("PERCENTAGE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PERCENTAGE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PERCENTAGE"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        //public EbixString IS_ACTIVE
        //{
        //    get
        //    {
        //        return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_ACTIVE"]) == null ? new EbixString("IS_ACTIVE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_ACTIVE"]);
        //    }
        //    set
        //    {
        //        ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_ACTIVE"]).CurrentValue = Convert.ToString(value);
        //    }
        //}

        //public EbixInt32 CREATED_BY
        //{
        //    get
        //    {
        //        return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CREATED_BY"]) == null ? new EbixInt32("CREATED_BY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CREATED_BY"]);
        //    }
        //    set
        //    {
        //        ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CREATED_BY"]).CurrentValue = Convert.ToInt32(value);
        //    }
        //}

        //public EbixDateTime CREATED_DATETIME
        //{
        //    get
        //    {
        //        return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["CREATED_DATETIME"]) == null ? new EbixDateTime("CREATED_DATETIME") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["CREATED_DATETIME"]);
        //    }
        //    set
        //    {
        //        ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["CREATED_DATETIME"]).CurrentValue = Convert.ToDateTime(value);
        //    }
        //}

        //public EbixInt32 MODIFIED_BY
        //{
        //    get
        //    {
        //        return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MODIFIED_BY"]) == null ? new EbixInt32("MODIFIED_BY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MODIFIED_BY"]);
        //    }
        //    set
        //    {
        //        ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MODIFIED_BY"]).CurrentValue = Convert.ToInt32(value);
        //    }
        //}

        //public EbixDateTime LAST_UPDATED_DATETIME
        //{
        //    get
        //    {
        //        return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["LAST_UPDATED_DATETIME"]) == null ? new EbixDateTime("LAST_UPDATED_DATETIME") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["LAST_UPDATED_DATETIME"]);
        //    }
        //    set
        //    {
        //        ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["LAST_UPDATED_DATETIME"]).CurrentValue = Convert.ToDateTime(value);
        //    }
        //}

        public EbixDateTime EFFECTIVE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_DATE"]) == null ? new EbixDateTime("EFFECTIVE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixDateTime FINAL_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["FINAL_DATE"]) == null ? new EbixDateTime("FINAL_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["FINAL_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["FINAL_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixInt32 LEVEL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LEVEL"]) == null ? new EbixInt32("LEVEL") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LEVEL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LEVEL"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        #endregion

        #region Methods
        public int ADDDiscountSurchargeData()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertDiscountSurcharge";

                base.ReturnIDName = "@DISCOUNT_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 98;
                base.CLIENT_ID = 0;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID =0;
       
                //base.TRANS_DESC = "Discount Surcharge added";
                //end 

                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.DISCOUNT_ID.IsDBParam = false;
                this.EFFECTIVE_DATE.IsTimePartRequired = false;
                
                this.FINAL_DATE.IsTimePartRequired = false;
                returnResult = base.Save();

                this.DISCOUNT_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }

        public DataSet FetchData(int DISCOUNT_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetDiscountSurchargeData";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@DISCOUNT_ID", DISCOUNT_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }

        /// <summary>
        /// Delete DiscountSurcharge Data 
        /// </summary>
        /// <returns></returns>
        public int DeleteDiscountSurchargeData()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Delete_Name = "Proc_DeleteDiscountSurchargeData";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@DISCOUNT_ID", DISCOUNT_ID.CurrentValue);
                
                //For Transaction Log
                base.TRANS_TYPE_ID = 100;
                base.CLIENT_ID = 0;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID = 0;
                //base.TRANS_DESC = "Discount Surcharge Deleted";
                //end 
                base.ProcReturnValue = true;
                base.Delete();
                returnValue = Proc_ReturnValue;
                    

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;

        }

        /// <summary>
        /// Deactivate DiscountSurcharge
        /// </summary>
        /// <returns></returns>
        public int ActivateDeactivateDiscountSurcharge()
        {
            int returnValue = 0;
            try
            {
                base.Proc_ActivateDeactivate_Name = "[Proc_ActivateDeactivateDiscountSurcharge]";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@DISCOUNT_ID", DISCOUNT_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);


                //For Transaction Log
               
                base.CLIENT_ID = 0;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID =0;

                if (IS_ACTIVE.CurrentValue.ToString() == "Y")
                {
                    base.TRANS_TYPE_ID = 101;
                    //base.TRANS_DESC = "MariTime Activated";
                }
                else
                {
                    base.TRANS_TYPE_ID = 102;
                    //base.TRANS_DESC = "Discount Surcharge Deactivated";
                }
                base.ProcReturnValue=true;
                base.ActivateDeactivate();
                returnValue = Proc_ReturnValue;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return returnValue;

        }

        /// <summary>
        /// Update DiscountSurcharge
        /// </summary>
        /// <returns></returns>
        public int UpdateDiscountSurcharge()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateDiscountSurcharge";

                //For Transaction Log
                base.TRANS_TYPE_ID = 99;
                base.CLIENT_ID = 0;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID = 0;
                
                //base.TRANS_DESC = "Discount Surcharge Updated";
                //end 

                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
               

                //this.CLIENT_ID = this.CREATED_BY.CurrentValue;
                returnValue = base.Update();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }
        #endregion
    }
}
