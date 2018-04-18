/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: 07/05/2007
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
	/// Summary description for ClsReinsuranceMajorParticipationInfo.
	/// </summary>
	public class ClsReinsuranceMajorParticipationInfo: Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 
		
		private const string MNT_REINSURANCE_MAJORMINOR_PARTICIPATION = "MNT_REINSURANCE_MAJORMINOR_PARTICIPATION";
		
		public ClsReinsuranceMajorParticipationInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REINSURANCE_MAJORMINOR_PARTICIPATION";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("PARTICIPATION_ID",typeof(int));
			base.dtModel.Columns.Add("REINSURANCE_COMPANY",typeof(string));
			base.dtModel.Columns.Add("LAYER",typeof(int));
			base.dtModel.Columns.Add("NET_RETENTION",typeof(string));
			base.dtModel.Columns.Add("WHOLE_PERCENT",typeof(double));
			base.dtModel.Columns.Add("MINOR_PARTICIPANTS",typeof(string));
			base.dtModel.Columns.Add("CONTRACT_ID",typeof(int));
			
					
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		// model for database field PARTICIPATION_ID(int)
		public int PARTICIPATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PARTICIPATION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PARTICIPATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARTICIPATION_ID"] = value;
			}
		}


		// model for database field REINSURANCE_COMPANY(string)
		public string REINSURANCE_COMPANY
		{
			get
			{
				return base.dtModel.Rows[0]["REINSURANCE_COMPANY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REINSURANCE_COMPANY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REINSURANCE_COMPANY"] = value;
			}
		}


		// model for database field LAYER(int)
		public int LAYER
		{
			get
			{
				return base.dtModel.Rows[0]["LAYER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LAYER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAYER"] = value;
			}
		}


		// model for database field NET_RETENTION(int)
		public int NET_RETENTION
		{
			get
			{
				return base.dtModel.Rows[0]["NET_RETENTION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NET_RETENTION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NET_RETENTION"] = value;
			}
		}


		// model for database field WHOLE_PERCENT(double)
		public double WHOLE_PERCENT
		{
			get
			{
				return base.dtModel.Rows[0]["WHOLE_PERCENT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["WHOLE_PERCENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WHOLE_PERCENT"] = value;
			}
		}

		
		// model for database field MINOR_PARTICIPANTS(int)
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

		# region COMMON TO ALL INFO FILES
		

		# endregion COMMON TO ALL INFO FILES

		# endregion 
	}
}




