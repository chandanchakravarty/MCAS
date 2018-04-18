/******************************************************************************************
<Author					: -   Nidhi
<Start Date				: -	8/26/2005 3:13:20 PM
<End Date				: -	
<Description			: - to display data.
<Review Date			: - 
<Reviewed By			: - 


<Modified Date			: - 08/02/06
<Modified By			: - Shafi/Ashwani
<Purpose				: - Add Submit Application Function.	

<Modified Date			: - 10/02/06
<Modified By			: - Shafi
<Purpose				: - Get Policy Details And Set The Session	

<Modified Date			: - 13 Feb. 2006 
<Modified By			: - Ashwani marked as <001>
<Purpose				: - If all UW rules   are verified then New business process is launched and completed.

<Modified Date			: - 20/02/06
<Modified By			: - Shafi
<Purpose				: - Enable Policy Menu When Application Is Submitted Successfully And Make App. changes in windows Visiblity


Modification History */

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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote ;
using Cms.BusinessLayer.BlApplication;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
namespace  Cms.Application.Aspx 
{
	/// <summary>
	/// Summary description for ShowDialog.
	/// </summary>
	public class ShowDialog : Cms.Application.appbase
	{
		private const int SHOW_VERIFICATION_STATUS=1;
		private const int SHOW_WRONG_INPUT =2;
		private const int SHOW_FAILED_MESSAGE=3;
		//ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
		int customerID=0,appId=0,appVersionId=0;
		public int validApplication=0;
		protected string appLobID="",strCalledFrom="";
		string showHTML="", strCSSNo="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden policyNumber;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy_Version_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNewBusinessProcess;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSubmitApp;
        public string head;
		private void Page_Load(object sender, System.EventArgs e)
		{

			try
			{
				// check the query string
				GetQueryString();
                SetCultureThread(GetLanguageCode());
                head = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1931");
				// call the method to return the html
				ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules(CarrierSystemID);
				 strCSSNo = GetColorScheme();
				
				if(strCalledFrom=="POLICY")
				{
					showHTML=objVerifyRules.CheckHTML(customerID,appId,appVersionId);
					if(showHTML=="" || showHTML==null)
					{								 
						showHTML= objVerifyRules.VerifyApplication(customerID,appId,appVersionId,appLobID,strCSSNo,out validApplication);
						Response.Write(showHTML);
					}
				}
				else if(strCalledFrom==CALLED_FROM_PROCESS || strCalledFrom==CALLED_FROM_PROCESS_POLICY)
				{
					string strRulesStatus="0";
					bool valid=false;	

					//string strRulesHTML = base.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);
					Cms.BusinessLayer.BlProcess.clsprocess objProcess = new Cms.BusinessLayer.BlProcess.clsprocess();

					objProcess.SystemID = CarrierSystemID;

                    string strRulesHTML = objProcess.strHTML(customerID, int.Parse(hidPolicy_ID.Value), int.Parse(hidPolicy_Version_ID.Value), out valid, out strRulesStatus, strCalledFrom);
			
					Response.Write(strRulesHTML);
					if(valid && strRulesStatus == "0") // then commit
						validApplication = 1;
					else
					{
						validApplication = 0;
					}
				}
				else if(strCalledFrom==COMMIT_ANYWAYS_RULES)
				{
					string strRulesHTML = objVerifyRules.GetRulesVerification(customerID, int.Parse(hidPolicy_ID.Value), int.Parse(hidPolicy_Version_ID.Value));			
					if(strRulesHTML!="")
					{
						string strRulesHTML1 =strRulesHTML.Substring(0,strRulesHTML.IndexOf("cms/cmsweb/css/css")) + "cms/cmsweb/css/css" + strCSSNo + strRulesHTML.Substring(strRulesHTML.IndexOf("cms/cmsweb/css/css")+19);
						Response.Write(strRulesHTML1);	
					}
					else //Added by Manoj Rathore on 12th May 2009 Itrack # 5830
					{
						string strRulesStatus="0";
						bool valid=false;	 
						Cms.BusinessLayer.BlProcess.clsprocess objProcess = new Cms.BusinessLayer.BlProcess.clsprocess();

						objProcess.SystemID = CarrierSystemID;

                        strRulesHTML = objProcess.strHTML(customerID, int.Parse(hidPolicy_ID.Value), int.Parse(hidPolicy_Version_ID.Value), out valid, out strRulesStatus, strCalledFrom);
						Response.Write(strRulesHTML);						
					}					
					return;
				}
				else
				{
					if(strCalledFrom.ToUpper().ToString().Equals("IMAGE"))
					{
						// get the validation range 
						ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
						string strIsValid=objGenInfo.GetValidationRange(customerID,appId,appVersionId);
                        //Changed by Charles on 19-May-10 for Itrack 51
                        string strCarrierSystemID = CarrierSystemID; //System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
						string strSystemID = GetSystemId();  
						//if not the Wolverine User
						if((strSystemID.ToUpper()!=strCarrierSystemID.ToUpper()) && strIsValid=="1")
						{
							hidSubmitApp.Value ="1";
							ClientScript.RegisterStartupScript(this.GetType(),"PolicyStatus","<script language='javascript'>if(window.opener.document.getElementById('cltClientTop_hidReturnPolicyStatus')!=null){window.opener.document.getElementById('cltClientTop_hidReturnPolicyStatus').value='2';}</script>");
							//Response.Write("<script>alert('Application can be submitted only with in 15 days of Effective Date');</script>");
							Response.Write("<script>window.close();</script>");
							return;
						}
					}
					
					if(strCalledFrom.ToUpper().ToString().Equals("SUBMITANYWAY"))
					{
						//Check for Deactivated Application while submit Anyway :Modified 7 sep 2007
						string isActive="";
						string strMessageApp="";
						Cms.BusinessLayer.BlApplication.ClsGeneralInformation.CheckIsactiveApplication(customerID,appId,appVersionId,out isActive,out strMessageApp);
						if(isActive!="N")
							SubmitAnyway();	
						else
						{
							string strMsgDeactiveApp="<script language='javascript'>checkForDeactiveApp();</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "AutoClose", strMsgDeactiveApp);
						}
						
					}
					else if(strCalledFrom.ToUpper()=="SUBMITAPP")
					{
						#region Submit Application
                        ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
						string strIsValid=objGenInfo.GetValidationRange(customerID,appId,appVersionId);
                        //Changed by Charles on 19-May-10 for Itrack 51
                        string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
						string strSystemID = GetSystemId(),AppStatus="";  
						//Application Status Check :Modifed Sep 07 2007
						string isActive="";
						string strMessageApp="";
						Cms.BusinessLayer.BlApplication.ClsGeneralInformation.CheckIsactiveApplication(customerID,appId,appVersionId,out isActive,out strMessageApp);
						if(isActive!="N")
						{
							//if not the Wolverine User
							if((strSystemID.ToUpper()!=strCarrierSystemID.ToUpper()) && strIsValid=="1")
							{
								hidSubmitApp.Value ="1";
                                ClientScript.RegisterStartupScript(this.GetType(), "PolicyStatus", "<script language='javascript'>if(window.opener.document.getElementById('cltClientTop_hidReturnPolicyStatus')!=null){window.opener.document.getElementById('cltClientTop_hidReturnPolicyStatus').value='2';}</script>");
								//Response.Write("<script>alert('Application can be submitted only with in 15 days of Effective Date');</script>");
								Response.Write("<script>window.close();</script>");
								return;
							}

							Cms.Application.Aspx.GeneralInformation objAppGenInfo = new Cms.Application.Aspx.GeneralInformation();
							int iPolId,iPolVersion,returnResult;						
							string strPolNumber,strMessage="";						
							objAppGenInfo.CommonApptoPolSubmit(customerID, appId, appVersionId,out iPolId, out iPolVersion, out strPolNumber, strCSSNo, ClsGeneralInformation.CalledFromSubmitImage,out returnResult, out validApplication,out showHTML, out AppStatus, out strMessage);
							Response.Write(showHTML);						
							if(returnResult>0)
							{
								SetCookieValue();
								hidPolicy_ID.Value = iPolId.ToString();
								hidPolicy_Version_ID.Value = iPolVersion.ToString();																						
								//RegisterStartupScript("PolicyStatus","<script language='javascript'>if(window.opener.document.getElementById('cltClientTop_imgSubmitApp')!=null){window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';}</script>");											
								//Response.Write("<script> window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';</script>");
								//string policyNumber = GetPolicyDetails(customerID,appId,appVersionId,appLobID);									
					
								//hidSubmitApp.Value=policyNumber;
								hidSubmitApp.Value=strPolNumber;							
					
								string sscstr="<script language='javascript'>window.opener.top.topframe.enableMenu('1,2,3');" +
									//"window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';
									"window.opener.document.getElementById('cltClientTop_imgSubmitAnyway').style.display='none';checkForalert();</script>";
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", sscstr);							
							}
							else if(returnResult==-1)
							{
								hidSubmitApp.Value="3";
								string sscstr="<script language='javascript'>" +
									//"window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';
									"window.opener.document.getElementById('cltClientTop_imgSubmitAnyway').style.display='none';checkForalert();</script>";
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", sscstr);										
							}
							else if(returnResult==-2)
							{
								hidSubmitApp.Value="6";
								string sscstr="<script language='javascript'>" +
									//"window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';
									"window.opener.document.getElementById('cltClientTop_imgSubmitAnyway').style.display='none';checkForalert();</script>";
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", sscstr);
								return;			
							}
							else
							{
								hidSubmitApp.Value="4";
								string sscstr="<script language='javascript'>checkForalert();</script>";
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", sscstr);											
							}
						}
						else
						{
							string strMsgDeactiveApp="<script language='javascript'>checkForDeactiveApp();</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "AutoClose", strMsgDeactiveApp);
						}
						
						#endregion
					}
					else if(!strCalledFrom.ToUpper().ToString().Equals("POLNEWBUSINESS"))
					{
						showHTML= objVerifyRules.VerifyApplication(customerID,appId,appVersionId,appLobID,strCSSNo,out validApplication);
						ClsRatingAndUnderwritingRules objRating = new ClsRatingAndUnderwritingRules(CarrierSystemID);
						objRating.UpdateRuleVerification(customerID,appId,appVersionId,validApplication);
						Response.Write(showHTML);
					}

				}
				
				// display the html
				//if the application is valid, show the html else transform using InputMessageXSL and shoiw the wrong/missing inputs
				if(strCalledFrom.ToUpper()=="SUBMITAPP")
				{
					if(validApplication==1)
					{
                        ClientScript.RegisterStartupScript(this.GetType(), "VisibleBtn", "<script language='javascript'>if(window.opener.document.getElementById('btnQuote')!=null){window.opener.document.getElementById('btnQuote').style.display = 'inline';}if( window.opener.document.getElementById('btnConvertAppToPolicy')!=null){window.opener.document.getElementById('btnConvertAppToPolicy').style.display = 'inline';}</script>");													
					}
				}
				if(strCalledFrom.ToUpper()!="SUBMITAPP" &&!strCalledFrom.ToUpper().ToString().Equals("POLNEWBUSINESS"))
				{
					if(validApplication==1)
					{
						//To enable the Quote button
                        ClientScript.RegisterStartupScript(this.GetType(), "VisibleBtn", "<script language='javascript'>if(window.opener.document.getElementById('btnQuote')!=null){window.opener.document.getElementById('btnQuote').style.display = 'inline';}if( window.opener.document.getElementById('btnConvertAppToPolicy')!=null){window.opener.document.getElementById('btnConvertAppToPolicy').style.display = 'inline';}</script>");							
						//Page.RegisterStartupScript("VisibleBtn", "<script language='javascript'>window.opener.document.getElementById('btnQuote').style.display = 'inline'; window.opener.document.getElementById('btnConvertAppToPolicy').style.display = 'inline';</script>");							
						/*if(strCalledFrom.ToUpper().ToString().Equals("IMAGE"))
						{
							ConvertAppintoPolicy();
						}*/				
					}
				}			
			
			}
			catch
			{}
			finally
			{}
		}
		private void SetCookieValue()
		{
            Response.Cookies["LastVisitedTab"].Value = "1";//"2";
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));
		}
		private void SubmitAnyway()
		{			
			try
			{				
				int iPolId=0,iPolVersion=0,returnResult=0;
                string strCSSNo = GetColorScheme(), strShowMessage = "", strPolNumber = "", AppStatus = "", strMessage = "";//strCalled="ANYWAY",
				Cms.Application.Aspx.GeneralInformation objAppGenInfo = new GeneralInformation();				
				objAppGenInfo.CommonApptoPolSubmit(customerID, appId, appVersionId,out iPolId, out iPolVersion, out strPolNumber, strCSSNo, ClsGeneralInformation.CalledFromSubmitAnywayImage,out returnResult, out validApplication,out strShowMessage, out AppStatus, out strMessage);
				if(validApplication==1)
				{
					SetCookieValue();
					if(returnResult>0)
					{
						//Response.Write("<script> window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';</script>");
						string strPolNum=GetPolicyDetails(customerID,appId,appVersionId,appLobID);
					
						hidSubmitApp.Value=strPolNum;
					
						string sscstr="<script language='javascript'>window.opener.top.topframe.enableMenu('1,2,3');" +
							"window.opener.document.getElementById('cltClientTop_imgSubmitAnyway').style.display='none';checkForalert();</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", sscstr);

					}	
					else if(returnResult==-2)
					{
						hidSubmitApp.Value="6";
						string sscstr="<script language='javascript'>" +
							//"window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';
							"window.opener.document.getElementById('cltClientTop_imgSubmitAnyway').style.display='none';checkForalert();</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", sscstr);
					}
				}
				else
					Response.Write(strShowMessage);
