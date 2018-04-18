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
	<Author					: - >
	<Start Date				: -	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 31/03/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Change in ascx file -- changing cellpadding=0 and cellspacing=0
    

	<Modified Date			: - > 08/04/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > enabling search on date click 
*******************************************************************************************/
namespace Cms.CmsWeb.WebControls
{
	/// <summary>
	///		Summary description for DiaryCalendar.
	/// </summary>
	public class DiaryCalendar : System.Web.UI.UserControl
	{
		protected string gStrStyle="css1.css", dtFormat;
		protected string gStrAppDates="";
		protected string gstruserid="";
		protected string lblMonths="";
		protected string lblDays="";
		public string gStrAPQC="0",gStrQPAC="0",gStrQPBC="0",gStrEEC="0",gStrRRC="0",gStrCR="0",gStrReinstate="0",gstrClmMvmnt="0",gStrBRC="0",gStrERC="0",gStrAAC="0";
		public string gStrReinstHeading,gStrCRHeading,gstrshoall,gstrAPQ,gstrQPA,gstrQPB,gstrEE,gstrRR,gstrBR,gstrER,gstrAA, gstrToday,gstrAppoint,gstrClmHeading,aAppCountry;
		protected string lStrStyleSheetName="";
		protected string listtypeid1,listtypeid2,listtypeid3,listtypeid4,listtypeid5,listtypeid6,listtypeid7,listtypeid8,listtypeid9,listtypeid10;
		public string cookieCustomerName="";
		public string cookieApplication="";
		public string cookiePolicy="";
		public string cookieCustDate="";
		public string cookieQQ="";
		public string cookieClaim="";
		public string cookieClaimDate="";
		//public string cookieClaim="";
		public string cookieQQDate="";
		//public string cookieClaimDate="";
		public string cookieQQSate="";
		public string cookieAppDate="";
		public string cookiePolDate="";
		//object reference of ResourceManager Class
		protected ResourceManager aObjResMang;

        public string strCF,strANF,strCRE;
        public string cStrCF,cStrANF,cStrCRE;
        protected string iLangId, strSystemID = "";
        public string strPT, strRV, strCustomer, strPolicy, strApplication, strClaim, strOn, strQuickAPP;//Added by Charles on 11-Mar-10 for Multilingual Implementation

        private string strColorScheme, strUserId, strCustomerId = "";
       

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			lStrStyleSheetName = ((cmsbase)this.Page).GetColorScheme();

            strSystemID = ((cmsbase)this.Page).GetSystemId();

            cStrCF=cStrANF=cStrCRE="0";

			//Set static variables from resource manager
			SetVariables();
			
			//Set count variables from database based on todolist type
			SetCountVariables();

			//Set diary date variable
			gStrAppDates=SetDiaryDates();

			getCookie();
            iLangId = Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID.ToString();
            
		}

