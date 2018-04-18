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
    /// Database Model for POL_NATURE_OF_BUSINESS.
    /// </summary>
    public class ClsPremiseLocationInfo : Cms.Model.ClsCommonModel
    {
        private const string POL_BOP_PREMISESLOCATIONS = "POL_BOP_PREMISESLOCATIONS";
        public ClsPremiseLocationInfo()
        {
            base.dtModel.TableName = "POL_BOP_PREMISESLOCATIONS";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table POL_SUP_FORM_SHOP
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
           base.dtModel.Columns.Add("LOCATION_ID", typeof(int));
            base.dtModel.Columns.Add("BUILDING", typeof(int));
            base.dtModel.Columns.Add("PREMLOC_ID", typeof(int));
            base.dtModel.Columns.Add("STREET_ADDR", typeof(string));
            base.dtModel.Columns.Add("CITY", typeof(string));
            base.dtModel.Columns.Add("STATE", typeof(string));
            base.dtModel.Columns.Add("COUNTY", typeof(string));

            
            base.dtModel.Columns.Add("ZIP", typeof(string));
            base.dtModel.Columns.Add("INTEREST", typeof(string));
            base.dtModel.Columns.Add("FL_TM_EMP", typeof(string));
             //RETAIL_STORE
            base.dtModel.Columns.Add("PT_TM_EMP", typeof(string));
            base.dtModel.Columns.Add("ANN_REVENUE", typeof(double));
            base.dtModel.Columns.Add("OCC_AREA", typeof(double));
            base.dtModel.Columns.Add("OPEN_AREA", typeof(double));
            base.dtModel.Columns.Add("TOT_AREA", typeof(double));

            base.dtModel.Columns.Add("AREA_LEASED", typeof(string));
            //base.dtModel.Columns.Add("DESC_BLDNG", typeof(string));

            //base.dtModel.Columns.Add("DESC_OPERTN", typeof(string));
            //base.dtModel.Columns.Add("LST_ALL_OCCUP", typeof(string));
            //base.dtModel.Columns.Add("ANN_SALES", typeof(double));
            //base.dtModel.Columns.Add("TOT_PAYROLL", typeof(double));

            //base.dtModel.Columns.Add("RATE_NUM", typeof(int));
            //base.dtModel.Columns.Add("RATE_GRP", typeof(int));
            //base.dtModel.Columns.Add("RATE_TER_NUM", typeof(int));
            //base.dtModel.Columns.Add("PROT_CLS", typeof(string));

            //base.dtModel.Columns.Add("IS_ALM_USED", typeof(string));
            //base.dtModel.Columns.Add("IS_RES_SPACE", typeof(string));
            //base.dtModel.Columns.Add("RES_SPACE_SMK_DET", typeof(string));
            //base.dtModel.Columns.Add("RES_OCC", typeof(string));
            //base.dtModel.Columns.Add("FIRE_HYDRANT_DIST", typeof(double));
            //base.dtModel.Columns.Add("FIRE_STATION_DIST", typeof(double));

            //base.dtModel.Columns.Add("FIRE_DIST_NAME", typeof(string));
            //base.dtModel.Columns.Add("FIRE_DIST_CODE", typeof(string));
            //base.dtModel.Columns.Add("BCEGS", typeof(string));
            //base.dtModel.Columns.Add("CITY_LMT", typeof(string));
            //base.dtModel.Columns.Add("SWIMMING_POOL", typeof(string));
            //base.dtModel.Columns.Add("PLAY_GROUND", typeof(string));

            //base.dtModel.Columns.Add("BUILD_UNDER_CON", typeof(string));
            //base.dtModel.Columns.Add("BUILD_SHPNG_CENT", typeof(string));
            //base.dtModel.Columns.Add("BOILER", typeof(string));
            //base.dtModel.Columns.Add("ALARM_TYPE", typeof(string));
            //base.dtModel.Columns.Add("ALARM_DESC", typeof(string));
            //base.dtModel.Columns.Add("SAFE_VAULT", typeof(string));

            //base.dtModel.Columns.Add("PREMISE_ALARM", typeof(string));
            //base.dtModel.Columns.Add("CYL_DOOR_LOCK", typeof(string));
            //base.dtModel.Columns.Add("SAFE_VAULT_LBL", typeof(string));
            //base.dtModel.Columns.Add("SAFE_VAULT_CLASS", typeof(string));
            //base.dtModel.Columns.Add("SAFE_VAULT_MANUFAC", typeof(string));
            
            //base.dtModel.Columns.Add("MAX_CASH_PREM", typeof(double));
            //base.dtModel.Columns.Add("MAX_CASH_MSG", typeof(double));
            //base.dtModel.Columns.Add("MONEY_OVER_NIGHT", typeof(double));

            //base.dtModel.Columns.Add("FREQUENCY_DEPOSIT", typeof(int));

            //base.dtModel.Columns.Add("SAFE_DOOR_CONST", typeof(string));
            //base.dtModel.Columns.Add("GRADE", typeof(string));
            //base.dtModel.Columns.Add("OTH_PROTECTION", typeof(string));
            //base.dtModel.Columns.Add("RIGHT_EXP_DESC", typeof(string));
            //base.dtModel.Columns.Add("RIGHT_EXP_DIST", typeof(string));

            //base.dtModel.Columns.Add("LEFT_EXP_DESC", typeof(string));
            //base.dtModel.Columns.Add("LEFT_EXP_DIST", typeof(string));
            //base.dtModel.Columns.Add("FRONT_EXP_DESC", typeof(string));
            //base.dtModel.Columns.Add("FRONT_EXP_DIST", typeof(string));
            //base.dtModel.Columns.Add("REAR_EXP_DESC", typeof(string));
            //base.dtModel.Columns.Add("REAR_EXP_DIST", typeof(string));
            //base.dtModel.Columns.Add("COUNTY", typeof(string));


       
          
            
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

        public int BUILDING
        {
            get
            {
                return base.dtModel.Rows[0]["BUILDING"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BUILDING"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUILDING"] = value;
            }
        }
        // model for database field BLDNG_ID(int)

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
        public int PREMLOC_ID
        {
            get
            {
                return base.dtModel.Rows[0]["PREMLOC_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PREMLOC_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PREMLOC_ID"] = value;
            }
        }
        // model for database field BUSINESS_NATURE(int)
        public string STREET_ADDR
        {
            get
            {
                return base.dtModel.Rows[0]["STREET_ADDR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STREET_ADDR"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["STREET_ADDR"] = value;
            }
        }
        // model for database field PRIMARY_OPERATION(string)
        public string CITY
        {
            get
            {
                return base.dtModel.Rows[0]["CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CITY"] = value;
            }
        }
        // model for database field STATE(int)
        public string STATE
        {
            get
            {
                return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STATE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["STATE"] = value;
            }
        }
        // model for database field BUSINESS_NATURE(int)


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


        // model for database field ZIP(string)
        public string ZIP
        {
            get
            {
                return base.dtModel.Rows[0]["ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ZIP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ZIP"] = value;
            }
        }

        // model for database field INTEREST(int)
        public string INTEREST
        {
            get
            {
                return base.dtModel.Rows[0]["INTEREST"] == DBNull.Value ? "" : base.dtModel.Rows[0]["INTEREST"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["INTEREST"] = value;
            }
        }
       
        // model for database field Full Time employee(string)
        public string FL_TM_EMP
        {
            get
            {
                return base.dtModel.Rows[0]["FL_TM_EMP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FL_TM_EMP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FL_TM_EMP"] = value;
            }
        }
        // model for database field Part Time employee(string)
        public string PT_TM_EMP
        {
            get
            {
                return base.dtModel.Rows[0]["PT_TM_EMP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PT_TM_EMP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PT_TM_EMP"] = value;
            }
        }

        // model for database field ANN_REVENUE(double)
        public decimal ANN_REVENUE
        {
            get
            {
                return base.dtModel.Rows[0]["ANN_REVENUE"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["ANN_REVENUE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ANN_REVENUE"] = value;
            }
        }


        // model for database field OCC_AREA(double)
        public decimal OCC_AREA
        {
            get
            {
                return base.dtModel.Rows[0]["OCC_AREA"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["OCC_AREA"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["OCC_AREA"] = value;
            }
        }
        // model for database field ANN_REVENUE(double)
        public decimal OPEN_AREA
        {
            get
            {
                return base.dtModel.Rows[0]["OPEN_AREA"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["OPEN_AREA"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["OPEN_AREA"] = value;
            }
        }
        // model for database field TOT_AREA(double)
        public decimal TOT_AREA
        {
            get
            {
                return base.dtModel.Rows[0]["TOT_AREA"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["TOT_AREA"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TOT_AREA"] = value;
            }
        }
        
        // model for database field BUSINESS_NATURE(int)
        // model for database field AREA_LEASED(string)
        public string AREA_LEASED
        {
            get
            {
                return base.dtModel.Rows[0]["AREA_LEASED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AREA_LEASED"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["AREA_LEASED"] = value;
            }
        }
 //       // model for database field DESC_BLDNG(string)
 //       public string DESC_BLDNG
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["DESC_BLDNG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DESC_BLDNG"].ToString();
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["DESC_BLDNG"] = value;
 //           }
 //       }
 //       // model for database field DESC_OPERTN(string)
 //       public string DESC_OPERTN
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["DESC_OPERTN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DESC_OPERTN"].ToString();
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["DESC_OPERTN"] = value;
 //           }
 //       }

       
 //       // model for database field LST_ALL_OCCUP(string)
 //       public string LST_ALL_OCCUP
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["LST_ALL_OCCUP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LST_ALL_OCCUP"].ToString();
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["LST_ALL_OCCUP"] = value;
 //           }
 //       }

 //       // model for database field ANN_SALES(string)
 //       public decimal ANN_SALES
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["ANN_SALES"] == DBNull.Value ? "" : decimal.Parse(base.dtModel.Rows[0]["LST_ALL_OCCUP"].ToString());
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["ANN_SALES"] = value;
 //           }
 //       }

 //       // model for database field TOT_PAYROLL(string)
 //       public decimal TOT_PAYROLL
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["TOT_PAYROLL"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["TOT_PAYROLL"].ToString());
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["TOT_PAYROLL"] = value;
 //           }
 //       }


 //     
 //       // model for database field LST_ALL_OCCUP(string)
 //       public string SAFE_VAULT_CLASS
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["SAFE_VAULT_CLASS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFE_VAULT_CLASS"].ToString();
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["SAFE_VAULT_CLASS"] = value;
 //           }
 //       }

 //       // model for database field LST_ALL_OCCUP(string)
 //       public string SAFE_VAULT_MANUFAC
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"].ToString();
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"] = value;
 //           }
 //       }

 //      
 //       // model for database field LST_ALL_OCCUP(string)
 //       public string SAFE_VAULT_MANUFAC
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"].ToString();
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"] = value;
 //           }
 //       }

 //       // model for database field LST_ALL_OCCUP(string)
 //       public string SAFE_VAULT_MANUFAC
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"].ToString();
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["SAFE_VAULT_MANUFAC"] = value;
 //           }
 //       }

 //// model for database field BUSINESS_START_DATE(DateTime)

 //       //dO ABOVE

 //       public DateTime BUSINESS_START_DATE
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["BUSINESS_START_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["BUSINESS_START_DATE"].ToString());
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["BUSINESS_START_DATE"] = value;
 //           }
 //       }
 //       // model for database field OTHER_OPERATION(string)
 //       public string OTHER_OPERATION
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["OTHER_OPERATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_OPERATION"].ToString();
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["OTHER_OPERATION"] = value;
 //           }
 //       }

 //       // model for database field REPAIR_WORK(int)
 //       public int REPAIR_WORK
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["REPAIR_WORK"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REPAIR_WORK"].ToString());
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["REPAIR_WORK"] = value;
 //           }
 //       }
 //       // model for database field PREMISES_WORK(int)
 //       public int PREMISES_WORK
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["PREMISES_WORK"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PREMISES_WORK"].ToString());
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["PREMISES_WORK"] = value;
 //           }
 //       }

 //       // model for database field RETAIL_STORE(int)
 //       public int RETAIL_STORE
 //       {
 //           get
 //           {
 //               return base.dtModel.Rows[0]["RETAIL_STORE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RETAIL_STORE"].ToString());
 //           }
 //           set
 //           {
 //               base.dtModel.Rows[0]["RETAIL_STORE"] = value;
 //           }
 //       }

        #endregion
    }
}
