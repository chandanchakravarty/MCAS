/******************************************************************************************
<Author				: -   Nidhi
<Start Date				: -	4/25/2005 7:11:42 PM
<End Date				: -	
<Description				: - 	this is the Model class for Application General Information
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
namespace Cms.Model.Application
{
	/// <summary>
	/// Database Model for APP_LIST.
	/// </summary>
	public class ClsGeneralInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_LIST = "APP_LIST";
		
		public ClsGeneralInfo()
		{
			base.dtModel.TableName = "APP_LIST";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_LIST
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("PARENT_APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("APP_STATUS",typeof(string));
			base.dtModel.Columns.Add("APP_NUMBER",typeof(string));
			base.dtModel.Columns.Add("APP_VERSION",typeof(string));
			base.dtModel.Columns.Add("APP_TERMS",typeof(string));
			base.dtModel.Columns.Add("APP_INCEPTION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("APP_EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("APP_EXPIRATION_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("APP_LOB",typeof(string));
			base.dtModel.Columns.Add("APP_SUBLOB",typeof(string));
			base.dtModel.Columns.Add("CSR",typeof(int));
			base.dtModel.Columns.Add("Producer",typeof(int));
			base.dtModel.Columns.Add("APP_AGENCY_ID",typeof(int));
			base.dtModel.Columns.Add("UNDERWRITER",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_CODE",typeof(string));
			base.dtModel.Columns.Add("COUNTRY_ID",typeof(int));

			//for billing
			base.dtModel.Columns.Add("DIV_ID",typeof(int));
			base.dtModel.Columns.Add("DEPT_ID",typeof(int));
			base.dtModel.Columns.Add("PC_ID",typeof(int));
			base.dtModel.Columns.Add("BILL_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("COMPLETE_APP",typeof(string));
			base.dtModel.Columns.Add("PROPRTY_INSP_CREDIT",typeof(string));
			base.dtModel.Columns.Add("INSTALL_PLAN_ID",typeof(int));
			base.dtModel.Columns.Add("CHARGE_OFF_PRMIUM",typeof(string));
			
			base.dtModel.Columns.Add("RECEIVED_PRMIUM",typeof(double));
			base.dtModel.Columns.Add("PROXY_SIGN_OBTAINED",typeof(int));
			base.dtModel.Columns.Add("POLICY_TYPE",typeof(int));
			base.dtModel.Columns.Add("POLICY_TYPE_CODE",typeof(string));

			//base.dtModel.Columns.Add("YEAR_AT_CURR_RESI",typeof(double));
			//base.dtModel.Columns.Add("YEARS_AT_PREV_ADD",typeof(double));
			//base.dtModel.Columns.Add("YEAR_AT_CURR_RESI",typeof(double));
			//Nov,07,2005:Sumit Chhabra: Dataype for YEAR_AT_CURR_RESI being changed from double to int
			base.dtModel.Columns.Add("YEAR_AT_CURR_RESI",typeof(int));
			base.dtModel.Columns.Add("YEARS_AT_PREV_ADD",typeof(string));
			base.dtModel.Columns.Add("PIC_OF_LOC",typeof(string));
			base.dtModel.Columns.Add("IS_HOME_EMP",typeof(bool));
			base.dtModel.Columns.Add("DOWN_PAY_MODE",typeof(int));
			base.dtModel.Columns.Add("QQ_ID",typeof(int));

		}
		
		#region Database schema details
		// model for database field YEARS_AT_PREV_ADD(string)
		public string YEARS_AT_PREV_ADD
		{
			get
			{
				
				return base.dtModel.Rows[0]["YEARS_AT_PREV_ADD"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["YEARS_AT_PREV_ADD"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["YEARS_AT_PREV_ADD"] = value;
			}
		}
		// model for database field YEAR_AT_CURR_RESI(double)
		public int YEAR_AT_CURR_RESI
		{
			get
			{
				return base.dtModel.Rows[0]["YEAR_AT_CURR_RESI"] == DBNull.Value ? int.MinValue : int.Parse(base.dtModel.Rows[0]["YEAR_AT_CURR_RESI"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEAR_AT_CURR_RESI"] = value;
			}
		}		
		
		// model for database field PROXY_SIGN_OBTAINED(int)
		public int PROXY_SIGN_OBTAINED
		{
			get
			{
				return base.dtModel.Rows[0]["PROXY_SIGN_OBTAINED"] == DBNull.Value ? int.MinValue : int.Parse(base.dtModel.Rows[0]["PROXY_SIGN_OBTAINED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PROXY_SIGN_OBTAINED"] = value;
			}
		}
		// model for database field CHARGE_OFF_PRMIUM(double)
		public string CHARGE_OFF_PRMIUM
		{
			get
			{
				
				return base.dtModel.Rows[0]["CHARGE_OFF_PRMIUM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHARGE_OFF_PRMIUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CHARGE_OFF_PRMIUM"] = value;
			}
		}
		// model for database field RECEIVED_PRMIUM(double)
		public double RECEIVED_PRMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["RECEIVED_PRMIUM"] == DBNull.Value ? double.MinValue : double.Parse(base.dtModel.Rows[0]["RECEIVED_PRMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECEIVED_PRMIUM"] = value;
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
		// model for database field APP_VERSION_ID(int)
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
		// model for database field PARENT_APP_VERSION_ID(int)
		public int PARENT_APP_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PARENT_APP_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PARENT_APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARENT_APP_VERSION_ID"] = value;
			}
		}

		public string STATE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STATE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_CODE"] = value;
			}
		}
		
		// model for database field PIC_OF_LOC(string)
		public string PIC_OF_LOC
		{
			get
			{
				return base.dtModel.Rows[0]["PIC_OF_LOC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PIC_OF_LOC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PIC_OF_LOC"] = value;
			}
		}

		// model for database field APP_STATUS(string)
		public string APP_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["APP_STATUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APP_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_STATUS"] = value;
			}
		}
		// model for database field APP_NUMBER(string)
		public string APP_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["APP_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APP_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_NUMBER"] = value;
			}
		}
		// model for database field APP_VERSION(string)
		public string APP_VERSION
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VERSION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APP_VERSION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION"] = value;
			}
		}
		// model for database field APP_TERMS(string)
		public string APP_TERMS
		{
			get
			{
				return base.dtModel.Rows[0]["APP_TERMS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APP_TERMS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_TERMS"] = value;
			}
		}
		// model for database field APP_INCEPTION_DATE(DateTime)
		public DateTime APP_INCEPTION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["APP_INCEPTION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["APP_INCEPTION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_INCEPTION_DATE"] = value;
			}
		}
		// model for database field APP_EFFECTIVE_DATE(DateTime)
		public DateTime APP_EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["APP_EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_EFFECTIVE_DATE"] = value;
			}
		}
		// model for database field APP_EXPIRATION_DATE(DateTime)
		public DateTime APP_EXPIRATION_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["APP_EXPIRATION_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["APP_EXPIRATION_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_EXPIRATION_DATE"] = value;
			}
		}
		// model for database field APP_LOB(string)
		public string APP_LOB
		{
			get
			{
				return base.dtModel.Rows[0]["APP_LOB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APP_LOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_LOB"] = value;
			}
		}
		// model for database field APP_SUBLOB(string)
		public string APP_SUBLOB
		{
			get
			{
				return base.dtModel.Rows[0]["APP_SUBLOB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APP_SUBLOB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APP_SUBLOB"] = value;
			}
		}
		// model for database field CSR(int)
		public int CSR
		{
			get
			{
				return base.dtModel.Rows[0]["CSR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CSR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CSR"] = value;
			}
		}
		// model for database field Producer(int)
		public int PRODUCER
		{
			get
			{
				return base.dtModel.Rows[0]["PRODUCER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PRODUCER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRODUCER"] = value;
			}
		}
		// model for database field APP_AGENCY_ID(int)
		public int APP_AGENCY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_AGENCY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_AGENCY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_AGENCY_ID"] = value;
			}
		}

		// model for database field UNDERWRITER(int)
		public int UNDERWRITER
		{
			get
			{
				return base.dtModel.Rows[0]["UNDERWRITER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["UNDERWRITER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UNDERWRITER"] = value;
			}
		}
		// model for database field STATE_ID(int)
		public int STATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STATE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}
		// model for database field COUNTRY_ID(int)
		public int COUNTRY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COUNTRY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY_ID"] = value;
			}
		}


		// model for database field DIV_ID(int)
		public int DIV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DIV_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIV_ID"] = value;
			}
		}
		// model for database field DEPT_ID(int)
		public int DEPT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DEPT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DEPT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPT_ID"] = value;
			}
		}
		// model for database field PC_ID(int)
		public int PC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PC_ID"] = value;
			}
		}
		// model for database field BILL_TYPE(string)
		public int BILL_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["BILL_TYPE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(base.dtModel.Rows[0]["BILL_TYPE_ID"]);
			}
			set
			{
				base.dtModel.Rows[0]["BILL_TYPE_ID"] = value;
			}
		}
		// model for database field COMPLETE_APP(string)
		public string COMPLETE_APP
		{
			get
			{
				return base.dtModel.Rows[0]["COMPLETE_APP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COMPLETE_APP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COMPLETE_APP"] = value;
			}
		}
		// model for database field PROPRTY_INSP_CREDIT(string)
		public string PROPRTY_INSP_CREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["PROPRTY_INSP_CREDIT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROPRTY_INSP_CREDIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROPRTY_INSP_CREDIT"] = value;
			}
		}

		
			// model for database field INSTALL_PLAN_ID(int)
			public int INSTALL_PLAN_ID
			{
				get
				{
					return base.dtModel.Rows[0]["INSTALL_PLAN_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INSTALL_PLAN_ID"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["INSTALL_PLAN_ID"] = value;
				}
			}

		public int POLICY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TYPE"] = value;
			}
		}
		
		// model for database field CHARGE_OFF_PRMIUM(double)
		public string POLICY_TYPE_CODE
		{
			get
			{
				
				return base.dtModel.Rows[0]["POLICY_TYPE_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_TYPE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_TYPE_CODE"] = value;
			}
		}

		public bool IS_HOME_EMP
		{
			get
			{
				
				return base.dtModel.Rows[0]["IS_HOME_EMP"] == DBNull.Value ? Convert.ToBoolean(null) : bool.Parse(base.dtModel.Rows[0]["IS_HOME_EMP"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IS_HOME_EMP"] = value;
			}
		}
	// model for database field DOWN_PAY_MODE(int)
		public int DOWN_PAY_MODE
		{
			get
			{
				return base.dtModel.Rows[0]["DOWN_PAY_MODE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DOWN_PAY_MODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DOWN_PAY_MODE"] = value;
			}
		}
		
		public int QQ_ID
		{
			get
			{
				return base.dtModel.Rows[0]["QQ_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["QQ_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["QQ_ID"] = value;
			}
		}
		

		//ARC 19 May, 2005 Commented following 5 properties which are not 
		//required here, because they are already declared in 
		//base class i.e. Cms.Model.ClsCommonModel
		#region Commented by Arun
//		// model for database field IS_ACTIVE(string)
//		public string IS_ACTIVE
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
//			}
//			set
//			{
//				base.dtModel.Rows[0]["IS_ACTIVE"] = value;
//			}
//		}
//		// model for database field CREATED_BY(int)
//		public int CREATED_BY
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["CREATED_BY"] = value;
//			}
//		}
//		// model for database field CREATED_DATETIME(DateTime)
//		public DateTime CREATED_DATETIME
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
//			}
//		}
//		// model for database field MODIFIED_BY(int)
//		public int MODIFIED_BY
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["MODIFIED_BY"] = value;
//			}
//		}
//		// model for database field LAST_UPDATED_DATETIME(DateTime)
//		public DateTime LAST_UPDATED_DATETIME
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
//			}
//			set
//			{
//				base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
//			}
//		}
		#endregion Commented by Arun
		#endregion
	}
}
