/******************************************************************************************

Created By				: Pravesh K. Chandel
dated					: 15 dec 2006
purpose					: for MVR and Undisclose Drivers  
  
<Modified Date			: - > 26-Jun-2007
<Modified By			: - > Mohit Agarwal
<Purpose				: - > ITrack 2030 ->  various enhancements in MVR fetching
******************************************************************************************/

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
using System.Reflection;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
//using APToolkitNET;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Application;
using Cms.Model.Quote;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.BusinessLayer.BlQuote ;
using Cms.BusinessLayer.BlClient;
//using Cms.BusinessLayer.BlAccount;
using System.Globalization;
using Cms.CmsWeb.WebControls;

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for MvrForm.
	/// </summary>
	public class MvrForm : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgUNDICLOSED_DRIVER;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidKeyValues;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckedRowIDs;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDelString;
		protected System.Web.UI.WebControls.Label lblErr;
		protected int NO_OF_UDI;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRequestType;
		protected int NO_OF_EXC_MVR;
		protected System.Web.UI.WebControls.PlaceHolder GridHolderOpe;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOperators;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnCloseDriv;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnFetchMVR;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnFetchUDVI;
		protected Hashtable   htNOMVR;

		private string NewUDVI = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnFetchMVR.Attributes.Add("onclick","javascript:if (Confirm() == false) return false;");
			if(hidCheckedRowIDs.Value!="")
				DeleteUDI();
