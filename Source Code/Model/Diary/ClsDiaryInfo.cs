/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date			: -	4/21/2005 10:56:10 AM
<End Date			: -	
<Description		: - 	
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - June 27, 2005
<Modified By		: - Anshuman
<Purpose			: - Adding fields customer_id, app_id, policy_id etc.
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Diary
{
    /// <summary>
    /// Database Model for TODOLIST.
    /// </summary>
    public class TodolistInfo : Cms.Model.ClsCommonModel
    {
        private const string TODOLIST = "TODOLIST";
        public TodolistInfo()
        {
            base.dtModel.TableName = "TODOLIST";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table TODOLIST
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("LISTID",typeof(long));
            base.dtModel.Columns.Add("RECBYSYSTEM",typeof(string));
            base.dtModel.Columns.Add("RECDATE",typeof(DateTime));
            base.dtModel.Columns.Add("FOLLOWUPDATE",typeof(DateTime));
            base.dtModel.Columns.Add("LISTTYPEID",typeof(long));
            //base.dtModel.Columns.Add("POLICYCLIENTID",typeof(long));
            //base.dtModel.Columns.Add("POLICYID",typeof(long));
            //base.dtModel.Columns.Add("POLICYVERSION",typeof(long));
            //base.dtModel.Columns.Add("POLICYCARRIERID",typeof(long));
            base.dtModel.Columns.Add("POLICYBROKERID",typeof(long));
            base.dtModel.Columns.Add("SUBJECTLINE",typeof(string));
            base.dtModel.Columns.Add("LISTOPEN",typeof(string));
            base.dtModel.Columns.Add("SYSTEMFOLLOWUPID",typeof(long));
            base.dtModel.Columns.Add("PRIORITY",typeof(string));
            base.dtModel.Columns.Add("TOUSERID",typeof(long));
            base.dtModel.Columns.Add("FROMUSERID",typeof(long));
            base.dtModel.Columns.Add("STARTTIME",typeof(DateTime));
            base.dtModel.Columns.Add("ENDTIME",typeof(DateTime));
            base.dtModel.Columns.Add("NOTE",typeof(string));
            base.dtModel.Columns.Add("PROPOSALVERSION",typeof(long));
            base.dtModel.Columns.Add("QUOTEID",typeof(long));
            base.dtModel.Columns.Add("CLAIMID",typeof(long));
            base.dtModel.Columns.Add("CLAIMMOVEMENTID",typeof(long));
            base.dtModel.Columns.Add("TOENTITYID",typeof(long));
            base.dtModel.Columns.Add("FROMENTITYID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_NAME",typeof(string));
			base.dtModel.Columns.Add("APPLICATION_NUMBER",typeof(string));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("RULES_VERIFIED",typeof(int));

			base.dtModel.Columns.Add("MODULE_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("USERGROUP_ID",typeof(string));
			base.dtModel.Columns.Add("USERLIST_ID",typeof(string));
			base.dtModel.Columns.Add("FOLLOW_UP",typeof(int));
			base.dtModel.Columns.Add("PROCESS_ROW_ID",typeof(int));
			
			base.dtModel.Columns.Add("STARTTIMEONLY",typeof(string));
			base.dtModel.Columns.Add("ENDTIMEONLY",typeof(string));
			base.dtModel.Columns.Add("RECORDED_BY",typeof(int));
			

        }
        #region Database schema details
        // model for database field LISTID(long)
        public long LISTID
        {
            get
            {
                return base.dtModel.Rows[0]["LISTID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["LISTID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LISTID"] = value;
            }
        }
        // model for database field RECBYSYSTEM(string)
        public string RECBYSYSTEM
        {
            get
            {
                return base.dtModel.Rows[0]["RECBYSYSTEM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RECBYSYSTEM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["RECBYSYSTEM"] = value;
            }
        }
        // model for database field RECDATE(DateTime)
        public DateTime RECDATE
        {
            get
            {
                return base.dtModel.Rows[0]["RECDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["RECDATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["RECDATE"] = value;
            }
        }
        // model for database field FOLLOWUPDATE(DateTime)
        public DateTime FOLLOWUPDATE
        {
            get
            {
                return base.dtModel.Rows[0]["FOLLOWUPDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["FOLLOWUPDATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FOLLOWUPDATE"] = value;
            }
        }
        // model for database field LISTTYPEID(long)
        public long LISTTYPEID
        {
            get
            {
                return base.dtModel.Rows[0]["LISTTYPEID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["LISTTYPEID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LISTTYPEID"] = value;
            }
        }
//        // model for database field POLICYCLIENTID(long)
//        public long POLICYCLIENTID
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["POLICYCLIENTID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["POLICYCLIENTID"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["POLICYCLIENTID"] = value;
//            }
//        }
//        // model for database field POLICYID(long)
//        public long POLICYID
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["POLICYID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["POLICYID"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["POLICYID"] = value;
//            }
//        }
//        // model for database field POLICYVERSION(long)
//        public long POLICYVERSION
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["POLICYVERSION"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["POLICYVERSION"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["POLICYVERSION"] = value;
//            }
//        }
//        // model for database field POLICYCARRIERID(long)
//        public long POLICYCARRIERID
//        {
//            get
//            {
//                return base.dtModel.Rows[0]["POLICYCARRIERID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["POLICYCARRIERID"].ToString());
//            }
//            set
//            {
//                base.dtModel.Rows[0]["POLICYCARRIERID"] = value;
//            }
//        }
        // model for database field POLICYBROKERID(long)
        public long POLICYBROKERID
        {
            get
            {
                return base.dtModel.Rows[0]["POLICYBROKERID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["POLICYBROKERID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICYBROKERID"] = value;
            }
        }
        // model for database field SUBJECTLINE(string)
        public string SUBJECTLINE
        {
            get
            {
                return base.dtModel.Rows[0]["SUBJECTLINE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUBJECTLINE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUBJECTLINE"] = value;
            }
        }
        // model for database field LISTOPEN(string)
        public string LISTOPEN
        {
            get
            {
                return base.dtModel.Rows[0]["LISTOPEN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LISTOPEN"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LISTOPEN"] = value;
            }
        }
        // model for database field SYSTEMFOLLOWUPID(long)
        public long SYSTEMFOLLOWUPID
        {
            get
            {
                return base.dtModel.Rows[0]["SYSTEMFOLLOWUPID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["SYSTEMFOLLOWUPID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["SYSTEMFOLLOWUPID"] = value;
            }
        }
        // model for database field PRIORITY(string)
        public string PRIORITY
        {
            get
            {
                return base.dtModel.Rows[0]["PRIORITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PRIORITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PRIORITY"] = value;
            }
        }
        // model for database field TOUSERID(long)
        public long TOUSERID
        {
            get
            {
                return base.dtModel.Rows[0]["TOUSERID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["TOUSERID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TOUSERID"] = value;
            }
        }
        // model for database field FROMUSERID(long)
        public long FROMUSERID
        {
            get
            {
                return base.dtModel.Rows[0]["FROMUSERID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["FROMUSERID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FROMUSERID"] = value;
            }
        }
        // model for database field STARTTIME(DateTime)
        public DateTime STARTTIME
        {
            get
            {
                return base.dtModel.Rows[0]["STARTTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["STARTTIME"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["STARTTIME"] = value;
            }
        }
        // model for database field ENDTIME(DateTime)
        public DateTime ENDTIME
        {
            get
            {
                return base.dtModel.Rows[0]["ENDTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["ENDTIME"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ENDTIME"] = value;
            }
        }
        // model for database field NOTE(string)
        public string NOTE
        {
            get
            {
                return base.dtModel.Rows[0]["NOTE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NOTE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NOTE"] = value;
            }
        }
        // model for database field PROPOSALVERSION(long)
        public long PROPOSALVERSION
        {
            get
            {
                return base.dtModel.Rows[0]["PROPOSALVERSION"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["PROPOSALVERSION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["PROPOSALVERSION"] = value;
            }
        }
        // model for database field QUOTEID(long)
        public long QUOTEID
        {
            get
            {
                return base.dtModel.Rows[0]["QUOTEID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["QUOTEID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["QUOTEID"] = value;
            }
        }
        // model for database field CLAIMID(long)
        public long CLAIMID
        {
            get
            {
                return base.dtModel.Rows[0]["CLAIMID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["CLAIMID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CLAIMID"] = value;
            }
        }
        // model for database field CLAIMMOVEMENTID(long)
        public long CLAIMMOVEMENTID
        {
            get
            {
                return base.dtModel.Rows[0]["CLAIMMOVEMENTID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["CLAIMMOVEMENTID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CLAIMMOVEMENTID"] = value;
            }
        }
        // model for database field TOENTITYID(long)
        public long TOENTITYID
        {
            get
            {
                return base.dtModel.Rows[0]["TOENTITYID"] == DBNull.Value ? Convert.ToInt32(null) : long.Parse(base.dtModel.Rows[0]["TOENTITYID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TOENTITYID"] = value;
            }
        }
        // model for database field FROMENTITYID(int)
        public int FROMENTITYID
        {
            get
            {
                return base.dtModel.Rows[0]["FROMENTITYID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FROMENTITYID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FROMENTITYID"] = value;
            }
        }
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
		// text field of CUSTOMER_ID(int)
		public string CUSTOMER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CUSTOMER_NAME"].ToString();//Done for Itrack Issue 6548 on 22 Oct 09
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_NAME"] = value;
			}
		}
		// model for database field APP_ID(int)
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
		// model for database field APP_VERSION_ID(smallint)
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
		// model for database field POLICY_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_ID"] = value;
			}
		}
		// model for database field POLICY_VERSION_ID(smallint)
		public int POLICY_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// text field of APPLICATION_ID(int)
		public string APPLICATION_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["APPLICATION_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APPLICATION_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APPLICATION_NUMBER"] = value;
			}
		}
		// text field of CUSTOMER_ID(int)
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
		// model for database field RULES_VERIFIED(int)
		public int RULES_VERIFIED
		{
			get
			{
				return base.dtModel.Rows[0]["RULES_VERIFIED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RULES_VERIFIED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RULES_VERIFIED"] = value;
			}
		}

		public int MODULE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["MODULE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODULE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODULE_ID"] = value;
			}
		}

		public int LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}

		public int FOLLOW_UP
		{
			get
			{
				return base.dtModel.Rows[0]["FOLLOW_UP"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FOLLOW_UP"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FOLLOW_UP"] = value;
			}
		}


		public string USERGROUP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USERGROUP_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USERGROUP_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USERGROUP_ID"] = value;
			}
		}

		public string USERLIST_ID
		{
			get
			{
				return base.dtModel.Rows[0]["USERLIST_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USERLIST_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USERLIST_ID"] = value;
			}
		}

		public int PROCESS_ROW_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PROCESS_ROW_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PROCESS_ROW_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROCESS_ROW_ID"] = value;
			}
		}

		public string STARTTIMEONLY
		{
			get
			{
				return base.dtModel.Rows[0]["STARTTIMEONLY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STARTTIMEONLY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STARTTIMEONLY"] = value;
			}
		}

		public string ENDTIMEONLY
		{
			get
			{
				return base.dtModel.Rows[0]["ENDTIMEONLY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ENDTIMEONLY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENDTIMEONLY"] = value;
			}
		}
		// model for database field RECORDED_BY(int)
		public int RECORDED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["RECORDED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RECORDED_BY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECORDED_BY"] = value;
			}
		}
        #endregion
    }
}
