/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	5/17/2005 7:59:10 PM
<End Date				: -	
<Description				: - 	Models APP_WATERCRAFT_TRAILER_INFO
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
namespace Cms.Model.Application.Watercrafts
{
    /// <summary>
    /// Database Model for APP_WATERCRAFT_TRAILER_INFO.
    /// </summary>
    public class ClsWatercraftTrailerInfo : Cms.Model.ClsCommonModel
    {
        private const string APP_WATERCRAFT_TRAILER_INFO = "APP_WATERCRAFT_TRAILER_INFO";
        public ClsWatercraftTrailerInfo()
        {
            base.dtModel.TableName = "APP_WATERCRAFT_TRAILER_INFO";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table APP_WATERCRAFT_TRAILER_INFO
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
            base.dtModel.Columns.Add("APP_ID",typeof(int));
            base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
            base.dtModel.Columns.Add("TRAILER_ID",typeof(int));
            base.dtModel.Columns.Add("TRAILER_NO",typeof(int));
            base.dtModel.Columns.Add("YEAR",typeof(int));           
            base.dtModel.Columns.Add("MANUFACTURER",typeof(string));
            base.dtModel.Columns.Add("SERIAL_NO",typeof(string));           
            base.dtModel.Columns.Add("ASSOCIATED_BOAT",typeof(int));
			base.dtModel.Columns.Add("INSURED_VALUE",typeof(double));
			base.dtModel.Columns.Add("MODEL",typeof(string));
			base.dtModel.Columns.Add("TRAILER_TYPE",typeof(int));
			//Added on 13 feb 2006
			base.dtModel.Columns.Add("TRAILER_TYPE_CODE",typeof(string));
			//Asfa 13-june-2007
			base.dtModel.Columns.Add("TRAILER_DED",typeof(double));
			base.dtModel.Columns.Add("TRAILER_DED_ID",typeof(int));
			base.dtModel.Columns.Add("TRAILER_DED_AMOUNT_TEXT",typeof(string));
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
        // model for database field TRAILER_ID(int)
        public int TRAILER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["TRAILER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRAILER_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TRAILER_ID"] = value;
            }
        }
        // model for database field TRAILER_NO(int)
        public int TRAILER_NO
        {
            get
            {
                return base.dtModel.Rows[0]["TRAILER_NO"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRAILER_NO"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TRAILER_NO"] = value;
            }
        }
        // model for database field YEAR(int)
        public int YEAR
        {
            get
            {
                return base.dtModel.Rows[0]["YEAR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["YEAR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["YEAR"] = value;
            }
        }
        
        // model for database field MANUFACTURER(string)
        public string MANUFACTURER
        {
            get
            {
                return base.dtModel.Rows[0]["MANUFACTURER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MANUFACTURER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MANUFACTURER"] = value;
            }
        }
        // model for database field SERIAL_NO(string)
        public string SERIAL_NO
        {
            get
            {
                return base.dtModel.Rows[0]["SERIAL_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SERIAL_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SERIAL_NO"] = value;
            }
        }
           
 
        // model for database field PREMIUM(double)
        public double INSURED_VALUE
        {
            get
            {
                return base.dtModel.Rows[0]["INSURED_VALUE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["INSURED_VALUE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INSURED_VALUE"] = value;
            }
        }
        // model for database field ASSOCIATED_BOAT(int)
        public int ASSOCIATED_BOAT
        {
            get
            {
                return base.dtModel.Rows[0]["ASSOCIATED_BOAT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ASSOCIATED_BOAT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ASSOCIATED_BOAT"] = value;
            }
        }
		// model for database field TRAILER_TYPE(string)
		public int TRAILER_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["TRAILER_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRAILER_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRAILER_TYPE"] = value;
			}
		}	
		//// model for database field TRAILER_TYPE_CODE(string)
        public string TRAILER_TYPE_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["TRAILER_TYPE_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TRAILER_TYPE_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["TRAILER_TYPE_CODE"] = value;
            }
        }
		//// model for database field TRAILER_DED(double)
		public double TRAILER_DED
		{
			get
			{
				return base.dtModel.Rows[0]["TRAILER_DED"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["TRAILER_DED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRAILER_DED"] = value;
			}
		}
		//// model for database field TRAILER_DED_ID(int)
		public int TRAILER_DED_ID 
		{
			get
			{
				return base.dtModel.Rows[0]["TRAILER_DED_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRAILER_DED_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRAILER_DED_ID"] = value;
			}
		}
		//// model for database field TRAILER_DED_AMOUNT_TEXT(string)
		public string TRAILER_DED_AMOUNT_TEXT 
		{
			get
			{
				return base.dtModel.Rows[0]["TRAILER_DED_AMOUNT_TEXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TRAILER_DED_AMOUNT_TEXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRAILER_DED_AMOUNT_TEXT"] = value;
			}
		}
		// model for database field MODEL(string)
		public string MODEL
		{
			get
			{
				return base.dtModel.Rows[0]["MODEL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MODEL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MODEL"] = value;
			}
		}
        #endregion
    }
}
