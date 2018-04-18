using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model.Support;
using Cms.EbixDataTypes;
using System.Data;

namespace Cms.Model.Account
{
    [Serializable]
   public class ClsPolicyMeleventInfo : ClsModelBaseClass
    {
        public ClsPolicyMeleventInfo()
		{
            this.PropertyCollection();
		}
        
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("ROW_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("DATE", DATE);
            base.htPropertyCollection.Add("POLICY_NO", POLICY_NO);
           

        }
        public EbixInt32 ROW_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ROW_ID"]) == null ? new EbixInt32("ROW_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ROW_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ROW_ID"]).CurrentValue = Convert.ToInt32(value);
            }
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

        public EbixDateTime DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DATE"]) == null ? new EbixDateTime("DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }
        public EbixString POLICY_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_NO"]) == null ? new EbixString("POLICY_NO") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["POLICY_NO"]).CurrentValue = Convert.ToString(value);
            }
        }


        #region Methods
        public int AddPolicy()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "PROC_INSERT_MEL_EVENTOS_POLICY_EXCLUSION";
                base.ReturnIDName = "@RETURN_VALUE";
                this.RequiredTransactionLog = false;

                //For Transaction Log
                base.TRANS_TYPE_ID = 430;
                this.ROW_ID.IsDBParam = false;
                //base.RECORDED_BY = CREATED_BY.CurrentValue;
               
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;              
                this.DATE.IsTimePartRequired = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                returnResult = base.Save();
                // this.CRR_RATE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

                //if (returnResult > 0)
                //{
                //    this.CRR_RATE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                //}
                //else
                //{
                   
               // }
                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;

        }
        
        public int UpdatePolicy()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "PROC_UPDATE_MEL_EVENTOS_POLICY_EXCLUSION";

                base.RequiredTransactionLog = false;
                //base.ReturnIDName = "@RETURN_VALUE";             
                //For Transaction Log
               // base.TRANS_TYPE_ID = 296;
               // base.CLIENT_ID = 0;
               // base.RECORDED_BY = MODIFIED_BY.CurrentValue;
               // base.POLICYID = 0;
               // base.POLICYVERTRACKING_ID = 0;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                 this.IS_ACTIVE.IsDBParam = false;

                 this.MODIFIED_BY.IsDBParam = false;                
                 this.LAST_UPDATED_DATETIME.IsDBParam = false;
                 this.RequiredTransactionLog = false;
                returnValue = base.Update();
                //if (returnValue < 0)
                //{
                //    return base.ReturnIDNameValue;
                //    //  this.CRR_RATE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                //}
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

        public DataSet FetchData(int row_id)
        {
             DataSet dsCount = null;

             try
             {
                 base.Proc_FetchData = "PROC_GET_MEL_EVENTOS_POLICY_EXCLUSION";                                
                 base.htGetDataParamCollections.Clear();
                 base.htGetDataParamCollections.Add("@ROW_ID", row_id);
                 
                
                 dsCount = base.GetData();

             }//try
             catch (Exception ex)
             {
                 throw (ex);
             }//catch (Exception ex)
             return dsCount;
           
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <returns>Int Status</returns>
        public int DeletePolicy()
        {

            int returnValue = 0;
            try
            {
                base.Proc_Delete_Name = "PROC_DELETE_MEL_EVENTOS_POLICY_EXCLUSION";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@ROW_ID", ROW_ID.CurrentValue);
                base.RequiredTransactionLog = false;

                //For Transaction Log
                base.TRANS_TYPE_ID = 432;
                base.CLIENT_ID = 0;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = 0;
                base.POLICYVERTRACKING_ID = 0;
                //End 


                returnValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;



        }
        #endregion
    }
}
