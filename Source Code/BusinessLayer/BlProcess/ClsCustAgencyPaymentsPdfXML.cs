using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Swastika Gaur</CreatedBy>
	/// <Dated>10th Jul'2007</Dated>
	/// <Purpose>Generate DB receipt for Customer payements from Agency</Purpose>
	/// <summary>
	/// <ModifiedBy>Mohit Agarwal</CreatedBy>
	/// <Dated>9th Aug'2007</Dated>
	/// <Purpose>Generate input XML for Customer payements from Agency</Purpose>
	/// </summary>
	public class ClsCustAgencyPaymentsPdfXML : ClsCommonPdfXML
	{
		#region Declarations
		private string polList = "";
		private DataWrapper gobjSqlHelper;
		private XmlElement DecPageRootElement;
		#endregion

		public ClsCustAgencyPaymentsPdfXML(string pollist, string lStrCalledFrom)
		{
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			polList = pollist;
			DSTempDataSet = new DataSet();
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			
			SetPDFVersionLobNode("CHK",System.DateTime.Now);
		}

		public string getCustAgencyPaymentsPDFXml(ref string custom_info)
		{
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementForAllRootPDFs();
			CreateCustAgencyPaymentsXML(ref custom_info);
			return AcordPDFXML.OuterXml;
		}

		private void createRootElementForAllRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);
			DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("RECPPAGE"));
			
		}

		#region Creating Cust Agency Payments Xml 
		private void CreateCustAgencyPaymentsXML(ref string custom_info)
		{
			String [] chkRows = polList.Split('~');

			if(chkRows.Length > 0)
			{
				//int chkCounter=0;
				#region Cust Agency Page Root Element
				XmlElement CustAgyRootElement;
				CustAgyRootElement = AcordPDFXML.CreateElement("CUSTAGENCYDETAILS");
				DecPageRootElement.AppendChild(CustAgyRootElement);
				CustAgyRootElement.SetAttribute(fieldType,fieldTypeMultiple);
				CustAgyRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("RECIPPAGE"));
				CustAgyRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("RECIPPAGE"));
				CustAgyRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("RECIPPAGEEXTN"));
				CustAgyRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("RECIPPAGEEXTN"));
				#endregion

				for ( int ctr=0;  ctr < chkRows.Length ; ctr++)
				{
					string polNum = "";
					string amt = "0.00";
					string [] polNum_Amt = chkRows[ctr].Split('|');
					if(polNum_Amt.Length >1 )
					{
						polNum = polNum_Amt[0];
						amt = polNum_Amt[1];
						if (amt.IndexOf(".")<0)
						{
							amt+= ".00";
						}
					}
					else
						polNum = polNum_Amt[0];

					string userID = System.Web.HttpContext.Current.Session["userId"].ToString();
					DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPolicyInfoFromPolNumberPDF '" + polNum + "','" + userID + "'");

					if (DSTempDataSet.Tables[0].Rows.Count > 0 )
					{
						#region CustAgencyPage Element
						DataRow ChkDetail = DSTempDataSet.Tables[0].Rows[0];

						XmlElement CustAgyPageElement;
						CustAgyPageElement = AcordPDFXML.CreateElement("CUSTAGYDETAILSINFO");
						CustAgyRootElement.AppendChild(CustAgyPageElement);
						CustAgyPageElement.SetAttribute(fieldType,fieldTypeNormal);
						CustAgyPageElement.SetAttribute(id,ctr.ToString());

						CustAgyPageElement.InnerXml += "<recp_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</recp_date>";
						CustAgyPageElement.InnerXml += "<recp_polNum " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(polNum) + "</recp_polNum>";
						CustAgyPageElement.InnerXml += "<recp_nam " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CUSTOMER_NAME"].ToString()) + "</recp_nam>";
						string custAddr = ChkDetail["CUSTOMER_ADDRESS1"].ToString();
						if(ChkDetail["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
							custAddr += ", " + ChkDetail["CUSTOMER_ADDRESS2"].ToString();
						CustAgyPageElement.InnerXml += "<recp_addr " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(custAddr) + "</recp_addr>";
						CustAgyPageElement.InnerXml += "<recp_city " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ChkDetail["CUSTOMER_CITY"].ToString() + " " + ChkDetail["STATE_CODE"].ToString() + " " + ChkDetail["CUSTOMER_ZIP"].ToString()) + "</recp_city>";
						//CustAgyPageElement.InnerXml += "<recp_Agencynam " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Your agency (" + ChkDetail["AGENCY_DISPLAY_NAME"].ToString() +") has made a payment of $" + amt + " on your behalf.") + "</recp_Agencynam>";
						/*
						CustAgyPageElement.InnerXml += "<recp_Agencynam " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("A payment of $" + amt +" was recevied by " + ChkDetail["AGENCY_DISPLAY_NAME"].ToString() + " on " + DateTime.Now.ToString("MM/dd/yyyy")) + "</recp_Agencynam>";

						//GET USER ID FROM SESSION / IN CASE OF EOD FETCHING EOD USER: 
						// Itrack NO. 3430
							  
						string userID = System.Web.HttpContext.Current.Session["userId"].ToString();
						if(IsEODProcess)
                            userID = EODUserID.ToString();

						DataSet ds = null;
						ds = BlCommon.ClsUser.GetUserName(userID);
						string UserName = ds.Tables[0].Rows[0]["USERNAME"].ToString();
						*/

						string AgencyName="", UserName =""; 
						
						if(DSTempDataSet.Tables[1].Rows[0]["AGENCY_DISPLAY_NAME"] != DBNull.Value)
						{
							AgencyName = DSTempDataSet.Tables[1].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
						}
						
						if(DSTempDataSet.Tables[1].Rows[0]["USERNAME"] != DBNull.Value)
						{
							UserName = DSTempDataSet.Tables[1].Rows[0]["USERNAME"].ToString();
						}

						CustAgyPageElement.InnerXml += "<recp_Agencynam " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("A payment of $" + amt +" was received by " + AgencyName  + " on " + DateTime.Now.ToString("MM/dd/yyyy")) + "</recp_Agencynam>";
						CustAgyPageElement.InnerXml += "<recp_usernam " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(UserName.Trim()) + "</recp_usernam>";


						//CustAgyPageElement.InnerXml += "<recp_usernam " + fieldType + "=\"" + fieldTypeText + "\">" + UserName.ToString().Trim() + "</recp_usernam>";
						//CustAgyPageElement.InnerXml += "<recp_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("$" + amt) + "</recp_amt>";

						custom_info += ";Client Name: " + ChkDetail["CUSTOMER_NAME"].ToString();
						custom_info += ";Policy Number: " + polNum;
						custom_info += ";Amount: $" + amt + ";";
					}
					#endregion CustAgencyPage Element
				}
			}
		}
		#endregion
	}
}