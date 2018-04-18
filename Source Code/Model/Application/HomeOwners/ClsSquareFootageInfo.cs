/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	5/12/2005 7:45:59 PM
<End Date				: -	
<Description				: - 	Models APP_SQR_FOOT_IMPROVEMENTS
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Application.HomeOwners
{
    /// <summary>
    /// Database Model for APP_SQR_FOOT_IMPROVEMENTS.
    /// </summary>
    public class ClsSquareFootageInfo : Cms.Model.ClsCommonModel
    {
        private const string APP_SQR_FOOT_IMPROVEMENTS = "APP_SQR_FOOT_IMPROVEMENTS";
        public ClsSquareFootageInfo()
        {
            base.dtModel.TableName = "APP_SQR_FOOT_IMPROVEMENTS";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table APP_SQR_FOOT_IMPROVEMENTS
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
            base.dtModel.Columns.Add("APP_ID",typeof(int));
            base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
            base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
            base.dtModel.Columns.Add("TOT_SQR_FOOTAGE",typeof(double));
            base.dtModel.Columns.Add("GARAGE_SQR_FOOTAGE",typeof(double));
            base.dtModel.Columns.Add("BREEZE_SQR_FOOTAGE",typeof(double));
            base.dtModel.Columns.Add("BASMT_SQR_FOOTAGE",typeof(double));
            base.dtModel.Columns.Add("WIRING_RENOVATION",typeof(int));
            base.dtModel.Columns.Add("WIRING_UPDATE_YEAR",typeof(int));
            base.dtModel.Columns.Add("PLUMBING_RENOVATION",typeof(int));
            base.dtModel.Columns.Add("PLUMBING_UPDATE_YEAR",typeof(int));
            base.dtModel.Columns.Add("HEATING_RENOVATION",typeof(int));
            base.dtModel.Columns.Add("HEATING_UPDATE_YEAR",typeof(int));
            base.dtModel.Columns.Add("ROOFING_RENOVATION",typeof(int));
            base.dtModel.Columns.Add("ROOFING_UPDATE_YEAR",typeof(int));
            base.dtModel.Columns.Add("NO_OF_AMPS",typeof(int));
            base.dtModel.Columns.Add("CIRCUIT_BREAKERS",typeof(string));
           
        }
        #region Database schema details
        // model for database field CUSTOMER_ID(int)
        public int CUSTOMER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
            }
        }
        // model for database field APP_ID(int)
        public int APP_ID
        {
            get
            {
                return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["APP_ID"] = value;
            }
        }
        // model for database field APP_VERSION_ID(int)
        public int APP_VERSION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
            }
        }
        // model for database field DWELLING_ID(int)
        public int DWELLING_ID
        {
            get
            {
                return base.dtModel.Rows[0]["DWELLING_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DWELLING_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DWELLING_ID"] = value;
            }
        }
        // model for database field TOT_SQR_FOOTAGE(double)
        public double TOT_SQR_FOOTAGE
        {
            get
            {
                return base.dtModel.Rows[0]["TOT_SQR_FOOTAGE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["TOT_SQR_FOOTAGE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TOT_SQR_FOOTAGE"] = value;
            }
        }
        // model for database field GARAGE_SQR_FOOTAGE(double)
        public double GARAGE_SQR_FOOTAGE
        {
            get
            {
                return base.dtModel.Rows[0]["GARAGE_SQR_FOOTAGE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["GARAGE_SQR_FOOTAGE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["GARAGE_SQR_FOOTAGE"] = value;
            }
        }
        // model for database field BREEZE_SQR_FOOTAGE(double)
        public double BREEZE_SQR_FOOTAGE
        {
            get
            {
                return base.dtModel.Rows[0]["BREEZE_SQR_FOOTAGE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["BREEZE_SQR_FOOTAGE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BREEZE_SQR_FOOTAGE"] = value;
            }
        }
        // model for database field BASMT_SQR_FOOTAGE(double)
        public double BASMT_SQR_FOOTAGE
        {
            get
            {
                return base.dtModel.Rows[0]["BASMT_SQR_FOOTAGE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["BASMT_SQR_FOOTAGE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BASMT_SQR_FOOTAGE"] = value;
            }
        }
        // model for database field WIRING_RENOVATION(int)
        public int WIRING_RENOVATION
        {
            get
            {
                return base.dtModel.Rows[0]["WIRING_RENOVATION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["WIRING_RENOVATION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["WIRING_RENOVATION"] = value;
            }
        }
        // model for database field WIRING_UPDATE_YEAR(int)
        public int WIRING_UPDATE_YEAR
        {
            get
            {
                return base.dtModel.Rows[0]["WIRING_UPDATE_YEAR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["WIRING_UPDATE_YEAR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["WIRING_UPDATE_YEAR"] = value;
            }
        }
        // model for database field PLUMBING_RENOVATION(int)
        public int PLUMBING_RENOVATION
        {
            get
            {
                return base.dtModel.Rows[0]["PLUMBING_RENOVATION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PLUMBING_RENOVATION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PLUMBING_RENOVATION"] = value;
            }
        }
        // model for database field PLUMBING_UPDATE_YEAR(int)
        public int PLUMBING_UPDATE_YEAR
        {
            get
            {
                return base.dtModel.Rows[0]["PLUMBING_UPDATE_YEAR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PLUMBING_UPDATE_YEAR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PLUMBING_UPDATE_YEAR"] = value;
            }
        }
        // model for database field HEATING_RENOVATION(int)
        public int HEATING_RENOVATION
        {
            get
            {
                return base.dtModel.Rows[0]["HEATING_RENOVATION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["HEATING_RENOVATION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["HEATING_RENOVATION"] = value;
            }
        }
        // model for database field HEATING_UPDATE_YEAR(int)
        public int HEATING_UPDATE_YEAR
        {
            get
            {
                return base.dtModel.Rows[0]["HEATING_UPDATE_YEAR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["HEATING_UPDATE_YEAR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["HEATING_UPDATE_YEAR"] = value;
            }
        }
        // model for database field ROOFING_RENOVATION(int)
        public int ROOFING_RENOVATION
        {
            get
            {
                return base.dtModel.Rows[0]["ROOFING_RENOVATION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ROOFING_RENOVATION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ROOFING_RENOVATION"] = value;
            }
        }
        // model for database field ROOFING_UPDATE_YEAR(int)
        public int ROOFING_UPDATE_YEAR
        {
            get
            {
                return base.dtModel.Rows[0]["ROOFING_UPDATE_YEAR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ROOFING_UPDATE_YEAR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ROOFING_UPDATE_YEAR"] = value;
            }
        }
        // model for database field NO_OF_AMPS(int)
        public int NO_OF_AMPS
        {
            get
            {
                return base.dtModel.Rows[0]["NO_OF_AMPS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NO_OF_AMPS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["NO_OF_AMPS"] = value;
            }
        }
        // model for database field CIRCUIT_BREAKERS(string)
        public string CIRCUIT_BREAKERS
        {
            get
            {
                return base.dtModel.Rows[0]["CIRCUIT_BREAKERS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CIRCUIT_BREAKERS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CIRCUIT_BREAKERS"] = value;
            }
        }
       
        #endregion
    }
}
