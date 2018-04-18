/******************************************************************************************
<Author					: -   Vijay Arora
<Start Date				: -	 11/18/2005 3:58:10 PM
<End Date				: -	
<Description				: - 	Model Class for Policy Home Owner Chimney Stove
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

namespace Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Database Model for POL_HOME_OWNER_CHIMNEY_STOVE.
	/// </summary>
	public class ClsPolicyChimneyStoveInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_HOME_OWNER_CHIMNEY_STOVE = "POL_HOME_OWNER_CHIMNEY_STOVE";
		public ClsPolicyChimneyStoveInfo()
		{
			base.dtModel.TableName = "POL_HOME_OWNER_CHIMNEY_STOVE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_HOME_OWNER_CHIMNEY_STOVE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("FUEL_ID",typeof(int));
			base.dtModel.Columns.Add("IS_STOVE_VENTED",typeof(string));
			base.dtModel.Columns.Add("OTHER_DEVICES_ATTACHED",typeof(string));
			base.dtModel.Columns.Add("CONSTRUCT_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("IS_TILE_FLUE_LINING",typeof(string));
			base.dtModel.Columns.Add("IS_CHIMNEY_GROUND_UP",typeof(string));
			base.dtModel.Columns.Add("CHIMNEY_INST_AFTER_HOUSE_BLT",typeof(string));
			base.dtModel.Columns.Add("IS_CHIMNEY_COVERED",typeof(string));
			base.dtModel.Columns.Add("DIST_FROM_SMOKE_PIPE",typeof(int));
			base.dtModel.Columns.Add("THIMBLE_OR_MATERIAL",typeof(string));
			base.dtModel.Columns.Add("STOVE_PIPE_IS",typeof(string));
			base.dtModel.Columns.Add("DOES_SMOKE_PIPE_FIT",typeof(string));
			base.dtModel.Columns.Add("SMOKE_PIPE_WASTE_HEAT",typeof(string));
			base.dtModel.Columns.Add("STOVE_CONN_SECURE",typeof(string));
			base.dtModel.Columns.Add("SMOKE_PIPE_PASS",typeof(string));
			base.dtModel.Columns.Add("SELECT_PASS",typeof(string));
			base.dtModel.Columns.Add("PASS_INCHES",typeof(double));//Changed from int by Charles on 21-Oct-09 for Itrack 6599
			base.dtModel.Columns.Add("CHIMNEY_CONSTRUCTION",typeof(string));
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
		// model for database field FUEL_ID(int)
		public int FUEL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FUEL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FUEL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FUEL_ID"] = value;
			}
		}
		// model for database field IS_STOVE_VENTED(string)
		public string IS_STOVE_VENTED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_STOVE_VENTED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_STOVE_VENTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_STOVE_VENTED"] = value;
			}
		}
		// model for database field OTHER_DEVICES_ATTACHED(string)
		public string OTHER_DEVICES_ATTACHED
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_DEVICES_ATTACHED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_DEVICES_ATTACHED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_DEVICES_ATTACHED"] = value;
			}
		}
		// model for database field CONSTRUCT_OTHER_DESC(string)
		public string CONSTRUCT_OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["CONSTRUCT_OTHER_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONSTRUCT_OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONSTRUCT_OTHER_DESC"] = value;
			}
		}
		// model for database field IS_TILE_FLUE_LINING(string)
		public string IS_TILE_FLUE_LINING
		{
			get
			{
				return base.dtModel.Rows[0]["IS_TILE_FLUE_LINING"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_TILE_FLUE_LINING"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_TILE_FLUE_LINING"] = value;
			}
		}
		// model for database field IS_CHIMNEY_GROUND_UP(string)
		public string IS_CHIMNEY_GROUND_UP
		{
			get
			{
				return base.dtModel.Rows[0]["IS_CHIMNEY_GROUND_UP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_CHIMNEY_GROUND_UP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_CHIMNEY_GROUND_UP"] = value;
			}
		}
		// model for database field CHIMNEY_INST_AFTER_HOUSE_BLT(string)
		public string CHIMNEY_INST_AFTER_HOUSE_BLT
		{
			get
			{
				return base.dtModel.Rows[0]["CHIMNEY_INST_AFTER_HOUSE_BLT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHIMNEY_INST_AFTER_HOUSE_BLT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHIMNEY_INST_AFTER_HOUSE_BLT"] = value;
			}
		}
		// model for database field IS_CHIMNEY_COVERED(string)
		public string IS_CHIMNEY_COVERED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_CHIMNEY_COVERED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_CHIMNEY_COVERED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_CHIMNEY_COVERED"] = value;
			}
		}
		// model for database field DIST_FROM_SMOKE_PIPE(int)
		public int DIST_FROM_SMOKE_PIPE
		{
			get
			{
				return base.dtModel.Rows[0]["DIST_FROM_SMOKE_PIPE"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["DIST_FROM_SMOKE_PIPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIST_FROM_SMOKE_PIPE"] = value;
			}
		}
		// model for database field THIMBLE_OR_MATERIAL(string)
		public string THIMBLE_OR_MATERIAL
		{
			get
			{
				return base.dtModel.Rows[0]["THIMBLE_OR_MATERIAL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["THIMBLE_OR_MATERIAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["THIMBLE_OR_MATERIAL"] = value;
			}
		}
		// model for database field STOVE_PIPE_IS(string)
		public string STOVE_PIPE_IS
		{
			get
			{
				return base.dtModel.Rows[0]["STOVE_PIPE_IS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STOVE_PIPE_IS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STOVE_PIPE_IS"] = value;
			}
		}
		// model for database field DOES_SMOKE_PIPE_FIT(string)
		public string DOES_SMOKE_PIPE_FIT
		{
			get
			{
				return base.dtModel.Rows[0]["DOES_SMOKE_PIPE_FIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DOES_SMOKE_PIPE_FIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DOES_SMOKE_PIPE_FIT"] = value;
			}
		}
		// model for database field SMOKE_PIPE_WASTE_HEAT(string)
		public string SMOKE_PIPE_WASTE_HEAT
		{
			get
			{
				return base.dtModel.Rows[0]["SMOKE_PIPE_WASTE_HEAT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SMOKE_PIPE_WASTE_HEAT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SMOKE_PIPE_WASTE_HEAT"] = value;
			}
		}
		// model for database field STOVE_CONN_SECURE(string)
		public string STOVE_CONN_SECURE
		{
			get
			{
				return base.dtModel.Rows[0]["STOVE_CONN_SECURE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STOVE_CONN_SECURE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STOVE_CONN_SECURE"] = value;
			}
		}
		// model for database field SMOKE_PIPE_PASS(string)
		public string SMOKE_PIPE_PASS
		{
			get
			{
				return base.dtModel.Rows[0]["SMOKE_PIPE_PASS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SMOKE_PIPE_PASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SMOKE_PIPE_PASS"] = value;
			}
		}
		// model for database field SELECT_PASS(string)
		public string SELECT_PASS
		{
			get
			{
				return base.dtModel.Rows[0]["SELECT_PASS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SELECT_PASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SELECT_PASS"] = value;
			}
		}
		// model for database field PASS_INCHES(int)
		public double PASS_INCHES //Changed from int by Charles on 21-Oct-09 for Itrack 6599
		{
			get
			{
				return base.dtModel.Rows[0]["PASS_INCHES"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["PASS_INCHES"].ToString());//Changed from int.Parse by Charles on 21-Oct-09 for Itrack 6599
			}
			set
			{
				base.dtModel.Rows[0]["PASS_INCHES"] = value;
			}
		}
		// model for database field CHIMNEY_CONSTRUCTION(string)
		public string CHIMNEY_CONSTRUCTION
		{
			get
			{
				return base.dtModel.Rows[0]["CHIMNEY_CONSTRUCTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHIMNEY_CONSTRUCTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHIMNEY_CONSTRUCTION"] = value;
			}
		}
		#endregion
	}
}
