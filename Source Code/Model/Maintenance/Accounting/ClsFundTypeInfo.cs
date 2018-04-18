/******************************************************************************************
<Author				: - Agniswar Das
<Start Date			: -	28/09/2011
<End Date			: -	
<Description		: - Database Model for MNT_FUND_TYPE
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
namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
    /// Database Model for MNT_FUND_TYPE.
	/// </summary>
	public class ClsFundTypeInfo : Cms.Model.ClsCommonModel
	{
        private const string ACT_BUDGET_CATEGORY = "MNT_FUND_TYPES";
        public ClsFundTypeInfo()
		{
            base.dtModel.TableName = "MNT_FUND_TYPES";	// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_BUDGET_CATEGORY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
            base.dtModel.Columns.Add("FUND_TYPE_ID", typeof(int));           
            base.dtModel.Columns.Add("FUND_TYPE_CODE", typeof(string));
            base.dtModel.Columns.Add("FUND_TYPE_NAME", typeof(string));
            base.dtModel.Columns.Add("FUND_TYPE_SOURCE_D", typeof(bool));
            base.dtModel.Columns.Add("FUND_TYPE_SOURCE_DO", typeof(bool));
            base.dtModel.Columns.Add("FUND_TYPE_SOURCE_RIO", typeof(bool));
            base.dtModel.Columns.Add("FUND_TYPE_SOURCE_RIA", typeof(bool));
		}
		#region Database schema details
        // model for database field FUND_TYPE_ID(int)
        public int FUND_TYPE_ID
		{
			get
			{
                return base.dtModel.Rows[0]["FUND_TYPE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FUND_TYPE_ID"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["FUND_TYPE_ID"] = value;
			}
		}

        // model for database field FUND_TYPE_CODE(string)
        public string FUND_TYPE_CODE
		{
			get
			{
                return base.dtModel.Rows[0]["FUND_TYPE_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FUND_TYPE_CODE"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["FUND_TYPE_CODE"] = value;
			}
		}
        // model for database field FUND_TYPE_NAME(string)
        public string FUND_TYPE_NAME
		{
			get
			{
                return base.dtModel.Rows[0]["FUND_TYPE_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FUND_TYPE_NAME"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["FUND_TYPE_NAME"] = value;
			}
		}

        // model for database field FUND_TYPE_SOURCE_D(string)
        public bool FUND_TYPE_SOURCE_D
        {
            get
            {
                return base.dtModel.Rows[0]["FUND_TYPE_SOURCE_D"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["FUND_TYPE_SOURCE_D"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FUND_TYPE_SOURCE_D"] = value;
            }
        }

        // model for database field FUND_TYPE_SOURCE_DO(string)
        public bool FUND_TYPE_SOURCE_DO
        {
            get
            {
                return base.dtModel.Rows[0]["FUND_TYPE_SOURCE_DO"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["FUND_TYPE_SOURCE_DO"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FUND_TYPE_SOURCE_DO"] = value;
            }
        }

        // model for database field FUND_TYPE_SOURCE_RIO(string)
        public bool FUND_TYPE_SOURCE_RIO
        {
            get
            {
                return base.dtModel.Rows[0]["FUND_TYPE_SOURCE_RIO"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["FUND_TYPE_SOURCE_RIO"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FUND_TYPE_SOURCE_RIO"] = value;
            }
        }

        // model for database field FUND_TYPE_SOURCE_RIA(string)
        public bool FUND_TYPE_SOURCE_RIA
        {
            get
            {
                return base.dtModel.Rows[0]["FUND_TYPE_SOURCE_RIA"] == DBNull.Value ? false : bool.Parse(base.dtModel.Rows[0]["FUND_TYPE_SOURCE_RIA"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FUND_TYPE_SOURCE_RIA"] = value;
            }
        }
		#endregion
	}
}
