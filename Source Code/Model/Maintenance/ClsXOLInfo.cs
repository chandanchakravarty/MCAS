
/******************************************************************************************
<Author				: - Aditya Goel
<Start Date			: - 22 DEC, 2010
<End Date			: -	
<Description		: - Model Class for Monetary Information page functionality.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Cms.Model.Maintenance
{
   
    [Serializable]
    public class ClsXOLInfo : ClsModelBaseClass
    {

               /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsXOLInfo()
        {
            //this.SetColumnsName();
            this.PropertyCollection();
        }


     

        #region Delare the add the parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
                          

            base.htPropertyCollection.Add("XOL_ID", XOL_ID);
            base.htPropertyCollection.Add("LOB_ID", LOB_ID);
            base.htPropertyCollection.Add("RECOVERY_BASE", RECOVERY_BASE);
            base.htPropertyCollection.Add("LOSS_DEDUCTION", LOSS_DEDUCTION);
            base.htPropertyCollection.Add("AGGREGATE_LIMIT", AGGREGATE_LIMIT);
            base.htPropertyCollection.Add("MIN_DEPOSIT_PREMIUM", MIN_DEPOSIT_PREMIUM);
            base.htPropertyCollection.Add("FLAT_ADJ_RATE", FLAT_ADJ_RATE);
            base.htPropertyCollection.Add("REINSTATE_PREMIUM_RATE", REINSTATE_PREMIUM_RATE);
            base.htPropertyCollection.Add("REINSTATE_NUMBER", REINSTATE_NUMBER);
            base.htPropertyCollection.Add("PREMIUM_DISCOUNT", PREMIUM_DISCOUNT);
            base.htPropertyCollection.Add("CONTRACT_ID", CONTRACT_ID);
            base.htPropertyCollection.Add("MIN_CLAIM_LIMIT", MIN_CLAIM_LIMIT);
          
        }//private void PropertyCollection()s   
   
        #endregion

        #region Declare the Property for every data table columns

        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 XOL_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["XOL_ID"]) == null ? new EbixInt32("XOL_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["XOL_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["XOL_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 XOL_ID 

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
        }//public EbixInt32 LOB_ID 


        public EbixInt32 CONTRACT_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACT_ID"]) == null ? new EbixInt32("CONTRACT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACT_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 CONTRACT_ID 

        public EbixInt32 RECOVERY_BASE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RECOVERY_BASE"]) == null ? new EbixInt32("RECOVERY_BASE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RECOVERY_BASE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RECOVERY_BASE"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 RECOVERY_BASE 

        public EbixInt32 REINSTATE_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REINSTATE_NUMBER"]) == null ? new EbixInt32("REINSTATE_NUMBER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REINSTATE_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REINSTATE_NUMBER"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 REINSTATE_NUMBER 

        public EbixDouble LOSS_DEDUCTION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LOSS_DEDUCTION"]) == null ? new EbixDouble("LOSS_DEDUCTION") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LOSS_DEDUCTION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LOSS_DEDUCTION"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble LOSS_DEDUCTION

        public EbixDouble AGGREGATE_LIMIT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["AGGREGATE_LIMIT"]) == null ? new EbixDouble("AGGREGATE_LIMIT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["AGGREGATE_LIMIT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["AGGREGATE_LIMIT"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble AGGREGATE_LIMIT

        public EbixDouble MIN_DEPOSIT_PREMIUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MIN_DEPOSIT_PREMIUM"]) == null ? new EbixDouble("MIN_DEPOSIT_PREMIUM") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MIN_DEPOSIT_PREMIUM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MIN_DEPOSIT_PREMIUM"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble MIN_DEPOSIT_PREMIUM

        public EbixDouble FLAT_ADJ_RATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FLAT_ADJ_RATE"]) == null ? new EbixDouble("FLAT_ADJ_RATE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FLAT_ADJ_RATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FLAT_ADJ_RATE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble FLAT_ADJ_RATE

        public EbixDouble REINSTATE_PREMIUM_RATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REINSTATE_PREMIUM_RATE"]) == null ? new EbixDouble("REINSTATE_PREMIUM_RATE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REINSTATE_PREMIUM_RATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REINSTATE_PREMIUM_RATE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble REINSTATE_PREMIUM_RATE
        public EbixDouble PREMIUM_DISCOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PREMIUM_DISCOUNT"]) == null ? new EbixDouble("PREMIUM_DISCOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PREMIUM_DISCOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PREMIUM_DISCOUNT"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble PREMIUM_DISCOUNT

        public EbixInt32 MIN_CLAIM_LIMIT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MIN_CLAIM_LIMIT"]) == null ? new EbixInt32("MIN_CLAIM_LIMIT") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MIN_CLAIM_LIMIT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MIN_CLAIM_LIMIT"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 MIN_CLAIM_LIMIT 
                 

     


        #endregion
       


        public int Add()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertXOLInformation";

                base.ReturnIDName = "@XOL_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 387;
               
                //end 
                this.LOB_ID.IsDBParam = true;
                this.AGGREGATE_LIMIT.IsDBParam = true;
                this.FLAT_ADJ_RATE.IsDBParam = true;
                this.LOB_ID.IsDBParam = true;
                this.LOSS_DEDUCTION.IsDBParam = true;
                this.MIN_DEPOSIT_PREMIUM.IsDBParam = true;


                this.CONTRACT_ID.IsDBParam = true;

                this.PREMIUM_DISCOUNT.IsDBParam = true;
                this.RECOVERY_BASE.IsDBParam = true;
                this.REINSTATE_NUMBER.IsDBParam = true;
                this.REINSTATE_PREMIUM_RATE.IsDBParam = true;
                this.MIN_CLAIM_LIMIT.IsDBParam = true;


               this.IS_ACTIVE.IsDBParam = false;
               this.XOL_ID.IsDBParam = false;
               this.MODIFIED_BY.IsDBParam = false;
               this.LAST_UPDATED_DATETIME.IsDBParam = false;
               
                returnResult = base.Save();

                this.XOL_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }


        public int Update()
        {
            int returnValue = 0;
            try
            {

              
                base.Proc_Update_Name = "Proc_UpdateXOLInformation";
                

                //For Transaction Log
                base.TRANS_TYPE_ID = 323;
                
                //end 
                this.LOB_ID.IsDBParam = true;
                this.XOL_ID.IsDBParam = true;
                this.AGGREGATE_LIMIT.IsDBParam = true;
                this.FLAT_ADJ_RATE.IsDBParam = true;
                this.LOB_ID.IsDBParam = true;
                this.LOSS_DEDUCTION.IsDBParam = true;
                this.MIN_DEPOSIT_PREMIUM.IsDBParam = true;
                this.MIN_CLAIM_LIMIT.IsDBParam = true;

                this.PREMIUM_DISCOUNT.IsDBParam = true;
                this.RECOVERY_BASE.IsDBParam = true;
                this.REINSTATE_NUMBER.IsDBParam = true;
                this.REINSTATE_PREMIUM_RATE.IsDBParam = true;

                this.MODIFIED_BY.IsDBParam = true;
                this.LAST_UPDATED_DATETIME.IsDBParam = true;

                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false; 
                base.IS_ACTIVE.IsDBParam = false;
                this.CONTRACT_ID.IsDBParam = false;

                returnValue = base.Update();
                

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }


        public int ActivateDeactivate()
        {
            int returnValue = 0;
            try
            {


                base.Proc_Update_Name = "Proc_ActivateDeactivateXOLInformation";
                


                //For Transaction Log
                if( base.IS_ACTIVE.ToString()=="Y") // TO DEACTIVATE
                    base.TRANS_TYPE_ID = 391;
                else
                    base.TRANS_TYPE_ID = 390;      // TO ACTIVATE

                //end 

              
                this.AGGREGATE_LIMIT.IsDBParam = false;
                this.FLAT_ADJ_RATE.IsDBParam = false;
                this.LOB_ID.IsDBParam = false;
                this.LOSS_DEDUCTION.IsDBParam = false;
                this.MIN_DEPOSIT_PREMIUM.IsDBParam = false;
                this.MIN_CLAIM_LIMIT.IsDBParam = false;

                this.PREMIUM_DISCOUNT.IsDBParam = false;
                this.RECOVERY_BASE.IsDBParam = false;
                this.REINSTATE_NUMBER.IsDBParam = false;
                this.REINSTATE_PREMIUM_RATE.IsDBParam = false;
                

                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CONTRACT_ID.IsDBParam = false;

                returnValue = base.Update();
                

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }
        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetXOLInformation";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@XOL_ID", XOL_ID.CurrentValue);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)

    }
}

