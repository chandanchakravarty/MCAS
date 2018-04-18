using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;

namespace Cms.Application.Aspx
{
    /// <summary>
    /// Summary description for QuoteGenerator.
    /// </summary>
    public class QuoteGenerator : Cms.Application.appbase
    {
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRATE_XML;

        public int lIntQuoteID, lIntCUSTOMER_ID, lIntAPP_ID, lIntAPP_VERSION_ID, lIntShowQuote, lIntPOLICY_ID, lIntPOLICY_VERSION_ID;
        public string lStrLobID, CALLEDFROM;

        eRateEngine objeRateEngine;
        QuoteRequestType objQuoteRequestType;
        QuoteResponseType objQuoteResponseType;

        int customerID = 0, quoteId = 0, qqId = 0, showDetails = 0, appID = 0, appVersionID = 0, dwellingID = 2, PolicyID = 0, PolicyVersionID = 0, PolicyNewVersionID = 0, ProcessRowID = 0, CntGrossPrmCal = 0;
        DateTime EFFECTIVE_DATE, EXPIRY_DATE;
        string showRateDetails="", appLobID = "", strCalledFrom = "", QQInputXml = "", QQPremiumXmlOld = "", QQPremiumXmlNew = "", QQAPPNumber = "";

        private void Page_Load(object sender, System.EventArgs e)
        {
            //get the values from the querystring
            GetQueryString();
            // Put user code to initialize the page here
            //if (Request["CALLED_FOR"] != null && Request["CALLED_FOR"].ToString() != "RATE")
            //  genrateRate();
            SetCultureThread(GetLanguageCode()); //itrack # 1402.set page culture according to user language.added by lalit July 21 2011
            if (strCalledFrom == "QAPP")
            {
                GenerateQuickQuote();
            }
            else if (Request["POLICY_ID"] != null && Request["POLICY_ID"].ToString() != "")
                generatePolicyQuote();  //Commentaed By Lalit 
            //GenratePolicyRate();   //Genrate Rate For policy


            else if (Request["QQ_ID"] != null && Request["QQ_ID"].ToString() != "")
                generateQQuote();
            else
                generateAppQuote();
        }
        private void GenerateQuickQuote()
        {
            Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID=" + customerID + "&POLICY_ID=" + PolicyID + "&POLICY_VERSION_ID=" + PolicyVersionID + "&LOBID=" + GetLOBID() + "&SHOW=9");
        }
        private void GetQueryString()
        {
            if (Request.QueryString["CUSTOMER_ID"] != null)
            {
                customerID = int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
            }
            if (Request.QueryString["APP_ID"] != null)
            {
                appID = int.Parse(Request.QueryString["APP_ID"].ToString());
            }
            if (Request.QueryString["APP_VERSION_ID"] != null)
            {
                appVersionID = int.Parse(Request.QueryString["APP_VERSION_ID"].ToString());
            }
            if (Request.QueryString["QUOTE_ID"] != null)
            {
                quoteId = int.Parse(Request.QueryString["QUOTE_ID"].ToString());
            }
            if (Request.QueryString["QQ_ID"] != null)
            {
                qqId = int.Parse(Request.QueryString["QQ_ID"].ToString());
            }
            // SHOW 
            if (Request.QueryString["SHOW"] != null)
            {
                showDetails = int.Parse(Request.QueryString["SHOW"].ToString());
            }
            if (Request.QueryString["SHOWRATE"] != null)
            {
                showRateDetails = Request.QueryString["SHOWRATE"].ToString();
            }
            if (Request.QueryString["LOBID"] != null)
            {
                appLobID = Request.QueryString["LOBID"].ToString();
            }
            if (Request.QueryString["POLICY_ID"] != null)
            {
                PolicyID = int.Parse(Request.QueryString["POLICY_ID"].ToString());
            }
            if (Request.QueryString["POLICY_VERSION_ID"] != null)
            {
                PolicyVersionID = int.Parse(Request.QueryString["POLICY_VERSION_ID"].ToString());
            }
            if (Request.QueryString["NEW_POLICY_VERSION_ID"] != null)
            {
                PolicyNewVersionID = int.Parse(Request.QueryString["NEW_POLICY_VERSION_ID"].ToString());
            }
            if (Request.QueryString["EFFECTIVE_DATETIME"] != null && Request.QueryString["EFFECTIVE_DATETIME"].ToString() != "0" && Request.QueryString["EFFECTIVE_DATETIME"].ToString() != "")
            {
                EFFECTIVE_DATE = Convert.ToDateTime(Request.QueryString["EFFECTIVE_DATETIME"].ToString());
            }
            if (Request.QueryString["EXPIRY_DATE"] != null && Request.QueryString["EXPIRY_DATE"].ToString() != "0" && Request.QueryString["EXPIRY_DATE"].ToString() != "")
            {
                EXPIRY_DATE = Convert.ToDateTime(Request.QueryString["EXPIRY_DATE"].ToString());
            }
            if (Request.QueryString["CALLEDFROM"] != null)
            {
                strCalledFrom = Request.QueryString["CALLEDFROM"].ToString();
            }
        }

