using System;
using System.Xml;
using System.Data.SqlClient;
using Cms.DataLayer;
using System.Data;
using Cms.Model.Quote;
using System.Configuration;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsWatercraft.
	/// </summary>
	public class ClsWatercraft : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		public ClsWatercraft()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string PrepareBoatAcordXml(string CustomerId,string QuoteId,string AcordXmlPath,string StateCd,string QuoteNumber)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			DataSet ObjDs;

			string QuickQuoteXml = new ClsQuickQuote().GetQuickQuoteXml(CustomerId,QuoteId);
			string AcordXml="";
			
			XmlDocument AcordDom=new XmlDocument();
			XmlDocument QuoteDom=new XmlDocument();

			if (QuickQuoteXml.Trim()!="")
			{
				QuoteDom.LoadXml(QuickQuoteXml);
				AcordDom.Load(AcordXmlPath);
				XmlNode CustomerNode = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/WatercraftPolicyQuoteInqRq/InsuredOrPrincipal");
				
				//Customer Info
				objWrapper.AddParameter("@CustomerID",CustomerId);
				ObjDs = objWrapper.ExecuteDataSet("Proc_GetCustomerDetails");

				string strCustomerStateCode = ObjDs.Tables[0].Rows[0]["CUSTOMER_STATE_CODE"].ToString().Trim();
				string strCustomerStateID = ObjDs.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString().Trim();	

				if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11110")
				{
					CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/Surname").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/GivenName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
				}
				else if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11109")
				{
					CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
					
				}
				
				//CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/AddrTypeCd").InnerText = "StreetAddress";
				XmlNode Node1 = CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/AddrTypeCd");
				Node1.InnerText = "StreetAddress";
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr1").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr2").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/City").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString().Trim();
				//CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateProvCd").InnerText = StateCd;
				
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateProvCd").InnerText = StateCd;
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateID").InnerText = strCustomerStateID;
				//CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = QuoteDom.SelectSingleNode("//zipCode").InnerText.ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/CountryCd").InnerText = "us";
								
				CustomerNode.SelectSingleNode("InsuredOrPrincipalInfo").Attributes["id"].Value = CustomerId;

				//Customer Info End
				//Agency Info
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/WatercraftPolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString().Trim();
				//add by kranti on 17th april 2007 for agency id
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/WatercraftPolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialId").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_ID"].ToString().Trim();

				//Agency Info End

				//Pers Policy Info Start
				XmlNode PersPolicyNode = AcordDom.SelectSingleNode("//PersPolicy");
				PersPolicyNode.SelectSingleNode("ControllingStateProvCd").InnerText = StateCd;

				PersPolicyNode.SelectSingleNode("ContractTerm/EffectiveDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();

				
				//AppTerms add by kranti
				if(QuoteDom.SelectSingleNode("//POLICYTERMS")!= null)
				PersPolicyNode.SelectSingleNode("ContractTerm/AppTerms").InnerText = QuoteDom.SelectSingleNode("//POLICYTERMS").InnerText;


				string effDate = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
				DateTime dtExpDate = DateTime.Parse(effDate).AddMonths(int.Parse(QuoteDom.SelectSingleNode("//POLICYTERMS").InnerText.ToString()));
				PersPolicyNode.SelectSingleNode("ContractTerm/ExpirationDt").InnerText = dtExpDate.ToString();
				
				PersPolicyNode.SelectSingleNode("OriginalInceptionDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
				
				PersPolicyNode.SelectSingleNode("QuoteInfo/CompanysQuoteNumber").InnerText = QuoteNumber;
				
				PersPolicyNode.SelectSingleNode("QuoteInfo/InitialQuoteRequestDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
				//Seting Home Boat Discount  : 15 feb 2006
				PersPolicyNode.SelectSingleNode("CreditSurcharge[@id='BOATHOMEDISCOUNT']").InnerText = QuoteDom.SelectSingleNode("//BOATHOMEDISC").InnerText.ToString().Trim();
				//

				// set values for policynumber and version in case of 'attached to home' : 14 feb 2006				
				XmlNode nodTemp = QuoteDom.SelectSingleNode("//ATTACHTOWOLVERINE");
				if(nodTemp!= null && nodTemp.InnerText.ToUpper().Trim()=="Y")
				{	
					PersPolicyNode.SelectSingleNode("PolicyNumber").InnerText = QuoteDom.SelectSingleNode("//HOMEAPPNUMBER").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("PolicyVersion").InnerText = QuoteDom.SelectSingleNode("//HOMEPPVERID").InnerText.ToString().Trim();

				}
				//End
				//PersPolicyNode.SelectSingleNode("CreditScoreInfo/CreditScore").InnerText = QuoteDom.SelectSingleNode("//INSURANCESCORE").InnerText.ToString().Trim();
				
				//BOAT IMPLEMENTATION	
				XmlNode BoatNode = AcordDom.SelectSingleNode("//WatercraftLineBusiness/Watercraft");
				string BoatBlankXml = BoatNode.OuterXml;
				AcordDom.SelectSingleNode("//WatercraftLineBusiness").RemoveChild(BoatNode);
				
				//XmlNode dwellNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']");
				//XmlNode quoteNode = null;
				//XmlNode acordNode = null;
				//XmlNode sampleNode = BoatNode.SelectSingleNode("Coverage[CoverageCd='Sample']");

				foreach (XmlNode Node in QuoteDom.SelectNodes("//BOATS/*"))
				{
					XmlDocument BoatDom = new XmlDocument();
					string strInsuredValue=Node.SelectSingleNode("MARKETVALUE").InnerText.ToString().Trim();
					BoatDom.LoadXml(BoatBlankXml);

					BoatDom.FirstChild.Attributes["id"].Value="W" + Node.Attributes["ID"].Value.ToString().Trim();
					
					BoatDom.FirstChild.SelectSingleNode("Make").InnerText=Node.SelectSingleNode("MANUFACTURER").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("Model").InnerText=Node.SelectSingleNode("MODEL").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("Year").InnerText=Node.SelectSingleNode("YEAR").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("SerialNumber").InnerText=Node.SelectSingleNode("SERIALNUMBER").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("WaterUnitTypeCd").InnerText=Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("Length/NumUnits").InnerText=Node.SelectSingleNode("LENGTH").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("Horsepower/NumUnits").InnerText=Node.SelectSingleNode("HORSEPOWER").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("WatersNavigatedCd").InnerText=Node.SelectSingleNode("WATERSCODE").InnerText.ToString().Trim();
					//Added By Ravindra(09-05-2006) For Coverage Type Basis Field
					BoatDom.FirstChild.SelectSingleNode("CoverageTypeBasisCd").InnerText=Node.SelectSingleNode("COVERAGEBASIS").InnerText.ToString().Trim();	

					BoatDom.FirstChild.SelectSingleNode("Speed/NumUnits").InnerText=Node.SelectSingleNode("CAPABLESPEED").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("PresentValueAmt/Amt").InnerText=strInsuredValue;
					
					// Commented by Swastika on 5th May'06 for Gen Iss 2674 &  2675
					//BoatDom.FirstChild.SelectSingleNode("WeightCapacity/NumUnits").InnerText=Node.SelectSingleNode("WEIGHT").InnerText.ToString().Trim();
                    
					BoatDom.FirstChild.SelectSingleNode("HullMaterialTypeCd").InnerText = Node.SelectSingleNode("CONSTRUCTIONCODE").InnerText.ToString().Trim();
					
					

					//County - Area of Operation Commented on 30 may : 
					/*string stateName= QuoteDom.SelectSingleNode("//STATENAME").InnerText.ToString().Trim().ToUpper();
					XmlNode countyNode = Node.SelectSingleNode("COUNTYOFOPERATION"); 

					int intForAllOtherMI=2584;//Lookup Unique id for all other MI
					int intForAllOtherWI=11442;//Lookup Unique id for all other WI
					int intForAllOtherIN=11567;//Lookup Unique id for all other IN
					int intForOakland=6781;
					int intForClairandWayne=6782;
					int intForMacomb=2910;
					int intForKenosha=11443;
					 
					int intForCounty=0;
					if(countyNode != null)
					{
						
						string countyText=countyNode.InnerText.Trim().ToUpper();

						// Text of all
						if (countyText=="ALL OTHER" && stateName =="MICHIGAN" )
						{
							intForCounty = intForAllOtherMI;
						}
						else if(countyText=="ALL OTHER" && stateName =="INDIANA")
						{
							intForCounty = intForAllOtherIN;
						}
						else if(countyText =="ALL OTHER" && stateName =="WISCONSIN")
						{
							intForCounty = intForAllOtherWI;
						}
						else if(countyText=="MACOMB")
						{
							intForCounty = intForMacomb;
						}
						else if(countyText=="OAKLAND")
						{
							intForCounty = intForOakland;
						}
						else if(countyText.IndexOf("ST. CLAIR")!=-1)
						{
							intForCounty = intForClairandWayne;
						}
						else if(countyText.IndexOf("KENOSHA COUNTY")!=-1)
						{
							intForCounty = intForKenosha;
						}
					}
					BoatDom.FirstChild.SelectSingleNode("County").InnerText=intForCounty.ToString();*/

					//new 
					BoatDom.FirstChild.SelectSingleNode("County").InnerText = Node.SelectSingleNode("COUNTYCODE").InnerText.ToString().Trim();
					//


					
					//Discounts and Surcharges	
					XmlNode disNode = Node.SelectSingleNode("DIESELENGINE"); 
					if ( disNode  != null )
					{
						if(disNode.InnerText.Trim().ToUpper()=="N")
						{
							BoatDom.FirstChild.RemoveChild(BoatDom.FirstChild.SelectSingleNode("CreditSurcharge[@id='DIESELENGINE']"));

						}
					}
					disNode = Node.SelectSingleNode("SHORESTATION"); 
					if ( disNode  != null )
					{
						if(disNode.InnerText.Trim().ToUpper()=="N")
						{
							BoatDom.FirstChild.RemoveChild(BoatDom.FirstChild.SelectSingleNode("CreditSurcharge[@id='SHORESTATION']"));

						}
					}
					disNode = Node.SelectSingleNode("HALONFIRE"); 
					if ( disNode  != null )
					{
						if(disNode.InnerText.Trim().ToUpper()=="N")
						{
							BoatDom.FirstChild.RemoveChild(BoatDom.FirstChild.SelectSingleNode("CreditSurcharge[@id='HALONFIRE']"));

						}
					}
					disNode = Node.SelectSingleNode("LORANNAVIGATIONSYSTEM"); 
					if ( disNode  != null )
					{
						if(disNode.InnerText.Trim().ToUpper()=="N")
						{
							BoatDom.FirstChild.RemoveChild(BoatDom.FirstChild.SelectSingleNode("CreditSurcharge[@id='LORANNAVIGATIONSYSTEM']"));

						}
					}
					disNode = Node.SelectSingleNode("DUALOWNERSHIP"); 
					if ( disNode  != null )
					{
						if(disNode.InnerText.Trim().ToUpper()=="N")
						{
							BoatDom.FirstChild.RemoveChild(BoatDom.FirstChild.SelectSingleNode("CreditSurcharge[@id='DUALOWNERSHIP']"));

						}
					}

					
					disNode = Node.SelectSingleNode("REMOVESAILBOAT"); 
					if ( disNode  != null )
					{
						if(disNode.InnerText.Trim().ToUpper()=="N")
						{
							BoatDom.FirstChild.RemoveChild(BoatDom.FirstChild.SelectSingleNode("CreditSurcharge[@id='REMOVESAILBOAT']"));

						}
					}
					// ******************************




					//Coverages*************************************************************************

					//Deductible
					XmlNode covNode = Node.SelectSingleNode("DEDUCTIBLE");
					if ( covNode  != null )
					{
						if ( covNode.InnerText.Trim() != "" )
						{
							string strDeductible =covNode.InnerText.Trim();
							string[] arrDed = strDeductible.Split('-');
							string strPercentage="";
							string strAmount ="";
							
							if (arrDed.Length ==2 )
							{
								strPercentage = " -" + arrDed[0];
								strAmount= arrDed[1];
							}
							else if (arrDed.Length == 1 )
							{
								strAmount = arrDed[0];
							}
							
							// If Trailer Deductible then don't send in Coverage part. Asfa Praveen - 02/july/2007
							if (Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "TRAI" || Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "JT" || Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "WRT")
							{
								BoatDom.FirstChild.SelectSingleNode("TrailerDedId").InnerText = ClsCommon.GetTrailerDedId(Convert.ToDateTime(QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText),Convert.ToDecimal(strAmount.Trim()),QuoteDom.SelectSingleNode("//STATENAME").InnerText.ToString(),Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim()).ToString();
								BoatDom.FirstChild.SelectSingleNode("TrailerDed").InnerText = strAmount;
								if (arrDed.Length ==2 )
								{
									BoatDom.FirstChild.SelectSingleNode("TralierDedTxt").InnerText = strPercentage;
								}
							}
							else
							{
								XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
								XmlNode clonedNode = sampleNode.Clone();
								//jet Ski Coverage : 14 feb 2006
								//							if (Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "JS")
								//								{
								//									clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBPPDJ";
								//								}
								//								else
								//								{
								//									clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBPPDACV";
								//								}
								//BOATSTYLECOD
								if (Node.SelectSingleNode("BOATSTYLECODE").InnerText.ToString().ToUpper().Trim() == "JS")
								{
									clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBPPDJ";
								}
								else
								{
									clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBPPDACV";
								}
								//end 
								//clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBPPDAV";
								clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = strAmount;
								if (arrDed.Length ==2 )
								{
									clonedNode.SelectSingleNode("Deductible/Text").InnerText = strPercentage;
								}
								clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = strInsuredValue;							
								BoatDom.FirstChild.AppendChild(clonedNode);
							}
						}
					}
					
					//Deductible add by kranti
					XmlNode dedNode = Node.SelectSingleNode("DEDUCTIBLE");
					if ( dedNode  != null )
					{
						if ( dedNode.InnerText.Trim() != "" )
						{
							if ((Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() != "TRAI") && (Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() != "JT")  && (Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() !="WRT"))
							{
								if(dedNode.InnerText.IndexOf("%-")>0)
								{									
									string[] strDeductible = dedNode.InnerText.ToString().Trim().Split('-');								
									XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
									XmlNode clonedNode = sampleNode.Clone();
							
									clonedNode.SelectSingleNode("CoverageCd").InnerText = "BDEDUC";

									//clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim();
									clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = strDeductible[1].ToString().Trim();
									clonedNode.SelectSingleNode("Deductible/Text").InnerText = "-" + strDeductible[0].ToString().Trim();

									BoatDom.FirstChild.AppendChild(clonedNode);	

								}
								else
								{
									string strDeductible =dedNode.InnerText.Trim();
									XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
									XmlNode clonedNode = sampleNode.Clone();

									clonedNode.SelectSingleNode("CoverageCd").InnerText = "BDEDUC";
									clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim();
									clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = strDeductible;

									BoatDom.FirstChild.AppendChild(clonedNode);	

								}
							}
						}
					}
						


					//Personal Liability Limits
					covNode = QuoteDom.SelectSingleNode("//PERSONALLIABILITY");
					if ( covNode  != null )
					{
						//Case 1 : When No Coverage is Not selected
						if ( covNode.InnerText.Trim().ToUpper() != "NO COVERAGE")
						{
							XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
							XmlNode clonedNode = sampleNode.Clone();
							clonedNode.SelectSingleNode("CoverageCd").InnerText = "LCCSL";
							//Extended From HO check  : 02 May 2008
							if(Node.SelectSingleNode("EXTENDED_FROM_HO")!=null)
							{
								if(Node.SelectSingleNode("EXTENDED_FROM_HO").InnerText.ToString().ToUpper().Trim() == "Y")
									clonedNode.SelectSingleNode("Limit/Text").InnerText = "Extended from HO";
								else
									clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim();
							}
							else
								clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim();


							BoatDom.FirstChild.AppendChild(clonedNode);

						}
						//Case 2 :Extended from Home and NO COVERAGE selected on PERSON LIABILITY : 02 May 2008
						if(Node.SelectSingleNode("EXTENDED_FROM_HO")!=null)
						{
							if (Node.SelectSingleNode("EXTENDED_FROM_HO").InnerText.ToString().ToUpper().Trim() == "Y"
								&& covNode.InnerText.Trim().ToUpper() == "NO COVERAGE")
							{
								XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
								XmlNode clonedNode = sampleNode.Clone();
								clonedNode.SelectSingleNode("CoverageCd").InnerText = "LCCSL";
								//Extended From HO check
								clonedNode.SelectSingleNode("Limit/Text").InnerText = "Extended from HO";
								BoatDom.FirstChild.AppendChild(clonedNode);

							}
						}
					}
					
					//Medical Payments to Others
					covNode = QuoteDom.SelectSingleNode("//MEDICALPAYMENTSOTHER");
					if ( covNode  != null )
					{
						if ( covNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
						{
							XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
							XmlNode clonedNode = sampleNode.Clone();
							clonedNode.SelectSingleNode("CoverageCd").InnerText = "MCPAY";
							//Extended From HO check
							if(Node.SelectSingleNode("EXTENDED_FROM_HO")!=null)
							{
								if(Node.SelectSingleNode("EXTENDED_FROM_HO").InnerText.ToString().ToUpper().Trim() == "Y")
									clonedNode.SelectSingleNode("Limit/Text").InnerText = "Extended from HO";
								else
								{
									//For Jet ski's, waverunners or mini-jetboat no increase in med pay is allowed. So when Rate is calculated only "1000" should be taken as Medical Payment even if more than 1000 is selected. 
									/* (W)Waverunner ,(JS)Jet Ski ,(PWT)Jet Ski With Lift bar,(MBT) Mini Jet Boat*/
									if(Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "W" || Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "MJB" || Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "JS" || Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "PWT")
										clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = "1000";
									else
										clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim();
								}
							}
							else
							{
								//For Jet ski's, waverunners or mini-jetboat no increase in med pay is allowed. So when Rate is calculated only "1000" should be taken as Medical Payment even if more than 1000 is selected. 
								/* (W)Waverunner ,(JS)Jet Ski ,(PWT)Jet Ski With Lift bar,(MBT) Mini Jet Boat*/
								if(Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "W" || Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "MJB" || Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "JS" || Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().ToUpper().Trim() == "PWT")
									clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = "1000";
								else
									clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim();

							}

							BoatDom.FirstChild.AppendChild(clonedNode);

						}
					}

					//Uninsured boaters : 23 may 2006
					covNode = QuoteDom.SelectSingleNode("//UNINSUREDBOATERS");
					if ( covNode  != null )
					{
						//if ( covNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
						//{
							XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
							XmlNode clonedNode = sampleNode.Clone();
							clonedNode.SelectSingleNode("CoverageCd").InnerText = "UMBCS";
							clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim();
							BoatDom.FirstChild.AppendChild(clonedNode);
						//}
					}
					// END Uninsured boaters : 23 may 2006



					//Increase in "Unattached Equipment" And Personal Effects Coverage 
					covNode = QuoteDom.SelectSingleNode("//UNATTACHEDEQUIPMENT");
					if ( covNode  != null )
					{
						if ( covNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
						{
							XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
							XmlNode clonedNode = sampleNode.Clone();
							clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBIUE";
							clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim().Replace("$","");

							System.Xml.XmlNode deduc = QuoteDom.SelectSingleNode("//UNATTACHEDEQUIPMENT_DEDUCTIBLE");

							if (deduc != null)
							{
								//Copying deductible
								clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = deduc.InnerText.Trim();;
							}

							BoatDom.FirstChild.AppendChild(clonedNode);

						}
					}

					//Client Entertainment Liability (OP 720) 
					covNode = Node.SelectSingleNode("OP720");
					if ( covNode  != null )
					{
						if ( covNode.InnerText.Trim().ToUpper() == "Y" )
						{
							XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
							XmlNode clonedNode = sampleNode.Clone();
							clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBSMECE";
							//clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim().Replace("$","");

							BoatDom.FirstChild.AppendChild(clonedNode);

						}
					}

					//Watercraft Liability Pollution Coverage (OP 900) 
					XmlNode covNode_limit = Node.SelectSingleNode("OP900_LIMIT");
					covNode = Node.SelectSingleNode("OP900");
					if ( covNode  != null )
					{
						if ( covNode.InnerText.Trim().ToUpper() == "Y" )
						{
							XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
							XmlNode clonedNode = sampleNode.Clone();
							clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBSMWL";

							if (covNode_limit != null)
							{
								clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = covNode_limit.InnerText.Trim();
							}

							BoatDom.FirstChild.AppendChild(clonedNode);

						}
					}

					//Agreed Value (AV-100)
					covNode = Node.SelectSingleNode("AV100");
					if ( covNode  != null )
					{
						if ( covNode.InnerText.Trim().ToUpper() == "Y" )
						{
							XmlNode sampleNode = BoatDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='Sample']");
							XmlNode clonedNode = sampleNode.Clone();
							clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBSCEAV";
							//clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = covNode.InnerText.Trim().Replace("$","");

							BoatDom.FirstChild.AppendChild(clonedNode);

						}
					}

					//*******End of Coverages******************************************************************************
					AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml = AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml + BoatDom.OuterXml.ToString().Trim();
				}
				//Boat End

				//Driver IMPLEMENTATION	
				XmlNode DriverNode = AcordDom.SelectSingleNode("//WatercraftLineBusiness/PersDriver");
				string DriverBlankXml = DriverNode.OuterXml;
				AcordDom.SelectSingleNode("//WatercraftLineBusiness").RemoveChild(DriverNode);
				
				foreach (XmlNode Node in QuoteDom.SelectNodes("//OPERATORS/*"))
				{
					XmlDocument DriverDom = new XmlDocument();
					DriverDom.LoadXml(DriverBlankXml);
					DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/Surname").InnerText = Node.SelectSingleNode("OPERATORLNAME").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/GivenName").InnerText = Node.SelectSingleNode("OPERATORFNAME").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/OtherGivenName").InnerText = Node.SelectSingleNode("OPERATORMNAME").InnerText.ToString().Trim();

					DriverDom.FirstChild.Attributes["id"].Value ="D" + Node.Attributes["ID"].Value.ToString().Trim();

					if (Node.SelectSingleNode("GENDER").InnerText.ToString().Trim().ToUpper()=="MALE")
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/GenderCd").InnerText = "M";
					else
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/GenderCd").InnerText = "F";

					DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/BirthDt").InnerText = Node.SelectSingleNode("BIRTHDATE").InnerText.ToString().Trim();

					/*if (Node.SelectSingleNode("MARITALSTATUS").InnerText.ToString().Trim().ToUpper()=="MARRIED")
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "M";
					else
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "S";*/
					DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText  = Node.SelectSingleNode("MARITALSTATUS").InnerText.ToString().Trim().ToUpper();

					
					DriverDom.SelectSingleNode("PersDriver/PersDriverInfo").Attributes["VehPrincipallyDrivenRef"].Value = "W" + Node.SelectSingleNode("BOATASSIGNEDASOPERATOR").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/PersDriverInfo/DriverTypeCd").InnerText = Node.SelectSingleNode("BOATDRIVEDAS").InnerText.ToString().Trim();
					
					
					DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/PowerSquadronCourse").InnerText = Node.SelectSingleNode("POWERSQUADRONCOURSE").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/CoastGuardAuxService").InnerText = Node.SelectSingleNode("COASTGUARDAUXILARYCOURSE").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/FiveYearsOperatorExp").InnerText = Node.SelectSingleNode("BOATINGEXPERIENCESINCE").InnerText.ToString().Trim();
					
					//Years licensed
					DriverDom.SelectSingleNode("PersDriver/DriverInfo/DriversLicense/YearsLicensed").InnerText = Node.SelectSingleNode("YEARSLICENSED").InnerText.ToString().Trim();

					
					AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml = AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml + DriverDom.OuterXml.ToString().Trim();
				}
				//Driver End

				//violations
				XmlNode ViolationNode = AcordDom.SelectSingleNode("//PersPolicy/AccidentViolation");
				string ViolationBlankXml = ViolationNode.OuterXml;
				AcordDom.SelectSingleNode("//PersPolicy").RemoveChild(ViolationNode);

				foreach (XmlNode Node in QuoteDom.SelectNodes("//VIOLATIONS/*"))
				{
					XmlDocument ViolationDom = new XmlDocument();
					ViolationDom.LoadXml(ViolationBlankXml);
					
					ViolationDom.FirstChild.Attributes["VehRef"].Value="W" + Node.ParentNode.ParentNode.SelectSingleNode("BOATASSIGNEDASOPERATOR").InnerText.ToString().Trim();
					ViolationDom.FirstChild.Attributes["DriverRef"].Value="D" + Node.ParentNode.ParentNode.Attributes["ID"].Value.ToString().Trim();
					
					ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDt").InnerText= Node.SelectSingleNode("VIODATE").InnerText.ToString().Trim();
					ViolationDom.FirstChild.SelectSingleNode("DamageTotalAmt/Amt").InnerText= Node.SelectSingleNode("AMOUNTPAID").InnerText.ToString().Trim();
					
					//string[] ViolationType = Node.SelectSingleNode("VIOLATIONTYPE").InnerText.ToString().Trim().Split(':');
					//ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= ViolationType[1].ToString().Trim();
					//ViolationDom.FirstChild.SelectSingleNode("AccidentViolationCd").InnerText= ViolationType[0].ToString().Trim();

					//Passing the Violation CODE : 
					string ViolationType = Node.SelectSingleNode("VIOLATION_CODE").InnerText.ToString();
					ViolationDom.FirstChild.SelectSingleNode("AccidentViolationCd").InnerText = ViolationType.ToString().Trim();

					//Modified on 3 Dec 2007
					//Purpose : Use Violation ID when Violation Code is not present for Some violation which doesnt have ant Violation Types:
					if(ViolationType == "")
                        ViolationDom.FirstChild.SelectSingleNode("AccidentViolationId").InnerText = Node.SelectSingleNode("VIOLATIONID").InnerText.ToString();
					//END 


					//Points Assigned
					//Get Assigned Points Modified on 30 Nov 2007
					string pointsAssigned = "0";
					if(Node.SelectSingleNode("MVRPOINTS")!=null)
					 pointsAssigned  = Node.SelectSingleNode("MVRPOINTS").InnerText.ToString();
					if(ViolationDom.FirstChild.SelectSingleNode("PointsAssigned")!=null)
					ViolationDom.FirstChild.SelectSingleNode("PointsAssigned").InnerText = pointsAssigned.Trim();


					string tempString="";
					if(Node.SelectSingleNode("DEATH")!= null)
					{
						tempString= Node.SelectSingleNode("DEATH").InnerText;
						if (tempString.Trim().ToUpper() == "Y")
							ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= "DEATH";
						else
							ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= "";
					}
					AcordDom.SelectSingleNode("//PersPolicy").InnerXml = AcordDom.SelectSingleNode("//PersPolicy").InnerXml + ViolationDom.OuterXml.ToString().Trim();
				}


				//Equipment Implementation
				XmlNode equipmentNode = AcordDom.SelectSingleNode("//WatercraftLineBusiness/WatercraftAccessory");
				
				XmlNode sampleEquipmentNode = AcordDom.SelectSingleNode("//WatercraftLineBusiness/WatercraftAccessory");

				//string equipmentBlankXml = equipmentNode.OuterXml;
				//AcordDom.SelectSingleNode("//WatercraftLineBusiness").RemoveChild(equipmentNode);
				
				foreach (XmlNode Node in QuoteDom.SelectNodes("//POLICY/SCHEDULEDMISCSPORTS/*"))
				{
					//XmlDocument equipmentDom = new XmlDocument();
					//equipmentDom.LoadXml(equipmentBlankXml);
					
					// cloning to make the same number of Equipment nodes in the acord xml
					XmlNode clonedNode   = sampleEquipmentNode.Clone();

					// Electronic (Y/N)
					clonedNode.SelectSingleNode("EquipmentTypeCd").InnerText = Node.SelectSingleNode("SCH_MISC_ELECTRONIC").InnerText.ToString().Trim().ToUpper();
					
					
					// Deductible and Insuring Value
					XmlNode  covNode = clonedNode.SelectSingleNode("Coverage");
					XmlNode dedNode = Node.SelectSingleNode("SCH_MISC_DEDUCTIBLE");
					XmlNode insuringNode = Node.SelectSingleNode("SCH_MISC_AMOUNT");
					if ( covNode != null )
					{						
							string strDeductible = dedNode.InnerText.Trim();						
							covNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = strDeductible;

							string strInsuringAmount = insuringNode.InnerText.Trim();
							covNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = strInsuringAmount;
					}
 					
					// Serial Number
					clonedNode.SelectSingleNode("SerialNumber").InnerText = Node.SelectSingleNode("SCH_MISC_SERIAL_NO").InnerText.ToString().Trim().Replace("H673GSUYD7G3J73UDH","'");
                   
					// Item Description
					clonedNode.SelectSingleNode("Make").InnerText = Node.SelectSingleNode("SCH_MISC_ITEM").InnerText.ToString().Trim();

					//Item Description In Case Of Equip Type -> Other
					clonedNode.SelectSingleNode("OtherDescription").InnerText = Node.SelectSingleNode("SCH_MISC_ITEM_DESC").InnerText.ToString().Trim().Replace("H673GSUYD7G3J73UDH","'");

					//equipmentNode.AppendChild(clonedNode);	
					AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml = AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml + clonedNode.OuterXml.ToString().Trim();
				}
				//End of Equipment Implementation

				AcordXml=AcordDom.OuterXml;
			}
			return(AcordXml);
		}
		#region GetAppNumber
		//For fetching App Numbers from Home which are INCOMPLETE --24 jan 2006
		public string GetHomeAppNumber(string strCustomerId,string strStateName)
		{
			DataSet ldsHomeModel = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteHomeApplications " + strCustomerId + ",'" + strStateName + "'");
			string HomeAppXml="";
			foreach(DataRow Row in ldsHomeModel.Tables[0].Rows)
			{
				HomeAppXml = HomeAppXml + Row[0].ToString();
            }
			HomeAppXml = HomeAppXml.Replace("<APP_LIST/>","");
			HomeAppXml = HomeAppXml.Replace("<APP_LIST ","<APP ");
			//Passed Home App Number ,App ID ,App version 
			HomeAppXml = "<APPS><APP APP_NO=\"\" CUSTOMER_ID=\"\" APP_ID=\"\" APP_VER_ID=\"\" APP_NUMBER=\"\"/>" + HomeAppXml + "</APPS>";

			return(HomeAppXml);
		}
		#endregion
		#region Get Home Coverage Limits PL and MEDPM
		public DataSet GetHomeLimits(int customerID,int appID,int appVerID)
		{
			DataSet dsTemp = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appID,SqlDbType.Int );
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVerID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetHomeCoverageLimit");
				return dsTemp;

			}
			catch(Exception ex)
			{	
				throw(ex);
			}
			finally
			{
				dsTemp = null;
				objDataWrapper = null;
			}
		}
		#endregion
		#region Get Primary Applicant Info
		public DataSet GetPrimaryApplicantInfoForRates(string strCustomerId, string strAppID,string strAppVerID,string strUserId,string strCalledFrom)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",strCustomerId,SqlDbType.VarChar );
				objDataWrapper.AddParameter("@APPID",strAppID,SqlDbType.VarChar );
				objDataWrapper.AddParameter("@APPVERSIONID",strAppVerID,SqlDbType.VarChar);
				objDataWrapper.AddParameter("@USERID",strUserId,SqlDbType.VarChar);
				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom,SqlDbType.VarChar);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPrimaryApplicantInfo");
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}
		#endregion
		//To calculate the TERRITORY CODE (Not In Use)
//		public string GetTerritoryWatercraft(string QQ_STATE,string strZipCode)
//		{
//			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetWATERCRAFT_TERRCODE '" + QQ_STATE + "','" + strZipCode + "'");
//			if (ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
//				return(ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());
//			else
//				return("");
//		}
		//End

		

	}
}
