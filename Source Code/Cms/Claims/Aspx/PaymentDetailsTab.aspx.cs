
/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	06/22/2006
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
using Cms.CmsWeb.Controls;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class PaymentDetailsTab : Cms.Claims.ClaimBase
	{
		
		#region Local form variables
		public string strCalledFrom =  "";
		//creating resource manager object (used for reading field and label mapping)		
		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;			
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected Cms.CmsWeb.WebControls.ClaimActivityTop cltClaimActivityTop;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;	
		protected Cms.CmsWeb.Controls.CmsButton btnGoBack;
		protected string ActivityClientID,ActivityTotalPaymentClientID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_PAYMENT;
		#endregion



		
		#region Original PageLoad
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="308";	
			btnGoBack.CmsButtonClass		=	CmsButtonType.Read;
			btnGoBack.PermissionString		=	gstrSecurityXML;			
			if(!IsPostBack)
			{
				btnGoBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");				
				GetQueryStringValues();
			}
			SetTabs();
			SetClaimTop();
			SetClaimActivityTop();
			ActivityClientID = cltClaimActivityTop.PanelClientID;			
			ActivityTotalPaymentClientID = cltClaimActivityTop.PanelPaymentClientID;
		}
		#endregion


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void SetClaimTop()
		{
			if(GetCustomerID()!=string.Empty)
				cltClaimTop.CustomerID = int.Parse(GetCustomerID());
			if(GetPolicyID()!=string.Empty)
				cltClaimTop.PolicyID = int.Parse(GetPolicyID());
			if(GetPolicyVersionID()!=string.Empty)
				cltClaimTop.PolicyVersionID = int.Parse(GetPolicyVersionID());
			if(hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
				cltClaimTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
			if(GetLOBID()!=string.Empty)
				cltClaimTop.LobID = int.Parse(GetLOBID());        

			cltClaimTop.ShowHeaderBand ="Claim";
			cltClaimTop.Visible = true;        
		}

		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")			
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();				
			else
				hidCLAIM_ID.Value = "0";

			if(Request["ACTIVITY_ID"]!=null && Request["ACTIVITY_ID"].ToString()!="")
				hidACTIVITY_ID.Value = Request["ACTIVITY_ID"].ToString();
			else
				hidACTIVITY_ID.Value = "0";

			if(Request["ACTION_ON_PAYMENT"]!=null && Request["ACTION_ON_PAYMENT"].ToString()!="")
				hidACTION_ON_PAYMENT.Value = Request["ACTION_ON_PAYMENT"].ToString();
			else
				hidACTION_ON_PAYMENT.Value = "0";
		}

		private void SetTabs()
		{
			string url;					
			url = "PaymentDetails.aspx?CLAIM_ID=" + hidCLAIM_ID.Value + "&ACTIVITY_ID=" + hidACTIVITY_ID.Value + "&ACTION_ON_PAYMENT=" + hidACTION_ON_PAYMENT.Value + "&";
			TabCtl.TabURLs = url;
			/*url = "PayeeIndex.aspx?CLAIM_ID=" + hidCLAIM_ID.Value + "&ACTIVITY_ID=" + hidACTIVITY_ID.Value + "&CALLED_FROM=" + CALLED_FROM_PAYMENT;
			TabCtl.TabURLs = TabCtl.TabURLs + "," + url;*/						
			TabCtl.TabTitles  = "Indemnity Payment Details";
		}
		private void SetClaimActivityTop()
		{
			if(hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
			{
				cltClaimActivityTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
			}			
			if(hidACTIVITY_ID.Value!="" && hidACTIVITY_ID.Value!="0")
			{
				cltClaimActivityTop.ActivityID = int.Parse(hidACTIVITY_ID.Value);
			}			
			//cltClaimActivityTop.ActivityID = 1;
			//cltClaimActivityTop.ShowHeaderBand ="Claim";
			cltClaimActivityTop.Visible = true;        
		}

	}
}
