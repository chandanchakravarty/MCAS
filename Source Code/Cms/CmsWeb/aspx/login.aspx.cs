/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 08/02/2005
	<End Date				: - > 08/04/2005   
	<Description			: - > Login Page

*******************************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Resources; 
using System.Reflection; 
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher;
using System.Configuration;
using System.Globalization;
using System.Threading;
namespace Cms.CmsWeb.Aspx
{
	/// <summary>
	/// Class represents login functionality.
	/// It sets session after validation and also set cookie for the logged in user. Logged in user will be able to 
	/// log in automatically using cookies.
	/// </summary>
	public class login : Cms.CmsWeb.cmsbase
	{
		
		#region CONTROL REFERENCE
            //protected System.Web.UI.WebControls.RequiredFieldValidator reqTxtSystemId;
            //protected System.Web.UI.WebControls.RequiredFieldValidator reqTxtUserID;
            //protected System.Web.UI.WebControls.RequiredFieldValidator reqTxtPassword;	// object for resource manager class

            protected System.Web.UI.WebControls.Label capSystemId;
            protected System.Web.UI.WebControls.Label capUserID;
            protected System.Web.UI.WebControls.Label capPassword;
		#endregion

		#region GLOBAL VARIABLES DECLARATION			
		public ClsCommon objClsCommon;
		protected System.Web.UI.WebControls.Button tempButton;
		protected System.Web.UI.WebControls.Label lblHidUserName;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblUserMessage;
		protected System.Web.UI.WebControls.TextBox txtSystemId;
		protected System.Web.UI.WebControls.TextBox txtUserID;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLG; // object reference of clsCommon class
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMLG; // object reference of clsCommon class
        public ResourceManager aObjResMang;
		#endregion

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            //HttpBrowserCapabilities cult = HttpContext.Current.Request.Browser;//System.Threading.Thread.CurrentThread.CurrentUICulture;
            string BrowserCult = Request.UserLanguages[0];
            CultureInfo objculture = new CultureInfo(BrowserCult);
            Thread.CurrentThread.CurrentUICulture = objculture;
            SetSessionVariables();
			if(!Page.IsPostBack )
			{
				// IF REQUEST URL CONTAINS QUERYSTRING VARIABLE WITH LG VALUE CALL LOGOUT METHOD
				if (Request.QueryString["action"]=="lg")
				{
					Response.Redirect("/cms/cmsweb/aspx/logout.aspx",true);
				}
				
			}
			
			// setting page labels 
			SetPageLabels();
			int lResult=-1; // using for storing return value for function
			
