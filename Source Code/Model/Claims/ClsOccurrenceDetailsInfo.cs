/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	5/3/2006 3:50:55 PM
<End Date				: -	
<Description				: - 	Model Class for Occurance Details
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_OCCURRENCE_DETAIL.
	/// </summary>
	public class ClsOccurrenceDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_OCCURRENCE_DETAIL = "CLM_OCCURRENCE_DETAIL";
		public ClsOccurrenceDetailsInfo()
		{
			base.dtModel.TableName = "CLM_OCCURRENCE_DETAIL";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_OCCURRENCE_DETAIL
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("OCCURRENCE_DETAIL_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("LOSS_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("AUTHORITY_CONTACTED",typeof(string));
			base.dtModel.Columns.Add("REPORT_NUMBER",typeof(string));
			base.dtModel.Columns.Add("VIOLATIONS",typeof(string));
			base.dtModel.Columns.Add("LOSS_TYPE",typeof(string));
			base.dtModel.Columns.Add("LOSS_LOCATION",typeof(string));
			base.dtModel.Columns.Add("ESTIMATE_AMOUNT",typeof(double));	
			base.dtModel.Columns.Add("OTHER_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("WATERBACKUP_SUMPPUMP_LOSS",typeof(int)); //Added by Charles on 1-Dec-09 for Itrack 6647
			base.dtModel.Columns.Add("WEATHER_RELATED_LOSS",typeof(int)); //Added for Itrack 6640 on 9 Dec 09
            //Added by Santosh Kumar Gautam on 25 Nov 2010
            base.dtModel.Columns.Add("LOSS_LOCATION_ZIP", typeof(string));
            base.dtModel.Columns.Add("LOSS_LOCATION_CITY", typeof(string));
            base.dtModel.Columns.Add("LOSS_LOCATION_STATE", typeof(int)); 
		}
		#region Database schema details
		// model for database field OCCURRENCE_DETAIL_ID(int)
		public int OCCURRENCE_DETAIL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["OCCURRENCE_DETAIL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["OCCURRENCE_DETAIL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OCCURRENCE_DETAIL_ID"] = value;
			}
		}
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field LOSS_DESCRIPTION(string)
		public string LOSS_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOSS_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_DESCRIPTION"] = value;
			}
		}
		// model for database field AUTHORITY_CONTACTED(string)
		public string AUTHORITY_CONTACTED
		{
			get
			{
				return base.dtModel.Rows[0]["AUTHORITY_CONTACTED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AUTHORITY_CONTACTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AUTHORITY_CONTACTED"] = value;
			}
		}
		// model for database field REPORT_NUMBER(string)
		public string REPORT_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["REPORT_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REPORT_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REPORT_NUMBER"] = value;
			}
		}
		// model for database field VIOLATIONS(string)
		public string VIOLATIONS
		{
			get
			{
				return base.dtModel.Rows[0]["VIOLATIONS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VIOLATIONS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VIOLATIONS"] = value;
			}
		}
		// model for database field LOSS_TYPE(string)
		public string LOSS_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOSS_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_TYPE"] = value;
			}
		}
		// model for database field LOSS_LOCATION(string)
		public string LOSS_LOCATION
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_LOCATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOSS_LOCATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_LOCATION"] = value;
			}
		}
		// model for database field ESTIMATE_AMOUNT(double)
		public double ESTIMATE_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["ESTIMATE_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ESTIMATE_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ESTIMATE_AMOUNT"] = value;
			}
		}
		// model for database field OTHER_DESCRIPTION(string)
		public string OTHER_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_DESCRIPTION"] = value;
			}
		}
		//Added by Charles on 1-Dec-09 for Itrack 6647
		public int WATERBACKUP_SUMPPUMP_LOSS
		{
			get
			{
				return base.dtModel.Rows[0]["WATERBACKUP_SUMPPUMP_LOSS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["WATERBACKUP_SUMPPUMP_LOSS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WATERBACKUP_SUMPPUMP_LOSS"] = value;
			}
		}
		// model for database field WEATHER_RELATED_LOSS(int)
		//Added for Itrack 6640 on 9 Dec 09
		public int WEATHER_RELATED_LOSS
		{
			get
			{
				return base.dtModel.Rows[0]["WEATHER_RELATED_LOSS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["WEATHER_RELATED_LOSS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WEATHER_RELATED_LOSS"] = value;
			}
		}

        // model for database field LOSS_LOCATION_ZIP(string)
        public string LOSS_LOCATION_ZIP
        {
            get
            {
                return base.dtModel.Rows[0]["LOSS_LOCATION_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOSS_LOCATION_ZIP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LOSS_LOCATION_ZIP"] = value;
            }
        }
            // model for database field LOSS_LOCATION_CITY(string)
        public string LOSS_LOCATION_CITY
        {
            get
            {
                return base.dtModel.Rows[0]["LOSS_LOCATION_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOSS_LOCATION_CITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LOSS_LOCATION_CITY"] = value;
            }
        }

        // model for database field LOSS_LOCATION_STATE(int)
        public int LOSS_LOCATION_STATE
        {
            get
            {
                return base.dtModel.Rows[0]["LOSS_LOCATION_STATE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOSS_LOCATION_STATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LOSS_LOCATION_STATE"] = value;
            }
        }
		#endregion
	}
}
