/******************************************************************************************
	<Author					: - > Anurag Verma	
	<Start Date				: -	> March 22,2005
	<End Date				: - >
	<Description			: - > This is a model class that represents mnt_default_value_list table
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 
	<Modified By			: - > 
	<Purpose				: - >   
    
*******************************************************************************************/

using System;

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Summary description for ClsDefaultValueInfo.
	/// </summary>
	public class ClsDefaultValueInfo : Cms.Model.ClsCommonModel  
	{
	    private const string MNT_DEFAULT_VALUE_LIST = "MNT_DEFAULT_VALUE_LIST";

        public ClsDefaultValueInfo()
		{
            base.dtModel.TableName = "MNT_DEFAULT_VALUE_LIST";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table MNT_DEFAULT_VALUE_LIST
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

        private void AddColumns()
        {
            base.dtModel.Columns.Add("DEFV_ID",typeof(int));
            base.dtModel.Columns.Add("DEFV_ENTITY_NAME",typeof(string));
            base.dtModel.Columns.Add("DEFV_VALUE",typeof(string));
        }

        #region Database schema details
        // model for database field DEFV_ID(int)
        public int DEFV_ID
        {
            get
            {
                return base.dtModel.Rows[0]["DEFV_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DEFV_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEFV_ID"] = value;
            }
        }
        // model for database field DEFV_ENTITY_NAME(string)
        public string DEFV_ENTITY_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["DEFV_ENTITY_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEFV_ENTITY_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DEFV_ENTITY_NAME"] = value;
            }
        }
        // model for database field DEFV_VALUE(string)
        public string DEFV_VALUE
        {
            get
            {
                return base.dtModel.Rows[0]["DEFV_VALUE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEFV_VALUE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DEFV_VALUE"] = value;
            }
        }
		//ARC 19 May, 2005 Commented following 5 properties which are not 
		//required here, because they are already declared in 
		//base class i.e. Cms.Model.ClsCommonModel
		#region Commented by Arun
//        // model for database field IS_ACTIVE(string)
//        public string IS_ACTIVE
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
//            }
//            set
//            {
//                base.dtModel.Rows[0]["IS_ACTIVE"] = value;
//            }
//        }
//        // model for database field CREATED_BY(int)
//        public int CREATED_BY
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["CREATED_BY"] = value;
//            }
//        }
//        // model for database field CREATED_DATETIME(DateTime)
//        public DateTime CREATED_DATETIME
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
//            }
//        }
//        // model for database field MODIFIED_BY(int)
//        public int MODIFIED_BY
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["MODIFIED_BY"] = value;
//            }
//        }
//        // model for database field LAST_UPDATED_DATETIME(DateTime)
//        public DateTime LAST_UPDATED_DATETIME
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
//            }
//        }
		#endregion Commented by Arun
        #endregion

	}
}
