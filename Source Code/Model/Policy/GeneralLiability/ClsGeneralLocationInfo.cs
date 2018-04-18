/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	8/23/2005 11:25:28 AM
<End Date				: -	
<Description				: - 	Model class for location
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
namespace Cms.Model.Policy.GeneralLiability
{
	/// <summary>
	/// Database Model for POL_LOCATIONS.
	/// </summary>
	public class ClsLocationsInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_LOCATIONS = "POL_LOCATIONS";
		public ClsLocationsInfo()
		{
			base.dtModel.TableName = "POL_LOCATIONS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_LOCATIONS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("LOC_NUM",typeof(int));
			base.dtModel.Columns.Add("LOC_ADD1",typeof(string));
			base.dtModel.Columns.Add("LOC_ADD2",typeof(string));
			base.dtModel.Columns.Add("LOC_CITY",typeof(string));
			base.dtModel.Columns.Add("LOC_COUNTY",typeof(string));
			base.dtModel.Columns.Add("LOC_STATE",typeof(string));
			base.dtModel.Columns.Add("LOC_ZIP",typeof(string));
			base.dtModel.Columns.Add("LOC_TERRITORY",typeof(string));
			base.dtModel.Columns.Add("LOC_COUNTRY",typeof(string));


		}
		#region Database schema details
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
		// model for database field LOC_NUM(int)
		public int LOC_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_NUM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOC_NUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOC_NUM"] = value;
			}
		}
		// model for database field LOC_ADD1(string)
		public string LOC_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_ADD1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOC_ADD1"].ToString();
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
				return base.dtModel.Rows[0]["LOC_ADD2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOC_ADD2"].ToString();
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
				return base.dtModel.Rows[0]["LOC_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOC_CITY"].ToString();
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
				return base.dtModel.Rows[0]["LOC_COUNTY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOC_COUNTY"].ToString();
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
				return base.dtModel.Rows[0]["LOC_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOC_STATE"].ToString();
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
				return base.dtModel.Rows[0]["LOC_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOC_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_ZIP"] = value;
			}
		}
		// model for database field TERRITORY(string)
		public string LOC_TERRITORY
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_TERRITORY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_TERRITORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_TERRITORY"] = value;
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
		#endregion
	}
}
