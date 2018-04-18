/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	26/04/2006
<End Date				: -	
<Description			: - Models CLM_DRIVER_INFORMATION
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
	/// Database Model for CLM_DRIVER_INFORMATION.
	/// </summary>
	public class ClsDriverDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_DRIVER_INFORMATION = "CLM_DRIVER_INFORMATION";
		public ClsDriverDetailsInfo()
		{
			base.dtModel.TableName = "CLM_DRIVER_INFORMATION";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_DRIVER_INFORMATION
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("DRIVER_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("TYPE_OF_DRIVER",typeof(int));
			base.dtModel.Columns.Add("NAME",typeof(string));
			base.dtModel.Columns.Add("ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));			
			base.dtModel.Columns.Add("ZIP",typeof(string));						
			base.dtModel.Columns.Add("HOME_PHONE",typeof(string));
			base.dtModel.Columns.Add("WORK_PHONE",typeof(string));
			base.dtModel.Columns.Add("EXTENSION",typeof(string));
			base.dtModel.Columns.Add("MOBILE_PHONE",typeof(string));
			base.dtModel.Columns.Add("DESCRIBE_DAMAGE",typeof(string));
			base.dtModel.Columns.Add("RELATION_INSURED",typeof(string));			
			base.dtModel.Columns.Add("LICENSE_NUMBER",typeof(string));			
			base.dtModel.Columns.Add("PURPOSE_OF_USE",typeof(string));
			base.dtModel.Columns.Add("USED_WITH_PERMISSION",typeof(string));
			base.dtModel.Columns.Add("OTHER_VEHICLE_INSURANCE",typeof(string));
			base.dtModel.Columns.Add("DATE_OF_BIRTH",typeof(DateTime));
			base.dtModel.Columns.Add("ESTIMATE_AMOUNT",typeof(double));			
			base.dtModel.Columns.Add("LICENSE_STATE",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("COUNTRY",typeof(int));
			base.dtModel.Columns.Add("SEX",typeof(string));
			base.dtModel.Columns.Add("SSN",typeof(string));
			base.dtModel.Columns.Add("VEHICLE_OWNER",typeof(int));
			base.dtModel.Columns.Add("TYPE_OF_OWNER",typeof(int));
			base.dtModel.Columns.Add("DRIVERS_INJURY",typeof(string));			
		}
		#region Database schema details
		// model for database field DRIVER_ID(int)
		public int DRIVER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DRIVER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ID"] = value;
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
		// model for database field TYPE_OF_DRIVER(int)
		public int TYPE_OF_DRIVER
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE_OF_DRIVER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TYPE_OF_DRIVER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TYPE_OF_DRIVER"] = value;
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
		// model for database field DRIVERS_INJURY(string)
		public string DRIVERS_INJURY
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVERS_INJURY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVERS_INJURY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVERS_INJURY"] = value;
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
		// model for database field DESCRIBE_DAMAGE(string)
		public string DESCRIBE_DAMAGE
		{
			get
			{
				return base.dtModel.Rows[0]["DESCRIBE_DAMAGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESCRIBE_DAMAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESCRIBE_DAMAGE"] = value;
			}
		}			
		// model for database field RELATION_INSURED(string)
		public string RELATION_INSURED
		{
			get
			{
				return base.dtModel.Rows[0]["RELATION_INSURED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RELATION_INSURED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RELATION_INSURED"] = value;
			}
		}
		// model for database field LICENSE_NUMBER(string)
		public string LICENSE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["LICENSE_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LICENSE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LICENSE_NUMBER"] = value;
			}
		}		
		
		// model for database field PURPOSE_OF_USE(string)
		public string PURPOSE_OF_USE
		{
			get
			{
				return base.dtModel.Rows[0]["PURPOSE_OF_USE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PURPOSE_OF_USE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PURPOSE_OF_USE"] = value;
			}
		}	
		// model for database field USED_WITH_PERMISSION(string)
		public string USED_WITH_PERMISSION
		{
			get
			{
				return base.dtModel.Rows[0]["USED_WITH_PERMISSION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USED_WITH_PERMISSION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USED_WITH_PERMISSION"] = value;
			}
		}			
		
		// model for database field OTHER_VEHICLE_INSURANCE(string)
		public string OTHER_VEHICLE_INSURANCE
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_VEHICLE_INSURANCE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_VEHICLE_INSURANCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_VEHICLE_INSURANCE"] = value;
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
		// model for database field LICENSE_STATE(int)
		public int LICENSE_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["LICENSE_STATE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LICENSE_STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LICENSE_STATE"] = value;
			}
		}
		// model for database field DATE_OF_BIRTH(DateTime)
		public DateTime DATE_OF_BIRTH
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_OF_BIRTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_OF_BIRTH"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_OF_BIRTH"] = value;
			}
		}
		// model for database field ESTIMATE_AMOUNT(double)
		public double ESTIMATE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["ESTIMATE_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ESTIMATE_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ESTIMATE_AMOUNT"] = value;
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
		// model for database field SEX(string)
		public string SEX
		{
			get
			{
				return base.dtModel.Rows[0]["SEX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SEX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SEX"] = value;
			}
		}	
		// model for database field SSN(string)
		public string SSN
		{
			get
			{
				return base.dtModel.Rows[0]["SSN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SSN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SSN"] = value;
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
		#endregion
	}
}
