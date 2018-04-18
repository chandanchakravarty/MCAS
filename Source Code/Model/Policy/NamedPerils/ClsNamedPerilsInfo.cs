
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	22-03-2010
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
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Cms.Model.Policy.NamedPerils
{
    /// <summary>
    /// Database Model for AddNamedPerils.
    /// </summary>
    [Serializable]
    public class ClsNamedPerilsInfo : ClsModelBaseClass
    {

        #region Declare the Type object of every Property

        /// <summary>
        /// Declare the Type object of every Property
        /// </summary>
        /*private EbixInt32 _CUSTOMER_ID= new EbixInt32();
        private EbixInt32 _POLICY_ID=new EbixInt32();
        private EbixInt32 _POLICY_VERSION_ID=new EbixInt32();
        private EbixInt32 _PERIL_ID=new EbixInt32();
        private EbixInt32 _LOCATION = new EbixInt32();
        private EbixString _OCCUPANCY = new EbixString();
        private EbixString _CONSTRUCTION = new EbixString();
        private EbixString _ACTIVITY_TYPE = new EbixString();
        private EbixInt32 _VR = new EbixInt32();
        private EbixInt32 _LMI = new EbixInt32();

        private EbixInt32 _BUILDING = new EbixInt32();
        private EbixInt32 _MRI = new EbixInt32();
        private EbixInt32 _TYPE = new EbixInt32();
        private EbixInt32 _LOSS = new EbixInt32();
        private EbixInt32 _LOYALTY = new EbixInt32();
        private EbixInt32 _PERC_LOYALTY = new EbixInt32();
        private EbixInt32 _DEDUCTIBLE_OPTION = new EbixInt32();
        private EbixString _MULTIPLE_DEDUCTIBLE = new EbixString();
        private EbixString _CATEGORY = new EbixString();
        private EbixString _FIRE_CORPS = new EbixString();

        private EbixInt32 _PUNCTUATION_QUEST = new EbixInt32();
        private EbixString _SHOWER_CLASSIFICATION = new EbixString();
        private EbixInt32 _CREATED_BY = new EbixInt32();
        private EbixDateTime _CREATED_DATETIME = new EbixDateTime();
        private EbixInt32 _MODIFIED_BY = new EbixInt32();
        private EbixDateTime _LAST_UPDATED_DATETIME = new EbixDateTime();
        private EbixString _IS_ACTIVE = new EbixString();
        private EbixString _CORRAL_SYSTEM = new EbixString();
        private EbixString _RAWVALUES = new EbixString();
        private EbixString _REMARKS = new EbixString();

        private EbixString _PARKING_SPACES = new EbixString();
        private EbixString _CLAIM_RATIO = new EbixString();
        private EbixString _RAW_MATERIAL_VALUE = new EbixString();
        private EbixString _CONTENT_VALUE = new EbixString();
        private EbixInt32 _HYDRANTS = new EbixInt32();
        private EbixInt32 _SHOWERS = new EbixInt32();
        private EbixInt32 _S_DETECT_ALARM = new EbixInt32();
        private EbixInt32 _CAR_COMBAT = new EbixInt32();
        private EbixInt32 _S_MANUAL_INERT_GAS = new EbixInt32();
        private EbixInt32 _S_FIXED_INSERT_GAS = new EbixInt32();

        private EbixInt32 _S_FOAM_PER_MANUAL = new EbixInt32();
        private EbixInt32 _S_FIXED_FOAM = new EbixInt32();
        private EbixInt32 _S_FIRE_UNIT = new EbixInt32();
        private EbixInt32 _E_FIRE = new EbixInt32();
        private EbixInt32 _ASSIST24 = new EbixInt32();
         */ 
         
        
        #endregion 

        /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsNamedPerilsInfo()
        {
            //this.SetColumnsName();
            this.PropertyCollection();
        }

       
        #region Declare the every property's parameters name
        /// <summary>
        /// Declare the every property's parameters name 
        /// </summary>
        /* 
        private void SetColumnsName()
        {
            if (ClauseName == base.SelectClause)
            {
                _PERIL_ID.ParamName = "@PERIL_ID";
            }
            if (ClauseName == base.AddClause)
            {
                _CUSTOMER_ID.ParamName          = "@CUSTOMER_ID";
                _POLICY_ID.ParamName            = "@POLICY_ID";
                _POLICY_VERSION_ID.ParamName    = "@POLICY_VERSION_ID";

                _PERIL_ID.ParamName             = "@PERIL_ID";

                _LOCATION.ParamName             = "@LOCATION";
                _OCCUPANCY.ParamName            = "@OCCUPANCY";
                _CONSTRUCTION.ParamName         = "@CONSTRUCTION";
                _ACTIVITY_TYPE.ParamName        = "@ACTIVITY_TYPE";
                _VR.ParamName                   = "@VR";
                _LMI.ParamName                  = "@LMI";
                _BUILDING.ParamName             = "@BUILDING";
                _MRI.ParamName                  = "@MRI";
                _TYPE.ParamName                 = "@TYPE";
                _LOSS.ParamName                 = "@LOSS";
                _LOYALTY.ParamName              = "@LOYALTY";
                _PERC_LOYALTY.ParamName         = "@PERC_LOYALTY";
                _DEDUCTIBLE_OPTION.ParamName    = "@DEDUCTIBLE_OPTION";
                _MULTIPLE_DEDUCTIBLE.ParamName  = "@MULTIPLE_DEDUCTIBLE";
                _CATEGORY.ParamName             = "@CATEGORY";
                _FIRE_CORPS.ParamName           = "@FIRE_CORPS";
                _PUNCTUATION_QUEST.ParamName    = "@PUNCTUATION_QUEST";
                _SHOWER_CLASSIFICATION.ParamName= "@SHOWER_CLASSIFICATION";
                _IS_ACTIVE.ParamName            = "@IS_ACTIVE";

                _CREATED_BY.ParamName           = "@CREATED_BY";
                _CREATED_DATETIME.ParamName     = "@CREATED_DATETIME";

                _CORRAL_SYSTEM.ParamName        = "@CORRAL_SYSTEM";
                _RAWVALUES.ParamName            = "@RAWVALUES";
                _REMARKS.ParamName              = "@REMARKS";
                _PARKING_SPACES.ParamName       = "@PARKING_SPACES";
                _CLAIM_RATIO.ParamName          = "@CLAIM_RATIO";
                _RAW_MATERIAL_VALUE.ParamName   = "@RAW_MATERIAL_VALUE";
                _CONTENT_VALUE.ParamName        = "@CONTENT_VALUE";
                _HYDRANTS.ParamName             = "@HYDRANTS";
                _SHOWERS.ParamName              = "@SHOWERS";
                _S_DETECT_ALARM.ParamName       = "@S_DETECT_ALARM";
                _CAR_COMBAT.ParamName           = "@CAR_COMBAT";
                _S_MANUAL_INERT_GAS.ParamName   = "@S_MANUAL_INERT_GAS";
                _S_FIXED_INSERT_GAS.ParamName   = "@S_FIXED_INSERT_GAS";
                _S_FOAM_PER_MANUAL.ParamName    = "@S_FOAM_PER_MANUAL";
                _S_FIXED_FOAM.ParamName         = "@S_FIXED_FOAM";
                _S_FIRE_UNIT.ParamName          = "@S_FIRE_UNIT";
                _E_FIRE.ParamName               = "@E_FIRE";
                _ASSIST24.ParamName             = "@ASSIST24";

            }
            if (ClauseName == base.UpdateClause)
            {
                _PERIL_ID.ParamName             = "@PERIL_ID";

                _LOCATION.ParamName             = "@LOCATION";
                _OCCUPANCY.ParamName            = "@OCCUPANCY";
                _CONSTRUCTION.ParamName         = "@CONSTRUCTION";
                _ACTIVITY_TYPE.ParamName        = "@ACTIVITY_TYPE";
                _VR.ParamName                   = "@VR";
                _LMI.ParamName                  = "@LMI";
                _BUILDING.ParamName             = "@BUILDING";
                _MRI.ParamName                  = "@MRI";
                _TYPE.ParamName                 = "@TYPE";
                _LOSS.ParamName                 = "@LOSS";
                _LOYALTY.ParamName              = "@LOYALTY";
                _PERC_LOYALTY.ParamName         = "@PERC_LOYALTY";
                _DEDUCTIBLE_OPTION.ParamName    = "@DEDUCTIBLE_OPTION";
                _MULTIPLE_DEDUCTIBLE.ParamName  = "@MULTIPLE_DEDUCTIBLE";
                _CATEGORY.ParamName             = "@CATEGORY";
                _FIRE_CORPS.ParamName           = "@FIRE_CORPS";
                _PUNCTUATION_QUEST.ParamName    = "@PUNCTUATION_QUEST";
                _SHOWER_CLASSIFICATION.ParamName= "@SHOWER_CLASSIFICATION";
                _IS_ACTIVE.ParamName            = "@IS_ACTIVE";

                _MODIFIED_BY.ParamName          = "@MODIFIED_BY";
                _LAST_UPDATED_DATETIME.ParamName= "@LAST_UPDATED_DATETIME";

                _CORRAL_SYSTEM.ParamName        = "@CORRAL_SYSTEM";
                _RAWVALUES.ParamName            = "@RAWVALUES";
                _REMARKS.ParamName              = "@REMARKS";
                _PARKING_SPACES.ParamName       = "@PARKING_SPACES";
                _CLAIM_RATIO.ParamName          = "@CLAIM_RATIO";
                _RAW_MATERIAL_VALUE.ParamName   = "@RAW_MATERIAL_VALUE";
                _CONTENT_VALUE.ParamName        = "@CONTENT_VALUE";
                _HYDRANTS.ParamName             = "@HYDRANTS";
                _SHOWERS.ParamName              = "@SHOWERS";
                _S_DETECT_ALARM.ParamName       = "@S_DETECT_ALARM";
                _CAR_COMBAT.ParamName           = "@CAR_COMBAT";
                _S_MANUAL_INERT_GAS.ParamName   = "@S_MANUAL_INERT_GAS";
                _S_FIXED_INSERT_GAS.ParamName   = "@S_FIXED_INSERT_GAS";
                _S_FOAM_PER_MANUAL.ParamName    = "@S_FOAM_PER_MANUAL";
                _S_FIXED_FOAM.ParamName         = "@S_FIXED_FOAM";
                _S_FIRE_UNIT.ParamName          = "@S_FIRE_UNIT";
                _E_FIRE.ParamName               = "@E_FIRE";
                _ASSIST24.ParamName             = "@ASSIST24";

            }
           
            if (ClauseName == base.ActivateDeactivateClause)
            {
                _PERIL_ID.ParamName             = "@PERIL_ID";
                _CUSTOMER_ID.ParamName          = "@CUSTOMER_ID";
                _POLICY_ID.ParamName            = "@POLICY_ID";
                _POLICY_VERSION_ID.ParamName    = "@POLICY_VERSION_ID";
                _IS_ACTIVE.ParamName            = "@IS_ACTIVE";
            }
            if (ClauseName == base.DeleteClause)
            {
                _PERIL_ID.ParamName = "@PERIL_ID";
                _CUSTOMER_ID.ParamName = "@CUSTOMER_ID";
                _POLICY_ID.ParamName = "@POLICY_ID";
                _POLICY_VERSION_ID.ParamName = "@POLICY_VERSION_ID";
                 
            }

        }//private void SetColumnsName()
        */
        #endregion

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
                base.htPropertyCollection.Add("PERIL_ID", PERIL_ID);
                base.htPropertyCollection.Add("LOCATION", LOCATION);
                base.htPropertyCollection.Add("OCCUPANCY", OCCUPANCY);
                base.htPropertyCollection.Add("CONSTRUCTION", CONSTRUCTION);
                base.htPropertyCollection.Add("ACTIVITY_TYPE", ACTIVITY_TYPE);
                base.htPropertyCollection.Add("VR", VR);
                base.htPropertyCollection.Add("LMI", LMI);
                base.htPropertyCollection.Add("BUILDING", BUILDING);
                base.htPropertyCollection.Add("MRI", MRI);
                base.htPropertyCollection.Add("TYPE", TYPE);
                base.htPropertyCollection.Add("LOSS", LOSS);
                base.htPropertyCollection.Add("LOYALTY", LOYALTY);
                base.htPropertyCollection.Add("PERC_LOYALTY", PERC_LOYALTY);
                base.htPropertyCollection.Add("DEDUCTIBLE_OPTION", DEDUCTIBLE_OPTION);
                base.htPropertyCollection.Add("MULTIPLE_DEDUCTIBLE", MULTIPLE_DEDUCTIBLE);
                base.htPropertyCollection.Add("CATEGORY", CATEGORY);
                base.htPropertyCollection.Add("RAWVALUES", RAWVALUES);
                base.htPropertyCollection.Add("REMARKS", REMARKS);
                base.htPropertyCollection.Add("PARKING_SPACES", PARKING_SPACES);
                base.htPropertyCollection.Add("CLAIM_RATIO", CLAIM_RATIO);
                base.htPropertyCollection.Add("RAW_MATERIAL_VALUE", RAW_MATERIAL_VALUE);
                base.htPropertyCollection.Add("CONTENT_VALUE", CONTENT_VALUE);
                base.htPropertyCollection.Add("ASSIST24", ASSIST24);
                base.htPropertyCollection.Add("BONUS", BONUS);
                base.htPropertyCollection.Add("CO_APPLICANT_ID", CO_APPLICANT_ID);
                base.htPropertyCollection.Add("LOCATION_NUMBER", LOCATION_NUMBER);
                base.htPropertyCollection.Add("ITEM_NUMBER", ITEM_NUMBER);
                base.htPropertyCollection.Add("ACTUAL_INSURED_OBJECT", ACTUAL_INSURED_OBJECT);
                base.htPropertyCollection.Add("ORIGINAL_VERSION_ID", ORIGINAL_VERSION_ID);
                base.htPropertyCollection.Add("EXCEEDED_PREMIUM", EXCEEDED_PREMIUM);
            //}
            
        }//private void PropertyCollection()s
       

        #endregion
        
        #region Declare the Property for every data table columns

        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 CUSTOMER_ID
        {
            get { 
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]) == null ? new EbixInt32("CUSTOMER_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]).CurrentValue =Convert.ToInt32(value);
            }
        }//public EbixInt32 CUSTOMER_ID 
       
        public EbixInt32 POLICY_ID
        {
            get { 
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]) == null ? new EbixInt32("POLICY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 POLICY_ID
        
        public EbixInt32 POLICY_VERSION_ID
        {
            get { //return _POLICY_VERSION_ID;
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
        
        public EbixInt32 PERIL_ID
        {
            get {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERIL_ID"]) == null ? new EbixInt32("PERIL_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERIL_ID"]); 
            }
            set
            {
                //_POLICY_ID.CurrentValue = Convert.ToInt32(value);
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERIL_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 PERIL_ID
        
        public EbixInt32 LOCATION
        {
            get { //return _LOCATION;
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION"]) == null ? new EbixInt32("LOCATION") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION"]); 
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOCATION"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 LOCATION
        
        public EbixString OCCUPANCY
        {
            get {// return _OCCUPANCY;
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OCCUPANCY"]) == null ? new EbixString("OCCUPANCY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OCCUPANCY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OCCUPANCY"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString OCCUPANCY
        
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
        
        public EbixString ACTIVITY_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ACTIVITY_TYPE"]) == null ? new EbixString("ACTIVITY_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ACTIVITY_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ACTIVITY_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }// public EbixString ACTIVITY_TYPE
        
        public EbixDouble VR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VR"]) == null ? new EbixDouble("VR") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VR"]); 
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["VR"]).CurrentValue = Convert.ToDouble(value);

            }
        }//public EbixDouble VR

        public EbixDouble LMI
        {
            get { return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LMI"]) == null ? new EbixDouble("LMI") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LMI"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LMI"]).CurrentValue = Convert.ToDouble(value);
            }

        }//public EbixDouble LMI
        //Change From Ebixint32 to EbixDouble itrack - 440 by Pradeep on 22-March-2011
        public EbixDouble BUILDING
        {
            get { return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BUILDING"]) == null ? new EbixDouble("BUILDING") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BUILDING"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BUILDING"]).CurrentValue = Convert.ToDouble(value);
            }
        }// public EbixInt32 BUILDING

        public EbixDouble MRI
        {
            get { return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MRI"]) == null ? new EbixDouble("MRI") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MRI"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["MRI"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixDouble MRI

        public EbixInt32 TYPE
        {
            get { return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE"]) == null ? new EbixInt32("TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 TYPE
        //Change From Ebixint32 to EbixDouble itrack - 440 by Pradeep on 22-March-2011
        public EbixDouble LOSS
        {
            get { return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LOSS"]) == null ? new EbixDouble("LOSS") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LOSS"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LOSS"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixInt32 LOSS

        public EbixInt32 LOYALTY
        {
            get { return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOYALTY"]) == null ? new EbixInt32("LOYALTY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOYALTY"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["LOYALTY"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 LOYALTY

        public EbixInt32 PERC_LOYALTY
        {
            get { return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERC_LOYALTY"]) == null ? new EbixInt32("PERC_LOYALTY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERC_LOYALTY"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PERC_LOYALTY"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 PERC_LOYALTY

        public EbixInt32 DEDUCTIBLE_OPTION
        {
            get { return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DEDUCTIBLE_OPTION"]) == null ? new EbixInt32("DEDUCTIBLE_OPTION") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DEDUCTIBLE_OPTION"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DEDUCTIBLE_OPTION"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 DEDUCTIBLE_OPTION

        public EbixString MULTIPLE_DEDUCTIBLE
        {
            get { return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MULTIPLE_DEDUCTIBLE"]) == null ? new EbixString("MULTIPLE_DEDUCTIBLE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MULTIPLE_DEDUCTIBLE"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MULTIPLE_DEDUCTIBLE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString MULTIPLE_DEDUCTIBLE

        public EbixString CATEGORY
        {
            get { return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CATEGORY"]) == null ? new EbixString("CATEGORY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CATEGORY"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CATEGORY"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString MULTIPLE_DEDUCTIBLE

        public EbixString RAWVALUES
        {
            get { return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RAWVALUES"]) == null ? new EbixString("RAWVALUES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RAWVALUES"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RAWVALUES"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString RAWVALUE

        public EbixString REMARKS
        {
            get { return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REMARKS"]) == null ? new EbixString("REMARKS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REMARKS"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REMARKS"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString REMARKS

        public EbixString PARKING_SPACES
        {
            get { return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PARKING_SPACES"]) == null ? new EbixString("PARKING_SPACES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PARKING_SPACES"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PARKING_SPACES"]).CurrentValue = Convert.ToString(value);
            }
        }// public EbixString PARKING_SPACES

        public EbixDouble CLAIM_RATIO
        {
            get { return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CLAIM_RATIO"]) == null ? new EbixDouble("CLAIM_RATIO") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CLAIM_RATIO"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CLAIM_RATIO"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixString CLAIM_RATIO

        public EbixDouble BONUS
        {
            get { return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BONUS"]) == null ? new EbixDouble("BONUS") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BONUS"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["BONUS"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixString BONUS

        public EbixString RAW_MATERIAL_VALUE
        {
            get { return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RAW_MATERIAL_VALUE"]) == null ? new EbixString("RAW_MATERIAL_VALUE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RAW_MATERIAL_VALUE"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RAW_MATERIAL_VALUE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString RAW_MATERIAL_VALUE

        public EbixString CONTENT_VALUE
        {
            get { return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONTENT_VALUE"]) == null ? new EbixString("CONTENT_VALUE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONTENT_VALUE"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONTENT_VALUE"]).CurrentValue = Convert.ToString(value);
            }
        }//public EbixString CONTENT_VALUE

        public EbixInt32 ASSIST24
        {
            get { return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ASSIST24"]) == null ? new EbixInt32("ASSIST24") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ASSIST24"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ASSIST24"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixInt32 CO_APPLICANT_ID
        {
            get { return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CO_APPLICANT_ID"]) == null ? new EbixInt32("CO_APPLICANT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CO_APPLICANT_ID"]); }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CO_APPLICANT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

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
        
        //public EbixInt32 ASSIST24

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
        }// public EbixString ACTUAL_INSURED_OBJECT

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
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetLocationNumNAddress(Int32 CustomerID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetLoactionNumNAddress";
                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CustomerID);
                ds = base.GetData();
            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }// public DataSet GetLocationNumNAddress()
        
        /// <summary>
        /// To get the Building Name,Address,Number,Compliment,District and City from the Pol_location 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <returns></returns>
        public DataSet GetLocationDetailsForNamedPerils(Int32 CustomerID, Int32 PolicyID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetLocationDetailsForNamedPerils";
                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CustomerID);
                base.htGetDataParamCollections.Add("@POLICY_ID", PolicyID);
                ds = base.GetData();
            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }// public DataSet GetLocationDetailsForNamedPerils(Int32 CustomerID, Int32 PolicyID)

        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetNamedPerilsDatainfo";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@PERIL_ID",PERIL_ID.CurrentValue);
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
        public int AddNamedParilsData()
        {
            int returnResult = 0;
            try
            {
                 
                base.Proc_Add_Name = "Proc_InsertNamedPerils";
                
                base.ReturnIDName = "@PERIL_ID";
                
                //For Transaction Log
                base.ProcReturnValue = true;
              
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY =CREATED_BY.CurrentValue ;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                 
                //end 

                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.PERIL_ID.IsDBParam = false;

                returnResult = base.Save();

                this.PERIL_ID.CurrentValue =base.ReturnIDNameValue; //get the out parameter
                returnResult = Proc_ReturnValue;
            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally{ }
            return returnResult;
        }//public int AddNamedParilsData()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int UpdateNamedParilsData()
        {
            int returnValue = 0;
            try
            {
                 

                base.Proc_Update_Name = "Proc_UpdateNamedPerils";

                base.ProcReturnValue = true;
                //For Transaction Log
                
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
               
                //end 
                this.ORIGINAL_VERSION_ID.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CO_APPLICANT_ID.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;    
                 
                returnValue = base.Update();
                returnValue = Proc_ReturnValue;

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
        public int DeleteNamedParilsData(Int32 ConfirmValue)
        {
            int returnValue=0;
            try
            {
                 
                base.Proc_Delete_Name = "Proc_DeleteNamedPerils";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@PERIL_ID", PERIL_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID",POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION.CurrentValue);
                base.htGetDataParamCollections.Add("@CONFIRMVALUE", ConfirmValue);

                //For Transaction Log
      
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                 
                //end 
                ProcReturnValue = true;
                returnValue = base.Delete();
                returnValue = Proc_ReturnValue;
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
        public int ActivateDeactivateNamedPerils()
        {
            int returnValue = 0;
            try
            {
                 
                base.Proc_ActivateDeactivate_Name = "Proc_ActivateDeactivateNamedPeril";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@PERIL_ID", PERIL_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);
                base.htGetDataParamCollections.Add("@LOCATION_NUMBER", LOCATION_NUMBER.CurrentValue);
                base.htGetDataParamCollections.Add("@ITEM_NUMBER", ITEM_NUMBER.CurrentValue);


                //For Transaction Log
                
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                base.ProcReturnValue = true;
                //end 

                returnValue = base.ActivateDeactivate();
                returnValue = Proc_ReturnValue;

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;
 
        }
        
         

    }
}

