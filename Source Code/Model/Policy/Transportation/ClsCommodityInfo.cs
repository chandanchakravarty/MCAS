/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	15-04-2010
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
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;


namespace Cms.Model.Policy.Transportation
{
    [Serializable]
    public class ClsCommodityInfo : ClsModelBaseClass
    {
        public ClsCommodityInfo()
        {
            this.PropertyCollection();
        }
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);

            base.htPropertyCollection.Add("COMMODITY_ID", COMMODITY_ID);

            base.htPropertyCollection.Add("COMMODITY_NUMBER", COMMODITY_NUMBER);
            base.htPropertyCollection.Add("COMMODITY", COMMODITY);
            base.htPropertyCollection.Add("CONVEYANCE_TYPE", CONVEYANCE_TYPE);
            base.htPropertyCollection.Add("DEPARTING_DATE", DEPARTING_DATE);
            base.htPropertyCollection.Add("ORIGIN_COUNTRY", ORIGIN_COUNTRY);
            base.htPropertyCollection.Add("ORIGIN_STATE", ORIGIN_STATE);
            base.htPropertyCollection.Add("ORIGIN_CITY", ORIGIN_CITY);
            base.htPropertyCollection.Add("DESTINATION_COUNTRY", DESTINATION_COUNTRY);
            base.htPropertyCollection.Add("DESTINATION_STATE", DESTINATION_STATE);
            base.htPropertyCollection.Add("DESTINATION_CITY", DESTINATION_CITY);
            base.htPropertyCollection.Add("REMARKS", REMARKS);
            base.htPropertyCollection.Add("CO_APPLICANT_ID", CO_APPLICANT_ID);

            base.htPropertyCollection.Add("ORIGN_COUNTRY", ORIGN_COUNTRY);
            base.htPropertyCollection.Add("DEST_COUNTRY", DEST_COUNTRY);
            base.htPropertyCollection.Add("ORIGN_STATE", ORIGN_STATE);
            base.htPropertyCollection.Add("DEST_STATE", DEST_STATE);
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

        public EbixInt32 COMMODITY_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMMODITY_ID"]) == null ? new EbixInt32("COMMODITY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMMODITY_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMMODITY_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 COMMODITY_ID

        public EbixDouble COMMODITY_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMMODITY_NUMBER"]) == null ? new EbixDouble("COMMODITY_NUMBER") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMMODITY_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMMODITY_NUMBER"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble COMMODITY_NUMBER

        public EbixString COMMODITY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COMMODITY"]) == null ? new EbixString("COMMODITY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COMMODITY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COMMODITY"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString COMMODITY

        public EbixInt32 CONVEYANCE_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONVEYANCE_TYPE"]) == null ? new EbixInt32("CONVEYANCE_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONVEYANCE_TYPE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONVEYANCE_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 CONVEYANCE_TYPE

