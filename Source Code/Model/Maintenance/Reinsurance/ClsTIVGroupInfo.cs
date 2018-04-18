/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 27, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for the Reinsurance ClsTIVGroupInfo.
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
	/// Summary description for ClsTIVGroupInfo.
	/// </summary>
	public class ClsTIVGroupInfo:Cms.Model.ClsCommonModel
	{
		

		# region D E C L A R A T I O N 

		public ClsTIVGroupInfo()
		{
					// setting table name for data table that holds property values.
		base.dtModel.TableName = "MNT_REIN_TIV_GROUP";	

					// add columns of the database table MNT_REINSURANCE_CONTRACT
		this.AddColumns();								
					
					// add a blank row in the datatable
		base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("REIN_TIV_GROUP_ID",typeof(int)); //1
			base.dtModel.Columns.Add("REIN_TIV_CONTRACT_NUMBER",typeof(string));//2
			base.dtModel.Columns.Add("REIN_TIV_EFFECTIVE_DATE",typeof(DateTime));//3
			base.dtModel.Columns.Add("REIN_TIV_FROM",typeof(string));//4
			base.dtModel.Columns.Add("REIN_TIV_TO",typeof(string));//5
			base.dtModel.Columns.Add("REIN_TIV_GROUP_CODE",typeof(string));//6

			//base.dtModel.Columns.Add("IS_ACTIVE",typeof(string));//32
			//base.dtModel.Columns.Add("CREATED_BY",typeof(int));//33
			//base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));//34
			//base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));//35
			//base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));//36
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		#region REIN_TIV_GROUP_ID

		public int REIN_TIV_GROUP_ID
		{
		get
		{
		return base.dtModel.Rows[0]["REIN_TIV_GROUP_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_TIV_GROUP_ID"].ToString());
		}
		set
		{
		base.dtModel.Rows[0]["REIN_TIV_GROUP_ID"] = value;
		}
		}
		# endregion
		#region REIN_CONTACT_NAME

		public string REIN_TIV_CONTRACT_NUMBER
		{
		get
		{
		return base.dtModel.Rows[0]["REIN_TIV_CONTRACT_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_TIV_CONTRACT_NUMBER"].ToString();
		}
		set
		{
		base.dtModel.Rows[0]["REIN_TIV_CONTRACT_NUMBER"] = value;
		}
		}
		#endregion
		#region REIN_TIV_EFFECTIVE_DATE

		public DateTime REIN_TIV_EFFECTIVE_DATE
		{
		get
		{
		return base.dtModel.Rows[0]["REIN_TIV_EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["REIN_TIV_EFFECTIVE_DATE"].ToString());
		}
		set
		{
		base.dtModel.Rows[0]["REIN_TIV_EFFECTIVE_DATE"] = value;
		}
		}
		#endregion
		#region REIN_TIV_FROM

		public string REIN_TIV_FROM
		{
		get
		{
		return base.dtModel.Rows[0]["REIN_TIV_FROM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_TIV_FROM"].ToString();
		}
		set
		{
		base.dtModel.Rows[0]["REIN_TIV_FROM"] = value;
		}
		}
		#endregion
		#region REIN_TIV_TO

		public string REIN_TIV_TO
		{
		get
		{
		return base.dtModel.Rows[0]["REIN_TIV_TO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_TIV_TO"].ToString();
		}
		set
		{
		base.dtModel.Rows[0]["REIN_TIV_TO"] = value;
		}
		}
		#endregion
		#region REIN_TIV_GROUP_CODE

		public string REIN_TIV_GROUP_CODE
		{
		get
		{
		return base.dtModel.Rows[0]["REIN_TIV_GROUP_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_TIV_GROUP_CODE"].ToString();
		}
		set
		{
		base.dtModel.Rows[0]["REIN_TIV_GROUP_CODE"] = value;
		}
		}
		#endregion

		# endregion
	}
}



