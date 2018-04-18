/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	4/20/2006 4:18:48 PM
<End Date				: -	
<Description				: - 	Model Class for Types Details 
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
using Cms.BusinessLayer.BlCommon;
using System.Collections.Generic;
using Cms.Model.Maintenance;
namespace Cms.Model.Maintenance.Claims
    
{
	/// <summary>
	/// Database Model for CLM_TYPE_DETAIL.
	/// </summary>
	public class ClsDefaultValuesInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_TYPE_DETAIL = "CLM_TYPE_DETAIL";
		public ClsDefaultValuesInfo()
		{
			base.dtModel.TableName = "CLM_TYPE_DETAIL";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_TYPE_DETAIL
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("DETAIL_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("DETAIL_TYPE_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("TRANSACTION_CODE",typeof(int));
			base.dtModel.Columns.Add("TRANSACTION_CATEGORY",typeof(string));
			base.dtModel.Columns.Add("IS_SYSTEM_GENERATED",typeof(string));
			base.dtModel.Columns.Add("SelectedDebitLedgers",typeof(string));
			base.dtModel.Columns.Add("SelectedCreditLedgers",typeof(string));

            // Added by Agniswar for Singapore Implementation
            base.dtModel.Columns.Add("LOSS_TYPE_CODE", typeof(string));
            base.dtModel.Columns.Add("LOSS_DEPARTMENT", typeof(string));
            base.dtModel.Columns.Add("LOSS_EXTRA_COVER", typeof(string));
		}
		#region Database schema details
		// model for database field IS_SYSTEM_GENERATED(char)
		public string IS_SYSTEM_GENERATED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_SYSTEM_GENERATED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_SYSTEM_GENERATED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_SYSTEM_GENERATED"] = value;
			}
		}

		// model for database field DETAIL_TYPE_ID(int)
		public int DETAIL_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DETAIL_TYPE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DETAIL_TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DETAIL_TYPE_ID"] = value;
			}
		}
		// model for database field TYPE_ID(int)
		public int TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TYPE_ID"] = value;
			}
		}
		// model for database field DETAIL_TYPE_DESCRIPTION(string)
		public string DETAIL_TYPE_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["DETAIL_TYPE_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DETAIL_TYPE_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DETAIL_TYPE_DESCRIPTION"] = value;
			}
		}
		// model for database field TRANSACTION_CODE(int)
		public int TRANSACTION_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSACTION_CODE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TRANSACTION_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRANSACTION_CODE"] = value;
			}
		}

		// model for database field TRANSACTION_CATEGORY(string)
		public string TRANSACTION_CATEGORY
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSACTION_CATEGORY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TRANSACTION_CATEGORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRANSACTION_CATEGORY"] = value;
			}
		}

		// model for database field SelectedDebitLedgers(string)
		public string SelectedDebitLedgers
		{
			get
			{
				return base.dtModel.Rows[0]["SelectedDebitLedgers"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SelectedDebitLedgers"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SelectedDebitLedgers"] = value;
			}
		}

		// model for database field SelectedCreditLedgers(string)
		public string SelectedCreditLedgers
		{
			get
			{
				return base.dtModel.Rows[0]["SelectedCreditLedgers"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SelectedCreditLedgers"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SelectedCreditLedgers"] = value;
			}
		}
		
        public string LOSS_TYPE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_TYPE_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOSS_TYPE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_TYPE_CODE"] = value;
			}
		}
		
        public string LOSS_DEPARTMENT
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_DEPARTMENT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOSS_DEPARTMENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_DEPARTMENT"] = value;
			}
		}
		

        public string LOSS_EXTRA_COVER
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_EXTRA_COVER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOSS_EXTRA_COVER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_EXTRA_COVER"] = value;
			}
		}
		
       #endregion

        public static void ActivateDeactivateDefaultValues(int p, string is_Active, ClsDefaultValuesInfo objDefaultValuesInfo)
        {
            throw new NotImplementedException();
        }





        

        public int ActivateDeactivateDefaultValues(ClsDefaultValuesInfo objDefaultValuesInfo)
        {
            throw new NotImplementedException();
        }

       
    }
}
