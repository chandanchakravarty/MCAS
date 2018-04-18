/******************************************************************************************

Created By				: Mohit Agarwal
Dated					: 2 Jan 2007
Purpose					: for Prior Loss from IIX response to add to DB and display in Grid   
 
<Modified Date			: - > 
<Modified By			: - > 
<Purpose				: - > 
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
using Cms.Model.Application.PriorLoss;

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for LossReport.
	/// </summary>
	public class LossReport : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.PlaceHolder GridHolderOpe;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnCloseDriv;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnFetchMVR;
		protected System.Web.UI.WebControls.Label lblMessage;
		private int gIntCUSTOMER_ID;
		int gIntAPP_ID;
		int gIntAPP_VERSION_ID;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//btnCloseDriv.Attributes.Add("onclick","javascript:CloseWindow();");

			if(!Page.IsPostBack)
			{
				//SetDriverAutoLoss();
				//Done for Itrack Issue 6708 on 19 Nov 09
				if(Request["CalledFor"].ToString()=="PriorPolicy")
				{
					//Done for Itrack Issue 6708 on 14 Dec 09
					FetchPriorPolicy();
                    string JavascriptText = "parent.document.title = '" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2072") + "';";
					ClientScript.RegisterStartupScript(this.GetType(),"RefreshGRid","<script language='javascript'>" + JavascriptText + "window.opener.location.reload(true);</script>");
					btnCloseDriv.Visible = false;
					btnFetchMVR.Visible = false;
				}
			}
			if(Request["CalledFor"].ToString()!="PriorPolicy")
			{
				ShowDrivers();
			}
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
					break;
				case 4:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();     
					break;
			}

			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion     
    
			// Put user code to initialize the page here
		}
		private void ShowDrivers()
		{
			#region loading web grid control
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{
				string gstrLobID= GetLOBID().ToString();
				//Setting web grid control properties
				
				objWebGrid.WebServiceURL =   httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = "CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID, ISNULL(DRIVER_FNAME,'')+ ' ' + ISNULL(DRIVER_MNAME,'')+ ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME";
				
				//Condition added by Charles on 2-Jul-09 for Itrack 6037
				if(gstrLobID == "2" || gstrLobID == "3")
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
					
				objWebGrid.HeaderString = "Request Loss Report for the Following Drivers";
					
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
			this.btnFetchMVR.ServerClick += new System.EventHandler(this.btnFetchMVR_ServerClick);

		}
		#endregion

		private void btnFetchMVR_ServerClick(object sender, System.EventArgs e)
		{
			if(hidDRIVER_ID.Value == "")
				return;
			
			lblMessage.Text="";
			//			lblMessage.Text="Fetching MVRs, please wait...";
			//SetDriverAutoLoss();
			SetDriverAutoLoss_NEW();

//			#region loading web grid control
//			Control c1= LoadControl("../../cmsweb/webcontrols/BaseDataGrid.ascx");
//			try
//			{
//                
//				/*************************************************************************/
//				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
//				/************************************************************************/
//				//specifying webservice URL
//				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
//				//specifying columns for select query
//				((BaseDataGrid)c1).SelectClause ="PL.CUSTOMER_ID,PL.LOSS_ID,CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101) OCCURENCE_DATE,PL.LOB,PL.LOSS_TYPE,CONVERT(VARCHAR(15),PL.AMOUNT_PAID,1) AMOUNT_PAID,CONVERT(VARCHAR(15),PL.AMOUNT_RESERVED,1) AMOUNT_RESERVED,PL.CLAIM_STATUS,PL.LOSS_DESC,PL.REMARKS,PL.MOD,PL.LOSS_RUN,PL.CAT_NO,PL.CLAIMID,PL.IS_ACTIVE,PL.CREATED_BY,PL.CREATED_DATETIME,PL.MODIFIED_BY,PL.LAST_UPDATED_DATETIME,isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101),'') + ' ' +  (CASE PL.LOB WHEN '-1' THEN 'Other' WHEN '' THEN '' ELSE LV.LOB_DESC END) LOSS,LV.LOB_DESC LOB1,LV.LOB_ID , MLV.LOOKUP_VALUE_DESC   ";
//				//specifying tables for from clause
//				((BaseDataGrid)c1).FromClause=" APP_PRIOR_LOSS_INFO PL LEFT JOIN MNT_LOB_MASTER LV ON PL.LOB=CONVERT(VARCHAR,LV.LOB_ID)    LEFT JOIN MNT_LOOKUP_VALUES MLV ON PL.LOSS_TYPE=MLV.LOOKUP_UNIQUE_ID    ";
//				//specifying conditions for where clause
//				((BaseDataGrid)c1).WhereClause=" PL.CUSTOMER_ID=" + gIntCUSTOMER_ID + " AND PL.DRIVER_NAME LIKE '" + gIntCUSTOMER_ID.ToString() + "^" + gIntAPP_ID.ToString() + "^" + gIntAPP_VERSION_ID.ToString() + "%'";
//				//specifying Text to be shown in combo box
//				((BaseDataGrid)c1).SearchColumnHeadings="Losses^Date of Occurence^Line of Business";
//				((BaseDataGrid)c1).DropDownColumns="^^LOB";	
//				//specifying column to be used for combo box
//				((BaseDataGrid)c1).SearchColumnNames="(isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101),'') ! ' ' !  isnull(LV.LOB_DESC,''))^PL.OCCURENCE_DATE^LV.LOB_ID";
//				//search column data type specifying data type of the column to be used for combo box
//				((BaseDataGrid)c1).SearchColumnType="T^D^T";
//				//specifying column for order by clause
//				((BaseDataGrid)c1).OrderByClause="LOSS asc";
//				//specifying column numbers of the query to be displyed in grid
//				((BaseDataGrid)c1).DisplayColumnNumbers="21^3^22^23^6";
//				//specifying column names from the query
//				((BaseDataGrid)c1).DisplayColumnNames="LOSS_ID^LOSS^OCCURENCE_DATE^LOB1^LOOKUP_VALUE_DESC^AMOUNT_PAID";
//				//specifying text to be shown as column headings
//				((BaseDataGrid)c1).DisplayColumnHeadings="Id^Losses^Date of Occurrence^Line of Business^Loss Type^Amount Paid";
//				//specifying column heading display text length
//				((BaseDataGrid)c1).DisplayTextLength="5^20^20^15^20^20";
//				//specifying width percentage for columns
//				((BaseDataGrid)c1).DisplayColumnPercent="5^20^20^15^20^20";
//				//specifying primary column number
//				((BaseDataGrid)c1).PrimaryColumns="2";
//				//specifying primary column name
//				((BaseDataGrid)c1).PrimaryColumnsName="PL.LOSS_ID^PL.CUSTOMER_ID";
//				//specifying column type of the data grid
//				((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B";
//				//specifying links pages 
//				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
//				//specifying if double click is allowed or not
//				((BaseDataGrid)c1).AllowDBLClick="true"; 
//				//specifying which columns are to be displayed on first tab
//				((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23";
//				//specifying message to be shown
//				((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
//				//specifying buttons to be displayed on grid
//				((BaseDataGrid)c1).ExtraButtons="1^OK^0^OkClicked";
//				//specifying number of the rows to be shown
//				((BaseDataGrid)c1).PageSize=int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));
//				//specifying cache size (use for top clause)
//				((BaseDataGrid)c1).CacheSize=int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));
//				//specifying image path
//				((BaseDataGrid)c1).ImagePath=System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
//				((BaseDataGrid)c1).HImagePath=System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
//				//specifying heading
//				((BaseDataGrid)c1).HeaderString = "Prior Loss";
//				//((BaseDataGrid)c1).SelectClass = colors;
//				//specifying text to be shown for filter checkbox
//				//((BaseDataGrid)c1).FilterLabel="Show Complete";
//				//specifying column to be used for filtering
//				//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
//				//value of filtering record
//				//((BaseDataGrid)c1).FilterValue="Y";
//				// property indiacating whether query string is required or not
//				((BaseDataGrid)c1).RequireQuery ="Y";
//				// column numbers to create query string
//				((BaseDataGrid)c1).QueryStringColumns ="LOSS_ID^CUSTOMER_ID";
//				((BaseDataGrid)c1).DefaultSearch="Y";
//				// to show completed task we have to give check box
//				GridHolder.Controls.Add(c1);
//			}
//			catch(Exception ex)
//			{
//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
//			}
//        
//			#endregion

		}
		private void SetDriverAutoLoss_NEW()
		{
			int nCount =0; 
			DataSet objDSDriver;
			DataSet objDSPoilcy;
			XmlDocument objDriverResponse;
			string strPoilcyID="";
			string strPoilcyVerNo="";
			string strStateID="";
			string retAutoLoss;
			try
			{
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation  objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			
				System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
				string strUserName = dic["UserName"].ToString();
				string strPassword = dic["Password"].ToString();
				string strAccountNumber = dic["AccountNumber"].ToString();
				string strUrl = dic["URL"].ToString();
				Cms.CmsWeb.Utils.Utility objMVRUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);
				int userID =int.Parse(GetUserId()); 
				string gstrLobID= GetLOBID().ToString();
				//strStateID= Request["STATE_ID"];
				gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
				gIntAPP_ID 			=	int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID","") : Request["APP_ID"].ToString());				
				gIntAPP_VERSION_ID 	=	int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID","") : Request["APP_VERSION_ID"].ToString());
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
				/////////////
				string strDateOfBirth="";
				string trans_desc = "";
				string trans_custom = "";
				string ReportStatus = "";
				int index=0,rowCount=0;
				Cms.Model.Application.ClsDriverDetailsInfo objDriverInfo = new Cms.Model.Application.ClsDriverDetailsInfo();
				if(objDSDriver.Tables[0].Rows.Count>0)
				{
					int driv_chosen = 0;
					trans_desc = "Auto Loss Report was requested on {"+DateTime.Now.ToString("MM/dd/yyyy")+"} for";
					trans_custom = "";
					ReportStatus = "";
				
					if(hidDRIVER_ID.Value != "")
					{
						string []driv_ids = hidDRIVER_ID.Value.Split('~');
						for(rowCount=0; rowCount < driv_ids.Length; rowCount++)
						{
							for(index=0; index < objDSDriver.Tables[0].Rows.Count; index++)
							{
								if(objDSDriver.Tables[0].Rows[index]["DRIVER_ID"].ToString() == driv_ids[0].Trim())
								{
									driv_chosen = 1;
									break;
								}
							}
						}
					}

					if(driv_chosen != 1)
						return;

				
					string driv_state = objDSDriver.Tables[0].Rows[index]["STATE_NAME"].ToString();

					trans_custom += ";" + objDSDriver.Tables[0].Rows[index]["DRIVER_FNAME"].ToString() + " ";
					if(objDSDriver.Tables[0].Rows[index]["DRIVER_MNAME"] != System.DBNull.Value)
						trans_custom += objDSDriver.Tables[0].Rows[index]["DRIVER_MNAME"].ToString() + " ";
					trans_custom += objDSDriver.Tables[0].Rows[index]["DRIVER_LNAME"].ToString();
					trans_custom += " - ";
					if(objDSDriver.Tables[0].Rows[index]["DRIVER_DRIV_LIC"] != System.DBNull.Value)
						trans_custom += objDSDriver.Tables[0].Rows[index]["DRIVER_DRIV_LIC"].ToString() + " / ";
					trans_custom += driv_state + " / ";
					if(objDSDriver.Tables[0].Rows[index]["DRIVER_DOB"] != System.DBNull.Value)
						trans_custom += Convert.ToDateTime(objDSDriver.Tables[0].Rows[index]["DRIVER_DOB"]).ToString("MM/dd/yyyy");
				
					objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");
					//string strDriverXml = Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetAppDriverDetailsXML(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, Convert.ToInt32(objDSDriver.Tables[0].Rows[index]["DRIVER_ID"]) );
					//Cms.Model.Application.ClsDriverDetailsInfo objDriverInfo = new Cms.Model.Application.ClsDriverDetailsInfo();
					//base.PopulateModelObject(objDriverInfo, strDriverXml);
					objDriverInfo.CUSTOMER_ID = gIntCUSTOMER_ID;
					objDriverInfo.APP_ID = gIntAPP_ID;
					objDriverInfo.APP_VERSION_ID = gIntAPP_VERSION_ID;
					objDriverInfo.DRIVER_SSN = objDSDriver.Tables[0].Rows[index]["DRIVER_SSN"]==null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_SSN"].ToString();
					objDriverInfo.DRIVER_CITY = objDSDriver.Tables[0].Rows[index]["DRIVER_CITY"]==null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_CITY"].ToString();
					objDriverInfo.DRIVER_ZIP = objDSDriver.Tables[0].Rows[index]["DRIVER_ZIP"]==null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_ZIP"].ToString();
					objDriverInfo.DRIVER_ADD1 = objDSDriver.Tables[0].Rows[index]["DRIVER_ADD1"]==null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_ADD1"].ToString();
				
					if (objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"]!= null)
					{
						strDateOfBirth=Convert.ToDateTime(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");   
					}
					strStateID=	Convert.ToString(objDSDriver.Tables[0].Rows[nCount]["STATE_ID"].ToString());
					//objDriver.getd
				}

				/////////////
				string strAdditionalDrivers="";
				int BlockId=1;
				// for each addtional driver or operator getting a list of violation from iix web service
				for (nCount =0; nCount <= objDSDriver.Tables[0].Rows.Count -1 ; nCount++)
				{
					if (nCount==5) break;
					int driv_chosen = 0;
					string trans_desc1 = "Auto Loss Report was requested on {"+DateTime.Now.ToString("MM/dd/yyyy")+"} for";
					string trans_custom1 = "";

					if(hidDRIVER_ID.Value != "")
					{
						string []driv_ids = hidDRIVER_ID.Value.Split('~');
						for(int index1=0; index1 < driv_ids.Length; index1++)
						{
							if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString() == driv_ids[index1].Trim() && objDSDriver.Tables[0].Rows[index]["DRIVER_ID"].ToString()!= driv_ids[index1].Trim())
							{
								driv_chosen = 1;
								break;
							}
						}
					}

					if(driv_chosen != 1)
						continue;

				
					string driv_state = objDSDriver.Tables[0].Rows[nCount]["STATE_NAME"].ToString();

					trans_custom1 += ";" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " ";
					if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"] != System.DBNull.Value)
						trans_custom1 += objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString() + " ";
					trans_custom1 += objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString();
					trans_custom1 += " - ";
					if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"] != System.DBNull.Value)
						trans_custom1 += objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"].ToString() + " / ";
					trans_custom1 += driv_state + " / ";
					if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"] != System.DBNull.Value)
						trans_custom1 += Convert.ToDateTime(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"]).ToString("MM/dd/yyyy");
				
					objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc1, int.Parse(GetUserId()),trans_custom1, "Application");
					string strSSN="";
					if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_SSN"].ToString()!= "")
					{
						strSSN = objDSDriver.Tables[0].Rows[nCount]["DRIVER_SSN"].ToString();
						strSSN=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(strSSN);
						strSSN=strSSN.ToString().Replace("-","");
					}
					string strDateOfBirth1="";
					if (objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"]!= null)
					{
						strDateOfBirth1=Convert.ToDateTime(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");   
					}
					//For Additional Driver Block
					BlockId=BlockId+1;
					strAdditionalDrivers+="D"+ BlockId.ToString() ;
					if (objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString().Length>20)
						strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString().Substring(0,20);
					else
						strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString().PadRight(20,' ');
					if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString().Length>15)
						strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString().Substring(0,15);
					else
						strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString().PadRight(15,' ');
					if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString().Length>15)
						strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString().Substring(0,15);
					else
						strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString().PadRight(15,' ');
					if(objDSDriver.Tables[0].Rows[nCount]["DRIVER_SUFFIX"].ToString().Length>3)
						strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["DRIVER_SUFFIX"].ToString().Substring(0,3);
					else
						strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["DRIVER_SUFFIX"].ToString().PadRight(3,' ');
					if(strSSN.Length>9)
						strAdditionalDrivers+=strSSN.Substring(0,9);
					else
						strAdditionalDrivers+=strSSN.PadRight(9,' ');
					strAdditionalDrivers+=strDateOfBirth1.PadRight(8,' ');
					strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"].ToString().PadRight(20,' ');
					strAdditionalDrivers+=objDSDriver.Tables[0].Rows[nCount]["STATE_CODE"].ToString().PadRight(2,' ');
					strAdditionalDrivers+="".ToString().PadRight(20,' ');
					strAdditionalDrivers+="".ToString().PadRight(2,' ');

					//objDriver.getd
				}
				// xml retrived from web service
				objDriverResponse = new System.Xml.XmlDocument();
				//objDriverResponse
				string strWebResponse = "";
				retAutoLoss	= objMVRUtil.GetAutoLoss(objDSDriver.Tables[0].Rows[index]["STATE_CODE"]==null?"":objDSDriver.Tables[0].Rows[index]["STATE_CODE"].ToString(),
					objDSDriver.Tables[0].Rows[index]["DRIVER_DRIV_LIC"]==null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_DRIV_LIC"].ToString(),
					objDSDriver.Tables[0].Rows[index]["DRIVER_LNAME"]==null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_LNAME"].ToString(), 
					objDSDriver.Tables[0].Rows[index]["DRIVER_FNAME"]==null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_FNAME"].ToString() ,
					objDSDriver.Tables[0].Rows[index]["DRIVER_SUFFIX"]==null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_SUFFIX"].ToString() ,
					objDSDriver.Tables[0].Rows[index]["DRIVER_MNAME"]==null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_MNAME"].ToString() ,
					strDateOfBirth ,
					objDSDriver.Tables[0].Rows[index]["DRIVER_SEX"]== null?"":objDSDriver.Tables[0].Rows[index]["DRIVER_SEX"].ToString() ,"",objDriverInfo, ref strWebResponse,strAdditionalDrivers);

				if(strWebResponse.StartsWith("Accept"))
					trans_desc = "Loss Report inquiry accepted under request Id :" + strWebResponse.Substring(7);
				else if (strWebResponse.StartsWith("Error"))
					trans_desc = "Loss Report inquiry gave an error :" + strWebResponse.Substring(6);
				string strDriverName=objDSDriver.Tables[0].Rows[index]["DRIVER_FNAME"].ToString();
				strDriverName+=objDSDriver.Tables[0].Rows[index]["DRIVER_MNAME"].ToString().Trim()==""?"":" " + objDSDriver.Tables[0].Rows[index]["DRIVER_MNAME"].ToString().Trim();
				strDriverName+= " " + objDSDriver.Tables[0].Rows[index]["DRIVER_LNAME"].ToString();
				ClsPriorLoss objPriorLoss=new ClsPriorLoss();
				int intNameMatch = retAutoLoss.IndexOf("INSURED NAME DIFFERENT");//Done for Itrack Issue 6723 on 7 Dec 09
				if(retAutoLoss.Length > 935)
				{
					string strLblText=objPriorLoss.InsertToPriorLossTable(objDSDriver,retAutoLoss.Substring(944, retAutoLoss.Length-944), objDSDriver.Tables[0].Rows[index]["DRIVER_ID"].ToString(), strDriverName.TrimEnd() ,userID,"APP",intNameMatch);//Done for Itrack Issue 6723 on 7 Dec 09
					lblMessage.Text += strLblText;
					lblMessage.Text += "Prior Loss Report fetched for: " + strDriverName.TrimEnd() + "<br>";
				}
				else
				{
					if (!strWebResponse.StartsWith("Error"))
						trans_desc = "No Auto Loss Report found";
					lblMessage.Text += "Prior Loss Report not found for: " + strDriverName.Trim() + "<br>";
				}

				objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");
				//objPriorLoss.SetLossOrdered(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID,int.Parse(objDSDriver.Tables[0].Rows[index]["DRIVER_ID"].ToString()),"PPA",ReportStatus);
				string []Seleteddriv_ids = hidDRIVER_ID.Value.Split('~');
				for(int indx=0; indx < Seleteddriv_ids.Length; indx++)
				{
					if(Seleteddriv_ids[indx].Trim()!="")
						objPriorLoss.SetLossOrdered(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID,int.Parse(Seleteddriv_ids[indx].Trim()),"PPA",ReportStatus);
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text=ex.Message;
			}
		}
		private void SetDriverAutoLoss()
		{
			int nCount =0; 
			//DataSet objDSDriverViolDetail;
			DataSet objDSDriver;
			DataSet objDSPoilcy;
			XmlDocument objDriverResponse;
			//XmlNode objNode= null;
			string strPoilcyID="";
			string strPoilcyVerNo="";
			//string strXmlQuery="";
			string strStateID="";
			string retAutoLoss;
    
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
			gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
			gIntAPP_ID 			=	int.Parse(Request["APP_ID"] == null || Request["APP_ID"] =="" ? FetchValueFromXML("APP_ID","") : Request["APP_ID"].ToString());				
			gIntAPP_VERSION_ID 	=	int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] ==""  ?  FetchValueFromXML("APP_VERSION_ID","") : Request["APP_VERSION_ID"].ToString());
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

			
			
			// for each driver or operator getting a list of violation from iix web service
			for (nCount =0; nCount <= objDSDriver.Tables[0].Rows.Count -1 ; nCount++)
			{
				
				int driv_chosen = 0;
				string trans_desc = "Auto Loss Report was requested on {"+DateTime.Now.ToString("MM/dd/yyyy")+"} for";
				string trans_custom = "";
				string ReportStatus = "";

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
				
				objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");

				string strDriverXml = Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetAppDriverDetailsXML(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, Convert.ToInt32(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"]) );
				Cms.Model.Application.ClsDriverDetailsInfo objDriverInfo = new Cms.Model.Application.ClsDriverDetailsInfo();
				//base.PopulateModelObject(objDriverInfo, strDriverXml);
				objDriverInfo.CUSTOMER_ID = gIntCUSTOMER_ID;
				objDriverInfo.APP_ID = gIntAPP_ID;
				objDriverInfo.APP_VERSION_ID = gIntAPP_VERSION_ID;
				objDriverInfo.DRIVER_SSN = objDSDriver.Tables[0].Rows[nCount]["DRIVER_SSN"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_SSN"].ToString();
				objDriverInfo.DRIVER_CITY = objDSDriver.Tables[0].Rows[nCount]["DRIVER_CITY"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_CITY"].ToString();
				objDriverInfo.DRIVER_ZIP = objDSDriver.Tables[0].Rows[nCount]["DRIVER_ZIP"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_ZIP"].ToString();
				objDriverInfo.DRIVER_ADD1 = objDSDriver.Tables[0].Rows[nCount]["DRIVER_ADD1"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_ADD1"].ToString();
				//objDriver.getd

				string strDateOfBirth="";
				if (objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"]!= null)
				{
					strDateOfBirth=Convert.ToDateTime(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");   
				}
				strStateID=	Convert.ToString(objDSDriver.Tables[0].Rows[nCount]["STATE_ID"].ToString());
				// xml retrived from web service
				objDriverResponse = new System.Xml.XmlDocument();
				
				//objDriverResponse
					
				string strWebResponse = "";
				retAutoLoss	= objMVRUtil.GetAutoLoss(objDSDriver.Tables[0].Rows[nCount]["STATE_CODE"]==null?"":objDSDriver.Tables[0].Rows[nCount]["STATE_CODE"].ToString(),
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"].ToString(),
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString(), 
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() ,
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_SUFFIX"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_SUFFIX"].ToString() ,
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"]==null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString() ,
					strDateOfBirth ,
					objDSDriver.Tables[0].Rows[nCount]["DRIVER_SEX"]== null?"":objDSDriver.Tables[0].Rows[nCount]["DRIVER_SEX"].ToString() ,"",objDriverInfo, ref strWebResponse,"");

				if(strWebResponse.StartsWith("Accept"))
					trans_desc = "Loss Report inquiry accepted under request Id :" + strWebResponse.Substring(7);
				else if (strWebResponse.StartsWith("Error"))
					trans_desc = "Loss Report inquiry gave an error :" + strWebResponse.Substring(6);

				if(retAutoLoss.Length > 935)
				{
					InsertToPriorLossTable(retAutoLoss.Substring(944, retAutoLoss.Length-944), objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(), objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString());
					lblMessage.Text += "Prior Loss Report fetched for: " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString() + "<br>";
				}
				else
				{
					if (!strWebResponse.StartsWith("Error"))
						trans_desc = "No Auto Loss Report found";
					lblMessage.Text += "Prior Loss Report not found for: " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString() + "<br>";
				}

				objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");

				ClsPriorLoss objPriorLoss = new  ClsPriorLoss(); 
				objPriorLoss.SetLossOrdered(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID,int.Parse(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString()),"PPA",ReportStatus);

			//	lblMessage.Text = "The record has been added to Prior Loss Table. Please use Prior Information menu to see its updated rows.";
													
			}
			if(nCount == 0)
			{
			//	lblMessage.Text = "No record added. Please add a driver first to add the record.";
			}
		}
		private void InsertToPriorLossTable(string retAutoLoss, string driver_id, string driver_name)
		{
			ClsPriorLossInfo objPriorLossInfo;
			objPriorLossInfo                            = new ClsPriorLossInfo();
			string insurername="";
			string policynum="";
			string polTyp="";
			string oc_Date="";
			string clmTyp="";
			string lossTyp="";
			string clmStat="";
			string lossAmt="";
			string driv_rel="";
			//int lossadded = 0;

			try
			{
				retAutoLoss = retAutoLoss.Replace("&","and");
				retAutoLoss = retAutoLoss.Replace(" xmlns=\"Common\"","");

				XmlDocument lossxml = new XmlDocument();
				lossxml.LoadXml(retAutoLoss);

				XmlNodeList lossnodes = lossxml.SelectNodes("ISO/PassportSvcRs/PassportInqRs/Match/Claim");
				foreach(XmlNode lossnode in lossnodes)
				{

					try { insurername = lossnode.SelectSingleNode("Insurer/InsurerName").InnerText; }	
					catch(Exception){}
					objPriorLossInfo.LOSS_CARRIER = insurername;

					try { policynum = lossnode.SelectSingleNode("Policy/PolicyNumber").InnerText;}	
					catch(Exception){}
					objPriorLossInfo.POLICY_NUM = policynum;

					try { polTyp = lossnode.SelectSingleNode("Policy/PolicyTypeCd").InnerText;}	
					catch(Exception){}

					if(polTyp == "PAPP" || polTyp == "CAPP")
						objPriorLossInfo.LOB = "2";
					else if(polTyp == "CYPP")
						objPriorLossInfo.LOB = "3";
					else if(polTyp == "MPHH")
						objPriorLossInfo.LOB = "-1";
				
					//Code for retreiving the forms valus will go here		
					try { oc_Date    = lossnode.SelectSingleNode("Loss/LossDt").InnerText;}	
					catch(Exception){}
					//headerLen = Convert.ToInt32(retAutoLoss.Substring(3,5)) + 8;
					// string cl_Date                              = txtCLAIM_DATE.Text;


					/*			strhidRowId                                 = hidLOSS_ID.Value;            
								if(hidLOSS_ID.Value!="New")
									objPriorLossInfo.LOSS_ID                = int.Parse(hidLOSS_ID.Value);
					*/
					if(oc_Date!="")
						objPriorLossInfo.OCCURENCE_DATE             = Convert.ToDateTime(oc_Date);
					else
						objPriorLossInfo.OCCURENCE_DATE             = Convert.ToDateTime("1/1/1900");


					try { clmTyp    = lossnode.SelectSingleNode("Payment/CoverageCd").InnerText;}	
					catch(Exception){}

					objPriorLossInfo.CLAIMS_TYPE =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CMTYPS", clmTyp);

					try { lossTyp    = lossnode.SelectSingleNode("Payment/CoverageCd").InnerText;}	
					catch(Exception){}
					objPriorLossInfo.LOSS_TYPE =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CLOSS", lossTyp);

					try { clmStat    = lossnode.SelectSingleNode("Payment/ClaimStatusCd").InnerText;}	
					catch(Exception){}
					objPriorLossInfo.CLAIM_STATUS =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CLAIM", clmStat).ToString();

					try { lossAmt = lossnode.SelectSingleNode("Payment/LossPaymentAmt").InnerText;}	
					catch(Exception){}
					objPriorLossInfo.AMOUNT_PAID                = Double.Parse(lossAmt);//txtAMOUNT_PAID.Text==""?0:double.Parse(txtAMOUNT_PAID.Text);

					try { driv_rel = lossnode.SelectSingleNode("DriverRelationshipToOwnerCd").InnerText;}	
					catch(Exception){}
				
					if(driv_rel == "PH")
						objPriorLossInfo.RELATIONSHIP = 3468;
					else if(driv_rel == "SP" || driv_rel == "S")
						objPriorLossInfo.RELATIONSHIP = 3472;
					else if(driv_rel == "CH" || driv_rel == "C")
						objPriorLossInfo.RELATIONSHIP = 3467;
					else
						objPriorLossInfo.RELATIONSHIP = 3470;

					/* if(cl_Date!="")    
							objPriorLossInfo.CLAIM_DATE                 = Convert.ToDateTime(cl_Date);
						else
							objPriorLossInfo.CLAIM_DATE                 = Convert.ToDateTime("1/1/1900");	*/


					//objPriorLossInfo.LOB                        = GetLOBID().ToString();
					//objPriorLossInfo.LOSS_TYPE                  = 0;//cmbLOSS_TYPE.SelectedValue.ToString()=="" ? 0 : int.Parse (cmbLOSS_TYPE.SelectedValue.ToString()) ;
					//objPriorLossInfo.AMOUNT_RESERVED            = txtAMOUNT_RESERVED.Text==""?0:double.Parse(txtAMOUNT_RESERVED.Text);
					//objPriorLossInfo.CLAIM_STATUS               = "8704";//cmbCLAIM_STATUS.SelectedValue;
					//  objPriorLossInfo.LOSS_DESC                  = txtLOSS_DESC.Text;
					//objPriorLossInfo.REMARKS                    = lossnode.SelectSingleNode("Cause-Of-Loss-Text").InnerText;//txtDESC_OF_LOSS_AND_REMARKS.Text;
					// objPriorLossInfo.MOD                        = txtMOD.Text;
					// objPriorLossInfo.LOSS_RUN                   = chkLOSS_RUN.Checked == false? "N" : "Y" ;
					// objPriorLossInfo.CAT_NO                     = txtCAT_NO.Text;            
					objPriorLossInfo.CUSTOMER_ID                = int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());//hidCUSTOMER_ID.Value == "" ? 0 : int.Parse(hidCUSTOMER_ID.Value) ;

					objPriorLossInfo.APLUS_REPORT_ORDERED = 1;

					objPriorLossInfo.DRIVER_ID = int.Parse(driver_id);
					//objPriorLossInfo.DRIVER_NAME = driver_name;
					objPriorLossInfo.DRIVER_NAME = gIntCUSTOMER_ID.ToString() + "^" + gIntAPP_ID.ToString() + "^" + gIntAPP_VERSION_ID.ToString() + "^" + driver_id + "^" + "APP";

					/*if(cmbAPLUS_REPORT_ORDERED.SelectedItem!=null && cmbAPLUS_REPORT_ORDERED.SelectedItem.Value!="")
						objPriorLossInfo.APLUS_REPORT_ORDERED = int.Parse(cmbAPLUS_REPORT_ORDERED.SelectedItem.Value);

					if(objPriorLossInfo.LOB == ((int)enumLOB.AUTOP).ToString() || objPriorLossInfo.LOB == ((int)enumLOB.CYCL).ToString())
					{
						if(cmbCLAIMS_TYPE.SelectedItem!=null && cmbCLAIMS_TYPE.SelectedItem.Value!="")
							objPriorLossInfo.CLAIMS_TYPE = int.Parse(cmbCLAIMS_TYPE.SelectedItem.Value);

						if(cmbAT_FAULT.SelectedItem!=null && cmbAT_FAULT.SelectedItem.Value!="")
							objPriorLossInfo.AT_FAULT = int.Parse(cmbAT_FAULT.SelectedItem.Value);

						if(cmbCHARGEABLE.SelectedItem!=null && cmbCHARGEABLE.SelectedItem.Value!="")
							objPriorLossInfo.CHARGEABLE = int.Parse(cmbCHARGEABLE.SelectedItem.Value);
						else
							objPriorLossInfo.CHARGEABLE = -1;
					}
					*/
					objPriorLossInfo.CREATED_BY = int.Parse(GetUserId());
					objPriorLossInfo.CREATED_DATETIME = DateTime.Now;
					objPriorLossInfo.MODIFIED_BY  = int.Parse(GetUserId());
					objPriorLossInfo.LAST_UPDATED_DATETIME  = DateTime.Now;
					objPriorLossInfo.IS_ACTIVE ="Y";

					//Calling the add method of business layer class
					ClsPriorLoss objPriorLoss = new  ClsPriorLoss(); 
					#region Check Update
					//					string ConnStr = System.Configuration.ConfigurationSettings.AppSettings.Get("DB_CON_STRING");
					string sqlSelPriorLoss = "SELECT * FROM APP_PRIOR_LOSS_INFO WHERE OCCURENCE_DATE = '" + oc_Date + "' AND LOB = '" + objPriorLossInfo.LOB.ToString() + "' AND CUSTOMER_ID = " + (Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString()) + " AND DRIVER_ID = " + driver_id;
					//					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
					//					DataSet PriorLossds = objDataWrapper.ExecuteDataSet(sqlSelPriorLoss);
					DataSet PriorLossds = objPriorLoss.GetExistingPriorLoss(sqlSelPriorLoss);
					#endregion
					if(PriorLossds.Tables[0].Rows.Count == 0)
					{	
							int intRetVal = objPriorLoss.Add(objPriorLossInfo);
							//lossadded = 1;
							lblMessage.Text += "Prior Loss Record added for: " + driver_name + "<br>";
						
					}
				}

				//if(lossadded == 1)
				//	lblMessage.Text += "Prior Loss Record added for: " + driver_name + "<br>";
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				//int i =0;
			}
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

		//Done for Itrack Issue 6708 on 19 Nov 09
		private void FetchPriorPolicy()
		{
			string retPriorPolicy = "";
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
            System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
			string strUserName = dic["UserName"].ToString();
			string strPassword = dic["Password"].ToString().PadRight(10,' ');
			string strAccountNumber = dic["AccountNumber"].ToString();
			string strUrl = dic["URL"].ToString();
			int userID =int.Parse(GetUserId());
			int gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
			
			Cms.BusinessLayer.BlClient.ClsCustomer objCustomer = new ClsCustomer();
			//string strcustXML = objCustomer.GetCustomer_PriorPolicy_details(gIntCUSTOMER_ID);
			string strcustXML = objCustomer.FillCustomerDetails(gIntCUSTOMER_ID);
			//btnFetchUDVI.Visible = true;

			Cms.Model.Client.ClsCustomerInfo objCustomerInfo = new Cms.Model.Client.ClsCustomerInfo();
			if(strcustXML != "")
			{
				Cms.CmsWeb.cmsbase objBase=new Cms.CmsWeb.cmsbase();
				objBase.PopulateModelObject(objCustomerInfo, strcustXML);
				
				
				string strWebResponse = "";
				Cms.CmsWeb.Utils.Utility objUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);
				ClsPriorPolicy objPriorPolicy = new ClsPriorPolicy();
				string strcustomerName="";
				
				retPriorPolicy = objUtil.GetPriorPolicy(objCustomerInfo, ref strWebResponse);

				try
				{
					string trans_desc = "";
					string trans_custom = "";
					if(strWebResponse.StartsWith("Accept"))
						trans_desc = "Coverage Verifier accepted under request Id :" + strWebResponse.Substring(7);
					else if (strWebResponse.StartsWith("Error"))
						trans_desc = "Coverage Verifier gave an error :" + strWebResponse.Substring(6);

					if(objCustomerInfo.CustomerMiddleName.ToString() != "")
						strcustomerName = objCustomerInfo.CustomerFirstName + objCustomerInfo.CustomerMiddleName + objCustomerInfo.CustomerLastName;
					else
						strcustomerName = objCustomerInfo.CustomerFirstName + objCustomerInfo.CustomerLastName;
					if(retPriorPolicy.Length > 935)
					{
						string strLblText=objPriorPolicy.InsertToPriorPolicyTable(objCustomerInfo,retPriorPolicy.Substring(944, retPriorPolicy.Length-944),strcustomerName.Trim(),userID);
						//Done for Itrack Issue 6708 on 14 Dec 09
//						lblMessage.Text += strLblText;
//						lblMessage.Text += "Prior Policy Report fetched for: " + strcustomerName.Trim() + "<br>";
						lblMessage.Text += "Prior policy information fetched successfully<br>";
					}
					else
					{
						if (!strWebResponse.StartsWith("Error"))
							trans_desc = "No Prior Policy Report found";
						//Done for Itrack Issue 6708 on 14 Dec 09
//						lblMessage.Text += "Prior Policy Report not found for: " + strcustomerName.Trim() + "<br>";
                        lblMessage.Text += Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2074") + "<br>";//Prior policy information not found
					}

					objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "POLICY");
				}
				catch(Exception ex)
				{
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}

			}				
				

		}


	}
}
