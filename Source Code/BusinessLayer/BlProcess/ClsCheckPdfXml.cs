using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for ClsCheckPdfXml.
	/// </summary>
	public class ClsCheckPdfXml : ClsCommonPdf
	{
		#region Declarations
		private XmlElement RootElementCheckPage;
		//		private int accountID;
		private string checkID;
		//private int chkInfoCounter=0;
		private DataWrapper gobjWrapper;
		#endregion

		public ClsCheckPdfXml(string CHECK_ID, DataWrapper LobjWrapper)
		{
			DSTempDataSet = new DataSet();
			checkID = CHECK_ID;
			this.gobjWrapper = LobjWrapper;
			//gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			
			//	DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			//	if(DSTempDataSet.Tables[0].Rows.Count>0)
			SetPDFVersionLobNode("CHK",System.DateTime.Now);
		}

		public ClsCheckPdfXml(string CHECK_ID)
		{
			DSTempDataSet = new DataSet();
			checkID = CHECK_ID;
			//this.gobjWrapper = LobjWrapper;
			gobjWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			//	DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			//	if(DSTempDataSet.Tables[0].Rows.Count>0)
			SetPDFVersionLobNode("CHK",System.DateTime.Now);
		}
		public string getCheckPDFXml(ref string blank_num, ref string check_num)
		{
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementForAllRootPDFs();
			blank_num = createCheckXML(ref check_num);
			if(blank_num == "1")
				blank_num = "BLANK_NUM";
			else if(blank_num == "2")
				blank_num = "BLANK_NUM_SOME";
			else 
				blank_num = "";
			
			return AcordPDFXML.OuterXml;
		}
		private void createRootElementForAllRootPDFs()
		{
			//			RootElementCheckPage = AcordPDFXML.CreateElement(RootElementForAllPDF);
			//			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(RootElementCheckPage);
			//			RootElementCheckPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHECKPAGE"));
			
		}

		private string Month(int mont)
		{
			switch(mont)
			{
				case 1: return "January";
					break;
				case 2: return "February";
					break;
				case 3: return "March";
					break;
				case 4: return "April";
					break;
				case 5: return "May";
					break;
				case 6: return "June";
					break;
				case 7: return "July";
					break;
				case 8: return "August";
					break;
				case 9: return "September";
					break;
				case 10: return "October";
					break;
				case 11: return "November";
					break;
				case 12: return "December";
					break;
				default: return "";
					break;
			}
		}

		private string createCheckXML(ref string check_num)
		{
			String [] chkRows = checkID.Split('~');
			int blank_num = 0;
			int chkCounter=0;
			int claims_check=0;

			check_num = "";
			if(chkRows.Length > 0)
			{
				
				#region Commented By Ravindra(02-26-2008) 
				/*
				//Check for Claims check
				DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFChecksInfo " + chkRows[0] + ",'CLAIMS'");
				if (DSTempDataSet.Tables[0].Rows.Count > 0 )
				{
					if(DSTempDataSet.Tables[0].Rows[0]["CHECK_TYPE"].ToString() == "9937")
						claims_check = 1;
				}
					
				#region createRootElementForAllRootPDFs
				if(claims_check == 0)
				{
					RootElementCheckPage = AcordPDFXML.CreateElement(RootElementForAllPDF);
					AcordPDFXML.SelectSingleNode(RootElement).AppendChild(RootElementCheckPage);
					RootElementCheckPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHECKPAGE"));
				}
				else
				{
					RootElementCheckPage = AcordPDFXML.CreateElement(RootElementForAllPDF);
					AcordPDFXML.SelectSingleNode(RootElement).AppendChild(RootElementCheckPage);
					RootElementCheckPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CLAIMCHECKPAGE"));
				}

				#endregion createRootElementForAllRootPDFs

				#region Check Page Root Element
				XmlElement CheckRootElement;
				CheckRootElement = AcordPDFXML.CreateElement("CHECKDETAILS");
				RootElementCheckPage.AppendChild(CheckRootElement);
				CheckRootElement.SetAttribute(fieldType,fieldTypeMultiple);
				if(claims_check == 0)
				{
					CheckRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHKPAGE"));
					CheckRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CHKPAGE"));
					CheckRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGEEXTN"));
					CheckRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CHKPAGEEXTN"));
				}
				else
				{
					CheckRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CLAIMCHKPAGE"));
					CheckRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CLAIMCHKPAGE"));
					CheckRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CLAIMCHKPAGEEXTN"));
					CheckRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CLAIMCHKPAGEEXTN"));
				}

				#endregion
				*/
				#endregion 

				
				PayeeName = "";
				CheckDate = "";
				CheckAmount = "";
				CheckNumber = "";
				XmlElement CheckRootElement;
								
				for ( int ctr=0;  ctr < chkRows.Length ; ctr++)
				{
					//Assign Check Number 
					gobjWrapper.ClearParameteres();
					gobjWrapper.AddParameter("@CHECK_ID",int.Parse(chkRows[ctr]));
					gobjWrapper.ExecuteNonQuery("ProcAssignCheckNumber");
					gobjWrapper.ClearParameteres();
					//Fetch Check Data
					gobjWrapper.ClearParameteres();
					gobjWrapper.AddParameter("@CHECKID",int.Parse(chkRows[ctr]));
					DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFChecksInfo");
					gobjWrapper.ClearParameteres();
					//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFChecksInfo " + int.Parse(chkRows[ctr]));
					if (DSTempDataSet.Tables[0].Rows.Count > 0 )
					{

						if(DSTempDataSet.Tables[0].Rows[0]["CHECK_TYPE"].ToString() == "9938" || DSTempDataSet.Tables[0].Rows[0]["CHECK_TYPE"].ToString() == "9940" || DSTempDataSet.Tables[0].Rows[0]["CHECK_TYPE"].ToString() == "9945")
						{
							
								CheckRootElement = AcordPDFXML.CreateElement("CHECKDETAILS");
							
							if(DSTempDataSet.Tables[0].Rows[0]["CHECK_TYPE"].ToString() == "9937")
								claims_check = 1;


							if(claims_check == 0)
							{
								RootElementCheckPage = AcordPDFXML.CreateElement(RootElementForAllPDF);
								AcordPDFXML.SelectSingleNode(RootElement).AppendChild(RootElementCheckPage);
								RootElementCheckPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHECKPAGE"));
								//RootElementCheckPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGEEXTN"));
							}
							else
							{
								RootElementCheckPage = AcordPDFXML.CreateElement(RootElementForAllPDF);
								AcordPDFXML.SelectSingleNode(RootElement).AppendChild(RootElementCheckPage);
								RootElementCheckPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CLAIMCHECKPAGE"));
							}
							
							RootElementCheckPage.AppendChild(CheckRootElement);
							CheckRootElement.SetAttribute(fieldType,fieldTypeMultiple);
							if(claims_check == 0)
							{
								CheckRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHKPAGE"));
								CheckRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CHKPAGE"));
								CheckRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGESEC"));
								CheckRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CHKPAGESEC"));
							}
							else
							{
								CheckRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CLAIMCHKPAGE"));
								CheckRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CLAIMCHKPAGE"));
								CheckRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CLAIMCHKPAGEEXTN"));
								CheckRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CLAIMCHKPAGEEXTN"));
							}
						}
							//If first iteration create Root based on check type
						else
						{
							CheckRootElement = AcordPDFXML.CreateElement("CHECKDETAILS");
				
//							if(ctr==0)
						//{
								if(DSTempDataSet.Tables[0].Rows[0]["CHECK_TYPE"].ToString() == "9937")
									claims_check = 1;


								if(claims_check == 0)
								{
									RootElementCheckPage = AcordPDFXML.CreateElement(RootElementForAllPDF);
									AcordPDFXML.SelectSingleNode(RootElement).AppendChild(RootElementCheckPage);
									RootElementCheckPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHECKPAGE"));
									//RootElementCheckPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGEEXTN"));
								}
								else
								{
									RootElementCheckPage = AcordPDFXML.CreateElement(RootElementForAllPDF);
									AcordPDFXML.SelectSingleNode(RootElement).AppendChild(RootElementCheckPage);
									RootElementCheckPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CLAIMCHECKPAGE"));
								}
							
								RootElementCheckPage.AppendChild(CheckRootElement);
								CheckRootElement.SetAttribute(fieldType,fieldTypeMultiple);
								if(claims_check == 0)
								{
									CheckRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHKPAGE"));
									CheckRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CHKPAGE"));
									CheckRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGESEC"));
									CheckRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CHKPAGESEC"));
								}
								else
								{
									CheckRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CLAIMCHKPAGE"));
									CheckRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CLAIMCHKPAGE"));
									CheckRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CLAIMCHKPAGEEXTN"));
									CheckRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CLAIMCHKPAGEEXTN"));
								}
							//}
						}


						#region CheckPage Element

						if(PayeeName == "")
							PayeeName = DSTempDataSet.Tables[0].Rows[0]["PAYEE_ENTITY_NAME"].ToString();
						else
							PayeeName += "~" + DSTempDataSet.Tables[0].Rows[0]["PAYEE_ENTITY_NAME"].ToString();
						
						if(DSTempDataSet.Tables[0].Rows[0]["CHECK_NUMBER"].ToString() == "")
						{
							if(CheckNumber == "")
								CheckNumber = "BLANK";						
							else
								CheckNumber += "~" + "BLANK";
						}
						else
						{
							if(CheckNumber == "")
								CheckNumber = DSTempDataSet.Tables[0].Rows[0]["CHECK_NUMBER"].ToString();						
							else
								CheckNumber += "~" + DSTempDataSet.Tables[0].Rows[0]["CHECK_NUMBER"].ToString();
						}

						if(CheckDate == "")
							CheckDate = DSTempDataSet.Tables[0].Rows[0]["CHECK_DATE"].ToString();
						else
							CheckDate += "~" + DSTempDataSet.Tables[0].Rows[0]["CHECK_DATE"].ToString();

						if(CheckAmount == "")
							CheckAmount = DSTempDataSet.Tables[0].Rows[0]["CHECK_AMOUNT"].ToString();
						else
							CheckAmount += "~" + DSTempDataSet.Tables[0].Rows[0]["CHECK_AMOUNT"].ToString();
							
				
				
						#endregion

						string checkType="";
						string preChkType="";
						if(DSTempDataSet.Tables[0].Rows[0]["CHECK_NUMBER"].ToString() == "")
						{
							blank_num = 1;
							continue;
						}
						else
						{
							if(check_num == "")
								check_num = DSTempDataSet.Tables[0].Rows[0]["CHECK_NUMBER"].ToString();
							else
								check_num += ", " + DSTempDataSet.Tables[0].Rows[0]["CHECK_NUMBER"].ToString();
						}

						//foreach(DataRow ChkDetail in DSTempDataSet.Tables[0].Rows)
					{
						DataRow ChkDetail = DSTempDataSet.Tables[0].Rows[0];

						XmlElement CheckPageElement;
						CheckPageElement = AcordPDFXML.CreateElement("CHECKDETAILSINFO");
						CheckRootElement.AppendChild(CheckPageElement);
						CheckPageElement.SetAttribute(fieldType,fieldTypeNormal);
						CheckPageElement.SetAttribute(id,chkCounter.ToString());
						checkType = ChkDetail["CHECK_TYPE"].ToString();
						//					string strfloatX = "380";
						//					string strfloatY = "36";
						//					string strfloatW = "153";
						//					string strfloatH = "22";
						//					string strpageNo = "2";
						//					CheckPageElement.InnerXml += "<SIGNATURE " + fieldType +"=\""+ fieldTypeImage +"\" " + imageType + "=\"" + imageTypeYes + "\" " + floatX + "=\"" + strfloatX + "\" " + floatY + "=\"" + strfloatY + "\" " + floatW + "=\"" + strfloatW  + "\" " + floatH + "=\"" + strfloatH  + "\" " + pageNo + "=\"" + strpageNo  + "\"></SIGNATURE>";
							
							
							
						CheckPageElement.InnerXml += "<DispHead1 " + fieldType + "=\"" + fieldTypeText + "\">" + "Wolverine Mutual Insurance Company" + "</DispHead1>";
						CheckPageElement.InnerXml += "<DispHead2 " + fieldType + "=\"" + fieldTypeText + "\">" + "One Wolverine Way" + "</DispHead2>";
						CheckPageElement.InnerXml += "<DispAdd1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("P.O. Box 530") + "</DispAdd1>";
						CheckPageElement.InnerXml += "<DispAdd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Dowagiac, Michigan 49047-0530") + "</DispAdd2>";
						//							CheckPageElement.InnerXml += "<BankName " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Fifth Third Bank") + "</BankName>";
						//							CheckPageElement.InnerXml += "<DispBankInfo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Western Michigan \n 74-005/724") + "</DispBankInfo>";


						CheckPageElement.InnerXml += "<BankName " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_NAME"].ToString()) + "</BankName>";
						CheckPageElement.InnerXml += "<DispBankInfo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_ADDRESS"].ToString()) + "</DispBankInfo>";

						CheckPageElement.InnerXml += "<ChkNo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_NUMBER"].ToString()) + "</ChkNo>";
						CheckPageElement.InnerXml += "<DispTAmt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["check_amt"].ToString()) + "</DispTAmt>";
						CheckPageElement.InnerXml += "<Dispamount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) + "</Dispamount>";
						//CheckPageElement.InnerXml += "<Disp_check_no " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("C" + ChkDetail["CHECK_NUMBER"].ToString() + "C") + "</Disp_check_no>";
							
						//Ravindra(06-16-2008): MICR line will be printed by Cheque Printer. Ref iTrack 4345
						//CheckPageElement.InnerXml += "<Disp_route_no " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("C" + ChkDetail["CHECK_NUMBER"].ToString() + "C" + " A" + ChkDetail["BANK_MICR_CODE"].ToString()+ "A " + ChkDetail["BANK_NUMBER"].ToString() + "C") + "</Disp_route_no>";
							
						//CheckPageElement.InnerXml += "<Disp_micr_no " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_NUMBER"].ToString() + "C") + "</Disp_micr_no>";
						CheckPageElement.InnerXml += "<bank_no " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_NUMBER_TOP"].ToString()) + "</bank_no>";
						CheckPageElement.InnerXml += "<check_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_DATE"].ToString()) + "</check_date>";
						CheckPageElement.InnerXml += "<check_note " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_NOTE"].ToString()) + "</check_note>";
					
						if(ChkDetail["CHECK_TYPE"].ToString() == "9937" && ChkDetail["CLAIM_TO_ORDER_DESC"].ToString() != "")
							CheckPageElement.InnerXml += "<claim_toorderof " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CLAIM_TO_ORDER_DESC"].ToString()) + "</claim_toorderof>";
						else
						{
							if(ChkDetail["COMM_TYPE"].ToString().Trim() != "CAC")
							{
								CheckPageElement.InnerXml += "<Payee_entity_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name>";
								CheckPageElement.InnerXml += "<Payee_entity_name_footer " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name_footer>";

								//CheckPageElement.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["address"].ToString()) + "</Disp_payee_add1>";
								CheckPageElement.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_1"].ToString()) + "</Disp_payee_add1>";
								if(ChkDetail["CHECK_TYPE"].ToString() != "9937")
								CheckPageElement.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_add2>";
							}
							else
							{
								CheckPageElement.InnerXml += "<Payee_entity_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name>";
								CheckPageElement.InnerXml += "<csr_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</csr_name>";
								CheckPageElement.InnerXml += "<Payee_entity_name_footer " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGENCY_NAME"].ToString()) + "</Payee_entity_name_footer>";
								//CheckPageElement.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["address"].ToString()) + "</Disp_payee_add1>";
								CheckPageElement.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_1"].ToString()) + "</Disp_payee_add1>";
								if(ChkDetail["CHECK_TYPE"].ToString() != "9937")
								CheckPageElement.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_add2>";
							}
						}
					
					
						if(ChkDetail["CHECK_TYPE"].ToString() == "9937")
						{
							CheckPageElement.InnerXml += "<Payee_entity_name1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME1"].ToString()) + "</Payee_entity_name1>";
							//CheckPageElement.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["address"].ToString()) + "</Disp_payee_add1>";
							CheckPageElement.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ADD1"].ToString()) + "</Disp_payee_add1>";
							if(ChkDetail["PAYEE_ADD2"].ToString()!="")
							{
								CheckPageElement.InnerXml += "<Disp_payee_address2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ADD2"].ToString()) + "</Disp_payee_address2>";							
								CheckPageElement.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_add2>";
							}
							else
							{
								CheckPageElement.InnerXml += "<Disp_payee_address2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_address2>";							
								CheckPageElement.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</Disp_payee_add2>";
							}
														
						}
					
						//CheckPageElement.InnerXml += "<Payee_Entity_name_ref " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_Entity_name_ref>";
						//CheckPageElement.InnerXml += "<Disp_payee_add1_ref " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["address"].ToString()) + "</Disp_payee_add1_ref>";

						//string strfloatX = "430";
						//string strfloatY = "630";
						//string strfloatW = "153";
						//string strfloatH = "22";
						//string strpageNo = "1";

						//							if(ChkDetail["SIGN_FILE_1"] != null)
						//								CheckPageElement.InnerXml += "<SIGNATURE " + fieldType +"=\""+ fieldTypeImage +"\" IMAGEPATH=\"" + RemoveJunkXmlCharacters(ChkDetail["SIGN_FILE_1"].ToString()) + "\" " + imageType + "=\"" + imageTypeYes + "\" " + floatX + "=\"" + strfloatX + "\" " + floatY + "=\"" + strfloatY + "\" " + floatW + "=\"" + strfloatW  + "\" " + floatH + "=\"" + strfloatH  + "\" " + pageNo + "=\"" + strpageNo  + "\"></SIGNATURE>";
				
						//				chkInfoCounter=0;
						//				foreach(DataRow ChkInfo in DSTempDataSet.Tables[1].Rows)
					{
						/*				XmlElement CheckPageInfoElement;
											CheckPageInfoElement = AcordPDFXML.CreateElement("CHECKINFO");
											CheckRootInfoElement.AppendChild(CheckPageInfoElement);
											CheckPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
											CheckPageInfoElement.SetAttribute(id,chkInfoCounter.ToString());
								*/			
						/*
							CheckPageInfoElement.InnerXml += "<reference " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkInfo["REF_CUSTOMER_NAME"].ToString()) +"</reference>";
							CheckPageInfoElement.InnerXml += "<description " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkInfo["LINE_ITEM_DESCRIPTION"].ToString()) +"</description>";
							CheckPageInfoElement.InnerXml += "<account " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkInfo["ACC_DISP_NUMBER"].ToString()) +"</account>";
							CheckPageInfoElement.InnerXml += "<amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkInfo["RECEIPT_AMOUNT"].ToString()) +"</amount>";
							CheckPageInfoElement.InnerXml += "<total_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkInfo["RECEIPT_AMOUNT"].ToString()) +"</total_amount>";
							*/

					// Populate Second page of the check 
//						XmlElement ChkElement;
//						if(ChkDetail["CHECK_TYPE"].ToString() == "9938" || ChkDetail["CHECK_TYPE"].ToString() == "9940" || ChkDetail["CHECK_TYPE"].ToString() == "9945")
//						{
//							// node to add extra page to check
//							
//							ChkElement = AcordPDFXML.CreateElement("CHECKDISTIRBUTION");
//							CheckPageElement.AppendChild(ChkElement);
//							ChkElement.SetAttribute(fieldType,fieldTypeMultiple);
//							ChkElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHKPAGE"));
//							ChkElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CHKPAGE"));
//							ChkElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGEEXTN"));
//							ChkElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CHKPAGEEXTN"));
//								
//						}

						#region switch for check type
						switch(ChkDetail["CHECK_TYPE"].ToString())
						{
								//Agency Commission Checks
							case "2472":
							{
								
								
								DateTime checkdt;
								int agencyindex = 0;
								int stubindex = 0;
								string stubsuffix = "", reference="", desc="", acc="", amt="";

                                checkdt = Convert.ToDateTime(ChkDetail["STMT_DATE"]);
								CheckPageElement.InnerXml += "<reference " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Month(checkdt.Month) + " " + checkdt.Year.ToString()) +"</reference>";
								CheckPageElement.InnerXml += "<total_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</total_amount>";

								// node to add extra page to check
								XmlElement ChkElement;
								ChkElement = AcordPDFXML.CreateElement("CHECKDISTIRBUTION");
								CheckPageElement.AppendChild(ChkElement);
								ChkElement.SetAttribute(fieldType,fieldTypeMultiple);
								ChkElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHKPAGE"));
								ChkElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CHKPAGE"));
								ChkElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGEEXTN"));
								ChkElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CHKPAGEEXTN"));

								for(agencyindex = 0; agencyindex < 5; agencyindex++)
								{
									if(stubindex == 0)
										stubsuffix = "";
									else
										stubsuffix = stubindex.ToString();

									if(ChkDetail["AMT_COMMISSION_PAYABLE_DB"].ToString() != "" && agencyindex == 0)
									{
										reference = "";
										if(ChkDetail["COMM_TYPE"].ToString().Trim() == "CAC")
										{
											desc = "APP UPLOAD BONUS (DB).";
										}
										else if(ChkDetail["COMM_TYPE"].ToString().Trim() == "ADC")
										{
											desc = "PRODUCTION PLUS COMM (DB).";
										}
										else
										{
											desc = "DIRECT BILL COMM.";
										}
										acc = ChkDetail["LIB_COMM_PAYB_DIRECT_BILL"].ToString();
										amt = ChkDetail["AMT_COMMISSION_PAYABLE_DB"].ToString();
									}
									else if(agencyindex == 0)
										continue;

									if(ChkDetail["AMT_COMMISSION_PAYABLE_AB"].ToString() != "" && agencyindex == 1)
									{
										reference = "";

										if(ChkDetail["COMM_TYPE"].ToString().Trim() == "CAC")
										{
											desc = "APP UPLOAD BONUS (AB).";
										}
										else if(ChkDetail["COMM_TYPE"].ToString().Trim() == "ADC")
										{
											desc = "PRODUCTION PLUS COMM (AB).";
										}
										else
										{
											desc = "AGENCY BILL COMM.";
										}
										acc = ChkDetail["LIB_COMM_PAYB_AGENCY_BILL"].ToString();
										amt = ChkDetail["AMT_COMMISSION_PAYABLE_AB"].ToString();
									}
									else if(agencyindex == 1)
										continue;

									if(ChkDetail["AMT_UNCOLLECTED_PREMIUM_AB"].ToString() != "" && agencyindex == 2)
									{
										reference = "";
										desc = "AGENCY BILLED PREM.";
										acc = ChkDetail["AST_UNCOLL_PRM_AGENCY"].ToString();
										amt = ChkDetail["AMT_UNCOLLECTED_PREMIUM_AB"].ToString();
									}
									else if(agencyindex == 2)
										continue;

									if(ChkDetail["DIFFERENCE_AMOUNT"].ToString() != "0.00" && ChkDetail["AGENCY_ACC_NUM"].ToString() != "" && agencyindex == 3)
									{
										reference = "";
										desc = ChkDetail["DESCRIPTION"].ToString();
										acc = ChkDetail["AGENCY_ACC_NUM"].ToString();
										amt = ChkDetail["DIFFERENCE_AMOUNT"].ToString();
									}
									else if(agencyindex == 3)
										continue;

									if(ChkDetail["AMOUNT_TO_APPLY_OP"].ToString() != "" && agencyindex == 4)
									{
										reference = "";
										desc = "AGENCY BILLED PREM.";
										acc = ChkDetail["AST_UNCOLL_PRM_AGENCY"].ToString();
										amt = ChkDetail["AMOUNT_TO_APPLY_OP"].ToString();
									}
									else if(agencyindex == 4)
										continue;
									
									XmlElement ChkElementDetails;
									ChkElementDetails = AcordPDFXML.CreateElement("CHECKDISTIRBUTIONDETAILS");
									ChkElement.AppendChild(ChkElementDetails);
									ChkElementDetails.SetAttribute(fieldType,fieldTypeNormal);
									ChkElementDetails.SetAttribute(id,stubindex.ToString());
									
									
									if(stubindex != 0)
									ChkElementDetails.InnerXml += "<reference " +  fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(reference) +"</reference>";
									ChkElementDetails.InnerXml += "<description " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(desc) +"</description>";
									ChkElementDetails.InnerXml += "<account " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(acc) +"</account>";
									ChkElementDetails.InnerXml += "<amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(amt) +"</amount>";
									stubindex++;
								}
								CheckPageElement.InnerXml += "<agency_code " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGENCY_CODE"].ToString()) +"</agency_code>";
								break;
							}
								//Premium Refund Checks for Return Premium Payment
							case "2474":
							{
								CheckPageElement.InnerXml += "<reference " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHANGE_EFF_DATE"].ToString())+ "</reference>";
								//									if(ChkDetail["PROCESS_ID"].ToString() == "3" || ChkDetail["PROCESS_ID"].ToString() == "14")
								//										CheckPageElement.InnerXml += "<description " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["chk_description"].ToString()) +"</description>";
								//									else if(ChkDetail["PROCESS_ID"].ToString() == "2" || ChkDetail["PROCESS_ID"].ToString() == "12")
								CheckPageElement.InnerXml += "<description " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["chk_description"].ToString()) +"</description>";
								CheckPageElement.InnerXml += "<account " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["OFFSET_ACC_ID"].ToString()) +"</account>";
								CheckPageElement.InnerXml += "<amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</amount>";
								CheckPageElement.InnerXml += "<total_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</total_amount>";

								CheckPageElement.InnerXml += "<agency " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGN_NAME"].ToString()) +"</agency>";
								CheckPageElement.InnerXml += "<Disp_ins_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGN_ADDRESS_LINE_1"].ToString()) +"</Disp_ins_add1>";
								CheckPageElement.InnerXml += "<Disp_ins_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGN_ADDRESS_LINE_2"].ToString()) +"</Disp_ins_add2>";

								break;
							}
								//Premium Refund Checks for Over Payment
							case "9935":
							{
								CheckPageElement.InnerXml += "<reference " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["DEPOSIT_TRAN_DATE"].ToString()+ "-" + ChkDetail["DEPOSIT_NUMBER"].ToString()) +"</reference>";
								CheckPageElement.InnerXml += "<description " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["chk_description"].ToString()) +"</description>";
								CheckPageElement.InnerXml += "<account " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["OFFSET_ACC_ID"].ToString()) +"</account>";
								CheckPageElement.InnerXml += "<amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</amount>";
								CheckPageElement.InnerXml += "<total_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</total_amount>";

								CheckPageElement.InnerXml += "<agency " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGN_NAME"].ToString()) +"</agency>";
								CheckPageElement.InnerXml += "<Disp_ins_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGN_ADDRESS_LINE_1"].ToString()) +"</Disp_ins_add1>";
								CheckPageElement.InnerXml += "<Disp_ins_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGN_ADDRESS_LINE_2"].ToString()) +"</Disp_ins_add2>";

								break;
							}
								//Premium Refund Checks for Suspense Amount
							case "9936":
							{
								CheckPageElement.InnerXml += "<reference " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["DEPOSIT_TRAN_DATE"].ToString()+ "-" + ChkDetail["DEPOSIT_NUMBER"].ToString()) +"</reference>";
								CheckPageElement.InnerXml += "<description " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["chk_description"].ToString()) +"</description>";
								CheckPageElement.InnerXml += "<account " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["OFFSET_ACC_ID"].ToString()) +"</account>";
								CheckPageElement.InnerXml += "<amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</amount>";
								CheckPageElement.InnerXml += "<total_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</total_amount>";
								CheckPageElement.InnerXml += "<agency " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["INSURED"].ToString()) +"</agency>";
								//CheckPageElement.InnerXml += "<Disp_ins_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["InsAddress"].ToString()) +"</Disp_ins_add1>";
								CheckPageElement.InnerXml += "<Disp_ins_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["InsAddress1"].ToString()) +"</Disp_ins_add1>";
								CheckPageElement.InnerXml += "<Disp_ins_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["InsAddress2"].ToString()) +"</Disp_ins_add2>";
								break;
							}
								//Claims Checks	
							case "9937":
							{
								if(ChkDetail["CHECK_NOTE"].ToString() != "")
									CheckPageElement.InnerXml += "<Memo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Memo: " + ChkDetail["CHECK_NOTE"].ToString()) +"</Memo>";
								#region Claims Description
								if(DSTempDataSet.Tables.Count > 1)
								{
									CheckPageElement.InnerXml += "<description " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Insured:  " + DSTempDataSet.Tables[1].Rows[0]["CLAIM_INSURED"].ToString()) +"</description>";
									CheckPageElement.InnerXml += "<description1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Agency:  " + DSTempDataSet.Tables[1].Rows[0]["CLAIM_AGENCY"].ToString()) +"</description1>";
									CheckPageElement.InnerXml += "<description2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Policy No.:  " + DSTempDataSet.Tables[1].Rows[0]["POLICY_NUMBER"].ToString()) +"</description2>";
									CheckPageElement.InnerXml += "<description3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Claims #:  " + DSTempDataSet.Tables[1].Rows[0]["CLAIM_NUMBER"].ToString()) +"</description3>";
									CheckPageElement.InnerXml += "<description4 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Check #:  " + DSTempDataSet.Tables[1].Rows[0]["CHECK_NUMBER"].ToString()) +"</description4>";
									CheckPageElement.InnerXml += "<description5 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Description:  " + DSTempDataSet.Tables[1].Rows[0]["DESCRIPTION"].ToString()) +"</description5>";
									CheckPageElement.InnerXml += "<description6 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Date of Check Printing:  " + DSTempDataSet.Tables[1].Rows[0]["CHECK_PRINTED_DATE"].ToString()) +"</description6>";
									CheckPageElement.InnerXml += "<description7 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Activity Reasons:  " + DSTempDataSet.Tables[1].Rows[0]["ACTIVITY_REASON"].ToString()) +"</description7>";
									CheckPageElement.InnerXml += "<description8 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Coverages:  " + DSTempDataSet.Tables[1].Rows[0]["COVERAGES_DESC"].ToString().Replace("\r\n\t\t\t\t","")) +"</description8>";
									CheckPageElement.InnerXml += "<description9 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Date of Loss:  " + DSTempDataSet.Tables[1].Rows[0]["LOSS_DATE"].ToString()) +"</description9>";
									CheckPageElement.InnerXml += "<description10 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Amount:  " + ChkDetail["CHECK_AMOUNT"].ToString()) +"</description10>";
									CheckPageElement.InnerXml += "<description11 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Invoice #:  " + DSTempDataSet.Tables[1].Rows[0]["INVOICE_NUMBER"].ToString()) +"</description11>";
								}
								#endregion Claims Description

								break;
							}
								//Vendor Checks
							case "9938":
							{
								int vendindex = 0;
								string vendsuffix = "";
								
								// node to add extra page to check
								XmlElement ChkElement;
								ChkElement = AcordPDFXML.CreateElement("CHECKDISTIRBUTION");
								CheckPageElement.AppendChild(ChkElement);
								ChkElement.SetAttribute(fieldType,fieldTypeMultiple);
								ChkElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHKPAGE"));
								ChkElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CHKPAGE"));
								ChkElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGEEXTN"));
								ChkElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CHKPAGEEXTN"));
								
				
								int iCounter =0;
								
								foreach(DataRow ChkDetail1 in DSTempDataSet.Tables[0].Rows)
								{
									XmlElement ChkElementDetails;
									ChkElementDetails = AcordPDFXML.CreateElement("CHECKDISTIRBUTIONDETAILS");
									ChkElement.AppendChild(ChkElementDetails);
									ChkElementDetails.SetAttribute(fieldType,fieldTypeNormal);
									ChkElementDetails.SetAttribute(id,iCounter.ToString());
									
									////////////////////////////////////
									ChkElementDetails.InnerXml += "<DispHead1 " + fieldType + "=\"" + fieldTypeText + "\">" + "Wolverine Mutual Insurance Company" + "</DispHead1>";
									ChkElementDetails.InnerXml += "<DispHead2 " + fieldType + "=\"" + fieldTypeText + "\">" + "One Wolverine Way" + "</DispHead2>";
									ChkElementDetails.InnerXml += "<DispAdd1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("P.O. Box 530") + "</DispAdd1>";
									ChkElementDetails.InnerXml += "<DispAdd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Dowagiac, Michigan 49047-0530") + "</DispAdd2>";
									
									ChkElementDetails.InnerXml += "<BankName " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_NAME"].ToString()) + "</BankName>";
									ChkElementDetails.InnerXml += "<DispBankInfo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_ADDRESS"].ToString()) + "</DispBankInfo>";
									ChkElementDetails.InnerXml += "<ChkNo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_NUMBER"].ToString()) + "</ChkNo>";
									ChkElementDetails.InnerXml += "<DispTAmt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["check_amt"].ToString()) + "</DispTAmt>";
									ChkElementDetails.InnerXml += "<Dispamount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) + "</Dispamount>";
						
									ChkElementDetails.InnerXml += "<bank_no " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_NUMBER_TOP"].ToString()) + "</bank_no>";
									ChkElementDetails.InnerXml += "<check_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_DATE"].ToString()) + "</check_date>";
									ChkElementDetails.InnerXml += "<check_note " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_NOTE"].ToString()) + "</check_note>";
					
									if(ChkDetail["COMM_TYPE"].ToString().Trim() != "CAC")
									{
										ChkElementDetails.InnerXml += "<Payee_entity_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name>";
										ChkElementDetails.InnerXml += "<Payee_entity_name_footer " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name_footer>";
										ChkElementDetails.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_1"].ToString()) + "</Disp_payee_add1>";
										ChkElementDetails.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_add2>";
									}
									else
									{
										ChkElementDetails.InnerXml += "<Payee_entity_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name>";
										ChkElementDetails.InnerXml += "<csr_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</csr_name>";
										ChkElementDetails.InnerXml += "<Payee_entity_name_footer " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGENCY_NAME"].ToString()) + "</Payee_entity_name_footer>";
										ChkElementDetails.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_1"].ToString()) + "</Disp_payee_add1>";
										ChkElementDetails.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_add2>";
									}
									
									if(vendindex > 0)
										vendsuffix = vendindex.ToString();
									else
										vendsuffix = "";
									ChkElementDetails.InnerXml += "<reference " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["REF_INVOICE_NO"].ToString()) + "</reference>";
									ChkElementDetails.InnerXml += "<description "+ fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["REF_INVOICE_REF_NO"].ToString()) +"</description>";
									ChkElementDetails.InnerXml += "<account " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["OFFSET_ACC_ID"].ToString()) +"</account>";
									ChkElementDetails.InnerXml += "<amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["AMOUNT_TO_APPLY"].ToString()) +"</amount>";
									if(vendindex == 0)
										ChkElementDetails.InnerXml += "<total_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</total_amount>";
									vendindex++;
									iCounter++;
