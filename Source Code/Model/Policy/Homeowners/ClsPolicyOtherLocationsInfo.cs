#region Page Info
/******************************************************************************************
<Author					: - Swastika Gaur  
<Start Date				: -	6/13/2006 5:55:51 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
#endregion

using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Summary description for ClsPolicyOtherLocationsInfo.
	/// </summary>
	public class ClsPolicyOtherLocationsInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_OTHER_LOCATIONS = "POL_OTHER_LOCATIONS";	
		public ClsPolicyOtherLocationsInfo()
		{
			base.dtModel.TableName = "POL_OTHER_LOCATIONS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_OTHER_LOCATIONS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
	
		}


		#region Add Columns
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("LOC_NUM",typeof(int));
			base.dtModel.Columns.Add("LOC_ADD1",typeof(string));
			base.dtModel.Columns.Add("LOC_CITY",typeof(string));
			base.dtModel.Columns.Add("LOC_COUNTY",typeof(string));
			base.dtModel.Columns.Add("LOC_STATE",typeof(string));
			base.dtModel.Columns.Add("LOC_ZIP",typeof(string));
			base.dtModel.Columns.Add("PHOTO_ATTACHED",typeof(int));
			base.dtModel.Columns.Add("OCCUPIED_BY_INSURED",typeof(int));
			base.dtModel.Columns.Add("DESCRIPTION",typeof(string));
		}
		#endregion

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
		// model for database field DWELLING_ID(int)
		public int DWELLING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DWELLING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_ID"] = value;
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
		// model for database field PHOTO_ATTACHED(int)
		public int PHOTO_ATTACHED
		{
			get
			{
				return base.dtModel.Rows[0]["PHOTO_ATTACHED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PHOTO_ATTACHED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PHOTO_ATTACHED"] = value;
			}
		}
		// model for database field OCCUPIED_BY_INSURED(int)
		public int OCCUPIED_BY_INSURED
		{
			get
			{
				return base.dtModel.Rows[0]["OCCUPIED_BY_INSURED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["OCCUPIED_BY_INSURED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OCCUPIED_BY_INSURED"] = value;
			}
		}
		// model for database field DESCRIPTION(string)
		public string DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESCRIPTION"] = value;
			}
		}
		#endregion
	}
}
