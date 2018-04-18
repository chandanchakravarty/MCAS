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
    /// Database Model for POL_SUP_FORM_SHOP
    /// </summary>
    public class ClsSuppFormShopInfo: Cms.Model.ClsCommonModel
    {
        private const string POL_BOP_PREMISES_LOC_DETAILS = "POL_SUP_FORM_SHOP";
        public ClsSuppFormShopInfo()
        {
            base.dtModel.TableName = "POL_SUP_FORM_SHOP";		// setting table name for data table that holds property values.
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
            base.dtModel.Columns.Add("SHOP_ID", typeof(int));
            
           
            //base.dtModel.Columns.Add("AREA_LEASED", typeof(string));
            base.dtModel.Columns.Add("UNITS", typeof(int));


            base.dtModel.Columns.Add("RESTURANT_OCUP", typeof(string));
            base.dtModel.Columns.Add("PERCENT_OCUP", typeof(double));
            base.dtModel.Columns.Add("FLAME_COOKING", typeof(string));

            //base.dtModel.Columns.Add("RATE_NUM", typeof(int));
            //base.dtModel.Columns.Add("RATE_GRP", typeof(int));
            //base.dtModel.Columns.Add("RATE_TER_NUM", typeof(int));
            base.dtModel.Columns.Add("NUM_FRYERS", typeof(string));

            base.dtModel.Columns.Add("NUM_GRILLS", typeof(string));
            base.dtModel.Columns.Add("DUCT_SYS", typeof(string));
            base.dtModel.Columns.Add("SUPPR_SYS", typeof(string));
            base.dtModel.Columns.Add("DUCT_CLND_PST_SIX_MONTHS", typeof(string));

            base.dtModel.Columns.Add("IS_INSURED", typeof(string));
            base.dtModel.Columns.Add("TENANT_LIABILITY", typeof(string));

          
         
            base.dtModel.Columns.Add("SEPARATE_BAR", typeof(string));
            base.dtModel.Columns.Add("BBQ_PIT", typeof(string));
            base.dtModel.Columns.Add("BLDG_TYPE_COOKNG", typeof(string));
            base.dtModel.Columns.Add("IS_ENTERTNMT", typeof(string));

            base.dtModel.Columns.Add("PERCENT_SALES", typeof(double));
            base.dtModel.Columns.Add("BBQ_PIT_DIST", typeof(double));
                            




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
        public int SHOP_ID
        {
            get
            {
                return base.dtModel.Rows[0]["SHOP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SHOP_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["SHOP_ID"] = value;
            }
        }       
                                         
        // model for database field DESC_BLDNG(string)
        public string RESTURANT_OCUP
        {
            get
            {
                return base.dtModel.Rows[0]["RESTURANT_OCUP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RESTURANT_OCUP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["RESTURANT_OCUP"] = value;
            }
        }
        // model for database field DESC_OPERTN(string)
        public string FLAME_COOKING
        {
            get
            {
                return base.dtModel.Rows[0]["FLAME_COOKING"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FLAME_COOKING"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FLAME_COOKING"] = value;
            }
        }


        // model for database field LST_ALL_OCCUP(string)
        public string NUM_FRYERS
        {
            get
            {
                return base.dtModel.Rows[0]["NUM_FRYERS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NUM_FRYERS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NUM_FRYERS"] = value;
            }
        }

        // model for database field ANN_SALES(string)
        public string NUM_GRILLS
        {
            get
            {
                return base.dtModel.Rows[0]["NUM_GRILLS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NUM_GRILLS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NUM_GRILLS"] = value;
            }
        }

        // model for database field TOT_PAYROLL(string)
        public string DUCT_SYS
        {
            get
            {
                return base.dtModel.Rows[0]["DUCT_SYS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DUCT_SYS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DUCT_SYS"] = value;
            }
        }


      

        // model for database field LST_ALL_OCCUP(string)
        public string SUPPR_SYS
        {
            get
            {
                return base.dtModel.Rows[0]["SUPPR_SYS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUPPR_SYS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUPPR_SYS"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string DUCT_CLND_PST_SIX_MONTHS
        {
            get
            {
                return base.dtModel.Rows[0]["DUCT_CLND_PST_SIX_MONTHS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DUCT_CLND_PST_SIX_MONTHS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DUCT_CLND_PST_SIX_MONTHS"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string IS_INSURED
        {
            get
            {
                return base.dtModel.Rows[0]["IS_INSURED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_INSURED"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_INSURED"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string TENANT_LIABILITY
        {
            get
            {
                return base.dtModel.Rows[0]["TENANT_LIABILITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TENANT_LIABILITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["TENANT_LIABILITY"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string SEPARATE_BAR
        {
            get
            {
                return base.dtModel.Rows[0]["SEPARATE_BAR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SEPARATE_BAR"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SEPARATE_BAR"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
       
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
        public string BBQ_PIT
        {
            get
            {
                return base.dtModel.Rows[0]["BBQ_PIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BBQ_PIT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BBQ_PIT"] = value;
            }
        }
        // model for database field LST_ALL_OCCUP(string)
        public string BLDG_TYPE_COOKNG
        {
            get
            {
                return base.dtModel.Rows[0]["BLDG_TYPE_COOKNG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BLDG_TYPE_COOKNG"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BLDG_TYPE_COOKNG"] = value;
            }
        }


        // model for database field LST_ALL_OCCUP(string)
        public string IS_ENTERTNMT
        {
            get
            {
                return base.dtModel.Rows[0]["IS_ENTERTNMT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ENTERTNMT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_ENTERTNMT"] = value;
            }
        }
      
      
       

        // model for database field LST_ALL_OCCUP(string)
        public decimal BBQ_PIT_DIST
        {
            get
            {
                return base.dtModel.Rows[0]["BBQ_PIT_DIST"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["BBQ_PIT_DIST"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BBQ_PIT_DIST"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public decimal PERCENT_SALES
        {
            get
            {
                return base.dtModel.Rows[0]["PERCENT_SALES"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["PERCENT_SALES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PERCENT_SALES"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
        public decimal PERCENT_OCUP
        {
            get
            {
                return base.dtModel.Rows[0]["PERCENT_OCUP"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["PERCENT_OCUP"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PERCENT_OCUP"] = value;
            }
        }
        // model for database field FREQUENCY_DEPOSIT(int)
        public int UNITS
        {
            get
            {
                return base.dtModel.Rows[0]["UNITS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["UNITS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["UNITS"] = value;
            }
        }

        // model for database field LST_ALL_OCCUP(string)
       

              
        #endregion
    }
}
