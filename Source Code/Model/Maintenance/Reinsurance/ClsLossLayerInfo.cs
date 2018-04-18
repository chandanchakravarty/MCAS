/******************************************************************************************
<Author					: -		Swarup Kumar Pal
<Start Date				: -		14-Aug-2007 
<End Date				: -	
<Description			: - 	Model for ClsLossLayerInfo.
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

namespace Cms.Model.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ClsLossLayerInfo.
	/// </summary>
	public class ClsLossLayerInfo:Cms.Model.ClsCommonModel
	{
		
		# region D E C L A R A T I O N 
		public ClsLossLayerInfo()
		{
				// setting table name for data table that holds property values.
				base.dtModel.TableName = "MNT_REIN_LOSSLAYER";	

				// add columns of the database table MNT_REIN_LOSSLAYER
				this.AddColumns();								
			
				// add a blank row in the datatable
				base.dtModel.Rows.Add(base.dtModel.NewRow());
		}
		#endregion

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("LOSS_LAYER_ID",typeof(int)); //1
			base.dtModel.Columns.Add("CONTRACT_ID",typeof(int));//2
			base.dtModel.Columns.Add("LAYER",typeof(int));//3
			base.dtModel.Columns.Add("COMPANY_RETENTION",typeof(int));//4
			base.dtModel.Columns.Add("LAYER_AMOUNT",typeof(double));//5
			base.dtModel.Columns.Add("RETENTION_AMOUNT",typeof(double));//6
			base.dtModel.Columns.Add("RETENTION_PERCENTAGE",typeof(double));//7
			base.dtModel.Columns.Add("REIN_CEDED",typeof(double));//8
			base.dtModel.Columns.Add("REIN_CEDED_PERCENTAGE",typeof(double));//9

          
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 
		// model for database field LOSS_LAYER_ID(int)
		public int LOSS_LAYER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_LAYER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSS_LAYER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_LAYER_ID"] = value;
			}
		}
		// model for database field CONTRACT_ID(int)
		public int CONTRACT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CONTRACT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_ID"] = value;
			}
		}
		// model for database field LAYER(int)
		public int LAYER
		{
			get
			{
				return base.dtModel.Rows[0]["LAYER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LAYER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAYER"] = value;
			}
		}
		// model for database field COMPANY_RETENTION(int)
		public int COMPANY_RETENTION
		{
			get
			{
				return base.dtModel.Rows[0]["COMPANY_RETENTION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COMPANY_RETENTION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMPANY_RETENTION"] = value;
			}
		}
		// model for database field LAYER_AMOUNT(string)
		public double LAYER_AMOUNT
		{
			get
			{
                return base.dtModel.Rows[0]["LAYER_AMOUNT"] == DBNull.Value ? Convert.ToDouble(null):double.Parse(base.dtModel.Rows[0]["LAYER_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAYER_AMOUNT"] = value;
			}
		}


		// model for database field RETENTION_AMOUNT(string)
		public double  RETENTION_AMOUNT
		{
			get
			{
                return base.dtModel.Rows[0]["RETENTION_AMOUNT"] == DBNull.Value ? Convert.ToDouble(null):double.Parse(base.dtModel.Rows[0]["RETENTION_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RETENTION_AMOUNT"] = value;
			}
		}



		// model for database field RETENTION_PERCENTAGE(int)
        public double RETENTION_PERCENTAGE
        {
            get
            {
                return base.dtModel.Rows[0]["RETENTION_PERCENTAGE"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RETENTION_PERCENTAGE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["RETENTION_PERCENTAGE"] = value;
            }
        }
		
		// model for database field REIN_CEDED(string)
        public double REIN_CEDED
		{
			get
			{
                return base.dtModel.Rows[0]["REIN_CEDED"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["REIN_CEDED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CEDED"] = value;
			}
		}
		// model for database field REIN_CEDED_PERCENTAGE
        public double REIN_CEDED_PERCENTAGE
        {
            get
            {
                return base.dtModel.Rows[0]["REIN_CEDED_PERCENTAGE"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["REIN_CEDED_PERCENTAGE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["REIN_CEDED_PERCENTAGE"] = value;
            }
        }
		
		#endregion
		
	}
}
