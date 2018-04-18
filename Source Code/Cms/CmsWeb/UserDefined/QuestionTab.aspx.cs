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
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 

namespace Cms.CmsWeb.UserDefined
{
	/// <summary>
	/// Summary description for QuestionTab.
	/// </summary>
	public class QuestionTab  : Cms.CmsWeb.cmsbase  
	{
		protected System.Web.UI.HtmlControls.HtmlTableRow udScreen;
		protected System.Web.UI.HtmlControls.HtmlTableRow noudScreen;
		protected System.Web.UI.WebControls.DropDownList ddlScreens;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="197";
			if(!IsPostBack)
			{
				ClsUserQuestion loQuestion = new ClsUserQuestion();
				//string lsCarrierId="1";
                string lsAppId = GetPolicyID(); //GetAppID();
                string lsAppVerId = GetPolicyVersionID(); //GetAppVersionID();
				string lsClientId= GetCustomerID();
			
				cltClientTop.CustomerID = int.Parse(GetCustomerID());
                cltClientTop.PolicyID = int.Parse(lsAppId);//int.Parse(GetAppID());
                cltClientTop.PolicyVersionID = int.Parse(lsAppVerId);//int.Parse(GetAppVersionID());
            
				cltClientTop.ShowHeaderBand ="Policy";
				cltClientTop.Visible = true;  

				DataSet dsTemp = loQuestion.GetScreensList(lsAppId,lsAppVerId,lsClientId);
				if (dsTemp.Tables[0].Rows.Count == 0 )
				{
					noudScreen.Visible=true;
					udScreen.Visible=false;
					return;

				}
				 noudScreen.Visible=false;
				if (dsTemp.Tables[0].Rows.Count  == 1)
				{
					string ScreenId = dsTemp.Tables[0].Rows[0]["ScreenId"].ToString();
					Response.Redirect("PreviewUserQuestionTab.aspx?CallerId=menu&ScreenID=" + ScreenId);
				}
				else
				{
					ddlScreens.Items.Clear();
					ddlScreens.DataSource =  dsTemp;
					ddlScreens.DataValueField = "SCREENID";
					ddlScreens.DataTextField = "SCREENNAME";
					ddlScreens.DataBind();
					ddlScreens.Items.Insert(0,new ListItem("",""));
					ddlScreens.SelectedIndex=0;
					ddlScreens.AutoPostBack=true;

				}
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
			this.ddlScreens.SelectedIndexChanged += new System.EventHandler(this.ddlScreens_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ddlScreens_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//added by manab to provide a back button on the screen
			Response.Redirect("PreviewUserQuestionTab.aspx?backurl=questiontab.aspx&CallerId=menu&ScreenID=" + ddlScreens.SelectedItem.Value);
		}
	}
}
