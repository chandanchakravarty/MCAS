using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;


namespace Cms.BusinessLayer.BLClaims
{
    [Serializable]
    public class ClsCoinsuranceInfo : ClsModelBaseClass
    {
          /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsCoinsuranceInfo()
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

            base.htPropertyCollection.Add("COINSURANCE_ID", COINSURANCE_ID);
            base.htPropertyCollection.Add("CLAIM_ID", CLAIM_ID);
            base.htPropertyCollection.Add("LEADER_SUSEP_CODE", LEADER_SUSEP_CODE);
            base.htPropertyCollection.Add("LEADER_POLICY_NUMBER", LEADER_POLICY_NUMBER);
            base.htPropertyCollection.Add("LEADER_ENDORSEMENT_NUMBER", LEADER_ENDORSEMENT_NUMBER);
            base.htPropertyCollection.Add("LEADER_CLAIM_NUMBER", LEADER_CLAIM_NUMBER);
            base.htPropertyCollection.Add("CLAIM_REGISTRATION_DATE", CLAIM_REGISTRATION_DATE);
            base.htPropertyCollection.Add("LITIGATION_FILE", LITIGATION_FILE);

   

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

        public EbixInt32 COINSURANCE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COINSURANCE_ID"]) == null ? new EbixInt32("COINSURANCE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COINSURANCE_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COINSURANCE_ID"]).CurrentValue = Convert.ToInt32(value);
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
        public EbixString LEADER_SUSEP_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_SUSEP_CODE"]) == null ? new EbixString("LEADER_SUSEP_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_SUSEP_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_SUSEP_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString LEADER_POLICY_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_POLICY_NUMBER"]) == null ? new EbixString("LEADER_POLICY_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_POLICY_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_POLICY_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString LEADER_ENDORSEMENT_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_ENDORSEMENT_NUMBER"]) == null ? new EbixString("LEADER_ENDORSEMENT_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_ENDORSEMENT_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_ENDORSEMENT_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString LEADER_CLAIM_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_CLAIM_NUMBER"]) == null ? new EbixString("LEADER_CLAIM_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_CLAIM_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LEADER_CLAIM_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixDateTime CLAIM_REGISTRATION_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["CLAIM_REGISTRATION_DATE"]) == null ? new EbixDateTime("CLAIM_REGISTRATION_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["CLAIM_REGISTRATION_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["CLAIM_REGISTRATION_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixInt32 LITIGATION_FILE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LITIGATION_FILE"]) == null ? new EbixInt32("LITIGATION_FILE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LITIGATION_FILE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LITIGATION_FILE"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        #endregion



      

        public DataSet GetClaimCoinsuranceDetails()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetClaimCoinsuranceDetails";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CLAIM_ID", CLAIM_ID.CurrentValue);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)
        public int AddClaimCoinsuranceDetails()
        {
            int returnResult = 0;
            try
            {
                //For Transaction Log
                this.TRANS_TYPE_ID = 304;
                base.Proc_Add_Name = "Proc_InsertClaimCoinsuranceDetails";

                base.ReturnIDName = "@COINSURANCE_ID";

                this.COINSURANCE_ID.IsDBParam = false;
                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
             
                returnResult = base.Save();

                this.COINSURANCE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }


        public int UpdateClaimCoinsuranceDetails()
        {
            int returnValue = 0;
            try
            {

                this.TRANS_TYPE_ID = 305;
                base.Proc_Update_Name = "Proc_UpdateClaimCoinsuranceDetails";

               
                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
                
                this.IS_ACTIVE.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
               
                
                this.CREATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;

                returnValue = base.Update();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }


    }
}
