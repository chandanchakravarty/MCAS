using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Summary description for ClsPolicyOtherStructuresInfo.
	/// </summary>
	public class ClsPolicyOtherStructuresInfo:Cms.Model.ClsCommonModel
	{
		private const string POL_OTHER_STRUCTURE_DWELLING = "POL_OTHER_STRUCTURE_DWELLING";
		public string strCalledFrom="";
		public ClsPolicyOtherStructuresInfo()
		{
			base.dtModel.TableName = "POL_OTHER_STRUCTURE_DWELLING";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_OTHER_STRUCTURE_DWELLING
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			base.dtModel.Columns.Add("OTHER_STRUCTURE_ID",typeof(int));
			base.dtModel.Columns.Add("PREMISES_LOCATION",typeof(string));
			base.dtModel.Columns.Add("PREMISES_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("PREMISES_USE",typeof(string));
			base.dtModel.Columns.Add("PREMISES_CONDITION",typeof(string));
			base.dtModel.Columns.Add("PICTURE_ATTACHED",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_BASIS",typeof(string));
			base.dtModel.Columns.Add("SATELLITE_EQUIPMENT",typeof(string));
			base.dtModel.Columns.Add("LOCATION_ADDRESS",typeof(string));
			base.dtModel.Columns.Add("LOCATION_CITY",typeof(string));
			base.dtModel.Columns.Add("LOCATION_STATE",typeof(string));
			base.dtModel.Columns.Add("LOCATION_ZIP",typeof(string));
			base.dtModel.Columns.Add("ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED",typeof(double));
			base.dtModel.Columns.Add("INSURING_VALUE",typeof(double));
			base.dtModel.Columns.Add("INSURING_VALUE_OFF_PREMISES",typeof(double));
			base.dtModel.Columns.Add("COVERAGE_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("LIABILITY_EXTENDED",typeof(int));	
			base.dtModel.Columns.Add("SOLID_FUEL_DEVICE",typeof(string)); //Added by Charles on 27-Nov-09 for Itrack 6681
			base.dtModel.Columns.Add("APPLY_ENDS",typeof(string)); //Added by Charles on 3-Dec for Itrack 6405
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
		// model for database field OTHER_STRUCTURE_ID(int)
		public int OTHER_STRUCTURE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_STRUCTURE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["OTHER_STRUCTURE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_STRUCTURE_ID"] = value;
			}
		}
		// model for database field PREMISES_LOCATION(string)
		public string PREMISES_LOCATION
		{
			get
			{
				return base.dtModel.Rows[0]["PREMISES_LOCATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PREMISES_LOCATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREMISES_LOCATION"] = value;
			}
		}
		// model for database field PREMISES_DESCRIPTION(string)
		public string PREMISES_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["PREMISES_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PREMISES_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREMISES_DESCRIPTION"] = value;
			}
		}
		// model for database field PREMISES_USE(string)
		public string PREMISES_USE
		{
			get
			{
				return base.dtModel.Rows[0]["PREMISES_USE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PREMISES_USE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREMISES_USE"] = value;
			}
		}
		// model for database field PREMISES_CONDITION(string)
		public string PREMISES_CONDITION
		{
			get
			{
				return base.dtModel.Rows[0]["PREMISES_CONDITION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PREMISES_CONDITION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREMISES_CONDITION"] = value;
			}
		}
		// model for database field PICTURE_ATTACHED(string)
		public string PICTURE_ATTACHED
		{
			get
			{
				return base.dtModel.Rows[0]["PICTURE_ATTACHED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PICTURE_ATTACHED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PICTURE_ATTACHED"] = value;
			}
		}
		// model for database field COVERAGE_BASIS(string)
		public string COVERAGE_BASIS
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_BASIS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COVERAGE_BASIS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_BASIS"] = value;
			}
		}
		// model for database field SATELLITE_EQUIPMENT(string)
		public string SATELLITE_EQUIPMENT
		{
			get
			{
				return base.dtModel.Rows[0]["SATELLITE_EQUIPMENT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SATELLITE_EQUIPMENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SATELLITE_EQUIPMENT"] = value;
			}
		}
		// model for database field LOCATION_ADDRESS(string)
		public string LOCATION_ADDRESS
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ADDRESS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOCATION_ADDRESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ADDRESS"] = value;
			}
		}
		// model for database field LOCATION_CITY(string)
		public string LOCATION_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOCATION_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_CITY"] = value;
			}
		}
		// model for database field LOCATION_STATE(string)
		public string LOCATION_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOCATION_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_STATE"] = value;
			}
		}
		// model for database field LOCATION_ZIP(string)
		public string LOCATION_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOCATION_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ZIP"] = value;
			}
		}
		
		// model for database field ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED(double)
		public double ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED
		{
			get{return base.dtModel.Rows[0]["ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED"].ToString());}
			set{base.dtModel.Rows[0]["ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED"] = value;}
		}
		// model for database field INSURING_VALUE(int)
		public double INSURING_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["INSURING_VALUE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["INSURING_VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURING_VALUE"] = value;
			}
		}
		// model for database field INSURING_VALUE_OFF_PREMISES(int)
		public double INSURING_VALUE_OFF_PREMISES
		{
			get
			{
				return base.dtModel.Rows[0]["INSURING_VALUE_OFF_PREMISES"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["INSURING_VALUE_OFF_PREMISES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURING_VALUE_OFF_PREMISES"] = value;
			}
		}
		//Called From
		public string CalledFrom
		{
			get
			{
				return strCalledFrom.ToString();
			}
			set
			{
				strCalledFrom = value;
			}
		}		
		// model for database field COVERAGE_AMOUNT(int)
		public double COVERAGE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["COVERAGE_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_AMOUNT"] = value;
			}
		}
		// model for database field LIABILITY_EXTENDED(int)
		public int LIABILITY_EXTENDED
		{
			get
			{
				return base.dtModel.Rows[0]["LIABILITY_EXTENDED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIABILITY_EXTENDED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIABILITY_EXTENDED"] = value;
			}
		}
		//Added by Charles on 27-Nov-09 for Itrack 6681
		public string SOLID_FUEL_DEVICE
		{
			get
			{
				return base.dtModel.Rows[0]["SOLID_FUEL_DEVICE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SOLID_FUEL_DEVICE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SOLID_FUEL_DEVICE"] = value;
			}
		}
		//Added by Charles on 3-Dec-09 for Itrack 6405
		public string APPLY_ENDS
		{
			get
			{
				return base.dtModel.Rows[0]["APPLY_ENDS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["APPLY_ENDS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APPLY_ENDS"] = value;
			}
		}
		#endregion

	}
}
