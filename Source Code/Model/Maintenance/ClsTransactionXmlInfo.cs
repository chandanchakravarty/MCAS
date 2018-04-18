/******************************************************************************************
<Author : - Mohit Gupta
<Start Date : - 5/5/2005 3:38:47 PM
<End Date : - 
<Description : - Modal For Transaction Entity.
<Review Date : - 
<Reviewed By : - 
Modification History
<Modified Date : - 
<Modified By : - 
<Purpose : - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_TRANSACTION_XML.
	/// </summary>
	public class ClsTransactionXmlInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_TRANSACTION_XML = "MNT_TRANSACTION_XML";
		public ClsTransactionXmlInfo()
		{
			base.dtModel.TableName = "MNT_TRANSACTION_XML";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_TRANSACTION_XML
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
			//
			// TODO: Add constructor logic here
			//
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("TRANS_ID",typeof(int));
			base.dtModel.Columns.Add("TRANS_DETAILS",typeof(string));
			base.dtModel.Columns.Add("IS_VALIDXML",typeof(char));			
		}
		
		#region Database schema details
		// model for database field TRANS_ID(int)
		public int TRANS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["TRANS_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TRANS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRANS_ID"] = value;
			}
		}
		// model for database field TRANS_DETAILS(string)
		public string TRANS_DETAILS
		{
			get
			{
				return base.dtModel.Rows[0]["TRANS_DETAILS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TRANS_DETAILS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRANS_DETAILS"] = value;
			}
		}
		// model for database field IS_VALIDXML(char)
		public string IS_VALIDXML
		{
			get
			{
				return base.dtModel.Rows[0]["IS_VALIDXML"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_VALIDXML"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_VALIDXML"] = value;
			}
		}
		#endregion
	}
}