//			lblMessage.Text="";
			hidRequestType.Value=Request["CalledFor"].ToString();
			if (!IsPostBack) 
			{
				//				FetchOperatorsMVR();
			}
			ShowDrivers();

			#region loading web grid control
			
			if (Request["CalledFor"].ToString()=="MVR")
			{
				GridHolder.Visible =false;
				lblErr.Visible =false;
				return;
			}
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{
				if (!IsPostBack) 
				{
					FetchUndisclosedDrivers();
					//				if (NO_OF_UDI !=0)
					//				{
					//					//lblMessage.Text = NO_OF_UDI + " Undisclosed Driver has been saved.";
					//					//if(hidCheckedRowIDs.Value!="")
					//					//	DeleteUDI();
					//					
					//				}
					//				else
					//				{
					//					lblErr.Text = "No Undisclosed Driver found.";
					//					lblErr.Visible = true;
					//				}
				}
				lblErr.Visible = true;
				//Setting web grid control properties
				
				// Commented for ITRack 2135 27-Jul 2007
	/*			objWebGrid.WebServiceURL = "http://" + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = "CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,UNDISCLOSED_DRIVER_ID,IsNull(DRIVER_NAME,'') DRIVER";
				objWebGrid.FromClause = "APP_UNDISCLOSED_DRIVER ";
				objWebGrid.WhereClause = " CUSTOMER_ID = '" + int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString())  + "" 
					+ "' AND APP_ID = '" + int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID","") : Request["APP_ID"].ToString())  
					+ "' AND APP_VERSION_ID = '" + int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID","") : Request["APP_VERSION_ID"].ToString()) + "'";

					objWebGrid.SearchColumnHeadings = "Driver Name";
					objWebGrid.SearchColumnNames = "DRIVER_NAME";
					objWebGrid.DisplayColumnNames = "DRIVER";
					objWebGrid.DisplayColumnHeadings = "Driver Name";
					objWebGrid.SearchColumnType = "T^";				
					objWebGrid.OrderByClause = "DRIVER ASC";				
					objWebGrid.DisplayColumnNumbers = "6";
					            

					objWebGrid.DisplayTextLength = "100";
					objWebGrid.DisplayColumnPercent = "100";
					objWebGrid.PrimaryColumns = "2";
					objWebGrid.PrimaryColumnsName = "UNDISCLOSED_DRIVER_ID";

					objWebGrid.ColumnTypes = "B";				
					
					objWebGrid.HeaderString = "Undisclosed Drivers";
					
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^2^3^4^5^6";

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireCheckbox ="Y";
				objWebGrid.ExtraButtons = "2^Delete~Close^0~1^DeleteRecords~CloseWindow";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
				objWebGrid.HImagePath = System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
				
				//objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "DRIVER_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1); */
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		

#endregion
		}

		private void FetchOperatorsMVR()
		{
			lblErr.Visible =false;
			lblErr.Text ="";
			try
			{
				if(Request["CalledFor"].ToString()=="MVR")
				{
					hidRequestType.Value ="MVR"; 
					htNOMVR=new Hashtable(); 
					SetDriverViolations();
					//if (NO_OF_UDI !=0)
					//lblMessage.Text = NO_OF_UDI + " MVR Driver and " + NO_OF_EXC_MVR + " Exception has been saved.<br>";
					//	lblMessage.Text = "MVR Report fetched and imported succefully and " + NO_OF_EXC_MVR + " Exception has been saved.<br>";
					//else if (NO_OF_EXC_MVR !=0)
					//lblMessage.Text = NO_OF_EXC_MVR + " Exception MVR Driver has been saved.<br>";
					//else
					//{
					if (htNOMVR.Keys.Count>0)  
					{
						IDictionaryEnumerator iEnum = htNOMVR.GetEnumerator(); 
						while (iEnum.MoveNext()) 
						{ 
							//lblMessage.Text=lblMessage.Text + iEnum.Key.ToString().PadRight(20,' ')  + " : " +  iEnum.Value + "<br>";
							lblMessage.Text=lblMessage.Text + iEnum.Value + "<br>";
						} 
					}
					else
						lblMessage.Text = "No MVR Driver Found.";
					//}
				}
				else
				{
//					FetchUndisclosedDrivers();
//					if (NO_OF_UDI !=0)
//					{
//						//lblMessage.Text = NO_OF_UDI + " Undisclosed Driver has been saved.";
//						//if(hidCheckedRowIDs.Value!="")
//						//	DeleteUDI();
//
//					}
//					else
//						lblMessage.Text = "No Undisclosed Driver found.";
				}
			}
			catch(Exception  ex)
			{
				if(Request["CalledFor"].ToString()=="MVR")
					lblMessage.Text=ex.Message;
				else
				{
					lblErr.Text =ex.Message;
					lblErr.Visible =true;
				}
				return;
			}
		}
		//Added by Mohit Agarwal 26-Jun-2007
		private void ShowDrivers()
		{
			#region loading web grid control
			
			if (Request["CalledFor"].ToString()=="MVR")
			{
				GridHolderOpe.Visible =true;
				lblErr.Visible =false;
			}
			else
			{
				GridHolderOpe.Visible =false;
				lblErr.Visible =false;
				return;
			}
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{
				string gstrLobID= GetLOBID().ToString();
				//Setting web grid control properties
				
				objWebGrid.WebServiceURL =   httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = "CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID, ISNULL(DRIVER_FNAME,'')+ ' ' + ISNULL(DRIVER_MNAME,'')+ ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,DRIVER_DRIV_LIC ";
				if(gstrLobID == "2" || gstrLobID == "3")//ppa or mot lob
					objWebGrid.FromClause = "APP_DRIVER_DETAILS ";
				else
					objWebGrid.FromClause = "APP_WATERCRAFT_DRIVER_DETAILS ";
				objWebGrid.WhereClause = " CUSTOMER_ID = '" + int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString())  + "" 
					+ "' AND APP_ID = '" + int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID","") : Request["APP_ID"].ToString())  
					+ "' AND APP_VERSION_ID = '" + int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID","") : Request["APP_VERSION_ID"].ToString()) + "'";

				objWebGrid.SearchColumnHeadings = "Driver First Name^Driver Last Name";
				objWebGrid.SearchColumnNames = "DRIVER_FNAME^DRIVER_LNAME";
				objWebGrid.DisplayColumnNames = "DRIVER_NAME";
				objWebGrid.DisplayColumnHeadings = "Driver Name";
				objWebGrid.SearchColumnType = "T^";				
				objWebGrid.OrderByClause = "DRIVER_ID ASC";				
				objWebGrid.DisplayColumnNumbers = "6";
					            

				objWebGrid.DisplayTextLength = "100";
				objWebGrid.DisplayColumnPercent = "100";
				objWebGrid.PrimaryColumns = "4";
				objWebGrid.PrimaryColumnsName = "DRIVER_ID";

				objWebGrid.ColumnTypes = "B";				
					
				objWebGrid.HeaderString = "Request MVR for the Following Drivers";
					
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6";

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireCheckbox ="Y";
//				objWebGrid.ExtraButtons = "2^Cancel Request~Request MVR^0~1^CloseWindow~FetchMVR";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				
				//objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "DRIVER_ID";
				
				//Adding to controls to gridholder
				GridHolderOpe.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		

			#endregion

		}
		#region  for delete records
		private void DeleteUDI()
		{
			string  unDisClosedIds= "'" + hidCheckedRowIDs.Value.Replace("~","','") + "'"; 
			try
			{
				int result=ClsGeneralInformation.DeleteUndisclosedDrivers(unDisClosedIds); 
				if (result==-1) 
				{
					lblErr.Text ="Some Error while deleting records"; 
					lblErr.Visible =true;
				}
				else
				{
					lblErr.Text = result + " Record/s deleted successfully."; 
					lblErr.Visible =true;
				}
			}
			catch(Exception ex)
			{
				lblErr.Text=ex.Message.ToString();
				lblErr.Visible =true;
			}


			
		}
		#endregion
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
			this.btnFetchMVR.ServerClick += new System.EventHandler(this.btnFetchMVR_ServerClick);
			this.btnFetchUDVI.ServerClick += new System.EventHandler(this.btnFetchUDVI_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region fetching  mvrinfo
		private void SetDriverViolations()
		{
			int nCount =0; 
			DataSet objDSDriverViolDetail;
			DataSet objDSDriver;
			DataSet objDSPoilcy;
			XmlDocument objDriverResponse;
			XmlNode objNode= null;
			string strPoilcyID="";
			string strPoilcyVerNo="";
			string strXmlQuery="";
			string strStateID="";
    
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation  objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			
			System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
			string strUserName = dic["UserName"].ToString();
			string strPassword = dic["Password"].ToString();
			string strAccountNumber = dic["AccountNumber"].ToString();
			string strUrl = dic["URL"].ToString();
			//Cms.CmsWeb.Utils.Utility objUtility = new Utility(strUserName, strPassword, strAccountNumber);	
			Cms.CmsWeb.Utils.Utility objMVRUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);
			int userID =int.Parse(GetUserId()); 
			string gstrLobID= GetLOBID().ToString();
			//strStateID= Request["STATE_ID"];
			int gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
			int gIntAPP_ID 			=	int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID","") : Request["APP_ID"].ToString());				
			int gIntAPP_VERSION_ID 	=	int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID","") : Request["APP_VERSION_ID"].ToString());
			//getting info about polcy and poilcy ver
			objDSPoilcy = objGenInfo.GetPoilcyInfo(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID);
			if(objDSPoilcy.Tables[0].Rows.Count > 0)
			{
				strPoilcyID=objDSPoilcy.Tables[0].Rows[0]["POLICY_ID"].ToString();
				strPoilcyVerNo=objDSPoilcy.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
			}
			else
			{
				strPoilcyID="0";
				strPoilcyVerNo="0";
			}

			//getting driver or operator list on the basis of lob and application 
			objDSDriver = objGenInfo.GetDriverList(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,gstrLobID);

			string trans_desc = "MVR Ordered on {"+DateTime.Now.ToString("MM/dd/yyyy")+"} for";
			string trans_custom = "";
			
			// for each driver or operator getting a list of violation from iix web service
			for (nCount =0; nCount <= objDSDriver.Tables[0].Rows.Count -1 ; nCount++)
			{
				int driv_chosen = 0;
				string mvr_status = "0";
				string mvr_remarks = "";
				string MVR_LICENCE_CLASS = "";
				string MVR_DRIVER_LIC_APP = "";
				if(hidDRIVER_ID.Value != "")
				{
					string []driv_ids = hidDRIVER_ID.Value.Split('~');
					for(int index=0; index < driv_ids.Length; index++)
					{
						if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString() == driv_ids[index].Trim())
						{
							driv_chosen = 1;
							break;
						}
					}
				}

				if(driv_chosen != 1)
					continue;

				string driv_state = objDSDriver.Tables[0].Rows[nCount]["STATE_NAME"].ToString();

				trans_custom += ";" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " ";
				if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"] != System.DBNull.Value)
					trans_custom += objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString() + " ";
				trans_custom += objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString();
				trans_custom += " - ";
				if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"] != System.DBNull.Value)
					trans_custom += objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"].ToString() + " / ";
				trans_custom += driv_state + " / ";
				if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"] != System.DBNull.Value)
					trans_custom += Convert.ToDateTime(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"]).ToString("MM/dd/yyyy");


				string strDriverXml = Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetAppDriverDetailsXML(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, Convert.ToInt32(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"]) );
				Cms.Model.Application.ClsDriverDetailsInfo objDriverInfo = new Cms.Model.Application.ClsDriverDetailsInfo();
				//base.PopulateModelObject(objDriverInfo, strDriverXml);

				//objDriver.getd

				string strDateOfBirth="";
				if (objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"]!= null)
				{
					strDateOfBirth=Convert.ToDateTime(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");   
				}
				strStateID=	Convert.ToString(objDSDriver.Tables[0].Rows[nCount]["STATE_ID"].ToString());
				// xml retrived from web service
				objDriverResponse = new System.Xml.XmlDocument();
				
				objDriverResponse = objMVRUtil.GetViolation(objDSDriver.Tables[0].Rows[nCount]["STATE_CODE"]==null?"":objDSDriver.Tables[0].Rows[nCount]["STATE_CODE"].ToString(),
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"].ToString(),
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString(), 
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() ,
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_SUFFIX"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_SUFFIX"].ToString() ,
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString() ,
					strDateOfBirth ,
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_SEX"]== null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_SEX"].ToString() ,"",gIntCUSTOMER_ID.ToString(),strPoilcyID,strPoilcyVerNo);


				//				objDriverResponse.LoadXml( objMVRUtil.GetUndisclosedDriver(objDSDriver.Tables[0].Rows[nCount]["STATE_CODE"]==null?"":objDSDriver.Tables[0].Rows[nCount]["STATE_CODE"].ToString(),
				//					objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"].ToString(),
				//					objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString(), 
				//					objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() ,
				//					objDSDriver.Tables[0].Rows[nCount]["DRIVER_SUFFIX"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_SUFFIX"].ToString() ,
				//					objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString() ,
				//					strDateOfBirth ,
				//					objDSDriver.Tables[0].Rows[nCount]["DRIVER_SEX"]== null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_SEX"].ToString() ,"",objDriverInfo));
				//checking if violation come from web service 
				// then enter these violation into database !!
				
				if(objDriverResponse.DocumentElement.SelectNodes("Error").Count>0)
				{
					//throw new Exception("Error:" + objDriverResponse.DocumentElement.GetAttribute("Msg").ToString());
					//throw new Exception(objDriverResponse.DocumentElement.SelectNodes("Error").Item(0).Attributes["Msg"].InnerText);
					if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"].ToString() != "")
                        htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString() ,(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + objDriverResponse.DocumentElement.SelectNodes("Error").Item(0).Attributes["Msg"].InnerText); 
					else
						htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString() ,(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + objDriverResponse.DocumentElement.SelectNodes("Error").Item(0).Attributes["Msg"].InnerText + ". No License number for this driver."); 

					continue;

				}
			
				string fetch_desc = "";
				string fetch_custom = "";
				NO_OF_UDI = 0;

				if (objDriverResponse.DocumentElement.SelectNodes("Violation").Count==0)
				{
					if (objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Count>0)
					{
						if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_LICENCE_CLASS"].InnerText !="") 
							MVR_LICENCE_CLASS=objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_LICENCE_CLASS"].InnerText;
						if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_DRIVER_LIC_APP"].InnerText !="") 
							MVR_DRIVER_LIC_APP =objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_DRIVER_LIC_APP"].InnerText;
						if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_REMARKS"].InnerText !="") 
							mvr_remarks = mvr_remarks + " " + objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_REMARKS"].InnerText;
						mvr_status  =objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_STATUS"].InnerText;
					}
					if(mvr_status != "C")
					{
						fetch_desc = "No MVR Driver Found.";
						fetch_custom = ";0 MVRs found for driver :" + (objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString());
						htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(),(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + "No MVR Driver Found."); 
					}
					else
					{
						fetch_desc = "MVR Information Updated.";
						fetch_custom = ";MVR Information Updated for driver :" + (objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString());
						htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(),(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + "MVR Information Updated."); 
					}

				}
				
				if (objDriverResponse.DocumentElement.SelectNodes("Violation").Count >0)
				{
					// if violations come from iix web service 
					//htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(),(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + "MVR Report fetched and imported successfully."); 
					int nViolationRec= objDriverResponse.DocumentElement.ChildNodes.Count; 
					string strViolationCode="";
					int nNode;
					string recordNum="";
					int mvr_found = 0;
					// getting all violation code 
					for (nNode =0; nNode<= nViolationRec - 1; nNode++)
					{
						//Added by Mohit Agarwal 25-Jul-2007 ITrack 2183
						XmlNode ViolNode=objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode);
						if (ViolNode==null) continue;
						if((recordNum = objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["viol_record"].InnerText) != "5")
						{
							if(recordNum == "1")		
								mvr_status = objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["viol_status"].InnerText;
							if(mvr_status == "1" || mvr_status == "2")		
								mvr_status = "N";
							if(recordNum == "2")
								mvr_remarks = objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["viol_remarks"].InnerText;
							continue;
						}
						if(nNode==0)	
						{
							XmlNode ViolMVRNode=objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode);
							if (ViolMVRNode!=null) 
							{
							if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_LICENCE_CLASS"].InnerText !="") 
								MVR_LICENCE_CLASS=objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_LICENCE_CLASS"].InnerText;
							if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_DRIVER_LIC_APP"].InnerText !="") 
								MVR_DRIVER_LIC_APP =objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_DRIVER_LIC_APP"].InnerText;
							if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_REMARKS"].InnerText !="") 
								mvr_remarks = mvr_remarks + " " + objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_REMARKS"].InnerText;
								mvr_status  =objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_STATUS"].InnerText;
							}
						}
						mvr_found = 1;
						if (objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["AViolation_code"].InnerText !="")
						{
							if (strViolationCode =="")
							{
								strViolationCode ="'"+objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["AViolation_code"].InnerText +"'"; 
							}
							else
							{
								strViolationCode =strViolationCode +",'"+ objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["AViolation_code"].InnerText +"'"; 
							}
						}
					}
					if(mvr_found == 0)
					{
						if(mvr_status == "1" || mvr_status == "2" || mvr_status == "N") //"1" or "2" indicates a NOT FOUND
						{
							fetch_desc = "No MVR Driver Found.";
							fetch_custom = ";0 MVRs found for driver :" + (objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString());
							htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(),(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + "No MVR Driver Found."); 
						}
						else
						{
							fetch_desc = "MVR Information Updated.";
							fetch_custom = ";MVR Information Updated for driver :" + (objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString());
							htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(),(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + "MVR Information Updated."); 
						}
					}
					else
					{
						if(mvr_status != "" && mvr_status != " " && mvr_status != "V")
						{
							fetch_desc = "MVR Report available and imported successfully.";
							htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(),(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + "MVR Report available and imported successfully."); 
						}
						else
						{
							fetch_desc = "MVR Information Updated.";
							htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(),(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + "MVR Information Updated."); 
						}
					}

					if(strViolationCode=="")
					{
						strViolationCode="''";
					}

					// mapp all violation code with wolverine violation code and get details 
					if(gstrLobID == "1")
						objDSDriverViolDetail= objGenInfo.GetViolationRecords(strViolationCode,"4",strStateID);
					else
						objDSDriverViolDetail= objGenInfo.GetViolationRecords(strViolationCode,gstrLobID,strStateID);
					
						// creating a mver object to store all info retrived 
					ClsMvrInfo objMVRInfo= new ClsMvrInfo();
					//Cms.Model.Policy.ClsPolicyAutoMVR objMVRInfo= new Cms.Model.Policy.ClsPolicyAutoMVR(); 
					objMVRInfo.APP_ID =gIntAPP_ID;
					objMVRInfo.CUSTOMER_ID = gIntCUSTOMER_ID;
					objMVRInfo.APP_VERSION_ID = gIntAPP_VERSION_ID;
					objMVRInfo.CREATED_BY= objMVRInfo.MODIFIED_BY=userID; 
					objMVRInfo.CREATED_DATETIME = DateTime.Now;  

					string[] objArrCode = strViolationCode.Split(','); 
					bool bIsExist; 
					string strDescription="";
					string strVcode =""; 
					string strVtype =""; 
					string strVpoints =""; 
					// checking which iix violations mapped with wolverine violation 
					for ( nNode=0; nNode<= objArrCode.Length  - 1; nNode++)
					{
						strDescription="";
						strVcode =""; 
						bIsExist= false; 
						if (objDSDriverViolDetail != null)
						{
							if(objDSDriverViolDetail.Tables[0]!= null)
							{
								for (int nRecord =0; nRecord <=objDSDriverViolDetail.Tables[0].Rows.Count -1 ;nRecord ++)
								{
									string strSSVCode= objDSDriverViolDetail.Tables[0].Rows[nRecord]["SSV_CODE"].ToString().Trim();
									string strVCode = objArrCode[nNode].Replace("'","").Trim()   ;
									if( strSSVCode == strVCode  )
									{
										bIsExist= true;
										break;
									}
								}
							}
						}
						// if iix violation not mapped then put this entry in exception table for future use 
						if (bIsExist== false)
						{
							if(objArrCode[nNode].ToString() != "''")
							{
								strXmlQuery="/ResultData/Violation[@AViolation_code="+objArrCode[nNode].ToString().Trim()+"]";
								objNode=objDriverResponse.DocumentElement.SelectSingleNode(strXmlQuery); 
								if (objNode==null) continue;
								strDescription=objNode.Attributes["viol_description"].InnerText.Trim()   ; 
								strVcode =objNode.Attributes["AViolation_code"].InnerText.Trim();
								strVtype =objNode.Attributes["viol_type"].InnerText.Trim();  
								strVpoints =objNode.Attributes["APoints"].InnerText.Trim();
								string strMVRDate =objNode.Attributes["viol_date"].InnerText.Trim(); 
								string strMVRConvectionDate =objNode.Attributes["conviction_date"].InnerText.Trim(); 

								if (strMVRDate!="")
								{
									strMVRDate=strMVRDate.Substring(0,2).ToString()  + "/" +  strMVRDate.Substring(2,2) + "/" + strMVRDate.Substring(4,4);
									//objMVRInfo.MVR_DATE= Convert.ToDateTime(strMVRDate);
									objMVRInfo.OCCURENCE_DATE= Convert.ToDateTime(strMVRDate);
								}
								if (strMVRConvectionDate!="")
								{
									strMVRConvectionDate=strMVRConvectionDate.Substring(0,2).ToString()  + "/" +  strMVRConvectionDate.Substring(2,2) + "/" + strMVRConvectionDate.Substring(4,4);
									//objMVRInfo.MVR_DATE= Convert.ToDateTime(strMVRDate);
									objMVRInfo.MVR_DATE  = Convert.ToDateTime(strMVRConvectionDate);
								}	
								objMVRInfo.DRIVER_ID =int.Parse(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString()) ;
								//objGenInfo.InsertUnmatchedMVRViolationDetail(objMVRInfo,strVcode,strDescription,gIntAPP_ID.ToString(),gIntAPP_VERSION_ID.ToString(),false);
								int resultID=objGenInfo.InsertUnmatchedMVRViolationDetail(objMVRInfo,strVcode,strDescription,gIntAPP_ID.ToString(),gIntAPP_VERSION_ID.ToString(),strPoilcyID,strPoilcyVerNo,false);
								//objGenInfo.InsertUnmatchedMVRViolationDetail(objMVRInfo,strVcode,strDescription,strPoilcyID,strPoilcyVerNo,false);
								if (resultID==-1) continue;
								NO_OF_EXC_MVR=NO_OF_EXC_MVR+1;
//								objMVRInfo.MVR_DEATH ="N"; 
//								objMVRInfo.MVR_AMOUNT=0; 
								objMVRInfo.VIOLATION_ID =resultID;
								objMVRInfo.IS_ACTIVE ="Y";
								//objMVRInfo.VIOLATION_TYPE =int.Parse("13220");
								Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
								
								//Added by Mohit Agarwal 3-Jul-07
								try
								{
									if(strVtype.StartsWith("ADMI") || strVtype.StartsWith("INFO") || strVtype.StartsWith("ACCI") || strVtype.StartsWith("REIN") || strVtype.StartsWith("REVO") || strVtype.StartsWith("CANC") || strVtype.StartsWith("DISQ") || strVtype.StartsWith("DENI") || strVtype.StartsWith("UNKN") || strVtype.StartsWith("GISM")) // || strVtype.StartsWith("SUSP") || (objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null))
									{
										DataSet dsViol = objMvr.GetMNTViolationInfo(int.Parse(gstrLobID),int.Parse(strStateID),strVtype);
										if(dsViol != null && dsViol.Tables[0].Rows.Count > 0)
										{
											objMVRInfo.VIOLATION_TYPE = int.Parse(dsViol.Tables[0].Rows[0]["VIOLATION_ID"].ToString());
											objMVRInfo.VIOLATION_ID =0;
										}

										objMVRInfo.VERIFIED = 1;
										objMVRInfo.DETAILS = strDescription;
									}
									else
									{
										objMVRInfo.VIOLATION_TYPE = 0;
										objMVRInfo.DETAILS = strDescription;
										objMVRInfo.VERIFIED = 1;
									}
									objMVRInfo.ADJUST_VIOLATION_POINTS = 100;
									if(strVpoints=="") strVpoints="0";
									objMVRInfo.POINTS_ASSIGNED = int.Parse(strVpoints);
								}
								catch(Exception ex)
								{
                                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
								}
								//if ppa or motercycle
								if (gstrLobID == "4")
								{
									objMvr.AddForWater(objMVRInfo);
									NO_OF_UDI=NO_OF_UDI +1;
								}
								else if (gstrLobID == "2" )
								{
									objMvr.Add(objMVRInfo, "PPA");
									NO_OF_UDI=NO_OF_UDI +1;
								}
								else if ( gstrLobID == "3")
								{
									objMvr.Add(objMVRInfo, "MOT");
									NO_OF_UDI=NO_OF_UDI +1;
								}
								fetch_custom = ";" + NO_OF_UDI.ToString() + " MVRs found for driver :" + (objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString());
								////
							}
						}

					}
					// now insert all mapped violations and its details 
					if (objDSDriverViolDetail != null)
					{
						if (objDSDriverViolDetail.Tables[0] != null)
						{
							int ViolCount=objDriverResponse.DocumentElement.SelectNodes("Violation").Count;
							//for (int nRecord =0; nRecord <=objDSDriverViolDetail.Tables[0].Rows.Count -1  ;nRecord ++)
							for (int nRecord =0; nRecord <= ViolCount -1 ;nRecord ++)
							{
								//strXmlQuery="/ResultData/Violation[@AViolation_code='"+objDSDriverViolDetail.Tables[0].Rows[nRecord]["SSV_CODE"].ToString().Trim()  +"']";
								XmlNode ViolNode=objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nRecord);
								if (ViolNode==null) continue;
								//objNode=objDriverResponse.DocumentElement.SelectSingleNode(strXmlQuery); 
								objNode=ViolNode;
								strDescription=objNode.Attributes["viol_description"].InnerText.Trim()   ; 
								strVcode =objNode.Attributes["AViolation_code"].InnerText.Trim();
								strVtype =objNode.Attributes["viol_type"].InnerText.Trim();  
								strVpoints =objNode.Attributes["APoints"].InnerText.Trim();
						        if (strVcode=="") continue;
								objMVRInfo.DRIVER_ID=  int.Parse (objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"]==null?"0": objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString() ); 
								if (objNode.Attributes["viol_date"].InnerText !="")
								{
									string date = objNode.Attributes["viol_date"].InnerText ; 
									//							DateTimeFormatInfo myDTFI = new DateTimeFormatInfo();
									//							myDTFI.ShortDatePattern = "ddMMyyyy";
									//							DateTime dt = DateTime.Parse(date, myDTFI);
									objMVRInfo.MVR_DATE		 = new DateTime(int.Parse( date.Substring(4,4)),int.Parse(date.Substring(0,2)),int.Parse(date.Substring(2,2))) ;
									objMVRInfo.OCCURENCE_DATE= new DateTime(int.Parse( date.Substring(4,4)),int.Parse(date.Substring(0,2)),int.Parse(date.Substring(2,2))) ;
								}
								if (objNode.Attributes["conviction_date"].InnerText !="")
								{
									string Convictiondate = objNode.Attributes["conviction_date"].InnerText ; 
										objMVRInfo.MVR_DATE= new DateTime(int.Parse( Convictiondate.Substring(4,4)),int.Parse(Convictiondate.Substring(0,2)),int.Parse(Convictiondate.Substring(2,2))) ;
								}

//								objMVRInfo.MVR_DEATH ="N"; 
//								objMVRInfo.MVR_AMOUNT=0; 
								//objMVRInfo.VIOLATION_ID =int.Parse ( objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null?"0":objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"].ToString()) ;
									DataRow[] dr= objDSDriverViolDetail.Tables[0].Select("SSV_CODE='" + strVcode + "'");
									if (dr.Length>0)
									{
										objMVRInfo.VIOLATION_ID =int.Parse (dr[0]["VIOLATION_ID"]==null?"0":dr[0]["VIOLATION_ID"].ToString());
										objMVRInfo.IS_ACTIVE =dr[0]["IS_ACTIVE"]== null?"":dr[0]["IS_ACTIVE"].ToString() ;
										strVpoints=dr[0]["MVR_POINTS"]==null?"0":dr[0]["MVR_POINTS"].ToString();
									}
									else
									{
										objMVRInfo.VIOLATION_ID =0;
										objMVRInfo.IS_ACTIVE="";
									}
								//objMVRInfo.VIOLATION_TYPE =int.Parse("13220");
								Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
								// if watercraft 
								
								//Added by Mohit Agarwal 3-Jul-07
								try
								{
									if(strVtype.StartsWith("ADMI") || strVtype.StartsWith("INFO") || strVtype.StartsWith("ACCI") || strVtype.StartsWith("REIN") || strVtype.StartsWith("REVO") || strVtype.StartsWith("CANC") || strVtype.StartsWith("DISQ") || strVtype.StartsWith("DENI") || strVtype.StartsWith("UNKN") || strVtype.StartsWith("GISM")) // || strVtype.StartsWith("SUSP") || (objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null))
									{
										DataSet dsViol = objMvr.GetMNTViolationInfo(int.Parse(gstrLobID),int.Parse(strStateID),strVtype);
										if(dsViol != null && dsViol.Tables[0].Rows.Count > 0)
										{
											objMVRInfo.VIOLATION_TYPE = int.Parse(dsViol.Tables[0].Rows[0]["VIOLATION_ID"].ToString());
											objMVRInfo.VIOLATION_ID =0;
										}

										objMVRInfo.VERIFIED = 1;
										objMVRInfo.DETAILS = strDescription;
									}
									else
									{
										objMVRInfo.VIOLATION_TYPE = 0;
										objMVRInfo.VERIFIED = 1;
										objMVRInfo.DETAILS = strDescription;
									}
									objMVRInfo.ADJUST_VIOLATION_POINTS = 100;
									if(strVpoints=="") strVpoints="0";
									objMVRInfo.POINTS_ASSIGNED = int.Parse(strVpoints);
								}
								catch(Exception ex)
								{throw(ex);
								}

								if (gstrLobID == "4")
								{
									objMvr.AddForWater(objMVRInfo);
									NO_OF_UDI=NO_OF_UDI +1;
								}
									//if ppa or motercycle
								else if (gstrLobID == "2" )
								{
									objMvr.Add(objMVRInfo, "PPA");
									NO_OF_UDI=NO_OF_UDI +1; 
								}
								else if ( gstrLobID == "3")
								{
									objMvr.Add(objMVRInfo, "MOT");
									NO_OF_UDI=NO_OF_UDI +1;
								}
								fetch_custom = ";" + NO_OF_UDI.ToString() + " MVRs found for driver :" + (objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString());
							}
						}
					}

				}
				try
				{
					objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, fetch_desc, int.Parse(GetUserId()),fetch_custom, "Application");
					objGenInfo.SetMvrOrdered(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,Convert.ToInt32(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"]),gstrLobID,mvr_remarks, mvr_status, "Application",MVR_LICENCE_CLASS,MVR_DRIVER_LIC_APP);
				}
				catch(Exception ex)
				{
					throw(ex);
				}

													
			}	
			if(trans_custom != "")
				objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");
		}
		private string FetchValueFromXML(string nodeName, string XMLString)
		{
			try 
			{
				string strRetval="";
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(XMLString);
				XmlNodeList nodList = doc.GetElementsByTagName(nodeName);
				if (nodList.Count >0)
				{
					strRetval=nodList.Item(0).InnerText;
				}
				return strRetval;

			
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		
		
		}
		/// <summary>
		/// fetch undisclosed drivers
		/// </summary>
		/// <param name="e">pravesh chandel</param>
		
		private void FetchUndisclosedDrivers()
		{
			//int nCount =0; 
			
			//DataSet objDSDriverViolDetail;
			//DataSet objDSDriver;
			//DataSet objDSPoilcy;
			//XmlDocument objDriverResponse;
			//XmlNode objNode= null;
			//string strPoilcyID="";
			//string strPoilcyVerNo="";
			//string strXmlQuery="";
			//string strStateID="";
			NO_OF_UDI=0;
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
            System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
			string strUserName = dic["UserName"].ToString();
			string strPassword = dic["Password"].ToString().PadRight(10,' ');
			string strAccountNumber = dic["AccountNumber"].ToString();
			string strUrl = dic["URL"].ToString();
			int userID =int.Parse(GetUserId());
			string gstrLobID= Request["LOB_ID"];
			int gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
			int gIntAPP_ID 			=	int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID","") : Request["APP_ID"].ToString());				
			int gIntAPP_VERSION_ID 	=	int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID","") : Request["APP_VERSION_ID"].ToString());
			//getting info about polcy and poilcy ver

			//Added by Mohit Agarwal ITrack 2135 27-Jul-2007
			Cms.BusinessLayer.BlClient.ClsCustomer objCustomer = new ClsCustomer();
			//Done for Itrack Issue 6565 on 6 Nov 09
			//string strcustXML = objCustomer.FillCustomerDetails(gIntCUSTOMER_ID);
			//Done for Itrack Issue 6816 on 14 Dec 09
			string strcustXML ="";
			if(gstrLobID != "2")
			{
				strcustXML = objCustomer.FillCustomerDetails(gIntCUSTOMER_ID);
			}
			else
				strcustXML = objCustomer.FillCustomer_Vehicle_Details(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,"APP");
			

			btnFetchUDVI.Visible = true;

			Cms.Model.Client.ClsCustomerInfo objCustomerInfo = new Cms.Model.Client.ClsCustomerInfo();
			if(strcustXML != "")
			{
				Cms.CmsWeb.cmsbase objBase=new Cms.CmsWeb.cmsbase();
				objBase.PopulateModelObject(objCustomerInfo, strcustXML);
				
				try
				{
                    string trans_desc = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1513");// "Undisclosed Driver Vehicle Report was requested";
					string trans_custom = ";Names Insured: ";
					DataSet dsAppl = objGenInfo.GetNamedInsuredList(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, "APPLICATION");
					if(dsAppl != null && dsAppl.Tables.Count > 1 && dsAppl.Tables[1].Rows.Count > 0)
					{
						foreach(DataRow drIns in dsAppl.Tables[1].Rows)
						{
							if(trans_custom != ";Names Insured: ")
								trans_custom += " & " + drIns["APPNAME"].ToString();
							else
								trans_custom += drIns["APPNAME"].ToString();
						}
					}
					objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");
				}
				catch(Exception ex)
				{
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}

				string strWebResponse = "";
				Cms.CmsWeb.Utils.Utility objUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);

				string errMsg = "";
				if(NewUDVI == "")
				{
					DataSet dsUDVI = objGenInfo.GetUDVIReport(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, "Application");
					if(dsUDVI != null && dsUDVI.Tables.Count > 0 && dsUDVI.Tables[0].Rows.Count > 0)
						errMsg = dsUDVI.Tables[0].Rows[0]["REPORT_HTML"].ToString();
				}
				if(errMsg == "")
				{
					errMsg = objUtil.GetUndisclosedDriverVehicle("000", "UDV", objCustomerInfo, ref strWebResponse);
					if(errMsg!=null && errMsg!="" && errMsg.IndexOf("Error Msg",0)<0)
						objGenInfo.AddUDVIReport(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, int.Parse(GetUserId()), errMsg, "Application");

					try
					{
						string trans_desc = "";
						string trans_custom = "";
						if(strWebResponse.StartsWith("Accept"))
							trans_desc = "Report inquiry accepted under request Id :" + strWebResponse.Substring(7);
						else if (strWebResponse.StartsWith("Error"))
							trans_desc = "Report inquiry gave an error :" + strWebResponse.Substring(6);
						else
							trans_desc = "Report inquiry gave an error.";

						trans_custom = ";Primary Applicant: " + objCustomerInfo.CustomerFirstName + " ";
						if(objCustomerInfo.CustomerMiddleName.Trim() != "")
							trans_custom += objCustomerInfo.CustomerMiddleName + " ";
						trans_custom += objCustomerInfo.CustomerLastName;
						objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");
					}
					catch(Exception ex)
					{
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					}

				}
				
				//lblErr.Text = objUtil.GetUndisclosedDriverVehicle("000", "UDV", objCustomerInfo, ref strWebResponse);
				
				//Added by Asfa(14-July-2008) - iTrack #4460
				if(errMsg!=null && errMsg!="" && errMsg.IndexOf("Error Msg",0)>0)
				{
					errMsg = errMsg.Replace("\"","'");
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(errMsg);
					XmlNode node = null;
					node  = doc.SelectSingleNode("ResultData/Error/@Msg");
					lblErr.Text = node.InnerText.ToString();
					btnCloseDriv.Visible=false;
					btnFetchMVR.Visible=false;
				}
				else
					lblErr.Text = errMsg;
			}


			// Commented for ITRack 2135 27-Jul 2007
/*			objDSPoilcy = objGenInfo.GetPoilcyInfo(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID);
			if(objDSPoilcy.Tables[0].Rows.Count > 0)
			{
				strPoilcyID=objDSPoilcy.Tables[0].Rows[0]["POLICY_ID"].ToString();
				strPoilcyVerNo=objDSPoilcy.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
			}
			//getting driver or operator list on the basis of lob and application 
			objDSDriver = objGenInfo.GetDriverList(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,gstrLobID);

			
			Cms.Model.Application.ClsDriverDetailsInfo[] objDrivers = new Cms.Model.Application.ClsDriverDetailsInfo[objDSDriver.Tables[0].Rows.Count];

			// for each driver or operator getting a list of violation from iix web service
			for (nCount =0; nCount <= objDSDriver.Tables[0].Rows.Count -1 ; nCount++)
			{
				string strDriverXml = ClsDriverDetail.GetAppDriverDetailsXML(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, Convert.ToInt32(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"]) );
				objDrivers[nCount] = new Cms.Model.Application.ClsDriverDetailsInfo();
				Cms.CmsWeb.cmsbase objBase=new Cms.CmsWeb.cmsbase();
				objBase.PopulateModelObject(objDrivers[nCount], strDriverXml);

				string strDateOfBirth="";
				if (objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"]!= null)
				{
					strDateOfBirth=Convert.ToDateTime(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");   
				}
			Cms.CmsWeb.Utils.Utility objUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);
			//System.Collections.Specialized.StringCollection DriversCol = objUtil.GetUndisclosedDrivers(objDrivers);
				string resultData=objUtil.GetUndisclosedDriver(objDrivers[nCount].STATE_CODE.ToString(),objDrivers[nCount].DRIVER_DRIV_LIC.ToString(),objDrivers[nCount].DRIVER_LNAME.ToString(),objDrivers[nCount].DRIVER_FNAME," "," ",strDateOfBirth,objDrivers[nCount].DRIVER_SEX," ",objDrivers[nCount] );
				XmlDocument xmlDoc=new XmlDocument();
				xmlDoc.LoadXml(resultData);
				XmlNode driverNode=xmlDoc.SelectSingleNode("ResultData"); 
				//if (driverNode.ChildNodes[0].Attributes[0].Value =="Msg") 
				if (xmlDoc.DocumentElement.SelectNodes("Error").Count>0) 
					throw new Exception(driverNode.ChildNodes[0].Attributes["Msg"].Value); 
				for (int indexDriver=0;indexDriver<driverNode.ChildNodes.Count;indexDriver++)
				{
					string strDriverName=driverNode.ChildNodes[indexDriver].Attributes["name"].Value;  
					string driverID=objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(); 
					objGenInfo.InsertUndisclosedDriverDetail( gIntCUSTOMER_ID.ToString(),gIntAPP_ID.ToString(),gIntAPP_VERSION_ID.ToString(),driverID,strDriverName.Trim());
					NO_OF_UDI=NO_OF_UDI+1;
				}
 
objWebGrid.WebServiceURL = httpProtocol
			}
*/					//Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo=new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			/*		for(int driverCount=0;driverCount<DriversCol.Count;driverCount++)
			{
				if (DriversCol[driverCount].ToString() !="")
				{
					objGenInfo.InsertUndisclosedDriverDetail( gIntCUSTOMER_ID.ToString(),gIntAPP_ID.ToString(),gIntAPP_VERSION_ID.ToString(),"4",DriversCol[driverCount].ToString());
					NO_OF_UDI=NO_OF_UDI+1;
				}
			}*/
			
		}
		#endregion

		private void btnFetchMVR_ServerClick(object sender, System.EventArgs e)
		{
//			lblMessage.Text="Fetching MVRs, please wait...";
			if (Request["CalledFor"].ToString()=="MVR")
			{
				FetchOperatorsMVR();
			}
		}

		private void btnFetchUDVI_ServerClick(object sender, System.EventArgs e)
		{
			NewUDVI = "y";
			FetchUndisclosedDrivers();
			lblErr.Visible = true;
		}
	}
}
