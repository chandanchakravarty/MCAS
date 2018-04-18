/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	21-04-2010
<End Date			: -	
<Description		: -The Model is use to deal with  Civil Liability Transportation vehicle information 
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
using Cms.EbixDataLayer;



namespace Cms.Model.Policy.Transportation
{
    [Serializable]
    public class ClsCivilTransportVehicleInfo : ClsModelBaseClass
    {
        public ClsCivilTransportVehicleInfo()
        { this.PropertyCollection(); }
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);

            base.htPropertyCollection.Add("VEHICLE_ID", VEHICLE_ID);

            base.htPropertyCollection.Add("CLIENT_ORDER", CLIENT_ORDER);
            base.htPropertyCollection.Add("VEHICLE_NUMBER", VEHICLE_NUMBER);
            base.htPropertyCollection.Add("MANUFACTURED_YEAR", MANUFACTURED_YEAR);
            base.htPropertyCollection.Add("FIPE_CODE", FIPE_CODE);

            base.htPropertyCollection.Add("CATEGORY", CATEGORY);
            base.htPropertyCollection.Add("CAPACITY", CAPACITY);
            base.htPropertyCollection.Add("MAKE_MODEL", MAKE_MODEL);
            base.htPropertyCollection.Add("LICENSE_PLATE", LICENSE_PLATE);
            base.htPropertyCollection.Add("CHASSIS", CHASSIS);

            base.htPropertyCollection.Add("MANDATORY_DEDUCTIBLE", MANDATORY_DEDUCTIBLE);
            base.htPropertyCollection.Add("FACULTATIVE_DEDUCTIBLE", FACULTATIVE_DEDUCTIBLE);
            base.htPropertyCollection.Add("SUB_BRANCH", SUB_BRANCH);
            base.htPropertyCollection.Add("RISK_EFFECTIVE_DATE", RISK_EFFECTIVE_DATE);
            base.htPropertyCollection.Add("RISK_EXPIRE_DATE", RISK_EXPIRE_DATE);

            base.htPropertyCollection.Add("REGION", REGION);
            base.htPropertyCollection.Add("COV_GROUP_CODE", COV_GROUP_CODE);
            base.htPropertyCollection.Add("FINANCE_ADJUSTMENT", FINANCE_ADJUSTMENT);
            base.htPropertyCollection.Add("REFERENCE_PROPOSASL", REFERENCE_PROPOSASL);
            base.htPropertyCollection.Add("REMARKS", REMARKS);
            base.htPropertyCollection.Add("CO_APPLICANT_ID", CO_APPLICANT_ID);