        public EbixDateTime DEPARTING_DATE
        {
            get 
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DEPARTING_DATE"]) == null ? new EbixDateTime("DEPARTING_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DEPARTING_DATE"]); 
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["DEPARTING_DATE"]).CurrentValue = Convert.ToDateTime(value);

            }
        }// public EbixDateTime DEPARTING_DATE

        public EbixInt32 ORIGIN_COUNTRY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGIN_COUNTRY"]) == null ? new EbixInt32("ORIGIN_COUNTRY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGIN_COUNTRY"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGIN_COUNTRY"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 ORIGIN_COUNTRY

        public EbixInt32 ORIGIN_STATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGIN_STATE"]) == null ? new EbixInt32("ORIGIN_STATE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGIN_STATE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ORIGIN_STATE"]).CurrentValue = Convert.ToInt32(value);
            }
        }// public EbixInt32 ORIGIN_STATE

        public EbixString ORIGIN_CITY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ORIGIN_CITY"]) == null ? new EbixString("ORIGIN_CITY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ORIGIN_CITY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ORIGIN_CITY"]).CurrentValue = Convert.ToString(value);
            }

        }//public EbixString ORIGIN_CITY

        public EbixInt32 DESTINATION_COUNTRY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DESTINATION_COUNTRY"]) == null ? new EbixInt32("DESTINATION_COUNTRY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DESTINATION_COUNTRY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DESTINATION_COUNTRY"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 DESTINATION_COUNTRY

        public EbixInt32 DESTINATION_STATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DESTINATION_STATE"]) == null ? new EbixInt32("DESTINATION_STATE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DESTINATION_STATE"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DESTINATION_STATE"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 DESTINATION_STATE

        public EbixString DESTINATION_CITY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DESTINATION_CITY"]) == null ? new EbixString("DESTINATION_CITY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DESTINATION_CITY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DESTINATION_CITY"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString DESTINATION_CITY

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
        }//public EbixString REMARKS

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
        
        //Added by Pradeep Kushwaha on 30-March-2011 Itrack-856 
        public EbixString ORIGN_COUNTRY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ORIGN_COUNTRY"]) == null ? new EbixString("ORIGN_COUNTRY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ORIGN_COUNTRY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ORIGN_COUNTRY"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString ORIGN_COUNTRY
        public EbixString DEST_COUNTRY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEST_COUNTRY"]) == null ? new EbixString("DEST_COUNTRY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEST_COUNTRY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEST_COUNTRY"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString DEST_COUNTRY
        public EbixString ORIGN_STATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ORIGN_STATE"]) == null ? new EbixString("ORIGN_STATE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ORIGN_STATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ORIGN_STATE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString ORIGN_STATE
        public EbixString DEST_STATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEST_STATE"]) == null ? new EbixString("DEST_STATE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEST_STATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DEST_STATE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString DEST_STATE

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

        //Added till here on 30-March-2011 Itrack-856 
        #endregion


        /// <summary>
        /// Use to get the Commodity info details using Commodity id
        /// </summary>
        /// <param name="CommodityID"></param>
        /// <returns></returns>
        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetCommodityDatainfo";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@COMMODITY_ID", COMMODITY_ID.CurrentValue);
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
        }//public DataSet FetchData(int CommodityID)



        /// <summary>
        /// Use to Insert the Commodity Info Data
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
        public int AddCommodityData()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertCommodityInfo";

                base.ReturnIDName = "@COMMODITY_ID";//Set the out parameter

                //For Transaction Log
                base.TRANS_TYPE_ID = 105;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 
                //Added for Itrack-856
                this.DESTINATION_COUNTRY.IsDBParam = false;
                this.DESTINATION_STATE.IsDBParam = false;
                this.ORIGIN_COUNTRY.IsDBParam = false;
                this.ORIGIN_STATE.IsDBParam = false;
                //Added till here 

                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.COMMODITY_ID.IsDBParam = false;
                ProcReturnValue = true;
                returnResult = base.Save();
                returnResult = Proc_ReturnValue;

                this.COMMODITY_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
                 
            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }//public int AddCommodityData()

        /// <summary>
        /// Uset to Update the Commodity info details using commodity id
        /// </summary>
        /// <returns></returns>
        public int UpdateCommodityData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateCommodityInfo";

                //For Transaction Log
                base.TRANS_TYPE_ID = 106;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 

                //Added for Itrack-856
                this.ORIGINAL_VERSION_ID.IsDBParam = false;
                this.DESTINATION_COUNTRY.IsDBParam = false;
                this.DESTINATION_STATE.IsDBParam = false;
                this.ORIGIN_COUNTRY.IsDBParam = false;
                this.ORIGIN_STATE.IsDBParam = false;
                //Added till here 

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
        }//public int UpdateCommodityData()

        /// <summary>
        /// Use to Delete the Commodity Info details using COMMODITY_ID,CUSTOMER_ID,POLICY_ID and POLICY_VERSION_ID
        /// </summary>
        /// <returns></returns>
        public int DeleteCommodityData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Delete_Name = "Proc_DeleteCommodityInfo";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@COMMODITY_ID", COMMODITY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);


                //For Transaction Log
                base.TRANS_TYPE_ID = 107;
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

        }//public int DeleteCommodityData()
        /// <summary>
        /// use to Activate and Deactivate the comoodity info record using COMMODITY_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,IS_ACTIVE
        /// </summary>
        /// <returns></returns>
        public int ActivateDeactivateCommodityData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_ActivateDeactivate_Name = "Proc_ActivateDeactivateCommodityInfo";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@COMMODITY_ID", COMMODITY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);


                //For Transaction Log
                if (IS_ACTIVE.CurrentValue.ToString() == "Y")
                    base.TRANS_TYPE_ID = 108;
                else
                    base.TRANS_TYPE_ID = 109;
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

        }//public int ActivateDeactivateCommodityData()
    }
}