        private string GetQuoteRateXML()
        {
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQuoteDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            DataSet dsRate = objQuoteDetails.FetchDataForQQRqXml(customerID, PolicyID, PolicyVersionID);

            DataTable dtRate = dsRate.Tables[0];

            if (dtRate.Rows.Count != 0)
            {
                XmlDocument doc = new XmlDocument();
                //XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                //doc.AppendChild(docNode);

                XmlNode productsNode = doc.CreateElement("Product");
                doc.AppendChild(productsNode);

                XmlNode productCode = doc.CreateElement("ProductCode");
                productsNode.AppendChild(productCode);
                productCode.InnerText = "AUTO";

                XmlNode productname = doc.CreateElement("ProductName");
                productsNode.AppendChild(productname);
                productname.InnerText = "Auto Product";

                XmlNode sessionName = doc.CreateElement("SessionActionCode");
                productsNode.AppendChild(sessionName);
                productname.InnerText = "NewBusiness";

                XmlNode sessionURL = doc.CreateElement("EndSessionURL");
                productsNode.AppendChild(sessionURL);

                XmlNode messageStatus = doc.CreateElement("MessageStatus");
                productsNode.AppendChild(messageStatus);

                XmlNode messageCode = doc.CreateElement("MessageStatusCode");
                messageStatus.AppendChild(messageCode);

                XmlNode messageDesc = doc.CreateElement("MessageStatusDescription");
                messageStatus.AppendChild(messageDesc);

                XmlNode msgExCode = doc.CreateElement("ExtendedStatusCode");
                messageStatus.AppendChild(msgExCode);

                XmlNode msgStatusDesc = doc.CreateElement("ExtendedStatusDescription");
                messageStatus.AppendChild(msgStatusDesc);

                XmlNode policyHeader = doc.CreateElement("PolicyHeader");
                productsNode.AppendChild(policyHeader);

                XmlNode languageCode = doc.CreateElement("DefaultLanguageCode");
                policyHeader.AppendChild(languageCode);
                languageCode.InnerText = "en-US";

                XmlNode currencyCode = doc.CreateElement("DefaultCurrencyCode");
                policyHeader.AppendChild(currencyCode);
                currencyCode.InnerText = "USD";

                XmlNode indicator = doc.CreateElement("IsTestIndicator");
                policyHeader.AppendChild(indicator);

                XmlNode contractDetails = doc.CreateElement("ContractDetails");
                policyHeader.AppendChild(contractDetails);

                XmlNode cStateCode = doc.CreateElement("ContractStateCode");
                contractDetails.AppendChild(cStateCode);
                cStateCode.InnerText = "NewBusiness";

                XmlNode rStateCode = doc.CreateElement("RevisionStateCode");
                contractDetails.AppendChild(rStateCode);

                XmlNode bStateCode = doc.CreateElement("BindStateCode");
                contractDetails.AppendChild(bStateCode);
                bStateCode.InnerText = "New";

                XmlNode closeIndicator = doc.CreateElement("IsClosedIndicator");
                contractDetails.AppendChild(closeIndicator);

                XmlNode uki = doc.CreateElement("UKI");
                contractDetails.AppendChild(uki);

                XmlNode cPaymentInd = doc.CreateElement("CloseOnPaymentIndicator");
                contractDetails.AppendChild(cPaymentInd);

                XmlNode cActionCode = doc.CreateElement("StartContractActionCode");
                contractDetails.AppendChild(cActionCode);

                XmlNode rActionCode = doc.CreateElement("StartRevisionActionCode");
                contractDetails.AppendChild(rActionCode);

                XmlNode renewInd = doc.CreateElement("LapseOnRenewalIndicator");
                contractDetails.AppendChild(renewInd);

                XmlNode insuranceTerm = doc.CreateElement("PeriodOfInsurance");
                policyHeader.AppendChild(insuranceTerm);

                XmlNode inceptionDate = doc.CreateElement("InceptionDateTime");
                insuranceTerm.AppendChild(inceptionDate); // To be filled from DB

                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    inceptionDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                }

                XmlNode effectiveDate = doc.CreateElement("EffectiveDateTime");
                insuranceTerm.AppendChild(effectiveDate); // To be filled from DB

                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    effectiveDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                }

                XmlNode expiryDate = doc.CreateElement("ExpiryDateTime");
                insuranceTerm.AppendChild(expiryDate); // To be filled from DB

