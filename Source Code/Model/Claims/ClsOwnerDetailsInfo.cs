/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	26/04/2006
<End Date				: -	
<Description			: - Models CLM_OWNER_INFORMATION
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
	/// Database Model for CLM_OWNER_INFORMATION.
	/// </summary>
	public class ClsOwnerDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_OWNER_INFORMATION = "CLM_OWNER_INFORMATION";
		public ClsOwnerDetailsInfo()
		{
			base.dtModel.TableName = "CLM_OWNER_INFORMATION";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_OWNER_INFORMATION
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("OWNER_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("TYPE_OF_OWNER",typeof(int));
			base.dtModel.Columns.Add("NAME",typeof(string));
			base.dtModel.Columns.Add("ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));			
			base.dtModel.Columns.Add("ZIP",typeof(string));						
			base.dtModel.Columns.Add("HOME_PHONE",typeof(string));
			base.dtModel.Columns.Add("MOBILE_PHONE",typeof(string));
			base.dtModel.Columns.Add("DEFAULT_PHONE_TO_NOTICE",typeof(string));
			base.dtModel.Columns.Add("PRODUCTS_INSURED_IS",typeof(int));			
			base.dtModel.Columns.Add("OTHER_DESCRIPTION",typeof(string));			
			base.dtModel.Columns.Add("TYPE_OF_PRODUCT",typeof(string));
			base.dtModel.Columns.Add("WHERE_PRODUCT_SEEN",typeof(string));
			base.dtModel.Columns.Add("OTHER_LIABILITY",typeof(string));
			base.dtModel.Columns.Add("WORK_PHONE",typeof(string));
			base.dtModel.Columns.Add("EXTENSION",typeof(string));
			base.dtModel.Columns.Add("VEHICLE_OWNER",typeof(int));
			base.dtModel.Columns.Add("TYPE_OF_HOME",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("COUNTRY",typeof(int));
			
		}
		#region Database schema details
		// model for database field OWNER_ID(int)
		public int OWNER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["OWNER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["OWNER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OWNER_ID"] = value;
			}
		}
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field TYPE_OF_OWNER(int)
		public int TYPE_OF_OWNER
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE_OF_OWNER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TYPE_OF_OWNER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TYPE_OF_OWNER"] = value;
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
		// model for database field ADDRESS1(string)
		public string ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADDRESS1"].ToString();
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
				return base.dtModel.Rows[0]["ADDRESS2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADDRESS2"].ToString();
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
				return base.dtModel.Rows[0]["CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CITY"].ToString();
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
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STATE"].ToString());
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
				return base.dtModel.Rows[0]["ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ZIP"] = value;
			}
		}
				
		// model for database field HOME_PHONE(string)
		public string HOME_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOME_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOME_PHONE"] = value;
			}
		}
		
		// model for database field MOBILE_PHONE(string)
		public string MOBILE_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["MOBILE_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MOBILE_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MOBILE_PHONE"] = value;
			}
		}		
		// model for database field DEFAULT_PHONE_TO_NOTICE(string)
		public string DEFAULT_PHONE_TO_NOTICE
		{
			get
			{
				return base.dtModel.Rows[0]["DEFAULT_PHONE_TO_NOTICE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEFAULT_PHONE_TO_NOTICE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEFAULT_PHONE_TO_NOTICE"] = value;
			}
		}	
		// model for database field PRODUCTS_INSURED_IS(int)
		public int PRODUCTS_INSURED_IS
		{
			get
			{
				return base.dtModel.Rows[0]["PRODUCTS_INSURED_IS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PRODUCTS_INSURED_IS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRODUCTS_INSURED_IS"] = value;
			}
		}
		// model for database field OTHER_DESCRIPTION(string)
		public string OTHER_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_DESCRIPTION"] = value;
			}
		}		
		
		// model for database field TYPE_OF_PRODUCT(string)
		public string TYPE_OF_PRODUCT
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE_OF_PRODUCT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TYPE_OF_PRODUCT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TYPE_OF_PRODUCT"] = value;
			}
		}	
		// model for database field WHERE_PRODUCT_SEEN(string)
		public string WHERE_PRODUCT_SEEN
		{
			get
			{
				return base.dtModel.Rows[0]["WHERE_PRODUCT_SEEN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WHERE_PRODUCT_SEEN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WHERE_PRODUCT_SEEN"] = value;
			}
		}			
		
		// model for database field OTHER_LIABILITY(string)
		public string OTHER_LIABILITY
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_LIABILITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_LIABILITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_LIABILITY"] = value;
			}
		}		
		
		// model for database field WORK_PHONE(string)
		public string WORK_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["WORK_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WORK_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WORK_PHONE"] = value;
			}
		}	
		// model for database field EXTENSION(string)
		public string EXTENSION
		{
			get
			{
				return base.dtModel.Rows[0]["EXTENSION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXTENSION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXTENSION"] = value;
			}
		}			
		// model for database field VEHICLE_OWNER(int)
		public int VEHICLE_OWNER
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_OWNER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["VEHICLE_OWNER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_OWNER"] = value;
			}
		}
		// model for database field TYPE_OF_HOME(string)
		public string TYPE_OF_HOME
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE_OF_HOME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TYPE_OF_HOME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TYPE_OF_HOME"] = value;
			}
		}		
		// model for database field VEHICLE_ID(int)
		public int VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ID"] = value;
			}
		}
		// model for database field COUNTRY(int)
		public int COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COUNTRY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY"] = value;
			}
		}				
		#endregion
	}
}
