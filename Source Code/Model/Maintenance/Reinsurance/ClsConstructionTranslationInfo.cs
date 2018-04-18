/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 27, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for the Reinsurance ClsConstructionTranslationInfo
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
	/// Summary description for ClsConstructionTranslationInfo.
	/// </summary>
	public class ClsConstructionTranslationInfo:Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 

		public ClsConstructionTranslationInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REIN_CONSTRUCTION_TRANSLATION";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("REIN_CONSTRUCTION_CODE_ID",typeof(int)); //1
			base.dtModel.Columns.Add("REIN_EXTERIOR_CONSTRUCTION",typeof(string));//2
			base.dtModel.Columns.Add("REIN_DESCRIPTION",typeof(string));//3
			base.dtModel.Columns.Add("REIN_REPORT_CODE",typeof(string));//4
			base.dtModel.Columns.Add("REIN_NISS",typeof(string));//5
			
			//base.dtModel.Columns.Add("IS_ACTIVE",typeof(string));//32
			//base.dtModel.Columns.Add("CREATED_BY",typeof(int));//33
			//base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));//34
			//base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));//35
			//base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));//36
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		#region REIN_CONSTRUCTION_CODE_ID

		public int REIN_CONSTRUCTION_CODE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONSTRUCTION_CODE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_CONSTRUCTION_CODE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONSTRUCTION_CODE_ID"] = value;
			}
		}
		# endregion
		#region REIN_EXTERIOR_CONSTRUCTION

		public string REIN_EXTERIOR_CONSTRUCTION
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_EXTERIOR_CONSTRUCTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_EXTERIOR_CONSTRUCTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_EXTERIOR_CONSTRUCTION"] = value;
			}
		}
		#endregion
		#region REIN_DESCRIPTION

		public string REIN_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_DESCRIPTION"] = value;
			}
		}
		#endregion
		#region REIN_REPORT_CODE

		public string REIN_REPORT_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_REPORT_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_REPORT_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_REPORT_CODE"] = value;
			}
		}
		#endregion
		#region REIN_NISS

		public string REIN_NISS
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_NISS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_NISS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_NISS"] = value;
			}
		}
		#endregion
		
		# endregion

	}
}


