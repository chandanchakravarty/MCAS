using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Mohit Agarwal</CreatedBy>
	/// <Dated>05-Jan-2007</Dated>
	/// <Purpose>To Create XML for ADDL INT PDF for HOME LOB</Purpose>
	/// </summary>
	public class ClsAddIntPdfXML : ClsCommonPdf
	{
		#region Declarations
		private XmlElement DecPageRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private DataWrapper gobjWrapper;
		private string stCode="";
		private string gStrLobCode="";
		private int gaddlintindex=0;
		private   DataSet DSTempPolicyDataSet= new DataSet() ;		
		private   DataSet DSTempApplicantDataSet = new DataSet();
		private   DataSet DstempDocument = new DataSet();
		private   DataSet DSAddWordSet = new DataSet();
		private   DataSet DSTempAutoDetailDataSet = new DataSet();
		private   DataSet DSTempAddIntrst = new DataSet();
		#endregion

		#region Constructor
		public ClsAddIntPdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string lStrLobCode,string stateCode, string strProcessID, int addlintindex, DataWrapper objWrapper, DataSet lDSPolicyDataSet, DataSet lDSTempApplicantDataSet, DataSet lDSTempAutoDetailDataSet, DataSet lDSAddWordSet, DataSet lDstempDocument,DataSet lDSTempAddIntrst)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			gStrLobCode=lStrLobCode.ToUpper().Trim();
			gStrProcessID = strProcessID;
			gaddlintindex=addlintindex;
			stCode=stateCode;
			DSTempPolicyDataSet = lDSPolicyDataSet.Copy();
			DSTempApplicantDataSet = lDSTempApplicantDataSet.Copy();
			DstempDocument = lDstempDocument.Copy();
			DSAddWordSet = lDSAddWordSet.Copy();
			DSTempAutoDetailDataSet = lDSTempAutoDetailDataSet.Copy();
			DSTempAddIntrst = lDSTempAddIntrst;
			DSTempDataSet = new DataSet();
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);	
			this.gobjWrapper = objWrapper;
			gobjWrapper.ClearParameteres();
