	/******************************************************************************************
	<Author				: -   Ajit Singh chahal
	<Start Date				: -	6/6/2005 5:52:19 PM
	<End Date				: -	
	<Description				: - 	Model For Transaction Codes.
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
	namespace  Cms.Model.Maintenance.Accounting
	{
		/// <summary>
		/// Database Model for ACT_TRANSACTION_CODES.
		/// </summary>
		public class ClsTransactionCodesInfo : Cms.Model.ClsCommonModel
		{
			private const string ACT_TRANSACTION_CODES = "ACT_TRANSACTION_CODES";
			public ClsTransactionCodesInfo()
			{
				base.dtModel.TableName = "ACT_TRANSACTION_CODES";		// setting table name for data table that holds property values.
				this.AddColumns();								// add columns of the database table ACT_TRANSACTION_CODES
				base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
			}
			private void AddColumns()
			{
				base.dtModel.Columns.Add("TRAN_ID",typeof(int));
				base.dtModel.Columns.Add("CATEGOTY_CODE",typeof(string));
				base.dtModel.Columns.Add("TRAN_TYPE",typeof(string));
				base.dtModel.Columns.Add("TRAN_CODE",typeof(string));
				base.dtModel.Columns.Add("DISPLAY_DESCRIPTION",typeof(string));
				base.dtModel.Columns.Add("PRINT_DESCRIPTION",typeof(string));
				base.dtModel.Columns.Add("DEF_AMT_CALC_TYPE",typeof(string));
				base.dtModel.Columns.Add("DEF_AMT",typeof(double));
				base.dtModel.Columns.Add("AGENCY_COMM_APPLIES",typeof(string));
				base.dtModel.Columns.Add("GL_INCOME_ACC",typeof(int));
				base.dtModel.Columns.Add("IS_DEF_NEGATIVE",typeof(string));
			}
			#region Database schema details
			// model for database field TRAN_ID(int)
			public int TRAN_ID
			{
				get
				{
					return base.dtModel.Rows[0]["TRAN_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRAN_ID"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["TRAN_ID"] = value;
				}
			}
			// model for database field CATEGOTY_CODE(string)
			public string CATEGOTY_CODE
			{
				get
				{
					return base.dtModel.Rows[0]["CATEGOTY_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CATEGOTY_CODE"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["CATEGOTY_CODE"] = value;
				}
			}
			// model for database field TRAN_TYPE(string)
			public string TRAN_TYPE
			{
				get
				{
					return base.dtModel.Rows[0]["TRAN_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TRAN_TYPE"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["TRAN_TYPE"] = value;
				}
			}
			// model for database field TRAN_CODE(string)
			public string TRAN_CODE
			{
				get
				{
					return base.dtModel.Rows[0]["TRAN_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TRAN_CODE"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["TRAN_CODE"] = value;
				}
			}
			// model for database field DISPLAY_DESCRIPTION(string)
			public string DISPLAY_DESCRIPTION
			{
				get
				{
					return base.dtModel.Rows[0]["DISPLAY_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DISPLAY_DESCRIPTION"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["DISPLAY_DESCRIPTION"] = value;
				}
			}
			// model for database field PRINT_DESCRIPTION(string)
			public string PRINT_DESCRIPTION
			{
				get
				{
					return base.dtModel.Rows[0]["PRINT_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PRINT_DESCRIPTION"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["PRINT_DESCRIPTION"] = value;
				}
			}
			// model for database field DEF_AMT_CALC_TYPE(string)
			public string DEF_AMT_CALC_TYPE
			{
				get
				{
					return base.dtModel.Rows[0]["DEF_AMT_CALC_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEF_AMT_CALC_TYPE"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["DEF_AMT_CALC_TYPE"] = value;
				}
			}
			// model for database field DEF_AMT(double)
			public double DEF_AMT
			{
				get
				{
					return base.dtModel.Rows[0]["DEF_AMT"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["DEF_AMT"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["DEF_AMT"] = value;
				}
			}
			// model for database field AGENCY_COMM_APPLIES(string)
			public string AGENCY_COMM_APPLIES
			{
				get
				{
					return base.dtModel.Rows[0]["AGENCY_COMM_APPLIES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_COMM_APPLIES"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["AGENCY_COMM_APPLIES"] = value;
				}
			}
			// model for database field GL_INCOME_ACC(int)
			public int GL_INCOME_ACC
			{
				get
				{
					return base.dtModel.Rows[0]["GL_INCOME_ACC"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["GL_INCOME_ACC"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["GL_INCOME_ACC"] = value;
				}
			}
			// model for database field IS_DEF_NEGATIVE(string)
			public string IS_DEF_NEGATIVE
			{
				get
				{
					return base.dtModel.Rows[0]["IS_DEF_NEGATIVE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_DEF_NEGATIVE"].ToString();
				}
				set
				{
					base.dtModel.Rows[0]["IS_DEF_NEGATIVE"] = value;
				}
			}
			#endregion
		}
	}
