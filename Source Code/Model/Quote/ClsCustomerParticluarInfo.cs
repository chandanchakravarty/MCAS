using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Quote
{
    public class ClsCustomerParticluarInfo : Cms.Model.ClsCommonModel 
    {
        private const string QQ_CUSTOMER_PARTICULAR = "QQ_CUSTOMER_PARTICULAR";
        public ClsCustomerParticluarInfo()
        {
            base.dtModel.TableName = "QQ_CUSTOMER_PARTICULAR";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table QOT_CUSTOMER_QUOTE_LIST
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("QUOTE_ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_CODE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_TYPE", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_FIRST_NAME", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_MIDDLE_NAME", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_LAST_NAME", typeof(string));
            base.dtModel.Columns.Add("DATE_OF_BIRTH", typeof(DateTime));
            base.dtModel.Columns.Add("GENDER", typeof(string));
            base.dtModel.Columns.Add("NATIONALITY", typeof(string));
            base.dtModel.Columns.Add("IS_HOME_EMPLOYEE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_OCCU", typeof(int));
            base.dtModel.Columns.Add("DRIVER_EXP_YEAR", typeof(int));
            base.dtModel.Columns.Add("ANY_CLAIM", typeof(string));
            base.dtModel.Columns.Add("EXIST_NCD_LESS_10", typeof(string));
            base.dtModel.Columns.Add("EXISTING_NCD", typeof(string));
            base.dtModel.Columns.Add("DEMERIT_DISCOUNT", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_AGENCY_ID", typeof(int));
            //base.dtModel.Columns.Add("IS_ACTIVE", typeof(string));
            //base.dtModel.Columns.Add("CREATED_BY", typeof(int));
            //base.dtModel.Columns.Add("CREATED_DATETIME", typeof(DateTime));
            //base.dtModel.Columns.Add("MODIFIED_BY", typeof(int));
            //base.dtModel.Columns.Add("LAST_UPDATED_DATETIME", typeof(DateTime));
            base.dtModel.Columns.Add("CUSTOMER_ADDRESS1", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_ADDRESS2", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_CITY", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_STATE", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_ZIP", typeof(string));
            base.dtModel.Columns.Add("MARITAL_STATUS", typeof(string));
            base.dtModel.Columns.Add("CUSTOMER_CONTACT_NO", typeof(string));
            base.dtModel.Columns.Add("PASSPORT_NO", typeof(string));
            base.dtModel.Columns.Add("EXISTING_INSURER", typeof(string));
            base.dtModel.Columns.Add("EXISTING_POL_NUM", typeof(string));
            base.dtModel.Columns.Add("EXIST_POL_EXP_DATE", typeof(string));
            base.dtModel.Columns.Add("VEHICLE_NO", typeof(string));
            base.dtModel.Columns.Add("DATE_OF_QUOTATION", typeof(string));
            //base.dtModel.Columns.Add("CUSTOMER_AGENCY_ID", typeof(string));
            //base.dtModel.Columns.Add("CUSTOMER_AGENCY_ID", typeof(string));


        }

        public int ID
        {
            get
            {
                return base.dtModel.Rows[0]["ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ID"] = value;
            }
        }

        public int CUSTOMER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
            }
        }

        public int QUOTE_ID
        {
            get
            {
                return base.dtModel.Rows[0]["QUOTE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["QUOTE_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["QUOTE_ID"] = value;
            }
        }

        public string CUSTOMER_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_CODE"] = value;
            }
        }

        public int CUSTOMER_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_TYPE"] = value;
            }
        }

        public string CUSTOMER_FIRST_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_FIRST_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_FIRST_NAME"] = value;
            }
        }

        public string CUSTOMER_MIDDLE_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_MIDDLE_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_MIDDLE_NAME"] = value;
            }
        }

        public string CUSTOMER_LAST_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_LAST_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_LAST_NAME"] = value;
            }
        }

        public DateTime DATE_OF_BIRTH
        {
            get
            {
                return base.dtModel.Rows[0]["DATE_OF_BIRTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_OF_BIRTH"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DATE_OF_BIRTH"] = value;
            }
        }
        public string GENDER
        {
            get
            {
                return base.dtModel.Rows[0]["GENDER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GENDER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["GENDER"] = value;
            }
        }
        public string NATIONALITY
        {
            get
            {
                return base.dtModel.Rows[0]["NATIONALITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NATIONALITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NATIONALITY"] = value;
            }
        }
        public string IS_HOME_EMPLOYEE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_HOME_EMPLOYEE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_HOME_EMPLOYEE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_HOME_EMPLOYEE"] = value;
            }
        }

        public int CUSTOMER_OCCU
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_OCCU"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_OCCU"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_OCCU"] = value;
            }
        }

        public int DRIVER_EXP_YEAR
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_EXP_YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DRIVER_EXP_YEAR"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_EXP_YEAR"] = value;
            }
        }

        public string ANY_CLAIM
        {
            get
            {
                return base.dtModel.Rows[0]["ANY_CLAIM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ANY_CLAIM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ANY_CLAIM"] = value;
            }
        }


        public string EXIST_NCD_LESS_10
        {
            get
            {
                return base.dtModel.Rows[0]["EXIST_NCD_LESS_10"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXIST_NCD_LESS_10"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EXIST_NCD_LESS_10"] = value;
            }
        }


        public string EXISTING_NCD
        {
            get
            {
                return base.dtModel.Rows[0]["EXISTING_NCD"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXISTING_NCD"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EXISTING_NCD"] = value;
            }
        }

        public string DEMERIT_DISCOUNT
        {
            get
            {
                return base.dtModel.Rows[0]["DEMERIT_DISCOUNT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DEMERIT_DISCOUNT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DEMERIT_DISCOUNT"] = value;
            }
        }

        public int CUSTOMER_AGENCY_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_AGENCY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_AGENCY_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_AGENCY_ID"] = value;
            }
        }

        public string IS_ACTIVE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_ACTIVE"] = value;
            }
        }

        public int CREATED_BY
        {
            get
            {
                return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CREATED_BY"] = value;
            }
        }

        public DateTime CREATED_DATETIME
        {
            get
            {
                return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
            }
        }

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

        public string CUSTOMER_ADDRESS1
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ADDRESS1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_ADDRESS1"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ADDRESS1"] = value;
            }
        }

        public string CUSTOMER_ADDRESS2
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ADDRESS2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_ADDRESS2"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ADDRESS2"] = value;
            }
        }

        public string CUSTOMER_CITY
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_CITY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_CITY"] = value;
            }
        }

        public string CUSTOMER_STATE
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_STATE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_STATE"] = value;
            }
        }

        public string CUSTOMER_ZIP
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_ZIP"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ZIP"] = value;
            }
        }

        public string MARITAL_STATUS
        {
            get
            {
                return base.dtModel.Rows[0]["MARITAL_STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MARITAL_STATUS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MARITAL_STATUS"] = value;
            }
        }

        public string CUSTOMER_CONTACT_NO
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_CONTACT_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_CONTACT_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_CONTACT_NO"] = value;
            }
        }

        public string PASSPORT_NO
        {
            get
            {
                return base.dtModel.Rows[0]["PASSPORT_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PASSPORT_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PASSPORT_NO"] = value;
            }
        }

        public string EXISTING_INSURER
        {
            get
            {
                return base.dtModel.Rows[0]["EXISTING_INSURER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXISTING_INSURER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EXISTING_INSURER"] = value;
            }
        }

        public string EXISTING_POL_NUM
        {
            get
            {
                return base.dtModel.Rows[0]["EXISTING_POL_NUM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXISTING_POL_NUM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EXISTING_POL_NUM"] = value;
            }
        }

        public string EXIST_POL_EXP_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["EXIST_POL_EXP_DATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXIST_POL_EXP_DATE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["EXIST_POL_EXP_DATE"] = value;
            }
        }

        public string VEHICLE_NO
        {
            get
            {
                return base.dtModel.Rows[0]["VEHICLE_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VEHICLE_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["VEHICLE_NO"] = value;
            }
        }

        public string DATE_OF_QUOTATION
        {
            get
            {
                return base.dtModel.Rows[0]["DATE_OF_QUOTATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DATE_OF_QUOTATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DATE_OF_QUOTATION"] = value;
            }
        }
    }
}
