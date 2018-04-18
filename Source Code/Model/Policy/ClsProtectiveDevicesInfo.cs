
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	19-04-2010
<End Date			: -	
<Description		: - Protection Devices Tab
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
    /// <summary>
    /// Database Model for Protection Devices Tab
    /// </summary>
    [Serializable]
    public class ClsProtectiveDevicesInfo:ClsModelBaseClass
    {
        /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsProtectiveDevicesInfo()
        { this.PropertyCollection(); }

         #region Delare the add the parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);

            base.htPropertyCollection.Add("RISK_ID", RISK_ID);
            base.htPropertyCollection.Add("PROTECTIVE_DEVICE_ID", PROTECTIVE_DEVICE_ID);
            base.htPropertyCollection.Add("FIRE_EXTINGUISHER", FIRE_EXTINGUISHER);
            base.htPropertyCollection.Add("SPL_FIRE_EXTINGUISHER_UNIT", SPL_FIRE_EXTINGUISHER_UNIT);
            base.htPropertyCollection.Add("MANUAL_FOAM_SYSTEM", MANUAL_FOAM_SYSTEM);
            base.htPropertyCollection.Add("FOAM", FOAM);

            base.htPropertyCollection.Add("INERT_GAS_SYSTEM", INERT_GAS_SYSTEM);
            base.htPropertyCollection.Add("MANUAL_INERT_GAS_SYSTEM", MANUAL_INERT_GAS_SYSTEM);
            base.htPropertyCollection.Add("COMBAT_CARS", COMBAT_CARS);
            base.htPropertyCollection.Add("CORRAL_SYSTEM", CORRAL_SYSTEM);
            base.htPropertyCollection.Add("ALARM_SYSTEM", ALARM_SYSTEM);
            base.htPropertyCollection.Add("FREE_HYDRANT", FREE_HYDRANT);

            base.htPropertyCollection.Add("SPRINKLERS", SPRINKLERS);
            base.htPropertyCollection.Add("SPRINKLERS_CLASSIFICATION", SPRINKLERS_CLASSIFICATION);
            base.htPropertyCollection.Add("FIRE_FIGHTER", FIRE_FIGHTER);
            base.htPropertyCollection.Add("QUESTIION_POINTS", QUESTIION_POINTS);
            base.htPropertyCollection.Add("LOCATION_ID", LOCATION_ID);
            
        }

        #endregion

        #region Declare the Property for every data table columns

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

        public EbixInt32 RISK_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]) == null ? new EbixInt32("RISK_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]);
            }
            set
            {
                 
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 RISK_ID

        public EbixInt32 PROTECTIVE_DEVICE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROTECTIVE_DEVICE_ID"]) == null ? new EbixInt32("PROTECTIVE_DEVICE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROTECTIVE_DEVICE_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PROTECTIVE_DEVICE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }// public EbixInt32 PROTECTIVE_DEVICE_ID

        public EbixInt32 FIRE_EXTINGUISHER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FIRE_EXTINGUISHER"]) == null ? new EbixInt32("FIRE_EXTINGUISHER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FIRE_EXTINGUISHER"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FIRE_EXTINGUISHER"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 FIRE_EXTINGUISHER

        public EbixInt32 SPL_FIRE_EXTINGUISHER_UNIT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SPL_FIRE_EXTINGUISHER_UNIT"]) == null ? new EbixInt32("SPL_FIRE_EXTINGUISHER_UNIT") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SPL_FIRE_EXTINGUISHER_UNIT"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SPL_FIRE_EXTINGUISHER_UNIT"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 SPL_FIRE_EXTINGUISHER_UNIT

        public EbixInt32 MANUAL_FOAM_SYSTEM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUAL_FOAM_SYSTEM"]) == null ? new EbixInt32("MANUAL_FOAM_SYSTEM") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUAL_FOAM_SYSTEM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUAL_FOAM_SYSTEM"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 MANUAL_FOAM_SYSTEM

        public EbixInt32 FOAM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FOAM"]) == null ? new EbixInt32("FOAM") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FOAM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FOAM"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 FOAM

        public EbixInt32 INERT_GAS_SYSTEM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INERT_GAS_SYSTEM"]) == null ? new EbixInt32("INERT_GAS_SYSTEM") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INERT_GAS_SYSTEM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INERT_GAS_SYSTEM"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 INERT_GAS_SYSTEM

        public EbixInt32 MANUAL_INERT_GAS_SYSTEM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUAL_INERT_GAS_SYSTEM"]) == null ? new EbixInt32("MANUAL_INERT_GAS_SYSTEM") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUAL_INERT_GAS_SYSTEM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MANUAL_INERT_GAS_SYSTEM"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 MANUAL_INERT_GAS_SYSTEM

        public EbixInt32 COMBAT_CARS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMBAT_CARS"]) == null ? new EbixInt32("COMBAT_CARS") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMBAT_CARS"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMBAT_CARS"]).CurrentValue = Convert.ToInt32(value);
            }
        }// public EbixInt32 COMBAT_CARS

        public EbixInt32 CORRAL_SYSTEM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CORRAL_SYSTEM"]) == null ? new EbixInt32("CORRAL_SYSTEM") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CORRAL_SYSTEM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CORRAL_SYSTEM"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 CORRAL_SYSTEM

        public EbixInt32 ALARM_SYSTEM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ALARM_SYSTEM"]) == null ? new EbixInt32("ALARM_SYSTEM") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ALARM_SYSTEM"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ALARM_SYSTEM"]).CurrentValue = Convert.ToInt32(value);
            }
        }// public EbixInt32 ALARM_SYSTEM

        public EbixInt32 FREE_HYDRANT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FREE_HYDRANT"]) == null ? new EbixInt32("FREE_HYDRANT") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FREE_HYDRANT"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["FREE_HYDRANT"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 FREE_HYDRANT

        public EbixInt32 SPRINKLERS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SPRINKLERS"]) == null ? new EbixInt32("SPRINKLERS") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SPRINKLERS"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SPRINKLERS"]).CurrentValue = Convert.ToInt32(value);
            }

        }//public EbixInt32 SPRINKLERS

        public EbixString SPRINKLERS_CLASSIFICATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SPRINKLERS_CLASSIFICATION"]) == null ? new EbixString("SPRINKLERS_CLASSIFICATION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SPRINKLERS_CLASSIFICATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["SPRINKLERS_CLASSIFICATION"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString SPRINKLERS_CLASSIFICATION

        public EbixString FIRE_FIGHTER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FIRE_FIGHTER"]) == null ? new EbixString("FIRE_FIGHTER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FIRE_FIGHTER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["FIRE_FIGHTER"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString FIRE_FIGHTER

        public EbixDouble QUESTIION_POINTS
        {
            get { return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["QUESTIION_POINTS"]) == null ? new EbixDouble("QUESTIION_POINTS") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["QUESTIION_POINTS"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["QUESTIION_POINTS"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble QUESTIION_POINTS

        public EbixInt32 LOCATION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION_ID"]) == null ? new EbixInt32("LOCATION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 RISK_ID
        #endregion

        /// <summary>
        /// Use to get the Protective device info data
        /// </summary>
        /// <returns></returns>
        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetProtectiveDeviceInfoData";
                base.htGetDataParamCollections.Clear();



                base.htGetDataParamCollections.Add("@RISK_ID", GetIntNullFromNegative(RISK_ID.CurrentValue));
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LOCATION_ID",GetIntNullFromNegative( LOCATION_ID.CurrentValue));
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }//public DataSet FetchData(int Protective_device_ID)

        public static object GetIntNullFromNegative(int intValue)
        {
            if (intValue == -1 || intValue == Int32.MinValue)
            {
                return System.DBNull.Value;
            }
            else
            {
                return intValue;
            }

        }


        /// <summary>
        /// Use to insert Protective Device information
        /// </summary>
        /// <returns>int</returns>
        public int AddProtectiveDeviceData()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertProtectiveDevice";

                base.ReturnIDName = "@PROTECTIVE_DEVICE_ID"; // //Set the out parameter

                //For Transaction Log
                base.TRANS_TYPE_ID = 119;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 


                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.PROTECTIVE_DEVICE_ID.IsDBParam = false;

                returnResult = base.Save();

                this.PROTECTIVE_DEVICE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }// public int AddProtectiveDeviceData()

        /// <summary>
        /// Use to Update Protective Device information using PROTECTIVE_DEVICE_ID
        /// </summary>
        /// <returns></returns>
        public int UpdateProtectiveDeviceData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateProtectiveDevice";

                //For Transaction Log
                base.TRANS_TYPE_ID = 121;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 

                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;

                returnValue = base.Update();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }//public int UpdateProtectiveDeviceData()

        /// <summary>
        ///  Use to Delete Protective Device information
        /// </summary>
        /// <returns></returns>
        public int DeleteProtectiveDeviceData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Delete_Name = "Proc_DeleteProtectiveDevice";
                base.htGetDataParamCollections.Clear();
                
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@RISK_ID", RISK_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PROTECTIVE_DEVICE_ID", PROTECTIVE_DEVICE_ID.CurrentValue);

                //For Transaction Log
                base.TRANS_TYPE_ID = 120;
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

        }//public int DeleteProtectiveDeviceData()

        /// <summary>
        ///  Use to Activate and Deactivate Protective Device information based on the some keys 
        /// </summary>
        /// <returns></returns>
        public int ActivateDeactivateProtectiveDeviceData()
        {
            int returnValue = 0;
            try
            {

                base.Proc_ActivateDeactivate_Name = "Proc_ActivateDeactivateProtectiveDevice";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@RISK_ID", RISK_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PROTECTIVE_DEVICE_ID", PROTECTIVE_DEVICE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);


                //For Transaction Log
                if (IS_ACTIVE.CurrentValue.ToString() == "Y")
                    base.TRANS_TYPE_ID = 122;
                else
                    base.TRANS_TYPE_ID = 123;

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

        }//public int ActivateDeactivateProtectiveDeviceData()
    }
}
