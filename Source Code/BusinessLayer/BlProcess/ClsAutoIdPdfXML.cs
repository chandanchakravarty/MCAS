using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Mohit Agarwal</CreatedBy>
	/// <Dated>5-Mar-2007</Dated>
	/// <Purpose>To Create XML for Auto ID XML for MI and IN states</Purpose>
	/// </summary>
	public class ClsAutoIdPdfXML : ClsCommonPdf
	{
		#region Declarations
		private XmlElement DecPageRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private DataWrapper gobjWrapper;
		private string stCode="";
		private string gStrLobCode="";
		private string strmakexml="";
		DataSet DSTempCustDataSet;
		private   DataSet DSTempApplicantDataSet = new DataSet();
		#endregion

		#region Constructor
		public ClsAutoIdPdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lstrStateCode, string lstrProcessId, DataWrapper objWrapper, DataSet lDSTempApplicantDataSet)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			//gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			gStrLobCode="AUTO";
			stCode=lstrStateCode;
			DSTempApplicantDataSet = lDSTempApplicantDataSet.Copy();
			gStrProcessID = lstrProcessId;
			DSTempDataSet = new DataSet();
			DSTempCustDataSet = new DataSet();
			this.gobjWrapper = objWrapper;//new DataWrapper(ConnStr,CommandType.Text);
			SetPDFVersionLobNode(gStrLobCode,System.DateTime.Now);
		
		}
		public ClsAutoIdPdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lstrStateCode, string lstrLOB, DataWrapper objWrapper)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			//gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			gStrLobCode=lstrLOB;
			stCode=lstrStateCode;
			DSTempDataSet = new DataSet();
			DSTempCustDataSet = new DataSet();
			this.gobjWrapper = objWrapper;//new DataWrapper(ConnStr,CommandType.Text);
			SetPDFVersionLobNode("AUTO",System.DateTime.Now);
		
		}
		#endregion

		public string getAutoIdPDFXml()
		{
			CreateEndorsementVehicle();
			if(strmakexml !="MATCHED")
			{
				AcordPDFXML = new XmlDocument();
				AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

				createRootElementForAllRootPDFs();
				//LoadRateXML();
				FillMonth();

				//creating Xml From Here
				CreateAutoIdXML();
				return AcordPDFXML.OuterXml;
			}
			else 
			{
				return "";
			}
		}

		#region To Create Root Element For All Root PDFs
		private void createRootElementForAllRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			if(stCode == "MI")
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTOIDENMI"));
			else
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTOIDENIN"));
		}
		#endregion

		#region Creating Auto ID Xml 
		private void CreateAutoIdXML()
		{
			XmlElement DecPageAutoIdRoot;
			DecPageAutoIdRoot = AcordPDFXML.CreateElement("AUTOIDDETAILS");
			DecPageRootElement.AppendChild(DecPageAutoIdRoot);
			DecPageAutoIdRoot.SetAttribute(fieldType,fieldTypeMultiple);
			
			if(gStrLobCode=="MOT")
			{
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempCustDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
				gobjWrapper.ClearParameteres();
				DSTempApplicantDataSet = DSTempCustDataSet.Copy();
				gStrLobCode="AUTO";
			}			
			//DSTempCustDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			string Insured = NamedInsuredWithSuffixs(DSTempApplicantDataSet);
			
			gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
			gobjWrapper.AddParameter("@POLICY_ID",gStrPolicyId);
			gobjWrapper.AddParameter("@POLICY_VERSION_ID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLED_FROM",gStrCalledFrom);
			gobjWrapper.AddParameter("@PROCESS_STATUS","");
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetCancellationNoticeData");
			gobjWrapper.ClearParameteres();
			//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "',''");
			//string Insured = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
//			string InsAdd = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
//			if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
//			{
//				InsAdd+= ", " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
//			}
//			string InsCity = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString();
			string Agency = DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
			string AgnAdd1 = DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD1"].ToString();
//			if(AgnAdd1.Trim() != "")
//				AgnAdd1 += ", " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString();
//			else
//				AgnAdd1 += DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString();

			if(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString().Trim() != "")
			{
				AgnAdd1 += ", " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString();
			}
			string AgnPhn = DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString(); 
			string AgnAdd2 = DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_STATE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ZIP"].ToString();
			string	PolNum = DSTempDataSet.Tables[0].Rows[0]["POL_NUMBER"].ToString();
			string Eff_dat="", Exp_dat="";
			if(DSTempDataSet.Tables[0].Rows[0]["PROCESS_EFFECTIVE_DATE"] != System.DBNull.Value)
				Eff_dat = Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["PROCESS_EFFECTIVE_DATE"]).ToString("MM/dd/yyyy");
			if(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
				Exp_dat = Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy");

			if(stCode == "MI")
			{
				DecPageAutoIdRoot.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTOIDMI"));
				DecPageAutoIdRoot.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOIDMI"));
				DecPageAutoIdRoot.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAUTOIDMIEXTN"));
				DecPageAutoIdRoot.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOIDMIEXTN"));
			}
			else //if(stCode == "IN")
			{
				DecPageAutoIdRoot.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTOIDIN"));
				DecPageAutoIdRoot.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOIDIN"));
				DecPageAutoIdRoot.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAUTOIDINEXTN"));
				DecPageAutoIdRoot.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOIDINEXTN"));
			}

			if(gStrCalledFrom.ToUpper() == "APPLICATION")
			{
				gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
				gobjWrapper.AddParameter("@APP_ID",gStrPolicyId);
				gobjWrapper.AddParameter("@APP_VERSION_ID",gStrPolicyVersion);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetVehicleIDs");
				gobjWrapper.ClearParameteres();
				//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetVehicleIDs " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);			
			}
			else if(gStrCalledFrom.ToUpper() == "POLICY")
			{
				gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
				gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
				gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetVehicleAutoIdDetailsPolicy");
				gobjWrapper.ClearParameteres();
				//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetVehicleAutoIdDetailsPolicy " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);			
			}

			if(DSTempDataSet != null && DSTempDataSet.Tables.Count > 0 && DSTempDataSet.Tables[0].Rows.Count>0)
			{
				int autoCounter = 0;
				foreach(DataRow AutoDetail in DSTempDataSet.Tables[0].Rows)
				{
					XmlElement DecPageAutoIdElement;
					DecPageAutoIdElement = AcordPDFXML.CreateElement("AUTOIDDETAILSINFO");
					DecPageAutoIdRoot.AppendChild(DecPageAutoIdElement);
					DecPageAutoIdElement.SetAttribute(fieldType,fieldTypeNormal);
					DecPageAutoIdElement.SetAttribute(id,autoCounter.ToString());

					string InsAdd = AutoDetail["GARAGE_ADDRESS1"].ToString();
					if(AutoDetail["GARAGE_ADDRESS2"].ToString().Trim() != "")
					{
						InsAdd+= ", " + AutoDetail["GARAGE_ADDRESS2"].ToString();
					}
					string InsCity = AutoDetail["GARAGE_CITY"].ToString() + " " + AutoDetail["GARAGE_STATE"].ToString() + " " + AutoDetail["GARAGE_ZIP"].ToString();
			
					if(stCode == "IN")
					{
						DecPageAutoIdElement.InnerXml += "<State " + fieldType + "=\"" + fieldTypeText + "\">" + "Indiana" + "</State>";
						DecPageAutoIdElement.InnerXml += "<Company_num " + fieldType + "=\"" + fieldTypeText + "\">" + "15047" + "</Company_num>";
						DecPageAutoIdElement.InnerXml += "<Agency_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AgnAdd1) + "</Agency_add>";
						DecPageAutoIdElement.InnerXml += "<Agency_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AgnAdd2) + "</Agency_add1>";
						DecPageAutoIdElement.InnerXml += "<Agency_Phone " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AgnPhn) + "</Agency_Phone>";
					}
					DecPageAutoIdElement.InnerXml += "<Company " + fieldType + "=\"" + fieldTypeText + "\">" + "Wolverine Mutual Insurance Company" + "</Company>";
					DecPageAutoIdElement.InnerXml += "<Policy_num " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(PolNum) + "</Policy_num>";
					DecPageAutoIdElement.InnerXml += "<Eff_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Eff_dat) + "</Eff_date>";
					DecPageAutoIdElement.InnerXml += "<Exp_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Exp_dat) + "</Exp_date>";
					DecPageAutoIdElement.InnerXml += "<Year " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) + "</Year>";
					DecPageAutoIdElement.InnerXml += "<MakeModel " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString() + "/" + AutoDetail["MODEL"].ToString()) + "</MakeModel>";
					DecPageAutoIdElement.InnerXml += "<VIN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) + "</VIN>";
					DecPageAutoIdElement.InnerXml += "<Agency_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Agency) + "</Agency_name>";
					DecPageAutoIdElement.InnerXml += "<Ins_name1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</Ins_name1>";
					DecPageAutoIdElement.InnerXml += "<Ins_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</Ins_add1>";
					if(DSTempDataSet.Tables[1].Rows[0]["EXCLUDED_DRIVER"].ToString() =="TRUE")
					{
						DecPageAutoIdElement.InnerXml += "<EXCLUDEDDRIVERMESSAGE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[1].Rows[0]["EXCLUDED_DRIVER_MESSAGE"].ToString()) + "</EXCLUDEDDRIVERMESSAGE>";
					}
					if(stCode == "IN")
					{						
						DecPageAutoIdElement.InnerXml += "<Ins_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString())+ RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["SUFFIX"].ToString()) + "</Ins_name>";	
						if(DSTempApplicantDataSet.Tables[0].Rows.Count>1)
							DecPageAutoIdElement.InnerXml += "<Ins_name1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[1]["APPNAME"].ToString())+ RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[1]["SUFFIX"].ToString()) + "</Ins_name1>";
					}
					if(stCode == "MI")
					{
//						if(DSTempDataSet.Tables[1].Rows[0]["EXCLUDED_DRIVER"].ToString() =="TRUE")
//						{
							DecPageAutoIdElement.InnerXml += "<Ins_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString())+ RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["SUFFIX"].ToString()) + "</Ins_name>";	
							if(DSTempApplicantDataSet.Tables[0].Rows.Count>1)
							DecPageAutoIdElement.InnerXml += "<Ins_name1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[1]["APPNAME"].ToString())+ RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[1]["SUFFIX"].ToString()) + "</Ins_name1>";
//						}
//						else
//						{
						//	DecPageAutoIdElement.InnerXml += "<Ins_nameL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Insured) + "</Ins_nameL>";
						//}
					}
					// Address
					if(stCode == "IN")
					{
//						if(DSTempCustDataSet.Tables[0].Rows.Count>1)
//						{
							DecPageAutoIdElement.InnerXml += "<Ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsAdd) + "</Ins_add>";
							DecPageAutoIdElement.InnerXml += "<Ins_city " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsCity) + "</Ins_city>";
