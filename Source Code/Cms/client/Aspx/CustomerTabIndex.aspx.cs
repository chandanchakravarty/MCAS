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
namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for CustomerTabIndex.
	/// </summary>
	public class CustomerTabIndex : Cms.Client.clientbase
	{
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		

		private string strCustomerId;
		private string strSaveMsg;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabNumber;
		private string strCalledFrom = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			GetQueryString();
            base.ScreenId = "134";
			if (strCustomerId != null && strCustomerId.Trim() != "")
			{
				ShowClientTopControl(int.Parse(strCustomerId));
				
				if (strSaveMsg != null && strSaveMsg.Trim() != "" && Session["Insert"]!=null && Session["Insert"].ToString()=="1")// .ToString() added by Charles on 27-Oct-09 for Itrack 6521
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
					TabCtl.TabURLs = "AddCustomer.aspx?CalledFrom=" + strCalledFrom + "&CUSTOMER_ID=" + strCustomerId + "&SaveMsg=" + strSaveMsg;
                    TabCtl.TabTitles = ClsMessages.GetTabTitles("134", "2");
				}
				else
				{
					//TabCtl.TabURLs = "AddCustomer.aspx?CalledFrom=" + strCalledFrom + "&CUSTOMER_ID=" + strCustomerId;
					string url; 
					if(hidTabNumber.Value=="1")					
					{
						TabCtl.TabURLs = "AddCustomer.aspx?CalledFrom=" + strCalledFrom + "&CUSTOMER_ID=" + strCustomerId + "&BACK_TO_APPLICATION=1&";
						url = "ApplicantInsuedIndex.aspx?calledfrom="+strCalledFrom+"&Customer_ID=" + strCustomerId + "&CUSTOMER_TYPE=" + "11" + "&BackOption=" + "Y" + "&BACK_TO_APPLICATION=1";
					}
					else if(hidTabNumber.Value=="2")
					{
						TabCtl.TabURLs = "AddCustomer.aspx?CalledFrom=" + strCalledFrom + "&CUSTOMER_ID=" + strCustomerId + "&BACK_TO_APPLICATION=2&";
						url = "ApplicantInsuedIndex.aspx?calledfrom="+strCalledFrom+"&Customer_ID=" + strCustomerId + "&CUSTOMER_TYPE=" + "11" + "&BackOption=" + "Y" + "&BACK_TO_APPLICATION=2";
					}
					else
					{
						TabCtl.TabURLs = "AddCustomer.aspx?CalledFrom=" + strCalledFrom + "&CUSTOMER_ID=" + strCustomerId;
						url = "ApplicantInsuedIndex.aspx?calledfrom="+strCalledFrom+"&Customer_ID=" + strCustomerId + "&CUSTOMER_TYPE=" + "11" + "&BackOption=" + "Y" + "&BACK_TO_APPLICATION=0";


					}
					
					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

                    
                    //Add  Attachment Tab

                    url = "/cms/cmsweb/Maintenance/ContactIndex.aspx?CALLEDFROM=" + "CUSTOMER" + "&EntityId=" + strCustomerId + "&EntityType= " + "CUSTOMER" + "&CONTACT_TYPE_ID=" + "2";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

                    url = "/cms/cmsweb/Maintenance/AttachmentIndex.aspx?CALLEDFROM=" + "CUSTOMER" + "&EntityId=" + strCustomerId + "&EntityType= CUSTOMER";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

					//DrawTab(3,top.frames[1],'Balance Info',Url);

                    //Commented for Remove AttentionNotes Tab
                    //if(hidTabNumber.Value=="1")
                    //    url="AttentionNotes.aspx?calledfrom="+strCalledFrom+"&CustomerID=" + strCustomerId + "&BackOption="+"Y"+"&BACK_TO_APPLICATION=1";
                    //else
                    //    url="AttentionNotes.aspx?calledfrom="+strCalledFrom+"&CustomerID=" + strCustomerId + "&BackOption="+"Y"+"&";
                    //TabCtl.TabURLs = TabCtl.TabURLs + "," + url;



					//DrawTab(4,top.frames[1],'Attention Note',Url);
                   // TabCtl.TabTitles = "Customer Details,Co-Applicant Details,Attachments";
						//TabCtl.TabTitles =	cltClientTop.settabtitles(
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    TabCtl.TabTitles=ClsMessages.GetTabTitles("134", "1");
				}
			}
			else
			{
				cltClientTop.Visible = false;
				TabCtl.TabURLs = "AddCustomer.aspx?CalledFrom=" + strCalledFrom;
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                TabCtl.TabTitles = ClsMessages.GetTabTitles("134", "2");

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

		private void GetQueryString()
		{
			if (Request.Params["CalledFrom"]=="Direct")
			{
				strCustomerId	= GetCustomerID();
				strCalledFrom	=	Request.Params["CalledFrom"];
			}
			else
			{
				strCustomerId	= Request.Params["CUSTOMER_ID"];
			}
			strSaveMsg		= Request.Params["SaveMsg"];

			if (Request.Params["TabNumber"]!=null && Request.Params["TabNumber"]!="")
			{
				hidTabNumber.Value = Request.Params["TabNumber"].ToString();
			}
			else
			{
				hidTabNumber.Value ="0";
			}			

		}

		private void ShowClientTopControl(int intCustomerId)
		{
			if (intCustomerId != 0)
			{
				cltClientTop.CustomerID = intCustomerId;
				cltClientTop.Visible = true;
				cltClientTop.ShowHeaderBand = "Client";
			}
			else
			{
				cltClientTop.Visible = false;
			}
		}
	}
}
