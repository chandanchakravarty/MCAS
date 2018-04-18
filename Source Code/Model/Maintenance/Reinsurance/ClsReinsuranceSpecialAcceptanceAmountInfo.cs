/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 18, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for Special Acceptance Limit
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
	/// Summary description for ClsReinsuranceSpecialAcceptanceAmountInfo.
	/// </summary>
	public class ClsReinsuranceSpecialAcceptanceAmountInfo: Cms.Model.ClsCommonModel
	{
		#region DECLARATION

		private const string MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT="MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT";
		
		public ClsReinsuranceSpecialAcceptanceAmountInfo()
		{
			//
			// TODO: Add constructor logic here
			//
			//Setting Table Name for DATATABLE that holds property values.
			base.dtModel.TableName ="MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT";
 
			//Add COLUMNS of the DATABASE TABLE MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT
			this.AddColumns();

			//Add a Blank row in the DATATABLE
			base.dtModel.Rows.Add(base.dtModel.NewRow());
		}
		#endregion
		#region ADDING COLUMNS TO TABLE
		private void AddColumns()
		{
			
			base.dtModel.Columns.Add("SPECIAL_ACCEPTANCE_LIMIT_ID",typeof(int));
			base.dtModel.Columns.Add("SPECIAL_ACCEPTANCE_LIMIT",typeof(string));
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));//2
			base.dtModel.Columns.Add("LOB_ID",typeof(int));//3
		}
		#endregion
		#region DATABASE SCHEMA DETAILS

		// model for database field SPECIAL_ACCEPTANCE_LIMIT_ID(int)
		public int SPECIAL_ACCEPTANCE_LIMIT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SPECIAL_ACCEPTANCE_LIMIT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SPECIAL_ACCEPTANCE_LIMIT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SPECIAL_ACCEPTANCE_LIMIT_ID"] = value;
			}
		}

		
		// model for database field SPECIAL_ACCEPTANCE_LIMIT(string)
		public string SPECIAL_ACCEPTANCE_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["SPECIAL_ACCEPTANCE_LIMIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SPECIAL_ACCEPTANCE_LIMIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SPECIAL_ACCEPTANCE_LIMIT"] = value;
			}
		}

		// model for database field EFFECTIVE_DATE(DateTime)
		public DateTime EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_DATE"] = value;
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
		#endregion

		
	}
}
