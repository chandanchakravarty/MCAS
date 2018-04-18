/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date			: -	 05/05/2006
<End Date			: -	 
<Description		: -  Class to display the Claims tabs named Claims Notifcation, etc.
<Review Date		: - 
<Reviewed By		: - 	
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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class ActivityTab : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;		
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;		
		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;			

		protected string strCustomerId,strPolicyID,strPolicyVersionID,strLOB_ID,strAddNew,strNew_Record;
		public string strHOMEOWNER,strRECR_VEH,strIN_MARINE,strLOSS_DATE;
		protected System.Web.UI.HtmlControls.HtmlForm PolicyInformation;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAUTHORIZE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;		
		
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="309";	
			GetQueryStringValues();
			SetClaimTop();		
			SetTabs();
		}
		private void SetTabs()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
			string url;
			string title;
			url = "ActivityIndex.aspx?&CLAIM_ID=" + hidCLAIM_ID.Value +  "&AUTHORIZE=" + hidAUTHORIZE.Value +  "&ACTIVITY_ID=" + hidACTIVITY_ID.Value +  "&";

            title = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1302");// "Activity Details";
			TabCtl.TabTitles = title;
			TabCtl.TabURLs = url;

			url = "Reports/PaidClaimsByCoverageReport.aspx?&CLAIM_ID=" + hidCLAIM_ID.Value +  "&";

            title = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1303");//"Coverage Report";
			TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
			TabCtl.TabTitles = TabCtl.TabTitles + "," + title;

			
			
		}
		private void GetQueryStringValues()
		{
			if(Request.QueryString["ACTIVITY_ID"]!=null && Request.QueryString["ACTIVITY_ID"].ToString()!="")			
				hidACTIVITY_ID.Value = Request.QueryString["ACTIVITY_ID"].ToString(); //Take the activity_id value to edit the record							
			else
				hidACTIVITY_ID.Value = ""; //When no activity_id is being specified, let the index page open and leave as it is

			if(Request.QueryString["AUTHORIZE"]!=null && Request.QueryString["AUTHORIZE"].ToString()!="")			
				hidAUTHORIZE.Value = Request.QueryString["AUTHORIZE"].ToString(); 
			else
				hidAUTHORIZE.Value = "0"; 
		}
		
		private void SetClaimTop()
		{
			strCustomerId = GetCustomerID();
			strPolicyID = GetPolicyID();
			strPolicyVersionID = GetPolicyVersionID();
			hidCLAIM_ID.Value = GetClaimID();
			strLOB_ID = GetLOBID();

			if(strCustomerId!=null && strCustomerId!="")
			{
				cltClaimTop.CustomerID = int.Parse(strCustomerId);
			}			

			if(strPolicyID!=null && strPolicyID!="")
			{
				cltClaimTop.PolicyID = int.Parse(strPolicyID);
			}

			if(strPolicyVersionID!=null && strPolicyVersionID!="")
			{
				cltClaimTop.PolicyVersionID = int.Parse(strPolicyVersionID);
			}
			if(hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
			{
				cltClaimTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
			}
			if(strLOB_ID!=null && strLOB_ID!="")
			{
				cltClaimTop.LobID = int.Parse(strLOB_ID);
			}
        
			cltClaimTop.ShowHeaderBand ="Claim";

			cltClaimTop.Visible = true;        
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
			//this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
		
	}
}
