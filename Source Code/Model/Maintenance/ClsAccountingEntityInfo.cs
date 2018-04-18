/******************************************************************************************
<Author : - Gaurav Tyagi
<Start Date : - 4/15/2005 3:38:47 PM
<End Date : - 
<Description : - Modal For Accounting Entity.
<Review Date : - 
<Reviewed By : - 
Modification History
<Modified Date : - 
<Modified By : - 
<Purpose : - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_ACCOUNTING_ENTITY_LIST.
	/// </summary>
	public class ClsAccountingInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_ACCOUNTING_ENTITY_LIST = "MNT_ACCOUNTING_ENTITY_LIST";
		public ClsAccountingInfo()
		{
			base.dtModel.TableName = "MNT_ACCOUNTING_ENTITY_LIST"; // setting table name for data table that holds property values.
			this.AddColumns(); // add columns of the database table MNT_ACCOUNTING_ENTITY_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow()); // add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("REC_ID",typeof(int));
			base.dtModel.Columns.Add("ENTITY_ID",typeof(int));
			base.dtModel.Columns.Add("ENTITY_TYPE",typeof(string));
			base.dtModel.Columns.Add("DIVISION_ID",typeof(int));
			base.dtModel.Columns.Add("DEPARTMENT_ID",typeof(int));
			base.dtModel.Columns.Add("PROFIT_CENTER_ID",typeof(int));
		}
		#region Database schema details
		// model for database field REC_ID(int)
		public int REC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REC_ID"] = value;
			}
		}
		// model for database field ENTITY_ID(int)
		public int ENTITY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ENTITY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ENTITY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ENTITY_ID"] = value;
			}
		}
		// model for database field ENTITY_TYPE(string)
		public string ENTITY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ENTITY_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ENTITY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENTITY_TYPE"] = value;
			}
		}
		// model for database field DIVISION_ID(int)
		public int DIVISION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DIVISION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIVISION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIVISION_ID"] = value;
			}
		}
		// model for database field DEPARTMENT_ID(int)
		public int DEPARTMENT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DEPARTMENT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DEPARTMENT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPARTMENT_ID"] = value;
			}
		}
		// model for database field PROFIT_CENTER_ID(int)
		public int PROFIT_CENTER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROFIT_CENTER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PROFIT_CENTER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROFIT_CENTER_ID"] = value;
			}
		}
		#endregion
	}
}

