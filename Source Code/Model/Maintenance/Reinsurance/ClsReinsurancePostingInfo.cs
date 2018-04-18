using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ClsReinsurancePostingInfo.
	/// </summary>
	public class ClsReinsurancePostingInfo : Cms.Model.ClsCommonModel
	{
		private const string mnt_reinsurance_posting = "mnt_reinsurance_posting";
		public ClsReinsurancePostingInfo()
		{
			base.dtModel.TableName = "mnt_reinsurance_posting";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table mnt_reinsurance_posting
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		#region AddColumns
		private void AddColumns()
		{
			base.dtModel.Columns.Add("GL_ID",typeof(int));
			base.dtModel.Columns.Add("rein_posting_id",typeof(int));
			base.dtModel.Columns.Add("contract_id",typeof(int));
			base.dtModel.Columns.Add("Commision_applicable",typeof(string));
			base.dtModel.Columns.Add("Rein_Premium_Act",typeof(int));
			base.dtModel.Columns.Add("Rein_Payment_Act",typeof(int));
			base.dtModel.Columns.Add("Rein_Commision_Act",typeof(int));
			base.dtModel.Columns.Add("Rein_Commision_Recevable",typeof(int));
		}
		#endregion
		#region Database schema details
		// model for database field rein_posting_id(int)
		public int GL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["GL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["GL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GL_ID"] = value;
			}
		}
		// model for database field rein_posting_id(int)
		public int rein_posting_id
		{
			get
			{
				return base.dtModel.Rows[0]["rein_posting_id"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["rein_posting_id"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["rein_posting_id"] = value;
			}
		}
		// model for database field contract_id(int)
		public int contract_id
		{
			get
			{
				return base.dtModel.Rows[0]["contract_id"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["contract_id"].ToString());
			
			}
			set
			{
				base.dtModel.Rows[0]["contract_id"] = value;
			}
		}
		// model for database field Commision_applicable(int)
		public int Commision_applicable
		{
			get
			{
				return base.dtModel.Rows[0]["Commision_applicable"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["Commision_applicable"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Commision_applicable"] = value;
			}
		}
		// model for database field Rein_Premium_Act(int)
		public int Rein_Premium_Act
		{
			get
			{
				return base.dtModel.Rows[0]["Rein_Premium_Act"] == DBNull.Value ? Convert.ToInt32(null) :Convert.ToInt32( base.dtModel.Rows[0]["Rein_Premium_Act"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Rein_Premium_Act"] = value;
			}
		}
		// model for database field Rein_Commision_Act(int)
		public int Rein_Commision_Act
		{
			get
			{
				return base.dtModel.Rows[0]["Rein_Commision_Act"] == DBNull.Value ? Convert.ToInt32(null) :Convert.ToInt32( base.dtModel.Rows[0]["Rein_Commision_Act"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Rein_Commision_Act"] = value;
			}
		}
		// model for database field Rein_Commision_Recevable(int)
		public int Rein_Commision_Recevable
		{
			get
			{
				return base.dtModel.Rows[0]["Rein_Commision_Recevable"] == DBNull.Value ?Convert.ToInt32(null) :Convert.ToInt32( base.dtModel.Rows[0]["Rein_Commision_Recevable"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Rein_Commision_Recevable"] = value;
			}

		}
		// model for database field Rein_Payment_Act(int)
		public int Rein_Payment_Act
		{
			get
			{
				return base.dtModel.Rows[0]["Rein_Payment_Act"] == DBNull.Value ?Convert.ToInt32(null) :Convert.ToInt32( base.dtModel.Rows[0]["Rein_Payment_Act"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Rein_Payment_Act"] = value;
			}

		}


		#endregion
			
		
	}
}