			if(Page.IsPostBack)
			{
				//Commented on 24 feb 2009 Cookies Coutn will always be Zero 
				/*if(Response.Cookies.Count>1)
				{
					//If userid cookie is null than it is a new user and needs to be validated
					//else set session variables through cookie variables
					if((Response.Cookies["ckUserId"]== null)||(Response.Cookies["ckUserTypeId"] == null)||(Response.Cookies["ckSysId"] == null))
					{
						//	COOKIE COLLECTION IS EMPTY THEREFORE VALIDATE USER CREDENTIALS
						lResult=ValidateUser();
					}
					else if(!ValidateLoginLocked(Request.Cookies["ckUserId"].Value.ToString()))	
					{
						//	COOKIE COLLECTION IS NOT EMPTY THEREFORE SET SESSION VARIABLES THROUGH COOKIE VARIABLES
						SetUserId(Request.Cookies["ckUserId"].Value.ToString());
						SetUserTypeId(Request.Cookies["ckUserTypeId"].Value.ToString());
						SetUserName((Request.Cookies["ckUserNm"] == null ? "" : Request.Cookies["ckUserNm"].Value.ToString()));
						SetSystemId(Request.Cookies["ckSysId"].Value.ToString());
						SetImageFolder((Request.Cookies["ckImgFld"] == null ? "1" : Request.Cookies["ckImgFld"].Value.ToString()));
						SetColorScheme((Request.Cookies["ckClrSch"] == null ? "1" : Request.Cookies["ckClrSch"].Value.ToString()));
						SetUserFLName((Request.Cookies["ckUserFLNm"] == null ? "" : Request.Cookies["ckUserFLNm"].Value.ToString()));
						//<Mohit Gupta>  31-May-2005 : START : <>
						SetGridSize((Request.Cookies["ckGridSize"] == null ? "20" : Request.Cookies["ckGridSize"].Value.ToString()));
						//<Mohit Gupta>  31-May-2005 : END

                    
					
					
						hidLG.Value ="1";
						lblHidUserName.Text="<font color=white size='4px'>Welcome ! </font> <br><br><font size='3px'>" + Request.Cookies["ckUserFLNm"].Value.ToString() + "</font>";
						lResult=3;	
						lblMessage.Text="";
						lResult = ValidateCookieUser(Request.Cookies["ckSysId"].Value.ToString(), Request.Cookies["ckUserId"].Value.ToString(), lResult);
						if(lResult == 6)
							hidLG.Value = "0";
					}
					else
						Response.Redirect("/cms/cmsweb/aspx/logout.aspx",true); 
				}
				else*/
				//Call this Function as Respose.Cookies Coutn will always be Zero : 05 Feb 2009
				lResult=ValidateUser();
			}
			else
			{
				//Commented on 24 feb 2009 
				/*if(Request.Cookies.Count>1)
				{
					if((Request.Cookies["ckUserId"]!=null)&&(Request.Cookies["ckUserTypeId"] != null)&&(Request.Cookies["ckSysId"] != null))
					{
						if(ValidateLoginLocked(Request.Cookies["ckUserId"].Value.ToString()))	Response.Redirect("/cms/cmsweb/aspx/logout.aspx",true); 
						//	SET SESSION VARIABLES THROUGH COOKIE VARIABLES
						SetUserId(Request.Cookies["ckUserId"].Value.ToString());
						SetUserTypeId(Request.Cookies["ckUserTypeId"].Value.ToString());
						SetUserName((Request.Cookies["ckUserNm"] == null ? "" : Request.Cookies["ckUserNm"].Value.ToString()));
						SetSystemId(Request.Cookies["ckSysId"].Value.ToString());
						SetImageFolder((Request.Cookies["ckImgFld"] == null ? "1" : Request.Cookies["ckImgFld"].Value.ToString()));
						SetColorScheme((Request.Cookies["ckClrSch"] == null ? "1" : Request.Cookies["ckClrSch"].Value.ToString()));
						SetUserFLName((Request.Cookies["ckUserFLNm"] == null ? "" : Request.Cookies["ckUserFLNm"].Value.ToString()));
						//<Mohit Gupta>  31-May-2005 : START : <>
						SetGridSize((Request.Cookies["ckGridSize"] == null ? "20" : Request.Cookies["ckGridSize"].Value.ToString()));
						//<Mohit Gupta>  31-May-2005 : END
                

						lblHidUserName.Text="<font color=white size='4px'>Welcome ! </font> <br><br><font size='3px'>" + Request.Cookies["ckUserFLNm"].Value.ToString() + "</font>";
						hidLG.Value ="1";
						lResult=3;	
						lblMessage.Text="";
						lResult = ValidateCookieUser(Request.Cookies["ckSysId"].Value.ToString(), Request.Cookies["ckUserId"].Value.ToString(), lResult);
						if(lResult == 6)
							hidLG.Value = "0";
					
					}
				}
				else*/
					lResult=3;	
			}

