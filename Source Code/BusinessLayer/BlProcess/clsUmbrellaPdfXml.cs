using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for clsUmbrellaPdfXml.
	/// </summary>
	public class clsUmbrellaPdfXml : ClsCommonPdfXML
	{
		#region Declarations
		private XmlElement RootElementDecPage;
		private XmlElement Acord83RootElement;
		private XmlElement SupplementalRootElement;
		private Hashtable htpremium=new Hashtable(); 

		int RowCounter=0;
		string globID="UMB";
		string gcallFOR = "POLICY";
		private DataWrapper gobjSqlHelper;
		private string stCode="";
		#endregion

		#region Constructor
		public clsUmbrellaPdfXml(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string stateCode,string strProcessID,string Agn_Ins)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			stCode=stateCode;
			gStrProcessID = strProcessID;
			gStrCopyTo = Agn_Ins;
			if(Agn_Ins != null && Agn_Ins != "")
			{
				string []copyTo = Agn_Ins.Split('-');
				CopyTo= "Copy To: " + copyTo[0];
			}
			
			DSTempDataSet = new DataSet();
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempDataSet.Tables[0].Rows.Count>0)
			SetPDFVersionLobNode("UMB",DateTime.Parse(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
		}
		#endregion
		
		public string getUmbrellaAcordPDFXml()
		{
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementForAllRootPDFs();
//			//LoadRateXML();
			FillMonth();
			CreatePolicyAgencyXML();
			CreateCoApplicantXml();
			CreateLocationXml();
			UmbrellaInfoXml();
			CreateAutoRecVehicleXml();
			CreateNamedInsuredCoAppXml();
			CreategeneralInformationXml();
			if(gStrCopyTo == "CUSTOMER")
				createAddWordingsXML();
			
			return AcordPDFXML.OuterXml;
		}


		#region Root Element For All Root PDFs
		private void createRootElementForAllRootPDFs()
		{
			if (gStrPdfFor == PDFForDecPage)
			{
				RootElementDecPage = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(RootElementDecPage);
				if(stCode.Equals("IN")) 
					RootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIN"));
				else if(stCode.Equals("MI")) 
					RootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEMI"));

				
			}
			else if (gStrPdfFor == PDFForAcord)
			{
				Acord83RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord83RootElement);
				if(stCode.Equals("IN")) 
				Acord83RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD83IN"));
				else if(stCode.Equals("MI")) 
					Acord83RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD83MI"));

				SupplementalRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(SupplementalRootElement);
				if(stCode.Equals("IN")) 
					SupplementalRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTIN"));
				else if(stCode.Equals("MI")) 
					SupplementalRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTMI"));
			}
		}
		#endregion

		#region Policy And Agency Xml 
		private void CreatePolicyAgencyXML()
		{
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{	
				#region Global Variable Assignment
				PolicyNumber	= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString());
				PolicyEffDate	= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
				PolicyExpDate	= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
				if(gStrProcessID != null && gStrProcessID != "")
				{
					DataSet DSAddWordSet = new DataSet();
					DSAddWordSet = gobjSqlHelper.ExecuteDataSet("Proc_GetAddWordingsData " +  "0," +  "0," + gStrProcessID);
			
					if (DSAddWordSet.Tables.Count > 1 && DSAddWordSet.Tables[1].Rows.Count > 0)
						Reason		=	RemoveJunkXmlCharacters(DSAddWordSet.Tables[1].Rows[0]["PROCESS_DESC"].ToString());
				}
				else
					Reason			= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["Reason"].ToString());
//				CopyTo			= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COPY_TO"].ToString());

				if(Reason.Trim() != "" && DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
					Reason += " / Effective Date: " + DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
				AgencyName = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString());
				AgencyAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString());
				AgencyCitySTZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString());
				AgencyPhoneNumber = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString());
				AgencyCode = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NUM_AGENCY_CODE"].ToString());
				AgencySubCode = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString());
				AgencyBilling = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString())=="AB"?"Agency Bill":"Direct Bill" ;
				#endregion
			
				if (gStrPdfFor == PDFForDecPage)
				{
					#region DecPage Element
					XmlElement DecPagePolicyElement;
					DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
					RootElementDecPage.AppendChild(DecPagePolicyElement);
					DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
					#endregion

					DecPagePolicyElement.InnerXml += "<POLICY_NO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICY_NO>";
					DecPagePolicyElement.InnerXml += "<EFFECTIVE_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVE_DATE>";
					DecPagePolicyElement.InnerXml += "<EXPIRATION_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATION_DATE>";
					DecPagePolicyElement.InnerXml += "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
					DecPagePolicyElement.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
					DecPagePolicyElement.InnerXml += "<AGENCY_NAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCY_NAME>";
					DecPagePolicyElement.InnerXml += "<AGENCY_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCY_ADDRESS>";
					DecPagePolicyElement.InnerXml += "<AGENCY_MAILING_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCY_MAILING_ADDRESS>";
					DecPagePolicyElement.InnerXml += "<AGENCY_CODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NUM_AGENCY_CODE"].ToString()) + "</AGENCY_CODE>";
					DecPagePolicyElement.InnerXml += "<AGENCY_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCY_PHONE>";
					DecPagePolicyElement.InnerXml += "<AGENCY_SUB_CODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCY_SUB_CODE>";
					DecPagePolicyElement.InnerXml += "<BILLING " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString().ToUpper()) + "</BILLING>";
					DecPagePolicyElement.InnerXml += "<PERSONAL_EXCESS_LIABILITY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_LIMITS"].ToString().ToUpper()) + "</PERSONAL_EXCESS_LIABILITY>";
					DecPagePolicyElement.InnerXml += "<SELF_RETAINED_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RETENTION_LIMITS"].ToString().ToUpper()) + "</SELF_RETAINED_LIMIT>";
					DecPagePolicyElement.InnerXml += "<PERSONAL_EXCESS_LIABILITY_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_LIMITS"].ToString().ToUpper()) + "</PERSONAL_EXCESS_LIABILITY_LIMIT>";
										
				}
				if (gStrPdfFor == PDFForAcord)
				{
					#region Acord83 Page

					XmlElement AcordPolicyElement;
					AcordPolicyElement = AcordPDFXML.CreateElement("POLICY");
					Acord83RootElement.AppendChild(AcordPolicyElement);
					AcordPolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</DATE>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCY_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCY_PHONE>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCY_FAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCY_FAX>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCY_NAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCY_NAME>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCY_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCY_ADDRESS>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCY_MAILING_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCY_MAILING_ADDRESS>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EMAIL_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_EMAIL"].ToString()) + "</EMAIL_ADDRESS>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCY_CODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NUM_AGENCY_CODE"].ToString()) + "</AGENCY_CODE>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCY_SUB_CODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCY_SUB_CODE>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<POLICY_NO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICY_NO>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EFFECTIVE_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVE_DATE>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EXPIRATION_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATION_DATE>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EFF_DATE_0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFF_DATE_0>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EXP_DATE_0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXP_DATE_0>";
					if(DSTempDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString() != "0")
						AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<CURR_YEAR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString()) + "</CURR_YEAR>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<CO_PLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine Mutual") + "</CO_PLAN>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<POLICY_AMT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_LIMITS"].ToString()) + "</POLICY_AMT>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<RETENTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RETENTION_LIMITS"].ToString()) + "</RETENTION>";
					//					if(stCode.Equals("IN"))