//				ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules();
//				//Ravindra Gupta(03-21-2006)
//				//Calling SubmitAnywayAppVerify only if LOB is other than Umbrella & General Liability
//				//and making application valid in case of umbrella 
//				string strShowMessage = " ";
//				if(appLobID=="5" || appLobID=="7")
//				{
//					validApplication =1;
//				}
//				else
//				{
//					strShowMessage= objVerifyRules.SubmitAnywayAppVerify(customerID,appId ,appVersionId,appLobID,strCSSNo,out validApplication,strCalled);
//				}
//				//////////////////////////////////
//				if(validApplication==1)
//				{
//					ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
//					int returnResult;
//					//gstrLobID= appLobID;
//					returnResult = objGenInfo.CopyAppToPolicy(customerID,appId,appVersionId,  int.Parse(GetUserId()),appLobID,strCalled);
//						
//					if(returnResult >0)	
//					{
//						base.ScreenId = ScreenId;						
//						string strSystemID = GetSystemId();
//						int userID =int.Parse(GetUserId());
//								
//						
//						Response.Write("<script> window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';</script>");
//						string strPolNum=GetPolicyDetails(customerID,appId,appVersionId,appLobID);
//					
//						hidSubmitApp.Value=strPolNum;
//					
//						string sscstr="<script language='javascript'>window.opener.top.topframe.enableMenu('1,2,3');" +
//							"window.opener.document.getElementById('cltClientTop_imgSubmitAnyway').style.display='none';checkForalert();</script>";
//						RegisterStartupScript("ShowMessage",sscstr);
//						
//						//txtAPP_STATUS.Text ="Complete";
//						//lblMessage.Text= "Policy Number " + strPolNum + " has been created successfully";
//						//lblMessage.Visible=true;
//						
//						
//					}
//					else
//					{
//					
////						lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("533");				
////						this.btnConvertAppToPolicy.Attributes.Add("Style","display:inline");
////						this.btnQuote.Attributes.Add("Style","display:inline");
////						lblMessage.Visible=true;
//					}
//					
//				
//				}
//				else					
//				{
//					showHTML= objVerifyRules.VerifyApplication(customerID,appId,appVersionId,appLobID,strCSSNo,out validApplication);
//					Response.Write(showHTML);
//				}	
	
			}
			catch
			{
//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
//				lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("553") + "\n" + ex.Message.ToString();		
//				lblMessage.Visible=true;
			}	

		}
		


