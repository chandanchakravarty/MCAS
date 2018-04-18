/******************************************************************************************

Created By				: Swarup Kumar Pal
dated					: 15 Mar 2007
purpose					: for MVR and Undisclose Drivers    
<Modified Date			: - > 
<Modified By			: - > 
<Purpose				: -  > 
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
//using Cms.DataLayer;
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
using Cms.BusinessLayer.BlAccount;
using System.Globalization;
using Cms.CmsWeb.WebControls;
using Cms.Model.Policy ;

namespace Policies.Aspx
{
	/// <summary>
	/// Summary description for PolMvrForm.
	/// </summary>
	public class PolMvrForm : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidKeyValues;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckedRowIDs;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDelString;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRequestType;
		protected int NO_OF_UDI;
		protected int NO_OF_EXC_MVR;
		protected System.Web.UI.WebControls.PlaceHolder GridHolderOpe;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOperators;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnCloseDriv;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnFetchMVR;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnFetchUDVI;
		protected Hashtable   htNOMVR;

		private string NewUDVI = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			#region P A G E - L O A D
			btnFetchMVR.Attributes.Add("onclick","javascript:if (Confirm() == false) return false;");
			if(hidCheckedRowIDs.Value!="")
				DeleteUDI();
			lblMessage.Text="";
			hidRequestType.Value=Request["CalledFor"].ToString();
			if (!IsPostBack) 
			{
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
					/*				if (NO_OF_UDI !=0)
									{
										//lblMessage.Text = NO_OF_UDI + " Undisclosed Driver has been saved.";
										//if(hidCheckedRowIDs.Value!="")
										//	DeleteUDI();

									}
									else
									{
										//lblMessage.Text = "No Undisclosed Driver found.";
										lblErr.Text = "No Undisclosed Driver found.";
										lblErr.Visible = true;
									} */
				}
				lblErr.Visible = true;
				//Setting web grid control properties
				
				// Commented for ITRack 2135 27-Jul 2007
				/*			objWebGrid.WebServiceURL = "http://" + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = "CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,UNDISCLOSED_DRIVER_ID,IsNull(DRIVER_NAME,'') DRIVER";
				objWebGrid.FromClause = "POL_UNDISCLOSED_DRIVER ";
				objWebGrid.WhereClause = " CUSTOMER_ID = '" + int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString())  + "" 
					+ "' AND POLICY_ID = '" + int.Parse(Request["POLICY_ID"] == null || Request["POLICY_ID"] =="" ? FetchValueFromXML("POLICY_ID","") : Request["POLICY_ID"].ToString())  
					+ "' AND POLICY_VERSION_ID = '" + int.Parse(Request["POLICY_VERSION_ID"] == null || Request["POLICY_VERSION_ID"] ==""  ?  FetchValueFromXML("POLICY_VERSION_ID","") : Request["POLICY_VERSION_ID"].ToString()) + "'";

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
					if (htNOMVR.Keys.Count>0)  
					{
						IDictionaryEnumerator iEnum = htNOMVR.GetEnumerator(); 
						while (iEnum.MoveNext()) 
						{ 
							lblMessage.Text=lblMessage.Text + iEnum.Value + "<br>";
						} 
					}
					else
						lblMessage.Text = "No MVR Driver Found.";
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
//						//lblMessage.Text = "No Undisclosed Driver found.";
//						lblErr.Text = "No Undisclosed Driver found.";
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

		#region  F O R - D E L E T E - R E C O R D S
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

		#region F E T C H I N G - M V R - I N F O
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
			int gIntPOLICY_ID = 0;
			int gIntPOLICY_VERSION_ID = 0;

			Cms.BusinessLayer.BlApplication.ClsGeneralInformation  objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			
			System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
			string strUserName = dic["UserName"].ToString();
			string strPassword = dic["Password"].ToString();
			string strAccountNumber = dic["AccountNumber"].ToString();
			string strUrl = dic["URL"].ToString();
			Cms.CmsWeb.Utils.Utility objMVRUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);
			int userID =int.Parse(GetUserId()); 
			string gstrLobID= GetLOBID().ToString();
			int gIntCUSTOMER_ID		= int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
			int gIntAPP_ID 		=  gIntPOLICY_ID	=	int.Parse(Request["POL_ID"] == null || Request["POL_ID"] =="" ? FetchValueFromXML("POL_ID","") : Request["POL_ID"].ToString());				
			int gIntAPP_VERSION_ID 	= gIntPOLICY_VERSION_ID	=	int.Parse(Request["POLICY_VERSION_ID"] == null || Request["POLICY_VERSION_ID"] ==""  ?  FetchValueFromXML("POLICY_VERSION_ID","") : Request["POLICY_VERSION_ID"].ToString());
			int gIntLobID=int.Parse(GetLOBID());
			
			//getting info about polcy and poilcy ver
			objDSPoilcy = objGenInfo.GetPoilcyInfo(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID);
			
			//getting driver or operator list on the basis of lob and application 
			if (gIntPOLICY_ID.ToString()  == null && gIntPOLICY_VERSION_ID.ToString() == null)
			{
				strPoilcyID=objDSPoilcy.Tables[0].Rows[0]["POLICY_ID"].ToString();
				strPoilcyVerNo=objDSPoilcy.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
			}
			else
			{
				strPoilcyID= gIntPOLICY_ID.ToString();
				strPoilcyVerNo= gIntPOLICY_VERSION_ID.ToString();
			}
			objDSDriver = objGenInfo.GetPolicyDriverList(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,gIntLobID);

			string trans_desc = "MVR Ordered on {"+DateTime.Now.ToString("MM/dd/yyyy")+"} for";
			string trans_custom = "";
					
			// for each driver or operator getting a list of violation from iix web service
			for (nCount =0; nCount <= objDSDriver.Tables[0].Rows.Count -1 ; nCount++)
			{
				string mvr_status = "0";
				string mvr_remarks = "";
				string MVR_LICENCE_CLASS = "";
				string MVR_DRIVER_LIC_APP = "";
				int driv_chosen = 0;
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


			//checking if violation come from web service 
			// then enter these violation into database !!
				
				if(objDriverResponse.DocumentElement.SelectNodes("Error").Count>0)
				{
					//htNOMVR.Add(gIntCUSTOMER_ID.ToString() + "-" + gIntAPP_ID.ToString() + "-" + gIntAPP_VERSION_ID + "-" + objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString() ,(objDSDriver.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() + " " + objDSDriver.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString()) + " : " + objDriverResponse.DocumentElement.SelectNodes("Error").Item(0).Attributes["Msg"].InnerText); 
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
					if(mvr_status == "1" || mvr_status == "2" || mvr_status == "N")  //"1" or "2" indicates a NOT FOUND
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
						if(mvr_status == "1" || mvr_status == "2" || mvr_status == "N") //"1" or "2" indicates a NOT FOUND if(mvr_status != "C")
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
					ClsPolicyAutoMVR objMVRInfo = new ClsPolicyAutoMVR();

					objMVRInfo.POLICY_ID = gIntPOLICY_ID;
					objMVRInfo.CUSTOMER_ID=gIntCUSTOMER_ID;
					objMVRInfo.POLICY_VERSION_ID=gIntPOLICY_VERSION_ID;
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
									objMVRInfo.MVR_DATE  = Convert.ToDateTime(strMVRConvectionDate);
								}		
								objMVRInfo.DRIVER_ID =int.Parse(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString()) ;
								int resultID=objGenInfo.InsertUnmatchedMVRViolationDetail(objMVRInfo,strVcode,strDescription,strPoilcyID,strPoilcyVerNo,false);
								if (resultID==-1) continue;
								NO_OF_EXC_MVR=NO_OF_EXC_MVR+1;
//								objMVRInfo.MVR_DEATH ="N"; 
//								objMVRInfo.MVR_AMOUNT=0; 
								objMVRInfo.VIOLATION_ID =resultID;
								objMVRInfo.IS_ACTIVE ="Y";
								if (objDSDriverViolDetail.Tables[0].Rows[0]["VIOLATION_ID"]!=System.DBNull.Value)   
									objMVRInfo.VIOLATION_TYPE = int.Parse(objDSDriverViolDetail.Tables[0].Rows[0]["VIOLATION_ID"].ToString().Trim());

								Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
								
								//Added by Mohit Agarwal 3-Jul-07
								try
								{
									if(strVtype.StartsWith("ADMI") || strVtype.StartsWith("INFO") ||  strVtype.StartsWith("ACCI") || strVtype.StartsWith("REIN") || strVtype.StartsWith("REVO") || strVtype.StartsWith("CANC") || strVtype.StartsWith("DISQ") || strVtype.StartsWith("DENI") || strVtype.StartsWith("UNKN") || strVtype.StartsWith("GISM")) // ||strVtype.StartsWith("SUSP") || (objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null))
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
									if (strVpoints=="") strVpoints="0";
									objMVRInfo.POINTS_ASSIGNED = int.Parse(strVpoints);
								}
								catch//(Exception ex)
								{
								}
								// if watercraft 
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
							//for (int nRecord =0; nRecord <=objDSDriverViolDetail.Tables[0].Rows.Count -1 ;nRecord ++)
							for (int nRecord =0; nRecord <= ViolCount -1 ;nRecord ++)
							{
								//strXmlQuery="/ResultData/Violation[@AViolation_code='"+objDSDriverViolDetail.Tables[0].Rows[nRecord]["SSV_CODE"].ToString().Trim()  +"']";
								XmlNode ViolNode=objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nRecord);
								if (ViolNode==null) continue;
								objNode=ViolNode;
								//objNode=objDriverResponse.DocumentElement.SelectSingleNode(strXmlQuery); 
								strDescription=objNode.Attributes["viol_description"].InnerText.Trim()   ; 
								strVcode =objNode.Attributes["AViolation_code"].InnerText.Trim();
								strVtype =objNode.Attributes["viol_type"].InnerText.Trim();  
								strVpoints =objNode.Attributes["APoints"].InnerText.Trim();
								if (strVcode=="") continue;
								objMVRInfo.DRIVER_ID=  int.Parse (objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"]==null?"": objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString() ); 
								if (objNode.Attributes["viol_date"].InnerText !="")
								{
									string date = objNode.Attributes["viol_date"].InnerText ; 
									objMVRInfo.MVR_DATE			= new DateTime(int.Parse( date.Substring(4,4)),int.Parse(date.Substring(0,2)),int.Parse(date.Substring(2,2))) ; //defaulting conviction date to occurance date
									objMVRInfo.OCCURENCE_DATE	= new DateTime(int.Parse( date.Substring(4,4)),int.Parse(date.Substring(0,2)),int.Parse(date.Substring(2,2))) ;
								}
								if (objNode.Attributes["conviction_date"].InnerText !="")
								{
									string Convictiondate = objNode.Attributes["conviction_date"].InnerText ; 
									objMVRInfo.MVR_DATE= new DateTime(int.Parse( Convictiondate.Substring(4,4)),int.Parse(Convictiondate.Substring(0,2)),int.Parse(Convictiondate.Substring(2,2))) ;
								}
//								objMVRInfo.MVR_DEATH ="N"; 
//								objMVRInfo.MVR_AMOUNT=0; 
								//objMVRInfo.VIOLATION_ID =int.Parse ( objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null?"":objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"].ToString()) ;
								//objMVRInfo.IS_ACTIVE =objDSDriverViolDetail.Tables[0].Rows[nRecord]["IS_ACTIVE"]== null?"":objDSDriverViolDetail.Tables[0].Rows[nRecord]["IS_ACTIVE"].ToString() ;
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
								Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
								//Added by Mohit Agarwal 3-Jul-07
								try
								{
									if(strVtype.StartsWith("ADMI") || strVtype.StartsWith("INFO") || strVtype.StartsWith("ACCI") || strVtype.StartsWith("REIN") || strVtype.StartsWith("REVO") || strVtype.StartsWith("CANC") || strVtype.StartsWith("DISQ") || strVtype.StartsWith("DENI") || strVtype.StartsWith("UNKN") || strVtype.StartsWith("GISM")) // || strVtype.StartsWith("SUSP") ||(objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null))
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
									if (strVpoints=="") strVpoints="0";
									objMVRInfo.POINTS_ASSIGNED = int.Parse(strVpoints);
								}
								catch(Exception ex)
								{throw(ex);
								}

								// if watercraft 
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
					objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, fetch_desc, int.Parse(GetUserId()),fetch_custom, "Policy");
					objGenInfo.SetMvrOrdered(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,Convert.ToInt32(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"]),gstrLobID, mvr_remarks, mvr_status, "POLICY", MVR_LICENCE_CLASS ,MVR_DRIVER_LIC_APP);
				}
				catch(Exception ex)
				{ throw(ex);}

										
			}
			if(trans_custom != "")
				objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "Policy");
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
			int gIntPOLICY_ID = 0;
			int gIntPOLICY_VERSION_ID = 0;

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
			int gIntAPP_ID 		=  gIntPOLICY_ID	=	int.Parse(Request["POLICY_ID"] == null || Request["POLICY_ID"] =="" ? FetchValueFromXML("POLICY_ID","") : Request["POLICY_ID"].ToString());				
			int gIntAPP_VERSION_ID 	= gIntPOLICY_VERSION_ID	=	int.Parse(Request["POLICY_VERSION_ID"] == null || Request["POLICY_VERSION_ID"] ==""  ?  FetchValueFromXML("POLICY_VERSION_ID","") : Request["POLICY_VERSION_ID"].ToString());
			int gIntLobID=int.Parse(GetLOBID());
			//getting info about polcy and poilcy ver

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
				strcustXML = objCustomer.FillCustomer_Vehicle_Details(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,"POL");
			
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
					DataSet dsAppl = objGenInfo.GetNamedInsuredList(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, "POLICY");
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
					objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "POLICY");
				}
				catch//(Exception ex)
				{
				}

				string strWebResponse = "";
				Cms.CmsWeb.Utils.Utility objUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);
				string errMsg = "";
				if(NewUDVI == "")
				{
					DataSet dsUDVI = objGenInfo.GetUDVIReport(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, "Policy");
					if(dsUDVI != null && dsUDVI.Tables.Count > 0 && dsUDVI.Tables[0].Rows.Count > 0)
						errMsg = dsUDVI.Tables[0].Rows[0]["REPORT_HTML"].ToString();
				}
				if(errMsg == "")
				{
					errMsg = objUtil.GetUndisclosedDriverVehicle("000", "UDV", objCustomerInfo, ref strWebResponse);
					if(errMsg!=null && errMsg!="" && errMsg.IndexOf("Error Msg",0)<0)
						objGenInfo.AddUDVIReport(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, int.Parse(GetUserId()), errMsg, "Policy");

					try
					{
						string trans_desc = "";
						string trans_custom = "";
						if(strWebResponse.StartsWith("Accept"))
							trans_desc = "Report inquiry accepted under request Id :" + strWebResponse.Substring(7);
						else if (strWebResponse.StartsWith("Error"))
							trans_desc = "Report inquiry gave an error :" + strWebResponse.Substring(6);

						trans_custom = ";Primary Applicant: " + objCustomerInfo.CustomerFirstName + " ";
						if(objCustomerInfo.CustomerMiddleName.Trim() != "")
							trans_custom += objCustomerInfo.CustomerMiddleName + " ";
						trans_custom += objCustomerInfo.CustomerLastName;
						objGenInfo.WriteTransactionLog(gIntCUSTOMER_ID, gIntAPP_ID, gIntAPP_VERSION_ID, trans_desc, int.Parse(GetUserId()),trans_custom, "POLICY");
					}
					catch//(Exception ex)
					{
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
			/*
			objDSPoilcy = objGenInfo.GetPoilcyInfo(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID);
			if(objDSPoilcy.Tables[0].Rows.Count > 0)
			{
				strPoilcyID=objDSPoilcy.Tables[0].Rows[0]["POLICY_ID"].ToString();
				strPoilcyVerNo=objDSPoilcy.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
			}
			//getting driver or operator list on the basis of lob and policy 
			//objDSDriver = objGenInfo.GetDriverList(gIntCUSTOMER_ID,gIntAPP_ID,gIntAPP_VERSION_ID,gstrLobID);
			objDSDriver = objGenInfo.GetPolDriverList(gIntCUSTOMER_ID,gIntPOLICY_ID,gIntPOLICY_VERSION_ID,gstrLobID);
			
			Cms.Model.Policy.ClsPolicyDriverInfo[] objDrivers = new Cms.Model.Policy.ClsPolicyDriverInfo[objDSDriver.Tables[0].Rows.Count];

			// for each driver or operator getting a list of violation from iix web service
			for (nCount =0; nCount <= objDSDriver.Tables[0].Rows.Count -1 ; nCount++)
			{
				string strDriverXml = ClsDriverDetail.GetPolDriverDetailsXML(gIntCUSTOMER_ID, gIntPOLICY_ID, gIntPOLICY_VERSION_ID, Convert.ToInt32(objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"]) );
				objDrivers[nCount] = new Cms.Model.Policy.ClsPolicyDriverInfo();				
				Cms.CmsWeb.cmsbase objBase=new Cms.CmsWeb.cmsbase();
				objBase.PopulateModelObject(objDrivers[nCount], strDriverXml);

				string strDateOfBirth="";
				if (objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"]!= null)
				{
					strDateOfBirth=Convert.ToDateTime(objDSDriver.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");   
				}
				Cms.CmsWeb.Utils.Utility objUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);
				string resultData=objUtil.GetUndisclosedPolDriver(objDrivers[nCount].DRIVER_STATE.ToString(),objDrivers[nCount].DRIVER_DRIV_LIC.ToString(),objDrivers[nCount].DRIVER_LNAME.ToString(),objDrivers[nCount].DRIVER_FNAME," "," ",strDateOfBirth,objDrivers[nCount].DRIVER_SEX," ",objDrivers[nCount]);
				XmlDocument xmlDoc=new XmlDocument();
				xmlDoc.LoadXml(resultData);
				XmlNode driverNode=xmlDoc.SelectSingleNode("ResultData"); 
				if (xmlDoc.DocumentElement.SelectNodes("Error").Count>0) 
					throw new Exception(driverNode.ChildNodes[0].Attributes["Msg"].Value); 
				for (int indexDriver=0;indexDriver<driverNode.ChildNodes.Count;indexDriver++)
				{
					string strDriverName=driverNode.ChildNodes[indexDriver].Attributes["name"].Value;  
					string driverID=objDSDriver.Tables[0].Rows[nCount]["DRIVER_ID"].ToString(); 
					objGenInfo.InsertUndisclosedPolDriverDetail( gIntCUSTOMER_ID.ToString(),gIntPOLICY_ID.ToString(),gIntPOLICY_VERSION_ID.ToString(),driverID,strDriverName.Trim());
					NO_OF_UDI=NO_OF_UDI+1;
				}
 

			}*/
		
		}
		#endregion

		//Added by Mohit Agarwal 4-Jul-2007 ITRack 2030
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
				//Setting web grid control properties
				
				string gstrLobID= GetLOBID().ToString();
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = "CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID, ISNULL(DRIVER_FNAME,'')+ ' ' + ISNULL(DRIVER_MNAME,'')+ ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,DRIVER_DRIV_LIC ";
				if(gstrLobID == "2" || gstrLobID == "3")//ppa or mot lob
					objWebGrid.FromClause = "POL_DRIVER_DETAILS ";
				else
					objWebGrid.FromClause = "POL_WATERCRAFT_DRIVER_DETAILS ";
				objWebGrid.WhereClause = " CUSTOMER_ID = '" + int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString())  + "" 
					+ "' AND POLICY_ID = '" + int.Parse(Request["POL_ID"] == null || Request["POL_ID"] =="" ? FetchValueFromXML("POLICY_ID","") : Request["POL_ID"].ToString())  
					+ "' AND POLICY_VERSION_ID = '" + int.Parse(Request["POLICY_VERSION_ID"] == null || Request["POLICY_VERSION_ID"] ==""  ?  FetchValueFromXML("POLICY_VERSION_ID","") : Request["POLICY_VERSION_ID"].ToString()) + "'";

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
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
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
		private void btnFetchMVR_ServerClick(object sender, System.EventArgs e)
		{
			//			lblMessage.Text="Fetching MVRs, please wait...";
			if (Request["CalledFor"].ToString()=="MVR")
			{
				FetchOperatorsMVR();

				//Added by Manoj Rathore on 13th May 2009 Itrack # 5829
				base.OpenEndorsementDetails();
			}
		}

		private void btnFetchUDVI_ServerClick(object sender, System.EventArgs e)
		{
			NewUDVI = "y";
			FetchUndisclosedDrivers();			
			lblErr.Visible = true;

			//Added by Manoj Rathore on 13th May 2009 Itrack # 5829
			base.OpenEndorsementDetails();
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
			this.btnFetchMVR.ServerClick += new System.EventHandler(this.btnFetchMVR_ServerClick);
			this.btnFetchUDVI.ServerClick += new System.EventHandler(this.btnFetchUDVI_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
