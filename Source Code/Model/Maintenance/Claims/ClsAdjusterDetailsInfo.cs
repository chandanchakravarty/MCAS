/******************************************************************************************
<Author				: -   
<Start Date				: -	4/21/2006 11:41:56 AM
<End Date				: -	
<Description				: - 	
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
namespace Cms.Model.Maintenance.Claims
{
	/// <summary>
	/// Database Model for CLM_ADJUSTER.
	/// </summary>
	public class ClsAdjusterDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_ADJUSTER = "CLM_ADJUSTER";
		public ClsAdjusterDetailsInfo()
		{
			base.dtModel.TableName = "CLM_ADJUSTER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_ADJUSTER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("ADJUSTER_ID",typeof(int));
			base.dtModel.Columns.Add("ADJUSTER_TYPE",typeof(int));
			base.dtModel.Columns.Add("ADJUSTER_NAME",typeof(string));
			base.dtModel.Columns.Add("ADJUSTER_CODE",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_LEGAL_NAME",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_CITY",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_STATE",typeof(int));
			base.dtModel.Columns.Add("SUB_ADJUSTER_ZIP",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_PHONE",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_FAX",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_EMAIL",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_WEBSITE",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_NOTES",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_CONTACT_NAME",typeof(string));
			base.dtModel.Columns.Add("SA_ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("SA_ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("SA_CITY",typeof(string));
			base.dtModel.Columns.Add("SA_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("SA_STATE",typeof(int));
			base.dtModel.Columns.Add("SA_ZIPCODE",typeof(string));
			base.dtModel.Columns.Add("SA_PHONE",typeof(string));
			base.dtModel.Columns.Add("SA_FAX",typeof(string));
			base.dtModel.Columns.Add("LOB_ID",typeof(string));
			base.dtModel.Columns.Add("USER_ID",typeof(int)); //Added By Asfa 29-Aug-2007
            base.dtModel.Columns.Add("DISPLAY_ON_CLAIM", typeof(int)); //Added By Asfa 29-Aug-2007

            //Added by Agniswar for Singapore Implementation on 16 Seo 2011
            base.dtModel.Columns.Add("SUB_ADJUSTER_GST", typeof(string));
            base.dtModel.Columns.Add("SUB_ADJUSTER_GST_REG_NO", typeof(string));
            base.dtModel.Columns.Add("SUB_ADJUSTER_MOBILE", typeof(string));
            base.dtModel.Columns.Add("SUB_ADJUSTER_CLASSIFICATION", typeof(string));

		}
		#region Database schema details
		// model for database field ADJUSTER_ID(int)
		public int ADJUSTER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUSTER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ADJUSTER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADJUSTER_ID"] = value;
			}
		}
		// model for database field USER_ID(int)
		public int USER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["USER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USER_ID"] = value;
			}
		}
		// model for database field ADJUSTER_TYPE(int)
		public int ADJUSTER_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUSTER_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ADJUSTER_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADJUSTER_TYPE"] = value;
			}
		}

        // model for database field DISPLAY_ON_CLAIM(int)
        public int DISPLAY_ON_CLAIM
        {
            get
            {
                return base.dtModel.Rows[0]["DISPLAY_ON_CLAIM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DISPLAY_ON_CLAIM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DISPLAY_ON_CLAIM"] = value;
            }
        }
		// model for database field ADJUSTER_NAME(string)
		public string ADJUSTER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUSTER_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADJUSTER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADJUSTER_NAME"] = value;
			}
		}
		public string ADJUSTER_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUSTER_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADJUSTER_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADJUSTER_CODE"] = value;
			}
		}		
		// model for database field SUB_ADJUSTER(string)
		public string SUB_ADJUSTER
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_LEGAL_NAME(string)
		public string SUB_ADJUSTER_LEGAL_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_LEGAL_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_LEGAL_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_LEGAL_NAME"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_ADDRESS1(string)
		public string SUB_ADJUSTER_ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_ADDRESS1"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_ADDRESS2(string)
		public string SUB_ADJUSTER_ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_ADDRESS2"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_CITY(string)
		public string SUB_ADJUSTER_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_CITY"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_STATE(int)
		public int SUB_ADJUSTER_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_STATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SUB_ADJUSTER_STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_STATE"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_ZIP(string)
		public string SUB_ADJUSTER_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_ZIP"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_PHONE(string)
		public string SUB_ADJUSTER_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_PHONE"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_FAX(string)
		public string SUB_ADJUSTER_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_FAX"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_EMAIL(string)
		public string SUB_ADJUSTER_EMAIL
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_EMAIL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_EMAIL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_EMAIL"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_WEBSITE(string)
		public string SUB_ADJUSTER_WEBSITE
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_WEBSITE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_WEBSITE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_WEBSITE"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_NOTES(string)
		public string SUB_ADJUSTER_NOTES
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_NOTES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_NOTES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_NOTES"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_COUNTRY(string)
		public string SUB_ADJUSTER_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_ADJUSTER_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_COUNTRY"] = value;
			}
		}
		// model for database field SUB_ADJUSTER_CONTACT_NAME(string)
		public string SUB_ADJUSTER_CONTACT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_CONTACT_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_ADJUSTER_CONTACT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_CONTACT_NAME"] = value;
			}
		}
		// model for database field SA_ADDRESS1(string)
		public string SA_ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["SA_ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_ADDRESS1"] = value;
			}
		}
		// model for database field SA_ADDRESS2(string)
		public string SA_ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["SA_ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_ADDRESS2"] = value;
			}
		}
		// model for database field SA_CITY(string)
		public string SA_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["SA_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_CITY"] = value;
			}
		}
		// model for database field SA_COUNTRY(string)
		public string SA_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["SA_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SA_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_COUNTRY"] = value;
			}
		}
		// model for database field SA_STATE(int)
		public int SA_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["SA_STATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SA_STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SA_STATE"] = value;
			}
		}
		// model for database field SA_ZIPCODE(string)
		public string SA_ZIPCODE
		{
			get
			{
				return base.dtModel.Rows[0]["SA_ZIPCODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_ZIPCODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_ZIPCODE"] = value;
			}
		}
		// model for database field SA_PHONE(string)
		public string SA_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["SA_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_PHONE"] = value;
			}
		}
		// model for database field SA_FAX(string)
		public string SA_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["SA_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SA_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SA_FAX"] = value;
			}
		}
		// model for database field LOB_ID(string)
		public string LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOB_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}


        public string SUB_ADJUSTER_GST
        {
            get
            {
                return base.dtModel.Rows[0]["SUB_ADJUSTER_GST"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_GST"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUB_ADJUSTER_GST"] = value;
            }
        }

        public string SUB_ADJUSTER_GST_REG_NO
        {
            get
            {
                return base.dtModel.Rows[0]["SUB_ADJUSTER_GST_REG_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_GST_REG_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUB_ADJUSTER_GST_REG_NO"] = value;
            }
        }

        public string SUB_ADJUSTER_MOBILE
        {
            get
            {
                return base.dtModel.Rows[0]["SUB_ADJUSTER_MOBILE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_MOBILE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUB_ADJUSTER_MOBILE"] = value;
            }
        }

        public string SUB_ADJUSTER_CLASSIFICATION
        {
            get
            {
                return base.dtModel.Rows[0]["SUB_ADJUSTER_CLASSIFICATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SUB_ADJUSTER_CLASSIFICATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUB_ADJUSTER_CLASSIFICATION"] = value;
            }
        }
		#endregion
	}
}
