/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	20/04/2006
<End Date				: -	
<Description				: - 	Models CLM_AUTHORITY_LIMIT
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
	/// Database Model for CLM_AUTHORITY_LIMIT.
	/// </summary>
	public class ClsLimitsOfAuthorityInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_AUTHORITY_LIMIT = "CLM_AUTHORITY_LIMIT";
		public ClsLimitsOfAuthorityInfo()
		{
			base.dtModel.TableName = "CLM_AUTHORITY_LIMIT";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_AUTHORITY_LIMIT
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{

			base.dtModel.Columns.Add("LIMIT_ID",typeof(int));
			base.dtModel.Columns.Add("AUTHORITY_LEVEL",typeof(int));
			base.dtModel.Columns.Add("TITLE",typeof(string));
			base.dtModel.Columns.Add("PAYMENT_LIMIT",typeof(double));
			base.dtModel.Columns.Add("RESERVE_LIMIT",typeof(double));
            //Added by Agniswar for Singapore Implementation
            base.dtModel.Columns.Add("REOPEN_CLAIM_LIMIT", typeof(double));
            base.dtModel.Columns.Add("GRATIA_CLAIM_AMOUNT", typeof(double));
            //
			base.dtModel.Columns.Add("CLAIM_ON_DUMMY_POLICY",typeof(bool));
		}
		#region Database schema details
		// model for database field LIMIT_ID(int)
		public int LIMIT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LIMIT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_ID"] = value;
			}
		}
		// model for database field AUTHORITY_LEVEL(int)
		public int AUTHORITY_LEVEL
		{
			get
			{
				return base.dtModel.Rows[0]["AUTHORITY_LEVEL"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AUTHORITY_LEVEL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AUTHORITY_LEVEL"] = value;
			}
		}
		
		// model for database field TITLE(string)
		public string TITLE
		{
			get
			{
				return base.dtModel.Rows[0]["TITLE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TITLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TITLE"] = value;
			}
		}
		// model for database field PAYMENT_LIMIT(double)		
		public double PAYMENT_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["PAYMENT_LIMIT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["PAYMENT_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAYMENT_LIMIT"] = value;
			}
		}
		// model for database field RESERVE_LIMIT(double)		
		public double RESERVE_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["RESERVE_LIMIT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RESERVE_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESERVE_LIMIT"] = value;
			}
		}

        //Added by Agniswar for Singapore Implementation
        // model for database field RESERVE_LIMIT(double)		
        public double REOPEN_CLAIM_LIMIT
        {
            get
            {
                return base.dtModel.Rows[0]["REOPEN_CLAIM_LIMIT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["REOPEN_CLAIM_LIMIT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["REOPEN_CLAIM_LIMIT"] = value;
            }
        }

        // model for database field RESERVE_LIMIT(double)		
        public double GRATIA_CLAIM_AMOUNT
        {
            get
            {
                return base.dtModel.Rows[0]["GRATIA_CLAIM_AMOUNT"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["GRATIA_CLAIM_AMOUNT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["GRATIA_CLAIM_AMOUNT"] = value;
            }
        }

        // Till here
		// model for database field CLAIM_ON_DUMMY_POLICY(decimal)
		public bool CLAIM_ON_DUMMY_POLICY
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ON_DUMMY_POLICY"] == DBNull.Value ? Convert.ToBoolean(null) :bool.Parse( base.dtModel.Rows[0]["CLAIM_ON_DUMMY_POLICY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ON_DUMMY_POLICY"] = value;
			}
		}
		#endregion
	}
}
