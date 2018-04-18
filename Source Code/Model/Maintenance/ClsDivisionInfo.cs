/******************************************************************************************
<Author					: -   Ashwani
<Start Date				: -	5/10/2005 10:28:03 AM
<End Date				: -	
<Description			: - 	Model for Division table.
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
namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_DIV_LIST.
	/// </summary>
	public class ClsDivisionInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_DIV_LIST = "MNT_DIV_LIST";
		public ClsDivisionInfo()
		{
			base.dtModel.TableName = "MNT_DIV_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_DIV_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("DIV_CODE",typeof(string));
			base.dtModel.Columns.Add("DIV_NAME",typeof(string));
			base.dtModel.Columns.Add("DIV_ADD1",typeof(string));
			base.dtModel.Columns.Add("DIV_ADD2",typeof(string));
			base.dtModel.Columns.Add("DIV_CITY",typeof(string));
			base.dtModel.Columns.Add("DIV_STATE",typeof(string));
			base.dtModel.Columns.Add("DIV_ZIP",typeof(string));
			base.dtModel.Columns.Add("DIV_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("DIV_PHONE",typeof(string));
			base.dtModel.Columns.Add("DIV_EXT",typeof(string));
			base.dtModel.Columns.Add("DIV_FAX",typeof(string));
			base.dtModel.Columns.Add("DIV_EMAIL",typeof(string));
			base.dtModel.Columns.Add("NAIC_CODE",typeof(string));
            base.dtModel.Columns.Add("BRANCH_CODE", typeof(string));
            base.dtModel.Columns.Add("BRANCH_CNPJ", typeof(string));
            base.dtModel.Columns.Add("ACTIVITY", typeof(int));
            base.dtModel.Columns.Add("REGIONAL_IDENTIFICATION", typeof(string));
            base.dtModel.Columns.Add("REG_ID_ISSUE", typeof(string));
            base.dtModel.Columns.Add("CPF", typeof(string));
            base.dtModel.Columns.Add("DATE_OF_BIRTH", typeof(DateTime));
            base.dtModel.Columns.Add("REG_ID_ISSUE_DATE", typeof(DateTime));
		}
		#region Database schema details
		// model for database field DIV_ID(int)
		public int DIV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIV_ID"] = value;
			}
		}
		// model for database field DIV_CODE(string)
		public string DIV_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_CODE"] = value;
			}
		}
		// model for database field DIV_NAME(string)
		public string DIV_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_NAME"] = value;
			}
		}
		// model for database field DIV_ADD1(string)
		public string DIV_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_ADD1"] = value;
			}
		}
		// model for database field DIV_ADD2(string)
		public string DIV_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_ADD2"] = value;
			}
		}
		// model for database field DIV_CITY(string)
		public string DIV_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_CITY"] = value;
			}
		}
		// model for database field DIV_STATE(string)
		public string DIV_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_STATE"] = value;
			}
		}
		// model for database field DIV_ZIP(string)
		public string DIV_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_ZIP"] = value;
			}
		}
		// model for database field DIV_COUNTRY(string)
		public string DIV_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_COUNTRY"] = value;
			}
		}
		// model for database field DIV_PHONE(string)
		public string DIV_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_PHONE"] = value;
			}
		}
		// model for database field DIV_EXT(string)
		public string DIV_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_EXT"] = value;
			}
		}
		// model for database field DIV_FAX(string)
		public string DIV_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_FAX"] = value;
			}
		}
		// model for database field DIV_EMAIL(string)
		public string DIV_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_EMAIL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DIV_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIV_EMAIL"] = value;
			}
		}
		// model for database field NAIC_CODE(string)
		public string NAIC_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["NAIC_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NAIC_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NAIC_CODE"] = value;
			}
		}
        // model for database field BRANCH_CODE(string)
        public string BRANCH_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["BRANCH_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BRANCH_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BRANCH_CODE"] = value;
            }
        }

        // model for database field BRANCH_CNPJ(string)
        public string BRANCH_CNPJ
        {
            get
            {
                return base.dtModel.Rows[0]["BRANCH_CNPJ"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BRANCH_CNPJ"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["BRANCH_CNPJ"] = value;
            }
        }

        // model for database field DATE_OF_BIRTH(string)
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
        public DateTime DATE_OF_BIRTH
        {
            get
            {
                return base.dtModel.Rows[0]["DATE_OF_BIRTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_OF_BIRTH"]);

            }
            set
            {
                base.dtModel.Rows[0]["DATE_OF_BIRTH"] = value;
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
		#endregion
	}
}
