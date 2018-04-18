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
using System.Resources;
using System.Reflection;

namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for CustomerManagerIndex.
	/// </summary>
	public class CustomerManagerIndex : Cms.Client.clientbase
	{
		protected System.Web.UI.WebControls.Label lblContacts;
		protected System.Web.UI.WebControls.Label lblNameAddress;
		protected System.Web.UI.WebControls.Label lblName;
		protected System.Web.UI.WebControls.Label lblHomePhone;
		protected System.Web.UI.WebControls.Label lblProducerInfo;
		protected System.Web.UI.WebControls.Label lblAddress1;
		protected System.Web.UI.WebControls.Label lblWorkPhone;
		protected System.Web.UI.WebControls.Label lblAddress2;
		protected System.Web.UI.WebControls.Label lblFax;
		protected System.Web.UI.WebControls.Label lblCity;
		protected System.Web.UI.WebControls.Label lblAccountExe;
		protected System.Web.UI.WebControls.Label lblCsr;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWolverineUser;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApp_id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersion_id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAgencyLogin;
		protected System.Web.UI.WebControls.HyperLink hlEdit;
		protected System.Web.UI.WebControls.Label lblNameHead;
		protected System.Web.UI.WebControls.Label lblHomeHead;
		protected System.Web.UI.WebControls.Label lblAddressHead1;
		protected System.Web.UI.WebControls.Label lblWorkHead;
		protected System.Web.UI.WebControls.Label lblAddressHead2;
        protected System.Web.UI.WebControls.Label lblHeader;
        protected Cms.CmsWeb.Controls.CmsButton btnBackToSearch;
		protected Cms.CmsWeb.Controls.CmsButton btnEdit;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.WebControls.Label lblFaxHead;
		protected System.Web.UI.WebControls.Label lblAccountHead;
		protected System.Web.UI.WebControls.Label lblCsrHead;
		protected System.Web.UI.WebControls.Label lblCityHead;
		//protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Label lblProducer;
		protected System.Web.UI.WebControls.Image AspImageNote;
		protected System.Web.UI.WebControls.Label lblProducerHead;
		public int intCustomerID;
        public string CustAsst; //Added by Charles on 12-Mar-10 for Multilingual Support

        private ResourceManager objResourceMgr;
		
		public int LastVisitedTab = 0;	//Holds the values of tab number last visited from cookie

		private void Page_Load(object sender, System.EventArgs e)
		{

			btnEdit.Attributes.Add("OnClick","javascript:return NewWin();");
			btnBackToSearch.Attributes.Add("onclick","javascript:return DoBack();");
			
			base.ScreenId="120";
			btnEdit.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnEdit.PermissionString				=	gstrSecurityXML;	

			btnBackToSearch.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnBackToSearch.PermissionString				=	gstrSecurityXML;	
			//cms/application/aspx/applicationindex.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCUSTOMER_ID').value + "?

            CustAsst = CmsWeb.ClsMessages.GetTabTitles(ScreenId, "CustAsst");//Added by Charles on 12-Mar-10 for Multilingual Support

			//Retreives the cookie values
			GetCookieValues();
            
            //Added by Charles on 10-Mar-10 for Policy Page Implementation
            if (!IsPostBack)
            {
                SetCaptions();
            }//Added till here

			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString().Length>0)
			{
				hidCustomer_id.Value = Request.QueryString["CUSTOMER_ID"];

				if (GetCustomerID() != hidCustomer_id.Value)
				{
					//Adding new customer session
					base.SetCustomerID(hidCustomer_id.Value);

					//Removing the application session
					
					base.SetAppID("");
					base.SetAppVersionID("");
					base.SetLOBString("");					
					/*
					 * Modified By	:	Anurag Verma
					 * Modified On	:	07/03/2007
					 * Purpose		:	If customer is different then session for 
					 *					policy ID,LOBID, Policy status, policy version ID, claim ID 
					 *					should be updated to blank
					 */
					base.SetLOBID("");
					base.SetPolicyStatus(""); 
					base.SetPolicyID(""); 
					base.SetClaimID("");
					base.SetPolicyVersionID("");
					
				}
			}	
			else
			{
				hidCustomer_id.Value = base.GetCustomerID();
			}
			CheckUser();
			CheckAgencyLogin();
			CustomerDetails(int.Parse(hidCustomer_id.Value));		
			//TabCtl.TabURLs="../../cmsweb/aspx/quickquotelist.aspx?CalledFrom=INCLT&CUSTOMER_ID="+hidCustomer_id.Value + "&";
			
		}

        /// <summary>
        /// Set Captions
        /// </summary>
        /// Added by Charles on 10-Mar-10 for Policy Page Implementation
        private void SetCaptions()
        {
            //SetCultureThread(GetLanguageCode());
            objResourceMgr = new ResourceManager("Cms.Client.Aspx.CustomerManagerIndex", Assembly.GetExecutingAssembly());

            lblHeader.Text = objResourceMgr.GetString("lblHeader");
            lblNameAddress.Text = objResourceMgr.GetString("lblNameAddress");
            lblNameHead.Text = objResourceMgr.GetString("lblNameHead");
            lblHomeHead.Text = objResourceMgr.GetString("lblHomeHead");
            lblAddressHead1.Text = objResourceMgr.GetString("lblAddressHead1");
            lblAddressHead2.Text = objResourceMgr.GetString("lblAddressHead2");
            lblFaxHead.Text = objResourceMgr.GetString("lblFaxHead");
            lblCityHead.Text = objResourceMgr.GetString("lblCityHead");
            lblWorkHead.Text = objResourceMgr.GetString("lblWorkHead");
            lblContacts.Text = objResourceMgr.GetString("lblContacts");
            btnBackToSearch.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(ScreenId, "btnBack");
            btnEdit.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(ScreenId, "btnEdit");
        }

		private void CheckUser()
		{
			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strSystemID = GetSystemId();
			if(strCarrierSystemID.ToUpper()!=strSystemID.ToUpper())
				hidWolverineUser.Value = "1";
			else
				hidWolverineUser.Value = "0";
		}

		//Added by Mohit Agarwal 7-May to check if Agency Login
		private void CheckAgencyLogin()
		{
			string agency_name = GetSystemId();
            string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
			if(agency_name.ToUpper()!=strCarrierSystemID.Trim().ToUpper())
				hidAgencyLogin.Value = "1";
			else
				hidAgencyLogin.Value = "0";				
		}

		private void GetCookieValues()
		{
			if(Request.QueryString["Calledfor"]== "Claim")
				LastVisitedTab=1;
			else if(Request.QueryString["Calledfor"]== "AGENQUOTE")
				LastVisitedTab=0;
			else if(Request.QueryString["Calledfor"]== "AGENAPP")
				LastVisitedTab=1;
            else if (Request.QueryString["Calledfor"] == "TrLog")
                LastVisitedTab = 6;
            else if (Request.QueryString["Calledfor"]!= null && Request.QueryString["Calledfor"].ToString().ToUpper() == "POLICY")
                LastVisitedTab = 1;
            else if (Request.QueryString["Calledfor"] != null && Request.QueryString["Calledfor"].ToString().ToUpper() == "APPLICATION")
                LastVisitedTab = 1;
			else
			{
				if (Request.Cookies["LastVisitedTab"] != null )
				{
					if (Request.Cookies["LastVisitedTab"].Value != "" && Request.Cookies["LastVisitedTab"].Value != null)
						LastVisitedTab = int.Parse(Request.Cookies["LastVisitedTab"].Value);
                    Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0, 0));
				}
			}
            
		}

		private void CustomerDetails(int intCustomerId)
		{
			DataSet dsCustomer = new DataSet();
			Cms.BusinessLayer.BlClient.ClsCustomer objCustomer = new Cms.BusinessLayer.BlClient.ClsCustomer();

			dsCustomer = objCustomer.CustomerDetails(intCustomerId);

			try
			{
				if(dsCustomer!=null)
				{
					if(dsCustomer.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString()!="")
					lblName.Text = dsCustomer.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()+ " " + dsCustomer.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString();
					else
					lblName.Text = dsCustomer.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
					lblAddress1.Text = dsCustomer.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
					lblAddress2.Text = dsCustomer.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
					lblHomePhone.Text = dsCustomer.Tables[0].Rows[0]["CUSTOMER_HOME_PHONE"].ToString();
					lblWorkPhone.Text = dsCustomer.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString();
					lblFax.Text = dsCustomer.Tables[0].Rows[0]["CUSTOMER_FAX"].ToString();
					lblCity.Text = dsCustomer.Tables[0].Rows[0]["CITY_STATE_ZIP"].ToString();
					//lblAccountExe.Text = dsCustomer.Tables[0].Rows[0]["USER_NAME_ACC"].ToString();
					//lblCsr.Text = dsCustomer.Tables[0].Rows[0]["USER_NAME_CSR"].ToString();
					//lblProducerInfo.Text = dsCustomer.Tables[0].Rows[0]["USER_NAME"].ToString();
					if(dsCustomer.Tables[0].Rows[0]["CUSTOMER_ATTENTION_NOTE"].ToString()!="0")
					{
						AspImageNote.ImageUrl="~/cmsweb/images/att-ecs.gif"; 
					}
					else
					{
						AspImageNote.ImageUrl="~/cmsweb/images/att-ecs-grey.gif";
					}
					SetCustomerCookies(lblName.Text,intCustomerId);
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(dsCustomer!=null)
					dsCustomer.Dispose();
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

		private void SetCustomerCookies(string lblName,int custID)
		{
			//Added Mohit Agarwal 19-Feb 2007 to store last 3 Items
			# region last 3 cookies
			string AgencyId = GetSystemId();
			if(AgencyId.ToUpper() != CarrierSystemID)
			{
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastPageVisited = new ClsGeneralInformation();
				DataSet ds = objLastPageVisited.GetLastVisitedPageEntry(int.Parse(GetUserId()),AgencyId);

				//if(ds!=null && ds.Tables[0].Rows.Count > 0)
				//{
					//if(System.Web.HttpContext.Current.Request.Cookies["customerName" + GetSystemId() + "_" + GetUserId() + "_1"] != null)
					if(ds!=null && ds.Tables[0].Rows.Count>0 && ds.Tables[0].Rows[0]["LAST_VISITED_CUSTOMER"]!=System.DBNull.Value)
					{
						string prevCook = ds.Tables[0].Rows[0]["LAST_VISITED_CUSTOMER"].ToString();
						string [] cookArr = prevCook.Split('@');
						if(cookArr.Length > 0 && cookArr.Length < 4)
						{
							//System.Web.HttpContext.Current.Response.Cookies["customerName" + GetSystemId() + "_" + GetUserId() + "_1"].Value = lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date + "@" + System.Web.HttpContext.Current.Request.Cookies["customerName" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
							string Cust_Details=lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date + "@" + ds.Tables[0].Rows[0]["LAST_VISITED_CUSTOMER"].ToString();
							Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedCustomer= new ClsGeneralInformation(); 
							objLastVisitedCustomer.UpdateLastVisitedPageEntry("Customer",Cust_Details,int.Parse(GetUserId()),AgencyId);
						}
						else if(cookArr.Length >= 4)
						{
							int maxindex = cookArr.Length-1;
							if(maxindex >= 3)
								maxindex = 2;

							//System.Web.HttpContext.Current.Response.Cookies["customerName" + GetSystemId() + "_" + GetUserId() + "_1"].Value = lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date + "@";
							string Cust_Details=lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date + "@";
							Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedCustomer= new ClsGeneralInformation(); 
							objLastVisitedCustomer.UpdateLastVisitedPageEntry("Customer",Cust_Details,int.Parse(GetUserId()),AgencyId);
							for(int cookindex = 0; cookindex < maxindex; cookindex++)
							{
								//System.Web.HttpContext.Current.Response.Cookies["customerName" + GetSystemId() + "_" + GetUserId() + "_1"].Value += cookArr[cookindex] + "@";
								Cust_Details += cookArr[cookindex] + "@";
							}
						}
						else
						{
							//System.Web.HttpContext.Current.Response.Cookies["customerName" + GetSystemId() + "_" + GetUserId() + "_1"].Value=lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date + "@";
							string Cust_Details=lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date + "@";
							Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedCustomer= new ClsGeneralInformation(); 
							objLastVisitedCustomer.UpdateLastVisitedPageEntry("Customer",Cust_Details,int.Parse(GetUserId()),AgencyId);
						}
						//System.Web.HttpContext.Current.Response.Cookies["customerName" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
					}
					else
					{
						//System.Web.HttpContext.Current.Response.Cookies["customerName" + GetSystemId() + "_" + GetUserId() + "_1"].Value=lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date + "@";
						//System.Web.HttpContext.Current.Response.Cookies["customerName" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
						string Cust_Details=lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date + "@";
						Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedCustomer= new ClsGeneralInformation(); 
						objLastVisitedCustomer.UpdateLastVisitedPageEntry("Customer",Cust_Details,int.Parse(GetUserId()),AgencyId);
					}
				//}
			}
				#endregion

			else
			{
//				System.Web.HttpContext.Current.Response.Cookies["customerName" + GetSystemId() + "_" + GetUserId()].Value=lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date;
//				System.Web.HttpContext.Current.Response.Cookies["customerName" + GetSystemId() + "_" + GetUserId()].Expires=DateTime.MaxValue; 
				string Customer_Details=lblName + "~" + custID.ToString() + "~" + DateTime.Today.Date;
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedApp= new ClsGeneralInformation(); 
				objLastVisitedApp.UpdateLastVisitedPageEntry("Customer",Customer_Details,int.Parse(GetUserId()),AgencyId);
			}		

		}
	}
}
