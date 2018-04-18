/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	5/9/2005 11:20:30 AM
<End Date				: -	
<Description				: - 	This file is used to 
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_USER_LIST.
	/// </summary>
	public class ClsUserInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_USER_LIST = "MNT_USER_LIST";
		public ClsUserInfo()
		{
			base.dtModel.TableName = "MNT_USER_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_USER_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("USER_ID",typeof(int));
			base.dtModel.Columns.Add("USER_LOGIN_ID",typeof(string));
			base.dtModel.Columns.Add("USER_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_CODE",typeof(string));	
			base.dtModel.Columns.Add("USER_PWD",typeof(string));
			base.dtModel.Columns.Add("USER_TITLE",typeof(string));
			base.dtModel.Columns.Add("USER_FNAME",typeof(string));
			base.dtModel.Columns.Add("USER_LNAME",typeof(string));
			base.dtModel.Columns.Add("USER_INITIALS",typeof(string));
			base.dtModel.Columns.Add("USER_ADD1",typeof(string));
			base.dtModel.Columns.Add("USER_ADD2",typeof(string));
			base.dtModel.Columns.Add("USER_CITY",typeof(string));
			base.dtModel.Columns.Add("USER_STATE",typeof(string));
			base.dtModel.Columns.Add("USER_ZIP",typeof(string));
			base.dtModel.Columns.Add("USER_PHONE",typeof(string));
			base.dtModel.Columns.Add("USER_EXT",typeof(string));
			base.dtModel.Columns.Add("USER_FAX",typeof(string));
			base.dtModel.Columns.Add("USER_MOBILE",typeof(string));
			base.dtModel.Columns.Add("USER_EMAIL",typeof(string));
			base.dtModel.Columns.Add("USER_SPR",typeof(string));
			base.dtModel.Columns.Add("PINK_SLIP_NOTIFY",typeof(string));
			base.dtModel.Columns.Add("USER_MGR_ID",typeof(int));
			base.dtModel.Columns.Add("USER_DEF_DIV_ID",typeof(int));
			base.dtModel.Columns.Add("USER_DEF_DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("USER_DEF_PC_ID",typeof(int));
			base.dtModel.Columns.Add("USER_CHANGE_COMM",typeof(string));
			base.dtModel.Columns.Add("USER_VIEW_COMM",typeof(string));
			base.dtModel.Columns.Add("USER_BAD_LOGINS",typeof(int));
			base.dtModel.Columns.Add("USER_LOCKED",typeof(string));
			base.dtModel.Columns.Add("USER_LOCKED_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("USER_TIME_ZONE",typeof(string));
			base.dtModel.Columns.Add("USER_NOTES",typeof(string));//*************
			base.dtModel.Columns.Add("USER_SHOW_COMPLETE_TODOLIST",typeof(string));
			base.dtModel.Columns.Add("USER_INACTIVE_DATETIME",typeof(DateTime));
			base.dtModel.Columns.Add("USER_SYSTEM_ID",typeof(string));
			base.dtModel.Columns.Add("USER_IMAGE_FOLDER",typeof(string));
			base.dtModel.Columns.Add("USER_COLOR_SCHEME",typeof(string));
			base.dtModel.Columns.Add("COUNTRY",typeof(string));
			base.dtModel.Columns.Add("SSN_NO",typeof(string));
			base.dtModel.Columns.Add("GRID_SIZE",typeof(int));
			base.dtModel.Columns.Add("DATE_OF_BIRTH",typeof(DateTime));
			base.dtModel.Columns.Add("DRIVER_LIC_NO",typeof(string));
			base.dtModel.Columns.Add("DATE_EXPIRY",typeof(DateTime));
			base.dtModel.Columns.Add("LICENSE_STATUS",typeof(string));
			base.dtModel.Columns.Add("NON_RESI_LICENSE_STATE",typeof(string));
			base.dtModel.Columns.Add("NON_RESI_LICENSE_NO",typeof(string));
			//Added by Manoj Rathore (06 Nov 2006)

			base.dtModel.Columns.Add("NON_RESI_LICENSE_STATE2",typeof(string));
			base.dtModel.Columns.Add("NON_RESI_LICENSE_NO2",typeof(string));
			base.dtModel.Columns.Add("BRICS_USER",typeof(int));
			//--------

			base.dtModel.Columns.Add("LIC_BRICS_USER",typeof(int));
			base.dtModel.Columns.Add("ADJUSTER_CODE",typeof(string));
			base.dtModel.Columns.Add("CHANGE_PWD_NEXT_LOGIN",typeof(int));// Added by Sibin on 9 Dec 08 for Itrack Issue 4994
			//sibin
			base.dtModel.Columns.Add("NON_RESI_LICENSE_EXP_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("NON_RESI_LICENSE_STATUS",typeof(string));
			base.dtModel.Columns.Add("NON_RESI_LICENSE_EXP_DATE2",typeof(DateTime));
			base.dtModel.Columns.Add("NON_RESI_LICENSE_STATUS2",typeof(string));
            base.dtModel.Columns.Add("ACTIVITY", typeof(int));
            base.dtModel.Columns.Add("REGIONAL_IDENTIFICATION", typeof(string));
            base.dtModel.Columns.Add("REG_ID_ISSUE", typeof(string));
            base.dtModel.Columns.Add("CPF", typeof(string));
            base.dtModel.Columns.Add("REG_ID_ISSUE_DATE", typeof(DateTime));

            //Added by Chetna on March 03,10
            base.dtModel.Columns.Add("LANG_ID", typeof(int));
		}
		#region Database schema details
		// model for database field USER_ID(int)
		public int USER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_ID"] = value;
			}
		}
		// model for database field SUB_CODE(string)
		
		public string SUB_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_CODE"] = value;
			}
		}

		public string USER_LOGIN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_LOGIN_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_LOGIN_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_LOGIN_ID"] = value;
			}
		}
		// model for database field USER_TYPE_ID(int)
		public int USER_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_TYPE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_TYPE_ID"] = value;
			}
		}
		// model for database field USER_TYPE_ID(int)
		public int COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COUNTRY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY"] = value;
			}
		}
		// model for database field NON_RESI_LICENSE_STATE(string)
		public string NON_RESI_LICENSE_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["NON_RESI_LICENSE_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NON_RESI_LICENSE_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_RESI_LICENSE_STATE"] = value;
			}
		}
		
		// model for database field NON_RESI_LICENSE_STATE2(string) By Manoj Rathore (06 Nov 2006))
		public string NON_RESI_LICENSE_STATE2
		{
			get
			{
				return base.dtModel.Rows[0]["NON_RESI_LICENSE_STATE2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NON_RESI_LICENSE_STATE2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_RESI_LICENSE_STATE2"] = value;
			}
		}




		// model for database field USER_PWD(string)
		public string USER_PWD
		{
			get
			{
				return base.dtModel.Rows[0]["USER_PWD"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_PWD"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_PWD"] = value;
			}
		}
		// model for database field USER_TITLE(string)/////////////////
		public string USER_TITLE
		{
			get
			{
				return base.dtModel.Rows[0]["USER_TITLE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_TITLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_TITLE"] = value;
			}
		}
		// model for database field USER_FNAME(string)
		public string USER_FNAME
		{
			get
			{
				return base.dtModel.Rows[0]["USER_FNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_FNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_FNAME"] = value;
			}
		}
		// model for database field USER_LNAME(string)
		public string USER_LNAME
		{
			get
			{
				return base.dtModel.Rows[0]["USER_LNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_LNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_LNAME"] = value;
			}
		}
		// model for database field USER_INITIALS(string)
		public string USER_INITIALS
		{
			get
			{
				return base.dtModel.Rows[0]["USER_INITIALS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_INITIALS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_INITIALS"] = value;
			}
		}
		// model for database field USER_ADD1(string)
		public string USER_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["USER_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_ADD1"] = value;
			}
		}
		// model for database field USER_ADD2(string)
		public string USER_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["USER_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_ADD2"] = value;
			}
		}
		// model for database field USER_CITY(string)
		public string USER_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["USER_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_CITY"] = value;
			}
		}
		// model for database field USER_STATE(string)
		public string USER_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["USER_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_STATE"] = value;
			}
		}
		// model for database field USER_ZIP(string)
		public string USER_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["USER_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_ZIP"] = value;
			}
		}
		// model for database field USER_PHONE(string)
		public string USER_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["USER_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_PHONE"] = value;
			}
		}
		// model for database field USER_EXT(string)
		public string USER_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["USER_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_EXT"] = value;
			}
		}
		// model for database field USER_FAX(string)
		public string USER_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["USER_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_FAX"] = value;
			}
		}
		// model for database field USER_MOBILE(string)
		public string USER_MOBILE
		{
			get
			{
				return base.dtModel.Rows[0]["USER_MOBILE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_MOBILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_MOBILE"] = value;
			}
		}
		// model for database field USER_EMAIL(string)
		public string USER_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["USER_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_EMAIL"] = value;
			}
		}
		// model for database field USER_SPR(string)
		public string USER_SPR
		{
			get
			{
				return base.dtModel.Rows[0]["USER_SPR"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_SPR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_SPR"] = value;
			}
		}
		
		// model for database field PINK_SLIP_NOTIFY(string)
		public string PINK_SLIP_NOTIFY
		{
			get
			{
				return base.dtModel.Rows[0]["PINK_SLIP_NOTIFY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PINK_SLIP_NOTIFY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PINK_SLIP_NOTIFY"] = value;
			}
		}
		// model for database field USER_MGR_ID(int)
		public int USER_MGR_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_MGR_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_MGR_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_MGR_ID"] = value;
			}
		}
		// model for database field USER_DEF_DIV_ID(int)
		public int USER_DEF_DIV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_DEF_DIV_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_DEF_DIV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_DEF_DIV_ID"] = value;
			}
		}
		// model for database field USER_DEF_DEPT_ID(int)
		public int USER_DEF_DEPT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_DEF_DEPT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_DEF_DEPT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_DEF_DEPT_ID"] = value;
			}
		}
		// model for database field USER_DEF_PC_ID(int)
		public int USER_DEF_PC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_DEF_PC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_DEF_PC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_DEF_PC_ID"] = value;
			}
		}
		// model for database field USER_CHANGE_COMM(string)
		public string USER_CHANGE_COMM
		{
			get
			{
				return base.dtModel.Rows[0]["USER_CHANGE_COMM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_CHANGE_COMM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_CHANGE_COMM"] = value;
			}
		}
		// model for database field USER_VIEW_COMM(string)
		public string USER_VIEW_COMM
		{
			get
			{
				return base.dtModel.Rows[0]["USER_VIEW_COMM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_VIEW_COMM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_VIEW_COMM"] = value;
			}
		}
		// model for database field USER_BAD_LOGINS(int)
		public int USER_BAD_LOGINS
		{
			get
			{
				return base.dtModel.Rows[0]["USER_BAD_LOGINS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USER_BAD_LOGINS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_BAD_LOGINS"] = value;
			}
		}
		// model for database field USER_LOCKED(string)
		public string USER_LOCKED
		{
			get
			{
				return base.dtModel.Rows[0]["USER_LOCKED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_LOCKED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_LOCKED"] = value;
			}
		}
		// model for database field CHANGE_PWD_NEXT_LOGIN(int)// Added by Sibin on 9 Dec 08 for Itrack Issue 4994
		public int CHANGE_PWD_NEXT_LOGIN
		{
			get
			{
				return base.dtModel.Rows[0]["CHANGE_PWD_NEXT_LOGIN"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CHANGE_PWD_NEXT_LOGIN"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CHANGE_PWD_NEXT_LOGIN"] = value;
			}
		}
		// model for database field USER_LOCKED_DATETIME(DateTime)
		public DateTime USER_LOCKED_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["USER_LOCKED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["USER_LOCKED_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_LOCKED_DATETIME"] = value;
			}
		}
		// model for database field USER_TIME_ZONE(string)
		public string USER_TIME_ZONE
		{
			get
			{
				return base.dtModel.Rows[0]["USER_TIME_ZONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_TIME_ZONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_TIME_ZONE"] = value;
			}
		}

		//model for database field USER_NOTES(string)
		public string USER_NOTES
		{
			get
			{
				return base.dtModel.Rows[0]["USER_NOTES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_NOTES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_NOTES"] = value;
			}
		}
		// model for database field USER_SHOW_COMPLETE_TODOLIST(string)
		public string USER_SHOW_COMPLETE_TODOLIST
		{
			get
			{
				return base.dtModel.Rows[0]["USER_SHOW_COMPLETE_TODOLIST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_SHOW_COMPLETE_TODOLIST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_SHOW_COMPLETE_TODOLIST"] = value;
			}
		}
		// model for database field USER_INACTIVE_DATETIME(DateTime)
		public DateTime USER_INACTIVE_DATETIME
		{
			get
			{
				return base.dtModel.Rows[0]["USER_INACTIVE_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["USER_INACTIVE_DATETIME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_INACTIVE_DATETIME"] = value;
			}
		}
		// model for database field USER_SYSTEM_ID(string)
		public string USER_SYSTEM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_SYSTEM_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_SYSTEM_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_SYSTEM_ID"] = value;
			}
		}
		// model for database field USER_IMAGE_FOLDER(string)
		public string USER_IMAGE_FOLDER
		{
			get
			{
				return base.dtModel.Rows[0]["USER_IMAGE_FOLDER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_IMAGE_FOLDER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_IMAGE_FOLDER"] = value;
			}
		}
		// model for database field USER_COLOR_SCHEME(string)
		public string USER_COLOR_SCHEME
		{
			get
			{
				return base.dtModel.Rows[0]["USER_COLOR_SCHEME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USER_COLOR_SCHEME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USER_COLOR_SCHEME"] = value;
			}
		}
		// model for database field GRID_SIZE(int) 
		// Added by mohit.
		public int GRID_SIZE
		{
			get
			{
				return base.dtModel.Rows[0]["GRID_SIZE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["GRID_SIZE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GRID_SIZE"] = value;
			}
		}
		// model for database field SSN_NO(string)
		public string SSN_NO
		{
			get
			{
				return base.dtModel.Rows[0]["SSN_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SSN_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SSN_NO"] = value;
			}
		}
		// model for database field DATE_OF_BIRTH(Date)
		public DateTime DATE_OF_BIRTH
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_OF_BIRTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_OF_BIRTH"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_OF_BIRTH"] = value;
			}
		}
		// model for database field DRIVER_LIC_NO(string)
		public string DRIVER_LIC_NO
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_LIC_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_LIC_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_LIC_NO"] = value;
			}
		}
		// model for database field NON_RESI_LICENSE_NO(string)
		public string NON_RESI_LICENSE_NO
		{
			get
			{
				return base.dtModel.Rows[0]["NON_RESI_LICENSE_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NON_RESI_LICENSE_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_RESI_LICENSE_NO"] = value;
			}
		}

		// model for database field NON_RESI_LICENSE_NO2(string) By Manoj Rathore (06 Nov 2006)
		public string NON_RESI_LICENSE_NO2
		{
			get
			{
				return base.dtModel.Rows[0]["NON_RESI_LICENSE_NO2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NON_RESI_LICENSE_NO2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_RESI_LICENSE_NO2"] = value;
			}
		}


		// model for database field DATE_EXPIRY(Date)
		public DateTime DATE_EXPIRY
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_EXPIRY"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_EXPIRY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_EXPIRY"] = value;
			}
		}
		// model for database field LICENSE_STATUS(string)
		public string LICENSE_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["LICENSE_STATUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LICENSE_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LICENSE_STATUS"] = value;
			}
		}
		
		//sibin
		// model for database field DATE_EXPIRY(Date)
		public DateTime NON_RESI_LICENSE_EXP_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["NON_RESI_LICENSE_EXP_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["NON_RESI_LICENSE_EXP_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NON_RESI_LICENSE_EXP_DATE"] = value;
			}
		}
		// model for database field LICENSE_STATUS(string)
		public string NON_RESI_LICENSE_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["NON_RESI_LICENSE_STATUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NON_RESI_LICENSE_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_RESI_LICENSE_STATUS"] = value;
			}
		}

		// model for database field DATE_EXPIRY(Date)
		public DateTime NON_RESI_LICENSE_EXP_DATE2
		{
			get
			{
				return base.dtModel.Rows[0]["NON_RESI_LICENSE_EXP_DATE2"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["NON_RESI_LICENSE_EXP_DATE2"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NON_RESI_LICENSE_EXP_DATE2"] = value;
			}
		}
		// model for database field LICENSE_STATUS(string)
		public string NON_RESI_LICENSE_STATUS2
		{
			get
			{
				return base.dtModel.Rows[0]["NON_RESI_LICENSE_STATUS2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NON_RESI_LICENSE_STATUS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_RESI_LICENSE_STATUS2"] = value;
			}
		}
		//sibin

		// model for database field LIC_BRICS_USER(int) 		
		public int LIC_BRICS_USER
		{
			get
			{
				return base.dtModel.Rows[0]["LIC_BRICS_USER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIC_BRICS_USER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIC_BRICS_USER"] = value;
			}
		}
		public int BRICS_USER
		{
			get
			{
				return base.dtModel.Rows[0]["BRICS_USER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BRICS_USER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BRICS_USER"] = value;
			}
		}
		// model for database field ADJUSTER_CODE(string) By Manoj Rathore (06 Nov 2006)
		public string ADJUSTER_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUSTER_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADJUSTER_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADJUSTER_CODE"] = value;
			}
		}


        //added by Chetna
        public int LANG_ID
        {
            get
            {
                return base.dtModel.Rows[0]["LANG_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LANG_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LANG_ID"] = value;
            }
        }
        public int ACTIVITY
        {
            get
            {
                return base.dtModel.Rows[0]["ACTIVITY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ACTIVITY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACTIVITY"] = value;
            }
        }
        public string CPF
        {
            get
            {
                return base.dtModel.Rows[0]["CPF"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CPF"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CPF"] = value;
            }
        }
        public string REG_ID_ISSUE
        {
            get
            {
                return base.dtModel.Rows[0]["REG_ID_ISSUE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REG_ID_ISSUE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REG_ID_ISSUE"] = value;
            }
        }
        public string REGIONAL_IDENTIFICATION
        {
            get
            {
                return base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REGIONAL_IDENTIFICATION"] = value;
            }
        }
        public DateTime REG_ID_ISSUE_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["REG_ID_ISSUE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["REG_ID_ISSUE_DATE"]);

            }
            set
            {
                base.dtModel.Rows[0]["REG_ID_ISSUE_DATE"] = value;
            }
        }




		#endregion
	}
}
