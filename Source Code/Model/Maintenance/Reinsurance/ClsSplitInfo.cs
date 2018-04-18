/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 27, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for the Reinsurance ClsSplitInfo
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/
using System;
using System.Data;
using Cms.Model;


namespace Cms.Model.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ClsSplitInfo.
	/// </summary>
	public class ClsSplitInfo:Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 

		public ClsSplitInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REIN_SPLIT";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			
			base.dtModel.Columns.Add("REIN_SPLIT_DEDUCTION_ID",typeof(int)); //1
			base.dtModel.Columns.Add("REIN_EFFECTIVE_DATE",typeof(DateTime));//2
			base.dtModel.Columns.Add("REIN_LINE_OF_BUSINESS",typeof(string));//3
			base.dtModel.Columns.Add("REIN_STATE",typeof(string));//4
			base.dtModel.Columns.Add("REIN_COVERAGE",typeof(string));//5
			base.dtModel.Columns.Add("REIN_IST_SPLIT",typeof(string)); //6
			base.dtModel.Columns.Add("REIN_IST_SPLIT_COVERAGE",typeof(string));//7
			base.dtModel.Columns.Add("REIN_2ND_SPLIT",typeof(string));//8
			base.dtModel.Columns.Add("REIN_2ND_SPLIT_COVERAGE",typeof(string));//9
			base.dtModel.Columns.Add("POLICY_TYPE",typeof(string)); //10

			//base.dtModel.Columns.Add("IS_ACTIVE",typeof(string));//32
			//base.dtModel.Columns.Add("CREATED_BY",typeof(int));//33
			//base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));//34
			//base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));//35
			//base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));//36
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		#region REIN_SPLIT_DEDUCTION_ID

		public int REIN_SPLIT_DEDUCTION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_SPLIT_DEDUCTION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_SPLIT_DEDUCTION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_SPLIT_DEDUCTION_ID"] = value;
			}
		}
		# endregion
		#region REIN_EFFECTIVE_DATE

		public DateTime REIN_EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["REIN_EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_EFFECTIVE_DATE"] = value;
			}
		}
		#endregion
		#region REIN_LINE_OF_BUSINESS

		public string REIN_LINE_OF_BUSINESS
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_LINE_OF_BUSINESS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_LINE_OF_BUSINESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_LINE_OF_BUSINESS"] = value;
			}
		}
		#endregion
		#region REIN_STATE

		public string REIN_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_STATE"] = value;
			}
		}
		#endregion
		#region REIN_COVERAGE

		public string REIN_COVERAGE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_COVERAGE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_COVERAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_COVERAGE"] = value;
			}
		}
		#endregion
		#region REIN_IST_SPLIT

		public string REIN_IST_SPLIT
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_IST_SPLIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_IST_SPLIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_IST_SPLIT"] = value;
			}
		}
		#endregion
		#region REIN_IST_SPLIT_COVERAGE

		public string REIN_IST_SPLIT_COVERAGE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_IST_SPLIT_COVERAGE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_IST_SPLIT_COVERAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_IST_SPLIT_COVERAGE"] = value;
			}
		}
		#endregion
		#region REIN_2ND_SPLIT

		public string REIN_2ND_SPLIT
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_2ND_SPLIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_2ND_SPLIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_2ND_SPLIT"] = value;
			}
		}
		#endregion
		#region REIN_2ND_SPLIT_COVERAGE

		public string REIN_2ND_SPLIT_COVERAGE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_2ND_SPLIT_COVERAGE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_2ND_SPLIT_COVERAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_2ND_SPLIT_COVERAGE"] = value;
			}
		}
		#endregion

		

		#region POLICY_TYPE

		public string POLICY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TYPE"] = value;
			}
		}
		# endregion

		# endregion
		
	}
}


