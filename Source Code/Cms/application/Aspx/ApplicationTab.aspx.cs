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

namespace Cms.Application.Aspx
{
	/// <summary>
	/// Summary description for ApplicationTab.
	/// </summary>
	public class ApplicationTab: Cms.Application.appbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		private const string CALLED_FROM_CLIENT="CLT";
		private const string CALLED_FROM_INNER_CLIENT ="InCLT";
		private const string CALLED_FROM_APP = "APP";
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		private string strCustomerId,strAppId,strAppVersionId,strLOB_ID = "";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabNumber;
        
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			GetQueryString();
//			string strCustomerId	= GetCustomerID();
//			string strAppId		= Request.Params["APP_ID"];
//			string strAppVersionId	= Request.Params["APP_VERSION_ID"];
//			int intResult=ClsGeneralInformation.CheckForApplicationStatus(int.Parse(strCustomerId==""?"0": strCustomerId) ,int.Parse (strAppId ==""?"0":strAppId),int.Parse(strAppVersionId==""?"0":strAppVersionId));
			string strLoadAfterSave = "";

			//This parameter will be used to check whether page is being loaded
			//after saving the application
			if (Request.Params["LoadedAfterSave"] != null)
			{
				strLoadAfterSave = Request.Params["LoadedAfterSave"].ToString();
			}
			if (Request.Params["TabNumber"] != null && Request.Params["TabNumber"] != "" )
			{
				hidTabNumber.Value= Request.Params["TabNumber"].ToString();
			}			

			if (strCustomerId != null && strCustomerId.Trim() != "")
			{
				
				//TabCtl.TabURLs = "GeneralInformation.aspx?CUSTOMER_ID=" + strCustomerId + "&APP_ID="+strAppId+"&APP_VERSION_ID="+strAppVersionId+"&CALLEDFROM="+Request.Params["CalledFrom"] + "&LoadedAfterSave=" + strLoadAfterSave + "&";
				// setting lob id
				if(strCustomerId != null && strAppId != null && strAppVersionId != null  && strAppId != "" && strAppVersionId != "")
				{
					ShowClientTopControl(int.Parse(strCustomerId),"APP");

					//RAvindra(09-11-2009)
					//DataSet dsApplication = ClsGeneralInformation.FetchApplication(int.Parse(strCustomerId),int.Parse(strAppId),int.Parse(strAppVersionId));
					
					DataSet dsApplication = ClsGeneralInformation.FetchApplicationLobID(int.Parse(strCustomerId),int.Parse(strAppId),int.Parse(strAppVersionId));
					
					if(dsApplication != null)
					{
						if(dsApplication.Tables[0].Rows.Count > 0)
						{
							strLOB_ID = dsApplication.Tables[0].Rows[0]["LOB_ID"].ToString();
						}
					}
				}
				else
				{
					ShowClientTopControl(int.Parse(strCustomerId),"CUST");
				}
				string url = "GeneralInfoEx.aspx?CUSTOMER_ID=" + strCustomerId + "&APP_ID="+strAppId+"&APP_VERSION_ID="+strAppVersionId+"&CALLEDFROM="+Request.Params["CalledFrom"] + "&LoadedAfterSave=" + strLoadAfterSave + "&";
				TabCtl.TabURLs = url;
				if(strLOB_ID=="5")
				{
					url="PkgLobDetailsIndex.aspx?CUSTOMER_ID="+strCustomerId+"&APP_ID="+strAppId+"&APP_VERSION_ID="+strAppVersionId+"&";
					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

					url="AddApplicantInsured.aspx?CUSTOMER_ID="+strCustomerId+"&APP_ID="+strAppId+"&APP_VERSION_ID="+strAppVersionId+"&LOB_ID="+strLOB_ID+"&";
					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

					url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=Application&EntityType=Application&EntityId=0&CUSTOMER_ID=" + strCustomerId +"&APP_ID="+ strAppId + "&APP_VERSION_ID="+ strAppVersionId  + "&";
					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

					url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=Application&EntityType=Application&EntityId=0&CUSTOMER_ID=" + strCustomerId +"&APP_ID="+ strAppId + "&APP_VERSION_ID="+ strAppVersionId  + "&";
					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

//					url="DecPage.aspx?CALLEDFOR=DECPAGE";
//					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

					//TabCtl.TabTitles  = "Application Information,Co-Applicant Details,Attachment, Declaration Page";	
					TabCtl.TabTitles  = "Application Information,Co-Applicant Details,Attachment";						
					
				}
				else if(strLOB_ID!="5" && strLOB_ID!="0")
				{
					url="AddApplicantInsured.aspx?CUSTOMER_ID="+strCustomerId+"&APP_ID="+strAppId+"&APP_VERSION_ID="+strAppVersionId+"&LOB_ID="+strLOB_ID+"&";
					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

					url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=Application&EntityType=Application&EntityId=0&CUSTOMER_ID=" + strCustomerId +"&APP_ID="+ strAppId + "&APP_VERSION_ID="+ strAppVersionId  + "&";
					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

					url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=Application&EntityType=Application&EntityId=0&CUSTOMER_ID=" + strCustomerId +"&APP_ID="+ strAppId + "&APP_VERSION_ID="+ strAppVersionId  + "&";
					TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

					
					//TabCtl.TabURLs = TabCtl.TabURLs + "," + "/cms/application/aspx/decpage.aspx?CALLEDFOR=DECPAGE";

					//TabCtl.TabTitles  = "Application Information,Co-Applicant Details,Attachment,Declaration Page";
					TabCtl.TabTitles  = "Application Information,Co-Applicant Details,Attachment";
					
				}				

			}
			else
			{
				cltClientTop.Visible = false;
				TabCtl.TabURLs = "GeneralInformation.aspx?CALLEDFROM="+Request.Params["CalledFrom"] + "&LoadedAfterSave=" + strLoadAfterSave + "&";

			}
			#region Setting screen id
			switch(strLOB_ID)
			{
				case "1" : // HOME
					base.ScreenId	=	"201_1";
					break;
				case "2" : // Private passenger automobile
					base.ScreenId	=	"201_2";
					break;
				case "3" : // Motorcycle
					base.ScreenId	=	"201_3";
					break;
				case "4" : // Watercraft
					base.ScreenId	=	"201_4";
					break;
				case "5" : // Umbrella
					base.ScreenId	=	"201_5";
					break;
				case "6" : // Rental dwelling
					base.ScreenId	=	"201_6";
					break;
				case "7" : // General liability
					base.ScreenId	=	"201_7";
					break;
				case "8" : // Aviation
					base.ScreenId	=	"201_10";
					break;
				case "" : // Application is added
					base.ScreenId	=	"201_0";
					break;
				default : //
					base.ScreenId	=	"201_0";
					break;
			}
			#endregion