		private void getCookie()
		{
			string custName="";
			//string custID="";
			string appString="";
			string polString="";
			string qqString="";
			string clString="";
			cmsbase obj = new cmsbase();
		    Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastPageVisited = new ClsGeneralInformation();
			DataSet ds = objLastPageVisited.GetLastVisitedPageEntry(int.Parse(obj.GetUserId()),obj.GetSystemId());
			if(ds!=null && ds.Tables[0].Rows.Count > 0)
			{					

				//if(Request.Cookies["customerName" + ((cmsbase)this.Page).GetSystemId()+ "_" + ((cmsbase)this.Page).GetUserId()] !=null)
				//custName=Request.Cookies["customerName" + ((cmsbase)this.Page).GetSystemId().ToString() + "_" + ((cmsbase)this.Page).GetUserId().ToString()].Value;

				if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
					custName=ds.Tables[0].Rows[0]["LAST_VISITED_CUSTOMER"].ToString();

				if(custName!="")
				{
					string [] custArray=custName.Split(new char[]{'~'});
					if(custArray.Length>2 )
					{
						string strCustomerPath=Request.ApplicationPath.ToString() + "/client/aspx/customermanagerindex.aspx?customer_id=" + custArray[1].ToString();
						if(CustomerId=="") //Called from main page
						{
							//cookieCustomerName="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/client/aspx/customermanagerindex.aspx?customer_id=" + custArray[1].ToString() + ">" + custArray[0].ToString()   + "</a>";  
							//custArray[0] = 
							//cookieCustomerName="<a class=CalLink href=" + strCustomerPath + ">" + ReplaceEntities(custArray[0].ToString())   + "</a>";
                            cookieCustomerName = "<a class=CalLink href=javascript:openCustomerPath('','" + custArray[1].ToString() + "')>" + ReplaceEntities(custArray[0].ToString()) + "</a>";  															 
							
						}
						else		//Called from 
						{
                            cookieCustomerName = "<a class=CalLink href=javascript:openCustomerPath('','" + custArray[1].ToString() + "')>" + ReplaceEntities(custArray[0].ToString()) + "</a>";  															 
						}
                        if (ReplaceEntities(custArray[0].ToString()).Trim() != "")
						    cookieCustDate=custArray[2].ToString().Substring(0,custArray[2].ToString().IndexOf(" "));
					}
				}
				else
				{
					cookieCustomerName="";
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
				//				if(Request.Cookies["qqno" + ((cmsbase)this.Page).GetSystemId() + "_" + ((cmsbase)this.Page).GetUserId()]!=null)
				//				{
				//					qqString=Request.Cookies["qqno" + ((cmsbase)this.Page).GetSystemId() + "_" + ((cmsbase)this.Page).GetUserId()].Value ;
				if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
				{
					qqString=ds.Tables[0].Rows[0]["LAST_VISITED_QUOTE"].ToString();

					if(qqString!="")
					{
						string [] qqArray=qqString.Split(new char[]{'~'});
						if(qqArray.Length >0)//>6) //Changed by Charles on 17-Jun-10 for Quick App Implementation
						{							
							/*Commented by Charles on 17-Jun-10 for Quick App Implementation
							string rentDwe;
							rentDwe=Server.UrlEncode(qqArray[4].ToString());
                             */
                            if (qqArray.Length == 5)
                            {
                            
                                cookieQQ = "<a class=CalLink  href=javascript:openQuickApp('" + qqArray[0].ToString().Trim() + "','" + qqArray[1].ToString().Trim() + "','" + qqArray[2].ToString().Trim() + "')> " + qqArray[4].ToString() + " </a>";
                            }
                            else
                            {
                                 cookieQQ = "<a class=CalLink  href=javascript:openQuickApp('" + qqArray[0].ToString().Trim() + "','" + qqArray[1].ToString().Trim() + "','" + qqArray[2].ToString().Trim() + "')> " + strQuickAPP + " </a>";
                            }

                           
                            //Commented by Charles on 17-Jun-10 for Quick App Implementation
							//cookieQQ="<a class=CalLink  href=javascript:openQuickQuote('" + qqArray[1].ToString().Trim() + "','" + qqArray[2].ToString().Trim() + "','" + qqArray[3].ToString().Trim() + "','" + rentDwe + "','" + qqArray[0].ToString().Trim() +  "','" + qqArray[5].ToString().Trim() + "')>" + qqArray[0].ToString()   + "</a>";
                            

                            //Commented by Charles on 17-Jun-10 for Quick App Implementation
							//cookieQQDate=qqArray[6].ToString().Substring(0,qqArray[6].ToString().IndexOf(" "));
                            cookieQQDate = qqArray[3].ToString().Substring(0, qqArray[3].ToString().IndexOf(" "));
						}
					}

				}
				else
				{
					cookieQQ="";
				}
				if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
					polString=ds.Tables[0].Rows[0]["LAST_VISITED_POLICY"].ToString();
				if(polString!="")
				{
					string [] polarray=polString.Split(new char[]{'~'});
					if(polarray.Length>7)
					{
						string strPolicyPath=Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?";
						string strCust= polarray[1].ToString().Trim();
						string strPolId=polarray[2].ToString().Trim();					
						string strPolVer=polarray[3].ToString().Trim();					
						string strApp=polarray[4].ToString().Trim();
						string strAppVer=polarray[5].ToString().Trim();					
						string strPolLOB=polarray[6].ToString().Trim();

                        if (CustomerId == "")
                            //cookiePolicy="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?customer_id=" + polarray[1].ToString().Trim() + "&POLICY_ID=" + polarray[2].ToString().Trim() + "&APP_ID=" + polarray[4].ToString().Trim() + "&APP_VERSION_ID=" + polarray[5].ToString().Trim() +  "&POLICY_LOB=" + polarray[6].ToString().Trim() + "&POLICY_VERSION_ID=" + polarray[3].ToString().Trim() + ">" + polarray[0].ToString() + "</a>";//Done for Itrack Issue 6133 on 25 Aug 2009 
                            //cookiePolicy="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?customer_id=" + polarray[1].ToString() + "&POLICY_ID=" + polarray[2].ToString() + "&APP_ID=" + polarray[4].ToString() + "&APP_VERSION_ID=" + polarray[5].ToString() +  "&POLICY_LOB=" + polarray[6].ToString().Trim() + "&POLICY_VERSION_ID=" + polarray[3].ToString() + ">" + polarray[0].ToString() + "</a>";
                            cookiePolicy = "<a class=CallLink  href=javascript:OpenPolicyPath('" + strPolicyPath + "','" + strCust + "','" + strPolId + "','" + strApp + "','" + strAppVer + "','" + strPolLOB + "','" + strPolVer + "')>" + polarray[0].ToString() + "</a>";
                        else
                            cookiePolicy = "<a class=CallLink  href=javascript:OpenPolicyPath('" + strPolicyPath + "','" + strCust + "','" + strPolId + "','" + strApp + "','" + strAppVer + "','" + strPolLOB + "','" + strPolVer + "')>" + polarray[0].ToString() + "</a>";
                            //cookiePolicy="<a class=CalLink href=" + strPolicyPath?customer_id=" + + "&POLICY_ID=" + strPolicyID + "&APP_ID=" +  + "&APP_VERSION_ID=" +  +  "&POLICY_LOB=" +  + "&POLICY_VERSION_ID=" +  + ">" + polarray[0].ToString() + "</a>";    

                        if (polarray[0].ToString() != "")
                            cookiePolDate = polarray[7].ToString().Substring(0, polarray[7].ToString().IndexOf(" ")); 
						
					}
				}
				else
				{
					cookiePolicy="";
				}



				//Added By Shafi For Application Status
				 
				if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
					appString=ds.Tables[0].Rows[0]["LAST_VISITED_APPLICATION"].ToString();

				if(appString!="")
				{
					string [] appArray=appString.Split(new char[]{'~'});  
					//int deleteStatus=ApplicationStatus(int.Parse (appArray[1].ToString()) , int.Parse (appArray[2].ToString()) , int.Parse(appArray[3].ToString()));
                    //if (deleteStatus ==1)
                    //{
                        if(appArray.Length>7)
						{
							//string strApplicationPath=Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?customer_id=" + appArray[2].ToString() +  "&app_id=" + appArray[2].ToString() + "&app_version_id=" + appArray[3].ToString();
                            string strApplicationPath = Request.ApplicationPath.ToString() + "/Policies/aspx/PolicyTab.aspx?";
							string strCust = appArray[1].ToString().Trim();
                            //string strApp = appArray[2].ToString().Trim();
                            //string strAppVer = appArray[3].ToString().Trim();
                            string strPolId = appArray[2].ToString().Trim();
                            string strPolVer = appArray[3].ToString().Trim();
                            
                            string strApp = appArray[4].ToString().Trim();
                            string strAppVer = appArray[5].ToString().Trim();
                            //string strPolLOB = appArray[6].ToString().Trim();

							if(CustomerId=="")	//called from main page
								//cookieApplication="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?customer_id=" + appArray[1].ToString().Trim() + "&app_id=" + appArray[2].ToString().Trim() + "&app_version_id=" + appArray[3].ToString().Trim() + ">" + appArray[0].ToString().Trim() + "</a>";//Done for Itrack Issue 6133 on 25 Aug 2009
								//cookieApplication="<a class=CalLink href=" + Request.ApplicationPath.ToString() + "/application/aspx/applicationtab.aspx?customer_id=" + appArray[1].ToString()+ "&app_id=" + appArray[2].ToString()+ "&app_version_id=" + appArray[3].ToString() + ">" + appArray[0].ToString() + "</a>";      
                                cookieApplication = "<a class=CallLink  href=javascript:OpenApplicationPath('" + strApplicationPath + "','" + strCust + "','" + strPolId + "','" + strPolVer + "')>" + appArray[0].ToString() + "</a>";

							else
                                cookieApplication = "<a class=CallLink  href=javascript:OpenApplicationPath('" + strApplicationPath + "','" + strCust + "','" + strPolId + "','" + strPolVer + "')>" + appArray[0].ToString() + "</a>";

							
							//							cookieApplication="<a class=CallLink  href=javascript:openQuickQuote('" + appArray[1].ToString() + "','" + appArray[2].ToString() + "','" + appArray[3].ToString() + "','" + appArray[1].ToString() + "','" + appArray[1].ToString() +  "','" + appArray[1].ToString() + "')>" + appArray[0].ToString()   + "</a>";
							//						cookieApplication="<a class=CallLink  href=javascript:OpenApplicationPath('" + strApplicationPath + "','" + strCust + "','" +  strApp + "','" + strAppVer + "')>" + appArray[0].ToString()   + "</a>";
							//cookieApplication="<a class=CalLink href=javascript:OpenApplicationPath('" +  appArray[1].ToString() + "','" +  appArray[2].ToString() + "','" +  appArray[3].ToString() + "')>" + appArray[0].ToString() + "</a>";      
                            if (appArray[0].ToString() != "")				
							    cookieAppDate=appArray[7].ToString().Substring(0,appArray[7].ToString().IndexOf(" ")); 
						}
                    //}
                    //else
                    //{
                    //    //cookieApplication= appArray[0].ToString();
                    //    cookieApplication= "";
                    //    if (appArray.Length > 7)
                    //        cookieAppDate=appArray[7].ToString().Substring(0,appArray[7].ToString().IndexOf(" ")); 
						
                    //}
				}
				else
				{
					cookieApplication="";
				}

				#region RECENTLY ACCESSED CLAIM

				//Added By Anurag Verma on 20/07/2006 For last visited Claim 
				 
				if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)	
					clString=ds.Tables[0].Rows[0]["LAST_VISITED_CLAIM"].ToString();

				if(clString!="")
				{
					string [] claimarray=clString.Split(new char[]{'~'});
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
						cookieClaim="<a class=CalLink  href=javascript:OpenClaimPath('" + strClaimPath + "','" + strCust + "','" +  strPolId + "','" + strClaimID + "','" + strLOB + "','" + strPolVer + "')>" + claimarray[0].ToString()   + "</a>";
 
					
						cookieClaimDate=claimarray[6].ToString().Substring(0,claimarray[6].ToString().IndexOf(" ")); 
					}
				}
				else
				{
					cookieClaim="";
				}

				#endregion
			}
		}

		private int ApplicationStatus(int customerId,int appId,int appVersion)
		{
			string applicationStatus=@ClsGeneralInformation.FetchApplicationXML(customerId,appId,appVersion );
			if(applicationStatus=="")
			  return 0;
			else
              return 1;

		}
		private void SetVariables()
		{
			//declaring object for resource manager	
            (new cmsbase()).SetCultureThread((new cmsbase()).GetLanguageCode());//Added by Charles on 11-Mar-10 for Multilingual Implementation
			aObjResMang=new ResourceManager("Cms.CmsWeb.WebControls.DiaryCalendar",Assembly.GetExecutingAssembly());   
			lblMonths=aObjResMang.GetString("lblMonths");
			lblDays=aObjResMang.GetString("lblDays");
			gstrshoall=aObjResMang.GetString("strALL");
			gstrQPA=aObjResMang.GetString("strQPA");
			gstrQPB=aObjResMang.GetString("strQPB");
			gstrEE=aObjResMang.GetString("strAR");
			gstrRR=aObjResMang.GetString("strRR");
			gstrBR=aObjResMang.GetString("strBR");
			gstrER=aObjResMang.GetString("strER");
			gstrAA=aObjResMang.GetString("strAA");
			gstrToday=aObjResMang.GetString("strToday");
			gstrAppoint=aObjResMang.GetString("strAppoint");
			gstruserid=Request.QueryString["UserId"];
            strCF=aObjResMang.GetString("strCF");
            strANF=aObjResMang.GetString("strANF");
            strCRE=aObjResMang.GetString("strCRE");

            //Added by Charles on 11-Mar-10 for Multilingual Implementation
            strPT = aObjResMang.GetString("strPT");
            strRV = aObjResMang.GetString("strRV");
            strCustomer = aObjResMang.GetString("strCustomer");
            strPolicy = aObjResMang.GetString("strPolicy");
            strApplication = aObjResMang.GetString("strApplication");
            strClaim = aObjResMang.GetString("strClaim");
            strOn = aObjResMang.GetString("strOn");
            strQuickAPP = aObjResMang.GetString("strQuickAPP");
            //Added till here
			
			string gStrMode="Default";
			string gStrToDo="";
			dtFormat="MM/dd/yyyy"; 
			aAppCountry="US";

			if(gstruserid==null || gstruserid.Equals("") || gstruserid.Equals("-5"))
			{
				gstruserid=UserId;
			}

			if(Request.QueryString["Mode"]!=null)
			{
				gStrMode=Request.QueryString["Mode"];
			}
			if(Request.QueryString["ToDoType"]==null || Request.QueryString["ToDoType"]=="" || Request.QueryString["ToDoType"]=="0")
			{
				gStrToDo="%";
			}
			else
			{
				gStrToDo=Request.QueryString["ToDoType"];
			}

		}

		/// <summary>
		/// This function is used for fetching data from database for displying count of pending tasks based on grouped data 
		/// </summary>
		private void SetCountVariables()
		{
			ClsDiary objClsDiary=new ClsDiary(); 	
			DataSet lObjDs=objClsDiary.GetCountListType(int.Parse(UserId),CustomerId);
			gStrEEC	= gStrQPAC = gStrQPBC= gStrRRC= gStrBRC= gStrERC= gStrAAC= cStrCF= cStrANF= cStrCRE="0";
			listtypeid1="1";
			listtypeid7="6";
			listtypeid2="3";
			listtypeid6="8";
			listtypeid5="7";
			listtypeid4="2";
			listtypeid3="4";
			listtypeid8="9";
			listtypeid9="10";
			listtypeid10="11";




			foreach(DataRow lRowJoin in lObjDs.Tables[0].Rows)
			{
				if(lRowJoin["listtypeid"].ToString().Equals("1"))
				{
					gStrEEC=lRowJoin["Counting"].ToString();
                    listtypeid1=lRowJoin["listtypeid"].ToString();
				}
				
				if(lRowJoin["listtypeid"].ToString().Equals("3"))
				{
					gStrQPAC=lRowJoin["Counting"].ToString();
                    listtypeid2=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("4"))
				{
					gStrQPBC=lRowJoin["Counting"].ToString();
                    listtypeid3=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("2"))
				{
					gStrRRC=lRowJoin["Counting"].ToString();
                    listtypeid4=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("7"))
				{
					gStrBRC=lRowJoin["Counting"].ToString();
					listtypeid5=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("8"))
				{
					gStrERC=lRowJoin["Counting"].ToString();
					listtypeid6=lRowJoin["listtypeid"].ToString();
				}
                if (lRowJoin["listtypeid"].ToString().Equals("48"))//if(lRowJoin["listtypeid"].ToString().Equals("6"))
				{
					gStrAAC=lRowJoin["Counting"].ToString();
					listtypeid7=lRowJoin["listtypeid"].ToString();
				}
                if(lRowJoin["listtypeid"].ToString().Equals("9"))
                {
                    cStrCF=lRowJoin["Counting"].ToString();
                    listtypeid8=lRowJoin["listtypeid"].ToString();
                }
                if(lRowJoin["listtypeid"].ToString().Equals("10"))
                {
                    cStrANF=lRowJoin["Counting"].ToString();
                    listtypeid9=lRowJoin["listtypeid"].ToString();
                }
                if(lRowJoin["listtypeid"].ToString().Equals("11"))
                {
                    cStrCRE=lRowJoin["Counting"].ToString();
                    listtypeid10=lRowJoin["listtypeid"].ToString();
                }
			}
			objClsDiary.Dispose(); 
		}


		/// <summary>
		/// fetching diary dates for appointments
		/// </summary>
		/// <returns>string contaning diary dates seperated with ~</returns>
		private string SetDiaryDates()
		{
			string diaryDates="";
			ClsDiary objClsDiary=new ClsDiary(); 			
			
			diaryDates=objClsDiary.SetDiaryDates(int.Parse(UserId),CustomerId);			

			return diaryDates;
		}
		private string ReplaceEntities(string str)
		{
			str  = str.Replace("&","&amp;");
			str  = str.Replace("\"","&quot;");	
			return str;
		}

		public string ColorScheme
		{
			get
			{
				return strColorScheme;
			}
			set
			{
				strColorScheme = value;
			}
		}
		public string UserId
		{
			get
			{
				return strUserId;
			}
			set
			{
				strUserId = value;
			}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
