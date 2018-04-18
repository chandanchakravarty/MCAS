/******************************************************************************************
<Modified Date			: sep. 24,2007
<Modified By			: Pravesh K Chandel
<Purpose				: Modify and add code for Coverage details and other
*************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using Cms.BusinessLayer.BlApplication;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using Cms.BusinessLayer.BlQuote;
//using Microsoft.Xml.XQuery;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls; 


namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for PolicyReinsuranceInquiry.
	/// </summary>
	public class PolicyReinsuranceInquiry : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm ReinsuranceInquiry;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabNumber;
		protected System.Web.UI.WebControls.Label capTRANSACTION_DATE;
		protected System.Web.UI.WebControls.Label txtTRANSACTION_DATE;
		protected System.Web.UI.WebControls.Label capTRANSACTION_NUMBER;
		protected System.Web.UI.WebControls.Label txtTRANSACTION_NUMBER;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capTRANSACTION_DESC;
		protected System.Web.UI.WebControls.Label txtTRANSACTION_DESC;

		//Added by Sibin to use print button
		protected Cms.CmsWeb.Controls.CmsButton btnPrintReport;
		
		
		string strCUSTOMER_ID ;
		string strPolicyID ;
		string strPolVerId ;
		//XPathNavigator nav;
		private const string REIN_CALC_PREMIUM="13047";
		private const string REIN_CALC_SPLIT="14242";
		private const string REIN_CALC_BOTH="13048";
		private const string REIN_CALC_NOTAPPLICABLE="13046";
		private const string REIN_CALC_TIV="14243";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting screen id
			base.ScreenId	=	"224_12"; //Changed Screen id to 224_12 for 500_1 by Sibin on 21 Oct 08 to add it into Policy Details Permission List
			#endregion

			//Added by Sibin on 03-10-08
			btnPrintReport.CmsButtonClass		=	CmsButtonType.Read;
			btnPrintReport.PermissionString		=	gstrSecurityXML;

			btnPrintReport.Attributes.Add("OnClick","return PrintReport();");

			 strCUSTOMER_ID = GetCustomerID();
			 strPolicyID = GetPolicyID();
			 strPolVerId = GetPolicyVersionID();
			 capMessage.Visible=false;
			 FillInfo();

		}
		private void FillInfo()
		{
			try
			{
				Cms.BusinessLayer.BlApplication.clsapplication objapplication = new clsapplication();

				DataSet dsReins = objapplication.GetReinsurance_Inquiry_Details(int.Parse(strCUSTOMER_ID), int.Parse(strPolicyID), int.Parse(strPolVerId));
				StringBuilder strBuilder = new StringBuilder();
				strBuilder.Append("");
				int policyVersionID=0;
				string lobID="0",strProcessId="0";
				if(dsReins != null && dsReins.Tables[0].Rows.Count > 0)
				{
				
					strBuilder.Append("<table width='100%>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td class='DataGridRow' align='center' colspan='6'><b>REINSURANCE INQUIRY</b></td>");
					strBuilder.Append("</tr>");

					//				strBuilder.Append("<tr height='20'>");
					//				strBuilder.Append("<td  class='DataGridRow' align='Left' colspan='6'></td>");
					//				strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Policy Number</b></td>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["POLICY_NUMBER"].ToString() +"</td>");

					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Insured</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString() + "</td>");

					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Line of Business</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["LOB_DESC"].ToString() + "</td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>State</b></td>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["STATE_DESC"].ToString() +"</td>");

					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Policy Effective Date</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString() + "</td>");

					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Policy Expiration Date</b></td>");
					strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString() + "</td>");
					strBuilder.Append("</tr>");
			
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Special Acceptances</b></td>");
					strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + dsReins.Tables[0].Rows[0]["REINS_SPECIAL_ACPT"].ToString() +"</td>");
					strBuilder.Append("<td  width='70%' class='DataGridRow' colspan='4'></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("</table>");


					lobID=dsReins.Tables[0].Rows[0]["LOB_ID"].ToString();
					//capMessage.Text = strBuilder.ToString();

				}
			
				if(dsReins != null && dsReins.Tables[1].Rows.Count > 0)
				{
					for ( int rowCounter=0;rowCounter<dsReins.Tables[1].Rows.Count;rowCounter++)
					{
						//policyVersionID=int.Parse(dsReins.Tables[1].Rows[rowCounter]["POLICY_VERSION_ID"].ToString());
						policyVersionID=int.Parse(dsReins.Tables[1].Rows[rowCounter]["NEW_POLICY_VERSION_ID"].ToString());
						string strSplitCoverageIds="";
						strProcessId=dsReins.Tables[1].Rows[rowCounter]["PROCESS_ID"].ToString();
						strBuilder.Append("<table width='100% class='tableWidth'>");
						strBuilder.Append("<tr>");
						strBuilder.Append("<TD class='headerEffectSystemParams' colSpan='4'>Transaction Details</TD>");
						strBuilder.Append("</tr>");
						strBuilder.Append("<tr>");
						strBuilder.Append("<TD class='midcolora' width='50%' colSpan='2'>Transaction Date</TD>");
						strBuilder.Append("<TD class='midcolora' width='50%' colSpan='2'>" + dsReins.Tables[1].Rows[rowCounter]["TRAN_DATE"].ToString() + "</TD>");
						strBuilder.Append("</tr>");
						int strSeqNo = rowCounter + 1;
						strBuilder.Append("<tr>");
						strBuilder.Append("<TD class='midcolora' width='50%' colSpan='2'>Transaction Sequence #</TD>");
						strBuilder.Append("<TD class='midcolora' width='50%' colSpan='2'>" + strSeqNo .ToString()  + "</TD>");
						//dsReins.Tables[1].Rows[rowCounter]["SOURCE_ROW_ID"].ToString() 
						strBuilder.Append("</tr>");

						strBuilder.Append("<tr>");
						strBuilder.Append("<TD class='midcolora' width='50%' colSpan='2'>Effective Date</TD>");
						strBuilder.Append("<TD class='midcolora' width='50%' colSpan='2'>" + dsReins.Tables[1].Rows[rowCounter]["TRAN_EFF_DATE"].ToString() + "</TD>");
						strBuilder.Append("</tr>");

						strBuilder.Append("<tr>");
						strBuilder.Append("<TD class='midcolora' width='50%' colSpan='2'>Transaction Description</TD>");
						strBuilder.Append("<TD class='midcolora' width='50%' colSpan='2'>" + dsReins.Tables[1].Rows[rowCounter]["TRAN_DESC"].ToString() + "</TD>");
						strBuilder.Append("</tr>");

						strBuilder.Append("<tr>");
						strBuilder.Append("<TD class='headerEffectSystemParams' colSpan='4'>Coverage Details</TD>");
						strBuilder.Append("</tr>");
						double dblTotal=0;
						double dblLimitTotal=0;
						//string strcoverages = getCoveragePremium(int.Parse(strCUSTOMER_ID), int.Parse(strPolicyID), policyVersionID,4);
						if(dsReins.Tables[3].Rows.Count>0 && dsReins.Tables[2].Rows.Count>0)
						{
							strBuilder.Append("<tr>");
							//							strBuilder.Append("<TD class='midcolora' width='40%'><b>Policy Assessment</b></TD>");
							strBuilder.Append("<TD class='midcolora' width='40%'><b></b></TD>");
							strBuilder.Append("<TD class='midcolora' width='20%'><b>Total Insurance Value Limit</b></TD>");
							strBuilder.Append("<TD class='midcolora' width='20%'><b>Premiums</TD></b>");
							strBuilder.Append("<TD class='midcolora' width='20%'><b>Sub Total By Coverage Code</b></TD>");
							strBuilder.Append("</tr>");
							strBuilder.Append("<tr>");
							//strBuilder.Append("<TD class='midcolora' colspan = '4'>Policy Assessment</TD>");
							strBuilder.Append("<TD class='midcolora' colspan = '2' >Policy Assessment/ Fees Deducted</TD>");
							if (dsReins.Tables[4].Rows.Count>0)
							{
								DataRow[] drFee= dsReins.Tables[4].Select("POLICY_VERSION_ID=" + policyVersionID.ToString()); 
								if (drFee.Length==0)
									strBuilder.Append("<TD class='midcolora' width='20%'></TD>");
								else									
									strBuilder.Append("<TD class='midcolorr' width='20%'><b>$" + drFee[0]["FEES"].ToString().Trim()+ "</b></TD>");
							}
							else
								strBuilder.Append("<TD class='midcolora' width='20%'></TD>");
							strBuilder.Append("<TD class='midcolora' width='20%'></TD>");
							
							strBuilder.Append("</tr>");
							
							for(int cateCount=0;cateCount<dsReins.Tables[3].Rows.Count;cateCount++)
							{
								
								strBuilder.Append("<tr>");
								strBuilder.Append("<TD class='midcolora' colspan='4'><b>" + dsReins.Tables[3].Rows[cateCount]["COV_CATEGORY"].ToString() +"</b></TD>");
								strBuilder.Append("</tr>");
								string CategoryId=dsReins.Tables[3].Rows[cateCount]["CATEGORY_CODE"].ToString();

								if(dsReins.Tables[2].Rows.Count==0)
								{
									strBuilder.Append("<tr>");
									strBuilder.Append("<TD class='midcolora' colspan='4'></TD>");
									strBuilder.Append("</tr>");
								}
								else
								{
									//DataRow[] drLimit= dsReins.Tables[2].Select("POLICY_VERSION_ID=" + policyVersionID.ToString() + " AND REINSURANCE_COV=" + CategoryId ); // + strPolVerId);
									DataRow[] drLimit= dsReins.Tables[2].Select("(POLICY_VERSION_ID=" + policyVersionID.ToString() + " AND REINSURANCE_COV=" + CategoryId + ") OR (COMPONENT_CODE='REINSURANCE' AND REINSURANCE_COV=" + CategoryId + ")"); // + strPolVerId);
									double dblSubTotal=0;
									double dblSubLimitTotal=0;
									//double dblTotal=0;
									
									if (drLimit.Length==0)
									{
										strBuilder.Append("<tr>");
										strBuilder.Append("<TD class='midcolora' colspan='4'></TD>");
										strBuilder.Append("</tr>");
									}
									else
									{
										string tempCovType="";
										dblSubTotal=0;dblSubLimitTotal=0;
										for(int ctr=0;ctr<drLimit.Length;ctr++)
										{
											if (tempCovType!=drLimit[ctr]["REINSURANCE_COV"].ToString().Trim())
											{
									
												if (tempCovType!="")
												{
													strBuilder.Append("<tr>");
													strBuilder.Append("<TD class='midcolora' width='40%'><b>Sub Total</b></TD>");
													if(dblSubLimitTotal>0)
														strBuilder.Append("<TD class='midcolorr' width='20%'><b>$" + dblSubLimitTotal.ToString("###,###") + "</b></TD>");
													else
														strBuilder.Append("<TD class='midcolorr' width='20%'><b>" + "" + "</b></TD>");
													strBuilder.Append("<TD class='midcolora' width='20%'></TD>");
													if(dblSubTotal>0 || dblSubTotal<0 )
														strBuilder.Append("<TD class='midcolorr' width='20%'><b>$" + dblSubTotal.ToString("###,###.00") + "</b></TD>");
													else
														strBuilder.Append("<TD class='midcolorr' width='20%'><b>" + "" + "</b></TD>");
													strBuilder.Append("</tr>");
												}
												tempCovType=drLimit[ctr]["REINSURANCE_COV"].ToString().Trim();
												dblSubTotal=0;dblSubLimitTotal=0;
											}
											strBuilder.Append("<tr>");
											strBuilder.Append("<TD class='midcolora' width='40%'>" + drLimit[ctr]["COV_DES"].ToString() + "</TD>");
											//if reinsurance Calculation is both then show bith limit and premium if premium the show only premium and if not applicable then dont show any limit and premium
											if (drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_NOTAPPLICABLE || drLimit[ctr]["REINSURANCE_CALC"].ToString()=="0")
											{
												strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");
												strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");
												strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");
											}
											else
											{
												if (drLimit[ctr]["POLICY_LIMITS"].ToString()!="" && drLimit[ctr]["POLICY_LIMITS"].ToString()!="0" && (drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_BOTH || drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_TIV || drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_SPLIT ))
												{
													
													strBuilder.Append("<TD class='midcolorr' width='20%'>$" + double.Parse(drLimit[ctr]["POLICY_LIMITS"].ToString()).ToString("###,###") + "</TD>");
												}
												else
													strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");
												
												if (drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_SPLIT && drLimit[ctr]["COMPONENT_CODE"].ToString()=="REINSURANCE")
												{
													DataRow[] drSplitpremium= dsReins.Tables[5].Select("POLICY_VERSION_ID=" + policyVersionID.ToString() + " AND (COV_ID_1ST_SPLIT=" + drLimit[ctr]["COV_ID"].ToString() + " OR COV_ID_2ND_SPLIT= " + drLimit[ctr]["COV_ID"].ToString() + ")" ); 
													strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");
													if (drSplitpremium.Length==0)
														strBuilder.Append("<TD class='midcolorr' width='20%'></TD>");
													else if(drSplitpremium[0]["COV_ID_1ST_SPLIT"].ToString()==drLimit[ctr]["COV_ID"].ToString())
													{
														strBuilder.Append("<TD class='midcolorr' width='20%'>$" + double.Parse(drSplitpremium[0]["COVERAGE_PREMIUM_1ST_SPLIT"].ToString()).ToString("###,###.00") + "</TD>");
														dblSubTotal=double.Parse(drSplitpremium[0]["COVERAGE_PREMIUM_1ST_SPLIT"].ToString());
													}
													else
													{
														strBuilder.Append("<TD class='midcolorr' width='20%'>$" + double.Parse(drSplitpremium[0]["COVERAGE_PREMIUM_2ND_SPLIT"].ToString()).ToString("###,###.00") + "</TD>");
														dblSubTotal=double.Parse(drSplitpremium[0]["COVERAGE_PREMIUM_2ND_SPLIT"].ToString());
													}
													if (drSplitpremium.Length!=0)
														strSplitCoverageIds=drSplitpremium[0]["COV_ID"].ToString();
												
												}
												else if (drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_TIV ) //|| drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_SPLIT)
												{
													strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");
													strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");

												}
												else
												{
													if (drLimit[ctr]["COVERAGE_PREMIUM"].ToString().ToUpper()!="INCLUDED" && drLimit[ctr]["COVERAGE_PREMIUM"].ToString().ToUpper()!="EXTENDED" && drLimit[ctr]["COVERAGE_PREMIUM"].ToString().ToUpper()!="0")
														strBuilder.Append("<TD class='midcolorr' width='20%'>$" + double.Parse(drLimit[ctr]["COVERAGE_PREMIUM"].ToString()).ToString("###,###.00") + "</TD>");
													else
														strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");
													
													if (drLimit[ctr]["COVERAGE_PREMIUM"].ToString().ToUpper()!="INCLUDED" && drLimit[ctr]["COVERAGE_PREMIUM"].ToString().ToUpper()!="EXTENDED" && drLimit[ctr]["COVERAGE_PREMIUM"].ToString().ToUpper()!="0"
														&& strSplitCoverageIds.IndexOf(drLimit[ctr]["COV_ID"].ToString())==-1 )
													{
														//&& drLimit[ctr]["REINSURANCE_CALC"].ToString()!=REIN_CALC_SPLIT)
														strBuilder.Append("<TD class='midcolorr' width='20%'>$" + double.Parse(drLimit[ctr]["COVERAGE_PREMIUM"].ToString()).ToString("###,###.00") + "</TD>");
													}
													else
														strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");
												}
												strBuilder.Append("</tr>");
												try
												{
													if (drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_SPLIT && drLimit[ctr]["COMPONENT_CODE"].ToString()=="REINSURANCE")
													{
														dblSubTotal = dblSubTotal+ Double.Parse(drLimit[ctr]["COVERAGE_PREMIUM"].ToString());
													}
													else if (drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_BOTH)
													{
														if ( strSplitCoverageIds.IndexOf(drLimit[ctr]["COV_ID"].ToString())==-1)
															dblSubTotal = dblSubTotal+ Double.Parse(drLimit[ctr]["COVERAGE_PREMIUM"].ToString());
														dblSubLimitTotal=dblSubLimitTotal+Double.Parse(drLimit[ctr]["POLICY_LIMITS"].ToString());
														//dblTotal=dblTotal+Double.Parse(drLimit[ctr]["COVERAGE_PREMIUM"].ToString());
														dblLimitTotal=dblLimitTotal + Double.Parse(drLimit[ctr]["POLICY_LIMITS"].ToString());
													}
													else if(drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_TIV)
													{
														dblSubLimitTotal=dblSubLimitTotal+Double.Parse(drLimit[ctr]["POLICY_LIMITS"].ToString());
														dblLimitTotal=dblLimitTotal + Double.Parse(drLimit[ctr]["POLICY_LIMITS"].ToString());
													}
													else if(drLimit[ctr]["REINSURANCE_CALC"].ToString()==REIN_CALC_SPLIT )
													{
														//dblSubTotal = dblSubTotal+ Double.Parse(drLimit[ctr]["COVERAGE_PREMIUM"].ToString());
														//dblTotal=dblTotal+Double.Parse(drLimit[ctr]["COVERAGE_PREMIUM"].ToString());
													}
													else
													{
														if (strSplitCoverageIds.IndexOf(drLimit[ctr]["COV_ID"].ToString())==-1)
															dblSubTotal = dblSubTotal+ Double.Parse(drLimit[ctr]["COVERAGE_PREMIUM"].ToString());
														//dblTotal=dblTotal+Double.Parse(drLimit[ctr]["COVERAGE_PREMIUM"].ToString());
													}

												}
												catch(Exception ex)
												{
													string strExp=ex.Message;
												}
											}

										}
									}
									//writing sub total
									strBuilder.Append("<tr>");
									strBuilder.Append("<TD class='midcolora' width='40%'><b>Sub Total</b></TD>");
									if(dblSubLimitTotal>0 )
										strBuilder.Append("<TD class='midcolorr' width='20%'><b>$" + dblSubLimitTotal.ToString("###,###") + "</b></TD>");
									else
										strBuilder.Append("<TD class='midcolorr' width='20%'>" + "" + "</TD>");
									strBuilder.Append("<TD class='midcolorr' width='20%'></TD>");
									if(dblSubTotal>0 || dblSubTotal<0)
										strBuilder.Append("<TD class='midcolorr' width='20%'><b>$" + dblSubTotal.ToString("###,###.00") + "</b></TD>");
									else
										strBuilder.Append("<TD class='midcolorr' width='20%'><b>" + "" + "</b></TD>");
									strBuilder.Append("</tr>");
									dblTotal=dblTotal+dblSubTotal;
								}//end of if table[2] count
							}//end for loop for cateCount
							//writing Grand Total
							strBuilder.Append("<tr>");
							strBuilder.Append("<TD class='midcolora' width='40%'><b>Totals</b></TD>");
							strBuilder.Append("<TD class='midcolorr' width='20%'><b>$" + dblLimitTotal.ToString("###,###") + "</b></TD>");
							strBuilder.Append("<TD class='midcolorr' width='20%'></TD>");
							strBuilder.Append("<TD class='midcolorr' width='20%'><b>$" + dblTotal.ToString("###,###.00") + "</b></TD>");
							strBuilder.Append("</tr>");
						}//end of if table[3] count
						//strBuilder.Append(strcoverages);
//						strBuilder.Append("<tr>");
//						strBuilder.Append("<TD class='midcolorr' width='20%'><b>$<input type=submit id=btnPrint'""</b></TD>");
//						strBuilder.Append("</tr>");
						
						strBuilder.Append("<tr>");
						strBuilder.Append("<TD class='headerEffectSystemParams' colSpan='2'>Reinsurance Details&nbsp<img src='../../cmsweb/images/selecticon.gif' style='CURSOR:hand' alt ='Reinsurance Breakdown' onclick	='openBreakDownDetails(" + strCUSTOMER_ID + "," + strPolicyID + "," +  policyVersionID.ToString() + "," + strProcessId + "," + strSeqNo .ToString() + ");'></img></TD>");
						//strBuilder.Append("<TD class='headerEffectSystemParams' colSpan='2'><a href='../../cmsweb/Maintenance/Reinsurance/ReinsuranceBreakdown.aspx?',target='_blank'>Click Here</a></TD>");
						strBuilder.Append("</tr>");
						strBuilder.Append("</table>");
					}
				}

		
				lblTemplate.Text=strBuilder.ToString();
				//capMessage.Text = strBuilder.ToString();
			}
			catch(Exception ex)
			{
				capMessage.Text = ex.Message;
				capMessage.Visible=true;
			}
		}
		private string getCoveragePremium(int customerID,int PolicyID,int PolicyVersionID,int LobID)
		{
			int showDetails=0,quoteId=0;
			string strPremium="";
			Cms.BusinessLayer.BlQuote.ClsGenerateQuote objGenerateQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(CarrierSystemID);
			string strCSSNo =GetColorScheme();		
			int intPolQuote_ID;
			showDetails =  objGenerateQuote.GeneratePolicyQuote(customerID,PolicyID,PolicyVersionID,LobID.ToString(),strCSSNo,out intPolQuote_ID,GetUserId());				
			quoteId=intPolQuote_ID;
			if(showDetails ==5)
			{
				//Response.Write("This policy has been modified. Please verify policy again");
				return "";
			}
			
			ClsGenerateQuote objGenerQuote=new ClsGenerateQuote(CarrierSystemID);							
			DataSet dsTemp = objGenerQuote.FetchQuote_Pol(customerID,quoteId,PolicyID,PolicyVersionID);
			//int dwelling;
			if(dsTemp!=null && dsTemp.Tables[0]!=null && dsTemp.Tables[0].Rows.Count>0)
			{
				string quoteFirstComponentXml="",quoteSecondComponentXml="";								
				quoteFirstComponentXml = dsTemp.Tables[0].Rows[0]["QUOTE_XML"].ToString();

				quoteFirstComponentXml=ShowPremium(quoteFirstComponentXml,LobID.ToString());
				//check the rowcount
				if (dsTemp.Tables[0].Rows.Count>1)
				{
					//if count>1 then it has both home+boat
					//appLobID=4;
					quoteSecondComponentXml= dsTemp.Tables[0].Rows[1]["QUOTE_XML"].ToString();

					quoteSecondComponentXml=ShowPremium(quoteSecondComponentXml,LobID.ToString());
				}
					
				strPremium = quoteFirstComponentXml + quoteSecondComponentXml;
				strPremium = strPremium.Replace("H673GSUYD7G3J73UDH","'");
				//	Response.Write(strPremium);
				
			}
			return strPremium;
		}
	private string ShowPremium( string quoteXml,string appLobID)
			{
				string	finalQuoteXSLPath = "";	
		StringBuilder strBuilder=new StringBuilder();
		
					switch(appLobID)
					{
						case LOB_HOME:
							finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteHO3");
							break;
						case LOB_PRIVATE_PASSENGER:
							finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteAuto");
							break;
						case LOB_MOTORCYCLE:
							finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteCYCL");
							break;
						case LOB_WATERCRAFT:
							finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteBOAT");
							break;
						case LOB_RENTAL_DWELLING:
							finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQuoteREDW");
							break;
						case LOB_UMBRELLA:
							finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FinalQutoteUMB");
							break;
						default:
							break;
					
					}

				string strPremium ="";
				XmlDocument xmlDocInput = new XmlDocument();
				xmlDocInput.LoadXml(quoteXml);
				XmlNode toRemoveNode;
				toRemoveNode=xmlDocInput.SelectSingleNode("PRIMIUM/OPERATOR");
				toRemoveNode.ParentNode.RemoveChild(toRemoveNode);
				toRemoveNode=xmlDocInput.SelectSingleNode("PRIMIUM/VIOLATION");
				toRemoveNode.ParentNode.RemoveChild(toRemoveNode);
				toRemoveNode=xmlDocInput.SelectSingleNode("PRIMIUM/HEADER");
				toRemoveNode.ParentNode.RemoveChild(toRemoveNode);
				toRemoveNode=xmlDocInput.SelectSingleNode("PRIMIUM/CLIENT_TOP_INFO");
				toRemoveNode.ParentNode.RemoveChild(toRemoveNode);
				toRemoveNode=xmlDocInput.SelectSingleNode("PRIMIUM/MINIM");
				toRemoveNode.ParentNode.RemoveChild(toRemoveNode);
				
		XmlNodeList xmlNodelst=xmlDocInput.SelectNodes("PRIMIUM/RISK");
		for(int i=0;i<xmlNodelst.Count;i++)
		{
			XmlNode xNode =xmlNodelst.Item(i);
			XmlNodeList xStepNodeList=xNode.SelectNodes("STEP");
			for( int j=0;j<xStepNodeList.Count;j++)
			{
				string stepRemium="",stepLimit="",stepDesc="",ComponentType="";
				 
				stepRemium=xStepNodeList.Item(j).Attributes["STEPPREMIUM"].Value;
				stepLimit=xStepNodeList.Item(j).Attributes["LIMITVALUE"].Value;
				stepDesc=xStepNodeList.Item(j).Attributes["STEPDESC"].Value;
				XmlAttribute XmlAtr	=xStepNodeList.Item(j).Attributes["COMPONENT_TYPE"];
				if (XmlAtr!=null)
					{
						ComponentType=xStepNodeList.Item(j).Attributes["COMPONENT_TYPE"].Value;
						if (stepDesc!="" && ComponentType=="P")
						{
							strBuilder.Append("<tr>");
							strBuilder.Append("<TD class='midcolora' width='40%'>" + stepDesc + "</TD>");
							strBuilder.Append("<TD class='midcolora' width='20%'>" + stepLimit + "</TD>");
							strBuilder.Append("<TD class='midcolora' width='20%'>" + stepRemium + "</TD>");
							strBuilder.Append("<TD class='midcolora' width='20%'>" + stepRemium + "</TD>");
							strBuilder.Append("</tr>");
						}
					}
				//strPremium=strPremium +stepDesc + stepLimit +stepDesc ;
			}
		}
		
				
				//Transform the Input XMl to generate the premium
				//XslTransform tr	= new XslTransform();				
				//tr.Load(finalQuoteXSLPath);
				//nav = ((IXPathNavigable) xmlDocInput).CreateNavigator();
				//StringWriter swReport = new StringWriter();
				//tr.Transform(nav,null,swReport);
				// here we will chk for Boat attached to a Home or not 		
				//string strPremium = swReport.ToString();
				//strPremium=strPremium.Substring(strPremium.IndexOf("<body"), strPremium.IndexOf("</body")-strPremium.IndexOf("<body"));
				strPremium=strBuilder.ToString();;
				return strPremium;
			
			}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
