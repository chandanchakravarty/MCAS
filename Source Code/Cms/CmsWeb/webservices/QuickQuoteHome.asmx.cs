using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services; 
using System.Xml;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application;
using Cms.Model.Quote;
using System.IO;
using System.Web.Services.Protocols;

namespace Cms.CmsWeb.webservices
{
	[WebService(Namespace="http://wolverinemutual.com")]
	public class QuickQuoteHome : System.Web.Services.WebService
	{
		public AuthenticationToken AuthenticationTokenHeader;
		private string authenticationKey ="";
		string SystemID ; 

		public QuickQuoteHome()
		{
			InitializeComponent();
			SystemID = System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID"); 
		}
		
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetDropDownDefaultXml(string LobCd)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				XmlDocument HomeDefaultValues = new XmlDocument();
			
				string lstrUserName=ConfigurationSettings.AppSettings.Get("IUserName");
				string lstrPassword=ConfigurationSettings.AppSettings.Get("IPassWd");
				string lstrDomain=ConfigurationSettings.AppSettings.Get("IDomain");
			
				ClsAttachment lImpertionation =  new ClsAttachment();
				if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
				{
					string DefaultXmlPath = "";
					if (LobCd == "HOME")
						DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\HOME_DEFAULT_VALUES.XML";
					else if (LobCd == "UMB")
						DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\UMB_DEFAULT_VALUES.XML";
					else
						DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\REDW_DEFAULT_VALUES.XML";

					HomeDefaultValues.Load(DefaultXmlPath);
					lImpertionation.endImpersonation();
					return(HomeDefaultValues.OuterXml);	
				}
				else
				{
					return("<quickQuote><error>Impertionation Failed: Contact Your Software Provider.</error></quickQuote>");
				}
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetDefaultXml(string UserId,string CustomerId,string LobCd,string StateName)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				XmlDocument DefaultXml = new XmlDocument();
			
				string strDefault_Xml="";
				strDefault_Xml = new ClsHome().GetUserDefaultXml(UserId,LobCd,StateName);
				if (strDefault_Xml.Trim()=="")
				{
					string lstrUserName=ConfigurationSettings.AppSettings.Get("IUserName");
					string lstrPassword=ConfigurationSettings.AppSettings.Get("IPassWd");
					string lstrDomain=ConfigurationSettings.AppSettings.Get("IDomain");
		
					ClsAttachment lImpertionation =  new ClsAttachment();
					if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
					{
						string DefaultXmlPath = "";
						if (LobCd == "HOME")
							DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\HOME_WOLVERINE.XML";
						else if (LobCd == "UMB")
							DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\UMB_WOLVERINE.XML";
						else
							DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\REDW_WOLVERINE.XML";

						DefaultXml.Load(DefaultXmlPath);
						lImpertionation.endImpersonation();
						strDefault_Xml = DefaultXml.OuterXml;
					}
					else
					{
						strDefault_Xml = "<quickQuote><error>Impertionation Failed: Contact Your Software Provider.</error></quickQuote>";
					}
				}
				if (LobCd != "UMB")
					strDefault_Xml = new ClsHome().UpdateZipInsuranceScoreIntoXml(strDefault_Xml,CustomerId);

