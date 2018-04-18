/******************************************************************************************
<Author				: -		Rajeev
<Start Date			: -		014-11-2011
<End Date			: -	
<Description		: - 	Model Class for shop form
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: -		Insurors
*******************************************************************************************/
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy
{
    /// <summary>
    /// Database Model for POL_SUP_FORM_RESTAURANT
    /// </summary>
    public class ClsSuppFormRestaurantInfo: Cms.Model.ClsCommonModel
    {
        private const string POL_BOP_PREMISES_LOC_DETAILS = "POL_SUP_FORM_RESTAURANT";
        public ClsSuppFormRestaurantInfo()
        {
            base.dtModel.TableName = "POL_SUP_FORM_RESTAURANT";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table POL_SUP_FORM_SHOP
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("LOCATION_ID", typeof(int));
            base.dtModel.Columns.Add("PREMISES_ID", typeof(int));
            base.dtModel.Columns.Add("RESTAURANT_ID", typeof(int));
            
           

            //base.dtModel.Columns.Add("AREA_LEASED", typeof(string));
            base.dtModel.Columns.Add("SEATINGCAPACITY", typeof(int));

            base.dtModel.Columns.Add("BUS_TYP_RESTURANT", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_FM_STYLE", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_NGHT_CLUB", typeof(bool));
          
            base.dtModel.Columns.Add("BUS_TYP_FRNCHSED", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_NT_FRNCHSED", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_SEASONAL", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_YR_ROUND", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_DINNER", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_BNQT_HALL", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_BREKFAST", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_FST_FOOD", typeof(bool));

            base.dtModel.Columns.Add("BUS_TYP_TAVERN", typeof(bool));
            base.dtModel.Columns.Add("BUS_TYP_OTHER", typeof(bool));
            base.dtModel.Columns.Add("STAIRWAYS", typeof(bool));
            base.dtModel.Columns.Add("ELEVATORS", typeof(bool));
            base.dtModel.Columns.Add("ESCALATORS", typeof(bool));
            base.dtModel.Columns.Add("GRILLING", typeof(bool));
            base.dtModel.Columns.Add("FRYING", typeof(bool));
            base.dtModel.Columns.Add("BROILING", typeof(bool));
            base.dtModel.Columns.Add("ROASTING", typeof(bool));
            base.dtModel.Columns.Add("COOKING", typeof(bool));


            base.dtModel.Columns.Add("PRK_TYP_VALET", typeof(bool));
            base.dtModel.Columns.Add("PRK_TYP_PREMISES", typeof(bool));

            
            base.dtModel.Columns.Add("OPR_ON_PREMISES", typeof(bool));
            base.dtModel.Columns.Add("OPR_OFF_PREMISES", typeof(bool));

            base.dtModel.Columns.Add("EMRG_LIGHTS", typeof(bool));
            base.dtModel.Columns.Add("WOOD_STOVE", typeof(bool));
            base.dtModel.Columns.Add("HIST_MARKER", typeof(bool));
            base.dtModel.Columns.Add("EXTNG_SYS_COV_COOKNG", typeof(bool));
            base.dtModel.Columns.Add("EXTNG_SYS_MNT_CNTRCT", typeof(bool));
            base.dtModel.Columns.Add("GAS_OFF_COOKNG", typeof(bool));
            base.dtModel.Columns.Add("HOOD_FILTER_CLND", typeof(bool));
            base.dtModel.Columns.Add("HOOD_DUCTS_EQUIP", typeof(bool));
            base.dtModel.Columns.Add("HOOD_DUCTS_MNT_SCH", typeof(bool));
            base.dtModel.Columns.Add("BC_EXTNG_AVL", typeof(bool));


            //base.dtModel.Columns.Add("RATE_NUM", typeof(int));
            //base.dtModel.Columns.Add("RATE_GRP", typeof(int));
            //base.dtModel.Columns.Add("RATE_TER_NUM", typeof(int));

            base.dtModel.Columns.Add("ADQT_CLEARANCE", typeof(bool));
            base.dtModel.Columns.Add("BEER_SALES", typeof(bool));
            base.dtModel.Columns.Add("WINE_SALES", typeof(bool));
            base.dtModel.Columns.Add("FULL_BAR", typeof(bool));


    
            base.dtModel.Columns.Add("TOT_EXPNS_FOOD_LIQUOR", typeof(double));
            base.dtModel.Columns.Add("TOT_EXPNS_OTHERS", typeof(double));
            base.dtModel.Columns.Add("NET_PROFIT", typeof(double));

            base.dtModel.Columns.Add("ACCNT_PAYABLE", typeof(double));
            base.dtModel.Columns.Add("NOTES_PAYABLE", typeof(double));
            base.dtModel.Columns.Add("BNK_LOANS_PAYABLE", typeof(double));
      




        }
        #region Database schema details
        // model for database field POLICY_ID(int)
        public int POLICY_ID
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_ID"] = value;
            }
        }
        // model for database field POLICY_VERSION_ID(int)
        public int POLICY_VERSION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
            }
        }

        // model for database field CUSTOMER_ID(int)
        public int CUSTOMER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
            }
        }
        // model for database field LOCATION_ID(int)
        public int LOCATION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["LOCATION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOCATION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LOCATION_ID"] = value;
            }
        }
        // model for database field PREMISES_ID(int)
        public int PREMISES_ID
        {
            get
            {
                return base.dtModel.Rows[0]["PREMISES_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PREMISES_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PREMISES_ID"] = value;
            }
        }
        // model for database field LOC_DETAILS_ID(int)
        public int RESTAURANT_ID
        {
            get
            {
                return base.dtModel.Rows[0]["RESTAURANT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RESTAURANT_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["RESTAURANT_ID"] = value;
            }
        }
        public int SEATINGCAPACITY
        {
            get
            {
                return base.dtModel.Rows[0]["SEATINGCAPACITY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SEATINGCAPACITY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["SEATINGCAPACITY"] = value;
            }
        }
        //Below for reference
        public bool FUND_TYPE_SOURCE_D
        {
            get
            {
                return base.dtModel.Rows[0]["FUND_TYPE_SOURCE_D"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["FUND_TYPE_SOURCE_D"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FUND_TYPE_SOURCE_D"] = value;
            }
        }
        //*****************************************BUS_TYP_RESTURANT
                     
        // model for database field DESC_BLDNG(string)
        public bool BUS_TYP_RESTURANT
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_RESTURANT"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_RESTURANT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_RESTURANT"] = value;
            }
        }
       
        // model for database field DESC_OPERTN(string)

        public bool BUS_TYP_FM_STYLE
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_FM_STYLE"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_FM_STYLE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_FM_STYLE"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public bool BUS_TYP_NGHT_CLUB
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_NGHT_CLUB"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_NGHT_CLUB"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_NGHT_CLUB"] = value;
            }
        }

        // model for database field ANN_SALES(string)
        public bool BUS_TYP_FRNCHSED
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_FRNCHSED"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_FRNCHSED"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_FRNCHSED"] = value;
            }
        }

        // model for database field TOT_PAYROLL(string)
        public bool BUS_TYP_NT_FRNCHSED
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_NT_FRNCHSED"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_NT_FRNCHSED"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_NT_FRNCHSED"] = value;
            }
        }

              

        // model for database field LST_ALL_OCCUP(string)
        public bool BUS_TYP_SEASONAL
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_SEASONAL"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_SEASONAL"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_SEASONAL"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BUS_TYP_YR_ROUND
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_YR_ROUND"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_YR_ROUND"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_YR_ROUND"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BUS_TYP_DINNER
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_DINNER"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_DINNER"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_DINNER"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BUS_TYP_BNQT_HALL
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_BNQT_HALL"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_BNQT_HALL"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_BNQT_HALL"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BUS_TYP_BREKFAST
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_BREKFAST"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_BREKFAST"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_BREKFAST"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BUS_TYP_FST_FOOD
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_FST_FOOD"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_FST_FOOD"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_FST_FOOD"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BUS_TYP_TAVERN
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_TAVERN"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_TAVERN"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_TAVERN"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BUS_TYP_OTHER
        {
            get
            {
                return base.dtModel.Rows[0]["BUS_TYP_OTHER"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BUS_TYP_OTHER"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUS_TYP_OTHER"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool STAIRWAYS
        {
            get
            {
                return base.dtModel.Rows[0]["STAIRWAYS"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["STAIRWAYS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["STAIRWAYS"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool ELEVATORS
        {
            get
            {
                return base.dtModel.Rows[0]["ELEVATORS"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["ELEVATORS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ELEVATORS"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool ESCALATORS
        {
            get
            {
                return base.dtModel.Rows[0]["ESCALATORS"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["ESCALATORS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ESCALATORS"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool GRILLING
        {
            get
            {
                return base.dtModel.Rows[0]["GRILLING"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["GRILLING"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["GRILLING"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool FRYING
        {
            get
            {
                return base.dtModel.Rows[0]["FRYING"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["FRYING"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FRYING"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BROILING
        {
            get
            {
                return base.dtModel.Rows[0]["BROILING"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BROILING"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BROILING"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool ROASTING
        {
            get
            {
                return base.dtModel.Rows[0]["ROASTING"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["ROASTING"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ROASTING"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)COOKING
        public bool COOKING
        {
            get
            {
                return base.dtModel.Rows[0]["COOKING"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["COOKING"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COOKING"] = value;
            }
        }
        public bool PRK_TYP_VALET
        {
            get
            {
                return base.dtModel.Rows[0]["PRK_TYP_VALET"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["PRK_TYP_VALET"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PRK_TYP_VALET"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool PRK_TYP_PREMISES
        {
            get
            {
                return base.dtModel.Rows[0]["PRK_TYP_PREMISES"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["PRK_TYP_PREMISES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PRK_TYP_PREMISES"] = value;
            }
        }
        
             // model for database field LST_ALL_OCCUP(string)
        public bool OPR_ON_PREMISES
        {
            get
            {
                return base.dtModel.Rows[0]["OPR_ON_PREMISES"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["OPR_ON_PREMISES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["OPR_ON_PREMISES"] = value;
            }
        }
         // model for database field LST_ALL_OCCUP(string)
        public bool OPR_OFF_PREMISES
        {
            get
            {
                return base.dtModel.Rows[0]["OPR_OFF_PREMISES"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["OPR_OFF_PREMISES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["OPR_OFF_PREMISES"] = value;
            }
        }
         // model for database field LST_ALL_OCCUP(string)
        public bool EMRG_LIGHTS
        {
            get
            {
                return base.dtModel.Rows[0]["EMRG_LIGHTS"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["EMRG_LIGHTS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EMRG_LIGHTS"] = value;
            }
        }
         // model for database field LST_ALL_OCCUP(string)
        public bool WOOD_STOVE
        {
            get
            {
                return base.dtModel.Rows[0]["WOOD_STOVE"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["WOOD_STOVE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["WOOD_STOVE"] = value;
            }
        }
         // model for database field LST_ALL_OCCUP(string)
        public bool HIST_MARKER
        {
            get
            {
                return base.dtModel.Rows[0]["HIST_MARKER"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["HIST_MARKER"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["HIST_MARKER"] = value;
            }
        }
         // model for database field LST_ALL_OCCUP(string)
        public bool EXTNG_SYS_COV_COOKNG
        {
            get
            {
                return base.dtModel.Rows[0]["EXTNG_SYS_COV_COOKNG"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["EXTNG_SYS_COV_COOKNG"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXTNG_SYS_COV_COOKNG"] = value;
            }
        }
       // model for database field LST_ALL_OCCUP(string)
        public bool EXTNG_SYS_MNT_CNTRCT
        {
            get
            {
                return base.dtModel.Rows[0]["EXTNG_SYS_MNT_CNTRCT"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["EXTNG_SYS_MNT_CNTRCT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXTNG_SYS_MNT_CNTRCT"] = value;
            }
        }
        public bool GAS_OFF_COOKNG
        {
            get
            {
                return base.dtModel.Rows[0]["GAS_OFF_COOKNG"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["GAS_OFF_COOKNG"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["GAS_OFF_COOKNG"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool HOOD_FILTER_CLND
        {
            get
            {
                return base.dtModel.Rows[0]["HOOD_FILTER_CLND"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["HOOD_FILTER_CLND"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["HOOD_FILTER_CLND"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool HOOD_DUCTS_EQUIP
        {
            get
            {
                return base.dtModel.Rows[0]["HOOD_DUCTS_EQUIP"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["HOOD_DUCTS_EQUIP"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["HOOD_DUCTS_EQUIP"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool HOOD_DUCTS_MNT_SCH
        {
            get
            {
                return base.dtModel.Rows[0]["HOOD_DUCTS_MNT_SCH"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["HOOD_DUCTS_MNT_SCH"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["HOOD_DUCTS_MNT_SCH"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BC_EXTNG_AVL
        {
            get
            {
                return base.dtModel.Rows[0]["BC_EXTNG_AVL"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BC_EXTNG_AVL"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BC_EXTNG_AVL"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool ADQT_CLEARANCE
        {
            get
            {
                return base.dtModel.Rows[0]["ADQT_CLEARANCE"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["ADQT_CLEARANCE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ADQT_CLEARANCE"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public bool BEER_SALES
        {
            get
            {
                return base.dtModel.Rows[0]["BEER_SALES"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["BEER_SALES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BEER_SALES"] = value;
            }
        }
             // model for database field LST_ALL_OCCUP(string)
        public bool WINE_SALES
        {
            get
            {
                return base.dtModel.Rows[0]["WINE_SALES"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["WINE_SALES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["WINE_SALES"] = value;
            }
        }
             // model for database field LST_ALL_OCCUP(string)
        public bool FULL_BAR
        {
            get
            {
                return base.dtModel.Rows[0]["FULL_BAR"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["FULL_BAR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FULL_BAR"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)

        // model for database field TOT_AREA(double)
        public decimal TOT_EXPNS_FOOD_LIQUOR
        {
            get
            {
                return base.dtModel.Rows[0]["TOT_EXPNS_FOOD_LIQUOR"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["TOT_EXPNS_FOOD_LIQUOR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TOT_EXPNS_FOOD_LIQUOR"] = value;
            }
        }
        // model for database field TOT_AREA(double)
        public decimal TOT_EXPNS_OTHERS
        {
            get
            {
                return base.dtModel.Rows[0]["TOT_EXPNS_OTHERS"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["TOT_EXPNS_OTHERS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TOT_EXPNS_OTHERS"] = value;
            }
        }
        // model for database field TOT_AREA(double)
        public decimal NET_PROFIT
        {
            get
            {
                return base.dtModel.Rows[0]["NET_PROFIT"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["NET_PROFIT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["NET_PROFIT"] = value;
            }
        }
        // model for database field TOT_AREA(double)
        public decimal ACCNT_PAYABLE
        {
            get
            {
                return base.dtModel.Rows[0]["ACCNT_PAYABLE"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["ACCNT_PAYABLE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACCNT_PAYABLE"] = value;
            }
        }
        // model for database field TOT_AREA(double)
        public decimal NOTES_PAYABLE
        {
            get
            {
                return base.dtModel.Rows[0]["NOTES_PAYABLE"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["NOTES_PAYABLE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["NOTES_PAYABLE"] = value;
            }
        }
        public decimal BNK_LOANS_PAYABLE
        {
            get
            {
                return base.dtModel.Rows[0]["BNK_LOANS_PAYABLE"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["BNK_LOANS_PAYABLE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BNK_LOANS_PAYABLE"] = value;
            }
        }
       
              
        #endregion
    }
}
