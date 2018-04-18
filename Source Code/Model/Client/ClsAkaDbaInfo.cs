/******************************************************************************************
<Author					: -	Pradeep
<Start Date				: -	26 Apr, 2005
<End Date				: -	
<Description			: - Model class for Customer AKA/DBA	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*****************************************************************************************/

using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Client
{
	/// <summary>
	/// Summary description for ClsAkaDbaInfo.
	/// </summary>
	public class ClsAkaDbaInfo : ClsCommonModel
	{
		private const string CLIENTTABLE = "CLT_CUSTOMER_AKADBA";
		
		public ClsAkaDbaInfo()
		{
			dtModel.TableName = CLIENTTABLE;
			AddColumns();							// add columns of the database table CLT_CUSTOMER_LIST
			dtModel.Rows.Add(dtModel.NewRow());	// add a blank row in the datatable
		}

		private void AddColumns()
		{
			dtModel.Columns.Add("AKADBA_ID",typeof(int));
			dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			dtModel.Columns.Add("AKADBA_TYPE",typeof(int));
			dtModel.Columns.Add("AKADBA_NAME",typeof(string));
			dtModel.Columns.Add("AKADBA_ADD",typeof(string));
			dtModel.Columns.Add("AKADBA_ADD2",typeof(string));
			dtModel.Columns.Add("AKADBA_CITY",typeof(string));
			dtModel.Columns.Add("AKADBA_STATE",typeof(int));
			dtModel.Columns.Add("AKADBA_COUNTRY",typeof(int));
			dtModel.Columns.Add("AKADBA_ZIP",typeof(string));
			dtModel.Columns.Add("AKADBA_WEBSITE",typeof(string));
			dtModel.Columns.Add("AKADBA_EMAIL",typeof(string));
			dtModel.Columns.Add("AKADBA_LEGAL_ENTITY_CODE",typeof(int));
			dtModel.Columns.Add("AKADBA_NAME_ON_FORM",typeof(string));
			dtModel.Columns.Add("AKADBA_DISP_ORDER",typeof(int));
			dtModel.Columns.Add("AKADBA_MEMO",typeof(string));
		
		
		}

		public int AKADBA_ID
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["AKADBA_ID"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_ID"] = value;
			}
		}

		public int CUSTOMER_ID
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["CUSTOMER_ID"]);
			}
			set
			{
				dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}

		public int AKADBA_TYPE
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["AKADBA_TYPE"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_TYPE"] = value;
			}
		}

		public string AKADBA_NAME
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["AKADBA_NAME"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_NAME"] = value;
			}
		}

		public string AKADBA_ADD
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["AKADBA_ADD"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_ADD"] = value;
			}
		}
		
		public string AKADBA_ADD2
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["AKADBA_ADD2"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_ADD2"] = value;
			}
		}

		public string AKADBA_CITY
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["AKADBA_CITY"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_CITY"] = value;
			}
		}

		public int AKADBA_STATE
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["AKADBA_STATE"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_STATE"] = value;
			}
		}
		
		public int AKADBA_COUNTRY
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["AKADBA_COUNTRY"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_COUNTRY"] = value;
			}
		}

		public string AKADBA_ZIP
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["AKADBA_ZIP"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_ZIP"] = value;
			}
		}

		public string AKADBA_WEBSITE
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["AKADBA_WEBSITE"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_WEBSITE"] = value;
			}
		}

		public string AKADBA_EMAIL
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["AKADBA_EMAIL"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_EMAIL"] = value;
			}
		}

		public int AKADBA_LEGAL_ENTITY_CODE
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["AKADBA_LEGAL_ENTITY_CODE"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_LEGAL_ENTITY_CODE"] = value;
			}
		}

		public string AKADBA_NAME_ON_FORM
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["AKADBA_NAME_ON_FORM"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_NAME_ON_FORM"] = value;
			}
		}

		public int AKADBA_DISP_ORDER
		{
			get
			{
				return this.GetInt(dtModel.Rows[0]["AKADBA_DISP_ORDER"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_DISP_ORDER"] = value;
			}
		}

		public string AKADBA_MEMO
		{
			get
			{
				return this.GetString(dtModel.Rows[0]["AKADBA_MEMO"]);
			}
			set
			{
				dtModel.Rows[0]["AKADBA_MEMO"] = value;
			}
		}


	}
}
