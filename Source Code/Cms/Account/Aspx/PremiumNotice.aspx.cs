/******************************************************************************************
<Created Date			: - > 8-Jan-2007
<Modified By			: - > Mohit Agarwal
<Purpose				: - > To generate Premium Notice
Modification History

    
<Modified Date			: - > 
<Modified By			: - > 
<Purpose				: - > 
******************************************************************************************/
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
using System.Text;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for PremiumNotice.
	/// </summary>
	public class PremiumNotice : Cms.Account.AccountBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell tdArReport;
		protected System.Web.UI.WebControls.TextBox txtPolicyNo;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnPOLICY_NO;
		protected System.Web.UI.HtmlControls.HtmlImage imgPOLICY_NO;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblMessageDB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		public string URL="";
		protected System.Web.UI.HtmlControls.HtmlForm AR_Inquiry_Info;
		public string strPolicyNo="";
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPolicyNo;
		public string callingCustomerId="";
		protected System.Web.UI.WebControls.Button btnClose;
		public string calledfrom = "";
		

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId ="390";

			#region Button Permissions
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			btnSave.CmsButtonClass =CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;
			
			#endregion
			calledfrom = Request.QueryString["CalledFrom"];
			btnClose.Attributes.Add("onclick","javascript:window.close();");
			//AR_Inquiry_Info.Attributes.Add("onKeyPress","javascript:ShowReport();");
			URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();

			lblMessageDB.Text = "Premium Notice will be generated for DB policies only.";
			lblMessageDB.Visible = true;

			callingCustomerId="";
			try
			{
				callingCustomerId = Request.Params["CUSTOMER_ID"].ToString();
			}
			catch(Exception)
			{
			}

			if(callingCustomerId!="")
				txtPolicyNo.ReadOnly = true;

			if(!Page.IsPostBack)
			{
				rfvPolicyNo.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("485");
			}
		}

/*		public string GenerateRandomCode(string FirstName, string LastName)
		{
			Random rand = new Random(51);
			string lastpart = rand.ToString();
			if(FirstName.Length <1 && LastName.Length < 1)
				return "";
			string firstpart = FirstName;
			string secpart = LastName;

			if(firstpart.Length > 3)
				firstpart = firstpart.Substring(0,3);
			if(secpart.Length > 4)
				secpart = secpart.Substring(0,4);

			return firstpart + secpart + lastpart;
		}
*/
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int intPolicyId=0;
			int intPolicy_version_id=0;
			int intCustomerID=0;
			string[] arrLookUp;
			string querystring,responsestring;

//			Cms.BusinessLayer.BlCommon.ClsPolicyNoSetup.GetFiscalYearPoliciesFile();
			if(hidPOLICYINFO.Value !="")
			{
				arrLookUp=(hidPOLICYINFO.Value).Split('^');
				strPolicyNo=arrLookUp[2].ToString();
			}

			if(hidPOLICYINFO.Value !="" && strPolicyNo == txtPolicyNo.Text.Trim())
			{
				arrLookUp=(hidPOLICYINFO.Value).Split('^');
				intPolicyId =int.Parse(arrLookUp[0].ToString());
				intPolicy_version_id =int.Parse(arrLookUp[1].ToString());
				intCustomerID =int.Parse(arrLookUp[2].ToString());
				strPolicyNo=arrLookUp[2].ToString();
				querystring = "&CUSTOMER_ID=" + intCustomerID.ToString() + "&POLICY_ID=" + intPolicyId.ToString() + "&POLICY_VER_ID=" + intPolicy_version_id.ToString();
				responsestring = "<script language='javascript'> window.open(" + "'../../application/aspx/DecPage.aspx?CalledFrom=Notice&CALLEDFOR=PREM&LOB_ID="+"CHK" + querystring + "'); </script>";
				//				responsestring = "<script language='javascript'> window.open(" + "'../../application/aspx/DecPage.aspx?CalledFrom=POLICY&CALLEDFOR=REINS_NOTICE&CHECK_ID=Agent&LOB_ID="+"2" + querystring + "'); </script>";
				lblMessage.Visible = false;
				Response.Write(responsestring);
			}
			else if(txtPolicyNo.Text.Trim() != "")
			{
				DataSet dsPolicy = Cms.BusinessLayer.BlCommon.ClsPolicyNoSetup.GetPremiumNoticePolicyData(txtPolicyNo.Text.Trim());
				if(dsPolicy == null || dsPolicy.Tables[0].Rows.Count < 1)
				{
					lblMessage.Text = "Not a valid policy number provided, please check.";
					lblMessage.Visible = true;
				}
				else if(dsPolicy != null && dsPolicy.Tables[0].Rows.Count > 0 && dsPolicy.Tables[0].Rows[0]["BILL_TYPE"].ToString().Trim() == "AB")
				{ 
					lblMessage.Text = "For AB Type Policies no Premium Notice will be Generated.";
					lblMessage.Visible = true;
				}
				else if(dsPolicy != null && dsPolicy.Tables[0].Rows.Count > 0 && dsPolicy.Tables[0].Rows[0]["BILL_TYPE"].ToString().Trim() != "DB")
				{ 
					lblMessage.Text = "Not a DB Policy provided, please check.";
					lblMessage.Visible = true;
				}
				else
				{
					try
					{
						intPolicyId =int.Parse(dsPolicy.Tables[0].Rows[0]["POLICY_ID"].ToString());
						intPolicy_version_id =int.Parse(dsPolicy.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
						intCustomerID =int.Parse(dsPolicy.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
						strPolicyNo=txtPolicyNo.Text.Trim();
						querystring = "&CUSTOMER_ID=" + intCustomerID.ToString() + "&POLICY_ID=" + intPolicyId.ToString() + "&POLICY_VER_ID=" + intPolicy_version_id.ToString();
                        responsestring = "<script language='javascript'> window.open(" + "'../../application/aspx/DecPage.aspx?CalledFrom=Notice&CALLEDFOR=PREM&LOB_ID=" + "CHK" + querystring + "'); </script>";
						//responsestring = "<script language='javascript'> window.open(" + "'../../application/aspx/DecPage.aspx?CalledFrom=CHECKPDFPRINT&CALLEDFOR=CHECK&LOB_ID="+"CHK" + querystring + "'); </script>";
						//				responsestring = "<script language='javascript'> window.open(" + "'../../application/aspx/DecPage.aspx?CalledFrom=POLICY&CALLEDFOR=REINS_NOTICE&CHECK_ID=Agent&LOB_ID="+"2" + querystring + "'); </script>";
						lblMessage.Visible = false;
						Response.Write(responsestring);
					}
					catch(Exception ex)
					{
						lblMessage.Text = "Some error occurred in retreiving policy details.";
						lblMessage.Visible = true;
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					}
				}
			}
			else
			{
				lblMessage.Visible=true;
				lblMessage.Text = "Please provide a Policy Number.";
			}
		}
	}
}