                if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    expiryDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString()).ToShortDateString();
                }

                XmlNode insproductCode = doc.CreateElement("InsurerProductCode");
                policyHeader.AppendChild(insproductCode);
                insproductCode.InnerText = "AUTO";

                XmlNode accNumId = doc.CreateElement("AccountNumberId");
                policyHeader.AppendChild(accNumId);

                XmlNode policyPremium1 = doc.CreateElement("PolicyPremiums");
                policyHeader.AppendChild(policyPremium1);

                XmlNode fullTermP1 = doc.CreateElement("FullTermPremiums");
                policyPremium1.AppendChild(fullTermP1);

                //
                XmlNode assessAmtFTP1 = doc.CreateElement("AssessmentAmount");
                fullTermP1.AppendChild(assessAmtFTP1);
                assessAmtFTP1.InnerText = "0.00";

                XmlNode fireServAmtFTP1 = doc.CreateElement("FireServicesLevyAmount");
                fullTermP1.AppendChild(fireServAmtFTP1);
                fireServAmtFTP1.InnerText = "0.00";

                XmlNode earthQkAmtFTP1 = doc.CreateElement("EarthQuakeLevyAmount");
                fullTermP1.AppendChild(earthQkAmtFTP1);
                earthQkAmtFTP1.InnerText = "0.00";

                XmlNode stampAmtFTP1 = doc.CreateElement("StampDutyAmount");
                fullTermP1.AppendChild(stampAmtFTP1);
                stampAmtFTP1.InnerText = "0.00";

                XmlNode taxAmtFTP1 = doc.CreateElement("TaxAmount");
                fullTermP1.AppendChild(taxAmtFTP1);
                stampAmtFTP1.InnerText = "0.00";

                XmlNode commAmtFTP1 = doc.CreateElement("CommissionAmount");
                fullTermP1.AppendChild(commAmtFTP1);
                commAmtFTP1.InnerText = "0.00";

                XmlNode commTaxAmtFTP1 = doc.CreateElement("CommissionTaxAmount");
                fullTermP1.AppendChild(commTaxAmtFTP1);
                commTaxAmtFTP1.InnerText = "0.00";

                XmlNode commPctFTP1 = doc.CreateElement("CommissionPercent");
                fullTermP1.AppendChild(commPctFTP1);
                commPctFTP1.InnerText = "0.00";

                XmlNode uwFeeAmtFTP1 = doc.CreateElement("UnderwritingFeeAmount");
                fullTermP1.AppendChild(uwFeeAmtFTP1);
                uwFeeAmtFTP1.InnerText = "0.00";

                XmlNode uwFeetaxAmtFTP1 = doc.CreateElement("UnderwritingFeeTaxAmount");
                fullTermP1.AppendChild(uwFeetaxAmtFTP1);
                uwFeetaxAmtFTP1.InnerText = "0.00";

                XmlNode interFeeAmtFTP1 = doc.CreateElement("IntermediaryFeeAmount");
                fullTermP1.AppendChild(interFeeAmtFTP1);
                interFeeAmtFTP1.InnerText = "0.00";

                XmlNode interFeeTaxAmtFTP1 = doc.CreateElement("IntermediaryFeeTaxAmount");
                fullTermP1.AppendChild(interFeeTaxAmtFTP1);
                interFeeTaxAmtFTP1.InnerText = "0.00";

                XmlNode interDiscAmtFTP1 = doc.CreateElement("IntermediaryDiscountAmount");
                fullTermP1.AppendChild(interDiscAmtFTP1);
                interDiscAmtFTP1.InnerText = "0.00";

                XmlNode interDiscTaxAmtFTP1 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                fullTermP1.AppendChild(interDiscTaxAmtFTP1);
                interDiscTaxAmtFTP1.InnerText = "0.00";
                //

                XmlNode currTermP1 = doc.CreateElement("CurrentTermPremiums");
                policyPremium1.AppendChild(currTermP1);
                //
                XmlNode assessAmtCTP1 = doc.CreateElement("AssessmentAmount");
                currTermP1.AppendChild(assessAmtCTP1);
                assessAmtCTP1.InnerText = "0.00";

                XmlNode fireServAmtCTP1 = doc.CreateElement("FireServicesLevyAmount");
                currTermP1.AppendChild(fireServAmtCTP1);
                fireServAmtCTP1.InnerText = "0.00";

                XmlNode earthQkAmtCTP1 = doc.CreateElement("EarthQuakeLevyAmount");
                currTermP1.AppendChild(earthQkAmtCTP1);
                earthQkAmtCTP1.InnerText = "0.00";

                XmlNode stampAmtCTP1 = doc.CreateElement("StampDutyAmount");
                currTermP1.AppendChild(stampAmtCTP1);
                stampAmtCTP1.InnerText = "0.00";

                XmlNode taxAmtCTP1 = doc.CreateElement("TaxAmount");
                currTermP1.AppendChild(taxAmtCTP1);
                taxAmtCTP1.InnerText = "0.00";

                XmlNode commAmtCTP1 = doc.CreateElement("CommissionAmount");
                currTermP1.AppendChild(commAmtCTP1);
                commAmtCTP1.InnerText = "0.00";

                XmlNode commTaxAmtCTP1 = doc.CreateElement("CommissionTaxAmount");
                currTermP1.AppendChild(commTaxAmtCTP1);
                commTaxAmtCTP1.InnerText = "0.00";

                XmlNode commPctCTP1 = doc.CreateElement("CommissionPercent");
                currTermP1.AppendChild(commPctCTP1);
                commPctCTP1.InnerText = "0.00";

                XmlNode uwFeeAmtCTP1 = doc.CreateElement("UnderwritingFeeAmount");
                currTermP1.AppendChild(uwFeeAmtCTP1);
                uwFeeAmtCTP1.InnerText = "0.00";

                XmlNode uwFeetaxAmtCTP1 = doc.CreateElement("UnderwritingFeeTaxAmount");
                currTermP1.AppendChild(uwFeetaxAmtCTP1);
                uwFeetaxAmtCTP1.InnerText = "0.00";

                XmlNode interFeeAmtCTP1 = doc.CreateElement("IntermediaryFeeAmount");
                currTermP1.AppendChild(interFeeAmtCTP1);
                interFeeAmtCTP1.InnerText = "0.00";

                XmlNode interFeeTaxAmtCTP1 = doc.CreateElement("IntermediaryFeeTaxAmount");
                currTermP1.AppendChild(interFeeTaxAmtCTP1);
                interFeeTaxAmtCTP1.InnerText = "0.00";

                XmlNode interDiscAmtCTP1 = doc.CreateElement("IntermediaryDiscountAmount");
                currTermP1.AppendChild(interDiscAmtCTP1);
                interDiscAmtCTP1.InnerText = "0.00";

                XmlNode interDiscTaxAmtCTP1 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                currTermP1.AppendChild(interDiscTaxAmtCTP1);
                interDiscTaxAmtCTP1.InnerText = "0.00";
                //

                XmlNode TransP1 = doc.CreateElement("TransactionPremiums");
                policyPremium1.AppendChild(TransP1);

                //
                XmlNode assessAmtTP1 = doc.CreateElement("AssessmentAmount");
                TransP1.AppendChild(assessAmtTP1);
                assessAmtTP1.InnerText = "0.00";

                XmlNode fireServAmtTP1 = doc.CreateElement("FireServicesLevyAmount");
                TransP1.AppendChild(fireServAmtTP1);
                fireServAmtTP1.InnerText = "0.00";

                XmlNode earthQkAmtTP1 = doc.CreateElement("EarthQuakeLevyAmount");
                TransP1.AppendChild(earthQkAmtTP1);
                earthQkAmtTP1.InnerText = "0.00";

                XmlNode stampAmtTP1 = doc.CreateElement("StampDutyAmount");
                TransP1.AppendChild(stampAmtTP1);
                stampAmtTP1.InnerText = "0.00";

                XmlNode taxAmtTP1 = doc.CreateElement("TaxAmount");
                TransP1.AppendChild(taxAmtTP1);
                taxAmtTP1.InnerText = "0.00";

                XmlNode commAmtTP1 = doc.CreateElement("CommissionAmount");
                TransP1.AppendChild(commAmtTP1);
                commAmtTP1.InnerText = "0.00";

                XmlNode commTaxAmtTP1 = doc.CreateElement("CommissionTaxAmount");
                TransP1.AppendChild(commTaxAmtTP1);
                commTaxAmtTP1.InnerText = "0.00";

                XmlNode commPctTP1 = doc.CreateElement("CommissionPercent");
                TransP1.AppendChild(commPctTP1);
                commPctTP1.InnerText = "0.00";

                XmlNode uwFeeAmtTP1 = doc.CreateElement("UnderwritingFeeAmount");
                TransP1.AppendChild(uwFeeAmtTP1);
                uwFeeAmtTP1.InnerText = "0.00";

                XmlNode uwFeetaxAmtTP1 = doc.CreateElement("UnderwritingFeeTaxAmount");
                TransP1.AppendChild(uwFeetaxAmtTP1);
                uwFeetaxAmtTP1.InnerText = "0.00";

                XmlNode interFeeAmtTP1 = doc.CreateElement("IntermediaryFeeAmount");
                TransP1.AppendChild(interFeeAmtTP1);
                interFeeAmtTP1.InnerText = "0.00";

                XmlNode interFeeTaxAmtTP1 = doc.CreateElement("IntermediaryFeeTaxAmount");
                TransP1.AppendChild(interFeeTaxAmtTP1);
                interFeeTaxAmtTP1.InnerText = "0.00";

                XmlNode interDiscAmtTP1 = doc.CreateElement("IntermediaryDiscountAmount");
                TransP1.AppendChild(interDiscAmtTP1);
                interDiscAmtTP1.InnerText = "0.00";

                XmlNode interDiscTaxAmtTP1 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                TransP1.AppendChild(interDiscTaxAmtTP1);
                interDiscTaxAmtTP1.InnerText = "0.00";
                //

                XmlNode billpartyCode = doc.CreateElement("BillingPartyCode");
                policyHeader.AppendChild(billpartyCode);

                XmlNode billMethodCode = doc.CreateElement("BillingMethodCode");
                policyHeader.AppendChild(billMethodCode);

                XmlNode createDate = doc.CreateElement("CreatedDateTime");
                policyHeader.AppendChild(createDate);

                XmlNode createdBy = doc.CreateElement("CreatedBy");
                policyHeader.AppendChild(createdBy);

                XmlNode lastSaveDate = doc.CreateElement("LastSavedDateTime");
                policyHeader.AppendChild(lastSaveDate);

                XmlNode lastSavedBy = doc.CreateElement("LastSavedBy");
                policyHeader.AppendChild(lastSavedBy);

                XmlNode nodeLOBs = doc.CreateElement("LOBs");
                productsNode.AppendChild(nodeLOBs);

                XmlNode nodeLOB = doc.CreateElement("LOB");
                nodeLOBs.AppendChild(nodeLOB);

                XmlNode nodeLOBCode = doc.CreateElement("LOBCode");
                nodeLOB.AppendChild(nodeLOBCode);
                nodeLOBCode.InnerText = "Vehicle";

                XmlNode nodeLOBId = doc.CreateElement("LOBID");
                nodeLOB.AppendChild(nodeLOBId);
                nodeLOBId.InnerText = "6";

                XmlNode nodeLOBVersion = doc.CreateElement("LOBVersion");
                nodeLOB.AppendChild(nodeLOBVersion);

                XmlNode nodeLOBName = doc.CreateElement("LOBName");
                nodeLOB.AppendChild(nodeLOBName);
                nodeLOBName.InnerText = "Vehicle Auto Product";

                XmlNode nodeLOBCoverCode = doc.CreateElement("CoverCode");
                nodeLOB.AppendChild(nodeLOBCoverCode);


                XmlNode policyPremium2 = doc.CreateElement("PolicyPremiums");
                nodeLOB.AppendChild(policyPremium2);

                XmlNode fullTermP2 = doc.CreateElement("FullTermPremiums");
                policyPremium2.AppendChild(fullTermP2);

                //
                XmlNode assessAmtFTP2 = doc.CreateElement("AssessmentAmount");
                fullTermP2.AppendChild(assessAmtFTP2);
                assessAmtFTP2.InnerText = "0.00";

                XmlNode fireServAmtFTP2 = doc.CreateElement("FireServicesLevyAmount");
                fullTermP2.AppendChild(fireServAmtFTP2);
                fireServAmtFTP2.InnerText = "0.00";

                XmlNode earthQkAmtFTP2 = doc.CreateElement("EarthQuakeLevyAmount");
                fullTermP2.AppendChild(earthQkAmtFTP2);
                earthQkAmtFTP2.InnerText = "0.00";

                XmlNode stampAmtFTP2 = doc.CreateElement("StampDutyAmount");
                fullTermP2.AppendChild(stampAmtFTP2);
                stampAmtFTP2.InnerText = "0.00";

                XmlNode taxAmtFTP2 = doc.CreateElement("TaxAmount");
                fullTermP2.AppendChild(taxAmtFTP2);
                taxAmtFTP2.InnerText = "0.00";

                XmlNode commAmtFTP2 = doc.CreateElement("CommissionAmount");
                fullTermP2.AppendChild(commAmtFTP2);
                commAmtFTP2.InnerText = "0.00";

                XmlNode commTaxAmtFTP2 = doc.CreateElement("CommissionTaxAmount");
                fullTermP2.AppendChild(commTaxAmtFTP2);
                commTaxAmtFTP2.InnerText = "0.00";

                XmlNode commPctFTP2 = doc.CreateElement("CommissionPercent");
                fullTermP2.AppendChild(commPctFTP2);
                commPctFTP2.InnerText = "0.00";

                XmlNode uwFeeAmtFTP2 = doc.CreateElement("UnderwritingFeeAmount");
                fullTermP2.AppendChild(uwFeeAmtFTP2);
                uwFeeAmtFTP2.InnerText = "0.00";

                XmlNode uwFeetaxAmtFTP2 = doc.CreateElement("UnderwritingFeeTaxAmount");
                fullTermP2.AppendChild(uwFeetaxAmtFTP2);
                uwFeetaxAmtFTP2.InnerText = "0.00";

                XmlNode interFeeAmtFTP2 = doc.CreateElement("IntermediaryFeeAmount");
                fullTermP2.AppendChild(interFeeAmtFTP2);
                interFeeAmtFTP2.InnerText = "0.00";

                XmlNode interFeeTaxAmtFTP2 = doc.CreateElement("IntermediaryFeeTaxAmount");
                fullTermP2.AppendChild(interFeeTaxAmtFTP2);
                interFeeTaxAmtFTP2.InnerText = "0.00";

                XmlNode interDiscAmtFTP2 = doc.CreateElement("IntermediaryDiscountAmount");
                fullTermP2.AppendChild(interDiscAmtFTP2);
                interDiscAmtFTP2.InnerText = "0.00";

                XmlNode interDiscTaxAmtFTP2 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                fullTermP2.AppendChild(interDiscTaxAmtFTP2);
                interDiscTaxAmtFTP2.InnerText = "0.00";
                //

                XmlNode currTermP2 = doc.CreateElement("CurrentTermPremiums");
                policyPremium2.AppendChild(currTermP2);
                //
                XmlNode assessAmtCTP2 = doc.CreateElement("AssessmentAmount");
                currTermP2.AppendChild(assessAmtCTP2);
                assessAmtCTP2.InnerText = "0.00";

                XmlNode fireServAmtCTP2 = doc.CreateElement("FireServicesLevyAmount");
                currTermP2.AppendChild(fireServAmtCTP2);
                fireServAmtCTP2.InnerText = "0.00";

                XmlNode earthQkAmtCTP2 = doc.CreateElement("EarthQuakeLevyAmount");
                currTermP2.AppendChild(earthQkAmtCTP2);
                earthQkAmtCTP2.InnerText = "0.00";

                XmlNode stampAmtCTP2 = doc.CreateElement("StampDutyAmount");
                currTermP2.AppendChild(stampAmtCTP2);
                stampAmtCTP2.InnerText = "0.00";

                XmlNode taxAmtCTP2 = doc.CreateElement("TaxAmount");
                currTermP2.AppendChild(taxAmtCTP2);
                taxAmtCTP2.InnerText = "0.00";

                XmlNode commAmtCTP2 = doc.CreateElement("CommissionAmount");
                currTermP2.AppendChild(commAmtCTP2);
                commAmtCTP2.InnerText = "0.00";

                XmlNode commTaxAmtCTP2 = doc.CreateElement("CommissionTaxAmount");
                currTermP2.AppendChild(commTaxAmtCTP2);
                commTaxAmtCTP2.InnerText = "0.00";

                XmlNode commPctCTP2 = doc.CreateElement("CommissionPercent");
                currTermP2.AppendChild(commPctCTP2);
                commPctCTP2.InnerText = "0.00";

                XmlNode uwFeeAmtCTP2 = doc.CreateElement("UnderwritingFeeAmount");
                currTermP2.AppendChild(uwFeeAmtCTP2);
                uwFeeAmtCTP2.InnerText = "0.00";

                XmlNode uwFeetaxAmtCTP2 = doc.CreateElement("UnderwritingFeeTaxAmount");
                currTermP2.AppendChild(uwFeetaxAmtCTP2);
                uwFeetaxAmtCTP2.InnerText = "0.00";

                XmlNode interFeeAmtCTP2 = doc.CreateElement("IntermediaryFeeAmount");
                currTermP2.AppendChild(interFeeAmtCTP2);
                interFeeAmtCTP2.InnerText = "0.00";

                XmlNode interFeeTaxAmtCTP2 = doc.CreateElement("IntermediaryFeeTaxAmount");
                currTermP2.AppendChild(interFeeTaxAmtCTP2);
                interFeeTaxAmtCTP2.InnerText = "0.00";

                XmlNode interDiscAmtCTP2 = doc.CreateElement("IntermediaryDiscountAmount");
                currTermP2.AppendChild(interDiscAmtCTP2);
                interDiscAmtCTP2.InnerText = "0.00";

                XmlNode interDiscTaxAmtCTP2 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                currTermP2.AppendChild(interDiscTaxAmtCTP2);
                interDiscTaxAmtCTP2.InnerText = "0.00";
                //

                XmlNode TransP2 = doc.CreateElement("TransactionPremiums");
                policyPremium2.AppendChild(TransP2);

                //
                XmlNode assessAmtTP2 = doc.CreateElement("AssessmentAmount");
                TransP2.AppendChild(assessAmtTP2);
                assessAmtTP2.InnerText = "0.00";

                XmlNode fireServAmtTP2 = doc.CreateElement("FireServicesLevyAmount");
                TransP2.AppendChild(fireServAmtTP2);
                fireServAmtTP2.InnerText = "0.00";

                XmlNode earthQkAmtTP2 = doc.CreateElement("EarthQuakeLevyAmount");
                TransP2.AppendChild(earthQkAmtTP2);
                earthQkAmtTP2.InnerText = "0.00";

                XmlNode stampAmtTP2 = doc.CreateElement("StampDutyAmount");
                TransP2.AppendChild(stampAmtTP2);
                stampAmtTP2.InnerText = "0.00";

                XmlNode taxAmtTP2 = doc.CreateElement("TaxAmount");
                TransP2.AppendChild(taxAmtTP2);
                taxAmtTP2.InnerText = "0.00";

                XmlNode commAmtTP2 = doc.CreateElement("CommissionAmount");
                TransP2.AppendChild(commAmtTP2);
                commAmtTP2.InnerText = "0.00";

                XmlNode commTaxAmtTP2 = doc.CreateElement("CommissionTaxAmount");
                TransP2.AppendChild(commTaxAmtTP2);
                commTaxAmtTP2.InnerText = "0.00";

                XmlNode commPctTP2 = doc.CreateElement("CommissionPercent");
                TransP2.AppendChild(commPctTP2);
                commPctTP2.InnerText = "0.00";

                XmlNode uwFeeAmtTP2 = doc.CreateElement("UnderwritingFeeAmount");
                TransP2.AppendChild(uwFeeAmtTP2);
                uwFeeAmtTP2.InnerText = "0.00";

                XmlNode uwFeetaxAmtTP2 = doc.CreateElement("UnderwritingFeeTaxAmount");
                TransP2.AppendChild(uwFeetaxAmtTP2);
                uwFeetaxAmtTP2.InnerText = "0.00";

                XmlNode interFeeAmtTP2 = doc.CreateElement("IntermediaryFeeAmount");
                TransP2.AppendChild(interFeeAmtTP2);
                interFeeAmtTP2.InnerText = "0.00";

                XmlNode interFeeTaxAmtTP2 = doc.CreateElement("IntermediaryFeeTaxAmount");
                TransP2.AppendChild(interFeeTaxAmtTP2);
                interFeeTaxAmtTP2.InnerText = "0.00";

                XmlNode interDiscAmtTP2 = doc.CreateElement("IntermediaryDiscountAmount");
                TransP2.AppendChild(interDiscAmtTP2);
                interDiscAmtTP2.InnerText = "0.00";

                XmlNode interDiscTaxAmtTP2 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                TransP2.AppendChild(interDiscTaxAmtTP2);
                interDiscTaxAmtTP2.InnerText = "0.00";
                //

                XmlNode productRiskData = doc.CreateElement("ProductRiskData");
                nodeLOB.AppendChild(productRiskData);

                XmlNode nodeQQInfo = doc.CreateElement("QuickQuoteInformation");
                productRiskData.AppendChild(nodeQQInfo);

                XmlNode nodePolLOB = doc.CreateElement("POLICY_LOB");
                nodeQQInfo.AppendChild(nodePolLOB);

                if (dtRate.Rows[0]["POLICY_LOB"] != System.DBNull.Value)
                {
                    int LOB_UID = int.Parse(dtRate.Rows[0]["POLICY_LOB"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(LOB_UID);
                        nodePolLOB.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }

                }

                XmlNode nodePOLCurr = doc.CreateElement("PolicyCurrencydropdown");
                nodeQQInfo.AppendChild(nodePOLCurr);

                if (dtRate.Rows[0]["POLICY_CURRENCY"] != System.DBNull.Value)
                {
                    int POL_CURR = int.Parse(dtRate.Rows[0]["POLICY_CURRENCY"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(POL_CURR);
                        nodePOLCurr.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeTerm = doc.CreateElement("APP_TERMS");
                nodeQQInfo.AppendChild(nodeTerm);

                if (dtRate.Rows[0]["APP_TERMS"] != System.DBNull.Value)
                {
                    nodeTerm.InnerText = dtRate.Rows[0]["APP_TERMS"].ToString() + " " + "Months";
                }

                XmlNode nodeEffDate = doc.CreateElement("APP_EFFECTIVE_DATE");
                nodeQQInfo.AppendChild(nodeEffDate);
                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    nodeEffDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeExpDate = doc.CreateElement("APP_EXPIRATION_DATE");
                nodeQQInfo.AppendChild(nodeExpDate);
                if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    nodeExpDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeBillType = doc.CreateElement("BILL_TYPE_ID");
                nodeQQInfo.AppendChild(nodeBillType);
                if (dtRate.Rows[0]["BILL_TYPE"] != System.DBNull.Value)
                {
                    string BillCode = dtRate.Rows[0]["BILL_TYPE"].ToString();
                    int BILL_UID = 0;
                    switch (BillCode)
                    {
                        case "AB":
                            BILL_UID = 8459;
                            break;
                        case "DB":
                            BILL_UID = 8460;
                            break;
                        case "DM":
                            BILL_UID = 11150;
                            break;
                        case "DI":
                            BILL_UID = 11191;
                            break;
                        case "MB":
                            BILL_UID = 11276;
                            break;
                        case "AM":
                            BILL_UID = 11277;
                            break;
                        case "IM":
                            BILL_UID = 11278;
                            break;
                        default:
                            BILL_UID = 8459;
                            break;
                    }
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(BILL_UID);
                        nodeBillType.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeInstallType = doc.CreateElement("INSTALL_PLAN_ID");
                nodeQQInfo.AppendChild(nodeInstallType);
                if (dtRate.Rows[0]["INSTALL_PLAN_ID"] != System.DBNull.Value)
                {
                    nodeInstallType.InnerText = dtRate.Rows[0]["INSTALL_PLAN_ID"].ToString();
                }

                XmlNode nodeMultiple = doc.CreateElement("MULTIPLIER");
                nodeQQInfo.AppendChild(nodeMultiple);

                XmlNode nodeClientCode = doc.CreateElement("eProfessionalClientCode");
                nodeQQInfo.AppendChild(nodeClientCode);

                XmlNode nodeCustomerType = doc.CreateElement("CUSTOMER_TYPE");
                nodeQQInfo.AppendChild(nodeCustomerType);
                if (dtRate.Rows[0]["CUSTOMER_TYPE"] != System.DBNull.Value)
                {
                    int custTypeID = int.Parse(dtRate.Rows[0]["CUSTOMER_TYPE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(custTypeID);
                        nodeCustomerType.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeFName = doc.CreateElement("FirstName");
                nodeQQInfo.AppendChild(nodeFName);
                if (dtRate.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                {
                    nodeFName.InnerText = dtRate.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
                }

                XmlNode nodeMName = doc.CreateElement("MiddleName");
                nodeQQInfo.AppendChild(nodeMName);
                if (dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
                {
                    nodeMName.InnerText = dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                }

                XmlNode nodeLName = doc.CreateElement("LastName");
                nodeQQInfo.AppendChild(nodeLName);
                if (dtRate.Rows[0]["CUSTOMER_LAST_NAME"] != System.DBNull.Value)
                {
                    nodeLName.InnerText = dtRate.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                }

                XmlNode nodeGender = doc.CreateElement("GENDER");
                nodeQQInfo.AppendChild(nodeGender);
                if (dtRate.Rows[0]["GENDER"] != System.DBNull.Value)
                {
                    string strGender = dtRate.Rows[0]["GENDER"].ToString();
                    if (strGender == "M")
                    {
                        nodeLName.InnerText = "Male";
                    }
                    else
                    {
                        nodeLName.InnerText = "Female";
                    }
                }

                XmlNode nodeDOB = doc.CreateElement("DOB");
                nodeQQInfo.AppendChild(nodeDOB);
                if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    nodeDOB.InnerText = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString()).ToShortDateString();
                }

                XmlNode nodeNationality = doc.CreateElement("NationalityExcludeForeigner");
                nodeQQInfo.AppendChild(nodeNationality);
                if (dtRate.Rows[0]["NATIONALITY"] != System.DBNull.Value)
                {
                    string strCountry = dtRate.Rows[0]["NATIONALITY"].ToString();
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetCountryName(strCountry);
                        nodeNationality.InnerText = dt.Rows[0]["COUNTRY_NAME"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeOccupationType = doc.CreateElement("OCCUPATION");
                nodeQQInfo.AppendChild(nodeOccupationType);
                if (dtRate.Rows[0]["IS_HOME_EMPLOYEE"] != System.DBNull.Value)
                {
                    string strOCCType = dtRate.Rows[0]["IS_HOME_EMPLOYEE"].ToString();
                    if (strOCCType == "1")
                    {
                        nodeOccupationType.InnerText = "Indoor";
                    }
                    else
                    {
                        nodeOccupationType.InnerText = "Outdoor";
                    }
                }

                XmlNode nodeOccupation = doc.CreateElement("OccupationList");
                nodeQQInfo.AppendChild(nodeOccupation);
                if (dtRate.Rows[0]["CUSTOMER_OCCU"] != System.DBNull.Value)
                {
                    int strOccId = int.Parse(dtRate.Rows[0]["CUSTOMER_OCCU"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(strOccId);
                        nodeOccupation.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeDriveExp = doc.CreateElement("DRIVING_EXP");
                nodeQQInfo.AppendChild(nodeDriveExp);
                if (dtRate.Rows[0]["DRIVER_EXP_YEAR"] != System.DBNull.Value)
                {
                    nodeDriveExp.InnerText = dtRate.Rows[0]["DRIVER_EXP_YEAR"].ToString();
                }

                XmlNode nodeExistNCD = doc.CreateElement("EXISTING_NCD");
                nodeQQInfo.AppendChild(nodeExistNCD);
                if (dtRate.Rows[0]["EXISTING_NCD"] != System.DBNull.Value)
                {
                    string EXNCD = dtRate.Rows[0]["EXISTING_NCD"].ToString();
                    nodeExistNCD.InnerText = EXNCD.Remove(EXNCD.IndexOf("%"));
                }

                XmlNode nodeAnyClaim = doc.CreateElement("AnyClaimmade");
                nodeQQInfo.AppendChild(nodeAnyClaim);
                if (dtRate.Rows[0]["ANY_CLAIM"] != System.DBNull.Value)
                {
                    string strIsClaim = dtRate.Rows[0]["ANY_CLAIM"].ToString();
                    if (strIsClaim.Trim() == "1")
                    {
                        nodeAnyClaim.InnerText = "Yes";
                    }
                    else
                    {
                        nodeAnyClaim.InnerText = "No";
                    }
                }

                XmlNode nodeNCDLESS10 = doc.CreateElement("Isyourexisting");
                nodeQQInfo.AppendChild(nodeNCDLESS10);
                if (dtRate.Rows[0]["EXIST_NCD_LESS_10"] != System.DBNull.Value)
                {
                    string NCDLess = dtRate.Rows[0]["EXIST_NCD_LESS_10"].ToString();
                    if (NCDLess.Trim() == "1")
                    {
                        nodeNCDLESS10.InnerText = "Yes";
                    }
                    else
                    {
                        nodeNCDLESS10.InnerText = "No";
                    }
                }

                XmlNode nodeDemerit = doc.CreateElement("DemeritsPointsFreeDiscounts");
                nodeQQInfo.AppendChild(nodeDemerit);
                if (dtRate.Rows[0]["DEMERIT_DISCOUNT"] != System.DBNull.Value)
                {
                    string strDemerit = dtRate.Rows[0]["DEMERIT_DISCOUNT"].ToString();
                    if (strDemerit.Trim() == "1")
                    {
                        nodeDemerit.InnerText = "Yes";
                    }
                    else
                    {
                        nodeDemerit.InnerText = "No";
                    }
                }

                XmlNode nodeDriverAge = doc.CreateElement("Driver_AGE");
                nodeQQInfo.AppendChild(nodeDriverAge);


                int yearDOB = 0;
                int yearEff = 0;
                if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    DateTime dtDOB = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString());
                    yearDOB = dtDOB.Year;
                }
                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    DateTime dtEff = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                    yearEff = dtEff.Year;
                }

                int dAge = yearEff - yearDOB;
                nodeDemerit.InnerText = dAge.ToString();



                XmlNode nodeRiskInfo = doc.CreateElement("RiskInformation");
                productRiskData.AppendChild(nodeRiskInfo);

                XmlNode nodeYearReg = doc.CreateElement("YEAR_OF_REG");
                nodeRiskInfo.AppendChild(nodeYearReg);
                if (dtRate.Rows[0]["YEAR_OF_REG"] != System.DBNull.Value)
                {
                    nodeYearReg.InnerText = dtRate.Rows[0]["YEAR_OF_REG"].ToString();
                }

                XmlNode nodeMake = doc.CreateElement("MAKE");
                nodeRiskInfo.AppendChild(nodeMake);
                if (dtRate.Rows[0]["MAKE"] != System.DBNull.Value)
                {
                    int makeID = int.Parse(dtRate.Rows[0]["MAKE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(makeID);
                        nodeMake.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeModel = doc.CreateElement("MODEL");
                nodeRiskInfo.AppendChild(nodeModel);
                if (dtRate.Rows[0]["MODEL"] != System.DBNull.Value)
                {
                    int ModelID = int.Parse(dtRate.Rows[0]["MODEL"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetModelDesc(ModelID);
                        nodeModel.InnerText = dt.Rows[0]["MODEL"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeVehType = doc.CreateElement("VEHICLE_TYPE");
                nodeRiskInfo.AppendChild(nodeVehType);
                if (dtRate.Rows[0]["MODEL_TYPE"] != System.DBNull.Value)
                {
                    int TypeId = int.Parse(dtRate.Rows[0]["MODEL_TYPE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetModelTypeDesc(TypeId);
                        nodeVehType.InnerText = dt.Rows[0]["MODEL_TYPE"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeEngCapacity = doc.CreateElement("ENG_CAPACITY");
                nodeRiskInfo.AppendChild(nodeEngCapacity);
                if (dtRate.Rows[0]["ENG_CAPACITY"] != System.DBNull.Value)
                {
                    nodeEngCapacity.InnerText = dtRate.Rows[0]["ENG_CAPACITY"].ToString();
                }

                XmlNode nodeNumDriver = doc.CreateElement("NO_OF_DRIVERS");
                nodeRiskInfo.AppendChild(nodeNumDriver);
                if (dtRate.Rows[0]["NO_OF_DRIVERS"] != System.DBNull.Value)
                {
                    nodeNumDriver.InnerText = dtRate.Rows[0]["NO_OF_DRIVERS"].ToString();
                }

                XmlNode nodeClaims = doc.CreateElement("Areclaims");
                nodeRiskInfo.AppendChild(nodeClaims);
                if (dtRate.Rows[0]["CLAIM_RISK"] != System.DBNull.Value)
                {
                    string strRiskClaim = dtRate.Rows[0]["CLAIM_RISK"].ToString();
                    if (strRiskClaim.Trim() == "1")
                    {
                        nodeClaims.InnerText = "Yes";
                    }
                    else
                    {
                        nodeClaims.InnerText = "No";
                    }
                }

                XmlNode nodeNumClaim = doc.CreateElement("Noofclaims");
                nodeRiskInfo.AppendChild(nodeNumClaim);
                if (dtRate.Rows[0]["NO_OF_CLAIM"] != System.DBNull.Value)
                {
                    nodeNumClaim.InnerText = dtRate.Rows[0]["NO_OF_CLAIM"].ToString();
                }

                XmlNode nodeTotalClaimAmt = doc.CreateElement("TOTAL_CLAIM_AMT");
                nodeRiskInfo.AppendChild(nodeTotalClaimAmt);
                if (dtRate.Rows[0]["TOTAL_CLAIM_AMT"] != System.DBNull.Value)
                {
                    nodeTotalClaimAmt.InnerText = dtRate.Rows[0]["TOTAL_CLAIM_AMT"].ToString();
                }

                XmlNode nodeCoverage = doc.CreateElement("COVERAGE_TYPE");
                nodeRiskInfo.AppendChild(nodeCoverage);
                if (dtRate.Rows[0]["COVERAGE_TYPE"] != System.DBNull.Value)
                {
                    int COVID = int.Parse(dtRate.Rows[0]["COVERAGE_TYPE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetCoverageInfobyID(COVID);
                        nodeCoverage.InnerText = dt.Rows[0]["COV_DES"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeNoClaimDisc = doc.CreateElement("NoClaimsDiscount");
                nodeRiskInfo.AppendChild(nodeNoClaimDisc);
                if (dtRate.Rows[0]["NO_CLAIM_DISCOUNT"] != System.DBNull.Value)
                {
                    string strNoClaim = dtRate.Rows[0]["NO_CLAIM_DISCOUNT"].ToString();
                    if (strNoClaim.Trim() == "1")
                    {
                        nodeNoClaimDisc.InnerText = "Yes";
                    }
                    else
                    {
                        nodeNoClaimDisc.InnerText = "No";
                    }
                }

                XmlNode nodeFromDate = doc.CreateElement("Fromdate");
                nodeRiskInfo.AppendChild(nodeFromDate);
                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    nodeFromDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeToDate = doc.CreateElement("ToDate");
                nodeRiskInfo.AppendChild(nodeToDate);
                if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    nodeToDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeCalEvoke = doc.CreateElement("CalculateEvokePage");
                productRiskData.AppendChild(nodeCalEvoke);

                XmlNode nodepremiumOprion = doc.CreateElement("PagePremiumOptionExist");
                productRiskData.AppendChild(nodepremiumOprion);

                XmlNode nodeDisplayCom = doc.CreateElement("DisplayComputation");
                productRiskData.AppendChild(nodeDisplayCom);

                XmlNode nodeMultiQuote = doc.CreateElement("MultiQuoteApplies");
                productRiskData.AppendChild(nodeMultiQuote);


                string strRqXML = doc.OuterXml.ToString();

                return strRqXML;

            }
            else
            {
                return "";
            }
        }

        private string GetPremiumFromXml(string strResponseXml)
        {
            string strTotalPremium = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strResponseXml);

            if (doc.InnerText != null)
            {
                XmlNode nodePremium = doc.SelectSingleNode("Product/PolicyHeader/PolicyPremiums/FullTermPremiums");

                if (nodePremium != null)
                {
                    XmlNode currentNode = nodePremium.SelectSingleNode("AssessmentAmount");
                    strTotalPremium = currentNode.InnerText.ToString();
                }
            }
            return strTotalPremium;
        }

        private void generateAppQuote()
        {
            try
            {
                #region		GENERATE QUOTE
                ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();

                lIntCUSTOMER_ID = int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
                lIntAPP_ID = int.Parse(Request["APP_ID"] == null || Request["APP_ID"] == "" ? "0" : Request["APP_ID"].ToString());
                lIntAPP_VERSION_ID = int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] == "" ? "0" : Request["APP_VERSION_ID"].ToString());
                lStrLobID = Request["LOB_ID"] == null || Request["LOB_ID"] == "" ? "0" : Request["LOB_ID"].ToString();
                if (lStrLobID == "" || lStrLobID == "0")
                {
                    lStrLobID = GetLOBID();
                }


                ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);


                string lStrShowQuote = "";
                lStrShowQuote = objGenerateQuote.GenerateQuote(lIntCUSTOMER_ID, lIntAPP_ID, lIntAPP_VERSION_ID, lStrLobID, out lIntQuoteID).ToString();

                if (lStrShowQuote.ToString() == "1" || lStrShowQuote.ToString() == "6")
                {
                    lIntShowQuote = int.Parse(lStrShowQuote);
                }

                Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID=" + lIntCUSTOMER_ID + "&APP_ID=" + lIntAPP_ID + "&APP_VERSION_ID=" + lIntAPP_VERSION_ID + "&LOBID=" + lStrLobID + "&QUOTE_ID=" + lIntQuoteID + "&SHOW=" + lIntShowQuote);

                # endregion
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        private void generatePolicyQuote()
        {
            try
            {
                #region		GENERATE QUOTE


                lIntCUSTOMER_ID = int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
                lIntPOLICY_ID = int.Parse(Request["POLICY_ID"] == null || Request["POLICY_ID"] == "" ? "0" : Request["POLICY_ID"].ToString());
                lIntPOLICY_VERSION_ID = int.Parse(Request["POLICY_VERSION_ID"] == null || Request["POLICY_VERSION_ID"] == "" ? "0" : Request["POLICY_VERSION_ID"].ToString());
                if (Request.QueryString["CALLEDFROM"] != null && Request.QueryString["CALLEDFROM"].ToString() != "")
                {
                    CALLEDFROM = Request.QueryString["CALLEDFROM"].ToString();
                }
                if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                    lStrLobID = Request.QueryString["LOB_ID"].ToString();
                else
                    lStrLobID = GetLOBID();
                if (CALLEDFROM != "QAPP") //Added by Lalit August 2,2010
                {
                    Cms.BusinessLayer.BlQuote.ClsGenerateQuote objGenerateQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(CarrierSystemID);
                    string strCSSNo = base.GetColorScheme();
                    //string strpolLobID = GetLOBID();
                    int intPolQuote_ID, intShowQuote;
                    // if (Convert.ToInt32(lStrLobID) < 9)
                    // {

                    intShowQuote = objGenerateQuote.GeneratePolicyQuote(lIntCUSTOMER_ID, lIntPOLICY_ID, lIntPOLICY_VERSION_ID, lStrLobID, strCSSNo, out intPolQuote_ID, GetUserId());
                    if (intShowQuote == 1 || intShowQuote == 2 || intShowQuote == 3 || intShowQuote == 6)
                    {
                        Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&ONLYEFFECTIVE=false&CUSTOMER_ID=" + lIntCUSTOMER_ID + "&POLICY_ID=" + lIntPOLICY_ID + "&POLICY_VERSION_ID=" + lIntPOLICY_VERSION_ID + "&LOBID=" + lStrLobID + "&QUOTE_ID=" + intPolQuote_ID + "&SHOW=" + intShowQuote);
                    }
                    else if (intShowQuote == 5)
                    {
                        
                        Response.Write(ClsMessages.FetchGeneralMessage("1169"));
                        // Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&ONLYEFFECTIVE=false&CUSTOMER_ID=" + lIntCUSTOMER_ID + "&POLICY_ID=" + lIntPOLICY_ID + "&POLICY_VERSION_ID=" + lIntPOLICY_VERSION_ID + "&LOBID=" + lStrLobID + "&QUOTE_ID=" + intPolQuote_ID + "&SHOW=" + intShowQuote);
                    }
                    if (intShowQuote == 0)
                    {
                        string verificationHTML = "";
                        ClsGenerateQuote ObjGenerQuote = new ClsGenerateQuote(CarrierSystemID);
                        ClsGeneralInformation objGenInformation = new ClsGeneralInformation();
                        string strInputXML = objGenInformation.GetPolicyInputXML(lIntCUSTOMER_ID, lIntPOLICY_ID, lIntPOLICY_VERSION_ID, lStrLobID);
                        string retVal = ObjGenerQuote.InputXmlVerification(strInputXML, lStrLobID); //return is in the format string#0 or string#1 . 0 --> invalid  1-->valid
                        string[] retValue = retVal.Split('#');
                        verificationHTML = retValue[0];
                        Response.Write(verificationHTML);
                        Response.End();
                    }
                }
                else
                {
                    //Generate QuickApp Qote
                    Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM="+CALLEDFROM+"&CUSTOMER_ID=" + lIntCUSTOMER_ID + "&POLICY_ID=" + lIntPOLICY_ID + "&POLICY_VERSION_ID=" + lIntPOLICY_VERSION_ID +"&LOBID=" + lStrLobID);
                }
                # endregion
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        //For Generating QQ rates:
        private void generateQQuote()
        {
            try
            {
                #region		GENERATE QUOTE FOR QQ


                lIntCUSTOMER_ID = int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
                lIntQuoteID = int.Parse(Request["QQ_ID"] == null ? "0" : Request["QQ_ID"].ToString());
                lStrLobID = Request["LOB_ID"] == null || Request["LOB_ID"] == "" ? "0" : Request["LOB_ID"].ToString();
                Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QQ&CUSTOMER_ID=" + lIntCUSTOMER_ID + "&QQ_ID=" + lIntQuoteID + "&LOBID=" + lStrLobID);

                # endregion
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
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

        private void GenratePolicyRate(int lIntCUSTOMER_ID, int lIntPOLICY_ID, int lIntPOLICY_VERSION_ID, int lStrLobID, string CALLEDFROM)
        {
            int QouteId = 0;

            ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(CarrierSystemID);
            QouteId = objGenerateQuote.GetCoverages_QuoteId(lIntCUSTOMER_ID, lIntPOLICY_ID, lIntPOLICY_VERSION_ID, lStrLobID, int.Parse(GetColorScheme()), int.Parse(GetUserId()), CALLEDFROM);
            if (CALLEDFROM == "" || CALLEDFROM == null)
            {
                CALLEDFROM = "QUOTE_POL";
            }

            if (QouteId != 0)
            {
                Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID=" + lIntCUSTOMER_ID + "&POLICY_ID=" + lIntPOLICY_ID + "&POLICY_VERSION_ID=" + lIntPOLICY_VERSION_ID + "&LOBID=" + lStrLobID + "&CALLEDFROM=" + CALLEDFROM + "&QUOTE_ID=" + QouteId);
            }
        }


    }
}
