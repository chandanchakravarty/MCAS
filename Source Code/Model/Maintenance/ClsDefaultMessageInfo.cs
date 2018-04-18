 /******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	4/19/2005 2:27:48 PM
<End Date				: -	
<Description				: - 	Models mnt_message_list table
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
    /// Database Model for MNT_MESSAGE_LIST.
    /// </summary>
    public class ClsDefaultMessageInfo : Cms.Model.ClsCommonModel
    {
        private const string MNT_MESSAGE_LIST = "MNT_MESSAGE_LIST";
        public ClsDefaultMessageInfo()
        {
            base.dtModel.TableName = "MNT_MESSAGE_LIST";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table MNT_MESSAGE_LIST
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("MSG_ID",typeof(int));
            base.dtModel.Columns.Add("MSG_TYPE",typeof(string));
            base.dtModel.Columns.Add("MSG_CODE",typeof(string));
            base.dtModel.Columns.Add("MSG_DESC",typeof(string));
            base.dtModel.Columns.Add("MSG_TEXT",typeof(string));
            base.dtModel.Columns.Add("MSG_POSITION",typeof(string));
            base.dtModel.Columns.Add("MSG_APPLY_TO",typeof(string));
            /*base.dtModel.Columns.Add("IS_ACTIVE",typeof(string));
            base.dtModel.Columns.Add("CREATED_BY",typeof(int));
            base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));
            base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));
            base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));*/
        }
        #region Database schema details
        // model for database field MSG_ID(int)
        public int MSG_ID
        {
            get
            {
                return base.dtModel.Rows[0]["MSG_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MSG_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MSG_ID"] = value;
            }
        }
        // model for database field MSG_TYPE(string)
        public string MSG_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["MSG_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MSG_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MSG_TYPE"] = value;
            }
        }
        // model for database field MSG_CODE(string)
        public string MSG_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["MSG_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MSG_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MSG_CODE"] = value;
            }
        }
        // model for database field MSG_DESC(string)
        public string MSG_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["MSG_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MSG_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MSG_DESC"] = value;
            }
        }
        // model for database field MSG_TEXT(string)
        public string MSG_TEXT
        {
            get
            {
                return base.dtModel.Rows[0]["MSG_TEXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MSG_TEXT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MSG_TEXT"] = value;
            }
        }
        // model for database field MSG_POSITION(string)
        public string MSG_POSITION
        {
            get
            {
                return base.dtModel.Rows[0]["MSG_POSITION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MSG_POSITION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MSG_POSITION"] = value;
            }
        }
        // model for database field MSG_APPLY_TO(string)
        public string MSG_APPLY_TO
        {
            get
            {
                return base.dtModel.Rows[0]["MSG_APPLY_TO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MSG_APPLY_TO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MSG_APPLY_TO"] = value;
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
