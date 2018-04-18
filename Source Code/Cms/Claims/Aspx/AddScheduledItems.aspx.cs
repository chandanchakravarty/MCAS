/******************************************************************************************
<Author					: -   Sumit Chhabra
<Start Date				: -	  29 May , 2006
<End Date				: -	
<Description			: -  Scheduled Items / Coverages for Inland Marine (Policy) at Claims
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:   
<Purpose				:  
*******************************************************************************************/ 

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Application;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// Summary description for Coverages.
	/// </summary>
	public class AddScheduledItems : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgCoverages;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;				
		protected System.Web.UI.WebControls.Label lblTitle;				
		
		
		DataSet dsCoverages = null;				
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		private string strPolicyId,strClaimID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="300_8";	

			// Put user code to initialize the page here
			if ( !Page.IsPostBack)
			{

				GetQueryStringValues();
				

				BindGrid();


			}			
		}

		
		
		private void BindGrid()
		{
			
			Cms.BusinessLayer.BLClaims.ClsScheduledItems objScheduledItems = new Cms.BusinessLayer.BLClaims.ClsScheduledItems();
				
			
			dsCoverages=objScheduledItems.GetPolInlandCoverages(Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(hidPolicyID.Value),
				Convert.ToInt32(hidPolicyVersionID.Value),
				"N"
				);

			
			
			dgCoverages.DataSource = dsCoverages.Tables[0];
			dgCoverages.DataBind();


		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			//this.dgCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);
			//this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			//this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request["CUSTOMER_ID"]!=null && Request["CUSTOMER_ID"].ToString()!="")
				hidCustomerID.Value = Request["CUSTOMER_ID"].ToString();
			else
				hidCustomerID.Value = "";

			if(Request["POLICY_ID"]!=null && Request["POLICY_ID"].ToString()!="")			
				hidPolicyID.Value = Request["POLICY_ID"].ToString();				
			else
				hidPolicyID.Value = "";

			strPolicyId = hidPolicyID.Value;

			if(Request["POLICY_VERSION_ID"]!=null && Request["POLICY_VERSION_ID"].ToString()!="")
				hidPolicyVersionID.Value = Request["POLICY_VERSION_ID"].ToString();
			else
				hidPolicyVersionID.Value = "";
			
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")
				strClaimID = Request["Claim_ID"].ToString();
			else
				strClaimID = "0";

		}


		
	}
}