			switch (lResult)
			{
				case 0:					
					lblMessage.Text="Please enter valid information";
					break;
				case 1:
					Response.Redirect("/cms/cmsweb/aspx/index.aspx",true); 
					break;
				case 3:
					break;
				case 4:
					lblMessage.Text="User is Inactive, Please contact administrator";
					break;
				case 5:
					lblMessage.Text="Agency is Inactive, Please contact administrator";
					break;
				case 6:					
					lblMessage.Text="User logged in earlier does not exists now, Please contact administrator";
					break;
				case 7:					
					lblMessage.Text="User Account has been locked, Please contact administrator";
					break;
				case 8:					
					Response.Redirect("/cms/cmsweb/aspx/ChangePassword.aspx",true); 
					break;
 				default:
					break;
			}
		}

        private bool SetCarrierInformation(string lSystemId)
        {
            ClsLogin clsLogin = new ClsLogin(); 
            string strDBConStrng ="";
            DataSet ds = new DataSet();
            ds = clsLogin.GetCarrierInformation(lSystemId.Replace("'", "''"));
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    strDBConStrng = ds.Tables[0].Rows[0]["CARRIER_CON_STRING"].ToString();
                    ClsCommon.ConnStr = strDBConStrng;
                    SetConnStr(strDBConStrng);
                    Cms.EbixDataLayer.DataWrapper.ConnString = strDBConStrng;
                    strDBConStrng = ds.Tables[0].Rows[0]["CARRIER_CON_GRID_STRING"].ToString();
                    ClsCommon.ConnGridStr = strDBConStrng;
                    SetConnGridStr(strDBConStrng);
                    ExceptionPublisher.ExceptionManagement.ExceptionManager.ExceptionConString = strDBConStrng;
                    CarrierSystemID = ds.Tables[0].Rows[0]["CARRIER_CODE"].ToString();
                    setCarrierSystemID(CarrierSystemID);
                    ClsCommon.CarrierSystemID = ds.Tables[0].Rows[0]["CARRIER_CODE"].ToString();
                    
                }
                if (strDBConStrng == "")
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void SetSessionVariables()
        {       
            SetAppID("");
            SetCustomerID("");
            SetAppVersionID("");
        }

        /// <summary>
        /// TO SET THE PAGE LABELS OR ERROR MESSAGES CALL SetPageLabels METHOD 
        /// </summary>
		private void SetPageLabels()
		{
			aObjResMang=new ResourceManager("Cms.CmsWeb.Aspx.login",Assembly.GetExecutingAssembly());
            capSystemId.Text = aObjResMang.GetString("txtSystemId");
            capUserID.Text = aObjResMang.GetString("txtUserID");
            capPassword.Text = aObjResMang.GetString("txtPassword"); 	
		}

		//Added by Mohit Agarwal 25-Jun-2007
		private int ValidateCookieUser(string systemId, string userId, int lResult)
		{
			ClsLogin clsLogin = new ClsLogin(); // creating object of clslogin class
						
			DataSet ds=new DataSet();
			ds = clsLogin.GetUserInformation(systemId.Replace("'","''"),userId.Replace("'","''").ToUpper(),"");
 
			try
			{
				if(ds!=null && ds.Tables.Count > 1)
				{
					if(ds.Tables[1].Rows.Count<=0)
					{
						return 6;
					}
				}
			}
			catch
			{}
			return lResult;
		}

