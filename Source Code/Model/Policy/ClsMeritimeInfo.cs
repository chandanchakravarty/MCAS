
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	09-04-2010
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


namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsMeritimeInfo:ClsModelBaseClass
    {
        public ClsMeritimeInfo()
        { this.PropertyCollection(); }
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {

            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID",POLICY_VERSION_ID);
            base.htPropertyCollection.Add("MARITIME_ID", MARITIME_ID);
            base.htPropertyCollection.Add("VESSEL_NUMBER",VESSEL_NUMBER);
            base.htPropertyCollection.Add("NAME_OF_VESSEL",NAME_OF_VESSEL);
            base.htPropertyCollection.Add("TYPE_OF_VESSEL",TYPE_OF_VESSEL);
            base.htPropertyCollection.Add("MANUFACTURE_YEAR",MANUFACTURE_YEAR);
            base.htPropertyCollection.Add("MANUFACTURER",MANUFACTURER);
            base.htPropertyCollection.Add("BUILDER",BUILDER);

            base.htPropertyCollection.Add("CONSTRUCTION",CONSTRUCTION);
            base.htPropertyCollection.Add("PROPULSION",PROPULSION);
            base.htPropertyCollection.Add("CLASSIFICATION",CLASSIFICATION);
            base.htPropertyCollection.Add("LOCAL_OPERATION",LOCAL_OPERATION);
            base.htPropertyCollection.Add("LIMIT_NAVIGATION",LIMIT_NAVIGATION);
            base.htPropertyCollection.Add("PORT_REGISTRATION",PORT_REGISTRATION);
            base.htPropertyCollection.Add("REGISTRATION_NUMBER",REGISTRATION_NUMBER);
            base.htPropertyCollection.Add("TIE_NUMBER",TIE_NUMBER);
            base.htPropertyCollection.Add("VESSEL_ACTION_NAUTICO_CLUB",VESSEL_ACTION_NAUTICO_CLUB);
            base.htPropertyCollection.Add("NAME_OF_CLUB",NAME_OF_CLUB);

            base.htPropertyCollection.Add("LOCAL_CLUB",LOCAL_CLUB);
            base.htPropertyCollection.Add("NUMBER_OF_CREW",NUMBER_OF_CREW);
            base.htPropertyCollection.Add("NUMBER_OF_PASSENGER",NUMBER_OF_PASSENGER);
            base.htPropertyCollection.Add("REMARKS", REMARKS);
            base.htPropertyCollection.Add("CO_APPLICANT_ID", CO_APPLICANT_ID);
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

        public EbixInt32 MARITIME_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MARITIME_ID"]) == null ? new EbixInt32("MARITIME_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MARITIME_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MARITIME_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 MARITIME_ID

        public EbixDouble VESSEL_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VESSEL_NUMBER"]) == null ? new EbixDouble("VESSEL_NUMBER") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VESSEL_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VESSEL_NUMBER"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixString VESSEL_NUMBER

        public EbixString NAME_OF_VESSEL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME_OF_VESSEL"]) == null ? new EbixString("NAME_OF_VESSEL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME_OF_VESSEL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME_OF_VESSEL"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString NAME_OF_VESSEL

        public EbixString TYPE_OF_VESSEL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYPE_OF_VESSEL"]) == null ? new EbixString("TYPE_OF_VESSEL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYPE_OF_VESSEL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYPE_OF_VESSEL"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString TYPE_OF_VESSEL

        public EbixInt32 MANUFACTURE_YEAR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUFACTURE_YEAR"]) == null ? new EbixInt32("MANUFACTURE_YEAR") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUFACTURE_YEAR"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUFACTURE_YEAR"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 MANUFACTURE_YEAR

        public EbixString MANUFACTURER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MANUFACTURER"]) == null ? new EbixString("MANUFACTURER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MANUFACTURER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MANUFACTURER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString MANUFACTURER

        public EbixString BUILDER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BUILDER"]) == null ? new EbixString("BUILDER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BUILDER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["BUILDER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString BUILDER

        public EbixString CONSTRUCTION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONSTRUCTION"]) == null ? new EbixString("CONSTRUCTION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONSTRUCTION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONSTRUCTION"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString CONSTRUCTION

        public EbixString PROPULSION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROPULSION"]) == null ? new EbixString("PROPULSION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROPULSION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PROPULSION"]).CurrentValue = Convert.ToString(value);
            }
        }// public EbixString PROPULSION

        public EbixString CLASSIFICATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLASSIFICATION"]) == null ? new EbixString("CLASSIFICATION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLASSIFICATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLASSIFICATION"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString CLASSIFICATION

        public EbixString LOCAL_OPERATION
        {  get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCAL_OPERATION"]) == null ? new EbixString("LOCAL_OPERATION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCAL_OPERATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCAL_OPERATION"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOCAL_OPERATION

        public EbixString LIMIT_NAVIGATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LIMIT_NAVIGATION"]) == null ? new EbixString("LIMIT_NAVIGATION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LIMIT_NAVIGATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LIMIT_NAVIGATION"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LIMIT_NAVIGATION

        public EbixString PORT_REGISTRATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PORT_REGISTRATION"]) == null ? new EbixString("PORT_REGISTRATION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PORT_REGISTRATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PORT_REGISTRATION"]).CurrentValue = Convert.ToString(value);
            }

        }//public EbixString PORT_REGISTRATION

        public EbixString REGISTRATION_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REGISTRATION_NUMBER"]) == null ? new EbixString("REGISTRATION_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REGISTRATION_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REGISTRATION_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString REGISTRATION_NUMBER

        public EbixString TIE_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TIE_NUMBER"]) == null ? new EbixString("TIE_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TIE_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TIE_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString TIE_NUMBER

        public EbixInt32 VESSEL_ACTION_NAUTICO_CLUB
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VESSEL_ACTION_NAUTICO_CLUB"]) == null ? new EbixInt32("VESSEL_ACTION_NAUTICO_CLUB") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VESSEL_ACTION_NAUTICO_CLUB"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VESSEL_ACTION_NAUTICO_CLUB"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 VESSEL_ACTION_NAUTICO_CLUB

        public EbixString NAME_OF_CLUB
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME_OF_CLUB"]) == null ? new EbixString("NAME_OF_CLUB") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME_OF_CLUB"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME_OF_CLUB"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString NAME_OF_CLUB

        public EbixString LOCAL_CLUB
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCAL_CLUB"]) == null ? new EbixString("LOCAL_CLUB") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCAL_CLUB"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCAL_CLUB"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString LOCAL_CLUB

        public EbixInt32 NUMBER_OF_CREW
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NUMBER_OF_CREW"]) == null ? new EbixInt32("NUMBER_OF_CREW") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NUMBER_OF_CREW"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NUMBER_OF_CREW"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 NUMBER_OF_CREW

        public EbixInt32 NUMBER_OF_PASSENGER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NUMBER_OF_PASSENGER"]) == null ? new EbixInt32("NUMBER_OF_PASSENGER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NUMBER_OF_PASSENGER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["NUMBER_OF_PASSENGER"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 NUMBER_OF_PASSENGER

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
        }//public EbixString CO_APPLICANT_ID

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

        public DataSet getNameOfVessel()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "PROC_GetNameOfVessel";
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)
        public DataSet getVesselDataatRisk(int VesselID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "PROC_GetDataForVesselatRisk";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@VesselID", VesselID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData(int Peril_ID)
        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetMariTimeDatainfo";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@MARITIME_ID", MARITIME_ID.CurrentValue);
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
        }//public DataSet FetchData(int Peril_ID)

      

        /// <summary>
        /// 
        /// </summary>
        /// <returns>int</returns>
        public int AddMariTimeData()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertMariTime";

                base.ReturnIDName = "@MARITIME_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 88;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                
                //end 
                
                 
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.MARITIME_ID.IsDBParam = false;
                ProcReturnValue = true;
                returnResult = base.Save();
                returnResult = Proc_ReturnValue;

                this.MARITIME_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }//public int AddNamedParilsData()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int UpdateMariTimeData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateMariTime";
                
                //For Transaction Log
                base.TRANS_TYPE_ID = 90;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                 
                //end 
                this.ORIGINAL_VERSION_ID.IsDBParam = false;
                this.CO_APPLICANT_ID.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false; 

                returnValue = base.Update();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }//public int UpdateNamedParilsData()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int DeleteMariTimeData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Delete_Name = "Proc_DeleteMariTime";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@MARITIME_ID", MARITIME_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);


                //For Transaction Log
                base.TRANS_TYPE_ID = 89;
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

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ActivateDeactivateMariTimeData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_ActivateDeactivate_Name = "Proc_ActivateDeactivateMariTime";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@MARITIME_ID", MARITIME_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);


                //For Transaction Log
                if(IS_ACTIVE.CurrentValue.ToString() == "Y")
                    base.TRANS_TYPE_ID = 91;
                else
                    base.TRANS_TYPE_ID = 92;
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

        }
    }
}
