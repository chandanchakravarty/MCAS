/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	5/1/2006 3:21:07 PM
<End Date				: -	
<Description				: - 	Model Class for Insured Location
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_INSURED_LOCATION.
	/// </summary>
	public class ClsInsuredLocationInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_INSURED_LOCATION = "CLM_INSURED_LOCATION";
		public ClsInsuredLocationInfo()
		{
			base.dtModel.TableName = "CLM_INSURED_LOCATION";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_INSURED_LOCATION
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("INSURED_LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("LOCATION_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));
			base.dtModel.Columns.Add("ZIP",typeof(string));
			base.dtModel.Columns.Add("COUNTRY",typeof(int));
			base.dtModel.Columns.Add("POLICY_LOCATION_ID",typeof(int));
		}
		#region Database schema details
		// model for database field INSURED_LOCATION_ID(int)
		public int INSURED_LOCATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED_LOCATION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INSURED_LOCATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURED_LOCATION_ID"] = value;
			}
		}
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field LOCATION_DESCRIPTION(string)
		public string LOCATION_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOCATION_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_DESCRIPTION"] = value;
			}
		}
		// model for database field ADDRESS1(string)
		public string ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS1"] = value;
			}
		}
		// model for database field ADDRESS2(string)
		public string ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS2"] = value;
			}
		}
		// model for database field CITY(string)
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
		public int STATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE"] = value;
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
		// model for database field COUNTRY(int)
		public int COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COUNTRY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY"] = value;
			}
		}
		// model for database field POLICY_LOCATION_ID(int)
		public int POLICY_LOCATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_LOCATION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_LOCATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_LOCATION_ID"] = value;
			}
		}
		#endregion
	}
}
