
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
    /// <summary>
    /// Database Model for Risk Information.
    /// </summary>
    [Serializable]
    public class ClsRiskInfo : ClsModelBaseClass
    {

        #region Declare the Type object of every Property

      


        #endregion

        /// <summary>
        /// Initialize the default value 
        /// </summary>
        public ClsRiskInfo()
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

            base.htPropertyCollection.Add("INSURED_PRODUCT_ID", INSURED_PRODUCT_ID);
            base.htPropertyCollection.Add("CLAIM_ID", CLAIM_ID);

            base.htPropertyCollection.Add("POL_RISK_ID", POL_RISK_ID);
            //base.htPropertyCollection.Add("POL_VEHICLE_ID", POL_VEHICLE_ID);
            base.htPropertyCollection.Add("YEAR", YEAR);
            base.htPropertyCollection.Add("VEHICLE_INSURED_PLEADED_GUILTY", VEHICLE_INSURED_PLEADED_GUILTY);
            base.htPropertyCollection.Add("VEHICLE_MAKER", VEHICLE_MAKER);
            base.htPropertyCollection.Add("VEHICLE_MODEL", VEHICLE_MODEL);
            base.htPropertyCollection.Add("VEHICLE_VIN", VEHICLE_VIN);
            base.htPropertyCollection.Add("DAMAGE_DESCRIPTION", DAMAGE_DESCRIPTION);
            
            base.htPropertyCollection.Add("VESSEL_TYPE", VESSEL_TYPE);
            base.htPropertyCollection.Add("VESSEL_NAME", VESSEL_NAME);
   
            base.htPropertyCollection.Add("VESSEL_MANUFACTURER", VESSEL_MANUFACTURER);
           // base.htPropertyCollection.Add("POL_LOCATION_ID", POL_LOCATION_ID);
            base.htPropertyCollection.Add("LOCATION_ADDRESS", LOCATION_ADDRESS);
            base.htPropertyCollection.Add("LOCATION_COMPLIMENT", LOCATION_COMPLIMENT);
            base.htPropertyCollection.Add("LOCATION_DISTRICT", LOCATION_DISTRICT);
            base.htPropertyCollection.Add("LOCATION_ZIPCODE", LOCATION_ZIPCODE);
           
          //  base.htPropertyCollection.Add("POL_VOYAGE_ID", POL_VOYAGE_ID);
            base.htPropertyCollection.Add("VOYAGE_CONVEYENCE_TYPE", VOYAGE_CONVEYENCE_TYPE);
            base.htPropertyCollection.Add("VOYAGE_DEPARTURE_DATE", VOYAGE_DEPARTURE_DATE);
           
           // base.htPropertyCollection.Add("POL_PERSON_ID", POL_PERSON_ID);
            base.htPropertyCollection.Add("INSURED_NAME", INSURED_NAME);
            base.htPropertyCollection.Add("EFFECTIVE_DATE", EFFECTIVE_DATE);
            base.htPropertyCollection.Add("EXPIRE_DATE", EXPIRE_DATE);
            // FOR Itrack 651
            base.htPropertyCollection.Add("LICENCE_PLATE_NUMBER", LICENCE_PLATE_NUMBER);
            base.htPropertyCollection.Add("DAMAGE_TYPE", DAMAGE_TYPE);
            base.htPropertyCollection.Add("PERSON_DOB", PERSON_DOB);
            base.htPropertyCollection.Add("PERSON_DISEASE_DATE", PERSON_DISEASE_DATE);
            base.htPropertyCollection.Add("VOYAGE_CERT_NUMBER", VOYAGE_CERT_NUMBER);
            
            base.htPropertyCollection.Add("VOYAGE_PREFIX", VOYAGE_PREFIX);
            base.htPropertyCollection.Add("VESSEL_NUMBER", VESSEL_NUMBER);
            base.htPropertyCollection.Add("VOYAGE_TRAN_COMPANY", VOYAGE_TRAN_COMPANY);
            base.htPropertyCollection.Add("VOYAGE_IO_DESC", VOYAGE_IO_DESC);
            base.htPropertyCollection.Add("VOYAGE_ARRIVAL_DATE", VOYAGE_ARRIVAL_DATE);
            base.htPropertyCollection.Add("VOYAGE_SURVEY_DATE", VOYAGE_SURVEY_DATE);
            base.htPropertyCollection.Add("ITEM_NUMBER", ITEM_NUMBER);  
             base.htPropertyCollection.Add("RURAL_INSURED_AREA", RURAL_INSURED_AREA);
             base.htPropertyCollection.Add("RURAL_PROPERTY", RURAL_PROPERTY);
             base.htPropertyCollection.Add("RURAL_CULTIVATION", RURAL_CULTIVATION);
             base.htPropertyCollection.Add("RURAL_FESR_COVERAGE", RURAL_FESR_COVERAGE);
             base.htPropertyCollection.Add("RURAL_MODE", RURAL_MODE);
             base.htPropertyCollection.Add("RURAL_SUBSIDY_PREMIUM", RURAL_SUBSIDY_PREMIUM);
             base.htPropertyCollection.Add("PA_NUM_OF_PASS", PA_NUM_OF_PASS);
             base.htPropertyCollection.Add("DP_TICKET_NUMBER", DP_TICKET_NUMBER);
             base.htPropertyCollection.Add("DP_CATEGORY", DP_CATEGORY);

             base.htPropertyCollection.Add("STATE1", STATE1);
             base.htPropertyCollection.Add("STATE2", STATE2);
             base.htPropertyCollection.Add("COUNTRY1", COUNTRY1);
             base.htPropertyCollection.Add("COUNTRY2", COUNTRY2);
             base.htPropertyCollection.Add("CITY1", CITY1);
             base.htPropertyCollection.Add("CITY2", CITY2);
             base.htPropertyCollection.Add("ACTUAL_INSURED_OBJECT", ACTUAL_INSURED_OBJECT);
             base.htPropertyCollection.Add("RISK_CO_APP_ID", RISK_CO_APP_ID);


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

        public EbixInt32 INSURED_PRODUCT_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSURED_PRODUCT_ID"]) == null ? new EbixInt32("INSURED_PRODUCT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSURED_PRODUCT_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["INSURED_PRODUCT_ID"]).CurrentValue = Convert.ToInt32(value);
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


        public EbixInt32 POL_RISK_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POL_RISK_ID"]) == null ? new EbixInt32("POL_RISK_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POL_RISK_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POL_RISK_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 YEAR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["YEAR"]) == null ? new EbixInt32("YEAR") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["YEAR"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["YEAR"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString VEHICLE_INSURED_PLEADED_GUILTY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_INSURED_PLEADED_GUILTY"]) == null ? new EbixString("VEHICLE_INSURED_PLEADED_GUILTY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_INSURED_PLEADED_GUILTY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_INSURED_PLEADED_GUILTY"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString VEHICLE_MAKER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_MAKER"]) == null ? new EbixString("VEHICLE_MAKER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_MAKER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_MAKER"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString VEHICLE_MODEL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_MODEL"]) == null ? new EbixString("VEHICLE_MODEL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_MODEL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_MODEL"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString VEHICLE_VIN
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_VIN"]) == null ? new EbixString("VEHICLE_VIN") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_VIN"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VEHICLE_VIN"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString DAMAGE_DESCRIPTION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DAMAGE_DESCRIPTION"]) == null ? new EbixString("DAMAGE_DESCRIPTION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DAMAGE_DESCRIPTION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DAMAGE_DESCRIPTION"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString VESSEL_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_TYPE"]) == null ? new EbixString("VESSEL_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString VESSEL_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_NAME"]) == null ? new EbixString("VESSEL_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }

       

        public EbixString VESSEL_MANUFACTURER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_MANUFACTURER"]) == null ? new EbixString("VESSEL_MANUFACTURER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_MANUFACTURER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_MANUFACTURER"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString LOCATION_ADDRESS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_ADDRESS"]) == null ? new EbixString("LOCATION_ADDRESS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_ADDRESS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_ADDRESS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString LOCATION_COMPLIMENT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_COMPLIMENT"]) == null ? new EbixString("LOCATION_COMPLIMENT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_COMPLIMENT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_COMPLIMENT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString LOCATION_DISTRICT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_DISTRICT"]) == null ? new EbixString("LOCATION_DISTRICT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_DISTRICT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_DISTRICT"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString LOCATION_ZIPCODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_ZIPCODE"]) == null ? new EbixString("LOCATION_ZIPCODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_ZIPCODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOCATION_ZIPCODE"]).CurrentValue = Convert.ToString(value);
            }
        }

       
               
        public EbixString VOYAGE_CONVEYENCE_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_CONVEYENCE_TYPE"]) == null ? new EbixString("VOYAGE_CONVEYENCE_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_CONVEYENCE_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_CONVEYENCE_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDateTime VOYAGE_DEPARTURE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["VOYAGE_DEPARTURE_DATE"]) == null ? new EbixDateTime("VOYAGE_DEPARTURE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["VOYAGE_DEPARTURE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["VOYAGE_DEPARTURE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

      
        public EbixString INSURED_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["INSURED_NAME"]) == null ? new EbixString("INSURED_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["INSURED_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["INSURED_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixDateTime EFFECTIVE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_DATE"]) == null ? new EbixDateTime("EFFECTIVE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EFFECTIVE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }

        public EbixDateTime EXPIRE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EXPIRE_DATE"]) == null ? new EbixDateTime("EXPIRE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EXPIRE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["EXPIRE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }
    
        public EbixInt32 DAMAGE_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DAMAGE_TYPE"]) == null ? new EbixInt32("DAMAGE_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DAMAGE_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DAMAGE_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixDateTime PERSON_DOB
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["PERSON_DOB"]) == null ? new EbixDateTime("PERSON_DOB") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["PERSON_DOB"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["PERSON_DOB"]).CurrentValue = Convert.ToDateTime(value);
            }
        }
        public EbixDateTime PERSON_DISEASE_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["PERSON_DISEASE_DATE"]) == null ? new EbixDateTime("PERSON_DISEASE_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["PERSON_DISEASE_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["PERSON_DISEASE_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }
        public EbixString VOYAGE_CERT_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_CERT_NUMBER"]) == null ? new EbixString("VOYAGE_CERT_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_CERT_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_CERT_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString LICENCE_PLATE_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LICENCE_PLATE_NUMBER"]) == null ? new EbixString("LICENCE_PLATE_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LICENCE_PLATE_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LICENCE_PLATE_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }
     
        public EbixString VOYAGE_PREFIX
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_PREFIX"]) == null ? new EbixString("VOYAGE_PREFIX") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_PREFIX"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_PREFIX"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString VESSEL_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_NUMBER"]) == null ? new EbixString("VESSEL_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VESSEL_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString VOYAGE_TRAN_COMPANY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_TRAN_COMPANY"]) == null ? new EbixString("VOYAGE_TRAN_COMPANY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_TRAN_COMPANY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_TRAN_COMPANY"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString VOYAGE_IO_DESC
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_IO_DESC"]) == null ? new EbixString("VOYAGE_IO_DESC") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_IO_DESC"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["VOYAGE_IO_DESC"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixDateTime VOYAGE_ARRIVAL_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["VOYAGE_ARRIVAL_DATE"]) == null ? new EbixDateTime("VOYAGE_ARRIVAL_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["VOYAGE_ARRIVAL_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["VOYAGE_ARRIVAL_DATE"]).CurrentValue = Convert.ToDateTime(value);
            }
        }
        public EbixDateTime VOYAGE_SURVEY_DATE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["VOYAGE_SURVEY_DATE"]) == null ? new EbixDateTime("VOYAGE_SURVEY_DATE") : ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["VOYAGE_SURVEY_DATE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDateTime)htPropertyCollection["VOYAGE_SURVEY_DATE"]).CurrentValue = Convert.ToDateTime(value);
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
        public EbixInt32 RURAL_INSURED_AREA
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_INSURED_AREA"]) == null ? new EbixInt32("RURAL_INSURED_AREA") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_INSURED_AREA"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_INSURED_AREA"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 RURAL_PROPERTY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_PROPERTY"]) == null ? new EbixInt32("RURAL_PROPERTY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_PROPERTY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_PROPERTY"]).CurrentValue = Convert.ToInt32(value);
            }
        }
				
        public EbixInt32 RURAL_CULTIVATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_CULTIVATION"]) == null ? new EbixInt32("RURAL_CULTIVATION") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_CULTIVATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_CULTIVATION"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixInt32 RURAL_FESR_COVERAGE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_FESR_COVERAGE"]) == null ? new EbixInt32("RURAL_FESR_COVERAGE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_FESR_COVERAGE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_FESR_COVERAGE"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 RURAL_MODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_MODE"]) == null ? new EbixInt32("RURAL_MODE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_MODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RURAL_MODE"]).CurrentValue = Convert.ToInt32(value);
            }
        }
       
        public EbixDouble RURAL_SUBSIDY_PREMIUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RURAL_SUBSIDY_PREMIUM"]) == null ? new EbixDouble("RURAL_SUBSIDY_PREMIUM") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RURAL_SUBSIDY_PREMIUM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RURAL_SUBSIDY_PREMIUM"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixDouble PA_NUM_OF_PASS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PA_NUM_OF_PASS"]) == null ? new EbixDouble("PA_NUM_OF_PASS") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PA_NUM_OF_PASS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PA_NUM_OF_PASS"]).CurrentValue = Convert.ToDouble(value);
            }
        }


       
        public EbixInt32 DP_TICKET_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DP_TICKET_NUMBER"]) == null ? new EbixInt32("DP_TICKET_NUMBER") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DP_TICKET_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DP_TICKET_NUMBER"]).CurrentValue = Convert.ToInt32(value);
            }
        }
      
        public EbixInt32 DP_CATEGORY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DP_CATEGORY"]) == null ? new EbixInt32("DP_CATEGORY") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DP_CATEGORY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DP_CATEGORY"]).CurrentValue = Convert.ToInt32(value);
            }
        }

      

       

        public EbixString STATE1
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATE1"]) == null ? new EbixString("STATE1") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATE1"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATE1"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString STATE2
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATE2"]) == null ? new EbixString("STATE2") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATE2"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["STATE2"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString COUNTRY1
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COUNTRY1"]) == null ? new EbixString("COUNTRY1") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COUNTRY1"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COUNTRY1"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString COUNTRY2
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COUNTRY2"]) == null ? new EbixString("COUNTRY2") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COUNTRY2"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["COUNTRY2"]).CurrentValue = Convert.ToString(value);
            }
        }

   


        
        public EbixString CITY1
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY1"]) == null ? new EbixString("CITY1") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY1"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY1"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString CITY2
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY2"]) == null ? new EbixString("CITY2") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY2"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CITY2"]).CurrentValue = Convert.ToString(value);
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
        }

        public EbixInt32 RISK_CO_APP_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_CO_APP_ID"]) == null ? new EbixInt32("RISK_CO_APP_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_CO_APP_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_CO_APP_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        #endregion

        /// <summary>
        /// To get the location id and location details to display in dropdown
        /// </summary>
        /// <param name="ClaimID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public DataSet GetRiskTypes(Int32 LobID,Int32 ClaimID, Int32 CustomerID, Int32 PolicyID, Int32 PolicyVersionID,int LangId)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetRiskTypes";
                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@LOB_ID", LobID);
                base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CustomerID);
                base.htGetDataParamCollections.Add("@POLICTY_ID", PolicyID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", PolicyVersionID);
                base.htGetDataParamCollections.Add("@LANG_ID", LangId);
                
                ds = base.GetData();
            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }



        /// <summary>
        /// To get the location id and location details to display in dropdown
        /// </summary>
        /// <param name="ClaimID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public DataSet GetRiskTypeDetails(Int32 ClaimID,Int32 LobID, Int32 RiskID, Int32 CustomerID, Int32 PolicyID, Int32 PolicyVersionID)
        {
            DataSet ds = null;
            try
            {
                base.Proc_FetchData = "Proc_GetRiskTypeDetails";
                base.htGetDataParamCollections.Clear();

                base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
                base.htGetDataParamCollections.Add("@LOB_ID", LobID);
                base.htGetDataParamCollections.Add("@RISK_ID", RiskID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CustomerID);
                base.htGetDataParamCollections.Add("@POLICTY_ID", PolicyID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", PolicyVersionID);
                ds = base.GetData();
            }//try
            catch (Exception ex)
            { throw (ex); }//catch (Exception ex)
            return ds;
        }

        public int AddRiskInformation()
        {
            int returnResult = 0;
            try
            {
                this.TRANS_TYPE_ID = 290;
                base.Proc_Add_Name = "Proc_InsertRiskInformation";

                base.ReturnIDName = "@INSURED_PRODUCT_ID";

             
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 
                this.INSURED_PRODUCT_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.RISK_CO_APP_ID.IsDBParam = true;
              //  this.PERIL_ID.IsDBParam = false;

                returnResult = base.Save();

                this.INSURED_PRODUCT_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }


        public int UpdateRiskInformation()
        {
            int returnValue = 0;
            try
            {
                //For Transaction Log               
                this.TRANS_TYPE_ID = 291;
                base.Proc_Update_Name = "Proc_UpdateRiskInformation";
                base.ReturnIDName = "@ERROR_CODE";
                
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 

                this.CREATED_BY.IsDBParam = false;
                this.CLAIM_ID.IsDBParam = true;
                this.CUSTOMER_ID.IsDBParam = false;
                this.POLICY_ID.IsDBParam = false;
                this.POLICY_VERSION_ID.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;
                this.RISK_CO_APP_ID.IsDBParam = false;
                returnValue = base.Update();

                if (base.ReturnIDNameValue != 0)
                    returnValue = base.ReturnIDNameValue; //get the out parameter

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnValue;
        }


        public DataSet FetchData()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetRiskInformation";
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
        
         

    }
}

