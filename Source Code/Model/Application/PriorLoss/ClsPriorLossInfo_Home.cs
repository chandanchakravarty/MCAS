using System;

namespace Cms.Model.Application.PriorLoss
{
	/// <summary>
	/// Summary description for ClsPriorLossInfo_Home.
	/// </summary>
	public class ClsPriorLossInfo_Home :Cms.Model.ClsCommonModel
	{
		 private const string PRIOR_LOSS_HOME = "PRIOR_LOSS_HOME";
		public ClsPriorLossInfo_Home()
		{
			base.dtModel.TableName = "PRIOR_LOSS_HOME";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_PRIOR_LOSS_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("PRIOR_LOSS_ID",typeof(int));        
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));        
			base.dtModel.Columns.Add("LOSS_ID",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("LOSS_ADD1",typeof(string));
			base.dtModel.Columns.Add("LOSS_ADD2",typeof(string));
			base.dtModel.Columns.Add("LOSS_CITY",typeof(string));
			base.dtModel.Columns.Add("LOSS_STATE",typeof(string));
			base.dtModel.Columns.Add("LOSS_ZIP",typeof(string));
			base.dtModel.Columns.Add("CURRENT_ADD1",typeof(string));
			base.dtModel.Columns.Add("CURRENT_ADD2",typeof(string));
			base.dtModel.Columns.Add("CURRENT_CITY",typeof(string));
			base.dtModel.Columns.Add("CURRENT_STATE",typeof(string));
			base.dtModel.Columns.Add("CURRENT_ZIP",typeof(string));
			base.dtModel.Columns.Add("POLICY_TYPE",typeof(string));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("LOSS_CARRIER",typeof(string));
			base.dtModel.Columns.Add("WATERBACKUP_SUMPPUMP_LOSS",typeof(int)); //Added by Charles on 30-Nov-09 for Itrack 6647
			base.dtModel.Columns.Add("WEATHER_RELATED_LOSS",typeof(int)); //Added for Itrack 6640 on 9 Dec 09
			
		}
		#region Database schema details
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
		// model for database field LOSS_ID(int)
		public int LOSS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_ID"] = value;
			}
		}

		// model for database field @LOCATION_ID(int)
		public int LOCATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOCATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ID"] = value;
			}
		}
     




		// model for database field PRIOR_LOSS_ID(int)
		public int PRIOR_LOSS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PRIOR_LOSS_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PRIOR_LOSS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRIOR_LOSS_ID"] = value;
			}
		}
     
		// model for database field LOSS_ADD1(string)
		public string LOSS_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_ADD1"] = value;
			}
		}
		// model for database field LOSS_ADD2(string)
		public string LOSS_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_ADD2"] = value;
			}
		}
		
		
		// model for database field LOSS_CITY(string)
		public string LOSS_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_CITY"] = value;
			}
		}
		// model for database field LOSS_STATE(string)
		public string LOSS_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_STATE"] = value;
			}
		}
		// model for database field LOSS_ZIP(string)
		public string LOSS_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_ZIP"] = value;
			}
		}
		// model for database field CURRENT_ADD1(string)
		public string CURRENT_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["CURRENT_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CURRENT_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CURRENT_ADD1"] = value;
			}
		}
		// model for database field CURRENT_ADD2(string)
		public string CURRENT_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["CURRENT_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CURRENT_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CURRENT_ADD2"] = value;
			}
		}
		// model for database field CURRENT_CITY(string)
		public string CURRENT_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["CURRENT_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CURRENT_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CURRENT_CITY"] = value;
			}
		}
		// model for database field CURRENT_STATE(string)
		public string CURRENT_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["CURRENT_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CURRENT_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CURRENT_STATE"] = value;
			}
		}
		// model for database field CURRENT_ZIP(string)
		public string CURRENT_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["CURRENT_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CURRENT_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CURRENT_ZIP"] = value;
			}
		}
		// model for database field POLICY_TYPE(string)
		public string POLICY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TYPE"] = value;
			}
		}
		// model for database field CURRENT_ZIP(string)
		public string POLICY_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NUMBER"] = value;
			}
		}
		 //Added by Charles on 30-Nov-09 for Itrack 6647
		public int WATERBACKUP_SUMPPUMP_LOSS
		{
			get
			{
				return base.dtModel.Rows[0]["WATERBACKUP_SUMPPUMP_LOSS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["WATERBACKUP_SUMPPUMP_LOSS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WATERBACKUP_SUMPPUMP_LOSS"] = value;
			}
		}
		//Added till here

		// model for database field WEATHER_RELATED_LOSS(int)
		//Added for Itrack 6640 on 9 Dec 09
		public int WEATHER_RELATED_LOSS
		{
			get
			{
				return base.dtModel.Rows[0]["WEATHER_RELATED_LOSS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["WEATHER_RELATED_LOSS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WEATHER_RELATED_LOSS"] = value;
			}
		}
		//Added till here
		#endregion
	}
}
