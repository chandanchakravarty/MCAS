/******************************************************************************************
<Author				: -   Amar
<Start Date				: -	5/1/2006 5:06:18 PM
<End Date				: -	
<Description				: - 	
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
	/// Database ADDRESS1 for CLM_WATERCRAFT_PROPERTY_DAMAGED.
	/// </summary>
	public class ClsWatercraftPropertyDamagedInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_WATERCRAFT_PROPERTY_DAMAGED = "CLM_WATERCRAFT_PROPERTY_DAMAGED";
		public ClsWatercraftPropertyDamagedInfo()
		{
			base.dtModel.TableName = "CLM_WATERCRAFT_PROPERTY_DAMAGED";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_WATERCRAFT_PROPERTY_DAMAGED
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("PROPERTY_DAMAGED_ID",typeof(int));			
			base.dtModel.Columns.Add("DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("OTHER_VEHICLE",typeof(string));
			base.dtModel.Columns.Add("OTHER_INSURANCE_NAME",typeof(string));									
			base.dtModel.Columns.Add("OTHER_OWNER_NAME",typeof(string));
			base.dtModel.Columns.Add("ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));						
			base.dtModel.Columns.Add("ZIP",typeof(string));
			base.dtModel.Columns.Add("HOME_PHONE",typeof(string));
			base.dtModel.Columns.Add("WORK_PHONE",typeof(string));			
			
		}
		#region Database schema details
		// ADDRESS1 for database field CLAIM_ID(int)
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
		// ADDRESS1 for database field PROPERTY_DAMAGED_ID(int)
		public int PROPERTY_DAMAGED_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROPERTY_DAMAGED_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PROPERTY_DAMAGED_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROPERTY_DAMAGED_ID"] = value;
			}
		}
		
		// ADDRESS1 for database field DESCRIPTION(string)
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
		// ADDRESS1 for database field OTHER_VEHICLE(string)
		public string OTHER_VEHICLE
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_VEHICLE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_VEHICLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_VEHICLE"] = value;
			}
		}
		// ADDRESS1 for database field OTHER_INSURANCE_NAME(string)
		public string OTHER_INSURANCE_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_INSURANCE_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_INSURANCE_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_INSURANCE_NAME"] = value;
			}
		}
		// ADDRESS1 for database field OTHER_OWNER_NAME(string)
		public string OTHER_OWNER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_OWNER_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_OWNER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_OWNER_NAME"] = value;
			}
		}
		// ADDRESS1 for database field ADDRESS1(string)
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
		// ADDRESS1 for database field ADDRESS2(string)
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
		// ADDRESS1 for database field CITY(string)
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
		
		// ADDRESS1 for database field ZIP(string)
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
		// ADDRESS1 for database field STATE(int)
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
		// ADDRESS1 for database field HOME_PHONE(string)
		public string HOME_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["HOME_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOME_PHONE"] = value;
			}
		}	
		// ADDRESS1 for database field WORK_PHONE(string)
		public string WORK_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["WORK_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["WORK_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WORK_PHONE"] = value;
			}
		}	
		#endregion
	}
}
