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
using System.Configuration;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for QuickQuoteLoad.
	/// </summary>
	public class QuickQuoteLoad : cmsbase
	{
		public string strState = "";
		public string strClientId = "";
		public string strUserId = "";
		public string strQuoteId = "";
		public string strType = "";
		public string strQuickQuoteOtherDtls = "";
		public string strErrorMsg = "";
		public string strClass = "";
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			strClientId = Request.QueryString["customer_id"].ToString();
			
			//Commented by pawan
			//strState = Request.QueryString["state_name1"].ToString().Trim().ToUpper();
			strState = Request.QueryString["state_name"].ToString().Trim().ToUpper();

			//Setting the customer in session
			SetCustomerID(strClientId);

			//strUserId = Request.QueryString["UserId"].ToString();
			strUserId = GetUserId(); 
			strQuoteId = Request.QueryString["QQ_ID"].ToString();
			SetQQ_ID(strQuoteId);
			SetCalledFor("CUSTOMER");
			strType = Request.QueryString["QQ_TYPE"].ToString();
			string strQuickQuoteNumber = Request.QueryString["QQ_NUMBER"].ToString();
			int indSpace;
			indSpace=strQuickQuoteNumber.IndexOf("(");
			if(indSpace>0)
				strQuickQuoteNumber=strQuickQuoteNumber.Substring(0,strQuickQuoteNumber.IndexOf("(")-1 );
		

			strQuickQuoteNumber=strQuickQuoteNumber.ToString().Trim();
			string strQuickQuoteLob = Server.UrlDecode( Request.QueryString["LOB_DESC"].ToString());

				
			
			//Changed By Pawan 
			//SetCookieQuote(strQuickQuoteNumber, strClientId,strQuoteId,strType,strQuickQuoteLob,Request.QueryString["state_name1"].ToString());
			SetCookieQuote(strQuickQuoteNumber, strClientId,strQuoteId,strType,strQuickQuoteLob,Request.QueryString["state_name"].ToString());
			cltClientTop.CustomerID = int.Parse(strClientId);
			cltClientTop.Visible = true;
			cltClientTop.ShowHeaderBand = "Client";

			strQuickQuoteOtherDtls = new Cms.BusinessLayer.BlCommon.ClsQuickQuote().GetQuickQuoteOthDtls(strClientId,strQuoteId,strQuickQuoteLob,strUserId,CarrierSystemID);
			
			if (strType=="AUTOP" || strType=="HOME" || strType=="CYCL" || strType=="REDW" || strType=="UMB")
			{
				if (strState.Trim().ToUpper() != "INDIANA" && strState.Trim().ToUpper() != "MICHIGAN")
				{
					strState="";
					strErrorMsg = "State is not Indiana or Michigan";
				}
			}
			else if (strType=="BOAT")
			{
				if (strState.Trim().ToUpper() != "INDIANA" && strState.Trim().ToUpper() != "MICHIGAN" && strState.Trim().ToUpper() != "WISCONSIN")
				{
					strState="";
					strErrorMsg = "State is not Indiana or Michigan Or Wisconsin";
				}
			}
			else
			{
				strState="";
				strErrorMsg = "Lob Not Supported Yet.";
			}
			//Note Controls to be rendered requires http: unlike http://
			//Added a new property in Cmsbase httpProtocolQQ 
			//Praveen 
			if (strType=="AUTOP")
			{
				strClass = httpProtocolQQ+"QuickQuote.dll#QuickQuote.Auto";
			}
			else if (strType=="HOME")
			{
				strClass = httpProtocolQQ+"QuickQuoteHome.dll#QuickQuoteHome.Home";
			}
			else if (strType=="CYCL")
			{
				strClass = httpProtocolQQ+"QuickQuoteCycl.dll#QuickQuoteCycl.CYCL";
			}
			else if (strType=="BOAT")
			{
				strClass = httpProtocolQQ+"QuickQuoteBoat.dll#QuickQuoteBoat.Boat";
			}
			else if (strType=="REDW")
			{
				strClass = httpProtocolQQ+"QuickQuoteDwell.dll#QuickQuoteDwell.Dwell";
			}
			else if (strType=="UMB")
			{
				strClass = httpProtocolQQ+"QuickQuoteUmbrella.dll#QuickQuoteUmbrella.Umbrella";
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
		private void SetCookieQuote(string aQuickQuoteNumber,string aUserId,string aQuoteId,string aType,string aQuickQuoteLob,string astate_name1)
		{
			//Added Mohit Agarwal 19-Feb 2007 to store last 3 cookies
			# region last 3 cookies
			string AgencyId = GetSystemId();
			if(AgencyId.ToUpper() !=CarrierSystemID)
			{
				//if(System.Web.HttpContext.Current.Request.Cookies["qqno" + GetSystemId() + "_" + GetUserId() + "_1"] != null)
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastPageVisited = new ClsGeneralInformation();
				DataSet ds = objLastPageVisited.GetLastVisitedPageEntry(int.Parse(GetUserId()),AgencyId);

					//if(System.Web.HttpContext.Current.Request.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"] != null)
					if(ds!=null && ds.Tables[0].Rows.Count>0 && ds.Tables[0]!=null && ds.Tables[0].Rows[0]["LAST_VISITED_QUOTE"].ToString() !="")
					{
						string prevCook = ds.Tables[0].Rows[0]["LAST_VISITED_QUOTE"].ToString();
						string [] cookArr = prevCook.Split('@');
						if(cookArr.Length > 0 && cookArr.Length < 4)
						{
							string quote_Details=aQuickQuoteNumber + "~" + aUserId + "~" + aQuoteId + "~" +  aType + "~" +  aQuickQuoteLob + "~" + astate_name1 + "~" + DateTime.Now.ToString() + "@" + ds.Tables[0].Rows[0]["LAST_VISITED_QUOTE"].ToString();
							Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedQuote= new ClsGeneralInformation(); 
							objLastVisitedQuote.UpdateLastVisitedPageEntry("Quote",quote_Details,int.Parse(GetUserId()),AgencyId);
							//System.Web.HttpContext.Current.Response.Cookies["qqno" + GetSystemId() + "_" + GetUserId() + "_1"].Value = aQuickQuoteNumber + "~" + aUserId + "~" + aQuoteId + "~" +  aType + "~" +  aQuickQuoteLob + "~" + astate_name1 + "~" + DateTime.Now.ToString() + "@" + System.Web.HttpContext.Current.Request.Cookies["qqno" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
						}
						else if(cookArr.Length >= 4)
						{
							int maxindex = cookArr.Length-1;
							if(maxindex >= 3)
								maxindex = 2;

							//System.Web.HttpContext.Current.Response.Cookies["qqno" + GetSystemId() + "_" + GetUserId() + "_1"].Value = aQuickQuoteNumber + "~" + aUserId + "~" + aQuoteId + "~" +  aType + "~" +  aQuickQuoteLob + "~" + astate_name1 + "~" + DateTime.Now.ToString() + "@";
							string quote_Details=aQuickQuoteNumber + "~" + aUserId + "~" + aQuoteId + "~" +  aType + "~" +  aQuickQuoteLob + "~" + astate_name1 + "~" + DateTime.Now.ToString() + "@";
							Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedQuote= new ClsGeneralInformation(); 
							objLastVisitedQuote.UpdateLastVisitedPageEntry("Quote",quote_Details,int.Parse(GetUserId()),AgencyId);
							for(int cookindex = 0; cookindex < maxindex; cookindex++)
							{
								quote_Details=cookArr[cookindex] + "@";
								//System.Web.HttpContext.Current.Response.Cookies["qqno" + GetSystemId() + "_" + GetUserId() + "_1"].Value += cookArr[cookindex] + "@";
							}
							objLastVisitedQuote.UpdateLastVisitedPageEntry("Quote",quote_Details,int.Parse(GetUserId()),AgencyId);
						}
						else
						{
							string quote_Details=aQuickQuoteNumber + "~" + aUserId + "~" + aQuoteId + "~" +  aType + "~" +  aQuickQuoteLob + "~" + astate_name1 + "~" + DateTime.Now.ToString() + "@";
							Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedQuote= new ClsGeneralInformation();
							objLastVisitedQuote.UpdateLastVisitedPageEntry("Quote",quote_Details,int.Parse(GetUserId()),AgencyId);
							//System.Web.HttpContext.Current.Response.Cookies["qqno" + GetSystemId() + "_" + GetUserId() + "_1"].Value=aQuickQuoteNumber + "~" + aUserId + "~" + aQuoteId + "~" +  aType + "~" +  aQuickQuoteLob + "~" + astate_name1 + "~" + DateTime.Now.ToString() + "@";
						}
						//System.Web.HttpContext.Current.Response.Cookies["qqno" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
					}
					else
					{
						string quote_Details=aQuickQuoteNumber + "~" + aUserId + "~" + aQuoteId + "~" +  aType + "~" +  aQuickQuoteLob + "~" + astate_name1 + "~" + DateTime.Now.ToString();
						Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedQuote= new ClsGeneralInformation(); 
						objLastVisitedQuote.UpdateLastVisitedPageEntry("Quote",quote_Details,int.Parse(GetUserId()),AgencyId);
						//System.Web.HttpContext.Current.Response.Cookies["qqno" + GetSystemId() + "_" + GetUserId() + "_1"].Value=aQuickQuoteNumber + "~" + aUserId + "~" + aQuoteId + "~" +  aType + "~" +  aQuickQuoteLob + "~" + astate_name1 + "~" + DateTime.Now.ToString() + "@";
						//System.Web.HttpContext.Current.Response.Cookies["qqno" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
					}
				}
					#endregion
				else
				{
					string quote_Details=aQuickQuoteNumber + "~" + aUserId + "~" + aQuoteId + "~" +  aType + "~" +  aQuickQuoteLob + "~" + astate_name1 + "~" + DateTime.Now.ToString();
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedQuote= new ClsGeneralInformation(); 
					objLastVisitedQuote.UpdateLastVisitedPageEntry("Quote",quote_Details,int.Parse(GetUserId()),AgencyId);					
					
				}
			}

		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
