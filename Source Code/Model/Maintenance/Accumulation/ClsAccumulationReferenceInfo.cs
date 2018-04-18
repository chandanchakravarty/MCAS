/******************************************************************************************
<Author				: - Kuldeep Saxena
<Start Date			: -	24/10/2011
<End Date			: -	
<Description		: - Database Model for MNT_ACCUMULATION_REFERENCE
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance.Accumulation
{
  public  class ClsAccumulationReferenceInfo:Cms.Model.ClsCommonModel
    {
      /// <summary>
        /// Database Model for MNT_ACCUMULATION_REFERENCE.
	/// </summary>

      private const string MNT_ACCUMULATION_REFERENCE = "MNT_ACCUMULATION_REFERENCE";
        public ClsAccumulationReferenceInfo()
		{
            base.dtModel.TableName = "MNT_ACCUMULATION_REFERENCE";	// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table MNT_ACCUMULATION_REFERENCE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
            base.dtModel.Columns.Add("ACC_ID", typeof(int));
            base.dtModel.Columns.Add("ACC_REF_NO", typeof(string));
            base.dtModel.Columns.Add("LOB_ID", typeof(int));
            base.dtModel.Columns.Add("CRITERIA_ID", typeof(int));
            base.dtModel.Columns.Add("CRITERIA_VALUE", typeof(string));
            base.dtModel.Columns.Add("TREATY_CAPACITY_LIMIT", typeof(double));
            base.dtModel.Columns.Add("USED_LIMIT", typeof(double));
            base.dtModel.Columns.Add("EFFECTIVE_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("EXPIRATION_DATE", typeof(DateTime));
   		}
		#region Database schema details
        // model for database field [ACC_ID] INT
        public int ACC_ID
        {
            get
            {
                return base.dtModel.Rows[0]["ACC_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ACC_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACC_ID"] = value;
            }
        }

        // model for database field [ACC_REF_NO] NVARCHAR
        public string ACC_REF_NO
		{
			get
			{
                return base.dtModel.Rows[0]["ACC_REF_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ACC_REF_NO"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["ACC_REF_NO"] = value;
			}
		}
        // model for database field [LOBID] int
        public int LOB_ID
        {
            get
            {
                return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LOB_ID"] = value;
            }
        }
        // model for database field CRITERIA_ID(int)
        public int CRITERIA_ID
		{
			get
			{
                return base.dtModel.Rows[0]["CRITERIA_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CRITERIA_ID"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["CRITERIA_ID"] = value;
			}
		}

        // model for database field [CRITERIA_VALUE] NVARCHAR
        public string CRITERIA_VALUE
		{
			get
			{
                return base.dtModel.Rows[0]["CRITERIA_VALUE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CRITERIA_VALUE"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["CRITERIA_VALUE"] = value;
			}
		}
        // model for database field  [TREATY_CAPACITY_LIMIT] DECIMAL
        public double TREATY_CAPACITY_LIMIT
		{
			get
			{
                return base.dtModel.Rows[0]["TREATY_CAPACITY_LIMIT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["TREATY_CAPACITY_LIMIT"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["TREATY_CAPACITY_LIMIT"] = value;
			}
		}

        // model for database [USED_LIMIT] DECIMAL
        public double USED_LIMIT
        {
            get
            {
                return base.dtModel.Rows[0]["USED_LIMIT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["USED_LIMIT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["USED_LIMIT"] = value;
            }
        }
        // model for database  [EFFECTIVE_DATE] DATETIME
        public DateTime EFFECTIVE_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue: Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EFFECTIVE_DATE"] = value;
            }
        }
        // model for database  [EXPIRATION_DATE] DATETIME
        public DateTime EXPIRATION_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["EXPIRATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXPIRATION_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXPIRATION_DATE"] = value;
            }
        }
    
        
		#endregion
    }
}
