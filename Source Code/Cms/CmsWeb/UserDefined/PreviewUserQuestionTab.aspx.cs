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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.WebControls;

namespace Cms.CmsWeb.UserDefined
{
	/// <summary>
	/// Summary description for PreviewUserQuestionTab.
	/// </summary>
	public class PreviewUserQuestionTab : Cms.CmsWeb.cmsbase  
	{
		public string gStrStyle,cssFolder;
		protected System.Web.UI.WebControls.PlaceHolder TabHolder;
		protected System.Web.UI.WebControls.Label lblScreenID; 
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		ClsQuestionTab objQuestionTab = new ClsQuestionTab();
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Giving the screen id
			base.ScreenId = "197_0";

			//gStrStyle=EbxSession.Get("CustCSS");
			//cssFolder=gStrStyle.Substring(0,gStrStyle.IndexOf("."))+"/";
			string lstrScreenName="";
			string lstrURLPath="";
			int lintTabID = 0;
			int rowcount = 0;
			DataSet dsTab = new DataSet();
			string lsCallerId = Request["CallerId"];

			if (lsCallerId == "menu")
			{
				cltClientTop.CustomerID = int.Parse(GetCustomerID());
                cltClientTop.PolicyID = int.Parse(GetPolicyID());//int.Parse(GetAppID());
                cltClientTop.PolicyVersionID = int.Parse(GetPolicyVersionID()); //int.Parse(GetAppVersionID());
            
				cltClientTop.ShowHeaderBand ="Policy";
				cltClientTop.Visible = true;  
			}

			
			//added by manab to provide back button functionaliy
			string backbutton="false";
			if (Request.QueryString["backurl"] != null)
			{
				backbutton="true";
			}
			//******************************

			 if(!IsPostBack)
			{
				lblScreenID.Text = Request["ScreenID"];
			}
			try
			{
				dsTab = objQuestionTab.GetTabs(lblScreenID.Text);
				if(dsTab.Tables[0].Rows.Count > 0 )
				{
					while(rowcount< dsTab.Tables[0].Rows.Count)
					{	
						lintTabID = int.Parse(dsTab.Tables[0].Rows[rowcount].ItemArray[0].ToString());
						if(lstrScreenName.Trim()=="")
						{
							lstrScreenName = dsTab.Tables[0].Rows[rowcount].ItemArray[1].ToString();

							if (lsCallerId == "menu")
							{
								lstrURLPath = "UserQuestion.aspx?backbutton=" + backbutton + "&TabID="+lintTabID+"&ScreenID="+lblScreenID.Text;
							}
							else
							{
								lstrURLPath = "PreviewUserQuestion.aspx?TabID="+lintTabID+"&ScreenID="+lblScreenID.Text;
							}
						}
						else
						{
							lstrScreenName = lstrScreenName +"," + dsTab.Tables[0].Rows[rowcount].ItemArray[1].ToString();
							if (lsCallerId == "menu")
							{
								lstrURLPath = lstrURLPath + "," + "UserQuestion.aspx?backbutton=" + backbutton + "&TabID="+lintTabID+"&ScreenID="+lblScreenID.Text;
							}
							else
							{
								lstrURLPath = lstrURLPath + "," + "PreviewUserQuestion.aspx?TabID="+lintTabID+"&ScreenID="+lblScreenID.Text;
							}
						}
						rowcount = rowcount + 1;
					}
				}
				Control c2 = LoadControl("../webcontrols/BaseTabControl.ascx");
				((BaseTabControl)c2).TabTitles = lstrScreenName;
				((BaseTabControl)c2).TabURLs = lstrURLPath;
				((BaseTabControl)c2).TabLength=175;
				((BaseTabControl)c2).ID = "TabCtl";
				TabHolder.Controls.Add(c2);
			}
			catch(Exception ex)
			{
//				if(EbxSession.Exists())
//				{				
//					throw  new BritAmazonException(ex);
//				}
//				else
//				{
//					Response.Write("<SCRIPT language=Javascript>\n<!--\n");
//					Response.Write("top.location.href = \"../index.aspx?se=1\"\n");
//					Response.Write("//-->\n</SCRIPT>\n");
//					Response.End();
//
//				}
				throw(ex);
				
			}
			finally
			{
				GC.SuppressFinalize(this);
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

		}
		#endregion
	}
}