			#region set Workflow cntrol
			SetWorkFlow();
			#endregion
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
			if (Request.Params["CalledFrom"]==CALLED_FROM_CLIENT || Request.Params["CalledFrom"]==CALLED_FROM_INNER_CLIENT)
			{
				strCustomerId	= GetCustomerID();
				strAppId		= Request.Params["APP_ID"];
				strAppVersionId	= Request.Params["APP_VERSION_ID"];
			}
			else if(Request.Params["CalledFrom"]==CALLED_FROM_APP)
			{
				strCustomerId	= GetCustomerID();
				strAppId		= GetAppID();
				strAppVersionId	= GetAppVersionID();
			}
			else
			{
				strCustomerId	= Request.Params["CUSTOMER_ID"];
				strAppId		= Request.Params["APP_ID"];
				strAppVersionId	= Request.Params["APP_VERSION_ID"];

				

			}			
		}

		private void ShowClientTopControl(int intCustomerId,string strAPP)
		{
			if(strAPP.Equals("APP"))
			{
				cltClientTop.ApplicationID = int.Parse(strAppId);
				cltClientTop.CustomerID = intCustomerId;
				cltClientTop.AppVersionID = int.Parse(strAppVersionId);
				cltClientTop.ShowHeaderBand = "Application";
				cltClientTop.FlagApp="APP";
				cltClientTop.Visible= true;
			}
			else
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
	
		private void SetWorkFlow()
		{
			if(base.ScreenId == "201_1" || base.ScreenId == "201_2" || base.ScreenId == "201_3" || base.ScreenId == "201_4" 
				|| base.ScreenId == "201_5" || base.ScreenId == "201_6" || base.ScreenId == "201_7" || base.ScreenId == "201_10" )
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",strCustomerId);
				myWorkFlow.AddKeyValue("APP_ID",strAppId);
				myWorkFlow.AddKeyValue("APP_VERSION_ID",strAppVersionId);
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
	}
}

