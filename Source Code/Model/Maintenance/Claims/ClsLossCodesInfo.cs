/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	24/04/2006
<End Date				: -	
<Description				: - 	Models CLM_LOSS_CODES
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
namespace Cms.Model.Maintenance.Claims
{
	/// <summary>
	/// Database Model for CLM_LOSS_CODES.
	/// </summary>
	public class ClsLossCodesInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_LOSS_CODES = "CLM_LOSS_CODES";
		public ClsLossCodesInfo()
		{
			base.dtModel.TableName = "CLM_LOSS_CODES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_LOSS_CODES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{

			base.dtModel.Columns.Add("LOSS_CODE_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));			
			base.dtModel.Columns.Add("LOSS_CODE_TYPE",typeof(int));			
		}
		#region Database schema details
		// model for database field LOSS_CODE_ID(int)
		public int LOSS_CODE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_CODE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSS_CODE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_CODE_ID"] = value;
			}
		}
		// model for database field LOB_ID(int)
		public int LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}
		// model for database field LOSS_CODE_TYPE(int)
		public int LOSS_CODE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_CODE_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSS_CODE_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_CODE_TYPE"] = value;
			}
		}			
		#endregion
	}
}
