/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	10/3/2005 4:27:16 PM
<End Date			: -	
<Description		: - 	Database Model for ACT_BUDGET_CATEGORY
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
	/// Database Model for ACT_BUDGET_CATEGORY.
	/// </summary>
	public class ClsBudgetCategoryInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_BUDGET_CATEGORY = "ACT_BUDGET_CATEGORY";
		public ClsBudgetCategoryInfo()
		{
			base.dtModel.TableName = "ACT_BUDGET_CATEGORY";	// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_BUDGET_CATEGORY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CATEGEORY_ID",typeof(int));
			base.dtModel.Columns.Add("CATEGEORY_CODE",typeof(int));
			base.dtModel.Columns.Add("CATEGORY_DEPARTEMENT_NAME",typeof(string));
			base.dtModel.Columns.Add("RESPONSIBLE_EMPLOYEE_NAME",typeof(string));
		}
		#region Database schema details
		// model for database field CATEGEORY_ID(int)
		public int CATEGEORY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CATEGEORY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CATEGEORY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CATEGEORY_ID"] = value;
			}
		}
		// model for database field CATEGEORY_CODE(int)
		public int CATEGEORY_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["CATEGEORY_CODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CATEGEORY_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CATEGEORY_CODE"] = value;
			}
		}
		// model for database field CATEGORY_DEPARTEMENT_NAME(string)
		public string CATEGORY_DEPARTEMENT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CATEGORY_DEPARTEMENT_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CATEGORY_DEPARTEMENT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CATEGORY_DEPARTEMENT_NAME"] = value;
			}
		}
		// model for database field RESPONSIBLE_EMPLOYEE_NAME(string)
		public string RESPONSIBLE_EMPLOYEE_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["RESPONSIBLE_EMPLOYEE_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RESPONSIBLE_EMPLOYEE_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RESPONSIBLE_EMPLOYEE_NAME"] = value;
			}
		}
		#endregion
	}
}
