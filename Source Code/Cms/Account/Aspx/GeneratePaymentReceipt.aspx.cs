/******************************************************************************************
<Author					: - Pradeep Kushwaha 
<Start Date				: -	17-Nov-2010
<End Date				: -	
<Description			: - Payment receipt display screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Display payment receipt details
*******************************************************************************************/
#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Model.Account;
using System.Threading;
using System.Globalization;
using System.Data;
using System.Text;
using Cms.BusinessLayer.BlCommon;
#endregion


namespace Cms.Account.Aspx
{
    public partial class GeneratePaymentReceipt : AccountBase
    {
        ClsDepositDetails ObjDepositDetails = new ClsDepositDetails();

        
                public NumberFormatInfo nfi;
        protected void Page_Load(object sender, EventArgs e)
                {
                    btnPrint.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505");
            if (!IsPostBack)
            {
                try
                {
                    GenerateReceipt();
                }
                catch(Exception ex)
                {   LblMsg.Visible = true;
                    LblMsg.Text = ex.Message.ToString();
                }
            }//if (!IsPostBack)
        }//protected void Page_Load(object sender, EventArgs e)
        private void GenerateReceipt()
        {
            ClsAddDepositDetailsinfo objAddDepositDetailsinfo = new ClsAddDepositDetailsinfo();
            
            if (Request.QueryString["DEPOSIT_ID"] != null && Request.QueryString["DEPOSIT_ID"].ToString() != "" &&
            Request.QueryString["CD_LINE_ITEM_ID"] != null && Request.QueryString["CD_LINE_ITEM_ID"].ToString() != "" &&
            (Request.QueryString["CALLED_FOR"] == null || Request.QueryString["CALLED_FOR"].ToString() == ""))
            {
                StringBuilder HTML = new StringBuilder();
                
                Int32 _DEPOSIT_ID = 0;
                Int32 _CD_LINE_ITEM_ID = 0;
                String _RECEIPT_NUM = String.Empty;
                _DEPOSIT_ID = int.Parse(Request.QueryString["DEPOSIT_ID"].ToString());
                _CD_LINE_ITEM_ID = int.Parse(Request.QueryString["CD_LINE_ITEM_ID"].ToString());
                
                
                objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue = _DEPOSIT_ID;
                objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue = _CD_LINE_ITEM_ID;

                this.ViedReceiptOfCommittedDeposit(objAddDepositDetailsinfo); 

                 
            }
            if (Request.QueryString["CALLED_FOR"] != null && Request.QueryString["CALLED_FOR"].ToString() != "" && Request.QueryString["CALLED_FOR"].ToString() == "ALL" 
                && Request.QueryString["DEPOSIT_ID"] != null && Request.QueryString["DEPOSIT_ID"].ToString() != "" )
            {
                objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue = int.Parse(Request.QueryString["DEPOSIT_ID"]);
                objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue = base.GetEbixIntDefaultValue();
                this.ViedReceiptOfCommittedDeposit(objAddDepositDetailsinfo); 
            }
        }
        /// <summary>
        /// Use to generated the All receipt of commited deposit 
        /// </summary>
        /// <param name="objAddDepositDetailsinfo"></param>
        private void ViedReceiptOfCommittedDeposit(ClsAddDepositDetailsinfo objAddDepositDetailsinfo)
        {
            
           
            DataSet dsDepositCommitted = objAddDepositDetailsinfo.FetchDataOfCommittedDeposit();
            StringBuilder HTML = new StringBuilder();

            if (dsDepositCommitted!=null && dsDepositCommitted.Tables[0].Rows.Count != 0)
            {   //The Receipt print one per page - implementation by Pradeep on 15-July-2011
                int Count = 0;
                foreach (DataRow dr in dsDepositCommitted.Tables[0].Rows)
                {
                    ClsAddDepositDetailsinfo objClsAddDepositDetailsinfo = new ClsAddDepositDetailsinfo();
                    String _RECEIPT_NUM = String.Empty;

                    objClsAddDepositDetailsinfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objClsAddDepositDetailsinfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());

                    objClsAddDepositDetailsinfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    objClsAddDepositDetailsinfo.RECEIPT_ISSUED_DATE.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    objClsAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue = int.Parse(dr["DEPOSIT_ID"].ToString());
                    objClsAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue = int.Parse(dr["CD_LINE_ITEM_ID"].ToString());
                    objClsAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue = int.Parse(dr["CUSTOMER_ID"].ToString());
                    objClsAddDepositDetailsinfo.POLICY_ID.CurrentValue = int.Parse(dr["POLICY_ID"].ToString());
                    objClsAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue = int.Parse(dr["POLICY_VERSION_ID"].ToString());
                    _RECEIPT_NUM = dr["RECEIPT_NUM"].ToString();
                    //The Receipt print one per page - implementation by Pradeep on 15-July-2011
                    if (Count < dsDepositCommitted.Tables[0].Rows.Count - 1)
                    {
                        HTML.Append("<div style='page-break-after: always'>");
                        if (_RECEIPT_NUM != "")
                        {
                            objClsAddDepositDetailsinfo.RECEIPT_NUM.CurrentValue = _RECEIPT_NUM;

                            HTML.Append(GenerateReceiptDetailsHTML(objClsAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue
                                , objClsAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue,
                                _RECEIPT_NUM));
                        }
                        else
                            HTML.Append(GenerateReceiptDetails(objClsAddDepositDetailsinfo, _RECEIPT_NUM));
                        HTML.Append("</div>");
                    }
                    else
                    {
                        if (_RECEIPT_NUM != "")
                        {
                            objClsAddDepositDetailsinfo.RECEIPT_NUM.CurrentValue = _RECEIPT_NUM;
                            HTML.Append(GenerateReceiptDetailsHTML(objClsAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue
                                , objClsAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue,
                                _RECEIPT_NUM));
                        }
                        else
                            HTML.Append(GenerateReceiptDetails(objClsAddDepositDetailsinfo, _RECEIPT_NUM));
                    }
                    Count++;
                    //till here 
                }

                LiteralControl includet = new LiteralControl(HTML.ToString());
                Panel1.Controls.Add(includet);
              
            }//if (dsDepositCommitted!=null && dsDepositCommitted.Tables[0].Rows.Count != 0)
            if (HTML.ToString() == "")
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                LblMsg.Visible = true;
                LblMsg.Text = ClsMessages.FetchGeneralMessage("1997");
            }
            else
            {
                btnPrint.Visible = true;
                LblMsg.Visible = false;
            }
        }
        
        private void GetDepositReceiptData(ClsAddDepositDetailsinfo objAddDepositDetailsinfo)
        {
            DataSet dsDepositReceipt = objAddDepositDetailsinfo.FetchData();
            if (dsDepositReceipt.Tables[0].Rows.Count != 0)
            {
                objAddDepositDetailsinfo.RECEIPT_NUM.CurrentValue = dsDepositReceipt.Tables[0].Rows[0]["RECEIPT_NUM"].ToString();
               
            }//if (dsDepositReceipt.Tables[0].Rows.Count != 0)
        }
        private StringBuilder GenerateReceiptDetails(ClsAddDepositDetailsinfo objAddDepositDetailsinfo, String RECEIPT_NUM)
        {
            StringBuilder HTML = new StringBuilder();
            try
            {
               
                if (RECEIPT_NUM == "")
                {
                    int intRetval = ObjDepositDetails.GenerateReceiptOfDepositLineItemInfo(objAddDepositDetailsinfo);

                    if (intRetval > 0)
                    {

                        this.GetDepositReceiptData(objAddDepositDetailsinfo);
                        HTML.Append(GenerateReceiptDetailsHTML(objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue,
                        objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue,
                        objAddDepositDetailsinfo.RECEIPT_NUM.CurrentValue));
                    }
                    else if (intRetval == -10)
                    {
                        LblMsg.Text = ClsMessages.GetMessage(base.ScreenId, "31");
                        LblMsg.Visible = true;
                    }
                    else
                    {
                        LblMsg.Text = ClsMessages.GetMessage(base.ScreenId, "32");
                        LblMsg.Visible = true;
                    }
                }
                else
                {
                   HTML.Append(GenerateReceiptDetailsHTML(objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue,
                        objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue,
                        objAddDepositDetailsinfo.RECEIPT_NUM.CurrentValue));
                }
               

            }
            catch (Exception ex)
            {
                
                LblMsg.Text = ClsMessages.GetMessage(base.ScreenId, "32") + " - " + ex.Message;
                LblMsg.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return HTML;
        }
        /// <summary>
        /// Use to get the receipt details 
        /// </summary>
        /// <param name="DEPOSIT_ID"></param>
        /// <param name="CD_LINE_ITEM_ID"></param>
        /// <param name="RECEIPT_NUM"></param>
        private StringBuilder GenerateReceiptDetailsHTML(int DEPOSIT_ID, int CD_LINE_ITEM_ID, string RECEIPT_NUM)
        {
            DataSet GeneratedReceiptDS = ObjDepositDetails.FetchGeneratedReceiptDepositLineItemsData(DEPOSIT_ID, CD_LINE_ITEM_ID, RECEIPT_NUM,int.Parse(GetUserId()));
            StringBuilder HTML = new StringBuilder();
            if (GeneratedReceiptDS.Tables[0].Rows.Count != 0 && GeneratedReceiptDS.Tables[1].Rows.Count!=0)
            {
                

                
                #region H T M L C O D I N G
                    HTML.Append("<table align='center' ><tr><td>");//top table
                    HTML.Append("<table align='center' style='width: 700px;  border: 1pt double #000000; background-color: #FFFFFF;'>");//table start
                    HTML.Append("<tr>");
                    HTML.Append("<td>");
                    HTML.Append("<table align='center' style='height: 430px; width: 700px; vertical-align: baseline; border: .1pt solid #000000;'>");
                    HTML.Append("<tr><td>");
                    HTML.Append("<table align='center' style='height: 420px; width: 674px; vertical-align: baseline; border: 1pt dotted #000000;'>");
                    // 1 TD START 
                    HTML.Append("<tr><td align='right' valign='middle' ><table><tr><td>");
                    HTML.Append("<img alt='' src=/cms/cmsweb/images/AliancaLogo.gif />");//dynamic
                    HTML.Append("</td></tr></table></td>");
                    // END

                    HTML.Append(" <td align='center' >");
                    HTML.Append("<span style='font-family:Times New Roman, Times, serif;font-weight: bold;font-size: medium;'>CAMPANHIA DE SEGUROS ALIANCA DA BAHIA</span>");//dynamic  
                    HTML.Append("<br />" + GeneratedReceiptDS.Tables[0].Rows[0]["REIN_COMAPANY_ADD1"].ToString() + " Tel.: " + GeneratedReceiptDS.Tables[0].Rows[0]["REIN_COMAPANY_PHONE"].ToString() + " - " + GeneratedReceiptDS.Tables[0].Rows[0]["REIN_COMAPANY_CITY"].ToString() + " - " + GeneratedReceiptDS.Tables[0].Rows[0]["REIN_COMAPANY_STATE"].ToString() + "<br />");
                    HTML.Append(" C.N.P.J - " + GeneratedReceiptDS.Tables[0].Rows[0]["CARRIER_CNPJ"].ToString() + "</td></tr>");//dynamic       
                    
                    HTML.Append("<tr><td colspan='2' align='center' valign='top'><hr /></td></tr>");
                    HTML.Append("<tr align='center'><tr><td colspan='2'><table><tr><td align='center' valign='top' style='width:30%'></td><td align='center'>");
                    HTML.Append("<table align='center' style='width: 238px; border: 1pt double #000000; background-color: #FFFFFF;'><tr>");
                    HTML.Append("<td align='center' style='font-size: small; font-family: Arial, Helvetica, sans-serif; font-weight: 700;'>");
                    HTML.Append("RECIBO" + " PROVISÓRIO" + "</td>");//Dynamic
                    HTML.Append("<td align='right'></td></tr></table>");
                    HTML.Append("</td><td align='center' style='width:40%'>" + GeneratedReceiptDS.Tables[1].Rows[0]["RECEIPT_NUM"].ToString() + "</td></tr></table></td></tr></tr>");//Daynamic
                    HTML.Append(" <tr><td colspan='2' align='left' valign='top'>");
                    String strAPPLICANT_NAME=String.Empty;
                    if(GeneratedReceiptDS.Tables[1].Rows[0]["APPLICANT_NAME"].ToString().Length<=50)
                        strAPPLICANT_NAME = GeneratedReceiptDS.Tables[1].Rows[0]["APPLICANT_NAME"].ToString().PadRight(50-GeneratedReceiptDS.Tables[1].Rows[0]["APPLICANT_NAME"].ToString().Length, '_');
                    else
                        strAPPLICANT_NAME = GeneratedReceiptDS.Tables[1].Rows[0]["APPLICANT_NAME"].ToString().PadLeft(GeneratedReceiptDS.Tables[1].Rows[0]["APPLICANT_NAME"].ToString().Length, '_');

                    HTML.Append(" <span style='font-size: medium;'>Recebemos de</span> <span style='text-decoration: underline;'>" + strAPPLICANT_NAME + " </span></td></tr>");//Dynamic
                    HTML.Append("<tr><td colspan='2' align='left' valign='top'>");

                    System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;
                    nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
                    nfi.NumberDecimalDigits = 2;

                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(enumCulture.BR);
                    System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                    culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

                    DateTime dtRECEIPT_ISSUED_DATE = Convert.ToDateTime(GeneratedReceiptDS.Tables[1].Rows[0]["RECEIPT_ISSUED_DATE"]);
                    String Strday = Convert.ToDateTime(GeneratedReceiptDS.Tables[1].Rows[0]["RECEIPT_ISSUED_DATE"]).Day.ToString();
                    String StrMonth = String.Format("{0:MMMM}", dtRECEIPT_ISSUED_DATE);  // "3 03 Mar March"  month
                    String StrYear = Convert.ToDateTime(GeneratedReceiptDS.Tables[1].Rows[0]["RECEIPT_ISSUED_DATE"]).Year.ToString();
    

                    String strReceiptAmount = Convert.ToDouble(GeneratedReceiptDS.Tables[1].Rows[0]["RECEIPT_AMOUNT"]).ToString("N", nfi).ToString();
                    ClsCommon ObjClsCommon = new ClsCommon();
                    Double num = Double.Parse(strReceiptAmount, CultureInfo.GetCultureInfo("pt-BR").NumberFormat);
           
                    Thread.CurrentThread.CurrentCulture = oldculture;

                    String StrConvertedWordPortugueseWords = ObjClsCommon.changeNumericToWords(num).Trim();
                    
                    if (StrConvertedWordPortugueseWords.Length <= 60)
                        StrConvertedWordPortugueseWords = StrConvertedWordPortugueseWords.PadRight(38, '*');
                    else
                        StrConvertedWordPortugueseWords = StrConvertedWordPortugueseWords.PadRight(138, '*');

                    StrConvertedWordPortugueseWords += "***";


                    strReceiptAmount=strReceiptAmount.PadRight(25-strReceiptAmount.Length,'_');

                    HTML.Append("<span style='font-size: medium;'> a importância de R$ </span><span style='text-decoration: underline;'>" + strReceiptAmount + "</span> ( &nbsp;<span style='text-decoration: underline;'>" + StrConvertedWordPortugueseWords + " </span>&nbsp; </td></tr>");//dynamic
                    HTML.Append("<tr><td colspan='2' align='left' valign='top'>");
                    String strINSTALLMENT_NO = GeneratedReceiptDS.Tables[1].Rows[0]["INSTALLMENT_NO"].ToString().PadRight(42,'_');
                    HTML.Append("<span style='font-size: medium;'>correspondente ao pagamento da parcela </span> <span style='text-decoration: underline;'>" + strINSTALLMENT_NO + "</span></td></tr>"); //dynamic
                    HTML.Append("<tr><td colspan='2' align='left' valign='top'>");
                    String strAPP_NUMBER=GeneratedReceiptDS.Tables[1].Rows[0]["APP_NUMBER"].ToString().PadRight(38,'_');
                    HTML.Append(" <span style='font-size: medium;'>do prêmio da Apólice conforme proposta nº </span>  <span style='text-decoration: underline;'>" + strAPP_NUMBER + "</span></td></tr>");//dynamic
                    HTML.Append("<tr><td colspan='2' align='left' valign='top'>");
                    String strPOLICY_LOB = GeneratedReceiptDS.Tables[1].Rows[0]["LOB_DESC"].ToString().PadRight(50, '_');
                    
                    HTML.Append("<span style='font-size: medium;'>Ramo  </span><span style='text-decoration: underline;'>" + strPOLICY_LOB + "</span> em emissão.</td></tr>");
                    HTML.Append("<tr><td align='left' style='width:15%' valign='top'>");
                    HTML.Append("<span style='font-size: medium;'>Cheque nº </span>  ");//dynamic
                    String strCHECK_NUM = String.Empty;
                    if (GeneratedReceiptDS.Tables[1].Rows[0]["CHECK_NUM"].ToString().Length <= 7)
                        strCHECK_NUM = GeneratedReceiptDS.Tables[1].Rows[0]["CHECK_NUM"].ToString().PadRight(5 - GeneratedReceiptDS.Tables[1].Rows[0]["CHECK_NUM"].ToString().Length, '_');
                    else
                        strCHECK_NUM = GeneratedReceiptDS.Tables[1].Rows[0]["CHECK_NUM"].ToString().PadRight(5,'_');
                    HTML.Append("<span style='text-decoration: underline;'>" + strCHECK_NUM + "</span></td> ");//dynamic
                    HTML.Append("<td style='width:50%'><span style='font-size: medium;'>Banco:</span>");//dynamic
                    String strBANK_NUMBER = String.Empty;
                    if (GeneratedReceiptDS.Tables[1].Rows[0]["BANK_NUMBER"].ToString().Length <= 25)
                        strBANK_NUMBER = GeneratedReceiptDS.Tables[1].Rows[0]["BANK_NUMBER"].ToString().PadRight(25,'_');
                    else
                        strBANK_NUMBER = GeneratedReceiptDS.Tables[1].Rows[0]["BANK_NUMBER"].ToString();

                    HTML.Append("<span style='text-decoration: underline;'>" + strBANK_NUMBER + "</span></td></tr>");//dynamic
                    HTML.Append("<tr><td colspan='2' align='center' valign='top' style='text-decoration: underline;'");
                
                    HTML.Append("<span style='font-size: medium;'>" + GeneratedReceiptDS.Tables[1].Rows[0]["RECEIPT_BRANCH_CITY"].ToString() + ", " + Strday + " de " + StrMonth + " de " + StrYear + " </span> ");//",31" + "de" + "Março" + "de" + "2010");//dynamic
                    HTML.Append("</td></tr>");
                    HTML.Append("<tr><td colspan='2' align='right' valign='baseline'>");
                    HTML.Append("<span style='font-size: medium;'> " + GeneratedReceiptDS.Tables[0].Rows[0]["USER_FULLNAME"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td></tr>");//dynamic

                    HTML.Append("</table>");
                    HTML.Append("</td></tr>");
                    HTML.Append("</table>");
                    HTML.Append("</td>");
                    HTML.Append("</tr>");
                    HTML.Append("</table>");//Table end
                    HTML.Append("</td></tr><tr>");
                    HTML.Append("<td align='right'>1* VIA - SEGURADO </td></tr><tr>");
                    HTML.Append("<td align='right'>-----</td></tr><tr>");
                    HTML.Append("<td>");
                    HTML.Append("<table align='center' style='width: 700px;  border: 1pt double #000000; background-color: #FFFFFF;'>");//table start
                    HTML.Append("<tr>");
                    HTML.Append("<td>");
                    HTML.Append("<table align='center' style='height: 430px; width: 700px; vertical-align: baseline; border: .1pt solid #000000;'>");
                    HTML.Append("<tr><td>");
                    HTML.Append("<table align='center' style='height: 420px; width: 674px; vertical-align: baseline; border: 1pt dotted #000000;'>");
                    // 1 TD START 
                    HTML.Append("<tr><td align='right' valign='middle' ><table><tr><td>");
                    HTML.Append("<img alt='' src=/cms/cmsweb/images/AliancaLogo.gif />");//dynamic
                    HTML.Append("</td></tr></table></td>");
                    // END
                    HTML.Append(" <td align='center' >");
                    HTML.Append("<span style='font-family:Times New Roman, Times, serif;font-weight: bold;font-size: medium;'>CAMPANHIA DE SEGUROS ALIANCA DA BAHIA</span>");//dynamic  
                    HTML.Append("<br />" + GeneratedReceiptDS.Tables[0].Rows[0]["REIN_COMAPANY_ADD1"].ToString() + " Tel.: " + GeneratedReceiptDS.Tables[0].Rows[0]["REIN_COMAPANY_PHONE"].ToString() + " - " + GeneratedReceiptDS.Tables[0].Rows[0]["REIN_COMAPANY_CITY"].ToString() + " - " + GeneratedReceiptDS.Tables[0].Rows[0]["REIN_COMAPANY_STATE"].ToString() + "<br />");
                    HTML.Append(" C.N.P.J - " + GeneratedReceiptDS.Tables[0].Rows[0]["CARRIER_CNPJ"].ToString() + "</td></tr>");//dynamic       
                    
                    HTML.Append("<tr><td colspan='2' align='center' valign='top'><hr /></td></tr>");
                    HTML.Append("<tr align='center'><tr><td colspan='2'><table><tr><td align='center' valign='top' style='width:30%'></td><td align='center'>");
                    HTML.Append("<table align='center' style='width: 238px; border: 1pt double #000000; background-color: #FFFFFF;'><tr>");
                    HTML.Append("<td align='center' style='font-size: small; font-family: Arial, Helvetica, sans-serif; font-weight: 700;'>");
                    HTML.Append("RECIBO" + " PROVISÓRIO" + "</td>");//Dynamic
                    HTML.Append("<td align='right'></td></tr></table>");
                    HTML.Append("</td><td align='center' style='width:40%'>" + GeneratedReceiptDS.Tables[1].Rows[0]["RECEIPT_NUM"].ToString() + "</td></tr></table></td></tr></tr>");//Daynamic

                    HTML.Append(" <tr><td colspan='2' align='left' valign='top'>");
                    HTML.Append(" <span style='font-size: medium;'>Recebemos de</span> <span style='text-decoration: underline;'>" + strAPPLICANT_NAME + " </span></td></tr>");//Dynamic
                    HTML.Append("<tr><td colspan='2' align='left' valign='top'>");
                    HTML.Append("<span style='font-size: medium;'> a importância de R$ </span><span style='text-decoration: underline;'>" + strReceiptAmount + "</span> ( &nbsp;<span style='text-decoration: underline;'>" + StrConvertedWordPortugueseWords + " </span>&nbsp; </td></tr>");//dynamic
                    HTML.Append("<tr><td colspan='2' align='left' valign='top'>");
                    HTML.Append("<span style='font-size: medium;'>correspondente ao pagamento da parcela </span> <span style='text-decoration: underline;'>" + strINSTALLMENT_NO + "</span></td></tr>"); //dynamic
                    HTML.Append("<tr><td colspan='2' align='left' valign='top'>");
                    HTML.Append(" <span style='font-size: medium;'>do prêmio da Apólice conforme proposta nº </span>  <span style='text-decoration: underline;'>" + strAPP_NUMBER + "</span></td></tr>");//dynamic
                    HTML.Append("<tr><td colspan='2' align='left' valign='top'>");
                    HTML.Append("<span style='font-size: medium;'>Ramo  </span><span style='text-decoration: underline;'>" + strPOLICY_LOB + "</span> em emissão.</td></tr>");
                    HTML.Append("<tr><td align='left' style='width:15%' valign='top'>");
                    HTML.Append("<span style='font-size: medium;'>Cheque nº </span>  ");//dynamic
                    HTML.Append("<span style='text-decoration: underline;'>" + strCHECK_NUM + "</span></td> ");//dynamic
                    HTML.Append("<td style='width:50%'><span style='font-size: medium;'>Banco:</span>");//dynamic
                    HTML.Append("<span style='text-decoration: underline;'>" + strBANK_NUMBER + "</span></td></tr>");//dynamic
                    HTML.Append("<tr><td colspan='2' align='center' valign='top' style='text-decoration: underline;'");
                    HTML.Append("<span style='font-size: medium;'>" + GeneratedReceiptDS.Tables[1].Rows[0]["RECEIPT_BRANCH_CITY"].ToString() + ", " + Strday + " de " + StrMonth + " de " + StrYear + " </span> ");//",31" + "de" + "Março" + "de" + "2010");//dynamic
                    HTML.Append("</td></tr>");
                    HTML.Append("<tr><td colspan='2' align='right' valign='baseline'>");
                    HTML.Append("<span style='font-size: medium;'> " + GeneratedReceiptDS.Tables[0].Rows[0]["USER_FULLNAME"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td></tr>");//dynamic

                    HTML.Append("</table>");
                    HTML.Append("</td></tr>");
                    HTML.Append("</table>");
                    HTML.Append("</td>");
                    HTML.Append("</tr>");
                    HTML.Append("</table>");//Table end
                    HTML.Append("</td>");//td end
                    HTML.Append("</tr><tr>");
                    HTML.Append("<td align='right'>2ª VIA - CONTABILIDATE (anexar cópia de depósito ou aviso de crédito) </td>");
                    HTML.Append("</tr></table>");

                    #endregion
                
                //LiteralControl includet = new LiteralControl(HTML.ToString());
                //Panel1.Controls.Add(includet);
                //btnPrint.Visible = true;
                //LblMsg.Visible = false;
                 

                #region D I S P L A Y F O R M A T COMMENTS
                // <table align="center" ><tr><td>
                //           <table align="center" style="width: 700px;  border: 1pt double #000000; background-color: #FFFFFF;">
                //    <tr>
                //        <td>
                //            <table align="center" style="height: 500px; width: 700px; vertical-align: baseline; border: .1pt solid #000000;">
                //                <tr>
                //                    <td>
                //                        <table align="center" style="height: 484px; width: 674px; vertical-align: baseline; border: 1pt dotted #000000;">
                //                        <tr><td>
                //                            <tr>
                //                                <td align="right" valign="middle" height="20px">
                //                                    <table>
                //                                        <tr>
                //                                            <td>
                //                                                <img alt="" src="logo.gif" />
                                                               
                //                                            </td>
                //                                        </tr>
                //                                    </table>
                //                                </td>
                //                                <td>
                //                                    <h3 align="center">
                //                                        <b>CAMPANHIA DE SEGUROS ALIANCA DA BAHIA</b></h3>
                //                                    <p align="center">
                //                                        Rua Pinto Martins, 11 Tel.:242-1055 - Salvador - Bahia</p>
                //                                    <p align="center">
                //                                        C.G.C. - 15.144.017/0001 - 90</p>
                //                                </td>
                //                            </tr>
                                            
                //                            <tr>
                //                                <td colspan="2" align="center" valign="top">
                //                                    <hr />
                //                                </td>
                //                            </tr>
                //                            <tr>
                //                                    <td colspan="2">
                //                                        <table>
                //                                            <tr>
                //                                                <td align="center" valign="top" style="width:30%">
                //                                                </td>
                //                                                <td align="center">
                //                                                    <table align="left" style="width: 238px; border: 1pt double #000000; background-color: #FFFFFF;">
                //                                                        <tr>
                //                                                            <td style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: large;">
                //                                                                RECIBO PROVISÓRIO
                //                                                            </td>
                //                                                            <td align="right"></td>
                //                                                        </tr>
                //                                                    </table>
                                                                   
                                                                    
                //                                                </td>
                //                                                 <td align="center" style="width:40%">05 100032
                //                                                    </td>
                //                                            </tr>
                //                                        </table>
                //                                    </td>
                //                                </tr>
                //                            <tr>
                //                                <td colspan="2" align="left" valign="top">
                //                                    Recebemos de <span style="text-decoration: underline;">dyanamic </span>
                //                                </td>
                //                            </tr>
                //                            <tr>
                //                                <td colspan="2" align="left" valign="top">
                //                                    a importância de R$ <span style="text-decoration: underline;">dynamic</span>
                //                                </td>
                //                            </tr>
                //                            <tr>
                //                                <td colspan="2" align="left" valign="top">
                //                                    correspondente ao pagamento da parcela&nbsp; <span style="text-decoration: underline;">dynamic</span>
                //                                </td>
                //                            </tr>                                
                //                            <tr>
                //                                <td colspan="2" align="left" valign="top">
                //                                    do prêmio da Apólice conforme proposta nº <span style="text-decoration: underline;">dynamic</span>
                //                                </td>
                //                            </tr>
                //                            <tr>
                //                                <td colspan="2" align="left" valign="top">
                //                                    Ramo <span style="text-decoration: underline;">hdhhg</span>
                //                                </td>
                //                            </tr>
                                            
                //                            <tr>
                //                                <td align="left" style="width:15%" valign="top">
                //                                    Cheque nº  
                //                                    <span style="text-decoration: underline;">Dynamic</span>
                //                                </td>
                //                                <td style="width:50%">
                //                               Banco:
                //                                <span style="text-decoration: underline;">Dynamic</span>
                //                                </td>
                //                            </tr>
                                            
                //                            <tr>
                //                                <td colspan="2" align="center" valign="top" style='text-decoration: underline;'>
                //                                    São Paulo, 31 de Março de 2010
                //                                </td>
                //                            </tr>
                //                            <tr>
                //                                <td colspan="2" align="right" valign="baseline">
                                                    
                //                                    ENCARREGADO DE COBRANCA&nbsp;</td>
                //                            </tr>
                //                          </td></tr>  
                //                        </table>
                //                 </td>
                //                </tr>
                //        </table>
                //        </td>
                //    </tr>
                //</table>
                //            </td></tr><tr>
                //<td align="right">1* VIA - SEGURADO </td></tr><tr>
                //<td align="right">-----</td>
                //</tr><tr>
                //<td>c</td>
                //</tr><tr>
                //<td align="right">2* VIA - CONTABILIDATE (an exar copia de deposito ou aviso de credito) </td>
                //</tr></table>
                #endregion
               
            }
            return HTML;
        }
    }
}
