
/******************************************************************************************
<Author				: - Lalit Chauhan
<Start Date			: -	04-26-2010
<End Date			: -	
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 6 MArch 2010
<Modified By		: - Pravesh K Chandel
<Purpose			: - Review and optimize the code
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
namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsProductLocationInfo : ClsModelBaseClass
    {
         

         public ClsProductLocationInfo() 
         {
             this.PropertyCollection();
         }

         private void PropertyCollection()
         {
             base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
             base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
             base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
             base.htPropertyCollection.Add("PRODUCT_RISK_ID", PRODUCT_RISK_ID);
             base.htPropertyCollection.Add("LOCATION", LOCATION);             
             base.htPropertyCollection.Add("VALUE_AT_RISK", VALUE_AT_RISK);
             base.htPropertyCollection.Add("MAXIMUM_LIMIT", MAXIMUM_LIMIT);
             base.htPropertyCollection.Add("POSSIBLE_MAX_LOSS", POSSIBLE_MAX_LOSS);
             base.htPropertyCollection.Add("PARKING_SPACES", PARKING_SPACES);
             base.htPropertyCollection.Add("CONSTRUCTION", CONSTRUCTION);
             base.htPropertyCollection.Add("MULTIPLE_DEDUCTIBLE", MULTIPLE_DEDUCTIBLE);
             base.htPropertyCollection.Add("ASSIST24", ASSIST24);
             base.htPropertyCollection.Add("REMARKS", REMARKS);
             base.htPropertyCollection.Add("BUILDING_VALUE", BUILDING_VALUE);
             base.htPropertyCollection.Add("CONTENTS_VALUE", CONTENTS_VALUE);
             base.htPropertyCollection.Add("RAW_MATERIAL_VALUE", RAW_MATERIAL_VALUE);
             base.htPropertyCollection.Add("CONTENTS_RAW_VALUES", CONTENTS_RAW_VALUES);
             base.htPropertyCollection.Add("MRI_VALUE", MRI_VALUE);
             base.htPropertyCollection.Add("ACTIVITY_TYPE", ACTIVITY_TYPE);
             base.htPropertyCollection.Add("OCCUPIED_AS", OCCUPIED_AS);
             base.htPropertyCollection.Add("RUBRICA", RUBRICA);
             base.htPropertyCollection.Add("CLAIM_RATIO",CLAIM_RATIO);
             base.htPropertyCollection.Add("BONUS", BONUS);
             base.htPropertyCollection.Add("CO_APPLICANT_ID", CO_APPLICANT_ID);
             base.htPropertyCollection.Add("CLASS_FIELD", CLASS_FIELD);
             base.htPropertyCollection.Add("LOCATION_NUMBER", LOCATION_NUMBER);
             base.htPropertyCollection.Add("ITEM_NUMBER", ITEM_NUMBER);
             base.htPropertyCollection.Add("ACTUAL_INSURED_OBJECT", ACTUAL_INSURED_OBJECT);
             base.htPropertyCollection.Add("ORIGINAL_VERSION_ID", ORIGINAL_VERSION_ID);
             base.htPropertyCollection.Add("PORTABLE_EQUIPMENT", PORTABLE_EQUIPMENT);
             base.htPropertyCollection.Add("EXCEEDED_PREMIUM", EXCEEDED_PREMIUM);
             
         }
        #region Declare the Property for every  table columns

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
             { //return _POLICY_VERSION_ID;
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]) == null ? new EbixInt32("POLICY_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]);
             }
             set
             {
                 //_POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(value);
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
        

         public EbixInt32 PRODUCT_RISK_ID
         {
             get
             { //return _POLICY_VERSION_ID;
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PRODUCT_RISK_ID"]) == null ? new EbixInt32("PRODUCT_RISK_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PRODUCT_RISK_ID"]);
             }
             set
             {
                 //_POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(value);
                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PRODUCT_RISK_ID"]).CurrentValue = Convert.ToInt32(value);
             }
         } //public EbixInt32 PRODUCT_RISK_ID 

         public EbixInt32 LOCATION
         {
             get
             { //return _POLICY_VERSION_ID;
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION"]) == null ? new EbixInt32("LOCATION") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION"]);
             }
             set
             {
                 //_POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(value);
                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION"]).CurrentValue = Convert.ToInt32(value);
             }
         } //public EbixInt32 LOCATION 

         public EbixDouble VALUE_AT_RISK
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VALUE_AT_RISK"]) == null ? new EbixDouble("VALUE_AT_RISK") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VALUE_AT_RISK"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VALUE_AT_RISK"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble VALUE_AT_RISK 

         public EbixDouble MAXIMUM_LIMIT
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MAXIMUM_LIMIT"]) == null ? new EbixDouble("MAXIMUM_LIMIT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MAXIMUM_LIMIT"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MAXIMUM_LIMIT"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble MAXIMUM_LIMIT 

         public EbixDouble POSSIBLE_MAX_LOSS
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["POSSIBLE_MAX_LOSS"]) == null ? new EbixDouble("POSSIBLE_MAX_LOSS") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["POSSIBLE_MAX_LOSS"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["POSSIBLE_MAX_LOSS"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble POSSIBLE_MAX_LOSS 

         //public EbixInt32 PARKING_SPACES
         //{
         //    get
         //    {
         //        return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PARKING_SPACES"]) == null ? new EbixInt32("PARKING_SPACES") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PARKING_SPACES"]);
         //    }
         //    set
         //    {

         //        ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PARKING_SPACES"]).CurrentValue = Convert.ToInt32(value);
         //    }
         //}

         public EbixString PARKING_SPACES
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PARKING_SPACES"]) == null ? new EbixString("PARKING_SPACES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PARKING_SPACES"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PARKING_SPACES"]).CurrentValue = Convert.ToString(value);
             }
         }//public EbixString PARKING_SPACES 

         public EbixInt32 CONSTRUCTION
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONSTRUCTION"]) == null ? new EbixInt32("CONSTRUCTION") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONSTRUCTION"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONSTRUCTION"]).CurrentValue = Convert.ToInt32(value);
             }
         } //public EbixInt32 CONSTRUCTION 

         public EbixInt32 MULTIPLE_DEDUCTIBLE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MULTIPLE_DEDUCTIBLE"]) == null ? new EbixInt32("MULTIPLE_DEDUCTIBLE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MULTIPLE_DEDUCTIBLE"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MULTIPLE_DEDUCTIBLE"]).CurrentValue = Convert.ToInt32(value);
             }
         } //public EbixInt32 MULTIPLE_DEDUCTIBLE 

         public EbixInt32 ASSIST24
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ASSIST24"]) == null ? new EbixInt32("ASSIST24") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ASSIST24"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ASSIST24"]).CurrentValue = Convert.ToInt32(value);
             }
         }  //public EbixInt32 ASSIST24 

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
         }  //public EbixString REMARKS         

         public EbixDouble BUILDING_VALUE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BUILDING_VALUE"]) == null ? new EbixDouble("BUILDING_VALUE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BUILDING_VALUE"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BUILDING_VALUE"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble BUILDING_VALUE 

         public EbixDouble CONTENTS_VALUE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CONTENTS_VALUE"]) == null ? new EbixDouble("CONTENTS_VALUE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CONTENTS_VALUE"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CONTENTS_VALUE"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble CONTENTS_VALUE 

         public EbixDouble RAW_MATERIAL_VALUE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RAW_MATERIAL_VALUE"]) == null ? new EbixDouble("RAW_MATERIAL_VALUE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RAW_MATERIAL_VALUE"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RAW_MATERIAL_VALUE"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble RAW_MATERIAL_VALUE 

         public EbixDouble CONTENTS_RAW_VALUES
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CONTENTS_RAW_VALUES"]) == null ? new EbixDouble("CONTENTS_RAW_VALUES") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CONTENTS_RAW_VALUES"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CONTENTS_RAW_VALUES"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble CONTENTS_RAW_VALUES 

         public EbixDouble MRI_VALUE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MRI_VALUE"]) == null ? new EbixDouble("MRI_VALUE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MRI_VALUE"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MRI_VALUE"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble MRI_VALUE 

         public EbixInt32 ACTIVITY_TYPE
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTIVITY_TYPE"]) == null ? new EbixInt32("ACTIVITY_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTIVITY_TYPE"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTIVITY_TYPE"]).CurrentValue = Convert.ToInt32(value);
             }
         }  //public EbixInt32 ACTIVITY_TYPE 

         public EbixInt32 OCCUPIED_AS
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["OCCUPIED_AS"]) == null ? new EbixInt32("OCCUPIED_AS") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["OCCUPIED_AS"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["OCCUPIED_AS"]).CurrentValue = Convert.ToInt32(value);
             }
         }  //public EbixInt32 OCCUPIED_AS 

         public EbixString RUBRICA
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RUBRICA"]) == null ? new EbixString("RUBRICA") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RUBRICA"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RUBRICA"]).CurrentValue = Convert.ToString(value);
             }
         }  //public EbixString RUBRICA   
         public EbixDouble CLAIM_RATIO
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CLAIM_RATIO"]) == null ? new EbixDouble("CLAIM_RATIO") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CLAIM_RATIO"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CLAIM_RATIO"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble CLAIM_RATIO
         public EbixDouble BONUS
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BONUS"]) == null ? new EbixDouble("BONUS") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BONUS"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BONUS"]).CurrentValue = Convert.ToDouble(value);
             }
         } //public EbixDouble BONUS
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
         public EbixInt32 CLASS_FIELD
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLASS_FIELD"]) == null ? new EbixInt32("CLASS_FIELD") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLASS_FIELD"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLASS_FIELD"]).CurrentValue = Convert.ToInt32(value);
             }
         }//public EbixInt32 CLASS_FIELD
         public EbixInt32 LOCATION_NUMBER
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION_NUMBER"]) == null ? new EbixInt32("LOCATION_NUMBER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION_NUMBER"]);
             }
             set
             {
                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION_NUMBER"]).CurrentValue = Convert.ToInt32(value);
             }
         }
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
         }
         public EbixString ACTUAL_INSURED_OBJECT
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ACTUAL_INSURED_OBJECT"]) == null ? new EbixString("ACTUAL_INSURED_OBJECT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ACTUAL_INSURED_OBJECT"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ACTUAL_INSURED_OBJECT"]).CurrentValue = Convert.ToString(value);
             }
         }  //public EbixString ACTUAL_INSURED_OBJECT   


         public EbixInt32 PORTABLE_EQUIPMENT
         {
             get
             {
                 return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PORTABLE_EQUIPMENT"]) == null ? new EbixInt32("PORTABLE_EQUIPMENT") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PORTABLE_EQUIPMENT"]);
             }
             set
             {

                 ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PORTABLE_EQUIPMENT"]).CurrentValue = Convert.ToInt32(value);
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

        #endregion

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

         public int AddLocationInformation()
         {
             int returnvalue = 0;
           
             
             try
             {
                 base.Proc_Add_Name = "PROC_INSERT_POL_PRODUCT_LOCATION_INFO";   //Insert  Stored Procedure  Name
                 base.ReturnIDName = "@PRODUCT_RISK_ID";   //set out Parameter

                //For Transaction Log
                 base.ProcReturnValue = true;
                 base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                 base.RECORDED_BY = CREATED_BY.CurrentValue;
                 base.POLICYID = POLICY_ID.CurrentValue;
                 base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                 //End

                 base.MODIFIED_BY.IsDBParam = false;                  //set db parameter
                 base.LAST_UPDATED_DATETIME.IsDBParam = false;        //set db parameter    
                 this.PRODUCT_RISK_ID.IsDBParam = false;              //set db parameter 

                 returnvalue=base.Save();  //base Save methode for Insert

                 this.PRODUCT_RISK_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                 returnvalue = Proc_ReturnValue;

             }
             catch
             {

             }
             finally { }

             return returnvalue;
         }

         public int UpdateLocationInformation()
         {
             int returnvalue = 0;
             

             try
             {
                 base.Proc_Update_Name = "PROC_UPDATE_POL_PRODUCT_LOCATION_INFO";

                 //For Transaction Log
                 base.ProcReturnValue = true;
                 base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                 base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                 base.POLICYID = POLICY_ID.CurrentValue;
                 base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                 //End
                 this.ORIGINAL_VERSION_ID.IsDBParam = false;
                 
                 base.CREATED_BY.IsDBParam = false;                  //set db parameter
                 base.CREATED_DATETIME.IsDBParam = false;        //set db parameter    
                 base.IS_ACTIVE.IsDBParam = false;

                 returnvalue = base.Update();  //base Save methode for Insert
                 returnvalue = Proc_ReturnValue;


             }
             catch
             {

             }
             
             return returnvalue;

         }

         public int DeleteLocationInformation(Int32 ConfirmValue)
         {
             int returnvalue = 0;

             

             try
             {
                 //Set DB Parameters For Delete
                 base.Proc_Delete_Name = "PROC_DELETE_POL_PRODUCT_LOCATION_INFO";
                 base.htGetDataParamCollections.Clear();
                 base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@PRODUCT_RISK_ID", PRODUCT_RISK_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION.CurrentValue);
                 base.htGetDataParamCollections.Add("@CONFIRMVALUE", ConfirmValue);
                 //For Transaction Log
                  
                 base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                 base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                 base.POLICYID = POLICY_ID.CurrentValue;
                 base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                 //end 
                 ProcReturnValue = true;
                 returnvalue = base.Delete();
                 returnvalue = Proc_ReturnValue;

             }
             catch (Exception ex)
             {
                 throw (ex);
             }
             return returnvalue;
         }

         public int ActivateDeactivateLocationInformation()
         {
             int returnvalue = 0;

          
             try
             {
                 base.Proc_ActivateDeactivate_Name = "PROC_ACTIVATEDEACTIVATE_PROCUCT_LOCATION_INFO";
                 base.htGetDataParamCollections.Clear();
                 base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@PRODUCT_RISK_ID", PRODUCT_RISK_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);
                 base.htGetDataParamCollections.Add("@LOCATION_NUMBER", LOCATION_NUMBER.CurrentValue);
                 base.htGetDataParamCollections.Add("@ITEM_NUMBER", ITEM_NUMBER.CurrentValue);

                 
                 //For Transaction Log
                 
                 base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                 base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                 base.POLICYID = POLICY_ID.CurrentValue;
                 base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                 //end 
                 base.ProcReturnValue = true;
                 returnvalue = base.ActivateDeactivate();
                 returnvalue = Proc_ReturnValue;

             }
             catch
             {

             }
             finally { }

             return returnvalue;
         }

         public DataSet FetchLocationInformation()
         {
             
             DataSet DsData = null;
             try
             {
                 base.Proc_FetchData = "PROC_FETCH_POL_PRODUCT_LOCATION_INFO";
                 base.htGetDataParamCollections.Clear();
                 base.htGetDataParamCollections.Add("@PRODUCT_RISK_ID", PRODUCT_RISK_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                 base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);

                 DsData=base.GetData();

             }
             catch
             {

             }
             finally { }
             return DsData;

         }  //public  DataSet FetchLocationInformation(Int PRODUCT_RISK_ID)

         /// <summary>
         /// To get the Building Name,Address,Number,Compliment,District and City from the Pol_location 
         /// </summary>
         /// <param name="CustomerID"></param>
         /// <param name="PolicyID"></param>
         /// <returns></returns>
         public DataSet GetLocationDetailsForProductsData(Int32 CustomerID,Int32 PolicyID)
         {
             DataSet ds = null;
             try
             {
                 base.Proc_FetchData = "Proc_GetLocationDetailsForPolProducts";
                 base.htGetDataParamCollections.Clear();

                 base.htGetDataParamCollections.Add("@CUSTOMER_ID", CustomerID);
                 base.htGetDataParamCollections.Add("@POLICY_ID", PolicyID);
                 ds = base.GetData();
             }//try
             catch (Exception ex)
             { throw (ex); }//catch (Exception ex)
             return ds;
         }//public DataSet GetLocationDetailsForProductsData(Int32 CustomerID,Int32 PolicyID)

         /// <summary>
         /// To get the locatin details
         /// </summary>
         /// <param name="LocationID"></param>
         /// <returns></returns>
         public DataSet FetchLocationDataLocationID(Int32 CustomerID,Int32 LocationID)
         {
             DataSet ds = null;
             try
             {
                 base.Proc_FetchData = "Proc_FetchLocationDetails";
                 base.htGetDataParamCollections.Clear();

                 base.htGetDataParamCollections.Add("@CUSTOMER_ID", CustomerID);
                 base.htGetDataParamCollections.Add("@LOCATION_ID", LocationID);
                
                  ds = base.GetData();
                  
             }//try
             catch (Exception ex)
             { throw (ex); }//catch (Exception ex)
             return ds;
         }//public DataSet GetLocationDetailsForProductsData(Int32 CustomerID,Int32 PolicyID)
    }

}