//									if(vendindex > 14)
//										break;
								}
								break;
							}
								//Miscellaneous (Other) Checks
							case "9940":
							{
								int miscindex = 0;
								string miscsuffix = "";

								// node to add extra page to check
								XmlElement ChkElement;
								ChkElement = AcordPDFXML.CreateElement("CHECKDISTIRBUTION");
								CheckPageElement.AppendChild(ChkElement);
								ChkElement.SetAttribute(fieldType,fieldTypeMultiple);
								ChkElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHKPAGE"));
								ChkElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CHKPAGE"));
								ChkElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGEEXTN"));
								ChkElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CHKPAGEEXTN"));
								
				
								int iCounter =0;
								foreach(DataRow ChkDetail1 in DSTempDataSet.Tables[0].Rows)
								{
									
	
									
									XmlElement ChkElementDetails;
									ChkElementDetails = AcordPDFXML.CreateElement("CHECKDISTIRBUTIONDETAILS");
									ChkElement.AppendChild(ChkElementDetails);
									ChkElementDetails.SetAttribute(fieldType,fieldTypeNormal);
									ChkElementDetails.SetAttribute(id,iCounter.ToString());
									
									////////////////////////////////////
									ChkElementDetails.InnerXml += "<DispHead1 " + fieldType + "=\"" + fieldTypeText + "\">" + "Wolverine Mutual Insurance Company" + "</DispHead1>";
									ChkElementDetails.InnerXml += "<DispHead2 " + fieldType + "=\"" + fieldTypeText + "\">" + "One Wolverine Way" + "</DispHead2>";
									ChkElementDetails.InnerXml += "<DispAdd1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("P.O. Box 530") + "</DispAdd1>";
									ChkElementDetails.InnerXml += "<DispAdd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Dowagiac, Michigan 49047-0530") + "</DispAdd2>";
									
									ChkElementDetails.InnerXml += "<BankName " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_NAME"].ToString()) + "</BankName>";
									ChkElementDetails.InnerXml += "<DispBankInfo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_ADDRESS"].ToString()) + "</DispBankInfo>";
									ChkElementDetails.InnerXml += "<ChkNo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_NUMBER"].ToString()) + "</ChkNo>";
									ChkElementDetails.InnerXml += "<DispTAmt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["check_amt"].ToString()) + "</DispTAmt>";
									ChkElementDetails.InnerXml += "<Dispamount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) + "</Dispamount>";
						
									ChkElementDetails.InnerXml += "<bank_no " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_NUMBER_TOP"].ToString()) + "</bank_no>";
									ChkElementDetails.InnerXml += "<check_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_DATE"].ToString()) + "</check_date>";
									ChkElementDetails.InnerXml += "<check_note " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_NOTE"].ToString()) + "</check_note>";
					
									if(ChkDetail["COMM_TYPE"].ToString().Trim() != "CAC")
									{
										ChkElementDetails.InnerXml += "<Payee_entity_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name>";
										ChkElementDetails.InnerXml += "<Payee_entity_name_footer " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name_footer>";
										ChkElementDetails.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_1"].ToString()) + "</Disp_payee_add1>";
										ChkElementDetails.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_add2>";
									}
									else
									{
										ChkElementDetails.InnerXml += "<Payee_entity_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name>";
										ChkElementDetails.InnerXml += "<csr_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</csr_name>";
										ChkElementDetails.InnerXml += "<Payee_entity_name_footer " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGENCY_NAME"].ToString()) + "</Payee_entity_name_footer>";
										ChkElementDetails.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_1"].ToString()) + "</Disp_payee_add1>";
										ChkElementDetails.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_add2>";
									}
									////////////////////////////////////
									if(miscindex > 0)
										miscsuffix = miscindex.ToString();
									else
										miscsuffix = "";
									ChkElementDetails.InnerXml += "<reference " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["NOTE"].ToString()) + "</reference>";
									ChkElementDetails.InnerXml += "<description " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["ACC_DESCRIPTION"].ToString()) +"</description>";
									ChkElementDetails.InnerXml += "<account " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["DIST_ACC_NUM"].ToString()) +"</account>";
									ChkElementDetails.InnerXml += "<amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["DISTRIBUTION_AMOUNT"].ToString()) +"</amount>";
									if(miscindex == 0)
										ChkElementDetails.InnerXml += "<total_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</total_amount>";
									miscindex++;
									iCounter++;
								}
									
								break;
							}
								//Reinsurance Premium Checks
							case "9945":
							{
								int reinindex = 0;
								string reinsuffix = "";

								// node to add extra page to check
								XmlElement ChkElement;
								ChkElement = AcordPDFXML.CreateElement("CHECKDISTIRBUTION");
								CheckPageElement.AppendChild(ChkElement);
								ChkElement.SetAttribute(fieldType,fieldTypeMultiple);
								ChkElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("CHKPAGE"));
								ChkElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("CHKPAGE"));
								ChkElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("CHKPAGEEXTN"));
								ChkElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("CHKPAGEEXTN"));
								
								int iCounter =0;
								foreach(DataRow ChkDetail1 in DSTempDataSet.Tables[0].Rows)
								{
									XmlElement ChkElementDetails;
									ChkElementDetails = AcordPDFXML.CreateElement("CHECKDISTIRBUTIONDETAILS");
									ChkElement.AppendChild(ChkElementDetails);
									ChkElementDetails.SetAttribute(fieldType,fieldTypeNormal);
									ChkElementDetails.SetAttribute(id,iCounter.ToString());
									
									////////////////////////////////////
									ChkElementDetails.InnerXml += "<DispHead1 " + fieldType + "=\"" + fieldTypeText + "\">" + "Wolverine Mutual Insurance Company" + "</DispHead1>";
									ChkElementDetails.InnerXml += "<DispHead2 " + fieldType + "=\"" + fieldTypeText + "\">" + "One Wolverine Way" + "</DispHead2>";
									ChkElementDetails.InnerXml += "<DispAdd1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("P.O. Box 530") + "</DispAdd1>";
									ChkElementDetails.InnerXml += "<DispAdd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Dowagiac, Michigan 49047-0530") + "</DispAdd2>";
									
									ChkElementDetails.InnerXml += "<BankName " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_NAME"].ToString()) + "</BankName>";
									ChkElementDetails.InnerXml += "<DispBankInfo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_ADDRESS"].ToString()) + "</DispBankInfo>";
									ChkElementDetails.InnerXml += "<ChkNo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_NUMBER"].ToString()) + "</ChkNo>";
									ChkElementDetails.InnerXml += "<DispTAmt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["check_amt"].ToString()) + "</DispTAmt>";
									ChkElementDetails.InnerXml += "<Dispamount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) + "</Dispamount>";
						
									ChkElementDetails.InnerXml += "<bank_no " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["BANK_NUMBER_TOP"].ToString()) + "</bank_no>";
									ChkElementDetails.InnerXml += "<check_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_DATE"].ToString()) + "</check_date>";
									ChkElementDetails.InnerXml += "<check_note " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_NOTE"].ToString()) + "</check_note>";
					
									if(ChkDetail["COMM_TYPE"].ToString().Trim() != "CAC")
									{
										ChkElementDetails.InnerXml += "<Payee_entity_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name>";
										ChkElementDetails.InnerXml += "<Payee_entity_name_footer " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name_footer>";
										ChkElementDetails.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_1"].ToString()) + "</Disp_payee_add1>";
										ChkElementDetails.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_add2>";
									}
									else
									{
										ChkElementDetails.InnerXml += "<Payee_entity_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</Payee_entity_name>";
										ChkElementDetails.InnerXml += "<csr_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["PAYEE_ENTITY_NAME"].ToString()) + "</csr_name>";
										ChkElementDetails.InnerXml += "<Payee_entity_name_footer " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGENCY_NAME"].ToString()) + "</Payee_entity_name_footer>";
										ChkElementDetails.InnerXml += "<Disp_payee_add1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_1"].ToString()) + "</Disp_payee_add1>";
										ChkElementDetails.InnerXml += "<Disp_payee_add2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["ADDRESS_LINE_2"].ToString()) + "</Disp_payee_add2>";
									}
									////////////////////////////////////
									if(reinindex > 0)
										reinsuffix = reinindex.ToString();
									else
										reinsuffix = "";
									ChkElementDetails.InnerXml += "<reference " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["REINS_ID"].ToString()) + "</reference>";
									ChkElementDetails.InnerXml += "<description " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["NOTE"].ToString()) +"</description>";
									ChkElementDetails.InnerXml += "<account " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["DIST_ACC_NUM"].ToString()) +"</account>";
									ChkElementDetails.InnerXml += "<amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail1["DISTRIBUTION_AMOUNT"].ToString()) +"</amount>";
									if(reinindex == 0)
										ChkElementDetails.InnerXml += "<total_amount" + reinsuffix + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CHECK_AMOUNT"].ToString()) +"</total_amount>";
									reinindex++;
									iCounter++;
								}
								break;
							}

						}
						#endregion check type
						//Itrack No.  3586  By  Manoj Rathore  On  12th Feb 2008
						//							if(ChkDetail["CHECK_TYPE"].ToString() != "9938" && ChkDetail["CHECK_TYPE"].ToString() != "9936")
						//								CheckPageElement.InnerXml += "<agency " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["AGENCY_DISPLAY_NAME"].ToString()) +"</agency>";

						//		chkInfoCounter++;


						
					}
						preChkType = checkType;
						chkCounter++;
					}
				}
					chkCounter=0;
			}
		}

		if(blank_num == 0)
		return "0";
		else if(blank_num == 1 && chkCounter == 0)
		return "1";
		else if(blank_num == 1 && chkCounter > 0)
		return "2";
		else
		return "0";
	}

}

}