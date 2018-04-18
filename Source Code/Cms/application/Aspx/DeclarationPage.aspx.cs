using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Cms.BusinessLayer.BlCommon;
using System.Web.UI.HtmlControls;
using BlGeneratePdf;
using System.IO;
using System.Xml;
using Cms.BusinessLayer.BlProcess;


namespace Cms.application.Aspx
{
    public partial class DeclarationPage : Cms.Application.appbase
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton btnOPEN_ALL;
        Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenralInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
        protected void Page_Load(object sender, EventArgs e)
        {
            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
            int CUSTOMER_ID =0;
            int POLICY_ID =0;
            int POLICY_VERSION_ID=0 ;
            int CLAIM_ID = 0;
            int ACTIVITY_ID = 0;
            string report = "";

            //Added by Pradeep Kushwaha itrack-872
            if (Request.QueryString["CALLED_FROM"] != null && Request.QueryString["CALLED_FROM"].ToString() == "VIEWER")
            {
                this.GenerateDocformTransactionLog();
            }
            else
            {
                // FOR CLAIM RECEIPT AND CLAIM REMIND LETTER
                if (Request.QueryString["CALLED_FROM"] != null && (Request.QueryString["CALLED_FROM"].ToString() == "CLM_RECEIPT" || Request.QueryString["CALLED_FROM"].ToString() == "CLM_REMIND") && Request.QueryString["Claim_ID"].ToString() != null && Request.QueryString["Activity_ID"].ToString() != null)
                {
                    // hidCLM_RECEIPT.Value    = Request.QueryString["CALLED_FROM"].ToString();
                    hidCLAIM_ID.Value = Request.QueryString["Claim_ID"].ToString();
                    hidACTIVITY_ID.Value = Request.QueryString["Activity_ID"].ToString();
                    hidCLAIM_DOC_TYPE.Value = Request.QueryString["CALLED_FROM"].ToString();

                  

                    if (Request.QueryString["CUSTOMER_ID"] != null && !int.TryParse(Request.QueryString["CUSTOMER_ID"].ToString(), out CUSTOMER_ID))
                        CUSTOMER_ID = int.Parse(GetCustomerID());

                    if (Request.QueryString["POLICY_ID"] != null && !int.TryParse(Request.QueryString["POLICY_ID"].ToString(), out POLICY_ID))
                        POLICY_ID = int.Parse(GetPolicyID());

                    if (Request.QueryString["POLICY_VERSION_ID"] != null && !int.TryParse(Request.QueryString["POLICY_VERSION_ID"].ToString(), out POLICY_VERSION_ID))
                        POLICY_VERSION_ID = int.Parse(GetPolicyVersionID()); 

                    if (!int.TryParse(hidCLAIM_ID.Value, out CLAIM_ID))
                        CLAIM_ID = 0;


                    if (!int.TryParse(hidACTIVITY_ID.Value, out ACTIVITY_ID))
                        ACTIVITY_ID = 0;
                }
                else
                {
                    if (GetCustomerID().ToString() != "")
                        CUSTOMER_ID = int.Parse(GetCustomerID());
                    if (GetPolicyID().ToString() != "")
                        POLICY_ID = int.Parse(GetPolicyID());
                    if (GetPolicyVersionID().ToString() != "")
                        POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
                }

                report = GenerateReceiptDetails(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, CLAIM_ID, ACTIVITY_ID);
            }
            //Added till here 
           
          
           LiteralControl HtmlTable = new LiteralControl(report);
           Panel1.Controls.Add(HtmlTable);
           //capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1386");
           btnOPEN_ALL.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1390");
        }

