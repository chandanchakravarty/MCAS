/******************************************************************************************

Created By				: Pravesh K. Chandel
dated					: 08 Jan 2007
purpose					: for Home/Rental Loss Report   
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
	/// Summary description for HomeLossReport.
	/// </summary>
	public class HomeLossReport : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblMessage;
		private Hashtable htMessageTable;
		protected string strCalledFrom="";
	
		private void Page_Load(object sender, System.EventArgs e)

		{
			if ( !this.IsPostBack)
			{
				#region Setting ScreenId
				// Check from where the screen is called.
				if (Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] !="")
				{
					strCalledFrom=Request.QueryString["CalledFrom"].ToString();
				}			
				//Setting screen Id.	
				switch (strCalledFrom.ToUpper()) 
				{
					case "HOME":
						base.ScreenId="52_0_0";
						break;
					case "RENTAL":
						base.ScreenId="153_0_0";
						break;			
				}
				#endregion
				lblMessage.Text ="";
				try
				{
					GetHomeLoss();
					if (htMessageTable.Keys.Count>0)  
					{
						IDictionaryEnumerator iEnum = htMessageTable.GetEnumerator(); 

						while (iEnum.MoveNext()) 
						{ 
							//lblMessage.Text=lblMessage.Text + iEnum.Key.ToString().PadRight(20,' ')  + " : " +  iEnum.Value + "<br>";
							lblMessage.Text=lblMessage.Text + iEnum.Value + "<br>";
						} 
					}
				}
				catch(Exception ex)
				{
					lblMessage.Text =ex.Message.ToString(); 
					return;
				}
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
    
			#region loading web grid control
			Control c1= LoadControl("../../cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
                
				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//specifying columns for select query
				((BaseDataGrid)c1).SelectClause ="PL.CUSTOMER_ID,PL.LOSS_ID,CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101) OCCURENCE_DATE,PL.LOB,PL.LOSS_TYPE,CONVERT(VARCHAR(15),PL.AMOUNT_PAID,1) AMOUNT_PAID,CONVERT(VARCHAR(15),PL.AMOUNT_RESERVED,1) AMOUNT_RESERVED,PL.CLAIM_STATUS,PL.LOSS_DESC,PL.REMARKS,PL.MOD,PL.LOSS_RUN,PL.CAT_NO,PL.CLAIMID,PL.IS_ACTIVE,PL.CREATED_BY,PL.CREATED_DATETIME,PL.MODIFIED_BY,PL.LAST_UPDATED_DATETIME,isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101),'') + ' ' +  (CASE PL.LOB WHEN '-1' THEN 'Other' WHEN '' THEN '' ELSE LV.LOB_DESC END) LOSS,LV.LOB_DESC LOB1,LV.LOB_ID , MLV.LOOKUP_VALUE_DESC   ";
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause=" APP_PRIOR_LOSS_INFO PL LEFT JOIN MNT_LOB_MASTER LV ON PL.LOB=CONVERT(VARCHAR,LV.LOB_ID)    LEFT JOIN MNT_LOOKUP_VALUES MLV ON PL.LOSS_TYPE=MLV.LOOKUP_UNIQUE_ID    ";
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause=" LOB=" + GetLOBID().ToString()  + " AND PL.CUSTOMER_ID=" + GetCustomerID().ToString()  + " AND PL.DRIVER_NAME LIKE '" + GetCustomerID().ToString() + "^" + GetAppID().ToString() + "^" + GetAppVersionID().ToString() + "%'";
				//specifying Text to be shown in combo box
				((BaseDataGrid)c1).SearchColumnHeadings="Losses^Date of Occurence^Line of Business";
				((BaseDataGrid)c1).DropDownColumns="^^LOB";	
				//specifying column to be used for combo box
				((BaseDataGrid)c1).SearchColumnNames="(isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101),'') ! ' ' !  isnull(LV.LOB_DESC,''))^PL.OCCURENCE_DATE^LV.LOB_ID";
				//search column data type specifying data type of the column to be used for combo box
				((BaseDataGrid)c1).SearchColumnType="T^D^T";
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause="LOSS asc";
				//specifying column numbers of the query to be displyed in grid
				((BaseDataGrid)c1).DisplayColumnNumbers="21^3^22^23^6";
				//specifying column names from the query
				((BaseDataGrid)c1).DisplayColumnNames="LOSS_ID^LOSS^OCCURENCE_DATE^LOB1^LOOKUP_VALUE_DESC^AMOUNT_PAID";
				//specifying text to be shown as column headings
				((BaseDataGrid)c1).DisplayColumnHeadings="Id^Losses^Date of Occurrence^Line of Business^Loss Type^Amount Paid";
				//specifying column heading display text length
				((BaseDataGrid)c1).DisplayTextLength="5^20^20^15^20^20";
				//specifying width percentage for columns
				((BaseDataGrid)c1).DisplayColumnPercent="5^20^20^15^20^20";
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="2";
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="PL.LOSS_ID^PL.CUSTOMER_ID";
				//specifying column type of the data grid
				((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B";
				//specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23";
				//specifying message to be shown
				((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//specifying buttons to be displayed on grid
				((BaseDataGrid)c1).ExtraButtons="1^Close^0^CloseClicked";
				//specifying number of the rows to be shown
                ((BaseDataGrid)c1).PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE"));
				//specifying cache size (use for top clause)
                ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				//specifying heading
				((BaseDataGrid)c1).HeaderString = "Prior Loss";
				((BaseDataGrid)c1).SelectClass = colors;
				//specifying text to be shown for filter checkbox
				//((BaseDataGrid)c1).FilterLabel="Show Complete";
				//specifying column to be used for filtering
				//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
				//value of filtering record
				//((BaseDataGrid)c1).FilterValue="Y";
				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="LOSS_ID^CUSTOMER_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);
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

		}
		#endregion
		#region Call Home Loss Report using IIX Web service 

		private string NamedInsured(DataSet dsIns)
		{
			string names = "";
			if(dsIns.Tables.Count > 0)
			{
				foreach(DataRow drIns in dsIns.Tables[0].Rows)
				{
					if(names != "")
						names += " & " + drIns["APPNAME"].ToString();
					else
						names += drIns["APPNAME"].ToString();
				}
			}
			return names;

		}
		
		public  string GetHomeLoss()
		{
			//int nCount =0; 
			//DataSet objDSDriver;
			//DataSet objDSPoilcy;
			//XmlDocument objDriverResponse;
			//XmlNode objNode= null;
			//string strPoilcyID="";
			//string strPoilcyVerNo="";
			string strXmlQuery="";
			string strStateID="";
			string strLOB_ID= GetLOBID().ToString(); 
			string strAPP_ID= GetAppID().ToString();
			string strAPP_VERSION_ID= GetAppVersionID().ToString();
			string strCUSTOMER_ID=GetCustomerID().ToString();
			try
			{
				//ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
				Cms.CmsWeb.Utils.Utility objMVRUtil = new Cms.CmsWeb.Utils.Utility();
				int userID =int.Parse(GetUserId());
				strStateID= "22";
				System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
				string strUserName = dic["UserName"].ToString();
				string strPassword = dic["Password"].ToString();
				string strAccountNumber = dic["AccountNumber"].ToString();
				string strUrl = dic["URL"].ToString();
				///////
				ClsLocation objLocations = new ClsLocation(); 
			
				int intCustomerID = Convert.ToInt32(GetCustomerID());
				int intAppID = Convert.ToInt32(GetAppID());
				int intAppVersionID = Convert.ToInt32(GetAppVersionID());
	
				DataTable dtLocations = null;
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation  objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				if(strCalledFrom.ToUpper() == "WAT")
					dtLocations = ClsLocation.GetLocationsWithCustomer(intCustomerID,intAppID,intAppVersionID, "WAT");
				else
					dtLocations = ClsLocation.GetLocationsWithCustomer(intCustomerID,intAppID,intAppVersionID);
		
				htMessageTable=new Hashtable(); 
				Cms.CmsWeb.Utils.Utility objUtil =new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);
				Cms.Model.Application.HomeOwners.clsGeneralInfo  objHomeInfo =new Cms.Model.Application.HomeOwners.clsGeneralInfo(); 		
				for(int i=0;i< dtLocations.Rows.Count;i++)
				{
					string strWebResponse = "";
					bool LossFound=true;
					string ReportStatus ="",Msg="";
					//strXmlQuery = objUtil.GetHomeRentLoss(dtLocations.Rows[i]["STATE_CODE"].ToString()   ,dtLocations.Rows[i]["CITY_NAME"].ToString(),dtLocations.Rows[i]["LAST_NAME"].ToString(), dtLocations.Rows[i]["FIRST_NAME"].ToString(),dtLocations.Rows[i]["MIDDLE_NAME"].ToString(),dtLocations.Rows[i]["DATE_OF_BIRTH"].ToString().Replace("/","") ,	objHomeInfo);
					strXmlQuery = objUtil.GetHomeRentLoss(dtLocations.Rows[i]["STATE_CODE"].ToString()   ,dtLocations.Rows[i]["CITY_NAME"].ToString(),dtLocations.Rows[i]["LAST_NAME"].ToString(), dtLocations.Rows[i]["FIRST_NAME"].ToString(),dtLocations.Rows[i]["MIDDLE_NAME"].ToString(),dtLocations.Rows[i]["DATE_OF_BIRTH"].ToString().Replace("/",""),dtLocations.Rows[i]["SSN_NO"].ToString(),dtLocations.Rows[i]["LOC_ZIP"].ToString(),dtLocations.Rows[i]["LOC_ADD1"].ToString(),dtLocations.Rows[i]["LOC_ADD2"].ToString(),strCUSTOMER_ID, ref strWebResponse);
					try
					{
						string trans_desc = "";
						string trans_custom = "";
	
						string strXMLHomeLoss = strXmlQuery.Replace("&","and");
						XmlDocument lossxml = new XmlDocument();
						lossxml.LoadXml(strXMLHomeLoss);
				
						XmlNodeList lossnodes = lossxml.SelectNodes("ResultData/Error");

						if(lossnodes != null && lossnodes.Count != 0)
						{
							if (strWebResponse.StartsWith("Error"))
								trans_desc = "Loss Report inquiry gave an error :" + strWebResponse.Substring(6);
							else
								trans_desc = "No Property Loss Report found";
							ReportStatus=lossnodes.Item(0).Attributes["Status"].Value;
							Msg=lossnodes.Item(0).Attributes["Msg"].Value;
						}
						else if(strWebResponse.StartsWith("Accept"))
							trans_desc = "Loss Report inquiry accepted under request Id :" + strWebResponse.Substring(7);
						else if (strWebResponse.StartsWith("Error"))
						{
							trans_desc = "Loss Report inquiry gave an error :" + strWebResponse.Substring(6);
							Msg=lossnodes.Item(0).Attributes["Msg"].Value;
						}
						trans_custom = ";Primary Applicant: " + dtLocations.Rows[i]["FIRST_NAME"].ToString() + " ";
						//Added by Asfa (05-June-2008) - iTrack #4294
						if(dtLocations.Rows[i]["MIDDLE_NAME"].ToString().Trim() != "")
							trans_custom += dtLocations.Rows[i]["MIDDLE_NAME"].ToString() + " ";
						trans_custom += dtLocations.Rows[i]["LAST_NAME"].ToString();
						
						//Done for Itrack Issue 6487 on 30 Sept 09
						if(dtLocations.Rows[i]["LOC_ADD2"].ToString() !="")
							trans_custom += ";Address: " + dtLocations.Rows[i]["LOC_ADD1"].ToString() + ", " + dtLocations.Rows[i]["LOC_ADD2"].ToString() + ", " + dtLocations.Rows[i]["CITY_NAME"].ToString() + ", " + dtLocations.Rows[i]["STATE_NAME"].ToString() + " " + dtLocations.Rows[i]["LOC_ZIP"].ToString();
						else
							trans_custom += ";Address: " + dtLocations.Rows[i]["LOC_ADD1"].ToString() + ", " + dtLocations.Rows[i]["CITY_NAME"].ToString() + ", " + dtLocations.Rows[i]["STATE_NAME"].ToString() + " " + dtLocations.Rows[i]["LOC_ZIP"].ToString();
						/* Commented by Asfa (05-June-2008) - iTrack #4294
						if(dtLocations.Rows[i]["MIDDLE_NAME"].ToString().Trim() != "")
							trans_custom += dtLocations.Rows[i]["MIDDLE_NAME"].ToString() + " ";
						trans_custom += dtLocations.Rows[i]["LAST_NAME"].ToString();
						*/
						objGenInfo.WriteTransactionLog(intCustomerID, intAppID, intAppVersionID, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");
					}
					catch(Exception ex)
					{
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					}

					int indexError=strXmlQuery.IndexOf("Error");
					if(indexError !=-1)
					{
						htMessageTable.Add(dtLocations.Rows[i]["LOCATION_ID"].ToString(),Msg); //strXmlQuery.Substring(indexError+10,strXmlQuery.Substring(indexError+10).IndexOf("/>") ));  		//, strXmlQuery.IndexOf("</ResultSet>")  ,strXmlQuery.IndexOf("/>")-indexError+9
						//LossFound=false;
					}
					else if(strXmlQuery.IndexOf("<APLUS-PROPERTY>") < 0)
						InsertToPriorLossTable(strXmlQuery,dtLocations.Rows[i]["LOCATION_ID"].ToString());
					else
						InsertToPriorLossTableAPlus(strXmlQuery,dtLocations.Rows[i]["LOCATION_ID"].ToString());

					string fetch_custom = "";

					#region Get All Named Insured
					string ConnStr = System.Configuration.ConfigurationManager.AppSettings.Get("DB_CON_STRING");
					string sqlSelAppl = "Proc_GetPDFApplicantDetails " + intCustomerID.ToString() + "," + intAppID.ToString() + "," + intAppVersionID.ToString() + ",'application'"; // AND DRIVER_ID = " + driver_id;
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
					DataSet Applds = objDataWrapper.ExecuteDataSet(sqlSelAppl);
					#endregion

					fetch_custom += ";Primary Applicant: " + NamedInsured(Applds);
					//Done for Itrack Issue 6816 on 14 Dec 09
					if(dtLocations.Rows[i]["LOC_ADD2"].ToString() !="")
						fetch_custom += ";Address: " + dtLocations.Rows[i]["LOC_ADD1"].ToString() + ", " + dtLocations.Rows[i]["LOC_ADD2"].ToString() + ", " + dtLocations.Rows[i]["CITY_NAME"].ToString() + ", " + dtLocations.Rows[i]["STATE_NAME"].ToString() + " " + dtLocations.Rows[i]["LOC_ZIP"].ToString();
					else
						fetch_custom += ";Address: " + dtLocations.Rows[i]["LOC_ADD1"].ToString() + ", " + dtLocations.Rows[i]["CITY_NAME"].ToString() + ", " + dtLocations.Rows[i]["STATE_NAME"].ToString() + " " + dtLocations.Rows[i]["LOC_ZIP"].ToString();

					objGenInfo.WriteTransactionLog(intCustomerID, intAppID, intAppVersionID, "Property Loss Ordered on " + DateTime.Now.ToString("MM/dd/yyyy") + " for", int.Parse(GetUserId()),fetch_custom, "Application");

					ClsPriorLoss objPriorLoss = new  ClsPriorLoss(); 

