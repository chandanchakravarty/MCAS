/******************************************************************************************
<Author				: -		Pravesh K Chandel	
<Start Date			: -		10-Sep-2009
<End Date			: -		
<Description		: - 	Buisness Layer for Policy Home Pdfs Xml Generation.
<Review Date		: -  
<Reviewed By		: - 	
*******************************************************************************************/ 

using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{

	public class ClsHomePdfXMLs : ClsCommonPdfXML
	{
		#region Declarations
		private XmlElement DecPageRootElement;
		private XmlElement Acord80RootElement;
		private XmlElement Acord81RootElement;
		private XmlElement Acord73RootElement;
		private XmlElement Acord82RootElement;
		private XmlElement SupplementalRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private Hashtable htpremium_dis=new Hashtable(); 
		private Hashtable htpremium_sur=new Hashtable(); 
		//double sumtotal=0;
		string gstrGetPremium="0";
		string goldVewrsionId="0";
		int gintGetindex=0;
		string getPhotoAttach;
		string getMarineSurvey;
		string gstrBoatTerritory;
		bool gCtrlnChkHO65=false;
		private DataWrapper objHomeWrapper;
		private string stCode="";
		//private string strAppAddress1,strAppAddress2,strAppCity,strAppState,strAppZip;
		private string strRVLiabLim = "", strRVMedLim = "";
		private string inspercent = "";
		private string []prnOrd_covCode;
		private string []prnOrd_attFile;
		private int []prnOrd;
		DataSet DsCommonPolicyDetails,DsHomeRiskDetails;
		DataSet DsPolicyDetails;
		DataSet DsApplicantDetails;
		DataSet DsUnderWrittingDetails;
		DataSet DsRVDetails,DsScheduleArticalDetails,DsSolidFuel;
		DataSet DsDwellingDetails,DsAcord81GeneralInfo,DsOtherStructure;
		DataSet DsHomeCoverges,DsHomeAddIntDetails,DsRVAddIntDetails,DsSchArticles;
		DataSet DsBoatDetails;
		DataSet DsBoatCoverages;

		#endregion

		#region Constructor
		public ClsHomePdfXMLs(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string stateCode,string strProcessID,string Agn_Ins,string temp)
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
			gStrtemp=temp;
		}
		#endregion

		public string getHomeAcordPDFXml()
		{
			try
			{
				if (base.objWrapper == null)
					objHomeWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
				else
                    objHomeWrapper = base.objWrapper;

				AcordPDFXML = new XmlDocument();
				AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
				getHomeCommonsPDFsDataSet();
				if(DsPolicyDetails.Tables[0].Rows.Count>0)
				{
					SetPDFVersionLobNode("HOME",DateTime.Parse(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
					SetPDFInsScoresLobNode("HOME",DateTime.Parse(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				}
				createRootElementForAllRootPDFs();
				//LoadRateXML();
				FillMonth();
				//creating Xml From Here
				CreatePolicyAgencyXML();
				CreateNamedInsuredCoAppXml();
				CreatePriorPolicyCoverage();
				CreatePriorLossXml();
				createDwellingXML();
				createAcord80HomeAddlIntXml();
				createDwellingUnderwritingGeneralXML();
				CreateScheduleArticlesXML();
				CreateGeneralInfoXML();

				createWaterCraftXml();
				createBoatXML();
				createAcord82BoatAddlIntXml();
				createAcord82OperatorXML();
				createAcord82OperatorExpViolationXML();
				createBoatUnderwritingGeneralXML();
				CreateRVXML();
				CreateSolidFuelXml();
				if (gStrCopyTo == "CUSTOMER-NOWORD"  &&					
					(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() ||gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
					|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString()
					)
					) 
				{
					RemoveEnorsementWordings();
					createEndorsementWordings();
				}
				if(gStrCopyTo != "AGENCY" && gStrCopyTo != "CUSTOMER-NOWORD")
					createEndorsementWordings();
				if(gStrCopyTo == "CUSTOMER")
					createAddWordingsXML();
				DisposeDataSets();
				return AcordPDFXML.OuterXml;
			}
			catch(Exception ex)
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while generating PDF.");
				addInfo.Add("CustomerID" ,gStrClientID);
				addInfo.Add("PolicyID",gStrPolicyId);
				addInfo.Add("PolicyVersionID",gStrPolicyVersion);
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				DisposeDataSets();
				throw(new Exception("Error while generating PDF.",ex));
			}
		}

		#region getting datasets for Xml
		private DataSet getHomeCommonsPDFsDataSet()
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DsCommonPolicyDetails = objHomeWrapper.ExecuteDataSet("Proc_GetPDFHomeownerCommonDetails");
			DsHomeRiskDetails	  =	objHomeWrapper.ExecuteDataSet("Proc_GetPDFHomeownerRiskDetails");
			objHomeWrapper.ClearParameteres();
			FillDataSets();
			return DsCommonPolicyDetails;
          
		}
		private void FillDataSets()
		{
			//fill Policy Info
			DsPolicyDetails= new DataSet();
			DsPolicyDetails.Tables.Add(DsCommonPolicyDetails.Tables[0].Copy()); 
			//Fill Applicant Info
			DsApplicantDetails= new DataSet();
			DsApplicantDetails.Tables.Add(DsCommonPolicyDetails.Tables[1].Copy()); 
			DsApplicantDetails.Tables.Add(DsCommonPolicyDetails.Tables[2].Copy()); 
			//Fill Underwriting Info
			DsUnderWrittingDetails= new DataSet();
			DsUnderWrittingDetails.Tables.Add(DsCommonPolicyDetails.Tables[3].Copy()); 
			// fill RV Info
			DsRVDetails= new DataSet();
			DsRVDetails.Tables.Add(DsCommonPolicyDetails.Tables[4].Copy());
			DsRVDetails.Tables.Add(DsCommonPolicyDetails.Tables[5].Copy());
			// Fill schedule Articles info
			DsScheduleArticalDetails= new DataSet();
			DsScheduleArticalDetails.Tables.Add(DsCommonPolicyDetails.Tables[6].Copy());
			DsScheduleArticalDetails.Tables.Add(DsCommonPolicyDetails.Tables[7].Copy()); 
			// Fill Solid Fuel Info 
			DsSolidFuel= new DataSet();
			DsSolidFuel.Tables.Add(DsCommonPolicyDetails.Tables[8].Copy()); 
			//fill DsAcord81GeneralInfo
			DsAcord81GeneralInfo = new DataSet();
			DsAcord81GeneralInfo.Tables.Add(DsCommonPolicyDetails.Tables[9].Copy()); 
			
			//Filling Risk dependends Info
			//fill Dwelling info
			DsDwellingDetails = new DataSet();
			DsDwellingDetails.Tables.Add(DsHomeRiskDetails.Tables[0].Copy()); 
			//Fill other structure info
			DsOtherStructure = new DataSet();
			DsOtherStructure.Tables.Add(DsHomeRiskDetails.Tables[1].Copy()); 
			// fill risk Coverags
			DsHomeCoverges = new DataSet();
			DsHomeCoverges.Tables.Add(DsHomeRiskDetails.Tables[2].Copy()); 
			DsHomeCoverges.Tables.Add(DsHomeRiskDetails.Tables[3].Copy()); 
			// get Home additional Info
			DsHomeAddIntDetails= new DataSet();
			DsHomeAddIntDetails.Tables.Add(DsHomeRiskDetails.Tables[4].Copy()); 
			// get RV additional Info
			DsRVAddIntDetails= new DataSet();
			DsRVAddIntDetails.Tables.Add(DsHomeRiskDetails.Tables[5].Copy()); 
			// get Schedule Articlle Info
			DsSchArticles= new DataSet();
			DsSchArticles.Tables.Add(DsHomeRiskDetails.Tables[6].Copy()); 
			//DsCommonPolicyDetails.Dispose();
			//DsHomeRiskDetails.Dispose();
		}


		private void DisposeDataSets()
		{
			//if(DsCommonPolicyDetails!=null) DsCommonPolicyDetails.Dispose();
			if(DsPolicyDetails!=null) DsPolicyDetails.Dispose();
			if(DsApplicantDetails!=null) DsApplicantDetails.Dispose();
			if(DsUnderWrittingDetails!=null) DsUnderWrittingDetails.Dispose();
			if(DsRVDetails!=null) DsRVDetails.Dispose();
			if(DsScheduleArticalDetails!=null) DsScheduleArticalDetails.Dispose();
			if(DsSolidFuel!=null) DsSolidFuel.Dispose();
			if(DsDwellingDetails!=null) DsDwellingDetails.Dispose();
			if(DsHomeCoverges!=null) DsHomeCoverges.Dispose();
			if(DsBoatDetails!=null) DsBoatDetails.Dispose();
			if(DsBoatCoverages!=null) DsBoatCoverages.Dispose();
			if(DsAcord81GeneralInfo!=null) DsAcord81GeneralInfo.Dispose();
			if(DsOtherStructure!=null) DsOtherStructure.Dispose();
			if(DsHomeAddIntDetails!=null) DsHomeAddIntDetails.Dispose();
			if(DsRVAddIntDetails!=null) DsRVAddIntDetails.Dispose();
			if(DsSchArticles!=null) DsSchArticles.Dispose();
		}


		private DataView getHomeOtherStructureDataSet(string RiskID)
		{
//			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures");
			DataView dv = new DataView(DsOtherStructure.Tables[0],"DWELLING_ID=" + RiskID,"DWELLING_ID" ,DataViewRowState.CurrentRows);
			return dv;
		}

		private DataSet getHomeCoveragesDataSet(string StrCustomerID,string StrPolicyId,string StrPolicyVersion, string DwellingID)
		{
//			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
			DataView dv = new DataView(DsHomeCoverges.Tables[0],"DWELLING_ID=" + DwellingID,"DWELLING_ID" ,DataViewRowState.CurrentRows);
			DataSet ds= new DataSet();
			ds.Tables.Add(dv.Table.Copy());
			dv.Dispose();
			ds.Tables.Add(DsHomeCoverges.Tables[1].Copy());
			return ds;
		}
		private DataSet getHomeAddInterestDataSet(string RiskID)
		{
//			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
			if(RiskID!="0")
			{
				DataView dv = new DataView(DsHomeAddIntDetails.Tables[0],"DWELLING_ID=" + RiskID,"DWELLING_ID" ,DataViewRowState.CurrentRows);
				DataSet ds= new DataSet();
				ds.Tables.Add(dv.Table.Copy());
				dv.Dispose();
				return ds;
			}
			else
			return DsHomeAddIntDetails;

		}
		private DataSet getPriorPolicyAndLossDataSet(string strDataFor)
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@LOBCODE","HOME");
			objHomeWrapper.AddParameter("@DATAFOR",strDataFor);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}

		
		private DataSet getRVAdditionalInterestDataSet(string rvID)
		{
//			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFRVAdditionalInterests");
			DataView dv = new DataView(DsRVAddIntDetails.Tables[0],"REC_VEH_ID=" + rvID,"REC_VEH_ID" ,DataViewRowState.CurrentRows);
			DataSet ds= new DataSet();
			ds.Tables.Add(dv.Table.Copy());
			dv.Dispose();
			return ds;
		}

		private DataSet getHomeScheduleArticleDetailDataSet(string itemID)
		{
//			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("PROC_GETPDF_HOME_SCHEDULE_ARTICLES_DETAILS");
			DataView dv = new DataView(DsSchArticles.Tables[0],"ITEM_ID=" + itemID,"ITEM_ID" ,DataViewRowState.CurrentRows);
			DataSet ds= new DataSet();
			ds.Tables.Add(dv.Table.Copy());
			dv.Dispose();
			return ds;
		}
		private DataSet getBoatOperatorExpDtlsDataSet()
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFOperatorExpDtls");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}
		private DataSet getBoatEngineDetailDataSet(string RiskID)
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@BOATID",RiskID);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}	
		private DataSet getOperatorDataSet()
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFOperatorDtls");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}
		private DataSet getBoatUnderWrittingDataSet()
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFUnderwritingDetails");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}
		private DataSet getBoatDetailsDataSet()
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}
		private DataSet getBoatCoveragesDataSet(string RiskID)
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@BOATID",RiskID);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}
		private DataSet getSchEquipmentsDataSet(string RiskID)
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@BOATID",RiskID);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}
		private DataSet getBoatTrailerDetailsDataSet(string RiskID)
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@BOATID",RiskID);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}
		private DataSet getBoatAddInterestDataSet(string RiskID)
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objHomeWrapper.AddParameter("@POLID",gStrPolicyId);
			objHomeWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			objHomeWrapper.AddParameter("@BOATID",RiskID);
			objHomeWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}
		private DataSet getAddWordingDataSet(string stateId,string LobId,string ProcessId)
		{
			objHomeWrapper.ClearParameteres();
			objHomeWrapper.AddParameter("@STATE_ID",stateId);
			objHomeWrapper.AddParameter("@LOB_ID",LobId);
			objHomeWrapper.AddParameter("@PROCESS_ID",ProcessId);
			DataSet DSTempDataSet = objHomeWrapper.ExecuteDataSet("Proc_GetAddWordingsData");
			objHomeWrapper.ClearParameteres();
			return DSTempDataSet;
		}

		private string getDecriptSSN(string strEncriptSSN)
		{
			string strDecriptSSN = "";
			if(strEncriptSSN !="" && strEncriptSSN !="0")
			{
				strDecriptSSN = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(strEncriptSSN);
				if(strDecriptSSN.Trim()!="")
				{
					string strvaln = "xxx-xx-";
					strvaln += strDecriptSSN.Substring(strvaln.Length, strDecriptSSN.Length - strvaln.Length);
					strDecriptSSN = strvaln;
				}
				else
					strDecriptSSN="";
			}
			return strDecriptSSN.Trim();
		}

		#endregion
		#region To Create Root Element For All Root PDFs
		private void createRootElementForAllRootPDFs()
		{
			if (gStrPdfFor == PDFForDecPage)
			{
				DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGE"));
			}
			else if (gStrPdfFor == PDFForAcord)
			{
				Acord80RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord80RootElement);

				Acord80RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80"));
			
				Acord81RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord81RootElement);

				Acord81RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD81"));

				Acord73RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord73RootElement);

				Acord73RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD73"));

				Acord82RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord82RootElement);

				Acord82RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82"));

				SupplementalRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(SupplementalRootElement);

				SupplementalRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENT"));
			}
		}
		#endregion

		#region Creating Policy And Agency Xml 
		private void CreatePolicyAgencyXML()
		{
			#region Global Variable Assignment
			PolicyNumber	= RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["POLICY_NUMBER"].ToString());
			PolicyEffDate	= RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
			PolicyExpDate	= RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
			if(gStrProcessID != null && gStrProcessID != "")
			{
				DataSet DSAddWordSet = new DataSet();
				DSAddWordSet =getAddWordingDataSet("0","0",gStrProcessID);
			
				if (DSAddWordSet.Tables.Count > 1 && DSAddWordSet.Tables[1].Rows.Count > 0)
					Reason		=	RemoveJunkXmlCharacters(DSAddWordSet.Tables[1].Rows[0]["PROCESS_DESC"].ToString());
			}
			else
				Reason			= RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["Reason"].ToString());
			goldVewrsionId=DsPolicyDetails.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
			//		CopyTo			= RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["COPY_TO"].ToString());
			if(Reason.Trim() != "" && DsPolicyDetails.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
				Reason += " / Effective Date: " + DsPolicyDetails.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
			AgencyName = RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString());
			AgencyAddress = RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_ADD"].ToString());
			AgencyCitySTZip = RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString());
			AgencyPhoneNumber = RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_PHONE"].ToString());
			AgencyCode = RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CODE"].ToString());
			AgencySubCode = RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["SUB_CODE"].ToString());
			AgencyBilling = RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PAYMENTBILLING"].ToString())=="AB"?"Agency Bill":"Direct Bill" ;
			currTerm = int.Parse(DsPolicyDetails.Tables[0].Rows[0]["CURRENT_TERM"].ToString());
			if(currTerm > 1)
				applyInsScore = int.Parse(DsPolicyDetails.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString());
			#endregion

			if (gStrPdfFor == PDFForAcord)
			{
				#region Acord80 Page
				XmlElement AcordPolicyElement;
				AcordPolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord80RootElement.AppendChild(AcordPolicyElement);
				AcordPolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</DATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVEDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATIONDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDEREFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</BINDEREFFDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDEREXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</BINDEREXPDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDERCVGNOTBOUND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["BINDERCVGNOTBOUND"].ToString()) + "</BINDERCVGNOTBOUND>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTBILLING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PAYMENTBILLING"].ToString()) + "</PAYMENTBILLING>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTDIRECTBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PAYMENTDIRECTBILL"].ToString()) + "</PAYMENTDIRECTBILL>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()) + "</PAYMENTAPPBILL>";
				if(DsPolicyDetails.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString() != "0")
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<DATECURRRESIDENCE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString()) + "</DATECURRRESIDENCE>";
				if(DsPolicyDetails.Tables[0].Rows[0]["POLICY_TYPE"].ToString()!="")
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine Mutual/" + DsPolicyDetails.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</APPLICANTCOPLAN>";

				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APPLICANTPREVIOUSADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("" + DsPolicyDetails.Tables[0].Rows[0]["YEARS_AT_PREV_ADD"].ToString()) + "</APPLICANTPREVIOUSADDRESS>";
				//Agency
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCYPHONE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYFAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCYFAX>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";
				//ATTACHMENT INFO
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTCHINLANDMARINE " + fieldType + "=\"" + fieldTypeText + "\"></ATTCHINLANDMARINE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTCHREPLCOSTEST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PROPRTY_INSP_CREDIT"].ToString()) + "</ATTCHREPLCOSTEST>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTCHPHOTOGRAPH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PIC_OF_LOC"].ToString()) + "</ATTCHPHOTOGRAPH>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTACHSOLIDFUEL " + fieldType + "=\"" + fieldTypeText + "\"></ATTACHSOLIDFUEL>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTACHPROTDEVICE " + fieldType + "=\"" + fieldTypeText + "\"></ATTACHPROTDEVICE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTACHRV " + fieldType + "=\"" + fieldTypeText + "\"></ATTACHRV>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTACHBOAT " + fieldType + "=\"" + fieldTypeText + "\"></ATTACHBOAT>";

				#endregion

				#region Acord81 Page
				XmlElement Acord81PolicyElement;
				Acord81PolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord81RootElement.AppendChild(Acord81PolicyElement);
				Acord81PolicyElement.SetAttribute(fieldType,fieldTypeSingle);

				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</DATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<EFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVEDATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<EXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATIONDATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDEREFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</BINDEREFFECTIVEDATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDEREXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</BINDEREXPIRATIONDATE>";
				//				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDEREFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["BINDEREFFDATE"].ToString()) + "</BINDEREFFDATE>";
				//				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDEREXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["BINDEREXPDATE"].ToString()) + "</BINDEREXPDATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDERCVGNOTBOUND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["BINDERCVGNOTBOUND"].ToString()) + "</BINDERCVGNOTBOUND>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<PAYMENTBILLING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PAYMENTBILLING"].ToString()) + "</PAYMENTBILLING>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine/" + DsPolicyDetails.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</APPLICANTCOPLAN>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<PAYMENTPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString()) + "</PAYMENTPLAN>";
				//Agency
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCYPHONE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYFAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCYFAX>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";
			
				#endregion

				#region Acord73 Page
				XmlElement Acord73PolicyElement;
				Acord73PolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord73RootElement.AppendChild(Acord73PolicyElement);
				Acord73PolicyElement.SetAttribute(fieldType,fieldTypeSingle);

				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</DATE>";
				
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<EFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVEDATE>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<EXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATIONDATE>";
				//Agency
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";
				#endregion

				#region Acord82 Page
				XmlElement Acord82PolicyElement;
				Acord82PolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord82RootElement.AppendChild(Acord82PolicyElement);
				Acord82PolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</DATE>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<APPLICANTPOLNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</APPLICANTPOLNUM>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<APPLICANTEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</APPLICANTEFFDATE>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<APPLICANTEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</APPLICANTEXPDATE>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<BINDEREFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</BINDEREFFDATE>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<BINDEREXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</BINDEREXPDATE>";
				if(DsPolicyDetails.Tables[0].Rows[0]["BINDERCVGNOTBOUND"].ToString() == "1")
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<BINDERCVGNOTBOUND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Y") + "</BINDERCVGNOTBOUND>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<PAYMENTBILLING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PAYMENTBILLING"].ToString()) + "</PAYMENTBILLING>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<PAYMENTDIRECTBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PAYMENTDIRECTBILL"].ToString()) + "</PAYMENTDIRECTBILL>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()) + "</PAYMENTAPPBILL>";
				//Agency
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCYPHONE>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYFAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCYFAX>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsPolicyDetails.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";
				Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine/" + DsPolicyDetails.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</APPLICANTCOPLAN>";

				#endregion
			}
		}
		#endregion

		#region Creating Named Insured And CoApplicant Info
		private void CreateNamedInsuredCoAppXml()
		{
			if (DsApplicantDetails.Tables[0].Rows.Count > 0 )
			{
				#region Global Variable Assignment
				ApplicantName = RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["APPNAME"].ToString());
				ApplicantAddress = RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTADDRESS"].ToString());
				ApplicantCityStZip = RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString());

				reason_code1 = RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTOMER_REASON_CODE"].ToString());
				reason_code2 = RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTOMER_REASON_CODE2"].ToString());
				reason_code3 = RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTOMER_REASON_CODE3"].ToString());
				reason_code4 = RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTOMER_REASON_CODE4"].ToString());
				if(currTerm <= 1)
				{
					if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, DsApplicantDetails.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
						strNeedPage2 = "Y";
					else
						strNeedPage2 = "N";
				}
				else
				{
					if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, applyInsScore.ToString()))
						strNeedPage2 = "Y";
					else
						strNeedPage2 = "N";
				}

				#endregion
                
				if (gStrPdfFor == PDFForAcord)
				{
					#region Acord 82 Page
					XmlElement Acord82NamedInsuredElement;
					Acord82NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord82RootElement.AppendChild(Acord82NamedInsuredElement);
					Acord82NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTHOMEPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["PHONE"].ToString()) + "</APPLICANTHOMEPHONE>";

					#endregion

					#region Acord 73 Page
					XmlElement Acord73NamedInsuredElement;
					Acord73NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord73RootElement.AppendChild(Acord73NamedInsuredElement);
					Acord73NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord73NamedInsuredElement.InnerXml = Acord73NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord73NamedInsuredElement.InnerXml = Acord73NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord73NamedInsuredElement.InnerXml = Acord73NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					#endregion
				
					#region Acord 81 Page
					XmlElement Acord81NamedInsuredElement;
					Acord81NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord81RootElement.AppendChild(Acord81NamedInsuredElement);
					Acord81NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTBUSSPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString()) + "</APPLICANTBUSSPHONE>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["DOB"].ToString()) + "</APPLICANTDOB>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTMARITALSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["MARTSTATUSCODE"].ToString()) + "</APPLICANTMARITALSTATUS>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["OCCUPATION"].ToString()) + "</APPLICANTOCCUPATION>";
					if (DsApplicantDetails.Tables[0].Rows.Count > 1)
						Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<SPOUSEOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[1]["OCCUPATION"].ToString()) + "</SPOUSEOCCUPATION>";
					else
						Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<SPOUSEOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\"></SPOUSEOCCUPATION>";

					#endregion

					#region Acord 80 Page

					int RowCounter=0;
					string strCoappSSn="";
					XmlElement Acord80NamedInsuredElement;
					Acord80NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord80RootElement.AppendChild(Acord80NamedInsuredElement);
					Acord80NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTHOMEPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["PHONE"].ToString()) + "</APPLICANTHOMEPHONE>";
					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTBUSSPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString()) + "</APPLICANTBUSSPHONE>";
			
					XmlElement AddlCoApplicantElement;
					AddlCoApplicantElement = AcordPDFXML.CreateElement("ADDITIONALCOAPPLICANTS");
					Acord80RootElement.AppendChild(AddlCoApplicantElement);
					AddlCoApplicantElement.SetAttribute(fieldType,fieldTypeMultiple);
					AddlCoApplicantElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80COAPP"));
					AddlCoApplicantElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80COAPP"));
					AddlCoApplicantElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80COAPPEXTN"));
					AddlCoApplicantElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80COAPPEXTN"));
 
					if(DsApplicantDetails.Tables[0].Rows.Count == 1)
					{
						XmlElement AppElement;
						AppElement =  AcordPDFXML.CreateElement("APPLICANT");
						AddlCoApplicantElement.AppendChild(AppElement);
						AppElement.SetAttribute(fieldType,fieldTypeNormal);
						AppElement.SetAttribute(id,(RowCounter).ToString());

						AppElement.InnerXml = AppElement.InnerXml +  "<ADDLPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</ADDLPOLICYNUMBER>";
						AppElement.InnerXml = AppElement.InnerXml +  "<ADDLAPPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</ADDLAPPLICANTNAME>";
						AppElement.InnerXml = AppElement.InnerXml +  "<NAMEINSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</NAMEINSUREDNAME>";
						AppElement.InnerXml = AppElement.InnerXml +  "<NAMEINSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</NAMEINSUREDADDRESS>";
						AppElement.InnerXml = AppElement.InnerXml +  "<NAMEINSUREDCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</NAMEINSUREDCITYSTATEZIP>";
						// to be changed
						DataSet DSDwellDataSet = DsDwellingDetails;
						foreach(DataRow Dwellrow in DSDwellDataSet.Tables[0].Rows)
						{
							if(ApplicantAddress.Trim() != Dwellrow["LOC_ADDRESS"].ToString().Trim() || ApplicantCityStZip.Trim() != Dwellrow["LOC_CITYSTATEZIP"].ToString().Trim())
							{
								AppElement.InnerXml = AppElement.InnerXml +  "<LOCATIONADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_ADDRESS"].ToString()) + "</LOCATIONADD>";
								AppElement.InnerXml = AppElement.InnerXml +  "<LOCATIONADD2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_CITYSTATEZIP"].ToString()) + "</LOCATIONADD2>";
								break;
							}
						}
						strCoappSSn = getDecriptSSN(DsApplicantDetails.Tables[0].Rows[RowCounter]["SSN"].ToString());
						AppElement.InnerXml = AppElement.InnerXml +  "<APPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["OCCUPATION"].ToString()) + "</APPOCCUPATION>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["APPNAME"].ToString()) + "</APPEMPLOYEENAME>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["YEARSEMPL"].ToString()) + "</APPYEAREMPL>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["MARTSTATUS"].ToString()) + "</APPMARTSTATUS>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["DOB"].ToString()) + "</APPDATEOFBIRTH>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoappSSn) + "</APPSSN>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPEMPLOYERNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_NAME"].ToString()) + "</APPEMPLOYERNAME>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPEMPLOYERADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_ADD"].ToString()) + "</APPEMPLOYERADD>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPYEAROCCU " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["YEARSOCCU"].ToString()) + "</APPYEAROCCU>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPMARTSTATUSCD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["MARTSTATUSCODE"].ToString()) + "</APPMARTSTATUSCD>";
					}

					for(RowCounter=1;RowCounter<DsApplicantDetails.Tables[0].Rows.Count;RowCounter++)
					{
						XmlElement CoAppElement;
						CoAppElement =  AcordPDFXML.CreateElement("COAPPLICANT");
						AddlCoApplicantElement.AppendChild(CoAppElement);
						CoAppElement.SetAttribute(fieldType,fieldTypeNormal);
						CoAppElement.SetAttribute(id,(RowCounter-1).ToString());

						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<ADDLPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</ADDLPOLICYNUMBER>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<ADDLAPPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</ADDLAPPLICANTNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</NAMEINSUREDNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</NAMEINSUREDADDRESS>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</NAMEINSUREDCITYSTATEZIP>";

						if(RowCounter == 1)
						{
							// to changed
							DataSet DSDwellDataSet = DsDwellingDetails;
							foreach(DataRow Dwellrow in DSDwellDataSet.Tables[0].Rows)
							{
								if(ApplicantAddress.Trim() != Dwellrow["LOC_ADDRESS"].ToString().Trim() || ApplicantCityStZip.Trim() != Dwellrow["LOC_CITYSTATEZIP"].ToString().Trim())
								{
									CoAppElement.InnerXml = CoAppElement.InnerXml +  "<LOCATIONADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_ADDRESS"].ToString()) + "</LOCATIONADD>";
									CoAppElement.InnerXml = CoAppElement.InnerXml +  "<LOCATIONADD2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_CITYSTATEZIP"].ToString()) + "</LOCATIONADD2>";
									break;
								}
							}
							strCoappSSn=getDecriptSSN(DsApplicantDetails.Tables[0].Rows[0]["SSN"].ToString());
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["OCCUPATION"].ToString()) + "</APPOCCUPATION>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPEMPLOYEENAME>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["YEARSEMPL"].ToString()) + "</APPYEAREMPL>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["MARTSTATUS"].ToString()) + "</APPMARTSTATUS>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["DOB"].ToString()) + "</APPDATEOFBIRTH>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoappSSn) + "</APPSSN>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPEMPLOYERNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CO_APPLI_EMPL_NAME"].ToString()) + "</APPEMPLOYERNAME>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPEMPLOYERADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["CO_APPLI_EMPL_ADD"].ToString()) + "</APPEMPLOYERADD>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPYEAROCCU " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["YEARSOCCU"].ToString()) + "</APPYEAROCCU>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPMARTSTATUSCD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[0]["MARTSTATUSCODE"].ToString()) + "</APPMARTSTATUSCD>";
						}
						strCoappSSn=getDecriptSSN(DsApplicantDetails.Tables[0].Rows[RowCounter]["SSN"].ToString());
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["OCCUPATION"].ToString()) + "</COAPPOCCUPATION>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["APPNAME"].ToString()) + "</COAPPEMPLOYEENAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["YEARSEMPL"].ToString()) + "</COAPPYEAREMPL>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["MARTSTATUS"].ToString()) + "</COAPPMARTSTATUS>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["DOB"].ToString()) + "</COAPPDATEOFBIRTH>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoappSSn) + "</COAPPSSN>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYERNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_NAME"].ToString()) + "</COAPPEMPLOYERNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYERADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_ADD"].ToString()) + "</COAPPEMPLOYERADD>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAROCCU " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["YEARSOCCU"].ToString()) + "</COAPPYEAROCCU>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPMARTSTATUSCD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DsApplicantDetails.Tables[0].Rows[RowCounter]["MARTSTATUSCODE"].ToString()) + "</COAPPMARTSTATUSCD>";
					}
					#endregion
				}
			}
		}
		#endregion

		#region Creating Prior Policy/Coverage XML
		private void CreatePriorPolicyCoverage()
		{
				DataSet DSPriorPolicyLossDetail			;
			if (gStrPdfFor == PDFForAcord)
			{
				DSPriorPolicyLossDetail= getPriorPolicyAndLossDataSet("POLICY");
				if (DSPriorPolicyLossDetail.Tables[0].Rows.Count>0)
				{
					#region Acord 80 Page
			
					XmlElement Acord80PriorPolicyElement;
					Acord80PriorPolicyElement = AcordPDFXML.CreateElement("PRIORPOLICYCOVERAGE");
					Acord80RootElement.AppendChild(Acord80PriorPolicyElement);
					Acord80PriorPolicyElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord80PriorPolicyElement.InnerXml = Acord80PriorPolicyElement.InnerXml +  "<PRIORCARRIER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSPriorPolicyLossDetail.Tables[0].Rows[0]["CARRIER"].ToString()) + "</PRIORCARRIER>";
					Acord80PriorPolicyElement.InnerXml = Acord80PriorPolicyElement.InnerXml +  "<PRIORPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSPriorPolicyLossDetail.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString()) + "</PRIORPOLICYNUMBER>";
					Acord80PriorPolicyElement.InnerXml = Acord80PriorPolicyElement.InnerXml +  "<PRIOREXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSPriorPolicyLossDetail.Tables[0].Rows[0]["EFF_DATE"].ToString()) + "</PRIOREXPIRATIONDATE>";
					#endregion
				}
			}
		}
		#endregion

		#region Creating Prior Loss XML
		private void CreatePriorLossXml()
		{
			DataSet DsPriorLoss			;
			if (gStrPdfFor == PDFForAcord)
			{
				DsPriorLoss = getPriorPolicyAndLossDataSet("LOSS");
				if (DsPriorLoss.Tables[0].Rows.Count>0)
				{
					XmlElement Acord80PriorLoss;
					Acord80PriorLoss = AcordPDFXML.CreateElement("PRIORLOSSCOVERAGE");
					Acord80RootElement.AppendChild(Acord80PriorLoss);
					Acord80PriorLoss.SetAttribute(fieldType,fieldTypeMultiple);
					Acord80PriorLoss.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80LOSSHIST"));
					Acord80PriorLoss.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80LOSSHIST"));
					Acord80PriorLoss.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80LOSSHISTEXTN"));
					Acord80PriorLoss.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80LOSSHISTEXTN"));

					int RowCounter=0;
					foreach (DataRow Row in DsPriorLoss.Tables[0].Rows)
					{
						XmlElement Acord80PriorLossElement;
						Acord80PriorLossElement =  AcordPDFXML.CreateElement("COAPPLICANT");
						Acord80PriorLoss.AppendChild(Acord80PriorLossElement);
						Acord80PriorLossElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord80PriorLossElement.SetAttribute(id,RowCounter.ToString());

						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<PRIORLOSSENTERED " + fieldType + "=\"" + fieldTypeText + "\">Y</PRIORLOSSENTERED>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</APPLICANTNAME>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<LOSSHISTDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OCCURENCE_DATE"].ToString()) + "</LOSSHISTDATE>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<LOSSHISTTYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOSS_TYPE"].ToString()) + "</LOSSHISTTYPE>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<LOSSHISTDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOSS_DESC"].ToString()) + "</LOSSHISTDESCRIPTION>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<LOSSHISTAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["AMOUNT"].ToString()) + "</LOSSHISTAMOUNT>";
						RowCounter++;
					}

				}
			}
		}
		#endregion

		#region Creating Solid Fuel Xml
		private void CreateSolidFuelXml()
		{
			DataSet DsSolidFuelDetail;
			if (gStrPdfFor == PDFForAcord)
			{
				DsSolidFuelDetail = DsSolidFuel;
				if (DsSolidFuelDetail.Tables[0].Rows.Count > 0) 
				{
					Acord80RootElement.SelectSingleNode("POLICY/ATTACHSOLIDFUEL").InnerText = "1";

					XmlElement Acord73SolidFuel;
					Acord73SolidFuel = AcordPDFXML.CreateElement("SOLIDFUEL");
					Acord73RootElement.AppendChild(Acord73SolidFuel);
					Acord73SolidFuel.SetAttribute(fieldType,fieldTypeMultiple);
					Acord73SolidFuel.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD73SOLIDFUEL"));
					Acord73SolidFuel.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD73SOLIDFUEL"));
					Acord73SolidFuel.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD73SOLIDFUELEXTN"));
					Acord73SolidFuel.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD73SOLIDFUELEXTN"));
					int RowCounter=0;
					foreach (DataRow Row in DsSolidFuelDetail.Tables[0].Rows)
					{
						XmlElement Acord73SolidFuelElement;
						Acord73SolidFuelElement =  AcordPDFXML.CreateElement("SOLIDFUELINFO");
						Acord73SolidFuel.AppendChild(Acord73SolidFuelElement);
						Acord73SolidFuelElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord73SolidFuelElement.SetAttribute(id,RowCounter.ToString());

						//Solid Fuel Details
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_MANUFACTURER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MANUFACTURER"].ToString()) + "</SFD_MANUFACTURER>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_BRANDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["BRAND_NAME"].ToString()) + "</SFD_BRANDNAME>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_MODELNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MODEL_NUMBER"].ToString()) + "</SFD_MODELNUM>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_FUEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["FUEL"].ToString()) + "</SFD_FUEL>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_STOVETYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_TYPE"].ToString()) + "</SFD_STOVETYPE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_LABTESTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HAVE_LABORATORY_LABEL"].ToString()) + "</SFD_LABTESTED>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_ISTHEUNIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_UNIT"].ToString()) + "</SFD_ISTHEUNIT>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_ISTHEUNITOTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["UNIT_OTHER_DESC"].ToString()) + "</SFD_ISTHEUNITOTHDESC>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_CONSTR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CONSTRUCTION"].ToString()) + "</SFD_CONSTR>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_LOCATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOCATION"].ToString()) + "</SFD_LOCATION>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_LOCATIONOTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOC_OTHER_DESC"].ToString()) + "</SFD_LOCATIONOTHDESC>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_YEARDEVINST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["YEAR_DEVICE_INSTALLED"].ToString()) + "</SFD_YEARDEVINST>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_INSTALLDONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["WAS_PROF_INSTALL_DONE"].ToString()) + "</SFD_INSTALLDONE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_INSTINSPBY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["INSTALL_INSPECTED_BY"].ToString()) + "</SFD_INSTINSPBY>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_INSTINSPBYOTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["INSTALL_OTHER_DESC"].ToString()) + "</SFD_INSTINSPBYOTHDESC>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_HEATUSE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HEATING_USE"].ToString()) + "</SFD_HEATUSE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_HEATTYPEUSE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HEATING_SOURCE"].ToString()) + "</SFD_HEATTYPEUSE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_HEATTYPEUSEOTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OTHER_DESC"].ToString()) + "</SFD_HEATTYPEUSEOTHDESC>";
						//Chimney
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_STOVEVENTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_STOVE_VENTED"].ToString()) + "</CHIM_STOVEVENTED>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHMOTHERATTCH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OTHER_DEVICES_ATTACHED"].ToString()) + "</CHMOTHERATTCH>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHMCONSTR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CHIMNEY_CONSTRUCTION"].ToString()) + "</CHMCONSTR>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHMCONSTROTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CONSTRUCT_OTHER_DESC"].ToString()) + "</CHMCONSTROTHDESC>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_TILEFLUELINE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_TILE_FLUE_LINING"].ToString()) + "</CHIM_TILEFLUELINE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_FROMGNDUP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_CHIMNEY_GROUND_UP"].ToString()) + "</CHIM_FROMGNDUP>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_AFTERHSEBLT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CHIMNEY_INST_AFTER_HOUSE_BLT"].ToString()) + "</CHIM_AFTERHSEBLT>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_COVCOMWALL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_CHIMNEY_COVERED"].ToString()) + "</CHIM_COVCOMWALL>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_COVCOMWALLDIST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DIST_FROM_SMOKE_PIPE"].ToString()) + "</CHIM_COVCOMWALLDIST>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_THIMPROTEDGE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["THIMBLE_OR_MATERIAL"].ToString()) + "</CHIM_THIMPROTEDGE>";
						//Stove Pipe
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPSTOVEPIPEIS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_PIPE_IS"].ToString()) + "</SPSTOVEPIPEIS>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPPIPEFITSNUG " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DOES_SMOKE_PIPE_FIT"].ToString()) + "</SPPIPEFITSNUG>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPWASTEHEAT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SMOKE_PIPE_WASTE_HEAT"].ToString()) + "</SPWASTEHEAT>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPCONSECURE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_CONN_SECURE"].ToString()) + "</SPCONSECURE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPPASSINETERIOR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SMOKE_PIPE_PASS"].ToString()) + "</SPPASSINETERIOR>";
						
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPTHIMBLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SELECT_PASS"].ToString()) + "</SPTHIMBLE>";
						if (Row["SELECT_PASS"].ToString()=="8966")
						{
							Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPPASSINCHES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["PASS_INCHES"].ToString()) + "</SPPASSINCHES>";
						}
						else if(Row["SELECT_PASS"].ToString()=="8967")
						{
							Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPPASSINCHES1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["PASS_INCHES"].ToString()) + "</SPPASSINCHES1>";
						}
						//Unit Clearance
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<UCUSECONFORM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_INSTALLATION_CONFORM_SPECIFICATIONS"].ToString()) + "</UCUSECONFORM>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<UCPROTMETDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["PROT_MAT_SPACED"].ToString()) + "</UCPROTMETDESC>";
						//FIRE PROTECTION
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<FPSMOKEDETECTOR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_SMOKE_DETECTOR"].ToString()) + "</FPSMOKEDETECTOR>";
						//CLEANING
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CLPIPECLEAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_SMOKE_PIPE_CLEANED"].ToString()) + "</CLPIPECLEAN>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CLPIPEINSPECTEDBY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_CLEANER"].ToString()) + "</CLPIPEINSPECTEDBY>";
						//REMARKS
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CLFPREMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["REMARKS"].ToString()) + "</CLFPREMARKS>";

						RowCounter++;
					}
				}
				else
					AcordPDFXML.SelectSingleNode(RootElement).RemoveChild(Acord73RootElement);
			}
		}
		#endregion

		#region Create RV XML

		private string getRVPremium(DataSet DSTempDataSet, string comp_code, string risk_id)
		{
			if(DSTempDataSet.Tables.Count > 1 && DSTempDataSet.Tables[1].Rows.Count > 0)
			{
				foreach (DataRow Row in DSTempDataSet.Tables[1].Rows)
				{
					if(Row["COMPONENT_CODE"].ToString() == comp_code && Row["RISK_ID"].ToString() == risk_id)
						return Row["COVERAGE_PREMIUM"].ToString();
				}
			}
			return "";
		}

		private void CreateRVXML()
		{
			if (DsRVDetails.Tables[0].Rows.Count > 0) 
			{
				XmlElement DecPageRV;
				DecPageRV = AcordPDFXML.CreateElement("RECREATIONALVEHICLES");

				XmlElement Acord80RV;
				Acord80RV = AcordPDFXML.CreateElement("RECREATIONALVEHICLES");

				if (gStrPdfFor == PDFForDecPage)
				{
					#region Elements for DecPage
					DecPageRootElement.AppendChild(DecPageRV);
					DecPageRV.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageRV.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERV"));
					DecPageRV.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERV"));
					DecPageRV.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERVEXTN"));
					DecPageRV.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVEXTN"));
					#endregion
				}
				else if (gStrPdfFor == PDFForAcord)
				{
					Acord80RootElement.SelectSingleNode("POLICY/ATTACHRV").InnerText = "1";
					#region Elements for Acord 80 RV
					Acord80RootElement.AppendChild(Acord80RV);
					Acord80RV.SetAttribute(fieldType,fieldTypeMultiple);
					Acord80RV.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80RV"));
					Acord80RV.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80RV"));
					Acord80RV.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80RVEXTN"));
					Acord80RV.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVEXTN"));
					#endregion
				}

				int RVRowCounter=0;
				foreach (DataRow Row in DsRVDetails.Tables[0].Rows)
				{
					XmlElement DecPageRVElement;
					DecPageRVElement =  AcordPDFXML.CreateElement("RVINFO");

					XmlElement Acord80RVElement;
					Acord80RVElement =  AcordPDFXML.CreateElement("RVINFO");
					if (gStrPdfFor == PDFForDecPage)
					{
						#region Dec Page
						DecPageRV.AppendChild(DecPageRVElement);
						DecPageRVElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageRVElement.SetAttribute(id,RVRowCounter.ToString());

						if(string.Compare(gStrCalledFrom,"Policy",true)==0)
						{
							DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</RVPOLICYNUMBER>";
							DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</RVEFFDATE>";
							DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</RVEXPDATE>";
							DecPageRVElement.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
						}
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVNAMEDINSUNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</RVNAMEDINSUNAME>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVNAMEDINSUADD " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</RVNAMEDINSUADD>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVNAMEDINSUCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</RVNAMEDINSUCITYSTZIP>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</RVAGENCYNAME>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYADD " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</RVAGENCYADD>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</RVAGENCYCITYSTZIP>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</RVAGENCYPHONE>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</RVAGENCYCODE>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</RVAGENCYSUBCODE>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</RVAGENCYBILLING>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["COMPANY_ID_NUMBER"].ToString()) + "</RVNUMBER>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["VEHICLE_DESC"].ToString()) + "</RVDESCRIPTION>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVYEARMAKEMODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["YEARMAKEMODEL"].ToString()) + "</RVYEARMAKEMODEL>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVHPCC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HORSE_POWER"].ToString()) + "</RVHPCC>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVSERIALNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SERIAL"].ToString()) + "</RVSERIALNUMBER>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVINSURINGVALUE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["INSURING_VALUE"].ToString()) + "</RVINSURINGVALUE>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVPERSONALLIABLIM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strRVLiabLim) + "</RVPERSONALLIABLIM>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVMEDPMLIABLIM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strRVMedLim) + "</RVMEDPMLIABLIM>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVDEDUCTIBLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DEDUCTIBLE"].ToString()) + "</RVDEDUCTIBLE>";
						if(DsRVDetails.Tables.Count > 1 && DsRVDetails.Tables[1].Rows.Count > 0)
						{
							DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + getRVPremium(DsRVDetails, "SUMTOTAL", Row["REC_VEH_ID"].ToString()) + "</RVPREMIUM>";
							DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVPERSONALLIABPREM " + fieldType + "=\"" + fieldTypeText + "\">" + getRVPremium(DsRVDetails, "LIAB", Row["REC_VEH_ID"].ToString()) + "</RVPERSONALLIABPREM>";
							//DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVMEDPMLIABPREM " + fieldType + "=\"" + fieldTypeText + "\">" + getRVPremium(DSTempDataSet, "PD", Row["REC_VEH_ID"].ToString()) + "</RVMEDPMLIABPREM>";
						}
						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Acord 80
						Acord80RV.AppendChild(Acord80RVElement);
						Acord80RVElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord80RVElement.SetAttribute(id,RVRowCounter.ToString());

						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</RVPOLICYNUMBER>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</RVEFFDATE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</RVEXPDATE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVNAMEDINSUNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</RVNAMEDINSUNAME>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVNAMEDINSUADD " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</RVNAMEDINSUADD>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVNAMEDINSUCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</RVNAMEDINSUCITYSTZIP>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVAGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</RVAGENCYNAME>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVAGENCYADD " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</RVAGENCYADD>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVAGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</RVAGENCYCITYSTZIP>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVAGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</RVAGENCYCODE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVREMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["REMARKS"].ToString()) + "</RVREMARKS>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["COMPANY_ID_NUMBER"].ToString()) + "</RVNUMBER>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVYEAR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["YEAR"].ToString()) + "</RVYEAR>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVMAKE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MAKE"].ToString()) + "</RVMAKE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVMODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MODEL"].ToString()) + "</RVMODEL>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVVEHICLETYPE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(Row["VEHICLE_TYPE"].ToString()) + "</RVVEHICLETYPE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVHPCC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HORSE_POWER"].ToString()) + "</RVHPCC>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVSERIALNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SERIAL"].ToString()) + "</RVSERIALNUMBER>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVINSURINGVALUE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["INSURING_VALUE"].ToString()) + "</RVINSURINGVALUE>";
						if(Row["DEDUCTIBLE"].ToString()!="")
						{
							Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVDEDUCTIBLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DEDUCTIBLE"].ToString()) + "</RVDEDUCTIBLE>";
						}
						
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + getRVPremium(DsRVDetails, "SUMTOTAL", Row["REC_VEH_ID"].ToString()) + "</RVPREMIUM>";
						
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVPARTRACE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["USED_IN_RACE_SPEED"].ToString()) + "</RVPARTRACE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVPRIORLOSS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["PRIOR_LOSSES"].ToString()) + "</RVPRIORLOSS>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVSTATEREG " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_UNIT_REG_IN_OTHER_STATE"].ToString()) + "</RVSTATEREG>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVRISKCANCEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["RISK_DECL_BY_OTHER_COMP"].ToString()) + "</RVRISKCANCEL>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVGENINFOANYYESDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DESC_RISK_DECL_BY_OTHER_COMP"].ToString()) + "</RVGENINFOANYYESDESC>";
						#endregion
					}

					#region Additional Interests
					DataSet DSTmpRVAddlInt = new DataSet();
					DSTmpRVAddlInt	= getRVAdditionalInterestDataSet(Row["REC_VEH_ID"].ToString());
					if (DSTmpRVAddlInt.Tables[0].Rows.Count>0)
					{
						XmlElement DecPageRVAddInt;
						DecPageRVAddInt = AcordPDFXML.CreateElement("RVADDLINT");
						XmlElement Acord80RVAddInt;
						Acord80RVAddInt = AcordPDFXML.CreateElement("RVADDLINT");						

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Elements for Dec Page
							DecPageRVElement.AppendChild(DecPageRVAddInt);
							DecPageRVAddInt.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageRVAddInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERVADDLINT"));
							DecPageRVAddInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVADDLINT"));
							DecPageRVAddInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERVADDLINTEXTN"));
							DecPageRVAddInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVADDLINTEXTN"));
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Elements For Acord 80 RV
							Acord80RVElement.AppendChild(Acord80RVAddInt);
							Acord80RVAddInt.SetAttribute(fieldType,fieldTypeMultiple);
							Acord80RVAddInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80RVADDLINT"));
							Acord80RVAddInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVADDLINT"));
							Acord80RVAddInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80RVADDLINTEXTN"));
							Acord80RVAddInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVADDLINTEXTN"));
							#endregion
						}

						int RVAddIntRowCounter=0;
						string strAddIntExtnTxt = "";
						foreach (DataRow AddlRow in DSTmpRVAddlInt.Tables[0].Rows)
						{
							if (RVRowCounter%2 == 0)
								strAddIntExtnTxt="";
							else
								strAddIntExtnTxt="1";
							
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page
								XmlElement DecPageRVAddIntElement;
								DecPageRVAddIntElement =  AcordPDFXML.CreateElement("RVADDLINTINFO");
								DecPageRVAddInt.AppendChild(DecPageRVAddIntElement);
								DecPageRVAddIntElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageRVAddIntElement.SetAttribute(id,RVAddIntRowCounter.ToString());

								DecPageRVAddIntElement.InnerXml = DecPageRVAddIntElement.InnerXml +  "<RVADLINTNUMBER" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["ADD_INT_ID"].ToString()) + "</RVADLINTNUMBER" + strAddIntExtnTxt + ">";
								DecPageRVAddIntElement.InnerXml = DecPageRVAddIntElement.InnerXml +  "<RVADLINTNAMEADDRESS" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["ADDLINT_NAME"].ToString()) + "</RVADLINTNAMEADDRESS" + strAddIntExtnTxt + ">";
								DecPageRVAddIntElement.InnerXml = DecPageRVAddIntElement.InnerXml +  "<RVADLINTNATUREINT" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["NATUREOFINT"].ToString()) + "</RVADLINTNATUREINT" + strAddIntExtnTxt + ">";
								DecPageRVAddIntElement.InnerXml = DecPageRVAddIntElement.InnerXml +  "<RVADLINTLOANNUM" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["LOAN_REF_NUMBER"].ToString()) + "</RVADLINTLOANNUM" + strAddIntExtnTxt + ">";
								#endregion
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								#region Acord 80
								XmlElement Acord80RVAddIntElement;
								Acord80RVAddIntElement =  AcordPDFXML.CreateElement("RVADDLINTINFO");
								Acord80RVAddInt.AppendChild(Acord80RVAddIntElement);
								Acord80RVAddIntElement.SetAttribute(fieldType,fieldTypeNormal);
								Acord80RVAddIntElement.SetAttribute(id,RVAddIntRowCounter.ToString());

								Acord80RVAddIntElement.InnerXml = Acord80RVAddIntElement.InnerXml +  "<RVADLINTNUMBER" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["ADD_INT_ID"].ToString()) + "</RVADLINTNUMBER" + strAddIntExtnTxt + ">";
								Acord80RVAddIntElement.InnerXml = Acord80RVAddIntElement.InnerXml +  "<RVADLINTNAMEADDRESS" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["ADDLINT_NAME"].ToString()) + "</RVADLINTNAMEADDRESS" + strAddIntExtnTxt + ">";
								Acord80RVAddIntElement.InnerXml = Acord80RVAddIntElement.InnerXml +  "<RVADLINTNATUREINT" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["NATUREOFINT"].ToString()) + "</RVADLINTNATUREINT" + strAddIntExtnTxt + ">";
								Acord80RVAddIntElement.InnerXml = Acord80RVAddIntElement.InnerXml +  "<RVADLINTLOANNUM" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["LOAN_REF_NUMBER"].ToString()) + "</RVADLINTLOANNUM" + strAddIntExtnTxt + ">";
								#endregion
							}
							RVAddIntRowCounter++;
						}
					}
					#endregion

					#region Operators
					DataSet DSTmpRVOperators = new DataSet();
					DSTmpRVOperators= getOperatorDataSet();
					if (DSTmpRVOperators.Tables[0].Rows.Count>0)
					{
						XmlElement DecPageRVOperator;
						DecPageRVOperator = AcordPDFXML.CreateElement("RVOPERATOR");

						XmlElement Acord80RVOperator;
						Acord80RVOperator = AcordPDFXML.CreateElement("RVOPERATOR");
						if (gStrPdfFor == PDFForDecPage)
						{
							#region Elements For Dec Page
							DecPageRVElement.AppendChild(DecPageRVOperator);
							DecPageRVOperator.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageRVOperator.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERVOPERATOR"));
							DecPageRVOperator.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVOPERATOR"));
							DecPageRVOperator.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERVOPERATOREXTN"));
							DecPageRVOperator.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVOPERATOREXTN"));
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Elements For Acord 80 RV
							Acord80RVElement.AppendChild(Acord80RVOperator);
							Acord80RVOperator.SetAttribute(fieldType,fieldTypeMultiple);
							Acord80RVOperator.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80RVOPERATOR"));
							Acord80RVOperator.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVOPERATOR"));
							Acord80RVOperator.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80RVOPERATOREXTN"));
							Acord80RVOperator.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVOPERATOREXTN"));
							#endregion
						}

						int RVOprRowCounter=0;
						foreach (DataRow AddlRow in DSTmpRVOperators.Tables[0].Rows)
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page
								XmlElement DecPageRVOperatorElement;
								DecPageRVOperatorElement =  AcordPDFXML.CreateElement("RVADDLINTINFO");
								DecPageRVOperator.AppendChild(DecPageRVOperatorElement);
								DecPageRVOperatorElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageRVOperatorElement.SetAttribute(id,RVOprRowCounter.ToString());

								DecPageRVOperatorElement.InnerXml = DecPageRVOperatorElement.InnerXml +  "<RVOPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_NAME"].ToString()) + "</RVOPERATORNAME>";
								DecPageRVOperatorElement.InnerXml = DecPageRVOperatorElement.InnerXml +  "<RVOPERATORLICNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_DRIV_LIC"].ToString()) + "</RVOPERATORLICNUM>";
								DecPageRVOperatorElement.InnerXml = DecPageRVOperatorElement.InnerXml +  "<RVOPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_DOB"].ToString()) + "</RVOPERATORDOB>";
								#endregion
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								#region Acord 80
								XmlElement Acord80RVOperatorElement;
								Acord80RVOperatorElement =  AcordPDFXML.CreateElement("RVADDLINTINFO");
								Acord80RVOperator.AppendChild(Acord80RVOperatorElement);
								Acord80RVOperatorElement.SetAttribute(fieldType,fieldTypeNormal);
								Acord80RVOperatorElement.SetAttribute(id,RVOprRowCounter.ToString());

								Acord80RVOperatorElement.InnerXml = Acord80RVOperatorElement.InnerXml +  "<RVOPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_NAME"].ToString()) + "</RVOPERATORNAME>";
								Acord80RVOperatorElement.InnerXml = Acord80RVOperatorElement.InnerXml +  "<RVOPERATORLICNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_DRIV_LIC"].ToString()) + "</RVOPERATORLICNUM>";
								Acord80RVOperatorElement.InnerXml = Acord80RVOperatorElement.InnerXml +  "<RVOPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_DOB"].ToString()) + "</RVOPERATORDOB>";
								Acord80RVOperatorElement.InnerXml = Acord80RVOperatorElement.InnerXml +  "<RVOPERATORSTATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["STATE_NAME"].ToString()) + "</RVOPERATORSTATE>";
								#endregion
							}
							RVOprRowCounter++;
						}
					}
					#endregion

					RVRowCounter++;
				}
			}
		}
		#endregion

		#region Dec Page WaterCraft Information
		private void createWaterCraftXml()
		{
			LoadRateXML("HOME-BOAT");
			//int BoatCtr = 0;

			if (gStrPdfFor == PDFForDecPage)
			{
				int intBoatCtr=0,intTmpCtr=0;
				DataSet BoattmpDataSet;

				DSTempDataSet = getBoatDetailsDataSet();
				
				
				if(DSTempDataSet.Tables[0].Rows.Count>0)
				{
					#region declaration Boat Root Element DecPage
					XmlElement BoatRootElementDecPage;
					BoatRootElementDecPage = AcordPDFXML.CreateElement("BOAT");
					DecPageRootElement.AppendChild(BoatRootElementDecPage);

					BoatRootElementDecPage.SetAttribute(fieldType,fieldTypeMultiple);
					BoatRootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBOAT"));
					BoatRootElementDecPage.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOAT"));
					BoatRootElementDecPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBOATEXTN"));
					BoatRootElementDecPage.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOATEXTN"));
					#endregion

					string sumTtl="0";
					
					getPhotoAttach	=	DSTempDataSet.Tables[0].Rows[0]["PHOTO_ATTACHED"].ToString();
										
					getMarineSurvey	=	DSTempDataSet.Tables[0].Rows[0]["MARINE_SURVEY"].ToString(); 
					

					if(gStrtemp != "final")
					{
						foreach (XmlNode SumTotalNode in GetSumTotalPremium())
						{
							sumTtl= getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString() ;
						}
					
					}
					else 
					{
						double sumTtl1 = 0.0;
						foreach(DataRow BoatDetails in DSTempDataSet.Tables[0].Rows)
						{
							DataSet BoattmpDataSet1 = getBoatCoveragesDataSet(BoatDetails["BOAT_ID"].ToString());
							
							sumTtl1=GetPremiumSch(BoattmpDataSet1,"MSE","COVERAGE_PREMIUM");
							sumTtl1 += GetPremiumSch(BoattmpDataSet1,"B","COVERAGE_PREMIUM");
							sumTtl1 += GetPremiumSch(BoattmpDataSet1,"T","COVERAGE_PREMIUM");

							string sumUn = GetPremiumAll(BoattmpDataSet1, "BOAT_UNATTACH_PREMIUM", BoatDetails["BOAT_ID"].ToString());
							if (sumUn != "")
								sumTtl1 += double.Parse(sumUn.Replace("$",""));


						}
						sumTtl = sumTtl1.ToString();
					}
				

					foreach(DataRow BoatDetails in DSTempDataSet.Tables[0].Rows)
					{
						htpremium.Clear(); 
						foreach (XmlNode PremiumNode in GetPremium(BoatDetails["BOAT_ID"].ToString()))
						{
							if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
								htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
						}

						#region Boat Child Element Dec Page
						XmlElement BoatElementSuppliment;
						BoatElementSuppliment	= AcordPDFXML.CreateElement("BOATINFO");

						XmlElement BoatElementDecPage;
						BoatElementDecPage = AcordPDFXML.CreateElement("BOATINFO");
						BoatRootElementDecPage.AppendChild(BoatElementDecPage);

						BoatElementDecPage.SetAttribute(fieldType,fieldTypeNormal);
						BoatElementDecPage.SetAttribute(id,intBoatCtr.ToString());
						#endregion

						#region Boat Root Element Node XML
						if(string.Compare(gStrCalledFrom,"Policy",true)==0)
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</WATERCRAFT_POLICYNO>";
							BoatElementDecPage.InnerXml += "<WATERCRAFT_POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</WATERCRAFT_POLICYPERIODFROM>";
							BoatElementDecPage.InnerXml += "<WATERCRAFT_POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</WATERCRAFT_POLICYPERIODTO>";
						}
						BoatElementDecPage.InnerXml += "<WATERCRAFT_REASON " + fieldType + "=\"" + fieldTypeText + "\"></WATERCRAFT_REASON>";
						BoatElementDecPage.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</WATERCRAFT_PRIMARYCONTACTNAME>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_PRIMARYCONTACTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</WATERCRAFT_PRIMARYCONTACTADDRESS>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_PRIMARYCONTACTCITY " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</WATERCRAFT_PRIMARYCONTACTCITY>";
						if(BoatDetails["LOCADD"].ToString()!="")
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_LOCATIONADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetails["LOCADD"].ToString())+"</WATERCRAFT_LOCATIONADDRESS>"; 
						}
						if(BoatDetails["LOCCITY"].ToString()!="")
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_LOCATIONCITYSTZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetails["LOCCITY"].ToString())+"</WATERCRAFT_LOCATIONCITYSTZIP>"; 
						}
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</WATERCRAFT_AGENCYNAME>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</WATERCRAFT_AGENCYADDRESS>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</WATERCRAFT_AGENCYCITYSTATEZIP>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYPHONENO " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</WATERCRAFT_AGENCYPHONENO>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</WATERCRAFT_AGENCYCODE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</WATERCRAFT_AGENCYSUBCODE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</WATERCRAFT_AGENCYBILLING>";
						#endregion
				
						#region Boat Element Node XML
						BoatElementDecPage.InnerXml += "<WATERCRAFT_USE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["WATERS_NAVIGATED_USE"].ToString()) + "</WATERCRAFT_USE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_TERRITORY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_TERRITORY"].ToString()) + "</WATERCRAFT_TERRITORY>";
						
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_NO"].ToString()) + "</WATERCRAFT_BOATNO>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATTYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOATTYPE"].ToString()) + "</WATERCRAFT_BOATTYPE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPURCHASEYEAR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_YEAR"].ToString())+ "</WATERCRAFT_BOATPURCHASEYEAR>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATMODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_MAKE_MODEL"].ToString()) + "</WATERCRAFT_BOATMODEL>";
						if(BoatDetails["BOAT_INCHES"].ToString()=="")
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATLENGTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_LENGTH"].ToString() +"'") + "</WATERCRAFT_BOATLENGTH>";
						}
						else
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATLENGTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_LENGTH"].ToString() +"'" + BoatDetails["BOAT_INCHES"].ToString() +"''") + "</WATERCRAFT_BOATLENGTH>";
						
						}
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATCC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["HORSE_POWER"].ToString()) + "</WATERCRAFT_BOATCC>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATSERIALNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["HULL_ID_NO"].ToString()) + "</WATERCRAFT_BOATSERIALNO>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATCONSTRUCTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["HULL_MATERIAL"].ToString()) + "</WATERCRAFT_BOATCONSTRUCTION>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATINSURINGVALUE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["PRESENT_VALUE"].ToString()) + "</WATERCRAFT_BOATINSURINGVALUE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATDED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["DEDUCTIBLE"].ToString()) + "</WATERCRAFT_BOATDED>";
						

						#region Coverages
						BoattmpDataSet = getBoatCoveragesDataSet(BoatDetails["BOAT_ID"].ToString());
						//double dblSumUnattach=0;
						foreach(DataRow CoverageDetails in BoattmpDataSet.Tables[0].Rows)
						{
							switch(CoverageDetails["COV_CODE"].ToString())
							{
								case "EBPPDACV":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_BOATPREMIUM>";
									break;
								case "EBPPDAV":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_BOATPREMIUM>";
									break;
								case "EBPPDJ":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_BOATPREMIUM>";
									break;
								case "LCCSL":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_PERSONALLIABILITY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_PERSONALLIABILITY>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_PERSONALLIABILITYLIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+ "  Each Occurence" )+ "</WATERCRAFT_PERSONALLIABILITYLIMIT>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_PERSONALLIABILITYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_PERSONALLIABILITYPREMIUM>";										
									break;
								case "MCPAY":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_MEDICALPAYMENTS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_MEDICALPAYMENTS>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_MEDICALPAYMENTSLIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+ "  Per Person 25000.00 Each Occurence") + "</WATERCRAFT_MEDICALPAYMENTSLIMIT>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_MEDICALPAYMENTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_MEDICALPAYMENTSPREMIUM>";
									break;
								case "UMBCS":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_UNINSUREDBOATERS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_UNINSUREDBOATERS>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_UNINSUREDBOATERSLIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+"  Each Occurence/Each Unit ") + "</WATERCRAFT_UNINSUREDBOATERSLIMIT>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_UNINSUREDBOATERSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_UNINSUREDBOATERSPREMIUM>";
									break;
							}

						}

						#region  Forms and Endorsements
						#region Declaration Page Boat Endorsements
						XmlElement DecPageHomeEndmts;
						DecPageHomeEndmts = AcordPDFXML.CreateElement("BOATENDORSEMENTS");
						BoatElementDecPage.AppendChild(DecPageHomeEndmts);
						DecPageHomeEndmts.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageHomeEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHOMEEND"));
						DecPageHomeEndmts.SetAttribute(PrimPDFBlocks,"6");
						DecPageHomeEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEENDEXTN"));
						DecPageHomeEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEENDEXTN"));
						#endregion

						int DecPageEndCtr = 0;
						foreach(DataRow CoverageDetails in BoattmpDataSet.Tables[0].Rows)
						{
							string CovCode = CoverageDetails["COV_CODE"].ToString();
							if (CovCode!="LCCSL" && CovCode!="MCPAY" && CovCode!="UMBCS")
							{	
								if(CovCode=="EBIUE" && BoatDetails["BOAT_ID"].ToString() != "1")
									continue;
								XmlElement DecPageBoatEndmtElement;
								DecPageBoatEndmtElement = AcordPDFXML.CreateElement("BOATENDORSEMENTSINFO");
								DecPageHomeEndmts.AppendChild(DecPageBoatEndmtElement);
								DecPageBoatEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageBoatEndmtElement.SetAttribute(id,(DecPageEndCtr).ToString() );

								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_DECPAGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("HOMEOWNERS  DECLARATION")+"</HM_DECPAGE>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<WAT_SECTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("WATERCRAFT SECTION")+"</WAT_SECTION>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</HM_ENDFORM>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
								if(CoverageDetails["LIMIT_1"].ToString().Trim() != "0.00")
									DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
								DecPageBoatEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CovCode))+"</HM_ENDPREMIUM>";
								DecPageEndCtr++;
							}
						}
						#endregion
						#endregion

						#region Creating Credit And Surcharge Xml
						if (isRateGenerated)
						{
							int CreditSurchRowCounter = 0;
							string AdditionalExtnTxt="";

							#region Credits
							XmlElement DecPageBoatCredit;
							DecPageBoatCredit = AcordPDFXML.CreateElement("BOATCREDIT");

							
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								BoatElementDecPage.AppendChild(DecPageBoatCredit);
								DecPageBoatCredit.SetAttribute(fieldType,fieldTypeMultiple);
								DecPageBoatCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
								DecPageBoatCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
								DecPageBoatCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
								DecPageBoatCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));
								#endregion
							}
							

							foreach (XmlNode CreditNode in GetCredits(BoatDetails["BOAT_ID"].ToString()))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									string step_desc = getAttributeValue(CreditNode,"STEPDESC").Replace(" ","");
									string step_prem = getAttributeValue(CreditNode,"STEPPREMIUM").Trim();

									if(step_desc.IndexOf("+0%") >0 || step_desc.IndexOf("-0%") >0 || step_prem == "0")
										continue;

									#region Dec Page
									XmlElement DecPageBoatCreditElement;
									DecPageBoatCreditElement = AcordPDFXML.CreateElement("BOATCREDITINFO");
									DecPageBoatCredit.AppendChild(DecPageBoatCreditElement);
									DecPageBoatCreditElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageBoatCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
									DecPageBoatCreditElement.InnerXml += "<WATERCRAFT_CREDIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","")) +"</WATERCRAFT_CREDIT>"; 

									DecPageBoatCreditElement.InnerXml += "<CREDITDISCPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</CREDITDISCPREM>"; 
									#endregion
								}
								

								CreditSurchRowCounter++;
							}
							#endregion

							CreditSurchRowCounter = 0;
							AdditionalExtnTxt="";

							#region Surcharges
							XmlElement DecPageBoatSurch;
							DecPageBoatSurch = AcordPDFXML.CreateElement("BOATSURCHARGE");

							XmlElement SupplementBoatSurch;
							SupplementBoatSurch = AcordPDFXML.CreateElement("BOATSURCHARGE");

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								BoatElementDecPage.AppendChild(DecPageBoatSurch);
								DecPageBoatSurch.SetAttribute(fieldType,fieldTypeMultiple);
								DecPageBoatSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
								DecPageBoatSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
								DecPageBoatSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
								DecPageBoatSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));
								#endregion
							}
							

							foreach (XmlNode CreditNode in GetSurcharges(BoatDetails["BOAT_ID"].ToString()))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									string step_desc = getAttributeValue(CreditNode,"STEPDESC").Replace(" ","");
									string step_prem = getAttributeValue(CreditNode,"STEPPREMIUM").Trim();

									if(step_desc.IndexOf("+0%") >0 || step_desc.IndexOf("-0%") >0 || step_prem == "0")
										continue;
									#region Dec Page
									XmlElement DecPageBoatSurchElement;
									DecPageBoatSurchElement = AcordPDFXML.CreateElement("BOATSURCHARGEINFO");
									DecPageBoatSurch.AppendChild(DecPageBoatSurchElement);
									DecPageBoatSurchElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageBoatSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());

									DecPageBoatSurchElement.InnerXml += "<WATERCRAFT_SURCHARGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</WATERCRAFT_SURCHARGE>"; 
									DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM>"; 
									#endregion
								}
								
								CreditSurchRowCounter++;
							}
							#endregion
						}
						#endregion
			
						#region Additional Int

						#region declaration Additional Int Root Element 
						XmlElement AdditionalIntRootElement;
						AdditionalIntRootElement = AcordPDFXML.CreateElement("ADDITIONALINT");
						BoatElementDecPage.AppendChild(AdditionalIntRootElement);
						AdditionalIntRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						AdditionalIntRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDLINT"));
						AdditionalIntRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINT"));
						AdditionalIntRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEADDLINTEXTN"));
						AdditionalIntRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTEXTN"));
						#endregion
				
						BoattmpDataSet = getBoatAddInterestDataSet(BoatDetails["BOAT_ID"].ToString());
					
						foreach(DataRow AddIntDetails in BoattmpDataSet.Tables[0].Rows)
						{
							#region declaration Additional Int Element 
							XmlElement AdditionalIntElement;
							AdditionalIntElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");
							AdditionalIntRootElement.AppendChild(AdditionalIntElement);
							AdditionalIntElement.SetAttribute(fieldType,fieldTypeNormal);
							AdditionalIntElement.SetAttribute(id,intTmpCtr.ToString());
							#endregion
				
							AdditionalIntElement.InnerXml += "<WATERCRAFT_AISERIALNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["BOAT_NO"].ToString()) + "</WATERCRAFT_AISERIALNO>";
							AdditionalIntElement.InnerXml += "<ADDLINTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["HOLDER_NAME"].ToString()) + "</ADDLINTNAME>";
							AdditionalIntElement.InnerXml += "<ADDLINTADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["ADDRESS"].ToString()) + "</ADDLINTADD>";
							AdditionalIntElement.InnerXml += "<WATERCRAFT_NATUREOFINTEREST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["ADDLINTNAME"].ToString()) + "</WATERCRAFT_NATUREOFINTEREST>";
							AdditionalIntElement.InnerXml += "<ADDLINTLOANNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["LOAN_REF_NUMBER"].ToString()) + "</ADDLINTLOANNUM>";
							AdditionalIntElement.InnerXml += "<ADDLINTTYPEOFINT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["NATURE_OF_INTEREST"].ToString()) + "</ADDLINTTYPEOFINT>";

							intTmpCtr++;
						}
						intTmpCtr = 0;	
						#endregion

						#region Schedule equipment
			
						#region EQUIPMENT ROOT ELEMENT DECLARATION
						XmlElement EquipmentRootElement;
						EquipmentRootElement = AcordPDFXML.CreateElement("EQUIPMENT");
						BoatElementDecPage.AppendChild(EquipmentRootElement);
						EquipmentRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						EquipmentRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEQUIPMENT"));
						EquipmentRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEQUIPMENT"));
						EquipmentRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEQUIPMENTEXTN"));
						EquipmentRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEQUIPMENTEXTN"));
						#endregion

						int intTmpCtr1 = 0;
						BoattmpDataSet = getSchEquipmentsDataSet("0");
						foreach(DataRow EquipmentDetails in BoattmpDataSet.Tables[0].Rows)
						{
				
							#region EQUIPMENT ELEMENT DECLARATION
							XmlElement EquipmentElement;
							EquipmentElement = AcordPDFXML.CreateElement("EQUIPMENTINFO");
							EquipmentRootElement.AppendChild(EquipmentElement);
							EquipmentElement.SetAttribute(fieldType,fieldTypeNormal);
							EquipmentElement.SetAttribute(id,intTmpCtr1.ToString());
							#endregion
				
							EquipmentElement.InnerXml += "<WATERCRAFT_SERIALNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIP_NO"].ToString()) + "</WATERCRAFT_SERIALNO>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIPDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIPDESC"].ToString()) + "</WA_SCHEQUIPDESC>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIPSERIAL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["SERIAL_NO"].ToString()) + "</WA_SCHEQUIPSERIAL>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIP_ELECTRONIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIPMENT_TYPE"].ToString()) + "</WA_SCHEQUIP_ELECTRONIC>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIP_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["INSURED_VALUE"].ToString()) + "</WA_SCHEQUIP_LIMIT>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIP_DED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIP_AMOUNT"].ToString()) + "</WA_SCHEQUIP_DED>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIP_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\"></WA_SCHEQUIP_PREMIUM>";
				
							intTmpCtr1 ++;
						}
						intTmpCtr1 = 0;
						#endregion

						#region Opraters
			
						#region OPRATERS ROOT ELEMENT DECLARATION
						XmlElement OperatorRootElement;
						OperatorRootElement = AcordPDFXML.CreateElement("OPERATOR");
						BoatElementDecPage.AppendChild(OperatorRootElement);
						OperatorRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						OperatorRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOPERATOR"));
						OperatorRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOR"));
						OperatorRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOPERATOREXTN"));
						OperatorRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOREXTN"));
						#endregion

						BoattmpDataSet = getOperatorDataSet();
						foreach(DataRow OperatorDetails in BoattmpDataSet.Tables[0].Rows)
						{
				
							#region OPERATOR ELEMENT DECLARATION
							XmlElement OperatorElement;
							OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
							OperatorRootElement.AppendChild(OperatorElement);
							OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
							OperatorElement.SetAttribute(id,intTmpCtr1.ToString());
							#endregion
							string strOprSSn="";
							strOprSSn =  OperatorDetails["DRIVER_SSN"].ToString();
							if(strOprSSn !="" && strOprSSn !="0")
							{
								strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(OperatorDetails["DRIVER_SSN"].ToString());
								
								if(strOprSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
								{
									string strvaln = "xxx-xx-";
									strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
									strOprSSn = strvaln;
								}
								else
									strOprSSn="";
							}
							OperatorElement.InnerXml += "<OPERATORNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["OPERATORNO"].ToString()) + "</OPERATORNUMBER>";
							OperatorElement.InnerXml += "<OPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_NAME"].ToString()) + "</OPERATORNAME>";
							OperatorElement.InnerXml += "<OPERATORSEX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_SEX"].ToString()) + "</OPERATORSEX>";
							//OperatorElement.InnerXml += "<26_10_MAR_STATUS " + fieldType + "=\"" + fieldTypeText + "\"></26_10_MAR_STATUS>";
							OperatorElement.InnerXml += "<OPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_DOB"].ToString()) + "</OPERATORDOB>";
							OperatorElement.InnerXml += "<OPERATORAUTODRVLIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_DRIV_LIC"].ToString()) + "</OPERATORAUTODRVLIC>";
							OperatorElement.InnerXml += "<OPERATORLICSTATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_LIC_STATE"].ToString()) + "</OPERATORLICSTATE>";
							OperatorElement.InnerXml += "<OPERATORSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strOprSSn) + "</OPERATORSSN>";
								
							intTmpCtr1 ++;
						}
						intTmpCtr1 = 0;
						#endregion
               
						intBoatCtr++;
						#endregion

					}
				}
			}
		}
		#endregion

		#region Schedule Articles XML
		private string getSchPremium(DataSet DSTempDataSet, string comp_code)
		{
			if(DSTempDataSet.Tables.Count > 1 && DSTempDataSet.Tables[1].Rows.Count > 0)
			{
				foreach (DataRow Row in DSTempDataSet.Tables[1].Rows)
				{
					if(Row["COMPONENT_CODE"].ToString() == comp_code)// && Row["RISK_ID"].ToString() == risk_id)
						return Row["COVERAGE_PREMIUM"].ToString();
				}
			}
			return "";
		}

		private void CreateScheduleArticlesXML()
		{
			int tmpArtCtr=0,tmpctr=0, TempCounter=0;
			//string strOtherInfo="";
			DataSet tmpSchArticle;

			DSTempDataSet = DsScheduleArticalDetails;
			if(DSTempDataSet.Tables[0].Rows.Count>0)
			{
				XmlElement SchArticleRootElement;
				SchArticleRootElement = AcordPDFXML.CreateElement("SCHEDULEARTICLES");

				XmlElement ArticleInfoInlandRootElement;
				ArticleInfoInlandRootElement = AcordPDFXML.CreateElement("ARTICLES");

				XmlElement XArticleInfoInlandRootElement;
				XArticleInfoInlandRootElement = AcordPDFXML.CreateElement("XARTICLES");
				
				if (gStrPdfFor == PDFForDecPage)
				{
					#region declaration Schedule Article Root Element DecPage
					DecPageRootElement.AppendChild(SchArticleRootElement);
					SchArticleRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					SchArticleRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESCHARTICLES"));
					SchArticleRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESCHARTICLES"));
					SchArticleRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESCHARTICLESEXTN"));
					SchArticleRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESCHARTICLESEXTN"));
					#endregion
				}
				if (gStrPdfFor == PDFForAcord)
				{
					Acord80RootElement.SelectSingleNode("POLICY/ATTCHINLANDMARINE").InnerText = "1";
					#region Article Info Root Element for Inland Acord 81
					Acord81RootElement.AppendChild(ArticleInfoInlandRootElement);
					ArticleInfoInlandRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					ArticleInfoInlandRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD81INLANDMARIN"));
					ArticleInfoInlandRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD81INLANDMARIN"));
					ArticleInfoInlandRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD81INLANDMARINEXTN"));
					ArticleInfoInlandRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD81INLANDMARINEXTN"));
					#endregion
					
					#region Extra Article Info Root Element for Inland Acord 81
					Acord81RootElement.AppendChild(XArticleInfoInlandRootElement);
					XArticleInfoInlandRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					XArticleInfoInlandRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD81XINLANDMARIN"));
					XArticleInfoInlandRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD81XINLANDMARIN"));
					XArticleInfoInlandRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD81XINLANDMARINEXTN"));
					XArticleInfoInlandRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD81XINLANDMARINEXTN"));
					
					#endregion
					
				}

				XmlElement ArticleInfoInlandElement;
				ArticleInfoInlandElement = AcordPDFXML.CreateElement("ARTICLEINFO");

				//				XmlElement ADDITIONALRATINGINFORMATIONElement;
				//				ADDITIONALRATINGINFORMATIONElement = AcordPDFXML.CreateElement("ADDITIONALRATINGINFORMATION");

				ArticleInfoInlandRootElement.AppendChild(ArticleInfoInlandElement);
				ArticleInfoInlandElement.SetAttribute(fieldType,fieldTypeNormal);
				ArticleInfoInlandElement.SetAttribute(id,"0");

				
			
				foreach(DataRow SchArticleDetail in DSTempDataSet.Tables[0].Rows)
				{		
					//ArticleInfoInlandElement.AppendChild(ADDITIONALRATINGINFORMATIONElement);

					#region SWITCH CASE

					string com_code = SchArticleDetail["COMPONENT_CODE"].ToString();
					switch(SchArticleDetail["COV_CODE"].ToString())
					{
						case "JEWEL":
							ArticleInfoInlandElement.InnerXml += "<JEWELLERYAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</JEWELLERYAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<JEWELLERYRATE " + fieldType + "=\"" + fieldTypeText + "\"></JEWELLERYRATE>";

							ArticleInfoInlandElement.InnerXml += "<JEWELLERYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</JEWELLERYPREMIUM>";																			
							break;
						case "FURS":
							ArticleInfoInlandElement.InnerXml += "<FURSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</FURSAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<FURSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FURSRATE>";
							ArticleInfoInlandElement.InnerXml += "<FURSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</FURSPREMIUM>";																			
							break;
						case "FINEBR":
							ArticleInfoInlandElement.InnerXml += "<FINEARTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</FINEARTSAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<FINEARTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FINEARTSRATE>";
							ArticleInfoInlandElement.InnerXml += "<FINEARTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</FINEARTSPREMIUM>";																			

							if (SchArticleDetail["AMOUNT"].ToString()!="" && Double.Parse(SchArticleDetail["AMOUNT"].ToString())>0)
							{
								ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">1</BREAKAGECOVERAGE>";
							}
							else
							{
								ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">0</BREAKAGECOVERAGE>";
							}
							break;

						case "CAMER":
							ArticleInfoInlandElement.InnerXml += "<CAMERASAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</CAMERASAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<CAMERASRATE " + fieldType + "=\"" + fieldTypeText + "\"></CAMERASRATE>";
							ArticleInfoInlandElement.InnerXml += "<CAMERASPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</CAMERASPREMIUM>";																			
								
							break;
						case "MUSIC":
							ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</MUSICALINSTRUMENTSAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></MUSICALINSTRUMENTSRATE>";
							ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</MUSICALINSTRUMENTSPREMIUM>";																			
																
							break;
						case "SILVE":
							ArticleInfoInlandElement.InnerXml += "<SILVERWAREAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</SILVERWAREAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<SILVERWARERATE " + fieldType + "=\"" + fieldTypeText + "\"></SILVERWARERATE>";
							ArticleInfoInlandElement.InnerXml += "<SILVERWAREPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</SILVERWAREPREMIUM>";																			
																								
							break;
						case "STAMP":
							ArticleInfoInlandElement.InnerXml += "<STAMPSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</STAMPSAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<STAMPSRATE " + fieldType + "=\"" + fieldTypeText + "\"></STAMPSRATE>";
							ArticleInfoInlandElement.InnerXml += "<STAMPSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</STAMPSPREMIUM>";																			
							
							break;
						case "RARE":
							ArticleInfoInlandElement.InnerXml += "<COINSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</COINSAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<COINSRATE " + fieldType + "=\"" + fieldTypeText + "\"></COINSRATE>";
							ArticleInfoInlandElement.InnerXml += "<COINSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</COINSPREMIUM>";																			
							break;
						case "GOLF":
							ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</GOLFERSEQUIPMENTAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTRATE " + fieldType + "=\"" + fieldTypeText + "\"></GOLFERSEQUIPMENTRATE>";
							ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</GOLFERSEQUIPMENTPREMIUM>";																			

							break;
						case "PERSOD":
							ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</PERSONALCOMPUTERAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERRATE " + fieldType + "=\"" + fieldTypeText + "\"></PERSONALCOMPUTERRATE>";
							ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</PERSONALCOMPUTERPREMIUM>";																			
						
							break;
						case "PERSOL":
							ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</PERSONALCOMPUTERAMOUNT>";
							ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERRATE " + fieldType + "=\"" + fieldTypeText + "\"></PERSONALCOMPUTERRATE>";
							ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</PERSONALCOMPUTERPREMIUM>";																			
							if(htpremium.Contains("PRS_CMP"))
							{
								//ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</PERSONALCOMPUTERPREMIUM>";										
							}					
							break;
						default:
							XmlElement XArticleInfoInlandElement;
							XArticleInfoInlandElement =  AcordPDFXML.CreateElement("XArticleInfoInlandElementINFO");			
							XArticleInfoInlandRootElement.AppendChild(XArticleInfoInlandElement);
							XArticleInfoInlandElement.SetAttribute(fieldType,fieldTypeNormal);
							XArticleInfoInlandElement.SetAttribute(id,TempCounter.ToString());
							

							XArticleInfoInlandElement.InnerXml += "<OTHERPROP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["COV_DES"].ToString()) + "</OTHERPROP>";
							XArticleInfoInlandElement.InnerXml += "<OTHERPREM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code)) + "</OTHERPREM>";
							XArticleInfoInlandElement.InnerXml += "<OTHERAMT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</OTHERAMT>";
							
							TempCounter++; 
							break;						
					}
					#endregion										
				}
	
				TempCounter = 0;
								
				#region Schedule Article Details
				foreach(DataRow SchArticleDetails in DSTempDataSet.Tables[0].Rows)
				{
					XmlElement SchArticleElement;
					SchArticleElement = AcordPDFXML.CreateElement("SCHEDULEARTICLESINFO");
						
					XmlElement ArticleInfoRootElement;
					ArticleInfoRootElement = AcordPDFXML.CreateElement("ARTICLES");

					htpremium.Clear(); 
					foreach (XmlNode PremiumNode in GetPremium())
					{
						if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
							htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
					}
						
					if (gStrPdfFor == PDFForDecPage)
					{
						#region declaration Schedule Article Element DecPage
						SchArticleRootElement.AppendChild(SchArticleElement);
						SchArticleElement.SetAttribute(fieldType,fieldTypeNormal);
						SchArticleElement.SetAttribute(id, tmpArtCtr.ToString());
				
						SchArticleElement.InnerXml += "<SCHART_POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</SCHART_POLICYNO>";
						SchArticleElement.InnerXml += "<SCHART_POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</SCHART_POLICYPERIODFROM>";
						SchArticleElement.InnerXml += "<SCHART_POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</SCHART_POLICYPERIODTO>";
						SchArticleElement.InnerXml += "<SCHART_REASON " + fieldType + "=\"" + fieldTypeText + "\"></SCHART_REASON>";
						SchArticleElement.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
						SchArticleElement.InnerXml += "<SCHART_PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</SCHART_PRIMARYCONTACTNAME>";
						SchArticleElement.InnerXml += "<SCHART_PRIMARYCONTACTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress  + "</SCHART_PRIMARYCONTACTADDRESS>";
						SchArticleElement.InnerXml += "<SCHART_PRIMARYCONTACTCITY " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</SCHART_PRIMARYCONTACTCITY>";
						SchArticleElement.InnerXml += "<SCHART_AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName  + "</SCHART_AGENCYNAME>";
						SchArticleElement.InnerXml += "<SCHART_AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</SCHART_AGENCYADDRESS>";
						SchArticleElement.InnerXml += "<SCHART_AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip  + "</SCHART_AGENCYCITYSTATEZIP>";
						SchArticleElement.InnerXml += "<SCHART_AGENCYPHONENO " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</SCHART_AGENCYPHONENO>";
						SchArticleElement.InnerXml += "<SCHART_AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</SCHART_AGENCYCODE>";
						SchArticleElement.InnerXml += "<SCHART_AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode  + "</SCHART_AGENCYSUBCODE>";
						SchArticleElement.InnerXml += "<SCHART_AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</SCHART_AGENCYBILLING>";
				
						SchArticleElement.InnerXml += "<SCHART_CATEGORY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetails["COV_DES"].ToString()) + "</SCHART_CATEGORY>";
						SchArticleElement.InnerXml += "<SCHART_TOTALAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetails["AMOUNT"].ToString()) + "</SCHART_TOTALAMOUNT>";
						SchArticleElement.InnerXml += "<SCHART_DEDUCTIBLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetails["LIMIT_DEDUC_AMOUNT"].ToString()) + "</SCHART_DEDUCTIBLE>";
						SchArticleElement.InnerXml += "<SCHART_FORM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetails["FORM_NO"].ToString()) + "</SCHART_FORM>";

						string comp_code = SchArticleDetails["COMPONENT_CODE"].ToString().ToUpper();
						switch (SchArticleDetails["COV_CODE"].ToString().ToUpper())
						{
							case	"BICYC"	:
								if(htpremium.Contains("BICYCLE"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}									
								}
								break;
							case	"CAMER"	:
								if(htpremium.Contains("CMRA"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}																		
								}
								break;
							case	"CELLU"	:
								if(htpremium.Contains("CELL"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}									
								}
								break;
							case	"FINEBR"	:
								if(htpremium.Contains("FNE_ART_WTH_BRK"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}									
								}
								break;
							case	"FINEWBR"	:
								if(htpremium.Contains("FNE_ART_WO_BRK"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}								
								}
								break;
							case	"FURS"	:
								if(htpremium.Contains("FUR"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}									
								}
								break;
							case	"GOLF"	:
								if(htpremium.Contains("GOLF"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}									
								}
								break;
							case	"GUNS"	:
								if(htpremium.Contains("GUN"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}
								}
								break;
							case	"JEWEL"	:
								if(htpremium.Contains("JWL"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}									
								}
								break;

							case	"MUSIC"	:
								if(htpremium.Contains("MSC_NON_PRF"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}									
								}
								break;

							case	"RARE"	:
								if(htpremium.Contains("RARE_COIN"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}									
								}
								break;

							case	"SILVE"	:
								if(htpremium.Contains("SLVR"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}
								}
								break;

							case	"STAMP"	:
								if(htpremium.Contains("STMP"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}
								}
								break;
							case	"PERSOL"	:
								if(htpremium.Contains("PRS_CMP"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									}
								}
								break;
							case	"PERSOD"	:
								if(htpremium.Contains("PRS_CMP"))
								{
									if (gStrPdfFor == PDFForDecPage)
									{
										//SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</SCHART_PREMIUM>";
									}
								}
								break;
						}
						#endregion
					
						#region Article Info Root Element for Dec page
						SchArticleElement.AppendChild(ArticleInfoRootElement);

						ArticleInfoRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						ArticleInfoRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEARTICLES"));
						ArticleInfoRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEARTICLES"));
						ArticleInfoRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEARTICLESEXTN"));
						ArticleInfoRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEARTICLESEXTN"));
						#endregion
					}
				
					tmpSchArticle = getHomeScheduleArticleDetailDataSet (SchArticleDetails["ITEM_ID"].ToString());
					
					//XmlElement ArticleInfoInlandElement;
					ArticleInfoInlandElement = AcordPDFXML.CreateElement("ARTICLEINFO");

					#region Article Info Details
					foreach(DataRow ArticleInfo in tmpSchArticle.Tables[0].Rows)
					{
						XmlElement ArticleInfoElement;
						ArticleInfoElement = AcordPDFXML.CreateElement("ARTICLEINFO");
						if (gStrPdfFor == PDFForDecPage)
						{
							#region Article Info Element for Dec page
							ArticleInfoRootElement.AppendChild(ArticleInfoElement);
							ArticleInfoElement.SetAttribute(fieldType,fieldTypeNormal);
							ArticleInfoElement.SetAttribute(id,tmpctr.ToString());
							ArticleInfoElement.InnerXml += "<SCHEQUIP_CAT1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_NUMBER"].ToString()) + "</SCHEQUIP_CAT1>";
							ArticleInfoElement.InnerXml += "<SCHEQUIP_CAT1DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_DESCRIPTION"].ToString()) + "</SCHEQUIP_CAT1DESC>";
							ArticleInfoElement.InnerXml += "<SCHEQUIP_CAT1SERIALNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_SERIAL_NUMBER"].ToString()) + "</SCHEQUIP_CAT1SERIALNO>";
							ArticleInfoElement.InnerXml += "<SCHEQUIP_CAT1INSUREDVALUE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</SCHEQUIP_CAT1INSUREDVALUE>";
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							/*#region Article Info for Indand Acord 81
								switch(SchArticleDetails["COV_CODE"].ToString())
								{
									case "JEWEL":
										ArticleInfoInlandElement.InnerXml += "<JEWELLERYAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</JEWELLERYAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<JEWELLERYRATE " + fieldType + "=\"" + fieldTypeText + "\"></JEWELLERYRATE>";

										if(htpremium.Contains("JWL"))
										{
											ArticleInfoInlandElement.InnerXml += "<JEWELLERYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["JWL"].ToString() + ".00")+"</JEWELLERYPREMIUM>";																			
										}
									
										break;
									case "FURS":
										ArticleInfoInlandElement.InnerXml += "<FURSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</FURSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<FURSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FURSRATE>";
										if(htpremium.Contains("FURS"))
										{
											ArticleInfoInlandElement.InnerXml += "<FURSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["FURS"].ToString() + ".00")+"</FURSAMOUNT>";
										}									
										break;
									case "FINEBR":
										ArticleInfoInlandElement.InnerXml += "<FINEARTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</FINEARTSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<FINEARTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FINEARTSRATE>";
										if(htpremium.Contains("FNE_ART_WTH_BRK"))
										{
											ArticleInfoInlandElement.InnerXml += "<FINEARTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WTH_BRK"].ToString() + ".00")+"</FINEARTSPREMIUM>";
										}									
									
										if (ArticleInfo["ITEM_INSURING_VALUE"].ToString()!="" && Double.Parse(ArticleInfo["ITEM_INSURING_VALUE"].ToString())>0)
										{
											ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">1</BREAKAGECOVERAGE>";
										}
										else
										{
											ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">0</BREAKAGECOVERAGE>";
										}
										break;
									case "FINEWBR":
										ArticleInfoInlandElement.InnerXml += "<FINEARTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</FINEARTSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<FINEARTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FINEARTSRATE>";
										if(htpremium.Contains("FNE_ART_WO_BRK"))
										{
											ArticleInfoInlandElement.InnerXml += "<FINEARTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString() + ".00")+"</FINEARTSPREMIUM>";
										}									
									
										if (ArticleInfo["ITEM_INSURING_VALUE"].ToString()!="" && Double.Parse(ArticleInfo["ITEM_INSURING_VALUE"].ToString())>0)
										{
											ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">1</BREAKAGECOVERAGE>";
										}
										else
										{
											ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">0</BREAKAGECOVERAGE>";
										}
										break;
									case "CAMER":
										ArticleInfoInlandElement.InnerXml += "<CAMERASAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</CAMERASAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<CAMERASRATE " + fieldType + "=\"" + fieldTypeText + "\"></CAMERASRATE>";
										if(htpremium.Contains("CAMER"))
										{
											ArticleInfoInlandElement.InnerXml += "<CAMERASPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["CAMER"].ToString() + ".00")+"</CAMERASPREMIUM>";
										}									
										break;
									case "MUSIC":
										ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</MUSICALINSTRUMENTSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></MUSICALINSTRUMENTSRATE>";
										if(htpremium.Contains("MUSIC"))
										{
											ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["MUSIC"].ToString() + ".00")+"</MUSICALINSTRUMENTSPREMIUM>";
										}																		
										break;
									case "SILVE":
										ArticleInfoInlandElement.InnerXml += "<SILVERWAREAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</SILVERWAREAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<SILVERWARERATE " + fieldType + "=\"" + fieldTypeText + "\"></SILVERWARERATE>";
										if(htpremium.Contains("SLVR"))
										{
											ArticleInfoInlandElement.InnerXml += "<SILVERWAREPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["SLVR"].ToString() + ".00")+"</SILVERWAREPREMIUM>";
										}																											
										break;
									case "STAMP":
										ArticleInfoInlandElement.InnerXml += "<STAMPSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</STAMPSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<STAMPSRATE " + fieldType + "=\"" + fieldTypeText + "\"></STAMPSRATE>";
										if(htpremium.Contains("STMP"))
										{
											ArticleInfoInlandElement.InnerXml += "<STAMPSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["STMP"].ToString() + ".00")+"</STAMPSPREMIUM>";
										}									
										break;
									case "RARE":
										ArticleInfoInlandElement.InnerXml += "<COINSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</COINSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<COINSRATE " + fieldType + "=\"" + fieldTypeText + "\"></COINSRATE>";
										if(htpremium.Contains("RARE_COIN"))
										{
											ArticleInfoInlandElement.InnerXml += "<COINSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["RARE_COIN"].ToString() + ".00")+"</COINSPREMIUM>";
										}										
										break;
									case "GOLF":
										ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</GOLFERSEQUIPMENTAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTRATE " + fieldType + "=\"" + fieldTypeText + "\"></GOLFERSEQUIPMENTRATE>";
										if(htpremium.Contains("GOLF"))
										{
											ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["GOLF"].ToString() + ".00")+"</GOLFERSEQUIPMENTPREMIUM>";
										}							
									
										break;
									case "PERSOD":
										ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</PERSONALCOMPUTERAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERRATE " + fieldType + "=\"" + fieldTypeText + "\"></PERSONALCOMPUTERRATE>";
										if(htpremium.Contains("PRS_CMP"))
										{
											ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</PERSONALCOMPUTERPREMIUM>";										
										}									
										break;
									case "PERSOL":
										ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</PERSONALCOMPUTERAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERRATE " + fieldType + "=\"" + fieldTypeText + "\"></PERSONALCOMPUTERRATE>";
										if(htpremium.Contains("PRS_CMP"))
										{
											//ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</PERSONALCOMPUTERPREMIUM>";										
										}					
										break;
									default:
										strOtherInfo = SchArticleDetails["COV_DES"].ToString();
										strOtherInfo = strOtherInfo + " Insuring Value:-" + ArticleInfo["ITEM_INSURING_VALUE"].ToString() + System.Environment.NewLine;
										ADDITIONALRATINGINFORMATIONElement.InnerText += strOtherInfo;
										break;
						
								}
							#endregion */
						}
						tmpctr++;
					}
					#endregion
					tmpctr = 0;
					tmpArtCtr++;
				}
				#endregion			
			}

			else
			{
				if (gStrPdfFor == PDFForAcord)
					AcordPDFXML.SelectSingleNode(RootElement).RemoveChild(Acord81RootElement);
			}
			
		}
		#endregion

		#region GeneralInfo XML For Acord 81
		private void CreateGeneralInfoXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				//DSTempDataSet = DsAcord81GeneralInfo;
				if(DsAcord81GeneralInfo.Tables[0].Rows.Count>0)
				{
					DataRow GeneralDetail = DsAcord81GeneralInfo.Tables[0].Rows[0];
					XmlElement GeneralInfoRootElement;
					GeneralInfoRootElement = AcordPDFXML.CreateElement("GeneralInfo");
					Acord81RootElement.AppendChild(GeneralInfoRootElement);
					GeneralInfoRootElement.SetAttribute(fieldType,fieldTypeSingle);

					GeneralInfoRootElement.InnerXml += "<PROTECTIVEDEVICES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["PROTDEVICE"].ToString())  + "</PROTECTIVEDEVICES>";
					GeneralInfoRootElement.InnerXml += "<PROPERTYEXHIBITED " + fieldType + "=\"" + fieldTypeText + "\"></PROPERTYEXHIBITED>";
					GeneralInfoRootElement.InnerXml += "<DEDUCTIBLEAPPLY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["ISDEDUCTIBLE"].ToString())  + "</DEDUCTIBLEAPPLY>";
					GeneralInfoRootElement.InnerXml += "<OTHERINSURANCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["OTHERINSURANCEWITHWOLVERINE"].ToString())  + "</OTHERINSURANCE>";
					GeneralInfoRootElement.InnerXml += "<LOSSOCCUR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["COVDECLINEDCANCELLED"].ToString())  + "</LOSSOCCUR>";
					GeneralInfoRootElement.InnerXml += "<COVERAGEDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["PRIORLOSS"].ToString())  + "</COVERAGEDECLINED>";
					GeneralInfoRootElement.InnerXml += "<PRIORINSURANCEPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["OLD_POLICY_NUMBER_AND_CARRIER"].ToString())  + "</PRIORINSURANCEPOLICYNUMBER>";
				}
			}
		}
		#endregion

		#region code for Acord 82 boat info Xml
		private void createBoatXML()
		{
			double dblSumTotal=0;

			DSTempDataSet = getBoatDetailsDataSet();
							
			if (gStrPdfFor == PDFForAcord)
			{
				
				if(DSTempDataSet.Tables[0].Rows.Count>0)
				{
					Acord80RootElement.SelectSingleNode("POLICY/ATTACHBOAT").InnerText = "1";
					
					gstrBoatTerritory = DSTempDataSet.Tables[0].Rows[0]["BOAT_TERRITORY"].ToString();
			
					XmlElement BoatRootElementAcord82;
					DataSet DSTempEngineTrailer;
					int BoatCtr = 0, Eng_tra_Ctr = 0;
			
					BoatRootElementAcord82    = AcordPDFXML.CreateElement("BOAT");
					Acord82RootElement.AppendChild(BoatRootElementAcord82);

					#region setting Boat Root Attribute
					BoatRootElementAcord82.SetAttribute(fieldType,fieldTypeMultiple);
					BoatRootElementAcord82.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82BOAT"));
					BoatRootElementAcord82.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOAT"));
					BoatRootElementAcord82.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82BOATEXTN"));
					BoatRootElementAcord82.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOATEXTN"));
					#endregion
				
					foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement BoatElementAcord82;
						BoatElementAcord82	= AcordPDFXML.CreateElement("BOATINFO");

						htpremium.Clear(); 
						foreach (XmlNode PremiumNode in GetPremium(BoatDetail["BOAT_ID"].ToString()))
						{
							if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
								htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
						}
				
						#region Boat Element for Acord82
						BoatRootElementAcord82.AppendChild(BoatElementAcord82);
						BoatElementAcord82.SetAttribute(fieldType,fieldTypeNormal);
						BoatElementAcord82.SetAttribute(id,BoatCtr.ToString());
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString())+"</BOATNUMBER>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATPOWER " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_POWER"].ToString())+"</BOATPOWER>";
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATHULLMATERIAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULLMATERIAL"].ToString())+"</BOATHULLMATERIAL>";
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<HULLOTHERMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULLMATERIAL_DESCRIPTION"].ToString())+"</HULLOTHERMATERIAL>";
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString())+"</BOATYEAR>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MAKE_MODEL"].ToString())+"</BOATMAKEMODEL>"; 
						
						//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'" + BoatDetail["BOAT_INCHES"].ToString() +"''") +"</BOATLENGTH>"; 

						if(BoatDetail["BOAT_INCHES"].ToString()=="")
						{
							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'") +"</BOATLENGTH>"; 
						}
						else
						{
							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'" + BoatDetail["BOAT_INCHES"].ToString() +"''") +"</BOATLENGTH>"; 
						}


						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATSPEED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_SPEED"].ToString())+"</BOATSPEED>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATDTPURCHASED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["DATE_PURCHASED"].ToString())+"</BOATDTPURCHASED>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATPVALUE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["PRESENT_VALUE"].ToString())+"</BOATPVALUE>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATHULLIDNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_ID_NO"].ToString())+"</BOATHULLIDNUM>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATWATERSNAV  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["WATERS_NAVIGATED_USE"].ToString())+"</BOATWATERSNAV>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATTERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_TERRITORY"].ToString())+"</BOATTERRITORY>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLOCATION   " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["LOCADD"].ToString()+", " + BoatDetail["LOCCITY"].ToString())+"</BOATLOCATION>"; 
						string strLayUPFrom="";
						string strLayUPTo="";
						if (BoatDetail["LAY_UP_PERIOD_FROM_MONTH"].ToString()!="0" && BoatDetail["LAY_UP_PERIOD_FROM_DAY"].ToString()!="0")
							strLayUPFrom="From "+ MonthName[BoatDetail["LAY_UP_PERIOD_FROM_MONTH"].ToString()].ToString() +" / " + BoatDetail["LAY_UP_PERIOD_FROM_DAY"].ToString() + " ";
						if (BoatDetail["LAY_UP_PERIOD_TO_MONTH"].ToString()!="0" && BoatDetail["LAY_UP_PERIOD_TO_DAY"].ToString()!="0")
							strLayUPTo=" To "+ MonthName[BoatDetail["LAY_UP_PERIOD_TO_MONTH"].ToString()].ToString() +" / " + BoatDetail["LAY_UP_PERIOD_TO_DAY"].ToString();
				
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLAYUP   " + fieldType +"=\""+ fieldTypeText +"\">" + strLayUPFrom + strLayUPTo + "</BOATLAYUP>"; 
						#endregion
				
						#region Acord82 Engine Details
						DSTempEngineTrailer = getBoatEngineDetailDataSet(BoatDetail["BOAT_ID"].ToString());
						if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
						{
							XmlElement EngineRootElement;
							EngineRootElement	= AcordPDFXML.CreateElement("ENGINE");
							BoatElementAcord82.AppendChild(EngineRootElement);
											
							#region setting Root ENGINE Attribute
							EngineRootElement.SetAttribute(fieldType,fieldTypeMultiple);
							EngineRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82ENGINE"));
							EngineRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82ENGINE"));
							EngineRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82ENGINEEXTN"));
							EngineRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82ENGINEEXTN"));
							#endregion

							foreach(DataRow EngineDetail in DSTempEngineTrailer.Tables[0].Rows)
							{
								XmlElement EngineElement;
								EngineElement = AcordPDFXML.CreateElement("ENGINEINFO");
								EngineRootElement.AppendChild(EngineElement);
								EngineElement.SetAttribute(fieldType,fieldTypeNormal);
								EngineElement.SetAttribute(id,Eng_tra_Ctr.ToString());

								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["YEAR"].ToString())+"</ENGINEYEAR>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["MAKE_MODEL"].ToString())+"</ENGINEMAKEMODEL>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINESRNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["SERIAL_NO"].ToString())+"</ENGINESRNUM>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEHP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["HORSEPOWER"].ToString())+"</ENGINEHP>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEFUELTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(EngineDetail["FUEL_TYPE"].ToString())+"</ENGINEFUELTYPE>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEPRESENTVALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["PRESENT_VALUE"].ToString())+"</ENGINEPRESENTVALUE>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEOTHER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["OTHER"].ToString())+"</ENGINEOTHER>"; 

								Eng_tra_Ctr++;
							}
							Eng_tra_Ctr = 0;
						}
						#endregion
				
						#region Acord82 trailer Details
						DSTempEngineTrailer = getSchEquipmentsDataSet(BoatDetail["BOAT_ID"].ToString());
						if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
						{
							XmlElement TrailerRootElement;
							TrailerRootElement = AcordPDFXML.CreateElement("TRAILER"); 
							BoatElementAcord82.AppendChild(TrailerRootElement);

							#region setting Root TRAILER Attribute
							TrailerRootElement.SetAttribute(fieldType,fieldTypeMultiple);
							TrailerRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82TRAILER"));
							TrailerRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82TRAILER"));
							TrailerRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82TRAILEREXTN"));
							TrailerRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82TRAILEREXTN"));
							#endregion

					
							foreach(DataRow TrailerDetail in DSTempEngineTrailer.Tables[0].Rows)
							{
								XmlElement TrailerElement;
								TrailerElement = AcordPDFXML.CreateElement("TRAILERINFO");
								TrailerRootElement.AppendChild(TrailerElement);
								TrailerElement.SetAttribute(fieldType,fieldTypeNormal);
								TrailerElement.SetAttribute(id,Eng_tra_Ctr.ToString());
					
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["YEAR"].ToString())+"</TRAILERYEAR>"; 
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["MANUFACTURER"].ToString())+"</TRAILERMAKEMODEL>"; 
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERSRNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["SERIAL_NO"].ToString())+"</TRAILERSRNUM>"; 
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERCOST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["INSURED_VALUE"].ToString())+"</TRAILERCOST>"; 
					
								Eng_tra_Ctr++;
							}
							Eng_tra_Ctr = 0;
						}
						#endregion

						#region COVERAGES
						dblSumTotal=0;
						DSTempEngineTrailer =getBoatCoveragesDataSet(BoatDetail["BOAT_ID"].ToString());
						foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
						{
							string prem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
							prem = prem.Replace("$","");
							if(prem != "" && prem != "Included")
								dblSumTotal+=int.Parse(prem.Replace(".00",""));
							switch(CoverageDetails["COV_CODE"].ToString())
							{
								case "EBPPDACV":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVHULLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVHULLDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVHULLPREM>";
									if(htpremium.Contains("BOAT_PD"))
									{
										gstrGetPremium = htpremium["BOAT_PD"].ToString();
										gintGetindex = gstrGetPremium.IndexOf(".");
										//										if(gintGetindex == -1)
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</BOATCOVHULLPREM>";
										//										else
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</BOATCOVHULLPREM>";
										//										if(prem != "" && prem != "Included")
										//											//dblSumTotal+=int.Parse(prem);
										//										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "EBPPDAV":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVHULLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVHULLDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVHULLPREM>";
									if(htpremium.Contains("BOAT_PD"))
									{
										//gstrGetPremium	=	htpremium["BOAT_PD"].ToString();
										gintGetindex	=	gstrGetPremium.IndexOf(".");
										//										if(gintGetindex==	-1)
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</BOATCOVHULLPREM>";
										//										else
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</BOATCOVHULLPREM>";
										//										if(prem != "" && prem != "Included")
										//											//dblSumTotal+=int.Parse(prem);
										//										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "OUTBOARD1":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM1LIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVOM1LIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM1DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVOM1DED>";
									break;
								case "OUTBOARD2":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM2LIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVOM2LIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM2DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVOM2DED>";
									break;
								case "PORTACCESS":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVPORTACCLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVPORTACCDED>";
									break;
								case "TRAILER":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVTRAILERLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVTRAILERDED>";

									break;
								case "LCCSL":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVCSLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVCSLDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVCSLPREM>";
									if(htpremium.Contains("BOAT_ LIABILITY_PREMIUM"))
									{
										//gstrGetPremium	=	htpremium["BOAT_ LIABILITY_PREMIUM"].ToString();
										gintGetindex	=	gstrGetPremium.IndexOf(".");
										//										if(gintGetindex==	-1)
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString() + ".00")+"</BOATCOVCSLPREM>";
										//										else
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString())+"</BOATCOVCSLPREM>";
										//										if(prem != "" && prem != "Included")
										//											//dblSumTotal+=int.Parse(prem);
										//										//dblSumTotal+=int.Parse(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString());
									}
									break;
								case "MCPAY":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVMEDPMLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVMEDPMDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVMEDPMPREM>";
									if(htpremium.Contains("BOAT_MP_PREMIUM"))
									{
										//gstrGetPremium	=	htpremium["BOAT_MP_PREMIUM"].ToString();
										gintGetindex	=	gstrGetPremium.IndexOf(".");
										//										if(gintGetindex==	-1)
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString() + ".00")+"</BOATCOVMEDPMPREM>";
										//										else
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString())+"</BOATCOVMEDPMPREM>";
										//										if(prem != "" && prem != "Included")
										//											//dblSumTotal+=int.Parse(prem);
										//										//dblSumTotal+=int.Parse(htpremium["BOAT_MP_PREMIUM"].ToString());
									}
									break;
								case "UMBCS":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVUBCSLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVUBCSLDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVUBCSLPREM>";
									if(htpremium.Contains("BOAT_UB_PREMIUM"))
									{
										//gstrGetPremium	=	htpremium["BOAT_UB_PREMIUM"].ToString();
										gintGetindex	=	gstrGetPremium.IndexOf(".");
										//										if(gintGetindex==	-1)
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString() + ".00")+"</BOATCOVUBCSLPREM>";
										//										else
										//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString())+"</BOATCOVUBCSLPREM>";
										//										if(prem != "" && prem != "Included")
										//											//dblSumTotal+=int.Parse(prem);
										//										//dblSumTotal+=int.Parse(htpremium["BOAT_UB_PREMIUM"].ToString());
									}
									break;
							
							}
					
						}

						if (gStrPdfFor == PDFForAcord)
						{
							if(htpremium.Contains("SUMTOTAL"))
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTTOTAL " + fieldType +"=\""+ fieldTypeText +"\">"+(dblSumTotal.ToString()  + ".00" )+"</BOATCOVTTOTAL>";
							}
					
						}
						#endregion
				
						BoatCtr++;
					}
				}
				else
					AcordPDFXML.SelectSingleNode(RootElement).RemoveChild(Acord82RootElement);
			}
		}

		#endregion

		#region Acord 82 Code for Boat Trailer Addl Interests
		private void createAcord82BoatAddlIntXml()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				DSTempDataSet = getBoatAddInterestDataSet("0");
				#region Acord82 Page
				if (DSTempDataSet.Tables[0].Rows.Count >0)
				{
					XmlElement Acord82AddlInts;
					Acord82AddlInts = AcordPDFXML.CreateElement("ADDITIONALINTERESTS");
					Acord82RootElement.AppendChild(Acord82AddlInts);
					Acord82AddlInts.SetAttribute(fieldType,fieldTypeMultiple);
					Acord82AddlInts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82ADDLINT"));
					Acord82AddlInts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82ADDLINT"));
					Acord82AddlInts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82ADDLINTEXTN"));
					Acord82AddlInts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82ADDLINTEXTN"));

					int RowCounter = 0;
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord82ADDLINTElement;
						Acord82ADDLINTElement = AcordPDFXML.CreateElement("ADDLINTINFO");
						Acord82AddlInts.AppendChild(Acord82ADDLINTElement);
						Acord82ADDLINTElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord82ADDLINTElement.SetAttribute(id,RowCounter.ToString());

						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTTYPEOFINT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString()) + "</ADDLINTTYPEOFINT>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HOLDER_NAME"].ToString()) + "</ADDLINTNAME>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["ADDRESS"].ToString()) + "</ADDLINTADD>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTLOANNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["BOAT_NO"].ToString() + "/" + Row["LOAN_REF_NUMBER"].ToString()) + "</ADDLINTLOANNUM>";
					
						RowCounter++;
					}
				}
				#endregion
			}
		}
		#endregion

		#region Code for Acord82Operators
		private void createAcord82OperatorXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				DSTempDataSet = getOperatorDataSet();
				#region Acord82 Page
				if (DSTempDataSet.Tables[0].Rows.Count >0)
				{
					XmlElement Acord82Operators;
					Acord82Operators = AcordPDFXML.CreateElement("OPERATORS");
					Acord82RootElement.AppendChild(Acord82Operators);
					Acord82Operators.SetAttribute(fieldType,fieldTypeMultiple);
					Acord82Operators.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82OPERATOR"));
					Acord82Operators.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPERATOR"));
					Acord82Operators.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82OPERATOREXTN"));
					Acord82Operators.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPERATOREXTN"));

					int RowCounter = 0;
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord82OperatorElement;
						Acord82OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
						Acord82Operators.AppendChild(Acord82OperatorElement);
						Acord82OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord82OperatorElement.SetAttribute(id,RowCounter.ToString());
						string strOprSSn="";
						strOprSSn =  Row["DRIVER_SSN"].ToString();
						if(strOprSSn !="" && strOprSSn !="0")
						{
							strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(Row["DRIVER_SSN"].ToString());
							
							if(strOprSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
							{
								string strvaln = "xxx-xx-";
								strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
								strOprSSn = strvaln;
							}
							else
								strOprSSn="";

						}

						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OPERATORNO"].ToString()) + "</OPERATORNUMBER>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_NAME"].ToString()) + "</OPERATORNAME>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORSEX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_SEX"].ToString()) + "</OPERATORSEX>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_DOB"].ToString()) + "</OPERATORDOB>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORAUTODRVLIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_DRIV_LIC"].ToString()) + "</OPERATORAUTODRVLIC>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORLICSTATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STATE_NAME"].ToString()) + "</OPERATORLICSTATE>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_SSN"].ToString()) + "</OPERATORSSN>";

						RowCounter++;
					}
				}
				#endregion
			}
		}
		#endregion

		#region Code for Acord82 Operator Experience and Violations
		private void createAcord82OperatorExpViolationXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				DSTempDataSet = getBoatOperatorExpDtlsDataSet();
				#region Acord82 Page
				if (DSTempDataSet.Tables[0].Rows.Count >0)
				{
					XmlElement Acord82OperatorViolations;
					Acord82OperatorViolations = AcordPDFXML.CreateElement("VIOLATIONS");
					Acord82RootElement.AppendChild(Acord82OperatorViolations);
					Acord82OperatorViolations.SetAttribute(fieldType,fieldTypeMultiple);
					Acord82OperatorViolations.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82OPREXP"));
					Acord82OperatorViolations.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPREXP"));
					Acord82OperatorViolations.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82OPREXPEXTN"));
					Acord82OperatorViolations.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPREXPEXTN"));

					int RowCounter = 0;
					
					string tempStr="0";
					//To fill the Operators and MVR details on main Page.
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						if(tempStr!=Row["DRIVER_ID"].ToString())
						{
							XmlElement Acord82ViolationElement;
							Acord82ViolationElement = AcordPDFXML.CreateElement("VIOLATIONINFO");
							Acord82OperatorViolations.AppendChild(Acord82ViolationElement);
							Acord82ViolationElement.SetAttribute(fieldType,fieldTypeNormal);
							Acord82ViolationElement.SetAttribute(id,RowCounter.ToString());
							
							
							Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OPERATORNO"].ToString()) + "</OPERATOREXPNUM>";
							Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EXPDESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
							RowCounter++;
						}
						tempStr=Row["DRIVER_ID"].ToString();
							
					}
					tempStr="0";
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						if(tempStr==Row["DRIVER_ID"].ToString())
						{
							XmlElement Acord82ViolationElement1;
							Acord82ViolationElement1 = AcordPDFXML.CreateElement("VIOLATIONINFO");
							Acord82OperatorViolations.AppendChild(Acord82ViolationElement1);
							Acord82ViolationElement1.SetAttribute(fieldType,fieldTypeNormal);
							Acord82ViolationElement1.SetAttribute(id,RowCounter.ToString());
							
						
							Acord82ViolationElement1.InnerXml = Acord82ViolationElement1.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OPERATORNO"].ToString()) + "</OPERATOREXPNUM>";
							Acord82ViolationElement1.InnerXml = Acord82ViolationElement1.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EXPDESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
							RowCounter++;
						}
						tempStr=Row["DRIVER_ID"].ToString();
							
					}

				}
				#endregion
			}
		}
		#endregion

		#region Code for Acord 82 Underwriting And General Info Xml Generation
		private void createBoatUnderwritingGeneralXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				DSTempDataSet = getBoatUnderWrittingDataSet();
				if(DSTempDataSet.Tables[0].Rows.Count >0)
				{
					#region Acord82 Page General Info
					XmlElement Acord82GenInfoElement;
					Acord82GenInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
					Acord82RootElement.AppendChild(Acord82GenInfoElement);
					Acord82GenInfoElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOAPPLIVCURADD3YR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LIVED_CURR_ADD_3YR"].ToString()) + "</GENINFOAPPLIVCURADD3YR>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOPRPHYIMPAIRMENT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED"].ToString()) + "</GENINFOOPRPHYIMPAIRMENT>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFODRVLICSUSPEND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED"].ToString()) + "</GENINFODRVLICSUSPEND>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOPRHADACCIDENT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT"].ToString()) + "</GENINFOOPRHADACCIDENT>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOTHINSUWITHCOMP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED"].ToString()) + "</GENINFOOTHINSUWITHCOMP>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOANYLOSS3YRS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_LOSS_THREE_YEARS"].ToString()) + "</GENINFOANYLOSS3YRS>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOCOVGDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED"].ToString()) + "</GENINFOCOVGDECLINED>";
					//Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOREMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REMARKGENINFO"].ToString()) + "</GENINFOREMARKS>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<ATTACHPHOTOGRAPH " + fieldType + "=\"" + fieldTypeText + "\">" + getPhotoAttach + "</ATTACHPHOTOGRAPH>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<ATTACHSURVEY " + fieldType + "=\"" + fieldTypeText + "\">" + getMarineSurvey + "</ATTACHSURVEY>";
				
					#endregion
				}
					
			}
		}
		#endregion

		#region code for dwelling info Xml
		private int GetLowestPrnIndex(ref int []prnOrder, int prnCount)
		{
			int retIndex = 0;
			for(int prnIndex = 0; prnIndex < prnCount; prnIndex++)
			{
				if(prnOrder[prnIndex] < prnOrder[retIndex])
					retIndex = prnIndex;
			}
			prnOrder[retIndex] = 999999;
			return retIndex;
		}

		private string AfterComma(int number)
		{
			string num = number.ToString();
			if(num.Length == 1)
				num = "00" + num;
			else if(num.Length == 2)
				num = "0" + num;
			return num;
		}

		private void createDwellingXML()
		{
			XmlElement DecPageDwellMultipleElement;
			DecPageDwellMultipleElement    = AcordPDFXML.CreateElement("HOME");
			
			XmlElement Acord80DwellMultipleElement;
			Acord80DwellMultipleElement    = AcordPDFXML.CreateElement("HOME");
			
			XmlElement SupplementDwellMultipleElement;
			SupplementDwellMultipleElement    = AcordPDFXML.CreateElement("HOME");
			
			XmlElement DecPageDwellMultipleElement0;
			DecPageDwellMultipleElement0    = AcordPDFXML.CreateElement("HOME0");
			
			XmlElement DecPageHomeElement0;
			DecPageHomeElement0	= AcordPDFXML.CreateElement("HOMEINFO0");

			int DwellingCtr = 0,AddInt=0;			

			#region setting Dwelling Root Attribute
			if (gStrPdfFor == PDFForDecPage)
			{
				prnOrd_covCode = new string[100];
				prnOrd_attFile = new string[100];
				prnOrd = new int[100];
				#region Dwelling Root Element for DecPage
				DecPageRootElement.AppendChild(DecPageDwellMultipleElement0);
				DecPageDwellMultipleElement0.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageDwellMultipleElement0.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHOME"));
				DecPageDwellMultipleElement0.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOME"));
				DecPageDwellMultipleElement0.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEEXTN"));
				DecPageDwellMultipleElement0.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEEXTN"));
				#endregion

				// Added by Mohit Agarwal for Customer Details irrespective of added boats
				// 14-Mar-2007
				#region setting Customer Agency Details
				DecPageDwellMultipleElement0.AppendChild(DecPageHomeElement0);
				DecPageHomeElement0.SetAttribute(fieldType,fieldTypeNormal);
				DecPageHomeElement0.SetAttribute(id,DwellingCtr.ToString());
				if(string.Compare(gStrCalledFrom,"Policy",true)==0)
				{
					DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNO>";
					DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYPERIODFROM>";
					DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYPERIODTO>";
				}
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</PRIMARYCONTACTNAME>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<PRIMARYCONTACTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</PRIMARYCONTACTADDRESS>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<PRIMARYCONTACTCITY " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</PRIMARYCONTACTCITY>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</AGENCYADDRESS>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</AGENCYCITYSTATEZIP>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYPHONENO " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</AGENCYPHONENO>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</AGENCYSUBCODE>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</AGENCYBILLING>";

				if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
					createPage2XML(ref DecPageHomeElement0);
				//Reason Code
				//				DecPageHomeElement0.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
				//				DecPageHomeElement0.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
				//				DecPageHomeElement0.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
				//				DecPageHomeElement0.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
				#endregion

				DecPageHomeElement0.AppendChild(DecPageDwellMultipleElement);
				DecPageDwellMultipleElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageDwellMultipleElement.SetAttribute(PrimPDF,"");
				DecPageDwellMultipleElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOME"));
				DecPageDwellMultipleElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEEXTN"));
				DecPageDwellMultipleElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEEXTN"));
			}
			else if (gStrPdfFor == PDFForAcord)
			{
				#region Dwelling Root Element for Accord 80
				Acord80RootElement.AppendChild(Acord80DwellMultipleElement);
				Acord80DwellMultipleElement.SetAttribute(fieldType,fieldTypeMultiple);
				Acord80DwellMultipleElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80HOME"));
				Acord80DwellMultipleElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80HOME"));
				Acord80DwellMultipleElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80HOMEEXTN"));
				Acord80DwellMultipleElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80HOMEEXTN"));
				#endregion

				#region Dwelling Root Element for Accord 80 Supplement
				SupplementalRootElement.AppendChild(SupplementDwellMultipleElement);
				SupplementDwellMultipleElement.SetAttribute(fieldType,fieldTypeMultiple);
				SupplementDwellMultipleElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTHOME"));
				SupplementDwellMultipleElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTHOME"));
				SupplementDwellMultipleElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTHOMEEXTN"));
				SupplementDwellMultipleElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTHOMEEXTN"));
				#endregion			
			}
			#endregion

			DataSet DSDwellingDetails = DsDwellingDetails;
			if (gStrPdfFor == PDFForAcord)
			{
				#region Acord 81 Page
				if (DSDwellingDetails.Tables[0].Rows.Count>0)
				{
					XmlElement Acord81RatingInfo;
					Acord81RatingInfo = AcordPDFXML.CreateElement("RATINGINFO");
					Acord81RootElement.AppendChild(Acord81RatingInfo);
					Acord81RatingInfo.SetAttribute(fieldType,fieldTypeSingle);
				
					Acord81RatingInfo.InnerXml += "<APPLICANTPROTECTCLASS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSDwellingDetails.Tables[0].Rows[0]["PROT_CLASS"].ToString()) +"</APPLICANTPROTECTCLASS>"; 
					//if(!strAppAddress1.Equals(DSTempDataSet.Tables[0].Rows[0]["LOC_ADD1"].ToString()) && !strAppAddress2.Equals(DSTempDataSet.Tables[0].Rows[0]["LOC_ADD2"].ToString()) && !strAppCity.Equals(DSTempDataSet.Tables[0].Rows[0]["LOC_CITY"].ToString()) && !strAppState.Equals(DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString()) && !strAppZip.Equals(DSTempDataSet.Tables[0].Rows[0]["LOC_ZIP"].ToString())) 
					if(ApplicantAddress.Trim() != DSDwellingDetails.Tables[0].Rows[0]["LOC_ADDRESS"].ToString().Trim() || ApplicantCityStZip.Trim() != DSDwellingDetails.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString().Trim())
					{
						Acord81RatingInfo.InnerXml += "<LOCATIONADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSDwellingDetails.Tables[0].Rows[0]["LOC_ADDRESS"].ToString()) +"</LOCATIONADDRESS>"; 
						Acord81RatingInfo.InnerXml += "<LOCATIONCITYSTZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSDwellingDetails.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString()) +"</LOCATIONCITYSTZIP>";
					}
					Acord81RatingInfo.InnerXml += "<DWELLINGTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSDwellingDetails.Tables[0].Rows[0]["DWELLING_TYPE"].ToString()) +"</DWELLINGTYPE>"; 
					Acord81RatingInfo.InnerXml += "<CONSTRUCTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSDwellingDetails.Tables[0].Rows[0]["CONSTRUCTION_TYPE"].ToString()) +"</CONSTRUCTION>"; 
					Acord81RatingInfo.InnerXml += "<NOFAMILIES " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSDwellingDetails.Tables[0].Rows[0]["NO_OF_FAMILIES"].ToString()) +"</NOFAMILIES>";
				
					//ACORD 80 ATTACHMENTS
					Acord80RootElement.SelectSingleNode("POLICY/ATTACHPROTDEVICE").InnerText = RemoveJunkXmlCharacters(DSDwellingDetails.Tables[0].Rows[0]["ALARM_CERT_ATTACHED"].ToString());
				}
				else
					AcordPDFXML.SelectSingleNode(RootElement).RemoveChild(SupplementalRootElement);
				#endregion
			}	
			foreach(DataRow DwellingDetail in DSDwellingDetails.Tables[0].Rows)
			{
				#region Dwelling Details 
				XmlElement DecPageHomeElement;
				DecPageHomeElement	= AcordPDFXML.CreateElement("HOMEINFO");

				XmlElement Acord80HomeElement;
				Acord80HomeElement	= AcordPDFXML.CreateElement("HOMEINFO");

				XmlElement SupplementHomeElement;
				SupplementHomeElement	= AcordPDFXML.CreateElement("HOMEINFO");

				LoadRateXML("HOME");

				DataSet DSTempDwelling =	getHomeCoveragesDataSet(gStrClientID,gStrPolicyId,gStrPolicyVersion,DwellingDetail["DWELLING_ID"].ToString());
				htpremium.Clear(); 
				foreach (XmlNode PremiumNode in GetPremium(DwellingDetail["DWELLING_ID"].ToString()))
				{
					if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
						htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
				}
				
				double sumTtl=0;
				if(gStrtemp !="final")
				{
					foreach (XmlNode SumTotalNode in GetSumTotalPremium(DwellingDetail["DWELLING_ID"].ToString()))
					{
						if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
							sumTtl += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
					}
				}
				else
				{
					string sumtotal = GetPremiumAll(DSTempDwelling, "SUMTOTAL", DwellingDetail["DWELLING_ID"].ToString());
					if (sumtotal != "")
						sumTtl = double.Parse(sumtotal);
				}

				XmlNodeList ins_scoreNodeList = GetCreditForHomeInsScore();
				XmlNode ins_scoreNode;
				if(ins_scoreNodeList.Count > 0)
				{
					ins_scoreNode = ins_scoreNodeList.Item(0);
					String [] discRows = getAttributeValue(ins_scoreNode,"STEPDESC").Split('-');

					if(discRows.Length >= 1)
						inspercent = discRows[discRows.Length -1];
				}

				if (gStrPdfFor == PDFForDecPage)
				{
					#region Dwelling Element for Dec Page
					DecPageDwellMultipleElement.AppendChild(DecPageHomeElement);
					DecPageHomeElement.SetAttribute(fieldType,fieldTypeNormal);
					DecPageHomeElement.SetAttribute(id,DwellingCtr.ToString());

					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<PREMISESDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["LOC_ADDRESS"].ToString()) + ", " + RemoveJunkXmlCharacters(DwellingDetail["LOC_CITYSTATEZIP"].ToString() + DwellingDetail["INFLATION_PRECENT"].ToString()) +"</PREMISESDESCRIPTION>"; 
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<OCCUPANCY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["OCCUPANCY"].ToString())+"</OCCUPANCY>"; 
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<CONSTRUCTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["CONSTRUCTION_TYPE"].ToString())+"</CONSTRUCTION>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<YEARCONSTRUCTION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["YEAR_BUILT"].ToString())+"</YEARCONSTRUCTION>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<MARKETVALUE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["MARKET_VALUE"].ToString())+"</MARKETVALUE>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<COUNTY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["COUNTY"].ToString()) +"</COUNTY>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<FIREHYDRANT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HYDRANTDEC"].ToString() + " FT")+"</FIREHYDRANT>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<FIRESTATION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["FIRE_STATION_DIST"].ToString() + " MI" )+"</FIRESTATION>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<PROTECTIONCLASS  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PROT_CLASS"].ToString())+"</PROTECTIONCLASS>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<TERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+gstrBoatTerritory+"</TERRITORY>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<INSURANCESCORE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE"].ToString()) +"</INSURANCESCORE>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<INSURANCEPERCENTAGE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(inspercent) +"</INSURANCEPERCENTAGE>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<PERCENTAGETYPE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE_TYPE"].ToString()) +"</PERCENTAGETYPE>";

				
					#endregion
				}	
				else if (gStrPdfFor == PDFForAcord)
				{
					#region Dwelling Element for Accord 80	
					Acord80DwellMultipleElement.AppendChild(Acord80HomeElement);
					Acord80HomeElement.SetAttribute(fieldType,fieldTypeNormal);
					Acord80HomeElement.SetAttribute(id,DwellingCtr.ToString());

					string dwellingID=	DwellingDetail["DWELLING_ID"].ToString();
			
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<HOFORM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HO_FORM"].ToString()) +"</HOFORM>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ENDORSEMENTDESC " + fieldType +"=\""+ fieldTypeText +"\">See Homeowners Application Supplement</ENDORSEMENTDESC>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<DEPOSITAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["RECEIVED_PRMIUM"].ToString()) +"</DEPOSITAMOUNT>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ESTIMATEDTOTALPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ (sumTtl + ".00") +"</ESTIMATEDTOTALPREMIUM>"; 
					if (sumTtl!=0)
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<BALANCEAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+ (sumTtl - (DwellingDetail["RECEIVED_PRMIUM"].ToString()==""?0:double.Parse(DwellingDetail["RECEIVED_PRMIUM"].ToString()) ) ) + ".00" +"</BALANCEAMOUNT>"; 
					
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<CONSTRUCTIONCODE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["EXTERIOR_CONSTRUCTION"].ToString()) +"</CONSTRUCTIONCODE>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<EXTERIOR_CONSTRUCTION_DESC " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["EXTERIOR_CONSTRUCTION_DESC"].ToString()) +"</EXTERIOR_CONSTRUCTION_DESC>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<YRBUILT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["YEAR_BUILT"].ToString()) +"</YRBUILT>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<MARKETVALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["MARKET_VALUE"].ToString()) +"</MARKETVALUE>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<NOAPTS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NEED_OF_UNITS"].ToString()) +"</NOAPTS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<REPLACEMENTCOST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["REPLACEMENT_COST"].ToString()) +"</REPLACEMENTCOST>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<CONSTRUCTIONTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["STRUCTURE_TYPE"].ToString()) +"</CONSTRUCTIONTYPE>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<USAGETYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["USAGE_TYPE"].ToString()) +"</USAGETYPE>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<FARM " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["ANY_FORMING"].ToString()) +"</FARM>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<BUILDERSRISK " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["COC"].ToString()) +"</BUILDERSRISK>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<COMPDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["COMP_DATE"].ToString()) +"</COMPDATE>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<NOFAMILIES " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NO_OF_FAMILIES"].ToString()) +"</NOFAMILIES>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PURCHASEDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PUR_YR_PRICE"].ToString()) +"</PURCHASEDATE>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONCLASS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PROT_CLASS"].ToString()) +"</PROTECTIONCLASS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<DISTANCEHYDRANT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HYDRANTDEC"].ToString()) +"</DISTANCEHYDRANT>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<DISTANCEFIRESTATION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["FIRE_STATION_DIST"].ToString()) +"</DISTANCEFIRESTATION>";
					if(DwellingDetail["CENT_ST_BURG_FIRE"].ToString()=="Y" || DwellingDetail["CENT_ST_FIRE"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKECENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPESMOKECENTRAL>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKECENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPESMOKECENTRAL>";
					}	

					if(DwellingDetail["CENT_ST_BURG_FIRE"].ToString()=="Y" || DwellingDetail["CENT_ST_BURG"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPEBURGLARCENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPEBURGLARCENTRAL>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPEBURGLARCENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPEBURGLARCENTRAL>";
					}	

					if(DwellingDetail["DIR_FIRE_AND_POLICE"].ToString()=="Y" || DwellingDetail["DIR_FIRE"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKEDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPESMOKEDIRECT>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKEDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPESMOKEDIRECT>";
					}	
					if(DwellingDetail["DIR_FIRE_AND_POLICE"].ToString()=="Y" || DwellingDetail["DIR_POLICE"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPEBURGLARDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPEBURGLARDIRECT>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPEBURGLARDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPEBURGLARDIRECT>";
					}	

					if(DwellingDetail["LOC_FIRE_GAS"].ToString()=="Y" || DwellingDetail["TWO_MORE_FIRE"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKELOCAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPESMOKELOCAL>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKELOCAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPESMOKELOCAL>";
					}	
				
					if(DwellingDetail["PRIMARY_HEAT_OTHER_DESC"].ToString().Length <= 0)
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PRIMARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PHEAT_TYPE"].ToString()) +"</PRIMARYHEATTYPE>";	
					else
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PRIMARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PRIMARY_HEAT_OTHER_DESC"].ToString()) +"</PRIMARYHEATTYPE>";	

					if(DwellingDetail["SECONDARY_HEAT_OTHER_DESC"].ToString().Length <= 0)
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SECONDARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SHEAT_TYPE"].ToString()) +"</SECONDARYHEATTYPE>";
					else
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SECONDARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SECONDARY_HEAT_OTHER_DESC"].ToString()) +"</SECONDARYHEATTYPE>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPARTWIRING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["WIRING"].ToString()) +"</RENOVATIONTYPEPARTWIRING>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEWIRINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["WIRING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEWIRINGYEAR>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPARTPLUMBING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PLUMBING"].ToString()) +"</RENOVATIONTYPEPARTPLUMBING>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPLUMBINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PLUMBING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEPLUMBINGYEAR>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPARTHEATING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HEATING"].ToString()) +"</RENOVATIONTYPEPARTHEATING>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEHEATINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HEATING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEHEATINGYEAR>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPARTROOFING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["ROOFING"].ToString()) +"</RENOVATIONTYPEPARTROOFING>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEROOFINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["ROOFING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEROOFINGYEAR>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<NOAMPS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NO_OF_AMPS"].ToString()) +"</NOAMPS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<CIRCUITBREAKERS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["CIRCUIT_BREAKERS"].ToString()) +"</CIRCUITBREAKERS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<FOUNDATIONOPEN " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["FOUNDATION"].ToString()) +"</FOUNDATIONOPEN>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<OCCUPANCY_CODE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["OCCUPANCY_CODE"].ToString()) +"</OCCUPANCY_CODE>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<NOWEEKSRENTED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NO_WEEKS_RENTED"].ToString()) +"</NOWEEKSRENTED>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<VISIBLETONEIGHBOURS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NEIGHBOURS_VISIBLE"].ToString()) +"</VISIBLETONEIGHBOURS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ISSWIMMINGPOOL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SWIMMING_POOL"].ToString()) +"</ISSWIMMINGPOOL>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SWIMMINGPOOLPOSITION " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SWIMMING_POOL_TYPE"].ToString()) +"</SWIMMINGPOOLPOSITION>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SWIMMINGPOOLAPPROVEDFENCE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["APPROVED_FENCE"].ToString()) +"</SWIMMINGPOOLAPPROVEDFENCE>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SWIMMINGPOOLDIVINGBOARD " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["DIVING_BOARD"].ToString()) +"</SWIMMINGPOOLDIVINGBOARD>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SWIMMINGPOOLSLICE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SLIDE"].ToString()) +"</SWIMMINGPOOLSLICE>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<INSPECTED " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["INSPECTED"].ToString()) +"</INSPECTED>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<OCCUPIEDDAILY " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["OCCUPIED_DAILY"].ToString()) +"</OCCUPIEDDAILY>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<TERRCODE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["TERR"].ToString()) +"</TERRCODE>";
					string premgrp = "";
					foreach (XmlNode PremiumNode in GetRentalPremGroup())
					{
						premgrp=getAttributeValue(PremiumNode,"GROUPDESC").ToString() ;
						if(premgrp.Length>0 )
						{
							premgrp = premgrp.Replace("Premium Group:","");
							if(premgrp.IndexOf(",") > 1)
								premgrp = premgrp.Substring(1, premgrp.IndexOf(",")-1);
							else
								premgrp = premgrp.Substring(1, premgrp.Length - 1);
							break;
						}
					}
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PREMGROUP " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ RemoveJunkXmlCharacters(premgrp)  +"</PREMGROUP>";

					if(DwellingDetail["LOOKUP_VALUE_DESC"].ToString()!="")
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ROOFMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["LOOKUP_VALUE_DESC"].ToString())  +"</ROOFMATERIAL>";					
					else
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ROOFMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["ROOF_OTHER_DESC"].ToString())  +"</ROOFMATERIAL>";

				
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SPRINKLER " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["SPRINKER"].ToString())  +"</SPRINKLER>";
					#endregion

					#region Dwelling Element for Supplement	
					SupplementDwellMultipleElement.AppendChild(SupplementHomeElement);
					SupplementHomeElement.SetAttribute(fieldType,fieldTypeNormal);
					SupplementHomeElement.SetAttribute(id,DwellingCtr.ToString());

					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<APPLICANTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ ApplicantName +"</APPLICANTNAME>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<APPLICANTNO " + fieldType +"=\""+ fieldTypeText +"\">"+ PolicyNumber +"</APPLICANTNO>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<INSURANCESCORE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE"].ToString()) +"</INSURANCESCORE>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<INSURANCEPERCENTAGE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(inspercent) +"</INSURANCEPERCENTAGE>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<ENCLOSEDSWIMMINGPOOL " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["IS_SWIMPOLL_HOTTUB"].ToString()) +"</ENCLOSEDSWIMMINGPOOL>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml +  "<PERCENTAGETYPE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE_TYPE"].ToString()) +"</PERCENTAGETYPE>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml +  "<insuranceCredit " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(sumTtl.ToString()) +"</insuranceCredit>";
					#endregion
				}
				#endregion

				#region Coverages
				//double dblSumTotal=0;
				//int RowCounter=0;
				double red_covc = 0.00;
				DataRow[] drCovg= DSTempDwelling.Tables[0].Select("COV_CODE='REDUC'");
				if(drCovg.Length > 0 &&  drCovg[0]["LIMIT_1"].ToString() != "")
				{
					try { red_covc = double.Parse(drCovg[0]["LIMIT_1"].ToString()); }catch (Exception ex)
                    { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
				}
				/*
				foreach(DataRow CoverageDetails in DSTempDwelling.Table.Rows)
				{
					if(CoverageDetails["COV_CODE"].ToString() == "REDUC" && CoverageDetails["LIMIT_1"].ToString() != "")
					{
						try { red_covc = double.Parse(CoverageDetails["LIMIT_1"].ToString()); }
						catch (Exception ex) {}
						break;
					}
				}*/
				foreach(DataRow CoverageDetails in DSTempDwelling.Tables[0].Rows)
				{
					#region Dec Page Coverages
					string CovCode = CoverageDetails["COV_CODE"].ToString();
					
					switch(CoverageDetails["COV_CODE"].ToString())
					{
						case "DWELL":
							if (gStrPdfFor == PDFForDecPage)
							{
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEADWELLING " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGEADWELLING>";
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEADWELLINGLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</PC_COVERAGEADWELLINGLIMIT>";
								//								if(htpremium.Contains("COV_A"))
							{
								//gstrGetPremium	=	htpremium["COV_A"].ToString();
								gintGetindex	=	gstrGetPremium.IndexOf(".");
								//									if(gintGetindex==	-1)
								//										DecPageHomeElement.InnerXml += "<PC_COVERAGEADWELLINGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_A"].ToString() + ".00")+"</PC_COVERAGEADWELLINGPREMIUM>";
								//									else
								//										DecPageHomeElement.InnerXml += "<PC_COVERAGEADWELLINGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_A"].ToString())+"</PC_COVERAGEADWELLINGPREMIUM>";
								DecPageHomeElement.InnerXml += "<PC_COVERAGEADWELLINGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, CovCode))+"</PC_COVERAGEADWELLINGPREMIUM>";
								//dblSumTotal+=int.Parse(htpremium["COV_A"].ToString());
							}
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<DWELLINGAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) +"</DWELLINGAMOUNT>"; 
							}
							break;
						case "OS":
							if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
								continue;
							if (gStrPdfFor == PDFForDecPage)
							{
					
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURES " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGEBOTHERSTRUCTURES>";
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURESLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</PC_COVERAGEBOTHERSTRUCTURESLIMIT>";
								//								if(htpremium.Contains("COV_B"))
							{
								//gstrGetPremium	=	htpremium["COV_B"].ToString();
								gintGetindex	=	gstrGetPremium.IndexOf(".");
								//									if(gintGetindex==	-1)
								//										DecPageHomeElement.InnerXml += "<PC_COVERAGEBOTHERSTRUCTURESPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_B"].ToString() + ".00")+"</PC_COVERAGEBOTHERSTRUCTURESPREMIUM>";
								//									else
								//										DecPageHomeElement.InnerXml += "<PC_COVERAGEBOTHERSTRUCTURESPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_B"].ToString())+"</PC_COVERAGEBOTHERSTRUCTURESPREMIUM>";
								DecPageHomeElement.InnerXml += "<PC_COVERAGEBOTHERSTRUCTURESPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, CovCode))+"</PC_COVERAGEBOTHERSTRUCTURESPREMIUM>";
								//dblSumTotal+=int.Parse(htpremium["COV_B"].ToString());
							}
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<OTHERSTRUCTURESAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) +"</OTHERSTRUCTURESAMOUNT>"; 
							}
							break;
						case "EBUSPP":
							int lim_covc=0;
							string limcovc = "";
							try
							{
								lim_covc = int.Parse(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace(",",""));
								lim_covc = lim_covc - Convert.ToInt32(red_covc);
								int lim_million = lim_covc /1000;
								if(lim_million / 1000 > 0)
								{
									limcovc = (lim_million /1000).ToString() + "," + AfterComma(lim_million %1000);
								}
								else
									limcovc = lim_million.ToString();
								if(lim_covc / 1000 > 0)
								{
									limcovc += "," + AfterComma(lim_covc %1000) + ".00";
								}
								else
									limcovc = lim_covc.ToString() + ".00";
							}
							catch//(Exception ex)
							{}
							if (gStrPdfFor == PDFForDecPage)
							{
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTY>";
								if(lim_covc != 0.00)
									DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limcovc)+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYLIMIT>";
								else
									DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYLIMIT>";
								//								if(htpremium.Contains("COV_C"))
							{
								//gstrGetPremium	=	htpremium["COV_C"].ToString();
								gintGetindex	=	gstrGetPremium.IndexOf(".");
								//									if(gintGetindex==	-1)
								//										DecPageHomeElement.InnerXml += "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_C"].ToString() + ".00")+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM>";
								//									else
								//										DecPageHomeElement.InnerXml += "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_C"].ToString())+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM>";
								DecPageHomeElement.InnerXml += "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, CovCode))+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM>";
								//dblSumTotal+=int.Parse(htpremium["COV_C"].ToString());
							}
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								if(lim_covc != 0.00)
									Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PERSONALPROPERTYAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limcovc) +"</PERSONALPROPERTYAMOUNT>"; 
								else
									Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PERSONALPROPERTYAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) +"</PERSONALPROPERTYAMOUNT>"; 
							}
							break;
						case "LOSUR":
							if (gStrPdfFor == PDFForDecPage)
							{
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEDLOSSOFUSE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGEDLOSSOFUSE>";
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEDLOSSOFUSELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</PC_COVERAGEDLOSSOFUSELIMIT>";

								//								if(htpremium.Contains("COV_D"))
							{
								//gstrGetPremium	=	htpremium["COV_D"].ToString();
								gintGetindex	=	gstrGetPremium.IndexOf(".");
								//									if(gintGetindex==	-1)
								//										DecPageHomeElement.InnerXml += "<PC_COVERAGEDLOSSOFUSEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_D"].ToString() + ".00")+"</PC_COVERAGEDLOSSOFUSEPREMIUM>";
								//									else
								//										DecPageHomeElement.InnerXml += "<PC_COVERAGEDLOSSOFUSEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_D"].ToString())+"</PC_COVERAGEDLOSSOFUSEPREMIUM>";
								DecPageHomeElement.InnerXml += "<PC_COVERAGEDLOSSOFUSEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, CovCode))+"</PC_COVERAGEDLOSSOFUSEPREMIUM>";
								//dblSumTotal+=int.Parse(htpremium["COV_D"].ToString());
							}

							}
							else if (gStrPdfFor == PDFForAcord)
							{
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<LOSSOFUSEAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) +"</LOSSOFUSEAMOUNT>"; 
							}
							break;
						case "PL":
							if (gStrPdfFor == PDFForDecPage)
							{
								strRVLiabLim = CoverageDetails["LIMIT_1"].ToString();
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PERSONALLIABILITYLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</PERSONALLIABILITYLIMIT>";							
								//								if(htpremium.Contains("COV_E"))
							{
								//gstrGetPremium	=	htpremium["COV_E"].ToString();
								gintGetindex	=	gstrGetPremium.IndexOf(".");
								//									if(gintGetindex==	-1)
								//										DecPageHomeElement.InnerXml += "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_E"].ToString() + ".00")+"</PERSONALLIABILITYPREMIUM>";
								//									else
								//										DecPageHomeElement.InnerXml += "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_E"].ToString())+"</PERSONALLIABILITYPREMIUM>";
								DecPageHomeElement.InnerXml += "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, CovCode))+"</PERSONALLIABILITYPREMIUM>";
								//dblSumTotal+=int.Parse(htpremium["COV_E"].ToString());
							}
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PERSONALLIABILITY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) +"</PERSONALLIABILITY>"; 
							}
							break;
						case "MEDPM":
							if (gStrPdfFor == PDFForDecPage)
							{
								strRVMedLim = CoverageDetails["LIMIT_1"].ToString();
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<MEDICALPAYMENTSLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</MEDICALPAYMENTSLIMIT>";							
								//								if(htpremium.Contains("COV_F"))
							{
								//gstrGetPremium	=	htpremium["COV_F"].ToString();
								gintGetindex	=	gstrGetPremium.IndexOf(".");
								//									if(gintGetindex==	-1)
								//										DecPageHomeElement.InnerXml += "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_F"].ToString() + ".00")+"</MEDICALPAYMENTSPREMIUM>";
								//									else
								//										DecPageHomeElement.InnerXml += "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_F"].ToString())+"</MEDICALPAYMENTSPREMIUM>";
								DecPageHomeElement.InnerXml += "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, CovCode))+"</MEDICALPAYMENTSPREMIUM>";

								//dblSumTotal+=int.Parse(htpremium["COV_F"].ToString());
							}
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<MEDICALPAYMENTSAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) +"</MEDICALPAYMENTSAMOUNT>"; 
							}
							break;						

						case "APD":
							if (gStrPdfFor == PDFForDecPage)
							{
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEALLPERILDEDUC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE"].ToString())+"</PC_COVERAGEALLPERILDEDUC>";
							}
							if (gStrPdfFor == PDFForAcord)
							{
								if(CoverageDetails["DEDUCTIBLE"]!=null && CoverageDetails["DEDUCTIBLE"].ToString()!="" )
									if(double.Parse(CoverageDetails["DEDUCTIBLE"].ToString()) > 0.00 )
									{
										Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ALL_PERIL_CHECK " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+1+"</ALL_PERIL_CHECK>"; 
										Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ALL_PERIL_AMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE"].ToString()) +"</ALL_PERIL_AMOUNT>"; 

									}
									else
									{
										Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ALL_PERIL_CHECK " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+0.00+"</ALL_PERIL_CHECK>"; 
									}
							}
							break;		
				
						case "EBEP11":
							if (gStrPdfFor == PDFForAcord)
							{
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<REPLACEMENTCOSTDWELLING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+1+"</REPLACEMENTCOSTDWELLING>"; 
							}
							break;
						case "EBRCPP":
							if (gStrPdfFor == PDFForAcord)
							{
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<REPLACEMENTCOSTCONTENTS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+1+"</REPLACEMENTCOSTCONTENTS>"; 
							}
							break;

					}
					#endregion
				}
				#endregion

				#region Endorsements
				XmlElement DecPageHomeEndmts;
				DecPageHomeEndmts= AcordPDFXML.CreateElement("ENDORSEMENTS");

				XmlElement SupplementHomeEndmts;
				SupplementHomeEndmts= AcordPDFXML.CreateElement("ENDORSEMENTS");

				
							

				if (gStrPdfFor == PDFForDecPage)
				{
					#region Declaration Page Dwelling Endorsements
					DecPageHomeElement.AppendChild(DecPageHomeEndmts);
					DecPageHomeEndmts.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageHomeEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHOMEEND"));
					DecPageHomeEndmts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEEND"));
					DecPageHomeEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEENDEXTN"));
					DecPageHomeEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEENDEXTN"));
					#endregion
				}
				else if (gStrPdfFor == PDFForAcord)
				{
					#region Supplemental Page Dwelling Endorsements
					SupplementHomeElement.AppendChild(SupplementHomeEndmts);
					SupplementHomeEndmts.SetAttribute(fieldType,fieldTypeMultiple);
					SupplementHomeEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTHOMEEND"));
					SupplementHomeEndmts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTHOMEEND"));
					SupplementHomeEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTHOMEENDEXTN"));
					SupplementHomeEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTHOMEENDEXTN"));
					#endregion
				}	
				//int endCtr=0;
				int DecPageEndCtr=1;
				if (gStrPdfFor == PDFForDecPage)
				{
					foreach(DataRow CoverageDetails in DSTempDwelling.Tables[0].Rows)
					{	
						int prnCtr = 0;
						string CovCode=CoverageDetails["COV_CODE"].ToString();
						if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y" && CoverageDetails["ENDORS_PRINT"].ToString() !="N")
						{
							while(prnOrd_covCode[prnCtr] != CoverageDetails["COV_CODE"].ToString() && prnOrd_covCode[prnCtr] != null)
							{
								prnCtr++;
							}
							//Endorsement already in list
							if(prnOrd_covCode[prnCtr] == CoverageDetails["COV_CODE"].ToString())
								continue;
							prnOrd_covCode[prnCtr] = CoverageDetails["COV_CODE"].ToString();
							if(CoverageDetails["ATTACH_FILE"] != System.DBNull.Value)
								prnOrd_attFile[prnCtr] = CoverageDetails["ATTACH_FILE"].ToString();
							if(CoverageDetails["PRINT_ORDER"] != System.DBNull.Value)
								prnOrd[prnCtr] = int.Parse(CoverageDetails["PRINT_ORDER"].ToString());
							else
								prnOrd[prnCtr] = 0;
						}
						//					else
						//						prnOrd[prnCtr] = 999999;
						//					prnCtr++;
					}
				}
				foreach(DataRow CoverageDetails in DSTempDwelling.Tables[0].Rows)
				{
					string CovCode = CoverageDetails["COV_CODE"].ToString();
					if (CovCode!="DWELL" && CovCode!="OS" && CovCode!="EBUSPP" && CovCode!="LOSUR" && CovCode!="PL" && CovCode!="MEDPM" )
					{	
						#region DECPAGE
						if(gStrPdfFor.Equals(PDFForDecPage))
						{
							if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							{
								XmlElement DecPageDwellEndmtElement;
								DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
								DecPageHomeEndmts.AppendChild(DecPageDwellEndmtElement);
								DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr-1).ToString() );


								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_DECPAGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("HOMEOWNERS  DECLARATION")+"</HM_DECPAGE>";
								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</HM_ENDFORM>";
								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
								if(CoverageDetails["LIMIT_1"].ToString().Trim() != "0.00")
									DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE"].ToString())+"</HM_ENDDEDUCTIBLE>";
								DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, CovCode))+"</HM_ENDPREMIUM>";
							

								#region SWITCH CASE FOR DEC PAGE
								//switch(CoverageDetails["COV_CODE"].ToString())
								switch(CovCode)
								{
									case	"BPCES"	:

										//										if(htpremium.Contains("BSNS_PRSUIT"))
									{
										if (gStrPdfFor == PDFForDecPage)
										{
											double HO71_prem = 0;
											try {HO71_prem	=	double.Parse(htpremium["CLASS_A"].ToString());}	
											catch (Exception ex)
                                            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
											try {HO71_prem	+=	double.Parse(htpremium["CLASS_B"].ToString());}	
											catch (Exception ex)
                                            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
											try {HO71_prem	+=	double.Parse(htpremium["CLASS_C"].ToString());}	
											catch (Exception ex)
                                            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
											try {HO71_prem	+=	double.Parse(htpremium["CLASS_D"].ToString());}	
											catch (Exception ex)
                                            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
											if(HO71_prem != 0)
											{
												gstrGetPremium = HO71_prem.ToString();
												//												//gstrGetPremium	=	htpremium["BSNS_PRSUIT"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												if(gintGetindex==	-1)		
													DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(gstrGetPremium + ".00")+"</HM_ENDPREMIUM>";
												else
													DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(gstrGetPremium)+"</HM_ENDPREMIUM>";
											}
										}
										//											//dblSumTotal+=int.Parse(htpremium["BSNS_PRSUIT"].ToString());
									}
										break;

								}
								#endregion
							}
						}
						#endregion
	
						#region SUPPLEMENT PAGE
						if(gStrPdfFor.Equals(PDFForAcord) )
						{
							if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							{
								XmlElement SupplementDwellEndmtElement;
								SupplementDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
								SupplementHomeEndmts.AppendChild(SupplementDwellEndmtElement);
								SupplementDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
								SupplementDwellEndmtElement.SetAttribute(id,(DecPageEndCtr-1).ToString());

								if(gStrPdfFor == PDFForAcord)
								{
									if(CovCode!="APD")
									{
										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString()) +"</HM_ENDFORM>";							
										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										if(CoverageDetails["LIMIT_1"].ToString().Trim() != "0.00")
											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
										if(CoverageDetails["DEDUCTIBLE"].ToString().Trim() != "0.00")
											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE"].ToString())+"</HM_ENDDEDUCTIBLE>";
										SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, CovCode))+"</HM_ENDPREMIUM>";											
									}
								}
							

								#region SWITCH CASE FOR ACCORD
								switch(CoverageDetails["COV_CODE"].ToString())
								{
									case "APD":
									
										break;
									case "EBNBPL":
										break;
									case "WOODS":								
										if(htpremium.Contains("S_WOOD_STV"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["S_WOOD_STV"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["S_WOOD_STV"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["S_WOOD_STV"].ToString())+"</HM_ENDPREMIUM>";											
											}
											////dblSumTotal+=int.Parse(htpremium["S_WOOD_STV"].ToString());
										}
										break;
									case "EBRCPP":
										if(htpremium.Contains("RPLC_CST"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["RPLC_CST"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RPLC_CST"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RPLC_CST"].ToString())+"</HM_ENDPREMIUM>";											

											}

											//dblSumTotal+=int.Parse(htpremium["RPLC_CST"].ToString());
										}
										break;
									case "EBP20":
										if(htpremium.Contains("PRFRD_PLS"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["PRFRD_PLS"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRFRD_PLS"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRFRD_PLS"].ToString())+"</HM_ENDPREMIUM>";											
											}
										}
										break;
									case "EBP22":
										if (gStrPdfFor == PDFForDecPage)
										{
									

										}
										break;
									case "EBP24":
										if (gStrPdfFor == PDFForDecPage)
										{
									

										}
										break;
									case "EBEP11":
										if(htpremium.Contains("EXPND_RPLC_CST"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["EXPND_RPLC_CST"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["EXPND_RPLC_CST"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["EXPND_RPLC_CST"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["EXPND_RPLC_CST"].ToString());
										}
										break;
									case	"EBIF96"	:
										if(htpremium.Contains("FIRE_DPT"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["FIRE_DPT"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FIRE_DPT"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FIRE_DPT"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["FIRE_DPT"].ToString());
										}
										break;
									case	"IBUSP"	:
										if(htpremium.Contains("BSNS_PRP_INCR"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["BSNS_PRP_INCR"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BSNS_PRP_INCR"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BSNS_PRP_INCR"].ToString())+"</HM_ENDPREMIUM>";

											}
											//dblSumTotal+=int.Parse(htpremium["BSNS_PRP_INCR"].ToString());
										}
										break;
									case	"EBOS40"	:
										if(htpremium.Contains("OTHR_STR_RNT"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["OTHR_STR_RNT"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RNT"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RNT"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["OTHR_STR_RNT"].ToString());
										}
										break;
									case	"EBPPC"	:
										break;
									case	"EBPPOP"	:
										if(htpremium.Contains("PRS_PRP_AWY_PRM"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["PRS_PRP_AWY_PRM"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_PRP_AWY_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_PRP_AWY_PRM"].ToString())+"</HM_ENDPREMIUM>";											
											
											}
											//dblSumTotal+=int.Parse(htpremium["PRS_PRP_AWY_PRM"].ToString());
										}
										break;
									case	"EBALEXP"	:
										break;
									case	"EBOS489"	:
										if(htpremium.Contains("OTHR_STR_RPR_CST"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["OTHR_STR_RPR_CST"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RPR_CST"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RPR_CST"].ToString())+"</HM_ENDPREMIUM>";
											}
											//dblSumTotal+=int.Parse(htpremium["OTHR_STR_RPR_CST"].ToString());
										}
										break;
									case	"EBICC53"	:
										if(htpremium.Contains("CRDT_CRD"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["CRDT_CRD"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CRDT_CRD"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CRDT_CRD"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["CRDT_CRD"].ToString());
										}
										break;
									case	"EBCCSL"	:
										if (gStrPdfFor == PDFForDecPage)
										{
									
										}
										break;
									case	"SEWER"	:
										if(htpremium.Contains("ORD_LW"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["ORD_LW"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ORD_LW"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ORD_LW"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["ORD_LW"].ToString());
										}
										break;
									case	"EROK"	:
										if(htpremium.Contains("ERTHQKE"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["ERTHQKE"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString() + ".00")+"</HM_ENDPREMIUM>";		
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString())+"</HM_ENDPREMIUM>";	
											}
											//dblSumTotal+=int.Parse(htpremium["ERTHQKE"].ToString());
										}
										break;
									case	"FRAUD"	:
										if(htpremium.Contains("ID_FRAUD"))
										{
									
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["ID_FRAUD"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ID_FRAUD"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ID_FRAUD"].ToString() )+"</HM_ENDPREMIUM>";
											}
											//dblSumTotal+=int.Parse(htpremium["ID_FRAUD"].ToString());
										}
										break;
									case	"EBDUC"	:
								
										break;

									case	"EBCASP"	:
										if(htpremium.Contains("UNT_OWNR_CVG_A"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["UNT_OWNR_CVG_A"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UNT_OWNR_CVG_A"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UNT_OWNR_CVG_A"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["UNT_OWNR_CVG_A"].ToString());
										}
										break;
									case	"EBUNIT"	:
										if(htpremium.Contains("CND_UNIT"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["CND_UNIT"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_UNIT"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_UNIT"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["CND_UNIT"].ToString());
										}
										break;
									case	"LAC"	:
										break;
									case	"EBAIRP"	:
								

										break;
									case	"EBBAA"	:
										if(htpremium.Contains("BLDG_ALTR"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["BLDG_ALTR"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALTR"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALTR"].ToString())+"</HM_ENDPREMIUM>";

											}
											//dblSumTotal+=int.Parse(htpremium["BLDG_ALTR"].ToString());
										}
										break;
									case	"EBRDC"	:
										if(htpremium.Contains("RNT_DLX"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["RNT_DLX"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RNT_DLX"].ToString() + ".00")+"</HM_ENDPREMIUM>";	
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RNT_DLX"].ToString())+"</HM_ENDPREMIUM>";
											}
											//dblSumTotal+=int.Parse(htpremium["RNT_DLX"].ToString());
										}
										break;
									case	"EBCDC"	:
										if(htpremium.Contains("CND_DLX"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["CND_DLX"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_DLX"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_DLX"].ToString())+"</HM_ENDPREMIUM>";	

											}
											//dblSumTotal+=int.Parse(htpremium["CND_DLX"].ToString());
										}
										break;
									case	"EBMC"	:

										break;

									case	"RECST"	:
								
										break;

									case	"MIN"	:
								
								
										break;	
							
									case	"EBSS490"	:
										if(htpremium.Contains("SPCFC_STR_PRMS"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["SPCFC_STR_PRMS"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SPCFC_STR_PRMS"].ToString() + ".00")+"</HM_ENDPREMIUM>";	
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SPCFC_STR_PRMS"].ToString())+"</HM_ENDPREMIUM>";
											}
											//dblSumTotal+=int.Parse(htpremium["SPCFC_STR_PRMS"].ToString());
										}
										break;

									case	"EBCCSM"	:

										break;

									case	"ESCCSS"	:
								
										break;

									case	"EBCCSI"	:
										break;

									case	"EBCCSF"	:

								
										break;


									case	"WBSPO"	:
										if(htpremium.Contains("BK_UP_SM_PMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["BK_UP_SM_PMP"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BK_UP_SM_PMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BK_UP_SM_PMP"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["BK_UP_SM_PMP"].ToString());
										}
										break;


									case	"APOBI"	:
										if(htpremium.Contains("ADDL_PRM_OCC_INS"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["ADDL_PRM_OCC_INS"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_OCC_INS"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_OCC_INS"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_OCC_INS"].ToString());
										}
										break;


									case	"APRPR"	:
										if(htpremium.Contains("ADDL_PRM_RNTD_OTH_RES"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString())+"</HM_ENDPREMIUM>";

											}
											//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString());
										}
										break;


									case	"APOLR"	:
										if(htpremium.Contains("ADDL_PRM_RNTD_1FMLY"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["ADDL_PRM_RNTD_1FMLY"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_1FMLY"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_1FMLY"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_RNTD_1FMLY"].ToString());
										}
										break;


									case	"APOLO"	:
										if(htpremium.Contains("ADDL_PRM_RNTD_2FMLY"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["ADDL_PRM_RNTD_2FMLY"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_2FMLY"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_2FMLY"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_RNTD_2FMLY"].ToString());
										}
										break;


									case	"IOPSS"	:
										if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_PRM"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString());
										}
										break;


									case	"IOPSL"	:
										break;


									case	"IOPSI"	:
										if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_INST"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString());
										}
										break;


									case	"IOPSO"	:
										if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_OFF_PRM"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString());
										}
										break;


									case	"PERIJ"	:
										if(htpremium.Contains("PRS_INJRY"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["PRS_INJRY"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_INJRY"].ToString() + ".00")+"</HM_ENDPREMIUM>";
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_INJRY"].ToString())+"</HM_ENDPREMIUM>";
											}
											//dblSumTotal+=int.Parse(htpremium["PRS_INJRY"].ToString());
										}
										break;


									case	"REEMN"	:
										if(htpremium.Contains("RESI_EMPL"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["RESI_EMPL"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RESI_EMPL"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RESI_EMPL"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["RESI_EMPL"].ToString());
										}
										break;

									case	"BPCES"	:

										if(htpremium.Contains("BSNS_PRSUIT"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												double HO71_prem = 0;
												try {HO71_prem	=	double.Parse(htpremium["CLASS_A"].ToString());}	
												catch (Exception ex)
                                                { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
												try {HO71_prem	+=	double.Parse(htpremium["CLASS_B"].ToString());}	
												catch (Exception ex)
                                                { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
												try {HO71_prem	+=	double.Parse(htpremium["CLASS_C"].ToString());}	
												catch (Exception ex)
                                                { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
												try {HO71_prem	+=	double.Parse(htpremium["CLASS_D"].ToString());}	
												catch (Exception ex)
                                                { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
												if(HO71_prem != 0)
												{
													gstrGetPremium = HO71_prem.ToString();
													gintGetindex	=	gstrGetPremium.IndexOf(".");
													if(gintGetindex==	-1)
														SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(gstrGetPremium + ".00")+"</HM_ENDPREMIUM>";											
													else
														SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(gstrGetPremium)+"</HM_ENDPREMIUM>";											
											
												}
											}
											//dblSumTotal+=int.Parse(htpremium["BSNS_PRSUIT"].ToString());
										}
										break;

									case	"BPSCM"	:
								
										break;

									case	"BPTAL"	:
								
										break;


									case	"BPTNO"	:

							
										break;


									case	"FLIFP"	:
										if(htpremium.Contains("INCT_FRMG_RESI_PRM"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["INCT_FRMG_RESI_PRM"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RESI_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RESI_PRM"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["INCT_FRMG_RESI_PRM"].ToString());
										}
										break;
									case	"FLOFO"	:
										if(htpremium.Contains("INCT_FRMG_OPRT_INS"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["INCT_FRMG_OPRT_INS"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_OPRT_INS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_OPRT_INS"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["INCT_FRMG_OPRT_INS"].ToString());
										}
										break;
									case	"FLOFR"	:
										if(htpremium.Contains("INCT_FRMG_RNTD_OTH"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["INCT_FRMG_RNTD_OTH"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RNTD_OTH"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RNTD_OTH"].ToString() )+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["INCT_FRMG_RNTD_OTH"].ToString());
										}
										break;


									case	"OSTDISH"	:
									
								
										break;


									case	"HO200"	:
										if(htpremium.Contains("WTRBD_LBLTY"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["WTRBD_LBLTY"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WTRBD_LBLTY"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WTRBD_LBLTY"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["WTRBD_LBLTY"].ToString());
										}
										break;
									case	"HO9"	:
										if(htpremium.Contains("SUB_SRFCE_WTR"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["SUB_SRFCE_WTR"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SUB_SRFCE_WTR"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SUB_SRFCE_WTR"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["SUB_SRFCE_WTR"].ToString());
										}
										break;
									case	"REDUC"	:

										break;
									case	"BICYC"	:
										if(htpremium.Contains("BICYCLE"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["BICYCLE"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BICYCLE"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BICYCLE"].ToString())+"</HM_ENDPREMIUM>";											

											}
											//dblSumTotal+=int.Parse(htpremium["BICYCLE"].ToString());
										}
										break;
									case	"CAMER"	:
										if(htpremium.Contains("CMRA"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["CMRA"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CMRA"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CMRA"].ToString())+"</HM_ENDPREMIUM>";											

											}
											//dblSumTotal+=int.Parse(htpremium["CMRA"].ToString());
										}
										break;
									case	"CELLU"	:
										if(htpremium.Contains("CELL"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["CELL"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CELL"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CELL"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["CELL"].ToString());
										}
										break;
									case	"FINEBR"	:
										if(htpremium.Contains("FNE_ART_WTH_BRK"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["FNE_ART_WTH_BRK"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WTH_BRK"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WTH_BRK"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["FNE_ART_WTH_BRK"].ToString());
										}
										break;
									case	"FINEWBR"	:
										if(htpremium.Contains("FNE_ART_WO_BRK"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["FNE_ART_WO_BRK"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["FNE_ART_WO_BRK"].ToString());
										}
										break;
									case	"FURS"	:
										if(htpremium.Contains("FUR"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["FUR"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FUR"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FUR"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["FUR"].ToString());
										}
										break;
									case	"GOLF"	:
										if(htpremium.Contains("GOLF"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["GOLF"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GOLF"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GOLF"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["GOLF"].ToString());
										}
										break;
									case	"GUNS"	:
										if(htpremium.Contains("GUN"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["GUN"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GUN"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GUN"].ToString() )+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["GUN"].ToString());
										}
										break;
									case	"HANDI"	:
										break;
									case	"HEARI"	:
										break;
									case	"INSUL"	:
										break;
									case	"JEWEL"	:
										if(htpremium.Contains("JWL"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["JWL"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["JWL"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["JWL"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["JWL"].ToString());
										}
										break;
									case	"MART"	:
										break;
									case	"MUSIC"	:
										if(htpremium.Contains("MSC_NON_PRF"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["MSC_NON_PRF"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MSC_NON_PRF"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MSC_NON_PRF"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["MSC_NON_PRF"].ToString());
										}
										break;
									case	"PERSOD"	:
										if(htpremium.Contains("PRS_CMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["PRS_CMP"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["PRS_CMP"].ToString());
										}
									
										break;
									case	"PERSOL"	:
										if(htpremium.Contains("PRS_CMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["PRS_CMP"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["PRS_CMP"].ToString());
										}
										
										break;
									case	"RARE"	:
										if(htpremium.Contains("RARE_COIN"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["RARE_COIN"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RARE_COIN"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RARE_COIN"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["RARE_COIN"].ToString());
										}
										break;
									case	"SALES"	:
										break;
									case	"SCUBA"	:
										break;
									case	"SILVE"	:
										if(htpremium.Contains("SLVR"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["SLVR"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SLVR"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SLVR"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["SLVR"].ToString());
										}
										break;
									case	"SNOW"	:
										break;
									case	"STAMP"	:
										if(htpremium.Contains("STMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["STMP"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["STMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["STMP"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["STMP"].ToString());
										}
										break;
									case	"TACK"	:
										break;
									case	"TOOLSP"	:
										break;
									case	"TOOLSB"	:
										break;
									case	"TRACT"	:
										break;
									case	"TRAIN"	:
										break;
									case	"WHEEL"	:
										break;
									case	"EOP17"	:

								

										break;
									case	"ECOB"	:
										break;
									case	"ECOC"	:
										break;
									case	"BUMC"	:
										break;
									case	"LF330"	:
								
										break;
									case	"SP350"	:

								
										break;
									case	"LAS360"	:


								
									
										break;
									case	"CWC373"	:
										if(htpremium.Contains("WRKR_CMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["WRKR_CMP"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WRKR_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WRKR_CMP"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["WRKR_CMP"].ToString());
										}
										break;

									case	"HO214"	:
										if(htpremium.Contains("PRS_CMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												//gstrGetPremium	=	htpremium["PRS_CMP"].ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												//												if(gintGetindex==	-1)
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";											
												//												else
												//													SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString())+"</HM_ENDPREMIUM>";											
											}
											//dblSumTotal+=int.Parse(htpremium["PRS_CMP"].ToString());
										}
										break;
								
									case	"HO216"	:
								
										if (gStrPdfFor == PDFForDecPage)
										{

										}
										break;

									case	"HO300"	:
										if (gStrPdfFor == PDFForDecPage)
										{

										}
										break;

									case	"HO864"	:

										if (gStrPdfFor == PDFForDecPage)
										{

										}
										break;


									case	"IFGHO"	:

										break;

									case	"WP865"	:
										if (gStrPdfFor == PDFForDecPage)
										{

									
										}
										break;

									default:
										if (gStrPdfFor == PDFForDecPage)
										{
											if(CoverageDetails["COV_CODE"].ToString()=="EMO42" || CoverageDetails["COV_CODE"].ToString()=="EOP42")
											{
									
											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO65" || CoverageDetails["COV_CODE"].ToString()=="EOP65")
											{
												#region Dec Page Element
										
												#endregion
											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO70" || CoverageDetails["COV_CODE"].ToString()=="EOP70")
											{
												#region Dec Page Element
										
												#endregion

											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO71" || CoverageDetails["COV_CODE"].ToString()=="EOP71")
											{
												#region Dec Page Element
										
												#endregion

											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR211" || CoverageDetails["COV_CODE"].ToString()=="EOP211")
											{
												#region Dec Page Element
										
												#endregion
											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR310")
											{
												#region Dec Page Element
										
												#endregion
											
											}

											if(CoverageDetails["COV_CODE"].ToString()=="EMO315" || CoverageDetails["COV_CODE"].ToString()=="EOP315")
											{
											
											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR320")
											{
												#region Dec Page Element
									
												#endregion
											
											}
											if(CoverageDetails["COV_CODE"].ToString()=="HO6D")
											{
											
											}
											if(CoverageDetails["COV_CODE"].ToString()=="HO4D")
											{
											
											}
										}
										else if(gStrPdfFor == PDFForAcord)
										{
											if(CoverageDetails["COV_CODE"].ToString()=="EMO42" || CoverageDetails["COV_CODE"].ToString()=="EOP42"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO65" || CoverageDetails["COV_CODE"].ToString()=="EOP65"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO70" || CoverageDetails["COV_CODE"].ToString()=="EOP70"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO71" || CoverageDetails["COV_CODE"].ToString()=="EOP71"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR211" || CoverageDetails["COV_CODE"].ToString()=="EOP211"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR310"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO315" || CoverageDetails["COV_CODE"].ToString()=="EOP315"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR320"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMRSFP"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EOP48"){}
											if(CoverageDetails["COV_CODE"].ToString()=="HO6D"){}
											if(CoverageDetails["COV_CODE"].ToString()=="HO4D"){}
										}									
										break;
								}
								#endregion
							}
						
						}
						#endregion

						if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
						{
							DecPageEndCtr++;
							//endCtr++;
						}						
					}									
					
					//endCtr++;
				}
				#endregion			

				#region Supplemental Page Outbuildings
				if (gStrPdfFor == PDFForAcord)
				{
					DataView DSTempOutbuildings = getHomeOtherStructureDataSet(DwellingDetail["DWELLING_ID"].ToString());
					if(DSTempOutbuildings.Table.Rows.Count>0   )
					{
						XmlElement SupplementRootOutbuildings;
						SupplementRootOutbuildings= AcordPDFXML.CreateElement("OUTBUILDINGS");
						SupplementHomeElement.AppendChild(SupplementRootOutbuildings);
						SupplementRootOutbuildings.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementRootOutbuildings.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTOUTBUILDING"));
						SupplementRootOutbuildings.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTOUTBUILDING"));
						SupplementRootOutbuildings.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTOUTBUILDINGEXTN"));
						SupplementRootOutbuildings.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTOUTBUILDINGEXTN"));

						int outCtr=0;
						foreach(DataRow CoverageDetails in DSTempOutbuildings.Table.Rows)
						{
							XmlElement SupplementOutbuildings;
							SupplementOutbuildings= AcordPDFXML.CreateElement("OUTBUILDINGSINFO");
							SupplementRootOutbuildings.AppendChild(SupplementOutbuildings);
							SupplementOutbuildings.SetAttribute(fieldType,fieldTypeNormal);
							SupplementOutbuildings.SetAttribute(id,outCtr.ToString() );

							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<DESCRIPTIONOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["PREMISES_DESCRIPTION"].ToString()) +"</DESCRIPTIONOUTBUILDINGS>";
							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<USEOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["PREMISES_USE"].ToString()) +"</USEOUTBUILDINGS>";
							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<CONDITIONOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["LOOKUP_VALUE_DESC"].ToString()) +"</CONDITIONOUTBUILDINGS>";
							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<PHOTOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["PICTURE_ATTACHED"].ToString()) +"</PHOTOUTBUILDINGS>";
							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<VALUEOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["INSURING_VALUE"].ToString()) +"</VALUEOUTBUILDINGS>";

							outCtr++;
						}
					}
				}
				#endregion

				#region Addl Interests
				XmlElement DecPageDwellAddlInt;
				DecPageDwellAddlInt = AcordPDFXML.CreateElement("ADDITIONALINT");

				//				XmlElement Acord80DwellAddlInt;
				//				Acord80DwellAddlInt = AcordPDFXML.CreateElement("ADDITIONALINT");
				if (gStrPdfFor == PDFForDecPage)
				{
					#region Additional Root Element int Dec Page
					DecPageHomeElement.AppendChild(DecPageDwellAddlInt);
					DecPageDwellAddlInt.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageDwellAddlInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHOMEADDLINT"));
					DecPageDwellAddlInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEADDLINT"));
					DecPageDwellAddlInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEADDLINTEXTN"));
					DecPageDwellAddlInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEADDLINTEXTN"));
					#endregion
				}
				else if (gStrPdfFor == PDFForAcord)
				{
					#region Additional Root Element Acord 80 Page
					//				Acord80HomeElement.AppendChild(Acord80DwellAddlInt);
					//				Acord80DwellAddlInt.SetAttribute(fieldType,fieldTypeMultiple);
					//				Acord80DwellAddlInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80ADDLINT"));
					//				Acord80DwellAddlInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80ADDLINT"));
					//				Acord80DwellAddlInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80ADDLINTEXTN"));
					//				Acord80DwellAddlInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80ADDLINTEXTN"));
					#endregion
				}
				DataSet DSTempDwellinAddInt =getHomeAddInterestDataSet(DwellingDetail["DWELLING_ID"].ToString());
				foreach(DataRow AddlIntDetails in DSTempDwellinAddInt.Tables[0].Rows)
				{
					if (gStrPdfFor == PDFForDecPage)
					{
						#region AddlInt Dec Page
						XmlElement DecPageDwelAddlIntElement;
						DecPageDwelAddlIntElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");
						DecPageDwellAddlInt.AppendChild(DecPageDwelAddlIntElement);
						DecPageDwelAddlIntElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageDwelAddlIntElement.SetAttribute(id,AddInt.ToString());
					
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_SERIALNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["RANK"].ToString())+"</HOMEOWNER_SERIALNO>"; 
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_NAMEADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDER_CITYSTATEZIP"].ToString())+"</HOMEOWNER_NAMEADDRESS>"; 
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOOKUP_VALUE_DESC"].ToString())+"</HOMEOWNER_NATUREOFINTEREST>"; 
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_LOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</HOMEOWNER_LOANNO>"; 
						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region AddlInt Acord 80 Page
						//					XmlElement Acord80DwellAddlIntElement;
						//					Acord80DwellAddlIntElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");
						//					Acord80DwellAddlInt.AppendChild(Acord80DwellAddlIntElement);
						//					Acord80DwellAddlIntElement.SetAttribute(fieldType,fieldTypeNormal);
						//					Acord80DwellAddlIntElement.SetAttribute(id,AddInt.ToString());
						//							
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["NAME_ADDRESS"].ToString())+"</ADDITIONALINTERESTADDRESS>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTCITYSTATEZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDERCITYSTATEZIP"].ToString())+"</ADDITIONALINTERESTCITYSTATEZIP>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["NATURE_OF_INTEREST"].ToString())+"</NATUREOFINTEREST>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTLOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</ADDITIONALINTERESTLOANNO>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<NATUREOFINTDESC " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["NATUREOFINT_DESC"].ToString())+"</NATUREOFINTDESC>"; 
						#endregion
					}
					AddInt++;
				}
				AddInt=0;
				#endregion

				#region Creating Credit And Surcharge Xml
				if (isRateGenerated)
				{
					int CreditSurchRowCounter = 0;
					int creditAddPage = 0;
					string AdditionalExtnTxt="";

					#region Credits
					XmlElement DecPageHOMECredit;
					DecPageHOMECredit = AcordPDFXML.CreateElement("HOMECREDIT");

					XmlElement SupplementHOMECredit;
					SupplementHOMECredit = AcordPDFXML.CreateElement("HOMECREDIT");

					XmlElement SupplementHOMECreditElement;
					SupplementHOMECreditElement = AcordPDFXML.CreateElement("HOMECREDITINFO");	
					SupplementHOMECredit.AppendChild(SupplementHOMECreditElement);
					SupplementHOMECreditElement.SetAttribute(fieldType,fieldTypeNormal);
					//SupplementHOMECreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
					SupplementHOMECreditElement.SetAttribute(id,"0");

					XmlElement DecPageHOMECreditElement;
					DecPageHOMECreditElement = AcordPDFXML.CreateElement("HOMECREDITINFO");
					DecPageHOMECredit.AppendChild(DecPageHOMECreditElement);
					DecPageHOMECreditElement.SetAttribute(fieldType,fieldTypeNormal);
					DecPageHOMECreditElement.SetAttribute(id,"0");

					if (gStrPdfFor == PDFForDecPage)
					{
						#region Dec Page Element
						DecPageHomeElement.AppendChild(DecPageHOMECredit);
						DecPageHOMECredit.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageHOMECredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
						DecPageHOMECredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
						DecPageHOMECredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
						DecPageHOMECredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));

					
						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Supplement Page Element
						SupplementHomeElement.AppendChild(SupplementHOMECredit);
						SupplementHOMECredit.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementHOMECredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDIT"));
						SupplementHOMECredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDIT"));
						SupplementHOMECredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDITEXTN"));
						SupplementHOMECredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));
						
						
						#endregion
					}

					htpremium_dis.Clear(); 
					foreach (XmlNode CreditNode in GetCredits(DwellingDetail["DWELLING_ID"].ToString()))
					{
						if(!htpremium_dis.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
							htpremium_dis.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
						else
							continue;

						string step_desc = getAttributeValue(CreditNode,"STEPDESC").Replace(" ","");
						string step_prem = getAttributeValue(CreditNode,"STEPPREMIUM").Trim();

						if(step_desc.IndexOf("+0%") >0 || step_desc.IndexOf("-0%") >0 || step_prem == "0")
							continue;
						if (gStrPdfFor == PDFForDecPage)
						{							
							#region Dec Page
											
							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_PROT_DVC")	
							{
								DecPageHOMECreditElement.InnerXml += "<HM_PROTECTIVEDEVICE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_PROTECTIVEDEVICE>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_DEVICEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_DEVICEPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_EXP")	
							{
								DecPageHOMECreditElement.InnerXml += "<HM_MATURITYCREDIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_MATURITYCREDIT>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_MATURITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_MATURITYPREMIUM>"; 
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
							{							
								DecPageHOMECreditElement.InnerXml += "<HM_RENEWALDISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_RENEWALDISCOUNT>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_RENEWALPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RENEWALPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_SMKR")	
							{							
								DecPageHOMECreditElement.InnerXml += "<HM_NONSOMKER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_NONSOMKER>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_NONSMOKERPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_NONSMOKERPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_AGE_HOME")	
							{											
								DecPageHOMECreditElement.InnerXml += "<HM_AGEOFHOME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_AGEOFHOME>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_AGEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_AGEPREMIUM>"; 	
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_MULTIPOL")	
							{
								DecPageHOMECreditElement.InnerXml += "<HM_MULTIPOLICY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_MULTIPOLICY>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_MULTIPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_MULTIPPREMIUM>"; 
							}

							//Builders Risk
							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_DUC")	
							{							
								DecPageHOMECreditElement.InnerXml += "<HM_BUILDERRISK " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_BUILDERRISK>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_RISKPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RISKPREMIUM>"; 
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_SEASNL")	
							{							
								DecPageHOMECreditElement.InnerXml += "<HM_SECONDARYRESIDENCE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_SECONDARYRESIDENCE>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_RESIDENCE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RESIDENCE>"; 
								creditAddPage = 1;
							}
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page
							
							if (DwellingCtr%2 == 0)
								AdditionalExtnTxt="";
							else
								AdditionalExtnTxt="1";

						
							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_PROT_DVC")	
							{
								SupplementHOMECreditElement.InnerXml += "<HM_PROTECTIVEDEVICE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_PROTECTIVEDEVICE>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_DEVICEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_DEVICEPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_EXP")	
							{
								SupplementHOMECreditElement.InnerXml += "<HM_MATURITYCREDIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_MATURITYCREDIT>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_MATURITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_MATURITYPREMIUM>"; 
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
							{							
								SupplementHOMECreditElement.InnerXml += "<HM_RENEWALDISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_RENEWALDISCOUNT>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_RENEWALPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RENEWALPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_SMKR")	
							{							
								SupplementHOMECreditElement.InnerXml += "<HM_NONSOMKER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_NONSOMKER>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_NONSMOKERPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_NONSMOKERPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_AGE_HOME")	
							{											
								SupplementHOMECreditElement.InnerXml += "<HM_AGEOFHOME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_AGEOFHOME>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_AGEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_AGEPREMIUM>"; 	
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_MULTIPOL")	
							{
								SupplementHOMECreditElement.InnerXml += "<HM_MULTIPOLICY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_MULTIPOLICY>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_MULTIPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_MULTIPPREMIUM>"; 
							}

							//Builders Risk
							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_DUC")	
							{							
								SupplementHOMECreditElement.InnerXml += "<HM_BUILDERRISK " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_BUILDERRISK>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_RISKPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RISKPREMIUM>"; 
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_SEASNL")	
							{							
								SupplementHOMECreditElement.InnerXml += "<HM_SECONDARYRESIDENCE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_SECONDARYRESIDENCE>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_RESIDENCE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RESIDENCE>"; 
								creditAddPage = 1;
							}
							#endregion
						}

						CreditSurchRowCounter++;
					}
					if(creditAddPage == 1)
					{
						if (gStrPdfFor == PDFForAcord)
						{
							XmlElement SupplementHOMECreditElement1;
							SupplementHOMECreditElement1 = AcordPDFXML.CreateElement("HOMECREDITINFO");	
							SupplementHOMECredit.AppendChild(SupplementHOMECreditElement1);
							SupplementHOMECreditElement1.SetAttribute(fieldType,fieldTypeNormal);
							//SupplementHOMECreditElement1.SetAttribute(id,CreditSurchRowCounter.ToString());
							SupplementHOMECreditElement1.SetAttribute(id,"1");

							SupplementHOMECreditElement1.InnerXml = SupplementHOMECreditElement.InnerXml;
						}

						if (gStrPdfFor == PDFForDecPage)
						{							
							XmlElement DecPageHOMECreditElement1;
							DecPageHOMECreditElement1 = AcordPDFXML.CreateElement("HOMECREDITINFO");
							DecPageHOMECredit.AppendChild(DecPageHOMECreditElement1);
							DecPageHOMECreditElement1.SetAttribute(fieldType,fieldTypeNormal);
							DecPageHOMECreditElement1.SetAttribute(id,"1");

							DecPageHOMECreditElement1.InnerXml = DecPageHOMECreditElement.InnerXml;
						}
					}
					#endregion

					CreditSurchRowCounter = 0;
					AdditionalExtnTxt="";

					#region Surcharges
					XmlElement DecPageHOMESurch;
					DecPageHOMESurch = AcordPDFXML.CreateElement("HOMESURCHARGE");

					XmlElement SupplementHOMESurch;
					SupplementHOMESurch = AcordPDFXML.CreateElement("HOMESURCHARGE");

					XmlElement DecPageHOMESurchElement;
					DecPageHOMESurchElement = AcordPDFXML.CreateElement("HOMESURCHARGEINFO");

					XmlElement SupplementHOMESurchElement;
					SupplementHOMESurchElement = AcordPDFXML.CreateElement("HOMESURCHARGEINFO");
									

					if (gStrPdfFor == PDFForDecPage)
					{
						#region Dec Page Element
						DecPageHomeElement.AppendChild(DecPageHOMESurch);
						DecPageHOMESurch.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageHOMESurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
						DecPageHOMESurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
						DecPageHOMESurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
						DecPageHOMESurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));

						DecPageHOMESurch.AppendChild(DecPageHOMESurchElement);
						DecPageHOMESurchElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageHOMESurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Supplement Page Element
						SupplementHomeElement.AppendChild(SupplementHOMESurch);
						SupplementHOMESurch.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementHOMESurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHG"));
						SupplementHOMESurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHG"));
						SupplementHOMESurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHGEXTN"));
						SupplementHOMESurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHGEXTN"));

						SupplementHOMESurch.AppendChild(SupplementHOMESurchElement);
						SupplementHOMESurchElement.SetAttribute(fieldType,fieldTypeNormal);
						SupplementHOMESurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());						
						#endregion
					}

					htpremium_sur.Clear(); 
					foreach (XmlNode CreditNode in GetSurcharges(DwellingDetail["DWELLING_ID"].ToString()))
					{
						if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
							htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
						else
							continue;
						string step_desc = getAttributeValue(CreditNode,"STEPDESC").Replace(" ","");
						string step_prem = getAttributeValue(CreditNode,"STEPPREMIUM").Trim();

						if(step_desc.IndexOf("+0%") >0 || step_desc.IndexOf("-0%") >0 || step_prem == "0")
							continue;

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page							
							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="S_WOOD_STV")	
							{							
								DecPageHOMESurchElement.InnerXml += "<hm_WOODSTOVE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_WOODSTOVE>"; 
								DecPageHOMESurchElement.InnerXml += "<HM_WOODSTOVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_WOODSTOVEPREMIUM>";
							}
							else if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="S_BREED")	
							{							
								DecPageHOMESurchElement.InnerXml += "<hm_SPECIALBREEDDOG " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_SPECIALBREEDDOG>"; 
								DecPageHOMESurchElement.InnerXml += "<HM_BREEDDOGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_BREEDDOGPREMIUM>";
							}
							else //if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
							{							
								DecPageHOMESurchElement.InnerXml += "<hm_NEWBUSINESSPRIORLOSS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_NEWBUSINESSPRIORLOSS>"; 
								DecPageHOMESurchElement.InnerXml += "<HM_NEWBUSINESSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_NEWBUSINESSPREMIUM>";
							}
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page

							if (DwellingCtr%2 == 0)
								AdditionalExtnTxt="";
							else
								AdditionalExtnTxt="1";

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
							{							
								SupplementHOMESurchElement.InnerXml += "<HM_FAMILYUNITS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_FAMILYUNITS>"; 
								//DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM>";
							}
							else if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="S_WOOD_STV")	
							{							
								SupplementHOMESurchElement.InnerXml += "<hm_WOODSTOVE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_WOODSTOVE>"; 
								SupplementHOMESurchElement.InnerXml += "<HM_WOODSTOVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_WOODSTOVEPREMIUM>";
							}
							else if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="S_BREED")	
							{							
								SupplementHOMESurchElement.InnerXml += "<hm_SPECIALBREEDDOG " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_SPECIALBREEDDOG>"; 
								SupplementHOMESurchElement.InnerXml += "<HM_BREEDDOGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_BREEDDOGPREMIUM>";
							}
							else //if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
							{							
								SupplementHOMESurchElement.InnerXml += "<hm_NEWBUSINESSPRIORLOSS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_NEWBUSINESSPRIORLOSS>"; 
								SupplementHOMESurchElement.InnerXml += "<HM_NEWBUSINESSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_NEWBUSINESSPREMIUM>";
							}
							#endregion
						}
						CreditSurchRowCounter++;
					}
					#endregion
				}
				#endregion


				DwellingCtr++;
			}
		}
		#endregion

		#region Code for Dwelling Addl Interests
		private void createAcord80HomeAddlIntXml()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				DataSet DSTempDwellinAdd = getHomeAddInterestDataSet("0");
			
				#region Acord80 Page
				if (DSTempDwellinAdd.Tables[0].Rows.Count >0)
				{
					XmlElement Acord80AddlInts;
					Acord80AddlInts = AcordPDFXML.CreateElement("ADDITIONALINTERESTS");
					Acord80RootElement.AppendChild(Acord80AddlInts);
					Acord80AddlInts.SetAttribute(fieldType,fieldTypeMultiple);
					Acord80AddlInts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80ADDLINT"));
					Acord80AddlInts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80ADDLINT"));
					Acord80AddlInts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80ADDLINTEXTN"));
					Acord80AddlInts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80ADDLINTEXTN"));

					int RowCounter = 0;
					foreach(DataRow Row in DSTempDwellinAdd.Tables[0].Rows)
					{
						XmlElement Acord80ADDLINTElement;
						Acord80ADDLINTElement = AcordPDFXML.CreateElement("ADDLINTINFO");
						Acord80AddlInts.AppendChild(Acord80ADDLINTElement);
						Acord80ADDLINTElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord80ADDLINTElement.SetAttribute(id,RowCounter.ToString());

						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<ADDITIONALINTERESTADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["NAME_ADDRESS"].ToString())+"</ADDITIONALINTERESTADDRESS>"; 
						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<ADDITIONALINTERESTCITYSTATEZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["HOLDERCITYSTATEZIP"].ToString())+"</ADDITIONALINTERESTCITYSTATEZIP>"; 
						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString())+"</NATUREOFINTEREST>"; 
						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<ADDITIONALINTERESTLOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["LOAN_REF_NUMBER"].ToString()) +"</ADDITIONALINTERESTLOANNO>"; 
						if(Row["NATURE_OF_INTEREST"].ToString() == "3")
							Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<NATUREOFINT_DESC " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["LOOKUP_VALUE_DESC"].ToString())+"</NATUREOFINT_DESC>"; 

						RowCounter++;
					}
				}
				#endregion
			}
		}
		#endregion

		#region Code for Underwriting And General Info Xml Generation
		private void createDwellingUnderwritingGeneralXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				DSTempDataSet = DsUnderWrittingDetails;
				if(DSTempDataSet.Tables[0].Rows.Count >0)
				{
					#region Acord80 Page General Info
					XmlElement Acord80GenInfoElement;
					Acord80GenInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
					Acord80RootElement.AppendChild(Acord80GenInfoElement);
					Acord80GenInfoElement.SetAttribute(fieldType,fieldTypeSingle);
				

					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<FARMING " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_FARMING_BUSINESS_COND"].ToString()) + "</FARMING>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<RESIDENCEEMPLOYEES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_RESIDENCE_EMPLOYEE"].ToString()) + "</RESIDENCEEMPLOYEES>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<OTHERRESIDENCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OTHER_RESI_OWNED"].ToString()) + "</OTHERRESIDENCE>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<OTHERINSURANCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_INSU_COMP"].ToString()) + "</OTHERINSURANCE>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<INSURANCETRANSFERRED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["HAS_INSU_TRANSFERED_AGENCY"].ToString()) + "</INSURANCETRANSFERRED>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<COVERAGEDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_COV_DECLINED_CANCELED"].ToString()) + "</COVERAGEDECLINED>";
					string any_pets = "0";
					if(DSTempDataSet.Tables[0].Rows[0]["ANIMALS_EXO_PETS_HISTORY"].ToString().Trim() != "0")
						any_pets = "1";
					else if(DSTempDataSet.Tables[0].Rows[0]["NO_OF_PETS"].ToString().Trim() != "0")
						any_pets = "1";
					else if(DSTempDataSet.Tables[0].Rows[0]["NO_HORSES"] != System.DBNull.Value && DSTempDataSet.Tables[0].Rows[0]["NO_HORSES"].ToString().Trim() != "0")
						any_pets = "1";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<ANIMALSPETS " + fieldType + "=\"" + fieldTypeText + "\">" + any_pets + "</ANIMALSPETS>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<FIVEACRES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["OF_ACRES"].ToString()) + "</FIVEACRES>";

					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<APPLICANTCONVICTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CONVICTION_DEGREE_IN_PAST"].ToString()) + "</APPLICANTCONVICTED>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<BUILDINGUNDERRENOVATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_RENOVATION"].ToString()) + "</BUILDINGUNDERRENOVATION>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<PROPERTYCOMMERCIAL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_PROP_NEXT_COMMERICAL"].ToString()) + "</PROPERTYCOMMERCIAL>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<TRAMPOLINE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TRAMPOLINE"].ToString()) + "</TRAMPOLINE>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<BUILDINGUNDERCONSTRUCTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BUILD_UNDER_CON_GEN_CONT"].ToString()) + "</BUILDINGUNDERCONSTRUCTION>";

					Acord80GenInfoElement.InnerXml =  Acord80GenInfoElement.InnerXml +  "<DATELASTINSPECTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LAST_INSPECTED_DATE"].ToString()) + "</DATELASTINSPECTION>";
					//All Remarks Go here.
					if(DSTempDataSet.Tables[0].Rows[0]["FARMING_DESC"].ToString() != "")
						Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<ANY_FARMING_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #1: " + DSTempDataSet.Tables[0].Rows[0]["FARMING_DESC"].ToString()) + "</ANY_FARMING_DESC>";
					#endregion
				
					if(DSTempDataSet.Tables[0].Rows[0]["DESC_RESIDENCE_EMPLOYEE"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_RESIDENCE"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_INSURANCE"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DESC_INSU_TRANSFERED_AGENCY"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["DESC_COV_DECLINED_CANCELED"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["BREED_OTHER_DESCRIPTION"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["DESC_CONVICTION_DEGREE_IN_PAST"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DESC_RENOVATION"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["DESC_PROPERTY"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DESC_TRAMPOLINE"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["REMARKS"].ToString() != "")
					{
						#region Acord 80 supplement Sheet Remarks
						XmlElement ACORD80ADDLREMARKS;
						ACORD80ADDLREMARKS = AcordPDFXML.CreateElement("ACORD80ADDLREMARKS");
						Acord80RootElement.AppendChild(ACORD80ADDLREMARKS);
						ACORD80ADDLREMARKS.SetAttribute(fieldType,fieldTypeMultiple);
						ACORD80ADDLREMARKS.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80REMARKS"));
						ACORD80ADDLREMARKS.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80REMARKS"));
						ACORD80ADDLREMARKS.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80REMARKSEXTN"));
						ACORD80ADDLREMARKS.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80REMARKSEXTN"));
				
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<NO_OF_RESI_EMPLOYEES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #2: " + DSTempDataSet.Tables[0].Rows[0]["DESC_RESIDENCE_EMPLOYEE"].ToString()) + "</NO_OF_RESI_EMPLOYEES>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<OTHER_RESIDENCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #4: " + DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_RESIDENCE"].ToString()) + "</OTHER_RESIDENCE>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<OTHER_INSURANCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #5: " + DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_INSURANCE"].ToString()) + "</OTHER_INSURANCE>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<INSURANCE_TRANSFERRED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #6: " + DSTempDataSet.Tables[0].Rows[0]["DESC_INSU_TRANSFERED_AGENCY"].ToString()) + "</INSURANCE_TRANSFERRED>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<COVERAGE_DECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #7: " + DSTempDataSet.Tables[0].Rows[0]["DESC_COV_DECLINED_CANCELED"].ToString()) + "</COVERAGE_DECLINED>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<BREED_OF_DOG " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #9: " + DSTempDataSet.Tables[0].Rows[0]["BREED_OTHER_DESCRIPTION"].ToString()) + "</BREED_OF_DOG>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<ANY_CONVICTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #14: " + DSTempDataSet.Tables[0].Rows[0]["DESC_CONVICTION_DEGREE_IN_PAST"].ToString()) + "</ANY_CONVICTED>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<CONSTRUCTION_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #19: " + DSTempDataSet.Tables[0].Rows[0]["DESC_RENOVATION"].ToString()) + "</CONSTRUCTION_DATE>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<PROPERTY_DESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #21: " + DSTempDataSet.Tables[0].Rows[0]["DESC_PROPERTY"].ToString()) + "</PROPERTY_DESCRIPTION>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<TRAMPOLINE_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #22: " + DSTempDataSet.Tables[0].Rows[0]["DESC_TRAMPOLINE"].ToString()) + "</TRAMPOLINE_DESC>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<ANY_REMARK " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Any Other Remarks: " + DSTempDataSet.Tables[0].Rows[0]["REMARKS"].ToString()) + "</ANY_REMARK>";
						ACORD80ADDLREMARKS.InnerXml = "<ACORD80REMARKSINFO " + fieldType + "=\"" + fieldTypeNormal + "\" " + id + "=\"0\">" + ACORD80ADDLREMARKS.InnerXml + "</ACORD80REMARKSINFO>";
						#endregion
					}
				}
			}
		}
		#endregion

		#region Endorsement Wordings
		//by prAVESH ON 25 MARCH 2008 FOR REMOVING THOSE ENDORSEMENT WHICH HAS NOT BEEN CHANGED AT ENDORSEMENT /RENEWAL PROCESS
		private void RemoveEnorsementWordings()
		{
			try
			{
				if (prnOrd_covCode==null) return;
				DataSet dsDwelling = new DataSet();
				dsDwelling =  DsDwellingDetails; 

				foreach(DataRow DwellingDetail in dsDwelling.Tables[0].Rows)
				{
					DataSet DSNewCoverageEndorsemet = getHomeCoveragesDataSet(gStrClientID,gStrPolicyId,gStrPolicyVersion,DwellingDetail["DWELLING_ID"].ToString());
					DataSet DSOldCoverageEndorsemet = getHomeCoveragesDataSet(gStrClientID,gStrPolicyId,goldVewrsionId,DwellingDetail["DWELLING_ID"].ToString()); 
					foreach ( DataRow NewEndDetails in DSNewCoverageEndorsemet.Tables[0].Rows)
					{
						string CovCode=NewEndDetails["COV_CODE"].ToString();
						int counter = 0;
						DataRow[] drOld= DSOldCoverageEndorsemet.Tables[0].Select("COV_CODE='" + CovCode + "' AND ENDORS_ASSOC_COVERAGE='Y'");
						if( drOld.Length>0)
						{
							//							if (drOld[0]["LIMIT_1"].ToString()==NewEndDetails["LIMIT_1"].ToString() && drOld[0]["EDITION_DATE"].ToString()==NewEndDetails["EDITION_DATE"].ToString() && drOld[0]["DEDUCTIBLE_1"].ToString()==NewEndDetails["DEDUCTIBLE_1"].ToString() 
							//								&& drOld[0]["DEDUCTIBLE"].ToString()==NewEndDetails["DEDUCTIBLE"].ToString() 
							//								&& drOld[0]["LIMIT_2"].ToString()==NewEndDetails["LIMIT_2"].ToString()  
							//								)
							if(drOld[0]["EDITION_DATE"].ToString()==NewEndDetails["EDITION_DATE"].ToString())
							{
								for (counter=0;counter< prnOrd_covCode.Length;counter++)
								{
									if (prnOrd_covCode[counter]==null) continue;
									if(prnOrd_covCode[counter].ToString()==CovCode)
									{
										prnOrd_covCode[counter]=null;
									}
								}
								
							}
						}
						
					}

				}	
			}
			catch(Exception ex)
			{
				throw(ex);
			}

		}


		private void createEndorsementWordings()
		{
			if(gStrPdfFor == PDFForDecPage)
			{
				int endorsCount = 0;
				int counter = 0;
				//while(prnOrd_covCode[endorsCount] != null)
				//	endorsCount++;
				endorsCount=prnOrd_covCode.Length;
				while(counter < endorsCount)
				{
					int lowestIndex = GetLowestPrnIndex(ref prnOrd, endorsCount);
					string prncovCode="";
					if (prnOrd_covCode[lowestIndex]!=null)
						prncovCode = prnOrd_covCode[lowestIndex];
					string prnAttFile = prnOrd_attFile[lowestIndex];
					#region SWITCH CASE FOR DEC PAGE
					//switch(CoverageDetails["COV_CODE"].ToString())
					switch(prncovCode)
					{
						case "EBNBPL":
							break;
						case "WOODS":								
							//										//if(htpremium.Contains("S_WOOD_STV"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["S_WOOD_STV"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["S_WOOD_STV"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["S_WOOD_STV"].ToString())+"</HM_ENDPREMIUM>";


							}
										
							//dblSumTotal+=int.Parse(htpremium["S_WOOD_STV"].ToString());
						}
							break;
						case "EBRCPP":
							//										//if(htpremium.Contains("RPLC_CST"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["RPLC_CST"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RPLC_CST"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RPLC_CST"].ToString())+"</HM_ENDPREMIUM>";
										
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoEBRCPP;
								DecPageHomeEndoEBRCPP = AcordPDFXML.CreateElement("HOMEENDORSEMENTEEBRCPP");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBRCPP);
								DecPageHomeEndoEBRCPP.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBRCPP.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBRCPP.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBRCPP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBRCPP"));
									DecPageHomeEndoEBRCPP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBRCPP"));
								}
								DecPageHomeEndoEBRCPP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBRCPPEXTN"));
								DecPageHomeEndoEBRCPP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBRCPPEXTN"));

								XmlElement EndoElementEBRCPP;
								EndoElementEBRCPP = AcordPDFXML.CreateElement("ENDOELEMENTEBRCPPINFO");
								DecPageHomeEndoEBRCPP.AppendChild(EndoElementEBRCPP);
								EndoElementEBRCPP.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBRCPP.SetAttribute(id,"0");
										
											
							}	
								#endregion
							}
										
							//											//dblSumTotal+=int.Parse(htpremium["RPLC_CST"].ToString());
						}

							break;
						case "EBP20":
							//										//if(htpremium.Contains("PRFRD_PLS"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRFRD_PLS"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRFRD_PLS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRFRD_PLS"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoEBP20;
								DecPageHomeEndoEBP20 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEEBP20");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBP20);
								DecPageHomeEndoEBP20.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBP20.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBP20.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBP20.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBP20"));
									DecPageHomeEndoEBP20.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP20"));
								}
								DecPageHomeEndoEBP20.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBP20EXTN"));
								DecPageHomeEndoEBP20.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP20EXTN"));

								XmlElement EndoElementEBP20;
								EndoElementEBP20 = AcordPDFXML.CreateElement("ENDOELEMENTEBP20INFO");
								DecPageHomeEndoEBP20.AppendChild(EndoElementEBP20);
								EndoElementEBP20.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBP20.SetAttribute(id,"0");
							}
								#endregion

							}
							//											//dblSumTotal+=int.Parse(htpremium["PRFRD_PLS"].ToString());
						}
									

							break;
						case "EBP22":
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoEBP22;
								DecPageHomeEndoEBP22 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEEBP22");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBP22);
								DecPageHomeEndoEBP22.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBP22.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBP22.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBP22.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBP22"));
									DecPageHomeEndoEBP22.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP22"));
								}
								DecPageHomeEndoEBP22.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBP22EXTN"));
								DecPageHomeEndoEBP22.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP22EXTN"));

								XmlElement EndoElementEBP22;
								EndoElementEBP22 = AcordPDFXML.CreateElement("ENDOELEMENTEBP22INFO");
								DecPageHomeEndoEBP22.AppendChild(EndoElementEBP22);
								EndoElementEBP22.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBP22.SetAttribute(id,"0");
							}		
								#endregion

							}
							break;
						case "EBP24":
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoEBP24;
								DecPageHomeEndoEBP24 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEEBP24");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBP24);
								DecPageHomeEndoEBP24.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBP24.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBP24.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBP24.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBP24"));
									DecPageHomeEndoEBP24.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP24"));
								}
								DecPageHomeEndoEBP24.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBP24EXTN"));
								DecPageHomeEndoEBP24.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP24EXTN"));

								XmlElement EndoElementEBP24;
								EndoElementEBP24 = AcordPDFXML.CreateElement("ENDOELEMENTEBP24INFO");
								DecPageHomeEndoEBP24.AppendChild(EndoElementEBP24);
								EndoElementEBP24.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBP24.SetAttribute(id,"0");
							
							}	
								#endregion

							}
							break;
						case "EBEP11":
							//										//if(htpremium.Contains("EXPND_RPLC_CST"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["EXPND_RPLC_CST"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["EXPND_RPLC_CST"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["EXPND_RPLC_CST"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoEBEP11;
								DecPageHomeEndoEBEP11 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBEP11");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBEP11);
								DecPageHomeEndoEBEP11.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBEP11.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBEP11.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBEP11.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBEP11"));
									DecPageHomeEndoEBEP11.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBEP11"));
								}
								DecPageHomeEndoEBEP11.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBEP11EXTN"));
								DecPageHomeEndoEBEP11.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBEP11EXTN"));

								XmlElement EndoElementEBEP11;
								EndoElementEBEP11 = AcordPDFXML.CreateElement("ENDOELEMENTEBEP11INFO");
								DecPageHomeEndoEBEP11.AppendChild(EndoElementEBEP11);
								EndoElementEBEP11.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBEP11.SetAttribute(id,"0");
							}		
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["EXPND_RPLC_CST"].ToString());
						}
							break;
						case	"EBIF96"	:
							//										//if(htpremium.Contains("FIRE_DPT"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["FIRE_DPT"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FIRE_DPT"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FIRE_DPT"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEBIF96;
								DecPageHomeEndoEBIF96 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBIF96");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBIF96);
								DecPageHomeEndoEBIF96.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBIF96.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBIF96.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBIF96.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBIF96"));
									DecPageHomeEndoEBIF96.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBIF96"));
								}
								DecPageHomeEndoEBIF96.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBIF96EXTN"));
								DecPageHomeEndoEBIF96.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBIF96EXTN"));

								XmlElement EndoElementEBIF96;
								EndoElementEBIF96 = AcordPDFXML.CreateElement("ENDOELEMENTEBIF96INFO");
								DecPageHomeEndoEBIF96.AppendChild(EndoElementEBIF96);
								EndoElementEBIF96.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBIF96.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["FIRE_DPT"].ToString());
						}
							break;
						case	"IBUSP"	:
							//										//if(htpremium.Contains("BSNS_PRP_INCR"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["BSNS_PRP_INCR"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BSNS_PRP_INCR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BSNS_PRP_INCR"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoIBUSP;
								DecPageHomeEndoIBUSP = AcordPDFXML.CreateElement("HOMEENDORSEMENTIBUSP");
								DecPageRootElement.AppendChild(DecPageHomeEndoIBUSP);
								DecPageHomeEndoIBUSP.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIBUSP.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIBUSP.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIBUSP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIBUSP"));
									DecPageHomeEndoIBUSP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSP"));
								}
								DecPageHomeEndoIBUSP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIBUSPEXTN"));
								DecPageHomeEndoIBUSP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPEXTN"));

								XmlElement EndoElementIBUSP;
								EndoElementIBUSP = AcordPDFXML.CreateElement("ENDOELEMENTIBUSPINFO");
								DecPageHomeEndoIBUSP.AppendChild(EndoElementIBUSP);
								EndoElementIBUSP.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIBUSP.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["BSNS_PRP_INCR"].ToString());
						}
							break;
						case	"EBOS40"	:
							//										//if(htpremium.Contains("OTHR_STR_RNT"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["OTHR_STR_RNT"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RNT"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RNT"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEBOS40;
								DecPageHomeEndoEBOS40 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBOS40");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBOS40);
								DecPageHomeEndoEBOS40.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBOS40.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBOS40.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBOS40.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBOS40"));
									DecPageHomeEndoEBOS40.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBOS40"));
								}
								DecPageHomeEndoEBOS40.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBOS40EXTN"));
								DecPageHomeEndoEBOS40.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBOS40EXTN"));

								XmlElement EndoElementEBOS40;
								EndoElementEBOS40 = AcordPDFXML.CreateElement("ENDOELEMENTEBOS40INFO");
								DecPageHomeEndoEBOS40.AppendChild(EndoElementEBOS40);
								EndoElementEBOS40.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBOS40.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["OTHR_STR_RNT"].ToString());
						}
							break;
						case	"EBPPC"	:
							break;
						case	"EBPPOP"	:
							//										//if(htpremium.Contains("PRS_PRP_AWY_PRM"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRS_PRP_AWY_PRM"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_PRP_AWY_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_PRP_AWY_PRM"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEBPPOP;
								DecPageHomeEndoEBPPOP = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBPPOP");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBPPOP);
								DecPageHomeEndoEBPPOP.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBPPOP.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBPPOP.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBPPOP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBPPOP"));
									DecPageHomeEndoEBPPOP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBPPOP"));
								}
								DecPageHomeEndoEBPPOP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBPPOPEXTN"));
								DecPageHomeEndoEBPPOP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBPPOPEXTN"));

								XmlElement EndoElementEBPPOP;
								EndoElementEBPPOP = AcordPDFXML.CreateElement("ENDOELEMENTEBPPOPINFO");
								DecPageHomeEndoEBPPOP.AppendChild(EndoElementEBPPOP);
								EndoElementEBPPOP.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBPPOP.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["PRS_PRP_AWY_PRM"].ToString());
						}
							break;
						case	"EBALEXP"	:
							break;
						case	"EBOS489"	:
							//										//if(htpremium.Contains("OTHR_STR_RPR_CST"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["OTHR_STR_RPR_CST"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RPR_CST"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RPR_CST"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEBOS489;
								DecPageHomeEndoEBOS489 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBOS489");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBOS489);
								DecPageHomeEndoEBOS489.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBOS489.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBOS489.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBOS489.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBOS489"));
									DecPageHomeEndoEBOS489.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBOS489"));
								}
								DecPageHomeEndoEBOS489.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBOS489EXTN"));
								DecPageHomeEndoEBOS489.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBOS489EXTN"));

								XmlElement EndoElementEBOS489;
								EndoElementEBOS489 = AcordPDFXML.CreateElement("ENDOELEMENTEBOS489INFO");
								DecPageHomeEndoEBOS489.AppendChild(EndoElementEBOS489);
								EndoElementEBOS489.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBOS489.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["OTHR_STR_RPR_CST"].ToString());
						}
							break;
						case	"EBICC53"	:
							//if(htpremium.Contains("CRDT_CRD"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["CRDT_CRD"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CRDT_CRD"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CRDT_CRD"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{	
									XmlElement DecPageHomeEndoEBICC53;
									DecPageHomeEndoEBICC53 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBICC53");
									DecPageRootElement.AppendChild(DecPageHomeEndoEBICC53);
									DecPageHomeEndoEBICC53.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEBICC53.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEBICC53.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEBICC53.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBICC53"));
										DecPageHomeEndoEBICC53.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBICC53"));
									}
									DecPageHomeEndoEBICC53.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBICC53EXTN"));
									DecPageHomeEndoEBICC53.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBICC53EXTN"));

									XmlElement EndoElementEBICC53;
									EndoElementEBICC53 = AcordPDFXML.CreateElement("ENDOELEMENTEBICC53INFO");
									DecPageHomeEndoEBICC53.AppendChild(EndoElementEBICC53);
									EndoElementEBICC53.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEBICC53.SetAttribute(id,"0");
									gCtrlnChkHO65=false;
								}	
								#endregion
							}
							//dblSumTotal+=int.Parse(htpremium["CRDT_CRD"].ToString());
						}
							break;
						case	"EBCCSL"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{												
									XmlElement DecPageHomeEndoEBCCSL;
									DecPageHomeEndoEBCCSL = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCCSL");
									DecPageRootElement.AppendChild(DecPageHomeEndoEBCCSL);
									DecPageHomeEndoEBCCSL.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEBCCSL.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEBCCSL.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEBCCSL.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCCSL"));
										DecPageHomeEndoEBCCSL.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSL"));
									}
									DecPageHomeEndoEBCCSL.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCCSLEXTN"));
									DecPageHomeEndoEBCCSL.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSLEXTN"));

									XmlElement EndoElementEBCCSL;
									EndoElementEBCCSL = AcordPDFXML.CreateElement("ENDOELEMENTEBCCSLINFO");
									DecPageHomeEndoEBCCSL.AppendChild(EndoElementEBCCSL);
									EndoElementEBCCSL.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEBCCSL.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion
							}
							break;
						case	"SEWER"	:
							//										//if(htpremium.Contains("ORD_LW"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ORD_LW"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ORD_LW"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ORD_LW"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoSEWER;
								DecPageHomeEndoSEWER = AcordPDFXML.CreateElement("HOMEENDORSEMENTSEWER");
								DecPageRootElement.AppendChild(DecPageHomeEndoSEWER);
								DecPageHomeEndoSEWER.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoSEWER.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoSEWER.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoSEWER.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESEWER"));
									DecPageHomeEndoSEWER.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESEWER"));
								}
								DecPageHomeEndoSEWER.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESEWEREXTN"));
								DecPageHomeEndoSEWER.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESEWEREXTN"));

								XmlElement EndoElementSEWER;
								EndoElementSEWER = AcordPDFXML.CreateElement("ENDOELEMENTSEWERINFO");
								DecPageHomeEndoSEWER.AppendChild(EndoElementSEWER);
								EndoElementSEWER.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementSEWER.SetAttribute(id,"0");
							}
								#endregion											
							}
							//											//dblSumTotal+=int.Parse(htpremium["ORD_LW"].ToString());
						}
							break;
						case	"EROK"	:
							//										//if(htpremium.Contains("ERTHQKE"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ERTHQKE"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								/*
												 //if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
												{
													XmlElement DecPageHomeEndoEROK;
													DecPageHomeEndoEROK = AcordPDFXML.CreateElement("HOMEENDORSEMENTEROK");
													DecPageRootElement.AppendChild(DecPageHomeEndoEROK);
													DecPageHomeEndoEROK.SetAttribute(fieldType,fieldTypeMultiple);
													DecPageHomeEndoEROK.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEROK"));
													DecPageHomeEndoEROK.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEROK"));
													DecPageHomeEndoEROK.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEROKEXTN"));
													DecPageHomeEndoEROK.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEROKEXTN"));

													XmlElement EndoElementEROK;
													EndoElementEROK = AcordPDFXML.CreateElement("ENDOELEMENTEROKINFO");
													DecPageHomeEndoEROK.AppendChild(EndoElementEROK);
													EndoElementEROK.SetAttribute(fieldType,fieldTypeNormal);
													EndoElementEROK.SetAttribute(id,"0");
							
												}				*/
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["ERTHQKE"].ToString());
						}
							break;
						case	"FRAUD"	:
							//										//if(htpremium.Contains("ID_FRAUD"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ID_FRAUD"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ID_FRAUD"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ID_FRAUD"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoFRAUD;
								DecPageHomeEndoFRAUD = AcordPDFXML.CreateElement("HOMEENDORSEMENTFRAUD");
								DecPageRootElement.AppendChild(DecPageHomeEndoFRAUD);
								DecPageHomeEndoFRAUD.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoFRAUD.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoFRAUD.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoFRAUD.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEFRAUD"));
									DecPageHomeEndoFRAUD.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFRAUD"));
								}
								DecPageHomeEndoFRAUD.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEFRAUDEXTN"));
								DecPageHomeEndoFRAUD.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFRAUDEXTN"));

								XmlElement EndoElementFRAUD;
								EndoElementFRAUD = AcordPDFXML.CreateElement("ENDOELEMENTFRAUDINFO");
								DecPageHomeEndoFRAUD.AppendChild(EndoElementFRAUD);
								EndoElementFRAUD.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementFRAUD.SetAttribute(id,"0");
							}	
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["ID_FRAUD"].ToString());
						}
							break;
						case	"EBDUC"	:
							#region Dec Page Element
							//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						{												
							XmlElement DecPageHomeEndoEBDUC;
							DecPageHomeEndoEBDUC = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBDUC");
							DecPageRootElement.AppendChild(DecPageHomeEndoEBDUC);
							DecPageHomeEndoEBDUC.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageHomeEndoEBDUC.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageHomeEndoEBDUC.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageHomeEndoEBDUC.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBDUC"));
								DecPageHomeEndoEBDUC.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBDUC"));
							}
							DecPageHomeEndoEBDUC.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBDUCEXTN"));
							DecPageHomeEndoEBDUC.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBDUCEXTN"));

							XmlElement EndoElementEBDUC;
							EndoElementEBDUC = AcordPDFXML.CreateElement("ENDOELEMENTEBDUCINFO");
							DecPageHomeEndoEBDUC.AppendChild(EndoElementEBDUC);
							EndoElementEBDUC.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementEBDUC.SetAttribute(id,"0");
						}				
							#endregion 
							break;

						case	"EBCASP"	:
							//if(htpremium.Contains("UNT_OWNR_CVG_A"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["UNT_OWNR_CVG_A"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UNT_OWNR_CVG_A"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UNT_OWNR_CVG_A"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["UNT_OWNR_CVG_A"].ToString());
						}
							break;
						case	"EBUNIT"	:
							//if(htpremium.Contains("CND_UNIT"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["CND_UNIT"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_UNIT"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_UNIT"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["CND_UNIT"].ToString());
						}
							break;
						case	"LAC"	:
							break;
						case	"EBAIRP"	:
							#region Dec Page Element
							//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						{												
							XmlElement DecPageHomeEndoEBAIRP;
							DecPageHomeEndoEBAIRP = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBAIRP");
							DecPageRootElement.AppendChild(DecPageHomeEndoEBAIRP);
							DecPageHomeEndoEBAIRP.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageHomeEndoEBAIRP.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageHomeEndoEBAIRP.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageHomeEndoEBAIRP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBAIRP"));
								DecPageHomeEndoEBAIRP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBAIRP"));
							}
							DecPageHomeEndoEBAIRP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBAIRPEXTN"));
							DecPageHomeEndoEBAIRP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBAIRPEXTN"));

							XmlElement EndoElementEBAIRP;
							EndoElementEBAIRP = AcordPDFXML.CreateElement("ENDOELEMENTEBAIRPINFO");
							DecPageHomeEndoEBAIRP.AppendChild(EndoElementEBAIRP);
							EndoElementEBAIRP.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementEBAIRP.SetAttribute(id,"0");
						}				
							#endregion 

							break;
						case	"EBBAA"	:
							//										//if(htpremium.Contains("BLDG_ALTR"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["BLDG_ALTR"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALTR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALTR"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoEBBAA;
								DecPageHomeEndoEBBAA = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBBAA");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBBAA);
								DecPageHomeEndoEBBAA.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBBAA.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBBAA.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBBAA.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBBAA"));
									DecPageHomeEndoEBBAA.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBBAA"));
								}
								DecPageHomeEndoEBBAA.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBBAAEXTN"));
								DecPageHomeEndoEBBAA.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBBAAEXTN"));

								XmlElement EndoElementEBBAA;
								EndoElementEBBAA = AcordPDFXML.CreateElement("ENDOELEMENTEBBAAINFO");
								DecPageHomeEndoEBBAA.AppendChild(EndoElementEBBAA);
								EndoElementEBBAA.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBBAA.SetAttribute(id,"0");
							}	
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["BLDG_ALTR"].ToString());
						}
							break;
						case	"EBRDC"	:
							//										//if(htpremium.Contains("RNT_DLX"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["RNT_DLX"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RNT_DLX"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RNT_DLX"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{										
								XmlElement DecPageHomeEndoEBRDC;
								DecPageHomeEndoEBRDC = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBRDC");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBRDC);
								DecPageHomeEndoEBRDC.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBRDC.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBRDC.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBRDC.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBRDC"));
									DecPageHomeEndoEBRDC.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBRDC"));
								}
								DecPageHomeEndoEBRDC.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBRDCEXTN"));
								DecPageHomeEndoEBRDC.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBRDCEXTN"));

								XmlElement EndoElementEBRDC;
								EndoElementEBRDC = AcordPDFXML.CreateElement("ENDOELEMENTEBRDCINFO");
								DecPageHomeEndoEBRDC.AppendChild(EndoElementEBRDC);
								EndoElementEBRDC.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBRDC.SetAttribute(id,"0");
							}	
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["RNT_DLX"].ToString());
						}
							break;
						case	"EBCDC"	:
							//if(htpremium.Contains("CND_DLX"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["CND_DLX"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_DLX"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_DLX"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								/*
												//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
												{									
													XmlElement DecPageHomeEndoEBCDC;
													DecPageHomeEndoEBCDC = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCDC");
													DecPageRootElement.AppendChild(DecPageHomeEndoEBCDC);
													DecPageHomeEndoEBCDC.SetAttribute(fieldType,fieldTypeMultiple);
													DecPageHomeEndoEBCDC.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCDC"));
													DecPageHomeEndoEBCDC.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCDC"));
													DecPageHomeEndoEBCDC.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCDCEXTN"));
													DecPageHomeEndoEBCDC.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCDCEXTN"));

													XmlElement EndoElementEBCDC;
													EndoElementEBCDC = AcordPDFXML.CreateElement("ENDOELEMENTEBCDCINFO");
													DecPageHomeEndoEBCDC.AppendChild(EndoElementEBCDC);
													EndoElementEBCDC.SetAttribute(fieldType,fieldTypeNormal);
													EndoElementEBCDC.SetAttribute(id,"0");
												}
												*/
								#endregion 
							}
							//dblSumTotal+=int.Parse(htpremium["CND_DLX"].ToString());
						}
							break;
						case	"EBMC"	:

							break;

						case	"RECST"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoRECST;
								DecPageHomeEndoRECST = AcordPDFXML.CreateElement("HOMEENDORSEMENTRECST");
								DecPageRootElement.AppendChild(DecPageHomeEndoRECST);
								DecPageHomeEndoRECST.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoRECST.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoRECST.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoRECST.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERECST"));
									DecPageHomeEndoRECST.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERECST"));
								}
								DecPageHomeEndoRECST.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERECSTEXTN"));
								DecPageHomeEndoRECST.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERECSTEXTN"));

								XmlElement EndoElementRECST;
								EndoElementRECST = AcordPDFXML.CreateElement("ENDOELEMENTRECSTINFO");
								DecPageHomeEndoRECST.AppendChild(EndoElementRECST);
								EndoElementRECST.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementRECST.SetAttribute(id,"0");
							}		
								#endregion 
							}
							break;

						case	"MIN"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoMIN;
								DecPageHomeEndoMIN = AcordPDFXML.CreateElement("HOMEENDORSEMENTMIN");
								DecPageRootElement.AppendChild(DecPageHomeEndoMIN);
								DecPageHomeEndoMIN.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoMIN.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoMIN.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoMIN.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEMIN"));
									DecPageHomeEndoMIN.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEMIN"));
								}
								DecPageHomeEndoMIN.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEMINEXTN"));
								DecPageHomeEndoMIN.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEMINEXTN"));

								XmlElement EndoElementMIN;
								EndoElementMIN = AcordPDFXML.CreateElement("ENDOELEMENTMININFO");
								DecPageHomeEndoMIN.AppendChild(EndoElementMIN);
								EndoElementMIN.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementMIN.SetAttribute(id,"0");
							}		
								#endregion 
							}
							break;	
							
						case	"EBSS490"	:
							//										//if(htpremium.Contains("SPCFC_STR_PRMS"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//												DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SPCFC_STR_PRMS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoEBSS490;
								DecPageHomeEndoEBSS490 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBSS490");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBSS490);
								DecPageHomeEndoEBSS490.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBSS490.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBSS490.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBSS490.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBSS490"));
									DecPageHomeEndoEBSS490.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBSS490"));
								}
								DecPageHomeEndoEBSS490.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBSS490EXTN"));
								DecPageHomeEndoEBSS490.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBSS490EXTN"));

								XmlElement EndoElementEBSS490;
								EndoElementEBSS490 = AcordPDFXML.CreateElement("ENDOELEMENTEBSS490INFO");
								DecPageHomeEndoEBSS490.AppendChild(EndoElementEBSS490);
								EndoElementEBSS490.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBSS490.SetAttribute(id,"0");
							}	
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["SPCFC_STR_PRMS"].ToString());
						}
							break;

						case	"EBCCSM"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{												
									XmlElement DecPageHomeEndoEBCCSM;
									DecPageHomeEndoEBCCSM = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCCSM");
									DecPageRootElement.AppendChild(DecPageHomeEndoEBCCSM);
									DecPageHomeEndoEBCCSM.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEBCCSM.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEBCCSM.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEBCCSM.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCCSM"));
										DecPageHomeEndoEBCCSM.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSM"));
									}
									DecPageHomeEndoEBCCSM.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCCSMEXTN"));
									DecPageHomeEndoEBCCSM.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSMEXTN"));

									XmlElement EndoElementEBCCSM;
									EndoElementEBCCSM = AcordPDFXML.CreateElement("ENDOELEMENTEBCCSMINFO");
									DecPageHomeEndoEBCCSM.AppendChild(EndoElementEBCCSM);
									EndoElementEBCCSM.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEBCCSM.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion 
							}
							break;

						case	"ESCCSS"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{													
									XmlElement DecPageHomeEndoESCCSS;
									DecPageHomeEndoESCCSS = AcordPDFXML.CreateElement("HOMEENDORSEMENTESCCSS");
									DecPageRootElement.AppendChild(DecPageHomeEndoESCCSS);
									DecPageHomeEndoESCCSS.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoESCCSS.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoESCCSS.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoESCCSS.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEESCCSS"));
										DecPageHomeEndoESCCSS.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEESCCSS"));
									}
									DecPageHomeEndoESCCSS.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEESCCSSEXTN"));
									DecPageHomeEndoESCCSS.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEESCCSSEXTN"));

									XmlElement EndoElementESCCSS;
									EndoElementESCCSS = AcordPDFXML.CreateElement("ENDOELEMENTESCCSSINFO");
									DecPageHomeEndoESCCSS.AppendChild(EndoElementESCCSS);
									EndoElementESCCSS.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementESCCSS.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion 
							}
							break;

						case	"EBCCSI"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{													
									XmlElement DecPageHomeEndoEBCCSI;
									DecPageHomeEndoEBCCSI = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCCSI");
									DecPageRootElement.AppendChild(DecPageHomeEndoEBCCSI);
									DecPageHomeEndoEBCCSI.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEBCCSI.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEBCCSI.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEBCCSI.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCCSI"));
										DecPageHomeEndoEBCCSI.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSI"));
									}
									DecPageHomeEndoEBCCSI.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCCSIEXTN"));
									DecPageHomeEndoEBCCSI.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSIEXTN"));

									XmlElement EndoElementEBCCSI;
									EndoElementEBCCSI = AcordPDFXML.CreateElement("ENDOELEMENTEBCCSIINFO");
									DecPageHomeEndoEBCCSI.AppendChild(EndoElementEBCCSI);
									EndoElementEBCCSI.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEBCCSI.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion
							}
							break;

						case	"EBCCSF"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{													
									XmlElement DecPageHomeEndoEBCCSF;
									DecPageHomeEndoEBCCSF = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCCSF");
									DecPageRootElement.AppendChild(DecPageHomeEndoEBCCSF);
									DecPageHomeEndoEBCCSF.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEBCCSF.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEBCCSF.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEBCCSF.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCCSF"));
										DecPageHomeEndoEBCCSF.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSF"));
									}
									DecPageHomeEndoEBCCSF.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCCSFEXTN"));
									DecPageHomeEndoEBCCSF.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSFEXTN"));

									XmlElement EndoElementEBCCSF;
									EndoElementEBCCSF = AcordPDFXML.CreateElement("ENDOELEMENTEBCCSFINFO");
									DecPageHomeEndoEBCCSF.AppendChild(EndoElementEBCCSF);
									EndoElementEBCCSF.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEBCCSF.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion
							}
							break;


						case	"WBSPO"	:
							//										//if(htpremium.Contains("BK_UP_SM_PMP"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["BK_UP_SM_PMP"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BK_UP_SM_PMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BK_UP_SM_PMP"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoWBSPO;
								DecPageHomeEndoWBSPO = AcordPDFXML.CreateElement("HOMEENDORSEMENTWBSPO");
								DecPageRootElement.AppendChild(DecPageHomeEndoWBSPO);
								DecPageHomeEndoWBSPO.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoWBSPO.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoWBSPO.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoWBSPO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEWBSPO"));
									DecPageHomeEndoWBSPO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWBSPO"));
								}
								DecPageHomeEndoWBSPO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWBSPOEXTN"));
								DecPageHomeEndoWBSPO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWBSPOEXTN"));

								XmlElement EndoElementWBSPO;
								EndoElementWBSPO = AcordPDFXML.CreateElement("ENDOELEMENTWBSPOINFO");
								DecPageHomeEndoWBSPO.AppendChild(EndoElementWBSPO);
								EndoElementWBSPO.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementWBSPO.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["BK_UP_SM_PMP"].ToString());
						}
							break;


						case	"APOBI"	:
							//if(htpremium.Contains("ADDL_PRM_OCC_INS"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ADDL_PRM_OCC_INS"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_OCC_INS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_OCC_INS"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_OCC_INS"].ToString());
						}
							break;


						case	"APRPR"	:
							//										//if(htpremium.Contains("ADDL_PRM_RNTD_OTH_RES"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoAPRPR;
								DecPageHomeEndoAPRPR = AcordPDFXML.CreateElement("HOMEENDORSEMENTAPRPR");
								DecPageRootElement.AppendChild(DecPageHomeEndoAPRPR);
								DecPageHomeEndoAPRPR.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoAPRPR.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoAPRPR.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoAPRPR.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAPRPR"));
									DecPageHomeEndoAPRPR.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAPRPR"));
								}
								DecPageHomeEndoAPRPR.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAPRPREXTN"));
								DecPageHomeEndoAPRPR.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAPRPREXTN"));

								XmlElement EndoElementAPRPR;
								EndoElementAPRPR = AcordPDFXML.CreateElement("ENDOELEMENTAPRPRINFO");
								DecPageHomeEndoAPRPR.AppendChild(EndoElementAPRPR);
								EndoElementAPRPR.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementAPRPR.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString());
						}
							break;


						case	"APOLR"	:
							//if(htpremium.Contains("ADDL_PRM_RNTD_1FMLY"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ADDL_PRM_RNTD_1FMLY"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_1FMLY"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_1FMLY"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_RNTD_1FMLY"].ToString());
						}
							break;


						case	"APOLO"	:
							//if(htpremium.Contains("ADDL_PRM_RNTD_2FMLY"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ADDL_PRM_RNTD_2FMLY"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_2FMLY"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_2FMLY"].ToString())+"</HM_ENDPREMIUM>";

							}
							//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_RNTD_2FMLY"].ToString());
						}
							break;


						case	"IOPSS"	:
							//										//if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_PRM"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoIOPSS;
								DecPageHomeEndoIOPSS = AcordPDFXML.CreateElement("HOMEENDORSEMENTIOPSS");
								DecPageRootElement.AppendChild(DecPageHomeEndoIOPSS);
								DecPageHomeEndoIOPSS.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIOPSS.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIOPSS.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIOPSS.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIOPSS"));
									DecPageHomeEndoIOPSS.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSS"));
								}
								DecPageHomeEndoIOPSS.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIOPSSEXTN"));
								DecPageHomeEndoIOPSS.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSSEXTN"));

								XmlElement EndoElementIOPSS;
								EndoElementIOPSS = AcordPDFXML.CreateElement("ENDOELEMENTIOPSSINFO");
								DecPageHomeEndoIOPSS.AppendChild(EndoElementIOPSS);
								EndoElementIOPSS.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIOPSS.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString());
						}
							break;


						case	"IOPSL"	:
							break;


						case	"IOPSI"	:
							//										//if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_INST"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoIOPSI;
								DecPageHomeEndoIOPSI = AcordPDFXML.CreateElement("HOMEENDORSEMENTIOPSI");
								DecPageRootElement.AppendChild(DecPageHomeEndoIOPSI);
								DecPageHomeEndoIOPSI.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIOPSI.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIOPSI.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIOPSI.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIOPSI"));
									DecPageHomeEndoIOPSI.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSI"));
								}
								DecPageHomeEndoIOPSI.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIOPSIEXTN"));
								DecPageHomeEndoIOPSI.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSIEXTN"));

								XmlElement EndoElementIOPSI;
								EndoElementIOPSI = AcordPDFXML.CreateElement("ENDOELEMENTIOPSIINFO");
								DecPageHomeEndoIOPSI.AppendChild(EndoElementIOPSI);
								EndoElementIOPSI.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIOPSI.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString());
						}
							break;


						case	"IOPSO"	:
							//										//if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_OFF_PRM"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoIOPSO;
								DecPageHomeEndoIOPSO = AcordPDFXML.CreateElement("HOMEENDORSEMENTIOPSO");
								DecPageRootElement.AppendChild(DecPageHomeEndoIOPSO);
								DecPageHomeEndoIOPSO.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIOPSO.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIOPSO.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIOPSO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIOPSO"));
									DecPageHomeEndoIOPSO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSO"));
								}
								DecPageHomeEndoIOPSO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIOPSOEXTN"));
								DecPageHomeEndoIOPSO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSOEXTN"));

								XmlElement EndoElementIOPSO;
								EndoElementIOPSO = AcordPDFXML.CreateElement("ENDOELEMENTIOPSOINFO");
								DecPageHomeEndoIOPSO.AppendChild(EndoElementIOPSO);
								EndoElementIOPSO.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIOPSO.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString());
						}
							break;


						case	"PERIJ"	:
							//										//if(htpremium.Contains("PRS_INJRY"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRS_INJRY"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_INJRY"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_INJRY"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoPERIJ;
								DecPageHomeEndoPERIJ = AcordPDFXML.CreateElement("HOMEENDORSEMENTPERIJ");
								DecPageRootElement.AppendChild(DecPageHomeEndoPERIJ);
								DecPageHomeEndoPERIJ.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoPERIJ.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoPERIJ.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoPERIJ.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPERIJ"));
									DecPageHomeEndoPERIJ.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPERIJ"));
								}
								DecPageHomeEndoPERIJ.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPERIJEXTN"));
								DecPageHomeEndoPERIJ.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPERIJEXTN"));

								XmlElement EndoElementPERIJ;
								EndoElementPERIJ = AcordPDFXML.CreateElement("ENDOELEMENTPERIJINFO");
								DecPageHomeEndoPERIJ.AppendChild(EndoElementPERIJ);
								EndoElementPERIJ.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementPERIJ.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["PRS_INJRY"].ToString());
						}
							break;


						case	"REEMN"	:
							//if(htpremium.Contains("RESI_EMPL"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["RESI_EMPL"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RESI_EMPL"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RESI_EMPL"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["RESI_EMPL"].ToString());
						}
							break;

						case	"BPCES"	:

							//										//if(htpremium.Contains("BSNS_PRSUIT"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoBPCES;
								DecPageHomeEndoBPCES = AcordPDFXML.CreateElement("HOMEENDORSEMENTBPCES");
								DecPageRootElement.AppendChild(DecPageHomeEndoBPCES);
								DecPageHomeEndoBPCES.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoBPCES.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoBPCES.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoBPCES.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBPCES"));
									DecPageHomeEndoBPCES.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPCES"));
								}
								DecPageHomeEndoBPCES.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBPCESEXTN"));
								DecPageHomeEndoBPCES.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPCESEXTN"));

								XmlElement EndoElementBPCES;
								EndoElementBPCES = AcordPDFXML.CreateElement("ENDOELEMENTBPCESINFO");
								DecPageHomeEndoBPCES.AppendChild(EndoElementBPCES);
								EndoElementBPCES.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementBPCES.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["BSNS_PRSUIT"].ToString());
						}
							break;

						case	"BPSCM"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoBPSCM;
								DecPageHomeEndoBPSCM = AcordPDFXML.CreateElement("HOMEENDORSEMENTBPSCM");
								DecPageRootElement.AppendChild(DecPageHomeEndoBPSCM);
								DecPageHomeEndoBPSCM.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoBPSCM.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoBPSCM.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoBPSCM.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBPSCM"));
									DecPageHomeEndoBPSCM.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPSCM"));
								}
								DecPageHomeEndoBPSCM.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBPSCMEXTN"));
								DecPageHomeEndoBPSCM.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPSCMEXTN"));

								XmlElement EndoElementBPSCM;
								EndoElementBPSCM = AcordPDFXML.CreateElement("ENDOELEMENTBPSCMINFO");
								DecPageHomeEndoBPSCM.AppendChild(EndoElementBPSCM);
								EndoElementBPSCM.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementBPSCM.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;

						case	"BPTAL"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoBPTAL;
								DecPageHomeEndoBPTAL = AcordPDFXML.CreateElement("HOMEENDORSEMENTBPTAL");
								DecPageRootElement.AppendChild(DecPageHomeEndoBPTAL);
								DecPageHomeEndoBPTAL.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoBPTAL.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoBPTAL.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoBPTAL.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBPTAL"));
									DecPageHomeEndoBPTAL.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPTAL"));
								}
								DecPageHomeEndoBPTAL.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBPTALEXTN"));
								DecPageHomeEndoBPTAL.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPTALEXTN"));

								XmlElement EndoElementBPTAL;
								EndoElementBPTAL = AcordPDFXML.CreateElement("ENDOELEMENTBPTALINFO");
								DecPageHomeEndoBPTAL.AppendChild(EndoElementBPTAL);
								EndoElementBPTAL.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementBPTAL.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;


						case	"BPTNO"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoBPTNO;
								DecPageHomeEndoBPTNO = AcordPDFXML.CreateElement("HOMEENDORSEMENTBPTNO");
								DecPageRootElement.AppendChild(DecPageHomeEndoBPTNO);
								DecPageHomeEndoBPTNO.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoBPTNO.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoBPTNO.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoBPTNO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBPTNO"));
									DecPageHomeEndoBPTNO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPTNO"));
								}
								DecPageHomeEndoBPTNO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBPTNOEXTN"));
								DecPageHomeEndoBPTNO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPTNOEXTN"));

								XmlElement EndoElementBPTNO;
								EndoElementBPTNO = AcordPDFXML.CreateElement("ENDOELEMENTBPTNOINFO");
								DecPageHomeEndoBPTNO.AppendChild(EndoElementBPTNO);
								EndoElementBPTNO.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementBPTNO.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;


						case	"FLIFP"	:
							//										//if(htpremium.Contains("INCT_FRMG_RESI_PRM"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCT_FRMG_RESI_PRM"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RESI_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RESI_PRM"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoFLIFP;
								DecPageHomeEndoFLIFP = AcordPDFXML.CreateElement("HOMEENDORSEMENTFLIFP");
								DecPageRootElement.AppendChild(DecPageHomeEndoFLIFP);
								DecPageHomeEndoFLIFP.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoFLIFP.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoFLIFP.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoFLIFP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEFLIFP"));
									DecPageHomeEndoFLIFP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFLIFP"));
								}
								DecPageHomeEndoFLIFP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEFLIFPEXTN"));
								DecPageHomeEndoFLIFP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFLIFPEXTN"));

								XmlElement EndoElementFLIFP;
								EndoElementFLIFP = AcordPDFXML.CreateElement("ENDOELEMENTFLIFPINFO");
								DecPageHomeEndoFLIFP.AppendChild(EndoElementFLIFP);
								EndoElementFLIFP.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementFLIFP.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCT_FRMG_RESI_PRM"].ToString());
						}
							break;
						case	"FLOFO"	:
							//										//if(htpremium.Contains("INCT_FRMG_OPRT_INS"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCT_FRMG_OPRT_INS"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_OPRT_INS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_OPRT_INS"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								/*											
												//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
												{
													XmlElement DecPageHomeEndoFLOFO;
													DecPageHomeEndoFLOFO = AcordPDFXML.CreateElement("HOMEENDORSEMENTFLOFO");
													DecPageRootElement.AppendChild(DecPageHomeEndoFLOFO);
													DecPageHomeEndoFLOFO.SetAttribute(fieldType,fieldTypeMultiple);
													DecPageHomeEndoFLOFO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEFLOFO"));
													DecPageHomeEndoFLOFO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFLOFO"));
													DecPageHomeEndoFLOFO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEFLOFOEXTN"));
													DecPageHomeEndoFLOFO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFLOFOEXTN"));

													XmlElement EndoElementFLOFO;
													EndoElementFLOFO = AcordPDFXML.CreateElement("ENDOELEMENTFLOFOINFO");
													DecPageHomeEndoFLOFO.AppendChild(EndoElementFLOFO);
													EndoElementFLOFO.SetAttribute(fieldType,fieldTypeNormal);
													EndoElementFLOFO.SetAttribute(id,"0");
												}
																*/
								#endregion
										
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCT_FRMG_OPRT_INS"].ToString());
						}
							break;
						case	"FLOFR"	:
							//if(htpremium.Contains("INCT_FRMG_RNTD_OTH"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCT_FRMG_RNTD_OTH"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RNTD_OTH"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RNTD_OTH"].ToString() )+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["INCT_FRMG_RNTD_OTH"].ToString());
						}
							break;


						case	"OSTDISH"	:
									
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoOSTDISH;
								DecPageHomeEndoOSTDISH = AcordPDFXML.CreateElement("HOMEENDORSEMENTOSTDISH");
								DecPageRootElement.AppendChild(DecPageHomeEndoOSTDISH);
								DecPageHomeEndoOSTDISH.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoOSTDISH.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoOSTDISH.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoOSTDISH.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOSTDISH"));
									DecPageHomeEndoOSTDISH.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOSTDISH"));
								}
								DecPageHomeEndoOSTDISH.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOSTDISHEXTN"));
								DecPageHomeEndoOSTDISH.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOSTDISHEXTN"));

								XmlElement EndoElementOSTDISH;
								EndoElementOSTDISH = AcordPDFXML.CreateElement("ENDOELEMENTOSTDISHINFO");
								DecPageHomeEndoOSTDISH.AppendChild(EndoElementOSTDISH);
								EndoElementOSTDISH.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementOSTDISH.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;


						case	"HO200"	:
							//										//if(htpremium.Contains("WTRBD_LBLTY"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["WTRBD_LBLTY"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WTRBD_LBLTY"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WTRBD_LBLTY"].ToString())+"</HM_ENDPREMIUM>";
											
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoHO200;
								DecPageHomeEndoHO200 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO200");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO200);
								DecPageHomeEndoHO200.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO200.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO200.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO200.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO200"));
									DecPageHomeEndoHO200.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO200"));
								}
								DecPageHomeEndoHO200.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO200EXTN"));
								DecPageHomeEndoHO200.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO200EXTN"));

								XmlElement EndoElementHO200;
								EndoElementHO200 = AcordPDFXML.CreateElement("ENDOELEMENTHO200INFO");
								DecPageHomeEndoHO200.AppendChild(EndoElementHO200);
								EndoElementHO200.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO200.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["WTRBD_LBLTY"].ToString());
						}
							break;
						case	"HO9"	:
							//										//if(htpremium.Contains("SUB_SRFCE_WTR"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["SUB_SRFCE_WTR"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SUB_SRFCE_WTR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SUB_SRFCE_WTR"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO9;
								DecPageHomeEndoHO9 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO9");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO9);
								DecPageHomeEndoHO9.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO9.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO9.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO9.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO9"));
									DecPageHomeEndoHO9.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO9"));
								}
								DecPageHomeEndoHO9.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO9EXTN"));
								DecPageHomeEndoHO9.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO9EXTN"));

								XmlElement EndoElementHO9;
								EndoElementHO9 = AcordPDFXML.CreateElement("ENDOELEMENTHO9INFO");
								DecPageHomeEndoHO9.AppendChild(EndoElementHO9);
								EndoElementHO9.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO9.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["SUB_SRFCE_WTR"].ToString());
						}
							break;
						case	"REDUC"	:

							break;
						case	"BICYC"	:
							//if(htpremium.Contains("BICYCLE"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["BICYCLE"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BICYCLE"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BICYCLE"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["BICYCLE"].ToString());
						}
							break;
						case	"CAMER"	:
							//if(htpremium.Contains("CMRA"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["CMRA"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CMRA"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CMRA"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["CMRA"].ToString());
						}
							break;
						case	"CELLU"	:
							//if(htpremium.Contains("CELL"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["CELL"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CELL"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CELL"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["CELL"].ToString());
						}
							break;
						case	"FINEBR"	:
							//if(htpremium.Contains("FNE_ART_WTH_BRK"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["FNE_ART_WTH_BRK"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WTH_BRK"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WTH_BRK"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["FNE_ART_WTH_BRK"].ToString());
						}
							break;
						case	"FINEWBR"	:
							//if(htpremium.Contains("FNE_ART_WO_BRK"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["FNE_ART_WO_BRK"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["FNE_ART_WO_BRK"].ToString());
						}
							break;
						case	"FURS"	:
							//if(htpremium.Contains("FUR"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["FUR"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FUR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FUR"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["FUR"].ToString());
						}
							break;
						case	"GOLF"	:
							//if(htpremium.Contains("GOLF"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["GOLF"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GOLF"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GOLF"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["GOLF"].ToString());
						}
							break;
						case	"GUNS"	:
							//if(htpremium.Contains("GUN"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["GUN"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GUN"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GUN"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["GUN"].ToString());
						}
							break;
						case	"HANDI"	:
							break;
						case	"HEARI"	:
							break;
						case	"INSUL"	:
							break;
						case	"JEWEL"	:
							//if(htpremium.Contains("JWL"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["JWL"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["JWL"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["JWL"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["JWL"].ToString());
						}
							break;
						case	"MART"	:
							break;
						case	"MUSIC"	:
							//if(htpremium.Contains("MSC_NON_PRF"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["MSC_NON_PRF"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MSC_NON_PRF"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MSC_NON_PRF"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["MSC_NON_PRF"].ToString());
						}
							break;
						case	"PERSOD"	:
							//if(htpremium.Contains("PRS_CMP"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRS_CMP"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString())+"</HM_ENDPREMIUM>";
							}
							//dblSumTotal+=int.Parse(htpremium["PRS_CMP"].ToString());
						}
									
							break;
						case	"PERSOL"	:
							//if(htpremium.Contains("PRS_CMP"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRS_CMP"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString())+"</HM_ENDPREMIUM>";

							}
							////dblSumTotal+=int.Parse(htpremium["PRS_CMP"].ToString());
						}
										
							break;
						case	"RARE"	:
							//if(htpremium.Contains("RARE_COIN"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["RARE_COIN"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RARE_COIN"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RARE_COIN"].ToString())+"</HM_ENDPREMIUM>";

							}
							////dblSumTotal+=int.Parse(htpremium["RARE_COIN"].ToString());
						}
							break;
						case	"SALES"	:
							break;
						case	"SCUBA"	:
							break;
						case	"SILVE"	:
							//if(htpremium.Contains("SLVR"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["SLVR"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SLVR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SLVR"].ToString())+"</HM_ENDPREMIUM>";
							}
							////dblSumTotal+=int.Parse(htpremium["SLVR"].ToString());
						}
							break;
						case	"SNOW"	:
							break;
						case	"STAMP"	:
							//if(htpremium.Contains("STMP"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["STMP"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["STMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["STMP"].ToString())+"</HM_ENDPREMIUM>";
							}
							////dblSumTotal+=int.Parse(htpremium["STMP"].ToString());
						}
							break;
						case	"TACK"	:
							break;
						case	"TOOLSP"	:
							break;
						case	"TOOLSB"	:
							break;
						case	"TRACT"	:
							break;
						case	"TRAIN"	:
							break;
						case	"WHEEL"	:
							break;
						case	"EOP17"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEOP17;
								DecPageHomeEndoEOP17 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEOP17");
								DecPageRootElement.AppendChild(DecPageHomeEndoEOP17);
								DecPageHomeEndoEOP17.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEOP17.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEOP17.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEOP17.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEOP17"));
									DecPageHomeEndoEOP17.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEOP17"));
								}
								DecPageHomeEndoEOP17.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEOP17EXTN"));
								DecPageHomeEndoEOP17.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEOP17EXTN"));

								XmlElement EndoElementEOP17;
								EndoElementEOP17 = AcordPDFXML.CreateElement("ENDOELEMENTEOP17INFO");
								DecPageHomeEndoEOP17.AppendChild(EndoElementEOP17);
								EndoElementEOP17.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEOP17.SetAttribute(id,"0");
							}		
								#endregion
							}

							break;
						case	"ECOB"	:
							break;
						case	"ECOC"	:
							break;
						case	"BUMC"	:
							break;
						case	"LF330"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoLF330;
								DecPageHomeEndoLF330 = AcordPDFXML.CreateElement("HOMEENDORSEMENTLF330");
								DecPageRootElement.AppendChild(DecPageHomeEndoLF330);
								DecPageHomeEndoLF330.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoLF330.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoLF330.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoLF330.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGELF330"));
									DecPageHomeEndoLF330.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGELF330"));
								}
								DecPageHomeEndoLF330.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGELF330EXTN"));
								DecPageHomeEndoLF330.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGELF330EXTN"));

								XmlElement EndoElementLF330;
								EndoElementLF330 = AcordPDFXML.CreateElement("ENDOELEMENTLF330INFO");
								DecPageHomeEndoLF330.AppendChild(EndoElementLF330);
								EndoElementLF330.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementLF330.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;
						case	"SP350"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								/*												
											//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
											{		
												XmlElement DecPageHomeEndoSP350;
												DecPageHomeEndoSP350 = AcordPDFXML.CreateElement("HOMEENDORSEMENTSP350");
												DecPageRootElement.AppendChild(DecPageHomeEndoSP350);
												DecPageHomeEndoSP350.SetAttribute(fieldType,fieldTypeMultiple);
												DecPageHomeEndoSP350.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESP350"));
												DecPageHomeEndoSP350.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESP350"));
												DecPageHomeEndoSP350.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESP350EXTN"));
												DecPageHomeEndoSP350.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESP350EXTN"));

												XmlElement EndoElementSP350;
												EndoElementSP350 = AcordPDFXML.CreateElement("ENDOELEMENTSP350INFO");
												DecPageHomeEndoSP350.AppendChild(EndoElementSP350);
												EndoElementSP350.SetAttribute(fieldType,fieldTypeNormal);
												EndoElementSP350.SetAttribute(id,"0");
											}	*/	
								#endregion
							}
							break;
						case	"LAS360"	:


							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoLAS360;
								DecPageHomeEndoLAS360 = AcordPDFXML.CreateElement("HOMEENDORSEMENTLAS360");
								DecPageRootElement.AppendChild(DecPageHomeEndoLAS360);
								DecPageHomeEndoLAS360.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoLAS360.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoLAS360.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoLAS360.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGELAS360"));
									DecPageHomeEndoLAS360.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGELAS360"));
								}
								DecPageHomeEndoLAS360.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGELAS360EXTN"));
								DecPageHomeEndoLAS360.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGELAS360EXTN"));

								XmlElement EndoElementLAS360;
								EndoElementLAS360 = AcordPDFXML.CreateElement("ENDOELEMENTLAS360INFO");
								DecPageHomeEndoLAS360.AppendChild(EndoElementLAS360);
								EndoElementLAS360.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementLAS360.SetAttribute(id,"0");
							}		
								#endregion
							}
									
							break;
						case	"CWC373"	:
							//										//if(htpremium.Contains("WRKR_CMP"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["WRKR_CMP"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WRKR_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WRKR_CMP"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoCWC373;
								DecPageHomeEndoCWC373 = AcordPDFXML.CreateElement("HOMEENDORSEMENTCWC373");
								DecPageRootElement.AppendChild(DecPageHomeEndoCWC373);
								DecPageHomeEndoCWC373.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoCWC373.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoCWC373.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoCWC373.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECWC373"));
									DecPageHomeEndoCWC373.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECWC373"));
								}
								DecPageHomeEndoCWC373.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECWC373EXTN"));
								DecPageHomeEndoCWC373.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECWC373EXTN"));

								XmlElement EndoElementCWC373;
								EndoElementCWC373 = AcordPDFXML.CreateElement("ENDOELEMENTCWC373INFO");
								DecPageHomeEndoCWC373.AppendChild(EndoElementCWC373);
								EndoElementCWC373.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementCWC373.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["WRKR_CMP"].ToString());
						}
							break;

						case	"LLE382"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoLLE382;
								DecPageHomeEndoLLE382 = AcordPDFXML.CreateElement("HOMEENDORSEMENTLLE382");
								DecPageRootElement.AppendChild(DecPageHomeEndoLLE382);
								DecPageHomeEndoLLE382.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoLLE382.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoLLE382.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoLLE382.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGELLE382"));
									DecPageHomeEndoLLE382.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGELLE382"));
								}
								DecPageHomeEndoLLE382.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGELLE382EXTN"));
								DecPageHomeEndoLLE382.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGELLE382EXTN"));

								XmlElement EndoElementLLE382;
								EndoElementLLE382 = AcordPDFXML.CreateElement("ENDOELEMENTLLE382INFO");
								DecPageHomeEndoLLE382.AppendChild(EndoElementLLE382);
								EndoElementLLE382.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementLLE382.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;
						case	"CBF4"	:

							break;
						case	"CF5"	:

							break;
						case	"CUOF6"	:

							break;
						case	"SPP900"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoSPP900;
								DecPageHomeEndoSPP900 = AcordPDFXML.CreateElement("HOMEENDORSEMENTSPP900");
								DecPageRootElement.AppendChild(DecPageHomeEndoSPP900);
								DecPageHomeEndoSPP900.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoSPP900.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoSPP900.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoSPP900.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESPP900"));
									DecPageHomeEndoSPP900.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESPP900"));
								}
								DecPageHomeEndoSPP900.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESPP900EXTN"));
								DecPageHomeEndoSPP900.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESPP900EXTN"));

								XmlElement EndoElementSPP900;
								EndoElementSPP900 = AcordPDFXML.CreateElement("ENDOELEMENTSPP900INFO");
								DecPageHomeEndoSPP900.AppendChild(EndoElementSPP900);
								EndoElementSPP900.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementSPP900.SetAttribute(id,"0");
							}		
								#endregion

							}
							break;

						case	"CO100"	:
							break;
						case	"HO100"	:
							break;
						case	"PFBF2"	:
							break;
						case	"NSPC220"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoNSPC220;
								DecPageHomeEndoNSPC220 = AcordPDFXML.CreateElement("HOMEENDORSEMENTNSPC220");
								DecPageRootElement.AppendChild(DecPageHomeEndoNSPC220);
								DecPageHomeEndoNSPC220.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoNSPC220.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoNSPC220.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoNSPC220.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGENSPC220"));
									DecPageHomeEndoNSPC220.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGENSPC220"));
								}
								DecPageHomeEndoNSPC220.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGENSPC220EXTN"));
								DecPageHomeEndoNSPC220.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGENSPC220EXTN"));

								XmlElement EndoElementNSPC220;
								EndoElementNSPC220 = AcordPDFXML.CreateElement("ENDOELEMENTNSPC220INFO");
								DecPageHomeEndoNSPC220.AppendChild(EndoElementNSPC220);
								EndoElementNSPC220.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementNSPC220.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;

						case	"PFSF3"	:

							break;
						case	"IBUSPA"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoIBUSPA;
								DecPageHomeEndoIBUSPA = AcordPDFXML.CreateElement("HOMEENDORSEMENTIBUSPA");
								DecPageRootElement.AppendChild(DecPageHomeEndoIBUSPA);
								DecPageHomeEndoIBUSPA.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIBUSPA.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIBUSPA.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIBUSPA.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIBUSPA"));
									DecPageHomeEndoIBUSPA.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPA"));
								}
								DecPageHomeEndoIBUSPA.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIBUSPAEXTN"));
								DecPageHomeEndoIBUSPA.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPAEXTN"));

								XmlElement EndoElementIBUSPA;
								EndoElementIBUSPA = AcordPDFXML.CreateElement("ENDOELEMENTIBUSPAINFO");
								DecPageHomeEndoIBUSPA.AppendChild(EndoElementIBUSPA);
								EndoElementIBUSPA.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIBUSPA.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;


						case	"IBUSPO"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoIBUSPO;
								DecPageHomeEndoIBUSPO = AcordPDFXML.CreateElement("HOMEENDORSEMENTIBUSPO");
								DecPageRootElement.AppendChild(DecPageHomeEndoIBUSPO);
								DecPageHomeEndoIBUSPO.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIBUSPO.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIBUSPO.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIBUSPO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIBUSPO"));
									DecPageHomeEndoIBUSPO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPO"));
								}
								DecPageHomeEndoIBUSPO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIBUSPOEXTN"));
								DecPageHomeEndoIBUSPO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPOEXTN"));

								XmlElement EndoElementIBUSPO;
								EndoElementIBUSPO = AcordPDFXML.CreateElement("ENDOELEMENTIBUSPOINFO");
								DecPageHomeEndoIBUSPO.AppendChild(EndoElementIBUSPO);
								EndoElementIBUSPO.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIBUSPO.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;


						case	"IBUSPOA"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoIBUSPOA;
								DecPageHomeEndoIBUSPOA = AcordPDFXML.CreateElement("HOMEENDORSEMENTIBUSPOA");
								DecPageRootElement.AppendChild(DecPageHomeEndoIBUSPOA);
								DecPageHomeEndoIBUSPOA.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIBUSPOA.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIBUSPOA.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIBUSPOA.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIBUSPOA"));
									DecPageHomeEndoIBUSPOA.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPOA"));
								}
								DecPageHomeEndoIBUSPOA.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIBUSPOAEXTN"));
								DecPageHomeEndoIBUSPOA.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPOAEXTN"));

								XmlElement EndoElementIBUSPOA;
								EndoElementIBUSPOA = AcordPDFXML.CreateElement("ENDOELEMENTIBUSPOAINFO");
								DecPageHomeEndoIBUSPOA.AppendChild(EndoElementIBUSPOA);
								EndoElementIBUSPOA.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIBUSPOA.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;

						case	"AROF1"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoAROF1;
								DecPageHomeEndoAROF1 = AcordPDFXML.CreateElement("HOMEENDORSEMENTAROF1");
								DecPageRootElement.AppendChild(DecPageHomeEndoAROF1);
								DecPageHomeEndoAROF1.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoAROF1.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoAROF1.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoAROF1.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAROF1"));
									DecPageHomeEndoAROF1.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAROF1"));
								}
								DecPageHomeEndoAROF1.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAROF1EXTN"));
								DecPageHomeEndoAROF1.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAROF1EXTN"));

								XmlElement EndoElementAROF1;
								EndoElementAROF1 = AcordPDFXML.CreateElement("ENDOELEMENTAROF1INFO");
								DecPageHomeEndoAROF1.AppendChild(EndoElementAROF1);
								EndoElementAROF1.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementAROF1.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;


						case	"AROF2"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoAROF2;
								DecPageHomeEndoAROF2 = AcordPDFXML.CreateElement("HOMEENDORSEMENTAROF2");
								DecPageRootElement.AppendChild(DecPageHomeEndoAROF2);
								DecPageHomeEndoAROF2.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoAROF2.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoAROF2.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoAROF2.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAROF2"));
									DecPageHomeEndoAROF2.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAROF2"));
								}
								DecPageHomeEndoAROF2.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAROF2EXTN"));
								DecPageHomeEndoAROF2.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAROF2EXTN"));

								XmlElement EndoElementAROF2;
								EndoElementAROF2= AcordPDFXML.CreateElement("ENDOELEMENTAROF2INFO");
								DecPageHomeEndoAROF2.AppendChild(EndoElementAROF2);
								EndoElementAROF2.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementAROF2.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;
						case	"HO417"	:
							if (gStrPdfFor == PDFForDecPage)
							{
	
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO417;
								DecPageHomeEndoHO417 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO417");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO417);
								DecPageHomeEndoHO417.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO417.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO417.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO417.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO417"));
									DecPageHomeEndoHO417.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO417"));
								}
								DecPageHomeEndoHO417.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO417EXTN"));
								DecPageHomeEndoHO417.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO417EXTN"));

								XmlElement EndoElementHO417;
								EndoElementHO417= AcordPDFXML.CreateElement("ENDOELEMENTHO417INFO");
								DecPageHomeEndoHO417.AppendChild(EndoElementHO417);
								EndoElementHO417.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO417.SetAttribute(id,"0");
							}
											
								#endregion
							}
							break;


						case	"HO61"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO61;
								DecPageHomeEndoHO61 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO61");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO61);
								DecPageHomeEndoHO61.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO61.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO61.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO61.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO61"));
									DecPageHomeEndoHO61.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO61"));
								}
								DecPageHomeEndoHO61.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO61EXTN"));
								DecPageHomeEndoHO61.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO61EXTN"));

								XmlElement EndoElementHO61;
								EndoElementHO61= AcordPDFXML.CreateElement("ENDOELEMENTHO61INFO");
								DecPageHomeEndoHO61.AppendChild(EndoElementHO61);
								EndoElementHO61.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO61.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;


						case	"HO214"	:
							//										//if(htpremium.Contains("PRS_CMP"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRS_CMP"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO214;
								DecPageHomeEndoHO214 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO214");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO214);
								DecPageHomeEndoHO214.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO214.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO214.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO214.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO214"));
									DecPageHomeEndoHO214.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO214"));
								}
								DecPageHomeEndoHO214.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO214EXTN"));
								DecPageHomeEndoHO214.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO214EXTN"));

								XmlElement EndoElementHO214;
								EndoElementHO214= AcordPDFXML.CreateElement("ENDOELEMENTHO214INFO");
								DecPageHomeEndoHO214.AppendChild(EndoElementHO214);
								EndoElementHO214.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO214.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["PRS_CMP"].ToString());
						}
							break;
								
						case	"HO216"	:
								
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO216;
								DecPageHomeEndoHO216 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO216");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO216);
								DecPageHomeEndoHO216.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO216.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO216.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO216.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO216"));
									DecPageHomeEndoHO216.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO216"));
								}
								DecPageHomeEndoHO216.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO216EXTN"));
								DecPageHomeEndoHO216.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO216EXTN"));

								XmlElement EndoElementHO216;
								EndoElementHO216= AcordPDFXML.CreateElement("ENDOELEMENTHO216INFO");
								DecPageHomeEndoHO216.AppendChild(EndoElementHO216);
								EndoElementHO216.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO216.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;

						case	"HO300"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoHO300;
								DecPageHomeEndoHO300 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO300");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO300);
								DecPageHomeEndoHO300.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO300.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO300.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO300.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO300"));
									DecPageHomeEndoHO300.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO300"));
								}
								DecPageHomeEndoHO300.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO300EXTN"));
								DecPageHomeEndoHO300.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO300EXTN"));

								XmlElement EndoElementHO300;
								EndoElementHO300= AcordPDFXML.CreateElement("ENDOELEMENTHO300INFO");
								DecPageHomeEndoHO300.AppendChild(EndoElementHO300);
								EndoElementHO300.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO300.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;

						case	"HO864"	:

							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO864;
								DecPageHomeEndoHO864 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO864");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO864);
								DecPageHomeEndoHO864.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO864.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO864.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO864.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO864"));
									DecPageHomeEndoHO864.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO864"));
								}
								DecPageHomeEndoHO864.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO864EXTN"));
								DecPageHomeEndoHO864.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO864EXTN"));

								XmlElement EndoElementHO864;
								EndoElementHO864= AcordPDFXML.CreateElement("ENDOELEMENTHO864INFO");
								DecPageHomeEndoHO864.AppendChild(EndoElementHO864);
								EndoElementHO864.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO864.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;


						case	"IFGHO"	:

							break;

						case	"WP865"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoWP865;
								DecPageHomeEndoWP865 = AcordPDFXML.CreateElement("HOMEENDORSEMENTWP865");
								DecPageRootElement.AppendChild(DecPageHomeEndoWP865);
								DecPageHomeEndoWP865.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoWP865.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoWP865.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoWP865.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEWP865"));
									DecPageHomeEndoWP865.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP865"));
								}
								DecPageHomeEndoWP865.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWP865EXTN"));
								DecPageHomeEndoWP865.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP865EXTN"));

								XmlElement EndoElementWP865;
								EndoElementWP865= AcordPDFXML.CreateElement("ENDOELEMENTWP865INFO");
								DecPageHomeEndoWP865.AppendChild(EndoElementWP865);
								EndoElementWP865.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementWP865.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;

						default:
							if (gStrPdfFor == PDFForDecPage)
							{
								if(prncovCode=="EMO42" || prncovCode=="EOP42")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{												
									XmlElement DecPageHomeEndoEMO42;
									DecPageHomeEndoEMO42 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMO42");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMO42);
									DecPageHomeEndoEMO42.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMO42.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMO42.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMO42.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMO42"));
										DecPageHomeEndoEMO42.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO42"));
									}
									DecPageHomeEndoEMO42.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMO42EXTN"));
									DecPageHomeEndoEMO42.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO42EXTN"));

									XmlElement EndoElementEMO42;
									EndoElementEMO42= AcordPDFXML.CreateElement("ENDOELEMENTEMO42INFO");
									DecPageHomeEndoEMO42.AppendChild(EndoElementEMO42);
									EndoElementEMO42.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMO42.SetAttribute(id,"0");
								}
									#endregion
								}
								if(prncovCode=="EMO65" || prncovCode=="EOP65")
								{
									#region Dec Page Element
									if(gCtrlnChkHO65!=true)	
									{											
										XmlElement DecPageHomeEndoEMO65;
										DecPageHomeEndoEMO65 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMO65");
										DecPageRootElement.AppendChild(DecPageHomeEndoEMO65);
										DecPageHomeEndoEMO65.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageHomeEndoEMO65.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageHomeEndoEMO65.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageHomeEndoEMO65.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMO65"));
											DecPageHomeEndoEMO65.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO65"));
										}
										DecPageHomeEndoEMO65.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMO65EXTN"));
										DecPageHomeEndoEMO65.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO65EXTN"));

										XmlElement EndoElementEMO65;
										EndoElementEMO65= AcordPDFXML.CreateElement("ENDOELEMENTEMO65INFO");
										DecPageHomeEndoEMO65.AppendChild(EndoElementEMO65);
										EndoElementEMO65.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementEMO65.SetAttribute(id,"0");
										gCtrlnChkHO65=true;
									}				
									#endregion
								}
								if(prncovCode=="EMO70" || prncovCode=="EOP70")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMO70;
									DecPageHomeEndoEMO70 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMO70");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMO70);
									DecPageHomeEndoEMO70.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMO70.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMO70.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMO70.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMO70"));
										DecPageHomeEndoEMO70.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO70"));
									}
									DecPageHomeEndoEMO70.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMO70EXTN"));
									DecPageHomeEndoEMO70.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO70EXTN"));

									XmlElement EndoElementEMO70;
									EndoElementEMO70= AcordPDFXML.CreateElement("ENDOELEMENTEMO70INFO");
									DecPageHomeEndoEMO70.AppendChild(EndoElementEMO70);
									EndoElementEMO70.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMO70.SetAttribute(id,"0");
								}
									#endregion

								}
								if(prncovCode=="EMO71" || prncovCode=="EOP71")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMO71;
									DecPageHomeEndoEMO71 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMO71");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMO71);
									DecPageHomeEndoEMO71.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMO71.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMO71.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMO71.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMO71"));
										DecPageHomeEndoEMO71.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO71"));
									}
									DecPageHomeEndoEMO71.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMO71EXTN"));
									DecPageHomeEndoEMO71.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO71EXTN"));

									XmlElement EndoElementEMO71;
									EndoElementEMO71= AcordPDFXML.CreateElement("ENDOELEMENTEMO71INFO");
									DecPageHomeEndoEMO71.AppendChild(EndoElementEMO71);
									EndoElementEMO71.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMO71.SetAttribute(id,"0");
								}
									#endregion

								}
								if(prncovCode=="EMR211" || prncovCode=="EOP211")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMR211;
									DecPageHomeEndoEMR211 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMR211");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMR211);
									DecPageHomeEndoEMR211.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMR211.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMR211.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMR211.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMR211"));
										DecPageHomeEndoEMR211.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR211"));
									}
									DecPageHomeEndoEMR211.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMR211EXTN"));
									DecPageHomeEndoEMR211.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR211EXTN"));

									XmlElement EndoElementEMR211;
									EndoElementEMR211= AcordPDFXML.CreateElement("ENDOELEMENTEMR211INFO");
									DecPageHomeEndoEMR211.AppendChild(EndoElementEMR211);
									EndoElementEMR211.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMR211.SetAttribute(id,"0");
								}
									#endregion
								}
								if(prncovCode=="EMR310")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMR310;
									DecPageHomeEndoEMR310 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMR310");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMR310);
									DecPageHomeEndoEMR310.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMR310.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMR310.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMR310.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMR310"));
										DecPageHomeEndoEMR310.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR310"));
									}
									DecPageHomeEndoEMR310.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMR310EXTN"));
									DecPageHomeEndoEMR310.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR310EXTN"));

									XmlElement EndoElementEMR310;
									EndoElementEMR310= AcordPDFXML.CreateElement("ENDOELEMENTEMR310INFO");
									DecPageHomeEndoEMR310.AppendChild(EndoElementEMR310);
									EndoElementEMR310.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMR310.SetAttribute(id,"0");
								}
									#endregion
											
								}

								if(prncovCode=="EMO315" || prncovCode=="EOP315")
								{
									
								}
								if(prncovCode=="EMR320")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMR320;
									DecPageHomeEndoEMR320 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMR320");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMR320);
									DecPageHomeEndoEMR320.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMR320.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMR320.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMR320.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMR320"));
										DecPageHomeEndoEMR320.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR320"));
									}
									DecPageHomeEndoEMR320.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMR320EXTN"));
									DecPageHomeEndoEMR320.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR320EXTN"));

									XmlElement EndoElementEMR320;
									EndoElementEMR320= AcordPDFXML.CreateElement("ENDOELEMENTEMR320INFO");
									DecPageHomeEndoEMR320.AppendChild(EndoElementEMR320);
									EndoElementEMR320.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMR320.SetAttribute(id,"0");
								}
									#endregion
											
								}
								if(prncovCode=="HO6D")
								{
									
								}
								if(prncovCode=="HO4D")
								{
									
								}
							}
																		
							break;
					}
					#endregion
					counter++;
				}
			}

		}

		#endregion Endorsement Wordings

		#region Addition Wordings
		private void createAddWordingsXML()
		{
			string lob_id="1";
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
					DSAddWordSet = getAddWordingDataSet(state_id,lob_id,gStrProcessID);
					if (DSAddWordSet.Tables[0].Rows.Count > 0)
					{
						XmlElement DecAddWordingsElement;
						DecAddWordingsElement = AcordPDFXML.CreateElement("ADDWORDINGS");

						DecPageRootElement.AppendChild(DecAddWordingsElement);
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