        public string GenerateReceiptDetails(int CUSTOMER_ID, int POLICY_ID,int POLICY_VERSION_ID,int CLAIM_ID,int ACTIVITY_ID)
        {
            DataSet ds = null;
            string report = "";
            
            ds = objGenralInfo.GetPolicyDocumentList(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, hidCLAIM_DOC_TYPE.Value, CLAIM_ID, ACTIVITY_ID);
            if (ds != null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0) 
            {
                report = CreateReport(ds, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, CLAIM_ID, ACTIVITY_ID);

            }
            return report;

        }
        //Added by Pradeep Kushwaha itrack-872
        private void GenerateDocformTransactionLog()
        {
            if (Request.QueryString["PN"] != null && Request.QueryString["Ext"] != null)
            {
                String pathName = Request.QueryString["PN"].ToString().ToString().Replace(" ", "+");
                String ext = Request.QueryString["Ext"].ToString().Replace(" ", "+");

                pathName = ClsCommon.DecryptString(pathName);
                ext = ClsCommon.DecryptString(ext);
                String FilePathName = String.Empty;


                
                //=======================================================
                // ADDED BY SANTOSH KUMAR GAUTAM ON 29 JUNE 2011
                // IF IS_PROCESSED FLAG IS FALSE THEN DO NOT CKECK EXISTS
                // ======================================================
                string IS_PROCESSED_FLAG = "";
                string IS_GENERETED_BOLETO = String.Empty;
                string[] strArr = ext.Split('&');
                int PRINT_JOBS_ID = 0;
                if (strArr.Length > 0)
                {
                    
                    for (int Count = 0; Count < strArr.Length; Count++)
                    {   
                        string strData = strArr[Count];

                        if (strData.IndexOf("IS_GENERETED_BOLETO=") >= 0)
                            IS_GENERETED_BOLETO = strData.Substring(strData.IndexOf("IS_GENERETED_BOLETO=") + 20, strData.Length - (strData.IndexOf("IS_GENERETED_BOLETO=") + 20));
                        
                        if (strData.IndexOf("PRINT_JOBS_ID=") >= 0)
                            int.TryParse(strData.Substring(strData.IndexOf("PRINT_JOBS_ID=") + 14, strData.Length - (strData.IndexOf("PRINT_JOBS_ID=") + 14)), out PRINT_JOBS_ID);

                        if (strData.IndexOf("IS_PROCESSED=") >= 0)
                        {
                            int Idx = strData.IndexOf("IS_PROCESSED=");
                            if (Idx >= 0)
                            {
                                IS_PROCESSED_FLAG = strData.Substring(Idx + 13, strData.Length - (Idx + 13));
                            }
                        }
                    }
                     
                }
                if (pathName.Split('=').Length > 0)
                    FilePathName = pathName.Split('=')[0].ToString();
                else
                    FilePathName = pathName;

                string FilePath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + FilePathName;
             
                //Beginigng the impersonation 
                Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new ClsAttachment();

                //if IS_GENERETED_BOLETO=Y then check the boleto exists or not
                if ((IS_PROCESSED_FLAG == "Y" || IS_PROCESSED_FLAG == "") &&
                    (IS_GENERETED_BOLETO == "Y" || IS_GENERETED_BOLETO == "") && 
                    objAttachment.IsFileExists(FilePath))
                {
                    string str = "/cms/CmsWeb/aspx/Viewer.aspx?PN=" + Request.QueryString["PN"].ToString().ToString().Replace(" ", "+")
                                     + "&Ext=" + Request.QueryString["Ext"].ToString().Replace(" ", "+") + "&";
                    Response.Redirect(str);
                    return;
                }
                else
                {
                    try
                    {

                        // REPLACE IS_PROCESSED FLAG TO TRUE TO AVOID REGENARATE OF DOCUMENT

                      ext=  ext.Replace("IS_PROCESSED=N", "IS_PROCESSED=Y");


                        int CUSTOMER_ID = 0;
                        int POLICY_ID = 0;
                        int POLICY_VERSION_ID = 0;
                        int CLAIM_ID = 0;
                        int ACTIVITY_ID = 0;
                        String DOC_CODE = String.Empty;

                        String[] strpathName = pathName.Substring(pathName.LastIndexOf('\\') == -1 ? pathName.LastIndexOf('/') : pathName.LastIndexOf('\\')).Split('_');

                        if (ChackValidNumber(strpathName[1]))
                        {
                            CUSTOMER_ID = int.Parse(strpathName[1]);
                            POLICY_ID = int.Parse(strpathName[2]);
                            POLICY_VERSION_ID = int.Parse(strpathName[3]);
                        }
                        else
                        {
                            CUSTOMER_ID = int.Parse(strpathName[2]);
                            POLICY_ID = int.Parse(strpathName[3]);
                            POLICY_VERSION_ID = int.Parse(strpathName[4]);

                        }
                        if (ext.Split('=').Length > 1)
                            DOC_CODE = ext.Split('=')[1].Split('&')[0].ToString();

                        // FOR CLAIM ID AND ACTIVITY ID
                        string[] ClaimData = ext.Split('?');
                        if (ClaimData.Length > 1)
                        {
                            DOC_CODE = ClaimData[0].Substring(ClaimData[0].LastIndexOf("PDF=") + 4, ClaimData[0].Length - (ClaimData[0].LastIndexOf("PDF=") + 4));

                            string[] ClaimDetails = ClaimData[1].Split('&');
                            if (ClaimDetails.Length > 0 && ClaimDetails[0].StartsWith("CLAIMID"))
                            {
                                int ClaimLocIndex = ClaimDetails[0].LastIndexOf("CLAIMID=");
                                int ActivityLocIndex = ClaimDetails[1].LastIndexOf("ACTIVITYID=");
                                string strClaimID = ClaimDetails[0].Substring(ClaimLocIndex + 8, ClaimDetails[0].Length - (ClaimLocIndex + 8));
                                string strActivityID = ClaimDetails[1].Substring(ActivityLocIndex + 11, ClaimDetails[1].Length - (ActivityLocIndex + 11));

                                CLAIM_ID = int.Parse(strClaimID);
                                ACTIVITY_ID = int.Parse(strActivityID);

                            }
                        }

                        if (GenerateDocument(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DOC_CODE, CLAIM_ID, ACTIVITY_ID,PRINT_JOBS_ID))
                        {
                            ext = ext.Replace("IS_GENERETED_BOLETO=N", "IS_GENERETED_BOLETO=Y");//Replace IS_GENERETED_BOLETO=N to Y if the Boleto has been generated sucessfully 
                            ext=  ClsCommon.EncryptString(ext);
                            string str = "/cms/CmsWeb/aspx/Viewer.aspx?PN=" + Request.QueryString["PN"].ToString().ToString().Replace(" ", "+")
                                     + "&Ext=" + ext; //Request.QueryString["Ext"].ToString().Replace(" ", "+") + "&";
                            Response.Redirect(str);
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            btnOPEN_ALL.Visible = false;
                            return;
                        }


                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                        lblMessage.Visible = true;
                        btnOPEN_ALL.Visible = false;
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                        return;
                    }
                }
            }
        }
        private Boolean ChackValidNumber(String value)
        {
            Boolean retvalue = false;
            try
            {
                int.Parse(value);
                retvalue = true;
            }
            catch// (Exception ex)
            {
                retvalue = false;
            }
            return retvalue;
            
        }        
        //Added by Pradeep Kushwaha itrack-872
        private Boolean GenerateDocument(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, String DOC_CODE, int CLAIM_ID, int ACTIVITY_ID,int PRINT_JOB_ID)
        {
            ClsProductPdfXml objGeneratePdfMotor = new ClsProductPdfXml();
            objGeneratePdfMotor.generateDocuments_Motor(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DOC_CODE, CLAIM_ID, ACTIVITY_ID, PRINT_JOB_ID);
           //GeneratePDFDocument( CUSTOMER_ID,  POLICY_ID,  POLICY_VERSION_ID,  DOC_CODE,  CLAIM_ID,  ACTIVITY_ID,  PRINT_JOB_ID);
            return true;
            Boolean retValue = false;
            String strExceptionLog = String.Empty;
            String strErrorOutputDesc = String.Empty;
            try
            {
                //Object declaration declare 
                //Initialization of

                ClsGeneratePdf objGeneratePdf = new ClsGeneratePdf();
                strExceptionLog = "Object declaration of ClsGeneratePdf();";
                objGeneratePdf.ConnStr = ClsCommon.ConnStr;
                strExceptionLog += "Connection string Initialization;";

                String strPdfTemplatePath = System.Configuration.ConfigurationManager.AppSettings.Get("PdfTemplatePath").ToString();
                strExceptionLog += "To get strPdfTemplatePath;" + strPdfTemplatePath;
                objGeneratePdf.RTF_To_Html = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "RTF_HTML");
                strExceptionLog += "To get RTF_To_Html;" + objGeneratePdf.RTF_To_Html;
                #region Comment code 
                ////First 
                //string WebURl=  System.Configuration.ConfigurationManager.AppSettings.Get("CmsWebUrl");
                //WebURl = WebURl.ToUpper().Replace("CMS/CMSWEB/", "");
                //string ClauseFilePath = WebURl + @"/ClausesAttachment/";
                //objGeneratePdf.ClauseFilePath = ClauseFilePath;
                //objGeneratePdf.PdfPath = WebURl + @"/upload";
                ////
                #endregion
                
                //Second

                objGeneratePdf.ClauseFilePath = System.Configuration.ConfigurationManager.AppSettings.Get("ClausePath");
                strExceptionLog += "To get ClauseFilePath;" + objGeneratePdf.ClauseFilePath;
                objGeneratePdf.PdfPath = System.Configuration.ConfigurationManager.AppSettings.Get("PdfPath");
                strExceptionLog += "To get PdfPath;" + objGeneratePdf.PdfPath;
                //
                objGeneratePdf.FinalPDFPath = "final";
                objGeneratePdf.CompanyLogo = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "CompanyLogo");
                objGeneratePdf.BoletoCssPath = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "BoletoPDFCss");
                objGeneratePdf.BarcodeImagePath = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "BarcodeImagePath");
                objGeneratePdf.ImagePath = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "LogoPath");
                //114
                objGeneratePdf.Xsl_Coverage_114_Comprehensive_Dwelling = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_114_Comprehensive_Dwelling");
                objGeneratePdf.Xsl_Coverage_114_More_Than_One_Broker_Dwelling = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_114_More_Than_One_Broker_Dwelling");
                objGeneratePdf.Xsl_Coverage_114_More_Than_One_Broker__With_Ceded_COI_Dwelling = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_114_More_Than_One_Broker__With_Ceded_COI_Dwelling");
                //116
                objGeneratePdf.Xsl_Coverage_116_Comprehensive_Condominium =ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_116_Comprehensive_Condominium");
                objGeneratePdf.Xsl_Coverage_116_More_Than_One_Broker_Condominium = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath,"Xsl_Coverage_116_More_Than_One_Broker_Condominium");
                objGeneratePdf.Xsl_Coverage_116_More_Than_One_Broker__With_Ceded_COI_Condominium = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_116_More_Than_One_Broker__With_Ceded_COI_Condominium");

                //118
                objGeneratePdf.Xsl_Coverage_118_Comprehensive_Company = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_118_Comprehensive_Company");
                objGeneratePdf.Xsl_Coverage_118_More_Than_One_Broker_Company = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_118_More_Than_One_Broker_Company");
                objGeneratePdf.Xsl_Coverage_118_More_Than_One_Broker__With_Ceded_COI_Company = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_118_More_Than_One_Broker__With_Ceded_COI_Company");

                //171
                objGeneratePdf.xsl_Coverage_171_Diversified_Risk = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "xsl_Coverage_171_Diversified_Risk");
                objGeneratePdf.xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker");
                objGeneratePdf.xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker_With_Ceded_COI = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker_With_Ceded_COI");


                //Endorsment 118,116,114
                objGeneratePdf.Endo_Type_21_0114_0116_0118_0196_0115_0167 = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167");
                objGeneratePdf.XslFilepathCover_Endorsement_Type_21_0114 = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "xslFilepathCover_Endorsement_Type_21_0114");

                objGeneratePdf.Xsl_Coverage_D_012_Endorsement_type_0993_0982_0520_v2 = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_D_012_Endorsement_type_0993_0982_0520_v2");
                
                //Added new key for LOB 22
                objGeneratePdf.Xsl_Coverage_D_037_Endorsement_type_31_Product_0520_v1 = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_D_037_Endorsement_type_31_Product_0520_v1");
                //For Endorsement Cancellation,itrack 1449,modified by naveen
                objGeneratePdf.Xsl_Cover_Cancel_Policy = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Cover_Cancel_Policy");
                objGeneratePdf.Xsl__header_Cancel_Policy = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl__header_Cancel_Policy");

                objGeneratePdf.Xsl_Coverage_523_Liability_Transportation = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_523_Liability_Transportation");
                objGeneratePdf.Xsl_Coverage_523_Liability_Transportation_withPremium = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_523_Liability_Transportation_withPremium");
                objGeneratePdf.Xsl_Coverage_553_Facultive_Liability = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_553_Facultive_Liability");

                objGeneratePdf.Xsl_Coverage_981_Individual_personal_accident = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Coverage_981_Individual_personal_accident");

                objGeneratePdf.CoverXslNewBusiness_Path = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "xslFilepathCover");
                objGeneratePdf.HeaderXslNewBusiness_Path = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "xslFilepathHeader");
                objGeneratePdf.CoverXSLEndorsement_Path = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "xslFilepathCover_Endorsement");
                objGeneratePdf.HeaderXSLEdorsement_Path = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "xslFilepathHeader_Endorsement");
                objGeneratePdf.Xsl_Cover_Renewal = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Cover_Renewal");
                objGeneratePdf.Xsl_Header_Renewal = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Xsl_Header_Renewal");
                objGeneratePdf.Xsl_COI_More_Thn_One_Broker_Policy = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "COI_More_Thn_One_Broker_Policy");
                objGeneratePdf.Xsl_COI_More_Thn_One_Broker_Policy_Endorsement = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "COI_More_Thn_One_Broker_Policy_Endorsement");

                objGeneratePdf.Endorsement_type_31_523_Civil_Liability_Transportation = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Endorsement_type_31_523_Civil_Liability_Transportation"); 
                objGeneratePdf.Endorsement_type_31_553_D_015_Facultive_Liability = ClsCommon.GetPdfTemplatePath(strPdfTemplatePath, "Endorsement_type_31_553_D_015_Facultive_Liability");


                objGeneratePdf.Impersonate_Inactive = "1"; //ConfigurationManager.AppSettings.Get("Impersonate");
                objGeneratePdf.IUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                objGeneratePdf.IPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                objGeneratePdf.IDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");
                Int32 returnValue = 0;
                strExceptionLog += "Calling GeneratePolicyDoc_OntheFly;" + DOC_CODE;
                returnValue = objGeneratePdf.GeneratePolicyDoc_OntheFly(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DOC_CODE, CLAIM_ID, ACTIVITY_ID, 2, out strErrorOutputDesc, PRINT_JOB_ID);
                if (returnValue == -2)
                    return false;
                else if(returnValue!=0)
                    retValue = true;
                 
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                throw (new Exception(ex.Message.ToString() + strExceptionLog + strErrorOutputDesc));

            }
            return retValue;
        }
        //Added till here 
        private string CreateReport(DataSet ds, int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID,int CLAIM_ID, int ACTIVITY_ID)
        {
            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());

            string strIS_PROCESSED="";
        
                StringBuilder HTML = new StringBuilder();
                try
                {

                HTML.Append("<table class='tableWidthHeader' id='Table1' cellspacing='0' cellpadding='0' width='100%' border='0'>");
                HTML.Append("<tr>");
                if(CLAIM_ID!=0 ||ACTIVITY_ID!=0)
                HTML.Append("<td class='headereffectCenter'  colspan='4'>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2025") + "</td>");
                    else
                HTML.Append("<td class='headereffectCenter'  colspan='4'>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1386") + "</td>");
                HTML.Append("</tr>");
                HTML.Append("<tr>");
                HTML.Append("<td class='midcolorba'>");
                HTML.Append("</td>");
                HTML.Append("<td class='midcolorba' ><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1387") + "</b></td>");
                HTML.Append("<td class='midcolorba' ><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1388") + "</b></td>");
                HTML.Append("<td class='midcolorba' ><b>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1389") + "</b></td>");
                HTML.Append("</tr>");

                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    if (dr["IS_PROCESSED"].ToString() == "True")
                        strIS_PROCESSED = "Y";
                    else
                        strIS_PROCESSED = "N";

                    String File = dr["FILE_NAME"].ToString();//"BROKER_2568_25_1_2184_634292490518908285.pdf";
                    string filename = Server.MapPath(dr["URL_PATH"].ToString()) + "\\" + File; //dr["FILE_NAME"]; // hidRootPath.Value + ClsCommon.FetchValueFromXML("ATTACH_FILE_NAME", hidOldData.Value);
                    int startOfFile = filename.IndexOf("Upload");
                    string filePath = filename.Substring(startOfFile + 6);
                    string DOC_CODE = dr["DOC_CODE"].ToString();
                    string PRINT_JOBS_ID = dr["PRINT_JOBS_ID"].ToString();
                    
                   // filePath = filePath + "=" + DOC_CODE+"?CLAIMID="+hidCLAIM_ID.Value+"&ACTIVITYID="+hidACTIVITY_ID.Value;
                    if (hidCLAIM_ID.Value != null && hidCLAIM_ID.Value != "" && hidCLAIM_ID.Value != "0")
                    {
                        filePath = filePath + "=" + DOC_CODE + "?CLAIMID=" + hidCLAIM_ID.Value + "&ACTIVITYID=" + hidACTIVITY_ID.Value + "&IS_PROCESSED=" + strIS_PROCESSED;
                    }
                    else
                        filePath = filePath + "=" + DOC_CODE + "&IS_PROCESSED=" + strIS_PROCESSED;

                    //Added by Pradeep Kushwaha on 30 June-2011 - itrack -1125 (Watermark on Boletos)
                    //
                    string IS_GENERETED_BOLETO = "Y";
                    //if DOC_CODE=Boleto then get the installment details with release status
                    if (DOC_CODE.ToUpper().Trim() == "BOLETO")
                    {
                       DataSet dsInstallDetails = objGenralInfo.GetInstallmentDetails(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);
                       foreach (DataRow drInstall in dsInstallDetails.Tables[0].Rows)
                       {
                           //if release status found with Y status
                           if (drInstall["RELEASED_STATUS"].ToString() != "" && drInstall["RELEASED_STATUS"].ToString() == "Y")
                           {
                               IS_GENERETED_BOLETO = "N";//set IS_GENERETED_BOLETO=N to regenerate the boletos
                               filePath += "&IS_GENERETED_BOLETO=" + IS_GENERETED_BOLETO;
                               break;
                           }
                       }
                    }
                    //Added till here 
                    filePath += "&PRINT_JOBS_ID=" + PRINT_JOBS_ID;
                    string[] fileURL = filePath.Split('.');

                    
                    string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
                    String EncryptdPth = EncryptedPath;

                    //String FinalLink = "<a  href = '" + EncryptdPth + "' target='blank' name='FilePATH' id='" + i.ToString() + "_lnkFilePATH'>" + File + "</a>";
                    String FinalLink = "<a  href = '" + EncryptdPth + "' target='blank' name='FilePATH' id='" + i.ToString() + "_lnkFilePATH'>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2003") + "</a>";


                    HTML.Append("<tr>");
                    HTML.Append("<td class='midcolorba'><input type='checkbox' name='chk_pol_doc' id='" + i.ToString() + "_chkFilePATH' />");
                    
                    HTML.Append("</td>");
                    HTML.Append("<td class='midcolorba'>" + dr["ENTITY_TYPE"]);
                    HTML.Append("</td>");
                    HTML.Append("<td class='midcolorba' align=left>" + dr["DOCUMENT_CODE"]);
                    HTML.Append("</td>");
                    HTML.Append("<td class='midcolorba'>" + FinalLink + "</td>");
                    HTML.Append("</td>");
                    HTML.Append("</tr>");

                    i += 1;
                }
                HTML.Append("</table>");
              //  return HTML.ToString();

            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

             return HTML.ToString();
        }
    }
}