//					try
//					{
						if(strCalledFrom.ToUpper() == "WAT" && LossFound==true)
							objPriorLoss.SetLossOrdered(intCustomerID,intAppID,intAppVersionID,int.Parse(dtLocations.Rows[i]["LOCATION_ID"].ToString()),"WAT",ReportStatus);
						else if (LossFound)
							objPriorLoss.SetLossOrdered(intCustomerID,intAppID,intAppVersionID,int.Parse(dtLocations.Rows[i]["LOCATION_ID"].ToString()),"HOME",ReportStatus);
//					}
//					catch(Exception)
//					{}
				}
		
													
				return strXmlQuery;	
			}
			catch( Exception ex)
			{
				throw(ex);
			}
		}

		private void InsertToPriorLossTable(string strXMLHomeLoss,string LOC_ID)
		{
			ClsPriorLossInfo objPriorLossInfo;
			ClsPriorLossInfo_Home  objPriorLossInfoHome;
			objPriorLossInfo                            = new ClsPriorLossInfo();
			objPriorLossInfoHome						=new ClsPriorLossInfo_Home(); 
			int InserCount=0,UpdateCount=0;
			try
			{
				strXMLHomeLoss = strXMLHomeLoss.Replace("&","and");
				XmlDocument lossxml = new XmlDocument();
				lossxml.LoadXml(strXMLHomeLoss);
				
				XmlNodeList lossnodes = lossxml.SelectNodes("ResultData/LOSS_REPORT");
				foreach(XmlNode lossnode in lossnodes)
				{
					//Code for retreiving the forms valus will go here		
					string oc_Date    = lossnode.Attributes["LOSS-DATE"].InnerText.Trim();
					oc_Date=oc_Date.Substring(0,2) + "/" + oc_Date.Substring(2,2) + "/" + oc_Date.Substring(4,4);
					if(oc_Date!="")
						objPriorLossInfo.OCCURENCE_DATE             = Convert.ToDateTime(oc_Date);
					else
						objPriorLossInfo.OCCURENCE_DATE             = Convert.ToDateTime("1/1/1900");
  

					/* if(cl_Date!="")    
						 objPriorLossInfo.CLAIM_DATE                 = Convert.ToDateTime(cl_Date);
					 else
						 objPriorLossInfo.CLAIM_DATE                 = Convert.ToDateTime("1/1/1900");	*/
						
					string lossAmt=lossnode.Attributes["LOSS-AMOUNT"].InnerText.Trim();
					//objPriorLossInfo.LOB                        = GetLOBID().ToString();
					string polType = lossnode.Attributes["POLICY-TYPE"].InnerText.Trim();;
					if(polType == "HO" || polType == "H" || polType == "T" || polType == "C" || polType == "I" || polType == "IM") 
						objPriorLossInfo.LOB = "1";
					else if(polType == "BOAT" || polType == "B")
						objPriorLossInfo.LOB = "4";
					else if(polType == "FIRE" || polType == "F" || polType == "EC")
						objPriorLossInfo.LOB = "6";

					else if(polType == "J")
						objPriorLossInfo.LOB = "5";
					else
						objPriorLossInfo.LOB  = GetLOBID().ToString();//"-1";
					string lossTyp = lossnode.Attributes["CLAIM-TYPE"].InnerText.Trim();
					objPriorLossInfo.LOSS_TYPE =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CLOSS", lossTyp);
					//objPriorLossInfo.LOSS_TYPE                  = 0;//cmbLOSS_TYPE.SelectedValue.ToString()=="" ? 0 : int.Parse (cmbLOSS_TYPE.SelectedValue.ToString()) ;
					objPriorLossInfo.AMOUNT_PAID                = Double.Parse(lossAmt);//Double.Parse(lossnode.Attributes["LOSS-AMOUNT"].InnerText.Trim());
					//objPriorLossInfo.AMOUNT_RESERVED            = txtAMOUNT_RESERVED.Text==""?0:double.Parse(txtAMOUNT_RESERVED.Text);
					objPriorLossInfo.CLAIM_STATUS               = "8704";//lossnode.Attributes["CLAIM-STATUS"].InnerText 
					string strCouseOFLoss =lossnode.Attributes["CAUSE-OF-LOSS"].InnerText.Trim();
					//objPriorLossInfo.LOSS_DESC                  = strCouseOFLoss;
					objPriorLossInfo.REMARKS                    = strCouseOFLoss + " " + lossnode.Attributes["REMARKS"].InnerText.Trim();
					// objPriorLossInfo.MOD                        = txtMOD.Text;
					// objPriorLossInfo.LOSS_RUN                   = chkLOSS_RUN.Checked == false? "N" : "Y" ;
					// objPriorLossInfo.CAT_NO                     = txtCAT_NO.Text;            
					objPriorLossInfo.CUSTOMER_ID                = int.Parse(GetCustomerID().ToString()) ;
					string strClaimStatus=lossnode.Attributes["CLAIM-STATUS"].InnerText.Trim();
					if (strClaimStatus=="O")
						objPriorLossInfo.CLAIM_STATUS =	"8702";
					else if (strClaimStatus=="C")
						objPriorLossInfo.CLAIM_STATUS =	"8703";
					else
						objPriorLossInfo.CLAIM_STATUS =	"8704";
					objPriorLossInfo.APLUS_REPORT_ORDERED = 1;
					//
					objPriorLossInfoHome.LOCATION_ID =int.Parse(LOC_ID.ToString()); 
					objPriorLossInfoHome.CUSTOMER_ID = int.Parse(GetCustomerID().ToString());   
					objPriorLossInfoHome.LOSS_ADD1 = lossnode.Attributes["LOSS-LOCATION-ADD1"].InnerText.Trim();
					objPriorLossInfoHome.LOSS_ADD2 = lossnode.Attributes["LOSS-LOCATION-ADD2"].InnerText.Trim();
					objPriorLossInfoHome.LOSS_CITY =  lossnode.Attributes["LOSS-LOCATION-CITY"].InnerText.Trim();
					objPriorLossInfoHome.LOSS_STATE = lossnode.Attributes["LOSS-LOCATION-STATE"].InnerText.Trim();
					objPriorLossInfoHome.LOSS_ZIP  = lossnode.Attributes["LOSS-LOCATION-ZIP"].InnerText.Trim();
					
					objPriorLossInfoHome.CURRENT_ADD1   = lossnode.Attributes["CURRENT-ADDR1"].InnerText.Trim();
					objPriorLossInfoHome.CURRENT_ADD2   = lossnode.Attributes["CURRENT-ADDR2"].InnerText.Trim();
					objPriorLossInfoHome.CURRENT_CITY   = lossnode.Attributes["CURRENT-ADDR-CITY"].InnerText.Trim();
					objPriorLossInfoHome.CURRENT_STATE   = lossnode.Attributes["CURRENT-ADDR-STATE"].InnerText.Trim();
					objPriorLossInfoHome.CURRENT_ZIP   = lossnode.Attributes["CURRENT-ADDR-ZIP"].InnerText.Trim();
					
					objPriorLossInfoHome.POLICY_TYPE    = lossnode.Attributes["POLICY-TYPE"].InnerText.Trim();
					objPriorLossInfoHome.POLICY_NUMBER   = lossnode.Attributes["POLICY-NUM"].InnerText.Trim();
					objPriorLossInfo.POLICY_NUM 		= lossnode.Attributes["POLICY-NUM"].InnerText.Trim();
					objPriorLossInfo.LOSS_CARRIER		= lossnode.Attributes["LOSS-CARRIER"].InnerText.Trim();
					//
					objPriorLossInfo.DRIVER_NAME = objPriorLossInfo.CUSTOMER_ID.ToString() + "^" + GetAppID().ToString() + "^" + GetAppVersionID().ToString() + "^" + LOC_ID + "^" + "APP";

					
					objPriorLossInfo.CREATED_BY = int.Parse(GetUserId());
					objPriorLossInfo.CREATED_DATETIME = DateTime.Now;
					objPriorLossInfo.MODIFIED_BY  = int.Parse(GetUserId());
					objPriorLossInfo.LAST_UPDATED_DATETIME  = DateTime.Now;
					objPriorLossInfo.IS_ACTIVE ="Y";

					//Calling the add method of business layer class
					ClsPriorLoss objPriorLoss = new  ClsPriorLoss(); 
					#region Check Update
                    string ConnStr = System.Configuration.ConfigurationManager.AppSettings.Get("DB_CON_STRING");
					string sqlSelPriorLoss = "SELECT * FROM APP_PRIOR_LOSS_INFO WHERE OCCURENCE_DATE = '" + oc_Date + "' AND LOB = '" + objPriorLossInfo.LOB + "' AND REMARKS = '" + objPriorLossInfo.REMARKS + "' AND CUSTOMER_ID = " + int.Parse(GetCustomerID().ToString()) + "";   // AND DRIVER_NAME='" + objPriorLossInfo.DRIVER_NAME.ToString() + "'"; // AND DRIVER_ID = " + driver_id;
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
					DataSet PriorLossds = objDataWrapper.ExecuteDataSet(sqlSelPriorLoss);
					#endregion
					if(PriorLossds.Tables[0].Rows.Count == 0)
					{
						int intRetVal = 0;
						if(strCalledFrom.ToUpper() != "WAT")
							intRetVal = objPriorLoss.Add(objPriorLossInfoHome,objPriorLossInfo);
						else
							intRetVal = objPriorLoss.Add(objPriorLossInfo);
						InserCount=InserCount+1;
					}
					else
						UpdateCount=UpdateCount+1;
				}
				if (InserCount>0 || UpdateCount>0)
				htMessageTable.Add(LOC_ID + "_" + objPriorLossInfo.LOB ,"Report details found – Prior Loss Tab Updated.");
			}
			catch(Exception ex)
			{
				throw(ex); 
			}
		}

		private void InsertToPriorLossTableAPlus(string strXMLHomeLoss,string LOC_ID)
		{
			ClsPriorLossInfo objPriorLossInfo;
			ClsPriorLossInfo_Home  objPriorLossInfoHome;
			objPriorLossInfo                            = new ClsPriorLossInfo();
			objPriorLossInfoHome						=new ClsPriorLossInfo_Home(); 
			int InserCount=0,UpdateCount=0;
			try
			{
				strXMLHomeLoss = strXMLHomeLoss.Replace("&","and");
				XmlDocument lossxml = new XmlDocument();
				lossxml.LoadXml(strXMLHomeLoss);
				
				XmlNodeList lossnodes = lossxml.SelectNodes("APLUS-PROPERTY/RETURNED-DATA");
				foreach(XmlNode lossnode in lossnodes)
				{
					//Code for retreiving the forms valus will go here		
					string oc_Date    = lossnode.SelectSingleNode("LOSS-DATA").InnerText.Trim();
					oc_Date=oc_Date.Substring(0,2) + "/" + oc_Date.Substring(2,2) + "/" + oc_Date.Substring(4,4);
					if(oc_Date!="")
						objPriorLossInfo.OCCURENCE_DATE             = Convert.ToDateTime(oc_Date);
					else
						objPriorLossInfo.OCCURENCE_DATE             = Convert.ToDateTime("1/1/1900");
  

					/* if(cl_Date!="")    
						 objPriorLossInfo.CLAIM_DATE                 = Convert.ToDateTime(cl_Date);
					 else
						 objPriorLossInfo.CLAIM_DATE                 = Convert.ToDateTime("1/1/1900");	*/
						
					string lossAmt=lossnode.SelectSingleNode("LOSS-AMOUNT").InnerText.Replace("$","").Replace(",","").Trim();
					string polType = lossnode.SelectSingleNode("POLICY-TYPE").InnerText.Trim();
					if(polType == "HO" || polType == "H" || polType == "T" || polType == "C" || polType == "I" || polType == "IM") 
						objPriorLossInfo.LOB = "1";
					else if(polType == "BOAT" || polType == "B")
						objPriorLossInfo.LOB = "4";
					else if(polType == "FIRE" || polType == "F" || polType == "EC")
						objPriorLossInfo.LOB = "6";

					else if(polType == "J")
						objPriorLossInfo.LOB = "5";
					else
						objPriorLossInfo.LOB  =  GetLOBID().ToString(); //"-1";
					//objPriorLossInfo.LOSS_TYPE                  = 0;//cmbLOSS_TYPE.SelectedValue.ToString()=="" ? 0 : int.Parse (cmbLOSS_TYPE.SelectedValue.ToString()) ;
					objPriorLossInfo.AMOUNT_PAID                = Double.Parse(lossAmt);//Double.Parse(lossnode.SelectSingleNode("LOSS-AMOUNT").InnerText.Trim());
					//objPriorLossInfo.AMOUNT_RESERVED            = txtAMOUNT_RESERVED.Text==""?0:double.Parse(txtAMOUNT_RESERVED.Text);
					string claimStat = lossnode.SelectSingleNode("CLAIM-STATUS").InnerText.Trim();
//					if(claimStat == "C")
//						objPriorLossInfo.CLAIM_STATUS               = "8703";
//					else if(claimStat == "O")
//						objPriorLossInfo.CLAIM_STATUS               = "8702";
//					else
//						objPriorLossInfo.CLAIM_STATUS               = "8704";

					objPriorLossInfo.CLAIM_STATUS = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CLAIM", claimStat).ToString();

					string lossTyp = lossnode.SelectSingleNode("CLAIM-TYPE").InnerText.Trim();
					objPriorLossInfo.LOSS_TYPE =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CLOSS", lossTyp);
					objPriorLossInfo.LOSS_LOCATION = lossnode.SelectSingleNode("LOSS-LOCATION").InnerText.Trim();
					objPriorLossInfo.CAUSE_OF_LOSS = lossnode.SelectSingleNode("CAUSE-OF-LOSS").InnerText.Trim();
					objPriorLossInfo.POLICY_NUM= lossnode.SelectSingleNode("POLICY-NUM").InnerText.Trim();
					objPriorLossInfo.LOSS_CARRIER= lossnode.SelectSingleNode("LOSS-CARRIER").InnerText.Trim();

					objPriorLossInfo.REMARKS                    = lossnode.SelectSingleNode("REMARKS").InnerText.Trim();
					// objPriorLossInfo.MOD                        = txtMOD.Text;
					// objPriorLossInfo.LOSS_RUN                   = chkLOSS_RUN.Checked == false? "N" : "Y" ;
					// objPriorLossInfo.CAT_NO                     = txtCAT_NO.Text;            
					objPriorLossInfo.CUSTOMER_ID                = int.Parse(GetCustomerID().ToString()) ;

					objPriorLossInfo.APLUS_REPORT_ORDERED = 1;
					//
/*					objPriorLossInfoHome.LOCATION_ID =int.Parse(LOC_ID.ToString()); 
					objPriorLossInfoHome.CUSTOMER_ID = int.Parse(GetCustomerID().ToString());   
					objPriorLossInfoHome.LOSS_ADD1 = lossnode.SelectSingleNode("LOSS-LOCATION-ADD1").InnerText.Trim();
					objPriorLossInfoHome.LOSS_ADD2 = lossnode.SelectSingleNode("LOSS-LOCATION-ADD2").InnerText.Trim();
					objPriorLossInfoHome.LOSS_CITY =  lossnode.SelectSingleNode("LOSS-LOCATION-CITY").InnerText.Trim();
					objPriorLossInfoHome.LOSS_STATE = lossnode.SelectSingleNode("LOSS-LOCATION-STATE").InnerText.Trim();
					objPriorLossInfoHome.LOSS_ZIP  = lossnode.SelectSingleNode("LOSS-LOCATION-ZIP").InnerText.Trim();
					
					objPriorLossInfoHome.CURRENT_ADD1   = lossnode.SelectSingleNode("CURRENT-ADDR1").InnerText.Trim();
					objPriorLossInfoHome.CURRENT_ADD2   = lossnode.SelectSingleNode("CURRENT-ADDR2").InnerText.Trim();
					objPriorLossInfoHome.CURRENT_CITY   = lossnode.SelectSingleNode("CURRENT-ADDR-CITY").InnerText.Trim();
					objPriorLossInfoHome.CURRENT_STATE   = lossnode.SelectSingleNode("CURRENT-ADDR-STATE").InnerText.Trim();
					objPriorLossInfoHome.CURRENT_ZIP   = lossnode.SelectSingleNode("CURRENT-ADDR-ZIP").InnerText.Trim();
					
					objPriorLossInfoHome.POLICY_TYPE    = lossnode.SelectSingleNode("POLICY-TYPE").InnerText.Trim();
					objPriorLossInfoHome.POLICY_NUMBER   = lossnode.SelectSingleNode("POLICY-NUM").InnerText.Trim();
*/
					//
					objPriorLossInfo.DRIVER_NAME = objPriorLossInfo.CUSTOMER_ID.ToString() + "^" + GetAppID().ToString() + "^" + GetAppVersionID().ToString() + "^" + LOC_ID + "^" + "APP";

					
					objPriorLossInfo.CREATED_BY = int.Parse(GetUserId());
					objPriorLossInfo.CREATED_DATETIME = DateTime.Now;
					objPriorLossInfo.MODIFIED_BY  = int.Parse(GetUserId());
					objPriorLossInfo.LAST_UPDATED_DATETIME  = DateTime.Now;
					objPriorLossInfo.IS_ACTIVE ="Y";

					//Calling the add method of business layer class
					ClsPriorLoss objPriorLoss = new  ClsPriorLoss(); 
					#region Check Update
                    string ConnStr = System.Configuration.ConfigurationManager.AppSettings.Get("DB_CON_STRING");
					string sqlSelPriorLoss = "SELECT * FROM APP_PRIOR_LOSS_INFO WHERE OCCURENCE_DATE = '" + oc_Date + "' AND LOB = '" + objPriorLossInfo.LOB + "' AND REMARKS = '" + objPriorLossInfo.REMARKS + "' AND CUSTOMER_ID = " + int.Parse(GetCustomerID().ToString()) + "";   // AND DRIVER_NAME='" + objPriorLossInfo.DRIVER_NAME.ToString() + "'"; // AND DRIVER_ID = " + driver_id;
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
					DataSet PriorLossds = objDataWrapper.ExecuteDataSet(sqlSelPriorLoss);
					#endregion
					if(PriorLossds.Tables[0].Rows.Count == 0)
					{
						int intRetVal = 0;
						if(strCalledFrom.ToUpper() != "WAT")
							intRetVal = objPriorLoss.Add(objPriorLossInfoHome,objPriorLossInfo);
						else
							intRetVal = objPriorLoss.Add(objPriorLossInfo);
						InserCount=InserCount+1;
					}
					else
						UpdateCount=UpdateCount+1;
				}
				if (InserCount>0 || UpdateCount>0)
					htMessageTable.Add(LOC_ID + "_" + objPriorLossInfo.LOB ,"Report details found – Prior Loss Tab Updated.");
			}
			catch(Exception ex)
			{
				throw(ex); 
			}
		}
#endregion

	}
}