//		#region Convert app into policy when called from Image
//		private void ConvertAppintoPolicy()
//		{
//		
//			ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
//			int returnResult;
//			string strCalled="SUBMIT";
//			try
//			{
//				returnResult=5;
//				returnResult = objGenInfo.CopyAppToPolicy(customerID,appId,appVersionId,int.Parse(GetUserId()),appLobID,strCalled);
//				if(returnResult>0)
//				{
//										
//					//RegisterStartupScript("PolicyStatus","<script language='javascript'>if(window.opener.document.getElementById('cltClientTop_imgSubmitApp')!=null){window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';}</script>");											
//					Response.Write("<script> window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';</script>");
//					string policyNumber = GetPolicyDetails(customerID,appId,appVersionId,appLobID);									
//					
//					hidSubmitApp.Value=policyNumber;
//					
//					string sscstr="<script language='javascript'>window.opener.top.topframe.enableMenu('1,2,3');" +
//					"window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';window.opener.document.getElementById('cltClientTop_imgSubmitAnyway').style.display='none';checkForalert();</script>";
//					RegisterStartupScript("ShowMessage",sscstr);
//					#region NewBusiness 
//					//if(CommitNewBusinessProcess())
//					string strHTML="";
//					//Cms.Application.Aspx.GeneralInformation
//					Cms.Application.Aspx.GeneralInformation objGeneralInfo = new Cms.Application.Aspx.GeneralInformation();
//					string strNewBusinessProcess="";					
//					strNewBusinessProcess = objGenInfo.CheckForStartNBSProcess(customerID,appId, appVersionId);
//					if(strNewBusinessProcess.ToUpper() == "Y")
//					{
//						if(objGeneralInfo.StartNBSProcess(customerID,appId,appVersionId,int.Parse(hidPolicy_ID.Value),int.Parse(hidPolicy_Version_ID.Value),CALLED_FROM_SHOW_DIAG,out strHTML,appLobID))
//						{
//							hidNewBusinessProcess.Value="5";
//						}
//						else
//							Response.Write(strHTML);
//					}
//					# endregion			
//				}
//				else if(returnResult==-1)
//				{
//					hidSubmitApp.Value="3";
//					string sscstr="<script language='javascript'>" +
//						"window.opener.document.getElementById('cltClientTop_imgSubmitApp').style.display='none';window.opener.document.getElementById('cltClientTop_imgSubmitAnyway').style.display='none';checkForalert();</script>";
//					RegisterStartupScript("ShowMessage",sscstr);										
//				}
//				else
//				{
//					hidSubmitApp.Value="4";
//					string sscstr="<script language='javascript'>checkForalert();</script>";
//					RegisterStartupScript("ShowMessage",sscstr);											
//				}
//			}						
//			catch(Exception ex)
//			{}	
//		}
//
////		The following code to start and commit new business has been commented here...
////		A common method "StartNBSProcess" defined at application/aspx/GenerelInformation.aspx.cs will be used instead
//
////		private bool CommitNewBusinessProcess()
////		{
////			// chk the web config key NewBusinessProcess for new business process 
////			string strNewBusinessProcess="";
////			bool retval=false,valid=false;
////			strNewBusinessProcess=System.Configuration.ConfigurationSettings.AppSettings.Get("NewBusinessProcess").ToString();
////			if(strNewBusinessProcess.ToUpper() == "Y")
////			{
////				ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules();
////				//<001> <start> Code added by Ashwani on 13 Feb. 2006 							
////				DataSet ds = new DataSet();
////				int userID =int.Parse(GetUserId());
////				ds=objVerifyRules.OldInputXML(customerID,appId,appVersionId);
////				string strReturn="";
////				if(ds.Tables[0].Rows.Count>0)
////				{
////					strReturn=ds.Tables[0].Rows[0]["APP_VERIFICATION_XML"].ToString(); 
////				}							
////				XmlDocument xmlDocOutput = new XmlDocument();
////				strReturn= strReturn.Replace("\t","");
////				strReturn= strReturn.Replace("\r\n","");					
////				strReturn= strReturn.Replace( "<LINK" ,"<!-- <LINK");				
////				strReturn= strReturn.Replace( " rel=\"stylesheet\"> ","rel=\"stylesheet/\"> -->");
////				strReturn= strReturn.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">","");					
////				xmlDocOutput.LoadXml("<RULEHTML>" +  strReturn + "</RULEHTML>");  
////				XmlNodeList nodLst= xmlDocOutput.GetElementsByTagName("verifyStatus");
////				// 0  All ok 
////				// 1 -- Not ok
////				string strAppStatus="";
////				if(nodLst.Count>0)
////				{
////					strAppStatus=nodLst.Item(0).InnerText; //1 or 0
////				}
////				//if '0' New business process is launched and completed.
////				
////				if (strAppStatus.Trim().Equals("0"))
////				{
////					Cms.Model.Policy.Process.ClsProcessInfo  objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
////					Cms.BusinessLayer.BlProcess.ClsNewBusinessProcess objProcess = new Cms.BusinessLayer.BlProcess.ClsNewBusinessProcess();
////					objProcessInfo.CUSTOMER_ID = customerID;	
////					objProcessInfo.POLICY_ID = int.Parse(hidPolicy_ID.Value);
////					objProcessInfo.POLICY_VERSION_ID = int.Parse(hidPolicy_Version_ID.Value);
////					objProcessInfo.CREATED_BY = userID;
////					objProcessInfo.CREATED_DATETIME = DateTime.Now;
////					retval = objProcess.StartProcess(objProcessInfo);
////					if(retval == true)
////					{
////						// Check policy mandatory informations before process commit.
////						Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules();
////						string strRulesStatus="";
////						showHTML=objRules.VerifyPolicy(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID ,objProcessInfo.POLICY_VERSION_ID,GetLOBID(),out  valid,strCSSNo,out strRulesStatus);	
////						if(valid)
////						{
////							objProcessInfo.COMPLETED_BY = userID;
////							objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
////							retval = objProcess.CommitProcess(objProcessInfo);
////						}
////						else
////						{
////							//Showing the rules window
////							Response.Write(showHTML);
////						}
////					}										
////				}
////			
////			}
////			if(retval==true && valid== true)
////			{
////				// send 5 for policy created sucesfully and accepted.
////				return true;
////			}	
////			else
////			{
////				return false;
////			}
////			
////		}
//	
//		#endregion 
		
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
         
		#region GetPolicy Detail
		private string GetPolicyDetails(int intcustomerId,int intappId,int intappVersion,string strappLobID)
		{
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			DataSet ds = objGeneralInformation.GetPolicyDetails(intcustomerId,intappId,intappVersion);
				
			
			if (ds.Tables[0].Rows.Count > 0)
			{
				
				//Setting the polic details in session
				SetCustomerID(intcustomerId.ToString());
				SetAppID(intappId.ToString());
				SetAppVersionID(intappVersion.ToString());
                SetPolicyID(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				hidPolicy_ID.Value=ds.Tables[0].Rows[0]["POLICY_ID"].ToString();
				hidPolicy_Version_ID.Value=ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
				SetPolicyVersionID(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				SetLOBID(strappLobID);				
				
				//(cmsbase(this.Page)).SetLOBID(FetchValueFromXML("LOB_ID",hidOldData.Value));
			}
            objGeneralInformation = null;
			return ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
			

		}
			#endregion

		#region  Query String 
		/// <summary>
		/// get the query string 
		/// </summary>
		private void GetQueryString()
		{
			if(Request.QueryString["CUSTOMER_ID"]!= null && Request.QueryString["CUSTOMER_ID"].ToString()!="")
			{
				customerID =int.Parse (Request.QueryString["CUSTOMER_ID"].ToString());
			}
			if(Request.QueryString["APP_ID"]!= null && Request.QueryString["APP_ID"].ToString()!="")
			{
				appId=int.Parse (Request.QueryString["APP_ID"].ToString());
			}
			if(Request.QueryString["APP_VERSION_ID"]!= null && Request.QueryString["APP_VERSION_ID"].ToString()!="")
			{
				appVersionId=int.Parse (Request.QueryString["APP_VERSION_ID"].ToString());
			}
				
			if(Request.QueryString["LOBID"]!= null && Request.QueryString["LOBID"].ToString()!="")
			{
				appLobID=Request.QueryString["LOBID"].ToString();
			}
			else
			{
				DataSet dsApplication = ClsGeneralInformation.FetchApplication(int.Parse(customerID.ToString()),int.Parse(appId.ToString()),int.Parse(appVersionId.ToString()));
				if(dsApplication != null)
				{
					if(dsApplication.Tables[0].Rows.Count > 0)
					{
						appLobID = dsApplication.Tables[0].Rows[0]["LOB_ID"].ToString();
					}
				}
			}

			if(Request.QueryString["CALLEDFROM"]!= null && Request.QueryString["CALLEDFROM"].ToString()!="")
			{
				strCalledFrom=Request.QueryString["CALLEDFROM"].ToString();
			}	
			if(Request.QueryString["POLICY_ID"]!= null && Request.QueryString["POLICY_ID"].ToString()!="")
			{
				hidPolicy_ID.Value=Request.QueryString["POLICY_ID"].ToString();
			}	
			if(Request.QueryString["POLICY_VERSION_ID"]!= null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="")
			{
				hidPolicy_Version_ID.Value=Request.QueryString["POLICY_VERSION_ID"].ToString();
			}	
		}
		#endregion

	}
}