//						}
//						else
//						{
//							DecPageAutoIdElement.InnerXml += "<InsExcl_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsAdd) + "</InsExcl_add>";
//							DecPageAutoIdElement.InnerXml += "<Ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsCity) + "</Ins_add>";
//						}
					}
					else if(stCode == "MI")
					{
//						if(DSTempCustDataSet.Tables[0].Rows.Count>1)
//						{
//							if(DSTempDataSet.Tables[1].Rows[0]["EXCLUDED_DRIVER"].ToString() =="TRUE" && DSTempCustDataSet.Tables[0].Rows[1]["APPNAME"].ToString() !="")
//							{
//								DecPageAutoIdElement.InnerXml += "<Ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsAdd) + "</Ins_add>";
//								DecPageAutoIdElement.InnerXml += "<Ins_city " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsCity) + "</Ins_city>";
//							}
//							else if(DSTempDataSet.Tables[1].Rows[0]["EXCLUDED_DRIVER"].ToString() =="TRUE" && DSTempCustDataSet.Tables[0].Rows[1]["APPNAME"].ToString() =="")
//							{
//								DecPageAutoIdElement.InnerXml += "<InsExcl_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsAdd) + "</InsExcl_add>";
//								DecPageAutoIdElement.InnerXml += "<Ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsCity) + "</Ins_add>";
//							}
//							else if(DSTempDataSet.Tables[1].Rows[0]["EXCLUDED_DRIVER"].ToString() =="FALSE" && DSTempCustDataSet.Tables[0].Rows[1]["APPNAME"].ToString() =="") 
//							{
//								DecPageAutoIdElement.InnerXml += "<InsExcl_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsAdd) + "</InsExcl_add>";
//								DecPageAutoIdElement.InnerXml += "<Ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsCity) + "</Ins_add>";
//							}
//							else if(DSTempDataSet.Tables[1].Rows[0]["EXCLUDED_DRIVER"].ToString() =="FALSE" && DSTempCustDataSet.Tables[0].Rows[1]["APPNAME"].ToString() !="") 
//							{
//								DecPageAutoIdElement.InnerXml += "<Ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsAdd) + "</Ins_add>";
//								DecPageAutoIdElement.InnerXml += "<Ins_city " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsCity) + "</Ins_city>";
//							}
//						}
//						else 
//						{
							DecPageAutoIdElement.InnerXml += "<Ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsAdd) + "</Ins_add>";
							//DecPageAutoIdElement.InnerXml += "<InsExcl_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsAdd) + "</InsExcl_add>";
							//DecPageAutoIdElement.InnerXml += "<Ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsCity) + "</Ins_add>";
							DecPageAutoIdElement.InnerXml += "<Ins_city " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(InsCity) + "</Ins_city>";

//						}

					}
					autoCounter++;
				}
			}
		}
		#endregion
		private string CreateEndorsementVehicle()
		{
			try
			{
				if(gStrProcessID =="")
				{
					gStrProcessID = GetPolicyProcess( gStrClientID ,gStrPolicyId ,gStrPolicyVersion , gStrCalledFrom,gobjWrapper);
				}
				if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString())
				{
					gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
					gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
					gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
					DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetVehicleAutoIdDetailsPolicy");
					gobjWrapper.ClearParameteres();
					//DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetVehicleAutoIdDetailsPolicy " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);			
					if(	DSTempDataSet.Tables[0].Rows.Count >0)
					{
						strmakexml="TRUE";
					}
					else
					{
						strmakexml="MATCHED";
					}
				}
				else
				{
					strmakexml="TRUE";
				}
				return strmakexml;
			}
			catch//(Exception ex)
			{
					return "";
			}
		}
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
	}
}