            base.htPropertyCollection.Add("TICKET_NUMBER", TICKET_NUMBER);
            base.htPropertyCollection.Add("STATE_ID", STATE_ID);
            base.htPropertyCollection.Add("ZIP_CODE", ZIP_CODE);
            base.htPropertyCollection.Add("CALLED_FROM", CALLED_FROM);
            base.htPropertyCollection.Add("ORIGINAL_VERSION_ID", ORIGINAL_VERSION_ID);
            base.htPropertyCollection.Add("EXCEEDED_PREMIUM", EXCEEDED_PREMIUM);
            

        }//private void PropertyCollection()

        #region Declare the Property for every data table columns


        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
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

        public EbixInt32 VEHICLE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VEHICLE_ID"]) == null ? new EbixInt32("VEHICLE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VEHICLE_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VEHICLE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 VEHICLE_ID

        public EbixDouble CLIENT_ORDER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CLIENT_ORDER"]) == null ? new EbixDouble("CLIENT_ORDER") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CLIENT_ORDER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CLIENT_ORDER"]).CurrentValue = Convert.ToDouble(value);
            }
        }// public EbixInt32 CLIENT_ORDER

        public EbixDouble VEHICLE_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VEHICLE_NUMBER"]) == null ? new EbixDouble("VEHICLE_NUMBER") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VEHICLE_NUMBER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VEHICLE_NUMBER"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixInt32 VEHICLE_NUMBER

        public EbixInt32 MANUFACTURED_YEAR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUFACTURED_YEAR"]) == null ? new EbixInt32("MANUFACTURED_YEAR") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUFACTURED_YEAR"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUFACTURED_YEAR"]).CurrentValue = Convert.ToInt32(value);
            }
        }// public EbixInt32 MANUFACTURED_YEAR

        public EbixString FIPE_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FIPE_CODE"]) == null ? new EbixString("FIPE_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FIPE_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FIPE_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }// public EbixString FIPE_CODE

        public EbixInt32 CATEGORY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CATEGORY"]) == null ? new EbixInt32("CATEGORY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CATEGORY"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CATEGORY"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 CATEGORY

        public EbixString CAPACITY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CAPACITY"]) == null ? new EbixString("CAPACITY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CAPACITY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CAPACITY"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString CAPACITY

        public EbixString MAKE_MODEL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MAKE_MODEL"]) == null ? new EbixString("MAKE_MODEL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MAKE_MODEL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MAKE_MODEL"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString MAKE_MODEL

        public EbixString LICENSE_PLATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LICENSE_PLATE"]) == null ? new EbixString("LICENSE_PLATE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LICENSE_PLATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LICENSE_PLATE"]).CurrentValue = Convert.ToString(value);
            }
        }// public EbixString LICENSE_PLATE

        public EbixString CHASSIS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CHASSIS"]) == null ? new EbixString("CHASSIS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CHASSIS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CHASSIS"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString CHASSIS

        public EbixDouble MANDATORY_DEDUCTIBLE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MANDATORY_DEDUCTIBLE"]) == null ? new EbixDouble("MANDATORY_DEDUCTIBLE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MANDATORY_DEDUCTIBLE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MANDATORY_DEDUCTIBLE"]).CurrentValue = Convert.ToDouble(value);
            }
        }// public EbixDouble MANDATORY_DEDUCTIBLE

        public EbixDouble FACULTATIVE_DEDUCTIBLE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FACULTATIVE_DEDUCTIBLE"]) == null ? new EbixDouble("FACULTATIVE_DEDUCTIBLE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FACULTATIVE_DEDUCTIBLE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["FACULTATIVE_DEDUCTIBLE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble FACULTATIVE_DEDUCTIBLE

        public EbixInt32 SUB_BRANCH
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUB_BRANCH"]) == null ? new EbixInt32("SUB_BRANCH") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUB_BRANCH"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUB_BRANCH"]).CurrentValue = Convert.ToInt32(value);
            }

        }//public EbixInt32 SUB_BRANCH

        public EbixDateTime RISK_EFFECTIVE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RISK_EFFECTIVE_DATE"]) == null ? new EbixDateTime("RISK_EFFECTIVE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RISK_EFFECTIVE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RISK_EFFECTIVE_DATE"]).CurrentValue = Convert.ToDateTime(value);

            }
        }//public EbixDateTime RISK_EFFECTIVE_DATE

        public EbixDateTime RISK_EXPIRE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RISK_EXPIRE_DATE"]) == null ? new EbixDateTime("RISK_EXPIRE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RISK_EXPIRE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["RISK_EXPIRE_DATE"]).CurrentValue = Convert.ToDateTime(value);

            }
        }//public EbixDateTime RISK_EXPIRE_DATE

        public EbixInt32 REGION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REGION"]) == null ? new EbixInt32("REGION") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REGION"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REGION"]).CurrentValue = Convert.ToInt32(value);
            }
        }// public EbixInt32 REGION

        public EbixString COV_GROUP_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COV_GROUP_CODE"]) == null ? new EbixString("COV_GROUP_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COV_GROUP_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COV_GROUP_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString COV_GROUP_CODE

        public EbixString FINANCE_ADJUSTMENT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FINANCE_ADJUSTMENT"]) == null ? new EbixString("FINANCE_ADJUSTMENT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FINANCE_ADJUSTMENT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FINANCE_ADJUSTMENT"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString FINANCE_ADJUSTMENT

        public EbixString REFERENCE_PROPOSASL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REFERENCE_PROPOSASL"]) == null ? new EbixString("REFERENCE_PROPOSASL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REFERENCE_PROPOSASL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REFERENCE_PROPOSASL"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString REFERENCE_PROPOSASL

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
        }// public EbixString REMARKS

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
        public EbixInt32 TICKET_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TICKET_NUMBER"]) == null ? new EbixInt32("TICKET_NUMBER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TICKET_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TICKET_NUMBER"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 TICKET_NUMBER 
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

        public EbixString ZIP_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ZIP_CODE"]) == null ? new EbixString("ZIP_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ZIP_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ZIP_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }// public EbixString ZIP_CODE
        
        public EbixString CALLED_FROM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CALLED_FROM"]) == null ? new EbixString("CALLED_FROM") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CALLED_FROM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CALLED_FROM"]).CurrentValue = Convert.ToString(value);
            }
        }// public EbixString CALLED_FROM

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

        #endregion

        /// <summary>
        /// Use to get the Civil Transport Vehicle info data
        /// </summary>
        /// <returns></returns>
        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetCivilTransportVehicleInfoData";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@VEHICLE_ID", VEHICLE_ID.CurrentValue);
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

         
        /// <summary>
        /// Use to insert Civil Transport Vehicle information
        /// </summary>
        /// <returns>int</returns>
        /// 

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
        public int AddCivilTransportVehicleData( )
        {
            

            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertCivilTransportVehicle";

                base.ReturnIDName = "@VEHICLE_ID"; // //Set the out parameter

                //For Transaction Log
               
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

              
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.VEHICLE_ID.IsDBParam = false;
                this.STATE_ID.IsDBParam = false;
                this.TICKET_NUMBER.IsDBParam = false;
 
                
                base.ProcReturnValue = true;//Set true for Return Value from Procedure  
                returnResult = base.Save();

                returnResult = Proc_ReturnValue;

                this.VEHICLE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;

            
        }//public int AddCivilTransportVehicleData()

        public int AddCivilDPVATTransportVehicleData()
        {


            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertDPVATCivilTransportVehicle";

                base.ReturnIDName = "@VEHICLE_ID"; // //Set the out parameter

                //For Transaction Log

                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.VEHICLE_ID.IsDBParam = false;

                this.CLIENT_ORDER.IsDBParam = false;
                this.VEHICLE_NUMBER.IsDBParam = false;
                this.MANUFACTURED_YEAR.IsDBParam = false;
                this.FIPE_CODE.IsDBParam = false;
                this.CAPACITY.IsDBParam = false;
                this.MAKE_MODEL.IsDBParam = false;
                this.LICENSE_PLATE.IsDBParam = false;
                this.CHASSIS.IsDBParam = false;
                this.MANDATORY_DEDUCTIBLE.IsDBParam = false;
                this.FACULTATIVE_DEDUCTIBLE.IsDBParam = false;
                this.SUB_BRANCH.IsDBParam = false;
                this.RISK_EFFECTIVE_DATE.IsDBParam = false;
                this.RISK_EXPIRE_DATE.IsDBParam = false;
                this.REGION.IsDBParam = false;
                this.COV_GROUP_CODE.IsDBParam = false;
                this.FINANCE_ADJUSTMENT.IsDBParam = false;
                this.REFERENCE_PROPOSASL.IsDBParam = false;
                this.REMARKS.IsDBParam = false;
                this.CO_APPLICANT_ID.IsDBParam = false;
                this.ZIP_CODE.IsDBParam = false;
                this.CALLED_FROM.IsDBParam = false;
                
               
                base.ProcReturnValue = true;//Set true for Return Value from Procedure  
                returnResult = base.Save();

                returnResult = Proc_ReturnValue;

                this.VEHICLE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;


        }//public int AddCivilTransportVehicleData()

        /// <summary>
        /// Use to Update DPVAT (Cat. 3 e 4) product information 
        /// </summary>
        /// <returns></returns>
        public int UpdateDPVATCivilTransportVehicleData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateDPVATCivilTransportVehicle";

                //For Transaction Log

                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 

                this.ORIGINAL_VERSION_ID.IsDBParam = false;
                this.CLIENT_ORDER.IsDBParam = false;
                this.VEHICLE_NUMBER.IsDBParam = false;
                this.MANUFACTURED_YEAR.IsDBParam = false;
                this.FIPE_CODE.IsDBParam = false;
                this.CAPACITY.IsDBParam = false;
                this.MAKE_MODEL.IsDBParam = false;
                this.LICENSE_PLATE.IsDBParam = false;
                this.CHASSIS.IsDBParam = false;
                this.MANDATORY_DEDUCTIBLE.IsDBParam = false;
                this.FACULTATIVE_DEDUCTIBLE.IsDBParam = false;
                this.SUB_BRANCH.IsDBParam = false;
                this.RISK_EFFECTIVE_DATE.IsDBParam = false;
                this.RISK_EXPIRE_DATE.IsDBParam = false;
                this.REGION.IsDBParam = false;
                this.COV_GROUP_CODE.IsDBParam = false;
                this.FINANCE_ADJUSTMENT.IsDBParam = false;
                this.REFERENCE_PROPOSASL.IsDBParam = false;
                this.REMARKS.IsDBParam = false;
                this.CO_APPLICANT_ID.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.ZIP_CODE.IsDBParam = false;
                this.CALLED_FROM.IsDBParam = false;    
                
                base.ProcReturnValue = true;//Set true for Return Value from Procedure  

                returnValue = base.Update();

                returnValue = base.Proc_ReturnValue; //Get the retun value from procedure 

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }//public int UpdateDPVATCivilTransportVehicleData()

        /// <summary>
        /// Use to Update Civil Transport Vehicle information 
        /// </summary>
        /// <returns></returns>
        public int UpdateCivilTransportVehicleData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateCivilTransportVehicle";

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
                this.STATE_ID.IsDBParam = false;
                this.TICKET_NUMBER.IsDBParam = false;
               
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
        }//public int UpdateCivilTransportVehicleData()

        /// <summary>
        ///  Use to Delete Civil Transport Vehicle information
        /// </summary>
        /// <returns></returns>
        public int DeleteCivilTransportVehicleData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Delete_Name = "Proc_DeleteCivilTransportVehicle";
                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@VEHICLE_ID", VEHICLE_ID.CurrentValue);
               
                 

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

        }// public int DeleteCivilTransportVehicleData()

        /// <summary>
        ///  Use to Activate and Deactivate Civil Transport Vehicle information based on the some keys 
        /// </summary>
        /// <returns></returns>
        public int ActivateDeactivateCivilTransportVehicleData(string CalledFrom)
        {
            int returnValue = 0;
            try
            {

                base.Proc_ActivateDeactivate_Name = "Proc_ActivateDeactivateCivilTransportVehicle";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@VEHICLE_ID", VEHICLE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);
                base.htGetDataParamCollections.Add("@CalledFrom", CalledFrom);

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

        }//public int ActivateDeactivateCivilTransportVehicleData()



    }
}
