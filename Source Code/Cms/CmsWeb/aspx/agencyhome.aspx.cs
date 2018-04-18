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
using System.Resources; 
using System.Reflection; 
using Cms.BusinessLayer.BlCommon; 
using Cms.BusinessLayer.BlApplication;

/******************************************************************************************
	<Author					: Mohit Agarwal	- >
	<Start Date				: 14-Feb-2007	-	>
	<End Date				: - >
	<Description			: To display home page for Agency Login- >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

namespace Cms.CmsWeb.Aspx
{
	/// <summary>
	/// Summary description for index.
	/// </summary>
	/// 
	
	public class agencyhome : cmsbase
	{
		protected System.Web.UI.WebControls.HyperLink hlkQuoteSearch;
		protected System.Web.UI.WebControls.HyperLink hlkAppNew;
		protected System.Web.UI.WebControls.HyperLink hlkAppSearch;
		protected System.Web.UI.WebControls.HyperLink hlkPolicy;
		protected System.Web.UI.WebControls.HyperLink hlkClaim;
		protected System.Web.UI.WebControls.HyperLink hlkARInquiry;
		protected System.Web.UI.WebControls.Label capQuoteNew;
		protected System.Web.UI.WebControls.Label lblQuoteNew;
		protected System.Web.UI.WebControls.HyperLink hlkQuoteNew;
		protected System.Web.UI.WebControls.HyperLink hlkDecPage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_LIST;

        private string strCustomerId = "";//strColorScheme,strUserId,
		public string cookieCustomerName_1="";
		public string cookieApplication_1="";
		public string cookiePolicy_1="";
		public string cookieCustDate_1="";
		public string cookieQQ_1="";
		public string cookieClaim_1="";
		public string cookieClaimDate_1="";
		//public string cookieClaim="";
		public string cookieQQDate_1="";
		//public string cookieClaimDate="";
		public string cookieQQSate_1="";
		public string cookieAppDate_1="";
		public string cookiePolDate_1="";

		public string cookieCustomerName_2="";
		public string cookieApplication_2="";
		public string cookiePolicy_2="";
		public string cookieCustDate_2="";
		public string cookieQQ_2="";
		public string cookieClaim_2="";
		public string cookieClaimDate_2="";
		//public string cookieClaim="";
		public string cookieQQDate_2="";
		//public string cookieClaimDate="";
		public string cookieQQSate_2="";
		public string cookieAppDate_2="";
		public string cookiePolDate_2="";
		
		public string cookieCustomerName_3="";
		public string cookieApplication_3="";
		public string cookiePolicy_3="";
		public string cookieCustDate_3="";
		public string cookieQQ_3="";
		public string cookieClaim_3="";
		public string cookieClaimDate_3="";
		//public string cookieClaim="";
		public string cookieQQDate_3="";
		//public string cookieClaimDate="";
		public string cookieQQSate_3="";
		public string cookieAppDate_3="";
		public string cookiePolDate_3="";
		
		public string dtFormat="";
		protected string lblMonths="";
		protected string lblDays="";
		public string URL="";
		public string agencyURL = "";
		protected System.Web.UI.WebControls.Calendar agencyCalendar;
		protected System.Web.UI.WebControls.HyperLink hlkCustPayFrmAgency;
		protected System.Web.UI.WebControls.HyperLink hlkExternalAgnUrl;
		protected System.Web.UI.WebControls.HyperLink hlkAutoIdCard;

		protected ResourceManager aObjResMang;

		//public string sesImageFolder="";
		private void Page_Load(object sender, System.EventArgs e)
		{
			//agencyCalendar.Attributes.Add("OnClick", "return false;");
			
			//hlkQuoteNew.Attributes.Add("onclick", "javascript:NewCustomer();");
			// Put user code to initialize the page here
			//sesImageFolder=Session["imagefolder"].ToString(); 
			dtFormat = "MM/dd/yyyy";
			URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
			
			hidAGENCY_CODE.Value = GetSystemId();

			if(!Page.IsPostBack)
			{
				FillAgency(hidAGENCY_CODE.Value);
				
			}

			//Get Agency Lic Num AGENCY_LIC_NUM
			string agencyNumCode = "";
			agencyURL = Cms.BusinessLayer.BlCommon.ClsCommon.GetExternalAgencyURL();
			agencyNumCode = Cms.BusinessLayer.BlCommon.ClsCommon.GetAgencyNumCode(Session["systemId"].ToString().Trim());
			agencyURL = agencyURL + agencyNumCode;



			//When called from Capital rater
			if(Session["GUID"]!=null && Session["GUID"].ToString()!="")
			{
				Response.Redirect("/cms/cmsweb/aspx/CapitalRateComparison_Test.aspx");
			}

			getCookie_1();
			SetVariables();
		}
		private void SetVariables()
		{
			//declaring object for resource manager	
			aObjResMang=new ResourceManager("Cms.CmsWeb.WebControls.DiaryCalendar",Assembly.GetExecutingAssembly());   
			lblMonths=aObjResMang.GetString("lblMonths");
			lblDays=aObjResMang.GetString("lblDays");
		}
		private void FillAgency(string agency_Code)
		{
			DataSet dsAgency = null;
			dsAgency = Cms.BusinessLayer.BlCommon.ClsCommon.GetAgencyFromCombinedCode( GetSystemId(), GetUserId() );
			if(dsAgency!=null && dsAgency.Tables[0].Rows.Count > 0 )
			{
				DataView dvSortView = new DataView(dsAgency.Tables[0]);
				dvSortView.Sort = "AGENCY_DISPLAY_NAME";
				cmbAGENCY_LIST.DataSource = dvSortView;
				cmbAGENCY_LIST.DataTextField="AGENCY_DISPLAY_NAME"; 
				cmbAGENCY_LIST.DataValueField="AGENCY_CODE";
				cmbAGENCY_LIST.DataBind();

				//Select 
				ListItem li=new ListItem();
				li=cmbAGENCY_LIST.Items.FindByValue(agency_Code.ToString());
				if(li!=null)
					li.Selected=true; 

			}
		}
		private void cmbAGENCY_LIST_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetAgencySession();
			getCookie_1();
		}

		private void SetAgencySession()
		{
			string agency_Code = cmbAGENCY_LIST.SelectedValue;
			if(agency_Code!=null && agency_Code!="")
			{
				SetSystemId(agency_Code);
				hidAGENCY_CODE.Value = agency_Code; //Added for Dec page and Auto ID card lookups
				ListItem li=new ListItem();
				li=cmbAGENCY_LIST.Items.FindByValue(agency_Code.ToString());
				if(li!=null)
					li.Selected=true; 
			}

		}

		private void getCookie_1()
		{
			string custName="";
			string appString="";
			string polString="";
			string qqString="";
			string clString="";
			/////////	string claimString="";

			//Done to Remove Cookies and fetch values from database
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastPageVisited = new ClsGeneralInformation();
			DataSet ds = objLastPageVisited.GetLastVisitedPageEntry(int.Parse(GetUserId()),GetSystemId());

		
				if(ds!=null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
					custName=ds.Tables[0].Rows[0]["LAST_VISITED_CUSTOMER"].ToString();

				if(custName!="")
				{
					string [] cookArr = custName.Split(new char[]{'@'});
					string [] custArray_1;
					if(cookArr.Length >= 2)
					{
						string [] custArray=cookArr[0].Split(new char[]{'~'});
						if(custArray.Length>2 )
						{
							string strCustomerPath=Request.ApplicationPath.ToString() + "/client/aspx/customermanagerindex.aspx?customer_id=" + custArray[1].ToString();
							if(CustomerId=="") //Called from main page
							{
								//cookieCustomerName="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/client/aspx/customermanagerindex.aspx?customer_id=" + custArray[1].ToString() + ">" + custArray[0].ToString()   + "</a>";  

								cookieCustomerName_1="<a class=CalLink href=" + strCustomerPath + ">" + ReplaceEntities(custArray[0].ToString())   + "</a>";  
								
							}
							else		//Called from 
							{
								cookieCustomerName_1="<a class=CalLink href=javascript:openCustomerPath('" + strCustomerPath + "')>" + ReplaceEntities(custArray[0].ToString())   + "</a>";  															 
							}
							cookieCustDate_1=custArray[2].ToString().Substring(0,custArray[2].ToString().IndexOf(" "));
						}
					}
					else //Added
					{
						cookieCustomerName_1 = "";
						cookieCustDate_1 = "";
					}

					if(cookArr.Length >= 3)
					{
						//Modified by Asfa(03-July-2008) - iTrack #4434
						custArray_1=cookArr[0].Split(new char[]{'~'});
						string [] custArray=cookArr[1].Split(new char[]{'~'});
						if(custArray.Length>2 && custArray_1[1].ToString() != custArray[1].ToString())
						{
							string strCustomerPath=Request.ApplicationPath.ToString() + "/client/aspx/customermanagerindex.aspx?customer_id=" + custArray[1].ToString();
							if(CustomerId=="") //Called from main page
							{
								//cookieCustomerName="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/client/aspx/customermanagerindex.aspx?customer_id=" + custArray[1].ToString() + ">" + custArray[0].ToString()   + "</a>";  
								cookieCustomerName_2="<a class=CalLink href=" + strCustomerPath + ">" + ReplaceEntities(custArray[0].ToString())   + "</a>";  
								cookieCustomerName_2 = ReplaceEntities(cookieCustomerName_2);
							}
							else		//Called from 
							{
								cookieCustomerName_2="<a class=CalLink href=javascript:openCustomerPath('" + strCustomerPath + "')>" + ReplaceEntities(custArray[0].ToString())   + "</a>";  															 
							}
							cookieCustDate_2=custArray[2].ToString().Substring(0,custArray[2].ToString().IndexOf(" "));
						}
					}
					else //Added
					{
                        cookieCustomerName_2 = "";
						cookieCustDate_2 = "";
					}

					if(cookArr.Length >= 4)
					{
						//Modified by Asfa(03-July-2008) - iTrack #4434
						custArray_1=cookArr[0].Split(new char[]{'~'});
						string [] custArray_2=cookArr[1].Split(new char[]{'~'});
						string [] custArray=cookArr[2].Split(new char[]{'~'});
						if(custArray.Length>2 && custArray_1[1].ToString() != custArray[1].ToString() && custArray_2[1].ToString() != custArray[1].ToString())
						{
							string strCustomerPath=Request.ApplicationPath.ToString() + "/client/aspx/customermanagerindex.aspx?customer_id=" + custArray[1].ToString();
							if(CustomerId=="") //Called from main page
							{
								//cookieCustomerName="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/client/aspx/customermanagerindex.aspx?customer_id=" + custArray[1].ToString() + ">" + custArray[0].ToString()   + "</a>";  
								(cookieCustomerName_3)="<a class=CalLink href=" + strCustomerPath + ">" + ReplaceEntities(custArray[0].ToString())   + "</a>";  
								

								
							}
							else		//Called from 
							{
								cookieCustomerName_3="<a class=CalLink href=javascript:openCustomerPath('" + strCustomerPath + "')>" + ReplaceEntities(custArray[0].ToString())   + "</a>";  															 
							}
							cookieCustDate_3=custArray[2].ToString().Substring(0,custArray[2].ToString().IndexOf(" "));
						}
					}
					else //Added
					{
                        cookieCustomerName_3 = "";
						cookieCustDate_3 = "";
					}
				}
				else
				{
					cookieCustomerName_1="";
					cookieCustDate_1 = "";
					cookieCustomerName_2="";
					cookieCustDate_2 = "";
					cookieCustomerName_3="";
					cookieCustDate_3 = "";
				}
				//////////				//=== Claims Cookie
				//////////			if(Request.Cookies["claimNo" + ((cmsbase)this.Page).GetSystemId() + "_" + ((cmsbase)this.Page).GetUserId()]!=null)
				//////////			{
				//////////				claimString=Request.Cookies["claimNo" + ((cmsbase)this.Page).GetSystemId() + "_" + ((cmsbase)this.Page).GetUserId()].Value ;
				//////////				if(claimString!="")
				//////////				{
				//////////					string [] claimArray=claimString.Split(new char[]{'~'});
				//////////					if(claimArray.Length >6)
				//////////					{	
				//////////						string rentDwe;
				//////////						rentDwe=Server.UrlEncode(claimArray[4].ToString());
				//////////						cookieClaim="<a class=CalLink  href=javascript:openQuickQuote('" + claimArray[1].ToString() + "','" + claimArray[2].ToString() + "','" + claimArray[3].ToString() + "','" + rentDwe + "','" + claimArray[0].ToString() +  "','" + claimArray[5].ToString() + "')>" + claimArray[0].ToString()   + "</a>";
				//////////						cookieClaimDate=claimArray[6].ToString().Substring(0,claimArray[6].ToString().IndexOf(" "));
				//////////					}
				//////////				}
				//////////
				//////////			}
				//////////			else
				//////////			{
				//////////				cookieClaim="";
				//////////			}
				//////////				//===
				////Done to Remove Cookies and fetch values from database
				if(ds!=null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
				{
					qqString=ds.Tables[0].Rows[0]["LAST_VISITED_QUOTE"].ToString();
					if(qqString!="")
					{
						string [] qqArray_1;
						string [] cookArr = qqString.Split(new char[]{'@'});
						if(cookArr.Length >= 2)
						{
							string [] qqArray=cookArr[0].Split(new char[]{'~'});
							if(qqArray.Length >6)
							{
								//int indBracket=qqArray[0].ToString().IndexOf("(");
								// string qqNo=qqArray[0].ToString().Substring(0,indBracket-1);
							
								string rentDwe;
								rentDwe=Server.UrlEncode(qqArray[4].ToString());

								//cookieQQ="<a class=CallLink  href=" + Request.ApplicationPath.ToString() + "/cmsweb/aspx/QuickQuoteLoad.aspx?customer_id=" + qqArray[1].ToString() + "&QQ_ID=" + qqArray[2].ToString() + "&QQ_TYPE=" + qqArray[3].ToString() + "&LOB_DESC=" + qqArray[4].ToString() + "&state_name1=" + qqArray[5].ToString()  + "&QQ_NUMBER=" + qqArray[0].ToString() + ">" + qqArray[0].ToString()   + "</a>";  
								cookieQQ_1="<a class=CalLink  href=javascript:openQuickQuote('" + qqArray[1].ToString() + "','" + qqArray[2].ToString() + "','" + qqArray[3].ToString() + "','" + rentDwe + "','" + qqArray[0].ToString() +  "','" + qqArray[5].ToString() + "')>" + qqArray[0].ToString()   + "</a>";
								//cookieQQ="<a class=CallLink  href=javascript:openQuickQuote('" + qqArray[1].ToString() + "','" + qqArray[2].ToString() + "','" + qqArray[3].ToString() + "','" + qqArray[4].ToString() + "','" + qqArray[5].ToString() + "','" + qqArray[0].ToString() + "')" + ">" + qqArray[0].ToString()   + "</a>";
								cookieQQDate_1=qqArray[6].ToString().Substring(0,qqArray[6].ToString().IndexOf(" "));
							}
						}
						else //Added
						{
							cookieQQ_1 = "";
							cookieQQDate_1 = "";
						}
						if(cookArr.Length >= 3)
						{
							//Modified by Asfa(03-July-2008) - iTrack #4434
							qqArray_1=cookArr[0].Split(new char[]{'~'});
							string [] qqArray=cookArr[1].Split(new char[]{'~'});
							if(qqArray.Length >6 && qqArray_1[0].ToString() != qqArray[0].ToString())
							{
								//int indBracket=qqArray[0].ToString().IndexOf("(");
								// string qqNo=qqArray[0].ToString().Substring(0,indBracket-1);
							
								string rentDwe;
								rentDwe=Server.UrlEncode(qqArray[4].ToString());

								//cookieQQ="<a class=CallLink  href=" + Request.ApplicationPath.ToString() + "/cmsweb/aspx/QuickQuoteLoad.aspx?customer_id=" + qqArray[1].ToString() + "&QQ_ID=" + qqArray[2].ToString() + "&QQ_TYPE=" + qqArray[3].ToString() + "&LOB_DESC=" + qqArray[4].ToString() + "&state_name1=" + qqArray[5].ToString()  + "&QQ_NUMBER=" + qqArray[0].ToString() + ">" + qqArray[0].ToString()   + "</a>";  
								cookieQQ_2="<a class=CalLink  href=javascript:openQuickQuote('" + qqArray[1].ToString() + "','" + qqArray[2].ToString() + "','" + qqArray[3].ToString() + "','" + rentDwe + "','" + qqArray[0].ToString() +  "','" + qqArray[5].ToString() + "')>" + qqArray[0].ToString()   + "</a>";
								//cookieQQ="<a class=CallLink  href=javascript:openQuickQuote('" + qqArray[1].ToString() + "','" + qqArray[2].ToString() + "','" + qqArray[3].ToString() + "','" + qqArray[4].ToString() + "','" + qqArray[5].ToString() + "','" + qqArray[0].ToString() + "')" + ">" + qqArray[0].ToString()   + "</a>";
								cookieQQDate_2=qqArray[6].ToString().Substring(0,qqArray[6].ToString().IndexOf(" "));
							}
						}
						else
						{
							cookieQQ_2 = "";
							cookieQQDate_2 = "";                            
						}
						if(cookArr.Length >= 4)
						{
							//Modified by Asfa(03-July-2008) - iTrack #4434
							qqArray_1=cookArr[0].Split(new char[]{'~'});
							string [] qqArray_2=cookArr[1].Split(new char[]{'~'});
							string [] qqArray=cookArr[2].Split(new char[]{'~'});
							if(qqArray.Length >6 && qqArray_1[0].ToString() != qqArray[0].ToString() && qqArray_2[0].ToString() != qqArray[0].ToString())
							{
								//int indBracket=qqArray[0].ToString().IndexOf("(");
								// string qqNo=qqArray[0].ToString().Substring(0,indBracket-1);
							
								string rentDwe;
								rentDwe=Server.UrlEncode(qqArray[4].ToString());

								//cookieQQ="<a class=CallLink  href=" + Request.ApplicationPath.ToString() + "/cmsweb/aspx/QuickQuoteLoad.aspx?customer_id=" + qqArray[1].ToString() + "&QQ_ID=" + qqArray[2].ToString() + "&QQ_TYPE=" + qqArray[3].ToString() + "&LOB_DESC=" + qqArray[4].ToString() + "&state_name1=" + qqArray[5].ToString()  + "&QQ_NUMBER=" + qqArray[0].ToString() + ">" + qqArray[0].ToString()   + "</a>";  
								cookieQQ_3="<a class=CalLink  href=javascript:openQuickQuote('" + qqArray[1].ToString() + "','" + qqArray[2].ToString() + "','" + qqArray[3].ToString() + "','" + rentDwe + "','" + qqArray[0].ToString() +  "','" + qqArray[5].ToString() + "')>" + qqArray[0].ToString()   + "</a>";
								//cookieQQ="<a class=CallLink  href=javascript:openQuickQuote('" + qqArray[1].ToString() + "','" + qqArray[2].ToString() + "','" + qqArray[3].ToString() + "','" + qqArray[4].ToString() + "','" + qqArray[5].ToString() + "','" + qqArray[0].ToString() + "')" + ">" + qqArray[0].ToString()   + "</a>";
								cookieQQDate_3=qqArray[6].ToString().Substring(0,qqArray[6].ToString().IndexOf(" "));
							}
						}
						else  //Added
						{
							cookieQQ_3 = "";
							cookieQQDate_3 = "";
						}
					}
					else //aded
					{
						cookieQQ_1="";
						cookieQQDate_1 = "";
						cookieQQ_2="";
						cookieQQDate_2 = "";
						cookieQQ_3="";
						cookieQQDate_2 = "";
					}

				}
				else
				{
					cookieQQ_1="";
					cookieQQDate_1 = "";
					cookieQQ_2="";
					cookieQQDate_2 = "";
					cookieQQ_3="";
					cookieQQDate_2 = "";
				}
				//Done to Remove Cookies and fetch values from database
				if(ds!=null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
					polString=ds.Tables[0].Rows[0]["LAST_VISITED_POLICY"].ToString();
				if(polString!="")
				{
					string [] polarray_1;
					string [] cookArr = polString.Split(new char[]{'@'});
					if(cookArr.Length >= 2)
					{
						string [] polarray=cookArr[0].Split(new char[]{'~'});
						if(polarray.Length>7)
						{
							string strPolicyPath=Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?";
							string strCust= polarray[1].ToString().Trim();
							string strPolId=polarray[2].ToString().Trim();					
							string strPolVer=polarray[3].ToString().Trim();					
							string strApp=polarray[4].ToString().Trim();
							string strAppVer=polarray[5].ToString().Trim();					
							string strPolLOB=polarray[6].ToString().Trim();
						
							if(CustomerId=="")
								cookiePolicy_1="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?customer_id=" + polarray[1].ToString() + "&POLICY_ID=" + polarray[2].ToString() + "&APP_ID=" + polarray[4].ToString() + "&APP_VERSION_ID=" + polarray[5].ToString() +  "&POLICY_LOB=" + polarray[6].ToString() + "&POLICY_VERSION_ID=" + polarray[3].ToString() + ">" + polarray[0].ToString() + "</a>";    
							else
								cookiePolicy_1="<a class=CallLink  href=javascript:OpenPolicyPath('" + strPolicyPath + "','" + strCust + "','" +  strPolId + "','" + strApp + "','" + strAppVer + "','" + strPolLOB + "','" + strPolVer + "')>" + polarray[0].ToString()   + "</a>";
							//cookiePolicy="<a class=CalLink href=" + strPolicyPath?customer_id=" + + "&POLICY_ID=" + strPolicyID + "&APP_ID=" +  + "&APP_VERSION_ID=" +  +  "&POLICY_LOB=" +  + "&POLICY_VERSION_ID=" +  + ">" + polarray[0].ToString() + "</a>";    
							cookiePolDate_1=polarray[7].ToString().Substring(0,polarray[7].ToString().IndexOf(" ")); 
						}
					}
					if(cookArr.Length >= 3)
					{
						//Modified by Asfa(03-July-2008) - iTrack #4434
						polarray_1=cookArr[0].Split(new char[]{'~'});
						string [] polarray=cookArr[1].Split(new char[]{'~'});
						if(polarray.Length>7 && polarray_1[0].ToString() != polarray[0].ToString())
						{
							string strPolicyPath=Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?";
							string strCust= polarray[1].ToString().Trim();
							string strPolId=polarray[2].ToString().Trim();					
							string strPolVer=polarray[3].ToString().Trim();					
							string strApp=polarray[4].ToString().Trim();
							string strAppVer=polarray[5].ToString().Trim();					
							string strPolLOB=polarray[6].ToString().Trim();
						
							if(CustomerId=="")
								cookiePolicy_2="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?customer_id=" + polarray[1].ToString() + "&POLICY_ID=" + polarray[2].ToString() + "&APP_ID=" + polarray[4].ToString() + "&APP_VERSION_ID=" + polarray[5].ToString() +  "&POLICY_LOB=" + polarray[6].ToString() + "&POLICY_VERSION_ID=" + polarray[3].ToString() + ">" + polarray[0].ToString() + "</a>";    
							else
								cookiePolicy_2="<a class=CallLink  href=javascript:OpenPolicyPath('" + strPolicyPath + "','" + strCust + "','" +  strPolId + "','" + strApp + "','" + strAppVer + "','" + strPolLOB + "','" + strPolVer + "')>" + polarray[0].ToString()   + "</a>";
							//cookiePolicy="<a class=CalLink href=" + strPolicyPath?customer_id=" + + "&POLICY_ID=" + strPolicyID + "&APP_ID=" +  + "&APP_VERSION_ID=" +  +  "&POLICY_LOB=" +  + "&POLICY_VERSION_ID=" +  + ">" + polarray[0].ToString() + "</a>";    
							cookiePolDate_2=polarray[7].ToString().Substring(0,polarray[7].ToString().IndexOf(" ")); 
						}
					}
					if(cookArr.Length >= 4)
					{
						//Modified by Asfa(03-July-2008) - iTrack #4434
						polarray_1=cookArr[0].Split(new char[]{'~'});
						string [] polarray_2=cookArr[1].Split(new char[]{'~'});
						string [] polarray=cookArr[2].Split(new char[]{'~'});
						if(polarray.Length>7 && polarray_1[0].ToString() != polarray[0].ToString() && polarray_2[0].ToString() != polarray[0].ToString())
						{
							string strPolicyPath=Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?";
							string strCust= polarray[1].ToString().Trim();
							string strPolId=polarray[2].ToString().Trim();					
							string strPolVer=polarray[3].ToString().Trim();					
							string strApp=polarray[4].ToString().Trim();
							string strAppVer=polarray[5].ToString().Trim();					
							string strPolLOB=polarray[6].ToString().Trim();
						
							if(CustomerId=="")
								cookiePolicy_3="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?customer_id=" + polarray[1].ToString() + "&POLICY_ID=" + polarray[2].ToString() + "&APP_ID=" + polarray[4].ToString() + "&APP_VERSION_ID=" + polarray[5].ToString() +  "&POLICY_LOB=" + polarray[6].ToString() + "&POLICY_VERSION_ID=" + polarray[3].ToString() + ">" + polarray[0].ToString() + "</a>";    
							else
								cookiePolicy_3="<a class=CallLink  href=javascript:OpenPolicyPath('" + strPolicyPath + "','" + strCust + "','" +  strPolId + "','" + strApp + "','" + strAppVer + "','" + strPolLOB + "','" + strPolVer + "')>" + polarray[0].ToString()   + "</a>";
							//cookiePolicy="<a class=CalLink href=" + strPolicyPath?customer_id=" + + "&POLICY_ID=" + strPolicyID + "&APP_ID=" +  + "&APP_VERSION_ID=" +  +  "&POLICY_LOB=" +  + "&POLICY_VERSION_ID=" +  + ">" + polarray[0].ToString() + "</a>";    
							cookiePolDate_3=polarray[7].ToString().Substring(0,polarray[7].ToString().IndexOf(" ")); 
						}
					}
				}
				else
				{
					cookiePolicy_1="";
					cookiePolDate_1 = "";
					cookiePolicy_2="";
					cookiePolDate_2 ="";
					cookiePolicy_3="";
					cookiePolDate_3 = "";

				}



				//Added By Shafi For Application Status
				//Done to Remove Cookies and fetch values from database	 
				if(ds!=null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
					appString=ds.Tables[0].Rows[0]["LAST_VISITED_APPLICATION"].ToString();

				if(appString!="")
				{
					string [] cookArr = appString.Split(new char[]{'@'});
					if(cookArr.Length >= 2)
					{
						string [] appArray=cookArr[0].Split(new char[]{'~'});
						int deleteStatus=ApplicationStatus(int.Parse (appArray[1].ToString()) , int.Parse (appArray[2].ToString()) , int.Parse(appArray[3].ToString()));
						if (deleteStatus ==1)
						{
							if(appArray.Length>4)
							{
								//string strApplicationPath=Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?customer_id=" + appArray[2].ToString() +  "&app_id=" + appArray[2].ToString() + "&app_version_id=" + appArray[3].ToString();
								string strApplicationPath=Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?";
								string strCust = appArray[1].ToString().Trim();
								string strApp = appArray[2].ToString().Trim();
								string strAppVer = appArray[3].ToString().Trim();
								
								if(CustomerId=="")	//called from main page
									cookieApplication_1="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?customer_id=" + appArray[1].ToString() + "&app_id=" + appArray[2].ToString() + "&app_version_id=" + appArray[3].ToString() + ">" + appArray[0].ToString() + "</a>";      
								else
									cookieApplication_1="<a class=CallLink  href=javascript:OpenApplicationPath('" + strApplicationPath + "','" + strCust + "','" +  strApp + "','" + strAppVer + "')>" + appArray[0].ToString()   + "</a>";

							
								//							cookieApplication="<a class=CallLink  href=javascript:openQuickQuote('" + appArray[1].ToString() + "','" + appArray[2].ToString() + "','" + appArray[3].ToString() + "','" + appArray[1].ToString() + "','" + appArray[1].ToString() +  "','" + appArray[1].ToString() + "')>" + appArray[0].ToString()   + "</a>";
								//						cookieApplication="<a class=CallLink  href=javascript:OpenApplicationPath('" + strApplicationPath + "','" + strCust + "','" +  strApp + "','" + strAppVer + "')>" + appArray[0].ToString()   + "</a>";
								//cookieApplication="<a class=CalLink href=javascript:OpenApplicationPath('" +  appArray[1].ToString() + "','" +  appArray[2].ToString() + "','" +  appArray[3].ToString() + "')>" + appArray[0].ToString() + "</a>";      
												
								cookieAppDate_1=appArray[4].ToString().Substring(0,appArray[4].ToString().IndexOf(" ")); 
							}
						}
						else
						{
							//cookieApplication= appArray[0].ToString();
							cookieApplication_1= "";
                            cookieAppDate_1 = appArray[7].ToString().Substring(0,15); 
						
						}
					}
					if(cookArr.Length >= 3)
					{
						string [] appArray_1=cookArr[0].Split(new char[]{'~'});
						string [] appArray=cookArr[1].Split(new char[]{'~'});
						int deleteStatus=ApplicationStatus(int.Parse (appArray[1].ToString()) , int.Parse (appArray[2].ToString()) , int.Parse(appArray[3].ToString()));
						if (deleteStatus ==1)
						{
							if(appArray.Length>4 && appArray_1[0].ToString() != appArray[0].ToString())
							{
								//string strApplicationPath=Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?customer_id=" + appArray[2].ToString() +  "&app_id=" + appArray[2].ToString() + "&app_version_id=" + appArray[3].ToString();
								string strApplicationPath=Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?";
								string strCust = appArray[1].ToString().Trim();
								string strApp = appArray[2].ToString().Trim();
								string strAppVer = appArray[3].ToString().Trim();
								
								if(CustomerId=="")	//called from main page
									cookieApplication_2="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?customer_id=" + appArray[1].ToString() + "&app_id=" + appArray[2].ToString() + "&app_version_id=" + appArray[3].ToString() + ">" + appArray[0].ToString() + "</a>";      
								else
									cookieApplication_2="<a class=CallLink  href=javascript:OpenApplicationPath('" + strApplicationPath + "','" + strCust + "','" +  strApp + "','" + strAppVer + "')>" + appArray[0].ToString()   + "</a>";

							
								//							cookieApplication="<a class=CallLink  href=javascript:openQuickQuote('" + appArray[1].ToString() + "','" + appArray[2].ToString() + "','" + appArray[3].ToString() + "','" + appArray[1].ToString() + "','" + appArray[1].ToString() +  "','" + appArray[1].ToString() + "')>" + appArray[0].ToString()   + "</a>";
								//						cookieApplication="<a class=CallLink  href=javascript:OpenApplicationPath('" + strApplicationPath + "','" + strCust + "','" +  strApp + "','" + strAppVer + "')>" + appArray[0].ToString()   + "</a>";
								//cookieApplication="<a class=CalLink href=javascript:OpenApplicationPath('" +  appArray[1].ToString() + "','" +  appArray[2].ToString() + "','" +  appArray[3].ToString() + "')>" + appArray[0].ToString() + "</a>";      
												
								cookieAppDate_2=appArray[4].ToString().Substring(0,appArray[4].ToString().IndexOf(" ")); 
							}
						}
						else
						{
							//cookieApplication= appArray[0].ToString();
							cookieApplication_2= "";
							cookieAppDate_2=appArray[4].ToString().Substring(0,appArray[4].ToString().IndexOf(" ")); 
						
						}
					}
					if(cookArr.Length >= 4)
					{
						string [] appArray_1=cookArr[0].Split(new char[]{'~'});
						string [] appArray_2=cookArr[1].Split(new char[]{'~'});
						string [] appArray=cookArr[2].Split(new char[]{'~'});
						int deleteStatus=ApplicationStatus(int.Parse (appArray[1].ToString()) , int.Parse (appArray[2].ToString()) , int.Parse(appArray[3].ToString()));
						if (deleteStatus ==1)
						{
							if(appArray.Length>4 && appArray_1[0].ToString() != appArray[0].ToString() && appArray_2[0].ToString() != appArray[0].ToString())
							{
								//string strApplicationPath=Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?customer_id=" + appArray[2].ToString() +  "&app_id=" + appArray[2].ToString() + "&app_version_id=" + appArray[3].ToString();
								string strApplicationPath=Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?";
								string strCust = appArray[1].ToString().Trim();
								string strApp = appArray[2].ToString().Trim();
								string strAppVer = appArray[3].ToString().Trim();
								
								if(CustomerId=="")	//called from main page
									cookieApplication_3="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?customer_id=" + appArray[1].ToString() + "&app_id=" + appArray[2].ToString() + "&app_version_id=" + appArray[3].ToString() + ">" + appArray[0].ToString() + "</a>";      
								else
									cookieApplication_3="<a class=CallLink  href=javascript:OpenApplicationPath('" + strApplicationPath + "','" + strCust + "','" +  strApp + "','" + strAppVer + "')>" + appArray[0].ToString()   + "</a>";

							
								//							cookieApplication="<a class=CallLink  href=javascript:openQuickQuote('" + appArray[1].ToString() + "','" + appArray[2].ToString() + "','" + appArray[3].ToString() + "','" + appArray[1].ToString() + "','" + appArray[1].ToString() +  "','" + appArray[1].ToString() + "')>" + appArray[0].ToString()   + "</a>";
								//						cookieApplication="<a class=CallLink  href=javascript:OpenApplicationPath('" + strApplicationPath + "','" + strCust + "','" +  strApp + "','" + strAppVer + "')>" + appArray[0].ToString()   + "</a>";
								//cookieApplication="<a class=CalLink href=javascript:OpenApplicationPath('" +  appArray[1].ToString() + "','" +  appArray[2].ToString() + "','" +  appArray[3].ToString() + "')>" + appArray[0].ToString() + "</a>";      
												
								cookieAppDate_3=appArray[4].ToString().Substring(0,appArray[4].ToString().IndexOf(" ")); 
							}
						}
						else
						{
							//cookieApplication= appArray[0].ToString();
							cookieApplication_3= "";
							cookieAppDate_3=appArray[4].ToString().Substring(0,appArray[4].ToString().IndexOf(" ")); 
						
						}
					}
				}
				else
				{
					cookieApplication_1="";
					cookieAppDate_1 = "";
					cookieApplication_2="";
					cookieAppDate_2 = "";
					cookieApplication_3="";
					cookieAppDate_3 = "";
				}
                
				#region RECENTLY ACCESSED CLAIM

				//Added By Anurag Verma on 20/07/2006 For last visited Claim 
				////Done to Remove Cookies and fetch values from database 
				if(ds!=null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
					clString=ds.Tables[0].Rows[0]["LAST_VISITED_CLAIM"].ToString();

				if(clString!="")
				{
					string [] cookArr = clString.Split(new char[]{'@'});
					if(cookArr.Length >= 2)
					{
						string [] claimarray=cookArr[0].Split(new char[]{'~'});
						if(claimarray.Length>=7)
						{
							string strClaimPath=Request.ApplicationPath.ToString() + "/claims/aspx/ClaimsTab.aspx?";
							string strCust= claimarray[1].ToString().Trim();
							string strPolId=claimarray[2].ToString().Trim();					
							string strPolVer=claimarray[3].ToString().Trim();					
							string strClaimID=claimarray[4].ToString().Trim();					
							string strLOB=claimarray[5].ToString().Trim();
						
							//if(CustomerId=="")
							//	cookieClaim="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/claims/aspx/ClaimsTab.aspx?CUSTOMER_ID=" + claimarray[1].ToString() + "&POLICY_ID=" + claimarray[2].ToString() + "&CLAIM_ID=" + claimarray[4].ToString() +  "&LOB_ID=" + claimarray[5].ToString() + "&POLICY_VERSION_ID=" + claimarray[3].ToString() + ">" + claimarray[0].ToString() + "</a>";    
							//else
							cookieClaim_1="<a class=CalLink  href=javascript:OpenClaimPath('" + strClaimPath + "','" + strCust + "','" +  strPolId + "','" + strClaimID + "','" + strLOB + "','" + strPolVer + "')>" + claimarray[0].ToString()   + "</a>";
 
					
							cookieClaimDate_1=claimarray[6].ToString().Substring(0,claimarray[6].ToString().IndexOf(" ")); 
						}
					}
					if(cookArr.Length >= 3)
					{
						string [] claimarray_1=cookArr[0].Split(new char[]{'~'});
						string [] claimarray=cookArr[1].Split(new char[]{'~'});
						if(claimarray.Length>=7 && claimarray_1[0].ToString() != claimarray[0].ToString())
						{
							string strClaimPath=Request.ApplicationPath.ToString() + "/claims/aspx/ClaimsTab.aspx?";
							string strCust= claimarray[1].ToString().Trim();
							string strPolId=claimarray[2].ToString().Trim();					
							string strPolVer=claimarray[3].ToString().Trim();					
							string strClaimID=claimarray[4].ToString().Trim();					
							string strLOB=claimarray[5].ToString().Trim();
						
							//if(CustomerId=="")
							//	cookieClaim="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/claims/aspx/ClaimsTab.aspx?CUSTOMER_ID=" + claimarray[1].ToString() + "&POLICY_ID=" + claimarray[2].ToString() + "&CLAIM_ID=" + claimarray[4].ToString() +  "&LOB_ID=" + claimarray[5].ToString() + "&POLICY_VERSION_ID=" + claimarray[3].ToString() + ">" + claimarray[0].ToString() + "</a>";    
							//else
							cookieClaim_2="<a class=CalLink  href=javascript:OpenClaimPath('" + strClaimPath + "','" + strCust + "','" +  strPolId + "','" + strClaimID + "','" + strLOB + "','" + strPolVer + "')>" + claimarray[0].ToString()   + "</a>";
 
					
							cookieClaimDate_2=claimarray[6].ToString().Substring(0,claimarray[6].ToString().IndexOf(" ")); 
						}
					}
					if(cookArr.Length >= 4)
					{
						string [] claimarray_1=cookArr[0].Split(new char[]{'~'});
						string [] claimarray_2=cookArr[1].Split(new char[]{'~'});
						string [] claimarray=cookArr[2].Split(new char[]{'~'});
						if(claimarray.Length>=7 && claimarray_1[0].ToString() != claimarray[0].ToString() && claimarray_2[0].ToString() != claimarray[0].ToString())
						{
							string strClaimPath=Request.ApplicationPath.ToString() + "/claims/aspx/ClaimsTab.aspx?";
							string strCust= claimarray[1].ToString().Trim();
							string strPolId=claimarray[2].ToString().Trim();					
							string strPolVer=claimarray[3].ToString().Trim();					
							string strClaimID=claimarray[4].ToString().Trim();					
							string strLOB=claimarray[5].ToString().Trim();
						
							//if(CustomerId=="")
							//	cookieClaim="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/claims/aspx/ClaimsTab.aspx?CUSTOMER_ID=" + claimarray[1].ToString() + "&POLICY_ID=" + claimarray[2].ToString() + "&CLAIM_ID=" + claimarray[4].ToString() +  "&LOB_ID=" + claimarray[5].ToString() + "&POLICY_VERSION_ID=" + claimarray[3].ToString() + ">" + claimarray[0].ToString() + "</a>";    
							//else
							cookieClaim_3="<a class=CalLink  href=javascript:OpenClaimPath('" + strClaimPath + "','" + strCust + "','" +  strPolId + "','" + strClaimID + "','" + strLOB + "','" + strPolVer + "')>" + claimarray[0].ToString()   + "</a>";
 
					
							cookieClaimDate_3=claimarray[6].ToString().Substring(0,claimarray[6].ToString().IndexOf(" ")); 
						}
					}
				}
				else
				{
					cookieClaim_1="";
					cookieClaimDate_1 = "";
					cookieClaim_2="";
					cookieClaimDate_2 = "";
					cookieClaim_3="";
					cookieClaimDate_3 = "";
				}
			
			#endregion
		}

		private int ApplicationStatus(int customerId,int appId,int appVersion)
		{
			string applicationStatus=@ClsGeneralInformation.FetchApplicationXML(customerId,appId,appVersion );
			if(applicationStatus=="")
				return 0;
			else
				return 1;

		}
		private string ReplaceEntities(string str)
		{
			str  = str.Replace("&","&amp;");
			str  = str.Replace("\"","&quot;");	
			return str;
		}

		public string CustomerId
		{
			get
			{
				return strCustomerId;
			}
			set
			{
				strCustomerId = value;
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
			this.cmbAGENCY_LIST.SelectedIndexChanged += new System.EventHandler(this.cmbAGENCY_LIST_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
