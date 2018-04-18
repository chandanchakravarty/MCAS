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
    /// Database Model for POL_BOP_PREMISES_LOC_DETAILS
    /// </summary>
    public class ClsPremisesInfo : Cms.Model.ClsCommonModel
    {
        private const string POL_BOP_PREMISES_LOC_DETAILS = "POL_BOP_PREMISES_LOC_DETAILS";
        public ClsPremisesInfo()
        {
            base.dtModel.TableName = "POL_BOP_PREMISES_LOC_DETAILS";		// setting table name for data table that holds property values.
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
            base.dtModel.Columns.Add("LOC_DETAILS_ID", typeof(int));
            
            //base.dtModel.Columns.Add("STREET_ADDR", typeof(string));
            //base.dtModel.Columns.Add("CITY", typeof(string));
            //base.dtModel.Columns.Add("STATE", typeof(int));
            //base.dtModel.Columns.Add("ZIP", typeof(string));
            //base.dtModel.Columns.Add("INTEREST", typeof(int));
            //base.dtModel.Columns.Add("FL_TM_EMP", typeof(string));
            ////RETAIL_STORE
            //base.dtModel.Columns.Add("PT_TM_EMP", typeof(string));
            //base.dtModel.Columns.Add("ANN_REVENUE", typeof(double));
            //base.dtModel.Columns.Add("OCC_AREA", typeof(double));
            //base.dtModel.Columns.Add("OPEN_AREA", typeof(double));
            //base.dtModel.Columns.Add("TOT_AREA", typeof(double));

            //base.dtModel.Columns.Add("AREA_LEASED", typeof(string));
            base.dtModel.Columns.Add("DESC_BLDNG", typeof(string));

            base.dtModel.Columns.Add("DESC_OPERTN", typeof(string));
            base.dtModel.Columns.Add("LST_ALL_OCCUP", typeof(string));
            base.dtModel.Columns.Add("ANN_SALES", typeof(double));
            base.dtModel.Columns.Add("TOT_PAYROLL", typeof(double));

            //base.dtModel.Columns.Add("RATE_NUM", typeof(int));
            //base.dtModel.Columns.Add("RATE_GRP", typeof(int));
            //base.dtModel.Columns.Add("RATE_TER_NUM", typeof(int));
            base.dtModel.Columns.Add("PROT_CLS", typeof(string));

            base.dtModel.Columns.Add("IS_ALM_USED", typeof(string));
            base.dtModel.Columns.Add("IS_RES_SPACE", typeof(string));
            base.dtModel.Columns.Add("RES_SPACE_SMK_DET", typeof(string));
            base.dtModel.Columns.Add("RES_OCC", typeof(string));
            base.dtModel.Columns.Add("FIRE_HYDRANT_DIST", typeof(double));
            base.dtModel.Columns.Add("FIRE_STATION_DIST", typeof(double));

            base.dtModel.Columns.Add("FIRE_DIST_NAME", typeof(string));
            base.dtModel.Columns.Add("FIRE_DIST_CODE", typeof(string));
            base.dtModel.Columns.Add("BCEGS", typeof(string));
            base.dtModel.Columns.Add("CITY_LMT", typeof(string));
            base.dtModel.Columns.Add("SWIMMING_POOL", typeof(string));
            base.dtModel.Columns.Add("PLAY_GROUND", typeof(string));

            base.dtModel.Columns.Add("BUILD_UNDER_CON", typeof(string));
            base.dtModel.Columns.Add("BUILD_SHPNG_CENT", typeof(string));
            base.dtModel.Columns.Add("BOILER", typeof(string));
            base.dtModel.Columns.Add("MED_EQUIP", typeof(string));

            
            base.dtModel.Columns.Add("ALARM_TYPE", typeof(string));
            base.dtModel.Columns.Add("ALARM_DESC", typeof(string));
            base.dtModel.Columns.Add("SAFE_VAULT", typeof(string));

            base.dtModel.Columns.Add("PREMISE_ALARM", typeof(string));
            base.dtModel.Columns.Add("CYL_DOOR_LOCK", typeof(string));
            base.dtModel.Columns.Add("SAFE_VAULT_LBL", typeof(string));
            base.dtModel.Columns.Add("SAFE_VAULT_CLASS", typeof(string));
            base.dtModel.Columns.Add("SAFE_VAULT_MANUFAC", typeof(string));

            base.dtModel.Columns.Add("MAX_CASH_PREM", typeof(double));
            base.dtModel.Columns.Add("MAX_CASH_MSG", typeof(double));
            base.dtModel.Columns.Add("MONEY_OVER_NIGHT", typeof(double));

            base.dtModel.Columns.Add("FREQUENCY_DEPOSIT", typeof(int));

            base.dtModel.Columns.Add("SAFE_DOOR_CONST", typeof(string));
            base.dtModel.Columns.Add("GRADE", typeof(string));
            base.dtModel.Columns.Add("OTH_PROTECTION", typeof(string));
            base.dtModel.Columns.Add("RIGHT_EXP_DESC", typeof(string));
            base.dtModel.Columns.Add("RIGHT_EXP_DIST", typeof(string));

            base.dtModel.Columns.Add("LEFT_EXP_DESC", typeof(string));
            base.dtModel.Columns.Add("LEFT_EXP_DIST", typeof(string));
            base.dtModel.Columns.Add("FRONT_EXP_DESC", typeof(string));
            base.dtModel.Columns.Add("FRONT_EXP_DIST", typeof(string));
            base.dtModel.Columns.Add("REAR_EXP_DESC", typeof(string));
            base.dtModel.Columns.Add("REAR_EXP_DIST", typeof(string));
            base.dtModel.Columns.Add("COUNTY", typeof(string));
            base.dtModel.Columns.Add("BLANKETRATE", typeof(string));





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
        public int LOC_DETAILS_ID
        {
            get
            {
                return base.dtModel.Rows[0]["LOC_DETAILS_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOC_DETAILS_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LOC_DETAILS_ID"] = value;
            }
        }       
                                         
        // model for database field DESC_BLDNG(string)
        public string DESC_BLDNG
        {
            get
            {
                return base.dtModel.Rows[0]["DESC_BLDNG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DESC_BLDNG"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DESC_BLDNG"] = value;
            }
        }
        // model for database field DESC_OPERTN(string)
        public string DESC_OPERTN
        {
            get
            {
                return base.dtModel.Rows[0]["DESC_OPERTN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DESC_OPERTN"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DESC_OPERTN"] = value;
            }
        }


        // model for database field LST_ALL_OCCUP(string)
        public string LST_ALL_OCCUP
        {
            get
            {
                return base.dtModel.Rows[0]["LST_ALL_OCCUP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LST_ALL_OCCUP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LST_ALL_OCCUP"] = value;
            }
        }

        // model for database field ANN_SALES(string)
        public decimal ANN_SALES
        {
            get
            {
                return base.dtModel.Rows[0]["ANN_SALES"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["ANN_SALES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ANN_SALES"] = value;
            }
        }

        // model for database field TOT_PAYROLL(string)
        public decimal TOT_PAYROLL
        {
            get
            {
                return base.dtModel.Rows[0]["TOT_PAYROLL"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["TOT_PAYROLL"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TOT_PAYROLL"] = value;
            }
        }


        // model for database field RATE_TER_NUM(int)
        //public int RATE_TER_NUM
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["RATE_TER_NUM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RATE_TER_NUM"].ToString());
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["RATE_TER_NUM"] = value;
        //    }
        //}

        // model for database field LST_ALL_OCCUP(string)
        public string PROT_CLS
        {
            get
            {
                return base.dtModel.Rows[0]["PROT_CLS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PROT_CLS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PROT_CLS"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string IS_ALM_USED
        {
            get
            {
                return base.dtModel.Rows[0]["IS_ALM_USED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ALM_USED"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_ALM_USED"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string IS_RES_SPACE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_RES_SPACE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_RES_SPACE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_RES_SPACE"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string RES_SPACE_SMK_DET
        {
            get
            {
                return base.dtModel.Rows[0]["RES_SPACE_SMK_DET"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RES_SPACE_SMK_DET"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["RES_SPACE_SMK_DET"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string RES_OCC
        {
            get
            {
                return base.dtModel.Rows[0]["RES_OCC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RES_OCC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["RES_OCC"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public decimal FIRE_HYDRANT_DIST
        {
            get
            {
                return base.dtModel.Rows[0]["FIRE_HYDRANT_DIST"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["FIRE_HYDRANT_DIST"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FIRE_HYDRANT_DIST"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public decimal FIRE_STATION_DIST
        {
            get
            {
                return base.dtModel.Rows[0]["FIRE_STATION_DIST"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["FIRE_STATION_DIST"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FIRE_STATION_DIST"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string FIRE_DIST_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["FIRE_DIST_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FIRE_DIST_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FIRE_DIST_NAME"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string FIRE_DIST_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["FIRE_DIST_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FIRE_DIST_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FIRE_DIST_CODE"] = value;
            }
        }


        // model for database field LST_ALL_OCCUP(string)
        public string BCEGS
        {
            get
            {
                return base.dtModel.Rows[0]["BCEGS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BCEGS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BCEGS"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string CITY_LMT
        {
            get
            {
                return base.dtModel.Rows[0]["CITY_LMT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CITY_LMT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CITY_LMT"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string SWIMMING_POOL
        {
            get
            {
                return base.dtModel.Rows[0]["SWIMMING_POOL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SWIMMING_POOL"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SWIMMING_POOL"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string PLAY_GROUND
        {
            get
            {
                return base.dtModel.Rows[0]["PLAY_GROUND"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PLAY_GROUND"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PLAY_GROUND"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string BUILD_UNDER_CON
        {
            get
            {
                return base.dtModel.Rows[0]["BUILD_UNDER_CON"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BUILD_UNDER_CON"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BUILD_UNDER_CON"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string BUILD_SHPNG_CENT
        {
            get
            {
                return base.dtModel.Rows[0]["BUILD_SHPNG_CENT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BUILD_SHPNG_CENT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BUILD_SHPNG_CENT"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string BOILER
        {
            get
            {
                return base.dtModel.Rows[0]["BOILER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BOILER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BOILER"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string MED_EQUIP
        {
            get
            {
                return base.dtModel.Rows[0]["MED_EQUIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MED_EQUIP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MED_EQUIP"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string ALARM_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["ALARM_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ALARM_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ALARM_TYPE"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string ALARM_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["ALARM_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ALARM_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ALARM_DESC"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string SAFE_VAULT
        {
            get
            {
                return base.dtModel.Rows[0]["SAFE_VAULT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFE_VAULT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SAFE_VAULT"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string PREMISE_ALARM
        {
            get
            {
                return base.dtModel.Rows[0]["PREMISE_ALARM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PREMISE_ALARM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PREMISE_ALARM"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string CYL_DOOR_LOCK
        {
            get
            {
                return base.dtModel.Rows[0]["CYL_DOOR_LOCK"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CYL_DOOR_LOCK"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CYL_DOOR_LOCK"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string SAFE_VAULT_LBL
        {
            get
            {
                return base.dtModel.Rows[0]["SAFE_VAULT_LBL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFE_VAULT_LBL"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SAFE_VAULT_LBL"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string SAFE_VAULT_CLASS
        {
            get
            {
                return base.dtModel.Rows[0]["SAFE_VAULT_CLASS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFE_VAULT_CLASS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SAFE_VAULT_CLASS"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string SAFE_VAULT_MANUFAC
        {
            get
            {
                return base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public decimal MAX_CASH_PREM
        {
            get
            {
                return base.dtModel.Rows[0]["MAX_CASH_PREM"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["MAX_CASH_PREM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MAX_CASH_PREM"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public decimal MAX_CASH_MSG
        {
            get
            {
                return base.dtModel.Rows[0]["MAX_CASH_MSG"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["MAX_CASH_MSG"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MAX_CASH_MSG"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public decimal MONEY_OVER_NIGHT
        {
            get
            {
                return base.dtModel.Rows[0]["MONEY_OVER_NIGHT"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["MONEY_OVER_NIGHT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MONEY_OVER_NIGHT"] = value;
            }
        }
        // model for database field FREQUENCY_DEPOSIT(int)
        public int FREQUENCY_DEPOSIT
        {
            get
            {
                return base.dtModel.Rows[0]["FREQUENCY_DEPOSIT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FREQUENCY_DEPOSIT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FREQUENCY_DEPOSIT"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string SAFE_DOOR_CONST
        {
            get
            {
                return base.dtModel.Rows[0]["SAFE_DOOR_CONST"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFE_DOOR_CONST"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SAFE_DOOR_CONST"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string GRADE
        {
            get
            {
                return base.dtModel.Rows[0]["GRADE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GRADE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["GRADE"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string OTH_PROTECTION
        {
            get
            {
                return base.dtModel.Rows[0]["OTH_PROTECTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTH_PROTECTION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["OTH_PROTECTION"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string RIGHT_EXP_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["RIGHT_EXP_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RIGHT_EXP_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["RIGHT_EXP_DESC"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string RIGHT_EXP_DIST
        {
            get
            {
                return base.dtModel.Rows[0]["RIGHT_EXP_DIST"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RIGHT_EXP_DIST"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["RIGHT_EXP_DIST"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string LEFT_EXP_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["LEFT_EXP_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LEFT_EXP_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LEFT_EXP_DESC"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string LEFT_EXP_DIST
        {
            get
            {
                return base.dtModel.Rows[0]["LEFT_EXP_DIST"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LEFT_EXP_DIST"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LEFT_EXP_DIST"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string FRONT_EXP_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["FRONT_EXP_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FRONT_EXP_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FRONT_EXP_DESC"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public string FRONT_EXP_DIST
        {
            get
            {
                return base.dtModel.Rows[0]["FRONT_EXP_DIST"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FRONT_EXP_DIST"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FRONT_EXP_DIST"] = value;
            }
        }
        public string REAR_EXP_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["REAR_EXP_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REAR_EXP_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REAR_EXP_DESC"] = value;
            }
        }
        public string REAR_EXP_DIST
        {
            get
            {
                return base.dtModel.Rows[0]["REAR_EXP_DIST"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REAR_EXP_DIST"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REAR_EXP_DIST"] = value;
            }
        }
        // model for database field COUNTY(string)
        public string COUNTY
        {
            get
            {
                return base.dtModel.Rows[0]["COUNTY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COUNTY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["COUNTY"] = value;
            }
        }
        // model for database field BLANKETRATE(string)
        public string BLANKETRATE
        {
            get
            {
                return base.dtModel.Rows[0]["BLANKETRATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BLANKETRATE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BLANKETRATE"] = value;
            }
        }

              
        #endregion
    }
}
