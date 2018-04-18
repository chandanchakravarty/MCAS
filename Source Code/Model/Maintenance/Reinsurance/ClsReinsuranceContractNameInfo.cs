/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for Contract Types
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
	/// Summary description for ClsReinsuranceContractNameInfo.
	/// </summary>
	public class ClsReinsuranceContractNameInfo : Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 
		
		private const string MNT_CONTRACT_NAME = "MNT_CONTRACT_NAME";
		
		public ClsReinsuranceContractNameInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_CONTRACT_NAME";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CONTRACT_NAME_ID",typeof(int));
			base.dtModel.Columns.Add("CONTRACT_NAME",typeof(string));
			base.dtModel.Columns.Add("CONTRACT_DESCRITION",typeof(string));
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		// model for database field CONTRACT_NAME_ID(int)
		public string CONTRACT_NAME_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_NAME_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTRACT_NAME_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_NAME_ID"] = value;
			}
		}

		// model for database field CONTRACT_NAME(string)
		
		public string CONTRACT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTRACT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_NAME"] = value;
			}
		}

		// model for database field IS_ACTIVE(string)
		public string CONTRACT_DESCRITION
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_DESCRITION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTRACT_DESCRITION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_DESCRITION"] = value;
			}
		}
		
		# endregion 

	}
}
