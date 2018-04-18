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
using System.Security;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.IO;
using Cms.CmsWeb.Utils;
using Cms.Model.Client;
using Cms.BusinessLayer.BlQuote;
//using APToolkitNET;
using System.Web.Services.Protocols;

namespace Cms.CmsWeb.webservices
{
	/// <summary>
	/// Summary description for AcordWebServices.
	/// </summary>
	
	[WebService(Namespace="http://wolverinemutual.com")]
	public class AcordWebServices : System.Web.Services.WebService
	{
		public AuthenticationToken AuthenticationTokenHeader;
		private string authenticationKey = "";
		string SystemID ; 
		public AcordWebServices()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

            SystemID = cmsbase.CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID"); 
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
			authenticationKey = ClsCommon.ServiceCapitalAuthenticationToken;
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

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

//		[WebMethod]
//		public string HelloWorld()
//		{
//			return "Hello World";
//		}
		
		/// <summary>
		///  Returns Acord xml with premium nodes
		/// </summary>
		/// <param name="strFilePath"></param>
		/// <returns></returns>
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string ReturnAcordXMLPremium(string strAcordXML, string AgencyId)
		{

			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{

				#region PARSE CUSTOMER TO POPULATE CUSTOMER MODEL FOR WEB METHOD INPUT
			
				//			XmlDocument doc = new XmlDocument();
				//			doc.LoadXml(strAcordXML);
				//
				//			ClsCustomerInfo objCustInfo = null;  //Client Model
				//
				//			//Populating the Client Model Object for Insurance Score:
				//            foreach(XmlNode node in doc.DocumentElement.SelectNodes("InsuranceSvcRq"))
				//			{
				//				XmlNodeList appNodeList = node.SelectNodes("PersAutoPolicyQuoteInqRq");		
				//				foreach(XmlNode nodeApp in appNodeList)
				//				{
				//					objCustInfo = obj.ParseInsuredOrPrincipal(nodeApp);
				//				}
				//			}
				#endregion
			
				#region APPEND INSURANCE SCORE
				//Appending Insurance Score in Acord XML at Policy Level:
				//			const string CREDITSCOREINFO = "CreditScoreInfo";
				//			const string CREDITSCORE = "CreditScore";
				//			const int INSURANCESCORE = 100;
				//			XmlDocument policyDoc = new XmlDocument();
				//			policyDoc.LoadXml(strAcordXML);
				//
				//			XmlNode polNode = null;
				//			if(policyDoc.SelectSingleNode("//PersPolicy")!=null)
				//			polNode = policyDoc.SelectSingleNode("//PersPolicy");
				//
				//			//Create Elements
				//			XmlElement creditElememt = policyDoc.CreateElement(CREDITSCOREINFO);
				//			XmlElement creditElememtChild = policyDoc.CreateElement(CREDITSCORE);
				//
				//			//Appending Childs
				//			creditElememt.AppendChild(creditElememtChild);
				//
				//			XmlText iScoreTxt = null;
				//			polNode.AppendChild(creditElememt);
				//
				//			//Get Insurance Score
				//			//ClsCustomerInfo objCustInfo = new ClsCustomerInfo();
				//			CreditScoreDetails objScore;
				//			int intScore = -1;
				//			
				//			System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig("IIXSettings");
				//			string strUserName = dic["UserName"].ToString();
				//			string strPassword = dic["Password"].ToString();
				//			string strAccountNumber = dic["AccountNumber"].ToString();
				//			Utility objUtility = new Utility(strUserName, strPassword, strAccountNumber);	
				//			try
				//			{
				//				objScore = objUtility.GetCustomerCreditScore(objCustInfo);
				//				intScore = objScore.Score;
				//				if(intScore == -1 || intScore.ToString() == "000")
				//				{
				//					iScoreTxt = policyDoc.CreateTextNode(INSURANCESCORE.ToString());
				//				}
				//				else
				//				{
				//					iScoreTxt = policyDoc.CreateTextNode(intScore.ToString());
				//				}
				//				
				//				
				//			}
				//			catch(Exception ex)
				//			{
				//				//throw(new Exception("Error occured while parsing iix response." + objExp.Message));
				//				//this.lblMessage.Text = "Customer Insurance score can not be retrieved";
				//			}					
				//			
				//			creditElememtChild.AppendChild(iScoreTxt);
				//
				//			//Get OuterXML of Processed Document
				//			strAcordXML = "";
				//			strAcordXML = policyDoc.OuterXml; 
            
				#endregion

				

				BusinessLayer.BlQuote.ClsAcord objClsAcord = new Cms.BusinessLayer.BlQuote.ClsAcord(SystemID);
				string RetVAl = objClsAcord.GetAcordXMLPremium(strAcordXML,AgencyId); 
 			   if(RetVAl=="noAgency")
				   return "Agency does not Exist for this Request.";
				else
					return RetVAl; 
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
		
		public void SaveAcordXML(string strAcordXML,string strRqUIDXmlNode)
		{
			
			BusinessLayer.BlQuote.ClsAcord objClsAcord = new Cms.BusinessLayer.BlQuote.ClsAcord(SystemID); 
			int intAgencyID = 0;
			objClsAcord.SaveACORD_XML(strAcordXML,null,null,strRqUIDXmlNode,intAgencyID);			
		}
		
		[WebMethod(EnableSession=true)]
		[SoapHeader("AuthenticationTokenHeader")]
		public string ReturnMakeAppNumber( string GUID,string agencyID)
		{
			string ReturnValue = "";
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string ReturnMakeAppNumber = "";
				Session["userId"] ="3"; //To avoid HttpContext Checks in Bl Functions:
				
				Cms.BusinessLayer.BlQuote.AcordXMLParser.ClsAutoAcordXMLParser objClsAutoAcordXMLParser = new Cms.BusinessLayer.BlQuote.AcordXMLParser.ClsAutoAcordXMLParser(SystemID);
				// Here we will call LOB's specific blclass  object & need to pass GUID & Agency ID

				ReturnMakeAppNumber = objClsAutoAcordXMLParser.MakeApplication(GUID,agencyID);
				if(ReturnMakeAppNumber.ToUpper() == "NOAGENCY")
				{
					ReturnValue =  "This Agency is not Authorized to create EBIX ADVANTAGE application.";
				}
				else
					ReturnValue = ReturnMakeAppNumber;
			}			
			else
			{
				ReturnValue = ClsCommon.ServiceAuthenticationMsg;
			}

			string RequestDetails, ResponseMessage;//, AdditionalMessage;

			RequestDetails = "GUID= " + GUID + "  :: AgencyID = " + agencyID ; 
			ResponseMessage = ReturnValue; 
            AddRequestLog(GUID,RequestDetails,ResponseMessage,""); 
			return ReturnValue ; 
		}

		private void AddRequestLog(string RequestID, string RequestDetails, string ResponseMessage, string AdditionalMessage)
		{
			Cms.DataLayer.DataWrapper objWrapper = new Cms.DataLayer.DataWrapper(ClsCommon.ConnStr ,CommandType.StoredProcedure);
			objWrapper.AddParameter("@INSURANCE_SVC_RQ",RequestID);
			objWrapper.AddParameter("@REQUEST_DATETIME",DateTime.Now );
			objWrapper.AddParameter("@REQUEST_DETAILS",RequestDetails);
			objWrapper.AddParameter("@RETURN_MESSAGE",ResponseMessage);
			objWrapper.AddParameter("@ADDITIONAL_INFO",AdditionalMessage);
			objWrapper.ExecuteNonQuery("Proc_AddRealTimeLog");
			objWrapper.Dispose();
		}

/*
		#region Private class member
		private string strOutputPath;
		private string strInputPath;
		private string strTmpPDfPath;
		private string strInputXml;
		private string strLobCode;
		private int intClientId;
		private int intPolicyId;
		private int intPolicyVersion;
		private string strImpersonationUserId;
		private string strImpersonationPassword;
		private string strImpersonationDomain;
		private ArrayList arrFileInfo = new ArrayList();
		ImpersonateWrapper lObjImpersionate;
		#endregion

		#region Constant Variable
		const string ISIMAGE = "ISIMAGE";
		const string FLOATX = "FLOATX";
		const string FLOATY = "FLOATY";
		const string FLOATW = "FLOATW";
		const string FLOATH = "FLOATH";
		const string PAGENO = "PAGENO";
		const string IMAGEPATH = "IMAGEPATH";



		const string FIELDTYPES = "S";
		const string FIELDTYPEM = "M";
		const string FIELDTYPET = "T";
		const string FIELDTYPEI = "I";
		const string FIELDTYPEC = "C";
		const string FIELDTYPEN = "N";
		const string FIELDTYPE  = "fieldType";
		const string NODEID  = "id";
		const string PRIMARYPDF = "PrimPDF";
		const string SECONDARYPDF = "SecondPDF";
		const string PRIMARYPDFBLOCK = "PrimPDFBlocks";
		const string SECONDARYPDFBLOCK = "SecondPDFBlocks";
		#endregion

		APToolkitNET.Toolkit PrimePdfMain = new APToolkitNET.Toolkit();
		

		#region Public properties
		public string OutputPath
		{
			set
			{
				strOutputPath =	value;
			}
			get
			{
				return strOutputPath;
			}
		}
		
		public string InputPath
		{
			set
			{
				strInputPath = value;
			}
			get
			{
				return strInputPath;
			}
		}

		public string InputXml
		{
			set
			{
				strInputXml = value;
			}
		}

		public string LobCode
		{
			set
			{
				strLobCode = value;
			}
			get
			{
				return strLobCode;
			}
		}
		
		public int ClientId
		{
			set
			{
				intClientId = value;
			}
			get
			{
				return intClientId;
			}
		}
		
		public int PolicyId
		{
			set
			{
				intPolicyId = value;
			}
			get
			{
				return intPolicyId;
			}
		}

		public int PolicyVersion
		{
			set
			{
				intPolicyVersion = value;
			}
			get
			{
				return intPolicyVersion;
			}
		}
		public string ImpersonationUserId
		{
			set
			{
				strImpersonationUserId = value;
			}
			get
			{
				return strImpersonationUserId;
			}
		}
		public string ImpersonationPassword
		{
			set
			{
				strImpersonationPassword = value;
			}
			get
			{
				return strImpersonationPassword;
			}
		}
		public string ImpersonationDomain
		{
			set
			{
				strImpersonationDomain = value;
			}
			get
			{
				return strImpersonationDomain;
			}
		}
		#endregion

		[WebMethod]
		public string GeneratePDF(int iClientID, int iPolicyID, int iPolicyVersion, string sLobCode, string sInputXml, string sInputPath, string sOutputPath, string sImpersonationUserId, string sImpersonationPassword, string sImpersonationDomain,string strCalledFrom)
		{
			intClientId = iClientID;
			intPolicyId = iPolicyID;
			intPolicyVersion = iPolicyVersion;
			strLobCode = sLobCode;
			strInputXml = sInputXml;
			strInputPath = sInputPath;
			strOutputPath = sOutputPath;
			strImpersonationUserId = sImpersonationUserId;
			strImpersonationPassword = sImpersonationPassword;
			strImpersonationDomain = sImpersonationDomain;
			try
			{
				lObjImpersionate= new ImpersonateWrapper();
				try
				{
					lObjImpersionate.ImpersonateUser(strImpersonationUserId, strImpersonationPassword, strImpersonationDomain);
					
					#region Create folder for tmpPDFs
					string strFileName = "";
					strTmpPDfPath = strOutputPath + "\\" + "tmp";
					SetTmpPDFPath();//Create folder for tmpPDFs if not exists
					#endregion

					PrintPdfInformation();//Parse XML and generate pdf	

					#region Generate final PDF
					APToolkitNET.Toolkit finalPdf = new Toolkit();
					if(strCalledFrom.ToUpper().Equals("APPLICATION"))
						finalPdf.SetViewerPreferences(true,true,true,true,true); 
					else
						finalPdf.SetViewerPreferences(true,true,true,true,true); 

					strFileName = strLobCode + "_" + intClientId.ToString() + "_" + intPolicyId.ToString()  + "_" + intPolicyVersion.ToString() +  "_" + finalPdf.GetUniqueFileName();
					finalPdf.OpenOutputFile(strOutputPath + "\\" + strFileName );
					FileInfo fileObj ;

					for(int i =0 ;i<arrFileInfo.Count;i++)
					{
						finalPdf.OpenInputFile(strTmpPDfPath + "\\" + arrFileInfo[i].ToString());
						finalPdf.CopyForm(0,0);
						finalPdf.CloseInputFile();
						fileObj = new FileInfo(strTmpPDfPath + "\\" + arrFileInfo[i].ToString());
						fileObj.Delete();
					}
					finalPdf.CloseOutputFile();
					#endregion
					
					lObjImpersionate.endImpersonation();

					return strFileName;
				}
				catch(Exception e)
				{
					throw e;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
		
		private void PrintPdfInformation()
		{
			try
			{			
				#region Variable Declaration
				string strPrimePdfName="",strfilename="",strFieldType="",strIsImage="";
				APToolkitNET.Toolkit PrimePdfMain;
				#endregion

				#region Fetching XML
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				//doc.Load(Server.MapPath("Boat-PDF.XML"));
				doc.LoadXml(strInputXml);
				#endregion

				
				
				foreach(System.Xml.XmlNode RootNode in doc.SelectNodes("//ACORD"))
				{
					#region Creating Main Pdf File
					PrimePdfMain = new APToolkitNET.Toolkit();
					strPrimePdfName = getAttributeValue(RootNode,PRIMARYPDF);
					PrimePdfMain.OpenInputFile(strInputPath +"\\"+ strPrimePdfName);
					strfilename = PrimePdfMain.GetUniqueFileName();
					PrimePdfMain.OpenOutputFile(strTmpPDfPath +"\\"+ strfilename);
					arrFileInfo.Add(strfilename);
					#endregion

					#region Parsing XML
					foreach (System.Xml.XmlNode Node in RootNode.ChildNodes)
					{
						strFieldType = getAttributeValue(Node,FIELDTYPE);
						strIsImage = getAttributeValue(Node,ISIMAGE);

						switch(strFieldType)
						{
							case FIELDTYPES://Data to write on the main PDF File
								printPrimaryPdfValue(Node,PrimePdfMain,strIsImage);
								break;
							case FIELDTYPEM://Data to write on the main and extension PDF File
								string strSecondaryPdf="",strSubPrimaryPdf="";
								int intPrimaryPdfBlock=0, intSecondaryPdfBlock=0;
								strSecondaryPdf = getAttributeValue(Node,SECONDARYPDF);
								strSubPrimaryPdf = getAttributeValue(Node,PRIMARYPDF);
								intPrimaryPdfBlock = Convert.ToInt32(getAttributeValue(Node,PRIMARYPDFBLOCK));
								intSecondaryPdfBlock = Convert.ToInt32(getAttributeValue(Node,SECONDARYPDFBLOCK));
								printSecondaryPdfValue(Node.ChildNodes,PrimePdfMain,strSecondaryPdf,intPrimaryPdfBlock,intSecondaryPdfBlock,strSubPrimaryPdf);
								break;
						}
					}
					#region Closing Main Pdf File
					PrimePdfMain.CopyForm(0,0);
					PrimePdfMain.CloseOutputFile();
					PrimePdfMain.CloseInputFile();
					#endregion
				}
				#endregion

				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
  					
		}

		private void printPrimaryPdfValue(System.Xml.XmlNode node,APToolkitNET.Toolkit PrimePdf,string strImageTyp)
		{
			string strIsImage="",strfloatX = "",strfloatY = "",strfloatW = "",strfloatH = "",strpageNo = "",strimgfile = "";
			



			foreach (System.Xml.XmlNode Subnode in node)
			{
				strIsImage = getAttributeValue(Subnode,ISIMAGE);
				strfloatX = getAttributeValue(Subnode,FLOATX);
				strfloatY = getAttributeValue(Subnode,FLOATY);
				strfloatW = getAttributeValue(Subnode,FLOATW);
				strfloatH = getAttributeValue(Subnode,FLOATH);
				strpageNo = getAttributeValue(Subnode,PAGENO);
				strimgfile = getAttributeValue(Subnode,IMAGEPATH);


				if(strIsImage.Equals("Y") )
				{
					if(strimgfile == "")
					{
						//						PrimePdf.PrintImage(strInputPath  +  "G B Laing.jpg",float.Parse(strfloatX),float.Parse(strfloatY),float.Parse(strfloatW),float.Parse(strfloatH),true,0); 
						//						PrimePdf.AddLogo(strInputPath  +  "G B Laing.jpg",1);
					}
					else
					{
						PrimePdf.PrintImage(strInputPath  +  strimgfile,float.Parse(strfloatX),float.Parse(strfloatY),float.Parse(strfloatW),float.Parse(strfloatH),true,Convert.ToInt32(strpageNo)); 
						PrimePdf.AddLogo(strInputPath  +  strimgfile,1);
					}

				}
				else
					PrimePdf.SetFormFieldData(Subnode.Name,RemoveXmlEscapeSequence(Subnode.InnerText.ToString()),0);
			}

		}

		private void printSecondaryPdfValue(System.Xml.XmlNodeList nodes,APToolkitNET.Toolkit PrimaryPdf, string strSecondaryPdf, int intPrimaryPdfBlock, int intSecondaryPdfBlock, string subPrimaryPdf)
		{	
			#region Variable Declaraction
			string strfileName = "",strFieldType="",strSubSecondaryPdf="",strSubPrimary="",strIsImage="",strfloatX = "",strfloatY = "",strfloatW = "",strfloatH = "",strpageNo = "",strimgfile = "";
			int intSubPrimaryPdfBlock=0, intSubSecondaryPdfBlock=0;
			int intFieldId=0,intPrimaryCountId=0,intSheetctr = 0,intActualFieldId = 0;
			APToolkitNET.Toolkit SecondaryPdfMain = new APToolkitNET.Toolkit();
			APToolkitNET.Toolkit ActPrimaryPdf;
			#endregion

			#region Parsing Xml

			if (subPrimaryPdf.Trim()=="")
			{
				ActPrimaryPdf = PrimaryPdf;
			}
			else 
			{
				ActPrimaryPdf = new APToolkitNET.Toolkit();
				ActPrimaryPdf.OpenInputFile(strInputPath +"\\"+subPrimaryPdf);
				strfileName = ActPrimaryPdf.GetUniqueFileName();
				ActPrimaryPdf.OpenOutputFile(strTmpPDfPath +"\\"+strfileName);
				arrFileInfo.Add(strfileName);

			}

			foreach (System.Xml.XmlNode Subnode in nodes)
			{
				intFieldId = Convert.ToInt32(getAttributeValue(Subnode,NODEID));

				#region creating extension PDF File
				if (intPrimaryCountId == intPrimaryPdfBlock && intPrimaryCountId == intFieldId)
				{
					if(strSecondaryPdf.Trim()=="")
					{
						return;
					}
					SecondaryPdfMain.OpenInputFile(strInputPath +"\\"+ strSecondaryPdf);
					strfileName = SecondaryPdfMain.GetUniqueFileName();
					SecondaryPdfMain.OpenOutputFile(strTmpPDfPath +"\\"+strfileName);
					arrFileInfo.Add(strfileName);
				}
				#endregion

				foreach (System.Xml.XmlNode childNode in Subnode.ChildNodes)
				{
					#region closing and recreating extension PDF File
					if ((intPrimaryCountId - intPrimaryPdfBlock) >= intSecondaryPdfBlock)
					{
						//intPrimaryCountId = intPrimaryPdfBlock - 1;
						intPrimaryCountId = intPrimaryPdfBlock;
						intSheetctr++ ;
						SecondaryPdfMain.CopyForm(0,0);
						SecondaryPdfMain.CloseOutputFile();
						SecondaryPdfMain.CloseInputFile();
						SecondaryPdfMain.OpenInputFile(strInputPath +"\\"+ strSecondaryPdf);
						strfileName = SecondaryPdfMain.GetUniqueFileName();
						SecondaryPdfMain.OpenOutputFile(strTmpPDfPath +"\\"+ strfileName);
						arrFileInfo.Add(strfileName);
					}
					#endregion
					
					strFieldType = getAttributeValue(childNode,FIELDTYPE);
					strIsImage = getAttributeValue(childNode,ISIMAGE);
					strfloatX = getAttributeValue(childNode,FLOATX);
					strfloatY = getAttributeValue(childNode,FLOATY);
					strfloatW = getAttributeValue(childNode,FLOATW);
					strfloatH = getAttributeValue(childNode,FLOATH);
					strpageNo = getAttributeValue(childNode,PAGENO);
					strimgfile = getAttributeValue(childNode,IMAGEPATH);

					switch(strFieldType)
					{
						case FIELDTYPES:

							#region calling printPrimaryPdfValue function
							printPrimaryPdfValue(childNode,ActPrimaryPdf,strIsImage);
							break;
							#endregion

						case FIELDTYPEM:

							#region getting extension PDF Parameter
							strSubSecondaryPdf = getAttributeValue(childNode,SECONDARYPDF);
							intSubPrimaryPdfBlock = Convert.ToInt32(getAttributeValue(childNode,PRIMARYPDFBLOCK));
							intSubSecondaryPdfBlock = Convert.ToInt32(getAttributeValue(childNode,SECONDARYPDFBLOCK));
							strSubPrimary = getAttributeValue(childNode,PRIMARYPDF);
							#endregion

							#region calling printSecondaryPdfValue function
							if (childNode.ChildNodes.Count > 0)
							{
								if (intFieldId < intPrimaryPdfBlock)
								{
									printSecondaryPdfValue(childNode.ChildNodes,ActPrimaryPdf,strSubSecondaryPdf,intSubPrimaryPdfBlock,intSubSecondaryPdfBlock,strSubPrimary);
								}
								else 
								{
									printSecondaryPdfValue(childNode.ChildNodes,SecondaryPdfMain,strSubSecondaryPdf,intSubPrimaryPdfBlock,intSubSecondaryPdfBlock,strSubPrimary);
								}
							}
							break;
							#endregion

						default:
							#region printing xml data to Pdf
							if (intFieldId < intPrimaryPdfBlock)
							{
								if(strFieldType.Equals(FIELDTYPEI))
								{
									if(strimgfile == "")
									{
										//	ActPrimaryPdf.PrintImage(strInputPath  +  "G B Laing.jpg",391,48,203,22,true,0); 
									}
									else
									{
										ActPrimaryPdf.PrintImage(strInputPath  +  strimgfile,float.Parse(strfloatX),float.Parse(strfloatY),float.Parse(strfloatW),float.Parse(strfloatH),true,Convert.ToInt32(strpageNo)); 
										ActPrimaryPdf.AddLogo(strInputPath  +  strimgfile,1);
									}
								}
								else
									ActPrimaryPdf.SetFormFieldData(childNode.Name + '_'+ intFieldId.ToString(),RemoveXmlEscapeSequence(childNode.InnerText.ToString()),0);
							}
							else 
							{
								if(strFieldType.Equals(FIELDTYPEI))
								{
									if(strimgfile == "")
									{
										//										SecondaryPdfMain.PrintImage(strInputPath  +  "G B Laing.jpg",391,48,203,22,true,0); 
									}
									else
									{
										SecondaryPdfMain.PrintImage(strInputPath  +  strimgfile,float.Parse(strfloatX),float.Parse(strfloatY),float.Parse(strfloatW),float.Parse(strfloatH),true,Convert.ToInt32(strpageNo)); 
										SecondaryPdfMain.AddLogo(strInputPath  +  strimgfile,1);
									}
								}
								else
								{
									intActualFieldId = ((intFieldId -(intPrimaryPdfBlock + (intSecondaryPdfBlock*intSheetctr))));
									SecondaryPdfMain.SetFormFieldData(childNode.Name + '_'+ intActualFieldId.ToString() ,RemoveXmlEscapeSequence(childNode.InnerText.ToString()),0);
								}
							}
							break;
							#endregion

					}//switch
					
				}//For each inner
				
				
				intPrimaryCountId++;
								
			}//For each

			#region Closing extension Pdf File
			SecondaryPdfMain.CopyForm(0,0);
			SecondaryPdfMain.CloseOutputFile();
			SecondaryPdfMain.CloseInputFile();

			if (subPrimaryPdf.Trim()!="")
			{
				ActPrimaryPdf.CopyForm(0,0);
				ActPrimaryPdf.CloseOutputFile();
				ActPrimaryPdf.CloseInputFile();
			}
			

			#endregion

			#endregion
		}

		private string getAttributeValue(System.Xml.XmlNode node,string strAttName)
		{
			foreach(XmlAttribute attri in node.Attributes)
			{
				if(attri.Name.ToUpper() == strAttName.ToUpper())
				{
					return attri.Value;
				}
			}
			return "";
		}

		private void SetTmpPDFPath()
		{
			try
			{
				DirectoryInfo DirInfo = new DirectoryInfo(strTmpPDfPath);

				if(DirInfo.Exists==false)
				{
					DirInfo.Create();
				}
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
		private string RemoveXmlEscapeSequence(string NodeText)
		{
			NodeText = NodeText.Replace("&lt;","<");
			NodeText = NodeText.Replace("&gt;",">");
			return(NodeText);
		}		
		
	}

	public class ImpersonateWrapper
	{
		// Declare the logon types as constants
		const int LOGON32_LOGON_INTERACTIVE = 2;
		const int LOGON32_LOGON_NETWORK = 3;

		// Declare the logon providers as constants
		const int LOGON32_PROVIDER_DEFAULT = 0;
		const int LOGON32_PROVIDER_WINNT50 = 3;
		const int LOGON32_PROVIDER_WINNT40 = 2;
		const int LOGON32_PROVIDER_WINNT35 = 1;

		WindowsImpersonationContext impersonationContext; 

		[DllImport("advapi32.dll", CharSet=CharSet.Auto)]
		public static extern int LogonUser(String lpszUserName,	String lpszDomain,String lpszPassword,int dwLogonType, int dwLogonProvider,ref IntPtr phToken);
		[DllImport("advapi32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto, SetLastError=true)]
		public extern static int DuplicateToken(IntPtr hToken, 	int impersonationLevel,  ref IntPtr hNewToken);

		public ImpersonateWrapper()
		{
				 
		}

		public bool ImpersonateUser(String userName,  String password,String domainName)
		{
			WindowsIdentity tempWindowsIdentity;
			IntPtr token = IntPtr.Zero;
			IntPtr tokenDuplicate = IntPtr.Zero;
			bool authentication = false;

			try
			{
				if(LogonUser(userName, domainName, password, LOGON32_LOGON_INTERACTIVE,	LOGON32_PROVIDER_DEFAULT, ref token) != 0)
				{
					if(DuplicateToken(token, 2, ref tokenDuplicate) != 0) 
					{
						tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
						impersonationContext = tempWindowsIdentity.Impersonate();
						if (impersonationContext != null)
							authentication =  true;
						else
							authentication = false; 
					}
					else
						authentication =  false;
				} 
				else
					authentication = false;
			}
			catch(Exception ex)
			{
			}
			return authentication ;
		}

		public void endImpersonation()
		{
			try
			{
				if (impersonationContext !=null) impersonationContext.Undo();
			}
			catch (Exception ex)
			{
				System.Diagnostics.EventLog.WriteEntry("EbixASP WebMerge 3.0","Impersionation Error; Message:-" + ex.Message);	
			}
		} */
	}

//	public class AcordXMLParserWebService : System.Web.Services.WebService
//	{
//
//		#region Private class member
//		private string strOutputPath;
//		private string strInputPath;
//		private string strTmpPDfPath;
//		private string strInputXml;
//		private string strLobCode;
//		private int intClientId;
//		private int intPolicyId;
//		private int intPolicyVersion;
//		private string strImpersonationUserId;
//		private string strImpersonationPassword;
//		private string strImpersonationDomain;
//		private ArrayList arrFileInfo = new ArrayList();
//		ImpersonateWrapper lObjImpersionate;
//		#endregion
//
//		#region Constant Variable
//		const string ISIMAGE = "ISIMAGE";
//		const string FLOATX = "FLOATX";
//		const string FLOATY = "FLOATY";
//		const string FLOATW = "FLOATW";
//		const string FLOATH = "FLOATH";
//		const string PAGENO = "PAGENO";
//		const string IMAGEPATH = "IMAGEPATH";
//
//
//
//		const string FIELDTYPES = "S";
//		const string FIELDTYPEM = "M";
//		const string FIELDTYPET = "T";
//		const string FIELDTYPEI = "I";
//		const string FIELDTYPEC = "C";
//		const string FIELDTYPEN = "N";
//		const string FIELDTYPE  = "fieldType";
//		const string NODEID  = "id";
//		const string PRIMARYPDF = "PrimPDF";
//		const string SECONDARYPDF = "SecondPDF";
//		const string PRIMARYPDFBLOCK = "PrimPDFBlocks";
//		const string SECONDARYPDFBLOCK = "SecondPDFBlocks";
//		#endregion
//
//		APToolkitNET.Toolkit PrimePdfMain = new APToolkitNET.Toolkit();
//		
//
//		#region Public properties
//		public string OutputPath
//		{
//			set
//			{
//				strOutputPath =	value;
//			}
//			get
//			{
//				return strOutputPath;
//			}
//		}
//		
//		public string InputPath
//		{
//			set
//			{
//				strInputPath = value;
//			}
//			get
//			{
//				return strInputPath;
//			}
//		}
//
//		public string InputXml
//		{
//			set
//			{
//				strInputXml = value;
//			}
//		}
//
//		public string LobCode
//		{
//			set
//			{
//				strLobCode = value;
//			}
//			get
//			{
//				return strLobCode;
//			}
//		}
//		
//		public int ClientId
//		{
//			set
//			{
//				intClientId = value;
//			}
//			get
//			{
//				return intClientId;
//			}
//		}
//		
//		public int PolicyId
//		{
//			set
//			{
//				intPolicyId = value;
//			}
//			get
//			{
//				return intPolicyId;
//			}
//		}
//
//		public int PolicyVersion
//		{
//			set
//			{
//				intPolicyVersion = value;
//			}
//			get
//			{
//				return intPolicyVersion;
//			}
//		}
//		public string ImpersonationUserId
//		{
//			set
//			{
//				strImpersonationUserId = value;
//			}
//			get
//			{
//				return strImpersonationUserId;
//			}
//		}
//		public string ImpersonationPassword
//		{
//			set
//			{
//				strImpersonationPassword = value;
//			}
//			get
//			{
//				return strImpersonationPassword;
//			}
//		}
//		public string ImpersonationDomain
//		{
//			set
//			{
//				strImpersonationDomain = value;
//			}
//			get
//			{
//				return strImpersonationDomain;
//			}
//		}
//		#endregion
//
//		public AcordXMLParserWebService()
//		{
//			//CODEGEN: This call is required by the ASP.NET Web Services Designer
//			InitializeComponent();
//		}
//
//		#region Component Designer generated code
//		
//		//Required by the Web Services Designer 
//		private IContainer components = null;
//				
//		/// <summary>
//		/// Required method for Designer support - do not modify
//		/// the contents of this method with the code editor.
//		/// </summary>
//		private void InitializeComponent()
//		{
//		}
//
//		/// <summary>
//		/// Clean up any resources being used.
//		/// </summary>
//		protected override void Dispose( bool disposing )
//		{
//			if(disposing && components != null)
//			{
//				components.Dispose();
//			}
//			base.Dispose(disposing);		
//		}
//		
//		#endregion
//		
//		[WebMethod]
//		public string GeneratePDF(string strCalledFrom)
//		{
//			try
//			{
//				lObjImpersionate= new ImpersonateWrapper();
//				try
//				{
//					lObjImpersionate.ImpersonateUser(strImpersonationUserId, strImpersonationPassword, strImpersonationDomain);
//					
//					#region Create folder for tmpPDFs
//					string strFileName = "";
//					strTmpPDfPath = strOutputPath + "\\" + "tmp";
//					SetTmpPDFPath();//Create folder for tmpPDFs if not exists
//					#endregion
//
//					PrintPdfInformation();//Parse XML and generate pdf	
//
//					#region Generate final PDF
//					APToolkitNET.Toolkit finalPdf = new Toolkit();
//					if(strCalledFrom.ToUpper().Equals("APPLICATION"))
//						finalPdf.SetViewerPreferences(true,true,true,true,true); 
//					else
//						finalPdf.SetViewerPreferences(true,true,true,true,true); 
//
//					strFileName = strLobCode + "_" + intClientId.ToString() + "_" + intPolicyId.ToString()  + "_" + intPolicyVersion.ToString() +  "_" + finalPdf.GetUniqueFileName();
//					finalPdf.OpenOutputFile(strOutputPath + "\\" + strFileName );
//					FileInfo fileObj ;
//
//					for(int i =0 ;i<arrFileInfo.Count;i++)
//					{
//						finalPdf.OpenInputFile(strTmpPDfPath + "\\" + arrFileInfo[i].ToString());
//						finalPdf.CopyForm(0,0);
//						finalPdf.CloseInputFile();
//						fileObj = new FileInfo(strTmpPDfPath + "\\" + arrFileInfo[i].ToString());
//						fileObj.Delete();
//					}
//					finalPdf.CloseOutputFile();
//					#endregion
//					
//					lObjImpersionate.endImpersonation();
//
//					return strFileName;
//				}
//				catch(Exception e)
//				{
//					throw e;
//				}
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//			finally
//			{
//			}
//		}
//		
//		private void PrintPdfInformation()
//		{
//			try
//			{			
//				#region Variable Declaration
//				string strPrimePdfName="",strfilename="",strFieldType="",strIsImage="";
//				APToolkitNET.Toolkit PrimePdfMain;
//				#endregion
//
//				#region Fetching XML
//				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
//				//doc.Load(Server.MapPath("Boat-PDF.XML"));
//				doc.LoadXml(strInputXml);
//				#endregion
//
//				
//				
//				foreach(System.Xml.XmlNode RootNode in doc.SelectNodes("//ACORD"))
//				{
//					#region Creating Main Pdf File
//					PrimePdfMain = new APToolkitNET.Toolkit();
//					strPrimePdfName = getAttributeValue(RootNode,PRIMARYPDF);
//					PrimePdfMain.OpenInputFile(strInputPath +"\\"+ strPrimePdfName);
//					strfilename = PrimePdfMain.GetUniqueFileName();
//					PrimePdfMain.OpenOutputFile(strTmpPDfPath +"\\"+ strfilename);
//					arrFileInfo.Add(strfilename);
//					#endregion
//
//					#region Parsing XML
//					foreach (System.Xml.XmlNode Node in RootNode.ChildNodes)
//					{
//						strFieldType = getAttributeValue(Node,FIELDTYPE);
//						strIsImage = getAttributeValue(Node,ISIMAGE);
//
//						switch(strFieldType)
//						{
//							case FIELDTYPES://Data to write on the main PDF File
//								printPrimaryPdfValue(Node,PrimePdfMain,strIsImage);
//								break;
//							case FIELDTYPEM://Data to write on the main and extension PDF File
//								string strSecondaryPdf="",strSubPrimaryPdf="";
//								int intPrimaryPdfBlock=0, intSecondaryPdfBlock=0;
//								strSecondaryPdf = getAttributeValue(Node,SECONDARYPDF);
//								strSubPrimaryPdf = getAttributeValue(Node,PRIMARYPDF);
//								intPrimaryPdfBlock = Convert.ToInt32(getAttributeValue(Node,PRIMARYPDFBLOCK));
//								intSecondaryPdfBlock = Convert.ToInt32(getAttributeValue(Node,SECONDARYPDFBLOCK));
//								printSecondaryPdfValue(Node.ChildNodes,PrimePdfMain,strSecondaryPdf,intPrimaryPdfBlock,intSecondaryPdfBlock,strSubPrimaryPdf);
//								break;
//						}
//					}
//					#region Closing Main Pdf File
//					PrimePdfMain.CopyForm(0,0);
//					PrimePdfMain.CloseOutputFile();
//					PrimePdfMain.CloseInputFile();
//					#endregion
//				}
//				#endregion
//
//				
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//			finally
//			{
//			}
//  					
//		}
//
//		private void printPrimaryPdfValue(System.Xml.XmlNode node,APToolkitNET.Toolkit PrimePdf,string strImageTyp)
//		{
//			string strIsImage="",strfloatX = "",strfloatY = "",strfloatW = "",strfloatH = "",strpageNo = "",strimgfile = "";
//			
//
//
//
//			foreach (System.Xml.XmlNode Subnode in node)
//			{
//				strIsImage = getAttributeValue(Subnode,ISIMAGE);
//				strfloatX = getAttributeValue(Subnode,FLOATX);
//				strfloatY = getAttributeValue(Subnode,FLOATY);
//				strfloatW = getAttributeValue(Subnode,FLOATW);
//				strfloatH = getAttributeValue(Subnode,FLOATH);
//				strpageNo = getAttributeValue(Subnode,PAGENO);
//				strimgfile = getAttributeValue(Subnode,IMAGEPATH);
//
//
//				if(strIsImage.Equals("Y") )
//				{
//					if(strimgfile == "")
//					{
//						//						PrimePdf.PrintImage(strInputPath  +  "G B Laing.jpg",float.Parse(strfloatX),float.Parse(strfloatY),float.Parse(strfloatW),float.Parse(strfloatH),true,0); 
//						//						PrimePdf.AddLogo(strInputPath  +  "G B Laing.jpg",1);
//					}
//					else
//					{
//						PrimePdf.PrintImage(strInputPath  +  strimgfile,float.Parse(strfloatX),float.Parse(strfloatY),float.Parse(strfloatW),float.Parse(strfloatH),true,Convert.ToInt32(strpageNo)); 
//						PrimePdf.AddLogo(strInputPath  +  strimgfile,1);
//					}
//
//				}
//				else
//					PrimePdf.SetFormFieldData(Subnode.Name,RemoveXmlEscapeSequence(Subnode.InnerText.ToString()),0);
//			}
//
//		}
//
//		private void printSecondaryPdfValue(System.Xml.XmlNodeList nodes,APToolkitNET.Toolkit PrimaryPdf, string strSecondaryPdf, int intPrimaryPdfBlock, int intSecondaryPdfBlock, string subPrimaryPdf)
//		{	
//			#region Variable Declaraction
//			string strfileName = "",strFieldType="",strSubSecondaryPdf="",strSubPrimary="",strIsImage="",strfloatX = "",strfloatY = "",strfloatW = "",strfloatH = "",strpageNo = "",strimgfile = "";
//			int intSubPrimaryPdfBlock=0, intSubSecondaryPdfBlock=0;
//			int intFieldId=0,intPrimaryCountId=0,intSheetctr = 0,intActualFieldId = 0;
//			APToolkitNET.Toolkit SecondaryPdfMain = new APToolkitNET.Toolkit();
//			APToolkitNET.Toolkit ActPrimaryPdf;
//			#endregion
//
//			#region Parsing Xml
//
//			if (subPrimaryPdf.Trim()=="")
//			{
//				ActPrimaryPdf = PrimaryPdf;
//			}
//			else 
//			{
//				ActPrimaryPdf = new APToolkitNET.Toolkit();
//				ActPrimaryPdf.OpenInputFile(strInputPath +"\\"+subPrimaryPdf);
//				strfileName = ActPrimaryPdf.GetUniqueFileName();
//				ActPrimaryPdf.OpenOutputFile(strTmpPDfPath +"\\"+strfileName);
//				arrFileInfo.Add(strfileName);
//
//			}
//
//			foreach (System.Xml.XmlNode Subnode in nodes)
//			{
//				intFieldId = Convert.ToInt32(getAttributeValue(Subnode,NODEID));
//
//				#region creating extension PDF File
//				if (intPrimaryCountId == intPrimaryPdfBlock && intPrimaryCountId == intFieldId)
//				{
//					if(strSecondaryPdf.Trim()=="")
//					{
//						return;
//					}
//					SecondaryPdfMain.OpenInputFile(strInputPath +"\\"+ strSecondaryPdf);
//					strfileName = SecondaryPdfMain.GetUniqueFileName();
//					SecondaryPdfMain.OpenOutputFile(strTmpPDfPath +"\\"+strfileName);
//					arrFileInfo.Add(strfileName);
//				}
//				#endregion
//
//				foreach (System.Xml.XmlNode childNode in Subnode.ChildNodes)
//				{
//					#region closing and recreating extension PDF File
//					if ((intPrimaryCountId - intPrimaryPdfBlock) >= intSecondaryPdfBlock)
//					{
//						//intPrimaryCountId = intPrimaryPdfBlock - 1;
//						intPrimaryCountId = intPrimaryPdfBlock;
//						intSheetctr++ ;
//						SecondaryPdfMain.CopyForm(0,0);
//						SecondaryPdfMain.CloseOutputFile();
//						SecondaryPdfMain.CloseInputFile();
//						SecondaryPdfMain.OpenInputFile(strInputPath +"\\"+ strSecondaryPdf);
//						strfileName = SecondaryPdfMain.GetUniqueFileName();
//						SecondaryPdfMain.OpenOutputFile(strTmpPDfPath +"\\"+ strfileName);
//						arrFileInfo.Add(strfileName);
//					}
//					#endregion
//					
//					strFieldType = getAttributeValue(childNode,FIELDTYPE);
//					strIsImage = getAttributeValue(childNode,ISIMAGE);
//					strfloatX = getAttributeValue(childNode,FLOATX);
//					strfloatY = getAttributeValue(childNode,FLOATY);
//					strfloatW = getAttributeValue(childNode,FLOATW);
//					strfloatH = getAttributeValue(childNode,FLOATH);
//					strpageNo = getAttributeValue(childNode,PAGENO);
//					strimgfile = getAttributeValue(childNode,IMAGEPATH);
//
//					switch(strFieldType)
//					{
//						case FIELDTYPES:
//
//							#region calling printPrimaryPdfValue function
//							printPrimaryPdfValue(childNode,ActPrimaryPdf,strIsImage);
//							break;
//							#endregion
//
//						case FIELDTYPEM:
//
//							#region getting extension PDF Parameter
//							strSubSecondaryPdf = getAttributeValue(childNode,SECONDARYPDF);
//							intSubPrimaryPdfBlock = Convert.ToInt32(getAttributeValue(childNode,PRIMARYPDFBLOCK));
//							intSubSecondaryPdfBlock = Convert.ToInt32(getAttributeValue(childNode,SECONDARYPDFBLOCK));
//							strSubPrimary = getAttributeValue(childNode,PRIMARYPDF);
//							#endregion
//
//							#region calling printSecondaryPdfValue function
//							if (childNode.ChildNodes.Count > 0)
//							{
//								if (intFieldId < intPrimaryPdfBlock)
//								{
//									printSecondaryPdfValue(childNode.ChildNodes,ActPrimaryPdf,strSubSecondaryPdf,intSubPrimaryPdfBlock,intSubSecondaryPdfBlock,strSubPrimary);
//								}
//								else 
//								{
//									printSecondaryPdfValue(childNode.ChildNodes,SecondaryPdfMain,strSubSecondaryPdf,intSubPrimaryPdfBlock,intSubSecondaryPdfBlock,strSubPrimary);
//								}
//							}
//							break;
//							#endregion
//
//						default:
//							#region printing xml data to Pdf
//							if (intFieldId < intPrimaryPdfBlock)
//							{
//								if(strFieldType.Equals(FIELDTYPEI))
//								{
//									if(strimgfile == "")
//									{
//										//	ActPrimaryPdf.PrintImage(strInputPath  +  "G B Laing.jpg",391,48,203,22,true,0); 
//									}
//									else
//									{
//										ActPrimaryPdf.PrintImage(strInputPath  +  strimgfile,float.Parse(strfloatX),float.Parse(strfloatY),float.Parse(strfloatW),float.Parse(strfloatH),true,Convert.ToInt32(strpageNo)); 
//										ActPrimaryPdf.AddLogo(strInputPath  +  strimgfile,1);
//									}
//								}
//								else
//									ActPrimaryPdf.SetFormFieldData(childNode.Name + '_'+ intFieldId.ToString(),RemoveXmlEscapeSequence(childNode.InnerText.ToString()),0);
//							}
//							else 
//							{
//								if(strFieldType.Equals(FIELDTYPEI))
//								{
//									if(strimgfile == "")
//									{
//										//										SecondaryPdfMain.PrintImage(strInputPath  +  "G B Laing.jpg",391,48,203,22,true,0); 
//									}
//									else
//									{
//										SecondaryPdfMain.PrintImage(strInputPath  +  strimgfile,float.Parse(strfloatX),float.Parse(strfloatY),float.Parse(strfloatW),float.Parse(strfloatH),true,Convert.ToInt32(strpageNo)); 
//										SecondaryPdfMain.AddLogo(strInputPath  +  strimgfile,1);
//									}
//								}
//								else
//								{
//									intActualFieldId = ((intFieldId -(intPrimaryPdfBlock + (intSecondaryPdfBlock*intSheetctr))));
//									SecondaryPdfMain.SetFormFieldData(childNode.Name + '_'+ intActualFieldId.ToString() ,RemoveXmlEscapeSequence(childNode.InnerText.ToString()),0);
//								}
//							}
//							break;
//							#endregion
//
//					}//switch
//					
//				}//For each inner
//				
//				
//				intPrimaryCountId++;
//								
//			}//For each
//
//			#region Closing extension Pdf File
//			SecondaryPdfMain.CopyForm(0,0);
//			SecondaryPdfMain.CloseOutputFile();
//			SecondaryPdfMain.CloseInputFile();
//
//			if (subPrimaryPdf.Trim()!="")
//			{
//				ActPrimaryPdf.CopyForm(0,0);
//				ActPrimaryPdf.CloseOutputFile();
//				ActPrimaryPdf.CloseInputFile();
//			}
//			
//
//			#endregion
//
//			#endregion
//		}
//
//		private string getAttributeValue(System.Xml.XmlNode node,string strAttName)
//		{
//			foreach(XmlAttribute attri in node.Attributes)
//			{
//				if(attri.Name.ToUpper() == strAttName.ToUpper())
//				{
//					return attri.Value;
//				}
//			}
//			return "";
//		}
//
//		private void SetTmpPDFPath()
//		{
//			try
//			{
//				DirectoryInfo DirInfo = new DirectoryInfo(strTmpPDfPath);
//
//				if(DirInfo.Exists==false)
//				{
//					DirInfo.Create();
//				}
//				
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//			finally
//			{
//			}
//		}
//		private string RemoveXmlEscapeSequence(string NodeText)
//		{
//			NodeText = NodeText.Replace("&lt;","<");
//			NodeText = NodeText.Replace("&gt;",">");
//			return(NodeText);
//		}
//	}
}
