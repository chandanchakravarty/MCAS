/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	16-Dec-2010
<End Date			: -	
<Purpose			: - Use for Penhor Rural product Model
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 

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
using System.Xml;
using Cms.EbixDataLayer;

namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsPenhorRuralInfo : ClsModelBaseClass, IDisposable
    {
        public ClsPenhorRuralInfo()
        {
            PropertyCollection();
        }
        public void Dispose()
        {
            System.GC.ReRegisterForFinalize(this);
        }
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);

            base.htPropertyCollection.Add("PENHOR_RURAL_ID", PENHOR_RURAL_ID);
            base.htPropertyCollection.Add("ITEM_NUMBER", ITEM_NUMBER);
            base.htPropertyCollection.Add("FESR_COVERAGE", FESR_COVERAGE);
            base.htPropertyCollection.Add("MODE", MODE);
            base.htPropertyCollection.Add("PROPERTY", PROPERTY);
            base.htPropertyCollection.Add("CULTIVATION", CULTIVATION);
            base.htPropertyCollection.Add("CITY", CITY);
            base.htPropertyCollection.Add("STATE_ID", STATE_ID);
            base.htPropertyCollection.Add("INSURED_AREA", INSURED_AREA);
            base.htPropertyCollection.Add("SUBSIDY_PREMIUM", SUBSIDY_PREMIUM);
            base.htPropertyCollection.Add("SUBSIDY_STATE", SUBSIDY_STATE);
            base.htPropertyCollection.Add("REMARKS", REMARKS);
            base.htPropertyCollection.Add("ORIGINAL_VERSION_ID", ORIGINAL_VERSION_ID);
            base.htPropertyCollection.Add("EXCEEDED_PREMIUM", EXCEEDED_PREMIUM);
    
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
        }//public EbixInt32 CUSTOMER_ID 
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
        }//public EbixInt32 POLICY_ID
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
        }//public EbixInt32 POLICY_VERSION_ID 
        public EbixInt32 ORIGINAL_VERSION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGINAL_VERSION_ID"]) == null ? new EbixInt32("ORIGINAL_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGINAL_VERSION_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGINAL_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 ORIGINAL_VERSION_ID 
        public EbixInt32 PENHOR_RURAL_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PENHOR_RURAL_ID"]) == null ? new EbixInt32("PENHOR_RURAL_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PENHOR_RURAL_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PENHOR_RURAL_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 PENHOR_RURAL_ID 
        public EbixInt32 ITEM_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ITEM_NUMBER"]) == null ? new EbixInt32("ITEM_NUMBER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ITEM_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ITEM_NUMBER"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 ITEM_NUMBER 
        public EbixInt32 FESR_COVERAGE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FESR_COVERAGE"]) == null ? new EbixInt32("FESR_COVERAGE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FESR_COVERAGE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FESR_COVERAGE"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 FESR_COVERAGE 
        public EbixInt32 MODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MODE"]) == null ? new EbixInt32("MODE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MODE"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 MODE 
        public EbixInt32 PROPERTY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROPERTY"]) == null ? new EbixInt32("PROPERTY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROPERTY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROPERTY"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 PROPERTY 
        public EbixInt32 CULTIVATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CULTIVATION"]) == null ? new EbixInt32("CULTIVATION") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CULTIVATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CULTIVATION"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 CULTIVATION 
        public EbixString CITY 
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY"]) == null ? new EbixString("CITY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY"]).CurrentValue = Convert.ToString(value);
            }
        }//  public EbixString CITY 
        public EbixInt32 STATE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STATE_ID"]) == null ? new EbixInt32("STATE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STATE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["STATE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 STATE_ID 
        public EbixInt32 INSURED_AREA
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSURED_AREA"]) == null ? new EbixInt32("INSURED_AREA") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSURED_AREA"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSURED_AREA"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 INSURED_AREA 
        public EbixDouble SUBSIDY_PREMIUM 
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["SUBSIDY_PREMIUM"]) == null ? new EbixDouble("SUBSIDY_PREMIUM") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["SUBSIDY_PREMIUM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["SUBSIDY_PREMIUM"]).CurrentValue = Convert.ToDouble(value);
            }
        }// public EbixDouble SUBSIDY_PREMIUM 
        public EbixInt32 SUBSIDY_STATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBSIDY_STATE"]) == null ? new EbixInt32("SUBSIDY_STATE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBSIDY_STATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUBSIDY_STATE"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 SUBSIDY_STATE 
        public EbixString REMARKS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REMARKS"]) == null ? new EbixString("REMARKS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REMARKS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REMARKS"]).CurrentValue = Convert.ToString(value);
            }
        }//  public EbixString REMARKS 


        public EbixInt32 EXCEEDED_PREMIUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXCEEDED_PREMIUM"]) == null ? new EbixInt32("EXCEEDED_PREMIUM") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXCEEDED_PREMIUM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXCEEDED_PREMIUM"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        /// <summary>
        /// Use to get the Penhor Rural info data
        /// </summary>
        /// <returns></returns>
        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetPenhorRuralInfoData";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@PENHOR_RURAL_ID", PENHOR_RURAL_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData()

        public int AddPenhorRuralData()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_InsertPenhorRuralInfo";
                base.ReturnIDName = "@PENHOR_RURAL_ID"; // //Set the out parameter

                //For Transaction Log

                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.PENHOR_RURAL_ID.IsDBParam = false;
                

                base.ProcReturnValue = true;//Set true for Return Value from Procedure  
                returnResult = base.Save();

                returnResult = Proc_ReturnValue;

                this.PENHOR_RURAL_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;

        }// public int AddPenhorRuralData()
        
             /// <summary>
        /// Use to Update Penhor Rural information 
        /// </summary>
        /// <returns></returns>
        public int UpdatePenhorRuralData()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "Proc_UpdatePenhorRuralInfo";
                //For Transaction Log
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //end 
                this.ORIGINAL_VERSION_ID.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;

                base.ProcReturnValue = true;//Set true for Return Value from Procedure  

                returnValue = base.Update();

                returnValue =base.Proc_ReturnValue; //Get the retun value from procedure 

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }//public int UpdatePenhorRuralData()
        /// <summary>
        ///  Use to Delete Civil Transport Vehicle information
        /// </summary>
        /// <returns></returns>
        public int DeletePenhorRuralData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Delete_Name = "Proc_DeletePenhorRuralInfo";
                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PENHOR_RURAL_ID", PENHOR_RURAL_ID.CurrentValue);


                //For Transaction Log

                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 

                returnValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;

        }//  public int DeletePenhorRuralData()
        /// <summary>
        ///  Use to Activate and Deactivate Penhor Rural information based on the some keys 
        /// </summary>
        /// <returns></returns>
        public int ActivateDeactivatePenhorRuralData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_ActivateDeactivate_Name = "Proc_ActivateDeactivatePenhorRuralInfo";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PENHOR_RURAL_ID", PENHOR_RURAL_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);


                //For Transaction Log

                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;



                returnValue = base.ActivateDeactivate();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;

        }//public int ActivateDeactivatePenhorRuralData()

    }
}
