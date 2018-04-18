using System;
using System.Data;

namespace Cms.Model
{
	/// <summary>
	/// Summary description for ClsBaseModel.
	/// </summary>
	public abstract class ClsBaseModel:IModelInfo
	{
		int _TransID = 0;
        private string strCustomInfo = ""; // Added by Charles on 20-Apr-2010 for Multilingual Implementation of Transaction Log

        public static int OLD_MODEL_LANG_ID = 1;

		public int TransID
		{
			get
			{
				return _TransID;
			}
			set
			{
				_TransID = value;
			}
		}


		public ClsBaseModel()
		{
			strLabelFieldXML = "";					// string variable which will hold the label and field mapping
			dtModel = new DataTable();				// datatable which will hold  the derived class field name, field type and field value information 
		}

		public int GetInt(object o)
		{
			return o == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(o);
		}

		public string GetString(object o)
		{
			return o == DBNull.Value ? Convert.ToString(null) : Convert.ToString(o);
		}



		#region functions and properties implemented for IModelInfo interface
		/// <summary>
		/// client model object's information
		/// </summary>
		/// <returns>Customer information in datatable</returns>
		public DataTable TableInfo
		{
			get
			{
				return dtModel;
			}
			set
			{
				dtModel = value;
			}
		}

		/// <summary>
		/// an XML string which mapps the label name(for transaction log) against field_name of the client table
		/// </summary>
		public string TransactLabel
		{
			get
			{
				return strLabelFieldXML;
			}

			set
			{
				strLabelFieldXML	=	value;
			}
		}

        /// <summary>
        /// Custom Information to be maintained in Transaction LOg
        /// </summary>
        /// Added by Charles on 20-Apr-2010 for Multilingual Implementation of Transaction Log
        public string CUSTOM_INFO
        {
            get
            {
                return strCustomInfo;
            }
            set
            {
                strCustomInfo = value;
            }
        }

		protected DataTable dtModel;			// datatable to store information of derived class
		protected string strLabelFieldXML;	// string to hold information of Label and field mapping in XML format.
		#endregion
	}
	public abstract class ClsCommonModel:ClsBaseModel
	{
		public ClsCommonModel()
		{
			AddColumns();							// add columns of the database table CLT_CUSTOMER_LIST
		}

		private void AddColumns()
		{
			dtModel.Columns.Add("CREATED_BY",typeof(int));
			dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));
			dtModel.Columns["CREATED_DATETIME"].ExtendedProperties.Add("FORMAT_DATE","N");
			dtModel.Columns.Add("MODIFIED_BY",typeof(int));
            dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));
			dtModel.Columns["LAST_UPDATED_DATETIME"].ExtendedProperties.Add("FORMAT_DATE","N");
			dtModel.Columns.Add("IS_ACTIVE",typeof(string));
			
			dtModel.Columns.Add("TO_FOLLOWUP_ID",typeof(int));


		}

		#region Database schema details
		public int CREATED_BY
		{
			get
			{
				return dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(dtModel.Rows[0]["CREATED_BY"]);
			}
			set
			{
				dtModel.Rows[0]["CREATED_BY"] = value;
			}
		}

		/// <summary>
		/// model for database field CREATED_DATETIME(datetime)
		/// </summary>
		public DateTime CREATED_DATETIME
		{
			get
			{
				return dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtModel.Rows[0]["CREATED_DATETIME"]);
			}
			set
			{
				dtModel.Rows[0]["CREATED_DATETIME"] = value;
			}
		}

		/// <summary>
		/// model for database field MODIFIED_BY(int)
		/// </summary>
		public int MODIFIED_BY
		{
			get
			{
				return dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(dtModel.Rows[0]["MODIFIED_BY"]);
			}
			set
			{
				dtModel.Rows[0]["MODIFIED_BY"] = value;
			}
		}

		/// <summary>
		/// model for database field LAST_UPDATED_DATETIME(datetime)
		/// </summary>
		public DateTime LAST_UPDATED_DATETIME
		{
			get
			{
				return dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtModel.Rows[0]["LAST_UPDATED_DATETIME"]);
			}
			set
			{
				dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
			}
		}

		public string IS_ACTIVE
		{
			get
			{
				return dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? Convert.ToString(null) : dtModel.Rows[0]["IS_ACTIVE"].ToString();
			}
			set
			{
				dtModel.Rows[0]["IS_ACTIVE"] = value;
			}
		}

		
		/// <summary>
		/// model for database field TO_FOLLOWUP_ID(int)
		/// </summary>
		public int TO_FOLLOWUP_ID
		{
			get
			{
				return dtModel.Rows[0]["TO_FOLLOWUP_ID"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(dtModel.Rows[0]["TO_FOLLOWUP_ID"]);
			}
			set
			{
				dtModel.Rows[0]["TO_FOLLOWUP_ID"] = value;
			}
		}

		#endregion

	}
}
