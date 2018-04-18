/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	4/28/2005 10:34:57 AM
<End Date				: -	
<Description				: - 	Models APP_EXPIRY_DATES table
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
namespace Cms.Model.Application.PriorLoss
{
    /// <summary>
    /// Database Model for APP_EXPIRY_DATES.
    /// </summary>
    public class ClsExpiryDatesInfo : Cms.Model.ClsCommonModel
    {
        private const string APP_EXPIRY_DATES = "APP_EXPIRY_DATES";
        public ClsExpiryDatesInfo()
        {
            base.dtModel.TableName = "APP_EXPIRY_DATES";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table APP_EXPIRY_DATES
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
            base.dtModel.Columns.Add("EXPDT_ID",typeof(int));
            base.dtModel.Columns.Add("APP_ID",typeof(int));
            base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
            base.dtModel.Columns.Add("EXPDT_LOB",typeof(int));
            base.dtModel.Columns.Add("EXPDT_CARR",typeof(string));
            base.dtModel.Columns.Add("EXPDT_DATE",typeof(DateTime));
            base.dtModel.Columns.Add("EXPDT_PREM",typeof(double));
            base.dtModel.Columns.Add("EXPDT_CONT_DATE",typeof(DateTime));
            base.dtModel.Columns.Add("EXPDT_CSR",typeof(int));
            base.dtModel.Columns.Add("EXPDT_PROD",typeof(int));
            base.dtModel.Columns.Add("EXPDT_NOTES",typeof(string));
            base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
            
           
        }
        #region Database schema details
        // model for database field CUSTOMER_ID(int)
        public int CUSTOMER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
            }
        }
        // model for database field EXPDT_ID(int)
        public int EXPDT_ID
        {
            get
            {
                return base.dtModel.Rows[0]["EXPDT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXPDT_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXPDT_ID"] = value;
            }
        }
     

        // model for database field EXPDT_ID(int)
        public int APP_ID
        {
            get
            {
                return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["APP_ID"] = value;
            }
        }
     

        // model for database field EXPDT_ID(int)
        public int APP_VERSION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
            }
        }
     

        // model for database field EXPDT_LOB(int)
        public int EXPDT_LOB
        {
            get
            {
                return base.dtModel.Rows[0]["EXPDT_LOB"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXPDT_LOB"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXPDT_LOB"] = value;
            }
        }
        // model for database field EXPDT_CARR(string)
        public string EXPDT_CARR
        {
            get
            {
                return base.dtModel.Rows[0]["EXPDT_CARR"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPDT_CARR"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EXPDT_CARR"] = value;
            }
        }
        // model for database field EXPDT_DATE(DateTime)
        public DateTime EXPDT_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["EXPDT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXPDT_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXPDT_DATE"] = value;
            }
        }


        
        // model for database field EXPDT_PREM(double)
        public double EXPDT_PREM
        {
            get
            {
                return base.dtModel.Rows[0]["EXPDT_PREM"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["EXPDT_PREM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXPDT_PREM"] = value;
            }
        }
        // model for database field EXPDT_CONT_DATE(DateTime)
        public DateTime EXPDT_CONT_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["EXPDT_CONT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXPDT_CONT_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXPDT_CONT_DATE"] = value;
            }
        }
        // model for database field EXPDT_CSR(int)
        public int EXPDT_CSR
        {
            get
            {
                return base.dtModel.Rows[0]["EXPDT_CSR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXPDT_CSR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXPDT_CSR"] = value;
            }
        }
        // model for database field EXPDT_PROD(int)
        public int EXPDT_PROD
        {
            get
            {
                return base.dtModel.Rows[0]["EXPDT_PROD"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXPDT_PROD"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["EXPDT_PROD"] = value;
            }
        }
        // model for database field EXPDT_NOTES(string)
        public string EXPDT_NOTES
        {
            get
            {
                return base.dtModel.Rows[0]["EXPDT_NOTES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXPDT_NOTES"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EXPDT_NOTES"] = value;
            }
        }
        // model for database field POLICY_NUMBER(string)
        public string POLICY_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_NUMBER"] = value;
            }
        }
        
		//ARC 19 May, 2005 Commented following 5 properties which are not 
		//required here, because they are already declared in 
		//base class i.e. Cms.Model.ClsCommonModel
		#region Commented by Arun
//		// model for database field IS_ACTIVE(string)
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
