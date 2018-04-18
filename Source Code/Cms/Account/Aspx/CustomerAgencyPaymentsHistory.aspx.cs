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
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for CustomerAgencyPaymentsHistory.
	/// </summary>
	public class CustomerAgencyPaymentsHistory : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.CompareValidator cmpSpoolDate;
		protected System.Web.UI.WebControls.TextBox txtDateFrom;
		protected System.Web.UI.WebControls.HyperLink hlkDateFrom;
		protected System.Web.UI.WebControls.CustomValidator csvDateFrom;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDateFrom;
		protected System.Web.UI.WebControls.TextBox txtDateTo;
		protected System.Web.UI.WebControls.HyperLink hlkDateTo;
		protected System.Web.UI.WebControls.CustomValidator csvDateTo;
		protected System.Web.UI.WebControls.TextBox txtPolicyNo;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnPOLICY_NO;
		protected System.Web.UI.HtmlControls.HtmlImage imgPOLICY_NO;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgCustomer;
		protected System.Web.UI.WebControls.TextBox txtAMOUNT;
		protected System.Web.UI.WebControls.TextBox txtAGENCY;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDateTo;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;		
		public string strAgencyID="";
		public string  strSystemID="";
		public string strUserID="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			base.ScreenId="411";  //Screen Id Added by Sibin on 30 Oct 08
			hlkDateFrom.Attributes.Add("OnClick","fPopCalendar(document.CustomerAgencyPaymentsHistory.txtDateFrom,document.CustomerAgencyPaymentsHistory.txtDateFrom)"); //Javascript Implementation for Calender				
			hlkDateTo.Attributes.Add("OnClick","fPopCalendar(document.CustomerAgencyPaymentsHistory.txtDateTo,document.CustomerAgencyPaymentsHistory.txtDateTo)"); //Javascript Implementation for Calender				

			btnReport.CmsButtonClass	= CmsButtonType.Execute;
			btnReport.PermissionString	= gstrSecurityXML;

			//Added by Asfa (29-May-2008) - iTrack #3993
			strSystemID			 = GetSystemId();
			strUserID            = GetUserId();

            //Changed by Charles on 19-May-10 for Itrack 51
            string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
			if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
			{
				DataSet objDataSet =Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyIDAndNameFromCode(strSystemID);

				if (objDataSet.Tables[0].Rows.Count > 0 )
				{
					string strAgencyName = objDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
					strAgencyID = objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
					hidAGENCY_ID.Value = strAgencyID;

					// Agency TextBox
					txtAGENCY.Text = strAgencyName;
					imgAGENCY.Visible=false;
				}
			}

			if(!Page.IsPostBack)
			{
				SetValidators();
			}


		}

		private void SetValidators()
		{
			revDateFrom.ValidationExpression = aRegExpDate;
			revDateTo.ValidationExpression	= aRegExpDate;


			revDateFrom.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revDateTo.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			
			csvDateFrom.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("448");
			csvDateTo.ErrorMessage					= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("446");
	
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
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReport_Click(object sender, System.EventArgs e)
		{
			string strValue = "<script>"
				+ "window.open('CustomerAgencyPaymentsHistoryDetails.aspx?FromDate=" + txtDateFrom.Text + "&ToDate=" + txtDateTo.Text + "&PolicyNo=" + txtPolicyNo.Text + "&Agency=" + hidAGENCY_ID.Value + "&Customer=" +hidCustomer_ID.Value + "&Amount=" +txtAMOUNT.Text+"');</script>";
	
			if(!ClientScript.IsClientScriptBlockRegistered("CustomerAgencyPaymentsHistoryDetails"))
				ClientScript.RegisterClientScriptBlock(this.GetType(),"CustomerAgencyPaymentsHistoryDetails",strValue);
		}
	}
}
