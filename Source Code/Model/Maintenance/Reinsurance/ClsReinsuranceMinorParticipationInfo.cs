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
	/// Summary description for ClsReinsuranceMinorParticipationInfo.
	/// </summary>
	public class ClsReinsuranceMinorParticipationInfo : Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 
		
		private const string MNT_REIN_MINOR_PARTICIPATION = "MNT_REIN_MINOR_PARTICIPATION";
		
		public ClsReinsuranceMinorParticipationInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REIN_MINOR_PARTICIPATION";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("MINOR_PARTICIPATION_ID",typeof(int));
			base.dtModel.Columns.Add("MINOR_LAYER",typeof(int));
			base.dtModel.Columns.Add("MINOR_PARTICIPANTS",typeof(int));
			base.dtModel.Columns.Add("MAJOR_PARTICIPANTS",typeof(string));
			base.dtModel.Columns.Add("MINOR_WHOLE_PERCENT",typeof(double));
			base.dtModel.Columns.Add("CONTRACT_ID",typeof(int));
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		// model for database field PARTICIPATION_ID(int)
		public int MINOR_PARTICIPATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["MINOR_PARTICIPATION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MINOR_PARTICIPATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MINOR_PARTICIPATION_ID"] = value;
			}
		}

		// model for database field CONTRACT_ID(int)
		public int MINOR_LAYER
		{
			get
			{
				return base.dtModel.Rows[0]["MINOR_LAYER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MINOR_LAYER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MINOR_LAYER"] = value;
			}
		}

		// model for database field NET_RETENTION(int)
		public int MINOR_PARTICIPANTS
		{
			get
			{
				return base.dtModel.Rows[0]["MINOR_PARTICIPANTS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MINOR_PARTICIPANTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MINOR_PARTICIPANTS"] = value;
			}
		}


		// model for database field REINSURANCE_COMPANY(string)
		public string MAJOR_PARTICIPANTS
		{
			get
			{
				return base.dtModel.Rows[0]["MAJOR_PARTICIPANTS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MAJOR_PARTICIPANTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAJOR_PARTICIPANTS"] = value;
			}
		}


		// model for database field WHOLE_PERCENT(double)
		public double MINOR_WHOLE_PERCENT
		{
			get
			{
				return base.dtModel.Rows[0]["MINOR_WHOLE_PERCENT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MINOR_WHOLE_PERCENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MINOR_WHOLE_PERCENT"] = value;
			}
		}
		// model for database field CONTRACT_ID(int)
		public int CONTRACT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CONTRACT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_ID"] = value;
			}
		}

		# endregion 
	}
}
