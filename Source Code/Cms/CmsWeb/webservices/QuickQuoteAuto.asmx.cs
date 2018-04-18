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
using System.Web.Services.Protocols;

namespace Cms.CmsWeb.webservices
{
	[WebService(Namespace="http://wolverinemutual.com")]
	public class QuickQuoteAuto : System.Web.Services.WebService
	{
		AuthenticationToken AuthenticationTokenHeader;
		private string authenticationKey = "";
		private string SystemID; 
		public QuickQuoteAuto()
		{
			InitializeComponent();
			SystemID = System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID"); 
		}
		
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetAutoDropDownDefaultXml(string LobCode)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				XmlDocument AutoDefaultValues = new XmlDocument();
			
				string lstrUserName=ConfigurationSettings.AppSettings.Get("IUserName");
				string lstrPassword=ConfigurationSettings.AppSettings.Get("IPassWd");
				string lstrDomain=ConfigurationSettings.AppSettings.Get("IDomain");
			
				ClsAttachment lImpertionation =  new ClsAttachment();
				if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
				{
					string DefaultXmlPath="";
				
					if (LobCode=="CYCL")
						DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\CYCL_DEFAULT_VALUES.xml";
					else if (LobCode=="BOAT")
						DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\BOAT_DEFAULT_VALUES.xml";
					else
						DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\AUTO_DEFAULT_VALUES.XML";
				
					AutoDefaultValues.Load(DefaultXmlPath);
					lImpertionation.endImpersonation();
					//Commented (Default values are set up in Default XML)//
					/*if (LobCode=="BOAT")   
						return(new ClsAuto().GetCountyList(AutoDefaultValues.OuterXml));
					else*/
					return(AutoDefaultValues.OuterXml);	
				}
				else
				{
					return("<quickQuote><error>Impertionation Failed: Contact Your Software Provider.</error></quickQuote>");
				}
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
		
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetAutoMakes(string strModelYear)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				return(new ClsAuto().GetVehicleMakeXml(strModelYear));
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetAutoModels(string strModelYear,string strModelMake)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
                return(new ClsAuto().GetVehicleModelXml(strModelYear,strModelMake));
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetAutoVinNos(string strModelYear,string strModelMake,string strModelName)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
                return(new ClsAuto().GetVehicleVinSymbol(strModelYear,strModelMake,strModelName));
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetUnderWritingTier(string lapseDays,string priorLimit,string totalNAF,string limitType,string effectiveDate,string stateID)
		{	
			ClsUnderwritingTier objTier= new ClsUnderwritingTier("QQ");
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				return(objTier.GetUnderWritingTierQQ(lapseDays,priorLimit,totalNAF,limitType,effectiveDate,stateID));
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetDefaultXml(string UserId,string CustomerId,string LobCode,string StateName)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				XmlDocument DefaultXml = new XmlDocument();
			
				string strDefault_Xml="";
				strDefault_Xml = new ClsAuto().GetUserDefaultXml(UserId,LobCode,StateName);
				if (strDefault_Xml.Trim()=="")
				{
					string lstrUserName=ConfigurationSettings.AppSettings.Get("IUserName");
					string lstrPassword=ConfigurationSettings.AppSettings.Get("IPassWd");
					string lstrDomain=ConfigurationSettings.AppSettings.Get("IDomain");
		
					ClsAttachment lImpertionation =  new ClsAttachment();
					if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
					{
						string DefaultXmlPath="";
					
						if (LobCode=="CYCL")
							DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\CYCL_WOLVERINE.xml";						
						else if (LobCode=="BOAT")
							DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\BOAT_WOLVERINE.xml";	
						else
							DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\AUTO_WOLVERINE.XML";

						DefaultXml.Load(DefaultXmlPath);
						lImpertionation.endImpersonation();
						strDefault_Xml = DefaultXml.OuterXml;
					}
					else
					{
						strDefault_Xml = "<quickQuote><error>Impertionation Failed: Contact Your Software Provider.</error></quickQuote>";
					}
				}
				if (LobCode=="CYCL" || LobCode=="AUTOP")
					strDefault_Xml = new ClsAuto().UpdateZipInsuranceScoreIntoXml(strDefault_Xml,CustomerId,"quickQuote/policy");
				else if (LobCode=="BOAT")
					strDefault_Xml = new ClsAuto().UpdateZipInsuranceScoreIntoXml(strDefault_Xml,CustomerId,"QUICKQUOTE/POLICY");
				return(strDefault_Xml);
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
		
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetQuoteXml(string UserId,string CustomerId,string QuoteId,string LobCd,string StateName)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				XmlDocument DefaultXml = new XmlDocument();
			
				string strDefault_Xml="";
				strDefault_Xml = new ClsQuickQuote().GetQuickQuoteXml(CustomerId,QuoteId);
				if (strDefault_Xml.Trim()=="")
				{
					strDefault_Xml = new ClsAuto().GetUserDefaultXml(UserId,LobCd,StateName);
					if (strDefault_Xml.Trim()=="")
					{
						string lstrUserName=ConfigurationSettings.AppSettings.Get("IUserName");
						string lstrPassword=ConfigurationSettings.AppSettings.Get("IPassWd");
						string lstrDomain=ConfigurationSettings.AppSettings.Get("IDomain");
			
						ClsAttachment lImpertionation =  new ClsAttachment();
						if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
						{
							string DefaultXmlPath="";
					
							if (LobCd=="CYCL")
								DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\CYCL_WOLVERINE.xml";						
							else if (LobCd=="BOAT")
								DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\BOAT_WOLVERINE.xml";	
							else
								DefaultXmlPath = Server.MapPath("../QuickQuote").ToString() + "\\AUTO_WOLVERINE.XML";

							DefaultXml.Load(DefaultXmlPath);
							lImpertionation.endImpersonation();
							strDefault_Xml = DefaultXml.OuterXml;
						}
						else
						{
							strDefault_Xml = "<quickQuote><error>Impertionation Failed: Contact Your Software Provider.</error></quickQuote>";
						}
					}
					if (LobCd=="CYCL" || LobCd=="AUTOP")
						strDefault_Xml = new ClsAuto().UpdateZipInsuranceScoreIntoXml(strDefault_Xml,CustomerId,"quickQuote/policy");
					else if (LobCd=="BOAT")
						strDefault_Xml = new ClsAuto().UpdateZipInsuranceScoreIntoXml(strDefault_Xml,CustomerId,"QUICKQUOTE/POLICY");
				}
					//To fetch the Insurance score form the database : 8 feb 2006
				else
				{
					if (LobCd=="CYCL" || LobCd=="AUTOP")
						strDefault_Xml = new ClsAuto().UpdateInsuranceScoreIntoXml(strDefault_Xml,CustomerId,"quickQuote/policy");
					else if (LobCd=="BOAT")
						strDefault_Xml = new ClsAuto().UpdateInsuranceScoreIntoXml(strDefault_Xml,CustomerId,"QUICKQUOTE/POLICY");

				}
				//End Insurance Score
				return(strDefault_Xml);
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]		
		public string CheckZipCode(string StateName,string LobCd,string ZipCode,string polEffectiveDate)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				return(new ClsQuickQuote().CheckZipCode(StateName,LobCd,ZipCode,polEffectiveDate));
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public string CheckZipCodeAutoComm(string StateName,string LobCd,string ZipCode,string polEffectiveDate)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				return(new ClsQuickQuote().CheckZipCodeAutoComm(StateName,LobCd,ZipCode,polEffectiveDate));
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}


		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public bool SendDefaultXml(string DefaultXml,string UserId,string LobCd,string StateName)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				ClsAuto blClsAuto = new ClsAuto();
				ClsQuickQuoteInfo modelQQInfo = new ClsQuickQuoteInfo();
				modelQQInfo.USER_ID = int.Parse(UserId);
				modelQQInfo.DEFAULT_XML = DefaultXml.Replace("'","''");
				modelQQInfo.LOB = LobCd;
				modelQQInfo.STATE = StateName;
				blClsAuto.SaveUserDefaultXml(modelQQInfo);
				return(true);
			}
			else
				return(false);
		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
 		public bool SendQuickQuoteXml(string QuickQuoteXml,string ClientId,string QuoteId,string LobCd)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string strDefault_Xml = new ClsQuickQuote().GetQuickQuoteXml(ClientId,QuoteId);
				if (strDefault_Xml!=QuickQuoteXml)
				{
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation ObjClass = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
					if (LobCd=="AUTOP")
					{
						QuickQuoteXml = new ClsQuickQuote().UpdateGaragedLocationAddress(QuickQuoteXml,"2");
						QuickQuoteXml = ObjClass.SetAssignDriverAcciVioPointsNode(QuickQuoteXml);
					}
					else if (LobCd=="CYCL")
					{
						QuickQuoteXml = new ClsQuickQuote().UpdateGaragedLocationAddress(QuickQuoteXml,"3");
						QuickQuoteXml = ObjClass.SetAssignDriverAcciVioPointsNodeMotor(QuickQuoteXml);

					}
					new ClsQuickQuote().UpdateQuickQuoteXml(ClientId,QuoteId,QuickQuoteXml);
				}
				return(true);
			}
			else
				return(false);


		}
		
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public bool GenerateRatingReport(string QuickQuoteXml,string ClientId,string QuoteId,string LobCd)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation ObjClass = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				string strDefault_Xml = new ClsQuickQuote().GetQuickQuoteXml(ClientId,QuoteId);
				if (strDefault_Xml!=QuickQuoteXml)
				{
					if (LobCd=="AUTOP")
					{
						QuickQuoteXml = new ClsQuickQuote().UpdateGaragedLocationAddress(QuickQuoteXml,"2");
						QuickQuoteXml = ObjClass.SetAssignDriverAcciVioPointsNode(QuickQuoteXml);
					}
					else if (LobCd=="CYCL")
					{
						QuickQuoteXml = new ClsQuickQuote().UpdateGaragedLocationAddress(QuickQuoteXml,"3");
						QuickQuoteXml = ObjClass.SetAssignDriverAcciVioPointsNodeMotor(QuickQuoteXml);
					}
					new ClsQuickQuote().UpdateQuickQuoteXml(ClientId,QuoteId,QuickQuoteXml);
				}
				if (new ClsQuickQuote().CheckQuickQuoteUpdateForRating(ClientId,QuoteId))
				{
					string RatingReport="";
					if (LobCd == "AUTOP")
					{
						RatingReport = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(SystemID).GetAutoQuoteXMLForQuickQuote(QuickQuoteXml);
						//Modified on 13 June 2008 : Repalce Invalid Characters 
						RatingReport = RatingReport.Replace("&amp;","&");
						new ClsQuickQuote().UpdateQuickQuoteRatingReport(ClientId,QuoteId,RatingReport);
					}
					else if (LobCd == "BOAT")
					{
						RatingReport = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(SystemID).GetWatercraftQuoteXMLForQuickQuote(QuickQuoteXml);
						//Modified on 5 May 2008 : Repalce Invalid Characters 
						RatingReport = RatingReport.Replace("&amp;","&");
						new ClsQuickQuote().UpdateQuickQuoteRatingReport(ClientId,QuoteId,RatingReport);
					}
					else if (LobCd == "CYCL")
					{
						RatingReport = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(SystemID).GetMotorcycleQuoteXMLForQuickQuote(QuickQuoteXml);
						RatingReport = RatingReport.Replace("&amp;","&");
						new ClsQuickQuote().UpdateQuickQuoteRatingReport(ClientId,QuoteId,RatingReport);
					}
				}
				return(true);
			}
			else
				return(false);

		}
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public bool GetQuickQuoteStatus(string ClientId,string QuoteId)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
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

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public string MakeApplication(string LobCd,string strCustomerId,string strQuickQuoteId,string strState,string strQuickQuoteNumber,string strUserId)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string strMessage = "";
				string strMessageId = "0";  //0 Lob Not Implemented //1 Successfully Exported //2 Information Missing//3 Runtime Error Occured
				string strAppNumber = "";
				string strFolderPath = ConfigurationSettings.AppSettings.Get("CmsWebUrl").ToString() + "support/";
				string AcordXml = "";
				if (LobCd == "AUTOP")
				{
					try
					{
						AcordXml = new ClsAuto().PrepareAutoAcordXml(strCustomerId,strQuickQuoteId,strFolderPath + "ACORD_AUTOP_INTERFACING_XML.xml",strState,strQuickQuoteNumber);

						Cms.CmsWeb.AcordXmlParser objParser = new AcordXmlParser();
						objParser.LoadXmlString(AcordXml);
						AutoP obj = null;
				
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
						strMessage = " Application Created Successfully.\n Your new Application Number is " + objAppInfo.APP_NUMBER.ToString().Trim() + "";
						strMessageId = "1";
					}
					catch(Exception QQEx)
					{
						strMessage = " Some Runtime error Occured.\n Error: " + QQEx.Message.ToString();
						strMessageId = "3";
					}
				}
				else if(LobCd == "CYCL")
				{
					try
					{
						AcordXml = new ClsAuto().PrepareCyclAcordXml(strCustomerId,strQuickQuoteId,strFolderPath + "ACORD_MOTORCYCLE_INTERFACING_XML.xml",strState,strQuickQuoteNumber);

						Cms.CmsWeb.AcordXmlParser objParser = new AcordXmlParser();
						objParser.LoadXmlString(AcordXml);
						AutoP obj = null;
				
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
						strMessage = " Application Created Successfully.\n Your new Application Number is " + objAppInfo.APP_NUMBER.ToString().Trim() + "";
						strMessageId = "1";
					}
					catch(Exception QQEx)
					{
						strMessage = " Some Runtime error Occured.\n Error: " + QQEx.Message.ToString();
						strMessageId = "3";
					}
				}
				else if(LobCd == "BOAT")
				{
					try
					{
						string path = strFolderPath + "ACORD_WATERCRAFT_INTERFACING_XML.xml";

						ClsWatercraft objWater = new ClsWatercraft();
				
						AcordXml = objWater.PrepareBoatAcordXml(strCustomerId,strQuickQuoteId,path,strState,strQuickQuoteNumber);
				
						//Load ACORD XML into parser
						Cms.CmsWeb.ClsWatercraftParser objParser = new ClsWatercraftParser();	
						objParser.LoadXmlString(AcordXml);
				
						//AutoP obj = null;
						//ClsAcordWatercraft objBoat = null;
						AcordBase objBase = null;

						//Import the Watercraft data
						try
						{
							objBase = objParser.Parse();
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
							if ( objBase!= null )
							{
								objBase.QQ_ID = int.Parse(strQuickQuoteId);
								objBase.UserID = strUserId;
								objAppInfo = objBase.Import();
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
						strMessage = " Application Created Successfully.\n Your new Application Number is " + objAppInfo.APP_NUMBER.ToString().Trim() + "";
						strMessageId = "1";

					}
					catch(Exception QQEx)
					{
						strMessage = " Some Runtime error Occured.\n Error: " + QQEx.Message.ToString();
						strMessageId = "3";
					}

					//strMessage = "Lob Not Implemented Yet.";
				
					//ClsAuto objBoat = new ClsAuto();
					//AcordXml = objBoat.PrepareBoatAcordXml(strCustomerId,strQuickQuoteId,strFolderPath + "ACORD_MOTORCYCLE_INTERFACING_XML.xml",strState,strQuickQuoteNumber);



				}
			Finally:
				strMessage = "<QuickQuoteReply><Message id=\"" + strMessageId + "\" AppNumber=\"" + strAppNumber + "\">" + strMessage + "</Message></QuickQuoteReply>";
				return(strMessage);
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public string GetMotorCycleVinXml(string effectiveDate)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				return(new ClsAuto().GetMotorCycleVinXml(effectiveDate));
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public string GetHomeAppXml(string strCustomerId,string strStateName)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				return(new ClsWatercraft().GetHomeAppNumber(strCustomerId,strStateName));
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public DataSet GetHomeCoverageLimits(int customerID,int appId,int appVersionId )
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
                return(new ClsWatercraft().GetHomeLimits(customerID,appId,appVersionId));
			else
				return null;
			
		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public string GetEligibleDrivers(string strInputXml)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
                return(new ClsAuto().GetEligibleDrivers(strInputXml));
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public string GetVehicleClass(string strInputXml,string strLob)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
                return(new ClsAuto().GetVehicleClass(strInputXml,strLob));
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public string GetMotorVehicleClass(string strInputXml)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
                return(new ClsAuto().GetMotorVehicleClass(strInputXml));
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]	
		public  DataSet GetPrimaryApplicantInfo(string strCustomerId, string strAppID,string strAppVerID,string strUserId,string strCalledFrom)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
                return(new ClsWatercraft().GetPrimaryApplicantInfoForRates(strCustomerId,strAppID,strAppVerID,strUserId,strCalledFrom));
			else
				return null;
			
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
