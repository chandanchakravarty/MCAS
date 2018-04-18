/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	5/9/2005 12:50:04 PM
<End Date				: -	
<Description				: - 	Models MNT_AGENCY_STATE_LOB_ASSOC
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
namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_AGENCY_STATE_LOB_ASSOC.
	/// </summary>
	public class ClsAgencyStateLobAssocInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_AGENCY_STATE_LOB_ASSOC = "MNT_AGENCY_STATE_LOB_ASSOC";
		public ClsAgencyStateLobAssocInfo()
		{
			base.dtModel.TableName = "MNT_AGENCY_STATE_LOB_ASSOC";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_AGENCY_STATE_LOB_ASSOC
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("MNT_AGENCY_STATE_LOB_ASSOC_ID",typeof(int));
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));			
			base.dtModel.Columns.Add("LOB_ID",typeof(string));			
		}

		#region Database schema details

		// model for database field MNT_AGENCY_STATE_LOB_ASSOC_ID(int)
		public int MNT_AGENCY_STATE_LOB_ASSOC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["MNT_AGENCY_STATE_LOB_ASSOC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MNT_AGENCY_STATE_LOB_ASSOC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MNT_AGENCY_STATE_LOB_ASSOC_ID"] = value;
			}
		}

		// model for database field AGENCY_ID(int)
		public int AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_ID"] = value;
			}
		}

		// model for database field STATE_ID(int)
		public int STATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STATE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}

		// model for database field LOB_ID(string)
		public string LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOB_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}


		
		#endregion
		
	}
}
