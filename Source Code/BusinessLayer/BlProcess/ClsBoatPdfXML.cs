using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;
using Cms.BusinessLayer.BlApplication;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Deepak Gupta</CreatedBy>
	/// <Dated>19-June-2006</Dated>
	/// <Purpose>To Create XML for Acord82 PDF for Boat LOB</Purpose>
	/// </summary>
	public class ClsBoatPdfXML : ClsCommonPdfXML
	{
		#region Declarations
		private XmlElement DecPageRootElement;
		private XmlElement Acord82RootElement;
		private XmlElement SupplementalRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private Hashtable htpremium_dis=new Hashtable(); 
		private Hashtable htpremium_sur=new Hashtable(); 
		double sumtotal=0;
		double dblSumTotal=0;
		double dblOldSum=0;
		string expiry_date = "";
		string eff_date = "";
		string lstrGetPremium="0";
		string goldVewrsionId="0";
		//string AccordXml="";
		string strInsuScore="";
		string newInsuScr="";
		string oldInsuScr="";
		string NamedInsuredWithSuffix="";
		int lintGetindex=0;
		private DataWrapper gobjWrapper;
		private string stCode="";
		private string ApplicantName1 = "";
		private string []prnOrd_covCode;
		private string []prnOrd_attFile;
		private int []prnOrd;
		private int schEquip = 0, noBoats=0, intschEquip=0;
		private int UnattachedInclude=1500;
		int intPrivacyPage = 0;
		
		
		#endregion

		#region Constructor
		public ClsBoatPdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string stateCode, string strProcessID, string Agn_Ins,string temp,DataWrapper lobjWrapper)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			stCode=stateCode;
			gStrProcessID = strProcessID;
			gStrCopyTo = Agn_Ins;
			gStrtemp = temp;
			if(Agn_Ins != null && Agn_Ins != "")
			{
				string []copyTo = Agn_Ins.Split('-');
				if(copyTo[0] == "CUSTOMER")
					CopyTo = "CUSTOMER'S COPY";
				else if(copyTo[0] == "AGENCY")
					CopyTo = "AGENT'S COPY";
				//CopyTo= "Copy To: " + copyTo[0];
			}
			
			DSTempDataSet = new DataSet();
			//gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			this.gobjWrapper = lobjWrapper;

			gobjWrapper.ClearParameteres();
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempDataSet.Tables[0].Rows.Count>0)
			{
				SetPDFVersionLobNode("BOAT",DateTime.Parse(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				SetPDFInsScoresLobNode("BOAT",DateTime.Parse(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
			}
		}
		#endregion

		public string getBoatAcordPDFXml()
		{
			try
			{
				AcordPDFXML = new XmlDocument();
				AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
				if(gStrCopyTo != "AGENCY")
				{
					creatmaillingpage();
				}
				createRootElementForAllRootPDFs();
				FillMonth();
				LoadRateXML("BOAT");
							
				//Creating Xml For Policy
				CreatePolicyAgencyXml();
				CreateCoApplicantXml();
				CreatePriorPolicyXml();			
			
				createBoatXML();
			
				createAcord82BoatAddlIntXml();
				createSchEquipmentsXml();
				createUnderwritingGeneralXML();			
				createAttachmentXML();
				createAcord82OperatorXML();
				createAcord82OperatorExpViolationXML();
			
			
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
				{
					createEndorsementWordings();
				}

				if(gStrCopyTo == "CUSTOMER" || gStrCopyTo == "CUSTOMER-NOWORD")
				{
					createAddWordingsXML();
				}
				//creatmaillingpage();
					string customerFullxml="";
					customerFullxml=AcordPDFXML.OuterXml;
					InsertCustomerFullWordingXml(gStrClientID,gStrPolicyId,gStrPolicyVersion,gStrCopyTo,customerFullxml);
				
				return AcordPDFXML.OuterXml;
			}
			catch(Exception ex)
			{
				throw(new Exception("Error while creating Boat XML.",ex));
			}
		}
		#region insret customer full wording xml
		private void InsertCustomerFullWordingXml(string strCustomerId,string strAppId, string strAppVersionId, string strCopyTo, string strcutomerxml)
		{
			try
			{
				//DataWrapper objDataWrapper;
				//objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);	
					
				gobjWrapper.AddParameter("@CUSTOMER_ID",strCustomerId);
				gobjWrapper.AddParameter("@POLICY_ID",strAppId);
				gobjWrapper.AddParameter("@POLICY_VERSION_ID",strAppVersionId);
				gobjWrapper.AddParameter("@CALLED_FOR",strCopyTo);
				gobjWrapper.AddParameter("@CUSTOMER_XML",strcutomerxml);
				gobjWrapper.ExecuteNonQuery("PROC_GETFULLCUSTOMERXML_INFO");
				gobjWrapper.ClearParameteres();
			}
			
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{}

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
				Acord82RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord82RootElement);

				Acord82RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82"));

				SupplementalRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(SupplementalRootElement);

				SupplementalRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENT"));
			}
		}
		#endregion

		#region code for mailling page xml Generation
		private void creatmaillingpage()
		{
			string strsendmessg="";
			string strInsname="";
			string strInsAdd="";
			string strcityzip="";
			XmlElement MaillingRootElementDecPage;
			MaillingRootElementDecPage    = AcordPDFXML.CreateElement("MAILLINGPAGE");
			DataSet DstempAppDocument = new DataSet();

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DstempAppDocument = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//DstempAppDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			strInsname=RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["APPNAME"].ToString()) + " " + RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["SUFFIX"].ToString());
			strInsAdd=RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["APPADDRESS"].ToString());
			strcityzip=RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["APPCITYSTZIP"].ToString());
			DataSet DstempDocument = new DataSet();
			
			if (gStrPdfFor == PDFForDecPage)
			{
				gobjWrapper.AddParameter("@DOCUMENT_CODE","DEC_PAGE");
				DstempDocument = gobjWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage");
				gobjWrapper.ClearParameteres();

				//DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + "DEC_PAGE" + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
				//AcordPDFXML.SelectSingleNode(RootElement)
				
				if(strsendmessg	=="Y")
				{
					AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
					//DecPageRootElement.AppendChild(MaillingRootElementDecPage);
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
					DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MESSAGE " + fieldType + "=\"" + fieldTypeText + "\">" + DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString()+ "</MESSAGE>";
				
					// Blank Document added for duplex printing
//					XmlElement DecBlankElement;
//					DecBlankElement =  AcordPDFXML.CreateElement("BLANKPAGE");					
//					MaillingRootElementDecPage.AppendChild(DecBlankElement);
//					DecBlankElement.SetAttribute(fieldType,fieldTypeMultiple);
//					DecBlankElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("BLANKDOCUMENT"));
//					DecBlankElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("BLANKDOCUMENT"));
//					DecBlankElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("BLANKDOCUMENTEXTN"));
//					DecBlankElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("BLANKDOCUMENTEXTN"));
//
//					XmlElement DecBlankMailElement;
//					DecBlankMailElement =  AcordPDFXML.CreateElement("BARCODE");
//					DecBlankElement.AppendChild(DecBlankMailElement);
//					DecBlankMailElement.SetAttribute(fieldType,fieldTypeSingle);
//					DecBlankMailElement.SetAttribute(id,"0");
//					DecBlankMailElement.InnerXml = DecBlankMailElement.InnerXml +  "<DEMO " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</DEMO>";
//					

					
				}
			}
			else if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.AddParameter("@DOCUMENT_CODE","ACORD");
				DstempDocument = gobjWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage");
				gobjWrapper.ClearParameteres();

				//DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + "ACORD" + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
				
				//Acord82RootElement.AppendChild(MaillingRootElementDecPage);

				if(strsendmessg	=="Y")
				{
					AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
					//Acord82RootElement.AppendChild(MaillingRootElementDecPage);
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

					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<MAILLINGNAME " + fieldType +"=\""+ fieldTypeText +"\">"+strInsname+"</MAILLINGNAME>"; 
					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<MAILLINGADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+strInsAdd+"</MAILLINGADDRESS>"; 
					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<CITYSTATEZIP " + fieldType +"=\""+ fieldTypeText +"\">"+strcityzip+"</CITYSTATEZIP>"; 
					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<MESSAGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString())+"</MESSAGE>"; 
				}
			}

		}
		#endregion
		#region Code for Policy And Agency Info Xml Generation
		private double GetEffectivePremium( double OldGrossPremium,double NewGrossPremium)
		{
			double EffectivePremium = 0;
			int TotalPolicyTime = 365;
			int DaysDiff=0;
			ClsGeneralInformation objGen=new ClsGeneralInformation();
			//			DataSet dsPolicy= objGen.GetPolicyDetails(int.Parse(gStrClientID),0,0,int.Parse(gStrPolicyId),int.Parse(gStrPolicyVersion));
			
			gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
			gobjWrapper.AddParameter("@POLICY_ID",0);
			gobjWrapper.AddParameter("@POLICY_VERSION_ID",0);
			gobjWrapper.AddParameter("@CALLED_FOR",gStrPolicyId);
			gobjWrapper.AddParameter("@CUSTOMER_XML",gStrPolicyVersion);
			DataSet dsPolicy = gobjWrapper.ExecuteDataSet("Proc_GetPolicyDetails");
			gobjWrapper.ClearParameteres();
			
			//DataSet dsPolicy = gobjSqlHelper.ExecuteDataSet("Proc_GetPolicyDetails " + gStrClientID + ",0,0," + gStrPolicyId + "," + gStrPolicyVersion);

			if (dsPolicy.Tables[0].Rows.Count>0)
			{
				TotalPolicyTime= Convert.ToInt32(dsPolicy.Tables[0].Rows[0]["POLICY_DAYS"].ToString());
			}
			Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess objProcess = new
				Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess ();

			Cms.Model.Policy.Process.ClsProcessInfo objInfo = objProcess.GetRunningProcess(int.Parse(gStrClientID), int.Parse(gStrPolicyId));
			if (objInfo!=null)
			{
				if ( objInfo.PROCESS_ID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS)
				{
					//				objProcess.GetPreviousVersionPremiumDetails(PolicyID, objInfo.POLICY_VERSION_ID, customerID
					//					, out dblPremium, out dblFees, out dblMCCAFees);
					//
					//				double TotalPremium = dblPremium + dblFees + dblMCCAFees;

				

					DaysDiff = TimeSpan.FromTicks(objInfo.EXPIRY_DATE.Ticks - objInfo.EFFECTIVE_DATETIME.Ticks).Days;

					//EffectivePremium = ((GrossPremium - TotalPremium) / TotalPolicyTime) * DaysDiff;
					//EffectivePremium = ((NewGrossPremium - OldGrossPremium) / TotalPolicyTime) * DaysDiff;

				}
			}
			else
			{
				if (expiry_date != "" && eff_date !="")
					DaysDiff = TimeSpan.FromTicks(Convert.ToDateTime(expiry_date).Ticks - Convert.ToDateTime(eff_date).Ticks).Days;
				else
					DaysDiff =0;
			}
			EffectivePremium = ((NewGrossPremium - OldGrossPremium) / TotalPolicyTime) * DaysDiff;

			return EffectivePremium;
		}

		private void CalculateEffcPremium(string oldPolicyVersion)
		{
			string newVersion = gStrPolicyVersion;
			double unattach_prem = 0;
			double sumTtlC=0;
			double SchPrem =0;
			// Adjusted premium after endorsement
			try
			{
				if(oldPolicyVersion != "" && oldPolicyVersion != "0")
				{
					
					// if policy proccess is not committed
					if(gStrtemp!="final")
					{
						DataSet DSTempRateDataSet = new DataSet();

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						gobjWrapper.AddParameter("@LOB","BOAT");
						DSTempRateDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML");
						gobjWrapper.ClearParameteres();

						//DSTempRateDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFQuoteRateXML " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "','" + "BOAT" + "'");
						gStrPolicyVersion = oldPolicyVersion;
						// Load quote premium xml
						if(DSTempRateDataSet.Tables[0].Rows.Count>0)
						{
							LoadRateXML("BOAT");
						}
						// fetch unattched premium form split table
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID","1");
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DataSet DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
						gobjWrapper.ClearParameteres();

						//DataSet DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +"1" +  ",'" + gStrCalledFrom + "'");
						// total premium of boat from quote premium xml
						foreach (XmlNode SumTotalNode in GetSumTotalPremium())
						{
							if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
								sumTtlC += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
						}
						// fetch premium for scheduled equipment
						SchPrem = GetPortAccessPrem();
						double schPremAdj=0;
						// Schdeule equipment minimum premium
						//						foreach (XmlNode PremAdjNode in GetMinPremSch())
						//						{
						//							if(getAttributeValue(PremAdjNode,"STEPPREMIUM")!=null && getAttributeValue(PremAdjNode,"STEPPREMIUM").ToString()!="" )
						//							{
						//								schPremAdj = double.Parse(getAttributeValue(PremAdjNode,"STEPPREMIUM").ToString()) ;						
						//								break;
						//							}
						//						}
						SchPrem += Convert.ToInt32(schPremAdj);

						foreach (XmlNode UnattachedNode in GetUnattachedPremium())
						{
							if(getAttributeValue(UnattachedNode,"STEPPREMIUM")!=null && getAttributeValue(UnattachedNode,"STEPPREMIUM").ToString()!="" &&  getAttributeValue(UnattachedNode,"STEPPREMIUM").ToString()!="Included" )
								unattach_prem += double.Parse(getAttributeValue(UnattachedNode,"STEPPREMIUM").ToString()) ;						
						}
						
					}
						//If policy is committed
					else
					{
						DataSet DSTempDatasetBoatDetail = new DataSet();
						DataSet DstrailerPre = new DataSet();
						DataSet DsSchprem = new DataSet();
						gStrPolicyVersion = newVersion;

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						DSTempDatasetBoatDetail = gobjWrapper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium");
						gobjWrapper.ClearParameteres();

						//DSTempDatasetBoatDetail = gobjSqlHelper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion +"");			
						if(DSTempDatasetBoatDetail.Tables[0].Rows[0]["WRITTEN_PREMIUM"].ToString() != null && DSTempDatasetBoatDetail.Tables[0].Rows[0]["WRITTEN_PREMIUM"].ToString() != "")
						{
							dblOldSum=Convert.ToDouble(DSTempDatasetBoatDetail.Tables[0].Rows[0]["WRITTEN_PREMIUM"].ToString());
						}
					}
					if(gStrtemp!="final")
					{
						dblOldSum = sumTtlC + SchPrem + unattach_prem;
					}
					gStrPolicyVersion = newVersion;
					LoadRateXML("BOAT",gobjWrapper);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		private void CreatePolicyAgencyXml()
		{
			if(gStrProcessID != null && gStrProcessID != "")
			{
				DataSet DSAddWordSet = new DataSet();

				gobjWrapper.AddParameter("@STATE_ID","0");
				gobjWrapper.AddParameter("@LOB_ID","0");
				gobjWrapper.AddParameter("@PROCESS_ID",gStrProcessID);
				DSAddWordSet = gobjWrapper.ExecuteDataSet("Proc_GetAddWordingsData");
				gobjWrapper.ClearParameteres();

				//DSAddWordSet = gobjSqlHelper.ExecuteDataSet("Proc_GetAddWordingsData " +  "0," +  "0," + gStrProcessID);
			
				if (DSAddWordSet.Tables.Count > 1 && DSAddWordSet.Tables[1].Rows.Count > 0)
				{
					Reason		=	RemoveJunkXmlCharacters(DSAddWordSet.Tables[1].Rows[0]["PROCESS_DESC"].ToString());
					//if(DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
					//Reason += " / Effective: " + DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
				}
			}
			else
				Reason		=	RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REASON"].ToString());

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			goldVewrsionId=DSTempDataSet.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
			
			if(gStrProcessID =="")
			{
				gStrProcessID = GetPolicyProcess( gStrClientID ,gStrPolicyId ,gStrPolicyVersion , gStrCalledFrom,gobjWrapper);
			}
			if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
			|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString())
			{
				CalculateEffcPremium(DSTempDataSet.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			#region Global Variable Assignment
			PolicyNumber = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString());
			PolicyEffDate = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
			PolicyExpDate = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
			expiry_date = DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString();
			eff_date = DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
			
			//			CopyTo			= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COPY_TO"].ToString());
			
			if(Reason.Trim() != "" && DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
				Reason += " / Effective Date: " + DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
			AgencyName = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString());
			AgencyAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString());
			AgencyCitySTZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString());
			AgencyPhoneNumber = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString());
			AgencyCode = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NUM_AGENCY_CODE"].ToString());
			AgencySubCode = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString());
			AgencyBilling = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString())=="AB"?"Agency Bill":"Direct Bill" ;
			currTerm = int.Parse(DSTempDataSet.Tables[0].Rows[0]["CURRENT_TERM"].ToString());
			if(currTerm > 1)
				applyInsScore = int.Parse(DSTempDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString());
			#endregion

			if (gStrPdfFor==PDFForAcord)
			{
				#region Acord82 Page
				XmlElement AcordPolicyElement;
				AcordPolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord82RootElement.AppendChild(AcordPolicyElement);
				AcordPolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy") ) + "</DATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APPLICANTPOLNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</APPLICANTPOLNUM>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APPLICANTEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</APPLICANTEFFDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APPLICANTEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</APPLICANTEXPDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDEREFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</BINDEREFFDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDEREXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</BINDEREXPDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDERCVGNOTBOUND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BINDERCVGNOTBOUND"].ToString()) + "</BINDERCVGNOTBOUND>";

				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTBILLING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString()) + "</PAYMENTBILLING>";

				if(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString() == "DB" || DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString() == "MB")
				{
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTDIRECTBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTDIRECTBILL"].ToString()) + "</PAYMENTDIRECTBILL>";
					
					string strbillPlan=DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString();
					if(DSTempDataSet.Tables[0].Rows[0]["PLAN_CODE"].ToString() == "FULLPAY")
					{
						strbillPlan="FP";
						AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters("FP") + "</PAYMENTAPPBILL>";
					}
					else if(DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString() != "FP" && DSTempDataSet.Tables[0].Rows[0]["PLAN_CODE"].ToString() != "FULLPAY")
					{
						AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BILL_PLAN " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString()) + "</BILL_PLAN>";
					}
				}
				if(DSTempDataSet.Tables[0].Rows[0]["PLAN_CODE"].ToString() != "FULLPAY")
				{
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()) + "</PAYMENTAPPBILL>";
				}
				//Agency
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				if(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()!="")
				{
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString())+"</AGENCYADDRESS>";
				}
				else
				{
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString())+"</AGENCYADDRESS>";
				}
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCYPHONE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYFAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCYFAX>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NUM_AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";

				string strfloatX = "280";
				string strfloatY = "25";
				string strfloatW = "153";
				string strfloatH = "22";
				string strpageNo = "2";

				AcordPolicyElement.InnerXml += "<SIGNATURE " + fieldType +"=\""+ fieldTypeImage +"\" " + imageType + "=\"" + imageTypeYes + "\" " + floatX + "=\"" + strfloatX + "\" " + floatY + "=\"" + strfloatY + "\" " + floatW + "=\"" + strfloatW  + "\" " + floatH + "=\"" + strfloatH  + "\" " + pageNo + "=\"" + strpageNo  + "\"></SIGNATURE>";
				#endregion

				#region Supplemental Page
				XmlElement SupplementPolicyElement;
				SupplementPolicyElement = AcordPDFXML.CreateElement("POLICY");
				SupplementalRootElement.AppendChild(SupplementPolicyElement);
				SupplementPolicyElement.SetAttribute(fieldType,fieldTypeSingle);

				SupplementPolicyElement.InnerXml = SupplementPolicyElement.InnerXml +  "<APPLICANTIONNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</APPLICANTIONNUMBER>";
				if(DSTempDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString()!="" && DSTempDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString()!="0" && DSTempDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString()!="0.00")
				{
					if(DSTempDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString().IndexOf(".00")<0)
					{
						SupplementPolicyElement.InnerXml = SupplementPolicyElement.InnerXml +  "<AMOUNTENCLOSED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString()) + "</AMOUNTENCLOSED>";
					}
					else
					{
						SupplementPolicyElement.InnerXml = SupplementPolicyElement.InnerXml +  "<AMOUNTENCLOSED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString())+ "</AMOUNTENCLOSED>";
					}
				}
				else
				{
					SupplementPolicyElement.InnerXml = SupplementPolicyElement.InnerXml +  "<AMOUNTENCLOSED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</AMOUNTENCLOSED>";
				}

				#endregion
			}
			/*else if(gStrPdfFor == PDFForDecPage)
			{
				if((Reason != "New Business") && (Reason != "Rewrite"))
				{
					XmlElement DecPagePolicyElement;
					DecPagePolicyElement = AcordPDFXML.CreateElement("WORDINGS");
					DecPageRootElement.AppendChild(DecPagePolicyElement);
					DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<not_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<not_txt1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt1>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<not_txt2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt2>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_add>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<hotline " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</hotline>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<website " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</website>";
				}
			}
			*/

		}
		#endregion

		#region Code for Co-Applicant or Named Insured Info Xml Generation.
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
		private void CreateCoApplicantXml()
		{
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{
				#region Global Variable Assignment
				ApplicantName = RemoveJunkXmlCharacters(NamedInsured(DSTempDataSet));
				NamedInsuredWithSuffix = RemoveJunkXmlCharacters(NamedInsuredWithSuffixs(DSTempDataSet));
				ApplicantName1 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString());
				ApplicantAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString());
				ApplicantCityStZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString());
				
				reason_code1 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE"].ToString());
				reason_code2 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE2"].ToString());
				reason_code3 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE3"].ToString());
				reason_code4 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE4"].ToString());

				CustomerAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString());
				CustomerCityStZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString());
				strInsuScore= DSTempDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString();
				if(currTerm <= 1)
				{

					if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, DSTempDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
						strNeedPage2 = "Y";
					else
						strNeedPage2 = "N";
				}
				else
				{
					if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, applyInsScore.ToString()))
					{
						strNeedPage2 = "Y";
					}
				}
				// If Process is not endoresement,rewrite,reinstatement
				/*if(gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
					&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString()
					&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString()
					)
				{
					if(currTerm <= 1)
					{
						if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString())
						{
							if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, applyInsScore.ToString()))
								strNeedPage2 = "Y";
							else
							{
								if(DSTempDataSet.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString()=="Y")
								{
									strNeedPage2 = "Y";
								}
								else
								{
									strNeedPage2 = "N";
								}
							}
						}
						else
						{
							if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, DSTempDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
								strNeedPage2 = "Y";
							else
								strNeedPage2 = "N";
						}
					}
					else
					{
						if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString())
						{
							if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, applyInsScore.ToString()))
								strNeedPage2 = "Y";
							else
							{
								if(DSTempDataSet.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString()=="Y")
								{
									strNeedPage2 = "Y";
								}
								else
								{
									strNeedPage2 = "N";
								}
							}
						}
						else
						{
							if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, applyInsScore.ToString()))
								strNeedPage2 = "Y";
							else
								strNeedPage2 = "N";
						}
					}
				}
				else
				{
					if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, applyInsScore.ToString()))
					{
						strNeedPage2 = "Y";
					}
					else
					{
						if(DSTempDataSet.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString()=="Y")
							strNeedPage2 = "Y";
						else
							strNeedPage2 = "N";
					}
				}*/
				#endregion

				//				if(gStrPdfFor == PDFForDecPage)
				//				{
				//					#region Dec Page Co-Applicants
				//
				//					XmlElement AddlCoApplicantElement;
				//					AddlCoApplicantElement = AcordPDFXML.CreateElement("ADDITIONALCOAPPLICANTS");
				//					DecPageRootElement.AppendChild(AddlCoApplicantElement);
				//					AddlCoApplicantElement.SetAttribute(fieldType,fieldTypeMultiple);
				//					AddlCoApplicantElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ADDLAPPLICANT"));
				//					AddlCoApplicantElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ADDLAPPLICANT"));
				//					AddlCoApplicantElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ADDLAPPLICANTEXTN"));
				//					AddlCoApplicantElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ADDLAPPLICANTEXTN"));
				//
				//					for(int RowCounter=1;RowCounter<DSTempDataSet.Tables[0].Rows.Count;RowCounter++)
				//					{
				//						XmlElement CoAppElement;
				//						CoAppElement =  AcordPDFXML.CreateElement("COAPPLICANT");
				//						AddlCoApplicantElement.AppendChild(CoAppElement);
				//						CoAppElement.SetAttribute(fieldType,fieldTypeNormal);
				//						CoAppElement.SetAttribute(id,(RowCounter-1).ToString());
				//
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</APPLICANTNAME>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</NAMEINSUREDNAME>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</NAMEINSUREDADDRESS>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</NAMEINSUREDCITYSTATEZIP>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["OCCUPATION"].ToString()) + "</COAPPOCCUPATION>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["APPNAME"].ToString()) + "</COAPPEMPLOYEENAME>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["YEARSEMPL"].ToString()) + "</COAPPYEAREMPL>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["MARTSTATUS"].ToString()) + "</COAPPMARTSTATUS>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["DOB"].ToString()) + "</COAPPDATEOFBIRTH>";
				//						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString()) + "</COAPPSSN>";
				//					}
				//					#endregion
				//
				//				}

				if (gStrPdfFor==PDFForAcord)
				{
					#region Acord 82 Page
					XmlElement Acord82NamedInsuredElement;
					Acord82NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord82RootElement.AppendChild(Acord82NamedInsuredElement);
					Acord82NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTHOMEPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PHONE"].ToString()) + "</APPLICANTHOMEPHONE>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTBUSINESSPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString()) + "</APPLICANTBUSINESSPHONE>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine Mutual/Watercraft") + "</APPLICANTCOPLAN>";

					XmlElement AddlCoApplicantElement;
					if(DSTempDataSet.Tables[0].Rows.Count > 1)
					{
						AddlCoApplicantElement = AcordPDFXML.CreateElement("ADDITIONALCOAPPLICANTS");
						Acord82RootElement.AppendChild(AddlCoApplicantElement);
						AddlCoApplicantElement.SetAttribute(fieldType,fieldTypeMultiple);
						AddlCoApplicantElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ADDLAPPLICANT"));
						AddlCoApplicantElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ADDLAPPLICANT"));
						AddlCoApplicantElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ADDLAPPLICANTEXTN"));
						AddlCoApplicantElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ADDLAPPLICANTEXTN"));

						for(int RowCounter=1;RowCounter<DSTempDataSet.Tables[0].Rows.Count;RowCounter++)
						{
							XmlElement CoAppElement;
							CoAppElement =  AcordPDFXML.CreateElement("COAPPLICANT");
							AddlCoApplicantElement.AppendChild(CoAppElement);
							CoAppElement.SetAttribute(fieldType,fieldTypeNormal);
							CoAppElement.SetAttribute(id,(RowCounter-1).ToString());

							string strOprSSn="";
							//if(DSTempDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString()=="")
							strOprSSn =  DSTempDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString().ToString();
							if(strOprSSn !="" && strOprSSn !="0")
							{
								strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(DSTempDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString().ToString());
								
								if(strOprSSn.Trim() != "")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
								{
									string strvaln = "xxx-xx-";
									strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
									strOprSSn = strvaln;
								}
								else
									strOprSSn="";
							}

							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</NAMEINSUREDNAME>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</NAMEINSUREDADDRESS>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</NAMEINSUREDCITYSTATEZIP>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["OCCUPATION"].ToString()) + "</COAPPOCCUPATION>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["APPNAME"].ToString()) + "</COAPPEMPLOYEENAME>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["YEARSEMPL"].ToString()) + "</COAPPYEAREMPL>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["MARTSTATUS"].ToString()) + "</COAPPMARTSTATUS>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["DOB"].ToString()) + "</COAPPDATEOFBIRTH>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strOprSSn) + "</COAPPSSN>";
						}
					}
					#endregion

					#region Supplemental Page
					XmlElement SupplementalNamedInsuredElement;
					SupplementalNamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					SupplementalRootElement.AppendChild(SupplementalNamedInsuredElement);
					SupplementalNamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					SupplementalNamedInsuredElement.InnerXml = SupplementalNamedInsuredElement.InnerXml +  "<APPLICANTIONNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</APPLICANTIONNUMBER>";
					SupplementalNamedInsuredElement.InnerXml = SupplementalNamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					#endregion
				}
			}
		}
		#endregion

		#region Code for Scheduled Equipments XML Generation
		private void createSchEquipmentsXml()
		{

			int SchPrem=0;
			double sumTtlC=0;
			DataSet DsSchprem = new DataSet();

			gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
			gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
			gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DsSchprem = gobjWrapper.ExecuteDataSet("PROC_GetScheduleEquip_Premium");
			gobjWrapper.ClearParameteres();

			//DsSchprem =  gobjSqlHelper.ExecuteDataSet("PROC_GetScheduleEquip_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			// total Premium 
			//declare dataset for trailer premium			
			DataSet Dstrailpre = new DataSet();	

			
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			// fetch total policy premium
			foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
			{
			
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempEngineTrailer1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempEngineTrailer1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				
				gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
				gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
				gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOAT_ID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				Dstrailpre = gobjWrapper.ExecuteDataSet("PROC_GetTrailer_Premium");
				gobjWrapper.ClearParameteres();

				//Dstrailpre = gobjSqlHelper.ExecuteDataSet("PROC_GetTrailer_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				// If Proccess is not committed then fetch premiums from premium xml
				// if proccess is committed then fetch premiums from split table
				if(gStrtemp!="final")
				{
				}
				else
				{
					// premium from split table 
					
					SchPrem=Convert.ToInt32(GetTrailerPremium(DsSchprem,"COVERAGE_PREMIUM"));
					sumTtlC += GetPremiumSch(DSTempEngineTrailer1,"B","COVERAGE_PREMIUM");
					sumTtlC += GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
					sumTtlC +=Convert.ToInt32(GetPremiumUnattached(DSTempEngineTrailer1,"UAE","COVERAGE_PREMIUM"));
				}
			}
			/////
			
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
						
			
			if (DSTempDataSet.Tables[0].Rows.Count > 0)
			{
				XmlElement DecSchEquipments;
				DecSchEquipments = AcordPDFXML.CreateElement("SCHEQUIPMENTS");
				
				XmlElement SupplementSchEquipments;
				SupplementSchEquipments = AcordPDFXML.CreateElement("SCHEQUIPMENTS");

				if (gStrPdfFor == PDFForDecPage)
				{
					#region Adding Dec Page Multiple Group Node
					DecPageRootElement.AppendChild(DecSchEquipments);
					DecSchEquipments.SetAttribute(fieldType,fieldTypeMultiple);
					DecSchEquipments.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESCHEQUP"));
					DecSchEquipments.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESCHEQUP"));
					DecSchEquipments.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESCHEQUPEXTN"));
					DecSchEquipments.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESCHEQUPEXTN"));
					#endregion
				}
				else if (gStrPdfFor == PDFForAcord)
				{
					#region Adding Supplement Page Multiple Group Node
					SupplementalRootElement.AppendChild(SupplementSchEquipments);
					SupplementSchEquipments.SetAttribute(fieldType,fieldTypeMultiple);
					SupplementSchEquipments.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTSCHEQUP"));
					SupplementSchEquipments.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSCHEQUP"));
					SupplementSchEquipments.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTSCHEQUPEXTN"));
					SupplementSchEquipments.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSCHEQUPEXTN"));
					#endregion
				}

				int RowCounter = 0;
				double EquipLimit=0;
				double EquipDeductible=0;
				double SchtotPre=0;
				string[] arrDropdown; 

				int i=0;

				arrDropdown=new string[GetSchPremium().Count];
  
				double schPremAdj=0;
				//				foreach (XmlNode PremAdjNode in GetMinPremSch())
				//				{
				//					if(getAttributeValue(PremAdjNode,"STEPPREMIUM")!=null && getAttributeValue(PremAdjNode,"STEPPREMIUM").ToString()!="" )
				//					{
				//						schPremAdj = double.Parse(getAttributeValue(PremAdjNode,"STEPPREMIUM").ToString()) ;						
				//						break;
				//					}
				//				}

				foreach (XmlNode SchPremiumNode in GetSchPremium())
				{
					arrDropdown[i]=getAttributeValue(SchPremiumNode,"STEPPREMIUM").ToString();
					if(i == 0)
					{
						try
						{
							double Actualprem = double.Parse(arrDropdown[i]);

							Actualprem += schPremAdj;
							arrDropdown[i] = Actualprem.ToString();
						}
						catch//(Exception ex)
						{}
					}
					i++;
				}

				foreach (DataRow Row in DSTempDataSet.Tables[0].Rows)
				{
					if (gStrPdfFor == PDFForDecPage)
					{
						#region Dec Page
						XmlElement DecEquipment;
						DecEquipment =  AcordPDFXML.CreateElement("EQUIPMENT");
						DecSchEquipments.AppendChild(DecEquipment);
						DecEquipment.SetAttribute(fieldType,fieldTypeNormal);
						DecEquipment.SetAttribute(id,(RowCounter).ToString());

						DecEquipment.InnerXml = DecEquipment.InnerXml +  "<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + NamedInsuredWithSuffix + "</INSUREDNAME>";
						if(gStrCalledFrom.Equals(CalledFromPolicy))
						{					
							DecEquipment.InnerXml = DecEquipment.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
							DecEquipment.InnerXml = DecEquipment.InnerXml +  "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
							DecEquipment.InnerXml = DecEquipment.InnerXml +  "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
							DecEquipment.InnerXml = DecEquipment.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
						}
						string pageOf = (noBoats + intschEquip).ToString() + " of " + (noBoats + intschEquip).ToString();
						DecEquipment.InnerXml = DecEquipment.InnerXml +  "<PAGE " + fieldType + "=\"" + fieldTypeText + "\">" + pageOf + "</PAGE>";

						DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EQUIP_NO"].ToString()) + "</SCHEQUPNUMBER>";
						
//						if(Row["OTHER_DESCRIPTION"].ToString()!="" && Row["OTHER_DESCRIPTION"].ToString()!=null)
//						{
							DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EQUIPDESC"].ToString()) + "</SCHEQUPDESC>";
//						}
						DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPSERIALNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SERIAL_NO"].ToString()) + "</SCHEQUPSERIALNUM>";
						DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPELECTRONIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EQUIPMENT_TYPE"].ToString()) + "</SCHEQUPELECTRONIC>";
						DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPLIMIT " + fieldType + "=\"" + fieldTypeText + "\">"+ RemoveJunkXmlCharacters(Row["INSURED_VALUE"].ToString().Replace(".00","")) + "</SCHEQUPLIMIT>";
						if(Row["EQUIP_AMOUNT"].ToString().Trim() != "")
							DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPDED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EQUIP_AMOUNT"].ToString().Replace(".00","")) + "</SCHEQUPDED>";
						else
							DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPDED " + fieldType + "=\"" + fieldTypeText + "\">" + "None" + "</SCHEQUPDED>";

						//Need To Update This Premium Field as this area is under development at wolverine team's end.
						//nbisht
						if(gStrtemp!="final")
						{
							if(arrDropdown.Length>0)
							{
								lstrGetPremium = arrDropdown[RowCounter];
								lintGetindex= lstrGetPremium.IndexOf(".");
								if(lintGetindex == -1)
								{
									DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPPREM " + fieldType + "=\"" + fieldTypeText + "\">" + ("$" + arrDropdown[RowCounter].ToString() + ".00")  + "</SCHEQUPPREM>";
								}
								else
								{
									DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPPREM " + fieldType + "=\"" + fieldTypeText + "\">" + ("$" + arrDropdown[RowCounter].ToString())  + "</SCHEQUPPREM>";
								}
								if(RowCounter == 0)
									DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPPREMTOT "  + fieldType + "=\"" + fieldTypeText + "\">" + (GetPortAccessPrem() + Convert.ToInt32(schPremAdj)).ToString() + ".00" + "</SCHEQUPPREMTOT>";
					
							}
						}
						else 
						{
							// fetch premium for each trialer
							// if process committed
							foreach(DataRow SchRow in DsSchprem.Tables[0].Rows)
							{
								if (SchRow["EQUIP_NO"].ToString() == Row["EQUIP_NO"].ToString())
								{
									DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPPREM " + fieldType + "=\"" + fieldTypeText + "\">" + ("$" + SchRow["COVERAGE_PREMIUM"].ToString())+".00" + "</SCHEQUPPREM>";
								}
								SchtotPre+=Convert.ToDouble(SchRow["COVERAGE_PREMIUM"].ToString());								
							}
							DecEquipment.InnerXml = DecEquipment.InnerXml +  "<SCHEQUPPREMTOT "  + fieldType + "=\"" + fieldTypeText + "\">" + SchPrem + ".00" + "</SCHEQUPPREMTOT>";
							
					
						}
						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Supplemental Page
						XmlElement SupplementEquipment;
						SupplementEquipment =  AcordPDFXML.CreateElement("EQUIPMENT");
						SupplementSchEquipments.AppendChild(SupplementEquipment);
						SupplementEquipment.SetAttribute(fieldType,fieldTypeNormal);
						SupplementEquipment.SetAttribute(id,(RowCounter).ToString());

						SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<EQUIPMENTNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EQUIP_NO"].ToString()) + "</EQUIPMENTNUMBER>";
						SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<ITEMNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EQUIPDESC"].ToString()) + "</ITEMNAME>";
						SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<MANUFACTURER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MAKE"].ToString()) + "</MANUFACTURER>";
						SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<SERIALNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SERIAL_NO"].ToString()) + "</SERIALNUMBER>";
						SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<ELECTRONIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EQUIPMENT_TYPE"].ToString()) + "</ELECTRONIC>";
						SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<AMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["INSURED_VALUE"].ToString().Replace(".00","")) + "</AMOUNT>";
						SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<DEDUCTIBLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EQUIP_AMOUNT"].ToString().Replace(".00","")) + "</DEDUCTIBLE>";
						//Need To Update This Premium Field as this area is under development at wolverine team's end.
						if(gStrtemp!="final")
						{
							if(arrDropdown.Length>0)
							{
								if(RowCounter<arrDropdown.Length)
								{
									lstrGetPremium = arrDropdown[RowCounter].ToString();
									lintGetindex = lstrGetPremium.IndexOf(".");
									if(lintGetindex == -1)
									{
										SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ("$" + arrDropdown[RowCounter].ToString()    + ".00") + "</PREMIUM>";
									}
									else
										SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ("$" + arrDropdown[RowCounter].ToString()) + "</PREMIUM>";

									sumtotal+=int.Parse(arrDropdown[RowCounter].ToString() ); 
								}
							}
						}
						else
						{
							foreach(DataRow SchRow in DsSchprem.Tables[0].Rows)
							{
								if (SchRow["EQUIP_NO"].ToString() == Row["EQUIP_NO"].ToString())
								{
									SupplementEquipment.InnerXml = SupplementEquipment.InnerXml +  "<PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ("$" + SchRow["COVERAGE_PREMIUM"].ToString())+".00" + "</PREMIUM>";
								}
							}
						}
					
						if(Row["INSURED_VALUE"].ToString()!="")
							EquipLimit = EquipLimit + double.Parse(Row["INSURED_VALUE"].ToString().Replace("$","").Replace(",",""));

						if(Row["EQUIP_AMOUNT"].ToString()!="")
							EquipDeductible = EquipDeductible + double.Parse(Row["EQUIP_AMOUNT"].ToString().Replace("$","").Replace(",",""));
						#endregion
					}

					RowCounter++ ;
				}

				if (gStrPdfFor == PDFForAcord)
				{
					#region Supplemental Page Sch Equipments Total Limit Ded Premium Code.
					XmlElement SupplementSchEqupTotals;
					SupplementSchEqupTotals = AcordPDFXML.CreateElement("SCHEQUIPSTOTAL");
					SupplementalRootElement.AppendChild(SupplementSchEqupTotals);
					SupplementSchEqupTotals.SetAttribute(fieldType,fieldTypeSingle);

					SupplementSchEqupTotals.InnerXml = SupplementSchEqupTotals.InnerXml +  "<TOTALDEDUCTIBLE " + fieldType + "=\"" + fieldTypeText + "\">" + ""+ "</TOTALDEDUCTIBLE>";
					SupplementSchEqupTotals.InnerXml = SupplementSchEqupTotals.InnerXml +  "<TOTALAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("$" + EquipLimit.ToString()) + "</TOTALAMOUNT>";
					//Premium Total is rest.

					lstrGetPremium = sumtotal.ToString();
					lintGetindex   = lstrGetPremium.IndexOf(".");
					if(gStrtemp!="final")
					{
						if(lintGetindex ==-1)
						{
							SupplementSchEqupTotals.InnerXml = SupplementSchEqupTotals.InnerXml +  "<TOTALPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ("$" + sumtotal.ToString() + ".00")+ "</TOTALPREMIUM>";
						}
						else
							SupplementSchEqupTotals.InnerXml = SupplementSchEqupTotals.InnerXml +  "<TOTALPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ("$" + sumtotal.ToString())+ "</TOTALPREMIUM>";
					}
					else
					{
						SupplementSchEqupTotals.InnerXml = SupplementSchEqupTotals.InnerXml +  "<TOTALPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ("$" + SchPrem)+".00"+ "</TOTALPREMIUM>";
					}
					
					#endregion
				}
			}
		}
		#endregion

		#region Code for Underwriting And General Info Xml Generation
		private void createUnderwritingGeneralXML()
		{
			double sumTtlC=0,unattach_prem=0;
			int SchPrem=0;
			DataSet Dstrailpre = new DataSet();	
			DataSet DsSchprem = new DataSet();

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			
			gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
			gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
			gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DsSchprem = gobjWrapper.ExecuteDataSet("PROC_GetScheduleEquip_Premium");
			gobjWrapper.ClearParameteres();

			//DsSchprem =  gobjSqlHelper.ExecuteDataSet("PROC_GetScheduleEquip_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			// fetch total policy premium
			foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
			{
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempEngineTrailer1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
				gobjWrapper.ClearParameteres();
			
				//DataSet DSTempEngineTrailer1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
			
				gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
				gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
				gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOAT_ID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				Dstrailpre = gobjWrapper.ExecuteDataSet("PROC_GetTrailer_Premium");
				gobjWrapper.ClearParameteres();

				//Dstrailpre = gobjSqlHelper.ExecuteDataSet("PROC_GetTrailer_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				// If Proccess is not committed then fetch premiums from premium xml
				// if proccess is committed then fetch premiums from split table
				if(gStrtemp!="final")
				{					
				}
				else
				{
					// premium from split table 
					
					SchPrem=Convert.ToInt32(GetTrailerPremium(DsSchprem,"COVERAGE_PREMIUM"));
					sumTtlC += GetPremiumSch(DSTempEngineTrailer1,"B","COVERAGE_PREMIUM");
					sumTtlC += GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
					sumTtlC +=Convert.ToInt32(GetPremiumUnattached(DSTempEngineTrailer1,"UAE","COVERAGE_PREMIUM"));
				}
			}
			if(gStrtemp!="final")
			{
				foreach (XmlNode SumTotalNode in GetSumTotalPremium())
				{
					if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
						sumTtlC += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
				}
				//	SchPrem = GetPortAccessPrem();
				// premium for unattached equipment
				foreach (XmlNode PremiumNode in GetUnattachedPremium("1"))
				{
					if(getAttributeValue(PremiumNode,"COMPONENT_CODE")=="BOAT_UNATTACH_PREMIUM" && getAttributeValue(PremiumNode,"STEPPREMIUM")!="" && getAttributeValue(PremiumNode,"STEPPREMIUM")!="Included")
					{
						unattach_prem=Convert.ToDouble(getAttributeValue(PremiumNode,"STEPPREMIUM"));
					}
				}
			}
			if (gStrPdfFor == PDFForAcord)
			{
				XmlElement Accord82BoatRemark;
				Accord82BoatRemark = AcordPDFXML.CreateElement("BOATREMARK");

	
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFUnderwritingDetails");
				gobjWrapper.ClearParameteres();

				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFUnderwritingDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
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
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOTHINSUWITHCOMP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED"].ToString().Trim()) + "</GENINFOOTHINSUWITHCOMP>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOANYLOSS3YRS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_LOSS_THREE_YEARS"].ToString()) + "</GENINFOANYLOSS3YRS>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOCOVGDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED"].ToString()) + "</GENINFOCOVGDECLINED>";

					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<PREVIOUSADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Previous Address: " + DSTempDataSet.Tables[0].Rows[0]["YEARS_AT_PREV_ADD"].ToString()) + "</PREVIOUSADDRESS>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<DRIVERIMPAIRMENT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Driver Impairment Description: " + DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED_DESC"].ToString()) + "</DRIVERIMPAIRMENT>";
					
					#endregion

					if(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_DESC"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT_DESC"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["ANY_LOSS_THREE_YEARS_DESC"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED_DESC"].ToString() != "")
					{
						#region Accord 82 Boat Remark Element
						Acord82RootElement.AppendChild(Accord82BoatRemark);
						Accord82BoatRemark.SetAttribute(fieldType,fieldTypeMultiple);
						Accord82BoatRemark.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82BOATREMARKS"));
						Accord82BoatRemark.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOATREMARKS"));
						Accord82BoatRemark.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82BOATREMARKSEXTN"));
						Accord82BoatRemark.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOATREMARKSEXTN"));
						#endregion

						#region Acord 82 Sheet Remarks
						XmlElement ACORD82REMARKSINFO;
						ACORD82REMARKSINFO = AcordPDFXML.CreateElement("ACORD82REMARKSINFO");
						Accord82BoatRemark.AppendChild(ACORD82REMARKSINFO);				
						ACORD82REMARKSINFO.SetAttribute(fieldType,fieldTypeNormal);
						ACORD82REMARKSINFO.SetAttribute(id,"0");
				
					
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<LICENSEDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Driver License Description: " + DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_DESC"].ToString()) + "</LICENSEDESC>";
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<VIOLATIONDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Violation Description: " + DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT_DESC"].ToString()) + "</VIOLATIONDESC>";
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<POLICYDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Multipolicy Description: " + DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"].ToString()) + "</POLICYDESC>";
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<LOSSDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Loss Description: " + DSTempDataSet.Tables[0].Rows[0]["ANY_LOSS_THREE_YEARS_DESC"].ToString()) + "</LOSSDESC>";
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<COVERAGEDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Coverage Declination Description: " + DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED_DESC"].ToString()) + "</COVERAGEDECLINED>";					
					
						#endregion
					}
					#region Supplemental Page underwriting info
					XmlElement SupplementUnderwritingElement;
					SupplementUnderwritingElement = AcordPDFXML.CreateElement("UNDERWRITINGINFO");
					SupplementalRootElement.AppendChild(SupplementUnderwritingElement);
					SupplementUnderwritingElement.SetAttribute(fieldType,fieldTypeSingle);
					if(!DSTempDataSet.Tables[0].Rows[0]["PARTICIPATE_RACE"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PARTICIPATE " + fieldType + "=\"" + fieldTypeText + "\">" + "Yes" + "</PARTICIPATE>";
					else 
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PARTICIPATE " + fieldType + "=\"" + fieldTypeText + "\">" + "No" + "</PARTICIPATE>";
					if(!DSTempDataSet.Tables[0].Rows[0]["CARRY_PASSENGER_FOR_CHARGE"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PASSENGERS " + fieldType + "=\"" + fieldTypeText + "\">" + "Yes" + "</PASSENGERS>";
					else
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PASSENGERS " + fieldType + "=\"" + fieldTypeText + "\">" + "No" + "</PASSENGERS>";
					if(!DSTempDataSet.Tables[0].Rows[0]["IS_RENTED_OTHERS"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<RENTED " + fieldType + "=\"" + fieldTypeText + "\">" +"Yes" + "</RENTED>";
					else
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<RENTED " + fieldType + "=\"" + fieldTypeText + "\">" +"No" + "</RENTED>";
					//Watercraft used other than WI/MI/IN states is not done yet as it is not implemented in Wolverine CMS.
					if(!DSTempDataSet.Tables[0].Rows[0]["IS_BOAT_USED_IN_ANY_WATER"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<OTHERUSE " + fieldType + "=\"" + fieldTypeText + "\">" + "Yes" + "</OTHERUSE>";
					else
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<OTHERUSE " + fieldType + "=\"" + fieldTypeText + "\">" + "No" + "</OTHERUSE>";

					SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REGISTEREDPLACE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["STATE_REG"].ToString()) + "</REGISTEREDPLACE>";
					if(!DSTempDataSet.Tables[0].Rows[0]["IS_REGISTERED_OTHERS"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<OTHERREGISTERED " + fieldType + "=\"" + fieldTypeText + "\">" +"Yes" + "</OTHERREGISTERED>";
					else 
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<OTHERREGISTERED " + fieldType + "=\"" + fieldTypeText + "\">" +"No" + "</OTHERREGISTERED>";
					if(!DSTempDataSet.Tables[0].Rows[0]["DRINK_DRUG_VOILATION"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<DRINKING " + fieldType + "=\"" + fieldTypeText + "\">" +"Yes" + "</DRINKING>";
					else 
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<DRINKING " + fieldType + "=\"" + fieldTypeText + "\">" +"No" + "</DRINKING>";
					if(!DSTempDataSet.Tables[0].Rows[0]["IS_BOAT_COOWNED"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<COOWNED " + fieldType + "=\"" + fieldTypeText + "\">" + "Yes" + "</COOWNED>";
					else 
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<COOWNED " + fieldType + "=\"" + fieldTypeText + "\">" + "No" + "</COOWNED>";
					SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PRIORINSURER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PRIOR_INSURANCE"].ToString()) + "</PRIORINSURER>";

					if (gStrPdfFor == PDFForAcord)
					{
						if(gStrtemp!="final")
						{
							if(htpremium.Contains("SUMTOTAL"))
							{
								SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<POLICYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ((sumTtlC + sumtotal + unattach_prem) + ".00")+ "</POLICYPREMIUM>";
							}
						}
						else
						{
							SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<POLICYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ((sumTtlC + SchPrem) + ".00")+ "</POLICYPREMIUM>";
						}
						
					}

					//All Remarks Go here.
					if(DSTempDataSet.Tables[0].Rows[0]["PARTICIPATE_RACE_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKPARTICIPATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#1" + DSTempDataSet.Tables[0].Rows[0]["PARTICIPATE_RACE_DESC"].ToString()) + "</REMARKPARTICIPATE>";
					if(DSTempDataSet.Tables[0].Rows[0]["CARRY_PASSENGER_FOR_CHARGE_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKPASSENGER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#2" + DSTempDataSet.Tables[0].Rows[0]["CARRY_PASSENGER_FOR_CHARGE_DESC"].ToString()) + "</REMARKPASSENGER>";
					if(DSTempDataSet.Tables[0].Rows[0]["IS_RENTED_OTHERS_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKRENTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#3" + DSTempDataSet.Tables[0].Rows[0]["IS_RENTED_OTHERS_DESC"].ToString()) + "</REMARKRENTED>";
					if(DSTempDataSet.Tables[0].Rows[0]["IS_BOAT_USED_IN_ANY_WATER_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKSWATERUSED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#4" + DSTempDataSet.Tables[0].Rows[0]["IS_BOAT_USED_IN_ANY_WATER_DESC"].ToString()) + "</REMARKSWATERUSED>";
					if(DSTempDataSet.Tables[0].Rows[0]["IS_REGISTERED_OTHERS_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKREGISTRATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#6" + DSTempDataSet.Tables[0].Rows[0]["IS_REGISTERED_OTHERS_DESC"].ToString()) + "</REMARKREGISTRATION>";
					#endregion
				}
			}
		}
		#endregion

		#region Code for Attachment Details XML Generation
		private void createAttachmentXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFAttachmentsDtls");
				gobjWrapper.ClearParameteres();

				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAttachmentsDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				#region Acord82 Page
				XmlElement Acord82AttachElement;
				Acord82AttachElement = AcordPDFXML.CreateElement("ATTACHMENTINFO");
				Acord82RootElement.AppendChild(Acord82AttachElement);
				Acord82AttachElement.SetAttribute(fieldType,fieldTypeSingle);

				Acord82AttachElement.InnerXml = Acord82AttachElement.InnerXml +  "<ATTACHPHOTOGRAPH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PHOTOGRAPH"].ToString()) + "</ATTACHPHOTOGRAPH>";
				Acord82AttachElement.InnerXml = Acord82AttachElement.InnerXml +  "<ATTACHSURVEY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SURVEY"].ToString()) + "</ATTACHSURVEY>";
				Acord82AttachElement.InnerXml = Acord82AttachElement.InnerXml +  "<ATTACHBLANKMARINECHKBOX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SURVEY"].ToString()) + "</ATTACHBLANKMARINECHKBOX>";
				Acord82AttachElement.InnerXml = Acord82AttachElement.InnerXml +  "<ATTACHCOSTGUARDCHKBOX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COSTGUARD"].ToString()) + "</ATTACHCOSTGUARDCHKBOX>";
				#endregion
			}
		}
		#endregion

		#region Code for Acord82Operators
		private void createAcord82OperatorXML()
		{
			if(gStrPdfFor == PDFForAcord)
			{

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetAccordOperatorDtls");
				gobjWrapper.ClearParameteres();

				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetAccordOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
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

					int RowCounter = 0,operator_id=1;
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
							
							if(strOprSSn.Trim() !="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
							{
								string strvaln = "xxx-xx-";
								strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
								strOprSSn = strvaln;
							}
							else 
								strOprSSn="";
						}

						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operator_id.ToString()) + "</OPERATORNUMBER>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_NAME"].ToString()) + "</OPERATORNAME>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORSEX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_SEX"].ToString()) + "</OPERATORSEX>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_DOB"].ToString()) + "</OPERATORDOB>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORAUTODRVLIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_DRIV_LIC"].ToString()) + "</OPERATORAUTODRVLIC>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORLICSTATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STATE_NAME"].ToString()) + "</OPERATORLICSTATE>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strOprSSn) + "</OPERATORSSN>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORMARITALSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MARITAL_STATUS"].ToString()) + "</OPERATORMARITALSTATUS>";
						operator_id++;
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
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempDriver = gobjWrapper.ExecuteDataSet("Proc_GetAccordOperatorDtls");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempDriver = gobjSqlHelper.ExecuteDataSet("Proc_GetAccordOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				string gStrdriverId="";
				int OperatorNo=1;
				int RowCounter = 0;
				foreach(DataRow Row in DSTempDriver.Tables[0].Rows)
				{				
					gStrdriverId=Row["OPERATORNO"].ToString();

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@DRIVERID",gStrdriverId);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFOperatorExpDtls");
					gobjWrapper.ClearParameteres();

					//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFOperatorExpDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "','"+ gStrdriverId +"'");
					#region Acord82 Page
					if (DSTempDataSet.Tables[0].Rows.Count >0 || DSTempDataSet.Tables[1].Rows.Count >0)
					{
						XmlElement Acord82OperatorViolations;
						Acord82OperatorViolations = AcordPDFXML.CreateElement("VIOLATIONS");
						Acord82RootElement.AppendChild(Acord82OperatorViolations);
						Acord82OperatorViolations.SetAttribute(fieldType,fieldTypeMultiple);
					
						Acord82OperatorViolations.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82OPREXP"));
						Acord82OperatorViolations.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPREXP"));
					
						Acord82OperatorViolations.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82OPREXPEXTN"));
						Acord82OperatorViolations.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPREXPEXTN"));

						
						string tempStr="0";
						//To fill the Operators and MVR details on main Page.
						//foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
						//{
						if(tempStr!=OperatorNo.ToString())//Row["DRIVER_ID"].ToString())
						{
							XmlElement Acord82ViolationElement;
							Acord82ViolationElement = AcordPDFXML.CreateElement("VIOLATIONINFO");
							Acord82OperatorViolations.AppendChild(Acord82ViolationElement);
							Acord82ViolationElement.SetAttribute(fieldType,fieldTypeNormal);
							Acord82ViolationElement.SetAttribute(id,RowCounter.ToString());
							
							if(DSTempDataSet.Tables[0].Rows.Count >0 &&  DSTempDataSet.Tables[1].Rows.Count > 0)
							{
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + OperatorNo.ToString() + "</OPERATOREXPNUM>";
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EXPDESCRIPTION"].ToString()) +  RemoveJunkXmlCharacters(DSTempDataSet.Tables[1].Rows[0]["VIODESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
							}
							if(DSTempDataSet.Tables[0].Rows.Count >0 &&  DSTempDataSet.Tables[1].Rows.Count <= 0)
							{
								
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + OperatorNo.ToString() + "</OPERATOREXPNUM>";
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EXPDESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
								
							}
							if(DSTempDataSet.Tables[0].Rows.Count <= 0 && DSTempDataSet.Tables[1].Rows.Count >0)
							{
								
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + OperatorNo.ToString() + "</OPERATOREXPNUM>";
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[1].Rows[1]["VIODESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
								
							}
							 
							RowCounter++;
						}
						tempStr=gStrdriverId;
							
						//}
					
						tempStr="0";
						//To fill the Operators and MVR details on Add on Operator Experience Page.
						/*foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
						{*/
						if(tempStr==OperatorNo.ToString())//Row["DRIVER_ID"].ToString())
						{
							XmlElement Acord82ViolationElement1;
							Acord82ViolationElement1 = AcordPDFXML.CreateElement("VIOLATIONINFO");
							Acord82OperatorViolations.AppendChild(Acord82ViolationElement1);
							Acord82ViolationElement1.SetAttribute(fieldType,fieldTypeNormal);
							Acord82ViolationElement1.SetAttribute(id,RowCounter.ToString());
							
							Acord82ViolationElement1.InnerXml = Acord82ViolationElement1.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + OperatorNo.ToString() + "</OPERATOREXPNUM>";
							Acord82ViolationElement1.InnerXml = Acord82ViolationElement1.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EXPDESCRIPTION"].ToString()) + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["VIODESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
							RowCounter++;
						}
						tempStr=gStrdriverId;
							
						//}
					}
					OperatorNo++;
				}
				#endregion
			}
		}
		#endregion

		#region code for boat info Xml

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
		private int GetPortAccessPrem()
		{

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails");
			gobjWrapper.ClearParameteres();

			//DataSet DSTempDataSet1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			int sumtotal = 0;

			if (DSTempDataSet1.Tables[0].Rows.Count > 0)
			{
				int RowCounter = 0;
				string[] arrDropdown; 
				int i=0;

				schEquip = 1;
				arrDropdown=new string[GetSchPremium().Count];
				foreach (XmlNode SchPremiumNode in GetSchPremium())
				{
					arrDropdown[i]=getAttributeValue(SchPremiumNode,"STEPPREMIUM").ToString();
					schEquip=i;
					i++;
				}
				foreach (DataRow Row in DSTempDataSet1.Tables[0].Rows)
				{
					if(arrDropdown.Length>0)
					{
						if(RowCounter<arrDropdown.Length)
						{
							sumtotal+=int.Parse(arrDropdown[RowCounter].ToString() ); 
						}
					}
					RowCounter++ ;
				}
			}
			return sumtotal;
		}

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

		private void createBoatXML()
		{
			DataSet DSTempEngineTrailer;
			int BoatCtr = 0, Eng_tra_Ctr = 0, Boatpage=1;
			int SchPrem = 0;
			double unattach_prem = 0;
			double sumTtlC=0;
			string boat_ded = "";
			double trailertotalprem=0;
			

			XmlElement BoatRootElementDecPage;
			BoatRootElementDecPage    = AcordPDFXML.CreateElement("BOAT");

			XmlElement BoatRootElementAcord82;
			BoatRootElementAcord82 = AcordPDFXML.CreateElement("BOAT");
			
			XmlElement BoatRootElementSupplement;
			BoatRootElementSupplement = AcordPDFXML.CreateElement("BOAT");

			XmlElement BoatRootElementDecPage0;
			BoatRootElementDecPage0    = AcordPDFXML.CreateElement("BOAT0");

			XmlElement BoatElementDecPage0;
			BoatElementDecPage0	= AcordPDFXML.CreateElement("BOATINFO0");
			
			#region setting Boat Root Elements
			if (gStrPdfFor == PDFForDecPage)
			{
				#region Boat Root Element for DecPage
				DecPageRootElement.AppendChild(BoatRootElementDecPage0);
				BoatRootElementDecPage0.SetAttribute(fieldType,fieldTypeMultiple);
				BoatRootElementDecPage0.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBOAT"));
				BoatRootElementDecPage0.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOAT"));
				BoatRootElementDecPage0.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBOATNEWEXTN"));
				BoatRootElementDecPage0.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOATNEWEXTN"));
				#endregion

				prnOrd_covCode = new string[50];
				prnOrd_attFile = new string[50];
				prnOrd = new int[50];

				// Added by Mohit Agarwal for Customer Details irrespective of added boats
				// 14-Mar-2007
				#region setting Customer Agency Details
				BoatRootElementDecPage0.AppendChild(BoatElementDecPage0);
				BoatElementDecPage0.SetAttribute(fieldType,fieldTypeNormal);
				BoatElementDecPage0.SetAttribute(id,BoatCtr.ToString());
				//Policy
				BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<CURRDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToShortDateString()) + "</CURRDATE>";
				if(gStrCalledFrom.Equals(CalledFromPolicy))
				{					
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
					BoatElementDecPage0.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
				}
					
				//Status Missing--Dont now How to maintain it.
				//POlicy Premium Adjustment.
				//Agency
				BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
				if(AgencyAddress!="")
				{
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress +" "+ "</AGENCYADDRESS>";
					
				}
				if(AgencyCitySTZip!="")
				{
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + " "+"</AGENCYCITYSTZIP>";
					
				}
				if(AgencyPhoneNumber!="")
				{
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber +"</AGENCYPHONE>";
				}
				if(AgencyCode!="")
				{
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
				}
				BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</AGENCYSUBCODE>";
				BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</AGENCYBILLING>";
				//Named Insured
				BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + NamedInsuredWithSuffix + "</INSUREDNAME>";
				BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<INSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerAddress + "</INSUREDADDRESS>";
				BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<INSUREDCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerCityStZip + "</INSUREDCITYSTZIP>";
				// adverse letter
				string strAdverseRep="";
				DataSet dsTempPolicy = new DataSet();

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				dsTempPolicy = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
				gobjWrapper.ClearParameteres();

				//dsTempPolicy = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				strAdverseRep = dsTempPolicy.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
					// if process is blank
				if(gStrProcessID=="")
				{
					gStrProcessID = GetPolicyProcess( gStrClientID ,gStrPolicyId ,gStrPolicyVersion , gStrCalledFrom, gobjWrapper);
				}
				//check for diffferent process
				if(gStrCopyTo != "AGENCY")
				{
					if(gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
						&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString()
						&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString()
						&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString()
						&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString())
					{
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
							createPage2XML(ref BoatElementDecPage0);
					}
					else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() )
					{
						//store preassigned value of strNeedPage2
						string elginsusc=strNeedPage2;
						if(gStrCopyTo != "AGENCY" && strAdverseRep=="Y" && strNeedPage2 == "Y" && strInsuScore !="-2")
						{
							ChkPreInsuScr();
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(oldInsuScr !="-2")
								{
									// refernce itrack 3222 if fallen in lower tier then print
									if(strNeedPage2 =="Y")
										createPage2AdverseXML(ref BoatElementDecPage0);
								}
								else
								{
									createPage2AdverseXML(ref BoatElementDecPage0);
								}
							}
							
						}
						else if(gStrCopyTo != "AGENCY"  && strAdverseRep=="Y" && strNeedPage2 == "Y" && strInsuScore =="-2")
						{
							//call ChkPreInsuScr function to assign values to newInsuScr,oldInsuScr
							ChkPreInsuScr();
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(elginsusc=="Y")
									createPage2NHNSAdverseXML(ref BoatElementDecPage0);
							}
						}

					}
					else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString())
					{
						//store preassigned value of strNeedPage2
						string elginsusc=strNeedPage2;
						// if insurance score is not no hit no score
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y" && strInsuScore !="-2")
						{

							ChkPreInsuScr();
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(oldInsuScr !="-2")
								{
									// refernce itrack 3222 if fallen in lower tier then print
									if(strNeedPage2 =="Y")
										createPage2AdverseXML(ref BoatElementDecPage0);
								}
								else
								{
									createPage2AdverseXML(ref BoatElementDecPage0);
								}
							}
						}
							//if insuracne score is no hit no score
						else if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y" && strInsuScore =="-2")
						{
							//call ChkPreInsuScr function to assign values to newInsuScr,oldInsuScr
							ChkPreInsuScr();
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(elginsusc=="Y")
								createPage2NHNSAdverseXML(ref BoatElementDecPage0);
							}
						}
					}
					else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString())
					{
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
						{
							createPage2XML(ref BoatElementDecPage0);
						}
						else if( strNeedPage2 == "N")
						{
							createPage2PrivacyPageXML(ref BoatElementDecPage0);
						}
					}
					else if(gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString())
					{
						if( strNeedPage2 == "N")
						{
							createPage2PrivacyPageXML(ref BoatElementDecPage0);
						}
						else
						{
							createPage2XML(ref BoatElementDecPage0);
						}
					}
					else
					{
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
						{
							createPage2AdverseXML(ref BoatElementDecPage0);
						}
					}
				}
						
				//Reason Code
				//				BoatElementDecPage0.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
				//				BoatElementDecPage0.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
				//				BoatElementDecPage0.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
				//				BoatElementDecPage0.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
				#endregion 

				BoatElementDecPage0.AppendChild(BoatRootElementDecPage);
				BoatRootElementDecPage.SetAttribute(fieldType,fieldTypeMultiple);
				BoatRootElementDecPage.SetAttribute(PrimPDF,"");
				BoatRootElementDecPage.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOAT"));
				BoatRootElementDecPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBOATNEWEXTN"));
				BoatRootElementDecPage.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOATNEWEXTN"));

			}
			else if (gStrPdfFor == PDFForAcord)
			{
				#region Boat Root Element for Acord82
				Acord82RootElement.AppendChild(BoatRootElementAcord82);
				BoatRootElementAcord82.SetAttribute(fieldType,fieldTypeMultiple);
				BoatRootElementAcord82.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82BOAT"));
				BoatRootElementAcord82.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOAT"));
				BoatRootElementAcord82.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82BOATEXTN"));
				BoatRootElementAcord82.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOATEXTN"));
				#endregion
			
				#region Boat Root Element for Supplement
				SupplementalRootElement.AppendChild(BoatRootElementSupplement);

				BoatRootElementSupplement.SetAttribute(fieldType,fieldTypeMultiple);
				BoatRootElementSupplement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTBOAT"));
				BoatRootElementSupplement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTBOAT"));
				BoatRootElementSupplement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTBOATEXTN"));
				BoatRootElementSupplement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTBOATEXTN"));
				#endregion
			}
			#endregion
			//declare dataset for trailer premium			
			DataSet Dstrailpre = new DataSet();	
			DataSet DsSchprem = new DataSet();

			
			
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
			gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
			gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DsSchprem = gobjWrapper.ExecuteDataSet("PROC_GetScheduleEquip_Premium");
			gobjWrapper.ClearParameteres();
			
			//DsSchprem =  gobjSqlHelper.ExecuteDataSet("PROC_GetScheduleEquip_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			// fetch total policy premium
			foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
			{
			
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempEngineTrailer1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempEngineTrailer1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
				gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
				gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOAT_ID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				Dstrailpre = gobjWrapper.ExecuteDataSet("PROC_GetTrailer_Premium");
				gobjWrapper.ClearParameteres();

				//Dstrailpre = gobjSqlHelper.ExecuteDataSet("PROC_GetTrailer_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				// If Proccess is not committed then fetch premiums from premium xml
				// if proccess is committed then fetch premiums from split table
				if(gStrtemp!="final")
				{
				}
				else
				{
					// premium from split table 
					
					SchPrem=Convert.ToInt32(GetTrailerPremium(DsSchprem,"COVERAGE_PREMIUM"));
					sumTtlC += GetPremiumSch(DSTempEngineTrailer1,"B","COVERAGE_PREMIUM");
					sumTtlC += GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
					sumTtlC +=Convert.ToInt32(GetPremiumUnattached(DSTempEngineTrailer1,"UAE","COVERAGE_PREMIUM"));

					if(SchPrem !=0 || SchPrem !=0.00)
					{
						intschEquip = 1;
					}
				}
			}
			#region Boat Xml for each one
			foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
			{
				
				XmlElement BoatElementDecPage;
				BoatElementDecPage	= AcordPDFXML.CreateElement("BOATINFO");
				
				XmlElement BoatElementAcord82;
				BoatElementAcord82	= AcordPDFXML.CreateElement("BOATINFO");

				XmlElement BoatElementSuppliment;
				BoatElementSuppliment	= AcordPDFXML.CreateElement("BOATINFO");

				htpremium.Clear(); 

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempEngineTrailer1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempEngineTrailer1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				
				gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
				gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
				gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOAT_ID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				Dstrailpre = gobjWrapper.ExecuteDataSet("PROC_GetTrailer_Premium");
				gobjWrapper.ClearParameteres();

				//Dstrailpre = gobjSqlHelper.ExecuteDataSet("PROC_GetTrailer_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				//DataSet DSTempEngineTrailer1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				// total premium of boat
				LoadRateXML("BOAT");
				foreach (XmlNode PremiumNode in GetBoatPremium(BoatDetail["BOAT_ID"].ToString()))
				{
					if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
						htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
				}

				double sumTtlB=0;
				double Accordtrailertotalprem=0;
				// premium from quote premium xml
				if(gStrtemp!="final")
				{
					// fetch premiums from quote premium xml
					foreach (XmlNode SumTotalNode in GetSumTotalPremium())
					{
						if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
							sumTtlC += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
					}
					// premium for schedule equipment
					SchPrem = GetPortAccessPrem();
					//sumTtlC += SchPrem;
					double schPremAdj=0;
					//					foreach (XmlNode PremAdjNode in GetMinPremSch())
					//					{
					//						if(getAttributeValue(PremAdjNode,"STEPPREMIUM")!=null && getAttributeValue(PremAdjNode,"STEPPREMIUM").ToString()!="" )
					//						{
					//							schPremAdj = double.Parse(getAttributeValue(PremAdjNode,"STEPPREMIUM").ToString()) ;						
					//							break;
					//						}
					//					}
					SchPrem += Convert.ToInt32(schPremAdj);
					if(SchPrem !=0 || SchPrem !=0.00)
					{
						intschEquip = 1;
					}
					// fetch final premium for each boat and add it. 
					foreach (XmlNode SumTotalNode in GetSumTotalPremium(BoatDetail["BOAT_ID"].ToString()))
					{
						if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
						{
							sumTtlB += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
							break;
						}
					}
					// premium for unattached equipment
					foreach (XmlNode PremiumNode in GetUnattachedPremium("1"))
					{
						if(getAttributeValue(PremiumNode,"COMPONENT_CODE")=="BOAT_UNATTACH_PREMIUM" && getAttributeValue(PremiumNode,"STEPPREMIUM")!="" && getAttributeValue(PremiumNode,"STEPPREMIUM")!="Included")
						{
							unattach_prem=Convert.ToDouble(getAttributeValue(PremiumNode,"STEPPREMIUM"));
						}
					}
					
				}
				else
				{
					sumTtlB = GetPremiumBoat(DSTempEngineTrailer1,"B",BoatDetail["BOAT_ID"].ToString(),"COVERAGE_PREMIUM");
					sumTtlB += GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
					trailertotalprem=GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
					Accordtrailertotalprem=GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
					unattach_prem = GetPremiumUnattached(DSTempEngineTrailer1,"UAE","COVERAGE_PREMIUM");
					//sumTtlB += unattach_prem;
					//					SchPrem=GetPremiumSch();
				}

				// fetch premium information of trailer attched with boat
				double finaltrailPrem=0;

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempEngineTrail = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempEngineTrail = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				foreach (DataRow trailRow in DSTempEngineTrail.Tables[0].Rows)
				{
					foreach (XmlNode TrailNode in GetSumTotalTrailerPremium(trailRow["TRAILER_NUMBER"].ToString()))
					{
						// if proccess not committed
						if(gStrtemp !="final")
						{
							if(getAttributeValue(TrailNode,"STEPPREMIUM")!=null && getAttributeValue(TrailNode,"STEPPREMIUM").ToString()!="" )
							{
								finaltrailPrem += double.Parse(getAttributeValue(TrailNode,"STEPPREMIUM").ToString()) ;						
								trailertotalprem=finaltrailPrem;
								Accordtrailertotalprem=finaltrailPrem;
								break;
							}
								// if proccess committed
							else
							{
							
							}
						}
					}
				}

				//					if(AutoDetail["AMOUNT"] != System.DBNull.Value)
				//						balance_due += Convert.ToInt32(AutoDetail["AMOUNT"]);
				// boat deductible
				
				double sumTtlE=0;
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
								boat_ded = "$" + DollarFormat(granddeduc);
						}
						//break;
					}

					if(CoverageDetails1["ENDORS_ASSOC_COVERAGE"].ToString().Trim() == "Y")
					{
						//string endorsprem = GetPremium(DSTempEngineTrailer1, covCode1);
						//Added by asfa (20-Feb-2008) - iTrack issue #3331
						string endorsprem="";
						if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
							endorsprem= GetPremiumBeforeCommit(DSTempEngineTrailer1, covCode1,htpremium );
						else // when gStrtemp != "temp"
							endorsprem = GetPremium(DSTempEngineTrailer1, covCode1);
								
						try
						{
							if(endorsprem != "" && endorsprem != "Included")
								sumTtlE += double.Parse(endorsprem.Replace("$",""));
						}
						catch//(Exception ex)
						{}
					}
				}
				// Maping for desining of Dec page 
				if (gStrPdfFor == PDFForDecPage)
				{
					#region Boat Element for Dec Page
					BoatRootElementDecPage.AppendChild(BoatElementDecPage);
					BoatElementDecPage.SetAttribute(fieldType,fieldTypeNormal);
					BoatElementDecPage.SetAttribute(id,BoatCtr.ToString());
					//Policy
					/*						BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<CURRDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToShortDateString()) + "</CURRDATE>";
										if(gStrCalledFrom.Equals(CalledFromPolicy))
										{					
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
											BoatElementDecPage.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
										}
					
										//Status Missing--Dont now How to maintain it.
										//POlicy Premium Adjustment.
										//Agency
										BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
										if(AgencyAddress!="")
										{
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress +" "+ "</AGENCYADDRESS>";
					
										}
										if(AgencyCitySTZip!="")
										{
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + " "+"</AGENCYCITYSTZIP>";
					
										}
										if(AgencyPhoneNumber!="")
										{
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber +"</AGENCYPHONE>";
										}
										if(AgencyCode!="")
										{
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
										}
										BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</AGENCYSUBCODE>";
										BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</AGENCYBILLING>";
										//Named Insured
										BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</INSUREDNAME>";
										BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<INSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerAddress + "</INSUREDADDRESS>";
										BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<INSUREDCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerCityStZip + "</INSUREDCITYSTZIP>";

										//Reason Code
										BoatElementDecPage.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
										BoatElementDecPage.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
										BoatElementDecPage.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
										BoatElementDecPage.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
					*/					
					//Location
					// preparing inner xml
					BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + NamedInsuredWithSuffix + "</INSUREDNAME>";
					BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<CURRDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToShortDateString()) + "</CURRDATE>";
					if(gStrCalledFrom.Equals(CalledFromPolicy))
					{
						BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
						BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
						BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
						BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
					}
					noBoats= DSTempDataSet.Tables[0].Rows.Count;
					string pageOf = Boatpage + " of " + (DSTempDataSet.Tables[0].Rows.Count+intschEquip).ToString();
					BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<PAGE " + fieldType + "=\"" + fieldTypeText + "\">" + pageOf + "</PAGE>";
					// Watercraft Location
					if(BoatDetail["LOCADD"].ToString()!="")
					{
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<LOCATIONADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["LOCADD"].ToString())+"</LOCATIONADDRESS>"; 
					}
					if(BoatDetail["LOCCITY"].ToString()!="")
					{
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<LOCATIONCITYSTZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["LOCCITY"].ToString())+"</LOCATIONCITYSTZIP>"; 
					}

					// Added by Mohit Agarwal 25-Jan-2007
					// Insurance Score
					if(BoatDetail["APPLY_INSURANCE_SCORE"] != null && BoatDetail["APPLY_INSURANCE_SCORE"].ToString().IndexOf("-") < 0)
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<INS_SCORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["APPLY_INSURANCE_SCORE"].ToString())+"</INS_SCORE>"; 

					XmlNodeList ins_scoreNodeList = GetCreditForInsScore();
					XmlNode ins_scoreNode;
					if(ins_scoreNodeList.Count > 0)
					{
						ins_scoreNode = ins_scoreNodeList.Item(0);
						String [] discRows = getAttributeValue(ins_scoreNode,"STEPDESC").Split('-');

						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<DISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(discRows[discRows.Length -1])+"</DISCOUNT>"; 
					}
					//maping Boat details
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<WATERSNAVIGATED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["WATERS_NAVIGATED_USE"].ToString())+"</WATERSNAVIGATED>"; 
					string terr = BoatDetail["BOAT_TERRITORY"].ToString();
					string []territory = terr.Split('-');

					string makemodel = "";
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATTERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(territory[0])+"</BOATTERRITORY>"; 
												
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString())+"</BOATNUMBER>"; 
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOATTYPE"].ToString())+"</BOATTYPE>";
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATYEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString())+"</BOATYEAR>";
					makemodel = BoatDetail["BOAT_MAKE"].ToString();
					if(BoatDetail["BOAT_MODEL"].ToString() != "")
						makemodel += "/" + BoatDetail["BOAT_MODEL"].ToString();
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATDESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</BOATDESC>";
					//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MODEL"].ToString())+"</BOATMODEL>";
					if(BoatDetail["BOAT_INCHES"].ToString()=="")
					{
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATLENGTH  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'")+"</BOATLENGTH>";
					}
					else
					{
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATLENGTH  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'"+" "+ BoatDetail["BOAT_INCHES"].ToString() +"''")+"</BOATLENGTH>";
					}
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATHPCC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HORSE_POWER"].ToString().Replace(".00",""))+"</BOATHPCC>";
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATSERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_ID_NO"].ToString())+"</BOATSERIAL>";
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATCONSTRUCTION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_MATERIAL"].ToString())+"</BOATCONSTRUCTION>";
					if(BoatDetail["PRESENT_VALUE"].ToString()!="" && BoatDetail["PRESENT_VALUE"].ToString()!="0")
					{
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATLIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + BoatDetail["PRESENT_VALUE"].ToString().Replace(".00",""))+"</BOATLIMIT>";
					}
					else
					{
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATLIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</BOATLIMIT>";
					}
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATDED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATDED>";
					// Mapping Boat Coverages
					string lstrGetPremium="";
					if(gStrtemp !="final")
					{
						if(htpremium.Contains("BOAT_PD"))
						{
							lstrGetPremium = htpremium["BOAT_PD"].ToString();
							int lintGetPremium = lstrGetPremium.IndexOf(".");
							if(lstrGetPremium != "0" && lstrGetPremium !="")
							{
								if(lintGetPremium == -1)
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + htpremium["BOAT_PD"].ToString()+".00")+"</BOATPREMIUM>";
								}
								else
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + htpremium["BOAT_PD"].ToString())+"</BOATPREMIUM>";
								}
							}
						}
					}
					else
					{
						int lintGetPremium=0;
						foreach(DataRow BoatPd in  DSTempEngineTrailer1.Tables[1].Rows)
							if(BoatPd["COMPONENT_CODE"].ToString() == "BOAT_PD")
							{
								lstrGetPremium = BoatPd["COVERAGE_PREMIUM"].ToString();
								lintGetPremium = lstrGetPremium.IndexOf(".");
							}
						if(lstrGetPremium != "0" && lstrGetPremium !="")
						{
							if(lintGetPremium == -1)
							{
								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + lstrGetPremium +".00")+"</BOATPREMIUM>";
							}
							else
							{
								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + lstrGetPremium)+"</BOATPREMIUM>";
							}
						}
					}
					//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATPREMIUM  " + fieldType +"=\""+ fieldTypeText +"\"></BOATPREMIUM>";
					//int strcount=0,strtrialer=0 ;
					//Engine Information...
					//#region engine & trailer Element for Dec Page
					int engtrictr=0;
					double trailPremium=0;	
		
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS");
					gobjWrapper.ClearParameteres();

					//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
					
					int countengine = DSTempEngineTrailer.Tables[0].Rows.Count;
					
					if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
					{
						foreach(DataRow EngineDetail in DSTempEngineTrailer.Tables[0].Rows)
						{
							if(engtrictr < 2)
							{
								if(engtrictr==0)
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["ENGINE_NO"].ToString())+"</ENGINE1NUMBER>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["ENGINETYPE"].ToString())+"</ENGINE1TYPE>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["YEAR"].ToString())+"</ENGINE1YEAR>";
									makemodel = EngineDetail["MAKE"].ToString();
									if(EngineDetail["MODEL"].ToString() != "")
										makemodel += "/" + EngineDetail["MODEL"].ToString();
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE1DESC>";
									//BoatElementDecPage.InnerXml= DecPageEngineElement.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1HPCC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["HORSEPOWER"].ToString())+"</ENGINE1HPCC>";								
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["SERIAL_NO"].ToString())+"</ENGINE1SERIAL>";
									if(EngineDetail["BOAT_INSURING_VALUE"].ToString() =="0.00" || EngineDetail["BOAT_INSURING_VALUE"].ToString() =="0")
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</ENGINE1LIMIT>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</ENGINE1PREMIUM>";
									}
									else
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINE1LIMIT>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINE1PREMIUM>";
									}
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</ENGINE1DED>";
									
									engtrictr++;
								}
								else
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["ENGINE_NO"].ToString())+"</ENGINE2NUMBER>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["ENGINETYPE"].ToString())+"</ENGINE2TYPE>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["YEAR"].ToString())+"</ENGINE2YEAR>";
									makemodel = EngineDetail["MAKE"].ToString();
									if(EngineDetail["MODEL"].ToString() != "")
										makemodel += "/" + EngineDetail["MODEL"].ToString();
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE2DESC>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2HPCC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["HORSEPOWER"].ToString())+"</ENGINE2HPCC>";
									//BoatElementDecPage.InnerXml= DecPageEngineElement.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["SERIAL_NO"].ToString())+"</ENGINE2SERIAL>";
									if(EngineDetail["BOAT_INSURING_VALUE"].ToString() =="0.00" || EngineDetail["BOAT_INSURING_VALUE"].ToString() =="0")
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</ENGINE2LIMIT>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</ENGINE2PREMIUM>";
									}
									else
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINE2LIMIT>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINE2PREMIUM>";
									}
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</ENGINE2DED>";
									
									engtrictr++;
								}
							}
						}
					
						//TRAILER INFORMATION...

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
						gobjWrapper.ClearParameteres();

						//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
						int counttrailer = DSTempEngineTrailer.Tables[0].Rows.Count;
						int secondtrailer=0,enginectr=0,trialerctr=0;
						if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
						{
							foreach(DataRow trailerdetail in DSTempEngineTrailer.Tables[0].Rows)
							{
								// fetch premium for each trialer
								// if process committed
								if(gStrtemp =="final")
								{
									foreach(DataRow trailerRow in Dstrailpre.Tables[0].Rows)
									{
										if((trailerRow["TRAILER_NUMBER"].ToString()) == (trailerdetail["TRAILER_NUMBER"].ToString()))
											trailPremium=Convert.ToDouble(trailerRow["COVERAGE_PREMIUM"].ToString());
									}
								}
									// if proccess not committed
								else
								{
									foreach (XmlNode TrailNode in GetSumTotalTrailerPremium(trailerdetail["TRAILER_NUMBER"].ToString()))
									{
										if(getAttributeValue(TrailNode,"STEPPREMIUM")!=null && getAttributeValue(TrailNode,"STEPPREMIUM").ToString()!="" )
										{
											trailPremium = double.Parse(getAttributeValue(TrailNode,"STEPPREMIUM").ToString()) ;						
											break;
										}
									}
									
								}
							
								//end fetching trailerpremium
								
									if(countengine==1 && enginectr==0)
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</ENGINE2NUMBER>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</ENGINE2TYPE>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</ENGINE2YEAR>";
										makemodel = trailerdetail["MANUFACTURER"].ToString();
										if(trailerdetail["MODEL"].ToString() != "")
											makemodel += "/" + trailerdetail["MODEL"].ToString();
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE2DESC>";
										//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</TRAILERMODEL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</ENGINE2SERIAL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</ENGINE2LIMIT>";

										string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
										string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
										if(grandpercent.IndexOf("%") > 0)
										{
											double granddeduc = GetPrecentDed(grandpercent, DSTempEngineTrailer.Tables[0].Rows[0]["TRAILER_LIMIT"].ToString());
											if(granddeduc != 0)
												trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
										}
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</ENGINE2DED>";

										if(trailPremium != 0)
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</ENGINE2PREMIUM>";
										enginectr++;
										engtrictr++;
									}
										////////////--------
										///
									else
									{
										if(trialerctr < 2)
										{
											if(secondtrailer==0)
											{
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</TRAILER1NUMBER>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</TRAILER1TYPE>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</TRAILER1YEAR>";
												makemodel = trailerdetail["MANUFACTURER"].ToString();
												if(trailerdetail["MODEL"].ToString() != "")
													makemodel += "/" + trailerdetail["MODEL"].ToString();
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</TRAILER1DESC>";
												//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILERMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</TRAILERMODEL>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</TRAILER1SERIAL>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</TRAILER1LIMIT>";

												string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
												string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
												if(grandpercent.IndexOf("%") > 0)
												{
													double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
													if(granddeduc != 0)
														trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
												}

												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</TRAILER1DED>";
												if(trailPremium != 0)
													BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</TRAILER1PREMIUM>";
												secondtrailer++;
												engtrictr++;
												trialerctr++;

											}
											else
											{
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</TRAILER2NUMBER>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</TRAILER2TYPE>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</TRAILER2YEAR>";
												makemodel = trailerdetail["MANUFACTURER"].ToString();
												if(trailerdetail["MODEL"].ToString() != "")
													makemodel += "/" + trailerdetail["MODEL"].ToString();
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</TRAILER2DESC>";
												//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILERMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</TRAILERMODEL>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</TRAILER2SERIAL>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</TRAILER2LIMIT>";

												string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
												string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
												if(grandpercent.IndexOf("%") > 0)
												{
													double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
													if(granddeduc != 0)
														trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
												}

												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</TRAILER2DED>";
												if(trailPremium != 0)
													BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</TRAILER2PREMIUM>";
												engtrictr++;
												trialerctr++;
											}
										}
									}
								
							}
							//reinialize secondtrailer counter for next boat
							secondtrailer=0;
							trialerctr=0;
						}	
						// reinialize enginecounter for next boat
						countengine=0;
					}
					else // No Engine
					{
						// mapping Boat Attached trailer information

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
						gobjWrapper.ClearParameteres();

						//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
						int counttrailer=DSTempEngineTrailer.Tables[0].Rows.Count;
						int RowCount=0;
						if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
						{
							foreach(DataRow trailerdetail in DSTempEngineTrailer.Tables[0].Rows)
							{
								// fetch premium for each trialer
								// if process committed
								if(gStrtemp =="final")
								{
									foreach(DataRow trailerRow in Dstrailpre.Tables[0].Rows)
									{
										if((trailerRow["TRAILER_NUMBER"].ToString()) == (trailerdetail["TRAILER_NUMBER"].ToString()))
											trailPremium=Convert.ToDouble(trailerRow["COVERAGE_PREMIUM"].ToString());
									}
								}
									// if proccess not committed
								else
								{
									foreach (XmlNode TrailNode in GetSumTotalTrailerPremium(trailerdetail["TRAILER_NUMBER"].ToString()))
									{
										if(getAttributeValue(TrailNode,"STEPPREMIUM")!=null && getAttributeValue(TrailNode,"STEPPREMIUM").ToString()!="" )
										{
											trailPremium = double.Parse(getAttributeValue(TrailNode,"STEPPREMIUM").ToString()) ;						
											break;
										}
									}
								}
							
								//end fetching trailerpremium
								// first trailer
								if(RowCount <4)
								{
									if(RowCount==0)
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</ENGINE1NUMBER>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</ENGINE1TYPE>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</ENGINE1YEAR>";
										makemodel = trailerdetail["MANUFACTURER"].ToString();
										if(trailerdetail["MODEL"].ToString() != "")
											makemodel += "/" + trailerdetail["MODEL"].ToString();
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE1DESC>";
										//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</ENGINE1SERIAL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</ENGINE1LIMIT>";

										string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
										string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
										if(grandpercent.IndexOf("%") > 0)
										{
											double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
											if(granddeduc != 0)
												trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
										}

										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</ENGINE1DED>";
									
									
										if(trailPremium != 0)
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</ENGINE1PREMIUM>";
								
										RowCount++;
										engtrictr++;
									}
										// second trialer
									else if(RowCount==1)
									{
									
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</ENGINE2NUMBER>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</ENGINE2TYPE>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</ENGINE2YEAR>";
										makemodel = trailerdetail["MANUFACTURER"].ToString();
										if(trailerdetail["MODEL"].ToString() != "")
											makemodel += "/" + trailerdetail["MODEL"].ToString();
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE2DESC>";
										//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</ENGINE2SERIAL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</ENGINE2LIMIT>";

										string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
										string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
										if(grandpercent.IndexOf("%") > 0)
										{
											double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
											if(granddeduc != 0)
												trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
										}

										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</ENGINE2DED>";
										if(trailPremium != 0)
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</ENGINE2PREMIUM>";
								
										RowCount++;
										engtrictr++;
									}
										//third trailer
									else if(RowCount==2)
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</TRAILER1NUMBER>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</TRAILER1TYPE>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</TRAILER1YEAR>";
										makemodel = trailerdetail["MANUFACTURER"].ToString();
										if(trailerdetail["MODEL"].ToString() != "")
											makemodel += "/" + trailerdetail["MODEL"].ToString();
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</TRAILER1DESC>";
										//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</TRAILER1SERIAL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</TRAILER1LIMIT>";

										string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
										string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
										if(grandpercent.IndexOf("%") > 0)
										{
											double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
											if(granddeduc != 0)
												trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
										}

										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</TRAILER1DED>";
										if(trailPremium != 0)
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</TRAILER1PREMIUM>";
										RowCount++;
										engtrictr++;
									}
										//fourth trailer
									else 
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</TRAILER2NUMBER>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</TRAILER2TYPE>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</TRAILER2YEAR>";
										makemodel = trailerdetail["MANUFACTURER"].ToString();
										if(trailerdetail["MODEL"].ToString() != "")
											makemodel += "/" + trailerdetail["MODEL"].ToString();
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</TRAILER2DESC>";
										//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</TRAILER2SERIAL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</TRAILER2LIMIT>";

										string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
										string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
										if(grandpercent.IndexOf("%") > 0)
										{
											double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
											if(granddeduc != 0)
												trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
										}

										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</TRAILER2DED>";
										if(trailPremium != 0)
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</TRAILER2PREMIUM>";
										RowCount++;
										engtrictr++;
									}
								}
							}
							// reinialize counter for second boat
							RowCount=0;
						}
							
					}
					// re intialize counters for second boat
					engtrictr=0;
				}
						
					#endregion
				
					// Mapping for Accord pdf 
				else if (gStrPdfFor == PDFForAcord)
				{
					#region Boat Element for Acord82
					BoatRootElementAcord82.AppendChild(BoatElementAcord82);
					BoatElementAcord82.SetAttribute(fieldType,fieldTypeNormal);
					BoatElementAcord82.SetAttribute(id,BoatCtr.ToString());
					// mapping Boat Details
					BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<APPLICANTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ApplicantName1+"</APPLICANTNAME>"; 
					BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<APPLICANTIONNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(PolicyNumber)+"</APPLICANTIONNUMBER>"; 
					BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString())+"</BOATNUMBER>"; 
					BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATPOWER " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_POWER"].ToString())+"</BOATPOWER>";
					if(BoatDetail["BOAT_POWER"]!=null && BoatDetail["BOAT_POWER"].ToString()!="")
						if(BoatDetail["BOAT_POWER"].ToString().Equals("OTH"))
						{
							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATPOWEROTHER " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOATTYPE"].ToString())+"</BOATPOWEROTHER>";
						}
					BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATHULLMATERIAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULLMATERIAL"].ToString())+"</BOATHULLMATERIAL>";
					BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<HULLOTHERMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULLMATERIAL_DESCRIPTION"].ToString())+"</HULLOTHERMATERIAL>";
					BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString())+"</BOATYEAR>"; 
					BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MAKE_MODEL"].ToString())+"</BOATMAKEMODEL>"; 
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

					#region Boat Element for suppliment Page
					BoatRootElementSupplement.AppendChild(BoatElementSuppliment);
					BoatElementSuppliment.SetAttribute(fieldType,fieldTypeNormal);
					BoatElementSuppliment.SetAttribute(id,BoatCtr.ToString());
					BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<APPLICANTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ApplicantName1+"</APPLICANTNAME>"; 
					BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<APPLICANTIONNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(PolicyNumber)+"</APPLICANTIONNUMBER>"; 
					BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<BOATNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString()+ " TYPE: " + BoatDetail["BOATTYPE"].ToString())+"</BOATNUMBER>"; 
					BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<BOATYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString())+"</BOATYEAR>"; 
					BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<BOATMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MAKE_MODEL"].ToString())+"</BOATMAKEMODEL>"; 
					#endregion

					#region Acord82 Engine Details
											
					#region setting Root ENGINE Attribute
					XmlElement EngineRootElement;
					EngineRootElement	= AcordPDFXML.CreateElement("ENGINE");
					BoatElementAcord82.AppendChild(EngineRootElement);
					EngineRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					EngineRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82ENGINE"));
					EngineRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82ENGINE"));
					EngineRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82ENGINEEXTN"));
					EngineRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82ENGINEEXTN"));
					#endregion
					
					// fetching boat engine information

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS");
					gobjWrapper.ClearParameteres();

					//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");

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
						//EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEPRESENTVALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["PRESENT_VALUE"].ToString())+"</ENGINEPRESENTVALUE>"; 
						EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEPRESENTVALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINEPRESENTVALUE>"; 
						if(BoatDetail["BOAT_POWER"]!=null && BoatDetail["BOAT_POWER"].ToString()!= "")
							EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEOTHER " + fieldType +"=\""+ fieldTypeText +"\">" + BoatDetail["TWIN_SINGLE"].ToString() + " - " +  RemoveJunkXmlCharacters(EngineDetail["OTHER"].ToString()) + "</ENGINEOTHER>"; 

						Eng_tra_Ctr++;
					}
					Eng_tra_Ctr = 0;
					#endregion

					#region Acord82 trailer Details

					#region setting Root TRAILER Attribute
					XmlElement TrailerRootElement;
					TrailerRootElement = AcordPDFXML.CreateElement("TRAILER"); 
					BoatElementAcord82.AppendChild(TrailerRootElement);
					TrailerRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					TrailerRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82TRAILER"));
					TrailerRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82TRAILER"));
					TrailerRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82TRAILEREXTN"));
					TrailerRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82TRAILEREXTN"));
					#endregion
					
					// maping Boat attached Trailer 

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
					gobjWrapper.ClearParameteres();

					//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");

					foreach(DataRow TrailerDetail in DSTempEngineTrailer.Tables[0].Rows)
					{
						XmlElement TrailerElement;
						TrailerElement = AcordPDFXML.CreateElement("TRAILERINFO");
						TrailerRootElement.AppendChild(TrailerElement);
						TrailerElement.SetAttribute(fieldType,fieldTypeNormal);
						TrailerElement.SetAttribute(id,Eng_tra_Ctr.ToString());
					
						TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["YEAR"].ToString())+"</TRAILERYEAR>"; 
						if(TrailerDetail["MODEL"].ToString() !="" && TrailerDetail["MANUFACTURER"].ToString() !="")
						{
							TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["MANUFACTURER"].ToString()+ "/" + TrailerDetail["MODEL"].ToString())+"</TRAILERMAKEMODEL>"; 
						}
						else if(TrailerDetail["MANUFACTURER"].ToString() !="")
						{
							TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["MANUFACTURER"].ToString())+"</TRAILERMAKEMODEL>"; 
						}
						else
						{
							TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["MODEL"].ToString())+"</TRAILERMAKEMODEL>"; 
						}
							TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERSRNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["SERIAL_NO"].ToString())+"</TRAILERSRNUM>"; 
						if(TrailerDetail["INSURED_VALUE"].ToString()!="")
						{
							if(Eng_tra_Ctr == 0)
							{
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERCOST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["INSURED_VALUE"].ToString().Replace(".00",""))+"</TRAILERCOST>"; 
							}
							else
							{
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERCOST " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(TrailerDetail["INSURED_VALUE"].ToString().Replace(".00",""))+"</TRAILERCOST>"; 
							}
						}
						else
						{
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERCOST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</TRAILERCOST>"; 
						}
						Eng_tra_Ctr++;
					}
					Eng_tra_Ctr = 0;
					#endregion
				}

				#region COVERAGES

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
				gobjWrapper.ClearParameteres();

				//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				
				// Three Boat coverages moved separately for other in separate xml portion
				// 16-Aug 2007 Mohit Agarwal
				

				foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
				{
					//Added by asfa (20-Feb-2008) - iTrack issue #3331
					string covPrem="";
					if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
						covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
					else // when gStrtemp != "temp"
						covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
					//					if(CoverageDetails["COV_CODE"].ToString() == "EBIUE")
					//					{
					//						unattach_prem = double.Parse(covPrem);
					//					}
					
					if(covPrem == "")
						covPrem = "Included";
					else if(covPrem != "Included" && covPrem != "Extended") 
					{
						covPrem ="$" + covPrem;
					}
					// premium for Unattached Equipment And Personal Effects
					//try
					//{
					//if(CoverageDetails["COV_CODE"].ToString() == "EBIUE" && covPrem.Trim() != "Included")
					//unattach_prem = double.Parse(covPrem.Replace("$",""));
					//covPrem = "$" + System.Convert.ToString(unattach_prem);
					//}
					//catch(Exception ex)
					//{}

					switch(CoverageDetails["COV_CODE"].ToString())
					{
							// premium for Watercraft Liability
						case "LCCSL":
							// mapping for dec page
							if (gStrPdfFor == PDFForDecPage)
							{
								
								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIAB " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPERSONALLIAB>";
								if(CoverageDetails["LIMIT_1"].ToString() != "")
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","")+ " Each Occurrence" )+"</BOATCOVPERSONALLIABLIM>";
								else
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString())+"</BOATCOVPERSONALLIABLIM>";
								if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() =="Extended from HO")
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+"Extended"+"</BOATCOVPERSONALLIABPREM>";
								
								}
								else
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPERSONALLIABPREM>";
								}

								//								if(htpremium.Contains("BOAT_ LIABILITY_PREMIUM"))
								//								{
								//									lstrGetPremium = htpremium["BOAT_ LIABILITY_PREMIUM"].ToString();
								//									lintGetindex   = lstrGetPremium.IndexOf(".");
								//									if(lintGetindex ==-1)
								//									{
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString() + ".00")+"</BOATCOVPERSONALLIABPREM>";
								//									}
								//									else
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString())+"</BOATCOVPERSONALLIABPREM>";
								//								}
								//								
							}
							break;
							// premium for Section II - Medical
						case "MCPAY":
							// mapping for dec page
							if (gStrPdfFor == PDFForDecPage)
							{
								//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVMEDPM>";
								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Medical Payment")+"</BOATCOVMEDPM>";
								if(CoverageDetails["LIMIT_1"].ToString() != "")
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","") + " Per Person /$25,000 Each Occurrence" )+"</BOATCOVMEDPMLIM>";
								else
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString())+"</BOATCOVMEDPMLIM>";
								if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() =="Extended from HO")
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+"Extended"+"</BOATCOVMEDPMPREM>";
								
								}
								else
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVMEDPMPREM>";
								}
								//								if(htpremium.Contains("BOAT_MP_PREMIUM"))
								//								{
								//									lstrGetPremium = htpremium["BOAT_MP_PREMIUM"].ToString();
								//									lintGetindex = lstrGetPremium.IndexOf(".");
								//									if(lintGetindex ==-1)
								//									{
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString() + ".00")+"</BOATCOVMEDPMPREM>";
								//									}
								//									else
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString())+"</BOATCOVMEDPMPREM>";
								//
								//								}
								//								
							}
							break;
							// premium for Uninsured Boaters
						case "UMBCS":
							// mapping for dec page
							if (gStrPdfFor == PDFForDecPage)
							{
								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVUMBOATERS>";
								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","") + " Each Occurrence/Each Unit")+"</BOATCOVUMBOATERSLIM>";
								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVUMBOATERSPREM>";
								//								if(htpremium.Contains("BOAT_UB_PREMIUM"))
								//								{	
								//									lstrGetPremium = htpremium["BOAT_UB_PREMIUM"].ToString();
								//									lintGetindex = lstrGetPremium.IndexOf(".");
								//									if(lintGetindex ==-1)
								//									{
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString() + ".00")+"</BOATCOVUMBOATERSPREM>";
								//									}
								//									else
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString())+"</BOATCOVUMBOATERSPREM>";
								//
								//								}
								//								
							}
							break;
						default: break;
					}
				}
				string strAVDed="",strAVLim="";
				double sumTtl=0;
				double dblSumUnattach=0;
				// premium of trailer attched with boat
				foreach (XmlNode SumTotalNode in GetSumTotalTrailerPremium())
				{
					if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
						sumTtl += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						

				}
	
				if(BoatCtr != 0)
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALBOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(sumTtlB + finaltrailPrem))+"</TOTALBOATPREMIUM>";
				else
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALBOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+"$" + DollarFormat(sumTtlB+ finaltrailPrem + unattach_prem)+"</TOTALBOATPREMIUM>";
				if(gStrtemp!="final")
				{
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALPOLPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(sumTtlC+(double)SchPrem+unattach_prem))+"</TOTALPOLPREMIUM>";							
				}
				else
				{
					BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALPOLPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(sumTtlC+(double)SchPrem))+"</TOTALPOLPREMIUM>";							
				}
				
				if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
					|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString())
				{
				
						// premium adjusted after endorsement
						// if procces not committed
						if(gStrtemp!="final")
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<POLPREMIUMADJUSTMENT " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(GetEffectivePremium(dblOldSum,sumTtlC+unattach_prem+(double)SchPrem)))+"</POLPREMIUMADJUSTMENT>";							
						}
							// if proccess committed
						else
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<POLPREMIUMADJUSTMENT " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(dblOldSum))+"</POLPREMIUMADJUSTMENT>";							
						}
				}
				else
				{
					if(dblOldSum != 0 || dblOldSum != 0.00)
					{
						// premium adjusted after endorsement
						// if procces not committed
						if(gStrtemp!="final")
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<POLPREMIUMADJUSTMENT " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(GetEffectivePremium(dblOldSum,sumTtlC+unattach_prem+(double)SchPrem)))+"</POLPREMIUMADJUSTMENT>";							
						}
							// if proccess committed
						else
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<POLPREMIUMADJUSTMENT " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(dblOldSum))+"</POLPREMIUMADJUSTMENT>";							
						}
					}
				}
				#region DecPage CovRoot Element
				XmlElement DecPageCovRootElement;
				DecPageCovRootElement = AcordPDFXML.CreateElement("COVERAGES");
				BoatElementDecPage.AppendChild(DecPageCovRootElement);
				DecPageCovRootElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageCovRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECOVERAGE"));
				DecPageCovRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECOVERAGE"));
				DecPageCovRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECOVERAGEEXTN"));
				DecPageCovRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECOVERAGEEXTN"));
				#endregion

				
				int CovCtr=0;
				//Added by Mohit Agarwal 6-Aug-07 for Endorsements Print Order

				if(gStrPdfFor == PDFForDecPage)
				{
					foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
					{	
						int prnCtr = 0;
						string CovCode=CoverageDetails["COV_CODE"].ToString();
						if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y" && (CovCode=="EBSCEAV" || CovCode=="EBSMECE" || CovCode=="WAT400" || CovCode=="EBSMWL" || CovCode=="BRCC"|| CovCode=="OP300M") && CoverageDetails["ENDORS_PRINT"].ToString() !="N")
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
					// Print order for WP100 will be last because it contains signature
					foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
					{
						int prnCtr = 0;
						string CovCode=CoverageDetails["COV_CODE"].ToString();
						if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y" && (CovCode=="WP100" ) && CoverageDetails["ENDORS_PRINT"].ToString() !="N")
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
					}

				}
				string strDeduct="";
				foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
				{

					
					string covCode = CoverageDetails["COV_CODE"].ToString();
					//Added by asfa (20-Feb-2008) - iTrack issue #3331
					string covPrem="";
					if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
						covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
					else // when gStrtemp != "temp"
						covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
					if(covPrem == "")
						covPrem = "Included";
					else if(covPrem != "Included") 
					{
						covPrem ="$" + covPrem;
					}
					if(CoverageDetails["COV_CODE"].ToString()=="BDEDUC")
					{
						strDeduct= CoverageDetails["DEDUCTIBLE_1"].ToString();
						string grandpercents = CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"].ToString();
						if(grandpercents.IndexOf("%") > 0)
						{
							double granddeduc = GetPrecentDed(grandpercents, BoatDetail["PRESENT_VALUE"].ToString());
							if(granddeduc != 0)
								strDeduct = "$" + DollarFormat(granddeduc);
						}
					}
					//string covCode1 = CoverageDetails1["COV_CODE"].ToString();
					boat_ded = CoverageDetails["DEDUCTIBLE_1"].ToString();
					string grandpercent = CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"].ToString();
					if(grandpercent.IndexOf("%") > 0)
					{
						double granddeduc = GetPrecentDed(grandpercent, BoatDetail["PRESENT_VALUE"].ToString());
						if(granddeduc != 0)
							boat_ded = "$" + DollarFormat(granddeduc);
					}

					if((gStrPdfFor == PDFForDecPage) && ((covCode == "BDEDUC") ||(covCode == "EBPPDACV") || (covCode == "EBPPDAV") || (covCode == "EBPPDJ") || (covCode == "TRAILER") || (covCode == "OUTBOARD1") || (covCode == "OUTBOARD2") || (covCode == "PORTACCESS") || (covCode == "LCCSL") || (covCode == "MCPAY") || (covCode == "UMBCS")))
						continue;

					if(BoatDetail["BOAT_ID"].ToString() != "1" && gStrPdfFor == PDFForDecPage && covCode == "EBIUE")
						continue;

					#region DecPage  Coverage Element 
					XmlElement DecpageCovElement;
					DecpageCovElement = AcordPDFXML.CreateElement("COVERAGEINFO");
					DecPageCovRootElement.AppendChild(DecpageCovElement);
					DecpageCovElement.SetAttribute(fieldType,fieldTypeNormal);
					DecpageCovElement.SetAttribute(id,CovCtr.ToString());
					

					// Boat Deductible "BDEDUC" 
					string strDeductible="0";
					strDeductible=GetDeducWat(DSTempEngineTrailer,"BDEDUC");
					strDeductible=strDeductible.Replace(".00","");
					#endregion
					#region SWITCH CASE
					switch(CoverageDetails["COV_CODE"].ToString())
					{

							
							// mapping for Section 1 - Covered Property Damage - Actual Cash Value
						case "EBPPDACV":
							// mapping for dec page
							
								//break;
							if (gStrPdfFor == PDFForDecPage)
							{
								//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
								//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
								//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
								//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								CovCtr++;
							}
								// mapping for accord page
							else if (gStrPdfFor == PDFForAcord)
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVHULLLIM>";
								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
									strAVLim=CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""); 

								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVHULLDED>";
								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00","").Replace("$",""); 
								
								//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVHULLPREM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVHULLPREM>";
					
								if(htpremium.Contains("BOAT_PD"))
								{
									lstrGetPremium = htpremium["BOAT_PD"].ToString();
									lintGetindex = lstrGetPremium.IndexOf(".");
									if(lintGetindex == -1)
									{
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</BOATCOVHULLPREM>";
									}
									//									else
									//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</BOATCOVHULLPREM>";

									dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
								}
							}
							break;
							// mapping for Section 1 - Covered Property Damage - Agreed Value
						case "EBPPDAV":
							// mapping for  dec page
							if (gStrPdfFor == PDFForDecPage)
							{
								//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
								//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
								//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
								//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								CovCtr++;
							}
								// mapping for accord page
							else if (gStrPdfFor == PDFForAcord)
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVHULLLIM>";
								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
									strAVLim=CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""); 
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVHULLDED>";
								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00","").Replace("$",""); 
								//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVHULLPREM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVHULLPREM>";

								if(htpremium.Contains("BOAT_PD"))
								{	
									lstrGetPremium = htpremium["BOAT_PD"].ToString();
									lintGetindex = lstrGetPremium.IndexOf(".");
									if(lintGetindex ==-1)
									{
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</BOATCOVHULLPREM>";
									}
									//									else
									//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</BOATCOVHULLPREM>";

									dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
								}
							}
							break;
							//
						case "OUTBOARD1":
							// mapping for accord page
							if (gStrPdfFor == PDFForAcord)
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM1LIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVOM1LIM>";
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM1DED " + fieldType +"=\""+ fieldTypeText +"\">"+boat_ded.Replace("$","")+"</BOATCOVOM1DED>";
							}
							// mapping for dec page
							if(gStrPdfFor == PDFForDecPage)
							{
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								CovCtr++;

							}
							break;
						case "OUTBOARD2":
							if (gStrPdfFor == PDFForAcord)
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM2LIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVOM2LIM>";
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM2DED " + fieldType +"=\""+ fieldTypeText +"\">"+boat_ded.Replace(".00","").Replace("$","")+"</BOATCOVOM2DED>";
							}
							break;
						case "PORTACCESS":
							if (gStrPdfFor == PDFForAcord)
							{
								if(BoatCtr == 0)
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVPORTACCLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVPORTACCDED>";
									if(SchPrem !=0)
									{
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCPREM " + fieldType +"=\""+ fieldTypeText +"\">"+ (SchPrem.ToString() + ".00") +"</BOATCOVPORTACCPREM>";
									}
									else
									{
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</BOATCOVPORTACCPREM>";
									}
								}
								
							}
							//							if (gStrPdfFor == PDFForDecPage)
							//							{
							//								//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
							//								//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
							//								//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
							//								//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
							//								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
							//								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
							//									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</BOATCOVLIM>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</BOATCOVDED>";
							//								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
							//								CovCtr++;
							//							}
							break;
						case "TRAILER":
							if (gStrPdfFor == PDFForAcord)
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVTRAILERLIM>";
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$","")) +"</BOATCOVTRAILERDED>";
															
								if(trailertotalprem!=0 && CoverageDetails["LIMIT_1"].ToString() != "")
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERPREM " + fieldType +"=\""+ fieldTypeText +"\">"+ (trailertotalprem.ToString() + ".00") +"</BOATCOVTRAILERPREM>";
								}
								dblSumTotal+=sumTtl;
							}
							if(gStrPdfFor == PDFForDecPage)
							{
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								CovCtr++;

							}
							break;
							// mapping for Boat Replacement Cost
						case "BRCC":
						{
							// mapping for dec page
							if(gStrPdfFor == PDFForDecPage)
							{
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								CovCtr++;

							}
								// mapping for accord page
							else if (gStrPdfFor == PDFForAcord)
							{
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVBRCCDESC>";
								if(CoverageDetails["LIMIT_1"].ToString() !="" && CoverageDetails["LIMIT_1"].ToString() !="0" && CoverageDetails["LIMIT_1"].ToString() !="0.00" && CoverageDetails["LIMIT_1"].ToString() !="Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVBRCCLIMIT>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVBRCCLIMIT>";
								}
								if(boat_ded !="" && boat_ded != "0" && boat_ded != "0.00" && boat_ded !="Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVBRCCDEDUCTIBLE>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVBRCCDEDUCTIBLE>";
								}
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVBRCCPREMIUM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								if(covPrem !="" && covPrem !="0" && covPrem != "0.00" && covPrem !="Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVBRCCPREMIUM>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVBRCCPREMIUM>";
								}
							}
							break;

						}
							// mapping for Watercraft Liability
						case "LCCSL":
							// mapping for dec page
							if (gStrPdfFor == PDFForAcord)
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVCSLLIM>";
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVCSLDED>";
								//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVCSLPREM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() =="Extended from HO")
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+"Extended"+"</BOATCOVCSLPREM>";
								}
								else
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVCSLPREM>";
								}
								if(htpremium.Contains("BOAT_ LIABILITY_PREMIUM"))
								{
									lstrGetPremium = htpremium["BOAT_ LIABILITY_PREMIUM"].ToString();
									lintGetindex = lstrGetPremium.IndexOf(".");
									if(lintGetindex ==-1)
									{
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString() + ".00")+"</BOATCOVCSLPREM>";
									}
									else
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString())+"</BOATCOVCSLPREM>";

										dblSumTotal+=int.Parse(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString());
								}
							}
								// mapping for dec pge
							else if (gStrPdfFor == PDFForDecPage)
							{
								
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIAB " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPERSONALLIAB>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+ " Each Occurence" )+"</BOATCOVPERSONALLIABLIM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVPERSONALLIABPREM>";
								//								if(htpremium.Contains("BOAT_ LIABILITY_PREMIUM"))
								//								{
								//									lstrGetPremium = htpremium["BOAT_ LIABILITY_PREMIUM"].ToString();
								//									lintGetindex   = lstrGetPremium.IndexOf(".");
								//									if(lintGetindex ==-1)
								//									{
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString() + ".00")+"</BOATCOVPERSONALLIABPREM>";
								//									}
								//									else
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString())+"</BOATCOVPERSONALLIABPREM>";
								//								}
								//								
							}
							break;
							// mapping for Section II - Medical
						case "MCPAY":
							// mapping for accord page
							if (gStrPdfFor == PDFForAcord)
							{
								if(CoverageDetails["LIMIT_1"].ToString()!="")
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$","")+"/25,000")+"</BOATCOVMEDPMLIM>";
								}
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVMEDPMDED>";
								//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVMEDPMPREM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() =="Extended from HO")
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+"Extended"+"</BOATCOVMEDPMPREM>";
								
								}
								else
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVMEDPMPREM>";
								}
								if(htpremium.Contains("BOAT_MP_PREMIUM"))
								{
									lstrGetPremium = htpremium["BOAT_MP_PREMIUM"].ToString();
									lintGetindex = lstrGetPremium.IndexOf(".");
									if(lintGetindex ==-1)
									{
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString() + ".00")+"</BOATCOVMEDPMPREM>";
									}
									else
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString())+"</BOATCOVMEDPMPREM>";

										dblSumTotal+=int.Parse(htpremium["BOAT_MP_PREMIUM"].ToString());
								}
							}
								// mapping for de cpage
							else if (gStrPdfFor == PDFForDecPage)
							{
								//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVMEDPM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Medical Payment")+"</BOATCOVMEDPM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString() + " Per Person 25000.00 Each Occurence" )+"</BOATCOVMEDPMLIM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVMEDPMPREM>";
								//								if(htpremium.Contains("BOAT_MP_PREMIUM"))
								//								{
								//									lstrGetPremium = htpremium["BOAT_MP_PREMIUM"].ToString();
								//									lintGetindex = lstrGetPremium.IndexOf(".");
								//									if(lintGetindex ==-1)
								//									{
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString() + ".00")+"</BOATCOVMEDPMPREM>";
								//									}
								//									else
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString())+"</BOATCOVMEDPMPREM>";
								//
								//								}
								//								
							}
							break;
							// mapping for Uninsured Boaters
						case "UMBCS":
							// mapping for accord pge
							if (gStrPdfFor == PDFForAcord)
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$","") )+"</BOATCOVUBCSLLIM>";
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVUBCSLDED>";
								//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVUBCSLPREM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVUBCSLPREM>";
								if(htpremium.Contains("BOAT_UB_PREMIUM"))
								{
									lstrGetPremium = htpremium["BOAT_UB_PREMIUM"].ToString();
									lintGetindex = lstrGetPremium.IndexOf(".");
									if(lintGetindex ==-1)
									{
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString() + ".00")+"</BOATCOVUBCSLPREM>";
									}
									else
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString())+"</BOATCOVUBCSLPREM>";
										dblSumTotal+=int.Parse(htpremium["BOAT_UB_PREMIUM"].ToString());
								}
							}
								// mapping for dec page
							else if (gStrPdfFor == PDFForDecPage)
							{
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVUMBOATERS>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString() + " Each Occurence/Each Unit")+"</BOATCOVUMBOATERSLIM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVUMBOATERSPREM>";
								//								if(htpremium.Contains("BOAT_UB_PREMIUM"))
								//								{	
								//									lstrGetPremium = htpremium["BOAT_UB_PREMIUM"].ToString();
								//									lintGetindex = lstrGetPremium.IndexOf(".");
								//									if(lintGetindex ==-1)
								//									{
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString() + ".00")+"</BOATCOVUMBOATERSPREM>";
								//									}
								//									else
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString())+"</BOATCOVUMBOATERSPREM>";
								//
								//								}
								//								
							}
							break;
							// mapping for Broad Form Watercraft Policy (WP-100)
						case "WP100":
							// mapping for dec page
							if (gStrPdfFor == PDFForDecPage)
							{
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVWCRAFTPOLICYDESC>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYNUM " + fieldType +"=\""+ fieldTypeText +"\">WP-100</BOATCOVWCRAFTPOLICYNUM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYDATE " + fieldType +"=\""+ fieldTypeText +"\">03/01</BOATCOVWCRAFTPOLICYDATE>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYLIM " + fieldType +"=\""+ fieldTypeText +"\"></BOATCOVWCRAFTPOLICYLIM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYDED " + fieldType +"=\""+ fieldTypeText +"\"></BOATCOVWCRAFTPOLICYDED>";
								//								if(htpremium.Count>0)
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVWCRAFTPOLICYPREM>";
								//								
								//								
								//								#region Dec Page Element
								//								if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								//								{
								//
								//									XmlElement DecPageBoatEndoWP100;
								//									DecPageBoatEndoWP100 = AcordPDFXML.CreateElement("BOATENDORSEMENTWP100");
								//									BoatElementDecPage.AppendChild(DecPageBoatEndoWP100);
								//									DecPageBoatEndoWP100.SetAttribute(fieldType,fieldTypeMultiple);
								//									DecPageBoatEndoWP100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEWP100"));
								//									DecPageBoatEndoWP100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100"));
								//									DecPageBoatEndoWP100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWP100EXTN"));
								//									DecPageBoatEndoWP100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100EXTN"));
								//
								//									XmlElement EndoElementWP100;
								//									EndoElementWP100 = AcordPDFXML.CreateElement("EndoElementWP100INFO");
								//									DecPageBoatEndoWP100.AppendChild(EndoElementWP100);
								//									EndoElementWP100.SetAttribute(fieldType,fieldTypeNormal);
								//									EndoElementWP100.SetAttribute(id,BoatCtr.ToString());
								//								}
								//								#endregion
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								string limit="0";
								if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
								{
									limit="Included";
								}
								else
								{
									limit=CoverageDetails["LIMIT_1"].ToString();
								}
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDeduct.Replace(".00",""))+"</BOATCOVDED>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								//								if(htpremium.Count>0)
								//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPREM>";
								
								
								CovCtr++;
							}
								// mapping for accord page
							else if (gStrPdfFor == PDFForAcord)
							{
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVWPDESC>";
								string limit="0";
								if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
								{
									limit="Included";
								}
								else
								{
									limit= "$" + CoverageDetails["LIMIT_1"].ToString();
								}
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVWPLIMIT>";
								if(strDeduct !="" && strDeduct !="0" && strDeduct != "0.00" && strDeduct != "Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDeduct.Replace(".00",""))+"</BOATCOVWPDEDUCTIBLE>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDeduct.Replace(".00","").Replace("$",""))+"</BOATCOVWPDEDUCTIBLE>";
								}
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVWPPREMIUM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								if(covPrem !="" && covPrem !="0" && covPrem != "0.00" && covPrem != "Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters("Included")+"</BOATCOVWPPREMIUM>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVWPPREMIUM>";
								}
							}
							break;
							// mapping for Boat Towing and Emergency Service
						case "BTESC":
							// mapping for dec page
							if (gStrPdfFor == PDFForDecPage)
							{
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVTOWINGDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVTOWINGDESC>";
								//								if(BoatDetail["PRESENT_VALUE"]!= null && BoatDetail["PRESENT_VALUE"].ToString()!="")
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVTOWINGLIM " + fieldType +"=\""+ fieldTypeText +"\">"+( (.05) * double.Parse(BoatDetail["PRESENT_VALUE"].ToString()) + ".00")+"</BOATCOVTOWINGLIM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVTOWINGDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVTOWINGDED>";
								//								if(htpremium.Count>0 )
								//								{
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVTOWINGPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVTOWINGPREM>";
								//								}
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+"N/A"+"</BOATCOVDED>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								//								if(htpremium.Count>0 )
								//								{
								//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPREM>";
								//								}
								CovCtr++;
							}
							break;
							// mapping for Unattached Equipment And Personal Effects
						case "EBIUE":
							// mapping for dec page
							if (gStrPdfFor == PDFForDecPage)
							{
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVUNATTACHEQUIPDESC>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</BOATCOVUNATTACHEQUIPLIM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPDED " + fieldType +"=\""+ fieldTypeText +"\">100.00</BOATCOVUNATTACHEQUIPDED>";
								//								if(htpremium.Contains("BOAT_UNATTACH_PREMIUM"))
								//								{
								//									lstrGetPremium = htpremium["BOAT_UNATTACH_PREMIUM"].ToString();
								//									lintGetindex = lstrGetPremium.IndexOf(".");
								//									if(lintGetindex == -1)
								//									{
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UNATTACH_PREMIUM"].ToString()  + ".00")+"</BOATCOVUNATTACHEQUIPPREM>";
								//									}
								//									else
								//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UNATTACH_PREMIUM"].ToString())+"</BOATCOVUNATTACHEQUIPPREM>";
								//
								//								}
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00",""))+"</BOATCOVDED>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
								CovCtr++;
								string UnattachedPremium="";
								UnattachedPremium =unattach_prem.ToString();
								if(UnattachedPremium == "" || UnattachedPremium == "0" ||  UnattachedPremium == "0.00")
									UnattachedPremium = "Included";
								else if(UnattachedPremium != "Included" && UnattachedPremium.IndexOf(".00")<0) 
								{
									UnattachedPremium ="$" + UnattachedPremium+".00";
								}
								else
								{
									UnattachedPremium ="$" + UnattachedPremium;
								}
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(UnattachedPremium)+"</BOATCOVPREM>";
								//								if(htpremium.Contains("BOAT_UNATTACH_PREMIUM"))
								//								{
								//									lstrGetPremium = htpremium["BOAT_UNATTACH_PREMIUM"].ToString();
								//									lintGetindex = lstrGetPremium.IndexOf(".");
								//									if(lintGetindex == -1)
								//									{
								//										DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UNATTACH_PREMIUM"].ToString()  + ".00")+"</BOATCOVPREM>";
								//									}
								//									else
								//										DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UNATTACH_PREMIUM"].ToString())+"</BOATCOVPREM>";
								//
								//								}								
							}
								// mapping for accord page
							else if (gStrPdfFor == PDFForAcord)
							{
								string Unattachedincl = CoverageDetails["LIMIT_1"].ToString();
								Unattachedincl = Unattachedincl.Replace("$","");
								Unattachedincl = Unattachedincl.Replace(",","");
								Unattachedincl = Unattachedincl.Replace(".00","");

								int UnattachedInc = System.Convert.ToInt32(Unattachedincl) - UnattachedInclude;
								
								if(UnattachedInc!=0)
								{
									Unattachedincl = UnattachedInc.ToString("###,###");
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATUNATTACHED " + fieldType +"=\""+ fieldTypeText +"\">"+"Unattached Equip."+"</BOATUNATTACHED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATUNATTACHEDLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Unattachedincl.ToString().Replace(".00","").Replace("$",""))+"</BOATUNATTACHEDLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATUNATTACHEDDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00","").Replace("$","")) +"</BOATUNATTACHEDDED>";
									
									string UnattachedPremium="";
									UnattachedPremium =unattach_prem.ToString();
									if(UnattachedPremium == "" || UnattachedPremium == "0" ||  UnattachedPremium == "0.00" || UnattachedPremium == "Included")
										UnattachedPremium = "";
									else if(UnattachedPremium != "Included" && UnattachedPremium.IndexOf(".00")<0) 
									{
										UnattachedPremium = UnattachedPremium+".00";
									}
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUNATTACHEDPREM " + fieldType +"=\""+ fieldTypeText +"\">"+ (UnattachedPremium.ToString()) +"</BOATCOVUNATTACHEDPREM>";
								}
							}
							break;
							// mapping for Agreed Value (AV 100)

						case "EBSCEAV":
							// mapping for dec page
							if (gStrPdfFor == PDFForDecPage)
							{
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVAVEDESC>";
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVELIM " + fieldType +"=\""+ fieldTypeText +"\">"+strAVLim+"</BOATCOVAVELIM>";
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEDED " + fieldType +"=\""+ fieldTypeText +"\">"+strAVDed+"</BOATCOVAVEDED>";
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVENUM " + fieldType +"=\""+ fieldTypeText +"\">AV-100</BOATCOVAVENUM>";
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEDATE " + fieldType +"=\""+ fieldTypeText +"\">03/01</BOATCOVAVEDATE>";
								//									if(htpremium.Contains("BOAT_AV100"))
								//									{
								//										lstrGetPremium = htpremium["BOAT_AV100"].ToString();
								//										lintGetindex = lstrGetPremium.IndexOf(".");
								//										if(lintGetindex == -1)
								//										{
								//											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString() + ".00")+"</BOATCOVAVEPREMIUM>";
								//										}
								//										else
								//											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString())+"</BOATCOVAVEPREMIUM>";
								//										
								//									}
								//
								//									#region Dec Page Element
								//									if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								//									{
								//										XmlElement DecPageBoatEndoAV100;
								//										DecPageBoatEndoAV100 = AcordPDFXML.CreateElement("BOATENDORSEMENTAV100");
								//										BoatElementDecPage.AppendChild(DecPageBoatEndoAV100);
								//										DecPageBoatEndoAV100.SetAttribute(fieldType,fieldTypeMultiple);
								//										DecPageBoatEndoAV100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAV100"));
								//										DecPageBoatEndoAV100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100"));
								//										DecPageBoatEndoAV100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAV100EXTN"));
								//										DecPageBoatEndoAV100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100EXTN"));
								//
								//										XmlElement EndoElementAV100;
								//										EndoElementAV100 = AcordPDFXML.CreateElement("EndoElementAV100INFO");
								//										DecPageBoatEndoAV100.AppendChild(EndoElementAV100);
								//										EndoElementAV100.SetAttribute(fieldType,fieldTypeNormal);
								//										EndoElementAV100.SetAttribute(id,BoatCtr.ToString());
								//									}
								//									#endregion
							
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								string limit="0";
								if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
								{
									limit="Included";
								}
								else
								{
									limit=CoverageDetails["LIMIT_1"].ToString();
								}
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDeduct.Replace(".00",""))+"</BOATCOVDED>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
								CovCtr++;
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								//								if(htpremium.Contains("BOAT_AV100"))
								//								{
								//									lstrGetPremium = htpremium["BOAT_AV100"].ToString();
								//									lintGetindex = lstrGetPremium.IndexOf(".");
								//									if(lintGetindex == -1)
								//									{
								//										DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString() + ".00")+"</BOATCOVPREM>";
								//									}
								//									else
								//										DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString())+"</BOATCOVPREM>";
								//									
								//								}

							}
								// mapping for accord page
							else if (gStrPdfFor == PDFForAcord)
							{
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVAVEDESC>";
								string limit="0";
								if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
								{
									limit="Included";
								}
								else
								{
									limit=CoverageDetails["LIMIT_1"].ToString();
								}
								if(limit!="Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVAVELIMIT>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00","").Replace("$",""))+"</BOATCOVAVELIMIT>";
								}
								if(boat_ded !="" && boat_ded !="0" && boat_ded !="0.00")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVAVEDEDUCTIBLE>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVAVEDEDUCTIBLE>";
								}
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVAVEPREMIUM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								if(covPrem != "" && covPrem != "0" && covPrem != "0.00" && covPrem != "Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVAVEPREMIUM>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVAVEPREMIUM>";
								}
								if(htpremium.Contains("BOAT_AV100"))
								{
									lstrGetPremium = htpremium["BOAT_AV100"].ToString();
									lintGetindex = lstrGetPremium.IndexOf(".");
									if(lintGetindex == -1)
									{
										//											BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString() + ".00")+"</BOATCOVAVEPREMIUM>";
									}
									//										else
									//											BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString())+"</BOATCOVAVEPREMIUM>";

								}

							}
								
							
							break;

							// OP 300 coverage 
							/////////////////////
							
						case "OP300M":
							// mapping for dec page
							if (gStrPdfFor == PDFForDecPage)
							{
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								string limit="0";
								if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
								{
									limit="Included";
								}
								else
								{
									limit=CoverageDetails["LIMIT_1"].ToString();
								}
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVDED>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								//								if(htpremium.Count>0)
								//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPREM>";
								CovCtr++;
							}
								// mapping for accord page
							else if (gStrPdfFor == PDFForAcord)
							{
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVOP3DESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVOP3DESC>";
								string limit="0";
								if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
								{
									limit="Included";
								}
								else
								{
									limit=CoverageDetails["LIMIT_1"].ToString();
								}
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVOP3LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00","").Replace("$",""))+"</BOATCOVOP3LIMIT>";
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVOP3DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVOP3DEDUCTIBLE>";
								//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVWPPREMIUM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVOP3PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVOP3PREMIUM>";
							}
							break;
							////////////////////////////
						
							//						case "OP720":
							//							
							//							if (gStrPdfFor == PDFForDecPage)
							//							{
							//								
							//								#region Dec Page Element
							//								if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							//								{
							//
							//									XmlElement DecPageBoatEndo;
							//									DecPageBoatEndo = AcordPDFXML.CreateElement("BOATENDORSEMENT");
							//									BoatElementDecPage.AppendChild(DecPageBoatEndo);
							//									DecPageBoatEndo.SetAttribute(fieldType,fieldTypeMultiple);
							//									DecPageBoatEndo.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP720"));
							//									DecPageBoatEndo.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720"));
							//									DecPageBoatEndo.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP720EXTN"));
							//									DecPageBoatEndo.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720EXTN"));
							//
							//									XmlElement EndoElement;
							//									EndoElement = AcordPDFXML.CreateElement("EndoElement");
							//									DecPageBoatEndo.AppendChild(EndoElement);
							//									EndoElement.SetAttribute(fieldType,fieldTypeNormal);
							//									EndoElement.SetAttribute(id,BoatCtr.ToString());
							//								}
							//								#endregion
							//								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
							//								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
							//									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00",""))+"</BOATCOVDED>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
							//								CovCtr++;
							//							}	
							//							break;	
							// mapping for Client Entertainment Liability (OP 720)
						case "EBSMECE":
							if (gStrPdfFor == PDFForDecPage)
							{
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVCLIENTENTERTAINDESC>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINLIM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVCLIENTENTERTAINLIM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVCLIENTENTERTAINDED>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINNUM " + fieldType +"=\""+ fieldTypeText +"\">OP-720</BOATCOVCLIENTENTERTAINNUM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINDATE " + fieldType +"=\""+ fieldTypeText +"\">05/97</BOATCOVCLIENTENTERTAINDATE>";
								//
								//								if(htpremium.Contains("BOAT_OP720"))
								//								{
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINPREM " + fieldType +"=\""+ fieldTypeText +"\">15.00</BOATCOVCLIENTENTERTAINPREM>";
								//								}								
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								string limit="0";
								if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
								{
									limit="Included";
								}
								else
								{
									limit=CoverageDetails["LIMIT_1"].ToString();
								}
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVLIM>";
															
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+"Included"+"</BOATCOVDED>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								//								if(htpremium.Contains("BOAT_OP720"))
								//								{
								//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">15.00</BOATCOVPREM>";
								//								}									
								CovCtr++;
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVCEDESC>";
								string limit="0";
								if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
								{
									limit="Included";
								}
								else
								{
									limit="$" + CoverageDetails["LIMIT_1"].ToString();
								}
								
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVCELIMIT>";
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVCEDEDUCTIBLE>";
								//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVCEPREMIUM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								if(covPrem !="" && covPrem !="0" && covPrem !="0.00" && covPrem != "Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVCEPREMIUM>";
								}
								else
								{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVCEPREMIUM>";
								}
									if(htpremium.Contains("BOAT_OP720"))
								{
									lstrGetPremium = htpremium["BOAT_OP720"].ToString();
									lintGetindex = lstrGetPremium.IndexOf(".");
									if(lintGetindex == -1)
									{
										//										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP720"].ToString() + ".00")+"</BOATCOVCEPREMIUM>";
									}
									//									else
									//										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP720"].ToString())+"</BOATCOVCEPREMIUM>";

									if(htpremium["BOAT_OP720"].ToString()!=null)
									{
										dblSumUnattach+=int.Parse(htpremium["BOAT_OP720"].ToString());
									}										
								}
							}
							break;
						case "WAT400":
							if (gStrPdfFor == PDFForDecPage)
							{
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPERSONALWCRAFTDESC>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTLIM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPERSONALWCRAFTLIM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVPERSONALWCRAFTDED>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTNUM " + fieldType +"=\""+ fieldTypeText +"\">OP-400</BOATCOVPERSONALWCRAFTNUM>";							
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTDATE " + fieldType +"=\""+ fieldTypeText +"\">05/97</BOATCOVPERSONALWCRAFTDATE>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPERSONALWCRAFTPREM>";
								//								
								//								
								//								#region Dec Page Element
								//								if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
								//								{
								//									XmlElement DecPageBoatEndoOP400;
								//									DecPageBoatEndoOP400 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP400");
								//									BoatElementDecPage.AppendChild(DecPageBoatEndoOP400);
								//									DecPageBoatEndoOP400.SetAttribute(fieldType,fieldTypeMultiple);
								//									DecPageBoatEndoOP400.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP400"));
								//									DecPageBoatEndoOP400.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400"));
								//									DecPageBoatEndoOP400.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP400EXTN"));
								//									DecPageBoatEndoOP400.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400EXTN"));
								//
								//									XmlElement EndoElementOP400;
								//									EndoElementOP400 = AcordPDFXML.CreateElement("EndoElementOP400INFO");
								//									DecPageBoatEndoOP400.AppendChild(EndoElementOP400);
								//									EndoElementOP400.SetAttribute(fieldType,fieldTypeNormal);
								//									EndoElementOP400.SetAttribute(id,BoatCtr.ToString() );
								//								}
								//								#endregion

								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVDED>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								CovCtr++;
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPWEDESC>";
								if(CoverageDetails["LIMIT_1"].ToString()!="" && CoverageDetails["LIMIT_1"].ToString() !="0" && CoverageDetails["LIMIT_1"].ToString() !="0.00" && CoverageDetails["LIMIT_1"].ToString() !="Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVPWELIMIT>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVPWELIMIT>";
								}
								if(boat_ded !="" && boat_ded !="0" && boat_ded != "0.00")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVPWEDEDUCTIBLE>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVPWEDEDUCTIBLE>";
								}
								//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVPWEPREMIUM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								if(covPrem !="0" && covPrem !="0.00" && covPrem !="Included" && covPrem !="")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPWEPREMIUM>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVPWEPREMIUM>";
								}
								//To be Implemented as ITableMapping is only Endorsement
								if(htpremium.Contains("BOAT_OP400"))
								{
									//									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP400"].ToString() + ".00")+"</BOATCOVPWEPREMIUM>";
								}
								//								else 
								//									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPWEPREMIUM>";
							}
							break;
							//						case "OP900":
							//
							//							if (gStrPdfFor == PDFForDecPage)
							//							{
							//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTNUM " + fieldType +"=\""+ fieldTypeText +"\">OP-900</BOATCOVLIABPOLTNUM>";							
							//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTDATE " + fieldType +"=\""+ fieldTypeText +"\">03/06</BOATCOVLIABPOLTDATE>";
							//								#region Dec Page Element
							//								if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
							//								{
							//
							//									XmlElement DecPageBoatEndoOP900;
							//									DecPageBoatEndoOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900");
							//									BoatElementDecPage.AppendChild(DecPageBoatEndoOP900);
							//									DecPageBoatEndoOP900.SetAttribute(fieldType,fieldTypeMultiple);
							//									DecPageBoatEndoOP900.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP900"));
							//									DecPageBoatEndoOP900.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900"));
							//									DecPageBoatEndoOP900.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP900EXTN"));
							//									DecPageBoatEndoOP900.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900EXTN"));
							//
							//									XmlElement EndoElementOP900;
							//									EndoElementOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900INFO");
							//									DecPageBoatEndoOP900.AppendChild(EndoElementOP900);
							//									EndoElementOP900.SetAttribute(fieldType,fieldTypeNormal);
							//
							//									EndoElementOP900.SetAttribute(id,BoatCtr.ToString());
							//								}
							//								#endregion
							//								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
							//								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
							//									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+"N/A"+"</BOATCOVDED>";
							//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
							//								CovCtr++;
							//							}
							//							break;
						case "EBSMWL":
							if (gStrPdfFor == PDFForDecPage)
							{
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVLIABPOLTDESC>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTLIM " + fieldType +"=\""+ fieldTypeText +"\">50000.00</BOATCOVLIABPOLTLIM>";
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVLIABPOLTDED>";
								//								if(htpremium.Contains("BOAT_OP900"))
								//								{
								//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTPREM " + fieldType +"=\""+ fieldTypeText +"\">10.00</BOATCOVLIABPOLTPREM>";
								//								}
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+"N/A"+"</BOATCOVDED>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								//								if(htpremium.Contains("BOAT_OP900"))
								//								{
								//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">10.00</BOATCOVPREM>";
								//								}
								CovCtr++;
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVWLPCDESC>";
								if(CoverageDetails["LIMIT_1"].ToString() != "" || CoverageDetails["LIMIT_1"].ToString() != "0" || CoverageDetails["LIMIT_1"].ToString() != "0.00" || CoverageDetails["LIMIT_1"].ToString()!="Included")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVWLPCLIMIT>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVWLPCLIMIT>";
								}
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVWLPCDEDUCTIBLE>";
								//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVWLPCPREMIUM>";
								//Added by asfa (20-Feb-2008) - iTrack issue #3331
								covPrem="";
								if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
									covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
								else // when gStrtemp != "temp"
									covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
								if(covPrem != "" && covPrem !="Included" && covPrem !="0" && covPrem != "0.00")
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVWLPCPREMIUM>";
								}
								else
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVWLPCPREMIUM>";
								}
								if(htpremium.Contains("BOAT_OP900"))
								{
									lstrGetPremium = htpremium["BOAT_OP900"].ToString();
									lintGetindex = lstrGetPremium.IndexOf(".");
									if(lintGetindex == -1)
									{
										//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP900"].ToString() + ".00")+"</BOATCOVWLPCPREMIUM>";
									}
									//									else
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP900"].ToString())+"</BOATCOVWLPCPREMIUM>";

									dblSumUnattach+=int.Parse(htpremium["BOAT_OP900"].ToString());
								}
							}
							break;
						case "BDEDUC":
							if (gStrPdfFor == PDFForDecPage)
							{
								//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
								//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
								//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
								//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
								// Assign Value of deductible to variable
								
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								CovCtr++;
							}
							break;
						case "EBPPDJ":
							if (gStrPdfFor == PDFForDecPage)
							{
								//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
								//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
								//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
								//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								CovCtr++;
							}
							break;
						default:
							if(gStrPdfFor == PDFForDecPage)
							{
								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								CovCtr++;

							}

							break;
					}
					//					if(gStrPdfFor == PDFForDecPage)
					//					{
					//						int lowestIndex = GetLowestPrnIndex(ref prnOrd, DSTempEngineTrailer.Tables[0].Rows.Count);
					//						string prncovCode = prnOrd_covCode[lowestIndex];
					//						string prnAttFile = prnOrd_attFile[lowestIndex];
					//						switch(prncovCode)
					//						{
					//							case "WP100": 								
					//								#region Dec Page Element
					//								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
					//								{
					//
					//									XmlElement DecPageBoatEndoWP100;
					//									DecPageBoatEndoWP100 = AcordPDFXML.CreateElement("BOATENDORSEMENTWP100");
					//									DecpageCovElement.AppendChild(DecPageBoatEndoWP100);
					//									DecPageBoatEndoWP100.SetAttribute(fieldType,fieldTypeMultiple);
					//									if(prnAttFile != null && prnAttFile.ToString() != "")
					//									{
					//										DecPageBoatEndoWP100.SetAttribute(PrimPDF,prnAttFile.ToString());
					//										DecPageBoatEndoWP100.SetAttribute(PrimPDFBlocks,"1");
					//									}
					//									else
					//									{
					//										DecPageBoatEndoWP100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEWP100"));
					//										DecPageBoatEndoWP100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100"));
					//									}
					//									DecPageBoatEndoWP100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWP100EXTN"));
					//									DecPageBoatEndoWP100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100EXTN"));
					//
					//									XmlElement EndoElementWP100;
					//									EndoElementWP100 = AcordPDFXML.CreateElement("EndoElementWP100INFO");
					//									DecPageBoatEndoWP100.AppendChild(EndoElementWP100);
					//									EndoElementWP100.SetAttribute(fieldType,fieldTypeNormal);
					//									EndoElementWP100.SetAttribute(id,BoatCtr.ToString());
					//								}
					//								#endregion		
					//								break;
					//							case "EBSCEAV": 
					//								#region Dec Page Element
					//								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
					//								{
					//									XmlElement DecPageBoatEndoAV100;
					//									DecPageBoatEndoAV100 = AcordPDFXML.CreateElement("BOATENDORSEMENTAV100");
					//									DecpageCovElement.AppendChild(DecPageBoatEndoAV100);
					//									DecPageBoatEndoAV100.SetAttribute(fieldType,fieldTypeMultiple);
					//									if(prnAttFile != null && prnAttFile.ToString() != "")
					//									{
					//										DecPageBoatEndoAV100.SetAttribute(PrimPDF,prnAttFile.ToString());
					//										DecPageBoatEndoAV100.SetAttribute(PrimPDFBlocks,"1");
					//									}
					//									else
					//									{
					//										DecPageBoatEndoAV100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAV100"));
					//										DecPageBoatEndoAV100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100"));
					//									}
					//									DecPageBoatEndoAV100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAV100EXTN"));
					//									DecPageBoatEndoAV100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100EXTN"));
					//
					//									XmlElement EndoElementAV100;
					//									EndoElementAV100 = AcordPDFXML.CreateElement("EndoElementAV100INFO");
					//									DecPageBoatEndoAV100.AppendChild(EndoElementAV100);
					//									EndoElementAV100.SetAttribute(fieldType,fieldTypeNormal);
					//									EndoElementAV100.SetAttribute(id,BoatCtr.ToString());
					//								}
					//								#endregion				
					//								break;
					//							case "OP720": 
					//								#region Dec Page Element
					//								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
					//								{
					//
					//									XmlElement DecPageBoatEndo;
					//									DecPageBoatEndo = AcordPDFXML.CreateElement("BOATENDORSEMENT");
					//									DecpageCovElement.AppendChild(DecPageBoatEndo);
					//									DecPageBoatEndo.SetAttribute(fieldType,fieldTypeMultiple);
					//									if(prnAttFile != null && prnAttFile.ToString() != "")
					//									{
					//										DecPageBoatEndo.SetAttribute(PrimPDF,prnAttFile.ToString());
					//										DecPageBoatEndo.SetAttribute(PrimPDFBlocks,"1");
					//									}
					//									else
					//									{
					//										DecPageBoatEndo.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP720"));
					//										DecPageBoatEndo.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720"));
					//									}
					//									DecPageBoatEndo.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP720EXTN"));
					//									DecPageBoatEndo.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720EXTN"));
					//
					//									XmlElement EndoElement;
					//									EndoElement = AcordPDFXML.CreateElement("EndoElement");
					//									DecPageBoatEndo.AppendChild(EndoElement);
					//									EndoElement.SetAttribute(fieldType,fieldTypeNormal);
					//									EndoElement.SetAttribute(id,BoatCtr.ToString());
					//								}
					//								#endregion
					//								break;
					//							case "WAT400": 
					//								#region Dec Page Element
					//								//if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
					//								{
					//									XmlElement DecPageBoatEndoOP400;
					//									DecPageBoatEndoOP400 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP400");
					//									DecpageCovElement.AppendChild(DecPageBoatEndoOP400);
					//									DecPageBoatEndoOP400.SetAttribute(fieldType,fieldTypeMultiple);
					//									if(prnAttFile != null && prnAttFile.ToString() != "")
					//									{
					//										DecPageBoatEndoOP400.SetAttribute(PrimPDF,prnAttFile.ToString());
					//										DecPageBoatEndoOP400.SetAttribute(PrimPDFBlocks,"1");
					//									}
					//									else
					//									{
					//										DecPageBoatEndoOP400.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP400"));
					//										DecPageBoatEndoOP400.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400"));
					//									}
					//									DecPageBoatEndoOP400.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP400EXTN"));
					//									DecPageBoatEndoOP400.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400EXTN"));
					//
					//									XmlElement EndoElementOP400;
					//									EndoElementOP400 = AcordPDFXML.CreateElement("EndoElementOP400INFO");
					//									DecPageBoatEndoOP400.AppendChild(EndoElementOP400);
					//									EndoElementOP400.SetAttribute(fieldType,fieldTypeNormal);
					//									EndoElementOP400.SetAttribute(id,BoatCtr.ToString() );
					//								}
					//								#endregion
					//								break;
					//							case "OP900":
					//								#region Dec Page Element
					//								//if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
					//								{
					//
					//									XmlElement DecPageBoatEndoOP900;
					//									DecPageBoatEndoOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900");
					//									DecpageCovElement.AppendChild(DecPageBoatEndoOP900);
					//									DecPageBoatEndoOP900.SetAttribute(fieldType,fieldTypeMultiple);
					//									if(prnAttFile != null && prnAttFile.ToString() != "")
					//									{
					//										DecPageBoatEndoOP900.SetAttribute(PrimPDF,prnAttFile.ToString());
					//										DecPageBoatEndoOP900.SetAttribute(PrimPDFBlocks,"1");
					//									}
					//									else
					//									{
					//										DecPageBoatEndoOP900.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP900"));
					//										DecPageBoatEndoOP900.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900"));
					//									}
					//									DecPageBoatEndoOP900.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP900EXTN"));
					//									DecPageBoatEndoOP900.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900EXTN"));
					//
					//									XmlElement EndoElementOP900;
					//									EndoElementOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900INFO");
					//									DecPageBoatEndoOP900.AppendChild(EndoElementOP900);
					//									EndoElementOP900.SetAttribute(fieldType,fieldTypeNormal);
					//
					//									EndoElementOP900.SetAttribute(id,BoatCtr.ToString());
					//								}
					//								#endregion
					//								break;
					//							default: break;
					//						}
					//					}
					#endregion

					
				}

				if(DSTempEngineTrailer.Tables[0].Rows.Count>0) 
				{
					if (gStrPdfFor == PDFForAcord)
					{
//						if(BoatCtr == 0)
//							sumTtlE += SchPrem;
						if(sumTtlE !=0)
						{
							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVENDMTPREM " + fieldType +"=\""+ fieldTypeText +"\">"+(sumTtlE + ".00")+"</BOATCOVENDMTPREM>";
						}
						else
						{
							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVENDMTPREM " + fieldType +"=\""+ fieldTypeText +"\">"+""+"</BOATCOVENDMTPREM>";
						}
						dblSumTotal+=dblSumUnattach;
					}
				}


				if (gStrPdfFor == PDFForAcord)
				{
					if(BoatCtr == 0)
					{
						sumTtlB += SchPrem;
						sumTtlB += unattach_prem;
					}
					if(gStrtemp!="final")
					{
						sumTtlB += Accordtrailertotalprem;
					}
					BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTTOTAL " + fieldType +"=\""+ fieldTypeText +"\">"+(sumTtlB.ToString()  + ".00" )+"</BOATCOVTTOTAL>";
					//					if(htpremium.Contains("SUMTOTAL"))
					//					{
					//						if (gStrPdfFor == PDFForDecPage)
					//						{
					//						
					////							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALPOLPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+(dblSumTotal.ToString()  + ".00" )+"</TOTALPOLPREMIUM>";							
					//						}
					//						else if (gStrPdfFor == PDFForAcord)
					//						{
					//							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTTOTAL " + fieldType +"=\""+ fieldTypeText +"\">"+(dblSumTotal.ToString()  + ".00" )+"</BOATCOVTTOTAL>";
					//						}
					//					}
					
				}
				dblSumTotal=0;
				dblSumUnattach=0;
				#endregion

				if (gStrPdfFor == PDFForDecPage)
				{
					#region operator Dec Page

					#region setting Root Operate Attribute
					XmlElement OperaterRootElement;
					OperaterRootElement = AcordPDFXML.CreateElement("OPERATOR");
					BoatElementDecPage.AppendChild(OperaterRootElement);
					OperaterRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					OperaterRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOPERATOR"));
					OperaterRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOR"));
					OperaterRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOPERATOREXTN"));
					OperaterRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOREXTN"));

					
					#endregion
					
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@VEHICLEID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("Proc_GetPDFOperatorDtls");
					gobjWrapper.ClearParameteres();

					//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "',"  +BoatDetail["BOAT_ID"] );
					foreach(DataRow OperatorDetail in DSTempEngineTrailer.Tables[0].Rows)
					{
						XmlElement OperatorElement;
						OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
						OperaterRootElement.AppendChild(OperatorElement);
						OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
						OperatorElement.SetAttribute(id,Eng_tra_Ctr.ToString());

						OperatorElement.InnerXml= OperatorElement.InnerXml +  "<OPERATORNAME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_NAME"].ToString())+"</OPERATORNAME>"; 
						OperatorElement.InnerXml= OperatorElement.InnerXml +  "<OPERATORLICENSENUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DRIV_LIC"].ToString())+"</OPERATORLICENSENUM>"; 
						OperatorElement.InnerXml= OperatorElement.InnerXml +  "<OPERATORDATEOBIRTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DOB"].ToString())+"</OPERATORDATEOBIRTH>"; 
						OperatorElement.InnerXml= OperatorElement.InnerXml +  "<OPERATOR_RATED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["ASSIGNED_ASS"].ToString())+"</OPERATOR_RATED>"; 

						Eng_tra_Ctr++;
					}
					Eng_tra_Ctr=0;
					#endregion
				}

				#region Additional Int
				XmlElement AddlIntDecRootElement;
				AddlIntDecRootElement = AcordPDFXML.CreateElement("ADDITIONALINT");
				if (gStrPdfFor == PDFForDecPage)
				{
					#region setting AddlInt DecRootElement Attribute
					BoatElementDecPage.AppendChild(AddlIntDecRootElement);
					AddlIntDecRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					AddlIntDecRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDLINT"));
					AddlIntDecRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINT"));
					AddlIntDecRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEADDLINTEXTN"));
					AddlIntDecRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTEXTN"));
					#endregion
				}

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
				gobjWrapper.ClearParameteres();

				//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + "," + BoatDetail["BOAT_ID"] + ",'" + gStrCalledFrom + "'");
				foreach(DataRow AddlIntDetails in DSTempEngineTrailer.Tables[0].Rows)
				{
					if (gStrPdfFor == PDFForDecPage)
					{
						#region AddlInt Dec Page
						XmlElement AddlIntDecElement;
						AddlIntDecElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");
						AddlIntDecRootElement.AppendChild(AddlIntDecElement);
						AddlIntDecElement.SetAttribute(fieldType,fieldTypeNormal);
						AddlIntDecElement.SetAttribute(id,Eng_tra_Ctr.ToString());

						//Added Mohit Agarwal 29-Aug ITrack 2440
						//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLSERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_ID_NO"].ToString())+"</ADDLSERIAL>"; 
						//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MAKE_MODEL"].ToString())+"</ADDLDESC>"; 
						//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString())+"</ADDLLENGTH>"; 
						//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLHP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HORSE_POWER"].ToString())+"</ADDLHP>"; 
						//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLCONS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_MATERIAL"].ToString())+"</ADDLCONS>"; 
						//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLUSE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["WATERS_NAVIGATED_USE"].ToString())+"</ADDLUSE>"; 
						//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLTERR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_TERRITORY"].ToString())+"</ADDLTERR>"; 

						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["BOAT_NO"].ToString())+"</ADDLINTNUM>"; 
						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDER_NAME"].ToString() + ", " + AddlIntDetails["ADDRESS"].ToString() +""+ AddlIntDetails["CITYSTATEZIP"].ToString())+"</ADDLINTNAME>"; 
						//AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["ADDRESS"].ToString())+"</ADDLINTADDRESS>"; 
						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTNATUREINTEREST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["ADDLINTNAME"].ToString())+"</ADDLINTNATUREINTEREST>"; 
						if(AddlIntDetails["LOAN_REF_NUMBER"].ToString()!="")
						{
							AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTLOANNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString() + "/" + AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</ADDLINTLOANNUM>"; 
						}
						else
						{
							AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTLOANNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["BOAT_NO"].ToString())+"</ADDLINTLOANNUM>"; 
						}
						if(AddlIntDetails["BOAT_TRAILER"].ToString() != "2")
							AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOATTYPE"].ToString())+"</ADDLINTTYPE>"; 
						else
							AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Trailer")+"</ADDLINTTYPE>"; 
							
						Eng_tra_Ctr++;
						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region AddlInt Supplement Page
						if(AddlIntDetails["BOAT_TRAILER"].ToString() == "2")
						{
							BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<ADDLINTNATUREINT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["NATURE_OF_INTEREST"].ToString())+"</ADDLINTNATUREINT>"; 
							BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<ADDINTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDER_NAME"].ToString()) + ", " + RemoveJunkXmlCharacters(AddlIntDetails["ADDRESS"].ToString()) +"</ADDINTNAME>"; 
							BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<ADDINTADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["CITYSTATEZIP"].ToString())+"</ADDINTADDRESS>"; 
							BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<ADDINTLOANNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</ADDINTLOANNUMBER>"; 
							if(BoatDetail["BOAT_MODEL"].ToString()!="")
							{
								BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<BOATYEARMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString()+"/"+BoatDetail["BOAT_MAKE"].ToString()+"/"+BoatDetail["BOAT_MODEL"].ToString())+"</BOATYEARMAKEMODEL>"; 
							}
							else
							{
								BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<BOATYEARMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString()+"/"+BoatDetail["BOAT_MAKE"].ToString())+"</BOATYEARMAKEMODEL>"; 
							}
								break;
						}
						#endregion
					}
				}
				Eng_tra_Ctr=0;	

				#endregion Additional Int

				#region Creating Credit And Surcharge Xml
				if (isRateGenerated)
				{
					int CreditSurchRowCounter = 0;
					string AdditionalExtnTxt="";

					#region Credits
					XmlElement DecPageBoatCredit;
					DecPageBoatCredit = AcordPDFXML.CreateElement("BOATCREDIT");

					XmlElement SupplementBoatCredit;
					SupplementBoatCredit = AcordPDFXML.CreateElement("BOATCREDIT");

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
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Supplement Page Element
						BoatElementSuppliment.AppendChild(SupplementBoatCredit);
						SupplementBoatCredit.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementBoatCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDIT"));
						SupplementBoatCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDIT"));
						SupplementBoatCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDITEXTN"));
						SupplementBoatCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));
						#endregion
					}

					htpremium_dis.Clear(); 
					foreach (XmlNode CreditNode in GetCredits(BoatDetail["BOAT_ID"].ToString()))
					{
						if(!htpremium_dis.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
							htpremium_dis.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
						else
							continue;

						string component_code = getAttributeValue(CreditNode,"COMPONENT_CODE");
						string creditdesc = getAttributeValue(CreditNode,"STEPDESC");
						if(creditdesc.Replace(" ","").IndexOf("+0%") > 0 || creditdesc.Replace(" ","").IndexOf("-0%") > 0)
							creditdesc = "";

						if(component_code != "D_MULTIPOL" && component_code != "D_DUC" && creditdesc == "")
							continue;

						if(getAttributeValue(CreditNode,"STEPPREMIUM").Trim() == "0")
							continue;

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page
							XmlElement DecPageBoatCreditElement;
							DecPageBoatCreditElement = AcordPDFXML.CreateElement("BOATCREDITINFO");
							DecPageBoatCredit.AppendChild(DecPageBoatCreditElement);
							DecPageBoatCreditElement.SetAttribute(fieldType,fieldTypeNormal);
							DecPageBoatCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());

							//							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_MULTIPOL")
							//								DecPageBoatCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">" + BoatDetail["DESC_MULTI_POLICY_DISC_APPLIED"].ToString().Trim()  +"</CREDITDISC>"; 
							//							else if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_DUC") 
							//								DecPageBoatCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+ BoatDetail["DWELLING_CONST_DATE"].ToString().Trim()  +"</CREDITDISC>"; 
							//							else
							DecPageBoatCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Discount - ","").Trim()) +"</CREDITDISC>"; 

							//							DecPageBoatCreditElement.InnerXml += "<CREDITDISCPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</CREDITDISCPREM>"; 
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page
							XmlElement SupplementBoatCreditElement;
							SupplementBoatCreditElement = AcordPDFXML.CreateElement("BOATCREDITINFO");
							SupplementBoatCredit.AppendChild(SupplementBoatCreditElement);
							SupplementBoatCreditElement.SetAttribute(fieldType,fieldTypeNormal);
							SupplementBoatCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
							if (BoatCtr%2 == 0)
								AdditionalExtnTxt="";
							else
								AdditionalExtnTxt="1";

							SupplementBoatCreditElement.InnerXml += "<CREDITDISC" + AdditionalExtnTxt + " " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Discount - ","").Trim())+"</CREDITDISC" + AdditionalExtnTxt + ">"; 
							SupplementBoatCreditElement.InnerXml += "<CREDITDISCPREM" + AdditionalExtnTxt + " " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</CREDITDISCPREM" + AdditionalExtnTxt + ">"; 
							#endregion
						}

						CreditSurchRowCounter++;
					}

					#region Surcharges
					htpremium_sur.Clear(); 
					foreach (XmlNode CreditNode in GetSurcharges(BoatDetail["BOAT_ID"].ToString()))
					{
						if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
							htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
						else
							continue;

						string surcdesc = getAttributeValue(CreditNode,"STEPDESC");
						if(surcdesc.Replace(" ","").IndexOf("+0%") > 0 || surcdesc.Replace(" ","").IndexOf("-0%") > 0)
							continue;

						if(getAttributeValue(CreditNode,"STEPPREMIUM").Trim() == "0")
							continue;

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page
							XmlElement DecPageBoatSurchElement;
							DecPageBoatSurchElement = AcordPDFXML.CreateElement("BOATCREDITINFO");
							DecPageBoatCredit.AppendChild(DecPageBoatSurchElement);
							DecPageBoatSurchElement.SetAttribute(fieldType,fieldTypeNormal);
							DecPageBoatSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
	
							DecPageBoatSurchElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Charge - ","").Replace(" for Unrelated parties","").Trim())+"</CREDITDISC>";
							//								if(getAttributeValue(CreditNode,"STEPPREMIUM") == "0")
							//									DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</SURCHARGEPREM>"; 
							//								else
							//									DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM>"; 
							#endregion
						}
						CreditSurchRowCounter++;
					}

					#endregion Surcharges

					#endregion

					CreditSurchRowCounter = 0;
					AdditionalExtnTxt="";

					#region Surcharges
					//					XmlElement DecPageBoatSurch;
					//					DecPageBoatSurch = AcordPDFXML.CreateElement("BOATSURCHARGE");

					XmlElement SupplementBoatSurch;
					SupplementBoatSurch = AcordPDFXML.CreateElement("BOATSURCHARGE");

					if (gStrPdfFor == PDFForDecPage)
					{
						//						#region Dec Page Element
						//						BoatElementDecPage.AppendChild(DecPageBoatSurch);
						//						DecPageBoatSurch.SetAttribute(fieldType,fieldTypeMultiple);
						//						DecPageBoatSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
						//						DecPageBoatSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
						//						DecPageBoatSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
						//						DecPageBoatSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));
						//						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Supplement Page Element
						BoatElementSuppliment.AppendChild(SupplementBoatSurch);
						SupplementBoatSurch.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementBoatSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHG"));
						SupplementBoatSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHG"));
						SupplementBoatSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHGEXTN"));
						SupplementBoatSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHGEXTN"));
						#endregion
					}

					htpremium_sur.Clear(); 
					foreach (XmlNode CreditNode in GetSurcharges(BoatDetail["BOAT_ID"].ToString()))
					{
						if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
							htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
						else
							continue;

						string surcdesc = getAttributeValue(CreditNode,"STEPDESC");
						if(surcdesc.Replace(" ","").IndexOf("+0%") > 0 || surcdesc.Replace(" ","").IndexOf("-0%") > 0)
							continue;

						if(getAttributeValue(CreditNode,"STEPPREMIUM").Trim() == "0")
							continue;

						if (gStrPdfFor == PDFForDecPage)
						{
							//							#region Dec Page
							//							XmlElement DecPageBoatSurchElement;
							//							DecPageBoatSurchElement = AcordPDFXML.CreateElement("BOATSURCHARGEINFO");
							//							DecPageBoatSurch.AppendChild(DecPageBoatSurchElement);
							//							DecPageBoatSurchElement.SetAttribute(fieldType,fieldTypeNormal);
							//							DecPageBoatSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
							//
							//							DecPageBoatSurchElement.InnerXml += "<SURCHARGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Charge - ","").Replace(" for Unrelated parties","").Trim())+"</SURCHARGE>";
							//							if(getAttributeValue(CreditNode,"STEPPREMIUM") == "0")
							//								DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</SURCHARGEPREM>"; 
							//							else
							//								DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM>"; 
							//							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page
							XmlElement SupplementBoatSurchElement;
							SupplementBoatSurchElement = AcordPDFXML.CreateElement("BOATSURCHARGEINFO");
							SupplementBoatSurch.AppendChild(SupplementBoatSurchElement);
							SupplementBoatSurchElement.SetAttribute(fieldType,fieldTypeNormal);
							SupplementBoatSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());

							if (BoatCtr%2 == 0)
								AdditionalExtnTxt="";
							else
								AdditionalExtnTxt="1";

							SupplementBoatSurchElement.InnerXml += "<SURCHARGE" + AdditionalExtnTxt + " " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Charge - ","").Replace(" for Unrelated parties","").Trim())+"</SURCHARGE" + AdditionalExtnTxt + ">"; 
							SupplementBoatSurchElement.InnerXml += "<SURCHARGEPREM" + AdditionalExtnTxt + " " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM" + AdditionalExtnTxt + ">"; 
							#endregion
						}
						CreditSurchRowCounter++;
					}
					#endregion
				}
				#endregion
				
				BoatCtr++;
				Boatpage++;
				//Reset Total Policy premium Variable			
				if(gStrtemp!="final")
				{
					sumTtlC=0;
				}
				#endregion
			}

		}
		

		#endregion

		#region Code for Boat Trailer Addl Interests
		private void createAcord82BoatAddlIntXml()
		{
			if (gStrPdfFor == PDFForAcord)
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

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempDataSet1 = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempDataSet1 = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
				foreach(DataRow BoatDetail in DSTempDataSet1.Tables[0].Rows)
				{
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
					gobjWrapper.ClearParameteres();

					//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + BoatDetail["BOAT_ID"].ToString() + ",'" + gStrCalledFrom + "'");
			
					#region Acord82 Page
					//					if (DSTempDataSet.Tables[0].Rows.Count >0)
				{
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord82ADDLINTElement;
						Acord82ADDLINTElement = AcordPDFXML.CreateElement("ADDLINTINFO");
						Acord82AddlInts.AppendChild(Acord82ADDLINTElement);
						Acord82ADDLINTElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord82ADDLINTElement.SetAttribute(id,RowCounter.ToString());
						
						string strAddress=Row["ACCORDADDRESS"].ToString();
						
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTTYPEOFINT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString()) + "</ADDLINTTYPEOFINT>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HOLDER_NAME"].ToString()) + ", " +  RemoveJunkXmlCharacters(strAddress) + "</ADDLINTNAME>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CITYSTATEZIP"].ToString()) + "</ADDLINTADD>";
						if(Row["LOAN_REF_NUMBER"].ToString()!="")
						{
							Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTLOANNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["BOAT_NO"].ToString() + "/" + Row["LOAN_REF_NUMBER"].ToString()) + "</ADDLINTLOANNUM>";
						}
						else
						{
							Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTLOANNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["BOAT_NO"].ToString() )+ "</ADDLINTLOANNUM>";
						}
						if(BoatDetail["BOAT_MODEL"].ToString()!="")
						{
							Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +"<BOATYEARMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString()+"/"+BoatDetail["BOAT_MAKE"].ToString()+"/"+BoatDetail["BOAT_MODEL"].ToString())+"</BOATYEARMAKEMODEL>"; 
						}
						else
						{
							Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +"<BOATYEARMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString()+"/"+BoatDetail["BOAT_MAKE"].ToString())+"</BOATYEARMAKEMODEL>"; 
						}
						RowCounter++;
					}
				}
				}
				#endregion
			}
		}
		#endregion

		#region Creating Data For Prior Loss
		private void CreatePriorPolicyXml()
		{
			if (gStrPdfFor==PDFForAcord)
			{

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@LOBCODE","BOAT");
				gobjWrapper.AddParameter("@DATAFOR","LOSS");
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails");
				gobjWrapper.ClearParameteres();


				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails " + gStrClientID + ",'BOAT','LOSS'");
				if (DSTempDataSet.Tables[0].Rows.Count>0)
				{
					XmlElement Acord82PriorLoss;
					Acord82PriorLoss = AcordPDFXML.CreateElement("PRIORLOSSCOVERAGE");
					Acord82RootElement.AppendChild(Acord82PriorLoss);
					Acord82PriorLoss.SetAttribute(fieldType,fieldTypeMultiple);
					Acord82PriorLoss.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82LOSSHIST"));
					Acord82PriorLoss.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82LOSSHIST"));
					Acord82PriorLoss.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82LOSSHISTEXTN"));
					Acord82PriorLoss.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82LOSSHISTEXTN"));

					int RowCounter=0;
					foreach (DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord82PriorLossElement;
						Acord82PriorLossElement =  AcordPDFXML.CreateElement("COAPPLICANT");
						Acord82PriorLoss.AppendChild(Acord82PriorLossElement);
						Acord82PriorLossElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord82PriorLossElement.SetAttribute(id,RowCounter.ToString());

						Acord82PriorLossElement.InnerXml = Acord82PriorLossElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</APPLICANTNAME>";
						Acord82PriorLossElement.InnerXml = Acord82PriorLossElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
						Acord82PriorLossElement.InnerXml = Acord82PriorLossElement.InnerXml +  "<LOSSHISTDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OCCURENCE_DATE"].ToString()) + "</LOSSHISTDATE>";
						Acord82PriorLossElement.InnerXml = Acord82PriorLossElement.InnerXml +  "<LOSSHISTTYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOSS_TYPE"].ToString()) + "</LOSSHISTTYPE>";
						Acord82PriorLossElement.InnerXml = Acord82PriorLossElement.InnerXml +  "<LOSSHISTDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOSS_DESC"].ToString()) + "</LOSSHISTDESCRIPTION>";
						if(Row["AMOUNT"].ToString()!="")
						{
							Acord82PriorLossElement.InnerXml = Acord82PriorLossElement.InnerXml +  "<LOSSHISTAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(Row["AMOUNT"].ToString()) + "</LOSSHISTAMOUNT>";
						}
						else
						{
							Acord82PriorLossElement.InnerXml = Acord82PriorLossElement.InnerXml +  "<LOSSHISTAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</LOSSHISTAMOUNT>";
						}
						RowCounter++;
					}
					
				}
			}
		}
		#endregion

		
		//by prAVESH ON 21 MARCH 2008 FOR REMOVING THOSE ENDORSEMENT WHICH HAS NOT BEEN CHANGED AT ENDORSEMENT /RENEWAL PROCESS
		private void RemoveEnorsementWordings()
		{
			try
			{
				if (prnOrd_covCode==null) return;
				DataSet dsBoats = new DataSet();

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				dsBoats = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
				gobjWrapper.ClearParameteres();

				//dsBoats = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			
				foreach(DataRow BoatDetail in dsBoats.Tables[0].Rows)
				{

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSNewCoverageEndorsemet = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
					gobjWrapper.ClearParameteres();

					//DataSet DSNewCoverageEndorsemet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
					
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",goldVewrsionId);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSOldCoverageEndorsemet = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
					gobjWrapper.ClearParameteres();

					//DataSet DSOldCoverageEndorsemet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + goldVewrsionId + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				
					foreach ( DataRow NewEndDetails in DSNewCoverageEndorsemet.Tables[0].Rows)
					{
						string CovCode=NewEndDetails["COV_CODE"].ToString();
						int counter = 0;
						DataRow[] drOld= DSOldCoverageEndorsemet.Tables[0].Select("COV_CODE='" + CovCode + "' AND ENDORS_ASSOC_COVERAGE='Y'");
						if( drOld.Length>0)
						{
							//if (drOld[0]["LIMIT_1"].ToString()==NewEndDetails["LIMIT_1"].ToString() && drOld[0]["EDITION_DATE"].ToString()==NewEndDetails["EDITION_DATE"].ToString() && drOld[0]["DEDUCTIBLE_1"].ToString()==NewEndDetails["DEDUCTIBLE_1"].ToString())
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
		#region Endorsement Wordings
		private void createEndorsementWordings()
		{
			if(gStrPdfFor == PDFForDecPage)
			{
				int endorsCount = 0;
				int counter = 0,Cntrl=0;
				int BlankPagePrinted=0;
				noBoats +=intschEquip;
				noBoats += intPrivacyPage;
				//check for even and odd number of pages
				noBoats = noBoats%2;
				//					//while(prnOrd_covCode[endorsCount] != null)
				//		endorsCount++;
				endorsCount=prnOrd_covCode.Length;

				while(counter < endorsCount)
				{
					//if (prnOrd_covCode[counter]==null) { counter++;continue; }
					int lowestIndex = GetLowestPrnIndex(ref prnOrd, endorsCount);
					string prncovCode = "";
					if (prnOrd_covCode[lowestIndex]!=null)
						prncovCode=prnOrd_covCode[lowestIndex];
					//string prncovCode = prnOrd_covCode[lowestIndex];
					string prnAttFile = prnOrd_attFile[lowestIndex];
					if(prnAttFile != null && prnAttFile != ""  && prncovCode!="")
					{
						XmlElement DecPageBoatEndoW;
						XmlElement EndoElementW;
						if(BlankPagePrinted==0 && noBoats!=0)
						{
							DecPageBoatEndoW = AcordPDFXML.CreateElement("BOATENDORSEMENTWP" + "_" + 0);
							DecPageRootElement.AppendChild(DecPageBoatEndoW);
							DecPageBoatEndoW.SetAttribute(fieldType,fieldTypeMultiple);

							DecPageBoatEndoW.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBLANKDOCUMENT"));
							DecPageBoatEndoW.SetAttribute(PrimPDFBlocks,"1");

							DecPageBoatEndoW.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBLANKDOCUMENT"));
							DecPageBoatEndoW.SetAttribute(SecondPDFBlocks,"1");

						
							EndoElementW = AcordPDFXML.CreateElement("ENDOELEMENTWINFO" + "_" + 0);
							DecPageBoatEndoW.AppendChild(EndoElementW);
							EndoElementW.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementW.SetAttribute(id,"0");
							BlankPagePrinted++;
							//counter++;
						}
						
						if(noBoats!=0)
						{
							Cntrl=counter+1;
							DecPageBoatEndoW = AcordPDFXML.CreateElement("BOATENDORSEMENTWP" + "_" + Cntrl);
							DecPageRootElement.AppendChild(DecPageBoatEndoW);
							DecPageBoatEndoW.SetAttribute(fieldType,fieldTypeMultiple);

							DecPageBoatEndoW.SetAttribute(PrimPDF,prnAttFile);
							DecPageBoatEndoW.SetAttribute(PrimPDFBlocks,"1");

							DecPageBoatEndoW.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWWORDEXTN"));
							DecPageBoatEndoW.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWWORDEXTN"));

							EndoElementW = AcordPDFXML.CreateElement("ENDOELEMENTWINFO" + "_" + Cntrl);
							DecPageBoatEndoW.AppendChild(EndoElementW);
							EndoElementW.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementW.SetAttribute(id,"0");
						}
						else
						{
							DecPageBoatEndoW = AcordPDFXML.CreateElement("BOATENDORSEMENTWP" + "_" + counter);
							DecPageRootElement.AppendChild(DecPageBoatEndoW);
							DecPageBoatEndoW.SetAttribute(fieldType,fieldTypeMultiple);

							DecPageBoatEndoW.SetAttribute(PrimPDF,prnAttFile);
							DecPageBoatEndoW.SetAttribute(PrimPDFBlocks,"1");

							DecPageBoatEndoW.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWWORDEXTN"));
							DecPageBoatEndoW.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWWORDEXTN"));

							EndoElementW = AcordPDFXML.CreateElement("ENDOELEMENTWINFO" + "_" + counter);
							DecPageBoatEndoW.AppendChild(EndoElementW);
							EndoElementW.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementW.SetAttribute(id,"0");
						}
						
					}
					counter++;
				}
				#region CommentedCode
				/*switch(prncovCode)
					{
						case "WP100": 								
				#region Dec Page Element
							//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						{

							
							DecPageBoatEndoWP100 = AcordPDFXML.CreateElement("BOATENDORSEMENTWP100");
							DecPageRootElement.AppendChild(DecPageBoatEndoWP100);
							DecPageBoatEndoWP100.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageBoatEndoWP100.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageBoatEndoWP100.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageBoatEndoWP100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEWP100"));
								DecPageBoatEndoWP100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100"));
							}
							DecPageBoatEndoWP100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWP100EXTN"));
							DecPageBoatEndoWP100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100EXTN"));

							XmlElement EndoElementWP100;
							EndoElementWP100 = AcordPDFXML.CreateElement("EndoElementWP100INFO");
							DecPageBoatEndoWP100.AppendChild(EndoElementWP100);
							EndoElementWP100.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementWP100.SetAttribute(id,"0");
						}
				#endregion		
							break;
						case "EBSCEAV": 
				#region Dec Page Element
							//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						{
							XmlElement DecPageBoatEndoAV100;
							DecPageBoatEndoAV100 = AcordPDFXML.CreateElement("BOATENDORSEMENTAV100");
							DecPageRootElement.AppendChild(DecPageBoatEndoAV100);
							DecPageBoatEndoAV100.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageBoatEndoAV100.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageBoatEndoAV100.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageBoatEndoAV100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAV100"));
								DecPageBoatEndoAV100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100"));
							}
							DecPageBoatEndoAV100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAV100EXTN"));
							DecPageBoatEndoAV100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100EXTN"));

							XmlElement EndoElementAV100;
							EndoElementAV100 = AcordPDFXML.CreateElement("EndoElementAV100INFO");
							DecPageBoatEndoAV100.AppendChild(EndoElementAV100);
							EndoElementAV100.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementAV100.SetAttribute(id,"0");
						}
				#endregion				
							break;
						case "EBSMECE": 
				#region Dec Page Element
							//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						{

							XmlElement DecPageBoatEndo;
							DecPageBoatEndo = AcordPDFXML.CreateElement("BOATENDORSEMENT");
							DecPageRootElement.AppendChild(DecPageBoatEndo);
							DecPageBoatEndo.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageBoatEndo.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageBoatEndo.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageBoatEndo.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP720"));
								DecPageBoatEndo.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720"));
							}
							DecPageBoatEndo.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP720EXTN"));
							DecPageBoatEndo.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720EXTN"));

							XmlElement EndoElement;
							EndoElement = AcordPDFXML.CreateElement("EndoElement");
							DecPageBoatEndo.AppendChild(EndoElement);
							EndoElement.SetAttribute(fieldType,fieldTypeNormal);
							EndoElement.SetAttribute(id,"0");
						}
				#endregion
							break;
						case "WAT400": 
				#region Dec Page Element
							//if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
						{
							XmlElement DecPageBoatEndoOP400;
							DecPageBoatEndoOP400 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP400");
							DecPageRootElement.AppendChild(DecPageBoatEndoOP400);
							DecPageBoatEndoOP400.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageBoatEndoOP400.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageBoatEndoOP400.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageBoatEndoOP400.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP400"));
								DecPageBoatEndoOP400.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400"));
							}
							DecPageBoatEndoOP400.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP400EXTN"));
							DecPageBoatEndoOP400.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400EXTN"));

							XmlElement EndoElementOP400;
							EndoElementOP400 = AcordPDFXML.CreateElement("EndoElementOP400INFO");
							DecPageBoatEndoOP400.AppendChild(EndoElementOP400);
							EndoElementOP400.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementOP400.SetAttribute(id,"0");
						}
				#endregion
							break;
						case "EBSMWL":
				#region Dec Page Element
							//if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
						{

							XmlElement DecPageBoatEndoOP900;
							DecPageBoatEndoOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900");
							DecPageRootElement.AppendChild(DecPageBoatEndoOP900);
							DecPageBoatEndoOP900.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageBoatEndoOP900.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageBoatEndoOP900.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageBoatEndoOP900.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP900"));
								DecPageBoatEndoOP900.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900"));
							}
							DecPageBoatEndoOP900.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP900EXTN"));
							DecPageBoatEndoOP900.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900EXTN"));

							XmlElement EndoElementOP900;
							EndoElementOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900INFO");
							DecPageBoatEndoOP900.AppendChild(EndoElementOP900);
							EndoElementOP900.SetAttribute(fieldType,fieldTypeNormal);

							EndoElementOP900.SetAttribute(id,"0");
						}
							break;
							//OP-300 File Attachment
						case "OP300M":
							
							//if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
						{

							XmlElement DecPageBoatEndoOP300;
							DecPageBoatEndoOP300 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP300");
							DecPageRootElement.AppendChild(DecPageBoatEndoOP300);
							DecPageBoatEndoOP300.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageBoatEndoOP300.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageBoatEndoOP300.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageBoatEndoOP300.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP300"));
								DecPageBoatEndoOP300.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP300"));
							}
							DecPageBoatEndoOP300.SetAttribute(SecondPDF,"0");
							DecPageBoatEndoOP300.SetAttribute(SecondPDFBlocks,"0");
							
							XmlElement EndoElementOP300;
							EndoElementOP300 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP300INFO");
							DecPageBoatEndoOP300.AppendChild(EndoElementOP300);
							EndoElementOP300.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementOP300.SetAttribute(id,"0");
						}
				#endregion
							break;
						default: break;
					}
					counter++;*/
				#endregion 
			}
		}

		#endregion Endorsement Wordings

		#region Addition Wordings
		private void createAddWordingsXML()
		{
			string lob_id="4";
			string state_id="";

			if(stCode == "IN")
			{
				state_id = "14";
			}
			else if(stCode == "MI")
			{
				state_id = "22";
			}
			else if(stCode == "WI")
			{
				state_id = "49";
			}
			if (gStrPdfFor == PDFForDecPage)
			{
				if(gStrProcessID != null && gStrProcessID != "")
				{
					DataSet DSAddWordSet = new DataSet();

					gobjWrapper.AddParameter("@STATE_ID",state_id);
					gobjWrapper.AddParameter("@LOB_ID",lob_id);
					gobjWrapper.AddParameter("@PROCESS_ID",gStrProcessID);
					DSAddWordSet = gobjWrapper.ExecuteDataSet("Proc_GetAddWordingsData");
					gobjWrapper.ClearParameteres();

					//DSAddWordSet = gobjSqlHelper.ExecuteDataSet("Proc_GetAddWordingsData " + state_id + "," + lob_id + "," + gStrProcessID);
			
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
			//string strAdverseRep="";
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
			if(strInsuScore =="-2")
			{
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas_1_0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas_1_0>";
				DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</reason_code1>";
				DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</reason_code2>";
				DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</reason_code3>";
				DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</reason_code4>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet1>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet2>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet3>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet4 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet4>";
			}
			else
			{
				DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
				DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
				DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
				DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
			}
			
		}
		private void createPage2PrivacyPageXML(ref XmlElement DecPage1Element)
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
			//string strAdverseRep="";
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
						
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_hd1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_hd2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas_1_0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas_1_0>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet3>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet4 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet4>";
								
						
		}
		private void createPage2AdverseXML(ref XmlElement DecPage1Element)
		{
			XmlElement DecAddPageElement;
			DecAddPageElement = AcordPDFXML.CreateElement("ADDPAGE2");

			DecPage1Element.AppendChild(DecAddPageElement);
			DecAddPageElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecAddPageElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));

			// if Proccess is endorsement or Renewal then we wil not print adverse section

					
			//int RowCounter = 0;
			string strAdverseRep="";
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
			DataSet dsTempPolicy = new DataSet();

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			dsTempPolicy = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//dsTempPolicy = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			strAdverseRep = dsTempPolicy.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
			
			
				///////////////////////
				///
																					//"<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_pol " + fieldType + "=\"" + fieldTypeText + "\">"  + RemoveJunkXmlCharacters("") + "</priv_pol>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_text " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("")+ "</priv_text>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_coll " + fieldType + "=\"" + fieldTypeText + "\">"+ RemoveJunkXmlCharacters("") + "</inf_coll>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<infr_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</infr_txt>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") +"</inf_bull1>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_bull2>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_bull3>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_disc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_disc>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_txt>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_mut " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</wolv_mut>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</wolv_txt>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_proc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</sec_proc>";
					DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</sec_txt>";
					

				DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
				DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
				DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
				DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
				
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt1>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt2>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_add>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<hotline " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</hotline>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<website " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</website>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<foot_left " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</foot_left>";
				

			
		}
		private void createPage2NHNSAdverseXML(ref XmlElement DecPage1Element)
		{
			XmlElement DecAddPageElement;
			DecAddPageElement = AcordPDFXML.CreateElement("ADDPAGE2");

			DecPage1Element.AppendChild(DecAddPageElement);
			DecAddPageElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecAddPageElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));

			// if Proccess is endorsement or Renewal then we wil not print adverse section

					
			//int RowCounter = 0;
			string strAdverseRep="";
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
			DataSet dsTempPolicy = new DataSet();

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			dsTempPolicy = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//dsTempPolicy = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			strAdverseRep = dsTempPolicy.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
			
				///////////////////////
				///
				//"<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_pol " + fieldType + "=\"" + fieldTypeText + "\">"  + RemoveJunkXmlCharacters("") + "</priv_pol>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_text " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("")+ "</priv_text>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_coll " + fieldType + "=\"" + fieldTypeText + "\">"+ RemoveJunkXmlCharacters("") + "</inf_coll>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<infr_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</infr_txt>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") +"</inf_bull1>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_bull2>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_bull3>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_disc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_disc>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_txt>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_mut " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</wolv_mut>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</wolv_txt>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_proc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</sec_proc>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</sec_txt>";
				
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas_1_0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas_1_0>";
			
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet1>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet2>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet3>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet4 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet4>";
			
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt1>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt2>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_add>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<hotline " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</hotline>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<website " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</website>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<foot_left " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</foot_left>";
				

			
		}
		private string ChkPreInsuScr()
		{

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",goldVewrsionId);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet dsoldInsuScr = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//DataSet dsoldInsuScr = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + goldVewrsionId + ",'" + gStrCalledFrom + "'");
			
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet dsnewInsuScr = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//DataSet dsnewInsuScr = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			newInsuScr = dsnewInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString();
			oldInsuScr = dsoldInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString();
			if(Convert.ToDouble(dsnewInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()) < Convert.ToDouble(dsoldInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
			{
				strNeedPage2 ="Y";
				return strNeedPage2;
			}
			else if(Convert.ToDouble(dsnewInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()) >= Convert.ToDouble(dsoldInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
			{
				strNeedPage2 ="N";
				return strNeedPage2;
			}
			else
			{
				return strNeedPage2;
			}
		}
		#endregion

	}
}
