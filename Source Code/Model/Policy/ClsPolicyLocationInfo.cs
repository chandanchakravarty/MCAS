/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	10/11/2005 4:56:38 PM
<End Date				: -	
<Description				: - 	Model class for Policy Location.
<Review Date				: - 
<Reviewed By			: - 	

*******************************************************************************************/ 
using System;
using System.Collections;
using System.Data;
using Cms.Model;


namespace Cms.Model.Policy
{
	/// <summary>
	/// Summary description for ClsPolicyLocationInfo.
	/// </summary>
	public class ClsPolicyLocationInfo: Cms.Model.ClsCommonModel
	{
		private ArrayList alSublocations;
		private const string POL_LOCATIONS = "POL_LOCATIONS";
		public ClsPolicyLocationInfo()
		{
			base.dtModel.TableName = "POL_LOCATIONS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_LOCATIONS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(Int64));//Change as per the tfs#2701 itrack-1366
            base.dtModel.Columns.Add("LOC_NUM", typeof(Int64));//Change as per the tfs#2701 itrack-1366
			base.dtModel.Columns.Add("IS_PRIMARY",typeof(string));
			base.dtModel.Columns.Add("LOC_ADD1",typeof(string));
			base.dtModel.Columns.Add("LOC_ADD2",typeof(string));
			base.dtModel.Columns.Add("LOC_CITY",typeof(string));
			base.dtModel.Columns.Add("LOC_COUNTY",typeof(string));
			base.dtModel.Columns.Add("LOC_STATE",typeof(string));
			base.dtModel.Columns.Add("LOC_ZIP",typeof(string));
			base.dtModel.Columns.Add("LOC_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("PHONE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("FAX_NUMBER",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE",typeof(string));
			base.dtModel.Columns.Add("NAMED_PERILL",typeof(int));
			base.dtModel.Columns.Add("DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("ID",typeof(string));
			base.dtModel.Columns.Add("ADDR_TYPE",typeof(string));
			base.dtModel.Columns.Add("TERRITORY",typeof(string));
			base.dtModel.Columns.Add("LOCATION_TYPE",typeof(int));

			base.dtModel.Columns.Add("RENTED_WEEKLY",typeof(string));
			base.dtModel.Columns.Add("WEEKS_RENTED",typeof(string));
			base.dtModel.Columns.Add("LOSSREPORT_ORDER",typeof(int));
			base.dtModel.Columns.Add("LOSSREPORT_DATETIME",typeof(DateTime));		
			base.dtModel.Columns.Add("REPORT_STATUS",typeof(string));	
	
            //added by Chetna on March 17,2010
            base.dtModel.Columns.Add("CAL_NUM", typeof(string));
            base.dtModel.Columns.Add("NAME", typeof(string));
            base.dtModel.Columns.Add("NUMBER", typeof(string));
            base.dtModel.Columns.Add("DISTRICT", typeof(string));
            base.dtModel.Columns.Add("OCCUPIED", typeof(int));
            base.dtModel.Columns.Add("EXT", typeof(string));
            base.dtModel.Columns.Add("CATEGORY", typeof(string));
            base.dtModel.Columns.Add("ACTIVITY_TYPE", typeof(int));
            base.dtModel.Columns.Add("CONSTRUCTION", typeof(int));
            base.dtModel.Columns.Add("SOURCE_LOCATION_ID", typeof(Int64));//Change as per the tfs#2701 itrack-1366
            base.dtModel.Columns.Add("IS_BILLING", typeof(string));
		}

		public void SetSublocations(ArrayList alSubloc)
		{
			alSublocations = alSubloc;
		}

		public ArrayList GetSublocations()
		{
			return alSublocations;
			
		}

		#region Database schema details

		// model for database field IS_PRIMARY(string)
		public string ID
		{
			get
			{
				return base.dtModel.Rows[0]["ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ID"] = value;
			}
		}
		
		public string ADDR_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ADDR_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADDR_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDR_TYPE"] = value;
			}
		}

		// model for database field LOCATION_TYPE(int)
		public int LOCATION_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOCATION_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_TYPE"] = value;
			}
		}
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
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_ID"] = value;
			}
		}
		// model for database field APP_VERSION_ID(int)
		public int POLICY_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field LOCATION_ID(int)
        //Modified int to Int64 as per the tfs#2701 itrack-1366
        public Int64 LOCATION_ID
		{
			get
			{
                return base.dtModel.Rows[0]["LOCATION_ID"] == DBNull.Value ? Convert.ToInt64(null) : Int64.Parse(base.dtModel.Rows[0]["LOCATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ID"] = value;
			}
		}
		// model for database field LOC_NUM(int)
        //Modified int to Int64 as per the tfs#2701 itrack-1366
        public Int64 LOC_NUM
		{
			get
			{
                return base.dtModel.Rows[0]["LOC_NUM"] == DBNull.Value ? Convert.ToInt64(null) : Int64.Parse(base.dtModel.Rows[0]["LOC_NUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOC_NUM"] = value;
			}
		}
		// model for database field IS_PRIMARY(string)
		public string IS_PRIMARY
		{
			get
			{
				return base.dtModel.Rows[0]["IS_PRIMARY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_PRIMARY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_PRIMARY"] = value;
			}
		}
		// model for database field LOC_ADD1(string)
		public string LOC_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_ADD1"] = value;
			}
		}
		// model for database field LOC_ADD2(string)
		public string LOC_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_ADD2"] = value;
			}
		}
		// model for database field LOC_CITY(string)
		public string LOC_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_CITY"] = value;
			}
		}
		// model for database field LOC_COUNTY(string)
		public string LOC_COUNTY
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_COUNTY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_COUNTY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_COUNTY"] = value;
			}
		}
		// model for database field LOC_STATE(string)
		public string LOC_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_STATE"] = value;
			}
		}
		// model for database field LOC_ZIP(string)
		public string LOC_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_ZIP"] = value;
			}
		}
		// model for database field LOC_COUNTRY(string)
		public string LOC_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_COUNTRY"] = value;
			}
		}
		// model for database field PHONE_NUMBER(string)
		public string PHONE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["PHONE_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PHONE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PHONE_NUMBER"] = value;
			}
		}
		// model for database field FAX_NUMBER(string)
		public string FAX_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["FAX_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FAX_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FAX_NUMBER"] = value;
			}
		}
		// model for database field DEDUCTIBLE(string)
		public string DEDUCTIBLE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCTIBLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE"] = value;
			}
		}
		// model for database field NAMED_PERILL(int)
		public int NAMED_PERILL
		{
			get
			{
				return base.dtModel.Rows[0]["NAMED_PERILL"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NAMED_PERILL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NAMED_PERILL"] = value;
			}
		}
		// model for database field DESCRIPTION(string)
		public string DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESCRIPTION"] = value;
			}
		}

		// model for database field DESCRIPTION(string)
		public string TERRITORY
		{
			get
			{
				return base.dtModel.Rows[0]["TERRITORY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TERRITORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TERRITORY"] = value;
			}
		}

		// model for database field WEEKS_RENTED(string)
		public string WEEKS_RENTED
		{
			get
			{
				return base.dtModel.Rows[0]["WEEKS_RENTED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WEEKS_RENTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WEEKS_RENTED"] = value;
			}
		}

		
		// model for database field RENTED_WEEKLY(string)
		public string RENTED_WEEKLY
		{
			get
			{
				return base.dtModel.Rows[0]["RENTED_WEEKLY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RENTED_WEEKLY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RENTED_WEEKLY"] = value;
			}
		}

		// model for database field LOSSREPORT_ORDER(int)
		public int LOSSREPORT_ORDER
		{
			get
			{
				return base.dtModel.Rows[0]["LOSSREPORT_ORDER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSSREPORT_ORDER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSSREPORT_ORDER"] = value;
			}
		}

		/// <summary>
		/// model for database field LOSSREPORT_DATETIME(datetime)
		/// </summary>
		public DateTime LOSSREPORT_DATETIME
		{
			get
			{
				return dtModel.Rows[0]["LOSSREPORT_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtModel.Rows[0]["LOSSREPORT_DATETIME"]);
			}
			set
			{
				dtModel.Rows[0]["LOSSREPORT_DATETIME"] = value;
			}
		}
		// model for database field REPORT_STATUS(string)
		public string REPORT_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["REPORT_STATUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REPORT_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REPORT_STATUS"] = value;
			}
		}

        // model for database field CAL_NUM(string)
        public string CAL_NUM
        {
            get
            {
                return base.dtModel.Rows[0]["CAL_NUM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CAL_NUM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CAL_NUM"] = value;
            }
        }

        // model for database field NAME(string)
        public string NAME
        {
            get
            {
                return base.dtModel.Rows[0]["NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NAME"] = value;
            }
        }

        // model for database field NUMBER(string)
        public string NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NUMBER"] = value;
            }
        }

        // model for database field DISTRICT(string)
        public string DISTRICT
        {
            get
            {
                return base.dtModel.Rows[0]["DISTRICT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DISTRICT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DISTRICT"] = value;
            }
        }

        // model for database field OCCUPIED(int)
        public int OCCUPIED
        {
            get
            {
                return base.dtModel.Rows[0]["OCCUPIED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["OCCUPIED"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["OCCUPIED"] = value;
            }
        }

        // model for database field EXT(string)
        public string EXT
        {
            get
            {
                return base.dtModel.Rows[0]["EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EXT"] = value;
            }
        }

        // model for database field CATEGORY(string)
        public string CATEGORY
        {
            get
            {
                return base.dtModel.Rows[0]["CATEGORY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CATEGORY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CATEGORY"] = value;
            }
        }

        // model for database field ACTIVITY_TYPE(int)
        public int ACTIVITY_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["ACTIVITY_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ACTIVITY_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACTIVITY_TYPE"] = value;
            }
        }

        // model for database field CONSTRUCTION(int)
        public int CONSTRUCTION
        {
            get
            {
                return base.dtModel.Rows[0]["CONSTRUCTION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CONSTRUCTION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CONSTRUCTION"] = value;
            }
        }
        //Modified int to Int64 as per the tfs#2701 itrack-1366
        public Int64 SOURCE_LOCATION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["SOURCE_LOCATION_ID"] == DBNull.Value ? Convert.ToInt64(null) : Int64.Parse(base.dtModel.Rows[0]["SOURCE_LOCATION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["SOURCE_LOCATION_ID"] = value;
            }
        }
        
        public string IS_BILLING
        {
            get
            {
                return base.dtModel.Rows[0]["IS_BILLING"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_BILLING"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_BILLING"] = value;
            }
        }

		#endregion
	}
}
