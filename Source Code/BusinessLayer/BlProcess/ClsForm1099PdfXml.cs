using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;
using Cms.Model.Account;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for ClsForm1099PdfXml.
	/// </summary>
	/// 
	public class ClsForm1099PdfXml : ClsCommonPdfXML
	{
		#region Declarations
		private XmlElement Form1099RootElement;
		private DataSet dsForm1099;
		string strEntityId="";
		public string strAgencyCode="";
		//string strAgencyName="";
		string PDFFinalPath="";
		public string PDFName="";
		string PDFlink="";
		string strAgyName="";
		DataWrapper objDataWrapper;
		#endregion
		public ClsForm1099PdfXml(string strEntityFormId)
		{
			strEntityId = strEntityFormId;
		}
		// get xml for populating pdf template
		public string getForm1099PDFXml()
		{
			try
			{
				AcordPDFXML = new XmlDocument();
				AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
				SetPDFVersionLobNode("FORM1099",System.DateTime.Now);
				createRootElementForAllRootPDFs();
				createForm1099PDFXml();
				return AcordPDFXML.OuterXml;
			}
			
			catch(Exception ex)
			{
				throw(new Exception("Error while generating form1099 PDF.",ex));
			}

		}
		// create root element for xml genration 
		public void createRootElementForAllRootPDFs()
		{
			Form1099RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Form1099RootElement);
			Form1099RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("FORM1099PAGE"));
			Form1099RootElement.SetAttribute(fieldType,fieldTypeSingle);
			Form1099RootElement.SetAttribute(id,"1");

		}
		public void createForm1099PDFXml()
		{
			try
			{
				// create first child element to add pdf template
				XmlElement Form1099Element;
				Form1099Element    = AcordPDFXML.CreateElement("FORM1099");
				Form1099RootElement.AppendChild(Form1099Element);
				Form1099Element.SetAttribute(PrimPDF,"");
				Form1099Element.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("FORM1099PAGE"));
				Form1099Element.SetAttribute(SecondPDF,getAcordPDFNameFromXML("FORM1099PAGEEXTN"));
				Form1099Element.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("FORM1099PAGEEXTN"));
				Form1099Element.SetAttribute(fieldType,fieldTypeMultiple);
				//Form1099Element.SetAttribute(id,"1");
				
				
				
				
				//execute dataset 
				objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
				dsForm1099 =objDataWrapper.ExecuteDataSet("Proc_GetForm1099Details " + strEntityId);
				// mapping 
				for(int agyadd=0 ; agyadd<2; agyadd++)
				{
					// create element to get details of 1099  processing
					XmlElement AgForm1099Element;
					AgForm1099Element = AcordPDFXML.CreateElement("AGENCY");
					Form1099Element.AppendChild(AgForm1099Element);
					AgForm1099Element.SetAttribute(fieldType,fieldTypeNormal);
					AgForm1099Element.SetAttribute(id,agyadd.ToString());
					if(agyadd==0)
					{
						AgForm1099Element.InnerXml  = AgForm1099Element.InnerXml + "<A_1099_1_AGENCY_NAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_AGENCY_NAME"].ToString()) + "</A_1099_1_AGENCY_NAME>";
						AgForm1099Element.InnerXml  = AgForm1099Element.InnerXml + "<A_1099_1_AGENCY_ADD1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_AGENCY_ADD1"].ToString()) + "</A_1099_1_AGENCY_ADD1>";
						AgForm1099Element.InnerXml  = AgForm1099Element.InnerXml + "<A_1099_1_AGENCY_STATE_CITY_ZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_AGENCY_STATE_CITY_ZIP"].ToString()) + "</A_1099_1_AGENCY_STATE_CITY_ZIP>";
						AgForm1099Element.InnerXml  = AgForm1099Element.InnerXml + "<A_1099_1_AGENCY_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_AGENCY_PHONE"].ToString()) + "</A_1099_1_AGENCY_PHONE>";
					}
					else
					{
						AgForm1099Element.InnerXml  = AgForm1099Element.InnerXml + "<A_1099_1_AGENCY_NAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_AGENCY_NAME"].ToString()) + "</A_1099_1_AGENCY_NAME>";
						AgForm1099Element.InnerXml  = AgForm1099Element.InnerXml + "<A_1099_1_AGENCY_ADD1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_AGENCY_ADD1"].ToString()) + "</A_1099_1_AGENCY_ADD1>";
						AgForm1099Element.InnerXml  = AgForm1099Element.InnerXml + "<A_1099_1_AGENCY_STATE_CITY_ZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_AGENCY_STATE_CITY_ZIP"].ToString()) + "</A_1099_1_AGENCY_STATE_CITY_ZIP>";
						AgForm1099Element.InnerXml  = AgForm1099Element.InnerXml + "<A_1099_1_AGENCY_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_AGENCY_PHONE"].ToString()) + "</A_1099_1_AGENCY_PHONE>";
					//	AgForm1099Element.InnerXml  = AgForm1099Element.InnerXml + "<A_1099_1_AGENCY_NAME_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_AGENCY_NAME_ADDRESS"].ToString()) + "</A_1099_1_AGENCY_NAME_ADDRESS>";
					}
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_RENTS"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_RENTS"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_RENTS " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(dsForm1099.Tables[0].Rows[0]["A_1099_1_RENTS"].ToString()))) + "</A_1099_1_RENTS>";
					AgForm1099Element.InnerXml +=  "<A_1099_1_RENTS " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_RENTS"].ToString())) + "</A_1099_1_RENTS>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_ROYALTIES"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_ROYALTIES"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_ROYALTIES " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(dsForm1099.Tables[0].Rows[0]["A_1099_1_ROYALTIES"].ToString()))) + "</A_1099_1_ROYALTIES>";
					AgForm1099Element.InnerXml +=  "<A_1099_1_ROYALTIES " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_ROYALTIES"].ToString())) + "</A_1099_1_ROYALTIES>";
					
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_OTHERINCOME"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_OTHERINCOME"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_OTHERINCOME " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(dsForm1099.Tables[0].Rows[0]["A_1099_1_OTHERINCOME"].ToString()))) + "</A_1099_1_OTHERINCOME>";
                    AgForm1099Element.InnerXml +=  "<A_1099_1_OTHERINCOME " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_OTHERINCOME"].ToString())) + "</A_1099_1_OTHERINCOME>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_FEDERALINCOMETAXHELD"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_FEDERALINCOMETAXHELD"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_FEDERALINCOMETAXHELD " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(dsForm1099.Tables[0].Rows[0]["A_1099_1_FEDERALINCOMETAXHELD"].ToString()))) + "</A_1099_1_FEDERALINCOMETAXHELD>";
                    AgForm1099Element.InnerXml +=  "<A_1099_1_FEDERALINCOMETAXHELD " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_FEDERALINCOMETAXHELD"].ToString())) + "</A_1099_1_FEDERALINCOMETAXHELD>";   
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_PAYORID"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_PAYORID"].ToString()!="0")
					{
						string strPayeeId=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(dsForm1099.Tables[0].Rows[0]["A_1099_1_PAYORID"].ToString());
						
						if(strPayeeId.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
						{
							string strvaln = "xxxxx";
							strvaln = strPayeeId.Substring(0,2)+"-"+strPayeeId.Substring(2,strPayeeId.Length-2);
							//strvaln += strRecipeintId.Substring(strvaln.Length, strRecipeintId.Length - strvaln.Length);
							strPayeeId = strvaln;
						}
						else
							strPayeeId="";
						  AgForm1099Element.InnerXml +=  "<A_1099_1_PAYORID " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strPayeeId) + "</A_1099_1_PAYORID>"; 
					}
					//AgForm1099Element.InnerXml +=  "<A_1099_1_PAYORID " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_PAYORID"].ToString()) + "</A_1099_1_PAYORID>";
                  
					
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_RECIPIENTID"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_RECIPIENTID"].ToString()!="0")
					{
						//AgForm1099Element.InnerXml +=  "<A_1099_1_RECIPIENTID " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_RECIPIENTID"].ToString()) + "</A_1099_1_RECIPIENTID>";
						string strRecipeintId=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(dsForm1099.Tables[0].Rows[0]["A_1099_1_RECIPIENTID"].ToString());
						
						if(strRecipeintId.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
						{
							string strvaln = "xxxxx";
							strvaln = strRecipeintId.Substring(0,2)+"-"+strRecipeintId.Substring(2,strRecipeintId.Length-2);
							//strvaln += strRecipeintId.Substring(strvaln.Length, strRecipeintId.Length - strvaln.Length);
							strRecipeintId = strvaln;
						}
						else
							strRecipeintId="";

						AgForm1099Element.InnerXml +=  "<A_1099_1_RECIPIENTID " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strRecipeintId) + "</A_1099_1_RECIPIENTID>";
					}
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_MEDICALPAYMENTS"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_MEDICALPAYMENTS"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_MEDICALPAYMENTS " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(dsForm1099.Tables[0].Rows[0]["A_1099_1_MEDICALPAYMENTS"].ToString()))) + "</A_1099_1_MEDICALPAYMENTS>";
						AgForm1099Element.InnerXml +=  "<A_1099_1_MEDICALPAYMENTS " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_MEDICALPAYMENTS"].ToString())) + "</A_1099_1_MEDICALPAYMENTS>";					
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_FISHINGBOAT"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_FISHINGBOAT"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_FISHINGBOAT " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_FISHINGBOAT"].ToString())) + "</A_1099_1_FISHINGBOAT>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_RECIPIENTNAME"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_RECIPIENTNAME"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_RECIPIENTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_RECIPIENTNAME"].ToString()) + "</A_1099_1_RECIPIENTNAME>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_NONEMPLOYEECOMP"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_NONEMPLOYEECOMP"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_NONEMPLOYEECOMP " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_NONEMPLOYEECOMP"].ToString())) + "</A_1099_1_NONEMPLOYEECOMP>";
					//AgForm1099Element.InnerXml +=  "<A_1099_1_NONEMPLOYEECOMP " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(dsForm1099.Tables[0].Rows[0]["A_1099_1_NONEMPLOYEECOMP"].ToString()))) + "</A_1099_1_NONEMPLOYEECOMP>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_SUBSTITUTE_PAYMENTS"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_SUBSTITUTE_PAYMENTS"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_SUBSTITUTEPAYMENTS " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_SUBSTITUTE_PAYMENTS"].ToString())) + "</A_1099_1_SUBSTITUTEPAYMENTS>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_STREETADDRESS"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_STREETADDRESS"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_STREETADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_STREETADDRESS"].ToString()) + "</A_1099_1_STREETADDRESS>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_RECEIPINT_CITYSTATEZIP"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_RECEIPINT_CITYSTATEZIP"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_RECEIPINT_CITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_RECEIPINT_CITYSTATEZIP"].ToString()) + "</A_1099_1_RECEIPINT_CITYSTATEZIP>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_DIRECTSALES"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_DIRECTSALES"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_DIRECTSALES " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(dsForm1099.Tables[0].Rows[0]["A_1099_1_DIRECTSALES"].ToString()))) + "</A_1099_1_DIRECTSALES>";
						AgForm1099Element.InnerXml +=  "<A_1099_1_DIRECTSALES " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_DIRECTSALES"].ToString())) + "</A_1099_1_DIRECTSALES>";       
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_CROPINSURANCE"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_CROPINSURANCE"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_CROPINSURANCE " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(dsForm1099.Tables[0].Rows[0]["A_1099_1_CROPINSURANCE"].ToString()))) + "</A_1099_1_CROPINSURANCE>";
						AgForm1099Element.InnerXml +=  "<A_1099_1_CROPINSURANCE " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_CROPINSURANCE"].ToString())) + "</A_1099_1_CROPINSURANCE>"; 
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_11NUMBERONLY"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_11NUMBERONLY"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_11NUMBERONLY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_11NUMBERONLY"].ToString()) + "</A_1099_1_11NUMBERONLY>";
						AgForm1099Element.InnerXml +=  "<A_1099_1_11NUMBERONLY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_11NUMBERONLY"].ToString()) + "</A_1099_1_11NUMBERONLY>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_ACCOUNTNUMBER"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_ACCOUNTNUMBER"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_ACCOUNTNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_ACCOUNTNUMBER"].ToString()) + "</A_1099_1_ACCOUNTNUMBER>";
						AgForm1099Element.InnerXml +=  "<A_1099_1_ACCOUNTNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_ACCOUNTNUMBER"].ToString()) + "</A_1099_1_ACCOUNTNUMBER>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_GOLDENPAYMENTS"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_GOLDENPAYMENTS"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_GOLDENPAYMENTS " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_GOLDENPAYMENTS"].ToString())) + "</A_1099_1_GOLDENPAYMENTS>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_GROSSPAYTOATTORNEY"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_GROSSPAYTOATTORNEY"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_GROSSPAYTOATTORNEY " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_GROSSPAYTOATTORNEY"].ToString())) + "</A_1099_1_GROSSPAYTOATTORNEY>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_STATETAXES"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_STATETAXES"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_STATETAXES " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_STATETAXES"].ToString())) + "</A_1099_1_STATETAXES>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_STATEPAYERSTATENO"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_STATEPAYERSTATENO"].ToString()!="0")
						AgForm1099Element.InnerXml +=  "<A_1099_1_STATEPAYERSTATENO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsForm1099.Tables[0].Rows[0]["A_1099_1_STATEPAYERSTATENO"].ToString().Replace(".00","")) + "</A_1099_1_STATEPAYERSTATENO>";
					if(dsForm1099.Tables[0].Rows[0]["A_1099_1_STATEINCOME"].ToString()!="" && dsForm1099.Tables[0].Rows[0]["A_1099_1_STATEINCOME"].ToString()!="0")
					//AgForm1099Element.InnerXml +=  "<A_1099_1_STATEINCOME " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(dsForm1099.Tables[0].Rows[0]["A_1099_1_STATEINCOME"].ToString()))) + "</A_1099_1_STATEINCOME>";
						AgForm1099Element.InnerXml +=  "<A_1099_1_STATEINCOME " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(NumberFormat(dsForm1099.Tables[0].Rows[0]["A_1099_1_STATEINCOME"].ToString())) + "</A_1099_1_STATEINCOME>";

				}
				if(dsForm1099.Tables[0].Rows[0]["AGENCY_CODE"].ToString()!="")
					strAgencyCode = dsForm1099.Tables[0].Rows[0]["AGENCY_CODE"].ToString().Trim();
				if(dsForm1099.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()!="")
					strAgyName= dsForm1099.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString().Trim();
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		public void GenrateForm1099Pdf()
		{
			try
			{
				AcordPDF.AcordXMLParser objEbixAcordPDF = new AcordPDF.AcordXMLParser();
				objEbixAcordPDF.ClientId=int.Parse(strEntityId);
				objEbixAcordPDF.InputXml = getForm1099PDFXml();
				objEbixAcordPDF.LobCode="FORM1099"+"_" + strAgencyCode;
				//objEbixAcordPDF.PolicyId=int.Parse(strAgencyCode);
				//objEbixAcordPDF.PolicyVersion="";

				string strInputPath = InputBase  + "CHK\\" ;
				string strOutputPath = OutputPath + strAgencyCode+ "\\FORM1099";

				
				objEbixAcordPDF.InputPath = strInputPath;
				objEbixAcordPDF.OutputPath = strOutputPath;

				objEbixAcordPDF.ImpersonationUserId = ImpersonationUserId;
				objEbixAcordPDF.ImpersonationPassword = ImpersonationPassword;
				objEbixAcordPDF.ImpersonationDomain = ImpersonationDomain;

				PDFName = objEbixAcordPDF.GeneratePDF("","");  

				PDFFinalPath = FinalBasePath + strAgencyCode + "/" + "FORM1099/";
				PDFlink = PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";

				string TranLogMess = "Form 1099 pdf for "+"  "+strAgyName+" " + " have been generated successfully.";

				WriteTransactionLog(0 , 0 , 0,TranLogMess, int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()), PDFlink,"PREM_NOTICE", "",objDataWrapper);
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		public string GetAgencyCd()
		{
			try
			{
				//execute dataset 
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
				dsForm1099 =objDataWrapper.ExecuteDataSet("Proc_GetForm1099Details " + strEntityId);
				string agcde=dsForm1099.Tables[0].Rows[0]["AGENCY_CODE"].ToString().Trim();
				return agcde;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
	}
}