//						AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<COV_UNINSUREDMOTORIST " + fieldType + "=\"" + fieldTypeText + "\">" +""+ "</COV_UNINSUREDMOTORIST>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BILLING " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString().ToUpper()) + "</BILLING>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<DIRECTBILL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTDIRECTBILL"].ToString().ToUpper()) + "</DIRECTBILL>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BILLINGPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString().ToUpper()) + "</BILLINGPLAN>";
				
					#endregion
				}
			}

		}
		#endregion

		#region Co-Applicant or Named Insured Info Xml Generation.
		private void CreateCoApplicantXml()
		{
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{

				#region Global Variable Assignment
				ApplicantName = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString());
				ApplicantAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString());
				ApplicantCityStZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString());
				
				reason_code1 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE"].ToString());
				reason_code2 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE2"].ToString());
				reason_code3 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE3"].ToString());
				reason_code4 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE4"].ToString());

				CustomerAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString());
				CustomerCityStZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString());
				#endregion

				if (gStrPdfFor == PDFForDecPage)
				{
					#region DecPage Element
					XmlElement DecPageNamedInsuredElement;
					DecPageNamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					RootElementDecPage.AppendChild(DecPageNamedInsuredElement);
					DecPageNamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);
					#endregion
					
					DecPageNamedInsuredElement.InnerXml += "<APP_NAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APP_NAME>";
					DecPageNamedInsuredElement.InnerXml += "<APP_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString()) + "</APP_ADDRESS>";
					DecPageNamedInsuredElement.InnerXml += "<APP_CITYCOUNTYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString()) + "</APP_CITYCOUNTYSTATEZIP>";
					//Reason Code
