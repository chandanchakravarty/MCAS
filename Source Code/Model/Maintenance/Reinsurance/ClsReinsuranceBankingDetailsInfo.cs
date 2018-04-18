/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 24, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for the Reinsurance BANKING DETAILS
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
	/// Summary description for ClsReinsuranceBankingDetailsInfo.
	/// </summary>
	public class ClsReinsuranceBankingDetailsInfo:Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 

		public ClsReinsuranceBankingDetailsInfo()
		{
			
			//
			// TODO: Add constructor logic here
			//


			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REIN_BANKING_DETAIL";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("REIN_BANK_DETAIL_ID",typeof(int)); //1

			base.dtModel.Columns.Add("REIN_BANK_DETAIL_ADDRESS_1",typeof(string));//2
			base.dtModel.Columns.Add("REIN_BANK_DETAIL_ADDRESS_2",typeof(string));//3
			base.dtModel.Columns.Add("REIN_BANK_DETAIL_CITY",typeof(string));//4
			base.dtModel.Columns.Add("REIN_BANK_DETAIL_COUNTRY",typeof(string));//5
			base.dtModel.Columns.Add("REIN_BANK_DETAIL_STATE",typeof(string));//6
			base.dtModel.Columns.Add("REIN_BANK_DETAIL_ZIP",typeof(string));//7
			
			base.dtModel.Columns.Add("M_REIN_BANK_DETAIL_ADDRESS_1",typeof(string));//8
			base.dtModel.Columns.Add("M_REIN_BANK_DETAIL_ADDRESS_2",typeof(string));//9
			base.dtModel.Columns.Add("M_REIN_BANK_DETAIL_CITY",typeof(string));//10
			base.dtModel.Columns.Add("M_REIN_BANK_DETAIL_COUNTRY",typeof(string));//11
			base.dtModel.Columns.Add("M_REIN_BANK_DETAIL_STATE",typeof(string));//12
			base.dtModel.Columns.Add("M_REIN_BANK_DETAIL_ZIP",typeof(string));//13
			
			
			base.dtModel.Columns.Add("REIN_PAYMENT_BASIS",typeof(string));//14
			base.dtModel.Columns.Add("REIN_BANK_NAME",typeof(string));//14
			base.dtModel.Columns.Add("REIN_TRANSIT_ROUTING",typeof(string));//15
			base.dtModel.Columns.Add("REIN_BANK_ACCOUNT",typeof(string));//16
			base.dtModel.Columns.Add("REIN_COMAPANY_ID",typeof(int));//16

			
			
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		#region REIN_BANK_DETAIL_ID

		public int REIN_BANK_DETAIL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_BANK_DETAIL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_BANK_DETAIL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_BANK_DETAIL_ID"] = value;
			}
		}
		# endregion
		
		#region REIN_COMAPANY_ID

		public int REIN_COMAPANY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_COMAPANY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_COMAPANY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_COMAPANY_ID"] = value;
			}
		}
		# endregion

		#region REIN_BANK_DETAIL_ADDRESS_1

		public string REIN_BANK_DETAIL_ADDRESS_1
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_BANK_DETAIL_ADDRESS_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_BANK_DETAIL_ADDRESS_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_BANK_DETAIL_ADDRESS_1"] = value;
			}
		}
		#endregion
		#region REIN_BANK_DETAIL_ADDRESS_2

		public string REIN_BANK_DETAIL_ADDRESS_2
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_BANK_DETAIL_ADDRESS_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_BANK_DETAIL_ADDRESS_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_BANK_DETAIL_ADDRESS_2"] = value;
			}
		}
		#endregion
		#region REIN_BANK_DETAIL_CITY

		public string REIN_BANK_DETAIL_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_BANK_DETAIL_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_BANK_DETAIL_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_BANK_DETAIL_CITY"] = value;
			}
		}
		#endregion
		#region REIN_BANK_DETAIL_COUNTRY

		public string REIN_BANK_DETAIL_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_BANK_DETAIL_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_BANK_DETAIL_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_BANK_DETAIL_COUNTRY"] = value;
			}
		}
		#endregion
		#region REIN_BANK_DETAIL_STATE

		public string REIN_BANK_DETAIL_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_BANK_DETAIL_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_BANK_DETAIL_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_BANK_DETAIL_STATE"] = value;
			}
		}
		#endregion
		#region REIN_BANK_DETAIL_ZIP

		public string REIN_BANK_DETAIL_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_BANK_DETAIL_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_BANK_DETAIL_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_BANK_DETAIL_ZIP"] = value;
			}
		}
		#endregion
		
		#region M_REIN_BANK_DETAIL_ADDRESS_1

		public string M_REIN_BANK_DETAIL_ADDRESS_1
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_ADDRESS_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_ADDRESS_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_ADDRESS_1"] = value;
			}
		}
		#endregion
		#region M_REIN_BANK_DETAIL_ADDRESS_2

		public string M_REIN_BANK_DETAIL_ADDRESS_2
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_ADDRESS_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_ADDRESS_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_ADDRESS_2"] = value;
			}
		}
		#endregion
		#region M_REIN_BANK_DETAIL_CITY

		public string M_REIN_BANK_DETAIL_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_CITY"] = value;
			}
		}
		#endregion
		#region M_REIN_BANK_DETAIL_COUNTRY

		public string M_REIN_BANK_DETAIL_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_COUNTRY"] = value;
			}
		}
		#endregion
		#region M_REIN_BANK_DETAIL_STATE

		public string M_REIN_BANK_DETAIL_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_STATE"] = value;
			}
		}
		#endregion
		#region M_REIN_BANK_DETAIL_ZIP

		public string M_REIN_BANK_DETAIL_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_BANK_DETAIL_ZIP"] = value;
			}
		}
		#endregion
		#region REIN_PAYMENT_BASIS

		public string REIN_PAYMENT_BASIS
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_PAYMENT_BASIS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_PAYMENT_BASIS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_PAYMENT_BASIS"] = value;
			}
		}
		#endregion
		#region REIN_BANK_NAME

		public string REIN_BANK_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_BANK_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_BANK_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_BANK_NAME"] = value;
			}
		}
		#endregion
		#region REIN_TRANSIT_ROUTING

		public string REIN_TRANSIT_ROUTING
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_TRANSIT_ROUTING"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_TRANSIT_ROUTING"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_TRANSIT_ROUTING"] = value;
			}
		}
		#endregion
		#region REIN_BANK_ACCOUNT

		public string REIN_BANK_ACCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_BANK_ACCOUNT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_BANK_ACCOUNT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_BANK_ACCOUNT"] = value;
			}
		}
		#endregion

		# endregion
	}
}
