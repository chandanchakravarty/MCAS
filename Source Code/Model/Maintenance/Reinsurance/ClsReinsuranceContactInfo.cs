/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 20, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is the model class for the Reinsurance Contacts
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
	/// Summary description for ClsReinsuranceContactInfo.
	/// </summary>
	public class ClsReinsuranceContactInfo:Cms.Model.ClsCommonModel
	{
		
		# region D E C L A R A T I O N 

		public ClsReinsuranceContactInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REIN_CONTACT";	

			// add columns of the database table MNT_REINSURANCE_CONTRACT
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());	
		}

		# endregion 

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("REIN_CONTACT_ID",typeof(int)); //1
			base.dtModel.Columns.Add("REIN_CONTACT_NAME",typeof(string));//2
			base.dtModel.Columns.Add("REIN_CONTACT_CODE",typeof(string));//3
			base.dtModel.Columns.Add("REIN_CONTACT_POSITION",typeof(string));//4
			base.dtModel.Columns.Add("REIN_CONTACT_SALUTATION",typeof(string));//5
			base.dtModel.Columns.Add("REIN_CONTACT_ADDRESS_1",typeof(string));//6
			base.dtModel.Columns.Add("REIN_CONTACT_ADDRESS_2",typeof(string));//7
			base.dtModel.Columns.Add("REIN_CONTACT_CITY",typeof(string));//8
			base.dtModel.Columns.Add("REIN_CONTACT_COUNTRY",typeof(string));//9
			base.dtModel.Columns.Add("REIN_CONTACT_STATE",typeof(string));//10
			base.dtModel.Columns.Add("REIN_CONTACT_ZIP",typeof(string));//11
			base.dtModel.Columns.Add("REIN_CONTACT_PHONE_1",typeof(string));//12
			base.dtModel.Columns.Add("REIN_CONTACT_PHONE_2",typeof(string));//13
			base.dtModel.Columns.Add("REIN_CONTACT_EXT_1",typeof(string));//14
			base.dtModel.Columns.Add("REIN_CONTACT_EXT_2",typeof(string));//15
			base.dtModel.Columns.Add("M_REIN_CONTACT_ADDRESS_1",typeof(string));//16
			base.dtModel.Columns.Add("M_REIN_CONTACT_ADDRESS_2",typeof(string));//17
			base.dtModel.Columns.Add("M_REIN_CONTACT_CITY",typeof(string));//18
			base.dtModel.Columns.Add("M_REIN_CONTACT_COUNTRY",typeof(string));//19
			base.dtModel.Columns.Add("M_REIN_CONTACT_STATE",typeof(string));//20
			base.dtModel.Columns.Add("M_REIN_CONTACT_ZIP",typeof(string));//21
			base.dtModel.Columns.Add("M_REIN_CONTACT_PHONE_1",typeof(string));//22
			base.dtModel.Columns.Add("M_REIN_CONTACT_PHONE_2",typeof(string));//23
			base.dtModel.Columns.Add("M_REIN_CONTACT_EXT_1",typeof(string));//24
			base.dtModel.Columns.Add("M_REIN_CONTACT_EXT_2",typeof(string));//25
			base.dtModel.Columns.Add("REIN_CONTACT_MOBILE",typeof(string));//26
			base.dtModel.Columns.Add("REIN_CONTACT_FAX",typeof(string));//27
			base.dtModel.Columns.Add("REIN_CONTACT_SPEED_DIAL",typeof(string));//28
			base.dtModel.Columns.Add("REIN_CONTACT_EMAIL_ADDRESS",typeof(string));//29
			base.dtModel.Columns.Add("REIN_CONTACT_CONTRACT_DESC",typeof(string));//30
			base.dtModel.Columns.Add("REIN_CONTACT_COMMENTS",typeof(string));//31
			base.dtModel.Columns.Add("REIN_COMAPANY_ID",typeof(int));//31
			//base.dtModel.Columns.Add("IS_ACTIVE",typeof(string));//32
			//base.dtModel.Columns.Add("CREATED_BY",typeof(int));//33
			//base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));//34
			//base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));//35
			//base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));//36
		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 

		#region REIN_CONTACT_ID

		public int REIN_CONTACT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["REIN_CONTACT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_ID"] = value;
			}
		}
		# endregion
		#region REIN_CONTACT_NAME

		public string REIN_CONTACT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_NAME"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_CODE

		public string REIN_CONTACT_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_CODE"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_POSITION

		public string REIN_CONTACT_POSITION
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_POSITION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_POSITION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_POSITION"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_SALUTATION

		public string REIN_CONTACT_SALUTATION
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_SALUTATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_SALUTATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_SALUTATION"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_ADDRESS_1

		public string REIN_CONTACT_ADDRESS_1
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_ADDRESS_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_ADDRESS_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_ADDRESS_1"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_ADDRESS_2

		public string REIN_CONTACT_ADDRESS_2
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_ADDRESS_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_ADDRESS_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_ADDRESS_2"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_CITY

		public string REIN_CONTACT_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_CITY"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_COUNTRY

		public string REIN_CONTACT_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_COUNTRY"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_STATE

		public string REIN_CONTACT_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_STATE"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_ZIP

		public string REIN_CONTACT_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_ZIP"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_PHONE_1

		public string REIN_CONTACT_PHONE_1
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_PHONE_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_PHONE_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_PHONE_1"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_PHONE_2

		public string REIN_CONTACT_PHONE_2
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_PHONE_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_PHONE_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_PHONE_2"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_EXT_1

		public string REIN_CONTACT_EXT_1
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_EXT_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_EXT_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_EXT_1"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_EXT_2

		public string REIN_CONTACT_EXT_2
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_EXT_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_EXT_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_EXT_2"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_ADDRESS_1

		public string M_REIN_CONTACT_ADDRESS_1
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_ADDRESS_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_ADDRESS_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_ADDRESS_1"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_ADDRESS_2

		public string M_REIN_CONTACT_ADDRESS_2
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_ADDRESS_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_ADDRESS_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_ADDRESS_2"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_CITY

		public string M_REIN_CONTACT_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_CITY"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_COUNTRY

		public string M_REIN_CONTACT_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_COUNTRY"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_STATE

		public string M_REIN_CONTACT_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_STATE"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_ZIP

		public string M_REIN_CONTACT_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_ZIP"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_PHONE_1

		public string M_REIN_CONTACT_PHONE_1
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_PHONE_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_PHONE_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_PHONE_1"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_PHONE_2

		public string M_REIN_CONTACT_PHONE_2
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_PHONE_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_PHONE_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_PHONE_2"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_EXT_1

		public string M_REIN_CONTACT_EXT_1
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_EXT_1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_EXT_1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_EXT_1"] = value;
			}
		}
		#endregion
		#region M_REIN_CONTACT_EXT_2

		public string M_REIN_CONTACT_EXT_2
		{
			get
			{
				return base.dtModel.Rows[0]["M_REIN_CONTACT_EXT_2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["M_REIN_CONTACT_EXT_2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["M_REIN_CONTACT_EXT_2"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_MOBILE

		public string REIN_CONTACT_MOBILE
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_MOBILE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_MOBILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_MOBILE"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_FAX

		public string REIN_CONTACT_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_FAX"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_SPEED_DIAL

		public string REIN_CONTACT_SPEED_DIAL
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_SPEED_DIAL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_SPEED_DIAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_SPEED_DIAL"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_EMAIL_ADDRESS

		public string REIN_CONTACT_EMAIL_ADDRESS
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_EMAIL_ADDRESS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_EMAIL_ADDRESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_EMAIL_ADDRESS"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_CONTRACT_DESC

		public string REIN_CONTACT_CONTRACT_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_CONTRACT_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_CONTRACT_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_CONTRACT_DESC"] = value;
			}
		}
		#endregion
		#region REIN_CONTACT_COMMENTS

		public string REIN_CONTACT_COMMENTS
		{
			get
			{
				return base.dtModel.Rows[0]["REIN_CONTACT_COMMENTS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REIN_CONTACT_COMMENTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REIN_CONTACT_COMMENTS"] = value;
			}
		}
		#endregion

		#region Commented Region
		/*#region IS_ACTIVE

		public string IS_ACTIVE
		{
			get
			{
				return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_ACTIVE"] = value;
			}
		}
		#endregion
		#region CREATED_BY

		public int CREATED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CREATED_BY"] = value;
			}
		}
		#endregion
		#region CREATED_DATETIME

		public DateTime CREATED_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
			}
		}
		#endregion
		#region MODIFIED_BY

		public int MODIFIED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODIFIED_BY"] = value;
			}
		}
		#endregion
		#region LAST_UPDATED_DATETIME

		public DateTime LAST_UPDATED_DATETIME
		{
            get
			{
                return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
			}
		}
		#endregion*/
		#endregion

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

		# endregion
		

	}
}
