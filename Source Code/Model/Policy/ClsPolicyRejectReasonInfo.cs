
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	07-07-2010
<End Date			: -	
<Description		: - Reject Policy reason description
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

namespace Model.Policy
{
    [Serializable]
    public class ClsPolicyRejectReasonInfo : ClsModelBaseClass
    {
       public ClsPolicyRejectReasonInfo()
        { this.PropertyCollection(); }
       /// <summary>
       /// Use to add the parameter collection for the data wrapper class
       /// </summary>
       private void PropertyCollection()
       {

           base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
           base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
           base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
           base.htPropertyCollection.Add("REJECT_REASON_ID", REJECT_REASON_ID);
           base.htPropertyCollection.Add("REASON_TYPE_ID", REASON_TYPE_ID);
           base.htPropertyCollection.Add("REASON_DESC", REASON_DESC);
           

       }//private void PropertyCollection()

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

       public EbixInt32 REJECT_REASON_ID
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REJECT_REASON_ID"]) == null ? new EbixInt32("REJECT_REASON_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REJECT_REASON_ID"]);
           }
           set
           {

               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REJECT_REASON_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }//public EbixInt32 REJECT_REASON_ID

       public EbixInt32 REASON_TYPE_ID
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REASON_TYPE_ID"]) == null ? new EbixInt32("REASON_TYPE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REASON_TYPE_ID"]);
           }
           set
           {

               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REASON_TYPE_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }//public EbixInt32 REASON_TYPE_ID

       public EbixString REASON_DESC
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REASON_DESC"]) == null ? new EbixString("REASON_DESC") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REASON_DESC"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REASON_DESC"]).CurrentValue = Convert.ToString(value);
           }
       }//public EbixString REASON_DESC


       /// <summary>
       /// Use to insert the policy reject reason data
       /// </summary>
       /// <returns>int</returns>
       public int AddPolicyRejectReasonData()
       {
           int returnResult = 0;
           try
           {

               base.Proc_Add_Name = "Proc_InsertPolRejectReason";

               base.ReturnIDName = "@REJECT_REASON_ID";

               //For Transaction Log
               base.TRANS_TYPE_ID = 263;
               base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
               base.RECORDED_BY = CREATED_BY.CurrentValue;
               base.POLICYID = POLICY_ID.CurrentValue;
               base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

               //end 


               this.MODIFIED_BY.IsDBParam = false;
               this.LAST_UPDATED_DATETIME.IsDBParam = false;
               this.REJECT_REASON_ID.IsDBParam = false;
                
               returnResult = base.Save();
                
               this.REJECT_REASON_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

           }
           catch (Exception ex)
           {
               throw (ex);
           }//catch (Exception ex)
           finally { }
           return returnResult;
       }//public int AddPolicyRejectReasonData()
       public int UpdateRejectReasonInformation()
       {
           int returnvalue = 0;
           try
           {
               base.Proc_Update_Name = "PROC_UPDATE_POL_POLICY_REJECTION";
               //For Transaction Log
               //base.TRANS_TYPE_ID = 264;
               base.TRANS_TYPE_ID = 263;
               base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
               base.RECORDED_BY = MODIFIED_BY.CurrentValue;
               base.POLICYID = POLICY_ID.CurrentValue;
               base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
               //End
               base.CREATED_BY.IsDBParam = false;                  //set db parameter
               base.CREATED_DATETIME.IsDBParam = false;        //set db parameter    
               base.IS_ACTIVE.IsDBParam = false;

               returnvalue = base.Update();  //base Save methode for Insert
            }
           catch
           {}
            return returnvalue;

       }// public int UpdateRejectReasonInformation()

       public DataSet FetchRejectReasonDetails()
       {

           DataSet DsData = null;
           try
           {
               base.Proc_FetchData = "PROC_FETCH_POL_POLICY_REJECTION";
               base.htGetDataParamCollections.Clear();
               base.htGetDataParamCollections.Add("@REJECT_REASON_ID", REJECT_REASON_ID.CurrentValue);
               base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
               base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
               base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);

               DsData = base.GetData();

           }
           catch 
           {

           }
           finally { }
           return DsData;

       }  //public  DataSet FetchRejectReasonDetails()
    }
}
