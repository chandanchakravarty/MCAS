/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	24/05/2006
<End Date				: -	
<Description			: - Models CLM_INSURED_BOAT
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
	/// Database Model for CLM_INSURED_BOAT.
	/// </summary>
	public class ClsInsuredBoatInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_INSURED_BOAT = "CLM_INSURED_BOAT";
		public ClsInsuredBoatInfo()
		{
			base.dtModel.TableName = "CLM_INSURED_BOAT";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_INSURED_BOAT
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));			
			base.dtModel.Columns.Add("BOAT_ID",typeof(int));
			base.dtModel.Columns.Add("SERIAL_NUMBER",typeof(string));			
			base.dtModel.Columns.Add("YEAR",typeof(int));
			base.dtModel.Columns.Add("MAKE",typeof(string));			
			base.dtModel.Columns.Add("MODEL",typeof(string));			
			base.dtModel.Columns.Add("BODY_TYPE",typeof(int));			
			base.dtModel.Columns.Add("LENGTH",typeof(string));
			base.dtModel.Columns.Add("WEIGHT",typeof(string));
			base.dtModel.Columns.Add("HORSE_POWER",typeof(double));			
			base.dtModel.Columns.Add("OTHER_HULL_TYPE",typeof(string));						
			base.dtModel.Columns.Add("PLATE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("STATE",typeof(int));			
			base.dtModel.Columns.Add("INCLUDE_TRAILER",typeof(int));			
			base.dtModel.Columns.Add("POLICY_BOAT_ID",typeof(int));
			base.dtModel.Columns.Add("WHERE_BOAT_SEEN",typeof(string));
			
		}
		#region Database schema details
		// model for database field WHERE_BOAT_SEEN(string)
		public string WHERE_BOAT_SEEN
		{
			get
			{
				return base.dtModel.Rows[0]["WHERE_BOAT_SEEN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["WHERE_BOAT_SEEN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WHERE_BOAT_SEEN"] = value;
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
		// model for database field BOAT_ID(int)
		public int BOAT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["BOAT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BOAT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BOAT_ID"] = value;
			}
		}
		// model for database field SERIAL_NUMBER(string)
		public string SERIAL_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["SERIAL_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SERIAL_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SERIAL_NUMBER"] = value;
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
		// model for database field MAKE(string)
		public string MAKE
		{
			get
			{
				return base.dtModel.Rows[0]["MAKE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAKE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAKE"] = value;
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
		// model for database field BODY_TYPE(int)
		public int BODY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["BODY_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BODY_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BODY_TYPE"] = value;
			}
		}		
		
		// model for database field LENGTH(string)
		public string LENGTH
		{
			get
			{
				return base.dtModel.Rows[0]["LENGTH"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LENGTH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LENGTH"] = value;
			}
		}	
		// model for database field WEIGHT(string)
		public string WEIGHT
		{
			get
			{
				return base.dtModel.Rows[0]["WEIGHT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WEIGHT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WEIGHT"] = value;
			}
		}		
		// model for database field HORSE_POWER(double)
		public double HORSE_POWER
		{
			get
			{
				return base.dtModel.Rows[0]["HORSE_POWER"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["HORSE_POWER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HORSE_POWER"] = value;
			}
		}
		// model for database field OTHER_HULL_TYPE(string)
		public string OTHER_HULL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_HULL_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_HULL_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_HULL_TYPE"] = value;
			}
		}
		
		// model for database field PLATE_NUMBER(string)
		public string PLATE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["PLATE_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PLATE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PLATE_NUMBER"] = value;
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
		// model for database field INCLUDE_TRAILER(int)
		public int INCLUDE_TRAILER
		{
			get
			{
				return base.dtModel.Rows[0]["INCLUDE_TRAILER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INCLUDE_TRAILER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INCLUDE_TRAILER"] = value;
			}
		}
		// model for database field POLICY_BOAT_ID(int)
		public int POLICY_BOAT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_BOAT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_BOAT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_BOAT_ID"] = value;
			}
		}
		#endregion
	}
}
