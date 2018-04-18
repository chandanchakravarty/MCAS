/******************************************************************************************
<Author				: - Kuldeep Saxena
<Start Date			: -	24/10/2011
<End Date			: -	
<Description		: - Database Model for MNT_ACCUMULATION_CRITERIA_MASTER
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
  public  class ClsAccumulationCriteriaInfo:Cms.Model.ClsCommonModel
    {/// <summary>
        /// Database Model for MNT_ACCUMULATION_CRITERIA_MASTER.
	/// </summary>
	
        private const string MNT_ACCUMULATION_CRITERIA_MASTER = "MNT_ACCUMULATION_CRITERIA_MASTER";
        public ClsAccumulationCriteriaInfo()
		{
            base.dtModel.TableName = "MNT_ACCUMULATION_CRITERIA_MASTER";	// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_ACCUMULATION_CRITERIA_MASTER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
            base.dtModel.Columns.Add("CRITERIA_ID", typeof(int));           
            base.dtModel.Columns.Add("CRITERIA_CODE", typeof(string));
            base.dtModel.Columns.Add("CRITERIA_DESC", typeof(string));
            base.dtModel.Columns.Add("LOB_ID", typeof(int));
           
   		}
		#region Database schema details
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

        // model for database field FUND_TYPE_CODE(string)
        public string CRITERIA_CODE
		{
			get
			{
                return base.dtModel.Rows[0]["CRITERIA_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CRITERIA_CODE"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["CRITERIA_CODE"] = value;
			}
		}
        // model for database field CRITERIA_DESC(string)
        public string CRITERIA_DESC
		{
			get
			{
                return base.dtModel.Rows[0]["CRITERIA_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CRITERIA_DESC"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["CRITERIA_DESC"] = value;
			}
		}

        // model for database field LOB_ID(int)
        public int LOB_ID
        {
            get
            {
                return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? 0: int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LOB_ID"] = value;
            }
        }
     
        
		#endregion
    }
}
