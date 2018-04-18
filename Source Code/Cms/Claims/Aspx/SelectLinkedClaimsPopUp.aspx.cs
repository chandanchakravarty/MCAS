using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Cms.CmsWeb.Controls; 
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlClient; 
using Cms.BusinessLayer.BLClaims;
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using Cms.CmsWeb.WebControls;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// Summary description for SelectLinkedClaimsPopUp.
	/// </summary>
	public class SelectLinkedClaimsPopUp : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.DataGrid dgClaimList;
		protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label lblTitle;
        protected System.Web.UI.WebControls.Label capSearchOPtion;
        protected System.Web.UI.WebControls.Label capSearchCriteria;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidExistingClaimNumList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidExistingClaimID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNumberRows;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataGridID;
        protected System.Web.UI.WebControls.DropDownList drpSearchCols;
		protected System.Web.UI.WebControls.TextBox txtSearchCriteria;
		protected System.Web.UI.WebControls.Button btnSearchResults;
		protected string []arrExistingClaimList;
        protected ResourceManager objResourceMgr;
        public string headerstr;
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Done for Itrack Issue 6553 on 13 Oct 09
			 base.ScreenId = "306_0_0";
             objResourceMgr = new ResourceManager("Cms.Claims.Aspx.SelectLinkedClaimsPopUp", Assembly.GetExecutingAssembly());	
			btnClose.CmsButtonClass			=	CmsButtonType.Read;
			btnClose.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			if(!IsPostBack)			
			{
				GetQueryStringValues();
				hidDataGridID.Value = dgClaimList.ClientID;
				btnSave.Attributes.Add("onClick","javascript: return submitForm();");
				btnClose.Attributes.Add("onClick","javascript: return closeForm();");	
				//Added By Praveen Kumar
                txtSearchCriteria.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('"+btnSearchResults.UniqueID+"').click();return false;}} else {return true}; ");


				//
				BindGrid();
                setCaptions();
                BindNotifying();

				hidNumberRows.Value = dgClaimList.Items.Count.ToString();
			}
			//RP - shifted under not post back code
			//BindGrid();
			//hidNumberRows.Value = dgClaimList.Items.Count.ToString();
		}
		private void GetQueryStringValues()
		{
			if(Request.QueryString["ExistingClaimNumList"]!=null && Request.QueryString["ExistingClaimNumList"].ToString()!="")
				hidExistingClaimNumList.Value = Request.QueryString["ExistingClaimNumList"];
			if(Request.QueryString["ExistingClaimID"]!=null && Request.QueryString["ExistingClaimID"].ToString()!="")
			{
				hidExistingClaimID.Value = Request.QueryString["ExistingClaimID"];
				arrExistingClaimList = hidExistingClaimNumList.Value.Split('^');
			}			
		}
        private void setCaptions()
        {
            lblTitle.Text = objResourceMgr.GetString("lblTitle");
            capSearchOPtion.Text = objResourceMgr.GetString("capSearchOPtion");
            btnSearchResults.Text = objResourceMgr.GetString("btnSearchResults");
            capSearchCriteria.Text = objResourceMgr.GetString("capSearchCriteria");
            headerstr = objResourceMgr.GetString("headerstr");   
          
        }
        private void BindNotifying()
        {
            drpSearchCols.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("InsNa");
            drpSearchCols.DataTextField = "LookupDesc";
            drpSearchCols.DataValueField = "LookupCode";
            drpSearchCols.DataBind();

        }

		private void BindGrid()
		{
			ClsClaimsNotification objClaimsNotification = new ClsClaimsNotification();			
			DataTable dtClaimsList = objClaimsNotification.GetClaimsList(int.Parse(hidExistingClaimID.Value),"NA","");				
			if(dtClaimsList!=null && dtClaimsList.Rows.Count>0)
			{
				dgClaimList.DataSource = dtClaimsList;
				dgClaimList.DataBind();
			}
			else
			{
				lblMessage.Text = "No records found";
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
			this.btnSearchResults.Click += new System.EventHandler(this.btnSearchResults_Click);
			
			this.dgClaimList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgClaimList_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgClaimList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				CheckBox chkClaimNum = (CheckBox)e.Item.FindControl("chkClaimNum");
				Label lblCLAIM_ID = (Label)e.Item.FindControl("lblCLAIM_ID");
				if (chkClaimNum==null || lblCLAIM_ID==null || lblCLAIM_ID.Text=="" || arrExistingClaimList.Length==0)
					return;			
				for(int iCounter=0;iCounter<arrExistingClaimList.Length;iCounter++)
				{
					if(lblCLAIM_ID.Text.Trim() == arrExistingClaimList[iCounter].ToString().Trim())
						chkClaimNum.Checked = true;
				}
				
			}
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.Cells[0].Text = objResourceMgr.GetString("SELECT");
                e.Item.Cells[1].Text = objResourceMgr.GetString("CLAIM_NUMBER");
                e.Item.Cells[2].Text = objResourceMgr.GetString("LOSS_DATE");
                e.Item.Cells[3].Text = objResourceMgr.GetString("ADJUSTER_NAME");
                e.Item.Cells[4].Text = objResourceMgr.GetString("CLAIMANT_NAME");
                e.Item.Cells[5].Text = objResourceMgr.GetString("CLAIM_STATUS_DESC");
            }
		}

		private void btnSearchResults_Click(object sender, System.EventArgs e)
		{
			GetQueryStringValues();

			ClsClaimsNotification objClaimsNotification = new ClsClaimsNotification();			
			DataTable dtClaimsList = objClaimsNotification.GetClaimsList(int.Parse(hidExistingClaimID.Value)
																		,drpSearchCols.SelectedValue.ToString()
																		,txtSearchCriteria.Text.ToString().Replace("'","''"));//Done for Itrack 5138 on 14 May 2009

			if(dtClaimsList!=null ) //&& dtClaimsList.Rows.Count>0)
			{
				dgClaimList.DataSource = dtClaimsList;
				dgClaimList.DataBind();
				hidNumberRows.Value = dgClaimList.Items.Count.ToString();
			}
			else
			{
				lblMessage.Text = "No records found";
			}			
		}

		
	}
}
