/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for the Excess Layer
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
	/// Summary description for ClsReinsuranceContractTypeInfo.
	/// </summary>
	public class ClsReinsuranceContractTypeInfo: Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 

		public ClsReinsuranceContractTypeInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REINSURANCE_CONTRACT_TYPE";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CONTRACTTYPEID",typeof(int));
			base.dtModel.Columns.Add("CONTRACT_TYPE_DESC",typeof(string));
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		// model for database field CONTRACT_NAME_ID(int)
		public int CONTRACTTYPEID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACTTYPEID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTRACTTYPEID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACTTYPEID"] = value;
			}
		}

		// model for database field CONTRACT_NAME(string)
		public string CONTRACT_TYPE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_TYPE_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTRACT_TYPE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_TYPE_DESC"] = value;
			}
		}

	
		# endregion 
	}
}