//			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
//			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
//			gobjWrapper.ClearParameteres();
			//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempPolicyDataSet.Tables[0].Rows.Count>0)
			{
				if(gStrLobCode == (((int)(LOBType.HOME)).ToString()))
					SetPDFVersionLobNode("HOME",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				else if(gStrLobCode == ((int)(LOBType.RENT)).ToString())
					SetPDFVersionLobNode("REDW",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				if(gStrLobCode == (((int)(LOBType.MOT)).ToString()))
					SetPDFVersionLobNode("MOT",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				else if(gStrLobCode == ((int)(LOBType.PPA)).ToString())
					SetPDFVersionLobNode("AUTO",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				else if(gStrLobCode == ((int)(LOBType.WAT)).ToString())
					SetPDFVersionLobNode("BOAT",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
			}
		}
		public ClsAddIntPdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string lStrLobCode,string stateCode, string strProcessID, int addlintindex, DataWrapper objWrapper)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			gStrLobCode=lStrLobCode.ToUpper().Trim();
			gStrProcessID = strProcessID;
			gaddlintindex=addlintindex;
			stCode=stateCode;
			DSTempDataSet = new DataSet();
			if(objWrapper==null)
				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);	
			this.gobjWrapper = objWrapper;
			gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
						gobjWrapper.ClearParameteres();
			DSTempPolicyDataSet = DSTempDataSet.Copy();
			ConvertLobCode();
			//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempPolicyDataSet.Tables[0].Rows.Count>0)
			{
				if(gStrLobCode == (((int)(LOBType.HOME)).ToString()))
					SetPDFVersionLobNode("HOME",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				else if(gStrLobCode == ((int)(LOBType.RENT)).ToString())
					SetPDFVersionLobNode("REDW",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				if(gStrLobCode == (((int)(LOBType.MOT)).ToString()))
					SetPDFVersionLobNode("MOT",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				else if(gStrLobCode == ((int)(LOBType.PPA)).ToString())
					SetPDFVersionLobNode("AUTO",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				else if(gStrLobCode == ((int)(LOBType.WAT)).ToString())
					SetPDFVersionLobNode("BOAT",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
			}
		}
		#endregion

		public string getAddIntPDFXml()
		{
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
			createRootElementForAllRootPDFs();			
			//LoadRateXML();
			FillMonth();

			//creating Xml From Here
			CreatePolicyAgencyXML();
			CreateNamedInsuredCoAppXml();
			createHomeAddlIntXml();
			createDwellingVehicleXML();
			creatmaillingpage();
			return AcordPDFXML.OuterXml;
		}

		#region code for mailling page xml Generation
		private void creatmaillingpage()
		{
			string strsendmessg="";
			string strInsname="";
			string strInsAdd="";
			string strcityzip="";
			XmlElement MaillingRootElementDecPage;
			MaillingRootElementDecPage    = AcordPDFXML.CreateElement("MAILLINGPAGE");
			//DataSet DstempAppDocument = new DataSet();
			//DataSet DstempDocument = new DataSet();
			// Additinol Intrest name and mailing address
			DataSet DSTempDwellinAdd=null;
			if(gStrLobCode == ((int)(LOBType.RENT)).ToString())
			{
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",0);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
				gobjWrapper.ClearParameteres();
				//DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			}
			else if (gStrLobCode == (((int)(LOBType.HOME)).ToString()))
				DSTempDwellinAdd=DSTempAddIntrst.Copy();
			
			else if(gStrLobCode == (((int)(LOBType.MOT)).ToString()))
			{
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@VEHICLEID",0);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
				gobjWrapper.ClearParameteres();
			}
			else if(gStrLobCode == ((int)(LOBType.PPA)).ToString())
				DSTempDwellinAdd=DSTempAddIntrst.Copy();
				//DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			
			else if(gStrLobCode == ((int)(LOBType.WAT)).ToString())
			{
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",0);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
				gobjWrapper.ClearParameteres();
				//DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			}
			
			if (DSTempDwellinAdd.Tables[0].Rows.Count >0)
			{
				DataRow Row = DSTempDwellinAdd.Tables[0].Rows[gaddlintindex];
				strInsname=RemoveJunkXmlCharacters(Row["HOLDER_NAME"].ToString());
				strInsAdd=RemoveJunkXmlCharacters(Row["ADDRESS"].ToString());
				if(strInsAdd.Trim().EndsWith(","))
					strInsAdd = strInsAdd.Substring(0,strInsAdd.LastIndexOf(","));
				if(gStrLobCode == (((int)(LOBType.HOME)).ToString()) || gStrLobCode == ((int)(LOBType.RENT)).ToString())
				{
					strcityzip=RemoveJunkXmlCharacters(Row["HOLDERCITYSTATEZIP"].ToString());
				}
				else
				{
					strcityzip=RemoveJunkXmlCharacters(Row["CITYSTATEZIP"].ToString());
				}
			}
			if (gStrPdfFor == PDFForDecPage)
			{
				if(gStrLobCode != ((int)(LOBType.PPA)).ToString() && gStrLobCode != ((int)(LOBType.HOME)).ToString())
				{
					gobjWrapper.AddParameter("@DOCUMENT_CODE","DEC_PAGE");
					DstempDocument = gobjWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage");
				}
				//gobjWrapper.ClearParameteres();
				//DstempDocument = gobjWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + "DEC_PAGE" + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
				//DecPageRootElement.AppendChild(MaillingRootElementDecPage);
				if(strsendmessg	=="Y")
				{
					//DecPageRootElement.AppendChild(MaillingRootElementDecPage);
					AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
					MaillingRootElementDecPage.SetAttribute(fieldType,fieldTypeMultiple);
					MaillingRootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("MAILLINGPAGE"));
					MaillingRootElementDecPage.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("MAILLINGPAGE"));
					MaillingRootElementDecPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("MAILLINGPAGEEXTN"));
					MaillingRootElementDecPage.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("MAILLINGPAGEEXTN"));
				
					XmlElement DecMailElement;
					DecMailElement =  AcordPDFXML.CreateElement("MAILADDRESS");
					MaillingRootElementDecPage.AppendChild(DecMailElement);
					DecMailElement.SetAttribute(fieldType,fieldTypeSingle);
					DecMailElement.SetAttribute(id,"0");


					DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MAILLINGNAME " + fieldType + "=\"" + fieldTypeText + "\">" + strInsname + "</MAILLINGNAME>";
					DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MAILLINGADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + strInsAdd + "</MAILLINGADDRESS>";
					DecMailElement.InnerXml = DecMailElement.InnerXml +  "<CITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + strcityzip + "</CITYSTATEZIP>";
					if(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString()!="0")
						DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MESSAGE " + fieldType + "=\"" + fieldTypeText + "\">" + DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString() + "</MESSAGE>";
				}
			}
			

		}
		#endregion
		
		#region To Create Root Element For All Root PDFs
		private void createRootElementForAllRootPDFs()
		{
			if (gStrPdfFor == PDFForDecPage)
			{
				DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

				//				if(gStrLobCode == "HOME")
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDLINTNEW"));
				/*				else if(gStrLobCode == "RENT")
									DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEREDWADDLINTNEW"));
								if(gStrLobCode == "MOT")
									DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEMOTADDLINTNEW"));
								else if(gStrLobCode == "PPA")
									DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTOADDLINTNEW"));
								else if(gStrLobCode == "WAT")
									DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBOATADDLINTNEW"));
				*/			}
		}
		#endregion

		private void ConvertLobCode()
		{
			if(gStrLobCode=="HOME")
				gStrLobCode="1";
			else if(gStrLobCode=="MOT")
				gStrLobCode="3";
			else if(gStrLobCode=="RENT")
				gStrLobCode="6";
			else if(gStrLobCode=="WAT")
				gStrLobCode="4";
		}
		#region Creating Policy And Agency Xml 
		private void CreatePolicyAgencyXML()
		{
//			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
//			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
//			gobjWrapper.ClearParameteres();
			//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			#region Global Variable Assignment
			PolicyNumber	= RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString());
			PolicyEffDate	= RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
			PolicyExpDate	= RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
			Reason			= RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["Reason"].ToString());
			AgencyName = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString());
			AgencyAddress = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString());
			AgencyCitySTZip = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString());
			AgencyPhoneNumber = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString());
			AgencyCode = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString());
			AgencySubCode = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString());
			AgencyBilling = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString())=="AB"?"Agency Bill":"Direct Bill" ;
			if(AgencyPhoneNumber !="")
			{
				if(AgencyPhoneNumber.IndexOf(")")>0)
				{
					string[] AgencyPhoneNumbers = new string[0];
					AgencyPhoneNumbers = AgencyPhoneNumber.Split(')');
					AgencyPhoneNumber = AgencyPhoneNumbers[0]+") "+AgencyPhoneNumbers[1];
				}
			}
			#endregion

			if (gStrPdfFor == PDFForDecPage)
			{
				#region Add Int Policy Agency Part
				XmlElement DecPagePolicyElement;
				DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
				DecPageRootElement.AppendChild(DecPagePolicyElement);
				DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<sign_dt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</sign_dt>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<polNum " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</polNum>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<pol_from " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</pol_from>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<pol_to " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</pol_to>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<POLICYVERSION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(gStrPolicyVersion) + "</POLICYVERSION>";

				if(gStrProcessID != null && gStrProcessID != "")
				{
					if(gStrLobCode != ((int)(LOBType.PPA)).ToString())
					{
					//	DataSet DSAddWordSet = new DataSet();
						gobjWrapper.AddParameter("@STATE_ID",0);
						gobjWrapper.AddParameter("@LOB_ID",0);
						gobjWrapper.AddParameter("@PROCESS_ID",gStrProcessID);
						DSAddWordSet = gobjWrapper.ExecuteDataSet("Proc_GetAddWordingsData");
						gobjWrapper.ClearParameteres();
					}
					//DSAddWordSet = gobjWrapper.ExecuteDataSet("Proc_GetAddWordingsData " +  "0," +  "0," + gStrProcessID);
			
					if (DSAddWordSet.Tables.Count > 1 && DSAddWordSet.Tables[1].Rows.Count > 0)
					{
						Reason		=	RemoveJunkXmlCharacters(DSAddWordSet.Tables[1].Rows[0]["PROCESS_DESC"].ToString());
					}
				}
				else
					Reason		=	RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["REASON"].ToString());

				if(Reason.Trim() != "" && DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
					Reason += " / Effective Date: " + DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();


				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<polReason " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Reason) + "</polReason>";
				//Agency
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AgenNam " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AgenNam>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AgenAddr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AgenAddr1>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AgenAddr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AgenAddr2>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AgenPhn " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AgencyPhoneNumber) + "</AgenPhn>";
				#endregion
			}
		}
		#endregion

		#region Creating Named Insured And CoApplicant Info
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
		private string NamedInsuredWithSuffixs(DataSet dsIns)
		{
			string names = "";
			if(dsIns.Tables.Count > 0)
			{
				foreach(DataRow drIns in dsIns.Tables[0].Rows)
				{
					if(names != "")
						names += " & " + drIns["APPNAME"].ToString()+ drIns["SUFFIX"].ToString();
					else
						names += drIns["APPNAME"].ToString() + drIns["SUFFIX"].ToString();
				}
			}
			return names;

		}

		private void CreateNamedInsuredCoAppXml()
		{
			if(gStrLobCode != ((int)(LOBType.PPA)).ToString() && gStrLobCode != ((int)(LOBType.HOME)).ToString())
			{
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
				gobjWrapper.ClearParameteres();
				DSTempApplicantDataSet = DSTempDataSet.Copy();
			}
			//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if (DSTempApplicantDataSet.Tables[0].Rows.Count > 0 )
			{
				#region Global Variable Assignment
				ApplicantName = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString());
				ApplicantAddress = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString());
				ApplicantCityStZip = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString());
				#endregion
                
				if (gStrPdfFor == PDFForDecPage)
				{
					#region Add Int Insured Details
					XmlElement DecPageNamedInsuredElement;
					DecPageNamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					DecPageRootElement.AppendChild(DecPageNamedInsuredElement);
					DecPageNamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					DecPageNamedInsuredElement.InnerXml = DecPageNamedInsuredElement.InnerXml +  "<InsName " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(NamedInsuredWithSuffixs(DSTempApplicantDataSet)) + "</InsName>";
					DecPageNamedInsuredElement.InnerXml = DecPageNamedInsuredElement.InnerXml +  "<InsAddr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString()) + "</InsAddr1>";
					DecPageNamedInsuredElement.InnerXml = DecPageNamedInsuredElement.InnerXml +  "<InsAddr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString()) + "</InsAddr2>";
					#endregion

				}
			}
		}
		#endregion

		#region Code for Dwelling Addl Interests
		private void createHomeAddlIntXml()
		{
			if (gStrPdfFor == PDFForDecPage)
			{
				DataSet DSTempDwellinAdd=null;
				if(gStrLobCode == ((int)(LOBType.RENT)).ToString())
				{
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@DWELLINGID",0);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
					gobjWrapper.ClearParameteres();
					//DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
				}
				else if (gStrLobCode == (((int)(LOBType.HOME)).ToString()))
					DSTempDwellinAdd=DSTempAddIntrst.Copy();
				else if(gStrLobCode == (((int)(LOBType.MOT)).ToString()))
				{
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@VEHICLEID",0);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
					gobjWrapper.ClearParameteres();
					//DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
				}
				else if(gStrLobCode == ((int)(LOBType.PPA)).ToString())
					DSTempDwellinAdd=DSTempAddIntrst.Copy();
				else if(gStrLobCode == ((int)(LOBType.WAT)).ToString())
				{
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",0);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
					gobjWrapper.ClearParameteres();
					//DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
				}
				#region Addl Int Info
				if (DSTempDwellinAdd.Tables[0].Rows.Count >0)
				{
					XmlElement DecPageAddlInts;
					DecPageAddlInts = AcordPDFXML.CreateElement("ADDITIONALINTERESTS");
					DecPageRootElement.AppendChild(DecPageAddlInts);
					DecPageAddlInts.SetAttribute(fieldType,fieldTypeSingle);

					DataRow Row = DSTempDwellinAdd.Tables[0].Rows[gaddlintindex];
					//					int RowCounter = 0;
					//					foreach(DataRow Row in DSTempDwellinAdd.Tables[0].Rows)
				{
					//						XmlElement Acord80ADDLINTElement;
					//						Acord80ADDLINTElement = AcordPDFXML.CreateElement("ADDLINTINFO");
					//						DecPageAddlInts.AppendChild(Acord80ADDLINTElement);
					//						Acord80ADDLINTElement.SetAttribute(fieldType,fieldTypeNormal);
					//						Acord80ADDLINTElement.SetAttribute(id,RowCounter.ToString());

					if(gStrLobCode == (((int)(LOBType.HOME)).ToString()) || gStrLobCode == ((int)(LOBType.RENT)).ToString())
					{
						//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<AddIntNam " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["HOLDER_NAME"].ToString())+"</AddIntNam>"; 
						string addl_addr = Row["ADDRESS"].ToString();
						if(addl_addr.Trim().EndsWith(","))
							addl_addr = addl_addr.Substring(0,addl_addr.LastIndexOf(","));
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<AddIntAddr1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addl_addr)+"</AddIntAddr1>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<AddIntAddr2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["HOLDERCITYSTATEZIP"].ToString())+"</AddIntAddr2>"; 
						//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString())+"</NATUREOFINTEREST>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<LoanNo " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["LOAN_REF_NUMBER"].ToString()) +"</LoanNo>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<NatInters " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["LOOKUP_VALUE_DESC"].ToString())+"</NatInters>"; 
					}
					else if(gStrLobCode == (((int)(LOBType.MOT)).ToString()) || gStrLobCode == ((int)(LOBType.PPA)).ToString())
					{
						//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<AddIntNam " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["HOLDER_NAME"].ToString())+"</AddIntNam>"; 
						string addl_addr = Row["ADDRESS"].ToString();
						if(addl_addr.Trim().EndsWith(","))
							addl_addr = addl_addr.Substring(0,addl_addr.LastIndexOf(","));
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<AddIntAddr1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addl_addr)+"</AddIntAddr1>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<AddIntAddr2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["CITYSTATEZIP"].ToString())+"</AddIntAddr2>"; 
						//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString())+"</NATUREOFINTEREST>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<LoanNo " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["LOAN_REF_NUMBER"].ToString()) +"</LoanNo>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<NatInters " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["ADDLINTNAME"].ToString())+"</NatInters>"; 
					}
					else if(gStrLobCode == ((int)(LOBType.WAT)).ToString())
					{
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<AddIntNam " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["HOLDER_NAME"].ToString())+"</AddIntNam>"; 
						string addl_addr = Row["ADDRESS"].ToString();
						if(addl_addr.Trim().EndsWith(","))
							addl_addr = addl_addr.Substring(0,addl_addr.LastIndexOf(","));
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<AddIntAddr1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addl_addr)+"</AddIntAddr1>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<AddIntAddr2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["CITYSTATEZIP"].ToString())+"</AddIntAddr2>"; 
						//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString())+"</NATUREOFINTEREST>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<LoanNo " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["LOAN_REF_NUMBER"].ToString()) +"</LoanNo>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<NatInters " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["ADDLINTNAME"].ToString())+"</NatInters>"; 
					}


					//						RowCounter++;
				}
				}
				#endregion
			}
		}
		#endregion

		#region code for dwelling/vehicle info Xml
		private double GetPrecentDed(string grandpercent, string insuredval)
		{
			double granddeduc = 0;
			if(grandpercent.IndexOf("%") > 0)
			{
				granddeduc = double.Parse(insuredval.Replace(",","")) * double.Parse(grandpercent.Replace("%","").Replace("-","").Trim()) / 100.00;
				if(grandpercent.Replace("%","").Replace("-","").Trim() == "1" && granddeduc < 100.00)
					granddeduc = 100.00;
				else if(grandpercent.Replace("%","").Replace("-","").Trim() == "2" && granddeduc < 200.00)
					granddeduc = 200.00;
				else if(grandpercent.Replace("%","").Replace("-","").Trim() == "5" && granddeduc < 500.00)
					granddeduc = 500.00;
			}
			return granddeduc;
		}

		private void createDwellingVehicleXML()
		{
			
			XmlElement DecPageDwellVehElement;
			DecPageDwellVehElement    = AcordPDFXML.CreateElement("DWELLCOV");
			string Id_prev = "";
			//			int DwellingCtr = 0,AddInt=0;			
			
			if(gStrLobCode == (((int)(LOBType.HOME)).ToString()) || gStrLobCode == ((int)(LOBType.RENT)).ToString())
			{
				if(gStrLobCode == ((int)(LOBType.RENT)).ToString())
				{
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details");
					gobjWrapper.ClearParameteres();
				}
				else if(gStrLobCode == (((int)(LOBType.HOME)).ToString()))
					DSTempDataSet = DSTempAutoDetailDataSet.Copy();
				//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			
				if (gStrPdfFor == PDFForDecPage)
				{
					#region Desc of Premises
					if (DSTempDataSet.Tables[0].Rows.Count>0)
					{
						XmlElement DecPageDescLoc;
						DecPageDescLoc = AcordPDFXML.CreateElement("DESCLOC");
						DecPageRootElement.AppendChild(DecPageDescLoc);
						DecPageDescLoc.SetAttribute(fieldType,fieldTypeSingle);

						DecPageDescLoc.InnerXml += "<res_prem_addr " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LOC_ADDRESS"].ToString() + ", " + DSTempDataSet.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString()) +"</res_prem_addr>"; 
					}
					#endregion
					
					
				}	
				if (DSTempDataSet.Tables[0].Rows.Count>0)
				{
					int RowCounter=0;
					#region setting Dwelling Root Attribute
					if (gStrPdfFor == PDFForDecPage)
					{
						#region Dwelling Root Element for DecPage
						DecPageRootElement.AppendChild(DecPageDwellVehElement);
						DecPageDwellVehElement.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageDwellVehElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDLINTNEWEXTN"));
						DecPageDwellVehElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTNEWEXTN"));
						DecPageDwellVehElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEADDLINTNEWSUPPLEMENT"));
						DecPageDwellVehElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTNEWSUPPLEMENT"));
						#endregion
					}
					#endregion
					foreach(DataRow DwellingDetail in DSTempDataSet.Tables[0].Rows)
					{
						if(Id_prev == DwellingDetail["DWELLING_ID"].ToString())
							continue;

						Id_prev = DwellingDetail["DWELLING_ID"].ToString();

						#region Coverages
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DataSet DSTempDwelling = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
						gobjWrapper.ClearParameteres();
						//DataSet DSTempDwelling = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
						//double dblSumTotal=0;
						string strLimit="";	
						string strDed1="";
						string covCode="";
						
						foreach(DataRow CoverageDetails in DSTempDwelling.Tables[0].Rows)
						{
							#region Dec Page Coverages
							//Added $ sign with Limit_1 for Itrack # 3487
							covCode = CoverageDetails["COV_CODE"].ToString();
							strLimit=CoverageDetails["LIMIT_1"].ToString().Trim() ;
							if(strLimit != "0")
								strLimit = "$" + strLimit; 
							if(strLimit.IndexOf(".00") > 0)
								strLimit = strLimit.Replace(".00","");
	
							//added to get deductible for Itrack # 3487
							if(CoverageDetails["COV_CODE"].ToString() == "APDI" || CoverageDetails["COV_CODE"].ToString() == "APD")
							{
							XmlElement DecPerilDeductible;
							DecPerilDeductible = AcordPDFXML.CreateElement("PERILDEDUC");
							DecPageRootElement.AppendChild(DecPerilDeductible);
							DecPerilDeductible.SetAttribute(fieldType,fieldTypeSingle);
							
							
								strDed1 = CoverageDetails["DEDUCTIBLE"].ToString();
								if(strDed1.IndexOf(".00") > 0)
									strDed1 = "$" + strDed1.Replace(".00","");
								if(strDed1.IndexOf("$")==-1)
									strDed1="$"+strDed1;
								DecPerilDeductible.InnerXml += "<ALL_PERIL_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed1)+"</ALL_PERIL_DEDUCTIBLE>";
							}
							//|| covCode == "LPP" || covCode == "RV" 
							if(covCode == "DWELL" || covCode == "OSTR" )
							{
								XmlElement DecPageCovElement;
								DecPageCovElement = AcordPDFXML.CreateElement("COVINFO");
								DecPageDwellVehElement.AppendChild(DecPageCovElement);
								DecPageCovElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageCovElement.SetAttribute(id,RowCounter.ToString());
								DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<frm_no " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters((RowCounter+1).ToString())+"</frm_no>";
								DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<ed_dt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</ed_dt>";
								DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";						
								DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit)+"</limit>";
							}
							
							//
						//	DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<ALL_PERIL_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed1)+"</ALL_PERIL_DEDUCTIBLE>";
							#endregion
							RowCounter++;
						}
						//				switch(CoverageDetails["COV_CODE"].ToString())
						//				{
						//					case "DWELL":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<ded " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</ded>";
						//						}
						//						break;
						//					case "OS":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//				
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";
						//						}
						//						break;
						//					case "EBUSPP":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";
						//						}
						//						break;
						//					case "LOSUR":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";
						//
						//
						//						}
						//						break;
						//					case "PL":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+".00")+"</limit>";							
						//						}
						//						break;
						//					case "MEDPM":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";							
						//						}
						//						break;						
						//
						//					case "APD":
						//						break;		
						//			
						//					case "EBEP11":
						//						break;
						//
						//				}
						#endregion
					}
				}
			}
			else if(gStrLobCode == (((int)(LOBType.MOT)).ToString()) || gStrLobCode == ((int)(LOBType.PPA)).ToString())
			{
		
				if(gStrLobCode == (((int)(LOBType.MOT)).ToString()))
				{
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details");
					gobjWrapper.ClearParameteres();
				}
				else if(gStrLobCode == ((int)(LOBType.PPA)).ToString())
				{
					DSTempDataSet = DSTempAutoDetailDataSet.Copy();
				}
				//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
				if(DSTempDataSet.Tables[0].Rows.Count>0)
				{
					XmlElement DecPageDescLoc;
					DecPageDescLoc = AcordPDFXML.CreateElement("DESCLOC");
					DecPageRootElement.AppendChild(DecPageDescLoc);
					DecPageDescLoc.SetAttribute(fieldType,fieldTypeSingle);
					
					int RowCounter=0;
					#region setting Dwelling Root Attribute
					if (gStrPdfFor == PDFForDecPage)
					{
						#region Dwelling Root Element for DecPage
						DecPageRootElement.AppendChild(DecPageDwellVehElement);
						DecPageDwellVehElement.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageDwellVehElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDLINTNEWEXTN"));
						DecPageDwellVehElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTNEWEXTN"));
						DecPageDwellVehElement.SetAttribute(SecondPDF,getAcordPDFBlockFromXML("DECPAGEADDLINTNEWSUPPLEMENT"));
						DecPageDwellVehElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTNEWSUPPLEMENT"));
						#endregion
					}
					#endregion

					foreach(DataRow AutoDetail in DSTempDataSet.Tables[0].Rows)
					{
						if(Id_prev == AutoDetail["VEHICLE_ID"].ToString())
							continue;

						Id_prev = AutoDetail["VEHICLE_ID"].ToString();
						
						DataSet DSTempDwellinAdd=null;
						if(gStrLobCode == ((int)(LOBType.MOT)).ToString())
						{
							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@VEHICLEID",0);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
							gobjWrapper.ClearParameteres();
						}
						else if(gStrLobCode == ((int)(LOBType.PPA)).ToString())
							DSTempDwellinAdd=DSTempAddIntrst.Copy();
						//DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
						string auto_id="";
						if (DSTempDwellinAdd.Tables[0].Rows.Count >0)
						{
							DataRow Row = DSTempDwellinAdd.Tables[0].Rows[gaddlintindex];
							auto_id = Row["VEHICLE_ID"].ToString();
						}
						
						#region Coverages
						string strlimit="",strded="";
						if(AutoDetail["VEHICLE_ID"].ToString() == auto_id)
						{
							DataSet DSTempVehicle=new DataSet();
							if(gStrLobCode == (((int)(LOBType.MOT)).ToString()))
							{
								gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
								gobjWrapper.AddParameter("@POLID",gStrPolicyId);
								gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
								gobjWrapper.AddParameter("@VEHICLEID",auto_id);
								gobjWrapper.AddParameter("@RISKTYPE",AutoDetail["MOTORCYCLETYPE"]);
								gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
								DSTempVehicle = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details");
								gobjWrapper.ClearParameteres();
								//DSTempVehicle = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + auto_id +  ",'"+ AutoDetail["MOTORCYCLETYPE"] +"','" + gStrCalledFrom + "'");
							}
							else if(gStrLobCode == ((int)(LOBType.PPA)).ToString())
							{
								gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
								gobjWrapper.AddParameter("@POLID",gStrPolicyId);
								gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
								gobjWrapper.AddParameter("@VEHICLEID",auto_id);
								gobjWrapper.AddParameter("@RISKTYPE",AutoDetail["RISKTYPE"]);
								gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
								DSTempVehicle = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_RiskWise_Coverage_Details");
								gobjWrapper.ClearParameteres();
								//DSTempVehicle = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + auto_id +  ",'"+ AutoDetail["RISKTYPE"] +"','" + gStrCalledFrom + "'");
							}
							if(gStrLobCode == (((int)(LOBType.MOT)).ToString()))
							{
								DecPageDescLoc.InnerXml += "<desc_MOTONum " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(AutoDetail["VEHICLE_CC"].ToString()+" CC's")  +"</desc_MOTONum>";
							}
							else if(gStrLobCode == ((int)(LOBType.PPA)).ToString())
							{
								DecPageDescLoc.InnerXml += "<desc_autoNum " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</desc_autoNum>";
							}
					
							foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
							{
								#region Dec Page Coverages
								XmlElement DecPageCovElement;
								if(CoverageDetails["COV_CODE"].ToString() == "BISPL" || CoverageDetails["COV_CODE"].ToString() == "PD" || CoverageDetails["COV_CODE"].ToString() == "SLL" ||  CoverageDetails["COV_CODE"].ToString() == "RLCSL")
								{
									if(CoverageDetails["LIMIT_1"].ToString()!="")
										strlimit=  GetIntFormat(CoverageDetails["LIMIT_1"].ToString());
									if(CoverageDetails["LIMIT_2"].ToString() != "" && strlimit != "")
									{
										strlimit=strlimit +  ",000";
										strlimit += "/" + GetIntFormat(CoverageDetails["LIMIT_2"].ToString());										
									}
									if(CoverageDetails["DEDUCTIBLE_1"].ToString()!="")
									strded =  GetIntFormat(CoverageDetails["DEDUCTIBLE_1"].ToString());
									DecPageCovElement = AcordPDFXML.CreateElement("COVINFO");
									DecPageDwellVehElement.AppendChild(DecPageCovElement);
									DecPageCovElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageCovElement.SetAttribute(id,RowCounter.ToString());
									
									DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<COV " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</COV>";
									DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strlimit)+"</LIMIT>";
									DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strded)+"</DED>";
									#endregion
									RowCounter++;
								}
							}
							RowCounter=0;
							foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
							{
								#region Dec Page Coverages
								XmlElement DecPageCovElement;
								strlimit="";strded="";
								if(CoverageDetails["COV_CODE"].ToString() == "COMP" || CoverageDetails["COV_CODE"].ToString() == "OTC" || CoverageDetails["COV_CODE"].ToString() == "COLL")
								{
									if(CoverageDetails["LIMIT_1"].ToString()!="")
									{
										strlimit=  GetIntFormat(CoverageDetails["LIMIT_1"].ToString());
									}
									else
									{
										strlimit=  "Included";
									}
									if(CoverageDetails["DEDUCTIBLE_1"].ToString()!="")
										strded =  GetIntFormat(CoverageDetails["DEDUCTIBLE_1"].ToString());
									if(CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"].ToString()!="")
										strded = strded + " "+CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"].ToString();
									DecPageCovElement = AcordPDFXML.CreateElement("COVLCINFO");
									DecPageDwellVehElement.AppendChild(DecPageCovElement);
									DecPageCovElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageCovElement.SetAttribute(id,RowCounter.ToString());
							
									DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<LC_COV " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</LC_COV>";
									DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<LC_ED_DT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</LC_ED_DT>";
									DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strlimit)+"</LC_LIMIT>";
									DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<LC_DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strded)+"</LC_DED>";
									#endregion
									RowCounter++;
								}
							}
						}
						#endregion
					}
				}
			}
			else if(gStrLobCode == ((int)(LOBType.WAT)).ToString())
			{
		
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
				gobjWrapper.ClearParameteres();
				//DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
				if(DSTempDataSet.Tables[0].Rows.Count>0)
				{
					int RowCounter=0;

					string boat_id = "0";
					DataSet DSTempDwellinAdd=null;
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",0);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
					gobjWrapper.ClearParameteres();
					//DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");

					if (DSTempDwellinAdd.Tables[0].Rows.Count >0)
					{
						DataRow Row = DSTempDwellinAdd.Tables[0].Rows[gaddlintindex];
						boat_id = Row["BOAT_ID"].ToString();
					}

					foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
					{
						//						if(Id_prev == BoatDetail["BOAT_ID"].ToString())
						//							continue;
						//
						//						Id_prev = BoatDetail["BOAT_ID"].ToString();
						//
						#region Coverages
						//						DataSet DSTempVehicle = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
						//						double dblSumTotal=0;
						//						foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
						if(BoatDetail["BOAT_ID"].ToString() == boat_id)
						{
							XmlElement DecPageDescLoc;
							DecPageDescLoc = AcordPDFXML.CreateElement("DESCLOC");
							DecPageRootElement.AppendChild(DecPageDescLoc);
							DecPageDescLoc.SetAttribute(fieldType,fieldTypeSingle);
							DecPageDescLoc.InnerXml += "<watLoc " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["LOCADD"].ToString()) +"</watLoc>";
							//Added by Manoj Rathore Itrack No. 3243
							DecPageDescLoc.InnerXml += "<watLoccity " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["LOCCITY"].ToString()) +"</watLoccity>";

							//Added 15-Jan-08 Mohit Agarwal
							#region To Get Boat Deductible
							string boat_ded = "";
							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@BOATID",boat_id);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DataSet DSTempEngineTrailer1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
							gobjWrapper.ClearParameteres();
							//DataSet DSTempEngineTrailer1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +boat_id +  ",'" + gStrCalledFrom + "'");
							foreach(DataRow CoverageDetails1 in DSTempEngineTrailer1.Tables[0].Rows)
							{

								string covCode1 = CoverageDetails1["COV_CODE"].ToString();
								if(covCode1 == "BDEDUC" && boat_ded == "")
								{
									boat_ded = CoverageDetails1["DEDUCTIBLE_1"].ToString();
									string grandpercent = CoverageDetails1["DEDUCTIBLE1_AMOUNT_TEXT"].ToString();
									if(grandpercent.IndexOf("%") > 0)
									{
										double granddeduc = GetPrecentDed(grandpercent, BoatDetail["PRESENT_VALUE"].ToString());
										if(granddeduc != 0)
											boat_ded = DollarFormat(granddeduc);
									}
									//break;
								}
							}
							#endregion To Get Boat Deductible

							#region setting Dwelling Root Attribute
							DecPageRootElement.AppendChild(DecPageDwellVehElement);
							DecPageDwellVehElement.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageDwellVehElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDLINTNEWEXTN"));
							DecPageDwellVehElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTNEWEXTN"));
							DecPageDwellVehElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEADDLINTNEWSUPPLEMENT"));
							DecPageDwellVehElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTNEWSUPPLEMENT"));
							#endregion

							#region Dec Page Coverages
							XmlElement DecPageCovElement;
							DecPageCovElement = AcordPDFXML.CreateElement("COVINFO");
							DecPageDwellVehElement.AppendChild(DecPageCovElement);
							DecPageCovElement.SetAttribute(fieldType,fieldTypeNormal);
							DecPageCovElement.SetAttribute(id,RowCounter.ToString());
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<WATERSNAVIGATED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["WATERS_NAVIGATED_USE"].ToString())+"</WATERSNAVIGATED>"; 
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATTERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_TERRITORY"].ToString())+"</BOATTERRITORY>"; 
						
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString())+"</BOATNUMBER>"; 
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOATTYPE"].ToString())+"</BOATTYPE>";
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATYEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString())+"</BOATYEAR>";
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATDESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MAKE_MODEL"].ToString())+"</BOATDESC>";
							if(BoatDetail["BOAT_INCHES"].ToString()=="")
							{
								DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATLENGTH  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'")+"</BOATLENGTH>";
							}
							else
							{
								DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATLENGTH  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'"+" "+ BoatDetail["BOAT_INCHES"].ToString() +"''")+"</BOATLENGTH>";
							}
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATHPCC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HORSE_POWER"].ToString().Replace(".00",""))+"</BOATHPCC>";
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATSERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_ID_NO"].ToString())+"</BOATSERIAL>";
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATCONSTRUCTION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_MATERIAL"].ToString())+"</BOATCONSTRUCTION>";
							if(BoatDetail["PRESENT_VALUE"].ToString() == "")
							{
								DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATLIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</BOATLIMIT>";
							}
							else
							{
								DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATLIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + BoatDetail["PRESENT_VALUE"].ToString())+"</BOATLIMIT>";
							}
								//Added $ Sign for Itrack # 2440
							if(boat_ded.IndexOf("$")<0)
							{
								boat_ded = "$" + boat_ded;
							}
							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATDED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATDED>";
						//	DecPageCovElement.InnerXml= DecPageCovElement.InnerXml +  "<BOATDED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATDED>";
							//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<frm_no " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters((RowCounter+1).ToString())+"</frm_no>";
							//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<ed_dt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</ed_dt>";
							//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";
							//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";
							//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<ded " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</ded>";
							#endregion

							break;
							RowCounter++;
						}
						//				switch(CoverageDetails["COV_CODE"].ToString())
						//				{
						//					case "DWELL":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<ded " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</ded>";
						//						}
						//						break;
						//					case "OS":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//				
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";
						//						}
						//						break;
						//					case "EBUSPP":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";
						//						}
						//						break;
						//					case "LOSUR":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<cov " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</cov>";
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";
						//
						//
						//						}
						//						break;
						//					case "PL":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+".00")+"</limit>";							
						//						}
						//						break;
						//					case "MEDPM":
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//							DecPageCovElement.InnerXml= DecPageCovElement.InnerXml + "<limit " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</limit>";							
						//						}
						//						break;						
						//
						//					case "APD":
						//						break;		
						//			
						//					case "EBEP11":
						//						break;
						//
						//				}
						#endregion
					}
				}
			}
		}
		#endregion

	}
}