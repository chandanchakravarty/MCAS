/******************************************************************************************
<Author					: - Sneha
<Start Date				: -	25/11/2011
<End Date				: -	
<Description			: - 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Xml;
using Cms.EbixDataLayer;


namespace Cms.Model.Policy
{
  [Serializable]
  public  class ClsContractorsDetailInfo : ClsModelBaseClass
    {
        public ClsContractorsDetailInfo()
        {
            this.PropertyCollection();
        }
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("LOCATION_ID", LOCATION_ID);
            base.htPropertyCollection.Add("PREMISES_ID", PREMISES_ID);
            base.htPropertyCollection.Add("CONTRACTOR_ID", CONTRACTOR_ID);
            base.htPropertyCollection.Add("TYP_CONTRACTOR", TYP_CONTRACTOR);
            base.htPropertyCollection.Add("YR_EXP", YR_EXP);
            base.htPropertyCollection.Add("CONT_LICENSE", CONT_LICENSE);
            base.htPropertyCollection.Add("LICENSE_HOLDER", LICENSE_HOLDER);
            base.htPropertyCollection.Add("LMT_CONTRACTOR_OCC", LMT_CONTRACTOR_OCC);
            base.htPropertyCollection.Add("LMT_CONTRACTOR_AGG", LMT_CONTRACTOR_AGG);
            base.htPropertyCollection.Add("TOT_CST_PST_YR", TOT_CST_PST_YR);
            base.htPropertyCollection.Add("IS_EXPL_ENVRNT", IS_EXPL_ENVRNT);
            base.htPropertyCollection.Add("IS_FIRE_ALARM", IS_FIRE_ALARM);
            base.htPropertyCollection.Add("IS_HOSPITALS", IS_HOSPITALS);
            base.htPropertyCollection.Add("IS_EXP_ENVRNT", IS_EXP_ENVRNT);
            base.htPropertyCollection.Add("IS_SWIMMING_POOL", IS_SWIMMING_POOL);
            base.htPropertyCollection.Add("IS_BRG_ALARM", IS_BRG_ALARM);
            base.htPropertyCollection.Add("IS_PWR_PLANTS", IS_PWR_PLANTS);
            base.htPropertyCollection.Add("IS_BCK_EQUIPMNT", IS_BCK_EQUIPMNT);
            base.htPropertyCollection.Add("IS_LIVE_WIRES", IS_LIVE_WIRES);
            base.htPropertyCollection.Add("IS_ARPT_CONSTRCT", IS_ARPT_CONSTRCT);
            base.htPropertyCollection.Add("IS_HIGH_VOLTAGE", IS_HIGH_VOLTAGE);
            base.htPropertyCollection.Add("IS_TRAFFIC_WRK", IS_TRAFFIC_WRK);
            base.htPropertyCollection.Add("IS_LND_FILL", IS_LND_FILL);
            base.htPropertyCollection.Add("IS_DAM_CONSTRNT", IS_DAM_CONSTRNT);
            base.htPropertyCollection.Add("IS_REFINERY", IS_REFINERY);
            base.htPropertyCollection.Add("IS_HZD_MATERIAL", IS_HZD_MATERIAL);
            base.htPropertyCollection.Add("IS_PETRO_PLNT", IS_PETRO_PLNT);
            base.htPropertyCollection.Add("IS_NUCL_PLNT", IS_NUCL_PLNT);
            base.htPropertyCollection.Add("IS_PWR_LINES", IS_PWR_LINES);
            base.htPropertyCollection.Add("DRW_PLANS", DRW_PLANS);
            base.htPropertyCollection.Add("OPR_BLASTING", OPR_BLASTING);
            base.htPropertyCollection.Add("OPR_TRENCHING", OPR_TRENCHING);
            base.htPropertyCollection.Add("OPR_EXCAVACATION", OPR_EXCAVACATION);
            base.htPropertyCollection.Add("IS_SECTY_POLICY", IS_SECTY_POLICY);
            base.htPropertyCollection.Add("ANY_DEMOLITION", ANY_DEMOLITION);
            base.htPropertyCollection.Add("ANY_CRANES", ANY_CRANES);
            base.htPropertyCollection.Add("PERCENT_ROOFING", PERCENT_ROOFING);
            base.htPropertyCollection.Add("ANY_LESS_LMTS", ANY_LESS_LMTS);
            base.htPropertyCollection.Add("ANY_SHOP_WRK", ANY_SHOP_WRK);
            base.htPropertyCollection.Add("PERCENT_RENOVATION", PERCENT_RENOVATION);
            base.htPropertyCollection.Add("ANY_GUTTING", ANY_GUTTING);
            base.htPropertyCollection.Add("PERCENT_SNOWPLOWING", PERCENT_SNOWPLOWING);
            base.htPropertyCollection.Add("ANY_WRK_LOAD", ANY_WRK_LOAD);
            base.htPropertyCollection.Add("PERCENT_PNTG_OUTSIDE", PERCENT_PNTG_OUTSIDE);
            base.htPropertyCollection.Add("PERCENT_PNTG_INSIDE", PERCENT_PNTG_INSIDE);
            base.htPropertyCollection.Add("ANY_PNTG_TNKS", ANY_PNTG_TNKS);
            base.htPropertyCollection.Add("PERCENT_PNTG_SPRY", PERCENT_PNTG_SPRY);
            base.htPropertyCollection.Add("ANY_EPOXIES", ANY_EPOXIES);
            base.htPropertyCollection.Add("ANY_ACID", ANY_ACID);
            base.htPropertyCollection.Add("ANY_LEASE_EQUIPMNT", ANY_LEASE_EQUIPMNT);
            base.htPropertyCollection.Add("ANY_BOATS_OWND", ANY_BOATS_OWND);
            base.htPropertyCollection.Add("DRCT_SIGHT_WRK_IN_PRGRSS", DRCT_SIGHT_WRK_IN_PRGRSS);
            base.htPropertyCollection.Add("PRDCT_SOLD_IN_APPL_NAME", PRDCT_SOLD_IN_APPL_NAME);
            base.htPropertyCollection.Add("UTILITY_CMPNY_CALLED", UTILITY_CMPNY_CALLED);
            base.htPropertyCollection.Add("TYP_IN_DGGN_PRCSS", TYP_IN_DGGN_PRCSS);
            base.htPropertyCollection.Add("PERMIT_OBTAINED", PERMIT_OBTAINED);
            base.htPropertyCollection.Add("PERCENT_SPRINKLE_WRK", PERCENT_SPRINKLE_WRK);
            base.htPropertyCollection.Add("ANY_EXCAVAION", ANY_EXCAVAION);
            base.htPropertyCollection.Add("ANY_PEST_SPRAY", ANY_PEST_SPRAY);
            base.htPropertyCollection.Add("PERCENT_TREE_TRIMNG", PERCENT_TREE_TRIMNG);
            base.htPropertyCollection.Add("ANY_WRK_OFFSEASON", ANY_WRK_OFFSEASON);
            base.htPropertyCollection.Add("ANY_MIX_TRANSIT", ANY_MIX_TRANSIT);
            base.htPropertyCollection.Add("ANY_CONTSRUCTION_WRK", ANY_CONTSRUCTION_WRK);
            base.htPropertyCollection.Add("ANY_WRK_ABVE_THREE_STORIES", ANY_WRK_ABVE_THREE_STORIES);
            base.htPropertyCollection.Add("ANY_SCAFHOLDING_ABVE_TWELVE_FEET", ANY_SCAFHOLDING_ABVE_TWELVE_FEET);
            base.htPropertyCollection.Add("ANY_PNTG_TOWERS", ANY_PNTG_TOWERS);
            base.htPropertyCollection.Add("ANY_SPRAY_GUNS", ANY_SPRAY_GUNS);
            base.htPropertyCollection.Add("ANY_REMOVAL_DONE", ANY_REMOVAL_DONE);
            base.htPropertyCollection.Add("ANY_WAXING_FLOORS", ANY_WAXING_FLOORS);
            base.htPropertyCollection.Add("PER_RESIDENT", PER_RESIDENT);
            base.htPropertyCollection.Add("PER_COMMERICAL", PER_COMMERICAL);
            base.htPropertyCollection.Add("PER_CONST", PER_CONST);
            base.htPropertyCollection.Add("PER_REMODEL", PER_REMODEL);
            base.htPropertyCollection.Add("MAJOR_ELECT", MAJOR_ELECT);
            base.htPropertyCollection.Add("CARRY_LIMITS", CARRY_LIMITS);

        }

      
        public EbixInt32 PREMISES_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PREMISES_ID"]) == null ? new EbixInt32("PREMISES_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PREMISES_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PREMISES_ID"]).CurrentValue = Convert.ToInt32(value);
            }

        }


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

        }

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
        }

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
        }

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
        }

        public EbixInt32 CONTRACTOR_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACTOR_ID"]) == null ? new EbixInt32("CONTRACTOR_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACTOR_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACTOR_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString TYP_CONTRACTOR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_CONTRACTOR"]) == null ? new EbixString("TYP_CONTRACTOR") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_CONTRACTOR"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_CONTRACTOR"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString YR_EXP
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["YR_EXP"]) == null ? new EbixString("YR_EXP") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["YR_EXP"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["YR_EXP"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString CONT_LICENSE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONT_LICENSE"]) == null ? new EbixString("CONT_LICENSE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONT_LICENSE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CONT_LICENSE"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString LICENSE_HOLDER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LICENSE_HOLDER"]) == null ? new EbixString("LICENSE_HOLDER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LICENSE_HOLDER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LICENSE_HOLDER"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString LMT_CONTRACTOR_OCC
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LMT_CONTRACTOR_OCC"]) == null ? new EbixString("LMT_CONTRACTOR_OCC") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LMT_CONTRACTOR_OCC"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LMT_CONTRACTOR_OCC"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString LMT_CONTRACTOR_AGG
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LMT_CONTRACTOR_AGG"]) == null ? new EbixString("LMT_CONTRACTOR_AGG") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LMT_CONTRACTOR_AGG"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LMT_CONTRACTOR_AGG"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixDouble TOT_CST_PST_YR
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOT_CST_PST_YR"]) == null ? new EbixDouble("TOT_CST_PST_YR") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOT_CST_PST_YR"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["TOT_CST_PST_YR"]).CurrentValue = Convert.ToDouble(value);
            }
        }
        public EbixString IS_EXPL_ENVRNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_EXPL_ENVRNT"]) == null ? new EbixString("IS_EXPL_ENVRNT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_EXPL_ENVRNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_EXPL_ENVRNT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_FIRE_ALARM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_FIRE_ALARM"]) == null ? new EbixString("IS_FIRE_ALARM") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_FIRE_ALARM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_FIRE_ALARM"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_HOSPITALS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_HOSPITALS"]) == null ? new EbixString("IS_HOSPITALS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_HOSPITALS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_HOSPITALS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_EXP_ENVRNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_EXP_ENVRNT"]) == null ? new EbixString("IS_EXP_ENVRNT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_EXP_ENVRNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_EXP_ENVRNT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_SWIMMING_POOL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_SWIMMING_POOL"]) == null ? new EbixString("IS_SWIMMING_POOL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_SWIMMING_POOL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_SWIMMING_POOL"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_BRG_ALARM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_BRG_ALARM"]) == null ? new EbixString("IS_BRG_ALARM") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_BRG_ALARM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_BRG_ALARM"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_PWR_PLANTS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PWR_PLANTS"]) == null ? new EbixString("IS_PWR_PLANTS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PWR_PLANTS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PWR_PLANTS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_BCK_EQUIPMNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_BCK_EQUIPMNT"]) == null ? new EbixString("IS_BCK_EQUIPMNT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_BCK_EQUIPMNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_BCK_EQUIPMNT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_LIVE_WIRES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_LIVE_WIRES"]) == null ? new EbixString("IS_LIVE_WIRES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_LIVE_WIRES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_LIVE_WIRES"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_ARPT_CONSTRCT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_ARPT_CONSTRCT"]) == null ? new EbixString("IS_ARPT_CONSTRCT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_ARPT_CONSTRCT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_ARPT_CONSTRCT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_HIGH_VOLTAGE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_HIGH_VOLTAGE"]) == null ? new EbixString("IS_HIGH_VOLTAGE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_HIGH_VOLTAGE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_HIGH_VOLTAGE"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_TRAFFIC_WRK
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_TRAFFIC_WRK"]) == null ? new EbixString("IS_TRAFFIC_WRK") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_TRAFFIC_WRK"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_TRAFFIC_WRK"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_LND_FILL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_LND_FILL"]) == null ? new EbixString("IS_LND_FILL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_LND_FILL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_LND_FILL"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_DAM_CONSTRNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_DAM_CONSTRNT"]) == null ? new EbixString("IS_DAM_CONSTRNT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_DAM_CONSTRNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_DAM_CONSTRNT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_REFINERY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_REFINERY"]) == null ? new EbixString("IS_REFINERY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_REFINERY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_REFINERY"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_HZD_MATERIAL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_HZD_MATERIAL"]) == null ? new EbixString("IS_HZD_MATERIAL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_HZD_MATERIAL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_HZD_MATERIAL"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_PETRO_PLNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PETRO_PLNT"]) == null ? new EbixString("IS_PETRO_PLNT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PETRO_PLNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PETRO_PLNT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_NUCL_PLNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_NUCL_PLNT"]) == null ? new EbixString("IS_NUCL_PLNT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_NUCL_PLNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_NUCL_PLNT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_PWR_LINES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PWR_LINES"]) == null ? new EbixString("IS_PWR_LINES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PWR_LINES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_PWR_LINES"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString DRW_PLANS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DRW_PLANS"]) == null ? new EbixString("DRW_PLANS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DRW_PLANS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DRW_PLANS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString OPR_BLASTING
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPR_BLASTING"]) == null ? new EbixString("OPR_BLASTING") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPR_BLASTING"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPR_BLASTING"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString OPR_TRENCHING
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPR_TRENCHING"]) == null ? new EbixString("OPR_TRENCHING") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPR_TRENCHING"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPR_TRENCHING"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString OPR_EXCAVACATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPR_EXCAVACATION"]) == null ? new EbixString("OPR_EXCAVACATION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPR_EXCAVACATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["OPR_EXCAVACATION"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString IS_SECTY_POLICY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_SECTY_POLICY"]) == null ? new EbixString("IS_SECTY_POLICY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_SECTY_POLICY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["IS_SECTY_POLICY"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_DEMOLITION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_DEMOLITION"]) == null ? new EbixString("ANY_DEMOLITION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_DEMOLITION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_DEMOLITION"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_CRANES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_CRANES"]) == null ? new EbixString("ANY_CRANES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_CRANES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_CRANES"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PERCENT_ROOFING
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_ROOFING"]) == null ? new EbixString("PERCENT_ROOFING") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_ROOFING"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_ROOFING"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_LESS_LMTS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_LESS_LMTS"]) == null ? new EbixString("ANY_LESS_LMTS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_LESS_LMTS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_LESS_LMTS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_SHOP_WRK
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_SHOP_WRK"]) == null ? new EbixString("ANY_SHOP_WRK") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_SHOP_WRK"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_SHOP_WRK"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PERCENT_RENOVATION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_RENOVATION"]) == null ? new EbixString("PERCENT_RENOVATION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_RENOVATION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_RENOVATION"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_GUTTING
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_GUTTING"]) == null ? new EbixString("ANY_GUTTING") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_GUTTING"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_GUTTING"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PERCENT_SNOWPLOWING
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_SNOWPLOWING"]) == null ? new EbixString("PERCENT_SNOWPLOWING") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_SNOWPLOWING"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_SNOWPLOWING"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_WRK_LOAD
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WRK_LOAD"]) == null ? new EbixString("ANY_WRK_LOAD") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WRK_LOAD"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WRK_LOAD"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PERCENT_PNTG_OUTSIDE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_PNTG_OUTSIDE"]) == null ? new EbixString("PERCENT_PNTG_OUTSIDE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_PNTG_OUTSIDE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_PNTG_OUTSIDE"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PERCENT_PNTG_INSIDE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_PNTG_INSIDE"]) == null ? new EbixString("PERCENT_PNTG_INSIDE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_PNTG_INSIDE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_PNTG_INSIDE"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_PNTG_TNKS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_PNTG_TNKS"]) == null ? new EbixString("ANY_PNTG_TNKS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_PNTG_TNKS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_PNTG_TNKS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PERCENT_PNTG_SPRY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_PNTG_SPRY"]) == null ? new EbixString("PERCENT_PNTG_SPRY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_PNTG_SPRY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_PNTG_SPRY"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_EPOXIES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_EPOXIES"]) == null ? new EbixString("ANY_EPOXIES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_EPOXIES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_EPOXIES"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_ACID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_ACID"]) == null ? new EbixString("ANY_ACID") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_ACID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_ACID"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_LEASE_EQUIPMNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_LEASE_EQUIPMNT"]) == null ? new EbixString("ANY_LEASE_EQUIPMNT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_LEASE_EQUIPMNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_LEASE_EQUIPMNT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_BOATS_OWND
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_BOATS_OWND"]) == null ? new EbixString("ANY_BOATS_OWND") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_BOATS_OWND"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_BOATS_OWND"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString DRCT_SIGHT_WRK_IN_PRGRSS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DRCT_SIGHT_WRK_IN_PRGRSS"]) == null ? new EbixString("DRCT_SIGHT_WRK_IN_PRGRSS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DRCT_SIGHT_WRK_IN_PRGRSS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DRCT_SIGHT_WRK_IN_PRGRSS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PRDCT_SOLD_IN_APPL_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PRDCT_SOLD_IN_APPL_NAME"]) == null ? new EbixString("PRDCT_SOLD_IN_APPL_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PRDCT_SOLD_IN_APPL_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PRDCT_SOLD_IN_APPL_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString UTILITY_CMPNY_CALLED
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["UTILITY_CMPNY_CALLED"]) == null ? new EbixString("UTILITY_CMPNY_CALLED") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["UTILITY_CMPNY_CALLED"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["UTILITY_CMPNY_CALLED"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString TYP_IN_DGGN_PRCSS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_IN_DGGN_PRCSS"]) == null ? new EbixString("TYP_IN_DGGN_PRCSS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_IN_DGGN_PRCSS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["TYP_IN_DGGN_PRCSS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PERMIT_OBTAINED
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERMIT_OBTAINED"]) == null ? new EbixString("PERMIT_OBTAINED") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERMIT_OBTAINED"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERMIT_OBTAINED"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PERCENT_SPRINKLE_WRK
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_SPRINKLE_WRK"]) == null ? new EbixString("PERCENT_SPRINKLE_WRK") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_SPRINKLE_WRK"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_SPRINKLE_WRK"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_EXCAVAION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_EXCAVAION"]) == null ? new EbixString("ANY_EXCAVAION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_EXCAVAION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_EXCAVAION"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_PEST_SPRAY
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_PEST_SPRAY"]) == null ? new EbixString("ANY_PEST_SPRAY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_PEST_SPRAY"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_PEST_SPRAY"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PERCENT_TREE_TRIMNG
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_TREE_TRIMNG"]) == null ? new EbixString("PERCENT_TREE_TRIMNG") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_TREE_TRIMNG"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERCENT_TREE_TRIMNG"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_WRK_OFFSEASON
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WRK_OFFSEASON"]) == null ? new EbixString("ANY_WRK_OFFSEASON") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WRK_OFFSEASON"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WRK_OFFSEASON"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_MIX_TRANSIT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_MIX_TRANSIT"]) == null ? new EbixString("ANY_MIX_TRANSIT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_MIX_TRANSIT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_MIX_TRANSIT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_CONTSRUCTION_WRK
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_CONTSRUCTION_WRK"]) == null ? new EbixString("ANY_CONTSRUCTION_WRK") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_CONTSRUCTION_WRK"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_CONTSRUCTION_WRK"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_WRK_ABVE_THREE_STORIES
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WRK_ABVE_THREE_STORIES"]) == null ? new EbixString("ANY_WRK_ABVE_THREE_STORIES") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WRK_ABVE_THREE_STORIES"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WRK_ABVE_THREE_STORIES"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_SCAFHOLDING_ABVE_TWELVE_FEET
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_SCAFHOLDING_ABVE_TWELVE_FEET"]) == null ? new EbixString("ANY_SCAFHOLDING_ABVE_TWELVE_FEET") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_SCAFHOLDING_ABVE_TWELVE_FEET"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_SCAFHOLDING_ABVE_TWELVE_FEET"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_PNTG_TOWERS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_PNTG_TOWERS"]) == null ? new EbixString("ANY_PNTG_TOWERS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_PNTG_TOWERS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_PNTG_TOWERS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_SPRAY_GUNS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_SPRAY_GUNS"]) == null ? new EbixString("ANY_SPRAY_GUNS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_SPRAY_GUNS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_SPRAY_GUNS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_REMOVAL_DONE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_REMOVAL_DONE"]) == null ? new EbixString("ANY_REMOVAL_DONE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_REMOVAL_DONE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_REMOVAL_DONE"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString ANY_WAXING_FLOORS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WAXING_FLOORS"]) == null ? new EbixString("ANY_WAXING_FLOORS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WAXING_FLOORS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ANY_WAXING_FLOORS"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PER_RESIDENT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_RESIDENT"]) == null ? new EbixString("PER_RESIDENT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_RESIDENT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_RESIDENT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PER_COMMERICAL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_COMMERICAL"]) == null ? new EbixString("PER_COMMERICAL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_COMMERICAL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_COMMERICAL"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PER_CONST
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_CONST"]) == null ? new EbixString("PER_CONST") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_CONST"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_CONST"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString PER_REMODEL
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_REMODEL"]) == null ? new EbixString("PER_REMODEL") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_REMODEL"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PER_REMODEL"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString MAJOR_ELECT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MAJOR_ELECT"]) == null ? new EbixString("MAJOR_ELECT") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MAJOR_ELECT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["MAJOR_ELECT"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString CARRY_LIMITS
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CARRY_LIMITS"]) == null ? new EbixString("CARRY_LIMITS") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CARRY_LIMITS"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CARRY_LIMITS"]).CurrentValue = Convert.ToString(value);
            }
        }

        public int AddConrtDetailADD()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_Insert_POL_SUP_FORM_CONTRACTOR";
                base.ProcReturnValue = true;

                base.ReturnIDName = "@CONTRACTOR_ID";
                base.TRANS_DESC = "Contractor Detail Added";
                base.RECORDED_BY = CREATED_BY.CurrentValue;

                this.CONTRACTOR_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                this.ProcReturnValue = true;
                returnResult = Proc_ReturnValue;
                returnResult = base.Save();

                this.CONTRACTOR_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }

        public int updateConrtDetail()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "Proc_UPDATE_POL_SUP_FORM_CONTRACTOR";
                base.TRANS_DESC = "Contractor Detail Updated";
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.IS_ACTIVE.IsDBParam = false;
                returnValue = base.Update();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }

        public DataSet FetchData(Int32 CONTRACTOR_ID, Int32 CUSTOMER_ID, Int32 POLICY_ID, Int32 POLICY_VERSION_ID, Int32 LOCATION_ID, Int32 PREMISES_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GET_POL_SUP_FORM_CONTRACTOR";
                base.htGetDataParamCollections.Clear();

                this.IS_ACTIVE.IsDBParam = false;

                base.htGetDataParamCollections.Add("@CONTRACTOR_ID", CONTRACTOR_ID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID);
                
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }

        public DataSet FetchId(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string PREMISES_ID, string LOCATION_ID)
        {
            DataSet dsCount = null;

            try
            {

                base.Proc_FetchData = "Proc_FETCH_POL_SUP_FORM_CONTRACTOR";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID);
                dsCount = base.GetData();
                this.CONTRACTOR_ID.CurrentValue = base.ReturnIDNameValue;

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }


        public int DelConrtDetail()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Delete_Name = "DELETE_POL_SUP_FORM_CONTRACTOR";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@LOCATION_ID", LOCATION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@PREMISES_ID", PREMISES_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CONTRACTOR_ID", CONTRACTOR_ID.CurrentValue);
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.TRANS_DESC = "Constractor Detail Deleted";


                returnResult = base.Delete();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }
    }
}