		/// <summary>
		/// TO VALIDATE USER LOGIN AND SETTING SESSION VARIABLES ON SUCCESSFUL LOGIN CALL ValidateUser METHOD
		/// RETURNS INTEGER INDICATING SUCCESSFUL LOGIN OR NOT
		/// </summary>
		/// <param></param>
		/// <returns>INT</returns>
		private int ValidateUser()
		{
			string lSystemId="",lUserId="",lPassword=""; // for storing form control values
			int lResult=-1; // to store the result after save operation
	
			lSystemId=txtSystemId.Text.Trim();
			lUserId=txtUserID.Text.Trim();
			lPassword=txtPassword.Text.Trim();
            if(!SetCarrierInformation(lSystemId)) return 0 ;
			ClsLogin clsLogin = new ClsLogin(); // creating object of clslogin class
						
			DataSet ds=new DataSet();
			ds = clsLogin.GetUserInformation(lSystemId.Replace("'","''"),lUserId.Replace("'","''").ToUpper(),lPassword.Replace("'","''"));
 
			try
			{
				if(ds!=null)
				{
					if(ds.Tables[0].Rows.Count>0)
					{
						if(ds.Tables[0].Rows[0]["is_active"].ToString().Equals("N"))
						{
							lResult=4;
						}
						else if(ds.Tables[0].Rows[0]["Agency_IsActive"].ToString().Equals("N"))
						{
							lResult=5;
						}
						else if(ds.Tables[0].Rows[0]["USER_LOCKED"].ToString().Equals("Y"))
						{
							lResult=7;
						}
						else if(ds.Tables[0].Rows[0]["CHANGE_PWD_NEXT_LOGIN"].ToString().Equals("10963"))
						{
							SetUserId(ds.Tables[0].Rows[0]["user_id"].ToString());
							SetUserTypeId(ds.Tables[0].Rows[0]["user_type_id"].ToString());
							SetUserName(ds.Tables[0].Rows[0]["user_login_id"].ToString());
							SetSystemId(ds.Tables[0].Rows[0]["user_system_id"].ToString());
							SetImageFolder(ds.Tables[0].Rows[0]["user_image_folder"].ToString());
							SetColorScheme(ds.Tables[0].Rows[0]["user_color_scheme"].ToString());
                            SetLanguageID(ds.Tables[0].Rows[0]["LANG_ID"].ToString());//By Chetna
                            SetLanguageCode(ds.Tables[0].Rows[0]["Lang_Code"].ToString());//By Chetna
                            SetSYSBaseCurrency(ds.Tables[0].Rows[0]["BASE_CURRENCY"].ToString());//Added by Pradeep on 29-Sep-2010 for Base currency implementation
                            // Added by Charles on 12-Apr-2010 for Multilingual Implementation
                            SetCultureThread(ds.Tables[0].Rows[0]["Lang_Code"].ToString());
                            //ClsMessages.SetCustomizedXml(ds.Tables[0].Rows[0]["Lang_Code"].ToString());
                            //ClsCommon.SetCustomizedXml(ds.Tables[0].Rows[0]["Lang_Code"].ToString());
                            //ClsCommon.BL_LANG_CULTURE = ds.Tables[0].Rows[0]["Lang_Code"].ToString();
                            //ClsCommon.BL_LANG_ID = int.Parse(ds.Tables[0].Rows[0]["LANG_ID"].ToString());
                           // ClsSingleton.strLangSelectClause = "LANG_ID = " + ClsCommon.BL_LANG_ID.ToString();
                            //Added till here
                            SetRegExpCulture();//Added by Charles on 27-May-2010 for Multilingual Support

                            SetIsUserSuperVisor(ds.Tables[0].Rows[0]["USER_SPR"].ToString().Trim());
							if(Session["userId"]!=null && Session["userId"].ToString()!="")
							{
								ValidateMultipleLogin(Session["userId"].ToString(),Session.SessionID.ToString());
							}
                            SetSysNavigation(ds.Tables[0].Rows[0]["SYS_NAVIGATION"].ToString().Trim());
							lResult=8;
						}
						else
						{
							SetUserId(ds.Tables[0].Rows[0]["user_id"].ToString());
							SetUserTypeId(ds.Tables[0].Rows[0]["user_type_id"].ToString());
							SetUserName(ds.Tables[0].Rows[0]["user_login_id"].ToString());
							SetSystemId(ds.Tables[0].Rows[0]["user_system_id"].ToString());
							SetImageFolder(ds.Tables[0].Rows[0]["user_image_folder"].ToString());
							SetColorScheme(ds.Tables[0].Rows[0]["user_color_scheme"].ToString());
                            SetLanguageID(ds.Tables[0].Rows[0]["LANG_ID"].ToString());//By Chetna
                            SetLanguageCode(ds.Tables[0].Rows[0]["Lang_Code"].ToString());//By Chetna
							SetUserFLName(ds.Tables[0].Rows[0]["user_name"].ToString());
							SetGridSize(ds.Tables[0].Rows[0]["grid_size"].ToString());
                            SetSYSBaseCurrency(ds.Tables[0].Rows[0]["BASE_CURRENCY"].ToString());//Added by Pradeep on 29-Sep-2010 for Base currency implementation
                            // Added by Charles on 12-Mar-2010 for Multilingual Implementation
                            SetCultureThread(ds.Tables[0].Rows[0]["Lang_Code"].ToString());
                            //ClsMessages.SetCustomizedXml(ds.Tables[0].Rows[0]["Lang_Code"].ToString());
                            //ClsCommon.SetCustomizedXml(ds.Tables[0].Rows[0]["Lang_Code"].ToString());
                            //ClsCommon.BL_LANG_CULTURE = ds.Tables[0].Rows[0]["Lang_Code"].ToString();
                            //ClsCommon.BL_LANG_ID = int.Parse(ds.Tables[0].Rows[0]["LANG_ID"].ToString());
                            //Added till here
                            //ClsSingleton.strLangSelectClause = "LANG_ID = " + ClsCommon.BL_LANG_ID.ToString(); // Added by Charles on 12-Apr-2010 for Multilingual Implementation                            
                            
                            SetRegExpCulture();//Added by Charles on 27-May-2010 for Multilingual Support
                            SetIsUserSuperVisor(ds.Tables[0].Rows[0]["USER_SPR"].ToString().Trim());
							//SetCookie(); //Commented on )5 Feb 2009
                            SetSysNavigation(ds.Tables[0].Rows[0]["SYS_NAVIGATION"].ToString().Trim());
							lResult=1;
						}
					}
					else if(ds.Tables[2].Rows[0]["IS_USER_LOCKED"].ToString().Equals("Y"))
						{
							lResult=7;
						}
					else
					{	
						SetUserId("");
						SetUserTypeId("");
						SetUserName("");
						SetSystemId("");
						SetImageFolder("");
						SetColorScheme("");
						SetUserFLName("");
						SetGridSize("");
                     
						lResult=0;
						
					}
				}
			}
			catch (Exception e)
			{
				lblMessage.Text=e.Message; 
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(e);
			}
			finally
			{
				ds.Dispose(); 
			}
			return lResult;
		}
		private void ValidateMultipleLogin(string userID,string sessionID)
		{
			//Check if user
			ClsLogin clsLogin = new ClsLogin();
			DataSet ldsStatus = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetUserLoggedStatus " + int.Parse(Session["userId"].ToString()));
			if(ldsStatus!=null && ldsStatus.Tables[0].Rows.Count>0)
			{
				clsLogin.UpdateLoggedStatus(int.Parse(userID.ToString()),"N",sessionID);
				Session.Remove(ldsStatus.Tables[0].Rows[0]["SESSION_ID"].ToString());		
				
			}
		}
		private bool ValidateLoginLocked(string userID)
		{
			//Check if user accout Locked or not
			DataSet ldsStatus = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetUserLockedStatus " + int.Parse(userID));
			if(ldsStatus!=null && ldsStatus.Tables[0].Rows.Count>0)
			{
				 if(ldsStatus.Tables[0].Rows[0]["USER_LOCKED"].ToString()=="1" 
					 || ldsStatus.Tables[0].Rows[0]["USER_LOCKED"].ToString()=="Y"
					 || ldsStatus.Tables[0].Rows[0]["CHANGE_PWD_NEXT_LOGIN"].ToString()=="Y" )		
				 return true;
				else 
				  return false;
			}
			else
				return false;

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
