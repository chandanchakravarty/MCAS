
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	05-28-2010
<End Date			: -	
<Description		: - 
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
using Cms.Model.Support;
using Cms.EbixDataTypes;
using System.Data;
using System.Collections;
using Cms.EbixDataLayer;

namespace Cms.Model.Policy.Accident
{
    [Serializable]
    public class ClsPassengerAccidentInfo : ClsModelBaseClass
    {
        public ClsPassengerAccidentInfo()
        { this.PropertyCollection(); }
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("PERSONAL_ACCIDENT_ID", PERSONAL_ACCIDENT_ID);
            base.htPropertyCollection.Add("START_DATE", START_DATE);
            base.htPropertyCollection.Add("END_DATE", END_DATE);
            base.htPropertyCollection.Add("NUMBER_OF_PASSENGERS", NUMBER_OF_PASSENGERS);
            base.htPropertyCollection.Add("CO_APPLICANT_ID", CO_APPLICANT_ID);
            base.htPropertyCollection.Add("ORIGINAL_VERSION_ID", ORIGINAL_VERSION_ID);
            base.htPropertyCollection.Add("RISK_ORIGINAL_ENDORSEMENT_NO", RISK_ORIGINAL_ENDORSEMENT_NO);
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
        public EbixInt32 PERSONAL_ACCIDENT_ID
        {
            get
            {  
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERSONAL_ACCIDENT_ID"]) == null ? new EbixInt32("PERSONAL_ACCIDENT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERSONAL_ACCIDENT_ID"]);
            }
            set
            {
                
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERSONAL_ACCIDENT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        } //public EbixInt32 PERSONAL_ACCIDENT_ID 


        public EbixDateTime START_DATE
        {
            get { return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["START_DATE"]) == null ? new EbixDateTime("START_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["START_DATE"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["START_DATE"]).CurrentValue = Convert.ToDateTime(value);

            }
        }//public EbixDateTime START_DATE

        public EbixDateTime END_DATE
        {
            get { return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["END_DATE"]) == null ? new EbixDateTime("END_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["END_DATE"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["END_DATE"]).CurrentValue = Convert.ToDateTime(value);

            }
        }//public EbixDateTime END_DATE

        public EbixDouble NUMBER_OF_PASSENGERS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["NUMBER_OF_PASSENGERS"]) == null ? new EbixDouble("NUMBER_OF_PASSENGERS") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["NUMBER_OF_PASSENGERS"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["NUMBER_OF_PASSENGERS"]).CurrentValue = Convert.ToDouble(value);
            }
        } //public EbixDouble NUMBER_OF_PASSENGERS

        public EbixInt32 CO_APPLICANT_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CO_APPLICANT_ID"]) == null ? new EbixInt32("CO_APPLICANT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CO_APPLICANT_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CO_APPLICANT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }


        public EbixInt32 RISK_ORIGINAL_ENDORSEMENT_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ORIGINAL_ENDORSEMENT_NO"]) == null ? new EbixInt32("RISK_ORIGINAL_ENDORSEMENT_NO") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ORIGINAL_ENDORSEMENT_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ORIGINAL_ENDORSEMENT_NO"]).CurrentValue = Convert.ToInt32(value);
            }
        }

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
        
        public DataSet FetchApplicants(int CUSTOMER_ID, int POLICY_VERSION_ID, int POLICY_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_FetchApplicant";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        } 
        public int AddInsuredInformation()
        {
            int returnvalue = 0;


            try
            {
                base.Proc_Add_Name = "PROC_INSERT_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO";   //Insert  Stored Procedure  Name
                base.ReturnIDName = "@PERSONAL_ACCIDENT_ID";   //set out Parameter

                //For Transaction Log
                base.TRANS_TYPE_ID = 217;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //End

                base.MODIFIED_BY.IsDBParam = false;                  //set db parameter
                base.LAST_UPDATED_DATETIME.IsDBParam = false;        //set db parameter    
                this.PERSONAL_ACCIDENT_ID.IsDBParam = false;  //set db parameter 
                this.START_DATE.IsTimePartRequired=false;
                this.END_DATE.IsTimePartRequired=false;
                base.ProcReturnValue = true;
                returnvalue = base.Save();  //base Save methode for Insert
                returnvalue = Proc_ReturnValue;
                this.PERSONAL_ACCIDENT_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                
                
            }
            catch
            {

            }
            finally { }

            return returnvalue;
        }

        public int UpdateInsuredInformation()
        {
            int returnvalue = 0;


            try
            {
                base.Proc_Update_Name = "PROC_UPDATE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO";

                //For Transaction Log
                base.TRANS_TYPE_ID = 218;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //End

                this.ORIGINAL_VERSION_ID.IsDBParam = false;
                base.CREATED_BY.IsDBParam = false;                  //set db parameter
                base.CREATED_DATETIME.IsDBParam = false;        //set db parameter    
                base.IS_ACTIVE.IsDBParam = false;
                this.START_DATE.IsTimePartRequired = false;
                this.END_DATE.IsTimePartRequired = false;
                this.RISK_ORIGINAL_ENDORSEMENT_NO.IsDBParam = false;
                base.ProcReturnValue = true;
                returnvalue = base.Update();  //base Save methode for Insert
                returnvalue = Proc_ReturnValue;

            }
            catch
            {

            }

            return returnvalue;

        }

        public int DeleteInsuredInformation()
        {
            int returnvalue = 0;



            try
            {
                //Set DB Parameters For Delete
                base.Proc_Delete_Name = "PROC_DELETE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PERSONAL_ACCIDENT_ID", PERSONAL_ACCIDENT_ID.CurrentValue);
                
                //For Transaction Log
                base.TRANS_TYPE_ID = 219;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 
            
                returnvalue = base.Delete();
                

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return returnvalue;
        }

        public int ActivateDeactivateInsuredInformation()
        {
            int returnvalue = 0;


            try
            {
                base.Proc_ActivateDeactivate_Name = "PROC_ACTIVATEDEACTIVATE_PASSENGERS_PERSONAL_ACCIDENT_INFO";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PERSONAL_ACCIDENT_ID", PERSONAL_ACCIDENT_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);

                //For Transaction Log
                 if(IS_ACTIVE.CurrentValue.ToString() == "Y")
                    base.TRANS_TYPE_ID = 220;
                else
                    base.TRANS_TYPE_ID = 221;

                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //end 

                returnvalue = base.ActivateDeactivate();

            }
            catch
            {

            }
            finally { }

            return returnvalue;
        }

        public DataSet FetchInsuredInformation()
        {

            DataSet DsData = null;
            try
            {
                base.Proc_FetchData = "PROC_FETCH_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO";
                base.htGetDataParamCollections.Clear();
               
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PERSONAL_ACCIDENT_ID", PERSONAL_ACCIDENT_ID.CurrentValue);

                DsData = base.GetData();

            }
            catch 
            {

            }
            finally { }
            return DsData;

        }  //public  DataSet FetchLocationInformation(Int PRODUCT_RISK_ID)
    }
}
