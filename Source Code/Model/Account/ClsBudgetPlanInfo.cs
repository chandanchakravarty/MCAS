/******************************************************************************************
<Author				      : -   Manoj Rathore
<Start Date				  : -	6/22/2007 10:03:05 AM
<End Date				  : -	
<Description			  : - 	Model for Budget Plan.
<Review Date			  : - 
<Reviewed By			  : - 	
Modification History
<Modified Date			  : - 
<Modified By			  : - 
<Purpose				  : - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Account
{
	/// <summary>
	/// Summary description for ClsBudgetPlanInfo.
	/// </summary>
	public class ClsBudgetPlanInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_BUDGET_PLAN = "ACT_BUDGET_PLAN";

		public ClsBudgetPlanInfo()
		{
			base.dtModel.TableName = "ACT_BUDGET_PLAN";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_BUDGET_PLAN
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			
			
			base.dtModel.Columns.Add("IDEN_ROW_ID",typeof(int));
			base.dtModel.Columns.Add("GL_ID",typeof(int));
			base.dtModel.Columns.Add("FISCAL_ID",typeof(int));
			base.dtModel.Columns.Add("BUDGET_CATEGORY_ID",typeof(int));
			base.dtModel.Columns.Add("JAN_BUDGET",typeof(double));
			base.dtModel.Columns.Add("FEB_BUDGET",typeof(double));
			base.dtModel.Columns.Add("MARCH_BUDGET",typeof(double));
			base.dtModel.Columns.Add("APRIL_BUDGET",typeof(double));
			base.dtModel.Columns.Add("MAY_BUDGET",typeof(double));
			base.dtModel.Columns.Add("JUNE_BUDGET",typeof(double));
			base.dtModel.Columns.Add("JULY_BUDGET",typeof(double));
			base.dtModel.Columns.Add("AUG_BUDGET",typeof(double));
			base.dtModel.Columns.Add("SEPT_BUDGET",typeof(double));
			base.dtModel.Columns.Add("OCT_BUDGET",typeof(double));
			base.dtModel.Columns.Add("NOV_BUDGET",typeof(double));
			base.dtModel.Columns.Add("DEC_BUDGET",typeof(double));
			base.dtModel.Columns.Add("ACCOUNT_ID",typeof(int));

			
		}
		#region Database schema details
		// model for database field GL_ID(int)
		public int IDEN_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["IDEN_ROW_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IDEN_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IDEN_ROW_ID"] = value;
			}
		}

		public int GL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["GL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["GL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GL_ID"] = value;
			}
		}
		public int FISCAL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FISCAL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FISCAL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FISCAL_ID"] = value;
			}
		}
		// model for database field BUDGET_CATEGORY_ID(int)
		public int BUDGET_CATEGORY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["BUDGET_CATEGORY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BUDGET_CATEGORY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BUDGET_CATEGORY_ID"] = value;
			}
		}
		// model for database field JAN_BUDGET(double)
		public double JAN_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["JAN_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["JAN_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["JAN_BUDGET"] = value;
			}
		}
		// model for database field FEB_BUDGET(double)
		public double FEB_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["FEB_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["FEB_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FEB_BUDGET"] = value;
			}
		}
		// model for database field MARCH_BUDGET(double)
		public double MARCH_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["MARCH_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MARCH_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MARCH_BUDGET"] = value;
			}
		}
		// model for database field APRIL_BUDGET(double)
		public double APRIL_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["APRIL_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["APRIL_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APRIL_BUDGET"] = value;
			}
		}
		// model for database field MAY_BUDGET(double)
		public double MAY_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["MAY_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MAY_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MAY_BUDGET"] = value;
			}
		}
		// model for database field JUNE_BUDGET(double)
		public double JUNE_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["JUNE_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["JUNE_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["JUNE_BUDGET"] = value;
			}
		}
		// model for database field JULY_BUDGET(double)
		public double JULY_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["JULY_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["JULY_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["JULY_BUDGET"] = value;
			}
		}
		// model for database field AUG_BUDGET(double)
		public double AUG_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["AUG_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["AUG_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AUG_BUDGET"] = value;
			}
		}
		// model for database field SEPT_BUDGET(double)
		public double SEPT_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["SEPT_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["SEPT_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SEPT_BUDGET"] = value;
			}
		}
		// model for database field OCT_BUDGET(double)
		public double OCT_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["OCT_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["OCT_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OCT_BUDGET"] = value;
			}
		}
		// model for database field CHECK_AMOUNT(double)
		public double NOV_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["NOV_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["NOV_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NOV_BUDGET"] = value;
			}
		}
		// model for database field DEC_BUDGET(double)
		public double DEC_BUDGET
		{
			get
			{
				return base.dtModel.Rows[0]["DEC_BUDGET"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["DEC_BUDGET"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEC_BUDGET"] = value;
			}
		}

		public int ACCOUNT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ACCOUNT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACCOUNT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACCOUNT_ID"] = value;
			}
		}

		#endregion
	}
}