//					DecPageNamedInsuredElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
//					DecPageNamedInsuredElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
//					DecPageNamedInsuredElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
//					DecPageNamedInsuredElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
//					if(gStrCopyTo != "AGENCY")
//						createPage2XML(ref RootElementDecPage);
					
					
					
				}
				
				if (gStrPdfFor==PDFForAcord)
				{	
					#region Acord 83 Page
					XmlElement Acord83ApplicantRootElement;
					Acord83ApplicantRootElement = AcordPDFXML.CreateElement("APPLICANTDETAILSELEMENT");
					Acord83RootElement.AppendChild(Acord83ApplicantRootElement);
					Acord83ApplicantRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					Acord83ApplicantRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD83APPLICANT"));
					Acord83ApplicantRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD83APPLICANT"));
					Acord83ApplicantRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD83APPLICANTEXTN"));
					Acord83ApplicantRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD83APPLICANTEXTN"));
					#endregion
					int appCounter=0;
					foreach(DataRow AppDetail in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement AcordUmbrellaApplicantElement;
						AcordUmbrellaApplicantElement = AcordPDFXML.CreateElement("APPLICANTDETAILSINFO");
						Acord83ApplicantRootElement.AppendChild(AcordUmbrellaApplicantElement);
						AcordUmbrellaApplicantElement.SetAttribute(fieldType,fieldTypeNormal);
						AcordUmbrellaApplicantElement.SetAttribute(id,appCounter.ToString());

						AcordUmbrellaApplicantElement.InnerXml = AcordUmbrellaApplicantElement.InnerXml +  "<APP_NAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AppDetail["APPNAME"].ToString()) + "</APP_NAME>";
						AcordUmbrellaApplicantElement.InnerXml = AcordUmbrellaApplicantElement.InnerXml +  "<APP_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AppDetail["CUSTADDRESS"].ToString()) + "</APP_ADDRESS>";
						AcordUmbrellaApplicantElement.InnerXml = AcordUmbrellaApplicantElement.InnerXml +  "<APP_CITYCOUNTYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AppDetail["APPCITYSTZIP"].ToString()) + "</APP_CITYCOUNTYSTATEZIP>";
						AcordUmbrellaApplicantElement.InnerXml = AcordUmbrellaApplicantElement.InnerXml +  "<APP_HOMEPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AppDetail["PHONE"].ToString()) + "</APP_HOMEPHONE>";
						AcordUmbrellaApplicantElement.InnerXml = AcordUmbrellaApplicantElement.InnerXml +  "<BUSINESS_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AppDetail["CUSTOMER_BUSINESS_PHONE"].ToString()) + "</BUSINESS_PHONE>";
						
						appCounter++;
					}
					
					
				}
			}
		}
		#endregion

		#region Location/Property Xml Generation.
		private void CreateLocationXml()
		{
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFUmbrella_LocationDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{

				if (gStrPdfFor==PDFForAcord)
				{	
					#region Acord 83 Page
					XmlElement Acord83LocationRootElement;
					Acord83LocationRootElement = AcordPDFXML.CreateElement("LOCATIONDETAILELEMENT");
					Acord83RootElement.AppendChild(Acord83LocationRootElement);
					Acord83LocationRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					Acord83LocationRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD83LOCATION"));
					Acord83LocationRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD83LOCATION"));
					Acord83LocationRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD83LOCATIONEXTN"));
					Acord83LocationRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD83LOCATIONEXTN"));
					#endregion
					int locCounter=0;
					foreach(DataRow LocDetail in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement AcordUmbrellaLocationElement;
						AcordUmbrellaLocationElement = AcordPDFXML.CreateElement("APPLICANTDETAILSINFO");
						Acord83LocationRootElement.AppendChild(AcordUmbrellaLocationElement);
						AcordUmbrellaLocationElement.SetAttribute(fieldType,fieldTypeNormal);
						AcordUmbrellaLocationElement.SetAttribute(id,locCounter.ToString());

						AcordUmbrellaLocationElement.InnerXml = AcordUmbrellaLocationElement.InnerXml +  "<PROP_LOCATION_NO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(LocDetail["LOCATION_NUMBER"].ToString()) + "</PROP_LOCATION_NO>";
						AcordUmbrellaLocationElement.InnerXml = AcordUmbrellaLocationElement.InnerXml +  "<PROP_LOCATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(LocDetail["LOCATION"].ToString()) + "</PROP_LOCATION>";
						AcordUmbrellaLocationElement.InnerXml = AcordUmbrellaLocationElement.InnerXml +  "<PROP_DESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(LocDetail["LOCDESCRIPTION"].ToString()) + "</PROP_DESCRIPTION>";
						AcordUmbrellaLocationElement.InnerXml = AcordUmbrellaLocationElement.InnerXml +  "<PROP_OCCUPANCY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(LocDetail["OCCUPANCY"].ToString()) + "</PROP_OCCUPANCY>";
						AcordUmbrellaLocationElement.InnerXml = AcordUmbrellaLocationElement.InnerXml +  "<PROP_USAGE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(LocDetail["USAGE"].ToString()) + "</PROP_USAGE>";
						
						locCounter++;
					}
					
					
				}
			}
		}
		#endregion

		#region Umbrella Info Xml Generation
		private string GetUmbPremium(DataSet dsCov, string comp_code)
		{
			if(dsCov.Tables.Count > 2)
			{
				foreach(DataRow drCov in dsCov.Tables[2].Rows)
				{
					if(drCov["COMPONENT_CODE"].ToString() == comp_code)
						return drCov["PREMIUM"].ToString();
				}
			}
			return "";
		}

		private void UmbrellaInfoXml()
		{
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFUmbrella_Cov_Forms_Endo_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			#region UNDERLYING POLICY	
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{
				
				if (gStrPdfFor == PDFForDecPage)
				{	
					#region DecPage Element
					XmlElement DecPageUnderlyingPolElement;
					DecPageUnderlyingPolElement = AcordPDFXML.CreateElement("UNDERLYINGPOLICY");
					RootElementDecPage.AppendChild(DecPageUnderlyingPolElement);
					DecPageUnderlyingPolElement.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageUnderlyingPolElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUNDERLYINGPOL"));
					DecPageUnderlyingPolElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNDERLYINGPOL"));
					DecPageUnderlyingPolElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUNDERLYINGPOLEXTN"));
					DecPageUnderlyingPolElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNDERLYINGPOLEXTN"));
					#endregion
					
					RowCounter = 0;
					string polNum = "";
					foreach(DataRow UndPol in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement DecPageUnderLyingPol;
						DecPageUnderLyingPol = AcordPDFXML.CreateElement("UNDERLYINGPOLICYINFO");
						DecPageUnderlyingPolElement.AppendChild(DecPageUnderLyingPol);
						DecPageUnderLyingPol.SetAttribute(fieldType,fieldTypeNormal);
						DecPageUnderLyingPol.SetAttribute(id,RowCounter.ToString());
					
						if(polNum == UndPol["POLICY_NUMBER"].ToString())
							continue;

						polNum = UndPol["POLICY_NUMBER"].ToString();
						DecPageUnderLyingPol.InnerXml += "<POLICYTYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_LOB"].ToString()) + "</POLICYTYPE>";
						DecPageUnderLyingPol.InnerXml += "<COMPANYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_COMPANY"].ToString()) + "</COMPANYNAME>";
						DecPageUnderLyingPol.InnerXml += "<EFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["EFFECTIVE_DATE"].ToString()) + "</EFFECTIVEDATE>";
						DecPageUnderLyingPol.InnerXml += "<POLICYLIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</POLICYLIMIT>";
						DecPageUnderLyingPol.InnerXml += "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
						if(RowCounter == 0)
							DecPageUnderLyingPol.InnerXml += "<TOTALPOLICYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GetUmbPremium(DSTempDataSet, "SUMTOTAL")) + "</TOTALPOLICYPREMIUM>";
						RowCounter++;
					}
					
				}


				if (gStrPdfFor == PDFForAcord)
				{	
					#region Acord Element
					XmlElement AcordPolicyInfoElement;
					AcordPolicyInfoElement = AcordPDFXML.CreateElement("POLICYINFO");
					Acord83RootElement.AppendChild(AcordPolicyInfoElement);
					AcordPolicyInfoElement.SetAttribute(fieldType,fieldTypeMultiple);
					AcordPolicyInfoElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUNDERLYINGPOL"));
					AcordPolicyInfoElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNDERLYINGPOL"));
					AcordPolicyInfoElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUNDERLYINGPOLEXTN"));
					AcordPolicyInfoElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNDERLYINGPOLEXTN"));
					#endregion
					
					int rv = 0;
					DataSet DSTempDataSet1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFUmbrella_AutoRecVehicleDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
					if (DSTempDataSet1.Tables[1].Rows.Count > 0 )
						rv = 1;

					RowCounter = 0;
					XmlElement AcordPolicy;
					AcordPolicy = AcordPDFXML.CreateElement("UNDERLYINGPOLICYINFO");
					AcordPolicyInfoElement.AppendChild(AcordPolicy);
					AcordPolicy.SetAttribute(fieldType,fieldTypeNormal);
					AcordPolicy.SetAttribute(id,"0");
					foreach(DataRow UndPol in DSTempDataSet.Tables[0].Rows)
					{
						
						switch(UndPol["POLICY_LOB"].ToString())
						{
							case "AUTOP":
							{
								AcordPolicy.InnerXml += "<AUTO_COMPNAMEPOLNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_COMPANY"].ToString())+"/"+RemoveJunkXmlCharacters(UndPol["POLICY_NUMBER"].ToString()) + "</AUTO_COMPNAMEPOLNO>";
								AcordPolicy.InnerXml += "<AUTO_POLPERIOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["EFFECTIVE_DATE"].ToString())+"-"+RemoveJunkXmlCharacters(UndPol["EXPIRATION_DATE"].ToString()) + "</AUTO_POLPERIOD>";
								switch(UndPol["COV_CODE"].ToString())
								{
									case "PUNCS":
										AcordPolicy.InnerXml += "<COV_UNINSUREDMOTORIST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</COV_UNINSUREDMOTORIST>";
										AcordPolicy.InnerXml += "<AUTO_LIMITUICSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</AUTO_LIMITUICSL>";
										break;
									case "UNCSL":
										AcordPolicy.InnerXml += "<UNDERINSURED_MOTORIST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</UNDERINSURED_MOTORIST>";
										break;
									case "PUMSP":
										AcordPolicy.InnerXml += "<COV_UNINSUREDMOTORIST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</COV_UNINSUREDMOTORIST>";
										AcordPolicy.InnerXml += "<AUTO_LIMITUIBI " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</AUTO_LIMITUIBI>";
										break;
									case "UNDSP":
										AcordPolicy.InnerXml += "<UNDERINSURED_MOTORIST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</UNDERINSURED_MOTORIST>";
										break;
									case "PD":
										AcordPolicy.InnerXml += "<AUTO_LIMITPD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</AUTO_LIMITPD>";
										break;
									case "BISPL":
										AcordPolicy.InnerXml += "<AUTO_LIMITBI " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</AUTO_LIMITBI>";
										break;
									default:
										if(UndPol["COVERAGE_DESC"].ToString().Trim() == "Single Limits Liability (CSL)")
											AcordPolicy.InnerXml += "<AUTO_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</AUTO_LIMITCSL>";
										break;
								}
							break;
							}

							
							case "REDW":
							{
									AcordPolicy.InnerXml += "<RENTAL_COMPNAMEPOLNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_COMPANY"].ToString())+"/"+RemoveJunkXmlCharacters(UndPol["POLICY_NUMBER"].ToString()) + "</RENTAL_COMPNAMEPOLNO>";
									AcordPolicy.InnerXml += "<RENTAL_POLPERIOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["EFFECTIVE_DATE"].ToString())+"-"+RemoveJunkXmlCharacters(UndPol["EXPIRATION_DATE"].ToString()) + "</RENTAL_POLPERIOD>";
									switch(UndPol["COV_CODE"].ToString())
									{
										case "PL":
											AcordPolicy.InnerXml += "<RENTAL_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</RENTAL_LIMITCSL>";
											break;
										default:
											break;
									}
								break;
							}

							case "BOAT":
							{
									AcordPolicy.InnerXml += "<WATERCRAFT_COMPNAMEPOLNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_COMPANY"].ToString())+"/"+RemoveJunkXmlCharacters(UndPol["POLICY_NUMBER"].ToString()) + "</WATERCRAFT_COMPNAMEPOLNO>";
									AcordPolicy.InnerXml += "<WATERCRAFT_POLPERIOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["EFFECTIVE_DATE"].ToString())+"-"+RemoveJunkXmlCharacters(UndPol["EXPIRATION_DATE"].ToString()) + "</WATERCRAFT_POLPERIOD>";
									switch(UndPol["COV_CODE"].ToString())
									{
//										case "UMBCS":
//											AcordPolicy.InnerXml += "<RENTAL_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</RENTAL_LIMITCSL>";
//											break;
										case "LCCSL":
											AcordPolicy.InnerXml += "<WATERCRAFT_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</WATERCRAFT_LIMITCSL>";
											break;
										default:
											break;
									}
								break;
							}

							case "HOME":
							{
									
									AcordPolicy.InnerXml += "<PL_COMPNAMEPOLNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_COMPANY"].ToString())+"/"+RemoveJunkXmlCharacters(UndPol["POLICY_NUMBER"].ToString()) + "</PL_COMPNAMEPOLNO>";
									AcordPolicy.InnerXml += "<PL_POLPERIOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["EFFECTIVE_DATE"].ToString())+"-"+RemoveJunkXmlCharacters(UndPol["EXPIRATION_DATE"].ToString()) + "</PL_POLPERIOD>";
							
									if(rv == 1)
									{
										AcordPolicy.InnerXml += "<RECVEHBAS_COMPNAMEPOLNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_COMPANY"].ToString())+"/"+RemoveJunkXmlCharacters(UndPol["POLICY_NUMBER"].ToString()) + "</RECVEHBAS_COMPNAMEPOLNO>";
										AcordPolicy.InnerXml += "<RECVEHBAS_POLPERIOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["EFFECTIVE_DATE"].ToString())+"-"+RemoveJunkXmlCharacters(UndPol["EXPIRATION_DATE"].ToString()) + "</RECVEHBAS_POLPERIOD>";
								
								
										AcordPolicy.InnerXml += "<RECVEHUI_COMPNAMEPOLNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_COMPANY"].ToString())+"/"+RemoveJunkXmlCharacters(UndPol["POLICY_NUMBER"].ToString()) + "</RECVEHUI_COMPNAMEPOLNO>";
										AcordPolicy.InnerXml += "<RECVEHUI_POLPERIOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["EFFECTIVE_DATE"].ToString())+"-"+RemoveJunkXmlCharacters(UndPol["EXPIRATION_DATE"].ToString()) + "</RECVEHUI_POLPERIOD>";
									}
									switch(UndPol["COV_CODE"].ToString())
									{
										case "PL":
											AcordPolicy.InnerXml += "<PL_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</PL_LIMITCSL>";
											if(rv == 1)
												AcordPolicy.InnerXml += "<RECVEHBAS_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</RECVEHBAS_LIMITCSL>";
											break;
										default:
											break;
									}
								break;
							}
						
								//For EL
								/*	if(UndPol[POLICY_LOB].Equals("????"))
									{
										AcordPolicy.InnerXml += "<EL_COMPNAMEPOLNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_COMPANY"].ToString())+"/"+RemoveJunkXmlCharacters(UndPol["POLICY_NUMBER"].ToString()) + "</EL_COMPNAMEPOLNO>";
										AcordPolicy.InnerXml += "<EL_POLPERIOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["EFFECTIVE_DATE"].ToString())+"-"+RemoveJunkXmlCharacters(UndPol["EXPIRATION_DATE"].ToString()) + "</EL_POLPERIOD>";
							
									} */

							case "CYCL":
							{
									AcordPolicy.InnerXml += "<MOTOR_COMPNAMEPOLNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["POLICY_COMPANY"].ToString())+"/"+RemoveJunkXmlCharacters(UndPol["POLICY_NUMBER"].ToString()) + "</MOTOR_COMPNAMEPOLNO>";
									AcordPolicy.InnerXml += "<MOTOR_POLPERIOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["EFFECTIVE_DATE"].ToString())+"-"+RemoveJunkXmlCharacters(UndPol["EXPIRATION_DATE"].ToString()) + "</MOTOR_POLPERIOD>";
									switch(UndPol["COV_CODE"].ToString())
									{
										case "PUNCS":
											AcordPolicy.InnerXml += "<MOTOR_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</MOTOR_LIMITCSL>";
											break;
										case "UNCSL":
											break;
										case "PUMSP":
											AcordPolicy.InnerXml += "<MOTOR_LIMITBI " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</MOTOR_LIMITBI>";
											break;
										case "UNDSP":
											break;
										case "PD":
											AcordPolicy.InnerXml += "<MOTOR_LIMITPD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</MOTOR_LIMITPD>";
											break;
										case "BISPL":
											AcordPolicy.InnerXml += "<MOTOR_LIMITBI " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</MOTOR_LIMITBI>";
											break;
										case "RLCSL":
											AcordPolicy.InnerXml += "<MOTOR_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UndPol["LIMIT"].ToString()) + "</MOTOR_LIMITCSL>";
											break;
										default:
											break;
									}
								break;
							}

						}
						AcordPolicy.InnerXml += "<EL_COMPNAMEPOLNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("N/A") + "</EL_COMPNAMEPOLNO>";
						AcordPolicy.InnerXml += "<EL_POLPERIOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("N/A") + "</EL_POLPERIOD>";
						AcordPolicy.InnerXml += "<EL_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("N/A") + "</EL_LIMITCSL>";
						if(rv == 1)
						{
							AcordPolicy.InnerXml += "<RECVEHBAS_LIMITBI " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("N/A") + "</RECVEHBAS_LIMITBI>";
							AcordPolicy.InnerXml += "<RECVEHBAS_LIMITPD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("N/A") + "</RECVEHBAS_LIMITPD>";
							AcordPolicy.InnerXml += "<RECVEHUI_LIMITCSL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("N/A") + "</RECVEHUI_LIMITCSL>";
							AcordPolicy.InnerXml += "<RECVEHUI_LIMITBI " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("N/A") + "</RECVEHUI_LIMITBI>";
							AcordPolicy.InnerXml += "<RECVEHUI_LIMITPD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("N/A") + "</RECVEHUI_LIMITPD>";
						}

						if(stCode.Equals("MI"))
							AcordPolicy.InnerXml = AcordPolicy.InnerXml +  "<COV_UNINSUREDMOTORIST " + fieldType + "=\"" + fieldTypeText + "\">" + "Not Applicable" + "</COV_UNINSUREDMOTORIST>";
						if(stCode.Equals("MI"))
							AcordPolicy.InnerXml = AcordPolicy.InnerXml +  "<UNDERINSURED_MOTORIST " + fieldType + "=\"" + fieldTypeText + "\">" + "Not Applicable" + "</UNDERINSURED_MOTORIST>";


							RowCounter++;
					}
					
				}
				
			}
			#endregion

			#region FORMS AND ENDORSEMENT
			if (DSTempDataSet.Tables[1].Rows.Count > 0 )
			{
				
				if (gStrPdfFor == PDFForDecPage)
				{	
					#region DecPage Element
					XmlElement DecPageFormsEndoElement;
					DecPageFormsEndoElement = AcordPDFXML.CreateElement("FORMSENDORSEMENTINFO");
					RootElementDecPage.AppendChild(DecPageFormsEndoElement);
					DecPageFormsEndoElement.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageFormsEndoElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEFORMENDO"));
					DecPageFormsEndoElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFORMENDO"));
					DecPageFormsEndoElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEFORMENDOEXTN"));
					DecPageFormsEndoElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFORMENDOEXTN"));
					#endregion
					
					RowCounter = 0;
					foreach(DataRow FormsEndo in DSTempDataSet.Tables[1].Rows)
					{
						XmlElement DecPageformEndo;
						DecPageformEndo = AcordPDFXML.CreateElement("FORMSENDO");
						DecPageFormsEndoElement.AppendChild(DecPageformEndo);
						DecPageformEndo.SetAttribute(fieldType,fieldTypeNormal);
						DecPageformEndo.SetAttribute(id,RowCounter.ToString());
					
						DecPageformEndo.InnerXml += "<FORMNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(FormsEndo["FORM_NUMBER"].ToString()) + "</FORMNO>";
						DecPageformEndo.InnerXml += "<EDITIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(FormsEndo["EDITION_DATE"].ToString()) + "</EDITIONDATE>";
						DecPageformEndo.InnerXml += "<DESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(FormsEndo["COV_DES"].ToString()) + "</DESCRIPTION>";
						DecPageformEndo.InnerXml += "<LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + "Included" + "</LIMIT>";
						DecPageformEndo.InnerXml += "<DEDUCTIBLE " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</DEDUCTIBLE>";
						DecPageformEndo.InnerXml += "<PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + "Included" + "</PREMIUM>";
						if(RowCounter == 0)
							DecPageformEndo.InnerXml += "<TOTALPOLICYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GetUmbPremium(DSTempDataSet, "SUMTOTAL")) + "</TOTALPOLICYPREMIUM>";
						//Premium not done...
						#region SWITCH CASE
						switch(FormsEndo["COV_CODE"].ToString())
						{
							
							case "UBPULP":
							{
								
								break;
							}

							case "UBEXFUNG":
							{
								if(stCode.Equals("IN")) 
								{
									XmlElement DecPageUMBEndoUBEXFUNG;
									DecPageUMBEndoUBEXFUNG = AcordPDFXML.CreateElement("UMBENDORSEMENTUBEXFUNG");
									DecPageformEndo.AppendChild(DecPageUMBEndoUBEXFUNG);
									DecPageUMBEndoUBEXFUNG.SetAttribute(fieldType,fieldTypeMultiple);
									DecPageUMBEndoUBEXFUNG.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUBEXFUNG"));
									DecPageUMBEndoUBEXFUNG.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBEXFUNG"));
									DecPageUMBEndoUBEXFUNG.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUBEXFUNGEXTN"));
									DecPageUMBEndoUBEXFUNG.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBEXFUNGEXTN"));
									// nsbisht
									XmlElement EndoElementUBEXFUNG;
									EndoElementUBEXFUNG = AcordPDFXML.CreateElement("ENDOELEMENTUBEXFUNGINFO");
									DecPageUMBEndoUBEXFUNG.AppendChild(EndoElementUBEXFUNG);
									EndoElementUBEXFUNG.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementUBEXFUNG.SetAttribute(id,"0");
								}

								if(stCode.Equals("MI")) 
								{
									XmlElement DecPageUMBEndoUBEXFUNGMI;
									DecPageUMBEndoUBEXFUNGMI = AcordPDFXML.CreateElement("UMBENDORSEMENTUBEXFUNGMI");
									DecPageformEndo.AppendChild(DecPageUMBEndoUBEXFUNGMI);
									DecPageUMBEndoUBEXFUNGMI.SetAttribute(fieldType,fieldTypeMultiple);
									DecPageUMBEndoUBEXFUNGMI.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUBEXFUNGMI"));
									DecPageUMBEndoUBEXFUNGMI.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBEXFUNGMI"));
									DecPageUMBEndoUBEXFUNGMI.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUBEXFUNGMIEXTN"));
									DecPageUMBEndoUBEXFUNGMI.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBEXFUNGMIEXTN"));
									// nsbisht
									XmlElement EndoElementUBEXFUNGMI;
									EndoElementUBEXFUNGMI = AcordPDFXML.CreateElement("ENDOELEMENTUBEXFUNGMIINFO");
									DecPageUMBEndoUBEXFUNGMI.AppendChild(EndoElementUBEXFUNGMI);
									EndoElementUBEXFUNGMI.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementUBEXFUNGMI.SetAttribute(id,"0");
								}

								break;
							}

							case "UBAMPP":
							{
								if(stCode.Equals("MI")) 
								{
									XmlElement DecPageUMBEndoUBAMPP;
									DecPageUMBEndoUBAMPP = AcordPDFXML.CreateElement("UMBENDORSEMENTDecPageUMBEndoUBAMPP");
									DecPageformEndo.AppendChild(DecPageUMBEndoUBAMPP);
									DecPageUMBEndoUBAMPP.SetAttribute(fieldType,fieldTypeMultiple);
									DecPageUMBEndoUBAMPP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUBAMPP"));
									DecPageUMBEndoUBAMPP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBAMPP"));
									DecPageUMBEndoUBAMPP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUBAMPPEXTN"));
									DecPageUMBEndoUBAMPP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBAMPPEXTN"));
									// nsbisht
									XmlElement EndoElementUBAMPP;
									EndoElementUBAMPP = AcordPDFXML.CreateElement("ENDOELEMENTUBAMPPINFO");
									DecPageUMBEndoUBAMPP.AppendChild(EndoElementUBAMPP);
									EndoElementUBAMPP.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementUBAMPP.SetAttribute(id,"0");
								}
								break;
							}

							case "UBAUTLIB":
							{
								break;
							}
							case "UBEXDAE":
							{
								break;
							}

							case "UBDRMV":
							{
								break;
							}

							case "UBDW":
							{
								break;
							}

							case "UBAHAZ":
							{
								break;
							}

							
							case "UBEXIND":
							{
								break;
							}

							case "UBPLJAC":
							{
								break;
							}

							case "UBEXUMOT":
							{

								if(stCode.Equals("IN")) 
								{
									XmlElement DecPageUMBEndoUBEXUMOT;
									DecPageUMBEndoUBEXUMOT = AcordPDFXML.CreateElement("UMBENDORSEMENTUBEXUMOT");
									DecPageformEndo.AppendChild(DecPageUMBEndoUBEXUMOT);
									DecPageUMBEndoUBEXUMOT.SetAttribute(fieldType,fieldTypeMultiple);
									DecPageUMBEndoUBEXUMOT.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUBEXUMOT"));
									DecPageUMBEndoUBEXUMOT.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBEXUMOT"));
									DecPageUMBEndoUBEXUMOT.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUBEXUMOTEXTN"));
									DecPageUMBEndoUBEXUMOT.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBEXUMOTEXTN"));
									// nsbisht
									XmlElement EndoElementUBEXUMOT;
									EndoElementUBEXUMOT = AcordPDFXML.CreateElement("ENDOELEMENTUBEXUMOTINFO");
									DecPageUMBEndoUBEXUMOT.AppendChild(EndoElementUBEXUMOT);
									EndoElementUBEXUMOT.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementUBEXUMOT.SetAttribute(id,"0");
								}
								break;
							}

							case "UBEXUMCOV":
							{
								if(stCode.Equals("IN")) 
								{
									XmlElement DecPageUMBEndoUBEXUMCOV;
									DecPageUMBEndoUBEXUMCOV = AcordPDFXML.CreateElement("UMBENDORSEMENTUBEXUMCOV");
									DecPageformEndo.AppendChild(DecPageUMBEndoUBEXUMCOV);
									DecPageUMBEndoUBEXUMCOV.SetAttribute(fieldType,fieldTypeMultiple);
									DecPageUMBEndoUBEXUMCOV.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUBEXUMCOV"));
									DecPageUMBEndoUBEXUMCOV.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBEXUMCOV"));
									DecPageUMBEndoUBEXUMCOV.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUBEXUMCOVEXTN"));
									DecPageUMBEndoUBEXUMCOV.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUBEXUMCOVEXTN"));
									// nsbisht
									XmlElement EndoElementUBEXUMCOV;
									EndoElementUBEXUMCOV = AcordPDFXML.CreateElement("ENDOELEMENTUBEXUMCOVINFO");
									DecPageUMBEndoUBEXUMCOV.AppendChild(EndoElementUBEXUMCOV);
									EndoElementUBEXUMCOV.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementUBEXUMCOV.SetAttribute(id,"0");
								}
								break;
							}

							case "UBREJUND":
							{
								break;
							}

						}
					#endregion
						
						//DecPageformEndo.InnerXml += "<PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(FormsEndo["POLICY_NUMBER"].ToString()) + "</PREMIUM>";
						
								RowCounter++;
					}
					
				}
				
			}
			#endregion
		}
		#endregion

		#region Automobile/Recreational Vehicles  Xml Generation.
		private void CreateAutoRecVehicleXml()
		{
			
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFUmbrella_AutoRecVehicleDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{

				if (gStrPdfFor==PDFForAcord)
				{	
					#region Acord 83 Page
					XmlElement Acord83AutoRootElement;
					Acord83AutoRootElement = AcordPDFXML.CreateElement("AUTODETAILELEMENT");
					Acord83RootElement.AppendChild(Acord83AutoRootElement);
					Acord83AutoRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					Acord83AutoRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD83AUTO"));
					Acord83AutoRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD83AUTO"));
					Acord83AutoRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD83AUTOEXTN"));
					Acord83AutoRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD83AUTOEXTN"));
					#endregion
					int autoCounter=0;
					foreach(DataRow AutoDetail in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement AcordUmbrellaAutoElement;
						AcordUmbrellaAutoElement = AcordPDFXML.CreateElement("AUTOMOBILEDETAILSINFO");
						Acord83AutoRootElement.AppendChild(AcordUmbrellaAutoElement);
						AcordUmbrellaAutoElement.SetAttribute(fieldType,fieldTypeNormal);
						AcordUmbrellaAutoElement.SetAttribute(id,autoCounter.ToString());

						AcordUmbrellaAutoElement.InnerXml = AcordUmbrellaAutoElement.InnerXml +  "<VEHICLE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "</VEHICLE_NO>";
						AcordUmbrellaAutoElement.InnerXml = AcordUmbrellaAutoElement.InnerXml +  "<VEHICLE_YEAR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) + "</VEHICLE_YEAR>";
						AcordUmbrellaAutoElement.InnerXml = AcordUmbrellaAutoElement.InnerXml +  "<VEHICLE_MAKEMODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AutoDetail["MAKE_MODEL"].ToString()) + "</VEHICLE_MAKEMODEL>";
						
						autoCounter++;
					}
					
				}
			}

			if (DSTempDataSet.Tables[1].Rows.Count > 0 )
			{

				if (gStrPdfFor==PDFForAcord)
				{	
					#region Acord 83 Page
					XmlElement Acord83RecVehRootElement;
					Acord83RecVehRootElement = AcordPDFXML.CreateElement("RECVEHICLEDETAILELEMENT");
					Acord83RootElement.AppendChild(Acord83RecVehRootElement);
					Acord83RecVehRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					Acord83RecVehRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD83RECVEHICLE"));
					Acord83RecVehRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD83RECVEHICLE"));
					Acord83RecVehRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD83RECVEHICLEEXTN"));
					Acord83RecVehRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD83RECVEHICLEEXTN"));
					#endregion
					int RecVehCounter=0;
					foreach(DataRow RecVehDetail in DSTempDataSet.Tables[1].Rows)
					{
						XmlElement AcordUmbrellaRecVehElement;
						AcordUmbrellaRecVehElement = AcordPDFXML.CreateElement("RECVEHICLEDETAILSINFO");
						Acord83RecVehRootElement.AppendChild(AcordUmbrellaRecVehElement);
						AcordUmbrellaRecVehElement.SetAttribute(fieldType,fieldTypeNormal);
						AcordUmbrellaRecVehElement.SetAttribute(id,RecVehCounter.ToString());

						AcordUmbrellaRecVehElement.InnerXml = AcordUmbrellaRecVehElement.InnerXml +  "<RV_NO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(RecVehDetail["COMPANY_ID_NUMBER"].ToString()) + "</RV_NO>";
						AcordUmbrellaRecVehElement.InnerXml = AcordUmbrellaRecVehElement.InnerXml +  "<RV_YEAR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(RecVehDetail["YEAR"].ToString()) + "</RV_YEAR>";
						AcordUmbrellaRecVehElement.InnerXml = AcordUmbrellaRecVehElement.InnerXml +  "<RV_TYPEMAKEMODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(RecVehDetail["TYPEMAKEMODEL"].ToString()) + "</RV_TYPEMAKEMODEL>";
						
						RecVehCounter++;
					}
					
				}
			}

			if (DSTempDataSet.Tables[2].Rows.Count > 0 )
			{

				if (gStrPdfFor==PDFForAcord)
				{	
					#region Acord 83 Page
					XmlElement Acord83UmbWaterCraftRootElement;
					Acord83UmbWaterCraftRootElement = AcordPDFXML.CreateElement("WARTERCRAFTDETAILELEMENT");
					Acord83RootElement.AppendChild(Acord83UmbWaterCraftRootElement);
					Acord83UmbWaterCraftRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					Acord83UmbWaterCraftRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD83WATERCRAFT"));
					Acord83UmbWaterCraftRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD83WATERCRAFT"));
					Acord83UmbWaterCraftRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD83WATERCRAFTEXTN"));
					Acord83UmbWaterCraftRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD83WATERCRAFTEXTN"));
					#endregion
					int watercraftCounter=0;
					//int speed=0;
					foreach(DataRow watercraftDetail in DSTempDataSet.Tables[2].Rows)
					{
						XmlElement AcordUmbrellaWaterCraftElement;
						AcordUmbrellaWaterCraftElement = AcordPDFXML.CreateElement("WATERCRAFTDETAILSINFO");
						Acord83UmbWaterCraftRootElement.AppendChild(AcordUmbrellaWaterCraftElement);
						AcordUmbrellaWaterCraftElement.SetAttribute(fieldType,fieldTypeNormal);
						AcordUmbrellaWaterCraftElement.SetAttribute(id,watercraftCounter.ToString());
						
						AcordUmbrellaWaterCraftElement.InnerXml = AcordUmbrellaWaterCraftElement.InnerXml +  "<BOATN0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(watercraftDetail["BOAT_NO"].ToString()) + "</BOATN0>";
						AcordUmbrellaWaterCraftElement.InnerXml = AcordUmbrellaWaterCraftElement.InnerXml +  "<WAT_YEAR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(watercraftDetail["YEAR"].ToString()) + "</WAT_YEAR>";
						AcordUmbrellaWaterCraftElement.InnerXml = AcordUmbrellaWaterCraftElement.InnerXml +  "<MOTORTYPE_MANUFACTURE_MODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(watercraftDetail["TYPEMAKEMODEL"].ToString()) + "</MOTORTYPE_MANUFACTURE_MODEL>";
						AcordUmbrellaWaterCraftElement.InnerXml = AcordUmbrellaWaterCraftElement.InnerXml +  "<BOAT_LENGTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(watercraftDetail["LENGTH"].ToString()) + "</BOAT_LENGTH>";
						AcordUmbrellaWaterCraftElement.InnerXml = AcordUmbrellaWaterCraftElement.InnerXml +  "<BOAT_HORSEPOWER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(watercraftDetail["WATERCRAFT_HORSE_POWER"].ToString()) + "</BOAT_HORSEPOWER>";
						AcordUmbrellaWaterCraftElement.InnerXml = AcordUmbrellaWaterCraftElement.InnerXml +  "<BOAT_MAXSPEED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(watercraftDetail["MAX_SPEED"].ToString()) + "</BOAT_MAXSPEED>";
						if(watercraftDetail["INSURING_VALUE"].ToString() != ".00")
							AcordUmbrellaWaterCraftElement.InnerXml = AcordUmbrellaWaterCraftElement.InnerXml +  "<VALUE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(watercraftDetail["INSURING_VALUE"].ToString()) + "</VALUE>";
						AcordUmbrellaWaterCraftElement.InnerXml = AcordUmbrellaWaterCraftElement.InnerXml +  "<WATERS_NAVIGATED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(watercraftDetail["WATERS_NAVIGATED"].ToString()) + "</WATERS_NAVIGATED>";
						
						watercraftCounter++;
					}
					
				}
			}

			if (DSTempDataSet.Tables[3].Rows.Count > 0 )
			{

				if (gStrPdfFor==PDFForAcord)
				{	
					#region Acord 83 Page
					XmlElement Acord83UmbOperatorRootElement;
					Acord83UmbOperatorRootElement = AcordPDFXML.CreateElement("OPERATORDETAILELEMENT");
					Acord83RootElement.AppendChild(Acord83UmbOperatorRootElement);
					Acord83UmbOperatorRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					Acord83UmbOperatorRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD83OPERATOR"));
					Acord83UmbOperatorRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD83OPERATOR"));
					Acord83UmbOperatorRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD83OPERATOREXTN"));
					Acord83UmbOperatorRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD83OPERATOREXTN"));
					#endregion
					int operatorCounter=0;
					
					foreach(DataRow operatorDetail in DSTempDataSet.Tables[3].Rows)
					{
						XmlElement AcordUmbrellaOperatorElement;
						AcordUmbrellaOperatorElement = AcordPDFXML.CreateElement("WATERCRAFTDETAILSINFO");
						Acord83UmbOperatorRootElement.AppendChild(AcordUmbrellaOperatorElement);
						AcordUmbrellaOperatorElement.SetAttribute(fieldType,fieldTypeNormal);
						AcordUmbrellaOperatorElement.SetAttribute(id,operatorCounter.ToString());
						string strOprSSn="";
						strOprSSn =  operatorDetail["DRIVER_SSN"].ToString();
						if(strOprSSn !="" && strOprSSn !="0")
						{
							strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(operatorDetail["DRIVER_SSN"].ToString());
						}
						AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<OPERATOR_NO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operatorDetail["DRIVER_ID"].ToString()) + "</OPERATOR_NO>";
						AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<OPERATOR_NAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operatorDetail["OPERATOR_NAME"].ToString()) + "</OPERATOR_NAME>";
						AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<OPERATOR_SEX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operatorDetail["DRIVER_SEX"].ToString()) + "</OPERATOR_SEX>";
						AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<MARITAL_STATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operatorDetail["DRIVER_MART_STAT"].ToString()) + "</MARITAL_STATUS>";
						AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<DATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operatorDetail["DRIVER_DOB"].ToString()) + "</DATEOFBIRTH>";
						AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<DATE_LIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operatorDetail["DATE_LICENSED"].ToString()) + "</DATE_LIC>";
						AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<LIC_STATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operatorDetail["LIC_STATE"].ToString()) + "</LIC_STATE>";
						AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<SOCIAL_SECURITY_NO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strOprSSn) + "</SOCIAL_SECURITY_NO>";
						if(operatorDetail["ASSIGNED_VEHICLE"].ToString() != "0")
							AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<VEHICLE_ASSIGNED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operatorDetail["ASSIGNED_VEHICLE"].ToString()) + "</VEHICLE_ASSIGNED>";
						AcordUmbrellaOperatorElement.InnerXml = AcordUmbrellaOperatorElement.InnerXml +  "<OTHER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operatorDetail["DRIVER_TYPE"].ToString()) + "</OTHER>";
						
						operatorCounter++;
					}
					
				}
			}


		}
		#endregion

		#region Employment detail xml
		private void CreateNamedInsuredCoAppXml()
		{
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{
				if (gStrPdfFor==PDFForAcord)
				{	
					#region Acord 83 Page
					XmlElement Acord83EMPRootElement;
					Acord83EMPRootElement = AcordPDFXML.CreateElement("EMPDETAILELEMENT");
					Acord83RootElement.AppendChild(Acord83EMPRootElement);
					Acord83EMPRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					Acord83EMPRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD83EMP"));
					Acord83EMPRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD83EMP"));
					Acord83EMPRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD83EMPEXTN"));
					Acord83EMPRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD83EMPEXTN"));
					#endregion
					int empCounter=0;
					foreach(DataRow empDetail in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement AcordUmbrellaEMPElement;
						AcordUmbrellaEMPElement = AcordPDFXML.CreateElement("EMPDETAILSINFO");
						Acord83EMPRootElement.AppendChild(AcordUmbrellaEMPElement);
						AcordUmbrellaEMPElement.SetAttribute(fieldType,fieldTypeNormal);
						AcordUmbrellaEMPElement.SetAttribute(id,empCounter.ToString());

						AcordUmbrellaEMPElement.InnerXml = AcordUmbrellaEMPElement.InnerXml +  "<APP_OCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(empDetail["OCCUPATION"].ToString()) + "</APP_OCCUPATION>";
						AcordUmbrellaEMPElement.InnerXml = AcordUmbrellaEMPElement.InnerXml +  "<EMP_NAME_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(empDetail["CO_APPLI_EMPL_NAME"].ToString()+" "+empDetail["CO_APPLI_EMPL_ADD"].ToString()) + "</EMP_NAME_ADDRESS>";
						AcordUmbrellaEMPElement.InnerXml = AcordUmbrellaEMPElement.InnerXml +  "<EMPL_YEARS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(empDetail["YEARSOCCU"].ToString()) + "</EMPL_YEARS>";
						
						empCounter++;
					}
					
				}


			}
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails " + gStrClientID + ",'" + globID + "','"+ gcallFOR + "'");
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{
				if (gStrPdfFor==PDFForAcord)
				{
					XmlElement Acord83PriorElement;
					Acord83PriorElement = AcordPDFXML.CreateElement("PRIORPOLICYINFO");
					Acord83RootElement.AppendChild(Acord83PriorElement);
					Acord83PriorElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord83PriorElement.InnerXml = Acord83PriorElement.InnerXml +  "<PRIOR_CARRIER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CARRIER"].ToString()) + "</PRIOR_CARRIER>";
					Acord83PriorElement.InnerXml = Acord83PriorElement.InnerXml +  "<PRIOR_POLICY_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString()) + "</PRIOR_POLICY_NUMBER>";
				}

			}
			
		}
		#endregion

		#region General Information xml
		private void CreategeneralInformationXml()
		{
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFUmbrella_GeneralInfoDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{
				if (gStrPdfFor==PDFForAcord)
				{	
									
						XmlElement AcordUmbrellaGenInfoElement;
						AcordUmbrellaGenInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
						Acord83RootElement.AppendChild(AcordUmbrellaGenInfoElement);
						AcordUmbrellaGenInfoElement.SetAttribute(fieldType,fieldTypeSingle);
						

						AcordUmbrellaGenInfoElement.InnerXml +=  "<CALCULATIONS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CALCULATIONS"].ToString()) + "</CALCULATIONS>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REMARKS"].ToString()) + "</REMARKS>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<AIRCRAFT_OWNED_LEASED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_AIRCRAFT_OWNED_LEASED"].ToString()) + "</AIRCRAFT_OWNED_LEASED>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<AIRCRAFT_OWNED_LEASED_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_AIRCRAFT_OWNED_LEASED_DESC"].ToString()) + "</AIRCRAFT_OWNED_LEASED_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<OPERATION_CONVICTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OPERATOR_CON_TRAFFIC"].ToString()) + "</OPERATION_CONVICTED>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<OPERATION_CONVICTED_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OPERATOR_CON_TRAFFIC_DESC"].ToString()) + "</OPERATION_CONVICTED_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<PHYSICAL_MENTAL_IMPAIRTMENT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OPERATOR_IMPIRED"].ToString()) + "</PHYSICAL_MENTAL_IMPAIRTMENT>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<PHYSICAL_MENTAL_IMPAIRTMENT_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OPERATOR_IMPIRED_DESC"].ToString()) + "</PHYSICAL_MENTAL_IMPAIRTMENT_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<POOL_TUB_SPA " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_SWIMMING_POOL"].ToString()) + "</POOL_TUB_SPA>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<POOL_TUB_SPA_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_SWIMMING_POOL_DESC"].ToString()) + "</POOL_TUB_SPA_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<USED_COMMERCIALLY_BUSINESS_PURPOSE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REAL_STATE_VEHICLE_USED"].ToString()) + "</USED_COMMERCIALLY_BUSINESS_PURPOSE>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<USED_COMMERCIALLY_BUSINESS_PURPOSE_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REAL_STATE_VEHICLE_USED_DESC"].ToString()) + "</USED_COMMERCIALLY_BUSINESS_PURPOSE_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<NOT_COVERED_BY_PRIMARY_POLICIES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REAL_STATE_VEH_OWNED_HIRED"].ToString()) + "</NOT_COVERED_BY_PRIMARY_POLICIES>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<NOT_COVERED_BY_PRIMARY_POLICIES_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REAL_STATE_VEH_OWNED_HIRED_DESC"].ToString()) + "</NOT_COVERED_BY_PRIMARY_POLICIES_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<FARMING_OPERATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ENGAGED_IN_FARMING"].ToString()) + "</FARMING_OPERATION>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<FARMING_OPERATION_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ENGAGED_IN_FARMING_DESC"].ToString()) + "</FARMING_OPERATION_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<NON_COMPENSATED_POSITIONS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["HOLD_NON_COMP_POSITION"].ToString()) + "</NON_COMPENSATED_POSITIONS>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<NON_COMPENSATED_POSITIONS_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["HOLD_NON_COMP_POSITION_DESC"].ToString()) + "</NON_COMPENSATED_POSITIONS_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<FULL_TIME_EMPLOYEE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_FULL_TIME_EMPLOYEE"].ToString()) + "</FULL_TIME_EMPLOYEE>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<FULL_TIME_EMPLOYEE_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_FULL_TIME_EMPLOYEE_DESC"].ToString()) + "</FULL_TIME_EMPLOYEE_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<NON_OWNED_PROPERTY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NON_OWNED_PROPERTY_CARE"].ToString()) + "</NON_OWNED_PROPERTY>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<NON_OWNED_PROPERTY_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NON_OWNED_PROPERTY_CARE_DESC"].ToString()) + "</NON_OWNED_PROPERTY_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<BUSINESS_PROF_ACTIVITIES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BUSINESS_PROF_ACTIVITY"].ToString()) + "</BUSINESS_PROF_ACTIVITIES>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<BUSINESS_PROF_ACTIVITIES_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BUSINESS_PROF_ACTIVITY_DESC"].ToString()) + "</BUSINESS_PROF_ACTIVITIES_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<REDUCED_LIMIT_LIABILITY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REDUCED_LIMIT_OF_LIBLITY"].ToString()) + "</REDUCED_LIMIT_LIABILITY>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<REDUCED_LIMIT_LIABILITY_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REDUCED_LIMIT_OF_LIBLITY_DESC"].ToString()) + "</REDUCED_LIMIT_LIABILITY_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<COVERAGES_DECLINED_CANCELLED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_COVERAGE_DECLINED"].ToString()) + "</COVERAGES_DECLINED_CANCELLED>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<COVERAGES_DECLINED_CANCELLED_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_COVERAGE_DECLINED_DESC"].ToString()) + "</COVERAGES_DECLINED_CANCELLED_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<APP_TENANT_HAVE_PET_OR_ANIMAL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANIMALS_EXOTIC_PETS"].ToString()) + "</APP_TENANT_HAVE_PET_OR_ANIMAL>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<APP_TENANT_HAVE_PET_OR_ANIMAL_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANIMALS_EXOTIC_PETS_DESC"].ToString()) + "</APP_TENANT_HAVE_PET_OR_ANIMAL_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<INSURANCE_WITHIN_AGENCY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["INSU_TRANSFERED_IN_AGENCY"].ToString()) + "</INSURANCE_WITHIN_AGENCY>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<INSURANCE_WITHIN_AGENCY_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["INSU_TRANSFERED_IN_AGENCY_DESC"].ToString()) + "</INSURANCE_WITHIN_AGENCY_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<LITIGATION_COURT_JUDGEMENTS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PENDING_LITIGATIONS"].ToString()) + "</LITIGATION_COURT_JUDGEMENTS>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<LITIGATION_COURT_JUDGEMENTS_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PENDING_LITIGATIONS_DESC"].ToString()) + "</LITIGATION_COURT_JUDGEMENTS_REMARKS>";
						
						AcordUmbrellaGenInfoElement.InnerXml +=  "<TRAMPOLINE_ON_PREMISES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_TEMPOLINE"].ToString()) + "</TRAMPOLINE_ON_PREMISES>";
						AcordUmbrellaGenInfoElement.InnerXml +=  "<TRAMPOLINE_ON_PREMISES_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_TEMPOLINE_DESC"].ToString()) + "</TRAMPOLINE_ON_PREMISES_REMARKS>";
						
					
					
						XmlElement AcordSupplimentInfoElement;
						AcordSupplimentInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
						SupplementalRootElement.AppendChild(AcordSupplimentInfoElement);
						AcordSupplimentInfoElement.SetAttribute(fieldType,fieldTypeSingle);

						
						AcordSupplimentInfoElement.InnerXml +=  "<APPLICATIONNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ApplicantName) + "</APPLICATIONNAME>";
						AcordSupplimentInfoElement.InnerXml +=  "<APPLICATIONNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(PolicyNumber) + "</APPLICATIONNUMBER>";
						AcordSupplimentInfoElement.InnerXml +=  "<AUTO_CYCLE_TRUCK " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AUTO_CYCL_TRUCKS"].ToString()) + "</AUTO_CYCLE_TRUCK>";
						AcordSupplimentInfoElement.InnerXml +=  "<HOME_RENTALDWELLINGS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["HOME_RENT_DWELL"].ToString()) + "</HOME_RENTALDWELLINGS>";
						AcordSupplimentInfoElement.InnerXml +=  "<RECREATIONAVEHICLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RECR_VEH"].ToString()) + "</RECREATIONAVEHICLE>";
						AcordSupplimentInfoElement.InnerXml +=  "<WATERCRAFT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["WAT_DWELL"].ToString()) + "</WATERCRAFT>";
						
						AcordSupplimentInfoElement.InnerXml +=  "<AUTO_CYCLE_TRUCK_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AUTO_CYCL_TRUCKS_DESC"].ToString()) + "</AUTO_CYCLE_TRUCK_DESC>";
						AcordSupplimentInfoElement.InnerXml +=  "<HOME_RENTALDWELLINGS_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["HOME_RENT_DWELL_DESC"].ToString()) + "</HOME_RENTALDWELLINGS_DESC>";
						AcordSupplimentInfoElement.InnerXml +=  "<RECREATIONAVEHICLE_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RECR_VEH_DESC"].ToString()) + "</RECREATIONAVEHICLE_DESC>";
						AcordSupplimentInfoElement.InnerXml +=  "<WATERCRAFT_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["WAT_DWELL_DESC"].ToString()) + "</WATERCRAFT_DESC>";

						AcordSupplimentInfoElement.InnerXml +=  "<COVERAGEEXPLANATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED"].ToString()) + "</COVERAGEEXPLANATION>";
						AcordSupplimentInfoElement.InnerXml +=  "<COVERAGEEXPLANATION_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC"].ToString()) + "</COVERAGEEXPLANATION_DESC>";
						AcordSupplimentInfoElement.InnerXml +=  "<NONOWNEDPOLICY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["HAVE_NON_OWNED_AUTO_POL_DESC"].ToString()) + "</NONOWNEDPOLICY>";
						AcordSupplimentInfoElement.InnerXml +=  "<DOMICILED_OUTSIDE_MICHIGAN_INDIANA " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["INS_DOMICILED_OUTSIDE_DESC"].ToString()) + "</DOMICILED_OUTSIDE_MICHIGAN_INDIANA>";
						AcordSupplimentInfoElement.InnerXml +=  "<COMPENSATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["HOME_DAY_CARE_DESC"].ToString()) + "</COMPENSATION>";
							
					
				}


			}
			
			
		}
		#endregion
        
		#region Addition Wordings
		private void createAddWordingsXML()
		{
			string lob_id="5";
			string state_id="";

			if(stCode == "IN")
			{
				state_id = "14";
			}
			else if(stCode == "MI")
			{
				state_id = "22";
			}
			if (gStrPdfFor == PDFForDecPage)
			{
				if(gStrProcessID != null && gStrProcessID != "")
				{
					DataSet DSAddWordSet = new DataSet();
					DSAddWordSet = gobjSqlHelper.ExecuteDataSet("Proc_GetAddWordingsData " + state_id + "," + lob_id + "," + gStrProcessID);
			
					if (DSAddWordSet.Tables[0].Rows.Count > 0)
					{
						XmlElement DecAddWordingsElement;
						DecAddWordingsElement = AcordPDFXML.CreateElement("ADDWORDINGS");

						RootElementDecPage.AppendChild(DecAddWordingsElement);
						DecAddWordingsElement.SetAttribute(fieldType,fieldTypeMultiple);
						DecAddWordingsElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDWORDINGS"));
						DecAddWordingsElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDWORDINGS"));
						DecAddWordingsElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEADDWORDINGS"));
						DecAddWordingsElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDWORDINGS"));

						int RowCounter = 0;
						XmlElement DecWordingsInfoElement;
						DecWordingsInfoElement = AcordPDFXML.CreateElement("WORDINGSINFO");
						DecAddWordingsElement.AppendChild(DecWordingsInfoElement);
						DecWordingsInfoElement.SetAttribute(fieldType,fieldTypeNormal);
						DecWordingsInfoElement.SetAttribute(id,RowCounter.ToString());

						DecWordingsInfoElement.InnerXml = DecAddWordingsElement.InnerXml +  "<Wordings_text " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSAddWordSet.Tables[0].Rows[0]["PDF_WORDINGS"].ToString()) + "</Wordings_text>";
					}
				}
			}
		}
		#endregion

		// Added by Mohit Agarwal 13-Dec, ITrack 3211
		#region Page 2
		private void createPage2XML(ref XmlElement DecPage1Element)
		{
			XmlElement DecAddPageElement;
			DecAddPageElement = AcordPDFXML.CreateElement("ADDPAGE2");

			DecPage1Element.AppendChild(DecAddPageElement);
			DecAddPageElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecAddPageElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));

			//int RowCounter = 0;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");

			DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
			DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
			DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
			DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
		}
		#endregion
		
	}
}
