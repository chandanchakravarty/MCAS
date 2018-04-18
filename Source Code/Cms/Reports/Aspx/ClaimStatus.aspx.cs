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
using Cms.DataLayer;
using Cms.CmsWeb;

namespace Reports.Aspx
{
	/// <summary>
	/// Summary description for ClaimStatus.
	/// </summary>
	public class ClaimStatus : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblAgent;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.TextBox lstInsuredName;
		protected System.Web.UI.WebControls.Label lblInsured;
		protected System.Web.UI.WebControls.TextBox lstPolicy;
		protected System.Web.UI.WebControls.Label lblClaim;
		protected System.Web.UI.WebControls.TextBox lstClaim;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.HtmlControls.HtmlImage imgClaim;
		protected System.Web.UI.HtmlControls.HtmlImage imgPolicy;
		protected System.Web.UI.WebControls.Label lblStartDate;
		protected System.Web.UI.WebControls.TextBox txtStartDate;
		protected System.Web.UI.WebControls.HyperLink hlkStartDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revStartDate;
		protected System.Web.UI.WebControls.CompareValidator cmpDate;
		protected System.Web.UI.WebControls.Label lblEndDate;
		protected System.Web.UI.WebControls.TextBox txtEndDate;
		protected System.Web.UI.WebControls.HyperLink hlkEndDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEndDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_POLICY_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID_NAME;
		protected System.Web.UI.WebControls.Label lblPolicy;
		public string strAgencyID="";
		public string strSystemID ="";
		protected System.Web.UI.HtmlControls.HtmlImage imgINSURED_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSURED_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_NAME;
		//Added for Itrack Issue 6070 on 8 July 09
		protected System.Web.UI.WebControls.Label lblOrderBy;
		protected System.Web.UI.WebControls.ListBox lstOrderBy;
		protected System.Web.UI.WebControls.Button btnSel;
		protected System.Web.UI.WebControls.Button btnDeSel;
		protected System.Web.UI.WebControls.ListBox lstSelOrderBy;
		protected System.Web.UI.WebControls.CustomValidator csvSelOrderBy;

		public string strCarrierSystemID ="";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			hlkStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtStartDate,document.forms[0].txtStartDate)");			
			hlkEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtEndDate,document.forms[0].txtEndDate)");			
			revStartDate.ValidationExpression = aRegExpDate;
			revEndDate.ValidationExpression = aRegExpDate;	
			base.ScreenId="419";
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");
			btnSel.Attributes.Add("onClick","funcSetOrderBy();return false;");//Added for Itrack Issue 6070 on 8 July 09
			btnDeSel.Attributes.Add("onClick","funcRemoveOrderBy();return false;");//Added for Itrack Issue 6070 on 8 July 09

			/* AGENCY VALUES
			 * Check the SystemID of the logged in user.
			 * If the user is not a Wolverine user then display records of that agency ONLY
			 * else the normal flow follows */
			strSystemID			 = GetSystemId();
			SetErrorMessages();//Added for Itrack Issue 6070 on 8 July 09
            strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
			if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
			{
				DataSet objDataSet =Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyIDAndNameFromCode(strSystemID);

				if (objDataSet.Tables[0].Rows.Count > 0 )
				{
					string strAgencyName = objDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
					strAgencyID = objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
					hidAGENCY_ID.Value = strAgencyID;

					// Agency TextBox
					txtAGENCY_NAME.Text = strAgencyName;
					imgAGENCY_NAME.Visible=false;
					//fillListBox(strAgencyID);
				}
			}

			// Customer ListBox
			strAgencyID = hidAGENCY_ID.Value;
		}

		private void SetErrorMessages()
		{
			rfvAGENCY_NAME.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("99");
			csvSelOrderBy.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1053");//Added for Itrack Issue 6070 on 8 July 09
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