				return(strDefault_Xml);
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetQuoteXml(string UserId,string CustomerId,string QuoteId,string LobCd,string StateName)
		{

			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{

				XmlDocument DefaultXml = new XmlDocument();
			
				string strDefault_Xml="";
				strDefault_Xml = new ClsQuickQuote().GetQuickQuoteXml(CustomerId,QuoteId);
				if (strDefault_Xml.Trim()=="")
				{
					strDefault_Xml = new ClsHome().GetUserDefaultXml(UserId,LobCd,StateName);
					if (strDefault_Xml.Trim()=="")
					{
						string lstrUserName=ConfigurationSettings.AppSettings.Get("IUserName");
						string lstrPassword=ConfigurationSettings.AppSettings.Get("IPassWd");
						string lstrDomain=ConfigurationSettings.AppSettings.Get("IDomain");
			
						ClsAttachment lImpertionation =  new ClsAttachment();
						if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
						{
							string DefaultXmlPath = "";
							if (LobCd == "HOME")
								DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\HOME_WOLVERINE.XML";
							else if (LobCd == "UMB")
								DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\UMB_WOLVERINE.XML";
							else
								DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\REDW_WOLVERINE.XML";

							DefaultXml.Load(DefaultXmlPath);
							lImpertionation.endImpersonation();
							strDefault_Xml = DefaultXml.OuterXml;
						}
						else
						{
							strDefault_Xml = "<quickQuote><error>Impertionation Failed: Contact Your Software Provider.</error></quickQuote>";
						}
					}
				
					/*strDefault_Xml = new ClsHome().UpdateZipInsuranceScoreIntoXml(strDefault_Xml,CustomerId);*/
				}
				/*Update Insurance XML in case User chnages the Insurance Score:*/
				strDefault_Xml = new ClsHome().UpdateZipInsuranceScoreIntoXml(strDefault_Xml,CustomerId);
				return(strDefault_Xml);
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
			

		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string CheckZipCode(string StateName,string LobCd,string ZipCode,string polEffectiveDate)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				return(new ClsQuickQuote().CheckZipCode(StateName,LobCd,ZipCode,polEffectiveDate));
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}
		//To get the EarthQuake ZOne : 
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetEartquakeZone(string StateName,string LobCd,string ZipCode)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				return(new ClsQuickQuote().GetEarthquakeZone(LobCd,StateName,ZipCode));
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public DataSet GetProtectionClass(string protectionClass,int milesToDwell,string feetToHydrant,string lobCode)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				return(new ClsHome().FetchProtectionClass(protectionClass,milesToDwell,feetToHydrant,lobCode));
			}
			else
				return null;
			
		}


		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public bool SendDefaultXml(string DefaultXml,string UserId,string LobCd,string StateName)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				ClsHome blClsHome = new ClsHome();
				ClsQuickQuoteInfo modelQQInfo = new ClsQuickQuoteInfo();
				modelQQInfo.USER_ID = int.Parse(UserId);
				modelQQInfo.DEFAULT_XML = DefaultXml.Replace("'","''");
				modelQQInfo.LOB = LobCd;
				modelQQInfo.STATE = StateName;
				blClsHome.SaveUserDefaultXml(modelQQInfo);
				return(true);
			}
			else
				return(false);

		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public bool SendQuickQuoteXml(string QuickQuoteXml,string ClientId,string QuoteId)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				QuickQuoteXml = new ClsHome().UpdateTerritoryCodeIntoXml(QuickQuoteXml);
				if (QuickQuoteXml!="")
				{
					string strDefault_Xml = new ClsQuickQuote().GetQuickQuoteXml(ClientId,QuoteId);
					if (strDefault_Xml!=QuickQuoteXml)
					{
						new ClsQuickQuote().UpdateQuickQuoteXml(ClientId,QuoteId,QuickQuoteXml);
					}
					return(true);
				}
				else
					return(false);
			}
			else
				return(false);

		}
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public bool SendUmbQuickQuoteXml(string QuickQuoteXml,string ClientId,string QuoteId)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				//QuickQuoteXml = new ClsHome().UpdateTerritoryCodeIntoXml(QuickQuoteXml);
				if (QuickQuoteXml!="")
				{
					string strDefault_Xml = new ClsQuickQuote().GetQuickQuoteXml(ClientId,QuoteId);
					if (strDefault_Xml!=QuickQuoteXml)
					{
						new ClsQuickQuote().UpdateQuickQuoteXml(ClientId,QuoteId,QuickQuoteXml);
					}
					return(true);
				}
				else
					return(false);
			}
			else
				return(false);

		}
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetTerritoryCounty(string lobID,string zipCode)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				string strTerrCounty="";
				strTerrCounty = new ClsHome().GetTerritoryCounty(lobID,zipCode);
				return strTerrCounty;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public bool GetQuickQuoteStatus(string ClientId,string QuoteId)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				string strStatus = new ClsQuickQuote().GetQuickQuoteStatus(ClientId,QuoteId);
				if(strStatus == "Y")
					return(true);
				else
					return(false);
			}
			else
				return(false);

		}
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public  DataSet GetPrimaryApplicantInfo(string strCustomerId, string strAppID,string strAppVerID,string strUserId,string strCalledFrom)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				return(new ClsWatercraft().GetPrimaryApplicantInfoForRates(strCustomerId,strAppID,strAppVerID,strUserId,strCalledFrom));
			}
			else
				return null;

		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public bool GenerateRatingReport(string QuickQuoteXml,string ClientId,string QuoteId,string LobCd)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				QuickQuoteXml = new ClsHome().UpdateTerritoryCodeIntoXml(QuickQuoteXml);
				if (QuickQuoteXml!="")
				{
					string strDefault_Xml = new ClsQuickQuote().GetQuickQuoteXml(ClientId,QuoteId);
					if (strDefault_Xml!=QuickQuoteXml)
					{
						new ClsQuickQuote().UpdateQuickQuoteXml(ClientId,QuoteId,QuickQuoteXml);
					}
					if (new ClsQuickQuote().CheckQuickQuoteUpdateForRating(ClientId,QuoteId))
					{
						string RatingReport = "";
						if (LobCd == "HOME")
							RatingReport = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(SystemID).GetHO3QuoteXMLForQuickQuote(QuickQuoteXml);
						else if (LobCd == "REDW")
							RatingReport = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(SystemID).GetRentaDewllingForQuickQuote(QuickQuoteXml);

						new ClsQuickQuote().UpdateQuickQuoteRatingReport(ClientId,QuoteId,RatingReport);
					}
					return(true);			
				}
				else
					return(false);
			}
			else
				return(false);

		}
		
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string MakeApplication(string LobCd,string strCustomerId,string strQuickQuoteId,string strState,string strQuickQuoteNumber,string strUserId)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == ClsCommon.ServiceAuthenticationToken)
			{
				string strMessage = "";
				string strMessageId = "0";  //0 Lob Not Implemented //1 Successfully Exported //2 Information Missing//3 Runtime Error Occured
				string strAppNumber = "";
				string strFolderPath = ConfigurationSettings.AppSettings.Get("CmsWebUrl").ToString() + "support/";
				string AcordXml = "";

				//			try
				//			{
				//				//Temporary folder for pdf files
				//				Directory.CreateDirectory("C:\\temp");
				//				System.IO.StreamWriter QQWriter = new System.IO.StreamWriter("C:\\temp\\quote_id.txt");
				//				QQWriter.Write(strQuickQuoteId);
				//				QQWriter.Close();
				//
				//			}
				//			catch(Exception ex)
				//			{}
				//
				if (LobCd == "HOME")
				{
					try
					{
					
						AcordXml = new ClsHome().PrepareHomeAcordXml(strCustomerId,strQuickQuoteId,strFolderPath + "ACORD_HOME_INTERFACING_XML.xml",strState,strQuickQuoteNumber);

						Cms.CmsWeb.HomeLOBParser objParser = new HomeLOBParser();
						objParser.LoadXmlString(AcordXml);
						AcordBase obj = null;
				
						try
						{
							obj = objParser.Parse();
						}
						catch(Exception ex)
						{
							strMessage = ex.Message.ToString();
							strMessageId = "2";
							goto Finally;
						}
			
						ClsGeneralInfo objAppInfo = null;
						try
						{
							if ( obj!= null )
							{
								obj.QQ_ID = int.Parse(strQuickQuoteId);
								obj.UserID = strUserId;
								objAppInfo = obj.Import();
							}
						}
						catch(Exception ex)
						{
							strMessage = ex.Message.ToString();
							strMessageId = "2";
							goto Finally;
						}
				
						strAppNumber = objAppInfo.APP_NUMBER.ToString().Trim() + "_" + objAppInfo.APP_ID.ToString().Trim() + "_" + objAppInfo.APP_VERSION_ID.ToString().Trim();
						new ClsQuickQuote().UpdateQuickQuoteAppNumber(strCustomerId,strQuickQuoteId,objAppInfo.APP_NUMBER.ToString().Trim());
						strMessage = " Application Created Successfully.\n Your new Application Number is '" + objAppInfo.APP_NUMBER.ToString().Trim() + "'";
						strMessageId = "1";
					}
					catch(Exception QQEx)
					{
						strMessage = " Some Runtime error Occured.\n Error: " + QQEx.Message.ToString();
						strMessageId = "3";
					}
				}
				else if(LobCd == "REDW")
				{
					//strMessage = "Lob Not Implemented Yet.";
					string path = strFolderPath + "ACORD_RENTAL_INTERFACING_XML.xml";
					try
					{
						ClsHome objHome = new ClsHome();
					
						//AcordXml = objHome.PrepareHomeAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_HOME_INTERFACING_XML.XML"),hidState.Value.ToString(),txtQQ_NUMBER.Text.ToString().Trim());
						//AcordXml = objHome.PrepareHomeAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_HOME_INTERFACING_XML.XML"),hidState.Value.ToString(),lblQuickQuoteNumber.Text.ToString().Trim());
						AcordXml = objHome.PrepareRentalHomeAcordXml(strCustomerId,strQuickQuoteId,path,strState,strQuickQuoteNumber);

						Cms.CmsWeb.HomeLOBParser objParser = new HomeLOBParser();
						objParser.LoadXmlString(AcordXml);
				
						AcordBase obj = null;
				
						try
						{
							obj = objParser.Parse();
					
						}
						catch(Exception ex)
						{
							strMessageId = "2";
							goto Finally;
						}
			
						ClsGeneralInfo objAppInfo = null;
						try
						{
							if ( obj!= null )
							{
								obj.QQ_ID = int.Parse(strQuickQuoteId);
								obj.UserID = strUserId;
								objAppInfo = obj.Import();
							}
						}
						catch(Exception ex)
						{
							strMessageId = "2";
							goto Finally;
						}
						strAppNumber = objAppInfo.APP_NUMBER.ToString().Trim() + "_" + objAppInfo.APP_ID.ToString().Trim() + "_" + objAppInfo.APP_VERSION_ID.ToString().Trim();
						new ClsQuickQuote().UpdateQuickQuoteAppNumber(strCustomerId,strQuickQuoteId,objAppInfo.APP_NUMBER.ToString().Trim());
					
						//Session["appVersionID"] = objAppInfo.APP_VERSION_ID.ToString().Trim();
						//Session["appID"] = objAppInfo.APP_ID.ToString().Trim();

						strMessage = " Application Created Successfully.\n Your new Application Number is '" + objAppInfo.APP_NUMBER.ToString().Trim() + "'";
						strMessageId = "1";
					}
					catch(Exception QQEx)
					{
						strMessage = " Some Runtime error Occured.\n Error: " + QQEx.Message.ToString();
						strMessageId = "3";
					}
				}

			Finally:
				strMessage = "<QuickQuoteReply><Message id=\"" + strMessageId + "\" AppNumber=\"" + strAppNumber + "\">" + strMessage + "</Message></QuickQuoteReply>";
				return(strMessage);
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}

		
		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			authenticationKey = ClsCommon.ServiceAuthenticationToken;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion
	}
}
