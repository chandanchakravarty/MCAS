/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	7/4/2005 1:03:59 PM
<End Date				: -	
<Description			: - Model class for ACT_CUSTOMER_RECON_GROUP_DETAILS
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Account
{
	/// <summary>
	/// Database Model for ACT_CUSTOMER_RECON_GROUP_DETAILS.
	/// </summary>
	public class ClsReconDetailInfo : Cms.Model.ClsBaseModel
	{
		private const string ACT_CUSTOMER_RECON_GROUP_DETAILS = "ACT_CUSTOMER_RECON_GROUP_DETAILS";
		public ClsReconDetailInfo()
		{
			base.dtModel.TableName = "ACT_CUSTOMER_RECON_GROUP_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_CUSTOMER_RECON_GROUP_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("IDEN_ROW_NO",typeof(int));
			base.dtModel.Columns.Add("GROUP_ID",typeof(int));
			base.dtModel.Columns.Add("ITEM_TYPE",typeof(string));
			base.dtModel.Columns.Add("ITEM_REFERENCE_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_LEDGER_TYPE",typeof(string));
			base.dtModel.Columns.Add("RECON_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("NOTE",typeof(string));
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			base.dtModel.Columns.Add("CD_LINE_ITEM_ID",typeof(int));
		}
		#region Database schema details
		// model for database field IDEN_ROW_NO(int)
		public int IDEN_ROW_NO
		{
			get
			{
				return base.dtModel.Rows[0]["IDEN_ROW_NO"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["IDEN_ROW_NO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IDEN_ROW_NO"] = value;
			}
		}
		// model for database field GROUP_ID(int)
		public int GROUP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["GROUP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["GROUP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GROUP_ID"] = value;
			}
		}
		// model for database field ITEM_TYPE(string)
		public string ITEM_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ITEM_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_TYPE"] = value;
			}
		}
		// model for database field ITEM_REFERENCE_ID(int)
		public int ITEM_REFERENCE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_REFERENCE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ITEM_REFERENCE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_REFERENCE_ID"] = value;
			}
		}
		// model for database field SUB_LEDGER_TYPE(string)
		public string SUB_LEDGER_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LEDGER_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_LEDGER_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LEDGER_TYPE"] = value;
			}
		}
		// model for database field RECON_AMOUNT(double)
		public double RECON_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["RECON_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["RECON_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECON_AMOUNT"] = value;
			}
		}
		// model for database field NOTE(string)
		public string NOTE
		{
			get
			{
				return base.dtModel.Rows[0]["NOTE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NOTE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NOTE"] = value;
			}
		}
		// model for database field DIV_ID(int)
		public int DIV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DIV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIV_ID"] = value;
			}
		}
		// model for database field DEPT_ID(int)
		public int DEPT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DEPT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_ID"] = value;
			}
		}
		// model for database field PC_ID(int)
		public int PC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PC_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PC_ID"] = value;
			}
		}
		#endregion
	}
}
