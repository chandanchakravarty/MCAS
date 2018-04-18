/******************************************************************************************
<Author				: -   Ajit Singh chahal
<Start Date				: -	6/7/2005 7:34:34 PM
<End Date				: -	
<Description				: - 	Model For Transaction Code Group Details.
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

namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
	/// Database Model for ACT_TRAN_CODE_GROUP_DETAILS.
	/// </summary>
	public class ClsTransactionCodeGroupDetailsInfo: Cms.Model.ClsCommonModel
	{
		private const string ACT_TRAN_CODE_GROUP_DETAILS = "ACT_TRAN_CODE_GROUP_DETAILS";
		public ClsTransactionCodeGroupDetailsInfo()
		{
			base.dtModel.TableName = "ACT_TRAN_CODE_GROUP_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_TRAN_CODE_GROUP_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("DETAIL_ID",typeof(int));
			base.dtModel.Columns.Add("TRAN_GROUP_ID",typeof(int));
			base.dtModel.Columns.Add("TRAN_ID",typeof(int));
			base.dtModel.Columns.Add("DEF_SEQ",typeof(int));
		}
		#region Database schema details
		// model for database field DETAIL_ID(int)
		public int DETAIL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DETAIL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DETAIL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DETAIL_ID"] = value;
			}
		}
		// model for database field TRAN_GROUP_ID(int)
		public int TRAN_GROUP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["TRAN_GROUP_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRAN_GROUP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRAN_GROUP_ID"] = value;
			}
		}
		// model for database field TRAN_ID(int)
		public int TRAN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["TRAN_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRAN_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRAN_ID"] = value;
			}
		}
		// model for database field DEF_SEQ(int)
		public int DEF_SEQ
		{
			get
			{
				return base.dtModel.Rows[0]["DEF_SEQ"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DEF_SEQ"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEF_SEQ"] = value;
			}
		}
		#endregion
	}
}
