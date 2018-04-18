/******************************************************************************************

<Author : - Amit k. Mishra

<Start Date : - 10-Oct-2011 6:20:12 PM

<End Date : - 

<Description : - 

<Review Date : - 

<Reviewed By : - 

Modification History

<Modified Date : - 

<Modified By : - 

<Purpose : - 

*******************************************************************************************/ 

using System;

using System.Data;

using Cms.Model;

namespace Cms.Model.Maintenance.Reinsurance

{

	/// <summary>

	/// Database Model for MNT_CURRENT_RATING_LIST.

	/// </summary>

	public class ClsCurrentCreditRatingInfo : Cms.Model.ClsCommonModel

	{

		private const string MNT_CURRENT_RATING_LIST = "MNT_CURRENT_RATING_LIST";

		public ClsCurrentCreditRatingInfo()
		{
			base.dtModel.TableName = "MNT_CURRENT_RATING_LIST"; // setting table name for data table that holds property values.

			this.AddColumns(); // add columns of the database table MNT_CURRENT_RATING_LIST

			base.dtModel.Rows.Add(base.dtModel.NewRow()); // add a blank row in the datatable
		}
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("RATING_ID",typeof(int));
			base.dtModel.Columns.Add("COMPANY_ID",typeof(int));
			base.dtModel.Columns.Add("COMPANY_TYPE",typeof(string));
			base.dtModel.Columns.Add("AGENCY_ID",typeof(int));

			base.dtModel.Columns.Add("RATING",typeof(string));
			base.dtModel.Columns.Add("EFFECTIVE_YEAR",typeof(int));
            //base.dtModel.Columns.Add("CREATED_BY",typeof(int));
            //base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));
            //base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));
            //base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));            	
		}
		    
		#region Database schema details

		// model for database field RATING_ID(int)

		public int RATING_ID
		{
			get
			{

				return base.dtModel.Rows[0]["RATING_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RATING_ID"].ToString());

			}
			set
			{

				base.dtModel.Rows[0]["RATING_ID"] = value;

			}
		}

		// model for database field COMPANY_ID(int)

		public int COMPANY_ID
		{
		get
			{

				return base.dtModel.Rows[0]["COMPANY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMPANY_ID"].ToString());

			}
			set
			{
                base.dtModel.Rows[0]["COMPANY_ID"] = value;
			}
		}

		// model for database field COMPANY_TYPE(string)

		public string COMPANY_TYPE
		{
			get
			{
                return base.dtModel.Rows[0]["COMPANY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COMPANY_TYPE"].ToString();

			}
			set
			{
                base.dtModel.Rows[0]["COMPANY_TYPE"] = value;
            }
		}
		
		// model for database field AGENCY_ID(int)
		public int AGENCY_ID
         {
            get
                {
                    return base.dtModel.Rows[0]["AGENCY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["AGENCY_ID"].ToString());
    			}
            set
			{

				base.dtModel.Rows[0]["AGENCY_ID"] = value;
			}
		}
		
		// model for database field RATING(string)
		public string RATING
		{
            get
			{

				return base.dtModel.Rows[0]["RATING"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RATING"].ToString();
			}

			set
			{

    			base.dtModel.Rows[0]["RATING"] = value;
    		}
		}

		// model for database field EFFECTIVE_YEAR(int)

		public int EFFECTIVE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EFFECTIVE_YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_YEAR"] = value;
			}
		}

		// model for database field CREATED_BY(int)

        //public int CREATED_BY
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["CREATED_BY"] = value;
        //    }
        //}

		// model for database field MODIFIED_BY(int)

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
    
        // model for database field CREATED_DATETIME(DateTime)
		
        //public DateTime CREATED_DATETIME
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
        //    }
        //}

		// model for database field LAST_UPDATED_DATETIME(DateTime)
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

        #endregion
		
	}

}
