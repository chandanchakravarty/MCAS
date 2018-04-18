/******************************************************************************************
<Author					: -  Sumit Chhabra
<Start Date				: -	5/3/2006 3:50:55 PM
<End Date				: -	
<Description				: - 	Model Class for CLM_LIABILITY_TYPE
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_LIABILITY_TYPE.
	/// </summary>
	public class ClsLiabilityTypeInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_LIABILITY_TYPE = "CLM_LIABILITY_TYPE";
		public ClsLiabilityTypeInfo()
		{
			base.dtModel.TableName = "CLM_LIABILITY_TYPE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_LIABILITY_TYPE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("LIABILITY_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("PREMISES_INSURED",typeof(int));
			base.dtModel.Columns.Add("OTHER_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("TYPE_OF_PREMISES",typeof(string));

			base.dtModel.Columns.Add("AUTHORITY_CONTACTED",typeof(string));
			base.dtModel.Columns.Add("REPORT_NUMBER",typeof(string));
			base.dtModel.Columns.Add("VIOLATIONS",typeof(string));
			
			base.dtModel.Columns.Add("LOSS_LOCATION",typeof(string));
			base.dtModel.Columns.Add("ESTIMATE_AMOUNT",typeof(double));	
			
		}
		#region Database schema details
		// model for database field LIABILITY_TYPE_ID(int)
		public int LIABILITY_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LIABILITY_TYPE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIABILITY_TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIABILITY_TYPE_ID"] = value;
			}
		}
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field TYPE_OF_PREMISES(string)
		public string TYPE_OF_PREMISES
		{
			get
			{
				return base.dtModel.Rows[0]["TYPE_OF_PREMISES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TYPE_OF_PREMISES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TYPE_OF_PREMISES"] = value;
			}
		}
		
		// model for database field PREMISES_INSURED(int)
		public int PREMISES_INSURED
		{
			get
			{
				return base.dtModel.Rows[0]["PREMISES_INSURED"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PREMISES_INSURED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PREMISES_INSURED"] = value;
			}
		}
		
		// model for database field OTHER_DESCRIPTION(string)
		public string OTHER_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OTHER_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_DESCRIPTION"] = value;
			}
		}
		#endregion
	}
}
