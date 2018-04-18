
/******************************************************************************************
<Author				: - Santosh Kumar Gautam
<Start Date			: - 09 Nov, 2010
<End Date			: -	
<Description		: - Model Class for Risk Information page functionality.
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

namespace Cms.Model.Claims
{

    [Serializable]
    public class ClsLitigationInfo : ClsModelBaseClass
    {

               /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsLitigationInfo()
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
            /* if (ClauseName == base.SelectClause)
             {
                 base.htPropertyCollection.Add("PERIL_ID", PERIL_ID);
             }*/
            //if (ClauseName == base.AddClause || ClauseName == "")
            //{

            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);

            base.htPropertyCollection.Add("LITIGATION_ID", LITIGATION_ID);
            base.htPropertyCollection.Add("CLAIM_ID", CLAIM_ID);
            base.htPropertyCollection.Add("JUDICIAL_PROCESS_NO", JUDICIAL_PROCESS_NO);
            base.htPropertyCollection.Add("JUDICIAL_COMPLAINT_STATE", JUDICIAL_COMPLAINT_STATE);
            base.htPropertyCollection.Add("PLAINTIFF_NAME", PLAINTIFF_NAME);
            base.htPropertyCollection.Add("PLAINTIFF_CPF", PLAINTIFF_CPF);
            base.htPropertyCollection.Add("PLAINTIFF_REQUESTED_AMOUNT", PLAINTIFF_REQUESTED_AMOUNT);
            base.htPropertyCollection.Add("DEFEDANT_OFFERED_AMOUNT", DEFEDANT_OFFERED_AMOUNT);
            base.htPropertyCollection.Add("ESTIMATE_CLASSIFICATION", ESTIMATE_CLASSIFICATION);
            base.htPropertyCollection.Add("OPERATION_REASON", OPERATION_REASON);
            base.htPropertyCollection.Add("EXPERT_SERVICE_ID", EXPERT_SERVICE_ID);
            base.htPropertyCollection.Add("JUDICIAL_PROCESS_DATE", JUDICIAL_PROCESS_DATE);
                    

                    


        }//private void PropertyCollection()s


        #endregion

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
            { //return _POLICY_VERSION_ID;
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]) == null ? new EbixInt32("POLICY_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]);
            }
            set
            {
                //_POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(value);
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 POLICY_VERSION_ID 

        public EbixInt32 LITIGATION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LITIGATION_ID"]) == null ? new EbixInt32("LITIGATION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LITIGATION_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LITIGATION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixInt32 CLAIM_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_ID"]) == null ? new EbixInt32("CLAIM_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }


        public EbixInt32 JUDICIAL_COMPLAINT_STATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["JUDICIAL_COMPLAINT_STATE"]) == null ? new EbixInt32("JUDICIAL_COMPLAINT_STATE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["JUDICIAL_COMPLAINT_STATE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["JUDICIAL_COMPLAINT_STATE"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 EXPERT_SERVICE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXPERT_SERVICE_ID"]) == null ? new EbixInt32("EXPERT_SERVICE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXPERT_SERVICE_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["EXPERT_SERVICE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString JUDICIAL_PROCESS_NO
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["JUDICIAL_PROCESS_NO"]) == null ? new EbixString("JUDICIAL_PROCESS_NO") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["JUDICIAL_PROCESS_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["JUDICIAL_PROCESS_NO"]).CurrentValue = Convert.ToString(value);
            }
        }


        public EbixString PLAINTIFF_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PLAINTIFF_NAME"]) == null ? new EbixString("PLAINTIFF_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PLAINTIFF_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PLAINTIFF_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PLAINTIFF_CPF
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PLAINTIFF_CPF"]) == null ? new EbixString("PLAINTIFF_CPF") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PLAINTIFF_CPF"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PLAINTIFF_CPF"]).CurrentValue = Convert.ToString(value);
            }
        }
      

        public EbixDouble PLAINTIFF_REQUESTED_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PLAINTIFF_REQUESTED_AMOUNT"]) == null ? new EbixDouble("PLAINTIFF_REQUESTED_AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PLAINTIFF_REQUESTED_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PLAINTIFF_REQUESTED_AMOUNT"]).CurrentValue = Convert.ToDouble(value);
            }
        }



        public EbixDouble DEFEDANT_OFFERED_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["DEFEDANT_OFFERED_AMOUNT"]) == null ? new EbixDouble("DEFEDANT_OFFERED_AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["DEFEDANT_OFFERED_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["DEFEDANT_OFFERED_AMOUNT"]).CurrentValue = Convert.ToDouble(value);
            }
        }





        public EbixInt32 ESTIMATE_CLASSIFICATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ESTIMATE_CLASSIFICATION"]) == null ? new EbixInt32("ESTIMATE_CLASSIFICATION") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ESTIMATE_CLASSIFICATION"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ESTIMATE_CLASSIFICATION"]).CurrentValue = Convert.ToInt32(value);
            }
        }



        public EbixInt32 OPERATION_REASON
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["OPERATION_REASON"]) == null ? new EbixInt32("OPERATION_REASON") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["OPERATION_REASON"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["OPERATION_REASON"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixDateTime JUDICIAL_PROCESS_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["JUDICIAL_PROCESS_DATE"]) == null ? new EbixDateTime("JUDICIAL_PROCESS_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["JUDICIAL_PROCESS_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["JUDICIAL_PROCESS_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }


        #endregion

        


        public int AddLitigationInformation()
        {
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 288;
                base.Proc_Add_Name = "Proc_InsertLitigationInformation";

                base.ReturnIDName = "@LITIGATION_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 288;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;


                this.DEFEDANT_OFFERED_AMOUNT.IsDBParam = true;
                this.ESTIMATE_CLASSIFICATION.IsDBParam = true;
                this.EXPERT_SERVICE_ID.IsDBParam = true;
                this.JUDICIAL_COMPLAINT_STATE.IsDBParam = true;
                this.JUDICIAL_PROCESS_DATE.IsDBParam = true;


                this.JUDICIAL_PROCESS_NO.IsDBParam = true;
                this.OPERATION_REASON.IsDBParam = true;
                this.PLAINTIFF_CPF.IsDBParam = true;
                this.PLAINTIFF_NAME.IsDBParam = true;
                this.PLAINTIFF_REQUESTED_AMOUNT.IsDBParam = true;

                this.CREATED_BY.IsDBParam = true;
                this.CREATED_DATETIME.IsDBParam = true;
                //end 
                this.IS_ACTIVE.IsDBParam = false;
                this.LITIGATION_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
             

                returnResult = base.Save();

                this.LITIGATION_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }//public int AddNamedParilsData()


        public int UpdateLitigationInformation()
        {
            int returnValue = 0;
            try
            {

                this.TRANS_TYPE_ID = 289;
                base.Proc_Update_Name = "Proc_UpdateLitigationInformation";

                //For Transaction Log
                base.TRANS_TYPE_ID = 289;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 

                this.DEFEDANT_OFFERED_AMOUNT.IsDBParam = true;
                this.ESTIMATE_CLASSIFICATION.IsDBParam = true;
                this.EXPERT_SERVICE_ID.IsDBParam = true;
                this.JUDICIAL_COMPLAINT_STATE.IsDBParam = true;
                this.JUDICIAL_PROCESS_DATE.IsDBParam = true;


                this.JUDICIAL_PROCESS_NO.IsDBParam = true;
                this.OPERATION_REASON.IsDBParam = true;
                this.PLAINTIFF_CPF.IsDBParam = true;
                this.PLAINTIFF_NAME.IsDBParam = true;
                this.PLAINTIFF_REQUESTED_AMOUNT.IsDBParam = true;

                this.MODIFIED_BY.IsDBParam = true;
                this.LAST_UPDATED_DATETIME.IsDBParam = true;


                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CLAIM_ID.IsDBParam = false;
                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
               
                base.IS_ACTIVE.IsDBParam = false;

                returnValue = base.Update();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }//public int AddNamedParilsData()


        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetLitigationInformation";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@LITIGATION_ID", LITIGATION_ID.CurrentValue);               
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)

        public DataSet GetClaimExpertServiceProvider()
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetClaimExpertServiceProvider";
                base.htGetDataParamCollections.Clear();
                ds = base.GetData();

            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }


        public int ActivateDeactivate()
        {
            int returnValue = 0;
            try
            {


                base.Proc_Update_Name = "Proc_ActivateDeactivateLitigationInfo";



                //For Transaction Log
                if (base.IS_ACTIVE.ToString() == "Y") // TO DEACTIVATE
                    base.TRANS_TYPE_ID = 391;
                else
                    base.TRANS_TYPE_ID = 390;      // TO ACTIVATE

                //end 


                this.DEFEDANT_OFFERED_AMOUNT.IsDBParam = false;
                this.ESTIMATE_CLASSIFICATION.IsDBParam = false;
                this.EXPERT_SERVICE_ID.IsDBParam = false;
                this.JUDICIAL_COMPLAINT_STATE.IsDBParam = false;
                this.JUDICIAL_PROCESS_DATE.IsDBParam = false;


                this.JUDICIAL_PROCESS_NO.IsDBParam = false;
                this.OPERATION_REASON.IsDBParam = false;
                this.PLAINTIFF_CPF.IsDBParam = false;
                this.PLAINTIFF_NAME.IsDBParam = false;
                this.PLAINTIFF_REQUESTED_AMOUNT.IsDBParam = false;


              
               
                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;              

                returnValue = base.Update();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }
    }
}

