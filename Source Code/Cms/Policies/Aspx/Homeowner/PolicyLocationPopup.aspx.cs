/******************************************************************************************
<Author					: -   Pradeep Iyer
<Start Date				: -	  5/23/2005 3:02:03 PM
<End Date				: -	
<Description			: -  Popup window to copy the Recreational vehicles record	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 30/08/2005
<Modified By			: - Anurag Verma
<Purpose				: - Changing message and passing screen id
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

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for LocationPopup.
	/// </summary>
	public class PolicyLocationPopup : Cms.Policies.policiesbase 
	{
		//protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DropDownList cmbLOCATION_ID;
		protected System.Web.UI.WebControls.Panel pnl;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv_LOCATION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.WebControls.Label Label1;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmit;
		string strCalledFrom="";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
				
			}
		
			//Setting screen Id.	
			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					base.ScreenId="239_0_0";
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="259_0_0";
					break;
				default:
					base.ScreenId="261";
					break;
			}
			#endregion

			btnClose.Attributes.Add("onclick","javascript:window.close();");
			btnClose.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnClose.PermissionString = gstrSecurityXML;

			btnSubmit.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSubmit.PermissionString = gstrSecurityXML;
			if ( !Page.IsPostBack )
			{
				LoadDropdown();

				this.rfv_LOCATION_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			}
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
			this.btnSubmit.Click +=new System.EventHandler(this.btnSubmit_Click);

		}
		#endregion

		private void LoadDropdown()
		{
			
			Cms.BusinessLayer.BlApplication.ClsDwellingDetails objDwelling=new Cms.BusinessLayer.BlApplication.ClsDwellingDetails();
			int intCustomerID = Convert.ToInt32(GetCustomerID());
			int intPolID = Convert.ToInt32(GetPolicyID());
			int intPolVersionID = Convert.ToInt32(GetPolicyVersionID());
	        
			//Get The Remaining Locations
			DataTable dtLocations = objDwelling.GetRemainingLocationsPolicy(intCustomerID,intPolID,
				intPolVersionID					
				);
		

			if ( dtLocations.Rows.Count == 0 )
			{
				lblMessage.Visible = true;
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
				pnl.Visible = false;
				btnSubmit.Visible = false;
				return;
			}
			
			this.cmbLOCATION_ID.DataSource = dtLocations;
			this.cmbLOCATION_ID.DataTextField = "Address";
			//this.cmbLOCATION_ID.DataValueField = "LOC_SUBLOC";
			this.cmbLOCATION_ID.DataValueField = "LOCATION_ID";
			this.cmbLOCATION_ID.DataBind();

		}
		
		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			int intCustomerID = Convert.ToInt32(GetCustomerID());
			int intPolID = Convert.ToInt32(GetPolicyID());
			int intPolVersionID = Convert.ToInt32(GetPolicyVersionID());
			int intDwellingID = Convert.ToInt32(Request.QueryString["DWELLING_ID"]);

			string locSubLoc = this.cmbLOCATION_ID.SelectedItem.Value;
			//string[] arrlocSubLoc = locSubLoc.Split(",".ToCharArray());

			int intLocationID = Convert.ToInt32(locSubLoc);

			string LocNbr	 = this.cmbLOCATION_ID.SelectedItem.Text.Trim().Substring(0,1);

			int intSubLocID  = Convert.ToInt32(LocNbr);//Convert.ToInt32(arrlocSubLoc[1]);

			//int intSubLocID = 0;//Convert.ToInt32(arrlocSubLoc[1]);

			ClsDwellingDetails objDwelling = new ClsDwellingDetails();
			
			int dwellingID = 0;
			
			this.hidFormSaved.Value = "1";
			
			lblMessage.Visible=true; 
			
			int intFromUserID =	Convert.ToInt32(GetUserId());
			try
			{
				dwellingID = objDwelling.CopyPolicyDwellingDetails(intCustomerID,
					intPolID,
					intPolVersionID,
					intDwellingID,intLocationID,intSubLocID,intFromUserID);

				

				//Removing the selected combo items
				cmbLOCATION_ID.Items.Remove(cmbLOCATION_ID.SelectedItem);
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				if ( ex.InnerException != null )
				{
					lblMessage.Text = ex.InnerException.Message;
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					return;
				}
			}
			

			if ( dwellingID == -10 )
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"18");
				return;
			}

			if ( dwellingID > 0 )
			{
				if (!ClientScript.IsStartupScriptRegistered("Refresh"))
				{
					//string keyValue = this.hidREC_VEH_ID.Value;
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3" );
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
					
					string strCode = @"<script>window.opener.Refresh(1," + dwellingID.ToString() + "1);window.opener.document.getElementById('lblMessage').innerHTML = 'Information copied successfully.';window.close()" + 
						"//window.close();</script>";

					base.OpenEndorsementDetails();
					ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strCode);
					

				}
			}
		}
		

	}
}
